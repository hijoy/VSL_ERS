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

public partial class SalesForm_SalesApplySelect : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this.Page, "方案申请");
            this.Page.Title = "方案申请";
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            this.odsCustomer.SelectParameters["PositionID"].DefaultValue = position.PositionId.ToString();
        }
    }

    protected void SubCategoryDDL_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.SubCategoryDDL.SelectedValue != "0") {
            ERS.ExpenseSubCategoryRow subCategory = new MasterDataBLL().GetExpenseSubCategoryById(int.Parse(this.SubCategoryDDL.SelectedValue));
            if (subCategory.PageType == (int)SystemEnums.PageType.PromotionApply) {
                this.PromotionPriceTypeTD.Visible = true;
            } else {
                this.PromotionPriceTypeTD.Visible = false;
            }
        } else {
            this.PromotionPriceTypeTD.Visible = false;
        }
    }

    protected void NextButton_Click(object sender, EventArgs e) {
        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();
        if (startPeriod == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择起始费用期间!");
            return;
        }
        if (endPeriod == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择截止费用期间!");
            return;
        }
        DateTime dtstartPeriod = DateTime.Parse(startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01");
        DateTime dtendPeriod = DateTime.Parse(endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01");
        if (dtstartPeriod > dtendPeriod) {
            PageUtility.ShowModelDlg(this, "起始费用期间大于截止费用期间！");
            return;
        }
        MasterDataBLL bll = new MasterDataBLL();
        if (dtstartPeriod.AddMonths(-3).Year != dtendPeriod.AddMonths(-3).Year) {
            PageUtility.ShowModelDlg(this, "起始和截止费用期间必须在同一财年内!");
            return;
        }
        if (!bll.IsValidApplyYear(dtstartPeriod.AddMonths(-3).Year)) {
            PageUtility.ShowModelDlg(this, "不允许申请本财年项目，请联系财务部!");
            return;
        }

        if (this.CustomerDDL.SelectedValue == null || this.CustomerDDL.SelectedValue == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择客户！");
            return;
        }

        if (this.SubCategoryDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择费用小类！");
            return;
        }

        ERS.ExpenseSubCategoryRow subCategory = bll.GetExpenseSubCategoryById(int.Parse(this.SubCategoryDDL.SelectedValue));
        //检查如果是返利，只能填写一个月的
        if (subCategory.PageType == (int)SystemEnums.PageType.RebateApply) {
            if (dtstartPeriod.Year != dtendPeriod.Year || dtstartPeriod.Month != dtendPeriod.Month) {
                PageUtility.ShowModelDlg(this, "返利申请不能跨月！");
                return;
            }
            if (new SalesApplyBLL().GetRebateApplyCountByParameter(int.Parse(this.CustomerDDL.SelectedValue), dtstartPeriod.Year, dtstartPeriod.Month,int.Parse(this.SubCategoryDDL.SelectedValue)) > 0) {
                PageUtility.ShowModelDlg(this, "系统中已经存在该客户的返利申请，返利申请每月只能申请一次！");
                return;
            }
        }
        switch (subCategory.PageType) {
            case (int)SystemEnums.PageType.PromotionApply:
                this.Response.Redirect("~/SalesForm/SalesPromotionApply.aspx?CustomerID=" + this.CustomerDDL.SelectedValue + "&ExpenseSubCategoryID=" + this.SubCategoryDDL.SelectedValue + "&BeginPeriod=" + dtstartPeriod.ToShortDateString() + "&EndPeriod=" + dtendPeriod.ToShortDateString() + "&PromotionPriceType=" + this.PromotionPriceTypeDDL.SelectedValue);
                break;
            case (int)SystemEnums.PageType.GeneralApply:
                this.Response.Redirect("~/SalesForm/SalesGeneralApply.aspx?CustomerID=" + this.CustomerDDL.SelectedValue + "&ExpenseSubCategoryID=" + this.SubCategoryDDL.SelectedValue + "&BeginPeriod=" + dtstartPeriod.ToShortDateString() + "&EndPeriod=" + dtendPeriod.ToShortDateString() );
                break;
            case (int)SystemEnums.PageType.RebateApply:
                this.Response.Redirect("~/SalesForm/SalesRebateApply.aspx?CustomerID=" + this.CustomerDDL.SelectedValue + "&ExpenseSubCategoryID=" + this.SubCategoryDDL.SelectedValue + "&BeginPeriod=" + dtstartPeriod.ToShortDateString() + "&EndPeriod=" + dtendPeriod.ToShortDateString() );
                break;
        }
    }
}
