using Fiby.Cloud.Cross.Util.DTOGeneric;
using Fiby.Cloud.Web.Util.DTOGeneric;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Filters
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                //var _logger = scope.ServiceProvider.GetService<ILogger>();

                _ = app.UseExceptionHandler(appError =>
                {
                    appError.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null)
                        {

                            var err = $"Error: {contextFeature.Error.Message}";

                            //_logger.LogCritical(err);

                            await context.Response.WriteAsync(new ResponseObject<DefaultResponse>()
                            {
                                Data = new DefaultResponse() { },
                                Success = false,
                                Details = new ErrorDetails()
                                {
                                    StatusCode = context.Response.StatusCode,
                                    Message = err
                                }
                            }.ToString());
                        }
                    });
                });
            }
        }
    }
}
