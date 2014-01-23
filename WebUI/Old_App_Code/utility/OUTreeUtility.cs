using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessObjects;

/// <summary>
/// Summary description for OUTreeUtility
/// </summary>
public class OUTreeUtility {


    
    public static void InitOUTree(TreeView treeView, bool showPosition, bool checkOU, bool checkPosition, bool selectOU, bool selectPosition,bool showActiveOU) {
        treeView.Nodes.Clear();
        AuthorizationDS.OrganizationUnitRow[] rootOU;
        if (showActiveOU) {
            rootOU = new OUTreeBLL().GetActiveRootOrganizationUnits();
        } else {
            rootOU = new OUTreeBLL().GetRootOrganizationUnits();
        }

        foreach (AuthorizationDS.OrganizationUnitRow organizationUnit in rootOU) {
            treeView.Nodes.Add(CreateOUTreeNode(organizationUnit, showPosition, checkOU, checkPosition, selectOU, selectPosition,showActiveOU));
        }
    }

    private static TreeNode NewTreeNode(AuthorizationDS.PositionRow position, bool checkPosition, bool selectPosition) {
        TreeNode treeNode = new TreeNode();
        treeNode.Text = position.PositionName;
        treeNode.ShowCheckBox = checkPosition;
        if (!selectPosition) {
            treeNode.SelectAction = TreeNodeSelectAction.None;
        } else {
            treeNode.SelectAction = TreeNodeSelectAction.Select;
        }
        treeNode.Value = "PO" + position.PositionId.ToString();
        treeNode.ImageUrl = "~/Images/post.png";
        return treeNode;
    }

    private static TreeNode NewTreeNode(AuthorizationDS.OrganizationUnitRow ou, bool checkOU, bool selectOU) {
        TreeNode treeNode = new TreeNode();
        treeNode.Text = ou.OrganizationUnitName;
        treeNode.ShowCheckBox = checkOU;
        if (selectOU) {
            treeNode.SelectAction = TreeNodeSelectAction.Select;
        } else {
            treeNode.SelectAction = TreeNodeSelectAction.Expand;
        }
        treeNode.Value = "OU" + ou.OrganizationUnitId;
        treeNode.ImageUrl = "~/Images/department.png";
        return treeNode;
    }

    private static TreeNode CreateOUTreeNode(AuthorizationDS.OrganizationUnitRow organizationUnit,bool showPosition, bool checkOU, bool checkPosition, bool selectOU, bool selectPosition,bool showActiveOU) {
        TreeNode newNode = NewTreeNode(organizationUnit, checkOU, selectOU);
        AuthorizationDS.OrganizationUnitRow[] childOU;
        if (showActiveOU) {
            childOU = organizationUnit.GetActiveChildren();
        } else {
            childOU = organizationUnit.GetChildren();
        }
        foreach (AuthorizationDS.OrganizationUnitRow subOU in childOU) {
            newNode.ChildNodes.Add(CreateOUTreeNode(subOU, showPosition, checkOU, checkPosition, selectOU, selectPosition,showActiveOU));
        }
        if (showPosition) {
            foreach (AuthorizationDS.PositionRow position in organizationUnit.GetPositionRows()) {
                newNode.ChildNodes.Add(NewTreeNode(position, checkPosition, selectPosition));
            }
        }
        return newNode;
    }
}
