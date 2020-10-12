using Dapper;
using Entity.Base;
using SQLContext.Helpers;
using SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SQLContext.Services.Interfaces
{
    public interface ISqlContextExecution
    {
        Task<ResponseBase<IEnumerable<object>>> Execute(SelectModel selectModel, string connectionString);
        Task<IDisposable> QueryMultipleAsync(
            SqlConnection connection,
            string storedProcedure,
            DynamicParameter param = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null
        );

        Task<Output> QuerySingleAsync<Output>(
            SqlConnection connection,
            string storedProcedure,
            DynamicParameter param = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null
        );

        Task<Output> ExecuteScalarAsync<Output>(
            SqlConnection connection,
            string storedProcedure,
            DynamicParameter param = null,
            CommandType? commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null,
            int? commandTimeout = null
        );

        Task<IDisposable> ExecuteReaderAsync(
            SqlConnection connection,
            string storedProcedure,
            DynamicParameter param,
            CommandType? commandType,
            IDbTransaction transaction,
            int? commandTimeout
        );

        Task<ResponseBase<int>> ExecuteSave(SaveModel saveModel, string connectionString);
    }
}


