using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieMagic.DTO;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MovieMagic.WebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly IHostEnvironment _env;
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ApiError response;
                var statusCode = HttpStatusCode.InternalServerError;

                // [INFO] - Custom guid code to be displayed on UI
                //          which can be traced back in log files easily
                var errorGuid = Guid.NewGuid().ToString("N");

                if (_env.IsDevelopment())
                {
                    response = new ApiError((int)statusCode, errorGuid, ex.Message, ex.StackTrace.ToString());
                }
                else
                {
                    response = new ApiError((int)statusCode, errorGuid, ex.Message);
                }

                _logger.LogError(ex, $"{errorGuid} - {ex.Message}");
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response.ToString());
            }
        }
    }
}
