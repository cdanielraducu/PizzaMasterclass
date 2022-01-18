using System;
using System.Collections.Generic;
using System.Linq;
using apiv2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace apiv2.Utilities.Attributes
{
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly ICollection<Role> _roles;
        public AuthorizationAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { }; ;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var unauthorizationStatusCodeObject = new JsonResult(new { Message = "Unauthorized" })

            { StatusCode = StatusCodes.Status401Unauthorized };

            if (_roles == null)
            {
                context.Result = unauthorizationStatusCodeObject;
            }

            var apprentice = (Apprentice)context.HttpContext.Items["Apprentices"];
            
            // If the user has the apprentice role then it is unauthorized
            if (apprentice == null || apprentice.Role == Role.Apprentice)
            {
                context.Result = unauthorizationStatusCodeObject;
            }
        }
    }
}
