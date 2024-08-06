using CrossCutting.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PanteonWorkDemo.Models.Base;

namespace PanteonWorkDemo.Filters
{
    /*TOKEN CHECK ŞABLONU*/
    public class TokenAuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string token = filterContext.HttpContext.Request.Headers["token"].ToString();

            if (string.IsNullOrEmpty(token))
            {
                filterContext.Result = new ObjectResult(new ApiResult
                {
                    IsValid = false,
                    ErrorMessages = new List<string> { "Geçersiz İstek (Token Bulunamadı)" }
                })
                {
                    StatusCode = 401
                };
                return;
            }

            if (!TokenHelper.ValidateToken(token))
            {
                filterContext.Result = new ObjectResult(new ApiResult
                {
                    IsValid = false,
                    ErrorMessages = new List<string> { "Geçersiz İstek (Token Geçersiz veya Süresi Dolmuş)" }
                })
                {
                    StatusCode = 401
                };
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}