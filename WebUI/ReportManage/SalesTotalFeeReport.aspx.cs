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

public partial class ReportManage_SalesPrintSingleReport : System.Web.UI.Page {
    protected void Page_Load(object sender, System.EventArgs e) {
        PageUtility.SetContentTitle(this, "总费用管控");
        this.Page.Title = "总费用管控";
        int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SalesTotaFeeReport, SystemEnums.OperateEnum.View);
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
        reportName = "SalesTotalFee";
        AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
        ps[0] = new Microsoft.Reporting.WebForms.ReportParameter("OUID", position.OrganizationUnitId.ToString());
        ps[1] = new Microsoft.Reporting.WebForms.ReportParameter("EndDate", this.UCDateInputEndDate.SelectedDate);
        //load report 
        this.ReportViewer.LoadReport(reportName, ps);
    }
    protected void btn_search_Click(object sender, EventArgs e) {
        if (this.UCDateInputEndDate.SelectedDate == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择截止日期！");
            return;
        }
        LoadReport();
    }
}
