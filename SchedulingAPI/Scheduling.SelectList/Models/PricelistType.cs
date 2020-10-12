using Common.Extensions;
using Localization.Service;

namespace Scheduling.SelectList.Models
{
    public class PricelistType : ISelectListModel
    {
        public long Id => PricelistTypeId;
        public int PricelistTypeId { get; set; }
        private string name;
        public string Name { get => LocalizationService.GetTranslate(name.CamelCaseToKebabCase("label_{0}")); set => name = value; }
        public string Code { get; set; }
    }
}
