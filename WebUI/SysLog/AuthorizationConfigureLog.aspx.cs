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
using System.Text;


public partial class SysLog_AuthorizationConfigureLog : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "权限设置日志");
            this.Page.Title = "权限设置日志";
        }
    }
    protected void SearchBtn_Click(object sender, EventArgs e) {
        StringBuilder queryExpression = new StringBuilder();
        if (!this.ObjectDropDownList.SelectedValue.ToString().Equals("全部")) {
            queryExpression.Append("ConfigureTarget = '" + this.ObjectDropDownList.SelectedValue + "'");
        }

        if (!this.ActionTypeDropDownList.SelectedValue.ToString().Equals("全部")) {
            if (queryExpression.Length > 0) {
                queryExpression.Append(" and ");
            }
            queryExpression.Append("ConfigureType = '" + this.ActionTypeDropDownList.SelectedValue + "'");
        }

        if (this.StuffIdCtl.Text.Trim().Length > 0) {
            if (queryExpression.Length > 0) {
                queryExpression.Append(" and ");
            }
            queryExpression.Append("StuffId like '%" + PageUtility.SafeSqlLiteral(this.StuffIdCtl.Text.Trim()) + "%'");
        }

        if (this.StuffNameCtl.Text.Trim().Length > 0) {
            if (queryExpression.Length > 0) {
                queryExpression.Append(" and ");
            }
            queryExpression.Append("StuffName like '%" + PageUtility.SafeSqlLiteral(this.StuffNameCtl.Text.Trim()) + "%'");
        }

        if (this.SrhAfterLogInTimeCtl.Text.Trim().Length > 0) {
            DateTime afterTime = DateTime.Parse(this.SrhAfterLogInTimeCtl.Text);
            if (queryExpression.Length > 0) {
                queryExpression.Append(" and ");
            }
            queryExpression.Append("ConfigureTime > '" + afterTime.ToString() + "'");
        }

        if (this.SrhBeforeLogInTimeCtl.Text.Trim().Length > 0) {
            DateTime beforeTime = DateTime.Parse(this.SrhBeforeLogInTimeCtl.Text);
            if (queryExpression.Length > 0) {
                queryExpression.Append(" and ");
            }
            queryExpression.Append("ConfigureTime < '" + beforeTime.ToString() + "'");
        }

        this.RightLogDS.SelectParameters["queryExpression"].DefaultValue = queryExpression.ToString();
        this.RightLogGridView.DataBind();
        this.GridViewUpdatePanel.Update();
    }    
}
