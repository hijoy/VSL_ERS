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

public partial class StuffUserManagePage : BasePage {

    protected string templetFilePath = System.Configuration.ConfigurationSettings.AppSettings["TempletFilePath"] + "\\StuffRight.xls";
    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];
    protected object missing = Missing.Value;

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "用户设定");
            this.Page.Title = "用户设定";
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

    }

    protected void SearchButton_Click(object sender, EventArgs e) {
        // 重新绑定，进行查询处理。
        StuffUserGridView.DataBind();

    }

    protected void StuffUserGridView_SelectedIndexChanged(object sender, EventArgs e) {
        // 将选中的“编码”传给 Formview, 在FormView中显示。
        if (StuffUserGridView.SelectedValue == null) {
            StuffUserAddObjectDataSource.SelectParameters["StuffUserId"].DefaultValue = "-1";

        } else {
            StuffUserAddObjectDataSource.SelectParameters["StuffUserId"].DefaultValue = StuffUserGridView.SelectedValue.ToString();
        }
        this.StuffUserFormView.DataBind();
        this.StuffUserAddUpdatePanel.Update();
    }

    protected void EditLinkButton_Click(object sender, EventArgs e) {
        // 将Formview状态更改为 Edit
        StuffUserFormView.ChangeMode(FormViewMode.Edit);
    }

    protected void AddLinkButton_Click(object sender, EventArgs e) {
        // 将Formview状态更改为 Insert
        StuffUserFormView.ChangeMode(FormViewMode.Insert);
    }

    protected void BackButton_Click(object sender, EventArgs e) {
        this.StuffUserAddObjectDataSource.SelectParameters["StuffUserId"].DefaultValue = "0";
    }

    protected void InsertButton_Click(object sender, EventArgs e) {
        StuffUserGridView.DataSourceID = "StuffUserObjectDataSource";
    }

    protected void StuffNameLinkButton_Click(object sender, EventArgs e) {
        this.OrganizationTreeView.Nodes.Clear();
        // 将Formview状态更改为 ReadOnly
        StuffUserFormView.ChangeMode(FormViewMode.ReadOnly);
    }

    protected void StuffUserFormView_ItemUpdating(object sender, FormViewUpdateEventArgs e) {

        if (!IsUpdateDetailValid()) {
            e.Cancel = true;
        }
        TextBox pwCtl = (TextBox)this.StuffUserFormView.FindControl("UserPasswordTextBox");

        if (!pwCtl.Text.Equals("!!!!!!")) {
            this.StuffUserAddObjectDataSource.UpdateParameters["UserPassword"].DefaultValue = FormsAuthentication.HashPasswordForStoringInConfigFile(pwCtl.Text.Trim(), "MD5");
        }
    }

    protected void StuffUserObjectDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.ReturnValue is bool && ((bool)e.ReturnValue) == true) {
            e.AffectedRows = 1;
            this.StuffUserGridView.SelectedIndex = -1;
            this.StuffUserGridView_SelectedIndexChanged(null, null);
        }
    }

    protected void PositionBtn_Click(object sender, EventArgs e) {
        //初始化组织结构树
        this.StuffUserPositionDS.SelectParameters["StuffUserId"].DefaultValue = this.StuffUserGridView.SelectedDataKey.Value.ToString();
        this.StuffUserPositionGridView.DataBind();
        this.InitTreeView((int)this.StuffUserFormView.DataKey.Value);
        this.PositionSetPanel.Style["display"] = "";
        this.OrganizationUpdatePanel.Update();
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
        //AuthorizationDS.StuffUserRow stuffUser = this.AuthBLL.GetStuffUserById(stuffUserId);
        //BusinessObjects.AuthorizationDS.StuffUserAndPositionRow[] stuffUserAndPositions = stuffUser.GetStuffUserAndPositionRows();
        List<String> positionIds = new List<String>();
        //foreach (AuthorizationDS.StuffUserAndPositionRow stuffUserAndPosition in stuffUserAndPositions) {
        //    positionIds.Add(stuffUserAndPosition.PositionId.ToString());
        //}
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
        int stuffUserId = (int)this.StuffUserFormView.DataKey.Value;
        //this.AuthBLL.Init();
        this.AuthBLL.SetStuffUserPosition(stuffUserId, positionIds.ToArray());
        this.StuffUserPositionGridView.DataBind();
        PageUtility.ShowModelDlg(this, "设置成功");

    }

    protected void StuffUserFormView_DataBound(object sender, EventArgs e) {
        this.PositionSetPanel.Style["display"] = "none";
        this.OrganizationUpdatePanel.Update();
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

    protected void StuffUserAddObjectDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        // 捕获异常
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            //StuffUserFormView.ChangeMode(FormViewMode.Edit);
            e.ExceptionHandled = true;
        } else {
            this.StuffUserGridView.DataBind();
            this.StuffUserUpdatePanel.Update();
        }
    }

    protected void StuffUserAddObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        // 捕获异常
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            //StuffUserFormView.ChangeMode(FormViewMode.Insert);
            e.ExceptionHandled = true;
        } else {
            this.StuffUserGridView.DataBind();
            this.StuffUserUpdatePanel.Update();
        }
    }

    protected void StuffUserFormView_ItemInserting(object sender, FormViewInsertEventArgs e) {
        if (!IsAddDetailValid()) {
            e.Cancel = true;
        }
        e.Values["UserPassword"] = FormsAuthentication.HashPasswordForStoringInConfigFile(e.Values["UserPassword"].ToString().Trim(), "MD5");
    }

    protected bool IsAddDetailValid() {

        TextBox UserNameTextBox = (TextBox)this.StuffUserFormView.FindControl("UserNameTextBox");
        if (UserNameTextBox.Text.Trim() != string.Empty) {
            StuffUserBLL bll = new StuffUserBLL();
            if ((int)bll.StuffUserAdapter.QueryForInsDistinct(UserNameTextBox.Text.Trim()) > 0) {
                PageUtility.ShowModelDlg(this, "登陆帐号重复，请修改！");
                return false;
            }
        }

        TextBox StuffIdTextBox = (TextBox)this.StuffUserFormView.FindControl("StuffIdTextBox");

        if (StuffIdTextBox.Text.Trim() != string.Empty) {
            StuffUserBLL bll = new StuffUserBLL();
            if ((int)bll.StuffUserAdapter.QueryForInsDistinctStuffID(StuffIdTextBox.Text.Trim()) > 0) {
                PageUtility.ShowModelDlg(this, "员工工号重复，请修改！");
                return false;
            }
        }

        TextBox pwCtl = (TextBox)this.StuffUserFormView.FindControl("UserPasswordTextBox");
        if (pwCtl.Text.Trim().Length < 6) {
            PageUtility.ShowModelDlg(this, "密码长度不能小于6");
            return false;
        }

        UserControls_UCDateInput UCNewAttendDate = (UserControls_UCDateInput)this.StuffUserFormView.FindControl("UCNewAttendDate");
        if (UCNewAttendDate.SelectedDate == string.Empty) {
            PageUtility.ShowModelDlg(this, "必须录入入职日期");
            return false;
        }

        return true;
    }

    protected bool IsUpdateDetailValid() {
        int stuffUserID = int.Parse(this.StuffUserFormView.DataKey["StuffUserId"].ToString());
        TextBox UserNameTextBox = (TextBox)this.StuffUserFormView.FindControl("UserNameTextBox");
        if (UserNameTextBox.Text.Trim() != string.Empty) {
            StuffUserBLL bll = new StuffUserBLL();
            if ((int)bll.StuffUserAdapter.QueryForUpdDistinct(stuffUserID, UserNameTextBox.Text.Trim()) > 0) {
                PageUtility.ShowModelDlg(this, "登陆帐号重复，请修改！");
                return false;
            }
        }

        TextBox StuffIdTextBox = (TextBox)this.StuffUserFormView.FindControl("StuffIdTextBox");
        if (StuffIdTextBox.Text.Trim() != string.Empty) {
            StuffUserBLL bll = new StuffUserBLL();
            if ((int)bll.StuffUserAdapter.QueryForUpdDistinctStuffID(StuffIdTextBox.Text.Trim(), stuffUserID) > 0) {
                PageUtility.ShowModelDlg(this, "员工工号重复，请修改！");
                return false;
            }
        }

        TextBox pwCtl = (TextBox)this.StuffUserFormView.FindControl("UserPasswordTextBox");
        if (pwCtl.Text.Trim().Length < 6) {
            PageUtility.ShowModelDlg(this, "密码长度不能小于6");
            return false;
        }

        UserControls_UCDateInput UCAttendDate = (UserControls_UCDateInput)this.StuffUserFormView.FindControl("UCAttendDate");
        if (UCAttendDate.SelectedDate == string.Empty) {
            PageUtility.ShowModelDlg(this, "必须录入入职日期");
            return false;
        }
        return true;
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

    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        if (this.StuffUserFormView.CurrentMode == FormViewMode.Edit) {
            TextBox pwCtl = (TextBox)this.StuffUserFormView.FindControl("UserPasswordTextBox");
            TextBox rpwCtl = (TextBox)this.StuffUserFormView.FindControl("UserPasswordsTextBox");
            pwCtl.Attributes["value"] = pwCtl.Text;
            rpwCtl.Attributes["value"] = rpwCtl.Text;
        }
    }
}
