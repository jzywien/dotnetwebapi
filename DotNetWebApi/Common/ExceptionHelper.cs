using DotNetWebApi.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace DotNetWebApi.Common
{
    public static class ExceptionHelper
    {
        public static HttpStatusCode GetStatusCodeForException(Exception exception)
        {
            var exceptionType = exception.GetType();
            HttpStatusCode status = new HttpStatusCode();

            if (exceptionType.Equals(typeof(CustomException)))
            {
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType.Equals(typeof(ResultNotFoundException)))
            {
                status = HttpStatusCode.NotFound;
            }
            else if (exceptionType.Equals(typeof(NotImplementedException)))
            {
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType.Equals(typeof(HttpResponseException)))
            {
                status = ((HttpResponseException)exception).Response.StatusCode;
            }
            else
            {
                status = HttpStatusCode.BadRequest;
            }

            return status;
        }
    }
}