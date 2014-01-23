using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.FormDSTableAdapters;

namespace BusinessObjects {
    public class UtilityBLL {
        public UtilityBLL() {
        }

        private FormNoTableAdapter m_FormNoAdapter;
        public FormNoTableAdapter TAFormNo {
            get {
                if (m_FormNoAdapter == null) {
                    m_FormNoAdapter = new FormNoTableAdapter();
                }
                return m_FormNoAdapter;
            }
        }

        public string GetFormTypeString(int FormTypeID) {
            string returnStr = string.Empty;
            switch (FormTypeID) {
                case (int)SystemEnums.FormType.SalesApply:
                    returnStr = "SA";
                    break;
                case (int)SystemEnums.FormType.MaterialApply:
                    returnStr = "MA";
                    break;
                case (int)SystemEnums.FormType.ContractApply:
                    returnStr = "CA";
                    break;
                case (int)SystemEnums.FormType.PersonalReimburseApply:
                    returnStr = "PA";
                    break;
                case (int)SystemEnums.FormType.ReimburseApply:
                    returnStr = "RA";
                    break;
                case (int)SystemEnums.FormType.BudgetAllocationApply:
                    returnStr = "BA";
                    break;
                case (int)SystemEnums.FormType.TravelApply:
                    returnStr = "TA";
                    break;
            }
            return returnStr;
        }

        public string GetFormNo(string FormType) {
            DateTime? YearAndMonth = DateTime.Now;
            int? SequenceNo = new int();
            this.TAFormNo.GenerateFormNo(FormType, ref YearAndMonth, ref SequenceNo);
            if (YearAndMonth.HasValue && SequenceNo.HasValue)
                return FormType + "-" + YearAndMonth.Value.ToString("yyMM") + "-" + SequenceNo.Value.ToString().PadLeft(5, '0');
            else
                return null;
        }
        public static string GetContractTypeName(int ContractTypeID) {
            string returnStr = string.Empty;
            switch (ContractTypeID) {

                case (int)SystemEnums.ContractType.Sale:
                    returnStr = "A";
                    break;
                case (int)SystemEnums.ContractType.Market:
                    returnStr = "B";
                    break;
                case (int)SystemEnums.ContractType.Purchase:
                    returnStr = "C";
                    break;
                case (int)SystemEnums.ContractType.Admin:
                    returnStr = "D";
                    break;
                case (int)SystemEnums.ContractType.Financial:
                    returnStr = "E";
                    break;
                case (int)SystemEnums.ContractType.Facility:
                    returnStr = "F";
                    break;
            }
            return returnStr;
        }

        public static string GenerateRepeatFormStr(FormDS.FormDataTable tbForm) {
            string systemInfo = "";
            if (tbForm != null && tbForm.Count > 0) {
                foreach (FormDS.FormRow formRow in tbForm) {
                    systemInfo += "P" + formRow.FormID + ":" + formRow.FormNo + ":" + formRow.PageType + "P";
                }
            }
            return systemInfo;
        }
    }
}
