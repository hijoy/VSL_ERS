using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.AuthorizationDSTableAdapters;


namespace BusinessObjects {
    public class PositionRightBLL {
        private PositionTableAdapter m_PositionAD;
        public PositionTableAdapter PositionAD {
            get {
                if (m_PositionAD == null) {
                    m_PositionAD = new PositionTableAdapter();
                }
                return m_PositionAD;
            }
        }

        private PositionAndBusinessOperateTableAdapter m_PositionAndBusinessOperateTA;
        public PositionAndBusinessOperateTableAdapter PositionAndBusinessOperateTA {
            get {
                if (m_PositionAndBusinessOperateTA == null) {
                    m_PositionAndBusinessOperateTA = new PositionAndBusinessOperateTableAdapter();
                }
                return m_PositionAndBusinessOperateTA;
            }
        }

        private OperateScopeTableAdapter m_OperateScopeTA;
        public OperateScopeTableAdapter OperateScopeTA {
            get {
                if (m_OperateScopeTA == null) {
                    m_OperateScopeTA = new OperateScopeTableAdapter();
                }
                return this.m_OperateScopeTA;
            }
        }

        /// <summary>
        /// ���ְ���Ƿ���ִ��ҵ�������Ȩ��
        /// </summary>
        /// <param name="positionId">ְ��ID</param>
        /// <param name="businessOperateId">ҵ�����ID</param>        
        /// <returns>��Ȩ�޷���True</returns>
        public bool CheckPositionRight(int positionId, int businessOperateId) {
            return ((int)this.PositionAndBusinessOperateTA.HasRight(positionId, businessOperateId) > 0);
        }

    }
}
