using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        IConfiguration _configuration;
        IDbConnection _dbConnection;
        string _connectionString = string.Empty;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("MisaCukcukConnection");
            _dbConnection = new MySqlConnection(_connectionString);
        }

        public IEnumerable<Customer> getAllCustomers()
        {
            string sqlCommand = "SELECT * from Customer";
            var customers = _dbConnection.Query<Customer>(sqlCommand);
            return customers;
        }
        public Customer getCustomerById(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public int InsertCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
