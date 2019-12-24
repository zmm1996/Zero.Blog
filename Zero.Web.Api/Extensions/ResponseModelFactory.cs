using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Core.Response;

namespace Zero.Web.Api.Extensions
{
    public class ResponseModelFactory
    {
        public static ResponseModel CreateInstance => new ResponseModel();
        public static ResponseResultModel CreateResultInstance => new ResponseResultModel();
    }
}
