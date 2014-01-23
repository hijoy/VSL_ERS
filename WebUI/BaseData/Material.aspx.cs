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

public partial class Material : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "物资管理");
            this.Page.Title = "物资管理";
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Material, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Material, SystemEnums.OperateEnum.Manage);
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

    #region Material event


    protected void lbtnSearch_Click(object sender, EventArgs e) {
        string searchStr = "1=1";
        string temp = this.txtNameBySearch.Text;
        if (!temp.Equals("")) {
            searchStr += " and MaterialName like '%" + temp + "%'";
        }
        temp = this.txtMaterialNoBySearch.Text;
        if (!temp.Equals("")) {
            searchStr += " and MaterialNo like '%" + temp + "%'";
        }
        temp = this.txtPriceBegin.Text;
        if (!temp.Equals("")) {
            try {
                decimal.Parse(temp);
            } catch (Exception) {
                PageUtility.ShowModelDlg(this, "起始价格必须为数字！");
                return;
            }
            searchStr += " and MaterialPrice >= " + temp;
        }
        temp = this.txtPriceEnd.Text;
        if (!temp.Equals("")) {
            try {
                decimal.Parse(temp);
            } catch (Exception) {
                PageUtility.ShowModelDlg(this, "起始价格必须为数字！");
                return;
            }
            searchStr += " and MaterialPrice <= " + temp;
        }
        this.odsMaterial.SelectParameters["queryExpression"].DefaultValue = searchStr;
        this.gvMaterial.DataBind();
        this.upMaterial.Update();
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
