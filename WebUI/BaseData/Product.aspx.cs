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

public partial class Product : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "产品维护");
            this.Page.Title = "产品维护";
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SKU, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SKU, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            this.HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            this.HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            if (!this.HasViewRight && !HasManageRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
            if (!this.HasManageRight) {
                this.gvSKU.Columns[9].Visible = false;
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

    protected void odsSKUPrice_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsSKUPrice_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsSKUPrice_Deleted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    #endregion

    #region SKU event

    protected void gvSKU_RowDeleted(object sender, GridViewDeletedEventArgs e) {
        this.gvSKU.SelectedIndex = -1;
        this.gvSKU.EditIndex = -1;
    }

    protected void odsSKU_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    protected void odsSKU_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    protected void odsSKU_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

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
        temp = this.dplSKUActive.SelectedValue;
        if (!temp.Equals("3")) {
            searchStr += " and IsActive = " + temp;
        }
        this.odsSKU.SelectParameters["queryExpression"].DefaultValue = searchStr;
        this.gvSKU.DataBind();
        this.upSKU.Update();
    }

    public string GetSKUCateNameById(object Id) {
        return new MasterDataBLL().GetSKUCategoryById((int)Id).SKUCategoryName;
    }

    protected void gvSKU_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.gvSKU.SelectedIndex >= 0) {
            this.gvSKUPrice.Visible = true;
            if (this.HasManageRight) {
                this.fvSKUPrice.Visible = true;
            } else {
                this.fvSKUPrice.Visible = false;
            }
            this.gvSKUPrice.EditIndex = -1;
            this.gvSKUPrice.SelectedIndex = -1;

            this.odsSKUPrice.SelectParameters["SKUId"].DefaultValue = gvSKU.SelectedValue.ToString();
            this.gvSKUPrice.DataBind();
        } else {
            this.gvSKUPrice.Visible = false;
            this.fvSKUPrice.Visible = false;
        }
        this.upSKUPrice.Update();
    }

    #endregion

    #region SKUPrice event
    protected void odsSKUPrice_Selecting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["SKUId"] = gvSKU.SelectedValue;
    }

    protected void odsSKUPrice_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["UserID"] = ((AuthorizationDS.StuffUserRow)this.Session["StuffUser"]).StuffUserId;
        e.InputParameters["PositionID"] = ((AuthorizationDS.PositionRow)this.Session["Position"]).PositionId;
        e.InputParameters["SKUId"] = gvSKU.SelectedValue;
    }
    protected void odsSKUPrice_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["UserID"] = ((AuthorizationDS.StuffUserRow)this.Session["StuffUser"]).StuffUserId;
        e.InputParameters["PositionID"] = ((AuthorizationDS.PositionRow)this.Session["Position"]).PositionId;
        e.InputParameters["SKUId"] = gvSKU.SelectedValue;
    }
    protected void fvSKUPrice_ItemInserting(object sender, FormViewInsertEventArgs e) {
        e.Values["SKUId"] = gvSKU.SelectedValue;
    }
    protected void odsSKUPrice_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["UserID"] = ((AuthorizationDS.StuffUserRow)this.Session["StuffUser"]).StuffUserId;
        e.InputParameters["PositionID"] = ((AuthorizationDS.PositionRow)this.Session["Position"]).PositionId;
    }
    public string GetCustTypeNameById(object Id) {
        return new MasterDataBLL().GetCustomerTypeById((int)Id).CustomerTypeName;
    }
    #endregion
}
