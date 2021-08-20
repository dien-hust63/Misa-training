using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Repository;
using Misa.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        #region Declare
        ICustomerRepository _customerRepository;
        #endregion

        #region Constructor
        public CustomerService(IBaseRepository<Customer> baseRepository, ICustomerRepository customerRepository):base(baseRepository)
        {
            _customerRepository = customerRepository;
        }

        public ServiceResult GetCustomerFilterPaging(string searchData, Guid? customerGroupId, int pageIndex, int pageSize)
        {
            _serviceResult.Data = _customerRepository.GetCustomerFilterPaging(searchData, customerGroupId, pageIndex, pageSize);
            return _serviceResult;
        }


        #endregion

        #region Methods     

        #endregion
    }
}
