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
using System.Data.SqlClient;
using BusinessObjects.AuthorizationDSTableAdapters;

public partial class LogIn : Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (!this.IsPostBack) {
            if (this.Session["LoginErrorInfor"] != null) {
                this.ErrorCtl.Text = this.Session["LoginErrorInfor"].ToString();
            }
            this.Session.Clear();
        }
    }

    private StuffUserBLL m_BLL;
    public StuffUserBLL BLL {
        get {
            if (m_BLL == null) {
                m_BLL = new StuffUserBLL();
            }
            return m_BLL;
        }
    }

    protected void LogInValidator_ServerValidate(object source, ServerValidateEventArgs args) {
        args.IsValid = this.BLL.LogInUser(Request.UserHostAddress, this.UserIdCtl.Text.Trim(), FormsAuthentication.HashPasswordForStoringInConfigFile(this.PasswordCtl.Text, "MD5"));
        //args.IsValid = this.BLL.LogInUser(Request.UserHostAddress, this.UserIdCtl.Text.Trim(), this.PasswordCtl.Text);
        if (!args.IsValid) {
            AuthorizationDS.StuffUserDataTable stuffUserDT = this.BLL.GetStuffUserByUserId(this.UserIdCtl.Text.Trim());
            if (stuffUserDT != null && stuffUserDT.Rows.Count > 0) {
                if (!stuffUserDT[0].IsActive) {
                    this.MessageCtl.Text = "该帐户已被停用！";
                }
            } else {
                this.MessageCtl.Text = "系统不存在登陆帐号" + this.UserIdCtl.Text.Trim() + "!";
            }
        }

    }

    protected void LogInBtn_Click(object sender, EventArgs e) {
        if (this.IsValid) {
            BusinessObjects.AuthorizationDS.StuffUserDataTable table = this.BLL.GetStuffUserByUserId(this.UserIdCtl.Text.Trim());
            Session.Clear();
            Session["StuffUser"] = table[0];
            if (new AuthorizationBLL().GetProxyBusinessByProxyUserIDAndCurrentDate(table[0].StuffUserId, DateTime.Now).Count > 0) {
                this.Response.Redirect("~/BusinessProxySelect.aspx");
            } else {
                AuthorizationDS.PositionDataTable positions = new AuthorizationBLL().GetPositionByStuffUser(table[0].StuffUserId);
                if (positions.Count > 1) {
                    this.Response.Redirect("~/PositionSelect.aspx");
                } else {
                    this.Response.Redirect("~/Home.aspx");
                }
            }
        }
    }

    protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e) {
        SysLog.LogSystemError(e.Exception);
    }
}
