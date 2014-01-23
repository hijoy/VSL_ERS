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

public delegate void ShopNameTextChanged(object sender, EventArgs e);

public partial class UserControls_ShopControl : System.Web.UI.UserControl {

    public event ShopNameTextChanged ShopNameTextChanged;

    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        if (IsReadOnly) {
            this.txtDisplayShopName.Text = this.txtShopName.Text;
        }

        this.txtDisplayShopName.ReadOnly = this.IsReadOnly;
        this.txtShopName.Style["display"] = "none";
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

    private string _isVisible = "inline";
    public string IsVisible {
        get {
            return _isVisible;
        }
        set {
            this._isVisible = value.ToString().Equals("false", StringComparison.CurrentCultureIgnoreCase) ? "none" : "inline";
        }
    }

    private string _isNoClear = "inline";
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
            return this.txtDisplayShopName.CssClass;
        }
        set {
            this.txtDisplayShopName.CssClass = value;
        }
    }

    public Unit Height {
        get {
            return this.txtDisplayShopName.Height;
        }
        set {
            this.txtDisplayShopName.Height = value;
        }
    }

    public Unit Width {
        get {
            return this.txtDisplayShopName.Width;
        }
        set {
            this.txtDisplayShopName.Width = value;
        }
    }

    public bool AutoPostBack {
        get {
            return this.txtShopName.AutoPostBack;
        }
        set {
            this.txtShopName.AutoPostBack = value;
        }
    }

    public string ShopName {
        get {
            return this.txtDisplayShopName.Text.Trim();
        }
        set {
            this.txtDisplayShopName.Text = value;
        }
    }

    public string ShopID {
        get {
            return this.ShopIDCtl.Value.ToString();
        }
        set {
            if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value)))) {
                this.ShopIDCtl.Value = value;
                MasterDataBLL bll = new MasterDataBLL();
                this.ShopNameCtl.Value = bll.GetShopViewById(int.Parse(value)).ShopName;
                this.txtShopName.Text = bll.GetShopViewById(int.Parse(value)).ShopName;
            } else {
                this.ShopIDCtl.Value = "";
                this.ShopNameCtl.Value = "";
                this.txtShopName.Text = "";
            }
        }
    }

    #endregion

    #region script

    protected string GetShowDlgScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/Dialog/ShopSearch.aspx";
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayShopName.ClientID + @"').value = DialogValue[1];";
        }
        script.Append(@"var url = '" + url + @"';                        
                        var DialogValue = window.showModalDialog(url,window,'dialogHeight: 652px; dialogWidth: 895px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
                        if (DialogValue != null) {
                            document.getElementById('" + this.ShopIDCtl.ClientID + @"').value = DialogValue[0];
                            document.getElementById('" + this.ShopNameCtl.ClientID + @"').value = DialogValue[1];" + getDisplayName +
                            @"var ShopNameCtl = document.getElementById('" + this.txtShopName.ClientID + @"');
                            ShopNameCtl.value = DialogValue[1];
                            if (ShopNameCtl.onchange) {
                                document.getElementById('" + this.txtShopName.ClientID + @"').onchange();
                            }
                        }");
        return script.ToString();
    }

    protected string GetResetScript() {
        StringBuilder script = new StringBuilder();
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayShopName.ClientID + @"').value = '';";
        }
        script.Append(@"document.getElementById('" + this.ShopIDCtl.ClientID + @"').value = '';" + getDisplayName +
                        @"document.getElementById('" + this.ShopNameCtl.ClientID + @"').value = '';
                        var ShopNameCtl = document.getElementById('" + this.txtShopName.ClientID + @"');
                        ShopNameCtl.value = '';
                        if (ShopNameCtl.onchange) {
                            document.getElementById('" + this.txtShopName.ClientID + @"').onchange();
                        }");
        return script.ToString();
    }
    #endregion

    protected void txtShopName_TextChanged(object sender, EventArgs e) {

        if (ShopNameTextChanged != null) {
            ShopNameTextChanged(sender, e);
        }
    }
}
