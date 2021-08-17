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

        IBaseRepository<Customer> _customerRepository;
        ServiceResult _serviceResult;

        #region Constructor
        public CustomerService(IBaseRepository<Customer> customerRepository):base(customerRepository)
        {
            _customerRepository = customerRepository;
            _serviceResult = new ServiceResult();
        }


        #endregion

        #region Methods     

        public ServiceResult GetCustomerByGroup(Guid groupId)
        {
            throw new NotImplementedException();
        }
        ServiceResult ICustomerService.GetCustomerPaging(int limit, int offset)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
