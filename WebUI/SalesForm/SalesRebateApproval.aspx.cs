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

public partial class SaleForm_SalesRebateApproval : BasePage {

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
            this.BeginPeriodCtl.Text = rowFormApply.BeginPeriod.ToShortDateString();
            this.EndPeriodCtl.Text = rowFormApply.EndPeriod.ToShortDateString();
            MasterDataBLL masterBll = new MasterDataBLL();
            this.ExpenseSubCategoryCtl.Text = masterBll.GetExpenseSubCateNameById(rowFormApply.ExpenseSubCategoryID);
            ERS.CustomerRow customer = masterBll.GetCustomerById(rowFormApply.CustomerID);
            this.CustomerNameCtl.Text = customer.CustomerName;
            this.CustomerTypeCtl.Text = masterBll.GetCustomerTypeById(customer.CustomerTypeID).CustomerTypeName;
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

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                FormDS.FormRow rejectedForm = new SalesApplyBLL().GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/SalesForm/SalesRebateApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
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

            this.odsApplyDetails.SelectParameters["FormID"].DefaultValue = rowFormApply.FormApplyID.ToString();

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

            if (rowForm.StatusID == (int)SystemEnums.FormStatus.Rejected && stuffUser.StuffUserId == rowForm.UserID) {
                this.EditBtn.Visible = true;
                this.ScrapBtn.Visible = true;
            } else {
                this.EditBtn.Visible = false;
                this.ScrapBtn.Visible = false;
            }

            //关闭按钮
            if ((!rowFormApply.IsClose) && rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted) {
                if (stuffUser.StuffUserId == rowForm.UserID || new MasterDataBLL().GetProxyReimburseByParameter(rowForm.UserID, stuffUser.StuffUserId, rowForm.SubmitDate).Count > 0) {
                    this.CloseBtn.Visible = true;
                } else {
                    this.CloseBtn.Visible = false;
                }
            } else {
                this.CloseBtn.Visible = false;
            }

            //如果是弹出,取消按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.upButton.Visible = false;
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
            
        }
        this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
        this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
        this.cwfAppCheck.IsView = (bool)this.ViewState["IsView"];


    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        //如果是票扣的话自动生成报销但
       
        //int formApplyID = int.Parse(this.ViewState["ObjectId"].ToString());
        //FormDS.FormApplyRow formApply = this.SalesApplyBLL.GetFormApplyByID(formApplyID)[0];
        //if (formApply.PaymentTypeID == (int)SystemEnums.PaymentType.PiaoKou) {
        //    new APFlowBLL().GenerateRebateReimburse(formApplyID);
        //}
        try {
            if (this.cwfAppCheck.CheckInputData()) {
                AuthorizationDS.StuffUserRow currentStuff = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
                string ProxyStuffName = null;
                if (Session["ProxyStuffUserId"] != null) {
                    ProxyStuffName = new StuffUserBLL().GetStuffUserById(int.Parse(Session["ProxyStuffUserId"].ToString()))[0].StuffName;
                }
                new APFlowBLL().ApproveForm(CommonUtility.GetAPHelper(Session), this.cwfAppCheck.FormID, currentStuff.StuffUserId, currentStuff.StuffName,
                            this.cwfAppCheck.GetApproveOrReject(), this.cwfAppCheck.GetComments(), ProxyStuffName, int.Parse(ViewState["OrganizationUnitID"].ToString()));

                //如果是票扣的话自动生成报销单（返利申请单不会跨月）//如果是审批完成，对于返利自动认为执行完成

                FormDS.FormApplyRow formApply = this.SalesApplyBLL.GetFormApplyByID(this.cwfAppCheck.FormID)[0];
                if (this.SalesApplyBLL.GetFormByID(this.cwfAppCheck.FormID)[0].StatusID == 2) {
                    this.SalesApplyBLL.ExecuteConfirmForRebate(this.cwfAppCheck.FormID);
                    if(formApply.PaymentTypeID == (int)SystemEnums.PaymentType.PiaoKou){
                        new APFlowBLL().GenerateRebateReimburse(this.cwfAppCheck.FormID);
                    }
                }
                
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
        this.Response.Redirect("~/SalesForm/SalesRebateApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
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
            ERS.ExpenseItemRow row=new MasterDataBLL().GetExpenseItemByID(id);
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
}
