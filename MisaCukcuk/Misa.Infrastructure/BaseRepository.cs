using Dapper;
using Microsoft.Extensions.Configuration;
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
        IConfiguration _configuration;
        IDbConnection _dbConnection;
        string _connectionString = string.Empty;
        string _tableName;

        #endregion

        #region CONSTRUCTOR
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("MisaCukcukConnection");
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(TEntity).Name;
        }
        #endregion

        #region METHOD
        public int Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAllEntities()
        {
            string sqlCommand = $"SELECT * from {_tableName}";
            var entities = _dbConnection.Query<TEntity>(sqlCommand);
            return entities;
        }

        public TEntity GetEntityById(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public int Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Update(TEntity entity, Guid entityId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
