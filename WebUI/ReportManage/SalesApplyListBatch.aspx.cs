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

public partial class ReportManage_SalesApplyListBatchReport : System.Web.UI.Page {
    private string FormID;
    protected void Page_Load(object sender, System.EventArgs e) {
        if (!Page.IsPostBack) {
            HtmlControl div2 = (HtmlControl)this.Master.FindControl("DialogDiv2");
            div2.Visible = false;
            FormID = Request.Params["FormIDS"];
            if (FormID != null || FormID != string.Empty) {
                this.LoadReport(FormID);
            }
        }
    }

    protected void OnUnload(EventArgs e) {
        base.OnUnload(e);
        FormDS.FormApplyRow applyRow = new BusinessObjects.FormDSTableAdapters.FormApplyTableAdapter().GetDataByID(int.Parse(FormID))[0];
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='该方案已经被打印" + (applyRow.IsPrintCountNull() ? 0 : applyRow.PrintCount) + "次';" +
            "window.returnValue=arryReturn;</script>");
        Response.Write(@"<script language='javascript'>window.close();</script>");
    }

    private void LoadReport(string FormID) {
        string reportName = string.Empty;
        Microsoft.Reporting.WebForms.ReportParameter[] ps = new Microsoft.Reporting.WebForms.ReportParameter[1];
        reportName = "BatchSalesPromotionPrintReport";
        ps[0] = new Microsoft.Reporting.WebForms.ReportParameter("FormIDS", FormID);
        //load report 
        this.ReportViewer1.LoadReport(reportName, ps);
    }
}
