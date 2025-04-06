using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoria.Shared.Setup.API.Key
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IOptions<ApiKeyConfiguration> apiToken)
        {
            if (context.Request.Headers.TryGetValue("apiKey", out StringValues apiKey))
            {
                if (apiKey == apiToken.Value.Value)
                    await _next(context);
                else
                    ReturnApiKeyNotfound();
            }
            else
            {
                ReturnApiKeyNotfound();
            }

            void ReturnApiKeyNotfound()
            {
                throw new UnauthorizedAccessException("The API Key is missing");
            }
        }
    }
}
