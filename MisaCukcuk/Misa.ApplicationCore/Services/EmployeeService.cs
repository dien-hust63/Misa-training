using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Misa.ApplicationCore
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        ServiceResult _serviceResult;
        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _serviceResult = new ServiceResult();
        }

        #endregion
        #region Method
        public ServiceResult GetAllEmployees()
        {
            _serviceResult.Data = _employeeRepository.GetAllEmployees();
            return _serviceResult;
        }


        public ServiceResult GetEmployeeById(Guid employeeId)
        {
            _serviceResult.Data = _employeeRepository.GetEmployeeById(employeeId);
            return _serviceResult;
        }

        public ServiceResult InsertEmployee(Employee employee)
        {
            #region Validate thông tin

            
            //Kiểm tra các trường bắt buộc , không được phép để trống
            foreach (var property in employee.GetType().GetProperties())
            {
                var propertyAttributeRequired = property.GetCustomAttributes(typeof(AttributeRequired), true);
                if(propertyAttributeRequired.Length > 0)
                {
                    if (string.IsNullOrEmpty((string)property.GetValue(employee)))
                    {
                        var errorObj = new
                        {
                            devMessage = string.Format(Resources.ResourceVN.Exception_Required, property.Name),
                            userMsg = string.Format(Resources.ResourceVN.Exception_Required, property.Name),
                            errorCode = "400",
                            moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                            traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                        };
                        _serviceResult.IsValid = false;
                        _serviceResult.Data = errorObj;
                        return _serviceResult;
                    }
                    
                }
            }
           
            //Kiểm tra trùng mã nhân viên
            var employeeCode = _employeeRepository.GetEmployeeByCode(employee.EmployeeCode);
            if (employeeCode != null)
            {
                var errorObj = new
                {
                    userMsg = Resources.ResourceVN.Exception_EmployeeCodeDuplication,
                    errorCode = "400",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorObj;
                return _serviceResult;
            }

            //Kiểm tra định dạng email
            var regexEmail = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var isValidEmail = Regex.IsMatch(employee.Email, regexEmail);
            if (!isValidEmail)
            {
                var errorObj = new
                {
                    userMsg = Resources.ResourceVN.Exception_EmployeeEmail,
                    errorCode = "400",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorObj;
                return _serviceResult;
            }

            #endregion
            //Tạo Id nhân viên mới không bị trùng
            employee.EmployeeId = Guid.NewGuid();

            //Thêm nhân viên
            var rowEffects = _employeeRepository.InsertEmployee(employee);
            _serviceResult.Data = rowEffects;
            if (rowEffects < 1)
            {
                _serviceResult.IsValid = false;
            }
            return _serviceResult;

        }

        public ServiceResult UpdateEmployee(Employee employee, Guid employeeId)
        {
            #region Validate thông tin

            
            //Kiểm tra các trường bắt buộc , không được phép để trống
            foreach (var property in employee.GetType().GetProperties())
            {
                var propertyAttributeRequired = property.GetCustomAttributes(typeof(AttributeRequired), true);
                if (propertyAttributeRequired.Length > 0)
                {
                    if (string.IsNullOrEmpty((string)property.GetValue(employee)))
                    {
                        var errorObj = new
                        {
                            devMessage = string.Format(Resources.ResourceVN.Exception_Required, property.Name),
                            userMsg = string.Format(Resources.ResourceVN.Exception_Required, property.Name),
                            errorCode = "400",
                            moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                            traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                        };
                        _serviceResult.IsValid = false;
                        _serviceResult.Data = errorObj;
                        return _serviceResult;
                    }

                }
            }

            //Kiểm tra định dạng email
            var regexEmail = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var isValidEmail = Regex.IsMatch(employee.Email, regexEmail);
            if (!isValidEmail)
            {
                var errorObj = new
                {
                    userMsg = Resources.ResourceVN.Exception_EmployeeEmail,
                    errorCode = "400",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorObj;
                return _serviceResult;
            }
            #endregion

            //Sửa thông tin nhân viên
            employee.EmployeeId = employeeId;
            var rowEffects = _employeeRepository.UpdateEmployee(employee, employeeId);
            _serviceResult.Data = rowEffects;
            if (rowEffects < 1)
            {
                _serviceResult.IsValid = false;
            }
            return _serviceResult;
        }
        public ServiceResult DeleteEmployee(Guid employeeId)
        {
            var rowEffects = _employeeRepository.DeleteEmployee(employeeId);
            _serviceResult.Data = rowEffects;
            if (rowEffects < 1)
            {
                _serviceResult.IsValid = false;
            }
            return _serviceResult;
        }

        #endregion
    }
}
