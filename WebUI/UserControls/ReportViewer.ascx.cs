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
using Microsoft.Reporting.WebForms;
using System.Net;

public partial class UserControls_ReportViewer : System.Web.UI.UserControl {
    /// <summary> 
    /// 
    /// </summary> 
    /// <param name="connectionType"></param> 
    /// <param name="reportName"></param> 
    /// <param name="parameters"></param> 
    /// <remarks></remarks> 
    public void LoadReport(string reportName, Microsoft.Reporting.WebForms.ReportParameter[] parameters) {

        //ReportWS.ReportingService rs = new ReportWS.ReportingService();

        ////Create an instance of the CredentialCache class.
        //System.Net.CredentialCache cache =new System.Net.CredentialCache();

        //// Add a NetworkCredential instance to CredentialCache.
        //// Negotiate for NTLM or Kerberos authentication.
        ////cache.Add(new Uri(myProxy.Url), "Negotiate", new NetworkCredential("UserName", "Password", "Domain"));

        //cache.Add("192.168.1.30", 80, "Negotiate", new System.Net.NetworkCredential("wang", "smu", "grape-wang"));

        ////Assign CredentialCache to the Web service Client Proxy(myProxy) Credetials property.
        ////myProxy.Credentials = cache;


        //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

        string reportFolder = System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ReportFolder");
        string reportUserName = System.Configuration.ConfigurationManager.AppSettings["ReportUserName"];
        string reportUserPwd = System.Configuration.ConfigurationManager.AppSettings["ReportUserPwd"];
        string reportDomain = System.Configuration.ConfigurationManager.AppSettings["ReportDomain"];

        reportName = "/" + reportFolder + "/" + reportName;
        this.rptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        IReportServerCredentials irsc = new CustomReportCredentials(reportUserName, reportUserPwd, reportDomain);
        //this.rptViewer.ServerReport.ReportServerCredentials = irsc;

        this.rptViewer.ServerReport.ReportServerUrl = new Uri(System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ReportServer"));
        this.rptViewer.ServerReport.ReportPath = reportName;

        //Credentials 
        //Microsoft.Reporting.WebForms.DataSourceCredentials[] cr = new Microsoft.Reporting.WebForms.DataSourceCredentials[1];
        //cr[0] = new Microsoft.Reporting.WebForms.DataSourceCredentials();

        //DataSource 
        //string conn = string.Empty;
        //conn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ERSConnectionString"].ConnectionString;


        //Parameters 
        // ERROR: Not supported in C#: ReDimStatement
        //parameters[parameters.Length-1] = new Microsoft.Reporting.WebForms.ReportParameter("conn", conn);
        if (parameters != null && parameters.Length > 0) {
            this.rptViewer.ServerReport.SetParameters(parameters);
        }
    }

    public class CustomReportCredentials : IReportServerCredentials {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public CustomReportCredentials(string UserName, string PassWord, string DomainName) {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser {
            get { return null; }
        }

        public ICredentials NetworkCredentials {
            get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user,
         out string password, out string authority) {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
}
