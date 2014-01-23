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
using BusinessObjects.ImportDSTableAdapters;
using BusinessObjects.ERSTableAdapters;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Text;

public partial class ShopImport : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "门店信息导入");
            this.Page.Title = "门店信息导入";
            int opImportId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ShopManage, SystemEnums.OperateEnum.Import);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            this.HasImportRight = positionRightBLL.CheckPositionRight(position.PositionId, opImportId);
            if (!this.HasImportRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }
    protected bool HasImportRight {
        get {
            return (bool)this.ViewState["HasImportRight"];
        }
        set {
            this.ViewState["HasImportRight"] = value;
        }
    }

    public string GetOUNameByOuID(object ouID) {
        int id = Convert.ToInt32(ouID);
        return new OUTreeBLL().GetOrganizationUnitById(id).OrganizationUnitName;
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            StringBuilder queryExpression = new StringBuilder();

            string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();

            if (start != null && start != string.Empty) {
                queryExpression.Append(" ImportDate>= '" + start + "'");
            }

            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            if (end != null && end != string.Empty) {
                if (queryExpression.Length > 0) {
                    queryExpression.Append(" and ");
                }
                queryExpression.Append(" dateadd(day,-1,ImportDate)<= '" + end + "'");
            }

            if (queryExpression.ToString() != string.Empty) {
                queryExpression.Append(" And ImportType = 4");
            } else {
                queryExpression.Append(" ImportType = 4");
            }
            this.LogObjectDataSource.SelectParameters["queryExpression"].DefaultValue = queryExpression.ToString();
            this.LogGridView.DataBind();
            this.LogUpdatePanel.Update();
        }
    }

    public string GetStuffNameByID(object stuffUserID) {
        int id = Convert.ToInt32(stuffUserID);
        StuffUserBLL bll = new StuffUserBLL();
        if (bll.GetStuffUserById(id).Count != 0) {
            return bll.GetStuffUserById(id)[0].StuffName;
        } else {
            return string.Empty;
        }
    }

    protected bool checkSearchConditionValid() {
        string startPeriod = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        string endPeriod = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();

        if (startPeriod == null || startPeriod == string.Empty) {
            if (endPeriod != null && endPeriod != string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择起始上传时间!");
                return false;
            }
        } else {
            if (endPeriod == null || endPeriod == string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择截止上传时间!");
                return false;
            } else {
                DateTime dtstartPeriod = DateTime.Parse(startPeriod);
                DateTime dtendPeriod = DateTime.Parse(endPeriod);
                if (dtstartPeriod > dtendPeriod) {
                    PageUtility.ShowModelDlg(this, "起始上传时间大于截止上传时间！");
                    return false;
                }
            }
        }
        return true;
    }

    protected void LogGridView_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.LogGridView.SelectedIndex >= 0) {
            this.LogDetailGridView.Visible = true;
            this.LogDetailObjectDataSource.SelectParameters["queryExpression"].DefaultValue = "LogID = " + LogGridView.SelectedDataKey.Value.ToString();
            this.LogDetailGridView.DataBind();
        } else {
            this.LogDetailGridView.Visible = false;
        }
        this.LogDetailUpdatePanel.Update();
    }

    public string GetExpenseTypeNameByID(object ExpenseTypeID) {
        int id = Convert.ToInt32(ExpenseTypeID);
        return new MasterDataBLL().GetExpenseManageTypeByID(id).ExpenseManageTypeName;
    }

    public string GetUserNameByID(object UserID) {
        int id = Convert.ToInt32(UserID);
        return new StuffUserBLL().GetStuffUserById(id)[0].StuffName;
    }

    public string GetPositionNameByID(object PositionID) {
        int id = Convert.ToInt32(PositionID);
        return new OUTreeBLL().GetPositionById(id).PositionName;
    }
    protected void btnImport_Click(object sender, EventArgs e) {
        if (this.fileUpLoad.Value.Equals("") | this.fileUpLoad.Value == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择文件!");
            return;
        }
        ReadDataFromClient();
    }

    private void ReadDataFromClient() {
        string filename = this.fileUpLoad.PostedFile.FileName.ToString();
        string excelFileExtension = string.Empty;
        if (filename.IndexOf(".") > 0) {
            excelFileExtension = filename.Substring(filename.LastIndexOf(".") + 1);
            if (excelFileExtension != "xls" && excelFileExtension != "xlsx") {
                PageUtility.ShowModelDlg(this, "请选择Excel文件");
                return;
            } else {
                string tmpFile = filename.Remove(0, filename.LastIndexOf("\\") + 1);
                tmpFile = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + tmpFile;
                string tmpfilename = tmpFile;
                string pathName = CommonUtility.GetPathName();
                string path = Server.MapPath(@"~/" + pathName);
                string fullName = path + @"\" + tmpFile;
                this.fileUpLoad.PostedFile.SaveAs(fullName);
                this.SaveDataToDB(fullName, tmpfilename, excelFileExtension);
            }
        }
    }

    public void SaveDataToDB(string FullPath, string FileName, string excelFileExtension) {
        SqlTransaction transaction = null;
        try {
            DataTable dt = null;
            dt = this.GetDataSet(FullPath, excelFileExtension).Tables[0];
            if (dt.Rows.Count <= 1) {
                PageUtility.ShowModelDlg(this.Page, "文件中没有任何记录，请重新选择");
                return;
            }
            ShopTableAdapter TAShop = new ShopTableAdapter();
            ImportLogTableAdapter TAImportLog = new ImportLogTableAdapter();
            ImportLogDetailTableAdapter TAImportLogDetail = new ImportLogDetailTableAdapter();
            MasterDataBLL mdBLL = new MasterDataBLL();

            transaction = TableAdapterHelper.BeginTransaction(TAShop);
            TableAdapterHelper.SetTransaction(TAImportLog, transaction);
            TableAdapterHelper.SetTransaction(TAImportLogDetail, transaction);
            //存储log信息
            ImportDS.ImportLogDataTable logTable = new ImportDS.ImportLogDataTable();
            ImportDS.ImportLogRow logRow = logTable.NewImportLogRow();

            int stuffUserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            string fullname = this.fileUpLoad.PostedFile.FileName.ToString();
            string tmpFile = fullname.Remove(0, fullname.LastIndexOf("\\") + 1);
            logRow.FileName = tmpFile;
            logRow.ImportDate = DateTime.Now;
            logRow.ImportUserID = stuffUserID;
            logRow.ImportType = 4;
            logRow.TotalCount = dt.Rows.Count - 1;
            logRow.SuccessCount = dt.Rows.Count - 1;
            logRow.FailCount = 0;
            logTable.AddImportLogRow(logRow);
            TAImportLog.Update(logTable);

            //处理每条明细
            ERS.ShopDataTable tbShop = new ERS.ShopDataTable();
            ImportDS.ImportLogDetailDataTable ImportLogDetailTable = new ImportDS.ImportLogDetailDataTable();
            int row_count = dt.Rows.Count;
            string errorInfor = string.Empty;
            //  int expenseTypeID = int.Parse(ExpenseTypeDDL.SelectedValue);
            //开始处理每条明细
            for (int i = 1; i <= row_count - 1; i++) {
                if (CheckData(dt.Rows[i]) != null) {
                    errorInfor = "第" + (i + 1) + "行有错：" + CheckData(dt.Rows[i]);
                    ImportDS.ImportLogDetailRow ImportDetailRow = ImportLogDetailTable.NewImportLogDetailRow();
                    ImportDetailRow.LogID = logRow.LogID;
                    ImportDetailRow.Line = i + 1;
                    ImportDetailRow.Error = errorInfor;
                    ImportLogDetailTable.AddImportLogDetailRow(ImportDetailRow);
                    logRow.FailCount = logRow.FailCount + 1;
                    logRow.SuccessCount = logRow.SuccessCount - 1;
                    TAImportLog.Update(logRow);
                    continue;
                } else {
                    DataRow row = dt.Rows[i];
                    string ShopName = row[0].ToString().Trim();
                    string ShopNo = row[1].ToString().Trim();
                    string CustomerName = row[2].ToString().Trim();
                    string ShopLevelName = row[3].ToString().Trim();
                    string Address = row[4].ToString().Trim();
                    string Contacter = row[5].ToString().Trim();
                    string Telephone = row[6].ToString().Trim();
                    string Email = row[7].ToString().Trim();

                    ERS.CustomerDataTable tbCustomer = mdBLL.GetCustomerByCustomerName(CustomerName);

                    if (tbCustomer == null || tbCustomer.Count == 0) {
                        errorInfor = "第" + (i + 1) + "行有错：找不到客户《" + CustomerName + "》";
                        ImportDS.ImportLogDetailRow ImportDetailRow = ImportLogDetailTable.NewImportLogDetailRow();
                        ImportDetailRow.LogID = logRow.LogID;
                        ImportDetailRow.Line = i + 1;
                        ImportDetailRow.Error = errorInfor;
                        ImportLogDetailTable.AddImportLogDetailRow(ImportDetailRow);
                        logRow.FailCount = logRow.FailCount + 1;
                        logRow.SuccessCount = logRow.SuccessCount - 1;
                        TAImportLog.Update(logRow);
                        continue;
                    }

                    ERS.ShopLevelDataTable tbShopLevel = mdBLL.GetShopLevelByShopLevelName(ShopLevelName);

                    if (tbShopLevel == null || tbShopLevel.Count == 0) {
                        errorInfor = "第" + (i + 1) + "行有错：找不到门店等级《" + ShopLevelName + "》";
                        ImportDS.ImportLogDetailRow ImportDetailRow = ImportLogDetailTable.NewImportLogDetailRow();
                        ImportDetailRow.LogID = logRow.LogID;
                        ImportDetailRow.Line = i + 1;
                        ImportDetailRow.Error = errorInfor;
                        ImportLogDetailTable.AddImportLogDetailRow(ImportDetailRow);
                        logRow.FailCount = logRow.FailCount + 1;
                        logRow.SuccessCount = logRow.SuccessCount - 1;
                        TAImportLog.Update(logRow);
                        continue;
                    }
                    int iCount =(int) new ShopTableAdapter().SearchNameByIns(ShopName, tbCustomer[0].CustomerID, tbShopLevel[0].ShopLevelID);
                    if (iCount > 0) {
                        errorInfor = string.Format("第" + (i + 1) + "行有错：同一客户，同一门店等级({0})下门店名称({1})不能重复！",tbShopLevel[0].ShopLevelName,ShopName);
                        ImportDS.ImportLogDetailRow ImportDetailRow = ImportLogDetailTable.NewImportLogDetailRow();
                        ImportDetailRow.LogID = logRow.LogID;
                        ImportDetailRow.Line = i + 1;
                        ImportDetailRow.Error = errorInfor;
                        ImportLogDetailTable.AddImportLogDetailRow(ImportDetailRow);
                        logRow.FailCount = logRow.FailCount + 1;
                        logRow.SuccessCount = logRow.SuccessCount - 1;
                        TAImportLog.Update(logRow);
                        continue;
                    }

                    ERS.ShopRow rowShop = tbShop.NewShopRow();

                    //string ShopName = row[0].ToString().Trim();
                    //string ShopNo = row[1].ToString().Trim();
                    //string CustomerNo = row[2].ToString().Trim();
                    //string CustomerName = row[3].ToString().Trim();
                    //string ShopLevelName = row[4].ToString().Trim();
                    //string Address = row[5].ToString().Trim();
                    //string Contacter = row[6].ToString().Trim();
                    //string Telephone = row[7].ToString().Trim();
                    //string Email = row[8].ToString().Trim();

                    rowShop.ShopName = ShopName;
                    rowShop.ShopNo = ShopNo;
                    rowShop.CustomerID = tbCustomer[0].CustomerID;
                    rowShop.ShopLevelID = tbShopLevel[0].ShopLevelID;
                    rowShop.Address = Address;
                    rowShop.Contacter = Contacter;
                    rowShop.Tel = Telephone;
                    rowShop.Email = Email;
                    rowShop.IsActive = true;

                    tbShop.AddShopRow(rowShop);
                    TAShop.Update(rowShop);
                }
            }
            TAImportLog.Update(logRow);
            TAImportLogDetail.Update(ImportLogDetailTable);

            transaction.Commit();
            string returnString = "成功导入" + logRow.SuccessCount.ToString() + "条信息";
            PageUtility.ShowModelDlg(this.Page, returnString);
        } catch (Exception ex) {
            if (transaction != null) {
                transaction.Rollback();
            } PageUtility.ShowModelDlg(this.Page, "Save Fail!" + ex.ToString());
        } finally {
            if (transaction != null) {
                transaction.Dispose();
            }
        }
    }

    public DataSet GetDataSet(string filepath, string excelFileExtension) {

        try {
            System.Data.OleDb.OleDbConnection oledbcon = null;
            string strConn = string.Empty;
            switch (excelFileExtension.Trim()) {
                case "xls":
                    oledbcon = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=1;MaxScanRows=0;\"");
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filepath + ";" + "Extended Properties=Excel 8.0;";
                    break;
                case "xlsx":
                    oledbcon = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath
                   + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1'");
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath
                   + ";Extended Properties=Excel 12.0;";
                    break;
            }

            //excel
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
            string sheetName = dtSheetName.Rows[0]["TABLE_NAME"].ToString();
            System.Data.OleDb.OleDbDataAdapter oledbAdaptor = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [" + sheetName + "]", oledbcon);
            //select 
            DataSet ds = new DataSet();
            oledbAdaptor.Fill(ds);
            oledbcon.Close();
            return ds;
        } catch (Exception ex) {
            throw ex;
        }
    }

    private string CheckData(DataRow row) {
        //先检查普通错误

        //门店名称	门店编号	客户编号	客户名称	门店等级	地址	联系人	 电话	邮箱

        string ShopName = row[0].ToString().Trim();
        string ShopNo = row[1].ToString().Trim();
        string CustomerName = row[2].ToString().Trim();
        string ShopLevel = row[3].ToString().Trim();
        string Address = row[4].ToString().Trim();
        string Contacter = row[5].ToString().Trim();
        string Telephone = row[6].ToString().Trim();
        string Email = row[7].ToString().Trim();

        if (ShopName == string.Empty) {
            return "门店名称不能为空";
        }
        if (ShopLevel == string.Empty) {
            return "门店等级不能为空";
        }
        if (Address == string.Empty) {
            return "门店地址不能为空";
        }
        return null;
    }
}
