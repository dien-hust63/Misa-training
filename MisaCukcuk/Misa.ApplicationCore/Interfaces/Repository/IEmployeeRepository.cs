using Misa.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// danh sách nhân viên
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>danh sách nhân viên</returns>
        /// CreatedBy: nvdien(13/8/2021)
        /// ModifiedBy: nvdien(13/8/2021)
        IEnumerable<Employee> GetAllEmployees();

        /// <summary>
        /// Lấy thông tin chi tiết 1 nhân viên
        /// </summary>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <returns>Object: Thông tin nhân viên</returns>
        /// CreatedBy: nvdien(13/8/2021)
        /// ModifiedBy: nviden(13/8/2021)
        Employee GetEmployeeById(Guid employeeId);

        /// <summary>
        /// Lấy thông tin chi tiết 1 nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="employeeCode"> Mã nhân viên</param>
        /// <returns>Object: Thông tin nhân viên</returns>
        /// CreatedBy: nvdien(13/8/2021)
        /// ModifiedBy: nviden(13/8/2021)
        Employee GetEmployeeByCode(string employeeCode);

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên được thêm</param>
        /// <returns>1: thành công, 0: thất bại</returns>
        /// CreatedBy: nvdien(13/8/2021)
        /// ModifiedBy: nvdien(13/8/2021)
        int InsertEmployee(Employee employee);

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="employee">Thông tin cần sửa của nhân viên</param>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <returns>1: thành công 0: thất bại</returns>
        /// CreatedBy: nvdien(13/8/2021)
        /// ModifiedBy: nvdien(13/8/2021)
        int UpdateEmployee(Employee employee, Guid employeeId);

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <returns>1: thành công 0:thất bại</returns>
        /// CreatedBy: nvdien(13/8/2021)
        /// ModifiedBy: nvdien(13/8/2021)
        int DeleteEmployee(Guid employeeId);

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        /// CreatedBy: nvdien(16/8/2021)
        /// ModifiedBy: nvdien(16/8/2021)
        string GetNewEmployeeCode();
    }
}
