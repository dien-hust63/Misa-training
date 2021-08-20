using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
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
        object GetEmployeeFilterPaging(string searchData, Guid? departmentId, Guid? positionId, int pageIndex, int pageSize);

        /// <summary>
        /// Sinh mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        /// CreatedBy: nvdien(20/8/2021)
        /// ModifiedBy: nvdien(20/8/2021)
        string GetNewEmployeeCode();
    }
}
