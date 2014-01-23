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
using System.Text.RegularExpressions;

public delegate void DateTextChanged(object sender, EventArgs e);

public partial class UserControls_YearAndMonthUserControl : System.Web.UI.UserControl {
    public event DateTextChanged DateTextChanged;

    /// <summary> 
    /// 日期值 
    /// </summary> 
    /// <remarks></remarks> 
    public string SelectedDate {

        get { return this.txtDate.Text; }
        set {

            if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value)))) {

                if (this.IsExpensePeriod) {
                    this.txtDate.Text = DBNullConverter.ToDateTime(value).ToString("yyyyMM");
                } else {
                    this.txtDate.Text = DBNullConverter.ToDateTime(value).ToString("yyyy-MM-dd");
                }
            } else {

                this.txtDate.Text = string.Empty;

            }

        }
    }

    public System.Drawing.Color BackColor {
        get { return this.txtDate.BackColor; }



        set { this.txtDate.BackColor = value; }
    }


    /// <summary> 
    /// 是否是预算选择的控件 
    /// </summary> 
    /// <remarks></remarks> 
    private bool _expensePeriod = false;
    [System.ComponentModel.Description("是否用于预算选择的控件"), System.ComponentModel.DefaultValue(false)]
    public bool IsExpensePeriod {
        get { return this._expensePeriod; }
        set { this._expensePeriod = value; }
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
            this.ibtDate.Attributes.Add("onclick", "javascript:DateSelection('" + this.txtDate.ClientID + string.Format("',{0}); return false;", IsExpensePeriod.ToString().ToLower()));
        }
        if (this.IsReadOnly) {
            this.txtDate.Attributes.Add("readonly", "readonly");
        }
        this.txtDate.Attributes.Add("onkeypress", "javascript:return CheckInputIsDate(this);");
        this.txtDate.Attributes.Add("onblur", "javascript:return CheckExpensePeriodDatePicker(this);");
        base.OnLoad(e);
    }

    public bool AutoPostBack {
        get {
            return this.txtDate.AutoPostBack;
        }
        set {
            this.txtDate.AutoPostBack = value;
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e) {
        if (DateTextChanged != null && this.txtDate.Text != string.Empty && Regex.IsMatch(this.txtDate.Text.ToString(), @"^\d{4}?(?:0[1-9]|1[0-2])$")) {
            DateTextChanged(sender, e);
        }
    }
}
