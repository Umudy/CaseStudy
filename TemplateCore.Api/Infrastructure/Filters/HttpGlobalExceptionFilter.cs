using System.Net;
using TemplateCore.Api.Infrastructure.ActionResult;
using TemplateCore.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TemplateCore.Api.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);            

            if (context.Exception is TemplateCoreException || context.Exception is ValidationException)
            {
                var json = new JsonErrorResponse
                {
                    Errors = new[] { context.Exception.Message }
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception is TemplateCoreSecurityException)
            {
                context.Result = new ForbidResult();
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Errors = new[] { "Sistemde beklenmedik bir hata oluştu. Lütfen tekrar deneyiniz." }
                };

                if (env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception;
                }

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }


    }
    public class JsonErrorResponse
    {
        public JsonErrorResponse()
        {
            
        }

        public JsonErrorResponse(string errorMessage)
        {
            Errors = new[] {errorMessage};
        }

        public string[] Errors { get; set; }

        public object DeveloperMessage { get; set; }
    }
}
