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

public partial class SaleForm_SalesGeneralApproval : BasePage {

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
            this.BeginPeriodCtl.Text = rowFormApply.BeginPeriod.ToString("yyyy-MM");
            this.EndPeriodCtl.Text = rowFormApply.EndPeriod.ToString("yyyy-MM");
            MasterDataBLL masterBll = new MasterDataBLL();
            this.ExpenseSubCategoryCtl.Text = masterBll.GetExpenseSubCateNameById(rowFormApply.ExpenseSubCategoryID);
            ERS.CustomerRow customer = masterBll.GetCustomerById(rowFormApply.CustomerID);
            this.CustomerNameCtl.Text = customer.CustomerName;
            //this.CustomerTypeCtl.Text = masterBll.GetCustomerTypeById(customer.CustomerTypeID).CustomerTypeName;
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

            if (!rowFormApply.IsPromotionBeginDateNull()) {
                this.PromotionBeginCtl.Text = rowFormApply.PromotionBeginDate.ToString("yyyy-MM-dd");
            }
            if (!rowFormApply.IsPromotionEndDateNull()) {
                this.PromotionEndCtl.Text = rowFormApply.PromotionEndDate.ToString("yyyy-MM-dd");
            }
            this.PromotionScopeCtl.Text = masterBll.GetPromotionScopeById(rowFormApply.PromotionScopeID).PromotionScopeName;
            this.PromotionTypeCtl.Text = masterBll.GetPromotionTypeById(rowFormApply.PromotionTypeID).PromotionTypeName;
            if (!rowFormApply.IsPromotionDescNull()) {
                this.PromotionDescCtl.Text = rowFormApply.PromotionDesc;
            }
            this.ShelfTypeCtl.Text = masterBll.GetShelfTypeById(rowFormApply.ShelfTypeID).ShelfTypeName;
            if (!rowFormApply.IsFirstVolumeNull()) {
                this.FirstVolumeCtl.Text = rowFormApply.FirstVolume.ToString();
            }
            if (!rowFormApply.IsSecondVolumeNull()) {
                this.SecondVolumeCtl.Text = rowFormApply.SecondVolume.ToString();
            }
            if (!rowFormApply.IsThirdVolumeNull()) {
                this.ThirdVolumeCtl.Text = rowFormApply.ThirdVolume.ToString();
            }
            if (!rowFormApply.IsAverageVolumeNull()) {
                this.AverageVolumeCtl.Text = rowFormApply.AverageVolume.ToString();
            }
            if (!rowFormApply.IsEstimatedSaleVolumeNull()) {
                this.txtEstimatedSaleVolume.Text = rowFormApply.EstimatedSaleVolume.ToString();
            }
            if (!rowFormApply.IsPackageUnitPriceNull()) {
                this.txtPackageUnitPrice.Text = rowFormApply.PackageUnitPrice.ToString("N");
            }
            if (!rowFormApply.IsFormApplyNameNull()) {
                this.txtFormApplyName.Text = rowFormApply.FormApplyName;
            }
            if (!rowFormApply.IsFormApplyNameNull()) {
                this.txtFormApplyName.Text = rowFormApply.FormApplyName;
            }

