using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinessObjects.FormDSTableAdapters;
using lib.wf;

namespace BusinessObjects {
    public class BudgetAllocationApplyBLL {
        #region 属性
        private BudgetSalesFeeViewTableAdapter m_BudgetSalesFeeViewTA;
        public BudgetSalesFeeViewTableAdapter BudgetSalesFeeViewTA {
            get {
                if (m_BudgetSalesFeeViewTA == null) {
                    m_BudgetSalesFeeViewTA = new BudgetSalesFeeViewTableAdapter();
                }
                return m_BudgetSalesFeeViewTA;
            }
        }

        private FormDS m_FormDS;
        public FormDS FormDataSet {
            get {
                if (this.m_FormDS == null) {
                    this.m_FormDS = new FormDS();
                }
                return this.m_FormDS;
            }
            set {
                this.m_FormDS = value;
            }
        }

        private FormTableAdapter m_FormAdapter;
        public FormTableAdapter TAForm {
            get {
                if (this.m_FormAdapter == null) {
                    this.m_FormAdapter = new FormTableAdapter();
                }
                return this.m_FormAdapter;
            }
        }

        private FormBudgetAllocationTableAdapter m_FormBudgetAllocationAdapter;
        public FormBudgetAllocationTableAdapter TAFormBudgetAllocation {
            get {
                if (this.m_FormBudgetAllocationAdapter == null) {
                    this.m_FormBudgetAllocationAdapter = new FormBudgetAllocationTableAdapter();
                }
                return this.m_FormBudgetAllocationAdapter;
            }
        }

        private FormBudgetAllocationDetailTableAdapter m_FormBudgetAllocationDetailAdapter;
        public FormBudgetAllocationDetailTableAdapter TAFormBudgetAllocationDetail {
            get {
                if (this.m_FormBudgetAllocationDetailAdapter == null) {
                    this.m_FormBudgetAllocationDetailAdapter = new FormBudgetAllocationDetailTableAdapter();
                }
                return this.m_FormBudgetAllocationDetailAdapter;
            }
        }
        #endregion

        #region 获取数据

        public FormDS.FormDataTable GetFormByID(int FormID) {
            return this.TAForm.GetDataByID(FormID);
        }

        public FormDS.FormBudgetAllocationDataTable GetFormBudgetAllocationByID(int FormBudgetAllocationID) {
            return this.TAFormBudgetAllocation.GetDataByID(FormBudgetAllocationID);
        }

        public FormDS.FormBudgetAllocationDetailDataTable GetFormBudgetAllocationDetailByFormBudgetAllocationID(int FormBudgetAllocationlID) {
            return this.TAFormBudgetAllocationDetail.GetDataByFormID(FormBudgetAllocationlID);
        }

        public FormDS.FormBudgetAllocationDetailDataTable GetFormBudgetAllocationDetail(SystemEnums.AllocationType AllocationType) {
            FormDS.FormBudgetAllocationDetailDataTable table = new FormDS.FormBudgetAllocationDetailDataTable();
            DataRow[] rows = this.FormDataSet.FormBudgetAllocationDetail.Select("AllocationType=" + (int)AllocationType);
            foreach (FormDS.FormBudgetAllocationDetailRow item in rows) {
                table.ImportRow(item);
            }
            return table;
        }

        public FormDS.FormBudgetAllocationDetailDataTable GetFormBudgetAllocationDetail(int FormBudgetAllocationID, SystemEnums.AllocationType AllocationType) {
            FormDS.FormBudgetAllocationDetailDataTable table = new FormDS.FormBudgetAllocationDetailDataTable();
            DataRow[] rows = this.TAFormBudgetAllocationDetail.GetDataByFormID(FormBudgetAllocationID).Select("AllocationType=" + (int)AllocationType);
            foreach (FormDS.FormBudgetAllocationDetailRow item in rows) {
                table.ImportRow(item);
            }
            return table;
        }

        public FormDS.FormBudgetAllocationDetailDataTable GetFormBudgetAllocationDetailIn() {
            return GetFormBudgetAllocationDetail(SystemEnums.AllocationType.In);
        }

