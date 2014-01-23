using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.FormDSTableAdapters;
using System.Web.Security;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using lib.wf;

namespace BusinessObjects {
    public class PersonalReimburseApplyBLL {
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

        private FormPersonalReimburseTableAdapter m_FormPersonalReimburseAdapter;
        public FormPersonalReimburseTableAdapter TAFormPersonalReimburse {
            get {
                if (this.m_FormPersonalReimburseAdapter == null) {
                    this.m_FormPersonalReimburseAdapter = new FormPersonalReimburseTableAdapter();
                }
                return this.m_FormPersonalReimburseAdapter;
            }
        }

        private FormPersonalReimburseDetailTableAdapter m_FormPersonalReimburseDetailAdapter;
        public FormPersonalReimburseDetailTableAdapter TAFormPersonalReimburseDetail {
            get {
                if (this.m_FormPersonalReimburseDetailAdapter == null) {
                    this.m_FormPersonalReimburseDetailAdapter = new FormPersonalReimburseDetailTableAdapter();
                }
                return this.m_FormPersonalReimburseDetailAdapter;
            }
        }

        #endregion

        #region 获取数据

        public FormDS.FormDataTable GetFormByID(int FormID) {
            return this.TAForm.GetDataByID(FormID);
        }

        public FormDS.FormPersonalReimburseDataTable GetFormPersonalReimburseByID(int FormPersonalReimburseID) {
            return this.TAFormPersonalReimburse.GetDataByID(FormPersonalReimburseID);
        }

        public FormDS.FormPersonalReimburseDetailDataTable GetFormPersonalReimburseDetailByFormPersonalReimburseID(int FormPersonalReimburselID) {
            return this.TAFormPersonalReimburseDetail.GetDataByFormID(FormPersonalReimburselID);
        }

        public FormDS.FormPersonalReimburseDetailDataTable GetFormPersonalReimburseDetail() {
            return this.FormDataSet.FormPersonalReimburseDetail;
        }

        #endregion

        #region PersonalReimburseDetail

        public void AddFormPersonalReimburseDetail(AuthorizationDS.StuffUserRow User, int? FormPersonalReimburseID, DateTime? OccurDate, int ExpenseManageTypeID, decimal Amount, string Remark) {
            switch (ExpenseManageTypeID) {
                case 1:
                    if (Amount > User.TrafficFeeLimit) {
                        throw new ApplicationException("交通费用超过个人交通费用报销限制！");
                    }
                    break;
                case 2:
                    if (Amount > User.TelephoneFeeLimit) {
                        throw new ApplicationException("电话费用超过个人电话费用报销限制！");
                    }
                    break;
            }
            FormDS.FormPersonalReimburseDetailRow rowDetail = this.FormDataSet.FormPersonalReimburseDetail.NewFormPersonalReimburseDetailRow();
            rowDetail.FormPersonalReimburseID = FormPersonalReimburseID.GetValueOrDefault();
            rowDetail.OccurDate = OccurDate.GetValueOrDefault();
            rowDetail.ExpenseManageTypeID = ExpenseManageTypeID;
            rowDetail.Amount = Amount;
            rowDetail.RealAmount = Amount;
            rowDetail.Remark = Remark;
            // 填加行并进行更新处理
            this.FormDataSet.FormPersonalReimburseDetail.AddFormPersonalReimburseDetailRow(rowDetail);
        }

        public void UpdateFormPersonalReimburseDetail(AuthorizationDS.StuffUserRow User, int RowIndex, int? FormPersonalReimburseID, DateTime? OccurDate, int ExpenseManageTypeID, decimal Amount, string Remark, int FormPersonalReimburseDetailID) {
            switch (ExpenseManageTypeID) {
                case 1:
                    if (Amount > User.TrafficFeeLimit) {
                        throw new ApplicationException("交通费用超过个人交通费用报销限制！");
                    }
                    break;
                case 2:
                    if (Amount > User.TelephoneFeeLimit) {
                        throw new ApplicationException("电话费用超过个人电话费用报销限制！");
                    }
                    break;
            }
            FormDS.FormPersonalReimburseDetailRow rowDetail = (FormDS.FormPersonalReimburseDetailRow)this.FormDataSet.FormPersonalReimburseDetail.Rows[RowIndex];
            rowDetail.FormPersonalReimburseID = FormPersonalReimburseID.GetValueOrDefault();
            rowDetail.OccurDate = OccurDate.GetValueOrDefault();
            rowDetail.ExpenseManageTypeID = ExpenseManageTypeID;
            rowDetail.Amount = Amount;
            rowDetail.RealAmount = Amount;
            rowDetail.Remark = Remark;

            // 填加行并进行更新处理
        }

