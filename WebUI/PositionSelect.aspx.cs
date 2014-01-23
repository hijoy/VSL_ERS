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
using System.Collections.Generic;
using System.Text;
using BusinessObjects.AuthorizationDSTableAdapters;

public partial class PositionSelect : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        this.StuffUserPositionDS.SelectParameters["StuffUserId"].DefaultValue = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId.ToString();
    }

    protected void PositionGridView_SelectedIndexChanged(object sender, EventArgs e) {
        GridViewRow row = this.PositionGridView.SelectedRow;
        Label positionIdCtl = (Label)row.FindControl("PositionIdCtl");

        this.Session["SelectedPosition"] = new PositionTableAdapter().GetDataById(int.Parse(positionIdCtl.Text))[0];
        this.Response.Redirect("~/Home.aspx");
    }

    protected void StuffUserPositionGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            BusinessObjects.AuthorizationDS.PositionRow position = (BusinessObjects.AuthorizationDS.PositionRow)drv.Row;
            List<AuthorizationDS.OrganizationUnitRow> ous = new OUTreeBLL().GetParentOUsOfPosition(position.PositionId);

            StringBuilder ouNames = new StringBuilder();
            foreach (BusinessObjects.AuthorizationDS.OrganizationUnitRow ou in ous) {
                ouNames.Append(ou.OrganizationUnitName + "-");
            }
            Label label = (Label)e.Row.FindControl("ParentOUNamesCtl");
            label.Text = ouNames.ToString();
        }
    }
}
