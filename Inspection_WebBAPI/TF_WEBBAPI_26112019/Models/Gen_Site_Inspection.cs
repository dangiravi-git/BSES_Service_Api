using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class Gen_Site_Inspection
    {        
        public string INSPECTIONTYPE { get; set; }
        public string INSPECTIONPURPOSE { get; set; }
        public string CONSUMERNAME { get; set; }
        public string CONSUMERADDRESS { get; set; }
        public string CONSUMERPHONENO { get; set; }
        public string SITEOBSERVATION { get; set; }
        public string SITEPHOTO { get; set; }
        public string UPLOADBY { get; set; }
        public string INSPECTEDALONG { get; set; }
        public string LINEMANNAME { get; set; }

        //Added by chesta on 19-09-2024
        public string CIRCLE { get; set; }        
        public string DIVISION { get; set; }        
    }
}