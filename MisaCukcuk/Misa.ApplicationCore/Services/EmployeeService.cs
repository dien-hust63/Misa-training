using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Misa.ApplicationCore
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Declare
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        public EmployeeService(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeRepository):base(baseRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion

        #region Method
        public ServiceResult GetEmployeeFilterPaging(string searchData, Guid? departmentId, Guid? positionId, int pageIndex, int pageSize)
        {
            _serviceResult.Data = _employeeRepository.GetEmployeeFilterPaging(searchData, departmentId, positionId, pageIndex, pageSize);
            return _serviceResult;
        }

       public ServiceResult GetNewEmployeeCode()
        {
            _serviceResult.Data = _employeeRepository.GetNewEmployeeCode();
            return _serviceResult;
        }

        #endregion
    }
}
