using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class ResultResponse
    {
        public object Result { get; set; }
        public string Key { get; set; }
        public string Status { get; set; }
    }

    public class ResponseResult
    {
        public object LoginResult { get; set; }
        public object MCDPDF { get; set; }
        public object MCDResult { get; set; }
        public string Key { get; set; }
    }
}