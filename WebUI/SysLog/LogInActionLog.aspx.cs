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

public partial class SysLog_LogInActionLog : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "�û���¼��־");
            this.Page.Title = "�û���¼��־";
        }
    }
    protected void SearchBtn_Click(object sender, EventArgs e) {
        StringBuilder queryExpression = new StringBuilder();
        if (this.SrhStuffNameCtl.Text.Trim().Length > 0) {
            queryExpression.Append("StuffName like '%" + PageUtility.SafeSqlLiteral(this.SrhStuffNameCtl.Text.Trim()) + "%'");
        }

        if (this.SrhStuffIdCtl.Text.Trim().Length > 0) {
            if (queryExpression.Length > 0) {
                queryExpression.Append(" and ");
            }
            queryExpression.Append("StuffId = '" + PageUtility.SafeSqlLiteral(this.SrhStuffIdCtl.Text.Trim()) + "'");
        }

        if (this.SrhClientIPCtl.Text.Trim().Length > 0) {
            if (queryExpression.Length > 0) {
                queryExpression.Append(" and ");
            }
            queryExpression.Append("ClientIP = '" + PageUtility.SafeSqlLiteral(this.SrhClientIPCtl.Text.Trim()) + "'");
        }
        if (!this.LogActoinList.SelectedValue.ToString().Equals("ȫ��")) {
            if (this.LogActoinList.SelectedValue.ToString().Equals("�ɹ�")) {
                if (queryExpression.Length > 0) {
                    queryExpression.Append(" and ");
                }
                queryExpression.Append("Success = 1");
            }

            if (this.LogActoinList.SelectedValue.ToString().Equals("ʧ��")) {
                if (queryExpression.Length > 0) {
                    queryExpression.Append(" and ");
                }
                queryExpression.Append("Success = 0");
            }
        }
        if (this.SrhAfterLogInTimeCtl.Text.Trim().Length > 0) {

            try {
                DateTime afterTime = DateTime.Parse(this.SrhAfterLogInTimeCtl.Text);

                if (queryExpression.Length > 0) {
                    queryExpression.Append(" and ");
                }
                queryExpression.Append("LogInTime > '" + afterTime.ToString() + "'");
            } catch (Exception ex) {
                PageUtility.ShowModelDlg(this, "���ڸ�ʽ����ȷ�����磺YYYY-MM-DD hh:mm");
                return;
            }
        }

        if (this.SrhBeforeLogInTimeCtl.Text.Trim().Length > 0) {
            try {
                DateTime beforeTime = DateTime.Parse(this.SrhBeforeLogInTimeCtl.Text);
                if (queryExpression.Length > 0) {
                    queryExpression.Append(" and ");
                }
                queryExpression.Append("LogInTime < '" + beforeTime.ToString() + "'");
            } catch (Exception ex) {
                PageUtility.ShowModelDlg(this, "���ڸ�ʽ����ȷ�����磺YYYY-MM-DD hh:mm");
                return;
            }
        }

        this.LogInActionLogDS.SelectParameters["queryExpression"].DefaultValue = queryExpression.ToString();
        this.LogInActionLogGridView.DataBind();
        this.GridViewUpdatePanel.Update();
    }
}
