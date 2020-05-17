using JWT_SampleApp.TokenManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace JWT_SampleApp.filters
{
    public class TokenAuthorise : AuthorizeAttribute
    {
        private HttpResponseMessage response;
        private readonly string[] AllowedRoles;
        public TokenAuthorise(params string[] roles)
        {
            this.AllowedRoles = roles;
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool Authorise = false;
            response = this.CreateUnauthorisedResponse(actionContext);
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (AllowedRoles.Count() <= 0)
                {
                    Authorise = true;
                }
                else
                {
                    var roles = HttpContext.Current.User.GetClaimValue(ClaimTypes.Role);
                    if (AllowedRoles.Any(r => r.Contains(roles)))
                    {
                        Authorise = true; /* return true if current user is in a specific role */
                    }
                    else
                    {
                        response = this.CreateForbiddenResponse(actionContext);
                    }
                }
            }
            return Authorise;
        }
        private HttpResponseMessage CreateUnauthorisedResponse(HttpActionContext context)
        {
            HttpError error = new HttpError();
            error.Message = "Authorisation Error";
            return context.Request.CreateResponse(HttpStatusCode.Unauthorized, error);
        }

        private HttpResponseMessage CreateForbiddenResponse(HttpActionContext actionContext)
        {
            HttpError error = new HttpError();
            error.Message = "Forbidden response";
            return actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, error);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = response;
        }
    }
}