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

public partial class Dialog_CustomerSearch : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        this.Page.Title = "¿Í»§²éÑ¯";
        //if (Request["SearchAll"] != null) {
        //    String searchStr = "1=1";
        //    searchStr += " and IsActive = 1";
        //    this.odsCustomer.SelectParameters["queryExpression"].DefaultValue = searchStr;
        //    this.gvCustomer.DataBind();
        //}
    }

    protected void lbtnSearch_Click(object sender, EventArgs e) {
        string filterStr = "IsActive = 1";
        if (this.txtCustNoBySearch.Text != string.Empty) {
            if (filterStr == string.Empty) {
                filterStr = " CustomerNo like '%" + this.txtCustNoBySearch.Text.Trim() + "%'";
            } else {
                filterStr += " AND CustomerNo like '%" + this.txtCustNoBySearch.Text.Trim() + "%'";
            }
        }

        if (this.txtCustNameBySearch.Text != string.Empty) {
            if (filterStr == string.Empty) {
                filterStr = " CustomerName like '%" + this.txtCustNameBySearch.Text.Trim() + "%'";
            } else {
                filterStr += " AND CustomerName like '%" + this.txtCustNameBySearch.Text.Trim() + "%'";
            }
        }

        if (filterStr == string.Empty) {
            this.odsCustomer.SelectParameters["queryExpression"].DefaultValue = " IsActive = 1";
        } else {
            this.odsCustomer.SelectParameters["queryExpression"].DefaultValue = filterStr;
        }
        this.gvCustomer.DataBind();
    }

    protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e) {
        Label lblCustomerNo = (Label)e.Row.Cells[0].FindControl("lblCustomerNo");
        if (lblCustomerNo != null) {
            if (lblCustomerNo.Text != string.Empty) {
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
        gvCustomer.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string CustomerID = gvCustomer.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label lblCustomerNo = (Label)gvCustomer.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].Cells[0].FindControl("lblCustomerNoByEdit");
        string CustomerNo = lblCustomerNo.Text;
        Label lblCustomerName = (Label)gvCustomer.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].Cells[0].FindControl("lblCustomerNameByEdit");
        string CustomerName = lblCustomerName.Text;
        string returnValue = CustomerNo + "-" + CustomerName;
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + CustomerID + "';" + "arryReturn[1]='" + returnValue + "';" +
            "window.returnValue=arryReturn;</script>");
        Response.Write(@"<script language='javascript'>window.close();</script>");
    }

    public string GetExpenseItemNameById(object Id) {
        //return new MasterDataBLL().ExpenseItemAdapter.GetDataById((int)Id)[0].ExpenseItemName;
        ERS.ExpenseItemRow row = new MasterDataBLL().ExpenseItemAdapter.GetDataById((int)Id)[0];
        return row.AccountingCode + "--" + row.ExpenseItemName;
    }

    public String GetCityNameById(object id) {
        return GetProvinceNameByCityId((int)id) + "-" + new MasterDataBLL().GetCityNameById((int)id);
    }

    public String GetProvinceNameByCityId(object id) {
        return new MasterDataBLL().GetProvinceNameByCityId((int)id);
    }

    public string GetOUNameById(object id) {
        return new OUTreeBLL().GetOrganizationUnitById((int)id).OrganizationUnitName;
    }

    public string GetCustTypeNameById(object id) {
        return new MasterDataBLL().GetCustomerTypeById((int)id).CustomerTypeName;
    }

    public string GetChanTypeNameById(object id) {
        return new MasterDataBLL().GetChannelTypeById((int)id).ChannelTypeName;
    }

    protected void gvCustomer_RowDataBound1(object sender, GridViewRowEventArgs e) {
        if (e.Row.Cells.Count <= 1) {
            return;
        }
        Label lblCustomertNo = (Label)e.Row.Cells[0].FindControl("lblCustomerNoByEdit");
        if (lblCustomertNo != null) {
            if (lblCustomertNo.Text != string.Empty) {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
                e.Row.Attributes.Add("onDblClick", Page.GetPostBackEventReference(this, e.Row.DataItemIndex.ToString()));
            }
        }
    }
}
