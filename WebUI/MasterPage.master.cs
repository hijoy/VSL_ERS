using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessObjects;
using BusinessObjects.AuthorizationDSTableAdapters;

public partial class MasterPage : System.Web.UI.MasterPage {
    protected void Page_Load(object sender, EventArgs e) {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //lwy, migrate from mainpage
        if (Session["StuffUser"] == null) {
            this.NavigateMenu.Visible = false;
            return;
        }
        if (!IsPostBack) {
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            if (Session["ProxyStuffUserId"] != null) {
                this.Session["Position"] = new OUTreeBLL().GetPositionById((int)Session["PositionId"]);
                this.Session["StuffUser"] = new StuffUserBLL().GetStuffUserById((int)Session["StuffUserId"])[0];
                List<string> useRootItems = new List<string>();
                useRootItems.Add(" 申请与报销");
                //useRootItems.Add("个人费用报销申请");
                //useRootItems.Add("出差申请");
                //useRootItems.Add("出差报销申请");
                this.CustomizeNavigateMenu();
                List<MenuItem> noUseRootItems = new List<MenuItem>();
                foreach (MenuItem rootItem in this.NavigateMenu.Items) {
                    if (!useRootItems.Contains(rootItem.Text)) {
                        noUseRootItems.Add(rootItem);
                    }
                }
                foreach (MenuItem noUseRootItem in noUseRootItems) {
                    this.NavigateMenu.Items.Remove(noUseRootItem);
                }

                this.CustomizeNavigateMenu();
            } else {
                AuthorizationBLL bll = new AuthorizationBLL();
                AuthorizationDS.PositionDataTable positions = bll.GetPositionByStuffUser(stuffUser.StuffUserId);
                if (positions.Count == 0) {
                    PageUtility.ShowModelDlg(this.Page, "没有设置职务,请联系系统管理员");
                    this.Response.Redirect("~/ErrorPage/NoPositionErrorPage.aspx");
                    return;
                }
                this.PositionSelectCtl.Items.Clear();
                foreach (AuthorizationDS.PositionRow position in positions) {
                    this.PositionSelectCtl.Items.Add(new ListItem(position.PositionName, position.PositionId.ToString()));
                }
                if (positions.Count > 0) {
                    this.PositionSelectCtl.Visible = true;
                    this.PositionSelectLabel.Visible = true;
                }
                if (this.Session["SelectedPosition"] == null) {
                    int currentPositionId = int.Parse(this.PositionSelectCtl.SelectedValue);
                    Session["Position"] = new OUTreeBLL().GetPositionById(currentPositionId);
                } else {
                    Session["Position"] = Session["SelectedPosition"];
                    this.PositionSelectCtl.SelectedValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
                }
                if (Session["Position"] == null) {
                    int currentPositionId = int.Parse(this.PositionSelectCtl.SelectedValue);
                    Session["Position"] = new OUTreeBLL().GetPositionById(currentPositionId);
                } else {
                    this.PositionSelectCtl.SelectedValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
                }
            }
            AuthorizationDS.StuffUserRow user = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            this.StuffNameCtl.Text = user.StuffName;
            this.LastLogInTimeCtl.Text = user.IsLaterLogInTimeNull() ? "" : user.LaterLogInTime.ToShortDateString();
        }
        this.CustomizeNavigateMenu();
    }

    protected void Page_Error(Object sender, EventArgs args) {
        Exception e = Server.GetLastError();
        this.ModelDlgContentLiteral.Text = e.Message;
        this.ModelDlg.Style["display"] = "none";
        this.ModelDlgUpdatePanel.Update();
        Server.ClearError();
    }

    protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e) {
        throw e.Exception;
    }

    ///lwy, migrate from main page
    private void CustomizeNavigateMenu() {
        List<MenuItem> rootItems = new List<MenuItem>();
        foreach (MenuItem rootItem in this.NavigateMenu.Items) {
            rootItems.Add(rootItem);
        }

        List<string> uiEntryCodes = new List<string>();
        uiEntryCodes.AddRange(new AuthorizationBLL().GetEnabledUIEntryCode(((AuthorizationDS.PositionRow)this.Session["Position"]).PositionId));

        foreach (MenuItem item in rootItems) {
            this.CheckItemRight(item, uiEntryCodes);
        }
    }

    private void CheckItemRight(MenuItem item, List<string> uiEntryCodes) {
        if (item.Value.Equals("Folder")) {
            List<MenuItem> childItems = new List<MenuItem>();
            foreach (MenuItem childItem in item.ChildItems) {
                childItems.Add(childItem);
            }
            foreach (MenuItem childItem in childItems) {
                if (Session["ProxyStuffUserId"] != null && (childItem.Text == "等待我确认执行的申请单" || childItem.Text == "方案报销")) {
                    RemoveItem(childItem);
                }
                CheckItemRight(childItem, uiEntryCodes);
            }
            if (item.ChildItems.Count == 0) {
                RemoveItem(item);
            }
        } else if (!item.Value.Equals("open")) {
            if (!uiEntryCodes.Contains(item.Value)) {
                RemoveItem(item);
            }
        }
    }

    private void RemoveItem(MenuItem item) {
        if (item.Parent != null) {
            item.Parent.ChildItems.Remove(item);
        } else {
            this.NavigateMenu.Items.Remove(item);
        }
    }

    protected void PositionSelectCtl_SelectedIndexChanged(object sender, EventArgs e) {
        int currentPositionId = int.Parse(this.PositionSelectCtl.SelectedValue);
        Session["SelectedPosition"] = new OUTreeBLL().GetPositionById(currentPositionId);
        //Response.Redirect("~/MainPage.aspx");
        Response.Redirect("~/Home.aspx");
    }

    protected void LogOutBtn_Click(object sender, EventArgs e) {
        Session.Clear();
        Response.Redirect("~/LogIn.aspx");
    }

    protected void ModelDlgCloseBtn_Click(object sender, EventArgs e) {
        HtmlControl panel = (HtmlControl)this.FindControl("ModelDlg");
        panel.Style["display"] = "none";
        this.ModelDlgUpdatePanel.Update();
    }

    public String divAlert_ClientID {
        get {
            return this.ModelDlgUpdatePanel.FindControl("divAlert").ClientID;
        }
    }
}
