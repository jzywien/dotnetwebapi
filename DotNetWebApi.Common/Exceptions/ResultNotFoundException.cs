using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetWebApi.Common.Exceptions
{
    public class ResultNotFoundException : Exception
    {
        public ResultNotFoundException(string Message) : base(Message)
        {
        }

        public ResultNotFoundException(string[] Message) : base(Message.ToString())
        {
        }
    }
}