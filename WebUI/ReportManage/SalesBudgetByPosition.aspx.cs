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
public partial class ReportManage_SalesBudgetByPosition : System.Web.UI.Page {
    protected void Page_Load(object sender, System.EventArgs e) {
        this.Page.Title = "当前部门预算";
        if (!Page.IsPostBack) {
            HtmlControl div2 = (HtmlControl)this.Master.FindControl("DialogDiv2");
            div2.Visible = false;
            loadReport();
        }
    }
    private void loadReport() {
        string reportName = "SalesBudgetByPosition";
        AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
        Microsoft.Reporting.WebForms.ReportParameter[] ps = new Microsoft.Reporting.WebForms.ReportParameter[2];
        ps[0] = new Microsoft.Reporting.WebForms.ReportParameter("PositionID", position.PositionId.ToString());
        ps[1] = new Microsoft.Reporting.WebForms.ReportParameter("Year", DateTime.Now.Year.ToString());
        //load report 
        this.ReportViewer.LoadReport(reportName, ps);
    }
}
