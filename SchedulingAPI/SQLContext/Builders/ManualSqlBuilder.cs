using Common.Base;
using Entity.Base;
using SQLContext.Helpers;
using SQLContext.Services.Interfaces;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SQLContext.Builders
{
    public static class ManualSqlFactory
    {
        private static ISqlContextExecution _execution = (ISqlContextExecution)DependencyInjectionResolver.ServiceProvider.GetService(typeof(ISqlContextExecution));

        #region QueryMultipleAsync
        public static async Task<ResponseBase<T>> QueryMultipleAsync<T, Reader>(
            string connectionString,
            string storedProcedure,
            object param = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            Func<Reader, ResponseBase<T>> readMap = null
        ) where Reader : class, IDisposable
        {
            using (var connection = Connection.CreateConnection(connectionString))
            {
                return await ExecuteCommand(async () =>
                {
                    var dynamicParams = new DynamicParameter(param);
                    var res = await _execution.QueryMultipleAsync(connection.DbConnection, storedProcedure, dynamicParams, commandType, transaction, commandTimeout);
                    return readMap(res as Reader);
                });
            }
        }

        public static async Task<ResponseBase<T>> QueryMultipleAsync<T, Reader>(
            string connectionString,
            string storedProcedure,
            object param = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            Func<Reader, T> readMap = null
        ) where Reader : class, IDisposable
        {
            using (var connection = Connection.CreateConnection(connectionString))
            {
                return await ExecuteCommand(async () =>
                {
                    var dynamicParams = new DynamicParameter(param);
                    var res = await _execution.QueryMultipleAsync(connection.DbConnection, storedProcedure, dynamicParams, commandType, transaction, commandTimeout);
                    return connection.Messages.Count > 0 ? 
                        ResponseBase<T>.Error(connection.Messages) : 
                        ResponseBase<T>.Success(readMap(res as Reader), null);
                });
            }
        }

        public static async Task<ResponseBase<T>> QueryMultipleAsync<T, Reader>(
            string connectionString,
            string storedProcedure,
            object param = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            Func<Reader, ValueTuple<T, int>> readMap = null
        ) where Reader : class, IDisposable
        {
            using (var connection = Connection.CreateConnection(connectionString))
            {
                return await ExecuteCommand(async () =>
                {
                    var dynamicParams = new DynamicParameter(param);
                    var res = await _execution.QueryMultipleAsync(connection.DbConnection, storedProcedure, dynamicParams, commandType, transaction, commandTimeout);
                    if (connection.Messages.Count > 0)
                        return ResponseBase<T>.Error(connection.Messages);
                    var mapRes = readMap(res as Reader);
                    return ResponseBase<T>.Success(mapRes.Item1, connection.Messages, mapRes.Item2);
                });
            }
        }
        #endregion

        #region QuerySingleAsync
        public static async Task<ResponseBase<T>> QuerySingleAsync<T>(
            string connectionString,
            string storedProcedure,
            object param = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null
        ) 
        {
            using (var connection = Connection.CreateConnection(connectionString))
            {
                return await ExecuteCommand(async () =>
                {
                    var dynamicParams = new DynamicParameter(param);
                    var res = await _execution.QuerySingleAsync<T>(connection.DbConnection, storedProcedure, dynamicParams, commandType, transaction, commandTimeout);
                    return connection.Messages.Count > 0 ?
                        ResponseBase<T>.Error(connection.Messages) :
                        ResponseBase<T>.Success(res, null);
                });
            }
        }
        #endregion

        #region ExecuteScalar
        public static async Task<ResponseBase<T>> ExecuteScalarAsync<T>(
            string connectionString,
            string storedProcedure,
            object param = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null
        )
        {
            using (var connection = Connection.CreateConnection(connectionString))
            {
                return await ExecuteCommand(async () =>
                {
                    var dynamicParams = new DynamicParameter(param);
                    var res = await _execution.ExecuteScalarAsync<T>(connection.DbConnection, storedProcedure, dynamicParams, commandType, transaction, commandTimeout);
                    return connection.Messages.Count > 0 ?
                        ResponseBase<T>.Error(connection.Messages) :
                        ResponseBase<T>.Success(res, null);
                });
            }
        }
        #endregion

        #region ExecuteReaderAsync
        public static async Task<ResponseBase<T>> ExecuteReaderAsync<T, Reader>(
            string connectionString,
            string storedProcedure,
            object param = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            Func<Reader, ResponseBase<T>> readMap = null
        ) where Reader : class, IDisposable
        {
            using (var connection = Connection.CreateConnection(connectionString))
            {
                return await ExecuteCommand(async () =>
                {
                    var dynamicParams = new DynamicParameter(param);
                    var res = await _execution.ExecuteReaderAsync(connection.DbConnection, storedProcedure, dynamicParams, commandType, transaction, commandTimeout);
                    return readMap(res as Reader);
                });
            }
        }

        public static async Task<ResponseBase<T>> ExecuteReaderAsync<T, Reader>(
            string connectionString,
            string storedProcedure,
            object param = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            Func<Reader, T> readMap = null
        ) where Reader : class, IDisposable
        {
            using (var connection = Connection.CreateConnection(connectionString))
            {
                return await ExecuteCommand(async () =>
                {
                    var dynamicParams = new DynamicParameter(param);
                    var res = await _execution.ExecuteReaderAsync(connection.DbConnection, storedProcedure, dynamicParams, commandType, transaction, commandTimeout);
                    return connection.Messages.Count > 0 ?
                        ResponseBase<T>.Error(connection.Messages) :
                        ResponseBase<T>.Success(readMap(res as Reader), null);
                });
            }
        }

        public static async Task<ResponseBase<T>> ExecuteReaderAsync<T, Reader>(
            string connectionString,
            string storedProcedure,
            object param = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            Func<Reader, ValueTuple<T, int>> readMap = null
        ) where Reader : class, IDisposable
        {
            using (var connection = Connection.CreateConnection(connectionString))
            {
                return await ExecuteCommand(async () =>
                {
                    var dynamicParams = new DynamicParameter(param);
                    var res = await _execution.ExecuteReaderAsync(connection.DbConnection, storedProcedure, dynamicParams, commandType, transaction, commandTimeout);
                    if (connection.Messages.Count > 0)
                        return ResponseBase<T>.Error(connection.Messages);
                    var mapRes = readMap(res as Reader);
                    return ResponseBase<T>.Success(mapRes.Item1, connection.Messages, mapRes.Item2);
                });
            }
        }
        //public static async Task<ResponseBase<T>> ExecuteReaderAsync<T>(
        //    string connectionString,
        //    string storedProcedure,
        //    object param = null,
        //    CommandType? commandType = CommandType.StoredProcedure,
        //    IDbTransaction transaction = null,
        //    int? commandTimeout = null
        //)
        //{
        //    using (var connection = Connection.CreateConnection(connectionString))
        //    {
        //        return await ExecuteCommand(async () =>
        //        {
        //            var dynamicParams = new DynamicParameter(param);
        //            var res = await _execution.ExecuteScalarAsync<T>(connection.DbConnection, storedProcedure, dynamicParams, commandType, transaction, commandTimeout);
        //            return connection.Messages.Count > 0 ?
        //                ResponseBase<T>.Error(connection.Messages) :
        //                ResponseBase<T>.Success(res, null);
        //        });
        //    }
        //}
        #endregion

        private static async Task<ResponseBase<T>> ExecuteCommand<T>(Func<Task<ResponseBase<T>>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ResponseBase<T>.Error(ex.Message);
            }
        }
    }
}
