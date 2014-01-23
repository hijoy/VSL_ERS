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

public partial class EmailHistory : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "邮件发送历史");
            this.Page.Title = "邮件发送历史";
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.EmailHistory, SystemEnums.OperateEnum.View);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            this.HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            if (!this.HasViewRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }

    protected bool HasViewRight {
        get {
            return (bool)this.ViewState["HasViewRight"];
        }
        set {
            this.ViewState["HasViewRight"] = value;
        }
    }

    #region Shop event

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            if (this.Request["StartIndex"] != null) {
                string start = this.Request["StartIndex"].ToString();
                this.gvEmailHistory.PageIndex = int.Parse(start);
            }
            this.odsEmailHistory.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
            this.gvEmailHistory.DataBind();
        }
    }

    private string getSearchCondition() {
        string filterStr = "1=1";
        this.ViewState["SearchCondition"] = "Search=true";
        //收件人邮箱
        if (txtSendTo.Text.Trim() != string.Empty) {
            filterStr += " AND SentTo like '%" + this.txtSendTo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormNo=" + this.txtSendTo.Text.Trim();
        }

        //发送日期
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();

        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND SendDate >='" + start + "' AND dateadd(day,-1,SendDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SendDateStart=" + start + "&SendDateEnd=" + end;
        }

        if(this.ddlResult.SelectedValue!="") {
            filterStr += " and ResultType=" + this.ddlResult.SelectedValue;
        }

        return filterStr;
    }

    protected bool checkSearchConditionValid() {
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();

        if (start == null || start == string.Empty) {
            if (end != null && end != string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择申请提交起始日期!");
                return false;
            }
        } else {
            if (end == null || end == string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择申请提交截止日期!");
                return false;
            } else {
                DateTime dtStart = DateTime.Parse(start);
                DateTime dtEnd = DateTime.Parse(end);
                if (dtStart > dtEnd) {
                    PageUtility.ShowModelDlg(this, "起始日期大于截止日期！");
                    return false;
                }
            }
        }
        return true;
    }

    protected string GetResultTypeName(object ResultType) {
        return (int)ResultType == 0 ? "失败" : "成功";
    }

    #endregion

}
