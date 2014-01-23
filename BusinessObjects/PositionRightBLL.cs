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
        /// 检查职务是否有执行业务操作的权限
        /// </summary>
        /// <param name="positionId">职务ID</param>
        /// <param name="businessOperateId">业务操作ID</param>        
        /// <returns>有权限返回True</returns>
        public bool CheckPositionRight(int positionId, int businessOperateId) {
            return ((int)this.PositionAndBusinessOperateTA.HasRight(positionId, businessOperateId) > 0);
        }

    }
}
