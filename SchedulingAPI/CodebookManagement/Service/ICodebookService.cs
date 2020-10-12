using CodebookManagement.Models;
using Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodebookManagement.Service
{
    public interface ICodebookService
    {
        Task<ResponseBase<IEnumerable<T>>> SelectAll<T>(Expression<Func<T, object>> columns, Func<T, bool> Filter = null, CodebookPaging paging = null) where T : class;
        //Task<ResponseBase<int>> Save<T>(Expression<Action<List<T>>> param) where T : class, ICodebook;
        //Task<ResponseBase<int>> Save<T>(T value, Expression<Action<List<object>>> addMapping) where T : class, ICodebook;
        Task<ResponseBase<int>> Save<T>(T value, Expression<Func<T, object>> addMapping) where T : class, ICodebook;
        Task<ResponseBase<int>> Save<T>(T value, Expression<Func<T, object>> addMapping, int id) where T : class, ICodebook;
    }
}
