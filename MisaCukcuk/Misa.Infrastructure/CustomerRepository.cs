using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infrastructure
{
    public class CustomerRepository : BaseRepository<Customer> , ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration):base(configuration)
        {
        }

        public object GetCustomerFilterPaging(string searchData, Guid? customerGroupId, int pageIndex, int pageSize)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var customerFilter = searchData == null ? string.Empty : searchData;
                dynamicParameters.Add("@CustomerFilter", customerFilter);
                dynamicParameters.Add("@CustomerGroupId", customerGroupId);
                dynamicParameters.Add("@PageIndex", pageIndex);
                dynamicParameters.Add("@PageSize", pageSize);
                dynamicParameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamicParameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var proceduce = $"Proc_GetCustomerFilterPaging";
                var customers = _dbConnection.Query(proceduce, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                var totalRecord = dynamicParameters.Get<int>("TotalRecord");
                var totalPage = dynamicParameters.Get<int>("TotalPage");
                var result = new
                {
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
                    Customers = customers,
                };
                return result;
            }
        }
    }
}
