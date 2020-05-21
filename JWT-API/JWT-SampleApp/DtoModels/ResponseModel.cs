using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT_SampleApp.DtoModels
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
    }
}