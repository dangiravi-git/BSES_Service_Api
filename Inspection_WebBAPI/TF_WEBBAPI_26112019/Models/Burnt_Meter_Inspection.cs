using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class Burnt_Meter_Inspection
    {
        public string ID { get; set; }
        public string METERNO { get; set; }
        public string RE_METERNO { get; set; }
        public string CA_NO { get; set; }
        public string RE_CA_NO { get; set; }
        public string SANCTION_LOAD { get; set; }
        public string MDI { get; set; }
        public string CONNECTION_TYPE { get; set; }
        public string USEOFSUPPLY { get; set; }
        public string METER_LOCATION { get; set; }
        public string METER_INSTALLATION { get; set; }
        public string SUPPLY_CONDITION { get; set; }
        public string CONDITIONOF_BURNTMETER { get; set; }
        public string BUSBAR_STATUS { get; set; }
        public string CABLE_CONDITION { get; set; }
        public string NEWCABLE_REQUIRED { get; set; }
        public string REQUIREDCABLE_SIZE { get; set; }
        public string REQUIREDCABLE_LENGTH { get; set; }
        public string MCB_STATUS { get; set; }
        public string ELCB_STATUS { get; set; }
        public string SITE_OBSERVATION { get; set; }

        public string USESUPPLYREMARK { get; set; }
        public string METERLOC_REMARK { get; set; }
        public string CON_BURNTMETER_REMARK { get; set; }

        public string CREATEDDATE { get; set; }
        public string CREATEDBY { get; set; }
        public string DIVISION { get; set; }
        public string BURNTMETERPHOTO { get; set; }
        public string BUSBAR_CABLE_CONDITION { get; set; }

        public string MONTH_YEAR { get; set; }
    }
}