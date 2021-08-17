
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Services;
using Misa.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Misa.Web.Controllers
{
    public class CustomersController : BaseEntityController<Customer>
    {
        #region Declare
        IBaseService<Customer> _baseService;
        #endregion

        #region Constructor
        public CustomersController(IBaseService<Customer> baseService):base(baseService)
        {
            _baseService = baseService;
        }
        #endregion
    }
}
