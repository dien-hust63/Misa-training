using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore
{
    public class EmployeeService:IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Method
        //Lấy danh sách nhân viên
        public IEnumerable<Employee> GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees();
            return employees;
        }
        #endregion
    }
}
