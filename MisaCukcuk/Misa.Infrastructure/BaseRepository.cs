using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Interfaces.Base;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region DECLARE
        protected IConfiguration _configuration;
        protected IDbConnection _dbConnection;
        protected string _connectionString = string.Empty;
        private string _className;

        #endregion

        #region CONSTRUCTOR
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("MisaCukcukConnection");
            _className = typeof(TEntity).Name;
        }


        #endregion

        #region METHOD
        public int Delete(Guid entityId)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = $"DELETE FROM Employee WHERE {_className}Id = @{_className}Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{_className}Id", entityId);
                var rowEffects = _dbConnection.Execute(sqlCommand, param: parameters);
                return rowEffects;
            }

        }

        public IEnumerable<TEntity> GetAllEntities()
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = $"SELECT * from {_className} ORDER BY CreatedDate DESC";
                var entities = _dbConnection.Query<TEntity>(sqlCommand);
                return entities;
            }

        }

        public TEntity GetEntityById(Guid entityId)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = $"SELECT * from {_className} WHERE {_className}Id = @{_className}Id";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{_className}Id", entityId);
                var entity = _dbConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: dynamicParameters);
                return entity;
            }
        }

        public TEntity GetEntityByProperty(string propName, object propValue)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = $"SELECT * from {_className} WHERE {propName} = @{propName}";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{propName}", propValue);
                var entity = _dbConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: dynamicParameters);
                return entity;
            }
        }

        public int Insert(TEntity entity)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var properties = entity.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.IsDefined(typeof(MisaUnique), false)) continue;
                    var propName = property.Name;
                    var propValue = property.GetValue(entity);
                    dynamicParameters.Add($"@{propName}", propValue);

                }
                var proceduce = $"Proc_Insert{_className}";
                var rowEffects = _dbConnection.Execute(proceduce, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }

        public int Update(TEntity entity, Guid entityId)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var properties = entity.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.IsDefined(typeof(MisaUnique), false)) continue;
                    var propName = property.Name;
                    var propValue = property.GetValue(entity);
                    var propId = $"{_className}Id";
                    if (propName == propId)
                    {
                        dynamicParameters.Add($"{propName}", entityId);
                    }
                    else
                    {
                        dynamicParameters.Add($"{propName}", propValue);
                    }
                }

                var proceduce = $"Proc_Update{_className}";
                var rowEffects = _dbConnection.Execute(proceduce, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }


        #endregion
    }
}
