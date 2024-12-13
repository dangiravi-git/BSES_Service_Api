using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recovery_Webservice.Models
{
    public class MeterDetails
    {
        public string strKeyParam { get; set; }
        public string _sMeter { get; set; }
        public string _sCA { get; set; }

    }
    public class FollowupDate
    {
        public string strKeyParam { get; set; }
        public string _sCategory { get; set; }
        public string _sAmtBktID { get; set; }
        public string _sAgeBktID { get; set; }
        public string _sAccountClass { get; set; }
        public string _sDishonorFlag { get; set; }
        public string _sFEID { get; set; }


        public string COMPANY { get; set; }
        public string CA_NUMBER { get; set; }
        public string METER_NO { get; set; }
        public string CRN_NUMBER { get; set; }
        public string CYCLE_NO { get; set; }

        public string ACCOUNT_CLASS { get; set; }
        public string DIVISION { get; set; }
        public string NAME { get; set; }
        public string TELEPHONE_NO { get; set; }
        public string HOUSE_NUMBER { get; set; }

        public string STREET1 { get; set; }
        public string STREET2 { get; set; }
        public string STREET3 { get; set; }
        public string POLE_NO { get; set; }
        public string POSTAL_CODE { get; set; }
        public string STREET { get; set; }

        public string SEQUENCE_NUMBER { get; set; }
        public string RATE_CATEGORY { get; set; }
        public string BILL_STATUS { get; set; }
        public string DISHONOR_FLAG { get; set; }
        public string LAST_PAYMENT_DT { get; set; }

        public string DUE_DATE { get; set; }
        public string CONTRACT { get; set; }
        public string MOVE_IN_DATE { get; set; }
        public string CURRENT_DEMAND { get; set; }
        public string LAST_PAYMENT_AMT { get; set; }

        public string SANCTION_LOAD { get; set; }
        public string DEFAULT_AMT { get; set; }
        public string LPSC_AMOUNT { get; set; }
        public string PRINCIPAL_VALUE { get; set; }
        public string CONSUMER_STATUS { get; set; }
        public string SOFT_DISCONNECTION_DT { get; set; }

        public string RUNNING_BAL_AS_ON_DT { get; set; }
        public string CONNECTION_DT { get; set; }
        public string INITIAL_BUCKET { get; set; }
        public string NEW_OLD_FLAG { get; set; }
        public string UPLOAD_CYCLE { get; set; }

        public string UPDATION_ID { get; set; }
        public string INITIAL_DEFAULT_AMT { get; set; }
        public string PAYMNT_AMT { get; set; }
        public string PAYMNT_ENTRY_DT { get; set; }
        public string PAYMENT_MODE { get; set; }
        public string ASSIGNED_FE_ID { get; set; }
        public string ASSIGNED_FE_NAME { get; set; }

        public string _sDate { get; set; }

        public string DIRECTIVE { get; set; }
    }
    public class Action_Details
    {
        public string strKeyParam { get; set; }
        public string Code { get; set; }

        public string Description { get; set; }
    }
    public class Display_BillPDF
    {
        public string Bill { get; set; }
    }
    public class ValidateCA
    {
        public string CA_Number { get; set; }
    }
    public class DT_Data
    {
        public string timestamp { get; set; }
        public string discom { get; set; }
        public string action { get; set; }
        public string entry { get; set; }
        public string dd { get; set; }
    }
    public class IMEI_Login
    {
        public string imei_no { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
    }
    public class DetailofMIS
    {
        public string strKeyParam { get; set; }
        public string _sFEID { get; set; }
        public string TYPE { get; set; }
        public string CNT { get; set; }
        public string DEFAULT_AMT { get; set; }
    }

    public class FEEDBACK
    {
        public string name { get; set; }
        public string number { get; set; }
        public string ip_addr { get; set; }
        public string imei_no { get; set; }
        public string email { get; set; }
        public string feedback { get; set; }

    }

    public class GetARDAppVersion
    {
        public string _sAppID { get; set; }
    }

    public class GetValidateUser
    {
        public string _sUser { get; set; }
        public string _sPass { get; set; }
    }

    public class InsertChequeDetails
    {
        public string KEY { get; set; }

        public string CANUMBER { get; set; }

        public string CHEQUEDDNO { get; set; }
        public string CHEQUEDDAMOUNT { get; set; }
        public string CHEQUEDDDATE { get; set; }
        public string IMEI { get; set; }
        public string USERID { get; set; }
    }

    public class RecAllocDefltrs
    {
        public string strKeyParam { get; set; }
        public string _sCategory { get; set; }
        public string _sAmtBktID { get; set; }
        public string _sAgeBktID { get; set; }
        public string _sAccountClass { get; set; }
        public string _sDishonorFlag { get; set; }
        public string _sFEID { get; set; }


        public string COMPANY { get; set; }
        public string CA_NUMBER { get; set; }
        public string METER_NO { get; set; }
        public string CRN_NUMBER { get; set; }
        public string CYCLE_NO { get; set; }

        public string ACCOUNT_CLASS { get; set; }
        public string DIVISION { get; set; }
        public string NAME { get; set; }
        public string TELEPHONE_NO { get; set; }
        public string HOUSE_NUMBER { get; set; }

        public string STREET1 { get; set; }
        public string STREET2 { get; set; }
        public string STREET3 { get; set; }
        public string POLE_NO { get; set; }
        public string POSTAL_CODE { get; set; }
        public string STREET { get; set; }

        public string SEQUENCE_NUMBER { get; set; }
        public string RATE_CATEGORY { get; set; }
        public string BILL_STATUS { get; set; }
        public string DISHONOR_FLAG { get; set; }
        public string LAST_PAYMENT_DT { get; set; }

        public string DUE_DATE { get; set; }
        public string CONTRACT { get; set; }
        public string MOVE_IN_DATE { get; set; }
        public string CURRENT_DEMAND { get; set; }
        public string LAST_PAYMENT_AMT { get; set; }

        public string SANCTION_LOAD { get; set; }
        public string DEFAULT_AMT { get; set; }
        public string LPSC_AMOUNT { get; set; }
        public string PRINCIPAL_VALUE { get; set; }
        public string CONSUMER_STATUS { get; set; }
        public string SOFT_DISCONNECTION_DT { get; set; }

        public string RUNNING_BAL_AS_ON_DT { get; set; }
        public string CONNECTION_DT { get; set; }
        public string INITIAL_BUCKET { get; set; }
        public string NEW_OLD_FLAG { get; set; }
        public string UPLOAD_CYCLE { get; set; }

        public string UPDATION_ID { get; set; }
        public string INITIAL_DEFAULT_AMT { get; set; }
        public string PAYMNT_AMT { get; set; }
        public string PAYMNT_ENTRY_DT { get; set; }
        public string PAYMENT_MODE { get; set; }
        public string ASSIGNED_FE_ID { get; set; }
        public string ASSIGNED_FE_NAME { get; set; }

        public string DIRECTIVE { get; set; }

    }

    public class PunchAtr
    {
        public string strKeyParam { get; set; }
        public string _sCANumber { get; set; }
        public string _sStatus { get; set; }
        public string _sFEID { get; set; }
        public string _sTabUpdStatus { get; set; }
        public string _sRemarks { get; set; }
        public string _sImgFlg { get; set; }
        public string _sImgPath { get; set; }
        public string _sFollowDate { get; set; }

        public string _sAltContNo { get; set; }
        public string _sAltEmailID { get; set; }
        public string _sMobileNo { get; set; }
        public string _sMessage { get; set; }
        public string _sUpdationID { get; set; }
        public string _sImgPath1 { get; set; }
        public string Amount { get; set; }

        public string pdcDate { get; set; }
        public string RunningAmt { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }

    public class searchDefltrs
    {
        public string strKeyParam { get; set; }
        public string _sCANumber { get; set; }
        public string _sMeterNumber { get; set; }
        public string _sFEID { get; set; }

        public string COMPANY { get; set; }
        public string CA_NUMBER { get; set; }
        public string METER_NO { get; set; }
        public string CRN_NUMBER { get; set; }
        public string CYCLE_NO { get; set; }

        public string ACCOUNT_CLASS { get; set; }
        public string DIVISION { get; set; }
        public string NAME { get; set; }
        public string TELEPHONE_NO { get; set; }
        public string HOUSE_NUMBER { get; set; }

        public string STREET1 { get; set; }
        public string STREET2 { get; set; }
        public string STREET3 { get; set; }
        public string POLE_NO { get; set; }
        public string POSTAL_CODE { get; set; }
        public string STREET { get; set; }

        public string SEQUENCE_NUMBER { get; set; }
        public string RATE_CATEGORY { get; set; }
        public string BILL_STATUS { get; set; }
        public string DISHONOR_FLAG { get; set; }
        public string LAST_PAYMENT_DT { get; set; }

        public string DUE_DATE { get; set; }
        public string CONTRACT { get; set; }
        public string MOVE_IN_DATE { get; set; }
        public string CURRENT_DEMAND { get; set; }
        public string LAST_PAYMENT_AMT { get; set; }

        public string SANCTION_LOAD { get; set; }
        public string DEFAULT_AMT { get; set; }
        public string LPSC_AMOUNT { get; set; }
        public string PRINCIPAL_VALUE { get; set; }
        public string CONSUMER_STATUS { get; set; }
        public string SOFT_DISCONNECTION_DT { get; set; }

        public string RUNNING_BAL_AS_ON_DT { get; set; }
        public string CONNECTION_DT { get; set; }
        public string INITIAL_BUCKET { get; set; }
        public string NEW_OLD_FLAG { get; set; }
        public string UPLOAD_CYCLE { get; set; }

        public string UPDATION_ID { get; set; }
        public string INITIAL_DEFAULT_AMT { get; set; }
        public string PAYMNT_AMT { get; set; }
        public string PAYMNT_ENTRY_DT { get; set; }
        public string PAYMENT_MODE { get; set; }
        public string ASSIGNED_FE_ID { get; set; }
        public string ASSIGNED_FE_NAME { get; set; }
        public string DIRECTIVE { get; set; }

    }

    public class RecServices_ATR_Status
    {
        public string strKeyParam { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
    }

    public class changepassword
    {
        public string strKeyParam { get; set; }
        public string _sLogin { get; set; }
        public string _sOldPassword { get; set; }
        public string _sNewPassword { get; set; }
    }

    public class loginFE
    {
        public string strKeyParam { get; set; }
        public string _sLogin { get; set; }
        public string _sPassword { get; set; }
        public string count { get; set; }
        public string name { get; set; }
        public string divcode { get; set; }
        public string div_rights { get; set; }
        public string mob_no { get; set; }
        public string fe_address { get; set; }
        public string JwtToken { get; set; }
    }


    public class RecServices_Account_Class
    {
        public string strKeyParam { get; set; }
        public string ACLASSID { get; set; }

        public string ACOUNT_CLASS { get; set; }
    }

    public class RecServices_Aging_Bucket
    {
        public string strKeyParam { get; set; }
        public string AGIBKTID { get; set; }

        public string AGING_BUCKET { get; set; }

        public string RANGE_FROM { get; set; }

        public string RANGE_TO { get; set; }
    }

    public class RecServices_Amount_Bucket
    {
        public string strKeyParam { get; set; }
        public string AMTBKTID { get; set; }

        public string AMOUNT_BUCKET { get; set; }

        public string RANGE_FROM { get; set; }

        public string RANGE_TO { get; set; }
    }

    public class RecServices_Category
    {
        public string strKeyParam { get; set; }

        public string CATID { get; set; }

        public string CATEGORY { get; set; }
    }

    public class getPayment
    {
        public string strKeyParam { get; set; }

        public string _sCANumber { get; set; }

        public string _sMeterNumber { get; set; }

        public string PAYMNT_AMT { get; set; }
        public string PAYMNT_ENTRY_DT { get; set; }
    }


    public class Z_BAPI_IVRS
    {
        public string strKeyParam { get; set; }
        public string strContractAccountNumber { get; set; }
    }
    public class response
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }

    }
    public class SolarBses
    {
        public string RowID { get; set; }
        public string Consumer_Type { get; set; }
        public string Name_discom { get; set; }

        public string Consumer_number { get; set; }
        public string name_consumer { get; set; }

        public string address_consumer { get; set; }
        public string dist_consumer { get; set; }

        public string pin_code { get; set; }
        public string consumer_mobile { get; set; }

        public string consumer_email { get; set; }
        public string Sanction_load { get; set; }
        public string roof_area { get; set; }

        public string Size_plant { get; set; }
        public string Electicity_bill { get; set; }
        public string dt { get; set; }
    }
    public class response_new
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }
    }
}