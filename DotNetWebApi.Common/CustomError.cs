using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace DotNetWebApi.Common
{
    public class CustomError
    {
        public Dictionary<string, IEnumerable<string>> ValidationErrors { get; set; }
        public ErrorCodes ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        // TODO(jzywien): remove StackTrace once Splunk is set up
        public string StackTrace { get; set; }

        public CustomError(Exception ex)
        {
            ErrorCode = ex is CustomException 
                ? ((CustomException)ex).ErrorCode 
                : ErrorCodes.GenericError;
            ErrorMessage = ex.Message;
            StackTrace = ex.StackTrace;
        }

        public CustomError(ModelStateDictionary modelState)
        {
            ErrorMessage = "Model Validation Failed";
            ErrorCode = ErrorCodes.ValidationError;
            ValidationErrors = GetValidationErrors(modelState);
        }

        private static Dictionary<string, IEnumerable<string>> GetValidationErrors(ModelStateDictionary modelState)
        {
            if (modelState.Values == null) return null;

            Dictionary<string, IEnumerable<string>> validationErrors = new Dictionary<string, IEnumerable<string>>();

            foreach (var item in modelState)
            {
                if (item.Value.Errors == null) continue;

                var errorList = item.Value.Errors.Select(e => e.ErrorMessage).Distinct();

                validationErrors.Add(item.Key, errorList);
            }

            return validationErrors;
        }
    }
}