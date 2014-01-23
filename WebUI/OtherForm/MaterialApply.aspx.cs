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
using BusinessObjects.FormDSTableAdapters;

public partial class OtherForm_MaterialApply : BasePage {

    decimal TotalFee = 0;

    private MaterialApplyBLL m_MaterialApplyBLL;
    protected MaterialApplyBLL MaterialApplyBLL {
        get {
            if (this.m_MaterialApplyBLL == null) {
                this.m_MaterialApplyBLL = new MaterialApplyBLL();
            }
            return this.m_MaterialApplyBLL;
        }
    }

    private FormDS m_InnerDS;
    public FormDS InnerDS {
        get {
            if (this.ViewState["InnerDS"] == null) {
                this.ViewState["InnerDS"] = new FormDS();
            }
            return (FormDS)this.ViewState["InnerDS"];
        }
    }


    #region 页面初始化及事件处理


    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this.Page, "广宣物资申请");
            this.Page.Title = "广宣物资申请";

            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow rowUserPosition = (AuthorizationDS.PositionRow)Session["Position"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = rowUserPosition.PositionId;
            this.ViewState["FlowTemplate"] = new AuthorizationBLL().GetFlowTemplate((int)SystemEnums.BusinessUseCase.FormMaterial, stuffUser.StuffUserId);
            
            this.StuffNameCtl.Text = stuffUser.StuffName;
            this.PositionNameCtl.Text = rowUserPosition.PositionName;
            this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowUserPosition.OrganizationUnitId).OrganizationUnitName;
            this.ViewState["DepartmentID"] = rowUserPosition.OrganizationUnitId;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();

            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());
            }
            //如果是草稿进行赋值
            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
                OpenForm(int.Parse(this.ViewState["ObjectId"].ToString()));
            } else {
                this.DeleteBtn.Visible = false;
            }
            TextBox newQuantityCtl = (TextBox)this.fvMaterialDetails.FindControl("newQuantityCtl");
            newQuantityCtl.Attributes.Add("onchange", "ParameterChanged()");

            if (Session["ProxyStuffUserId"] != null) {
                this.SubmitBtn.Visible = false;
            }

        } else {
            TextBox newQuantityCtl = (TextBox)this.fvMaterialDetails.FindControl("newQuantityCtl");
            newQuantityCtl.Attributes.Add("onchange", "ParameterChanged()");
        }
    }

    protected void OpenForm(int formID) {
        FormTableAdapter taForm = new FormTableAdapter();
        taForm.FillByID(this.InnerDS.Form, formID);
        FormDS.FormRow rowForm = this.InnerDS.Form[0];
        FormMaterialTableAdapter taFormMaterial = new FormMaterialTableAdapter();
        taFormMaterial.FillByID(this.InnerDS.FormMaterial, formID);
        FormDS.FormMaterialRow rowFormMaterial = this.InnerDS.FormMaterial[0];
        //赋值
        this.UCShop.ShopID = rowFormMaterial.ShopID.ToString();
        this.FirstVolumeCtl.Text = rowFormMaterial.FirstVolume.ToString();
        this.SecondVolumeCtl.Text = rowFormMaterial.SecondVolume.ToString();
        this.ThirdVolumeCtl.Text = rowFormMaterial.ThirdVolume.ToString();
        this.RemarkCtl.Text = rowFormMaterial.Remark;

        // 打开明细表
        FormMaterialDetailTableAdapter taDetail = new FormMaterialDetailTableAdapter();
        taDetail.FillByFormMaterialID(this.InnerDS.FormMaterialDetail, formID);
    }

    #endregion

    protected void CancelBtn_Click(object sender, EventArgs e) {
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.MaterialApplyBLL.DeleteFormMaterial(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        if (!IsInputValid())
            return;
        SaveFormMaterial(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        if (!IsInputValid())
            return;
        if (!IsSubmitValid())
            return;
        SaveFormMaterial(SystemEnums.FormStatus.Awaiting);
    }

    protected void SaveFormMaterial(SystemEnums.FormStatus StatusID) {
        this.MaterialApplyBLL.FormDataSet = this.InnerDS;
        int? RejectedFormID = null;
        if (this.ViewState["RejectedObjectID"] != null) {
            RejectedFormID = (int)this.ViewState["RejectedObjectID"];
        }

        int UserID = (int)this.ViewState["StuffUserID"];
        int? ProxyStuffUserId = null;
        if (Session["ProxyStuffUserId"] != null) {
            ProxyStuffUserId = int.Parse(Session["ProxyStuffUserId"].ToString());
        }
        int OrganizationUnitID = (int)this.ViewState["DepartmentID"];
        int PositionID = (int)this.ViewState["PositionID"];
        int ShopID = int.Parse(this.UCShop.ShopID);
        int FirstVolume = int.Parse(this.FirstVolumeCtl.Text);
        int SecondVolume = int.Parse(this.SecondVolumeCtl.Text);
        int ThirdVolume = int.Parse(this.ThirdVolumeCtl.Text);
        string Remark = this.RemarkCtl.Text;
        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.MaterialApplyBLL.AddFormMaterial(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID,
                    SystemEnums.FormType.MaterialApply, StatusID, ShopID, FirstVolume, SecondVolume, ThirdVolume, Remark, ViewState["FlowTemplate"].ToString());
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.MaterialApplyBLL.UpdateFormMaterial(FormID, StatusID, SystemEnums.FormType.MaterialApply, ShopID, FirstVolume, SecondVolume, ThirdVolume, Remark, ViewState["FlowTemplate"].ToString());
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }

    }

    public bool IsInputValid() {
        if (this.UCShop.ShopID == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请选择门店!");
            return false;
        }
        if (this.FirstVolumeCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入第一个月销量!");
            return false;
        } else {
            try {
                int.Parse(this.FirstVolumeCtl.Text);
            } catch (Exception ex) {
                PageUtility.ShowModelDlg(this.Page, "第一个月销量必须为整数!");
                return false;
            }
        }
        if (this.SecondVolumeCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入第二个月销量!");
            return false;
        } else {
            try {
                int.Parse(this.SecondVolumeCtl.Text);
            } catch (Exception ex) {
                PageUtility.ShowModelDlg(this.Page, "第二个月销量必须为整数!");
                return false;
            }
        }
        if (this.ThirdVolumeCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入第三个月销量!");
            return false;
        } else {
            try {
                int.Parse(this.ThirdVolumeCtl.Text);
            } catch (Exception ex) {
                PageUtility.ShowModelDlg(this.Page, "第三个月销量必须为整数!");
                return false;
            }
        }
        if (this.RemarkCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入申请理由!");
            return false;
        }
        return true;
    }

    public bool IsSubmitValid() {
        if (this.gvMaterialDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入明细项");
            return false;
        }
        return true;
    }

    #region 数据绑定及事件


    protected void odsMaterialDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        UserControls_MaterialControl newUCMaterial = (UserControls_MaterialControl)this.fvMaterialDetails.FindControl("newUCMaterial");
        if (newUCMaterial.MaterialID == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请选择广宣物资");
            e.Cancel = true;
            return;
        }
        TextBox newQuantityCtl = (TextBox)this.fvMaterialDetails.FindControl("newQuantityCtl");
        ERS.MaterialRow material = new MasterDataBLL().GetMaterialById(int.Parse(newUCMaterial.MaterialID));
        if (decimal.Parse(newQuantityCtl.Text) < material.MinimumNumber) {
            PageUtility.ShowModelDlg(this.Page, "申请数量不能小于最小领用数量（" + material.MinimumNumber.ToString() + "）");
            e.Cancel = true;
            return;
        }
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormMaterialID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }
    }

    protected void odsMaterialDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        MaterialApplyBLL bll = (MaterialApplyBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }


    protected void gvMaterialDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormMaterialDetailRow row = (FormDS.FormMaterialDetailRow)drvDetail.Row;
                TotalFee = decimal.Round((TotalFee + row.Amount), 2);
            }
        }

        this.ViewState["ManualApplyFeeTotal"] = TotalFee;

        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label applbl = (Label)e.Row.FindControl("lblTotal");
                applbl.Text = TotalFee.ToString("N");
            }
        }
    }

    protected void OnMaterialNameTextChanged(object sender, EventArgs e) {
        UserControls_MaterialControl newUCMaterial = (UserControls_MaterialControl)this.fvMaterialDetails.FindControl("newUCMaterial");
        TextBox newUOMCtl = (TextBox)this.fvMaterialDetails.FindControl("newUOMCtl");
        TextBox newDescCtl = (TextBox)this.fvMaterialDetails.FindControl("newDescCtl");
        TextBox newMaterialPriceCtl = (TextBox)this.fvMaterialDetails.FindControl("newMaterialPriceCtl");
        TextBox newQuantityCtl = (TextBox)this.fvMaterialDetails.FindControl("newQuantityCtl");
        TextBox newTotalCtl = (TextBox)this.fvMaterialDetails.FindControl("newTotalCtl");
        if (newUCMaterial.MaterialID == string.Empty) {
            newUOMCtl.Text = "";
            newDescCtl.Text = "";
            newMaterialPriceCtl.Text = "";
        } else {
            ERS.MaterialRow material = new MasterDataBLL().GetMaterialById(int.Parse(newUCMaterial.MaterialID));
            newUOMCtl.Text = material.UOM;
            newDescCtl.Text = material.Description;
            newMaterialPriceCtl.Text = material.MaterialPrice.ToString();

            if (newQuantityCtl.Text.Trim() != string.Empty) {
                newTotalCtl.Text = ((decimal)(decimal.Parse(newQuantityCtl.Text) * material.MaterialPrice)).ToString("N");
            }
        }
    }

    #endregion

}
