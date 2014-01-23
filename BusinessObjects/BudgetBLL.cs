using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.BudgetDSTableAdapters;
using System.Data.SqlClient;

namespace BusinessObjects {
    public class BudgetBLL {

        #region TableAdapter

        private BudgetManageFeeTableAdapter m_BudgetManageAdapter;
        public BudgetManageFeeTableAdapter TABudgetManage {
            get {
                if (this.m_BudgetManageAdapter == null) {
                    this.m_BudgetManageAdapter = new BudgetManageFeeTableAdapter();
                }
                return this.m_BudgetManageAdapter;
            }
        }

        private BudgetManageFeeHistoryTableAdapter m_BudgetManageHistoryAdapter;
        public BudgetManageFeeHistoryTableAdapter TABudgetManageHistory {
            get {
                if (this.m_BudgetManageHistoryAdapter == null) {
                    this.m_BudgetManageHistoryAdapter = new BudgetManageFeeHistoryTableAdapter();
                }
                return this.m_BudgetManageHistoryAdapter;
            }
        }

        private BudgetSalesFeeTableAdapter m_BudgetSalesAdapter;
        public BudgetSalesFeeTableAdapter TABudgetSales {
            get {
                if (this.m_BudgetSalesAdapter == null) {
                    this.m_BudgetSalesAdapter = new BudgetSalesFeeTableAdapter();
                }
                return this.m_BudgetSalesAdapter;
            }
        }

        private BudgetSalesFeeHistoryTableAdapter m_BudgetSalesHistoryAdapter;
        public BudgetSalesFeeHistoryTableAdapter TABudgetSalesHistory {
            get {
                if (this.m_BudgetSalesHistoryAdapter == null) {
                    this.m_BudgetSalesHistoryAdapter = new BudgetSalesFeeHistoryTableAdapter();
                }
                return this.m_BudgetSalesHistoryAdapter;
            }
        }

        #endregion

        #region BudgetManageFee

        public BudgetDS.BudgetManageFeeRow GetBudgetManageFeeByID(int BudgetManageFeeID) {
            return this.TABudgetManage.GetDataByID(BudgetManageFeeID)[0];
        }

        public void DeleteBudgetManageFeeByID(int BudgetManageFeeID) {
            SqlTransaction transaction = null;
            BudgetDS.BudgetManageFeeRow provRow = TABudgetManage.GetDataByID(BudgetManageFeeID)[0];
            try {
                BudgetDS.BudgetManageFeeRow row = this.TABudgetManage.GetDataByID(BudgetManageFeeID)[0];
                decimal[] calculateAssistant = new decimal[4];
                calculateAssistant = this.GetPersonalBudgetByOUID(row.OrganizationUnitID, row.Period);
                if (row.TotalBudget > calculateAssistant[3]) {
                    throw new ApplicationException("�����޸ĵ���ԭ�м�¼��Ԥ�㣬����ɾ��");
                }
                transaction = TableAdapterHelper.BeginTransaction(TABudgetManage);
                TableAdapterHelper.SetTransaction(TABudgetManageHistory, transaction);
                TABudgetManageHistory.DeleteByBudgetManageFeeId(BudgetManageFeeID);
                TABudgetManage.Delete(BudgetManageFeeID);
                transaction.Commit();
            } catch (SqlException ex) {
                if (transaction != null) transaction.Rollback();
                if (ex.Class == 16) {
                    throw new ApplicationException("����ɾ�����ù���Ԥ���ѱ�����");
                } else {
                    throw ex;
                }
            } finally {
                if (transaction != null) transaction.Dispose();
            }
            this.TABudgetSales.Delete(BudgetManageFeeID);
        }

