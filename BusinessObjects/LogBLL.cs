using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.LogDSTableAdapters;

namespace BusinessObjects {
    [System.ComponentModel.DataObject]
    public class LogBLL {
        private LogInActionLogTableAdapter m_LogInActionLogTA;
        public LogInActionLogTableAdapter LogInActionLogTA {
            get {
                if (m_LogInActionLogTA == null) {
                    m_LogInActionLogTA = new LogInActionLogTableAdapter();
                }
                return m_LogInActionLogTA;
            }
        }

        private CommonDataEditActionLogTableAdapter m_CommonDataEditActionLogTA;
        public CommonDataEditActionLogTableAdapter CommonDataEditActionLogTA {
            get {
                if (m_CommonDataEditActionLogTA == null) {
                    m_CommonDataEditActionLogTA = new CommonDataEditActionLogTableAdapter();
                }
                return this.m_CommonDataEditActionLogTA;
            }
        }

        private AuthorizationConfigureLogTableAdapter m_AuthorizationConfigureLogTA;
        public AuthorizationConfigureLogTableAdapter AuthorizationConfigureLogTA {
            get {
                if (this.m_AuthorizationConfigureLogTA == null) {
                    this.m_AuthorizationConfigureLogTA = new AuthorizationConfigureLogTableAdapter();
                }
                return this.m_AuthorizationConfigureLogTA;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public LogDS.LogInActionLogDataTable GetLogInActionLog(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "LogInTime DESC";
            }
            return this.LogInActionLogTA.GetPage("LogInActionLog", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int TotalCount(string queryExpression) {
            return this.LogInActionLogTA.QueryDataCount("LogInActionLog", queryExpression).GetValueOrDefault();            
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public LogDS.CommonDataEditActionLogDataTable GetCommonDataEditActionLog(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ActionTime DESC";
            }
            return this.CommonDataEditActionLogTA.GetPage("CommonDataEditActionLog", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int CommonDataEditActionLogTotalCount(string queryExpression) {
            return this.CommonDataEditActionLogTA.QueryDataCount("CommonDataEditActionLog", queryExpression).GetValueOrDefault();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public LogDS.AuthorizationConfigureLogDataTable GetAuthorizationConfigureLog(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ConfigureTime DESC";
            }
            return this.AuthorizationConfigureLogTA.GetPage("AuthorizationConfigureLog", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int AuthorizationConfigureLogTotalCount(string queryExpression) {
            return this.AuthorizationConfigureLogTA.QueryDataCount("AuthorizationConfigureLog", queryExpression).GetValueOrDefault();
        }
    }
}
