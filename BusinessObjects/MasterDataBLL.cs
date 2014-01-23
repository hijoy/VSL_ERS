using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.ERSTableAdapters;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects {
    [System.ComponentModel.DataObject]
    public class MasterDataBLL {

        #region Tableadapters

        private ProvinceTableAdapter m_ProvinceAdapter;
        public ProvinceTableAdapter ProvinceAdapter {
            get {
                if (this.m_ProvinceAdapter == null) {
                    this.m_ProvinceAdapter = new ProvinceTableAdapter();
                }
                return this.m_ProvinceAdapter;
            }
        }

        private CityTableAdapter m_CityAdapter;
        public CityTableAdapter CityAdapter {
            get {
                if (this.m_CityAdapter == null) {
                    this.m_CityAdapter = new CityTableAdapter();
                }
                return this.m_CityAdapter;
            }
        }

        private ExpenseCategoryTableAdapter m_ExpenseCategoryAdapter;
        public ExpenseCategoryTableAdapter ExpenseCategoryAdapter {
            get {
                if (this.m_ExpenseCategoryAdapter == null) {
                    this.m_ExpenseCategoryAdapter = new ExpenseCategoryTableAdapter();
                }
                return this.m_ExpenseCategoryAdapter;
            }
        }

        private ExpenseSubCategoryTableAdapter m_ExpenseSubCategoryAdapter;
        public ExpenseSubCategoryTableAdapter ExpenseSubCategoryAdapter {
            get {
                if (this.m_ExpenseSubCategoryAdapter == null) {
                    this.m_ExpenseSubCategoryAdapter = new ExpenseSubCategoryTableAdapter();
                }
                return this.m_ExpenseSubCategoryAdapter;
            }
        }

        private ExpenseItemTableAdapter m_ExpenseItemAdapter;
        public ExpenseItemTableAdapter ExpenseItemAdapter {
            get {
                if (this.m_ExpenseItemAdapter == null) {
                    this.m_ExpenseItemAdapter = new ExpenseItemTableAdapter();
                }
                return this.m_ExpenseItemAdapter;
            }
        }

        private CustomerTableAdapter m_CustomerAdapter;
        public CustomerTableAdapter CustomerAdapter {
            get {
                if (this.m_CustomerAdapter == null) {
                    this.m_CustomerAdapter = new CustomerTableAdapter();
                }
                return this.m_CustomerAdapter;
            }
        }

        private CustomerTypeTableAdapter m_CustomerTypeAdapter;
        public CustomerTypeTableAdapter CustomerTypeAdapter {
            get {
                if (this.m_CustomerTypeAdapter == null) {
                    this.m_CustomerTypeAdapter = new CustomerTypeTableAdapter();
                }
                return this.m_CustomerTypeAdapter;
            }
        }

        private ChannelTypeTableAdapter m_ChannelTypeAdapter;
        public ChannelTypeTableAdapter ChannelTypeAdapter {
            get {
                if (this.m_ChannelTypeAdapter == null) {
                    this.m_ChannelTypeAdapter = new ChannelTypeTableAdapter();
                }
                return this.m_ChannelTypeAdapter;
            }
        }

        private ShelfTypeTableAdapter m_ShelfTypeAdapter;
        public ShelfTypeTableAdapter ShelfTypeAdapter {
            get {
                if (this.m_ShelfTypeAdapter == null) {
                    this.m_ShelfTypeAdapter = new ShelfTypeTableAdapter();
                }
                return this.m_ShelfTypeAdapter;
            }
        }

        private ExpenseManageTypeTableAdapter m_ExpenseManageTypeAdapter;
        public ExpenseManageTypeTableAdapter ExpenseManageTypeAdapter {
            get {
                if (this.m_ExpenseManageTypeAdapter == null) {
                    this.m_ExpenseManageTypeAdapter = new ExpenseManageTypeTableAdapter();
                }
                return this.m_ExpenseManageTypeAdapter;
            }
        }

        private CustomerAmountLimitTableAdapter m_CustomerAmountLimitAdapter;
        public CustomerAmountLimitTableAdapter CustomerAmountLimitAdapter {
            get {
                if (this.m_CustomerAmountLimitAdapter == null) {
                    this.m_CustomerAmountLimitAdapter = new CustomerAmountLimitTableAdapter();
                }
                return this.m_CustomerAmountLimitAdapter;
            }
        }

        private CustomerTimesLimitTableAdapter m_CustomerTimesLimitAdapter;
        public CustomerTimesLimitTableAdapter CustomerTimesLimitAdapter {
            get {
                if (this.m_CustomerTimesLimitAdapter == null) {
                    this.m_CustomerTimesLimitAdapter = new CustomerTimesLimitTableAdapter();
                }
                return this.m_CustomerTimesLimitAdapter;
            }
        }

        private ShopTableAdapter m_ShopAdapter;
        public ShopTableAdapter ShopAdapter {
            get {
                if (this.m_ShopAdapter == null) {
                    this.m_ShopAdapter = new ShopTableAdapter();
                }
                return this.m_ShopAdapter;
            }
        }

        private ShopLevelTableAdapter m_ShopLevelAdapter;
        public ShopLevelTableAdapter ShopLevelAdapter {
            get {
                if (this.m_ShopLevelAdapter == null) {
                    this.m_ShopLevelAdapter = new ShopLevelTableAdapter();
                }
                return this.m_ShopLevelAdapter;
            }
        }

        private SKUTableAdapter m_SKUAdapter;
        public SKUTableAdapter SKUAdapter {
            get {
                if (this.m_SKUAdapter == null) {
                    this.m_SKUAdapter = new SKUTableAdapter();
                }
                return this.m_SKUAdapter;
            }
        }

        private ContractTypeTableAdapter m_ContractTypeAdapter;
        public ContractTypeTableAdapter ContractTypeAdapter {
            get {
                if (this.m_ContractTypeAdapter == null) {
                    this.m_ContractTypeAdapter = new ContractTypeTableAdapter();
                }
                return this.m_ContractTypeAdapter;
            }
        }

        private PaymentTypeTableAdapter m_PaymentTypeAdapter;
        public PaymentTypeTableAdapter PaymentTypeAdapter {
            get {
                if (this.m_PaymentTypeAdapter == null) {
                    this.m_PaymentTypeAdapter = new PaymentTypeTableAdapter();
                }
                return this.m_PaymentTypeAdapter;
            }
        }

        private SKUCategoryTableAdapter m_SKUCategoryAdapter;
        public SKUCategoryTableAdapter SKUCategoryAdapter {
            get {
                if (this.m_SKUCategoryAdapter == null) {
                    this.m_SKUCategoryAdapter = new SKUCategoryTableAdapter();
                }
                return this.m_SKUCategoryAdapter;
            }
        }

        private SKUPriceTableAdapter m_SKUPriceAdapter;
        public SKUPriceTableAdapter SKUPriceAdapter {
            get {
                if (this.m_SKUPriceAdapter == null) {
                    this.m_SKUPriceAdapter = new SKUPriceTableAdapter();
                }
                return this.m_SKUPriceAdapter;
            }
        }

        private PromotionTypeTableAdapter m_PromotionTypeAdapter;
        public PromotionTypeTableAdapter PromotionTypeAdapter {
            get {
                if (this.m_PromotionTypeAdapter == null) {
                    this.m_PromotionTypeAdapter = new PromotionTypeTableAdapter();
                }
                return this.m_PromotionTypeAdapter;
            }
        }

        private PromotionScopeTableAdapter m_PromotionScopeAdapter;
        public PromotionScopeTableAdapter PromotionScopeAdapter {
            get {
                if (this.m_PromotionScopeAdapter == null) {
                    this.m_PromotionScopeAdapter = new PromotionScopeTableAdapter();
                }
                return this.m_PromotionScopeAdapter;
            }
        }

        private MaterialTableAdapter m_MaterialAdapter;
        public MaterialTableAdapter MaterialAdapter {
            get {
                if (this.m_MaterialAdapter == null) {
                    this.m_MaterialAdapter = new MaterialTableAdapter();
                }
                return this.m_MaterialAdapter;
            }
        }

        private ShopViewTableAdapter m_ShopViewAdapter;
        public ShopViewTableAdapter ShopViewAdapter {
            get {
                if (this.m_ShopViewAdapter == null) {
                    this.m_ShopViewAdapter = new ShopViewTableAdapter();
                }
                return this.m_ShopViewAdapter;
            }
        }

        private BulletinTableAdapter m_BulletinTableAdapter;
        public BulletinTableAdapter BulletinTableAdapter {
            get {
                if (this.m_BulletinTableAdapter == null) {
                    this.m_BulletinTableAdapter = new BulletinTableAdapter();
                }
                return this.m_BulletinTableAdapter;
            }
        }

        private RejectReasonTableAdapter m_RejectReasonAdapter;
        public RejectReasonTableAdapter RejectReasonAdapter {
            get {
                if (this.m_RejectReasonAdapter == null) {
                    this.m_RejectReasonAdapter = new ERSTableAdapters.RejectReasonTableAdapter();
                }
                return this.m_RejectReasonAdapter;
            }
        }

        private ApplyYearTableAdapter m_ApplyYearTableAdapter;
        public ApplyYearTableAdapter ApplyYearTableAdapter {
            get {
                if (this.m_ApplyYearTableAdapter == null) {
                    this.m_ApplyYearTableAdapter = new ERSTableAdapters.ApplyYearTableAdapter();
                }
                return this.m_ApplyYearTableAdapter;
            }
        }

        private CostCenterTableAdapter m_CostCenterTableAdapter;
        public CostCenterTableAdapter CostCenterAdapter {
            get {
                if (this.m_CostCenterTableAdapter == null) {
                    this.m_CostCenterTableAdapter = new ERSTableAdapters.CostCenterTableAdapter();
                }
                return this.m_CostCenterTableAdapter;
            }
        }

        private ReimbursePeriodTableAdapter m_ReimbursePeriodTableAdapter;
        public ReimbursePeriodTableAdapter ReimbursePeriodTableAdapter {
            get {
                if (this.m_ReimbursePeriodTableAdapter == null) {
                    this.m_ReimbursePeriodTableAdapter = new ERSTableAdapters.ReimbursePeriodTableAdapter();
                }
                return this.m_ReimbursePeriodTableAdapter;
            }
        }

        private ProxyReimburseTableAdapter m_ProxyReimburseTableAdapter;
        public ProxyReimburseTableAdapter ProxyReimburseAdapter {
            get {
                if (this.m_ProxyReimburseTableAdapter == null) {
                    this.m_ProxyReimburseTableAdapter = new ProxyReimburseTableAdapter();
                }
                return this.m_ProxyReimburseTableAdapter;
            }
        }

        private AccruedPeriodTableAdapter m_AccruedPeriodTableAdapter;
        public AccruedPeriodTableAdapter AccruedPeriodTableAdapter {
            get {
                if (this.m_AccruedPeriodTableAdapter == null) {
                    this.m_AccruedPeriodTableAdapter = new AccruedPeriodTableAdapter();
                }
                return this.m_AccruedPeriodTableAdapter;
            }
        }

        private EmailHistoryTableAdapter _TAEmailHistory;
        public EmailHistoryTableAdapter TAEmailHistory {
            get {
                if (this._TAEmailHistory == null) {
                    this._TAEmailHistory = new EmailHistoryTableAdapter();
                }
                return this._TAEmailHistory;
            }
        }

        #endregion

        #region Province Operate
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ERS.ProvinceDataTable GetProvince() {
            return this.ProvinceAdapter.GetAllData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.ProvinceDataTable GetProvinceById(int id) {
            return this.ProvinceAdapter.GetDataById(id);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public string GetNameById(int id) {
            string result = "";
            ERS.ProvinceDataTable dt = this.ProvinceAdapter.GetDataById(id);
            if (dt != null && dt.Rows.Count > 0) {
                result = dt[0].ProvinceName.ToString();
            }
            return result;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public int GetProvIDByName(string provName) {
            int provID = 0;
            ERS.ProvinceDataTable dt = this.ProvinceAdapter.GetDataByProvName(provName);
            if (dt != null && dt.Rows.Count > 0) {
                provID = dt[0].ProvinceID;
            }
            return provID;
        }

        public ERS.ProvinceDataTable GetProvinceByProvinceName(string provName) {
            return this.ProvinceAdapter.GetDataByProvName(provName);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertProvince(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, string ProvinceName, bool IsActive) {
            int iCount = (int)this.ProvinceAdapter.SearchProvinceNameByIns(ProvinceName);
            if (iCount > 0) {
                throw new ApplicationException("省份名称重复");
            }
            ERS.ProvinceDataTable currTab = new ERS.ProvinceDataTable();
            ERS.ProvinceRow provRow = currTab.NewProvinceRow();
            provRow.ProvinceName = ProvinceName;
            provRow.IsActive = IsActive;
            currTab.AddProvinceRow(provRow);
            this.ProvinceAdapter.Update(currTab);

            CommonDataEditAction action = new CommonDataEditAction();
            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            action.DataTableName = "省份";
            action.ActionType = "添加";
            action.ActionTime = DateTime.Now;
            action.NewValue = "省份名称：" + provRow.ProvinceName;
            SysLog.LogCommonDataEditAction(action);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateProvince(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int ProvinceID, string ProvinceName, bool IsActive) {
            int iCount = (int)this.ProvinceAdapter.SearchProvinceNameByUpd(ProvinceName, ProvinceID);
            if (iCount > 0) {
                throw new ApplicationException("省份名称重复");
            }

            CommonDataEditAction action = new CommonDataEditAction();

            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            action.DataTableName = "省份";
            action.ActionType = "更新";
            action.ActionTime = DateTime.Now;

            this.CityAdapter.DisabledCityByProvId(ProvinceID);
            // 根据 CurrId 查找要修改的数据
            ERS.ProvinceDataTable provTab = this.ProvinceAdapter.GetDataById(ProvinceID);

            ERS.ProvinceRow provRow = provTab[0];

            action.OldValue = "省份名称：" + provRow.ProvinceName;


            provRow.ProvinceName = ProvinceName;
            provRow.IsActive = IsActive;
            this.ProvinceAdapter.Update(provRow);
            action.NewValue = "省份名称：" + provRow.ProvinceName;
            SysLog.LogCommonDataEditAction(action);
        }

        #endregion

        #region City Operate

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ERS.CityDataTable GetCityByProvId(int ProvId) {
            return this.CityAdapter.GetDataById(ProvId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCity(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int ProvId, string CityName, bool IsActive) {

            // 检证"城市"唯一性
            int iCount = (int)this.CityAdapter.SearchCityNameByIns(CityName, ProvId);
            if (iCount > 0) {
                throw new ApplicationException("在同一省份下，城市名称不能重复！");
            }

            // 进行数据新增处理
            ERS.CityDataTable cityTab = new ERS.CityDataTable();
            ERS.CityRow cityRow = cityTab.NewCityRow();

            // 进行传值
            cityRow.ProvinceID = ProvId;
            cityRow.CityName = CityName;
            cityRow.IsActive = IsActive;
            // 填加行并进行更新处理
            cityTab.AddCityRow(cityRow);
            this.CityAdapter.Update(cityTab);

            CommonDataEditAction action = new CommonDataEditAction();
            action.ActionTime = DateTime.Now;
            action.ActionType = "添加";
            action.DataTableName = "城市";
            action.NewValue = "城市名称：" + cityRow.CityName;
            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            SysLog.LogCommonDataEditAction(action);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCity(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int CityId, string CityName, int ProvId, bool IsActive) {
            // 检证"城市"唯一性
            int iCount = (int)this.CityAdapter.SearchCityNameByUpd(CityName, CityId);
            if (iCount > 0) {
                throw new ApplicationException("在同一省份下，城市名称不能重复！");
            }
            ERS.CityDataTable cityTab = this.CityAdapter.GetDataById(CityId);
            ERS.CityRow cityRow = cityTab[0];
            cityRow.CityName = CityName;
            cityRow.IsActive = IsActive;
            this.CityAdapter.Update(cityRow);
        }

        public int TotalCount(int ProvId) {
            return (int)this.CityAdapter.QueryDataCount("City", "ProvinceID=" + ProvId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.CityDataTable GetCityPaged(int ProvId, int startRowIndex, int maximumRows, string sortExpression) {
            return this.CityAdapter.GetPage("City", sortExpression, startRowIndex, maximumRows, "ProvinceID=" + ProvId);
        }

        public ERS.CityDataTable GetCityByCityName(string CityName) {
            return this.CityAdapter.GetDataByCityName(CityName);
        }

        #endregion

        #region ExpenseCategory Operate
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ERS.ExpenseCategoryDataTable GetExpenseCategory() {
            return this.ExpenseCategoryAdapter.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.ExpenseCategoryDataTable GetExpenseCategoryById(int id) {
            return this.ExpenseCategoryAdapter.GetDataById(id);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public string GetExpenseCategoryNameById(int id) {
            string result = "";
            ERS.ExpenseCategoryDataTable dt = this.ExpenseCategoryAdapter.GetDataById(id);
            if (dt != null && dt.Rows.Count > 0) {
                result = dt[0].ExpenseCategoryName.ToString();
            }
            return result;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public int GetCateIDByName(string provName) {
            int provID = 0;
            ERS.ExpenseCategoryDataTable dt = this.ExpenseCategoryAdapter.GetDataByCateName(provName);
            if (dt != null && dt.Rows.Count > 0) {
                provID = dt[0].ExpenseCategoryID;
            }
            return provID;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertExpenseCategory(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, string ExpenseCategoryName, bool IsActive) {
            // 验证是否存在 ExpenseCategoryName 同名数据。
            int iCount = (int)this.ExpenseCategoryAdapter.SearchCateByIns(ExpenseCategoryName);
            if (iCount > 0) {
                throw new ApplicationException("费用大类名称重名");
            }

            // 进行数据新增处理
            ERS.ExpenseCategoryDataTable cateTab = new ERS.ExpenseCategoryDataTable();
            ERS.ExpenseCategoryRow cateRow = cateTab.NewExpenseCategoryRow();

            cateRow.ExpenseCategoryName = ExpenseCategoryName;
            cateRow.IsActive = IsActive;
            // 填加行并进行更新处理
            cateTab.AddExpenseCategoryRow(cateRow);
            this.ExpenseCategoryAdapter.Update(cateTab);

            CommonDataEditAction action = new CommonDataEditAction();
            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            action.DataTableName = "费用大类";
            action.ActionType = "添加";
            action.ActionTime = DateTime.Now;
            action.NewValue = "费用大类：" + cateRow.ExpenseCategoryName;
            SysLog.LogCommonDataEditAction(action);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCategory(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int ExpenseCategoryID, string ExpenseCategoryName, bool IsActive) {
            // 验证是否存在 ExpenseCategoryName 同名数据。
            int iCount = (int)this.ExpenseCategoryAdapter.SearchCateByUpd(ExpenseCategoryName, ExpenseCategoryID);
            if (iCount > 0) {
                throw new ApplicationException("费用大类名称重名");
            }

            CommonDataEditAction action = new CommonDataEditAction();

            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            action.DataTableName = "费用大类";
            action.ActionType = "更新";
            action.ActionTime = DateTime.Now;

            this.ExpenseSubCategoryAdapter.DisabledExpenseSubCateByCateId(ExpenseCategoryID);
            // 根据 CurrId 查找要修改的数据
            ERS.ExpenseCategoryDataTable provTab = this.ExpenseCategoryAdapter.GetDataById(ExpenseCategoryID);

            ERS.ExpenseCategoryRow provRow = provTab[0];

            action.OldValue = "费用大类名称：" + provRow.ExpenseCategoryName;


            provRow.ExpenseCategoryName = ExpenseCategoryName;
            provRow.IsActive = IsActive;
            this.ExpenseCategoryAdapter.Update(provRow);
            action.NewValue = "费用大类名称：" + provRow.ExpenseCategoryName;
            SysLog.LogCommonDataEditAction(action);
        }

        #endregion

        #region ExpenseSubCategory Operate

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ERS.ExpenseSubCategoryDataTable GetSubCategoryByCateId(int CateId) {
            return this.ExpenseSubCategoryAdapter.GetDataByCateId(CateId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertSubCategory(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, string ExpenseSubCategoryName, int CateId, int PageType, Boolean IsActive) {
            // 检证"费用小类"唯一性
            int iCount = (int)this.ExpenseSubCategoryAdapter.SearchSubCateNameByIns(ExpenseSubCategoryName, CateId);
            if (iCount > 0) {
                throw new ApplicationException("在同一费用大类下，费用小类不能重复！");
            }
            // 进行数据新增处理
            ERS.ExpenseSubCategoryDataTable subCateTab = new ERS.ExpenseSubCategoryDataTable();
            ERS.ExpenseSubCategoryRow subCateRow = subCateTab.NewExpenseSubCategoryRow();
            // 进行传值
            subCateRow.ExpenseCategoryID = CateId;
            subCateRow.ExpenseSubCategoryName = ExpenseSubCategoryName;
            subCateRow.PageType = PageType;
            subCateRow.IsActive = IsActive;
            // 填加行并进行更新处理
            subCateTab.AddExpenseSubCategoryRow(subCateRow);
            this.ExpenseSubCategoryAdapter.Update(subCateTab);

            CommonDataEditAction action = new CommonDataEditAction();
            action.ActionTime = DateTime.Now;
            action.ActionType = "添加";
            action.DataTableName = "费用小类";
            action.NewValue = "费用小类名称：" + subCateRow.ExpenseSubCategoryName;
            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            SysLog.LogCommonDataEditAction(action);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateSubCategory(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int ExpenseSubCategoryId, string ExpenseSubCategoryName, int CateId, int PageType, Boolean IsActive) {
            // 检证"费用小类"唯一性
            int iCount = (int)this.ExpenseSubCategoryAdapter.SearchSubCateNameByUpd(ExpenseSubCategoryId, ExpenseSubCategoryName, CateId);
            if (iCount > 0) {
                throw new ApplicationException("在同一费用大类下，费用小类名称不能重复！");
            }
            CommonDataEditAction action = new CommonDataEditAction();
            action.ActionTime = DateTime.Now;
            action.ActionType = "更新";
            action.DataTableName = "费用小类";
            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            // 根据 CurrId 查找要修改的数据
            ERS.ExpenseSubCategoryDataTable subCateTab = this.ExpenseSubCategoryAdapter.GetDataById(ExpenseSubCategoryId);
            ERS.ExpenseSubCategoryRow subCateRow = subCateTab[0];
            action.OldValue = "费用小类名称：" + subCateRow.ExpenseSubCategoryName;
            subCateRow.ExpenseSubCategoryName = ExpenseSubCategoryName;
            subCateRow.PageType = PageType;
            subCateRow.IsActive = IsActive;
            // 更新数据
            this.ExpenseSubCategoryAdapter.Update(subCateRow);
            action.NewValue = "费用小类名称：" + subCateRow.ExpenseSubCategoryName;
            SysLog.LogCommonDataEditAction(action);
        }

        public int ExpenseSubCategoryTotalCount(int CateId) {
            return (int)this.ExpenseSubCategoryAdapter.QueryDataCount("ExpenseSubCategory", "ExpenseCategoryID=" + CateId);
        }

        public String GetExpenseSubCateNameById(int SubCateId) {
            return (string)this.ExpenseSubCategoryAdapter.GetNameById(SubCateId);
        }

        public String GetExpenseCateNameBySubCateId(int SubCateId) {
            return (string)this.ExpenseCategoryAdapter.GetNameBySubCateId(SubCateId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.ExpenseSubCategoryDataTable GetSubCategoryPaged(int CateId, int startRowIndex, int maximumRows, string sortExpression) {
            return this.ExpenseSubCategoryAdapter.GetPage("ExpenseSubCategory", sortExpression, startRowIndex, maximumRows, "ExpenseCategoryID=" + CateId);
        }

        public ERS.ExpenseSubCategoryRow GetExpenseSubCategoryById(int SubCateId) {
            return this.ExpenseSubCategoryAdapter.GetDataById(SubCateId)[0];
        }

        #endregion

        #region ExpenseItem Operate
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.ExpenseItemDataTable GetExpenseItemPaged(int startRowIndex, int maximumRows, string sortExpression, string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "ExpenseItemID is not null";
            }
            return this.ExpenseItemAdapter.GetDataByPage("ExpenseItem", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertExpenseItem(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, string ExpenseItemName, int ExpenseSubCategoryID, string AccountingName, string AccountingCode, Boolean IsInContract, Boolean IsActive) {
            // 检证"费用项"唯一性
            int iCount = (int)this.ExpenseItemAdapter.SearchItemNameByIns(ExpenseItemName);
            if (iCount > 0) {
                throw new ApplicationException("在同一费用小类下，费用项不能重复！");
            }
            // 进行数据新增处理
            ERS.ExpenseItemDataTable subCateItemTab = new ERS.ExpenseItemDataTable();
            ERS.ExpenseItemRow subCateItemRow = subCateItemTab.NewExpenseItemRow();
            // 进行传值
            subCateItemRow.ExpenseSubCategoryID = ExpenseSubCategoryID;
            subCateItemRow.ExpenseItemName = ExpenseItemName;
            subCateItemRow.AccountingName = AccountingName;
            subCateItemRow.AccountingCode = AccountingCode;
            subCateItemRow.IsInContract = IsInContract;
            subCateItemRow.IsActive = IsActive;
            // 填加行并进行更新处理
            subCateItemTab.AddExpenseItemRow(subCateItemRow);
            this.ExpenseItemAdapter.Update(subCateItemTab);
            CommonDataEditAction action = new CommonDataEditAction();
            action.ActionTime = DateTime.Now;
            action.ActionType = "添加";
            action.DataTableName = "费用项";
            action.NewValue = "费用项名称：" + subCateItemRow.ExpenseItemName;
            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            SysLog.LogCommonDataEditAction(action);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateExpenseItem(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, string ExpenseItemName, int ExpenseSubCategoryID, string AccountingName, string AccountingCode, Boolean IsInContract, Boolean IsActive, int ExpenseItemID) {
            // 检证"费用项"唯一性
            int iCount = (int)this.ExpenseItemAdapter.SearchItemNameByUpd(ExpenseItemID, ExpenseItemName);
            if (iCount > 0) {
                throw new ApplicationException("在同一费用小类下，费用项名称不能重复！");
            }
            CommonDataEditAction action = new CommonDataEditAction();
            action.ActionTime = DateTime.Now;
            action.ActionType = "更新";
            action.DataTableName = "费用项";
            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            // 根据 ItemId 查找要修改的数据
            ERS.ExpenseItemDataTable expenseItemTab = this.ExpenseItemAdapter.GetDataById(ExpenseItemID);
            ERS.ExpenseItemRow expenseItemRow = expenseItemTab[0];
            action.OldValue = "费用项名称：" + expenseItemRow.ExpenseItemName;
            expenseItemRow.ExpenseItemName = ExpenseItemName;
            expenseItemRow.AccountingCode = AccountingCode;
            expenseItemRow.ExpenseSubCategoryID = ExpenseSubCategoryID;
            expenseItemRow.AccountingName = AccountingName;
            expenseItemRow.IsInContract = IsInContract;
            expenseItemRow.IsActive = IsActive;
            // 更新数据
            this.ExpenseItemAdapter.Update(expenseItemRow);
            action.NewValue = "费用项名称：" + expenseItemRow.ExpenseItemName;
            SysLog.LogCommonDataEditAction(action);
        }

        public int ExpenseItemTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "ExpenseItemID is not null";
            }
            return (int)this.ExpenseItemAdapter.QueryDataCount("ExpenseItem", queryExpression);
        }

        public ERS.ExpenseItemRow GetExpenseItemByID(int ExpenseItemID) {
            return this.ExpenseItemAdapter.GetDataById(ExpenseItemID)[0];
        }

        public ERS.ExpenseItemDataTable GetExpenseItemBySubCateId(int SubCateID) {
            return this.ExpenseItemAdapter.GetExpenseItemBySubCategoryId(SubCateID);
        }
        #endregion

        #region Customer Operate
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.CustomerDataTable GetCustomerPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "CustomerID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "CustomerID is not null";
            }
            return this.CustomerAdapter.GetCustomerPaged("Customer", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCustomer(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, string CustomerName, string CustomerNo, int CityID, int CustomerTypeID, int ChannelTypeID, int ApplyOrganizationUnitID, int OrganizationUnitID, Boolean IsActive) {
            if (ApplyOrganizationUnitID <= 0) {
                throw new ApplicationException("必须选择所属机构！");
            }
            if (OrganizationUnitID <= 0) {
                throw new ApplicationException("必须选择所属预算机构！");
            }
            // 检证"客户名称"唯一性
            int iCount = (int)this.CustomerAdapter.SearchNameByIns(CustomerName);
            if (iCount > 0) {
                throw new ApplicationException("客户名称不能重复！");
            }
            // 进行数据新增处理
            ERS.CustomerDataTable custItemTab = new ERS.CustomerDataTable();
            ERS.CustomerRow custRow = custItemTab.NewCustomerRow();
            // 进行传值
            custRow.CustomerName = CustomerName;
            custRow.CustomerNo = CustomerNo;
            custRow.CustomerTypeID = CustomerTypeID;
            custRow.CityID = CityID;
            custRow.ChannelTypeID = ChannelTypeID;
            custRow.OrganizationUnitID = OrganizationUnitID;
            custRow.ApplyOrganizationUnitID = ApplyOrganizationUnitID;
            custRow.IsActive = IsActive;
            // 填加行并进行更新处理
            custItemTab.AddCustomerRow(custRow);
            this.CustomerAdapter.Update(custItemTab);
            CommonDataEditAction action = new CommonDataEditAction();
            action.ActionTime = DateTime.Now;
            action.ActionType = "添加";
            action.DataTableName = "客户";
            action.NewValue = "客户名称：" + custRow.CustomerName;
            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            SysLog.LogCommonDataEditAction(action);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCustomer(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, string CustomerName, string CustomerNo, int CityID, int CustomerTypeID, int ChannelTypeID, int ApplyOrganizationUnitID, int OrganizationUnitID, Boolean IsActive, int CustomerID) {
            if (ApplyOrganizationUnitID <= 0) {
                throw new ApplicationException("必须选择所属机构！");
            }
            if (OrganizationUnitID <= 0) {
                throw new ApplicationException("必须选择所属预算机构！");
            }
            // 检证"客户名称"唯一性
            int iCount = (int)this.CustomerAdapter.SearchNameByUpd(CustomerName, CustomerID);
            if (iCount > 0) {
                throw new ApplicationException("客户名称不能重复！");
            }
            CommonDataEditAction action = new CommonDataEditAction();
            action.ActionTime = DateTime.Now;
            action.ActionType = "更新";
            action.DataTableName = "客户";
            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            // 根据 CustId 查找要修改的数据
            ERS.CustomerDataTable custTab = this.CustomerAdapter.GetDataById(CustomerID);
            ERS.CustomerRow custRow = custTab[0];
            action.OldValue = "客户名称：" + custRow.CustomerName;
            custRow.CustomerName = CustomerName;
            custRow.CustomerNo = CustomerNo;
            custRow.CustomerTypeID = CustomerTypeID;
            custRow.ChannelTypeID = ChannelTypeID;
            custRow.CityID = CityID;
            if (ApplyOrganizationUnitID > 0) {
                custRow.ApplyOrganizationUnitID = ApplyOrganizationUnitID;
            }
            if (OrganizationUnitID > 0) {
                custRow.OrganizationUnitID = OrganizationUnitID;
            }
            custRow.IsActive = IsActive;
            // 更新数据
            this.CustomerAdapter.Update(custRow);
            action.NewValue = "客户名称：" + custRow.CustomerName;
            SysLog.LogCommonDataEditAction(action);
        }

        public int CustomerTotalCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "CustomerID is not null";
            }
            return (int)this.CustomerAdapter.QueryDataCount("Customer", queryExpression);
        }

        public ERS.CustomerRow GetCustomerById(int Id) {
            return this.CustomerAdapter.GetDataById(Id)[0];
        }

        public string GetCityNameById(int Id) {
            return this.CityAdapter.GetCityNameById(Id);
        }

        public string GetProvinceNameByCityId(int Id) {
            return this.ProvinceAdapter.GetNameByCityId(Id);
        }

        public ERS.CustomerDataTable GetCustomerByPositionID(int PositionID) {
            return this.CustomerAdapter.GetDataByPositionID(PositionID);
        }

        public ERS.CustomerDataTable GetCustomerByCustomerName(string CustomerName) {
            return this.CustomerAdapter.GetDataByCustomerName(CustomerName);
        }

        public ERS.CustomerDataTable GetCustomerByCustomerNo(string CustomerNo) {
            return this.CustomerAdapter.GetDataByCustomerNo(CustomerNo);
        }

        #endregion

        #region CustomerType Operate
        public ERS.CustomerTypeRow GetCustomerTypeById(int Id) {
            return this.CustomerTypeAdapter.GetDataById(Id)[0];
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.CustomerTypeDataTable GetCustomerTypePaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "CustomerTypeID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "CustomerTypeID is not null";
            }
            return this.CustomerTypeAdapter.GetCustomerTypePaged("CustomerType", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCustomerType(string CustomerTypeName, bool IsActive) {
            // 进行数据新增处理
            ERS.CustomerTypeDataTable CustomerTypeTab = new ERS.CustomerTypeDataTable();
            ERS.CustomerTypeRow CustomerTypeRow = CustomerTypeTab.NewCustomerTypeRow();
            // 进行传值
            CustomerTypeRow.CustomerTypeName = CustomerTypeName;
            CustomerTypeRow.IsActive = IsActive;
            // 填加行并进行更新处理
            CustomerTypeTab.AddCustomerTypeRow(CustomerTypeRow);
            this.CustomerTypeAdapter.Update(CustomerTypeTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCustomerType(int CustomerTypeID, string CustomerTypeName, Boolean IsActive, int UserID, int PositionID) {
            ERS.CustomerTypeDataTable CustomerTypeTab = this.CustomerTypeAdapter.GetDataById(CustomerTypeID);
            ERS.CustomerTypeRow CustomerTypeRow = CustomerTypeTab[0];
            //传值
            CustomerTypeRow.CustomerTypeName = CustomerTypeName;
            CustomerTypeRow.IsActive = IsActive;
            // 更新数据
            this.CustomerTypeAdapter.Update(CustomerTypeRow);
        }

        public int CustomerTypeTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "CustomerTypeID is not null";
            }
            return (int)this.CustomerTypeAdapter.QueryDataCount("CustomerType", queryExpression);
        }

        public ERS.CustomerTypeDataTable GetCustomerTypeByCustomerTypeName(string CustomerTypeName) {
            return this.CustomerTypeAdapter.GetDataByCustomerTypeName(CustomerTypeName);
        }

        #endregion

        #region ChannelType Operate
        public ERS.ChannelTypeRow GetChannelTypeById(int Id) {
            return this.ChannelTypeAdapter.GetDataById(Id)[0];
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.ChannelTypeDataTable GetChannelTypePaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ChannelTypeID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "ChannelTypeID is not null";
            }
            return this.ChannelTypeAdapter.GetChannelTypePaged("ChannelType", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertChannelType(string ChannelTypeName, Boolean IsActive) {
            // 进行数据新增处理
            ERS.ChannelTypeDataTable ChannelTypeTab = new ERS.ChannelTypeDataTable();
            ERS.ChannelTypeRow ChannelTypeRow = ChannelTypeTab.NewChannelTypeRow();
            // 进行传值
            ChannelTypeRow.ChannelTypeName = ChannelTypeName;
            ChannelTypeRow.IsActive = IsActive;
            // 填加行并进行更新处理
            ChannelTypeTab.AddChannelTypeRow(ChannelTypeRow);
            this.ChannelTypeAdapter.Update(ChannelTypeTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateChannelType(int ChannelTypeID, string ChannelTypeName, bool IsActive, int UserID, int PositionID) {
            ERS.ChannelTypeDataTable ChannelTypeTab = this.ChannelTypeAdapter.GetDataById(ChannelTypeID);
            ERS.ChannelTypeRow ChannelTypeRow = ChannelTypeTab[0];
            //传值
            ChannelTypeRow.ChannelTypeName = ChannelTypeName;
            ChannelTypeRow.IsActive = IsActive;
            // 更新数据
            this.ChannelTypeAdapter.Update(ChannelTypeRow);
        }

        public int ChannelTypeTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "ChannelTypeID is not null";
            }
            return (int)this.ChannelTypeAdapter.QueryDataCount("ChannelType", queryExpression);
        }

        public ERS.ChannelTypeDataTable GetChannelTypeByChannelTypeName(string ChannelTypeName) {
            return ChannelTypeAdapter.GetDataByChannelTypeName(ChannelTypeName);
        }

        #endregion

        #region PaymentType Operate
        public ERS.PaymentTypeRow GetPaymentTypeById(int Id) {
            return this.PaymentTypeAdapter.GetDataById(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.PaymentTypeDataTable GetPaymentTypePaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "PaymentTypeID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "PaymentTypeID is not null";
            }
            return this.PaymentTypeAdapter.GetPaymentTypePaged("PaymentType", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertPaymentType(string PaymentTypeName, bool IsActive) {
            // 进行数据新增处理
            ERS.PaymentTypeDataTable PaymentTypeTab = new ERS.PaymentTypeDataTable();
            ERS.PaymentTypeRow PaymentTypeRow = PaymentTypeTab.NewPaymentTypeRow();
            // 进行传值
            PaymentTypeRow.PaymentTypeName = PaymentTypeName;
            PaymentTypeRow.IsActive = IsActive;
            // 填加行并进行更新处理
            PaymentTypeTab.AddPaymentTypeRow(PaymentTypeRow);
            this.PaymentTypeAdapter.Update(PaymentTypeTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdatePaymentType(int PaymentTypeID, string PaymentTypeName, bool IsActive, int UserID, int PositionID) {
            ERS.PaymentTypeDataTable PaymentTypeTab = this.PaymentTypeAdapter.GetDataById(PaymentTypeID);
            ERS.PaymentTypeRow PaymentTypeRow = PaymentTypeTab[0];
            //传值
            PaymentTypeRow.PaymentTypeName = PaymentTypeName;
            PaymentTypeRow.IsActive = IsActive;
            // 更新数据
            this.PaymentTypeAdapter.Update(PaymentTypeRow);
        }

        public int PaymentTypeTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "PaymentTypeID is not null";
            }
            return (int)this.PaymentTypeAdapter.QueryDataCount("PaymentType", queryExpression);
        }
        #endregion

        #region ContractType  Operate
        public ERS.ContractTypeRow GetContractTypeById(int Id) {
            return this.ContractTypeAdapter.GetDataById(Id)[0];
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.ContractTypeDataTable GetContractTypePaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ContractTypeID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "ContractTypeID is not null";
            }
            return this.ContractTypeAdapter.GetContractTypePaged("ContractType ", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertContractType(string ContractTypeName, string ContractTypeMark, bool IsActive) {
            ERS.ContractTypeDataTable ContractTypeTab = new ERS.ContractTypeDataTable();
            ERS.ContractTypeRow ContractTypeRow = ContractTypeTab.NewContractTypeRow();
            ContractTypeRow.ContractTypeName = ContractTypeName;
            ContractTypeRow.ContractTypeMark = ContractTypeMark;
            ContractTypeRow.IsActive = IsActive;
            ContractTypeTab.AddContractTypeRow(ContractTypeRow);
            this.ContractTypeAdapter.Update(ContractTypeTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateContractType(int ContractTypeID, string ContractTypeName, string ContractTypeMark, bool IsActive, int UserID, int PositionID) {
            ERS.ContractTypeDataTable ContractTypeTab = this.ContractTypeAdapter.GetDataById(ContractTypeID);
            ERS.ContractTypeRow ContractTypeRow = ContractTypeTab[0];
            ContractTypeRow.ContractTypeName = ContractTypeName;
            ContractTypeRow.ContractTypeMark = ContractTypeMark;
            ContractTypeRow.IsActive = IsActive;
            this.ContractTypeAdapter.Update(ContractTypeRow);
        }

        public int ContractTypeTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "ContractTypeID is not null";
            }
            return (int)this.ContractTypeAdapter.QueryDataCount("ContractType", queryExpression);
        }
        #endregion

        #region PromotionScope  Operate
        public ERS.PromotionScopeRow GetPromotionScopeById(int Id) {
            return this.PromotionScopeAdapter.GetDataById(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.PromotionScopeDataTable GetPromotionScopePaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "PromotionScopeID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "PromotionScopeID is not null";
            }
            return this.PromotionScopeAdapter.GetPromotionScopePaged("PromotionScope ", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertPromotionScope(string PromotionScopeName, bool IsActive) {
            ERS.PromotionScopeDataTable PromotionScopeTab = new ERS.PromotionScopeDataTable();
            ERS.PromotionScopeRow PromotionScopeRow = PromotionScopeTab.NewPromotionScopeRow();
            PromotionScopeRow.PromotionScopeName = PromotionScopeName;
            PromotionScopeRow.IsActive = IsActive;
            PromotionScopeTab.AddPromotionScopeRow(PromotionScopeRow);
            this.PromotionScopeAdapter.Update(PromotionScopeTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdatePromotionScope(int PromotionScopeID, string PromotionScopeName, bool IsActive, int UserID, int PositionID) {
            ERS.PromotionScopeDataTable PromotionScopeTab = this.PromotionScopeAdapter.GetDataById(PromotionScopeID);
            ERS.PromotionScopeRow PromotionScopeRow = PromotionScopeTab[0];
            PromotionScopeRow.PromotionScopeName = PromotionScopeName;
            PromotionScopeRow.IsActive = IsActive;
            this.PromotionScopeAdapter.Update(PromotionScopeRow);
        }

        public int PromotionScopeTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "PromotionScopeID is not null";
            }
            return (int)this.PromotionScopeAdapter.QueryDataCount("PromotionScope", queryExpression);
        }
        #endregion

        #region PromotionType  Operate
        public ERS.PromotionTypeRow GetPromotionTypeById(int Id) {
            return this.PromotionTypeAdapter.GetDataById(Id)[0];
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.PromotionTypeDataTable GetPromotionTypePaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "PromotionTypeID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "PromotionTypeID is not null";
            }
            return this.PromotionTypeAdapter.GetPromotionTypePaged("PromotionType ", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertPromotionType(string PromotionTypeName, bool IsActive) {
            ERS.PromotionTypeDataTable PromotionTypeTab = new ERS.PromotionTypeDataTable();
            ERS.PromotionTypeRow PromotionTypeRow = PromotionTypeTab.NewPromotionTypeRow();
            PromotionTypeRow.PromotionTypeName = PromotionTypeName;
            PromotionTypeRow.IsActive = IsActive;
            PromotionTypeTab.AddPromotionTypeRow(PromotionTypeRow);
            this.PromotionTypeAdapter.Update(PromotionTypeTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdatePromotionType(int PromotionTypeID, string PromotionTypeName, bool IsActive, int UserID, int PositionID) {
            ERS.PromotionTypeDataTable PromotionTypeTab = this.PromotionTypeAdapter.GetDataById(PromotionTypeID);
            ERS.PromotionTypeRow PromotionTypeRow = PromotionTypeTab[0];
            PromotionTypeRow.PromotionTypeName = PromotionTypeName;
            PromotionTypeRow.IsActive = IsActive;
            this.PromotionTypeAdapter.Update(PromotionTypeRow);
        }

        public int PromotionTypeTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "PromotionTypeID is not null";
            }
            return (int)this.PromotionTypeAdapter.QueryDataCount("PromotionType", queryExpression);
        }
        #endregion

        #region ExpenseManageType Operate

        public ERS.ExpenseManageTypeRow GetExpenseManageTypeByID(int ExpenseManageTypeID) {
            return this.ExpenseManageTypeAdapter.GetDataByID(ExpenseManageTypeID)[0];
        }

        public ERS.ExpenseManageTypeDataTable GetExpenseManageType(bool IsActive = true) {
            return this.ExpenseManageTypeAdapter.GetDataByIsActive(IsActive);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.ExpenseManageTypeDataTable GetExpenseManageTypePaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ExpenseManageTypeID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "ExpenseManageTypeID is not null";
            }
            return this.ExpenseManageTypeAdapter.GetExpenseManageTypePaged("ExpenseManageType ", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertExpenseManageType(string ExpenseManageTypeName, string AccountingCode, string AccountingName, bool IsActive, int ExpenseManageCategoryID) {
            ERS.ExpenseManageTypeDataTable ExpenseManageTypeTab = new ERS.ExpenseManageTypeDataTable();
            ERS.ExpenseManageTypeRow ExpenseManageTypeRow = ExpenseManageTypeTab.NewExpenseManageTypeRow();
            ExpenseManageTypeRow.ExpenseManageTypeName = ExpenseManageTypeName;
            ExpenseManageTypeRow.AccountingCode = AccountingCode;
            ExpenseManageTypeRow.AccountingName = AccountingName;
            ExpenseManageTypeRow.IsActive = IsActive;
            ExpenseManageTypeRow.ExpenseManageCategoryID = ExpenseManageCategoryID;
            ExpenseManageTypeTab.AddExpenseManageTypeRow(ExpenseManageTypeRow);
            this.ExpenseManageTypeAdapter.Update(ExpenseManageTypeTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateExpenseManageType(int ExpenseManageTypeID, string ExpenseManageTypeName, string AccountingCode, string AccountingName, bool IsActive, int ExpenseManageCategoryID, int UserID, int PositionID) {
            ERS.ExpenseManageTypeDataTable ExpenseManageTypeTab = this.ExpenseManageTypeAdapter.GetDataByID(ExpenseManageTypeID);
            ERS.ExpenseManageTypeRow ExpenseManageTypeRow = ExpenseManageTypeTab[0];
            ExpenseManageTypeRow.ExpenseManageTypeName = ExpenseManageTypeName;
            ExpenseManageTypeRow.AccountingCode = AccountingCode;
            ExpenseManageTypeRow.AccountingName = AccountingName;
            ExpenseManageTypeRow.IsActive = IsActive;
            ExpenseManageTypeRow.ExpenseManageCategoryID = ExpenseManageCategoryID;
            this.ExpenseManageTypeAdapter.Update(ExpenseManageTypeRow);
        }

        public int ExpenseManageTypeTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "ExpenseManageTypeID is not null";
            }
            return (int)this.ExpenseManageTypeAdapter.QueryDataCount("ExpenseManageType", queryExpression);
        }
        #endregion

        #region CustomerAmountLimit Operate

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.CustomerAmountLimitDataTable GetCustAmountOperatePaged(int startRowIndex, int maximumRows, string sortExpression, int CustId) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "CustomerID Desc";
            }
            return this.CustomerAmountLimitAdapter.GetDataPeged("CustomerAmountLimit", sortExpression, startRowIndex, maximumRows, "CustomerID=" + CustId);
        }

        public int CustomerAmountLimitTotalCount(int CustId) {
            return (int)this.CustomerAmountLimitAdapter.QueryDataCount("CustomerAmountLimit", "CustomerID=" + CustId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCustAmountLimit(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int CustId, int Year, int Amount) {
            // 检证"客户金额限制"唯一性
            int iCount = (int)this.CustomerAmountLimitAdapter.SearchAmountLimitByIns(CustId, Year);
            if (iCount > 0) {
                throw new ApplicationException("在同一年份下，客户金额限制不能重复！");
            }
            // 进行数据新增处理
            ERS.CustomerAmountLimitDataTable custAmountLimitTab = new ERS.CustomerAmountLimitDataTable();
            ERS.CustomerAmountLimitRow custAmountLimitRow = custAmountLimitTab.NewCustomerAmountLimitRow();

            // 进行传值
            custAmountLimitRow.CustomerID = CustId;
            custAmountLimitRow.Year = Year;
            custAmountLimitRow.Amount = Amount;
            // 填加行并进行更新处理
            custAmountLimitTab.AddCustomerAmountLimitRow(custAmountLimitRow);
            this.CustomerAmountLimitAdapter.Update(custAmountLimitTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCustAmountLimit(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int CustomerAmountLimitID, int Year, int Amount, int CustId) {
            // 检证"客户金额限制"唯一性
            int iCount = (int)this.CustomerAmountLimitAdapter.SearchAmountLimitByUpd(CustomerAmountLimitID, Year, CustId);
            if (iCount > 0) {
                throw new ApplicationException("在同一年份下，客户金额限制不能重复！");
            }
            // 根据 CustomerAmountLimitID 查找要修改的数据
            ERS.CustomerAmountLimitDataTable CustAmountLimitTab = this.CustomerAmountLimitAdapter.GetDataById(CustomerAmountLimitID);
            ERS.CustomerAmountLimitRow CustAmountLimitRow = CustAmountLimitTab[0];

            CustAmountLimitRow.Year = Year;
            CustAmountLimitRow.Amount = Amount;

            // 更新数据
            this.CustomerAmountLimitAdapter.Update(CustAmountLimitRow);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public void DeleteCustAmountLimit(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int CustomerAmountLimitID) {
            this.CustomerAmountLimitAdapter.DeleteById(CustomerAmountLimitID);
        }
        #endregion

        #region CustomerTimesLimit Operate

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.CustomerTimesLimitDataTable GetCustTimesOperatePaged(int startRowIndex, int maximumRows, string sortExpression, int CustId) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "CustomerID Desc";
            }
            return this.CustomerTimesLimitAdapter.GetDataPaged("CustomerTimesLimit", sortExpression, startRowIndex, maximumRows, "CustomerID=" + CustId);
        }

        public int CustomerTimesLimitTotalCount(int CustId) {
            return (int)this.CustomerTimesLimitAdapter.QueryDataCount("CustomerTimesLimit", "CustomerID=" + CustId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCustTimesLimit(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int CustId, int ExpenseItemID, int Times) {
            // 检证"客户次数限制"唯一性
            int iCount = (int)this.CustomerTimesLimitAdapter.SearchTimesLimitByIns(CustId, ExpenseItemID);
            if (iCount > 0) {
                throw new ApplicationException("在同一费用项下，客户次数限制不能重复！");
            }

            // 进行数据新增处理
            ERS.CustomerTimesLimitDataTable custTimesLimitTab = new ERS.CustomerTimesLimitDataTable();
            ERS.CustomerTimesLimitRow custTimesLimitRow = custTimesLimitTab.NewCustomerTimesLimitRow();

            // 进行传值
            custTimesLimitRow.CustomerID = CustId;
            custTimesLimitRow.Times = Times;
            custTimesLimitRow.ExpenseItemID = ExpenseItemID;
            // 填加行并进行更新处理
            custTimesLimitTab.AddCustomerTimesLimitRow(custTimesLimitRow);
            this.CustomerTimesLimitAdapter.Update(custTimesLimitTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCustTimesLimit(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int CustomerTimesLimitID, int ExpenseItemID, int Times, int CustId) {
            // 检证"客户次数限制"唯一性
            int iCount = (int)this.CustomerTimesLimitAdapter.SearchTimesLimitByUpd(CustId, ExpenseItemID, CustomerTimesLimitID);
            if (iCount > 0) {
                throw new ApplicationException("在同一费用项下，客户次数限制不能重复！");
            }

            // 根据 CustomerAmountLimitID 查找要修改的数据
            ERS.CustomerTimesLimitDataTable CustTimesLimitTab = this.CustomerTimesLimitAdapter.GetDataById(CustomerTimesLimitID);
            ERS.CustomerTimesLimitRow CustTimesLimitRow = CustTimesLimitTab[0];

            CustTimesLimitRow.ExpenseItemID = ExpenseItemID;
            CustTimesLimitRow.Times = Times;

            // 更新数据
            this.CustomerTimesLimitAdapter.Update(CustTimesLimitRow);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public void DeleteCustTimesLimit(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int CustomerTimesLimitID) {
            this.CustomerTimesLimitAdapter.DeleteById(CustomerTimesLimitID);
        }
        #endregion

        #region ShopLevel Operate
        public ERS.ShopLevelRow GetShopLevelById(int Id) {
            return this.ShopLevelAdapter.GetDataById(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.ShopLevelDataTable GetShopLevelPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ShopLevelID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "ShopLevelID is not null";
            }
            return this.ShopLevelAdapter.GetShopLevelPaged("ShopLevel", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertShopLevel(string ShopLevelName, bool IsActive) {
            ERS.ShopLevelDataTable ShopLevelTab = new ERS.ShopLevelDataTable();
            ERS.ShopLevelRow ShopLevelRow = ShopLevelTab.NewShopLevelRow();
            ShopLevelRow.ShopLevelName = ShopLevelName;
            ShopLevelRow.IsActive = IsActive;
            ShopLevelTab.AddShopLevelRow(ShopLevelRow);
            this.ShopLevelAdapter.Update(ShopLevelTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateShopLevel(int ShopLevelID, string ShopLevelName, Boolean IsActive, int UserID, int PositionID) {
            ERS.ShopLevelDataTable ShopLevelTab = this.ShopLevelAdapter.GetDataById(ShopLevelID);
            ERS.ShopLevelRow ShopLevelRow = ShopLevelTab[0];
            ShopLevelRow.ShopLevelName = ShopLevelName;
            ShopLevelRow.IsActive = IsActive;
            this.ShopLevelAdapter.Update(ShopLevelRow);
        }

        public int ShopLevelTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "ShopLevelID is not null";
            }
            return (int)this.CustomerTypeAdapter.QueryDataCount("ShopLevel", queryExpression);
        }

        public ERS.ShopLevelDataTable GetShopLevelByShopLevelName(string ShopLevelName) {
            return this.ShopLevelAdapter.GetDataByShopLevelName(ShopLevelName);
        }

        #endregion

        #region ShelfType Operate
        public ERS.ShelfTypeRow GetShelfTypeById(int Id) {
            return this.ShelfTypeAdapter.GetDataById(Id)[0];
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.ShelfTypeDataTable GetShelfTypePaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ShelfTypeID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "ShelfTypeID is not null";
            }
            return this.ShelfTypeAdapter.GetShelfTypePaged("ShelfType", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertShelfType(string ShelfTypeName, bool IsActive) {
            ERS.ShelfTypeDataTable ShelfTypeTab = new ERS.ShelfTypeDataTable();
            ERS.ShelfTypeRow ShelfTypeRow = ShelfTypeTab.NewShelfTypeRow();
            ShelfTypeRow.ShelfTypeName = ShelfTypeName;
            ShelfTypeRow.IsActive = IsActive;
            ShelfTypeTab.AddShelfTypeRow(ShelfTypeRow);
            this.ShelfTypeAdapter.Update(ShelfTypeTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateShelfType(int ShelfTypeID, string ShelfTypeName, Boolean IsActive, int UserID, int PositionID) {
            ERS.ShelfTypeDataTable ShelfTypeTab = this.ShelfTypeAdapter.GetDataById(ShelfTypeID);
            ERS.ShelfTypeRow ShelfTypeRow = ShelfTypeTab[0];
            ShelfTypeRow.ShelfTypeName = ShelfTypeName;
            ShelfTypeRow.IsActive = IsActive;
            this.ShelfTypeAdapter.Update(ShelfTypeRow);
        }

        public int ShelfTypeTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "ShelfTypeID is not null";
            }
            return (int)this.ShelfTypeAdapter.QueryDataCount("ShelfType", queryExpression);
        }
        #endregion

        #region Material  Operate

        public ERS.MaterialRow GetMaterialById(int Id) {
            return this.MaterialAdapter.GetDataById(Id)[0];
        }

        public ERS.MaterialDataTable GetMaterialPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "MaterialID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "MaterialID is not null";
            }
            return this.MaterialAdapter.GetMaterialPaged("Material ", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int MaterialTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "MaterialID is not null";
            }
            return (int)this.MaterialAdapter.QueryDataCount("Material", queryExpression);
        }

        public ERS.MaterialDataTable GetActiveMaterialPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "MaterialID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "IsActive = 1";
            }
            return this.MaterialAdapter.GetMaterialPaged("Material ", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int ActiveMaterialTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "IsActive = 1";
            }
            return (int)this.MaterialAdapter.QueryDataCount("Material", queryExpression);
        }

        public void InsertMaterial(string MaterialName, decimal MaterialPrice, string UOM, int MinimumNumber, string Description, bool IsActive, string MaterialNo) {
            ERS.MaterialDataTable MaterialTab = new ERS.MaterialDataTable();
            ERS.MaterialRow MaterialRow = MaterialTab.NewMaterialRow();
            MaterialRow.MaterialName = MaterialName;
            MaterialRow.MaterialPrice = MaterialPrice;
            MaterialRow.UOM = UOM;
            MaterialRow.MinimumNumber = MinimumNumber;
            MaterialRow.Description = Description;
            MaterialRow.IsActive = IsActive;
            MaterialRow.MaterialNo = MaterialNo;
            MaterialTab.AddMaterialRow(MaterialRow);
            this.MaterialAdapter.Update(MaterialTab);
        }

        public void UpdateMaterial(int MaterialID, string MaterialName, decimal MaterialPrice, string UOM, int MinimumNumber, string Description, bool IsActive, string MaterialNo) {
            ERS.MaterialDataTable MaterialTab = this.MaterialAdapter.GetDataById(MaterialID);
            ERS.MaterialRow MaterialRow = MaterialTab[0];
            MaterialRow.MaterialName = MaterialName;
            MaterialRow.MaterialPrice = MaterialPrice;
            MaterialRow.UOM = UOM;
            MaterialRow.MinimumNumber = MinimumNumber;
            MaterialRow.Description = Description;
            MaterialRow.IsActive = IsActive;
            MaterialRow.MaterialNo = MaterialNo;
            this.MaterialAdapter.Update(MaterialRow);
        }

        #endregion

        #region Shop Operate
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.ShopDataTable GetShopPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ShopID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "ShopID is not null";
            }
            return this.ShopAdapter.GetShopPaged("Shop", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertShop(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, string ShopName, int CustomerID, int ShopLevelID, string Address, string Contacter, string Tel, string Email, bool IsActive, string ShopNo) {

            // 检证"门店名称"唯一性
            int iCount = (int)this.ShopAdapter.SearchNameByIns(ShopName, CustomerID, ShopLevelID);
            if (iCount > 0) {
                throw new ApplicationException("同一客户，同一门店等级下门店名称不能重复！");
            }

            // 进行数据新增处理
            ERS.ShopDataTable shopTab = new ERS.ShopDataTable();
            ERS.ShopRow shopRow = shopTab.NewShopRow();

            // 进行传值
            shopRow.ShopName = ShopName;
            shopRow.CustomerID = CustomerID;
            shopRow.ShopLevelID = ShopLevelID;
            shopRow.Address = Address;
            shopRow.Contacter = Contacter;
            shopRow.Tel = Tel;
            shopRow.Email = Email;
            shopRow.IsActive = IsActive;
            shopRow.ShopNo = ShopNo;
            // 填加行并进行更新处理
            shopTab.AddShopRow(shopRow);
            this.ShopAdapter.Update(shopTab);

            CommonDataEditAction action = new CommonDataEditAction();
            action.ActionTime = DateTime.Now;
            action.ActionType = "添加";
            action.DataTableName = "门店";
            action.NewValue = "门店名称：" + shopRow.ShopName;
            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            SysLog.LogCommonDataEditAction(action);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateShop(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, string ShopName, int CustomerID, int ShopLevelID, string Address, string Contacter, string Tel, string Email, int ShopID, bool IsActive, string ShopNo) {
            // 检证"客户名称"唯一性
            int iCount = (int)this.ShopAdapter.SearchNameByUpd(ShopName, ShopID, CustomerID, ShopLevelID);
            if (iCount > 0) {
                throw new ApplicationException("同一客户，同一门店等级下门店名称不能重复！");
            }

            CommonDataEditAction action = new CommonDataEditAction();
            action.ActionTime = DateTime.Now;
            action.ActionType = "更新";
            action.DataTableName = "门店";

            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;

            // 根据 ShopId 查找要修改的数据
            ERS.ShopDataTable ShopTab = this.ShopAdapter.GetDataById(ShopID);
            ERS.ShopRow ShopRow = ShopTab[0];

            action.OldValue = "门店名称：" + ShopRow.ShopName;

            ShopRow.ShopName = ShopName;
            ShopRow.CustomerID = CustomerID;
            ShopRow.ShopLevelID = ShopLevelID;
            ShopRow.Address = Address;
            ShopRow.Contacter = Contacter;
            ShopRow.Tel = Tel;
            ShopRow.Email = Email;
            ShopRow.IsActive = IsActive;
            ShopRow.ShopNo = ShopNo;
            // 更新数据
            this.ShopAdapter.Update(ShopRow);
            action.NewValue = "门店名称：" + ShopRow.ShopName;
            SysLog.LogCommonDataEditAction(action);
        }

        public int ShopTotalCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "ShopID is not null";
            }
            return (int)this.ShopAdapter.QueryDataCount("Shop", queryExpression);
        }

        public ERS.ShopRow GetShopByID(int shopID) {
            return this.ShopAdapter.GetDataById(shopID)[0];
        }
        #endregion

        #region SKU Operate
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.SKUDataTable GetSKUPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SKUName";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "SKUID is not null";
            }
            return this.SKUAdapter.GetSKUPaged("SKU", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertSKU(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int SKUCategoryID, string SKUNo, string SKUName, string Spec,
            int PackageQuantity, decimal PackagePercent, string SKUCostCenter, Boolean IsActive, decimal CostPrice, string Taste) {

            // 进行数据新增处理
            ERS.SKUDataTable SKUTab = new ERS.SKUDataTable();
            ERS.SKURow SKURow = SKUTab.NewSKURow();

            // 进行传值
            SKURow.SKUName = SKUName;
            SKURow.SKUNo = SKUNo;
            SKURow.Spec = Spec;
            SKURow.PackageQuantity = PackageQuantity;
            SKURow.PackagePercent = PackagePercent;
            SKURow.IsActive = IsActive;
            SKURow.SKUCategoryID = SKUCategoryID;
            SKURow.SKUCostCenter = SKUCostCenter == null ? "" : SKUCostCenter;
            SKURow.CostPrice = CostPrice;
            SKURow.Taste = Taste;
            // 填加行并进行更新处理
            SKUTab.AddSKURow(SKURow);
            this.SKUAdapter.Update(SKUTab);

            CommonDataEditAction action = new CommonDataEditAction();
            action.ActionTime = DateTime.Now;
            action.ActionType = "添加";
            action.DataTableName = "产品";
            action.NewValue = "产品名称：" + SKURow.SKUName;
            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;
            SysLog.LogCommonDataEditAction(action);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateSKU(AuthorizationDS.StuffUserRow stuffUser, AuthorizationDS.PositionRow position, int SKUCategoryID, string SKUNo, string SKUName, string Spec,
            int PackageQuantity, decimal PackagePercent, string SKUCostCenter, Boolean IsActive, int SKUID, decimal CostPrice, string Taste) {

            CommonDataEditAction action = new CommonDataEditAction();
            action.ActionTime = DateTime.Now;
            action.ActionType = "更新";
            action.DataTableName = "产品";

            action.StuffId = stuffUser.StuffId;
            action.StuffName = stuffUser.StuffName;


            // 根据 SKUID 查找要修改的数据
            ERS.SKUDataTable SKUTab = this.SKUAdapter.GetDataById(SKUID);
            ERS.SKURow SKURow = SKUTab[0];

            action.OldValue = "产品名称：" + SKURow.SKUName;

            SKURow.SKUName = SKUName;
            SKURow.SKUNo = SKUNo;
            SKURow.Spec = Spec;
            SKURow.PackageQuantity = PackageQuantity;
            SKURow.PackagePercent = PackagePercent;
            SKURow.IsActive = IsActive;
            SKURow.SKUCategoryID = SKUCategoryID;
            SKURow.SKUCostCenter = SKUCostCenter == null ? "" : SKUCostCenter;
            SKURow.CostPrice = CostPrice;
            SKURow.Taste = Taste;
            // 更新数据
            this.SKUAdapter.Update(SKURow);
            action.NewValue = "产品名称：" + SKURow.SKUName;
            SysLog.LogCommonDataEditAction(action);
        }

        public ERS.SKURow GetSKUById(int SKUId) {
            return this.SKUAdapter.GetDataById(SKUId)[0];
        }

        public int SKUTotalCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "SKUID is not null";
            }
            return (int)this.ShopAdapter.QueryDataCount("SKU", queryExpression);
        }

        public decimal GetSKUPriceByCustomerTypeID(int SKUID, int CustomerTypeID) {
            if (this.SKUAdapter.GetSKUPriceByCustomerTypeID(SKUID, CustomerTypeID) == null) {
                return 0;
            } else {
                return (decimal)this.SKUAdapter.GetSKUPriceByCustomerTypeID(SKUID, CustomerTypeID);
            }
        }

        public decimal GetSKUPriceByCustomerID(int SKUID, int CustomerID) {
            ERS.CustomerRow customer = this.GetCustomerById(CustomerID);
            if (this.SKUAdapter.GetSKUPriceByCustomerTypeID(SKUID, customer.CustomerTypeID) == null) {
                return 0;
            } else {
                return (decimal)this.SKUAdapter.GetSKUPriceByCustomerTypeID(SKUID, customer.CustomerTypeID);
            }
        }

        #endregion

        #region SKUPrice Operate
        public ERS.SKUPriceRow GetSKUPriceById(int Id) {
            return this.SKUPriceAdapter.GetDataById(Id)[0];
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.SKUPriceDataTable GetSKUPricePaged(int SKUId, int startRowIndex, int maximumRows, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SKUPriceID Desc";
            }
            return this.SKUPriceAdapter.GetSKUPricePaged("SKUPrice", sortExpression, startRowIndex, maximumRows, "SKUID=" + SKUId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertSKUPrice(int CustomerTypeID, decimal? Price, int SKUId, int UserID, int PositionID) {
            // 检证"费用小类"唯一性
            int iCount = (int)this.SKUPriceAdapter.SearchPriceByIns(SKUId, CustomerTypeID);
            if (iCount > 0) {
                throw new ApplicationException("在同一客户类型下，产品价格不能重复！");
            }

            ERS.SKUPriceDataTable SKUPriceTab = new ERS.SKUPriceDataTable();
            ERS.SKUPriceRow SKUPriceRow = SKUPriceTab.NewSKUPriceRow();
            SKUPriceRow.CustomerTypeID = CustomerTypeID;
            SKUPriceRow.SKUID = SKUId;
            SKUPriceRow.Price = Price.GetValueOrDefault();
            SKUPriceTab.AddSKUPriceRow(SKUPriceRow);
            this.SKUPriceAdapter.Update(SKUPriceTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateSKUPrice(int SKUPriceID, int CustomerTypeID, int SKUId, decimal? Price, int UserID, int PositionID) {
            // 检证"产品价格"唯一性

            int iCount = (int)this.SKUPriceAdapter.SearchPriceByUpd(SKUPriceID, CustomerTypeID, SKUId);
            if (iCount > 0) {
                throw new ApplicationException("在同一客户类型下，产品价格不能重复！");
            }
            ERS.SKUPriceDataTable SKUPriceTab = this.SKUPriceAdapter.GetDataById(SKUPriceID);
            ERS.SKUPriceRow SKUPriceRow = SKUPriceTab[0];
            SKUPriceRow.Price = Price.GetValueOrDefault();
            SKUPriceRow.CustomerTypeID = CustomerTypeID;
            this.SKUPriceAdapter.Update(SKUPriceRow);
        }

        public void DeleteSKUPrice(int SKUPriceID, int UserID, int PositionID) {
            this.SKUPriceAdapter.DeleteById(SKUPriceID);
        }

        public int SKUPriceTotalCount(int SKUId) {
            return (int)this.SKUPriceAdapter.QueryDataCount("SKUPrice", "SKUID=" + SKUId);
        }
        #endregion

        #region SKUCategory Operate
        public ERS.SKUCategoryRow GetSKUCategoryById(int Id) {
            return this.SKUCategoryAdapter.GetDataById(Id)[0];
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.SKUCategoryDataTable GetSKUCategoryPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SKUCategoryID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "SKUCategoryID is not null";
            }
            return this.SKUCategoryAdapter.GetSKUCategoryPaged("SKUCategory", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertSKUCategory(string SKUCategoryName, bool IsActive) {
            ERS.SKUCategoryDataTable SKUCategoryTab = new ERS.SKUCategoryDataTable();
            ERS.SKUCategoryRow SKUCategoryRow = SKUCategoryTab.NewSKUCategoryRow();
            SKUCategoryRow.SKUCategoryName = SKUCategoryName;
            SKUCategoryRow.IsActive = IsActive;
            SKUCategoryTab.AddSKUCategoryRow(SKUCategoryRow);
            this.SKUCategoryAdapter.Update(SKUCategoryTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateSKUCategory(int SKUCategoryID, string SKUCategoryName, bool IsActive, int UserID, int PositionID) {
            ERS.SKUCategoryDataTable SKUCategoryTab = this.SKUCategoryAdapter.GetDataById(SKUCategoryID);
            ERS.SKUCategoryRow SKUCategoryRow = SKUCategoryTab[0];
            SKUCategoryRow.SKUCategoryName = SKUCategoryName;
            SKUCategoryRow.IsActive = IsActive;
            this.SKUCategoryAdapter.Update(SKUCategoryRow);
        }

        public int SKUCategoryTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "SKUCategoryID is not null";
            }
            return (int)this.SKUCategoryAdapter.QueryDataCount("SKUCategory", queryExpression);
        }
        #endregion

        #region ShopView  Operate

        public ERS.ShopViewRow GetShopViewById(int ShopId) {
            return this.ShopViewAdapter.GetDataByShopID(ShopId)[0];
        }

        public ERS.ShopViewDataTable GetShopViewPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ShopName Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "ShopID is not null";
            }
            return this.ShopViewAdapter.GetPagedData("ShopView ", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int ShopViewTotalCount(string queryExpression) {
            if (queryExpression == null || queryExpression == "") {
                queryExpression = "ShopID is not null";
            }
            return (int)this.ShopViewAdapter.QueryDataCount("ShopView", queryExpression);
        }
        #endregion

        #region Bulletin Operate


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.BulletinDataTable GetBulletin() {
            return this.BulletinTableAdapter.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.BulletinDataTable GetBulletinById(int BulletinId) {
            return this.BulletinTableAdapter.GetDataByID(BulletinId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertBulletin(string bulletinTitle, string bulletinContent, bool isHot, string attachFileName, string realAttachFileName) {
            this.BulletinTableAdapter.Insert(bulletinTitle, bulletinContent, "", DateTime.Now, true, isHot, attachFileName, realAttachFileName);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateBulletin(int bulletinId, string bulletinTitle, string bulletinContent, bool isHot, bool isActive, string attachFileName, string realAttachFileName) {

            ERS.BulletinDataTable bulletins = this.BulletinTableAdapter.GetDataByID(bulletinId);

            ERS.BulletinRow bulletinRow = bulletins[0];
            bulletinRow.BulletinTitle = bulletinTitle;
            bulletinRow.BulletinContent = bulletinContent;
            bulletinRow.CreateTime = DateTime.Now;
            bulletinRow.IsActive = isActive;
            bulletinRow.IsHot = isHot;
            bulletinRow.AttachFileName = attachFileName;
            bulletinRow.RealAttachFileName = realAttachFileName;
            this.BulletinTableAdapter.Update(bulletinRow);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public void DeleteBulletin(int bulletinId) {
            this.BulletinTableAdapter.DeleteByID(bulletinId);
        }

        // 获取总行数
        public int TotalCount(string queryExpression) {

            return (int)this.BulletinTableAdapter.QueryDataCount("Bulletin", queryExpression);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.BulletinDataTable GetPage(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "CreateTime desc";
            }
            return this.BulletinTableAdapter.GetPagedData("Bulletin", sortExpression, startRowIndex, maximumRows, queryExpression);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.BulletinDataTable GetPageInActive(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "IsHot Desc,CreateTime desc";
            }
            queryExpression = "IsActive = 1";
            return this.BulletinTableAdapter.GetPagedData("Bulletin", sortExpression, startRowIndex, maximumRows, queryExpression);
        }
        #endregion

        #region  RejectReason Operate


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ERS.RejectReasonDataTable GetRejectReason() {
            return this.RejectReasonAdapter.GetData();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ERS.RejectReasonDataTable GetRejectReasonById(int id) {
            return this.RejectReasonAdapter.GetDataById(id);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public bool InsertRejectReason(string RejectReasonTitle, int RejectReasonIndex, string RejectReasonContent, bool IsActive) {
            int rowsAffected = 0;

            // 进行数据新增处理
            ERS.RejectReasonDataTable tab = new ERS.RejectReasonDataTable();
            ERS.RejectReasonRow row = tab.NewRejectReasonRow();

            try {
                // 进行传值
                row.RejectReasonTitle = RejectReasonTitle;
                row.RejectReasonIndex = RejectReasonIndex;
                row.RejectReasonContent = RejectReasonContent;
                row.IsActive = IsActive;
                // 填加行并进行更新处理
                tab.AddRejectReasonRow(row);
                rowsAffected = this.RejectReasonAdapter.Update(tab);
            } catch (Exception e) {
                // put errors 
                throw e;
            }
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public bool UpdateRejectReason(string RejectReasonTitle, int RejectReasonId, int RejectReasonIndex, string RejectReasonContent, bool IsActive) {
            int rowsAffected = 0;
            ERS.RejectReasonDataTable tab = this.RejectReasonAdapter.GetDataById(RejectReasonId);
            if (tab.Count == 0) {
                return false;
            }
            try {
                // 进行传值
                ERS.RejectReasonRow row = tab[0];
                row.RejectReasonTitle = RejectReasonTitle;
                row.RejectReasonIndex = RejectReasonIndex;
                row.RejectReasonContent = RejectReasonContent;
                row.IsActive = IsActive;

                // 更新数据
                rowsAffected = this.RejectReasonAdapter.Update(row);
            } catch (Exception e) {
                throw e;
            }
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public bool DeleteById(int RejectReasonId) {
            int rowsAffected = 0;
            try {
                rowsAffected = this.RejectReasonAdapter.DeleteById(RejectReasonId);
            } catch (Exception e) {
                throw e;
            }
            return rowsAffected == 1;
        }

        public ERS.RejectReasonDataTable GetRejectReasonPaged(int startRowIndex, int maximumRows, string sortExpression) {
            return this.RejectReasonAdapter.GetPagedData("RejectReason", sortExpression, startRowIndex, maximumRows, "1=1");
        }

        public int QueryTotalCount() {
            return (int)this.RejectReasonAdapter.QueryDataCount("RejectReason", "1=1");
        }

        #endregion

        #region ApplyYear Operate
        public ERS.ApplyYearRow ApplyYearById(int Id) {
            return this.ApplyYearTableAdapter.GetDataByID(Id)[0];
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.ApplyYearDataTable GetApplyYearPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ApplyYearID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "ApplyYearID is not null";
            }
            return this.ApplyYearTableAdapter.GetApplyYearPagedData("ApplyYear", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertApplyYear(int ApplyYear) {
            // 进行数据新增处理
            ERS.ApplyYearDataTable ApplyYearTab = new ERS.ApplyYearDataTable();
            ERS.ApplyYearRow ApplyYearRow = ApplyYearTab.NewApplyYearRow();
            // 进行传值
            ApplyYearRow.ApplyYear = ApplyYear;

            // 填加行并进行更新处理
            ApplyYearTab.AddApplyYearRow(ApplyYearRow);
            this.ApplyYearTableAdapter.Update(ApplyYearTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateApplyYear(int ApplyYearID, int ApplyYear) {
            ERS.ApplyYearDataTable ApplyYearTab = this.ApplyYearTableAdapter.GetDataByID(ApplyYearID);
            ERS.ApplyYearRow ApplyYearRow = ApplyYearTab[0];
            //传值
            ApplyYearRow.ApplyYear = ApplyYear;

            // 更新数据
            this.ApplyYearTableAdapter.Update(ApplyYearRow);
        }

        public int ApplyYearTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "ApplyYearID is not null";
            }
            return (int)this.ApplyYearTableAdapter.QueryDataCount("ApplyYear", queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public bool DeleteApplyYearById(int ApplyYearId) {

            int rowsAffected = 0;
            try {
                rowsAffected = this.ApplyYearTableAdapter.DeleteByID(ApplyYearId);
            } catch (Exception e) {
                throw e;
            }
            return rowsAffected == 1;
        }

        public bool IsValidApplyYear(int FinaceApplyYear) {
            int count = (int)this.ApplyYearTableAdapter.QueryCountByFinanceApplyYear(FinaceApplyYear);
            if (count == 0) {
                return false;
            } else {
                return true;
            }
        }

        #endregion

        #region ReimbursePeriod Operate
        public ERS.ReimbursePeriodRow GetReimbursePeriodById(int Id) {
            return this.ReimbursePeriodTableAdapter.GetDataByID(Id)[0];
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.ReimbursePeriodDataTable GetReimbursePeriodPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ReimbursePeriodID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "ReimbursePeriodID is not null";
            }
            return this.ReimbursePeriodTableAdapter.GetReimbursePeriodPagedData("ReimbursePeriod", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertReimbursePeriod(DateTime ReimbursePeriod) {

            // 进行数据新增处理
            ERS.ReimbursePeriodDataTable ReimbursePeriodTab = new ERS.ReimbursePeriodDataTable();
            ERS.ReimbursePeriodRow ReimbursePeriodRow = ReimbursePeriodTab.NewReimbursePeriodRow();
            // 进行传值
            ReimbursePeriodRow.ReimbursePeriod = ReimbursePeriod;

            // 填加行并进行更新处理
            ReimbursePeriodTab.AddReimbursePeriodRow(ReimbursePeriodRow);
            this.ReimbursePeriodTableAdapter.Update(ReimbursePeriodTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateReimbursePeriod(int ReimbursePeriodID, string ReimbursePeriod) {
            DateTime DPeriod = DateTime.Parse(ReimbursePeriod.Substring(0, 4) + "-" + ReimbursePeriod.Substring(4, 2) + "-01");
            ERS.ReimbursePeriodDataTable ReimbursePeriodTab = this.ReimbursePeriodTableAdapter.GetDataByID(ReimbursePeriodID);
            ERS.ReimbursePeriodRow ReimbursePeriodRow = ReimbursePeriodTab[0];
            //传值
            ReimbursePeriodRow.ReimbursePeriod = DPeriod;

            // 更新数据
            this.ReimbursePeriodTableAdapter.Update(ReimbursePeriodRow);
        }

        public int ReimbursePeriodTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "ReimbursePeriodID is not null";
            }
            return (int)this.ReimbursePeriodTableAdapter.QueryDataCount("ReimbursePeriod", queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public bool DeleteReimbursePeriodById(int ReimbursePeriodId) {

            int rowsAffected = 0;
            try {
                rowsAffected = this.ReimbursePeriodTableAdapter.DeleteByID(ReimbursePeriodId);
            } catch (Exception e) {
                throw e;
            }
            return rowsAffected == 1;
        }
        #endregion

        #region CostCenter  Operate
        public ERS.CostCenterRow GetCostCenterById(int Id) {
            return this.CostCenterAdapter.GetDataByID(Id)[0];
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.CostCenterDataTable GetCostCenterPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "CostCenterID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "CostCenterID is not null";
            }
            return this.CostCenterAdapter.GetCostCenterPaged("CostCenter ", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public void InsertCostCenter(string CostCenterName, string CostCenterCode, bool IsActive) {
            ERS.CostCenterDataTable CostCenterTab = new ERS.CostCenterDataTable();
            ERS.CostCenterRow CostCenterRow = CostCenterTab.NewCostCenterRow();
            CostCenterRow.CostCenterName = CostCenterName;
            CostCenterRow.CostCenterCode = CostCenterCode;
            CostCenterRow.IsActive = IsActive;
            CostCenterTab.AddCostCenterRow(CostCenterRow);
            this.CostCenterAdapter.Update(CostCenterTab);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateCostCenter(int CostCenterID, string CostCenterName, string CostCenterCode, bool IsActive, int UserID, int PositionID) {
            ERS.CostCenterDataTable CostCenterTab = this.CostCenterAdapter.GetDataByID(CostCenterID);
            ERS.CostCenterRow CostCenterRow = CostCenterTab[0];
            CostCenterRow.CostCenterName = CostCenterName;
            CostCenterRow.CostCenterCode = CostCenterCode;
            CostCenterRow.IsActive = IsActive;
            this.CostCenterAdapter.Update(CostCenterRow);
        }

        public int CostCenterTotalCount(string queryExpression) {
            if (queryExpression == "") {
                queryExpression = "CostCenterID is not null";
            }
            return (int)this.CostCenterAdapter.QueryDataCount("CostCenter", queryExpression);
        }
        #endregion

        #region ProxyReimburse Operate

        public ERS.ProxyReimburseDataTable GetProxyReimbursePaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "ID Desc";
            }
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "ID is not null";
            }
            return this.ProxyReimburseAdapter.GetPagedData("ProxyReimburse", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public void InsertProxyReimburse(int UserID, int ProxyUserID, DateTime EndDate) {

            ERS.ProxyReimburseDataTable table = new ERS.ProxyReimburseDataTable();
            ERS.ProxyReimburseRow row = table.NewProxyReimburseRow();
            // 进行传值
            row.UserID = UserID;
            row.ProxyUserID = ProxyUserID;
            row.EndDate = EndDate;
            table.AddProxyReimburseRow(row);
            this.ProxyReimburseAdapter.Update(table);
        }

        public void DeleteProxyReimburseByID(int ID) {
            this.ProxyReimburseAdapter.DeleteByID(ID);
        }

        public int ProxyReimburseTotalCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "ID is not null";
            }
            return (int)this.ProxyReimburseAdapter.QueryDataCount("ProxyReimburse", queryExpression);
        }

        public ERS.ProxyReimburseRow GetProxyReimburseByID(int ID) {
            return this.ProxyReimburseAdapter.GetDataByID(ID)[0];
        }

        public ERS.ProxyReimburseDataTable GetProxyReimburseByParameter(int UserID, int ProxyUserID, DateTime SubmitDate) {
            return this.ProxyReimburseAdapter.GetProxyDataByParameter(UserID, ProxyUserID, SubmitDate);
        }

        #endregion

        #region AccruedPeriod Operate

        public ERS.AccruedPeriodRow GetAccruedPeriodByID(int AccruedPeriodID) {
            return this.AccruedPeriodTableAdapter.GetDataByID(AccruedPeriodID)[0];
        }

        public ERS.AccruedPeriodDataTable GetAccruedPeriod() {
            return this.AccruedPeriodTableAdapter.GetData();
        }

        public void InsertAccruedPeriod(DateTime AccruedPeriod) {
            ERS.AccruedPeriodDataTable AccruedPeriodTab = new ERS.AccruedPeriodDataTable();
            ERS.AccruedPeriodRow AccruedPeriodRow = AccruedPeriodTab.NewAccruedPeriodRow();
            AccruedPeriodRow.AccruedPeriod = AccruedPeriod;
            AccruedPeriodTab.AddAccruedPeriodRow(AccruedPeriodRow);
            this.AccruedPeriodTableAdapter.Update(AccruedPeriodTab);
        }

        public void DeleteAccruedPeriodById(int AccruedPeriodId) {
            this.AccruedPeriodTableAdapter.DeleteByID(AccruedPeriodId);
        }

        #endregion

        #region EmailHistory Operate
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public ERS.EmailHistoryDataTable GetEmailHistoryPaged(int startRowIndex, int maximumRows, string queryExpression, string sortExpression) {
            return this.TAEmailHistory.GetDataPaged("EmailHistory", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryEmailHistoryCount(string queryExpression) {
            return (int)this.TAEmailHistory.QueryDataCount("EmailHistory", queryExpression);
        }
        #endregion

    }
}
