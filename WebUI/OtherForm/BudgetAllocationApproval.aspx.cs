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
using System.Configuration;
using BusinessObjects;
using BusinessObjects.FormDSTableAdapters;

public partial class OtherForm_BudgetAllocationApproval : BasePage {

    decimal OutTotalFee = 0;
    decimal InTotalFee = 0;

    ListItemCollection m_itemList = new ListItemCollection();
    public ListItemCollection itemList {
        get { return m_itemList; }
    }

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
            PageUtility.SetContentTitle(this.Page, "预算调拨审批");
            this.Page.Title = "预算调拨审批";

            int formID = int.Parse(Request["ObjectId"]);
            this.ViewState["ObjectId"] = formID;

            FormDS.FormRow rowForm = this.BudgetAllocationApplyBLL.GetFormByID(formID)[0];
            FormDS.FormBudgetAllocationRow rowBudgetAllocation = this.BudgetAllocationApplyBLL.GetFormBudgetAllocationByID(formID)[0];

            if (rowForm.IsProcIDNull()) {
                ViewState["ProcID"] = "";
            } else {
                ViewState["ProcID"] = rowForm.ProcID;
            }

            ViewState["OrganizationUnitID"] = rowForm.OrganizationUnitID;
            //对控件进行赋值
            this.txtFormNo.Text = rowForm.FormNo;
            this.ApplyDateCtl.Text = rowForm.SubmitDate.ToShortDateString();
            AuthorizationDS.StuffUserRow applicant = new AuthorizationBLL().GetStuffUserById(rowForm.UserID);
            this.StuffNameCtl.Text = applicant.StuffName;
            this.PositionNameCtl.Text = new OUTreeBLL().GetPositionById(rowForm.PositionID).PositionName;
            if (new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID) != null) {
                this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID).OrganizationUnitName;
            }
            this.AttendDateCtl.Text = applicant.AttendDate.ToShortDateString();
            this.RemarkCtl.Text = rowBudgetAllocation.Remark;

            this.odsBudgetAllocationInDetails.SelectParameters["FormBudgetAllocationID"].DefaultValue = rowBudgetAllocation.FormBudgetAllocationID.ToString();
            this.odsBudgetAllocationOutDetails.SelectParameters["FormBudgetAllocationID"].DefaultValue = rowBudgetAllocation.FormBudgetAllocationID.ToString();

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                FormDS.FormRow rejectedForm = new BudgetAllocationApplyBLL().GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/OtherForm/BudgetAllocationApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }

            //审批页面处理&按钮处理
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            if (rowForm.InTurnUserIds.Contains("P" + stuffUser.StuffUserId + "P")) {
                this.SubmitBtn.Visible = true;
                this.cwfAppCheck.IsView = false;
                this.ViewState["IsView"] = false;
            } else {
                this.SubmitBtn.Visible = false;
                this.cwfAppCheck.IsView = true;
                this.ViewState["IsView"] = true;
            }

            if (rowForm.StatusID == (int)SystemEnums.FormStatus.Rejected && ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId == rowForm.UserID) {
                this.EditBtn.Visible = true;
                this.ScrapBtn.Visible = true;
            } else {
                this.EditBtn.Visible = false;
                this.ScrapBtn.Visible = false;
            }

            //如果是弹出,按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.upButton.Visible = false;
                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                }
            }
            this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
            this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
            this.cwfAppCheck.IsView = (bool)this.ViewState["IsView"];
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

    protected void odsBudgetAllocationDetails_Inserted(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    public string GetExpenseManageTypeNameById(object ID) {
        return new MasterDataBLL().GetExpenseManageTypeByID(int.Parse(ID.ToString())).ExpenseManageTypeName;
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        try {
            if (this.cwfAppCheck.CheckInputData()) {
                AuthorizationDS.StuffUserRow currentStuff = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
                string ProxyStuffName = null;
                if (Session["ProxyStuffUserId"] != null) {
                    ProxyStuffName = new StuffUserBLL().GetStuffUserById(int.Parse(Session["ProxyStuffUserId"].ToString()))[0].StuffName;
                }
                new APFlowBLL().ApproveForm(CommonUtility.GetAPHelper(Session), this.cwfAppCheck.FormID, currentStuff.StuffUserId, currentStuff.StuffName,
                            this.cwfAppCheck.GetApproveOrReject(), this.cwfAppCheck.GetComments(), ProxyStuffName, int.Parse(ViewState["OrganizationUnitID"].ToString()));
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

    protected void CancelBtn_Click(object sender, EventArgs e) {
        if (this.Request["Source"] != null) {
            this.Response.Redirect(this.Request["Source"].ToString());
        } else {
            this.Response.Redirect("~/Home.aspx");
        }
    }

    protected void EditBtn_Click(object sender, EventArgs e) {
        this.Response.Redirect("~/OtherForm/BudgetAllocationApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void gvBudgetAllocationInDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormBudgetAllocationDetailRow row = (FormDS.FormBudgetAllocationDetailRow)drvDetail.Row;
                InTotalFee = decimal.Round((InTotalFee + row.TransferBudget), 2);
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label applbl = (Label)e.Row.FindControl("lblTotal");
                applbl.Text = InTotalFee.ToString("N");
            }
        }
    }

    protected void gvBudgetAllocationOutDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormBudgetAllocationDetailRow row = (FormDS.FormBudgetAllocationDetailRow)drvDetail.Row;
                OutTotalFee = decimal.Round((OutTotalFee + row.TransferBudget), 2);
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label applbl = (Label)e.Row.FindControl("lblTotal");
                applbl.Text = OutTotalFee.ToString("N");
            }
        }
    }

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
