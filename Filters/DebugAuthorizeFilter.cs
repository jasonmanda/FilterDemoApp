using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
namespace FilterDemoApp.Filters
{
public class DebugAuthorizeFilter : IAuthorizationFilter//IAuthorizationFilter,Attribute
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.User==null)context.Result=new UnauthorizedObjectResult("L'utilisateur n'est pas connecté");
            else {
                if(!context.HttpContext.User.Identity.IsAuthenticated)context.Result=new UnauthorizedObjectResult("L'utilisateur n'est pas connecté");
            }
            // Console.WriteLine($"session:{context.HttpContext.User}");
        }
    }
  
}