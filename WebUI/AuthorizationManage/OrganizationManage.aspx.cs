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
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;

public partial class AuthorizationManage_OrganizationManage : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "组织机构管理");
            this.Page.Title = "组织机构管理";
            //初始化不显示作废组织结构
            this.ViewState["ShowActiveOU"] = true;
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.UseCase.OrganizationManage, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.UseCase.OrganizationManage, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool hasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            bool hasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);

            if (!hasViewRight && !hasManageRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
            if (!hasManageRight) {
                this.AddRootOrganizationUnitBtn.Visible = false;
                //this.ChangeParentUnitBtn.Visible=false;
                this.UpdataOrganizationUnitBtn.Visible = false;
                this.DeleteOrganizationUnitBtn.Visible = false;
                this.AddOrganizationUnitBtn.Visible = false;
                this.AddPositionBtn.Visible = false;
                //this.ChangeOrganizationUnitBtn.Visible = false;
                this.UpdatePositionBtn.Visible = false;
                this.DeletePositionBtn.Visible = false;
            }
            this.InitTreeView();
        }
        PageUtility.CloseModelDlg(this);
    }

    private OUTreeBLL AuthBLL {
        get {
            return new OUTreeBLL();
        }
    }

    private void InitTreeView() {
        AuthorizationDS.OrganizationUnitRow[] rootOU;
        if ((bool)this.ViewState["ShowActiveOU"] == true) {
            rootOU = this.AuthBLL.GetActiveRootOrganizationUnits();
        } else {
            rootOU = this.AuthBLL.GetRootOrganizationUnits();
        }
        foreach (AuthorizationDS.OrganizationUnitRow organizationUnit in rootOU) {
            //this.AuthBLL.BuildOUOrganizationTreePath(organizationUnit);
            this.OrganizationTreeView.Nodes.Add(CreateOUTreeNode(organizationUnit));
        }
        this.EditPositionPanelArea.Style["Display"] = "none";
        this.StuffPanel.Style["Display"] = "none";
        this.EditOUPanelArea.Style["Display"] = "none";
        this.NewOUPanelArea.Style["Display"] = "none";
        this.NewPositionPanelArea.Style["Display"] = "none";
    }

    private void SetTreeNode(TreeNode treeNode, AuthorizationDS.OrganizationUnitRow ou) {
        treeNode.Text = ou.OrganizationUnitName;
        treeNode.Value = NodeValue.FromObject(ou).ToString();
        treeNode.ShowCheckBox = true;
        treeNode.ImageUrl = "~/Images/department.png";
    }

    private void SetTreeNode(TreeNode treeNode, AuthorizationDS.PositionRow position) {
        treeNode.Text = position.PositionName;
        treeNode.Value = NodeValue.FromObject(position).ToString();
        treeNode.ShowCheckBox = false;
        treeNode.ImageUrl = "~/Images/post.png";
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
        AuthorizationDS.OrganizationUnitRow[] childOU;
        if ((bool)this.ViewState["ShowActiveOU"]) {
            childOU = organizationUnit.GetActiveChildren();
        } else {
            childOU = organizationUnit.GetChildren();
        }

        TreeNode newNode = NewTreeNode(organizationUnit);
        foreach (AuthorizationDS.OrganizationUnitRow subOU in childOU) {
            newNode.ChildNodes.Add(CreateOUTreeNode(subOU));
        }
        AuthorizationDS.PositionRow[] childPosition;
        if ((bool)this.ViewState["ShowActiveOU"]) {
            childPosition = (AuthorizationDS.PositionRow[])new PositionTableAdapter().GetActiveDataByOrganizationUnitId(organizationUnit.OrganizationUnitId).Select();
        } else {
            childPosition = organizationUnit.GetPositionRows();
        }

        foreach (AuthorizationDS.PositionRow position in childPosition) {
            newNode.ChildNodes.Add(NewTreeNode(position));
        }
        return newNode;
    }

    protected void OrganizationTreeView_SelectedNodeChanged(object sender, EventArgs e) {
        if (this.OrganizationTreeView.SelectedNode != null) {
            NodeValue nodeValue = NodeValue.Parse(this.OrganizationTreeView.SelectedNode.Value);
            switch (nodeValue.OType) {
                case NodeValue.ValueType.OU:
                    AuthorizationDS.OrganizationUnitRow organizationUnit = this.AuthBLL.GetOrganizationUnitById(nodeValue.ObjectId);
                    this.UnitNameCtl.Text = organizationUnit.OrganizationUnitName;
                    this.UnitCodeCtl.Text = organizationUnit.OrganizationUnitCode;
                    this.UnitIsActiveCtl.Checked = organizationUnit.IsActive;
                    this.UnitTypeCtl.SelectedValue = organizationUnit.IsOrganizationUnitTypeIdNull() ? "" : organizationUnit.OrganizationUnitTypeId.ToString();
                    this.CostCenterDDL.SelectedValue = organizationUnit.IsCostCenterIDNull() ? "0" : organizationUnit.CostCenterID.ToString();

                    this.ClearNewOUPanel();
                    this.ClearNewPositionPanel();

                    this.EditPositionPanelArea.Style["Display"] = "none";
                    this.StuffPanel.Style["Display"] = "none";
                    this.EditOUPanelArea.Style["Display"] = "";
                    if (organizationUnit.IsActive) {
                        this.NewOUPanelArea.Style["Display"] = "";
                        this.NewPositionPanelArea.Style["Display"] = "";
                    } else {
                        this.NewOUPanelArea.Style["Display"] = "none";
                        this.NewPositionPanelArea.Style["Display"] = "none";
                    }
                    break;
                case NodeValue.ValueType.Position:
                    AuthorizationDS.PositionRow position = this.AuthBLL.GetPositionById(nodeValue.ObjectId);
                    this.PositionNameCtl.Text = position.PositionName;
                    this.PositionIsActiveCtl.Checked = position.IsActive;
                    this.PositionTypeCtl.ClearSelection();
                    BusinessObjects.AuthorizationDS.PositionAndPositionTypeRow[] positionTypes = position.GetPositionAndPositionTypeRows();
                    if (positionTypes == null || positionTypes.Length == 0) {
                        //this.PositionTypeCtl.SelectedIndex = 0;
                    } else {
                        foreach (AuthorizationDS.PositionAndPositionTypeRow positionType in positionTypes) {
                            foreach (ListItem li in this.PositionTypeCtl.Items) {
                                if (li.Value.Equals(positionType.PositionTypeId.ToString())) {
                                    li.Selected = true;
                                    break;
                                }
                            }
                        }
                    }

                    this.EditPositionPanelArea.Style["Display"] = "";
                    this.StuffPanel.Style["Display"] = "";
                    this.PositionStuffDS.SelectParameters["PositionId"].DefaultValue = nodeValue.ObjectId.ToString();
                    this.EditOUPanelArea.Style["Display"] = "none";
                    this.NewOUPanelArea.Style["Display"] = "none";
                    this.NewPositionPanelArea.Style["Display"] = "none";
                    break;
            }
        } else {
            this.EditPositionPanelArea.Style["Display"] = "none";
            this.StuffPanel.Style["Display"] = "none";
            this.EditOUPanelArea.Style["Display"] = "none";
            this.NewOUPanelArea.Style["Display"] = "none";
            this.NewPositionPanelArea.Style["Display"] = "none";
        }
        //this.NewOUPanel.Update();
        //this.EditOUPanel.Update();
        //this.NewPositionPanel.Update();
        //this.EditPositionPanel.Update();
    }

    private void ClearNewPositionPanel() {
        this.NewPositionNameCtl.Text = "";
        foreach (ListItem listItem in NewPositionTypeCtl.Items) {
            listItem.Selected = false;
        }
        //this.NewPositionTypeCtl.SelectedIndex = 0;
    }

    private void ClearNewOUPanel() {
        this.NewUnitNameCtl.Text = "";
        this.NewUnitCodeCtl.Text = "";
        this.NewUnitTypeCtl.SelectedIndex = 0;
    }

    protected void AddRootOrganizationUnitBtn_Click(object sender, EventArgs e) {
        string strTypeId = this.RootUnitTypeCtl.SelectedValue;
        int? typeId = null;
        if (strTypeId.Length == 0) {
            typeId = null;
        } else {
            typeId = int.Parse(strTypeId);
        }
        int? costCenterID = null;
        if (this.RootCostCenterDDL.SelectedValue != "0") {
            costCenterID = int.Parse(this.RootCostCenterDDL.SelectedValue);
        }
        AuthorizationDS.OrganizationUnitRow organizationUnit = this.AuthBLL.AddOrganizationUnit(this.RootUnitNameCtl.Text, this.RootUnitCodeCtl.Text, null, typeId, costCenterID);
        this.OrganizationTreeView.Nodes.Add(NewTreeNode(organizationUnit));
        this.RootUnitNameCtl.Text = "";
        this.RootUnitCodeCtl.Text = "";
        this.RootUnitTypeCtl.SelectedIndex = 0;
        this.RootCostCenterDDL.SelectedIndex = 0;
    }

    //protected void ChangeParentUnitBtn_Click(object sender, EventArgs e) {
    //    if (this.OrganizationTreeView.CheckedNodes.Count == 1) {
    //        TreeNode newParentNode = this.OrganizationTreeView.CheckedNodes[0];
    //        NodeValue parentNodeValue = NodeValue.Parse(newParentNode.Value);
    //        if (parentNodeValue.OType == NodeValue.ValueType.OU) {
    //            if (parentNodeValue.IsActive) {
    //                TreeNode childNode = this.OrganizationTreeView.SelectedNode;
    //                NodeValue childNodeValue = NodeValue.Parse(childNode.Value);
    //                this.AuthBLL.SetOrganizationUnitParentOU(childNodeValue.ObjectId, parentNodeValue.ObjectId);
    //                if (childNode.Parent != null) {
    //                    childNode.Parent.ChildNodes.Remove(childNode);
    //                }
    //                newParentNode.ChildNodes.Add(childNode);
    //                this.OrganizationTreeView_SelectedNodeChanged(null, null);
    //            } else {
    //                PageUtility.ShowModelDlg(this, "目标组织机构处于非激活状态，不能迁移");
    //                return;
    //            }
    //        } else {
    //            PageUtility.ShowModelDlg(this, "请先选择一个目标组织机构");
    //            return;
    //        }
    //    } else {
    //        PageUtility.ShowModelDlg(this, "请选择唯一目标组织机构");
    //        return;
    //    }
    //}

    protected void ChangeParentUnitBtn_Click(object sender, EventArgs e) {
        if (this.OrganizationTreeView.CheckedNodes.Count == 1) {
            TreeNode newParentNode = this.OrganizationTreeView.CheckedNodes[0];
            NodeValue parentNodeValue = NodeValue.Parse(newParentNode.Value);
            if (parentNodeValue.OType == NodeValue.ValueType.OU) {
                if (parentNodeValue.IsActive) {
                    TreeNode childNode = this.OrganizationTreeView.SelectedNode;
                    NodeValue childNodeValue = NodeValue.Parse(childNode.Value);
                    this.AuthBLL.SetOrganizationUnitParentOU(childNodeValue.ObjectId, parentNodeValue.ObjectId);
                    if (childNode.Parent != null) {
                        childNode.Parent.ChildNodes.Remove(childNode);
                    }
                    newParentNode.ChildNodes.Add(childNode);
                    this.OrganizationTreeView_SelectedNodeChanged(null, null);
                    //this.Page_Load(null, null);
                    Response.Redirect("~/AuthorizationManage/OrganizationManage.aspx");
                } else {
                    PageUtility.ShowModelDlg(this, "目标组织机构处于非激活状态，不能迁移");
                    return;
                }
            } else {
                PageUtility.ShowModelDlg(this, "请先选择一个目标组织机构");
                return;
            }
        } else {
            PageUtility.ShowModelDlg(this, "请选择唯一目标组织机构");
            return;
        }
    }

    protected void UpdataOrganizationUnitBtn_Click(object sender, EventArgs e) {
        try {
            NodeValue nodeValue = NodeValue.Parse(this.OrganizationTreeView.SelectedNode.Value);
            string strTypeId = this.UnitTypeCtl.SelectedValue;
            int? typeId = null;
            if (strTypeId.Length == 0) {
                typeId = null;
            } else {
                typeId = int.Parse(strTypeId);
            }
            int? costCenterID = null;
            if (this.CostCenterDDL.SelectedValue != "0") {
                costCenterID = int.Parse(this.CostCenterDDL.SelectedValue);
            }

            this.AuthBLL.UpdateOrganizationUnit(nodeValue.ObjectId, this.UnitNameCtl.Text, this.UnitCodeCtl.Text, typeId, costCenterID);
            AuthorizationDS.OrganizationUnitRow ou = this.AuthBLL.ActiveOrganizationUnit(nodeValue.ObjectId, this.UnitIsActiveCtl.Checked);
            this.SetTreeNode(this.OrganizationTreeView.SelectedNode, ou);
            if (ou.IsActive) {
                this.NewOUPanelArea.Style["Display"] = "";
                this.NewPositionPanelArea.Style["Display"] = "";
            } else {
                this.NewOUPanelArea.Style["Display"] = "none";
                this.NewPositionPanelArea.Style["Display"] = "none";
            }
            //this.NewOUPanel.Update();
            //this.NewPositionPanel.Update();
            PageUtility.ShowModelDlg(this, "更新成功");

        } catch (ApplicationException ex) {
            PageUtility.DealWithException(this, ex);
        }
    }

    private void UpdateTreeNode(TreeNode treeNode) {
        foreach (TreeNode childNode in treeNode.ChildNodes) {
            UpdateTreeNode(childNode);
        }
        NodeValue nodeValue = NodeValue.Parse(treeNode.Value);
        switch (nodeValue.OType) {
            case NodeValue.ValueType.OU:
                this.SetTreeNode(treeNode, this.AuthBLL.GetOrganizationUnitById(nodeValue.ObjectId));
                break;
            case NodeValue.ValueType.Position:
                this.SetTreeNode(treeNode, this.AuthBLL.GetPositionById(nodeValue.ObjectId));
                break;
        }
    }

    protected void DeleteOrganizationUnitBtn_Click(object sender, EventArgs e) {
        TreeNode node = this.OrganizationTreeView.SelectedNode;
        NodeValue nodeValue = NodeValue.Parse(node.Value);
        try {
            this.AuthBLL.DeleteOrganizationUnit(nodeValue.ObjectId);
            if (node.Parent != null) {
                node.Parent.ChildNodes.Remove(node);
            } else {
                this.OrganizationTreeView.Nodes.Remove(node);
            }
            this.OrganizationTreeView_SelectedNodeChanged(null, null);
        } catch (ApplicationException ex) {
            PageUtility.DealWithException(this, ex);
        }
    }

    protected void AddOrganizationUnitBtn_Click(object sender, EventArgs e) {
        TreeNode parentNode = this.OrganizationTreeView.SelectedNode;
        NodeValue parentNodeValue = NodeValue.Parse(parentNode.Value);
        string strTypeId = this.NewUnitTypeCtl.SelectedValue;
        int? typeId = null;
        if (strTypeId.Length == 0) {
            typeId = null;
        } else {
            typeId = int.Parse(strTypeId);
        }
        int? costCenterID = null;
        if (this.NewCostCenterDDL.SelectedValue != "0") {
            costCenterID = int.Parse(this.NewCostCenterDDL.SelectedValue);
        }

        AuthorizationDS.OrganizationUnitRow newOU = this.AuthBLL.AddOrganizationUnit(this.NewUnitNameCtl.Text, this.NewUnitCodeCtl.Text, parentNodeValue.ObjectId, typeId, costCenterID);
        parentNode.ChildNodes.Add(NewTreeNode(newOU));
        this.ClearNewOUPanel();
    }

    protected void AddPositionBtn_Click(object sender, EventArgs e) {
        NodeValue nodeValue = NodeValue.Parse(this.OrganizationTreeView.SelectedNode.Value);
        List<int> positionTypeIds = new List<int>();
        foreach (ListItem li in this.NewPositionTypeCtl.Items) {
            if (li.Selected) {
                if (li.Value.Length > 0) {
                    positionTypeIds.Add(int.Parse(li.Value));
                } else {
                    positionTypeIds.Clear();
                    break;
                }
            }
        }
        AuthorizationDS.PositionRow newPosition = this.AuthBLL.AddPosition(this.NewPositionNameCtl.Text, nodeValue.ObjectId, positionTypeIds);

        TreeNode childNode = new TreeNode();
        this.SetTreeNode(childNode, newPosition);
        this.OrganizationTreeView.SelectedNode.ChildNodes.Add(childNode);
    }

    protected void ChangeOrganizationUnitBtn_Click(object sender, EventArgs e) {
        if (this.OrganizationTreeView.CheckedNodes.Count == 1) {
            TreeNode newParentNode = this.OrganizationTreeView.CheckedNodes[0];
            NodeValue parentNodeValue = NodeValue.Parse(newParentNode.Value);
            if (parentNodeValue.OType == NodeValue.ValueType.OU) {
                if (parentNodeValue.IsActive) {
                    TreeNode childNode = this.OrganizationTreeView.SelectedNode;
                    NodeValue childNodeValue = NodeValue.Parse(childNode.Value);
                    this.AuthBLL.SetPositionParentOU(childNodeValue.ObjectId, parentNodeValue.ObjectId);
                    childNode.Parent.ChildNodes.Remove(childNode);
                    newParentNode.ChildNodes.Add(childNode);
                    this.OrganizationTreeView_SelectedNodeChanged(null, null);
                } else {
                    PageUtility.ShowModelDlg(this, "目标组织机构处于非激活状态，不能迁移");
                    return;
                }
            } else {
                PageUtility.ShowModelDlg(this, "请先选择目标组织机构");
                return;
            }
        } else {
            PageUtility.ShowModelDlg(this, "请选择唯一目标组织机构");
            return;
        }


    }

    protected void UpdatePositionBtn_Click(object sender, EventArgs e) {
        try {
            NodeValue nodeValue = NodeValue.Parse(this.OrganizationTreeView.SelectedNode.Value);
            List<int> positionTypeIds = new List<int>();
            foreach (ListItem li in this.PositionTypeCtl.Items) {
                if (li.Selected) {
                    if (li.Value.Length > 0) {
                        positionTypeIds.Add(int.Parse(li.Value));
                    } else {
                        positionTypeIds.Clear();
                        break;
                    }
                }
            }
            this.AuthBLL.UpdatePosition(nodeValue.ObjectId, this.PositionNameCtl.Text, positionTypeIds);
            AuthorizationDS.PositionRow position = this.AuthBLL.ActivePosition(nodeValue.ObjectId, this.PositionIsActiveCtl.Checked);
            this.SetTreeNode(this.OrganizationTreeView.SelectedNode, position);
        } catch (ApplicationException ex) {
            PageUtility.DealWithException(this, ex);
        }
    }

    protected void DeletePositionBtn_Click(object sender, EventArgs e) {
        NodeValue nodeValue = NodeValue.Parse(this.OrganizationTreeView.SelectedNode.Value);
        try {
            this.AuthBLL.DeletePosition(nodeValue.ObjectId);
            this.OrganizationTreeView.SelectedNode.Parent.ChildNodes.Remove(this.OrganizationTreeView.SelectedNode);
            this.OrganizationTreeView_SelectedNodeChanged(null, null);
        } catch (ApplicationException ex) {
            PageUtility.DealWithException(this, ex);
        }
    }

    class NodeValue {
        public enum ValueType {
            OU,
            Position
        }
        public ValueType OType;
        public int ObjectId;
        public bool IsActive;
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            if (IsActive) {
                sb.Append("1");
            } else {
                sb.Append("0");
            }
            switch (OType) {
                case ValueType.OU:
                    sb.Append("OU");
                    break;
                case ValueType.Position:
                    sb.Append("PO");
                    break;
            }
            sb.Append(ObjectId);
            return sb.ToString();
        }

        public static NodeValue Parse(string strValue) {
            NodeValue result = new NodeValue();
            if (strValue.StartsWith("1")) {
                result.IsActive = true;
            } else {
                result.IsActive = false;
            }

            if (strValue.Substring(1, 2).Equals("OU")) {
                result.OType = ValueType.OU;
            } else {
                result.OType = ValueType.Position;
            }

            result.ObjectId = int.Parse(strValue.Substring(3));
            return result;
        }

        public static NodeValue FromObject(AuthorizationDS.OrganizationUnitRow organizationUnit) {
            NodeValue result = new NodeValue();
            result.ObjectId = organizationUnit.OrganizationUnitId;
            result.IsActive = organizationUnit.IsActive;
            result.OType = ValueType.OU;
            return result;
        }

        public static NodeValue FromObject(AuthorizationDS.PositionRow position) {
            NodeValue result = new NodeValue();
            result.OType = ValueType.Position;
            result.ObjectId = position.PositionId;
            result.IsActive = position.IsActive;
            return result;
        }
    }

    protected void RootUnitTypeCtl_DataBound(object sender, EventArgs e) {
        this.RootUnitTypeCtl.Items.Insert(0, new ListItem("无", ""));
    }

    protected void NewUnitTypeCtl_DataBound(object sender, EventArgs e) {
        this.NewUnitTypeCtl.Items.Insert(0, new ListItem("无", ""));
    }

    protected void UnitTypeCtl_DataBound(object sender, EventArgs e) {
        this.UnitTypeCtl.Items.Insert(0, new ListItem("无", ""));
    }

    //protected void PositionTypeCtl_DataBound(object sender, EventArgs e) {
    //    this.PositionTypeCtl.Items.Insert(0, new ListItem("无", ""));
    //}

    protected void ShowAllOU_Click(object sender, EventArgs e) {
        this.ViewState["ShowActiveOU"] = false;
        this.OrganizationTreeView.Nodes.Clear();
        this.InitTreeView();
    }
}
