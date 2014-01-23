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
using System.IO;
using System.Text;

public partial class UserControls_AttachmentFile : System.Web.UI.UserControl
{
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (IsView)
        {
            this.fileUpload.Style["display"] = "none";
            this.btnDelete.Visible = false;
            this.btnCancel.Visible = false;
            if (this.AttachmentFileName != null && !this.AttachmentFileName.Equals(string.Empty))
            {
                //string pathName = CommonUtility.GetPathName();

                //string path = this.Request.ApplicationPath + @"/" + pathName;
                //string serverFileFullName = path + @"/" + this.AttachmentFileName;
                this.lkDownload.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + @"/Dialog/download.aspx?FileName=" + this.AttachmentFileName;
                //this.lkDownload.NavigateUrl = "javascript:window.showModalDialog('" + this.Request.ApplicationPath + @"/Dialog/download.aspx?FileName=" + this.AttachmentFileName + "','', 'dialogWidth:860px;dialogHeight:700px;resizable:yes;')";
                this.lkDownload.Text = AttachmentFileName;
                //this.FUNameCtl.Value = AttachmentFileName;
                //this.FUSizeCtl.Value = AttachmentFileSize.ToString();
            }
        }
        else
        {
            if (this.AttachmentFileName!=null &&!this.AttachmentFileName.Equals(string.Empty))
            {
                this.fileUpload.Style["display"] = "none";
                this.btnDelete.Visible = true;
                this.btnCancel.Visible = false;
                //string pathName = CommonUtility.GetPathName();
                //string path = this.Request.ApplicationPath+@"/" + pathName;
                //string serverFileFullName = path + @"/" + this.AttachmentFileName;
                this.lkDownload.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + @"/Dialog/download.aspx?FileName=" + this.AttachmentFileName;
                //this.lkDownload.NavigateUrl = "javascript:window.showModalDialog('" + this.Request.ApplicationPath + @"/Dialog/download.aspx?FileName=" + this.AttachmentFileName + "','', 'dialogWidth:860px;dialogHeight:700px;resizable:yes;')";                
                this.lkDownload.Text = AttachmentFileName;
                //this.FUNameCtl.Value = AttachmentFileName;
                //this.FUSizeCtl.Value = AttachmentFileSize.ToString();
            }
            else
            {
                this.fileUpload.Style["display"] = "";
                this.btnDelete.Visible = false;
                this.lkDownload.Visible = false;
                if (this.OldFUNameCtl.Value != string.Empty)
                {
                    this.btnCancel.Visible = true;
                }
                else
                {
                    this.btnCancel.Visible = false;
                }
                //this.FUNameCtl.Value = "";
                //this.FUSizeCtl.Value = "";
            }
        }
    }

    #region property

    private bool _isView = false;
    public bool IsView
    {
        get
        {
            return _isView;
        }
        set
        {
            this._isView = value;
        }
    }

    public string CssClass
    {
        get
        {
            return this.fileUpload.CssClass;
        }
        set
        {
            this.fileUpload.CssClass = value;
        }
    }

    public Unit Width
    {
        get
        {
            return this.fileUpload.Width;
        }
        set
        {
            this.fileUpload.Width =value;
        }
    }

    public Unit Height
    {
        get
        {
            return this.fileUpload.Height;
        }
        set
        {
            this.fileUpload.Height = value;
        }
    }

    public string AttachmentFileName
    {
        get
        {
            if (this.FUNameCtl.Value.Length == 0)
            {
                return null;
            }
            else
            {
                return this.FUNameCtl.Value;
            }
        }
        set
        {
            if (value == null)
            {
                this.FUNameCtl.Value = "";
            }
            else
            {
                this.FUNameCtl.Value = value.ToString();
            }
        }
    }

    private int? _attachmentFileSize = null;
    public int? AttachmentFileSize
    {
        get
        {
            if (this.FUSizeCtl.Value.Length == 0)
            {
                return null;
            }
            else
            {
                return int.Parse(this.FUSizeCtl.Value);
            }
        }
        set
        {
            if (value == null)
            {
                this.FUSizeCtl.Value = "";
            }
            else
            {
                this.FUSizeCtl.Value = value.ToString();
            }
        }
    }

    #endregion


    #region "Public Event"

    public bool UploadFile()
    {
      
        bool returnValue = true;
        try
        {
            //if (this.fileUpload.HasFile)
            //{
                //if (CheckBeforeUpload())
                //{

            HttpFileCollection files = HttpContext.Current.Request.Files;

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile postedFile = files[i];

                if (postedFile.ContentLength > 2000000)
                {
                    PageUtility.ShowModelDlg(this.Parent.Page, "文件太大不能上传！");
                    return false;
                }

                string fileName, fileExtension;
                string saveName;

                fileName = Path.GetFileName(postedFile.FileName);
                if (fileName != null && fileName != "")
                {
                    fileExtension = Path.GetExtension(fileName);
                    if (postedFile == null)
                    {
                        PageUtility.ShowModelDlg(this.Parent.Page, "文件不存在！");
                        return false;
                    }
                    else
                    {
                        Random objRand = new Random();
                        DateTime date = DateTime.Now;

                        //生成随机文件名
                        saveName = date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + Convert.ToString(objRand.Next(99) * 97 + 100);
                        fileName = saveName + fileExtension;

                        this.FUNameCtl.Value = fileName;
                        this.FUSizeCtl.Value = postedFile.ContentLength.ToString();
                        string pathName = CommonUtility.GetPathName();
                        string path = Server.MapPath(@"~/" + pathName);
                        //string path = HttpContext.Current.Request.MapPath(@".\"+pathName);
                        string fullName = path + @"\" + fileName;
                        //
                        //保存文件
                        //
                        postedFile.SaveAs(fullName);
                    }
                }
            }



                    //
                    //建立上传对象
                    //
                    //HttpPostedFile postedFile = this.fileUpload.PostedFile;

                    //fileName = System.IO.Path.GetFileName(postedFile.FileName);
                    //fileExtension = System.IO.Path.GetExtension(fileName);

                    //
                    //根据日期和随机数生成随机的文件名
                    //       
                    //Random objRand = new Random();
                    //DateTime date = DateTime.Now;

                    ////生成随机文件名
                    //saveName = date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + Convert.ToString(objRand.Next(99) * 97 + 100);
                    //fileName = saveName + fileExtension;

                    //this.FUNameCtl.Value = fileName;
                    //this.FUSizeCtl.Value = postedFile.ContentLength.ToString();
                    //string pathName = CommonUtility.GetPathName();
                    //string path = Server.MapPath(@"~/" + pathName);
                    ////string path = HttpContext.Current.Request.MapPath(@".\"+pathName);
                    //string fullName = path + @"\" + fileName;
                    ////
                    ////保存文件
                    ////
                    //postedFile.SaveAs(fullName);
                //}
            //}
        }
        catch
        {
            PageUtility.ShowModelDlg(this.Parent.Page, "上传文件失败");
            returnValue = false;
        }
        return returnValue;
   
    }

    public void DoDeleteFileOnServer(string fileName)
    {
        try
        {
            string pathName = CommonUtility.GetPathName();

            string path = Server.MapPath(@"~/" + pathName);

            string serverFileFullName = path + @"\" + fileName;

            if (System.IO.File.Exists(serverFileFullName))
            {
                File.Delete(serverFileFullName);
            }
        }
        catch(Exception ex)
        {
            PageUtility.DealWithException(this.Page, ex);            
        }
    }

    #endregion

    #region script

    protected string GetResetScript()
    {
        StringBuilder script = new StringBuilder();
        script.Append(@"document.getElementById('" + this.FUNameCtl.ClientID + @"').value = '';
                        document.getElementById('" + this.FUSizeCtl.ClientID + @"').value = '';
                        document.getElementById('" + this.fileUpload.ClientID + @"').Style = '';
                        document.getElementById('" + this.lkDownload.ClientID + @"').Style = 'display:none';
                        document.getElementById('" + this.btnDelete.ClientID + @"').Style = 'display:none';");
        return script.ToString();
    }
    #endregion

    #region "Internal Method"

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        this.OldFUNameCtl.Value = this.FUNameCtl.Value;
        this.OldFUSizeCtl.Value = this.FUSizeCtl.Value;
        this.FUNameCtl.Value = "";
        this.FUSizeCtl.Value = "";
        this.fileUpload.Style["display"] = "";
        this.btnCancel.Visible = true;
        this.lkDownload.Visible = false;
        this.btnDelete.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.FUNameCtl.Value = this.OldFUNameCtl.Value;
        this.FUSizeCtl.Value = this.OldFUSizeCtl.Value;
        this.OldFUNameCtl.Value = "";
        this.OldFUSizeCtl.Value = "";
        this.fileUpload.Style["display"] = "none";
        this.btnCancel.Visible = false;
        this.lkDownload.Visible = true;
        this.btnDelete.Visible = true;
    }

    /// <summary>
    /// Check Before Upload
    /// </summary>
    private bool CheckBeforeUpload()
    {      
        HttpFileCollection files = HttpContext.Current.Request.Files;

        for (int i = 0; i < files.Count; i++)
        {
            HttpPostedFile postedFile = files[i];

            if (postedFile.ContentLength > 2000000)
            {
                PageUtility.ShowModelDlg(this.Parent.Page, "文件太大不能上传！");
                return false;
            }

            string fileName, fileExtension;

            fileName = Path.GetFileName(postedFile.FileName);
            if (fileName != null && fileName != "")
            {
                fileExtension = Path.GetExtension(fileName);
                if (postedFile == null)
                {
                    PageUtility.ShowModelDlg(this.Parent.Page, "文件不存在！");
                    return false;
                }
            }
        }

        return true;

        //string fileName = Path.GetFileName(postedFile.FileName);
        //if (fileName != null && fileName != "")
        //{
        //    //Check If File Exist
        //    FileInfo file = new FileInfo(postedFile.FileName);
        //    if (!file.Exists)
        //    {
        //        PageUtility.ShowModelDlg(this.Page, "文件不存在！");
        //        isValid = false;
        //    }
        //}

        //return isValid;
    }

    #endregion
}

