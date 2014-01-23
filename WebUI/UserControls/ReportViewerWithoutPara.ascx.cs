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

public partial class UserControls_ReportViewerWithoutPara : System.Web.UI.UserControl {
    /// <summary> 
    /// 
    /// </summary> 
    /// <param name="connectionType"></param> 
    /// <param name="reportName"></param> 
    /// <param name="parameters"></param> 
    /// <remarks></remarks> 
    public void LoadReport(string reportName, Microsoft.Reporting.WebForms.ReportParameter[] parameters) {
        ReportWS.ReportingService rs = new ReportWS.ReportingService();

        rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

        string reportFolder = System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ReportFolder");

        reportName = "/" + reportFolder + "/" + reportName;
        this.rptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        this.rptViewer.ServerReport.ReportServerUrl = new Uri(System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ReportServer"));
        this.rptViewer.ServerReport.ReportPath = reportName;
        this.rptViewer.Height = Height;
        this.rptViewer.Width = Width;
        //Credentials 
        Microsoft.Reporting.WebForms.DataSourceCredentials[] cr = new Microsoft.Reporting.WebForms.DataSourceCredentials[1];
        cr[0] = new Microsoft.Reporting.WebForms.DataSourceCredentials();


        this.rptViewer.ServerReport.SetParameters(parameters);
    }

    private int _Height;
    public int Height {
        set {
            _Height = value;
        }
        get {
            return _Height;
        }
    }

    private int _Width;
    public int Width {
        set {
            _Width = value;
        }
        get {
            return _Width;
        }
    }
}
