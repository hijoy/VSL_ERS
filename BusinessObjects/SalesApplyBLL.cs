using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.FormDSTableAdapters;
using BusinessObjects.ERSTableAdapters;
using System.Web.Security;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using lib.wf;

namespace BusinessObjects {
    public class SalesApplyBLL {

        #region 属性

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

        private FormApplyTableAdapter m_FormApplyAdapter;
        public FormApplyTableAdapter TAFormApply {
            get {
                if (this.m_FormApplyAdapter == null) {
                    this.m_FormApplyAdapter = new FormApplyTableAdapter();
                }
                return this.m_FormApplyAdapter;
            }
        }

        private FormApplySKUDetailTableAdapter m_FormApplySKUDetailAdapter;
        public FormApplySKUDetailTableAdapter TAFormApplySKUDetail {
            get {
                if (this.m_FormApplySKUDetailAdapter == null) {
                    this.m_FormApplySKUDetailAdapter = new FormApplySKUDetailTableAdapter();
                }
                return this.m_FormApplySKUDetailAdapter;
            }
        }

        private FormApplyExpenseDetailTableAdapter m_FormApplyExpenseDetailAdapter;
        public FormApplyExpenseDetailTableAdapter TAFormApplyExpenseDetail {
            get {
                if (this.m_FormApplyExpenseDetailAdapter == null) {
                    this.m_FormApplyExpenseDetailAdapter = new FormApplyExpenseDetailTableAdapter();
                }
                return this.m_FormApplyExpenseDetailAdapter;
            }
        }

        private FormApplyDetailViewTableAdapter m_FormApplyDetailViewAdapter;
        public FormApplyDetailViewTableAdapter TAFormApplyDetailView {
            get {
                if (this.m_FormApplyDetailViewAdapter == null) {
                    this.m_FormApplyDetailViewAdapter = new FormApplyDetailViewTableAdapter();
                }
                return this.m_FormApplyDetailViewAdapter;
            }
        }

        private FormApplySplitRateTableAdapter _TAFormApplySplitRate;
        public FormApplySplitRateTableAdapter TAFormApplySplitRate {
            get {
                if (this._TAFormApplySplitRate == null) {
                    this._TAFormApplySplitRate = new FormApplySplitRateTableAdapter();
                }
                return this._TAFormApplySplitRate;
            }
        }
        #endregion

        #region 获取数据

        public FormDS.FormDataTable GetFormByID(int FormID) {
            return this.TAForm.GetDataByID(FormID);
        }

        public FormDS.FormApplyDataTable GetFormApplyByID(int FormApplyID) {
            return this.TAFormApply.GetDataByID(FormApplyID);
        }

        public FormDS.FormApplySKUDetailDataTable GetFormApplySKUDetailByFormApplyID(int FormApplyID) {
            return this.TAFormApplySKUDetail.GetDataByFormApplyID(FormApplyID);
        }

        public FormDS.FormApplyExpenseDetailDataTable GetFormApplyExpenseDetailByFormApplySKUDetailID(int FormApplySKUDetailID) {
            return this.TAFormApplyExpenseDetail.GetDataByFormApplySKUDetailID(FormApplySKUDetailID);
        }

        public FormDS.FormApplyExpenseDetailDataTable GetFormApplyExpenseDetailByFormApplyID(int FormApplyID) {
            return this.TAFormApplyExpenseDetail.GetDataByFormApplyID(FormApplyID);
        }

        public FormDS.FormApplySKUDetailDataTable GetFormApplySKUDetail() {
            return this.FormDataSet.FormApplySKUDetail;
        }

        public FormDS.FormApplyExpenseDetailDataTable GetFormApplyExpenseDetail(int FormApplySKUDetailID) {

            FormDS.FormApplyExpenseDetailDataTable newExpense = new FormDS.FormApplyExpenseDetailDataTable();
            newExpense = (FormDS.FormApplyExpenseDetailDataTable)this.FormDataSet.FormApplyExpenseDetail.Clone();
            DataRow[] dr = this.FormDataSet.FormApplyExpenseDetail.Select("FormApplySKUDetailID = " + FormApplySKUDetailID.ToString());
            for (int i = 0; i < dr.Length; i++) {
                newExpense.ImportRow((DataRow)dr[i]);
            }
            return newExpense;//返回的查询结果
        }

        public FormDS.FormApplyDetailViewDataTable GetFormApplyDetailView() {
            return this.FormDataSet.FormApplyDetailView;
        }

        public FormDS.FormApplyDetailViewDataTable GetFormApplyDetailViewByFormID(int FormID) {
            return this.TAFormApplyDetailView.GetDataByFormID(FormID);
        }

        //得到费用大类
        public int getExpenseCategoryIDByExpenseSubCategoryID(int ExpenseSubCategoryID) {
            return new ExpenseSubCategoryTableAdapter().GetDataById(ExpenseSubCategoryID)[0].ExpenseCategoryID;
        }
        #endregion

        #region FormApplySKUDetail

        public void AddFormApplySKUDetail(int? FormApplyID, int SKUID, decimal SupplyPrice, decimal PromotionPrice, int BuyQuantity, int GiveQuantity, int EstimatedSaleVolume) {

            FormDS.FormApplySKUDetailRow rowDetail = this.FormDataSet.FormApplySKUDetail.NewFormApplySKUDetailRow();
            rowDetail.FormApplyID = FormApplyID.GetValueOrDefault();
            rowDetail.SKUID = SKUID;
            rowDetail.SupplyPrice = SupplyPrice;
            rowDetail.PromotionPrice = PromotionPrice;
            rowDetail.BuyQuantity = BuyQuantity;
            rowDetail.GiveQuantity = GiveQuantity;
            rowDetail.EstimatedSaleVolume = EstimatedSaleVolume;
            //rowDetail.Remark = Remark;
            this.FormDataSet.FormApplySKUDetail.AddFormApplySKUDetailRow(rowDetail);
        }

        public void UpdateFormApplySKUDetail(int FormApplySKUDetailID, int SKUID, decimal SupplyPrice, decimal PromotionPrice, int BuyQuantity, int GiveQuantity, int EstimatedSaleVolume) {

            FormDS.FormApplySKUDetailRow rowDetail = this.FormDataSet.FormApplySKUDetail.FindByFormApplySKUDetailID(FormApplySKUDetailID);
            if (rowDetail == null)
                return;
            rowDetail.SKUID = SKUID;
            rowDetail.SupplyPrice = SupplyPrice;
            rowDetail.PromotionPrice = PromotionPrice;
            rowDetail.BuyQuantity = BuyQuantity;
            rowDetail.GiveQuantity = GiveQuantity;
            //更新所有子表的单箱费用
            if (rowDetail.EstimatedSaleVolume != EstimatedSaleVolume) {
                FormDS.FormApplyExpenseDetailRow[] dr = (FormDS.FormApplyExpenseDetailRow[])this.FormDataSet.FormApplyExpenseDetail.Select("FormApplySKUDetailID = " + FormApplySKUDetailID.ToString());
                for (int i = 0; i < dr.Length; i++) {
                    dr[i].PackageUnitPrice = dr[i].Amount / EstimatedSaleVolume;
                }
            }
            rowDetail.EstimatedSaleVolume = EstimatedSaleVolume;
            //rowDetail.Remark = Remark;
        }

        public void DeleteFormApplySKUDetailByID(int FormApplySKUDetailID) {
            for (int index = 0; index < this.FormDataSet.FormApplySKUDetail.Rows.Count; index++) {
                if (this.FormDataSet.FormApplySKUDetail.Rows[index].RowState != DataRowState.Deleted && (int)this.FormDataSet.FormApplySKUDetail.Rows[index]["FormApplySKUDetailID"] == FormApplySKUDetailID) {
                    this.FormDataSet.FormApplySKUDetail.Rows[index].Delete();
                    break;
                }
            }
            for (int index = 0; index < this.FormDataSet.FormApplyExpenseDetail.Rows.Count; index++) {
                if (this.FormDataSet.FormApplyExpenseDetail.Rows[index].RowState != DataRowState.Deleted && (int)this.FormDataSet.FormApplyExpenseDetail.Rows[index]["FormApplySKUDetailID"] == FormApplySKUDetailID) {
                    this.FormDataSet.FormApplyExpenseDetail.Rows[index].Delete();
                }
            }

        }

