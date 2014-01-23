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

public partial class SKUCategory : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "产品类型管理");
            this.Page.Title = "产品类型管理";
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SKUCategory, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SKUCategory, SystemEnums.OperateEnum.Manage);
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

    #region SKUCategory event
    protected void odsSKUCategory_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["UserID"] = ((AuthorizationDS.StuffUserRow)this.Session["StuffUser"]).StuffUserId;
        e.InputParameters["PositionID"] = ((AuthorizationDS.PositionRow)this.Session["Position"]).PositionId;
    }
    protected void odsSKUCategory_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
    }
    protected void odsSKUCategory_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["UserID"] = ((AuthorizationDS.StuffUserRow)this.Session["StuffUser"]).StuffUserId;
        e.InputParameters["PositionID"] = ((AuthorizationDS.PositionRow)this.Session["Position"]).PositionId;
    }

    #endregion

    public string GetUserNameByID(object UserID) {
        int id = Convert.ToInt32(UserID);
        return new StuffUserBLL().GetStuffUserById(id)[0].StuffName;
    }

    public string GetPositionNameByID(object PositionID) {
        int id = Convert.ToInt32(PositionID);
        return new OUTreeBLL().GetPositionById(id).PositionName;
    }
}
