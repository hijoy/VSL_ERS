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

public partial class DefaultErrorPage : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "系统访问异常");
        }
    }
}
