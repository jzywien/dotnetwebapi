using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DotNetWebApi.Common.Attributes
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            if (actionContext.ModelState.IsValid) return;

            var error = new CustomError(actionContext.ModelState);

            actionContext.Response = request.CreateResponse(HttpStatusCode.BadRequest, error);
        }
    }
}