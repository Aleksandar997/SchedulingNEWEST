using Dapper;
using Entity.Base;
using SQLContext.Builders;
using SQLContext.Factories;
using SQLContext.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SQLContext
{
    public abstract class RepositoryBase 
    {
        public string ConnectionString { get; set; }

        public RepositoryBase(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #region QueryMultipleAsync
        public async Task<ResponseBase<T>> QueryMultipleAsync<T>(
                string storedProcedure,
                object param,
                Func<SqlMapper.GridReader, ResponseBase<T>> readMap,
                IDbTransaction transaction = null,
                int? commandTimeout = null,
                CommandType commandType = CommandType.StoredProcedure
            ) => await ManualSqlFactory.QueryMultipleAsync(ConnectionString, storedProcedure, param, commandType, transaction, commandTimeout, readMap);

        public async Task<ResponseBase<T>> QueryMultipleAsync<T>(
            string storedProcedure,
            object param,
            Func<SqlMapper.GridReader, T> readMap,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType commandType = CommandType.StoredProcedure
        ) => await ManualSqlFactory.QueryMultipleAsync(ConnectionString, storedProcedure, param, commandType, transaction, commandTimeout, readMap);

        public async Task<ResponseBase<T>> QueryMultipleAsync<T>(
            string storedProcedure,
            object param,
            Func<SqlMapper.GridReader, ValueTuple<T, int>> readMap,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType commandType = CommandType.StoredProcedure
        ) => await ManualSqlFactory.QueryMultipleAsync(ConnectionString, storedProcedure, param, commandType, transaction, commandTimeout, readMap);

        public async Task<ResponseBase<T>> QueryMultipleAsync<T>(
            string storedProcedure,
            Func<SqlMapper.GridReader, T> readMap,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType commandType = CommandType.StoredProcedure
        ) => await ManualSqlFactory.QueryMultipleAsync(ConnectionString, storedProcedure, null, commandType, transaction, commandTimeout, readMap);
        #endregion

        #region QuerySingleAsync
        public async Task<ResponseBase<T>> QuerySingleAsync<T>(
                string storedProcedure,
                object param,
                IDbTransaction transaction = null,
                int? commandTimeout = null,
                CommandType commandType = CommandType.StoredProcedure
            ) => await ManualSqlFactory.QuerySingleAsync<T>(ConnectionString, storedProcedure, param, commandType, transaction, commandTimeout);
        #endregion

        #region ExecuteScalarAsync
        public async Task<ResponseBase<T>> ExecuteScalarAsync<T>(
                string storedProcedure,
                object param,
                IDbTransaction transaction = null,
                int? commandTimeout = null,
                CommandType commandType = CommandType.StoredProcedure
            ) => await ManualSqlFactory.ExecuteScalarAsync<T>(ConnectionString, storedProcedure, param, commandType, transaction, commandTimeout);
        #endregion

        #region QueryMultipleAsync
        public async Task<ResponseBase<T>> ExecuteReaderAsync<T>(
                string storedProcedure,
                object param,
                Func<SqlMapper.GridReader, ResponseBase<T>> readMap,
                IDbTransaction transaction = null,
                int? commandTimeout = null,
                CommandType commandType = CommandType.StoredProcedure
            ) => await ManualSqlFactory.ExecuteReaderAsync(ConnectionString, storedProcedure, param, commandType, transaction, commandTimeout, readMap);

        public async Task<ResponseBase<T>> ExecuteReaderAsync<T>(
            string storedProcedure,
            object param,
            Func<SqlMapper.GridReader, T> readMap,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType commandType = CommandType.StoredProcedure
        ) => await ManualSqlFactory.QueryMultipleAsync(ConnectionString, storedProcedure, param, commandType, transaction, commandTimeout, readMap);

        public async Task<ResponseBase<T>> ExecuteReaderAsync<T>(
            string storedProcedure,
            object param,
            Func<SqlMapper.GridReader, ValueTuple<T, int>> readMap,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType commandType = CommandType.StoredProcedure
        ) => await ManualSqlFactory.ExecuteReaderAsync(ConnectionString, storedProcedure, param, commandType, transaction, commandTimeout, readMap);

        public async Task<ResponseBase<T>> ExecuteReaderAsync<T>(
            string storedProcedure,
            Func<SqlMapper.GridReader, T> readMap,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType commandType = CommandType.StoredProcedure
        ) => await ManualSqlFactory.ExecuteReaderAsync(ConnectionString, storedProcedure, null, commandType, transaction, commandTimeout, readMap);
        #endregion

    }
}
//public ResponseBase<T> ReadData<T>(Func<ResponseBase<T>> func)
//{
//    var sqlReaderModel = func.Target.GetType().GetField("read").GetValue(func.Target) as SqlReaderModel;
//    if (sqlReaderModel.Read == null)
//    {
//        return ResponseBase<T>.Error(sqlReaderModel.SqlMessages);
//    }
//    var res = func();
//    return res;
//}

//public ResponseBase<T> ReadData<T>(Func<T> func)
//{
//    var sqlReaderModel = func.Target.GetType().GetField("read").GetValue(func.Target) as SqlReaderModel;
//    if (sqlReaderModel.Read == null)
//    {
//        return ResponseBase<T>.Error(sqlReaderModel.SqlMessages);
//    }
//    var res = func();
//    return ResponseBase<T>.Success(res, sqlReaderModel.SqlMessages);
//}