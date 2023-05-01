using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NetCoreWebAPI.MiddleWare
{
    public class ExceptionMessage
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        public ExceptionMessage(ILogger<Exception> logger, RequestDelegate next)
        {
            this._logger = logger;
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                //Log the Exception
                _logger.LogError(ex.Message);


                //Should Return the Custom Error Message
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorMesage = new
                {
                    ErrorMessage = "Something went wrong. Please try again after some time"
                };

                await context.Response.WriteAsJsonAsync(errorMesage);

            }
        }

    }
}
