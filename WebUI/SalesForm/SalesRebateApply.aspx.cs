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

public partial class SaleForm_SalesRebateApply : BasePage {

    decimal manualApplyFeeTotal = 0;

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

    #region ҳ���ʼ�����¼�����

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this.Page, "��������");
            this.Page.Title = "��������";


            // �û���Ϣ��ְλ��Ϣ
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
            //����ǲݸ���и�ֵ
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
                    this.Session["ErrorInfor"] = "δ�ҵ��ͻ�������ϵ����Ա";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["BeginPeriod"] != null && Request["EndPeriod"] != null) {
                    this.ViewState["BeginPeriod"] = Request["BeginPeriod"];
                    this.ViewState["EndPeriod"] = Request["EndPeriod"];
                } else {
                    this.Session["ErrorInfor"] = "û�з����ڼ䣬����ϵ����Ա";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["ExpenseSubCategoryID"] != null) {
                    this.ViewState["ExpenseSubCategoryID"] = Request["ExpenseSubCategoryID"];
                } else {
                    this.Session["ErrorInfor"] = "δ�ҵ�����С�࣬����ϵ����Ա";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }

                this.BeginPeriodCtl.Text = DateTime.Parse(this.ViewState["BeginPeriod"].ToString()).ToString("yyyy-MM");
                this.EndPeriodCtl.Text = DateTime.Parse(this.ViewState["EndPeriod"].ToString()).ToString("yyyy-MM");
                this.ExpenseSubCategoryCtl.Text = new MasterDataBLL().GetExpenseSubCateNameById(int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString()));
                ERS.CustomerRow customer = new MasterDataBLL().GetCustomerById(int.Parse(this.ViewState["CustomerID"].ToString()));
                this.CustomerNameCtl.Text = customer.CustomerName;
                //this.CustomerTypeCtl.Text = new MasterDataBLL().GetCustomerTypeById(customer.CustomerTypeID).CustomerTypeName;
                this.odsShop.SelectParameters["CustomerID"].DefaultValue = customer.CustomerID.ToString();
            }
            //�жϷ����ڼ��Ƿ���ȷ
            MasterDataBLL bll = new MasterDataBLL();
            if (!bll.IsValidApplyYear(DateTime.Parse(this.ViewState["BeginPeriod"].ToString()).AddMonths(-3).Year)) {
                this.SubmitBtn.Visible = false;
                PageUtility.ShowModelDlg(this, "���������뱾������Ŀ������ϵ����!");
                return;
            }

            //Ԥ����Ϣ
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

            if (Session["ProxyStuffUserId"] != null) {
                this.SubmitBtn.Visible = false;
            }

        }
        // ����ϸ��
        FormApplyDetailViewTableAdapter taDetail = new FormApplyDetailViewTableAdapter();
        int customerID = int.Parse(this.ViewState["CustomerID"].ToString());
        DateTime period = DateTime.Parse(this.ViewState["BeginPeriod"].ToString());
        taDetail.FillDataFromRebate(this.InnerDS.FormApplyDetailView, customerID, period.Year, period.Month);
        //����Ԥ����Ϣ
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
        //��ֵ
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
        if (!rowFormApply.IsAttachedFileNameNull()) {
            this.UCFileUpload.AttachmentFileName = rowFormApply.AttachedFileName;
        }
        if (!rowFormApply.IsRealAttachedFileNameNull()) {
            this.UCFileUpload.RealAttachmentFileName = rowFormApply.RealAttachedFileName;
        }
        if (!rowFormApply.IsFormApplyNameNull()) {
            this.txtFormApplyName.Text = rowFormApply.FormApplyName.ToString();
        }
    }

    #endregion

    protected void CancelBtn_Click(object sender, EventArgs e) {
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //ɾ���ݸ�
        int formID = (int)this.ViewState["ObjectId"];
        this.SalesApplyBLL.DeleteFormApply(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        if (this.txtFormApplyName.Text == string.Empty) {
            PageUtility.ShowModelDlg(this, "��¼�뷽������");
            return;
        }
        SaveFormApply(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        if (this.txtFormApplyName.Text == string.Empty) {
            PageUtility.ShowModelDlg(this, "��¼�뷽������");
            return;
        }
        if (this.gvApplyDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "����¼����ϸ��");
            return;
        }

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

        if (StatusID == SystemEnums.FormStatus.Awaiting) {//�ύʱ��飬����ݸ岻���
            decimal[] calculateAssistant = new decimal[14];
            calculateAssistant = new BudgetBLL().GetSalesBudgetByParameter(int.Parse(this.ViewState["CustomerID"].ToString()), DateTime.Parse(this.ViewState["BeginPeriod"].ToString()), int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString()));
            if (calculateAssistant[12] < decimal.Parse(this.ViewState["ManualApplyFeeTotal"].ToString())) {
                PageUtility.ShowModelDlg(this.Page, "���������������Ԥ�㣬�����ύ");
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
        string AttachedFileName = this.UCFileUpload.AttachmentFileName;
        string RealAttachedFileName = this.UCFileUpload.RealAttachmentFileName;
        string Remark = this.RemarkCtl.Text;
        string FormApplyName = this.txtFormApplyName.Text;

        if (this.SalesApplyBLL.GetRebateApplyCountByParameter(CustomerID, BeginPeriod.Year, BeginPeriod.Month, ExpenseSubCategoryID) > 0) {
            PageUtility.ShowModelDlg(this, "ϵͳ���Ѿ����ڸÿͻ��ķ������룬��������ÿ��ֻ������һ�Σ�");
            return;
        }
        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.SalesApplyBLL.AddFormApplyRebate(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.SalesApply, StatusID, BeginPeriod,
                    EndPeriod, CustomerID, ShopID, PaymentTypeID, ExpenseSubCategoryID, ContractNo, CustomerBudget, CustomerBudgetRemain, OUBudget, OUAppovedAmount, OUApprovingAmount, OUCompletedAmount,
                    OUReimbursedAmount, OUBudgetRemain, OUBudgetRate, AttachedFileName, RealAttachedFileName, Remark, this.ViewState["FlowTemplate"].ToString(), FormApplyName);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.SalesApplyBLL.UpdateFormApplyRebate(FormID, StatusID, SystemEnums.FormType.SalesApply, ShopID, PaymentTypeID, ContractNo, CustomerBudget, CustomerBudgetRemain,
                    OUBudget, OUAppovedAmount, OUApprovingAmount,OUCompletedAmount, OUReimbursedAmount, OUBudgetRemain, OUBudgetRate, AttachedFileName, RealAttachedFileName, Remark, this.ViewState["FlowTemplate"].ToString(), FormApplyName);
            }
            this.Page.Response.Redirect("~/SalesForm/SalesApplySelect.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    #region ���ݰ󶨼��¼�

    protected void gvApplyDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // �������н��и�ֵ
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormApplyDetailViewRow row = (FormDS.FormApplyDetailViewRow)drvDetail.Row;
                manualApplyFeeTotal = decimal.Round((manualApplyFeeTotal + row.Amount), 2);
            }
        }

        this.ViewState["ManualApplyFeeTotal"] = manualApplyFeeTotal;

        if (e.Row.RowType == DataControlRowType.Footer) {
            Label sumlbl = new Label();
            sumlbl.Text = "�ϼ�";
            e.Row.Cells[2].Controls.Add(sumlbl);
            e.Row.Cells[2].CssClass = "RedTextAlignCenter";
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].Width = new Unit("145px");

            Label totallbl = new Label();
            totallbl.Text = manualApplyFeeTotal.ToString("N");
            e.Row.Cells[3].Controls.Add(totallbl);
            e.Row.Cells[3].CssClass = "RedTextAlignCenter";
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[3].Width = new Unit("120px");
        }
    }

    protected void odsApplyDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        SalesApplyBLL bll = (SalesApplyBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    public string GetProductNameByID(object skuID) {
        if (skuID.ToString() != string.Empty) {
            int id = Convert.ToInt32(skuID);
            //return new MasterDataBLL().GetSKUById(id).SKUName + "-" + new MasterDataBLL().GetSKUById(id).Spec;
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
}
