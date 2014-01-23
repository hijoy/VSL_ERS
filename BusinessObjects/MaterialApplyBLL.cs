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
    public class MaterialApplyBLL {
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

        private FormMaterialTableAdapter m_FormMaterialAdapter;
        public FormMaterialTableAdapter TAFormMaterial {
            get {
                if (this.m_FormMaterialAdapter == null) {
                    this.m_FormMaterialAdapter = new FormMaterialTableAdapter();
                }
                return this.m_FormMaterialAdapter;
            }
        }

        private FormMaterialDetailTableAdapter m_FormMaterialDetailAdapter;
        public FormMaterialDetailTableAdapter TAFormMaterialDetail {
            get {
                if (this.m_FormMaterialDetailAdapter == null) {
                    this.m_FormMaterialDetailAdapter = new FormMaterialDetailTableAdapter();
                }
                return this.m_FormMaterialDetailAdapter;
            }
        }

        #endregion

        #region 获取数据

        public FormDS.FormDataTable GetFormByID(int FormID) {
            return this.TAForm.GetDataByID(FormID);
        }

        public FormDS.FormMaterialDataTable GetFormMaterialByID(int FormMaterialID) {
            return this.TAFormMaterial.GetDataByID(FormMaterialID);
        }

        public FormDS.FormMaterialDetailDataTable GetFormMaterialDetailByFormMaterialID(int FormMaterialID) {
            return this.TAFormMaterialDetail.GetDataByFormMaterialID(FormMaterialID);
        }

        public FormDS.FormMaterialDetailDataTable GetFormMaterialDetail() {
            return this.FormDataSet.FormMaterialDetail;
        }

        #endregion

        #region MaterialDetail

        public void UpdateFormMaterialDetail(int FormMaterialDetailID, int MaterialID, decimal Quantity, string Remark) {

            FormDS.FormMaterialDetailDataTable table = this.FormDataSet.FormMaterialDetail;
            FormDS.FormMaterialDetailRow rowDetail = table.FindByFormMaterialDetailID(FormMaterialDetailID);
            if (rowDetail == null)
                return;
            ERS.MaterialRow material = new MasterDataBLL().GetMaterialById(MaterialID);
            rowDetail.MaterialID = MaterialID;
            rowDetail.MaterialName = material.MaterialName;
            rowDetail.UOM = material.UOM;
            rowDetail.Description = material.Description;
            rowDetail.MaterialPrice = material.MaterialPrice;
            rowDetail.Quantity = Quantity;
            rowDetail.Amount = rowDetail.MaterialPrice * Quantity;
            rowDetail.Remark = Remark;
        }

        public void AddFormMaterialDetail(int? FormMaterialID, int MaterialID, decimal Quantity, string Remark) {

            FormDS.FormMaterialDetailRow rowDetail = this.FormDataSet.FormMaterialDetail.NewFormMaterialDetailRow();
            rowDetail.FormMaterialID = FormMaterialID.GetValueOrDefault();
            ERS.MaterialRow material = new MasterDataBLL().GetMaterialById(MaterialID);
            rowDetail.MaterialID = MaterialID;
            rowDetail.MaterialName = material.MaterialName;
            rowDetail.UOM = material.UOM;
            rowDetail.Description = material.Description;
            rowDetail.MaterialPrice = material.MaterialPrice;
            rowDetail.Quantity = Quantity;
            rowDetail.Amount = rowDetail.MaterialPrice * Quantity;
            rowDetail.Remark = Remark;
            // 填加行并进行更新处理
            this.FormDataSet.FormMaterialDetail.AddFormMaterialDetailRow(rowDetail);

        }

        public void DeleteFormMaterialDetailByID(int FormMaterialDetailID) {
            for (int index = 0; index < this.FormDataSet.FormMaterialDetail.Count; index++) {
                if ((int)this.FormDataSet.FormMaterialDetail.Rows[index]["FormMaterialDetailID"] == FormMaterialDetailID) {
                    this.FormDataSet.FormMaterialDetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region FormMaterial

        public void AddFormMaterial(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID,
                SystemEnums.FormStatus StatusID, int ShopID, int FirstVolume, int SecondVolume, int ThirdVolume, string Remark, string FlowTemplate) {


            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormMaterial, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormMaterialDetail, transaction);

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
                formRow.PageType = (int)SystemEnums.PageType.MaterialApply;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormMaterialRow formMaterialRow = this.FormDataSet.FormMaterial.NewFormMaterialRow();
                formMaterialRow.FormMaterialID = formRow.FormID;
                formMaterialRow.ShopID = ShopID;
                formMaterialRow.FirstVolume = FirstVolume;
                formMaterialRow.SecondVolume = SecondVolume;
                formMaterialRow.ThirdVolume = ThirdVolume;
                formMaterialRow.Amount = 0;//默认值
                formMaterialRow.Remark = Remark;

                this.FormDataSet.FormMaterial.AddFormMaterialRow(formMaterialRow);
                this.TAFormMaterial.Update(formMaterialRow);

                //明细表
                decimal totalAmount = 0;//计算总申请金额

                if (RejectedFormID != null) {
                    FormDS.FormMaterialDetailDataTable newDetailTable = new FormDS.FormMaterialDetailDataTable();
                    foreach (FormDS.FormMaterialDetailRow detailRow in this.FormDataSet.FormMaterialDetail) {
                        if (detailRow.RowState != System.Data.DataRowState.Deleted) {
                            FormDS.FormMaterialDetailRow newDetailRow = newDetailTable.NewFormMaterialDetailRow();
                            newDetailRow.FormMaterialID = formMaterialRow.FormMaterialID;
                            newDetailRow.MaterialID = detailRow.MaterialID;
                            ERS.MaterialRow material = new MasterDataBLL().GetMaterialById(detailRow.MaterialID);
                            newDetailRow.MaterialName = material.MaterialName;
                            newDetailRow.UOM = material.UOM;
                            newDetailRow.Description = material.Description;
                            newDetailRow.MaterialPrice = material.MaterialPrice;
                            newDetailRow.Quantity = detailRow.Quantity;
                            newDetailRow.Amount = newDetailRow.MaterialPrice * detailRow.Quantity;
                            if (!detailRow.IsRemarkNull()) {
                                newDetailRow.Remark = detailRow.Remark;
                            }
                            totalAmount += newDetailRow.Amount;
                            newDetailTable.AddFormMaterialDetailRow(newDetailRow);
                        }
                    }
                    this.TAFormMaterialDetail.Update(newDetailTable);
                } else {
                    foreach (FormDS.FormMaterialDetailRow detailRow in this.FormDataSet.FormMaterialDetail) {
                        // 与父表绑定
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormMaterialID = formMaterialRow.FormMaterialID;
                            totalAmount += detailRow.Amount;
                        }
                    }
                    this.TAFormMaterialDetail.Update(this.FormDataSet.FormMaterialDetail);
                }

                formMaterialRow.Amount = totalAmount;
                this.TAFormMaterial.Update(formMaterialRow);

                // 正式提交或草稿
               
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["Apply_Amount"] = totalAmount;//金额
                    APHelper AP = new APHelper();
                    new APFlowBLL().ApplyForm(AP, TAForm, RejectedFormID, formRow, OrganizationUnitID, FlowTemplate, StatusID, dic);
                
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormMaterial(int FormID, SystemEnums.FormStatus StatusID, SystemEnums.FormType FormTypeID, int ShopID, int FirstVolume, int SecondVolume, int ThirdVolume, string Remark, string FlowTemplate) {
            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormMaterial, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormMaterialDetail, transaction);

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
                FormDS.FormMaterialRow formMaterialRow = this.TAFormMaterial.GetDataByID(FormID)[0];
                formMaterialRow.ShopID = ShopID;
                formMaterialRow.FirstVolume = FirstVolume;
                formMaterialRow.SecondVolume = SecondVolume;
                formMaterialRow.ThirdVolume = ThirdVolume;
                formMaterialRow.Remark = Remark;
                this.TAFormMaterial.Update(formMaterialRow);

                //明细表
                decimal totalAmount = 0;//计算总申请金额
                foreach (FormDS.FormMaterialDetailRow detailRow in this.FormDataSet.FormMaterialDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormMaterialID = formMaterialRow.FormMaterialID;
                        totalAmount += detailRow.Amount;
                    }
                }
                this.TAFormMaterialDetail.Update(this.FormDataSet.FormMaterialDetail);

                formMaterialRow.Amount = totalAmount;
                this.TAFormMaterial.Update(formMaterialRow);
                // 正式提交或草稿
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["Apply_Amount"] = totalAmount;//金额
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

        public void DeleteFormMaterial(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormMaterial, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormMaterialDetail, transaction);

                this.TAFormMaterialDetail.DeleteByFormMaterialID(FormID);
                this.TAFormMaterial.DeleteByID(FormID);
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
    }
}
