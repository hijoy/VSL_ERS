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
using System.Threading;

public partial class OtherForm_ContractApply : BasePage {

    decimal TotalFee = 0;

    private ContractApplyBLL m_ContractApplyBLL;
    protected ContractApplyBLL ContractApplyBLL {
        get {
            if (this.m_ContractApplyBLL == null) {
                this.m_ContractApplyBLL = new ContractApplyBLL();
            }
            return this.m_ContractApplyBLL;
        }
    }

    #region 页面初始化及事件处理

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this.Page, "合同申请");
            this.Page.Title = "合同申请";

            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow rowUserPosition = (AuthorizationDS.PositionRow)Session["Position"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = rowUserPosition.PositionId;
            this.ViewState["FlowTemplate"] = new AuthorizationBLL().GetFlowTemplate((int)SystemEnums.BusinessUseCase.FormContract, stuffUser.StuffUserId);
           
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
        FormDS.FormRow rowForm = taForm.GetDataByID(formID)[0];
        FormContractTableAdapter taFormContract = new FormContractTableAdapter();
        FormDS.FormContractRow rowFormContract = taFormContract.GetDataById(formID)[0];
        //赋值
        TextBox txtBeginDate = (TextBox)(this.UCPeriodBegin.FindControl("txtDate"));
        TextBox txtEndDate = (TextBox)(this.UCPeriodEnd.FindControl("txtDate"));
        this.txtFirstCompany.Text = rowFormContract.FirstCompany;
        if (!rowFormContract.IsSecondCompanyNull()) {
            this.txtSecondCompany.Text = rowFormContract.SecondCompany;
        }
        if (!rowFormContract.IsThirdCompanyNull()) {
            this.txtThirdCompany.Text = rowFormContract.ThirdCompany;
        }
        this.txtContractName.Text = rowFormContract.ContractName;
        this.txtContractAmount.Text = rowFormContract.ContractAmount.ToString();
        this.txtPageNumber.Text = rowFormContract.PageNumber.ToString();
        this.dplContractType.SelectedValue = rowFormContract.ContractTypeID.ToString();
        if (!rowFormContract.IsPaymentTypeNull()) {
            this.txtPaymentType.Text = rowFormContract.PaymentType;
        }
        if (!rowFormContract.IsBeginDateNull()) {
            txtBeginDate.Text = rowFormContract.BeginDate.ToShortDateString();
        }
        if (!rowFormContract.IsEndDateNull()) {
            txtEndDate.Text = rowFormContract.EndDate.ToShortDateString();
        }
        if (!rowFormContract.IsMainContentNull()) {
            this.txtMainContent.Text = rowFormContract.MainContent;
        }
        if (!rowFormContract.IsChangePartNull()) {
            this.txtChangePart.Text = rowFormContract.ChangePart;
        }
        if (!rowFormContract.IsAttachedFileNameNull()) {
            this.UCFileUpload.AttachmentFileName = rowFormContract.AttachedFileName;
        }
        if (!rowFormContract.IsRealAttachedFileNameNull()) {
            this.UCFileUpload.RealAttachmentFileName = rowFormContract.RealAttachedFileName;
        }
    }

    #endregion

    protected void CancelBtn_Click(object sender, EventArgs e) {
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.ContractApplyBLL.DeleteFormContract(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        Thread.Sleep(8000);
        if (!IsInputValid())
            return;
        SaveFormContractl(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        if (!IsInputValid())
            return;
        if (!IsSubmitValid())
            return;
        SaveFormContractl(SystemEnums.FormStatus.Awaiting);
    }

    protected void SaveFormContractl(SystemEnums.FormStatus StatusID) {
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

        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();

        string ContractName = this.txtContractName.Text;
        int ContractTypeID = int.Parse(this.dplContractType.SelectedValue.ToString());
        decimal ContractAmount = decimal.Parse(this.txtContractAmount.Text.ToString());
        int PageNumber = int.Parse(this.txtPageNumber.Text.ToString());
        string FirstCompany = this.txtFirstCompany.Text;
        string SecondCompany = this.txtSecondCompany.Text;
        string ThirdCompany = this.txtThirdCompany.Text;
        DateTime BeginDate = DateTime.Parse(startPeriod);
        DateTime EndDate = DateTime.Parse(endPeriod);
        string PaymentType = this.txtPaymentType.Text;
        string MainContent = this.txtMainContent.Text;
        string ChangePart = this.txtChangePart.Text;
        string AttachedFileName = this.UCFileUpload.AttachmentFileName;
        string RealAttachedFileName = this.UCFileUpload.RealAttachmentFileName;

        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.ContractApplyBLL.AddFormContract(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.ContractApply, StatusID, ContractName, ContractTypeID, ContractAmount, PageNumber, FirstCompany, SecondCompany, ThirdCompany, BeginDate, EndDate, PaymentType, MainContent, ChangePart, AttachedFileName, RealAttachedFileName, this.ViewState["FlowTemplate"].ToString());
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.ContractApplyBLL.UpdateFormContract(FormID, StatusID, SystemEnums.FormType.ContractApply, ContractName, ContractTypeID, ContractAmount, PageNumber, FirstCompany, SecondCompany, ThirdCompany, BeginDate, EndDate, PaymentType, MainContent, ChangePart, AttachedFileName, RealAttachedFileName, this.ViewState["FlowTemplate"].ToString());
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    public bool IsInputValid() {
        if (this.txtFirstCompany.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入对方单位名称!");
            return false;
        }
        if (this.txtContractName.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入合同名称!");
            return false;
        }
        if (this.txtContractAmount.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入合同金额!");
            return false;
        } else {
            try {
                decimal.Parse(this.txtContractAmount.Text);
            } catch (Exception ex) {
                PageUtility.ShowModelDlg(this.Page, "合同金额必须为数字!");
                return false;
            }
        }
        if (this.txtPageNumber.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入合同正本页数!");
            return false;
        } else {
            try {
                decimal.Parse(this.txtPageNumber.Text);
            } catch (Exception ex) {
                PageUtility.ShowModelDlg(this.Page, "合同正本页数必须为数字!");
                return false;
            }
        }
        if (this.dplContractType.SelectedValue == null || this.dplContractType.SelectedValue == string.Empty || this.dplContractType.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this.Page, "请录入合同类型!");
            return false;
        }
        if (this.UCPeriodBegin.SelectedDate == string.Empty || this.UCPeriodEnd.SelectedDate == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入合同期间!");
            return false;
        }
        if (this.txtMainContent.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入合同主要内容!");
            return false;
        }
        if (this.UCFileUpload.AttachmentFileName == null || this.UCFileUpload.RealAttachmentFileName == null || this.UCFileUpload.AttachmentFileName == string.Empty || this.UCFileUpload.RealAttachmentFileName == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请上传附件!");
            return false;
        }
        return true;
    }

    public bool IsSubmitValid() {

        return true;
    }
    protected override void OnUnload(EventArgs e) {
        base.OnUnload(e);
        
    }
}
