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
using System.Diagnostics;

public partial class Dialog_MaterialSearch : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        this.Page.Title = "广宣物资查询";
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        string filterStr = "IsActive = 1";
        if (this.txtMaterialName.Text != string.Empty) {
            if (filterStr == string.Empty) {
                filterStr = " MaterialName like '%" + this.txtMaterialName.Text.Trim() + "%'";
            } else {
                filterStr += " AND MaterialName like '%" + this.txtMaterialName.Text.Trim() + "%'";
            }
        }

        if (filterStr == string.Empty) {
            this.odsMaterial.SelectParameters["queryExpression"].DefaultValue = " IsActive = 1";
        } else {
            this.odsMaterial.SelectParameters["queryExpression"].DefaultValue = filterStr;
        }
        this.gvMaterial.DataBind();
    }

    protected void gvMaterial_RowDataBound(object sender, GridViewRowEventArgs e) {
        Label lblMaterialName = (Label)e.Row.Cells[0].FindControl("lblMaterialName");
        if (lblMaterialName != null) {
            if (lblMaterialName.Text != string.Empty) {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
                e.Row.Attributes.Add("onDblClick", Page.GetPostBackEventReference(this, e.Row.DataItemIndex.ToString()));
            }
        }
    }


    public void RaisePostBackEvent(string eventArgument) {
        GridViewSelectEventArgs e = null;
        int selectedRowIndex = -1;
        if (!string.IsNullOrEmpty(eventArgument)) {
            string[] args = eventArgument.Split('$');
            Int32.TryParse(args[0], out selectedRowIndex);
            e = new GridViewSelectEventArgs(selectedRowIndex);
            OnDblClick(e);
        }
    }

    protected virtual void OnDblClick(EventArgs e) {
        gvMaterial.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string MaterialID = gvMaterial.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label lblMaterialName = (Label)gvMaterial.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].Cells[0].FindControl("lblMaterialName");
        string MaterialName = lblMaterialName.Text;
        string returnValue = MaterialName;
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + MaterialID + "';" + "arryReturn[1]='" + returnValue + "';" +
            "window.returnValue=arryReturn;</script>");
        Response.Write(@"<script language='javascript'>window.close();</script>");
    }

}
