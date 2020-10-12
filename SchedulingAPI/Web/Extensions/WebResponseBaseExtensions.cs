using Entity.Base;
using Localization.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Web.Extensions
{
    public static class WebResponseBaseExtensions
    {
        public static IActionResult ToAutoResponse<T>(this ResponseBase<T> result)
        {
            if (result.Messages != null)
                result.Messages.ForEach(m =>
                {
                    m.Value = LocalizationService.GetTranslate(m.Code);
                });
            switch (result.Status)
            {
                case ResponseStatus.Success:
                    return new OkObjectResult(result);
                default:
                    var internalServerError = new ObjectResult(result);
                    internalServerError.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return internalServerError;
            }
        }

        public static async Task<IActionResult> ToAutoResponse<T>(this Task<ResponseBase<T>> result) => (await result).ToAutoResponse();
    }
}
