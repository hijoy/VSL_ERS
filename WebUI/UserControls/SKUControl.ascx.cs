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
using System.Diagnostics;

using BusinessObjects;

public delegate void SKUNameTextChanged(object sender, EventArgs e);

public partial class UserControls_SKUControl : System.Web.UI.UserControl {
    public event SKUNameTextChanged SKUNameTextChanged;

    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        if (IsReadOnly) {
            this.txtDisplaySKUName.Text = this.txtSKUName.Text;
        }

        this.txtDisplaySKUName.ReadOnly = this.IsReadOnly;
        this.txtSKUName.Style["display"] = "none";
    }

    #region property

    private bool _isReadOnly = true;
    public bool IsReadOnly {
        get {
            return _isReadOnly;
        }
        set {
            this._isReadOnly = value;
        }
    }

    private string _isVisible = "";
    public string IsVisible {
        get {
            return _isVisible;
        }
        set {
            this._isVisible = value;
        }
    }

    public string CssClass {
        get {
            return this.txtDisplaySKUName.CssClass;
        }
        set {
            this.txtDisplaySKUName.CssClass = value;
        }
    }

    public Unit Height {
        get {
            return this.txtDisplaySKUName.Height;
        }
        set {
            this.txtDisplaySKUName.Height = value;
        }
    }

    public Unit Width {
        get {
            return this.txtDisplaySKUName.Width;
        }
        set {
            this.txtDisplaySKUName.Width = value;
        }
    }

    public bool AutoPostBack {
        get {
            return this.txtSKUName.AutoPostBack;
        }
        set {
            this.txtSKUName.AutoPostBack = value;
        }
    }

    public string SKUName {
        get {
            return this.txtDisplaySKUName.Text.Trim();
        }
        set {
            this.txtDisplaySKUName.Text = value;
        }
    }

    public string SKUID {
        get {
            return this.SKUIDCtl.Value.ToString();
        }
        set {
            if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value)))) {
                this.SKUIDCtl.Value = value;
                MasterDataBLL bll = new MasterDataBLL();
                this.SKUNameCtl.Value = bll.GetSKUById(int.Parse(value)).SKUName;
                this.txtSKUName.Text = bll.GetSKUById(int.Parse(value)).SKUName;
            } else {
                this.SKUIDCtl.Value = "";
                this.SKUNameCtl.Value = "";
                this.txtSKUName.Text = "";
            }
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


    #region script

    protected string GetShowDlgScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/Dialog/SKUSearch.aspx";
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplaySKUName.ClientID + @"').value = DialogValue[1];";
        }
        script.Append(@"var url = '" + url + @"';                        
                        var DialogValue = window.showModalDialog(url,window,'dialogHeight: 652px; dialogWidth: 895px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
                        if (DialogValue != null) {
                            document.getElementById('" + this.SKUIDCtl.ClientID + @"').value = DialogValue[0];
                            document.getElementById('" + this.SKUNameCtl.ClientID + @"').value = DialogValue[1];" + getDisplayName +
                            @"var SKUNameCtl = document.getElementById('" + this.txtSKUName.ClientID + @"');
                            SKUNameCtl.value = DialogValue[1];
                            if (SKUNameCtl.onchange) {
                                document.getElementById('" + this.txtSKUName.ClientID + @"').onchange();
                            }
                        }");
        return script.ToString();
    }

    protected string GetResetScript() {
        StringBuilder script = new StringBuilder();
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplaySKUName.ClientID + @"').value = '';";
        }
        script.Append(@"document.getElementById('" + this.SKUIDCtl.ClientID + @"').value = '';" + getDisplayName +
                        @"document.getElementById('" + this.SKUNameCtl.ClientID + @"').value = '';
                        var SKUNameCtl = document.getElementById('" + this.txtSKUName.ClientID + @"');
                        SKUNameCtl.value = '';
                        if (SKUNameCtl.onchange) {
                            document.getElementById('" + this.txtSKUName.ClientID + @"').onchange();
                        }");
        return script.ToString();
    }
    #endregion


    protected void txtSKUName_TextChanged(object sender, EventArgs e) {
        if (SKUNameTextChanged != null) {
            SKUNameTextChanged(sender, e);
        }
    }
}
