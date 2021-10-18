using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace FilterDemoApp.Filters
{
        // [RequireHttps(Permanent =false)] utilisez cet attribut qui fait la même chose en mieux pour gagner du temps

    public class HttpsFilter : Attribute, IAuthorizationFilter
    {
        private IWebHostEnvironment _env;

        public HttpsFilter(IWebHostEnvironment hostEnv) => _env = hostEnv;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_env.IsDevelopment())
                if (!context.HttpContext.Request.IsHttps)
                    context.Result = new ForbidResult();


        }
    }
}