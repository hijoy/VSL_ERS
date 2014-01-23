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

public partial class UserControls_UCDateInput : System.Web.UI.UserControl {
    /// <summary> 
    /// ÈÕÆÚÖµ 
    /// </summary> 
    /// <remarks></remarks> 
    public string SelectedDate {

        get { return this.txtDate.Text; }
        set {

            if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value)))) {
                this.txtDate.Text = DBNullConverter.ToDateTime(value).ToString(FormatDateTimeString);
            } else {

                this.txtDate.Text = string.Empty;

            }

        }
    }

    private string _formatDateTimeString = "yyyy-MM-dd";
    public string FormatDateTimeString {
        get {
            return _formatDateTimeString;
        }
        set {
            _formatDateTimeString = value;
        }
    }

    public string ToolTip {
        get { return this.txtDate.ToolTip; }
        set {
            this.txtDate.ToolTip = value;
        }
    }

    public string ValidationGroup {
        get { return this.txtDate.ValidationGroup; }
        set {
            this.txtDate.ValidationGroup = value;
        }
    }

    public System.Drawing.Color BackColor {
        get { return this.txtDate.BackColor; }
        set { this.txtDate.BackColor = value; }
    }


    private bool _readonly;
    public bool IsReadOnly {
        get { return this._readonly; }
        set { this._readonly = value; }
    }

    public string CssClass {
        get { return this.txtDate.CssClass; }
        set { this.txtDate.CssClass = value; }
    }

    protected override void OnLoad(EventArgs e) {
        if ((!this.IsReadOnly)) {
            this.ibtDate.Attributes.Add("onclick", "javascript:DateSelection('" + this.txtDate.ClientID + string.Format("',{0}); return false;", "false"));

        }

        if (this.IsReadOnly) {

            this.txtDate.Attributes.Add("readonly", "readonly");

        }

        this.txtDate.Attributes.Add("onkeypress", "javascript:return CheckInputIsDate(this);");
        this.txtDate.Attributes.Add("onchange", string.Format("javascript:return CheckDatePicker(this,{0});", "false"));
    }
}
