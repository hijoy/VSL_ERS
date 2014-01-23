<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e) {

        
    }

    void Application_End(object sender, EventArgs e) {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e) {
        try
        {
            Exception objErr = Server.GetLastError().GetBaseException();
            this.Session["ErrorInfor"] = objErr.ToString();
            BusinessObjects.SysLog.LogSystemError(objErr);
            if (objErr is System.Data.SqlClient.SqlException)
            {
                Response.Redirect("~/ErrorPage/DatabaseErrorPage.aspx");
            }
            else
            {
                Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
            }
            Server.ClearError();
        }
        catch
        {
        }
    }

    void Session_Start(object sender, EventArgs e) {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }
       
</script>

