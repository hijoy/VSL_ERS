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

public partial class OtherForm_MaterialApproval : BasePage {

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
            PageUtility.SetContentTitle(this.Page, "广宣物资审批");
            this.Page.Title = "广宣物资审批";

            int formID = int.Parse(Request["ObjectId"]);
            this.ViewState["ObjectId"] = formID;
            FormDS.FormRow rowForm = this.MaterialApplyBLL.GetFormByID(formID)[0];
            FormDS.FormMaterialRow rowFormMaterial = this.MaterialApplyBLL.GetFormMaterialByID(formID)[0];
            if (rowForm.IsProcIDNull()) {
                ViewState["ProcID"] = "";
            } else {
                ViewState["ProcID"] = rowForm.ProcID;
            }

            ViewState["OrganizationUnitID"] = rowForm.OrganizationUnitID;
            this.FormNoCtl.Text = rowForm.FormNo;
            this.ApplyDateCtl.Text = rowForm.SubmitDate.ToShortDateString();
            AuthorizationDS.StuffUserRow applicant = new AuthorizationBLL().GetStuffUserById(rowForm.UserID);
            this.StuffNameCtl.Text = applicant.StuffName;
            this.PositionNameCtl.Text = new OUTreeBLL().GetPositionById(rowForm.PositionID).PositionName;
            if (new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID) != null) {
                this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID).OrganizationUnitName;
            }
            this.AttendDateCtl.Text = applicant.AttendDate.ToShortDateString();

            ERS.ShopRow shop = new MasterDataBLL().GetShopByID(rowFormMaterial.ShopID);
            this.CustomerNameCtl.Text = new MasterDataBLL().GetCustomerById(shop.CustomerID).CustomerName;
            this.ShopNameCtl.Text = shop.ShopName;
            this.FirstVolumeCtl.Text = rowFormMaterial.FirstVolume.ToString("N");
            this.SecondVolumeCtl.Text = rowFormMaterial.SecondVolume.ToString("N");
            this.ThirdVolumeCtl.Text = rowFormMaterial.ThirdVolume.ToString("N");
            this.AverageVolumeCtl.Text = rowFormMaterial.AverageVolume.ToString("N");
            this.RemarkCtl.Text = rowFormMaterial.Remark;

            this.odsMaterialDetails.SelectParameters["FormMaterialID"].DefaultValue = rowFormMaterial.FormMaterialID.ToString();

            //审批页面处理&按钮处理
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            if (rowForm.InTurnUserIds.Contains("P" + stuffUser.StuffUserId + "P")) {
                this.SubmitBtn.Visible = true;
                this.cwfAppCheck.IsView = false;
                this.ViewState["IsView"] = false;
            } else {
                this.SubmitBtn.Visible = false;
                this.cwfAppCheck.IsView = true;
                this.ViewState["IsView"] = true;
            }

            if (rowForm.StatusID == (int)SystemEnums.FormStatus.Rejected && ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId == rowForm.UserID) {
                this.EditBtn.Visible = true;
                this.ScrapBtn.Visible = true;
            } else {
                this.EditBtn.Visible = false;
                this.ScrapBtn.Visible = false;
            }

            //如果是弹出,取消按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.upButton.Visible = false;
                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                }
            }

           
        }
        this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
        this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
        this.cwfAppCheck.IsView = (bool)this.ViewState["IsView"];
    }

    #endregion

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        try {
            if (this.cwfAppCheck.CheckInputData()) {
                AuthorizationDS.StuffUserRow currentStuff = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
                string ProxyStuffName = null;
                if (Session["ProxyStuffUserId"] != null) {
                    ProxyStuffName = new StuffUserBLL().GetStuffUserById(int.Parse(Session["ProxyStuffUserId"].ToString()))[0].StuffName;
                }
                new APFlowBLL().ApproveForm(CommonUtility.GetAPHelper(Session), this.cwfAppCheck.FormID, currentStuff.StuffUserId, currentStuff.StuffName,
                            this.cwfAppCheck.GetApproveOrReject(), this.cwfAppCheck.GetComments(), ProxyStuffName, int.Parse(ViewState["OrganizationUnitID"].ToString()));
                if (this.Request["Source"] != null) {
                    this.Response.Redirect(this.Request["Source"].ToString());
                } else {
                    this.Response.Redirect("~/Home.aspx");
                }
            }
        } catch (Exception exception) {
            this.cwfAppCheck.ReloadCtrl();
            PageUtility.DealWithException(this, exception);
        }
    }

    protected void CancelBtn_Click(object sender, EventArgs e) {
        if (this.Request["Source"] != null) {
            this.Response.Redirect(this.Request["Source"].ToString());
        } else {
            this.Response.Redirect("~/Home.aspx");
        }
    }

    protected void EditBtn_Click(object sender, EventArgs e) {
        this.Response.Redirect("~/OtherForm/MaterialApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
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
            Label sumlbl = new Label();
            sumlbl.Text = "合计";
            e.Row.Cells[4].Controls.Add(sumlbl);
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].CssClass = "RedTextAlignCenter";
            e.Row.Cells[4].Width = new Unit("100px");

            Label totallbl = new Label();
            totallbl.Text = TotalFee.ToString("N");
            e.Row.Cells[5].Controls.Add(totallbl);
            e.Row.Cells[5].CssClass = "RedTextAlignCenter";
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].Width = new Unit("100px");
        }
    }
}