        #endregion

        #region FormApplyExpenseDetail

        public void AddFormApplyExpenseDetail(int? FormApplySKUDetailID, int ExpenseItemID, decimal Amount) {

            FormDS.FormApplyExpenseDetailRow rowDetail = this.FormDataSet.FormApplyExpenseDetail.NewFormApplyExpenseDetailRow();
            rowDetail.FormApplySKUDetailID = FormApplySKUDetailID.GetValueOrDefault();
            rowDetail.ExpenseItemID = ExpenseItemID;
            rowDetail.Amount = Amount;
            FormDS.FormApplySKUDetailRow skuRow = this.FormDataSet.FormApplySKUDetail.FindByFormApplySKUDetailID(FormApplySKUDetailID.GetValueOrDefault());
            rowDetail.PackageUnitPrice = Amount / skuRow.EstimatedSaleVolume;
            this.FormDataSet.FormApplyExpenseDetail.AddFormApplyExpenseDetailRow(rowDetail);
        }

        public void DeleteFormApplyExpenseDetailByID(int FormApplyExpenseDetailID) {
            for (int index = 0; index < this.FormDataSet.FormApplyExpenseDetail.Rows.Count; index++) {
                if (this.FormDataSet.FormApplyExpenseDetail.Rows[index].RowState != DataRowState.Deleted && (int)this.FormDataSet.FormApplyExpenseDetail.Rows[index]["FormApplyExpenseDetailID"] == FormApplyExpenseDetailID) {
                    this.FormDataSet.FormApplyExpenseDetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region FormApplyDetailView

        public void AddFormApplyDetailView(int? FormApplyID, int SKUID, int ExpenseItemID, decimal Amount, string Remark) {

            FormDS.FormApplyDetailViewRow rowDetail = this.FormDataSet.FormApplyDetailView.NewFormApplyDetailViewRow();
            rowDetail.FormApplyID = FormApplyID.GetValueOrDefault();
            rowDetail.SKUID = SKUID;
            rowDetail.ExpenseItemID = ExpenseItemID;
            rowDetail.Amount = Amount;
            rowDetail.Remark = Remark == null ? string.Empty : Remark;
            rowDetail.FormApplyExpenseDetailID = 1;
            rowDetail.SalesAmount = 0;
            rowDetail.Spec = string.Empty;
            rowDetail.PackageQuantity = 0;
            rowDetail.PackagePercent = 0;

            this.FormDataSet.FormApplyDetailView.AddFormApplyDetailViewRow(rowDetail);
        }

        public void DeleteFormApplyDetailByID(int FormApplySKUDetailID) {
            for (int index = 0; index < this.FormDataSet.FormApplyDetailView.Rows.Count; index++) {
                if (this.FormDataSet.FormApplyDetailView.Rows[index].RowState != DataRowState.Deleted && (int)this.FormDataSet.FormApplyDetailView.Rows[index]["FormApplySKUDetailID"] == FormApplySKUDetailID) {
                    this.FormDataSet.FormApplyDetailView.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region FormApply for promotion

        public void AddFormApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        DateTime BeginPeriod, DateTime EndPeriod, int CustomerID, int ShopID, int PaymentTypeID, int ExpenseSubCategoryID, string ContractNo, DateTime PromotionBeginDate, DateTime PromotionEndDate,
                        DateTime DeliveryBeginDate, DateTime DeliveryEndDate, int PromotionScopeID, int PromotionTypeID, string PromotionDesc, int ShelfTypeID, int? FirstVolume, int? SecondVolume, int? ThirdVolume,
                        decimal? CustomerBudget, decimal? CustomerBudgetRemain, decimal? OUBudget, decimal? OUAppovedAmount, decimal? OUApprovingAmount, decimal? OUCompletedAmount, decimal? OUReimbursedAmount, decimal? OUBudgetRemain, decimal? OUBudgetRate,
                        string AttachedFileName, string RealAttachedFileName, string Remark, int PromotionPriceType, int ReimburseRequirements, string FlowTemplate, string FormApplyName) {

            SqlTransaction transaction = null;

            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplySKUDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplyExpenseDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplySplitRate, transaction);

                //处理单据的内容
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
                formRow.PageType = (int)SystemEnums.PageType.PromotionApply;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormApplyRow formApplyRow = this.FormDataSet.FormApply.NewFormApplyRow();
                formApplyRow.FormApplyID = formRow.FormID;
                formApplyRow.Period = DateTime.Parse(BeginPeriod.Year.ToString() + "-" + BeginPeriod.Month.ToString() + "-01");
                formApplyRow.BeginPeriod = DateTime.Parse(BeginPeriod.Year.ToString() + "-" + BeginPeriod.Month.ToString() + "-01");
                formApplyRow.EndPeriod = DateTime.Parse(EndPeriod.Year.ToString() + "-" + EndPeriod.Month.ToString() + "-01");
                formApplyRow.ShopID = ShopID;
                formApplyRow.CustomerID = CustomerID;
                formApplyRow.PaymentTypeID = PaymentTypeID;
                formApplyRow.ExpenseSubCategoryID = ExpenseSubCategoryID;
                if (ContractNo != "") {
                    formApplyRow.ContractNo = ContractNo;
                }
                formApplyRow.PromotionBeginDate = PromotionBeginDate;
                formApplyRow.PromotionEndDate = PromotionEndDate;
                formApplyRow.DeliveryBeginDate = DeliveryBeginDate;
                formApplyRow.DeliveryEndDate = DeliveryEndDate;
                formApplyRow.PromotionScopeID = PromotionScopeID;
                formApplyRow.PromotionTypeID = PromotionTypeID;
                if (PromotionDesc != "") {
                    formApplyRow.PromotionDesc = PromotionDesc;
                }
                formApplyRow.ShelfTypeID = ShelfTypeID;
                if (FirstVolume != null) {
                    formApplyRow.FirstVolume = FirstVolume.GetValueOrDefault();
                }
                if (SecondVolume != null) {
                    formApplyRow.SecondVolume = SecondVolume.GetValueOrDefault();
                }
                if (ThirdVolume != null) {
                    formApplyRow.ThirdVolume = ThirdVolume.GetValueOrDefault();
                }
                if (CustomerBudget != null) {
                    formApplyRow.CustomerBudget = CustomerBudget.GetValueOrDefault();
                }
                if (CustomerBudgetRemain != null) {
                    formApplyRow.CustomerBudgetRemain = CustomerBudgetRemain.GetValueOrDefault();
                }
                if (OUBudget != null) {
                    formApplyRow.OUBudget = OUBudget.GetValueOrDefault();
                }
                if (OUAppovedAmount != null) {
                    formApplyRow.OUAppovedAmount = OUAppovedAmount.GetValueOrDefault();
                }
                if (OUApprovingAmount != null) {
                    formApplyRow.OUApprovingAmount = OUApprovingAmount.GetValueOrDefault();
                }
                if (OUCompletedAmount != null) {
                    formApplyRow.OUCompletedAmount = OUCompletedAmount.GetValueOrDefault();
                }
                if (OUReimbursedAmount != null) {
                    formApplyRow.OUReimbursedAmount = OUReimbursedAmount.GetValueOrDefault();
                }
                if (OUBudgetRemain != null) {
                    formApplyRow.OUBudgetRemain = OUBudgetRemain.GetValueOrDefault();
                }
                if (OUBudgetRate != null) {
                    formApplyRow.OUBudgetRate = OUBudgetRate.GetValueOrDefault();
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formApplyRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formApplyRow.RealAttachedFileName = RealAttachedFileName;
                }
                if (Remark != null && Remark != string.Empty) {
                    formApplyRow.Remark = Remark;
                }
                formApplyRow.FormApplyName = FormApplyName;
                formApplyRow.PromotionPriceType = PromotionPriceType;
                formApplyRow.ReimburseRequirements = ReimburseRequirements;

                formApplyRow.Amount = 0;//先赋默认值
                formApplyRow.IsClose = false;
                formApplyRow.IsComplete = false;
                formApplyRow.IsAutoSplit = false;
                this.FormDataSet.FormApply.AddFormApplyRow(formApplyRow);
                this.TAFormApply.Update(formApplyRow);

                //处理明细
                decimal totalAmount = 0;//计算总申请金额
                if (RejectedFormID != null) {
                    FormDS.FormApplyExpenseDetailDataTable newExpenseTable = new FormDS.FormApplyExpenseDetailDataTable();
                    foreach (FormDS.FormApplySKUDetailRow skuRow in this.FormDataSet.FormApplySKUDetail) {
                        if (skuRow.RowState != System.Data.DataRowState.Deleted) {
                            FormDS.FormApplySKUDetailDataTable newSKUTable = new FormDS.FormApplySKUDetailDataTable();
                            FormDS.FormApplySKUDetailRow newSKURow = newSKUTable.NewFormApplySKUDetailRow();
                            newSKURow.FormApplyID = formApplyRow.FormApplyID;
                            newSKURow.SKUID = skuRow.SKUID;
                            newSKURow.SupplyPrice = skuRow.SupplyPrice;
                            newSKURow.PromotionPrice = skuRow.PromotionPrice;
                            newSKURow.BuyQuantity = skuRow.BuyQuantity;
                            newSKURow.GiveQuantity = skuRow.GiveQuantity;
                            newSKURow.EstimatedSaleVolume = skuRow.EstimatedSaleVolume;
                            if (!skuRow.IsAmountNull()) {
                                newSKURow.Amount = skuRow.Amount;
                            }
                            if (!skuRow.IsRemarkNull()) {
                                newSKURow.Remark = skuRow.Remark;
                            }
                            newSKUTable.AddFormApplySKUDetailRow(newSKURow);
                            this.TAFormApplySKUDetail.Update(newSKUTable);
                            int newID = newSKURow.FormApplySKUDetailID;
                            FormDS.FormApplyExpenseDetailRow[] dr = (FormDS.FormApplyExpenseDetailRow[])this.FormDataSet.FormApplyExpenseDetail.Select("FormApplySKUDetailID = " + skuRow.FormApplySKUDetailID.ToString());
                            for (int i = 0; i < dr.Length; i++) {
                                FormDS.FormApplyExpenseDetailRow newExpenseRow = newExpenseTable.NewFormApplyExpenseDetailRow();
                                newExpenseRow.FormApplySKUDetailID = newID;
                                newExpenseRow.ExpenseItemID = dr[i].ExpenseItemID;
                                newExpenseRow.Amount = dr[i].Amount;
                                newExpenseRow.PackageUnitPrice = dr[i].PackageUnitPrice;
                                newExpenseRow.RepeatFormInfo = UtilityBLL.GenerateRepeatFormStr(this.TAForm.GetRepeatFormApplyNo(formApplyRow.PromotionBeginDate, formApplyRow.PromotionEndDate, newExpenseRow.ExpenseItemID, formApplyRow.ShopID, formApplyRow.FormApplyID));
                                newExpenseTable.AddFormApplyExpenseDetailRow(newExpenseRow);
                                if (dr[i].RowState != DataRowState.Deleted) {
                                    totalAmount += dr[i].Amount;
                                }
                            }
                        }
                    }
                    this.TAFormApplyExpenseDetail.Update(newExpenseTable);
                } else {
                    foreach (FormDS.FormApplySKUDetailRow skuRow in this.FormDataSet.FormApplySKUDetail) {
                        // 与父表绑定
                        if (skuRow.RowState != DataRowState.Deleted) {
                            skuRow.FormApplyID = formApplyRow.FormApplyID;
                            int oldID = skuRow.FormApplySKUDetailID;
                            this.TAFormApplySKUDetail.Update(skuRow);
                            int newID = skuRow.FormApplySKUDetailID;
                            FormDS.FormApplyExpenseDetailRow[] dr = (FormDS.FormApplyExpenseDetailRow[])this.FormDataSet.FormApplyExpenseDetail.Select("FormApplySKUDetailID = " + oldID.ToString());
                            for (int i = 0; i < dr.Length; i++) {
                                dr[i].FormApplySKUDetailID = newID;
                                dr[i].PackageUnitPrice = dr[i].Amount / skuRow.EstimatedSaleVolume;
                                if (dr[i].RowState != DataRowState.Deleted) {
                                    totalAmount += dr[i].Amount;
                                }
                                dr[i].RepeatFormInfo = UtilityBLL.GenerateRepeatFormStr(this.TAForm.GetRepeatFormApplyNo(formApplyRow.PromotionBeginDate, formApplyRow.PromotionEndDate, dr[i].ExpenseItemID, formApplyRow.ShopID, formApplyRow.FormApplyID));
                            }
                        }
                    }
                    this.TAFormApplyExpenseDetail.Update(this.FormDataSet.FormApplyExpenseDetail);
                }

                //分摊比例
                foreach (FormDS.FormApplySplitRateRow item in FormDataSet.FormApplySplitRate) {
                    item.FormApplyID = formApplyRow.FormApplyID;
                }
                this.TAFormApplySplitRate.Update(FormDataSet.FormApplySplitRate);

                formApplyRow.Amount = totalAmount;
                TAFormApply.Update(formApplyRow);

                // 正式提交或草稿

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["Apply_Amount"] = totalAmount;
                //dic["Dept"] = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID)[0].OrganizationUnitCode;
                dic["Expense_Category"] = getExpenseCategoryIDByExpenseSubCategoryID(ExpenseSubCategoryID).ToString();//此处待改动(目前市场部的ID为7)
                APHelper AP = new APHelper();
                new APFlowBLL().ApplyForm(AP, TAForm, RejectedFormID, formRow, OrganizationUnitID, FlowTemplate, StatusID, dic);

                transaction.Commit();
            } catch (ApplicationException ex) {
                transaction.Rollback();
                throw ex;
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }


        public void UpdateFormApply(int FormID, SystemEnums.FormStatus StatusID, SystemEnums.FormType FormTypeID, int ShopID, int PaymentTypeID, string ContractNo, DateTime PromotionBeginDate,
                        DateTime PromotionEndDate, DateTime DeliveryBeginDate, DateTime DeliveryEndDate, int PromotionScopeID, int PromotionTypeID, string PromotionDesc, int ShelfTypeID, int? FirstVolume, int? SecondVolume, int? ThirdVolume,
                        decimal? CustomerBudget, decimal? CustomerBudgetRemain, decimal? OUBudget, decimal? OUAppovedAmount, decimal? OUApprovingAmount, decimal? OUCompletedAmount, decimal? OUReimbursedAmount,
                        decimal? OUBudgetRemain, decimal? OUBudgetRate, string AttachedFileName, string RealAttachedFileName, string Remark, int ReimburseRequirements, string FlowTemplate, string FormApplyName) {

            SqlTransaction transaction = null;

            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplySKUDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplyExpenseDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplySplitRate, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormApplyRow formApplyRow = this.TAFormApply.GetDataByID(FormID)[0];

                //处理单据的内容
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
                //申请单内容
                formApplyRow.ShopID = ShopID;
                formApplyRow.PaymentTypeID = PaymentTypeID;
                if (ContractNo != "") {
                    formApplyRow.ContractNo = ContractNo;
                }
                formApplyRow.PromotionBeginDate = PromotionBeginDate;
                formApplyRow.PromotionEndDate = PromotionEndDate;
                formApplyRow.DeliveryBeginDate = DeliveryBeginDate;
                formApplyRow.DeliveryEndDate = DeliveryEndDate;
                formApplyRow.PromotionScopeID = PromotionScopeID;
                formApplyRow.PromotionTypeID = PromotionTypeID;
                if (PromotionDesc != "") {
                    formApplyRow.PromotionDesc = PromotionDesc;
                }
                formApplyRow.ShelfTypeID = ShelfTypeID;
                if (FirstVolume != null) {
                    formApplyRow.FirstVolume = FirstVolume.GetValueOrDefault();
                }
                if (SecondVolume != null) {
                    formApplyRow.SecondVolume = SecondVolume.GetValueOrDefault();
                }
                if (ThirdVolume != null) {
                    formApplyRow.ThirdVolume = ThirdVolume.GetValueOrDefault();
                }
                if (CustomerBudget != null) {
                    formApplyRow.CustomerBudget = CustomerBudget.GetValueOrDefault();
                }
                if (CustomerBudgetRemain != null) {
                    formApplyRow.CustomerBudgetRemain = CustomerBudgetRemain.GetValueOrDefault();
                }
                if (OUBudget != null) {
                    formApplyRow.OUBudget = OUBudget.GetValueOrDefault();
                }
                if (OUAppovedAmount != null) {
                    formApplyRow.OUAppovedAmount = OUAppovedAmount.GetValueOrDefault();
                }
                if (OUApprovingAmount != null) {
                    formApplyRow.OUApprovingAmount = OUApprovingAmount.GetValueOrDefault();
                }
                if (OUCompletedAmount != null) {
                    formApplyRow.OUCompletedAmount = OUCompletedAmount.GetValueOrDefault();
                }
                if (OUReimbursedAmount != null) {
                    formApplyRow.OUReimbursedAmount = OUReimbursedAmount.GetValueOrDefault();
                }
                if (OUBudgetRemain != null) {
                    formApplyRow.OUBudgetRemain = OUBudgetRemain.GetValueOrDefault();
                }
                if (OUBudgetRate != null) {
                    formApplyRow.OUBudgetRate = OUBudgetRate.GetValueOrDefault();
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formApplyRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formApplyRow.RealAttachedFileName = RealAttachedFileName;
                }
                if (Remark != null && Remark != string.Empty) {
                    formApplyRow.Remark = Remark;
                }
                formApplyRow.FormApplyName = FormApplyName;
                formApplyRow.ReimburseRequirements = ReimburseRequirements;

                this.TAForm.Update(formRow);
                this.TAFormApply.Update(formApplyRow);

                ArrayList deletedSKURows = new ArrayList();//save sku rows and expense rows that not deleted                
                decimal totalAmount = 0;//计算总申请金额
                foreach (FormDS.FormApplySKUDetailRow skuRow in this.FormDataSet.FormApplySKUDetail) {
                    // 与父表绑定
                    if (skuRow.RowState != DataRowState.Deleted) {
                        int oldID = skuRow.FormApplySKUDetailID;
                        this.TAFormApplySKUDetail.Update(skuRow);
                        int newID = skuRow.FormApplySKUDetailID;
                        FormDS.FormApplyExpenseDetailRow[] dr = (FormDS.FormApplyExpenseDetailRow[])this.FormDataSet.FormApplyExpenseDetail.Select("FormApplySKUDetailID = " + oldID.ToString());
                        for (int i = 0; i < dr.Length; i++) {
                            dr[i].FormApplySKUDetailID = newID;
                            if (dr[i].RowState != DataRowState.Deleted) {
                                totalAmount += dr[i].Amount;
                            }
                            dr[i].RepeatFormInfo = UtilityBLL.GenerateRepeatFormStr(this.TAForm.GetRepeatFormApplyNo(formApplyRow.PromotionBeginDate, formApplyRow.PromotionEndDate, dr[i].ExpenseItemID, formApplyRow.ShopID, formApplyRow.FormApplyID));
                        }
                    } else {
                        deletedSKURows.Add(skuRow);
                    }
                }
                //save expense detail rows

                foreach (FormDS.FormApplyExpenseDetailRow expenseRow in this.FormDataSet.FormApplyExpenseDetail) {
                    if (expenseRow.RowState != DataRowState.Deleted) {
                        expenseRow.RepeatFormInfo = UtilityBLL.GenerateRepeatFormStr(this.TAForm.GetRepeatFormApplyNo(formApplyRow.PromotionBeginDate, formApplyRow.PromotionEndDate, expenseRow.ExpenseItemID, formApplyRow.ShopID, formApplyRow.FormApplyID));
                    }
                }

                this.TAFormApplyExpenseDetail.Update(this.FormDataSet.FormApplyExpenseDetail);
                //save deleted sku rows
                foreach (FormDS.FormApplySKUDetailRow skuRow in deletedSKURows) {
                    if (skuRow.RowState == DataRowState.Deleted) {
                        this.TAFormApplySKUDetail.Update(skuRow);
                    }
                }
                formApplyRow.Amount = totalAmount;
                this.TAFormApply.Update(formApplyRow);

                this.TAFormApplySplitRate.Update(FormDataSet.FormApplySplitRate);

                // 正式提交或草稿
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["Apply_Amount"] = totalAmount;
                dic["Expense_Category"] = getExpenseCategoryIDByExpenseSubCategoryID(formApplyRow.ExpenseSubCategoryID).ToString();
                //dic["Dept"] = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID)[0].OrganizationUnitCode;
                APHelper AP = new APHelper();
                new APFlowBLL().ApplyForm(AP, TAForm, null, formRow, formRow.OrganizationUnitID, FlowTemplate, StatusID, dic);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void DeleteFormApply(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplySplitRate, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplySKUDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplyExpenseDetail, transaction);

                this.TAFormApplyExpenseDetail.DeleteByFormApplyID(FormID);
                this.TAFormApplySKUDetail.DeleteByFormApplyID(FormID);
                this.TAFormApplySplitRate.DeleteByApplyID(FormID);
                this.TAFormApply.DeleteByID(FormID);
                this.TAForm.DeleteByID(FormID);

                transaction.Commit();

            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                if (transaction != null) {
                    transaction.Dispose();
                }
            }

        }

        #endregion

        #region FormApply for no promotion

        public void AddFormApplyGeneral(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        DateTime BeginPeriod, DateTime EndPeriod, int CustomerID, int ShopID, int PaymentTypeID, int ExpenseSubCategoryID, string ContractNo, DateTime PromotionBeginDate, DateTime PromotionEndDate,
                        int PromotionScopeID, int PromotionTypeID, string PromotionDesc, int ShelfTypeID, int? FirstVolume, int? SecondVolume, int? ThirdVolume, int? EstimatedSaleVolume, decimal? CustomerBudget, decimal? CustomerBudgetRemain,
                        decimal? OUBudget, decimal? OUAppovedAmount, decimal? OUApprovingAmount, decimal? OUCompletedAmount, decimal? OUReimbursedAmount, decimal? OUBudgetRemain, decimal? OUBudgetRate,
                        string AttachedFileName, string RealAttachedFileName, string Remark, int ReimburseRequirements, string FlowTemplate, string FormApplyName) {
            APHelper AP = new APHelper();
            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplySKUDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplyExpenseDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplySplitRate, transaction);

                //处理单据的内容
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
                formRow.PageType = (int)SystemEnums.PageType.GeneralApply;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormApplyRow formApplyRow = this.FormDataSet.FormApply.NewFormApplyRow();
                formApplyRow.FormApplyID = formRow.FormID;
                formApplyRow.Period = DateTime.Parse(BeginPeriod.Year.ToString() + "-" + BeginPeriod.Month.ToString() + "-01");
                formApplyRow.BeginPeriod = DateTime.Parse(BeginPeriod.Year.ToString() + "-" + BeginPeriod.Month.ToString() + "-01");
                formApplyRow.EndPeriod = DateTime.Parse(EndPeriod.Year.ToString() + "-" + EndPeriod.Month.ToString() + "-01");
                formApplyRow.ShopID = ShopID;
                formApplyRow.CustomerID = CustomerID;
                formApplyRow.PaymentTypeID = PaymentTypeID;
                formApplyRow.ExpenseSubCategoryID = ExpenseSubCategoryID;
                if (ContractNo != "") {
                    formApplyRow.ContractNo = ContractNo;
                }
                formApplyRow.PromotionBeginDate = PromotionBeginDate;
                formApplyRow.PromotionEndDate = PromotionEndDate;
                formApplyRow.PromotionScopeID = PromotionScopeID;
                formApplyRow.PromotionTypeID = PromotionTypeID;
                if (PromotionDesc != "") {
                    formApplyRow.PromotionDesc = PromotionDesc;
                }
                formApplyRow.ShelfTypeID = ShelfTypeID;
                if (FirstVolume != null) {
                    formApplyRow.FirstVolume = FirstVolume.GetValueOrDefault();
                }
                if (SecondVolume != null) {
                    formApplyRow.SecondVolume = SecondVolume.GetValueOrDefault();
                }
                if (ThirdVolume != null) {
                    formApplyRow.ThirdVolume = ThirdVolume.GetValueOrDefault();
                }
                if (EstimatedSaleVolume != null) {
                    formApplyRow.EstimatedSaleVolume = EstimatedSaleVolume.GetValueOrDefault();
                }
                if (CustomerBudget != null) {
                    formApplyRow.CustomerBudget = CustomerBudget.GetValueOrDefault();
                }
                if (CustomerBudgetRemain != null) {
                    formApplyRow.CustomerBudgetRemain = CustomerBudgetRemain.GetValueOrDefault();
                }
                if (OUBudget != null) {
                    formApplyRow.OUBudget = OUBudget.GetValueOrDefault();
                }
                if (OUAppovedAmount != null) {
                    formApplyRow.OUAppovedAmount = OUAppovedAmount.GetValueOrDefault();
                }
                if (OUApprovingAmount != null) {
                    formApplyRow.OUApprovingAmount = OUApprovingAmount.GetValueOrDefault();
                }
                if (OUCompletedAmount != null) {
                    formApplyRow.OUCompletedAmount = OUCompletedAmount.GetValueOrDefault();
                }
                if (OUReimbursedAmount != null) {
                    formApplyRow.OUReimbursedAmount = OUReimbursedAmount.GetValueOrDefault();
                }
                if (OUBudgetRemain != null) {
                    formApplyRow.OUBudgetRemain = OUBudgetRemain.GetValueOrDefault();
                }
                if (OUBudgetRate != null) {
                    formApplyRow.OUBudgetRate = OUBudgetRate.GetValueOrDefault();
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formApplyRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formApplyRow.RealAttachedFileName = RealAttachedFileName;
                }
                if (Remark != null && Remark != string.Empty) {
                    formApplyRow.Remark = Remark;
                }
                formApplyRow.ReimburseRequirements = ReimburseRequirements;

                formApplyRow.Amount = 0;//先赋默认值
                formApplyRow.IsClose = false;
                formApplyRow.IsComplete = false;
                formApplyRow.IsAutoSplit = false;

                formApplyRow.FormApplyName = FormApplyName;
                this.FormDataSet.FormApply.AddFormApplyRow(formApplyRow);
                this.TAFormApply.Update(formApplyRow);

                //处理明细
                decimal totalAmount = 0;//计算总申请金额
                //处理子表
                foreach (FormDS.FormApplyDetailViewRow viewRow in this.FormDataSet.FormApplyDetailView) {
                    if (viewRow.RowState != DataRowState.Deleted) {
                        FormDS.FormApplySKUDetailRow skuRow = this.FormDataSet.FormApplySKUDetail.NewFormApplySKUDetailRow();
                        skuRow.FormApplyID = formApplyRow.FormApplyID;
                        skuRow.SKUID = viewRow.SKUID;
                        skuRow.Remark = viewRow.Remark;
                        this.FormDataSet.FormApplySKUDetail.AddFormApplySKUDetailRow(skuRow);
                        this.TAFormApplySKUDetail.Update(skuRow);

                        FormDS.FormApplyExpenseDetailRow expenseRow = this.FormDataSet.FormApplyExpenseDetail.NewFormApplyExpenseDetailRow();
                        expenseRow.FormApplySKUDetailID = skuRow.FormApplySKUDetailID;
                        expenseRow.ExpenseItemID = viewRow.ExpenseItemID;
                        expenseRow.Amount = viewRow.Amount;
                        this.FormDataSet.FormApplyExpenseDetail.AddFormApplyExpenseDetailRow(expenseRow);
                        expenseRow.RepeatFormInfo = UtilityBLL.GenerateRepeatFormStr(this.TAForm.GetRepeatFormApplyNo(formApplyRow.PromotionBeginDate, formApplyRow.PromotionEndDate, expenseRow.ExpenseItemID, formApplyRow.ShopID, formApplyRow.FormApplyID));
                        this.TAFormApplyExpenseDetail.Update(expenseRow);

                        totalAmount += viewRow.Amount;
                    }
                }

                //分摊比例
                foreach (FormDS.FormApplySplitRateRow item in FormDataSet.FormApplySplitRate) {
                    item.FormApplyID = formApplyRow.FormApplyID;
                }
                this.TAFormApplySplitRate.Update(FormDataSet.FormApplySplitRate);

                formApplyRow.Amount = totalAmount;
                if (!formApplyRow.IsEstimatedSaleVolumeNull()) {
                    formApplyRow.PackageUnitPrice = decimal.Round(formApplyRow.Amount / formApplyRow.EstimatedSaleVolume, 2);
                }
                TAFormApply.Update(formApplyRow);

                // 正式提交或草稿

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["Apply_Amount"] = totalAmount;
                // dic["Dept"] = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID)[0].OrganizationUnitCode;
                dic["Expense_Category"] = getExpenseCategoryIDByExpenseSubCategoryID(formApplyRow.ExpenseSubCategoryID).ToString();

                new APFlowBLL().ApplyForm(AP, TAForm, RejectedFormID, formRow, OrganizationUnitID, FlowTemplate, StatusID, dic);

                transaction.Commit();
            } catch (ApplicationException ex) {
                transaction.Rollback();
                throw ex;
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }


        public void UpdateFormApplyGeneral(int FormID, SystemEnums.FormStatus StatusID, SystemEnums.FormType FormTypeID, int ShopID, int PaymentTypeID, string ContractNo, DateTime PromotionBeginDate,
                        DateTime PromotionEndDate, int PromotionScopeID, int PromotionTypeID, string PromotionDesc, int ShelfTypeID, int? FirstVolume, int? SecondVolume, int? ThirdVolume, int? EstimatedSaleVolume,
                        decimal? CustomerBudget, decimal? CustomerBudgetRemain, decimal? OUBudget, decimal? OUAppovedAmount, decimal? OUApprovingAmount, decimal? OUCompletedAmount, decimal? OUReimbursedAmount,
                        decimal? OUBudgetRemain, decimal? OUBudgetRate, string AttachedFileName, string RealAttachedFileName, string Remark, int ReimburseRequirements, string FlowTemplate, string FormApplyName) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplySKUDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplyExpenseDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplySplitRate, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormApplyRow formApplyRow = this.TAFormApply.GetDataByID(FormID)[0];

                //处理单据的内容
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
                //申请单内容
                formApplyRow.ShopID = ShopID;
                formApplyRow.PaymentTypeID = PaymentTypeID;
                if (ContractNo != "") {
                    formApplyRow.ContractNo = ContractNo;
                }
                formApplyRow.PromotionBeginDate = PromotionBeginDate;
                formApplyRow.PromotionEndDate = PromotionEndDate;
                formApplyRow.PromotionScopeID = PromotionScopeID;
                formApplyRow.PromotionTypeID = PromotionTypeID;
                if (PromotionDesc != "") {
                    formApplyRow.PromotionDesc = PromotionDesc;
                }
                formApplyRow.ShelfTypeID = ShelfTypeID;
                if (FirstVolume != null) {
                    formApplyRow.FirstVolume = FirstVolume.GetValueOrDefault();
                }
                if (SecondVolume != null) {
                    formApplyRow.SecondVolume = SecondVolume.GetValueOrDefault();
                }
                if (ThirdVolume != null) {
                    formApplyRow.ThirdVolume = ThirdVolume.GetValueOrDefault();
                }
                if (EstimatedSaleVolume != null) {
                    formApplyRow.PackageUnitPrice = EstimatedSaleVolume.GetValueOrDefault();
                }
                if (CustomerBudget != null) {
                    formApplyRow.CustomerBudget = CustomerBudget.GetValueOrDefault();
                }
                if (CustomerBudgetRemain != null) {
                    formApplyRow.CustomerBudgetRemain = CustomerBudgetRemain.GetValueOrDefault();
                }
                if (OUBudget != null) {
                    formApplyRow.OUBudget = OUBudget.GetValueOrDefault();
                }
                if (OUAppovedAmount != null) {
                    formApplyRow.OUAppovedAmount = OUAppovedAmount.GetValueOrDefault();
                }
                if (OUApprovingAmount != null) {
                    formApplyRow.OUApprovingAmount = OUApprovingAmount.GetValueOrDefault();
                }
                if (OUCompletedAmount != null) {
                    formApplyRow.OUCompletedAmount = OUCompletedAmount.GetValueOrDefault();
                }
                if (OUReimbursedAmount != null) {
                    formApplyRow.OUReimbursedAmount = OUReimbursedAmount.GetValueOrDefault();
                }
                if (OUBudgetRemain != null) {
                    formApplyRow.OUBudgetRemain = OUBudgetRemain.GetValueOrDefault();
                }
                if (OUBudgetRate != null) {
                    formApplyRow.OUBudgetRate = OUBudgetRate.GetValueOrDefault();
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formApplyRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formApplyRow.RealAttachedFileName = RealAttachedFileName;
                }
                if (Remark != null && Remark != string.Empty) {
                    formApplyRow.Remark = Remark;
                }
                formApplyRow.FormApplyName = FormApplyName;
                formApplyRow.ReimburseRequirements = ReimburseRequirements;

                this.TAForm.Update(formRow);
                this.TAFormApply.Update(formApplyRow);

                ArrayList deletedSKURows = new ArrayList();//save sku rows and expense rows that not deleted                
                decimal totalAmount = 0;//计算总申请金额
                //先删除所有明细记录
                //FormDS.FormApplyExpenseDetailDataTable deleteExpenseTable = this.TAFormApplyExpenseDetail.GetDataByFormApplyID(formApplyRow.FormApplyID);
                //foreach (FormDS.FormApplyExpenseDetailRow deleteExpenseRow in deleteExpenseTable) {
                //    deleteExpenseRow.Delete();
                //}
                //this.TAFormApplyExpenseDetail.Update(deleteExpenseTable);

                //FormDS.FormApplySKUDetailDataTable deleteSKUTable = this.TAFormApplySKUDetail.GetDataByFormApplyID(formApplyRow.FormApplyID);
                //foreach (FormDS.FormApplySKUDetailRow deleteSKURow in deleteSKUTable) {
                //    deleteSKURow.Delete();
                //}
                //this.TAFormApplySKUDetail.Update(deleteSKUTable);

                this.TAFormApplyExpenseDetail.DeleteByFormApplyID(FormID);
                this.TAFormApplySKUDetail.DeleteByFormApplyID(FormID);

                //再插入新的表
                foreach (FormDS.FormApplyDetailViewRow viewRow in this.FormDataSet.FormApplyDetailView) {
                    if (viewRow.RowState != DataRowState.Deleted) {
                        FormDS.FormApplySKUDetailRow skuRow = this.FormDataSet.FormApplySKUDetail.NewFormApplySKUDetailRow();
                        skuRow.FormApplyID = formApplyRow.FormApplyID;
                        skuRow.SKUID = viewRow.SKUID;
                        skuRow.Remark = viewRow.Remark;
                        this.FormDataSet.FormApplySKUDetail.AddFormApplySKUDetailRow(skuRow);
                        this.TAFormApplySKUDetail.Update(skuRow);

                        FormDS.FormApplyExpenseDetailRow expenseRow = this.FormDataSet.FormApplyExpenseDetail.NewFormApplyExpenseDetailRow();
                        expenseRow.FormApplySKUDetailID = skuRow.FormApplySKUDetailID;
                        expenseRow.ExpenseItemID = viewRow.ExpenseItemID;
                        expenseRow.Amount = viewRow.Amount;
                        expenseRow.RepeatFormInfo = UtilityBLL.GenerateRepeatFormStr(this.TAForm.GetRepeatFormApplyNo(formApplyRow.PromotionBeginDate, formApplyRow.PromotionEndDate, expenseRow.ExpenseItemID, formApplyRow.ShopID, formApplyRow.FormApplyID));
                        this.FormDataSet.FormApplyExpenseDetail.AddFormApplyExpenseDetailRow(expenseRow);
                        this.TAFormApplyExpenseDetail.Update(expenseRow);

                        totalAmount += viewRow.Amount;
                    }
                }

                this.TAFormApplySplitRate.Update(FormDataSet.FormApplySplitRate);

                formApplyRow.Amount = totalAmount;
                if (!formApplyRow.IsEstimatedSaleVolumeNull()) {
                    formApplyRow.PackageUnitPrice = decimal.Round(formApplyRow.Amount / formApplyRow.EstimatedSaleVolume, 2);
                }
                this.TAFormApply.Update(formApplyRow);

                // 正式提交或草稿

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["Apply_Amount"] = totalAmount;
                //  dic["Dept"] = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID)[0].OrganizationUnitCode;
                dic["Expense_Category"] = getExpenseCategoryIDByExpenseSubCategoryID(formApplyRow.ExpenseSubCategoryID).ToString();//此处待改动
                APHelper AP = new APHelper();
                new APFlowBLL().ApplyForm(AP, TAForm, null, formRow, formRow.OrganizationUnitID, FlowTemplate, StatusID, dic);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        #endregion

        #region FormApply for rebate

        public void AddFormApplyRebate(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        DateTime BeginPeriod, DateTime EndPeriod, int CustomerID, int ShopID, int PaymentTypeID, int ExpenseSubCategoryID, string ContractNo, decimal? CustomerBudget,
                        decimal? CustomerBudgetRemain, decimal? OUBudget, decimal? OUAppovedAmount, decimal? OUApprovingAmount, decimal? OUCompletedAmount, decimal? OUReimbursedAmount, decimal? OUBudgetRemain,
                        decimal? OUBudgetRate, string AttachedFileName, string RealAttachedFileName, string Remark, string FlowTemplate, string FormApplyName) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplySKUDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplyExpenseDetail, transaction);

                //处理单据的内容
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
                formRow.PageType = (int)SystemEnums.PageType.RebateApply;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormApplyRow formApplyRow = this.FormDataSet.FormApply.NewFormApplyRow();
                formApplyRow.FormApplyID = formRow.FormID;
                formApplyRow.Period = DateTime.Parse(BeginPeriod.Year.ToString() + "-" + BeginPeriod.Month.ToString() + "-01");
                formApplyRow.BeginPeriod = DateTime.Parse(BeginPeriod.Year.ToString() + "-" + BeginPeriod.Month.ToString() + "-01");
                formApplyRow.EndPeriod = DateTime.Parse(EndPeriod.Year.ToString() + "-" + EndPeriod.Month.ToString() + "-01");
                formApplyRow.ShopID = ShopID;
                formApplyRow.CustomerID = CustomerID;
                formApplyRow.PaymentTypeID = PaymentTypeID;
                formApplyRow.ExpenseSubCategoryID = ExpenseSubCategoryID;
                if (ContractNo != "") {
                    formApplyRow.ContractNo = ContractNo;
                }
                if (CustomerBudget != null) {
                    formApplyRow.CustomerBudget = CustomerBudget.GetValueOrDefault();
                }
                if (CustomerBudgetRemain != null) {
                    formApplyRow.CustomerBudgetRemain = CustomerBudgetRemain.GetValueOrDefault();
                }
                if (OUBudget != null) {
                    formApplyRow.OUBudget = OUBudget.GetValueOrDefault();
                }
                if (OUAppovedAmount != null) {
                    formApplyRow.OUAppovedAmount = OUAppovedAmount.GetValueOrDefault();
                }
                if (OUApprovingAmount != null) {
                    formApplyRow.OUApprovingAmount = OUApprovingAmount.GetValueOrDefault();
                }
                if (OUCompletedAmount != null) {
                    formApplyRow.OUCompletedAmount = OUCompletedAmount.GetValueOrDefault();
                }
                if (OUReimbursedAmount != null) {
                    formApplyRow.OUReimbursedAmount = OUReimbursedAmount.GetValueOrDefault();
                }
                if (OUBudgetRemain != null) {
                    formApplyRow.OUBudgetRemain = OUBudgetRemain.GetValueOrDefault();
                }
                if (OUBudgetRate != null) {
                    formApplyRow.OUBudgetRate = OUBudgetRate.GetValueOrDefault();
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formApplyRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formApplyRow.RealAttachedFileName = RealAttachedFileName;
                }
                if (Remark != null && Remark != string.Empty) {
                    formApplyRow.Remark = Remark;
                }

                formApplyRow.Amount = 0;//先赋默认值
                formApplyRow.IsClose = false;
                formApplyRow.IsComplete = false;
                formApplyRow.IsAutoSplit = false;

                formApplyRow.FormApplyName = FormApplyName;
                this.FormDataSet.FormApply.AddFormApplyRow(formApplyRow);
                this.TAFormApply.Update(formApplyRow);

                //处理明细
                decimal totalAmount = 0;//计算总申请金额
                //处理子表
                foreach (FormDS.FormApplyDetailViewRow viewRow in this.FormDataSet.FormApplyDetailView) {
                    if (viewRow.RowState != DataRowState.Deleted) {
                        FormDS.FormApplySKUDetailRow skuRow = this.FormDataSet.FormApplySKUDetail.NewFormApplySKUDetailRow();
                        skuRow.FormApplyID = formApplyRow.FormApplyID;
                        skuRow.SKUID = viewRow.SKUID;
                        if (!viewRow.IsSalesAmountNull()) {
                            skuRow.SalesAmount = viewRow.SalesAmount;
                        }
                        this.FormDataSet.FormApplySKUDetail.AddFormApplySKUDetailRow(skuRow);
                        this.TAFormApplySKUDetail.Update(skuRow);

                        FormDS.FormApplyExpenseDetailRow expenseRow = this.FormDataSet.FormApplyExpenseDetail.NewFormApplyExpenseDetailRow();
                        expenseRow.FormApplySKUDetailID = skuRow.FormApplySKUDetailID;
                        expenseRow.ExpenseItemID = viewRow.ExpenseItemID;
                        expenseRow.Amount = viewRow.Amount;
                        this.FormDataSet.FormApplyExpenseDetail.AddFormApplyExpenseDetailRow(expenseRow);
                        this.TAFormApplyExpenseDetail.Update(expenseRow);

                        totalAmount += viewRow.Amount;
                    }
                }
                formApplyRow.Amount = totalAmount;
                TAFormApply.Update(formApplyRow);

                // 正式提交或草稿

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["Apply_Amount"] = totalAmount;
                // dic["Dept"] = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID)[0].OrganizationUnitCode;
                dic["Expense_Category"] = getExpenseCategoryIDByExpenseSubCategoryID(formApplyRow.ExpenseSubCategoryID).ToString();//此处待改动
                APHelper AP = new APHelper();
                new APFlowBLL().ApplyForm(AP, TAForm, null, formRow, formRow.OrganizationUnitID, FlowTemplate, StatusID, dic);

                transaction.Commit();
            } catch (ApplicationException ex) {
                transaction.Rollback();
                throw ex;
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormApplyRebate(int FormID, SystemEnums.FormStatus StatusID, SystemEnums.FormType FormTypeID, int ShopID, int PaymentTypeID, string ContractNo,
                        decimal? CustomerBudget, decimal? CustomerBudgetRemain, decimal? OUBudget, decimal? OUAppovedAmount, decimal? OUApprovingAmount, decimal? OUCompletedAmount, decimal? OUReimbursedAmount,
                        decimal? OUBudgetRemain, decimal? OUBudgetRate, string AttachedFileName, string RealAttachedFileName, string Remark, string FlowTemplate, string FormApplyName) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplySKUDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormApplyExpenseDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormApplyRow formApplyRow = this.TAFormApply.GetDataByID(FormID)[0];

                //处理单据的内容
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
                //申请单内容
                formApplyRow.ShopID = ShopID;
                formApplyRow.PaymentTypeID = PaymentTypeID;
                if (ContractNo != "") {
                    formApplyRow.ContractNo = ContractNo;
                }
                if (CustomerBudget != null) {
                    formApplyRow.CustomerBudget = CustomerBudget.GetValueOrDefault();
                }
                if (CustomerBudgetRemain != null) {
                    formApplyRow.CustomerBudgetRemain = CustomerBudgetRemain.GetValueOrDefault();
                }
                if (OUBudget != null) {
                    formApplyRow.OUBudget = OUBudget.GetValueOrDefault();
                }
                if (OUAppovedAmount != null) {
                    formApplyRow.OUAppovedAmount = OUAppovedAmount.GetValueOrDefault();
                }
                if (OUApprovingAmount != null) {
                    formApplyRow.OUApprovingAmount = OUApprovingAmount.GetValueOrDefault();
                }
                if (OUCompletedAmount != null) {
                    formApplyRow.OUCompletedAmount = OUCompletedAmount.GetValueOrDefault();
                }
                if (OUReimbursedAmount != null) {
                    formApplyRow.OUReimbursedAmount = OUReimbursedAmount.GetValueOrDefault();
                }
                if (OUBudgetRemain != null) {
                    formApplyRow.OUBudgetRemain = OUBudgetRemain.GetValueOrDefault();
                }
                if (OUBudgetRate != null) {
                    formApplyRow.OUBudgetRate = OUBudgetRate.GetValueOrDefault();
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formApplyRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formApplyRow.RealAttachedFileName = RealAttachedFileName;
                }
                if (Remark != null && Remark != string.Empty) {
                    formApplyRow.Remark = Remark;
                }
                formApplyRow.FormApplyName = FormApplyName;
                this.TAForm.Update(formRow);
                this.TAFormApply.Update(formApplyRow);

                decimal totalAmount = 0;//计算总申请金额

                this.TAFormApplyExpenseDetail.DeleteByFormApplyID(FormID);
                this.TAFormApplySKUDetail.DeleteByFormApplyID(FormID);

                //再插入新的表
                foreach (FormDS.FormApplyDetailViewRow viewRow in this.FormDataSet.FormApplyDetailView) {
                    if (viewRow.RowState != DataRowState.Deleted) {
                        FormDS.FormApplySKUDetailRow skuRow = this.FormDataSet.FormApplySKUDetail.NewFormApplySKUDetailRow();
                        skuRow.FormApplyID = formApplyRow.FormApplyID;
                        skuRow.SKUID = viewRow.SKUID;
                        if (!viewRow.IsSalesAmountNull()) {
                            skuRow.SalesAmount = viewRow.SalesAmount;
                        }
                        this.FormDataSet.FormApplySKUDetail.AddFormApplySKUDetailRow(skuRow);
                        this.TAFormApplySKUDetail.Update(skuRow);

                        FormDS.FormApplyExpenseDetailRow expenseRow = this.FormDataSet.FormApplyExpenseDetail.NewFormApplyExpenseDetailRow();
                        expenseRow.FormApplySKUDetailID = skuRow.FormApplySKUDetailID;
                        expenseRow.ExpenseItemID = viewRow.ExpenseItemID;
                        expenseRow.Amount = viewRow.Amount;
                        this.FormDataSet.FormApplyExpenseDetail.AddFormApplyExpenseDetailRow(expenseRow);
                        this.TAFormApplyExpenseDetail.Update(expenseRow);

                        totalAmount += viewRow.Amount;
                    }
                }

                formApplyRow.Amount = totalAmount;
                this.TAFormApply.Update(formApplyRow);

                // 正式提交或草稿

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["Apply_Amount"] = totalAmount;
                // dic["Dept"] = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID)[0].OrganizationUnitCode;
                dic["Expense_Category"] = getExpenseCategoryIDByExpenseSubCategoryID(formApplyRow.ExpenseSubCategoryID).ToString();//此处待改动
                APHelper AP = new APHelper();
                new APFlowBLL().ApplyForm(AP, TAForm, null, formRow, formRow.OrganizationUnitID, FlowTemplate, StatusID, dic);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }


