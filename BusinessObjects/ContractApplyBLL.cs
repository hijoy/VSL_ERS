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
    public class ContractApplyBLL {
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

        private ContractNoTableAdapter m_ContractNoAdapter;
        public ContractNoTableAdapter TAContractNo {
            get {
                if (this.m_ContractNoAdapter == null) {
                    this.m_ContractNoAdapter = new ContractNoTableAdapter();
                }
                return this.m_ContractNoAdapter;
            }
        }

        private FormContractTableAdapter m_FormContractAdapter;
        public FormContractTableAdapter TAFormContract {
            get {
                if (this.m_FormContractAdapter == null) {
                    this.m_FormContractAdapter = new FormContractTableAdapter();
                }
                return this.m_FormContractAdapter;
            }
        }

        #endregion

        #region 获取数据

        public FormDS.FormDataTable GetFormByID(int FormID) {
            return this.TAForm.GetDataByID(FormID);
        }

        public FormDS.FormContractDataTable GetFormContractByID(int FormContractID) {
            return this.TAFormContract.GetDataById(FormContractID);
        }

        #endregion

        #region FormContract

        public void AddFormContract(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID,
                SystemEnums.FormStatus StatusID, string ContractName, int ContractTypeID, Decimal? ContractAmount, int? PageNumber, string FirstCompany, string SecondCompany, string ThirdCompany, DateTime? BeginDate, DateTime? EndDate, string PaymentType, string MainContent, string ChangePart, string AttachedFileName, string RealAttachedFileName, string FlowTemplate) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormContract, transaction);

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
                formRow.PageType = (int)SystemEnums.PageType.ContractApply;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormContractRow formContractRow = this.FormDataSet.FormContract.NewFormContractRow();
                formContractRow.ContractName = ContractName;
                formContractRow.FormContractID = formRow.FormID;
                formContractRow.ContractTypeID = ContractTypeID;
                formContractRow.ContractAmount = ContractAmount.GetValueOrDefault();
                formContractRow.PageNumber = PageNumber.GetValueOrDefault();
                formContractRow.FirstCompany = FirstCompany;
                formContractRow.SecondCompany = SecondCompany;
                formContractRow.ThirdCompany = ThirdCompany;
                formContractRow.BeginDate = BeginDate.GetValueOrDefault();
                formContractRow.EndDate = EndDate.GetValueOrDefault();
                formContractRow.PaymentType = PaymentType;
                formContractRow.MainContent = MainContent;
                formContractRow.ChangePart = ChangePart;
                formContractRow.AttachedFileName = AttachedFileName;
                formContractRow.RealAttachedFileName = RealAttachedFileName;
                if (StatusID == SystemEnums.FormStatus.Awaiting)
                {
                    formContractRow.ContractNo = GetContractNo(ContractTypeID);
                }
                this.FormDataSet.FormContract.AddFormContractRow(formContractRow);
                this.TAFormContract.Update(formContractRow);

                // 正式提交或草稿
                
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["Apply_Amount"] = ContractAmount;//合同金额
                    dic["ContractType"] = new MasterDataBLL().GetContractTypeById(ContractTypeID).ContractTypeName;
                    APHelper AP = new APHelper();
                    new APFlowBLL().ApplyForm(AP, TAForm, RejectedFormID, formRow, OrganizationUnitID, FlowTemplate, StatusID, dic);
                
                //this.TAFormContract.Update(formContractRow);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormContract(int FormID, SystemEnums.FormStatus StatusID, SystemEnums.FormType FormTypeID, string ContractName, int ContractTypeID, Decimal? ContractAmount, int? PageNumber, string FirstCompany, string SecondCompany, string ThirdCompany, DateTime? BeginDate, DateTime? EndDate, string PaymentType, string MainContent, string ChangePart, string AttachedFileName, string RealAttachedFileName, string FlowTemplate) {
            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormContract, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
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

                //处理申请表的内容
                FormDS.FormContractRow formContractRow = this.TAFormContract.GetDataById(FormID)[0];
                formContractRow.ContractName = ContractName;
                formContractRow.ContractTypeID = ContractTypeID;
                formContractRow.ContractAmount = ContractAmount.GetValueOrDefault();
                formContractRow.PageNumber = PageNumber.GetValueOrDefault();
                formContractRow.FirstCompany = FirstCompany;
                formContractRow.SecondCompany = SecondCompany;
                formContractRow.ThirdCompany = ThirdCompany;
                formContractRow.BeginDate = BeginDate.GetValueOrDefault();
                formContractRow.EndDate = EndDate.GetValueOrDefault();
                formContractRow.PaymentType = PaymentType;
                formContractRow.MainContent = MainContent;
                formContractRow.ChangePart = ChangePart;
                formContractRow.AttachedFileName = AttachedFileName;
                formContractRow.RealAttachedFileName = RealAttachedFileName;
                if (StatusID == SystemEnums.FormStatus.Awaiting)
                {
                    formContractRow.ContractNo = GetContractNo(ContractTypeID);
                }
                this.TAFormContract.Update(formContractRow);

                // 正式提交或草稿
                
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["Apply_Amount"] = ContractAmount;//合同金额
                    dic["ContractType"] = new MasterDataBLL().GetContractTypeById(ContractTypeID).ContractTypeName;
                    APHelper AP = new APHelper();
                    new APFlowBLL().ApplyForm(AP, TAForm, null, formRow, formRow.OrganizationUnitID, FlowTemplate, StatusID, dic);
                
                    //this.TAFormContract.Update(formContractRow);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void DeleteFormContract(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormContract, transaction);
                this.TAFormContract.DeleteById(FormID);
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

        public string GetContractNo(int? ContractTypeID) {
            int? Year = 0;
            int? SequenceNo = 0;
            this.TAContractNo.GenerateContractNo(ContractTypeID, ref Year, ref SequenceNo);
            return new MasterDataBLL().GetContractTypeById(ContractTypeID.GetValueOrDefault()).ContractTypeMark + Year.GetValueOrDefault().ToString().Substring(2) + SequenceNo.GetValueOrDefault().ToString().PadLeft(4, '0');
        }

        public void ContractStamp(int FormID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormContract);
                FormDS.FormContractRow contract = this.TAFormContract.GetDataById(FormID)[0];
                contract.isStamped = true;
                this.TAFormContract.Update(contract);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }

        }

        public void ContractRecovery(int FormID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormContract);
                FormDS.FormContractRow contract = this.TAFormContract.GetDataById(FormID)[0];
                contract.isRecovery = true;
                this.TAFormContract.Update(contract);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }

        }

        #endregion
    }
}
