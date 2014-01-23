using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.FormDSTableAdapters;
using System.Web.Security;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using lib.wf;
using BusinessObjects.ERSTableAdapters;


namespace BusinessObjects {
    public class SalesReimburseBLL {

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

        private FormReimburseTableAdapter m_FormReimburseAdapter;
        public FormReimburseTableAdapter TAFormReimburse {
            get {
                if (this.m_FormReimburseAdapter == null) {
                    this.m_FormReimburseAdapter = new FormReimburseTableAdapter();
                }
                return this.m_FormReimburseAdapter;
            }
        }

        private FormReimburseSKUDetailTableAdapter m_FormReimburseSKUDetailAdapter;
        public FormReimburseSKUDetailTableAdapter TAFormReimburseSKUDetail {
            get {
                if (this.m_FormReimburseSKUDetailAdapter == null) {
                    this.m_FormReimburseSKUDetailAdapter = new FormReimburseSKUDetailTableAdapter();
                }
                return this.m_FormReimburseSKUDetailAdapter;
            }
        }

        private FormReimburseDeliveryTableAdapter m_FormReimburseDeliveryAdapter;
        public FormReimburseDeliveryTableAdapter TAFormReimburseDelivery {
            get {
                if (this.m_FormReimburseDeliveryAdapter == null) {
                    this.m_FormReimburseDeliveryAdapter = new FormReimburseDeliveryTableAdapter();
                }
                return this.m_FormReimburseDeliveryAdapter;
            }
        }

        private FormReimburseDetailTableAdapter m_FormReimburseDetailAdapter;
        public FormReimburseDetailTableAdapter TAFormReimburseDetail {
            get {
                if (this.m_FormReimburseDetailAdapter == null) {
                    this.m_FormReimburseDetailAdapter = new FormReimburseDetailTableAdapter();
                }
                return this.m_FormReimburseDetailAdapter;
            }
        }

