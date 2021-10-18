using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;

namespace FilterDemoApp.Filters
{
    public class ProduitFilter : IActionFilter
    {
        private readonly string _name;
        private readonly string _value;

        public ProduitFilter(string name, string value)
        {
            _name = name;
            _value = value;
        }
        public new void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
            MyDebug.Write(MethodBase.GetCurrentMethod(), context.HttpContext.Request.Path);
        }

        public new void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
            MyDebug.Write(MethodBase.GetCurrentMethod(), context.HttpContext.Request.Path);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_name, new string[] { _value });
            base.OnResultExecuting(context);
        }
    }
    public static class MyDebug
    {
        public static void Write(MethodBase m, string path)
        {
            Debug.WriteLine(m.ReflectedType.Name + "." + m.Name + " " +
                path);
        }
    }
}