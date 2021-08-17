using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Services
{
    public interface ICustomerService:IBaseService<Customer>
    {
        /// <summary>
        /// Láy dữ liệu phân trang
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021) 
        ServiceResult GetCustomerPaging(int limit, int offset);

        /// <summary>
        /// Lấy thông tin khách hàng theo nhóm khách hàng
        /// </summary>
        /// <param name="groupId">Id nhóm khách hàng</param>
        /// <returns></returns>
        /// /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021) 
        ServiceResult GetCustomerByGroup(Guid groupId);
    }
}
