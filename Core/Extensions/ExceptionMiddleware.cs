using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    //middleware klasörü yapılabilir
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
            catch (Exception e)
            {
                // ignored
            }
            finally
            {

            }
        }

        private  Task  HandleExceptionAsync(HttpContext httpContext, Exception e,string message="")
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            if (e.GetType() == typeof(ValidationException))
            {
                message = e.Message;
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                 Message = message
            }.ToString());
        }
         
    }
}
