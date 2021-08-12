using Dapper;
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
        public IEnumerable<Employee> GetEmployees()
        {

            //Truy cập vào database:
            //1.Khai báo thông tin kết nối database:
            var connectionString = "Host = 47.241.69.179;" +
                "Database = WEB07.MF959.NVDIEN.CukCuk;" +
                "User Id = dev;" +
                "Password = 12345678";

            //2.Khởi tạo đối tượng kết nối database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //3.Lấy dữ liệu
            var sqlCommand = "SELECT * FROM  Employee";
            var employees = dbConnection.Query<Employee>(sqlCommand);

            return employees;
        }
    }
}