        private FormReimburseInvoiceTableAdapter m_FormReimburseInvoiceAdapter;
        public FormReimburseInvoiceTableAdapter TAFormReimburseInvoice {
            get {
                if (this.m_FormReimburseInvoiceAdapter == null) {
                    this.m_FormReimburseInvoiceAdapter = new FormReimburseInvoiceTableAdapter();
                }
                return this.m_FormReimburseInvoiceAdapter;
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

        private PKRecordTableAdapter m_PKRecordAdapter;
        public PKRecordTableAdapter TAPKRecord {
            get {
                if (this.m_PKRecordAdapter == null) {
                    this.m_PKRecordAdapter = new PKRecordTableAdapter();
                }
                return this.m_PKRecordAdapter;
            }
        }

        #endregion

        #region 获取数据

        public FormDS.FormDataTable GetFormByID(int FormID) {
            return this.TAForm.GetDataByID(FormID);
        }

        public FormDS.FormReimburseDataTable GetFormReimburseByID(int FormReimburseID) {
            return this.TAFormReimburse.GetDataByID(FormReimburseID);
        }

        public FormDS.FormReimburseSKUDetailDataTable GetFormReimburseSKUDetailByFormReimburseID(int FormReimburseID) {
            return this.TAFormReimburseSKUDetail.GetDataByFormReimburseID(FormReimburseID);
        }

        public FormDS.FormReimburseDetailDataTable GetFormReimburseDetailByFormReimburseD(int FormReimburseID) {
            return this.TAFormReimburseDetail.GetDataByFormReimburseID(FormReimburseID);
        }

        public FormDS.FormReimburseInvoiceDataTable GetFormReimburseInvoiceByFormReimburseID(int FormReimburseID) {
            return this.TAFormReimburseInvoice.GetDataByFormReimburseID(FormReimburseID);
        }

        public FormDS.FormReimburseDetailDataTable GetFormReimburseDetail() {
            return this.FormDataSet.FormReimburseDetail;
        }

        public FormDS.FormReimburseSKUDetailDataTable GetFormReimburseSKUDetail() {
            return this.FormDataSet.FormReimburseSKUDetail;
        }

        public FormDS.FormReimburseInvoiceDataTable GetFormReimburseInvoice() {
            return this.FormDataSet.FormReimburseInvoice;
        }

        public string GetRepeatedInvoiceFormNosByInvioceNo(string InvoiceNo) {
            string formNos = "";
            this.TAFormReimburseInvoice.GetRepeatedInvoiceByInvoiceNo(InvoiceNo, ref formNos);
            return formNos;
        }

        public FormDS.FormReimburseDeliveryDataTable GetFormReimburseDeliveryByFormReimburseSKUDetailID(int FormReimburseSKUDetailID) {
            return this.TAFormReimburseDelivery.GetDataByFormReimburseSKUDetailID(FormReimburseSKUDetailID);
        }

        //得到费用大类
        public int getExpenseCategoryIDByFormID(int FormID) {
            FormDS.FormApplyRow formApplyRow = this.TAFormApply.GetDataByID(FormID)[0];
            return new ExpenseSubCategoryTableAdapter().GetDataById(formApplyRow.ExpenseSubCategoryID)[0].ExpenseCategoryID;
        }
        #endregion

        #region FormReimburseDetail

        public void AddFormReimburseDetail(int? FormReimburseID, int FormApplyExpenseDetailID, string ApplyFormNo, int ApplyPaymentTypeID, DateTime ApplyPeriod, int ShopID, int SKUID,
            int ExpenseItemID, decimal ApplyAmount, decimal RemainAmount, decimal Amount) {

            FormDS.FormReimburseDetailRow rowDetail = this.FormDataSet.FormReimburseDetail.NewFormReimburseDetailRow();
            rowDetail.FormReimburseID = FormReimburseID.GetValueOrDefault();
            rowDetail.FormApplyExpenseDetailID = FormApplyExpenseDetailID;
            rowDetail.ApplyFormNo = ApplyFormNo;
            rowDetail.ApplyPeriod = ApplyPeriod;
            rowDetail.ShopID = ShopID;
            rowDetail.SKUID = SKUID;
            rowDetail.ExpenseItemID = ExpenseItemID;
            rowDetail.ApplyAmount = ApplyAmount;
            rowDetail.RemainAmount = RemainAmount;
            rowDetail.Amount = Amount;
            rowDetail.ApplyPaymentTypeID = ApplyPaymentTypeID;
            this.FormDataSet.FormReimburseDetail.AddFormReimburseDetailRow(rowDetail);
        }

        //获取实物报销费用项条数
        public int QueryExpenseItemCountByReimburseID(int FormReimburseID) {
            return (int)this.TAFormReimburse.QueryExpenseItemCountByReimburseID(FormReimburseID);
        }

        public decimal GetPayedAmountByFormApplyExpenseDetailID(int FormApplyExpenseDetailID) {
            return (decimal)this.TAFormReimburseDetail.GetPayedAmountByFormApplyExpenseDetailID(FormApplyExpenseDetailID);
        }


        #endregion

        #region FormReimburseInvoice

        public void UpdateFormReimburseInvoice(int FormReimburseInvoiceID, string InvoiceNo, decimal InvoiceAmount, string Remark) {

            FormDS.FormReimburseInvoiceDataTable table = this.FormDataSet.FormReimburseInvoice;
            FormDS.FormReimburseInvoiceRow rowDetail = table.FindByFormReimburseInvoiceID(FormReimburseInvoiceID);
            if (rowDetail == null)
                return;
            rowDetail.InvoiceNo = InvoiceNo;
            rowDetail.InvoiceAmount = InvoiceAmount;
            rowDetail.Remark = Remark;

        }

        public void AddFormReimburseInvoice(int? FormReimburseID, string InvoiceNo, decimal InvoiceAmount, string Remark) {

            FormDS.FormReimburseInvoiceRow rowDetail = this.FormDataSet.FormReimburseInvoice.NewFormReimburseInvoiceRow();
            rowDetail.FormReimburseID = FormReimburseID.GetValueOrDefault();
            rowDetail.InvoiceNo = InvoiceNo;
            rowDetail.InvoiceAmount = InvoiceAmount;
            rowDetail.Remark = Remark;
            rowDetail.SystemInfo = UtilityBLL.GenerateRepeatFormStr(this.TAForm.GetDataByInvoiceNo(InvoiceNo));
            // 填加行并进行更新处理
            this.FormDataSet.FormReimburseInvoice.AddFormReimburseInvoiceRow(rowDetail);
        }

        public void DeleteFormReimburseInvoiceByID(int FormReimburseInvoiceID) {
            for (int index = 0; index < this.FormDataSet.FormReimburseInvoice.Count; index++) {
                if ((int)this.FormDataSet.FormReimburseInvoice.Rows[index]["FormReimburseInvoiceID"] == FormReimburseInvoiceID) {
                    this.FormDataSet.FormReimburseInvoice.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region FormReimburseSKUDetail

        public void UpdateFormReimburseSKUDetail(int FormReimburseSKUDetailID, int SKUID, decimal UnitPrice, decimal Quantity, string Remark) {

            FormDS.FormReimburseSKUDetailRow rowDetail = this.FormDataSet.FormReimburseSKUDetail.FindByFormReimburseSKUDetailID(FormReimburseSKUDetailID);
            if (rowDetail == null)
                return;
            rowDetail.SKUID = SKUID;
            ERS.SKURow sku = new MasterDataBLL().GetSKUById(SKUID);
            rowDetail.PackageQuantity = sku.PackageQuantity;
            rowDetail.UnitPrice = UnitPrice;
            rowDetail.Quantity = Quantity;
            rowDetail.Amount = UnitPrice * Quantity;
            rowDetail.Remark = Remark;

        }

        public void AddFormReimburseSKUDetail(int? FormReimburseID, int SKUID, decimal UnitPrice, decimal Quantity, string Remark) {

            FormDS.FormReimburseSKUDetailRow rowDetail = this.FormDataSet.FormReimburseSKUDetail.NewFormReimburseSKUDetailRow();
            rowDetail.FormReimburseID = FormReimburseID.GetValueOrDefault();
            rowDetail.SKUID = SKUID;
            ERS.SKURow sku = new MasterDataBLL().GetSKUById(SKUID);
            rowDetail.PackageQuantity = sku.PackageQuantity;
            rowDetail.UnitPrice = UnitPrice;
            rowDetail.Quantity = Quantity;
            rowDetail.Amount = UnitPrice * Quantity;
            rowDetail.Remark = Remark;
            // 填加行并进行更新处理
            this.FormDataSet.FormReimburseSKUDetail.AddFormReimburseSKUDetailRow(rowDetail);

        }

        public void DeleteFormReimburseSKUDetailByID(int FormReimburseSKUDetailID) {
            for (int index = 0; index < this.FormDataSet.FormReimburseSKUDetail.Count; index++) {
                if ((int)this.FormDataSet.FormReimburseSKUDetail.Rows[index]["FormReimburseSKUDetailID"] == FormReimburseSKUDetailID) {
                    this.FormDataSet.FormReimburseSKUDetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region FormReimburse

        public void AddFormReimburseMoney(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID,
                SystemEnums.FormStatus StatusID, int CustomerID, int PaymentTypeID, string AttachedFileName, string RealAttachedFileName, string Remark, string FormApplyIds, string FormApplyNos, string FlowTemplate) {

            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormReimburseDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormReimburseInvoice, transaction);
                TableAdapterHelper.SetTransaction(this.TAPKRecord, transaction);

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
                formRow.PageType = (int)SystemEnums.PageType.ReimburseMoneyApply;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);
                //处理PKRecord,如果提交才需要锁定
                if (PaymentTypeID == (int)SystemEnums.PaymentType.PiaoKou && StatusID == SystemEnums.FormStatus.Awaiting) {
                    TAPKRecord.LockPKRecord(formRow.FormID, FormApplyNos);
                }
                //处理申请表的内容
                FormDS.FormReimburseRow FormReimburseRow = this.FormDataSet.FormReimburse.NewFormReimburseRow();
                FormReimburseRow.FormReimburseID = formRow.FormID;
                FormReimburseRow.CustomerID = CustomerID;
                FormReimburseRow.PaymentTypeID = PaymentTypeID;
                FormReimburseRow.Amount = 0;//默认值
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    FormReimburseRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    FormReimburseRow.RealAttachedFileName = RealAttachedFileName;
                }
                if (Remark != null && Remark != string.Empty) {
                    FormReimburseRow.Remark = Remark;
                }
                FormReimburseRow.FormApplyIds = FormApplyIds;
                FormReimburseRow.FormApplyNos = FormApplyNos;
                FormReimburseRow.IsDeliveryComplete = false;
                this.FormDataSet.FormReimburse.AddFormReimburseRow(FormReimburseRow);
                this.TAFormReimburse.Update(FormReimburseRow);

                //发票
                if (RejectedFormID != null) {
                    FormDS.FormReimburseInvoiceDataTable newInvoiceTable = new FormDS.FormReimburseInvoiceDataTable();
                    foreach (FormDS.FormReimburseInvoiceRow invoiceRow in this.FormDataSet.FormReimburseInvoice) {
                        if (invoiceRow.RowState != DataRowState.Deleted) {
                            FormDS.FormReimburseInvoiceRow newInvoiceRow = newInvoiceTable.NewFormReimburseInvoiceRow();
                            newInvoiceRow.FormReimburseID = FormReimburseRow.FormReimburseID;
                            newInvoiceRow.InvoiceNo = invoiceRow.InvoiceNo;
                            newInvoiceRow.InvoiceAmount = invoiceRow.InvoiceAmount;
                            newInvoiceRow.Remark = invoiceRow.IsRemarkNull() ? "" : invoiceRow.Remark;
                            newInvoiceRow.SystemInfo = invoiceRow.IsSystemInfoNull() ? "" : invoiceRow.SystemInfo;
                            newInvoiceTable.AddFormReimburseInvoiceRow(newInvoiceRow);
                        }
                        this.TAFormReimburseInvoice.Update(newInvoiceTable);
                    }
                } else {
                    foreach (FormDS.FormReimburseInvoiceRow invoiceRow in this.FormDataSet.FormReimburseInvoice) {
                        // 与父表绑定
                        if (invoiceRow.RowState != DataRowState.Deleted) {
                            invoiceRow.FormReimburseID = FormReimburseRow.FormReimburseID;
                        }
                    }
                }
                this.TAFormReimburseInvoice.Update(this.FormDataSet.FormReimburseInvoice);

                //明细表
                decimal totalAmount = 0;//计算总申请金额
                decimal totalTaxAmount = 0;//总税金
                FormDS.FormReimburseDetailDataTable newDetailTable = new FormDS.FormReimburseDetailDataTable();
                foreach (FormDS.FormReimburseDetailRow detailRow in this.FormDataSet.FormReimburseDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmount += detailRow.Amount;
                        totalTaxAmount += detailRow.IsTaxAmountNull() ? 0 : detailRow.TaxAmount;
                        FormDS.FormReimburseDetailRow newDetailRow = newDetailTable.NewFormReimburseDetailRow();
                        newDetailRow.FormReimburseID = FormReimburseRow.FormReimburseID;
                        newDetailRow.FormApplyExpenseDetailID = detailRow.FormApplyExpenseDetailID;
                        newDetailRow.ApplyFormNo = detailRow.ApplyFormNo;
                        newDetailRow.ApplyPeriod = detailRow.ApplyPeriod;
                        newDetailRow.ShopID = detailRow.ShopID;
                        newDetailRow.SKUID = detailRow.SKUID;
                        newDetailRow.ExpenseItemID = detailRow.ExpenseItemID;
                        newDetailRow.ApplyAmount = detailRow.ApplyAmount;
                        newDetailRow.RemainAmount = detailRow.RemainAmount;
                        newDetailRow.Amount = detailRow.Amount;
                        newDetailRow.TaxAmount = detailRow.IsTaxAmountNull() ? 0 : detailRow.TaxAmount;
                        newDetailRow.PrePaidAmount = detailRow.IsPrePaidAmountNull() ? 0 : detailRow.PrePaidAmount;
                        newDetailRow.ApplyPaymentTypeID = detailRow.ApplyPaymentTypeID;
                        newDetailRow.FormApplyID = detailRow.FormApplyID;
                        newDetailRow.AccruedAmount = detailRow.IsAccruedAmountNull() ? 0 : detailRow.AccruedAmount;
                        newDetailTable.AddFormReimburseDetailRow(newDetailRow);
                    }
                }
                this.TAFormReimburseDetail.Update(newDetailTable);
                FormReimburseRow.Amount = totalAmount;
                FormReimburseRow.TaxAmount = totalTaxAmount;
                this.TAFormReimburse.Update(FormReimburseRow);

                //作废之前的单据
                if (RejectedFormID != null) {
                    FormDS.FormRow oldRow = this.TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        oldRow.StatusID = (int)SystemEnums.FormStatus.Scrap;
                        this.TAForm.Update(oldRow);
                    }
                }

                // 正式提交或草稿
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["Apply_Amount"] = totalAmount + totalTaxAmount;
                    //dic["Dept"] = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID)[0].OrganizationUnitCode;
                    dic["Expense_Category"] = getExpenseCategoryIDByFormID(int.Parse(FormApplyIds.Split(',')[0])).ToString();//此处待改动
                    APHelper AP = new APHelper();
                    new APFlowBLL().ApplyForm(AP, TAForm, null, formRow, formRow.OrganizationUnitID, FlowTemplate, StatusID, dic);
                }
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormReimburseMoney(int FormID, SystemEnums.FormStatus StatusID, SystemEnums.FormType FormTypeID,
            string AttachedFileName, string RealAttachedFileName, string Remark, string FlowTemplate) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormReimburseDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormReimburseInvoice, transaction);
                TableAdapterHelper.SetTransaction(this.TAPKRecord, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormReimburseRow FormReimburseRow = this.TAFormReimburse.GetDataByID(FormID)[0];

                //处理单据的内容
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString((int)FormTypeID);
                    formRow.FormNo = utility.GetFormNo(formTypeString);
                } else {
                    formRow.SetFormNoNull();
                }
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;

                this.TAForm.Update(formRow);

                //处理FormReimburseRow的内容
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    FormReimburseRow.AttachedFileName = AttachedFileName;
                } else {
                    FormReimburseRow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    FormReimburseRow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    FormReimburseRow.SetRealAttachedFileNameNull();
                }
                if (Remark != null && Remark != string.Empty) {
                    FormReimburseRow.Remark = Remark;
                }

                this.TAFormReimburse.Update(FormReimburseRow);

                //发票
                foreach (FormDS.FormReimburseInvoiceRow invoiceRow in this.FormDataSet.FormReimburseInvoice) {
                    // 与父表绑定
                    if (invoiceRow.RowState != DataRowState.Deleted) {
                        invoiceRow.FormReimburseID = FormReimburseRow.FormReimburseID;
                    }
                }
                this.TAFormReimburseInvoice.Update(this.FormDataSet.FormReimburseInvoice);


                //处理明细
                decimal totalAmount = 0;
                decimal totalTaxAmount = 0;//总税金

                foreach (FormDS.FormReimburseDetailRow detailRow in this.FormDataSet.FormReimburseDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmount += detailRow.Amount;
                        totalTaxAmount += detailRow.IsTaxAmountNull() ? 0 : detailRow.TaxAmount;
                        detailRow.FormReimburseID = FormReimburseRow.FormReimburseID;
                    }
                }
                this.TAFormReimburseDetail.Update(this.FormDataSet.FormReimburseDetail);