        #endregion

        #region Other function

        public int GetRebateApplyCountByParameter(int CustomerID, int RebateYear, int RebateMonth, int ExpenseSubCategoryID) {
            if (this.TAFormApply.GetApplyCountByParameter(CustomerID, RebateYear, RebateMonth, ExpenseSubCategoryID) == null) {
                return 0;
            } else {
                return (int)this.TAFormApply.GetApplyCountByParameter(CustomerID, RebateYear, RebateMonth, ExpenseSubCategoryID);
            }
        }

        public void CopyApplyForm(int FormApplyID, DateTime BeginPeriod, DateTime EndPeriod) {
            this.TAFormApply.CopyApplyForm(FormApplyID, BeginPeriod, EndPeriod);
        }

        public object[] GetCustomerTimesLimitByParameter(int CustomerID, int ExpenseItemID, int FinanceApplyYear) {
            bool? IsValid = true;
            int? TimesLimit = 0;
            int? ActualLimit = 0;
            object[] calculateAssistant = new object[3];
            this.TAFormApply.GetCustomerTimesLimitByParameter(CustomerID, ExpenseItemID, FinanceApplyYear, ref IsValid, ref TimesLimit, ref ActualLimit);
            calculateAssistant[0] = IsValid.GetValueOrDefault();
            calculateAssistant[1] = TimesLimit.GetValueOrDefault();
            calculateAssistant[2] = ActualLimit.GetValueOrDefault();
            return calculateAssistant;
        }

