using System;
using System.Data;
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

public partial class Dialog_BudgetSalesFeeViewSearch : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender,e);
        if (!this.IsPostBack)
        {
            this.Page.Title = "预算调拨查询";
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            this.odsCustomer.SelectParameters["PositionID"].DefaultValue = position.PositionId.ToString();
            string filterStr = "1=1";
            filterStr += "and CustomerName = ''";
            this.odsBudget.SelectParameters["queryExpression"].DefaultValue = filterStr;
        }
    }

    protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count <= 1)
        {
            return;
        }
        Label lblCustomertNo = (Label)e.Row.Cells[0].FindControl("lblCustomerNoByEdit");
        if (lblCustomertNo != null)
        {
            if (lblCustomertNo.Text != string.Empty)
            {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
                e.Row.Attributes.Add("onDblClick", Page.GetPostBackEventReference(this, e.Row.DataItemIndex.ToString()));
            }
        }
    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        string filterStr = "1=1";
        if (CustomerDDL.SelectedItem.Text != "全部")
        {
            filterStr += "and CustomerName = '" + this.CustomerDDL.SelectedItem.Text + "'";
        }
        if (SearchExpenseItemDDL.SelectedItem.Text != "全部")
        {
            filterStr += "and ExpenseItemName = '" + this.SearchExpenseItemDDL.SelectedItem.Text+ "'";
        }
        if (this.ucNewPeriod.SelectedDate != string.Empty)
        {
            DateTime Date = DateTime.Parse(this.ucNewPeriod.SelectedDate.Substring(0, 4) + "-" + this.ucNewPeriod.SelectedDate.Substring(4, 2) + "-01"); 

            filterStr += "and Period = '" + Date + "'";
        }
        if (filterStr == string.Empty)
        {
            this.odsBudget.SelectParameters["queryExpression"].DefaultValue = " 1 = 1";
        }
        else
        {
            this.odsBudget.SelectParameters["queryExpression"].DefaultValue = filterStr;
        }
        this.gvBudget.DataBind();
    }

    protected virtual void OnDblClick(EventArgs e)
    {
        gvBudget.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string BudgetSalesFeeID = gvBudget.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label lblCustomerNo = (Label)gvBudget.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].Cells[0].FindControl("lblCustomerNoByEdit");
        string CustomerNo = lblCustomerNo.Text;
        Label lblCustomerName = (Label)gvBudget.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].Cells[0].FindControl("lblCustomerNameByEdit");
        string CustomerName = lblCustomerName.Text;

        string returnValue = CustomerNo + "-" + CustomerName;
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + BudgetSalesFeeID + "';" + "arryReturn[1]='" + returnValue + "';" +
            "window.returnValue=arryReturn;</script>");
        Response.Write(@"<script language='javascript'>window.close();</script>");
    }

    public void RaisePostBackEvent(string eventArgument)
    {
        GridViewSelectEventArgs e = null;
        int selectedRowIndex = -1;
        if (!string.IsNullOrEmpty(eventArgument))
        {
            string[] args = eventArgument.Split('$');
            Int32.TryParse(args[0], out selectedRowIndex);
            e = new GridViewSelectEventArgs(selectedRowIndex);
            OnDblClick(e);

        }
    }

    public string GetExpenseItemNameById(object Id) {
        ERS.ExpenseItemRow row = new MasterDataBLL().ExpenseItemAdapter.GetDataById((int)Id)[0];
        return row.AccountingCode + "--" + row.AccountingName;
    }
    //protected virtual void OnDblClick(EventArgs e)
    //{
    //    gvBudget.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
    //    string CustomerID = gvBudget.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
    //    Label lblCustomerNo = (Label)gvBudget.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].Cells[0].FindControl("lblCustomerNoByEdit");
    //    string CustomerNo = lblCustomerNo.Text;
    //    Label lblCustomerName = (Label)gvBudget.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].Cells[0].FindControl("lblCustomerNameByEdit");
    //    string CustomerName = lblCustomerName.Text;
    //    string returnValue = CustomerNo + "-" + CustomerName;
    //    Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + CustomerID + "';" + "arryReturn[1]='" + returnValue + "';" +
    //        "window.returnValue=arryReturn;</script>");
    //    Response.Write(@"<script language='javascript'>window.close();</script>");
    //}
}