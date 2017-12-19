using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DotNetWebApi.Common
{
    public class CustomException : Exception
    {
        public ErrorCodes ErrorCode { get; }

        public CustomException():base() { }
        public CustomException(string message): base(message)
        {
            ErrorCode = ErrorCodes.GenericError;
        }
        public CustomException(string message, ErrorCodes code) : base(message) { ErrorCode = code; }
        public CustomException(String message, Exception innerException): base(message, innerException) { }
        protected CustomException(SerializationInfo info, StreamingContext context): base(info, context) { }

    }
}