        public object[] GetCustomerAmountLimitByParameter(int CustomerID, int ApplyYear, decimal InContractAmount) {
            bool? IsValid = true;
            decimal? AmountLimit = 0;
            decimal? ActualAmount = 0;
            object[] calculateAssistant = new object[4];
            this.TAFormApply.GetCustomerAmountLimitByParameter(CustomerID, ApplyYear, InContractAmount, ref IsValid, ref AmountLimit, ref ActualAmount);
            calculateAssistant[0] = IsValid.GetValueOrDefault();
            calculateAssistant[1] = AmountLimit.GetValueOrDefault();
            calculateAssistant[2] = ActualAmount.GetValueOrDefault();
            calculateAssistant[3] = AmountLimit.GetValueOrDefault() - ActualAmount.GetValueOrDefault();
            return calculateAssistant;
        }

        //根据FormApplyID，查找与该FormApply相关的待审批及退回待修改状态的报销单
        public int GetProcessingFormReimburseByApplyID(int FormApplyID) {
            return new FormReimburseTableAdapter().GetProcessingDataByApplyId(FormApplyID).Count;
        }

        //根据FormApplyID，查找与该FormApply相关的待审批及已审批状态的报销单
        public int GetEnabledFormReimburseByApplyID(int FormApplyID) {
            return (int)new FormReimburseTableAdapter().GetEnabledReimburseByApplyId(FormApplyID);
        }

