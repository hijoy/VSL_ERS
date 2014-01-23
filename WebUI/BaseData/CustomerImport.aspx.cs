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

public partial class CustomerImport : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "客户信息导入");
            this.Page.Title = "客户信息导入";
            int opImportId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Customer, SystemEnums.OperateEnum.Import);
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
                queryExpression.Append(" And ImportType = 3");
            } else {
                queryExpression.Append(" ImportType = 3");
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
            CustomerTableAdapter TACustomer = new CustomerTableAdapter();
            ImportLogTableAdapter TAImportLog = new ImportLogTableAdapter();
            ImportLogDetailTableAdapter TAImportLogDetail = new ImportLogDetailTableAdapter();
            MasterDataBLL mdBLL = new MasterDataBLL();

            transaction = TableAdapterHelper.BeginTransaction(TACustomer);
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
            logRow.ImportType = 3;
            logRow.TotalCount = dt.Rows.Count - 1;
            logRow.SuccessCount = dt.Rows.Count - 1;
            logRow.FailCount = 0;
            logTable.AddImportLogRow(logRow);
            TAImportLog.Update(logTable);

            //处理每条明细
            ERS.CustomerDataTable tbCustomer = new ERS.CustomerDataTable();
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
                    string CustomerNo = row[0].ToString().Trim();
                    string CustomerName = row[1].ToString().Trim();
                    string ProvinceName = row[2].ToString().Trim();
                    string CityName = row[3].ToString().Trim();
                    string CustomerTypeName = row[4].ToString().Trim();
                    string ChannelTypeName = row[5].ToString().Trim();
                    string OUName = row[6].ToString().Trim();
                    string BudgetOUName = row[7].ToString().Trim();

                    ERS.CustomerDataTable tbCustomerTemp = mdBLL.GetCustomerByCustomerName(CustomerName);

                    if (tbCustomerTemp != null && tbCustomerTemp.Count > 0) {
                        errorInfor = "第" + (i + 1) + "行有错：已存在客户《" + CustomerName + "》";
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

                    //ERS.ProvinceDataTable tbProvince = mdBLL.GetProvinceByProvinceName(ProvinceName);

                    //if (tbProvince == null || tbProvince.Count == 0) {
                    //    errorInfor = "第" + (i + 1) + "行有错：找不到省份《" + ProvinceName + "》";
                    //    ImportDS.ImportLogDetailRow ImportDetailRow = ImportLogDetailTable.NewImportLogDetailRow();
                    //    ImportDetailRow.LogID = logRow.LogID;
                    //    ImportDetailRow.Line = i + 1;
                    //    ImportDetailRow.Error = errorInfor;
                    //    ImportLogDetailTable.AddImportLogDetailRow(ImportDetailRow);
                    //    logRow.FailCount = logRow.FailCount + 1;
                    //    logRow.SuccessCount = logRow.SuccessCount - 1;
                    //    TAImportLog.Update(logRow);
                    //    continue;
                    //}

                    ERS.CityDataTable tbCity = mdBLL.GetCityByCityName(CityName);

                    if (tbCity == null || tbCity.Count == 0) {
                        errorInfor = "第" + (i + 1) + "行有错：找不到城市《" + CityName + "》";
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

                    ERS.CustomerTypeDataTable tbCustomerType = mdBLL.GetCustomerTypeByCustomerTypeName(CustomerTypeName);

                    if (tbCustomerType == null || tbCustomerType.Count == 0) {
                        errorInfor = "第" + (i + 1) + "行有错：找不到客户类型《" + CustomerTypeName + "》";
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

                    ERS.ChannelTypeDataTable tbChannelType = mdBLL.GetChannelTypeByChannelTypeName(ChannelTypeName);

                    if (tbChannelType == null || tbChannelType.Count == 0) {
                        errorInfor = "第" + (i + 1) + "行有错：找不到客户渠道《" + ChannelTypeName + "》";
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

                    AuthorizationDS.OrganizationUnitDataTable ouTable = new OUTreeBLL().GetDataByOrganizationUnitName(OUName);

                    if (ouTable == null || ouTable.Count == 0) {
                        errorInfor = "第" + (i + 1) + "行有错：系统中找不到此部门《" + OUName + "》";
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

                    if (ouTable.Count > 1) {
                        errorInfor = "第" + (i + 1) + "行有错：系统中找到多个此名称的部门《" + OUName + "》";
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

                    AuthorizationDS.OrganizationUnitDataTable BudgetOUTable = new OUTreeBLL().GetDataByOrganizationUnitName(BudgetOUName);

                    if (BudgetOUTable == null || BudgetOUTable.Count == 0) {
                        errorInfor = "第" + (i + 1) + "行有错：系统中找不到此部门《" + BudgetOUName + "》";
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

                    if (BudgetOUTable.Count > 1) {
                        errorInfor = "第" + (i + 1) + "行有错：系统中找到多个此名称的部门《" + BudgetOUName + "》";
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

                    ERS.CustomerRow rowCustomer = tbCustomer.NewCustomerRow();

                    rowCustomer.CustomerNo = CustomerNo;
                    rowCustomer.CustomerName = CustomerName;
                    rowCustomer.CityID = tbCity[0].CityID;
                    rowCustomer.CustomerTypeID = tbCustomerType[0].CustomerTypeID;
                    rowCustomer.ChannelTypeID = tbChannelType[0].ChannelTypeID;
                    rowCustomer.OrganizationUnitID = ouTable[0].OrganizationUnitId;
                    rowCustomer.ApplyOrganizationUnitID = BudgetOUTable[0].OrganizationUnitId;
                    rowCustomer.IsActive = true;

                    tbCustomer.AddCustomerRow(rowCustomer);
                    TACustomer.Update(rowCustomer);
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

        //客户编号	客户名称	省份	城市	客户类型	渠道类型	所属机构	所属预算机构

        string CustomerNo = row[0].ToString().Trim();
        string CustomerName = row[1].ToString().Trim();
        string ProvinceName = row[2].ToString().Trim();
        string CityName = row[3].ToString().Trim();
        string CustomerTypeName = row[4].ToString().Trim();
        string ChannelName = row[5].ToString().Trim();
        string OUName = row[6].ToString().Trim();
        string BudgetOUName = row[7].ToString().Trim();

        if (CustomerNo == string.Empty) {
            return "客户编号不能为空";
        }
        if (CustomerName == string.Empty) {
            return "客户名称不能为空";
        }
        //if (ProvinceName == string.Empty) {
        //    return "省份不能为空";
        //}
        if (CityName == string.Empty) {
            return "城市不能为空";
        }
        if (CustomerTypeName == string.Empty) {
            return "客户类型不能为空";
        }
        if (ChannelName == string.Empty) {
            return "客户渠道不能为空";
        }
        if (OUName == string.Empty) {
            return "所属部门不能为空";
        }
        if (BudgetOUName == string.Empty) {
            return "所属预算部门不能为空";
        }
        return null;
    }
}
