using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseStudy.DataAccess.Abstract;
using Dapper;

namespace CaseStudy.Core.Utilities.Helper
{
    public static class Orm
    {
        public static object Execute(IDbConnector connector, string sql, DynamicParameters parameters = null)
        {
            using var connection = connector.CreateConnection();

            return connection.Execute(sql, parameters);
        }

        public static async Task<object> ExecuteAsync(IDbConnector connector, string sql, DynamicParameters parameters = null)
        {
            using var connection = connector.CreateConnection();

            return await connection.ExecuteAsync(sql, parameters);
        }

        public static object ExecuteScalar(IDbConnector connector, string sql, DynamicParameters parameters = null)
        {
            using var connection = connector.CreateConnection();

            return connection.ExecuteScalar(sql, parameters);
        }

        public static async Task<object> ExecuteScalarAsync(IDbConnector connector, string sql, DynamicParameters parameters = null)
        {
            using var connection = connector.CreateConnection();

            return await connection.ExecuteScalarAsync<object>(sql, parameters);
        }

        public static IEnumerable<T> Query<T>(IDbConnector connector, string sql, DynamicParameters parameters = null)
        {
            using var connection = connector.CreateConnection();

            return connection.Query<T>(sql, parameters);
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(IDbConnector connector, string sql, DynamicParameters parameters = null)
        {
            using var connection = connector.CreateConnection();

            var result = await connection.QueryAsync<T>(sql, parameters);

            return result.AsList();
        }

        public static object QueryFirst(IDbConnector connector, string sql, DynamicParameters parameters = null)
        {
            using var connection = connector.CreateConnection();

            return connection.QueryFirst(sql, parameters);
        }

        public static async Task<T> QueryFirstAsync<T>(IDbConnector connector, string sql, DynamicParameters parameters = null)
        {
            using var connection = connector.CreateConnection();

            var result = await connection.QueryFirstAsync<T>(sql, parameters);

            return result;
        }
    }
}
