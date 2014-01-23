using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.AuthorizationDSTableAdapters;

namespace BusinessObjects {

    [System.ComponentModel.DataObject]
    public class StuffUserBLL {

        private StuffUserTableAdapter m_StuffUserAdapter;

        public StuffUserTableAdapter StuffUserAdapter {
            get {
                if (this.m_StuffUserAdapter == null) {
                    this.m_StuffUserAdapter = new StuffUserTableAdapter();
                }
                return this.m_StuffUserAdapter;
            }
        }

        public AuthorizationDS.StuffUserDataTable GetStuffUserByUserId(string userName) {
            return this.StuffUserAdapter.GetDataByUserName(userName);
        }

        public AuthorizationDS.StuffUserDataTable GetStuffUserById(int stuffUserId) {
            return this.StuffUserAdapter.GetDataById(stuffUserId);
        }

        public bool LogInUser(string clientIP, string userName, string password) {
            bool result = false;

            LogInAction action = new LogInAction();
            action.UserName = userName;
            action.ClientIP = clientIP;
            action.LogInTime = DateTime.Now;

            AuthorizationDS.StuffUserDataTable table = this.StuffUserAdapter.GetDataByUserName(userName);
            if (table.Count > 0) {
                action.StuffName = table[0].StuffName;
                action.StuffId = table[0].StuffId;

                if (table[0].UserPassword.Equals(password) ) {
                    if (table[0].IsLatestLogInTimeNull()) {
                        table[0].SetLaterLogInTimeNull();
                    } else {
                        table[0].LaterLogInTime = table[0].LatestLogInTime;
                    }
                    table[0].LatestLogInTime = DateTime.Now;
                    this.StuffUserAdapter.Update(table);
                    action.Success = true;
                    result = true;
                } 
            }
            if (result == false) {
                action.Success = false;
            }
            SysLog.LogLogInAction(action);
            return result;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public AuthorizationDS.StuffUserDataTable GetStuffUser(int StuffUserId) {
            return this.StuffUserAdapter.GetDataById(StuffUserId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public bool InsertStuffUser(string StuffName, string StuffId, string UserName, string UserPassword, string Telephone, string EMail, string EnglishName, DateTime AttendDate, decimal? TrafficFeeLimit, decimal? TelephoneFeeLimit, string BankCard) {
            int rowsAffected = 0;

            // ��Ψһ�Խ��м��
            int iCount = (int)this.StuffUserAdapter.QueryForInsDistinct(UserName);
            if (iCount > 0) {
                throw new ApplicationException("��½�ʺ��ظ������޸ģ�");
            }

            iCount = (int)this.StuffUserAdapter.QueryForInsDistinctStuffID(StuffId);
            if (iCount > 0) {
                throw new ApplicationException("Ա�������ظ������޸ģ�");
            }

            // ����������������
            AuthorizationDS.StuffUserDataTable tab = new AuthorizationDS.StuffUserDataTable();
            AuthorizationDS.StuffUserRow row = tab.NewStuffUserRow();

            try {
                // ���д�ֵ
                row.StuffName = StuffName;
                row.StuffId = StuffId;
                row.UserName = UserName;
                row.UserPassword = UserPassword;
                row.LastSetPasswordTime = DateTime.Now;
                row.IsActive = true;
                row.Telephone = Telephone;
                row.EMail = EMail;
                row.EnglishName = EnglishName;
                row.AttendDate = AttendDate;
                row.TrafficFeeLimit = TrafficFeeLimit.GetValueOrDefault();
                row.TelephoneFeeLimit = TelephoneFeeLimit.GetValueOrDefault();
                row.BankCard = BankCard;
                // ����в����и��´���
                tab.AddStuffUserRow(row);
                rowsAffected = this.StuffUserAdapter.Update(tab);
            } catch (Exception e) {
                // put errors 
                throw e;
            }
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public bool UpdateStuffUser(int StuffUserId, string StuffName, string StuffId, string UserName, string UserPassword, bool IsActive, string Telephone, string EMail, string EnglishName, DateTime AttendDate, decimal? TrafficFeeLimit, decimal? TelephoneFeeLimit, string BankCard) {

            int rowsAffected = 0;

            // ��Ψһ�Խ��м��
            int iCount = (int)this.StuffUserAdapter.QueryForUpdDistinct(StuffUserId, UserName);
            if (iCount > 0) {
                throw new ApplicationException("��½�ʺ��ظ���������");
            }

            iCount = (int)this.StuffUserAdapter.QueryForUpdDistinctStuffID(StuffId,StuffUserId);
            if (iCount > 0)
            {
                throw new ApplicationException("Ա�������ظ������޸ģ�");
            }

            // ���� StuffUserId ����Ҫ�޸ĵ�����
            AuthorizationDS.StuffUserDataTable tab = this.StuffUserAdapter.GetDataById(StuffUserId);

            if (tab.Count == 0) {
                return false;
            }

            try {
                // ���д�ֵ
                AuthorizationDS.StuffUserRow row = tab[0];
                row.StuffName = StuffName;
                row.StuffId = StuffId;
                row.UserName = UserName;
                if ((UserPassword != null) && (UserPassword != "")) {
                    row.UserPassword = UserPassword;
                    row.LastSetPasswordTime = DateTime.Now;
                }
                row.IsActive = IsActive;
                row.Telephone = Telephone;
                row.EMail = EMail;
                row.EnglishName = EnglishName;
                row.AttendDate = AttendDate;
                row.TrafficFeeLimit = TrafficFeeLimit.GetValueOrDefault();
                row.TelephoneFeeLimit = TelephoneFeeLimit.GetValueOrDefault();
                row.BankCard = BankCard;
                // ��������
                rowsAffected = this.StuffUserAdapter.Update(row);
            } catch (Exception e) {
                // put errors 
                throw e;
            }
            return rowsAffected == 1;

        }

        public bool UpdateStuffFinanceInfor(int StuffUserId, string StuffSAPNo, bool IsApprover, decimal? ApproveAmount) {

            int rowsAffected = 0;


            // ���� StuffUserId ����Ҫ�޸ĵ�����
            AuthorizationDS.StuffUserDataTable tab = this.StuffUserAdapter.GetDataById(StuffUserId);

            if (tab.Count == 0) {
                return false;
            }
            try {
                // ���д�ֵ
                AuthorizationDS.StuffUserRow row = tab[0];
                row.IsApprover = IsApprover;
                if (ApproveAmount != null) {
                    row.ApproveAmount = ApproveAmount.GetValueOrDefault();
                } else {
                    row.ApproveAmount = 0;
                }
                // ��������
                rowsAffected = this.StuffUserAdapter.Update(row);
            } catch (Exception e) {
                // put errors 
                throw e;
            }
            return rowsAffected == 1;

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public bool DeleteAlterFeeSubType(int StuffUserId) {

            int rowsAffected = 0;

            try {
                // ���� StuffUserId ɾ������
                rowsAffected = this.StuffUserAdapter.DeleteById(StuffUserId);
            } catch (Exception e) {
                throw e;
            }
            return rowsAffected == 1;
        }

        public int TotalCount(string queryExpression) {
            // ��ȡ������
            return this.StuffUserAdapter.TotalCount(queryExpression).GetValueOrDefault();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public AuthorizationDS.StuffUserDataTable GetStuffUserPaged(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            // ����ָ��ҳ������
            return this.StuffUserAdapter.GetDataPage(sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public AuthorizationDS.StuffRightViewDataTable GetStuffRightView() {
            // ����ָ��ҳ������
            return new StuffRightViewTableAdapter().GetData();
        }

        public int GetCostCenterIDByPositionID(int PositionID) {
            if (this.StuffUserAdapter.GetCostCenterByPositionID(PositionID) == null) {
                return 0;
            } else {
                return (int)this.StuffUserAdapter.GetCostCenterByPositionID(PositionID);
            }
        }
    }
}
