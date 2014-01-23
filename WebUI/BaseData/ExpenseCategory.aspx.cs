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

public partial class ExpenseCategory : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "费用类型维护");
            this.Page.Title = "费用类型维护";
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ExpenseCategory, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ExpenseCategory, SystemEnums.OperateEnum.Manage);
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

    #region Category event

    protected void gvCategory_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.gvCategory.SelectedIndex >= 0) {
            this.gvSubCategory.Visible = true;
            if (this.HasManageRight) {
                this.fvSubCategory.Visible = true;
            } else {
                this.fvSubCategory.Visible = false;
            }
            this.gvSubCategory.EditIndex = -1;
            this.gvSubCategory.SelectedIndex = -1;

            this.odsSubCategory.SelectParameters["CateId"].DefaultValue = gvCategory.SelectedValue.ToString();
            this.gvSubCategory.DataBind();
        } else {
            this.gvSubCategory.Visible = false;
            this.fvSubCategory.Visible = false;
        }
        this.upSubCategory.Update();
    }

    protected void gvCategory_RowDeleted(object sender, GridViewDeletedEventArgs e) {
        this.gvCategory.SelectedIndex = -1;
        this.gvCategory.EditIndex = -1;
        this.gvCategory_SelectedIndexChanged(this.gvCategory, null);
    }

    protected void odsCategory_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    protected void odsCategory_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];

    }

    protected void odsCategory_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    #endregion

    #region SubCategory event

    protected void odsSubCategory_Selecting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["CateId"] = gvCategory.SelectedValue;
    }

    protected void odsSubCategory_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
        e.InputParameters["CateId"] = gvCategory.SelectedValue;
    }

    protected void odsSubCategory_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
        e.InputParameters["CateId"] = gvCategory.SelectedValue;
    }

    protected void fvSubCategory_ItemInserting(object sender, FormViewInsertEventArgs e) {
        e.Values["CateId"] = gvCategory.SelectedValue;
    }

    protected void odsSubCategory_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }

    public String getPageType(Object pageType) {
        int pageTypeId = int.Parse(pageType.ToString());
        if (pageTypeId == (int)SystemEnums.PageType.RebateApply) {
            return "返利";
        } else if (pageTypeId == (int)SystemEnums.PageType.GeneralApply) {
            return "非价量相关";
        } else if (pageTypeId == (int)SystemEnums.PageType.PromotionApply) {
            return "价量相关";
        } else {
            return "";
        }
    }

    #endregion
}
