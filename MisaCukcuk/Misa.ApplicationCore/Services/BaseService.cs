using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Services
{

    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        #region DECLARE
        IBaseRepository<TEntity> _baseRepository;
        ServiceResult _serviceResult;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
        }

        #endregion

        #region Method

        public ServiceResult Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetAllEntities()
        {
            _serviceResult.Data = _baseRepository.GetAllEntities();
            return _serviceResult;
        }

        public ServiceResult GetEntityById(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Update(TEntity entity, Guid entityId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
