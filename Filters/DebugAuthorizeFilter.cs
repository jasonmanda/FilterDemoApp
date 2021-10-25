using Microsoft.AspNetCore.Mvc.Filters;
using System;
namespace FilterDemoApp.Filters
{
public class DebugAuthorizeFilter :Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            Console.WriteLine($"session:{context.HttpContext.User}");
        }
    }
  
}