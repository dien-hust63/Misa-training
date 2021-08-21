using Microsoft.AspNetCore.Http;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Services
{
    public interface ICustomerService:IBaseService<Customer>
    {
        /// <summary>
        /// Lọc và phân trang cho dữ liệu khách hàng
        /// </summary>
        /// <param name="searchData">chuỗi tìm kiếm</param>
        /// <param name="customerGroupId">Id nhóm khách hàng</param>
        /// <param name="pageIndex">chỉ số trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(20/8/2021)
        /// ModifiedBy: nvdien(20/8/2021)
        ServiceResult GetCustomerFilterPaging(string searchData, Guid? customerGroupId, int pageIndex, int pageSize);

        ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken);
    }
}
