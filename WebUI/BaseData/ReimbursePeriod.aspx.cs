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

public partial class BaseData_ReimbursePeriod : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "个人报销费用期间维护");
            this.Page.Title = "个人报销费用期间维护";
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ReimbursePeriod, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            if (!positionRightBLL.CheckPositionRight(position.PositionId, opManageId)) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }

    #region ReimbursePeriod event

    protected void odsReimbursePeriod_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        UserControls_YearAndMonthUserControl ucNewPeriod = (UserControls_YearAndMonthUserControl)this.fvReimbursePeriod.FindControl("ucNewPeriod");
        string period = ((TextBox)(ucNewPeriod.FindControl("txtDate"))).Text.Trim();
        if (period == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入费用期间!");
            e.Cancel = true;
            return;
        } else {
            DateTime yearAndMonth = DateTime.Parse(period.Substring(0, 4) + "-" + period.Substring(4, 2) + "-01");
            e.InputParameters["ReimbursePeriod"] = yearAndMonth;
        }
    }

    #endregion


}