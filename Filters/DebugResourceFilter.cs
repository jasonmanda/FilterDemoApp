using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemoApp.Filters
{
    public class DebugResourceFilter : IResourceFilter//IResourceFilter,Attribute
    {
        private readonly UserManager<IdentityUser> _userManager;
        public DebugResourceFilter(UserManager<IdentityUser> userManager){
            _userManager=userManager;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        //    Console.WriteLine($"{context.ActionDescriptor.DisplayName}");
        }

        public  void OnResourceExecuting(ResourceExecutingContext context)
        {
            // var user=await _userManager.GetUserAsync(context.HttpContext.User);
            // Console.WriteLine($"{user}");
        //    Console.WriteLine($"{context.ActionDescriptor.DisplayName}");
        }

   
    }
  
}