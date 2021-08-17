using Misa.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// lấy danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns>Service Result</returns>
        /// CreatedBy: nvdien(13/8/2021)
        /// ModifiedBy: nvdien(13/8/2021)
        ServiceResult GetAllEmployees();
        
        /// <summary>
        /// lấy thông tin nhân viên
        /// </summary>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <returns>Service Result</returns>
        /// CreatedBy: nvdien(13/8/2021)
        /// ModifiedBy: nviden(13/8/2021)
        ServiceResult GetEmployeeById(Guid employeeId);

        /// <summary>
        /// thêm nhân viên
        /// </summary>
        /// <param name="employee">thông tin thêm mới nhân viên</param>
        /// <returns>Service Result</returns>
        /// CreatedBy: nvdien(13/8/2021)
        /// ModifiedBy: nvdien(13/8/2021)
        ServiceResult InsertEmployee(Employee employee);

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="employee">thông tin cần sửa của nhân viên</param>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <returns>ServiceResult</returns>
        /// CreatedBy: nvdien(13/8/2021)
        /// ModifiedBy: nviden(13/8/2021)
        ServiceResult UpdateEmployee(Employee employee, Guid employeeId);

        /// <summary>
        /// Xóa nhân viên theo Id
        /// </summary>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <returns>ServiceResult</returns>
        /// CreatedBy: nvdien(13/8/2021)
        /// ModifiedBy: nvdien(13/8/2021)
        ServiceResult DeleteEmployee(Guid employeeId);
    }
}
