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

public delegate void MaterialNameTextChanged(object sender, EventArgs e);

public partial class UserControls_MaterialControl : System.Web.UI.UserControl {
    public event MaterialNameTextChanged MaterialNameTextChanged;

    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        if (IsReadOnly) {
            this.txtDisplayMaterialName.Text = this.txtMaterialName.Text;
        }

        this.txtDisplayMaterialName.ReadOnly = this.IsReadOnly;
        this.txtMaterialName.Style["display"] = "none";
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

    private string _isNoClear = "";
    public string IsNoClear {
        get {
            return _isNoClear;
        }
        set {
            this._isNoClear = value.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase) ? "none" : "inline";
        }
    }

    public string CssClass {
        get {
            return this.txtDisplayMaterialName.CssClass;
        }
        set {
            this.txtDisplayMaterialName.CssClass = value;
        }
    }


    public Unit Height {
        get {
            return this.txtDisplayMaterialName.Height;
        }
        set {
            this.txtDisplayMaterialName.Height = value;
        }
    }

    public Unit Width {
        get {
            return this.txtDisplayMaterialName.Width;
        }
        set {
            this.txtDisplayMaterialName.Width = value;
        }
    }

    public bool AutoPostBack {
        get {
            return this.txtMaterialName.AutoPostBack;
        }
        set {
            this.txtMaterialName.AutoPostBack = value;
        }
    }

    public string MaterialName {
        get {
            return this.txtDisplayMaterialName.Text.Trim();
        }
        set {
            this.txtDisplayMaterialName.Text = value;
        }
    }

    public string MaterialID {
        get {
            return this.MaterialIDCtl.Value.ToString();
        }
        set {
            if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value)))) {
                this.MaterialIDCtl.Value = value;
                MasterDataBLL bll = new MasterDataBLL();
                ERS.MaterialRow material = new MasterDataBLL().GetMaterialById(int.Parse(value));
                this.MaterialNameCtl.Value = material.MaterialName;
                this.txtMaterialName.Text = material.MaterialName ;
            } else {
                this.MaterialIDCtl.Value = "";
                this.MaterialNameCtl.Value = "";
                this.txtMaterialName.Text = "";
            }
        }
    }


    #endregion


    #region script

    protected string GetShowDlgScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/Dialog/MaterialSearch.aspx";
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayMaterialName.ClientID + @"').value = DialogValue[1];";
        }
        script.Append(@"var url = '" + url + @"';                        
                        var DialogValue = window.showModalDialog(url,window,'dialogHeight: 650px; dialogWidth: 800px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
                        if (DialogValue != null) {
                            document.getElementById('" + this.MaterialIDCtl.ClientID + @"').value = DialogValue[0];
                            document.getElementById('" + this.MaterialNameCtl.ClientID + @"').value = DialogValue[1];" + getDisplayName +
                            @"var MaterialNameCtl = document.getElementById('" + this.txtMaterialName.ClientID + @"');
                            MaterialNameCtl.value = DialogValue[1];
                            if (MaterialNameCtl.onchange) {
                                document.getElementById('" + this.txtMaterialName.ClientID + @"').onchange();
                            }
                        }");
        return script.ToString();
    }

    protected string GetResetScript() {
        StringBuilder script = new StringBuilder();
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayMaterialName.ClientID + @"').value = '';";
        }
        script.Append(@"document.getElementById('" + this.MaterialIDCtl.ClientID + @"').value = '';" + getDisplayName +
                        @"document.getElementById('" + this.MaterialNameCtl.ClientID + @"').value = '';
                        var MaterialNameCtl = document.getElementById('" + this.txtMaterialName.ClientID + @"');
                        MaterialNameCtl.value = '';
                        if (MaterialNameCtl.onchange) {
                            document.getElementById('" + this.txtMaterialName.ClientID + @"').onchange();
                        }");
        return script.ToString();
    }
    #endregion


    protected void txtMaterialName_TextChanged(object sender, EventArgs e) {

        if (MaterialNameTextChanged != null) {
            MaterialNameTextChanged(sender, e);
        }
    }
}
