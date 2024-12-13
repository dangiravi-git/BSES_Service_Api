using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.OleDb;
using Recovery_Webservice.Models;
using Recovery_Webservice.BusinessLogicLayer;
using System.Configuration;
using Recovery_Webservice.BusinessLogicLayer.Security;

namespace Recovery_Webservice.Controllers
{
    public class RecoveryController : ApiController
    {
        HttpResponseMessage Msg;
        response response = new response();
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataAdapter adp;
        public readonly string PublicAccessTokenKey;
        public RecoveryController()
        {
            PublicAccessTokenKey = ConfigurationManager.AppSettings["InternalAccessTokenKey"];
        }
        static void Main(string[] args)
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string ipAddressN = context.Request.ServerVariables["REMOTE_ADDR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        //public DataTable ChkRecordExist(string _sCANumber, string _sFEID, string _sChkInTable)
        //{
        //    string sql = "";
        //    if (_sChkInTable == "DEFLTR_ATR")
        //        sql = "SELECT * FROM DEFLTR_ATR WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
        //    using (con = new OleDbConnection(strConnection))
        //    {
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        con.Open();
        //        OleDbCommand cmd = new OleDbCommand();
        //        DataTable dt = new DataTable();
        //        OleDbDataAdapter oda = new OleDbDataAdapter();
        //        cmd.Connection = con;
        //        cmd = new OleDbCommand(sql, con);
        //        oda = new OleDbDataAdapter(cmd);
        //        oda.Fill(dt);
        //        return dt;
        //    }
        //}
        //public bool InsertTable(string _sCANumber, string _sStatus, string _sFEID, string _sIPAddr, string _sInsInTable)
        //{
        //    string sql = "";
        //    System.Web.HttpContext context = System.Web.HttpContext.Current;
        //    string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
        //    using (con = new OleDbConnection(strConnection))
        //    {
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        con.Open();
        //        if (_sInsInTable == "DEFLTR_ATR")
        //        {
        //            sql = "INSERT INTO DEFLTR_ATR (CA_NUMBER, UPDATION_ID, ASSIGN_USER_CODE, STATUS, ASSIGN_BY, TAB_ENTRY_DATE, TAB_UPD_BY, TAB_IPADDR)";
        //            sql += " SELECT UNIQUE CA_NUMBER,UPDATION_ID_DTLS,ASSIGN_USER_CODE,'" + _sStatus + "' STATUS,ASSIGN_BY,SYSDATE,'" + _sFEID + "' TAB_UPD_BY,'" + _sIPAddr + "' TAB_IP";
        //            sql += " FROM DEFLTR_CA_ASSIGN_LINK_DTLS WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(SYSDATE-30,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
        //            OleDbCommand cmd = new OleDbCommand();
        //            DataTable dt = new DataTable();
        //            OleDbDataAdapter oda = new OleDbDataAdapter();
        //            cmd.Connection = con;
        //            cmd = new OleDbCommand(sql, con);
        //            oda = new OleDbDataAdapter(cmd);
        //            oda.SelectCommand.CommandText = "INSERT INTO DEFLTR_ATR (CA_NUMBER, UPDATION_ID, ASSIGN_USER_CODE, STATUS, ASSIGN_BY, TAB_ENTRY_DATE, TAB_UPD_BY, TAB_IPADDR) SELECT UNIQUE CA_NUMBER,UPDATION_ID_DTLS,ASSIGN_USER_CODE,'" + _sStatus + "' STATUS,ASSIGN_BY,SYSDATE,'" + _sFEID + "' TAB_UPD_BY,'" + _sIPAddr + "' TAB_IP FROM DEFLTR_CA_ASSIGN_LINK_DTLS WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(SYSDATE-30,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
        //            dt.TableName = "DEFLTR_ATR";
        //            cmd.ExecuteNonQuery();
        //        }
        //        return true;
        //    }
        //}
        //public bool InsertSMS(string _sMobileNo, string _sMessage, string _sCANumber, string _sUpdationID, string _sIPAddress)
        //{
        //    string sql = "";
        //    sql = "INSERT INTO RECAPP_SMS (MOBILE_NO, MESSAGE_TXT, IP_ADDRESS, CA_NUMBER, UPDATION_ID)";
        //    sql += " VALUES ('" + _sMobileNo + "','" + _sMessage + "','" + _sIPAddress + "','" + _sCANumber + "','" + _sUpdationID + "')";

        //    using (con = new OleDbConnection(strConnection))
        //    {
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        con.Open();
        //        OleDbCommand cmd = new OleDbCommand();
        //        DataTable dt = new DataTable();
        //        OleDbDataAdapter oda = new OleDbDataAdapter();
        //        cmd.Connection = con;
        //        cmd = new OleDbCommand(sql, con);
        //        oda = new OleDbDataAdapter(cmd);
        //        oda.SelectCommand.CommandText = "INSERT INTO RECAPP_SMS (MOBILE_NO, MESSAGE_TXT, IP_ADDRESS, CA_NUMBER, UPDATION_ID) VALUES ('" + _sMobileNo + "','" + _sMessage + "','" + _sIPAddress + "','" + _sCANumber + "','" + _sUpdationID + "')";
        //        dt.TableName = "RECAPP_SMS";
        //        cmd.ExecuteNonQuery();
        //        return true;
        //    }
        //}

        //public bool UpdateTable(string _sCANumber, string _sStatus, string _sFEID, string _sIPAddr, string _sUpdInTable, string _sTabUpdStatus, string _sRemarks,
        //                      string _sImgFlg, string _sImg, string _sImage, string _sFollowDate, string _sAltContNo, string _sAltEmailID)
        //{
        //    string sql = "";
        //    using (con = new OleDbConnection(strConnection))
        //    {
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        con.Open();
        //        if (_sUpdInTable == "DEFLTR_ATR")
        //        {
        //            sql = "UPDATE DEFLTR_ATR SET STATUS = '" + _sStatus + "',TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "'";
        //            sql += " WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(SYSDATE-30,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";

        //            OleDbCommand cmd = new OleDbCommand();
        //            DataTable dt = new DataTable();
        //            OleDbDataAdapter oda = new OleDbDataAdapter();
        //            cmd.Connection = con;
        //            cmd = new OleDbCommand(sql, con);
        //            oda = new OleDbDataAdapter(cmd);
        //            oda.SelectCommand.CommandText = "UPDATE DEFLTR_ATR SET STATUS = '" + _sStatus + "', TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "' WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(SYSDATE-30,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
        //            dt.TableName = "DEFLTR_ATR";
        //            cmd.ExecuteNonQuery();
        //            return true;
        //        }
        //        else if (_sUpdInTable == "DEFLTR_CA_ASSIGN_LINK_DTLS")
        //        {
        //            try
        //            {
        //                if (_sImgFlg == "Y")
        //                {
        //                    byte[] _byImg = Convert.FromBase64String(_sImg);
        //                    _sImg = byteArrayToImage_Rec(_byImg, _sCANumber + DateTime.Now.ToString("yyyyMMdd"));

        //                    byte[] _byImg1 = Convert.FromBase64String(_sImage);
        //                    _sImage = byteArrayToImage_Rec(_byImg1, _sCANumber + "_1" + DateTime.Now.ToString("yyyyMMdd"));

        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                return false;
        //            }
        //            sql = "UPDATE DEFLTR_CA_ASSIGN_LINK_DTLS SET STATUS = '" + _sStatus + "',TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "',";
        //            sql += " TAB_UPD_STATUS = '" + _sTabUpdStatus + "', STATUS_REMARKS = '" + _sRemarks + "', TAB_IMAGE_FLG = '" + _sImgFlg + "', TAB_IMAGE_PATH = '" + _sImg + "',TAB_IMAGE_PATH1 = '" + _sImage + "',";
        //            sql += " FOLLOWUP_DATE = TO_DATE('" + _sFollowDate + "','dd/MM/yyyy'), ALT_CONTACTNO = '" + _sAltContNo + "', ALT_EMAILID = '" + _sAltEmailID + "', TAB_FLAG = 'Y'";
        //            sql += " WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(SYSDATE-30,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";

        //            OleDbCommand cmd = new OleDbCommand();
        //            DataTable dt = new DataTable();
        //            OleDbDataAdapter oda = new OleDbDataAdapter();
        //            cmd.Connection = con;
        //            cmd = new OleDbCommand(sql, con);
        //            oda = new OleDbDataAdapter(cmd);
        //            oda.SelectCommand.CommandText = "UPDATE DEFLTR_CA_ASSIGN_LINK_DTLS SET STATUS = '" + _sStatus + "', TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "', TAB_UPD_STATUS = '" + _sTabUpdStatus + "', STATUS_REMARKS = '" + _sRemarks + "', TAB_IMAGE_FLG = '" + _sImgFlg + "', TAB_IMAGE_PATH = '" + _sImg + "',TAB_IMAGE_PATH1 = '" + _sImage + "', FOLLOWUP_DATE = TO_DATE('" + _sFollowDate + "','dd/MM/yyyy'), ALT_CONTACTNO = '" + _sAltContNo + "', ALT_EMAILID = '" + _sAltEmailID + "', TAB_FLAG = 'Y' WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(SYSDATE-30,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
        //            dt.TableName = "DEFLTR_CA_ASSIGN_LINK_DTLS";
        //            cmd.ExecuteNonQuery();
        //            return true;
        //        }
        //    }
        //    con.Close();
        //    return true;
        //}

        [HttpPost]
        [Route("api/Recovery/auth/login")]
        public IHttpActionResult Login([FromBody] GetValidateUser login)
        {
            if (login._sUser == "admin" && login._sPass == "password")
            {
                var jwtTokenGenerator = new JwtTokenGenerator();
                var token = jwtTokenGenerator.GenerateToken(login._sUser, "Admin");
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        [Route("API/Recovery/InsertChequeDetails")]
        [HttpPost]
        public HttpResponseMessage InsertChequeDetails([FromBody] InsertChequeDetails icd)
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();

                DataTable dt = new DataTable();
                if (icd.KEY == PublicAccessTokenKey)
                {
                    using (con = new OleDbConnection(strConnection))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        sql = "INSERT INTO RECOVERY.DEFLTR_MOBILE_COLLECTION (CA_NUMBER,CHEQUE_DD_NO,CHEQUE_DD_AMOUNT,CHEQUE_DD_DATE,IMEI_NO,FE_ID)";
                        sql += " VALUES ('" + icd.CANUMBER + "','" + icd.CHEQUEDDNO + "','" + icd.CHEQUEDDAMOUNT + "',TO_DATE('" + icd.CHEQUEDDDATE + "','dd-mm-yyyy'),'" + icd.IMEI + "','" + icd.USERID + "')";
                        cmd = new OleDbCommand(sql, con);
                        adp = new OleDbDataAdapter(cmd);
                        adp.SelectCommand.CommandText = "INSERT INTO RECOVERY.DEFLTR_MOBILE_COLLECTION (CA_NUMBER,CHEQUE_DD_NO,CHEQUE_DD_AMOUNT,CHEQUE_DD_DATE,IMEI_NO,FE_ID) VALUES ('" + icd.CANUMBER + "','" + icd.CHEQUEDDNO + "','" + icd.CHEQUEDDAMOUNT + "',TO_DATE('" + icd.CHEQUEDDDATE + "','dd-mm-yyyy'),'" + icd.IMEI + "','" + icd.USERID + "')";
                        dt.TableName = "DEFLTR_MOBILE_COLLECTION";
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            response.Status = "Success";
                            response.Message = "Data inserted";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                        else
                        {
                            response.Status = "Failed";
                            response.Message = "Data not inserted";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [Route("API/Recovery/RecServices_ATR_Status")]
        [HttpPost]
        public HttpResponseMessage RecServices_ATR_Status([FromBody] RecServices_ATR_Status ras)
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (ras.strKeyParam == PublicAccessTokenKey)
                {
                    List<RecServices_ATR_Status> _RAS = new List<RecServices_ATR_Status>();
                    DataTable dt = new DataTable();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT '00' CODE,'-ACTION TAKEN-' DESCRIPTION FROM DUAL UNION SELECT CODE, DESCRIPTION FROM DEFLTR_COR_SYS_TYPE WHERE CORETYPE = 'RECOVRY_STATUS' AND ACCOUNT_CLASS IS NULL AND ACTIVE_FLAG = 'Y' ORDER BY DESCRIPTION";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecServices_ATR_Status r_ats = new RecServices_ATR_Status();
                                r_ats.CODE = dt.Rows[i]["CODE"].ToString();
                                r_ats.DESCRIPTION = dt.Rows[i]["DESCRIPTION"].ToString();
                                _RAS.Add(r_ats);
                                response.Data = _RAS;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [Route("API/Recovery/loginFE")]
        [HttpPost]
        public HttpResponseMessage loginFE([FromBody] loginFE l_fe)
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                DataTable dt = new DataTable();
                if (l_fe.strKeyParam == PublicAccessTokenKey)
                {
                    List<loginFE> _login = new List<loginFE>();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT UNIQUE FE_ID LOGIN_ID, PASSWRD PASSWORD, FE_NAME_DISP NAME, DIV_RIGHTS DIVCODE,(SELECT UNIQUE SAP_DIVISION_NAME";
                        sql += " FROM recovery.DEFLTR_DIVISIONS_MST WHERE TO_CHAR(DIV_RIGHTS) = TO_CHAR(SDO_CD)) DIV_RIGHTS, MOB_NO, FE_ADDRESS,";
                        sql += "(SELECT COUNT(*) FROM recovery.DEFLTR_LIST_MAIN A, recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1";
                        //sql += "  AND A.UPDATION_ID = (SELECT UNIQUE TO_CHAR(MAX(TO_DATE(UPDATION_ID || '01','YYYYMMDD')),'yyyyMM') FROM recovery.DEFLTR_LIST_MAIN)";
                        sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                        sql += "  AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC') AND UPPER(B.ASSIGN_USER_CODE) =UPPER('" + l_fe._sLogin + "')";
                        sql += " AND A.CA_NUMBER = B.CA_NUMBER(+) AND A.UPDATION_ID = B.UPDATION_ID_DTLS(+) 	AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS(+) )COUNT";
                        sql += " FROM recovery.DEFLTR_AGENCY_FE_MST WHERE UPPER(FE_ID) = UPPER('" + l_fe._sLogin + "') AND";
                        sql += " UPPER(PASSWRD) = UPPER('" + l_fe._sPassword + "') AND FE_CRITERIA_STATUS = 'IH' AND STATUS_FLAG2 = 'Y'";
                        OleDbCommand cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            JwtTokenGenerator jwtTokenGenerator = new JwtTokenGenerator();
                            string jwtToken = string.Empty;
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                jwtToken = jwtTokenGenerator.GenerateToken(l_fe._sLogin, Convert.ToString(dt.Rows[0]["DIV_RIGHTS"]));
                            }
                            
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                loginFE r_ats = new loginFE();
                                r_ats._sLogin = dt.Rows[0]["LOGIN_ID"].ToString();
                                r_ats.name = dt.Rows[0]["NAME"].ToString();
                                r_ats.divcode = dt.Rows[0]["DIVCODE"].ToString();
                                r_ats.div_rights = dt.Rows[0]["DIV_RIGHTS"].ToString();
                                r_ats.count = dt.Rows[0]["COUNT"].ToString();
                                r_ats.mob_no = dt.Rows[0]["MOB_NO"].ToString();
                                r_ats.fe_address = dt.Rows[0]["FE_ADDRESS"].ToString();
                                r_ats.JwtToken = jwtToken;
                                _login.Add(r_ats);
                                response.Data = _login;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }


        [Route("API/Recovery/Bill_PDF")] ////Added  By Babalu Kumar 10022021 PCN No.  1311202008
        [HttpPost]
        public HttpResponseMessage Bill_PDF([FromBody] ValidateCA Bill_PDF)
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            DataTable dt = new DataTable();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                V2_NEW.Service objpdf = new V2_NEW.Service();
                // ISU_LIVE.ISUService objpdf = new ISU_LIVE.ISUService();
                List<Display_BillPDF> objlist = new List<Display_BillPDF>();
                if (Bill_PDF.CA_Number.Length < 12 && Bill_PDF.CA_Number.Length > 0)
                    Bill_PDF.CA_Number = Bill_PDF.CA_Number.PadLeft(12, '0');
                using (con = new OleDbConnection(strConnection))
                {
                    //dt = objpdf.ZBAPI_ONLINE_BILL_PDF(Bill_PDF.CA_Number, "").Tables[0];
                    dt = objpdf.ZBAPI_ONLINE_BILL_PDF_V2(Bill_PDF.CA_Number, "").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Display_BillPDF objpdfdata = new Display_BillPDF();
                            objpdfdata.Bill = dt.Rows[i][0].ToString();
                            objlist.Add(objpdfdata);
                        }
                        response.Data = objlist;
                        response.Status = "Success";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        response.Status = "Please Enter Valid Input And Try Again";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }


        [Route("API/Recovery/RecServices_Account_Class")]
        [HttpPost]
        public HttpResponseMessage RecServices_Account_Class([FromBody] RecServices_Account_Class IPN)
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            DT_Data PO = new DT_Data();
            List<RecServices_Account_Class> _Poweroff = new List<RecServices_Account_Class>();
            DataTable dt = new DataTable();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (IPN.strKeyParam == PublicAccessTokenKey)
                {
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT '' ACLASSID,'-All-' ACOUNT_CLASS FROM DUAL UNION SELECT UNIQUE ACLASSID,ACOUNT_CLASS FROM RECAPP_ACLASS_MST WHERE ACTIVE = 'Y' ORDER BY ACOUNT_CLASS";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecServices_Account_Class poweroff = new RecServices_Account_Class();
                                poweroff.ACLASSID = dt.Rows[i]["ACLASSID"].ToString();
                                poweroff.ACOUNT_CLASS = dt.Rows[i]["ACOUNT_CLASS"].ToString();
                                _Poweroff.Add(poweroff);
                                response.Data = _Poweroff;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count <= 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "No Data";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }


        [Route("API/Recovery/RecServices_Amount_Bucket")]
        [HttpPost]
        public HttpResponseMessage RecServices_Amount_Bucket([FromBody] RecServices_Amount_Bucket rams)
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (rams.strKeyParam == PublicAccessTokenKey)
                {
                    List<RecServices_Amount_Bucket> _RAS = new List<RecServices_Amount_Bucket>();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT '' AMTBKTID,'-All-' AMOUNT_BUCKET,9999999999 RANGE_FROM,9999999999 RANGE_TO FROM DUAL UNION SELECT UNIQUE AMTBKTID,AMOUNT_BUCKET,RANGE_FROM,RANGE_TO FROM RECAPP_AMTBKT_MST WHERE ACTIVE = 'Y' ORDER BY RANGE_TO DESC";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Open)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecServices_Amount_Bucket r_ats = new RecServices_Amount_Bucket();
                                r_ats.AMTBKTID = dt.Rows[i]["AMTBKTID"].ToString();
                                r_ats.AMOUNT_BUCKET = dt.Rows[i]["AMOUNT_BUCKET"].ToString();
                                r_ats.RANGE_FROM = dt.Rows[i]["RANGE_FROM"].ToString();
                                r_ats.RANGE_TO = dt.Rows[i]["RANGE_TO"].ToString();
                                _RAS.Add(r_ats);
                                response.Data = _RAS;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }


        [Route("API/Recovery/RecServices_Category")]
        [HttpPost]
        public HttpResponseMessage RecServices_Category([FromBody] RecServices_Category rc)
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (rc.strKeyParam == PublicAccessTokenKey)
                {
                    List<RecServices_Category> _RAS = new List<RecServices_Category>();
                    DataTable dt = new DataTable();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT '' CATID,'-All-' CATEGORY FROM DUAL UNION SELECT UNIQUE CATID,CATEGORY FROM RECAPP_CATEGORY_MST WHERE ACTIVE = 'Y' ORDER BY CATEGORY";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecServices_Category r_ats = new RecServices_Category();
                                r_ats.CATID = dt.Rows[i]["CATID"].ToString();
                                r_ats.CATEGORY = dt.Rows[i]["CATEGORY"].ToString();
                                _RAS.Add(r_ats);
                                response.Data = _RAS;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }


        [Route("API/Recovery/getPayment")]
        [HttpPost]
        public HttpResponseMessage getPayment([FromBody] getPayment gp)
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (gp.strKeyParam == PublicAccessTokenKey)
                {
                    List<getPayment> _RAS = new List<getPayment>();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT UNIQUE PAYMNT_AMT,(CASE WHEN PAYMNT_ENTRY_DT IS NULL THEN '' ELSE TO_CHAR(PAYMNT_ENTRY_DT,'dd-Mon-yyyy') END) PAYMNT_ENTRY_DT FROM DEFLTR_LIST_MAIN A WHERE 1=1";
                        if (gp._sCANumber != "")
                            sql += " AND A.CA_NUMBER = '" + gp._sCANumber + "'";
                        if (gp._sMeterNumber != "")
                            sql += " AND A.METER_NO = '" + gp._sMeterNumber + "'";
                        //sql += " AND A.UPDATION_ID = (SELECT UNIQUE TO_CHAR(MAX(TO_DATE(UPDATION_ID || '01','YYYYMMDD')),'yyyyMM') FROM DEFLTR_LIST_MAIN)";
                        sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                        sql += " AND A.INITIAL_BUCKET IN ('DIV','MLCC','AGENCY')";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                getPayment gp_ats = new getPayment();
                                gp_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                gp_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                _RAS.Add(gp_ats);
                                response.Data = _RAS;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }


        [Route("API/Recovery/searchDefltrs")]
        [HttpPost]
        public HttpResponseMessage searchDefltrs([FromBody] searchDefltrs sD)
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (sD.strKeyParam == PublicAccessTokenKey)
                {
                    List<searchDefltrs> _RAS = new List<searchDefltrs>();
                    DataTable dt = new DataTable();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT CA_NUMBER FROM RECOVERY.DEFLTR_CA_ASSIGN_LINK_DTLS WHERE  TAB_UPD_BY IS NOT NULL AND TAB_UPD_STATUS = 'Online' AND UPDATION_ID_DTLS='" + System.DateTime.Now.ToString("yyyyMM") + "'";
                        if (sD._sCANumber != "")
                        {
                            sql += " AND CA_NUMBER = '" + sD._sCANumber + "'";
                        }
                        if (sD._sMeterNumber != "")
                        {
                            sql += " AND  MTR_NO = '" + sD._sMeterNumber + "'";
                        }
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count == 0)
                        {
                            sql = "SELECT A.COMPANY, A.CA_NUMBER, A.METER_NO, A.CRN_NUMBER, A.CYCLE_NO, A.ACCOUNT_CLASS, A.DIVISION, A.NAME, A.TELEPHONE_NO, A.HOUSE_NUMBER, A.STREET1, A.STREET2,";
                            sql += "A.STREET3, A.POLE_NO, A.POSTAL_CODE, A.STREET, A.SEQUENCE_NUMBER, A.RATE_CATEGORY, A.BILL_STATUS, A.DISHONOR_FLAG,";
                            //sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (A.LST_PYMT_DT || ' (' || TRUNC(TRUNC(MONTHS_BETWEEN (TRUNC(SYSDATE),TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))/12) || ' Years ' ||";
                            //sql += " MOD(TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'))), 12) || ' Months ' ||";
                            //sql += " ROUND( TRUNC(SYSDATE) - ADD_MONTHS(( TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')), TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))),0) || ' Days)') END) LST_PYMT_DT,";
                            sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (a.LST_PYMT_DT || ' (Pending Since ' || (trunc(sysdate) - trunc(a.LST_PYMT_DT)) || 'Days)') END) LST_PYMT_DT,";
                            sql += "A.DUE_DATE, A.CONTRACT, A.MOVE_IN_DATE, A.CURRENT_DEMAND, A.LAST_PAYMENT_AMT, A.SANCTION_LOAD, A.DEFAULT_AMT, A.LPSC_AMOUNT, A.PRINCIPAL_VALUE, A.CONSUMER_STATUS,";
                            sql += "A.SOFT_DISCONNECTION_DT, A.RUNNING_BAL_AS_ON_DT, A.CONNECTION_DT, A.INITIAL_BUCKET, A.NEW_OLD_FLAG, A.UPLOAD_CYCLE, A.UPDATION_ID, A.INITIAL_DEFAULT_AMT,";
                            sql += "A.PAYMNT_AMT, A.PAYMNT_ENTRY_DT, A.PAYMENT_MODE,B.ASSIGN_USER_CODE ASSIGNED_FE_ID, (SELECT UNIQUE C.FE_NAME_DISP FROM DEFLTR_AGENCY_FE_MST C WHERE B.ASSIGN_USER_CODE = C.FE_ID) ASSIGNED_FE_NAME";
                            sql += " FROM DEFLTR_LIST_MAIN A, DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1";
                            if (sD._sCANumber != "")
                                sql += " AND A.CA_NUMBER = '" + sD._sCANumber + "'";
                            if (sD._sMeterNumber != "")
                                sql += " AND A.METER_NO = '" + sD._sMeterNumber + "'";
                            //sql += " AND A.UPDATION_ID = (SELECT UNIQUE TO_CHAR(MAX(TO_DATE(UPDATION_ID || '01','YYYYMMDD')),'yyyyMM') FROM DEFLTR_LIST_MAIN)";
                            sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                            sql += " AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC', 'AGENCY')";
                            if (sD._sFEID != "")
                                sql += " AND B.ASSIGN_USER_CODE = '" + sD._sFEID + "'";
                            sql += " AND A.CA_NUMBER = B.CA_NUMBER AND A.UPDATION_ID = B.UPDATION_ID_DTLS AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS";
                            cmd = new OleDbCommand(sql, con);
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            adp = new OleDbDataAdapter(cmd);
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    searchDefltrs sD_ats = new searchDefltrs();
                                    sD._sCANumber = sD._sCANumber.ToString();
                                    sD._sMeterNumber = sD._sMeterNumber.ToString();
                                    sD._sFEID = sD._sFEID.ToString();
                                    sD_ats.COMPANY = dt.Rows[i]["COMPANY"].ToString();
                                    sD_ats.CA_NUMBER = dt.Rows[i]["CA_NUMBER"].ToString();
                                    sD_ats.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                    sD_ats.CRN_NUMBER = dt.Rows[i]["CRN_NUMBER"].ToString();
                                    sD_ats.CYCLE_NO = dt.Rows[i]["CYCLE_NO"].ToString();
                                    sD_ats.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                    sD_ats.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                    sD_ats.NAME = dt.Rows[i]["NAME"].ToString();
                                    sD_ats.TELEPHONE_NO = dt.Rows[i]["TELEPHONE_NO"].ToString();
                                    sD_ats.HOUSE_NUMBER = dt.Rows[i]["HOUSE_NUMBER"].ToString();
                                    sD_ats.STREET1 = dt.Rows[i]["STREET1"].ToString();
                                    sD_ats.STREET2 = dt.Rows[i]["STREET2"].ToString();
                                    sD_ats.STREET3 = dt.Rows[i]["STREET3"].ToString();
                                    sD_ats.POLE_NO = dt.Rows[i]["POLE_NO"].ToString();
                                    sD_ats.POSTAL_CODE = dt.Rows[i]["POSTAL_CODE"].ToString();
                                    sD_ats.STREET = dt.Rows[i]["STREET"].ToString();
                                    sD_ats.SEQUENCE_NUMBER = dt.Rows[i]["SEQUENCE_NUMBER"].ToString();
                                    sD_ats.RATE_CATEGORY = dt.Rows[i]["RATE_CATEGORY"].ToString();
                                    sD_ats.BILL_STATUS = dt.Rows[i]["BILL_STATUS"].ToString();
                                    sD_ats.DISHONOR_FLAG = dt.Rows[i]["DISHONOR_FLAG"].ToString();
                                    sD_ats.LAST_PAYMENT_DT = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                    sD_ats.DUE_DATE = dt.Rows[i]["DUE_DATE"].ToString();
                                    sD_ats.CONTRACT = dt.Rows[i]["CONTRACT"].ToString();
                                    sD_ats.MOVE_IN_DATE = dt.Rows[i]["MOVE_IN_DATE"].ToString();
                                    sD_ats.CURRENT_DEMAND = dt.Rows[i]["CURRENT_DEMAND"].ToString();
                                    sD_ats.LAST_PAYMENT_AMT = dt.Rows[i]["LAST_PAYMENT_AMT"].ToString();

                                    sD_ats.SANCTION_LOAD = dt.Rows[i]["SANCTION_LOAD"].ToString();
                                    sD_ats.DEFAULT_AMT = dt.Rows[i]["DEFAULT_AMT"].ToString();
                                    sD_ats.LPSC_AMOUNT = dt.Rows[i]["LPSC_AMOUNT"].ToString();
                                    sD_ats.PRINCIPAL_VALUE = dt.Rows[i]["PRINCIPAL_VALUE"].ToString();
                                    sD_ats.CONSUMER_STATUS = dt.Rows[i]["CONSUMER_STATUS"].ToString();
                                    sD_ats.SOFT_DISCONNECTION_DT = dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString();

                                    sD_ats.RUNNING_BAL_AS_ON_DT = dt.Rows[i]["RUNNING_BAL_AS_ON_DT"].ToString();
                                    sD_ats.CONNECTION_DT = dt.Rows[i]["CONNECTION_DT"].ToString();
                                    sD_ats.INITIAL_BUCKET = dt.Rows[i]["INITIAL_BUCKET"].ToString();
                                    sD_ats.NEW_OLD_FLAG = dt.Rows[i]["NEW_OLD_FLAG"].ToString();
                                    sD_ats.UPLOAD_CYCLE = dt.Rows[i]["UPLOAD_CYCLE"].ToString();

                                    sD_ats.UPDATION_ID = dt.Rows[i]["UPDATION_ID"].ToString();
                                    sD_ats.INITIAL_DEFAULT_AMT = dt.Rows[i]["INITIAL_DEFAULT_AMT"].ToString();
                                    sD_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                    sD_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                    sD_ats.PAYMENT_MODE = dt.Rows[i]["PAYMENT_MODE"].ToString();
                                    sD_ats.ASSIGNED_FE_ID = dt.Rows[i]["ASSIGNED_FE_ID"].ToString();
                                    sD_ats.ASSIGNED_FE_NAME = dt.Rows[i]["ASSIGNED_FE_NAME"].ToString();
                                    string sqlnew = "select Directive from recovery.DEFLTER_Acceptence_MST where Active='Y' and UPPER(RATE_CATEGORY)=UPPER('" + sD_ats.RATE_CATEGORY + "')";
                                    DataTable dt1 = new DataTable();
                                    cmd = new OleDbCommand(sqlnew, con);
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    adp = new OleDbDataAdapter(cmd);
                                    adp.Fill(dt1);
                                    if (dt1.Rows.Count > 0)
                                    {
                                        sD_ats.DIRECTIVE = dt1.Rows[0]["Directive"].ToString();
                                    }
                                    _RAS.Add(sD_ats);
                                    response.Data = _RAS;
                                    response.Status = "Success";
                                    response.Message = "Data retrieved";
                                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                                }
                            }
                            else if (dt.Rows.Count == 0)
                            {
                                response.Status = "No Data";
                                response.Message = "No Data Found";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else
                        {
                            response.Status = "Completed";
                            response.Message = "ATR Already Punched.";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [Route("API/Recovery/searchDefltrs1")]
        [HttpPost]
        public HttpResponseMessage searchDefltrs1([FromBody] searchDefltrs sD)
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (sD.strKeyParam == PublicAccessTokenKey)
                {
                    List<searchDefltrs> _RAS = new List<searchDefltrs>();
                    DataTable dt = new DataTable();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT CA_NUMBER FROM RECOVERY.DEFLTR_CA_ASSIGN_LINK_DTLS WHERE  TAB_UPD_BY IS NOT NULL AND TAB_UPD_STATUS = 'Online' AND UPDATION_ID_DTLS='" + System.DateTime.Now.ToString("yyyyMM") + "'";
                        if (sD._sCANumber != "")
                        {
                            sql += " AND CA_NUMBER = '" + sD._sCANumber + "'";
                        }
                        if (sD._sMeterNumber != "")
                        {
                            sql += " AND  MTR_NO = '" + sD._sMeterNumber + "'";
                        }
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count == 0)
                        {
                            sql = "SELECT A.COMPANY, A.CA_NUMBER, A.METER_NO, A.CRN_NUMBER, A.CYCLE_NO, A.ACCOUNT_CLASS, A.DIVISION, A.NAME, A.TELEPHONE_NO, A.HOUSE_NUMBER, A.STREET1, A.STREET2,";
                            sql += "A.STREET3, A.POLE_NO, A.POSTAL_CODE, A.STREET, A.SEQUENCE_NUMBER, A.RATE_CATEGORY, A.BILL_STATUS, A.DISHONOR_FLAG,";
                            //sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (A.LST_PYMT_DT || ' (' || TRUNC(TRUNC(MONTHS_BETWEEN (TRUNC(SYSDATE),TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))/12) || ' Years ' ||";
                            //sql += " MOD(TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'))), 12) || ' Months ' ||";
                            //sql += " ROUND( TRUNC(SYSDATE) - ADD_MONTHS(( TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')), TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))),0) || ' Days)') END) LST_PYMT_DT,";
                            sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (a.LST_PYMT_DT || ' (Pending Since ' || (trunc(sysdate) - trunc(a.LST_PYMT_DT)) || 'Days)') END) LST_PYMT_DT,";
                            sql += "A.DUE_DATE, A.CONTRACT, A.MOVE_IN_DATE, A.CURRENT_DEMAND, A.LAST_PAYMENT_AMT, A.SANCTION_LOAD, A.DEFAULT_AMT, A.LPSC_AMOUNT, A.PRINCIPAL_VALUE, A.CONSUMER_STATUS,";
                            sql += "A.SOFT_DISCONNECTION_DT, A.RUNNING_BAL_AS_ON_DT, A.CONNECTION_DT, A.INITIAL_BUCKET, A.NEW_OLD_FLAG, A.UPLOAD_CYCLE, A.UPDATION_ID, A.INITIAL_DEFAULT_AMT,";
                            sql += "A.PAYMNT_AMT, A.PAYMNT_ENTRY_DT, A.PAYMENT_MODE,B.ASSIGN_USER_CODE ASSIGNED_FE_ID, (SELECT UNIQUE C.FE_NAME_DISP FROM DEFLTR_AGENCY_FE_MST C WHERE B.ASSIGN_USER_CODE = C.FE_ID) ASSIGNED_FE_NAME";
                            sql += " FROM DEFLTR_LIST_MAIN A, DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1";
                            if (sD._sCANumber != "")
                                sql += " AND A.CA_NUMBER = '" + sD._sCANumber + "'";
                            if (sD._sMeterNumber != "")
                                sql += " AND A.METER_NO = '" + sD._sMeterNumber + "'";
                            //sql += " AND A.UPDATION_ID = (SELECT UNIQUE TO_CHAR(MAX(TO_DATE(UPDATION_ID || '01','YYYYMMDD')),'yyyyMM') FROM DEFLTR_LIST_MAIN)";
                            sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                            sql += " AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC','AGENCY')";
                            if (sD._sFEID != "")
                                sql += " AND B.ASSIGN_USER_CODE = '" + sD._sFEID + "'";
                            sql += " AND A.CA_NUMBER = B.CA_NUMBER AND A.UPDATION_ID = B.UPDATION_ID_DTLS AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS";
                            cmd = new OleDbCommand(sql, con);
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            adp = new OleDbDataAdapter(cmd);
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    searchDefltrs sD_ats = new searchDefltrs();
                                    sD._sCANumber = sD._sCANumber.ToString();
                                    sD._sMeterNumber = sD._sMeterNumber.ToString();
                                    sD._sFEID = sD._sFEID.ToString();
                                    sD_ats.COMPANY = dt.Rows[i]["COMPANY"].ToString();
                                    sD_ats.CA_NUMBER = dt.Rows[i]["CA_NUMBER"].ToString();
                                    sD_ats.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                    sD_ats.CRN_NUMBER = dt.Rows[i]["CRN_NUMBER"].ToString();
                                    sD_ats.CYCLE_NO = dt.Rows[i]["CYCLE_NO"].ToString();
                                    sD_ats.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                    sD_ats.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                    sD_ats.NAME = dt.Rows[i]["NAME"].ToString();
                                    sD_ats.TELEPHONE_NO = dt.Rows[i]["TELEPHONE_NO"].ToString();
                                    sD_ats.HOUSE_NUMBER = dt.Rows[i]["HOUSE_NUMBER"].ToString();
                                    sD_ats.STREET1 = dt.Rows[i]["STREET1"].ToString();
                                    sD_ats.STREET2 = dt.Rows[i]["STREET2"].ToString();
                                    sD_ats.STREET3 = dt.Rows[i]["STREET3"].ToString();
                                    sD_ats.POLE_NO = dt.Rows[i]["POLE_NO"].ToString();
                                    sD_ats.POSTAL_CODE = dt.Rows[i]["POSTAL_CODE"].ToString();
                                    sD_ats.STREET = dt.Rows[i]["STREET"].ToString();
                                    sD_ats.SEQUENCE_NUMBER = dt.Rows[i]["SEQUENCE_NUMBER"].ToString();
                                    sD_ats.RATE_CATEGORY = dt.Rows[i]["RATE_CATEGORY"].ToString();
                                    sD_ats.BILL_STATUS = dt.Rows[i]["BILL_STATUS"].ToString();
                                    sD_ats.DISHONOR_FLAG = dt.Rows[i]["DISHONOR_FLAG"].ToString();
                                    sD_ats.LAST_PAYMENT_DT = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                    sD_ats.DUE_DATE = dt.Rows[i]["DUE_DATE"].ToString();
                                    sD_ats.CONTRACT = dt.Rows[i]["CONTRACT"].ToString();
                                    sD_ats.MOVE_IN_DATE = dt.Rows[i]["MOVE_IN_DATE"].ToString();
                                    sD_ats.CURRENT_DEMAND = dt.Rows[i]["CURRENT_DEMAND"].ToString();
                                    sD_ats.LAST_PAYMENT_AMT = dt.Rows[i]["LAST_PAYMENT_AMT"].ToString();

                                    sD_ats.SANCTION_LOAD = dt.Rows[i]["SANCTION_LOAD"].ToString();
                                    sD_ats.DEFAULT_AMT = dt.Rows[i]["DEFAULT_AMT"].ToString();
                                    sD_ats.LPSC_AMOUNT = dt.Rows[i]["LPSC_AMOUNT"].ToString();
                                    sD_ats.PRINCIPAL_VALUE = dt.Rows[i]["PRINCIPAL_VALUE"].ToString();
                                    sD_ats.CONSUMER_STATUS = dt.Rows[i]["CONSUMER_STATUS"].ToString();
                                    sD_ats.SOFT_DISCONNECTION_DT = dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString();

                                    sD_ats.RUNNING_BAL_AS_ON_DT = dt.Rows[i]["RUNNING_BAL_AS_ON_DT"].ToString();
                                    sD_ats.CONNECTION_DT = dt.Rows[i]["CONNECTION_DT"].ToString();
                                    sD_ats.INITIAL_BUCKET = dt.Rows[i]["INITIAL_BUCKET"].ToString();
                                    sD_ats.NEW_OLD_FLAG = dt.Rows[i]["NEW_OLD_FLAG"].ToString();
                                    sD_ats.UPLOAD_CYCLE = dt.Rows[i]["UPLOAD_CYCLE"].ToString();

                                    sD_ats.UPDATION_ID = dt.Rows[i]["UPDATION_ID"].ToString();
                                    sD_ats.INITIAL_DEFAULT_AMT = dt.Rows[i]["INITIAL_DEFAULT_AMT"].ToString();
                                    sD_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                    sD_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                    sD_ats.PAYMENT_MODE = dt.Rows[i]["PAYMENT_MODE"].ToString();
                                    sD_ats.ASSIGNED_FE_ID = dt.Rows[i]["ASSIGNED_FE_ID"].ToString();
                                    sD_ats.ASSIGNED_FE_NAME = dt.Rows[i]["ASSIGNED_FE_NAME"].ToString();

                                    _RAS.Add(sD_ats);
                                    response.Data = _RAS;
                                    response.Status = "Success";
                                    response.Message = "Data retrieved";
                                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                                }
                            }
                            else if (dt.Rows.Count == 0)
                            {
                                response.Status = "No Data";
                                response.Message = "No Data Found";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else
                        {
                            response.Status = "Completed";
                            response.Message = "ATR Already Punched.";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }


        [Route("API/Recovery/RecAllocDefltrs")]
        [HttpPost]
        public HttpResponseMessage RecAllocDefltrs([FromBody] RecAllocDefltrs READ)
        {
            DT_Data PO = new DT_Data();
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (READ.strKeyParam == PublicAccessTokenKey)
                {
                    List<RecAllocDefltrs> _RAD = new List<RecAllocDefltrs>();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT A.COMPANY, A.CA_NUMBER, A.METER_NO, A.CRN_NUMBER, A.CYCLE_NO, A.ACCOUNT_CLASS, A.DIVISION, A.NAME, A.TELEPHONE_NO, A.HOUSE_NUMBER, A.STREET1, A.STREET2,";
                        sql += "A.STREET3, A.POLE_NO, A.POSTAL_CODE, A.STREET, A.SEQUENCE_NUMBER, A.RATE_CATEGORY, A.BILL_STATUS, A.DISHONOR_FLAG,";
                        //sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (A.LST_PYMT_DT || ' (' || TRUNC(TRUNC(MONTHS_BETWEEN (TRUNC(SYSDATE),TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))/12) || ' Years ' ||";
                        //sql += " MOD(TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'))), 12) || ' Months ' ||";
                        //sql += " ROUND( TRUNC(SYSDATE) - ADD_MONTHS(( TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')), TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))),0) || ' Days)') END) LST_PYMT_DT,";
                        sql += "(CASE WHEN A.LST_PYMT_DATE IS NULL THEN '' ELSE (a.LST_PYMT_DATE || ' (Pending Since ' || (trunc(sysdate) - trunc(TO_DATE(a.LST_PYMT_DATE, 'dd-MM-yy'))) || 'Days)') END) LST_PYMT_DT,";
                        sql += "A.DUE_DATE, A.CONTRACT, A.MOVE_IN_DATE, A.CURRENT_DEMAND, A.LAST_PAYMENT_AMT, A.SANCTION_LOAD, A.DEFAULT_AMT, A.LPSC_AMOUNT, A.PRINCIPAL_VALUE, A.CONSUMER_STATUS,";
                        sql += "A.SOFT_DISCONNECTION_DT, A.RUNNING_BAL_AS_ON_DT, A.CONNECTION_DT, A.INITIAL_BUCKET, A.NEW_OLD_FLAG, A.UPLOAD_CYCLE, A.UPDATION_ID, A.INITIAL_DEFAULT_AMT,";
                        sql += "A.PAYMNT_AMT, A.PAYMNT_ENTRY_DT, A.PAYMENT_MODE,B.ASSIGN_USER_CODE ASSIGNED_FE_ID, (SELECT UNIQUE C.FE_NAME_DISP FROM recovery.DEFLTR_AGENCY_FE_MST C WHERE B.ASSIGN_USER_CODE = C.FE_ID) ASSIGNED_FE_NAME";
                        sql += " FROM recovery.DEFLTR_LIST_MAIN A, recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1";// AND A.MOVE_IN_DATE is null"; ////Added MOVE_IN_DATE By Babalu Kumar 24122020
                        if (READ._sCategory != "")
                        {
                            if (READ._sCategory == "CAT0002")
                                sql += " AND A.DISHONOR_FLAG = 'Y'";
                            else if (READ._sCategory == "CAT0003")
                                sql += " AND (A.LAST_PAYMENT_AMT = 0 OR LST_PYMT_DT IS NULL)";
                            if (READ._sCategory == "CAT0004")
                                sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT)>180";
                            //sql += " AND FLOOR(TO_DATE(SYSDATE) - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) > 180";
                        }
                        if (READ._sAmtBktID != "")
                            sql += " AND A.DEFAULT_AMT BETWEEN (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "') AND (SELECT UNIQUE RANGE_TO FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "')";

                        if (READ._sAgeBktID != "")
                        {
                            //sql += " AND FLOOR(TO_DATE('01/' ||  SUBSTR(A.UPDATION_ID,5,2) || '/' || SUBSTR(A.UPDATION_ID,0,4),'dd/MM/yyyy') - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) BETWEEN";
                            sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT) BETWEEN";
                            sql += " (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "') AND (SELECT UNIQUE RANGE_TO FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "')";
                        }
                        if (READ._sAccountClass != "")
                            sql += " AND A.ACCOUNT_CLASS = '" + READ._sAccountClass + "'";
                        if (READ._sDishonorFlag == "Y")
                            sql += " AND A.DISHONOR_FLAG = 'X'";
                        else if (READ._sDishonorFlag == "N")
                            // sql += " AND A.DISHONOR_FLAG != 'X'";
                            sql += " AND (A.DISHONOR_FLAG is null OR A.DISHONOR_FLAG = 'Y')";
                        //sql += " AND A.UPDATION_ID = (SELECT UNIQUE TO_CHAR(MAX(TO_DATE(UPDATION_ID || '01','YYYYMMDD')),'yyyyMM') FROM recovery.DEFLTR_LIST_MAIN)";
                        sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                        sql += "  AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC') ";
                        if (READ._sFEID != "")
                            sql += " AND UPPER(B.ASSIGN_USER_CODE) = UPPER('" + READ._sFEID + "')";
                        sql += " AND A.CA_NUMBER = B.CA_NUMBER(+) AND A.UPDATION_ID = B.UPDATION_ID_DTLS(+) AND TAB_FLAG = 'N' AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS(+) ORDER BY SEQUENCE_NUMBER";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecAllocDefltrs sD_ats = new RecAllocDefltrs();
                                sD_ats._sCategory = READ._sCategory;
                                sD_ats._sAmtBktID = READ._sAmtBktID;
                                sD_ats._sAgeBktID = READ._sAgeBktID;
                                sD_ats._sAccountClass = READ._sAccountClass;
                                sD_ats._sDishonorFlag = READ._sDishonorFlag;
                                sD_ats._sFEID = READ._sFEID;
                                sD_ats.COMPANY = dt.Rows[i]["COMPANY"].ToString();
                                sD_ats.CA_NUMBER = dt.Rows[i]["CA_NUMBER"].ToString();
                                sD_ats.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                sD_ats.CRN_NUMBER = dt.Rows[i]["CRN_NUMBER"].ToString();
                                sD_ats.CYCLE_NO = dt.Rows[i]["CYCLE_NO"].ToString();
                                sD_ats.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                sD_ats.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                sD_ats.NAME = dt.Rows[i]["NAME"].ToString();
                                sD_ats.TELEPHONE_NO = dt.Rows[i]["TELEPHONE_NO"].ToString();
                                sD_ats.HOUSE_NUMBER = dt.Rows[i]["HOUSE_NUMBER"].ToString();
                                sD_ats.STREET1 = dt.Rows[i]["STREET1"].ToString();
                                sD_ats.STREET2 = dt.Rows[i]["STREET2"].ToString();
                                sD_ats.STREET3 = dt.Rows[i]["STREET3"].ToString();
                                sD_ats.POLE_NO = dt.Rows[i]["POLE_NO"].ToString();
                                sD_ats.POSTAL_CODE = dt.Rows[i]["POSTAL_CODE"].ToString();
                                sD_ats.STREET = dt.Rows[i]["STREET"].ToString();
                                sD_ats.SEQUENCE_NUMBER = dt.Rows[i]["SEQUENCE_NUMBER"].ToString();
                                sD_ats.RATE_CATEGORY = dt.Rows[i]["RATE_CATEGORY"].ToString();
                                sD_ats.BILL_STATUS = dt.Rows[i]["BILL_STATUS"].ToString();
                                sD_ats.DISHONOR_FLAG = dt.Rows[i]["DISHONOR_FLAG"].ToString();
                                string s = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                //if (dt.Rows[i]["LST_PYMT_DT"].ToString() != "" && dt.Rows[i]["LST_PYMT_DT"].ToString() != null && dt.Rows[i]["LST_PYMT_DT"].ToString() != string.Empty)
                                //    sD_ats.LAST_PAYMENT_DT = Convert.ToDateTime(dt.Rows[i]["LST_PYMT_DT"]).ToString("dd/mm/yyyy");
                                //else
                                sD_ats.LAST_PAYMENT_DT = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                //if (dt.Rows[i]["LST_PYMT_DT"].ToString() != null && dt.Rows[i]["LST_PYMT_DT"].ToString() != "")
                                //{
                                //    string str = sD_ats.LAST_PAYMENT_DT;//Added By Babalu Kumar
                                //    if (str != null)
                                //    {
                                //        string[] strarray = str.Split('/');
                                //        string sdate = strarray[0].ToString();
                                //        string smonth = strarray[1].ToString();
                                //        string syear = strarray[2].ToString();
                                //        if (Convert.ToInt32(smonth) > 12)
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = smonth + "/" + sdate + "/" + syear;
                                //        }
                                //        else
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = sdate + "/" + smonth + "/" + syear;
                                //        }
                                //    }
                                //}
                                sD_ats.DUE_DATE = dt.Rows[i]["DUE_DATE"].ToString();
                                sD_ats.CONTRACT = dt.Rows[i]["CONTRACT"].ToString();
                                sD_ats.MOVE_IN_DATE = dt.Rows[i]["MOVE_IN_DATE"].ToString();
                                sD_ats.CURRENT_DEMAND = dt.Rows[i]["CURRENT_DEMAND"].ToString();
                                sD_ats.LAST_PAYMENT_AMT = dt.Rows[i]["LAST_PAYMENT_AMT"].ToString();
                                sD_ats.SANCTION_LOAD = dt.Rows[i]["SANCTION_LOAD"].ToString();
                                sD_ats.DEFAULT_AMT = dt.Rows[i]["DEFAULT_AMT"].ToString();
                                sD_ats.LPSC_AMOUNT = dt.Rows[i]["LPSC_AMOUNT"].ToString();
                                sD_ats.PRINCIPAL_VALUE = dt.Rows[i]["PRINCIPAL_VALUE"].ToString();
                                sD_ats.CONSUMER_STATUS = dt.Rows[i]["CONSUMER_STATUS"].ToString();
                                sD_ats.SOFT_DISCONNECTION_DT = dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString();
                                sD_ats.RUNNING_BAL_AS_ON_DT = dt.Rows[i]["RUNNING_BAL_AS_ON_DT"].ToString();
                                sD_ats.CONNECTION_DT = dt.Rows[i]["CONNECTION_DT"].ToString();
                                sD_ats.INITIAL_BUCKET = dt.Rows[i]["INITIAL_BUCKET"].ToString();
                                sD_ats.NEW_OLD_FLAG = dt.Rows[i]["NEW_OLD_FLAG"].ToString();
                                sD_ats.UPLOAD_CYCLE = dt.Rows[i]["UPLOAD_CYCLE"].ToString();
                                sD_ats.UPDATION_ID = dt.Rows[i]["UPDATION_ID"].ToString();
                                sD_ats.INITIAL_DEFAULT_AMT = dt.Rows[i]["INITIAL_DEFAULT_AMT"].ToString();
                                sD_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                sD_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                sD_ats.PAYMENT_MODE = dt.Rows[i]["PAYMENT_MODE"].ToString();
                                sD_ats.ASSIGNED_FE_ID = dt.Rows[i]["ASSIGNED_FE_ID"].ToString();
                                sD_ats.ASSIGNED_FE_NAME = dt.Rows[i]["ASSIGNED_FE_NAME"].ToString();
                                string sqlnew = "select Directive from recovery.DEFLTER_Acceptence_MST where Active='Y' and UPPER(RATE_CATEGORY)=UPPER('" + sD_ats.RATE_CATEGORY + "')";
                                DataTable dt1 = new DataTable();
                                cmd = new OleDbCommand(sqlnew, con);
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                adp = new OleDbDataAdapter(cmd);
                                adp.Fill(dt1);
                                if (dt1.Rows.Count > 0)
                                {
                                    sD_ats.DIRECTIVE = dt1.Rows[0]["Directive"].ToString();
                                }
                                _RAD.Add(sD_ats);
                                response.Data = _RAD;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [Route("API/Recovery/RecAllocDefltrs1")]
        [HttpPost]
        public HttpResponseMessage RecAllocDefltrs1([FromBody] RecAllocDefltrs READ)
        {
            DT_Data PO = new DT_Data();
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (READ.strKeyParam == PublicAccessTokenKey)
                {
                    List<RecAllocDefltrs> _RAD = new List<RecAllocDefltrs>();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT A.COMPANY, A.CA_NUMBER, A.METER_NO, A.CRN_NUMBER, A.CYCLE_NO, A.ACCOUNT_CLASS, A.DIVISION, A.NAME, A.TELEPHONE_NO, A.HOUSE_NUMBER, A.STREET1, A.STREET2,";
                        sql += "A.STREET3, A.POLE_NO, A.POSTAL_CODE, A.STREET, A.SEQUENCE_NUMBER, A.RATE_CATEGORY, A.BILL_STATUS, A.DISHONOR_FLAG,";
                        sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (a.LST_PYMT_DT || ' (Pending Since ' || (trunc(sysdate) - trunc(a.LST_PYMT_DT)) || 'Days)') END) LST_PYMT_DT,";
                        sql += "A.DUE_DATE, A.CONTRACT, A.MOVE_IN_DATE, A.CURRENT_DEMAND, A.LAST_PAYMENT_AMT, A.SANCTION_LOAD, A.DEFAULT_AMT, A.LPSC_AMOUNT, A.PRINCIPAL_VALUE, A.CONSUMER_STATUS,";
                        sql += "A.SOFT_DISCONNECTION_DT, A.RUNNING_BAL_AS_ON_DT, A.CONNECTION_DT, A.INITIAL_BUCKET, A.NEW_OLD_FLAG, A.UPLOAD_CYCLE, A.UPDATION_ID, A.INITIAL_DEFAULT_AMT,";
                        sql += "A.PAYMNT_AMT, A.PAYMNT_ENTRY_DT, A.PAYMENT_MODE,B.ASSIGN_USER_CODE ASSIGNED_FE_ID, (SELECT UNIQUE C.FE_NAME_DISP FROM recovery.DEFLTR_AGENCY_FE_MST C WHERE B.ASSIGN_USER_CODE = C.FE_ID) ASSIGNED_FE_NAME";
                        sql += " FROM recovery.DEFLTR_LIST_MAIN A, recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1";// AND A.MOVE_IN_DATE is null"; ////Added MOVE_IN_DATE By Babalu Kumar 24122020
                        if (READ._sCategory != "")
                        {
                            if (READ._sCategory == "CAT0002")
                                sql += " AND A.DISHONOR_FLAG = 'Y'";
                            else if (READ._sCategory == "CAT0003")
                                sql += " AND (A.LAST_PAYMENT_AMT = 0 OR LST_PYMT_DT IS NULL)";
                            if (READ._sCategory == "CAT0004")
                                sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT)>180";
                        }
                        if (READ._sAmtBktID != "")
                            sql += " AND A.DEFAULT_AMT BETWEEN (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "') AND (SELECT UNIQUE RANGE_TO FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "')";
                        if (READ._sAgeBktID != "")
                        {
                            sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT) BETWEEN";
                            sql += " (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "') AND (SELECT UNIQUE RANGE_TO FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "')";
                        }
                        if (READ._sAccountClass != "")
                            sql += " AND A.ACCOUNT_CLASS = '" + READ._sAccountClass + "'";
                        if (READ._sDishonorFlag == "Y")
                            sql += " AND A.DISHONOR_FLAG = 'X'";
                        else if (READ._sDishonorFlag == "N")
                            sql += " AND (A.DISHONOR_FLAG is null OR A.DISHONOR_FLAG = 'Y')";
                        sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                        sql += "  AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC') ";
                        if (READ._sFEID != "")
                            sql += " AND UPPER(B.ASSIGN_USER_CODE) = UPPER('" + READ._sFEID + "')";
                        sql += " AND A.CA_NUMBER = B.CA_NUMBER(+) AND A.UPDATION_ID = B.UPDATION_ID_DTLS(+) AND TAB_FLAG = 'N' AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS(+) ORDER BY SEQUENCE_NUMBER";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecAllocDefltrs sD_ats = new RecAllocDefltrs();
                                sD_ats._sCategory = READ._sCategory;
                                sD_ats._sAmtBktID = READ._sAmtBktID;
                                sD_ats._sAgeBktID = READ._sAgeBktID;
                                sD_ats._sAccountClass = READ._sAccountClass;
                                sD_ats._sDishonorFlag = READ._sDishonorFlag;
                                sD_ats._sFEID = READ._sFEID;
                                sD_ats.COMPANY = dt.Rows[i]["COMPANY"].ToString();
                                sD_ats.CA_NUMBER = dt.Rows[i]["CA_NUMBER"].ToString();
                                sD_ats.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                sD_ats.CRN_NUMBER = dt.Rows[i]["CRN_NUMBER"].ToString();
                                sD_ats.CYCLE_NO = dt.Rows[i]["CYCLE_NO"].ToString();
                                sD_ats.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                sD_ats.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                sD_ats.NAME = dt.Rows[i]["NAME"].ToString();
                                sD_ats.TELEPHONE_NO = dt.Rows[i]["TELEPHONE_NO"].ToString();
                                sD_ats.HOUSE_NUMBER = dt.Rows[i]["HOUSE_NUMBER"].ToString();
                                sD_ats.STREET1 = dt.Rows[i]["STREET1"].ToString();
                                sD_ats.STREET2 = dt.Rows[i]["STREET2"].ToString();
                                sD_ats.STREET3 = dt.Rows[i]["STREET3"].ToString();
                                sD_ats.POLE_NO = dt.Rows[i]["POLE_NO"].ToString();
                                sD_ats.POSTAL_CODE = dt.Rows[i]["POSTAL_CODE"].ToString();
                                sD_ats.STREET = dt.Rows[i]["STREET"].ToString();
                                sD_ats.SEQUENCE_NUMBER = dt.Rows[i]["SEQUENCE_NUMBER"].ToString();
                                sD_ats.RATE_CATEGORY = dt.Rows[i]["RATE_CATEGORY"].ToString();
                                sD_ats.BILL_STATUS = dt.Rows[i]["BILL_STATUS"].ToString();
                                sD_ats.DISHONOR_FLAG = dt.Rows[i]["DISHONOR_FLAG"].ToString();
                                sD_ats.LAST_PAYMENT_DT = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                sD_ats.DUE_DATE = dt.Rows[i]["DUE_DATE"].ToString();
                                sD_ats.CONTRACT = dt.Rows[i]["CONTRACT"].ToString();
                                sD_ats.MOVE_IN_DATE = dt.Rows[i]["MOVE_IN_DATE"].ToString();
                                sD_ats.CURRENT_DEMAND = dt.Rows[i]["CURRENT_DEMAND"].ToString();
                                sD_ats.LAST_PAYMENT_AMT = dt.Rows[i]["LAST_PAYMENT_AMT"].ToString();
                                sD_ats.SANCTION_LOAD = dt.Rows[i]["SANCTION_LOAD"].ToString();
                                sD_ats.DEFAULT_AMT = dt.Rows[i]["DEFAULT_AMT"].ToString();
                                sD_ats.LPSC_AMOUNT = dt.Rows[i]["LPSC_AMOUNT"].ToString();
                                sD_ats.PRINCIPAL_VALUE = dt.Rows[i]["PRINCIPAL_VALUE"].ToString();
                                sD_ats.CONSUMER_STATUS = dt.Rows[i]["CONSUMER_STATUS"].ToString();
                                sD_ats.SOFT_DISCONNECTION_DT = dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString();
                                sD_ats.RUNNING_BAL_AS_ON_DT = dt.Rows[i]["RUNNING_BAL_AS_ON_DT"].ToString();
                                sD_ats.CONNECTION_DT = dt.Rows[i]["CONNECTION_DT"].ToString();
                                sD_ats.INITIAL_BUCKET = dt.Rows[i]["INITIAL_BUCKET"].ToString();
                                sD_ats.NEW_OLD_FLAG = dt.Rows[i]["NEW_OLD_FLAG"].ToString();
                                sD_ats.UPLOAD_CYCLE = dt.Rows[i]["UPLOAD_CYCLE"].ToString();
                                sD_ats.UPDATION_ID = dt.Rows[i]["UPDATION_ID"].ToString();
                                sD_ats.INITIAL_DEFAULT_AMT = dt.Rows[i]["INITIAL_DEFAULT_AMT"].ToString();
                                sD_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                sD_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                sD_ats.PAYMENT_MODE = dt.Rows[i]["PAYMENT_MODE"].ToString();
                                sD_ats.ASSIGNED_FE_ID = dt.Rows[i]["ASSIGNED_FE_ID"].ToString();
                                sD_ats.ASSIGNED_FE_NAME = dt.Rows[i]["ASSIGNED_FE_NAME"].ToString();
                                _RAD.Add(sD_ats);
                                response.Data = _RAD;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }


        //[Route("API/Recovery/PunchAtr")]
        //[HttpPost]
        //public HttpResponseMessage PunchAtr([FromBody] PunchAtr PAtr)
        //{
        //    int count = 0;
        //    DT_Data PO = new DT_Data();
        //    List<PunchAtr> _RAD = new List<PunchAtr>();
        //    OleDbConnection con = new OleDbConnection();
        //    DataTable dt = new DataTable();
        //    OleDbDataAdapter sda = new OleDbDataAdapter();
        //    OleDbDataAdapter oda = new OleDbDataAdapter();
        //    if (PAtr.strKeyParam == PublicAccessTokenKey)
        //    {
        //        try
        //        {
        //            using (con = new OleDbConnection(strConnection))
        //            {
        //                if (con.State == ConnectionState.Closed)
        //                {
        //                    con.Open();
        //                }
        //                System.Web.HttpContext context = System.Web.HttpContext.Current;
        //                string _sIPAddr = context.Request.ServerVariables["REMOTE_ADDR"];
        //                string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
        //                bool res = false;
        //                DataTable dtCheckATR = ChkRecordExist(PAtr._sCANumber, PAtr._sFEID, "DEFLTR_ATR");
        //                if (dtCheckATR.Rows.Count > 0)
        //                {
        //                    res = UpdateTable(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, _sIPAddr, "DEFLTR_ATR", "", "", "", "", "", "", "", "");
        //                    res = true;
        //                }
        //                else
        //                {
        //                    res = InsertTable(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, _sIPAddr, "DEFLTR_ATR");
        //                    res = true;
        //                }
        //                if (res == true)
        //                {
        //                    if (PAtr._sStatus == "05")
        //                    {
        //                        InsertSMS(PAtr._sMobileNo, PAtr._sMessage, PAtr._sCANumber, PAtr._sUpdationID, _sIPAddr);
        //                    }
        //                    res = UpdateTable(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, ipAddress, "DEFLTR_CA_ASSIGN_LINK_DTLS", PAtr._sTabUpdStatus, PAtr._sRemarks,
        //                                        PAtr._sImgFlg, PAtr._sImgPath, PAtr._sImgPath1, PAtr._sFollowDate, PAtr._sAltContNo, PAtr._sAltEmailID);
        //                    response.Status = "Success";
        //                    response.Message = "Data updated";
        //                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
        //                    return Msg;
        //                    con.Close();
        //                }
        //                else
        //                {
        //                    response.Status = "Failed";
        //                    response.Message = "Data not updated";
        //                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
        //                    return Msg;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return Msg;
        //            throw ex;
        //        }
        //        con.Close();
        //    }
        //    else
        //    {
        //        response.Status = "Failed";
        //        response.Message = "Key doesnot match !";
        //        Msg = Request.CreateResponse(HttpStatusCode.OK, response);

        //    }
        //    return Msg;
        //}

        [Route("API/Recovery/PunchAtr")]
        [HttpPost]
        public HttpResponseMessage PunchAtr([FromBody] PunchAtr PAtr)
        {
            int count = 0;
            DT_Data PO = new DT_Data();
            List<PunchAtr> _RAD = new List<PunchAtr>();
            OleDbConnection con = new OleDbConnection();
            DataTable dt = new DataTable();
            OleDbDataAdapter sda = new OleDbDataAdapter();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            RecoveryBL recoveryBL = new RecoveryBL();
            string sql = string.Empty;
            string strConnection = NDS.con();
            if (PAtr.strKeyParam == PublicAccessTokenKey)
            {
                try
                {
                    var authentication = new Authentication();
                    authentication.Validate();
                    using (con = new OleDbConnection(strConnection))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        System.Web.HttpContext context = System.Web.HttpContext.Current;
                        string _sIPAddr = context.Request.ServerVariables["REMOTE_ADDR"];
                        string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
                        bool res = false;
                        DataTable dtupdate = recoveryBL.ChkUpdatedate();
                        if (dtupdate.Rows.Count > 0)
                        {
                            if (dtupdate.Rows[0]["DT"].ToString() == dtupdate.Rows[0]["UDT"].ToString())
                            {
                                DataTable dtCheckATR = recoveryBL.ChkRecordExist(PAtr._sCANumber, PAtr._sFEID, "DEFLTR_ATR");
                                if (dtCheckATR.Rows.Count > 0)
                                {
                                    res = recoveryBL.UpdateTable(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, _sIPAddr, "DEFLTR_ATR", "", "", "", "", "", "", "", "");
                                    // res = UpdateTable(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, _sIPAddr, "DEFLTR_ATR", "", "", "", "", "", "", "", "");
                                    res = true;
                                }
                                else
                                {
                                    res = recoveryBL.InsertTable(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, _sIPAddr, "DEFLTR_ATR");
                                    res = true;
                                }
                                if (res == true)
                                {
                                    if (PAtr._sStatus == "05")
                                    {
                                        recoveryBL.InsertSMS(PAtr._sMobileNo, PAtr._sMessage, PAtr._sCANumber, PAtr._sUpdationID, _sIPAddr);
                                    }
                                    res = recoveryBL.UpdateTable(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, ipAddress, "DEFLTR_CA_ASSIGN_LINK_DTLS", PAtr._sTabUpdStatus, PAtr._sRemarks,
                                                        PAtr._sImgFlg, PAtr._sImgPath, PAtr._sImgPath1, PAtr._sFollowDate, PAtr._sAltContNo, PAtr._sAltEmailID);
                                    response.Status = "Success";
                                    response.Message = "Data updated";
                                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                                    return Msg;
                                    con.Close();
                                }
                                else
                                {
                                    response.Status = "Failed";
                                    response.Message = "Data not updated";
                                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                                    return Msg;
                                }
                            }
                            else
                            {
                                DataTable dtCheckATR = recoveryBL.ChkRecordExist1(PAtr._sCANumber, PAtr._sFEID, "DEFLTR_ATR");
                                if (dtCheckATR.Rows.Count > 0)
                                {
                                    res = recoveryBL.UpdateTable(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, _sIPAddr, "DEFLTR_ATR", "", "", "", "", "", "", "", "");
                                    //res = UpdateTable1(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, _sIPAddr, "DEFLTR_ATR", "", "", "", "", "", "", "", "");
                                    res = true;
                                }
                                else
                                {
                                    res = recoveryBL.InsertTable1(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, _sIPAddr, "DEFLTR_ATR");
                                    res = true;
                                }
                                if (res == true)
                                {
                                    if (PAtr._sStatus == "05")
                                    {
                                        recoveryBL.InsertSMS(PAtr._sMobileNo, PAtr._sMessage, PAtr._sCANumber, PAtr._sUpdationID, _sIPAddr);
                                    }
                                    res = recoveryBL.UpdateTable1(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, ipAddress, "DEFLTR_CA_ASSIGN_LINK_DTLS", PAtr._sTabUpdStatus, PAtr._sRemarks,
                                                        PAtr._sImgFlg, PAtr._sImgPath, PAtr._sImgPath1, PAtr._sFollowDate, PAtr._sAltContNo, PAtr._sAltEmailID);
                                    response.Status = "Success";
                                    response.Message = "Data updated";
                                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                                    return Msg;
                                    con.Close();
                                }
                                else
                                {
                                    response.Status = "Failed";
                                    response.Message = "Data not updated";
                                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                                    return Msg;
                                }
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
                }
                catch (Exception ex)
                {
                    return Msg;
                    throw ex;
                }
                con.Close();
            }
            else
            {
                response.Status = "Failed";
                response.Message = "Key doesnot match !";
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);

            }
            return Msg;
        }

        [Route("API/Recovery/changePassword")]
        [HttpPost]
        public HttpResponseMessage changePassword([FromBody] changepassword cp)
        {
            string pwd = string.Empty;
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (cp.strKeyParam == PublicAccessTokenKey)
                {
                    using (con = new OleDbConnection(strConnection))
                    {
                        DataTable dt = new DataTable();
                        sql = "SELECT fe_id from DEFLTR_AGENCY_FE_MST WHERE UPPER(FE_ID) = UPPER('" + cp._sLogin + "') AND UPPER(PASSWRD) = UPPER('" + cp._sOldPassword + "')";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            sql = "UPDATE DEFLTR_AGENCY_FE_MST SET PASSWRD = '" + cp._sNewPassword + "' WHERE UPPER(FE_ID) = UPPER('" + cp._sLogin + "') AND UPPER(PASSWRD) = UPPER('" + cp._sOldPassword + "')";
                            cmd = new OleDbCommand(sql, con);
                            adp = new OleDbDataAdapter(cmd);
                            adp.SelectCommand.CommandText = "UPDATE DEFLTR_AGENCY_FE_MST SET PASSWRD = '" + cp._sNewPassword + "' WHERE UPPER(FE_ID) = UPPER('" + cp._sLogin + "') AND UPPER(PASSWRD) = UPPER('" + cp._sOldPassword + "')";
                            dt.TableName = "DEFLTR_AGENCY_FE_MST";
                            cmd.ExecuteNonQuery();
                            response.Status = "Success";
                            response.Message = "Data updated";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                        else
                        {
                            response.Status = "Failed";
                            response.Message = "User Not Found !";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }


        [Route("API/Recovery/Z_BAPI_IVRS")]
        [HttpPost]
        public HttpResponseMessage Z_BAPI_IVRS([FromBody] Z_BAPI_IVRS ivrs)
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                LIVE.WebService objisu = new LIVE.WebService();
                if (ivrs.strKeyParam == PublicAccessTokenKey)
                {
                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();
                    dt = objisu.Z_BAPI_IVRS("000" + ivrs.strContractAccountNumber).Tables[0];
                    response.Data = dt;
                    response.Status = "Success";
                    response.Message = "Data updated";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            return Msg;
        }

        [Route("API/Recovery/DetailofMIS1")]
        [HttpPost]
        public HttpResponseMessage DetailofMIS1([FromBody] DetailofMIS DM)
        {
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (DM.strKeyParam == PublicAccessTokenKey)
                {
                    int count = 0;
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    using (con = new OleDbConnection(strConnection))
                    {
                        string sql = "";
                        //sql = "SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(DEFAULT_AMT),0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN A,recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B";
                        //sql += " WHERE A.ACCOUNT_CLASS IN ('SLCC','MLCC')";
                        //sql += " AND A.UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')";
                        //sql += " AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC')";
                        //sql += " AND UPPER(B.ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";
                        //sql += " AND A.CA_NUMBER = B.CA_NUMBER AND A.UPDATION_ID = B.UPDATION_ID_DTLS AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS";
                        sql = "select sum(A.cnt) CNT,sum(A.DEFAULT_AMT) DEFAULT_AMT from ";
                        sql += " (SELECT COUNT(UNIQUE CA_NUMBER) CNT, NVL(SUM(DEFAULT_AMT), 0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN WHERE  ca_number IN("; //Added  By Babalu Kumar 10022021 PCN No. 2021021003
                        sql += " SELECT CA_NUMBER FROM recovery.DEFLTR_CA_ASSIGN_LINK_DTLS WHERE UPDATION_ID_DTLS= TO_CHAR(SYSDATE,'yyyyMM') AND UPPER(ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "'))";
                        sql += " and UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')";
                        sql += " UNION ";
                        sql += " SELECT COUNT(UNIQUE CA_NUMBER) CNT,NVL(SUM(DEFAULT_AMT), 0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_DISCARD WHERE  ca_number IN(";
                        sql += " SELECT CA_NUMBER FROM recovery.DEFLTR_CA_ASSIGN_LINK_DTLS WHERE UPDATION_ID_DTLS= TO_CHAR(SYSDATE,'yyyyMM') AND UPPER(ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "'))";
                        sql += " and UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')) A";

                        string sql1 = "";
                        //sql1 = "SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(DEFAULT_AMT),0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN A,recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B";
                        //sql1 += " WHERE A.ACCOUNT_CLASS IN ('SLCC','MLCC')";
                        //sql1 += " AND A.UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')";
                        //sql1 += " AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC')";
                        //sql1 += " AND UPPER(B.ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";
                        //sql1 += " AND A.CA_NUMBER = B.CA_NUMBER AND A.UPDATION_ID = B.UPDATION_ID_DTLS AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS";
                        //sql1 += " AND (B.STATUS IS NOT NULL OR B.STATUS != '') AND A.PAYMNT_AMT > 0";
                        sql1 = " SELECT SUM(A.CNT) CNT,SUM(A.DEFAULT_AMT) DEFAULT_AMT FROM";
                        sql1 += " (SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(PAYMNT_AMT), 0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN A,";
                        sql1 += " recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE";
                        sql1 += " A.UPDATION_ID = TO_CHAR(SYSDATE, 'yyyyMM')";
                        sql1 += " AND UPPER(ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";//Added  By Babalu Kumar 10022021 PCN No. 2021021003
                        sql1 += " AND A.CA_NUMBER = B.CA_NUMBER";
                        sql1 += " AND A.UPDATION_ID = B.UPDATION_ID_DTLS";
                        sql1 += " AND A.PAYMNT_AMT > 0";
                        sql1 += " AND PAYMNT_ENTRY_DT IS NOT NULL";
                        sql1 += " UNION ";
                        sql1 += " SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(PAYMNT_AMT), 0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_discard A,";
                        sql1 += " recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE";
                        sql1 += " A.UPDATION_ID = TO_CHAR(SYSDATE, 'yyyyMM')";
                        sql1 += " AND UPPER(ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";
                        sql1 += " AND A.CA_NUMBER = B.CA_NUMBER";
                        sql1 += " AND A.UPDATION_ID = B.UPDATION_ID_DTLS";
                        sql1 += " AND A.PAYMNT_AMT > 0";
                        sql1 += " AND PAYMNT_ENTRY_DT IS NOT NULL) A";

                        string sql2 = "";
                        //sql2 = "SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(DEFAULT_AMT),0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN A,recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B";
                        //sql2 += " WHERE A.ACCOUNT_CLASS IN ('SLCC','MLCC')";
                        //sql2 += " AND A.UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')";
                        //sql2 += " AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC')";
                        //sql2 += " AND UPPER(B.ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";
                        //sql2 += " AND A.CA_NUMBER = B.CA_NUMBER AND A.UPDATION_ID = B.UPDATION_ID_DTLS AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS";
                        //sql2 += " AND B.STATUS IN ('01','18') AND A.PAYMNT_AMT = 0";


                        sql2 = "select NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(DEFAULT_AMT), 0) DEFAULT_AMT from recovery.DEFLTR_LIST_MAIN A where UPDATION_ID = TO_CHAR(SYSDATE, 'yyyyMM') and CA_NUMBER";
                        sql2 += " in(select CA_NUMBER from    recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B where STATUS IN('01', '18', '02', '25', '15')";
                        sql2 += " and UPDATION_ID_DTLS = TO_CHAR(SYSDATE, 'yyyyMM')  AND UPPER(ASSIGN_USER_CODE) = UPPER('" + DM._sFEID + "'))";

                        //string sql3 = "";
                        //sql3 = "SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(DEFAULT_AMT),0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN A,recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B";
                        //sql3 += " WHERE A.ACCOUNT_CLASS IN ('SLCC','MLCC')";
                        //sql3 += " AND A.UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')";
                        //sql3 += " AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC')";
                        //sql3 += " AND UPPER(B.ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";
                        //sql3 += " AND A.CA_NUMBER = B.CA_NUMBER AND A.UPDATION_ID = B.UPDATION_ID_DTLS AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS";
                        //sql3 += " AND B.STATUS = '15' AND A.PAYMNT_AMT = 0";

                        string sql4 = "";
                        //sql4 = "SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(DEFAULT_AMT),0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN A,recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B";
                        //sql4 += " WHERE A.ACCOUNT_CLASS IN ('SLCC','MLCC')";
                        //sql4 += " AND A.UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')";
                        //sql4 += " AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC')";
                        //sql4 += " AND UPPER(B.ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";
                        //sql4 += " AND A.CA_NUMBER = B.CA_NUMBER AND A.UPDATION_ID = B.UPDATION_ID_DTLS AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS";
                        //sql4 += " AND B.STATUS IN ('02','03','04','05','06','07','08','09','16','17','19','20','21','22','23','24','25') AND A.PAYMNT_AMT = 0";//Added  By Babalu Kumar 10022021 PCN No. 2021021003


                        sql4 = " select NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(DEFAULT_AMT), 0) DEFAULT_AMT from recovery.DEFLTR_LIST_MAIN A where UPDATION_ID = TO_CHAR(SYSDATE, 'yyyyMM') and CA_NUMBER ";
                        sql4 += " in(select CA_NUMBER from    recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B where STATUS IN('03', '04', '05', '06', '09', '16', '17', '19', '20', '21', '22', '24')";
                        sql4 += " and UPDATION_ID_DTLS = TO_CHAR(SYSDATE, 'yyyyMM')  AND UPPER(ASSIGN_USER_CODE) = UPPER('" + DM._sFEID + "'))";

                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        oda = new OleDbDataAdapter(cmd);
                        OleDbDataReader rats = cmd.ExecuteReader();
                        List<DetailofMIS> _RAS = new List<DetailofMIS>();
                        DetailofMIS r_DOM = new DetailofMIS();
                        while (rats.Read())
                        {
                            count = count + 1;
                            r_DOM._sFEID = DM._sFEID;
                            r_DOM.TYPE = "Allocated_Defaulters";
                            r_DOM.CNT = rats["CNT"].ToString();
                            r_DOM.DEFAULT_AMT = rats["DEFAULT_AMT"].ToString();
                        }
                        _RAS.Add(r_DOM);
                        cmd = new OleDbCommand(sql1, con);
                        oda = new OleDbDataAdapter(cmd);
                        OleDbDataReader rats_pr = cmd.ExecuteReader();
                        DetailofMIS r_DOM1 = new DetailofMIS();
                        while (rats_pr.Read())
                        {
                            count = count + 1;
                            r_DOM1._sFEID = DM._sFEID;
                            r_DOM1.TYPE = "Payment_Received";
                            r_DOM1.CNT = rats_pr["CNT"].ToString();
                            r_DOM1.DEFAULT_AMT = rats_pr["DEFAULT_AMT"].ToString();
                        }
                        _RAS.Add(r_DOM1);
                        cmd = new OleDbCommand(sql2, con);
                        oda = new OleDbDataAdapter(cmd);
                        OleDbDataReader rats_mr = cmd.ExecuteReader();
                        DetailofMIS r_DOM2 = new DetailofMIS();
                        while (rats_mr.Read())
                        {
                            count = count + 1;
                            r_DOM2._sFEID = DM._sFEID;
                            r_DOM2.TYPE = "Meter_Removed";
                            r_DOM2.CNT = rats_mr["CNT"].ToString();
                            r_DOM2.DEFAULT_AMT = rats_mr["DEFAULT_AMT"].ToString();
                        }
                        _RAS.Add(r_DOM2);
                        //cmd = new OleDbCommand(sql3, con);
                        //oda = new OleDbDataAdapter(cmd);
                        //OleDbDataReader rats_dis = cmd.ExecuteReader();
                        //DetailofMIS r_DOM3 = new DetailofMIS();
                        //while (rats_dis.Read())
                        //{
                        //    count = count + 1;
                        //    r_DOM3._sFEID = DM._sFEID;
                        //    r_DOM3.TYPE = "Disconnection";
                        //    r_DOM3.CNT = rats_dis["CNT"].ToString();
                        //    r_DOM3.DEFAULT_AMT = rats_dis["DEFAULT_AMT"].ToString();
                        //}
                        //_RAS.Add(r_DOM3);
                        cmd = new OleDbCommand(sql4, con);
                        oda = new OleDbDataAdapter(cmd);
                        OleDbDataReader rats_oa = cmd.ExecuteReader();
                        DetailofMIS r_DOM4 = new DetailofMIS();
                        while (rats_oa.Read())
                        {
                            count = count + 1;
                            r_DOM4._sFEID = DM._sFEID;
                            r_DOM4.TYPE = "Other_ATR";
                            r_DOM4.CNT = rats_oa["CNT"].ToString();
                            r_DOM4.DEFAULT_AMT = rats_oa["DEFAULT_AMT"].ToString();
                        }
                        _RAS.Add(r_DOM4);
                        cmd = new OleDbCommand(sql, con);
                        oda = new OleDbDataAdapter(cmd);
                        OleDbDataReader rats_pc = cmd.ExecuteReader();
                        DetailofMIS r_DOM5 = new DetailofMIS();
                        while (rats_pc.Read())
                        {
                            count = count + 1;
                            r_DOM5._sFEID = DM._sFEID;
                            r_DOM5.TYPE = "Pending_Cases";
                            r_DOM5.CNT = rats_pc["CNT"].ToString();
                            r_DOM5.DEFAULT_AMT = rats_pc["DEFAULT_AMT"].ToString();
                        }
                        _RAS.Add(r_DOM5);
                        response.Data = _RAS;
                        response.Status = "Success";
                        response.Message = "Data retrieved";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    return Msg;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [Route("API/Recovery/DetailofMIS")]
        [HttpPost]
        public HttpResponseMessage DetailofMIS([FromBody] DetailofMIS DM)
        {
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (DM.strKeyParam == PublicAccessTokenKey)
                {
                    int count = 0;
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    using (con = new OleDbConnection(strConnection))
                    {
                        string sql = "";
                        //sql = "SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(DEFAULT_AMT),0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN A,recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B";
                        //sql += " WHERE A.ACCOUNT_CLASS IN ('SLCC','MLCC')";
                        //sql += " AND A.UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')";
                        //sql += " AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC')";
                        //sql += " AND UPPER(B.ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";
                        //sql += " AND A.CA_NUMBER = B.CA_NUMBER AND A.UPDATION_ID = B.UPDATION_ID_DTLS AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS";
                        sql = "select sum(A.cnt) CNT,sum(A.DEFAULT_AMT) DEFAULT_AMT from ";
                        sql += " (SELECT COUNT(UNIQUE CA_NUMBER) CNT, NVL(SUM(DEFAULT_AMT), 0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN WHERE  ca_number IN("; //Added  By Babalu Kumar 10022021 PCN No. 2021021003
                        sql += " SELECT CA_NUMBER FROM recovery.DEFLTR_CA_ASSIGN_LINK_DTLS WHERE UPDATION_ID_DTLS= TO_CHAR(SYSDATE,'yyyyMM') AND UPPER(ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "'))";
                        sql += " and UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')";
                        sql += " UNION ";
                        sql += " SELECT COUNT(UNIQUE CA_NUMBER) CNT,NVL(SUM(DEFAULT_AMT), 0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_DISCARD WHERE  ca_number IN(";
                        sql += " SELECT CA_NUMBER FROM recovery.DEFLTR_CA_ASSIGN_LINK_DTLS WHERE UPDATION_ID_DTLS= TO_CHAR(SYSDATE,'yyyyMM') AND UPPER(ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "'))";
                        sql += " and UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')) A";

                        string sql1 = "";
                        //sql1 = "SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(DEFAULT_AMT),0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN A,recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B";
                        //sql1 += " WHERE A.ACCOUNT_CLASS IN ('SLCC','MLCC')";
                        //sql1 += " AND A.UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')";
                        //sql1 += " AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC')";
                        //sql1 += " AND UPPER(B.ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";
                        //sql1 += " AND A.CA_NUMBER = B.CA_NUMBER AND A.UPDATION_ID = B.UPDATION_ID_DTLS AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS";
                        //sql1 += " AND (B.STATUS IS NOT NULL OR B.STATUS != '') AND A.PAYMNT_AMT > 0";
                        sql1 = " SELECT SUM(A.CNT) CNT,SUM(A.DEFAULT_AMT) DEFAULT_AMT FROM";
                        sql1 += " (SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(PAYMNT_AMT), 0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN A,";
                        sql1 += " recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE";
                        sql1 += " A.UPDATION_ID = TO_CHAR(SYSDATE, 'yyyyMM')";
                        sql1 += " AND UPPER(ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";//Added  By Babalu Kumar 10022021 PCN No. 2021021003
                        sql1 += " AND A.CA_NUMBER = B.CA_NUMBER";
                        sql1 += " AND A.UPDATION_ID = B.UPDATION_ID_DTLS";
                        sql1 += " AND A.PAYMNT_AMT > 0";
                        sql1 += " AND PAYMNT_ENTRY_DT IS NOT NULL";
                        sql1 += " UNION ";
                        sql1 += " SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(PAYMNT_AMT), 0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_discard A,";
                        sql1 += " recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE";
                        sql1 += " A.UPDATION_ID = TO_CHAR(SYSDATE, 'yyyyMM')";
                        sql1 += " AND UPPER(ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";
                        sql1 += " AND A.CA_NUMBER = B.CA_NUMBER";
                        sql1 += " AND A.UPDATION_ID = B.UPDATION_ID_DTLS";
                        sql1 += " AND A.PAYMNT_AMT > 0";
                        sql1 += " AND PAYMNT_ENTRY_DT IS NOT NULL) A";

                        string sql2 = "";
                        sql2 = "SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(DEFAULT_AMT),0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN A,recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B";
                        sql2 += " WHERE A.ACCOUNT_CLASS IN ('SLCC','MLCC')";
                        sql2 += " AND A.UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')";
                        sql2 += " AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC')";
                        sql2 += " AND UPPER(B.ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";
                        sql2 += " AND A.CA_NUMBER = B.CA_NUMBER AND A.UPDATION_ID = B.UPDATION_ID_DTLS AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS";
                        sql2 += " AND B.STATUS IN ('01','18') AND A.PAYMNT_AMT = 0";

                        string sql3 = "";
                        sql3 = "SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(DEFAULT_AMT),0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN A,recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B";
                        sql3 += " WHERE A.ACCOUNT_CLASS IN ('SLCC','MLCC')";
                        sql3 += " AND A.UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')";
                        sql3 += " AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC')";
                        sql3 += " AND UPPER(B.ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";
                        sql3 += " AND A.CA_NUMBER = B.CA_NUMBER AND A.UPDATION_ID = B.UPDATION_ID_DTLS AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS";
                        sql3 += " AND B.STATUS = '15' AND A.PAYMNT_AMT = 0";

                        string sql4 = "";
                        sql4 = "SELECT NVL(COUNT(UNIQUE A.CA_NUMBER),0) CNT,NVL(SUM(DEFAULT_AMT),0) DEFAULT_AMT FROM recovery.DEFLTR_LIST_MAIN A,recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B";
                        sql4 += " WHERE A.ACCOUNT_CLASS IN ('SLCC','MLCC')";
                        sql4 += " AND A.UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM')";
                        sql4 += " AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC')";
                        sql4 += " AND UPPER(B.ASSIGN_USER_CODE) =UPPER('" + DM._sFEID + "')";
                        sql4 += " AND A.CA_NUMBER = B.CA_NUMBER AND A.UPDATION_ID = B.UPDATION_ID_DTLS AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS";
                        sql4 += " AND B.STATUS IN ('02','03','04','05','06','07','08','09','16','17','19','20','21','22','23','24','25') AND A.PAYMNT_AMT = 0";//Added  By Babalu Kumar 10022021 PCN No. 2021021003

                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        oda = new OleDbDataAdapter(cmd);
                        OleDbDataReader rats = cmd.ExecuteReader();
                        List<DetailofMIS> _RAS = new List<DetailofMIS>();
                        DetailofMIS r_DOM = new DetailofMIS();
                        while (rats.Read())
                        {
                            count = count + 1;
                            r_DOM._sFEID = DM._sFEID;
                            r_DOM.TYPE = "Allocated_Defaulters";
                            r_DOM.CNT = rats["CNT"].ToString();
                            r_DOM.DEFAULT_AMT = rats["DEFAULT_AMT"].ToString();
                        }
                        _RAS.Add(r_DOM);
                        cmd = new OleDbCommand(sql1, con);
                        oda = new OleDbDataAdapter(cmd);
                        OleDbDataReader rats_pr = cmd.ExecuteReader();
                        DetailofMIS r_DOM1 = new DetailofMIS();
                        while (rats_pr.Read())
                        {
                            count = count + 1;
                            r_DOM1._sFEID = DM._sFEID;
                            r_DOM1.TYPE = "Payment_Received";
                            r_DOM1.CNT = rats_pr["CNT"].ToString();
                            r_DOM1.DEFAULT_AMT = rats_pr["DEFAULT_AMT"].ToString();
                        }
                        _RAS.Add(r_DOM1);
                        cmd = new OleDbCommand(sql2, con);
                        oda = new OleDbDataAdapter(cmd);
                        OleDbDataReader rats_mr = cmd.ExecuteReader();
                        DetailofMIS r_DOM2 = new DetailofMIS();
                        while (rats_mr.Read())
                        {
                            count = count + 1;
                            r_DOM2._sFEID = DM._sFEID;
                            r_DOM2.TYPE = "Meter_Removed";
                            r_DOM2.CNT = rats_mr["CNT"].ToString();
                            r_DOM2.DEFAULT_AMT = rats_mr["DEFAULT_AMT"].ToString();
                        }
                        _RAS.Add(r_DOM2);
                        cmd = new OleDbCommand(sql3, con);
                        oda = new OleDbDataAdapter(cmd);
                        OleDbDataReader rats_dis = cmd.ExecuteReader();
                        DetailofMIS r_DOM3 = new DetailofMIS();
                        while (rats_dis.Read())
                        {
                            count = count + 1;
                            r_DOM3._sFEID = DM._sFEID;
                            r_DOM3.TYPE = "Disconnection";
                            r_DOM3.CNT = rats_dis["CNT"].ToString();
                            r_DOM3.DEFAULT_AMT = rats_dis["DEFAULT_AMT"].ToString();
                        }
                        _RAS.Add(r_DOM3);
                        cmd = new OleDbCommand(sql4, con);
                        oda = new OleDbDataAdapter(cmd);
                        OleDbDataReader rats_oa = cmd.ExecuteReader();
                        DetailofMIS r_DOM4 = new DetailofMIS();
                        while (rats_oa.Read())
                        {
                            count = count + 1;
                            r_DOM4._sFEID = DM._sFEID;
                            r_DOM4.TYPE = "Other_ATR";
                            r_DOM4.CNT = rats_oa["CNT"].ToString();
                            r_DOM4.DEFAULT_AMT = rats_oa["DEFAULT_AMT"].ToString();
                        }
                        _RAS.Add(r_DOM4);
                        cmd = new OleDbCommand(sql, con);
                        oda = new OleDbDataAdapter(cmd);
                        OleDbDataReader rats_pc = cmd.ExecuteReader();
                        DetailofMIS r_DOM5 = new DetailofMIS();
                        while (rats_pc.Read())
                        {
                            count = count + 1;
                            r_DOM5._sFEID = DM._sFEID;
                            r_DOM5.TYPE = "Pending_Cases";
                            r_DOM5.CNT = rats_pc["CNT"].ToString();
                            r_DOM5.DEFAULT_AMT = rats_pc["DEFAULT_AMT"].ToString();
                        }
                        _RAS.Add(r_DOM5);
                        response.Data = _RAS;
                        response.Status = "Success";
                        response.Message = "Data retrieved";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    return Msg;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }


        [Route("API/Recovery/RecServices_Aging_Bucket")]
        [HttpPost]
        public HttpResponseMessage RecServices_Aging_Bucket([FromBody] RecServices_Aging_Bucket IPN)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (IPN.strKeyParam == PublicAccessTokenKey)
                {
                    List<RecServices_Aging_Bucket> _RAB = new List<RecServices_Aging_Bucket>();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT '' AGIBKTID,'-All-' AGING_BUCKET,0 RANGE_FROM,0 RANGE_TO FROM DUAL UNION SELECT UNIQUE AGIBKTID,AGING_BUCKET,RANGE_FROM,RANGE_TO FROM RECAPP_AGIBKT_MST WHERE ACTIVE = 'Y' ORDER BY RANGE_TO";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecServices_Aging_Bucket poweroff_RAB = new RecServices_Aging_Bucket();
                                poweroff_RAB.AGIBKTID = dt.Rows[i]["AGIBKTID"].ToString();
                                poweroff_RAB.AGING_BUCKET = dt.Rows[i]["AGING_BUCKET"].ToString();
                                poweroff_RAB.RANGE_FROM = dt.Rows[i]["RANGE_FROM"].ToString();
                                poweroff_RAB.RANGE_TO = dt.Rows[i]["RANGE_TO"].ToString();
                                _RAB.Add(poweroff_RAB);
                                response.Data = _RAB;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }


        [Route("API/Recovery/Currentdatecases")]
        [HttpPost]
        public HttpResponseMessage Currentdatecases([FromBody] RecAllocDefltrs READ)
        {
            DT_Data PO = new DT_Data();
            List<RecAllocDefltrs> _RAD = new List<RecAllocDefltrs>();
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (READ.strKeyParam == PublicAccessTokenKey)
                {
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT A.COMPANY, A.CA_NUMBER, A.METER_NO, A.CRN_NUMBER, A.CYCLE_NO, A.ACCOUNT_CLASS, A.DIVISION, A.NAME, A.TELEPHONE_NO, A.HOUSE_NUMBER, A.STREET1, A.STREET2,";
                        sql += "A.STREET3, A.POLE_NO, A.POSTAL_CODE, A.STREET, A.SEQUENCE_NUMBER, A.RATE_CATEGORY, A.BILL_STATUS, A.DISHONOR_FLAG,";
                        //sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (A.LST_PYMT_DT || ' (' || TRUNC(TRUNC(MONTHS_BETWEEN (TRUNC(SYSDATE),TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))/12) || ' Years ' ||";
                        //sql += " MOD(TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'))), 12) || ' Months ' ||";
                        //sql += " ROUND( TRUNC(SYSDATE) - ADD_MONTHS(( TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')), TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))),0) || ' Days)') END) LST_PYMT_DT,";
                        sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (a.LST_PYMT_DT || ' (Pending Since ' || (trunc(sysdate) - trunc(a.LST_PYMT_DT)) || 'Days)') END) LST_PYMT_DT,";
                        sql += "A.DUE_DATE, A.CONTRACT, A.MOVE_IN_DATE, A.CURRENT_DEMAND, A.LAST_PAYMENT_AMT, A.SANCTION_LOAD, A.DEFAULT_AMT, A.LPSC_AMOUNT, A.PRINCIPAL_VALUE, A.CONSUMER_STATUS,";
                        sql += "A.SOFT_DISCONNECTION_DT, A.RUNNING_BAL_AS_ON_DT, A.CONNECTION_DT, A.INITIAL_BUCKET, A.NEW_OLD_FLAG, A.UPLOAD_CYCLE, A.UPDATION_ID, A.INITIAL_DEFAULT_AMT,";
                        sql += "A.PAYMNT_AMT, A.PAYMNT_ENTRY_DT, A.PAYMENT_MODE,B.ASSIGN_USER_CODE ASSIGNED_FE_ID, (SELECT UNIQUE C.FE_NAME_DISP FROM DEFLTR_AGENCY_FE_MST C WHERE B.ASSIGN_USER_CODE = C.FE_ID) ASSIGNED_FE_NAME";
                        sql += " FROM DEFLTR_LIST_MAIN A, DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1 AND TRUNC(B.FOLLOWUP_DATE)=TRUNC(SYSDATE)";
                        if (READ._sCategory != "")
                        {
                            if (READ._sCategory == "CAT0002")
                                sql += " AND A.DISHONOR_FLAG = 'Y'";
                            else if (READ._sCategory == "CAT0003")
                                sql += " AND (A.LAST_PAYMENT_AMT = 0 OR LST_PYMT_DT IS NULL)";
                            if (READ._sCategory == "CAT0004")
                                //sql += " AND FLOOR(TO_DATE(SYSDATE) - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) > 180";
                                sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT)>180";
                        }
                        if (READ._sAmtBktID != "")
                            sql += " AND A.DEFAULT_AMT BETWEEN (SELECT UNIQUE RANGE_FROM FROM RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "') AND (SELECT UNIQUE RANGE_TO FROM RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "')";

                        if (READ._sAgeBktID != "")
                        {
                            //sql += " AND FLOOR(TO_DATE('01/' ||  SUBSTR(A.UPDATION_ID,5,2) || '/' || SUBSTR(A.UPDATION_ID,0,4),'dd/MM/yyyy') - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) BETWEEN";
                            sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT) BETWEEN";
                            sql += " (SELECT UNIQUE RANGE_FROM FROM RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "') AND (SELECT UNIQUE RANGE_TO FROM RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "')";
                        }
                        if (READ._sAccountClass != "")
                            sql += " AND A.ACCOUNT_CLASS = '" + READ._sAccountClass + "'";
                        if (READ._sDishonorFlag == "Y")
                            sql += " AND A.DISHONOR_FLAG = 'X'";
                        else if (READ._sDishonorFlag == "N")
                            sql += " AND A.DISHONOR_FLAG != 'X'";
                        //sql += " AND A.UPDATION_ID = (SELECT UNIQUE TO_CHAR(MAX(TO_DATE(UPDATION_ID || '01','YYYYMMDD')),'yyyyMM') FROM DEFLTR_LIST_MAIN)";
                        sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                        sql += "  AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC','AGENCY') ";
                        if (READ._sFEID != "")
                            sql += " AND UPPER(B.ASSIGN_USER_CODE) = UPPER('" + READ._sFEID + "')";
                        sql += " AND A.CA_NUMBER = B.CA_NUMBER(+) AND A.UPDATION_ID = B.UPDATION_ID_DTLS(+) AND TAB_FLAG = 'Y' AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS(+)";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecAllocDefltrs sD_ats = new RecAllocDefltrs();
                                sD_ats._sCategory = READ._sCategory;
                                sD_ats._sAmtBktID = READ._sAmtBktID;
                                sD_ats._sAgeBktID = READ._sAgeBktID;
                                sD_ats._sAccountClass = READ._sAccountClass;
                                sD_ats._sDishonorFlag = READ._sDishonorFlag;
                                sD_ats._sFEID = READ._sFEID;
                                sD_ats.COMPANY = dt.Rows[i]["COMPANY"].ToString();
                                sD_ats.CA_NUMBER = dt.Rows[i]["CA_NUMBER"].ToString();
                                sD_ats.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                sD_ats.CRN_NUMBER = dt.Rows[i]["CRN_NUMBER"].ToString();
                                sD_ats.CYCLE_NO = dt.Rows[i]["CYCLE_NO"].ToString();
                                sD_ats.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                sD_ats.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                sD_ats.NAME = dt.Rows[i]["NAME"].ToString();
                                sD_ats.TELEPHONE_NO = dt.Rows[i]["TELEPHONE_NO"].ToString();
                                sD_ats.HOUSE_NUMBER = dt.Rows[i]["HOUSE_NUMBER"].ToString();
                                sD_ats.STREET1 = dt.Rows[i]["STREET1"].ToString();
                                sD_ats.STREET2 = dt.Rows[i]["STREET2"].ToString();
                                sD_ats.STREET3 = dt.Rows[i]["STREET3"].ToString();
                                sD_ats.POLE_NO = dt.Rows[i]["POLE_NO"].ToString();
                                sD_ats.POSTAL_CODE = dt.Rows[i]["POSTAL_CODE"].ToString();
                                sD_ats.STREET = dt.Rows[i]["STREET"].ToString();
                                sD_ats.SEQUENCE_NUMBER = dt.Rows[i]["SEQUENCE_NUMBER"].ToString();
                                sD_ats.RATE_CATEGORY = dt.Rows[i]["RATE_CATEGORY"].ToString();
                                sD_ats.BILL_STATUS = dt.Rows[i]["BILL_STATUS"].ToString();
                                sD_ats.DISHONOR_FLAG = dt.Rows[i]["DISHONOR_FLAG"].ToString();
                                sD_ats.LAST_PAYMENT_DT = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                //if (dt.Rows[i]["LST_PYMT_DT"].ToString() != null && dt.Rows[i]["LST_PYMT_DT"].ToString() != "")
                                //{
                                //    string str = sD_ats.LAST_PAYMENT_DT;//Added By Babalu Kumar
                                //    if (str != null)
                                //    {
                                //        string[] strarray = str.Split('/');
                                //        string sdate = strarray[0].ToString();
                                //        string smonth = strarray[1].ToString();
                                //        string syear = strarray[2].ToString();
                                //        if (Convert.ToInt32(smonth) > 12)
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = smonth + "/" + sdate + "/" + syear;
                                //        }
                                //        else
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = sdate + "/" + smonth + "/" + syear;
                                //        }
                                //    }
                                //}
                                sD_ats.DUE_DATE = dt.Rows[i]["DUE_DATE"].ToString();
                                sD_ats.CONTRACT = dt.Rows[i]["CONTRACT"].ToString();
                                sD_ats.MOVE_IN_DATE = dt.Rows[i]["MOVE_IN_DATE"].ToString();
                                sD_ats.CURRENT_DEMAND = dt.Rows[i]["CURRENT_DEMAND"].ToString();
                                sD_ats.LAST_PAYMENT_AMT = dt.Rows[i]["LAST_PAYMENT_AMT"].ToString();
                                sD_ats.SANCTION_LOAD = dt.Rows[i]["SANCTION_LOAD"].ToString();
                                sD_ats.DEFAULT_AMT = dt.Rows[i]["DEFAULT_AMT"].ToString();
                                sD_ats.LPSC_AMOUNT = dt.Rows[i]["LPSC_AMOUNT"].ToString();
                                sD_ats.PRINCIPAL_VALUE = dt.Rows[i]["PRINCIPAL_VALUE"].ToString();
                                sD_ats.CONSUMER_STATUS = dt.Rows[i]["CONSUMER_STATUS"].ToString();
                                sD_ats.SOFT_DISCONNECTION_DT = dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString();
                                sD_ats.RUNNING_BAL_AS_ON_DT = dt.Rows[i]["RUNNING_BAL_AS_ON_DT"].ToString();
                                sD_ats.CONNECTION_DT = dt.Rows[i]["CONNECTION_DT"].ToString();
                                sD_ats.INITIAL_BUCKET = dt.Rows[i]["INITIAL_BUCKET"].ToString();
                                sD_ats.NEW_OLD_FLAG = dt.Rows[i]["NEW_OLD_FLAG"].ToString();
                                sD_ats.UPLOAD_CYCLE = dt.Rows[i]["UPLOAD_CYCLE"].ToString();
                                sD_ats.UPDATION_ID = dt.Rows[i]["UPDATION_ID"].ToString();
                                sD_ats.INITIAL_DEFAULT_AMT = dt.Rows[i]["INITIAL_DEFAULT_AMT"].ToString();
                                sD_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                sD_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                sD_ats.PAYMENT_MODE = dt.Rows[i]["PAYMENT_MODE"].ToString();
                                sD_ats.ASSIGNED_FE_ID = dt.Rows[i]["ASSIGNED_FE_ID"].ToString();
                                sD_ats.ASSIGNED_FE_NAME = dt.Rows[i]["ASSIGNED_FE_NAME"].ToString();

                                string sqlnew = "select Directive from recovery.DEFLTER_Acceptence_MST where Active='Y' and UPPER(RATE_CATEGORY)=UPPER('" + sD_ats.RATE_CATEGORY + "')";
                                DataTable dt1 = new DataTable();
                                cmd = new OleDbCommand(sqlnew, con);
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                adp = new OleDbDataAdapter(cmd);
                                adp.Fill(dt1);
                                if (dt1.Rows.Count > 0)
                                {
                                    sD_ats.DIRECTIVE = dt1.Rows[0]["Directive"].ToString();
                                }
                                _RAD.Add(sD_ats);
                                response.Data = _RAD;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);

                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);

            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return Msg;
        }

        [Route("API/Recovery/Currentdatecases1")]
        [HttpPost]
        public HttpResponseMessage Currentdatecases1([FromBody] RecAllocDefltrs READ)
        {
            DT_Data PO = new DT_Data();
            List<RecAllocDefltrs> _RAD = new List<RecAllocDefltrs>();
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (READ.strKeyParam == PublicAccessTokenKey)
                {
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT A.COMPANY, A.CA_NUMBER, A.METER_NO, A.CRN_NUMBER, A.CYCLE_NO, A.ACCOUNT_CLASS, A.DIVISION, A.NAME, A.TELEPHONE_NO, A.HOUSE_NUMBER, A.STREET1, A.STREET2,";
                        sql += "A.STREET3, A.POLE_NO, A.POSTAL_CODE, A.STREET, A.SEQUENCE_NUMBER, A.RATE_CATEGORY, A.BILL_STATUS, A.DISHONOR_FLAG,";
                        //sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (A.LST_PYMT_DT || ' (' || TRUNC(TRUNC(MONTHS_BETWEEN (TRUNC(SYSDATE),TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))/12) || ' Years ' ||";
                        //sql += " MOD(TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'))), 12) || ' Months ' ||";
                        //sql += " ROUND( TRUNC(SYSDATE) - ADD_MONTHS(( TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')), TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))),0) || ' Days)') END) LST_PYMT_DT,";
                        sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (a.LST_PYMT_DT || ' (Pending Since ' || (trunc(sysdate) - trunc(a.LST_PYMT_DT)) || 'Days)') END) LST_PYMT_DT,";
                        sql += "A.DUE_DATE, A.CONTRACT, A.MOVE_IN_DATE, A.CURRENT_DEMAND, A.LAST_PAYMENT_AMT, A.SANCTION_LOAD, A.DEFAULT_AMT, A.LPSC_AMOUNT, A.PRINCIPAL_VALUE, A.CONSUMER_STATUS,";
                        sql += "A.SOFT_DISCONNECTION_DT, A.RUNNING_BAL_AS_ON_DT, A.CONNECTION_DT, A.INITIAL_BUCKET, A.NEW_OLD_FLAG, A.UPLOAD_CYCLE, A.UPDATION_ID, A.INITIAL_DEFAULT_AMT,";
                        sql += "A.PAYMNT_AMT, A.PAYMNT_ENTRY_DT, A.PAYMENT_MODE,B.ASSIGN_USER_CODE ASSIGNED_FE_ID, (SELECT UNIQUE C.FE_NAME_DISP FROM DEFLTR_AGENCY_FE_MST C WHERE B.ASSIGN_USER_CODE = C.FE_ID) ASSIGNED_FE_NAME";
                        sql += " FROM DEFLTR_LIST_MAIN A, DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1 AND TRUNC(B.FOLLOWUP_DATE)=TRUNC(SYSDATE)";
                        if (READ._sCategory != "")
                        {
                            if (READ._sCategory == "CAT0002")
                                sql += " AND A.DISHONOR_FLAG = 'Y'";
                            else if (READ._sCategory == "CAT0003")
                                sql += " AND (A.LAST_PAYMENT_AMT = 0 OR LST_PYMT_DT IS NULL)";
                            if (READ._sCategory == "CAT0004")
                                //sql += " AND FLOOR(TO_DATE(SYSDATE) - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) > 180";
                                sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT)>180";
                        }
                        if (READ._sAmtBktID != "")
                            sql += " AND A.DEFAULT_AMT BETWEEN (SELECT UNIQUE RANGE_FROM FROM RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "') AND (SELECT UNIQUE RANGE_TO FROM RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "')";

                        if (READ._sAgeBktID != "")
                        {
                            //sql += " AND FLOOR(TO_DATE('01/' ||  SUBSTR(A.UPDATION_ID,5,2) || '/' || SUBSTR(A.UPDATION_ID,0,4),'dd/MM/yyyy') - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) BETWEEN";
                            sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT) BETWEEN";
                            sql += " (SELECT UNIQUE RANGE_FROM FROM RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "') AND (SELECT UNIQUE RANGE_TO FROM RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "')";
                        }
                        if (READ._sAccountClass != "")
                            sql += " AND A.ACCOUNT_CLASS = '" + READ._sAccountClass + "'";
                        if (READ._sDishonorFlag == "Y")
                            sql += " AND A.DISHONOR_FLAG = 'X'";
                        else if (READ._sDishonorFlag == "N")
                            sql += " AND A.DISHONOR_FLAG != 'X'";
                        //sql += " AND A.UPDATION_ID = (SELECT UNIQUE TO_CHAR(MAX(TO_DATE(UPDATION_ID || '01','YYYYMMDD')),'yyyyMM') FROM DEFLTR_LIST_MAIN)";
                        sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                        sql += "  AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC','AGENCY') ";
                        if (READ._sFEID != "")
                            sql += " AND UPPER(B.ASSIGN_USER_CODE) = UPPER('" + READ._sFEID + "')";
                        sql += " AND A.CA_NUMBER = B.CA_NUMBER(+) AND A.UPDATION_ID = B.UPDATION_ID_DTLS(+) AND TAB_FLAG = 'N' AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS(+)";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecAllocDefltrs sD_ats = new RecAllocDefltrs();
                                sD_ats._sCategory = READ._sCategory;
                                sD_ats._sAmtBktID = READ._sAmtBktID;
                                sD_ats._sAgeBktID = READ._sAgeBktID;
                                sD_ats._sAccountClass = READ._sAccountClass;
                                sD_ats._sDishonorFlag = READ._sDishonorFlag;
                                sD_ats._sFEID = READ._sFEID;
                                sD_ats.COMPANY = dt.Rows[i]["COMPANY"].ToString();
                                sD_ats.CA_NUMBER = dt.Rows[i]["CA_NUMBER"].ToString();
                                sD_ats.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                sD_ats.CRN_NUMBER = dt.Rows[i]["CRN_NUMBER"].ToString();
                                sD_ats.CYCLE_NO = dt.Rows[i]["CYCLE_NO"].ToString();
                                sD_ats.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                sD_ats.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                sD_ats.NAME = dt.Rows[i]["NAME"].ToString();
                                sD_ats.TELEPHONE_NO = dt.Rows[i]["TELEPHONE_NO"].ToString();
                                sD_ats.HOUSE_NUMBER = dt.Rows[i]["HOUSE_NUMBER"].ToString();
                                sD_ats.STREET1 = dt.Rows[i]["STREET1"].ToString();
                                sD_ats.STREET2 = dt.Rows[i]["STREET2"].ToString();
                                sD_ats.STREET3 = dt.Rows[i]["STREET3"].ToString();
                                sD_ats.POLE_NO = dt.Rows[i]["POLE_NO"].ToString();
                                sD_ats.POSTAL_CODE = dt.Rows[i]["POSTAL_CODE"].ToString();
                                sD_ats.STREET = dt.Rows[i]["STREET"].ToString();
                                sD_ats.SEQUENCE_NUMBER = dt.Rows[i]["SEQUENCE_NUMBER"].ToString();
                                sD_ats.RATE_CATEGORY = dt.Rows[i]["RATE_CATEGORY"].ToString();
                                sD_ats.BILL_STATUS = dt.Rows[i]["BILL_STATUS"].ToString();
                                sD_ats.DISHONOR_FLAG = dt.Rows[i]["DISHONOR_FLAG"].ToString();
                                sD_ats.LAST_PAYMENT_DT = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                //if (dt.Rows[i]["LST_PYMT_DT"].ToString() != null && dt.Rows[i]["LST_PYMT_DT"].ToString() != "")
                                //{
                                //    string str = sD_ats.LAST_PAYMENT_DT;//Added By Babalu Kumar
                                //    if (str != null)
                                //    {
                                //        string[] strarray = str.Split('/');
                                //        string sdate = strarray[0].ToString();
                                //        string smonth = strarray[1].ToString();
                                //        string syear = strarray[2].ToString();
                                //        if (Convert.ToInt32(smonth) > 12)
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = smonth + "/" + sdate + "/" + syear;
                                //        }
                                //        else
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = sdate + "/" + smonth + "/" + syear;
                                //        }
                                //    }
                                //}
                                sD_ats.DUE_DATE = dt.Rows[i]["DUE_DATE"].ToString();
                                sD_ats.CONTRACT = dt.Rows[i]["CONTRACT"].ToString();
                                sD_ats.MOVE_IN_DATE = dt.Rows[i]["MOVE_IN_DATE"].ToString();
                                sD_ats.CURRENT_DEMAND = dt.Rows[i]["CURRENT_DEMAND"].ToString();
                                sD_ats.LAST_PAYMENT_AMT = dt.Rows[i]["LAST_PAYMENT_AMT"].ToString();
                                sD_ats.SANCTION_LOAD = dt.Rows[i]["SANCTION_LOAD"].ToString();
                                sD_ats.DEFAULT_AMT = dt.Rows[i]["DEFAULT_AMT"].ToString();
                                sD_ats.LPSC_AMOUNT = dt.Rows[i]["LPSC_AMOUNT"].ToString();
                                sD_ats.PRINCIPAL_VALUE = dt.Rows[i]["PRINCIPAL_VALUE"].ToString();
                                sD_ats.CONSUMER_STATUS = dt.Rows[i]["CONSUMER_STATUS"].ToString();
                                sD_ats.SOFT_DISCONNECTION_DT = dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString();
                                sD_ats.RUNNING_BAL_AS_ON_DT = dt.Rows[i]["RUNNING_BAL_AS_ON_DT"].ToString();
                                sD_ats.CONNECTION_DT = dt.Rows[i]["CONNECTION_DT"].ToString();
                                sD_ats.INITIAL_BUCKET = dt.Rows[i]["INITIAL_BUCKET"].ToString();
                                sD_ats.NEW_OLD_FLAG = dt.Rows[i]["NEW_OLD_FLAG"].ToString();
                                sD_ats.UPLOAD_CYCLE = dt.Rows[i]["UPLOAD_CYCLE"].ToString();
                                sD_ats.UPDATION_ID = dt.Rows[i]["UPDATION_ID"].ToString();
                                sD_ats.INITIAL_DEFAULT_AMT = dt.Rows[i]["INITIAL_DEFAULT_AMT"].ToString();
                                sD_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                sD_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                sD_ats.PAYMENT_MODE = dt.Rows[i]["PAYMENT_MODE"].ToString();
                                sD_ats.ASSIGNED_FE_ID = dt.Rows[i]["ASSIGNED_FE_ID"].ToString();
                                sD_ats.ASSIGNED_FE_NAME = dt.Rows[i]["ASSIGNED_FE_NAME"].ToString();
                                _RAD.Add(sD_ats);
                                response.Data = _RAD;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);

                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);

            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [Route("API/Recovery/TempCaseList")]
        [HttpPost]
        public HttpResponseMessage TempCaseList([FromBody] RecAllocDefltrs READ)/////Added  By Babalu Kumar 10022021 PCN No.  1311202008
        {
            DT_Data PO = new DT_Data();
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (READ.strKeyParam == PublicAccessTokenKey)
                {
                    List<RecAllocDefltrs> _RAD = new List<RecAllocDefltrs>();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT A.COMPANY, A.CA_NUMBER, A.METER_NO, A.CRN_NUMBER, A.CYCLE_NO, A.ACCOUNT_CLASS, A.DIVISION, A.NAME, A.TELEPHONE_NO, A.HOUSE_NUMBER, A.STREET1, A.STREET2,";
                        sql += "A.STREET3, A.POLE_NO, A.POSTAL_CODE, A.STREET, A.SEQUENCE_NUMBER, A.RATE_CATEGORY, A.BILL_STATUS, A.DISHONOR_FLAG,";
                        //sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (A.LST_PYMT_DT || ' (' || TRUNC(TRUNC(MONTHS_BETWEEN (TRUNC(SYSDATE),TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))/12) || ' Years ' ||";
                        //sql += " MOD(TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'))), 12) || ' Months ' ||";
                        //sql += " ROUND( TRUNC(SYSDATE) - ADD_MONTHS(( TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')), TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))),0) || ' Days)') END) LST_PYMT_DT,";
                        sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (a.LST_PYMT_DT || ' (Pending Since ' || (trunc(sysdate) - trunc(a.LST_PYMT_DT)) || 'Days)') END) LST_PYMT_DT,";
                        sql += "A.DUE_DATE, A.CONTRACT, A.MOVE_IN_DATE, A.CURRENT_DEMAND, A.LAST_PAYMENT_AMT, A.SANCTION_LOAD, A.DEFAULT_AMT, A.LPSC_AMOUNT, A.PRINCIPAL_VALUE, A.CONSUMER_STATUS,";
                        sql += "A.SOFT_DISCONNECTION_DT, A.RUNNING_BAL_AS_ON_DT, A.CONNECTION_DT, A.INITIAL_BUCKET, A.NEW_OLD_FLAG, A.UPLOAD_CYCLE, A.UPDATION_ID, A.INITIAL_DEFAULT_AMT,";
                        sql += "A.PAYMNT_AMT, A.PAYMNT_ENTRY_DT, A.PAYMENT_MODE,B.ASSIGN_USER_CODE ASSIGNED_FE_ID, (SELECT UNIQUE C.FE_NAME_DISP FROM recovery.DEFLTR_AGENCY_FE_MST C WHERE B.ASSIGN_USER_CODE = C.FE_ID) ASSIGNED_FE_NAME";
                        sql += " FROM recovery.DEFLTR_LIST_MAIN A, recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1 and  a.CA_NUMBER like '3%' ";
                        if (READ._sCategory != "")
                        {
                            if (READ._sCategory == "CAT0002")
                                sql += " AND A.DISHONOR_FLAG = 'Y'";
                            else if (READ._sCategory == "CAT0003")
                                sql += " AND (A.LAST_PAYMENT_AMT = 0 OR LST_PYMT_DT IS NULL)";
                            if (READ._sCategory == "CAT0004")
                                sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT)>180";
                            //sql += " AND FLOOR(TO_DATE(SYSDATE) - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) > 180";
                        }
                        if (READ._sAmtBktID != "")
                            sql += " AND A.DEFAULT_AMT BETWEEN (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "') AND (SELECT UNIQUE RANGE_TO FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "')";

                        if (READ._sAgeBktID != "")
                        {
                            //sql += " AND FLOOR(TO_DATE('01/' ||  SUBSTR(A.UPDATION_ID,5,2) || '/' || SUBSTR(A.UPDATION_ID,0,4),'dd/MM/yyyy') - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) BETWEEN";
                            sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT) BETWEEN";
                            sql += " (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "') AND (SELECT UNIQUE RANGE_TO FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "')";
                        }
                        if (READ._sAccountClass != "")
                            sql += " AND A.ACCOUNT_CLASS = '" + READ._sAccountClass + "'";
                        if (READ._sDishonorFlag == "Y")
                            sql += " AND A.DISHONOR_FLAG = 'X'";
                        else if (READ._sDishonorFlag == "N")
                            // sql += " AND A.DISHONOR_FLAG != 'X'";
                            sql += " AND (A.DISHONOR_FLAG is null OR A.DISHONOR_FLAG = 'Y')";
                        //sql += " AND A.UPDATION_ID = (SELECT UNIQUE TO_CHAR(MAX(TO_DATE(UPDATION_ID || '01','YYYYMMDD')),'yyyyMM') FROM recovery.DEFLTR_LIST_MAIN)";
                        sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                        sql += "  AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC') ";
                        if (READ._sFEID != "")
                            sql += " AND UPPER(B.ASSIGN_USER_CODE) = UPPER('" + READ._sFEID + "')";
                        sql += " AND A.CA_NUMBER = B.CA_NUMBER(+) AND A.UPDATION_ID = B.UPDATION_ID_DTLS(+) AND TAB_FLAG = 'N' AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS(+) ORDER BY SEQUENCE_NUMBER";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecAllocDefltrs sD_ats = new RecAllocDefltrs();
                                sD_ats._sCategory = READ._sCategory;
                                sD_ats._sAmtBktID = READ._sAmtBktID;
                                sD_ats._sAgeBktID = READ._sAgeBktID;
                                sD_ats._sAccountClass = READ._sAccountClass;
                                sD_ats._sDishonorFlag = READ._sDishonorFlag;
                                sD_ats._sFEID = READ._sFEID;
                                sD_ats.COMPANY = dt.Rows[i]["COMPANY"].ToString();
                                sD_ats.CA_NUMBER = dt.Rows[i]["CA_NUMBER"].ToString();
                                sD_ats.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                sD_ats.CRN_NUMBER = dt.Rows[i]["CRN_NUMBER"].ToString();
                                sD_ats.CYCLE_NO = dt.Rows[i]["CYCLE_NO"].ToString();
                                sD_ats.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                sD_ats.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                sD_ats.NAME = dt.Rows[i]["NAME"].ToString();
                                sD_ats.TELEPHONE_NO = dt.Rows[i]["TELEPHONE_NO"].ToString();
                                sD_ats.HOUSE_NUMBER = dt.Rows[i]["HOUSE_NUMBER"].ToString();
                                sD_ats.STREET1 = dt.Rows[i]["STREET1"].ToString();
                                sD_ats.STREET2 = dt.Rows[i]["STREET2"].ToString();
                                sD_ats.STREET3 = dt.Rows[i]["STREET3"].ToString();
                                sD_ats.POLE_NO = dt.Rows[i]["POLE_NO"].ToString();
                                sD_ats.POSTAL_CODE = dt.Rows[i]["POSTAL_CODE"].ToString();
                                sD_ats.STREET = dt.Rows[i]["STREET"].ToString();
                                sD_ats.SEQUENCE_NUMBER = dt.Rows[i]["SEQUENCE_NUMBER"].ToString();
                                sD_ats.RATE_CATEGORY = dt.Rows[i]["RATE_CATEGORY"].ToString();
                                sD_ats.BILL_STATUS = dt.Rows[i]["BILL_STATUS"].ToString();
                                sD_ats.DISHONOR_FLAG = dt.Rows[i]["DISHONOR_FLAG"].ToString();
                                sD_ats.LAST_PAYMENT_DT = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                //if (dt.Rows[i]["LST_PYMT_DT"].ToString() != null && dt.Rows[i]["LST_PYMT_DT"].ToString() != "")
                                //{
                                //    string str = sD_ats.LAST_PAYMENT_DT;//Added By Babalu Kumar
                                //    if (str != null)
                                //    {
                                //        string[] strarray = str.Split('/');
                                //        string sdate = strarray[0].ToString();
                                //        string smonth = strarray[1].ToString();
                                //        string syear = strarray[2].ToString();
                                //        if (Convert.ToInt32(smonth) > 12)
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = smonth + "/" + sdate + "/" + syear;
                                //        }
                                //        else
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = sdate + "/" + smonth + "/" + syear;
                                //        }
                                //    }
                                //}
                                sD_ats.DUE_DATE = dt.Rows[i]["DUE_DATE"].ToString();
                                sD_ats.CONTRACT = dt.Rows[i]["CONTRACT"].ToString();
                                sD_ats.MOVE_IN_DATE = dt.Rows[i]["MOVE_IN_DATE"].ToString();
                                sD_ats.CURRENT_DEMAND = dt.Rows[i]["CURRENT_DEMAND"].ToString();
                                sD_ats.LAST_PAYMENT_AMT = dt.Rows[i]["LAST_PAYMENT_AMT"].ToString();
                                sD_ats.SANCTION_LOAD = dt.Rows[i]["SANCTION_LOAD"].ToString();
                                sD_ats.DEFAULT_AMT = dt.Rows[i]["DEFAULT_AMT"].ToString();
                                sD_ats.LPSC_AMOUNT = dt.Rows[i]["LPSC_AMOUNT"].ToString();
                                sD_ats.PRINCIPAL_VALUE = dt.Rows[i]["PRINCIPAL_VALUE"].ToString();
                                sD_ats.CONSUMER_STATUS = dt.Rows[i]["CONSUMER_STATUS"].ToString();
                                sD_ats.SOFT_DISCONNECTION_DT = dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString();
                                sD_ats.RUNNING_BAL_AS_ON_DT = dt.Rows[i]["RUNNING_BAL_AS_ON_DT"].ToString();
                                sD_ats.CONNECTION_DT = dt.Rows[i]["CONNECTION_DT"].ToString();
                                sD_ats.INITIAL_BUCKET = dt.Rows[i]["INITIAL_BUCKET"].ToString();
                                sD_ats.NEW_OLD_FLAG = dt.Rows[i]["NEW_OLD_FLAG"].ToString();
                                sD_ats.UPLOAD_CYCLE = dt.Rows[i]["UPLOAD_CYCLE"].ToString();
                                sD_ats.UPDATION_ID = dt.Rows[i]["UPDATION_ID"].ToString();
                                sD_ats.INITIAL_DEFAULT_AMT = dt.Rows[i]["INITIAL_DEFAULT_AMT"].ToString();
                                sD_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                sD_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                sD_ats.PAYMENT_MODE = dt.Rows[i]["PAYMENT_MODE"].ToString();
                                sD_ats.ASSIGNED_FE_ID = dt.Rows[i]["ASSIGNED_FE_ID"].ToString();
                                sD_ats.ASSIGNED_FE_NAME = dt.Rows[i]["ASSIGNED_FE_NAME"].ToString();

                                string sqlnew = "select Directive from recovery.DEFLTER_Acceptence_MST where Active='Y' and UPPER(RATE_CATEGORY)=UPPER('" + sD_ats.RATE_CATEGORY + "')";
                                DataTable dt1 = new DataTable();
                                cmd = new OleDbCommand(sqlnew, con);
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                adp = new OleDbDataAdapter(cmd);
                                adp.Fill(dt1);
                                if (dt1.Rows.Count > 0)
                                {
                                    sD_ats.DIRECTIVE = dt1.Rows[0]["Directive"].ToString();
                                }

                                _RAD.Add(sD_ats);
                                response.Data = _RAD;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [Route("API/Recovery/TempCaseList1")]
        [HttpPost]
        public HttpResponseMessage TempCaseList1([FromBody] RecAllocDefltrs READ)//Added  By Babalu Kumar 24122020
        {
            DT_Data PO = new DT_Data();
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (READ.strKeyParam == PublicAccessTokenKey)
                {
                    List<RecAllocDefltrs> _RAD = new List<RecAllocDefltrs>();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT A.COMPANY, A.CA_NUMBER, A.METER_NO, A.CRN_NUMBER, A.CYCLE_NO, A.ACCOUNT_CLASS, A.DIVISION, A.NAME, A.TELEPHONE_NO, A.HOUSE_NUMBER, A.STREET1, A.STREET2,";
                        sql += "A.STREET3, A.POLE_NO, A.POSTAL_CODE, A.STREET, A.SEQUENCE_NUMBER, A.RATE_CATEGORY, A.BILL_STATUS, A.DISHONOR_FLAG,";
                        //sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (A.LST_PYMT_DT || ' (' || TRUNC(TRUNC(MONTHS_BETWEEN (TRUNC(SYSDATE),TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))/12) || ' Years ' ||";
                        //sql += " MOD(TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'))), 12) || ' Months ' ||";
                        //sql += " ROUND( TRUNC(SYSDATE) - ADD_MONTHS(( TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')), TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))),0) || ' Days)') END) LST_PYMT_DT,";
                        sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (a.LST_PYMT_DT || ' (Pending Since ' || (trunc(sysdate) - trunc(a.LST_PYMT_DT)) || 'Days)') END) LST_PYMT_DT,";
                        sql += "A.DUE_DATE, A.CONTRACT, A.MOVE_IN_DATE, A.CURRENT_DEMAND, A.LAST_PAYMENT_AMT, A.SANCTION_LOAD, A.DEFAULT_AMT, A.LPSC_AMOUNT, A.PRINCIPAL_VALUE, A.CONSUMER_STATUS,";
                        sql += "A.SOFT_DISCONNECTION_DT, A.RUNNING_BAL_AS_ON_DT, A.CONNECTION_DT, A.INITIAL_BUCKET, A.NEW_OLD_FLAG, A.UPLOAD_CYCLE, A.UPDATION_ID, A.INITIAL_DEFAULT_AMT,";
                        sql += "A.PAYMNT_AMT, A.PAYMNT_ENTRY_DT, A.PAYMENT_MODE,B.ASSIGN_USER_CODE ASSIGNED_FE_ID, (SELECT UNIQUE C.FE_NAME_DISP FROM recovery.DEFLTR_AGENCY_FE_MST C WHERE B.ASSIGN_USER_CODE = C.FE_ID) ASSIGNED_FE_NAME";
                        sql += " FROM recovery.DEFLTR_LIST_MAIN A, recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1 and  a.CA_NUMBER like '3%' ";
                        if (READ._sCategory != "")
                        {
                            if (READ._sCategory == "CAT0002")
                                sql += " AND A.DISHONOR_FLAG = 'Y'";
                            else if (READ._sCategory == "CAT0003")
                                sql += " AND (A.LAST_PAYMENT_AMT = 0 OR LST_PYMT_DT IS NULL)";
                            if (READ._sCategory == "CAT0004")
                                sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT)>180";
                            //sql += " AND FLOOR(TO_DATE(SYSDATE) - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) > 180";
                        }
                        if (READ._sAmtBktID != "")
                            sql += " AND A.DEFAULT_AMT BETWEEN (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "') AND (SELECT UNIQUE RANGE_TO FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "')";

                        if (READ._sAgeBktID != "")
                        {
                            //sql += " AND FLOOR(TO_DATE('01/' ||  SUBSTR(A.UPDATION_ID,5,2) || '/' || SUBSTR(A.UPDATION_ID,0,4),'dd/MM/yyyy') - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) BETWEEN";
                            sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT) BETWEEN";
                            sql += " (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "') AND (SELECT UNIQUE RANGE_TO FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "')";
                        }
                        if (READ._sAccountClass != "")
                            sql += " AND A.ACCOUNT_CLASS = '" + READ._sAccountClass + "'";
                        if (READ._sDishonorFlag == "Y")
                            sql += " AND A.DISHONOR_FLAG = 'X'";
                        else if (READ._sDishonorFlag == "N")
                            // sql += " AND A.DISHONOR_FLAG != 'X'";
                            sql += " AND (A.DISHONOR_FLAG is null OR A.DISHONOR_FLAG = 'Y')";
                        //sql += " AND A.UPDATION_ID = (SELECT UNIQUE TO_CHAR(MAX(TO_DATE(UPDATION_ID || '01','YYYYMMDD')),'yyyyMM') FROM recovery.DEFLTR_LIST_MAIN)";
                        sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                        sql += "  AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC') ";
                        if (READ._sFEID != "")
                            sql += " AND UPPER(B.ASSIGN_USER_CODE) = UPPER('" + READ._sFEID + "')";
                        sql += " AND A.CA_NUMBER = B.CA_NUMBER(+) AND A.UPDATION_ID = B.UPDATION_ID_DTLS(+) AND TAB_FLAG = 'N' AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS(+) ORDER BY SEQUENCE_NUMBER";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecAllocDefltrs sD_ats = new RecAllocDefltrs();
                                sD_ats._sCategory = READ._sCategory;
                                sD_ats._sAmtBktID = READ._sAmtBktID;
                                sD_ats._sAgeBktID = READ._sAgeBktID;
                                sD_ats._sAccountClass = READ._sAccountClass;
                                sD_ats._sDishonorFlag = READ._sDishonorFlag;
                                sD_ats._sFEID = READ._sFEID;
                                sD_ats.COMPANY = dt.Rows[i]["COMPANY"].ToString();
                                sD_ats.CA_NUMBER = dt.Rows[i]["CA_NUMBER"].ToString();
                                sD_ats.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                sD_ats.CRN_NUMBER = dt.Rows[i]["CRN_NUMBER"].ToString();
                                sD_ats.CYCLE_NO = dt.Rows[i]["CYCLE_NO"].ToString();
                                sD_ats.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                sD_ats.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                sD_ats.NAME = dt.Rows[i]["NAME"].ToString();
                                sD_ats.TELEPHONE_NO = dt.Rows[i]["TELEPHONE_NO"].ToString();
                                sD_ats.HOUSE_NUMBER = dt.Rows[i]["HOUSE_NUMBER"].ToString();
                                sD_ats.STREET1 = dt.Rows[i]["STREET1"].ToString();
                                sD_ats.STREET2 = dt.Rows[i]["STREET2"].ToString();
                                sD_ats.STREET3 = dt.Rows[i]["STREET3"].ToString();
                                sD_ats.POLE_NO = dt.Rows[i]["POLE_NO"].ToString();
                                sD_ats.POSTAL_CODE = dt.Rows[i]["POSTAL_CODE"].ToString();
                                sD_ats.STREET = dt.Rows[i]["STREET"].ToString();
                                sD_ats.SEQUENCE_NUMBER = dt.Rows[i]["SEQUENCE_NUMBER"].ToString();
                                sD_ats.RATE_CATEGORY = dt.Rows[i]["RATE_CATEGORY"].ToString();
                                sD_ats.BILL_STATUS = dt.Rows[i]["BILL_STATUS"].ToString();
                                sD_ats.DISHONOR_FLAG = dt.Rows[i]["DISHONOR_FLAG"].ToString();
                                sD_ats.LAST_PAYMENT_DT = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                //if (dt.Rows[i]["LST_PYMT_DT"].ToString() != null && dt.Rows[i]["LST_PYMT_DT"].ToString() != "")
                                //{
                                //    string str = sD_ats.LAST_PAYMENT_DT;//Added By Babalu Kumar
                                //    if (str != null)
                                //    {
                                //        string[] strarray = str.Split('/');
                                //        string sdate = strarray[0].ToString();
                                //        string smonth = strarray[1].ToString();
                                //        string syear = strarray[2].ToString();
                                //        if (Convert.ToInt32(smonth) > 12)
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = smonth + "/" + sdate + "/" + syear;
                                //        }
                                //        else
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = sdate + "/" + smonth + "/" + syear;
                                //        }
                                //    }
                                //}
                                sD_ats.DUE_DATE = dt.Rows[i]["DUE_DATE"].ToString();
                                sD_ats.CONTRACT = dt.Rows[i]["CONTRACT"].ToString();
                                sD_ats.MOVE_IN_DATE = dt.Rows[i]["MOVE_IN_DATE"].ToString();
                                sD_ats.CURRENT_DEMAND = dt.Rows[i]["CURRENT_DEMAND"].ToString();
                                sD_ats.LAST_PAYMENT_AMT = dt.Rows[i]["LAST_PAYMENT_AMT"].ToString();
                                sD_ats.SANCTION_LOAD = dt.Rows[i]["SANCTION_LOAD"].ToString();
                                sD_ats.DEFAULT_AMT = dt.Rows[i]["DEFAULT_AMT"].ToString();
                                sD_ats.LPSC_AMOUNT = dt.Rows[i]["LPSC_AMOUNT"].ToString();
                                sD_ats.PRINCIPAL_VALUE = dt.Rows[i]["PRINCIPAL_VALUE"].ToString();
                                sD_ats.CONSUMER_STATUS = dt.Rows[i]["CONSUMER_STATUS"].ToString();
                                sD_ats.SOFT_DISCONNECTION_DT = dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString();
                                sD_ats.RUNNING_BAL_AS_ON_DT = dt.Rows[i]["RUNNING_BAL_AS_ON_DT"].ToString();
                                sD_ats.CONNECTION_DT = dt.Rows[i]["CONNECTION_DT"].ToString();
                                sD_ats.INITIAL_BUCKET = dt.Rows[i]["INITIAL_BUCKET"].ToString();
                                sD_ats.NEW_OLD_FLAG = dt.Rows[i]["NEW_OLD_FLAG"].ToString();
                                sD_ats.UPLOAD_CYCLE = dt.Rows[i]["UPLOAD_CYCLE"].ToString();
                                sD_ats.UPDATION_ID = dt.Rows[i]["UPDATION_ID"].ToString();
                                sD_ats.INITIAL_DEFAULT_AMT = dt.Rows[i]["INITIAL_DEFAULT_AMT"].ToString();
                                sD_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                sD_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                sD_ats.PAYMENT_MODE = dt.Rows[i]["PAYMENT_MODE"].ToString();
                                sD_ats.ASSIGNED_FE_ID = dt.Rows[i]["ASSIGNED_FE_ID"].ToString();
                                sD_ats.ASSIGNED_FE_NAME = dt.Rows[i]["ASSIGNED_FE_NAME"].ToString();
                                _RAD.Add(sD_ats);
                                response.Data = _RAD;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }


        [Route("API/Recovery/SoftDisconnectionCase")]
        [HttpPost]
        public HttpResponseMessage SoftDisconnectionCase([FromBody] RecAllocDefltrs READ)/////Added  By Babalu Kumar 10022021 PCN No.  1311202008
        {
            DT_Data PO = new DT_Data();
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (READ.strKeyParam == PublicAccessTokenKey)
                {
                    List<RecAllocDefltrs> _RAD = new List<RecAllocDefltrs>();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT A.COMPANY, A.CA_NUMBER, A.METER_NO, A.CRN_NUMBER, A.CYCLE_NO, A.ACCOUNT_CLASS, A.DIVISION, A.NAME, A.TELEPHONE_NO, A.HOUSE_NUMBER, A.STREET1, A.STREET2,";
                        sql += "A.STREET3, A.POLE_NO, A.POSTAL_CODE, A.STREET, A.SEQUENCE_NUMBER, A.RATE_CATEGORY, A.BILL_STATUS, A.DISHONOR_FLAG,";
                        //sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (A.LST_PYMT_DT || ' (' || TRUNC(TRUNC(MONTHS_BETWEEN (TRUNC(SYSDATE),TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))/12) || ' Years ' ||";
                        //sql += " MOD(TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'))), 12) || ' Months ' ||";
                        //sql += " ROUND( TRUNC(SYSDATE) - ADD_MONTHS(( TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')), TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))),0) || ' Days)') END) LST_PYMT_DT,";
                        sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (a.LST_PYMT_DT || ' (Pending Since ' || (trunc(sysdate) - trunc(a.LST_PYMT_DT)) || 'Days)') END) LST_PYMT_DT,";
                        sql += "A.DUE_DATE, A.CONTRACT, A.MOVE_IN_DATE, A.CURRENT_DEMAND, A.LAST_PAYMENT_AMT, A.SANCTION_LOAD, A.DEFAULT_AMT, A.LPSC_AMOUNT, A.PRINCIPAL_VALUE, A.CONSUMER_STATUS,";
                        sql += "A.SOFT_DISCONNECTION_DT, A.RUNNING_BAL_AS_ON_DT, A.CONNECTION_DT, A.INITIAL_BUCKET, A.NEW_OLD_FLAG, A.UPLOAD_CYCLE, A.UPDATION_ID, A.INITIAL_DEFAULT_AMT,";
                        sql += "A.PAYMNT_AMT, A.PAYMNT_ENTRY_DT, A.PAYMENT_MODE,B.ASSIGN_USER_CODE ASSIGNED_FE_ID, (SELECT UNIQUE C.FE_NAME_DISP FROM recovery.DEFLTR_AGENCY_FE_MST C WHERE B.ASSIGN_USER_CODE = C.FE_ID) ASSIGNED_FE_NAME";
                        sql += " FROM recovery.DEFLTR_LIST_MAIN A, recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1 AND SOFT_DISCONNECTION_DT!='#'";
                        if (READ._sCategory != "")
                        {
                            if (READ._sCategory == "CAT0002")
                                sql += " AND A.DISHONOR_FLAG = 'Y'";
                            else if (READ._sCategory == "CAT0003")
                                sql += " AND (A.LAST_PAYMENT_AMT = 0 OR LST_PYMT_DT IS NULL)";
                            if (READ._sCategory == "CAT0004")
                                sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT)>180";
                            //sql += " AND FLOOR(TO_DATE(SYSDATE) - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) > 180";
                        }
                        if (READ._sAmtBktID != "")
                            sql += " AND A.DEFAULT_AMT BETWEEN (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "') AND (SELECT UNIQUE RANGE_TO FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "')";

                        if (READ._sAgeBktID != "")
                        {
                            //sql += " AND FLOOR(TO_DATE('01/' ||  SUBSTR(A.UPDATION_ID,5,2) || '/' || SUBSTR(A.UPDATION_ID,0,4),'dd/MM/yyyy') - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) BETWEEN";
                            sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT) BETWEEN";
                            sql += " (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "') AND (SELECT UNIQUE RANGE_TO FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "')";
                        }
                        if (READ._sAccountClass != "")
                            sql += " AND A.ACCOUNT_CLASS = '" + READ._sAccountClass + "'";
                        if (READ._sDishonorFlag == "Y")
                            sql += " AND A.DISHONOR_FLAG = 'X'";
                        else if (READ._sDishonorFlag == "N")
                            // sql += " AND A.DISHONOR_FLAG != 'X'";
                            sql += " AND (A.DISHONOR_FLAG is null OR A.DISHONOR_FLAG = 'Y')";
                        //sql += " AND A.UPDATION_ID = (SELECT UNIQUE TO_CHAR(MAX(TO_DATE(UPDATION_ID || '01','YYYYMMDD')),'yyyyMM') FROM recovery.DEFLTR_LIST_MAIN)";
                        sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                        sql += "  AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC') ";
                        if (READ._sFEID != "")
                            sql += " AND UPPER(B.ASSIGN_USER_CODE) = UPPER('" + READ._sFEID + "')";
                        sql += " AND A.CA_NUMBER = B.CA_NUMBER(+) AND A.UPDATION_ID = B.UPDATION_ID_DTLS(+) AND TAB_FLAG = 'N' AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS(+) ORDER BY SEQUENCE_NUMBER";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecAllocDefltrs sD_ats = new RecAllocDefltrs();
                                sD_ats._sCategory = READ._sCategory;
                                sD_ats._sAmtBktID = READ._sAmtBktID;
                                sD_ats._sAgeBktID = READ._sAgeBktID;
                                sD_ats._sAccountClass = READ._sAccountClass;
                                sD_ats._sDishonorFlag = READ._sDishonorFlag;
                                sD_ats._sFEID = READ._sFEID;
                                sD_ats.COMPANY = dt.Rows[i]["COMPANY"].ToString();
                                sD_ats.CA_NUMBER = dt.Rows[i]["CA_NUMBER"].ToString();
                                sD_ats.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                sD_ats.CRN_NUMBER = dt.Rows[i]["CRN_NUMBER"].ToString();
                                sD_ats.CYCLE_NO = dt.Rows[i]["CYCLE_NO"].ToString();
                                sD_ats.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                sD_ats.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                sD_ats.NAME = dt.Rows[i]["NAME"].ToString();
                                sD_ats.TELEPHONE_NO = dt.Rows[i]["TELEPHONE_NO"].ToString();
                                sD_ats.HOUSE_NUMBER = dt.Rows[i]["HOUSE_NUMBER"].ToString();
                                sD_ats.STREET1 = dt.Rows[i]["STREET1"].ToString();
                                sD_ats.STREET2 = dt.Rows[i]["STREET2"].ToString();
                                sD_ats.STREET3 = dt.Rows[i]["STREET3"].ToString();
                                sD_ats.POLE_NO = dt.Rows[i]["POLE_NO"].ToString();
                                sD_ats.POSTAL_CODE = dt.Rows[i]["POSTAL_CODE"].ToString();
                                sD_ats.STREET = dt.Rows[i]["STREET"].ToString();
                                sD_ats.SEQUENCE_NUMBER = dt.Rows[i]["SEQUENCE_NUMBER"].ToString();
                                sD_ats.RATE_CATEGORY = dt.Rows[i]["RATE_CATEGORY"].ToString();
                                sD_ats.BILL_STATUS = dt.Rows[i]["BILL_STATUS"].ToString();
                                sD_ats.DISHONOR_FLAG = dt.Rows[i]["DISHONOR_FLAG"].ToString();
                                sD_ats.LAST_PAYMENT_DT = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                //if (dt.Rows[i]["LST_PYMT_DT"].ToString() != null && dt.Rows[i]["LST_PYMT_DT"].ToString() != "")
                                //{
                                //    string str = sD_ats.LAST_PAYMENT_DT;//Added By Babalu Kumar
                                //    if (str != null)
                                //    {
                                //        string[] strarray = str.Split('/');
                                //        string sdate = strarray[0].ToString();
                                //        string smonth = strarray[1].ToString();
                                //        string syear = strarray[2].ToString();
                                //        if (Convert.ToInt32(smonth) > 12)
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = smonth + "/" + sdate + "/" + syear;
                                //        }
                                //        else
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = sdate + "/" + smonth + "/" + syear;
                                //        }
                                //    }
                                //}
                                sD_ats.DUE_DATE = dt.Rows[i]["DUE_DATE"].ToString();
                                sD_ats.CONTRACT = dt.Rows[i]["CONTRACT"].ToString();
                                sD_ats.MOVE_IN_DATE = dt.Rows[i]["MOVE_IN_DATE"].ToString();
                                sD_ats.CURRENT_DEMAND = dt.Rows[i]["CURRENT_DEMAND"].ToString();
                                sD_ats.LAST_PAYMENT_AMT = dt.Rows[i]["LAST_PAYMENT_AMT"].ToString();
                                sD_ats.SANCTION_LOAD = dt.Rows[i]["SANCTION_LOAD"].ToString();
                                sD_ats.DEFAULT_AMT = dt.Rows[i]["DEFAULT_AMT"].ToString();
                                sD_ats.LPSC_AMOUNT = dt.Rows[i]["LPSC_AMOUNT"].ToString();
                                sD_ats.PRINCIPAL_VALUE = dt.Rows[i]["PRINCIPAL_VALUE"].ToString();
                                sD_ats.CONSUMER_STATUS = dt.Rows[i]["CONSUMER_STATUS"].ToString();
                                var date = DateTime.ParseExact(dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InstalledUICulture);
                                sD_ats.SOFT_DISCONNECTION_DT = date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InstalledUICulture);
                                // sD_ats.SOFT_DISCONNECTION_DT = dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString();
                                sD_ats.RUNNING_BAL_AS_ON_DT = dt.Rows[i]["RUNNING_BAL_AS_ON_DT"].ToString();
                                sD_ats.CONNECTION_DT = dt.Rows[i]["CONNECTION_DT"].ToString();
                                sD_ats.INITIAL_BUCKET = dt.Rows[i]["INITIAL_BUCKET"].ToString();
                                sD_ats.NEW_OLD_FLAG = dt.Rows[i]["NEW_OLD_FLAG"].ToString();
                                sD_ats.UPLOAD_CYCLE = dt.Rows[i]["UPLOAD_CYCLE"].ToString();
                                sD_ats.UPDATION_ID = dt.Rows[i]["UPDATION_ID"].ToString();
                                sD_ats.INITIAL_DEFAULT_AMT = dt.Rows[i]["INITIAL_DEFAULT_AMT"].ToString();
                                sD_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                sD_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                sD_ats.PAYMENT_MODE = dt.Rows[i]["PAYMENT_MODE"].ToString();
                                sD_ats.ASSIGNED_FE_ID = dt.Rows[i]["ASSIGNED_FE_ID"].ToString();
                                sD_ats.ASSIGNED_FE_NAME = dt.Rows[i]["ASSIGNED_FE_NAME"].ToString();

                                string sqlnew = "select Directive from recovery.DEFLTER_Acceptence_MST where Active='Y' and UPPER(RATE_CATEGORY)=UPPER('" + sD_ats.RATE_CATEGORY + "')";
                                DataTable dt1 = new DataTable();
                                cmd = new OleDbCommand(sqlnew, con);
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                adp = new OleDbDataAdapter(cmd);
                                adp.Fill(dt1);
                                if (dt1.Rows.Count > 0)
                                {
                                    sD_ats.DIRECTIVE = dt1.Rows[0]["Directive"].ToString();
                                }
                                _RAD.Add(sD_ats);
                                response.Data = _RAD;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [Route("API/Recovery/SoftDisconnectionCase1")]
        [HttpPost]
        public HttpResponseMessage SoftDisconnectionCase1([FromBody] RecAllocDefltrs READ)//Added  By Babalu Kumar 24122020
        {
            DT_Data PO = new DT_Data();
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                var authentication = new Authentication();
                authentication.Validate();
                if (READ.strKeyParam == PublicAccessTokenKey)
                {
                    List<RecAllocDefltrs> _RAD = new List<RecAllocDefltrs>();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT A.COMPANY, A.CA_NUMBER, A.METER_NO, A.CRN_NUMBER, A.CYCLE_NO, A.ACCOUNT_CLASS, A.DIVISION, A.NAME, A.TELEPHONE_NO, A.HOUSE_NUMBER, A.STREET1, A.STREET2,";
                        sql += "A.STREET3, A.POLE_NO, A.POSTAL_CODE, A.STREET, A.SEQUENCE_NUMBER, A.RATE_CATEGORY, A.BILL_STATUS, A.DISHONOR_FLAG,";
                        //sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (A.LST_PYMT_DT || ' (' || TRUNC(TRUNC(MONTHS_BETWEEN (TRUNC(SYSDATE),TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))/12) || ' Years ' ||";
                        //sql += " MOD(TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'))), 12) || ' Months ' ||";
                        //sql += " ROUND( TRUNC(SYSDATE) - ADD_MONTHS(( TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')), TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE), TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy')))),0) || ' Days)') END) LST_PYMT_DT,";
                        sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (a.LST_PYMT_DT || ' (Pending Since ' || (trunc(sysdate) - trunc(a.LST_PYMT_DT)) || 'Days)') END) LST_PYMT_DT,";
                        sql += "A.DUE_DATE, A.CONTRACT, A.MOVE_IN_DATE, A.CURRENT_DEMAND, A.LAST_PAYMENT_AMT, A.SANCTION_LOAD, A.DEFAULT_AMT, A.LPSC_AMOUNT, A.PRINCIPAL_VALUE, A.CONSUMER_STATUS,";
                        sql += "A.SOFT_DISCONNECTION_DT, A.RUNNING_BAL_AS_ON_DT, A.CONNECTION_DT, A.INITIAL_BUCKET, A.NEW_OLD_FLAG, A.UPLOAD_CYCLE, A.UPDATION_ID, A.INITIAL_DEFAULT_AMT,";
                        sql += "A.PAYMNT_AMT, A.PAYMNT_ENTRY_DT, A.PAYMENT_MODE,B.ASSIGN_USER_CODE ASSIGNED_FE_ID, (SELECT UNIQUE C.FE_NAME_DISP FROM recovery.DEFLTR_AGENCY_FE_MST C WHERE B.ASSIGN_USER_CODE = C.FE_ID) ASSIGNED_FE_NAME";
                        sql += " FROM recovery.DEFLTR_LIST_MAIN A, recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1 AND SOFT_DISCONNECTION_DT!='#'";
                        if (READ._sCategory != "")
                        {
                            if (READ._sCategory == "CAT0002")
                                sql += " AND A.DISHONOR_FLAG = 'Y'";
                            else if (READ._sCategory == "CAT0003")
                                sql += " AND (A.LAST_PAYMENT_AMT = 0 OR LST_PYMT_DT IS NULL)";
                            if (READ._sCategory == "CAT0004")
                                sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT)>180";
                            //sql += " AND FLOOR(TO_DATE(SYSDATE) - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) > 180";
                        }
                        if (READ._sAmtBktID != "")
                            sql += " AND A.DEFAULT_AMT BETWEEN (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "') AND (SELECT UNIQUE RANGE_TO FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "')";

                        if (READ._sAgeBktID != "")
                        {
                            //sql += " AND FLOOR(TO_DATE('01/' ||  SUBSTR(A.UPDATION_ID,5,2) || '/' || SUBSTR(A.UPDATION_ID,0,4),'dd/MM/yyyy') - NVL(TO_DATE(A.LST_PYMT_DT,'dd/MM/yyyy'),TO_DATE(SUBSTR(A.CONNECTION_DT,0,10),'dd/MM/yyyy'))) BETWEEN";
                            sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT) BETWEEN";
                            sql += " (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "') AND (SELECT UNIQUE RANGE_TO FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "')";
                        }
                        if (READ._sAccountClass != "")
                            sql += " AND A.ACCOUNT_CLASS = '" + READ._sAccountClass + "'";
                        if (READ._sDishonorFlag == "Y")
                            sql += " AND A.DISHONOR_FLAG = 'X'";
                        else if (READ._sDishonorFlag == "N")
                            // sql += " AND A.DISHONOR_FLAG != 'X'";
                            sql += " AND (A.DISHONOR_FLAG is null OR A.DISHONOR_FLAG = 'Y')";
                        //sql += " AND A.UPDATION_ID = (SELECT UNIQUE TO_CHAR(MAX(TO_DATE(UPDATION_ID || '01','YYYYMMDD')),'yyyyMM') FROM recovery.DEFLTR_LIST_MAIN)";
                        sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                        sql += "  AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC') ";
                        if (READ._sFEID != "")
                            sql += " AND UPPER(B.ASSIGN_USER_CODE) = UPPER('" + READ._sFEID + "')";
                        sql += " AND A.CA_NUMBER = B.CA_NUMBER(+) AND A.UPDATION_ID = B.UPDATION_ID_DTLS(+) AND TAB_FLAG = 'N' AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS(+) ORDER BY SEQUENCE_NUMBER";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RecAllocDefltrs sD_ats = new RecAllocDefltrs();
                                sD_ats._sCategory = READ._sCategory;
                                sD_ats._sAmtBktID = READ._sAmtBktID;
                                sD_ats._sAgeBktID = READ._sAgeBktID;
                                sD_ats._sAccountClass = READ._sAccountClass;
                                sD_ats._sDishonorFlag = READ._sDishonorFlag;
                                sD_ats._sFEID = READ._sFEID;
                                sD_ats.COMPANY = dt.Rows[i]["COMPANY"].ToString();
                                sD_ats.CA_NUMBER = dt.Rows[i]["CA_NUMBER"].ToString();
                                sD_ats.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                sD_ats.CRN_NUMBER = dt.Rows[i]["CRN_NUMBER"].ToString();
                                sD_ats.CYCLE_NO = dt.Rows[i]["CYCLE_NO"].ToString();
                                sD_ats.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                sD_ats.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                sD_ats.NAME = dt.Rows[i]["NAME"].ToString();
                                sD_ats.TELEPHONE_NO = dt.Rows[i]["TELEPHONE_NO"].ToString();
                                sD_ats.HOUSE_NUMBER = dt.Rows[i]["HOUSE_NUMBER"].ToString();
                                sD_ats.STREET1 = dt.Rows[i]["STREET1"].ToString();
                                sD_ats.STREET2 = dt.Rows[i]["STREET2"].ToString();
                                sD_ats.STREET3 = dt.Rows[i]["STREET3"].ToString();
                                sD_ats.POLE_NO = dt.Rows[i]["POLE_NO"].ToString();
                                sD_ats.POSTAL_CODE = dt.Rows[i]["POSTAL_CODE"].ToString();
                                sD_ats.STREET = dt.Rows[i]["STREET"].ToString();
                                sD_ats.SEQUENCE_NUMBER = dt.Rows[i]["SEQUENCE_NUMBER"].ToString();
                                sD_ats.RATE_CATEGORY = dt.Rows[i]["RATE_CATEGORY"].ToString();
                                sD_ats.BILL_STATUS = dt.Rows[i]["BILL_STATUS"].ToString();
                                sD_ats.DISHONOR_FLAG = dt.Rows[i]["DISHONOR_FLAG"].ToString();
                                sD_ats.LAST_PAYMENT_DT = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                //if (dt.Rows[i]["LST_PYMT_DT"].ToString() != null && dt.Rows[i]["LST_PYMT_DT"].ToString() != "")
                                //{
                                //    string str = sD_ats.LAST_PAYMENT_DT;//Added By Babalu Kumar
                                //    if (str != null)
                                //    {
                                //        string[] strarray = str.Split('/');
                                //        string sdate = strarray[0].ToString();
                                //        string smonth = strarray[1].ToString();
                                //        string syear = strarray[2].ToString();
                                //        if (Convert.ToInt32(smonth) > 12)
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = smonth + "/" + sdate + "/" + syear;
                                //        }
                                //        else
                                //        {
                                //            sD_ats.LAST_PAYMENT_DT = sdate + "/" + smonth + "/" + syear;
                                //        }
                                //    }
                                //}
                                sD_ats.DUE_DATE = dt.Rows[i]["DUE_DATE"].ToString();
                                sD_ats.CONTRACT = dt.Rows[i]["CONTRACT"].ToString();
                                sD_ats.MOVE_IN_DATE = dt.Rows[i]["MOVE_IN_DATE"].ToString();
                                sD_ats.CURRENT_DEMAND = dt.Rows[i]["CURRENT_DEMAND"].ToString();
                                sD_ats.LAST_PAYMENT_AMT = dt.Rows[i]["LAST_PAYMENT_AMT"].ToString();
                                sD_ats.SANCTION_LOAD = dt.Rows[i]["SANCTION_LOAD"].ToString();
                                sD_ats.DEFAULT_AMT = dt.Rows[i]["DEFAULT_AMT"].ToString();
                                sD_ats.LPSC_AMOUNT = dt.Rows[i]["LPSC_AMOUNT"].ToString();
                                sD_ats.PRINCIPAL_VALUE = dt.Rows[i]["PRINCIPAL_VALUE"].ToString();
                                sD_ats.CONSUMER_STATUS = dt.Rows[i]["CONSUMER_STATUS"].ToString();
                                var date = DateTime.ParseExact(dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InstalledUICulture);
                                sD_ats.SOFT_DISCONNECTION_DT = date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InstalledUICulture);
                                // sD_ats.SOFT_DISCONNECTION_DT = dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString();
                                sD_ats.RUNNING_BAL_AS_ON_DT = dt.Rows[i]["RUNNING_BAL_AS_ON_DT"].ToString();
                                sD_ats.CONNECTION_DT = dt.Rows[i]["CONNECTION_DT"].ToString();
                                sD_ats.INITIAL_BUCKET = dt.Rows[i]["INITIAL_BUCKET"].ToString();
                                sD_ats.NEW_OLD_FLAG = dt.Rows[i]["NEW_OLD_FLAG"].ToString();
                                sD_ats.UPLOAD_CYCLE = dt.Rows[i]["UPLOAD_CYCLE"].ToString();
                                sD_ats.UPDATION_ID = dt.Rows[i]["UPDATION_ID"].ToString();
                                sD_ats.INITIAL_DEFAULT_AMT = dt.Rows[i]["INITIAL_DEFAULT_AMT"].ToString();
                                sD_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                sD_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                sD_ats.PAYMENT_MODE = dt.Rows[i]["PAYMENT_MODE"].ToString();
                                sD_ats.ASSIGNED_FE_ID = dt.Rows[i]["ASSIGNED_FE_ID"].ToString();
                                sD_ats.ASSIGNED_FE_NAME = dt.Rows[i]["ASSIGNED_FE_NAME"].ToString();
                                _RAD.Add(sD_ats);
                                response.Data = _RAD;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [Route("API/Recovery/Get_MeterDetails")]
        [HttpPost]
        public HttpResponseMessage Get_MeterDetails([FromBody] MeterDetails meter)//Added  By Babalu Kumar 24122020
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                if (meter.strKeyParam == PublicAccessTokenKey)
                {
                    List<MeterDetails> _login = new List<MeterDetails>();
                    string CONS_REF = string.Empty;
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable("TEST");
                    DataRow row = dt.NewRow();
                    dt.Columns.Add("CONS_REF", typeof(string));
                    dt.Rows.Add(row);
                    LIVE.WebService isu = new LIVE.WebService();
                    ds = isu.Z_BAPI_CMS_ISU_CA_DISPLAY("", "0000000000" + meter._sMeter, "", "", "", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dt.Rows[0]["CONS_REF"] = ds.Tables[0].Rows[0]["Ca_Number"].ToString().Substring(2, 10);
                        MeterDetails obhmtr = new MeterDetails();
                        obhmtr._sCA = dt.Rows[0]["CONS_REF"].ToString();
                        _login.Add(obhmtr);
                        response.Data = _login;
                        response.Status = "Success";
                        response.Message = "Data retrieved";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.Status = "No Data";
                        response.Message = "No Data Found";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            return Msg;
        }

        [Route("API/Recovery/Followup_Date")]
        [HttpPost]
        public HttpResponseMessage Followup_Date([FromBody] FollowupDate READ) //Added  By Babalu Kumar 10022021 PCN No. 2021021003
        {
            DT_Data PO = new DT_Data();
            List<FollowupDate> _RAD = new List<FollowupDate>();
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                if (READ.strKeyParam == PublicAccessTokenKey)
                {
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT A.COMPANY, A.CA_NUMBER, A.METER_NO, A.CRN_NUMBER, A.CYCLE_NO, A.ACCOUNT_CLASS, A.DIVISION, A.NAME, A.TELEPHONE_NO, A.HOUSE_NUMBER, A.STREET1, A.STREET2,";
                        sql += "A.STREET3, A.POLE_NO, A.POSTAL_CODE, A.STREET, A.SEQUENCE_NUMBER, A.RATE_CATEGORY, A.BILL_STATUS, A.DISHONOR_FLAG,";
                        sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (a.LST_PYMT_DT || ' (Pending Since ' || (trunc(sysdate) - trunc(a.LST_PYMT_DT)) || 'Days)') END) LST_PYMT_DT,";
                        sql += "A.DUE_DATE, A.CONTRACT, A.MOVE_IN_DATE, A.CURRENT_DEMAND, A.LAST_PAYMENT_AMT, A.SANCTION_LOAD, A.DEFAULT_AMT, A.LPSC_AMOUNT, A.PRINCIPAL_VALUE, A.CONSUMER_STATUS,";
                        sql += "A.SOFT_DISCONNECTION_DT, A.RUNNING_BAL_AS_ON_DT, A.CONNECTION_DT, A.INITIAL_BUCKET, A.NEW_OLD_FLAG, A.UPLOAD_CYCLE, A.UPDATION_ID, A.INITIAL_DEFAULT_AMT,";
                        sql += "A.PAYMNT_AMT, A.PAYMNT_ENTRY_DT, A.PAYMENT_MODE,B.ASSIGN_USER_CODE ASSIGNED_FE_ID, (SELECT UNIQUE C.FE_NAME_DISP FROM recovery.DEFLTR_AGENCY_FE_MST C WHERE B.ASSIGN_USER_CODE = C.FE_ID) ASSIGNED_FE_NAME";
                        sql += " FROM recovery.DEFLTR_LIST_MAIN A, recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1 AND TRUNC(B.FOLLOWUP_DATE)=TO_DATE('" + READ._sDate + "','dd-Mon-yyyy')";
                        if (READ._sCategory != "")
                        {
                            if (READ._sCategory == "CAT0002")
                                sql += " AND A.DISHONOR_FLAG = 'Y'";
                            else if (READ._sCategory == "CAT0003")
                                sql += " AND (A.LAST_PAYMENT_AMT = 0 OR LST_PYMT_DT IS NULL)";
                            if (READ._sCategory == "CAT0004")
                                sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT)>180";
                        }
                        if (READ._sAmtBktID != "")
                            sql += " AND A.DEFAULT_AMT BETWEEN (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "') AND (SELECT UNIQUE RANGE_TO FROM RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "')";

                        if (READ._sAgeBktID != "")
                        {
                            sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT) BETWEEN";
                            sql += " (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "') AND (SELECT UNIQUE RANGE_TO FROM RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "')";
                        }
                        if (READ._sAccountClass != "")
                            sql += " AND A.ACCOUNT_CLASS = '" + READ._sAccountClass + "'";
                        if (READ._sDishonorFlag == "Y")
                            sql += " AND A.DISHONOR_FLAG = 'X'";
                        else if (READ._sDishonorFlag == "N")
                            sql += " AND A.DISHONOR_FLAG != 'X'";
                        sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                        sql += "  AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC') ";
                        if (READ._sFEID != "")
                            sql += " AND UPPER(B.ASSIGN_USER_CODE) = UPPER('" + READ._sFEID + "')";
                        sql += " AND A.CA_NUMBER = B.CA_NUMBER(+) AND A.UPDATION_ID = B.UPDATION_ID_DTLS(+) AND TAB_FLAG='Y' AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS(+)";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                FollowupDate sD_ats = new FollowupDate();
                                sD_ats._sCategory = READ._sCategory;
                                sD_ats._sAmtBktID = READ._sAmtBktID;
                                sD_ats._sAgeBktID = READ._sAgeBktID;
                                sD_ats._sAccountClass = READ._sAccountClass;
                                sD_ats._sDishonorFlag = READ._sDishonorFlag;
                                sD_ats._sFEID = READ._sFEID;
                                sD_ats.COMPANY = dt.Rows[i]["COMPANY"].ToString();
                                sD_ats.CA_NUMBER = dt.Rows[i]["CA_NUMBER"].ToString();
                                sD_ats.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                sD_ats.CRN_NUMBER = dt.Rows[i]["CRN_NUMBER"].ToString();
                                sD_ats.CYCLE_NO = dt.Rows[i]["CYCLE_NO"].ToString();
                                sD_ats.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                sD_ats.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                sD_ats.NAME = dt.Rows[i]["NAME"].ToString();
                                sD_ats.TELEPHONE_NO = dt.Rows[i]["TELEPHONE_NO"].ToString();
                                sD_ats.HOUSE_NUMBER = dt.Rows[i]["HOUSE_NUMBER"].ToString();
                                sD_ats.STREET1 = dt.Rows[i]["STREET1"].ToString();
                                sD_ats.STREET2 = dt.Rows[i]["STREET2"].ToString();
                                sD_ats.STREET3 = dt.Rows[i]["STREET3"].ToString();
                                sD_ats.POLE_NO = dt.Rows[i]["POLE_NO"].ToString();
                                sD_ats.POSTAL_CODE = dt.Rows[i]["POSTAL_CODE"].ToString();
                                sD_ats.STREET = dt.Rows[i]["STREET"].ToString();
                                sD_ats.SEQUENCE_NUMBER = dt.Rows[i]["SEQUENCE_NUMBER"].ToString();
                                sD_ats.RATE_CATEGORY = dt.Rows[i]["RATE_CATEGORY"].ToString();
                                sD_ats.BILL_STATUS = dt.Rows[i]["BILL_STATUS"].ToString();
                                sD_ats.DISHONOR_FLAG = dt.Rows[i]["DISHONOR_FLAG"].ToString();
                                sD_ats.LAST_PAYMENT_DT = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                sD_ats.DUE_DATE = dt.Rows[i]["DUE_DATE"].ToString();
                                sD_ats.CONTRACT = dt.Rows[i]["CONTRACT"].ToString();
                                sD_ats.MOVE_IN_DATE = dt.Rows[i]["MOVE_IN_DATE"].ToString();
                                sD_ats.CURRENT_DEMAND = dt.Rows[i]["CURRENT_DEMAND"].ToString();
                                sD_ats.LAST_PAYMENT_AMT = dt.Rows[i]["LAST_PAYMENT_AMT"].ToString();
                                sD_ats.SANCTION_LOAD = dt.Rows[i]["SANCTION_LOAD"].ToString();
                                sD_ats.DEFAULT_AMT = dt.Rows[i]["DEFAULT_AMT"].ToString();
                                sD_ats.LPSC_AMOUNT = dt.Rows[i]["LPSC_AMOUNT"].ToString();
                                sD_ats.PRINCIPAL_VALUE = dt.Rows[i]["PRINCIPAL_VALUE"].ToString();
                                sD_ats.CONSUMER_STATUS = dt.Rows[i]["CONSUMER_STATUS"].ToString();
                                sD_ats.SOFT_DISCONNECTION_DT = dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString();
                                sD_ats.RUNNING_BAL_AS_ON_DT = dt.Rows[i]["RUNNING_BAL_AS_ON_DT"].ToString();
                                sD_ats.CONNECTION_DT = dt.Rows[i]["CONNECTION_DT"].ToString();
                                sD_ats.INITIAL_BUCKET = dt.Rows[i]["INITIAL_BUCKET"].ToString();
                                sD_ats.NEW_OLD_FLAG = dt.Rows[i]["NEW_OLD_FLAG"].ToString();
                                sD_ats.UPLOAD_CYCLE = dt.Rows[i]["UPLOAD_CYCLE"].ToString();
                                sD_ats.UPDATION_ID = dt.Rows[i]["UPDATION_ID"].ToString();
                                sD_ats.INITIAL_DEFAULT_AMT = dt.Rows[i]["INITIAL_DEFAULT_AMT"].ToString();
                                sD_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                sD_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                sD_ats.PAYMENT_MODE = dt.Rows[i]["PAYMENT_MODE"].ToString();
                                sD_ats.ASSIGNED_FE_ID = dt.Rows[i]["ASSIGNED_FE_ID"].ToString();
                                sD_ats.ASSIGNED_FE_NAME = dt.Rows[i]["ASSIGNED_FE_NAME"].ToString();
                                string sqlnew = "select Directive from recovery.DEFLTER_Acceptence_MST where Active='Y' and UPPER(RATE_CATEGORY)=UPPER('" + sD_ats.RATE_CATEGORY + "')";
                                DataTable dt1 = new DataTable();
                                cmd = new OleDbCommand(sqlnew, con);
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                adp = new OleDbDataAdapter(cmd);
                                adp.Fill(dt1);
                                if (dt1.Rows.Count > 0)
                                {
                                    sD_ats.DIRECTIVE = dt1.Rows[0]["Directive"].ToString();
                                }

                                _RAD.Add(sD_ats);
                                response.Data = _RAD;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);

                }
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);

            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [Route("API/Recovery/Followup_Date1")]
        [HttpPost]
        public HttpResponseMessage Followup_Date1([FromBody] FollowupDate READ)
        {
            DT_Data PO = new DT_Data();
            List<FollowupDate> _RAD = new List<FollowupDate>();
            DataTable dt = new DataTable();
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                if (READ.strKeyParam == PublicAccessTokenKey)
                {
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT A.COMPANY, A.CA_NUMBER, A.METER_NO, A.CRN_NUMBER, A.CYCLE_NO, A.ACCOUNT_CLASS, A.DIVISION, A.NAME, A.TELEPHONE_NO, A.HOUSE_NUMBER, A.STREET1, A.STREET2,";
                        sql += "A.STREET3, A.POLE_NO, A.POSTAL_CODE, A.STREET, A.SEQUENCE_NUMBER, A.RATE_CATEGORY, A.BILL_STATUS, A.DISHONOR_FLAG,";
                        sql += "(CASE WHEN A.LST_PYMT_DT IS NULL THEN '' ELSE (a.LST_PYMT_DT || ' (Pending Since ' || (trunc(sysdate) - trunc(a.LST_PYMT_DT)) || 'Days)') END) LST_PYMT_DT,";
                        sql += "A.DUE_DATE, A.CONTRACT, A.MOVE_IN_DATE, A.CURRENT_DEMAND, A.LAST_PAYMENT_AMT, A.SANCTION_LOAD, A.DEFAULT_AMT, A.LPSC_AMOUNT, A.PRINCIPAL_VALUE, A.CONSUMER_STATUS,";
                        sql += "A.SOFT_DISCONNECTION_DT, A.RUNNING_BAL_AS_ON_DT, A.CONNECTION_DT, A.INITIAL_BUCKET, A.NEW_OLD_FLAG, A.UPLOAD_CYCLE, A.UPDATION_ID, A.INITIAL_DEFAULT_AMT,";
                        sql += "A.PAYMNT_AMT, A.PAYMNT_ENTRY_DT, A.PAYMENT_MODE,B.ASSIGN_USER_CODE ASSIGNED_FE_ID, (SELECT UNIQUE C.FE_NAME_DISP FROM recovery.DEFLTR_AGENCY_FE_MST C WHERE B.ASSIGN_USER_CODE = C.FE_ID) ASSIGNED_FE_NAME";
                        sql += " FROM recovery.DEFLTR_LIST_MAIN A, recovery.DEFLTR_CA_ASSIGN_LINK_DTLS B WHERE 1=1 AND TRUNC(B.FOLLOWUP_DATE)=TO_DATE('" + READ._sDate + "','dd-Mon-yyyy')";
                        if (READ._sCategory != "")
                        {
                            if (READ._sCategory == "CAT0002")
                                sql += " AND A.DISHONOR_FLAG = 'Y'";
                            else if (READ._sCategory == "CAT0003")
                                sql += " AND (A.LAST_PAYMENT_AMT = 0 OR LST_PYMT_DT IS NULL)";
                            if (READ._sCategory == "CAT0004")
                                sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT)>180";
                        }
                        if (READ._sAmtBktID != "")
                            sql += " AND A.DEFAULT_AMT BETWEEN (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "') AND (SELECT UNIQUE RANGE_TO FROM RECAPP_AMTBKT_MST WHERE AMTBKTID = '" + READ._sAmtBktID + "')";

                        if (READ._sAgeBktID != "")
                        {
                            sql += " AND trunc(sysdate)-trunc(LST_PYMT_DT) BETWEEN";
                            sql += " (SELECT UNIQUE RANGE_FROM FROM recovery.RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "') AND (SELECT UNIQUE RANGE_TO FROM RECAPP_AGIBKT_MST WHERE AGIBKTID = '" + READ._sAgeBktID + "')";
                        }
                        if (READ._sAccountClass != "")
                            sql += " AND A.ACCOUNT_CLASS = '" + READ._sAccountClass + "'";
                        if (READ._sDishonorFlag == "Y")
                            sql += " AND A.DISHONOR_FLAG = 'X'";
                        else if (READ._sDishonorFlag == "N")
                            sql += " AND A.DISHONOR_FLAG != 'X'";
                        sql += " AND A.UPDATION_ID = (SELECT MAX(UPDATION_ID) FROM recovery.DEFLTR_LIST_MAIN)";//Added by Babalu Kumar 20102020 for slow response
                        sql += "  AND A.INITIAL_BUCKET IN ('DIV','CC2D','MLCC') ";
                        if (READ._sFEID != "")
                            sql += " AND UPPER(B.ASSIGN_USER_CODE) = UPPER('" + READ._sFEID + "')";
                        sql += " AND A.CA_NUMBER = B.CA_NUMBER(+) AND A.UPDATION_ID = B.UPDATION_ID_DTLS(+) AND TAB_FLAG='Y' AND A.INITIAL_BUCKET = B.INITIAL_BUCKET_DTLS(+)";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                FollowupDate sD_ats = new FollowupDate();
                                sD_ats._sCategory = READ._sCategory;
                                sD_ats._sAmtBktID = READ._sAmtBktID;
                                sD_ats._sAgeBktID = READ._sAgeBktID;
                                sD_ats._sAccountClass = READ._sAccountClass;
                                sD_ats._sDishonorFlag = READ._sDishonorFlag;
                                sD_ats._sFEID = READ._sFEID;
                                sD_ats.COMPANY = dt.Rows[i]["COMPANY"].ToString();
                                sD_ats.CA_NUMBER = dt.Rows[i]["CA_NUMBER"].ToString();
                                sD_ats.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                sD_ats.CRN_NUMBER = dt.Rows[i]["CRN_NUMBER"].ToString();
                                sD_ats.CYCLE_NO = dt.Rows[i]["CYCLE_NO"].ToString();
                                sD_ats.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                sD_ats.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                sD_ats.NAME = dt.Rows[i]["NAME"].ToString();
                                sD_ats.TELEPHONE_NO = dt.Rows[i]["TELEPHONE_NO"].ToString();
                                sD_ats.HOUSE_NUMBER = dt.Rows[i]["HOUSE_NUMBER"].ToString();
                                sD_ats.STREET1 = dt.Rows[i]["STREET1"].ToString();
                                sD_ats.STREET2 = dt.Rows[i]["STREET2"].ToString();
                                sD_ats.STREET3 = dt.Rows[i]["STREET3"].ToString();
                                sD_ats.POLE_NO = dt.Rows[i]["POLE_NO"].ToString();
                                sD_ats.POSTAL_CODE = dt.Rows[i]["POSTAL_CODE"].ToString();
                                sD_ats.STREET = dt.Rows[i]["STREET"].ToString();
                                sD_ats.SEQUENCE_NUMBER = dt.Rows[i]["SEQUENCE_NUMBER"].ToString();
                                sD_ats.RATE_CATEGORY = dt.Rows[i]["RATE_CATEGORY"].ToString();
                                sD_ats.BILL_STATUS = dt.Rows[i]["BILL_STATUS"].ToString();
                                sD_ats.DISHONOR_FLAG = dt.Rows[i]["DISHONOR_FLAG"].ToString();
                                sD_ats.LAST_PAYMENT_DT = dt.Rows[i]["LST_PYMT_DT"].ToString();
                                sD_ats.DUE_DATE = dt.Rows[i]["DUE_DATE"].ToString();
                                sD_ats.CONTRACT = dt.Rows[i]["CONTRACT"].ToString();
                                sD_ats.MOVE_IN_DATE = dt.Rows[i]["MOVE_IN_DATE"].ToString();
                                sD_ats.CURRENT_DEMAND = dt.Rows[i]["CURRENT_DEMAND"].ToString();
                                sD_ats.LAST_PAYMENT_AMT = dt.Rows[i]["LAST_PAYMENT_AMT"].ToString();
                                sD_ats.SANCTION_LOAD = dt.Rows[i]["SANCTION_LOAD"].ToString();
                                sD_ats.DEFAULT_AMT = dt.Rows[i]["DEFAULT_AMT"].ToString();
                                sD_ats.LPSC_AMOUNT = dt.Rows[i]["LPSC_AMOUNT"].ToString();
                                sD_ats.PRINCIPAL_VALUE = dt.Rows[i]["PRINCIPAL_VALUE"].ToString();
                                sD_ats.CONSUMER_STATUS = dt.Rows[i]["CONSUMER_STATUS"].ToString();
                                sD_ats.SOFT_DISCONNECTION_DT = dt.Rows[i]["SOFT_DISCONNECTION_DT"].ToString();
                                sD_ats.RUNNING_BAL_AS_ON_DT = dt.Rows[i]["RUNNING_BAL_AS_ON_DT"].ToString();
                                sD_ats.CONNECTION_DT = dt.Rows[i]["CONNECTION_DT"].ToString();
                                sD_ats.INITIAL_BUCKET = dt.Rows[i]["INITIAL_BUCKET"].ToString();
                                sD_ats.NEW_OLD_FLAG = dt.Rows[i]["NEW_OLD_FLAG"].ToString();
                                sD_ats.UPLOAD_CYCLE = dt.Rows[i]["UPLOAD_CYCLE"].ToString();
                                sD_ats.UPDATION_ID = dt.Rows[i]["UPDATION_ID"].ToString();
                                sD_ats.INITIAL_DEFAULT_AMT = dt.Rows[i]["INITIAL_DEFAULT_AMT"].ToString();
                                sD_ats.PAYMNT_AMT = dt.Rows[i]["PAYMNT_AMT"].ToString();
                                sD_ats.PAYMNT_ENTRY_DT = dt.Rows[i]["PAYMNT_ENTRY_DT"].ToString();
                                sD_ats.PAYMENT_MODE = dt.Rows[i]["PAYMENT_MODE"].ToString();
                                sD_ats.ASSIGNED_FE_ID = dt.Rows[i]["ASSIGNED_FE_ID"].ToString();
                                sD_ats.ASSIGNED_FE_NAME = dt.Rows[i]["ASSIGNED_FE_NAME"].ToString();
                                _RAD.Add(sD_ats);
                                response.Data = _RAD;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);

                }
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);

            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [Route("API/Recovery/Action_Details")]
        [HttpPost]
        public HttpResponseMessage Action_Details([FromBody] Action_Details objaction) //Added  By Babalu Kumar 10022021 PCN No. 2021021003
        {
            string sql = string.Empty;
            string strConnection = NDS.con();
            try
            {
                DataTable dt = new DataTable();
                if (objaction.strKeyParam == PublicAccessTokenKey)
                {
                    List<Action_Details> actionlist = new List<Action_Details>();
                    using (con = new OleDbConnection(strConnection))
                    {
                        sql = "SELECT CODE,DESCRIPTION FROM recovery.DEFLTR_COR_SYS_TYPE WHERE 1=1  and CORETYPE='RECOVRY_STATUS'";
                        sql += " AND ACTIVE_FLAG = 'Y' and ACCOUNT_CLASS is null   ORDER BY CODE ASC";
                        OleDbCommand cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                Action_Details actiondata = new Action_Details();
                                actiondata.Code = dt.Rows[i]["CODE"].ToString();
                                actiondata.Description = dt.Rows[i]["DESCRIPTION"].ToString();
                                actionlist.Add(actiondata);
                                response.Data = actionlist;
                                response.Status = "Success";
                                response.Message = "Data retrieved";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            response.Status = "No Data";
                            response.Message = "No Data Found";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = "Key doesnot match !";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con!=null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [Route("API/Recovery/PunchAtr1")] //Added  By Babalu Kumar 10022021 PCN No. 2021021003
        [HttpPost]
        public HttpResponseMessage PunchAtr1([FromBody] PunchAtr PAtr)
        {
            string _sMessage = string.Empty;
            bool Result;
            int count = 0;
            DT_Data PO = new DT_Data();
            List<PunchAtr> _RAD = new List<PunchAtr>();
            OleDbConnection con = new OleDbConnection();
            DataTable dt = new DataTable();
            OleDbDataAdapter sda = new OleDbDataAdapter();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            RecoveryBL recoveryBL = new RecoveryBL();
            string sql = string.Empty;
            string strConnection = NDS.con();
            if (PAtr.strKeyParam == PublicAccessTokenKey)
            {
                try
                {

                    PAtr._sFollowDate = PAtr._sFollowDate.Replace("Sept", "Sep");

                    using (con = new OleDbConnection(strConnection))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        System.Web.HttpContext context = System.Web.HttpContext.Current;
                        string _sIPAddr = context.Request.ServerVariables["REMOTE_ADDR"];
                        string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
                        bool res = false;
                        DataTable dtupdate = recoveryBL.ChkUpdatedate4();
                        if (dtupdate.Rows.Count > 0)
                        {
                            if (dtupdate.Rows[0]["DT"].ToString() == dtupdate.Rows[0]["UDT"].ToString())
                            {
                                DataTable dtCheckATR = recoveryBL.ChkRecordExist4(PAtr._sCANumber, PAtr._sFEID, "DEFLTR_ATR");
                                if (dtCheckATR.Rows.Count > 0)
                                {
                                    res = recoveryBL.UpdateTable10(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, _sIPAddr, "DEFLTR_ATR", "", "", "", "", "", "", "", "", "", "", "", "", "");
                                    res = true;
                                }
                                else
                                {
                                    res = recoveryBL.InsertTable7(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, _sIPAddr, "DEFLTR_ATR");
                                    res = true;
                                }
                                if (res == true)
                                {
                                    if (PAtr._sStatus == "05")
                                    {
                                        recoveryBL.InsertSMS9(PAtr._sMobileNo, PAtr._sMessage, PAtr._sCANumber, PAtr._sUpdationID, _sIPAddr);
                                    }
                                    if (PAtr._sStatus == "17")
                                    {
                                        if (PAtr._sFollowDate != "" && PAtr._sFollowDate != null)
                                        {
                                            _sMessage = "Dear Consumer,CA No." + PAtr._sCANumber + ", Rs. " + PAtr.RunningAmt + ", follow up request received for " + PAtr._sFollowDate + " Please pay the bill by follow up date to avoid disconnection. Team BRPL";
                                            Result = recoveryBL.ConsumerSMS1(PAtr._sMobileNo, _sMessage, PAtr._sCANumber, PAtr._sFEID);
                                        }
                                    }

                                    res = recoveryBL.UpdateTable10(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, ipAddress, "DEFLTR_CA_ASSIGN_LINK_DTLS", PAtr._sTabUpdStatus, PAtr._sRemarks,
                                                        PAtr._sImgFlg, PAtr._sImgPath, PAtr._sImgPath1, PAtr._sFollowDate, PAtr._sAltContNo, PAtr._sAltEmailID, PAtr.Amount, PAtr.pdcDate, PAtr.RunningAmt, PAtr.Latitude, PAtr.Longitude);
                                    response.Status = "Success";
                                    response.Message = "1 record updated successfully";
                                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                                    return Msg;
                                    con.Close();
                                }
                                else
                                {
                                    response.Status = "Failed";
                                    response.Message = "Data not updated";
                                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                                    return Msg;
                                }
                            }
                            else
                            {
                                DataTable dtCheckATR = recoveryBL.ChkRecordExist5(PAtr._sCANumber, PAtr._sFEID, "DEFLTR_ATR");
                                if (dtCheckATR.Rows.Count > 0)
                                {
                                    res = recoveryBL.UpdateTable10(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, _sIPAddr, "DEFLTR_ATR", "", "", "", "", "", "", "", "", "", "", "", "", "");
                                    res = true;
                                }
                                else
                                {
                                    res = recoveryBL.InsertTable8(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, _sIPAddr, "DEFLTR_ATR");
                                    res = true;
                                }
                                if (res == true)
                                {
                                    if (PAtr._sStatus == "05")
                                    {
                                        recoveryBL.InsertSMS9(PAtr._sMobileNo, PAtr._sMessage, PAtr._sCANumber, PAtr._sUpdationID, _sIPAddr);
                                    }
                                    if (PAtr._sStatus == "17")
                                    {
                                        if (PAtr._sFollowDate != "" && PAtr._sFollowDate != null)
                                        {
                                            _sMessage = "Dear Consumer,CA No." + PAtr._sCANumber + ", Rs. " + PAtr.RunningAmt + ", follow up request received for " + PAtr._sFollowDate + " Please pay the bill by follow up date to avoid disconnection. Team BRPL";
                                            Result = recoveryBL.ConsumerSMS1(PAtr._sMobileNo, _sMessage, PAtr._sCANumber, PAtr._sFEID);
                                        }
                                    }

                                    res = recoveryBL.UpdateTable11(PAtr._sCANumber, PAtr._sStatus, PAtr._sFEID, ipAddress, "DEFLTR_CA_ASSIGN_LINK_DTLS", PAtr._sTabUpdStatus, PAtr._sRemarks,
                                                        PAtr._sImgFlg, PAtr._sImgPath, PAtr._sImgPath1, PAtr._sFollowDate, PAtr._sAltContNo, PAtr._sAltEmailID, PAtr.Amount, PAtr.pdcDate, PAtr.RunningAmt, PAtr.Latitude, PAtr.Longitude);
                                    response.Status = "Success";
                                    response.Message = "1 record updated successfully";
                                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                                    return Msg;
                                    con.Close();
                                }
                                else
                                {
                                    response.Status = "Failed";
                                    response.Message = "Data not updated";
                                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                                    return Msg;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Msg;
                    throw ex;
                }
                con.Close();
            }
            else
            {
                response.Status = "Failed";
                response.Message = "Key doesnot match !";
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);

            }
            return Msg;
        }
    }
}

