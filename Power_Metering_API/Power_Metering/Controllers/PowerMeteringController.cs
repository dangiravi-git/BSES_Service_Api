using System;
using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Power_Metering.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Formatting;
using System.Data.SqlClient;
using System.Text;
using Power_Metering.BusinessLogicLayer.Security;
using Power_Metering.BusinessLogicLayer;
using Serilog;

namespace Power_Metering.Controllers
{
    public class PowerMeteringController : ApiController
    {
        SqlConnection con1;
        SqlCommand cmd1;
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataAdapter adp;
        Response response = new Response();
        Response2 response2 = new Response2();
        Response1 response1 = new Response1();
        private readonly JwtTokenGenerator jwtTokenGenerator;
        private readonly Authentication authentication ;
        private readonly PowerMeteringBL powerMeteringBL;
        public PowerMeteringController()
        {
            Log.Information($"Init {nameof(PowerMeteringController)}");
            try
            {
                jwtTokenGenerator = new JwtTokenGenerator();
                authentication = new Authentication();
                powerMeteringBL = new PowerMeteringBL();
                con = new OleDbConnection();
                cmd = new OleDbCommand();
                con1 = new SqlConnection();
                adp = new OleDbDataAdapter();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in PowerMeteringController Constractor");
            }
        }

        [HttpPost]
        [Route("api/PowerMetering/auth/login")]
        public IHttpActionResult LoginTest([FromBody] Logindetails login)
        {
            if (login.userID  == "admin" && login.password == "password")
            {
                var token = jwtTokenGenerator.GenerateToken(login.userID, "Admin");
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        [Route("API/PowerMetering/Login")]
        [HttpPost]
        public HttpResponseMessage Login([FromBody] Logindetails login)
        {
             string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
             string connectionString = NDS.con();
            int count = 0;
            Logindetails logins = new Logindetails();
            List<UserDetails> _UserDetails = new List<UserDetails>();

            try
            {
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT USER_ID, USER_NAME FROM MOBINT.PM_LOGIN WHERE USER_ID = '" + login.userID.ToUpper() + "'";
                    sqlQuery += " AND PASSWORD = '" + login.password.ToUpper() + "' AND STATUS = 'Y' AND ROLL_TYPE='INSTALLER'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        UserDetails userdetail = new UserDetails();
                        count = count + 1;
                        userdetail.userID = rdr["USER_ID"].ToString();
                        userdetail.userName = rdr["USER_NAME"].ToString();
                        userdetail.JwtToken = jwtTokenGenerator.GenerateToken(Convert.ToString(rdr["USER_ID"]), "Admin");
                        _UserDetails.Add(userdetail);
                        response.Data = _UserDetails;

                        response.Status = "Success";
                        response.Message = "User is valid";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        return Msg;
                    }
                }
                response.Status = "Failed";
                response.Message = "Userid or password is not valid";
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                return Msg;
            }
            catch (Exception ex)
            {
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        [Route("API/PowerMetering/CaseList")]
        [HttpPost]
        public HttpResponseMessage CaseList([FromBody] Logindetails login)
        {
            int count = 0;
            string sqlQuery = string.Empty; 
            HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            //Logindetails logins = new Logindetails();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                List<CaseLists> lstemployee = new List<CaseLists>();
                using (con = new OleDbConnection(connectionString))
                {
                    //sqlQuery = "SELECT MID.ORDERID,to_char(MID.START_DATE, 'dd/MM/yyyy') START_DATE , to_char(MID.FINISH_DATE, 'dd/MM/yyyy') FINISH_DATE ,MID.AUART,";
                    //sqlQuery += " MID.ILART_ACTIVITY_TYPE,(SELECT MCSM.NAME FROM MCR_COR_SYS_MST MCSM INNER JOIN  MOBINT.MCR_INPUT_DETAILS ON MCSM.PM_ACTIVITY = ILART_ACTIVITY_TYPE AND ROWNUM = 1) ILART_ACTIVITY_NAME,";
                    //sqlQuery += " MID.PLANNER_GROUP,MID.DIVISION,(SELECT DIVISION_NAME FROM MOBINT.MCR_DIVISION MD INNER JOIN  MCR_INPUT_DETAILS MID ON MD.DIST_CD = MID.DIVISION AND ROWNUM = 1) DIVISION_NAME, MID.CA_NO,";
                    //sqlQuery += " MID.NAME,MID.FATHER_NAME,MID.ADDRESS,MID.TEL_NO,SUBSTR(MID.METER_NO,11,18) METER_NO,MID.SANCTIONED_LOAD, MID.CABLE_LENGTH,MID.POLE_NO FROM MCR_INPUT_DETAILS MID INNER JOIN PM_LOGIN MLM  ON";
                    //sqlQuery += " MID.ALLOCATE_TO = MLM.USER_ID WHERE USER_ID = '" + login.userID.ToUpper() + "' AND ACCOUNT_CLASS = 'KCC' AND FLAG = 'PY' AND MLM.USER_ID = '" + login.userID.ToUpper() + "'  and MLM.STATUS = 'Y'";

                    sqlQuery = "SELECT MID.ORDERID,to_char(MID.START_DATE, 'dd/MM/yyyy') START_DATE , to_char(MID.FINISH_DATE, 'dd/MM/yyyy') FINISH_DATE ,MID.AUART,";
                    sqlQuery += " MID.ILART_ACTIVITY_TYPE,MCSM.PM_DESC ILART_ACTIVITY_NAME, MID.PLANNER_GROUP,MID.DIVISION,DIVISION_NAME,MID.CA_NO,MID.NAME,MID.FATHER_NAME,MID.ADDRESS,";
                    sqlQuery += " MID.TEL_NO,METER_NO,MID.SANCTIONED_LOAD, MID.CABLE_LENGTH,MID.POLE_NO  FROM MOBINT.MCR_INPUT_DETAILS MID,";
                    sqlQuery += " MOBINT.PM_LOGIN MLM, MOBINT.MCR_DIVISION MD, mobint.MCR_ORDER_PM_MASTER MCSM  WHERE MID.ALLOCATE_TO = MLM.USER_ID";
                    sqlQuery += " AND MID.DIVISION = MD.DIST_CD  AND MID.ILART_ACTIVITY_TYPE = MCSM.PM_ACTIVTY(+)  ";
                    //sqlQuery += " AND ACCOUNT_CLASS = 'KCC'";
                    sqlQuery += " AND FLAG = 'PY'  AND MID.ALLOCATE_TO= '" + login.userID.ToUpper() + "' AND MLM.STATUS = 'Y' AND COMPANY = 'BRPL'";

                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        count = count + 1;
                        CaseLists caselists = new CaseLists();
                        caselists.orderId = rdr["ORDERID"].ToString();
                        caselists.start_date = rdr["START_DATE"].ToString();
                        caselists.finish_date = rdr["FINISH_DATE"].ToString();
                        caselists.division_name = rdr["DIVISION_NAME"].ToString();
                        caselists.order_type = rdr["AUART"].ToString();
                        caselists.activity_type = rdr["ILART_ACTIVITY_TYPE"].ToString();
                        caselists.activityType_name = rdr["ILART_ACTIVITY_NAME"].ToString();
                        caselists.planner_group = rdr["PLANNER_GROUP"].ToString();
                        caselists.division = rdr["DIVISION"].ToString();
                        caselists.ca_no = rdr["CA_NO"].ToString();
                        caselists.name = rdr["NAME"].ToString();
                        caselists.father_name = rdr["FATHER_NAME"].ToString();
                        caselists.address = rdr["ADDRESS"].ToString();
                        caselists.tel_no = rdr["TEL_NO"].ToString();
                        caselists.meter_no = rdr["METER_NO"].ToString();
                        caselists.sanctioned_load = rdr["SANCTIONED_LOAD"].ToString();
                        caselists.cable_length = rdr["CABLE_LENGTH"].ToString();
                        caselists.pole_no = rdr["POLE_NO"].ToString();
                        caselists.ct_box_number = "";
                        caselists.mf = "";
                        caselists.tariff_category = "";
                        caselists.user_responsible = "";
                        caselists.previous_reading_date = "";
                        caselists.kwh = "";
                        caselists.kw = "";
                        caselists.kvah = "";
                        caselists.kva = "";
                        caselists.other1 = "";
                        caselists.other2 = "";
                        caselists.other3 = "";
                        caselists.other4 = "";
                        lstemployee.Add(caselists);
                        response.Data = lstemployee;
                    }
                    con.Close();
                    if (count > 0)
                    {
                        response.Status = "Success";
                        response.Message = "Userid is valid";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        return Msg;
                    }
                    else
                    {
                        response.Status = "404";
                        response.Message = "No record found";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        return Msg;
                    }
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
                return Msg;
            }
        }

        [Route("API/PowerMetering/CaseListNew")]
        [HttpPost]
        public HttpResponseMessage CaseListNew([FromBody] Logindetails login)
        {
            int count = 0;
            string sqlQuery = string.Empty; 
            HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            //Logindetails logins = new Logindetails();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                List<CaseLists> lstemployee = new List<CaseLists>();
                using (con = new OleDbConnection(connectionString))
                {
                    //sqlQuery = "SELECT MID.ORDERID,to_char(MID.START_DATE, 'dd/MM/yyyy') START_DATE , to_char(MID.FINISH_DATE, 'dd/MM/yyyy') FINISH_DATE ,MID.AUART,";
                    //sqlQuery += " MID.ILART_ACTIVITY_TYPE,(SELECT MCSM.NAME FROM MCR_COR_SYS_MST MCSM INNER JOIN  MOBINT.MCR_INPUT_DETAILS ON MCSM.PM_ACTIVITY = ILART_ACTIVITY_TYPE AND ROWNUM = 1) ILART_ACTIVITY_NAME,";
                    //sqlQuery += " MID.PLANNER_GROUP,MID.DIVISION,(SELECT DIVISION_NAME FROM MOBINT.MCR_DIVISION MD INNER JOIN  MCR_INPUT_DETAILS MID ON MD.DIST_CD = MID.DIVISION AND ROWNUM = 1) DIVISION_NAME, MID.CA_NO,";
                    //sqlQuery += " MID.NAME,MID.FATHER_NAME,MID.ADDRESS,MID.TEL_NO,SUBSTR(MID.METER_NO,11,18) METER_NO,MID.SANCTIONED_LOAD, MID.CABLE_LENGTH,MID.POLE_NO FROM MCR_INPUT_DETAILS MID INNER JOIN PM_LOGIN MLM  ON";
                    //sqlQuery += " MID.ALLOCATE_TO = MLM.USER_ID WHERE USER_ID = '" + login.userID.ToUpper() + "' AND ACCOUNT_CLASS = 'KCC' AND FLAG = 'PY' AND MLM.USER_ID = '" + login.userID.ToUpper() + "'  and MLM.STATUS = 'Y'";

                    sqlQuery = "SELECT MID.ORDERID,to_char(MID.START_DATE, 'dd/MM/yyyy') START_DATE , to_char(MID.FINISH_DATE, 'dd/MM/yyyy') FINISH_DATE ,MID.AUART,";
                    sqlQuery += " MID.ILART_ACTIVITY_TYPE,MCSM.PM_DESC ILART_ACTIVITY_NAME, MID.PLANNER_GROUP,MID.DIVISION,DIVISION_NAME,MID.CA_NO,MID.NAME,MID.FATHER_NAME,MID.ADDRESS,";
                    sqlQuery += " MID.TEL_NO,METER_NO,MID.SANCTIONED_LOAD, MID.CABLE_LENGTH,MID.POLE_NO  FROM MOBINT.MCR_INPUT_DETAILS MID,";
                    sqlQuery += " MOBINT.PM_LOGIN MLM, MOBINT.MCR_DIVISION MD, mobint.MCR_ORDER_PM_MASTER MCSM  WHERE MID.ALLOCATE_TO = MLM.USER_ID";
                    sqlQuery += " AND MID.DIVISION = MD.DIST_CD  AND MID.ILART_ACTIVITY_TYPE = MCSM.PM_ACTIVTY(+)  ";
                    //sqlQuery += " AND ACCOUNT_CLASS = 'KCC'";
                    sqlQuery += " AND FLAG = 'PY'  AND MID.ALLOCATE_TO= '" + login.userID.ToUpper() + "' AND MLM.STATUS = 'Y' AND COMPANY = 'BRPL'";

                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        count = count + 1;
                        CaseLists caselists = new CaseLists();
                        caselists.orderId = rdr["ORDERID"].ToString();
                        caselists.start_date = rdr["START_DATE"].ToString();
                        caselists.finish_date = rdr["FINISH_DATE"].ToString();
                        caselists.division_name = rdr["DIVISION_NAME"].ToString();
                        caselists.order_type = rdr["AUART"].ToString();
                        caselists.activity_type = rdr["ILART_ACTIVITY_TYPE"].ToString();
                        caselists.activityType_name = rdr["ILART_ACTIVITY_NAME"].ToString();
                        caselists.planner_group = rdr["PLANNER_GROUP"].ToString();
                        caselists.division = rdr["DIVISION"].ToString();
                        caselists.ca_no = rdr["CA_NO"].ToString();
                        caselists.name = rdr["NAME"].ToString();
                        caselists.father_name = rdr["FATHER_NAME"].ToString();
                        caselists.address = rdr["ADDRESS"].ToString();
                        caselists.tel_no = rdr["TEL_NO"].ToString();
                        caselists.meter_no = rdr["METER_NO"].ToString();
                        caselists.sanctioned_load = rdr["SANCTIONED_LOAD"].ToString();
                        caselists.cable_length = rdr["CABLE_LENGTH"].ToString();
                        caselists.pole_no = rdr["POLE_NO"].ToString();
                        caselists.ct_box_number = "";
                        caselists.mf = "";
                        caselists.tariff_category = "";
                        caselists.user_responsible = "";
                        caselists.previous_reading_date = "";
                        caselists.kwh = "";
                        caselists.kw = "";
                        caselists.kvah = "";
                        caselists.kva = "";
                        caselists.other1 = "";
                        caselists.other2 = "";
                        caselists.other3 = "";
                        caselists.other4 = "";
                        lstemployee.Add(caselists);
                        response.Data = lstemployee;
                    }
                    con.Close();
                    if (count > 0)
                    {
                        response.Status = "Success";
                        response.Message = "Userid is valid";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        return Msg;
                    }
                    else
                    {
                        response.Status = "404";
                        response.Message = "No record found";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        return Msg;
                    }
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
                return Msg;
            }
        }

        [Route("API/PowerMetering/InsertPMPunchOrder")]
        [HttpPost]
        public HttpResponseMessage InsertPMPunchOrder([FromBody] PMPunchOrder _pdata)
        {
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }

