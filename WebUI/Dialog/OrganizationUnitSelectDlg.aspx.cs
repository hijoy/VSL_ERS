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

public partial class Dialog_OrganizationUnitSelectDlg : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {            
            OUTreeUtility.InitOUTree(this.OUTree,false, false, false,true, false,true);
            if (Request["OUId"] != null) {
                PageUtility.SelectTreeNodeByNodeValue(this.OUTree, "OU" + Request["OUId"]);
            }
        }
    }
    protected void OUTree_SelectedNodeChanged(object sender, EventArgs e) {
        this.OUIdCtl.Value = "";
        this.OUCodeCtl.Value = "";
        this.OUNameCtl.Value = "";
        if (this.OUTree.SelectedNode != null) {
            if (this.OUTree.SelectedNode.Value.StartsWith("OU")) {
                this.OUIdCtl.Value = this.OUTree.SelectedNode.Value.Substring(2);
                AuthorizationDS.OrganizationUnitRow ou = new OUTreeBLL().GetOrganizationUnitById(int.Parse(this.OUIdCtl.Value));
                this.OUCodeCtl.Value = ou.IsOrganizationUnitCodeNull() ? "" : ou.OrganizationUnitCode;
                this.OUNameCtl.Value = this.OUTree.SelectedNode.Text;
            }
        }
    }
}
