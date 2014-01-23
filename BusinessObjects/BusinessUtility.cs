using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.AuthorizationDSTableAdapters;

namespace BusinessObjects {
    /// <summary>
    /// 用于业务层的便利方法
    /// </summary>
    public class BusinessUtility {

        /// <summary>
        /// 根据业务模块和业务操作获取BusinessOperateId
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
