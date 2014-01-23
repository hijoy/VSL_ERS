using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects.ImportDSTableAdapters;

namespace BusinessObjects {
    class ImportBLL {
        private ImportLogTableAdapter m_ImportLogTA;
        public ImportLogTableAdapter ImportLogTA {
            get {
                if (m_ImportLogTA == null) {
                    m_ImportLogTA = new ImportLogTableAdapter();
                }
                return m_ImportLogTA;
            }
        }

        private ImportLogDetailTableAdapter m_ImportLogDetailTA;
        public ImportLogDetailTableAdapter ImportLogDetailTA {
            get {
                if (m_ImportLogDetailTA == null) {
                    m_ImportLogDetailTA = new ImportLogDetailTableAdapter();

                }
                return m_ImportLogDetailTA;
            }
        }

        public ImportDS.ImportLogDataTable GetPagedImportLog(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ImportDate DESC";
            }
            return this.ImportLogTA.GetImportLogPaged("ImportLog", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryImportLogCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.ImportLogTA.QueryDataCount("ImportLog", queryExpression);
        }

        public ImportDS.ImportLogDetailDataTable GetPagedImportLogDetail(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "Line";
            }
            return this.ImportLogDetailTA.GetImportLogDetailPaged("ImportLogDetail", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryImportLogDetailCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.ImportLogDetailTA.QueryDataCount("ImportLogDetail", queryExpression);
        }
    }
}
