using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessObjects;

public partial class PageMasterData_BulletinDetail : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        try {
            //��ȡURL���ݲ���
            string strMode = this.Request.QueryString["mode"] == null ? "" : this.Request.QueryString["mode"].ToString();
            string strID = this.Request.QueryString["id"] == null ? "" : this.Request.QueryString["id"].ToString();

            //���ÿؼ�״̬
            setControlStatus(strMode);

            // ��ȡҳ��������Ϣ
            if (!strMode.Equals("") && !strMode.Equals("add") && !strID.Equals("")) {
                MasterDataBLL bll = new MasterDataBLL();
                ERS.BulletinDataTable dt = bll.GetBulletinById(Convert.ToInt32(strID));
                if (dt != null && dt.Count > 0) {
                    ERS.BulletinRow dr = dt[0];
                    txtTitle.Text = dr.BulletinTitle.ToString();
                    lblCreator.Text = dr.Creator.ToString();
                    lblCreateTime.Text = dr.CreateTime.ToString();
                    txtContent.Text = dr.BulletinContent.ToString();
                }
            }
        } catch (Exception ex) {
            PageUtility.DealWithException(this, ex);
        }
    }

    /// <summary>
    /// ����ҳ��Title�Լ��ؼ�״̬
    /// </summary>
    /// <param name="strMode"></param>
    private void setControlStatus(string strMode) {
        string strTitle = "";
        switch (strMode) {
            case "add":
                strTitle = "����ϵͳ����";
                txtContent.ReadOnly = false;
                txtTitle.ReadOnly = false;
                trCreator.Style["display"] = "none";
                trCreateTime.Style["display"] = "none";
                txtContent.CssClass = "InputText";
                txtTitle.CssClass = "InputText";
                break;
            case "update":
                strTitle = "ϵͳ�����޸�";
                txtContent.ReadOnly = false;
                txtTitle.ReadOnly = false;
                trCreator.Style.Remove("display");
                trCreateTime.Style.Remove("display");
                txtContent.CssClass = "InputText";
                txtTitle.CssClass = "InputText";
                break;
            case "view":
                txtContent.ReadOnly = true;
                txtTitle.ReadOnly = true;
                trCreator.Style.Remove("display");
                trCreateTime.Style.Remove("display");
                txtContent.CssClass = "InputTextReadonly";
                txtTitle.CssClass = "InputTextReadOnly";
                strTitle = "ϵͳ������ϸ";
                break;
        }
        PageUtility.SetContentTitle(this, strTitle);
    }

    protected void btnSubmit_Click(object sender, EventArgs e) {

    }
}
