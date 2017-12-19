using DotNetWebApi.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DotNetWebApi.Common
{
    public class BaseController : ApiController
    {
        protected BaseController() { }

        protected IHttpActionResult Try<T>(Func<T> tryFunc)
        {
            return Impl(tryFunc);
        }

        protected async Task<IHttpActionResult> TryAsync<T>(Func<Task<T>> tryFuncAsync)
        {
            return await ImplAsync<T>(tryFuncAsync).ConfigureAwait(false);
        }
        protected async Task<IHttpActionResult> TryAsync(Func<Task> tryFuncAsync)
        {
            return await ImplAsync(tryFuncAsync).ConfigureAwait(false);
        }

        protected IHttpActionResult Try(Action tryAction)
        {
            try
            {
                tryAction();

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        private IHttpActionResult Impl<T>(Func<T> tryFunc)
        {
            try
            {
                var result = tryFunc();

                if (result == null) throw new ResultNotFoundException("Result Not Found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private async Task<IHttpActionResult> ImplAsync<T>(Func<Task<T>> tryFuncAsync)
        {
            try
            {
                var result = await tryFuncAsync().ConfigureAwait(false);

                if (result == null)
                {
                    throw new ResultNotFoundException("Result Not Found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex).ConfigureAwait(false);
            }
        }

        private async Task<IHttpActionResult> ImplAsync(Func<Task> tryFuncAsync)
        {
            await tryFuncAsync().ConfigureAwait(false);
            return Ok();
        }

        private Task<IHttpActionResult> HandleExceptionAsync(Exception ex)
        {
            return Task.FromResult(HandleException(ex));
        }

        private IHttpActionResult HandleException(Exception ex)
        {
            var status = ExceptionHelper.GetStatusCodeForException(ex);
            var error = new CustomError(ex);

            return ResponseMessage(Request.CreateResponse(status, error));
        }

    }
}