        public FormDS.FormBudgetAllocationDetailDataTable GetFormBudgetAllocationDetailIn(int FormBudgetAllocationID) {
            return GetFormBudgetAllocationDetail(FormBudgetAllocationID, SystemEnums.AllocationType.In);
        }
        public FormDS.FormBudgetAllocationDetailDataTable GetFormBudgetAllocationDetailOut() {
            return GetFormBudgetAllocationDetail(SystemEnums.AllocationType.Out);
        }
        public FormDS.FormBudgetAllocationDetailDataTable GetFormBudgetAllocationDetailOut(int FormBudgetAllocationID) {
            return GetFormBudgetAllocationDetail(FormBudgetAllocationID, SystemEnums.AllocationType.Out);
        }
        #endregion

        #region QueryBudgetSalesFeeView

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public FormDS.BudgetSalesFeeViewDataTable BudgetSalesFeePaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "CustomerID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "CustomerID is not null";
            }

            return this.BudgetSalesFeeViewTA.GetPagedBudgetSalsesFeeData("BudgetSalesFeeView", sortExpression, startRowIndex, maximumRows, queryExpression);

        }

        public int BudgetSalesFeeTotalCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "CustomerID is not null";
            }
            return (int)this.BudgetSalesFeeViewTA.QueryDataCount("BudgetSalesFeeView", queryExpression);
        }

        public FormDS.BudgetSalesFeeViewRow GetBudgetSalesFeeViewRowById(int Id) {
            return this.BudgetSalesFeeViewTA.GetDataByID(Id)[0];
        }

        #endregion

        #region BudgetAllocationDetail

        public void AddFormBudgetAllocationDetail(AuthorizationDS.StuffUserRow User, int? FormBudgetAllocationID, int BudgetSaleFeeViewId, decimal TransferBudget, SystemEnums.AllocationType AllocationType) {
            int count=this.FormDataSet.FormBudgetAllocationDetail.Count;

           FormDS.BudgetSalesFeeViewRow budgetSalesFeeRow = this.BudgetSalesFeeViewTA.GetDataByID(BudgetSaleFeeViewId)[0];
            FormDS.FormBudgetAllocationDetailRow rowDetail = this.FormDataSet.FormBudgetAllocationDetail.NewFormBudgetAllocationDetailRow();
            if (TransferBudget > budgetSalesFeeRow.TotalBudget) {
                throw new ApplicationException("调入/调出预算不能超出总预算！");
            }
            rowDetail.FormBudgetAllocationID = FormBudgetAllocationID.GetValueOrDefault();
            rowDetail.CustomerID = budgetSalesFeeRow.CustomerID;
            rowDetail.CustomerName = budgetSalesFeeRow.CustomerName;
            rowDetail.ExpenseItemID = budgetSalesFeeRow.ExpenseItemID;
            rowDetail.ExpenseItemName = budgetSalesFeeRow.ExpenseItemName;
            rowDetail.Period = budgetSalesFeeRow.Period;
            rowDetail.OriginalBudget = budgetSalesFeeRow.OriginalBudget;
            rowDetail.NormalBudget = budgetSalesFeeRow.NormalBudget;
            rowDetail.TotalBudget = budgetSalesFeeRow.TotalBudget;
            rowDetail.AdjustBudget = budgetSalesFeeRow.AdjustBudget;
            rowDetail.TransferBudget = TransferBudget;
            rowDetail.AllocationType = (int)AllocationType;
            rowDetail.CustomerName = budgetSalesFeeRow.CustomerName;
            rowDetail.ExpenseItemName = budgetSalesFeeRow.ExpenseItemName;
            rowDetail.BudgetSalesFeeId = budgetSalesFeeRow.BudgetSalesFeeID;
            // 填加行并进行更新处理

            this.FormDataSet.FormBudgetAllocationDetail.AddFormBudgetAllocationDetailRow(rowDetail);
        }

        public void DeleteFormBudgetAllocationDetailByID(int FormBudgetAllocationDetailID) {
            for (int index = 0; index < this.FormDataSet.FormBudgetAllocationDetail.Count; index++) {
               FormDS.FormBudgetAllocationDetailRow row=(FormDS.FormBudgetAllocationDetailRow)this.FormDataSet.FormBudgetAllocationDetail.Rows[index];
                if (row.RowState!=DataRowState.Deleted && row.FormBudgetAllocationDetailID == FormBudgetAllocationDetailID) {
                    row.Delete();
                    break;
                }
            }
        }

        #endregion

        #region FormBudgetAllocation

        public void AddFormBudgetAllocation(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID,
                SystemEnums.FormStatus StatusID, string Remark, string AttachFileName, string RealAttachFileName, string FlowTemplate) {

            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormBudgetAllocation, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormBudgetAllocationDetail, transaction);

                FormDS.FormRow formRow = this.FormDataSet.Form.NewFormRow();
                if (RejectedFormID != null) {
                    formRow.RejectedFormID = RejectedFormID.GetValueOrDefault();
                }
                formRow.UserID = UserID;
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString((int)FormTypeID);
                    formRow.FormNo = utility.GetFormNo(formTypeString);
                } else {
                    formRow.SetFormNoNull();
                }
                if (ProxyUserID != null) {
                    formRow.ProxyUserID = ProxyUserID.GetValueOrDefault();
                }
                if (ProxyPositionID != null) {
                    formRow.ProxyPositionID = ProxyPositionID.GetValueOrDefault();
                }
                formRow.OrganizationUnitID = OrganizationUnitID;
                formRow.PositionID = PositionID;
                formRow.FormTypeID = (int)FormTypeID;
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;
                formRow.InTurnUserIds = "P";//待改动
                formRow.InTurnPositionIds = "P";//待改动
                formRow.PageType = (int)SystemEnums.PageType.BudgetAllocationApply;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormBudgetAllocationRow formBudgetAllocationRow = this.FormDataSet.FormBudgetAllocation.NewFormBudgetAllocationRow();
                formBudgetAllocationRow.FormBudgetAllocationID = formRow.FormID;
                formBudgetAllocationRow.Amount = decimal.Zero;
                formBudgetAllocationRow.AttachFileName = AttachFileName;
                formBudgetAllocationRow.RealAttachFileName = RealAttachFileName;
                formBudgetAllocationRow.Remark = Remark;


                this.FormDataSet.FormBudgetAllocation.AddFormBudgetAllocationRow(formBudgetAllocationRow);
                this.TAFormBudgetAllocation.Update(formBudgetAllocationRow);

                //明细表
                decimal totalAmount = 0;//计算总申请金额
                decimal totalInAmount = 0;//计算调入总金额
                decimal totalOutAmount = 0;//计算调出总金额

                if (RejectedFormID != null) {
                    FormDS.FormBudgetAllocationDetailDataTable newDetailTable = new FormDS.FormBudgetAllocationDetailDataTable();
                    foreach (FormDS.FormBudgetAllocationDetailRow detailRow in this.FormDataSet.FormBudgetAllocationDetail) {
                        if (detailRow.RowState != System.Data.DataRowState.Deleted) {
                            FormDS.FormBudgetAllocationDetailRow newDetailRow = newDetailTable.NewFormBudgetAllocationDetailRow();
                            newDetailRow.FormBudgetAllocationID = formBudgetAllocationRow.FormBudgetAllocationID;
                            newDetailRow.CustomerID = detailRow.CustomerID;
                            newDetailRow.CustomerName = detailRow.CustomerName;
                            newDetailRow.ExpenseItemID = detailRow.ExpenseItemID;
                            newDetailRow.ExpenseItemName = detailRow.ExpenseItemName;
                            newDetailRow.Period = detailRow.Period;
                            newDetailRow.OriginalBudget = detailRow.OriginalBudget;
                            newDetailRow.NormalBudget = detailRow.NormalBudget;
                            newDetailRow.AdjustBudget = detailRow.AdjustBudget;
                            newDetailRow.TotalBudget = detailRow.TotalBudget;
                            newDetailRow.TransferBudget = detailRow.TransferBudget;
                            newDetailRow.AllocationType = detailRow.AllocationType;
                            newDetailRow.BudgetSalesFeeId = detailRow.BudgetSalesFeeId;
                            totalAmount += newDetailRow.TransferBudget;
                            if (detailRow.AllocationType == (int)SystemEnums.AllocationType.In) {
                                totalInAmount += detailRow.TransferBudget;
                            } else {
                                totalOutAmount += detailRow.TransferBudget;
                            }
                            newDetailTable.AddFormBudgetAllocationDetailRow(newDetailRow);
                        }
                    }
                    this.TAFormBudgetAllocationDetail.Update(newDetailTable);
                } else {
                    foreach (FormDS.FormBudgetAllocationDetailRow detailRow in this.FormDataSet.FormBudgetAllocationDetail) {
                        // 与父表绑定
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormBudgetAllocationID = formBudgetAllocationRow.FormBudgetAllocationID;
                            if (detailRow.AllocationType == (int)SystemEnums.AllocationType.In) {
                                totalInAmount += detailRow.TransferBudget;
                            } else {
                                totalOutAmount += detailRow.TransferBudget;
                            }
                            totalAmount += detailRow.TransferBudget;
                        }
                    }
                    this.TAFormBudgetAllocationDetail.Update(this.FormDataSet.FormBudgetAllocationDetail);
                }

                formBudgetAllocationRow.Amount = totalOutAmount;
                this.TAFormBudgetAllocation.Update(formBudgetAllocationRow);
                // 正式提交或草稿
             
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["Apply_Amount"] = totalOutAmount;//调拨金额

                    APHelper AP = new APHelper();
                    new APFlowBLL().ApplyForm(AP, TAForm, RejectedFormID, formRow, OrganizationUnitID, FlowTemplate, StatusID, dic);
                
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormBudgetAllocation(int FormID, SystemEnums.FormStatus StatusID, SystemEnums.FormType FormTypeID, string Remark, string AttachFileName, string RealAttachFileName, string FlowTemplate) {
            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormBudgetAllocation, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormBudgetAllocationDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString((int)FormTypeID);
                    formRow.FormNo = utility.GetFormNo(formTypeString);
                    formRow.InTurnUserIds = "P";//待改动
                    formRow.InTurnPositionIds = "P";//待改动
                } else {
                    formRow.SetFormNoNull();
                }
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormBudgetAllocationRow formBudgetAllocationRow = this.TAFormBudgetAllocation.GetDataByID(FormID)[0];
                formBudgetAllocationRow.Amount = decimal.Zero;
                formBudgetAllocationRow.AttachFileName = AttachFileName;
                formBudgetAllocationRow.RealAttachFileName = RealAttachFileName;
                formBudgetAllocationRow.Remark = Remark;
                this.TAFormBudgetAllocation.Update(formBudgetAllocationRow);

                //明细表
                decimal totalAmount = 0;//计算总申请金额
                decimal totalInAmount = 0;//计算调入总金额
                decimal totalOutAmount = 0;//计算调出总金额

                foreach (FormDS.FormBudgetAllocationDetailRow detailRow in this.FormDataSet.FormBudgetAllocationDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormBudgetAllocationID = formBudgetAllocationRow.FormBudgetAllocationID;
                        if (detailRow.AllocationType == (int)SystemEnums.AllocationType.In) {
                            totalInAmount += detailRow.TransferBudget;
                        } else {
                            totalOutAmount += detailRow.TransferBudget;
                        }
                        totalAmount += detailRow.TransferBudget;
                    }
                }

                this.TAFormBudgetAllocationDetail.Update(this.FormDataSet.FormBudgetAllocationDetail);

                formBudgetAllocationRow.Amount = totalOutAmount;
                this.TAFormBudgetAllocation.Update(formBudgetAllocationRow);

                // 正式提交或草稿
               
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["Apply_Amount"] = totalOutAmount;//调拨金额

                    APHelper AP = new APHelper();
                    new APFlowBLL().ApplyForm(AP, TAForm, null, formRow, formRow.OrganizationUnitID, FlowTemplate, StatusID, dic);
                

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void DeleteFormBudgetAllocation(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormBudgetAllocation, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormBudgetAllocationDetail, transaction);
                this.TAFormBudgetAllocationDetail.DeleteByFormID(FormID);
                this.TAFormBudgetAllocation.DeleteByID(FormID);
                this.TAForm.DeleteByID(FormID);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                if (transaction != null) {
                    transaction.Dispose();
                }
            }
        }
        #endregion
    }
}
