using Microsoft.AspNetCore.Mvc.Filters;
using System;
namespace FilterDemoApp.Filters
{
    public class DebugExceptionFilter : Attribute,IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
        }
    }

}