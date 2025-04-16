using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;
using ErrorOr;
using Mentoria.Services.Mentoring.API.Common.Http;

namespace Mentoria.Services.Mentoring.API.Common.Errors
{
    public class ProblemDetails : ProblemDetailsFactory
    {

        private readonly ApiBehaviorOptions _options;

        public ProblemDetails(ApiBehaviorOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public override Microsoft.AspNetCore.Mvc.ProblemDetails CreateProblemDetails(
            HttpContext httpContext, 
            int? statusCode = null, 
            string? title = null, 
            string? type = null, 
            string? detail = null, 
            string? instance = null)
        {
            statusCode ??= 500;

            var detallesDeProblema = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance
            };

            this.ApplyProblemDetailsDefaults(httpContext, detallesDeProblema, statusCode.Value);

            return detallesDeProblema;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext, 
            ModelStateDictionary modelStateDictionary, 
            int? statusCode = null, 
            string? title = null, 
            string? type = null, 
            string? detail = null, 
            string? instance = null)
        {
            if (modelStateDictionary is null)
            {
                throw new ArgumentNullException(nameof(modelStateDictionary));
            }

            statusCode ??= 400;

            var detallesDeProblema = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Type = type,
                Detail = detail,
                Instance = instance
            };

            if (title != null)
            {
                detallesDeProblema.Title = title;
            }

            this.ApplyProblemDetailsDefaults(httpContext, detallesDeProblema, statusCode.Value);

            return detallesDeProblema;
        }

        private void ApplyProblemDetailsDefaults(
            HttpContext httpContext,
            Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails, 
            int statusCode)
        {
            problemDetails.Status ??= statusCode;

            if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
                problemDetails.Type ??= clientErrorData.Link;
            }

            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            var errors = httpContext?.Items[HttpContextItemsKeys.Errors] as List<Error>;

            if (errors != null)
            {
                problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
            }
        }
    }
}
