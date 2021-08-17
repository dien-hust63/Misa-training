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
    public class EmployeeRepository : IEmployeeRepository
    {
        IConfiguration _configuration;
        string _connectionString = String.Empty;
        IDbConnection _dbConnection;

        #region Constructor
        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MisaCukcukConnection");
            _dbConnection = new MySqlConnection(_connectionString);
        }
        #endregion

        #region Method
        public int DeleteEmployee(Guid employeeId)
        {
            //3.Lấy dữ liệu
            var sqlCommand = "DELETE FROM Employee WHERE EmployeeId = @EmployeeId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            var rowEffect = _dbConnection.Execute(sqlCommand, param: parameters);
            return rowEffect;
        }

        public Employee GetEmployeeByCode(string employeeCode)
        {
            var sqlCommand = "SELECT * FROM  Employee WHERE EmployeeCode = @EmployeeCode";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", employeeCode);
            var employee = _dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: parameters);
            return employee;
        }

        public Employee GetEmployeeById(Guid employeeId)
        {
            //3.Lấy dữ liệu
            var sqlCommand = "SELECT * FROM  Employee WHERE EmployeeId = @EmployeeId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            var employee = _dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: parameters);
            return employee;
        }
       
        public IEnumerable<Employee> GetAllEmployees()
        {
            //Lấy dữ liệu
            var sqlCommand = "SELECT * FROM  Employee";            
            var employees = _dbConnection.Query<Employee>(sqlCommand);

            return employees;
        }

        public int InsertEmployee(Employee employee)
        {
            var dynamicParams = new DynamicParameters();

            

            //3.Lấy dữ liệu
            var columnNames = string.Empty;
            var columnValues = string.Empty;
            var properties = employee.GetType().GetProperties();
            foreach (var prop in properties)
            {

                //Lấy tên property
                var propName = prop.Name;

                //Lấy value của property
                var propValue = prop.GetValue(employee);

                dynamicParams.Add($"@{propName}", propValue);
                columnNames += $"{propName},";
                columnValues += $"@{propName},";
            }

            columnNames = columnNames.Remove(columnNames.Length - 1);
            columnValues = columnValues.Remove(columnValues.Length - 1);

            var sqlCommand = $"INSERT INTO Employee ({columnNames}) VALUES({columnValues}); ";
            var rowEffects = _dbConnection.Execute(sqlCommand, param: dynamicParams);
            return rowEffects;
        }

        public int UpdateEmployee(Employee employee, Guid employeeId)
        {
            var dynamicParams = new DynamicParameters();

            //3.Lấy dữ liệu
            var properties = employee.GetType().GetProperties();
            var valueSet = string.Empty;
            foreach (var prop in properties)
            {
                //Lấy tên property
                var propName = prop.Name;

                //Lấy value của property
                var propValue = prop.GetValue(employee);

                dynamicParams.Add($"@{propName}", propValue);
                valueSet += $"{propName} = @{propName},";
            }

            valueSet = valueSet.Remove(valueSet.Length - 1);
            //column = value
            var sqlCommand = $"UPDATE Employee SET {valueSet} WHERE Employeeid = @EmployeeId ";
            var rowEffects = _dbConnection.Execute(sqlCommand, param: dynamicParams);
            return rowEffects;
        }

        public string GetNewEmployeeCode()
        {
            return "";
        }

      


        #endregion

    }
}
