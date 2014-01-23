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

public partial class OtherForm_TravelReimburseApply : BasePage {
    decimal TotalFee = 0;

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

    #region 页面初始化及事件处理

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this.Page, "出差费用报销申请");
            this.Page.Title = "出差费用报销申请";

            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow rowUserPosition = (AuthorizationDS.PositionRow)Session["Position"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = rowUserPosition.PositionId;

            this.StuffNameCtl.Text = stuffUser.StuffName;
            this.PositionNameCtl.Text = rowUserPosition.PositionName;
            this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowUserPosition.OrganizationUnitId).OrganizationUnitName;
            this.ViewState["DepartmentID"] = rowUserPosition.OrganizationUnitId;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();

            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());
            }
            if (Request["FormTravelApplyID"] != null) {
                this.ViewState["FormTravelApplyID"] = int.Parse(Request["FormTravelApplyID"]);
                OpenTravelApplyForm(int.Parse(Request["FormTravelApplyID"]));
            }
            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                OpenForm(int.Parse(this.ViewState["ObjectId"].ToString()));
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
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
        FormPersonalReimburseTableAdapter taFormTravelReimburse = new FormPersonalReimburseTableAdapter();
        taFormTravelReimburse.FillByID(this.InnerDS.FormPersonalReimburse, formID);
        FormDS.FormPersonalReimburseRow rowFormTravelReimburse = this.InnerDS.FormPersonalReimburse[0];
        this.ViewState["FormTravelApplyID"] = rowFormTravelReimburse.FormTravelApplyID;
        //赋值
        this.PeriodDDL.DataSourceID = "odsPeriod";
        this.PeriodDDL.DataBind();
        ListItem item = this.PeriodDDL.Items.FindByText(rowFormTravelReimburse.Period.ToString("yyyy-MM"));
        if (item != null) {
            this.PeriodDDL.SelectedValue = item.Value;
        }
        if (!rowFormTravelReimburse.IsRemarkNull()) {
            this.RemarkCtl.Text = rowFormTravelReimburse.Remark;
        }
        if (!rowFormTravelReimburse.IsAttachedFileNameNull()) {
            this.UCFileUpload.AttachmentFileName = rowFormTravelReimburse.AttachedFileName;
        }
        if (!rowFormTravelReimburse.IsRealAttachedFileNameNull()) {
            this.UCFileUpload.RealAttachmentFileName = rowFormTravelReimburse.RealAttachedFileName;
        }

        OpenTravelApplyForm(rowFormTravelReimburse.FormTravelApplyID);
        // 打开明细表
        FormPersonalReimburseDetailTableAdapter taDetail = new FormPersonalReimburseDetailTableAdapter();
        taDetail.FillByFormID(this.InnerDS.FormPersonalReimburseDetail, formID);
    }

    protected void OpenTravelApplyForm(int TravelApplyID) {
        FormDS.FormTravelApplyRow travelApply = this.PersonalReimburseBLL.GetFormTravelApplyByID(TravelApplyID);
        if (!travelApply.IsTransportFeeNull()) {
            this.txtTransportFee.Text = travelApply.TransportFee.ToString("N");
        }
        if (!travelApply.IsHotelFeeNull()) {
            this.txtHotelFee.Text = travelApply.HotelFee.ToString("N");
        }
        if (!travelApply.IsMealFeeNull()) {
            this.txtMealFee.Text = travelApply.MealFee.ToString("N");
        }
        if (!travelApply.IsOtherFeeNull()) {
            this.txtOtherFee.Text = travelApply.OtherFee.ToString("N");
        }
        if (!travelApply.IsTotalFeeNull()) {
            this.txtTotal.Text = travelApply.TotalFee.ToString("N");
        }
        gvFormTravelApplyDetails.DataSource = new FormTravelApplyDetailTableAdapter().GetDataByApplyID(TravelApplyID);
        gvFormTravelApplyDetails.DataBind();
    }
    #endregion

    protected void CancelBtn_Click(object sender, EventArgs e) {
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.PersonalReimburseBLL.DeleteFormTravelReimburse(formID);
        this.Page.Response.Redirect("~/Home.aspx");
        SqlDataSource sds = new SqlDataSource();
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        if (!IsInputValid())
            return;
        SaveFormTravelReimburse(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        if (!IsInputValid())
            return;
        if (!IsSubmitValid())
            return;
        SaveFormTravelReimburse(SystemEnums.FormStatus.Awaiting);
    }

    protected void SaveFormTravelReimburse(SystemEnums.FormStatus StatusID) {
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

        DateTime Period = new MasterDataBLL().GetReimbursePeriodById(int.Parse(this.PeriodDDL.SelectedValue)).ReimbursePeriod;
        string Remark = this.RemarkCtl.Text;
        string AttachedFileName = this.UCFileUpload.AttachmentFileName;
        string RealAttachedFileName = this.UCFileUpload.RealAttachmentFileName;

        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.PersonalReimburseBLL.AddFormTravelReimburse(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID,
                  SystemEnums.FormType.PersonalReimburseApply, StatusID, Period, Remark, AttachedFileName, RealAttachedFileName, (int)this.ViewState["FormTravelApplyID"]);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.PersonalReimburseBLL.UpdateFormTravelReimburse(FormID, StatusID, SystemEnums.FormType.PersonalReimburseApply, Period,
                    Remark, AttachedFileName, RealAttachedFileName);
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    public bool IsInputValid() {
        if (this.PeriodDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this.Page, "请选择费用期间!");
            return false;
        }
        return true;
    }

    public bool IsSubmitValid() {
        if (this.gvTravelReimburseDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入明细项/明细项必须新增方可提交！");
            return false;
        }
        return true;
    }

    #region 数据绑定及事件

    protected void odsTravelReimburseDetails_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsTravelReimburseDetails_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
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

    protected void odsTravelReimburseDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormPersonalReimburseID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }
        UserControls_UCDateInput ucOccurDate = (UserControls_UCDateInput)this.fvTravelReimburseDetails.FindControl("UCOccurDate");
        e.InputParameters["OccurDate"] = ucOccurDate.SelectedDate;
        DropDownList dplExpenseManageType = (DropDownList)this.fvTravelReimburseDetails.FindControl("dplExpenseManageType");
        e.InputParameters["ExpenseManageTypeID"] = dplExpenseManageType.SelectedItem.Value;
        TextBox txtAmount = (TextBox)this.fvTravelReimburseDetails.FindControl("txtAmount");
        e.InputParameters["Amount"] = txtAmount.Text;
        TextBox txtRemark = (TextBox)this.fvTravelReimburseDetails.FindControl("txtRemark");
        e.InputParameters["Remark"] = txtRemark.Text;
        e.InputParameters["User"] = Session["StuffUser"];
    }

    protected void odsTravelReimburseDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["User"] = Session["StuffUser"];
    }

    protected void odsTravelReimburseDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        PersonalReimburseBLL bll = (PersonalReimburseBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void gvTravelReimburseDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormPersonalReimburseDetailRow row = (FormDS.FormPersonalReimburseDetailRow)drvDetail.Row;
                TotalFee = decimal.Round((TotalFee + row.Amount), 2);
            }
        }
        this.ViewState["ManualApplyFeeTotal"] = TotalFee;

        if (e.Row.RowType == DataControlRowType.Footer) {
            Label sumlbl = new Label();
            sumlbl.Text = "合计：";
            sumlbl.ID = "sumlbl";
            e.Row.Cells[2].Controls.Add(sumlbl);
            e.Row.Cells[2].CssClass = "RedTextAlignCenter";
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].Width = new Unit("250px");

            Label totallbl = new Label();
            totallbl.ID = "totallbl";
            totallbl.Text = TotalFee.ToString("N");
            e.Row.Cells[3].Controls.Add(totallbl);
            e.Row.Cells[3].CssClass = "RedTextAlignCenter";
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].Width = new Unit("150px");
        }
    }

    public string GetExpenseManageTypeNameById(object ID) {
        return new MasterDataBLL().GetExpenseManageTypeByID(int.Parse(ID.ToString())).ExpenseManageTypeName;
    }

    #endregion

}
