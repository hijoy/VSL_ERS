using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BusinessObjects;

public partial class BaseData_Announcement : BasePage {
    MasterDataBLL ms = new MasterDataBLL();
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            //判断权限
            PageUtility.SetContentTitle(this, "系统公告");
            this.Page.Title = "系统公告";
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Bulletin, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Bulletin, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            this.HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            this.HasManageRight = (positionRightBLL.CheckPositionRight(position.PositionId, opManageId) && Session["ProxyStuffUserId"] == null);
            this.Opdiv.Visible = false;
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

    protected bool HasManageRight {
        get {
            return (bool)this.ViewState["HasManageRight"];
        }
        set {
            this.ViewState["HasManageRight"] = value;
        }
    }



    protected void BindData(LinkButton button) {
        GridViewRow gvr = (GridViewRow)button.Parent.Parent;
        int selectID = Convert.ToInt32(BulletinGridView.DataKeys[gvr.RowIndex].Value);
        this.EditID.Text = selectID.ToString();
        ERS.BulletinRow bulletin = ms.GetBulletinById(selectID)[0];

        string name = button.ID;
        this.EditBulletinTitleTextBox.Text = bulletin.BulletinTitle;
        this.EditBulletinContentTextBox.Text = bulletin.BulletinContent;
        this.EditIsActiveCheckBox.Checked = bulletin.IsActive;
        this.EditIsHotCheckBox.Checked = bulletin.IsHot;
        if (!bulletin.IsAttachFileNameNull())
            this.EditUCFileUpload.AttachmentFileName = bulletin.AttachFileName;
        else
            this.EditUCFileUpload.AttachmentFileName = null;
        if (!bulletin.IsRealAttachFileNameNull())
            this.EditUCFileUpload.RealAttachmentFileName = bulletin.RealAttachFileName;
        else
            this.EditUCFileUpload.RealAttachmentFileName = null;

        if (name == "TitleCtl") {
            EditBulletinTitleTextBox.ReadOnly = true;
            EditBulletinContentTextBox.ReadOnly = true;
            EditIsHotCheckBox.Enabled = false;
            EditIsActiveCheckBox.Enabled = false;
            tr1.Visible = false;
            this.EditUCFileUpload.IsView = true;
        }
        if (name == "EditBtn") {
            show();
        }
        this.Opdiv.Visible = true;
    }


    //点击标题
    protected void TitleCtl_Click(object sender, EventArgs e) {
        LinkButton button = (LinkButton)sender;
        BindData(button);
    }

    //编辑
    protected void Edit_Click(object sender, EventArgs e) {
        LinkButton button = (LinkButton)sender;
        BindData(button);
    }
    //添加
    protected void NewBtn_Click(object sender, EventArgs e) {
        clear();
        show();
        this.Opdiv.Visible = true;
    }

    //保存
    protected void btnSave_Click(object sender, EventArgs e) {
        if (this.EditBulletinTitleTextBox.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入标题");
            return;
        }
        if (this.EditBulletinContentTextBox.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入内容");
            return;
        }

        //新增
        if (string.IsNullOrEmpty(EditID.Text)) {
            ms.InsertBulletin(this.EditBulletinTitleTextBox.Text, this.EditBulletinContentTextBox.Text, this.EditIsHotCheckBox.Checked, this.EditUCFileUpload.AttachmentFileName, this.EditUCFileUpload.RealAttachmentFileName);
        }
            //更新
        else {
            ms.UpdateBulletin(int.Parse(this.EditID.Text), this.EditBulletinTitleTextBox.Text, this.EditBulletinContentTextBox.Text, this.EditIsHotCheckBox.Checked, this.EditIsActiveCheckBox.Checked, this.EditUCFileUpload.AttachmentFileName, this.EditUCFileUpload.RealAttachmentFileName);
        }
        this.BulletinGridView.DataBind();
        btnCancel_Click(sender, e);
    }


    //取消
    protected void btnCancel_Click(object sender, EventArgs e) {
        this.Opdiv.Visible = false;
        clear();
    }
    //删除
    protected void BulletinGridView_RowDeleted(object sender, GridViewDeletedEventArgs e) {
        this.Opdiv.Visible = false;
    }

    public void clear() {
        this.EditBulletinTitleTextBox.Text = "";
        this.EditBulletinContentTextBox.Text = "";
        this.EditIsActiveCheckBox.Checked = false;
        this.EditIsHotCheckBox.Checked = false;
        EditID.Text = "";
        this.EditUCFileUpload.AttachmentFileName = null;
        this.EditUCFileUpload.RealAttachmentFileName = null;
    }

    public void show() {
        EditBulletinTitleTextBox.ReadOnly = false;
        EditBulletinContentTextBox.ReadOnly = false;
        EditIsHotCheckBox.Enabled = true;
        EditIsActiveCheckBox.Enabled = true;
        tr1.Visible = true;
        this.EditUCFileUpload.IsView = false;
    }
}