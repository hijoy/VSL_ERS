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
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Configuration;

public partial class UserControls_ucUpdateProgress : System.Web.UI.UserControl
{

    #region Property

    public String AssociatedUpdatePanelID
    {
        set
        {
            this.UpdateProgress1.AssociatedUpdatePanelID = value;
        }
        get
        {
            return this.UpdateProgress1.AssociatedUpdatePanelID;
        }
    }


    public String divProcessing_ClientID
    {
        get
        {
            return this.UpdateProgress1.FindControl("divProcessing").ClientID;
        }
    }

    public String divBg_ClientID {
        get {
            return this.UpdateProgress1.FindControl("divBg").ClientID;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
