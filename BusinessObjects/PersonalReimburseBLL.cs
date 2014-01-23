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
    public class PersonalReimburseBLL {
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

        private FormTravelApplyTableAdapter _TAFormTravelApply;
        public FormTravelApplyTableAdapter TAFormTravelApply {
            get {
                if (this._TAFormTravelApply == null) {
                    this._TAFormTravelApply = new FormTravelApplyTableAdapter();
                }
                return this._TAFormTravelApply;
            }
        }

        private FormTravelApplyDetailTableAdapter _TAFormTravelApplyDetail;
        public FormTravelApplyDetailTableAdapter TAFormTravelApplyDetail {
            get {
                if (this._TAFormTravelApplyDetail == null) {
                    this._TAFormTravelApplyDetail = new FormTravelApplyDetailTableAdapter();
                }
                return this._TAFormTravelApplyDetail;
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

        public FormDS.FormTravelApplyRow GetFormTravelApplyByID(int FormTravelApplyID) {
            return this.TAFormTravelApply.GetDataByID(FormTravelApplyID)[0];
        }

        public FormDS.FormTravelApplyDetailRow GetFormTravelApplyDetailByID(int FormTravelApplyDetailID) {
            return this.TAFormTravelApplyDetail.GetDataByID(FormTravelApplyDetailID)[0];
        }

        public FormDS.FormTravelApplyDetailDataTable GetFormTravelApplyDetailByFormTravelApplyID(int FormTravelApplyID) {
            return this.FormDataSet.FormTravelApplyDetail;
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

        public void UpdateFormPersonalReimburseDetail(int FormPersonalReimburseDetailID, AuthorizationDS.StuffUserRow User, int? FormPersonalReimburseID, DateTime? OccurDate, int ExpenseManageTypeID, decimal Amount, string Remark) {
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
            FormDS.FormPersonalReimburseDetailRow rowDetail = (FormDS.FormPersonalReimburseDetailRow)this.FormDataSet.FormPersonalReimburseDetail.FindByFormPersonalReimburseDetailID(FormPersonalReimburseDetailID);
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

        #region FormTravelApply Operate

        public void AddFormTravelApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID,
            int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID, decimal? TransportFee, decimal? HotelFee,
            decimal? MealFee, decimal? OtherFee, string Remark, string AttachmentFileName, string RealAttachmentFileName) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormTravelApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormTravelApplyDetail, transaction);

                FormDS.FormDataTable tbForm = new FormDS.FormDataTable();
                FormDS.FormRow formRow = tbForm.NewFormRow();
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
                formRow.PageType = (int)SystemEnums.PageType.TravelApply;
                tbForm.AddFormRow(formRow);
                this.TAForm.Update(tbForm);

                //处理申请表的内容
                FormDS.FormTravelApplyDataTable tbFormTravelApply = new FormDS.FormTravelApplyDataTable();
                FormDS.FormTravelApplyRow rowFormTravelApply = tbFormTravelApply.NewFormTravelApplyRow();
                decimal TotalFee = 0;

                rowFormTravelApply.FormTravelApplyID = formRow.FormID;
                if (TransportFee != null) {
                    rowFormTravelApply.TransportFee = TransportFee.GetValueOrDefault();
                    TotalFee += rowFormTravelApply.TransportFee;
                }
                if (HotelFee != null) {
                    rowFormTravelApply.HotelFee = HotelFee.GetValueOrDefault();
                    TotalFee += rowFormTravelApply.HotelFee;
                }
                if (MealFee != null) {
                    rowFormTravelApply.MealFee = MealFee.GetValueOrDefault();
                    TotalFee += rowFormTravelApply.MealFee;
                }
                if (OtherFee != null) {
                    rowFormTravelApply.OtherFee = OtherFee.GetValueOrDefault();
                    TotalFee += rowFormTravelApply.OtherFee;
                }
                rowFormTravelApply.TotalFee = TotalFee;
                if (Remark != null) {
                    rowFormTravelApply.Remark = Remark;
                }
                rowFormTravelApply.AttachedFileName = AttachmentFileName;
                rowFormTravelApply.RealAttachedFileName = RealAttachmentFileName;
                tbFormTravelApply.AddFormTravelApplyRow(rowFormTravelApply);
                this.TAFormTravelApply.Update(tbFormTravelApply);

                int TotalDays = 0;
                //处理明细数据
                if (RejectedFormID != null) {
                    FormDS.FormTravelApplyDetailDataTable newDetailTable = new FormDS.FormTravelApplyDetailDataTable();
                    foreach (FormDS.FormTravelApplyDetailRow detailRow in this.FormDataSet.FormTravelApplyDetail) {
                        if (detailRow.RowState != System.Data.DataRowState.Deleted) {
                            FormDS.FormTravelApplyDetailRow newDetailRow = newDetailTable.NewFormTravelApplyDetailRow();
                            newDetailRow.FormTravelApplyID = rowFormTravelApply.FormTravelApplyID;
                            newDetailRow.BeginDate = detailRow.BeginDate;
                            newDetailRow.EndDate = detailRow.EndDate;
                            newDetailRow.Departure = detailRow.Departure;
                            newDetailRow.Destination = detailRow.Destination;
                            newDetailRow.Vehicle = detailRow.Vehicle;
                            newDetailRow.Remark = detailRow.Remark;
                            newDetailRow.Days = (detailRow.EndDate - detailRow.BeginDate).Days + 1;
                            TotalDays += newDetailRow.Days;
                            newDetailTable.AddFormTravelApplyDetailRow(newDetailRow);
                        }
                    }
                    this.TAFormTravelApplyDetail.Update(newDetailTable);
                } else {
                    foreach (FormDS.FormTravelApplyDetailRow detailRow in this.FormDataSet.FormTravelApplyDetail) {
                        // 与父表绑定
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormTravelApplyID = rowFormTravelApply.FormTravelApplyID;
                            TotalDays += detailRow.Days;
                        }
                    }
                    this.TAFormTravelApplyDetail.Update(this.FormDataSet.FormTravelApplyDetail);
                }

                this.TAFormTravelApply.Update(rowFormTravelApply);

                //作废之前的单据
                if (RejectedFormID != null) {
                    FormDS.FormRow oldRow = TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        new APFlowBLL().ScrapForm(oldRow.FormID);
                    }
                }

                // 正式提交或草稿 流程模板带修改
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["Apply_Amount"] = rowFormTravelApply.TotalFee;//金额
                AuthorizationDS.OrganizationUnitDataTable OUTable = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID);
                if (OUTable.Count == 0) {
                    throw new ApplicationException("没有找到您所在部门的流程，请联系管理员");
                }
                dic["Days"] = TotalDays;
                dic["Department"] = OUTable[0].OrganizationUnitCode;
                APHelper AP = new APHelper();
                new APFlowBLL().ApplyForm(AP, TAForm, RejectedFormID, formRow, OrganizationUnitID, new AuthorizationBLL().GetFlowTemplate((int)SystemEnums.BusinessUseCase.FormTravelApply, formRow.UserID), StatusID, dic);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormTravelApply(int FormID, SystemEnums.FormStatus StatusID, decimal? TransportFee, decimal? HotelFee,
            decimal? MealFee, decimal? OtherFee, string Remark, string AttachmentFileName, string RealAttachmentFileName) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormTravelApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormTravelApplyDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormTravelApplyRow FormTravelApplyApplyRow = this.TAFormTravelApply.GetDataByID(FormID)[0];

                //处理单据内容
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString(formRow.FormTypeID);
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

                decimal TotalAmount = 0;
                //处理申请表的内容
                if (TransportFee != null) {
                    FormTravelApplyApplyRow.TransportFee = TransportFee.GetValueOrDefault();
                    TotalAmount += FormTravelApplyApplyRow.TransportFee;
                }
                if (HotelFee != null) {
                    FormTravelApplyApplyRow.HotelFee = HotelFee.GetValueOrDefault();
                    TotalAmount += FormTravelApplyApplyRow.HotelFee;
                }
                if (MealFee != null) {
                    FormTravelApplyApplyRow.MealFee = MealFee.GetValueOrDefault();
                    TotalAmount += FormTravelApplyApplyRow.MealFee;
                }
                if (OtherFee != null) {
                    FormTravelApplyApplyRow.OtherFee = OtherFee.GetValueOrDefault();
                    TotalAmount += FormTravelApplyApplyRow.OtherFee;
                }
                FormTravelApplyApplyRow.Remark = Remark;
                FormTravelApplyApplyRow.TotalFee = TotalAmount;
                FormTravelApplyApplyRow.AttachedFileName = AttachmentFileName;
                FormTravelApplyApplyRow.RealAttachedFileName = RealAttachmentFileName;

                this.TAFormTravelApply.Update(FormTravelApplyApplyRow);

                //处理明细数据
                int TotalDays = 0;
                FormDS.FormTravelApplyDetailDataTable newDetailTable = new FormDS.FormTravelApplyDetailDataTable();
                foreach (FormDS.FormTravelApplyDetailRow detailRow in this.FormDataSet.FormTravelApplyDetail) {
                    if (detailRow.RowState != System.Data.DataRowState.Deleted) {
                        detailRow.FormTravelApplyID = FormTravelApplyApplyRow.FormTravelApplyID;
                        TotalDays += detailRow.Days;
                    }
                }
                FormTravelApplyApplyRow.Days = TotalDays;

                this.TAFormTravelApplyDetail.Update(this.FormDataSet.FormTravelApplyDetail);
                this.TAFormTravelApply.Update(FormTravelApplyApplyRow);

                // 正式提交或草稿 流程模板带修改
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["Apply_Amount"] = FormTravelApplyApplyRow.TotalFee;//金额
                dic["Days"] = TotalDays;
                AuthorizationDS.OrganizationUnitDataTable OUTable = new AuthorizationDSTableAdapters.OrganizationUnitTableAdapter().GetOrganizationUnitCodeByOrganizationUnitID(formRow.OrganizationUnitID);
                if (OUTable.Count == 0) {
                    throw new ApplicationException("没有找到您所在部门的流程，请联系管理员");
                }
                dic["Department"] = OUTable[0].OrganizationUnitCode;
                APHelper AP = new APHelper();
                new APFlowBLL().ApplyForm(AP, TAForm, null, formRow, formRow.OrganizationUnitID, new AuthorizationBLL().GetFlowTemplate((int)SystemEnums.BusinessUseCase.FormTravelApply, formRow.UserID), StatusID, dic);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void DeleteFormTravelApplyByID(int FormTravelApplyID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormTravelApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormTravelApplyDetail, transaction);
                FormDS.FormTravelApplyRow ftar = TAFormTravelApply.GetDataByID(FormTravelApplyID)[0];
                TAFormTravelApplyDetail.DeleteByApplyID(FormTravelApplyID);
                TAFormTravelApply.DeleteByID(FormTravelApplyID);
                TAForm.DeleteByID(FormTravelApplyID);
                transaction.Commit();
            } catch (Exception) {
                transaction.Rollback();
                throw;
            } finally {
                transaction.Dispose();
            }
        }

        #endregion

        #region FormTravelApplyDetail Operate

        public void AddFormTravelApplyDetail(DateTime BeginDate, DateTime EndDate, string Departure, string Destination, string Vehicle, string Remark) {
            FormDS.FormTravelApplyDetailRow rowDetail = this.FormDataSet.FormTravelApplyDetail.NewFormTravelApplyDetailRow();

            rowDetail.FormTravelApplyID = 0;
            rowDetail.BeginDate = BeginDate;
            rowDetail.EndDate = EndDate;
            rowDetail.Departure = Departure;
            rowDetail.Destination = Destination;
            rowDetail.Vehicle = Vehicle;
            rowDetail.Days = (EndDate - BeginDate).Days + 1;
            rowDetail.Remark = Remark;
            // 填加行并进行更新处理
            this.FormDataSet.FormTravelApplyDetail.AddFormTravelApplyDetailRow(rowDetail);
        }

        public void UpdateFormTravelApplyDetail(int FormTravelApplyDetailID, DateTime BeginDate, DateTime EndDate, string Departure, string Destination, string Vehicle, string Remark) {
            FormDS.FormTravelApplyDetailRow rowDetail = this.FormDataSet.FormTravelApplyDetail.FindByFormTravelApplyDetailID(FormTravelApplyDetailID);
            rowDetail.FormTravelApplyID = 0;
            rowDetail.BeginDate = BeginDate;
            rowDetail.EndDate = EndDate;
            rowDetail.Departure = Departure;
            rowDetail.Destination = Destination;
            rowDetail.Vehicle = Vehicle;
            rowDetail.Days = (EndDate - BeginDate).Days + 1;
            rowDetail.Remark = Remark;
        }

        public void DeleteFormTravelApplyDetailByID(int FormTravelApplyDetailID) {
            for (int index = 0; index < this.FormDataSet.FormTravelApplyDetail.Count; index++) {
                if ((int)this.FormDataSet.FormTravelApplyDetail.Rows[index]["FormTravelApplyDetailID"] == FormTravelApplyDetailID) {
                    this.FormDataSet.FormTravelApplyDetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region FormTravelReimburse

        public void AddFormTravelReimburse(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID,
                SystemEnums.FormStatus StatusID, DateTime? Period, string Remark, string AttachedFileName, string RealAttachedFileName, int FormTravelApplyID) {

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
                formRow.PageType = (int)SystemEnums.PageType.TravelReimburse;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormPersonalReimburseRow formPersonalReimburseRow = this.FormDataSet.FormPersonalReimburse.NewFormPersonalReimburseRow();
                formPersonalReimburseRow.FormPersonalReimburseID = formRow.FormID;
                formPersonalReimburseRow.FormTravelApplyID = FormTravelApplyID;
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
                            newDetailRow.Place = detailRow.Place;
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
                new APFlowBLL().ApplyForm(AP, TAForm, RejectedFormID, formRow, OrganizationUnitID, new AuthorizationBLL().GetFlowTemplate((int)SystemEnums.BusinessUseCase.FormTravelReimburse, formRow.UserID), StatusID, dic);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormTravelReimburse(int FormID, SystemEnums.FormStatus StatusID, SystemEnums.FormType FormTypeID, DateTime? Period, string Remark,
                string AttachedFileName, string RealAttachedFileName) {
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
                new APFlowBLL().ApplyForm(AP, TAForm, null, formRow, formRow.OrganizationUnitID, new AuthorizationBLL().GetFlowTemplate((int)SystemEnums.BusinessUseCase.FormTravelReimburse, formRow.UserID), StatusID, dic);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void SaveRealAmountForTravelReimburse(int FormID) {
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

        public void DeleteFormTravelReimburse(int FormID) {
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

        #region TravelReimburseDetail

        public void AddFormTravelReimburseDetail(AuthorizationDS.StuffUserRow User, int? FormPersonalReimburseID, string Place, DateTime? OccurDate, int ExpenseManageTypeID, decimal Amount, string Remark) {

            FormDS.FormPersonalReimburseDetailRow rowDetail = this.FormDataSet.FormPersonalReimburseDetail.NewFormPersonalReimburseDetailRow();
            rowDetail.FormPersonalReimburseID = FormPersonalReimburseID.GetValueOrDefault();

            rowDetail.Place = Place;
            rowDetail.OccurDate = OccurDate.GetValueOrDefault();
            rowDetail.ExpenseManageTypeID = ExpenseManageTypeID;
            rowDetail.Amount = Amount;
            rowDetail.RealAmount = Amount;
            rowDetail.Remark = Remark;
            // 填加行并进行更新处理
            this.FormDataSet.FormPersonalReimburseDetail.AddFormPersonalReimburseDetailRow(rowDetail);
        }

        public void UpdateFormTravelReimburseDetail(AuthorizationDS.StuffUserRow User, string Place, DateTime? OccurDate, int ExpenseManageTypeID, decimal Amount, string Remark, int FormPersonalReimburseDetailID) {

            FormDS.FormPersonalReimburseDetailRow rowDetail = (FormDS.FormPersonalReimburseDetailRow)this.FormDataSet.FormPersonalReimburseDetail.FindByFormPersonalReimburseDetailID(FormPersonalReimburseDetailID);
            rowDetail.Place = Place;
            rowDetail.OccurDate = OccurDate.GetValueOrDefault();
            rowDetail.ExpenseManageTypeID = ExpenseManageTypeID;
            rowDetail.Amount = Amount;
            rowDetail.RealAmount = Amount;
            rowDetail.Remark = Remark;

            // 填加行并进行更新处理
        }

        public void DeleteFormTravelReimburseDetailByID(int FormPersonalReimburseDetailID) {
            for (int index = 0; index < this.FormDataSet.FormPersonalReimburseDetail.Count; index++) {
                if ((int)this.FormDataSet.FormPersonalReimburseDetail.Rows[index]["FormPersonalReimburseDetailID"] == FormPersonalReimburseDetailID) {
                    this.FormDataSet.FormPersonalReimburseDetail.Rows[index].Delete();
                    break;
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
