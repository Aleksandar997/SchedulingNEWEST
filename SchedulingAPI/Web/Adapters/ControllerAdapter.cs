using Common.Base;
using Entity.Base;
using Localization.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Adapters
{
    public class ControllerAdapter : Controller
    {
        protected readonly ILocalizationService Localization;
        protected long UserId;
        protected Guid? CompanyId;
        protected int EmployeeId;
        //protected bool IsAdmin;
        public ControllerAdapter(ILocalizationService localization)
        {
            Localization = localization;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var nameIdentifier = JsonConvert.DeserializeObject(User?.Claims.SingleOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value, typeof(NameIdentifier)) as NameIdentifier;
            EmployeeId = Convert.ToInt32(User?.Claims.SingleOrDefault(p => p.Type == ClaimTypes.UserData)?.Value);
            UserId = nameIdentifier.UserId;
            CompanyId = nameIdentifier.CompanyId;
            //IsAdmin = Convert.ToBoolean(User?.Claims.SingleOrDefault(p => p.Type == ClaimTypes.UserData)?.Value);
            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        protected virtual IActionResult AutoResponse<T>(ResponseBase<T> result)
        {
            if (result.Messages != null)
                result.Messages.ForEach(m =>
                {
                    m.Value = LocalizationService.GetTranslate(m.Code);
                });
            switch (result.Status)
            {
                case ResponseStatus.Success:
                    return Ok(result);
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError, result);
            }
        }

        protected async virtual Task<IActionResult> AutoResponse<T>(Func<Task<ResponseBase<T>>> result, bool removeChildValidation = false)
        {
            //var a = result.Target.GetType().GetFields().LastOrDefault().GetValue(result.Target);
            //if (removeChildValidation)
            //    ModelState.RemoveChildValidation(a);
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            var res = await result();
            return AutoResponse(res);
        }

        protected IActionResult ResponseOk<T>(T data, int count = 0)
        {
            return StatusCode((int)HttpStatusCode.OK,
                new ResponseBase<T>()
                {
                    Status = ResponseStatus.Success,
                    Data = data,
                    Count = count
                });
        }
    }
}
