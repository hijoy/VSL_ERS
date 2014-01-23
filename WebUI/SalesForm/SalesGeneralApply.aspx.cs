using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessObjects;
using BusinessObjects.FormDSTableAdapters;
using System.Collections.Generic;

public partial class SaleForm_SalesGeneralApply : BasePage {

    decimal manualApplyFeeTotal = 0;
    decimal inContractAmount = 0;
    Dictionary<int, object[]> ExpenseItemRemainTimes;

    private SalesApplyBLL m_SalesApplyBLL;
    protected SalesApplyBLL SalesApplyBLL {
        get {
            if (this.m_SalesApplyBLL == null) {
                this.m_SalesApplyBLL = new SalesApplyBLL();
            }
            return this.m_SalesApplyBLL;
        }
    }

    private FormDS m_InnerDS;
    public FormDS InnerDS {
        get {
            if (this.ViewState["FormDS"] == null) {
                this.ViewState["FormDS"] = new FormDS();
            }
            return (FormDS)this.ViewState["FormDS"];
        }
    }

    #region 页面初始化及事件处理

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this.Page, "方案申请");
            this.Page.Title = "方案申请";

            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow rowUserPosition = (AuthorizationDS.PositionRow)Session["Position"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = rowUserPosition.PositionId;
            this.ViewState["FlowTemplate"] = new AuthorizationBLL().GetFlowTemplate((int)SystemEnums.BusinessUseCase.FormApply, stuffUser.StuffUserId);

            this.StuffNameCtl.Text = stuffUser.StuffName;
            this.PositionNameCtl.Text = rowUserPosition.PositionName;
            this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowUserPosition.OrganizationUnitId).OrganizationUnitName;
            this.ViewState["DepartmentID"] = rowUserPosition.OrganizationUnitId;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();

            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());

            }
            //如果是草稿进行赋值
            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
                OpenForm(int.Parse(this.ViewState["ObjectId"].ToString()));
            } else {
                this.DeleteBtn.Visible = false;
                if (Request["CustomerID"] != null) {
                    this.ViewState["CustomerID"] = Request["CustomerID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到客户，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["BeginPeriod"] != null && Request["EndPeriod"] != null) {
                    this.ViewState["BeginPeriod"] = Request["BeginPeriod"];
                    this.ViewState["EndPeriod"] = Request["EndPeriod"];
                } else {
                    this.Session["ErrorInfor"] = "没有费用期间，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["ExpenseSubCategoryID"] != null) {
                    this.ViewState["ExpenseSubCategoryID"] = Request["ExpenseSubCategoryID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到费用小类，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }

                this.BeginPeriodCtl.Text = DateTime.Parse(this.ViewState["BeginPeriod"].ToString()).ToString("yyyy-MM");
                this.EndPeriodCtl.Text = DateTime.Parse(this.ViewState["EndPeriod"].ToString()).ToString("yyyy-MM");
                this.ExpenseSubCategoryCtl.Text = new MasterDataBLL().GetExpenseSubCateNameById(int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString()));
                ERS.CustomerRow customer = new MasterDataBLL().GetCustomerById(int.Parse(this.ViewState["CustomerID"].ToString()));
                this.CustomerNameCtl.Text = customer.CustomerName;
                //this.CustomerTypeCtl.Text = new MasterDataBLL().GetCustomerTypeById(customer.CustomerTypeID).CustomerTypeName;
                this.odsShop.SelectParameters["CustomerID"].DefaultValue = customer.CustomerID.ToString();
                CommonUtility.InitSplitRate(InnerDS.FormApplySplitRate, DateTime.Parse(this.ViewState["BeginPeriod"].ToString()), DateTime.Parse(this.ViewState["EndPeriod"].ToString()));
            }
            this.ExpenseSubCategoryID.Value = this.ViewState["ExpenseSubCategoryID"].ToString();
            //判断费用期间是否正确
            MasterDataBLL bll = new MasterDataBLL();
            if (!bll.IsValidApplyYear(DateTime.Parse(this.ViewState["BeginPeriod"].ToString()).AddMonths(-3).Year)) {
                this.SubmitBtn.Visible = false;
                PageUtility.ShowModelDlg(this, "不允许申请本财年项目，请联系财务部!");
                return;
            }

            if (InnerDS.FormApplySplitRate != null && InnerDS.FormApplySplitRate.Count > 0) {
                this.divSplitRate.Visible = true;
                this.gvSplitRate.Visible = true;
                this.gvSplitRate.DataSource = InnerDS.FormApplySplitRate;
                this.gvSplitRate.DataBind();
            }

            //
            this.txtEstimatedSaleVolume.Attributes.Add("onBlur", "GetPackageUnitPrice('" + this.txtPackageUnitPrice.ClientID + "',this)");

            //预算信息
            decimal[] calculateAssistant = new decimal[14];
            calculateAssistant = new BudgetBLL().GetSalesBudgetByParameter(int.Parse(this.ViewState["CustomerID"].ToString()), DateTime.Parse(this.ViewState["BeginPeriod"].ToString()), int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString()));
            this.CustomerBudgetCtl.Text = calculateAssistant[0].ToString("N");
            this.CustomerBudgetRemainCtl.Text = calculateAssistant[5].ToString("N");
            this.OUBudgetCtl.Text = calculateAssistant[7].ToString("N");
            this.OUApprovedAmountCtl.Text = calculateAssistant[8].ToString("N");
            this.OUApprovingAmountCtl.Text = calculateAssistant[9].ToString("N");
            this.OUCompletedAmountCtl.Text = calculateAssistant[10].ToString("N");
            this.OUReimbursedAmountCtl.Text = calculateAssistant[11].ToString("N");
            this.OUBudgetRemainCtl.Text = calculateAssistant[12].ToString("N");
            this.OUBudgetRateCtl.Text = ((decimal)(calculateAssistant[13] * 100)).ToString("N") + "%";
            InitCustomerTimesLimit();

            if (Session["ProxyStuffUserId"] != null) {
                this.SubmitBtn.Visible = false;
            }

        } else {
            this.ExpenseItemRemainTimes = (Dictionary<int, object[]>)this.ViewState["ExpenseItemRemainTimes"];
        }


        //隐藏预算信息
        this.OUBudgetCtl.Text = "";
        this.OUApprovedAmountCtl.Text = "";
        this.OUApprovingAmountCtl.Text = "";
        this.OUCompletedAmountCtl.Text = "";
        this.OUReimbursedAmountCtl.Text = "";
        this.OUBudgetRemainCtl.Text = "";
        this.OUBudgetRateCtl.Text = "";

    }

    protected void OpenForm(int formID) {
        FormTableAdapter taForm = new FormTableAdapter();
        taForm.FillByID(this.InnerDS.Form, formID);
        FormDS.FormRow rowForm = this.InnerDS.Form[0];
        FormApplyTableAdapter taFormApply = new FormApplyTableAdapter();
        taFormApply.FillByID(this.InnerDS.FormApply, formID);
        FormDS.FormApplyRow rowFormApply = this.InnerDS.FormApply[0];
        //赋值
        this.ViewState["BeginPeriod"] = rowFormApply.BeginPeriod.ToShortDateString();
        this.ViewState["EndPeriod"] = rowFormApply.EndPeriod.ToShortDateString();
        this.BeginPeriodCtl.Text = rowFormApply.BeginPeriod.ToString("yyyy-MM");
        this.EndPeriodCtl.Text = rowFormApply.EndPeriod.ToString("yyyy-MM");
        this.ViewState["ExpenseSubCategoryID"] = rowFormApply.ExpenseSubCategoryID.ToString();
        this.ExpenseSubCategoryCtl.Text = new MasterDataBLL().GetExpenseSubCateNameById(rowFormApply.ExpenseSubCategoryID);
        this.ViewState["CustomerID"] = rowFormApply.CustomerID.ToString();
        ERS.CustomerRow customer = new MasterDataBLL().GetCustomerById(rowFormApply.CustomerID);
        this.CustomerNameCtl.Text = customer.CustomerName;
        //this.CustomerTypeCtl.Text = new MasterDataBLL().GetCustomerTypeById(customer.CustomerTypeID).CustomerTypeName;
        this.odsShop.SelectParameters["CustomerID"].DefaultValue = customer.CustomerID.ToString();

        this.ShopDDL.SelectedValue = rowFormApply.ShopID.ToString();
        this.PaymentTypeDDL.SelectedValue = rowFormApply.PaymentTypeID.ToString();
        if (!rowFormApply.IsContractNoNull()) {
            this.ContractNoCtl.Text = rowFormApply.ContractNo;
        }
        if (!rowFormApply.IsRemarkNull()) {
            this.RemarkCtl.Text = rowFormApply.Remark;
        }
        if (!rowFormApply.IsAttachedFileNameNull())
            this.UCFileUpload.AttachmentFileName = rowFormApply.AttachedFileName;
        if (!rowFormApply.IsRealAttachedFileNameNull())
            this.UCFileUpload.RealAttachmentFileName = rowFormApply.RealAttachedFileName;

        if (!rowFormApply.IsPromotionBeginDateNull()) {
            this.UCPromotionBegin.SelectedDate = rowFormApply.PromotionBeginDate.ToString("yyyy-MM-dd");
        }
        if (!rowFormApply.IsPromotionEndDateNull()) {
            this.UCPromotionEnd.SelectedDate = rowFormApply.PromotionEndDate.ToString("yyyy-MM-dd");
        }
        this.PromotionScopeDDL.SelectedValue = rowFormApply.PromotionScopeID.ToString();
        this.PromotionTypeDDL.SelectedValue = rowFormApply.PromotionTypeID.ToString();
        if (!rowFormApply.IsPromotionDescNull()) {
            this.PromotionDescCtl.Text = rowFormApply.PromotionDesc;
        }
        this.ShelfTypeDDL.SelectedValue = rowFormApply.ShelfTypeID.ToString();
        if (!rowFormApply.IsFirstVolumeNull()) {
            this.FirstVolumeCtl.Text = rowFormApply.FirstVolume.ToString();
        }
        if (!rowFormApply.IsSecondVolumeNull()) {
            this.SecondVolumeCtl.Text = rowFormApply.SecondVolume.ToString();
        }
        if (!rowFormApply.IsThirdVolumeNull()) {
            this.ThirdVolumeCtl.Text = rowFormApply.ThirdVolume.ToString();
        }
        if (!rowFormApply.IsEstimatedSaleVolumeNull()) {
            this.txtEstimatedSaleVolume.Text = rowFormApply.EstimatedSaleVolume.ToString();
        }
        if (!rowFormApply.IsPackageUnitPriceNull()) {
            this.txtPackageUnitPrice.Text = rowFormApply.PackageUnitPrice.ToString("N");
        }
        if (!rowFormApply.IsFormApplyNameNull()) {
            this.txtFormApplyName.Text = rowFormApply.FormApplyName.ToString();
        }
        if (!rowFormApply.IsReimburseRequirementsNull()) {
            int ReimburseRequirement = rowFormApply.ReimburseRequirements;
            if ((ReimburseRequirement & (int)SystemEnums.ReimburseRequirements.Picture) == (int)SystemEnums.ReimburseRequirements.Picture) {
                chkListReimburseRequirements.Items[0].Selected = true;
            }
            if ((ReimburseRequirement & (int)SystemEnums.ReimburseRequirements.Agreement) == (int)SystemEnums.ReimburseRequirements.Agreement) {
                chkListReimburseRequirements.Items[1].Selected = true;
            }
            if ((ReimburseRequirement & (int)SystemEnums.ReimburseRequirements.DeliveryOrder) == (int)SystemEnums.ReimburseRequirements.DeliveryOrder) {
                chkListReimburseRequirements.Items[2].Selected = true;
            }
            if ((ReimburseRequirement & (int)SystemEnums.ReimburseRequirements.Contract) == (int)SystemEnums.ReimburseRequirements.Contract) {
                chkListReimburseRequirements.Items[3].Selected = true;
            }
            if ((ReimburseRequirement & (int)SystemEnums.ReimburseRequirements.DM) == (int)SystemEnums.ReimburseRequirements.DM) {
                chkListReimburseRequirements.Items[4].Selected = true;
            }
            if ((ReimburseRequirement & (int)SystemEnums.ReimburseRequirements.Other) == (int)SystemEnums.ReimburseRequirements.Other) {
                chkListReimburseRequirements.Items[5].Selected = true;
            }
        }
        // 打开明细表
        FormApplyDetailViewTableAdapter taDetail = new FormApplyDetailViewTableAdapter();
        taDetail.FillByFormID(this.InnerDS.FormApplyDetailView, formID);
        new FormApplySplitRateTableAdapter().FillByApplyID(InnerDS.FormApplySplitRate, formID);
    }

    #endregion

    protected void CancelBtn_Click(object sender, EventArgs e) {
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.SalesApplyBLL.DeleteFormApply(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        if (!IsInputValid())
            return;
        SaveFormApply(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, ImageClickEventArgs e) {
        if (!IsInputValid())
            return;
        if (!IsSubmitValid())
            return;
        SaveFormApply(SystemEnums.FormStatus.Awaiting);
    }

    protected void SaveFormApply(SystemEnums.FormStatus StatusID) {
        decimal? CustomerBudget = null;
        decimal? CustomerBudgetRemain = null;
        decimal? OUBudget = null;
        decimal? OUAppovedAmount = null;
        decimal? OUApprovingAmount = null;
        decimal? OUCompletedAmount = null;
        decimal? OUReimbursedAmount = null;
        decimal? OUBudgetRemain = null;
        decimal? OUBudgetRate = null;

        if (StatusID == SystemEnums.FormStatus.Awaiting) {//提交时检查，保存草稿不检查

            //首先检查合同内金额是否超限
            object[] amountLimit = this.SalesApplyBLL.GetCustomerAmountLimitByParameter(int.Parse(this.ViewState["CustomerID"].ToString()), DateTime.Parse(this.ViewState["BeginPeriod"].ToString()).Year, decimal.Parse(this.ViewState["InContractAmount"].ToString()));
            if (amountLimit != null) {
                if (!(bool)amountLimit[0]) {
                    PageUtility.ShowModelDlg(this.Page, string.Format("您当前选择的客户的合同内金额超出限制（剩余金额{0}）", amountLimit[3].ToString()));
                    return;
                }
            }

            decimal[] calculateAssistant = new decimal[14];
            calculateAssistant = new BudgetBLL().GetSalesBudgetByParameter(int.Parse(this.ViewState["CustomerID"].ToString()), DateTime.Parse(this.ViewState["BeginPeriod"].ToString()), int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString()));
            if (calculateAssistant[12] < decimal.Parse(this.ViewState["ManualApplyFeeTotal"].ToString())) {
                PageUtility.ShowModelDlg(this.Page, "本次申请金额超过可用预算，不能提交");
                return;
            } else {
                CustomerBudget = calculateAssistant[0];
                CustomerBudgetRemain = calculateAssistant[5];
                OUBudget = calculateAssistant[7];
                OUAppovedAmount = calculateAssistant[8];
                OUApprovingAmount = calculateAssistant[9];
                OUCompletedAmount = calculateAssistant[10];
                OUReimbursedAmount = calculateAssistant[11];
                OUBudgetRemain = calculateAssistant[12];
                OUBudgetRate = calculateAssistant[13];
            }
        }

        this.SalesApplyBLL.FormDataSet = this.InnerDS;
        int? RejectedFormID = null;
        if (this.ViewState["RejectedObjectID"] != null) {
            RejectedFormID = (int)this.ViewState["RejectedObjectID"];
        }

        int UserID = (int)this.ViewState["StuffUserID"];
        int? ProxyStuffUserId = null;
        if (Session["ProxyStuffUserId"] != null) {
            ProxyStuffUserId = int.Parse(Session["ProxyStuffUserId"].ToString());
        }
        int OrganizationUnitID = (int)this.ViewState["DepartmentID"];
        int PositionID = (int)this.ViewState["PositionID"];
        DateTime BeginPeriod = DateTime.Parse(this.ViewState["BeginPeriod"].ToString());
        DateTime EndPeriod = DateTime.Parse(this.ViewState["EndPeriod"].ToString());
        int CustomerID = int.Parse(this.ViewState["CustomerID"].ToString());
        int ShopID = int.Parse(this.ShopDDL.SelectedValue);
        int PaymentTypeID = int.Parse(this.PaymentTypeDDL.SelectedValue);
        int ExpenseSubCategoryID = int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString());
        string ContractNo = this.ContractNoCtl.Text;
        DateTime PromotionBeginDate = DateTime.Parse(this.UCPromotionBegin.SelectedDate);
        DateTime PromotionEndDate = DateTime.Parse(this.UCPromotionEnd.SelectedDate);
        int PromotionScopeID = int.Parse(this.PromotionScopeDDL.SelectedValue);
        int PromotionTypeID = int.Parse(this.PromotionTypeDDL.SelectedValue);
        string PromotionDesc = this.PromotionDescCtl.Text;
        int ShelfTypeID = int.Parse(this.ShelfTypeDDL.SelectedValue);
        int? FirstVolume = null;
        if (this.FirstVolumeCtl.Text != string.Empty) {
            FirstVolume = int.Parse(this.FirstVolumeCtl.Text);
        }
        int? SecondVolume = null;
        if (this.SecondVolumeCtl.Text != string.Empty) {
            SecondVolume = int.Parse(this.SecondVolumeCtl.Text);
        }
        int? ThirdVolume = null;
        if (this.ThirdVolumeCtl.Text != string.Empty) {
            ThirdVolume = int.Parse(this.ThirdVolumeCtl.Text);
        }
        int? EstimatedSaleVolume = null;
        if (this.txtEstimatedSaleVolume.Text != string.Empty) {
            EstimatedSaleVolume = int.Parse(this.txtEstimatedSaleVolume.Text);
        }

        string AttachedFileName = this.UCFileUpload.AttachmentFileName;
        string RealAttachedFileName = this.UCFileUpload.RealAttachmentFileName;
        string Remark = this.RemarkCtl.Text;
        string FormApplyName = this.txtFormApplyName.Text;

        //提交时判断跨月方案分摊比例是否正常
        if (StatusID == SystemEnums.FormStatus.Awaiting && EndPeriod.Month != BeginPeriod.Month) {
            if (InnerDS.FormApplySplitRate == null || InnerDS.FormApplySplitRate.Count == 0) {
                PageUtility.ShowModelDlg(this, "方案分摊比例异常，请重新填写！");
                return;
            } else {
                int sum = 0;
                foreach (FormDS.FormApplySplitRateRow item in InnerDS.FormApplySplitRate) {
                    sum += item.Rate;
                }
                if (sum != 100) {
                    PageUtility.ShowModelDlg(this, "方案分摊比例总和异常，请重新填写！");
                    return;
                }
            }
        }

        //预算信息

        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.SalesApplyBLL.AddFormApplyGeneral(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.SalesApply, StatusID, BeginPeriod,
                    EndPeriod, CustomerID, ShopID, PaymentTypeID, ExpenseSubCategoryID, ContractNo, PromotionBeginDate, PromotionEndDate,
                    PromotionScopeID, PromotionTypeID, PromotionDesc, ShelfTypeID, FirstVolume, SecondVolume, ThirdVolume, EstimatedSaleVolume, CustomerBudget, CustomerBudgetRemain, OUBudget, OUAppovedAmount, OUApprovingAmount, OUCompletedAmount,
                    OUReimbursedAmount, OUBudgetRemain, OUBudgetRate, AttachedFileName, RealAttachedFileName, Remark, int.Parse(ViewState["ReimburseRequirements"].ToString()), this.ViewState["FlowTemplate"].ToString(), FormApplyName);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.SalesApplyBLL.UpdateFormApplyGeneral(FormID, StatusID, SystemEnums.FormType.SalesApply, ShopID, PaymentTypeID, ContractNo, PromotionBeginDate,
                    PromotionEndDate, PromotionScopeID, PromotionTypeID, PromotionDesc, ShelfTypeID, FirstVolume, SecondVolume, ThirdVolume, EstimatedSaleVolume, CustomerBudget, CustomerBudgetRemain,
                    OUBudget, OUAppovedAmount, OUApprovingAmount, OUCompletedAmount, OUReimbursedAmount, OUBudgetRemain, OUBudgetRate, AttachedFileName, RealAttachedFileName, Remark, int.Parse(ViewState["ReimburseRequirements"].ToString()), this.ViewState["FlowTemplate"].ToString(), FormApplyName);
            }
            this.Page.Response.Redirect("~/SalesForm/SalesApplySelect.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    #region 数据绑定及事件

    protected void gvApplyDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            ERS.ExpenseItemRow itemRow = null;
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormApplyDetailViewRow row = (FormDS.FormApplyDetailViewRow)drvDetail.Row;
                manualApplyFeeTotal = decimal.Round((manualApplyFeeTotal + row.Amount), 2);
                itemRow = new MasterDataBLL().GetExpenseItemByID(row.ExpenseItemID);
                if (itemRow.IsInContract) {
                    inContractAmount = decimal.Round((inContractAmount + row.Amount), 2);
                }
            }
        }

        this.ViewState["ManualApplyFeeTotal"] = manualApplyFeeTotal;
        this.ViewState["InContractAmount"] = inContractAmount;

        if (e.Row.RowType == DataControlRowType.Footer) {
            Label sumlbl = new Label();
            sumlbl.Text = "合计：";
            e.Row.Cells[1].Controls.Add(sumlbl);
            e.Row.Cells[1].CssClass = "RedTextAlignCenter";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].Width = new Unit("250px");

            Label totallbl = new Label();
            totallbl.Text = manualApplyFeeTotal.ToString("N");
            e.Row.Cells[2].Controls.Add(totallbl);
            e.Row.Cells[2].CssClass = "RedTextAlignCenter";
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].Width = new Unit("100px");
        }

    }

    protected void odsApplyDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        UserControls_SKUControl UCSKU = (UserControls_SKUControl)this.fvApplyDetails.FindControl("UCSKU");
        if (UCSKU.SKUID == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请选择产品");
            e.Cancel = true;
            return;
        } else {
            e.InputParameters["SKUID"] = UCSKU.SKUID;
        }
        DropDownList newExpenseItemDDL = (DropDownList)this.fvApplyDetails.FindControl("newExpenseItemDDL");
        if (ExpenseItemRemainTimes.ContainsKey(int.Parse(newExpenseItemDDL.SelectedValue))) {
            object[] array = ExpenseItemRemainTimes[int.Parse(newExpenseItemDDL.SelectedValue)];
            if (array != null) {
                if (!(bool)array[0]) {
                    PageUtility.ShowModelDlg(this.Page, string.Format("您当前选择的客户的该费用项超出限制次数（已使用{0}次）", array[1].ToString()));
                    e.Cancel = true;
                    return;
                }
            }
        }
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormApplyID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }
    }

    protected void odsApplyDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        SalesApplyBLL bll = (SalesApplyBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void NewSKUDDL_SelectedIndexChanged(object sender, EventArgs e) {
        DropDownList NewSKUDDL = (DropDownList)this.fvApplyDetails.FindControl("NewSKUDDL");
        TextBox newDescCtl = (TextBox)this.fvApplyDetails.FindControl("newDescCtl");
        if (NewSKUDDL.SelectedValue != "0") {
            newDescCtl.Text = new MasterDataBLL().GetSKUById(int.Parse(NewSKUDDL.SelectedValue)).Spec;
        } else {
            newDescCtl.Text = "";
        }
    }

    public string GetProductNameByID(object skuID) {
        if (skuID.ToString() != string.Empty) {
            int id = Convert.ToInt32(skuID);
            ERS.SKURow sku = new MasterDataBLL().GetSKUById(id);
            return sku.SKUNo + '-' + sku.SKUName + "-" + sku.Spec;
        } else {
            return null;
        }
    }

    public string GetExpenseItemNameByID(object expenseItemID) {
        if (expenseItemID.ToString() != string.Empty) {
            int id = Convert.ToInt32(expenseItemID);
            ERS.ExpenseItemRow row = new MasterDataBLL().GetExpenseItemByID(id);
            return row.AccountingCode + "--" + row.ExpenseItemName;
        } else {
            return null;
        }
    }

    #endregion

    public bool IsInputValid() {
        if (this.txtFormApplyName.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入方案名称!");
            return false;
        }

        if (UCPromotionBegin.SelectedDate == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入促销开始日期!");
            return false;
        }
        if (UCPromotionEnd.SelectedDate == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入促销结束日期!");
            return false;
        }

        DateTime promotionBegin = DateTime.Parse(UCPromotionBegin.SelectedDate);
        DateTime promotionEnd = DateTime.Parse(UCPromotionEnd.SelectedDate);
        if (promotionBegin > promotionEnd) {
            PageUtility.ShowModelDlg(this.Page, "促销结束日期必须大于开始日期!");
            return false;
        }
        int ReimburseRequirements = 0;
        foreach (ListItem Item in chkListReimburseRequirements.Items) {
            if (Item.Selected) {
                ReimburseRequirements += int.Parse(Item.Value);
            }
        }
        this.ViewState["ReimburseRequirements"] = ReimburseRequirements;

        foreach (GridViewRow row in gvSplitRate.Rows) {
            if (row.RowType == DataControlRowType.DataRow) {
                FormDS.FormApplySplitRateRow detailRow = this.InnerDS.FormApplySplitRate[row.RowIndex];
                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                int Rate = 0;
                if (int.TryParse(txtRate.Text, out Rate)) {
                    detailRow.Rate = Rate;
                } else {
                    detailRow.Rate = 0;
                    PageUtility.ShowModelDlg(this.Page, "分摊比例只能为整数!");
                    return false;
                }
                detailRow.Remark = txtRemark.Text;
            }
        }
        return true;
    }

    public bool IsSubmitValid() {
        if (this.FirstVolumeCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入第一个月销量!");
            return false;
        } else {
            try {
                int.Parse(this.FirstVolumeCtl.Text);
            } catch (Exception ex) {
                PageUtility.ShowModelDlg(this.Page, "第一个月销量必须为整数!");
                return false;
            }
        }
        if (this.SecondVolumeCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入第二个月销量!");
            return false;
        } else {
            try {
                int.Parse(this.SecondVolumeCtl.Text);
            } catch (Exception ex) {
                PageUtility.ShowModelDlg(this.Page, "第二个月销量必须为整数!");
                return false;
            }
        }
        if (this.ThirdVolumeCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入第三个月销量!");
            return false;
        } else {
            try {
                int.Parse(this.ThirdVolumeCtl.Text);
            } catch (Exception ex) {
                PageUtility.ShowModelDlg(this.Page, "第三个月销量必须为整数!");
                return false;
            }
        }

        if (this.txtEstimatedSaleVolume.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入预估销售量!");
            return false;
        } else {
            int result = 0;
            if (!Int32.TryParse(this.txtEstimatedSaleVolume.Text, out result)) {
                PageUtility.ShowModelDlg(this.Page, "预估销售量必须为整数!");
                return false;
            }
        }

        if (this.gvApplyDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入明细项");
            return false;
        }

        ////如果是实物报销的话，只能有一条科目明细
        //if (int.Parse(PaymentTypeDDL.SelectedValue) == (int)SystemEnums.PaymentType.ShiWu) {
        //    if (this.gvApplyDetails.Rows.Count > 1) {
        //        PageUtility.ShowModelDlg(this.Page, "支付方式为货补的话，有且只能有一条费用项，请检查");
        //        return false;
        //    }
        //}

        //促销期间和费用期间必须一致
        DateTime PromotionBegin = DateTime.Parse(UCPromotionBegin.SelectedDate);
        DateTime PromotionEnd = DateTime.Parse(UCPromotionEnd.SelectedDate);
        DateTime BeginPeriodCtl = DateTime.Parse(this.ViewState["BeginPeriod"].ToString());
        DateTime EndPeriodCt = DateTime.Parse(this.ViewState["EndPeriod"].ToString());
        if (PromotionBegin.Year != BeginPeriodCtl.Year || PromotionBegin.Month != BeginPeriodCtl.Month) {
            PageUtility.ShowModelDlg(this.Page, "起始费用期间和促销开始日期必须一致，请检查");
            return false;
        }
        if (PromotionEnd.Year != EndPeriodCt.Year || PromotionEnd.Month != EndPeriodCt.Month) {
            PageUtility.ShowModelDlg(this.Page, "截止费用期间和促销截止日期必须一致，请检查");
            return false;
        }
        if (int.Parse(this.ViewState["ReimburseRequirements"].ToString()) <= 0) {
            PageUtility.ShowModelDlg(this.Page, "核销要求至少选一项，请检查");
            return false;
        }
        if (this.divSplitRate.Visible) {
            int RateTotal = 0;
            foreach (GridViewRow row in gvSplitRate.Rows) {
                if (row.RowType == DataControlRowType.DataRow) {
                    FormDS.FormApplySplitRateRow detailRow = this.InnerDS.FormApplySplitRate[row.RowIndex];
                    RateTotal += detailRow.IsRateNull() ? 0 : detailRow.Rate;
                    if (detailRow.IsRateNull() || detailRow.Rate <= 0) {
                        if (row.RowIndex + 1 == InnerDS.FormApplySplitRate.Count && RateTotal < 100) {
                            detailRow.Rate = 100 - RateTotal;
                        } else {
                            PageUtility.ShowModelDlg(this.Page, "分摊比例只能为数字且不能为空或零，请检查");
                            return false;
                        }
                    }
                }
            }
            if (RateTotal != 100) {
                PageUtility.ShowModelDlg(this.Page, "分摊比例总和应为100，请检查");
                return false;
            }
        }
        return true;
    }

    //查询各项费用项可用次数
    public void InitCustomerTimesLimit() {
        ExpenseItemRemainTimes = new Dictionary<int, object[]>();
        int CustomerID = int.Parse(this.ViewState["CustomerID"].ToString());
        int SubCategoryID = int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString());
        DateTime Period = DateTime.Parse(this.ViewState["EndPeriod"].ToString());
        ERS.ExpenseItemDataTable ExpenseTable = new MasterDataBLL().GetExpenseItemBySubCateId(SubCategoryID);
        object[] tempArray;
        object[] tempArray1 = new object[2];
        foreach (ERS.ExpenseItemRow item in ExpenseTable.Rows) {
            if (item.IsInContract) {
                tempArray = this.SalesApplyBLL.GetCustomerTimesLimitByParameter(CustomerID, item.ExpenseItemID, Period.Year);
                if ((bool)tempArray[0]) {
                    tempArray1[0] = (bool)tempArray[0];
                    tempArray1[1] = int.Parse(tempArray[2].ToString());
                    ExpenseItemRemainTimes.Add(item.ExpenseItemID, tempArray1);
                } else {
                    tempArray1[0] = false;
                    tempArray1[1] = int.Parse(tempArray[2].ToString());
                    ExpenseItemRemainTimes.Add(item.ExpenseItemID, tempArray1);
                }
            }
        }
        this.ViewState["ExpenseItemRemainTimes"] = ExpenseItemRemainTimes;
    }
}
