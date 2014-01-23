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

public partial class Customer : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "客户维护");
            this.Page.Title = "客户维护";
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Customer, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Customer, SystemEnums.OperateEnum.Manage);
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

    protected void odsCustTimesLimit_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsCustTimesLimit_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsCustTimesLimit_Deleted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsCustAmountLimit_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsCustAmountLimit_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsCust_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsCust_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    #endregion

    #region Customer event
    protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e) {
        if (e.CommandName.Equals("Edit") || !this.HasManageRight) {
            this.fvCustomer.Visible = false;
        } else {
            this.fvCustomer.Visible = true;
        }
    }

    protected void gvCustomer_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.gvCustomer.SelectedIndex >= 0) {
            this.CustAmountLimitGridView.Visible = true;
            this.CustTimesLimitGridView.Visible = true;
            if (this.HasManageRight) {
                this.CustAmountLimitFormView.Visible = true;
                this.fvCustTimesLimit.Visible = true;
            } else {
                this.CustAmountLimitFormView.Visible = false;
                this.fvCustTimesLimit.Visible = false;
            }
            this.CustAmountLimitGridView.EditIndex = -1;
            this.CustAmountLimitGridView.SelectedIndex = -1;
            this.odsCustAmountLimit.SelectParameters["CustId"].DefaultValue = gvCustomer.SelectedValue.ToString();
            this.CustAmountLimitGridView.DataBind();

            this.CustTimesLimitGridView.EditIndex = -1;
            this.CustTimesLimitGridView.SelectedIndex = -1;
            this.odsCustTimesLimit.SelectParameters["CustId"].DefaultValue = gvCustomer.SelectedValue.ToString();
            this.CustTimesLimitGridView.DataBind();
        } else {
            this.CustAmountLimitGridView.Visible = false;
            this.CustTimesLimitGridView.Visible = false;
        }
        this.CustAmountLimitUpdatePanel.Update();
        this.CustTimesLimitUpdatePanel.Update();
    }

    protected void odsCustomer_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    protected void odsCustomer_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    protected void odsCustomer_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    public String GetCityNameById(object id) {
        return new MasterDataBLL().GetCityNameById((int)id);
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
    #endregion

    #region CustAmountLimit event
    protected void odsCustAmountLimit_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }
    protected void odsCustAmountLimit_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
        e.InputParameters["CustId"] = this.gvCustomer.SelectedValue;
    }
    protected void odsCustAmountLimit_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }
    #endregion

    #region CustTimesLimit event
    protected void CustTimesLimitGridView_RowCommand(object sender, GridViewCommandEventArgs e) {
        if (e.CommandName.Equals("Edit") || !this.HasManageRight) {
            //this.Visible = false;
        } else {
            //this.ShopFormView.Visible = true;
        }
    }

    protected void odsCustTimesLimit_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    protected void odsCustTimesLimit_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
        e.InputParameters["CustId"] = this.gvCustomer.SelectedValue;
    }

    protected void odsCustTimesLimit_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
        e.InputParameters["CustId"] = this.gvCustomer.SelectedValue;
    }

    public string GetExpenseItemNameById(object Id) {
        //return new MasterDataBLL().ExpenseItemAdapter.GetDataById((int)Id)[0].ExpenseItemName;
        ERS.ExpenseItemRow row = new MasterDataBLL().ExpenseItemAdapter.GetDataById((int)Id)[0];
        return row.AccountingCode + "--" + row.AccountingName;
    }

    #endregion

    protected void lbtnSearch_Click(object sender, EventArgs e) {
        this.CustAmountLimitGridView.Visible = false;
        this.CustTimesLimitGridView.Visible = false;
        this.CustAmountLimitFormView.Visible = false;
        this.fvCustTimesLimit.Visible = false;

        String searchStr = "1=1";
        String temp = this.txtCustNoBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CustomerNo like '%" + temp + "%'";
        }
        temp = this.txtCustNameBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CustomerName like '%" + temp + "%'";
        }
        temp = this.dplCustCityBySearch.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CityID = " + temp;
        }
        temp = this.UCOUBySearch.OUId.ToString();
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and OrganizationUnitID = " + temp;
        }
        temp = this.dplCustActiveBySearch.SelectedValue;
        if (temp != "3") {
            searchStr += " and IsActive = " + temp;
        }
        this.odsCustomer.SelectParameters["queryExpression"].DefaultValue = searchStr;
        this.gvCustomer.DataBind();
        this.upCustomer.Update();
        this.CustTimesLimitUpdatePanel.Update();
        this.CustAmountLimitUpdatePanel.Update();
    }
}
