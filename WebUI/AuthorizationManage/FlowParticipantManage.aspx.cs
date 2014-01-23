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
using lib.wf;

public partial class AuthorizationManage_FlowParticipantManage :BasePage
{


    private AuthorizationBLL m_AuthorizationBLL;
    protected AuthorizationBLL AuthorizationBLL {
        get {
            if (this.m_AuthorizationBLL == null) {
                this.m_AuthorizationBLL = new AuthorizationBLL();
            }
            return this.m_AuthorizationBLL;
        }
    }

    private AuthorizationDS m_InnerDS;
    public AuthorizationDS InnerDS {
        get {
            if (this.ViewState["AuthorizationDS"] == null) {
                this.ViewState["AuthorizationDS"] = new AuthorizationDS();
            }
            return (AuthorizationDS)this.ViewState["AuthorizationDS"];
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender,e);
        if(!IsPostBack){
            PageUtility.SetContentTitle(this, "流程管理");
            this.Page.Title = "流程管理";
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FlowParticipant, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            if (!positionRightBLL.CheckPositionRight(position.PositionId, opManageId)) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }

            DropDownList ddlDefName = (DropDownList)this.fvFlowConfigure.FindControl("ddlDefName");
            DataTable dt = new APHelper().GetDefNames();
            ddlDefName.DataSource = dt;
            ddlDefName.DataValueField = "DefName";
            ddlDefName.DataTextField = "DefName";
            ddlDefName.DataBind();

            ViewState["ddlDefNameTable"] = dt;
          
           
        }
        
    }


    

    protected void gvFlowConfigure_RowDataBound(object sender, GridViewRowEventArgs e) {
        BusinessUseCaseTableAdapter BusinessUseCaseTA= new BusinessUseCaseTableAdapter();
        if (e.Row.RowType == DataControlRowType.DataRow) {
            Label FlowID = (Label)e.Row.FindControl("lblFlowParticipant");
            Label lblBusinessUseCaseName = (Label)e.Row.FindControl("lblBusinessUseCaseName");
            lblBusinessUseCaseName.Text = BusinessUseCaseTA.GetBusinessUseCaseName(int.Parse(lblBusinessUseCaseName.Text)).ToString();
            if (!string.IsNullOrEmpty(FlowID.Text.Trim())) {
                AuthorizationDS.FlowParticipantConfigureDetailDataTable dt = AuthorizationBLL.GetFlowParticipantConfigureDetailByID(FlowID.Text);
                if (dt.Rows.Count > 0) {
                    string User = string.Empty;
                    foreach (AuthorizationDS.FlowParticipantConfigureDetailRow dr in dt) {
                        User += dr.UserName + ",";
                    }
                    FlowID.Text = User.TrimEnd(',');
                } else {
                    FlowID.Text = "所有人";
                }
            } 

        }
       
    }

    protected void odsFlowConfigure_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {

        TextBox txtFlowName = (TextBox)this.fvFlowConfigure.FindControl("txtFlowName");
        DropDownList ddlDefName = (DropDownList)this.fvFlowConfigure.FindControl("ddlDefName");
        DropDownList ddlBusinessUseCase = (DropDownList)this.fvFlowConfigure.FindControl("ddlBusinessUseCase");
        if (!string.IsNullOrEmpty(txtFlowName.Text.Trim(' '))) {
            e.InputParameters["FlowTemplateName"] = ddlDefName.SelectedValue.ToString();
            e.InputParameters["FlowName"] = txtFlowName.Text.Trim(' ');
            e.InputParameters["BusinessUseCaseId"] = ddlBusinessUseCase.SelectedValue.ToString();
        } else {
            PageUtility.ShowModelDlg(this.Page, "请填写流程名称！");
            e.Cancel = true;
        }




    }

    //绑定流程模板下拉框
    protected void fvFlowConfigure_DataBound(object sender, EventArgs e) {
        DropDownList ddlDefName = (DropDownList)this.fvFlowConfigure.FindControl("ddlDefName");
        DataTable dt = (DataTable)ViewState["ddlDefNameTable"];
        ddlDefName.DataSource = dt;
        ddlDefName.DataValueField = "DefName";
        ddlDefName.DataTextField = "DefName";
        ddlDefName.DataBind();
        upCustomerType.Update();
    }

    protected void FlowParticipantDetail_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        UserControls_StaffControl StaffControl = (UserControls_StaffControl)this.fvStuff.FindControl("StaffControl1");
        TextBox txtFlowParticipant = (TextBox)this.fvStuff.FindControl("txtFlowParticipant");
        if (!string.IsNullOrEmpty(StaffControl.StaffID)) {
            e.InputParameters["FlowID"] = int.Parse(ViewState["FlowID"].ToString());
            e.InputParameters["UserName"] = txtFlowParticipant.Text.Trim(' ');
            e.InputParameters["StaffName"] = StaffControl.StaffName;
            e.InputParameters["UserID"] = StaffControl.StaffID;
        } else {
            PageUtility.ShowModelDlg(this.Page, "请选择用户！");
            e.Cancel = true;
            
        }
        
    }

    //传递数据
    protected void FlowParticipantDetail_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        AuthorizationBLL bll = (AuthorizationBLL)e.ObjectInstance;
       
        bll.ApproverCookieDS = this.InnerDS;
        
    }



    protected void txtStaffName_TextChanged(object sender, EventArgs e) {
        UserControls_StaffControl StaffControl = (UserControls_StaffControl)this.fvStuff.FindControl("StaffControl1");
        TextBox txtFlowParticipant = (TextBox)this.fvStuff.FindControl("txtFlowParticipant");
       if (StaffControl.StaffID != string.Empty) {
           txtFlowParticipant.Text = this.AuthorizationBLL.GetStuffUserById(int.Parse(StaffControl.StaffID)).UserName;
        } else {
            txtFlowParticipant.Text = "";
        }
    }



    protected void SubmitBtn_Click(object sender, EventArgs e) {
      this.AuthorizationBLL.ApproverCookieDS = this.InnerDS;
      this.AuthorizationBLL.SaveFlowParticipantConfigureDetail();
      this.gvFlowConfigure.DataBind();
      this.upCustomerType.Update();
      this.StuffPanel.Style["display"] = "none";
    }

    protected void CancelBtn_Click(object sender, EventArgs e) {
        this.StuffPanel.Style["display"] = "none";

    }
   

    //显示流程人员
    protected void lknSelect(object sender, EventArgs e) {
        LinkButton button = (LinkButton)sender;
        GridViewRow gvr = (GridViewRow)button.Parent.Parent;
        this.StuffPanel.Style["display"] = "block";
        ViewState["FlowID"]=gvFlowConfigure.DataKeys[gvr.RowIndex].Value;
        FlowParticipantConfigureDetailTableAdapter taFlowParticipantConfigureDetail = new FlowParticipantConfigureDetailTableAdapter();
        taFlowParticipantConfigureDetail.FillDataByFlowID(this.InnerDS.FlowParticipantConfigureDetail, int.Parse(ViewState["FlowID"].ToString())); 
        this.StuffGridView.DataBind();
        upFlowParticipantDetail.Update();
        
        
    }

}