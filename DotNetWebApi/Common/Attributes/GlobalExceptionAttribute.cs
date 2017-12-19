using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace DotNetWebApi.Common.Attributes
{
    public class GlobalExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// This is the 
        /// Exceptions handled in this function.
        /// Need not to keep try catch any ware the handler can be handeled hear
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            var exception = actionExecutedContext.Exception;

            var status = ExceptionHelper.GetStatusCodeForException(exception);
            var error = new CustomError(exception);

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(status, error);
        }
    }
}