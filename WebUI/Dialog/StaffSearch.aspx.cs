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

using BusinessObjects;
using System.Text;

public partial class Dialog_StaffSearch : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        this.Page.Title = "¿Í»§²éÑ¯";
    }

    protected void SearchButton_Click(object sender, EventArgs e) {
        string tmpSQL = "IsActive = 1";

        if (UserAccountTextBox.Text.Trim() != string.Empty) {
            if (tmpSQL.Equals(string.Empty)) {
                tmpSQL = "UserName like '%" + UserAccountTextBox.Text + "%'";
            } else {
                tmpSQL += " AND UserName like '%" + UserAccountTextBox.Text + "%'";
            }
        }
        if (StuffNameTextBox.Text != "") {
            if (tmpSQL.Equals(string.Empty)) {
                tmpSQL = "StuffName like '%" + StuffNameTextBox.Text + "%'";
            } else {
                tmpSQL += " AND StuffName like '%" + StuffNameTextBox.Text + "%'";
            }
        }

        if (EmployeeNoTextBox.Text != "") {
            if (tmpSQL.Equals(string.Empty)) {
                tmpSQL = "StuffId like '%" + EmployeeNoTextBox.Text + "%'";
            } else {
                tmpSQL += " AND StuffId like '%" + EmployeeNoTextBox.Text + "%'";
            }
        }

        if (tmpSQL == string.Empty) {
            this.odsStuffUser.SelectParameters["queryExpression"].DefaultValue = " IsActive = 1";
        } else {
            this.odsStuffUser.SelectParameters["queryExpression"].DefaultValue = tmpSQL;
        }
        this.gvStaff.DataBind();
    }

    protected void gvStaff_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            StringBuilder positions = new StringBuilder();
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)((DataRowView)e.Row.DataItem).Row;
            foreach (AuthorizationDS.PositionRow position in new AuthorizationBLL().GetPositionByStuffUser(stuffUser.StuffUserId)) {
                if (positions.Length > 0) {
                    positions.Append(",");
                }
                positions.Append(position.PositionName);
            }

            Label positionsLabel = (Label)e.Row.FindControl("PositionsLabel");
            positionsLabel.Text = positions.ToString();
        }

        Label lblUserName = (Label)e.Row.FindControl("lblUserName");
        if (lblUserName != null) {
            if (lblUserName.Text != string.Empty) {
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
        gvStaff.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string StuffUserID = gvStaff.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label lblStuffName = (Label)gvStaff.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].FindControl("lblStuffName");
        string returnValue = lblStuffName.Text;
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + StuffUserID + "';" + "arryReturn[1]='" + returnValue + "';" +
            "window.returnValue=arryReturn;</script>");
        Response.Write(@"<script language='javascript'>window.close();</script>");
    }

}
