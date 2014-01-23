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

public partial class OtherForm_BudgetAllocationApply : BasePage {

    decimal OutTotalFee = 0;
    decimal InTotalFee = 0;
    string inIds = "";
    string outIds = "";
    string tempInId = "";
    string tempOutId = "";

    private BudgetAllocationApplyBLL m_BudgetAllocationApplyBLL;
    protected BudgetAllocationApplyBLL BudgetAllocationApplyBLL {
        get {
            if (this.m_BudgetAllocationApplyBLL == null) {
                this.m_BudgetAllocationApplyBLL = new BudgetAllocationApplyBLL();
            }
            return this.m_BudgetAllocationApplyBLL;
        }
    }

    private FormDS m_InnerDS;
    public FormDS InnerDS {
        get {
            if (this.ViewState["InnerDS"] == null) {
                this.ViewState["InnerDS"] = new FormDS();
            }
            return (FormDS)this.ViewState["InnerDS"];
        }
    }


    #region 页面初始化及事件处理


    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this.Page, "预算调拨申请");
            this.Page.Title = "预算调拨申请";
            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow rowUserPosition = (AuthorizationDS.PositionRow)Session["Position"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = rowUserPosition.PositionId;
            this.ViewState["FlowTemplate"] = new AuthorizationBLL().GetFlowTemplate((int)SystemEnums.BusinessUseCase.FormBugetAllocation, stuffUser.StuffUserId);
           
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
            }

            if (Session["ProxyStuffUserId"] != null) {
                this.SubmitBtn.Visible = false;
            }

        }
    }

    protected void OpenForm(int formID) {
        FormTableAdapter taForm = new FormTableAdapter();
        taForm.FillByID(this.InnerDS.Form, formID);
        FormDS.FormRow rowForm = this.InnerDS.Form[0];
        FormBudgetAllocationTableAdapter taFormBudgetAllocation = new FormBudgetAllocationTableAdapter();
        taFormBudgetAllocation.FillByID(this.InnerDS.FormBudgetAllocation, formID);
        FormDS.FormBudgetAllocationRow rowFormBudgetAllocation = this.InnerDS.FormBudgetAllocation[0];
        //赋值
        if (!rowFormBudgetAllocation.IsRemarkNull()) {
            this.RemarkCtl.Text = rowFormBudgetAllocation.Remark;
        }
        if (!rowFormBudgetAllocation.IsAttachFileNameNull()) {
            this.UCFileUpload.AttachmentFileName = rowFormBudgetAllocation.AttachFileName;
        }
        if (!rowFormBudgetAllocation.IsRealAttachFileNameNull()) {
            this.UCFileUpload.RealAttachmentFileName = rowFormBudgetAllocation.RealAttachFileName;
        }

        // 打开明细表
        FormBudgetAllocationDetailTableAdapter taDetail = new FormBudgetAllocationDetailTableAdapter();
        taDetail.FillByFormID(this.InnerDS.FormBudgetAllocationDetail, formID);
    }

    #endregion

    protected void CancelBtn_Click(object sender, EventArgs e) {
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.BudgetAllocationApplyBLL.DeleteFormBudgetAllocation(formID);
        this.Page.Response.Redirect("~/Home.aspx");
        SqlDataSource sds = new SqlDataSource();
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        if (!IsInputValid())
            return;

        SaveFormBudgetAllocation(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        if (!IsInputValid())
            return;
        if (!IsSubmitValid())
            return;

        SaveFormBudgetAllocation(SystemEnums.FormStatus.Awaiting);
    }

    protected void SaveFormBudgetAllocation(SystemEnums.FormStatus StatusID) {
        this.BudgetAllocationApplyBLL.FormDataSet = this.InnerDS;
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

        string AttachFileName = this.UCFileUpload.AttachmentFileName;
        string RealAttachFileName = this.UCFileUpload.RealAttachmentFileName;
        string Remark = this.RemarkCtl.Text;
        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.BudgetAllocationApplyBLL.AddFormBudgetAllocation(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID,
                  SystemEnums.FormType.BudgetAllocationApply, StatusID, Remark, AttachFileName, RealAttachFileName, this.ViewState["FlowTemplate"].ToString());
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.BudgetAllocationApplyBLL.UpdateFormBudgetAllocation(FormID, StatusID, SystemEnums.FormType.BudgetAllocationApply, Remark, AttachFileName, RealAttachFileName,this.ViewState["FlowTemplate"].ToString());
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    public bool IsInputValid() {
        return true;
    }

    public bool IsSubmitValid() {
        if (this.gvBudgetAllocationOutDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入调出明细项");
            return false;
        }
        if (this.gvBudgetAllocationInDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入调入明细项");
            return false;
        }
        if (decimal.Parse(this.ViewState["InTotalFee"].ToString()) != decimal.Parse(this.ViewState["OutTotalFee"].ToString())) {
            PageUtility.ShowModelDlg(this.Page, "调出金额必须等于调入金额");
            return false;
        }
        return true;
    }

    #region 数据绑定及事件

    protected void odsBudgetAllocationOutDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        int index = -1;
        UserControls_BudgetSalesFeeViewControl uc = (UserControls_BudgetSalesFeeViewControl)this.fvBudgetAllocationOutDetails.FindControl("UCBudgetSales");
        if (uc.BudgetSalesFeeId == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择客户、费用期间、费用项！");
            e.Cancel = true;
            return;
        }
        foreach (FormDS.FormBudgetAllocationDetailRow row in this.InnerDS.FormBudgetAllocationDetail.Rows) {
            if ((row.AllocationType == (int)SystemEnums.AllocationType.Out) && row.BudgetSalesFeeId.ToString().Equals(uc.BudgetSalesFeeId)) {
                index = 1;
            }
        }

        if (index == 1) {
            PageUtility.ShowModelDlg(this, "添加失败，客户、费用期间、费用项不能重复！");
            e.Cancel = true;
            return;
        }
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormBudgetAllocationID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }

        e.InputParameters["BudgetSaleFeeViewId"] = uc.BudgetSalesFeeId;
        TextBox txtAmount = (TextBox)this.fvBudgetAllocationOutDetails.FindControl("txtTransferBudget");
        e.InputParameters["TransferBudget"] = txtAmount.Text;
        e.InputParameters["User"] = Session["StuffUser"];
        e.InputParameters["AllocationType"] = SystemEnums.AllocationType.Out;
        tempOutId = uc.BudgetSalesFeeId;
    }

    protected void odsBudgetAllocationOutDetails_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsBudgetAllocationOutDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        BudgetAllocationApplyBLL bll = (BudgetAllocationApplyBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void odsBudgetAllocationInDetails_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        } else {
        }
    }

    protected void odsBudgetAllocationInDetails_Deleted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        } else {
            inIds += tempInId + ",";
            this.ViewState["inIds"] = inIds;
        }
    }

    protected void odsBudgetAllocationInDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        int index = -1;
        UserControls_BudgetSalesFeeViewControl uc = (UserControls_BudgetSalesFeeViewControl)this.fvBudgetAllocationInDetails.FindControl("UCBudgetSales");
        if (uc.BudgetSalesFeeId == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择客户、费用期间、费用项！");
            e.Cancel = true;
            return;
        }
        TextBox txtAmount = (TextBox)this.fvBudgetAllocationInDetails.FindControl("txtTransferBudget");
        foreach (FormDS.FormBudgetAllocationDetailRow row in this.InnerDS.FormBudgetAllocationDetail.Rows) {
            if ((row.AllocationType == (int)SystemEnums.AllocationType.In) && row.BudgetSalesFeeId.ToString().Equals(uc.BudgetSalesFeeId)) {
                index = 1;
            }
        }
        if (index == 1) {
            PageUtility.ShowModelDlg(this, "添加失败，客户、费用期间、费用项不能重复！");
            e.Cancel = true;
            return;
        }
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormBudgetAllocationID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }
        e.InputParameters["BudgetSaleFeeViewId"] = uc.BudgetSalesFeeId;
        e.InputParameters["TransferBudget"] = txtAmount.Text;
        e.InputParameters["User"] = Session["StuffUser"];
        e.InputParameters["AllocationType"] = SystemEnums.AllocationType.In;
        tempInId = uc.BudgetSalesFeeId;
    }

    protected void odsBudgetAllocationInDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        BudgetAllocationApplyBLL bll = (BudgetAllocationApplyBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void gvBudgetAllocationInDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormBudgetAllocationDetailRow row = (FormDS.FormBudgetAllocationDetailRow)drvDetail.Row;
                this.inIds += row.BudgetSalesFeeId + ",";
                InTotalFee = decimal.Round((InTotalFee + row.TransferBudget), 2);
            }
        }
        this.ViewState["InTotalFee"] = InTotalFee;

        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label applbl = (Label)e.Row.FindControl("lblTotal");
                applbl.Text = InTotalFee.ToString("N");
            }
        }
    }

    protected void gvBudgetAllocationOutDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormBudgetAllocationDetailRow row = (FormDS.FormBudgetAllocationDetailRow)drvDetail.Row;
                this.outIds += row.BudgetSalesFeeId + ",";
                OutTotalFee = decimal.Round((OutTotalFee + row.TransferBudget), 2);
            }
        }
        this.ViewState["OutTotalFee"] = OutTotalFee;

        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label applbl = (Label)e.Row.FindControl("lblTotal");
                applbl.Text = OutTotalFee.ToString("N");
            }
        }
    }


    protected void odsBudgetAllocationOutDetails_Deleted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        } else {

        }
    }

    protected void OnDataTextChanged(object sender, EventArgs e) {
        UserControls_BudgetSalesFeeViewControl uc = (UserControls_BudgetSalesFeeViewControl)this.fvBudgetAllocationOutDetails.FindControl("UCBudgetSales");
        FormDS.BudgetSalesFeeViewRow row = this.BudgetAllocationApplyBLL.GetBudgetSalesFeeViewRowById(int.Parse(uc.BudgetSalesFeeId.ToString()));
        TextBox temp = (TextBox)this.fvBudgetAllocationOutDetails.FindControl("txtExpenseTypeName");
        //temp.Text = row.ExpenseItemName;
        temp.Text = GetExpenseItemNameByID(row.ExpenseItemID);
        temp = (TextBox)this.fvBudgetAllocationOutDetails.FindControl("txtPeriod");
        temp.Text = row.Period.ToString("yyyy/MM");
        temp = (TextBox)this.fvBudgetAllocationOutDetails.FindControl("txtOriginalBudget");
        temp.Text = row.OriginalBudget.ToString();
        temp = (TextBox)this.fvBudgetAllocationOutDetails.FindControl("txtNormalBudget");
        temp.Text = row.NormalBudget.ToString();
        temp = (TextBox)this.fvBudgetAllocationOutDetails.FindControl("txtAdjustBudget");
        temp.Text = row.AdjustBudget.ToString();
        temp = (TextBox)this.fvBudgetAllocationOutDetails.FindControl("txtTotalBudget");
        temp.Text = row.TotalBudget.ToString();
    }

    protected void OnDataTextChanged1(object sender, EventArgs e) {
        UserControls_BudgetSalesFeeViewControl uc = (UserControls_BudgetSalesFeeViewControl)this.fvBudgetAllocationInDetails.FindControl("UCBudgetSales");
        FormDS.BudgetSalesFeeViewRow row = this.BudgetAllocationApplyBLL.GetBudgetSalesFeeViewRowById(int.Parse(uc.BudgetSalesFeeId.ToString()));
        TextBox temp = (TextBox)this.fvBudgetAllocationInDetails.FindControl("txtExpenseTypeName");
        //temp.Text = row.ExpenseItemName;
        temp.Text = GetExpenseItemNameByID(row.ExpenseItemID);
        temp = (TextBox)this.fvBudgetAllocationInDetails.FindControl("txtPeriod");
        temp.Text = row.Period.ToString("yyyy/MM");
        temp = (TextBox)this.fvBudgetAllocationInDetails.FindControl("txtOriginalBudget");
        temp.Text = row.OriginalBudget.ToString();
        temp = (TextBox)this.fvBudgetAllocationInDetails.FindControl("txtNormalBudget");
        temp.Text = row.NormalBudget.ToString();
        temp = (TextBox)this.fvBudgetAllocationInDetails.FindControl("txtAdjustBudget");
        temp.Text = row.AdjustBudget.ToString();
        temp = (TextBox)this.fvBudgetAllocationInDetails.FindControl("txtTotalBudget");
        temp.Text = row.TotalBudget.ToString();
    }

    public string GetExpenseManageTypeNameById(object ID) {
        return new MasterDataBLL().GetExpenseManageTypeByID(int.Parse(ID.ToString())).ExpenseManageTypeName;
    }

    #endregion

    public string GetExpenseItemNameByID(object ExpenseItemID) {
        if (ExpenseItemID.ToString() != string.Empty) {
            int id = Convert.ToInt32(ExpenseItemID);
            ERS.ExpenseItemRow row = new MasterDataBLL().GetExpenseItemByID(id);
            return row.AccountingCode + "--" + row.AccountingName;
        } else {
            return null;
        }
    }

}
