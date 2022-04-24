using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Api.DataAccess.Interfaces;
using MySql.Data.MySqlClient;

namespace Api.DataAccess
{
    public class DataAccess : IData
    {
        private readonly IConfiguration _configuration;
        private string _connectionString = "MyConnectionString";
        private MySqlConnection _conn;

        public DataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection DbConnection
        {
            get
            {
                if (_conn == null)
                {
                    _conn = new MySqlConnection(_configuration.GetConnectionString(_connectionString));
                }
                return _conn;
            }
        }
    }
}