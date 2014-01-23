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

public partial class OtherForm_TravelApproval : BasePage {

    private PersonalReimburseBLL _PersonalReimburseBLL;
    protected PersonalReimburseBLL PersonalReimburseBLL {
        get {
            if (this._PersonalReimburseBLL == null) {
                this._PersonalReimburseBLL = new PersonalReimburseBLL();
            }
            return this._PersonalReimburseBLL;
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

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this.Page, "出差审批");
            this.Page.Title = "出差审批";
            int formID = int.Parse(Request["ObjectID"]);
            this.ViewState["ObjectId"] = formID;
            QueryDS.FormViewRow rowForm = new FormQueryBLL().GetFormViewByID(formID);
            FormDS.FormTravelApplyRow applyRow = this.PersonalReimburseBLL.GetFormTravelApplyByID(formID);
            if (rowForm.IsProcIDNull()) {
                ViewState["ProcID"] = "";
            } else {
                ViewState["ProcID"] = rowForm.ProcID;
            }

            ViewState["OrganizationUnitID"] = rowForm.OrganizationUnitID;
            txtFormNo.Text = rowForm.FormNo;
            AuthorizationDS.StuffUserRow applicant = new AuthorizationBLL().GetStuffUserById(rowForm.UserID);
            this.StuffNameCtl.Text = applicant.StuffName;
            this.PositionNameCtl.Text = new OUTreeBLL().GetPositionById(rowForm.PositionID).PositionName;

            if (new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID) != null) {
                this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID).OrganizationUnitName;
            }

            this.AttendDateCtl.Text = applicant.AttendDate.ToShortDateString();
            if (!applyRow.IsTransportFeeNull()) {
                this.txtTransportFee.Text = applyRow.TransportFee.ToString("N");
            }
            if (!applyRow.IsHotelFeeNull()) {
                this.txtHotelFee.Text = applyRow.HotelFee.ToString("N");
            }
            if (!applyRow.IsMealFeeNull()) {
                this.txtMealFee.Text = applyRow.MealFee.ToString("N");
            }
            if (!applyRow.IsOtherFeeNull()) {
                this.txtOtherFee.Text = applyRow.OtherFee.ToString("N");
            }
            if (!applyRow.IsTotalFeeNull()) {
                this.txtTotal.Text = applyRow.TotalFee.ToString("N");
            }
            this.RemarkCtl.Text = applyRow.Remark;
            if (!applyRow.IsAttachedFileNameNull() && !applyRow.IsRealAttachedFileNameNull()) {
                this.UCFileUpload.AttachmentFileName = applyRow.RealAttachedFileName;
                this.UCFileUpload.RealAttachmentFileName = applyRow.RealAttachedFileName;
            }

            // 打开明细表
            FormTravelApplyDetailTableAdapter taDetail = new FormTravelApplyDetailTableAdapter();
            taDetail.FillByApplyID(this.InnerDS.FormTravelApplyDetail, formID);

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                FormDS.FormRow rejectedForm = this.PersonalReimburseBLL.GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/OtherForm/TravelApproval.aspx?ShowDialog=1&ObjectID=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }

            //审批页面处理&按钮处理
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            if (Session["ProxyStuffUserId"] == null && rowForm.InTurnUserIds.Contains("P" + stuffUser.StuffUserId + "P")) {
                this.SubmitBtn.Visible = true;
                this.cwfAppCheck.IsView = false;
                this.ViewState["IsView"] = false;
            } else {
                this.SubmitBtn.Visible = false;
                this.cwfAppCheck.IsView = true;
                this.ViewState["IsView"] = true;
            }

            if (rowForm.StatusID == (int)SystemEnums.FormStatus.Rejected && (stuffUser.StuffUserId == rowForm.UserID)) {
                this.EditBtn.Visible = true;
                this.ScrapBtn.Visible = true;
            } else {
                this.EditBtn.Visible = false;
                this.ScrapBtn.Visible = false;
            }

            //如果是弹出,取消按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.upButton.Visible = false;
                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                }
            }
        }
        this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
        this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
        this.cwfAppCheck.IsView = (bool)this.ViewState["IsView"];
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        try {
            if (this.cwfAppCheck.CheckInputData()) {
                String attachmentName = string.Empty;
                String realAttachmentName = string.Empty;
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

    protected void EditBtn_Click(object sender, EventArgs e) {
        this.Response.Redirect("~/OtherForm/TravelApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    #region 数据绑定及事件

    protected void odsFormTravelApplyDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        PersonalReimburseBLL bll = (PersonalReimburseBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void gvFormTravelApplyDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormTravelApplyDetailRow row = (FormDS.FormTravelApplyDetailRow)drvDetail.Row;
            }
        }
    }

    #endregion

    protected void CancelBtn_Click(object sender, EventArgs e) {
        if (this.Request["Source"] != null) {
            this.Response.Redirect(this.Request["Source"].ToString());
        } else {
            this.Response.Redirect("~/Home.aspx");
        }
    }
}