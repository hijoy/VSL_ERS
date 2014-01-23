using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.AuthorizationDSTableAdapters;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BusinessObjects {
    public class OUTreeBLL {
        #region Singleton
        private static OUTreeBLL m_Instance;
        public static OUTreeBLL Instance {
            get {
                if (m_Instance == null) {
                    m_Instance = new OUTreeBLL();
                }
                return m_Instance;
            }
        }

        public OUTreeBLL() {
            this.Init();
        }

        #endregion

        #region TableAdapters

        private OrganizationUnitTableAdapter m_OrganizationUnitTA;
        public OrganizationUnitTableAdapter OrganizationUnitTA {
            get {
                if (m_OrganizationUnitTA == null) {
                    m_OrganizationUnitTA = new OrganizationUnitTableAdapter();
                }
                return m_OrganizationUnitTA;
            }
        }

        private PositionTableAdapter m_PositionTA;
        public PositionTableAdapter PositionTA {
            get {
                if (m_PositionTA == null) {
                    m_PositionTA = new PositionTableAdapter();
                }
                return this.m_PositionTA;
            }
        }

        private PositionAndPositionTypeTableAdapter m_PositionAndPositionTypeTA;
        public PositionAndPositionTypeTableAdapter PositionAndPositionTypeTA {
            get {
                if (m_PositionAndPositionTypeTA == null) {
                    m_PositionAndPositionTypeTA = new PositionAndPositionTypeTableAdapter();
                }
                return m_PositionAndPositionTypeTA;
            }
        }

        private PositionTypeTableAdapter m_PositionTypeTA;
        public PositionTypeTableAdapter PositionTypeTA {
            get {
                if (m_PositionTypeTA == null) {
                    m_PositionTypeTA = new PositionTypeTableAdapter();
                }
                return m_PositionTypeTA;
            }
        }

        private OrganizationUnitTypeTableAdapter m_OrganizationUnitTypeTA;
        public OrganizationUnitTypeTableAdapter OrganizationUnitTypeTA {
            get {
                if (m_OrganizationUnitTypeTA == null) {
                    m_OrganizationUnitTypeTA = new OrganizationUnitTypeTableAdapter();
                }
                return this.m_OrganizationUnitTypeTA;
            }
        }

        private StuffUserTableAdapter m_StuffUserTA;
        public StuffUserTableAdapter StuffUserTA {
            get {
                if (m_StuffUserTA == null) {
                    m_StuffUserTA = new StuffUserTableAdapter();
                }
                return m_StuffUserTA;
            }
        }

        private StuffUserAndPositionTableAdapter m_StuffUserAndPositionTA;
        public StuffUserAndPositionTableAdapter StuffUserAndPositionTA {
            get {
                if (m_StuffUserAndPositionTA == null) {
                    m_StuffUserAndPositionTA = new StuffUserAndPositionTableAdapter();
                }
                return this.m_StuffUserAndPositionTA;
            }
        }

        private SystemRoleTableAdapter m_SystemRoleTA;
        public SystemRoleTableAdapter SystemRoleTA {
            get {
                if (m_SystemRoleTA == null) {
                    m_SystemRoleTA = new SystemRoleTableAdapter();
                }
                return m_SystemRoleTA;
            }
        }

        private PositionAndSystemRoleTableAdapter m_PositionAndSystemRoleTA;
        public PositionAndSystemRoleTableAdapter PositionAndSystemRoleTA {
            get {
                if (m_PositionAndSystemRoleTA == null) {
                    m_PositionAndSystemRoleTA = new PositionAndSystemRoleTableAdapter();
                }
                return m_PositionAndSystemRoleTA;
            }
        }

        private OperateScopeTableAdapter m_OperateScopeTA;
        public OperateScopeTableAdapter OperateScopeTA {
            get {
                if (m_OperateScopeTA == null) {
                    m_OperateScopeTA = new OperateScopeTableAdapter();
                }
                return m_OperateScopeTA;
            }
        }

        #endregion

        #region DATA
        private AuthorizationDS m_DS;
        public AuthorizationDS DS {
            get {
                if (m_DS == null) {
                    m_DS = new AuthorizationDS();
                }
                return m_DS;
            }
        }

        public void Init() {
            this.OrganizationUnitTA.Fill(this.DS.OrganizationUnit);
            this.OrganizationUnitTypeTA.Fill(this.DS.OrganizationUnitType);
            this.PositionTA.Fill(this.DS.Position);
            this.PositionAndPositionTypeTA.Fill(this.DS.PositionAndPositionType);
            this.PositionTypeTA.Fill(this.DS.PositionType);
        }
        #endregion

        #region OU Operate
        /// <summary>
        /// ������֯�����ļ���״̬������ʱֻ�������֯����������ʱ�������֯�����Լ�����������ְ��
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">�������֯�ṹ���ϼ�����û����������֯�����׳��쳣</exception>
        public AuthorizationDS.OrganizationUnitRow ActiveOrganizationUnit(int organizationUnitId, bool isActive) {
            AuthorizationDS.OrganizationUnitRow ou = this.DS.OrganizationUnit.FindByOrganizationUnitId(organizationUnitId);
            if (isActive && !ou.IsActive) {
                if (ou.IsParentOrganizationUnitIdNull() || ou.OrganizationUnitRowParent.IsActive) {
                    ou.IsActive = true;
                    this.OrganizationUnitTA.Update(ou);
                } else {
                    throw new ApplicationException("���������ϼ���֯����");
                }
            } else if (ou.IsActive && !isActive) {
                foreach (AuthorizationDS.OrganizationUnitRow childOU in GetDepFirstChildren(ou)) {
                    foreach (AuthorizationDS.PositionRow position in childOU.GetPositionRows()) {
                        position.IsActive = false;
                    }
                    childOU.IsActive = false;
                }
                this.OrganizationUnitTA.Update(this.DS.OrganizationUnit);
                this.PositionTA.Update(this.DS.Position);
            }
            return ou;
        }

        /// <summary>
        /// �����֯����
        /// </summary>
        /// <param name="organizationUnitName"></param>
        /// <param name="organizationUnitCode"></param>
        /// <param name="parentUnitId"></param>
        /// <param name="organizationUnitTypeId"></param>
        /// <returns></returns>
        public AuthorizationDS.OrganizationUnitRow AddOrganizationUnit(string organizationUnitName, string organizationUnitCode, int? parentUnitId, int? organizationUnitTypeId,int? CostCenterID) {
            AuthorizationDS.OrganizationUnitRow result = this.DS.OrganizationUnit.NewOrganizationUnitRow();
            result.OrganizationUnitName = organizationUnitName;
            result.OrganizationUnitCode = organizationUnitCode;
            result.IsActive = true;
            if (parentUnitId != null) {
                result.ParentOrganizationUnitId = parentUnitId.GetValueOrDefault();
                AuthorizationDS.OrganizationUnitRow parentOR = OrganizationUnitTA.GetDataById(parentUnitId.GetValueOrDefault())[0];
                result.OrganizationLevel = parentOR.OrganizationLevel + 1;
            } else {
                result.OrganizationLevel = 0;
            }
            if (organizationUnitTypeId != null) {
                result.OrganizationUnitTypeId = organizationUnitTypeId.GetValueOrDefault();
            }
            if (CostCenterID != null) {
                result.CostCenterID = CostCenterID.GetValueOrDefault();
            }
            this.DS.OrganizationUnit.AddOrganizationUnitRow(result);
            this.OrganizationUnitTA.Update(result);
            this.BuildOUOrganizationTreePath(result);
            return result;
        }

        /// <summary>
        /// ������֯����������֯����
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <param name="parentOUId"></param>
        /// <returns></returns>
        public AuthorizationDS.OrganizationUnitRow SetOrganizationUnitParentOU(int organizationUnitId, int? parentOUId) {
            if (parentOUId != null) {
                if (!this.DS.OrganizationUnit.FindByOrganizationUnitId(parentOUId.GetValueOrDefault()).IsActive) {
                    throw new ApplicationException("��֯�������ڷǼ���״̬������Ǩ��");
                }
            }
            AuthorizationDS.OrganizationUnitRow ou = this.DS.OrganizationUnit.FindByOrganizationUnitId(organizationUnitId);
            if (parentOUId == null) {
                ou.SetParentOrganizationUnitIdNull();
                ou.OrganizationLevel = 0;
            } else {
                ou.ParentOrganizationUnitId = parentOUId.GetValueOrDefault();
                AuthorizationDS.OrganizationUnitRow parentOR = OrganizationUnitTA.GetDataById(parentOUId.GetValueOrDefault())[0];
                ou.OrganizationLevel = parentOR.OrganizationLevel + 1;

            }
            this.OrganizationUnitTA.Update(ou);
            this.BuildOUOrganizationTreePath(ou);
            return ou;
        }

        /// <summary>
        /// ������֯����������Ϣ
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <param name="ouName"></param>
        /// <param name="ouCode"></param>
        /// <param name="organizationUnitTypeId"></param>
        /// <returns></returns>
        public AuthorizationDS.OrganizationUnitRow UpdateOrganizationUnit(int organizationUnitId, string ouName, string ouCode, int? organizationUnitTypeId, int? CostCenterID) {
            AuthorizationDS.OrganizationUnitRow ou = this.DS.OrganizationUnit.FindByOrganizationUnitId(organizationUnitId);
            ou.OrganizationUnitName = ouName;
            ou.OrganizationUnitCode = ouCode;
            if (organizationUnitTypeId == null) {
                ou.SetOrganizationUnitTypeIdNull();
            } else {
                ou.OrganizationUnitTypeId = organizationUnitTypeId.GetValueOrDefault(); 
            }
            if (CostCenterID == null) {
                ou.SetCostCenterIDNull();
            } else {
                ou.CostCenterID = CostCenterID.GetValueOrDefault();                
            }
            this.OrganizationUnitTA.Update(ou);
            return ou;
        }

        /// <summary>
        /// ɾ����֯������ͬʱ�ϴθ���֯����������������ְ��
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <exception cref="System.ApplicationException">�������֯��������������������ְ���Ѿ���ʹ�ã�������ʱ�׳��쳣</exception>
        public void DeleteOrganizationUnit(int organizationUnitId) {
            SqlTransaction transaction = null;
            AuthorizationDS.OrganizationUnitRow ou = this.DS.OrganizationUnit.FindByOrganizationUnitId(organizationUnitId);
            string ouName = ou.OrganizationUnitName;
            DataSet backDS = this.DS.Copy();
            try {
                PositionTableAdapter positionAdapter = new PositionTableAdapter();
                OrganizationUnitTableAdapter ouAdapter = new OrganizationUnitTableAdapter();
                PositionAndPositionTypeTableAdapter ptAd = new PositionAndPositionTypeTableAdapter();
                transaction = TableAdapterHelper.BeginTransaction(positionAdapter);
                TableAdapterHelper.SetTransaction(ouAdapter, transaction);
                TableAdapterHelper.SetTransaction(ptAd, transaction);

                //action on backDS and database first
                AuthorizationDS actionDS = new AuthorizationDS();
                actionDS.Merge(backDS);
                AuthorizationDS.OrganizationUnitRow actionOU = actionDS.OrganizationUnit.FindByOrganizationUnitId(organizationUnitId);
                AuthorizationDS.OrganizationUnitRow[] actionOUs = this.GetDepFirstChildren(actionOU);
                foreach (AuthorizationDS.OrganizationUnitRow o in actionOUs) {
                    foreach (AuthorizationDS.PositionRow position in o.GetPositionRows()) {
                        foreach (AuthorizationDS.PositionAndPositionTypeRow positionType in position.GetPositionAndPositionTypeRows()) {
                            positionType.Delete();
                        }
                        position.Delete();
                    }
                    o.Delete();
                    ptAd.Update(actionDS.PositionAndPositionType);
                    positionAdapter.Update(actionDS.Position);
                    ouAdapter.Update(actionDS.OrganizationUnit);
                }
                transaction.Commit();

                //if success update dataset

                AuthorizationDS.OrganizationUnitRow[] ous = this.GetDepFirstChildren(ou);
                foreach (AuthorizationDS.OrganizationUnitRow o in ous) {
                    foreach (AuthorizationDS.PositionRow position in o.GetPositionRows()) {
                        foreach (AuthorizationDS.PositionAndPositionTypeRow positionType in position.GetPositionAndPositionTypeRows()) {
                            positionType.Delete();
                        }
                        position.Delete();
                    }
                    o.Delete();
                    //ptAd.Update(this.DS.PositionAndPositionType);
                    //positionAdapter.Update(this.DS.Position);
                    //ouAdapter.Update(this.DS.OrganizationUnit);
                    this.DS.AcceptChanges();
                }

            } catch (Exception ex) {
                transaction.Rollback();
                //this.DS.Clear();
                //this.DS.Merge(backDS);
                throw new ApplicationException("��֯����" + ouName + "���߷�֧����,ְλ�ѱ�ʹ��");
            } finally {
                transaction.Dispose();
            }
        }
        #endregion

        #region Position Operate
        /// <summary>
        /// ���ְ��
        /// </summary>
        /// <param name="positionName"></param>
        /// <param name="organizationUnitId"></param>
        /// <param name="positionTypeIds"></param>
        /// <returns></returns>
        public AuthorizationDS.PositionRow AddPosition(string positionName, int organizationUnitId, List<int> positionTypeIds) {
            AuthorizationDS.PositionRow result = this.DS.Position.NewPositionRow();
            result.PositionName = positionName;
            result.OrganizationUnitId = organizationUnitId;
            result.IsActive = true;
            this.DS.Position.AddPositionRow(result);
            this.PositionTA.Update(result);
            if (positionTypeIds != null && positionTypeIds.Count > 0) {
                foreach (int positionTypeId in positionTypeIds) {
                    BusinessObjects.AuthorizationDS.PositionAndPositionTypeRow positionAndPositionType = this.DS.PositionAndPositionType.NewPositionAndPositionTypeRow();
                    positionAndPositionType.PositionId = result.PositionId;
                    positionAndPositionType.PositionTypeId = positionTypeId;
                    this.DS.PositionAndPositionType.AddPositionAndPositionTypeRow(positionAndPositionType);
                }
                this.PositionAndPositionTypeTA.Update(this.DS.PositionAndPositionType);
            }
            this.BuildPositionOrganizationTreePath(result);
            return result;
        }

        /// <summary>
        /// ����ְ�������Ϣ
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="positionName"></param>
        /// <param name="positionTypeIds"></param>
        /// <returns></returns>
        public AuthorizationDS.PositionRow UpdatePosition(int positionId, string positionName, List<int> positionTypeIds) {
            AuthorizationDS.PositionRow position = DS.Position.FindByPositionId(positionId);
            position.PositionName = positionName;
            PositionTA.Update(position);
            BusinessObjects.AuthorizationDS.PositionAndPositionTypeRow[] oldPositionTypes = position.GetPositionAndPositionTypeRows();
            foreach (AuthorizationDS.PositionAndPositionTypeRow oldPositionType in oldPositionTypes) {
                if (!positionTypeIds.Contains(oldPositionType.PositionTypeId)) {
                    oldPositionType.Delete();
                } else {
                    positionTypeIds.Remove(oldPositionType.PositionTypeId);
                }
            }

            foreach (int positionTypeId in positionTypeIds) {
                AuthorizationDS.PositionAndPositionTypeRow newPositionType = this.DS.PositionAndPositionType.NewPositionAndPositionTypeRow();
                newPositionType.PositionId = position.PositionId;
                newPositionType.PositionTypeId = positionTypeId;
                this.DS.PositionAndPositionType.AddPositionAndPositionTypeRow(newPositionType);
            }
            this.PositionAndPositionTypeTA.Update(this.DS.PositionAndPositionType);

            return position;
        }

        /// <summary>
        /// ����ְ��������֯����
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="organizationUnitId"></param>
        /// <returns></returns>
        public AuthorizationDS.PositionRow SetPositionParentOU(int positionId, int organizationUnitId) {
            if (DS.OrganizationUnit.FindByOrganizationUnitId(organizationUnitId).IsActive) {
                AuthorizationDS.PositionRow position = DS.Position.FindByPositionId(positionId);
                position.OrganizationUnitId = organizationUnitId;
                PositionTA.Update(position);
                this.BuildPositionOrganizationTreePath(position);
                return position;
            } else {
                throw new ApplicationException("��֯�������ڷǼ���״̬������Ǩ��");
            }
        }

        /// <summary>
        /// ����ְ��ļ���״̬
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">���������֯�����Ƕ���״̬�������ְ���׳��쳣</exception>
        public AuthorizationDS.PositionRow ActivePosition(int positionId, bool isActive) {
            AuthorizationDS.PositionRow position = this.DS.Position.FindByPositionId(positionId);
            if (isActive != position.IsActive) {
                if (isActive) {
                    if (!position.OrganizationUnitRow.IsActive) {
                        throw new ApplicationException("���������ϼ���֯����");
                    }
                }
                position.IsActive = isActive;
                this.PositionTA.Update(position);
            }
            return position;
        }


        /// <summary>
        /// ɾ��ְ��
        /// </summary>
        /// <param name="positionId"></param>
        public void DeletePosition(int positionId) {
            SqlTransaction transaction = null;
            AuthorizationDS.PositionRow position = this.DS.Position.FindByPositionId(positionId);
            string positionName = position.PositionName;
            DataSet backDS = this.DS.Copy();
            try {
                //action on backDS and database first
                AuthorizationDS actionDS = new AuthorizationDS();
                actionDS.Merge(backDS);
                AuthorizationDS.PositionRow actionPosition = actionDS.Position.FindByPositionId(positionId);
                foreach (AuthorizationDS.PositionAndPositionTypeRow positionType in actionPosition.GetPositionAndPositionTypeRows()) {
                    positionType.Delete();
                }
                actionPosition.Delete();

                PositionAndPositionTypeTableAdapter ptAd = new PositionAndPositionTypeTableAdapter();
                PositionTableAdapter psAd = new PositionTableAdapter();
                transaction = TableAdapterHelper.BeginTransaction(ptAd);
                TableAdapterHelper.SetTransaction(psAd, transaction);
                ptAd.Update(actionDS.PositionAndPositionType);
                psAd.Update(actionDS.Position);
                transaction.Commit();

                //if success, update dataset
                foreach (AuthorizationDS.PositionAndPositionTypeRow positionType in position.GetPositionAndPositionTypeRows()) {
                    positionType.Delete();
                }
                position.Delete();
                this.DS.AcceptChanges();

            } catch (Exception ex) {
                transaction.Rollback();
                //this.DS.Clear();
                //this.DS.Merge(backupDS);
                throw new ApplicationException("ְλ" + positionName + "����ϵͳ��ʹ��");
            } finally {
                if (transaction != null) {
                    transaction.Dispose();
                }
            }
        }

        #endregion

        #region Tree Path
        /// <summary>
        /// ����ְ��Ļ���·��
        /// </summary>
        /// <param name="position"></param>
        private void BuildPositionOrganizationTreePath(BusinessObjects.AuthorizationDS.PositionRow position) {
            position.OrganizationTreePath = position.OrganizationUnitRow.OrganizationTreePath + "P" + position.OrganizationUnitRow.OrganizationUnitId + "P ";
            this.PositionTA.Update(position);
        }

        /// <summary>
        /// ���������Ļ���·��
        /// </summary>
        /// <param name="ou"></param>
        public void BuildOUOrganizationTreePath(BusinessObjects.AuthorizationDS.OrganizationUnitRow ou) {
            StringBuilder path = new StringBuilder();
            AuthorizationDS.OrganizationUnitRow parent = ou.OrganizationUnitRowParent;
            if (parent != null) {
                path.Append(parent.OrganizationTreePath);
                path.Append("P" + parent.OrganizationUnitId + "P ");
            }
            ou.OrganizationTreePath = path.ToString();
            this.OrganizationUnitTA.Update(ou);

            foreach (BusinessObjects.AuthorizationDS.PositionRow position in ou.GetPositionRows()) {
                BuildPositionOrganizationTreePath(position);
            }

            foreach (AuthorizationDS.OrganizationUnitRow childOU in ou.GetOrganizationUnitRows()) {
                BuildOUOrganizationTreePath(childOU);
            }
        }

        /// <summary>
        /// ��ȡһ����֯����
        /// </summary>
        /// <returns></returns>
        public AuthorizationDS.OrganizationUnitRow[] GetRootOrganizationUnits() {
            DataRow[] rootOUs = DS.OrganizationUnit.Select("ParentOrganizationUnitId is null");
            return (AuthorizationDS.OrganizationUnitRow[])rootOUs;
        }
        public AuthorizationDS.OrganizationUnitRow[] GetActiveRootOrganizationUnits() {
            DataRow[] rootOUs = DS.OrganizationUnit.Select("ParentOrganizationUnitId is null and IsActive = 'true'");
            return (AuthorizationDS.OrganizationUnitRow[])rootOUs;
        }

        /// <summary>
        /// ��ȡ�Ӹ���ʼ���ϼ�������
        /// </summary>
        /// <param name="ouId"></param>
        /// <returns></returns>
        public List<BusinessObjects.AuthorizationDS.OrganizationUnitRow> GetParentOUsOfOU(int ouId) {
            List<AuthorizationDS.OrganizationUnitRow> result = new List<AuthorizationDS.OrganizationUnitRow>();
            AuthorizationDS.OrganizationUnitRow ou = this.DS.OrganizationUnit.FindByOrganizationUnitId(ouId);
            ou = ou.OrganizationUnitRowParent;
            while (ou != null) {
                result.Add(ou);
                ou = ou.OrganizationUnitRowParent;
            }
            return result;
        }

        /// <summary>
        /// ��ȡְ����������֯����
        /// </summary>
        /// <param name="positionId">ְ��ID</param>
        /// <returns>�Ӹ�������ʼ����</returns>
        public List<BusinessObjects.AuthorizationDS.OrganizationUnitRow> GetParentOUsOfPosition(int positionId) {
            List<BusinessObjects.AuthorizationDS.OrganizationUnitRow> result = new List<AuthorizationDS.OrganizationUnitRow>();
            int ouId = DS.Position.FindByPositionId(positionId).OrganizationUnitId;
            BusinessObjects.AuthorizationDS.OrganizationUnitRow row = DS.OrganizationUnit.FindByOrganizationUnitId(ouId);
            while (row != null) {
                result.Insert(0, row);
                row = row.OrganizationUnitRowParent;
            }
            return result;
        }

        public string GetOUNamesOfPosition(int positionId) {
            StringBuilder result = new StringBuilder();
            int ouId = DS.Position.FindByPositionId(positionId).OrganizationUnitId;
            BusinessObjects.AuthorizationDS.OrganizationUnitRow row = DS.OrganizationUnit.FindByOrganizationUnitId(ouId);
            while (row != null) {
                result.Insert(0, row.OrganizationUnitName + "-");
                row = row.OrganizationUnitRowParent;
            }
            return result.ToString();
        }



        public AuthorizationDS.OrganizationUnitRow GetParentOUOfPosition(int positionId) {
            return DS.Position.FindByPositionId(positionId).OrganizationUnitRow;
        }



        /// <summary>
        /// ������Ȼ�ȡ����֯�ṹ
        /// </summary>
        /// <param name="ou"></param>
        /// <returns></returns>
        private AuthorizationDS.OrganizationUnitRow[] GetDepFirstChildren(AuthorizationDS.OrganizationUnitRow ou) {
            List<AuthorizationDS.OrganizationUnitRow> result = new List<AuthorizationDS.OrganizationUnitRow>();
            foreach (AuthorizationDS.OrganizationUnitRow childOu in ou.GetChildren()) {
                result.AddRange(GetDepFirstChildren(childOu));
            }
            result.Add(ou);
            return result.ToArray();
        }

        #endregion

        #region Query
        /// <summary>
        /// ֱ�ӻ�ȡ��֯����
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <returns></returns>
        public AuthorizationDS.OrganizationUnitRow GetOrganizationUnitById(int organizationUnitId) {
            return this.DS.OrganizationUnit.FindByOrganizationUnitId(organizationUnitId);
        }

        /// <summary>
        /// ֱ�ӻ�ȡְ��
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public AuthorizationDS.PositionRow GetPositionById(int positionId) {
            return this.DS.Position.FindByPositionId(positionId);
        }

        /// <summary>
        /// �ڻ����о���ְ�����͵�ְ��
        /// </summary>
        /// <param name="ouId"></param>
        /// <param name="positionTypeId"></param>
        /// <returns></returns>
        public AuthorizationDS.PositionRow[] GetPositionsInOU(int ouId, int positionTypeId) {
            List<AuthorizationDS.PositionRow> result = new List<AuthorizationDS.PositionRow>();
            AuthorizationDS.PositionDataTable pos = this.PositionTA.GetDataByScope("P" + ouId + "P", positionTypeId);
            foreach (AuthorizationDS.PositionRow po in pos) {
                result.Add(DS.Position.FindByPositionId(po.PositionId));
            }
            return result.ToArray();
        }

        /// <summary>
        /// �ڻ����о��л������͵���������
        /// </summary>
        /// <param name="ouId"></param>
        /// <param name="ouTypeId"></param>
        /// <returns></returns>
        public AuthorizationDS.OrganizationUnitRow[] GetOUsInOU(int ouId, int ouTypeId) {
            List<AuthorizationDS.OrganizationUnitRow> result = new List<AuthorizationDS.OrganizationUnitRow>();
            BusinessObjects.AuthorizationDS.OrganizationUnitDataTable ous = this.OrganizationUnitTA.GetDataByScope("P" + ouId + "P", ouTypeId);
            foreach (AuthorizationDS.OrganizationUnitRow ou in ous) {
                result.Add(DS.OrganizationUnit.FindByOrganizationUnitId(ou.OrganizationUnitId));
            }

            return result.ToArray();
        }

        public AuthorizationDS.OrganizationUnitRow GetTypeOUPositionIn(int positionId, int ouTypeId) {
            foreach (AuthorizationDS.OrganizationUnitRow ou in this.GetParentOUsOfPosition(positionId)) {
                if (!ou.IsOrganizationUnitTypeIdNull()) {
                    if (ou.OrganizationUnitTypeId == ouTypeId) {
                        return ou;
                    }
                }
            }
            return null;
        }

        public int GetNearDepartmentByPosition(int positionId) {
            int? departmentID = 0;
            OrganizationUnitTA.GetNearDepartmentByPosition(positionId,ref departmentID);
            return departmentID.GetValueOrDefault();
        }

        public AuthorizationDS.OrganizationUnitDataTable GetDataByOrganizationUnitName(string OrganizationUnitName) {
            return OrganizationUnitTA.GetDataByOrganizationUnitName(OrganizationUnitName);
        }

        #endregion

        #region WorkFlow Support

        /// <summary>
        /// ��ȡ��������������ְ��
        /// </summary>
        /// <param name="positionId">����ְ��ID</param>
        /// <param name="ouTypeId">����ְ�����ڻ������Ļ�������ID</param>
        /// <param name="poTypeId">����ְ���ְ������ID</param>
        /// <returns></returns>
        public AuthorizationDS.PositionRow[] GetNearbyPositions(int positionId, int? ouTypeId, int? poTypeId) {
            BusinessObjects.AuthorizationDS.PositionRow position = this.DS.Position.FindByPositionId(positionId);
            List<BusinessObjects.AuthorizationDS.OrganizationUnitRow> positionParentOUs = this.GetParentOUsOfPosition(positionId);
            AuthorizationDS.PositionRow[] result = new AuthorizationDS.PositionRow[0];
            if (ouTypeId == null) {
                for (int i = positionParentOUs.Count; i > 0; i--) {
                    result = GetPositionsInOU(positionParentOUs[i - 1].OrganizationUnitId, poTypeId.GetValueOrDefault());
                    if (result != null && result.Length > 0) {
                        List<AuthorizationDS.PositionRow> pos = new List<AuthorizationDS.PositionRow>();
                        //������ֱϵ�ϼ�����������������ְλ
                        foreach (AuthorizationDS.PositionRow sPosition in result) {
                            if (sPosition.OrganizationUnitId == positionParentOUs[i - 1].OrganizationUnitId) {
                                //if (!(poTypeId == 2 && sPosition.PositionId == positionId)) {//������ϼ����ܣ�����Ӧ�ų�
                                //    pos.Add(sPosition);
                                //}
                                pos.Add(sPosition);
                            }
                        }
                        if (pos.Count > 0) {
                            return pos.ToArray();
                        }
                        //����������ϼ����ܣ�������ϵ�ϼ�����������������ְλ
                        if (poTypeId != 2) {
                            return result;
                        }
                    }
                }
                return result;
            } else if (poTypeId == null) {
                for (int i = positionParentOUs.Count; i > 0; i--) {
                    BusinessObjects.AuthorizationDS.OrganizationUnitRow[] ous = GetOUsInOU(positionParentOUs[i - 1].OrganizationUnitId, ouTypeId.GetValueOrDefault());
                    if (ous != null && ous.Length != 0) {
                        List<AuthorizationDS.PositionRow> pos = new List<AuthorizationDS.PositionRow>();
                        foreach (BusinessObjects.AuthorizationDS.OrganizationUnitRow ou in ous) {
                            foreach (AuthorizationDS.PositionRow sPosition in ou.GetPositionRows()) {                                
                                pos.Add(sPosition);
                            }
                        }
                        if (pos.Count > 0) {
                            return pos.ToArray();
                        }
                    }
                }
                return result;
            } else {
                //������������ҷ��������Ļ�����������ҵ����ӵ����ϣ��ڻ������ҷ���������ְ��
                for (int i = positionParentOUs.Count; i > 0; i--) {
                    if (!positionParentOUs[i - 1].IsOrganizationUnitTypeIdNull()) {
                        if (positionParentOUs[i - 1].OrganizationUnitTypeId == ouTypeId) {
                            for (int j = positionParentOUs.Count; j >= i - 1; j--) {
                                result = this.GetPositionsInOU(positionParentOUs[j - 1].OrganizationUnitId, poTypeId.GetValueOrDefault());
                                if (result != null && result.Length != 0) {
                                    List<AuthorizationDS.PositionRow> pos = new List<AuthorizationDS.PositionRow>();
                                    //������ֱϵ�ϼ�����������������ְλ
                                    foreach (AuthorizationDS.PositionRow sPosition in result) {
                                        if (sPosition.OrganizationUnitId == positionParentOUs[j - 1].OrganizationUnitId) {
                                            //if (!(poTypeId == 2 && sPosition.PositionId == positionId)) {//������ϼ����ܣ�����Ӧ�ų�
                                            //    pos.Add(sPosition);
                                            //}
                                            pos.Add(sPosition);
                                        }
                                    }
                                    if (pos.Count > 0) {
                                        return pos.ToArray();
                                    }
                                    //����������ϼ����ܣ�������ϵ�ϼ�����������������ְλ
                                    if (poTypeId != 2) {
                                        return result;
                                    }
                                }
                            }
                        }
                    }
                }


                //��������������Ļ������������ҷ���������ְ��
                for (int i = positionParentOUs.Count; i > 0; i--) {
                    BusinessObjects.AuthorizationDS.OrganizationUnitRow[] ous = this.GetOUsInOU(positionParentOUs[i - 1].OrganizationUnitId, ouTypeId.GetValueOrDefault());
                    List<AuthorizationDS.PositionRow> pos = new List<AuthorizationDS.PositionRow>();
                    foreach (AuthorizationDS.OrganizationUnitRow ou in ous) {
                        foreach (AuthorizationDS.PositionRow sPosition in GetPositionsInOU(ou.OrganizationUnitId, poTypeId.GetValueOrDefault())) {
                            pos.Add(sPosition);
                        }
                    }
                    if (pos.Count > 0) {
                        result = pos.ToArray();
                        return result;
                    }
                }



                return result;
            }
        }

        /// <summary>
        /// ���ְ���Ƿ���������
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="ouTypeId">ְ�����ڵĻ������Ļ�������</param>
        /// <param name="poTypeId">ְ���ְ������</param>
        /// <returns></returns>
        public bool CheckPosition(int positionId, int? ouTypeId, int? poTypeId) {
            if (ouTypeId == null) {
                return this.CheckPositionType(positionId, poTypeId.GetValueOrDefault());
            } else if (poTypeId == null) {
                return this.CheckPositionOUType(positionId, ouTypeId.GetValueOrDefault());
            } else {
                return this.CheckPositionType(positionId, poTypeId.GetValueOrDefault()) && this.CheckPositionOUType(positionId, ouTypeId.GetValueOrDefault());
            }
        }

        /// <summary>
        /// ���ְ�����ڻ������Ļ�������
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="ouTypeId"></param>
        /// <returns></returns>
        public bool CheckPositionOUType(int positionId, int ouTypeId) {
            List<BusinessObjects.AuthorizationDS.OrganizationUnitRow> positionOUs = GetParentOUsOfPosition(positionId);
            foreach (BusinessObjects.AuthorizationDS.OrganizationUnitRow ou in positionOUs) {
                if (!ou.IsOrganizationUnitTypeIdNull()) {
                    if (ou.OrganizationUnitTypeId == ouTypeId) {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// ���ְ���ְ������
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="positionTypeId"></param>
        /// <returns></returns>
        public bool CheckPositionType(int positionId, int positionTypeId) {
            PositionAndPositionTypeTableAdapter ta = new PositionAndPositionTypeTableAdapter();
            return (int)ta.IsExist(positionId, positionTypeId) > 0;
        }

        #endregion
    }
}
