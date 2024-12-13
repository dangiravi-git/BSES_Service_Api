using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class Safety_Execution_Data
    {
        public string ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string DIVISION { get; set; }
        public string METER_NO { get; set; }
        public string ORDERID { get; set; }
        public string CA_NO { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string ACCOUNT_CLASS { get; set; }
        public string EXEC_SAFE_LOC { get; set; }
        public string EXEC_SAFE_REMARK { get; set; }
        public string EXEC_METER_SHIFTED { get; set; }
        public string EXEC_IMAGE_UPLOAD { get; set; }
        public string EXEC_METER_SHIFT_REMARK { get; set; }
        public string EXEC_METER_NOT_SHIFTED_REASON { get; set; }
        public string EXEC_METER_NOT_SHIFTED_REMARK { get; set; }
        public string EXEC_ACTIVITY_DATE { get; set; }
        public string EXEC_PUNCH_ID { get; set; }
        public string EXEC_PUNCH_NAME { get; set; }
        public string MACHINE_ADD { get; set; }
        public string PUNCH_MODE { get; set; }
        public string PUNCH_FLAG { get; set; }
        public string GIS_LAT { get; set; }
        public string GIS_LONG { get; set; }
        public string GIS_STATUS { get; set; }
        public string STATUS_FLAG { get; set; }

        public string SAFE_PLACE_METER_IMAGE { get; set; }
        public string SAFE_PLACE_METER_REMARK { get; set; }
        public string METER_SHIFTING_POSSIBLE { get; set; }
        public string CUSTOMER_AGREE { get; set; }
        public string FABRICATION_STATUS { get; set; }
        public string FABRICATION_IMAGE { get; set; }
        public string FABRICATION_REMARK { get; set; }
        public string REALLOCATION_SAFE_PLACE_IMAGE { get; set; }
        public string REALLOCATION_SAFE_PLACE_REMARK { get; set; }
        public string METER_SHIFTED_PHYSICALLY { get; set; }
    }
}