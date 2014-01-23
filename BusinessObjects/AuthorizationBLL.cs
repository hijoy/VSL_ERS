using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.AuthorizationDSTableAdapters;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects {
    [System.ComponentModel.DataObject]
    public class AuthorizationBLL {

        private AuthorizationDS m_ApproverCookieDs;
        public AuthorizationDS ApproverCookieDS {
            get {
                if (m_ApproverCookieDs == null) {
                    m_ApproverCookieDs = new AuthorizationDS();
                }
                return m_ApproverCookieDs;
            }
            set {
                m_ApproverCookieDs = value;
            }
        }

        #region TableAdapters
        private SystemRoleAndBusinessOperateTableAdapter m_RoleAndOperateTA;
        public SystemRoleAndBusinessOperateTableAdapter RoleAndOperateTA {
            get {
                if (m_RoleAndOperateTA == null) {
                    m_RoleAndOperateTA = new SystemRoleAndBusinessOperateTableAdapter();
                }
                return m_RoleAndOperateTA;
            }
        }

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

        private UIEntryRightTableAdapter m_UIEntryRightTA;
        public UIEntryRightTableAdapter UIEntryRightTA {
            get {
                if (m_UIEntryRightTA == null) {
                    this.m_UIEntryRightTA = new UIEntryRightTableAdapter();
                }
                return this.m_UIEntryRightTA;
            }
        }

        private BusinessUseCaseTableAdapter m_BusinessUseCaseTA;
        public BusinessUseCaseTableAdapter BusinessUseCaseTA {
            get {
                if (this.m_BusinessUseCaseTA == null) {
                    this.m_BusinessUseCaseTA = new BusinessUseCaseTableAdapter();
                }
                return this.m_BusinessUseCaseTA;
            }
        }

        private BusinessOperateTableAdapter m_BusinessOperateTA;
        public BusinessOperateTableAdapter BusinessOperateTA {
            get {
                if (this.m_BusinessOperateTA == null) {
                    this.m_BusinessOperateTA = new BusinessOperateTableAdapter();
                }
                return this.m_BusinessOperateTA;
            }
        }

        private FlowParticipantConfigureTableAdapter m_FowParticipantConfigureTA;

        public FlowParticipantConfigureTableAdapter FowParticipantConfigureTA {
            get {
                if (this.m_FowParticipantConfigureTA == null) {
                    this.m_FowParticipantConfigureTA = new FlowParticipantConfigureTableAdapter();
                }
                return this.m_FowParticipantConfigureTA;
            }
        }


        private FlowParticipantConfigureDetailTableAdapter m_FlowParticipantConfigureDetailTA;

        public FlowParticipantConfigureDetailTableAdapter FlowParticipantConfigureDetailTA {
            get {
                if (this.m_FlowParticipantConfigureDetailTA == null) {
                    this.m_FlowParticipantConfigureDetailTA = new FlowParticipantConfigureDetailTableAdapter();
                }
                return this.m_FlowParticipantConfigureDetailTA;
            }
        }

        private ProxyBusinessTableAdapter m_ProxyBusinessTableAdapter;
        public ProxyBusinessTableAdapter ProxyBusinessTA {
            get {
                if (this.m_ProxyBusinessTableAdapter == null) {
                    this.m_ProxyBusinessTableAdapter = new ProxyBusinessTableAdapter();
                }
                return this.m_ProxyBusinessTableAdapter;
            }
        }

        #endregion

        #region StuffUser

        public string GetOUPathNameByUserID(int stuffUserID) {
            string ouname = this.StuffUserTA.GetOUPathNameByUserID(stuffUserID);
            if (ouname == null)
                return "无部门";
            else
                return ouname;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="stuffUserId"></param>
        /// <returns></returns>
        public AuthorizationDS.StuffUserRow GetStuffUserById(int stuffUserId) {
            return this.StuffUserTA.GetDataById(stuffUserId)[0];
        }

        /// <summary>
        /// 设置用户的职务
        /// </summary>
        /// <param name="stuffUserId"></param>
        /// <param name="positionIds"></param>
        public void SetStuffUserPosition(int stuffUserId, int[] positionIds) {
            AuthorizationDS DS = new AuthorizationDS();
            this.StuffUserTA.Fill(DS.StuffUser);
            this.StuffUserAndPositionTA.Fill(DS.StuffUserAndPosition);

            List<int> newPositionIds = new List<int>(positionIds);
            BusinessObjects.AuthorizationDS.StuffUserRow stuffUser = DS.StuffUser.FindByStuffUserId(stuffUserId);
            foreach (BusinessObjects.AuthorizationDS.StuffUserAndPositionRow stuffUserAndPostion in stuffUser.GetStuffUserAndPositionRows()) {
                if (newPositionIds.Contains(stuffUserAndPostion.PositionId)) {
                    newPositionIds.Remove(stuffUserAndPostion.PositionId);
                } else {
                    stuffUserAndPostion.Delete();
                }
            }
            foreach (int newPositionId in newPositionIds) {
                BusinessObjects.AuthorizationDS.StuffUserAndPositionRow newRow = DS.StuffUserAndPosition.NewStuffUserAndPositionRow();
                newRow.PositionId = newPositionId;
                newRow.StuffUserId = stuffUserId;
                DS.StuffUserAndPosition.AddStuffUserAndPositionRow(newRow);
            }
            this.StuffUserAndPositionTA.Update(DS.StuffUserAndPosition);
        }

        /// <summary>
        /// 获取用户的职务
        /// </summary>
        /// <param name="stuffUserId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public BusinessObjects.AuthorizationDS.PositionDataTable GetPositionByStuffUser(int stuffUserId) {
            return this.PositionTA.GetDataByStuffUserId(stuffUserId);
        }

        /// <summary>
        /// 根据用户名称获取用户职务
        /// </summary>
        /// <param name="stuffUserId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public BusinessObjects.AuthorizationDS.StuffUserAndPositionDataTable GetPositionByStuffUserName(string stuffUserName) {
            return this.StuffUserAndPositionTA.GetDataByStuffName(stuffUserName);
        }

        public string GetApprovalNamesByUserIds(string UserIds) {
            string approvalNames = string.Empty;
            if ((!string.IsNullOrEmpty(UserIds)) && UserIds.IndexOf("$") > 0) {
                UserIds = UserIds.Substring(0, UserIds.IndexOf("$"));
            }
            this.StuffUserTA.GetApproverNamesByUserIds(UserIds, ref approvalNames);
            return approvalNames;
        }

        #endregion

        #region SystemRole

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public BusinessObjects.AuthorizationDS.SystemRoleDataTable GetSystemRoles() {
            return this.SystemRoleTA.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public bool InsertSystemRole(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, string systemRoleName, string description) {
            if (systemRoleName == null || systemRoleName.Length == 0) {
                throw new ApplicationException("系统角色名称不能为空");
            }
            BusinessObjects.AuthorizationDS.SystemRoleDataTable table = this.SystemRoleTA.GetDataBySystemRoleName(systemRoleName);
            if (table != null && table.Count > 0) {
                throw new ApplicationException("系统角色名称不能重复");
            }

            int rowsAffected = this.SystemRoleTA.Insert(systemRoleName, description);
            AuthorizationConfigure authorizationConfigure = new AuthorizationConfigure();
            authorizationConfigure.StuffId = stuffUser.StuffId;
            authorizationConfigure.StuffName = stuffUser.StuffName;
            authorizationConfigure.ConfigureTarget = "系统角色设置";
            authorizationConfigure.ConfigureTime = DateTime.Now;
            authorizationConfigure.ConfigureType = "新增";
            authorizationConfigure.NewContent = "系统角色：" + systemRoleName;
            SysLog.LogAuthorizationConfigure(authorizationConfigure);
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public bool UpdateSystemRole(int systemRoleId, string systemRoleName, string description) {
            BusinessObjects.AuthorizationDS.SystemRoleDataTable table = this.SystemRoleTA.GetDataBySystemRoleName(systemRoleName);
            if (table != null && table.Count > 0) {
                if (table[0].SystemRoleId != systemRoleId) {
                    throw new ApplicationException("系统角色名称不能重复");
                }
            }

            BusinessObjects.AuthorizationDS.SystemRoleDataTable roleTable = this.SystemRoleTA.GetDataById(systemRoleId);
            roleTable[0].SystemRoleName = systemRoleName;
            if (description == null) {
                roleTable[0].SetDescriptionNull();
            } else {
                roleTable[0].Description = description;
            }
            int rowsAffected = this.SystemRoleTA.Update(roleTable);
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public bool DeleteSystemRole(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int systemRoleId) {
            BusinessObjects.AuthorizationDS.SystemRoleAndBusinessOperateDataTable table = this.RoleAndOperateTA.GetDataBySystemRoleId(systemRoleId);
            foreach (AuthorizationDS.SystemRoleAndBusinessOperateRow row in table) {
                row.Delete();
            }
            this.RoleAndOperateTA.Update(table);

            AuthorizationDS.PositionAndSystemRoleDataTable positionAndRoleTable = this.PositionAndSystemRoleTA.GetDataBySystemRoleId(systemRoleId);
            foreach (AuthorizationDS.PositionAndSystemRoleRow row in positionAndRoleTable) {
                row.Delete();
            }
            this.PositionAndSystemRoleTA.Update(positionAndRoleTable);

            BusinessObjects.AuthorizationDS.SystemRoleDataTable roleTable = this.SystemRoleTA.GetDataById(systemRoleId);

            string roleName = roleTable[0].SystemRoleName;
            roleTable[0].Delete();
            int rowsAffected = this.SystemRoleTA.Update(roleTable);

            AuthorizationConfigure authorizationConfigure = new AuthorizationConfigure();
            authorizationConfigure.StuffId = stuffUser.StuffId;
            authorizationConfigure.StuffName = stuffUser.StuffName;
            authorizationConfigure.ConfigureTarget = "系统角色设置";
            authorizationConfigure.ConfigureTime = DateTime.Now;
            authorizationConfigure.ConfigureType = "删除";
            authorizationConfigure.OldContent = "系统角色：" + roleName;
            SysLog.LogAuthorizationConfigure(authorizationConfigure);

            return rowsAffected == 1;
        }

        /// <summary>
        /// 获取职务的系统角色
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public BusinessObjects.AuthorizationDS.SystemRoleDataTable GetSystemRoleByPostion(int positionId) {
            return this.SystemRoleTA.GetDataByPositionId(positionId);
        }

        /// <summary>
        /// 设置职务的系统角色
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="systemRoleIds"></param>
        public void SetPositionSystemRole(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int positionId, int[] systemRoleIds) {

            StringBuilder oldContent = new StringBuilder();
            StringBuilder newContent = new StringBuilder();

            BusinessObjects.AuthorizationDS.PositionAndSystemRoleDataTable table = this.PositionAndSystemRoleTA.GetDataByPositionId(positionId);
            List<int> roleIds = new List<int>(systemRoleIds);
            foreach (BusinessObjects.AuthorizationDS.PositionAndSystemRoleRow positionAndSystemRole in table) {
                if (roleIds.Contains(positionAndSystemRole.SystemRoleId)) {
                    roleIds.Remove(positionAndSystemRole.SystemRoleId);
                } else {
                    oldContent.Append("[").Append(this.SystemRoleTA.GetSystemRoleName(positionAndSystemRole.SystemRoleId)).Append("] ");
                    positionAndSystemRole.Delete();
                }
            }
            foreach (int roleId in roleIds) {
                BusinessObjects.AuthorizationDS.PositionAndSystemRoleRow newRow = table.NewPositionAndSystemRoleRow();
                newRow.PositionId = positionId;
                newRow.SystemRoleId = roleId;
                newContent.Append("[").Append(this.SystemRoleTA.GetSystemRoleName(roleId)).Append("] ");
                table.AddPositionAndSystemRoleRow(newRow);
            }

            this.PositionAndSystemRoleTA.Update(table);

            if (oldContent.Length > 0 || newContent.Length > 0) {
                AuthorizationConfigure authorizationConfigure = new AuthorizationConfigure();
                authorizationConfigure.StuffId = stuffUser.StuffId;
                authorizationConfigure.StuffName = stuffUser.StuffName;
                authorizationConfigure.ConfigureTarget = "职务权限设置";
                authorizationConfigure.ConfigureTime = DateTime.Now;
                authorizationConfigure.ConfigureType = "更新";
                string positionName = new OUTreeBLL().GetPositionById(positionId).PositionName;
                if (oldContent.Length > 0) {
                    oldContent.Insert(0, "撤销职务[" + positionName + "]的系统角色：");
                    authorizationConfigure.OldContent = oldContent.ToString();
                }
                if (newContent.Length > 0) {
                    newContent.Insert(0, "新增职务[" + positionName + "]的系统角色：");
                    authorizationConfigure.NewContent = newContent.ToString();
                }
                SysLog.LogAuthorizationConfigure(authorizationConfigure);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newOperateIds"></param>
        /// <param name="roleId"></param>
        public void SetSystemRoleOperateRight(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, List<int> newOperateIds, int roleId) {



            List<int> deleteOperateIds = new List<int>();

            SystemRoleAndBusinessOperateTableAdapter da = new SystemRoleAndBusinessOperateTableAdapter();
            BusinessObjects.AuthorizationDS.SystemRoleAndBusinessOperateDataTable table = da.GetDataBySystemRoleId(roleId);
            foreach (BusinessObjects.AuthorizationDS.SystemRoleAndBusinessOperateRow row in table) {

                if (newOperateIds.Contains(row.BusinessOperateId)) {
                    newOperateIds.Remove(row.BusinessOperateId);
                } else {
                    deleteOperateIds.Add(row.BusinessOperateId);
                    row.Delete();
                }
            }
            AuthorizationDS.SystemRoleAndBusinessOperateDataTable newTable = new AuthorizationDS.SystemRoleAndBusinessOperateDataTable();
            foreach (int newOperateId in newOperateIds) {
                BusinessObjects.AuthorizationDS.SystemRoleAndBusinessOperateRow newRow = newTable.NewSystemRoleAndBusinessOperateRow();
                newRow.BusinessOperateId = newOperateId;
                newRow.SystemRoleId = roleId;
                newTable.AddSystemRoleAndBusinessOperateRow(newRow);
            }

            da.Update(table);
            da.Update(newTable);

            if (deleteOperateIds.Count > 0 || newOperateIds.Count > 0) {
                AuthorizationConfigure authorizationConfigure = new AuthorizationConfigure();
                authorizationConfigure.StuffId = stuffUser.StuffId;
                authorizationConfigure.StuffName = stuffUser.StuffName;
                authorizationConfigure.ConfigureTarget = "系统角色设置";
                authorizationConfigure.ConfigureTime = DateTime.Now;
                authorizationConfigure.ConfigureType = "更新";
                if (deleteOperateIds.Count > 0) {
                    StringBuilder oldContent = new StringBuilder();
                    oldContent.Append("撤销系统角色[").Append(this.SystemRoleTA.GetSystemRoleName(roleId)).Append("]授权业务操作:");
                    foreach (int id in deleteOperateIds) {
                        oldContent.Append("[").Append(this.BusinessOperateTA.GetBusinessUseCaseName(id) + "-" + this.BusinessOperateTA.GetBusinessOperateName(id)).Append("] ");
                    }
                    authorizationConfigure.OldContent = oldContent.ToString();
                }
                if (newOperateIds.Count > 0) {
                    StringBuilder newContent = new StringBuilder();
                    newContent.Append("新增系统角色[").Append(this.SystemRoleTA.GetSystemRoleName(roleId));
                    newContent.Append("]授权业务操作：");
                    foreach (int id in newOperateIds) {
                        newContent.Append("[").Append(this.BusinessOperateTA.GetBusinessUseCaseName(id) + "-" + this.BusinessOperateTA.GetBusinessOperateName(id)).Append("] ");
                    }

                    authorizationConfigure.NewContent = newContent.ToString();
                }
                SysLog.LogAuthorizationConfigure(authorizationConfigure);
            }
        }

        public string[] GetEnabledUIEntryCode(int positionId) {
            AuthorizationDS.UIEntryRightDataTable table = this.UIEntryRightTA.GetDataByPositionId(positionId);
            List<string> entryCodes = new List<string>();
            foreach (AuthorizationDS.UIEntryRightRow uiEntry in table) {
                entryCodes.Add(uiEntry.UIEntryCode);
            }
            return entryCodes.ToArray();
        }

        #endregion

        #region OperateScope

        /// <summary>
        /// 批量设置职务的业务操作范围
        /// </summary>
        /// <param name="positionId">职务ID</param>
        /// <param name="operateIds">批量业务操作ID数组</param>
        /// <param name="scopeOUIds">操作范围覆盖的组织机构ID数组</param>
        /// <param name="scopePOIds">操作范围覆盖的职务ID数组</param>
        public void SetOperateScope(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int positionId, int[] operateIds, int[] scopeOUIds, int[] scopePOIds) {


            StringBuilder deleteContent = new StringBuilder();
            StringBuilder addContent = new StringBuilder();

            foreach (int operateId in operateIds) {
                string operateName = (string)this.BusinessOperateTA.GetBusinessOperateName(operateId);
                StringBuilder addStr = new StringBuilder();
                StringBuilder deleteStr = new StringBuilder();
                List<int> sOUIds = new List<int>(scopeOUIds);
                List<int> sPOIds = new List<int>(scopePOIds);
                BusinessObjects.AuthorizationDS.OperateScopeDataTable table = this.OperateScopeTA.GetDataByOperateAndPosition(operateId, positionId);
                foreach (BusinessObjects.AuthorizationDS.OperateScopeRow operateScope in table) {
                    if (operateScope.IsScopeOrganziationUnitIdNull()) {
                        if (sPOIds.Contains(operateScope.ScopePositionId)) {
                            sPOIds.Remove(operateScope.ScopePositionId);
                        } else {
                            deleteStr.Append("职务[" +
                            new OUTreeBLL().GetPositionById(operateScope.ScopePositionId).PositionName + "] ");
                            operateScope.Delete();
                        }
                    } else {
                        if (sOUIds.Contains(operateScope.ScopeOrganziationUnitId)) {
                            sOUIds.Remove(operateScope.ScopeOrganziationUnitId);
                        } else {
                            deleteStr.Append("机构[" +
                            new OUTreeBLL().GetOrganizationUnitById(operateScope.ScopeOrganziationUnitId).OrganizationUnitName + "] ");

                            operateScope.Delete();
                        }
                    }
                }

                if (deleteStr.Length > 0) {
                    deleteStr.Insert(0, "撤销业务操作[" + operateName + "]包含数据范围：");
                    deleteContent.Append(deleteStr.ToString());
                }

                foreach (int ouId in sOUIds) {
                    BusinessObjects.AuthorizationDS.OperateScopeRow newRow = table.NewOperateScopeRow();
                    newRow.PositionId = positionId;
                    newRow.BusinessOperateId = operateId;
                    newRow.ScopeOrganziationUnitId = ouId;
                    table.AddOperateScopeRow(newRow);
                    addStr.Append("机构[" + new OUTreeBLL().GetOrganizationUnitById(ouId).OrganizationUnitName + "] ");
                }
                foreach (int poId in sPOIds) {
                    BusinessObjects.AuthorizationDS.OperateScopeRow newRow = table.NewOperateScopeRow();
                    newRow.PositionId = positionId;
                    newRow.BusinessOperateId = operateId;
                    newRow.ScopePositionId = poId;
                    table.AddOperateScopeRow(newRow);
                    addStr.Append("职务[" + new OUTreeBLL().GetPositionById(poId).PositionName + "] ");
                }

                if (addStr.Length > 0) {
                    addStr.Insert(0, "新增业务操作[" + operateName + "]包含数据范围：");
                    addContent.Append(addStr.ToString());
                }

                this.OperateScopeTA.Update(table);
            }
            if (deleteContent.Length > 0 || addContent.Length > 0) {
                AuthorizationConfigure authorizationConfigure = new AuthorizationConfigure();
                authorizationConfigure.StuffId = stuffUser.StuffId;
                authorizationConfigure.StuffName = stuffUser.StuffName;
                authorizationConfigure.ConfigureType = "更新";
                authorizationConfigure.ConfigureTarget = "业务操作范围";
                authorizationConfigure.ConfigureTime = DateTime.Now;
                string positionName = new OUTreeBLL().GetPositionById(positionId).PositionName;
                if (deleteContent.Length > 0) {
                    deleteContent.Insert(0, "职务[" + positionName + "]：");
                    authorizationConfigure.OldContent = deleteContent.ToString();
                }
                if (addContent.Length > 0) {
                    addContent.Insert(0, "职务[" + positionName + "]：");
                    authorizationConfigure.NewContent = addContent.ToString();
                }
                SysLog.LogAuthorizationConfigure(authorizationConfigure);
            }

        }

        /// <summary>
        /// 获取职务在操作上的操作范围
        /// </summary>
        /// <param name="postionId"></param>
        /// <param name="businessOperateId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public AuthorizationDS.OperateScopeDataTable GetOperateScope(int positionId, int businessOperateId) {
            return this.OperateScopeTA.GetDataByOperateAndPosition(businessOperateId, positionId);
        }
        #endregion

        #region PositionType

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public BusinessObjects.AuthorizationDS.PositionTypeDataTable GetPositionTypes() {
            return this.PositionTypeTA.GetData();
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public bool InsertPositionType(string PositionTypeName, string PositionTypeCode) {


            int rowsAffected = this.PositionTypeTA.Insert(PositionTypeName, null, PositionTypeCode);

            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public bool UpdatePositionType(int PositionTypeId, string PositionTypeName, string PositionTypeCode) {

            BusinessObjects.AuthorizationDS.PositionTypeDataTable table = this.PositionTypeTA.GetDataByPositionTypeId(PositionTypeId);

            table[0].PositionTypeName = PositionTypeName;
            table[0].PositionTypeCode = PositionTypeCode;


            int rowsAffected = this.PositionTypeTA.Update(table);

            return rowsAffected == 1;



        }

        #endregion

        #region  FlowParticipantConfigure


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public BusinessObjects.AuthorizationDS.FlowParticipantConfigureDataTable GetFlowParticipantConfigure() {
            return this.FowParticipantConfigureTA.GetData();
        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public bool InsertFlowParticipantConfigure(string FlowTemplateName, string FlowName, string BusinessUseCaseId) {


            int rowsAffected = this.FowParticipantConfigureTA.Insert(FlowTemplateName, int.Parse(BusinessUseCaseId), FlowName);

            return rowsAffected == 1;
        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public bool DeleteFlowParticipantConfigure(int FlowID) {
            BusinessObjects.AuthorizationDS.FlowParticipantConfigureDetailDataTable table = this.FlowParticipantConfigureDetailTA.GetDataByFlowID(FlowID);
            foreach (AuthorizationDS.FlowParticipantConfigureDetailRow row in table) {
                row.Delete();
            }
            this.FlowParticipantConfigureDetailTA.Update(table);



            BusinessObjects.AuthorizationDS.FlowParticipantConfigureDataTable mainTable = this.FowParticipantConfigureTA.GetDataByFlowID(FlowID);
            mainTable[0].Delete();
            int rowsAffected = this.FowParticipantConfigureTA.Update(mainTable);



            return rowsAffected == 1;
        }

        //根据用户ID和业务实例ID得到流程模板
        public string GetFlowTemplate(int BusinessUseCaseId, int UserID) {
            string FlowTemplate = "";
            AuthorizationDS.FlowParticipantConfigureDataTable table = FowParticipantConfigureTA.GetFlowTemplateNameByUseCaseAndUserID(BusinessUseCaseId, UserID);
            if (table.Rows.Count > 0) {
                FlowTemplate = table[0].FlowTemplateName;
            }
            return FlowTemplate;
        }
        #endregion

        #region FlowParticipantDetailConfigure

        public AuthorizationDS.FlowParticipantConfigureDetailDataTable GetFlowParticipantConfigureDetailByID(string FlowID) {
            return this.FlowParticipantConfigureDetailTA.GetDataByFlowID(int.Parse(FlowID));
        }


        public AuthorizationDS.FlowParticipantConfigureDetailDataTable GetFlowParticipantConfigureDetail() {
            return this.ApproverCookieDS.FlowParticipantConfigureDetail;
        }


        public void AddFlowParticipantConfigureDetail(int FlowID, int UserID, string UserName, string StaffName) {

            AuthorizationDS.FlowParticipantConfigureDetailRow rowDetail = this.ApproverCookieDS.FlowParticipantConfigureDetail.NewFlowParticipantConfigureDetailRow();
            rowDetail.FlowID = FlowID;
            rowDetail.UserID = UserID;
            rowDetail.UserName = UserName;
            rowDetail.StaffName = StaffName;
            this.ApproverCookieDS.FlowParticipantConfigureDetail.AddFlowParticipantConfigureDetailRow(rowDetail);
        }

        public void DeleteFlowParticipantConfigureDetailByID(int FlowParticipantConfigureDetailID) {
            for (int index = 0; index < this.ApproverCookieDS.FlowParticipantConfigureDetail.Rows.Count; index++) {
                if (this.ApproverCookieDS.FlowParticipantConfigureDetail.Rows[index].RowState != DataRowState.Deleted && (int)this.ApproverCookieDS.FlowParticipantConfigureDetail.Rows[index]["FlowParticipantConfigureDetailID"] == FlowParticipantConfigureDetailID) {
                    this.ApproverCookieDS.FlowParticipantConfigureDetail.Rows[index].Delete();
                    break;
                }
            }
        }


        public void SaveFlowParticipantConfigureDetail() {

            this.FlowParticipantConfigureDetailTA.Update(this.ApproverCookieDS.FlowParticipantConfigureDetail);

        }
        #endregion

        #region ProxyBusiness Operate

        public void AddProxyBusiness(int UserID, int PositionID, int ProxyUserID, DateTime BeginDate, DateTime EndDate) {

            if (this.ProxyBusinessTA.GetRepeatedCountByParameter(UserID, PositionID, ProxyUserID).GetValueOrDefault() > 0) {
                throw new ApplicationException("已经设置过代理/the proxy user had been created");
            }

            AuthorizationDS.ProxyBusinessDataTable table = new AuthorizationDS.ProxyBusinessDataTable();
            AuthorizationDS.ProxyBusinessRow row = table.NewProxyBusinessRow();
            row.UserID = UserID;
            row.PositionID = PositionID;
            row.ProxyUserID = ProxyUserID;
            row.BeginDate = BeginDate;
            row.EndDate = EndDate;
            table.AddProxyBusinessRow(row);
            this.ProxyBusinessTA.Update(table);
        }

        public void DeleteProxyBusinessByID(int ProxyBusinessID) {
            this.ProxyBusinessTA.DeleteByID(ProxyBusinessID);
        }

        public AuthorizationDS.ProxyBusinessDataTable GetProxyBusinessByProxyUserIDAndCurrentDate(int ProxyUserID, DateTime CurrentDate) {
            return this.ProxyBusinessTA.GetDataByProxyUserIDAndCurrentDate(ProxyUserID, CurrentDate);
        }

        public AuthorizationDS.ProxyBusinessDataTable GetProxyBusinessByID(int ProxyBusinessID) {
            return this.ProxyBusinessTA.GetDataByID(ProxyBusinessID);
        }

        public AuthorizationDS.ProxyBusinessDataTable GetProxyBusinessByUserID(int UserID) {
            return this.ProxyBusinessTA.GetDataByUserID(UserID);
        }

        #endregion

    }
}
