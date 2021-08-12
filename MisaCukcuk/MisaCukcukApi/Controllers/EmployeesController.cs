using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces;
using Misa.Web.Properties;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MisaCukcukApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IEmployeeService _employeeService;
        #region Constructor
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        /// <summary>
        /// Lấy danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: nvdien(12/8/2021)
        /// ModifiedBy: ndien(12/8/2021)
        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {
                var employees = _employeeService.GetEmployees();
                //4.Trả về kết quả cho client
                if (employees.Count() > 0)
                {
                    var response = StatusCode(200, employees);
                    return response;
                }
                else
                {
                    return StatusCode(204, employees);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }


        }

        /// <summary>
        /// Lấy thông tin nhân viên theo Id của nhân viên
        /// </summary>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(12/8/2021)
        /// ModifiedBy: ndien(12/8/2021)
        [HttpGet("{employeeId}")]
        public IActionResult getEmployeeById(string employeeId)
        {
            try
            {
                // Truy cập vào database:
                //1.Khai báo thông tin kết nối database:
                var connectionString = "Host = 47.241.69.179;" +
                    "Database = WEB07.MF959.NVDIEN.CukCuk;" +
                    "User Id = dev;" +
                    "Password = 12345678";

                //2.Khởi tạo đối tượng kết nối database
                IDbConnection dbConnection = new MySqlConnection(connectionString);

                //3.Lấy dữ liệu
                var sqlCommand = "SELECT * FROM  Employee WHERE EmployeeId = @EmployeeId";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employeeId);
                var employee = dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: parameters);
                //4.Trả về kết quả cho client
                if (employee != null)
                {
                    return StatusCode(200, employee);
                }
                else
                    return StatusCode(204);
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }

        }

        /// <summary>
        /// Thêm nhân viên mới
        /// </summary>
        /// <param name="employee">Dữ liệu nhân viên được thêm</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(12/8/2021)
        /// ModifiedBy: ndien(12/8/2021)
        [HttpPost]
        public IActionResult InsertEmployee(Employee employee)
        {
            try
            {
                //Kiểm tra thông tin của khách hàng đã hợp lệ hay chưa ?
                //1. Mã khách hàng buộc phải có
                if (employee.EmployeeCode == "" || employee.EmployeeCode == null)
                {
                    var errorObj = new
                    {
                        userMsg = Resources.Exception_EmployeeCode,
                        errorCode = "400",
                        moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                    };
                    return BadRequest(errorObj);
                }
                //2.Email phải đúng định dạng
                var regexEmail = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                var isValidEmail = Regex.IsMatch(employee.Email, regexEmail);
                if (!isValidEmail)
                {
                    var errorObj = new
                    {
                        userMsg = Resources.Exception_EmployeeEmail,
                        errorCode = "400",
                        moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                    };
                    return BadRequest(errorObj);
                }


                // Truy cập vào database:
                var connectionString = "Host = 47.241.69.179;" +
                    "Database = WEB07.MF959.NVDIEN.CukCuk;" +
                    "User Id = dev;" +
                    "Password = 12345678";
                //2.Khởi tạo đối tượng kết nối database
                IDbConnection dbConnection = new MySqlConnection(connectionString);
                var dynamicParams = new DynamicParameters();

                //Kiểm tra mã nhân viên không được trùng với mã nhân viên cũ
                var queryEmployeeCode = @"SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeCode", employee.EmployeeCode);
                var result = dbConnection.QueryFirstOrDefault<Employee>(queryEmployeeCode, param: parameters);
                if (result != null)
                {
                    var errorObj = new
                    {
                        userMsg = Resources.Exception_EmployeeCodeDuplication,
                        errorCode = "400",
                        moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                    };
                    return BadRequest(errorObj);
                }

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
                var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParams);

                //4.Trả về kết quả cho client
                if (rowEffects > 0)
                {
                    return StatusCode(201, rowEffects);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }

        }

        /// <summary>
        /// Chỉnh sửa thông tin nhân viên theo Id
        /// </summary>
        /// <param name="employeeId">Id nhân viên</param>
        /// <param name="employee">Thông tin nhân viên muốn thay đổi</param>
        /// <returns></returns>
        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployee(string employeeId, Employee employee)
        {
            try
            {
                // Truy cập vào database:
                //1.Khai báo thông tin kết nối database:
                var connectionString = "Host = 47.241.69.179;" +
                    "Database = WEB07.MF959.NVDIEN.CukCuk;" +
                    "User Id = dev;" +
                    "Password = 12345678";

                //2.Khởi tạo đối tượng kết nối database
                IDbConnection dbConnection = new MySqlConnection(connectionString);
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
                var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParams);
                //4.Trả về kết quả cho client
                return StatusCode(200, rowEffects);

            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);

            }
        }


        /// <summary>
        /// Xóa nhân viên theo Id
        /// </summary>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(12/8/2021)
        /// ModifiedBy: ndien(12/8/2021)
        [HttpDelete("{employeeId}")]
        public IActionResult DeleleteEmployeeById(string employeeId)
        {
            try
            {
                // Truy cập vào database:
                //1.Khai báo thông tin kết nối database:
                var connectionString = "Host = 47.241.69.179;" +
                    "Database = WEB07.MF959.NVDIEN.CukCuk;" +
                    "User Id = dev;" +
                    "Password = 12345678";

                //2.Khởi tạo đối tượng kết nối database
                IDbConnection dbConnection = new MySqlConnection(connectionString);

                //3.Lấy dữ liệu
                var sqlCommand = "DELETE FROM Employee WHERE EmployeeId = @EmployeeId";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employeeId);
                var rowEffect = dbConnection.Execute(sqlCommand, param: parameters);
                //4.Trả về kết quả cho client
                if (rowEffect > 0)
                {
                    return StatusCode(200, rowEffect);
                }
                else
                {
                    var errorObj = new
                    {
                        userMsg = Resources.Exception_EmployeeIdDelete,
                        errorCode = "400",
                        moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                    };
                    return BadRequest(errorObj);
                }


            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }
        }
    }





}