        public void DeleteFormPersonalReimburseDetailByID(int FormPersonalReimburseDetailID) {
            for (int index = 0; index < this.FormDataSet.FormPersonalReimburseDetail.Count; index++) {
                if ((int)this.FormDataSet.FormPersonalReimburseDetail.Rows[index]["FormPersonalReimburseDetailID"] == FormPersonalReimburseDetailID) {
                    this.FormDataSet.FormPersonalReimburseDetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region FormPersonalReimburse

        public void AddFormPersonalReimburse(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID,
                SystemEnums.FormStatus StatusID, DateTime? Period, string Remark, string AttachedFileName, string RealAttachedFileName, string FlowTemplate) {

            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburseDetail, transaction);

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
                formRow.PageType = (int)SystemEnums.PageType.PersonalReimburseApply;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormPersonalReimburseRow formPersonalReimburseRow = this.FormDataSet.FormPersonalReimburse.NewFormPersonalReimburseRow();
                formPersonalReimburseRow.FormPersonalReimburseID = formRow.FormID;
                formPersonalReimburseRow.Period = Period.GetValueOrDefault();
                formPersonalReimburseRow.Amount = decimal.Zero;
                formPersonalReimburseRow.Remark = Remark;

                decimal[] calculateAssistant = this.GetPersonalBudgetByParameter(PositionID, Period);
                formPersonalReimburseRow.TotalBudget = calculateAssistant[0];
                formPersonalReimburseRow.ApprovedAmount = calculateAssistant[1];
                formPersonalReimburseRow.ApprovingAmount = calculateAssistant[2];
                formPersonalReimburseRow.RemainAmount = calculateAssistant[3];
                if (AttachedFileName != null) {
                    formPersonalReimburseRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null) {
                    formPersonalReimburseRow.RealAttachedFileName = RealAttachedFileName;
                }

                this.FormDataSet.FormPersonalReimburse.AddFormPersonalReimburseRow(formPersonalReimburseRow);
                this.TAFormPersonalReimburse.Update(formPersonalReimburseRow);

                //明细表
                decimal totalAmount = 0;//计算总申请金额

                if (RejectedFormID != null) {
                    FormDS.FormPersonalReimburseDetailDataTable newDetailTable = new FormDS.FormPersonalReimburseDetailDataTable();
                    foreach (FormDS.FormPersonalReimburseDetailRow detailRow in this.FormDataSet.FormPersonalReimburseDetail) {
                        if (detailRow.RowState != System.Data.DataRowState.Deleted) {
                            FormDS.FormPersonalReimburseDetailRow newDetailRow = newDetailTable.NewFormPersonalReimburseDetailRow();
                            newDetailRow.FormPersonalReimburseID = formPersonalReimburseRow.FormPersonalReimburseID;
                            newDetailRow.OccurDate = detailRow.OccurDate;
                            newDetailRow.ExpenseManageTypeID = detailRow.ExpenseManageTypeID;
                            newDetailRow.Amount = detailRow.Amount;
                            newDetailRow.RealAmount = detailRow.RealAmount;
                            if (!detailRow.IsRemarkNull()) {
                                newDetailRow.Remark = detailRow.Remark;
                            }
                            totalAmount += newDetailRow.Amount;
                            newDetailTable.AddFormPersonalReimburseDetailRow(newDetailRow);
                        }
                    }
                    this.TAFormPersonalReimburseDetail.Update(newDetailTable);
                } else {
                    foreach (FormDS.FormPersonalReimburseDetailRow detailRow in this.FormDataSet.FormPersonalReimburseDetail) {
                        // 与父表绑定
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormPersonalReimburseID = formPersonalReimburseRow.FormPersonalReimburseID;
                            totalAmount += detailRow.Amount;
                        }
                    }
                    this.TAFormPersonalReimburseDetail.Update(this.FormDataSet.FormPersonalReimburseDetail);
                }

                formPersonalReimburseRow.Amount = totalAmount;
                this.TAFormPersonalReimburse.Update(formPersonalReimburseRow);

                // 正式提交或草稿
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    //如果申请总额大于可用余额，不能提交
                    if (formPersonalReimburseRow.Amount > calculateAssistant[3]) {//如果是减少预算,要做检查
                        throw new ApplicationException("申请报销总额超出部门可用余额，不能提交！");
                    }

                    dic["Apply_Amount"] = totalAmount;//金额
                    AuthorizationDS.OrganizationUnitDataTable OUTable = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID);
                    if (OUTable.Count == 0) {
                        throw new ApplicationException("没有找到您所在部门的流程，请联系管理员");
                    }
                    dic["Department"] = OUTable[0].OrganizationUnitCode;
                    APHelper AP = new APHelper();
                    new APFlowBLL().ApplyForm(AP, TAForm, RejectedFormID, formRow, OrganizationUnitID, FlowTemplate, StatusID, dic);
                }
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormPersonalReimburse(int FormID, SystemEnums.FormStatus StatusID, SystemEnums.FormType FormTypeID, DateTime? Period, string Remark,
                string AttachedFileName, string RealAttachedFileName, string FlowTemplate) {
            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburseDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];

                decimal[] calculateAssistant = this.GetPersonalBudgetByParameter(formRow.PositionID, Period);
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
                FormDS.FormPersonalReimburseRow formPersonalReimburseRow = this.TAFormPersonalReimburse.GetDataByID(FormID)[0];
                formPersonalReimburseRow.FormPersonalReimburseID = formRow.FormID;
                formPersonalReimburseRow.Period = Period.GetValueOrDefault();
                formPersonalReimburseRow.Amount = decimal.Zero;
                formPersonalReimburseRow.Remark = Remark;

