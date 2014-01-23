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

public partial class ReportManage_SalesApplyExportReport : System.Web.UI.Page {
    protected void Page_Load(object sender, System.EventArgs e) {
        PageUtility.SetContentTitle(this, "方案申请导出");
        if (!IsPostBack) {
            //如果是弹出,取消按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                }
            }
            LoadReport();
        }
    }

    private void LoadReport() {
        string reportName = string.Empty;
        Microsoft.Reporting.WebForms.ReportParameter[] ps = new Microsoft.Reporting.WebForms.ReportParameter[3];
        if (Request["ExportType"] != null && Request["ExportType"] == "Good") {
            reportName = "SalesApply_Export(Good)";
        } else {
            reportName = "SalesApply_Export(Total)";
        }
        
        int stuffID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
        int positionID = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId;
        ps[0] = new Microsoft.Reporting.WebForms.ReportParameter("StuffUserID", stuffID.ToString());
        ps[1] = new Microsoft.Reporting.WebForms.ReportParameter("PositionID", positionID.ToString());
        ps[2] = new Microsoft.Reporting.WebForms.ReportParameter("QueryExpression", Session["QueryExpression"].ToString());
        //load report 
        this.ReportViewer.LoadReport(reportName, ps);
    }
}
