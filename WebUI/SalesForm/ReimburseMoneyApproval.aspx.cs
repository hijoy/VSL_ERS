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

public partial class SaleForm_ReimburseMoneyApproval : BasePage {

    decimal ApplyFeeTotal = 0;
    decimal RemainFeeTotal = 0;
    decimal ReimburseFeeTotal = 0;
    decimal InvoiceFeeTotal = 0;
    decimal PrePaidFeeTaltal = 0;
    decimal TaxFeeTotal = 0;

    private SalesReimburseBLL m_SalesReimburseBLL;
    protected SalesReimburseBLL SalesReimburseBLL {
        get {
            if (this.m_SalesReimburseBLL == null) {
                this.m_SalesReimburseBLL = new SalesReimburseBLL();
            }
            return this.m_SalesReimburseBLL;
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

    #region 页面初始化及事件处理

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this.Page, "方案报销审批");
            this.Page.Title = "方案报销审批";

            int formID = int.Parse(Request["ObjectId"]);
            this.ViewState["ObjectId"] = formID;
            FormDS.FormRow rowForm = this.SalesReimburseBLL.GetFormByID(formID)[0];
            FormDS.FormReimburseRow rowFormReimburse = this.SalesReimburseBLL.GetFormReimburseByID(formID)[0];
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
            MasterDataBLL masterBll = new MasterDataBLL();
            ERS.CustomerRow customer = masterBll.GetCustomerById(rowFormReimburse.CustomerID);
            this.CustomerNameCtl.Text = customer.CustomerName;
            this.CustomerTypeCtl.Text = masterBll.GetCustomerTypeById(customer.CustomerTypeID).CustomerTypeName;
            this.PaymentTypeCtl.Text = masterBll.GetPaymentTypeById(rowFormReimburse.PaymentTypeID).PaymentTypeName;
            if (!rowFormReimburse.IsRemarkNull()) {
                this.RemarkCtl.Text = rowFormReimburse.Remark;
            }
            if (!rowFormReimburse.IsAttachedFileNameNull())
                this.UCFileUpload.AttachmentFileName = rowFormReimburse.AttachedFileName;
            if (!rowFormReimburse.IsRealAttachedFileNameNull())
                this.UCFileUpload.RealAttachmentFileName = rowFormReimburse.RealAttachedFileName;

            this.odsInvoice.SelectParameters["FormReimburseID"].DefaultValue = rowFormReimburse.FormReimburseID.ToString();
            this.odsReimburseDetails.SelectParameters["FormReimburseID"].DefaultValue = rowFormReimburse.FormReimburseID.ToString();
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

            if (!rowFormReimburse.IsPaymentDateNull()) {
                this.txtPaymentDate.Text = rowFormReimburse.PaymentDate.ToString("yyyy-MM-dd");
                this.ucPaymentDate.SelectedDate = rowFormReimburse.PaymentDate.ToString("yyyy-MM-dd");
            }


            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            if (rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted && positionRightBLL.CheckPositionRight(position.PositionId, (int)SystemEnums.OperateEnum.Other + (int)SystemEnums.BusinessUseCase.FormReimburse)) {
                this.txtPaymentDate.Visible = false;
                this.ucPaymentDate.Visible = true;
                this.btnSavePaymentInfo.Visible = true;
            }

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                FormDS.FormRow rejectedForm = this.SalesReimburseBLL.GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/SalesForm/ReimburseMoneyApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
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

    #endregion

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
                //如果是票扣的话，如果拒绝的话要unlock PKRecord记录
                FormDS.FormReimburseRow row = this.SalesReimburseBLL.GetFormReimburseByID(this.cwfAppCheck.FormID)[0];
                if (!this.cwfAppCheck.GetApproveOrReject() && row.PaymentTypeID == (int)SystemEnums.PaymentType.PiaoKou) {
                    new PKRecordTableAdapter().UnLockPKRecord(row.FormReimburseID);
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

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void EditBtn_Click(object sender, EventArgs e) {
        this.Response.Redirect("~/SalesForm/ReimburseMoneyApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    #region 数据绑定及事件

    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormReimburseInvoiceRow row = (FormDS.FormReimburseInvoiceRow)drvDetail.Row;
                InvoiceFeeTotal = decimal.Round((InvoiceFeeTotal + row.InvoiceAmount), 2);
                string SystemInfo = row.IsSystemInfoNull() ? "" : row.SystemInfo;
                CommonUtility.GenerateRepeatControl(e.Row.Cells[3].Controls, SystemInfo);
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer) {
            Label sumlbl = new Label();
            sumlbl.Text = "合计";
            e.Row.Cells[0].Controls.Add(sumlbl);
            e.Row.Cells[0].CssClass = "RedTextAlignCenter";
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[0].Width = new Unit("220px");

            Label totallbl = new Label();
            totallbl.Text = InvoiceFeeTotal.ToString("N");
            e.Row.Cells[1].Controls.Add(totallbl);
            e.Row.Cells[1].CssClass = "RedTextAlignCenter";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[1].Width = new Unit("150px");
        }
    }

    protected void gvReimburseDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormReimburseDetailRow row = (FormDS.FormReimburseDetailRow)drvDetail.Row;
                ApplyFeeTotal = decimal.Round((ApplyFeeTotal + row.AccruedAmount), 2);
                RemainFeeTotal = decimal.Round((RemainFeeTotal + row.RemainAmount), 2);
                ReimburseFeeTotal = decimal.Round((ReimburseFeeTotal + row.Amount), 2);
                PrePaidFeeTaltal = decimal.Round(PrePaidFeeTaltal + (row.IsPrePaidAmountNull()?0:row.PrePaidAmount), 2);
                TaxFeeTotal = decimal.Round(TaxFeeTotal + (row.IsTaxAmountNull()?0:row.TaxAmount), 2);
                HyperLink lblApplyFormNo = (HyperLink)e.Row.FindControl("lblApplyFormNo");
                FormDS.FormRow form = this.SalesReimburseBLL.GetFormByID(row.FormApplyID)[0];
                switch (form.PageType) {
                    case (int)SystemEnums.PageType.PromotionApply:
                        lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/SalesForm/SalesPromotionApproval.aspx?ShowDialog=1&ObjectId=" + row.FormApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                        break;
                    case (int)SystemEnums.PageType.GeneralApply:
                        lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/SalesForm/SalesGeneralApproval.aspx?ShowDialog=1&ObjectId=" + row.FormApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                        break;
                    case (int)SystemEnums.PageType.RebateApply:
                        lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/SalesForm/SalesRebateApproval.aspx?ShowDialog=1&ObjectId=" + row.FormApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                        break;
                    case (int)SystemEnums.PageType.GeneralApplyExecute:
                        lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/SalesForm/SalesGeneralExecution.aspx?ShowDialog=1&ObjectId=" + row.FormApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                        break;
                    case (int)SystemEnums.PageType.PromotionApplyExecute:
                        lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/SalesForm/SalesPromotionExecution.aspx?ShowDialog=1&ObjectId=" + row.FormApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                        break;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer) {
            Label sumlbl = new Label();
            sumlbl.Text = "合计";
            e.Row.Cells[6].Controls.Add(sumlbl);
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].CssClass = "RedTextAlignCenter";
            e.Row.Cells[6].Width = new Unit("200px");

            Label applbl = new Label();
            applbl.Text = ApplyFeeTotal.ToString("N");
            e.Row.Cells[7].Controls.Add(applbl);
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[7].CssClass = "RedTextAlignCenter";
            e.Row.Cells[7].Width = new Unit("70px");

            Label Remainlbl = new Label();
            Remainlbl.Text = RemainFeeTotal.ToString("N");
            e.Row.Cells[8].Controls.Add(Remainlbl);
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[8].CssClass = "RedTextAlignCenter";
            e.Row.Cells[8].Width = new Unit("70px");

            Label lblPrePaidTotal= new Label();
            lblPrePaidTotal.Text = PrePaidFeeTaltal.ToString("N");
            lblPrePaidTotal.ID = "lblPrePaidTotal";
            e.Row.Cells[9].Controls.Add(lblPrePaidTotal);
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[9].CssClass = "RedTextAlignCenter";
            e.Row.Cells[9].Width = new Unit("70px");

            Label totallbl = new Label();
            totallbl.Text = ReimburseFeeTotal.ToString("N");
            totallbl.ID = "totallbl";
            e.Row.Cells[10].Controls.Add(totallbl);
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[10].CssClass = "RedTextAlignCenter";
            e.Row.Cells[10].Width = new Unit("70px");

            Label lblTaxTotal = new Label();
            lblTaxTotal.Text = TaxFeeTotal.ToString("N");
            lblTaxTotal.ID = "lblTaxTotal";
            e.Row.Cells[11].Controls.Add(lblTaxTotal);
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[11].CssClass = "RedTextAlignCenter";
            e.Row.Cells[11].Width = new Unit("70px");
        }
    }