            string sqlQuery = string.Empty;
            HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            using (con = new OleDbConnection(connectionString))
            {
                int count = 0;
                sqlQuery = "select USER_ID, PASSWORD FROM MOBINT.PM_LOGIN WHERE USER_ID='" + _pdata.UserId + "'";
                cmd = new OleDbCommand(sqlQuery, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    count = +1;
                    response.Data = _pdata.UserId;
                    response.Status = "Success";
                    response.Message = "UserID is valid";
                }
                con.Close();
                con.Dispose();
                if (_pdata.mtrPhoto1 != null && _pdata.mtrPhoto1 != "")
                {
                    byte[] _byImage = Convert.FromBase64String(_pdata.mtrPhoto1);
                    _pdata.mtrPhoto1 = powerMeteringBL.byteArrayToImage(_byImage, _pdata.OrderNo + "_1");
                }
                if (_pdata.mtrPhoto2 != null && _pdata.mtrPhoto2 != "")
                {
                    byte[] _byImage = Convert.FromBase64String(_pdata.mtrPhoto2);
                    _pdata.mtrPhoto2 = powerMeteringBL.byteArrayToImage(_byImage, _pdata.OrderNo + "_2");
                }
                if (_pdata.mcrPhoto != null && _pdata.mcrPhoto != "")
                {
                    byte[] _byImage = Convert.FromBase64String(_pdata.mcrPhoto);
                    _pdata.mcrPhoto = powerMeteringBL.byteArrayToImage(_byImage, _pdata.OrderNo + "_3");
                }
                if (_pdata.signaturePhoto != null && _pdata.signaturePhoto != "")
                {
                    byte[] _byImage = Convert.FromBase64String(_pdata.signaturePhoto);
                    _pdata.signaturePhoto = powerMeteringBL.byteArrayToImage(_byImage, _pdata.OrderNo + "_4");
                }
                if (_pdata.gunnyBagPhoto != null && _pdata.gunnyBagPhoto != "")
                {
                    byte[] _byImage = Convert.FromBase64String(_pdata.gunnyBagPhoto);
                    _pdata.gunnyBagPhoto = powerMeteringBL.byteArrayToImage(_byImage, _pdata.OrderNo + "_5");
                }
                if (_pdata.oldMtrPhoto1 != null && _pdata.oldMtrPhoto1 != "")
                {
                    byte[] _byImage = Convert.FromBase64String(_pdata.oldMtrPhoto1);
                    _pdata.oldMtrPhoto1 = powerMeteringBL.byteArrayToImage(_byImage, _pdata.OrderNo + "_6");
                }
                if (_pdata.oldMtrPhoto2 != null && _pdata.oldMtrPhoto2 != "")
                {
                    byte[] _byImage = Convert.FromBase64String(_pdata.oldMtrPhoto2);
                    _pdata.oldMtrPhoto2 = powerMeteringBL.byteArrayToImage(_byImage, _pdata.OrderNo + "_7");
                }
                try
                {
                    if (count > 0)
                    {
                        sqlQuery = "insert into PM_DETAILS (ORDER_NO, NEW_MTRBOX_SRNUM, OLD_MTRBOX_SRNUM, INSTALL_MTRBOX_TYPE, NEW_MTRSRL_NUM, OLD_MTRSRL_NUM, INSTALL_MTR_TYPE, SCAN_SR_NUM, NEW_AMR_SRL_NUM, OLD_AMR_SR_NUM, REMARKS, MTR_PHOTO1,";
                        sqlQuery += " MTR_PHOTO2, MCR_PHOTO, SIGNATURE_PHOTO, GUNNYBAG_PHOTO, CONSUMER_NAME, CONSUMER_CONTACT_NUM, EMP_NAME, EMP_NUMBER, MTRBX_REPLACEMENT_RESION, MTR_REPLACEMENT_RESION, CIRCLE_NAME, DIVISION_NAME, SB_DIVISION_NAME, DEPARTMENT_NAME,";
                        sqlQuery += " MCR_TYPE, MCR_NUMBER, MCR_DATE, MCR_METERING_ACTIVITY, MCR_FIELD_ACTIVITYTYPE, MCR_FIELDSB_ACTIVITYTYPE, MCR_VENDER_NAME, WORKORDER_NUM, READING_CORD, LUG_SUNTITY, PRC_QUANTITY, BUSHING_PROTECTOR_QUAN, BUSBAR_QUAN, ";
                        sqlQuery += " MSEAL_QUAN, SADDLE_CLAMP_QUAN, PIPE_QUAN, GL_ANGLE_QUAN, CU_WIRE_QUAN, NEW_MTRTERMINALSEAL1, NEW_MTRTERMINALSEAL2, NEW_MTRHEADSEAL1, NEW_MTRBOXSEAL1, NEW_MTRBOXSEAL2, NEW_MTRBOXSEAL3, NEW_MTRBOXICSEAL1, NEW_MTRBOXICSEAL2, NEW_MTRBOXICSEAL3,";
                        sqlQuery += " NEW_MTRBOXICSEAL4, NEW_MTRBOXOGSEAL1, NEW_MTRBOXOGSEAL2, NEW_MTRBOXOGSEAL3, NEW_MTRBOXOGSEAL4, NEW_BSBARSEAL1, NEW_BSBARSEAL2, NEW_HOLOGRAMTERMINALSEAL1, NEW_HOLOGRAMMTRBOXSEAL2, NEW_HOLOGRAMICSEAL3, NEW_HOLOGRAMICSEAL4, NEW_HOLOGRAMOGSEAL5,";
                        sqlQuery += " NEW_HOLOGRAMOGSEAL6, OLD_MTRTERMINALSEAL1, OLD_MTRTERMINALSEAL2, OLD_MTRHEADSEAL1, OLD_MTRBOXSEAL1, OLD_MTRBOXSEAL2, OLD_MTRBOXSEAL3, OLD_MTRBOXICSEAL1, OLD_MTRBOXICSEAL2, OLD_MTRBOXICSEAL3, ";
                        sqlQuery += " OLD_MTRBOXICSEAL4, OLD_MTRBOXOGSEAL1, OLD_MTRBOXOGSEAL2, OLD_MTRBOXOGSEAL3, OLD_MTRBOXOGSEAL4, OLD_BSBARSEAL1, OLD_BSBARSEAL2, OLD_HOLOGRAMTERMINALSEAL1, OLD_HOLOGRAMMTRBOXSEAL2, OLD_HOLOGRAMICSEAL3, OLD_HOLOGRAMICSEAL4, OLD_HOLOGRAMOGSEAL5,";
                        sqlQuery += " OLD_HOLOGRAMOGSEAL6, ICCBLTYPE, ICCBLESIZE, ICCBLLAYINGTYPE, ICCBLDRUMNUM, ICCBLSRLNUMFROM, ICCBLSRLNUMTO, ICTOTALCBLLENGTH, FEEDINGPOINTTYPE, FEEDINGPOINTSRLNUM, CBLUSEDINOG, OGCBLTYPE, OGCBLESIZE, OGCBLLAYINGTYPE, OGCBLDRUMNUM, OGCBLSRLNUMFROM,";
                        sqlQuery += " OGCBLSRLNUMTO, OGTOTALCBLLENGTH, REM_CBLSIZE, REM_CBLLENGTH, EXT_CBLSIZE, EXT_FEEDINGPOINTTYPE, EXT_FEEDINGSRNUM, NEW_AMRTYPE, NEW_AMRMANUFACTURER, NEW_AMRSIMID, NEW_AMRSIMNUM, NEW_AMROTHER,";
                        sqlQuery += " EXT_AMRTYPE, EXT_AMRMANUFACTURER, EXT_AMRSIMID, EXT_AMRSIMNUM, EXT_AMROTHER, REM_AMRTYPE, REM_AMRMANUFACTURER, REM_AMRSIMID, REM_AMRSIMNUM, REM_AMROTHER, ISGUNNYBAGPREPARED, GUNNYBAGNUM, GUNNYBAGCOLOR, GUNNYBAGSEALNUM, LABNOTICENUM, LABTESTINGDATE, ";
                        sqlQuery += " USERTYPE, GUNNYREASON, MTRIMPORTREADING, MTRINSTANIOUSVOLT, MTRINSTANIOUSCURNT, MTRINSTANIOUSPWRFACTOR, ISEXPORTPARAM, MTREXPORTREADING, ISMTRTESTINGCARRIEDOUT, MTRTESTRESULT, ISCTPTCARRIEDOUT, CTPTTESTRSLT, NEW_MTRMFCNAME, NEW_MTRMFCMONTH, NEW_MTRMFCYEAR,";
                        sqlQuery += " NEW_MTRTYPE, NEW_MTRPHASE, NEW_MTRCOFIG, NEW_MTRACCURACYCLASS, NEW_MTRCOSTANT, NEW_MTRCONSTANTUNIT, NEW_MTRVOLATGERATING, NEW_MTRCURRENTRATING, NEW_MTRDETAILOTHER, REM_MTRMFCNAME,";
                        sqlQuery += " REM_MTRMFCMONTH, REM_MTRMFCYEAR, REM_MTRTYPE, REM_MTRPHASE, REM_MTRCOFIG, REM_MTRACCURACYCLASS, REM_MTRCOSTANT, REM_MTRCONSTANTUNIT, REM_MTRVOLATGERATING, REM_MTRCURRENTRATING, REM_MTRDETAILOTHER, EXT_MTRMFCNAME, EXT_MTRMFCMONTH, EXT_MTRMFCYEAR, ";
                        sqlQuery += " EXT_MTRTYPE, EXT_MTRPHASE, EXT_MTRCOFIG, EXT_MTRACCURACYCLASS, EXT_MTRCOSTANT, EXT_MTRCONSTANTUNIT, EXT_MTRVOLATGERATING, EXT_MTRCURRENTRATING, EXT_MTRDETAILOTHER, NEW_MTRBOXTYPE, NEW_SINGLEPHASEMTRTYPE, NEW_SINGLEPHASEPREFITTEDBOX, NEW_LTCTMAKE, ";
                        sqlQuery += " NEW_LTCTMFGYEAR, NEW_CTPRIMARYVALUE, NEW_CTSECONDARYVALUE, NEW_LTCTACCURACYCLASS, NEW_HTMTRTYPE, NEW_HTMAKE, NEW_HTMFGYEAR, NEW_HTCTPRIMARYVALUE, NEW_HTCTSECONDRYVALE, NEW_HTCTACCURACYCLASS,";
                        sqlQuery += " NEW_HTCTVA, NEW_HTPTPRIMARYVALE, NEW_HTPTSECONDARYVALUE, NEW_HTPTACCURACYCLASS, NEW_HTPTVA, NEW_HTCOONECTIONCONFIG, NEW_EQID, EXT_MTRBOXTYPE, EXT_SINGLEPHASEMTRTYPE, EXT_SINGLEPHASEPREFITTEDBOX, EXT_LTCTMAKE, EXT_LTCTMFGYEAR, EXT_CTPRIMARYVALUE, ";
                        sqlQuery += " EXT_CTSECONDARYVALUE, EXT_LTCTACCURACYCLASS, EXT_HTMTRTYPE, EXT_HTMAKE, EXT_HTMFGYEAR, EXT_HTCTPRIMARYVALUE, EXT_HTCTSECONDRYVALE, EXT_HTCTACCURACYCLASS, EXT_HTCTVA, EXT_HTPTPRIMARYVALE, EXT_HTPTSECONDARYVALUE, EXT_HTPTACCURACYCLASS, EXT_HTPTVA, ";
                        sqlQuery += " EXT_HTCOONECTIONCONFIG, EXT_EQID, REM_MTRBOXTYPE, REM_SINGLEPHASEMTRTYPE, REM_SINGLEPHASEPREFITTEDBOX, REM_LTCTMAKE, REM_LTCTMFGYEAR, REM_CTPRIMARYVALUE, REM_CTSECONDARYVALUE, REM_LTCTACCURACYCLASS,";
                        sqlQuery += " REM_HTMTRTYPE, REM_HTMAKE, REM_HTMFGYEAR, REM_HTCTPRIMARYVALUE, REM_HTCTSECONDRYVALE, REM_HTCTACCURACYCLASS, REM_HTCTVA, REM_HTPTPRIMARYVALE, REM_HTPTSECONDARYVALUE, REM_HTPTACCURACYCLASS, REM_HTPTVA, REM_HTCOONECTIONCONFIG, REM_EQID, PREMISECATEGORY,";
                        sqlQuery += " SUBPREMISECATEGORY, MTRLOC,caNo , meter_Num , orderType , pmActivityType , oldMtrPhoto1 , oldMtrPhoto2 , consumerAddress , consumerFatherName , sectionedLoad , tariffCategoryValue , isMtrBxInstall , isAmrInstall , isAmrAvailableAtSite , isNewCblInstall ,";
                        sqlQuery += " isServiceCblReplaced , mtrImpRdkW , mtrImpRdkVAh , mtrImpRdkVA , mtrInstV1 , mtrInstV2 , mtrInstV3 , mtrInstL1 , mtrInstL2 , mtrInstL3 , mtrPfSign , mtrPfValue , mtrExpKwh , mtrExpkW , mtrExpkVAh , mtrExpkVA , errKwhSign , errkWhvalue , errkVAhSign ,";
                        sqlQuery += " errkVAhValue , ctTestRslt , ptTestRslt , mtrHeight , mtrStatus , mtrSupplyStatus , mtrBxStatus , mtrDisplayStatus , mtrDownLoadingStatus , mtrReadingCordStatus , mtrAmrStatus , mtrSrvcCblStatus , mtrBodySealStatus , mtrBodyUltraSonicStatus , mtrTerminalSealStatus ,";
                        sqlQuery += " mtrBxSealStatus , mtrBusBarSealStatus , MFOnBill , actualMF , billMfValue , tariffCategory , GIS_LAT , GIS_LONG , looseFlag,";
                        //sqlQuery += " PM_BB_CABLE_USED,PM_CABLESIZE2_REQUIRED,PM_OUTPUTCABLELENGTH,PM_SMARTMETERSIMNO,PM_SMARTMETERSIMCODE,PM_OLD_MR_KWH,PM_OLD_MR_KW,PM_MRKVAH_OLD,";
                        //sqlQuery += " PM_OLD_MR_KVA,PM_LAB_TSTNG_NTC,PM_NOTICE_DATE,PM_LABTESTING_DATE_OLD,PM_METER_REMOVED_BY,PM_OUTPUTBUSLENGTH,PM_CABLE_REQD,PM_INSTALLATION,PM_BUS_BAR_NO,PM_DRUMSIZE,PM_RUNNINGLENGTHFROM,PM_RUNNINGLENGTHTO,PM_CABLELENGTH,PM_CABLESIZE_OLD,PM_BUSBARSIZE,PM_ACCOUNT_CLASS,";
                        //sqlQuery += " PM_CONNECTION_OBJE,PM_PLANNER_GROUP_INGRP,PM_ERDAT_ORDER_CREATION_D,PM_ELCB_INSTALLED,PM_BUS_BAR_REMARK,PM_OLD_DEVICE_NO,PM_ACTIVITY_REASON,PM_MR_KWH,PM_MR_KW,PM_MR_KVAH,PM_MR_KVA,PM_CABLESIZE2,PM_B_BAR_CABLE_SIZE,PM_CABLEINSTALLTYPE,PM_OVERHEAD_UG,PM_CABLELENGTH_OLD,";
                        //sqlQuery += " PM_MCR_VEND,PM_REM_BOX_SEAL1,PM_REM_BOX_SEAL2,PM_REM_BUSBAR_SEAL1,PM_REM_BUSBAR_SEAL2,PM_REM_OTHER_SEAL,PM_COMP_CODE,PM_ACTIVITY_DATE,PM_FLAG1,PM_FLAG2,PM_FLAG3,PM_VENDOR_CODE,ACCOUNT_CLASS,AUART,ILART_ACTIVITY_TYPE,PM_TAKEPHOTOGRAPH,PM_PHOTO_ATTACH,KWH_TEST_RESULT,KVAH_TEST_RESULT,";
                        //sqlQuery += " REPT_NO,TESTED_BY,FLG_MT,MET_TEST_REM,ADD_REMARK,";
                        sqlQuery += " EXISTING_MTRBOX_SRNUM,EXISTING_AMR_SRNUM,CT_RPHASE_RE,CT_RPHASE_PE,CT_YPHASE_RE,CT_YPHASE_PE,CT_BPHASE_RE,CT_BPHASE_PE,PT_RPHASE_RE,PT_RPHASE_PE,PT_YPHASE_RE,PT_YPHASE_PE,PT_BPHASE_RE,PT_BPHASE_PE,RMVD_MTRIMPRD_KWH,RMVD_MTRIMPRD_KW,";
                        sqlQuery += " RMVD_MTRIMPRD_KVAH,RMVD_MTRIMPRD_KVA,RMVD_MTRINST_V1,RMVD_MTRINST_V2,RMVD_MTRINST_V3,RMVD_MTRINST_L1,RMVD_MTRINST_L2,RMVD_MTRINST_L3,RMVD_MTR_PFSIGN,RMVD_MTR_PFVALUE,RMVD_ISEXPORT_PARAM,RMVD_MTREXP_KWH,RMVD_MTREXP_KW,RMVD_MTREXP_KVAH,RMVD_MTREXP_KVA,RMVD_ISMTRTESTING_CARRIEDOUT,RMVD_ERRKWH_SIGN,RMVD_ERRKWH_VALUE,";
                        sqlQuery += " RMVD_ERRKVAH_SIGN,RMVD_ERRKVAH_VALUE,RMVD_ISCTPT_CARRIEDOUT,RMVD_CTRPHASE_RE,RMVD_CTRPHASE_PE,RMVD_CTYPHASE_RE,RMVD_CTYPHASE_PE,RMVD_CTBPHASE_RE,RMVD_CTBPHASE_PE,RMVD_PTRPHASE_RE,RMVD_PTRPHASE_PE,NEW_LTCTMFC,NEW_HTMFC,EXISTING_LTCTMFC,EXISTING_HTMFC,REMOVED_LTCTMFC,REMOVED_HTMFC,";
                        sqlQuery += " ISOLDMTR_REPLACED,ISOLDMTRBX_REPLACED,ISAMR_RMVDORREPLACED,rmvd_PtYPhase_RE, rmvd_PtYPhase_PE ,rmvd_PtBPhase_RE , rmvd_PtBPhase_PE,MTRIMPRDKWH,REG_CONTACT_NO,ISNEWMTRINSTALLATSITE)";
                        sqlQuery += " Values('" + _pdata.OrderNo + "','" + _pdata.newMtrBoxSrNum + "','" + _pdata.oldMtrBoxSrNum + "','" + _pdata.installMtrBoxType + "','" + _pdata.newMtrSrlNum + "','" + _pdata.oldMtrSrlNum + "','" + _pdata.installMtrType + "',";
                        sqlQuery += "'" + _pdata.scanSrNum + "','" + _pdata.newAmrSrlNum + "','" + _pdata.oldAmrSrNum + "','" + _pdata.remarks + "','" + _pdata.mtrPhoto1 + "','" + _pdata.mtrPhoto2 + "','" + _pdata.mcrPhoto + "','" + _pdata.signaturePhoto + "',";
                        sqlQuery += "'" + _pdata.gunnyBagPhoto + "','" + _pdata.consumerName + "','" + _pdata.consumerContactNum + "','" + _pdata.empName + "','" + _pdata.empNumber + "',";
                        sqlQuery += " '" + _pdata.mtrBxReplacementResion + "','" + _pdata.mtrReplacementResion + "','" + _pdata.circleName + "','" + _pdata.divisionName + "','" + _pdata.sbDivisionName + "','" + _pdata.departmentName.ToUpper() + "','" + _pdata.mcrType + "',";
                        sqlQuery += "'" + _pdata.mcrNumber + "',TO_DATE('" + _pdata.mcrDate + "','dd/MM/YYYY'),'" + _pdata.mcrMeteringActivity + "','" + _pdata.mcrFieldActivityType + "','" + _pdata.mcrFieldSbActivityType + "','" + _pdata.mcrVenderName + "','" + _pdata.workOrderNum + "',";
                        sqlQuery += " '" + _pdata.readingCord + "','" + _pdata.lugsuntity + "','" + _pdata.prcQuantity + "','" + _pdata.bushingProtectorQuan + "','" + _pdata.busbarQuan + "',";
                        sqlQuery += " '" + _pdata.mSealQuan + "','" + _pdata.saddleClampQuan + "','" + _pdata.pipeQuan + "','" + _pdata.glAngleQuan + "','" + _pdata.cuWireQuan + "','" + _pdata.newMtrTerminalSeal1 + "','" + _pdata.newMtrTerminalSeal2 + "',";
                        sqlQuery += " '" + _pdata.newMtrHeadSeal1 + "','" + _pdata.newMtrBoxSeal1 + "','" + _pdata.newMtrBoxSeal2 + "','" + _pdata.newMtrBoxSeal3 + "','" + _pdata.newMtrBoxICSeal1 + "','" + _pdata.newMtrBoxICSeal2 + "','" + _pdata.newMtrBoxICSeal3 + "',";
                        sqlQuery += " '" + _pdata.newMtrBoxICSeal4 + "','" + _pdata.newMtrBoxOGSeal1 + "','" + _pdata.newMtrBoxOGSeal2 + "','" + _pdata.newMtrBoxOGSeal3 + "','" + _pdata.newMtrBoxOGSeal4 + "',";
                        sqlQuery += "  '" + _pdata.newBsBarSeal1 + "','" + _pdata.newBsBarSeal2 + "','" + _pdata.newHoloGramTerminalSeal1 + "','" + _pdata.newHoloGramMtrBoxSeal2 + "','" + _pdata.newHoloGramICSeal3 + "','" + _pdata.newHoloGramICSeal4 + "',";
                        sqlQuery += " '" + _pdata.newHoloGramOGSeal5 + "','" + _pdata.newHoloGramOGSeal6 + "','" + _pdata.oldMtrTerminalSeal1 + "','" + _pdata.oldMtrTerminalSeal2 + "','" + _pdata.oldMtrHeadSeal1 + "','" + _pdata.oldMtrBoxSeal1 + "',";
                        sqlQuery += " '" + _pdata.oldMtrBoxSeal2 + "','" + _pdata.oldMtrBoxSeal3 + "','" + _pdata.oldMtrBoxICSeal1 + "','" + _pdata.oldMtrBoxICSeal2 + "','" + _pdata.oldMtrBoxICSeal3 + "','" + _pdata.oldMtrBoxICSeal4 + "',";
                        sqlQuery += " '" + _pdata.oldMtrBoxOGSeal1 + "','" + _pdata.oldMtrBoxOGSeal2 + "','" + _pdata.oldMtrBoxOGSeal3 + "','" + _pdata.oldMtrBoxOGSeal4 + "','" + _pdata.oldBsBarSeal1 + "','" + _pdata.oldBsBarSeal2 + "',    '" + _pdata.oldHoloGramTerminalSeal1 + "',";
                        sqlQuery += " '" + _pdata.oldHoloGramMtrBoxSeal2 + "','" + _pdata.oldHoloGramICSeal3 + "','" + _pdata.oldHoloGramICSeal4 + "','" + _pdata.oldHoloGramOGSeal5 + "','" + _pdata.oldHoloGramOGSeal6 + "','" + _pdata.icCblType + "','" + _pdata.icCbleSize + "',";
                        sqlQuery += "  '" + _pdata.icCblLayingType + "','" + _pdata.icCblDrumNum + "','" + _pdata.icCblSrlNumFrom + "','" + _pdata.icCblSrlNumTo + "',";
                        sqlQuery += " '" + _pdata.icTotalCblLength + "','" + _pdata.feedingPointType + "','" + _pdata.feedingPointSrlNum + "',    '" + _pdata.cblUsedInOG + "','" + _pdata.ogCblType + "','" + _pdata.ogCbleSize + "','" + _pdata.ogCblLayingType + "',";
                        sqlQuery += " '" + _pdata.ogCblDrumNum + "','" + _pdata.ogCblSrlNumFrom + "',    '" + _pdata.ogCblSrlNumTo + "','" + _pdata.ogTotalCblLength + "',    '" + _pdata.removedCblSize + "','" + _pdata.removedCblLength + "','" + _pdata.existingCblSize + "',";
                        sqlQuery += "'" + _pdata.existingFeedingPointType + "','" + _pdata.existingFeedingSrNum + "','" + _pdata.newAmrType + "','" + _pdata.newAmrManufacturer + "','" + _pdata.newAmrSimId + "',   ";
                        sqlQuery += " '" + _pdata.newAmrSimNum + "','" + _pdata.newAmrOther + "','" + _pdata.existingAmrType + "','" + _pdata.existingAmrManufacturer + "','" + _pdata.existingAmrSimId + "','" + _pdata.existingAmrSimNum + "','" + _pdata.existingAmrOther + "',";
                        sqlQuery += "  '" + _pdata.removedAmrType + "','" + _pdata.removedAmrManufacturer + "',";
                        sqlQuery += "  '" + _pdata.removedAmrSimId + "','" + _pdata.removedAmrSimNum + "','" + _pdata.removedAmrOther + "','" + _pdata.isGunnyBagPrepared + "','" + _pdata.gunnyBagNum + "','" + _pdata.gunnyBagColor + "','" + _pdata.gunnyBagSealNum + "',   ";
                        sqlQuery += " '" + _pdata.labNoticeNum + "',TO_DATE('" + _pdata.labTestingDate + "','dd/MM/YYYY'),'" + _pdata.userType + "',    '" + _pdata.gunnyReason + "','" + _pdata.mtrImportReading + "','" + _pdata.mtrInstaniousVolt + "','" + _pdata.mtrInstaniousCurnt + "',";
                        sqlQuery += "'" + _pdata.mtrInstaniousPwrFactor + "','" + _pdata.isExportParam + "','" + _pdata.mtrExportReading + "','" + _pdata.isMtrTestingCarriedOut + "',";
                        sqlQuery += " '" + _pdata.mtrTestResult + "','" + _pdata.isCTPTCarriedOut + "','" + _pdata.ctptTestRslt + "',    '" + _pdata.newMtrMfcName + "','" + _pdata.newMtrMfcMonth + "','" + _pdata.newMtrMfcYear + "','" + _pdata.newMtrType + "',    ";
                        sqlQuery += "'" + _pdata.newMtrPhase + "','" + _pdata.newMtrCofig + "',    '" + _pdata.newMtrAccuracyClass + "','" + _pdata.newMtrCostant + "','" + _pdata.newMtrConstantUnit + "',    '" + _pdata.newMtrVolatgeRating + "','" + _pdata.newMtrCurrentRating + "',";
                        sqlQuery += "'" + _pdata.newMtrDetailOther + "','" + _pdata.removedMtrMfcName + "','" + _pdata.removedMtrMfcMonth + "',    '" + _pdata.removedMtrMfcYear + "','" + _pdata.removedMtrType + "',  ";
                        sqlQuery += " '" + _pdata.removedMtrPhase + "','" + _pdata.removedMtrCofig + "','" + _pdata.removedMtrAccuracyClass + "',    '" + _pdata.removedMtrCostant + "','" + _pdata.removedMtrConstantUnit + "','" + _pdata.removedMtrVolatgeRating + "',";
                        sqlQuery += "'" + _pdata.removedMtrCurrentRating + "','" + _pdata.removedMtrDetailOther + "','" + _pdata.existingMtrMfcName + "','" + _pdata.existingMtrMfcMonth + "',    '" + _pdata.existingMtrMfcYear + "','" + _pdata.existingMtrType + "',";
                        sqlQuery += "'" + _pdata.existingMtrPhase + "','" + _pdata.existingMtrCofig + "','" + _pdata.existingMtrAccuracyClass + "','" + _pdata.existingMtrCostant + "','" + _pdata.existingMtrConstantUnit + "',   ";
                        sqlQuery += "'" + _pdata.existingMtrVolatgeRating + "','" + _pdata.existingMtrCurrentRating + "','" + _pdata.existingMtrDetailOther + "','" + _pdata.newMtrBoxType + "',    '" + _pdata.newSinglePhaseMtrType + "','" + _pdata.newSinglePhasePreFittedBox + "',";
                        sqlQuery += "'" + _pdata.newLtCtMonth + "','" + _pdata.newLtCtMfgYear + "','" + _pdata.newCtPrimaryValue + "','" + _pdata.newCtSecondaryValue + "',    '" + _pdata.newLtCtAccuracyClass + "','" + _pdata.newHtMtrType + "','" + _pdata.newHtMonth + "',   ";
                        sqlQuery += "'" + _pdata.newHtMfgYear + "','" + _pdata.newHtCTPrimaryValue + "','" + _pdata.newHtCTSecondryVale + "','" + _pdata.newHtCTAccuracyClass + "',    '" + _pdata.newHtCTVA + "',  ";
                        sqlQuery += "'" + _pdata.newHtPTPrimaryVale + "','" + _pdata.newHtPTSecondaryValue + "','" + _pdata.newHtPTAccuracyClass + "','" + _pdata.newHtPTVA + "',    '" + _pdata.newHtCoonectionConfig + "','" + _pdata.newEQId + "','" + _pdata.existingMtrBoxType + "',";
                        sqlQuery += "'" + _pdata.existingSinglePhaseMtrType + "','" + _pdata.existingSinglePhasePreFittedBox + "','" + _pdata.existingLtCtMonth + "','" + _pdata.existingLtCtMfgYear + "','" + _pdata.existingCtPrimaryValue + "','" + _pdata.existingCtSecondaryValue + "',";
                        sqlQuery += "'" + _pdata.existingLtCtAccuracyClass + "','" + _pdata.existingHtMtrType + "','" + _pdata.existingHtMonth + "','" + _pdata.existingHtMfgYear + "',   ";
                        sqlQuery += "'" + _pdata.existingHtCTPrimaryValue + "','" + _pdata.existingHtCTSecondryVale + "','" + _pdata.existingHtCTAccuracyClass + "','" + _pdata.existingHtCTVA + "','" + _pdata.existingHtPTPrimaryVale + "','" + _pdata.existingHtPTSecondaryValue + "',";
                        sqlQuery += "'" + _pdata.existingHtPTAccuracyClass + "','" + _pdata.existingHtPTVA + "','" + _pdata.existingHtCoonectionConfig + "','" + _pdata.existingEQId + "','" + _pdata.removedMtrBoxType + "','" + _pdata.removedSinglePhaseMtrType + "',";
                        sqlQuery += "'" + _pdata.removedSinglePhasePreFittedBox + "','" + _pdata.removedLtCtMonth + "','" + _pdata.removedLtCtMfgYear + "','" + _pdata.removedCtPrimaryValue + "',   ";
                        sqlQuery += "'" + _pdata.removedCtSecondaryValue + "','" + _pdata.removedLtCtAccuracyClass + "','" + _pdata.removedHtMtrType + "','" + _pdata.removedHtMonth + "','" + _pdata.removedHtMfgYear + "','" + _pdata.removedHtCTPrimaryValue + "',";
                        sqlQuery += "'" + _pdata.removedHtCTSecondryVale + "','" + _pdata.removedHtCTAccuracyClass + "',";
                        sqlQuery += "'" + _pdata.removedHtCTVA + "','" + _pdata.removedHtPTPrimaryVale + "','" + _pdata.removedHtPTSecondaryValue + "','" + _pdata.removedHtPTAccuracyClass + "','" + _pdata.removedHtPTVA + "','" + _pdata.removedHtCoonectionConfig + "',";
                        sqlQuery += "'" + _pdata.removedEQId + "','" + _pdata.premiseCategory + "',";
                        sqlQuery += "'" + _pdata.subPremiseCategory + "','" + _pdata.mtrLoc + "','" + _pdata.caNo + "','" + _pdata.meter_Num + "','" + _pdata.orderType + "','" + _pdata.pmActivityType + "','" + _pdata.oldMtrPhoto1 + "','" + _pdata.oldMtrPhoto2 + "','" + _pdata.consumerAddress + "',";
                        sqlQuery += "'" + _pdata.consumerFatherName + "','" + _pdata.sectionedLoad + "','" + _pdata.tariffCategoryValue + "','" + _pdata.isMtrBxInstall + "','" + _pdata.isAmrInstall + "','" + _pdata.isAmrAvailableAtSite + "','" + _pdata.isNewCblInstall + "','" + _pdata.isServiceCblReplaced + "',";
                        sqlQuery += "'" + _pdata.mtrImpRdkW + "','" + _pdata.mtrImpRdkVAh + "','" + _pdata.mtrImpRdkVA + "','" + _pdata.mtrInstV1 + "','" + _pdata.mtrInstV2 + "','" + _pdata.mtrInstV3 + "','" + _pdata.mtrInstL1 + "','" + _pdata.mtrInstL2 + "','" + _pdata.mtrInstL3 + "','" + _pdata.mtrPfSign + "',";
                        sqlQuery += "'" + _pdata.mtrPfValue + "','" + _pdata.mtrExpKwh + "','" + _pdata.mtrExpkW + "','" + _pdata.mtrExpkVAh + "','" + _pdata.mtrExpkVA + "','" + _pdata.errKwhSign + "','" + _pdata.errkWhvalue + "','" + _pdata.errkVAhSign + "','" + _pdata.errkVAhValue + "','" + _pdata.ctTestRslt + "',";
                        sqlQuery += "'" + _pdata.ptTestRslt + "','" + _pdata.mtrHeight + "','" + _pdata.mtrStatus + "','" + _pdata.mtrSupplyStatus + "','" + _pdata.mtrBxStatus + "','" + _pdata.mtrDisplayStatus + "','" + _pdata.mtrDownLoadingStatus + "','" + _pdata.mtrReadingCordStatus + "','" + _pdata.mtrAmrStatus + "',";
                        sqlQuery += "'" + _pdata.mtrSrvcCblStatus + "','" + _pdata.mtrBodySealStatus + "','" + _pdata.mtrBodyUltraSonicStatus + "','" + _pdata.mtrTerminalSealStatus + "','" + _pdata.mtrBxSealStatus + "','" + _pdata.mtrBusBarSealStatus + "','" + _pdata.MFOnBill + "','" + _pdata.actualMF + "','" + _pdata.billMfValue + "',";
                        sqlQuery += "'" + _pdata.tariffCategory + "','" + _pdata.GIS_LAT + "','" + _pdata.GIS_LONG + "','" + _pdata.looseFlag + "',";
                        sqlQuery += "'" + _pdata.existingMtrBoxSrNum + "','" + _pdata.existingAmrSrNum + "',";
                        sqlQuery += "'" + _pdata.ctRPhaseRE + "','" + _pdata.ctRPhasePE + "','" + _pdata.ctYPhaseRE + "','" + _pdata.ctYPhasePE + "','" + _pdata.ctBPhaseRE + "','" + _pdata.ctBPhasePE + "','" + _pdata.ptRPhaseRE + "','" + _pdata.ptRPhasePE + "','" + _pdata.ptYPhaseRE + "',";
                        sqlQuery += "'" + _pdata.ptYPhasePE + "','" + _pdata.ptBPhaseRE + "','" + _pdata.ptBPhasePE + "','" + _pdata.rmvdMtrImpRdkWh + "','" + _pdata.rmvdMtrImpRdkW + "','" + _pdata.rmvdMtrImpRdkVAh + "','" + _pdata.rmvdMtrImpRdkVA + "','" + _pdata.rmvdMtrInstV1 + "','" + _pdata.rmvdMtrInstV2 + "',";
                        sqlQuery += "'" + _pdata.rmvdMtrInstV3 + "','" + _pdata.rmvdMtrInstL1 + "','" + _pdata.rmvdMtrInstL2 + "','" + _pdata.rmvdMtrInstL3 + "','" + _pdata.rmvdMtrPfSign + "','" + _pdata.rmvdMtrPfValue + "','" + _pdata.rmvdIsExportParam + "','" + _pdata.rmvdMtrExpKwh + "','" + _pdata.rmvdMtrExpkW + "',";
                        sqlQuery += "'" + _pdata.rmvdMtrExpkVAh + "','" + _pdata.rmvdMtrExpkVA + "','" + _pdata.rmvdIsMtrTestingCarriedOut + "','" + _pdata.rmvdErrKwhSign + "','" + _pdata.rmvdErrkWhvalue + "','" + _pdata.rmvdErrkVAhSign + "','" + _pdata.rmvdErrkVAhValue + "','" + _pdata.rmvdIsCTPTCarriedOut + "',";
                        sqlQuery += "'" + _pdata.rmvdCtRPhaseRE + "','" + _pdata.rmvdCtRPhasePE + "','" + _pdata.rmvdCtYPhaseRE + "','" + _pdata.rmvdCtYPhasePE + "','" + _pdata.rmvdCtBPhaseRE + "','" + _pdata.rmvdCtBPhasePE + "','" + _pdata.rmvdPtRPhaseRE + "',";
                        sqlQuery += "'" + _pdata.rmvdPtRPhasePE + "','" + _pdata.newLtCtMfc + "','" + _pdata.newHtMfc + "','" + _pdata.existingLtCtMfc + "','" + _pdata.existingHtMfc + "','" + _pdata.removedLtCtMfc + "','" + _pdata.removedHtMfc + "',";
                        sqlQuery += "'" + _pdata.isOldMtrReplaced + "','" + _pdata.isOldMtrBxReplaced + "','" + _pdata.isAmrRmvdOrReplaced + "','" + _pdata.rmvdPtYPhaseRE + "','" + _pdata.rmvdPtYPhasePE + "','" + _pdata.rmvdPtBPhaseRE + "','" + _pdata.rmvdPtBPhasePE + "','" + _pdata.mtrImpRdkWh + "','" + _pdata.registerConsumerContactNum + "','" + _pdata.isNewMtrInstallAtSite + "')";
                        using (con = new OleDbConnection(connectionString))
                        {
                            cmd = new OleDbCommand(sqlQuery, con);
                            cmd.Connection = con;
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            int i = cmd.ExecuteNonQuery();
                            if (i != 0)
                            {
                                sqlQuery = "update MOBINT.MCR_INPUT_DETAILS set FLAG ='PC' where ORDERID='" + _pdata.OrderNo + "'";
                                cmd = new OleDbCommand(sqlQuery, con);
                                cmd.Connection = con;
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                int result = cmd.ExecuteNonQuery();
                                sqlQuery = "update MOBINT.PM_NEW_METERMETERUP set STATUS ='C' where METER_SERIAL_NO='" + _pdata.newMtrSrlNum + "'";
                                cmd = new OleDbCommand(sqlQuery, con);
                                cmd.Connection = con;
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                int result1 = cmd.ExecuteNonQuery();
                                if (result1 > 0)
                                {
                                    sqlQuery = "update mobint.PM_ALLOCATED_METER set STATUS ='C' where METER_SERIAL_NO='" + _pdata.newMtrSrlNum + "' AND STATUS='N'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                }
                                if (_pdata.newMtrTerminalSeal1 != null && _pdata.newMtrTerminalSeal1 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrTerminalSeal1 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re1 = cmd.ExecuteNonQuery();
                                    if (re1 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrTerminalSeal2 != null && _pdata.newMtrTerminalSeal2 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrTerminalSeal2 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re2 = cmd.ExecuteNonQuery();
                                    if (re2 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrHeadSeal1 != null && _pdata.newMtrHeadSeal1 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrHeadSeal1 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re3 = cmd.ExecuteNonQuery();
                                    if (re3 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrBoxSeal1 != null && _pdata.newMtrBoxSeal1 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrBoxSeal1 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re4 = cmd.ExecuteNonQuery();
                                    if (re4 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrBoxSeal2 != null && _pdata.newMtrBoxSeal2 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrBoxSeal2 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re5 = cmd.ExecuteNonQuery();
                                    if (re5 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrBoxSeal3 != null && _pdata.newMtrBoxSeal3 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrBoxSeal3 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re6 = cmd.ExecuteNonQuery();
                                    if (re6 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrBoxICSeal1 != null && _pdata.newMtrBoxICSeal1 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrBoxICSeal1 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re7 = cmd.ExecuteNonQuery();
                                    if (re7 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrBoxICSeal2 != null && _pdata.newMtrBoxICSeal2 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrBoxICSeal2 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re8 = cmd.ExecuteNonQuery();
                                    if (re8 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrBoxICSeal3 != null && _pdata.newMtrBoxICSeal3 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrBoxICSeal3 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re9 = cmd.ExecuteNonQuery();
                                    if (re9 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrBoxICSeal4 != null && _pdata.newMtrBoxICSeal4 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrBoxICSeal4 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re10 = cmd.ExecuteNonQuery();
                                    if (re10 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrBoxOGSeal1 != null && _pdata.newMtrBoxOGSeal1 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrBoxOGSeal1 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re11 = cmd.ExecuteNonQuery();
                                    if (re11 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrBoxOGSeal2 != null && _pdata.newMtrBoxOGSeal2 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrBoxOGSeal2 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re12 = cmd.ExecuteNonQuery();
                                    if (re12 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrBoxOGSeal3 != null && _pdata.newMtrBoxOGSeal3 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrBoxOGSeal3 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re13 = cmd.ExecuteNonQuery();
                                    if (re13 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newMtrBoxOGSeal4 != null && _pdata.newMtrBoxOGSeal4 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newMtrBoxOGSeal4 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re14 = cmd.ExecuteNonQuery();
                                    if (re14 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newBsBarSeal1 != null && _pdata.newBsBarSeal1 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newBsBarSeal1 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re15 = cmd.ExecuteNonQuery();
                                    if (re15 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newBsBarSeal2 != null && _pdata.newBsBarSeal2 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newBsBarSeal2 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re16 = cmd.ExecuteNonQuery();
                                    if (re16 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newHoloGramTerminalSeal1 != null && _pdata.newHoloGramTerminalSeal1 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newHoloGramTerminalSeal1 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re17 = cmd.ExecuteNonQuery();
                                    if (re17 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newHoloGramMtrBoxSeal2 != null && _pdata.newHoloGramMtrBoxSeal2 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newHoloGramMtrBoxSeal2 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re18 = cmd.ExecuteNonQuery();
                                    if (re18 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newHoloGramICSeal3 != null && _pdata.newHoloGramICSeal3 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newHoloGramICSeal3 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re19 = cmd.ExecuteNonQuery();
                                    if (re19 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newHoloGramICSeal4 != null && _pdata.newHoloGramICSeal4 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newHoloGramICSeal4 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re20 = cmd.ExecuteNonQuery();
                                    if (re20 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newHoloGramOGSeal5 != null && _pdata.newHoloGramOGSeal5 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newHoloGramOGSeal5 + "'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re21 = cmd.ExecuteNonQuery();
                                    if (re21 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (_pdata.newHoloGramOGSeal6 != null && _pdata.newHoloGramOGSeal6 != "")
                                {
                                    sqlQuery = "update MOBINT.PM_NEW_METER_SEAL_UPDATION set STATUS ='C' where SEAL_NUMBER='" + _pdata.newHoloGramOGSeal6 + "' AND STATUS='N'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    int re22 = cmd.ExecuteNonQuery();
                                    if (re22 > 0)
                                    {
                                        sqlQuery = "update MOBINT.PM_ALLOCATED_SEAL set STATUS ='C' where SEAL_ID='" + _pdata.newMtrTerminalSeal1 + "' AND STATUS='N'";
                                        cmd = new OleDbCommand(sqlQuery, con);
                                        cmd.Connection = con;
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                sqlQuery = "update MOBINT.PM_NEW_AMR_MSTUPDATION set STATUS ='C' where AMR_SERIAL_NO='" + _pdata.newAmrSrlNum + "'";
                                cmd = new OleDbCommand(sqlQuery, con);
                                cmd.Connection = con;
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                int result3 = cmd.ExecuteNonQuery();
                                if (result3 > 0)
                                {
                                    sqlQuery = "update MOBINT.PM_ALLOCATED_AMR set STATUS ='C' where AMR_SERIAL_NO='" + _pdata.newAmrSrlNum + "' AND STATUS='N'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                }
                                sqlQuery = "update MOBINT.PM_REPORT_BOOK_FORM set STATUS ='C' where REPORT_SERIAL_NO='" + _pdata.mcrNumber + "'";
                                cmd = new OleDbCommand(sqlQuery, con);
                                cmd.Connection = con;
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                int result4 = cmd.ExecuteNonQuery();
                                if (result4 > 0)
                                {
                                    sqlQuery = "update mobint.PM_ALLOCATED_REPORTBOOK set STATUS ='C' where REPORT_SERIAL_NO='" + _pdata.mcrNumber + "' AND STATUS ='N'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                }
                                sqlQuery = "update MOBINT.PM_NEWCT_BOXHT_MSTUPDATION set STATUS ='C' where BOX_SERIAL_NO='" + _pdata.newMtrBoxSrNum + "'";
                                cmd = new OleDbCommand(sqlQuery, con);
                                cmd.Connection = con;
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                int result5 = cmd.ExecuteNonQuery();
                                if (result5 > 0)
                                {
                                    sqlQuery = "update MOBINT.PM_ALLOCATED_BOX set STATUS ='C' where BOX_SERIAL_NO='" + _pdata.newMtrBoxSrNum + "' AND STATUS='N'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                }
                                sqlQuery = "update MOBINT.PM_GUNNYBAG_MASTER set STATUS ='C' where GUNNY_BAG_SR_NO='" + _pdata.gunnyBagNum + "'";
                                cmd = new OleDbCommand(sqlQuery, con);
                                cmd.Connection = con;
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                int result6 = cmd.ExecuteNonQuery();
                                if (result6 > 0)
                                {
                                    sqlQuery = "update MOBINT.PM_ALLOCATED_GUNNYBAG set STATUS ='C' where GUNNY_BAG_ID='" + _pdata.gunnyBagNum + "' AND STATUS='N'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                }
                                sqlQuery = "update MOBINT.PM_GUNNYBAG_SEAL_MASTER set STATUS ='C' where GUNNY_BAG_SEAL_SR_NO='" + _pdata.gunnyBagSealNum + "'";
                                cmd = new OleDbCommand(sqlQuery, con);
                                cmd.Connection = con;
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                int result7 = cmd.ExecuteNonQuery();
                                if (result7 > 0)
                                {
                                    sqlQuery = "update MOBINT.PM_ALLOCATED_GUNNYBAG_SEAL set STATUS ='C' where GUNNY_BAGSEAL_ID='" + _pdata.gunnyBagSealNum + "' AND STATUS='N'";
                                    cmd = new OleDbCommand(sqlQuery, con);
                                    cmd.Connection = con;
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                }
                                con.Close();
                                response.Message = "Success";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                            return Msg;
                        }
                    }
                    else
                    {
                        response.Status = "Failed";
                        response.Message = "UserID is not valid";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        return Msg;
                    }
                }
                catch (Exception ex)
                {
                    response.Status = "Failed";
                    response.Message = ex.Message.ToString();
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    return Msg;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        [HttpPost]
        [Route("API/PowerMetering/FetchAllDetails")]
        public HttpResponseMessage FetchAllDetails([FromBody] Logindetails login)
        {
            List<SealNumber> _SealNumber = new List<SealNumber>();
            List<MeterBoxDetails> _MeterBoxDetails = new List<MeterBoxDetails>();
            List<AMRDetails> _dlist = new List<AMRDetails>();
            List<MCRNODetails> _MCRNODetails = new List<MCRNODetails>();
            List<MeterNumber> _MeterNumbers = new List<MeterNumber>();
            List<GunnyBag> listgunny = new List<GunnyBag>();
            List<GunnyBagSrNo> listgunnyseal = new List<GunnyBagSrNo>();
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    //sqlQuery = "SELECT METER_NO FROM MOBINT.MCR_INPUT_DETAILS MID INNER JOIN MOBINT.PM_LOGIN MLM  ON MID.ALLOCATE_TO = MLM.USER_ID";
                    //sqlQuery += " WHERE MLM.USER_ID = '" + login.userID + "' AND MID.ACCOUNT_CLASS = 'KCC' AND MID.FLAG='PY'";
                    sqlQuery = " SELECT METER_SERIAL_NO FROM MOBINT.PM_NEW_METERMETERUP N, MOBINT.PM_LOGIN L WHERE N.ALLOCATED_TO = L.USER_ID AND N.ALLOCATED_TO = '" + login.userID + "' AND N.STATUS='A'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        MeterNumber _MeterNumber = new MeterNumber();
                        // string meternowithoutzero = rdr["METER_SERIAL_NO"].ToString();
                        _MeterNumber.meterNumber = rdr["METER_SERIAL_NO"].ToString();
                        //if (meternowithoutzero.Length >= 10)
                        //{
                        //    string sub = meternowithoutzero.Substring(10);
                        //    _MeterNumber.meterNumber = sub;
                        //}
                        _MeterNumbers.Add(_MeterNumber);
                        response1.meterNumber = _MeterNumbers;
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                using (con = new OleDbConnection(connectionString))
                {
                    // sqlQuery = "SELECT SEAL FROM MOBINT.MCR_SEAL_DETAILS MSD INNER JOIN MOBINT.PM_LOGIN MLM  ON MSD.ALLOTED_TO = MLM.USER_ID  WHERE USER_ID ='" + login.userID + "'";
                    sqlQuery = "SELECT SEAL_NUMBER FROM MOBINT.PM_NEW_METER_SEAL_UPDATION MSD INNER JOIN MOBINT.PM_LOGIN MLM  ON MSD.ALLOCATED_TO = MLM.USER_ID  WHERE USER_ID ='" + login.userID + "' AND MSD.STATUS='A'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        SealNumber _sealNumber = new SealNumber();
                        _sealNumber.sealNumber = rdr["SEAL_NUMBER"].ToString();
                        _SealNumber.Add(_sealNumber);
                        response1.SealNumber = _SealNumber;
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                using (con = new OleDbConnection(connectionString))
                {

                    //sqlQuery = "SELECT AMR_NO FROM MOBINT.PM_AMR_DETAILS WHERE ALLOTED_TO='" + login.userID + "'";
                    sqlQuery = "SELECT AMR_SERIAL_NO FROM MOBINT.PM_NEW_AMR_MSTUPDATION A,MOBINT.PM_LOGIN L WHERE A.ALLOCATED_TO = L.USER_ID AND A.ALLOCATED_TO='" + login.userID + "' AND A.STATUS='A'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        AMRDetails _details = new AMRDetails();
                        _details.amrNumber = rdr["AMR_SERIAL_NO"].ToString();
                        _dlist.Add(_details);
                        response1.FetchAMRNo = _dlist;
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT REPORT_SERIAL_NO FROM MOBINT.PM_REPORT_BOOK_FORM R,MOBINT.PM_LOGIN L WHERE R.ALLOCATED_TO = L.USER_ID AND R.ALLOCATED_TO ='" + login.userID + "' and R.STATUS='A'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        MCRNODetails MRNODetails = new MCRNODetails();
                        MRNODetails.mcrNumber = rdr["REPORT_SERIAL_NO"].ToString();
                        _MCRNODetails.Add(MRNODetails);
                        response1.FetchMCRNO = _MCRNODetails;
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                using (con = new OleDbConnection(connectionString))
                {
                    // sqlQuery = "SELECT MB_SERIAL_NO FROM MOBINT.PM_METERBOX_DETAILS  WHERE ALLOTED_TO ='" + login.userID + "' and CONSUME_FLAG='Y'";
                    sqlQuery = "SELECT BOX_SERIAL_NO FROM MOBINT.PM_NEWCT_BOXHT_MSTUPDATION B,MOBINT.PM_LOGIN L  WHERE B.ALLOCATED_TO = L.USER_ID AND b.ALLOCATED_TO ='" + login.userID + "' and B.STATUS='A'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        MeterBoxDetails MterBoxDetails = new MeterBoxDetails();
                        MterBoxDetails.mbSerialNumber = rdr["BOX_SERIAL_NO"].ToString();
                        _MeterBoxDetails.Add(MterBoxDetails);
                        response1.FetchMetreBoxDetails = _MeterBoxDetails;
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT GUNNY_BAG_SR_NO FROM MOBINT.PM_GUNNYBAG_MASTER G, MOBINT.PM_LOGIN L  WHERE G.ALLOCATED_TO = L.USER_ID AND G.ALLOCATED_TO ='" + login.userID + "' and G.STATUS='A'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        GunnyBag objgunnybag = new GunnyBag();
                        objgunnybag.Gunny_BagnSrno = rdr["GUNNY_BAG_SR_NO"].ToString();
                        listgunny.Add(objgunnybag);
                        response1.GunnyBagSerialNo = listgunny;
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT GUNNY_BAG_SEAL_SR_NO FROM MOBINT.PM_GUNNYBAG_SEAL_MASTER S,MOBINT.PM_LOGIN L  WHERE S.ALLOCATED_TO = L.USER_ID AND S.ALLOCATED_TO ='" + login.userID + "' and S.STATUS='A'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        GunnyBagSrNo objgunnyseal = new GunnyBagSrNo();
                        objgunnyseal.Gunny_BagSealSrNo = rdr["GUNNY_BAG_SEAL_SR_NO"].ToString();
                        listgunnyseal.Add(objgunnyseal);
                        response1.GunnyBagSealSerialNo = listgunnyseal;
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                response1.Status = "Success";
                response1.Message = "200";
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    response1.Status,
                    response1.Message,
                    response1.meterNumber,
                    response1.SealNumber,
                    response1.FetchAMRNo,
                    response1.FetchMCRNO,
                    response1.FetchMetreBoxDetails,
                    response1.GunnyBagSerialNo,
                    response1.GunnyBagSealSerialNo,
                    response1.DivSubdivisionList,
                    response1.ActivitySubactivityList
                },
                JsonMediaTypeFormatter.DefaultMediaType);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response1.Status = "Failed";
                response1.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response1);
                return Msg;
            }
        }

        [HttpPost]
        [Route("API/PowerMetering/FetchMasterDetails")]
        public HttpResponseMessage FetchMasterDetails([FromBody] Logindetails login)
        {
            List<DivSubdiv> _DivSubdiv = new List<DivSubdiv>();
            List<ActivitySubactivity> _ActivitySubactivity = new List<ActivitySubactivity>();
            List<PMPREMISEMST> _PMPREMISEMST = new List<PMPREMISEMST>();
            List<VENDORMST> _VENDORMST = new List<VENDORMST>();
            List<EmpolyeeDetails> listemp = new List<EmpolyeeDetails>();
            Response1 response1 = new Response1();
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    //sqlQuery = "SELECT DISTINCT DIVISION,DIVISION_DESC,SUB_DIVISION,SUB_DIVISION_DESC,";
                    //sqlQuery += "CASE WHEN CIRCLE='SOUTH 1' THEN 'SOUTH' WHEN CIRCLE='SOUTH 2' THEN 'SOUTH'";
                    //sqlQuery += "WHEN CIRCLE='WEST 1' THEN 'WEST' WHEN CIRCLE='WEST 2' THEN 'WEST' END  AS ";
                    //sqlQuery += "CIRCLE FROM MOBINT.MCR_SUBDIVISION_MST WHERE ACTIVE_FLAG='Y' ORDER BY DIVISION_DESC ASC";

                    sqlQuery = "SELECT DISTINCT DIVISION,DIVISION_DESC,SUB_DIVISION,SUB_DIVISION_DESC,DECODE(CIRCLE,'WEST1','WEST','WEST2','WEST','SOUTH1','SOUTH','SOUTH2','SOUTH') CIRCLE FROM MOBINT.MCR_SUBDIVISION_MST";
                    sqlQuery += " WHERE DIVISION LIKE 'S%' OR DIVISION LIKE 'W%' AND ACTIVE_FLAG = 'Y' ORDER BY DIVISION_DESC ASC";

                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        DivSubdiv Divubdiv = new DivSubdiv();
                        Divubdiv.division = rdr["DIVISION"].ToString();
                        Divubdiv.division_desc = rdr["DIVISION_DESC"].ToString();
                        Divubdiv.sub_division = rdr["SUB_DIVISION"].ToString();
                        Divubdiv.sub_division_desc = rdr["SUB_DIVISION_DESC"].ToString();
                        Divubdiv.circle = rdr["CIRCLE"].ToString();
                        _DivSubdiv.Add(Divubdiv);
                        response1.DivSubdivisionList = _DivSubdiv;
                    }
                }
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT DISTINCT SUB_ACTIVITY,SUB_ACTIVITY_DESC,SUB_ACTIVITY_CODE,SUB_ACTIVITY_CODE_DESC";
                    sqlQuery += " FROM MOBINT.PM_SUBACTIVITY_MST WHERE ACTIVE_FLAG='Y' ORDER BY SUB_ACTIVITY ASC";

                    //sqlQuery = "SELECT DISTINCT ORDER_TYPE SUB_ACTIVITY, ORDER_DESC SUB_ACTIVITY_DESC,PM_ACTIVTY SUB_ACTIVITY_CODE, PM_DESC SUB_ACTIVITY_CODE_DESC";
                    //sqlQuery += " from mobint.MCR_ORDER_PM_MASTER WHERE ACTIVE_FLAG = 'Y' ORDER BY SUB_ACTIVITY ASC";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        ActivitySubactivity activtsubact = new ActivitySubactivity();
                        activtsubact.activityType = rdr["SUB_ACTIVITY"].ToString();
                        activtsubact.activityType_desc = rdr["SUB_ACTIVITY_DESC"].ToString();
                        activtsubact.subActivityType = rdr["SUB_ACTIVITY_CODE"].ToString();
                        activtsubact.subActivityType_desc = rdr["SUB_ACTIVITY_CODE_DESC"].ToString();
                        _ActivitySubactivity.Add(activtsubact);
                        response1.ActivitySubactivityList = _ActivitySubactivity;
                    }
                }
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT DISTINCT PREMISE,SUB_PREMISE FROM MOBINT.PM_PREMISE_MST  WHERE ACTIVE_FLAG='Y'";
                    sqlQuery += " ORDER BY PREMISE ASC";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        PMPREMISEMST pmremisemst = new PMPREMISEMST();
                        pmremisemst.premise = rdr["PREMISE"].ToString();
                        pmremisemst.sub_premise = rdr["SUB_PREMISE"].ToString();
                        _PMPREMISEMST.Add(pmremisemst);
                        response1.PremiseMst = _PMPREMISEMST;
                    }
                }
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT DISTINCT DEPARTMENT VENDOR_ID,VENDOR_NAME,CIRCLE,WORK_ORDERNO FROM MOBINT.PM_VENDOR_MASTER";
                    sqlQuery += " WHERE CIRCLE='SOUTH' OR CIRCLE='WEST' AND STATUS='A' ORDER BY VENDOR_NAME ASC";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        VENDORMST venormst = new VENDORMST();
                        venormst.vendor_id = rdr["VENDOR_ID"].ToString();
                        venormst.vendor_name = rdr["VENDOR_NAME"].ToString();
                        venormst.circle = rdr["CIRCLE"].ToString();
                        venormst.work_order_no = rdr["WORK_ORDERNO"].ToString();
                        _VENDORMST.Add(venormst);
                        response1.VendorMst = _VENDORMST;
                    }
                }
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT EMP_NO,EMPLOYEE_NAME  FROM MOBINT.PM_EMPLOYEE_MASTER WHERE  FLAG='Y'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        EmpolyeeDetails objemp = new EmpolyeeDetails();
                        objemp.Empid = rdr["EMP_NO"].ToString();
                        objemp.Employeename = rdr["EMPLOYEE_NAME"].ToString();
                        listemp.Add(objemp);
                        response1.Employee = listemp;
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response1.Status = "Failed";
                response1.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response1);
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            response1.Status = "Success";
            response1.Message = "200";
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                response1.Status,
                response1.Message,
                response1.DivSubdivisionList,
                response1.ActivitySubactivityList,
                response1.PremiseMst,
                response1.VendorMst,
                response1.Employee
            },
            JsonMediaTypeFormatter.DefaultMediaType);
        }

        [HttpPost]
        [Route("api/PowerMetering/OrderCancellation")]
        public HttpResponseMessage OrderCancellation([FromBody] OrderCancellation CancelDetails)
        {
            string Image1 = string.Empty;
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    if (!String.IsNullOrEmpty(CancelDetails.ImagePath))
                    {
                        byte[] _byImage = Convert.FromBase64String(CancelDetails.ImagePath);
                        Image1 = powerMeteringBL.byteArrayToImage(_byImage, CancelDetails.OrderNum + ".jpg");
                    }
                    sqlQuery = "INSERT INTO MCR_ORDER_CANCEL(INSTALLER_ID,ORDERID,IMAGE_PATH,REASON,REMARKS,CUST_NAME,CUST_NO,CUST_ENT_DATTIME)";
                    sqlQuery += " VALUES('" + CancelDetails.UserId.ToUpper() + "','" + CancelDetails.OrderNum.ToUpper() + "','" + Image1 + "',";
                    sqlQuery += "'" + CancelDetails.reason.ToUpper() + "','" + CancelDetails.Remarks.ToUpper() + "','" + CancelDetails.customerName.ToUpper() + "',";
                    sqlQuery += "'" + CancelDetails.customerNumber.ToUpper() + "','" + CancelDetails.custDate + "')";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        sqlQuery = "UPDATE MCR_INPUT_DETAILS SET FLAG='PE'  WHERE ORDERID=" + CancelDetails.OrderNum.ToUpper() + "";
                        cmd = new OleDbCommand(sqlQuery, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                    }
                    response.Status = "Success";
                    response.Message = "";
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
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        [HttpPost]
        [Route("API/PowerMetering/CancelRemarks")]
        public HttpResponseMessage CancelRemarks([FromBody] OrderCancellation CancelDetails)
        {
            int count = 0;
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            List<CancelRemarks> Canremarkslist = new List<CancelRemarks>();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT CANCEL_ID,CAN_REMARK FROM MCR_CANCEL_RMKS_MST WHERE STATUS='Y'";
                    sqlQuery += " AND ORDER_TYPE LIKE'%" + CancelDetails.Ordertype + "%'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        CancelRemarks objdetail = new CancelRemarks();
                        count = count + 1;
                        objdetail.Cancelid = rdr["CANCEL_ID"].ToString();
                        objdetail.Remarks = rdr["CAN_REMARK"].ToString();
                        Canremarkslist.Add(objdetail);
                        response.Data = Canremarkslist;
                        response.Status = "Success";
                        response.Message = "";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    return Msg;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        [HttpPost]
        [Route("API/PowerMetering/GetAllItems")]
        public IHttpActionResult GetAllItems([FromUri] int id)
        {
            var result = new
            {
                x = "hello",
                y = "world"
            };
            return Ok(result);
        }

        [Route("API/PowerMetering/ChangePassword")]
        [HttpPost]
        public HttpResponseMessage ChangePassword([FromBody] Change_Password passdetails)
        {
            string pwd = string.Empty;
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    DataTable dt = new DataTable();
                    sqlQuery = "SELECT USER_ID FROM PM_LOGIN WHERE UPPER(USER_ID) = UPPER('" + passdetails.User_ID + "') AND UPPER(PASSWORD) = UPPER('" + passdetails.Current_Password + "') AND STATUS='Y'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        sqlQuery = "UPDATE MOBINT.PM_LOGIN SET PASSWORD ='" + passdetails.New_Password.ToUpper() + "' WHERE USER_ID='" + passdetails.User_ID + "' AND STATUS='Y'";
                        cmd = new OleDbCommand(sqlQuery, con);
                        cmd.Connection = con;
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        int result = cmd.ExecuteNonQuery();
                        response.Status = "Success";
                        response.Message = "Record updated successfully";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.Status = "Failed";
                        response.Message = "UserID is not valid";
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
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Msg;
        }

        [HttpPost]
        [Route("api/PowerMetering/Enf_Insert_Data")] //Added New Method related to Enforcement PCN No. 709202006
        public HttpResponseMessage Enf_Insert_Data([FromBody] Enf_Insert_Data CADetails)
        {
            string Image1 = string.Empty;
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    if (!String.IsNullOrEmpty(CADetails.Image_Path))
                    {
                        byte[] _byImage = Convert.FromBase64String(CADetails.Image_Path);
                        Image1 = powerMeteringBL.byteArrayToImageEnf(_byImage, CADetails.CA_No);
                    }
                    sqlQuery = "INSERT INTO MOBINT.ENFORCEMNT_CA_DETAILS(DIVISION,CA_No,Consumer_Name,OCCUPANTCONT_NAME,OCCUPANTCONT_NO,EMP_NAME,EMP_NUM,NEARBYMETER_NO,Image_Path,Longitude,Latitude,Remarks,checkBoxInput,siteTraceable,TheftFounds,IMEI,PUNCH_ID)";
                    sqlQuery += " VALUES('" + CADetails.DIVISION + "','" + CADetails.CA_No + "','" + CADetails.Consumer_Name + "','" + CADetails.OCCUPANTCONT_NAME + "','" + CADetails.OCCUPANTCONT_NO + "','" + CADetails.EMP_NAME.ToUpper() + "','" + CADetails.EMP_NUM.ToUpper() + "','" + CADetails.NEARBYMETER_NO + "','" + Image1 + "',";
                    sqlQuery += "'" + CADetails.Longitude + "','" + CADetails.Latitude + "','" + CADetails.Remarks + "','" + CADetails.checkBoxInput + "','" + CADetails.siteTraceable + "','" + CADetails.TheftFounds + "','" + CADetails.IMEI + "',SEQ_ENFDATA.NEXTVAL||to_char(sysdate,'ddmmyyyy'))";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        response.Status = "Success";
                        response.Message = i + " Record Inserted Successfully.";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        return Msg;
                    }
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
                Msg = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return Msg;
        }
     
        [HttpPost]
        [Route("API/PowerMetering/Enf_Validate_CA")] //Added New Method related to Enforcement PCN No. 709202006
        public HttpResponseMessage Enf_Validate_CA([FromBody] ENF_VALIDATE_CA enf_CA)
        {
            List<ENF_VALIDATE_CA> Canremarkslist = new List<ENF_VALIDATE_CA>();
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            DataTable dt = new DataTable();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT CONTRACT_ACCOUNT_ENF FROM ENFORCEMNT_CA_MST WHERE ACTIVE='Y' AND CONTRACT_ACCOUNT_ENF='" + enf_CA.ENF_CA + "'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    DataTable dtcheck = new DataTable();
                    adp.Fill(dtcheck);
                    if (dtcheck.Rows.Count > 0)
                    {
                        sqlQuery = "SELECT M.COMPANY_CODE,M.CONTRACT_ACCOUNT_ENF,M.User_Name_Enf,M.DIVISION,M.Enforcement_Bill_No,C.OCCUPANTCONT_NAME,C.OCCUPANTCONT_NO,";
                        sqlQuery += " C.EMP_NAME,C.EMP_NUM,C.NEARBYMETER_NO,C.LATITUDE,C.LONGITUDE,C.REMARKS,C.CHECKBOXINPUT,C.SITETRACEABLE,C.THEFTFOUNDS,C.ENTRY_DATE FROM  ENFORCEMNT_CA_MST M";
                        sqlQuery += ",ENFORCEMNT_CA_Details C WHERE M.CONTRACT_ACCOUNT_ENF = C.CA_NO AND ACTIVE = 'Y' AND M.CONTRACT_ACCOUNT_ENF='" + enf_CA.ENF_CA + "' ORDER BY ENTRY_DATE DESC";
                        cmd = new OleDbCommand(sqlQuery, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                ENF_VALIDATE_CA objcase = new ENF_VALIDATE_CA();
                                objcase.Company_Code = dt.Rows[i]["COMPANY_CODE"].ToString();
                                objcase.ENF_CA = dt.Rows[i]["CONTRACT_ACCOUNT_ENF"].ToString();
                                objcase.CONSUMER_NAME = dt.Rows[i]["User_Name_Enf"].ToString();
                                objcase.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                objcase.Enforcement_Bill_No = dt.Rows[i]["Enforcement_Bill_No"].ToString();
                                objcase.OCCUPANTCONT_NAME = dt.Rows[i]["OCCUPANTCONT_NAME"].ToString();
                                objcase.OCCUPANTCONT_NO = dt.Rows[i]["OCCUPANTCONT_NO"].ToString();
                                objcase.EMP_NAME = dt.Rows[i]["EMP_NAME"].ToString();
                                objcase.EMP_NUM = dt.Rows[i]["EMP_NUM"].ToString();
                                objcase.NEARBYMETER_NO = dt.Rows[i]["NEARBYMETER_NO"].ToString();
                                objcase.Latitude = dt.Rows[i]["LATITUDE"].ToString();
                                objcase.Longitude = dt.Rows[i]["LONGITUDE"].ToString();
                                objcase.Remarks = dt.Rows[i]["REMARKS"].ToString();
                                objcase.checkBoxInput = dt.Rows[i]["CHECKBOXINPUT"].ToString();
                                objcase.siteTraceable = dt.Rows[i]["SITETRACEABLE"].ToString();
                                objcase.TheftFound = dt.Rows[i]["THEFTFOUNDS"].ToString();
                                objcase.Punch_Date = dt.Rows[i]["ENTRY_DATE"].ToString();
                                Canremarkslist.Add(objcase);
                                response.Data = Canremarkslist;
                                response.Status = "Success";
                                response.Message = "Old Data";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }
                        else
                        {
                            sqlQuery = "SELECT COMPANY_CODE,CONTRACT_ACCOUNT_ENF,User_Name_Enf,DIVISION,Enforcement_Bill_No FROM  ENFORCEMNT_CA_MST WHERE ACTIVE='Y' AND CONTRACT_ACCOUNT_ENF='" + enf_CA.ENF_CA + "'";
                            cmd = new OleDbCommand(sqlQuery, con);
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            adp = new OleDbDataAdapter(cmd);
                            DataTable dt1 = new DataTable();
                            adp.Fill(dt1);
                            if (dt1.Rows.Count > 0)
                            {
                                for (int m = 0; m < dt1.Rows.Count; m++)
                                {
                                    ENF_VALIDATE_CA objcase = new ENF_VALIDATE_CA();
                                    objcase.Company_Code = dt1.Rows[m]["COMPANY_CODE"].ToString();
                                    objcase.ENF_CA = dt1.Rows[m]["CONTRACT_ACCOUNT_ENF"].ToString();
                                    objcase.CONSUMER_NAME = dt1.Rows[m]["User_Name_Enf"].ToString();
                                    objcase.DIVISION = dt1.Rows[m]["DIVISION"].ToString();
                                    objcase.Enforcement_Bill_No = dt1.Rows[m]["Enforcement_Bill_No"].ToString();
                                    Canremarkslist.Add(objcase);
                                    response.Data = Canremarkslist;
                                    response.Status = "Success";
                                    response.Message = "New Data";
                                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                                }
                            }
                        }
                        return Msg;
                    }
                    else
                    {
                        response.Status = "Success";
                        response.Message = "Enforcement CA Number Not Found";
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
                response.Status = "Failed";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return Msg;
        }

        [HttpPost]
        [Route("API/PowerMetering/Enf_Validate_CA_NEW")] //Added New Method related to Enforcement PCN No. 709202006
        public HttpResponseMessage Enf_Validate_CA_NEW([FromBody] ENF_VALIDATE_CA enf_CA)
        {
            List<ENF_VALIDATE_CA> Canremarkslist = new List<ENF_VALIDATE_CA>();
            List<Emp_Data> Emplist = new List<Emp_Data>();
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage()     ;
            string connectionString = NDS.con();
            DataTable dt = new DataTable();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    //sqlQuery = "SELECT CONTRACT_ACCOUNT_ENF FROM ENFORCEMNT_CA_MST WHERE ACTIVE='Y' AND CONTRACT_ACCOUNT_ENF='" + enf_CA.ENF_CA + "'";
                    sqlQuery = "SELECT CONTRACT_ACCOUNT_ENF CONTRACT_ACCOUNT_ENF FROM ENFORCEMNT_CA_MST WHERE  CONTRACT_ACCOUNT_ENF = '" + enf_CA.ENF_CA + "'";
                    sqlQuery += " union ";
                    sqlQuery += " SELECT CA_NO CONTRACT_ACCOUNT_ENF from mobint.ENFORCEMNT_CA_Details where CA_NO = '" + enf_CA.ENF_CA + "'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    DataTable dtcheck = new DataTable();
                    adp.Fill(dtcheck);
                    if (dtcheck.Rows.Count > 0)
                    {
                        //sqlQuery = "SELECT M.COMPANY_CODE,M.CONTRACT_ACCOUNT_ENF,M.User_Name_Enf,M.DIVISION,M.Enforcement_Bill_No,C.OCCUPANTCONT_NAME,C.OCCUPANTCONT_NO,";
                        //sqlQuery += " C.EMP_NAME,C.EMP_NUM,C.NEARBYMETER_NO,C.LATITUDE,C.LONGITUDE,C.REMARKS,C.CHECKBOXINPUT,C.SITETRACEABLE,C.THEFTFOUNDS,C.ENTRY_DATE FROM  ENFORCEMNT_CA_MST M";
                        //sqlQuery += ",ENFORCEMNT_CA_Details C WHERE M.CONTRACT_ACCOUNT_ENF = C.CA_NO AND ACTIVE = 'Y' AND M.CONTRACT_ACCOUNT_ENF='" + enf_CA.ENF_CA + "' ORDER BY ENTRY_DATE DESC";

                        sqlQuery = "SELECT C.COMPANY COMPANY_CODE, C.CA_NO CONTRACT_ACCOUNT_ENF, M.User_Name_Enf,C.DIVISION DIVISION, M.Enforcement_Bill_No,C.OCCUPANTCONT_NAME,C.OCCUPANTCONT_NO,";
                        sqlQuery += "C.EMP_NAME,C.EMP_NUM,C.NEARBYMETER_NO,C.LATITUDE,C.LONGITUDE,C.REMARKS,C.CHECKBOXINPUT,C.SITETRACEABLE,C.THEFTFOUNDS,C.ENTRY_DATE FROM  mobint.ENFORCEMNT_CA_MST M";
                        sqlQuery += ", mobint.ENFORCEMNT_CA_Details C WHERE C.CA_NO = M.CONTRACT_ACCOUNT_ENF(+)  AND C.CA_NO = '" + enf_CA.ENF_CA + "' ORDER BY ENTRY_DATE DESC";
                        cmd = new OleDbCommand(sqlQuery, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                ENF_VALIDATE_CA objcase = new ENF_VALIDATE_CA();
                                objcase.Company_Code = dt.Rows[i]["COMPANY_CODE"].ToString();
                                objcase.ENF_CA = dt.Rows[i]["CONTRACT_ACCOUNT_ENF"].ToString();
                                objcase.CONSUMER_NAME = dt.Rows[i]["User_Name_Enf"].ToString();
                                objcase.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                objcase.Enforcement_Bill_No = dt.Rows[i]["Enforcement_Bill_No"].ToString();
                                objcase.OCCUPANTCONT_NAME = dt.Rows[i]["OCCUPANTCONT_NAME"].ToString();
                                objcase.OCCUPANTCONT_NO = dt.Rows[i]["OCCUPANTCONT_NO"].ToString();
                                objcase.EMP_NAME = dt.Rows[i]["EMP_NAME"].ToString();
                                objcase.EMP_NUM = dt.Rows[i]["EMP_NUM"].ToString();
                                objcase.NEARBYMETER_NO = dt.Rows[i]["NEARBYMETER_NO"].ToString();
                                objcase.Latitude = dt.Rows[i]["LATITUDE"].ToString();
                                objcase.Longitude = dt.Rows[i]["LONGITUDE"].ToString();
                                objcase.Remarks = dt.Rows[i]["REMARKS"].ToString();
                                objcase.checkBoxInput = dt.Rows[i]["CHECKBOXINPUT"].ToString();
                                objcase.siteTraceable = dt.Rows[i]["SITETRACEABLE"].ToString();
                                objcase.TheftFound = dt.Rows[i]["THEFTFOUNDS"].ToString();
                                objcase.Punch_Date = dt.Rows[i]["ENTRY_DATE"].ToString();
                                Canremarkslist.Add(objcase);
                                response2.Data = Canremarkslist;
                            }
                            sqlQuery = "select distinct  EMP_NAME,EMP_NUM from  ENFORCEMNT_CA_DETAILS";
                            cmd = new OleDbCommand(sqlQuery, con);
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            OleDbDataReader rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {

                                Emp_Data objgunnyseal = new Emp_Data();
                                objgunnyseal.Emp_Id = rdr["EMP_NUM"].ToString();
                                objgunnyseal.Emp_Name = rdr["EMP_NAME"].ToString();
                                Emplist.Add(objgunnyseal);
                                response2.EMP_Data = Emplist;
                            }
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                            response2.Status = "Success";
                            response2.Message = "Old Data";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, new
                            {
                                response2.Status,
                                response2.Message,
                                response2.Data,
                                response2.EMP_Data
                            }, JsonMediaTypeFormatter.DefaultMediaType);
                        }
                        else
                        {
                            sqlQuery = "SELECT COMPANY_CODE,CONTRACT_ACCOUNT_ENF,User_Name_Enf,DIVISION,Enforcement_Bill_No FROM  ENFORCEMNT_CA_MST WHERE ACTIVE='Y' AND CONTRACT_ACCOUNT_ENF='" + enf_CA.ENF_CA + "'";
                            cmd = new OleDbCommand(sqlQuery, con);
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            adp = new OleDbDataAdapter(cmd);
                            DataTable dt1 = new DataTable();
                            adp.Fill(dt1);
                            if (dt1.Rows.Count > 0)
                            {
                                for (int m = 0; m < dt1.Rows.Count; m++)
                                {
                                    ENF_VALIDATE_CA objcase = new ENF_VALIDATE_CA();
                                    objcase.Company_Code = dt1.Rows[m]["COMPANY_CODE"].ToString();
                                    objcase.ENF_CA = dt1.Rows[m]["CONTRACT_ACCOUNT_ENF"].ToString();
                                    objcase.CONSUMER_NAME = dt1.Rows[m]["User_Name_Enf"].ToString();
                                    objcase.DIVISION = dt1.Rows[m]["DIVISION"].ToString();
                                    objcase.Enforcement_Bill_No = dt1.Rows[m]["Enforcement_Bill_No"].ToString();
                                    Canremarkslist.Add(objcase);
                                    response2.Data = Canremarkslist;
                                }
                            }
                            sqlQuery = "select distinct  EMP_NAME,EMP_NUM from  ENFORCEMNT_CA_DETAILS";
                            cmd = new OleDbCommand(sqlQuery, con);
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            OleDbDataReader rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {

                                Emp_Data objgunnyseal = new Emp_Data();
                                objgunnyseal.Emp_Id = rdr["EMP_NUM"].ToString();
                                objgunnyseal.Emp_Name = rdr["EMP_NAME"].ToString();
                                Emplist.Add(objgunnyseal);
                                response2.EMP_Data = Emplist;
                            }
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            response2.Status = "Success";
                            response2.Message = "New Data";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, new
                            {
                                response2.Status,
                                response2.Message,
                                response2.Data,
                                response2.EMP_Data
                            }, JsonMediaTypeFormatter.DefaultMediaType);
                        }
                        return Msg;
                    }
                    else
                    {
                        response2.Status = "Success";
                        response2.Message = "Enforcement CA Number Not Found";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response2);
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response2.Status = "Failed";
                response2.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response2);
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return Msg;
        }

        [HttpPost]
        [Route("API/PowerMetering/Enf_App_Version")] //Added New Method related to Enforcement PCN No. 709202006
        public HttpResponseMessage Enf_App_Version([FromBody] Version_Data versiondetails)
        {
            int count = 0;
            List<Version_Data> versionlist = new List<Version_Data>();
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage() ;
            string connectionString = NDS.con();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT MACHINE_USER,VERSION FROM mobint.MCR_APP_VERSION WHERE MACHINE_USER='ENFORCEMENET' AND STATUS='Y'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Version_Data objversion = new Version_Data();
                        count = count + 1;
                        objversion.Module_Name = rdr["MACHINE_USER"].ToString();
                        objversion.Version = rdr["VERSION"].ToString();
                        versionlist.Add(objversion);
                        response.Data = versionlist;
                        response.Status = "Success";
                        response.Message = "All Data";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
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
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        [HttpPost]
        [Route("API/PowerMetering/Power_Theft_CA_Details")]
        public HttpResponseMessage Power_Theft_CA_Details([FromBody] Power_Theft_CA theftca)
        {
            List<Power_Theft_CA> Theftlist = new List<Power_Theft_CA>();
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                V2.WebService isu = new V2.WebService();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                if (theftca.Inputvalue.Length == 9 || theftca.Inputvalue.Length == 12)
                {
                    if (theftca.Inputvalue.Length < 12 && theftca.Inputvalue.Length > 0)
                    {
                        theftca.Inputvalue = theftca.Inputvalue.PadLeft(12, '0');
                    }
                    theftca.CA = theftca.Inputvalue;
                    dt = isu.Z_BAPI_CMS_ISU_CA_DISPLAY(theftca.CA, "", "", "", "", "").Tables[0];
                }
                if (theftca.Inputvalue.Length == 8 || theftca.Inputvalue.Length == 18)
                {
                    if (theftca.Inputvalue.Length < 18 && theftca.Inputvalue.Length > 0)
                    {
                        theftca.Inputvalue = theftca.Inputvalue.PadLeft(18, '0');
                    }
                    theftca.MeterNo = theftca.Inputvalue;
                    dt = isu.Z_BAPI_CMS_ISU_CA_DISPLAY("", theftca.MeterNo, "", "", "", "").Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    theftca.CA = dt.Rows[0]["Ca_Number"].ToString();
                    theftca.MeterNo = dt.Rows[0]["Device_Sr_Number"].ToString();
                    theftca.Name = dt.Rows[0]["Bp_Name"].ToString();
                    theftca.Division = dt.Rows[0]["Reg_str_group"].ToString();
                    theftca.Address = dt.Rows[0]["Street"].ToString() + " " + dt.Rows[0]["Street2"].ToString() + " " + dt.Rows[0]["Street3"].ToString() + " " + dt.Rows[0]["Street4"].ToString() + " " + dt.Rows[0]["City"].ToString() + " " + dt.Rows[0]["Post_Code"].ToString();
                    Theftlist.Add(theftca);
                    response.Data = Theftlist;
                    response.Status = "Success";
                    response.Message = "All Data";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    response.Status = "No data found!";
                    response.Message = "All Data";
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
                return Msg;
            }
            return Msg;
        }

        [HttpPost]
        [Route("API/PowerMetering/Z_BAPI_DSS_ISU_CA_DISPLAY")]
        public HttpResponseMessage Z_BAPI_DSS_ISU_CA_DISPLAY([FromBody] CA_Details theftca)
        {
            List<CA_Details> Theftlist = new List<CA_Details>();
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                V2.WebService isu = new V2.WebService();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                if (theftca.CA.Length == 9 || theftca.CA.Length == 12)
                {
                    if (theftca.CA.Length < 12 && theftca.CA.Length > 0)
                    {
                        theftca.CA = theftca.CA.PadLeft(12, '0');
                    }
                    //theftca.CA = theftca.Inputvalue;
                    dt = isu.Z_BAPI_CMS_ISU_CA_DISPLAY(theftca.CA, "", "", "", "", "").Tables[0];
                }
                //if (theftca.MeterNo.Length == 8 || theftca.MeterNo.Length == 18)
                //{
                //    if (theftca.MeterNo.Length < 18 && theftca.MeterNo.Length > 0)
                //    {
                //        theftca.MeterNo = theftca.MeterNo.PadLeft(18, '0');
                //    }
                //    theftca.MeterNo = theftca.Inputvalue;
                //    dt = isu.Z_BAPI_CMS_ISU_CA_DISPLAY("", theftca.MeterNo, "", "", "", "").Tables[0];
                //}
                if (dt.Rows.Count > 0)
                {
                    theftca.CA = dt.Rows[0]["Ca_Number"].ToString();
                    theftca.MeterNo = dt.Rows[0]["Device_Sr_Number"].ToString();
                    theftca.FName = dt.Rows[0]["Bp_Name"].ToString();
                    theftca.Division = dt.Rows[0]["Reg_str_group"].ToString();
                    theftca.Bill_Class = dt.Rows[0]["Bill_Class"].ToString();
                    theftca.Email = dt.Rows[0]["E_Mail"].ToString();
                    theftca.LName = dt.Rows[0]["Bp_Name"].ToString();
                    theftca.Mobile = dt.Rows[0]["Tel1_Number"].ToString();
                    theftca.Pin = dt.Rows[0]["Post_Code"].ToString();
                    theftca.Tarriff_Cat = dt.Rows[0]["Rate_Cat"].ToString();

                    theftca.Address = dt.Rows[0]["Street"].ToString() + " " + dt.Rows[0]["Street2"].ToString() + " " + dt.Rows[0]["Street3"].ToString() + " " + dt.Rows[0]["Street4"].ToString() + " " + dt.Rows[0]["City"].ToString() + " " + dt.Rows[0]["Post_Code"].ToString();
                    Theftlist.Add(theftca);
                    response.Data = Theftlist;
                    response.Status = "Success";
                    response.Message = "All Data";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    response.Status = "No data found!";
                    response.Message = "All Data";
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
                return Msg;
            }
            return Msg;
        }

        [HttpPost]
        [Route("api/PowerMetering/Insert_Power_Data")]
        public HttpResponseMessage Insert_Power_Data([FromBody] Insert_Power_Theft Details)
        {
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "INSERT INTO MOBINT.Power_Theft_Details(Area,Division,SearchBy,OffenderName,OffenderAddress,NatureOfTheft,Load,Usage,TheftDescription,Landmark,SendPersonalDetail,Name,Address,Mobile,Email,POWER_ID,lat,longi)";
                    sqlQuery += " VALUES('" + Details.Area + "','" + Details.Division + "','" + Details.SearchBy + "','" + Details.OffenderName + "','" + Details.OffenderAddress + "','" + Details.NatureOfTheft + "','" + Details.Load + "','" + Details.Usage + "',";
                    sqlQuery += "'" + Details.TheftDescription + "','" + Details.Landmark + "','" + Details.SendPersonalDetail + "','" + Details.Name + "','" + Details.Address + "','" + Details.Mobile + "','" + Details.Email + "',POWER_THEFT.NEXTVAL,'" + Details.Lat + "','" + Details.Long + "')";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        response.Status = "Success";
                        response.Message = i + " Record Inserted Successfully.";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        return Msg;
                    }
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
                Msg = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return Msg;
        }

        [HttpPost]
        [Route("API/PowerMetering/Theft_Area_List")]
        public HttpResponseMessage Theft_Area_List([FromBody] Area_List areadetail)
        {
            int count = 0;
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            List<Area_List> arealist = new List<Area_List>();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "select DISTINCT LOCALITY_NAME AREA_NAME from AREA_MASTER WHERE ACTIVE='A'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Area_List objarea = new Area_List();
                        count = count + 1;
                        objarea.AREA_NAME = rdr["AREA_NAME"].ToString();
                        arealist.Add(objarea);
                        response.Data = arealist;
                        response.Status = "Success";
                        response.Message = "All Data";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
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
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        [HttpPost]
        [Route("API/PowerMetering/Theft_Area_Division")]
        public HttpResponseMessage Theft_Area_Division([FromBody] Area_Division areadivdetail)
        {
            int count = 0;
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            List<Area_Division> areadivlist = new List<Area_Division>();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "select DIV_CODE,DIVISION from AREA_MASTER WHERE ACTIVE='A' AND UPPER(LOCALITY_NAME)=UPPER('" + areadivdetail.AREA_NAME + "')";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Area_Division objareadiv = new Area_Division();
                        count = count + 1;
                        objareadiv.DIV_CODE = rdr["DIV_CODE"].ToString();
                        objareadiv.DIVISION = rdr["DIVISION"].ToString();
                        areadivlist.Add(objareadiv);
                        response.Data = areadivlist;
                        response.Status = "Success";
                        response.Message = "All Data";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
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
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        [HttpPost]
        [Route("API/PowerMetering/Theft_Area_List_New")]
        public HttpResponseMessage Theft_Area_List_New([FromBody] Area_List areadetail)
        {
            int count = 0;
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            List<Area_List> arealist = new List<Area_List>();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "select DISTINCT BRPL_LOCALITY_NAME AREA_NAME from BRPL_LOCALITY_MST WHERE ACTIVE='A'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Area_List objarea = new Area_List();
                        count = count + 1;
                        objarea.AREA_NAME = rdr["AREA_NAME"].ToString();
                        arealist.Add(objarea);
                        response.Data = arealist;
                        response.Status = "Success";
                        response.Message = "All Data";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
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
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        [HttpPost]
        [Route("API/PowerMetering/Theft_Area_Division_New")]
        public HttpResponseMessage Theft_Area_Division_New([FromBody] Area_Division areadivdetail)
        {
            int count = 0;
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            List<Area_Division> areadivlist = new List<Area_Division>();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "select DIVISION_CODE DIV_CODE,DIVISION_NAME DIVISION from BRPL_LOCALITY_MST WHERE ACTIVE='A' AND UPPER(BRPL_LOCALITY_NAME)=UPPER('" + areadivdetail.AREA_NAME + "')";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Area_Division objareadiv = new Area_Division();
                        count = count + 1;
                        objareadiv.DIV_CODE = rdr["DIV_CODE"].ToString();
                        objareadiv.DIVISION = rdr["DIVISION"].ToString();
                        areadivlist.Add(objareadiv);
                        response.Data = areadivlist;
                        response.Status = "Success";
                        response.Message = "All Data";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
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
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        [HttpPost]
        [Route("api/PowerMetering/EnactStatus_Details")]
        public HttpResponseMessage EnactStatus_Details([FromBody] EnactStatus_Return statusEnact)
        {
            //int count = 0;
            List<EnactStatus_Return> statusEnactList = new List<EnactStatus_Return>();
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            string connectionStringSqlServer = NDS.conSqlServer();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (var WebClient = new WebClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var request = (HttpWebRequest)WebRequest.Create("https://envisionapi.enact-systems.com/leads");
                    WebProxy wp1 = new WebProxy("10.125.64.134:3128");
                    request.Proxy = wp1;
                    request.Accept = "application/json";
                    request.Method = "POST";
                    //LeadSubmit objLead = new LeadSubmit();
                    //objLead.ca_number = CA;

                    var JSONString = new StringBuilder();
                    JSONString.Append("{ \n");
                    JSONString.Append("\"" + "filters" + "\":");
                    JSONString.Append("{\n");
                    JSONString.Append("\"" + "custom" + "\":" + "{" + "\n");
                    JSONString.Append("\"" + "nm_number" + "\":" + "\"" + statusEnact.NM_Number_Input + "\"" + "\n");
                    JSONString.Append("}");
                    JSONString.Append("}\n");
                    JSONString.Append("}");

                    var myContent = JSONString.ToString();       //"{\" "filters":{ "custom": { "ca_number":  "152761947" } }\"}" ;//jss_up.Serialize(objLead);
                    var data = Encoding.ASCII.GetBytes(myContent);
                    request.ContentType = "application/json";
                    request.Headers.Add("x-api-key", "u1KHrqWTTL6rmA0D95kQb1t6yRB0NWbh4AwQvJnr");

                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    WebResponse response = request.GetResponse();
                    Stream datastream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(datastream);
                    string responseFromServer = reader.ReadToEnd();
                    LeadDataCollection leadcollection = JsonConvert.DeserializeObject<LeadDataCollection>(responseFromServer);
                    string Status = ((HttpWebResponse)response).StatusDescription;
                    string Response_string = ((HttpWebResponse)response).ToString();
                    int message = (int)((HttpWebResponse)response).StatusCode;
                    string stat = ((HttpWebResponse)response).Headers.ToString();

                    if (message == 200)
                    {
                        int count = leadcollection.data.Count;
                        //ddlNm.Items.Clear();
                        EnactStatus_Return objEnactStatus_Return = new EnactStatus_Return();
                        if (count > 0)
                        {

                            using (con1 = new SqlConnection(connectionStringSqlServer))
                            {
                                sqlQuery = "select * from EnactLeadStatus where status_api_Res='" + statusEnact.Enact_API_Res_Input + "'";

                                cmd1 = new SqlCommand(sqlQuery, con1);
                                if (con1.State == ConnectionState.Closed)
                                {
                                    con1.Open();
                                }
                                SqlDataReader rdr = cmd1.ExecuteReader();
                                while (rdr.Read())
                                {
                                    CancelRemarks objdetail = new CancelRemarks();
                                    count = count + 1;
                                    objEnactStatus_Return.Enact_API_Res = rdr["Status_api_Res"].ToString();
                                    objEnactStatus_Return.Enact_API_Res_Input = statusEnact.Enact_API_Res_Input;
                                    objEnactStatus_Return.NM_Number = leadcollection.data[0].nm_number.ToString();
                                    objEnactStatus_Return.NM_Number_Input = statusEnact.NM_Number_Input.ToString();
                                    objEnactStatus_Return.Next_Step = rdr["Status_nextStep"].ToString();
                                    objEnactStatus_Return.Remarks = rdr["Remarks"].ToString();

                                    string sa2 = "";
                                    //sa2 = @"""/Date(" + leadcollection.data[0].last_update.ToString() + @")/""";
                                    string Status_ConsuApp = string.Empty;
                                    string Status_ConsuApp_Fnl = string.Empty;
                                    Status_ConsuApp = rdr["Status_ConsuApp"].ToString();
                                    if (leadcollection.data[0].task_completed.ToString().Length > 4)
                                    {
                                        sa2 = @"""/Date(" + leadcollection.data[0].task_completed.ToString() + @")/""";

                                        DateTime dt = new DateTime();
                                        dt = JsonConvert.DeserializeObject<DateTime>(sa2);
                                        // dt = "2014-08-28 3.00.00 PM"
                                        Status_ConsuApp_Fnl = string.Format(Status_ConsuApp, dt.ToString("dd-MMM-yyyy"));
                                        objEnactStatus_Return.LastUpdated_Date = dt.ToString("dd-MMM-yyyy");
                                    }
                                    else
                                    {
                                        Status_ConsuApp_Fnl = Status_ConsuApp;
                                        objEnactStatus_Return.LastUpdated_Date = "";
                                    }



                                    objEnactStatus_Return.Status_Consu = Status_ConsuApp_Fnl;

                                    // objEnactStatus_Return.Status_Consu = rdr["Status_ConsuApp"].ToString();

                                    statusEnactList.Add(objEnactStatus_Return);

                                }

                            }


                            //return Msg;

                        }

                    }
                }
                response.Data = statusEnactList;
                response.Status = "Success";
                response.Message = "";
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                return Msg;
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                return Msg;
            }
            finally
            {
                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }
            }
        }

        #region "Hooter WebAPI"       

        [Route("API/PowerMetering/HooterLogin")]
        [HttpPost]
        public HttpResponseMessage HooterLogin([FromBody] HooterLogindetails login)
        {
            int count = 0;
            //Logindetails logins = new Logindetails();
            List<UserDetails> _UserDetails = new List<UserDetails>();
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT L.USER_ID, USER_NAME, FIRST_PERSON_MOBILE L1_MOBILE FROM MOBINT.HOOTER_LOGIN L, mobint.ENF_HOOTER_LOGIN_MST D WHERE L.USER_ID=D.EMP_ID and  L.USER_ID = '" + login.userID.ToUpper() + "'";
                    sqlQuery += " AND L.PASSWORD = '" + login.password.ToString() + "' AND L.STATUS = 'Y' AND ROLL_TYPE='HA'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        UserDetails userdetail = new UserDetails();
                        count = count + 1;
                        userdetail.userID = rdr["USER_ID"].ToString();
                        userdetail.userName = rdr["USER_NAME"].ToString();
                        userdetail.L1_Mobile = rdr["L1_MOBILE"].ToString();
                        userdetail.JwtToken = jwtTokenGenerator.GenerateToken(Convert.ToString(rdr["USER_ID"]), "Admin");
                        _UserDetails.Add(userdetail);
                        response.Data = _UserDetails;
                        response.Status = "Success";
                        response.Message = "User is valid";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        return Msg;
                    }
                }
                response.Status = "Failed";
                response.Message = "Userid or password is not valid";
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                return Msg;
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
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        [Route("API/PowerMetering/HooterLogin_01122023")]
        [HttpPost]
        public HttpResponseMessage HooterLogin_01122023([FromBody] HooterLogindetails login)
        {
            int count = 0;
            //Logindetails logins = new Logindetails();
            List<UserDetails> _UserDetails = new List<UserDetails>();
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "SELECT L.USER_ID, USER_NAME, FIRST_PERSON_MOBILE L1_MOBILE FROM MOBINT.HOOTER_LOGIN L, mobint.ENF_HOOTER_LOGIN_MST D WHERE L.USER_ID=D.EMP_ID and  L.USER_ID = '" + login.userID.ToUpper() + "'";
                    sqlQuery += " AND L.PASSWORD = '" + login.password.ToString() + "' AND L.STATUS = 'Y' AND ROLL_TYPE='HA'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        UserDetails userdetail = new UserDetails();
                        count = count + 1;
                        userdetail.userID = rdr["USER_ID"].ToString();
                        userdetail.userName = rdr["USER_NAME"].ToString();
                        userdetail.L1_Mobile = rdr["L1_MOBILE"].ToString();
                        userdetail.JwtToken = jwtTokenGenerator.GenerateToken(Convert.ToString(rdr["USER_ID"]), "Admin");
                        _UserDetails.Add(userdetail);
                        response.Data = _UserDetails;
                        response.Status = "Success";
                        response.Message = "User is valid";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        return Msg;
                    }
                }
                response.Status = "Failed";
                response.Message = "Userid or password is not valid";
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                return Msg;
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
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        [HttpPost]
        [Route("api/PowerMetering/Insert_Hooter_Data")]
        public HttpResponseMessage Insert_Hooter_Data([FromBody] Insert_Hooter_Data Details)
        {
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "INSERT INTO MOBINT.HOOTER_ENF_DETAILS(DIVISION,CA_NO,METER_NO,CONSUMER_NAME,CONSUMER_ADDRESS,CONSUMER_MOB,LONGITUDE,LATITUDE,REMARKS,IMEI,PUNCH_ID)";
                    sqlQuery += " VALUES('" + Details.Division + "','" + Details.CA_Number + "','"+Details.Meter_No+"','" + Details.Consumer_Name + "','" + Details.Consumer_Address + "',";
                    sqlQuery += " '" + Details.Consumer_Mob + "','" + Details.Logitude + "','" + Details.Latitude + "','" + Details.Remarks.Trim().Replace("'", "''") + "',";
                    sqlQuery += " '" + Details.IMEI + "','" + Details.Punch_ID + "')";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        // GetHooter_SMS(Details.Punch_ID, Details.Consumer_Address);
                        powerMeteringBL.GetHooter_SMS(Details.Punch_ID, Details.Consumer_Address, Details.Logitude, Details.Latitude);
                        response.Status = "Success";
                        response.Message = i + " Record Inserted Successfully.";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        return Msg;
                    }
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
                Msg = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return Msg;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return Msg;
        }

        #endregion

        #region"BSES API"

        [HttpPost]
        [Route("api/BSES/SEND_BSES_SMS_API")]
        public HttpResponseMessage SEND_BSES_SMS_API([FromBody] BSES_SMS_API_DATA SMS_API)
        {
            HttpResponseMessage Msg = new HttpResponseMessage();
            try
            {
                if (!authentication.Validate()) throw new UnauthorizedAccessException("");
                string apiUrl = string.Empty; //!!B$E$@@*SMS
                string strResFlg = string.Empty;
                string strRes = string.Empty;
                string ResponseText = string.Empty;
                string Number = SMS_API.MobileNo.Length == 10 ? "91" + SMS_API.MobileNo.Trim() : SMS_API.MobileNo.Trim();
                string Message = string.Empty;

                if (SMS_API.OTPMsg.Length < 7)
                    Message = SMS_API.OTPMsg + " is your one time password (OTP). Please enter the OTP to proceed. Team BRPL";
                else
                    Message = SMS_API.OTPMsg;

                DataTable _dtSMSValidity = powerMeteringBL.CheckSMS_Validity(SMS_API.EncryptionKey, SMS_API.CompanyCode);

                if (Number.Length != 12)
                {                    
                    response.Status = "F";
                    response.Message = "Invalid Mobile Number";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else if (_dtSMSValidity.Rows.Count == 0)
                {                    
                    response.Status = "F";
                    response.Message = "Key Mismatch";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    apiUrl = _dtSMSValidity.Rows[0][0].ToString();
                    if (SMS_API.CompanyCode.Trim() == "BRPL")
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
                            ResponseText = powerMeteringBL.ParseHttpWebResponse(resp);
                            response.Status = "S";
                            response.Message = "SUCCESS";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                        else
                        {
                            ResponseText = "N";                            
                            response.Status = "F";
                            response.Message = "FAIL (SMS API is not Working)";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
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
                            ResponseText = powerMeteringBL.ParseHttpWebResponse(resp);                            
                            response.Status = "S";
                            response.Message = "SUCCESS";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                        else
                        {
                            ResponseText = "N";                           
                            response.Status = "F";
                            response.Message = "FAIL (SMS API is not Working)";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }

                    if (SMS_API.CompanyCode.Trim() == "BRPL")
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
                                powerMeteringBL.Insert_MessageLog(Message, Number.ToString(), "OTP", SMS_API.CompanyCode, "Y",
                                responseId, responseCode, responseStatus, System.DateTime.Now.ToString(), SMS_API.VendorCode.ToString(), SMS_API.AppName.ToString(),
                                 SMS_API.EmpCode.ToString());
                            }
                            else
                            {
                                powerMeteringBL.Insert_MessageLog(Message, Number.ToString(), "OTP", SMS_API.CompanyCode, "X", "", "", "", "",
                                                        SMS_API.VendorCode.ToString(), SMS_API.AppName.ToString(), SMS_API.EmpCode.ToString());
                            }
                        }
                        else
                        {
                            powerMeteringBL.Insert_MessageLog(Message, Number.ToString(), "OTP", SMS_API.CompanyCode, "X", "", "", "", "",
                                                        SMS_API.VendorCode.ToString(), SMS_API.AppName.ToString(), SMS_API.EmpCode.ToString());
                        }
                    }
                }
                               
                return Msg;
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = ex.Message.ToString();
                Msg = Request.CreateResponse(HttpStatusCode.OK, response);
                return Msg;
            }
            finally
            {
              
            }
        }

        #endregion

    }
}
