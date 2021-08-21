using Microsoft.AspNetCore.Http;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Repository;
using Misa.ApplicationCore.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        #region Declare
        ICustomerRepository _customerRepository;
        #endregion

        #region Constructor
        public CustomerService(IBaseRepository<Customer> baseRepository, ICustomerRepository customerRepository):base(baseRepository)
        {
            _customerRepository = customerRepository;
        }

        #endregion

        #region Methods     

        public ServiceResult GetCustomerFilterPaging(string searchData, Guid? customerGroupId, int pageIndex, int pageSize)
        {
            _serviceResult.Data = _customerRepository.GetCustomerFilterPaging(searchData, customerGroupId, pageIndex, pageSize);
            return _serviceResult;
        }

        public ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            var customers = new List<Customer>();
            var phoneNumberList = new List<string>();
            var customerCodeList = new List<string>();

            // đọc dữ liệu trong file .xlsx .xls
            using (var stream = new MemoryStream())
            {
                formFile.CopyToAsync(stream, cancellationToken);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 3; row <= rowCount; row++)
                    {
                        var phoneNumber = this.ConvertToString(worksheet.Cells[row, 5].Value);
                        var customerCode = this.ConvertToString(worksheet.Cells[row, 1].Value);

                        phoneNumberList.Add(phoneNumber);
                        customerCodeList.Add(customerCode);

                        customers.Add(new Customer()
                        {
                            CustomerCode = customerCode,
                            FullName = this.ConvertToString(worksheet.Cells[row, 2].Value),
                            MemberCardCode = this.ConvertToString(worksheet.Cells[row, 3].Value),
                            CustomerGroupName = this.ConvertToString(worksheet.Cells[row, 4].Value),
                            PhoneNumber = phoneNumber,
                            DateOfBirth = ConvertToDateTime(worksheet.Cells[row, 6].Value),
                            CompanyName = this.ConvertToString(worksheet.Cells[row, 7].Value),
                            CompanyTaxCode = this.ConvertToString(worksheet.Cells[row, 8].Value),
                            Email = this.ConvertToString(worksheet.Cells[row, 9].Value),
                            Address = this.ConvertToString(worksheet.Cells[row, 10].Value),
                        });
                    }


                }

            }

            customers = this.ValidatePreImport(customers);

            _serviceResult.Data = _customerRepository.Import(customers);

            return _serviceResult;
        }
        /// <summary>
        /// Chuyển đổi object to string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string ConvertToString(object value)
        {
            if (value == null) return String.Empty;
            return value.ToString().Trim();

        }

        /// <summary>
        /// Chuyển đổi object to datetime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        DateTime? ConvertToDateTime(object value)
        {
            if (value != null)
            {
                string tmp = value.ToString().Trim();
                // dd/MM/yyyy
                if (Regex.IsMatch(tmp, @"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$"))
                {
                    return DateTime.ParseExact(tmp, "dd/MM/yyyy", null);
                }

                if (Regex.IsMatch(tmp, @"^(((0)[0-9])|((1)[0-2]))(\/)\d{4}$"))
                {
                    return DateTime.ParseExact("01/" + tmp, "dd/MM/yyyy", null);
                }

                if (Regex.IsMatch(tmp, @"^\d{4}$"))
                {
                    return DateTime.ParseExact("01/01/" + tmp, "dd/MM/yyyy", null);
                }
            }

            return null;
        }

        /// <summary>
        /// validate danh sach thonh tin khach hang trước khi import
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="phoneNumberList"></param>
        /// <param name="customerCodeList"></param>
        /// <returns></returns>
        List<Customer> ValidatePreImport(List<Customer> customers)
        {
            return null;
        }


        #endregion
    }
}
