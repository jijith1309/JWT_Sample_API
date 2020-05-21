using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JWT_SampleApp.filters
{
    public class ValidateModel : ActionFilterAttribute
    {
        private readonly dynamic errorResponse;
        public ValidateModel()
        {
            errorResponse = new ExpandoObject();
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //if (actionContext.(actionContext))
            //{
            //    this.GenerateModelErrors(actionContext);
            //}
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = this.CreateResponse(actionContext);
            }
            base.OnActionExecuting(actionContext);
        }

        #region PrivateMethods
        private dynamic CreateResponse(HttpActionContext actionContext)
        {
            var errorList = actionContext.ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage == "" ? (e.Exception != null ? e.Exception.ToString() : "Input Data is no valid") : e.ErrorMessage).Where(x => x != "").ToList();
            errorList = errorList == null ? new List<string>() : errorList;
            HttpError error = new HttpError();
            error.Message = errorList[0];
            return actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, error);

        }
        #endregion
    }
}