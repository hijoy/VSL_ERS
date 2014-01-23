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

public partial class SaleForm_ReimburseGoodsApply : BasePage {

    decimal ApplyFeeTotal = 0;
    decimal RemainFeeTotal = 0;
    decimal ReimburseFeeTotal = 0;
    decimal SKUFeeTotal = 0;

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
                taDetail.FillCurrentDataByFormReimburseID(this.InnerDS.FormReimburseDetail, rowFormReimbuese.FormReimburseID);
                new FormReimburseSKUDetailTableAdapter().FillByFormReimburseID(this.InnerDS.FormReimburseSKUDetail, rowFormReimbuese.FormReimburseID);
            } else {
                this.DeleteBtn.Visible = false;

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
                // 填写明细表
                taDetail.FillByFormApplyIds(this.InnerDS.FormReimburseDetail, this.ViewState["FormApplyIds"].ToString());

            }
            ERS.CustomerRow customer = new MasterDataBLL().GetCustomerById(int.Parse(this.ViewState["CustomerID"].ToString()));
            this.CustomerNameCtl.Text = customer.CustomerName;

            if (Session["ProxyStuffUserId"] != null) {
                this.SubmitBtn.Visible = false;
            }

        }
        TextBox newQuantityCtl = (TextBox)this.fvSKUDetails.FindControl("newQuantityCtl");
        newQuantityCtl.Attributes.Add("onchange", "ParameterChanged()");

    }

    protected override void OnLoadComplete(EventArgs e) {
        base.OnLoadComplete(e);
        decimal total = 0;
        if (this.gvReimburseDetails.Rows.Count > 0) {
            foreach (GridViewRow item in gvReimburseDetails.Rows) {
                if (item.RowType == DataControlRowType.DataRow) {
                    TextBox textBox = (TextBox)item.FindControl("txtAmount");
                    total += decimal.Parse(textBox.Text == string.Empty ? "0" : textBox.Text.ToString());
                }
            }
        }
        Label lblTotal = (Label)gvReimburseDetails.FooterRow.FindControl("totallbl");
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
        if (this.gvSKUDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入领用产品信息");
            return;
        }
        SaveFormReimburse(SystemEnums.FormStatus.Awaiting);

    }

    protected void SaveFormReimburse(SystemEnums.FormStatus StatusID) {
        this.SalesReimburseBLL.FormDataSet = this.InnerDS;
        if (FillDetail()) {
            if (StatusID == SystemEnums.FormStatus.Awaiting) {
                if (decimal.Parse(this.ViewState["ReimburseFeeTotal"].ToString()) < decimal.Parse(this.ViewState["SKUFeeTotal"].ToString())) {
                    PageUtility.ShowModelDlg(this.Page, "领用产品总金额不能大于报销金额");
                    return;
                }
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
                    this.SalesReimburseBLL.AddFormReimburseGoods(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.ReimburseApply, StatusID,
                        CustomerID, PaymentTypeID, AttachedFileName, RealAttachedFileName, Remark, this.ViewState["FormApplyIds"].ToString(), this.ViewState["FormApplyNos"].ToString(), this.ViewState["FlowTemplate"].ToString());
                } else {
                    int FormID = (int)this.ViewState["ObjectId"];
                    this.SalesReimburseBLL.UpdateFormReimburseGoods(FormID, StatusID, SystemEnums.FormType.ReimburseApply, AttachedFileName, RealAttachedFileName, Remark, this.ViewState["FlowTemplate"].ToString());
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
                FormDS.FormReimburseDetailRow detailRow = this.InnerDS.FormReimburseDetail[row.RowIndex];
                TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
                if (string.IsNullOrEmpty(txtAmount.Text.Trim())) {
                    txtAmount.Text = "0";
                }
                decimal amount = decimal.Parse(txtAmount.Text.Trim());
                if (amount < 0) {
                    PageUtility.ShowModelDlg(this.Page, "不能录入负数");
                    isValid = false;
                    break;
                }
                if (amount > detailRow.RemainAmount) {
                    PageUtility.ShowModelDlg(this.Page, "报销金额不能大于可报销金额");
                    isValid = false;
                    break;
                }
                detailRow.Amount = amount;
                ReimburseFeeTotal = decimal.Round((ReimburseFeeTotal + amount), 2);
            }
        }
        if (ReimburseFeeTotal <= 0) {
            PageUtility.ShowModelDlg(this.Page, "报销总金额不能为零");
            isValid = false;
        }
        this.ViewState["ReimburseFeeTotal"] = ReimburseFeeTotal;
        return isValid;
    }

    #region 数据绑定及事件

    protected void odsSKUDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        DropDownList NewSKUDDL = (DropDownList)this.fvSKUDetails.FindControl("NewSKUDDL");
        if (NewSKUDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this.Page, "请选择产品");
            e.Cancel = true;
            return;
        }
        TextBox newQuantityCtl = (TextBox)this.fvSKUDetails.FindControl("newQuantityCtl");
        TextBox newPackageQuantityCtl = (TextBox)this.fvSKUDetails.FindControl("newPackageQuantityCtl");
        TextBox newUnitPriceCtl = (TextBox)this.fvSKUDetails.FindControl("newUnitPriceCtl");
        ERS.SKURow SKU = new MasterDataBLL().GetSKUById(int.Parse(NewSKUDDL.SelectedValue));
        if (NewSKUDDL.SelectedValue != "0") {
            newPackageQuantityCtl.Text = SKU.PackageQuantity.ToString();
            newUnitPriceCtl.Text = new MasterDataBLL().GetSKUPriceByCustomerID(SKU.SKUID, int.Parse(this.ViewState["CustomerID"].ToString())).ToString();
        }
        e.InputParameters["UnitPrice"] = decimal.Parse(newUnitPriceCtl.Text);
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormReimburseID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }
    }

    protected void odsSKUDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        SalesReimburseBLL bll = (SalesReimburseBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void gvSKUDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormReimburseSKUDetailRow row = (FormDS.FormReimburseSKUDetailRow)drvDetail.Row;
                SKUFeeTotal = decimal.Round((SKUFeeTotal + row.Amount), 2);
            }
        }

        this.ViewState["SKUFeeTotal"] = SKUFeeTotal;

        if (e.Row.RowType == DataControlRowType.Footer) {

            Label totalskulbl = (Label)e.Row.FindControl("totalskulbl");
            totalskulbl.Text = SKUFeeTotal.ToString("N");
        }

    }

    protected void NewSKUDDL_OnSelectedIndexChanged(object sender, EventArgs e) {
        DropDownList NewSKUDDL = (DropDownList)this.fvSKUDetails.FindControl("NewSKUDDL");
        TextBox newPackageQuantityCtl = (TextBox)this.fvSKUDetails.FindControl("newPackageQuantityCtl");
        TextBox newUnitPriceCtl = (TextBox)this.fvSKUDetails.FindControl("newUnitPriceCtl");
        TextBox newQuantityCtl = (TextBox)this.fvSKUDetails.FindControl("newQuantityCtl");
        TextBox newTotalCtl = (TextBox)this.fvSKUDetails.FindControl("newTotalCtl");
        if (NewSKUDDL.SelectedValue == "0") {
            newPackageQuantityCtl.Text = "";
            newUnitPriceCtl.Text = "";
            newTotalCtl.Text = "";
        } else {
            ERS.SKURow SKU = new MasterDataBLL().GetSKUById(int.Parse(NewSKUDDL.SelectedValue));
            newPackageQuantityCtl.Text = SKU.PackageQuantity.ToString();
            newUnitPriceCtl.Text = new MasterDataBLL().GetSKUPriceByCustomerID(SKU.SKUID, int.Parse(this.ViewState["CustomerID"].ToString())).ToString();
            if (newQuantityCtl.Text != string.Empty) {
                newTotalCtl.Text = (decimal.Parse(newUnitPriceCtl.Text) * decimal.Parse(newQuantityCtl.Text)).ToString();
            }

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

                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                txtAmount.Attributes.Add("onBlur", "PlusTotal(this);");
                txtAmount.Attributes.Add("onFocus", "MinusTotal(this)");

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

            Label totallbl = (Label)e.Row.FindControl("totallbl");
            totallbl.Text = ReimburseFeeTotal.ToString("N");

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
            ERS.ExpenseItemRow row = new MasterDataBLL().GetExpenseItemByID(id);
            return row.AccountingCode + "--" + row.ExpenseItemName;
        } else {
            return null;
        }
    }

    public string GetSKUInfoByID(object skuID) {
        if (skuID.ToString() != string.Empty) {
            int id = Convert.ToInt32(skuID);
            ERS.SKURow sku = new MasterDataBLL().GetSKUById(id);
            return sku.SKUNo + '-' + sku.SKUName + "-" + sku.Spec;
        } else {
            return null;
        }
    }

    #endregion


}
