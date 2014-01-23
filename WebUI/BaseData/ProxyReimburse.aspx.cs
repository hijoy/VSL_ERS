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

public partial class ProxyReimburse : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "方案报销代理设置");
            this.Page.Title = "方案报销代理设置";
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ProxyReimburse, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            if (!new PositionRightBLL().CheckPositionRight(position.PositionId, opManageId)) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }


    #region ProxyReimburse event

    protected void lbtnSearch_Click(object sender, EventArgs e) {
        string searchStr = "1=1";
        if (this.UCUser.StaffID != string.Empty) {
            searchStr += " And UserID=" + this.UCUser.StaffID;
        }
        if (this.UCProxyUser.StaffID != string.Empty) {
            searchStr += " And ProxyUserID=" + this.UCProxyUser.StaffID;
        }
        this.odsProxyReimburse.SelectParameters["queryExpression"].DefaultValue = searchStr;
        this.gvProxyReimburse.DataBind();
        this.upProxyReimburse.Update();
    }

    public string GetUserNameByID(object ID) {
        return new AuthorizationBLL().GetStuffUserById((int)ID).StuffName;
    }

    protected void odsProxyReimburse_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        UserControls_UCDateInput NewUCEndDate = (UserControls_UCDateInput)this.fvProxyReimburse.FindControl("NewUCEndDate");
        DateTime enddate = DateTime.Parse(NewUCEndDate.SelectedDate);
        e.InputParameters["EndDate"] = enddate;
    }

    #endregion

}
