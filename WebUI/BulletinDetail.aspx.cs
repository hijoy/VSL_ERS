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

public partial class BulletinDetail : Page {
    protected void Page_Load(object sender, EventArgs e) {
        this.Page.Title = "系统公告";
        MasterDataBLL bll = new MasterDataBLL();
        ERS.BulletinRow bulletin = bll.GetBulletinById(int.Parse(Request["ObjectId"]))[0];
        this.BulletinTitleLabel.Text = bulletin.BulletinTitle;
        this.BulletinContentCtl.Text = bulletin.BulletinContent;
        this.CreateTimeLabel.Text = bulletin.CreateTime.ToString("yyyy-MM-dd");
        if (!bulletin.IsAttachFileNameNull())
            this.ViewUCFileUpload.AttachmentFileName = bulletin.AttachFileName;
        if (!bulletin.IsRealAttachFileNameNull())
            this.ViewUCFileUpload.RealAttachmentFileName = bulletin.RealAttachFileName;
    }
}
