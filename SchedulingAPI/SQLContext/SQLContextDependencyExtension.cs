using Microsoft.Extensions.DependencyInjection;
using SQLContext.Services.Implementations;
using SQLContext.Services.Interfaces;
using System;

namespace SQLContext
{
    public static class SQLContextDependencyExtension
    {
        public static void AddSQLContextDependencies<
            ExecutionService, 
            SelectClauseService, 
            WhereClauseService, 
            OrderByClauseService, 
            JoinClauseService>(this IServiceCollection services) 
                where ExecutionService : class, ISqlContextExecution
                where SelectClauseService : class, ISelectClauseService
                where OrderByClauseService : class, IOrderByClauseService
        {
            services.AddTransient<ISqlContextExecution, ExecutionService>(x => Activator.CreateInstance<ExecutionService>());
            services.AddSingleton<ISelectClauseService, SelectClauseService>(x => Activator.CreateInstance<SelectClauseService>());
            services.AddSingleton<IOrderByClauseService, OrderByClauseService>(x => Activator.CreateInstance<OrderByClauseService>());
        }

        public static void AddSQLContextDependencies(this IServiceCollection services)
        {
            services.AddTransient<ISqlContextExecution, DapperExecution>(x => new DapperExecution());
            services.AddSingleton<ISelectClauseService, SelectClauseService>(x => new SelectClauseService());
            services.AddSingleton<IOrderByClauseService, OrderByClauseService>(x => new OrderByClauseService());
            services.AddSingleton<ISaveService, SaveService>(x => new SaveService());
        }
    }
}
