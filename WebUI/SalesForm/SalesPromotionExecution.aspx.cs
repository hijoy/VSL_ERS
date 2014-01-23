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
using System.Text;
using BusinessObjects;
using BusinessObjects.FormDSTableAdapters;

public partial class SaleForm_SalesPromotionExecution : BasePage {

    decimal AccruedFeeTotal = 0;

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

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this.Page, "方案申请审批");
            this.Page.Title = "方案申请审批";

            int formID = int.Parse(Request["ObjectId"]);
            this.ViewState["ObjectId"] = formID;
            FormDS.FormRow rowForm = this.SalesApplyBLL.GetFormByID(formID)[0];
            FormDS.FormApplyRow rowFormApply = this.SalesApplyBLL.GetFormApplyByID(formID)[0];
            if (rowForm.IsProcIDNull()) {
                ViewState["ProcID"] = "";
            } else {
                ViewState["ProcID"] = rowForm.ProcID;
            }
            ViewState["OrganizationUnitID"] = rowForm.OrganizationUnitID;
            this.FormNoCtl.Text = rowForm.FormNo;
            this.ApplyDateCtl.Text = rowForm.SubmitDate.ToShortDateString();
            AuthorizationDS.StuffUserRow applicant = new AuthorizationBLL().GetStuffUserById(rowForm.UserID);
            this.StuffNameCtl.Text = applicant.StuffName;
            this.PositionNameCtl.Text = new OUTreeBLL().GetPositionById(rowForm.PositionID).PositionName;
            if (new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID) != null) {
                this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID).OrganizationUnitName;
            }
            this.AttendDateCtl.Text = applicant.AttendDate.ToShortDateString();
            this.BeginPeriodCtl.Text = rowFormApply.BeginPeriod.ToString("yyyy-MM");
            this.EndPeriodCtl.Text = rowFormApply.EndPeriod.ToString("yyyy-MM");
            MasterDataBLL masterBll = new MasterDataBLL();
            this.ExpenseSubCategoryCtl.Text = masterBll.GetExpenseSubCateNameById(rowFormApply.ExpenseSubCategoryID);
            ERS.CustomerRow customer = masterBll.GetCustomerById(rowFormApply.CustomerID);
            this.CustomerNameCtl.Text = customer.CustomerName;
            //this.CustomerTypeCtl.Text = masterBll.GetCustomerTypeById(customer.CustomerTypeID).CustomerTypeName;
            this.ShopNameCtl.Text = masterBll.GetShopByID(rowFormApply.ShopID).ShopName;
            this.PaymentTypeCtl.Text = masterBll.GetPaymentTypeById(rowFormApply.PaymentTypeID).PaymentTypeName;
            if (!rowFormApply.IsContractNoNull()) {
                this.ContractNoCtl.Text = rowFormApply.ContractNo;
            }
            this.AmountCtl.Text = rowFormApply.Amount.ToString("N");
            if (!rowFormApply.IsRemarkNull()) {
                this.RemarkCtl.Text = rowFormApply.Remark;
            }
            if (!rowFormApply.IsAttachedFileNameNull())
                this.UCFileUpload.AttachmentFileName = rowFormApply.AttachedFileName;
            if (!rowFormApply.IsRealAttachedFileNameNull())
                this.UCFileUpload.RealAttachmentFileName = rowFormApply.RealAttachedFileName;

            if (!rowFormApply.IsPromotionBeginDateNull()) {
                this.PromotionBeginCtl.Text = rowFormApply.PromotionBeginDate.ToString("yyyy-MM-dd");
            }
            if (!rowFormApply.IsPromotionEndDateNull()) {
                this.PromotionEndCtl.Text = rowFormApply.PromotionEndDate.ToString("yyyy-MM-dd");
            }
            if (!rowFormApply.IsDeliveryBeginDateNull()) {
                this.DeliveryBeginCtl.Text = rowFormApply.DeliveryBeginDate.ToString("yyyy-MM-dd");
            }
            if (!rowFormApply.IsDeliveryEndDateNull()) {
                this.DeliveryEndCtl.Text = rowFormApply.DeliveryEndDate.ToString("yyyy-MM-dd");
            }
            this.PromotionScopeCtl.Text = masterBll.GetPromotionScopeById(rowFormApply.PromotionScopeID).PromotionScopeName;
            this.PromotionTypeCtl.Text = masterBll.GetPromotionTypeById(rowFormApply.PromotionTypeID).PromotionTypeName;
            if (!rowFormApply.IsPromotionDescNull()) {
                this.PromotionDescCtl.Text = rowFormApply.PromotionDesc;
            }
            this.ShelfTypeCtl.Text = masterBll.GetShelfTypeById(rowFormApply.ShelfTypeID).ShelfTypeName;
            if (!rowFormApply.IsFirstVolumeNull()) {
                this.FirstVolumeCtl.Text = rowFormApply.FirstVolume.ToString();
            }
            if (!rowFormApply.IsSecondVolumeNull()) {
                this.SecondVolumeCtl.Text = rowFormApply.SecondVolume.ToString();
            }
            if (!rowFormApply.IsThirdVolumeNull()) {
                this.ThirdVolumeCtl.Text = rowFormApply.ThirdVolume.ToString();
            }
            if (!rowFormApply.IsAverageVolumeNull()) {
                this.AverageVolumeCtl.Text = rowFormApply.AverageVolume.ToString();
            }
            if (!rowFormApply.IsFormApplyNameNull()) {
                this.txtFormApplyName.Text = rowFormApply.FormApplyName;
            }

            //查看预算信息按钮
            this.btnViewBudget.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/ReportManage/SalesFeeByPosition.aspx?ShowDialog=1','', 'dialogWidth:918px;dialogHeight:660px;resizable:yes;')";

            //预算信息
            this.CustomerBudgetCtl.Text = rowFormApply.CustomerBudget.ToString("N");
            this.CustomerBudgetRemainCtl.Text = rowFormApply.CustomerBudgetRemain.ToString("N");
            this.OUBudgetCtl.Text = rowFormApply.OUBudget.ToString("N");
            this.OUApprovedAmountCtl.Text = rowFormApply.OUAppovedAmount.ToString("N");
            this.OUApprovingAmountCtl.Text = rowFormApply.OUApprovingAmount.ToString("N");
            this.OUCompletedAmountCtl.Text = rowFormApply.OUCompletedAmount.ToString("N");
            this.OUReimbursedAmountCtl.Text = rowFormApply.OUReimbursedAmount.ToString("N");
            this.OUBudgetRemainCtl.Text = rowFormApply.OUBudgetRemain.ToString("N");
            this.OUBudgetRateCtl.Text = ((decimal)(rowFormApply.OUBudgetRate * 100)).ToString("N") + "%";


            //审批页面处理&按钮处理
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;

            //是否显示关闭按钮,如果没有执行完成的话那么不能关闭
            this.CloseBtn.Visible = false;
            if ((!rowFormApply.IsClose) && rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted) {
                if (stuffUser.StuffUserId == rowForm.UserID || new MasterDataBLL().GetProxyReimburseByParameter(rowForm.UserID, stuffUser.StuffUserId, rowForm.SubmitDate).Count > 0) {
                    if (!rowFormApply.IsIsCompleteNull()) {
                        if (rowFormApply.IsComplete) {
                            this.CloseBtn.Visible = true;
                        }
                    }
                }
            }

            //是否显示复制按钮
            if (rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted && stuffUser.StuffUserId == rowForm.UserID) {
                this.UCBeginPeriod.Visible = true;
                this.UCEndPeriod.Visible = true;
                this.lblSignal.Visible = true;
                this.CopyBtn.Visible = true;
            } else {
                this.UCBeginPeriod.Visible = false;
                this.UCEndPeriod.Visible = false;
                this.lblSignal.Visible = false;
                this.CopyBtn.Visible = false;
            }
            //是否显示打印信息
            if (((stuffUser.StuffUserId == rowForm.UserID || base.IsBusinessProxy(rowForm.UserID, rowForm.SubmitDate) && rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted) || (rowForm.StatusID == (int)SystemEnums.FormStatus.Scrap && rowFormApply.IsAutoSplit))) {
                this.PrintBtn.Visible = true;
            } else {
                this.PrintBtn.Visible = false;
            }

            if (rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted || (rowForm.StatusID == (int)SystemEnums.FormStatus.Scrap && rowFormApply.IsAutoSplit)) {
                this.PrintInfor.Visible = true;
                int printCount = rowFormApply.IsPrintCountNull() ? 0 : rowFormApply.PrintCount;
                this.PrintInfor.Text = "该方案已经被打印" + printCount + "次";
            } else {
                this.PrintInfor.Visible = false;
            }


            //如果是弹出,取消按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.upButton.Visible = false;
                    this.PrintInfor.Visible = false;
                    this.PrintBtn.Visible = false;
                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                }
            }
            //如果是申请人则隐藏预算信息
            if (stuffUser.StuffUserId == rowForm.UserID) {
                this.OUBudgetCtl.Text = "";
                this.OUApprovedAmountCtl.Text = "";
                this.OUApprovingAmountCtl.Text = "";
                this.OUCompletedAmountCtl.Text = "";
                this.OUReimbursedAmountCtl.Text = "";
                this.OUBudgetRemainCtl.Text = "";
                this.OUBudgetRateCtl.Text = "";
                this.btnViewBudget.Visible = false;
            }
            //处理执行确认内容
            if (!rowFormApply.IsConfirmCompleteDateNull()) {
                ConfirmCompleteDateCtl.Text = rowFormApply.ConfirmCompleteDate.ToString("yyyy-MM-dd");
            }
            if (!rowFormApply.IsAccruedPeriodNull()) {
                AccruedPeriodCtl.Text = rowFormApply.AccruedPeriod.ToString("yyyy-MM");
            }
            if (!rowFormApply.IsAccruedAmountNull()) {
                AccruedAmountCtl.Text = rowFormApply.AccruedAmount.ToString("N");
            }
            //如果是申请人并且没有执行确认，实际费用才可以编辑
            this.ViewState["ExecuteReadOnly"] = true;
            if (!rowFormApply.IsIsCompleteNull()) {
                if (!rowFormApply.IsComplete && rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted) {
                    if (rowForm.UserID == stuffUser.StuffUserId || new MasterDataBLL().GetProxyReimburseByParameter(rowForm.UserID, stuffUser.StuffUserId, rowForm.SubmitDate).Count > 0) {
                        this.ViewState["ExecuteReadOnly"] = false;
                        this.AccrudePeriodSignal.Visible = true;
                        this.PeriodDDL.Visible = true;
                        this.ExecuteConfirmBtn.Visible = true;
                        this.ExecuteCancelBtn.Visible = true;
                    }
                }
            }

            // 打开明细表
            FormApplySKUDetailTableAdapter taSKU = new FormApplySKUDetailTableAdapter();
            taSKU.FillByFormApplyID(this.InnerDS.FormApplySKUDetail, formID);
            FormApplyExpenseDetailTableAdapter taExpense = new FormApplyExpenseDetailTableAdapter();
            taExpense.FillByFormApplyID(this.InnerDS.FormApplyExpenseDetail, formID);

            //分摊比例
            new FormApplySplitRateTableAdapter().FillByApplyID(InnerDS.FormApplySplitRate, formID);
            if (InnerDS.FormApplySplitRate != null && InnerDS.FormApplySplitRate.Count > 0) {
                this.divSplitRate.Visible = true;
                this.gvSplitRate.Visible = true;
                this.gvSplitRate.DataSource = InnerDS.FormApplySplitRate;
                this.gvSplitRate.DataBind();
            }
        }


        this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
        this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
    }


    protected override void OnLoadComplete(EventArgs e) {
        base.OnLoadComplete(e);
        decimal totalFee = 0;
        Label lblSum = (Label)SKUListView.FindControl("lblSum");
        for (int i = 0; i < this.SKUListView.Items.Count; i++) {
            foreach (GridViewRow row in ((GridView)this.SKUListView.Items[i].FindControl("gvExpense")).Rows) {
                if (row.RowType == DataControlRowType.DataRow) {
                    TextBox txtAccruedAmount = (TextBox)row.FindControl("txtAccruedAmount");
                    if (string.IsNullOrEmpty(txtAccruedAmount.Text.Trim())) {
                        txtAccruedAmount.Text = "0";
                    }
                    decimal accruedAmount = decimal.Parse(txtAccruedAmount.Text.Trim());
                    totalFee = totalFee + accruedAmount;
                }
            }
        }
        lblSum.Text = totalFee.ToString("N");
        //if (this.gvApplyDetails.Rows.Count > 0) {
        //    foreach (GridViewRow item in gvApplyDetails.Rows) {
        //        if (item.RowType == DataControlRowType.DataRow) {
        //            TextBox textBox = (TextBox)item.FindControl("txtAccruedAmount");
        //            total += decimal.Parse(textBox.Text == string.Empty ? "0" : textBox.Text.ToString());
        //        }
        //    }
        //}
        //Label lblTotal = (Label)gvApplyDetails.FooterRow.FindControl("totallbl");
        //lblTotal.Text = total.ToString("N");
    }

    protected void ExecuteCancelBtn_Click(object sender, EventArgs e) {
        this.SalesApplyBLL.ExecuteCancel((int)this.ViewState["ObjectId"], ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId);
        if (this.Request["Source"] != null) {
            this.Response.Redirect(this.Request["Source"].ToString());
        } else {
            this.Response.Redirect("~/Home.aspx");
        }
    }

    protected void ExecuteConfirmBtn_Click(object sender, EventArgs e) {

        try {
            this.SalesApplyBLL.FormDataSet = this.InnerDS;
            if (this.PeriodDDL.SelectedValue == "0") {
                PageUtility.ShowModelDlg(this.Page, "请选择预提费用期间");
                return;
            }
            if (FillDetail()) {
                if (decimal.Parse(this.ViewState["AccruedFeeTotal"].ToString()) <= 0) {
                    PageUtility.ShowModelDlg(this.Page, "实际发生费用不能为零");
                    return;
                }
                DateTime Period = new MasterDataBLL().GetAccruedPeriodByID(int.Parse(this.PeriodDDL.SelectedValue)).AccruedPeriod;
                AuthorizationDS.StuffUserRow currentStuff = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
                this.SalesApplyBLL.ExecuteConfirmForPromotion(this.cwfAppCheck.FormID, Period, currentStuff.StuffUserId);
                if (this.Request["Source"] != null) {
                    this.Response.Redirect(this.Request["Source"].ToString());
                } else {
                    this.Response.Redirect("~/Home.aspx");
                }
            }
        } catch (Exception exception) {
            this.cwfAppCheck.ReloadCtrl();
            PageUtility.DealWithException(this, exception);
        }

    }

    private bool FillDetail() {
        bool isValid = true;
        this.ViewState["AccruedFeeTotal"] = 0;
        for (int i = 0; i < this.SKUListView.Items.Count; i++) {
            foreach (GridViewRow row in ((GridView)this.SKUListView.Items[i].FindControl("gvExpense")).Rows) {
                if (row.RowType == DataControlRowType.DataRow) {
                    Label lblFormApplyExpenseDetailID = (Label)row.FindControl("lblFormApplyExpenseDetailID");
                    FormDS.FormApplyExpenseDetailRow detailRow = this.InnerDS.FormApplyExpenseDetail.FindByFormApplyExpenseDetailID(int.Parse(lblFormApplyExpenseDetailID.Text));
                    TextBox txtAccruedAmount = (TextBox)row.FindControl("txtAccruedAmount");
                    if (string.IsNullOrEmpty(txtAccruedAmount.Text.Trim())) {
                        txtAccruedAmount.Text = "0";
                    }
                    decimal accruedAmount = decimal.Parse(txtAccruedAmount.Text.Trim());
                    if (accruedAmount < 0) {
                        PageUtility.ShowModelDlg(this.Page, "不能录入负数");
                        isValid = false;
                        break;
                    }
                    if (accruedAmount > detailRow.Amount) {
                        PageUtility.ShowModelDlg(this.Page, "实际费用不能大于申请费用");
                        isValid = false;
                        break;
                    }
                    detailRow.AccruedAmount = accruedAmount;
                    this.ViewState["AccruedFeeTotal"] = decimal.Parse(this.ViewState["AccruedFeeTotal"].ToString()) + accruedAmount;
                }
            }
        }
        return isValid;
    }


    protected void CancelBtn_Click(object sender, EventArgs e) {
        if (this.Request["Source"] != null) {
            this.Response.Redirect(this.Request["Source"].ToString());
        } else {
            this.Response.Redirect("~/Home.aspx");
        }
    }

    protected void EditBtn_Click(object sender, EventArgs e) {
        this.Response.Redirect("~/SalesForm/SalesPromotionApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void PrintBtn_Click(object sender, EventArgs e) {
        try {
            this.SalesApplyBLL.PrintCountForFormApply(int.Parse(this.ViewState["ObjectId"].ToString()));
            FormDS.FormApplyRow applyRow = new BusinessObjects.FormDSTableAdapters.FormApplyTableAdapter().GetDataByID(int.Parse(this.ViewState["ObjectId"].ToString()))[0];
            this.PrintInfor.Text = "该方案已经被打印" + applyRow.PrintCount + "次";
        } catch (Exception exception) {
            PageUtility.DealWithException(this, exception);
        }
    }

    protected void CopyBtn_Click(object sender, EventArgs e) {
        string stringBeginPeriod = ((TextBox)(this.UCBeginPeriod.FindControl("txtDate"))).Text.Trim();
        if (stringBeginPeriod == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择起始费用期间!");
            return;
        }
        DateTime beginPeriod = DateTime.Parse(stringBeginPeriod.Substring(0, 4) + "-" + stringBeginPeriod.Substring(4, 2) + "-01");
        string stringEndPeriod = ((TextBox)(this.UCEndPeriod.FindControl("txtDate"))).Text.Trim();
        if (stringEndPeriod == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择截止费用期间!");
            return;
        }
        DateTime endPeriod = DateTime.Parse(stringEndPeriod.Substring(0, 4) + "-" + stringEndPeriod.Substring(4, 2) + "-01");
        if (endPeriod < beginPeriod) {
            PageUtility.ShowModelDlg(this, "截止费用期间必须大于等于起始费用期间!");
            return;
        }
        try {
            this.SalesApplyBLL.CopyApplyForm(int.Parse(this.ViewState["ObjectId"].ToString()), beginPeriod, endPeriod);
            this.Response.Redirect("~/Home.aspx");
        } catch (Exception exception) {
            PageUtility.DealWithException(this, exception);
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

    protected void CloseBtn_Click(object sender, EventArgs e) {
        int FormApplyId = int.Parse(this.ViewState["ObjectId"].ToString());
        int count = this.SalesApplyBLL.GetProcessingFormReimburseByApplyID(FormApplyId);
        if (count > 0) {
            PageUtility.ShowModelDlg(this.Page, string.Format("不能关闭，还有{0}张相关报销单正在审批中!", count));
        } else {
            this.SalesApplyBLL.CloseFormApply(FormApplyId);
            this.Response.Redirect("~/Home.aspx");
        }
    }

    protected void odsSKU_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        SalesApplyBLL bll = (SalesApplyBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void odsExpense_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        SalesApplyBLL bll = (SalesApplyBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormApplyExpenseDetailRow row = (FormDS.FormApplyExpenseDetailRow)drvDetail.Row;
                if (!row.IsAccruedAmountNull()) {
                    AccruedFeeTotal = decimal.Round((AccruedFeeTotal + row.AccruedAmount), 2);
                }

                TextBox txtAccruedAmount = (TextBox)e.Row.FindControl("txtAccruedAmount");
                txtAccruedAmount.Attributes.Add("onBlur", "PlusTotal(this);");
                txtAccruedAmount.Attributes.Add("onFocus", "MinusTotal(this)");
                txtAccruedAmount.ReadOnly = (bool)this.ViewState["ExecuteReadOnly"];

                CommonUtility.GenerateRepeatControl(e.Row.Cells[3].Controls, row.IsRepeatFormInfoNull() ? "" : row.RepeatFormInfo);
            }
        }
    }


}
