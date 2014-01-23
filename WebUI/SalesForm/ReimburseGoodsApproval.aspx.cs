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

public partial class SaleForm_ReimburseGoodsApproval : BasePage {

    decimal ApplyFeeTotal = 0;
    decimal RemainFeeTotal = 0;
    decimal ReimburseFeeTotal = 0;
    decimal SKUFeeTotal = 0;
    decimal DeliveryQuantityTotal = 0;
    decimal DeliveryFeeTotal = 0;

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
            //如果单据没有审批完成的话，是不能录入或者查看发货信息的
            if (rowForm.StatusID != (int)SystemEnums.FormStatus.ApproveCompleted) {
                this.gvSKUDetails.Columns[6].Visible = false;
                this.gvSKUDetails.Columns[5].HeaderStyle.Width = 400;
                this.DeliveryDIV.Visible = false;
                this.upDelivery.Visible = false;
            }
            if (this.gvSKUDetails.SelectedIndex < 0) {
                this.fvDelievery.Visible = false;
            }
            //如果没有修改权限的话那么不能新增和删除
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.DeliveryInfo, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            this.HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);

            //流程ID
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

            this.odsSKUDetails.SelectParameters["FormReimburseID"].DefaultValue = rowFormReimburse.FormReimburseID.ToString();
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

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                FormDS.FormRow rejectedForm = this.SalesReimburseBLL.GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/OtherForm/ReimburseGoodsApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }

            //如果是弹出,按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.upButton.Visible = false;
                    this.gvSKUDetails.Columns[6].Visible = false;
                    this.gvSKUDetails.Columns[5].HeaderStyle.Width = 400;
                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                }
            }

            //发货完成按钮权限
            opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.DeliveryComplete, SystemEnums.OperateEnum.Other);
            position = (AuthorizationDS.PositionRow)this.Session["Position"];
            if (positionRightBLL.CheckPositionRight(position.PositionId, opManageId) && (rowFormReimburse.IsIsDeliveryCompleteNull() || rowFormReimburse.IsDeliveryComplete == false)) {
                this.btnDeliveryComplete.Visible = true;
            } else {
                this.btnDeliveryComplete.Visible = false;
                //this.gvDelivery.Visible = false;
                //this.gvSKUDetails.Columns[6].Visible=false;
                //this.gvSKUDetails.Columns[5].ItemStyle.Width = 400;
            }
        }
        this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
        this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
        this.cwfAppCheck.IsView = (bool)this.ViewState["IsView"];
    }

    protected bool HasManageRight {
        get {
            return (bool)this.ViewState["HasManageRight"];
        }
        set {
            this.ViewState["HasManageRight"] = value;
        }
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
        this.Response.Redirect("~/SalesForm/ReimburseGoodsApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {

        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void btnDeliveryComplete_Click(object sender, EventArgs e) {
        SalesReimburseBLL.DeliveryComplete((int)this.ViewState["ObjectId"], ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId);
        if (this.Request["Source"] != null) {
            this.Response.Redirect(this.Request["Source"].ToString());
        } else {
            this.Response.Redirect("~/Home.aspx");
        }
    }

    #region 数据绑定及事件

    protected void gvSKUDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            DataRowView drvDetail = (DataRowView)e.Row.DataItem;
            FormDS.FormReimburseSKUDetailRow row = (FormDS.FormReimburseSKUDetailRow)drvDetail.Row;
            SKUFeeTotal = decimal.Round((SKUFeeTotal + row.Amount), 2);
        }
        if (e.Row.RowType == DataControlRowType.Footer) {
            Label totalskulbl = (Label)e.Row.FindControl("totalskulbl");
            totalskulbl.Text = SKUFeeTotal.ToString("N");
        }
    }

    protected void gvReimburseDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormReimburseDetailRow row = (FormDS.FormReimburseDetailRow)drvDetail.Row;
                ApplyFeeTotal = decimal.Round((ApplyFeeTotal + row.ApplyAmount), 2);
                RemainFeeTotal = decimal.Round((RemainFeeTotal + row.RemainAmount), 2);
                ReimburseFeeTotal = decimal.Round((ReimburseFeeTotal + row.Amount), 2);
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
            e.Row.Cells[6].CssClass = "RedTextAlignCenter";
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].Width = new Unit("200px");

            Label applbl = new Label();
            applbl.Text = ApplyFeeTotal.ToString("N");
            e.Row.Cells[7].Controls.Add(applbl);
            e.Row.Cells[7].CssClass = "RedTextAlignCenter";
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[7].Width = new Unit("100px");

            Label Remainlbl = new Label();
            Remainlbl.Text = RemainFeeTotal.ToString("N");
            e.Row.Cells[8].Controls.Add(Remainlbl);
            e.Row.Cells[8].CssClass = "RedTextAlignCenter";
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[8].Width = new Unit("100px");

            Label totallbl = new Label();
            totallbl.Text = ReimburseFeeTotal.ToString("N");
            totallbl.ID = "totallbl";
            e.Row.Cells[9].Controls.Add(totallbl);
            e.Row.Cells[9].CssClass = "RedTextAlignCenter";
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[9].Width = new Unit("120px");
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

    public string GetSKUInfoByID(object skuID) {
        if (skuID.ToString() != string.Empty) {
            int id = Convert.ToInt32(skuID);
            ERS.SKURow sku = new MasterDataBLL().GetSKUById(id);
            return sku.SKUNo + '-' + sku.SKUName + "-" + sku.Spec;
        } else {
            return null;
        }
    }

    protected void gvSKUDetails_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.gvSKUDetails.SelectedIndex >= 0) {
            this.gvDelivery.Visible = true;
            this.odsDelivery.SelectParameters["FormReimburseSKUDetailID"].DefaultValue = gvSKUDetails.SelectedValue.ToString();
            this.gvDelivery.DataBind();
            if (this.HasManageRight) {
                this.fvDelievery.Visible = true;
                this.odsDelivery.InsertParameters["FormReimburseSKUDetailID"].DefaultValue = gvSKUDetails.SelectedValue.ToString();
            }
        } else {
            this.gvDelivery.Visible = false;
            this.fvDelievery.Visible = false;
        }
        this.upDelivery.Update();
    }

    protected void odsDelivery_inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["FormReimburseSKUDetailID"] = this.gvSKUDetails.SelectedValue;
        decimal RemainQuantity = (decimal)new FormReimburseDeliveryTableAdapter().QueryRemainQuantityByReimburseSKUID((int)this.gvSKUDetails.SelectedValue);
        TextBox txtDeliveryQuantity = (TextBox)this.fvDelievery.FindControl("newDeliveryQuantityCtl");
        decimal DeliveryQuantity = string.IsNullOrEmpty(txtDeliveryQuantity.Text) ? 0 : decimal.Parse(txtDeliveryQuantity.Text);
        if (DeliveryQuantity > RemainQuantity) {
            PageUtility.ShowModelDlg(this, "发货数量不能超过物品领用数量！");
            e.Cancel = true;
        }
    }

    protected void gvDelivery_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            DataRowView drvDetail = (DataRowView)e.Row.DataItem;
            FormDS.FormReimburseDeliveryRow row = (FormDS.FormReimburseDeliveryRow)drvDetail.Row;
            DeliveryQuantityTotal = decimal.Round((DeliveryQuantityTotal + row.DeliveryQuantity), 2);
            DeliveryFeeTotal = decimal.Round((DeliveryFeeTotal + row.DeliveryAmount), 2);
        }
        if (e.Row.RowType == DataControlRowType.Footer) {
            Label lblTotalDeliveryQuantity = (Label)e.Row.FindControl("lblTotalDeliveryQuantity");
            Label lblTotalDeliveryAmount = (Label)e.Row.FindControl("lblTotalDeliveryAmount");
            lblTotalDeliveryQuantity.Text = DeliveryQuantityTotal.ToString("N");
            lblTotalDeliveryAmount.Text = DeliveryFeeTotal.ToString("N");
        }
    }

    #endregion

}
