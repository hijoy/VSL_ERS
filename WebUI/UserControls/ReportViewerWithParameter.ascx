<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_ReportViewerWithParameter" Codebehind="ReportViewerWithParameter.ascx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    
<style>

html,body,form 
{height:100%}

</style>
 
<rsweb:reportviewer id="rptViewer" runat="server" EnableTheming="True"   ShowParameterPrompts="true"  AsyncRendering="true" Width="100%" Height="90%" ShowFindControls="False">
    <ServerReport ReportServerUrl="" />
</rsweb:reportviewer>