                FormReimburseRow.Amount = totalAmount;
                FormReimburseRow.TaxAmount = totalTaxAmount;
                this.TAFormReimburse.Update(FormReimburseRow);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["Apply_Amount"] = totalAmount + totalTaxAmount;
                    //dic["Dept"] = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID)[0].OrganizationUnitCode;
                    dic["Expense_Category"] = getExpenseCategoryIDByFormID(int.Parse(FormReimburseRow.FormApplyIds.Split(',')[0])).ToString();//此处待改动
                    APHelper AP = new APHelper();
                    new APFlowBLL().ApplyForm(AP, TAForm, null, formRow, formRow.OrganizationUnitID, FlowTemplate, StatusID, dic);
                }
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }


        public void AddFormReimburseGoods(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID,
                SystemEnums.FormStatus StatusID, int CustomerID, int PaymentTypeID, string AttachedFileName, string RealAttachedFileName, string Remark, string FormApplyIds, string FormApplyNos, string FlowTemplate) {

            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormReimburseDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormReimburseSKUDetail, transaction);

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
                formRow.PageType = (int)SystemEnums.PageType.ReimburseGoodsApply;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormReimburseRow FormReimburseRow = this.FormDataSet.FormReimburse.NewFormReimburseRow();
                FormReimburseRow.FormReimburseID = formRow.FormID;
                FormReimburseRow.CustomerID = CustomerID;
                FormReimburseRow.PaymentTypeID = PaymentTypeID;
                FormReimburseRow.Amount = 0;//默认值
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    FormReimburseRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    FormReimburseRow.RealAttachedFileName = RealAttachedFileName;
                }
                if (Remark != null && Remark != string.Empty) {
                    FormReimburseRow.Remark = Remark;
                }
                FormReimburseRow.FormApplyIds = FormApplyIds;
                FormReimburseRow.FormApplyNos = FormApplyNos;
                FormReimburseRow.IsDeliveryComplete = false;

                this.FormDataSet.FormReimburse.AddFormReimburseRow(FormReimburseRow);
                this.TAFormReimburse.Update(FormReimburseRow);


                //处理free goods明细
                if (RejectedFormID != null) {
                    FormDS.FormReimburseSKUDetailDataTable newFGTable = new FormDS.FormReimburseSKUDetailDataTable();
                    foreach (FormDS.FormReimburseSKUDetailRow detailFGRow in this.FormDataSet.FormReimburseSKUDetail) {
                        // 与父表绑定
                        if (detailFGRow.RowState != DataRowState.Deleted) {
                            FormDS.FormReimburseSKUDetailRow newDetailFGRow = newFGTable.NewFormReimburseSKUDetailRow();
                            newDetailFGRow.FormReimburseID = FormReimburseRow.FormReimburseID;
                            newDetailFGRow.SKUID = detailFGRow.SKUID;
                            newDetailFGRow.PackageQuantity = detailFGRow.PackageQuantity;
                            newDetailFGRow.UnitPrice = detailFGRow.UnitPrice;
                            newDetailFGRow.Quantity = detailFGRow.Quantity;
                            newDetailFGRow.Amount = detailFGRow.Amount;

                            if (!detailFGRow.IsRemarkNull()) {
                                newDetailFGRow.Remark = detailFGRow.Remark;
                            }
                            newFGTable.AddFormReimburseSKUDetailRow(newDetailFGRow);
                        }
                    }
                    this.TAFormReimburseSKUDetail.Update(newFGTable);
                } else {
                    foreach (FormDS.FormReimburseSKUDetailRow detailFGRow in this.FormDataSet.FormReimburseSKUDetail) {
                        // 与父表绑定
                        if (detailFGRow.RowState != DataRowState.Deleted) {
                            detailFGRow.FormReimburseID = FormReimburseRow.FormReimburseID;
                        }
                    }
                    this.TAFormReimburseSKUDetail.Update(this.FormDataSet.FormReimburseSKUDetail);
                }

                //明细表
                decimal totalAmount = 0;//计算总申请金额
                FormDS.FormReimburseDetailDataTable newDetailTable = new FormDS.FormReimburseDetailDataTable();
                foreach (FormDS.FormReimburseDetailRow detailRow in this.FormDataSet.FormReimburseDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmount += detailRow.Amount;

                        FormDS.FormReimburseDetailRow newDetailRow = newDetailTable.NewFormReimburseDetailRow();
                        newDetailRow.FormReimburseID = FormReimburseRow.FormReimburseID;
                        newDetailRow.FormApplyExpenseDetailID = detailRow.FormApplyExpenseDetailID;
                        newDetailRow.ApplyFormNo = detailRow.ApplyFormNo;
                        newDetailRow.ApplyPeriod = detailRow.ApplyPeriod;
                        newDetailRow.ShopID = detailRow.ShopID;
                        newDetailRow.SKUID = detailRow.SKUID;
                        newDetailRow.ExpenseItemID = detailRow.ExpenseItemID;
                        newDetailRow.ApplyAmount = detailRow.ApplyAmount;
                        newDetailRow.RemainAmount = detailRow.RemainAmount;
                        newDetailRow.Amount = detailRow.Amount;
                        newDetailRow.ApplyPaymentTypeID = detailRow.ApplyPaymentTypeID;
                        newDetailRow.FormApplyID = detailRow.FormApplyID;
                        newDetailRow.AccruedAmount = detailRow.IsAccruedAmountNull() ? 0 : detailRow.AccruedAmount;
                        newDetailTable.AddFormReimburseDetailRow(newDetailRow);

                    }
                }
                this.TAFormReimburseDetail.Update(newDetailTable);
                FormReimburseRow.Amount = totalAmount;
                this.TAFormReimburse.Update(FormReimburseRow);

                //作废之前的单据
                if (RejectedFormID != null) {
                    FormDS.FormRow oldRow = this.TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        oldRow.StatusID = (int)SystemEnums.FormStatus.Scrap;
                        this.TAForm.Update(oldRow);
                    }
                }

                // 正式提交或草稿
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["Apply_Amount"] = totalAmount;
                    //dic["Dept"] = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID)[0].OrganizationUnitCode;
                    dic["Expense_Category"] = getExpenseCategoryIDByFormID(int.Parse(FormApplyIds.Split(',')[0])).ToString();//此处待改动
                    APHelper AP = new APHelper();
                    new APFlowBLL().ApplyForm(AP, TAForm, RejectedFormID, formRow, OrganizationUnitID, FlowTemplate, StatusID, dic);
                }

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormReimburseGoods(int FormID, SystemEnums.FormStatus StatusID, SystemEnums.FormType FormTypeID,
            string AttachedFileName, string RealAttachedFileName, string Remark, string FlowTemplate) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormReimburseDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormReimburseSKUDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormReimburseRow FormReimburseRow = this.TAFormReimburse.GetDataByID(FormID)[0];

                //处理单据的内容
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString((int)FormTypeID);
                    formRow.FormNo = utility.GetFormNo(formTypeString);
                } else {
                    formRow.SetFormNoNull();
                }
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;

                this.TAForm.Update(formRow);

                //处理FormReimburseRow的内容
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    FormReimburseRow.AttachedFileName = AttachedFileName;
                } else {
                    FormReimburseRow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    FormReimburseRow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    FormReimburseRow.SetRealAttachedFileNameNull();
                }
                if (Remark != null && Remark != string.Empty) {
                    FormReimburseRow.Remark = Remark;
                }

                this.TAFormReimburse.Update(FormReimburseRow);

                //处理free goods明细               
                foreach (FormDS.FormReimburseSKUDetailRow detailFGRow in this.FormDataSet.FormReimburseSKUDetail) {
                    // 与父表绑定
                    if (detailFGRow.RowState != DataRowState.Deleted) {
                        detailFGRow.FormReimburseID = FormReimburseRow.FormReimburseID;
                    }
                }
                this.TAFormReimburseSKUDetail.Update(this.FormDataSet.FormReimburseSKUDetail);

                //处理明细
                decimal totalAmount = 0;
                foreach (FormDS.FormReimburseDetailRow detailRow in this.FormDataSet.FormReimburseDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmount += detailRow.Amount;
                        detailRow.FormReimburseID = FormReimburseRow.FormReimburseID;
                    }
                }
                this.TAFormReimburseDetail.Update(this.FormDataSet.FormReimburseDetail);

                FormReimburseRow.Amount = totalAmount;
                this.TAFormReimburse.Update(FormReimburseRow);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["Apply_Amount"] = totalAmount;
                    //dic["Dept"] = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID)[0].OrganizationUnitCode;
                    dic["Expense_Category"] = getExpenseCategoryIDByFormID(int.Parse(FormReimburseRow.FormApplyIds.Split(',')[0])).ToString();//此处待改动
                    APHelper AP = new APHelper();
                    new APFlowBLL().ApplyForm(AP, TAForm, null, formRow, formRow.OrganizationUnitID, FlowTemplate, StatusID, dic);
                }
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void DeleteFormReimburse(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormReimburseDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormReimburseInvoice, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormReimburseSKUDetail, transaction);

                this.TAFormReimburseDetail.DeleteByFormReimburseID(FormID);
                this.TAFormReimburseSKUDetail.DeleteByFormReimburseID(FormID);
                this.TAFormReimburseInvoice.DeleteByFormReimburseID(FormID);
                this.TAFormReimburse.DeleteByID(FormID);
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

        public void UpdateFormReimburseForRealPaymentInfo(int FormReimburseID, DateTime PaymentDate) {
            FormDS.FormReimburseRow reimburseRow = this.TAFormReimburse.GetDataByID(FormReimburseID)[0];
            reimburseRow.PaymentDate = PaymentDate;
            reimburseRow.PaymentAmount = reimburseRow.Amount;
            this.TAFormReimburse.Update(reimburseRow);
        }


        #endregion

        #region FormReimburseDelivery

        public void UpdateFormReimburseDelivery(int FormReimburseDeliveryID, string DeliveryNo, decimal DeliveryQuantity, DateTime DeliveryDate, string Remark) {
            FormDS.FormReimburseDeliveryRow rowDetail = this.TAFormReimburseDelivery.GetDataByID(FormReimburseDeliveryID)[0];
            if (rowDetail == null)
                return;
            rowDetail.DeliveryNo = DeliveryNo;
            rowDetail.DeliveryQuantity = DeliveryQuantity;
            rowDetail.DeliveryDate = DeliveryDate;
            FormDS.FormReimburseSKUDetailRow skuDetailRow = this.TAFormReimburseSKUDetail.GetDataByID(rowDetail.FormReimburseSKUDetailID)[0];
            ERS.SKURow sku = new MasterDataBLL().GetSKUById(skuDetailRow.SKUID);
            rowDetail.DeliveryAmount = skuDetailRow.UnitPrice * DeliveryQuantity;
            rowDetail.DeliveryCost = sku.CostPrice * DeliveryQuantity;
            rowDetail.Remark = Remark;
            this.TAFormReimburseDelivery.Update(rowDetail);
        }

        public void AddFormReimburseDelivery(int FormReimburseSKUDetailID, string DeliveryNo, decimal DeliveryQuantity, DateTime DeliveryDate, string Remark) {

            FormDS.FormReimburseDeliveryDataTable table = new FormDS.FormReimburseDeliveryDataTable();
            FormDS.FormReimburseDeliveryRow rowDetail = table.NewFormReimburseDeliveryRow();
            rowDetail.FormReimburseSKUDetailID = FormReimburseSKUDetailID;
            rowDetail.DeliveryNo = DeliveryNo;
            rowDetail.DeliveryQuantity = DeliveryQuantity;
            rowDetail.DeliveryDate = DeliveryDate;
            FormDS.FormReimburseSKUDetailRow skuDetailRow = this.TAFormReimburseSKUDetail.GetDataByID(rowDetail.FormReimburseSKUDetailID)[0];
            ERS.SKURow sku = new MasterDataBLL().GetSKUById(skuDetailRow.SKUID);
            rowDetail.DeliveryAmount = skuDetailRow.UnitPrice * DeliveryQuantity;
            rowDetail.DeliveryCost = sku.CostPrice * DeliveryQuantity;
            rowDetail.Remark = Remark;
            table.AddFormReimburseDeliveryRow(rowDetail);
            this.TAFormReimburseDelivery.Update(table);
        }

        public void DeleteFormReimburseDeliveryByID(int FormReimburseDeliveryID) {
            this.TAFormReimburseDelivery.DeleteByID(FormReimburseDeliveryID);
        }

        public void DeliveryComplete(int FormReimburseID, int UserID) {
            FormDS.FormReimburseRow reimburseRow = this.TAFormReimburse.GetDataByID(FormReimburseID)[0];

            reimburseRow.IsDeliveryComplete = true;
            reimburseRow.DeliveryCompleteDate = DateTime.Now;
            reimburseRow.DeliveryCompleteUserID = UserID;

            this.TAFormReimburse.Update(reimburseRow);
        }


        #endregion

    }
}
