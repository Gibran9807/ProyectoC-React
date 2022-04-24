using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Extensions
{
    public static class MiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler((appError) => {
                appError.Run(async context => {

                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    
                    if (contextFeature != null )
                    {
                        var response = new Response<string>();

                        response.Errors.Add(contextFeature.Error.Message);

                        await context.Response.WriteAsJsonAsync(response);
                    }
                });
            });
        }
    }
}