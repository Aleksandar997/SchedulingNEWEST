using Entity;
using Microsoft.Extensions.DependencyInjection;
using SchedulingExternal.Repository.Implementations;
using SchedulingExternal.Repository.Interfaces;
using System;

namespace SchedulingExternal
{
    public static class SchedulingExternalDependencyExtension
    {
        private static string ConnectionString => AppSettings.Instance.Database.ScheduleExternalConnectionString;
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IExternalScheduleRepository, ExternalScheduleRepository>(x => new ExternalScheduleRepository(ConnectionString));
        }
    }
}
