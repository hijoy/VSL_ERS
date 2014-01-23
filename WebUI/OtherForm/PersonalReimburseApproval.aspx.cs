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

public partial class OtherForm_PersonalReimburseApproval : BasePage {

    decimal TotalFee = 0;
    decimal TotalRealFee = 0;

    ListItemCollection m_itemList = new ListItemCollection();
    public ListItemCollection itemList {
        get { return m_itemList; }
    }

    private PersonalReimburseBLL m_PersonalReimburseBLL;
    protected PersonalReimburseBLL PersonalReimburseBLL {
        get {
            if (this.m_PersonalReimburseBLL == null) {
                this.m_PersonalReimburseBLL = new PersonalReimburseBLL();
            }
            return this.m_PersonalReimburseBLL;
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

    protected bool HasSaveRight {
        get {
            return (bool)this.ViewState["HasSaveRight"];
        }
        set {
            this.ViewState["HasSaveRight"] = value;
        }
    }

    #region ҳ���ʼ�����¼�����

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this.Page, "���˷��ñ�������");
            this.Page.Title = "���˷��ñ�������";

            int formID = int.Parse(Request["ObjectId"]);
            this.ViewState["ObjectId"] = formID;

            FormDS.FormRow rowForm = this.PersonalReimburseBLL.GetFormByID(formID)[0];
            FormDS.FormPersonalReimburseRow rowPersonalReimburse = this.PersonalReimburseBLL.GetFormPersonalReimburseByID(formID)[0];
            if (rowForm.IsProcIDNull()) {
                ViewState["ProcID"] = "";
            } else {
                ViewState["ProcID"] = rowForm.ProcID;
            }

            ViewState["OrganizationUnitID"] = rowForm.OrganizationUnitID;
            //�Կؼ����и�ֵ
            this.txtFormNo.Text = rowForm.FormNo;
            this.ApplyDateCtl.Text = rowForm.SubmitDate.ToShortDateString();
            AuthorizationDS.StuffUserRow applicant = new AuthorizationBLL().GetStuffUserById(rowForm.UserID);
            this.StuffNameCtl.Text = applicant.StuffName;
            this.PositionNameCtl.Text = new OUTreeBLL().GetPositionById(rowForm.PositionID).PositionName;
            if (new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID) != null) {
                this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID).OrganizationUnitName;
            }
            this.AttendDateCtl.Text = applicant.AttendDate.ToShortDateString();
            this.txtPeriod.Text = rowPersonalReimburse.Period.ToString("yyyyMM");
            if (!rowPersonalReimburse.IsRemarkNull()) {
                this.RemarkCtl.Text = rowPersonalReimburse.Remark;
            }

            // ����ϸ��
            FormPersonalReimburseDetailTableAdapter taDetail = new FormPersonalReimburseDetailTableAdapter();
            taDetail.FillByFormID(this.InnerDS.FormPersonalReimburseDetail, formID);

            //��ʷ����
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "��";
            } else {
                FormDS.FormRow rejectedForm = new ContractApplyBLL().GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/OtherForm/PersonalReimburseApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }

            //����ҳ�洦��&��ť����&Ԥ����Ϣ
            this.txtTotalBudget.Text = rowPersonalReimburse.TotalBudget.ToString("N");
            this.txtApprovingAmount.Text = rowPersonalReimburse.ApprovingAmount.ToString("N");
            this.txtApprovedAmount.Text = rowPersonalReimburse.ApprovedAmount.ToString("N");
            this.txtRemainAmount.Text = rowPersonalReimburse.RemainAmount.ToString("N");

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

            if (rowForm.StatusID == (int)SystemEnums.FormStatus.Rejected && stuffUser.StuffUserId == rowForm.UserID) {
                this.EditBtn.Visible = true;
                this.ScrapBtn.Visible = true;
            } else {
                this.EditBtn.Visible = false;
                this.ScrapBtn.Visible = false;
            }

            if (rowForm.UserID == stuffUser.StuffUserId) {
                this.BudgetTitleDIV.Visible = false;
                this.BudgetInfoDIV.Visible = false;
            }
            //����ǵ���,ȡ����ť���ɼ�
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.upButton.Visible = false;
                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                }
            }

            //�Ƿ���ʾ���ư�ť
            if (rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted && stuffUser.StuffUserId == rowForm.UserID) {
                this.UCPeriod.Visible = true;
                this.CopyBtn.Visible = true;
            } else {
                this.UCPeriod.Visible = false;
                this.CopyBtn.Visible = false;
            }

            //����ʵ����ť
            int opSaveId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FormPersonalReimburse, SystemEnums.OperateEnum.Other);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            if (new PositionRightBLL().CheckPositionRight(position.PositionId, opSaveId) && (rowForm.StatusID == 1 || rowForm.StatusID == 2)) {
                HasSaveRight = true;
            } else {
                HasSaveRight = false;
            }
            if (HasSaveRight) {
                this.SaveBtn.Visible = true;
            } else {
                this.SaveBtn.Visible = false;
            }

        }
        this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
        this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
        this.cwfAppCheck.IsView = (bool)this.ViewState["IsView"];
    }

    #endregion

    public string GetExpenseManageTypeNameById(object ID) {
        return new MasterDataBLL().GetExpenseManageTypeByID(int.Parse(ID.ToString())).ExpenseManageTypeName;
    }

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
        this.Response.Redirect("~/OtherForm/PersonalReimburseApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void CopyBtn_Click(object sender, EventArgs e) {
        string stringPeriod = ((TextBox)(this.UCPeriod.FindControl("txtDate"))).Text.Trim();
        if (stringPeriod == string.Empty) {
            PageUtility.ShowModelDlg(this, "��ѡ������ڼ�!");
            return;
        }
        DateTime Period = DateTime.Parse(stringPeriod.Substring(0, 4) + "-" + stringPeriod.Substring(4, 2) + "-01");
        try {
            this.PersonalReimburseBLL.CopyPersonalReimburseForm(int.Parse(this.ViewState["ObjectId"].ToString()), Period);
            this.Response.Redirect("~/Home.aspx");
        } catch (Exception exception) {
            PageUtility.DealWithException(this, exception);
        }
    }

    private bool FillDetail() {
        bool isValid = true;
        foreach (GridViewRow row in this.gvPersonalReimburseDetails.Rows) {
            if (row.RowType == DataControlRowType.DataRow) {
                FormDS.FormPersonalReimburseDetailRow detailRow = this.InnerDS.FormPersonalReimburseDetail[row.RowIndex];
                TextBox txtRealAmount = (TextBox)row.FindControl("txtRealAmount");
                if (string.IsNullOrEmpty(txtRealAmount.Text.Trim())) {
                    txtRealAmount.Text = "0";
                }

                decimal RealAmount = decimal.Parse(txtRealAmount.Text.Trim());
                if (RealAmount < 0) {
                    PageUtility.ShowModelDlg(this.Page, "����¼�븺��");
                    isValid = false;
                    break;
                }

                if (RealAmount > detailRow.Amount) {
                    PageUtility.ShowModelDlg(this.Page, "ʵ�����ܴ��ڱ������");
                    isValid = false;
                    break;
                }
                detailRow.RealAmount = RealAmount;
            }
        }
        return isValid;
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        this.PersonalReimburseBLL.FormDataSet = this.InnerDS;
        try {
            if (FillDetail()) {
                this.PersonalReimburseBLL.SaveRealAmountForPersonalReimburse(int.Parse(this.ViewState["ObjectId"].ToString()));
                this.Response.Redirect("~/Home.aspx");
            }
        } catch (Exception exception) {
            PageUtility.DealWithException(this, exception);
        }

    }

    protected void gvPersonalReimburseDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // �������н��и�ֵ
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormPersonalReimburseDetailRow row = (FormDS.FormPersonalReimburseDetailRow)drvDetail.Row;
                TotalFee = decimal.Round((TotalFee + row.Amount), 2);
                TotalRealFee = decimal.Round((TotalRealFee + row.RealAmount), 2);
                TextBox txtRealAmount = (TextBox)e.Row.FindControl("txtRealAmount");
                if (HasSaveRight) {
                    txtRealAmount.Attributes.Add("onBlur", "PlusTotal('totalRealAmountLbl',this);");
                    txtRealAmount.Attributes.Add("onFocus", "MinusTotal('totalRealAmountLbl',this)");
                } else {
                    txtRealAmount.ReadOnly = true;
                }

            }
        }

        this.ViewState["ManualApplyFeeTotal"] = TotalFee;

        if (e.Row.RowType == DataControlRowType.Footer) {
            Label sumlbl = new Label();
            sumlbl.Text = "�ϼƣ�";
            e.Row.Cells[1].Controls.Add(sumlbl);
            e.Row.Cells[1].CssClass = "RedTextAlignCenter";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].Width = new Unit("350px");

            Label totallbl = new Label();
            totallbl.Text = TotalFee.ToString("N");
            e.Row.Cells[2].CssClass = "RedTextAlignCenter";
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].Controls.Add(totallbl);
            e.Row.Cells[2].Width = new Unit("150px");

            Label totalRealAmountLbl = (Label)e.Row.FindControl("totalRealAmountLbl");
            totalRealAmountLbl.Text = TotalRealFee.ToString("N");
        }

    }

    protected void odsPersonalReimburseDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        PersonalReimburseBLL bll = (PersonalReimburseBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

}
