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

public partial class ReportManage_SalesAccruedTotalFee : System.Web.UI.Page {
    protected void Page_Load(object sender, System.EventArgs e) {
        PageUtility.SetContentTitle(this, "����ͳ�Ʊ���");
        this.Page.Title = "����ͳ�Ʊ���";
        int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SalesAccruedTotalFeeReport, SystemEnums.OperateEnum.View);
        AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
        PositionRightBLL positionRightBLL = new PositionRightBLL();
        bool HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
        if (!HasViewRight) {
            Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
            return;
        }
    }

    private void LoadReport() {
        string reportName = string.Empty;
        Microsoft.Reporting.WebForms.ReportParameter[] ps = new Microsoft.Reporting.WebForms.ReportParameter[2];
        reportName = "SalesAccruedTotalFee";
        ps[0] = new Microsoft.Reporting.WebForms.ReportParameter("OUID", this.UCOU.OUId.ToString());
        ps[1] = new Microsoft.Reporting.WebForms.ReportParameter("EndDate", this.UCDateInputEndDate.SelectedDate);
        //load report 
        this.ReportViewer.LoadReport(reportName, ps);
    }
    protected void btn_search_Click(object sender, EventArgs e) {
        if (this.UCOU.OUId == null || this.UCOU.OUId <= 0) {
            PageUtility.ShowModelDlg(this, "��ѡ���ţ�");
            return;
        }
        if (this.UCDateInputEndDate.SelectedDate == string.Empty) {
            PageUtility.ShowModelDlg(this, "��ѡ���ֹ���ڣ�");
            return;
        }
        LoadReport();
    }
}
