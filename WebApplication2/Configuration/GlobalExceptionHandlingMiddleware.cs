using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WarehouseWeb.Model;

namespace WarehouseWeb.Configuration
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            //HttpStatusCode status;
            //var stackTrace = string.Empty;
            //string message = "";

            var exceptionType = ex.GetType();
            string message = ex.Message;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var stackTrace = ex.StackTrace;


            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new { error = message, status = statusCode });
            //Result result = JsonConvert.SerializeObject(new { StatusCode = status });
            return context.Response.WriteAsync(result);
        }

    }
}
