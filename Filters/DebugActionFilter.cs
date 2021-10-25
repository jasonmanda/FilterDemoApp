using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemoApp.Filters
{
    public class DebugActionFilter : ActionFilterAttribute
    {
        private readonly UserManager<IdentityUser> _userManager;
        public DebugActionFilter(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
               Console.WriteLine($"[Action filter] {context.ActionDescriptor.DisplayName}");

        }
        public override async void OnActionExecuting(ActionExecutingContext context)
        {

             var user=await _userManager.GetUserAsync(context.HttpContext.User);
                    Console.WriteLine($"{user}");
                   Console.WriteLine($"[Action filter]1 {context.ActionDescriptor.DisplayName}");

        }


    }

}