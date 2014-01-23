using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.QueryDSTableAdapters;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects {
    public class FormQueryBLL {

        #region 属性

        private FormViewTableAdapter m_FormViewTA;
        public FormViewTableAdapter TAFormView {
            get {
                if (m_FormViewTA == null) {
                    m_FormViewTA = new FormViewTableAdapter();
                }
                return m_FormViewTA;
            }
        }

        private FormMaterialViewTableAdapter m_FormMaterialViewTA;
        public FormMaterialViewTableAdapter TAFormMaterialView {
            get {
                if (m_FormMaterialViewTA == null) {
                    m_FormMaterialViewTA = new FormMaterialViewTableAdapter();
                }
                return m_FormMaterialViewTA;
            }
        }

        private FormContractViewTableAdapter m_FormContractViewTA;
        public FormContractViewTableAdapter TAFormContractView {
            get {
                if (m_FormContractViewTA == null) {
                    m_FormContractViewTA = new FormContractViewTableAdapter();
                }
                return m_FormContractViewTA;
            }
        }

        private FormApplyViewTableAdapter m_FormApplyViewTA;
        public FormApplyViewTableAdapter TAFormApplyView {
            get {
                if (m_FormApplyViewTA == null) {
                    m_FormApplyViewTA = new FormApplyViewTableAdapter();
                }
                return m_FormApplyViewTA;
            }
        }

        private FormReimburseViewTableAdapter m_FormReimburseViewTA;
        public FormReimburseViewTableAdapter TAFormReimburseView {
            get {
                if (m_FormReimburseViewTA == null) {
                    m_FormReimburseViewTA = new FormReimburseViewTableAdapter();
                }
                return m_FormReimburseViewTA;
            }
        }

        private FormPersonalReimburseViewTableAdapter m_FormPersonalReimburseViewTA;
        public FormPersonalReimburseViewTableAdapter TAFormPersonalReimburse {
            get {
                if (m_FormPersonalReimburseViewTA == null) {
                    m_FormPersonalReimburseViewTA = new FormPersonalReimburseViewTableAdapter();
                }
                return m_FormPersonalReimburseViewTA;
            }
        }

        private FormBudgetAllocationViewTableAdapter m_FormBudgetAllocationViewTA;
        public FormBudgetAllocationViewTableAdapter FormBudgetAllocationViewTA {
            get {
                if (m_FormBudgetAllocationViewTA == null) {
                    m_FormBudgetAllocationViewTA = new FormBudgetAllocationViewTableAdapter();
                }
                return m_FormBudgetAllocationViewTA;
            }
        }

        private FormTravelApplyViewTableAdapter _TAFormTravelApplyView;
        public FormTravelApplyViewTableAdapter TAFormTravelApplyView {
            get {
                if (_TAFormTravelApplyView == null) {
                    _TAFormTravelApplyView = new FormTravelApplyViewTableAdapter();
                }
                return _TAFormTravelApplyView;
            }
        }

        #endregion

        #region Form

        public QueryDS.FormViewDataTable GetPagedFormView(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormView.GetPagedFormView(sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public QueryDS.FormViewDataTable GetPagedFormViewForAwaiting(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate ASC";
            }
            return this.TAFormView.GetPagedFormView(sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryFormViewCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormView.QueryFormViewCount(queryExpression);
        }

        public QueryDS.FormViewRow GetFormViewByID(int FormID) {
            return this.TAFormView.GetDataByID(FormID)[0];
        }
        #endregion

        #region FormMaterialView

        public QueryDS.FormMaterialViewDataTable GetPagedFormMaterialViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormMaterialView.GetPagedFormMaterialViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormMaterialViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormMaterialView.QueryFormMaterialViewCountByRight(queryExpression, UserID, PositionID);
        }

        #endregion

        #region FormContractView

        public QueryDS.FormContractViewDataTable GetPagedFormContractViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormContractView.GetPagedFormContractViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormContractViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormContractView.QueryFormContractViewCountByRight(queryExpression, UserID, PositionID);
        }

        #endregion

        #region FormApplyView

        public QueryDS.FormApplyViewDataTable GetPagedFormApplyViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormApplyView.GetPagedFormApplyViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormApplyViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormApplyView.QueryFormApplyViewCountByRight(queryExpression, UserID, PositionID);
        }

        public QueryDS.FormApplyViewDataTable GetPagedFormApplyView(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormApplyView.GetPagedData("FormApplyView", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryFormApplyViewCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormApplyView.QueryDataCount("FormApplyView", queryExpression);
        }

        #endregion

        #region FormTravelApplyView

        public QueryDS.FormTravelApplyViewDataTable GetPagedFormTravelApplyView(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormTravelApplyView.GetPagedFormTravelApplyView("FormTravelApplyView", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryFormTravelApplyViewCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormTravelApplyView.QueryDataCount("FormTravelApplyView", queryExpression);
        }

        #endregion

        #region FormReimburseView

        public QueryDS.FormReimburseViewDataTable GetPagedFormReimburseViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormReimburseView.GetPagedFormReimburseViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormReimburseViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormReimburseView.QueryFormReimburseViewCountByRight(queryExpression, UserID, PositionID);
        }


        #endregion

        #region FormPersonalReimburseView

        public QueryDS.FormPersonalReimburseViewDataTable GetPagedFormPersonalReimburseViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormPersonalReimburse.GetPagedFormPersonalReimburseByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormPersonalReimburseViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormPersonalReimburse.QueryFormPersonalReimburseViewCountByRight(queryExpression, UserID, PositionID);
        }

        #endregion

        #region FormBudgetAllocationView
        public QueryDS.FormBudgetAllocationViewDataTable GetPagedFormBugetAllocationViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            QueryDS.FormBudgetAllocationViewDataTable table = new QueryDS.FormBudgetAllocationViewDataTable();

            return this.FormBudgetAllocationViewTA.GetPagedFormBugetAllocationViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormBugetAllocationViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }

            return (int)this.FormBudgetAllocationViewTA.QueryFormBugetAllocationViewCountByRight(queryExpression, UserID, PositionID);
        }

        #endregion
    }
}
