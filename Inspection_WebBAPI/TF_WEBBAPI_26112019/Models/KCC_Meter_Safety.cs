using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class KCC_Meter_Safety
    {
        public string UNIQUE_REF_NO { get; set; }
        public string CA_NO { get; set; }
        public string METER_NO { get; set; }
        public string DIVISION { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE_NO { get; set; }
        public string REQUEST_TYPE { get; set; }
        public string METER_SAFE_LOCATION { get; set; }
        public string METER_IMAGE { get; set; }
        public string METER_IMAGE1 { get; set; }
        public string METER_IMAGE2 { get; set; }
        public string REMARKS { get; set; }
        public string PUNCH_DATE { get; set; }
        public string PUNCH_BY { get; set; }
    }
}