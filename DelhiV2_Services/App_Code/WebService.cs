using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Text;
using aejw.Network;
using System.IO;
using System.Globalization;
using System.Configuration;
using SAP.Middleware.Connector;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;
using Paytm;
using System.Web.Script.Serialization;
using System.Net.Mail;
using Newtonsoft.Json;
using System.Xml.Serialization;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    clsBapiCall obj = new clsBapiCall();
    public WebService()
    {
    }
    //   [WebMethod]
    //   public DataSet ZBAPIDOCLIST(string strAufnr, string strC_001, string strC_002, string strC_003, string strC_004, string strC_005, string strC_007, string strC_008, string strC_009, string strC_010,
    //       string strC_011, string strC_012, string strC_013, string strC_014, string strC_015, string strC_016, string strC_017, string strC_018, string strC_019, string strC_020, string strC_021, string strC_022,
    //       string strC_023, string strC_024, string strC_025, string strC_026, string strC_027, string strC_028, string strC_029, string strC_030, string strC_031, string strC_032, string strC_033, string strC_034,
    //       string strC_035, string strC_036, string strC_037, string strC_038, string strC_039, string strC_040, string strC_041, string strC_070, string strR_Cdll, string strR_Occ, string strR_Own, string strZ_Appltype)
    //   {
    //       DataSet Ds = obj.Get_ZBAPIDOCLIST(strAufnr, strC_001.ToUpper(), strC_002.ToUpper(), strC_003.ToUpper(), strC_004.ToUpper(), strC_005.ToUpper(), strC_007.ToUpper(), strC_008.ToUpper(), strC_009.ToUpper(), strC_010.ToUpper(),
    //        strC_011.ToUpper(), strC_012.ToUpper(), strC_013.ToUpper(), strC_014.ToUpper(), strC_015.ToUpper(), strC_016.ToUpper(), strC_017.ToUpper(), strC_018.ToUpper(), strC_019.ToUpper(), strC_020.ToUpper(), strC_021.ToUpper(), strC_022.ToUpper(),
    //        strC_023.ToUpper(), strC_024.ToUpper(), strC_025.ToUpper(), strC_026.ToUpper(), strC_027.ToUpper(), strC_028.ToUpper(), strC_029.ToUpper(), strC_030.ToUpper(), strC_031.ToUpper(), strC_032.ToUpper(), strC_033.ToUpper(), strC_034.ToUpper(),
    //        strC_035.ToUpper(), strC_036.ToUpper(), strC_037.ToUpper(), strC_038.ToUpper(), strC_039.ToUpper(), strC_040.ToUpper(), strC_041.ToUpper(), strC_070.ToUpper(), strR_Cdll.ToUpper(), strR_Occ.ToUpper(), strR_Own.ToUpper(), strZ_Appltype.ToUpper());
    //       return Ds;
    //   }

    //   [WebMethod]
    //   public DataSet ZBAPI_DISPLAY_BILL_WEB(string strCANumber, string strBillMonth)
    //   {
    //      DataSet Ds = obj.Get_ZBAPI_DISPLAY_BILL_WEB(strCANumber, strBillMonth);
    //      return Ds;
    //   }

    //   [WebMethod]
    //   public DataSet ZBAPI_IVR_CREATESO_ISU(string strCANumber, string strCACrn, string strCAKNumber, string strMeterNumber, string strISUOrder, 
    //                                                           string strComplaintType, string strContractNumber, string strTelephoneNo, string strDescription)
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_IVR_CREATESO_ISU(strCANumber, strCACrn, strCAKNumber, strMeterNumber, strISUOrder, strComplaintType, strContractNumber, strTelephoneNo, strDescription);
    //       return Ds;
    //   }

    //[WebMethod]
    //public DataSet Z_BAPI_CMS_ISU_CA_DISPLAY(string strCANumber, string strSerialNumber, string strConsumerNumber, string strTelephoneNumber, string strKNumber, string strContractNumber)
    //{
    //    DataSet Ds = obj.Get_Z_BAPI_CMS_ISU_CA_DISPLAY(strCANumber, strSerialNumber, strConsumerNumber, strTelephoneNumber, strKNumber, strContractNumber);
    //    return Ds;
    //}

    [WebMethod]
    public DataSet ZBAPI_CREATESO_POST(string strPMAufart, string strPlanPlant, string strRegioGroup, string strShortText, string strILA, string strMFText, string strUserFieldCH20, string StrControkey, string strSerialNumber, string strComplaintGroup,
                                                                                            string strCANumber, string strContract, string strMFText1)
    {
        DataSet Ds = obj.Get_ZBAPI_CREATESO_POST(strPMAufart, strPlanPlant, strRegioGroup, strShortText, strILA, strMFText, strUserFieldCH20, StrControkey,
                                                                strSerialNumber, strComplaintGroup, strCANumber, strContract, strMFText1);
        return Ds;
    }

    //   [WebMethod]
    //   public DataSet ZBAPI_CALERT(string strCompanyCode, string strDate, string strDivision, string strUnit)
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_CALERT(strCompanyCode, strDate, strDivision, strUnit);
    //       return Ds;
    //   }

    ////   [WebMethod]
    //   public DataSet ZBAPI_ONLINE_BILL_PDF(string strCANumber, string strEBSKNO) // Testing required
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_ONLINE_BILL_PDF(strCANumber,strEBSKNO);
    //       return Ds;
    //   }

    // //  [WebMethod]
    //   public DataSet ZBAPI_DSS_SO(string PARTNERCATEGORY, string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, 
    //                               string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, 
    //                               string CITY, string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string LEGITTYPE, 
    //                               string IDNUMBER, string ORDER_TYPE, string SHORTTEXT, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, 
    //                               string PMACTIVITYTYPE, string REQUESTNUM, string NNUMBER, string APPLIEDCAT, string APPLIEDLOAD, string CONNECTIONTYPE, 
    //                               string ORDERID, string FLAG)
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_DSS_SO( PARTNERCATEGORY,  PARTNERTYPE,  TITLE_KEY,  FIRSTNAME,  LASTNAME,  MIDDLENAME, 
    //                                FATHERSNAME,  HOUSE_NO,  BUILDING,  STR_SUPPL1,  STR_SUPPL2,  STR_SUPPL3,  POSTL_COD1, 
    //                                CITY,  E_MAIL,  LANDLINE,  MOBILE,  FEMALE,  MALE,  JOBGR,  LEGITTYPE, 
    //                                IDNUMBER,  ORDER_TYPE,  SHORTTEXT,  PLANNINGPLANT,  WORKCENTRE,  SYSTEMCOND, 
    //                                PMACTIVITYTYPE,  REQUESTNUM,  NNUMBER,  APPLIEDCAT,  APPLIEDLOAD,  CONNECTIONTYPE, 
    //                                ORDERID,  FLAG);
    //       return Ds;
    //   }



    ////   [WebMethod]
    //   public DataSet ZBAPI_DSS_SO_ECC(string PARTNERCATEGORY, string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, 
    //                               string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, 
    //                               string CITY, string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string IDTYPE, 
    //                               string IDNUMBER, string ORDER_TYPE, string SHORTTEXT, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, 
    //                               string PMACTIVITYTYPE, string REQUESTNUM, string NNUMBER, string APPLIEDCAT, string APPLIEDLOAD, string CONNECTIONTYPE, 
    //                               string ORDERID, string FLAG)
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_DSS_SO_ECC(PARTNERCATEGORY, PARTNERTYPE, TITLE_KEY, FIRSTNAME, LASTNAME, MIDDLENAME,
    //                                FATHERSNAME, HOUSE_NO, BUILDING, STR_SUPPL1, STR_SUPPL2, STR_SUPPL3, POSTL_COD1,
    //                                CITY, E_MAIL, LANDLINE, MOBILE, FEMALE, MALE, JOBGR, IDTYPE,
    //                                IDNUMBER, ORDER_TYPE, SHORTTEXT, PLANNINGPLANT, WORKCENTRE, SYSTEMCOND,
    //                                PMACTIVITYTYPE, REQUESTNUM, NNUMBER, APPLIEDCAT, APPLIEDLOAD, CONNECTIONTYPE,
    //                                ORDERID, FLAG);
    //       return Ds;
    //   }


    //   [WebMethod]
    //   public DataSet Z_BAPI_ZDSS_WEB_LINK(string I_ILART, string I_VKONT, string I_VKONA, string PARTNERCATEGORY, string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, 
    //                                   string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2, 
    //                                   string STR_SUPPL3, string POSTL_COD1, string CITY, string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, 
    //                                   string JOBGR, string IDTYPE, string IDNUMBER, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string APPLIEDCAT, 
    //                                   string APPLIEDLOAD, string APPLIEDLOADKVA, string CONNECTIONTYPE, string STATEMENT_CA, string START_DATE, string START_TIME, 
    //                                   string FINISH_DATE, string FINISH_TIME, string SORTFIELD, string ABKRS)
    //   {
    //       DataSet Ds = obj.Get_Z_BAPI_ZDSS_WEB_LINK(I_ILART,  I_VKONT,  I_VKONA,  PARTNERCATEGORY,  PARTNERTYPE,  TITLE_KEY,  FIRSTNAME, 
    //                                    LASTNAME,  MIDDLENAME,  FATHERSNAME,  HOUSE_NO,  BUILDING,  STR_SUPPL1,  STR_SUPPL2, 
    //                                    STR_SUPPL3,  POSTL_COD1,  CITY,  E_MAIL,  LANDLINE,  MOBILE,  FEMALE,  MALE, 
    //                                    JOBGR,  IDTYPE,  IDNUMBER,  PLANNINGPLANT,  WORKCENTRE,  SYSTEMCOND,  APPLIEDCAT, 
    //                                    APPLIEDLOAD,  APPLIEDLOADKVA,  CONNECTIONTYPE,  STATEMENT_CA,  START_DATE,  START_TIME, 
    //                                    FINISH_DATE,  FINISH_TIME,  SORTFIELD,  ABKRS);
    //       return Ds;
    //   }

    //   [WebMethod]
    //   public DataSet ZBAPI_ENFORCEMENT(string strCANumber, string strContract)
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_ENFORCEMENT(strCANumber, strContract);
    //       return Ds;
    //   }

    [WebMethod]
    public DataSet ZBI_WEBBILL_HIST(string strCANumber, string strBillMonth)
    {
        DataSet Ds = obj.Get_ZBI_WEBBILL_HIST(strCANumber, strBillMonth);
        return Ds;
    }

    [WebMethod]
    public DataSet Z_BAPI_IVRS(string strContractAccountNumber)
    {
        DataSet dsBAPIOutput = obj.Get_Z_BAPI_IVRS(strContractAccountNumber);
        return dsBAPIOutput;
    }

    #region DivyeshJain

    [WebMethod]
    public DataSet SEND_BSES_SMSAPI(string _sAppName, string _sEncryptionKey, string _sCompanyCode, string _sVendorCode, string _sEmpCode,
                                     string _MobileNo, string _sOTPMsg, string _sSMSType)
    {
        string apiUrl = string.Empty; //!!B$E$@@*SMS
        string strResFlg = string.Empty;
        string strRes = string.Empty;
        string ResponseText = string.Empty;
        string Number = _MobileNo.Length == 10 ? "91" + _MobileNo.Trim() : _MobileNo.Trim();
        string Message = string.Empty;

        if (_sOTPMsg.Length < 7)
            Message = _sOTPMsg + " is your one time password (OTP) for Need Validation. Please enter the OTP to proceed. Team BRPL";
        else
            Message = _sOTPMsg;

        DataTable _dtSMSValidity = CheckSMS_Validity(_sEncryptionKey, _sCompanyCode);

        //DataTable _dtSMSValidity = CheckSMS_Validity(_sEncryptionKey, _sCompanyCode, _sSMSType);

        //if (_sCompanyCode.Trim().ToUpper().Contains("BRPL") == false)
        //{
        //    strRes = "Invalid Company Code";
        //}
        //else
        if (Number.Length != 12)
        {
            strRes = "Invalid Mobile Number";
            strResFlg = "F";
        }
        else if (_dtSMSValidity.Rows.Count == 0)
        {
            strRes = "Key Mismatch";
            strResFlg = "F";
        }
        else
        {
            apiUrl = _dtSMSValidity.Rows[0][0].ToString();
            if (_sCompanyCode.Trim() == "BRPL")
            {
                // apiUrl = "https://japi.instaalerts.zone/failsafe/HttpLink?aid=508443&pin=bses@56&mnumber=" + Number + "&signature=BSESRP&message=" + Message;
                apiUrl += Number + "&signature= BSESRP&message=" + Message;
                string MyProxyHostString = "10.125.64.134";
                int MyProxyPort = 3128;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(apiUrl);
                req.Proxy = new WebProxy(MyProxyHostString, MyProxyPort);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                string results = sr.ReadToEnd();

                while (resp.StatusCode.ToString().Equals("OK") != true) { }
                if (resp.StatusCode.ToString().Equals("OK"))
                {
                    ResponseText = ParseHttpWebResponse(resp);
                    strRes = "SUCCESS";
                    strResFlg = "S";
                }
                else
                {
                    ResponseText = "N";
                    strRes = "FAIL(SMS API is not working)";
                    strResFlg = "F";
                }
            }
            else
            {
                apiUrl = "https://japi.instaalerts.zone/failsafe/HttpLink?aid=508443&pin=bses@56&mnumber=" + Number + "&signature=BSESYP&message=" + Message;
                string MyProxyHostString = "10.125.64.134";
                int MyProxyPort = 3128;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(apiUrl);
                req.Proxy = new WebProxy(MyProxyHostString, MyProxyPort);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                string results = sr.ReadToEnd();

                while (resp.StatusCode.ToString().Equals("OK") != true) { }
                if (resp.StatusCode.ToString().Equals("OK"))
                {
                    ResponseText = ParseHttpWebResponse(resp);
                    strRes = "SUCCESS";
                    strResFlg = "S";
                }
                else
                {
                    ResponseText = "N";
                    strRes = "FAIL (SMS API is not Working)";
                    strResFlg = "F";
                }
            }

            if (_sCompanyCode.Trim() == "BRPL")
            {
                if (ResponseText != "N")
                {
                    string[] sSms = ResponseText.Split('=');
                    string responseId = string.Empty, responseCode = string.Empty, responseStatus = string.Empty, responsedatetime = string.Empty;

                    if (sSms.Length > 3)
                    {
                        responseId = sSms[1].ToString().Replace("~code", "").Trim();
                        responseCode = sSms[2].ToString().Replace(" & info", "").Trim();
                        responseStatus = sSms[3].ToString().Replace(" & Time ", "").Trim();
                        responsedatetime = sSms[4].ToString();
                    }
                    else
                    {
                        responseId = "API000";
                        responseCode = "API000";
                        responseStatus = "Air2web accepted";
                        responsedatetime = System.DateTime.Now.ToString();
                    }

                    if (sSms.Length > 0)
                    {
                        Insert_MessageLog(Message, Number.ToString(), _sSMSType, _sCompanyCode, "Y",
                        responseId, responseCode, responseStatus, System.DateTime.Now.ToString(), _sVendorCode.ToString(), _sAppName.ToString(), _sEmpCode.ToString());
                    }
                    else
                    {
                        Insert_MessageLog(Message, Number.ToString(), _sSMSType, _sCompanyCode, "X", "", "", "", "",
                                                _sVendorCode.ToString(), _sAppName.ToString(), _sEmpCode.ToString());
                    }
                }
                else
                {
                    Insert_MessageLog(Message, Number.ToString(), _sSMSType, _sCompanyCode, "X", "", "", "", "",
                                                _sVendorCode.ToString(), _sAppName.ToString(), _sEmpCode.ToString());
                }
            }
        }

        DataSet ds1 = new DataSet();
        DataTable dtOutPut = new DataTable();
        DataColumn column = new DataColumn();
        dtOutPut.Columns.Add("FLAG", typeof(string));
        dtOutPut.Columns.Add("OUT_PUT", typeof(string));
        dtOutPut.Rows.Add(strResFlg, strRes);
        ds1.Tables.Add(dtOutPut);
        dtOutPut.AcceptChanges();
        return ds1;
    }

    private DataTable CheckSMS_Validity(string _sKey, string _sCompany)
    {
        string _sSql = string.Empty;
        DataTable _dt = new DataTable();

        _sSql = " SELECT API_URL FROM SMS_API_MST where ENCRYPTION_KEY='" + _sKey + "' and ACTIVE_STATUS='Y' ";
        _sSql += " and trunc(sysdate)<trunc(VALIDITY_END_DATE) and COMPANY_CODE='" + _sCompany + "' ";
        return dmlgetqueryCon(_sSql);
    }

    #endregion


    //   [WebMethod]
    //   public DataSet Z_BAPI_DSS_ISU_CA_DISPLAY(string strCANumber, string strCRNNumber)
    //   {
    //       DataSet dsBAPIOutput = obj.Get_Z_BAPI_DSS_ISU_CA_DISPLAY(strCANumber, strCRNNumber);

    //       return dsBAPIOutput;
    //   }

    //   [WebMethod]
    //   public DataSet ZBAPI_FICA_PREPAID_MTR(string strDOC_ID, string strTRANS_ID, string strCA, string strCOMPANY,
    //           string strCONSUMER_TYPE, string strMETER_MANFR, string strCONTRACT, string strCA_VALID_ISU, string strENTRY_DATE,
    //           string strS_ENC_TKN_1, string strS_ENC_TKN_2, string strS_ENC_TKN_3, string strS_ENC_TKN_4, string strGENUS_RESP_CODE,
    //           string strMETER_NO, string strACC_CLASS, string strAMNT_BANK, string strAMNT_ISU, string strADDRESS, string strTARIFTYP,
    //           string strTARIFID, string strPAY_METHOD)
    //   {
    //       DataSet dsBAPIOutput = obj.get_ZBAPI_FICA_PREPAID_MTR(strDOC_ID, strTRANS_ID, strCA, strCOMPANY,
    //           strCONSUMER_TYPE, strMETER_MANFR, strCONTRACT, strCA_VALID_ISU, strENTRY_DATE,
    //           strS_ENC_TKN_1, strS_ENC_TKN_2, strS_ENC_TKN_3, strS_ENC_TKN_4, strGENUS_RESP_CODE,
    //            strMETER_NO, strACC_CLASS, strAMNT_BANK, strAMNT_ISU, strADDRESS, strTARIFTYP,
    //            strTARIFID,  strPAY_METHOD);

    //       return dsBAPIOutput;
    //   }


    //[WebMethod]
    //public DataSet ZBAPI_CA_OUTSTANDING_AMT(string strCANumber)
    //{
    //    DataSet dsBAPIOutput = obj.get_ZBAPI_CA_OUTSTANDING_AMT(strCANumber);

    //    return dsBAPIOutput;
    //}

    [WebMethod]
    public DataSet Z_BAPI_DISPLAY_BIIL_OPOWER_ORC(string strCANumber, string strBillMonth)
    {
        DataSet BAPI_RESULT = new DataSet();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DataSet Ds = obj.Get_Z_BAPI_CMS_ISU_CA_DISPLAY(strCANumber, strSerialNumber, strConsumerNumber, strTelephoneNumber, strKNumber, strContractNumber);
            BAPI_RESULT = GetCA_OpowerData(strCANumber);
        }
        return BAPI_RESULT;
    }

    public bool checkConsumer(string _sFunName)
    {
        // In this method you can check the username and password 
        // with your database or something
        // You could also encrypt the password for more security

        //if (consumer != null)
        //{
        //    if (NewClassFile.getWebSericeAccess(consumer.userName, consumer.password, _sFunName))
        return true;
        //    else
        //        return false;
        //}
        //else
        //    return false;
    }

    public DataSet GetCA_OpowerData(string _sCaNumber)
    {
        DataSet ds = new DataSet();
        DataTable _dt = new DataTable();
        string _sSql = string.Empty;

        _sSql = " select CONTRACT_ACCOUNT  Ca_Number, BUSINESS_PARTNER Bp_Number, FIRST_NAME ||' '|| LAST_NAME Bp_Name,'0' Bp_Type, '0' Search_Term1, ";
        _sSql += " ACCOUNT_CLASS Search_Term2,'0' House_Number, '0'House_Number_Sup,'0'Floor,CONSUMER_STATUS Premise_Type, ";
        _sSql += " ADDRESS Street , '0'Street2,'0'Street3,'0'Street4, 'NEW DELHI' City,'0' Post_Code, 'DEL' Region,'IN' Country,'0' Desc_Con_Object, ";
        _sSql += " DIVISION Reg_Str_Group, '0' Device_Sr_Number, TEL_NUMBER Telephone_No,'0'Mru, '0' Func_Descr, ";
        _sSql += " '000000' Outage_Fromtime, '000000'Outage_Totime, '0' Legacy_Acct,'0' Bill_Class, RATE_CATEGORY Rate_Cat,'0' Activity,'0' Adr_Notes, MOBILE_NO Tel1_Number, ";
        _sSql += " '0' Vertrag, EMAIL E_Mail, '0' Move_Out_Date, '0' Con_Obj_No,'0' Clerk_Id,'0' Text,'00' Status, '0' Discreason, '0'POLE_NO ";
        _sSql += " from pcm.CONSUMER_SAP_MASTER where CONTRACT_ACCOUNT='" + _sCaNumber + "' ";

        _dt = dmlgetquery(_sSql);

        if (_dt.Rows.Count > 0)
        {

            DataTable ISUSTDTable = new DataTable();
            DataColumn column = new DataColumn();

            ISUSTDTable.Columns.Add("CONSUMER_NO", typeof(string));
            ISUSTDTable.Columns.Add("strBillMonth", typeof(string));
            ISUSTDTable.Columns.Add("FIRSTNAME", typeof(string));
            ISUSTDTable.Columns.Add("AMNT_AFT_DUE_DAT", typeof(string));
            ISUSTDTable.Columns.Add("BILL_MONTH", typeof(string));
            ISUSTDTable.Columns.Add("DUE_DATE", typeof(string));
            ISUSTDTable.Columns.Add("LASTNAME", typeof(string));
            ISUSTDTable.Columns.Add("SANC_LOAD", typeof(string));
            ISUSTDTable.Columns.Add("RATE_CATEGORY", typeof(string));
            ISUSTDTable.Columns.Add("BILL_BASIS", typeof(string));
            ISUSTDTable.Columns.Add("HOUSE_NO", typeof(string));
            ISUSTDTable.Columns.Add("FATHER_NAME", typeof(string));
            ISUSTDTable.Columns.Add("STR_SUPPL2", typeof(string));
            ISUSTDTable.Columns.Add("CITY", typeof(string));
            ISUSTDTable.Columns.Add("POSTL_COD1", typeof(string));
            ISUSTDTable.Columns.Add("TOT_UNITS_BILLED", typeof(string));
            ISUSTDTable.Columns.Add("MDI", typeof(string));
            ISUSTDTable.Columns.Add("INVOICE_NO", typeof(string));
            ISUSTDTable.Columns.Add("DATE_OF_INVOICE", typeof(string));
            ISUSTDTable.Columns.Add("CYCLE_NO", typeof(string));
            ISUSTDTable.Columns.Add("COMPANY_CODE", typeof(string));
            ISUSTDTable.Columns.Add("CUR_MTH_BILL_AMT", typeof(string));
            ISUSTDTable.Columns.Add("NET_AMNT", typeof(string));
            ISUSTDTable.Columns.Add("UNIT_DESCR", typeof(string));
            ISUSTDTable.Columns.Add("MIDDLE_NAME", typeof(string));
            ISUSTDTable.Columns.Add("OPOWER", typeof(string));

            DataRow dr;
            dr = ISUSTDTable.NewRow();

            dr["CONSUMER_NO"] = _dt.Rows[0]["Ca_Number"].ToString();
            dr["strBillMonth"] = "0";
            dr["FIRSTNAME"] = _dt.Rows[0]["Bp_Name"].ToString();
            dr["AMNT_AFT_DUE_DAT"] = "0";
            dr["BILL_MONTH"] = "0";
            dr["DUE_DATE"] = "0";
            dr["LASTNAME"] = "0";
            dr["SANC_LOAD"] = "0";
            dr["RATE_CATEGORY"] = _dt.Rows[0]["Rate_Cat"].ToString();
            dr["BILL_BASIS"] = "0";
            dr["HOUSE_NO"] = "0";
            dr["FATHER_NAME"] = "0";
            dr["STR_SUPPL2"] = "0";
            dr["CITY"] = "0";
            dr["POSTL_COD1"] = "0";
            dr["TOT_UNITS_BILLED"] = "0";
            dr["MDI"] = "0";
            dr["INVOICE_NO"] = "0";
            dr["DATE_OF_INVOICE"] = "0";
            dr["CYCLE_NO"] = "0";
            dr["COMPANY_CODE"] = "BRPL";
            dr["CUR_MTH_BILL_AMT"] = "";
            dr["NET_AMNT"] = "0";
            dr["UNIT_DESCR"] = "0";
            dr["MIDDLE_NAME"] = "0";
            dr["OPOWER"] = "0";

            ISUSTDTable.Rows.Add(dr);
            ISUSTDTable.AcceptChanges();

            ds.Tables.Add(ISUSTDTable);
            ds.Tables[0].TableName = "ISUSTDTable";
            ds.DataSetName = "BAPI_RESULT";

        }

        return ds;
    }

    public DataSet GetCAWise_DisplayData(string _sCaNumber)
    {
        DataSet ds = new DataSet();
        DataTable _dt = new DataTable();
        string _sSql = string.Empty;

        DataTable ISUSTDTable = new DataTable();
        DataColumn column = new DataColumn();

        ISUSTDTable.Columns.Add("Ca_Number", typeof(string));
        ISUSTDTable.Columns.Add("Bp_Number", typeof(string));
        ISUSTDTable.Columns.Add("Bp_Name", typeof(string));
        ISUSTDTable.Columns.Add("Bp_Type", typeof(string));
        ISUSTDTable.Columns.Add("Search_Term1", typeof(string));
        ISUSTDTable.Columns.Add("Search_Term2", typeof(string));
        ISUSTDTable.Columns.Add("House_Number", typeof(string));
        ISUSTDTable.Columns.Add("House_Number_Sup", typeof(string));
        ISUSTDTable.Columns.Add("Floor", typeof(string));
        ISUSTDTable.Columns.Add("Premise_Type", typeof(string));
        ISUSTDTable.Columns.Add("Street", typeof(string));
        ISUSTDTable.Columns.Add("Street2", typeof(string));
        ISUSTDTable.Columns.Add("Street3", typeof(string));
        ISUSTDTable.Columns.Add("Street4", typeof(string));
        ISUSTDTable.Columns.Add("City", typeof(string));
        ISUSTDTable.Columns.Add("Post_Code", typeof(string));
        ISUSTDTable.Columns.Add("Region", typeof(string));
        ISUSTDTable.Columns.Add("Country", typeof(string));
        ISUSTDTable.Columns.Add("Desc_Con_Object", typeof(string));
        ISUSTDTable.Columns.Add("Reg_Str_Group", typeof(string));
        ISUSTDTable.Columns.Add("Device_Sr_Number", typeof(string));
        ISUSTDTable.Columns.Add("Telephone_No", typeof(string));
        ISUSTDTable.Columns.Add("Mru", typeof(string));
        ISUSTDTable.Columns.Add("Func_Descr", typeof(string));
        ISUSTDTable.Columns.Add("Outage_Fromtime", typeof(string));
        ISUSTDTable.Columns.Add("Outage_Totime", typeof(string));
        ISUSTDTable.Columns.Add("Legacy_Acct", typeof(string));
        ISUSTDTable.Columns.Add("Bill_Class", typeof(string));
        ISUSTDTable.Columns.Add("Rate_Cat", typeof(string));
        ISUSTDTable.Columns.Add("Activity", typeof(string));
        ISUSTDTable.Columns.Add("Adr_Notes", typeof(string));
        ISUSTDTable.Columns.Add("Tel1_Number", typeof(string));
        ISUSTDTable.Columns.Add("Vertrag", typeof(string));
        ISUSTDTable.Columns.Add("E_Mail", typeof(string));
        ISUSTDTable.Columns.Add("Move_Out_Date", typeof(string));
        ISUSTDTable.Columns.Add("Con_Obj_No", typeof(string));
        ISUSTDTable.Columns.Add("Clerk_Id", typeof(string));
        ISUSTDTable.Columns.Add("Text", typeof(string));
        ISUSTDTable.Columns.Add("Status", typeof(string));
        ISUSTDTable.Columns.Add("Discreason", typeof(string));
        ISUSTDTable.Columns.Add("POLE_NO", typeof(string));
        ISUSTDTable.Columns.Add("DUE_DATE", typeof(string));
        ISUSTDTable.Columns.Add("NET_AMNT", typeof(string));
        ISUSTDTable.Columns.Add("SANCTIONED_LOAD", typeof(string));
        ISUSTDTable.Columns.Add("COMPANY_CODE", typeof(string));
        ISUSTDTable.Columns.Add("BILL_BASIS", typeof(string));

        _sSql = " select CONTRACT_ACCOUNT  Ca_Number, BUSINESS_PARTNER Bp_Number, FIRST_NAME ||' '|| LAST_NAME Bp_Name,'.' Bp_Type, '.' Search_Term1, ";
        _sSql += " s.ACCOUNT_CLASS Search_Term2,'.' House_Number, '.'House_Number_Sup,'.'Floor,CONSUMER_STATUS Premise_Type, ";
        _sSql += " ADDRESS Street , '.'Street2,'.'Street3,'.'Street4, 'NEW DELHI' City,'.' Post_Code, 'DEL' Region,'IN' Country,'.' Desc_Con_Object,SANCTIONED_LOAD, ";
        _sSql += " s.DIVISION Reg_Str_Group, '.' Device_Sr_Number, TEL_NUMBER Telephone_No,'.'Mru, '.' Func_Descr, ";
        _sSql += " '000000' Outage_Fromtime, '000000'Outage_Totime, '.' Legacy_Acct,'.' Bill_Class, RATE_CATEGORY Rate_Cat,'.' Activity,'.' Adr_Notes, S.MOBILE_NO Tel1_Number, ";
        _sSql += " '.' Vertrag, EMAIL E_Mail, '.' Move_Out_Date, '.' Con_Obj_No,'.' Clerk_Id,'.' Text,'.' Status, '.' Discreason, '.'POLE_NO, 'BRPL' COMPANY_CODE, ";
        //_sSql += " CASE WHEN trunc(SMS_GEN_DATE) <'19-May-2021' THEN '202105'||substr(due_date,1,2)  ELSE '202106'||substr(due_date,1,2) END  DueDate,substr(AMOUNT,4) AMOUNT ";
        _sSql += " to_char(DUE_DATE,'YYYYMMDD')  DueDate, AMOUNT,BILL_BASIS ";
        _sSql += " from pcm.CONSUMER_SAP_MASTER S, MOBAPP.BRPL_BILL_MASTER M where S.CONTRACT_ACCOUNT=M.CA_NUMBER(+)  AND S.CONTRACT_ACCOUNT='" + _sCaNumber + "' ";
        _sSql += " order by m.INVOICE_NUMBER desc  ";

        // _sSql += " order by m.SMS_GEN_DATE desc  ";  

        _dt = dmlgetquery(_sSql);

        if (_dt.Rows.Count == 0)
        {
            _sSql = "  select CONTRACT_ACCOUNT Ca_Number, BUSINESS_PARTNER Bp_Number, FIRST_NAME ||' '|| LAST_NAME Bp_Name,'0' Bp_Type, '0' Search_Term1, ";
            _sSql += " '.' Search_Term2,'.' House_Number, '.'House_Number_Sup,'.'Floor,'.' Premise_Type, ";
            _sSql += " ADDRESS Street , '0'Street2,'0'Street3,'0'Street4, 'DELHI' City,'0' Post_Code, 'DEL' Region,'IN' Country,'0' Desc_Con_Object, LOAD SANCTIONED_LOAD, ";
            _sSql += " DIVISION Reg_Str_Group, '0' Device_Sr_Number, TELEPHONE_NUMBER Telephone_No,'0'Mru, '0' Func_Descr,  ";
            _sSql += " '000000' Outage_Fromtime, '000000'Outage_Totime, '0' Legacy_Acct,S.ACCOUNT_CLASS Bill_Class, RATE_CATEGORY Rate_Cat,'0' Activity,'0' Adr_Notes, TELEPHONE_NUMBER Tel1_Number, ";
            _sSql += " '0' Vertrag, EMAIL E_Mail, '0' Move_Out_Date, '0' Con_Obj_No,'0' Clerk_Id,'0' Text,'00' Status, '0' Discreason, '0'POLE_NO ,COMPANY_CODE,  ";
            _sSql += " to_char(DUE_DATE,'YYYYMMDD') DUEDATE,AMOUNT,BILL_BASIS ";
            _sSql += " from admin.payment_mobile_data  S, ADMIN.BYPL_BILL_MASTER M where S.CONTRACT_ACCOUNT=M.CA_NUMBER  AND S.CONTRACT_ACCOUNT='" + _sCaNumber + "'  ";
            _sSql += "  order by m.SMS_GEN_DATE desc ";

            _dt = dmlgetquery(_sSql);


            //if (_dt.Rows.Count == 0)
            //{

            //    _sSql = " select CONS_REF Ca_Number,'.' Bp_Number, NAME bp_NAME,'.' Bp_Type, '.' Search_Term1,  ";
            //    _sSql += " '' Search_Term2,'.' House_Number, '.'House_Number_Sup,'.'Floor,'.' Premise_Type, ";
            //    _sSql += " '.' Street , '.'Street2,'.'Street3,'.'Street4, 'NEW DELHI' City,'.' Post_Code, 'DEL' Region,'IN' Country,'.' Desc_Con_Object,SANCT_LOAD SANCTIONED_LOAD, ";
            //    _sSql += " DISTRICT Reg_Str_Group, '.' Device_Sr_Number, S.MOBILE_NO Telephone_No,'.'Mru, '.' Func_Descr,";
            //    _sSql += " '000000' Outage_Fromtime, '000000'Outage_Totime, '.' Legacy_Acct,'.' Bill_Class, '.' Rate_Cat,'.' Activity,'.' Adr_Notes, S.MOBILE_NO Tel1_Number, ";
            //    _sSql += " '.' Vertrag, '.' E_Mail, '.' Move_Out_Date, '.' Con_Obj_No,'.' Clerk_Id,'.' Text,'.' Status, '.' Discreason, '.'POLE_NO, SAP_COMPANY COMPANY_CODE, ";
            //    _sSql += " CASE WHEN trunc(SMS_GEN_DATE) <'19-May-2021' THEN '202105'||substr(due_date,1,2)  ELSE '202106'||substr(due_date,1,2) END  DueDate,substr(AMOUNT,4) AMOUNT ";
            //    _sSql += " from rcmpa.sap_formy_vws S, MOBAPP.BRPL_BILL_MASTER M where S.CONS_REF='000'||M.CA_NUMBER(+)  AND S.CONS_REF='" + _sCaNumber + "'  ";
            //    _sSql += " order by m.SMS_GEN_DATE desc  ";

            //    _dt = dmlgetquery(_sSql);
            //}
        }


        if (_dt.Rows.Count > 0)
        {
            DataRow dr;
            dr = ISUSTDTable.NewRow();

            dr["Ca_Number"] = _dt.Rows[0]["Ca_Number"].ToString();
            dr["Bp_Number"] = _dt.Rows[0]["Bp_Number"].ToString();
            dr["Bp_Name"] = _dt.Rows[0]["Bp_Name"].ToString();
            dr["Bp_Type"] = _dt.Rows[0]["Bp_Type"].ToString();
            dr["Search_Term1"] = _dt.Rows[0]["Search_Term1"].ToString();
            dr["Search_Term2"] = _dt.Rows[0]["Search_Term2"].ToString();
            dr["House_Number"] = _dt.Rows[0]["House_Number"].ToString();
            dr["House_Number_Sup"] = _dt.Rows[0]["House_Number_Sup"].ToString();
            dr["Floor"] = _dt.Rows[0]["Floor"].ToString();
            dr["Premise_Type"] = _dt.Rows[0]["Premise_Type"].ToString();
            dr["Street"] = _dt.Rows[0]["Street"].ToString();
            dr["Street2"] = _dt.Rows[0]["Street2"].ToString();
            dr["Street3"] = _dt.Rows[0]["Street3"].ToString();
            dr["Street4"] = _dt.Rows[0]["Street4"].ToString();
            dr["City"] = _dt.Rows[0]["City"].ToString();
            dr["Post_Code"] = _dt.Rows[0]["Post_Code"].ToString();
            dr["Region"] = _dt.Rows[0]["Region"].ToString();
            dr["Country"] = _dt.Rows[0]["Country"].ToString();
            dr["Desc_Con_Object"] = _dt.Rows[0]["Desc_Con_Object"].ToString();
            dr["Reg_Str_Group"] = _dt.Rows[0]["Reg_Str_Group"].ToString();
            dr["Device_Sr_Number"] = _dt.Rows[0]["Device_Sr_Number"].ToString();
            dr["Telephone_No"] = _dt.Rows[0]["Telephone_No"].ToString();
            dr["Mru"] = _dt.Rows[0]["Mru"].ToString();
            dr["Func_Descr"] = _dt.Rows[0]["Func_Descr"].ToString();
            dr["Outage_Fromtime"] = _dt.Rows[0]["Outage_Fromtime"].ToString();
            dr["Outage_Totime"] = _dt.Rows[0]["Outage_Totime"].ToString();
            dr["Legacy_Acct"] = _dt.Rows[0]["Legacy_Acct"].ToString();
            dr["Bill_Class"] = _dt.Rows[0]["Bill_Class"].ToString();
            dr["Rate_Cat"] = _dt.Rows[0]["Rate_Cat"].ToString();
            dr["Activity"] = _dt.Rows[0]["Activity"].ToString();
            dr["Adr_Notes"] = _dt.Rows[0]["Adr_Notes"].ToString();
            dr["Tel1_Number"] = _dt.Rows[0]["Tel1_Number"].ToString();
            dr["Vertrag"] = _dt.Rows[0]["Vertrag"].ToString();
            dr["E_Mail"] = _dt.Rows[0]["E_Mail"].ToString();
            dr["Move_Out_Date"] = _dt.Rows[0]["Move_Out_Date"].ToString();
            dr["Con_Obj_No"] = _dt.Rows[0]["Con_Obj_No"].ToString();
            dr["Clerk_Id"] = _dt.Rows[0]["Clerk_Id"].ToString();
            dr["Text"] = _dt.Rows[0]["Text"].ToString();
            dr["Status"] = _dt.Rows[0]["Status"].ToString();
            dr["Discreason"] = _dt.Rows[0]["Discreason"].ToString();
            dr["POLE_NO"] = _dt.Rows[0]["POLE_NO"].ToString();
            dr["DUE_DATE"] = _dt.Rows[0]["DUEDATE"].ToString();
            dr["NET_AMNT"] = _dt.Rows[0]["AMOUNT"].ToString();
            dr["SANCTIONED_LOAD"] = _dt.Rows[0]["SANCTIONED_LOAD"].ToString();
            dr["COMPANY_CODE"] = _dt.Rows[0]["COMPANY_CODE"].ToString();
            dr["BILL_BASIS"] = _dt.Rows[0]["BILL_BASIS"].ToString();

            ISUSTDTable.Rows.Add(dr);
            ISUSTDTable.AcceptChanges();

            ds.Tables.Add(ISUSTDTable);
            ds.Tables[0].TableName = "ISUSTDTable";
            ds.DataSetName = "BAPI_RESULT";
        }
        else
        {

            ISUSTDTable.Rows.Add("No Record Found", "No Record Found", "No Record Found", "No Record Found", "No Record Found", "No Record Found");
            ds.Tables.Add(ISUSTDTable);

            ds.Tables[0].TableName = "ISUSTDTable";
            ds.DataSetName = "BAPI_RESULT";
        }

        return ds;
    }

    public DataTable dmlgetqueryCon(string sql)
    {
        DataTable dt = new DataTable();
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbDataAdapter da;

        OleDbConnection ocon = new OleDbConnection(objNDS.con());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand = new OleDbCommand();
            dbcommand.Connection = ocon;
            da = new OleDbDataAdapter();
            da.SelectCommand = dbcommand;
            dt = null;
            dt = new DataTable();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandText = sql;
            //dbcommand.Transaction = dbtrans;
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return dt;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }

    public DataTable dmlgetquery(string sql)
    {
        DataTable dt = new DataTable();
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbDataAdapter da;

        OleDbConnection ocon = new OleDbConnection(objNDS.conPCM());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand = new OleDbCommand();
            dbcommand.Connection = ocon;
            da = new OleDbDataAdapter();
            da.SelectCommand = dbcommand;
            dt = null;
            dt = new DataTable();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandText = sql;
            //dbcommand.Transaction = dbtrans;
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return dt;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }
    public DataTable dmlgetquery_Ebsdb(string sql)
    {
        DataTable dt = new DataTable();
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbDataAdapter da;

        OleDbConnection ocon = new OleDbConnection(objNDS.con());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand = new OleDbCommand();
            dbcommand.Connection = ocon;
            da = new OleDbDataAdapter();
            da.SelectCommand = dbcommand;
            dt = null;
            dt = new DataTable();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandText = sql;
            //dbcommand.Transaction = dbtrans;
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return dt;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }
    public DataTable dmlgetquery_IOMSDB(string sql)
    {
        DataTable dt = new DataTable();
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbDataAdapter da;

        OleDbConnection ocon = new OleDbConnection(objNDS.con_IOMSDB());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand = new OleDbCommand();
            dbcommand.Connection = ocon;
            da = new OleDbDataAdapter();
            da.SelectCommand = dbcommand;
            dt = null;
            dt = new DataTable();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandText = sql;
            //dbcommand.Transaction = dbtrans;
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return dt;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }

    [WebMethod]
    public DataSet ZBAPI_DISPLAY_BILL_WEB(string strCANumber, string strBillMonth)
    {
        DataSet Ds = obj.Get_ZBAPI_DISPLAY_BILL_WEB(strCANumber, strBillMonth);
        return Ds;


    }

    [WebMethod]
    public DataSet Z_BAPI_CMS_ISU_CA_DISPLAY(string strCANumber, string strSerialNumber, string strConsumerNumber, string strTelephoneNumber,
                                                    string strKNumber, string strContractNumber)
    {
        DataSet Ds = obj.Get_Z_BAPI_CMS_ISU_CA_DISPLAY(strCANumber, strSerialNumber, strConsumerNumber, strTelephoneNumber, strKNumber, strContractNumber);
       // DataSet BAPI_RESULT = GetCAWise_DisplayData(strCANumber);// commented by ajay 10.09.2023
        return Ds;
    }
    [WebMethod]
    public DataSet ZBAPI_DISPLAY_BILL_WEB1(string strCANumber, string strBillMonth)
    {
        //DataSet Ds = obj.Get_ZBAPI_DISPLAY_BILL_WEB(strCANumber, strBillMonth);
        DataTable _dtData = new DataTable();

        //CITY, FLAG2, NET_AMT_AFT_TDS -- By RV

        //  if(GetCAWise_DisplayData(strCANumber).Tables.Count>0)
        _dtData = GetCAWise_DisplayData(strCANumber).Tables[0];

        DataSet Ds = new DataSet();

        if (_dtData.Rows.Count > 0)
        {
            DataTable billDetailsTable1 = new DataTable();
            DataColumn column = new DataColumn();
            billDetailsTable1.Columns.Add("INVOICE_NO", typeof(string));
            billDetailsTable1.Columns.Add("CONSUMER_NO", typeof(string));
            billDetailsTable1.Columns.Add("DATE_OF_INVOICE", typeof(string));
            billDetailsTable1.Columns.Add("CYCLE_NO", typeof(string));
            billDetailsTable1.Columns.Add("FIRSTNAME", typeof(string));
            billDetailsTable1.Columns.Add("HOUSE_NO", typeof(string));
            billDetailsTable1.Columns.Add("UNIT_DESCR", typeof(string));
            billDetailsTable1.Columns.Add("CIRCLE_DESCR", typeof(string));
            billDetailsTable1.Columns.Add("NET_AMNT", typeof(string));
            billDetailsTable1.Columns.Add("DUE_DATE", typeof(string));
            billDetailsTable1.Columns.Add("BILL_MONTH", typeof(string));
            billDetailsTable1.Columns.Add("TOT_UNITS_BILLED", typeof(string));
            billDetailsTable1.Columns.Add("BILL_BASIS", typeof(string));
            billDetailsTable1.Columns.Add("SANC_LOAD", typeof(string));
            billDetailsTable1.Columns.Add("RATE_CATEGORY", typeof(string));
            billDetailsTable1.Columns.Add("CONT_DEMAND", typeof(string));
            billDetailsTable1.Columns.Add("TOT_ENRGY_CHRG", typeof(string));
            billDetailsTable1.Columns.Add("TOT_ENE_CHRG_AMD", typeof(string));
            billDetailsTable1.Columns.Add("TOT_ENE_CHG_AMDA", typeof(string));
            billDetailsTable1.Columns.Add("TOT_FIX_CHRG", typeof(string));
            billDetailsTable1.Columns.Add("TOT_FIX_CHRG_AMD", typeof(string));
            billDetailsTable1.Columns.Add("TOT_FIX_CHG_AMDA", typeof(string));
            billDetailsTable1.Columns.Add("ELECT_TAX", typeof(string));
            billDetailsTable1.Columns.Add("ELECT_TAX_AMNDS", typeof(string));
            billDetailsTable1.Columns.Add("ELECT_TAX_AMDA", typeof(string));
            billDetailsTable1.Columns.Add("REBATE", typeof(string));
            billDetailsTable1.Columns.Add("REBATE_AMNDS", typeof(string));
            billDetailsTable1.Columns.Add("REBATE_AMNDTAM", typeof(string));
            billDetailsTable1.Columns.Add("RATE_SUBSIDY", typeof(string));
            billDetailsTable1.Columns.Add("RATE_SUBSIDY_AMD", typeof(string));
            billDetailsTable1.Columns.Add("RATE_SBSIDY_AMDA", typeof(string));
            billDetailsTable1.Columns.Add("SPL_SUBSIDY", typeof(string));
            billDetailsTable1.Columns.Add("SPL_SUBSIDY_AMN", typeof(string));
            billDetailsTable1.Columns.Add("SPL_SBSIDY_AMDA", typeof(string));
            billDetailsTable1.Columns.Add("OTHER_CHRGS", typeof(string));
            billDetailsTable1.Columns.Add("OTHER_CHRGS_AMND", typeof(string));
            billDetailsTable1.Columns.Add("OTHER_CHRGS_AMDA", typeof(string));
            billDetailsTable1.Columns.Add("LPSC_CURRENT", typeof(string));
            billDetailsTable1.Columns.Add("CUR_MTH_BILL_AMT", typeof(string));
            billDetailsTable1.Columns.Add("CUR_MTH_BILL_AMD", typeof(string));
            billDetailsTable1.Columns.Add("CUR_MTH_BIL_AMDA", typeof(string));
            billDetailsTable1.Columns.Add("ARR_PAYABLE", typeof(string));
            billDetailsTable1.Columns.Add("ARR_PAYABLE_AMD", typeof(string));
            billDetailsTable1.Columns.Add("ARR_PAYABLE_AMDA", typeof(string));
            billDetailsTable1.Columns.Add("ARR_ENRGY_CHRG", typeof(string));
            billDetailsTable1.Columns.Add("ARR_ELECT_TAX", typeof(string));
            billDetailsTable1.Columns.Add("ARR_OTHER_CHRGS", typeof(string));
            billDetailsTable1.Columns.Add("ARR_LPSC", typeof(string));
            billDetailsTable1.Columns.Add("AR_LAST_MTH_BILL", typeof(string));
            billDetailsTable1.Columns.Add("REFUND", typeof(string));
            billDetailsTable1.Columns.Add("DEFERRED_AMNT", typeof(string));
            billDetailsTable1.Columns.Add("INST_NOT_DUE", typeof(string));
            billDetailsTable1.Columns.Add("PYMT_RECD_AMNT", typeof(string));
            billDetailsTable1.Columns.Add("ARREARS_PAYABLE", typeof(string));
            billDetailsTable1.Columns.Add("AMEN_PERIOD_FRM", typeof(string));
            billDetailsTable1.Columns.Add("AMEN_PERIOD_TO", typeof(string));
            billDetailsTable1.Columns.Add("AMD_REASON", typeof(string));
            billDetailsTable1.Columns.Add("TOT_ORIG_AMNT", typeof(string));
            billDetailsTable1.Columns.Add("TOT_AMND_AMNT", typeof(string));
            billDetailsTable1.Columns.Add("UNITS_1", typeof(string));
            billDetailsTable1.Columns.Add("UNITS_2", typeof(string));
            billDetailsTable1.Columns.Add("UNITS_3", typeof(string));
            billDetailsTable1.Columns.Add("UNITS_4", typeof(string));
            billDetailsTable1.Columns.Add("UNITS_5", typeof(string));
            billDetailsTable1.Columns.Add("UNITS_6", typeof(string));
            billDetailsTable1.Columns.Add("AMNT_AFT_DUE_DAT", typeof(string));
            billDetailsTable1.Columns.Add("PYMT_ACCNTD_UPTO", typeof(string));
            billDetailsTable1.Columns.Add("SEC_DEP_AMNT", typeof(string));
            billDetailsTable1.Columns.Add("TARIFF", typeof(string));
            billDetailsTable1.Columns.Add("METER_TYPE", typeof(string));
            billDetailsTable1.Columns.Add("COMPANY_CODE", typeof(string));
            billDetailsTable1.Columns.Add("LASTNAME", typeof(string));
            billDetailsTable1.Columns.Add("MIDDLE_NAME", typeof(string));
            billDetailsTable1.Columns.Add("FATHER_NAME", typeof(string));
            billDetailsTable1.Columns.Add("STREET", typeof(string));
            billDetailsTable1.Columns.Add("STR_SUPPL1", typeof(string));
            billDetailsTable1.Columns.Add("STR_SUPPL2", typeof(string));
            billDetailsTable1.Columns.Add("STR_SUPPL3", typeof(string));
            billDetailsTable1.Columns.Add("CITY", typeof(string));
            billDetailsTable1.Columns.Add("POSTL_COD1", typeof(string));
            billDetailsTable1.Columns.Add("ADJUSTMENT", typeof(string));
            billDetailsTable1.Columns.Add("PAYMENT_RECEIVED", typeof(string));
            billDetailsTable1.Columns.Add("PAYMENT_DATE", typeof(string));
            billDetailsTable1.Columns.Add("TOT_BIL_AMT", typeof(string));
            billDetailsTable1.Columns.Add("TOT_BIL_AMNDS", typeof(string));
            billDetailsTable1.Columns.Add("TOT_BIL_AMDTAM", typeof(string));
            billDetailsTable1.Columns.Add("MDI", typeof(string));
            billDetailsTable1.Columns.Add("MOB_NO", typeof(string));
            billDetailsTable1.Columns.Add("EMAIL", typeof(string));
            billDetailsTable1.Columns.Add("FLAG2", typeof(string));
            billDetailsTable1.Columns.Add("NET_AMT_AFT_TDS", typeof(string));

            DataRow dr;
            dr = billDetailsTable1.NewRow();
            dr["INVOICE_NO"] = ".";

            dr["CONSUMER_NO"] = _dtData.Rows[0]["CA_NUMBER"].ToString();
            dr["DATE_OF_INVOICE"] = ".";
            dr["CYCLE_NO"] = ".";
            dr["FIRSTNAME"] = _dtData.Rows[0]["BP_NAME"].ToString().Trim();
            dr["HOUSE_NO"] = ".";
            dr["UNIT_DESCR"] = _dtData.Rows[0]["REG_STR_GROUP"].ToString();
            dr["CIRCLE_DESCR"] = ".";

            if (_dtData.Rows[0]["NET_AMNT"] != null)
            {
                if (_dtData.Rows[0]["NET_AMNT"].ToString().Trim() != "")
                {
                    dr["NET_AMNT"] = _dtData.Rows[0]["NET_AMNT"].ToString().Replace(",", "").Trim();

                    if (_dtData.Rows[0]["NET_AMNT"].ToString().Replace(",", "").Trim() == "0")
                        dr["DUE_DATE"] = "0";
                    else
                        dr["DUE_DATE"] = _dtData.Rows[0]["DUE_DATE"].ToString();
                }
                else
                {
                    dr["NET_AMNT"] = "0";
                    dr["DUE_DATE"] = "0";
                }
            }
            else if (_dtData.Rows[0]["NET_AMNT"].ToString() == "")
            {
                dr["NET_AMNT"] = "0";
                dr["DUE_DATE"] = "0";
            }
            else
            {
                dr["NET_AMNT"] = "0";
                dr["DUE_DATE"] = "0";
            }

            //dr["DUE_DATE"] = _dtData.Rows[0]["DUE_DATE"].ToString();
            dr["BILL_MONTH"] = ".";
            dr["TOT_UNITS_BILLED"] = ".";
            dr["BILL_BASIS"] = _dtData.Rows[0]["BILL_BASIS"].ToString();
            dr["SANC_LOAD"] = _dtData.Rows[0]["SANCTIONED_LOAD"].ToString();
            dr["RATE_CATEGORY"] = _dtData.Rows[0]["Rate_Cat"].ToString();
            dr["CONT_DEMAND"] = "0";
            dr["TOT_ENRGY_CHRG"] = "0";
            dr["TOT_ENE_CHRG_AMD"] = "0";
            dr["TOT_ENE_CHG_AMDA"] = "0";
            dr["TOT_FIX_CHRG"] = "0";
            dr["TOT_FIX_CHRG_AMD"] = "0";
            dr["TOT_FIX_CHG_AMDA"] = "0";
            dr["ELECT_TAX"] = "0";
            dr["ELECT_TAX_AMNDS"] = "0";
            dr["ELECT_TAX_AMDA"] = "0";
            dr["REBATE"] = "0";
            dr["REBATE_AMNDS"] = "0";
            dr["REBATE_AMNDTAM"] = "0";
            dr["RATE_SUBSIDY"] = "0";
            dr["RATE_SUBSIDY_AMD"] = "0";
            dr["RATE_SBSIDY_AMDA"] = "0";
            dr["SPL_SUBSIDY"] = "0";
            dr["SPL_SUBSIDY_AMN"] = "0";
            dr["SPL_SBSIDY_AMDA"] = "0";
            dr["OTHER_CHRGS"] = "0";
            dr["OTHER_CHRGS_AMND"] = "0";
            dr["OTHER_CHRGS_AMDA"] = "0";
            dr["LPSC_CURRENT"] = "0";
            dr["CUR_MTH_BILL_AMT"] = "0";
            dr["CUR_MTH_BILL_AMD"] = "0";
            dr["CUR_MTH_BIL_AMDA"] = "0";
            dr["ARR_PAYABLE"] = "0";
            dr["ARR_PAYABLE_AMD"] = "0";
            dr["ARR_PAYABLE_AMDA"] = "0";
            dr["ARR_ENRGY_CHRG"] = "0";
            dr["ARR_ELECT_TAX"] = "0";
            dr["ARR_OTHER_CHRGS"] = "0";
            dr["ARR_LPSC"] = "0";
            dr["AR_LAST_MTH_BILL"] = "0";
            dr["REFUND"] = "0";
            dr["DEFERRED_AMNT"] = "0";
            dr["INST_NOT_DUE"] = "0";
            dr["PYMT_RECD_AMNT"] = "0";
            dr["ARREARS_PAYABLE"] = "0";
            dr["AMEN_PERIOD_FRM"] = "0";
            dr["AMEN_PERIOD_TO"] = "0";
            dr["AMD_REASON"] = ".";
            dr["TOT_ORIG_AMNT"] = "0";
            dr["TOT_AMND_AMNT"] = "0";
            dr["UNITS_1"] = "0";
            dr["UNITS_2"] = "0";
            dr["UNITS_3"] = "0";
            dr["UNITS_4"] = "0";
            dr["UNITS_5"] = "0";
            dr["UNITS_6"] = "0";
            dr["AMNT_AFT_DUE_DAT"] = "0";
            dr["PYMT_ACCNTD_UPTO"] = "0";
            dr["SEC_DEP_AMNT"] = "0";
            dr["TARIFF"] = "0";
            dr["METER_TYPE"] = ".";
            dr["COMPANY_CODE"] = _dtData.Rows[0]["COMPANY_CODE"].ToString();
            dr["LASTNAME"] = ".";
            dr["MIDDLE_NAME"] = ".";
            dr["FATHER_NAME"] = ".";
            dr["STREET"] = _dtData.Rows[0]["STREET"].ToString();
            dr["STR_SUPPL1"] = ".";
            dr["STR_SUPPL2"] = ".";
            dr["STR_SUPPL3"] = ".";
            dr["POSTL_COD1"] = ".";
            dr["ADJUSTMENT"] = ".";
            dr["PAYMENT_RECEIVED"] = "0";
            dr["PAYMENT_DATE"] = "0";
            dr["TOT_BIL_AMT"] = "0";
            dr["TOT_BIL_AMNDS"] = "0";
            dr["TOT_BIL_AMDTAM"] = "0";
            dr["MDI"] = "0";
            dr["MOB_NO"] = _dtData.Rows[0]["Tel1_Number"].ToString();
            dr["EMAIL"] = _dtData.Rows[0]["E_Mail"].ToString();
            dr["CITY"] = ".";
            dr["FLAG2"] = ".";
            dr["NET_AMT_AFT_TDS"] = ".";

            billDetailsTable1.Rows.Add(dr);
            billDetailsTable1.AcceptChanges();

            DataTable meterDetailsTable1 = new DataTable();
            meterDetailsTable1.Columns.Add("Meter_No", typeof(string));
            meterDetailsTable1.Columns.Add("Mf_Factor", typeof(string));
            meterDetailsTable1.Columns.Add("Pre_Mr_Date", typeof(string));
            meterDetailsTable1.Columns.Add("Curr_Mr_Date", typeof(string));
            meterDetailsTable1.Columns.Add("Prev_Mr_Kwh", typeof(string));
            meterDetailsTable1.Columns.Add("Curr_Mr_Kwh", typeof(string));
            meterDetailsTable1.Columns.Add("Unit_Consum_Kwh", typeof(string));
            meterDetailsTable1.Columns.Add("Prev_Mr_Kvah", typeof(string));
            meterDetailsTable1.Columns.Add("Curr_Mr_Kvah", typeof(string));
            meterDetailsTable1.Columns.Add("Unit_Consum_Kvah", typeof(string));
            meterDetailsTable1.Columns.Add("Prev_Mr_Kw", typeof(string));
            meterDetailsTable1.Columns.Add("Curr_Mr_Kw", typeof(string));
            meterDetailsTable1.Columns.Add("Unit_Consum_Kw", typeof(string));
            meterDetailsTable1.Columns.Add("Prev_Mr_Kva", typeof(string));
            meterDetailsTable1.Columns.Add("Curr_Mr_Kva", typeof(string));
            meterDetailsTable1.Columns.Add("Unit_Consum_Kva", typeof(string));

            DataRow dr1;
            dr1 = meterDetailsTable1.NewRow();
            dr1["Meter_No"] = "0";
            dr1["Mf_Factor"] = "0";
            dr1["Pre_Mr_Date"] = "0";
            dr1["Curr_Mr_Date"] = "0";
            dr1["Prev_Mr_Kwh"] = "0";
            dr1["Curr_Mr_Kwh"] = "0";
            dr1["Unit_Consum_Kwh"] = "0";
            dr1["Prev_Mr_Kvah"] = "0";
            dr1["Curr_Mr_Kvah"] = "0";
            dr1["Unit_Consum_Kvah"] = "0";
            dr1["Prev_Mr_Kw"] = "0";
            dr1["Curr_Mr_Kw"] = "0";
            dr1["Unit_Consum_Kw"] = "0";
            dr1["Prev_Mr_Kva"] = "0";
            dr1["Curr_Mr_Kva"] = "0";
            dr1["Unit_Consum_Kva"] = "0";

            meterDetailsTable1.Rows.Add(dr1);
            meterDetailsTable1.AcceptChanges();

            Ds.Tables.Add(billDetailsTable1);
            Ds.Tables.Add(meterDetailsTable1);

            Ds.Tables[0].TableName = "billDetailsTable";
            Ds.Tables[1].TableName = "meterDetailsTable";

            Ds.DataSetName = "BAPI_RESULT";

            // Ds.Merge(billDetailsTable.Copy());

        }

        return Ds;
    }

    public DataTable GetCAWise_SDKInfo(string _sCaNumber)
    {

        DataTable _dt = new DataTable();
        string _sSql = string.Empty;
        if (_sCaNumber.Length == 9)
            _sCaNumber = "000" + _sCaNumber;
        else if (_sCaNumber.Length == 10)
            _sCaNumber = "00" + _sCaNumber;
        else if (_sCaNumber.Length == 11)
            _sCaNumber = "0" + _sCaNumber;

        _sSql = " select S.MOBILE_NO Tel1_Number, EMAIL E_Mail,substr(AMOUNT,4)AMOUNT,'BRPL'COMPANY_CODE ";
        _sSql += " from pcm.CONSUMER_SAP_MASTER S, MOBAPP.BRPL_BILL_MASTER M where S.CONTRACT_ACCOUNT='000'||M.CA_NUMBER and S.CONTRACT_ACCOUNT='" + _sCaNumber + "' ";
        _sSql += " order by m.SMS_GEN_DATE desc  ";

        _dt = dmlgetquery(_sSql);

        if (_dt.Rows.Count == 0)
        {
            _sSql = "  select  TELEPHONE_NUMBER Tel1_Number,EMAIL E_Mail,AMOUNT,COMPANY_CODE ";
            _sSql += " from admin.payment_mobile_data  S, ADMIN.BYPL_BILL_MASTER M where S.CONTRACT_ACCOUNT='000'||M.CA_NUMBER  AND S.CONTRACT_ACCOUNT='" + _sCaNumber + "'  ";
            _sSql += "  order by m.SMS_GEN_DATE desc ";

            _dt = dmlgetquery(_sSql);

            if (_dt.Rows.Count == 0)
            {
                _sSql = " select  S.MOBILE_NO Tel1_Number,'' E_Mail,substr(AMOUNT,4) AMOUNT, SAP_COMPANY COMPANY_CODE";
                _sSql += " from rcmpa.sap_formy_vws S, MOBAPP.BRPL_BILL_MASTER M where S.CONS_REF='000'||M.CA_NUMBER(+)  AND S.CONS_REF='" + _sCaNumber + "'  ";
                _sSql += " order by m.SMS_GEN_DATE desc  ";

                _dt = dmlgetquery(_sSql);
            }
        }


        return _dt;
    }

    [WebMethod]
    public string BSES_SDK_PAYMENT(string strCANumber)
    {
        string _sResult = string.Empty;
        string _sTelNo = string.Empty, _sEmail = string.Empty, _sAmt = string.Empty, _sCompCode = string.Empty;



        DataTable _dtData = new DataTable();
        _dtData = GetCAWise_SDKInfo(strCANumber);

        if (_dtData.Rows.Count > 0)
        {
            if (_dtData.Rows[0]["Tel1_Number"] != null)
                _sTelNo = _dtData.Rows[0]["Tel1_Number"].ToString();
            else
                _sTelNo = "";

            if (_dtData.Rows[0]["E_Mail"] != null)
                _sEmail = _dtData.Rows[0]["E_Mail"].ToString();
            else
                _sEmail = "";

            if (_dtData.Rows[0]["AMOUNT"] != null)
            {
                _sAmt = _dtData.Rows[0]["AMOUNT"].ToString().Replace(",", "").Trim();
                if (_sAmt.ToString().Trim() == "")
                    _sAmt = "0";
            }
            else
                _sAmt = "0";

            if (_dtData.Rows[0]["COMPANY_CODE"] != null)
                _sCompCode = _dtData.Rows[0]["COMPANY_CODE"].ToString();
            else
                _sCompCode = "";

            _sResult = _sTelNo + "|" + _sEmail + "|" + _sAmt + "|" + _sCompCode;
        }
        else
        {
            _sResult = "n|n|n|n";
        }

        return _sResult;

    }

    [WebMethod]
    public DataSet ZBAPI_CA_OUTSTANDING_AMT(string strCANumber)
    {
        DataSet dsBAPIOutput = obj.get_ZFI_CURR_OUTS_FLAG(strCANumber);

        return dsBAPIOutput;
    }

    [WebMethod]
    public DataSet ZBAPI_LAST_MODE_PAY(string CA, string FLAG)
    {
        DataSet Ds = obj.Get_ZBAPI_LAST_MODE_PAY(CA, FLAG);
        return Ds;
    }

    [WebMethod]
    public DataSet BAPI_MTRREADDOC_GETLIST(string METERNO)//By Babalu Kumar
    {
        string KWH = string.Empty;
        string KW = string.Empty;
        string KVAH = string.Empty;
        string KVA = string.Empty;
        string Readdate = string.Empty;
        string MeterNo = string.Empty;
        DataSet Ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("Device", typeof(string));
        dt.Columns.Add("BillDate", typeof(string));
        dt.Columns.Add("KWH", typeof(string));
        dt.Columns.Add("KW", typeof(string));
        dt.Columns.Add("KVAH", typeof(string));
        dt.Columns.Add("KVA", typeof(string));
        try
        {
            DataSet ds = obj.Get_BAPI_MTRREADDOC_GETLIST(METERNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtnew = SelectDistinct(ds.Tables[0], "MRDATEFORBILLING");
                DataRow dr;
                for (int i = 0; i <= dtnew.Rows.Count - 1; i++)
                {
                    KWH = "NA";
                    KW = "NA";
                    KVAH = "NA";
                    KVA = "NA";
                    MeterNo = ds.Tables[0].Rows[i]["DEVICE"].ToString();
                    Readdate = dtnew.Rows[i]["MRDATEFORBILLING"].ToString();
                    dr = dt.NewRow();
                    DataRow[] result = ds.Tables[0].Select("MRDATEFORBILLING ='" + dtnew.Rows[i]["MRDATEFORBILLING"].ToString() + "'");
                    foreach (DataRow row in result)
                    {
                        if (row[3].ToString() == "001")
                        {
                            KWH = row[13].ToString();

                        }
                        else if (row[3].ToString() == "002")
                        {
                            KW = row[13].ToString();
                        }
                        else if (row[3].ToString() == "003")
                        {
                            KVAH = row[13].ToString();
                        }
                        else if (row[3].ToString() == "004")
                        {
                            KVA = row[13].ToString();
                        }
                    }
                    dr["Device"] = MeterNo;
                    dr["BillDate"] = Readdate;
                    dr["KWH"] = KWH;
                    dr["KW"] = KW;
                    dr["KVAH"] = KVAH;
                    dr["KVA"] = KVA;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
                Ds.Tables.Add(dt);
            }
            else
            {
                dt.Rows.Add("Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again");
                Ds.Tables.Add(dt);
            }
            return Ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add(ex.Message.ToString(), "", "", "", "", "");
            Ds.Tables.Add(dt);
            return Ds;
        }
    }

    private bool ColumnEqual(object A, object B)
    {
        if (A == DBNull.Value && B == DBNull.Value)
            return true;
        if (A == DBNull.Value || B == DBNull.Value)
            return false;
        return (A.Equals(B));
    }
    public DataTable SelectDistinct(DataTable SourceTable, string FieldName)
    {
        DataTable dt = new DataTable(SourceTable.TableName);
        dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);
        object LastValue = null;
        foreach (DataRow dr in SourceTable.Select("", FieldName))
        {
            if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
            {
                LastValue = dr[FieldName];
                dt.Rows.Add(new object[] { LastValue });
            }
        }
        return dt;
    }

    [WebMethod]
    public DataSet ZBAPI_FETCH_ENF_USER_DET(string CA_NUMBER) //Added By Babalu Kumar 14082020
    {
        DataTable dt = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("CASE_ID", typeof(string));
        dt.Columns.Add("COMPLAINT_ID", typeof(string));
        dt.Columns.Add("INSPECTION_DATE", typeof(string));
        dt.Columns.Add("INSP_CA_NUMBER", typeof(string));
        dt.Columns.Add("USER_NAME", typeof(string));
        dt.Columns.Add("HOUSEFLATNO", typeof(string));
        dt.Columns.Add("BUILDING_NAME1", typeof(string));
        dt.Columns.Add("STREET1", typeof(string));
        dt.Columns.Add("COLONY_AREA1", typeof(string));
        dt.Columns.Add("LANDMARK", typeof(string));
        dt.Columns.Add("CITY_CODE1", typeof(string));
        dt.Columns.Add("PIN_CODE1", typeof(string));
        dt.Columns.Add("CASE_TYPE", typeof(string));
        dt.Columns.Add("ENF_ORDER", typeof(string));
        dt.Columns.Add("ENF_CA", typeof(string));
        dt.Columns.Add("SOURCE_OF_COMPLA", typeof(string));
        dt.Columns.Add("COKEY", typeof(string));
        dt.Columns.Add("SUB_DIV", typeof(string));
        DataSet Ds = obj.Get_ZBAPI_FETCH_ENF_USER_DET(CA_NUMBER);
        if (Ds.Tables[0].Rows.Count > 0)
        {
            Ds.Tables.Add(dt);
            Ds.Tables.Remove("Table2");
            Ds.Tables.Remove("messageTable");
            dt.AcceptChanges();
        }
        else
        {
            Ds.Tables.Remove("Table1");
            Ds.Tables.Remove("messageTable");
            dt.TableName = "Table1";
            dt.Rows.Add("Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again",
                "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again",
                "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again",
                "Data not available try again", "Data not available try again", "Data not available try again");
            Ds.Tables.Add(dt);
            dt.AcceptChanges();

        }
        return Ds;
    }

    [WebMethod]
    public DataSet ZBAPI_BILL_DET(string CA_NUMBER)
    {
        DataSet dsCAInfo = new DataSet();
        DataTable dt = new DataTable();
        string _sCircle = string.Empty, _sDivision = string.Empty, _sCompany = string.Empty, _sName = string.Empty;
        string _sSerReqDateTime = System.DateTime.Now.ToString();
        string _sSerReqOutDateTime = string.Empty;

        DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
        dsCAInfo = new DataSet();
        dsCAInfo = obj1.ZBAPI_DISPLAY_BILL_WEB(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER, "");
        if (dsCAInfo.Tables[0].Rows.Count > 0)
        {
            dt = new DataTable();
            dt = dsCAInfo.Tables[0];
            if (dt.Rows.Count > 0)
            {
                //CA_NUMBER = CA_NUMBER.ToString().Substring(3, 9);
                _sName = Convert.ToString(dt.Rows[0]["FIRSTNAME"]) + " " + Convert.ToString(dt.Rows[0]["MIDDLE_NAME"]) + " " + Convert.ToString(dt.Rows[0]["LASTNAME"]);
                _sCircle = Convert.ToString(dt.Rows[0]["CIRCLE_DESCR"]);
                _sDivision = Convert.ToString(dt.Rows[0]["UNIT_DESCR"]);
                _sCompany = Convert.ToString(dt.Rows[0]["COMPANY_CODE"]);

                Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, "", "");
            }
        }

        DataSet Ds = obj.Get_ZBAPI_BILL_DET(CA_NUMBER);
        Insert_Service_Log(CA_NUMBER, null, _sName, _sCircle, _sDivision, _sCompany, "", "");

        return Ds;
    }

    private void Insert_Service_Log(string _sCA_No, string _sReqInDateTime, string Name, string Circle, string Division, string CompName, string MobileNo,
                                   string ResponseData)
    {
        string _sSql = string.Empty;
        string _sReqInTime = string.Empty;
        if (_sReqInDateTime != null)
            _sReqInTime = _sReqInDateTime;
        else
            _sReqInDateTime = null;

        if (_sReqInDateTime != null)
        {
            // System.DateTime.Now.ToString("dd/MM/yyyy hh:m:ss")
            _sReqInDateTime = Convert.ToDateTime(_sReqInDateTime).ToString("dd/MM/yyyy hh:m:ss");
        }

        if (_sReqInDateTime != null)
        {
            _sSql = "INSERT INTO mobapp.BRPL_SERVICE_LOG_DT(CA_NUMBER,SERVICE_TYPE,CONS_NAME,CIRCLE,DIVISION,COMPANY_NAME,MOBILE_NO,REQ_SERVICE_INDATE,REQ_SERVICE_OUTDATE,RESPONSE) VALUES ";
            _sSql += " ('" + _sCA_No + "','BILLEXPLAIN-WHATSAPP','" + Name + "','" + Circle + "','" + Division + "','" + CompName + "','" + MobileNo + "',TO_DATE('" + _sReqInDateTime + "','DD/MM/YYYY HH24:MI:SS'),sysdate,'" + ResponseData + "')";


            //_sSql = "INSERT INTO mobapp.BRPL_SERVICE_LOG_DT(CA_NUMBER,SERVICE_TYPE,CONS_NAME,CIRCLE,DIVISION,COMPANY_NAME,MOBILE_NO,REQ_SERVICE_INDATE,REQ_SERVICE_OUTDATE,RESPONSE) VALUES ";
            //_sSql += " ('" + _sCA_No + "','BILLEXPLAIN-WHATSAPP','" + Name + "','" + Circle + "','" + Division + "','" + CompName + "','" + MobileNo + "',sysdate,null,'" + ResponseData + "')";
        }
        //else
        //{
        //    _sSql = "INSERT INTO mobapp.BRPL_SERVICE_LOG_DT(CA_NUMBER,SERVICE_TYPE,CONS_NAME,CIRCLE,DIVISION,COMPANY_NAME,MOBILE_NO,REQ_SERVICE_INDATE,REQ_SERVICE_OUTDATE,RESPONSE) VALUES ";
        //    _sSql += " ('" + _sCA_No + "','BILLEXPLAIN-WHATSAPP','" + Name + "','" + Circle + "','" + Division + "','" + CompName + "','" + MobileNo + "',null,sysdate,'" + ResponseData + "')";
        //}

        dmlsinglequery(_sSql);
    }
    public bool dmlsinglequery(string sql)
    {
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbConnection ocon = new OleDbConnection(objNDS.con());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }

            dbcommand = new OleDbCommand(sql, ocon);
            //dbcommand.Transaction = dbtrans;
            dbcommand.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }
    public bool dmlsinglequery_IOMSDB(string sql)
    {
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbConnection ocon = new OleDbConnection(objNDS.con_IOMSDB());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }

            dbcommand = new OleDbCommand(sql, ocon);
            //dbcommand.Transaction = dbtrans;
            dbcommand.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }

    [WebMethod]
    public DataSet ZBAPI_BILL_DET_API(string CA_NUMBER)
    {
        DataSet dsCAInfo = new DataSet();
        DataTable dt = new DataTable();
        string _sCircle = string.Empty, _sDivision = string.Empty, _sCompany = string.Empty, _sName = string.Empty;
        string _sSerReqDateTime = System.DateTime.Now.ToString();
        string _sSerReqOutDateTime = string.Empty;

        DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
        dsCAInfo = new DataSet();
        dsCAInfo = obj1.ZBAPI_DISPLAY_BILL_WEB(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER, "");
        if (dsCAInfo.Tables[0].Rows.Count > 0)
        {
            dt = new DataTable();
            dt = dsCAInfo.Tables[0];
            if (dt.Rows.Count > 0)
            {
                //CA_NUMBER = CA_NUMBER.ToString().Substring(3, 9);
                _sName = Convert.ToString(dt.Rows[0]["FIRSTNAME"]) + " " + Convert.ToString(dt.Rows[0]["MIDDLE_NAME"]) + " " + Convert.ToString(dt.Rows[0]["LASTNAME"]);
                _sCircle = Convert.ToString(dt.Rows[0]["CIRCLE_DESCR"]);
                _sDivision = Convert.ToString(dt.Rows[0]["UNIT_DESCR"]);
                _sCompany = Convert.ToString(dt.Rows[0]["COMPANY_CODE"]);

                Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, "", "");
            }
        }

        string str = "";
        DataSet ds = obj.Get_ZBAPI_BILL_DET(CA_NUMBER);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            str += ds.Tables[0].Rows[i][0].ToString().Trim() + Environment.NewLine;
        }

        Insert_Service_Log(CA_NUMBER, null, _sName, _sCircle, _sDivision, _sCompany, "", "");

        DataTable dtOutPut = new DataTable();
        DataColumn column = new DataColumn();
        dtOutPut.Columns.Add("OUT_PUT", typeof(string));
        dtOutPut.Rows.Add(str);
        ds.Tables.Add(dtOutPut);
        dtOutPut.AcceptChanges();

        return ds;
    }

    [WebMethod]
    public DataSet ZBAPI_BILL_DET_API_PDF(string CA_NUMBER, string _sMobileNo)
    {
        DataSet dsCAInfo = new DataSet();
        DataTable dt = new DataTable();
        string _sCircle = string.Empty, _sDivision = string.Empty, _sCompany = string.Empty, _sName = string.Empty;
        string _sSerReqDateTime = System.DateTime.Now.ToString();
        string _sSerReqOutDateTime = string.Empty;
        string str = "";

        DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
        dsCAInfo = new DataSet();
        dsCAInfo = obj1.ZBAPI_DISPLAY_BILL_WEB(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER, "");
        if (dsCAInfo.Tables[0].Rows.Count > 0)
        {
            dt = new DataTable();
            dt = dsCAInfo.Tables[0];
            if (dt.Rows.Count > 0)
            {
                //CA_NUMBER = CA_NUMBER.ToString().Substring(3, 9);
                _sName = Convert.ToString(dt.Rows[0]["FIRSTNAME"]) + " " + Convert.ToString(dt.Rows[0]["MIDDLE_NAME"]) + " " + Convert.ToString(dt.Rows[0]["LASTNAME"]);
                _sCircle = Convert.ToString(dt.Rows[0]["CIRCLE_DESCR"]);
                _sDivision = Convert.ToString(dt.Rows[0]["UNIT_DESCR"]);
                _sCompany = Convert.ToString(dt.Rows[0]["COMPANY_CODE"]);

                // Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, _sMobileNo, "");
                // 

                if (_sCompany.Trim() == "BRPL")
                {
                    str = "https://bsesbrpl.co.in:7850/DelhiV2/BillPDF_DTAPI.aspx?CA_NO=" + CA_NUMBER;
                    Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, _sMobileNo, str);
                }
                else
                {
                    str = "Details not available for CA No. " + CA_NUMBER + " .Please call us at 19123 (Toll-Free) or write to us at brpl.customercare@relianceada.com. Thank you.";
                    Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, _sMobileNo, str);
                }

            }
        }
        else
        {

            str = "Details not available for CA No. " + CA_NUMBER + " .Please call us at 19123 (Toll-Free) or write to us at brpl.customercare@relianceada.com. Thank you.";
            Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, _sMobileNo, str);
        }


        DataSet ds = new DataSet();
        DataTable dtOutPut = new DataTable();
        DataColumn column = new DataColumn();
        dtOutPut.Columns.Add("OUT_PUT", typeof(string));
        dtOutPut.Rows.Add(str);
        ds.Tables.Add(dtOutPut);
        dtOutPut.AcceptChanges();

        return ds;
    }

    [WebMethod]
    public DataSet ZBAPI_BILL_DET_64(string CA_NUMBER)
    {
        DataSet dsCAInfo = new DataSet();
        DataTable dt = new DataTable();
        string _sCircle = string.Empty, _sDivision = string.Empty, _sCompany = string.Empty, _sName = string.Empty;
        string _sSerReqDateTime = System.DateTime.Now.ToString();
        string _sSerReqOutDateTime = string.Empty;

        DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
        dsCAInfo = new DataSet();
        dsCAInfo = obj1.ZBAPI_DISPLAY_BILL_WEB(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER, "");
        if (dsCAInfo.Tables[0].Rows.Count > 0)
        {
            dt = new DataTable();
            dt = dsCAInfo.Tables[0];
            if (dt.Rows.Count > 0)
            {
                //CA_NUMBER = CA_NUMBER.ToString().Substring(3, 9);
                _sName = Convert.ToString(dt.Rows[0]["FIRSTNAME"]) + " " + Convert.ToString(dt.Rows[0]["MIDDLE_NAME"]) + " " + Convert.ToString(dt.Rows[0]["LASTNAME"]);
                _sCircle = Convert.ToString(dt.Rows[0]["CIRCLE_DESCR"]);
                _sDivision = Convert.ToString(dt.Rows[0]["UNIT_DESCR"]);
                _sCompany = Convert.ToString(dt.Rows[0]["COMPANY_CODE"]);

                Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, "", "");
            }
        }

        DataSet Ds = obj.Get_ZBAPI_BILL_DET_64(CA_NUMBER);
        Insert_Service_Log(CA_NUMBER, null, _sName, _sCircle, _sDivision, _sCompany, "", "");

        return Ds;
    }

    [WebMethod]
    public DataSet ZBAPI_STREET_DET_UPD(string COMPANY, string CANUMBER, string DATA_PROCESS_DATE, string STLWATT, string NO_OF_POINT, string INSTALLATION_DATE, string MOVEOUT_DATE, string ACTIVATION, string DEACTIVATION, string REQUESTID, string REQUEST_DATE, string DOCUMENT_UPLOADED)//By Babalu Kumar
    {
        DataSet Ds = obj.Get_ZBAPI_STREET_DET_UPD(COMPANY, CANUMBER, DATA_PROCESS_DATE, STLWATT, NO_OF_POINT, INSTALLATION_DATE, MOVEOUT_DATE, ACTIVATION, DEACTIVATION, REQUESTID, REQUEST_DATE, DOCUMENT_UPLOADED);
        return Ds;
    }

    [WebMethod]
    public DataTable HES_GETLATESTBALANCE(string _sConsumerID, string _sMeterID)
    {
        DataSet dsCAInfo = new DataSet();
        DataTable dt = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("ErrorCode", typeof(string));
        dt.Columns.Add("Balance", typeof(string));
        dt.Columns.Add("MeterReading", typeof(string));
        dt.Columns.Add("MeterRTC", typeof(string));
        dt.Columns.Add("LastRechargeAmount", typeof(string));
        dt.Columns.Add("LastRechargeAmountDateTime", typeof(string));
        dt.Columns.Add("MeterID", typeof(string));

        string _sErrorCode = string.Empty, _sBalance = string.Empty, _sMeterReading = string.Empty;
        string _sMeterRTC = string.Empty, _sLastRechargeAmount = string.Empty, _sLastRechargeAmountDateTime = string.Empty, _sMeter_ID = string.Empty;

        HESWebReference.Service1 obj = new HESWebReference.Service1();
        HESWebReference.ConsumerLatestBalance objSer = new HESWebReference.ConsumerLatestBalance();
        objSer = obj.GetLatestBalance(_sConsumerID, _sMeterID);

        if (objSer.ErrorCode != null)
            _sErrorCode = objSer.ErrorCode;
        if (objSer.Balance != null)
            _sBalance = objSer.Balance.ToString();
        if (objSer.MeterReading != null)
            _sMeterReading = objSer.MeterReading.ToString();
        if (objSer.MeterRTC != null)
            _sMeterRTC = objSer.MeterRTC;
        if (objSer.LastRechargeAmount != null)
            _sLastRechargeAmount = objSer.LastRechargeAmount.ToString();
        if (objSer.LastRechargeAmountDateTime != null)
            _sLastRechargeAmountDateTime = objSer.LastRechargeAmountDateTime;
        if (objSer.MeterID != null)
            _sMeter_ID = objSer.MeterID;

        dt.TableName = "Table1";
        dt.Rows.Add(_sErrorCode, _sBalance, _sMeterReading, _sMeterRTC, _sLastRechargeAmount, _sLastRechargeAmountDateTime, _sMeter_ID);


        dt.AcceptChanges();

        return dt;
    }

    [WebMethod]
    public DataSet ZBAPI_ZBI_PREPAID_MTR(string CA_NUMBER)
    {
        DataSet ds = obj.Get_ZBI_PREPAID_MTR(CA_NUMBER);
        return ds;
    }

    //[WebMethod]
    //public DataTable ZBI_WEBBILL_HIST(string CA_NUMBER)
    //{
    //    DataTable dt = new DataTable();
    //    DelhiWSV2.WebService obj = new DelhiWSV2.WebService();
    //    dt = obj.ZBI_WEBBILL_HIST(CA_NUMBER, "").Tables[0];
    //    return dt;
    //}

    [WebMethod]
    public DataTable Z_BAPI_DSS_ISU_CA_DISPLAY(string CA_NUMBER)
    {
        DataTable dt = new DataTable();
        //DelhiWSV2.WebService obj = new DelhiWSV2.WebService();
        // dt = obj.Z_BAPI_DSS_ISU_CA_DISPLAY(CA_NUMBER, "").Tables[0];
        DataSet dsBAPIOutput = obj.Get_Z_BAPI_DSS_ISU_CA_DISPLAY(CA_NUMBER,"");

        return dsBAPIOutput.Tables[0];
       // return dt;
    }


    [WebMethod]
    public DataSet Z_BAPI_DSS_ISU_CA_DISPLAY_WEBSITE(string CA_NUMBER)
    {
        DataTable dt = new DataTable();
        DataTable ISUSTDTable = new DataTable();
        DataSet Ds = new DataSet();

        DataColumn column = new DataColumn();
        ISUSTDTable.Columns.Add("Ca_Number", typeof(string));
        ISUSTDTable.Columns.Add("Bp_Number", typeof(string));
        ISUSTDTable.Columns.Add("Bp_Name", typeof(string));
        ISUSTDTable.Columns.Add("Bp_Type", typeof(string));
        ISUSTDTable.Columns.Add("Search_Term1", typeof(string));
        ISUSTDTable.Columns.Add("Search_Term2", typeof(string));
        ISUSTDTable.Columns.Add("House_Number", typeof(string));
        ISUSTDTable.Columns.Add("House_Number_Sup", typeof(string));
        ISUSTDTable.Columns.Add("Floor", typeof(string));
        ISUSTDTable.Columns.Add("Premise_Type", typeof(string));
        ISUSTDTable.Columns.Add("Street", typeof(string));
        ISUSTDTable.Columns.Add("Street2", typeof(string));
        ISUSTDTable.Columns.Add("Street3", typeof(string));
        ISUSTDTable.Columns.Add("Street4", typeof(string));
        ISUSTDTable.Columns.Add("City", typeof(string));
        ISUSTDTable.Columns.Add("Post_Code", typeof(string));
        ISUSTDTable.Columns.Add("Region", typeof(string));
        ISUSTDTable.Columns.Add("Country", typeof(string));
        ISUSTDTable.Columns.Add("Desc_Con_Object", typeof(string));

        ISUSTDTable.Columns.Add("Reg_Str_Group", typeof(string));
        ISUSTDTable.Columns.Add("Device_Sr_Number", typeof(string));
        ISUSTDTable.Columns.Add("Telephone_No", typeof(string));
        ISUSTDTable.Columns.Add("Mru", typeof(string));
        ISUSTDTable.Columns.Add("Func_Descr", typeof(string));
        ISUSTDTable.Columns.Add("Outage_Fromtime", typeof(string));
        ISUSTDTable.Columns.Add("Outage_Totime", typeof(string));
        ISUSTDTable.Columns.Add("Legacy_Acct", typeof(string));
        ISUSTDTable.Columns.Add("Bill_Class", typeof(string));
        ISUSTDTable.Columns.Add("Rate_Cat", typeof(string));
        ISUSTDTable.Columns.Add("Activity", typeof(string));
        ISUSTDTable.Columns.Add("Adr_Notes", typeof(string));
        ISUSTDTable.Columns.Add("Tel1_Number", typeof(string));
        ISUSTDTable.Columns.Add("Vertrag", typeof(string));
        ISUSTDTable.Columns.Add("E_Mail", typeof(string));
        ISUSTDTable.Columns.Add("Move_Out_Date", typeof(string));
        ISUSTDTable.Columns.Add("Con_Obj_No", typeof(string));
        ISUSTDTable.Columns.Add("Clerk_Id", typeof(string));
        ISUSTDTable.Columns.Add("Text", typeof(string));
        ISUSTDTable.Columns.Add("Status", typeof(string));
        ISUSTDTable.Columns.Add("Discreason", typeof(string));
        ISUSTDTable.Columns.Add("TARIFTYP", typeof(string));
        ISUSTDTable.Columns.Add("WERT1", typeof(string));

        ISUSTDTable.Columns.Add("Bill_Schema", typeof(string));
        ISUSTDTable.Columns.Add("Bill_Desc", typeof(string));


      //  DelhiWSV2.WebService obj = new DelhiWSV2.WebService();
        //  dt = obj.Z_BAPI_DSS_ISU_CA_DISPLAY(CA_NUMBER, "").Tables[0];
        DataSet dsBAPIOutput = obj.Get_Z_BAPI_DSS_ISU_CA_DISPLAY(CA_NUMBER, "");
        dt = dsBAPIOutput.Tables[0];

        DataRow dr;
        dr = ISUSTDTable.NewRow();

        dr["Ca_Number"] = dt.Rows[0]["Ca_Number"].ToString();
        dr["Bp_Number"] = dt.Rows[0]["Bp_Number"].ToString().Trim();
        dr["Bp_Name"] = dt.Rows[0]["Bp_Name"].ToString();
        dr["Bp_Type"] = dt.Rows[0]["Bp_Type"].ToString();
        dr["Search_Term1"] = dt.Rows[0]["Search_Term1"].ToString();
        dr["Search_Term2"] = dt.Rows[0]["Search_Term2"].ToString();
        dr["House_Number"] = dt.Rows[0]["House_Number"].ToString();
        dr["House_Number_Sup"] = dt.Rows[0]["House_Number_Sup"].ToString();
        dr["Floor"] = dt.Rows[0]["Floor"].ToString();
        dr["Premise_Type"] = dt.Rows[0]["Premise_Type"].ToString();
        dr["Street"] = dt.Rows[0]["Street"].ToString();
        dr["Street2"] = dt.Rows[0]["Street2"].ToString();
        dr["Street3"] = dt.Rows[0]["Street3"].ToString();
        dr["Street4"] = dt.Rows[0]["Street4"].ToString();
        dr["City"] = dt.Rows[0]["City"].ToString();
        dr["Post_Code"] = dt.Rows[0]["Post_Code"].ToString();
        dr["Region"] = dt.Rows[0]["Region"].ToString();
        dr["Country"] = dt.Rows[0]["Country"].ToString();
        dr["Desc_Con_Object"] = dt.Rows[0]["Desc_Con_Object"].ToString();

        dr["Reg_Str_Group"] = dt.Rows[0]["Reg_Str_Group"].ToString();
        dr["Device_Sr_Number"] = dt.Rows[0]["Device_Sr_Number"].ToString();
        dr["Telephone_No"] = dt.Rows[0]["Telephone_No"].ToString();
        dr["Mru"] = dt.Rows[0]["Mru"].ToString();
        dr["Func_Descr"] = dt.Rows[0]["Func_Descr"].ToString();
        dr["Outage_Fromtime"] = dt.Rows[0]["Outage_Fromtime"].ToString();
        dr["Outage_Totime"] = dt.Rows[0]["Outage_Totime"].ToString();
        dr["Legacy_Acct"] = dt.Rows[0]["Legacy_Acct"].ToString();
        dr["Bill_Class"] = dt.Rows[0]["Bill_Class"].ToString();
        dr["Rate_Cat"] = dt.Rows[0]["Rate_Cat"].ToString();
        dr["Activity"] = dt.Rows[0]["Activity"].ToString();
        dr["Adr_Notes"] = dt.Rows[0]["Adr_Notes"].ToString();
        dr["Tel1_Number"] = dt.Rows[0]["Tel1_Number"].ToString();
        dr["Vertrag"] = dt.Rows[0]["Vertrag"].ToString();
        dr["E_Mail"] = dt.Rows[0]["E_Mail"].ToString();
        dr["Move_Out_Date"] = dt.Rows[0]["Move_Out_Date"].ToString();
        dr["Con_Obj_No"] = dt.Rows[0]["Con_Obj_No"].ToString();
        dr["Clerk_Id"] = dt.Rows[0]["Clerk_Id"].ToString();
        dr["Text"] = dt.Rows[0]["Text"].ToString();
        dr["Status"] = dt.Rows[0]["Status"].ToString();
        dr["Discreason"] = dt.Rows[0]["Discreason"].ToString();
        dr["TARIFTYP"] = dt.Rows[0]["TARIFTYP"].ToString();
        dr["WERT1"] = dt.Rows[0]["WERT1"].ToString();

        string _sSql = "select * from mobapp.Bill_Class where BILL_CODE='" + dt.Rows[0]["Rate_Cat"].ToString() + "'";
        DataTable _dtRate = dmlgetquery_Ebsdb(_sSql);
        if (_dtRate.Rows.Count > 0)
        {
            dr["Bill_Schema"] = _dtRate.Rows[0]["BILL_SCHEMA"].ToString();
            dr["Bill_Desc"] = _dtRate.Rows[0]["BILL_DESC"].ToString();
        }
        else
        {
            dr["Bill_Schema"] = "";
            dr["Bill_Desc"] = "";
        }

        ISUSTDTable.Rows.Add(dr);
        ISUSTDTable.AcceptChanges();

        Ds.Tables.Add(ISUSTDTable);
        Ds.Tables[0].TableName = "ISUSTDTable";
        Ds.DataSetName = "BAPI_RESULT";

        return Ds;
    }
    [WebMethod]
    public DataSet ZBAPI_ONLINE_BILL_PDF(string strCANumber, string strEBSKNO)
    {
        DataSet Ds = obj.Get_ZBAPI_ONLINE_BILL_PDF(strCANumber, strEBSKNO);
        return Ds;
    }

    [WebMethod]
    public DataSet ZBAPI_DISPLAY_BILL_WEB_SAP(string strCANumber, string strBillMonth)
    {
        DataSet Ds = obj.Get_ZBAPI_DISPLAY_BILL_WEB(strCANumber, strBillMonth);
        return Ds;
    }

    [WebMethod]
    public DataSet ZBI_BAPI_SOLAR1(string CA_NUMBER, string BILL_MONTH)//Added By Babalu Kumar on 20092021
    {
        DataSet Ds = obj.Get_ZBI_BAPI_SOLAR1(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER, BILL_MONTH);
        return Ds;
    }

    #region SAJID FOR RECORD MANAGEMENT SYSTEM 09092021
    [WebMethod]
    public DataSet ZBAPI_CS_CRN_CHECK(string CRNNO, string KNO, string orderno)
    {
        DataSet Ds = obj.ZBAPI_CS_CRN_CHECK(CRNNO, KNO, orderno);
        //_objLog.LogMessageToFile(strCANumber + "||" + strSerialNumber + "||" + strConsumerNumber + "||" + strTelephoneNumber + "||" + strKNumber + "||" + strContractNumber, Ds);
        return Ds;
    }
    #endregion


    [WebMethod]
    public DataSet ZBAPI_CS_BP_DETAIL_SAP(string strCompName, string strDate, string strOrderType)
    {
        DataSet Ds = obj.Get_ZBAPI_CS_BP_DETAIL(strCompName, strDate, strOrderType);
        return Ds;
    }

    [WebMethod]
    public DataSet ZBAPI_PAY_IN_SLIP_SAP(string CompName, string CA_NUMBER)
    {
        DataSet dsCAInfo = new DataSet();
        DataTable dt = new DataTable();

        DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
        // DelhiV2Dev.WebService obj1 = new DelhiV2Dev.WebService();
        dsCAInfo = new DataSet();
        dsCAInfo = obj1.ZBAPI_PAY_IN_SLIP(CompName, CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER);
        if (dsCAInfo.Tables[0].Rows.Count > 0)
        {
            dt = new DataTable();
            dt = dsCAInfo.Tables[0];
            if (dt.Rows.Count > 0)
            {

            }
        }

        return dsCAInfo;
    }

    [WebMethod]
    public DataTable GET_DSKORDER_STATUS(string _sMobileNo)
    {
        DataSet dsOrdInfo = new DataSet();
        DataTable OrdInfodt = new DataTable();
        DataTable dt = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("Sno", typeof(string));
        dt.Columns.Add("OrderNo", typeof(string));
        dt.Columns.Add("MobileNo", typeof(string));
        dt.Columns.Add("OrderDate", typeof(string));
        dt.Columns.Add("Ilart", typeof(string));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("OrderDesc", typeof(string));
        dt.Columns.Add("OrderType", typeof(string));
        dt.Columns.Add("Source", typeof(string));
        dt.Columns.Add("DefLink", typeof(string));
        dt.TableName = "Table1";

        string _sOrderNo = string.Empty, _sOrdStatus = string.Empty;
        string _sOrderDesc = string.Empty, _sOrdType = string.Empty, _sDefLink = string.Empty;

        OrdInfodt = GetOrder_DSKMobileInfo(_sMobileNo);

        if (OrdInfodt.Rows.Count > 0)
        {
            DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
            dsOrdInfo = new DataSet();

            for (int i = 0; i < OrdInfodt.Rows.Count; i++)
            {
                _sOrderNo = OrdInfodt.Rows[i]["aufnr"].ToString();
                dsOrdInfo = obj1.ZBAPI_CS_ORD_STAT(_sOrderNo.Length == 10 ? "00" + _sOrderNo : _sOrderNo);

                if (dsOrdInfo.Tables[0].Rows.Count > 0)
                {
                    if (dsOrdInfo.Tables[0].Rows.Count > 0)
                    {
                        //<Status>BWNE</Status>

                        if (OrdInfodt.Rows[i]["Def_Link"].ToString().Trim() != "")
                        {
                            _sOrdStatus = "Document Deficient";
                        }
                        else
                        {
                            _sOrdStatus = Convert.ToString(dsOrdInfo.Tables[0].Rows[0]["ORDER_STATUS"]);
                        }

                        _sOrderDesc = Convert.ToString(dsOrdInfo.Tables[0].Rows[0]["ORDER_DESC"]);
                        _sOrdType = Convert.ToString(dsOrdInfo.Tables[0].Rows[0]["ORDER_TYPE"]);

                        if (_sOrdStatus.Trim().ToUpper() == "NEW")
                        {
                            _sOrdStatus = "Request Received";
                        }
                        else if ((_sOrdStatus.Trim().ToUpper() == "ICFP") || (_sOrdStatus.Trim().ToUpper() == "TALR") || (_sOrdStatus.Trim().ToUpper() == "SDCR")
                            || (_sOrdStatus.Trim().ToUpper() == "SOCR") || (_sOrdStatus.Trim().ToUpper() == "SCRT") || (_sOrdStatus.Trim().ToUpper() == "ARSH")
                            || (_sOrdStatus.Trim().ToUpper() == "BTFR"))
                        {
                            _sOrdStatus = "In Progress";
                        }
                        else if ((_sOrdStatus.Trim().ToUpper() == "BWEX"))
                        {
                            _sOrdStatus = "Completed";
                        }
                        else if ((_sOrdStatus.Trim().ToUpper() == "DOCI"))
                        {
                            _sOrdStatus = "Document Deficient";
                        }
                        else if ((_sOrdStatus.Trim().ToUpper() == "BWNE"))
                        {
                            _sOrdStatus = "Order Cancelled";
                        }

                        if (_sOrdStatus.Trim() == "Document Deficient")
                        {
                            dt.Rows.Add((i + 1).ToString(), _sOrderNo, _sMobileNo, OrdInfodt.Rows[i]["ERDAT"].ToString(), OrdInfodt.Rows[i]["ilart"].ToString(),
                                OrdInfodt.Rows[i]["NAME"].ToString(), _sOrdStatus, _sOrderDesc, _sOrdType, OrdInfodt.Rows[i]["REG_SOURCE"].ToString(), OrdInfodt.Rows[i]["Def_Link"].ToString());
                        }
                        else
                        {
                            dt.Rows.Add((i + 1).ToString(), _sOrderNo, _sMobileNo, OrdInfodt.Rows[i]["ERDAT"].ToString(), OrdInfodt.Rows[i]["ilart"].ToString(),
                                OrdInfodt.Rows[i]["NAME"].ToString(), _sOrdStatus, _sOrderDesc, _sOrdType, OrdInfodt.Rows[i]["REG_SOURCE"].ToString(), "");
                        }
                    }
                }
            }
        }

        dt.AcceptChanges();
        return dt;
    }

    public DataTable GetOrder_DSKMobileInfo(string _sMobileNo)
    {
        DataTable _dt = new DataTable();
        string _sSql = "select aufnr, ilart,REG_SOURCE,(substr(ERDAT,9,2)||'-' ||substr(ERDAT,6,3) || substr(ERDAT,0,4))ERDAT, TITLE || ' ' || NAME2 || ' ' || NAMEMIDDLE || ' ' || NAME1 NAME,(case when WEBSITE_STATUS='DR' then 'https://bsesbrpl.co.in:7877/dsk_web/index.aspx?OD='||aufnr else'' end)Def_Link,entry_dt from mobapp.sap_sevakendra where ilart='U01'  AND TEL_NUMBER='" + _sMobileNo + "'  order by entry_dt desc ";

        _dt = dmlMobApp_getquery(_sSql);

        return _dt;
    }

    public DataTable dmlMobApp_getquery(string sql)
    {
        DataTable dt = new DataTable();
        OleDbCommand dbcommand;
        NDS objNDS = new NDS();
        OleDbDataAdapter da;

        OleDbConnection ocon = new OleDbConnection(objNDS.con());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand = new OleDbCommand();
            dbcommand.Connection = ocon;
            da = new OleDbDataAdapter();
            da.SelectCommand = dbcommand;
            dt = null;
            dt = new DataTable();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandText = sql;
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return dt;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }


    #region "N_Series_DSK"

    private int CheckRequest_LoadData(string _sCaNo, string _sType, int _iAppliedLoad, int _iAppliedLoadKVA)
    {
        int _iResult = 0;
        DataTable _dt = new DataTable();
        int _iSAN_LOADKW = 0, _iSAN_LOAD_PROPOSEDKW = 0;
        int _iSAN_LOAD_KVA = 0, _iSAN_LOAD_PROPOSED_KVA = 0;
        string _sSql = "select REQUESTTYPE,LOAD_TYPE,SAN_LOAD,SAN_LOAD_PROPOSED,SAN_LOAD_KVA,SAN_LOAD_PROPOSED_KVA from mobapp.CRM_MDI_CA  WHERE CA_NO='" + _sCaNo.Trim() + "' AND REQUESTTYPE='" + _sType.Trim() + "'";
        _dt = dmlMobApp_getquery(_sSql);
        if (_dt.Rows.Count > 0)
        {
            if (_dt.Rows[0]["SAN_LOAD"].ToString() != "")
            {
                try
                {
                    _iSAN_LOADKW = Convert.ToInt32(_dt.Rows[0]["SAN_LOAD"]);
                }
                catch
                {
                    _iSAN_LOADKW = 0;
                }
            }
            if (_dt.Rows[0]["SAN_LOAD_PROPOSED"].ToString() != "")
            {
                try
                {
                    _iSAN_LOAD_PROPOSEDKW = Convert.ToInt32(_dt.Rows[0]["SAN_LOAD_PROPOSED"]);
                }
                catch
                {
                    _iSAN_LOAD_PROPOSEDKW = 0;
                }
            }
            if (_dt.Rows[0]["SAN_LOAD_KVA"].ToString() != "")
            {
                try
                {
                    _iSAN_LOAD_KVA = Convert.ToInt32(_dt.Rows[0]["SAN_LOAD_KVA"]);
                }
                catch
                {
                    _iSAN_LOAD_KVA = 0;
                }
            }
            if (_dt.Rows[0]["SAN_LOAD_PROPOSED_KVA"].ToString() != "")
            {
                try
                {
                    _iSAN_LOAD_PROPOSED_KVA = Convert.ToInt32(_dt.Rows[0]["SAN_LOAD_PROPOSED_KVA"]);
                }
                catch
                {
                    _iSAN_LOAD_PROPOSED_KVA = 0;
                }
            }

            if (_iAppliedLoad > 0)
            {
                if (_iAppliedLoad >= _iSAN_LOAD_PROPOSEDKW)
                    _iResult = 0;
                else
                    _iResult = _iSAN_LOAD_PROPOSEDKW;
            }
            else
            {
                if (_iAppliedLoadKVA >= _iSAN_LOAD_PROPOSED_KVA)
                    _iResult = 0;
                else
                    _iResult = _iSAN_LOAD_PROPOSED_KVA;
            }
        }
        else
        {
            _iResult = 0;
        }

        return _iResult;
    }

    [WebMethod]
    public DataSet Z_BAPI_ZDSS_WEB_LINK_TEST(string I_ILART, string I_VKONT, string I_VKONA, string PARTNERCATEGORY, string PARTNERTYPE,
                        string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO,
                        string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, string CITY,
                        string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string IDTYPE,
                        string IDNUMBER, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string APPLIEDCAT, string APPLIEDLOAD,
                        string APPLIEDLOADKVA, string CONNECTIONTYPE, string STATEMENT_CA, string START_DATE, string START_TIME,
                        string FINISH_DATE, string FINISH_TIME, string SORTFIELD, string ABKRS, string LATITUDE, string LONGITUDE,
                        string GEOCOR_ADDRESS, string APPOINT_DIV)
    {
        DataSet dsOrdInfo = new DataSet();

        DataTable outputFlagsTable = new DataTable();
        DataTable SAPDATA_ErrorDataTable = new DataTable();
        DataTable ErrorTable = new DataTable();
        DataTable messageTable = new DataTable();
        DataTable dt = new DataTable();

        StringBuilder sql = new StringBuilder();
        string _sSql = string.Empty, TITLE = string.Empty, SEX = string.Empty, _sNReqNo = string.Empty, _sAppointDate = string.Empty;
        string _sSysDate = string.Empty, _sAppType = string.Empty, _sStartDate = string.Empty, _sEndDate = string.Empty;

        if (I_ILART.Trim() == "")
            I_ILART = ".";
        if (I_VKONT.Trim() == "")
            I_VKONT = ".";
        if (I_VKONA.Trim() == "")
            I_VKONA = ".";
        if (FIRSTNAME.Trim() == "")
            FIRSTNAME = ".";
        if (LASTNAME.Trim() == "")
            LASTNAME = ".";
        if (MIDDLENAME.Trim() == "")
            MIDDLENAME = ".";
        if (FATHERSNAME.Trim() == "")
            FATHERSNAME = ".";
        if (HOUSE_NO.Trim() == "")
            HOUSE_NO = ".";
        if (BUILDING.Trim() == "")
            BUILDING = ".";
        if (STR_SUPPL1.Trim() == "")
            STR_SUPPL1 = ".";
        if (STR_SUPPL2.Trim() == "")
            STR_SUPPL2 = ".";
        if (STR_SUPPL3.Trim() == "")
            STR_SUPPL3 = ".";
        if (CITY.Trim() == "")
            CITY = ".";
        if (FEMALE.Trim() == "")
            FEMALE = ".";
        if (MALE.Trim() == "")
            MALE = ".";
        if (JOBGR.Trim() == "")
            JOBGR = ".";
        if (IDTYPE.Trim() == "")
            IDTYPE = ".";
        if (IDNUMBER.Trim() == "")
            IDNUMBER = ".";
        if (WORKCENTRE.Trim() == "")
            WORKCENTRE = ".";
        if (SYSTEMCOND.Trim() == "")
            SYSTEMCOND = ".";
        if (STATEMENT_CA.Trim() == "")
            STATEMENT_CA = ".";
        if (START_DATE.Trim() == "")
            START_DATE = ".";
        if (START_TIME.Trim() == "")
            START_TIME = ".";
        if (FINISH_DATE.Trim() == "")
            FINISH_DATE = ".";
        if (FINISH_TIME.Trim() == "")
            FINISH_TIME = ".";
        if (SORTFIELD.Trim() == "")
            SORTFIELD = ".";
        if (ABKRS.Trim() == "")
            ABKRS = ".";
        if (GEOCOR_ADDRESS.Trim() == "")
            GEOCOR_ADDRESS = ".";

        DataColumn column = new DataColumn();
        outputFlagsTable.Columns.Add("E_Flag_Ap", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_Bp", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_So", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_Us", typeof(string));
        outputFlagsTable.Columns.Add("E_New_Partner", typeof(string));
        outputFlagsTable.Columns.Add("E_Service_Order", typeof(string));

        SAPDATA_ErrorDataTable.Columns.Add("Type", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Id", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Number", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Log_No", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Log_Msg_No", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V1", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V2", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V3", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V4", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Parameter", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Row", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Field", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("System", typeof(string));

        ErrorTable.Columns.Add("Data", typeof(string));

        messageTable.Columns.Add("messageCode", typeof(string));
        messageTable.Columns.Add("messageText", typeof(string));

        try
        {
            if (TITLE_KEY.Trim() == "0001")
                TITLE = "Ms";
            else if (TITLE_KEY.Trim() == "0002")
                TITLE = "Mr";
            else if (TITLE_KEY.Trim() == "0003")
                TITLE = "Dr";
            else if (TITLE_KEY.Trim() == "0004")
                TITLE = "Prof";
            else if (TITLE_KEY.Trim() == "0006")
                TITLE = "M/S";
            else
                TITLE = "Ms";

            if (MALE.Trim().ToUpper() == "X" || FEMALE.Trim().ToUpper() == "X")
                SEX = "X";

            if (START_DATE.Trim().Length == 8)
                _sStartDate = START_DATE.Substring(0, 4) + "-" + START_DATE.Substring(4, 2) + "-" + START_DATE.Substring(6, 2);
            if (FINISH_DATE.Trim().Length == 8)
                _sEndDate = FINISH_DATE.Substring(0, 4) + "-" + FINISH_DATE.Substring(4, 2) + "-" + FINISH_DATE.Substring(6, 2);

            if (START_DATE.Trim().Length == 8)
                _sAppointDate = START_DATE.Substring(6, 2) + "/" + START_DATE.Substring(4, 2) + "/" + START_DATE.Substring(0, 4);

            _sSql = "SELECT TO_CHAR(SYSDATE,'DDMMYY')|| LPAD(COUNT(1)+1,4,'0') FROM mobapp.sap_sevakendra WHERE SUBSTR(REQUEST_NO,6,6)=TO_CHAR(SYSDATE,'DDMMYY')";

            dt = dmlgetquery_Ebsdb(_sSql);
            // dt = dmlgetquery(_sSql);

            string _sReqID = "";
            if (dt.Rows.Count > 0)
            {
                if (I_ILART.Trim() == "U01")
                    _sReqID = "N";
                else if (I_ILART.Trim() == "U02")
                    _sReqID = "O";
                else if (I_ILART.Trim() == "U03")
                    _sReqID = "L";
                else if (I_ILART.Trim() == "U04")
                    _sReqID = "L";
                else if (I_ILART.Trim() == "U05")
                    _sReqID = "C";
                else if (I_ILART.Trim() == "U06")
                    _sReqID = "C";
                else if (I_ILART.Trim() == "U07")
                    _sReqID = "A";

                _sSysDate = System.DateTime.Now.ToString("yyyyMMdd");

                if ((I_ILART.Trim() != "U01") && ABKRS.Trim() == "03")
                {
                    WORKCENTRE = GetDivisionCode(WORKCENTRE.Trim().ToUpper());
                }

                if (APPOINT_DIV.Trim() == "")
                    APPOINT_DIV = WORKCENTRE;

                int _iLoadCheck = 0;
                if (APPLIEDLOAD.Trim() == "")
                    APPLIEDLOAD = "0";
                if (APPLIEDLOADKVA.Trim() == "")
                    APPLIEDLOADKVA = "0";

                if (I_ILART.Trim() == "U03")
                {
                    _iLoadCheck = CheckRequest_LoadData(I_VKONT, "E", Convert.ToInt32(APPLIEDLOAD.Trim()), Convert.ToInt32(APPLIEDLOADKVA.Trim()));
                }
                else if (I_ILART.Trim() == "U04")
                {
                    _iLoadCheck = CheckRequest_LoadData(I_VKONT, "R", Convert.ToInt32(APPLIEDLOAD.Trim()), Convert.ToInt32(APPLIEDLOADKVA.Trim()));
                }

                if (APPLIEDLOAD.Trim() == "")
                {
                    if (APPLIEDLOADKVA.Trim() != "")
                    {
                        APPLIEDLOAD = APPLIEDLOADKVA.Trim();
                    }
                }

                if ((I_ILART.Trim() == "U01" || I_ILART.Trim() == "U03" || I_ILART.Trim() == "U04") && (APPLIEDLOAD.Trim() == "" || APPLIEDLOAD.Trim() == "0"))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Please Enter Applied Load!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (_iLoadCheck != 0)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Applied Load should not be Less than " + _iLoadCheck, "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (MOBILE.Trim().Length != 10)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Please Enter Correct Mobile Number!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (((WORKCENTRE.Trim() == "S1NFC") || (WORKCENTRE.Trim() == "W1JFR") || (WORKCENTRE.Trim() == "W1NJF") ||
                    (WORKCENTRE.Trim() == "S1SVR") || (WORKCENTRE.Trim() == "W2VKP")) && (APPOINT_DIV.Trim() != WORKCENTRE.Trim()))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Not Allow to Take Request for Other Division!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (WORKCENTRE.Trim() != "")
                {
                    if (_sSysDate.Trim() == START_DATE.Trim())
                    {
                        _sAppType = "O" + _sReqID + WORKCENTRE.Trim().ToUpper().Substring(2, 3);
                    }
                    else
                    {
                        _sAppType = "A" + _sReqID + WORKCENTRE.Trim().ToUpper().Substring(2, 3);
                    }

                    _sNReqNo = _sAppType + dt.Rows[0][0].ToString();

                    DataTable _dtConsumer = new DataTable();
                    string _sName = string.Empty, NAME_FIRST = string.Empty, NAME_LAST = string.Empty, EXISTING_LOAD = string.Empty, TITLE1 = string.Empty;

                    if (I_ILART.Trim() != "U01")
                    {
                        _dtConsumer = GetSAP_Data(I_VKONT.Trim().Replace("'", "''"));
                        if (_dtConsumer.Rows.Count > 0)
                        {
                            _sName = Convert.ToString(_dtConsumer.Rows[0]["Bp_Name"].ToString().Trim());
                            TITLE1 = Convert.ToString(_sName.Split(' ')[0].ToString());

                            if (I_ILART.Trim() == "U02")
                            {
                                NAME_FIRST = Convert.ToString(_sName.Substring(TITLE1.Length, _sName.Length - TITLE1.Length)).Trim();
                                //NAME_FIRST = _sName;
                                NAME_LAST = ".";
                            }
                            else
                            {
                                NAME_FIRST = Convert.ToString(_sName.Substring(TITLE1.Length, _sName.Length - TITLE1.Length)).Trim();
                                //NAME_FIRST = _sName;
                                NAME_LAST = ".";
                                FIRSTNAME = Convert.ToString(_sName.Substring(TITLE1.Length, _sName.Length - TITLE1.Length)).Trim();
                                //FIRSTNAME = _sName;
                                LASTNAME = ".";
                            }

                            MOBILE = Convert.ToString(_dtConsumer.Rows[0]["Tel1_Number"]);
                            if (I_ILART.Trim() == "U07")
                            {

                            }
                            else
                            {
                                POSTL_COD1 = Convert.ToString(_dtConsumer.Rows[0]["Post_Code"]);
                                BUILDING = Convert.ToString(_dtConsumer.Rows[0]["Street"]);
                                HOUSE_NO = Convert.ToString(_dtConsumer.Rows[0]["House_Number"]);
                                STR_SUPPL1 = Convert.ToString(_dtConsumer.Rows[0]["Street2"]);
                                STR_SUPPL2 = Convert.ToString(_dtConsumer.Rows[0]["Street3"]);
                                STR_SUPPL3 = Convert.ToString(_dtConsumer.Rows[0]["Street4"]);
                            }

                            EXISTING_LOAD = Convert.ToString(_dtConsumer.Rows[0]["WERT1"]);
                        }
                    }
                    else
                    {
                        NAME_FIRST = FIRSTNAME.Trim().ToUpper().Replace("'", "''");
                        NAME_LAST = LASTNAME.Trim().ToUpper().Replace("'", "''");
                    }



                    sql.Append("insert into mobapp.sap_sevakendra(REQUEST_NO,AUFNR,ILART,ZZ_VKONT,AUART,TITLE,NAME_FIRST,NAME_LAST,NAME2,NAME1,NAMEMIDDLE,SECONDNAME,");
                    sql.Append(" XSEXU,HOUSE_NUM1,STREET,STR_SUPPL1,STR_SUPPL2,STR_SUPPL3,POST_CODE1,CITY_CODE,E_MAIL,TEL_NUMBER,TEL_EXTENS,ERDAT,BUKRS,VAPLZ, ");
                    sql.Append(" GSTRP,GSUZP,STRMN,STRUR,LTRMN,LTRUR, BPKIND,RUN_FOR_DATE,RUN_ON_DATE,FECOD,ZZ_RLOAD,ZZ_RLOADKVA,ZZ_CONNTYPE,");
                    sql.Append(" ENTRY_DT,FLAG,ACCOUNT_CLASS,REG_SOURCE,SAP_CREATED_BY,PLANNER_GROUP,ENTER_FRM_SRC, LANDLINE,I_VKONA, ");
                    sql.Append(" PARTNERCATEGORY,PARTNERTYPE,JOBGR,IDTYPE,IDNUMBER,PLANNINGPLANT,SYSTEMCOND,LATITUDE,LONGITUDE,GEOCOR_ADDRESS,APPOINT_DIV,EXISTING_LOAD,EXISTING_LOADKVA,MALE,FEMALE) ");
                    sql.Append(" VALUES ");

                    sql.Append(" ('" + _sNReqNo + "','" + _sNReqNo + "' ,'" + I_ILART.Trim().Replace("'", "''") + "', '" + I_VKONT.Trim().Replace("'", "''") + "','ZDSS','" + TITLE + "','" + FIRSTNAME.Trim().ToUpper().Replace("'", "''") + "','" + LASTNAME.Trim().ToUpper().Replace("'", "''") + "',");
                    sql.Append(" '" + NAME_FIRST.Trim().ToUpper().Replace("'", "''") + "','" + NAME_LAST.Trim().ToUpper().Replace("'", "''") + "','" + MIDDLENAME.Trim().ToUpper().Replace("'", "''") + "','" + FATHERSNAME.Trim().ToUpper().Replace("'", "''") + "',");
                    sql.Append(" '" + SEX + "','" + HOUSE_NO.Trim().ToUpper().Replace("'", "''") + "','" + BUILDING.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL1.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL2.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL3.Trim().ToUpper().Replace("'", "''") + "',");
                    sql.Append("  '" + POSTL_COD1.Replace("'", "''") + "','" + CITY.Replace("'", "''") + "', '" + E_MAIL.Trim().Replace("'", "''") + "','" + MOBILE.Trim().Replace("'", "''") + "','SMS',to_char(sysdate,'yyyy-MM-dd'),'BRPL','" + WORKCENTRE.Trim().ToUpper().Replace("'", "''") + "', ");
                    sql.Append("  to_char(sysdate,'yyyy-MM-dd'),'" + START_TIME.Trim().Replace("'", "''") + "','" + _sStartDate.Trim().Replace("'", "''") + "','" + START_TIME.Trim().Replace("'", "''") + "','" + _sEndDate.Trim().Replace("'", "''") + "','" + FINISH_TIME.Trim().Replace("'", "''") + "',");
                    sql.Append(" '0010',to_char(sysdate,'dd.MM.yyyy'),to_char(sysdate,'dd.MM.yyyy'),'" + APPLIEDCAT.Trim().Replace("'", "''") + "','" + APPLIEDLOAD.Trim().Replace("'", "''") + "','" + APPLIEDLOADKVA.Trim().Replace("'", "''") + "',");
                    sql.Append("  '" + CONNECTIONTYPE.Trim() + "',sysdate, '0','" + STATEMENT_CA.Replace("'", "''") + "','" + ABKRS.Trim().Replace("'", "''") + "', ");
                    sql.Append(" 'DSK_NSER', 'DSS','N','" + LANDLINE.Trim().Replace("'", "''") + "','" + I_VKONA.Trim().Replace("'", "''") + "','" + PARTNERCATEGORY.Trim().Replace("'", "''") + "' ,");
                    sql.Append(" '" + PARTNERTYPE.Trim().Replace("'", "''") + "','" + JOBGR.Trim().Replace("'", "''") + "','" + IDTYPE.Trim().Replace("'", "''") + "', ");
                    sql.Append("  '" + IDNUMBER.Trim().Replace("'", "''") + "','" + PLANNINGPLANT.Trim().Replace("'", "''") + "','" + SYSTEMCOND.Trim().Replace("'", "''") + "',");
                    sql.Append("  '" + LONGITUDE.Trim() + "','" + LATITUDE.Trim() + "','" + GEOCOR_ADDRESS.Trim().Replace("'", "''") + "','" + APPOINT_DIV.Trim() + "','" + EXISTING_LOAD.Trim() + "','" + EXISTING_LOAD.Trim() + "','" + MALE + "','" + FEMALE + "') ");

                    if (dmlsinglequery(sql.ToString()) == true)
                    {
                        DataRow dr;
                        dr = outputFlagsTable.NewRow();
                        dr["E_Flag_Ap"] = "";
                        dr["E_Flag_Bp"] = "";
                        dr["E_Flag_So"] = "";
                        dr["E_Flag_Us"] = "";
                        dr["E_New_Partner"] = "";
                        dr["E_Service_Order"] = _sNReqNo;
                        outputFlagsTable.Rows.Add(dr);
                        outputFlagsTable.AcceptChanges();
                    }

                    if (APPOINT_DIV.Trim().ToString() != "")
                    {
                        _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                        _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + APPOINT_DIV + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                    }
                    else
                    {
                        _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                        _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + WORKCENTRE + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                    }

                    dmlsinglequery(_sSql.ToString());

                    //SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "For appointment related queries, please contact our customer care no. 19122 / 19123 .", "", "000000", "", "", "", "", "", "0", "", "");
                    //SAPDATA_ErrorDataTable.AcceptChanges();
                }
            }
        }
        catch (Exception ex)
        {
            SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", ex.Message.ToString(), "", "000000", "", "", "", "", "", "0", "", "");
            SAPDATA_ErrorDataTable.AcceptChanges();
        }
        outputFlagsTable.TableName = "outputFlagsTable";
        SAPDATA_ErrorDataTable.TableName = "SAPDATA_ErrorDataTable";
        ErrorTable.TableName = "ErrorTable";
        messageTable.TableName = "messageTable";

        dsOrdInfo.Tables.Add(outputFlagsTable);
        dsOrdInfo.Tables.Add(SAPDATA_ErrorDataTable);
        dsOrdInfo.Tables.Add(ErrorTable);
        dsOrdInfo.Tables.Add(messageTable);

        outputFlagsTable.AcceptChanges();
        SAPDATA_ErrorDataTable.AcceptChanges();
        ErrorTable.AcceptChanges();
        messageTable.AcceptChanges();
        dsOrdInfo.DataSetName = "BAPI_RESULT";

        return dsOrdInfo;
    }

    [WebMethod]
    public DataSet Z_BAPI_ZDSS_WEB_LINK(string I_ILART, string I_VKONT, string I_VKONA, string PARTNERCATEGORY, string PARTNERTYPE,
                        string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO,
                        string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, string CITY,
                        string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string IDTYPE,
                        string IDNUMBER, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string APPLIEDCAT, string APPLIEDLOAD,
                        string APPLIEDLOADKVA, string CONNECTIONTYPE, string STATEMENT_CA, string START_DATE, string START_TIME,
                        string FINISH_DATE, string FINISH_TIME, string SORTFIELD, string ABKRS, string LATITUDE, string LONGITUDE,
                        string GEOCOR_ADDRESS, string APPOINT_DIV)
    {
        DataSet dsOrdInfo = new DataSet();

        DataTable outputFlagsTable = new DataTable();
        DataTable SAPDATA_ErrorDataTable = new DataTable();
        DataTable ErrorTable = new DataTable();
        DataTable messageTable = new DataTable();
        DataTable dt = new DataTable();
        DataTable DTHoliday = new DataTable();

        StringBuilder sql = new StringBuilder();
        string _sSql = string.Empty, TITLE = string.Empty, SEX = string.Empty, _sNReqNo = string.Empty, _sAppointDate = string.Empty;
        string _sSysDate = string.Empty, _sAppType = string.Empty, _sStartDate = string.Empty, _sEndDate = string.Empty, Bp_type = string.Empty;


        double E_O_AMT = 0;
        int NewConnCount = 0, NewMailCount = 0, Holiday = 0;
        DataSet dsBAPIOutput = new DataSet();

        if (I_ILART.Trim() == "")
            I_ILART = ".";
        if (I_VKONT.Trim() == "")
            I_VKONT = ".";
        if (I_VKONA.Trim() == "")
            I_VKONA = ".";
        if (FIRSTNAME.Trim() == "")
            FIRSTNAME = ".";
        if (LASTNAME.Trim() == "")
            LASTNAME = ".";
        if (MIDDLENAME.Trim() == "")
            MIDDLENAME = ".";
        if (FATHERSNAME.Trim() == "")
            FATHERSNAME = ".";
        if (HOUSE_NO.Trim() == "")
            HOUSE_NO = ".";
        if (BUILDING.Trim() == "")
            BUILDING = ".";
        if (STR_SUPPL1.Trim() == "")
            STR_SUPPL1 = ".";
        if (STR_SUPPL2.Trim() == "")
            STR_SUPPL2 = ".";
        if (STR_SUPPL3.Trim() == "")
            STR_SUPPL3 = ".";
        if (CITY.Trim() == "")
            CITY = ".";
        if (FEMALE.Trim() == "")
            FEMALE = ".";
        if (MALE.Trim() == "")
            MALE = ".";
        if (JOBGR.Trim() == "")
            JOBGR = ".";
        if (IDTYPE.Trim() == "")
            IDTYPE = ".";
        if (IDNUMBER.Trim() == "")
            IDNUMBER = ".";
        if (WORKCENTRE.Trim() == "")
            WORKCENTRE = ".";
        if (SYSTEMCOND.Trim() == "")
            SYSTEMCOND = ".";
        if (STATEMENT_CA.Trim() == "")
            STATEMENT_CA = ".";
        if (START_DATE.Trim() == "")
            START_DATE = ".";
        if (START_TIME.Trim() == "")
            START_TIME = ".";
        if (FINISH_DATE.Trim() == "")
            FINISH_DATE = ".";
        if (FINISH_TIME.Trim() == "")
            FINISH_TIME = ".";
        if (SORTFIELD.Trim() == "")
            SORTFIELD = ".";
        if (ABKRS.Trim() == "")
            ABKRS = ".";
        if (GEOCOR_ADDRESS.Trim() == "")
            GEOCOR_ADDRESS = ".";

        DataColumn column = new DataColumn();
        outputFlagsTable.Columns.Add("E_Flag_Ap", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_Bp", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_So", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_Us", typeof(string));
        outputFlagsTable.Columns.Add("E_New_Partner", typeof(string));
        outputFlagsTable.Columns.Add("E_Service_Order", typeof(string));

        SAPDATA_ErrorDataTable.Columns.Add("Type", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Id", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Number", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Log_No", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Log_Msg_No", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V1", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V2", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V3", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V4", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Parameter", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Row", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Field", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("System", typeof(string));

        ErrorTable.Columns.Add("Data", typeof(string));

        messageTable.Columns.Add("messageCode", typeof(string));
        messageTable.Columns.Add("messageText", typeof(string));

        try
        {
            if (TITLE_KEY.Trim() == "0001")
                TITLE = "Ms";
            else if (TITLE_KEY.Trim() == "0002")
                TITLE = "Mr";
            else if (TITLE_KEY.Trim() == "0003")
                TITLE = "Dr";
            else if (TITLE_KEY.Trim() == "0004")
                TITLE = "Prof";
            else if (TITLE_KEY.Trim() == "0006")
                TITLE = "M/S";
            else
                TITLE = "Ms";

            if (MALE.Trim().ToUpper() == "X" || FEMALE.Trim().ToUpper() == "X")
                SEX = "X";

            if (START_DATE.Trim().Length == 8)
                _sStartDate = START_DATE.Substring(0, 4) + "-" + START_DATE.Substring(4, 2) + "-" + START_DATE.Substring(6, 2);
            if (FINISH_DATE.Trim().Length == 8)
                _sEndDate = FINISH_DATE.Substring(0, 4) + "-" + FINISH_DATE.Substring(4, 2) + "-" + FINISH_DATE.Substring(6, 2);

            if (START_DATE.Trim().Length == 8)
                _sAppointDate = START_DATE.Substring(6, 2) + "/" + START_DATE.Substring(4, 2) + "/" + START_DATE.Substring(0, 4);

            //_sSql = "SELECT TO_CHAR(SYSDATE,'DDMMYY')|| LPAD(COUNT(1)+1,4,'0') FROM mobapp.sap_sevakendra WHERE SUBSTR(REQUEST_NO,6,6)=TO_CHAR(SYSDATE,'DDMMYY')"; // commented by ajay 29.07.2022
            _sSql = "select to_char(sysdate,'DDMMYY')||lpad(MOBAPP.AUART_Seq.nextval,4,'0') from dual";



            dt = dmlgetquery_Ebsdb(_sSql);
            // dt = dmlgetquery(_sSql);


            _sSql = ""; // added by ajay
            _sSql = "select *  from mobapp.DSS_HOLIDAY_MST where (TO_CHAR (h_date, 'DD-MM-YYYY'))='" + _sStartDate + "'";
            DTHoliday = dmlgetquery_Ebsdb(_sSql);
            if (DTHoliday.Rows.Count > 0)
            {
                Holiday = 1;
            }

            DataTable dtBP_Type = new DataTable();
            if (I_ILART.Trim() == "U02")  // added by ajay 30.06.2022
            {
                dtBP_Type = GetSAP_Data(I_VKONT.Trim().Replace("'", "''"));
                if (dtBP_Type.Rows.Count > 0)
                {
                    Bp_type = dtBP_Type.Rows[0]["Bp_Type"].ToString();
                }
            }


            if (I_ILART.Trim() == "U02")  // added by ajay 30.06.2022
            {
                dsBAPIOutput = obj.get_ZFI_CURR_OUTS_FLAG(I_VKONT);
                if (dsBAPIOutput.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(dsBAPIOutput.Tables[0].Rows[0]["E_O_AMT"]) != "")
                    {
                        E_O_AMT = Convert.ToDouble(dsBAPIOutput.Tables[0].Rows[0]["E_O_AMT"]);
                    }
                }
            }

            NewConnCount = Convert.ToInt32(ZBAPI_WEBMobileCNT(MOBILE, "", "30", "").Tables[0].Rows[0][0]);
            NewMailCount = Convert.ToInt32(ZBAPI_WEBEmailCNT(E_MAIL, "", "30", "").Tables[0].Rows[0][0]);

            string _sReqID = "";
            if (dt.Rows.Count > 0)
            {
                if (I_ILART.Trim() == "U01")
                    _sReqID = "N";
                else if (I_ILART.Trim() == "U02")
                    _sReqID = "O";
                else if (I_ILART.Trim() == "U03")
                    _sReqID = "L";
                else if (I_ILART.Trim() == "U04")
                    _sReqID = "L";
                else if (I_ILART.Trim() == "U05")
                    _sReqID = "C";
                else if (I_ILART.Trim() == "U06")
                    _sReqID = "C";
                else if (I_ILART.Trim() == "U07")
                    _sReqID = "A";

                _sSysDate = System.DateTime.Now.ToString("yyyyMMdd");

                if ((I_ILART.Trim() != "U01") && ABKRS.Trim() == "03")
                {
                    WORKCENTRE = GetDivisionCode(WORKCENTRE.Trim().ToUpper());
                }

                if (APPOINT_DIV.Trim() == "")
                    APPOINT_DIV = WORKCENTRE;

                if (APPLIEDLOAD.Trim() == "")
                {
                    if (APPLIEDLOADKVA.Trim() != "")
                    {
                        APPLIEDLOAD = APPLIEDLOADKVA.Trim();
                    }
                }

                int _iLoadCheck = 0;
                if (APPLIEDLOAD.Trim() == "")
                    APPLIEDLOAD = "0";
                if (APPLIEDLOADKVA.Trim() == "")
                    APPLIEDLOADKVA = "0";

                if (I_ILART.Trim() == "U03")
                {
                    _iLoadCheck = CheckRequest_LoadData(I_VKONT, "E", Convert.ToInt32(APPLIEDLOAD.Trim()), Convert.ToInt32(APPLIEDLOADKVA.Trim()));
                }
                else if (I_ILART.Trim() == "U04")
                {
                    _iLoadCheck = CheckRequest_LoadData(I_VKONT, "R", Convert.ToInt32(APPLIEDLOAD.Trim()), Convert.ToInt32(APPLIEDLOADKVA.Trim()));
                }

                if ((I_ILART.Trim() == "U01" || I_ILART.Trim() == "U03" || I_ILART.Trim() == "U04") && (APPLIEDLOAD.Trim() == "" || APPLIEDLOAD.Trim() == "0"))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Please Enter Applied Load!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (_iLoadCheck != 0)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Applied Load should not be Less than " + _iLoadCheck, "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (NewConnCount > 3)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "You requested more than 4 times, Please try again with another mobile number.", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (Holiday == 1)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Appointment not available on the given date. Please try another with another date", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (MOBILE.Trim().Length != 10)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Please Enter Correct Mobile Number!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (NewMailCount > 3)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "You requested more than 4 times, Please try again with another Email ID.", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if ((I_ILART.Trim() == "U02") && (E_O_AMT > 10))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", dsBAPIOutput.Tables[0].Rows[0]["E_O_MSG"].ToString(), "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if ((I_ILART.Trim() == "U02") && (Bp_type == "Tenant"))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Unable to register name change request as entered CA no. belongs to tenant connection.", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (((WORKCENTRE.Trim() == "S1NFC") || (WORKCENTRE.Trim() == "W1JFR") || (WORKCENTRE.Trim() == "W1NJF") ||
                    (WORKCENTRE.Trim() == "S1SVR") || (WORKCENTRE.Trim() == "W2VKP")) && (APPOINT_DIV.Trim() != WORKCENTRE.Trim()))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Not Allow to Take Request for Other Division!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (WORKCENTRE.Trim() != "")
                {
                    if (_sSysDate.Trim() == START_DATE.Trim())
                    {
                        _sAppType = "O" + _sReqID + WORKCENTRE.Trim().ToUpper().Substring(2, 3);
                    }
                    else
                    {
                        _sAppType = "A" + _sReqID + WORKCENTRE.Trim().ToUpper().Substring(2, 3);
                    }

                    _sNReqNo = _sAppType + dt.Rows[0][0].ToString();

                    DataTable _dtConsumer = new DataTable();
                    string _sName = string.Empty, NAME_FIRST = string.Empty, NAME_LAST = string.Empty, EXISTING_LOAD = string.Empty, TITLE1 = string.Empty;

                    if (I_ILART.Trim() != "U01")
                    {
                        _dtConsumer = GetSAP_Data(I_VKONT.Trim().Replace("'", "''"));
                        if (_dtConsumer.Rows.Count > 0)
                        {
                            _sName = Convert.ToString(_dtConsumer.Rows[0]["Bp_Name"].ToString().Trim());
                            TITLE1 = Convert.ToString(_sName.Split(' ')[0].ToString());

                            if (I_ILART.Trim() == "U02")
                            {
                                //NAME_FIRST = Convert.ToString(_sName.Substring(TITLE1.Length, _sName.Length - TITLE1.Length)).Trim();
                                NAME_FIRST = _sName;
                                NAME_LAST = ".";
                            }
                            else
                            {
                                //NAME_FIRST = Convert.ToString(_sName.Substring(TITLE1.Length, _sName.Length - TITLE1.Length)).Trim();
                                NAME_FIRST = _sName;
                                NAME_LAST = ".";
                                //FIRSTNAME = Convert.ToString(_sName.Substring(TITLE1.Length, _sName.Length - TITLE1.Length)).Trim();
                                FIRSTNAME = _sName;
                                LASTNAME = ".";
                            }

                            MOBILE = Convert.ToString(_dtConsumer.Rows[0]["Tel1_Number"]);
                            if (I_ILART.Trim() == "U07")
                            {

                            }
                            else
                            {
                                POSTL_COD1 = Convert.ToString(_dtConsumer.Rows[0]["Post_Code"]);
                                BUILDING = Convert.ToString(_dtConsumer.Rows[0]["Street"]);
                                HOUSE_NO = Convert.ToString(_dtConsumer.Rows[0]["House_Number"]);
                                STR_SUPPL1 = Convert.ToString(_dtConsumer.Rows[0]["Street2"]);
                                STR_SUPPL2 = Convert.ToString(_dtConsumer.Rows[0]["Street3"]);
                                STR_SUPPL3 = Convert.ToString(_dtConsumer.Rows[0]["Street4"]);
                            }

                            EXISTING_LOAD = Convert.ToString(_dtConsumer.Rows[0]["WERT1"]);
                        }
                    }
                    else
                    {
                        NAME_FIRST = FIRSTNAME.Trim().ToUpper().Replace("'", "''");
                        NAME_LAST = LASTNAME.Trim().ToUpper().Replace("'", "''");
                    }



                    sql.Append("insert into mobapp.sap_sevakendra(REQUEST_NO,AUFNR,ILART,ZZ_VKONT,AUART,TITLE,NAME_FIRST,NAME_LAST,NAME2,NAME1,NAMEMIDDLE,SECONDNAME,");
                    sql.Append(" XSEXU,HOUSE_NUM1,STREET,STR_SUPPL1,STR_SUPPL2,STR_SUPPL3,POST_CODE1,CITY_CODE,E_MAIL,TEL_NUMBER,TEL_EXTENS,ERDAT,BUKRS,VAPLZ, ");
                    sql.Append(" GSTRP,GSUZP,STRMN,STRUR,LTRMN,LTRUR, BPKIND,RUN_FOR_DATE,RUN_ON_DATE,FECOD,ZZ_RLOAD,ZZ_RLOADKVA,ZZ_CONNTYPE,");
                    sql.Append(" ENTRY_DT,FLAG,ACCOUNT_CLASS,REG_SOURCE,SAP_CREATED_BY,PLANNER_GROUP,ENTER_FRM_SRC, LANDLINE,I_VKONA, ");
                    sql.Append(" PARTNERCATEGORY,PARTNERTYPE,JOBGR,IDTYPE,IDNUMBER,PLANNINGPLANT,SYSTEMCOND,LATITUDE,LONGITUDE,GEOCOR_ADDRESS,APPOINT_DIV,EXISTING_LOAD,EXISTING_LOADKVA,MALE,FEMALE) ");
                    sql.Append(" VALUES ");

                    sql.Append(" ('" + _sNReqNo + "','" + _sNReqNo + "' ,'" + I_ILART.Trim().Replace("'", "''") + "', '" + I_VKONT.Trim().Replace("'", "''") + "','ZDSS','" + TITLE + "','" + FIRSTNAME.Trim().ToUpper().Replace("'", "''") + "','" + LASTNAME.Trim().ToUpper().Replace("'", "''") + "',");
                    sql.Append(" '" + NAME_FIRST.Trim().ToUpper().Replace("'", "''") + "','" + NAME_LAST.Trim().ToUpper().Replace("'", "''") + "','" + MIDDLENAME.Trim().ToUpper().Replace("'", "''") + "','" + FATHERSNAME.Trim().ToUpper().Replace("'", "''") + "',");
                    sql.Append(" '" + SEX + "','" + HOUSE_NO.Trim().ToUpper().Replace("'", "''") + "','" + BUILDING.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL1.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL2.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL3.Trim().ToUpper().Replace("'", "''") + "',");
                    sql.Append("  '" + POSTL_COD1.Replace("'", "''") + "','" + CITY.Replace("'", "''") + "', '" + E_MAIL.Trim().Replace("'", "''") + "','" + MOBILE.Trim().Replace("'", "''") + "','SMS',to_char(sysdate,'yyyy-MM-dd'),'BRPL','" + WORKCENTRE.Trim().ToUpper().Replace("'", "''") + "', ");
                    sql.Append("  to_char(sysdate,'yyyy-MM-dd'),'" + START_TIME.Trim().Replace("'", "''") + "','" + _sStartDate.Trim().Replace("'", "''") + "','" + START_TIME.Trim().Replace("'", "''") + "','" + _sEndDate.Trim().Replace("'", "''") + "','" + FINISH_TIME.Trim().Replace("'", "''") + "',");
                    sql.Append(" '0010',to_char(sysdate,'dd.MM.yyyy'),to_char(sysdate,'dd.MM.yyyy'),'" + APPLIEDCAT.Trim().Replace("'", "''") + "','" + APPLIEDLOAD.Trim().Replace("'", "''") + "','" + APPLIEDLOADKVA.Trim().Replace("'", "''") + "',");
                    sql.Append("  '" + CONNECTIONTYPE.Trim() + "',sysdate, '0','" + STATEMENT_CA.Replace("'", "''") + "','" + ABKRS.Trim().Replace("'", "''") + "', ");
                    sql.Append(" 'DSK_NSER', 'DSS','N','" + LANDLINE.Trim().Replace("'", "''") + "','" + I_VKONA.Trim().Replace("'", "''") + "','" + PARTNERCATEGORY.Trim().Replace("'", "''") + "' ,");
                    sql.Append(" '" + PARTNERTYPE.Trim().Replace("'", "''") + "','" + JOBGR.Trim().Replace("'", "''") + "','" + IDTYPE.Trim().Replace("'", "''") + "', ");
                    sql.Append("  '" + IDNUMBER.Trim().Replace("'", "''") + "','" + PLANNINGPLANT.Trim().Replace("'", "''") + "','" + SYSTEMCOND.Trim().Replace("'", "''") + "',");
                    sql.Append("  '" + LONGITUDE.Trim() + "','" + LATITUDE.Trim() + "','" + GEOCOR_ADDRESS.Trim().Replace("'", "''") + "','" + APPOINT_DIV.Trim() + "','" + EXISTING_LOAD.Trim() + "','" + EXISTING_LOAD.Trim() + "','" + MALE + "','" + FEMALE + "') ");

                    if (dmlsinglequery(sql.ToString()) == true)
                    {
                        DataRow dr;
                        dr = outputFlagsTable.NewRow();
                        dr["E_Flag_Ap"] = "";
                        dr["E_Flag_Bp"] = "";
                        dr["E_Flag_So"] = "";
                        dr["E_Flag_Us"] = "";
                        dr["E_New_Partner"] = "";
                        dr["E_Service_Order"] = _sNReqNo;
                        outputFlagsTable.Rows.Add(dr);
                        outputFlagsTable.AcceptChanges();
                    }

                    if (APPOINT_DIV.Trim().ToString() != "")
                    {
                        _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                        _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + APPOINT_DIV + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                    }
                    else
                    {
                        _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                        _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + WORKCENTRE + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                    }

                    dmlsinglequery(_sSql.ToString());

                    //SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "For appointment related queries, please contact our customer care no. 19122 / 19123 .", "", "000000", "", "", "", "", "", "0", "", "");
                    //SAPDATA_ErrorDataTable.AcceptChanges();
                }
            }
        }
        catch (Exception ex)
        {
            SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", ex.Message.ToString(), "", "000000", "", "", "", "", "", "0", "", "");
            SAPDATA_ErrorDataTable.AcceptChanges();
        }
        outputFlagsTable.TableName = "outputFlagsTable";
        SAPDATA_ErrorDataTable.TableName = "SAPDATA_ErrorDataTable";
        ErrorTable.TableName = "ErrorTable";
        messageTable.TableName = "messageTable";

        dsOrdInfo.Tables.Add(outputFlagsTable);
        dsOrdInfo.Tables.Add(SAPDATA_ErrorDataTable);
        dsOrdInfo.Tables.Add(ErrorTable);
        dsOrdInfo.Tables.Add(messageTable);

        outputFlagsTable.AcceptChanges();
        SAPDATA_ErrorDataTable.AcceptChanges();
        ErrorTable.AcceptChanges();
        messageTable.AcceptChanges();
        dsOrdInfo.DataSetName = "BAPI_RESULT";

        return dsOrdInfo;
    }

    public DataTable GetSAP_Data(string _sCaNo)
    {
        DataTable _dtSAP = new DataTable();
        DelhiWSV2.WebService obj = new DelhiWSV2.WebService();
        _dtSAP = obj.Z_BAPI_DSS_ISU_CA_DISPLAY(_sCaNo, "").Tables[0];
        return _dtSAP;
    }

    public DataTable GetSAP_Address(string _sCaNo)
    {
        DataTable _dtSAP = new DataTable();
        DelhiWSV2.WebService obj = new DelhiWSV2.WebService();
        _dtSAP = obj.ZBAPI_DISPLAY_BILL_WEB(_sCaNo, "").Tables[0];
        return _dtSAP;
    }

    public DataTable GetConsumer_Details(string _sContractNo)
    {
        DataTable dt = new DataTable();
        string _sDivCode = string.Empty;
        string _sSql = " select DIVISION,BUSINESS_PARTNER,FIRST_NAME,LAST_NAME,ADDRESS,TEL_NUMBER,EMAIL FROM pcm.CONSUMER_SAP_MASTER where CONTRACT_ACCOUNT='" + _sContractNo + "' ";
        dt = dmlgetSTDquery(_sSql);
        return dt;
    }

    public DataTable dmlgetSTDquery(string sql)
    {
        OleDbCommand dbcommand;
        //OleDbTransaction dbtrans;
        OleDbDataAdapter da;
        DataTable dt;
        NDS objNDS = new NDS();
        OleDbConnection ocon = new OleDbConnection(objNDS.STDcon());
        try
        {
            ocon.Close();
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand = new OleDbCommand();
            dbcommand.Connection = ocon;
            da = new OleDbDataAdapter();
            da.SelectCommand = dbcommand;
            dt = null;
            dt = new DataTable();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandText = sql;
            //dbcommand.Transaction = dbtrans;
            da.Fill(dt);
            return dt;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }

    public string GetDivisionCode(string _sDivName)
    {
        DataTable dt = new DataTable();
        string _sDivCode = string.Empty;
        string _sSql = " select DIST_CD from mobapp.ISUSTD_DIVISIONS_MST where  DIVISION_NAME ='" + _sDivName.ToUpper().Trim() + "'";
        //dt = dmlgetquery(_sSql);
        dt = dmlgetquery_Ebsdb(_sSql);

        if (dt.Rows.Count > 0)
        {
            _sDivCode = dt.Rows[0][0].ToString();
        }
        else
        {
            _sDivCode = "";
        }

        return _sDivCode;
    }

    [WebMethod]
    public DataSet ZBAPI_CNTAPP_DETAILMOBAPP(string strOrderType, string strDiv, string strApp_DT, string strAPPTM, string strCount)
    {
        DataSet dsOrdInfo = new DataSet();

        DataTable OutPutTable = new DataTable();
        DataTable message = new DataTable();
        DataTable Error = new DataTable();
        DataTable dt = new DataTable();

        StringBuilder sql = new StringBuilder();
        string _sSql = string.Empty;
        string _sAppointDate = string.Empty;

        DataColumn column = new DataColumn();
        OutPutTable.Columns.Add("Slot", typeof(string));

        Error.Columns.Add("Data", typeof(string));

        message.Columns.Add("messageCode", typeof(string));
        message.Columns.Add("messageText", typeof(string));

        //strAPPTM = strAPPTM.Replace(",", "','");
        //strApp_DT = strApp_DT.Replace(".", "/");
        _sAppointDate = strApp_DT.Replace(".", "/");

        try
        {
            _sSql += " select START_TIME from dcrep.WEB_SAP_TIME_SLOT where SK_FLAG='Y'";

            // DataTable _dtSlot = dmlgetquery(_sSql);
            DataTable _dtSlot = dmlgetquery_Ebsdb(_sSql);

            if (_dtSlot.Rows.Count > 0)
            {
                for (int s = 0; s < _dtSlot.Rows.Count; s++)
                {
                    DataRow dr;
                    //_sSql = " select (START_TIME ||'--' || FINISH_TIME)SLOT  from dcrep.WEB_SAP_TIME_SLOT ";
                    //_sSql += " WHERE  SK_FLAG='Y' AND START_TIME IN (Select B.app_slot ";
                    //_sSql += " from (select DISTRICT, APP_SLOT_CNT from dcrep.dss_sevakendra_mst where DISTRICT='"+ strDiv + "' AND ACTIVE='Y') A, ";
                    //_sSql += " (select DIVISION,app_slot,count(*) COUNT_APP from DSK_APPOINTMENT_DATA   ";
                    //_sSql += " where ORDER_TYPE='"+ strOrderType + "' and DIVISION='"+ strDiv + "' and trunc(APPOINTMENT_DATE)= TO_DATE('" + strApp_DT + "', 'dd/mm/yyyy') and APP_STATUS='Y' ";
                    //_sSql += " AND APP_SLOT IN (select START_TIME from dcrep.WEB_SAP_TIME_SLOT  ";
                    //_sSql += " where START_TIME in ('"+ strAPPTM + "') AND SK_FLAG='Y')  ";
                    //_sSql += " group by DIVISION,app_slot)B where a.DISTRICT=b.DIVISION and (a.APP_SLOT_CNT - b.COUNT_APP)>0) ";

                    _sSql = " select '" + strOrderType + "' ORDER_TYPE,DISTRICT DIVISION,'" + strApp_DT + "'APP_DATE,'" + _dtSlot.Rows[s][0].ToString() + "' APP_SLOT, ";
                    _sSql += " APP_SLOT_CNT - (select count(*) from mobapp.DSK_APPOINTMENT_DATA where ORDER_TYPE='" + strOrderType + "' and DIVISION='" + strDiv + "' AND ";
                    _sSql += " trunc(APPOINTMENT_DATE)= TO_DATE('" + _sAppointDate + "', 'dd/mm/yyyy') and APP_SLOT='" + _dtSlot.Rows[s][0].ToString() + "') SLOT_COUNT, ";
                    _sSql += " (select (START_TIME ||'--' || FINISH_TIME)SLOT  from dcrep.WEB_SAP_TIME_SLOT  WHERE  SK_FLAG='Y' AND START_TIME='" + _dtSlot.Rows[s][0].ToString() + "')SLOT ";
                    _sSql += " from dcrep.dss_sevakendra_mst where DISTRICT='" + strDiv + "' AND ACTIVE='Y' ";
                    _sSql += " AND '" + _dtSlot.Rows[s][0].ToString() + "'= (select START_TIME from dcrep.WEB_SAP_TIME_SLOT where START_TIME in ('" + _dtSlot.Rows[s][0].ToString() + "')  AND SK_FLAG='Y') ";

                    //dt = dmlgetquery(_sSql);
                    dt = dmlgetquery_Ebsdb(_sSql);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dt.Rows[i]["SLOT_COUNT"].ToString()) > 0)
                            {
                                dr = OutPutTable.NewRow();
                                dr["Slot"] = dt.Rows[i]["SLOT"].ToString();
                                OutPutTable.Rows.Add(dr);
                                OutPutTable.AcceptChanges();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Error.Rows.Add(ex.Message.ToString());
            Error.AcceptChanges();
        }

        OutPutTable.TableName = "OutPutTable";
        Error.TableName = "Error";
        message.TableName = "message";

        dsOrdInfo.Tables.Add(OutPutTable);
        dsOrdInfo.Tables.Add(Error);
        dsOrdInfo.Tables.Add(message);

        OutPutTable.AcceptChanges();
        Error.AcceptChanges();
        message.AcceptChanges();
        dsOrdInfo.DataSetName = "DocumentElement";

        return dsOrdInfo;
    }

    [WebMethod]
    public DataSet isuBAPINewConnRqstCountStatus(string app_src, string mobile_no, string period, string pm_activity, string val_user, string vaplz)
    {
        DataSet dsOrdInfo = new DataSet();

        DataTable OutPutTable = new DataTable();
        DataTable message = new DataTable();
        DataTable Error = new DataTable();
        DataTable dt = new DataTable();

        string _sSql = string.Empty;
        string _sAppointDate = string.Empty;

        DataColumn column = new DataColumn();
        OutPutTable.Columns.Add("msg", typeof(string));
        OutPutTable.Columns.Add("counts", typeof(string));

        Error.Columns.Add("Data", typeof(string));

        message.Columns.Add("messageCode", typeof(string));
        message.Columns.Add("messageText", typeof(string));

        try
        {
            _sSql += " select COUNT(REQUEST_NO) CNT from DSK_APPOINTMENT_DATA where APP_STATUS='Y' AND TRUNC(ENTRY_DATE) BETWEEN TRUNC(SYSDATE-30) AND TRUNC(SYSDATE) AND MOBILE_NO='" + mobile_no + "' ";

            //dt = dmlgetqueryCon(_sSql);
            dt = dmlgetquery_Ebsdb(_sSql);

            if (dt.Rows.Count > 0)
            {
                DataRow dr;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["CNT"].ToString()) > 3)
                    {
                        dr = OutPutTable.NewRow();
                        dr["msg"] = "Dear customer, numbers of request registration against a mobile number is greater than 3.";
                        dr["counts"] = dt.Rows[i]["CNT"].ToString();
                        OutPutTable.Rows.Add(dr);
                        OutPutTable.AcceptChanges();
                    }
                    else
                    {
                        dr = OutPutTable.NewRow();
                        dr["msg"] = "It is working";
                        dr["counts"] = dt.Rows[i]["CNT"].ToString();
                        OutPutTable.Rows.Add(dr);
                        OutPutTable.AcceptChanges();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Error.Rows.Add(ex.Message.ToString());
            Error.AcceptChanges();
        }

        OutPutTable.TableName = "OutPutTable";
        Error.TableName = "Error";
        message.TableName = "message";

        dsOrdInfo.Tables.Add(OutPutTable);
        dsOrdInfo.Tables.Add(Error);
        dsOrdInfo.Tables.Add(message);

        OutPutTable.AcceptChanges();
        Error.AcceptChanges();
        message.AcceptChanges();
        dsOrdInfo.DataSetName = "DocumentElement";

        return dsOrdInfo;
    }


    [WebMethod]
    public DataSet ZBAPI_WEBMobileCNT(string mobile_no, string pm_activity, string period, string val_user)
    {
        DataSet dsOrdInfo = new DataSet();

        DataTable COUNT = new DataTable();
        DataTable messageTable = new DataTable();
        DataTable Table = new DataTable();
        DataTable dt = new DataTable();

        string _sSql = string.Empty;
        string _sAppointDate = string.Empty;

        DataColumn column = new DataColumn();
        COUNT.Columns.Add("count", typeof(string));

        Table.Columns.Add("Data", typeof(string));

        messageTable.Columns.Add("messageCode", typeof(string));
        messageTable.Columns.Add("messageText", typeof(string));

        try
        {
            //_sSql += " select COUNT(AUFNR) CNT from MOBAPP.SAP_SEVAKENDRA where WEBSITE_STATUS NOT IN ('AC','CD','CI','CR') AND TRUNC(ENTRY_DT) BETWEEN TRUNC(SYSDATE-30) AND TRUNC(SYSDATE) AND TEL_NUMBER='" + mobile_no + "' "; // commented by ajay 13.09.2022
            _sSql += " select COUNT(AUFNR) CNT from MOBAPP.SAP_SEVAKENDRA where WEBSITE_STATUS NOT IN ('AC','CD','CI','CR') AND TRUNC(ENTRY_DT) BETWEEN TRUNC(SYSDATE-30) AND TRUNC(SYSDATE) AND TEL_NUMBER='" + mobile_no + "' and TEL_NUMBER not in(select mobileno from MOBAPP.CorporateNumber where isactive='Y')";

            //dt = dmlgetqueryCon(_sSql);
            dt = dmlgetquery_Ebsdb(_sSql);

            if (dt.Rows.Count > 0)
            {
                DataRow dr;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["CNT"].ToString()) > 3)
                    {
                        dr = COUNT.NewRow();
                        dr["count"] = dt.Rows[i]["CNT"].ToString();
                        COUNT.Rows.Add(dr);
                        COUNT.AcceptChanges();
                    }
                    else
                    {
                        dr = COUNT.NewRow();
                        dr["count"] = dt.Rows[i]["CNT"].ToString();
                        COUNT.Rows.Add(dr);
                        COUNT.AcceptChanges();
                    }
                }

                DataRow dr1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["CNT"].ToString()) > 3)
                    {
                        dr1 = messageTable.NewRow();
                        dr1["messageCode"] = dt.Rows[i]["CNT"].ToString();
                        dr1["messageText"] = "You requested more than 4 times, Please try enter another number.";
                        messageTable.Rows.Add(dr1);
                        messageTable.AcceptChanges();
                    }
                    else
                    {
                        dr1 = messageTable.NewRow();
                        dr1["messageCode"] = "1";
                        dr1["messageText"] = "OK";
                        messageTable.Rows.Add(dr1);
                        messageTable.AcceptChanges();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Table.Rows.Add(ex.Message.ToString());
            Table.AcceptChanges();
        }

        COUNT.TableName = "COUNT";
        Table.TableName = "Table";
        messageTable.TableName = "messageTable";

        dsOrdInfo.Tables.Add(COUNT);
        dsOrdInfo.Tables.Add(Table);
        dsOrdInfo.Tables.Add(messageTable);

        COUNT.AcceptChanges();
        Table.AcceptChanges();
        messageTable.AcceptChanges();
        dsOrdInfo.DataSetName = "DocumentElement";

        return dsOrdInfo;
    }

    [WebMethod]
    public DataSet ZBAPI_WEBEmailCNT(string EmailID, string pm_activity, string period, string val_user)
    {
        DataSet dsOrdInfo = new DataSet();

        DataTable COUNT = new DataTable();
        DataTable messageTable = new DataTable();
        DataTable Table = new DataTable();
        DataTable dt = new DataTable();

        string _sSql = string.Empty;
        string _sAppointDate = string.Empty;

        DataColumn column = new DataColumn();
        COUNT.Columns.Add("count", typeof(string));

        Table.Columns.Add("Data", typeof(string));

        messageTable.Columns.Add("messageCode", typeof(string));
        messageTable.Columns.Add("messageText", typeof(string));

        try
        {
            //_sSql += " select COUNT(AUFNR) CNT from MOBAPP.SAP_SEVAKENDRA where WEBSITE_STATUS NOT IN ('AC','CD','CI','CR') AND TRUNC(ENTRY_DT) BETWEEN TRUNC(SYSDATE-30) AND TRUNC(SYSDATE) AND E_MAIL='" + EmailID + "' AND E_MAIL is not null ";
            _sSql += " select COUNT(AUFNR) CNT from MOBAPP.SAP_SEVAKENDRA where WEBSITE_STATUS NOT IN ('AC','CD','CI','CR') AND TRUNC(ENTRY_DT) BETWEEN TRUNC(SYSDATE-30) AND TRUNC(SYSDATE) AND E_MAIL='" + EmailID + "' AND E_MAIL is not null and E_MAIL not in (select CorporateEmail from MOBAPP.CorporateEmail where isactive='Y')";
            //dt = dmlgetqueryCon(_sSql);
            dt = dmlgetquery_Ebsdb(_sSql);

            if (dt.Rows.Count > 0)
            {
                DataRow dr;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["CNT"].ToString()) > 3)
                    {
                        dr = COUNT.NewRow();
                        dr["count"] = dt.Rows[i]["CNT"].ToString();
                        COUNT.Rows.Add(dr);
                        COUNT.AcceptChanges();
                    }
                    else
                    {
                        dr = COUNT.NewRow();
                        dr["count"] = dt.Rows[i]["CNT"].ToString();
                        COUNT.Rows.Add(dr);
                        COUNT.AcceptChanges();
                    }
                }

                DataRow dr1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["CNT"].ToString()) > 3)
                    {
                        dr1 = messageTable.NewRow();
                        dr1["messageCode"] = dt.Rows[i]["CNT"].ToString();
                        dr1["messageText"] = "You requested more than 4 times, Please try enter another number.";
                        messageTable.Rows.Add(dr1);
                        messageTable.AcceptChanges();
                    }
                    else
                    {
                        dr1 = messageTable.NewRow();
                        dr1["messageCode"] = "1";
                        dr1["messageText"] = "OK";
                        messageTable.Rows.Add(dr1);
                        messageTable.AcceptChanges();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Table.Rows.Add(ex.Message.ToString());
            Table.AcceptChanges();
        }

        COUNT.TableName = "COUNT";
        Table.TableName = "Table";
        messageTable.TableName = "messageTable";

        dsOrdInfo.Tables.Add(COUNT);
        dsOrdInfo.Tables.Add(Table);
        dsOrdInfo.Tables.Add(messageTable);

        COUNT.AcceptChanges();
        Table.AcceptChanges();
        messageTable.AcceptChanges();
        dsOrdInfo.DataSetName = "DocumentElement";

        return dsOrdInfo;
    }


    [WebMethod]
    public DataSet ZBAPI_CS_MOBILE_APPCNT(string mobile_no, string pm_activity, string period, string val_user)
    {
        DataSet dsOrdInfo = new DataSet();

        DataTable COUNT = new DataTable();
        DataTable messageTable = new DataTable();
        DataTable Table = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtCorp = new DataTable();

        string _sSql = string.Empty;
        string _sSqlCorp = string.Empty;
        string _sAppointDate = string.Empty;

        DataColumn column = new DataColumn();
        COUNT.Columns.Add("count", typeof(string));

        Table.Columns.Add("Data", typeof(string));

        messageTable.Columns.Add("messageCode", typeof(string));
        messageTable.Columns.Add("messageText", typeof(string));

        try
        {
            _sSql += " select COUNT(REQUEST_NO) CNT from DSK_APPOINTMENT_DATA where APP_STATUS='Y' AND TRUNC(ENTRY_DATE) BETWEEN TRUNC(SYSDATE-30) AND TRUNC(SYSDATE) AND MOBILE_NO='" + mobile_no + "' ";

            _sSqlCorp = "select mobileno from MOBAPP.CorporateNumber where isactive='Y' and mobileno='" + mobile_no + "'";

            dtCorp = dmlgetquery_Ebsdb(_sSqlCorp);

            //dt = dmlgetqueryCon(_sSql);
            if (dtCorp.Rows.Count == 0)
            {
                dt = dmlgetquery_Ebsdb(_sSql);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["CNT"].ToString()) > 3)
                        {
                            dr = COUNT.NewRow();
                            dr["count"] = dt.Rows[i]["CNT"].ToString();
                            COUNT.Rows.Add(dr);
                            COUNT.AcceptChanges();
                        }
                        else
                        {
                            dr = COUNT.NewRow();
                            dr["count"] = dt.Rows[i]["CNT"].ToString();
                            COUNT.Rows.Add(dr);
                            COUNT.AcceptChanges();
                        }
                    }

                    DataRow dr1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["CNT"].ToString()) > 3)
                        {
                            dr1 = messageTable.NewRow();
                            dr1["messageCode"] = dt.Rows[i]["CNT"].ToString();
                            dr1["messageText"] = "You requested more than 4 times, Please try enter another number.";
                            messageTable.Rows.Add(dr1);
                            messageTable.AcceptChanges();
                        }
                        else
                        {
                            dr1 = messageTable.NewRow();
                            dr1["messageCode"] = "1";
                            dr1["messageText"] = "OK";
                            messageTable.Rows.Add(dr1);
                            messageTable.AcceptChanges();
                        }
                    }

                }
            }
            else
            {
                DataRow dr2;
                DataRow dr3;

                dr2 = COUNT.NewRow();
                dr2["count"] = "0";
                COUNT.Rows.Add(dr2);
                COUNT.AcceptChanges();


                dr3 = messageTable.NewRow();
                dr3["messageCode"] = "3";
                dr3["messageText"] = "Corporate";
                messageTable.Rows.Add(dr3);
                messageTable.AcceptChanges();

            }
        }
        catch (Exception ex)
        {
            Table.Rows.Add(ex.Message.ToString());
            Table.AcceptChanges();
        }

        COUNT.TableName = "COUNT";
        Table.TableName = "Table";
        messageTable.TableName = "messageTable";

        dsOrdInfo.Tables.Add(COUNT);
        dsOrdInfo.Tables.Add(Table);
        dsOrdInfo.Tables.Add(messageTable);

        COUNT.AcceptChanges();
        Table.AcceptChanges();
        messageTable.AcceptChanges();
        dsOrdInfo.DataSetName = "DocumentElement";

        return dsOrdInfo;
    }

    [WebMethod]
    public DataSet ZBAPI_CNTAPP_DETAILMOB(string strOrderType, string strDiv, string strApp_DT, string strAPPTM, string strCount)
    {
        DataSet dsOrdInfo = new DataSet();

        //  dataSet = _iSUService.ZBAPI_CNT_APP_DETAIL_MOB(strORDER_TYPE, strDIVISION, appointmentStartDate, strAPPOINTMENT_TIME, strREC_COUNT);

        DataTable IT_DATA_TABLE1 = new DataTable();
        DataTable messageTable = new DataTable();
        DataTable Error = new DataTable();
        DataTable dt = new DataTable();

        StringBuilder sql = new StringBuilder();
        string _sSql = string.Empty;
        string _sAppointDate = string.Empty;

        DataColumn column = new DataColumn();
        IT_DATA_TABLE1.Columns.Add("ORDER_TYPE", typeof(string));
        IT_DATA_TABLE1.Columns.Add("DIVISION", typeof(string));
        IT_DATA_TABLE1.Columns.Add("APPOINTMENT_START_DATE", typeof(string));
        IT_DATA_TABLE1.Columns.Add("APPOINTMENT_TIME", typeof(string));
        IT_DATA_TABLE1.Columns.Add("APPOINTMENT_FINISH_TIME", typeof(string));
        IT_DATA_TABLE1.Columns.Add("REC_COUNT", typeof(string));

        Error.Columns.Add("Data", typeof(string));

        messageTable.Columns.Add("messageCode", typeof(string));
        messageTable.Columns.Add("messageText", typeof(string));

        //strAPPTM = strAPPTM.Replace(",", "','");
        _sAppointDate = strApp_DT.Replace(".", "/");

        try
        {
            //_sSql = " select ORDER_TYPE,DIVISION, to_char(APPOINTMENT_DATE,'dd.mm.yyyy') APP_DATE,APP_SLOT, ";
            //_sSql += " ((select APP_SLOT_CNT from dcrep.dss_sevakendra_mst where DISTRICT='"+ strDiv + "' AND ACTIVE='Y')- COUNT(APP_SLOT)) SLOT_COUNT ";
            //_sSql += " from DSK_APPOINTMENT_DATA where ORDER_TYPE='"+ strOrderType + "' and DIVISION='"+ strDiv + "' and  trunc(APPOINTMENT_DATE)= TO_DATE('"+ strApp_DT + "', 'dd/mm/yyyy') and APP_STATUS='Y' ";
            //_sSql += " AND APP_SLOT IN (select START_TIME from dcrep.WEB_SAP_TIME_SLOT  ";
            //_sSql += " where START_TIME in ('"+ strAPPTM + "') AND SK_FLAG='Y') ";
            //_sSql += " GROUP BY ORDER_TYPE,DIVISION,to_char(APPOINTMENT_DATE,'dd.mm.yyyy'),APP_SLOT ";

            for (int m = 0; m < strAPPTM.Split(',').Length; m++)
            {
                DataRow dr;

                _sSql = " select '" + strOrderType + "' ORDER_TYPE,DISTRICT DIVISION,'" + strApp_DT + "'APP_DATE,'" + strAPPTM.Split(',')[m].ToString() + "' APP_SLOT, ";
                _sSql += " APP_SLOT_CNT - (select count(*) from mobapp.DSK_APPOINTMENT_DATA where ORDER_TYPE='" + strOrderType + "' and DIVISION='" + strDiv + "' AND ";
                _sSql += " trunc(APPOINTMENT_DATE)= TO_DATE('" + _sAppointDate + "', 'dd/mm/yyyy') and APP_SLOT='" + strAPPTM.Split(',')[m].ToString() + "') SLOT_COUNT, ";
                _sSql += " (select FINISH_TIME from dcrep.WEB_SAP_TIME_SLOT where START_TIME in ('" + strAPPTM.Split(',')[m].ToString() + "')  AND SK_FLAG='Y') APP_FINISH_TIME ";
                _sSql += " from dcrep.dss_sevakendra_mst where DISTRICT='" + strDiv + "' AND ACTIVE='Y' ";
                _sSql += " AND '" + strAPPTM.Split(',')[m].ToString() + "'= (select START_TIME from dcrep.WEB_SAP_TIME_SLOT where START_TIME in ('" + strAPPTM.Split(',')[m].ToString() + "')  AND SK_FLAG='Y') ";
                _sSql += " and TO_DATE('" + _sAppointDate + "', 'dd/mm/yyyy') not in (select H_DATE from mobapp.dss_holiday_mst) ";

                //_sSql = " select '" + strOrderType + "' ORDER_TYPE,DISTRICT DIVISION,'" + strApp_DT + "'APP_DATE,'" + strAPPTM.Split(',')[m].ToString() + "' APP_SLOT, ";
                //_sSql += " (select count(*) from mobapp.DSK_APPOINTMENT_DATA where ORDER_TYPE='" + strOrderType + "' and DIVISION='" + strDiv + "' AND ";
                //_sSql += " trunc(APPOINTMENT_DATE)= TO_DATE('" + _sAppointDate + "', 'dd/mm/yyyy') and APP_SLOT='" + strAPPTM.Split(',')[m].ToString() + "') SLOT_COUNT, ";
                //_sSql += " (select FINISH_TIME from dcrep.WEB_SAP_TIME_SLOT where START_TIME in ('" + strAPPTM.Split(',')[m].ToString() + "')  AND SK_FLAG='Y') APP_FINISH_TIME ";
                //_sSql += " from dcrep.dss_sevakendra_mst where DISTRICT='" + strDiv + "' AND ACTIVE='Y' ";
                //_sSql += " AND '" + strAPPTM.Split(',')[m].ToString() + "'= (select START_TIME from dcrep.WEB_SAP_TIME_SLOT where START_TIME in ('" + strAPPTM.Split(',')[m].ToString() + "')  AND SK_FLAG='Y') ";


                //dt = dmlgetquery(_sSql);
                dt = dmlgetquery_Ebsdb(_sSql);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        dr = IT_DATA_TABLE1.NewRow();
                        dr["ORDER_TYPE"] = strOrderType;
                        dr["DIVISION"] = strDiv;
                        dr["APPOINTMENT_START_DATE"] = dt.Rows[i]["APP_DATE"].ToString();
                        dr["APPOINTMENT_TIME"] = dt.Rows[i]["APP_SLOT"].ToString();
                        dr["APPOINTMENT_FINISH_TIME"] = dt.Rows[i]["APP_FINISH_TIME"].ToString();
                        //dr["REC_COUNT"] = dt.Rows[i]["SLOT_COUNT"].ToString();

                        if (Convert.ToInt32(dt.Rows[i]["SLOT_COUNT"].ToString()) > 0)
                            dr["REC_COUNT"] = dt.Rows[i]["SLOT_COUNT"].ToString();
                        else
                            dr["REC_COUNT"] = "0";

                        IT_DATA_TABLE1.Rows.Add(dr);
                        IT_DATA_TABLE1.AcceptChanges();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Error.Rows.Add(ex.Message.ToString());
            Error.AcceptChanges();
        }

        IT_DATA_TABLE1.TableName = "IT_DATA_TABLE1";
        Error.TableName = "Error";
        messageTable.TableName = "messageTable";

        dsOrdInfo.Tables.Add(IT_DATA_TABLE1);
        dsOrdInfo.Tables.Add(Error);
        dsOrdInfo.Tables.Add(messageTable);

        IT_DATA_TABLE1.AcceptChanges();
        Error.AcceptChanges();
        messageTable.AcceptChanges();
        dsOrdInfo.DataSetName = "BAPI_RESULT";

        return dsOrdInfo;
    }


    [WebMethod]
    public DataSet ZBAPI_CNTAPP_INTRADSK(string strApp_DT)
    {
        DataSet dsOrdInfo = new DataSet();

        DataTable IT_DATA_TABLE1 = new DataTable();
        DataTable messageTable = new DataTable();
        DataTable Error = new DataTable();
        DataTable dt = new DataTable();

        StringBuilder sql = new StringBuilder();
        string _sSql = string.Empty;
        string _sAppointDate = string.Empty;

        DataColumn column = new DataColumn();
        IT_DATA_TABLE1.Columns.Add("DISTRICT", typeof(string));
        IT_DATA_TABLE1.Columns.Add("SK_ADDRESS_P2", typeof(string));
        IT_DATA_TABLE1.Columns.Add("NAME", typeof(string));
        IT_DATA_TABLE1.Columns.Add("SLOT_COUNT", typeof(string));

        Error.Columns.Add("Data", typeof(string));

        messageTable.Columns.Add("messageCode", typeof(string));
        messageTable.Columns.Add("messageText", typeof(string));

        _sAppointDate = strApp_DT.Replace(".", "/");

        try
        {
            _sSql = " select DISTRICT,SK_ADDRESS_P2 ,NAME, APP_SLOT_CNT - (select count(*) from mobapp.DSK_APPOINTMENT_DATA where ORDER_TYPE='ZDSS' and DIVISION=M.DISTRICT AND ";
            _sSql += " trunc(APPOINTMENT_DATE)= TO_DATE('" + _sAppointDate + "', 'dd/mm/yyyy') and  ";
            _sSql += "     APP_SLOT in (select START_TIME from dcrep.WEB_SAP_TIME_SLOT where SK_FLAG='Y')) SLOT_COUNT ";
            _sSql += " from dcrep.dss_sevakendra_mst M where  substr(DISTRICT,0,1) in ('W','S') and ACTIVE='Y' ";

            //dt = dmlgetquery(_sSql);
            dt = dmlgetquery_Ebsdb(_sSql);

            if (dt.Rows.Count > 0)
            {
                DataRow dr;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = IT_DATA_TABLE1.NewRow();
                    dr["DISTRICT"] = dt.Rows[i]["DISTRICT"].ToString();
                    dr["SK_ADDRESS_P2"] = dt.Rows[i]["SK_ADDRESS_P2"].ToString();
                    dr["NAME"] = dt.Rows[i]["NAME"].ToString();
                    //dr["SLOT_COUNT"] = dt.Rows[i]["SLOT_COUNT"].ToString();

                    if (Convert.ToInt32(dt.Rows[i]["SLOT_COUNT"].ToString()) > 0)
                        dr["SLOT_COUNT"] = dt.Rows[i]["SLOT_COUNT"].ToString();
                    else
                        dr["SLOT_COUNT"] = "0";

                    IT_DATA_TABLE1.Rows.Add(dr);
                    IT_DATA_TABLE1.AcceptChanges();
                }
            }
        }
        catch (Exception ex)
        {
            Error.Rows.Add(ex.Message.ToString());
            Error.AcceptChanges();
        }

        IT_DATA_TABLE1.TableName = "IT_DATA_TABLE1";
        Error.TableName = "Error";
        messageTable.TableName = "messageTable";

        dsOrdInfo.Tables.Add(IT_DATA_TABLE1);
        dsOrdInfo.Tables.Add(Error);
        dsOrdInfo.Tables.Add(messageTable);

        IT_DATA_TABLE1.AcceptChanges();
        Error.AcceptChanges();
        messageTable.AcceptChanges();
        dsOrdInfo.DataSetName = "BAPI_RESULT";

        return dsOrdInfo;
    }

    [WebMethod]
    public DataSet ZBAPI_CS_ORD_STAT(string strAufnr)
    {
        DataSet dsOrdInfo = new DataSet();
        DataSet ds = new DataSet();

        DataTable Result = new DataTable();
        DataTable messageTable = new DataTable();
        DataTable Table1 = new DataTable();
        DataTable dt = new DataTable();

        string sqlOrd = string.Empty;
        DataTable dtOrd = new DataTable();

        StringBuilder sql = new StringBuilder();
        string _sSql = string.Empty, TITLE = string.Empty, SEX = string.Empty, _sNReqNo = string.Empty;
        string _sSysDate = string.Empty, _sAppType = string.Empty, _sStartDate = string.Empty, _sEndDate = string.Empty;

        DataColumn column = new DataColumn();
        Result.Columns.Add("ORDER_DESC", typeof(string));
        Result.Columns.Add("ORDER_STATUS", typeof(string));
        Result.Columns.Add("TEXT", typeof(string));
        Result.Columns.Add("PM_ACT_TEXT", typeof(string));
        Result.Columns.Add("COMPANY_CODE", typeof(string));
        Result.Columns.Add("CONTRACT_ACCOUNT", typeof(string));
        Result.Columns.Add("CA_NO", typeof(string));
        Result.Columns.Add("ORDER_TYPE", typeof(string));
        Result.Columns.Add("ORDER_STATUS_DESC", typeof(string));

        Table1.Columns.Add("Data", typeof(string));

        messageTable.Columns.Add("messageCode", typeof(string));
        messageTable.Columns.Add("messageText", typeof(string));


        try
        {
            DelhiWSV2.WebService obj = new DelhiWSV2.WebService();
            //  dsOrdInfo = obj.ZBAPI_CS_ORD_STAT(strAufnr.Length == 10 ? "00" + strAufnr : strAufnr);

            // _sSql = "SELECT TO_CHAR(SYSDATE,'DDMMYY')|| LPAD(COUNT(1)+1,4,'0') FROM mobapp.sap_sevakendra WHERE SUBSTR(REQUEST_NO,6,6)=TO_CHAR(SYSDATE,'DDMMYY')";
            // dt = dmlgetquery(_sSql);

            //if (dt.Rows.Count > 0)
            //{
            //    _sSysDate = System.DateTime.Now.ToString("yyyy-MM-dd");

            //    // _sNReqNo = _sAppType + dt.Rows[0][0].ToString();
            //    //if (dmlsinglequery(sql.ToString()) == true)
            //    for (int i = 0; i < strAPPTM.Split(',').Length; i++)
            //    {

            //if (dsOrdInfo.Tables[0].Rows[0]["ORDER_DESC"].ToString().Trim() != "")
            //{
            //    DataRow dr;
            //    dr = Result.NewRow();
            //    dr["ORDER_DESC"] = dsOrdInfo.Tables[0].Rows[0]["ORDER_DESC"].ToString();
            //    dr["ORDER_STATUS"] = dsOrdInfo.Tables[0].Rows[0]["ORDER_STATUS"].ToString();
            //    dr["TEXT"] = dsOrdInfo.Tables[0].Rows[0]["TEXT"].ToString();
            //    dr["PM_ACT_TEXT"] = dsOrdInfo.Tables[0].Rows[0]["PM_ACT_TEXT"].ToString();
            //    dr["COMPANY_CODE"] = dsOrdInfo.Tables[0].Rows[0]["COMPANY_CODE"].ToString();
            //    dr["CONTRACT_ACCOUNT"] = strAufnr;
            //    dr["CA_NO"] = dsOrdInfo.Tables[0].Rows[0]["CONTRACT_ACCOUNT"].ToString();
            //    dr["ORDER_TYPE"] = dsOrdInfo.Tables[0].Rows[0]["ORDER_TYPE"].ToString();

            //    sqlOrd = "select name from mobapp.SK_COR_SYS_MST where id='" + dsOrdInfo.Tables[0].Rows[0]["ORDER_STATUS"].ToString() + "' and ACTIVE='Y'";
            //    dtOrd = dmlgetquery(sqlOrd);
            //    if (dtOrd.Rows.Count > 0)
            //    {
            //        dr["ORDER_STATUS_DESC"] = dtOrd.Rows[0]["name"].ToString();
            //    }
            //    else
            //    {
            //        dr["ORDER_STATUS_DESC"] = "";
            //    }

            //    Result.Rows.Add(dr);
            //    Result.AcceptChanges();
            //}
            //else
            {
                _sSql = " select DECODE(ILART,'U01','New Connection','U02','Name Change','U03','Load Enhancement','U04','Load Reduction','U05','Category Change Low to High', ";
                _sSql += " 'U06','Category Change High to Low','U07','Address Correction') ORDER_DESC,WEBSITE_STATUS ORDER_STATUS, ";
                _sSql += " DECODE(WEBSITE_STATUS, 'N', 'Documents verification under process','DR', 'Documents verification under process','DOCI', 'Documents verification under process','DU', 'Documents verification under process', 'D', 'Field inspection under process', 'DR', 'Documents verification under process', ";
                _sSql += " 'A', 'Field inspection under process','ICFP', 'Field inspection under process', 'SOCR', 'Load Sanction under process', 'CTF_UP', 'Field inspection under process', 'PAL0', 'Field inspection under process', 'PAL2', 'Field inspection under process', 'PAONM', 'Field inspection under process', 'PADH', 'Field inspection under process' , 'RTL0', 'Field inspection under process', 'A', 'Field inspection under process', 'CFR', 'Field inspection under process', 'BTFR', 'Field inspection under process', 'DL', 'Field inspection under process', 'DEF_RUP', 'Field inspection under process', 'Rejected', 'Field inspection under process', 'Approved', 'Field inspection under process' , 'CF/TF Not Required', 'Field inspection under process', 'CF', 'Field inspection under process', 'PTF', 'Field inspection under process', 'BTFR+CFR', 'Field inspection under process', 'PCVL', 'Field inspection under process', 'TALR', 'Field inspection under process', 'SCRT', 'Load Sanction under process', 'LE', 'Load Sanction under process' , 'LR', 'Load Sanction under process', 'NC', 'Load Sanction under process', 'CC', 'Load Sanction under process', 'AC', 'Order Cancelled', 'CD', 'Order Cancelled', 'CI', 'Order Cancelled', 'CR', 'Order Cancelled') TEXT,  ";
                _sSql += " DECODE(ILART, 'U01', 'New Connection', 'U02', 'Name Change', 'U03', 'Load Enhancement', 'U04', 'Load Reduction', 'U05', 'Category Change Low to High', ";
                _sSql += " 'U06', 'Category Change High to Low', 'U07', 'Address Correction') PM_ACT_TEXT,BUKRS COMPANY_CODE, REQUEST_NO CONTRACT_ACCOUNT,ZZ_VKONT CA_NO,AUART ORDER_TYPE  ";
                _sSql += " from mobapp.sap_sevakendra where REQUEST_NO = '" + strAufnr + "' or AUFNR='" + strAufnr + "' ";

                // dt = dmlgetquery(_sSql);
                dt = dmlgetquery_Ebsdb(_sSql);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr;
                    dr = Result.NewRow();
                    dr["ORDER_DESC"] = dt.Rows[0]["ORDER_DESC"].ToString();
                    dr["ORDER_STATUS"] = dt.Rows[0]["ORDER_STATUS"].ToString();
                    dr["TEXT"] = dt.Rows[0]["TEXT"].ToString();
                    dr["PM_ACT_TEXT"] = dt.Rows[0]["PM_ACT_TEXT"].ToString();
                    dr["COMPANY_CODE"] = dt.Rows[0]["COMPANY_CODE"].ToString();
                    dr["CONTRACT_ACCOUNT"] = strAufnr;
                    dr["CA_NO"] = dt.Rows[0]["CA_NO"].ToString();
                    dr["ORDER_TYPE"] = dt.Rows[0]["ORDER_TYPE"].ToString();

                    //sqlOrd = "select name from mobapp.SK_COR_SYS_MST where id='" + dsOrdInfo.Tables[0].Rows[0]["ORDER_STATUS"].ToString() + "' and ACTIVE='Y'";
                    //dtOrd = dmlgetquery(sqlOrd);
                    //if (dtOrd.Rows.Count > 0)
                    //{
                    //    dr["ORDER_STATUS_DESC"] = dtOrd.Rows[0]["name"].ToString();
                    //}
                    //else
                    //{
                    //    dr["ORDER_STATUS_DESC"] = "";
                    //}

                    Result.Rows.Add(dr);
                    Result.AcceptChanges();
                }
                else
                {
                    DataRow dr;
                    dr = Result.NewRow();
                    dr["ORDER_DESC"] = "N/A";
                    dr["ORDER_STATUS"] = "N/A";
                    dr["TEXT"] = "N/A";
                    dr["PM_ACT_TEXT"] = "N/A";
                    dr["COMPANY_CODE"] = "N/A";
                    dr["CONTRACT_ACCOUNT"] = strAufnr;
                    dr["CA_NO"] = "N/A";
                    dr["ORDER_TYPE"] = "N/A";
                    Result.Rows.Add(dr);
                    Result.AcceptChanges();
                }
            }
        }
        catch (Exception ex)
        {
            messageTable.Rows.Add("E", ex.Message.ToString());
            messageTable.AcceptChanges();
        }

        Result.TableName = "Result";
        Table1.TableName = "Table1";
        messageTable.TableName = "message";

        ds.Tables.Add(Result);
        ds.Tables.Add(Table1);
        ds.Tables.Add(messageTable);

        Result.AcceptChanges();
        Table1.AcceptChanges();
        messageTable.AcceptChanges();
        ds.DataSetName = "BAPI_RESULT";

        return ds;
    }

    [WebMethod]
    public string SK_RegDistMsgTxt(string strKeyParam, string strDist)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            return (getSK_RegDistMsgTxt(strDist));
        }
        else
            return "Invalid";
    }

    public string getSK_RegDistMsgTxt(string strDist)
    {
        string lblMsgTxt, lblMsgTxtFinal = string.Empty;

        string strSKAdd = string.Empty;
        string strSKAdd2 = string.Empty;

        string strApointIntrvl = string.Empty;
        string strTelPhoneNo = string.Empty;

        string _sSql = " SELECT SK_ADDRESS_P2, SK_ADDRESS_P1 , APOINTMENT_DAYSINTERVAL, TELEPHONE_NO FROM dcrep.DSS_SEVAKENDRA_MST WHERE UPPER(district) = UPPER('" + strDist + "') AND active='Y' ";
        //DataTable dtReqstAddress = dmlgetquery(_sSql);
        DataTable dtReqstAddress = dmlgetquery_Ebsdb(_sSql);

        if (dtReqstAddress.Rows.Count > 0)
        {
            strSKAdd = dtReqstAddress.Rows[0]["SK_ADDRESS_P2"].ToString();
            strSKAdd2 = dtReqstAddress.Rows[0]["SK_ADDRESS_P1"].ToString();

            strApointIntrvl = dtReqstAddress.Rows[0]["APOINTMENT_DAYSINTERVAL"].ToString();
            strTelPhoneNo = dtReqstAddress.Rows[0]["TELEPHONE_NO"].ToString();

            if (strSKAdd != null)
            {
                _sSql = string.Empty;
                _sSql = " SELECT MSG_BODY_SUBMIT_SK_MOB FROM DSS_OTP_MSG_CONTENT ";
                //DataTable dtReqstMsgTxt = dmlgetquery(_sSql);
                DataTable dtReqstMsgTxt = dmlgetquery_Ebsdb(_sSql);

                if (dtReqstMsgTxt.Rows.Count > 0)
                {
                    string replaceText = dtReqstMsgTxt.Rows[0]["MSG_BODY_SUBMIT_SK_MOB"].ToString();

                    lblMsgTxt = replaceText.Replace("#5", strSKAdd);
                    lblMsgTxtFinal = lblMsgTxt.Replace("#6", strTelPhoneNo);

                    return lblMsgTxtFinal + "~" + strSKAdd2 + "$" + strApointIntrvl; // "Your request has been registered with the order no : #1 with the Name #2 and Address #3 . Your appointment for New Connection is scheduled on #4 .Please visit at #5 with the required document. For any discrepancies, please contact at 399999707.";
                }
            }
        }
        return "Your request has been registered with the Request no : #1 with the Name #2 and Address #3 . Your appointment for New Connection is scheduled on #4 .";
    }


    [WebMethod]
    public DataSet ZBAPI_CREATE_SUBORDER(string AUART, string AUFNR, string PARTNER, string KTEXT, string ILART, string GSTRP,
                                           string GLTRP, string GSUZP, string GLUZP)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("SUB_ORDER", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            ds = obj.Get_ZBAPI_CREATE_SUBORDER(AUART, AUFNR, PARTNER, KTEXT, ILART, GSTRP, GLTRP, GSUZP, GLUZP);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtZBAPI = ds.Tables[0];
                if (ds.Tables[0].Rows[0][0].ToString().Trim() != "")
                {
                    dt.Rows.Add(ds.Tables[0].Rows[0][0].ToString().Trim(), "SUB_ORDER");
                    ds.Tables.Add(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Insert_ZBAPI_CREATE_SUBORDER(AUART, AUFNR, PARTNER, KTEXT, ILART, GSTRP, GLTRP, GSUZP, GLUZP,
                                                        Convert.ToString(dt.Rows[i]["SUB_ORDER"]), "", "Y");
                        }
                    }

                }
                else
                {
                    dt.Rows.Add("", "Already Created in SAP");
                    ds.Tables.Add(dt);
                }
            }
            else
            {
                dt.Rows.Add("", "Mis-Match Data");
                ds.Tables.Add(dt);
            }
            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add(ex.Message.ToString(), "");
            ds.Tables.Add(dt);
            return ds;
        }
    }

    [WebMethod]
    public DataSet ZBAPI_CREATEBP_SUBORDER(string AUART, string AUFNR, string PARTNER, string DIVCODE, string ILART, string ANLZU, string ZZ_RLOAD,
                                         string ZZ_RLOADKVA, string ZZ_CONNTYPE)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("SUB_ORDER", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            ds = obj.Get_ZBAPI_CREATEBP_SUBORDER(AUART, PARTNER, DIVCODE, ILART, ANLZU, ZZ_RLOAD, ZZ_RLOADKVA, ZZ_CONNTYPE);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtZBAPI = ds.Tables[0];
                if (ds.Tables[0].Rows[0][0].ToString().Trim() != "")
                {
                    dt.Rows.Add(ds.Tables[0].Rows[0][0].ToString().Trim(), "SUB_ORDER");
                    ds.Tables.Add(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Insert_ZBAPI_CREATE_SUBORDER(AUART, AUFNR, PARTNER, "DSK-NS-L2", ILART, "", "", "", "",
                                                        Convert.ToString(dt.Rows[i]["SUB_ORDER"]), DIVCODE, "Z");
                        }
                    }
                }
                else
                {
                    dt.Rows.Add("", "Already Created in SAP");
                    ds.Tables.Add(dt);
                }
            }
            else
            {
                dt.Rows.Add("", "Mis-Match Data");
                ds.Tables.Add(dt);
            }
            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add(ex.Message.ToString(), "");
            ds.Tables.Add(dt);
            return ds;
        }
    }


    [WebMethod]
    public DataSet BAPI_ISUSMORDER_USERSTATUSSET(string NUMBER, string INTERN, string EXTERN, string LANGU,
                                                    string LANGU_ISO, string INACTIVE)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("RETURN", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            ds = obj.Get_BAPI_ISUSMORDER_USERSTATUSSET(NUMBER, INTERN, EXTERN, LANGU, LANGU_ISO, INACTIVE);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                {
                    dt.Rows.Add("", "Some Error in SAP");
                    ds.Tables.Add(dt);
                }
            }
            else
            {
                dt.Rows.Add("", "Mis-Match Data");
                ds.Tables.Add(dt);
            }
            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add(ex.Message.ToString(), "");
            ds.Tables.Add(dt);
            return ds;
        }
    }


    // Send TF Data in SAP e.g Z_AUFNR(1021130954), MNGRP(TC028),MNCO(8100),MATXT(TEXT), Return(1)
    [WebMethod]
    public DataSet ZBAPI_UPD_TF_DET(string Z_AUFNR, string MNGRP, string MNCO, string MATXT)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("TYPE", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            ds = obj.Get_ZBAPI_UPD_TF_DET(Z_AUFNR, MNGRP, MNCO, MATXT);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtZBAPI = ds.Tables[0];
                if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                {
                    dt.Rows.Add("", "Already Created in SAP");
                    ds.Tables.Add(dt);
                }
                else
                {
                    if (dtZBAPI.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtZBAPI.Rows.Count; i++)
                        {
                            for (int s = 0; s < MNGRP.Split(',').Length; s++)
                            {
                                Insert_ZBAPI_UPD_TF_DET(Z_AUFNR, MNGRP.Split(',')[s].ToString().Trim(), MNCO.Split(',')[s].ToString().Trim(),
                                 MATXT.Split(',')[s].ToString().Trim(), Convert.ToString(dtZBAPI.Rows[i][0]));
                            }
                            //Insert_ZBAPI_UPD_TF_DET(Z_AUFNR, MNGRP, MNCO, MATXT,Convert.ToString(dtZBAPI.Rows[i][0]));
                        }
                    }
                }
            }
            else
            {
                dt.Rows.Add("", "Mis-Match Data");
                ds.Tables.Add(dt);
            }
            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add(ex.Message.ToString(), "");
            ds.Tables.Add(dt);
            return ds;
        }
    }

    // Company, date wise getStatus Data e.g BUKRS,DATE(D021,20220315), output data get
    [WebMethod]
    public DataSet ZBAPI_NEWCON_STATUS(string BUKRS, string DATE)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            ds = obj.Get_ZBAPI_NEWCON_STATUS(BUKRS, DATE);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtZBAPI = ds.Tables[0];
                if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                {
                    dt.Rows.Add("01", "Mis-Match Data in SAP");
                    ds.Tables.Add(dt);
                }

                if (dtZBAPI.Rows.Count > 0)
                {
                    for (int i = 0; i < dtZBAPI.Rows.Count; i++)
                    {
                        Insert_ZBAPI_NEWCON_STATUS(Convert.ToString(dtZBAPI.Rows[i]["ORDERID"]), Convert.ToString(dtZBAPI.Rows[i]["BUSPARTNER"]), Convert.ToString(dtZBAPI.Rows[i]["CONT_ACCT"]),
                          Convert.ToString(dtZBAPI.Rows[i]["DNPD_DATE"]), Convert.ToString(dtZBAPI.Rows[i]["DNPD_AMOUNT"]), Convert.ToString(dtZBAPI.Rows[i]["AUTO_DEBIT_DATE"]),
                          Convert.ToString(dtZBAPI.Rows[i]["AUTO_DEBIT_AMOUNT"]), Convert.ToString(dtZBAPI.Rows[i]["BPAY_DATE"]), BUKRS, DATE);

                        Insert_ZBAPI_NEWCON_STATUS_Yotta(Convert.ToString(dtZBAPI.Rows[i]["ORDERID"]), Convert.ToString(dtZBAPI.Rows[i]["BUSPARTNER"]), Convert.ToString(dtZBAPI.Rows[i]["CONT_ACCT"]),
                         Convert.ToString(dtZBAPI.Rows[i]["DNPD_DATE"]), Convert.ToString(dtZBAPI.Rows[i]["DNPD_AMOUNT"]), Convert.ToString(dtZBAPI.Rows[i]["AUTO_DEBIT_DATE"]),
                         Convert.ToString(dtZBAPI.Rows[i]["AUTO_DEBIT_AMOUNT"]), Convert.ToString(dtZBAPI.Rows[i]["BPAY_DATE"]), BUKRS, DATE);
                    }
                }

            }
            else
            {
                dt.Rows.Add("01", "Mis-Match Data");
                ds.Tables.Add(dt);
            }

            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add("01", ex.Message.ToString());
            ds.Tables.Add(dt);
            return ds;
        }
    }

    private bool Insert_ZBAPI_UPD_TF_DET(string _Z_AUFNR, string _MNGRP, string _MNCO, string _MATXT, string _RETURN_FLAG)
    {

        string sql = " INSERT INTO MOBAPP.DSK_ZBAPI_UPD_TF_DET(Z_AUFNR,MNGRP,MNCO,MATXT,RETURN_FLAG,FLAG) VALUES ";
        sql += "   ('" + _Z_AUFNR + "', '" + _MNGRP + "', '" + _MNCO + "', '" + _MATXT + "', '" + _RETURN_FLAG + "', 'Y')  ";

        return dml_singlequery(sql);
    }

    private bool Insert_ZBAPI_CREATE_SUBORDER(string _AUART, string _AUFNR, string _PARTNER, string _KTEXT, string _ILART, string _GSTRP, string _GLTRP,
                                           string _GSUZP, string _GLUZP, string _SUB_ORDER, string _DIV, string _FLAG)
    {

        string sql = " INSERT INTO MOBAPP.DSK_ZBAPI_CREATE_SUBORDER (AUART,AUFNR,PARTNER,KTEXT,ILART,GSTRP,GLTRP,GSUZP,GLUZP,SUB_ORDER,SUB_ORDER2,FLAG) VALUES ";
        sql += "   ('" + _AUART + "', '" + _AUFNR + "', '" + _PARTNER + "', '" + _KTEXT + "', '" + _ILART + "', '" + _GSTRP + "', '" + _GLTRP + "', ";
        sql += "     '" + _GSUZP + "', '" + _GLUZP + "',  '" + _SUB_ORDER + "','" + _DIV + "','" + _FLAG + "')  ";

        return dml_singlequery(sql);
    }

    private bool Insert_ZBAPI_NEWCON_STATUS(string _ORDERID, string _BUSPARTNER, string _CONT_ACCT, string _DNPD_DATE, string _DNPD_AMOUNT, string _AUTO_DEBIT_DATE, string _AUTO_DEBIT_AMOUNT,
                                            string _BPAY_DATE, string _BUKRS, string _RUN_DATE)
    {

        string sql = " INSERT INTO MOBAPP.DSK_ZBAPI_NEWCON_STATUS(ORDERID,BUSPARTNER,CONT_ACCT,DNPD_DATE,DNPD_AMOUNT,AUTO_DEBIT_DATE,AUTO_DEBIT_AMOUNT,BPAY_DATE,BUKRS,RUN_DATE) VALUES ";
        sql += "   ('" + _ORDERID + "', '" + _BUSPARTNER + "', '" + _CONT_ACCT + "', '" + _DNPD_DATE + "', '" + _DNPD_AMOUNT + "', '" + _AUTO_DEBIT_DATE + "', '" + _AUTO_DEBIT_AMOUNT + "', ";
        sql += "     '" + _BPAY_DATE + "', '" + _BUKRS + "',  '" + _RUN_DATE + "')  ";

        return dml_singlequery(sql);
    }

    public bool dml_singlequery(string sql)
    {
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbConnection ocon = new OleDbConnection(objNDS.con());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }

            dbcommand = new OleDbCommand(sql, ocon);
            //dbcommand.Transaction = dbtrans;
            dbcommand.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }


    private bool Insert_ZBAPI_NEWCON_STATUS_Yotta(string _ORDERID, string _BUSPARTNER, string _CONT_ACCT, string _DNPD_DATE, string _DNPD_AMOUNT, string _AUTO_DEBIT_DATE, string _AUTO_DEBIT_AMOUNT,
                                           string _BPAY_DATE, string _BUKRS, string _RUN_DATE)
    {

        string sql = " INSERT INTO MOBAPP.DSK_ZBAPI_NEWCON_STATUS(ORDERID,BUSPARTNER,CONT_ACCT,DNPD_DATE,DNPD_AMOUNT,AUTO_DEBIT_DATE,AUTO_DEBIT_AMOUNT,BPAY_DATE,BUKRS,RUN_DATE) VALUES ";
        sql += "   ('" + _ORDERID + "', '" + _BUSPARTNER + "', '" + _CONT_ACCT + "', '" + _DNPD_DATE + "', '" + _DNPD_AMOUNT + "', '" + _AUTO_DEBIT_DATE + "', '" + _AUTO_DEBIT_AMOUNT + "', ";
        sql += "     '" + _BPAY_DATE + "', '" + _BUKRS + "',  '" + _RUN_DATE + "')  ";

        return dml_singlequery_Yotta(sql);
    }

    public bool dml_singlequery_Yotta(string sql)
    {
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbConnection ocon = new OleDbConnection(objNDS.conYotta());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }

            dbcommand = new OleDbCommand(sql, ocon);
            //dbcommand.Transaction = dbtrans;
            dbcommand.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }

    #endregion


    [WebMethod]
    public DataSet ZBAPI_NONENG_ENF_RTGS(string COMP_CODE, string ORDER_NO, string CONTRACT_ACCOUNT, string ACCOUNT_TYPE, string AMOUNT, string FLAG)
    {
        DataSet ds = new DataSet();

        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();

        try
        {
            ds = obj.Get_ZBAPI_NONENG_ENF_RTGS(COMP_CODE, ORDER_NO, CONTRACT_ACCOUNT, ACCOUNT_TYPE, AMOUNT, FLAG);

            return ds;
        }
        catch (Exception ex)
        {
            ds.Tables[0].Rows[0][0] = ex.Message.ToString();
            ds.AcceptChanges();
            return ds;
        }
    }

    #region "Team Allocation BAPI"
    [WebMethod]
    public DataSet ZBAPI_TEAM_ALLOCATION(string IMEI, string ILART, string DATE, string TIME)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtTF = new DataTable();
        DataTable dtCF = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            ds = obj.Get_ZBAPI_TEAM_ALLOCATION(IMEI, ILART, DATE, TIME);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtTF = ds.Tables[0];
                dtCF = ds.Tables[1];

                if (ds.Tables[0].Rows[0].ToString().Trim() == "")
                {
                    dt.Rows.Add("01", "Mis-Match Data in SAP");
                    ds.Tables.Add(dt);
                }
                else
                {
                    if (dtTF.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTF.Rows.Count; i++)
                        {
                            Insert_TFData(Convert.ToString(dtTF.Rows[i]["IEMI"]), Convert.ToString(dtTF.Rows[i]["AUFNR"]), Convert.ToString(dtTF.Rows[i]["VAPLZ"]),
                              Convert.ToString(dtTF.Rows[i]["KUNUM"]), Convert.ToString(dtTF.Rows[i]["ZZ_LEC"]), Convert.ToString(dtTF.Rows[i]["APPLICANT_NAME"]), Convert.ToString(dtTF.Rows[i]["CATEGORY"]),
                              Convert.ToString(dtTF.Rows[i]["FATHER_NAME"]), Convert.ToString(dtTF.Rows[i]["ZZ_RLOAD"]), Convert.ToString(dtTF.Rows[i]["ZZ_RLOADKVA"]), Convert.ToString(dtTF.Rows[i]["APPLICANT_ADDRESS"]),
                              Convert.ToString(dtTF.Rows[i]["TELEPHONE"]), Convert.ToString(dtTF.Rows[i]["DOC_RECEIVED"]), Convert.ToString(dtTF.Rows[i]["VISIT_DATE"]), Convert.ToString(dtTF.Rows[i]["TIME"]),
                              Convert.ToString(dtTF.Rows[i]["CREATION"]), Convert.ToString(dtTF.Rows[i]["DIVISION"]), Convert.ToString(dtTF.Rows[i]["MOHALLA_KHASRA"]), Convert.ToString(dtTF.Rows[i]["HOUSE_NO"]),
                              Convert.ToString(dtTF.Rows[i]["OTHER_ADDRESS"]), Convert.ToString(dtTF.Rows[i]["ZUSER"]), Convert.ToString(dtTF.Rows[i]["ZDATE"]), Convert.ToString(dtTF.Rows[i]["CTIME"]),
                              Convert.ToString(dtTF.Rows[i]["ILART"]), Convert.ToString(dtTF.Rows[i]["AEDAT"]), Convert.ToString(dtTF.Rows[i]["AEZEIT"]), Convert.ToString(dtTF.Rows[i]["ZZ_LICNO"]));
                        }

                        //ds.Clear();
                        //dt.Rows.Add("01", "Successfully Inserted");
                        //ds.Tables.Add(dt);
                    }
                    if (dtCF.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCF.Rows.Count; i++)
                        {
                            Insert_CFData(Convert.ToString(dtCF.Rows[i]["AUFNR"]), Convert.ToString(dtCF.Rows[i]["NET_OUTSTANDING"]), Convert.ToString(dtCF.Rows[i]["GPART"]),
                              Convert.ToString(dtCF.Rows[i]["VKONT"]), Convert.ToString(dtCF.Rows[i]["AUSZDAT"]), Convert.ToString(dtCF.Rows[i]["VREFER"]),
                              Convert.ToString(dtCF.Rows[i]["NAME"]), Convert.ToString(dtCF.Rows[i]["ADDRESS"]), Convert.ToString(dtCF.Rows[i]["CHK_ENF"]),
                              Convert.ToString(dtCF.Rows[i]["LAST_PAYMENT_MODE"]), Convert.ToString(dtCF.Rows[i]["SEQUENCE_NO"]));
                        }

                        ds.Clear();
                        dt.Rows.Add("01", "Successfully Inserted");
                        ds.Tables.Add(dt);
                    }
                }
            }
            else
            {
                dt.Rows.Add("01", "Mis-Match Data");
                ds.Tables.Add(dt);
            }

            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add("01", ex.Message.ToString());
            ds.Tables.Add(dt);
            return ds;
        }
    }

    private bool Insert_TFData(string _IEMI, string _AUFNR, string _VAPLZ, string _KUNUM, string _ZZ_LEC, string _APPLICANT_NAME, string _CATEGORY, string _FATHER_NAME, string _ZZ_RLOAD,
                            string _ZZ_RLOADKVA, string _APPLICANT_ADDRESS, string _TELEPHONE, string _DOC_RECEIVED, string _VISIT_DATE, string _VISIT_TIME, string _CREATION,
                              string _DIVISION, string _MOHALLA_KHASRA, string _HOUSE_NO, string _OTHER_ADDRESS, string _ZUSER, string _ZDATE, string _CTIME, string _ILART,
                              string _AEDAT, string _AEZEIT, string _ZZ_LIC_NO)
    {

        string sql = " INSERT INTO mobapp.ISUSTD_TEAM_ALLOCATION(IEMI,AUFNR,VAPLZ,KUNUM,ZZ_LEC,APPLICANT_NAME,CATEGORY,FATHER_NAME,ZZ_RLOAD, ";
        sql += " ZZ_RLOADKVA,APPLICANT_ADDRESS,TELEPHONE,DOC_RECEIVED,VISIT_DATE,VISIT_TIME,CREATION,DIVISION,MOHALLA_KHASRA,HOUSE_NO,OTHER_ADDRESS, ";
        sql += " ZUSER,ZDATE,CTIME,ILART,AEDAT,AEZEIT,ZZ_LIC_NO,SRC) VALUES ";
        sql += "   ('" + _IEMI + "', '" + _AUFNR + "', '" + _VAPLZ + "', '" + _KUNUM + "', '" + _ZZ_LEC + "', '" + _APPLICANT_NAME + "', '" + _CATEGORY + "','" + _FATHER_NAME + "','" + _ZZ_RLOAD + "', ";
        sql += "     '" + _ZZ_RLOADKVA + "','" + _APPLICANT_ADDRESS + "', '" + _TELEPHONE + "', '" + _DOC_RECEIVED + "','" + _VISIT_DATE + "','" + _VISIT_TIME + "','" + _CREATION + "', ";
        sql += " '" + _DIVISION + "','" + _MOHALLA_KHASRA + "','" + _HOUSE_NO + "','" + _OTHER_ADDRESS + "','" + _ZUSER + "','" + _ZDATE + "','" + _CTIME + "',";
        sql += "  '" + _ILART + "','" + _AEDAT + "','" + _AEZEIT + "','" + _ZZ_LIC_NO + "','N')  ";

        return dmlMobapp_singlequery(sql);
    }

    private bool Insert_CFData(string _AUFNR, string _NET_OUTSTANDING, string _GPART, string _VKONT, string _AUSZDAT, string _VREFER, string _NAME, string _ADDRESS,
                                   string _CHK_ENF, string _LAST_PAYMENT_MODE, string _SEQUENCE_NO)
    {

        string sql = " INSERT INTO mobapp.ISUSTD_TEAM_ALLOCATION_CF(AUFNR,NET_OUTSTANDING,GPART,VKONT,AUSZDAT,VREFER,NAME,ADDRESS,CHK_ENF, ";
        sql += "  LAST_PAYMENT_MODE,SEQUENCE_NO) VALUES ";
        sql += "   ('" + _AUFNR + "', '" + _NET_OUTSTANDING + "', '" + _GPART + "', '" + _VKONT + "', '" + _AUSZDAT + "', '" + _VREFER + "', '" + _NAME + "', ";
        sql += "     '" + _ADDRESS + "', '" + _CHK_ENF + "',  '" + _LAST_PAYMENT_MODE + "', '" + _SEQUENCE_NO + "')  ";

        return dmlMobapp_singlequery(sql);
    }

    public bool dmlMobapp_singlequery(string sql)
    {
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbConnection ocon = new OleDbConnection(objNDS.Mobcon());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }

            dbcommand = new OleDbCommand(sql, ocon);
            //dbcommand.Transaction = dbtrans;
            dbcommand.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }

    #endregion


    [WebMethod]
    public DataSet SavePaymentInfo_Pinlab(string key, string TranID, string CA, string Pay_date, string amount, string payment_Mode, string division, string Company, string Chequeno, string ChequeDate, string BankName, string branchName, string MachineID, string netBillAmt, string Duedate, string billingUser)
    {
        DataSet dsstatus = new DataSet();
        DataTable DTOUT = new DataTable();
        string Sql = string.Empty;
        string SqlLog = string.Empty;
        string LogMessage = string.Empty;
        //DataColumn dtClmn = new DataColumn();
        DTOUT.Columns.Add("ID", typeof(string));
        DTOUT.Columns.Add("Message", typeof(string));
        LogMessage = "Service called with Ca: " + CA;
        SqlLog = "insert into pcm.PineLabErrLog(Message,methodName)values('" + LogMessage + "','savePaymentInfo_Pinlab')";
        if (dml_singlequery_PCM(SqlLog.ToString()) == true)
        {
        }
        string KeyConfig = ConfigurationManager.AppSettings["PinLabKey"];
        try
        {
            if (key == KeyConfig)
            {
                if (TranID == "")
                {
                    DTOUT.Rows.Add("1", "Invalid TranID");
                    DTOUT.AcceptChanges();
                }
                else if (CA == "")
                {
                    DTOUT.Rows.Add("1", "Invalid TranID");
                    DTOUT.AcceptChanges();
                }
                else if (Pay_date == "")
                {
                    DTOUT.Rows.Add("1", "Invalid Pay date");
                    DTOUT.AcceptChanges();
                }
                else if (amount == "")
                {
                    DTOUT.Rows.Add("1", "Invalid amount");
                    DTOUT.AcceptChanges();
                }
                else if (payment_Mode == "")
                {
                    DTOUT.Rows.Add("1", "Invalid payment mode");
                    DTOUT.AcceptChanges();
                }
                else if (division == "")
                {
                    DTOUT.Rows.Add("1", "Invalid division");
                    DTOUT.AcceptChanges();
                }
                else if (Company == "")
                {
                    DTOUT.Rows.Add("1", "Invalid Company");
                    DTOUT.AcceptChanges();
                }
                else if (Chequeno == "")
                {
                    DTOUT.Rows.Add("1", "Invalid Chequeno");
                    DTOUT.AcceptChanges();
                }
                else if (ChequeDate == "")
                {
                    DTOUT.Rows.Add("1", "Invalid ChequeDate");
                    DTOUT.AcceptChanges();
                }
                else if (BankName == "")
                {
                    DTOUT.Rows.Add("1", "Invalid BankName");
                    DTOUT.AcceptChanges();
                }
                else if (branchName == "")
                {
                    DTOUT.Rows.Add("1", "Invalid branchName");
                    DTOUT.AcceptChanges();
                }
                else if (MachineID == "")
                {
                    DTOUT.Rows.Add("1", "Invalid MachineID");
                    DTOUT.AcceptChanges();
                }
                else if (MachineID == "")
                {
                    DTOUT.Rows.Add("1", "Invalid MachineID");
                    DTOUT.AcceptChanges();
                }
                else if (netBillAmt == "")
                {
                    DTOUT.Rows.Add("1", "Invalid net bill amount");
                    DTOUT.AcceptChanges();
                }
                else if (Duedate == "")
                {
                    DTOUT.Rows.Add("1", "Invalid Duedate");
                    DTOUT.AcceptChanges();
                }
                else if (billingUser == "")
                {
                    DTOUT.Rows.Add("1", "Invalid billingUser");
                    DTOUT.AcceptChanges();
                }
                else
                {
                    //TO_DATE('06/29/2022 11:05:02', 'MM/DD/YYYY HH24:MI:SS')
                    Sql = "insert into pcm.PaymentInfoPineLab(id,TranID, CA, Pay_date, amount , payment_Mode , division, Company, Chequeno, ChequeDate, BankName, branchName, MachineID, netBillAmt, Duedate,BILLINGUSER)";
                    Sql += " values(PaymentInfoPineLab_seq.nextval,'" + TranID + "','" + CA + "',TO_DATE('" + Pay_date + "', 'DD/MM/YYYY HH24:MI:SS')," + amount + ",'" + payment_Mode + "','" + division + "','" + Company + "','" + Chequeno + "',TO_DATE('" + ChequeDate + "', 'DD/MM/YYYY'),'" + BankName + "','" + branchName + "','" + MachineID + "'," + netBillAmt + ",TO_DATE('" + Duedate + "', 'DD/MM/YYYY'),'" + billingUser + "')";
                    if (dml_singlequery_PCM(Sql.ToString()) == true)
                    {
                        DTOUT.Rows.Add("0", "Successfully saved payment info");
                        DTOUT.AcceptChanges();
                    }
                    else
                    {
                        LogMessage = "Failed to saved payment info with query : " + Sql;
                        SqlLog = "insert into pcm.PineLabErrLog(Message,methodName)values('" + LogMessage + "','savePaymentInfo_Pinlab')";
                        if (dml_singlequery_PCM(SqlLog.ToString()) == true)
                        {
                        }

                        DTOUT.Rows.Add("1", "Failed to saved payment info");
                        DTOUT.AcceptChanges();
                    }

                }
            }
            else
            {
                DTOUT.Rows.Add("2", "Invalid Key");
                DTOUT.AcceptChanges();
            }
        }
        catch (Exception ex)
        {
            DTOUT.Rows.Add("3", "Invalid argument");
            DTOUT.AcceptChanges();

            LogMessage = "Exception : " + ex.Message.ToString();
            SqlLog = "insert into pcm.PineLabErrLog(Message,methodName)values('" + LogMessage + "','savePaymentInfo_Pinlab')";
            if (dml_singlequery_PCM(SqlLog.ToString()) == true)
            {
            }

        }
        DTOUT.TableName = "PaymentOut";
        dsstatus.Tables.Add(DTOUT);
        dsstatus.DataSetName = "PaymentOutTab";
        return dsstatus;
    }




    [WebMethod]
    public DataSet Z_BAPI_ZDSS_WEB_LINK_CRM(string I_ILART, string I_VKONT, string I_VKONA, string PARTNERCATEGORY, string PARTNERTYPE,
                        string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO,
                        string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, string CITY,
                        string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string IDTYPE,
                        string IDNUMBER, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string APPLIEDCAT, string APPLIEDLOAD,
                        string APPLIEDLOADKVA, string CONNECTIONTYPE, string STATEMENT_CA, string START_DATE, string START_TIME,
                        string FINISH_DATE, string FINISH_TIME, string SORTFIELD, string ABKRS, string LATITUDE, string LONGITUDE,
                        string GEOCOR_ADDRESS, string APPOINT_DIV, string EXISTING_LOAD, string EXISTING_LOADKVA, string New_FIRSTNAME, string New_last_Name)
    {
        DataSet dsOrdInfo = new DataSet();

        DataTable outputFlagsTable = new DataTable();
        DataTable SAPDATA_ErrorDataTable = new DataTable();
        DataTable ErrorTable = new DataTable();
        DataTable messageTable = new DataTable();
        DataTable dt = new DataTable();

        StringBuilder sql = new StringBuilder();
        string _sSql = string.Empty, TITLE = string.Empty, SEX = string.Empty, _sNReqNo = string.Empty, _sAppointDate = string.Empty;
        string _sSysDate = string.Empty, _sAppType = string.Empty, _sStartDate = string.Empty, _sEndDate = string.Empty;

        DataColumn column = new DataColumn();
        outputFlagsTable.Columns.Add("E_Flag_Ap", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_Bp", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_So", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_Us", typeof(string));
        outputFlagsTable.Columns.Add("E_New_Partner", typeof(string));
        outputFlagsTable.Columns.Add("E_Service_Order", typeof(string));

        SAPDATA_ErrorDataTable.Columns.Add("Type", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Id", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Number", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Log_No", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Log_Msg_No", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V1", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V2", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V3", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V4", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Parameter", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Row", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Field", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("System", typeof(string));

        ErrorTable.Columns.Add("Data", typeof(string));

        messageTable.Columns.Add("messageCode", typeof(string));
        messageTable.Columns.Add("messageText", typeof(string));

        try
        {
            if (TITLE_KEY.Trim() == "0001")
                TITLE = "Ms";
            else if (TITLE_KEY.Trim() == "0002")
                TITLE = "Mr";
            else if (TITLE_KEY.Trim() == "0003")
                TITLE = "Dr";
            else if (TITLE_KEY.Trim() == "0004")
                TITLE = "Prof";
            else if (TITLE_KEY.Trim() == "0006")
                TITLE = "M/S";
            else
                TITLE = "Ms";


            if (MALE.Trim().ToUpper() == "X" || FEMALE.Trim().ToUpper() == "X")
                SEX = "X";

            if (START_DATE.Trim().Length == 8)
                _sStartDate = START_DATE.Substring(0, 4) + "-" + START_DATE.Substring(4, 2) + "-" + START_DATE.Substring(6, 2);
            if (FINISH_DATE.Trim().Length == 8)
                _sEndDate = FINISH_DATE.Substring(0, 4) + "-" + FINISH_DATE.Substring(4, 2) + "-" + FINISH_DATE.Substring(6, 2);

            if (START_DATE.Trim().Length == 8)
                _sAppointDate = START_DATE.Substring(6, 2) + "/" + START_DATE.Substring(4, 2) + "/" + START_DATE.Substring(0, 4);




            _sSql = "SELECT TO_CHAR(SYSDATE,'DDMMYY')|| LPAD(COUNT(1)+1,4,'0') FROM mobapp.sap_sevakendra WHERE SUBSTR(REQUEST_NO,6,6)=TO_CHAR(SYSDATE,'DDMMYY')";
            dt = dmlgetquery_Ebsdb(_sSql);

            string _sReqID = "";
            if (dt.Rows.Count > 0)
            {
                if (I_ILART.Trim() == "U01")
                    _sReqID = "N";
                else if (I_ILART.Trim() == "U02")
                    _sReqID = "O";
                else if (I_ILART.Trim() == "U03")
                    _sReqID = "L";
                else if (I_ILART.Trim() == "U04")
                    _sReqID = "L";
                else if (I_ILART.Trim() == "U05")
                    _sReqID = "C";
                else if (I_ILART.Trim() == "U06")
                    _sReqID = "C";
                else if (I_ILART.Trim() == "U07")
                    _sReqID = "A";

                _sSysDate = System.DateTime.Now.ToString("yyyyMMdd");

                //if (APPOINT_DIV.Trim() != "")
                //{
                //    if (_sSysDate.Trim() == START_DATE.Trim())
                //    {
                //        _sAppType = "O" + _sReqID + APPOINT_DIV.Trim().ToUpper().Substring(2, 3);
                //    }
                //    else
                //    {
                //        _sAppType = "A" + _sReqID + APPOINT_DIV.Trim().ToUpper().Substring(2, 3);
                //    }
                //}
                //else
                //{

                if ((I_ILART.Trim() != "U01") && ABKRS.Trim() == "03")
                {
                    WORKCENTRE = GetDivisionCode(WORKCENTRE.Trim().ToUpper());
                }

                if (MOBILE.Trim().Length != 10)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Please Enter Correct Mobile Number!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (((WORKCENTRE.Trim() == "S1NFC") || (WORKCENTRE.Trim() == "W1JFR") || (WORKCENTRE.Trim() == "W1NJF") ||
                   (WORKCENTRE.Trim() == "S1SVR") || (WORKCENTRE.Trim() == "W2VKP")) && (APPOINT_DIV.Trim() != WORKCENTRE.Trim()))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Not Allow to Take Request for Other Division!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (WORKCENTRE.Trim() != "")
                {
                    //if (_sSysDate.Trim() == START_DATE.Trim())
                    //{
                    //    _sAppType = "O" + _sReqID + WORKCENTRE.Trim().ToUpper().Substring(2, 3);
                    //}
                    //else
                    //{
                    _sAppType = "A" + _sReqID + WORKCENTRE.Trim().ToUpper().Substring(2, 3);
                    //}

                    _sNReqNo = _sAppType + dt.Rows[0][0].ToString();

                    //DataTable _dtConsumer = new DataTable();
                    //string _sAddress1 = string.Empty;

                    //if (I_ILART.Trim() != "U01")
                    //{
                    //    _dtConsumer = GetConsumer_Details(I_VKONT.Trim().Replace("'", "''"));
                    //    if (_dtConsumer.Rows.Count > 0)
                    //    {
                    //        if (FIRSTNAME.Trim() == "")
                    //        {
                    //            FIRSTNAME = _dtConsumer.Rows[0]["FIRST_NAME"].ToString();
                    //        }
                    //        if (LASTNAME.Trim() == "")
                    //        {
                    //            LASTNAME = _dtConsumer.Rows[0]["LAST_NAME"].ToString();
                    //        }
                    //        if (MOBILE.Trim() == "")
                    //        {
                    //            MOBILE = _dtConsumer.Rows[0]["TEL_NUMBER"].ToString();
                    //        }
                    //        if (E_MAIL.Trim() == "")
                    //        {
                    //            E_MAIL = _dtConsumer.Rows[0]["EMAIL"].ToString();
                    //        }
                    //        if (WORKCENTRE.Trim() == "")
                    //        {
                    //            WORKCENTRE = _dtConsumer.Rows[0]["DIVISION"].ToString();
                    //        }
                    //        if (HOUSE_NO.Trim() == "")
                    //        {
                    //            _sAddress1 = _dtConsumer.Rows[0]["ADDRESS"].ToString();

                    //            if (_sAddress1.ToString().Length > 100)
                    //            {
                    //                if (HOUSE_NO.Trim() == "")
                    //                    HOUSE_NO = _sAddress1.Substring(0, 20);
                    //                if (BUILDING.Trim() == "")
                    //                    BUILDING = _sAddress1.Substring(20, 35);
                    //                if (STR_SUPPL1.Trim() == "")
                    //                    STR_SUPPL1 = _sAddress1.Substring(55, 35);
                    //            }
                    //            else if (_sAddress1.ToString().Length > 70)
                    //            {
                    //                if (HOUSE_NO.Trim() == "")
                    //                    HOUSE_NO = _sAddress1.Substring(0, 15);
                    //                if (BUILDING.Trim() == "")
                    //                    BUILDING = _sAddress1.Substring(15, 35);
                    //                if (STR_SUPPL1.Trim() == "")
                    //                    STR_SUPPL1 = _sAddress1.Substring(50, 15);
                    //            }
                    //            else if (_sAddress1.ToString().Length > 40)
                    //            {
                    //                if (HOUSE_NO.Trim() == "")
                    //                    HOUSE_NO = _sAddress1.Substring(0, 10);
                    //                if (BUILDING.Trim() == "")
                    //                    BUILDING = _sAddress1.Substring(10, 13);
                    //                if (STR_SUPPL1.Trim() == "")
                    //                    STR_SUPPL1 = _sAddress1.Substring(23, 15);
                    //            }
                    //        }
                    //    }
                    //}


                    sql.Append("insert into mobapp.sap_sevakendra(REQUEST_NO,AUFNR,ILART,ZZ_VKONT,AUART,TITLE,NAME_FIRST,NAME_LAST,NAME2,NAME1,NAMEMIDDLE,SECONDNAME,");
                    sql.Append(" XSEXU,HOUSE_NUM1,STREET,STR_SUPPL1,STR_SUPPL2,STR_SUPPL3,POST_CODE1,CITY_CODE,E_MAIL,TEL_NUMBER,TEL_EXTENS,ERDAT,BUKRS,VAPLZ, ");
                    sql.Append(" GSTRP,GSUZP,STRMN,STRUR,LTRMN,LTRUR, BPKIND,RUN_FOR_DATE,RUN_ON_DATE,FECOD,ZZ_RLOAD,ZZ_RLOADKVA,ZZ_CONNTYPE,");
                    sql.Append(" ENTRY_DT,FLAG,ACCOUNT_CLASS,REG_SOURCE,SAP_CREATED_BY,PLANNER_GROUP,ENTER_FRM_SRC, LANDLINE,I_VKONA, ");
                    sql.Append(" PARTNERCATEGORY,PARTNERTYPE,JOBGR,IDTYPE,IDNUMBER,PLANNINGPLANT,SYSTEMCOND,LATITUDE,LONGITUDE,GEOCOR_ADDRESS,APPOINT_DIV,EXISTING_LOAD,EXISTING_LOADKVA) ");
                    sql.Append(" VALUES ");

                    sql.Append(" ('" + _sNReqNo + "','" + _sNReqNo + "' ,'" + I_ILART.Trim().Replace("'", "''") + "', '" + I_VKONT.Trim().Replace("'", "''") + "','ZDSS','" + TITLE + "','" + New_FIRSTNAME.Trim().ToUpper().Replace("'", "''") + "','" + New_last_Name.Trim().ToUpper().Replace("'", "''") + "',");
                    sql.Append(" '" + FIRSTNAME.Trim().ToUpper().Replace("'", "''") + "','" + LASTNAME.Trim().ToUpper().Replace("'", "''") + "','" + MIDDLENAME.Trim().ToUpper().Replace("'", "''") + "','" + FATHERSNAME.Trim().ToUpper().Replace("'", "''") + "',");
                    sql.Append(" '" + SEX + "','" + HOUSE_NO.Trim().ToUpper().Replace("'", "''") + "','" + BUILDING.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL1.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL2.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL3.Trim().ToUpper().Replace("'", "''") + "',");
                    sql.Append("  '" + POSTL_COD1.Replace("'", "''") + "','" + CITY.Replace("'", "''") + "', '" + E_MAIL.Trim().Replace("'", "''") + "','" + MOBILE.Trim().Replace("'", "''") + "','SMS',to_char(sysdate,'yyyy-MM-dd'),'BRPL','" + WORKCENTRE.Trim().ToUpper().Replace("'", "''") + "', ");
                    sql.Append("  to_char(sysdate,'yyyy-MM-dd'),'" + START_TIME.Trim().Replace("'", "''") + "','" + _sStartDate.Trim().Replace("'", "''") + "','" + START_TIME.Trim().Replace("'", "''") + "','" + _sEndDate.Trim().Replace("'", "''") + "','" + FINISH_TIME.Trim().Replace("'", "''") + "',");
                    sql.Append(" '0010',to_char(sysdate,'dd.MM.yyyy'),to_char(sysdate,'dd.MM.yyyy'),'" + APPLIEDCAT.Trim().Replace("'", "''") + "','" + APPLIEDLOAD.Trim().Replace("'", "''") + "','" + APPLIEDLOADKVA.Trim().Replace("'", "''") + "',");
                    sql.Append("  '" + CONNECTIONTYPE.Trim() + "',sysdate, '0','" + STATEMENT_CA.Replace("'", "''") + "','" + ABKRS.Trim().Replace("'", "''") + "', ");
                    sql.Append(" 'DSK_NSER', 'DSS','N','" + LANDLINE.Trim().Replace("'", "''") + "','" + I_VKONA.Trim().Replace("'", "''") + "','" + PARTNERCATEGORY.Trim().Replace("'", "''") + "' ,");
                    sql.Append(" '" + PARTNERTYPE.Trim().Replace("'", "''") + "','" + JOBGR.Trim().Replace("'", "''") + "','" + IDTYPE.Trim().Replace("'", "''") + "', ");
                    sql.Append("  '" + IDNUMBER.Trim().Replace("'", "''") + "','" + PLANNINGPLANT.Trim().Replace("'", "''") + "','" + SYSTEMCOND.Trim().Replace("'", "''") + "',");
                    sql.Append("  '" + LONGITUDE.Trim() + "','" + LATITUDE.Trim() + "','" + GEOCOR_ADDRESS.Trim().Replace("'", "''") + "','" + APPOINT_DIV.Trim() + "','" + EXISTING_LOAD + "','" + EXISTING_LOADKVA + "') ");

                    //sql.Append(" ('" + _sNReqNo + "','" + _sNReqNo + "' ,'" + I_ILART.Trim().Replace("'", "''") + "', '" + I_VKONT.Trim().Replace("'", "''") + "','ZDSS','" + TITLE + "','" + FIRSTNAME.Trim().ToUpper().Replace("'", "''") + "','" + LASTNAME.Trim().ToUpper().Replace("'", "''") + "',");
                    //sql.Append(" '" + New_FIRSTNAME.Trim().ToUpper().Replace("'", "''") + "','" + LASTNAME.Trim().ToUpper().Replace("'", "''") + "','" + MIDDLENAME.Trim().ToUpper().Replace("'", "''") + "','" + FATHERSNAME.Trim().ToUpper().Replace("'", "''") + "',");
                    //sql.Append(" '" + SEX + "','" + HOUSE_NO.Trim().ToUpper().Replace("'", "''") + "','" + BUILDING.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL1.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL2.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL3.Trim().ToUpper().Replace("'", "''") + "',");
                    //sql.Append("  '" + POSTL_COD1.Replace("'", "''") + "','" + CITY.Replace("'", "''") + "', '" + E_MAIL.Trim().Replace("'", "''") + "','" + MOBILE.Trim().Replace("'", "''") + "','SMS',to_char(sysdate,'yyyy-MM-dd'),'BRPL','" + WORKCENTRE.Trim().ToUpper().Replace("'", "''") + "', ");
                    //sql.Append("  to_char(sysdate,'yyyy-MM-dd'),'" + START_TIME.Trim().Replace("'", "''") + "','" + _sStartDate.Trim().Replace("'", "''") + "','" + START_TIME.Trim().Replace("'", "''") + "','" + _sEndDate.Trim().Replace("'", "''") + "','" + FINISH_TIME.Trim().Replace("'", "''") + "',");
                    //sql.Append(" '0010',to_char(sysdate,'dd.MM.yyyy'),to_char(sysdate,'dd.MM.yyyy'),'" + APPLIEDCAT.Trim().Replace("'", "''") + "','" + APPLIEDLOAD.Trim().Replace("'", "''") + "','" + APPLIEDLOADKVA.Trim().Replace("'", "''") + "',");
                    //sql.Append("  '" + CONNECTIONTYPE.Trim() + "',sysdate, '0','" + STATEMENT_CA.Replace("'", "''") + "','" + ABKRS.Trim().Replace("'", "''") + "', ");
                    //sql.Append(" 'DSK_NSER', 'DSS','N','" + LANDLINE.Trim().Replace("'", "''") + "','" + I_VKONA.Trim().Replace("'", "''") + "','" + PARTNERCATEGORY.Trim().Replace("'", "''") + "' ,");
                    //sql.Append(" '" + PARTNERTYPE.Trim().Replace("'", "''") + "','" + JOBGR.Trim().Replace("'", "''") + "','" + IDTYPE.Trim().Replace("'", "''") + "', ");
                    //sql.Append("  '" + IDNUMBER.Trim().Replace("'", "''") + "','" + PLANNINGPLANT.Trim().Replace("'", "''") + "','" + SYSTEMCOND.Trim().Replace("'", "''") + "',");
                    //sql.Append("  '" + LONGITUDE.Trim() + "','" + LATITUDE.Trim() + "','" + GEOCOR_ADDRESS.Trim().Replace("'", "''") + "','" + APPOINT_DIV.Trim() + "','" + EXISTING_LOAD + "','" +EXISTING_LOADKVA + "') ");          //Commented by ajay 02.05.2022



                    if (dmlsinglequery(sql.ToString()) == true)
                    {
                        DataRow dr;
                        dr = outputFlagsTable.NewRow();
                        dr["E_Flag_Ap"] = "";
                        dr["E_Flag_Bp"] = "";
                        dr["E_Flag_So"] = "";
                        dr["E_Flag_Us"] = "";
                        dr["E_New_Partner"] = "";
                        dr["E_Service_Order"] = _sNReqNo;
                        outputFlagsTable.Rows.Add(dr);
                        outputFlagsTable.AcceptChanges();
                    }

                    if (APPOINT_DIV.Trim().ToString() != "")
                    {
                        _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                        _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + APPOINT_DIV + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                    }
                    else
                    {
                        _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                        _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + WORKCENTRE + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                    }

                    dmlsinglequery(_sSql.ToString());

                    //SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "For appointment related queries, please contact our customer care no. 19122 / 19123 .", "", "000000", "", "", "", "", "", "0", "", "");
                    //SAPDATA_ErrorDataTable.AcceptChanges();
                }
            }
        }
        catch (Exception ex)
        {
            SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", ex.Message.ToString(), "", "000000", "", "", "", "", "", "0", "", "");
            SAPDATA_ErrorDataTable.AcceptChanges();
        }
        outputFlagsTable.TableName = "outputFlagsTable";
        SAPDATA_ErrorDataTable.TableName = "SAPDATA_ErrorDataTable";
        ErrorTable.TableName = "ErrorTable";
        messageTable.TableName = "messageTable";

        dsOrdInfo.Tables.Add(outputFlagsTable);
        dsOrdInfo.Tables.Add(SAPDATA_ErrorDataTable);
        dsOrdInfo.Tables.Add(ErrorTable);
        dsOrdInfo.Tables.Add(messageTable);

        outputFlagsTable.AcceptChanges();
        SAPDATA_ErrorDataTable.AcceptChanges();
        ErrorTable.AcceptChanges();
        messageTable.AcceptChanges();
        dsOrdInfo.DataSetName = "BAPI_RESULT";

        return dsOrdInfo;
    }


    #region CRM API

    [WebMethod]
    public string Insert_MobApp_DataToBRPLCRM(string MobileNo, string DateTime, string Source, string RequestType, string LanguageType)
    {
        string ResultMsg = "";
        try
        {
            Convert.ToInt64(MobileNo);
            if (String.IsNullOrEmpty(MobileNo) || (MobileNo.Length != 10))
            {
                ResultMsg = "INVALID PHONE NUMBER";
                return ResultMsg;
            }
            else
            {
                string result = Insert_data_app_crm(MobileNo, DateTime, Source, RequestType, LanguageType);
                if (result != "false")
                {
                    if (LanguageType == "English")
                    {
                        ResultMsg = "Your request number " + result + " is generated successfully. Our customer care centre will call you shortly.";
                    }
                    else
                    {
                        //ResultMsg = "आपकी आवेदन संख्या " + result + " सफलतापूर्वक प्राप्त कि गई है । हमारा ग्राहक सेवा केंद्र आपको शीघ्र ही कॉल करेगा ।"; // Commented by ajay 22022024
                        ResultMsg = "आपका अनुरोध नंबर " + result + " सफलतापूर्वक दर्ज़ कर लिया गया है। हमारे ग्राहक सेवा प्रतिनिधि जल्द ही आपको कॉल करेंगे ।"; // Added by ajay 22022024 
                        
                       
                    }
                }
                else
                {
                    ResultMsg = "Error, contact to administrator!";
                }
            }
        }
        catch (Exception ex)
        {
            ResultMsg = ex.ToString();
        }
        return ResultMsg;
    }
    public string Insert_data_app_crm(string MobileNo, string DateTime, string Source, string RequestType, string LanguageType)
    {
        string result = "";
        StringBuilder sql = new StringBuilder();
        //Execute = new DbFunction();
        string Query = "";
        sql.Append(String.Format(" INSERT INTO NC_APP_ENTRY (MOBILE_NO, DATETIME, SOURCE, REQUEST_TYPE, LANGUAGE_TYPE) VALUES ( '" + MobileNo + "', TO_DATE('" + DateTime + "','DD-MM-YYYY HH24:MI:SS'), '" + Source + "','" + RequestType + "', '" + LanguageType + "' )"));
        if (dmlsinglequery_IOMSDB(sql.ToString()) == true)
        {
            Query = String.Format(" SELECT REQUEST_ID FROM NC_APP_ENTRY WHERE MOBILE_NO ='{0}' AND DATETIME = TO_DATE('{1}','DD-MM-YYYY HH24:MI:SS') AND SOURCE='{2}' AND  "
                    + "REQUEST_TYPE = '{3}' AND LANGUAGE_TYPE = '{4}'", MobileNo, DateTime, Source, RequestType, LanguageType);
            DataTable res = new DataTable();
            res = dmlgetquery_IOMSDB(Query);
            result = res.Rows[0]["REQUEST_ID"].ToString();
            return result;
        }
        else
            return result = "false";
    }
    #endregion

    [WebMethod]
    public DataSet ConsumerReq_Display(string key, string CA_NUMBER, string meterNo, string ApplicationNo)
    {
        DataTable dt_Sap = new DataTable();
        DataTable dt = new DataTable();
        DataColumn column = new DataColumn();
        DataSet Ds = new DataSet();
        string Ca_num = string.Empty;
        string sql = string.Empty;

        dt.Columns.Add("Ca_No", typeof(string));
        dt.Columns.Add("Consumer_Name", typeof(string));
        dt.Columns.Add("Consumer_Address", typeof(string));
        dt.Columns.Add("Consumer_EmailID", typeof(string));
        dt.Columns.Add("Consumer_Mobile", typeof(string));
        dt.Columns.Add("Consumer_Division", typeof(string));
        dt.Columns.Add("Msg", typeof(string));




        try
        {
            if (key == "!@!#$&")
            {
                DelhiWSV2.WebService obj = new DelhiWSV2.WebService();
                Ca_num = "";
                //meterNo = "";
                //ApplicationNo = "";
                if (CA_NUMBER != "")
                {
                    dt_Sap = obj.Z_BAPI_CMS_ISU_CA_DISPLAY("000" + CA_NUMBER, "", "", "", "", "").Tables[0];
                }
                else if (meterNo != "")
                {
                    dt_Sap = obj.Z_BAPI_CMS_ISU_CA_DISPLAY("", "0000000000" + meterNo, "", "", "", "").Tables[0];
                }

                if (dt_Sap.Rows.Count > 0)
                {
                    Ca_num = dt_Sap.Rows[0]["Ca_Number"].ToString();
                }

                sql = "select ZZ_VKONT Ca_No,NAME_FIRST || ' '|| NAME_LAST Consumer_Name, HOUSE_NUM1 || ', '|| STREET || ', '|| STR_SUPPL1 || ', '|| STR_SUPPL2 || ', '|| STR_SUPPL3 Consumer_Address,E_MAIL Consumer_EmailID,TEL_NUMBER Consumer_Mobile, VAPLZ Consumer_Division from  mobapp.sap_sevakendra where (AUFNR='" + ApplicationNo + "' or REQUEST_NO='" + ApplicationNo + "' or ZZ_VKONT='" + Ca_num + "')";
                DataTable _dtRate = dmlgetquery_Ebsdb(sql);
                if (_dtRate.Rows.Count > 0)
                {
                    DataRow dr;
                    dr = dt.NewRow();

                    dr["Ca_No"] = _dtRate.Rows[0]["Ca_No"].ToString();
                    dr["Consumer_Name"] = _dtRate.Rows[0]["Consumer_Name"].ToString();
                    dr["Consumer_Address"] = _dtRate.Rows[0]["Consumer_Address"].ToString();
                    dr["Consumer_EmailID"] = _dtRate.Rows[0]["Consumer_EmailID"].ToString();
                    dr["Consumer_Mobile"] = _dtRate.Rows[0]["Consumer_Mobile"].ToString();
                    dr["Consumer_Division"] = _dtRate.Rows[0]["Consumer_Division"].ToString();
                    dr["Msg"] = "Success";


                    dt.Rows.Add(dr);
                    dt.AcceptChanges();


                }
                else
                {
                    string input1 = string.Empty;
                    if (CA_NUMBER != "")
                    {
                        input1 = CA_NUMBER;
                    }
                    else if (meterNo != "")
                    {
                        input1 = meterNo;
                    }
                    else if (ApplicationNo != "")
                    {
                        input1 = ApplicationNo;
                    }

                    dt.Rows.Add(input1, "", "", "", "", "", "No data found");

                    dt.AcceptChanges();
                }
            }
            else
            {
                string input2 = string.Empty;
                if (CA_NUMBER != "")
                {
                    input2 = CA_NUMBER;
                }
                else if (meterNo != "")
                {
                    input2 = meterNo;
                }
                else if (ApplicationNo != "")
                {
                    input2 = ApplicationNo;
                }

                dt.Rows.Add(input2, "", "", "", "", "", "Invalid Key.");

                dt.AcceptChanges();
            }
        }
        catch (Exception ex)
        {
            string input = string.Empty;
            if (CA_NUMBER != "")
            {
                input = CA_NUMBER;
            }
            else if (meterNo != "")
            {
                input = meterNo;
            }
            else if (ApplicationNo != "")
            {
                input = ApplicationNo;
            }

            dt.Rows.Add(input, "", "", "", "", "", ex.Message.ToString());

            dt.AcceptChanges();


        }
        Ds.Tables.Add(dt);
        Ds.Tables[0].TableName = "EMSTABLE";
        Ds.DataSetName = "EMS_RESULT";

        return Ds;
    }
    //public string Insert_data_app_crm(string MobileNo, string DateTime, string Source, string RequestType, string LanguageType)
    //{
    //    string result = "";
    //    StringBuilder sql = new StringBuilder();
    //    //Execute = new DbFunction();
    //    string Query = "";
    //    sql.Append(String.Format(" INSERT INTO NC_APP_ENTRY (MOBILE_NO, DATETIME, SOURCE, REQUEST_TYPE, LANGUAGE_TYPE) VALUES ( '" + MobileNo + "', TO_DATE('" + DateTime + "','DD-MM-YYYY HH24:MI:SS'), '" + Source + "','" + RequestType + "', '" + LanguageType + "' )"));
    //    if (dmlsinglequery(sql.ToString()) == true)
    //    {
    //        Query = String.Format(" SELECT REQUEST_ID FROM NC_APP_ENTRY WHERE MOBILE_NO ='{0}' AND DATETIME = TO_DATE('{1}','DD-MM-YYYY HH24:MI:SS') AND SOURCE='{2}' AND  "
    //                + "REQUEST_TYPE = '{3}' AND LANGUAGE_TYPE = '{4}'", MobileNo, DateTime, Source, RequestType, LanguageType);
    //        DataTable res = new DataTable();
    //        res = dmlgetquery_Ebsdb(Query);
    //        result = res.Rows[0]["REQUEST_ID"].ToString();
    //        return result;
    //    }
    //    else
    //        return result = "false";
    //}


    [WebMethod]
    public DataSet Z_BAPI_ZDSS_WEB_LINK_CRM_Test(string I_ILART, string I_VKONT, string I_VKONA, string PARTNERCATEGORY, string PARTNERTYPE,
                         string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO,
                         string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, string CITY,
                         string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string IDTYPE,
                         string IDNUMBER, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string APPLIEDCAT, string APPLIEDLOAD,
                         string APPLIEDLOADKVA, string CONNECTIONTYPE, string STATEMENT_CA, string START_DATE, string START_TIME,
                         string FINISH_DATE, string FINISH_TIME, string SORTFIELD, string ABKRS, string LATITUDE, string LONGITUDE,
                         string GEOCOR_ADDRESS, string APPOINT_DIV, string BldgIndEstName)
    {
        DataSet dsOrdInfo = new DataSet();

        DataTable outputFlagsTable = new DataTable();
        DataTable SAPDATA_ErrorDataTable = new DataTable();
        DataTable ErrorTable = new DataTable();
        DataTable messageTable = new DataTable();
        DataTable dt = new DataTable();
        DataTable DTHoliday = new DataTable();

        StringBuilder sql = new StringBuilder();
        string _sSql = string.Empty, TITLE = string.Empty, SEX = string.Empty, _sNReqNo = string.Empty, _sAppointDate = string.Empty;
        string _sSysDate = string.Empty, _sAppType = string.Empty, _sStartDate = string.Empty, _sEndDate = string.Empty, Bp_type = string.Empty, totalDues = string.Empty;

        double E_O_AMT = 0;
        double DuesAmount = 0;
        int NewConnCount = 0, NewMailCount = 0, Holiday = 0;
        DataSet dsBAPIOutput = new DataSet();

        if (I_ILART.Trim() == "")
            I_ILART = ".";
        if (I_VKONT.Trim() == "")
            I_VKONT = ".";
        if (I_VKONA.Trim() == "")
            I_VKONA = ".";
        if (FIRSTNAME.Trim() == "")
            FIRSTNAME = ".";
        if (LASTNAME.Trim() == "")
            LASTNAME = ".";
        if (MIDDLENAME.Trim() == "")
            MIDDLENAME = ".";
        if (FATHERSNAME.Trim() == "")
            FATHERSNAME = ".";
        if (HOUSE_NO.Trim() == "")
            HOUSE_NO = ".";
        if (BUILDING.Trim() == "")
            BUILDING = ".";
        if (STR_SUPPL1.Trim() == "")
            STR_SUPPL1 = ".";
        if (STR_SUPPL2.Trim() == "")
            STR_SUPPL2 = ".";
        if (STR_SUPPL3.Trim() == "")
            STR_SUPPL3 = ".";
        if (CITY.Trim() == "")
            CITY = ".";
        if (FEMALE.Trim() == "")
            FEMALE = ".";
        if (MALE.Trim() == "")
            MALE = ".";
        if (JOBGR.Trim() == "")
            JOBGR = ".";
        if (IDTYPE.Trim() == "")
            IDTYPE = ".";
        if (IDNUMBER.Trim() == "")
            IDNUMBER = ".";
        if (WORKCENTRE.Trim() == "")
            WORKCENTRE = ".";
        if (SYSTEMCOND.Trim() == "")
            SYSTEMCOND = ".";
        if (STATEMENT_CA.Trim() == "")
            STATEMENT_CA = ".";
        if (START_DATE.Trim() == "")
            START_DATE = ".";
        if (START_TIME.Trim() == "")
            START_TIME = ".";
        if (FINISH_DATE.Trim() == "")
            FINISH_DATE = ".";
        if (FINISH_TIME.Trim() == "")
            FINISH_TIME = ".";
        if (SORTFIELD.Trim() == "")
            SORTFIELD = ".";
        if (ABKRS.Trim() == "")
            ABKRS = ".";
        if (GEOCOR_ADDRESS.Trim() == "")
            GEOCOR_ADDRESS = ".";

        DataColumn column = new DataColumn();
        outputFlagsTable.Columns.Add("E_Flag_Ap", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_Bp", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_So", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_Us", typeof(string));
        outputFlagsTable.Columns.Add("E_New_Partner", typeof(string));
        outputFlagsTable.Columns.Add("E_Service_Order", typeof(string));

        SAPDATA_ErrorDataTable.Columns.Add("Type", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Id", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Number", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Log_No", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Log_Msg_No", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V1", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V2", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V3", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V4", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Parameter", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Row", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Field", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("System", typeof(string));

        ErrorTable.Columns.Add("Data", typeof(string));

        messageTable.Columns.Add("messageCode", typeof(string));
        messageTable.Columns.Add("messageText", typeof(string));

        try
        {
            if (TITLE_KEY.Trim() == "0001")
                TITLE = "Ms";
            else if (TITLE_KEY.Trim() == "0002")
                TITLE = "Mr";
            else if (TITLE_KEY.Trim() == "0003")
                TITLE = "Dr";
            else if (TITLE_KEY.Trim() == "0004")
                TITLE = "Prof";
            else if (TITLE_KEY.Trim() == "0006")
                TITLE = "M/S";
            else
                TITLE = "Mr";


            if (MALE.Trim().ToUpper() == "X" || FEMALE.Trim().ToUpper() == "X")
                SEX = "X";

            if (START_DATE.Trim().Length == 8)
                _sStartDate = START_DATE.Substring(0, 4) + "-" + START_DATE.Substring(4, 2) + "-" + START_DATE.Substring(6, 2);
            if (FINISH_DATE.Trim().Length == 8)
                _sEndDate = FINISH_DATE.Substring(0, 4) + "-" + FINISH_DATE.Substring(4, 2) + "-" + FINISH_DATE.Substring(6, 2);

            if (START_DATE.Trim().Length == 8)
                _sAppointDate = START_DATE.Substring(6, 2) + "/" + START_DATE.Substring(4, 2) + "/" + START_DATE.Substring(0, 4);

            //_sSql = "SELECT TO_CHAR(SYSDATE,'DDMMYY')|| LPAD(COUNT(1)+1,4,'0') FROM mobapp.sap_sevakendra WHERE SUBSTR(REQUEST_NO,6,6)=TO_CHAR(SYSDATE,'DDMMYY')";
            _sSql = "select to_char(sysdate,'DDMMYY')||lpad(MOBAPP.AUART_Seq.nextval,4,'0') from dual";

            dt = dmlgetquery_Ebsdb(_sSql);
            // dt = dmlgetquery(_sSql);


            _sSql = ""; // added by ajay
            _sSql = "select *  from mobapp.DSS_HOLIDAY_MST where (TO_CHAR (h_date, 'DD-MM-YYYY'))='" + _sStartDate + "'";
            DTHoliday = dmlgetquery_Ebsdb(_sSql);
            if (DTHoliday.Rows.Count > 0)
            {
                Holiday = 1;
            }

            DataTable dtBP_Type = new DataTable();
            if (I_ILART.Trim() == "U02")  // added by ajay 30.06.2022
            {
                dtBP_Type = GetSAP_Data(I_VKONT.Trim().Replace("'", "''"));
                if (dtBP_Type.Rows.Count > 0)
                {
                    Bp_type = dtBP_Type.Rows[0]["Bp_Type"].ToString();
                }
            }

            if (I_ILART.Trim() == "U02")  // added by ajay 30.06.2022
            {
                dsBAPIOutput = obj.get_ZFI_CURR_OUTS_FLAG(I_VKONT);
                if (dsBAPIOutput.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(dsBAPIOutput.Tables[0].Rows[0]["E_O_AMT"]) != "")
                    {
                        E_O_AMT = Convert.ToDouble(dsBAPIOutput.Tables[0].Rows[0]["E_O_AMT"]);
                    }
                }
            }

            NewConnCount = Convert.ToInt32(ZBAPI_WEBMobileCNT(MOBILE, "", "30", "").Tables[0].Rows[0][0]);
            NewMailCount = Convert.ToInt32(ZBAPI_WEBEmailCNT(E_MAIL, "", "30", "").Tables[0].Rows[0][0]);


            totalDues = "0";
            if (I_ILART.Trim() == "U02")  // added by ajay 20.01.2023
            {
                DataSet dsDues = new DataSet();
                dsDues = ZBAPI_FI_CA_TOTAL_DUES("BRPL", I_VKONT);
                for (int i = 0; i < dsDues.Tables[0].Rows.Count; i++)
                {
                    DuesAmount = Convert.ToDouble(dsDues.Tables[0].Rows[i]["BETRW"].ToString());
                    if (dsDues.Tables[0].Rows[i]["MESSAGE"].ToString() == "Enforcement Dues")
                    {
                        if (DuesAmount > 0)
                        {
                            totalDues = "1";
                        }

                    }
                }
            }


            string _sReqID = "";
            if (dt.Rows.Count > 0)
            {
                if (I_ILART.Trim() == "U01")
                    _sReqID = "N";
                else if (I_ILART.Trim() == "U02")
                    _sReqID = "O";
                else if (I_ILART.Trim() == "U03")
                    _sReqID = "L";
                else if (I_ILART.Trim() == "U04")
                    _sReqID = "L";
                else if (I_ILART.Trim() == "U05")
                    _sReqID = "C";
                else if (I_ILART.Trim() == "U06")
                    _sReqID = "C";
                else if (I_ILART.Trim() == "U07")
                    _sReqID = "A";

                _sSysDate = System.DateTime.Now.ToString("yyyyMMdd");

                if ((I_ILART.Trim() != "U01") && ABKRS.Trim() == "03")
                {
                    WORKCENTRE = GetDivisionCode(WORKCENTRE.Trim().ToUpper());
                }

                if (APPOINT_DIV.Trim() == "")
                    APPOINT_DIV = WORKCENTRE;

                if (APPLIEDLOAD.Trim() == "")
                {
                    if (APPLIEDLOADKVA.Trim() != "")
                    {
                        APPLIEDLOAD = APPLIEDLOADKVA.Trim();
                    }
                }

                int _iLoadCheck = 0;
                if (APPLIEDLOAD.Trim() == "")
                    APPLIEDLOAD = "0";
                if (APPLIEDLOADKVA.Trim() == "")
                    APPLIEDLOADKVA = "0";

                if (I_ILART.Trim() == "U03")
                {
                    _iLoadCheck = CheckRequest_LoadData(I_VKONT, "E", Convert.ToInt32(APPLIEDLOAD.Trim()), Convert.ToInt32(APPLIEDLOADKVA.Trim()));
                }
                else if (I_ILART.Trim() == "U04")
                {
                    _iLoadCheck = CheckRequest_LoadData(I_VKONT, "R", Convert.ToInt32(APPLIEDLOAD.Trim()), Convert.ToInt32(APPLIEDLOADKVA.Trim()));
                }


                if ((I_ILART.Trim() == "U01" || I_ILART.Trim() == "U03" || I_ILART.Trim() == "U04") && (APPLIEDLOAD.Trim() == "" || APPLIEDLOAD.Trim() == "0"))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Please Enter Applied Load!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if ((I_ILART.Trim() == "U01") && (HOUSE_NO.Trim().Length > 10))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "House No length should not be greater than 10 characters", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                //else if (_iLoadCheck != 0)
                //{
                //    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Applied Load should not be Less than " + _iLoadCheck, "", "000000", "", "", "", "", "", "0", "", "");
                //    SAPDATA_ErrorDataTable.AcceptChanges();
                //}
                else if (NewConnCount > 3)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "You requested more than 4 times, Please try again with another mobile number.", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (MOBILE.Trim().Length != 10)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Please Enter Correct Mobile Number!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (NewMailCount > 3)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "You requested more than 4 times, Please try again with another Email ID.", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (Holiday == 1)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Appointment not available on the given date. Please try another with another date", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if ((I_ILART.Trim() == "U02") && (totalDues == "1"))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Dear Customer, It is worth noting that there is an outstanding Enforcement dues (Amount Rs. " + DuesAmount.ToString() + ") found against your connection. Please pay the said outstanding bills to avoid any delays. Please ignore if already paid.", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if ((I_ILART.Trim() == "U02") && (E_O_AMT > 10))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", dsBAPIOutput.Tables[0].Rows[0]["E_O_MSG"].ToString(), "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if ((I_ILART.Trim() == "U02") && (Bp_type == "Tenant"))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Unable to register name change request as entered CA no. belongs to tenant connection.", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (((WORKCENTRE.Trim() == "S1NFC") || (WORKCENTRE.Trim() == "W1JFR") || (WORKCENTRE.Trim() == "W1NJF") ||
                    (WORKCENTRE.Trim() == "S1SVR") || (WORKCENTRE.Trim() == "W2VKP")) && (APPOINT_DIV.Trim() != WORKCENTRE.Trim()))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Not Allow to Take Request for Other Division!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (WORKCENTRE.Trim() != "")
                {
                    if (_sSysDate.Trim() == START_DATE.Trim())
                    {
                        _sAppType = "O" + _sReqID + WORKCENTRE.Trim().ToUpper().Substring(2, 3);
                    }
                    else
                    {
                        _sAppType = "A" + _sReqID + WORKCENTRE.Trim().ToUpper().Substring(2, 3);
                    }

                    _sNReqNo = _sAppType + dt.Rows[0][0].ToString();

                    DataTable _dtConsumer = new DataTable();
                    string _sName = string.Empty, NAME_FIRST = string.Empty, NAME_LAST = string.Empty, EXISTING_LOAD = string.Empty, BP_Num = string.Empty, TITLE1 = string.Empty;
                    string _sPlannerGrp = "DSS";

                    if (I_ILART.Trim() != "U01")
                    {
                        _dtConsumer = GetSAP_Data(I_VKONT.Trim().Replace("'", "''"));
                        if (_dtConsumer.Rows.Count > 0)
                        {
                            _sName = Convert.ToString(_dtConsumer.Rows[0]["Bp_Name"].ToString().Trim());
                            TITLE1 = Convert.ToString(_sName.Split(' ')[0].ToString());

                            if (I_ILART.Trim() == "U02")
                            {
                                //NAME_FIRST = Convert.ToString(_sName.Substring(TITLE1.Length, _sName.Length - TITLE1.Length)).Trim();
                                NAME_FIRST = _sName;
                                NAME_LAST = ".";
                            }
                            else
                            {
                                //NAME_FIRST = Convert.ToString(_sName.Substring(TITLE1.Length, _sName.Length - TITLE1.Length)).Trim();
                                NAME_FIRST = _sName;
                                NAME_LAST = ".";
                                //FIRSTNAME = Convert.ToString(_sName.Substring(TITLE1.Length, _sName.Length - TITLE1.Length)).Trim();
                                FIRSTNAME = _sName;
                                LASTNAME = ".";
                            }

                            MOBILE = Convert.ToString(_dtConsumer.Rows[0]["Tel1_Number"]);
                            _sPlannerGrp = Convert.ToString(_dtConsumer.Rows[0]["Search_Term2"]);
                            EXISTING_LOAD = Convert.ToString(_dtConsumer.Rows[0]["WERT1"]);
                            BP_Num = Convert.ToString(_dtConsumer.Rows[0]["BP_Number"]);

                            if (I_ILART.Trim() == "U07")
                            {

                            }
                            else
                            {
                                DataTable _dtAddress = new DataTable();
                                _dtAddress = GetSAP_Address(I_VKONT.Trim().Replace("'", "''"));
                                if (_dtAddress.Rows.Count > 0)
                                {
                                    HOUSE_NO = Convert.ToString(_dtAddress.Rows[0]["HOUSE_NO"]);
                                    BUILDING = Convert.ToString(_dtAddress.Rows[0]["STREET"]);
                                    STR_SUPPL1 = Convert.ToString(_dtAddress.Rows[0]["STR_SUPPL1"]);
                                    STR_SUPPL2 = Convert.ToString(_dtAddress.Rows[0]["STR_SUPPL2"]);
                                    STR_SUPPL3 = Convert.ToString(_dtAddress.Rows[0]["STR_SUPPL3"]);
                                    POSTL_COD1 = Convert.ToString(_dtAddress.Rows[0]["POSTL_COD1"]);
                                }
                                else
                                {
                                    HOUSE_NO = Convert.ToString(_dtConsumer.Rows[0]["House_Number"]);
                                    BUILDING = Convert.ToString(_dtConsumer.Rows[0]["Street"]);
                                    STR_SUPPL1 = Convert.ToString(_dtConsumer.Rows[0]["Street2"]);
                                    STR_SUPPL2 = Convert.ToString(_dtConsumer.Rows[0]["Street3"]);
                                    STR_SUPPL3 = Convert.ToString(_dtConsumer.Rows[0]["Street4"]);
                                    POSTL_COD1 = Convert.ToString(_dtConsumer.Rows[0]["Post_Code"]);
                                }
                            }
                        }
                    }
                    else
                    {
                        NAME_FIRST = FIRSTNAME.Trim().ToUpper().Replace("'", "''");
                        NAME_LAST = LASTNAME.Trim().ToUpper().Replace("'", "''");
                    }




                    if (I_ILART.Trim() != "U01")
                    {
                        DataTable DTCheckDup = new DataTable();
                        _sSql = "select * from ( SELECT *  FROM MOBAPP.SAP_SEVAKENDRA where ilart in ('" + I_ILART.Trim() + "') and WEBSITE_STATUS  in ('PAL0','DU','BTFR+CFR','CF/TF Not Required','CF','CFR','DEF_RUP','D','BTFR','DL','0','05','DR','PAL2','OLD','A','N','PTF','CTF_UP') and ZZ_VKONT = '" + I_VKONT.Trim() + "' and REQUEST_NO is not null order by ENTRY_DT desc ) where rownum=1";

                        DTCheckDup = dmlgetquery_Ebsdb(_sSql);

                        if (DTCheckDup.Rows.Count == 0)
                        {
                            sql.Append("insert into mobapp.sap_sevakendra(REQUEST_NO,AUFNR,ILART,ZZ_VKONT,AUART,TITLE,NAME_FIRST,NAME_LAST,NAME2,NAME1,NAMEMIDDLE,SECONDNAME,");
                            sql.Append(" XSEXU,HOUSE_NUM1,STREET,STR_SUPPL1,STR_SUPPL2,STR_SUPPL3,POST_CODE1,CITY_CODE,E_MAIL,TEL_NUMBER,TEL_EXTENS,ERDAT,BUKRS,VAPLZ, ");
                            sql.Append(" GSTRP,GSUZP,STRMN,STRUR,LTRMN,LTRUR, BPKIND,RUN_FOR_DATE,RUN_ON_DATE,FECOD,ZZ_RLOAD,ZZ_RLOADKVA,ZZ_CONNTYPE,");
                            sql.Append(" ENTRY_DT,FLAG,ACCOUNT_CLASS,REG_SOURCE,SAP_CREATED_BY,PLANNER_GROUP,ENTER_FRM_SRC, LANDLINE,I_VKONA, ");
                            sql.Append(" PARTNERCATEGORY,PARTNERTYPE,JOBGR,IDTYPE,IDNUMBER,PLANNINGPLANT,SYSTEMCOND,LATITUDE,LONGITUDE,GEOCOR_ADDRESS,APPOINT_DIV,EXISTING_LOAD,EXISTING_LOADKVA,KUNUM,MALE,FEMALE,NAME_AUTH_SIGNATORY) ");
                            sql.Append(" VALUES ");

                            sql.Append(" ('" + _sNReqNo + "','" + _sNReqNo + "' ,'" + I_ILART.Trim().Replace("'", "''") + "', '" + I_VKONT.Trim().Replace("'", "''") + "','ZDSS','" + TITLE + "','" + FIRSTNAME.Trim().ToUpper().Replace("'", "''") + "','" + LASTNAME.Trim().ToUpper().Replace("'", "''") + "',");
                            sql.Append(" '" + NAME_FIRST.Trim().ToUpper().Replace("'", "''") + "','" + NAME_LAST.Trim().ToUpper().Replace("'", "''") + "','" + MIDDLENAME.Trim().ToUpper().Replace("'", "''") + "','" + FATHERSNAME.Trim().ToUpper().Replace("'", "''") + "',");
                            sql.Append(" '" + SEX + "','" + HOUSE_NO.Trim().ToUpper().Replace("'", "''") + "','" + BUILDING.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL1.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL2.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL3.Trim().ToUpper().Replace("'", "''") + "',");
                            sql.Append("  '" + POSTL_COD1.Replace("'", "''") + "','" + CITY.Replace("'", "''") + "', '" + E_MAIL.Trim().Replace("'", "''") + "','" + MOBILE.Trim().Replace("'", "''") + "','SMS',to_char(sysdate,'yyyy-MM-dd'),'BRPL','" + WORKCENTRE.Trim().ToUpper().Replace("'", "''") + "', ");
                            sql.Append("  to_char(sysdate,'yyyy-MM-dd'),'" + START_TIME.Trim().Replace("'", "''") + "','" + _sStartDate.Trim().Replace("'", "''") + "','" + START_TIME.Trim().Replace("'", "''") + "','" + _sEndDate.Trim().Replace("'", "''") + "','" + FINISH_TIME.Trim().Replace("'", "''") + "',");
                            sql.Append(" '0010',to_char(sysdate,'dd.MM.yyyy'),to_char(sysdate,'dd.MM.yyyy'),'" + APPLIEDCAT.Trim().Replace("'", "''") + "','" + APPLIEDLOAD.Trim().Replace("'", "''") + "','" + APPLIEDLOADKVA.Trim().Replace("'", "''") + "',");
                            sql.Append("  '" + CONNECTIONTYPE.Trim() + "',sysdate, '0','" + STATEMENT_CA.Replace("'", "''") + "','" + ABKRS.Trim().Replace("'", "''") + "', ");
                            sql.Append(" 'DSK_NSER', '" + _sPlannerGrp + "','N','" + LANDLINE.Trim().Replace("'", "''") + "','" + I_VKONA.Trim().Replace("'", "''") + "','" + PARTNERCATEGORY.Trim().Replace("'", "''") + "' ,");
                            sql.Append(" '" + PARTNERTYPE.Trim().Replace("'", "''") + "','" + JOBGR.Trim().Replace("'", "''") + "','" + IDTYPE.Trim().Replace("'", "''") + "', ");
                            sql.Append("  '" + IDNUMBER.Trim().Replace("'", "''") + "','" + PLANNINGPLANT.Trim().Replace("'", "''") + "','" + SYSTEMCOND.Trim().Replace("'", "''") + "',");
                            sql.Append("  '" + LONGITUDE.Trim() + "','" + LATITUDE.Trim() + "','" + GEOCOR_ADDRESS.Trim().Replace("'", "''") + "','" + APPOINT_DIV.Trim() + "','" + EXISTING_LOAD.Trim() + "','" + EXISTING_LOAD.Trim() + "','" + BP_Num + "','" + MALE + "','" + FEMALE + "','" + BldgIndEstName + "') ");

                            if (dmlsinglequery(sql.ToString()) == true)
                            {
                                DataRow dr;
                                dr = outputFlagsTable.NewRow();
                                dr["E_Flag_Ap"] = "";
                                dr["E_Flag_Bp"] = "";
                                dr["E_Flag_So"] = "";
                                dr["E_Flag_Us"] = "";
                                dr["E_New_Partner"] = "";
                                dr["E_Service_Order"] = _sNReqNo;
                                outputFlagsTable.Rows.Add(dr);
                                outputFlagsTable.AcceptChanges();
                            }

                            if (APPOINT_DIV.Trim().ToString() != "")
                            {
                                _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                                _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + APPOINT_DIV + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                            }
                            else
                            {
                                _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                                _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + WORKCENTRE + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                            }

                            dmlsinglequery(_sSql.ToString());
                        }
                        else
                        {
                            //SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "The Ca number " + I_VKONT.Trim() + " is already in progress. After closing the previous request, you may take new request", "", "000000", "", "", "", "", "", "0", "", "");
                            SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Application No. " + DTCheckDup.Rows[0]["AUFNR"].ToString() + " is aleady in progress. Kindly close to generate a new request.", "", "000000", "", "", "", "", "", "0", "", "");
                            SAPDATA_ErrorDataTable.AcceptChanges();
                        }
                    }
                    else
                    {

                        sql.Append("insert into mobapp.sap_sevakendra(REQUEST_NO,AUFNR,ILART,ZZ_VKONT,AUART,TITLE,NAME_FIRST,NAME_LAST,NAME2,NAME1,NAMEMIDDLE,SECONDNAME,");
                        sql.Append(" XSEXU,HOUSE_NUM1,STREET,STR_SUPPL1,STR_SUPPL2,STR_SUPPL3,POST_CODE1,CITY_CODE,E_MAIL,TEL_NUMBER,TEL_EXTENS,ERDAT,BUKRS,VAPLZ, ");
                        sql.Append(" GSTRP,GSUZP,STRMN,STRUR,LTRMN,LTRUR, BPKIND,RUN_FOR_DATE,RUN_ON_DATE,FECOD,ZZ_RLOAD,ZZ_RLOADKVA,ZZ_CONNTYPE,");
                        sql.Append(" ENTRY_DT,FLAG,ACCOUNT_CLASS,REG_SOURCE,SAP_CREATED_BY,PLANNER_GROUP,ENTER_FRM_SRC, LANDLINE,I_VKONA, ");
                        sql.Append(" PARTNERCATEGORY,PARTNERTYPE,JOBGR,IDTYPE,IDNUMBER,PLANNINGPLANT,SYSTEMCOND,LATITUDE,LONGITUDE,GEOCOR_ADDRESS,APPOINT_DIV,EXISTING_LOAD,EXISTING_LOADKVA,KUNUM,MALE,FEMALE,NAME_AUTH_SIGNATORY) ");
                        sql.Append(" VALUES ");

                        sql.Append(" ('" + _sNReqNo + "','" + _sNReqNo + "' ,'" + I_ILART.Trim().Replace("'", "''") + "', '" + I_VKONT.Trim().Replace("'", "''") + "','ZDSS','" + TITLE + "','" + FIRSTNAME.Trim().ToUpper().Replace("'", "''") + "','" + LASTNAME.Trim().ToUpper().Replace("'", "''") + "',");
                        sql.Append(" '" + NAME_FIRST.Trim().ToUpper().Replace("'", "''") + "','" + NAME_LAST.Trim().ToUpper().Replace("'", "''") + "','" + MIDDLENAME.Trim().ToUpper().Replace("'", "''") + "','" + FATHERSNAME.Trim().ToUpper().Replace("'", "''") + "',");
                        sql.Append(" '" + SEX + "','" + HOUSE_NO.Trim().ToUpper().Replace("'", "''") + "','" + BUILDING.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL1.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL2.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL3.Trim().ToUpper().Replace("'", "''") + "',");
                        sql.Append("  '" + POSTL_COD1.Replace("'", "''") + "','" + CITY.Replace("'", "''") + "', '" + E_MAIL.Trim().Replace("'", "''") + "','" + MOBILE.Trim().Replace("'", "''") + "','SMS',to_char(sysdate,'yyyy-MM-dd'),'BRPL','" + WORKCENTRE.Trim().ToUpper().Replace("'", "''") + "', ");
                        sql.Append("  to_char(sysdate,'yyyy-MM-dd'),'" + START_TIME.Trim().Replace("'", "''") + "','" + _sStartDate.Trim().Replace("'", "''") + "','" + START_TIME.Trim().Replace("'", "''") + "','" + _sEndDate.Trim().Replace("'", "''") + "','" + FINISH_TIME.Trim().Replace("'", "''") + "',");
                        sql.Append(" '0010',to_char(sysdate,'dd.MM.yyyy'),to_char(sysdate,'dd.MM.yyyy'),'" + APPLIEDCAT.Trim().Replace("'", "''") + "','" + APPLIEDLOAD.Trim().Replace("'", "''") + "','" + APPLIEDLOADKVA.Trim().Replace("'", "''") + "',");
                        sql.Append("  '" + CONNECTIONTYPE.Trim() + "',sysdate, '0','" + STATEMENT_CA.Replace("'", "''") + "','" + ABKRS.Trim().Replace("'", "''") + "', ");
                        sql.Append(" 'DSK_NSER', 'DSS','N','" + LANDLINE.Trim().Replace("'", "''") + "','" + I_VKONA.Trim().Replace("'", "''") + "','" + PARTNERCATEGORY.Trim().Replace("'", "''") + "' ,");
                        sql.Append(" '" + PARTNERTYPE.Trim().Replace("'", "''") + "','" + JOBGR.Trim().Replace("'", "''") + "','" + IDTYPE.Trim().Replace("'", "''") + "', ");
                        sql.Append("  '" + IDNUMBER.Trim().Replace("'", "''") + "','" + PLANNINGPLANT.Trim().Replace("'", "''") + "','" + SYSTEMCOND.Trim().Replace("'", "''") + "',");
                        sql.Append("  '" + LONGITUDE.Trim() + "','" + LATITUDE.Trim() + "','" + GEOCOR_ADDRESS.Trim().Replace("'", "''") + "','" + APPOINT_DIV.Trim() + "','" + EXISTING_LOAD.Trim() + "','" + EXISTING_LOAD.Trim() + "','" + BP_Num + "','" + MALE + "','" + FEMALE + "','" + BldgIndEstName + "') ");

                        if (dmlsinglequery(sql.ToString()) == true)
                        {
                            DataRow dr;
                            dr = outputFlagsTable.NewRow();
                            dr["E_Flag_Ap"] = "";
                            dr["E_Flag_Bp"] = "";
                            dr["E_Flag_So"] = "";
                            dr["E_Flag_Us"] = "";
                            dr["E_New_Partner"] = "";
                            dr["E_Service_Order"] = _sNReqNo;
                            outputFlagsTable.Rows.Add(dr);
                            outputFlagsTable.AcceptChanges();
                        }

                        if (APPOINT_DIV.Trim().ToString() != "")
                        {
                            _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                            _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + APPOINT_DIV + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                        }
                        else
                        {
                            _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                            _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + WORKCENTRE + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                        }

                        dmlsinglequery(_sSql.ToString());
                    }
                    //SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "For appointment related queries, please contact our customer care no. 19122 / 19123 .", "", "000000", "", "", "", "", "", "0", "", "");
                    //SAPDATA_ErrorDataTable.AcceptChanges();
                }
            }
        }
        catch (Exception ex)
        {
            SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", ex.Message.ToString(), "", "000000", "", "", "", "", "", "0", "", "");
            SAPDATA_ErrorDataTable.AcceptChanges();
        }
        outputFlagsTable.TableName = "outputFlagsTable";
        SAPDATA_ErrorDataTable.TableName = "SAPDATA_ErrorDataTable";
        ErrorTable.TableName = "ErrorTable";
        messageTable.TableName = "messageTable";

        dsOrdInfo.Tables.Add(outputFlagsTable);
        dsOrdInfo.Tables.Add(SAPDATA_ErrorDataTable);
        dsOrdInfo.Tables.Add(ErrorTable);
        dsOrdInfo.Tables.Add(messageTable);

        outputFlagsTable.AcceptChanges();
        SAPDATA_ErrorDataTable.AcceptChanges();
        ErrorTable.AcceptChanges();
        messageTable.AcceptChanges();
        dsOrdInfo.DataSetName = "BAPI_RESULT";

        return dsOrdInfo;
    }

    [WebMethod]           //After Sanjeev added fields
    public DataSet Z_BAPI_ZDSS_WEB_LINK_CRM_WebSite(string I_ILART, string I_VKONT, string I_VKONA, string PARTNERCATEGORY, string PARTNERTYPE,
                    string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO,
                    string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, string CITY,
                    string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string IDTYPE,
                    string IDNUMBER, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string APPLIEDCAT, string APPLIEDLOAD,
                    string APPLIEDLOADKVA, string CONNECTIONTYPE, string STATEMENT_CA, string START_DATE, string START_TIME,
                    string FINISH_DATE, string FINISH_TIME, string SORTFIELD, string ABKRS, string LATITUDE, string LONGITUDE,
                    string GEOCOR_ADDRESS, string APPOINT_DIV, string stilt_parking, string lift_certificate, string bdo_certificate,
                    string internal_wiring, string email_services_required, string upic_available, string gstin, string PAN, string elcb_certificate,
                    string building_height_lt_15, string building_height_lt_17, string fcc_certificate, string ownership_proof_type, string floor,
                    string tempStartDate, string tempEndDate, string SupplyType, string buildingType, string industrialLicense, string industrialLicenseCertificate,
                    string hasLiftCertificate, string LICENSE_CRE_NO, string LICENSE_ISSU_DATE, string LICENSE_VALID_FRM, string LICENSE_VALID_TO,
                    string OPT_SUBSIDY, string PURCHASE_METER, string PURPOSE_SUPPLY, string HOUSENO2, string FLOOR2, string BUILDINGNAME2, string STREET2, string COLONY_AREA2,
                    string LANDMARK2, string LANDMARK2_DT, string PINCODE2, string INDICATE_LANDMARK, string HEIGHT_9MTR, string HEIGHT_12MTR, string NON_CONFIRM_AREA,
                    string dpccClearanceRequired, string hasTradeLicense, string tradeLicenseCertificate, string displayCategory,
                    string UPIC_NO, string FRIM_NAME, string NAME_AUTH_SIGNATORY, string DESG_SIGNATORY, string DOI, string TYPE_OF_ORG, string REGED_ADDRESS,
                    string CONSUMER_TYPE)
    {
        DataSet dsOrdInfo = new DataSet();

        DataTable outputFlagsTable = new DataTable();
        DataTable SAPDATA_ErrorDataTable = new DataTable();
        DataTable ErrorTable = new DataTable();
        DataTable messageTable = new DataTable();
        DataTable dt = new DataTable();
        DataTable DTHoliday = new DataTable();

        StringBuilder sql = new StringBuilder();
        string _sSql = string.Empty, TITLE = string.Empty, SEX = string.Empty, _sNReqNo = string.Empty, _sAppointDate = string.Empty;
        string _sSysDate = string.Empty, _sAppType = string.Empty, _sStartDate = string.Empty, _sEndDate = string.Empty, fnltempStartDate = string.Empty, fnlTempEndDate = string.Empty;
        string _LICENSE_ISSU_DATE = string.Empty, _LICENSE_VALID_FRM = string.Empty, _LICENSE_VALID_TO = string.Empty, Bp_type = string.Empty;

        double E_O_AMT = 0;
        DataSet dsBAPIOutput = new DataSet();

        int NewConnCount = 0, NewMailCount = 0, Holiday = 0;
        DataSet dsCNT = new DataSet();

        DateTime dateCheckWeekEnd = DateTime.Now;
        string CheckWeekEnd = string.Empty;

        if (START_DATE.Trim().Length == 8)
        {
            CheckWeekEnd = START_DATE.Substring(6, 2) + "/" + START_DATE.Substring(4, 2) + "/" + START_DATE.Substring(0, 4);
            dateCheckWeekEnd = DateTime.ParseExact(CheckWeekEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }



        if (I_ILART.Trim() == "")
            I_ILART = ".";
        if (I_VKONT.Trim() == "")
            I_VKONT = ".";
        if (I_VKONA.Trim() == "")
            I_VKONA = ".";
        if (FIRSTNAME.Trim() == "")
            FIRSTNAME = ".";
        if (LASTNAME.Trim() == "")
            LASTNAME = ".";
        if (MIDDLENAME.Trim() == "")
            MIDDLENAME = ".";
        if (FATHERSNAME.Trim() == "")
            FATHERSNAME = ".";
        if (HOUSE_NO.Trim() == "")
            HOUSE_NO = ".";
        if (BUILDING.Trim() == "")
            BUILDING = ".";
        if (STR_SUPPL1.Trim() == "")
            STR_SUPPL1 = ".";
        if (STR_SUPPL2.Trim() == "")
            STR_SUPPL2 = ".";
        if (STR_SUPPL3.Trim() == "")
            STR_SUPPL3 = ".";
        if (CITY.Trim() == "")
            CITY = ".";
        if (FEMALE.Trim() == "")
            FEMALE = ".";
        if (MALE.Trim() == "")
            MALE = ".";
        if (JOBGR.Trim() == "")
            JOBGR = ".";
        if (IDTYPE.Trim() == "")
            IDTYPE = ".";
        if (IDNUMBER.Trim() == "")
            IDNUMBER = ".";
        if (WORKCENTRE.Trim() == "")
            WORKCENTRE = ".";
        if (SYSTEMCOND.Trim() == "")
            SYSTEMCOND = ".";
        if (STATEMENT_CA.Trim() == "")
            STATEMENT_CA = ".";
        if (START_DATE.Trim() == "")
            START_DATE = ".";
        if (START_TIME.Trim() == "")
            START_TIME = ".";
        if (FINISH_DATE.Trim() == "")
            FINISH_DATE = ".";
        if (FINISH_TIME.Trim() == "")
            FINISH_TIME = ".";
        if (SORTFIELD.Trim() == "")
            SORTFIELD = ".";
        if (ABKRS.Trim() == "")
            ABKRS = ".";
        if (GEOCOR_ADDRESS.Trim() == "")
            GEOCOR_ADDRESS = ".";

        DataColumn column = new DataColumn();
        outputFlagsTable.Columns.Add("E_Flag_Ap", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_Bp", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_So", typeof(string));
        outputFlagsTable.Columns.Add("E_Flag_Us", typeof(string));
        outputFlagsTable.Columns.Add("E_New_Partner", typeof(string));
        outputFlagsTable.Columns.Add("E_Service_Order", typeof(string));

        SAPDATA_ErrorDataTable.Columns.Add("Type", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Id", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Number", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Log_No", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Log_Msg_No", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V1", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V2", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V3", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Message_V4", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Parameter", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Row", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("Field", typeof(string));
        SAPDATA_ErrorDataTable.Columns.Add("System", typeof(string));

        ErrorTable.Columns.Add("Data", typeof(string));

        messageTable.Columns.Add("messageCode", typeof(string));
        messageTable.Columns.Add("messageText", typeof(string));

        try
        {
            if (TITLE_KEY.Trim() == "0001")
                TITLE = "Ms";
            else if (TITLE_KEY.Trim() == "0002")
                TITLE = "Mr";
            else if (TITLE_KEY.Trim() == "0003")
                TITLE = "Dr";
            else if (TITLE_KEY.Trim() == "0004")
                TITLE = "Prof";
            else if (TITLE_KEY.Trim() == "0006")
                TITLE = "M/S";
            else
                TITLE = "Mr";


            if (MALE.Trim().ToUpper() == "X" || FEMALE.Trim().ToUpper() == "X")
                SEX = "X";

            if (START_DATE.Trim().Length == 8)
                _sStartDate = START_DATE.Substring(0, 4) + "-" + START_DATE.Substring(4, 2) + "-" + START_DATE.Substring(6, 2);
            if (FINISH_DATE.Trim().Length == 8)
                _sEndDate = FINISH_DATE.Substring(0, 4) + "-" + FINISH_DATE.Substring(4, 2) + "-" + FINISH_DATE.Substring(6, 2);

            if (START_DATE.Trim().Length == 8)
                _sAppointDate = START_DATE.Substring(6, 2) + "/" + START_DATE.Substring(4, 2) + "/" + START_DATE.Substring(0, 4);


            if (tempStartDate.Trim().Length == 8)
                fnltempStartDate = tempStartDate.Substring(0, 4) + "-" + tempStartDate.Substring(4, 2) + "-" + tempStartDate.Substring(6, 2);

            if (tempEndDate.Trim().Length == 8)
                fnlTempEndDate = tempEndDate.Substring(0, 4) + "-" + tempEndDate.Substring(4, 2) + "-" + tempEndDate.Substring(6, 2);

            if (LICENSE_ISSU_DATE.Trim().Length == 8)
                _LICENSE_ISSU_DATE = LICENSE_ISSU_DATE.Substring(0, 4) + "-" + LICENSE_ISSU_DATE.Substring(4, 2) + "-" + LICENSE_ISSU_DATE.Substring(6, 2);
            if (LICENSE_VALID_FRM.Trim().Length == 8)
                _LICENSE_VALID_FRM = LICENSE_VALID_FRM.Substring(0, 4) + "-" + LICENSE_VALID_FRM.Substring(4, 2) + "-" + LICENSE_VALID_FRM.Substring(6, 2);
            if (LICENSE_VALID_TO.Trim().Length == 8)
                _LICENSE_VALID_TO = LICENSE_VALID_TO.Substring(0, 4) + "-" + LICENSE_VALID_TO.Substring(4, 2) + "-" + LICENSE_VALID_TO.Substring(6, 2);

            //_sSql = "SELECT TO_CHAR(SYSDATE,'DDMMYY')|| LPAD(COUNT(1)+1,4,'0') FROM mobapp.sap_sevakendra WHERE SUBSTR(REQUEST_NO,6,6)=TO_CHAR(SYSDATE,'DDMMYY')";
            _sSql = "select to_char(sysdate,'DDMMYY')||lpad(MOBAPP.AUART_Seq.nextval,4,'0') from dual";

            dt = dmlgetquery_Ebsdb(_sSql);
            // dt = dmlgetquery(_sSql);

            _sSql = ""; // added by ajay
            _sSql = "select *  from mobapp.DSS_HOLIDAY_MST where (TO_CHAR (h_date, 'DD-MM-YYYY'))='" + _sStartDate + "'";
            DTHoliday = dmlgetquery_Ebsdb(_sSql);
            if (DTHoliday.Rows.Count > 0)
            {
                Holiday = 1;
            }


            DataTable dtBP_Type = new DataTable();
            string _sMoveOutData = string.Empty;
            if (I_ILART.Trim() != "U01")  // added by ajay 30.06.2022
            {
                dtBP_Type = GetSAP_Data(I_VKONT.Trim().Replace("'", "''"));
                if (dtBP_Type.Rows.Count > 0)
                {
                    Bp_type = dtBP_Type.Rows[0]["Bp_Type"].ToString();
                    _sMoveOutData = Convert.ToString(dtBP_Type.Rows[0]["Move_Out_Date"].ToString().Trim());
                }
            }

            if (I_ILART.Trim() == "U02")  // added by ajay 30.06.2022
            {
                dsBAPIOutput = obj.get_ZFI_CURR_OUTS_FLAG(I_VKONT);
                if (dsBAPIOutput.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(dsBAPIOutput.Tables[0].Rows[0]["E_O_AMT"]) != "")
                    {
                        E_O_AMT = Convert.ToDouble(dsBAPIOutput.Tables[0].Rows[0]["E_O_AMT"]);
                    }
                }
            }

            //dsCNT = ZBAPI_CS_MOBILE_APPCNT(MOBILE, "", "30", "");
            //NewConnCount = Convert.ToInt32(dsCNT.Tables[0].Rows[0][0]);
            NewConnCount = Convert.ToInt32(ZBAPI_WEBMobileCNT(MOBILE, "", "30", "").Tables[0].Rows[0][0]);
            NewMailCount = Convert.ToInt32(ZBAPI_WEBEmailCNT(E_MAIL, "", "30", "").Tables[0].Rows[0][0]);

            string _sReqID = "";
            if (dt.Rows.Count > 0)
            {
                if (I_ILART.Trim() == "U01")
                    _sReqID = "N";
                else if (I_ILART.Trim() == "U02")
                    _sReqID = "O";
                else if (I_ILART.Trim() == "U03")
                    _sReqID = "L";
                else if (I_ILART.Trim() == "U04")
                    _sReqID = "L";
                else if (I_ILART.Trim() == "U05")
                    _sReqID = "C";
                else if (I_ILART.Trim() == "U06")
                    _sReqID = "C";
                else if (I_ILART.Trim() == "U07")
                    _sReqID = "A";

                _sSysDate = System.DateTime.Now.ToString("yyyyMMdd");

                //if ((I_ILART.Trim() != "U01") && ABKRS.Trim() == "03")
                //{
                //    WORKCENTRE = GetDivisionCode(WORKCENTRE.Trim().ToUpper());
                //}

                if (APPOINT_DIV.Trim() == "")
                    APPOINT_DIV = WORKCENTRE;

                if (APPLIEDLOAD.Trim() == "")
                {
                    if (APPLIEDLOADKVA.Trim() != "")
                    {
                        APPLIEDLOAD = APPLIEDLOADKVA.Trim();
                    }
                }

                if (I_ILART.Trim() == "U02")
                {
                    CONNECTIONTYPE = "";
                    APPLIEDCAT = "";
                }

                if ((I_ILART.Trim() == "U01" || I_ILART.Trim() == "U03" || I_ILART.Trim() == "U04") && (APPLIEDLOAD.Trim() == "" || APPLIEDLOAD.Trim() == "0"))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Please Enter Applied Load!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if ((I_ILART.Trim() == "U01") && ((HOUSE_NO.Trim().Length > 10) || (HOUSENO2.Trim().Length > 10)))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "House No length should not be greater than 10 characters", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if ((I_ILART.Trim() != "U01") && (_sMoveOutData.Trim() == "00000000"))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Unable to registered request as the consumer is disconnected", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if ((I_ILART.Trim() == "U01") && (NewConnCount > 3))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "You requested more than 4 times, Please try again with another mobile number.", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (MOBILE.Trim().Length != 10)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Please Enter Correct Mobile Number!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if ((I_ILART.Trim() == "U02") && (E_O_AMT > 10))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", dsBAPIOutput.Tables[0].Rows[0]["E_O_MSG"].ToString(), "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if ((I_ILART.Trim() == "U02") && (Bp_type == "Tenant"))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Unable to register name change request as entered CA no. belongs to tenant connection.", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (NewConnCount > 3)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "You requested more than 4 times, Please try again with another mobile number.", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (NewMailCount > 3)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "You requested more than 4 times, Please try again with another Email ID.", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (Holiday == 1)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Appointment not available on the given date. Please try another with another date", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (((WORKCENTRE.Trim() == "S1NFC") || (WORKCENTRE.Trim() == "W1JFR") || (WORKCENTRE.Trim() == "W1NJF") ||
                    (WORKCENTRE.Trim() == "S1SVR") || (WORKCENTRE.Trim() == "W2VKP")) && (APPOINT_DIV.Trim() != WORKCENTRE.Trim()))
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Not Allow to Take Request for Other Division!", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (dateCheckWeekEnd.DayOfWeek == DayOfWeek.Saturday || dateCheckWeekEnd.DayOfWeek == DayOfWeek.Sunday)
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Appointment not available on the given date", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else if (WORKCENTRE.Trim() != "")
                {
                    if (_sSysDate.Trim() == START_DATE.Trim())
                    {
                        _sAppType = "O" + _sReqID + WORKCENTRE.Trim().ToUpper().Substring(2, 3);
                    }
                    else
                    {
                        _sAppType = "A" + _sReqID + WORKCENTRE.Trim().ToUpper().Substring(2, 3);
                    }

                    _sNReqNo = _sAppType + dt.Rows[0][0].ToString();

                    DataTable _dtConsumer = new DataTable();
                    string _sName = string.Empty, NAME_FIRST = string.Empty, NAME_LAST = string.Empty, EXISTING_LOAD = string.Empty, BP_Num = string.Empty, TITLE1 = string.Empty;
                    string _sPlannerGrp = "DSS";

                    if (I_ILART.Trim() != "U01")
                    {
                        _dtConsumer = GetSAP_Data(I_VKONT.Trim().Replace("'", "''"));
                        if (_dtConsumer.Rows.Count > 0)
                        {
                            _sName = Convert.ToString(_dtConsumer.Rows[0]["Bp_Name"].ToString().Trim());
                            TITLE1 = Convert.ToString(_sName.Split(' ')[0].ToString());

                            try
                            {
                                if (I_ILART.Trim() == "U02")
                                {
                                    //NAME_FIRST = Convert.ToString(_sName.Substring(TITLE1.Length, _sName.Length - TITLE1.Length)).Trim();
                                    NAME_FIRST = _sName;
                                    NAME_LAST = ".";
                                }
                                else
                                {
                                    //NAME_FIRST = Convert.ToString(_sName.Substring(TITLE1.Length, _sName.Length - TITLE1.Length)).Trim();
                                    NAME_FIRST = _sName;
                                    NAME_LAST = ".";
                                    //FIRSTNAME = Convert.ToString(_sName.Substring(TITLE1.Length, _sName.Length - TITLE1.Length)).Trim();
                                    FIRSTNAME = _sName;
                                    LASTNAME = ".";
                                }
                            }
                            catch
                            {
                                NAME_FIRST = _sName;
                            }

                            MOBILE = Convert.ToString(_dtConsumer.Rows[0]["Tel1_Number"]);
                            _sPlannerGrp = Convert.ToString(_dtConsumer.Rows[0]["Search_Term2"]);
                            EXISTING_LOAD = Convert.ToString(_dtConsumer.Rows[0]["WERT1"]);
                            BP_Num = Convert.ToString(_dtConsumer.Rows[0]["BP_Number"]);

                            if (I_ILART.Trim() == "U07")
                            {

                            }
                            else
                            {
                                DataTable _dtAddress = new DataTable();
                                _dtAddress = GetSAP_Address(I_VKONT.Trim().Replace("'", "''"));
                                if (_dtAddress.Rows.Count > 0)
                                {
                                    HOUSE_NO = Convert.ToString(_dtAddress.Rows[0]["HOUSE_NO"]);
                                    BUILDING = Convert.ToString(_dtAddress.Rows[0]["STREET"]);
                                    STR_SUPPL1 = Convert.ToString(_dtAddress.Rows[0]["STR_SUPPL1"]);
                                    STR_SUPPL2 = Convert.ToString(_dtAddress.Rows[0]["STR_SUPPL2"]);
                                    STR_SUPPL3 = Convert.ToString(_dtAddress.Rows[0]["STR_SUPPL3"]);
                                    POSTL_COD1 = Convert.ToString(_dtAddress.Rows[0]["POSTL_COD1"]);
                                }
                                else
                                {
                                    POSTL_COD1 = Convert.ToString(_dtConsumer.Rows[0]["Post_Code"]);
                                    BUILDING = Convert.ToString(_dtConsumer.Rows[0]["Street"]);
                                    HOUSE_NO = Convert.ToString(_dtConsumer.Rows[0]["House_Number"]);
                                    STR_SUPPL1 = Convert.ToString(_dtConsumer.Rows[0]["Street2"]);
                                    STR_SUPPL2 = Convert.ToString(_dtConsumer.Rows[0]["Street3"]);
                                    STR_SUPPL3 = Convert.ToString(_dtConsumer.Rows[0]["Street4"]);
                                }
                            }
                        }
                    }
                    else
                    {
                        NAME_FIRST = FIRSTNAME.Trim().ToUpper().Replace("'", "''");
                        NAME_LAST = LASTNAME.Trim().ToUpper().Replace("'", "''");
                    }




                    if (I_ILART.Trim() != "U01")
                    {
                        DataTable DTCheckDup = new DataTable();
                        _sSql = "SELECT *  FROM MOBAPP.SAP_SEVAKENDRA where ilart in ('" + I_ILART.Trim() + "') and WEBSITE_STATUS  in ('PAL0','DU','BTFR+CFR','CF/TF Not Required','CF','CFR','DEF_RUP','D','BTFR','DL','0','05','DR','PAL2','OLD','A','N','PTF','CTF_UP') and ZZ_VKONT = '" + I_VKONT.Trim() + "'";

                        DTCheckDup = dmlgetquery_Ebsdb(_sSql);

                        if (DTCheckDup.Rows.Count == 0)
                        {
                            sql.Append("insert into mobapp.sap_sevakendra(REQUEST_NO,AUFNR,ILART,ZZ_VKONT,AUART,TITLE,NAME_FIRST,NAME_LAST,NAME2,NAME1,NAMEMIDDLE,SECONDNAME,");
                            sql.Append(" XSEXU,HOUSE_NUM1,STREET,STR_SUPPL1,STR_SUPPL2,STR_SUPPL3,POST_CODE1,CITY_CODE,E_MAIL,TEL_NUMBER,TEL_EXTENS,ERDAT,BUKRS,VAPLZ, ");
                            sql.Append(" GSTRP,GSUZP,STRMN,STRUR,LTRMN,LTRUR, BPKIND,RUN_FOR_DATE,RUN_ON_DATE,FECOD,ZZ_RLOAD,ZZ_RLOADKVA,ZZ_CONNTYPE,");
                            sql.Append(" ENTRY_DT,FLAG,ACCOUNT_CLASS,REG_SOURCE,SAP_CREATED_BY,PLANNER_GROUP,ENTER_FRM_SRC, LANDLINE,I_VKONA, ");
                            sql.Append(" PARTNERCATEGORY,PARTNERTYPE,JOBGR,IDTYPE,IDNUMBER,PLANNINGPLANT,SYSTEMCOND,LATITUDE,LONGITUDE,GEOCOR_ADDRESS,APPOINT_DIV,EXISTING_LOAD,EXISTING_LOADKVA, ");
                            sql.Append(" KUNUM,stilt_parking, lift_certificate, bdo_certificate, internal_wiring, email_services_required, upic_available,gstin, PAN, ");
                            sql.Append(" elcb_certificate, building_height_lt_15, building_height_lt_17, fcc_certificate, ownership_proof_type, floor,SORTFIELD,start_Date,end_date, ");
                            sql.Append("  supplyType,buildingType,industrialLicense,industrialLicenseCertificate,hasLiftCertificate,LICENSE_CRE_NO,LICENSE_ISSU_DATE,LICENSE_VALID_FRM,");
                            sql.Append("  LICENSE_VALID_TO,OPT_SUBSIDY,PURCHASE_METER,PURPOSE_SUPPLY,HOUSENO2,FLOOR2,BUILDINGNAME2,STREET2,COLONY_AREA2,LANDMARK2,LANDMARK2_DT,PINCODE2,");
                            sql.Append("  INDICATE_LANDMARK,HEIGHT_9MTR,HEIGHT_12MTR,NON_CONFIRM_AREA,dpccClearanceRequired,hasTradeLicense,tradeLicenseCertificate,displayCategory,");
                            sql.Append("  UPIC_NO,  FRIM_NAME,  NAME_AUTH_SIGNATORY,  DESG_SIGNATORY,  DOI,  TYPE_OF_ORG,  REGED_ADDRESS,CONSUMER_TYPE) ");
                            sql.Append(" VALUES ");

                            sql.Append(" ('" + _sNReqNo + "','" + _sNReqNo + "' ,'" + I_ILART.Trim().Replace("'", "''") + "', '" + I_VKONT.Trim().Replace("'", "''") + "','ZDSS','" + TITLE + "','" + FIRSTNAME.Trim().ToUpper().Replace("'", "''") + "','" + LASTNAME.Trim().ToUpper().Replace("'", "''") + "',");
                            sql.Append(" '" + NAME_FIRST.Trim().ToUpper().Replace("'", "''") + "','" + NAME_LAST.Trim().ToUpper().Replace("'", "''") + "','" + MIDDLENAME.Trim().ToUpper().Replace("'", "''") + "','" + FATHERSNAME.Trim().ToUpper().Replace("'", "''") + "',");
                            sql.Append(" '" + SEX + "','" + HOUSE_NO.Trim().ToUpper().Replace("'", "''") + "','" + BUILDING.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL1.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL2.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL3.Trim().ToUpper().Replace("'", "''") + "',");
                            sql.Append("  '" + POSTL_COD1.Replace("'", "''") + "','" + CITY.Replace("'", "''") + "', '" + E_MAIL.Trim().Replace("'", "''") + "','" + MOBILE.Trim().Replace("'", "''") + "','SMS',to_char(sysdate,'yyyy-MM-dd'),'BRPL','" + WORKCENTRE.Trim().ToUpper().Replace("'", "''") + "', ");
                            sql.Append("  to_char(sysdate,'yyyy-MM-dd'),'" + START_TIME.Trim().Replace("'", "''") + "','" + _sStartDate.Trim().Replace("'", "''") + "','" + START_TIME.Trim().Replace("'", "''") + "','" + _sEndDate.Trim().Replace("'", "''") + "','" + FINISH_TIME.Trim().Replace("'", "''") + "',");
                            sql.Append(" '0010',to_char(sysdate,'dd.MM.yyyy'),to_char(sysdate,'dd.MM.yyyy'),'" + APPLIEDCAT.Trim().Replace("'", "''") + "','" + APPLIEDLOAD.Trim().Replace("'", "''") + "','" + APPLIEDLOADKVA.Trim().Replace("'", "''") + "',");
                            sql.Append("  '" + CONNECTIONTYPE.Trim() + "',sysdate, '0','" + STATEMENT_CA.Replace("'", "''") + "','" + ABKRS.Trim().Replace("'", "''") + "', ");
                            sql.Append(" 'DSK_NSER', '" + _sPlannerGrp + "','N','" + LANDLINE.Trim().Replace("'", "''") + "','" + I_VKONA.Trim().Replace("'", "''") + "','" + PARTNERCATEGORY.Trim().Replace("'", "''") + "' ,");
                            sql.Append(" '" + PARTNERTYPE.Trim().Replace("'", "''") + "','" + JOBGR.Trim().Replace("'", "''") + "','" + IDTYPE.Trim().Replace("'", "''") + "', ");
                            sql.Append("  '" + IDNUMBER.Trim().Replace("'", "''") + "','" + PLANNINGPLANT.Trim().Replace("'", "''") + "','" + SYSTEMCOND.Trim().Replace("'", "''") + "',");
                            sql.Append("  '" + LONGITUDE.Trim() + "','" + LATITUDE.Trim() + "','" + GEOCOR_ADDRESS.Trim().Replace("'", "''") + "','" + APPOINT_DIV.Trim() + "','" + EXISTING_LOAD.Trim() + "','" + EXISTING_LOAD.Trim() + "', ");
                            sql.Append("  '" + BP_Num + "','" + stilt_parking + "','" + lift_certificate + "','" + bdo_certificate + "','" + internal_wiring + "','" + email_services_required + "','" + upic_available + "','" + gstin + "','" + PAN + "', ");
                            sql.Append("  '" + elcb_certificate + "','" + building_height_lt_15 + "','" + building_height_lt_17 + "','" + fcc_certificate + "','" + ownership_proof_type + "','" + floor + "','" + SORTFIELD + "','" + fnltempStartDate.Trim().Replace("'", "''") + "','" + fnlTempEndDate.Trim().Replace("'", "''") + "', ");
                            sql.Append("  '" + SupplyType.Trim().Replace("'", "''") + "','" + buildingType + "','" + industrialLicense + "','" + industrialLicenseCertificate + "','" + hasLiftCertificate + "','" + LICENSE_CRE_NO + "', ");
                            sql.Append("  '" + _LICENSE_ISSU_DATE.Trim().Trim().Replace("'", "''") + "','" + _LICENSE_VALID_FRM.Trim().Trim().Replace("'", "''") + "','" + _LICENSE_VALID_TO.Trim().Trim().Replace("'", "''") + "','" + OPT_SUBSIDY + "','" + PURCHASE_METER + "','" + PURPOSE_SUPPLY + "','" + HOUSENO2 + "', ");
                            sql.Append("  '" + FLOOR2 + "','" + BUILDINGNAME2 + "','" + STREET2 + "','" + COLONY_AREA2 + "','" + LANDMARK2 + "','" + LANDMARK2_DT + "','" + PINCODE2 + "',");
                            sql.Append("  '" + INDICATE_LANDMARK + "','" + HEIGHT_9MTR + "' ,'" + HEIGHT_12MTR + "' ,'" + NON_CONFIRM_AREA + "' ,'" + dpccClearanceRequired + "' ,'" + hasTradeLicense + "' ,'" + tradeLicenseCertificate + "' ,'" + displayCategory + "',");
                            sql.Append("  '" + UPIC_NO + "','" + FRIM_NAME + "','" + NAME_AUTH_SIGNATORY + "','" + DESG_SIGNATORY + "','" + DOI + "','" + TYPE_OF_ORG + "','" + REGED_ADDRESS + "','" + CONSUMER_TYPE + "' ) ");

                            if (dmlsinglequery(sql.ToString()) == true)
                            {
                                DataRow dr;
                                dr = outputFlagsTable.NewRow();
                                dr["E_Flag_Ap"] = "";
                                dr["E_Flag_Bp"] = "";
                                dr["E_Flag_So"] = "";
                                dr["E_Flag_Us"] = "";
                                dr["E_New_Partner"] = "";
                                dr["E_Service_Order"] = _sNReqNo;
                                outputFlagsTable.Rows.Add(dr);
                                outputFlagsTable.AcceptChanges();
                            }

                            if (APPOINT_DIV.Trim().ToString() != "")
                            {
                                _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                                _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + APPOINT_DIV + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                            }
                            else
                            {
                                _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                                _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + WORKCENTRE + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                            }

                            dmlsinglequery(_sSql.ToString());
                        }
                        else
                        {
                            SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "The Ca number " + I_VKONT.Trim() + " is already in progress. After closing the previous request, you may take new request", "", "000000", "", "", "", "", "", "0", "", "");
                            SAPDATA_ErrorDataTable.AcceptChanges();
                        }
                    }
                    else
                    {
                        if (HOUSENO2.Trim() != "")
                        {
                            string _sLandmark = LANDMARK2 + " " + LANDMARK2_DT;

                            sql.Append("insert into mobapp.sap_sevakendra(REQUEST_NO,AUFNR,ILART,ZZ_VKONT,AUART,TITLE,NAME_FIRST,NAME_LAST,NAME2,NAME1,NAMEMIDDLE,SECONDNAME,");
                            sql.Append(" XSEXU,HOUSE_NUM1,STREET,STR_SUPPL1,STR_SUPPL2,STR_SUPPL3,POST_CODE1,CITY_CODE,E_MAIL,TEL_NUMBER,TEL_EXTENS,ERDAT,BUKRS,VAPLZ, ");
                            sql.Append(" GSTRP,GSUZP,STRMN,STRUR,LTRMN,LTRUR, BPKIND,RUN_FOR_DATE,RUN_ON_DATE,FECOD,ZZ_RLOAD,ZZ_RLOADKVA,ZZ_CONNTYPE,");
                            sql.Append(" ENTRY_DT,FLAG,ACCOUNT_CLASS,REG_SOURCE,SAP_CREATED_BY,PLANNER_GROUP,ENTER_FRM_SRC, LANDLINE,I_VKONA, ");
                            sql.Append(" PARTNERCATEGORY,PARTNERTYPE,JOBGR,IDTYPE,IDNUMBER,PLANNINGPLANT,SYSTEMCOND,LATITUDE,LONGITUDE,GEOCOR_ADDRESS,APPOINT_DIV,EXISTING_LOAD,EXISTING_LOADKVA, ");
                            sql.Append(" KUNUM,stilt_parking, lift_certificate, bdo_certificate, internal_wiring, email_services_required, upic_available,gstin, PAN, ");
                            sql.Append(" elcb_certificate, building_height_lt_15, building_height_lt_17, fcc_certificate, ownership_proof_type, floor,SORTFIELD,start_Date,end_date, ");
                            sql.Append("  supplyType,buildingType,industrialLicense,industrialLicenseCertificate,hasLiftCertificate,LICENSE_CRE_NO,LICENSE_ISSU_DATE,LICENSE_VALID_FRM,");
                            sql.Append("  LICENSE_VALID_TO,OPT_SUBSIDY,PURCHASE_METER,PURPOSE_SUPPLY,HOUSENO2,FLOOR2,BUILDINGNAME2,STREET2,COLONY_AREA2,LANDMARK2,LANDMARK2_DT,PINCODE2,");
                            sql.Append("  INDICATE_LANDMARK,HEIGHT_9MTR,HEIGHT_12MTR,NON_CONFIRM_AREA,dpccClearanceRequired,hasTradeLicense,tradeLicenseCertificate,displayCategory, ");
                            sql.Append("  UPIC_NO,  FRIM_NAME,  NAME_AUTH_SIGNATORY,  DESG_SIGNATORY,  DOI,  TYPE_OF_ORG,  REGED_ADDRESS,CONSUMER_TYPE) ");
                            sql.Append(" VALUES ");

                            sql.Append(" ('" + _sNReqNo + "','" + _sNReqNo + "' ,'" + I_ILART.Trim().Replace("'", "''") + "', '" + I_VKONT.Trim().Replace("'", "''") + "','ZDSS','" + TITLE + "','" + FIRSTNAME.Trim().ToUpper().Replace("'", "''") + "','" + LASTNAME.Trim().ToUpper().Replace("'", "''") + "',");
                            sql.Append(" '" + NAME_FIRST.Trim().ToUpper().Replace("'", "''") + "','" + NAME_LAST.Trim().ToUpper().Replace("'", "''") + "','" + MIDDLENAME.Trim().ToUpper().Replace("'", "''") + "','" + FATHERSNAME.Trim().ToUpper().Replace("'", "''") + "',");
                            sql.Append(" '" + SEX + "','" + HOUSENO2.Trim().ToUpper().Replace("'", "''") + "','" + BUILDINGNAME2.Trim().ToUpper().Replace("'", "''") + "','" + STREET2.Trim().ToUpper().Replace("'", "''") + "','" + COLONY_AREA2.Trim().ToUpper().Replace("'", "''") + "','" + _sLandmark.Trim().ToUpper().Replace("'", "''") + "',");
                            sql.Append("  '" + POSTL_COD1.Replace("'", "''") + "','" + CITY.Replace("'", "''") + "', '" + E_MAIL.Trim().Replace("'", "''") + "','" + MOBILE.Trim().Replace("'", "''") + "','SMS',to_char(sysdate,'yyyy-MM-dd'),'BRPL','" + WORKCENTRE.Trim().ToUpper().Replace("'", "''") + "', ");
                            sql.Append("  to_char(sysdate,'yyyy-MM-dd'),'" + START_TIME.Trim().Replace("'", "''") + "','" + _sStartDate.Trim().Replace("'", "''") + "','" + START_TIME.Trim().Replace("'", "''") + "','" + _sEndDate.Trim().Replace("'", "''") + "','" + FINISH_TIME.Trim().Replace("'", "''") + "',");
                            sql.Append(" '0010',to_char(sysdate,'dd.MM.yyyy'),to_char(sysdate,'dd.MM.yyyy'),'" + APPLIEDCAT.Trim().Replace("'", "''") + "','" + APPLIEDLOAD.Trim().Replace("'", "''") + "','" + APPLIEDLOADKVA.Trim().Replace("'", "''") + "',");
                            sql.Append("  '" + CONNECTIONTYPE.Trim() + "',sysdate, '0','" + STATEMENT_CA.Replace("'", "''") + "','" + ABKRS.Trim().Replace("'", "''") + "', ");
                            sql.Append(" 'DSK_NSER', 'DSS','N','" + LANDLINE.Trim().Replace("'", "''") + "','" + I_VKONA.Trim().Replace("'", "''") + "','" + PARTNERCATEGORY.Trim().Replace("'", "''") + "' ,");
                            sql.Append(" '" + PARTNERTYPE.Trim().Replace("'", "''") + "','" + JOBGR.Trim().Replace("'", "''") + "','" + IDTYPE.Trim().Replace("'", "''") + "', ");
                            sql.Append("  '" + IDNUMBER.Trim().Replace("'", "''") + "','" + PLANNINGPLANT.Trim().Replace("'", "''") + "','" + SYSTEMCOND.Trim().Replace("'", "''") + "',");
                            sql.Append("  '" + LONGITUDE.Trim() + "','" + LATITUDE.Trim() + "','" + GEOCOR_ADDRESS.Trim().Replace("'", "''") + "','" + APPOINT_DIV.Trim() + "','" + EXISTING_LOAD.Trim() + "','" + EXISTING_LOAD.Trim() + "', ");
                            sql.Append("  '" + BP_Num + "','" + stilt_parking + "','" + lift_certificate + "','" + bdo_certificate + "','" + internal_wiring + "','" + email_services_required + "','" + upic_available + "','" + gstin + "','" + PAN + "', ");
                            sql.Append("  '" + elcb_certificate + "','" + building_height_lt_15 + "','" + building_height_lt_17 + "','" + fcc_certificate + "','" + ownership_proof_type + "','" + floor + "','" + SORTFIELD + "','" + fnltempStartDate.Trim().Replace("'", "''") + "','" + fnlTempEndDate.Trim().Replace("'", "''") + "', ");
                            sql.Append("  '" + SupplyType.Trim().Replace("'", "''") + "','" + buildingType + "','" + industrialLicense + "','" + industrialLicenseCertificate + "','" + hasLiftCertificate + "','" + LICENSE_CRE_NO + "', ");
                            sql.Append("  '" + _LICENSE_ISSU_DATE.Trim().Trim().Replace("'", "''") + "','" + _LICENSE_VALID_FRM.Trim().Trim().Replace("'", "''") + "','" + _LICENSE_VALID_TO.Trim().Trim().Replace("'", "''") + "','" + OPT_SUBSIDY + "','" + PURCHASE_METER + "','" + PURPOSE_SUPPLY + "','" + HOUSE_NO.Trim().ToUpper().Replace("'", "''") + "', ");
                            sql.Append("  '" + FLOOR2 + "','" + BUILDING.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL1.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL2.Trim().ToUpper().Replace("'", "''") + "','" + LANDMARK2 + "','" + STR_SUPPL3.Trim().ToUpper().Replace("'", "''") + "','" + PINCODE2 + "',");
                            sql.Append("  '" + INDICATE_LANDMARK + "','" + HEIGHT_9MTR + "' ,'" + HEIGHT_12MTR + "' ,'" + NON_CONFIRM_AREA + "' ,'" + dpccClearanceRequired + "' ,'" + hasTradeLicense + "' ,'" + tradeLicenseCertificate + "' ,'" + displayCategory + "', ");
                            sql.Append("  '" + UPIC_NO + "','" + FRIM_NAME + "','" + NAME_AUTH_SIGNATORY + "','" + DESG_SIGNATORY + "','" + DOI + "','" + TYPE_OF_ORG + "','" + REGED_ADDRESS + "','" + CONSUMER_TYPE + "' ) ");
                        }
                        else
                        {
                            sql.Append("insert into mobapp.sap_sevakendra(REQUEST_NO,AUFNR,ILART,ZZ_VKONT,AUART,TITLE,NAME_FIRST,NAME_LAST,NAME2,NAME1,NAMEMIDDLE,SECONDNAME,");
                            sql.Append(" XSEXU,HOUSE_NUM1,STREET,STR_SUPPL1,STR_SUPPL2,STR_SUPPL3,POST_CODE1,CITY_CODE,E_MAIL,TEL_NUMBER,TEL_EXTENS,ERDAT,BUKRS,VAPLZ, ");
                            sql.Append(" GSTRP,GSUZP,STRMN,STRUR,LTRMN,LTRUR, BPKIND,RUN_FOR_DATE,RUN_ON_DATE,FECOD,ZZ_RLOAD,ZZ_RLOADKVA,ZZ_CONNTYPE,");
                            sql.Append(" ENTRY_DT,FLAG,ACCOUNT_CLASS,REG_SOURCE,SAP_CREATED_BY,PLANNER_GROUP,ENTER_FRM_SRC, LANDLINE,I_VKONA, ");
                            sql.Append(" PARTNERCATEGORY,PARTNERTYPE,JOBGR,IDTYPE,IDNUMBER,PLANNINGPLANT,SYSTEMCOND,LATITUDE,LONGITUDE,GEOCOR_ADDRESS,APPOINT_DIV,EXISTING_LOAD,EXISTING_LOADKVA, ");
                            sql.Append(" KUNUM,stilt_parking, lift_certificate, bdo_certificate, internal_wiring, email_services_required, upic_available,gstin, PAN, ");
                            sql.Append(" elcb_certificate, building_height_lt_15, building_height_lt_17, fcc_certificate, ownership_proof_type, floor,SORTFIELD,start_Date,end_date, ");
                            sql.Append("  supplyType,buildingType,industrialLicense,industrialLicenseCertificate,hasLiftCertificate,LICENSE_CRE_NO,LICENSE_ISSU_DATE,LICENSE_VALID_FRM,");
                            sql.Append("  LICENSE_VALID_TO,OPT_SUBSIDY,PURCHASE_METER,PURPOSE_SUPPLY,HOUSENO2,FLOOR2,BUILDINGNAME2,STREET2,COLONY_AREA2,LANDMARK2,LANDMARK2_DT,PINCODE2,");
                            sql.Append("  INDICATE_LANDMARK,HEIGHT_9MTR,HEIGHT_12MTR,NON_CONFIRM_AREA,dpccClearanceRequired,hasTradeLicense,tradeLicenseCertificate,displayCategory, ");
                            sql.Append("  UPIC_NO,  FRIM_NAME,  NAME_AUTH_SIGNATORY,  DESG_SIGNATORY,  DOI,  TYPE_OF_ORG,  REGED_ADDRESS,CONSUMER_TYPE) ");
                            sql.Append(" VALUES ");

                            sql.Append(" ('" + _sNReqNo + "','" + _sNReqNo + "' ,'" + I_ILART.Trim().Replace("'", "''") + "', '" + I_VKONT.Trim().Replace("'", "''") + "','ZDSS','" + TITLE + "','" + FIRSTNAME.Trim().ToUpper().Replace("'", "''") + "','" + LASTNAME.Trim().ToUpper().Replace("'", "''") + "',");
                            sql.Append(" '" + NAME_FIRST.Trim().ToUpper().Replace("'", "''") + "','" + NAME_LAST.Trim().ToUpper().Replace("'", "''") + "','" + MIDDLENAME.Trim().ToUpper().Replace("'", "''") + "','" + FATHERSNAME.Trim().ToUpper().Replace("'", "''") + "',");
                            sql.Append(" '" + SEX + "','" + HOUSE_NO.Trim().ToUpper().Replace("'", "''") + "','" + BUILDING.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL1.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL2.Trim().ToUpper().Replace("'", "''") + "','" + STR_SUPPL3.Trim().ToUpper().Replace("'", "''") + "',");
                            sql.Append("  '" + POSTL_COD1.Replace("'", "''") + "','" + CITY.Replace("'", "''") + "', '" + E_MAIL.Trim().Replace("'", "''") + "','" + MOBILE.Trim().Replace("'", "''") + "','SMS',to_char(sysdate,'yyyy-MM-dd'),'BRPL','" + WORKCENTRE.Trim().ToUpper().Replace("'", "''") + "', ");
                            sql.Append("  to_char(sysdate,'yyyy-MM-dd'),'" + START_TIME.Trim().Replace("'", "''") + "','" + _sStartDate.Trim().Replace("'", "''") + "','" + START_TIME.Trim().Replace("'", "''") + "','" + _sEndDate.Trim().Replace("'", "''") + "','" + FINISH_TIME.Trim().Replace("'", "''") + "',");
                            sql.Append(" '0010',to_char(sysdate,'dd.MM.yyyy'),to_char(sysdate,'dd.MM.yyyy'),'" + APPLIEDCAT.Trim().Replace("'", "''") + "','" + APPLIEDLOAD.Trim().Replace("'", "''") + "','" + APPLIEDLOADKVA.Trim().Replace("'", "''") + "',");
                            sql.Append("  '" + CONNECTIONTYPE.Trim() + "',sysdate, '0','" + STATEMENT_CA.Replace("'", "''") + "','" + ABKRS.Trim().Replace("'", "''") + "', ");
                            sql.Append(" 'DSK_NSER', 'DSS','N','" + LANDLINE.Trim().Replace("'", "''") + "','" + I_VKONA.Trim().Replace("'", "''") + "','" + PARTNERCATEGORY.Trim().Replace("'", "''") + "' ,");
                            sql.Append(" '" + PARTNERTYPE.Trim().Replace("'", "''") + "','" + JOBGR.Trim().Replace("'", "''") + "','" + IDTYPE.Trim().Replace("'", "''") + "', ");
                            sql.Append("  '" + IDNUMBER.Trim().Replace("'", "''") + "','" + PLANNINGPLANT.Trim().Replace("'", "''") + "','" + SYSTEMCOND.Trim().Replace("'", "''") + "',");
                            sql.Append("  '" + LONGITUDE.Trim() + "','" + LATITUDE.Trim() + "','" + GEOCOR_ADDRESS.Trim().Replace("'", "''") + "','" + APPOINT_DIV.Trim() + "','" + EXISTING_LOAD.Trim() + "','" + EXISTING_LOAD.Trim() + "', ");
                            sql.Append("  '" + BP_Num + "','" + stilt_parking + "','" + lift_certificate + "','" + bdo_certificate + "','" + internal_wiring + "','" + email_services_required + "','" + upic_available + "','" + gstin + "','" + PAN + "', ");
                            sql.Append("  '" + elcb_certificate + "','" + building_height_lt_15 + "','" + building_height_lt_17 + "','" + fcc_certificate + "','" + ownership_proof_type + "','" + floor + "','" + SORTFIELD + "','" + fnltempStartDate.Trim().Replace("'", "''") + "','" + fnlTempEndDate.Trim().Replace("'", "''") + "', ");
                            sql.Append("  '" + SupplyType.Trim().Replace("'", "''") + "','" + buildingType + "','" + industrialLicense + "','" + industrialLicenseCertificate + "','" + hasLiftCertificate + "','" + LICENSE_CRE_NO + "', ");
                            sql.Append("  '" + _LICENSE_ISSU_DATE.Trim().Trim().Replace("'", "''") + "','" + _LICENSE_VALID_FRM.Trim().Trim().Replace("'", "''") + "','" + _LICENSE_VALID_TO.Trim().Trim().Replace("'", "''") + "','" + OPT_SUBSIDY + "','" + PURCHASE_METER + "','" + PURPOSE_SUPPLY + "','" + HOUSENO2 + "', ");
                            sql.Append("  '" + FLOOR2 + "','" + BUILDINGNAME2 + "','" + STREET2 + "','" + COLONY_AREA2 + "','" + LANDMARK2 + "','" + LANDMARK2_DT + "','" + PINCODE2 + "',");
                            sql.Append("  '" + INDICATE_LANDMARK + "','" + HEIGHT_9MTR + "' ,'" + HEIGHT_12MTR + "' ,'" + NON_CONFIRM_AREA + "' ,'" + dpccClearanceRequired + "' ,'" + hasTradeLicense + "' ,'" + tradeLicenseCertificate + "' ,'" + displayCategory + "', ");
                            sql.Append("  '" + UPIC_NO + "','" + FRIM_NAME + "','" + NAME_AUTH_SIGNATORY + "','" + DESG_SIGNATORY + "','" + DOI + "','" + TYPE_OF_ORG + "','" + REGED_ADDRESS + "','" + CONSUMER_TYPE + "' ) ");
                        }

                        if (dmlsinglequery(sql.ToString()) == true)
                        {
                            DataRow dr;
                            dr = outputFlagsTable.NewRow();
                            dr["E_Flag_Ap"] = "";
                            dr["E_Flag_Bp"] = "";
                            dr["E_Flag_So"] = "";
                            dr["E_Flag_Us"] = "";
                            dr["E_New_Partner"] = "";
                            dr["E_Service_Order"] = _sNReqNo;
                            outputFlagsTable.Rows.Add(dr);
                            outputFlagsTable.AcceptChanges();
                        }

                        if (APPOINT_DIV.Trim().ToString() != "")
                        {
                            _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                            _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + APPOINT_DIV + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                        }
                        else
                        {
                            _sSql = " Insert into MOBAPP.DSK_APPOINTMENT_DATA(ORDER_TYPE,REQUEST_NO,MOBILE_NO, DIVISION, APP_SLOT, APP_SOURCE, APPOINTMENT_DATE) ";
                            _sSql += "  Values ('ZDSS', '" + _sNReqNo.Trim() + "','" + MOBILE.Trim() + "','" + WORKCENTRE + "', '" + START_TIME + "', 'M', TO_DATE('" + _sAppointDate + "','dd/MM/yyyy')) ";
                        }

                        dmlsinglequery(_sSql.ToString());
                    }
                    //SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "For appointment related queries, please contact our customer care no. 19122 / 19123 .", "", "000000", "", "", "", "", "", "0", "", "");
                    //SAPDATA_ErrorDataTable.AcceptChanges();
                }
                else // addded by ajay 10.09.2022
                {
                    SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", "Workcentre should not be blank !", "", "000000", "", "", "", "", "", "0", "", "");
                    SAPDATA_ErrorDataTable.AcceptChanges();
                }
            }
        }
        catch (Exception ex)
        {
            SAPDATA_ErrorDataTable.Rows.Add("E", "", "000", ex.Message.ToString(), "", "000000", "", "", "", "", "", "0", "", "");
            SAPDATA_ErrorDataTable.AcceptChanges();
        }
        outputFlagsTable.TableName = "outputFlagsTable";
        SAPDATA_ErrorDataTable.TableName = "SAPDATA_ErrorDataTable";
        ErrorTable.TableName = "ErrorTable";
        messageTable.TableName = "messageTable";

        dsOrdInfo.Tables.Add(outputFlagsTable);
        dsOrdInfo.Tables.Add(SAPDATA_ErrorDataTable);
        dsOrdInfo.Tables.Add(ErrorTable);
        dsOrdInfo.Tables.Add(messageTable);

        outputFlagsTable.AcceptChanges();
        SAPDATA_ErrorDataTable.AcceptChanges();
        ErrorTable.AcceptChanges();
        messageTable.AcceptChanges();
        dsOrdInfo.DataSetName = "BAPI_RESULT";

        return dsOrdInfo;
    }


    [WebMethod]    // Ajay file upload
    public DataSet getDownLoad_CCPUploadFile(string Ca_num, string DOCUMENT_TYPE, string DOCUMENT_NAME, string fileNameWithExtention, string ORDER_NO, string REQ_TYPE, string REQ_DATE, byte[] bytes)
    {
        //REQUEST_NO=RS240520220078,       DOCUMENT_TYPE=Photo_Proof , ID_Proof,    DOCUMENT_NAME= Aadhar Card No.,   fileName= 2022_1653364233195.jpg

        string NAS_Directory = @"\\10.125.64.236\UploadedImages"; //Local directory where the files will be downloaded
        string NAS_UserName = "uploadimages";
        string NAS_Password = "Bses@123";
        string DateYear = string.Empty;
        string DateMonth = string.Empty;
        DateTime DateFull = DateTime.Now;
        string _sPath = string.Empty;
        NetworkDrive oNetDrive = new NetworkDrive();
        oNetDrive.LocalDrive = "ZZ:";
        oNetDrive.ShareName = NAS_Directory;
        int map = 0;
        try
        {
            oNetDrive.MapDrive(NAS_UserName, NAS_Password);
        }
        catch (Exception exx)
        {
            map = 1;
        }
        if (map > 0)
        {
            oNetDrive.UnMapDrive();
            oNetDrive.MapDrive(NAS_UserName, NAS_Password);
        }

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataColumn column = new DataColumn();
        dt.Columns.Add("Code", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));



        if (string.IsNullOrEmpty(ORDER_NO))
        {

            if (string.IsNullOrEmpty(Ca_num))
            {
                dt.Rows.Add("02", "Ca no. cannot be null");
                ds.Tables.Add(dt);
                return ds;
            }
            if (string.IsNullOrEmpty(DOCUMENT_TYPE))
            {
                dt.Rows.Add("03", "DOCUMENT_TYPE cannot be null");
                ds.Tables.Add(dt);
                return ds;
            }
            if (string.IsNullOrEmpty(DOCUMENT_NAME))
            {
                dt.Rows.Add("04", "DOCUMENT_NAME cannot be null");
                ds.Tables.Add(dt);
                return ds;
            }
            if (string.IsNullOrEmpty(fileNameWithExtention))
            {

                dt.Rows.Add("04", "DOCUMENT_NAME cannot be null");
                ds.Tables.Add(dt);
                return ds;
            }
            else
            {
                string[] Ext = fileNameWithExtention.Split('.');
                if (Ext[1] == "")
                {
                    dt.Rows.Add("05", "File name must with its extention.");
                    ds.Tables.Add(dt);
                    return ds;
                }
            }
            if (string.IsNullOrEmpty(REQ_TYPE))
            {
                dt.Rows.Add("04", "Req Type cannot be null");
                ds.Tables.Add(dt);
                return ds;
            }
            if (string.IsNullOrEmpty(REQ_DATE))
            {
                dt.Rows.Add("04", "Req Date cannot be null");
                ds.Tables.Add(dt);
                return ds;
            }
            if (bytes.Length == 0)
            {
                dt.Rows.Add("06", "Send file in the format of bytes and cannot be null.");
                ds.Tables.Add(dt);
                return ds;

            }

        }
        else
        {
            if (string.IsNullOrEmpty(Ca_num))
            {
                dt.Rows.Add("07", " Kindly provide Request no with CA no.");
                ds.Tables.Add(dt);
                return ds;
            }
            if (string.IsNullOrEmpty(REQ_DATE))
            {
                dt.Rows.Add("07", " Kindly provide Req Date.");
                ds.Tables.Add(dt);
                return ds;
            }
        }


        try
        {
            Ca_num = Ca_num.Trim();
            DOCUMENT_TYPE = DOCUMENT_TYPE.Trim();
            DOCUMENT_NAME = DOCUMENT_NAME.Trim();
            fileNameWithExtention = fileNameWithExtention.Trim();

            if (string.IsNullOrEmpty(ORDER_NO))
            {


                DateYear = DateFull.ToString("yyyy");
                DateMonth = DateFull.ToString("MM");
                _sPath = "/" + DateYear + "/" + DateMonth + "/" + Ca_num;

                if (!Directory.Exists(NAS_Directory + "/" + _sPath + "/"))
                    Directory.CreateDirectory(NAS_Directory + "/" + _sPath + "/");


                //Save the Byte Array as File.
                string filePath = Path.Combine(NAS_Directory + "/" + _sPath + "/", fileNameWithExtention); /// Server.MapPath(string.Format("~/Uploads/{0}", fileName));
                File.WriteAllBytes(filePath, bytes);
                if (Insert_CC_DOCS_PATH(Ca_num, REQ_DATE, DOCUMENT_TYPE, DOCUMENT_NAME, _sPath + "/" + fileNameWithExtention, REQ_TYPE))
                {
                    dt.Rows.Add("00", "Success");
                    ds.Tables.Add(dt);
                }
                else
                {
                    dt.Rows.Add("08", "Failed!!!");
                    ds.Tables.Add(dt);
                }

            }
            else
            {
                ORDER_NO = ORDER_NO.Trim();
                if (Update_CC_DOCS_PATH(Ca_num, ORDER_NO))
                {
                    dt.Rows.Add("09", "Order no. updated Successfully");
                    ds.Tables.Add(dt);
                }
                else
                {
                    dt.Rows.Add("10", "Failed to update Order no. update Successfully");
                    ds.Tables.Add(dt);
                }

            }
        }
        catch (Exception EX)
        {
            dt.Rows.Add("01", EX.Message.ToString());
            ds.Tables.Add(dt);

        }
        finally
        {
            oNetDrive.UnMapDrive();
        }
        return ds;
    }
    [WebMethod]    // Ajay file upload
    public DataSet NewConnectionUploadFile(string REQUEST_NO, string DOCUMENT_TYPE, string DOCUMENT_NAME, string fileNameWithExtention, string ORDER_NO, byte[] bytes)
    {
        //REQUEST_NO=RS240520220078,       DOCUMENT_TYPE=Photo_Proof , ID_Proof,    DOCUMENT_NAME= Aadhar Card No.,   fileName= 2022_1653364233195.jpg

        string NAS_Directory = @"\\10.125.64.236\UploadedImages"; //Local directory where the files will be downloaded
        string NAS_UserName = "uploadimages";
        string NAS_Password = "Bses@123";
        string DateYear = string.Empty;
        string DateMonth = string.Empty;
        DateTime DateFull = DateTime.Now;
        string _sPath = string.Empty;
        NetworkDrive oNetDrive = new NetworkDrive();
        oNetDrive.LocalDrive = "ZZ:";
        oNetDrive.ShareName = NAS_Directory;
        int map = 0;
        try
        {
            oNetDrive.MapDrive(NAS_UserName, NAS_Password);
        }
        catch (Exception exx)
        {
            map = 1;
        }
        if (map > 0)
        {
            oNetDrive.UnMapDrive();
            oNetDrive.MapDrive(NAS_UserName, NAS_Password);
        }

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataColumn column = new DataColumn();
        dt.Columns.Add("Code", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));



        if (string.IsNullOrEmpty(ORDER_NO))
        {

            if (string.IsNullOrEmpty(REQUEST_NO))
            {
                dt.Rows.Add("02", "Request no. cannot be null");
                ds.Tables.Add(dt);
                return ds;
            }
            if (string.IsNullOrEmpty(DOCUMENT_TYPE))
            {
                dt.Rows.Add("03", "DOCUMENT_TYPE cannot be null");
                ds.Tables.Add(dt);
                return ds;
            }
            if (string.IsNullOrEmpty(DOCUMENT_NAME))
            {
                dt.Rows.Add("04", "DOCUMENT_NAME cannot be null");
                ds.Tables.Add(dt);
                return ds;
            }
            if (string.IsNullOrEmpty(fileNameWithExtention))
            {

                dt.Rows.Add("04", "DOCUMENT_NAME cannot be null");
                ds.Tables.Add(dt);
                return ds;
            }
            else
            {
                string[] Ext = fileNameWithExtention.Split('.');
                if (Ext[1] == "")
                {
                    dt.Rows.Add("05", "File name must with its extention.");
                    ds.Tables.Add(dt);
                    return ds;
                }
            }

            if (bytes.Length == 0)
            {
                dt.Rows.Add("06", "Send file in the format of bytes and cannot be null.");
                ds.Tables.Add(dt);
                return ds;

            }
        }
        else
        {
            if (string.IsNullOrEmpty(REQUEST_NO))
            {
                dt.Rows.Add("07", " Kindly provide Request no with order no.");
                ds.Tables.Add(dt);
                return ds;
            }

        }


        try
        {
            REQUEST_NO = REQUEST_NO.Trim();
            DOCUMENT_TYPE = DOCUMENT_TYPE.Trim();
            DOCUMENT_NAME = DOCUMENT_NAME.Trim();
            fileNameWithExtention = fileNameWithExtention.Trim();

            if (string.IsNullOrEmpty(ORDER_NO))
            {


                DateYear = DateFull.ToString("yyyy");
                DateMonth = DateFull.ToString("MM");
                _sPath = "/" + DateYear + "/" + DateMonth + "/" + REQUEST_NO;

                if (!Directory.Exists(NAS_Directory + "/" + _sPath + "/"))
                    Directory.CreateDirectory(NAS_Directory + "/" + _sPath + "/");


                //Save the Byte Array as File.
                string filePath = Path.Combine(NAS_Directory + "/" + _sPath + "/", fileNameWithExtention); /// Server.MapPath(string.Format("~/Uploads/{0}", fileName));
                File.WriteAllBytes(filePath, bytes);
                if (Insert_DSS_NEW_CON_DOCS_PATH(REQUEST_NO, DOCUMENT_TYPE, DOCUMENT_NAME, _sPath + "/" + fileNameWithExtention))
                {
                    dt.Rows.Add("00", "Success");
                    ds.Tables.Add(dt);
                }
                else
                {
                    dt.Rows.Add("08", "Failed!!!");
                    ds.Tables.Add(dt);
                }

            }
            else
            {
                ORDER_NO = ORDER_NO.Trim();
                if (Update_DSS_NEW_CON_DOCS_PATH(REQUEST_NO, ORDER_NO))
                {
                    dt.Rows.Add("09", "Order no. updated Successfully");
                    ds.Tables.Add(dt);
                }
                else
                {
                    dt.Rows.Add("10", "Failed to update Order no. update Successfully");
                    ds.Tables.Add(dt);
                }

            }
        }
        catch (Exception EX)
        {
            dt.Rows.Add("01", EX.Message.ToString());
            ds.Tables.Add(dt);

        }
        finally
        {
            oNetDrive.UnMapDrive();
        }
        return ds;
    }



    private bool Insert_DSS_NEW_CON_DOCS_PATH(string REQUEST_NO, string DOCUMENT_TYPE, string DOCUMENT_NAME, string DOCUMENT_PATH)
    {

        string sql = " insert into dcrep.DSS_NEW_CON_DOCS_PATH (REQUEST_NO,DOCUMENT_TYPE,DOCUMENT_NAME,DOCUMENT_PATH)values ";
        sql += "   ('" + REQUEST_NO + "', '" + DOCUMENT_TYPE + "', '" + DOCUMENT_NAME + "', '" + DOCUMENT_PATH + "')  ";

        return dml_singlequery_PCM(sql);
    }
    private bool Update_CC_DOCS_PATH(string CA_num, string OrderNo)
    {
        string sql = " update dcrep.DSS_CCP_DOCS_PATH set ORDER_NO='" + OrderNo + "' where CA_NUMBER='" + CA_num + "' and ORDER_NO is null ";

        return dml_singlequery_PCM(sql);
    }


    private bool Insert_CC_DOCS_PATH(string CA_num, string ReqDate, string DOCUMENT_TYPE, string DOCUMENT_NAME, string DOCUMENT_PATH, string REQ_TYPE)
    {

        string sql = " insert into dcrep.DSS_CCP_DOCS_PATH (CA_NUMBER,REQ_DATE,DOCUMENT_TYPE,DOCUMENT_NAME,DOCUMENT_PATH,REQ_TYPE) values";
        sql += "   ('" + CA_num + "', '" + ReqDate + "', '" + DOCUMENT_TYPE + "', '" + DOCUMENT_NAME + "', '" + DOCUMENT_PATH + "', '" + REQ_TYPE + "')  ";

        return dml_singlequery_PCM(sql);
    }
    private bool Update_DSS_NEW_CON_DOCS_PATH(string REQUEST_NO, string OrderNo)
    {
        string sql = " update dcrep.DSS_NEW_CON_DOCS_PATH set ORDER_NO='" + OrderNo + "' where REQUEST_NO='" + REQUEST_NO + "' and ORDER_NO is null ";

        return dml_singlequery_PCM(sql);
    }
    public bool dml_singlequery_PCM(string sql)
    {
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbConnection ocon = new OleDbConnection(objNDS.conPCM_New());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }

            dbcommand = new OleDbCommand(sql, ocon);
            //dbcommand.Transaction = dbtrans;
            dbcommand.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }

    [WebMethod]
    public DataTable UPDATE_NSERIRES_DATA(string CA_NUMBER)
    {
        DataTable dt = new DataTable();
        DataTable SapDT = new DataTable();
        string _sSql = string.Empty;
        string _sCaNo = string.Empty, _sName = string.Empty;
        string AUFNR = string.Empty, TITLE = string.Empty, NAME_FIRST = string.Empty, NAME_LAST = string.Empty, NAMEMIDDLE = string.Empty;
        string NAME1 = string.Empty, NAME2 = string.Empty;
        string TEL_NUMBER = string.Empty, POST_CODE1 = string.Empty, STREET = string.Empty, HOUSE_NUM1 = string.Empty, STR_SUPPL1 = string.Empty;
        string STR_SUPPL2 = string.Empty, STR_SUPPL3 = string.Empty, EXISTING_LOAD = string.Empty;

        _sSql = "select ZZ_VKONT,aufnr,ENTER_FRM_SRC,ilart from mobapp.sap_sevakendra where ilart !='U01' and bukrs='BRPL' and request_no is not null and WEBSITE_STATUS='DR' and ZZ_VKONT is not null  AND ENTER_FRM_SRC !='U' and request_no='OCVKJ2804221192'";
        //dt = dmlgetquery(_sSql);
        dt = dmlgetquery_Ebsdb(_sSql);

        if (dt.Rows.Count > 0)
        {
            DelhiWSV2.WebService obj = new DelhiWSV2.WebService();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    _sCaNo = Convert.ToInt64(dt.Rows[i][0].ToString()).ToString();
                }
                catch
                {
                    _sCaNo = "0";
                }

                try
                {
                    if (_sCaNo != "0")
                    {
                        if (dt.Rows[i][0].ToString().Substring(0, 6) == "000000")
                            _sCaNo = dt.Rows[i][0].ToString().Substring(3, 12);
                        else
                            _sCaNo = dt.Rows[i][0].ToString();
                    }

                    AUFNR = dt.Rows[i][1].ToString();
                    SapDT = obj.Z_BAPI_DSS_ISU_CA_DISPLAY(_sCaNo, "").Tables[0];

                    if (SapDT.Rows.Count > 0)
                    {
                        _sName = Convert.ToString(SapDT.Rows[0]["Bp_Name"].ToString().Trim());
                        TITLE = Convert.ToString(_sName.Split(' ')[0].ToString());

                        //if (_sName.Split(' ').Length > 6)
                        //{
                        //    NAME_FIRST = Convert.ToString(_sName.Split(' ')[2].ToString());
                        //    NAME_LAST = Convert.ToString(_sName.Split(' ')[6].ToString());
                        //    NAMEMIDDLE = Convert.ToString(_sName.Split(' ')[4].ToString());
                        //}
                        //else if (_sName.Split(' ').Length == 5)
                        //{
                        //    NAME_FIRST = Convert.ToString(_sName.Split(' ')[2].ToString());
                        //    NAME_LAST = Convert.ToString(_sName.Split(' ')[4].ToString());
                        //    NAMEMIDDLE = ".";
                        //}
                        //else
                        {
                            if (dt.Rows[i][3].ToString().Trim() == "U02")
                            {
                                NAME2 = Convert.ToString(_sName.Substring(TITLE.Length, _sName.Length - TITLE.Length)).Trim();
                            }
                            else
                            {
                                NAME_FIRST = Convert.ToString(_sName.Substring(TITLE.Length, _sName.Length - TITLE.Length)).Trim();
                                NAME_LAST = ".";
                                NAMEMIDDLE = ".";
                                NAME2 = Convert.ToString(_sName.Substring(TITLE.Length, _sName.Length - TITLE.Length)).Trim();
                            }
                        }

                        TEL_NUMBER = Convert.ToString(SapDT.Rows[0]["Tel1_Number"]);
                        POST_CODE1 = Convert.ToString(SapDT.Rows[0]["Post_Code"]);

                        STREET = Convert.ToString(SapDT.Rows[0]["Street"]);
                        HOUSE_NUM1 = Convert.ToString(SapDT.Rows[0]["House_Number"]);
                        STR_SUPPL1 = Convert.ToString(SapDT.Rows[0]["Street2"]);
                        STR_SUPPL2 = Convert.ToString(SapDT.Rows[0]["Street3"]);
                        STR_SUPPL3 = Convert.ToString(SapDT.Rows[0]["Street4"]);
                        EXISTING_LOAD = Convert.ToString(SapDT.Rows[0]["WERT1"]);

                        if (Update_NseriesData(AUFNR, TITLE, NAME_FIRST, NAME_LAST, NAMEMIDDLE, NAME1, NAME2, TEL_NUMBER, POST_CODE1, STREET, HOUSE_NUM1, STR_SUPPL1, STR_SUPPL2,
                                            STR_SUPPL3, EXISTING_LOAD, dt.Rows[i][3].ToString().Trim()) == true)
                        {
                        }
                        else
                        {
                            string abc = _sName;
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }
        return SapDT;
    }

    private bool Update_NseriesData(string _AUFNR, string _TITLE, string _NAME_FIRST, string _NAME_LAST, string _NAMEMIDDLE,
                                    string _NAME1, string _NAME2, string _TEL_NUMBER,
                                           string _POST_CODE1, string _STREET, string _HOUSE_NUM1, string _STR_SUPPL1, string _STR_SUPPL2,
                                           string _STR_SUPPL3, string _EXISTING_LOAD, string _sType)
    {
        string sql = string.Empty;

        if (_sType.Trim() == "U02")
        {
            sql = " update mobapp.sap_sevakendra set TITLE='" + _TITLE + "',NAME1='" + _NAME1 + "',NAME2='" + _NAME2 + "', ";
            sql += "   TEL_NUMBER='" + _TEL_NUMBER + "',POST_CODE1='" + _POST_CODE1 + "',STREET='" + _STREET + "',HOUSE_NUM1='" + _HOUSE_NUM1 + "',STR_SUPPL1='" + _STR_SUPPL1 + "',STR_SUPPL2='" + _STR_SUPPL2 + "',STR_SUPPL3='" + _STR_SUPPL3 + "',EXISTING_LOAD='" + _EXISTING_LOAD + "',EXISTING_LOADKVA='" + _EXISTING_LOAD + "', ";
            sql += "   ENTER_FRM_SRC='U'  where aufnr='" + _AUFNR + "'  ";
        }
        else
        {
            sql = " update mobapp.sap_sevakendra set TITLE='" + _TITLE + "',NAME_FIRST='" + _NAME_FIRST + "',NAME_LAST='" + _NAME_LAST + "',NAMEMIDDLE='" + _NAMEMIDDLE + "',NAME1='" + _NAME1 + "',NAME2='" + _NAME2 + "', ";
            sql += "   TEL_NUMBER='" + _TEL_NUMBER + "',POST_CODE1='" + _POST_CODE1 + "',STREET='" + _STREET + "',HOUSE_NUM1='" + _HOUSE_NUM1 + "',STR_SUPPL1='" + _STR_SUPPL1 + "',STR_SUPPL2='" + _STR_SUPPL2 + "',STR_SUPPL3='" + _STR_SUPPL3 + "',EXISTING_LOAD='" + _EXISTING_LOAD + "',EXISTING_LOADKVA='" + _EXISTING_LOAD + "', ";
            sql += "   ENTER_FRM_SRC='U'  where aufnr='" + _AUFNR + "'  ";
        }

        return dml_singlequery(sql);
    }


    [WebMethod]
    public DataSet ZBAPI_DM_NCON_OTTAB(string DATE)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            string _SPSTING_DATE = string.Empty, _PSTING_DATE = string.Empty, _sMonth = string.Empty;
            string _sPunchDate = string.Empty, _PunchDate = string.Empty, _sInputDate = string.Empty;
            string _sMtrInDate = string.Empty, _MtrInDate = string.Empty;
            string _sMoveInDate = string.Empty;

            // _sInputDate = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToString();

            //string _sSql = " select max(input_date) MAXDT from mobint.mcr_details_non_tab ";
            //dt = dmlMobintgetquery(_sSql);

            //for (int i = 3; i < 10; i++)
            //{
            //    DATE = "2022040" + i;
            //}

            //for (int j = 2; j < 10; j++)
            {
                // _ObjSer.ZBAPI_NEWCON_STATUS("D021", System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToString());
                //ds = obj.Get_ZBAPI_DM_NCON_OTTAB("2022060" + j);
                //DATE = "2022060" + j;

                //ds = obj.Get_ZBAPI_DM_NCON_OTTAB("20220818");
                //DATE = "20220818";

                ds = obj.Get_ZBAPI_DM_NCON_OTTAB(DATE);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtZBAPI = ds.Tables[0];
                    if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                    {
                        dt.Rows.Add("01", "Mis-Match Data in SAP");
                        ds.Tables.Add(dt);
                    }

                    if (dtZBAPI.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtZBAPI.Rows.Count; i++)
                        {
                            _SPSTING_DATE = Convert.ToString(dtZBAPI.Rows[i]["PSTING_DATE"]);
                            if (_SPSTING_DATE.Trim().Length == 10)
                            {
                                if (_SPSTING_DATE.Substring(5, 2).Trim() == "01")
                                    _sMonth = "Jan";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "02")
                                    _sMonth = "Feb";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "03")
                                    _sMonth = "Mar";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "04")
                                    _sMonth = "Apr";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "05")
                                    _sMonth = "May";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "06")
                                    _sMonth = "Jun";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "07")
                                    _sMonth = "Jul";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "08")
                                    _sMonth = "Aug";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "09")
                                    _sMonth = "Sep";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "10")
                                    _sMonth = "Oct";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "11")
                                    _sMonth = "Nov";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "12")
                                    _sMonth = "Dec";

                                _PSTING_DATE = _SPSTING_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _SPSTING_DATE.Substring(0, 4);
                            }

                            _sPunchDate = Convert.ToString(dtZBAPI.Rows[i]["PUNCH_DATE"]);
                            if (_sPunchDate.Trim().Length == 10)
                            {
                                if (_sPunchDate.Substring(5, 2).Trim() == "01")
                                    _sMonth = "Jan";
                                else if (_sPunchDate.Substring(5, 2).Trim() == "02")
                                    _sMonth = "Feb";
                                else if (_sPunchDate.Substring(5, 2).Trim() == "03")
                                    _sMonth = "Mar";
                                else if (_sPunchDate.Substring(5, 2).Trim() == "04")
                                    _sMonth = "Apr";
                                else if (_sPunchDate.Substring(5, 2).Trim() == "05")
                                    _sMonth = "May";
                                else if (_sPunchDate.Substring(5, 2).Trim() == "06")
                                    _sMonth = "Jun";
                                else if (_sPunchDate.Substring(5, 2).Trim() == "07")
                                    _sMonth = "Jul";
                                else if (_sPunchDate.Substring(5, 2).Trim() == "08")
                                    _sMonth = "Aug";
                                else if (_sPunchDate.Substring(5, 2).Trim() == "09")
                                    _sMonth = "Sep";
                                else if (_sPunchDate.Substring(5, 2).Trim() == "10")
                                    _sMonth = "Oct";
                                else if (_sPunchDate.Substring(5, 2).Trim() == "11")
                                    _sMonth = "Nov";
                                else if (_sPunchDate.Substring(5, 2).Trim() == "12")
                                    _sMonth = "Dec";

                                _PunchDate = _sPunchDate.Substring(8, 2) + "-" + _sMonth + "-" + _sPunchDate.Substring(0, 4);
                            }

                            _sMtrInDate = Convert.ToString(dtZBAPI.Rows[i]["INST_DATE"]);
                            if (_sMtrInDate.Trim().Length == 10)
                            {
                                if (_sMtrInDate.Substring(5, 2).Trim() == "01")
                                    _sMonth = "Jan";
                                else if (_sMtrInDate.Substring(5, 2).Trim() == "02")
                                    _sMonth = "Feb";
                                else if (_sMtrInDate.Substring(5, 2).Trim() == "03")
                                    _sMonth = "Mar";
                                else if (_sMtrInDate.Substring(5, 2).Trim() == "04")
                                    _sMonth = "Apr";
                                else if (_sMtrInDate.Substring(5, 2).Trim() == "05")
                                    _sMonth = "May";
                                else if (_sMtrInDate.Substring(5, 2).Trim() == "06")
                                    _sMonth = "Jun";
                                else if (_sMtrInDate.Substring(5, 2).Trim() == "07")
                                    _sMonth = "Jul";
                                else if (_sMtrInDate.Substring(5, 2).Trim() == "08")
                                    _sMonth = "Aug";
                                else if (_sMtrInDate.Substring(5, 2).Trim() == "09")
                                    _sMonth = "Sep";
                                else if (_sMtrInDate.Substring(5, 2).Trim() == "10")
                                    _sMonth = "Oct";
                                else if (_sMtrInDate.Substring(5, 2).Trim() == "11")
                                    _sMonth = "Nov";
                                else if (_sMtrInDate.Substring(5, 2).Trim() == "12")
                                    _sMonth = "Dec";

                                _MtrInDate = _sMtrInDate.Substring(8, 2) + "-" + _sMonth + "-" + _sMtrInDate.Substring(0, 4);
                            }

                            _sMoveInDate = Convert.ToString(dtZBAPI.Rows[i]["MOVE_IN_DATE"]);
                            if (_sMoveInDate.Trim().Length == 10)
                            {
                                if (_sMoveInDate.Substring(5, 2).Trim() == "01")
                                    _sMonth = "Jan";
                                else if (_sMoveInDate.Substring(5, 2).Trim() == "02")
                                    _sMonth = "Feb";
                                else if (_sMoveInDate.Substring(5, 2).Trim() == "03")
                                    _sMonth = "Mar";
                                else if (_sMoveInDate.Substring(5, 2).Trim() == "04")
                                    _sMonth = "Apr";
                                else if (_sMoveInDate.Substring(5, 2).Trim() == "05")
                                    _sMonth = "May";
                                else if (_sMoveInDate.Substring(5, 2).Trim() == "06")
                                    _sMonth = "Jun";
                                else if (_sMoveInDate.Substring(5, 2).Trim() == "07")
                                    _sMonth = "Jul";
                                else if (_sMoveInDate.Substring(5, 2).Trim() == "08")
                                    _sMonth = "Aug";
                                else if (_sMoveInDate.Substring(5, 2).Trim() == "09")
                                    _sMonth = "Sep";
                                else if (_sMoveInDate.Substring(5, 2).Trim() == "10")
                                    _sMonth = "Oct";
                                else if (_sMoveInDate.Substring(5, 2).Trim() == "11")
                                    _sMonth = "Nov";
                                else if (_sMoveInDate.Substring(5, 2).Trim() == "12")
                                    _sMonth = "Dec";

                                _sMoveInDate = _sMoveInDate.Substring(8, 2) + "-" + _sMonth + "-" + _sMoveInDate.Substring(0, 4);
                            }

                            if (DATE.Trim().Length == 8)
                            {
                                if (DATE.Substring(4, 2).Trim() == "01")
                                    _sMonth = "Jan";
                                else if (DATE.Substring(4, 2).Trim() == "02")
                                    _sMonth = "Feb";
                                else if (DATE.Substring(4, 2).Trim() == "03")
                                    _sMonth = "Mar";
                                else if (DATE.Substring(4, 2).Trim() == "04")
                                    _sMonth = "Apr";
                                else if (DATE.Substring(4, 2).Trim() == "05")
                                    _sMonth = "May";
                                else if (DATE.Substring(4, 2).Trim() == "06")
                                    _sMonth = "Jun";
                                else if (DATE.Substring(4, 2).Trim() == "07")
                                    _sMonth = "Jul";
                                else if (DATE.Substring(4, 2).Trim() == "08")
                                    _sMonth = "Aug";
                                else if (DATE.Substring(4, 2).Trim() == "09")
                                    _sMonth = "Sep";
                                else if (DATE.Substring(4, 2).Trim() == "10")
                                    _sMonth = "Oct";
                                else if (DATE.Substring(4, 2).Trim() == "11")
                                    _sMonth = "Nov";
                                else if (DATE.Substring(4, 2).Trim() == "12")
                                    _sMonth = "Dec";

                                _sInputDate = DATE.Substring(6, 2) + "-" + _sMonth + "-" + DATE.Substring(0, 4);
                            }


                            Insert_ZBAPI_DM_NCON_OTTAB(Convert.ToString(dtZBAPI.Rows[i]["DSS_ORDER"]), Convert.ToString(dtZBAPI.Rows[i]["ORDERID"]), _sInputDate, Convert.ToString(dtZBAPI.Rows[i]["COMP_CODE"]), Convert.ToString(dtZBAPI.Rows[i]["DIVISION"]),
                              Convert.ToString(dtZBAPI.Rows[i]["AUART"]), Convert.ToString(dtZBAPI.Rows[i]["ILART"]), Convert.ToString(dtZBAPI.Rows[i]["VENDOR_CODE"]), _PSTING_DATE,
                              Convert.ToString(dtZBAPI.Rows[i]["METER_NO"]), Convert.ToString(dtZBAPI.Rows[i]["CA_NO"]), Convert.ToString(dtZBAPI.Rows[i]["POLE_NO"]), Convert.ToString(dtZBAPI.Rows[i]["BP_NO"]),
                              Convert.ToString(dtZBAPI.Rows[i]["PLANNER_GROUP"]), Convert.ToString(dtZBAPI.Rows[i]["CABLE_SIZE"]), Convert.ToString(dtZBAPI.Rows[i]["CABLE_LENGTH"]), Convert.ToString(dtZBAPI.Rows[i]["KTOKL"]),
                              Convert.ToString(dtZBAPI.Rows[i]["ZZ_RLOAD"]), Convert.ToString(dtZBAPI.Rows[i]["SORT1"]), Convert.ToString(dtZBAPI.Rows[i]["ACTIVITY_REASON"]), Convert.ToString(dtZBAPI.Rows[i]["DRUM_NO"]),
                              Convert.ToString(dtZBAPI.Rows[i]["RL_FROM"]), Convert.ToString(dtZBAPI.Rows[i]["RL_TO"]), Convert.ToString(dtZBAPI.Rows[i]["BUS_BAR_SIZE"]), Convert.ToString(dtZBAPI.Rows[i]["BUS_CABLE_SIZE"]),
                              Convert.ToString(dtZBAPI.Rows[i]["TERMINAL_SEAL"]), Convert.ToString(dtZBAPI.Rows[i]["METERBOXSEAL1"]), Convert.ToString(dtZBAPI.Rows[i]["METERBOXSEAL2"]), Convert.ToString(dtZBAPI.Rows[i]["BUSBARSEAL1"]),
                              Convert.ToString(dtZBAPI.Rows[i]["BUSBARSEAL2"]), Convert.ToString(dtZBAPI.Rows[i]["TERMINAL_SEAL2"]), Convert.ToString(dtZBAPI.Rows[i]["OUTPUT_CABLE_LEN"]),
                              _PunchDate, _MtrInDate, Convert.ToString(dtZBAPI.Rows[i]["INSTALLATION"]), Convert.ToString(dtZBAPI.Rows[i]["CONTRACT"]), Convert.ToString(dtZBAPI.Rows[i]["METER_READ_UNIT"]), Convert.ToString(dtZBAPI.Rows[i]["CYC_CD"]),
                              Convert.ToString(dtZBAPI.Rows[i]["RATE_CATEGORY"]), Convert.ToString(dtZBAPI.Rows[i]["CONSUMER_TYPE"]), Convert.ToString(dtZBAPI.Rows[i]["RT_SEQ"]), Convert.ToString(dtZBAPI.Rows[i]["BILLING_CLASS"]),
                              _sMoveInDate, Convert.ToString(dtZBAPI.Rows[i]["HERST"]), Convert.ToString(dtZBAPI.Rows[i]["MF"]), Convert.ToString(dtZBAPI.Rows[i]["VOLTAGE_LEVEL"]),
                              Convert.ToString(dtZBAPI.Rows[i]["REV_DIST_CD"]), Convert.ToString(dtZBAPI.Rows[i]["NEARST_METER"]), Convert.ToString(dtZBAPI.Rows[i]["PROCESS_MODE"]), Convert.ToString(dtZBAPI.Rows[i]["EMAIL"]));

                        }

                    }
                }
            }

            // ds = obj.Get_ZBAPI_DM_NCON_OTTAB(DATE);



            //            else
            //{
            //    dt.Rows.Add("01", "Mis-Match Data");
            //    ds.Tables.Add(dt);
            //}

            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add("01", ex.Message.ToString());
            ds.Tables.Add(dt);
            return ds;
        }
    }


    private bool Insert_ZBAPI_DM_NCON_OTTAB(string _AUFNR, string _ORDERID, string _INPUT_DATE, string _COMP_CODE, string _DIVISION, string _AUART, string _ILART, string _VENDOR_CODE, string _PSTING_DATE, string _METER_NO, string _CA_NO, string _POLE_NO, string _BP_NO,
                                          string _PLANNER_GROUP, string _CABLE_SIZE, string _CABLE_LENGTH, string _KTOKL_ACCOUNT_CLASS, string _ZZ_RLOAD, string _SORT1, string _ACTIVITY_REASON, string _DRUM_NO, string _RL_FROM,
                                          string _RL_TO, string _BUS_BAR_SIZE, string _BUS_CABLE_SIZE, string _TERMINAL_SEAL, string _METERBOXSEAL1, string _METERBOXSEAL2, string _BUSBARSEAL1, string _BUSBARSEAL2, string _TERMINAL_SEAL2,
                                          string _OUTPUT_CABLE_LEN, string _TAB_PUNCHED_DATE, string _METER_INSTALLATION_DATE,
                                          string _INSTALLATION, string _CONTRACT, string _METER_READ_UNIT, string _CYC_CD, string _RATE_CATEGORY, string _CONSUMER_TYPE, string _RT_SEQ,
                                          string _BILLING_CLASS, string _MOVE_IN_DATE, string _HERST, string _MANUFACTURER_NAME, string _VOLTAGE_LEVEL, string _REV_DIST_CD, string _NEAREST_METER_NO,
                                          string _PROCESS_MODE, string _EMAIL)
    {

        string sql = " INSERT INTO MOBINT.mcr_details_non_tab(AUFNR,ORDERID,INPUT_DATE,COMP_CODE,DIVISION,AUART,ILART,VENDOR_CODE,PSTING_DATE,METER_NO,CA_NO,POLE_NO,BP_NO, ";
        sql += " PLANNER_GROUP,CABLE_SIZE,CABLE_LENGTH,KTOKL_ACCOUNT_CLASS,ZZ_RLOAD,SORT1,ACTIVITY_REASON,DRUM_NO,RL_FROM, ";
        sql += " RL_TO,BUS_BAR_SIZE,BUS_CABLE_SIZE,TERMINAL_SEAL,METERBOXSEAL1,METERBOXSEAL2,BUSBARSEAL1,BUSBARSEAL2,TERMINAL_SEAL2,OUTPUT_CABLE_LEN,SAP_PUNCHED_DATE,METER_INSTALLATION_DATE, ";
        sql += " INSTALLATION,CONTRACT,METER_READ_UNIT,CYC_CD,RATE_CATEGORY,CONSUMER_TYPE,RT_SEQ,BILLING_CLASS,MOVE_IN_DATE,HERST,MANUFACTURER_NAME,VOLTAGE_LEVEL,REV_DIST_CD,TF_METER_NO_3,PROCESS_MODE,EMAIL,MF) VALUES ";
        sql += "   ('" + _AUFNR + "', '" + _ORDERID + "', '" + _INPUT_DATE + "', '" + _COMP_CODE + "', '" + _DIVISION + "', '" + _AUART + "', '" + _ILART + "', ";
        sql += "     '" + _VENDOR_CODE + "',  TO_DATE('" + _PSTING_DATE + "'),  '" + _METER_NO + "',  '" + _CA_NO + "', '" + _POLE_NO + "',  '" + _BP_NO + "',  '" + _PLANNER_GROUP + "', '" + _CABLE_SIZE + "',  '" + _CABLE_LENGTH + "',  ";
        sql += "     '" + _KTOKL_ACCOUNT_CLASS + "', '" + _ZZ_RLOAD + "',  '" + _SORT1 + "',  '" + _ACTIVITY_REASON + "', '" + _DRUM_NO + "',  '" + _RL_FROM + "',  '" + _RL_TO + "', '" + _BUS_BAR_SIZE + "',  '" + _BUS_CABLE_SIZE + "',  ";
        sql += "     '" + _TERMINAL_SEAL + "', '" + _METERBOXSEAL1 + "',  '" + _METERBOXSEAL2 + "',  '" + _BUSBARSEAL1 + "', '" + _BUSBARSEAL2 + "',  '" + _TERMINAL_SEAL2 + "',  '" + _OUTPUT_CABLE_LEN + "',TO_DATE('" + _TAB_PUNCHED_DATE + "'),TO_DATE('" + _METER_INSTALLATION_DATE + "'), ";
        sql += "     '" + _INSTALLATION + "','" + _CONTRACT + "','" + _METER_READ_UNIT + "','" + _CYC_CD + "','" + _RATE_CATEGORY + "','" + _CONSUMER_TYPE + "','" + _RT_SEQ + "','" + _BILLING_CLASS + "',TO_DATE('" + _MOVE_IN_DATE + "'), ";
        sql += "    '" + _HERST + "','" + _HERST + "','" + _VOLTAGE_LEVEL + "','" + _REV_DIST_CD + "','" + _NEAREST_METER_NO + "','" + _PROCESS_MODE + "','" + _EMAIL + "','" + _MANUFACTURER_NAME + "' )  ";

        return dml_Mobintsinglequery(sql);
    }

    public bool dml_Mobintsinglequery(string sql)
    {
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbConnection ocon = new OleDbConnection(objNDS.Mobintcon());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }

            dbcommand = new OleDbCommand(sql, ocon);
            //dbcommand.Transaction = dbtrans;
            dbcommand.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }


    public DataTable dmlMobintgetquery(string sql)
    {
        DataTable dt = new DataTable();
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbDataAdapter da;

        OleDbConnection ocon = new OleDbConnection(objNDS.Mobintcon());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand = new OleDbCommand();
            dbcommand.Connection = ocon;
            da = new OleDbDataAdapter();
            da.SelectCommand = dbcommand;
            dt = null;
            dt = new DataTable();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandText = sql;
            //dbcommand.Transaction = dbtrans;
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return dt;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }



    [WebMethod]
    public DataSet ZBAPI_IDENTIFICATION(string _sPartner, string _sIDType, string _sIDNumber)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            ds = obj.Get_ZBAPI_IDENTIFICATION(_sPartner, _sIDType, _sIDNumber);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtZBAPI = ds.Tables[0];
                if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                {
                    dt.Rows.Add("01", "Mis-Match Data in SAP");
                    ds.Tables.Add(dt);
                }
            }
            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add("01", ex.Message.ToString());
            ds.Tables.Add(dt);
            return ds;
        }
    }


    [WebMethod]
    public DataSet ZBAPI_CS_GET_KIT_DETAILS(string _sCompany, string _sPostingDate)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));
        string _SPSTING_DATE = string.Empty, _PSTING_DATE = string.Empty;
        string _SCREATION_DATE = string.Empty, _CREATION_DATE = string.Empty;
        string _sBASIC_START_DATE = string.Empty, _BASIC_START_DATE = string.Empty;
        string _sBASIC_FINISH_DATE = string.Empty, _BASIC_FINISH_DATE = string.Empty;
        string _sMonth = string.Empty;

        try
        {
            ds = obj.Get_ZBAPI_CS_GET_KIT_DETAILS(_sCompany, _sPostingDate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtZBAPI = ds.Tables[0];
                if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                {
                    dt.Rows.Add("01", "Mis-Match Data in SAP");
                    ds.Tables.Add(dt);
                }

                for (int i = 0; i < dtZBAPI.Rows.Count; i++)
                {
                    _SPSTING_DATE = Convert.ToString(dtZBAPI.Rows[i]["PSTING_DATE"]);
                    if (_SPSTING_DATE.Trim().Length == 10)
                    {
                        if (_SPSTING_DATE.Substring(5, 2).Trim() == "01")
                            _sMonth = "Jan";
                        else if (_SPSTING_DATE.Substring(5, 2).Trim() == "02")
                            _sMonth = "Feb";
                        else if (_SPSTING_DATE.Substring(5, 2).Trim() == "03")
                            _sMonth = "Mar";
                        else if (_SPSTING_DATE.Substring(5, 2).Trim() == "04")
                            _sMonth = "Apr";
                        else if (_SPSTING_DATE.Substring(5, 2).Trim() == "05")
                            _sMonth = "May";
                        else if (_SPSTING_DATE.Substring(5, 2).Trim() == "06")
                            _sMonth = "Jun";
                        else if (_SPSTING_DATE.Substring(5, 2).Trim() == "07")
                            _sMonth = "Jul";
                        else if (_SPSTING_DATE.Substring(5, 2).Trim() == "08")
                            _sMonth = "Aug";
                        else if (_SPSTING_DATE.Substring(5, 2).Trim() == "09")
                            _sMonth = "Sep";
                        else if (_SPSTING_DATE.Substring(5, 2).Trim() == "10")
                            _sMonth = "Oct";
                        else if (_SPSTING_DATE.Substring(5, 2).Trim() == "11")
                            _sMonth = "Nov";
                        else if (_SPSTING_DATE.Substring(5, 2).Trim() == "12")
                            _sMonth = "Dec";

                        _PSTING_DATE = _SPSTING_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _SPSTING_DATE.Substring(0, 4);
                    }

                    _SCREATION_DATE = Convert.ToString(dtZBAPI.Rows[i]["CREATION_DATE"]);
                    if (_SCREATION_DATE.Trim().Length == 10)
                    {
                        if (_SCREATION_DATE.Substring(5, 2).Trim() == "01")
                            _sMonth = "Jan";
                        else if (_SCREATION_DATE.Substring(5, 2).Trim() == "02")
                            _sMonth = "Feb";
                        else if (_SCREATION_DATE.Substring(5, 2).Trim() == "03")
                            _sMonth = "Mar";
                        else if (_SCREATION_DATE.Substring(5, 2).Trim() == "04")
                            _sMonth = "Apr";
                        else if (_SCREATION_DATE.Substring(5, 2).Trim() == "05")
                            _sMonth = "May";
                        else if (_SCREATION_DATE.Substring(5, 2).Trim() == "06")
                            _sMonth = "Jun";
                        else if (_SCREATION_DATE.Substring(5, 2).Trim() == "07")
                            _sMonth = "Jul";
                        else if (_SCREATION_DATE.Substring(5, 2).Trim() == "08")
                            _sMonth = "Aug";
                        else if (_SCREATION_DATE.Substring(5, 2).Trim() == "09")
                            _sMonth = "Sep";
                        else if (_SCREATION_DATE.Substring(5, 2).Trim() == "10")
                            _sMonth = "Oct";
                        else if (_SCREATION_DATE.Substring(5, 2).Trim() == "11")
                            _sMonth = "Nov";
                        else if (_SCREATION_DATE.Substring(5, 2).Trim() == "12")
                            _sMonth = "Dec";

                        _CREATION_DATE = _SCREATION_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _SCREATION_DATE.Substring(0, 4);
                    }

                    _sBASIC_START_DATE = Convert.ToString(dtZBAPI.Rows[i]["BASIC_START_DATE"]);
                    if (_sBASIC_START_DATE.Trim().Length == 10)
                    {
                        if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "01")
                            _sMonth = "Jan";
                        else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "02")
                            _sMonth = "Feb";
                        else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "03")
                            _sMonth = "Mar";
                        else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "04")
                            _sMonth = "Apr";
                        else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "05")
                            _sMonth = "May";
                        else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "06")
                            _sMonth = "Jun";
                        else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "07")
                            _sMonth = "Jul";
                        else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "08")
                            _sMonth = "Aug";
                        else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "09")
                            _sMonth = "Sep";
                        else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "10")
                            _sMonth = "Oct";
                        else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "11")
                            _sMonth = "Nov";
                        else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "12")
                            _sMonth = "Dec";

                        _BASIC_START_DATE = _sBASIC_START_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _sBASIC_START_DATE.Substring(0, 4);
                    }

                    _sBASIC_FINISH_DATE = Convert.ToString(dtZBAPI.Rows[i]["BASIC_FINISH_DATE"]);
                    if (_sBASIC_FINISH_DATE.Trim().Length == 10)
                    {
                        if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "01")
                            _sMonth = "Jan";
                        else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "02")
                            _sMonth = "Feb";
                        else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "03")
                            _sMonth = "Mar";
                        else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "04")
                            _sMonth = "Apr";
                        else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "05")
                            _sMonth = "May";
                        else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "06")
                            _sMonth = "Jun";
                        else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "07")
                            _sMonth = "Jul";
                        else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "08")
                            _sMonth = "Aug";
                        else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "09")
                            _sMonth = "Sep";
                        else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "10")
                            _sMonth = "Oct";
                        else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "11")
                            _sMonth = "Nov";
                        else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "12")
                            _sMonth = "Dec";

                        _BASIC_FINISH_DATE = _sBASIC_FINISH_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _sBASIC_FINISH_DATE.Substring(0, 4);
                    }

                    Insert_ZBAPI_CS_GET_KIT_DETAILS(Convert.ToString(dtZBAPI.Rows[i]["COMP_CODE"]), _PSTING_DATE, Convert.ToString(dtZBAPI.Rows[i]["ORDERID"]), Convert.ToString(dtZBAPI.Rows[i]["GERNR"]), Convert.ToString(dtZBAPI.Rows[i]["CT_NO"]),
                              Convert.ToString(dtZBAPI.Rows[i]["REGIOGROUP"]), Convert.ToString(dtZBAPI.Rows[i]["KUNUM"]), Convert.ToString(dtZBAPI.Rows[i]["CA"]), _CREATION_DATE,
                              Convert.ToString(dtZBAPI.Rows[i]["NAME"]), Convert.ToString(dtZBAPI.Rows[i]["FATHER_NAME"]), Convert.ToString(dtZBAPI.Rows[i]["ADDRESS"]), Convert.ToString(dtZBAPI.Rows[i]["TEL_NO"]),
                              _BASIC_START_DATE, _BASIC_FINISH_DATE, Convert.ToString(dtZBAPI.Rows[i]["PLANNER_GROUP"]), Convert.ToString(dtZBAPI.Rows[i]["NEAREST_POLE_NO"]),
                              Convert.ToString(dtZBAPI.Rows[i]["CABLE_SIZE"]), Convert.ToString(dtZBAPI.Rows[i]["CABLE_DETAIL"]), Convert.ToString(dtZBAPI.Rows[i]["AUART"]), Convert.ToString(dtZBAPI.Rows[i]["KTOKL"]),
                              Convert.ToString(dtZBAPI.Rows[i]["ZZ_RLOAD"]), Convert.ToString(dtZBAPI.Rows[i]["ZDSS"]), Convert.ToString(dtZBAPI.Rows[i]["ILART"]), Convert.ToString(dtZBAPI.Rows[i]["SORT1"]),
                              Convert.ToString(dtZBAPI.Rows[i]["ZZSCODE"]), Convert.ToString(dtZBAPI.Rows[i]["ZZSEQUN"]), Convert.ToString(dtZBAPI.Rows[i]["ZZBUILDG"]), Convert.ToString(dtZBAPI.Rows[i]["ZZMCORO"]),
                              Convert.ToString(dtZBAPI.Rows[i]["SERNR"]), Convert.ToString(dtZBAPI.Rows[i]["CONTRACT_ST_DT"]), Convert.ToString(dtZBAPI.Rows[i]["CONTRACT_END_DT"]),
                              Convert.ToString(dtZBAPI.Rows[i]["PARNR"]), Convert.ToString(dtZBAPI.Rows[i]["KTEXT"]), Convert.ToString(dtZBAPI.Rows[i]["TARIFTYP"]), Convert.ToString(dtZBAPI.Rows[i]["SEAL_NO"]));
                }
            }
            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add("01", ex.Message.ToString());
            ds.Tables.Add(dt);
            return ds;
        }
    }


    private bool Insert_ZBAPI_CS_GET_KIT_DETAILS(string _COMP_CODE, string _PSTING_DATE, string _ORDERID, string _METER_NO, string _VENDOR_CODE, string _DIVISION, string _BP_NO, string _CA_NO, string _ERDAT, string _NAME, string _FATHER_NAME, string _ADDRESS, string _TEL_NO,
                                          string _START_DATE, string _FINISH_DATE, string _PLANNER_GROUP, string _POLE_NO, string _CABLE_SIZE, string _CABLE_LENGTH, string _AUART, string _ACCOUNT_CLASS,
                                          string _SANCTIONED_LOAD, string _ZDSS, string _ILART_ACTIVITY_TYPE, string _SORT1_SEARCH_TERM, string _ZZSCODE_SUB_AREA_CODE, string _ZZSEQUN_SEQUENCE_NUMBER, string _ZZBUILDG_BUILDING_CODE,
                                          string _ZZMCORO_METER_POSITION, string _OLD_METERNO_SERNR, string _CONTRACT_ST_DT, string _CONTRACT_END_DT, string _PARNR_USERRESPONSIBLE_ID,
                                          string _KTEXT_ORDERTEXT, string _TARIFTYP_TARIFFTYPE, string _SEAL_NO)
    {

        string sql = " INSERT INTO mcr_input_details_net (COMP_CODE,PSTING_DATE,ORDERID,METER_NO,VENDOR_CODE,DIVISION,BP_NO,CA_NO,ERDAT,NAME,FATHER_NAME,ADDRESS,TEL_NO, ";
        sql += " START_DATE,FINISH_DATE,PLANNER_GROUP,POLE_NO,CABLE_SIZE,CABLE_LENGTH,AUART,ACCOUNT_CLASS, ";
        sql += " SANCTIONED_LOAD,ZDSS,ILART_ACTIVITY_TYPE,SORT1_SEARCH_TERM,ZZSCODE_SUB_AREA_CODE,ZZSEQUN_SEQUENCE_NUMBER,ZZBUILDG_BUILDING_CODE, ";
        //sql += " ZZMCORO_METER_POSITION,OLD_METERNO_SERNR,CONTRACT_ST_DT,CONTRACT_END_DT,PARNR_USERRESPONSIBLE_ID, ";
        sql += " ZZMCORO_METER_POSITION,OLD_METERNO_SERNR,PARNR_USERRESPONSIBLE_ID, ";
        sql += " KTEXT_ORDERTEXT,TARIFTYP_TARIFFTYPE,SEAL_NO)VALUES ";

        sql += "   ('" + _COMP_CODE + "',  TO_DATE('" + _PSTING_DATE + "'), '" + _ORDERID + "', '" + _METER_NO + "', '" + _VENDOR_CODE + "', '" + _DIVISION + "', '" + _BP_NO + "', '" + _CA_NO + "', TO_DATE('" + _ERDAT + "'),'" + _NAME + "','" + _FATHER_NAME + "','" + _ADDRESS + "','" + _TEL_NO + "',";
        sql += "     TO_DATE('" + _START_DATE + "'),  TO_DATE('" + _FINISH_DATE + "'),  '" + _PLANNER_GROUP + "',  '" + _POLE_NO + "', '" + _CABLE_SIZE + "',  '" + _CABLE_LENGTH + "',  '" + _AUART + "', '" + _ACCOUNT_CLASS + "',  ";
        sql += "     '" + _SANCTIONED_LOAD + "', '" + _ZDSS + "',  '" + _ILART_ACTIVITY_TYPE + "',  '" + _SORT1_SEARCH_TERM + "', '" + _ZZSCODE_SUB_AREA_CODE + "',  '" + _ZZSEQUN_SEQUENCE_NUMBER + "',  '" + _ZZBUILDG_BUILDING_CODE + "',  ";
        sql += "     '" + _ZZMCORO_METER_POSITION + "', '" + _OLD_METERNO_SERNR + "', '" + _PARNR_USERRESPONSIBLE_ID + "',  '" + _KTEXT_ORDERTEXT + "',  '" + _TARIFTYP_TARIFFTYPE + "','" + _SEAL_NO + "')  ";

        return dml_Mobintsinglequery(sql);
    }


    [WebMethod]
    public DataSet ZBAPI_ORDER_COUNT(string _sOrderType, string _sPMActivity, string _sCompany, string _sDate)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));
        string _SCREATIONDATE = string.Empty, _CREATIONDATE = string.Empty;
        string _sMonth = string.Empty;

        try
        {
            ds = obj.Get_ZBAPI_ORDER_COUNT(_sOrderType, _sPMActivity, _sCompany, _sDate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtZBAPI = ds.Tables[0];
                if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                {
                    dt.Rows.Add("01", "Mis-Match Data in SAP");
                    ds.Tables.Add(dt);
                }

                for (int i = 0; i < dtZBAPI.Rows.Count; i++)
                {
                    _SCREATIONDATE = _sDate;
                    if (_SCREATIONDATE.Trim().Length == 8)
                    {
                        if (_SCREATIONDATE.Substring(4, 2).Trim() == "01")
                            _sMonth = "Jan";
                        else if (_SCREATIONDATE.Substring(4, 2).Trim() == "02")
                            _sMonth = "Feb";
                        else if (_SCREATIONDATE.Substring(4, 2).Trim() == "03")
                            _sMonth = "Mar";
                        else if (_SCREATIONDATE.Substring(4, 2).Trim() == "04")
                            _sMonth = "Apr";
                        else if (_SCREATIONDATE.Substring(4, 2).Trim() == "05")
                            _sMonth = "May";
                        else if (_SCREATIONDATE.Substring(4, 2).Trim() == "06")
                            _sMonth = "Jun";
                        else if (_SCREATIONDATE.Substring(4, 2).Trim() == "07")
                            _sMonth = "Jul";
                        else if (_SCREATIONDATE.Substring(4, 2).Trim() == "08")
                            _sMonth = "Aug";
                        else if (_SCREATIONDATE.Substring(4, 2).Trim() == "09")
                            _sMonth = "Sep";
                        else if (_SCREATIONDATE.Substring(4, 2).Trim() == "10")
                            _sMonth = "Oct";
                        else if (_SCREATIONDATE.Substring(4, 2).Trim() == "11")
                            _sMonth = "Nov";
                        else if (_SCREATIONDATE.Substring(4, 2).Trim() == "12")
                            _sMonth = "Dec";

                        _CREATIONDATE = _SCREATIONDATE.Substring(6, 2) + "-" + _sMonth + "-" + _SCREATIONDATE.Substring(0, 4);
                    }

                    Insert_ZBAPI_ORDER_COUNT(Convert.ToString(dtZBAPI.Rows[i]["ORDER_TYPE"]), Convert.ToString(dtZBAPI.Rows[i]["PM_ACTIVITY"]), Convert.ToString(dtZBAPI.Rows[i]["ORDER_COUNT"]),
                                            _sCompany, _CREATIONDATE);
                }

            }
            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add("01", ex.Message.ToString());
            ds.Tables.Add(dt);
            return ds;
        }
    }

    [WebMethod]  //Ajay 03.08.2022
    public DataSet getBillClass(string BillCode)
    {
        DataSet ds = new DataSet();
        DataTable _dt = new DataTable();
        string _sSql = string.Empty;

        DataTable DT_Bill = new DataTable();
        DataColumn column = new DataColumn();
        DT_Bill.Columns.Add("BILL_SCHEMA", typeof(string));
        DT_Bill.Columns.Add("BILL_DESC", typeof(string));

        _sSql = "select * from mobapp.Bill_Class where BILL_CODE='" + BillCode + "'";
        _dt = dmlgetquery_Ebsdb(_sSql);

        if (_dt.Rows.Count > 0)
        {
            DataRow dr;
            dr = DT_Bill.NewRow();

            dr["BILL_SCHEMA"] = _dt.Rows[0]["BILL_SCHEMA"].ToString();
            dr["BILL_DESC"] = _dt.Rows[0]["BILL_DESC"].ToString();


            DT_Bill.Rows.Add(dr);
            DT_Bill.AcceptChanges();

            ds.Tables.Add(DT_Bill);
            ds.Tables[0].TableName = "BillClassTable";
            ds.DataSetName = "Bill_RESULT";
        }
        else
        {
            DT_Bill.Rows.Add("No Record Found", "No Record Found");
            ds.Tables.Add(DT_Bill);

            ds.Tables[0].TableName = "BillClassTable";
            ds.DataSetName = "Bill_RESULT";
        }


        return ds;
    }

    private bool Insert_ZBAPI_ORDER_COUNT(string _ORDER_TYPE, string _PM_ACTIVITY, string _ORDER_COUNT, string _COMPANY, string _CREATION_DATE)
    {
        string sql = " INSERT INTO ZBAPI_ORDER_COUNT (ORDER_TYPE,PM_ACTIVITY,ORDER_COUNT,COMPANY,CREATION_DATE)VALUES ";
        sql += "   ('" + _ORDER_TYPE + "', '" + _PM_ACTIVITY + "', '" + _ORDER_COUNT + "', '" + _COMPANY + "', '" + _CREATION_DATE + "')  ";

        return dml_Mobintsinglequery(sql);
    }

    [WebMethod]
    public DataSet ZBAPI_ZDIN_STATUS(string _sOrderNo)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));
        string _SSTATUS_DATE = string.Empty, _STATUS_DATE = string.Empty;
        string _sMonth = string.Empty;

        try
        {
            ds = obj.Get_ZBAPI_ZDIN_STATUS(_sOrderNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtZBAPI = ds.Tables[0];
                if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                {
                    dt.Rows.Add("01", "Mis-Match Data in SAP");
                    ds.Tables.Add(dt);
                }

                for (int i = 0; i < dtZBAPI.Rows.Count; i++)
                {
                    _SSTATUS_DATE = Convert.ToString(dtZBAPI.Rows[i]["STATUS_DATE"]);
                    if (_SSTATUS_DATE.Trim().Length == 10)
                    {
                        if (_SSTATUS_DATE.Substring(5, 2).Trim() == "01")
                            _sMonth = "Jan";
                        else if (_SSTATUS_DATE.Substring(5, 2).Trim() == "02")
                            _sMonth = "Feb";
                        else if (_SSTATUS_DATE.Substring(5, 2).Trim() == "03")
                            _sMonth = "Mar";
                        else if (_SSTATUS_DATE.Substring(5, 2).Trim() == "04")
                            _sMonth = "Apr";
                        else if (_SSTATUS_DATE.Substring(5, 2).Trim() == "05")
                            _sMonth = "May";
                        else if (_SSTATUS_DATE.Substring(5, 2).Trim() == "06")
                            _sMonth = "Jun";
                        else if (_SSTATUS_DATE.Substring(5, 2).Trim() == "07")
                            _sMonth = "Jul";
                        else if (_SSTATUS_DATE.Substring(5, 2).Trim() == "08")
                            _sMonth = "Aug";
                        else if (_SSTATUS_DATE.Substring(5, 2).Trim() == "09")
                            _sMonth = "Sep";
                        else if (_SSTATUS_DATE.Substring(5, 2).Trim() == "10")
                            _sMonth = "Oct";
                        else if (_SSTATUS_DATE.Substring(5, 2).Trim() == "11")
                            _sMonth = "Nov";
                        else if (_SSTATUS_DATE.Substring(5, 2).Trim() == "12")
                            _sMonth = "Dec";

                        _STATUS_DATE = _SSTATUS_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _SSTATUS_DATE.Substring(0, 4);
                    }
                    Insert_ZBAPI_ZDIN_STATUS(Convert.ToString(dtZBAPI.Rows[i]["BP"]), Convert.ToString(dtZBAPI.Rows[i]["CA"]), Convert.ToString(dtZBAPI.Rows[i]["ZDSS_ORDER_NO"]), Convert.ToString(dtZBAPI.Rows[i]["ZNCO_ORDER_NO"]),
                              Convert.ToString(dtZBAPI.Rows[i]["ZDIN_ORDER_NO"]), Convert.ToString(dtZBAPI.Rows[i]["STATUS"]), _STATUS_DATE,
                              "");
                }
            }
            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add("01", ex.Message.ToString());
            ds.Tables.Add(dt);
            return ds;
        }
    }

    private bool Insert_ZBAPI_ZDIN_STATUS(string _BP_NUMBER, string _CA, string _ZDSS_ORD_NO, string _ZNCO_ORD_NO, string _ZDIN_ORD_NO,
                                        string _STATUS, string _STATUS_DATE, string _STATUS_DESC)
    {
        string sql = " INSERT INTO ZBAPI_ZDIN_STATUS (BP_NUMBER,CA,ZDSS_ORD_NO,ZNCO_ORD_NO,ZDIN_ORD_NO,STATUS,STATUS_DATE,STATUS_DESC)VALUES ";
        sql += "   ('" + _BP_NUMBER + "', '" + _CA + "', '" + _ZDSS_ORD_NO + "', '" + _ZNCO_ORD_NO + "', '" + _ZDIN_ORD_NO + "', '" + _STATUS + "',  TO_DATE('" + _STATUS_DATE + "'),'" + _STATUS_DESC + "')  ";

        return dml_Mobintsinglequery(sql);
    }

    [WebMethod]
    public DataSet ZBAPI_MCD_SEAL_UPD(string _sDIARY_NO, string _sDIVISION, string _sMCD_ZONE, string _sSDMC_LETTER_DTL, string _sRCVD_DT, string _sFIN_YR,
                  string _sPROPERTY_ADD1, string _sPROPERTY_ADD2, string _sPROPERTY_ADD3, string _sPROPERTY_ADD4, string _sPROPERTY_ADD5, string _sPROPERTY_ADD6,
                  string _sPROPERTY_ADD7, string _sPROPERTY_ADD8, string _sPROPERTY_ADD9, string _sPROPERTY_ADD10, string _sPROPERTY_ADD11,
                  string _sMTR_NO, string _sCA_NO, string _sACTIVITY_TYP, string _sSTATUS, string _sBIS, string _sCREATED_BY, string _sCREATED_ON,
                  string _sREMARKS)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            ds = obj.Get_ZBAPI_MCD_SEAL_UPD(_sDIARY_NO, _sDIVISION, _sMCD_ZONE, _sSDMC_LETTER_DTL, _sRCVD_DT, _sFIN_YR,
                     _sPROPERTY_ADD1, _sPROPERTY_ADD2, _sPROPERTY_ADD3, _sPROPERTY_ADD4, _sPROPERTY_ADD5, _sPROPERTY_ADD6,
                     _sPROPERTY_ADD7, _sPROPERTY_ADD8, _sPROPERTY_ADD9, _sPROPERTY_ADD10, _sPROPERTY_ADD11,
                     _sMTR_NO, _sCA_NO, _sACTIVITY_TYP, _sSTATUS, _sBIS, _sCREATED_BY, _sCREATED_ON, _sREMARKS);

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtZBAPI = ds.Tables[0];
                if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                {
                    dt.Rows.Add("01", "Mis-Match Data in SAP");
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add("01", ex.Message.ToString());
            ds.Tables.Add(dt);
            return ds;
        }
    }

    [WebMethod]  //Ajay 03.08.2022
    public DataSet LocalitySearch(string Key, string SearchKey)
    {

        DataSet ds = new DataSet();
        DataTable _dt = new DataTable();
        string _sSql = string.Empty;

        DataTable DT_Bill = new DataTable();
        DataColumn column = new DataColumn();
        DT_Bill.Columns.Add("BRPL_LOCALITY_NAME", typeof(string));
        DT_Bill.Columns.Add("DIVISION_NAME", typeof(string));
        DT_Bill.Columns.Add("COMPLAINT_CENTER_NAME", typeof(string));
        DT_Bill.Columns.Add("DIVISION_CODE", typeof(string));
        if (Key == "!!B$E$@@")
        {
            _sSql = "select BRPL_LOCALITY_NAME,DIVISION_NAME,COMPLAINT_CENTER_NAME,DIVISION_CODE from mobint.BRPL_LOCALITY_MST where upper(brpl_locality_name) like '%" + SearchKey.ToUpper() + "%'";
            _dt = dmlMobintgetquery(_sSql);

            if (_dt.Rows.Count > 0)
            {
                //DataRow dr;
                //dr = DT_Bill.NewRow();

                //dr["BILL_SCHEMA"] = _dt.Rows[0]["BILL_SCHEMA"].ToString();
                //dr["BILL_DESC"] = _dt.Rows[0]["BILL_DESC"].ToString();


                //DT_Bill.Rows.Add(dr);
                //DT_Bill.AcceptChanges();

                ds.Tables.Add(_dt);
                ds.Tables[0].TableName = "LocalityTable";
                ds.DataSetName = "Locality_RESULT";
            }
            else
            {
                DT_Bill.Rows.Add("No Record Found", "No Record Found", "", "");
                ds.Tables.Add(DT_Bill);

                ds.Tables[0].TableName = "LocalityTable";
                ds.DataSetName = "Locality_RESULT";
            }
        }
        else
        {
            DT_Bill.Rows.Add("Invalid Key", "Invalid Key", "", "");
            ds.Tables.Add(DT_Bill);

            ds.Tables[0].TableName = "LocalityTable";
            ds.DataSetName = "Locality_RESULT";
        }
        return ds;
    }

    #region Ebill_Prasoon_dated_16082022
    [WebMethod]
    public string ZBAPI_UPD_DISPATCH(string CA_NUMBER, string DISPATCH_CONTROL)
    {
        String Ds = obj.ZBAPI_UPD_DISPATCH(CA_NUMBER, DISPATCH_CONTROL);
        Insert_Service_Log(CA_NUMBER, null, DISPATCH_CONTROL, "", "", "", "", Ds);
        return Ds;
    }
    #endregion

    [WebMethod]
    public DataSet ZBAPI_FI_CA_TOTAL_DUES(string _sCompanyCode, string _sContractAcc)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            ds = obj.Get_ZBAPI_FI_CA_TOTAL_DUES(_sCompanyCode, _sContractAcc);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtZBAPI = ds.Tables[0];
                if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                {
                    dt.Rows.Add("01", "Mis-Match Data in SAP");
                    ds.Tables.Add(dt);
                }
            }
            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add("01", ex.Message.ToString());
            ds.Tables.Add(dt);
            return ds;
        }
    }


    #region"AMC-MMG Billing and Score Automation/Delhi Govt"

    [WebMethod]
    public DataSet DPIIT_NEW_CON_DATA(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string _sSql = string.Empty;
        DataTable DT_NewConn = new DataTable();
        DataColumn column = new DataColumn();
        DT_NewConn.Columns.Add("SN", typeof(string));
        DT_NewConn.Columns.Add("MonthYear", typeof(string));
        DT_NewConn.Columns.Add("REMARKS", typeof(string));
        DT_NewConn.Columns.Add("DATA", typeof(string));

        if (Key == "!!B$E$@@")
        {
            _sSql = " Select Rownum SN, MTH MonthYear, LAST_UPDATE, cast(a.TOTAL_APPLICATIONS as int) TOTAL_APPLICATIONS, cast(a.DN_GNRTD_U6 as int) DN_GNRTD_U7, ";
            _sSql += " cast(NVL(DN_GNRTD_B6_B, 0) as int) DN_GNRTD_B7,  ";
            _sSql += " cast(REJECTIONS as int) REJECTIONS, cast(UNDER_PROCESS as int) UNDER_PROCESS, ";
            _sSql += " cast(AVERAGE_DAYS as number) AVERAGE_DAYS, cast(MEDIAN_DAYS as number) MEDIAN_DAYS from DAM.DAILY_NEW_CONNECTION_DOP_SUMMARY_DN a  Where company = 'BRPL' ";
            _sSql += " union ";
            _sSql += " Select 99 SN, 'Grand Total' MonthYear, '' LAST_UPDATE, Sum(a.TOTAL_APPLICATIONS), Sum(a.DN_GNRTD_U6) DN_GNRTD_U7, ";
            _sSql += " Sum(NVL(DN_GNRTD_B6_B, 0)) DN_GNRTD_B7,  Sum(REJECTIONS), Sum(UNDER_PROCESS), sum(cast(AVERAGE_DAYS as number)), sum(cast(MEDIAN_DAYS as number)) ";
            _sSql += " from DAM.DAILY_NEW_CONNECTION_DOP_SUMMARY_DN a  Where company = 'BRPL'  ";

            dt = dmlDAMgetquery(_sSql);

            if (dt.Rows.Count > 0)
            {
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = "NewConnData";
                ds.DataSetName = "NewConn_RESULT";
            }
            else
            {
                DT_NewConn.Rows.Add("1", "No Record Found", "No Record Found", "");
                ds.Tables.Add(DT_NewConn);

                ds.Tables[0].TableName = "NewConnData";
                ds.DataSetName = "NewConn_RESULT";
            }
        }
        else
        {
            DT_NewConn.Rows.Add("1", "Invalid Key", "Invalid Key", "");
            ds.Tables.Add(DT_NewConn);

            ds.Tables[0].TableName = "NewConnData";
            ds.DataSetName = "NewConn_RESULT";
        }
        return ds;
    }

    public DataTable dmlDAMgetquery(string sql)
    {
        DataTable dt = new DataTable();
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbDataAdapter da;

        OleDbConnection ocon = new OleDbConnection(objNDS.Mobintcon());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand = new OleDbCommand();
            dbcommand.Connection = ocon;
            da = new OleDbDataAdapter();
            da.SelectCommand = dbcommand;
            dt = null;
            dt = new DataTable();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandText = sql;
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return dt;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }


    //[WebMethod]
    //public DataSet ZBAPI_DM_SLA_DATA()
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    DataTable dtZBAPI = new DataTable();
    //    DataColumn column = new DataColumn();
    //    dt.Columns.Add("INFO", typeof(string));
    //    dt.Columns.Add("MESSAGE", typeof(string));

    //    try
    //    {
    //        string _SPSTING_DATE = string.Empty, _PSTING_DATE = string.Empty, _sMonth = string.Empty;
    //        string _sPunchDate = string.Empty, _PunchDate = string.Empty, _sInputDate = string.Empty;
    //        string _sMtrInDate = string.Empty, _MtrInDate = string.Empty;
    //        _sInputDate = System.DateTime.Now.AddDays(-12).ToString("yyyyMMdd").ToString();

    //        //for (int j = 2; j < 10; j++)
    //        {
    //            ds = obj.Get_ZBAPI_DM_SLA_DATA(_sInputDate);

    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                dtZBAPI = ds.Tables[0];
    //                if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
    //                {
    //                    dt.Rows.Add("01", "Mis-Match Data in SAP");
    //                    ds.Tables.Add(dt);
    //                }

    //                if (dtZBAPI.Rows.Count > 0)
    //                {
    //                    for (int i = 0; i < dtZBAPI.Rows.Count; i++)
    //                    {
    //                        _SPSTING_DATE = Convert.ToString(dtZBAPI.Rows[i]["PSTING_DATE"]);
    //                        if (_SPSTING_DATE.Trim().Length == 10)
    //                        {
    //                            if (_SPSTING_DATE.Substring(5, 2).Trim() == "01")
    //                                _sMonth = "Jan";
    //                            else if (_SPSTING_DATE.Substring(5, 2).Trim() == "02")
    //                                _sMonth = "Feb";
    //                            else if (_SPSTING_DATE.Substring(5, 2).Trim() == "03")
    //                                _sMonth = "Mar";
    //                            else if (_SPSTING_DATE.Substring(5, 2).Trim() == "04")
    //                                _sMonth = "Apr";
    //                            else if (_SPSTING_DATE.Substring(5, 2).Trim() == "05")
    //                                _sMonth = "May";
    //                            else if (_SPSTING_DATE.Substring(5, 2).Trim() == "06")
    //                                _sMonth = "Jun";
    //                            else if (_SPSTING_DATE.Substring(5, 2).Trim() == "07")
    //                                _sMonth = "Jul";
    //                            else if (_SPSTING_DATE.Substring(5, 2).Trim() == "08")
    //                                _sMonth = "Aug";
    //                            else if (_SPSTING_DATE.Substring(5, 2).Trim() == "09")
    //                                _sMonth = "Sep";
    //                            else if (_SPSTING_DATE.Substring(5, 2).Trim() == "10")
    //                                _sMonth = "Oct";
    //                            else if (_SPSTING_DATE.Substring(5, 2).Trim() == "11")
    //                                _sMonth = "Nov";
    //                            else if (_SPSTING_DATE.Substring(5, 2).Trim() == "12")
    //                                _sMonth = "Dec";

    //                            _PSTING_DATE = _SPSTING_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _SPSTING_DATE.Substring(0, 4);
    //                        }

    //                        _sPunchDate = Convert.ToString(dtZBAPI.Rows[i]["PUNCH_DATE"]);
    //                        if (_sPunchDate.Trim().Length == 10)
    //                        {
    //                            if (_sPunchDate.Substring(5, 2).Trim() == "01")
    //                                _sMonth = "Jan";
    //                            else if (_sPunchDate.Substring(5, 2).Trim() == "02")
    //                                _sMonth = "Feb";
    //                            else if (_sPunchDate.Substring(5, 2).Trim() == "03")
    //                                _sMonth = "Mar";
    //                            else if (_sPunchDate.Substring(5, 2).Trim() == "04")
    //                                _sMonth = "Apr";
    //                            else if (_sPunchDate.Substring(5, 2).Trim() == "05")
    //                                _sMonth = "May";
    //                            else if (_sPunchDate.Substring(5, 2).Trim() == "06")
    //                                _sMonth = "Jun";
    //                            else if (_sPunchDate.Substring(5, 2).Trim() == "07")
    //                                _sMonth = "Jul";
    //                            else if (_sPunchDate.Substring(5, 2).Trim() == "08")
    //                                _sMonth = "Aug";
    //                            else if (_sPunchDate.Substring(5, 2).Trim() == "09")
    //                                _sMonth = "Sep";
    //                            else if (_sPunchDate.Substring(5, 2).Trim() == "10")
    //                                _sMonth = "Oct";
    //                            else if (_sPunchDate.Substring(5, 2).Trim() == "11")
    //                                _sMonth = "Nov";
    //                            else if (_sPunchDate.Substring(5, 2).Trim() == "12")
    //                                _sMonth = "Dec";

    //                            _PunchDate = _sPunchDate.Substring(8, 2) + "-" + _sMonth + "-" + _sPunchDate.Substring(0, 4);
    //                        }

    //                        _sMtrInDate = Convert.ToString(dtZBAPI.Rows[i]["INST_DATE"]);
    //                        if (_sMtrInDate.Trim().Length == 10)
    //                        {
    //                            if (_sMtrInDate.Substring(5, 2).Trim() == "01")
    //                                _sMonth = "Jan";
    //                            else if (_sMtrInDate.Substring(5, 2).Trim() == "02")
    //                                _sMonth = "Feb";
    //                            else if (_sMtrInDate.Substring(5, 2).Trim() == "03")
    //                                _sMonth = "Mar";
    //                            else if (_sMtrInDate.Substring(5, 2).Trim() == "04")
    //                                _sMonth = "Apr";
    //                            else if (_sMtrInDate.Substring(5, 2).Trim() == "05")
    //                                _sMonth = "May";
    //                            else if (_sMtrInDate.Substring(5, 2).Trim() == "06")
    //                                _sMonth = "Jun";
    //                            else if (_sMtrInDate.Substring(5, 2).Trim() == "07")
    //                                _sMonth = "Jul";
    //                            else if (_sMtrInDate.Substring(5, 2).Trim() == "08")
    //                                _sMonth = "Aug";
    //                            else if (_sMtrInDate.Substring(5, 2).Trim() == "09")
    //                                _sMonth = "Sep";
    //                            else if (_sMtrInDate.Substring(5, 2).Trim() == "10")
    //                                _sMonth = "Oct";
    //                            else if (_sMtrInDate.Substring(5, 2).Trim() == "11")
    //                                _sMonth = "Nov";
    //                            else if (_sMtrInDate.Substring(5, 2).Trim() == "12")
    //                                _sMonth = "Dec";

    //                            _MtrInDate = _sMtrInDate.Substring(8, 2) + "-" + _sMonth + "-" + _sMtrInDate.Substring(0, 4);
    //                        }

    //                        Insert_ZBAPI_DM_SLA_DATA(Convert.ToString(dtZBAPI.Rows[i]["ORDER_NO"]), Convert.ToString(dtZBAPI.Rows[i]["BP"]), Convert.ToString(dtZBAPI.Rows[i]["COMP"]),
    //                                                "", "", "", "", Convert.ToString(dtZBAPI.Rows[i]["DIVISION"]), Convert.ToString(dtZBAPI.Rows[i]["ORDER_STATUS"]),
    //                                            Convert.ToString(dtZBAPI.Rows[i]["ORDER_TYPE"]), Convert.ToString(dtZBAPI.Rows[i]["PG"]), Convert.ToString(dtZBAPI.Rows[i]["PM_ACTIVITY"]), Convert.ToString(dtZBAPI.Rows[i]["VENDOR_CODE"]),
    //                                    Convert.ToString(dtZBAPI.Rows[i]["NEW_METER_NUMBER"]), Convert.ToString(dtZBAPI.Rows[i]["NEW_METER_STATUS"]),
    //                          "", Convert.ToString(dtZBAPI.Rows[i]["REM_METER_NUMBER"]), "", Convert.ToString(dtZBAPI.Rows[i]["REM_METER_STATUS"]), Convert.ToString(dtZBAPI.Rows[i]["PM_CABLE_LENGTH"]), Convert.ToString(dtZBAPI.Rows[i]["PM_CABLE_SIZE"]),
    //                        Convert.ToString(dtZBAPI.Rows[i]["BM_CABLE_LENGTH"]), Convert.ToString(dtZBAPI.Rows[i]["BM_CABLE_SIZE"]), Convert.ToString(dtZBAPI.Rows[i]["OP_CABLE_LENGTH"]),
    //                          Convert.ToString(dtZBAPI.Rows[i]["OP_CABLE_SIZE"]), Convert.ToString(dtZBAPI.Rows[i]["BB_INST_STATUS"]), Convert.ToString(dtZBAPI.Rows[i]["BB_INST_SIZE"]),
    //                          Convert.ToString(dtZBAPI.Rows[i]["REM_CABLE_LENGTH"]), Convert.ToString(dtZBAPI.Rows[i]["REM_CABLE_SIZE"]),
    //                          Convert.ToString(dtZBAPI.Rows[i]["BUS_BAR_NO"]),
    //                          Convert.ToString(dtZBAPI.Rows[i]["OH_UG"]));
    //                    }

    //                }
    //            }
    //        }
    //        return ds;
    //    }
    //    catch (Exception ex)
    //    {
    //        dt.Rows.Add("01", ex.Message.ToString());
    //        ds.Tables.Add(dt);
    //        return ds;
    //    }
    //}

    //private bool Insert_ZBAPI_DM_SLA_DATA(string _ORDER_NO, string _BP, string _COMP, string _CREATION_DATE, string _BASIC_START_DATE, string _BASIC_FINISH_DATE, string _ACTIVITY_DATE, string _DIVISION,
    //                                     string _ORDER_STATUS, string _ORDER_TYPE, string _PG, string _PM_ACTIVITY, string _VENDOR_CODE, string _NEW_METER_NUMBER, string _NEW_METER_STATUS,
    //                                    string _NEW_METER_INST_DATE, string _REM_METER_NUMBER, string _REM_METER_DATE, string _REM_METER_STATUS, string _PM_CABLE_LENGTH, string _PM_CABLE_SIZE,
    //                                   string _BM_CABLE_LENGTH, string _BM_CABLE_SIZE, string _OP_CABLE_LENGTH, string _OP_CABLE_SIZE, string _BB_INST_STATUS, string _BB_INST_SIZE,
    //                                    string _REM_CABLE_LENGTH, string _REM_CABLE_SIZE, string _BUS_BAR_NO, string _OH_UG)
    //{

    //    string sql = " INSERT INTO MOBINT.mcr_details_non_tab(AUFNR,ORDERID,INPUT_DATE,COMP_CODE,DIVISION,AUART,ILART,VENDOR_CODE,PSTING_DATE,METER_NO,CA_NO,POLE_NO,BP_NO, ";
    //    sql += " PLANNER_GROUP,CABLE_SIZE,CABLE_LENGTH,KTOKL_ACCOUNT_CLASS,ZZ_RLOAD,SORT1,ACTIVITY_REASON,DRUM_NO,RL_FROM, ";
    //    sql += " RL_TO,BUS_BAR_SIZE,BUS_CABLE_SIZE,TERMINAL_SEAL,METERBOXSEAL1,METERBOXSEAL2,BUSBARSEAL1,BUSBARSEAL2,TERMINAL_SEAL2,OUTPUT_CABLE_LEN,SAP_PUNCHED_DATE,METER_INSTALLATION_DATE) VALUES ";
    //    sql += "   ('" + _ORDER_NO + "', '" + _BP + "', '" + _COMP + "', TO_DATE('" + _CREATION_DATE + "'), TO_DATE('" + _BASIC_START_DATE + "'), TO_DATE('" + _BASIC_FINISH_DATE + "'), TO_DATE('" + _ACTIVITY_DATE + "'), ";
    //    sql += "     '" + _DIVISION + "', '" + _ORDER_STATUS + "',  '" + _ORDER_TYPE + "',  '" + _PG + "', '" + _PM_ACTIVITY + "',  '" + _VENDOR_CODE + "',  '" + _NEW_METER_NUMBER + "', '" + _NEW_METER_STATUS + "',  TO_DATE('" + _NEW_METER_INST_DATE + "'),  ";
    //    sql += "     '" + _REM_METER_NUMBER + "', TO_DATE('" + _REM_METER_DATE + "'),  '" + _REM_METER_STATUS + "',  '" + _PM_CABLE_LENGTH + "', '" + _PM_CABLE_SIZE + "',  '" + _BM_CABLE_LENGTH + "',  '" + _BM_CABLE_SIZE + "', '" + _OP_CABLE_LENGTH + "',  ";
    //    sql += "     '" + _OP_CABLE_SIZE + "', '" + _BB_INST_STATUS + "',  '" + _BB_INST_SIZE + "',  '" + _REM_CABLE_LENGTH + "', '" + _REM_CABLE_SIZE + "',  '" + _BUS_BAR_NO + "',  '" + _OH_UG + "'))  ";

    //    return dml_Mobintsinglequery(sql);
    //}


    [WebMethod]
    public DataSet ZBAPI_DM_SLA_DATA_REM(string _sFrmDate, string _sToDate)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            string _SPSTING_DATE = string.Empty, _PSTING_DATE = string.Empty, _sMonth = string.Empty;
            string _sPunchDate = string.Empty, _PunchDate = string.Empty, _sInputDate = string.Empty;
            string _sMtrInDate = string.Empty, _MtrInDate = string.Empty;
            _sInputDate = System.DateTime.Now.AddDays(-12).ToString("yyyyMMdd").ToString();

            //for (int j = 2; j < 10; j++)
            {
                ds = obj.ZBAPI_DM_SLA_DATA_REM(_sFrmDate, _sToDate);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtZBAPI = ds.Tables[0];
                    if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                    {
                        dt.Rows.Add("01", "Mis-Match Data in SAP");
                        ds.Tables.Add(dt);
                    }

                    if (dtZBAPI.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtZBAPI.Rows.Count; i++)
                        {
                            _SPSTING_DATE = Convert.ToString(dtZBAPI.Rows[i]["PSTING_DATE"]);
                            if (_SPSTING_DATE.Trim().Length == 10)
                            {
                                if (_SPSTING_DATE.Substring(5, 2).Trim() == "01")
                                    _sMonth = "Jan";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "02")
                                    _sMonth = "Feb";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "03")
                                    _sMonth = "Mar";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "04")
                                    _sMonth = "Apr";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "05")
                                    _sMonth = "May";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "06")
                                    _sMonth = "Jun";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "07")
                                    _sMonth = "Jul";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "08")
                                    _sMonth = "Aug";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "09")
                                    _sMonth = "Sep";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "10")
                                    _sMonth = "Oct";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "11")
                                    _sMonth = "Nov";
                                else if (_SPSTING_DATE.Substring(5, 2).Trim() == "12")
                                    _sMonth = "Dec";

                                _PSTING_DATE = _SPSTING_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _SPSTING_DATE.Substring(0, 4);
                            }





                            //Insert_ZBAPI_DM_SLA_DATA(Convert.ToString(dtZBAPI.Rows[i]["ORDER_NO"]), Convert.ToString(dtZBAPI.Rows[i]["BP"]), Convert.ToString(dtZBAPI.Rows[i]["COMP"]),
                            //                        "", "", "", "", Convert.ToString(dtZBAPI.Rows[i]["DIVISION"]), Convert.ToString(dtZBAPI.Rows[i]["ORDER_STATUS"]),
                            //                    Convert.ToString(dtZBAPI.Rows[i]["ORDER_TYPE"]), Convert.ToString(dtZBAPI.Rows[i]["PG"]), Convert.ToString(dtZBAPI.Rows[i]["PM_ACTIVITY"]), Convert.ToString(dtZBAPI.Rows[i]["VENDOR_CODE"]),
                            //            Convert.ToString(dtZBAPI.Rows[i]["NEW_METER_NUMBER"]), Convert.ToString(dtZBAPI.Rows[i]["NEW_METER_STATUS"]),
                            //  "", Convert.ToString(dtZBAPI.Rows[i]["REM_METER_NUMBER"]), "", Convert.ToString(dtZBAPI.Rows[i]["REM_METER_STATUS"]), Convert.ToString(dtZBAPI.Rows[i]["PM_CABLE_LENGTH"]), Convert.ToString(dtZBAPI.Rows[i]["PM_CABLE_SIZE"]),
                            //Convert.ToString(dtZBAPI.Rows[i]["BM_CABLE_LENGTH"]), Convert.ToString(dtZBAPI.Rows[i]["BM_CABLE_SIZE"]), Convert.ToString(dtZBAPI.Rows[i]["OP_CABLE_LENGTH"]),
                            //  Convert.ToString(dtZBAPI.Rows[i]["OP_CABLE_SIZE"]), Convert.ToString(dtZBAPI.Rows[i]["BB_INST_STATUS"]), Convert.ToString(dtZBAPI.Rows[i]["BB_INST_SIZE"]),
                            //  Convert.ToString(dtZBAPI.Rows[i]["REM_CABLE_LENGTH"]), Convert.ToString(dtZBAPI.Rows[i]["REM_CABLE_SIZE"]),
                            //  Convert.ToString(dtZBAPI.Rows[i]["BUS_BAR_NO"]),
                            //  Convert.ToString(dtZBAPI.Rows[i]["OH_UG"]));
                        }

                    }
                }
            }
            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add("01", ex.Message.ToString());
            ds.Tables.Add(dt);
            return ds;
        }
    }

    #endregion

    [WebMethod]
    public DataSet ZBAPI_UPDATE_TNO(string strCA_no, string strTelephone, string strMobile, string strEmail, string strLandmark, string strDISPATCH_CTRL)
    {
        DataSet _dsBapiResult = obj.Get_ZBAPI_UPDATE_TNO(strCA_no, strTelephone, strMobile, strEmail, strLandmark, strDISPATCH_CTRL);
        return _dsBapiResult;
    }


    [WebMethod]
    public DataSet ZBAPI_DUNNING_NOTICE_WHATSAPP(string strBUKRS, string strVKONT)
    {
        DataSet Ds = obj.get_ZBAPI_DUNNING_NOTICE_WHATSAPP(strBUKRS, strVKONT);
        return Ds;
    }

    [WebMethod]
    public DataSet ZBAPI_ELNOTICE_WHATSAPP(string CompanyCode, string CANumber)
    {
        DataSet Ds = obj.Get_ZBAPI_ELNOTICE_WHATSAPP(CompanyCode, CANumber);
        return Ds;
    }

    [WebMethod]
    public DataSet ZBAPI_MCR_DOC_NUM(string strAR_DATE, string strCOMPANY_CODE)
    {
        DataSet dsBAPIOutput = obj.get_ZBAPI_MCR_DOC_NUM(strAR_DATE, strCOMPANY_CODE);
        return dsBAPIOutput;
    }

    [WebMethod]
    public String ZBAPI_FINAL_BILL_FLAG(string CA_NUMBER)
    {
        DataSet ds = obj.Get_ZBAPI_BILL_DET(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER);
        string str = string.Empty;
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 266)
                str = "Y";
            else
                str = "N";
        }
        else
        {
            str = "N";
        }

        DataSet ds1 = new DataSet();
        DataTable dtOutPut = new DataTable();
        DataColumn column = new DataColumn();
        dtOutPut.Columns.Add("OUT_PUT", typeof(string));
        dtOutPut.Rows.Add(str);
        ds1.Tables.Add(dtOutPut);
        dtOutPut.AcceptChanges();

        //Added prasoon dated 28032023
        // return ds1;

        string response = DataTableToJSONWithJavaScriptSerializer(ds1.Tables[0]);

        return response;
        //Added prasoon dated 28032023
    }

    [WebMethod]
    public String ZBAPI_DUNNotice_FLAG(string CA_NUMBER)
    {
        DataTable _dt = new DataTable();
        string _sCANo = CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER;
        string _sSql = " select CA_NUMBER FROM MOBAPP.NOTICE_LOG_DATA WHERE CA_NUMBER='" + _sCANo + "'";
        string str = string.Empty;
        _dt = dmlgetquery_MobApp(_sSql);

        if (_dt.Rows.Count > 0)
            str = "A";
        else
            str = "N";

        if (str == "N")
        {
            DelhiWSV2.WebService obj2 = new DelhiWSV2.WebService();
            DataSet ds = obj2.ZBAPI_DUNNING_NOTICE_WHATSAPP("BRPL", CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER);

            if (ds.Tables[0].Rows.Count > 0)
            {
                str = "Y";
            }
            else
            {
                str = "N";
            }
        }

        DataSet ds1 = new DataSet();
        DataTable dtOutPut = new DataTable();
        DataColumn column = new DataColumn();
        dtOutPut.Columns.Add("OUT_PUT", typeof(string));
        dtOutPut.Rows.Add(str);
        ds1.Tables.Add(dtOutPut);
        dtOutPut.AcceptChanges();

        //Added prasoon dated 28032023
       // return ds1;

        string response= DataTableToJSONWithJavaScriptSerializer(ds1.Tables[0]);

        return response;
        //Added prasoon dated 28032023
    }


    #region addedprasoondated28032023
    public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }
    #endregion

    public DataTable dmlgetquery_MobApp(string sql)
    {
        DataTable dt = new DataTable();
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbDataAdapter da;

        OleDbConnection ocon = new OleDbConnection(objNDS.Mobcon());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand = new OleDbCommand();
            dbcommand.Connection = ocon;
            da = new OleDbDataAdapter();
            da.SelectCommand = dbcommand;
            dt = null;
            dt = new DataTable();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandText = sql;
            //dbcommand.Transaction = dbtrans;
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return dt;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }



    #region PayTM DQR Method
    [WebMethod]
    public DataSet GenerateDQR(string MID, string Key, string OrderID, string amount, string CA, string posId)
    {
        DataSet dsDQRGenData = new DataSet();
        DataTable DTOUT = new DataTable();
        string Sql = string.Empty;
        string SqlLog = string.Empty;
        string LogMessage = string.Empty;

        string resultStatus = string.Empty, resultCode = string.Empty, resultMsg = string.Empty, qrCodeId = string.Empty, qrData = string.Empty;
        //DataColumn dtClmn = new DataColumn();
        DTOUT.Columns.Add("ID", typeof(string));
        DTOUT.Columns.Add("resultStatus", typeof(string));
        DTOUT.Columns.Add("resultCode", typeof(string));
        DTOUT.Columns.Add("resultMsg", typeof(string));
        DTOUT.Columns.Add("qrCodeId", typeof(string));
        DTOUT.Columns.Add("qrData", typeof(string));
        DTOUT.Columns.Add("OrerID", typeof(string));
        DTOUT.Columns.Add("amount", typeof(string));

        //LogMessage = "Service called with Ca: " + CA;
        Dictionary<string, string> body = new Dictionary<string, string>();
        Dictionary<string, string> head = new Dictionary<string, string>();
        Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();

        //body.Add("mid", "BSESDe01038697032174");
        body.Add("mid", MID);
        //body.Add("orderId", "OREDRID9878903");
        body.Add("orderId", OrderID);
        //body.Add("amount", "1303.00");
        body.Add("amount", amount);
        body.Add("businessType", "UPI_QR_CODE");
        body.Add("posId", posId);

        try
        {
            LogMessage = "Service called with Ca: " + CA + " and OrdID:" + OrderID + " MID:" + MID + " Key:" + Key + " posId:" + posId;
            SqlLog = "insert into pcm.PineLabErrLog(Message,methodName)values('" + LogMessage + "','GenerateDQR')";
            if (dml_singlequery_PCM(SqlLog.ToString()) == true)
            {
            }

            var result = JsonConvert.SerializeObject(body);

            //string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), "2ADZvHrESSLqhKPH");
            string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), Key);

            head.Add("clientId", "C11");
            head.Add("version", "V1");
            head.Add("signature", paytmChecksum);

            requestBody.Add("body", body);
            requestBody.Add("head", head);

            string post_data = JsonConvert.SerializeObject(requestBody);

            //For  Staging
            //string url = "https://securegw-stage.paytm.in/paymentservices/qr/create";    // For testing
            ///////////string url = "https://business.paytm.com/docs/api/create-qr-code-api/?ref=dynamicQR"; // donot try for test
            //https://business.paytm.com/docs/api/v3/transaction-status-api?ref=dynamicQR
            //For  Production  url
            string url = "https://securegw.paytm.in/paymentservices/qr/create";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            WebProxy proxy = new WebProxy("10.125.64.134:3128");
            webRequest.Proxy = proxy;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = post_data.Length;

            using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                requestWriter.Write(post_data);
            }

            string responseData = string.Empty;

            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();
                string data = JObject.Parse(responseData).ToString();

                dynamic task = JObject.Parse(responseData);
                var dtot = task.body;
                //List<DtoTask> 
                string code = dtot.image;

                resultStatus = dtot.resultInfo.resultStatus;
                resultCode = dtot.resultInfo.resultCode;
                resultMsg = dtot.resultInfo.resultMsg;
                qrCodeId = dtot.qrCodeId;
                qrData = dtot.qrData;


                DTOUT.Rows.Add("1", resultStatus, resultCode, resultMsg, qrCodeId, qrData, OrderID, amount);
                DTOUT.AcceptChanges();





            }
        }
        catch (Exception ex)
        {
            LogMessage = "Erro with Ca: " + CA + " and OrdID:" + OrderID + " MID:" + MID + " Key:" + Key + " Error : " + ex.Message.ToString() + " resultStatus:" + resultStatus;
            SqlLog = "insert into pcm.PineLabErrLog(Message,methodName)values('" + LogMessage + "','GenerateDQR')";
            if (dml_singlequery_PCM(SqlLog.ToString()) == true)
            {
            }
            DTOUT.Rows.Add("2", resultStatus, resultCode, resultMsg, qrCodeId, qrData, OrderID, amount);
            DTOUT.AcceptChanges();

        }
        DTOUT.TableName = "DQRCodeGen";
        dsDQRGenData.Tables.Add(DTOUT);
        dsDQRGenData.DataSetName = "DQRCodeGen_Out";
        return dsDQRGenData;
    }
    [WebMethod]
    public DataSet DQR_Status(string MID, string Key, string OrderID, string amount, string CA, string posId1)
    {
        DataSet dsDQRGenData = new DataSet();
        DataTable DTOUT = new DataTable();
        string Sql = string.Empty;
        string SqlLog = string.Empty;
        string LogMessage = string.Empty;
        string resultStatus = string.Empty, resultCode = string.Empty, resultMsg = string.Empty, txnId = string.Empty, bankTxnId = string.Empty, orderId = string.Empty, txnAmount = string.Empty, txnType = string.Empty, gatewayName = string.Empty, bankName = string.Empty, mid = string.Empty, paymentMode = string.Empty, refundAmt = string.Empty, txnDate = string.Empty, merchantUniqueReference = string.Empty, posId = string.Empty, udf1 = string.Empty;
        //DataColumn dtClmn = new DataColumn();
        DTOUT.Columns.Add("ID", typeof(string));
        DTOUT.Columns.Add("resultStatus", typeof(string));
        DTOUT.Columns.Add("resultCode", typeof(string));
        DTOUT.Columns.Add("resultMsg", typeof(string));
        DTOUT.Columns.Add("txnId", typeof(string));
        DTOUT.Columns.Add("bankTxnId", typeof(string));
        DTOUT.Columns.Add("orderId", typeof(string));
        DTOUT.Columns.Add("txnAmount", typeof(string));
        DTOUT.Columns.Add("txnType", typeof(string));
        DTOUT.Columns.Add("gatewayName", typeof(string));
        DTOUT.Columns.Add("bankName", typeof(string));
        DTOUT.Columns.Add("mid", typeof(string));
        DTOUT.Columns.Add("paymentMode", typeof(string));
        DTOUT.Columns.Add("refundAmt", typeof(string));
        DTOUT.Columns.Add("txnDate", typeof(string));
        DTOUT.Columns.Add("merchantUniqueReference", typeof(string));
        DTOUT.Columns.Add("posId", typeof(string));
        DTOUT.Columns.Add("udf1", typeof(string));

        //LogMessage = "Service called with Ca: " + CA;
        Dictionary<string, string> body = new Dictionary<string, string>();
        Dictionary<string, string> head = new Dictionary<string, string>();
        Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();

        //body.Add("mid", "BSESDe01038697032174");
        body.Add("mid", MID);
        body.Add("orderId", OrderID);
        body.Add("amount", amount);
        body.Add("businessType", "UPI_QR_CODE");
        body.Add("posId", posId1);

        try
        {
            LogMessage = "Service called with Ca: " + CA + " and OrdID:" + OrderID + " MID:" + MID + " Key:" + Key + " posId:" + posId;
            SqlLog = "insert into pcm.PineLabErrLog(Message,methodName)values('" + LogMessage + "','GenerateDQR')";
            if (dml_singlequery_PCM(SqlLog.ToString()) == true)
            {
            }


            var result = JsonConvert.SerializeObject(body);

            //string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), "2ADZvHrESSLqhKPH");
            string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), Key);

            head.Add("clientId", "C11");
            head.Add("version", "V1");
            head.Add("signature", paytmChecksum);

            requestBody.Add("body", body);
            requestBody.Add("head", head);

            string post_data = JsonConvert.SerializeObject(requestBody);

            //For  Staging
            //string url = "https://securegw-stage.paytm.in/v3/order/status";  // For testing 
            /////////string url = "https://business.paytm.com/docs/api/create-qr-code-api/?ref=dynamicQR";  /// Do not try for testing
            //https://business.paytm.com/docs/api/v3/transaction-status-api?ref=dynamicQR
            //For  Production  url
            string url = "https://securegw.paytm.in/v3/order/status";


            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            WebProxy proxy = new WebProxy("10.125.64.134:3128");
            webRequest.Proxy = proxy;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = post_data.Length;

            using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                requestWriter.Write(post_data);
            }

            string responseData = string.Empty;

            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();
                string data = JObject.Parse(responseData).ToString();

                dynamic task = JObject.Parse(responseData);
                var dtot = task.body;
                //List<DtoTask> 
                string code = dtot.image;

                resultStatus = dtot.resultInfo.resultStatus;
                resultCode = dtot.resultInfo.resultCode;
                resultMsg = dtot.resultInfo.resultMsg;
                txnId = dtot.txnId;
                bankTxnId = dtot.bankTxnId;
                orderId = dtot.orderId;
                txnAmount = dtot.txnAmount;
                txnType = dtot.txnType;
                gatewayName = dtot.gatewayName;
                bankName = dtot.bankName;
                mid = dtot.mid;
                paymentMode = dtot.paymentMode;
                refundAmt = dtot.refundAmt;
                txnDate = dtot.txnDate;
                merchantUniqueReference = dtot.merchantUniqueReference;
                posId = dtot.posId;
                udf1 = dtot.udf1;

                DTOUT.Rows.Add("1", resultStatus, resultCode, resultMsg, txnId, bankTxnId, orderId, txnAmount, txnType, gatewayName, bankName, mid, paymentMode, refundAmt, txnDate, merchantUniqueReference, posId, udf1);
                DTOUT.AcceptChanges();


                LogMessage = "Success with Ca: " + CA + " and OrdID:" + OrderID + " Message: " + "_resultStatus:" + resultStatus + "_resultCode:" + resultCode + "_resultMsg:" + resultMsg + "_txnId:" + txnId + "_bankTxnId:" + bankTxnId + "_orderId:" + orderId + "_txnAmount:" + txnAmount + "_txnType:" + txnType + "_gatewayName:" + gatewayName + "_bankName:" + bankName + "_mid:" + mid + "_paymentMode:" + paymentMode + "_refundAmt:" + refundAmt + "_txnDate:" + txnDate + "_merchantUniqueReference:" + merchantUniqueReference + "_posId:" + posId + "_udf1:" + udf1;
                SqlLog = "insert into pcm.PineLabErrLog(Message,methodName)values('" + LogMessage + "','SuccessDQR')";
                if (dml_singlequery_PCM(SqlLog.ToString()) == true)
                {
                }

            }
        }
        catch (Exception ex)
        {
            LogMessage = "Erro with Ca: " + CA + " and OrdID:" + OrderID + " MID:" + MID + " Key:" + Key + " Error : " + ex.Message.ToString() + " resultStatus:" + resultStatus;
            SqlLog = "insert into pcm.PineLabErrLog(Message,methodName)values('" + LogMessage + "','GenerateDQR')";
            if (dml_singlequery_PCM(SqlLog.ToString()) == true)
            {
            }
            DTOUT.Rows.Add("2", resultStatus, resultCode, resultMsg, txnId, bankTxnId, orderId, txnAmount, txnType, gatewayName, bankName, mid, paymentMode, refundAmt, txnDate, merchantUniqueReference, posId, udf1);
            DTOUT.AcceptChanges();

        }


        DTOUT.TableName = "DQRCodeGen";
        dsDQRGenData.Tables.Add(DTOUT);
        dsDQRGenData.DataSetName = "DQRCodeGen_Out";

        return dsDQRGenData;
    }

    #endregion

    [WebMethod]
    public DataSet SEND_BSES_SMS_API(string _sAppName, string _sEncryptionKey, string _sCompanyCode, string _sVendorCode, string _sEmpCode,
                string _MobileNo, string _sOTPMsg, string _sPassword, string _sLoginID, string _sBttilyLnk, string _sBttilyLnkiOS, string _sSMSType)

    {
        string apiUrl = string.Empty; //!!B$E$@@*SMS
        string strRes = string.Empty;
        string strResFlg = string.Empty;
        string ResponseText = string.Empty;
        string Number = _MobileNo.Length == 10 ? "91" + _MobileNo.Trim() : _MobileNo.Trim();
        string Message = _sOTPMsg + " is your one time password (OTP). Please enter the OTP to proceed. Team BRPL";

        DataTable _dtSMSValidity = CheckSMS_Validity(_sEncryptionKey, _sCompanyCode, _sSMSType);
        if (Number.Length != 12)
        {
            strRes = "Invalid Mobile Number";
            strResFlg = "F";
        }
        else if (_dtSMSValidity.Rows.Count == 0)
        {
            strRes = "Key Mismatch";
            strResFlg = "F";
        }
        else
        {
            Message = _dtSMSValidity.Rows[0][1].ToString();

            if ((_sSMSType.Trim() == "OTP") || (_sSMSType.Trim() == "NOTP") || (_sSMSType.Trim() == "OTPNV"))
                Message = Message.Replace("########", _sOTPMsg);
            else if (_sSMSType.Trim() == "FGP")
                Message = Message.Replace("########", _sPassword);
            else if (_sSMSType.Trim() == "HAP")
            {                
                Message = Message.Replace("$$$$", _sBttilyLnk);
                Message = Message.Replace("$$$$$", _sBttilyLnkiOS);
                Message = Message.Replace("@@@@", _sVendorCode);
                Message = Message.Replace("########", _sLoginID);
                Message = Message.Replace("!!!!!!!!", _sPassword);
            }
            else if (_sSMSType.Trim() == "NOR")
            {
                Message = _sOTPMsg;
            }

            apiUrl = _dtSMSValidity.Rows[0][0].ToString();
            if (_sCompanyCode.Trim() == "BRPL")
            {
                // apiUrl = "https://japi.instaalerts.zone/failsafe/HttpLink?aid=508443&pin=bses@56&mnumber=" + Number + "&signature=BSESRP&message=" + Message;
                apiUrl += Number + "&signature= BSESRP&message=" + Message;
                string MyProxyHostString = "10.125.64.134";
                int MyProxyPort = 3128;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(apiUrl);
                req.Proxy = new WebProxy(MyProxyHostString, MyProxyPort);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                string results = sr.ReadToEnd();

                while (resp.StatusCode.ToString().Equals("OK") != true) { }
                if (resp.StatusCode.ToString().Equals("OK"))
                {
                    ResponseText = ParseHttpWebResponse(resp);
                    strRes = "SUCCESS";
                    strResFlg = "S";
                }
                else
                {
                    ResponseText = "N";
                    strRes = "FAIL";
                    strResFlg = "F";
                }

            }
            else
            {
                //apiUrl = "https://japi.instaalerts.zone/failsafe/HttpLink?aid=508443&pin=bses@56&mnumber=" + Number + "&signature=BSESYP&message=" + Message;
                //string MyProxyHostString = "10.125.64.134";
                //int MyProxyPort = 3128;
                //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(apiUrl);
                //req.Proxy = new WebProxy(MyProxyHostString, MyProxyPort);
                //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                //System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                //string results = sr.ReadToEnd();

                //while (resp.StatusCode.ToString().Equals("OK") != true) { }
                //if (resp.StatusCode.ToString().Equals("OK"))
                //{
                //    ResponseText = ParseHttpWebResponse(resp);
                //    strRes = "SUCCESS";
                //    strResFlg = "S";
                //}
                //else
                //{
                //    ResponseText = "N";
                //    strRes = "FAIL";
                //    strResFlg = "F";
                //}
                apiUrl += Number + "&signature= BSESRP&message=" + Message;
                string MyProxyHostString = "10.125.64.134";
                int MyProxyPort = 3128;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(apiUrl);
                req.Proxy = new WebProxy(MyProxyHostString, MyProxyPort);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                string results = sr.ReadToEnd();

                while (resp.StatusCode.ToString().Equals("OK") != true) { }
                if (resp.StatusCode.ToString().Equals("OK"))
                {
                    ResponseText = ParseHttpWebResponse(resp);
                    strRes = "SUCCESS";
                    strResFlg = "S";
                }
                else
                {
                    ResponseText = "N";
                    strRes = "FAIL";
                    strResFlg = "F";
                }
            }

           // if (_sCompanyCode.Trim() == "BRPL")
            {
                if (ResponseText != "N")
                {
                    string[] sSms = ResponseText.Split('=');
                    string responseId = string.Empty, responseCode = string.Empty, responseStatus = string.Empty, responsedatetime = string.Empty;

                    if (sSms.Length > 3)
                    {
                        responseId = sSms[1].ToString().Replace("~code", "").Trim();
                        responseCode = sSms[2].ToString().Replace(" & info", "").Trim();
                        responseStatus = sSms[3].ToString().Replace(" & Time ", "").Trim();
                        responsedatetime = sSms[4].ToString();
                    }
                    else
                    {
                        responseId = "API000";
                        responseCode = "API000";
                        responseStatus = "Air2web accepted";
                        responsedatetime = System.DateTime.Now.ToString();
                    }

                    if (sSms.Length > 0)
                    {
                        Insert_MessageLog(Message, Number.ToString(), _sSMSType, _sCompanyCode, "Y",
                        responseId, responseCode, responseStatus, System.DateTime.Now.ToString(), _sVendorCode.ToString(), _sAppName.ToString(), _sEmpCode.ToString());
                    }
                    else
                    {
                        Insert_MessageLog(Message, Number.ToString(), _sSMSType, _sCompanyCode, "X", "", "", "", "",
                                                _sVendorCode.ToString(), _sAppName.ToString(), _sEmpCode.ToString());
                    }
                }
                else
                {
                    Insert_MessageLog(Message, Number.ToString(), _sSMSType, _sCompanyCode, "X", "", "", "", "",
                                                _sVendorCode.ToString(), _sAppName.ToString(), _sEmpCode.ToString());
                }
            }
        }

        DataSet ds1 = new DataSet();
        DataTable dtOutPut = new DataTable();
        DataColumn column = new DataColumn();
        dtOutPut.Columns.Add("FLAG", typeof(string));
        dtOutPut.Columns.Add("OUT_PUT", typeof(string));
        dtOutPut.Rows.Add(strResFlg, strRes);
        ds1.Tables.Add(dtOutPut);
        dtOutPut.AcceptChanges();
        return ds1;
    }
    private string ParseHttpWebResponse(HttpWebResponse httpRep)
    {
        string rep;
        System.IO.StreamReader sr = new System.IO.StreamReader(httpRep.GetResponseStream());
        rep = sr.ReadToEnd();
        return rep;
    }

    private void Insert_MessageLog(string _sMsg, string _sMobileNo, string _sSMSType, string _sCompany, string _sStatus, string _sResponseId,
                        string _sResponseCode, string _sResponseStatus, string _sResponseTime, string _sVendor, string _sAppName, string _sEmpCode)
    {
        string sql = " INSERT INTO mobapp.dsk_sms_details (SMS_ID, SMS_TYPE, COMPANY, MOBILE_NO, MESSAGE_TXT,STATUS,SMS_SEND_DT,RESP_ID,RESP_INFO,RESP_CODE,RESP_TIME,VENDOR,APP_NAME,EMP_CODE) VALUES  ";
        sql = sql + "  (sms_id.NEXTVAL ,'" + _sSMSType + "' ,'" + _sCompany + "','" + _sMobileNo + "' ,'" + _sMsg + "' , ";
        sql = sql + "  '" + _sStatus + "' ,sysdate ,'" + _sResponseId + "', '" + _sResponseStatus + "','" + _sResponseCode + "' ,'" + _sResponseTime + "','" + _sVendor + "','" + _sAppName + "','" + _sEmpCode + "' )  ";

        dmlsinglequery(sql);
    }

    //private DataTable CheckSMS_Validity(string _sKey, string _sCompany)
    //{
    //    string _sSql = string.Empty;
    //    DataTable _dt = new DataTable();

    //    _sSql = " SELECT API_URL FROM SMS_API_MST where ENCRYPTION_KEY='" + _sKey + "' and ACTIVE_STATUS='Y' ";
    //    _sSql += " and trunc(sysdate)<trunc(VALIDITY_END_DATE) and COMPANY_CODE='" + _sCompany + "' ";
    //    return dmlgetqueryCon(_sSql);
    //}

    private DataTable CheckSMS_Validity(string _sKey, string _sCompany, string _sSMSType)
    {
        string _sSql = string.Empty;
        DataTable _dt = new DataTable();

        _sSql = " SELECT API_URL,SMS_TEXT FROM SMS_API_MST where ENCRYPTION_KEY='" + _sKey + "' and ACTIVE_STATUS='Y' ";
        _sSql += " and trunc(sysdate)<trunc(VALIDITY_END_DATE) and COMPANY_CODE='" + _sCompany + "' and SMS_TYPE='" + _sSMSType + "' ";
        return dmlgetqueryCon(_sSql);
    }

    #region SAPPASSWORDRESET
    [WebMethod]
    public DataSet ZBAPI_SEND_EMAIL_ID(string BNAME, string SAPCONNECTION = "")
    {
        DataSet ds = obj.ZBAPI_SEND_EMAIL_ID(BNAME, SAPCONNECTION);
        return ds;
    }
    [WebMethod]
    public DataSet ZBAPI_SEND_URL(string BNAME, string SMTP_ADDR, string URL, string MESSAGE, string SAPCONNECTION = "")
    {
        URL = URL.Replace("#", "&");
        DataSet ds = obj.ZBAPI_SEND_URL(BNAME, SMTP_ADDR, URL, MESSAGE, SAPCONNECTION);
        return ds;
    }
    [WebMethod]
    public DataSet ZBAPI_reset_password(string BNAME, string PASSWORD, string SAPCONNECTION = "")
    {
        DataSet ds = obj.ZBAPI_reset_password(BNAME, PASSWORD, SAPCONNECTION);
        return ds;
    }

    #endregion

    #region"Email Service API"

    [WebMethod]
    public string SendEmail_smtp(string toAddress, string CCAddress, string BCCAddress, string subject, string Mailbody,
                                                      string MailAttachmentAddress, string _sHTML, string SenderMailID)
    {
        string result = "Y";
        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(SenderMailID);
            mail.To.Add(toAddress);
            mail.CC.Add(CCAddress);
            mail.Bcc.Add(BCCAddress);
            mail.Subject = subject;
            mail.Body = Mailbody;

            if (_sHTML == "Y")
                mail.IsBodyHtml = true;

            if (MailAttachmentAddress != "")
            {
                //Utilities.Network.NetworkDrive nd = new Utilities.Network.NetworkDrive();
                //nd.MapNetworkDrive(@"\\10.125.64.236\UploadedImages", "Z:", "uploadimages", "Bses@123");
                string NAS_Directory = @"\\10.125.64.236\UploadedImages"; //Local directory where the files will be downloaded
                string NAS_UserName = "uploadimages";
                string NAS_Password = "Bses@123";
                string _sPath = string.Empty;
                NetworkDrive oNetDrive = new NetworkDrive();
                oNetDrive.LocalDrive = "ZZ:";
                oNetDrive.ShareName = NAS_Directory;
                int map = 0;

                try
                {
                    oNetDrive.MapDrive(NAS_UserName, NAS_Password);
                }
                catch (Exception exx)
                {
                    map = 1;
                }
                if (map > 0)
                {
                    oNetDrive.UnMapDrive();
                    oNetDrive.MapDrive(NAS_UserName, NAS_Password);
                }

                MailAttachmentAddress = MailAttachmentAddress.ToString().Replace(@"\", @"\\").ToString();

                for (int i = 0; i < MailAttachmentAddress.ToString().Split(',').Length; i++)
                {
                    //Attachment attachment = new Attachment(MailAttachmentAddress);
                    Attachment attachment = new Attachment(MailAttachmentAddress.ToString().Split(',')[i].ToString());
                    mail.Attachments.Add(attachment);
                }

                //oNetDrive.UnMapDrive();
            }


            SmtpClient smtp = new SmtpClient("10.8.61.84");

            smtp.Port = 25;
            smtp.Send(mail);

        }
        catch (Exception ex)
        {
            result = "N";
        }

        return result;
    }


    #endregion

    [WebMethod]
    public DataSet ZBAPI_STATUS163(string strVKONT)
    {
        DataSet dsBAPIOutput = obj.get_ZBAPI_STATUS163(strVKONT);
        return dsBAPIOutput;
    }


    //AANSHU KUMAR - 11-03-24 fetch data from solr using valid meter number
    [WebMethod]
    public void GetSolrData(string meterNo)
    {
        try
        {
            var client = new WebClient();
            if (meterNo != null && (meterNo.Length == 18 || meterNo.Length == 8))
            {
                if (meterNo.Length == 8)
                {
                    meterNo = "0000000000" + meterNo;
                }
                var solrUrl = "http://10.125.126.72:8983/solr/TFCF/select?q=*:*&fq=DEVICE_NO:" + meterNo + "&fl=SAP_DIVISION,ACCOUNT_CLASS,CONSUMER_STATUS&rows=1000";

                var jsonString = client.DownloadString(solrUrl);
                var solrResponse = JsonConvert.DeserializeObject<SolrResponse>(jsonString);
                if (solrResponse != null && solrResponse.response.numFound > 0)
                {
                    Context.Response.ContentType = "application/xml"; // Set content type of response
                    var xmlSerializer = new XmlSerializer(typeof(SolrResponse));
                    xmlSerializer.Serialize(Context.Response.OutputStream, solrResponse);
                    //Context.Response.Write(JsonConvert.SerializeObject(new { data = solrResponse.response.docs, message = "Solr search successful!" }));
                }
                else
                {
                    Context.Response.StatusCode = (int)HttpStatusCode.NotFound; // Set status code to 404
                    Context.Response.Write("No records found for the specified meter number.");
                }
            }
            else
            {
                Context.Response.StatusCode = (int)HttpStatusCode.NotFound; // Set status code to 404
                Context.Response.Write("Invalid meter number.");
            }
        }
        catch (Exception ex)
        {
            Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Set status code to 500
            Context.Response.Write("An error occurred while fetching data from Solr:" + ex.Message);
        }
    }



    //Added by ajay 01.May.2024
    [WebMethod]
    public void ConsumerDisplay(string CA_Meter_Mob)
    {
        string solrUrl = string.Empty;
        if (!string.IsNullOrEmpty(CA_Meter_Mob))
        {
            try
            {
                var client = new WebClient();
                if (CA_Meter_Mob.Length == 10)
                {
                    //solrUrl = "http://10.125.126.72:8983/solr/TFCF/select?q=*:*&fq=TEL_NUMBER:" + CA_Meter_Mob + "&fl=SAP_DIVISION,ACCOUNT_CLASS,CONSUMER_STATUS&rows=1000";
                    solrUrl = "http://10.125.126.72:8983/solr/TFCF/select?indent=true&q.op=OR&q=TEL_NUMBER:" + CA_Meter_Mob + "&&rows=1000";
                }
                else if (CA_Meter_Mob.Length == 9)
                {
                    //solrUrl = "http://10.125.126.72:8983/solr/TFCF/select?q=*:*&fq=CONTRACT_ACCOUNT:" + "000"+CA_Meter_Mob + "&fl=SAP_DIVISION,ACCOUNT_CLASS,CONSUMER_STATUS&rows=1000";
                    solrUrl = "http://10.125.126.72:8983/solr/TFCF/select?indent=true&q.op=OR&q=CONTRACT_ACCOUNT:" + "000" + CA_Meter_Mob + "&&rows=1000";
                }
                else if (CA_Meter_Mob.Length == 8)
                {
                    //solrUrl = "http://10.125.126.72:8983/solr/TFCF/select?q=*:*&fq=DEVICE_NO:" + "0000000000"+CA_Meter_Mob + "&fl=SAP_DIVISION,ACCOUNT_CLASS,CONSUMER_STATUS&rows=1000";
                    solrUrl = "http://10.125.126.72:8983/solr/TFCF/select?indent=true&q.op=OR&q=DEVICE_NO:" + "0000000000" + CA_Meter_Mob + "&&rows=1000";
                }

                List<DocsInfo> documents = new List<DocsInfo>();
                var jsonString = client.DownloadString(solrUrl);
                var solrResponse = JsonConvert.DeserializeObject<RootObject>(jsonString);
                if (solrResponse != null && solrResponse.response.docs.Count > 0)
                {

                    //foreach (var doc in solrResponse.response.docs)
                    //{
                    foreach (var doc in solrResponse.response.docs)
                    {
                        DocsInfo document = new DocsInfo();

                        // Check if the keys exist in the dictionary before accessing them
                        if (doc.ContainsKey("MOVE_OUT"))
                            document.MOVE_OUT = doc["MOVE_OUT"].ToString();
                        if (doc.ContainsKey("CONNECTION_TYPE"))
                            document.CONNECTION_TYPE = doc["CONNECTION_TYPE"].ToString();
                        if (doc.ContainsKey("ACTIVE"))
                            document.ACTIVE = doc["ACTIVE"].ToString();
                        if (doc.ContainsKey("SANCTIONED_LOAD"))
                            document.SANCTIONED_LOAD = doc["SANCTIONED_LOAD"].ToString();
                        if (doc.ContainsKey("RATE_CATEGORY"))
                            document.RATE_CATEGORY = doc["RATE_CATEGORY"].ToString();
                        if (doc.ContainsKey("BP_TYPE"))
                            document.BP_TYPE = doc["BP_TYPE"].ToString();
                        if (doc.ContainsKey("SAP_POLE_ID"))
                            document.SAP_POLE_ID = doc["SAP_POLE_ID"].ToString();
                        if (doc.ContainsKey("LAST_NAME"))
                            document.LAST_NAME = doc["LAST_NAME"].ToString();
                        if (doc.ContainsKey("MRU"))
                            document.MRU = doc["MRU"].ToString();
                        if (doc.ContainsKey("SUB_DIVISION"))
                            document.SUB_DIVISION = doc["SUB_DIVISION"].ToString();
                        if (doc.ContainsKey("DEVICE_NO"))
                            document.DEVICE_NO = doc["DEVICE_NO"].ToString();
                        if (doc.ContainsKey("INSTALLATION_TYPE"))
                            document.INSTALLATION_TYPE = doc["INSTALLATION_TYPE"].ToString();
                        if (doc.ContainsKey("CONSUMER_STATUS"))
                            document.CONSUMER_STATUS = doc["CONSUMER_STATUS"].ToString();
                        if (doc.ContainsKey("COMPANY_CODE"))
                            document.COMPANY_CODE = doc["COMPANY_CODE"].ToString();
                        if (doc.ContainsKey("CSTS_CD"))
                            document.CSTS_CD = doc["CSTS_CD"].ToString();
                        if (doc.ContainsKey("CONTRACT_ACCOUNT"))
                            document.CONTRACT_ACCOUNT = doc["CONTRACT_ACCOUNT"].ToString();
                        if (doc.ContainsKey("SAP_DEPARTMENT"))
                            document.SAP_DEPARTMENT = doc["SAP_DEPARTMENT"].ToString();
                        if (doc.ContainsKey("SEQUENCE_NO"))
                            document.SEQUENCE_NO = doc["SEQUENCE_NO"].ToString();
                        if (doc.ContainsKey("BILL_DISPATCH_CONTROL"))
                            document.BILL_DISPATCH_CONTROL = doc["BILL_DISPATCH_CONTROL"].ToString();
                        if (doc.ContainsKey("SAP_DIVISION"))
                            document.SAP_DIVISION = doc["SAP_DIVISION"].ToString();
                        if (doc.ContainsKey("ACCOUNT_CLASS"))
                            document.ACCOUNT_CLASS = doc["ACCOUNT_CLASS"].ToString();
                        if (doc.ContainsKey("MOBILE_NO"))
                            document.MOBILE_NO = doc["MOBILE_NO"].ToString();
                        if (doc.ContainsKey("SAP_NAME"))
                            document.SAP_NAME = doc["SAP_NAME"].ToString();
                        if (doc.ContainsKey("TARIFF"))
                            document.TARIFF = doc["TARIFF"].ToString();
                        if (doc.ContainsKey("CONS_REF"))
                            document.CONS_REF = doc["CONS_REF"].ToString();
                        if (doc.ContainsKey("SAP_ADDRESS"))
                            document.SAP_ADDRESS = doc["SAP_ADDRESS"].ToString();
                        if (doc.ContainsKey("MOVE_IN"))
                            document.MOVE_IN = doc["MOVE_IN"].ToString();
                        if (doc.ContainsKey("POLE_NO"))
                            document.MOVE_IN = doc["POLE_NO"].ToString();
                        if (doc.ContainsKey("BUSINESS_PARTNER"))
                            document.BUSINESS_PARTNER = doc["BUSINESS_PARTNER"].ToString();
                        else
                            document.BUSINESS_PARTNER = "";
                        if (doc.ContainsKey("EMAIL"))
                            document.Email = doc["EMAIL"].ToString();
                        else
                            document.Email = "";

                        documents.Add(document);
                    }



                    //DocsInfo document = new DocsInfo
                    //{
                    //    MOVE_OUT = doc["MOVE_OUT"].ToString(),
                    //    CONNECTION_TYPE = doc["CONNECTION_TYPE"].ToString(),
                    //    ACTIVE = doc["ACTIVE"].ToString(),
                    //    SANCTIONED_LOAD = doc["SANCTIONED_LOAD"].ToString(),
                    //    RATE_CATEGORY = doc["RATE_CATEGORY"].ToString(),
                    //    BP_TYPE = doc["BP_TYPE"].ToString(),
                    //    SAP_POLE_ID = doc["SAP_POLE_ID"].ToString(),
                    //    LAST_NAME = doc["LAST_NAME"].ToString(),
                    //    TEL_NUMBER = doc["TEL_NUMBER"].ToString(),
                    //    DIVISION = doc["DIVISION"].ToString(),
                    //    FIRST_NAME = doc["FIRST_NAME"].ToString(),
                    //    MRU = doc["MRU"].ToString(),
                    //    SUB_DIVISION = doc["SUB_DIVISION"].ToString(),
                    //    DEVICE_NO = doc["DEVICE_NO"].ToString(),
                    //    INSTALLATION_TYPE = doc["INSTALLATION_TYPE"].ToString(),
                    //    CONSUMER_STATUS = doc["CONSUMER_STATUS"].ToString(),
                    //    COMPANY_CODE = doc["COMPANY_CODE"].ToString(),
                    //    CSTS_CD = doc["CSTS_CD"].ToString(),
                    //    CONTRACT_ACCOUNT = doc["CONTRACT_ACCOUNT"].ToString(),
                    //    SAP_DEPARTMENT = doc["SAP_DEPARTMENT"].ToString(),
                    //    SEQUENCE_NO = doc["SEQUENCE_NO"].ToString(),
                    //    BILL_DISPATCH_CONTROL = doc["BILL_DISPATCH_CONTROL"].ToString(),
                    //    SAP_DIVISION = doc["SAP_DIVISION"].ToString(),
                    //    ACCOUNT_CLASS = doc["ACCOUNT_CLASS"].ToString(),
                    //    MOBILE_NO = doc["MOBILE_NO"].ToString(),
                    //    SAP_NAME = doc["SAP_NAME"].ToString(),
                    //    TARIFF = doc["TARIFF"].ToString(),
                    //    CONS_REF = doc["CONS_REF"].ToString(),
                    //    SAP_ADDRESS = doc["SAP_ADDRESS"].ToString(),
                    //    MOVE_IN = doc["MOVE_IN"].ToString(),
                    //    POLE_NO = doc["POLE_NO"].ToString(),
                    //    BUSINESS_PARTNER = doc["BUSINESS_PARTNER"].ToString(),
                    //    // Map other properties as needed
                    //};
                    //documents.Add(document);
                    ///}



                    Context.Response.ContentType = "application/xml"; // Set content type of response
                    var xmlSerializer = new XmlSerializer(typeof(List<DocsInfo>));
                    xmlSerializer.Serialize(Context.Response.OutputStream, documents);
                    //Context.Response.Write(JsonConvert.SerializeObject(new { data = solrResponse.response.docs, message = "Solr search successful!" }));
                }
                else
                {
                    Context.Response.StatusCode = (int)HttpStatusCode.NotFound; // Set status code to 404
                    Context.Response.Write("No records found");
                }

            }
            catch (Exception ex)
            {
                Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Set status code to 500
                Context.Response.Write("An error occurred while fetching data from Solr:" + ex.Message);
            }
        }
        else
        {
            Context.Response.StatusCode = (int)HttpStatusCode.NotFound; // Set status code to 404
            Context.Response.Write("Invalid meter number/CA Number/Mobile no..");
        }
    }
    [WebMethod]
    public DataSet ZBI_BAPI_CA_DUES_NIC(string strCA_number)
    {
        if (strCA_number.Length != 12)
            strCA_number = strCA_number.PadLeft(12, '0');
        DataSet dsBAPIOutput = obj.get_ZBI_BAPI_CA_DUES_NIC(strCA_number);
        return dsBAPIOutput;
    }

    [WebMethod]
    public DataSet ZBAPI_WHATSAPP_INTEGRATION(string strOrderID, string strIFlag)
    {
        DataSet Ds = obj.get_ZBAPI_WHATSAPP_INTEGRATION(strOrderID, strIFlag);
        return Ds;
    }

    [WebMethod]
    public DataSet Z_BAPI_CA_DISPLAY_WHATSAPP(string strCA_NUMBER, string strCONTRACT, string strSERIALNO, string strIMPORT_CRNNUMBER, string strIMPORT_TELEPHONE_NO, string strIMPORT_KNUMBER)
    {
        DataSet Ds = obj.get_Z_BAPI_CA_DISPLAY_WHATSAPP(strCA_NUMBER, strCONTRACT, strSERIALNO, strIMPORT_CRNNUMBER, strIMPORT_TELEPHONE_NO, strIMPORT_KNUMBER);
        return Ds;
    }
    [WebMethod]
    public DataSet ZBAPI_CA_ORDDETAILS(string strV_VKONT)
    {
        DataSet _dsDetails = obj.get_ZBAPI_CA_ORDDETAILS(strV_VKONT);
        return _dsDetails;
    }

    [WebMethod]
    public DataSet ZBAPI_CA_DISPLAY_CRM(string strIMPORT_CANUMBER)
    {
        DataSet _dsDetails = obj.get_ZBAPI_CA_DISPLAY_CRM(strIMPORT_CANUMBER);
        return _dsDetails;
    }

    [WebMethod]
    public DataSet ZCSUPDAT_PERSONAL_DETAILS(string strPARTNER, string strNAME_FIRST, string strNAMEMIDDLE, string strNAME_LAST, string strNAME_LST2, string strSTR_SUPPL1, string strSTR_SUPPL2, string strHOUSE_NUM1, string strSTREET, string strSTR_SUPPL3, string strTEL_NUMBER, string strSMTP_ADDR, string strFAX_NUMBER)
    {
        DataSet Ds = obj.get_ZCSUPDAT_PERSONAL_DETAILS(strPARTNER, strNAME_FIRST, strNAMEMIDDLE, strNAME_LAST, strNAME_LST2, strSTR_SUPPL1, strSTR_SUPPL2, strHOUSE_NUM1, strSTREET, strSTR_SUPPL3, strTEL_NUMBER, strSMTP_ADDR, strFAX_NUMBER);
        return Ds;
    }

    [WebMethod]
    public DataSet ZBAPI_ZCS_CLI_WEB(string strTelephoneNumber)
    {
        DataSet dsBAPIOutput = obj.get_ZBAPI_ZCS_CLI_WEB(strTelephoneNumber);
        return dsBAPIOutput;
    }

    [WebMethod]
    public string Get_CANo_From_Phone(string Phone)
    {
        DataSet ResultSet = new DataSet();
      
        try
        {
            if (!String.IsNullOrWhiteSpace(Phone))
            {
                ResultSet = obj.get_ZBAPI_ZCS_CLI_WEB(Phone);
                if (ResultSet == null || ResultSet.Tables.Count == 0 || ResultSet.Tables[0].Rows.Count == 0)
                    throw new Exception("INVALID PHONE NO. NO SUCH CONSUMER PRESENT");
                return ResultSet.Tables[0].Rows[0]["CONTRACT_ACCOUNT_NUMBER"].ToString().TrimStart(new char[] { '0' });
            }
            else
                throw new Exception("INVALID PHONE NUMBER. PLEASE PASS 10 DIGITS ONLY");
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    [WebMethod]
    public DataSet Get_CANo_From_Phone_WA(string Phone)
    {
        DataSet ResultSet = new DataSet();
        DataSet OutPutSet = new DataSet();
        try
        {
            ResultSet = obj.get_ZBAPI_ZCS_CLI_WEB(Phone);                  
        }
        catch (Exception e)
        {
            
        }

        OutPutSet.Tables.Add(getCADetailsFormattedTable(ResultSet.Tables[0]));
        return OutPutSet;
    }

    public DataTable getCADetailsFormattedTable(DataTable rawTable)
    {
        DataTable dtProcessedData = new DataTable();
        dtProcessedData = CADetailsFormDT();       
       
        if (rawTable.Rows.Count > 0)
        {
            for (int intRowCnt = 0; intRowCnt < rawTable.Rows.Count; intRowCnt++)
            {
                CADEtailsAddRow(dtProcessedData, intRowCnt+1,rawTable.Rows[intRowCnt][0].ToString());

            }
        }
        return dtProcessedData;
    }

    public void CADEtailsAddRow(DataTable dt, int Sno, string CAnumber)
    {
        DataRow dr = dt.NewRow();
        dr["SNO"] = Sno.ToString();       
        dr["CONTRACT_ACCOUNT_NUMBER"] = CAnumber;        
        dt.Rows.Add(dr);

    }

    public DataTable CADetailsFormDT()
    {
        DataTable dt = new DataTable("ConsumerDetails");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "SNO";
        dt.Columns.Add(dcol);

        DataColumn dco2 = new DataColumn();
        dco2.DataType = System.Type.GetType("System.String");
        dco2.ColumnName = "CONTRACT_ACCOUNT_NUMBER";
        dt.Columns.Add(dco2);

        return dt;
    }


    [WebMethod]
    public DataSet ZCS_DSS_ORDDETAILS(string strVAUFNR)
    {
        DataSet _dsDetails = obj.get_ZCS_DSS_ORDDETAILS(strVAUFNR);
        return _dsDetails;
    }
    [WebMethod]
    public DataSet ZBAPI_FICA_DEMAND_DUE_DATE(string strORDER_NO)
    {
        DataSet dsBAPIOutput = obj.get_ZBAPI_FICA_DEMAND_DUE_DATE(strORDER_NO);
        return dsBAPIOutput;
    }

    [WebMethod]
    public DataSet ZBAPI_MDI_LETTER(string CA_NUMBER)
    {
        DataSet dsCAInfo = new DataSet();
        DataTable dt = new DataTable();

        string str = "";
        DataSet ds = obj.Get_ZBAPI_MDI_LETTER(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            str += ds.Tables[0].Rows[i][0].ToString().Trim() + Environment.NewLine;
        }

        DataTable dtOutPut = new DataTable();
        DataColumn column = new DataColumn();
        dtOutPut.Columns.Add("OUT_PUT", typeof(string));
        dtOutPut.Rows.Add(str);
        ds.Tables.Add(dtOutPut);
        dtOutPut.AcceptChanges();

        return ds;
    }

    [WebMethod]
    public DataSet ZBAPI_ENF_INSTL(string _sCompanyCode, string _sContractAcc, string _sCaseID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            ds = obj.Get_ZBAPI_ENF_INSTL(_sCompanyCode, _sContractAcc, _sCaseID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtZBAPI = ds.Tables[0];
                if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                {
                    dt.Rows.Add("01", "Mis-Match Data in SAP");
                    ds.Tables.Add(dt);
                }
            }
            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add("01", ex.Message.ToString());
            ds.Tables.Add(dt);
            return ds;
        }
    }

    [WebMethod]
    public DataSet ZBAPI_CANCEL_ORD_DATA(string strCompany, string strType, string strCancelDate,string strCompDate)
    {
        DataSet Ds = obj.Get_ZBAPI_CANCEL_ORD_DATA(strCompany, strType, strCancelDate, strCompDate);

        //if (Ds.Tables[0].Rows.Count > 0)
        //{
        //    DataTable dt = new DataTable();
        //    dt = Ds.Tables[0];
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            Insert_DSK_Cancel_SAP_Data(Convert.ToString(dt.Rows[0]["COMPANY_CODE"]), Convert.ToString(dt.Rows[0]["ORDER_TYPE"]), Convert.ToString(dt.Rows[0]["ORDER_NO"]),
        //                                Convert.ToString(dt.Rows[0]["CANCEL_DATE"]), Convert.ToString(dt.Rows[0]["TECH_COMP_DATE"]), Convert.ToString(dt.Rows[0]["PM_ACTIVITY"]),
        //                                Convert.ToString(dt.Rows[0]["CREATED_ON"]), Convert.ToString(dt.Rows[0]["STATUS"]), Convert.ToString(dt.Rows[0]["KUNUM"]));
        //        }
        //    }
        //}


        return Ds;
    }

    //[WebMethod]
    //public DataSet GetDMS_DataAPI(string _sNumber, string _sType)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable _dt = new DataTable();
    //    string _sSql = string.Empty;

    //    if (_sType == "C")
    //    {
    //        if (_sNumber.Length == 9)
    //            _sNumber = "000" + _sNumber;
    //        else if (_sNumber.Length == 10)
    //            _sNumber = "00" + _sNumber;
    //        else if (_sNumber.Length == 11)
    //            _sNumber = "0" + _sNumber;
    //    }
    //    else if (_sType == "O")
    //    {
    //        if (_sNumber.Length == 10)
    //            _sNumber = "00" + _sNumber;
    //        else if (_sNumber.Length == 11)
    //            _sNumber = "0" + _sNumber;
    //    }

    //    _sSql = " select DOCUMENTID,DOCUMENTNAME,DOCUMENTTYPE,RECORD_ENTRY_DATE from mobapp.dsk_documents_mapping where  REQUEST_ID='" + _sNumber + "'   or  ORDER_NO ='" + _sNumber + "'  or  CA_NO ='" + _sNumber + "' ORDER BY RECORD_ENTRY_DATE ";

    //    _dt = dmlgetqueryYotta(_sSql);

    //    if (_dt.Rows.Count > 0)
    //    {

    //        DataTable ISUSTDTable = new DataTable();
    //        DataColumn column = new DataColumn();

    //        ISUSTDTable.Columns.Add("DOCUMENTID", typeof(string));
    //        ISUSTDTable.Columns.Add("DOCUMENTNAME", typeof(string));
    //        ISUSTDTable.Columns.Add("DOCUMENTTYPE", typeof(string));
    //        ISUSTDTable.Columns.Add("RECORD_ENTRY_DATE", typeof(string));

    //        DataRow dr;

    //        for (int i = 0; i < _dt.Rows.Count; i++)
    //        {
    //            dr = ISUSTDTable.NewRow();
    //            dr["DOCUMENTID"] = _dt.Rows[i]["DOCUMENTID"].ToString();
    //            dr["DOCUMENTNAME"] = _dt.Rows[i]["DOCUMENTNAME"].ToString();
    //            dr["DOCUMENTTYPE"] = _dt.Rows[i]["DOCUMENTTYPE"].ToString();
    //            dr["RECORD_ENTRY_DATE"] = _dt.Rows[i]["RECORD_ENTRY_DATE"].ToString();

    //            ISUSTDTable.Rows.Add(dr);
    //        }

    //        ISUSTDTable.AcceptChanges();
    //        ds.Tables.Add(ISUSTDTable);
    //        ds.Tables[0].TableName = "ISUSTDTable";
    //        ds.DataSetName = "BAPI_RESULT";

    //    }

    //    return ds;
    //}

    //public DataTable dmlgetqueryYotta(string sql)
    //{
    //    DataTable dt = new DataTable();
    //    OleDbCommand dbcommand;
    //    OleDbTransaction dbtrans;
    //    NDS objNDS = new NDS();
    //    OleDbDataAdapter da;

    //    OleDbConnection ocon = new OleDbConnection(objNDS.conYotta());
    //    try
    //    {
    //        if (ocon.State == ConnectionState.Closed)
    //        {
    //            ocon.Open();
    //        }
    //        dbcommand = new OleDbCommand();
    //        dbcommand.Connection = ocon;
    //        da = new OleDbDataAdapter();
    //        da.SelectCommand = dbcommand;
    //        dt = null;
    //        dt = new DataTable();
    //        dbcommand.CommandType = CommandType.Text;
    //        dbcommand.CommandText = sql;
            
    //        da.Fill(dt);
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        return dt;
    //    }
    //    finally
    //    {
    //        if (ocon.State == ConnectionState.Open)
    //        {
    //            ocon.Close();
    //            ocon.Dispose();
    //        }
    //    }
    //}
    [WebMethod]
    public DataSet ZBAPI_CA_MCD_DETAILS(string caNumber, string companyCode)
    {
        DataSet dataTablereponse = new DataSet();
       // dataTablereponse.TableName = "CA_MCD_DETAILS";

        
            dataTablereponse = obj.GETZBAPI_CA_MCD_DETAILS(caNumber, companyCode);
            return dataTablereponse;

        
       
    }

    [WebMethod]
    public DataSet ZBAPI_PREPAID_RTGS_Common(string strCOMP_CODE, string strCONTRACT_ACCOUNT, string strACCOUNT_TYPE, string strAMOUNT, string strFLAG)
    {
        DataSet dsBAPIOutput = obj.get_ZBAPI_PREPAID_RTGS_Common(strCOMP_CODE, strCONTRACT_ACCOUNT, strACCOUNT_TYPE, strAMOUNT, strFLAG);
        return dsBAPIOutput;
    }

    //code added by AANSHU for mcd solrData against division 
    [WebMethod]
    public void mcdSolrData(string division)
    {

        string solrUrl = string.Empty;
        string expectedApiKey = "solrbses@2024!"; // Define your static API key
        string providedApiKey = Context.Request.Headers["X-Api-Key"]; // Read the API key from request headers

        // Check if the API key is present and correct
        if (string.IsNullOrEmpty(providedApiKey) || providedApiKey != expectedApiKey)
        {
            Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // Set status code to 401 Unauthorized
            Context.Response.Write("Unauthorized access. Invalid API key.");
            return;
        }
        if (!string.IsNullOrEmpty(division))
        {
            try
            {
                var client = new WebClient();
                if (division.Length > 0)
                {
                    solrUrl = "http://10.125.126.72:8983/solr/TFCF/select?fq=BP_TYPE:\"Sealing\"&indent=true&q.op=OR&q=DIVISION:" + division + "&&rows=1000000";
                }


                List<DocsInfo> documents = new List<DocsInfo>();
                var jsonString = client.DownloadString(solrUrl);
                var solrResponse = JsonConvert.DeserializeObject<RootObject>(jsonString);
                if (solrResponse != null && solrResponse.response.docs.Count > 0)
                {

                    //foreach (var doc in solrResponse.response.docs)
                    //{
                    foreach (var doc in solrResponse.response.docs)
                    {
                        DocsInfo document = new DocsInfo();

                        // Check if the keys exist in the dictionary before accessing them
                        if (doc.ContainsKey("MOVE_OUT"))
                            document.MOVE_OUT = doc["MOVE_OUT"].ToString();
                        if (doc.ContainsKey("CONNECTION_TYPE"))
                            document.CONNECTION_TYPE = doc["CONNECTION_TYPE"].ToString();
                        if (doc.ContainsKey("ACTIVE"))
                            document.ACTIVE = doc["ACTIVE"].ToString();
                        if (doc.ContainsKey("SANCTIONED_LOAD"))
                            document.SANCTIONED_LOAD = doc["SANCTIONED_LOAD"].ToString();
                        if (doc.ContainsKey("RATE_CATEGORY"))
                            document.RATE_CATEGORY = doc["RATE_CATEGORY"].ToString();
                        if (doc.ContainsKey("BP_TYPE"))
                            document.BP_TYPE = doc["BP_TYPE"].ToString();
                        if (doc.ContainsKey("SAP_POLE_ID"))
                            document.SAP_POLE_ID = doc["SAP_POLE_ID"].ToString();
                        if (doc.ContainsKey("LAST_NAME"))
                            document.LAST_NAME = doc["LAST_NAME"].ToString();
                        if (doc.ContainsKey("MRU"))
                            document.MRU = doc["MRU"].ToString();
                        if (doc.ContainsKey("SUB_DIVISION"))
                            document.SUB_DIVISION = doc["SUB_DIVISION"].ToString();
                        if (doc.ContainsKey("DEVICE_NO"))
                            document.DEVICE_NO = doc["DEVICE_NO"].ToString();
                        if (doc.ContainsKey("INSTALLATION_TYPE"))
                            document.INSTALLATION_TYPE = doc["INSTALLATION_TYPE"].ToString();
                        if (doc.ContainsKey("CONSUMER_STATUS"))
                            document.CONSUMER_STATUS = doc["CONSUMER_STATUS"].ToString();
                        if (doc.ContainsKey("COMPANY_CODE"))
                            document.COMPANY_CODE = doc["COMPANY_CODE"].ToString();
                        if (doc.ContainsKey("CSTS_CD"))
                            document.CSTS_CD = doc["CSTS_CD"].ToString();
                        if (doc.ContainsKey("CONTRACT_ACCOUNT"))
                            document.CONTRACT_ACCOUNT = doc["CONTRACT_ACCOUNT"].ToString();
                        if (doc.ContainsKey("SAP_DEPARTMENT"))
                            document.SAP_DEPARTMENT = doc["SAP_DEPARTMENT"].ToString();
                        if (doc.ContainsKey("SEQUENCE_NO"))
                            document.SEQUENCE_NO = doc["SEQUENCE_NO"].ToString();
                        if (doc.ContainsKey("BILL_DISPATCH_CONTROL"))
                            document.BILL_DISPATCH_CONTROL = doc["BILL_DISPATCH_CONTROL"].ToString();
                        if (doc.ContainsKey("SAP_DIVISION"))
                            document.SAP_DIVISION = doc["SAP_DIVISION"].ToString();
                        if (doc.ContainsKey("ACCOUNT_CLASS"))
                            document.ACCOUNT_CLASS = doc["ACCOUNT_CLASS"].ToString();
                        if (doc.ContainsKey("MOBILE_NO"))
                            document.MOBILE_NO = doc["MOBILE_NO"].ToString();
                        if (doc.ContainsKey("SAP_NAME"))
                            document.SAP_NAME = doc["SAP_NAME"].ToString();
                        if (doc.ContainsKey("TARIFF"))
                            document.TARIFF = doc["TARIFF"].ToString();
                        if (doc.ContainsKey("CONS_REF"))
                            document.CONS_REF = doc["CONS_REF"].ToString();
                        if (doc.ContainsKey("SAP_ADDRESS"))
                            document.SAP_ADDRESS = doc["SAP_ADDRESS"].ToString();
                        if (doc.ContainsKey("MOVE_IN"))
                            document.MOVE_IN = doc["MOVE_IN"].ToString();
                        if (doc.ContainsKey("POLE_NO"))
                            document.MOVE_IN = doc["POLE_NO"].ToString();
                        if (doc.ContainsKey("BUSINESS_PARTNER"))
                            document.BUSINESS_PARTNER = doc["BUSINESS_PARTNER"].ToString();
                        else
                            document.BUSINESS_PARTNER = "";
                        if (doc.ContainsKey("EMAIL"))
                            document.Email = doc["EMAIL"].ToString();
                        else
                            document.Email = "";

                        documents.Add(document);
                    }



                    //DocsInfo document = new DocsInfo
                    //{
                    //    MOVE_OUT = doc["MOVE_OUT"].ToString(),
                    //    CONNECTION_TYPE = doc["CONNECTION_TYPE"].ToString(),
                    //    ACTIVE = doc["ACTIVE"].ToString(),
                    //    SANCTIONED_LOAD = doc["SANCTIONED_LOAD"].ToString(),
                    //    RATE_CATEGORY = doc["RATE_CATEGORY"].ToString(),
                    //    BP_TYPE = doc["BP_TYPE"].ToString(),
                    //    SAP_POLE_ID = doc["SAP_POLE_ID"].ToString(),
                    //    LAST_NAME = doc["LAST_NAME"].ToString(),
                    //    TEL_NUMBER = doc["TEL_NUMBER"].ToString(),
                    //    DIVISION = doc["DIVISION"].ToString(),
                    //    FIRST_NAME = doc["FIRST_NAME"].ToString(),
                    //    MRU = doc["MRU"].ToString(),
                    //    SUB_DIVISION = doc["SUB_DIVISION"].ToString(),
                    //    DEVICE_NO = doc["DEVICE_NO"].ToString(),
                    //    INSTALLATION_TYPE = doc["INSTALLATION_TYPE"].ToString(),
                    //    CONSUMER_STATUS = doc["CONSUMER_STATUS"].ToString(),
                    //    COMPANY_CODE = doc["COMPANY_CODE"].ToString(),
                    //    CSTS_CD = doc["CSTS_CD"].ToString(),
                    //    CONTRACT_ACCOUNT = doc["CONTRACT_ACCOUNT"].ToString(),
                    //    SAP_DEPARTMENT = doc["SAP_DEPARTMENT"].ToString(),
                    //    SEQUENCE_NO = doc["SEQUENCE_NO"].ToString(),
                    //    BILL_DISPATCH_CONTROL = doc["BILL_DISPATCH_CONTROL"].ToString(),
                    //    SAP_DIVISION = doc["SAP_DIVISION"].ToString(),
                    //    ACCOUNT_CLASS = doc["ACCOUNT_CLASS"].ToString(),
                    //    MOBILE_NO = doc["MOBILE_NO"].ToString(),
                    //    SAP_NAME = doc["SAP_NAME"].ToString(),
                    //    TARIFF = doc["TARIFF"].ToString(),
                    //    CONS_REF = doc["CONS_REF"].ToString(),
                    //    SAP_ADDRESS = doc["SAP_ADDRESS"].ToString(),
                    //    MOVE_IN = doc["MOVE_IN"].ToString(),
                    //    POLE_NO = doc["POLE_NO"].ToString(),
                    //    BUSINESS_PARTNER = doc["BUSINESS_PARTNER"].ToString(),
                    //    // Map other properties as needed
                    //};
                    //documents.Add(document);
                    ///}



                    Context.Response.ContentType = "application/xml"; // Set content type of response
                    var xmlSerializer = new XmlSerializer(typeof(List<DocsInfo>));
                    xmlSerializer.Serialize(Context.Response.OutputStream, documents);
                    //Context.Response.Write(JsonConvert.SerializeObject(new { data = solrResponse.response.docs, message = "Solr search successful!" }));
                }
                else
                {
                    Context.Response.StatusCode = (int)HttpStatusCode.NotFound; // Set status code to 404
                    Context.Response.Write("No records found");
                }

            }
            catch (Exception ex)
            {
                Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Set status code to 500
                Context.Response.Write("An error occurred while fetching data from Solr:" + ex.Message);
            }
        }
        else
        {
            Context.Response.StatusCode = (int)HttpStatusCode.NotFound; // Set status code to 404
            Context.Response.Write("Invalid meter number/CA Number/Mobile no..");
        }
    }


    [WebMethod]
    public DataSet ZBAPI_DM_SLA_DATA()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtZBAPI = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("MESSAGE", typeof(string));

        try
        {
            string _sCREATION_DATE = string.Empty, _sBASIC_START_DATE = string.Empty, _sBASIC_FINISH_DATE = string.Empty;
            string _sACTIVITY_DATE = string.Empty, _sNEW_METER_INST_DATE = string.Empty, _sZREM_METER_DATE = string.Empty;
            string _sInputDate = string.Empty, _sMonth = string.Empty;

            _sInputDate = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToString();
            //_sInputDate ="20180113";
           // _sInputDate = "20181013";

            //for (int j = 2; j < 10; j++)
            {
                ds = obj.Get_ZBAPI_DM_SLA_DATA(_sInputDate);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtZBAPI = ds.Tables[0];
                    if (ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                    {
                        dt.Rows.Add("01", "Mis-Match Data in SAP");
                        ds.Tables.Add(dt);
                    }

                    if (dtZBAPI.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtZBAPI.Rows.Count; i++)
                        {
                            _sCREATION_DATE = Convert.ToString(dtZBAPI.Rows[i]["CREATION_DATE"]);

                            if ((_sCREATION_DATE.Trim().Length == 10) && (_sCREATION_DATE.Trim() != "0000-00-00"))
                            {
                                if (_sCREATION_DATE.Substring(5, 2).Trim() == "01")
                                    _sMonth = "Jan";
                                else if (_sCREATION_DATE.Substring(5, 2).Trim() == "02")
                                    _sMonth = "Feb";
                                else if (_sCREATION_DATE.Substring(5, 2).Trim() == "03")
                                    _sMonth = "Mar";
                                else if (_sCREATION_DATE.Substring(5, 2).Trim() == "04")
                                    _sMonth = "Apr";
                                else if (_sCREATION_DATE.Substring(5, 2).Trim() == "05")
                                    _sMonth = "May";
                                else if (_sCREATION_DATE.Substring(5, 2).Trim() == "06")
                                    _sMonth = "Jun";
                                else if (_sCREATION_DATE.Substring(5, 2).Trim() == "07")
                                    _sMonth = "Jul";
                                else if (_sCREATION_DATE.Substring(5, 2).Trim() == "08")
                                    _sMonth = "Aug";
                                else if (_sCREATION_DATE.Substring(5, 2).Trim() == "09")
                                    _sMonth = "Sep";
                                else if (_sCREATION_DATE.Substring(5, 2).Trim() == "10")
                                    _sMonth = "Oct";
                                else if (_sCREATION_DATE.Substring(5, 2).Trim() == "11")
                                    _sMonth = "Nov";
                                else if (_sCREATION_DATE.Substring(5, 2).Trim() == "12")
                                    _sMonth = "Dec";

                                _sCREATION_DATE = _sCREATION_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _sCREATION_DATE.Substring(0, 4);
                            }

                            _sBASIC_START_DATE = Convert.ToString(dtZBAPI.Rows[i]["BASIC_START_DATE"]);
                            if ((_sBASIC_START_DATE.Trim().Length == 10) && (_sBASIC_START_DATE.Trim() != "0000-00-00"))
                            {
                                if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "01")
                                    _sMonth = "Jan";
                                else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "02")
                                    _sMonth = "Feb";
                                else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "03")
                                    _sMonth = "Mar";
                                else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "04")
                                    _sMonth = "Apr";
                                else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "05")
                                    _sMonth = "May";
                                else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "06")
                                    _sMonth = "Jun";
                                else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "07")
                                    _sMonth = "Jul";
                                else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "08")
                                    _sMonth = "Aug";
                                else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "09")
                                    _sMonth = "Sep";
                                else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "10")
                                    _sMonth = "Oct";
                                else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "11")
                                    _sMonth = "Nov";
                                else if (_sBASIC_START_DATE.Substring(5, 2).Trim() == "12")
                                    _sMonth = "Dec";

                                _sBASIC_START_DATE = _sBASIC_START_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _sBASIC_START_DATE.Substring(0, 4);
                            }

                            _sBASIC_FINISH_DATE = Convert.ToString(dtZBAPI.Rows[i]["BASIC_FINISH_DATE"]);
                            if ((_sBASIC_FINISH_DATE.Trim().Length == 10) && (_sBASIC_FINISH_DATE.Trim() != "0000-00-00"))
                            {
                                if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "01")
                                    _sMonth = "Jan";
                                else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "02")
                                    _sMonth = "Feb";
                                else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "03")
                                    _sMonth = "Mar";
                                else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "04")
                                    _sMonth = "Apr";
                                else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "05")
                                    _sMonth = "May";
                                else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "06")
                                    _sMonth = "Jun";
                                else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "07")
                                    _sMonth = "Jul";
                                else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "08")
                                    _sMonth = "Aug";
                                else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "09")
                                    _sMonth = "Sep";
                                else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "10")
                                    _sMonth = "Oct";
                                else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "11")
                                    _sMonth = "Nov";
                                else if (_sBASIC_FINISH_DATE.Substring(5, 2).Trim() == "12")
                                    _sMonth = "Dec";

                                _sBASIC_FINISH_DATE = _sBASIC_FINISH_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _sBASIC_FINISH_DATE.Substring(0, 4);
                            }

                            _sACTIVITY_DATE = Convert.ToString(dtZBAPI.Rows[i]["ACTIVITY_DATE"]);
                            if ((_sACTIVITY_DATE.Trim().Length == 10) && (_sACTIVITY_DATE.Trim() != "0000-00-00") && (_sACTIVITY_DATE.Trim() != "0000.00.00"))
                            {
                                if (_sACTIVITY_DATE.Substring(3, 2).Trim() == "01")
                                    _sMonth = "Jan";
                                else if (_sACTIVITY_DATE.Substring(3, 2).Trim() == "02")
                                    _sMonth = "Feb";
                                else if (_sACTIVITY_DATE.Substring(3, 2).Trim() == "03")
                                    _sMonth = "Mar";
                                else if (_sACTIVITY_DATE.Substring(3, 2).Trim() == "04")
                                    _sMonth = "Apr";
                                else if (_sACTIVITY_DATE.Substring(3, 2).Trim() == "05")
                                    _sMonth = "May";
                                else if (_sACTIVITY_DATE.Substring(3, 2).Trim() == "06")
                                    _sMonth = "Jun";
                                else if (_sACTIVITY_DATE.Substring(3, 2).Trim() == "07")
                                    _sMonth = "Jul";
                                else if (_sACTIVITY_DATE.Substring(3, 2).Trim() == "08")
                                    _sMonth = "Aug";
                                else if (_sACTIVITY_DATE.Substring(3, 2).Trim() == "09")
                                    _sMonth = "Sep";
                                else if (_sACTIVITY_DATE.Substring(3, 2).Trim() == "10")
                                    _sMonth = "Oct";
                                else if (_sACTIVITY_DATE.Substring(3, 2).Trim() == "11")
                                    _sMonth = "Nov";
                                else if (_sACTIVITY_DATE.Substring(3, 2).Trim() == "12")
                                    _sMonth = "Dec";

                                _sACTIVITY_DATE = _sACTIVITY_DATE.Substring(0, 2) + "-" + _sMonth + "-" + _sACTIVITY_DATE.Substring(6, 4);
                            }

                            _sNEW_METER_INST_DATE = Convert.ToString(dtZBAPI.Rows[i]["NEW_METER_INST_DATE"]);
                            if ((_sNEW_METER_INST_DATE.Trim().Length == 10) && (_sNEW_METER_INST_DATE.Trim() != "0000-00-00"))
                            {
                                if (_sNEW_METER_INST_DATE.Substring(5, 2).Trim() == "01")
                                    _sMonth = "Jan";
                                else if (_sNEW_METER_INST_DATE.Substring(5, 2).Trim() == "02")
                                    _sMonth = "Feb";
                                else if (_sNEW_METER_INST_DATE.Substring(5, 2).Trim() == "03")
                                    _sMonth = "Mar";
                                else if (_sNEW_METER_INST_DATE.Substring(5, 2).Trim() == "04")
                                    _sMonth = "Apr";
                                else if (_sNEW_METER_INST_DATE.Substring(5, 2).Trim() == "05")
                                    _sMonth = "May";
                                else if (_sNEW_METER_INST_DATE.Substring(5, 2).Trim() == "06")
                                    _sMonth = "Jun";
                                else if (_sNEW_METER_INST_DATE.Substring(5, 2).Trim() == "07")
                                    _sMonth = "Jul";
                                else if (_sNEW_METER_INST_DATE.Substring(5, 2).Trim() == "08")
                                    _sMonth = "Aug";
                                else if (_sNEW_METER_INST_DATE.Substring(5, 2).Trim() == "09")
                                    _sMonth = "Sep";
                                else if (_sNEW_METER_INST_DATE.Substring(5, 2).Trim() == "10")
                                    _sMonth = "Oct";
                                else if (_sNEW_METER_INST_DATE.Substring(5, 2).Trim() == "11")
                                    _sMonth = "Nov";
                                else if (_sNEW_METER_INST_DATE.Substring(5, 2).Trim() == "12")
                                    _sMonth = "Dec";

                                _sNEW_METER_INST_DATE = _sNEW_METER_INST_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _sNEW_METER_INST_DATE.Substring(0, 4);
                            }

                            _sZREM_METER_DATE = Convert.ToString(dtZBAPI.Rows[i]["ZREM_METER_DATE"]);
                            if ((_sZREM_METER_DATE.Trim().Length == 10) && (_sZREM_METER_DATE.Trim() != "0000-00-00"))
                            {
                                if (_sZREM_METER_DATE.Substring(5, 2).Trim() == "01")
                                    _sMonth = "Jan";
                                else if (_sZREM_METER_DATE.Substring(5, 2).Trim() == "02")
                                    _sMonth = "Feb";
                                else if (_sZREM_METER_DATE.Substring(5, 2).Trim() == "03")
                                    _sMonth = "Mar";
                                else if (_sZREM_METER_DATE.Substring(5, 2).Trim() == "04")
                                    _sMonth = "Apr";
                                else if (_sZREM_METER_DATE.Substring(5, 2).Trim() == "05")
                                    _sMonth = "May";
                                else if (_sZREM_METER_DATE.Substring(5, 2).Trim() == "06")
                                    _sMonth = "Jun";
                                else if (_sZREM_METER_DATE.Substring(5, 2).Trim() == "07")
                                    _sMonth = "Jul";
                                else if (_sZREM_METER_DATE.Substring(5, 2).Trim() == "08")
                                    _sMonth = "Aug";
                                else if (_sZREM_METER_DATE.Substring(5, 2).Trim() == "09")
                                    _sMonth = "Sep";
                                else if (_sZREM_METER_DATE.Substring(5, 2).Trim() == "10")
                                    _sMonth = "Oct";
                                else if (_sZREM_METER_DATE.Substring(5, 2).Trim() == "11")
                                    _sMonth = "Nov";
                                else if (_sZREM_METER_DATE.Substring(5, 2).Trim() == "12")
                                    _sMonth = "Dec";

                                _sZREM_METER_DATE = _sZREM_METER_DATE.Substring(8, 2) + "-" + _sMonth + "-" + _sZREM_METER_DATE.Substring(0, 4);
                            }

                            if (_sCREATION_DATE.Trim() == "0000-00-00")
                                _sCREATION_DATE = "";
                            if (_sBASIC_START_DATE.Trim() == "0000-00-00")
                                _sBASIC_START_DATE = "";
                            if (_sBASIC_FINISH_DATE.Trim() == "0000-00-00")
                                _sBASIC_FINISH_DATE = "";
                            if (_sACTIVITY_DATE.Trim() == "0000-00-00" || _sACTIVITY_DATE.Trim() == "0000.00.00")
                                _sACTIVITY_DATE = "";
                            if (_sNEW_METER_INST_DATE.Trim() == "0000-00-00")
                                _sNEW_METER_INST_DATE = "";
                            if (_sZREM_METER_DATE.Trim() == "0000-00-00")
                                _sZREM_METER_DATE = "";


                            Insert_ZBAPI_DM_SLA_SAP_DATA(Convert.ToString(dtZBAPI.Rows[i]["ORDER_NO"]), Convert.ToString(dtZBAPI.Rows[i]["BP"]), Convert.ToString(dtZBAPI.Rows[i]["CA"]),
                            Convert.ToString(dtZBAPI.Rows[i]["COMPANY_CODE"]), _sCREATION_DATE, _sBASIC_START_DATE, _sBASIC_FINISH_DATE, _sACTIVITY_DATE, Convert.ToString(dtZBAPI.Rows[i]["DIVISION"]),
                            Convert.ToString(dtZBAPI.Rows[i]["ORDER_STATUS"]), Convert.ToString(dtZBAPI.Rows[i]["ORDER_TYPE"]), Convert.ToString(dtZBAPI.Rows[i]["PG"]), Convert.ToString(dtZBAPI.Rows[i]["PM_ACTIVITY"]),
                             Convert.ToString(dtZBAPI.Rows[i]["VENDOR_CODE"]), Convert.ToString(dtZBAPI.Rows[i]["NEW_METER_NUMBER"]), Convert.ToString(dtZBAPI.Rows[i]["NEW_METER_STATUS"]),
                             _sNEW_METER_INST_DATE, Convert.ToString(dtZBAPI.Rows[i]["REM_METER_NUMBER"]), _sZREM_METER_DATE, Convert.ToString(dtZBAPI.Rows[i]["REM_METER_STATUS"]),
                            Convert.ToString(dtZBAPI.Rows[i]["PM_CABLE_LENGTH"]), Convert.ToString(dtZBAPI.Rows[i]["PM_CABLE_SIZE"]),
                            Convert.ToString(dtZBAPI.Rows[i]["BM_CABLE_LENGTH"]), Convert.ToString(dtZBAPI.Rows[i]["BM_CABLE_SIZE"]), Convert.ToString(dtZBAPI.Rows[i]["OP_CABLE_LENGTH"]),
                            Convert.ToString(dtZBAPI.Rows[i]["OP_CABLE_SIZE"]), Convert.ToString(dtZBAPI.Rows[i]["BB_INST_STATUS"]), Convert.ToString(dtZBAPI.Rows[i]["BB_INST_SIZE"]),
                            Convert.ToString(dtZBAPI.Rows[i]["REM_CABLE_LENGTH"]), Convert.ToString(dtZBAPI.Rows[i]["REM_CABLE_SIZE"]), Convert.ToString(dtZBAPI.Rows[i]["BUS_BAR_NO"]),
                            Convert.ToString(dtZBAPI.Rows[i]["OH_UG"]), Convert.ToString(dtZBAPI.Rows[i]["TERMINAL_SEAL1"]), Convert.ToString(dtZBAPI.Rows[i]["TERMINAL_SEAL2"]),
                            Convert.ToString(dtZBAPI.Rows[i]["METERBOXSEAL1"]), Convert.ToString(dtZBAPI.Rows[i]["METERBOXSEAL2"]), Convert.ToString(dtZBAPI.Rows[i]["BUSBARSEAL1"]),
                            Convert.ToString(dtZBAPI.Rows[i]["BUSBARSEAL2"]), Convert.ToString(dtZBAPI.Rows[i]["REMOVED_SEAL1"]), Convert.ToString(dtZBAPI.Rows[i]["REMOVED_SEAL2"]), _sInputDate);
                        }
                    }
                }
            }
            return ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add("01", ex.Message.ToString());
            ds.Tables.Add(dt);
            return ds;
        }
    }

    private bool Insert_ZBAPI_DM_SLA_SAP_DATA(string _ORDER_NO, string _BP, string _CA, string _COMPANY_CODE, string _CREATION_DATE, string _BASIC_START_DATE, string _BASIC_FINISH_DATE, string _ACTIVITY_DATE,
                                            string _DIVISION, string _ORDER_STATUS, string _ORDER_TYPE, string _PG, string _PM_ACTIVITY, string _VENDOR_CODE, string _NEW_METER_NUMBER, string _NEW_METER_STATUS,
                                            string _NEW_METER_INST_DATE, string _REM_METER_NUMBER, string _ZREM_METER_DATE, string _REM_METER_STATUS, string _PM_CABLE_LENGTH, string _PM_CABLE_SIZE,
                                            string _BM_CABLE_LENG, string _BM_CABLE_SIZE, string _OP_CABLE_LENG, string _OP_CABLE_SIZE, string _BB_INST_STATUS, string _BB_INST_SIZE, string _REM_CABLE_LENGTH,
                                            string _REM_CABLE_SIZE, string _BUS_BAR_NO, string _OH_UG, string _TERMINAL_SEAL1, string _TERMINAL_SEAL2, string _METERBOXSEAL1, string _METERBOXSEAL2,
                                            string _BUSBARSEAL1, string _BUSBARSEAL2, string _REMOVED_SEAL1, string _REMOVED_SEAL2, string _FetchDate)
    {

        string sql = " INSERT INTO MOBINT.MCR_DETAILS_SAP_DATA(ORDER_NO,BP,CA,COMPANY_CODE,CREATION_DATE,BASIC_START_DATE,BASIC_FINISH_DATE,ACTIVITY_DATE,DIVISION,ORDER_STATUS,         ";
        sql += " ORDER_TYPE,PG,PM_ACTIVITY,VENDOR_CODE,NEW_METER_NUMBER,NEW_METER_STATUS,NEW_METER_INST_DATE,REM_METER_NUMBER,ZREM_METER_DATE, ";
        sql += " REM_METER_STATUS,PM_CABLE_LENGTH,PM_CABLE_SIZE,BM_CABLE_LENG,BM_CABLE_SIZE,OP_CABLE_LENG,OP_CABLE_SIZE,BB_INST_STATUS, ";
        sql += " BB_INST_SIZE,REM_CABLE_LENGTH,REM_CABLE_SIZE,BUS_BAR_NO,OH_UG,TERMINAL_SEAL1,TERMINAL_SEAL2,METERBOXSEAL1,METERBOXSEAL2, ";
        sql += " BUSBARSEAL1,BUSBARSEAL2,REMOVED_SEAL1,REMOVED_SEAL2,FETCH_DATA_DT) VALUES ";

        sql += "   ('" + _ORDER_NO + "', '" + _BP + "', '" + _CA + "', '" + _COMPANY_CODE + "',TO_DATE('" + _CREATION_DATE + "'),TO_DATE('" + _BASIC_START_DATE + "'),TO_DATE('" + _BASIC_FINISH_DATE + "'), ";

        if (_ACTIVITY_DATE.Trim() != "")
        {
            if (_ACTIVITY_DATE.Trim().Substring(0, 2) != "00")
                sql += " TO_DATE('" + _ACTIVITY_DATE + "'),";
            else
                sql += " NULL,";
        }
        else
            sql += " NULL,";

        sql += "     '" + _DIVISION + "', '" + _ORDER_STATUS + "',  '" + _ORDER_TYPE + "',  '" + _PG + "', '" + _PM_ACTIVITY + "',  '" + _VENDOR_CODE + "',  '" + _NEW_METER_NUMBER + "', '" + _NEW_METER_STATUS + "',    ";

        if (_NEW_METER_INST_DATE.Trim() != "")
        {
            if (_NEW_METER_INST_DATE.Trim().Substring(0, 2) != "00")
                sql += " TO_DATE('" + _NEW_METER_INST_DATE + "'), ";
            else
                sql += " NULL,";
        }
        else
            sql += " NULL,";

        sql += "   '" + _REM_METER_NUMBER + "', ";

        if (_ZREM_METER_DATE.Trim() != "")
        {
            if (_ZREM_METER_DATE.Trim().Substring(0, 2) != "00")
                sql += " TO_DATE('" + _ZREM_METER_DATE + "'), ";
            else
                sql += " NULL,";
        }
        else
            sql += " NULL,";

        sql += " '" + _REM_METER_STATUS + "',  '" + _PM_CABLE_LENGTH + "', '" + _PM_CABLE_SIZE + "',  '" + _BM_CABLE_LENG + "',  '" + _BM_CABLE_SIZE + "', '" + _OP_CABLE_LENG + "',  ";
        sql += "     '" + _OP_CABLE_SIZE + "', '" + _BB_INST_STATUS + "',  '" + _BB_INST_SIZE + "',  '" + _REM_CABLE_LENGTH + "', '" + _REM_CABLE_SIZE + "',  '" + _BUS_BAR_NO + "',  '" + _OH_UG + "', ";
        sql += " '" + _TERMINAL_SEAL1 + "','" + _TERMINAL_SEAL2 + "','" + _METERBOXSEAL1 + "','" + _METERBOXSEAL2 + "','" + _BUSBARSEAL1 + "','" + _BUSBARSEAL2 + "','" + _REMOVED_SEAL1 + "','" + _REMOVED_SEAL2 + "','" + _FetchDate + "' )  ";

        return dml_Mobintsinglequery(sql);
    }

    //Added prasoon dated 28082024 --Namdev sir
    [WebMethod]
    public DataSet ZBAPI_PM_LT_FL_EQ(string _SWERK, string _BEBER, string _TPLNR, string _PLTXT)
    {
        DataSet Ds = obj.get_ZBAPI_PM_LT_FL_EQ(_SWERK, _BEBER, _TPLNR, _PLTXT);
        return Ds;
    }

    [WebMethod]
    public DataSet ZBAPI_PM_LT_LOAD(string _SWERK, string _BEBER, string _TPLNR, string _EQUNR_DTR, string _EQUNR_LTACB_SAP, string _LDATE, string _LTIME, string _FING, string _PLTXT, string _LTFDR_SAP, string _EQUNR_LTACB_SITE, string _LTFDR_SITE, string _CABLE1, string _CABLE2, string _R_PH_LD, string _Y_PH_LD, string _B_PH_LD, string _NEUT_LT, string _REMARK, string _LNAME, string _ADDLTACB, string _LUSID)
    {
        DataSet Ds = obj.ZBAPI_PM_LT_LOAD(_SWERK, _BEBER, _TPLNR, _EQUNR_DTR, _EQUNR_LTACB_SAP, _LDATE, _LTIME, _FING, _PLTXT, _LTFDR_SAP, _EQUNR_LTACB_SITE, _LTFDR_SITE, _CABLE1, _CABLE2, _R_PH_LD, _Y_PH_LD, _B_PH_LD, _NEUT_LT, _REMARK, _LNAME, _ADDLTACB, _LUSID);
        return Ds;
    }
    //Added prasoon dated 28082024 --Namdev sir

}
