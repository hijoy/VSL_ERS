using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessObjects;
using BusinessObjects.AuthorizationDSTableAdapters;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page {
    protected void Page_Load(object sender, EventArgs e) {
        AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
        if (stuffUser == null) {
            this.Response.Redirect("~/LogIn.aspx");
            return;
            string userName = User.Identity.Name;
            if (userName == null) {
                userName = "";
            }
            AuthorizationDS.StuffUserDataTable stable = new BusinessObjects.AuthorizationDSTableAdapters.StuffUserTableAdapter().GetDataByUserName(userName);
            if (stable.Count != 0) {
                Session["StuffUser"] = stable[0];
                stuffUser = stable[0];
            } else {
                this.Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
            }
        }
    }

    protected bool IsBusinessProxy(int ProxyUserID, DateTime SubmitDate) {
        AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
        ERS.ProxyReimburseDataTable tbProxy = new MasterDataBLL().GetProxyReimburseByParameter(ProxyUserID,stuffUser.StuffUserId , SubmitDate);
        if (tbProxy != null && tbProxy.Count > 0) {
            return true;
        }
        return false;
    }
}

