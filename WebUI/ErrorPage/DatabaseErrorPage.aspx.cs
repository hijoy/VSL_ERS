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

public partial class ErrorPage_DatabaseErrorPage : Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "数据库访问故障提示");
            if (this.Session["ErrorInfor"] != null) {
                this.lblError.Text = this.Session["ErrorInfor"].ToString();
            }
        }
    }
}
