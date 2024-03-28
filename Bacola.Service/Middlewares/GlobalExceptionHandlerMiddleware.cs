using System;
using Microsoft.AspNetCore.Http;

namespace Bacola.Service.Middlewares
{
	public class GlobalExceptionHandlerMiddleware
	{
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next )
		{
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
           
                try
                {
                    await _next.Invoke(context);
                }
                catch (Exception E)
                {
                context.Response.Redirect("/Shared/ErrorPage");
                }
            
        }
	}
}

