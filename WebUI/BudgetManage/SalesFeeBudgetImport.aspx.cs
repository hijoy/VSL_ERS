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
using BusinessObjects.BudgetDSTableAdapters;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Text;

public partial class SaleBudgetImport : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "销售费用预算导入");
            this.Page.Title = "销售费用预算导入";
            int opImportId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.BudgetSalesFee, SystemEnums.OperateEnum.Import);
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
                queryExpression.Append(" And ImportType = 2");
            } else {
                queryExpression.Append(" ImportType = 2");
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
            BudgetSalesFeeTableAdapter TABudgetSalesFee = new BudgetSalesFeeTableAdapter();
            ImportLogTableAdapter TAImportLog = new ImportLogTableAdapter();
            ImportLogDetailTableAdapter TAImportLogDetail = new ImportLogDetailTableAdapter();

            transaction = TableAdapterHelper.BeginTransaction(TABudgetSalesFee);
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
            logRow.ImportType = 2;
            logRow.TotalCount = dt.Rows.Count - 1;
            logRow.SuccessCount = dt.Rows.Count - 1;
            logRow.FailCount = 0;
            logTable.AddImportLogRow(logRow);
            TAImportLog.Update(logTable);

            //处理每条明细
            BudgetDS.BudgetSalesFeeDataTable BudgetSalesTable = new BudgetDS.BudgetSalesFeeDataTable();
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
                    string CustomerName = row[0].ToString().Trim();
                    string CustomerNo = row[1].ToString().Trim();
                    string Period_Year = row[2].ToString().Trim();
                    string Period_Month = row[3].ToString().Trim();
                    string AccountingCode = row[4].ToString().Trim();
                    string ExpenseManageType = row[5].ToString().Trim();
                    string OriginalBudget = row[6].ToString().Trim();
                    string NormalBudget = row[7].ToString().Trim();
                    string AdjustBudget = row[8].ToString().Trim();
                    string TransferBudget = row[9].ToString().Trim();
                    string Remark = row[11].ToString().Trim();

                    ERS.CustomerDataTable customerTable = new MasterDataBLL().GetCustomerPaged(0, 20, string.Format("CustomerNo='{0}'", CustomerNo), null);

                    if (customerTable == null || customerTable.Count == 0) {
                        errorInfor = "第" + (i + 1) + "行有错：系统中找不到此客户《" + CustomerNo + "》";
                        ImportDS.ImportLogDetailRow ImportDetailRow = ImportLogDetailTable.NewImportLogDetailRow();
                        ImportDetailRow.LogID = logRow.LogID;
                        ImportDetailRow.Line = i + 1;
                        ImportDetailRow.Error = errorInfor;
                        ImportLogDetailTable.AddImportLogDetailRow(ImportDetailRow);
                        logRow.FailCount = logRow.FailCount + 1;
                        logRow.SuccessCount = logRow.SuccessCount - 1;
                        TAImportLog.Update(logRow);
                        continue;
                    } else if (customerTable.Count > 1) {
                        errorInfor = "第" + (i + 1) + "行有错：系统中找到多个此编号的客户《" + CustomerNo + "》";
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
                        ERS.ExpenseItemDataTable expenseItemTable = new MasterDataBLL().GetExpenseItemPaged(0, 20, null, string.Format("AccountingCode='{0}'", AccountingCode));
                        if (expenseItemTable == null || expenseItemTable.Count == 0) {
                            errorInfor = "第" + (i + 1) + "行有错：系统中找不到此费用类别《" + AccountingCode + "》";
                            ImportDS.ImportLogDetailRow ImportDetailRow = ImportLogDetailTable.NewImportLogDetailRow();
                            ImportDetailRow.LogID = logRow.LogID;
                            ImportDetailRow.Line = i + 1;
                            ImportDetailRow.Error = errorInfor;
                            ImportLogDetailTable.AddImportLogDetailRow(ImportDetailRow);
                            logRow.FailCount = logRow.FailCount + 1;
                            logRow.SuccessCount = logRow.SuccessCount - 1;
                            TAImportLog.Update(logRow);
                            continue;
                        } else if (expenseItemTable.Count > 1) {
                            errorInfor = "第" + (i + 1) + "行有错：系统中找到多个此费用类别《" + AccountingCode + "》";
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
                            string temp = Period_Year + "-" + Period_Month + "-01";
                            string filter = string.Format("CustomerID = {0} and ExpenseItemID= {1} and Period = '{2}'", customerTable[0].CustomerID, expenseItemTable[0].ExpenseItemID, DateTime.Parse(Period_Year + "-" + Period_Month + "-01"));
                            if (new BudgetBLL().GetPagedBudgetSalesFee(filter, 0, 20, null).Count > 0) {
                                errorInfor = "第" + (i + 1) + "行有错：系统中已经存在本年月的预算信息，请在系统中直接更新";
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
                                BudgetDS.BudgetSalesFeeRow BudgetSalesRow = BudgetSalesTable.NewBudgetSalesFeeRow();
                                BudgetSalesRow.CustomerID = customerTable[0].CustomerID;
                                BudgetSalesRow.Period = DateTime.Parse(Period_Year + "-" + Period_Month + "-01");
                                BudgetSalesRow.ExpenseItemID = expenseItemTable[0].ExpenseItemID;
                                BudgetSalesRow.OriginalBudget = decimal.Parse(OriginalBudget);
                                BudgetSalesRow.NormalBudget = decimal.Parse(NormalBudget);
                                BudgetSalesRow.AdjustBudget = decimal.Parse(AdjustBudget);
                                BudgetSalesRow.TransferBudget = decimal.Parse(TransferBudget);
                                BudgetSalesRow.TotalBudget = decimal.Round(BudgetSalesRow.NormalBudget + BudgetSalesRow.AdjustBudget, 2);
                                BudgetSalesRow.ModifyReason = Remark;
                                BudgetSalesTable.AddBudgetSalesFeeRow(BudgetSalesRow);
                            }
                        }
                    }
                }
            }
            TAImportLog.Update(logRow);
            TAImportLogDetail.Update(ImportLogDetailTable);
            TABudgetSalesFee.Update(BudgetSalesTable);
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
        string CustomerName = row[0].ToString().Trim();
        string CustomerNo = row[1].ToString().Trim();
        string Period_Year = row[2].ToString().Trim();
        string Period_Month = row[3].ToString().Trim();
        string AccountingCode = row[4].ToString().Trim();
        string ExpenseManageType = row[5].ToString().Trim();
        string OriginalBudget = row[6].ToString().Trim();
        string NormalBudget = row[7].ToString().Trim();
        string AdjustBudget = row[8].ToString().Trim();
        string TransferBudget = row[9].ToString().Trim();
        string Remark = row[11].ToString().Trim();

        if (CustomerNo == string.Empty) {
            return "客户编号不能为空";
        }
        if (Period_Year == string.Empty) {
            return "费用年份不能为空";
        } else {
            try {
                int tPeriodYear = int.Parse(Period_Year);
            } catch (Exception) {
                return "费用年份应为日期";
            }
        }
        if (Period_Month == string.Empty) {
            return "费用月份不能为空";
        } else {
            try {
                int tPeriodMonth = int.Parse(Period_Month);
            } catch (Exception) {
                return "费用月份应为日期";
            }
        }
        if (AccountingCode == string.Empty) {
            return "费用项编号不能为空";
        }
        if (OriginalBudget == string.Empty) {
            return "初始预算不能为空";
        } else {
            try {
                decimal tBudget = decimal.Parse(OriginalBudget);
            } catch (Exception e) {
                return "初始预算应为数字";
            }
        }
        if (NormalBudget == string.Empty) {
            return "正常预算不能为空";
        } else {
            try {
                decimal tBudget = decimal.Parse(NormalBudget);
            } catch (Exception e) {
                return "正常预算应为数字";
            }
        }
        if (AdjustBudget == string.Empty) {
            return "调整预算不能为空";
        } else {
            try {
                decimal tBudget = decimal.Parse(AdjustBudget);
            } catch (Exception e) {
                return "调整预算应为数字";
            }
        }
        if (AdjustBudget == string.Empty) {
            return "预算调拨不能为空";
        } else {
            try {
                decimal tTransferBudget = decimal.Parse(TransferBudget);
            } catch (Exception e) {
                return "预算调拨应为数字";
            }
        }
        return null;
    }
}