    public string GetPaymentTypeNameByID(object PaymentTypeID) {
        if (PaymentTypeID.ToString() != string.Empty) {
            int id = Convert.ToInt32(PaymentTypeID);
            return new MasterDataBLL().GetPaymentTypeById(id).PaymentTypeName;
        } else {
            return null;
        }
    }

    public string GetShopNameByID(object ShopID) {
        if (ShopID.ToString() != string.Empty) {
            int id = Convert.ToInt32(ShopID);
            return new MasterDataBLL().GetShopByID(id).ShopName;
        } else {
            return null;
        }
    }

    public string GetSKUNameByID(object skuID) {
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
            ERS.ExpenseItemRow row = new MasterDataBLL().GetExpenseItemByID(id);
            return row.AccountingCode + "--" + row.ExpenseItemName;
        } else {
            return null;
        }
    }

    #endregion

    protected void btnSavePaymentInfo_Click(object sender, EventArgs e) {
        DateTime PaymentDate = DateTime.MinValue;
        decimal PaymentAmount = decimal.Zero;
        if (!string.IsNullOrEmpty(ucPaymentDate.SelectedDate)) {
            DateTime.TryParse(ucPaymentDate.SelectedDate, out PaymentDate);
        }
        if (PaymentDate == DateTime.MinValue) {
            PageUtility.ShowModelDlg(this, "支付日期填写不正确！");
            return;
        }
        try {
            SalesReimburseBLL.UpdateFormReimburseForRealPaymentInfo(int.Parse(this.ViewState["ObjectId"].ToString()), PaymentDate);
        } catch (Exception) {
            PageUtility.ShowModelDlg(this, e.ToString());
            throw;
        }
        if (this.Request["Source"] != null) {
            this.Response.Redirect(this.Request["Source"].ToString());
        } else {
            this.Response.Redirect("~/Home.aspx");
        }
    }
}
