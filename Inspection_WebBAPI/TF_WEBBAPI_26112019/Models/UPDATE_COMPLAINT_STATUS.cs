using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class UPDATE_COMPLAINT_STATUS
    {
        public string ASSIGN_TO { get; set; }
        public string STATUS { get; set; }
        public string HELPDESK_ID { get; set; }
        public string REMARK { get; set; }
        public string REASON { get; set; }
        public string TYPE { get; set; }
        public string SUBTYPE { get; set; }
        public string SUBTYPE_DESC { get; set; }
        public string PROBLEMAREA { get; set; }
        public string SUPPORTTYPE { get; set; }
        public string EMP_CODE { get; set; }
        public string EMPNAME { get; set; }
        public string ASSIGN_NAME { get; set; }
        public string PROBAREA_TEXT { get; set; }
        public string PROBTYPE_TEXT { get; set; }
        public string PROBSUBTYPE_TEXT { get; set; }
        public string PROBCAT_TEXT { get; set; }
        public string USER_IDTEXT { get; set; }
    }
}