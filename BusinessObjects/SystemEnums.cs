using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects {
    public class SystemEnums {

        public enum FlowRuleType {
            AmountJudge = 1 //金额判断
        }


        /// <summary>
        /// 流程状态
        /// </summary>
        public enum FlowNodeStatus {
            Wait = 0,//还未执行
            InTurn = 1,//当前待执行
            Pass = 2,//通过
            Reject = 3 //打回
        }

        /// <summary>
        /// 流程节点操作类型
        /// </summary>
        public enum FlowNodeOperateType {
            Apply = 1,//申请
            Approval = 2,//审批
            Report = 3,//回栏
            Modify = 4 //退回待修改
        }

        public enum RejectActionType {
            GoOn = 1,//继续流转不退回
            Reject = 2 //退回到申请人
        }


        public enum BusinessUseCase {
            CostCenter = 304,
            RejectReason = 307,
            Bulletin = 308,
            Province = 371,
            ExpenseCategory = 372,
            ExpenseItem = 373,
            Customer = 374,
            ShopManage = 375,
            SKU = 376,
            ChannelType = 377,
            ContractType = 378,
            CustomerType = 379,
            PaymentType = 380,
            SKUCategory = 381,
            ShopLevel = 382,
            ShelfType = 383,
            Material = 384,
            PromotionScope = 385,
            PromotionType = 386,
            ApplyYear = 387,
            ReimbursePeriod = 388,
            ExpenseManageType = 389,
            AccruedPeriod = 390,
            BudgetManageFee = 111,
            BudgetSalesFee = 112,
            FormApply = 61,
            FormReimburse = 62,
            FormContract = 71,
            FormMaterial = 72,
            FormPersonalReimburse = 73,
            FormBugetAllocation = 74,
            FormTravelApply = 75,
            FormTravelReimburse = 76,
            DeliveryInfo = 402,
            ProxyReimburse = 401,
            ProxyBusiness = 510,
            SalesTotaFeeForFinanceReport = 701,
            SalesTotaFeeReport = 702,
            SalesReimburseRateForFinanceReport = 703,
            SalesReimburseRateReport = 704,
            SalesReimburseDetailReport = 705,
            SalesReimburseDeliveryReport = 706,
            SalesAccruedTotalFeeReport = 707,
            PositionType = 506,
            FlowParticipant = 507,
            EmailHistory = 508,
            BatchPrint = 509,
            DeliveryComplete = 63
        }

        public enum UseCase {
            UserManage = 502,
            SystemRoleManage = 503,
            OrganizationManage = 501,
            PositionAuthorization = 504,
            OperateScope = 505
        }

        /// <summary>
        /// 单据状态
        /// </summary>
        public enum FormStatus {
            Draft = 0,//草稿
            Awaiting = 1,//待审批
            ApproveCompleted = 2,//审批完成
            Rejected = 3,//退回待修改            
            Scrap = 4 //作废
        }

        /// <summary>
        /// 组织机构类型
        /// </summary>
        public enum OrganizationUnitType {
            Filiale = 1, //分公司
            Department = 6, //部门
            Office = 3 //科室
        }

        /// <summary>
        /// 业务操作
        /// </summary>
        public enum OperateEnum {
            FillForm = 1000, //填单
            Approval = 2000, //审批
            View = 3000, //查看
            Print = 4000, //打印凭证
            Query = 5000, //查询
            Manage = 6000, //维护
            Import = 7000,//导入
            Scrap = 8000,//作废
            Other = 9000
        }

        /// <summary>
        /// 表单类型
        /// </summary>
        public enum PageType {
            PromotionApply = 0,//促销
            GeneralApply = 1,//非促销
            RebateApply = 2,//变动
            MaterialApply = 3,//广宣物资
            ContractApply = 4,//合同申请
            PersonalReimburseApply = 5,//个人费用报销
            ReimburseMoneyApply = 6,//方案报销
            ReimburseGoodsApply = 7,//方案报销
            BudgetAllocationApply = 8,//预算调拨申请
            PromotionApplyExecute = 9,//促销方案执行
            GeneralApplyExecute = 10,//非促销方案执行
            TravelApply = 11,//出差申请
            TravelReimburse = 12//出差报销
        }

        public enum FormType {
            SalesApply = 1,//方案申请            
            ContractApply = 2, //合同申请
            MaterialApply = 3, //广宣物资申请
            PersonalReimburseApply = 4,//个人费用报销申请
            ReimburseApply = 5,//报销申请
            BudgetAllocationApply = 6,//预算调拨申请
            TravelApply = 7//出差申请
        }

        public enum PromotionPriceType {
            Promotion = 1,//促销价格            
            Give = 2 //买送
        }
        /// <summary>
        /// 单据状态
        /// </summary>
        public enum ContractType {
            Purchase = 3,//采购部门
            Market = 4,//市场部门
            Sale = 5,//销售部门
            Admin = 6,//人事行政部门
            Financial = 7, //财务部门
            Facility = 8//厂务部门
        }

        /// <summary>
        /// 调拨类型
        /// </summary>
        public enum AllocationType {
            In = 1,//预算调入
            Out = 2//预算调出
        }

        public enum PaymentType {
            ZiJin = 1,//资金
            ZhangKou = 2,//帐扣
            PiaoKou = 3,//票扣
            ShiWu = 4//实物
        }

        public enum ReimburseRequirements {
            Picture = 1,
            Agreement = 2,
            DeliveryOrder = 4,
            Contract = 8,
            DM=16,
            Other = 32
        }

        public enum ManageReimburseType {
            PersonalReimburse = 1,
            TravelReimburse = 2
        }
    }
}
