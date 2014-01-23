using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects {
    public class SystemEnums {

        public enum FlowRuleType {
            AmountJudge = 1 //����ж�
        }


        /// <summary>
        /// ����״̬
        /// </summary>
        public enum FlowNodeStatus {
            Wait = 0,//��δִ��
            InTurn = 1,//��ǰ��ִ��
            Pass = 2,//ͨ��
            Reject = 3 //���
        }

        /// <summary>
        /// ���̽ڵ��������
        /// </summary>
        public enum FlowNodeOperateType {
            Apply = 1,//����
            Approval = 2,//����
            Report = 3,//����
            Modify = 4 //�˻ش��޸�
        }

        public enum RejectActionType {
            GoOn = 1,//������ת���˻�
            Reject = 2 //�˻ص�������
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
        /// ����״̬
        /// </summary>
        public enum FormStatus {
            Draft = 0,//�ݸ�
            Awaiting = 1,//������
            ApproveCompleted = 2,//�������
            Rejected = 3,//�˻ش��޸�            
            Scrap = 4 //����
        }

        /// <summary>
        /// ��֯��������
        /// </summary>
        public enum OrganizationUnitType {
            Filiale = 1, //�ֹ�˾
            Department = 6, //����
            Office = 3 //����
        }

        /// <summary>
        /// ҵ�����
        /// </summary>
        public enum OperateEnum {
            FillForm = 1000, //�
            Approval = 2000, //����
            View = 3000, //�鿴
            Print = 4000, //��ӡƾ֤
            Query = 5000, //��ѯ
            Manage = 6000, //ά��
            Import = 7000,//����
            Scrap = 8000,//����
            Other = 9000
        }

        /// <summary>
        /// ������
        /// </summary>
        public enum PageType {
            PromotionApply = 0,//����
            GeneralApply = 1,//�Ǵ���
            RebateApply = 2,//�䶯
            MaterialApply = 3,//��������
            ContractApply = 4,//��ͬ����
            PersonalReimburseApply = 5,//���˷��ñ���
            ReimburseMoneyApply = 6,//��������
            ReimburseGoodsApply = 7,//��������
            BudgetAllocationApply = 8,//Ԥ���������
            PromotionApplyExecute = 9,//��������ִ��
            GeneralApplyExecute = 10,//�Ǵ�������ִ��
            TravelApply = 11,//��������
            TravelReimburse = 12//�����
        }

        public enum FormType {
            SalesApply = 1,//��������            
            ContractApply = 2, //��ͬ����
            MaterialApply = 3, //������������
            PersonalReimburseApply = 4,//���˷��ñ�������
            ReimburseApply = 5,//��������
            BudgetAllocationApply = 6,//Ԥ���������
            TravelApply = 7//��������
        }

        public enum PromotionPriceType {
            Promotion = 1,//�����۸�            
            Give = 2 //����
        }
        /// <summary>
        /// ����״̬
        /// </summary>
        public enum ContractType {
            Purchase = 3,//�ɹ�����
            Market = 4,//�г�����
            Sale = 5,//���۲���
            Admin = 6,//������������
            Financial = 7, //������
            Facility = 8//������
        }

        /// <summary>
        /// ��������
        /// </summary>
        public enum AllocationType {
            In = 1,//Ԥ�����
            Out = 2//Ԥ�����
        }

        public enum PaymentType {
            ZiJin = 1,//�ʽ�
            ZhangKou = 2,//�ʿ�
            PiaoKou = 3,//Ʊ��
            ShiWu = 4//ʵ��
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
