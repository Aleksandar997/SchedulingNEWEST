using Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web.Attributes;

namespace Web.Extensions
{
    public static class ModelStateExtensions
    {
        public static void RemoveChildValidation<T>(this ModelStateDictionary modelState, T model)
        {
            //bool IsValid(object val, ValidationType validationType)
            //{
            //    switch (validationType)
            //    {
            //        case ValidationType.IsNull:
            //            return val != null;
            //        case ValidationType.IsZeroOrNull:
            //            return val != null && val.ToString() != "0";
            //        default:
            //            return true;
            //    }
            //}
            if (model == null) return;
            var objectClassName = model.GetType().Name;
            foreach (var prop in model.GetType().GetMembers().Where(p => p.IsDefined(typeof(ChildValidation))))
            {
                var requiredModel = prop.GetCustomAttribute<ChildValidation>().model;
                foreach (var item in requiredModel)
                {
                    var requiredFields = item.RequiredProperties;
                    var requiredProp = item.RequiredProp;
                    var requiredValue = item.RequiredValue;
                    var validationType = item.ValidationType;
                    modelState
                        .Where(e =>
                                e.Key.StartsWith(objectClassName + "." + prop.Name) ||
                                e.Key.StartsWith(prop.Name) ||
                                (
                                    requiredProp == null ||
                                    (
                                        prop.Name == requiredProp &&
                                        (string)model.GetNested(prop.Name) == requiredValue
                                    )
                                )
                               )
                        .ToList()
                        .ForEach(error =>
                        {
                            if (requiredFields == null || !requiredFields.Contains(error.Key))
                                modelState.Remove(error.Key);
                        });

                    //if (typeof(IEnumerable).IsAssignableFrom(((PropertyInfo)prop).PropertyType))
                    //{
                    //    requiredFields.ToList().ForEach(r =>
                    //    {
                    //        var props = (model.GetType().GetProperty(prop.Name).GetValue(model) as IEnumerable).Cast<object>();
                    //        for (int i = 0; i < props.Count(); i++)
                    //        {
                    //            var modelStateKey = $"{prop.Name}[{i}].{r}";
                    //            var element = props.ElementAt(i);
                    //            var isValid = IsValid(element.GetType().GetProperty(r).GetValue(element), validationType);
                    //            if (modelState.ContainsKey(modelStateKey) || IsValid(element.GetNested(r), validationType))
                    //                continue;
                    //            modelState.AddModelError(modelStateKey, $"{r.Split(".").LastOrDefault()}_required");
                    //        }
                    //    });
                    //    return;
                    //}
                    //requiredFields.ToList().ForEach(r =>
                    //{
                    //    if (!modelState.ContainsKey(r) && !IsValid(model.GetNested(r), validationType))
                    //       modelState.AddModelError(r, $"{r.Split(".").LastOrDefault()}_required");
                    //});
                }

            }
        }

        public static ModelStateDictionary SkipChildValidation<T>(this ModelStateDictionary modelState, T request)
        {
            modelState.RemoveChildValidation(request);
            return modelState;
        }


        public async static Task<IActionResult> OnValidation(this ModelStateDictionary modelState, Func<Task<IActionResult>> next) =>
            modelState.IsValid ? await next() : new BadRequestObjectResult(modelState);
    }
}
