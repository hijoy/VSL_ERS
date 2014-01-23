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

public partial class ProvinceCity : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "省份城市管理");
            this.Page.Title = "省份城市管理";
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Province, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Province, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            this.HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            this.HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            if (!this.HasViewRight && !HasManageRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        } else {
            PageUtility.CloseModelDlg(this);
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

    #region Province event
    protected void gvProvince_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.gvProvince.SelectedIndex >= 0) {
            this.gvCity.Visible = true;
            if(this.HasManageRight){
                this.fvCity.Visible = true;
            }else{
                this.fvCity.Visible = false;
            }
            this.gvCity.EditIndex = -1;
            this.gvCity.SelectedIndex = -1;

            this.odsCity.SelectParameters["ProvId"].DefaultValue = gvProvince.SelectedValue.ToString();
            this.gvCity.DataBind();
        } else {
            this.gvCity.Visible = false;
            this.fvCity.Visible = false;
        }
        this.upCity.Update();
    }

    protected void gvProvince_RowDeleted(object sender, GridViewDeletedEventArgs e) {
        this.gvProvince.SelectedIndex = -1;
        this.gvProvince.EditIndex = -1;
        this.gvProvince_SelectedIndexChanged(this.gvProvince, null);
    }

    protected void odsProvince_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    protected void odsProvince_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];

    }

    protected void odsProvince_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    protected void odsProvinc_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        } else {

        }
    }

    protected void odsProvinc_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    #endregion

    #region City event
    protected void odsCity_Selecting(object sender, ObjectDataSourceMethodEventArgs e) 
    {
        e.InputParameters["ProvId"] = gvProvince.SelectedValue; 
    }

    protected void odsCity_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
        e.InputParameters["ProvId"]= gvProvince.SelectedValue;     
    }
    protected void odsCity_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }
    protected void fvCity_ItemInserting(object sender, FormViewInsertEventArgs e) {
        e.Values["ProvId"] = gvProvince.SelectedValue;
    }
    protected void odsCity_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    protected void odsCity_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        } else {

        }
    }

    protected void odsCity_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }
    #endregion

}
