using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;
using System.Threading;

public partial class UserControls_MultiAttachmentFile : System.Web.UI.UserControl {
    #region property

    private bool _isView = false;

    public bool IsView {
        get {
            return _isView;
        }
        set {
            this._isView = value;
        }
    }

    public Unit Width {
        get {
            return this.gvMultiAttachmentFile.Width;
        }
        set {
            this.gvMultiAttachmentFile.Width = value;
            this.fileUpload.Width = Unit.Parse((value.Value * 0.8 - this.btAddFile.Width.Value - 10).ToString());
        }
    }

    public Unit Height {
        get {
            return this.fileUpload.Height;
        }
        set {
            this.fileUpload.Height = value;
        }
    }

    public string AttachmentFileName {
        get {
            if (this.FUNameCtl.Value.Length == 0 && this.NewFUNameCtl.Value.Length == 0) {
                return null;
            } else {
                string strAttachmentNameOrg = this.FUNameCtl.Value;
                string strAttachmentNameNew = this.NewFUNameCtl.Value;
                if (this.DeleteFUNameCtl.Value.Length != 0) {
                    string strDeleteAttachmentName = this.DeleteFUNameCtl.Value;
                    string[] deleteArg = strDeleteAttachmentName.Split(new char[] { ';' });
                    string[] attachmentNameOrgArg = strAttachmentNameOrg.Split(new char[] { ';' });
                    string[] attachmentNameNewArg = strAttachmentNameNew.Split(new char[] { ';' });
                    int lenOrg = attachmentNameOrgArg.Length;
                    int lenNew = attachmentNameNewArg.Length;
                    int len = deleteArg.Length;
                    for (int i = 0; i < len; i++) {
                        strAttachmentNameOrg = strAttachmentNameOrg.Replace(";" + deleteArg[i], "");
                        attachmentNameOrgArg = strAttachmentNameOrg.Split(new char[] { ';' });
                        int lenReplace = attachmentNameOrgArg.Length;
                        if (lenOrg == lenReplace) {
                            strAttachmentNameOrg = strAttachmentNameOrg.Replace(deleteArg[i], "");
                        }
                        strAttachmentNameNew = strAttachmentNameNew.Replace(";" + deleteArg[i], "");
                        attachmentNameNewArg = strAttachmentNameNew.Split(new char[] { ';' });

                        int lenNewReplace = attachmentNameNewArg.Length;
                        if (lenNew == lenNewReplace) {
                            strAttachmentNameNew = strAttachmentNameNew.Replace(deleteArg[i], "");
                        }
                        if (this.OldFUNameCtl.Value.Length == 0) {
                            this.OldFUNameCtl.Value = deleteArg[i];
                        } else {
                            this.OldFUNameCtl.Value = this.OldFUNameCtl.Value + ";" + deleteArg[i];
                        }
                    }
                }

                if (strAttachmentNameOrg.Length != 0 && strAttachmentNameOrg.Substring(0, 1) == ";") {
                    this.FUNameCtl.Value = strAttachmentNameOrg.Substring(1, strAttachmentNameOrg.Length - 1);
                } else {
                    this.FUNameCtl.Value = strAttachmentNameOrg;
                }

                if (strAttachmentNameNew.Length != 0 && strAttachmentNameNew.Substring(0, 1) == ";") {
                    this.NewFUNameCtl.Value = strAttachmentNameNew.Substring(1, strAttachmentNameNew.Length - 1);
                } else {
                    this.NewFUNameCtl.Value = strAttachmentNameNew;
                }
                this.DeleteFUNameCtl.Value = string.Empty;
                if (this.FUNameCtl.Value.Length == 0 && this.NewFUNameCtl.Value.Length == 0) {
                    return null;
                } else {
                    if (this.FUNameCtl.Value.Length == 0) {
                        return this.NewFUNameCtl.Value;
                    } else {
                        if (this.NewFUNameCtl.Value.Length == 0)
                            return this.FUNameCtl.Value;
                        else
                            if (this.FUNameCtl.Value.IndexOf(this.NewFUNameCtl.Value) != -1) {
                                return this.FUNameCtl.Value + ";";
                            } else {
                                return this.FUNameCtl.Value + ";" + this.NewFUNameCtl.Value;
                            }
                    }
                }
            }
        }
        set {
            if (value == null) {
                this.FUNameCtl.Value = "";
            } else {
                this.FUNameCtl.Value = value.ToString();
            }
        }
    }

