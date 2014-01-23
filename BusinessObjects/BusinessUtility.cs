using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.AuthorizationDSTableAdapters;

namespace BusinessObjects {
    /// <summary>
    /// ����ҵ���ı�������
    /// </summary>
    public class BusinessUtility {

        /// <summary>
        /// ����ҵ��ģ���ҵ�������ȡBusinessOperateId
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="operate"></param>
        /// <returns></returns>
        public static int GetBusinessOperateId(SystemEnums.UseCase useCase, SystemEnums.OperateEnum operate) {
            return (int)operate + (int)useCase;
        }
        public static int GetBusinessOperateId(SystemEnums.BusinessUseCase useCase, SystemEnums.OperateEnum operate) {
            return (int)operate + (int)useCase;
        }
        public static int GetBusinessOperateId(SystemEnums.FormType useCase, SystemEnums.OperateEnum operate) {
            return (int)operate + (int)useCase;
        }
    }
}
