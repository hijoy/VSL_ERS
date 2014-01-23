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

public partial class OtherForm_ContractApproval : BasePage {

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
            PageUtility.SetContentTitle(this.Page, "合同审批");
            this.Page.Title = "合同审批";

            int formID = int.Parse(Request["ObjectId"]);
            this.ViewState["ObjectId"] = formID;
            FormDS.FormRow rowForm = this.ContractApplyBLL.GetFormByID(formID)[0];
            FormDS.FormContractRow rowFormContract = this.ContractApplyBLL.GetFormContractByID(formID)[0];
            if (rowForm.IsProcIDNull()) {
                ViewState["ProcID"] = "";
            } else {
                ViewState["ProcID"] = rowForm.ProcID;
            }

            ViewState["OrganizationUnitID"] = rowForm.OrganizationUnitID;
            //对控件赋值
            this.txtFormNo.Text = rowForm.FormNo;
            this.ApplyDateCtl.Text = rowForm.SubmitDate.ToShortDateString();
            AuthorizationDS.StuffUserRow applicant = new AuthorizationBLL().GetStuffUserById(rowForm.UserID);
            this.StuffNameCtl.Text = applicant.StuffName;
            this.PositionNameCtl.Text = new OUTreeBLL().GetPositionById(rowForm.PositionID).PositionName;
            if (new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID) != null) {
                this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID).OrganizationUnitName;
            }
            this.AttendDateCtl.Text = applicant.AttendDate.ToShortDateString();

            this.txtContractName.Text = rowFormContract.ContractName;
            this.txtFirstCompany.Text = rowFormContract.FirstCompany;
            if (!rowFormContract.IsSecondCompanyNull()) {
                this.txtSecondCompany.Text = rowFormContract.SecondCompany;
            }
            if (!rowFormContract.IsThirdCompanyNull()) {
                this.txtThirdCompany.Text = rowFormContract.ThirdCompany;
            }
            this.txtContractNo.Text = rowFormContract.ContractNo;
            this.txtContractAmount.Text = rowFormContract.ContractAmount.ToString();
            this.txtPageNumber.Text = rowFormContract.PageNumber.ToString();
            this.txtContractType.Text = new MasterDataBLL().GetContractTypeById(rowFormContract.ContractTypeID).ContractTypeName;
            if (!rowFormContract.IsPaymentTypeNull()) {
                this.txtPaymentType.Text = rowFormContract.PaymentType;
            }
            if (!rowFormContract.IsBeginDateNull()) {
                this.txtBeginDate.Text = rowFormContract.BeginDate.ToShortDateString();
            }
            if (!rowFormContract.IsEndDateNull()) {
                this.txtEndDate.Text = rowFormContract.EndDate.ToShortDateString();
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

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
                trHistory.Visible = false;
            } else {
                FormDS.FormRow rejectedForm = new ContractApplyBLL().GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/OtherForm/ContractApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
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

            //如果是弹出,取消按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.upButton.Visible = false;
                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                }
            }

            //判断是否盖章
            this.IsStampCtl.Checked = rowFormContract.IsisStampedNull() ? false : rowFormContract.isStamped;
            this.IsRecoveryCtl.Checked = rowFormContract.IsisRecoveryNull() ? false : rowFormContract.isRecovery;
            int opContractInfoId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FormContract, SystemEnums.OperateEnum.Other);
            AuthorizationDS.PositionRow ViewerPosition = (AuthorizationDS.PositionRow)this.Session["Position"];
            if (new PositionRightBLL().CheckPositionRight(ViewerPosition.PositionId, opContractInfoId) && rowForm.StatusID == 2) {
                if (!rowFormContract.IsisStampedNull() && rowFormContract.isStamped) {
                    this.StampBtn.Visible = false;
                } else {
                    this.StampBtn.Visible = true;
                }
                if (!rowFormContract.IsisRecoveryNull() && rowFormContract.isRecovery) {
                    this.RecoveryBtn.Visible = false;
                } else {
                    this.RecoveryBtn.Visible = true;
                }
            } else {
                this.StampBtn.Visible = false;
                this.RecoveryBtn.Visible = false;
            }

        }

        this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
        this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
        this.cwfAppCheck.IsView = (bool)this.ViewState["IsView"];
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
        this.Response.Redirect("~/OtherForm/ContractApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void StampBtn_Click(object sender, EventArgs e) {
        try {
            this.ContractApplyBLL.ContractStamp(int.Parse(this.ViewState["ObjectId"].ToString()));
            PageUtility.ShowModelDlg(this.Page, "保存成功");
            this.IsStampCtl.Checked = true;
            this.StampBtn.Visible = false;
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    protected void RecoveryBtn_Click(object sender, EventArgs e) {
        try {
            this.ContractApplyBLL.ContractRecovery(int.Parse(this.ViewState["ObjectId"].ToString()));
            PageUtility.ShowModelDlg(this.Page, "保存成功");
            this.IsRecoveryCtl.Checked = true;
            this.RecoveryBtn.Visible = false;
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

}
    #endregion