using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;

namespace DevTest.Server.Repositories.Implementation
{
    public class GenericRepository<T> where T : class
    {
        private readonly string? _connectionstring;
        public GenericRepository(IConfiguration config)
        {
            _connectionstring = config.GetConnectionString("DevTest");
        }
        protected async Task<T> GetById<TP>(TP id)
        {
            using (IDbConnection connection = new SqlConnection(_connectionstring))
            {
                return await connection.GetAsync<T>(id);
            }
        }

        protected async Task<IEnumerable<T>> GetAll()
        {
            using (IDbConnection connection = new SqlConnection(_connectionstring))
            {
                return await connection.GetAllAsync<T>();
            }
        }

        protected async Task<IEnumerable<TP>> ExecuteQuery<TP>(string sql, object? param = null)
        {
            using (IDbConnection connection = new SqlConnection(_connectionstring))
            {
                return await connection.QueryAsync<TP>(sql, param);
            }
        }
        protected async Task<TP> ExecuteQueryFirstOrDefault<TP>(string sql, object? param = null)
        {
            using (IDbConnection connection = new SqlConnection(_connectionstring))
            {
                return await connection.QueryFirstAsync<TP>(sql, param);
            }
        }

        protected async Task<int> ExecuteNonQuery(string sql, object? param = null)
        {
            using (IDbConnection connection = new SqlConnection(_connectionstring))
            {
                return await connection.ExecuteAsync(sql, param);
            }
        }

        protected async Task<int> Create(T model)
        {
            using (IDbConnection connection = new SqlConnection(_connectionstring))
            {
                return await connection.InsertAsync<T>(model);
            }
        }

        protected async Task<int> CreateRange(IEnumerable<T> models)
        {
            using (IDbConnection connection = new SqlConnection(_connectionstring))
            {
                return await connection.InsertAsync(models);
            }
        }

        protected async Task<bool> Update(T model)
        {
            using (IDbConnection connection = new SqlConnection(_connectionstring))
            {
                return await connection.UpdateAsync<T>(model);
            }
        }

        protected async Task<bool> UpdateRange(IEnumerable<T> models)
        {
            using (IDbConnection connection = new SqlConnection(_connectionstring))
            {
                return await connection.UpdateAsync(models);
            }
        }
    }
}
