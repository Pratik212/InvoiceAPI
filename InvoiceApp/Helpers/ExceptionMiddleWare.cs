using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;
using InvoiceApp.Dtos;  
using InvoiceApp.Exceptions;
using InvoiceApp.Wrappers;

namespace InvoiceApp.Helpers
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Log.Error($"Error in {httpContext.Request.Path}: {ex}");

                var response = httpContext.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string> {Succeeded = false, Message = ex?.Message};

                switch (ex)
                {
                    case ApiExceptions e:
                        // custom application error
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        break;
                    case ValidationException e:
                        // custom application error
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);

                // await HandleExceptionAsync(httpContext, ex);
            }
        }
        
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}