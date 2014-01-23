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
using System.Text;

public partial class Home : BasePage {
    private AuthorizationBLL _authBLL = new AuthorizationBLL();

    public string IsProxyStuffUserStyle {
        get {
            if (Session["ProxyStuffUserId"] != null) {
                return "display:none";
            } else {
                return string.Empty;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //lwy added
        base.Page_Load(sender, e);

        if (!this.IsPostBack) {
            if (Request["refer"] == "sp") {
                AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
                if (new AuthorizationBLL().GetProxyBusinessByProxyUserIDAndCurrentDate(stuffUser.StuffUserId, DateTime.Now).Count > 0) {
                    this.Response.Redirect("~/BusinessProxySelect.aspx");
                } else {
                    AuthorizationDS.PositionDataTable positions = new AuthorizationBLL().GetPositionByStuffUser(stuffUser.StuffUserId);
                    if (positions.Count > 1) {
                        this.Response.Redirect("~/PositionSelect.aspx");
                    } else {
                        this.Response.Redirect("~/Home.aspx");
                    }
                }
            } else {
                PageUtility.SetContentTitle(this, "首页");
                this.Page.Title = "首页";
                if (Session["ProxyStuffUserId"] != null) {
                    int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
                    int proxyStuffuserID = int.Parse(Session["ProxyStuffUserId"].ToString());

                    this.odsMyDraft.SelectParameters["queryExpression"].DefaultValue = "StatusId=" + ((int)SystemEnums.FormStatus.Draft).ToString() + " AND Form.UserID=" + stuffuserID.ToString() + " AND ProxyUserID = " + proxyStuffuserID.ToString();

                    this.odsMySubmitted.SelectParameters["queryExpression"].DefaultValue = "StatusId>=" + ((int)SystemEnums.FormStatus.Awaiting).ToString() +
                        " AND Form.UserID=" + stuffuserID.ToString() + " AND ProxyUserID = " + proxyStuffuserID.ToString();
                } else {
                    int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;

                    this.odsMyDraft.SelectParameters["queryExpression"].DefaultValue = "StatusId=" + ((int)SystemEnums.FormStatus.Draft).ToString() + " AND Form.UserID=" + stuffuserID.ToString();

                    this.odsMySubmitted.SelectParameters["queryExpression"].DefaultValue = "StatusId>=" + ((int)SystemEnums.FormStatus.Awaiting).ToString() +
                        " AND Form.UserId=" + stuffuserID.ToString();

                    this.odsMyAwaiting.SelectParameters["queryExpression"].DefaultValue = "charindex('P" + stuffuserID.ToString() + "P',InTurnUserIds)>0";
                }
            }
        }
    }

    protected void gvMyDraft_RowDataBound(object sender, GridViewRowEventArgs e) {
        //对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormViewRow row = (QueryDS.FormViewRow)drvDetail.Row;
                LinkButton lblFormType = (LinkButton)e.Row.Cells[1].FindControl("lblFormType");
                lblFormType.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/Home.aspx");

                Label lblApplyName = (Label)e.Row.FindControl("lblFormApplyName");
                lblApplyName.Text = CommonUtility.GetNameByFormID(row.FormTypeID, row.FormID);

                LinkButton lbtnFormType = (LinkButton)e.Row.FindControl("lblFormType");
                if (row.PageType == (int)SystemEnums.PageType.TravelReimburse) {
                    lbtnFormType.Text = "差旅费报销申请";
                } else {
                    lbtnFormType.Text = row.FormTypeName;
                }
            }
        }
    }

    protected void gvMySubmitted_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormViewRow row = (QueryDS.FormViewRow)drvDetail.Row;

                LinkButton lbFormNo = (LinkButton)e.Row.Cells[2].FindControl("lblFormNo");
                lbFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/Home.aspx");

                Label lblStatus = (Label)e.Row.Cells[3].FindControl("lblStatus");
                lblStatus.Text = CommonUtility.GetStatusName(row.StatusID);

                Label lblApplyName = (Label)e.Row.FindControl("lblFormApplyName");
                lblApplyName.Text = CommonUtility.GetNameByFormID(row.FormTypeID, row.FormID);

                if (!row.IsApprovedDateNull()) {
                    Label lblApproveDate = (Label)e.Row.FindControl("lblApproveDate");
                    lblApproveDate.Text = row.ApprovedDate.ToString("yyyy-MM-dd HH:mm");
                }
            }
        }
    }

    protected void gvMyAwaiting_RowDataBound(object sender, GridViewRowEventArgs e) {
        //对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormViewRow row = (QueryDS.FormViewRow)drvDetail.Row;

                Label lblApplyName = (Label)e.Row.FindControl("lblFormApplyName");
                lblApplyName.Text = CommonUtility.GetNameByFormID(row.FormTypeID, row.FormID);

                LinkButton lbFormNo = (LinkButton)e.Row.Cells[2].FindControl("lblFormNo");
                lbFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/Home.aspx");
            }
        }
    }

    protected void BulletinGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                ERS.BulletinRow row = (ERS.BulletinRow)drvDetail.Row;

                HyperLink TitleLink = (HyperLink)e.Row.Cells[0].FindControl("TitleLink");
                TitleLink.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/BulletinDetail.aspx?ObjectId=" + row.BulletinId + "','', 'dialogWidth:836px;dialogHeight:638px;resizable:yes;')";
            }
        }
    }

    public String GetCurrentUserByInturnID(object Ids) {
        String resultStr = string.Empty;
        if (Ids != null) {
            string inTurnUserId = Ids.ToString();
            string tempStr = string.Empty;
            if (inTurnUserId.Trim() != "P") {
                int index = 0;
                int index1 = 0;
                do {
                    index = inTurnUserId.IndexOf("P");
                    index1 = inTurnUserId.IndexOf('P', index + 1);
                    if (index < 0 || index1 < 0) {
                        break;
                    }
                    tempStr = inTurnUserId.Substring(index, index1 + 1);
                    tempStr = tempStr.Replace("P", "");
                    inTurnUserId = inTurnUserId.Substring(index1 + 1);
                    resultStr += this._authBLL.GetStuffUserById(int.Parse(tempStr)).StuffName + ", ";
                } while (inTurnUserId.Trim() != "P");
            }
        }
        return resultStr;
    }

}
