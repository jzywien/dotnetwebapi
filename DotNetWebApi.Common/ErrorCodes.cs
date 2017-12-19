using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetWebApi.Common
{
    public enum ErrorCodes
    {
        /// <summary>
        /// Generic Error
        /// </summary>
        GenericError = 100,
        /// <summary>
        /// Invalid Model State
        /// </summary>
        ValidationError = 101
    }
}