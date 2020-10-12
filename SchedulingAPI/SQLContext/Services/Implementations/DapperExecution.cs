using Common.Extensions;
using Dapper;
using Entity.Base;
using SQLContext.Helpers;
using SQLContext.Models;
using SQLContext.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SQLContext.Services.Implementations
{
    public class DapperExecution : ISqlContextExecution
    {
        public async Task<Output> ExecuteScalarAsync<Output>(
            SqlConnection connection,
            string storedProcedure,
            DynamicParameter param,
            CommandType? commandType,
            IDbTransaction transaction,
            int? commandTimeout
        ) => await connection.ExecuteScalarAsync<Output>(storedProcedure, param, transaction, commandTimeout, commandType);

        public async Task<IDisposable> ExecuteReaderAsync(
            SqlConnection connection,
            string storedProcedure,
            DynamicParameter param,
            CommandType? commandType,
            IDbTransaction transaction,
            int? commandTimeout
        ) => await connection.ExecuteReaderAsync(storedProcedure, param, transaction, commandTimeout, commandType);

        public async Task<IDisposable> QueryMultipleAsync(
            SqlConnection connection,
            string storedProcedure,
            DynamicParameter param,
            CommandType? commandType,
            IDbTransaction transaction,
            int? commandTimeout
        ) => await connection.QueryMultipleAsync(storedProcedure, param, transaction, commandTimeout, commandType);

        public async Task<Output> QuerySingleAsync<Output>(
            SqlConnection connection,
            string storedProcedure,
            DynamicParameter param,
            CommandType? commandType,
            IDbTransaction transaction,
            int? commandTimeout
        ) => await connection.QuerySingleAsync<Output>(storedProcedure, param, transaction, commandTimeout, commandType);

        public async Task<ResponseBase<IEnumerable<object>>> Execute(SelectModel selectModel, string connectionString)
        {
            return await ExecuteSqlOperation(async () =>
            {
                using (var connection = Connection.CreateConnection(connectionString))
                {
                    var sql = selectModel.ToSql();
                    using (var multi = await connection.DbConnection.QueryMultipleAsync(sql, null, null, null, CommandType.Text))
                    {
                        var count = 0;
                        var types = selectModel.SplitOn.Types.Select(x => x.Parent).ToList();
                        var res = multi.Read(types.ToArray(), ((resRaw) =>
                        {
                            var res = resRaw.FirstOrDefault();
                            for (int i = 1; i < types.Count(); i++)
                            {
                                ReadData(res, selectModel.SplitOn.Types[i].NestedChildren, resRaw[i]);
                            }
                            return res;
                        }), selectModel.SplitOn.ToString());
                        return new ResponseBase<IEnumerable<object>>()
                        {
                            Data = res,
                            Count = count,
                            Status = connection.Messages.Any() ? ResponseStatus.Error : ResponseStatus.Success,
                            Messages = connection.Messages
                        };
                    }
                }
            });

        }

        private async Task<ResponseBase<IEnumerable<object>>> ExecuteSqlOperation(Func<Task<ResponseBase<IEnumerable<object>>>> func)
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ReadData(object obj, List<Type> types, object value)
        {
            var newObj = obj.GetType().GetProperty(types.FirstOrDefault().Name);
            if (types.Count == 1)
            {
                newObj.SetValue(obj, value);
                return;
            }
            ReadData(newObj.GetValue(obj), types.Skip(1).ToList(), value);
        }

        public async Task<ResponseBase<int>> ExecuteSave(SaveModel saveModel, string connectionString)
        {
            using (var connection = Connection.CreateConnection(connectionString))
            {
                var count = await connection.DbConnection.ExecuteScalarAsync<int>(saveModel.ToSql(), null, null, null, CommandType.Text);
                return new ResponseBase<int>()
                {
                    Count = count,
                    Status = connection.Messages.Any() ? ResponseStatus.Error : ResponseStatus.Success,
                    Messages = connection.Messages
                };
            }
        }
    }
}
