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
using System.Text.RegularExpressions;

public partial class OtherForm_TravelApply : BasePage {

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
            PageUtility.SetContentTitle(this.Page, "出差申请");
            this.Page.Title = "出差申请";

            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow UserPosition = (AuthorizationDS.PositionRow)Session["Position"];

            string NeedCostCenter = System.Configuration.ConfigurationManager.AppSettings["NeedCostCenter"];
            if ((!string.IsNullOrEmpty(NeedCostCenter)) && (new StuffUserBLL().GetCostCenterIDByPositionID(UserPosition.PositionId) == 0)) {
                this.Session["ErrorInfor"] = "未找到成本中心，请联系管理员";
                Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
            }
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = UserPosition.PositionId;

            this.StuffNameCtl.Text = stuffUser.StuffName;
            this.PositionNameCtl.Text = UserPosition.PositionName;
            this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(UserPosition.OrganizationUnitId).OrganizationUnitName;
            this.ViewState["DepartmentID"] = UserPosition.OrganizationUnitId;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();

            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());
            }

            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
                OpenForm(int.Parse(this.ViewState["ObjectId"].ToString()));
            } else {
                this.txtHotelFee.Text = "0";
                this.txtMealFee.Text = "0";
                this.txtOtherFee.Text = "0";
                this.txtTransportFee.Text = "0";
                this.txtTotalAmount.Text = "0";
                this.DeleteBtn.Visible = false;
            }
            if (Session["ProxyStuffUserId"] != null) {
                this.SubmitBtn.Visible = false;
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        base.OnLoadComplete(e);
        string methodName = string.Format("SetTotal('{0}','{1}','{2}','{3}','{4}')", txtTransportFee.ClientID, txtHotelFee.ClientID, txtMealFee.ClientID, txtOtherFee.ClientID, txtTotalAmount.ClientID);
        this.txtTransportFee.Attributes.Add("onBlur", methodName);
        this.txtHotelFee.Attributes.Add("onBlur", methodName);
        this.txtMealFee.Attributes.Add("onBlur", methodName);
        this.txtOtherFee.Attributes.Add("onBlur", methodName);
    }

    protected void OpenForm(int formID) {
        FormDS.FormRow rowForm = this.PersonalReimburseBLL.GetFormByID(formID)[0];
        FormDS.FormTravelApplyRow rowFormTravelApply = this.PersonalReimburseBLL.GetFormTravelApplyByID(formID);

        if (!rowFormTravelApply.IsTransportFeeNull()) {
            this.txtTransportFee.Text = rowFormTravelApply.TransportFee.ToString("N");
        } else {
            this.txtTransportFee.Text = "0";
        }
        if (!rowFormTravelApply.IsHotelFeeNull()) {
            this.txtHotelFee.Text = rowFormTravelApply.HotelFee.ToString("N");
        } else {
            this.txtHotelFee.Text = "0";
        }
        if (!rowFormTravelApply.IsMealFeeNull()) {
            this.txtMealFee.Text = rowFormTravelApply.MealFee.ToString("N");
        } else {
            this.txtMealFee.Text = "0";
        }
        if (!rowFormTravelApply.IsOtherFeeNull()) {
            this.txtOtherFee.Text = rowFormTravelApply.OtherFee.ToString("N");
        } else {
            this.txtOtherFee.Text = "0";
        }
        if (!rowFormTravelApply.IsTotalFeeNull()) {
            this.txtTotalAmount.Text = rowFormTravelApply.TotalFee.ToString("N");
        } else {
            this.txtOtherFee.Text = "0";
        }
        //赋值
        if (!rowFormTravelApply.IsRemarkNull()) {
            this.RemarkCtl.Text = rowFormTravelApply.Remark;
        }
        if (!rowFormTravelApply.IsAttachedFileNameNull() && !rowFormTravelApply.IsRealAttachedFileNameNull()) {
            this.UCFileUpload.AttachmentFileName = rowFormTravelApply.RealAttachedFileName;
            this.UCFileUpload.RealAttachmentFileName = rowFormTravelApply.RealAttachedFileName;
        }

        // 打开明细表
        FormTravelApplyDetailTableAdapter taDetail = new FormTravelApplyDetailTableAdapter();
        taDetail.FillByApplyID(this.InnerDS.FormTravelApplyDetail, formID);
    }

    protected void CancelBtn_Click(object sender, EventArgs e) {
        if (this.Request["Source"] != null) {
            this.Response.Redirect(this.Request["Source"].ToString());
        } else {
            this.Response.Redirect("~/Home.aspx");
        }
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.PersonalReimburseBLL.DeleteFormTravelApplyByID(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        SaveFormPersonal(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {

        if (!IsSubmitValid())
            return;
        SaveFormPersonal(SystemEnums.FormStatus.Awaiting);
    }

    public bool IsSubmitValid() {
        decimal temp = 0;
        if (string.IsNullOrEmpty(this.txtTransportFee.Text)) {
            PageUtility.ShowModelDlg(this, "必须输入交通费用！");
            return false;
        }
        if (!decimal.TryParse(this.txtTransportFee.Text, out temp)) {
            PageUtility.ShowModelDlg(this, "交通费用只能为数字！");
            return false;
        }
        if (string.IsNullOrEmpty(this.txtHotelFee.Text)) {
            PageUtility.ShowModelDlg(this, "必须输入住宿费用！");
            return false;
        }
        if (!decimal.TryParse(this.txtHotelFee.Text, out temp)) {
            PageUtility.ShowModelDlg(this, "住宿费用只能为数字！");
            return false;
        }
        if (string.IsNullOrEmpty(this.txtMealFee.Text)) {
            PageUtility.ShowModelDlg(this, "必须输入餐费！");
            return false;
        }
        if (!decimal.TryParse(this.txtMealFee.Text, out temp)) {
            PageUtility.ShowModelDlg(this, "餐费只能为数字！");
            return false;
        }
        if (string.IsNullOrEmpty(this.txtOtherFee.Text)) {
            PageUtility.ShowModelDlg(this, "必须输入其他费用！");
            return false;
        }
        if (!decimal.TryParse(this.txtOtherFee.Text, out temp)) {
            PageUtility.ShowModelDlg(this, "其他费用只能为数字！");
            return false;
        }
        return true;
    }

    protected void SaveFormPersonal(SystemEnums.FormStatus StatusID) {
        if (this.gvFormTravelApplyDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this, "请输入至少一条明细!");
            return;
        }
        this.PersonalReimburseBLL.FormDataSet = this.InnerDS;
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
        decimal? TransportFee = null;
        decimal? HotelFee = null;
        decimal? MealFee = null;
        decimal? OtherFee = null;
        decimal temp = 0;
        if ((!string.IsNullOrEmpty(this.txtTransportFee.Text)) && decimal.TryParse(this.txtTransportFee.Text, out temp)) {
            TransportFee = temp;
        }
        if ((!string.IsNullOrEmpty(this.txtHotelFee.Text)) && decimal.TryParse(this.txtHotelFee.Text, out temp)) {
            HotelFee = temp;
        }
        if ((!string.IsNullOrEmpty(this.txtMealFee.Text)) && decimal.TryParse(this.txtMealFee.Text, out temp)) {
            MealFee = temp;
        }
        if ((!string.IsNullOrEmpty(this.txtOtherFee.Text)) && decimal.TryParse(this.txtOtherFee.Text, out temp)) {
            OtherFee = temp;
        }

        string Remark = this.RemarkCtl.Text;
        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.PersonalReimburseBLL.AddFormTravelApply(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID,
                    SystemEnums.FormType.TravelApply, StatusID, TransportFee, HotelFee, MealFee, OtherFee, Remark, this.UCFileUpload.AttachmentFileName, this.UCFileUpload.RealAttachmentFileName);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.PersonalReimburseBLL.UpdateFormTravelApply(FormID, StatusID, TransportFee, HotelFee, MealFee, OtherFee, Remark, this.UCFileUpload.AttachmentFileName, this.UCFileUpload.RealAttachmentFileName);
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
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

}