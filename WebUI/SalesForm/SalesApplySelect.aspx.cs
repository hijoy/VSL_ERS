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
            PageUtility.SetContentTitle(this.Page, "��������");
            this.Page.Title = "��������";
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
            PageUtility.ShowModelDlg(this, "��ѡ����ʼ�����ڼ�!");
            return;
        }
        if (endPeriod == string.Empty) {
            PageUtility.ShowModelDlg(this, "��ѡ���ֹ�����ڼ�!");
            return;
        }
        DateTime dtstartPeriod = DateTime.Parse(startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01");
        DateTime dtendPeriod = DateTime.Parse(endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01");
        if (dtstartPeriod > dtendPeriod) {
            PageUtility.ShowModelDlg(this, "��ʼ�����ڼ���ڽ�ֹ�����ڼ䣡");
            return;
        }
        MasterDataBLL bll = new MasterDataBLL();
        if (dtstartPeriod.AddMonths(-3).Year != dtendPeriod.AddMonths(-3).Year) {
            PageUtility.ShowModelDlg(this, "��ʼ�ͽ�ֹ�����ڼ������ͬһ������!");
            return;
        }
        if (!bll.IsValidApplyYear(dtstartPeriod.AddMonths(-3).Year)) {
            PageUtility.ShowModelDlg(this, "���������뱾������Ŀ������ϵ����!");
            return;
        }

        if (this.CustomerDDL.SelectedValue == null || this.CustomerDDL.SelectedValue == string.Empty) {
            PageUtility.ShowModelDlg(this, "��ѡ��ͻ���");
            return;
        }

        if (this.SubCategoryDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "��ѡ�����С�࣡");
            return;
        }

        ERS.ExpenseSubCategoryRow subCategory = bll.GetExpenseSubCategoryById(int.Parse(this.SubCategoryDDL.SelectedValue));
        //�������Ƿ�����ֻ����дһ���µ�
        if (subCategory.PageType == (int)SystemEnums.PageType.RebateApply) {
            if (dtstartPeriod.Year != dtendPeriod.Year || dtstartPeriod.Month != dtendPeriod.Month) {
                PageUtility.ShowModelDlg(this, "�������벻�ܿ��£�");
                return;
            }
            if (new SalesApplyBLL().GetRebateApplyCountByParameter(int.Parse(this.CustomerDDL.SelectedValue), dtstartPeriod.Year, dtstartPeriod.Month,int.Parse(this.SubCategoryDDL.SelectedValue)) > 0) {
                PageUtility.ShowModelDlg(this, "ϵͳ���Ѿ����ڸÿͻ��ķ������룬��������ÿ��ֻ������һ�Σ�");
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
