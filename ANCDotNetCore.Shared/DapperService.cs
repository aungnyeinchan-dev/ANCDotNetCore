using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ANCDotNetCore.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, object? param = null) // DapperService => Query
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            /*if(param != null) 
            {
                var lst = db.Query<T>(query, param).ToList();
            }
            else
            {
                var lst =db.Query<T>(query).ToList();
            }*/
            var lst = db.Query<T> (query, param).ToList(); // Dapper => Query
            return lst;
        }
        public T QueryFirstOrDafault<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var lst = db.Query<T>(query, param).FirstOrDefault(); 
            return lst;
        }

        public int Execute(string query, object? param = null) 
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var result = db.Execute(query, param);
            return result;
        }
    }
}
