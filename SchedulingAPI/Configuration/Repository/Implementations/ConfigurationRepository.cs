using Configuration.Models;
using Entity.Base;
using SQLContext;
using SQLContext.Factories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Configuration.Repository
{
    public class ConfigurationRepository : RepositoryBase, IConfigurationRepository
    {
        public ConfigurationRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<ResponseBase<IEnumerable<ConfigurationModel>>> GetConfiguration() =>
            await QueryMultipleAsync(
              "[dbo].[sysConfiguration_SelectAll]",
              (reader) => reader.Read<ConfigurationModel>()
            );
    }
}
