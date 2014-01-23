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
using BusinessObjects.AuthorizationDSTableAdapters;

public partial class AuthorizationManage_PositionTypeManage : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender,e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "流程角色管理");
            this.Page.Title = "流程角色管理";
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.PositionType, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            if (!positionRightBLL.CheckPositionRight(position.PositionId, opManageId)) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }



    protected void odsPositionType_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {

        TextBox PositionTypeName = (TextBox)this.fvPositionType.FindControl("txtPositionTypeName");
        TextBox PositionTypeCode = (TextBox)this.fvPositionType.FindControl("txtPositionTypeCode");
        if (Validation(PositionTypeName.Text.Trim(' '), PositionTypeCode.Text.Trim(' '), "")) {
            e.InputParameters["PositionTypeName"] = PositionTypeName.Text.Trim(' ');
            e.InputParameters["PositionTypeCode"] = PositionTypeCode.Text.Trim(' ');
        } else {
            e.Cancel = true;
            
        }
    }
    protected void odsPositionType_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        TextBox PositionTypeName = (TextBox)this.gvCustomerType.Rows[gvCustomerType.EditIndex].FindControl("txtPositionTypeName");
        TextBox PositionTypeCode = (TextBox)this.gvCustomerType.Rows[gvCustomerType.EditIndex].FindControl("txtPositionTypeCode");
        if (Validation(PositionTypeName.Text.Trim(' '), PositionTypeCode.Text.Trim(' '), gvCustomerType.DataKeys[gvCustomerType.EditIndex].Value.ToString())) {
            e.InputParameters["PositionTypeName"] = PositionTypeName.Text.Trim(' ');
            e.InputParameters["PositionTypeCode"] = PositionTypeCode.Text.Trim(' ');
        } else {
            e.Cancel = true;
        }
    }

    private bool Validation(string PositionTypeName, string PositionTypeCode,string PositionTypeID) {

        if (string.IsNullOrEmpty(PositionTypeName)) {
            PageUtility.ShowModelDlg(this.Page, "流程角色名称不能为空");

            return false;
        }
        if (string.IsNullOrEmpty(PositionTypeCode)) {
            PageUtility.ShowModelDlg(this.Page, "流程角色代码不能为空");

            return false;
           
        }
        BusinessObjects.AuthorizationDS.PositionTypeDataTable dt = new AuthorizationDS.PositionTypeDataTable();
        PositionTypeTableAdapter PositionTypeTA = new PositionTypeTableAdapter();
        string QueryExpressionByPositionTypeName = "PositionTypeName='" + PositionTypeName + "'";
        if (!string.IsNullOrEmpty(PositionTypeID)) {

            QueryExpressionByPositionTypeName += "and PositionTypeId<>'" + PositionTypeID + "'";
        }
        dt = PositionTypeTA.GetDataByQueryExpression("PositionType", "", 0, 10, QueryExpressionByPositionTypeName);
        if (dt != null && dt.Count > 0) {
            PageUtility.ShowModelDlg(this.Page, "流程角色名称不能重复");
            return false;
          
        }

        string QueryExpressionByPositionTypeCode = "PositionTypeCode='" + PositionTypeCode + "'";
        if (!string.IsNullOrEmpty(PositionTypeID)) {

            QueryExpressionByPositionTypeCode += "and PositionTypeId<>'" + PositionTypeID + "'";
        }
        
        dt = new PositionTypeTableAdapter().GetDataByQueryExpression("PositionType", "", 0, 10, QueryExpressionByPositionTypeCode);
        if (dt != null && dt.Count > 0) {
            PageUtility.ShowModelDlg(this.Page, "流程角色代码不能重复");
            return false;  
        }

        return true;
        
    }
}