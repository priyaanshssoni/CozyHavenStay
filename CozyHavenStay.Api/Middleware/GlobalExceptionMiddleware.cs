using CozyHavenStay.Api.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace CozyHavenStay.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
       

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
           
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                
                HttpStatusCode status;
                var stackTrace = ex.StackTrace;
                var message = ex.Message;

                // Use a switch expression to handle different exception types
                switch (ex)
                {
                    case NotFoundException _:
                        status = HttpStatusCode.NotFound;
                        break;
                    case BadRequestException _:
                        status = HttpStatusCode.BadRequest;
                        break;
                    case Exceptions.NotImplementedException _:
                        status = HttpStatusCode.NotImplemented;
                        break;
                    case System.UnauthorizedAccessException _:
                        status = HttpStatusCode.Unauthorized;
                        break;
                    case Exceptions.KeyNotFoundException _:
                        status = HttpStatusCode.NotFound;
                        break;
                    case Exceptions.InvalidOperationException _:
                        status = HttpStatusCode.BadRequest;
                        break;
                    case UserNotFoundException _:
                        status = HttpStatusCode.NotFound;
                        break;
                    case RoleNotFoundException _:
                        status = HttpStatusCode.BadRequest;
                        break;
                    case RoleAlreadyAssignedException _:
                        status = HttpStatusCode.BadRequest;
                        break;
                    default:
                        status = HttpStatusCode.InternalServerError;
                        break;
                }

                await HandleExceptionAsync(context, status, message);
            }
        }
        public void FireMail() { }
        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            var response = new
            {
                StatusCode = (int)statusCode,
                Message = message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var jsonResponse = JsonConvert.SerializeObject(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }

}
