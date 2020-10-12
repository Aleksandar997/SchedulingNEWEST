using SQLContext.Models;
using System;
using System.Linq.Expressions;

namespace SQLContext.Services.Interfaces
{
    public interface ISaveService
    {
        SaveModel Save<T, TResult>(Expression<Func<T, TResult>> param, T input, int id) where T : class;
    }
}
