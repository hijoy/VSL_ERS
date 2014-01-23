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

public partial class Shop : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "门店维护");
            this.Page.Title = "门店维护";
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ShopManage, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ShopManage, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            this.HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            this.HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            if (!this.HasViewRight && !HasManageRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }

    protected bool HasViewRight {
        get {
            return (bool)this.ViewState["HasViewRight"];
        }
        set {
            this.ViewState["HasViewRight"] = value;
        }
    }

    protected bool HasManageRight {
        get {
            return (bool)this.ViewState["HasManageRight"];
        }
        set {
            this.ViewState["HasManageRight"] = value;
        }
    }

    #region datasource exception
    protected void odsShop_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        } else {

        }
    }

    protected void odsShop_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsShop_Deleted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }
    #endregion

    #region Shop event
    protected void gvShop_RowCommand(object sender, GridViewCommandEventArgs e) {
        if (e.CommandName.Equals("Edit") || !this.HasManageRight) {
            this.fvShop.Visible = false;
        } else {
            this.fvShop.Visible = true;
        }
    }
    protected void gvShop_RowDeleted(object sender, GridViewDeletedEventArgs e) {
        this.gvShop.SelectedIndex = -1;
        this.gvShop.EditIndex = -1;
    }
    protected void odsShop_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }
    protected void odsShop_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
        if (e.InputParameters["CustomerID"] == null) {
            PageUtility.ShowModelDlg(this, "请选择客户");
            e.Cancel = true;
        }
    }
    protected void odsShop_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    public String GetCustomerNameById(object id) {
        return new MasterDataBLL().GetCustomerById((int)id).CustomerName;
    }

    public String GetShopLevelNameById(object id) {
        return new MasterDataBLL().GetShopLevelById((int)id).ShopLevelName;
    }

    protected void lbtnSearch_Click(object sender, EventArgs e) {
        string searchStr = "1=1";
        string temp=this.txtShopNameBySearch.Text;
        if(temp!=string.Empty){
            searchStr += " and ShopName like '%"+temp+"%'";
        }
        temp = this.UCCustomerBySearch.CustomerID;
        if (!temp.Equals("")) {
            searchStr += " and CustomerID="+temp;
        }
        temp = this.dplShopLevelBySearch.SelectedValue;
        if (!temp.Equals("")) {
            searchStr += " and ShopLevelID=" + temp;
        }
        this.odsShop.SelectParameters["queryExpression"].DefaultValue = searchStr;
        this.gvShop.DataBind();
        this.upShop.Update();
    }
    #endregion

}