    public string RealAttachmentFileName {
        get {
            if (this.RealFUNameCtl.Value.Length == 0 && this.NewRealFUNameCtl.Value.Length == 0) {
                return null;
            } else {
                string strAttachmentNameOrg = this.RealFUNameCtl.Value;
                string strAttachmentNameNew = this.NewRealFUNameCtl.Value;
                if (this.DeleteRealFUNameCtl.Value.Length != 0) {
                    string strDeleteAttachmentName = this.DeleteRealFUNameCtl.Value;
                    string[] deleteArg = strDeleteAttachmentName.Split(new char[] { ';' });
                    string[] attachmentNameOrgArg = strAttachmentNameOrg.Split(new char[] { ';' });
                    string[] attachmentNameNewArg = strAttachmentNameNew.Split(new char[] { ';' });
                    int lenOrg = attachmentNameOrgArg.Length;
                    int lenNew = attachmentNameNewArg.Length;
                    int len = deleteArg.Length;
                    for (int i = 0; i < len; i++) {
                        strAttachmentNameOrg = strAttachmentNameOrg.Replace(";" + deleteArg[i], "");
                        attachmentNameOrgArg = strAttachmentNameOrg.Split(new char[] { ';' });
                        int lenReplace = attachmentNameOrgArg.Length;
                        if (lenOrg == lenReplace) {
                            strAttachmentNameOrg = strAttachmentNameOrg.Replace(deleteArg[i], "");
                        }
                        strAttachmentNameNew = strAttachmentNameNew.Replace(";" + deleteArg[i], "");
                        attachmentNameNewArg = strAttachmentNameNew.Split(new char[] { ';' });

                        int lenNewReplace = attachmentNameNewArg.Length;
                        if (lenNew == lenNewReplace) {
                            strAttachmentNameNew = strAttachmentNameNew.Replace(deleteArg[i], "");
                        }
                        if (this.OldRealFUNameCtl.Value.Length == 0) {
                            this.OldRealFUNameCtl.Value = deleteArg[i];
                        } else {
                            this.OldRealFUNameCtl.Value = this.OldRealFUNameCtl.Value + ";" + deleteArg[i];
                        }
                    }
                }

                if (strAttachmentNameOrg.Length != 0 && strAttachmentNameOrg.Substring(0, 1) == ";") {
                    this.RealFUNameCtl.Value = strAttachmentNameOrg.Substring(1, strAttachmentNameOrg.Length - 1);
                } else {
                    this.RealFUNameCtl.Value = strAttachmentNameOrg;
                }

                if (strAttachmentNameNew.Length != 0 && strAttachmentNameNew.Substring(0, 1) == ";") {
                    this.NewRealFUNameCtl.Value = strAttachmentNameNew.Substring(1, strAttachmentNameNew.Length - 1);
                } else {
                    this.NewRealFUNameCtl.Value = strAttachmentNameNew;
                }
                this.DeleteRealFUNameCtl.Value = string.Empty;
                if (this.RealFUNameCtl.Value.Length == 0 && this.NewRealFUNameCtl.Value.Length == 0) {
                    return null;
                } else {
                    if (this.RealFUNameCtl.Value.Length == 0) {
                        return this.NewRealFUNameCtl.Value;
                    } else {
                        if (this.NewRealFUNameCtl.Value.Length == 0)
                            return this.RealFUNameCtl.Value;
                        else
                            if (this.RealFUNameCtl.Value.IndexOf(this.NewRealFUNameCtl.Value) != -1) {
                                return this.RealFUNameCtl.Value + ";";
                            } else {
                                return this.RealFUNameCtl.Value + ";" + this.NewRealFUNameCtl.Value;
                            }
                    }
                }
            }
        }
        set {
            if (value == null) {
                this.RealFUNameCtl.Value = "";
            } else {
                this.RealFUNameCtl.Value = value.ToString();
            }
        }
    }

    #endregion

