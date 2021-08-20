using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Constructor
        public EmployeeRepository(IConfiguration configuration):base(configuration)
        {
        }
        #endregion

        #region Method
        public object GetEmployeeFilterPaging(string searchData, Guid? departmentId, Guid? positionId, int pageIndex, int pageSize)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var employeeFilter = searchData == null ? string.Empty : searchData;
                dynamicParameters.Add("@EmployeeFilter", employeeFilter);
                dynamicParameters.Add("@DepartmentId", departmentId);
                dynamicParameters.Add("@PositionId", positionId);
                dynamicParameters.Add("@PageIndex", pageIndex);
                dynamicParameters.Add("@PageSize", pageSize);
                dynamicParameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamicParameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var proceduce = "Proc_GetEmployeeFilterPaging";
                var employees = _dbConnection.Query(proceduce, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                var totalRecord = dynamicParameters.Get<int>("TotalRecord");
                var totalPage = dynamicParameters.Get<int>("TotalPage");
                var result = new
                {
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
                    Employees = employees,
                };
                return result;
            }
        }
        
        public string GetNewEmployeeCode()
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var proceduce = "Proc_GetNewEmployeeCode";

                var newEmployeecCode = _dbConnection.QueryFirstOrDefault<string>(proceduce, commandType: CommandType.StoredProcedure);
                return newEmployeecCode;
            }
        }



        #endregion

    }
}
