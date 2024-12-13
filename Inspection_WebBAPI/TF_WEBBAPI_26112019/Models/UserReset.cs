using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class UserReset
    {
        public string UserName { get; set; }
        public string OldPass { get; set; }
        public string Password { get; set; }
        public string ConfirmPass { get; set; }
    }
}