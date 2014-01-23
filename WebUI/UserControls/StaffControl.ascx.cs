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

public delegate void StaffNameTextChanged(object sender, EventArgs e);
public partial class UserControls_StaffControl : System.Web.UI.UserControl {
    public event StaffNameTextChanged StaffNameTextChanged;
    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        if (IsReadOnly) {
            this.txtDisplayStaffName.Text = this.txtStaffName.Text;
        }

        this.txtDisplayStaffName.ReadOnly = this.IsReadOnly;
        this.txtStaffName.Style["display"] = "none";
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
            return this.txtDisplayStaffName.CssClass;
        }
        set {
            this.txtDisplayStaffName.CssClass = value;
        }
    }


    public Unit Height {
        get {
            return this.txtDisplayStaffName.Height;
        }
        set {
            this.txtDisplayStaffName.Height = value;
        }
    }

    public Unit Width {
        get {
            return this.txtDisplayStaffName.Width;
        }
        set {
            this.txtDisplayStaffName.Width = value;
        }
    }

    public bool AutoPostBack {
        get {
            return this.txtStaffName.AutoPostBack;
        }
        set {
            this.txtStaffName.AutoPostBack = value;
        }
    }

    public string StaffName {
        get {
            return this.txtDisplayStaffName.Text.Trim();
        }
        set {
            this.txtDisplayStaffName.Text = value;
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

    public string StaffID {
        get {
            return this.StaffIDCtl.Value.ToString();
        }
        set {
            if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value)))) {
                this.StaffIDCtl.Value = value;

                AuthorizationBLL bll = new AuthorizationBLL();
                this.StaffNameCtl.Value = bll.GetStuffUserById(int.Parse(value)).StuffName;
                this.txtStaffName.Text = bll.GetStuffUserById(int.Parse(value)).StuffName;
            } else {
                this.StaffIDCtl.Value = "";
                this.StaffNameCtl.Value = "";
                this.txtStaffName.Text = "";
            }
        }
    }


    #endregion


    #region script

    protected string GetShowDlgScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/Dialog/StaffSearch.aspx";
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayStaffName.ClientID + @"').value = DialogValue[1];";
        }
        script.Append(@"var url = '" + url + @"';                        
                        var DialogValue = window.showModalDialog(url,window,'dialogHeight: 652px; dialogWidth: 880px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
                        if (DialogValue != null) {
                            document.getElementById('" + this.StaffIDCtl.ClientID + @"').value = DialogValue[0];
                            document.getElementById('" + this.StaffNameCtl.ClientID + @"').value = DialogValue[1];" + getDisplayName +
                            @"var StaffNameCtl = document.getElementById('" + this.txtStaffName.ClientID + @"');
                            StaffNameCtl.value = DialogValue[1];
                            if (StaffNameCtl.onchange) {
                                document.getElementById('" + this.txtStaffName.ClientID + @"').onchange();
                            }
                        }");
        return script.ToString();
    }

    protected string GetResetScript() {
        StringBuilder script = new StringBuilder();
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayStaffName.ClientID + @"').value = '';";
        }
        script.Append(@"document.getElementById('" + this.StaffIDCtl.ClientID + @"').value = '';" + getDisplayName +
                        @"document.getElementById('" + this.StaffNameCtl.ClientID + @"').value = '';
                        var StaffNameCtl = document.getElementById('" + this.txtStaffName.ClientID + @"');
                        StaffNameCtl.value = '';
                        if (StaffNameCtl.onchange) {
                            document.getElementById('" + this.txtStaffName.ClientID + @"').onchange();
                        }");
        return script.ToString();
    }
    #endregion

    protected void txtStaffName_TextChanged(object sender, EventArgs e) {

        if (StaffNameTextChanged != null) {
            StaffNameTextChanged(sender, e);
        }
    }


}
