using System;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Services.ApprenticeService;
using apiv2.Utilities.JWTUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace apiv2.Utilities
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        public JWTMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext httpContext, IApprenticeService apprenticeService, IJWTUtils jWTUtils)
        {
            // Bearer -token-

            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userID = jWTUtils.ValidateJWTToken(token);

            if(userID != Guid.Empty)
            {
                httpContext.Items["Apprentices"] = apprenticeService.GetById(userID);
            }

            await _next(httpContext);
        }
    }
}
