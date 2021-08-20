using Misa.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces
{
    public interface IEmployeeService:IBaseSerice<Employee>
    {
        /// <summary>
        /// Lọc và phân trang cho dữ liệu nhân viên
        /// </summary>
        /// <param name="searchData">chuỗi tìm kiếm</param>
        /// <param name="departmentId">Id phòng ban</param>
        /// <param name="positionId">id rị trí</param>
        /// <param name="pageIndex">chỉ số trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(20/8/2021)
        /// ModifiedBy: nvdien(20/8/2021)
        ServiceResult GetEmployeeFilterPaging(string searchData, Guid? departmentId, Guid? positionId, int pageIndex, int pageSize);

        /// <summary>
        /// Sinh mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: nvdien(20/8/2021)
        /// ModifiedBy: nvdien(20/8/2021)
        ServiceResult GetNewEmployeeCode();
    }
}
