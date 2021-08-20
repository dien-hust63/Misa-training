
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Services;
using Misa.Web.Properties;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Misa.Web.Controllers
{
    public class CustomersController : BaseEntityController<Customer>
    {
        #region Declare
        ICustomerService _customerService;
        #endregion

        #region Constructor
        public CustomersController(IBaseService<Customer> baseService, ICustomerService customerService) : base(baseService)
        {
            _customerService = customerService;
        }
        #endregion

        #region Method
        [HttpGet("Filter")]
        public IActionResult GetCustomerFilterPaging([FromQuery] string searchData, [FromQuery] Guid? customerGroupId, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            try
            {
                var serviceResult = _customerService.GetCustomerFilterPaging(searchData, customerGroupId, pageIndex, pageSize);
                return Ok(serviceResult.Data);
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

        [HttpPost("Import")]
        public IActionResult Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            try
            {
                var listEmployees = new List<Customer>();
                using (var stream = new MemoryStream())
                {
                    formFile.CopyToAsync(stream, cancellationToken);
                    using (var package = new ExcelPackage(stream))
                    {

                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                        
                        for (int row = 3; row <= rowCount; row++)
                        {
                            var customer = new Customer();
                            customer.CustomerCode = worksheet.Cells[row, 1].Value.ToString().Trim();
                            customer.FullName = worksheet.Cells[row, 2].Value.ToString().Trim();
                            customer.MemberCardCode = worksheet.Cells[row, 3].Value.ToString().Trim();
                            customer.PhoneNumber = worksheet.Cells[row, 5].Value.ToString().Trim();
                            //customer.DateOfBirth = DateTime.Parse(worksheet.Cells[row, 6].Value.ToString());
                            //customer.CompanyName = worksheet.Cells[row, 7].Value.ToString().Trim();
                            customer.Email = worksheet.Cells[row, 8].Value.ToString().Trim();
                            //customer.Address = worksheet.Cells[row, 9].Value.ToString().Trim();
                            //customer.Note = worksheet.Cells[row, 10].Value.ToString().Trim();
                            listEmployees.Add(customer);
                        }
                    }
                    return Ok(listEmployees);
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
        #endregion
    }
}
