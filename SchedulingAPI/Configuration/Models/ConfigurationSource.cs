using Configuration.Providers;
using Configuration.Services;
using Microsoft.Extensions.Configuration;

namespace Configuration.Models
{
    public class ConfigurationSource : IConfigurationSource
    {
        private IConfigurationService _configurationService;

        public ConfigurationSource(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CustomConfigurationProvider(_configurationService);
        }
    }
}
