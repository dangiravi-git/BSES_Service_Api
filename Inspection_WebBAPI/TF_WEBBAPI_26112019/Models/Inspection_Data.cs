using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class Inspection_Data
    {
        public string ID { get; set; }
        public string ISTABCASE { get; set; }
        public string CA_ORDERNO { get; set; }
        public string METER_NO { get; set; }
        public string CA_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDERTYPE { get; set; }
        public string ACTIVITYTYPE { get; set; }
        public string ACTIVITYDATE { get; set; }
        public string INSTAL_METER { get; set; }
        public string INSTAL_CABLE { get; set; }
        public string INSTAL_BUSBARREMARK { get; set; }
        public string INSTAL_SEALING { get; set; }
        public string INSTAL_METERREMARKS { get; set; }
        public string INSTAL_CABLEREMARK { get; set; }
        public string INSTAL_BUSBAR { get; set; }
        public string INSTAL_SEALINGREMARK { get; set; }
        public string QUALITY_METER { get; set; }
        public string QUALITY_CABLE { get; set; }
        public string QUALITY_FEEDINGPOINTS { get; set; }
        public string QUALITY_MARKING { get; set; }
        public string QUALITY_METERREMARK { get; set; }
        public string QUALITY_CABLEREMARK { get; set; }
        public string QUALITY_FEEDINGPOINTSREMARK { get; set; }
        public string QUALITY_MARKINGREMARK { get; set; }
        public string INSTALLATIONSAFETY { get; set; }
        public string INSTALLATIONSAFETYREMARK { get; set; }
        public string OTHEROBSERVATION { get; set; }
        public string INSTALLATIONPHOTO { get; set; }
        public string CREATEDDATE { get; set; }
        public string CREATEDBY { get; set; }
        public string DIVISION { get; set; }
        public string INSPECTION_TYPE { get; set; }

        public string MONTH_YEAR { get; set; }

        //Added by chesta on 19-09-2024
        public string INSPECTEDALONG { get; set; }
        public string LINEMANNAME { get; set; }

    }
}