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

public partial class BusinessProxySelect : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        this.BusinessProxyDS.SelectParameters["ProxyUserId"].DefaultValue = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId.ToString();
    }

    protected void SelfOperateBtn_Click(object sender, EventArgs e) {
        AuthorizationDS.StuffUserRow staff = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
        AuthorizationDS.PositionDataTable positions = new AuthorizationBLL().GetPositionByStuffUser(staff.StuffUserId);
        if (positions.Count > 1) {
            this.Response.Redirect("~/PositionSelect.aspx");
        } else {
            this.Response.Redirect("~/Home.aspx");
        }
    }

    protected void BusinessProxyGridView_SelectedIndexChanged(object sender, EventArgs e) {
        GridViewRow row = this.BusinessProxyGridView.SelectedRow;
        Label stuffUserIdCtl = (Label)row.FindControl("StuffUserIdCtl");
        Label positionIdCtl = (Label)row.FindControl("PositionIdCtl");
        this.Session["StuffUserId"] = int.Parse(stuffUserIdCtl.Text);
        this.Session["PositionId"] = int.Parse(positionIdCtl.Text);
        this.Session["ProxyStuffUserId"] = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
        this.Session["StuffUser"] = new StuffUserBLL().GetStuffUserById((int)Session["StuffUserId"])[0];
        this.Session["Position"] = new OUTreeBLL().GetPositionById((int)Session["PositionId"]);
        this.Response.Redirect("~/Home.aspx");
    }
}
