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
using System.Configuration;

public partial class UserControls_ReportViewerWithParameter : System.Web.UI.UserControl
{
    /// <summary> 
    /// 
    /// </summary> 
    /// <param name="connectionType"></param> 
    /// <param name="reportName"></param> 
    /// <param name="parameters"></param> 
    /// <remarks></remarks> 
    public void LoadReport(string reportName)    {

        ReportWS.ReportingService rs = new ReportWS.ReportingService();

        ////Create an instance of the CredentialCache class.
        //System.Net.CredentialCache cache =new System.Net.CredentialCache();

        //// Add a NetworkCredential instance to CredentialCache.
        //// Negotiate for NTLM or Kerberos authentication.
        ////cache.Add(new Uri(myProxy.Url), "Negotiate", new NetworkCredential("UserName", "Password", "Domain"));

        //cache.Add("192.168.1.30", 80, "Negotiate", new System.Net.NetworkCredential("wang", "smu", "grape-wang"));

        ////Assign CredentialCache to the Web service Client Proxy(myProxy) Credetials property.
        ////myProxy.Credentials = cache;


        rs.Credentials =System.Net.CredentialCache.DefaultCredentials;

        string reportFolder = System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ReportFolder");

        reportName = "/" + reportFolder + "/" + reportName;
        this.rptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        this.rptViewer.ServerReport.ReportServerUrl = new Uri(System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ReportServer"));
        this.rptViewer.ServerReport.ReportPath = reportName;

        //Credentials 
        Microsoft.Reporting.WebForms.DataSourceCredentials[] cr = new Microsoft.Reporting.WebForms.DataSourceCredentials[1];
        cr[0] = new Microsoft.Reporting.WebForms.DataSourceCredentials();

        //DataSource 
        //string conn = string.Empty;
        //conn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ERSConnectionString"].ConnectionString;


        //Parameters 
        // ERROR: Not supported in C#: ReDimStatement
        //parameters[parameters.Length-1] = new Microsoft.Reporting.WebForms.ReportParameter("conn", conn);


    } 

}