    #region event handle

    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);

        if (IsView) {
            this.fileUpload.Style["display"] = "none";
            this.btAddFile.Visible = false;
            this.gvMultiAttachmentFile.DataSource = CreateDataSource();
            this.gvMultiAttachmentFile.DataBind();
        } else {
            this.fileUpload.Style["display"] = "";
            this.btAddFile.Visible = true;
            this.gvMultiAttachmentFile.DataSource = CreateDataSource();
            this.gvMultiAttachmentFile.DataBind();
        }
    }

    protected void btAddFile_Click(object sender, EventArgs e) {
        try {
            int maxFileSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFileSize"]);
            HttpFileCollection files = HttpContext.Current.Request.Files;

            for (int i = 0; i < files.Count; i++) {
                HttpPostedFile postedFile = files[i];
                if (postedFile.ContentLength > maxFileSize) {
                    //PageUtility.ShowModelDlg(this.Page.Page, "文件太大不能上传！");
                    AlertInformation(this.Parent.Page, "文件太大不能上传！");
                    return;
                }

                string fileName, fileExtension;
                string saveName;

                fileName = Path.GetFileName(postedFile.FileName);
                if (fileName != null && fileName != "") {
                    string realFileName = fileName;
                    if (this.RealAttachmentFileName != null) {
                        if (this.RealAttachmentFileName.Contains(realFileName)) {
                            AlertInformation(this.Parent.Page, "不能上传同名文件！");
                            return;
                        }
                    }
                    fileExtension = Path.GetExtension(fileName);
                    if (postedFile == null) {
                        AlertInformation(this.Parent.Page, "文件不存在！");
                        return;
                    } else {
                        Random objRand = new Random();
                        DateTime date = DateTime.Now;

                        //生成随机文件名
                        saveName = date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + Convert.ToString(objRand.Next(99) * 97 + 100);
                        fileName = saveName + fileExtension;

                        if (this.NewFUNameCtl.Value.Length != 0) {
                            this.NewFUNameCtl.Value = this.NewFUNameCtl.Value + ";" + fileName;
                        } else {
                            this.NewFUNameCtl.Value = fileName;
                        }

                        if (this.NewUploadFUNameCtl.Value.Length != 0) {
                            this.NewUploadFUNameCtl.Value = this.NewUploadFUNameCtl.Value + ";" + fileName;
                        } else {
                            this.NewUploadFUNameCtl.Value = fileName;
                        }

                        if (this.NewRealFUNameCtl.Value.Length != 0) {
                            this.NewRealFUNameCtl.Value = this.NewRealFUNameCtl.Value + ";" + realFileName;
                        } else {
                            this.NewRealFUNameCtl.Value = realFileName;
                        }

                        if (this.NewUploadRealFUNameCtl.Value.Length != 0) {
                            this.NewUploadRealFUNameCtl.Value = this.NewUploadRealFUNameCtl.Value + ";" + realFileName;
                        } else {
                            this.NewUploadRealFUNameCtl.Value = realFileName;
                        }

                        string pathName = System.Configuration.ConfigurationManager.AppSettings["UploadDirectory"];
                        string path = Server.MapPath(@"~/" + pathName);
                        string fullName = path + @"\" + fileName;
                        //
                        //保存文件
                        //
                        postedFile.SaveAs(fullName);
                        CloseUploadProgress(this.Parent.Page);
                    }
                }
            }
        } catch {
            CloseUploadProgress(this.Parent.Page);
            AlertInformation(this.Parent.Page, "上传文件失败");
        }
    }
    #endregion

    #region Function

    /// <summary>
    /// 根据AttachmentFileName创建DataSet
    /// </summary>
    /// <returns></returns>
    protected DataSet CreateDataSource() {
        DataTable attachmentFileTB = new DataTable();

        DataColumn attachmentFileNameColumn = new DataColumn();
        attachmentFileNameColumn.DataType = System.Type.GetType("System.String");
        attachmentFileNameColumn.ColumnName = "AttachmentFileName";
        attachmentFileTB.Columns.Add(attachmentFileNameColumn);

        DataColumn downloadUrlColumn = new DataColumn();
        downloadUrlColumn.DataType = System.Type.GetType("System.String");
        downloadUrlColumn.ColumnName = "DownloadUrl";
        attachmentFileTB.Columns.Add(downloadUrlColumn);

        DataColumn realAttachmentFileNameColumn = new DataColumn();
        realAttachmentFileNameColumn.DataType = System.Type.GetType("System.String");
        realAttachmentFileNameColumn.ColumnName = "RealAttachmentFileName";
        attachmentFileTB.Columns.Add(realAttachmentFileNameColumn);

        if (this.AttachmentFileName != null && !this.AttachmentFileName.Equals(string.Empty) &&
            this.RealAttachmentFileName != null && !this.RealAttachmentFileName.Equals(string.Empty)) {
            string aburl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
            if (!aburl.EndsWith("/"))
                aburl += "/";

            if (this.FUNameCtl.Value.Length != 0 && this.RealFUNameCtl.Value.Length != 0) {
                string[] attachmentFileName = this.FUNameCtl.Value.Split(new char[] { ';' });
                string[] realAttachmentFileName = this.RealFUNameCtl.Value.Split(new char[] { ';' });
                int len = attachmentFileName.Length;
                for (int i = 0; i < len; i++) {
                    DataRow dataRow;
                    dataRow = attachmentFileTB.NewRow();
                    dataRow["AttachmentFileName"] = attachmentFileName[i];
                    dataRow["RealAttachmentFileName"] = realAttachmentFileName[i];
                    dataRow["DownloadUrl"] = aburl + @"Dialog/download.aspx?FileName=" + attachmentFileName[i];
                    attachmentFileTB.Rows.Add(dataRow);
                }
            }

            if (this.NewFUNameCtl.Value.Length != 0 && this.NewRealFUNameCtl.Value.Length != 0) {
                string[] attachmentFileName = this.NewFUNameCtl.Value.Split(new char[] { ';' });
                string[] realAttachmentFileName = this.NewRealFUNameCtl.Value.Split(new char[] { ';' });
                int len = attachmentFileName.Length;
                for (int i = 0; i < len; i++) {
                    if ((FUNameCtl.Value.IndexOf(attachmentFileName[i]) == -1) && (RealFUNameCtl.Value.IndexOf(realAttachmentFileName[i]) == -1)) {
                        DataRow dataRow;
                        dataRow = attachmentFileTB.NewRow();
                        dataRow["AttachmentFileName"] = attachmentFileName[i];
                        dataRow["RealAttachmentFileName"] = realAttachmentFileName[i];
                        dataRow["DownloadUrl"] = aburl + @"Dialog/download.aspx?FileName=" + attachmentFileName[i];
                        attachmentFileTB.Rows.Add(dataRow);
                    }
                }
            }
        }
        DataSet attachmentFileDs = new DataSet();
        attachmentFileDs.Tables.Add(attachmentFileTB);
        return attachmentFileDs;
    }

    protected void AlertInformation(System.Web.UI.Page page, string alertInformation) {
        string scriptContent = "<script>alert('" + alertInformation + "');</script>";
        page.RegisterStartupScript("errscript", scriptContent);
    }

    protected void CloseUploadProgress(System.Web.UI.Page page) {
        string scriptContent = "<script>document.getElementById('divBg11').style.display='none';document.getElementById('divProcessing11').style.display='none';</script>";
        page.RegisterStartupScript("errscript", scriptContent);
    }

    #endregion

    #region "Public Event"

    /// <summary>
    /// 提交成功调用
    /// </summary>
    /// <param name="fileName"></param>
    public void UploadSuccessDeleteFileOnServer() {
        try {
            string pathName = System.Configuration.ConfigurationManager.AppSettings["UploadDirectory"];
            string path = Server.MapPath(@"~/" + pathName);
            if (this.OldFUNameCtl.Value.Length != 0) {
                string strDelAttachmentFileName = this.OldFUNameCtl.Value;
                string[] delAttachmentFileNameArg = strDelAttachmentFileName.Split(new char[] { ';' });
                int len = delAttachmentFileNameArg.Length;
                for (int i = 0; i < len; i++) {
                    string serverFileFullName = path + @"\" + delAttachmentFileNameArg[i];
                    if (System.IO.File.Exists(serverFileFullName)) {
                        File.Delete(serverFileFullName);
                    }
                }
            }
        } catch (Exception ex) {
            //AlertInformation(this.Page, ex.ToString());
        }
    }


    /// <summary>
    /// 提交失败调用
    /// </summary>
    public void UploadFailedDeleteFileOnServer() {
        try {
            string pathName = System.Configuration.ConfigurationManager.AppSettings["UploadDirectory"];
            string path = Server.MapPath(@"~/" + pathName);
            if (this.NewUploadFUNameCtl.Value.Length != 0) {
                string strDelAttachmentFileName = this.NewUploadFUNameCtl.Value;
                string[] delAttachmentFileNameArg = strDelAttachmentFileName.Split(new char[] { ';' });
                int len = delAttachmentFileNameArg.Length;
                for (int i = 0; i < len; i++) {
                    string serverFileFullName = path + @"\" + delAttachmentFileNameArg[i];
                    if (System.IO.File.Exists(serverFileFullName)) {
                        File.Delete(serverFileFullName);
                    }
                }
            }
        } catch (Exception ex) {
            //AlertInformation(this.Page, ex.ToString());
        }
    }
    #endregion

    #region GridView Event

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMultiAttachmentFile_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                if (this.IsView) {
                    ImageButton lbDelete = (ImageButton)e.Row.Cells[1].FindControl("imgBtnDelete");
                    lbDelete.Visible = false;
                }
            }
        }
    }

    protected void gvMultiAttachmentFile_RowDeleting(object sender, GridViewDeleteEventArgs e) {
        string attachmentFileName = (string)this.gvMultiAttachmentFile.DataKeys[e.RowIndex].Value;
        if (this.DeleteFUNameCtl.Value.Length != 0) {
            this.DeleteFUNameCtl.Value = this.DeleteFUNameCtl.Value + ";" + attachmentFileName;
        } else {
            this.DeleteFUNameCtl.Value = attachmentFileName;
        }

        Label lblRealFileName = (Label)this.gvMultiAttachmentFile.Rows[e.RowIndex].FindControl("lblRealFileName");
        string realFileName = lblRealFileName.Text;
        if (this.DeleteRealFUNameCtl.Value.Length != 0) {
            this.DeleteRealFUNameCtl.Value = this.DeleteRealFUNameCtl.Value + ";" + realFileName;
        } else {
            this.DeleteRealFUNameCtl.Value = realFileName;
        }
    }

    #endregion
}
