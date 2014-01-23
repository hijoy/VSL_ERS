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
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using BusinessObjects;

public partial class ReportManage_SalesReimburseDetailReport : System.Web.UI.Page {
    protected void Page_Load(object sender, System.EventArgs e) {
        if (!Page.IsPostBack) {
            PageUtility.SetContentTitle(this, "销售部核销明细");
            this.Page.Title = "销售部核销明细";
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SalesReimburseDetailReport, SystemEnums.OperateEnum.View);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            if (!HasViewRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            string reportName = string.Empty;
            Microsoft.Reporting.WebForms.ReportParameter[] ps = new Microsoft.Reporting.WebForms.ReportParameter[1];
            ps[0] = new Microsoft.Reporting.WebForms.ReportParameter("QueryExpression",getSearchCondition());
            reportName = "SalesReimburseDetail";
            this.ReportViewer.LoadReport(reportName, ps);
        }
    }

    protected bool checkSearchConditionValid() {
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();

        if (start == null || start == string.Empty) {
            if (end != null && end != string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择申请提交起始日期!");
                return false;
            }
        } else {
            if (end == null || end == string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择申请提交截止日期!");
                return false;
            } else {
                DateTime dtStart = DateTime.Parse(start);
                DateTime dtEnd = DateTime.Parse(end);
                if (dtStart > dtEnd) {
                    PageUtility.ShowModelDlg(this, "起始日期大于截止日期！");
                    return false;
                }
            }
        }
        return true;
    }

    private string getSearchCondition() {
        int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
        string filterStr = "ReimburseForm.StatusId >=" + ((int)SystemEnums.FormStatus.Awaiting).ToString();
        this.ViewState["SearchCondition"] = "Search=true";
        //报销单编号
        if (txtFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND ReimburseForm.FormNo like '%" + this.txtFormNo.Text.Trim() + "%'";
        }

        //申请单编号
        if (txtApplyFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND ApplyForm.FormNo like '%" + this.txtApplyFormNo.Text.Trim() + "%'";
        }

        //申请人姓名
        if (txtStuffUser.Text.Trim() != string.Empty) {
            filterStr += " AND ReimburseUser.StuffName like '%" + this.txtStuffUser.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&StuffName=" + this.txtStuffUser.Text.Trim();
        }

        //申请人组织机构
        if (this.UCOU.OUId != null) {
            filterStr += " AND charindex('P" + this.UCOU.OUId + "P',ReimbursePosition.OrganizationTreePath) > 0";
            this.ViewState["SearchCondition"] += "&UCOUID=" + this.UCOU.OUId;
        }

        //客户
        if (this.UCCustomer.CustomerID != string.Empty) {
            filterStr += " AND FormReimburse.CustomerID = " + UCCustomer.CustomerID;
            this.ViewState["SearchCondition"] += "&CustomerID=" + this.UCCustomer.CustomerID;
        }

        //支付方式
        if (!this.PaymentTypeDDL.SelectedValue.Equals("0")) {
            filterStr += " AND FormReimburse.PaymentTypeID = " + PaymentTypeDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&PaymentTypeID=" + PaymentTypeDDL.SelectedValue;
        }

        //申请日期
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND ReimburseForm.SubmitDate >='" + start + "' AND dateadd(day,-1,ReimburseForm.SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&ReimburseForm.SubmitDateStart=" + start + "&ReimburseForm.SubmitDateEnd=" + end;
        }

        //单据状态
        filterStr += getStatusID();

        return filterStr;
    }

    private string getStatusID() {
        string strStatusID = string.Empty;
        //单据状态
        if (chkAwaiting.Checked == true ||
            chkApproveCompleted.Checked == true) {
            if (chkAwaiting.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkAwaiting=true";
                if (strStatusID == string.Empty) {
                    strStatusID = ((int)SystemEnums.FormStatus.Awaiting).ToString();
                } else {
                    strStatusID += "," + ((int)SystemEnums.FormStatus.Awaiting).ToString();
                }
            }
            if (chkApproveCompleted.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkApproveCompleted=true";
                if (strStatusID == string.Empty) {
                    strStatusID = ((int)SystemEnums.FormStatus.ApproveCompleted).ToString();
                } else {
                    strStatusID += "," + ((int)SystemEnums.FormStatus.ApproveCompleted).ToString();
                }
            }
            strStatusID = " AND ReimburseForm.StatusId IN (" + strStatusID + ")";
        }
        return strStatusID;
    }
}
