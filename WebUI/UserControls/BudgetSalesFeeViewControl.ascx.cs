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

public delegate void CustomerTextChanged(object sender, EventArgs e);
public partial class UserControls_BudgetSalesFeeViewControl : System.Web.UI.UserControl {
    public event CustomerTextChanged CustomerTextChanged;
    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        if (IsReadOnly) {
            this.txtDisplayCustomerName.Text = this.txtCustomerName.Text;
        }

        this.txtDisplayCustomerName.ReadOnly = this.IsReadOnly;
        this.txtCustomerName.Style["display"] = "none";
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
            return this.txtDisplayCustomerName.CssClass;
        }
        set {
            this.txtDisplayCustomerName.CssClass = value;
        }
    }


    public Unit Height {
        get {
            return this.txtDisplayCustomerName.Height;
        }
        set {
            this.txtDisplayCustomerName.Height = value;
        }
    }

    public Unit Width {
        get {
            return this.txtDisplayCustomerName.Width;
        }
        set {
            this.txtDisplayCustomerName.Width = value;
        }
    }

    public bool AutoPostBack {
        get {
            return this.txtCustomerName.AutoPostBack;
        }
        set {
            this.txtCustomerName.AutoPostBack = value;
        }
    }

    public string CustomerName {
        get {
            return this.txtDisplayCustomerName.Text.Trim();
        }
        set {
            this.txtDisplayCustomerName.Text = value;
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

    public string BudgetSalesFeeId {
        get { return this.BudgetSalesFeeIDIDCtl.Value; }
    }

    //public string CustomerID
    //{
    //    get
    //    {
    //        return this.CustomerIDCtl.Value.ToString();
    //    }
    //    set
    //    {
    //        if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value))))
    //        {
    //            this.CustomerIDCtl.Value = value;
    //            MasterDataBLL bll = new MasterDataBLL();
    //            this.CustomerNameCtl.Value = bll.GetCustomerById(int.Parse(value)).CustomerName;
    //            this.txtCustomerName.Text = bll.GetCustomerById(int.Parse(value)).CustomerName;
    //        }
    //        else
    //        {
    //            this.CustomerIDCtl.Value = "";
    //            this.CustomerNameCtl.Value = "";
    //            this.txtCustomerName.Text = "";
    //        }
    //    }
    //}


    #endregion


    #region script

    protected string GetShowDlgScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/Dialog/BudgetSalesFeeViewSearch.aspx";
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayCustomerName.ClientID + @"').value = DialogValue[1];";
        }
        script.Append(@"var url = '" + url + @"';                        
                        var DialogValue = window.showModalDialog(url,window,'dialogHeight: 652px; dialogWidth: 880px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
                        if (DialogValue != null) {
                            document.getElementById('" + this.BudgetSalesFeeIDIDCtl.ClientID + @"').value = DialogValue[0];
                            document.getElementById('" + this.CustomerNameCtl.ClientID + @"').value = DialogValue[1];" + getDisplayName +
                            @"var CustomerNameCtl = document.getElementById('" + this.txtCustomerName.ClientID + @"');
                            CustomerNameCtl.value = DialogValue[1];
                            if (CustomerNameCtl.onchange) {
                                document.getElementById('" + this.txtCustomerName.ClientID + @"').onchange();
                            }
                        }");
        return script.ToString();
    }

    protected string GetResetScript() {
        StringBuilder script = new StringBuilder();
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayCustomerName.ClientID + @"').value = '';";
        }
        script.Append(@"document.getElementById('" + this.BudgetSalesFeeIDIDCtl.ClientID + @"').value = '';" + getDisplayName +
                        @"document.getElementById('" + this.CustomerNameCtl.ClientID + @"').value = '';
                        var CustomerNameCtl = document.getElementById('" + this.txtCustomerName.ClientID + @"');
                        CustomerNameCtl.value = '';
                        if (CustomerNameCtl.onchange) {
                            document.getElementById('" + this.txtCustomerName.ClientID + @"').onchange();
                        }");
        return script.ToString();
    }
    #endregion


    protected void txtCustomerName_TextChanged(object sender, EventArgs e) {

        if (CustomerTextChanged != null) {
            CustomerTextChanged(sender, e);
        }
    }
}