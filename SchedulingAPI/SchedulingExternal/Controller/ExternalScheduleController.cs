using Localization.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Adapters;

namespace SchedulingExternal.Controller
{
    [Route("api/externalSchedule")]
    public class ExternalScheduleController : ControllerAdapter
    {
        public ExternalScheduleController(ILocalizationService localization) : base(localization)
        {
        }
    }
}
