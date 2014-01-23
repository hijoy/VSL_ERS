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

public partial class Dialog_SKUSearch : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            this.Page.Title = "产品查询";
            String searchStr = "1=1";
            searchStr += " and IsActive = 1";
            this.odsSKU.SelectParameters["queryExpression"].DefaultValue = searchStr;
            this.gvSKU.DataBind();
        }
    }

    protected override void OnPreRender(EventArgs e) {
        txtSKUNo.Attributes.Add("onkeypress", "EnterTextBox(event);");
    }

    #region SKU event

    protected void lbtnSearch_Click(object sender, EventArgs e) {
        string searchStr = "1=1";
        string temp = this.txtSKUNo.Text;
        if (!temp.Equals("")) {
            searchStr += " and SKUNo like '%" + temp + "%'";
        }
        temp = this.txtSKUName.Text;
        if (!temp.Equals("")) {
            searchStr += " and SKUName like '%" + temp + "%'";
        }
        temp = this.dplSKUCategory.SelectedValue;
        if (!temp.Equals("")) {
            searchStr += " and SKUCategoryID = " + temp;
        }
        searchStr += "and IsActive = 'true'";
        this.odsSKU.SelectParameters["queryExpression"].DefaultValue = searchStr;
        this.gvSKU.DataBind();
    }

    protected void gvSKU_RowDataBound1(object sender, GridViewRowEventArgs e) {
        Label lblSKUNo = (Label)e.Row.Cells[0].FindControl("lbtnSKUNoByEdit");
        if (lblSKUNo != null) {
            if (lblSKUNo.Text != string.Empty) {
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
        gvSKU.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string gvSKUID = gvSKU.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label lblSKUNo = (Label)gvSKU.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].Cells[0].FindControl("lbtnSKUNoByEdit");
        string gvSKUNo = lblSKUNo.Text;
        Label lblSKUName = (Label)gvSKU.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].Cells[0].FindControl("lblSKUNameByEdit");
        string gvSKUName = lblSKUName.Text;
        string returnValue = gvSKUNo + "-" + gvSKUName;
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + gvSKUID + "';" + "arryReturn[1]='" + returnValue + "';" +
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

    public string GetSKUCateNameById(object Id) {
        return new MasterDataBLL().GetSKUCategoryById((int)Id).SKUCategoryName;
    }

    #endregion
}
