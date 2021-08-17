using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public IActionResult GetAllEmployees()
        {
            try
            {
                var serviceResult = _employeeService.GetAllEmployees();
                //4.Trả về kết quả cho client
                if (serviceResult.Data != null)
                {
                    return StatusCode(200, serviceResult.Data);
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
        /// Lấy thông tin nhân viên theo Id của nhân viên
        /// </summary>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(12/8/2021)
        /// ModifiedBy: ndien(12/8/2021)
        [HttpGet("{employeeId}")]
        public IActionResult getEmployeeById(Guid employeeId)
        {
            try
            {
                var serviceResult = _employeeService.GetEmployeeById(employeeId);
                if (serviceResult.Data != null)
                {
                    return StatusCode(200, serviceResult.Data);
                }
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
                //Trả về kết quả cho client
                var serviceResult = _employeeService.InsertEmployee(employee);
                if (serviceResult.IsValid)
                {
                    return StatusCode(201);
                }
                else
                {
                    return BadRequest(serviceResult.Data);
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
        public IActionResult UpdateEmployee(Guid employeeId, Employee employee)
        {
            try
            {
                var serviceResult = _employeeService.UpdateEmployee(employee, employeeId);
                if (serviceResult.IsValid)
                {

                    return Ok(serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult.Data);
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
        /// Xóa nhân viên theo Id
        /// </summary>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(12/8/2021)
        /// ModifiedBy: ndien(12/8/2021)
        [HttpDelete("{employeeId}")]
        public IActionResult DeleleteEmployeeById(Guid employeeId)
        {
            try
            {
                var serviceResult = _employeeService.DeleteEmployee(employeeId);

                //4.Trả về kết quả cho client
                if (serviceResult.IsValid)
                {
                    return StatusCode(200, serviceResult.Data);
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
