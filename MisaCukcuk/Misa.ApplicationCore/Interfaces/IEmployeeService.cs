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
        /// <returns></returns>
        IEnumerable<Employee> GetEmployees();
    }
}
