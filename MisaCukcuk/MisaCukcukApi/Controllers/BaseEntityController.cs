using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Misa.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<TEntity> : ControllerBase
    {

        #region Declare
        IBaseService<TEntity> _baseService;
        #endregion

        #region Constructor
        public BaseEntityController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }
        #endregion
        #region Method
        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpGet]
        public IActionResult GetAllEntities()
        {
            try
            {
                var serviceResult = _baseService.GetAllEntities();
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
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="entityId">Id</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpGet("{entityId}")]
        public IActionResult GetEntityById(Guid entitiyId)
        {
            try
            {
                var serviceResult = _baseService.GetEntityById(entitiyId);
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
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Dữ liệu được thêm</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpPost]
        public IActionResult Insert(TEntity entity)
        {
            try
            {
                //Trả về kết quả cho client
                var serviceResult = _baseService.Insert(entity);
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
        /// Chỉnh sửa theo Id
        /// </summary>
        /// <param name="entityId">Id</param>
        /// <param name="entity">Thông tin muốn thay đổi</param>
        /// <returns></returns>
       /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpPut("{entityId}")]
        public IActionResult Update(Guid entityId, TEntity entity)
        {
            try
            {
                var serviceResult = _baseService.Update(entity, entityId);
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
        /// Xóa theo Id
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        { 
            try
            {
                var serviceResult = _baseService.Delete(entityId);

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
        #endregion
    }
}
