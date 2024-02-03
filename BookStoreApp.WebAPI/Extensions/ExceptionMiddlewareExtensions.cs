using BookStoreApp.Core.ResponseResultPattern.ErrorModel;
using BookStoreApp.Core.ResponseResultPattern.Exceptions;
using BookStoreApp.Core.Services;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BookStoreApp.WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                  

                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>(); // uygulama hatalarını alıyoruz.



                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            // Service katmanından fırlatılan hata eğer notfoundException ise StatusCode 404 olarak setleyelim.
                            NotFoundException => StatusCodes.Status404NotFound,
                            NotMatchedException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        // hatayı loglayalım.
                        logger.LogError($"Something went wrong : {contextFeature.Error}");
                        // Response'u kendi hata modelimizi kullanarak oluşturalım. ErrorModel

                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());


                    }

                });
            });
        }
    }
}
