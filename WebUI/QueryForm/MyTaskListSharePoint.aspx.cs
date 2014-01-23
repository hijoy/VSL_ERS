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

public partial class QueryForm_MyTaskListSharePoint : BasePage {
    private StuffUserBLL m_BLL;
    public StuffUserBLL BLL {
        get {
            if (m_BLL == null) {
                m_BLL = new StuffUserBLL();
            }
            return m_BLL;
        }
    }
    protected void Page_Load(object sender, EventArgs e) {
        //base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            if (!string.IsNullOrEmpty(Request.QueryString["extra"])) {
                string username = Request.QueryString["extra"];
                if (username.IndexOf("\\") > -1) {
                    username = username.Split('\\')[1].ToString();
                }

                BusinessObjects.AuthorizationDS.StuffUserDataTable table = this.BLL.GetStuffUserByUserId(username);
                if (table.Count < 1) {
                    this.odsMyAwaiting.SelectParameters["queryExpression"].DefaultValue = "1=2";
                } else {
                    AuthorizationDS.PositionDataTable positions = new AuthorizationBLL().GetPositionByStuffUser(table[0].StuffUserId);
                    AuthorizationDS.StuffUserRow user = table[0];
                    Session.Clear();
                    Session["StuffUser"] = table[0];
                    if (positions!=null&&positions.Count >= 1) {
                        this.Session["SelectedPosition"] = positions[0];
                        Session["Position"] = positions[0];
                    }
                    int stuffuserID = table[0].StuffUserId;
                    this.odsMyAwaiting.SelectParameters["queryExpression"].DefaultValue = "charindex('P" + stuffuserID.ToString() + "P',InTurnUserIds)>0";
                }
            } else {
                this.odsMyAwaiting.SelectParameters["queryExpression"].DefaultValue = "1=2";
            }
        }
    }

    protected void gvMyAwaiting_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormViewRow row = (QueryDS.FormViewRow)drvDetail.Row;
                HtmlAnchor link = (HtmlAnchor)e.Row.FindControl("FormNo");
                link.HRef = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/Home.aspx");

            }
        }
    }
}