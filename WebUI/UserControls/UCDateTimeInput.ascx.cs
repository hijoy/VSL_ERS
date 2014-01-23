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

public partial class UserControls_UCDateTimeInput : System.Web.UI.UserControl
{
    /// <summary> 
    /// ÈÕÆÚÖµ 
    /// </summary> 
    /// <remarks></remarks> 
    public string SelectedDate
    {

        get { return this.txtDate.Text + this.ddlHour.SelectedValue; }
        set
        {

            if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value))))
            {
                this.txtDate.Text = DBNullConverter.ToDateTime(value).ToString("yyyy-MM-dd");
                if (DBNullConverter.ToDateTime(value).Hour.ToString() == string.Empty)
                {
                    this.ddlHour.SelectedValue = "00";
                }
                else
                {
                    if (DBNullConverter.ToDateTime(value).Hour < 10)
                    {
                        this.ddlHour.SelectedValue = "0"+DBNullConverter.ToDateTime(value).Hour.ToString();
                    }
                    else
                    {
                        this.ddlHour.SelectedValue = DBNullConverter.ToDateTime(value).Hour.ToString();
                    }
                }

            }

            else
            {
                this.txtDate.Text = string.Empty;
                this.ddlHour.SelectedValue = "00";
            }

        }
    }

    private string _formatDateTimeString = "yyyy-MM-dd";
    public string FormatDateTimeString
    {
        get
        {
            return _formatDateTimeString;
        }
        set
        {
            _formatDateTimeString = value;
        }
    }

    public string ToolTip
    {
        get { return this.txtDate.ToolTip; }
        set
        {
            this.txtDate.ToolTip = value;
        }
    }

    public string ValidationGroup
    {
        get { return this.txtDate.ValidationGroup; }
        set
        {
            this.txtDate.ValidationGroup = value;
        }
    }

    public System.Drawing.Color BackColor
    {
        get { return this.txtDate.BackColor; }
        set { this.txtDate.BackColor = value; }
    }


    private bool _readonly;
    public bool IsReadOnly
    {
        get { return this._readonly; }
        set { this._readonly = value; }
    }

    public string CssClass
    {
        get { return this.txtDate.CssClass; }
        set { this.txtDate.CssClass = value; }
    }

    protected override void OnLoad(EventArgs e)
    {
        if ((!this.IsReadOnly))
        {
            this.ibtDate.Attributes.Add("onclick", "javascript:DateSelection('" + this.txtDate.ClientID + string.Format("',{0}); return false;", "false"));

        }

        if (this.IsReadOnly)
        {

            this.txtDate.Attributes.Add("readonly", "readonly");

        }

        this.txtDate.Attributes.Add("onkeypress", "javascript:return CheckInputIsDate(this);");
        this.txtDate.Attributes.Add("onchange", string.Format("javascript:return CheckDatePicker(this,{0});", "false"));

    }

    protected void ddlHour_Load(object sender, EventArgs e)
    {
        if (this.ddlHour.Items.Count == 0)
        {
            for (int index = 0; index < 24; index++)
            {
                ListItem itemIndex = new ListItem();
                if (index < 10)
                {
                    itemIndex.Text = "0" + index.ToString();
                    itemIndex.Value = "0" + index.ToString();
                }
                else
                {
                    itemIndex.Text = index.ToString();
                    itemIndex.Value = index.ToString();
                }

                ddlHour.Items.Add(itemIndex);
            }
        }
    }
}
