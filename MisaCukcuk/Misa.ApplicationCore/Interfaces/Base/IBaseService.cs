using Misa.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Base
{
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>  
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        ServiceResult GetAllEntities();

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="entityId">Id đối tượng</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021) 
        ServiceResult GetEntityById(Guid entityId);



        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Thông tin được thêm</param>
        /// <returns>số bản ghi được thêm</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        ServiceResult Insert(TEntity entity);

        /// <summary>
        /// Sửa thông tin
        /// </summary>
        /// <param name="entity">Thông tin cần sửa</param>
        /// <param name="entityId">Id </param>
        /// <returns>số bản ghi được sửa</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        ServiceResult Update(TEntity entity, Guid entityId);

        /// <summary>
        /// Xóa theo Id
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <returns>Số bản ghi được xóa</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        ServiceResult Delete(Guid entityId);

    }
}
