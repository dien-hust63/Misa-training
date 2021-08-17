using Misa.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// lấy danh sách khách hàng
        /// </summary>
        /// <returns>List khách hàng</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        IEnumerable<Customer> getAllCustomers();

        /// <summary>
        /// Lấy thông tin khách hàng qua Id
        /// </summary>
        /// <param name="customerId">Id khách hàng</param>
        /// <returns>Service Result</returns>
        /// <returns>object khách hàng</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        Customer getCustomerById(Guid customerId);

        /// <summary>
        /// Thêm khách hàng
        /// </summary>
        /// <param name="customer">Thông tin khách hàng cần thêm</param>
        /// <returns>Int: 1(thêm thành công)</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        int InsertCustomer(Customer customer);


    }
}
