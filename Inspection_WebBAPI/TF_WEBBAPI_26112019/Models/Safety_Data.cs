using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class Safety_Data
    {
        public string ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string DIVISION { get; set; }
        public string METER_NO { get; set; }
        public string ORDERID { get; set; }
        public string CA_NO { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string TEL_NO { get; set; }
        public string ACCOUNT_CLASS { get; set; }
        public string SAFETY_SAFE_LOC { get; set; }
        public string SAFETY_SAFE_REMARK { get; set; }
        public string SAFETY_IMAGE_UPLOAD { get; set; }
        public string SAFETY_ACTIVITY_DATE { get; set; }
        public string SAFETY_PUNCH_ID { get; set; }
        public string SAFETY_PUNCH_NAME { get; set; }
        public string MACHINE_USER { get; set; }
        public string MACHINE_NAME { get; set; }
        public string MACHINE_ADD { get; set; }
        public string PUNCH_MODE { get; set; }
        public string PUNCH_FLAG { get; set; }
        public string GIS_LAT { get; set; }
        public string GIS_LONG { get; set; }
        public string GIS_STATUS { get; set; }
        public string STATUS_FLAG { get; set; }
        public string EXEC_METER_NOT_SHIFTED_REASON { get; set; }

        public string METER_NF_STEAM_IMAGE { get; set; }
        public string METER_NF_STEAM_REMARK { get; set; }
    }
}