using Entity;
using Microsoft.Extensions.DependencyInjection;
using Scheduling.SelectList.Repository;

namespace Scheduling.SelectList
{
    public static class SelectListDependencyExtension
    {
        private static string connectionString => AppSettings.Instance.Database.ConnectionString;
        public static void AddSelectListDependencies(this IServiceCollection services)
        {
            services.AddSingleton<ISelectListRepository, SelectListRepository>(x => new SelectListRepository(connectionString));
        }
    }
}
