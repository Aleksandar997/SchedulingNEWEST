using Common.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Configuration.Models
{
    [Table("Configuration")]
    public class ConfigurationModel : ITreeview<ConfigurationModel>
    {
        [PrimaryKey]
        public int ConfigurationId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int ParentId { get; set; }
        public string Path { get; set; }
        public List<ConfigurationModel> Children { get; set; } = new List<ConfigurationModel>();
    }

}
