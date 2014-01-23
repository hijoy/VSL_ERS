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
using System.Text;
using BusinessObjects;

public partial class UserControls_OUSelect : System.Web.UI.UserControl {
    protected void Page_Load(object sender, EventArgs e) {

    }

    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        this.DisplayCtl.Text = this.OUNameCtl.Text;
        this.OUNameCtl.Style["display"] = "none";
    }

    #region property
    public string CssClass {
        get {
            return this.DisplayCtl.CssClass;
        }
        set {
            this.DisplayCtl.CssClass = value;
        }
    }



    public Unit Width {
        get {
            return this.DisplayCtl.Width;
        }
        set {
            this.DisplayCtl.Width = value;
        }
    }

    public Unit Height {
        get {
            return this.DisplayCtl.Height;
        }
        set {
            this.DisplayCtl.Height = value;
        }
    }

    public bool AutoPostBack {
        get {
            return this.OUNameCtl.AutoPostBack;
        }
        set {
            this.OUNameCtl.AutoPostBack = value;
        }
    }

    public int? OUId {
        get {
            if (this.OUIdCtl.Value.Length == 0) {
                return null;
            } else {
                return int.Parse(this.OUIdCtl.Value);
            }
        }
        set {
            if (value == null) {
                this.OUIdCtl.Value = "";
                this.OUNameCtl.Text = "";
                this.OUCodeCtl.Value = "";
            } else {
                this.OUIdCtl.Value = value.ToString();
                AuthorizationDS.OrganizationUnitRow ou = new OUTreeBLL().GetOrganizationUnitById(value.GetValueOrDefault());
                this.OUCodeCtl.Value = ou.IsOrganizationUnitCodeNull() ? "" : ou.OrganizationUnitCode;
                this.OUNameCtl.Text = ou.OrganizationUnitName;
            }
        }
    }

    public string OUCode {
        get {
            return this.OUCodeCtl.Value;
        }
    }

    public string OUName {
        get {
            return this.OUNameCtl.Text;
        }
    }

    public bool ReadOnly {
        get {
            if (this.ViewState["ReadOnly"] == null) {
                this.ViewState["ReadOnly"] = false;
            }
            return (bool)this.ViewState["ReadOnly"];
        }
        set {
            this.ViewState["ReadOnly"] = value;
        }
    }

    public bool Enabled {
        get {
            if (this.ViewState["Enabled"] == null) {
                this.ViewState["Enabled"] = true;
            }
            return (bool)this.ViewState["Enabled"];
        }
        set {
            this.ViewState["Enabled"] = value;
        }
    }

    private string _isNoClear = "";
    public string IsNoClear {
        get {
            return _isNoClear;
        }
        set {
            this._isNoClear = value.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase) ? "none" : "inline";
        }
    }

    #endregion

    #region enable readonly selectonly
    protected string GetSelectVisible() {
        if (ReadOnly) {
            return "none";
        } else {
            return "''";
        }
    }

    //protected string GetSelectDisable() {
    //    if (Enabled) {
    //        return "false";
    //    } else {
    //        return "true";
    //    }
    //}

    //protected string GetResetDisable() {
    //    if (Enabled) {
    //        return "false";
    //    } else {
    //        return "true";
    //    }
    //}

    #endregion

    #region script
    protected string GetShowDlgScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationSettings.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/Dialog/OrganizationUnitSelectDlg.aspx";
        script.Append(@"var ouIdCtl = document.getElementById('" + this.OUIdCtl.ClientID + @"');
                        var url = '" + url + @"';
                        if (ouIdCtl.value.length > 0) {
                            url = url + '?OUId=' + ouIdCtl.value;
                        }
                        var returnValue = window.showModalDialog(url,window,'dialogHeight: 700px; dialogWidth: 850px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
                        if (returnValue != null) {
                            ouIdCtl.value = returnValue.ouId;
                            document.getElementById('" + this.OUCodeCtl.ClientID + @"').value = returnValue.ouCode;
                            document.getElementById('" + this.DisplayCtl.ClientID + @"').value = returnValue.ouName;
                            var ouNameCtl = document.getElementById('" + this.OUNameCtl.ClientID + @"');
                            ouNameCtl.value = returnValue.ouName;
                            if (ouNameCtl.onchange) {
                                document.getElementById('" + this.OUNameCtl.ClientID + @"').onchange();
                            }
                        }");
        return script.ToString();
    }

    protected string GetResetScript() {
        StringBuilder script = new StringBuilder();
        script.Append(@"document.getElementById('" + this.OUIdCtl.ClientID + @"').value = '';
                        document.getElementById('" + this.OUCodeCtl.ClientID + @"').value = '';
                        document.getElementById('" + this.DisplayCtl.ClientID + @"').value = '';
                        var ouNameCtl = document.getElementById('" + this.OUNameCtl.ClientID + @"');
                        ouNameCtl.value = '';
                        if (ouNameCtl.onchange) {
                            document.getElementById('" + this.OUNameCtl.ClientID + @"').onchange();
                        }");
        return script.ToString();
    }
    #endregion

    #region selectedchange event
    public event EventHandler OUSelectedChanged;

    protected void OUNameCtl_TextChanged(object sender, EventArgs e) {
        if (this.OUSelectedChanged != null) {
            this.OUSelectedChanged(this, null);
        }
    }
    #endregion
}
