using CodebookManagement.Attributes;
using CodebookManagement.Models;
using CodebookManagement.Repository;
using Common.Base;
using Entity.Base;
using SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace CodebookManagement.Service
{
    internal class CodebookCacheModel<T>
    {
        public Expression<Func<T, T>> Func { get; set; }
        public List<JoinModel> Joins { get; set; }
        public List<CodebookColumn> Columns { get; set; }
        public CodebookCacheModel(Expression<Func<T, T>> func, List<JoinModel> joins, List<CodebookColumn> columns)
        {
            Func = func;
            Joins = joins;
            Columns = columns;
        }

        public CodebookCacheModel(List<CodebookColumn> columns)
        {
            Columns = columns;
        }
    }
    internal class CodebookCache
    {
        //internal static readonly IMemoryCache Cache = DependencyInjectionResolver.GetService<IMemoryCache>();
    }
    public class CodebookService : ICodebookService
    {
        ICodebookRepository _codebookRepositroy;
        
        public CodebookService(ICodebookRepository codebookRepositroy)
        {
            _codebookRepositroy = codebookRepositroy;
        }

        public async Task<ResponseBase<IEnumerable<T>>> SelectAll<T>(Expression<Func<T, object>> columns, Func<T, bool> Filter, CodebookPaging paging) where T : class =>
            (await GetData(columns, paging)).Bind(x => x.Where(x => Filter(x)));


        public async Task<ResponseBase<CodebookOutputModel<T>>> SelectById<T>(Func<T, bool> Filter) where T : class
        {
            throw new NotImplementedException();
            //var res = await GetData<T>();
            //return ResponseBase<CodebookOutputModel<T>>.ReturnResponse(
            //    new CodebookOutputModel<T>(
            //        res.Data.Columns,
            //        res.Data.Data.FirstOrDefault(x => Filter(x))),
            //    res.Status,
            //    res.Messages
            //);
        }

        public async Task<ResponseBase<int>> Save<T>(T request) where T : class, ICodebook
        {
            throw new NotImplementedException();
            //var type = typeof(T);
            //var cacheName = "Cache_" + type.Name;
            //CodebookCache.Cache.TryGetValue(cacheName, out CodebookCacheModel<T> cache);
            //if (cache == null)
            //{
            //    var joins = new List<JoinModel>();
            //    var columns = new List<CodebookColumn>();
            //    GetJoinAttributes(type, ref joins, ref columns);
            //    cache = new CodebookCacheModel<T>(columns);
            //}
            //cache.Columns = cache.Columns.Where(x => x.Editable).ToList();
            //var expression = ExpressionBuilderFactory
            //    .Instance()
            //    .NewExpression(
            //        NewExpressionModel.Instance(cache.Columns.Select(x => NewExpressionColumnModel.Instance(x.Name.Split(".").LastOrDefault(), x.Type)), type)
            //    )
            //    .LambdaExpression<T, T>() as Expression<Func<T, T>>;
            //return await _codebookRepositroy.Save(expression, request, cacheName, request.Id);
        }


        public async Task<ResponseBase<IEnumerable<T>>> GetData<T>(Expression<Func<T, object>> columns, CodebookPaging paging) where T : class =>
            await _codebookRepositroy.SelectAll(columns, paging, "Select" + typeof(T).Name);

        Task<ResponseBase<int>> ICodebookService.Save<T>(T value, Expression<Func<T, object>> addMapping)
        {
            throw new NotImplementedException();
        }

        Task<ResponseBase<int>> ICodebookService.Save<T>(T value, Expression<Func<T, object>> addMapping, int id)
        {
            throw new NotImplementedException();
        }
    }
}

