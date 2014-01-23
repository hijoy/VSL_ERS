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

public partial class AuthorizationManage_ProxyBusiness : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = "个人费用报销代理设置";
            PageUtility.SetContentTitle(this, title);

            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ProxyBusiness, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            if (!new PositionRightBLL().CheckPositionRight(position.PositionId, opManageId)) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            this.odsProxyBusiness.SelectParameters["UserID"].DefaultValue = stuffuserID.ToString();
        }
    }

    #region ProxyBusiness event

    public string GetUserNameByID(object ID) {
        return new AuthorizationBLL().GetStuffUserById((int)ID).StuffName;
    }

    public string GetPositionNameByID(object ID) {
        return new BusinessObjects.AuthorizationDSTableAdapters.PositionTableAdapter().GetDataById((int)ID)[0].PositionName;
    }

    protected void odsProxyBusiness_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["UserID"] = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
        e.InputParameters["PositionID"] = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId;
    }

    protected void odsProxyBusiness_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    #endregion

}
