using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace JWT_SampleApp.TokenManagement
{

    public class JwtTokenHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token;
            HttpStatusCode statusCode = new HttpStatusCode();
            string responseKey = "";
            var authHeader = request.Headers.Authorization;
            if (authHeader == null)
            {
                // Missing authorization header
                return base.SendAsync(request, cancellationToken);
            }
            if (!TryRetrieveToken(request, out token))
            {
                return CreateErrorResponse(request, HttpStatusCode.Unauthorized, responseKey);
            }
            try
            {
                TokenProvider.ValidateToken(token);
                return base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.Unauthorized;

            }
            return CreateErrorResponse(request, statusCode, responseKey);
        }
        public static Task<HttpResponseMessage> CreateErrorResponse(HttpRequestMessage request, HttpStatusCode status, string responseKey)
        {
            HttpError error = new HttpError();
            error.Message = "Authorisation Error";
            return Task<HttpResponseMessage>.Factory.StartNew(() => request.CreateResponse(status, error));
        }

        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authorizationHeaders;

            if (!request.Headers.TryGetValues("Authorization", out authorizationHeaders) || authorizationHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authorizationHeaders.ElementAt(0);
            token = bearerToken.StartsWith("BEARER ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }
    }
}