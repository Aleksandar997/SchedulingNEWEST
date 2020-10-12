using Common.Base;
using Entity.Base;
using Microsoft.Extensions.Caching.Memory;
using SQLContext.Builders;
using SQLContext.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SQLContext.Factories
{
    public class SqlContextFactory
    {
        public static SqlBuilder<T> Instance<T>(string connectionString, string key) where T : class =>
            new SqlBuilder<T>(SqlContextCache.TryGetValue(key), connectionString, key);


        public static SqlBuilder<T> Instance<T>(string connectionString) where T : class =>
            new SqlBuilder<T>(connectionString);
    }

    internal static class SqlContextCache
    {
        private static readonly IMemoryCache Cache = DependencyInjectionResolver.GetService<IMemoryCache>();

        public static SelectModel TryGetValue(string key)
        {
            SelectModel cache = null;
            if (key != null)
                Cache.TryGetValue(key, out cache);
            return cache;
        }

        internal static void TrySetValue(string key, SelectModel selectModel)
        {
            if (key != null)
                Cache.Set(key, selectModel);
        }
    }
}
