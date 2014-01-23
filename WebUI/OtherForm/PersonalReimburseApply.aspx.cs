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

public partial class OtherForm_PersonalReimburseApply : BasePage {
    decimal TotalFee = 0;

    private PersonalReimburseBLL m_PersonalReimburseBLL;
    protected PersonalReimburseBLL PersonalReimburseBLL {
        get {
            if (this.m_PersonalReimburseBLL == null) {
                this.m_PersonalReimburseBLL = new PersonalReimburseBLL();
            }
            return this.m_PersonalReimburseBLL;
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
            PageUtility.SetContentTitle(this.Page, "个人费用报销申请");
            this.Page.Title = "个人费用报销申请";

            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow rowUserPosition = (AuthorizationDS.PositionRow)Session["Position"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = rowUserPosition.PositionId;
            this.ViewState["FlowTemplate"] = new AuthorizationBLL().GetFlowTemplate((int)SystemEnums.BusinessUseCase.FormPersonalReimburse, stuffUser.StuffUserId);

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
        FormPersonalReimburseTableAdapter taFormPersonalReimburse = new FormPersonalReimburseTableAdapter();
        taFormPersonalReimburse.FillByID(this.InnerDS.FormPersonalReimburse, formID);
        FormDS.FormPersonalReimburseRow rowFormPersonalReimburse = this.InnerDS.FormPersonalReimburse[0];
        //赋值
        this.PeriodDDL.DataSourceID = "odsPeriod";
        this.PeriodDDL.DataBind();
        ListItem item = this.PeriodDDL.Items.FindByText(rowFormPersonalReimburse.Period.ToString("yyyy-MM"));
        if (item != null) {
            this.PeriodDDL.SelectedValue = item.Value;
        }
        if (!rowFormPersonalReimburse.IsRemarkNull()) {
            this.RemarkCtl.Text = rowFormPersonalReimburse.Remark;
        }
        if (!rowFormPersonalReimburse.IsAttachedFileNameNull()) {
            this.UCFileUpload.AttachmentFileName = rowFormPersonalReimburse.AttachedFileName;
        }
        if (!rowFormPersonalReimburse.IsRealAttachedFileNameNull()) {
            this.UCFileUpload.RealAttachmentFileName = rowFormPersonalReimburse.RealAttachedFileName;
        }

        // 打开明细表
        FormPersonalReimburseDetailTableAdapter taDetail = new FormPersonalReimburseDetailTableAdapter();
        taDetail.FillByFormID(this.InnerDS.FormPersonalReimburseDetail, formID);
    }


    #endregion

    protected void CancelBtn_Click(object sender, EventArgs e) {
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.PersonalReimburseBLL.DeleteFormPersonalReimburse(formID);
        this.Page.Response.Redirect("~/Home.aspx");
        SqlDataSource sds = new SqlDataSource();
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        if (!IsInputValid())
            return;
        SaveFormPersonalReimburse(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        if (!IsInputValid())
            return;
        if (!IsSubmitValid())
            return;
        SaveFormPersonalReimburse(SystemEnums.FormStatus.Awaiting);
    }

    protected void SaveFormPersonalReimburse(SystemEnums.FormStatus StatusID) {
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
                this.PersonalReimburseBLL.AddFormPersonalReimburse(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID,
                  SystemEnums.FormType.PersonalReimburseApply, StatusID, Period, Remark, AttachedFileName, RealAttachedFileName, ViewState["FlowTemplate"].ToString());
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.PersonalReimburseBLL.UpdateFormPersonalReimburse(FormID, StatusID, SystemEnums.FormType.PersonalReimburseApply, Period,
                    Remark, AttachedFileName, RealAttachedFileName, ViewState["FlowTemplate"].ToString());
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
        if (this.gvPersonalReimburseDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入明细项/明细项必须新增方可提交！");
            return false;
        }
        return true;
    }

    #region 数据绑定及事件

    protected void odsPersonalReimburseDetails_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsPersonalReimburseDetails_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsPersonalReimburseDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormPersonalReimburseID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }
        UserControls_UCDateInput ucOccurDate = (UserControls_UCDateInput)this.fvPersonalReimburseDetails.FindControl("UCOccurDate");
        e.InputParameters["OccurDate"] = ucOccurDate.SelectedDate;

        if (DateTime.Parse(ucOccurDate.SelectedDate) > DateTime.Now) {
            PageUtility.ShowModelDlg(this, "发生日期填写不正确！");
            e.Cancel = true;
            return;
        }

        DropDownList dplExpenseManageType = (DropDownList)this.fvPersonalReimburseDetails.FindControl("dplExpenseManageType");
        e.InputParameters["ExpenseManageTypeID"] = dplExpenseManageType.SelectedItem.Value;
        TextBox txtAmount = (TextBox)this.fvPersonalReimburseDetails.FindControl("txtAmount");
        e.InputParameters["Amount"] = txtAmount.Text;
        TextBox txtRemark = (TextBox)this.fvPersonalReimburseDetails.FindControl("txtRemark");
        e.InputParameters["Remark"] = txtRemark.Text;
        e.InputParameters["User"] = Session["StuffUser"];
    }

    protected void odsPersonalReimburseDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormPersonalReimburseID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }

        UserControls_UCDateInput ucOccurDate = (UserControls_UCDateInput)this.gvPersonalReimburseDetails.Rows[gvPersonalReimburseDetails.EditIndex].FindControl("UCOccurDate");
        if (DateTime.Parse(ucOccurDate.SelectedDate) > DateTime.Now) {
            PageUtility.ShowModelDlg(this, "发生日期填写不正确！");
        }
        e.InputParameters["User"] = Session["StuffUser"];
    }

    protected void odsPersonalReimburseDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        PersonalReimburseBLL bll = (PersonalReimburseBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void gvPersonalReimburseDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
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
            e.Row.Cells[1].Controls.Add(sumlbl);
            e.Row.Cells[1].CssClass = "RedTextAlignCenter";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].Width = new Unit("350px");

            Label totallbl = new Label();
            totallbl.ID = "totallbl";
            totallbl.Text = TotalFee.ToString("N");
            e.Row.Cells[2].Controls.Add(totallbl);
            e.Row.Cells[2].CssClass = "RedTextAlignCenter";
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].Width = new Unit("150px");
        }
    }

    public string GetExpenseManageTypeNameById(object ID) {
        return new MasterDataBLL().GetExpenseManageTypeByID(int.Parse(ID.ToString())).ExpenseManageTypeName;
    }

    #endregion

}
