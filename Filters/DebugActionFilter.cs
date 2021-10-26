using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using FilterDemoApp.Models;

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
            // Console.WriteLine($"[Action filter] {context.ActionDescriptor.DisplayName}");

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var param = context.ActionArguments.SingleOrDefault(p => p.Value is Produit);
            var entity = param.Value as Produit;
            if (entity == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }
            else
            {
                if (entity.Libelle is null)
                {
                    context.Result = new BadRequestObjectResult("Object is null");
                    return;
                }
            }


            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            //  var user=await _userManager.GetUserAsync(context.HttpContext.User);
            //         Console.WriteLine($"{user}");
            //        Console.WriteLine($"[Action filter]1 {context.ActionDescriptor.DisplayName}");

        }


    }

}