            if (!rowFormApply.IsReimburseRequirementsNull()) {
                int ReimburseRequirement = rowFormApply.ReimburseRequirements;
                if ((ReimburseRequirement & (int)SystemEnums.ReimburseRequirements.Picture) == (int)SystemEnums.ReimburseRequirements.Picture) {
                    chkListReimburseRequirements.Items[0].Selected = true;
                }
                if ((ReimburseRequirement & (int)SystemEnums.ReimburseRequirements.Agreement) == (int)SystemEnums.ReimburseRequirements.Agreement) {
                    chkListReimburseRequirements.Items[1].Selected = true;
                }
                if ((ReimburseRequirement & (int)SystemEnums.ReimburseRequirements.DeliveryOrder) == (int)SystemEnums.ReimburseRequirements.DeliveryOrder) {
                    chkListReimburseRequirements.Items[2].Selected = true;
                }
                if ((ReimburseRequirement & (int)SystemEnums.ReimburseRequirements.Contract) == (int)SystemEnums.ReimburseRequirements.Contract) {
                    chkListReimburseRequirements.Items[3].Selected = true;
                }
                if ((ReimburseRequirement & (int)SystemEnums.ReimburseRequirements.DM) == (int)SystemEnums.ReimburseRequirements.DM) {
                    chkListReimburseRequirements.Items[4].Selected = true;
                }
                if ((ReimburseRequirement & (int)SystemEnums.ReimburseRequirements.Other) == (int)SystemEnums.ReimburseRequirements.Other) {
                    chkListReimburseRequirements.Items[5].Selected = true;
                }
            }

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                FormDS.FormRow rejectedForm = new SalesApplyBLL().GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/SalesForm/SalesGeneralApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }

            //查看预算信息按钮
            this.btnViewBudget.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/ReportManage/SalesBudgetByPosition.aspx?ShowDialog=1','', 'dialogWidth:1035px;dialogHeight:660px;resizable:yes;')";

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

            //是否显示复制按钮
            if (rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted && stuffUser.StuffUserId == rowForm.UserID) {
                this.UCBeginPeriod.Visible = true;
                this.UCEndPeriod.Visible = true;
                this.lblSignal.Visible = true;
                this.CopyBtn.Visible = true;
            } else {
                this.UCBeginPeriod.Visible = false;
                this.UCEndPeriod.Visible = false;
                this.lblSignal.Visible = false;
                this.CopyBtn.Visible = false;
            }
            //是否显示打印信息
            if (((stuffUser.StuffUserId == rowForm.UserID || base.IsBusinessProxy(rowForm.UserID, rowForm.SubmitDate)) && (rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted) || (rowForm.StatusID == (int)SystemEnums.FormStatus.Scrap && rowFormApply.IsAutoSplit))) {
                this.PrintBtn.Visible = true;
            } else {
                this.PrintBtn.Visible = false;
            }

            if (rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted || (rowForm.StatusID == (int)SystemEnums.FormStatus.Scrap && rowFormApply.IsAutoSplit)) {
                this.PrintInfor.Visible = true;
                int printCount = rowFormApply.IsPrintCountNull() ? 0 : rowFormApply.PrintCount;
                this.PrintInfor.Text = "该方案已经被打印" + printCount + "次";
            } else {
                this.PrintInfor.Visible = false;
            }


            //如果是弹出,取消按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.upButton.Visible = false;
                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                    this.PrintInfor.Visible = false;
                    this.PrintBtn.Visible = false;
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

            //分摊比例
            new FormApplySplitRateTableAdapter().FillByApplyID(InnerDS.FormApplySplitRate, formID);
            if (InnerDS.FormApplySplitRate != null && InnerDS.FormApplySplitRate.Count > 0) {
                this.divSplitRate.Visible = true;
                this.gvSplitRate.Visible = true;
                this.gvSplitRate.DataSource = InnerDS.FormApplySplitRate;
                this.gvSplitRate.DataBind();
            }

            //如果同一门店
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

                //如果是跨越的话，拆分申请单
                FormDS.FormApplyRow formApply = this.SalesApplyBLL.GetFormApplyByID(this.cwfAppCheck.FormID)[0];
                FormDS.FormRow formRow = this.SalesApplyBLL.GetFormByID(this.cwfAppCheck.FormID)[0];
                //审批完成后，就应该是执行确认的步骤，要更改pagetype
                if (formRow.StatusID == 2) {
                    formRow.PageType = (int)SystemEnums.PageType.GeneralApplyExecute;
                    new FormTableAdapter().Update(formRow);
                    if (formApply.EndPeriod > formApply.BeginPeriod) {
                        new APFlowBLL().GenerateApplyForm(this.cwfAppCheck.FormID);
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

    protected void CopyBtn_Click(object sender, EventArgs e) {
        string stringBeginPeriod = ((TextBox)(this.UCBeginPeriod.FindControl("txtDate"))).Text.Trim();
        if (stringBeginPeriod == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择起始费用期间!");
            return;
        }
        DateTime beginPeriod = DateTime.Parse(stringBeginPeriod.Substring(0, 4) + "-" + stringBeginPeriod.Substring(4, 2) + "-01");
        string stringEndPeriod = ((TextBox)(this.UCEndPeriod.FindControl("txtDate"))).Text.Trim();
        if (stringEndPeriod == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择截止费用期间!");
            return;
        }
        DateTime endPeriod = DateTime.Parse(stringEndPeriod.Substring(0, 4) + "-" + stringEndPeriod.Substring(4, 2) + "-01");
        if (endPeriod < beginPeriod) {
            PageUtility.ShowModelDlg(this, "截止费用期间必须大于等于起始费用期间!");
            return;
        }
        try {
            this.SalesApplyBLL.CopyApplyForm(int.Parse(this.ViewState["ObjectId"].ToString()), beginPeriod, endPeriod);
            this.Response.Redirect("~/Home.aspx");
        } catch (Exception exception) {
            PageUtility.DealWithException(this, exception);
        }
    }

    protected void EditBtn_Click(object sender, EventArgs e) {
        this.Response.Redirect("~/SalesForm/SalesGeneralApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void PrintBtn_Click(object sender, EventArgs e) {
        try {
            this.SalesApplyBLL.PrintCountForFormApply(int.Parse(this.ViewState["ObjectId"].ToString()));
            FormDS.FormApplyRow applyRow = new BusinessObjects.FormDSTableAdapters.FormApplyTableAdapter().GetDataByID(int.Parse(this.ViewState["ObjectId"].ToString()))[0];
            this.PrintInfor.Text = "该方案已经被打印" + applyRow.PrintCount + "次";
        } catch (Exception exception) {
            PageUtility.DealWithException(this, exception);
        }
    }

    public string GetProductNameByID(object skuID) {
        if (skuID.ToString() != string.Empty) {
            int id = Convert.ToInt32(skuID);
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

    protected void gvApplyDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormApplyDetailViewRow row = (FormDS.FormApplyDetailViewRow)drvDetail.Row;

                manualApplyFeeTotal = decimal.Round((manualApplyFeeTotal + row.Amount), 2);

                CommonUtility.GenerateRepeatControl(e.Row.Cells[3].Controls, row.IsRepeatFormInfoNull() ? "" : row.RepeatFormInfo);
            }
        }

        this.ViewState["ManualApplyFeeTotal"] = manualApplyFeeTotal;

        if (e.Row.RowType == DataControlRowType.Footer) {
            Label sumlbl = new Label();
            sumlbl.Text = "合计：";
            e.Row.Cells[1].Controls.Add(sumlbl);
            e.Row.Cells[1].CssClass = "RedTextAlignCenter";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].Width = new Unit("250px");

            Label totallbl = new Label();
            totallbl.Text = manualApplyFeeTotal.ToString("N");
            e.Row.Cells[2].Controls.Add(totallbl);
            e.Row.Cells[2].CssClass = "RedTextAlignCenter";
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].Width = new Unit("100px");
        }
    }
}
