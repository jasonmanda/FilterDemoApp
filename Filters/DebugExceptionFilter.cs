using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
namespace FilterDemoApp.Filters
{
    public class DebugExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            
    //         context.Result=new RedirectToRouteResult(new RouteValueDictionary {
    //     { "action", "Get" },
    //     { "controller", "Error" },
    //     {"status",$"{context.HttpContext.Response.StatusCode}"}
    // } );


                    
    //         context.Result=new RedirectToActionResult("Get","Error", new RouteValueDictionary {
    //     {"status",$"{context.HttpContext.Response.StatusCode}"}
    // } );

                context.Result=new RedirectToActionResult("Get","Error", new 
        {status=$"{context.HttpContext.Response.StatusCode}"}
     );
    // https://localhost:5001/Produit?id=45
       
        }
    }

}
