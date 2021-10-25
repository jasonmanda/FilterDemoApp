using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemoApp.Filters
{
    public class DebugResultFilter : ResultFilterAttribute
    {
        //First
        // private readonly string _name;
        // private readonly string _value;

        // public DebugResultFilter(string name, string value)
        // {
        //     _name = name;
        //     _value = value;
        // }

        // public override void OnResultExecuting(ResultExecutingContext context)
        // {
        //     context.HttpContext.Response.Headers.Add(_name, new string[] { _value });
        //     base.OnResultExecuting(context);
        // }




        ///Second

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("Author", new string[] { "Jason Mandabrandja" });
            context.HttpContext.Response.Headers.Add("Author-Contact", new string[] { "+221 70 844 68 37|","+221 77 481 32 22|","jasonmandabrandja@gmail.com " });
            base.OnResultExecuting(context);
        }


   
    }
  
}