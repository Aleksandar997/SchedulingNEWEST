using Common.Base;
using Entity.Base;
using SQLContext.Factories;
using SQLContext.Models;
using SQLContext.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SQLContext.Builders
{
    public class SqlBuilder<T> where T : class
    {
        private ISelectClauseService _selectClauseService = DependencyInjectionResolver.GetService<ISelectClauseService>();
        private IOrderByClauseService _orderByClauseService = DependencyInjectionResolver.GetService<IOrderByClauseService>();
        private ISqlContextExecution _execution = DependencyInjectionResolver.GetService<ISqlContextExecution>();
        private ISaveService _save = DependencyInjectionResolver.GetService<ISaveService>();
        private string _connectionString;
        private string _key;
        private SelectModel selectModel;
        private SaveModel saveModel;
        private bool _isSelectImplemented = false;
        internal SqlBuilder(SelectModel cache, string connectionString, string key)
        {
            selectModel = cache ?? SelectModel.Instance();
            _connectionString = connectionString;
            _key = key;
            selectModel.Table = new TableModel(typeof(T));
        }
        internal SqlBuilder(string connectionString)
        {
            selectModel = SelectModel.Instance();
            _connectionString = connectionString;
            selectModel.Table = new TableModel(typeof(T));
        }

        internal SqlBuilder(
                ISelectClauseService selectClauseService, 
                IOrderByClauseService orderByClauseService
            )
        {
            _selectClauseService = selectClauseService;
            _orderByClauseService = orderByClauseService;
        }

        public SqlBuilder<T> Save<TResult>(Expression<Func<T, TResult>> param, T input, int id)
        {
            return ExecuteSqlOperation(() =>
            {
                if (_isSelectImplemented)
                    throw new Exception("SELECT IS IMPLEMENTED");
                saveModel = _save.Save(param, input, id);
                return this;
            });
        }

        public SqlBuilder<T> Select<TResult>(Expression<Func<T, TResult>> param)
        {
            return ExecuteSqlOperation(() =>
            {
                _isSelectImplemented = true;
                if (selectModel.IsGenerated)
                    return this;
                selectModel.SetSelectClause(_selectClauseService.Select(param));
                return this;
            });
        }
        public SqlBuilder<T> OrderBy(BasePaging param)
        {
            if (param == null)
                return this;
            return ExecuteSqlOperation(() =>
            {
                SelectImplementedCheck();
                selectModel.SetOrderByClause(_orderByClauseService.OrderBy(param));
                return this;
            });
        }
    
        public async Task<ResponseBase<IEnumerable<T>>> Execute()
        {
            SqlContextCache.TrySetValue(_key, selectModel.Generate());
            return await ExecuteQuery(async () => await _execution.Execute(selectModel, _connectionString));
        }

        public async Task<ResponseBase<T>> ExecuteSingle()
        {
            SqlContextCache.TrySetValue(_key, selectModel.Generate());
            return await ExecuteQuerySingle(async () => await _execution.Execute(selectModel, _connectionString));
        }

        public async Task<ResponseBase<int>> ExecuteSave()
        {
            return await _execution.ExecuteSave(saveModel, _connectionString);
        }

        private async Task<ResponseBase<T>> ExecuteQuerySingle(Func<Task<ResponseBase<IEnumerable<object>>>> function)
        {
            //try
            //{
                var res = await function.Invoke();
                return new ResponseBase<T>()
                {
                    Messages = res.Messages,
                    Data = res.Data.FirstOrDefault() as T,
                    Status = res.Status,
                    Count = res.Count
                };
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private async Task<ResponseBase<IEnumerable<T>>> ExecuteQuery(Func<Task<ResponseBase<IEnumerable<object>>>> function)
        {
            //try
            //{
                var res = await function.Invoke();
                return new ResponseBase<IEnumerable<T>>()
                {
                    Messages = res.Messages,
                    Data = res.Data.Select(x => x as T).ToList(),
                    Status = res.Status,
                    Count = res.Count
                };
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void SelectImplementedCheck()
        {
            if (!_isSelectImplemented)
                throw new Exception("SELECT IS NOT IMPLEMENTED");
        }

        private SqlBuilder<T> ExecuteSqlOperation(Func<SqlBuilder<T>> func)
        {
            //try
            //{
                return func.Invoke();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
}
