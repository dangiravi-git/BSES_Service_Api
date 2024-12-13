using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class Compliance_Master
    {
        public string ID { get; set; }
        public string Safety_Talk {get;set;}
        public string SafetyTalk_Remark {get;set;}
        public string Safety_Talk_Register{get;set;}
        public string SafetyTalkRegister_Remark{get;set;}
        public string Checking_Record_Registers { get; set; }
        public string Checking_Record_Remark { get; set; }
        public string MaterialIssuedBR{get;set;}
        public string MaterialIssuedBRRemark {get;set;}
        public string CableMaterialIssuans {get;set;}
        public string CableMaterialIssuanceRemark {get;set;}
        public string CableDrumIssueConsumpt {get;set;}
        public string CableDrumIssueConsumptRemark {get;set;}
        public string Reusable_OldCableConsumpt {get;set;}
        public string Reusable_OldCableRemark {get;set;}
        public string CheckPreviousDay_MCRs {get;set;}
        public string CheckPreviousDayMCRs_Remark {get;set;}
        public string Inspection_BurntMeters {get;set;}
        public string Inspection_BurntMeters_Remark {get;set;}
        public string Inspection_NewConnections {get;set;}
        public string Inspection_NewConnectionRemark {get;set;}
        public string NoOfVendorTeamsDeputed {get;set;}
        public string NoOfVendorTeamsDeputedRemark {get;set;}
        public string NoOfVehicles_Deputed {get;set;}
        public string NoOfVehicles_DeputedRemark {get;set;}
        public string QCVisitSurveillance {get;set;}
        public string QCVisitSurveillance_Remark {get;set;}
        public string NotificationInISUReason {get;set;}
        public string NotificationInISUReason_Remark {get;set;}
        public string ConsumerRemarksVerification {get;set;}
        public string ConsumerRkVerificationRemark {get;set;}
        public string CreatedDate {get;set;}
        public string CREATEDBY { get; set; }
        public string DIVISION { get; set; }
        public string ANY_OTHER_ISSUEREMARK { get; set; }

        public string MONTH_YEAR { get; set; }
    }
}