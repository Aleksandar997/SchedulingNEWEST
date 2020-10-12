using Common.Extensions;
using Localization.Service;

namespace Scheduling.SelectList.Models
{
    public class DocumentStatus : ISelectListModel
    {
        public long Id => DocumentStatusId;
        public string Name { get => LocalizationService.GetTranslate(Code.CamelCaseToKebabCase("label_{0}")); set { return; } }
        public int DocumentStatusId { get; set; }
        public string Code { get; set; }
    }
}
