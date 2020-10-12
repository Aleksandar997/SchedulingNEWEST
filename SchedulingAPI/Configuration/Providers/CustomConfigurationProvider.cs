using Configuration.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Configuration.Providers
{
    public class CustomConfigurationProvider : ConfigurationProvider
    {
        private IConfigurationService _configurationService;

        public CustomConfigurationProvider(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public override void Load()
        {
            var configData = _configurationService.GetConfiguration().Result;
            Data = configData.Data?.ToDictionary(c => c.Path, c => c.Value) ?? new Dictionary<string, string>();
        }
    }
}
