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

public partial class Dialog_ShopSearch : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        this.Page.Title = "客户查询";
    }

    #region Shop event

    protected void lbtnSearch_Click(object sender, EventArgs e) {
        string searchStr = "1=1";
        string temp = this.txtShopNameBySearch.Text;
        if (temp != string.Empty) {
            searchStr += " and ShopName like '%" + temp + "%'";
        }
        temp = this.txtCustomerNameBySearch.Text;
        if (temp != string.Empty) {
            searchStr += " and CustomerName like '%" + temp + "%'";
        }

        this.odsShop.SelectParameters["queryExpression"].DefaultValue = searchStr;
        this.gvShop.DataBind();
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
        gvShop.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string gvShopID = gvShop.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label lblShopName = (Label)gvShop.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].Cells[0].FindControl("lblShopNameByEdit");
        string gvShopName = lblShopName.Text;
        string returnValue = gvShopName;
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + gvShopID + "';" + "arryReturn[1]='" + returnValue + "';" +
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
    protected void gvShop_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.Cells.Count <= 1) {
            return;
        }
        Label lblCustomertNo = (Label)e.Row.Cells[0].FindControl("lblShopNameByEdit");
        if (lblCustomertNo != null) {
            if (lblCustomertNo.Text != string.Empty) {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
                e.Row.Attributes.Add("onDblClick", Page.GetPostBackEventReference(this, e.Row.DataItemIndex.ToString()));
            }
        }
    }
    #endregion
    
}
