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
using System.Collections.Generic;
using BusinessObjects;

public partial class AuthorizationManage_PositionRightManage : BasePage {
    AuthorizationBLL m_AuthBLL;
    private AuthorizationBLL AuthBLL {
        get {
            if (m_AuthBLL == null) {
                m_AuthBLL = new AuthorizationBLL();
            }
            return m_AuthBLL;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "职务权限设置");
            this.Page.Title = "职务权限设置";

            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.UseCase.PositionAuthorization, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.UseCase.PositionAuthorization, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool hasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            bool hasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);

            if (!hasViewRight && !hasManageRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
            if(!hasManageRight){
                this.SetPositionRightBtn.Visible = false;
            }

            OUTreeUtility.InitOUTree(this.OrganizationTreeView,true,false, false, false, true,false);
        }
    }


   
    protected void OrganizationTreeView_SelectedNodeChanged(object sender, EventArgs e) {
        if (this.OrganizationTreeView.SelectedNode != null) {
            TreeNode treeNode = this.OrganizationTreeView.SelectedNode;
            string id = treeNode.Value;
            if (treeNode.Value.StartsWith("OU")) {
                ClearSystemRoleGridViewSelection();
                this.SetPositionRightBtn.Enabled = false;
            } else {
                SetSystemRoleGridViewSelection(int.Parse(id.Substring(2)));
                this.SetPositionRightBtn.Enabled = true;
            }
        } else {
            ClearSystemRoleGridViewSelection();
            this.SetPositionRightBtn.Enabled = false;
        }
        this.RoleUpdatePanel.Update();
    }

    private void SetSystemRoleGridViewSelection(int positionId) {
        BusinessObjects.AuthorizationDS.SystemRoleDataTable roles = this.AuthBLL.GetSystemRoleByPostion(positionId);
        List<int> roleIds = new List<int>();
        foreach (BusinessObjects.AuthorizationDS.SystemRoleRow role in roles) {
            roleIds.Add(role.SystemRoleId);
        }

        foreach (GridViewRow row in this.SystemRoleGridView.Rows) {
            CheckBox checkBox = (CheckBox)row.FindControl("SystemRoleCheckBox");
            int roleId = (int)this.SystemRoleGridView.DataKeys[row.RowIndex].Value;
            if (roleIds.Contains(roleId)) {
                checkBox.Checked = true;
            } else {
                checkBox.Checked = false;
            }
        }
    }

    private void ClearSystemRoleGridViewSelection() {
        foreach (GridViewRow row in this.SystemRoleGridView.Rows) {
            CheckBox checkBox = (CheckBox)row.FindControl("SystemRoleCheckBox");
            checkBox.Checked = false;
        }
    }
    protected void SetPositionRightBtn_Click(object sender, EventArgs e) {
        List<int> roleIds = new List<int>();
        foreach (GridViewRow row in this.SystemRoleGridView.Rows) {
            CheckBox checkBox = (CheckBox)row.FindControl("SystemRoleCheckBox");
            if (checkBox.Checked) {
                int roleId = (int)this.SystemRoleGridView.DataKeys[row.RowIndex].Value;
                roleIds.Add(roleId);
            }
        }
        TreeNode treeNode = this.OrganizationTreeView.SelectedNode;
        int positionId = int.Parse(treeNode.Value.Substring(2));
        this.AuthBLL.SetPositionSystemRole((AuthorizationDS.StuffUserRow)Session["StuffUser"],(AuthorizationDS.PositionRow)Session["Position"], positionId, roleIds.ToArray());
        PageUtility.ShowModelDlg(this, "设置成功");        
    }
}
