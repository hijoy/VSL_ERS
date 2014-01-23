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
using System.Collections.Generic;


public partial class AuthorizationManage_SystemRoleManage : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "系统角色管理");
            this.Page.Title = "系统角色管理";
            //this.InitBusinessOperateTreeView();
            DataView dataView = this.AuthDS.BusinessUseCase.DefaultView;
            dataView.Sort = "BusinessUseCaseId";
            this.BusinessOperateGridView.DataSource = dataView;
            this.BusinessOperateGridView.DataBind();
        }
    }

    AuthorizationDS m_AuthDS;
    AuthorizationDS AuthDS {
        get {
            if (m_AuthDS == null) {
                m_AuthDS = new AuthorizationDS();
                BusinessUseCaseTableAdapter businessUseCaseTA = new BusinessUseCaseTableAdapter();
                BusinessOperateTableAdapter operateTA = new BusinessOperateTableAdapter();
                businessUseCaseTA.Fill(m_AuthDS.BusinessUseCase);
                operateTA.Fill(m_AuthDS.BusinessOperate);
                
            }
            return m_AuthDS;
        }
    }

    private void CheckOperate(int[] operateIds) {
        List<int> ids = new List<int>(operateIds);
        foreach (GridViewRow gridViewRow in this.BusinessOperateGridView.Rows) {
            CheckBoxList businessOperateCtl = (CheckBoxList)gridViewRow.FindControl("BusinessOperateCtl");
            foreach (ListItem listItem in businessOperateCtl.Items) {
                if (ids.Contains(int.Parse(listItem.Value))) {
                    listItem.Selected = true;
                } else {
                    listItem.Selected = false;
                }
            }
        }
    }

    private List<int> GetCheckOperate() {
        List<int> ids = new List<int>();
        foreach (GridViewRow gridViewRow in this.BusinessOperateGridView.Rows) {
            CheckBoxList businessOperateCtl = (CheckBoxList)gridViewRow.FindControl("BusinessOperateCtl");
            foreach (ListItem listItem in businessOperateCtl.Items) {
                if (listItem.Selected) {
                    ids.Add(int.Parse(listItem.Value));
                }
            }
        }
        return ids;
    }

    protected void BusinessOperateGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            CheckBoxList businessOperateCtl = (CheckBoxList)e.Row.FindControl("BusinessOperateCtl");
            DataRowView drv = (DataRowView)e.Row.DataItem;
            businessOperateCtl.DataSource = ((AuthorizationDS.BusinessUseCaseRow)drv.Row).GetBusinessOperateRows();
            businessOperateCtl.DataTextField = "BusinessOperateName";
            businessOperateCtl.DataValueField = "BusinessOperateId";
            businessOperateCtl.DataBind();
        }
    }
    
    protected void SetSystemRoleOperateBtn_Click(object sender, EventArgs e) {
        List<int> newOperateIds = this.GetCheckOperate();

        int roleId = (int)this.GridView1.SelectedValue;

        new AuthorizationBLL().SetSystemRoleOperateRight((AuthorizationDS.StuffUserRow)this.Session["StuffUser"], (AuthorizationDS.PositionRow)this.Session["Position"], newOperateIds, roleId);
        PageUtility.ShowModelDlg(this, "设置成功");
        
    }

    

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e) {
        this.CheckBusinessOperateGridView();
        this.UpdatePanel2.Update();
    }

    private void CheckBusinessOperateGridView() {
        if (this.GridView1.SelectedValue != null) {
            int roleId = (int)this.GridView1.SelectedValue;

            SystemRoleAndBusinessOperateTableAdapter da = new SystemRoleAndBusinessOperateTableAdapter();
            BusinessObjects.AuthorizationDS.SystemRoleAndBusinessOperateDataTable table = da.GetDataBySystemRoleId(roleId);
            List<int> operateIds = new List<int>();
            foreach (BusinessObjects.AuthorizationDS.SystemRoleAndBusinessOperateRow row in table) {
                operateIds.Add(row.BusinessOperateId);
            }
            this.CheckOperate(operateIds.ToArray());
           // this.BusinessOperateArea.Style["display"] = "";
            SetSystemRoleOperateBtn.Enabled = true;
        } else {
            //this.BusinessOperateArea.Style["display"] = "none";
            SetSystemRoleOperateBtn.Enabled = false;
        }
    }

    
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e) {
        this.GridView1.SelectedIndex = -1;
        this.GridView1_SelectedIndexChanged(null, null);

    }
   
    protected void SystemRoleDS_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void SystemRoleDS_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void SystemRoleDS_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }
    protected void SystemRoleDS_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["stuffUser"] = this.Session["StuffUser"];
        e.InputParameters["position"] = this.Session["Position"];
    }
}
