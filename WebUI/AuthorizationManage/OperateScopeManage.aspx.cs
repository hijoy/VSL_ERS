using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessObjects;
using BusinessObjects.AuthorizationDSTableAdapters;

public partial class AuthorizationManage_OperateScope : BasePage {

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
        base.Page_Load(sender,e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "职务业务操作范围设置");
            this.Page.Title = "职务业务操作范围设置";

            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.UseCase.OperateScope, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.UseCase.OperateScope, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool hasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);

            if (!hasViewRight && !HasManageRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
            if (!HasManageRight) {
                this.SetScopeBtn.Visible = false;
            }

            OUTreeUtility.InitOUTree(this.PositionTreeView,true,false, false,false,true,false);
            OUTreeUtility.InitOUTree(this.ScopeTreeView,false, true, true, true, true,false);            
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
    
    protected void PositionTreeView_SelectedNodeChanged(object sender, EventArgs e) {
        if (this.PositionTreeView.SelectedNode != null) {
            this.OperateUpdatePanel.Visible = true;
            //this.ScopeUpdatePanel.Visible = true;
            this.ScopeDIV.Visible = true;
            this.ScopeListUpdatePanel.Visible = true;
            this.ViewState["CheckedOperateIds"] = null;
            this.OperateDS.SelectParameters["PositionId"].DefaultValue = this.PositionTreeView.SelectedNode.Value.Substring(2);
            this.OperateGridView.DataBind();
            //this.OperateGridView.Sort("BusinessUseCaseName", SortDirection.Ascending);
        } else {
            this.OperateUpdatePanel.Visible = false;
            //this.ScopeUpdatePanel.Visible = false;
            this.ScopeDIV.Visible = false;
            this.ScopeListUpdatePanel.Visible = false;
        }
    }

    #region ScopeTreeView Operate

    private void SetScopeTreeView() {
        int operateId = (int)this.OperateGridView.SelectedValue;
        int positionId = int.Parse(this.PositionTreeView.SelectedNode.Value.Substring(2));
        List<string> nodeValues = new List<string>();
        BusinessObjects.AuthorizationDS.OperateScopeDataTable table = new OperateScopeTableAdapter().GetDataByOperateAndPosition(operateId, positionId);
        foreach (BusinessObjects.AuthorizationDS.OperateScopeRow row in table) {
            if (row.IsScopeOrganziationUnitIdNull()) {
                nodeValues.Add("PO" + row.ScopePositionId);
            } else {
                nodeValues.Add("OU" + row.ScopeOrganziationUnitId);
            }
        }
        foreach (TreeNode node in this.ScopeTreeView.Nodes) {
            CheckScopeTreeNode(node, nodeValues);
        }
    }

    private void CheckScopeTreeNode(TreeNode node, List<string> nodeValues) {
        foreach (TreeNode childNode in node.ChildNodes) {
            CheckScopeTreeNode(childNode, nodeValues);
        }
        if (nodeValues.Contains(node.Value)) {
            node.Checked = true;
        } else {
            node.Checked = false;
        }
    }

    private void ClearScopeTreeView() {
        List<TreeNode> checkedNodes = new List<TreeNode>();
        foreach (TreeNode node in this.ScopeTreeView.CheckedNodes) {
            checkedNodes.Add(node);
        }
        foreach (TreeNode checkedNode in checkedNodes) {
            checkedNode.Checked = false;
        }
    }

    protected void SetScopeBtn_Click(object sender, EventArgs e) {
        List<int> selectedOperateIds = this.GetCheckedOperateIds();
        if (selectedOperateIds.Count == 0) {
            PageUtility.ShowModelDlg(this, "请选择业务操作，再设置操作范围");
            return;
        }
        this.ValidScopeSelection();
        List<int> selectedScopeOUIds = GetSelectedScopeOUIds();
        List<int> selectedScopePOIds = GetSelectedScopePOIds();

        int positionId = int.Parse(this.PositionTreeView.SelectedNode.Value.Substring(2));
        this.AuthBLL.SetOperateScope((AuthorizationDS.StuffUserRow)Session["StuffUser"], (AuthorizationDS.PositionRow)Session["Position"], positionId, selectedOperateIds.ToArray(), selectedScopeOUIds.ToArray(), selectedScopePOIds.ToArray());

        this.ClearOperateCheck();
        PageUtility.ShowModelDlg(this, "设置成功");
    }

    private void ValidScopeSelection() {
        List<TreeNode> checkedNodes = new List<TreeNode>();
        foreach (TreeNode checkedNode in this.ScopeTreeView.CheckedNodes) {
            checkedNodes.Add(checkedNode);
        }
        foreach (TreeNode node in checkedNodes) {
            UncheckChildNodes(node);
        }
    }

    private void UncheckChildNodes(TreeNode node) {
        foreach (TreeNode childNode in node.ChildNodes) {
            UncheckChildNodes(childNode);
            childNode.Checked = false;
        }
    }

    private void SelectScopeTreeView(string nodeValue) {
        PageUtility.SelectTreeNodeByNodeValue(this.ScopeTreeView, nodeValue);
    }

    private List<int> GetSelectedScopePOIds() {
        List<int> result = new List<int>();
        foreach (TreeNode node in this.ScopeTreeView.CheckedNodes) {
            if (node.Value.StartsWith("PO")) {
                result.Add(int.Parse(node.Value.Substring(2)));
            }
        }
        return result;
    }

    private List<int> GetSelectedScopeOUIds() {
        List<int> result = new List<int>();
        foreach (TreeNode node in this.ScopeTreeView.CheckedNodes) {
            if (node.Value.StartsWith("OU")) {
                result.Add(int.Parse(node.Value.Substring(2)));
            }
        }
        return result;
    }

    #endregion

    #region ScopeList Operate
    
    protected void ScopedPositionLinkButton_Click(object sender, EventArgs e) {
        LinkButton btn = (LinkButton)sender;
        this.SelectScopeTreeView("PO" + btn.CommandArgument);
    }

    protected void ScopedOULinkButton_Click(object sender, EventArgs e) {
        LinkButton btn = (LinkButton)sender;
        this.SelectScopeTreeView("OU" + btn.CommandArgument);
    }

    protected void ScopeListGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            LinkButton scopedOULinkButton = (LinkButton)e.Row.FindControl("ScopedOULinkButton");

            DataRowView drv = (DataRowView)e.Row.DataItem;
            AuthorizationDS.OperateScopeRow operateScope = (AuthorizationDS.OperateScopeRow)drv.Row;
            if (operateScope.IsScopeOrganziationUnitIdNull()) {
                scopedOULinkButton.Visible = false;
            } else {
                scopedOULinkButton.Text = new OUTreeBLL().GetOrganizationUnitById(operateScope.ScopeOrganziationUnitId).OrganizationUnitName;
                scopedOULinkButton.CommandArgument = operateScope.ScopeOrganziationUnitId.ToString();
            }
        }
    }

    protected void ScopeListGridView_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.ScopeListGridView.SelectedValue != null) {
            int positionId = (int)this.ScopeListGridView.SelectedDataKey.Value;
            PageUtility.SelectTreeNodeByNodeValue(this.ScopeTreeView, "OU" + positionId);
        }
    }
    #endregion

    #region deal with Operate select

    private void ClearOperateCheck() {
        this.ViewState["CheckedOperateIds"] = null;
        foreach (GridViewRow gvr in this.OperateGridView.Rows) {
            CheckBox c = (CheckBox)gvr.FindControl("OperateCheckBox");
            c.Checked = false;
        }
    }

    protected void AllOperateCheckBox_CheckedChanged(object sender, EventArgs e) {
        CheckBox ctl = (CheckBox)sender;
        foreach (GridViewRow gvr in this.OperateGridView.Rows) {
            CheckBox c = (CheckBox)gvr.FindControl("OperateCheckBox");
            c.Checked = ctl.Checked;
        }
    }

    protected void OperateGridView_PageIndexChanging(object sender, GridViewPageEventArgs e) {
        this.ViewState["CheckedOperateIds"] = this.GetCheckedOperateIds();
    }

    protected void OperateGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
        List<int> checkedOperateIds = (List<int>)this.ViewState["CheckedOperateIds"];
        if (checkedOperateIds == null) {
            checkedOperateIds = new List<int>();
        }
        foreach (GridViewRow gridViewRow in this.OperateGridView.Rows) {
            if (gridViewRow.RowType == DataControlRowType.DataRow) {
                int operateId = (int)this.OperateGridView.DataKeys[gridViewRow.RowIndex].Value;
                CheckBox checkCtl = (CheckBox)gridViewRow.FindControl("OperateCheckBox");
                if (checkedOperateIds.Contains(operateId)) {
                    checkCtl.Checked = true;
                } else {
                    checkCtl.Checked = false;
                }
            }
        }
    }
    
    private List<int> GetCheckedOperateIds() {
        List<int> checkedOperateIds = (List<int>)this.ViewState["CheckedOperateIds"];
        if (checkedOperateIds == null) {
            checkedOperateIds = new List<int>();
        }

        foreach (GridViewRow gridViewRow in this.OperateGridView.Rows) {
            if (gridViewRow.RowType == DataControlRowType.DataRow) {
                int operateId = (int)this.OperateGridView.DataKeys[gridViewRow.RowIndex].Value;
                CheckBox checkCtl = (CheckBox)gridViewRow.FindControl("OperateCheckBox");
                if (checkCtl.Checked) {
                    if (!checkedOperateIds.Contains(operateId)) {
                        checkedOperateIds.Add(operateId);
                    }
                } else {
                    if (checkedOperateIds.Contains(operateId)) {
                        checkedOperateIds.Remove(operateId);
                    }
                }
            }
        }
        return checkedOperateIds;
    }

    #endregion

    #region OperateGridView Event
    protected void OperateGridView_DataBinding(object sender, EventArgs e) {
        this.OperateGridView.SelectedIndex = -1;
    }
    protected void OperateGridView_DataBound(object sender, EventArgs e) {
        this.OperateGridView_SelectedIndexChanged(this.OperateGridView, null);
    }

    protected void OperateGridView_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.OperateGridView.SelectedValue == null) {
            this.ClearScopeTreeView();

            this.ScopeListPanel.Visible = false;
        } else {
            this.SetScopeTreeView();

            this.ScopeListPanel.Visible = true;
            int operateId = (int)this.OperateGridView.SelectedValue;
            int positionId = int.Parse(this.PositionTreeView.SelectedNode.Value.Substring(2));
            this.PositionOperateScopeDS.SelectParameters["positionId"].DefaultValue = positionId.ToString();
            this.PositionOperateScopeDS.SelectParameters["businessOperateId"].DefaultValue = operateId.ToString();
            this.ScopeListGridView.DataBind();
        }
    }

    #endregion
}
