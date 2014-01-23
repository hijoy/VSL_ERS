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

public partial class SaleForm_ReimburseMoneyApply : BasePage {

    decimal ApplyFeeTotal = 0;
    decimal RemainFeeTotal = 0;
    decimal ReimburseFeeTotal = 0;
    decimal InvoiceFeeTotal = 0;
    decimal PrePaidTotal = 0;
    decimal AmountTotal = 0;
    decimal TaxTotal = 0;

    private SalesReimburseBLL m_SalesReimburseBLL;
    protected SalesReimburseBLL SalesReimburseBLL {
        get {
            if (this.m_SalesReimburseBLL == null) {
                this.m_SalesReimburseBLL = new SalesReimburseBLL();
            }
            return this.m_SalesReimburseBLL;
        }
    }

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
            PageUtility.SetContentTitle(this.Page, "方案报销");
            this.Page.Title = "方案报销";
            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow rowUserPosition = (AuthorizationDS.PositionRow)Session["Position"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = rowUserPosition.PositionId;
            this.ViewState["FlowTemplate"] = new AuthorizationBLL().GetFlowTemplate((int)SystemEnums.BusinessUseCase.FormReimburse, stuffUser.StuffUserId);

            this.StuffNameCtl.Text = stuffUser.StuffName;
            this.PositionNameCtl.Text = rowUserPosition.PositionName;
            this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowUserPosition.OrganizationUnitId).OrganizationUnitName;
            this.ViewState["DepartmentID"] = rowUserPosition.OrganizationUnitId;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();
            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());
            }
            FormReimburseDetailTableAdapter taDetail = new FormReimburseDetailTableAdapter();

            //如果是草稿进行赋值
            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                FormDS.FormReimburseRow rowFormReimbuese = this.SalesReimburseBLL.GetFormReimburseByID(int.Parse(this.ViewState["ObjectId"].ToString()))[0];
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
                this.ViewState["PaymentTypeID"] = rowFormReimbuese.PaymentTypeID;
                this.ViewState["CustomerID"] = rowFormReimbuese.CustomerID;
                this.ViewState["FormApplyIds"] = rowFormReimbuese.FormApplyIds;
                this.ViewState["FormApplyNos"] = rowFormReimbuese.FormApplyNos;

                if (!rowFormReimbuese.IsRemarkNull()) {
                    this.RemarkCtl.Text = rowFormReimbuese.Remark;
                }
                if (!rowFormReimbuese.IsAttachedFileNameNull()) {
                    this.UCFileUpload.AttachmentFileName = rowFormReimbuese.AttachedFileName;
                }
                if (!rowFormReimbuese.IsRealAttachedFileNameNull()) {
                    this.UCFileUpload.RealAttachmentFileName = rowFormReimbuese.RealAttachedFileName;
                }
                new FormReimburseInvoiceTableAdapter().FillByFormReimburseID(this.InnerDS.FormReimburseInvoice, rowFormReimbuese.FormReimburseID);
                // 填写明细表，如果是票扣的话那么写入方法不一样
                if (int.Parse(this.ViewState["PaymentTypeID"].ToString()) == (int)SystemEnums.PaymentType.PiaoKou) {
                    taDetail.FillFromPKRecordByApplyNos(this.InnerDS.FormReimburseDetail, this.ViewState["FormApplyNos"].ToString());
                } else {
                    taDetail.FillCurrentDataByFormReimburseID(this.InnerDS.FormReimburseDetail, rowFormReimbuese.FormReimburseID);
                }

            } else {
                this.DeleteBtn.Visible = false;
                if (Request["PaymentTypeID"] != null) {
                    this.ViewState["PaymentTypeID"] = Request["PaymentTypeID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到支付方式，请重新填写";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["CustomerID"] != null) {
                    this.ViewState["CustomerID"] = Request["CustomerID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到客户，请重新填写";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["FormApplyIds"] != null) {
                    this.ViewState["FormApplyIds"] = Request["FormApplyIds"];
                } else {
                    this.Session["ErrorInfor"] = "没有找到申请单，请重新填写";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["FormApplyNos"] != null) {
                    this.ViewState["FormApplyNos"] = Request["FormApplyNos"];
                } else {
                    this.Session["ErrorInfor"] = "没有找到申请单，请重新填写";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                // 填写明细表，如果是票扣的话那么写入方法不一样
                if (int.Parse(this.ViewState["PaymentTypeID"].ToString()) == (int)SystemEnums.PaymentType.PiaoKou) {
                    taDetail.FillFromPKRecordByApplyNos(this.InnerDS.FormReimburseDetail, this.ViewState["FormApplyNos"].ToString());
                } else {
                    taDetail.FillByFormApplyIds(this.InnerDS.FormReimburseDetail, this.ViewState["FormApplyIds"].ToString());
                }

            }
            ERS.CustomerRow customer = new MasterDataBLL().GetCustomerById(int.Parse(this.ViewState["CustomerID"].ToString()));
            this.CustomerNameCtl.Text = customer.CustomerName;
            this.PaymentTypeDDL.SelectedValue = this.ViewState["PaymentTypeID"].ToString();

            if (Session["ProxyStuffUserId"] != null) {
                this.SubmitBtn.Visible = false;
            }

        }

    }


    protected override void OnLoadComplete(EventArgs e) {
        base.OnLoadComplete(e);
        decimal total = 0;
        if (this.gvReimburseDetails.Rows.Count > 0) {
            foreach (GridViewRow item in gvReimburseDetails.Rows) {
                if (item.RowType == DataControlRowType.DataRow) {
                    TextBox txtAmount = (TextBox)item.FindControl("txtAmount");
                    total += decimal.Parse(txtAmount.Text == string.Empty ? "0" : txtAmount.Text.ToString());
                }
            }
        }
        Label lblTotal = (Label)gvReimburseDetails.FooterRow.FindControl("totallblBeforeTax");
        lblTotal.Text = total.ToString("N");
    }

    #endregion

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
        this.SalesReimburseBLL.DeleteFormReimburse(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        SaveFormReimburse(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        if (this.gvReimburseDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入明细项");
            return;
        }
        SaveFormReimburse(SystemEnums.FormStatus.Awaiting);
    }

    protected void SaveFormReimburse(SystemEnums.FormStatus StatusID) {
        this.SalesReimburseBLL.FormDataSet = this.InnerDS;
        if (FillDetail()) {
            if (StatusID == SystemEnums.FormStatus.Awaiting) {
                if (ReimburseFeeTotal <= 0) {
                    PageUtility.ShowModelDlg(this.Page, "报销总金额不能为零");
                    return;
                }
                //去掉此限制
                //if (this.PaymentTypeDDL.SelectedValue != ((int)SystemEnums.PaymentType.PiaoKou).ToString()) {
                //    if (decimal.Parse(this.ViewState["InvoiceFeeTotal"].ToString()) < decimal.Parse(this.ViewState["ReimburseFeeTotal"].ToString())) {
                //        PageUtility.ShowModelDlg(this.Page, "发票金额不得小于报销金额");
                //        return;
                //    }
                //}
            }
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
            int CustomerID = int.Parse(this.ViewState["CustomerID"].ToString());
            int PaymentTypeID = int.Parse(this.PaymentTypeDDL.SelectedValue);
            string AttachedFileName = this.UCFileUpload.AttachmentFileName;
            string RealAttachedFileName = this.UCFileUpload.RealAttachmentFileName;
            string Remark = this.RemarkCtl.Text;

            try {
                if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                    this.SalesReimburseBLL.AddFormReimburseMoney(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.ReimburseApply, StatusID,
                        CustomerID, PaymentTypeID, AttachedFileName, RealAttachedFileName, Remark, this.ViewState["FormApplyIds"].ToString(), this.ViewState["FormApplyNos"].ToString(), this.ViewState["FlowTemplate"].ToString());
                } else {
                    int FormID = (int)this.ViewState["ObjectId"];
                    this.SalesReimburseBLL.UpdateFormReimburseMoney(FormID, StatusID, SystemEnums.FormType.ReimburseApply, AttachedFileName, RealAttachedFileName, Remark, this.ViewState["FlowTemplate"].ToString());
                }
                this.Page.Response.Redirect("~/Home.aspx");
            } catch (Exception ex) {
                PageUtility.DealWithException(this.Page, ex);
            }
        }
    }

    private bool FillDetail() {
        bool isValid = true;
        foreach (GridViewRow row in this.gvReimburseDetails.Rows) {
            if (row.RowType == DataControlRowType.DataRow) {
                //Label lblFormApplyExpenseDetailID = (Label)row.FindControl("lblFormApplyExpenseDetailID");
                //DataRow[] drArray = this.InnerDS.FormReimburseDetail.Select("FormApplyExpenseDetailID =" + lblFormApplyExpenseDetailID.Text);
                //FormDS.FormReimburseDetailRow detailRow = (FormDS.FormReimburseDetailRow)drArray[0];
                FormDS.FormReimburseDetailRow detailRow = this.InnerDS.FormReimburseDetail[row.RowIndex];
                TextBox txtPrePaidAmount = (TextBox)row.FindControl("txtPrePaidAmount");
                TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
                TextBox txtTaxAmount = (TextBox)row.FindControl("txtTaxAmount");
                if (string.IsNullOrEmpty(txtPrePaidAmount.Text.Trim())) {
                    txtPrePaidAmount.Text = "0";
                }
                if (string.IsNullOrEmpty(txtAmount.Text.Trim())) {
                    txtAmount.Text = "0";
                }
                if (string.IsNullOrEmpty(txtTaxAmount.Text.Trim())) {
                    txtTaxAmount.Text = "0";
                }
                decimal PrePaidAmount = decimal.Parse(txtPrePaidAmount.Text.Trim());
                decimal Amount = decimal.Parse(txtAmount.Text.Trim());
                decimal TaxAmount = decimal.Parse(txtTaxAmount.Text.Trim());
                if (PrePaidAmount < 0 || TaxAmount < 0 || Amount<0) {
                    PageUtility.ShowModelDlg(this.Page, "不能录入负数");
                    isValid = false;
                    break;
                }
                decimal paidAmount = this.SalesReimburseBLL.GetPayedAmountByFormApplyExpenseDetailID(detailRow.FormApplyExpenseDetailID);
                detailRow.RemainAmount = detailRow.ApplyAmount - paidAmount;

                if (Amount + TaxAmount > detailRow.RemainAmount) {
                    PageUtility.ShowModelDlg(this.Page, "报销金额不能大于可报销金额");
                    isValid = false;
                    break;
                }
                detailRow.Amount = Amount;
                detailRow.PrePaidAmount = PrePaidAmount;
                detailRow.TaxAmount = TaxAmount;
                ReimburseFeeTotal = decimal.Round((ReimburseFeeTotal + Amount), 2);
            }
        }
        this.ViewState["ReimburseFeeTotal"] = ReimburseFeeTotal;
        return isValid;
    }

    #region 数据绑定及事件

    protected void odsInvoice_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        SalesReimburseBLL bll = (SalesReimburseBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

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

        this.ViewState["InvoiceFeeTotal"] = InvoiceFeeTotal;

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
            e.Row.Cells[1].Width = new Unit("80px");
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
                PrePaidTotal = decimal.Round((PrePaidTotal + row.PrePaidAmount), 2);
                AmountTotal = decimal.Round((AmountTotal + row.Amount), 2);
                TaxTotal = decimal.Round((TaxTotal + row.TaxAmount), 2);
                //如果是票扣的话，那么不能编辑
                TextBox txtPrePaidAmount = (TextBox)e.Row.FindControl("txtPrePaidAmount");
                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                TextBox txtTaxAmount = (TextBox)e.Row.FindControl("txtTaxAmount");

                if (int.Parse(this.ViewState["PaymentTypeID"].ToString()) == (int)SystemEnums.PaymentType.PiaoKou) {
                    txtAmount.ReadOnly = true;
                    txtTaxAmount.ReadOnly = true;
                    txtPrePaidAmount.ReadOnly = true;
                } else {
                    txtPrePaidAmount.Attributes.Add("onBlur", "PlusTotal('totallblPrePaid',this);");
                    txtPrePaidAmount.Attributes.Add("onFocus", "MinusTotal('totallblPrePaid',this)");

                    txtAmount.Attributes.Add("onBlur", "PlusTotal('totallblBeforeTax',this);");
                    txtAmount.Attributes.Add("onFocus", "MinusTotal('totallblBeforeTax',this)");

                    txtTaxAmount.Attributes.Add("onBlur", "PlusTotal('totallblTax',this);");
                    txtTaxAmount.Attributes.Add("onFocus", "MinusTotal('totallblTax',this)");
                }

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

            Label applbl = (Label)e.Row.FindControl("applbl");
            applbl.Text = ApplyFeeTotal.ToString("N");

            Label Remainlbl = (Label)e.Row.FindControl("Remainlbl");
            Remainlbl.Text = RemainFeeTotal.ToString("N");

            Label totallblPrePaid = (Label)e.Row.FindControl("totallblPrePaid");
            totallblPrePaid.Text = PrePaidTotal.ToString("N");

            Label totallblBeforeTax = (Label)e.Row.FindControl("totallblBeforeTax");
            totallblBeforeTax.Text = AmountTotal.ToString("N");

            Label totallblTax = (Label)e.Row.FindControl("totallblTax");
            totallblTax.Text = TaxTotal.ToString("N");
        }
    }

    protected void odsReimburseDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        SalesReimburseBLL bll = (SalesReimburseBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
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
            //return new MasterDataBLL().GetExpenseItemByID(id).ExpenseItemName;
            ERS.ExpenseItemRow row = new MasterDataBLL().GetExpenseItemByID(id);
            return row.AccountingCode + "--" + row.ExpenseItemName;
        } else {
            return null;
        }
    }

    #endregion
}