                formPersonalReimburseRow.TotalBudget = calculateAssistant[0];
                formPersonalReimburseRow.ApprovedAmount = calculateAssistant[1];
                formPersonalReimburseRow.ApprovingAmount = calculateAssistant[2];
                formPersonalReimburseRow.RemainAmount = calculateAssistant[3];

                if (AttachedFileName != null) {
                    formPersonalReimburseRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null) {
                    formPersonalReimburseRow.RealAttachedFileName = RealAttachedFileName;
                }

                this.TAFormPersonalReimburse.Update(formPersonalReimburseRow);

                //明细表
                decimal totalAmount = 0;//计算总申请金额
                foreach (FormDS.FormPersonalReimburseDetailRow detailRow in this.FormDataSet.FormPersonalReimburseDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormPersonalReimburseID = formPersonalReimburseRow.FormPersonalReimburseID;
                        totalAmount += detailRow.Amount;
                    }
                }
                this.TAFormPersonalReimburseDetail.Update(this.FormDataSet.FormPersonalReimburseDetail);

                formPersonalReimburseRow.Amount = totalAmount;
                this.TAFormPersonalReimburse.Update(formPersonalReimburseRow);

                // 正式提交或草稿
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    if (formPersonalReimburseRow.Amount > calculateAssistant[3]) {//如果是减少预算,要做检查
                        throw new ApplicationException("申请报销总额超出部门可用余额，不能提交！");
                    }
                    dic["Apply_Amount"] = totalAmount;//金额
                    AuthorizationDS.OrganizationUnitDataTable OUTable = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID);
                    if (OUTable.Count == 0) {
                        throw new ApplicationException("没有找到您所在部门的流程，请联系管理员");
                    }
                    dic["Department"] = OUTable[0].OrganizationUnitCode;
                    APHelper AP = new APHelper();
                    new APFlowBLL().ApplyForm(AP, TAForm, null, formRow, formRow.OrganizationUnitID, FlowTemplate, StatusID, dic);
                }
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void SaveRealAmountForPersonalReimburse(int FormID) {
            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburseDetail, transaction);

                //处理申请表的内容
                FormDS.FormPersonalReimburseRow formPersonalReimburseRow = this.TAFormPersonalReimburse.GetDataByID(FormID)[0];

                this.TAFormPersonalReimburse.Update(formPersonalReimburseRow);

                //明细表
                decimal totalRealAmount = 0;//计算总申请金额
                foreach (FormDS.FormPersonalReimburseDetailRow detailRow in this.FormDataSet.FormPersonalReimburseDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalRealAmount += detailRow.RealAmount;
                    }
                }
                this.TAFormPersonalReimburseDetail.Update(this.FormDataSet.FormPersonalReimburseDetail);

                formPersonalReimburseRow.Amount = totalRealAmount;
                this.TAFormPersonalReimburse.Update(formPersonalReimburseRow);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }


        public void DeleteFormPersonalReimburse(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburseDetail, transaction);
                this.TAFormPersonalReimburseDetail.DeleteByFormID(FormID);
                this.TAFormPersonalReimburse.DeleteByID(FormID);
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

        public decimal[] GetPersonalBudgetByOUID(int oranizationID, DateTime? Period) {

            decimal? TotalBudget = 0;
            decimal? ApprovedFee = 0;
            decimal? ApprovingFee = 0;
            decimal? RemainFee = 0;
            decimal[] calculateAssistant = new decimal[4];
            this.TAFormPersonalReimburse.GetPersonalBudgetByOUID(oranizationID, Period, ref TotalBudget, ref ApprovedFee, ref ApprovingFee, ref RemainFee);

            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedFee.GetValueOrDefault();
            calculateAssistant[2] = ApprovingFee.GetValueOrDefault();
            calculateAssistant[3] = RemainFee.GetValueOrDefault();

            return calculateAssistant;
        }

        public decimal[] GetPersonalBudgetByParameter(int positionID, DateTime? Period) {

            decimal? TotalBudget = 0;
            decimal? ApprovedFee = 0;
            decimal? ApprovingFee = 0;
            decimal? RemainFee = 0;
            decimal[] calculateAssistant = new decimal[4];
            this.TAFormPersonalReimburse.GetPersonalBudgetByParameter(positionID, Period, ref TotalBudget, ref ApprovedFee, ref ApprovingFee, ref RemainFee);

            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedFee.GetValueOrDefault();
            calculateAssistant[2] = ApprovingFee.GetValueOrDefault();
            calculateAssistant[3] = RemainFee.GetValueOrDefault();

            return calculateAssistant;
        }

        public void CopyPersonalReimburseForm(int FormPersonalReimburseID, DateTime Period) {
            this.TAFormPersonalReimburse.CopyPersonalReimburseForm(FormPersonalReimburseID, Period);
        }


    }
}