        public void CloseFormApply(int FormApplyID) {
            FormDS.FormApplyRow row = this.TAFormApply.GetDataByID(FormApplyID)[0];
            row.IsClose = true;
            this.TAFormApply.Update(row);
        }

        public void PrintCountForFormApply(int FormApplyID) {
            FormDS.FormApplyRow row = this.TAFormApply.GetDataByID(FormApplyID)[0];
            if (row.IsPrintCountNull()) {
                row.PrintCount = 1;
            } else {
                row.PrintCount = row.PrintCount + 1;
            }
            this.TAFormApply.Update(row);
        }

        #endregion

        #region Execute

        public void ExecuteConfirm(int FormApplyID, DateTime AccruedPeriod, int ConfirmCompleteUserID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormApplyExpenseDetail);
                TableAdapterHelper.SetTransaction(this.TAFormApply, transaction);
                //处理申请表的内容
                FormDS.FormApplyRow formApplyRow = this.TAFormApply.GetDataByID(FormApplyID)[0];
                formApplyRow.IsComplete = true;
                formApplyRow.AccruedPeriod = AccruedPeriod;
                formApplyRow.ConfirmCompleteDate = DateTime.Now;
                formApplyRow.ConfirmCompleteUserID = ConfirmCompleteUserID;

                //明细表
                decimal totalAmount = 0;//计算总申请金额
                foreach (FormDS.FormApplyDetailViewRow viewRow in this.FormDataSet.FormApplyDetailView) {
                    FormDS.FormApplyExpenseDetailRow detailRow = this.FormDataSet.FormApplyExpenseDetail.FindByFormApplyExpenseDetailID(viewRow.FormApplyExpenseDetailID);
                    detailRow.AccruedAmount = viewRow.AccruedAmount;
                    totalAmount += detailRow.AccruedAmount;
                }
                this.TAFormApplyExpenseDetail.Update(this.FormDataSet.FormApplyExpenseDetail);
                formApplyRow.AccruedAmount = totalAmount;
                this.TAFormApply.Update(formApplyRow);
                transaction.Commit();

            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }

        }

        public void ExecuteConfirmForPromotion(int FormApplyID, DateTime AccruedPeriod, int ConfirmCompleteUserID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormApplyExpenseDetail);
                TableAdapterHelper.SetTransaction(this.TAFormApply, transaction);
                //处理申请表的内容
                FormDS.FormApplyRow formApplyRow = this.TAFormApply.GetDataByID(FormApplyID)[0];
                formApplyRow.IsComplete = true;
                formApplyRow.AccruedPeriod = AccruedPeriod;
                formApplyRow.ConfirmCompleteDate = DateTime.Now;
                formApplyRow.ConfirmCompleteUserID = ConfirmCompleteUserID;

                //明细表
                decimal totalAmount = 0;//计算总申请金额
                foreach (FormDS.FormApplyExpenseDetailRow detailRow in this.FormDataSet.FormApplyExpenseDetail) {
                    totalAmount += detailRow.AccruedAmount;
                }
                this.TAFormApplyExpenseDetail.Update(this.FormDataSet.FormApplyExpenseDetail);
                formApplyRow.AccruedAmount = totalAmount;
                this.TAFormApply.Update(formApplyRow);
                transaction.Commit();

            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }

        }

        public void ExecuteConfirmForRebate(int FormApplyID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormApplyExpenseDetail);
                TableAdapterHelper.SetTransaction(this.TAFormApply, transaction);
                //处理申请表的内容
                FormDS.FormApplyRow formApplyRow = this.TAFormApply.GetDataByID(FormApplyID)[0];
                formApplyRow.IsComplete = true;
                formApplyRow.AccruedPeriod = formApplyRow.Period;
                formApplyRow.ConfirmCompleteDate = DateTime.Now;
                formApplyRow.ConfirmCompleteUserID = this.TAForm.GetDataByID(FormApplyID)[0].UserID;
                formApplyRow.AccruedAmount = formApplyRow.Amount;
                this.TAFormApply.Update(formApplyRow);

                //明细表
                FormDS.FormApplyExpenseDetailDataTable detailTable = this.TAFormApplyExpenseDetail.GetDataByFormApplyID(FormApplyID);
                foreach (FormDS.FormApplyExpenseDetailRow detailRow in this.FormDataSet.FormApplyExpenseDetail) {
                    detailRow.AccruedAmount = detailRow.Amount;
                }
                this.TAFormApplyExpenseDetail.Update(detailTable);
                transaction.Commit();

            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }

        }

        public void ExecuteCancel(int FormApplyID, int ConfirmCompleteUserID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormApply, transaction);
                FormDS.FormRow formRow = new FormTableAdapter().GetDataByID(FormApplyID)[0];
                formRow.StatusID = (int)SystemEnums.FormStatus.Scrap;
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormApplyRow formApplyRow = this.TAFormApply.GetDataByID(FormApplyID)[0];
                formApplyRow.IsComplete = false;
                formApplyRow.ConfirmCompleteDate = DateTime.Now;
                formApplyRow.ConfirmCompleteUserID = ConfirmCompleteUserID;
                this.TAFormApply.Update(formApplyRow);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }

        }

        #endregion
    }
}
