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
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;

public partial class PositionManagePage : BasePage {

    protected object missing = Missing.Value;

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "用户职务设定");
            this.Page.Title = "用户职务设定";
            int opUserManageViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.UseCase.UserManage, SystemEnums.OperateEnum.View);
            int opUserManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.UseCase.UserManage, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            this.HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opUserManageViewId);
            this.HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opUserManageId);

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

    protected void StuffUserObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e) {

        string tmpSQL = "";

        if (UserAccountTextBox.Text.Trim() != string.Empty) {
            if (tmpSQL.Equals(string.Empty)) {
                tmpSQL = "UserName like '%" + UserAccountTextBox.Text + "%'";
            } else {
                tmpSQL += " AND UserName like '%" + UserAccountTextBox.Text + "%'";
            }
        }
        if (StuffNameTextBox.Text != "") {
            if (tmpSQL.Equals(string.Empty)) {
                tmpSQL = "StuffName like '%" + StuffNameTextBox.Text + "%'";
            } else {
                tmpSQL += " AND StuffName like '%" + StuffNameTextBox.Text + "%'";
            }
        }

        if (EmployeeNoTextBox.Text != "") {
            if (tmpSQL.Equals(string.Empty)) {
                tmpSQL = "StuffId like '%" + EmployeeNoTextBox.Text + "%'";
            } else {
                tmpSQL += " AND StuffId like '%" + EmployeeNoTextBox.Text + "%'";
            }
        }

        if (EmailTextBox.Text != "") {
            if (tmpSQL.Equals(string.Empty)) {
                tmpSQL = "EMail like '%" + EmailTextBox.Text + "%'";
            } else {
                tmpSQL += " AND EMail like '%" + EmailTextBox.Text + "%'";
            }
        }

        if (TelTextBox.Text != "") {
            if (tmpSQL.Equals(string.Empty)) {
                tmpSQL = "Telephone like '%" + TelTextBox.Text + "%'";
            } else {
                tmpSQL += " AND Telephone like '%" + TelTextBox.Text + "%'";
            }
        }

        e.InputParameters["queryExpression"] = tmpSQL;
        this.PositionSetPanel.Style["display"] = "none";

    }
    protected void SearchButton_Click(object sender, EventArgs e) {
        // 重新绑定，进行查询处理。
        StuffUserGridView.DataBind();
    }

    protected void StuffUserGridView_SelectedIndexChanged(object sender, EventArgs e) {
        if (StuffUserGridView.SelectedValue == null) {
            StuffUserPositionDS.SelectParameters["StuffUserId"].DefaultValue = "-1";

        } else {
            this.StuffUserPositionDS.SelectParameters["StuffUserId"].DefaultValue = this.StuffUserGridView.SelectedDataKey.Value.ToString();
            this.StuffUserPositionGridView.DataBind();
            this.InitTreeView((int)this.StuffUserGridView.SelectedDataKey.Value);
            this.PositionSetPanel.Style["display"] = "";
        }
    }

    AuthorizationBLL m_AuthBLL;
    private AuthorizationBLL AuthBLL {
        get {
            if (m_AuthBLL == null) {
                m_AuthBLL = new AuthorizationBLL();
            }
            return m_AuthBLL;
        }
    }

    private void InitTreeView(int stuffUserId) {
        this.OrganizationTreeView.Nodes.Clear();
        foreach (AuthorizationDS.OrganizationUnitRow organizationUnit in new OUTreeBLL().GetRootOrganizationUnits()) {
            this.OrganizationTreeView.Nodes.Add(CreateOUTreeNode(organizationUnit));
        }

        AuthorizationDS.PositionDataTable positions = this.AuthBLL.GetPositionByStuffUser(stuffUserId);
        List<String> positionIds = new List<String>();
        foreach (AuthorizationDS.PositionRow position in positions) {
            positionIds.Add(position.PositionId.ToString());
        }
        foreach (TreeNode node in this.OrganizationTreeView.Nodes) {
            CheckTreeNode(node, positionIds);
        }

    }

    private void CheckTreeNode(TreeNode node, List<string> positionIds) {
        foreach (TreeNode childNode in node.ChildNodes) {
            CheckTreeNode(childNode, positionIds);
        }
        if (node.Value.StartsWith("PO") && positionIds.Contains(node.Value.Substring(2))) {
            node.Checked = true;
        } else {
            node.Checked = false;
        }
    }

    private void SetTreeNode(TreeNode treeNode, AuthorizationDS.OrganizationUnitRow ou) {
        treeNode.Text = ou.OrganizationUnitName;
        treeNode.Value = "OU" + ou.OrganizationUnitId;
        treeNode.ShowCheckBox = false;
        treeNode.ImageUrl = "../Images/department.png";
    }

    private void SetTreeNode(TreeNode treeNode, AuthorizationDS.PositionRow position) {
        treeNode.Text = position.PositionName;
        treeNode.Value = "PO" + position.PositionId.ToString();
        treeNode.ShowCheckBox = true;
        treeNode.ImageUrl = "../Images/post.png";
    }

    private TreeNode NewTreeNode(AuthorizationDS.PositionRow position) {
        TreeNode treeNode = new TreeNode();
        this.SetTreeNode(treeNode, position);
        return treeNode;
    }

    private TreeNode NewTreeNode(AuthorizationDS.OrganizationUnitRow ou) {
        TreeNode treeNode = new TreeNode();
        this.SetTreeNode(treeNode, ou);
        return treeNode;
    }

    private TreeNode CreateOUTreeNode(AuthorizationDS.OrganizationUnitRow organizationUnit) {
        TreeNode newNode = NewTreeNode(organizationUnit);
        foreach (AuthorizationDS.OrganizationUnitRow subOU in organizationUnit.GetChildren()) {
            newNode.ChildNodes.Add(CreateOUTreeNode(subOU));
        }
        foreach (AuthorizationDS.PositionRow position in organizationUnit.GetPositionRows()) {
            newNode.ChildNodes.Add(NewTreeNode(position));
        }
        return newNode;
    }

    protected void SavePositionBtn_Click(object sender, EventArgs e) {
        //获取所选职位
        List<int> positionIds = new List<int>();
        foreach (TreeNode node in OrganizationTreeView.CheckedNodes) {
            positionIds.Add(int.Parse(node.Value.Substring(2)));
        }
        int stuffUserId = (int)this.StuffUserGridView.SelectedDataKey.Value;
        //this.AuthBLL.Init();
        this.AuthBLL.SetStuffUserPosition(stuffUserId, positionIds.ToArray());
        this.StuffUserPositionGridView.DataBind();
        this.StuffUserGridView.DataBind();
        PageUtility.ShowModelDlg(this, "设置成功");

    }

    protected void StuffUserPositionGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            BusinessObjects.AuthorizationDS.PositionRow position = (BusinessObjects.AuthorizationDS.PositionRow)drv.Row;
            List<AuthorizationDS.OrganizationUnitRow> ous = new OUTreeBLL().GetParentOUsOfPosition(position.PositionId);

            StringBuilder ouNames = new StringBuilder();
            foreach (BusinessObjects.AuthorizationDS.OrganizationUnitRow ou in ous) {
                ouNames.Append(ou.OrganizationUnitName + "-");
            }
            Label label = (Label)e.Row.FindControl("ParentOUNamesCtl");
            label.Text = ouNames.ToString();
        }
    }

    protected void StuffUserPositionGridView_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.StuffUserPositionGridView.SelectedValue != null) {
            int positionId = (int)this.StuffUserPositionGridView.SelectedDataKey.Value;
            PageUtility.SelectTreeNodeByNodeValue(this.OrganizationTreeView, "PO" + positionId);
        }
    }

    protected void StuffUserGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            StringBuilder positions = new StringBuilder();
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)((DataRowView)e.Row.DataItem).Row;
            foreach (AuthorizationDS.PositionRow position in this.AuthBLL.GetPositionByStuffUser(stuffUser.StuffUserId)) {
                if (positions.Length > 0) {
                    positions.Append(",");
                }
                positions.Append(position.PositionName);
            }

            Label positionsLabel = (Label)e.Row.FindControl("PositionsLabel");
            positionsLabel.Text = positions.ToString();
        }
    }
}
