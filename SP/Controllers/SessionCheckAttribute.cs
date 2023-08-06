using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SP.Controllers
{
    public class SessionCheckAttribute : ActionFilterAttribute
    {
      
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            var email = context.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
            {
                // User is not logged in, redirect to login page
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                    { "controller", "Account" },
                    { "action", "Login" }
                    });
            }

            base.OnActionExecuting(filterContext);
        }
    }

}

