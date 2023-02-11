using System.Net;
using System.Text.Json;

namespace DevTest.Server.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            if (_env.IsDevelopment())
            {
                var json = JsonSerializer.Serialize<ResponseModel>(new ResponseModel()
                {
                    Success = false,
                    Message = exception.Message
                });
                return context.Response.WriteAsync(json);
            }

            var content = JsonSerializer.Serialize<ResponseModel>(new ResponseModel()
            {
                Success = false,
                Message = "Sorry error occurred, Please try again "
            });

            return context.Response.WriteAsync(content);
        }
    }

}