        public void InsertBudgetManageFee(int OrganizationUnitID, DateTime Period, int ExpenseManageTypeID, decimal OriginalBudget, decimal NormalBudget, decimal? AdjustBudget, int UserID, int PositionID, string ModifyReason) {
            SqlTransaction transaction = null;
            try {
                ////����ʼ
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetManage);
                TableAdapterHelper.SetTransaction(this.TABudgetManageHistory, transaction);

                // ����
                BudgetDS.BudgetManageFeeDataTable table = new BudgetDS.BudgetManageFeeDataTable();
                BudgetDS.BudgetManageFeeRow row = table.NewBudgetManageFeeRow();

                row.OrganizationUnitID = OrganizationUnitID;
                row.Period = Period;
                row.ExpenseManageTypeID = ExpenseManageTypeID;
                row.OriginalBudget = OriginalBudget;
                row.NormalBudget = NormalBudget;
                row.AdjustBudget = AdjustBudget.GetValueOrDefault();
                if (ModifyReason != null) {
                    row.ModifyReason = ModifyReason;
                }
                table.AddBudgetManageFeeRow(row);
                this.TABudgetManage.Update(table);

                // �ӱ�
                BudgetDS.BudgetManageFeeHistoryDataTable tableDetail = new BudgetDS.BudgetManageFeeHistoryDataTable();
                BudgetDS.BudgetManageFeeHistoryRow rowDetail = tableDetail.NewBudgetManageFeeHistoryRow();

                rowDetail.OrganizationUnitID = OrganizationUnitID;
                rowDetail.Period = Period;
                rowDetail.ExpenseManageTypeID = ExpenseManageTypeID;
                rowDetail.OriginalBudget = OriginalBudget;
                rowDetail.NormalBudget = NormalBudget;
                rowDetail.AdjustBudget = AdjustBudget.GetValueOrDefault();
                rowDetail.Action = "Create";
                rowDetail.ModifyDate = DateTime.Now;
                rowDetail.PositionID = PositionID;
                rowDetail.UserID = UserID;
                if (ModifyReason != null) {
                    rowDetail.ModifyReason = ModifyReason;
                }
                rowDetail.BudgetManageFeeID = row.BudgetManageFeeID;
                tableDetail.AddBudgetManageFeeHistoryRow(rowDetail);
                this.TABudgetManageHistory.Update(tableDetail);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateBudgetManageFee(int BudgetManageFeeID, decimal NormalBudget, decimal? AdjustBudget, int UserID, int PositionID, string ModifyReason) {
            SqlTransaction transaction = null;
            try {
                ////����ʼ
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetManage);
                TableAdapterHelper.SetTransaction(this.TABudgetManageHistory, transaction);

                // ����                
                BudgetDS.BudgetManageFeeRow row = this.TABudgetManage.GetDataByID(BudgetManageFeeID)[0];
                if (row.TotalBudget > (NormalBudget + AdjustBudget.GetValueOrDefault())) {//����Ǽ���Ԥ��,Ҫ�����
                    decimal[] calculateAssistant = new decimal[4];
                    calculateAssistant = this.GetPersonalBudgetByOUID(row.OrganizationUnitID, row.Period);
                    if (row.TotalBudget - NormalBudget - AdjustBudget.GetValueOrDefault() > calculateAssistant[3]) {
                        throw new ApplicationException("�����޸ĵ���ԭ�м�¼��Ԥ�㣬�����޸�");
                    }
                }
                row.NormalBudget = NormalBudget;
                row.AdjustBudget = AdjustBudget.GetValueOrDefault();
                row.ModifyReason = ModifyReason;
                this.TABudgetManage.Update(row);

                // �ӱ�
                BudgetDS.BudgetManageFeeHistoryDataTable tableDetail = new BudgetDS.BudgetManageFeeHistoryDataTable();
                BudgetDS.BudgetManageFeeHistoryRow rowDetail = tableDetail.NewBudgetManageFeeHistoryRow();

                rowDetail.OrganizationUnitID = row.OrganizationUnitID;
                rowDetail.Period = row.Period;
                rowDetail.ExpenseManageTypeID = row.ExpenseManageTypeID;
                rowDetail.OriginalBudget = row.OriginalBudget;
                rowDetail.NormalBudget = row.NormalBudget;
                rowDetail.AdjustBudget = row.AdjustBudget;
                rowDetail.Action = "Modify";
                rowDetail.ModifyDate = DateTime.Now;
                rowDetail.PositionID = PositionID;
                rowDetail.UserID = UserID;
                rowDetail.ModifyReason = ModifyReason;
                rowDetail.BudgetManageFeeID = row.BudgetManageFeeID;
                tableDetail.AddBudgetManageFeeHistoryRow(rowDetail);
                this.TABudgetManageHistory.Update(tableDetail);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public BudgetDS.BudgetManageFeeDataTable GetPagedBudgetManageFee(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "BudgetManageFeeID is not null";
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "Period Desc";
            }
            return this.TABudgetManage.GetPagedData("BudgetManageFee", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryBudgetManageFeeTotalCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "BudgetManageFeeID is not null";
            }
            return (int)this.TABudgetManage.QueryDataCount("BudgetManageFee", queryExpression);
        }

        public BudgetDS.BudgetManageFeeHistoryDataTable GetBudgetManageFeeHistoryByParentID(int BudgetManageFeeID) {
            return this.TABudgetManageHistory.GetDataByBudgetManageFeeID(BudgetManageFeeID);
        }

        public decimal[] GetPersonalBudgetByParameter(int PositionID, DateTime Period) {

            decimal? TotalBudget = 0;
            decimal? ApprovedFee = 0;
            decimal? ApprovingFee = 0;
            decimal? RemainFee = 0;
            decimal[] calculateAssistant = new decimal[4];

            this.TABudgetManage.GetPersonalBudgetByParameter(PositionID, Period, ref TotalBudget, ref ApprovedFee, ref ApprovingFee, ref RemainFee);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedFee.GetValueOrDefault();
            calculateAssistant[2] = ApprovingFee.GetValueOrDefault();
            calculateAssistant[3] = RemainFee.GetValueOrDefault();

            return calculateAssistant;
        }

        public decimal[] GetPersonalBudgetByOUID(int OUID, DateTime Period) {

            decimal? TotalBudget = 0;
            decimal? ApprovedFee = 0;
            decimal? ApprovingFee = 0;
            decimal? RemainFee = 0;
            decimal[] calculateAssistant = new decimal[4];

            this.TABudgetManage.GetPersonalBudgetByOUID(OUID, Period, ref TotalBudget, ref ApprovedFee, ref ApprovingFee, ref RemainFee);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedFee.GetValueOrDefault();
            calculateAssistant[2] = ApprovingFee.GetValueOrDefault();
            calculateAssistant[3] = RemainFee.GetValueOrDefault();

            return calculateAssistant;
        }

        #endregion

        #region BudgetSalesFee

        public BudgetDS.BudgetSalesFeeRow GetBudgetSalesFeeByID(int BudgetSalesFeeID) {
            return this.TABudgetSales.GetDataById(BudgetSalesFeeID)[0];
        }

        public void DeleteBudgetSalesFeeByID(int BudgetSalesFeeId) {
            SqlTransaction transaction = null;
            BudgetDS.BudgetSalesFeeRow row = TABudgetSales.GetDataById(BudgetSalesFeeId)[0];
            try {
                decimal[] calculateAssistant = new decimal[14];
                calculateAssistant = this.GetSalesBudgetByParameter(row.CustomerID, row.Period,new MasterDataBLL().GetExpenseItemByID(row.ExpenseItemID).ExpenseSubCategoryID);
                if (row.TotalBudget > calculateAssistant[12]) {
                    throw new ApplicationException("�����޸ĵ���ԭ�м�¼��Ԥ�㣬�����޸�");
                }
                transaction = TableAdapterHelper.BeginTransaction(TABudgetSales);
                TableAdapterHelper.SetTransaction(TABudgetSalesHistory, transaction);
                TABudgetSalesHistory.DeleteByBudgetSalesFeeId(BudgetSalesFeeId);
                TABudgetSales.Delete(BudgetSalesFeeId);
                transaction.Commit();
            } catch (SqlException ex) {
                if (transaction != null) transaction.Rollback();
                if (ex.Class == 16) {
                    throw new ApplicationException("����ɾ����������Ԥ���ѱ�����");
                } else {
                    throw ex;
                }
            } finally {
                if (transaction != null) transaction.Dispose();
            }
            this.TABudgetSales.Delete(BudgetSalesFeeId);
        }

        public void InsertBudgetSalesFee(int CustomerID, int ExpenseItemID, DateTime Period, decimal OriginalBudget, decimal NormalBudget, decimal AdjustBudget, decimal TransferBudget, int UserID, int PositionID, String ModifyReason) {
            SqlTransaction transaction = null;
            try {
                ////����ʼ
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetSales);
                TableAdapterHelper.SetTransaction(this.TABudgetSalesHistory, transaction);

                //�����ظ��� �ͻ�������������ڼ�������ظ�
                int iCount = this.TABudgetSales.SearchBudgetSalesByIns(CustomerID, ExpenseItemID, Period).GetValueOrDefault();
                if (iCount > 0) {
                    throw new ApplicationException("�ͻ�������������ڼ�������ظ�!");
                }

                // ����
                BudgetDS.BudgetSalesFeeDataTable table = new BudgetDS.BudgetSalesFeeDataTable();
                BudgetDS.BudgetSalesFeeRow row = table.NewBudgetSalesFeeRow();

                row.CustomerID = CustomerID;
                row.ExpenseItemID = ExpenseItemID;
                row.Period = Period;
                row.OriginalBudget = OriginalBudget;
                row.NormalBudget = NormalBudget;
                row.AdjustBudget = AdjustBudget;
                row.TransferBudget = TransferBudget;
                row.ModifyReason = ModifyReason;
                table.AddBudgetSalesFeeRow(row);
                this.TABudgetSales.Update(table);

                // �ӱ�
                BudgetDS.BudgetSalesFeeHistoryDataTable tableDetail = new BudgetDS.BudgetSalesFeeHistoryDataTable();
                BudgetDS.BudgetSalesFeeHistoryRow rowDetail = tableDetail.NewBudgetSalesFeeHistoryRow();

                rowDetail.BudgetSalesFeeID = row.BudgetSalesFeeID;
                rowDetail.UserID = UserID;
                rowDetail.PositionID = PositionID;
                rowDetail.ModifyDate = DateTime.Now;
                rowDetail.Action = "Create";
                rowDetail.CustomerID = CustomerID;
                rowDetail.ExpenseItemID = ExpenseItemID;
                rowDetail.Period = Period;
                rowDetail.OriginalBudget = OriginalBudget;
                rowDetail.NormalBudget = NormalBudget;
                rowDetail.AdjustBudget = AdjustBudget;
                rowDetail.TransferBudget = TransferBudget;
                rowDetail.ModifyReason = ModifyReason;
                tableDetail.AddBudgetSalesFeeHistoryRow(rowDetail);
                this.TABudgetSalesHistory.Update(tableDetail);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateBudgetSalesFee(int BudgetSalesFeeID, decimal NormalBudget, decimal AdjustBudget,decimal TransferBudget, int UserID, int PositionID, String ModifyReason) {
            SqlTransaction transaction = null;
            try {

                ////����ʼ
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetSales);
                TableAdapterHelper.SetTransaction(this.TABudgetSalesHistory, transaction);

                // ����                
                BudgetDS.BudgetSalesFeeRow row = this.TABudgetSales.GetDataById(BudgetSalesFeeID)[0];
                if (row.TotalBudget > (NormalBudget + AdjustBudget)) {//����Ǽ���Ԥ��,Ҫ�����
                    decimal[] calculateAssistant = new decimal[14];
                    calculateAssistant = this.GetSalesBudgetByParameter(row.CustomerID, row.Period, new MasterDataBLL().GetExpenseItemByID(row.ExpenseItemID).ExpenseSubCategoryID);
                    if (row.TotalBudget - NormalBudget - AdjustBudget - TransferBudget > calculateAssistant[12]) {
                        throw new ApplicationException("�����޸ĵ���ԭ�м�¼��Ԥ�㣬�����޸�");
                    }
                }
                row.NormalBudget = NormalBudget;
                row.AdjustBudget = AdjustBudget;
                row.TransferBudget = TransferBudget;
                row.ModifyReason = ModifyReason;
                this.TABudgetSales.Update(row);

                // �ӱ�
                BudgetDS.BudgetSalesFeeHistoryDataTable tableDetail = new BudgetDS.BudgetSalesFeeHistoryDataTable();
                BudgetDS.BudgetSalesFeeHistoryRow rowDetail = tableDetail.NewBudgetSalesFeeHistoryRow();

                rowDetail.BudgetSalesFeeID = row.BudgetSalesFeeID;
                rowDetail.UserID = UserID;
                rowDetail.PositionID = PositionID;
                rowDetail.ModifyDate = DateTime.Now;
                rowDetail.Action = "Modify";
                rowDetail.CustomerID = row.CustomerID;
                rowDetail.ExpenseItemID = row.ExpenseItemID;
                rowDetail.Period = row.Period;
                rowDetail.OriginalBudget = row.OriginalBudget;
                rowDetail.NormalBudget = NormalBudget;
                rowDetail.AdjustBudget = AdjustBudget;
                rowDetail.TransferBudget = TransferBudget;
                rowDetail.ModifyReason = ModifyReason;
                tableDetail.AddBudgetSalesFeeHistoryRow(rowDetail);
                this.TABudgetSalesHistory.Update(tableDetail);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public BudgetDS.BudgetSalesFeeDataTable GetPagedBudgetSalesFee(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "BudgetSalesFeeID is not null";
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "Period Desc";
            }
            return this.TABudgetSales.GetBudgetSalesFeePaged("BudgetSalesFee", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryBudgetSalesFeeTotalCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "BudgetSalesFeeID is not null";
            }
            return (int)this.TABudgetSales.QueryDataCount("BudgetSalesFee", queryExpression);
        }

        public BudgetDS.BudgetSalesFeeHistoryDataTable GetBudgetSalesFeeHistoryByParentID(int BudgetSalesFeeID) {
            return this.TABudgetSalesHistory.GetDataByBudgetSalesFeeId(BudgetSalesFeeID);
        }

        #endregion

        public decimal[] GetSalesBudgetByParameter(int CustomerID, DateTime Period, int ExpenseSubCategoryID) {
            decimal? CustomerBudget = 0;
            decimal? CustomerApprovedAmount = 0;
            decimal? CustomerApprovingAmount = 0;
            decimal? CustomerCompletedAmount = 0;
            decimal? CustomerReimbursedAmount = 0;
            decimal? CustomerBudgetRemain = 0;
            decimal? CustomerBudgetRate = 0;
            decimal? OUBudget = 0;
            decimal? OUApprovedAmount = 0;
            decimal? OUApprovingAmount = 0;
            decimal? OUCompletedAmount = 0;
            decimal? OUReimbursedAmount = 0;
            decimal? OUBudgetRemain = 0;
            decimal? OUBudgetRate = 0;
            decimal[] calculateAssistant = new decimal[14];
            this.TABudgetSales.GetSalesBudgetByParameter(CustomerID, Period, ExpenseSubCategoryID, ref CustomerBudget, ref CustomerApprovedAmount, ref CustomerApprovingAmount, ref CustomerCompletedAmount, ref CustomerReimbursedAmount,
                ref CustomerBudgetRemain, ref CustomerBudgetRate, ref OUBudget, ref OUApprovedAmount, ref OUApprovingAmount, ref OUCompletedAmount, ref OUReimbursedAmount, ref OUBudgetRemain, ref OUBudgetRate);
            calculateAssistant[0] = CustomerBudget.GetValueOrDefault();
            calculateAssistant[1] = CustomerApprovedAmount.GetValueOrDefault();
            calculateAssistant[2] = CustomerApprovingAmount.GetValueOrDefault();
            calculateAssistant[3] = CustomerCompletedAmount.GetValueOrDefault();
            calculateAssistant[4] = CustomerReimbursedAmount.GetValueOrDefault();
            calculateAssistant[5] = CustomerBudgetRemain.GetValueOrDefault();
            calculateAssistant[6] = CustomerBudgetRate.GetValueOrDefault();
            calculateAssistant[7] = OUBudget.GetValueOrDefault();
            calculateAssistant[8] = OUApprovedAmount.GetValueOrDefault();
            calculateAssistant[9] = OUApprovingAmount.GetValueOrDefault();
            calculateAssistant[10] = OUCompletedAmount.GetValueOrDefault();
            calculateAssistant[11] = OUReimbursedAmount.GetValueOrDefault();
            calculateAssistant[12] = OUBudgetRemain.GetValueOrDefault();
            calculateAssistant[13] = OUBudgetRate.GetValueOrDefault();
            return calculateAssistant;
        }
    }
}
