using System.Data.OleDb;
using System.Data;
using System;
using Recovery_Webservice.Models;
using System.Net.Http;
using System.Net;
using System.IO;

namespace Recovery_Webservice.BusinessLogicLayer
{
    public class RecoveryBL
    {
        HttpResponseMessage Msg;
        response response = new response();
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataAdapter adp;
        string sql = string.Empty;
        string strConnection= string.Empty;
        public RecoveryBL()
        {
            string strConnection = NDS.con();
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
        internal DataTable ChkUpdatedate()
        {
            string sql = "";
            sql = "select A_DATE.dt, b_date.UDT from (select TO_CHAR(SYSDATE, 'yyyyMM')DT from dual) A_DATE,(select max(UPDATION_ID_DTLS) UDT from recovery.DEFLTR_CA_ASSIGN_LINK_DTLS) B_DATE";
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                DataTable dt = new DataTable();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmd.Connection = con;
                cmd = new OleDbCommand(sql, con);
                oda = new OleDbDataAdapter(cmd);
                oda.Fill(dt);
                return dt;
            }
        }
        internal DataTable ChkRecordExist(string _sCANumber, string _sFEID, string _sChkInTable)
        {
            string sql = "";
            if (_sChkInTable == "DEFLTR_ATR")
                sql = "SELECT * FROM recovery.DEFLTR_ATR WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                DataTable dt = new DataTable();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmd.Connection = con;
                cmd = new OleDbCommand(sql, con);
                oda = new OleDbDataAdapter(cmd);
                oda.Fill(dt);
                return dt;
            }
        }
        internal DataTable ChkRecordExist1(string _sCANumber, string _sFEID, string _sChkInTable)
        {
            string sql = "";
            if (_sChkInTable == "DEFLTR_ATR")
                sql = "SELECT * FROM recovery.DEFLTR_ATR WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                DataTable dt = new DataTable();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmd.Connection = con;
                cmd = new OleDbCommand(sql, con);
                oda = new OleDbDataAdapter(cmd);
                oda.Fill(dt);
                return dt;
            }
        }
        internal bool InsertTable(string _sCANumber, string _sStatus, string _sFEID, string _sIPAddr, string _sInsInTable)
        {
            string sql = "";
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                if (_sInsInTable == "DEFLTR_ATR")
                {
                    sql = "INSERT INTO recovery.DEFLTR_ATR (CA_NUMBER, UPDATION_ID, ASSIGN_USER_CODE, STATUS, ASSIGN_BY, TAB_ENTRY_DATE, TAB_UPD_BY, TAB_IPADDR)";
                    sql += " SELECT UNIQUE CA_NUMBER,UPDATION_ID_DTLS,ASSIGN_USER_CODE,'" + _sStatus + "' STATUS,ASSIGN_BY,SYSDATE,'" + _sFEID + "' TAB_UPD_BY,'" + _sIPAddr + "' TAB_IP";
                    sql += " FROM DEFLTR_CA_ASSIGN_LINK_DTLS WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    OleDbCommand cmd = new OleDbCommand();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmd.Connection = con;
                    cmd = new OleDbCommand(sql, con);
                    oda = new OleDbDataAdapter(cmd);
                    oda.SelectCommand.CommandText = "INSERT INTO DEFLTR_ATR (CA_NUMBER, UPDATION_ID, ASSIGN_USER_CODE, STATUS, ASSIGN_BY, TAB_ENTRY_DATE, TAB_UPD_BY, TAB_IPADDR) SELECT UNIQUE CA_NUMBER,UPDATION_ID_DTLS,ASSIGN_USER_CODE,'" + _sStatus + "' STATUS,ASSIGN_BY,SYSDATE,'" + _sFEID + "' TAB_UPD_BY,'" + _sIPAddr + "' TAB_IP FROM DEFLTR_CA_ASSIGN_LINK_DTLS WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    dt.TableName = "DEFLTR_ATR";
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }
        internal bool InsertTable1(string _sCANumber, string _sStatus, string _sFEID, string _sIPAddr, string _sInsInTable)
        {
            string sql = "";
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                if (_sInsInTable == "DEFLTR_ATR")
                {
                    sql = "INSERT INTO recovery.DEFLTR_ATR (CA_NUMBER, UPDATION_ID, ASSIGN_USER_CODE, STATUS, ASSIGN_BY, TAB_ENTRY_DATE, TAB_UPD_BY, TAB_IPADDR)";
                    sql += " SELECT UNIQUE CA_NUMBER,UPDATION_ID_DTLS,ASSIGN_USER_CODE,'" + _sStatus + "' STATUS,ASSIGN_BY,SYSDATE,'" + _sFEID + "' TAB_UPD_BY,'" + _sIPAddr + "' TAB_IP";
                    sql += " FROM DEFLTR_CA_ASSIGN_LINK_DTLS WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    OleDbCommand cmd = new OleDbCommand();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmd.Connection = con;
                    cmd = new OleDbCommand(sql, con);
                    oda = new OleDbDataAdapter(cmd);
                    oda.SelectCommand.CommandText = "INSERT INTO DEFLTR_ATR (CA_NUMBER, UPDATION_ID, ASSIGN_USER_CODE, STATUS, ASSIGN_BY, TAB_ENTRY_DATE, TAB_UPD_BY, TAB_IPADDR) SELECT UNIQUE CA_NUMBER,UPDATION_ID_DTLS,ASSIGN_USER_CODE,'" + _sStatus + "' STATUS,ASSIGN_BY,SYSDATE,'" + _sFEID + "' TAB_UPD_BY,'" + _sIPAddr + "' TAB_IP FROM DEFLTR_CA_ASSIGN_LINK_DTLS WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    dt.TableName = "DEFLTR_ATR";
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }
        internal bool InsertSMS(string _sMobileNo, string _sMessage, string _sCANumber, string _sUpdationID, string _sIPAddress)
        {
            string sql = "";
            sql = "INSERT INTO RECAPP_SMS (MOBILE_NO, MESSAGE_TXT, IP_ADDRESS, CA_NUMBER, UPDATION_ID)";
            sql += " VALUES ('" + _sMobileNo + "','" + _sMessage + "','" + _sIPAddress + "','" + _sCANumber + "','" + _sUpdationID + "')";

            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                DataTable dt = new DataTable();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmd.Connection = con;
                cmd = new OleDbCommand(sql, con);
                oda = new OleDbDataAdapter(cmd);
                oda.SelectCommand.CommandText = "INSERT INTO RECAPP_SMS (MOBILE_NO, MESSAGE_TXT, IP_ADDRESS, CA_NUMBER, UPDATION_ID) VALUES ('" + _sMobileNo + "','" + _sMessage + "','" + _sIPAddress + "','" + _sCANumber + "','" + _sUpdationID + "')";
                dt.TableName = "RECAPP_SMS";
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        internal bool UpdateTable(string _sCANumber, string _sStatus, string _sFEID, string _sIPAddr, string _sUpdInTable, string _sTabUpdStatus, string _sRemarks,
                              string _sImgFlg, string _sImg, string _sImage, string _sFollowDate, string _sAltContNo, string _sAltEmailID)

        {
            string sql = "";
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                if (_sUpdInTable == "DEFLTR_ATR")
                {
                    sql = "UPDATE DEFLTR_ATR SET STATUS = '" + _sStatus + "',TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "'";
                    sql += " WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";

                    OleDbCommand cmd = new OleDbCommand();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmd.Connection = con;
                    cmd = new OleDbCommand(sql, con);
                    oda = new OleDbDataAdapter(cmd);
                    oda.SelectCommand.CommandText = "UPDATE DEFLTR_ATR SET STATUS = '" + _sStatus + "', TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "' WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    dt.TableName = "DEFLTR_ATR";
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else if (_sUpdInTable == "DEFLTR_CA_ASSIGN_LINK_DTLS")
                {
                    try
                    {
                        if (_sImgFlg == "Y")
                        {
                            byte[] _byImg = Convert.FromBase64String(_sImg);
                            _sImg = byteArrayToImage_Rec(_byImg, _sCANumber + DateTime.Now.ToString("yyyyMMdd"));

                            byte[] _byImg1 = Convert.FromBase64String(_sImage);
                            _sImage = byteArrayToImage_Rec(_byImg1, _sCANumber + "_1" + DateTime.Now.ToString("yyyyMMdd"));

                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    sql = "UPDATE DEFLTR_CA_ASSIGN_LINK_DTLS SET STATUS = '" + _sStatus + "',TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "',";
                    sql += " TAB_UPD_STATUS = '" + _sTabUpdStatus + "', STATUS_REMARKS = '" + _sRemarks + "', TAB_IMAGE_FLG = '" + _sImgFlg + "', TAB_IMAGE_PATH = '" + _sImg + "',TAB_IMAGE_PATH1 = '" + _sImage + "',";
                    sql += " FOLLOWUP_DATE = TO_DATE('" + _sFollowDate + "','dd/MM/yyyy'), ALT_CONTACTNO = '" + _sAltContNo + "', ALT_EMAILID = '" + _sAltEmailID + "', TAB_FLAG = 'Y'";
                    sql += " WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";

                    OleDbCommand cmd = new OleDbCommand();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmd.Connection = con;
                    cmd = new OleDbCommand(sql, con);
                    oda = new OleDbDataAdapter(cmd);
                    oda.SelectCommand.CommandText = "UPDATE DEFLTR_CA_ASSIGN_LINK_DTLS SET STATUS = '" + _sStatus + "', TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "', TAB_UPD_STATUS = '" + _sTabUpdStatus + "', STATUS_REMARKS = '" + _sRemarks + "', TAB_IMAGE_FLG = '" + _sImgFlg + "', TAB_IMAGE_PATH = '" + _sImg + "',TAB_IMAGE_PATH1 = '" + _sImage + "', FOLLOWUP_DATE = TO_DATE('" + _sFollowDate + "','dd/MM/yyyy'), ALT_CONTACTNO = '" + _sAltContNo + "', ALT_EMAILID = '" + _sAltEmailID + "', TAB_FLAG = 'Y' WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    dt.TableName = "DEFLTR_CA_ASSIGN_LINK_DTLS";
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            con.Close();
            return true;
        }
        internal bool UpdateTable1(string _sCANumber, string _sStatus, string _sFEID, string _sIPAddr, string _sUpdInTable, string _sTabUpdStatus, string _sRemarks,
                             string _sImgFlg, string _sImg, string _sImage, string _sFollowDate, string _sAltContNo, string _sAltEmailID)

        {
            string sql = "";
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                if (_sUpdInTable == "DEFLTR_ATR")
                {
                    sql = "UPDATE DEFLTR_ATR SET STATUS = '" + _sStatus + "',TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "'";
                    sql += " WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";

                    OleDbCommand cmd = new OleDbCommand();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmd.Connection = con;
                    cmd = new OleDbCommand(sql, con);
                    oda = new OleDbDataAdapter(cmd);
                    oda.SelectCommand.CommandText = "UPDATE DEFLTR_ATR SET STATUS = '" + _sStatus + "', TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "' WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    dt.TableName = "DEFLTR_ATR";
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else if (_sUpdInTable == "DEFLTR_CA_ASSIGN_LINK_DTLS")
                {
                    try
                    {
                        if (_sImgFlg == "Y")
                        {
                            byte[] _byImg = Convert.FromBase64String(_sImg);
                            _sImg = byteArrayToImage_Rec(_byImg, _sCANumber + DateTime.Now.ToString("yyyyMMdd"));

                            byte[] _byImg1 = Convert.FromBase64String(_sImage);
                            _sImage = byteArrayToImage_Rec(_byImg1, _sCANumber + "_1" + DateTime.Now.ToString("yyyyMMdd"));

                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    sql = "UPDATE DEFLTR_CA_ASSIGN_LINK_DTLS SET STATUS = '" + _sStatus + "',TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "',";
                    sql += " TAB_UPD_STATUS = '" + _sTabUpdStatus + "', STATUS_REMARKS = '" + _sRemarks + "', TAB_IMAGE_FLG = '" + _sImgFlg + "', TAB_IMAGE_PATH = '" + _sImg + "',TAB_IMAGE_PATH1 = '" + _sImage + "',";
                    sql += " FOLLOWUP_DATE = TO_DATE('" + _sFollowDate + "','dd/MM/yyyy'), ALT_CONTACTNO = '" + _sAltContNo + "', ALT_EMAILID = '" + _sAltEmailID + "', TAB_FLAG = 'Y'";
                    sql += " WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    OleDbCommand cmd = new OleDbCommand();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmd.Connection = con;
                    cmd = new OleDbCommand(sql, con);
                    oda = new OleDbDataAdapter(cmd);
                    oda.SelectCommand.CommandText = "UPDATE DEFLTR_CA_ASSIGN_LINK_DTLS SET STATUS = '" + _sStatus + "', TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "', TAB_UPD_STATUS = '" + _sTabUpdStatus + "', STATUS_REMARKS = '" + _sRemarks + "', TAB_IMAGE_FLG = '" + _sImgFlg + "', TAB_IMAGE_PATH = '" + _sImg + "',TAB_IMAGE_PATH1 = '" + _sImage + "', FOLLOWUP_DATE = TO_DATE('" + _sFollowDate + "','dd/MM/yyyy'), ALT_CONTACTNO = '" + _sAltContNo + "', ALT_EMAILID = '" + _sAltEmailID + "', TAB_FLAG = 'Y' WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS =TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    dt.TableName = "DEFLTR_CA_ASSIGN_LINK_DTLS";
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            con.Close();
            return true;
        }

        internal static string byteArrayToImage_Rec(byte[] byteArrayIn, string filename)
        {
            Utilities.Network.NetworkDrive nd = new Utilities.Network.NetworkDrive();
            string pth = string.Empty, sl = string.Empty;

            try
            {
                pth = @"\\10.125.64.236\UploadedImages";
                nd.MapNetworkDrive(@"\\10.125.64.236\UploadedImages", "Z:", "uploadimages", "Bses@123");
                sl = pth;

                string _sDir = sl + "\\RECOVERY\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();

                DirectoryInfo _DirInfo = new DirectoryInfo(_sDir);
                if (_DirInfo.Exists == false)
                    _DirInfo.Create();

                string _sPath = _DirInfo.FullName + "\\" + filename + ".jpeg";
                string _sFileNameWithoutExt = _DirInfo.FullName + "\\" + filename;
                int _iFileIndex = 1;
                while (File.Exists(_sPath))
                {
                    _sPath = _sFileNameWithoutExt + "_" + _iFileIndex.ToString() + ".jpeg";
                    _iFileIndex++;
                }
                File.WriteAllBytes(_sPath, byteArrayIn);

                return _sPath;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        internal DataTable ChkUpdatedate4()
        {
            string sql = "";
            sql = "select A_DATE.dt, b_date.UDT from (select TO_CHAR(SYSDATE, 'yyyyMM')DT from dual) A_DATE,(select max(UPDATION_ID_DTLS) UDT from recovery.DEFLTR_CA_ASSIGN_LINK_DTLS) B_DATE";
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                DataTable dt = new DataTable();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmd.Connection = con;
                cmd = new OleDbCommand(sql, con);
                oda = new OleDbDataAdapter(cmd);
                oda.Fill(dt);
                return dt;
            }
        }
        internal  DataTable ChkRecordExist4(string _sCANumber, string _sFEID, string _sChkInTable)
        {
            string sql = "";
            if (_sChkInTable == "DEFLTR_ATR")
                sql = "SELECT * FROM recovery.DEFLTR_ATR WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                DataTable dt = new DataTable();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmd.Connection = con;
                cmd = new OleDbCommand(sql, con);
                oda = new OleDbDataAdapter(cmd);
                oda.Fill(dt);
                return dt;
            }
        }
        internal DataTable ChkRecordExist5(string _sCANumber, string _sFEID, string _sChkInTable)
        {
            string sql = "";
            if (_sChkInTable == "DEFLTR_ATR")
                sql = "SELECT * FROM recovery.DEFLTR_ATR WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                DataTable dt = new DataTable();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmd.Connection = con;
                cmd = new OleDbCommand(sql, con);
                oda = new OleDbDataAdapter(cmd);
                oda.Fill(dt);
                return dt;
            }
        }
        internal bool InsertTable7(string _sCANumber, string _sStatus, string _sFEID, string _sIPAddr, string _sInsInTable)
        {
            string sql = "";
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                if (_sInsInTable == "DEFLTR_ATR")
                {
                    sql = "INSERT INTO recovery.DEFLTR_ATR (CA_NUMBER, UPDATION_ID, ASSIGN_USER_CODE, STATUS, ASSIGN_BY, TAB_ENTRY_DATE, TAB_UPD_BY, TAB_IPADDR)";
                    sql += " SELECT UNIQUE CA_NUMBER,UPDATION_ID_DTLS,ASSIGN_USER_CODE,'" + _sStatus + "' STATUS,ASSIGN_BY,SYSDATE,'" + _sFEID + "' TAB_UPD_BY,'" + _sIPAddr + "' TAB_IP";
                    sql += " FROM DEFLTR_CA_ASSIGN_LINK_DTLS WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    OleDbCommand cmd = new OleDbCommand();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmd.Connection = con;
                    cmd = new OleDbCommand(sql, con);
                    oda = new OleDbDataAdapter(cmd);
                    oda.SelectCommand.CommandText = "INSERT INTO DEFLTR_ATR (CA_NUMBER, UPDATION_ID, ASSIGN_USER_CODE, STATUS, ASSIGN_BY, TAB_ENTRY_DATE, TAB_UPD_BY, TAB_IPADDR) SELECT UNIQUE CA_NUMBER,UPDATION_ID_DTLS,ASSIGN_USER_CODE,'" + _sStatus + "' STATUS,ASSIGN_BY,SYSDATE,'" + _sFEID + "' TAB_UPD_BY,'" + _sIPAddr + "' TAB_IP FROM DEFLTR_CA_ASSIGN_LINK_DTLS WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    dt.TableName = "DEFLTR_ATR";
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }
        internal bool InsertTable8(string _sCANumber, string _sStatus, string _sFEID, string _sIPAddr, string _sInsInTable)
        {
            string sql = "";
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                if (_sInsInTable == "DEFLTR_ATR")
                {
                    sql = "INSERT INTO recovery.DEFLTR_ATR (CA_NUMBER, UPDATION_ID, ASSIGN_USER_CODE, STATUS, ASSIGN_BY, TAB_ENTRY_DATE, TAB_UPD_BY, TAB_IPADDR)";
                    sql += " SELECT UNIQUE CA_NUMBER,UPDATION_ID_DTLS,ASSIGN_USER_CODE,'" + _sStatus + "' STATUS,ASSIGN_BY,SYSDATE,'" + _sFEID + "' TAB_UPD_BY,'" + _sIPAddr + "' TAB_IP";
                    sql += " FROM DEFLTR_CA_ASSIGN_LINK_DTLS WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    OleDbCommand cmd = new OleDbCommand();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmd.Connection = con;
                    cmd = new OleDbCommand(sql, con);
                    oda = new OleDbDataAdapter(cmd);
                    oda.SelectCommand.CommandText = "INSERT INTO DEFLTR_ATR (CA_NUMBER, UPDATION_ID, ASSIGN_USER_CODE, STATUS, ASSIGN_BY, TAB_ENTRY_DATE, TAB_UPD_BY, TAB_IPADDR) SELECT UNIQUE CA_NUMBER,UPDATION_ID_DTLS,ASSIGN_USER_CODE,'" + _sStatus + "' STATUS,ASSIGN_BY,SYSDATE,'" + _sFEID + "' TAB_UPD_BY,'" + _sIPAddr + "' TAB_IP FROM DEFLTR_CA_ASSIGN_LINK_DTLS WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    dt.TableName = "DEFLTR_ATR";
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }
        internal bool InsertSMS9(string _sMobileNo, string _sMessage, string _sCANumber, string _sUpdationID, string _sIPAddress)
        {
            string sql = "";
            sql = "INSERT INTO RECAPP_SMS (MOBILE_NO, MESSAGE_TXT, IP_ADDRESS, CA_NUMBER, UPDATION_ID)";
            sql += " VALUES ('" + _sMobileNo + "','" + _sMessage + "','" + _sIPAddress + "','" + _sCANumber + "','" + _sUpdationID + "')";

            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                DataTable dt = new DataTable();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmd.Connection = con;
                cmd = new OleDbCommand(sql, con);
                oda = new OleDbDataAdapter(cmd);
                oda.SelectCommand.CommandText = "INSERT INTO RECAPP_SMS (MOBILE_NO, MESSAGE_TXT, IP_ADDRESS, CA_NUMBER, UPDATION_ID) VALUES ('" + _sMobileNo + "','" + _sMessage + "','" + _sIPAddress + "','" + _sCANumber + "','" + _sUpdationID + "')";
                dt.TableName = "RECAPP_SMS";
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        internal bool ConsumerSMS1(string _sMobileNo, string _sMessage, string _sCANumber, string _sFEID)//Added  By Babalu Kumar 10022021 PCN No. 2021021003
        {
            string sql = "";
            sql = "INSERT INTO RECOVERY.CONSUMER_SMS (MOBILE_NO, MESSAGE_TXT,CA_NUMBER, FE_ID,SEND_ID)";
            sql += " VALUES ('" + _sMobileNo + "','" + _sMessage + "','" + _sCANumber + "','" + _sFEID + "',SMS_REC_ID.NEXTVAL)";

            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                DataTable dt = new DataTable();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmd.Connection = con;
                cmd = new OleDbCommand(sql, con);
                oda = new OleDbDataAdapter(cmd);
                oda.SelectCommand.CommandText = "INSERT INTO RECOVERY.CONSUMER_SMS (MOBILE_NO, MESSAGE_TXT,CA_NUMBER, FE_ID,SEND_ID) VALUES ('" + _sMobileNo + "','" + _sMessage + "','" + _sCANumber + "','" + _sFEID + "',SMS_REC_ID.NEXTVAL)";
                dt.TableName = "CONSUMER_SMS";
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        internal bool UpdateTable10(string _sCANumber, string _sStatus, string _sFEID, string _sIPAddr, string _sUpdInTable, string _sTabUpdStatus, string _sRemarks,
                              string _sImgFlg, string _sImg, string _sImage, string _sFollowDate, string _sAltContNo, string _sAltEmailID, string Amount, string pdcDate, string RunningAmt, string Latitude, string Longitude)

        {
            string sql = "";
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                if (_sUpdInTable == "DEFLTR_ATR")
                {
                    sql = "UPDATE DEFLTR_ATR SET STATUS = '" + _sStatus + "',TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "'";
                    sql += " WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";

                    OleDbCommand cmd = new OleDbCommand();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmd.Connection = con;
                    cmd = new OleDbCommand(sql, con);
                    oda = new OleDbDataAdapter(cmd);
                    oda.SelectCommand.CommandText = "UPDATE DEFLTR_ATR SET STATUS = '" + _sStatus + "', TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "' WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    dt.TableName = "DEFLTR_ATR";
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else if (_sUpdInTable == "DEFLTR_CA_ASSIGN_LINK_DTLS")
                {
                    try
                    {
                        if (_sImgFlg == "Y")
                        {
                            if (!String.IsNullOrEmpty(_sImg))
                            {
                                byte[] _byImg = Convert.FromBase64String(_sImg);
                                _sImg = byteArrayToImage_Rec(_byImg, _sCANumber + DateTime.Now.ToString("yyyyMMdd"));
                            }
                            if (!String.IsNullOrEmpty(_sImage))
                            {
                                byte[] _byImg1 = Convert.FromBase64String(_sImage);
                                _sImage = byteArrayToImage_Rec(_byImg1, _sCANumber + "_1" + DateTime.Now.ToString("yyyyMMdd"));
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    //sql = "UPDATE DEFLTR_CA_ASSIGN_LINK_DTLS SET STATUS = '" + _sStatus + "',TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "',";
                    //sql += " TAB_UPD_STATUS = '" + _sTabUpdStatus + "', STATUS_REMARKS = '" + _sRemarks + "', TAB_IMAGE_FLG = '" + _sImgFlg + "', TAB_IMAGE_PATH = '" + _sImg + "',TAB_IMAGE_PATH1 = '" + _sImage + "',";
                    //sql += " FOLLOWUP_DATE = TO_DATE('" + _sFollowDate + "','dd/MM/yyyy'), ALT_CONTACTNO = '" + _sAltContNo + "', ALT_EMAILID = '" + _sAltEmailID + "', TAB_FLAG = 'Y',AMOUNT='" + Amount + "',PDC_DATE=TO_DATE('" + pdcDate + "','dd/MM/yyyy')";
                    //sql += " WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    sql = "UPDATE DEFLTR_CA_ASSIGN_LINK_DTLS SET STATUS = '" + _sStatus + "',TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "',";
                    sql += " TAB_UPD_STATUS = '" + _sTabUpdStatus + "', STATUS_REMARKS = '" + _sRemarks + "', TAB_IMAGE_FLG = '" + _sImgFlg + "', TAB_IMAGE_PATH = '" + _sImg + "',TAB_IMAGE_PATH1 = '" + _sImage + "',";
                    sql += " FOLLOWUP_DATE = TO_DATE('" + _sFollowDate + "','dd/MM/yyyy'), ALT_CONTACTNO = '" + _sAltContNo + "', ALT_EMAILID = '" + _sAltEmailID + "', TAB_FLAG = 'Y',RUNNINGAMT='" + RunningAmt + "',LATITUDE='" + Latitude + "',LONGITUDE='" + Longitude + "',AMOUNT='" + Amount + "',PDC_DATE=TO_DATE('" + pdcDate + "','dd/MM/yyyy')";
                    sql += " WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    OleDbCommand cmd = new OleDbCommand();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmd.Connection = con;
                    cmd = new OleDbCommand(sql, con);
                    oda = new OleDbDataAdapter(cmd);
                    oda.SelectCommand.CommandText = "UPDATE DEFLTR_CA_ASSIGN_LINK_DTLS SET STATUS = '" + _sStatus + "', TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "', TAB_UPD_STATUS = '" + _sTabUpdStatus + "', STATUS_REMARKS = '" + _sRemarks + "', TAB_IMAGE_FLG = '" + _sImgFlg + "', TAB_IMAGE_PATH = '" + _sImg + "',TAB_IMAGE_PATH1 = '" + _sImage + "', FOLLOWUP_DATE = TO_DATE('" + _sFollowDate + "','dd/MM/yyyy'), ALT_CONTACTNO = '" + _sAltContNo + "', ALT_EMAILID = '" + _sAltEmailID + "', TAB_FLAG = 'Y',RUNNINGAMT='" + RunningAmt + "',LATITUDE='" + Latitude + "',LONGITUDE='" + Longitude + "',AMOUNT='" + Amount + "',PDC_DATE=TO_DATE('" + pdcDate + "','dd/MM/yyyy') WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(SYSDATE,'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    dt.TableName = "DEFLTR_CA_ASSIGN_LINK_DTLS";
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            con.Close();
            return true;
        }
        internal bool UpdateTable11(string _sCANumber, string _sStatus, string _sFEID, string _sIPAddr, string _sUpdInTable, string _sTabUpdStatus, string _sRemarks,
                             string _sImgFlg, string _sImg, string _sImage, string _sFollowDate, string _sAltContNo, string _sAltEmailID, string Amount, string pdcDate, string RunningAmt, string Latitude, string Longitude)

        {
            string sql = "";
            using (con = new OleDbConnection(strConnection))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                if (_sUpdInTable == "DEFLTR_ATR")
                {
                    sql = "UPDATE DEFLTR_ATR SET STATUS = '" + _sStatus + "',TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "'";
                    sql += " WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";

                    OleDbCommand cmd = new OleDbCommand();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmd.Connection = con;
                    cmd = new OleDbCommand(sql, con);
                    oda = new OleDbDataAdapter(cmd);
                    oda.SelectCommand.CommandText = "UPDATE DEFLTR_ATR SET STATUS = '" + _sStatus + "', TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "' WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    dt.TableName = "DEFLTR_ATR";
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else if (_sUpdInTable == "DEFLTR_CA_ASSIGN_LINK_DTLS")
                {
                    try
                    {
                        if (_sImgFlg == "Y")
                        {
                            if (!String.IsNullOrEmpty(_sImg))
                            {
                                byte[] _byImg = Convert.FromBase64String(_sImg);
                                _sImg = byteArrayToImage_Rec(_byImg, _sCANumber + DateTime.Now.ToString("yyyyMMdd"));
                            }
                            if (!String.IsNullOrEmpty(_sImage))
                            {
                                byte[] _byImg1 = Convert.FromBase64String(_sImage);
                                _sImage = byteArrayToImage_Rec(_byImg1, _sCANumber + "_1" + DateTime.Now.ToString("yyyyMMdd"));
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    sql = "UPDATE DEFLTR_CA_ASSIGN_LINK_DTLS SET STATUS = '" + _sStatus + "',TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "',";
                    sql += " TAB_UPD_STATUS = '" + _sTabUpdStatus + "', STATUS_REMARKS = '" + _sRemarks + "', TAB_IMAGE_FLG = '" + _sImgFlg + "', TAB_IMAGE_PATH = '" + _sImg + "',TAB_IMAGE_PATH1 = '" + _sImage + "',";
                    sql += " FOLLOWUP_DATE = TO_DATE('" + _sFollowDate + "','dd/MM/yyyy'), ALT_CONTACTNO = '" + _sAltContNo + "', ALT_EMAILID = '" + _sAltEmailID + "', TAB_FLAG = 'Y',RUNNINGAMT='" + RunningAmt + "',LATITUDE='" + Latitude + "',LONGITUDE='" + Longitude + "',AMOUNT='" + Amount + "',PDC_DATE=TO_DATE('" + pdcDate + "','dd/MM/yyyy')";
                    sql += " WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS = TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    OleDbCommand cmd = new OleDbCommand();
                    DataTable dt = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmd.Connection = con;
                    cmd = new OleDbCommand(sql, con);
                    oda = new OleDbDataAdapter(cmd);
                    oda.SelectCommand.CommandText = "UPDATE DEFLTR_CA_ASSIGN_LINK_DTLS SET STATUS = '" + _sStatus + "', TAB_UPD_DT = SYSDATE, TAB_UPD_BY = '" + _sFEID + "', TAB_IPADDR = '" + _sIPAddr + "', TAB_UPD_STATUS = '" + _sTabUpdStatus + "', STATUS_REMARKS = '" + _sRemarks + "', TAB_IMAGE_FLG = '" + _sImgFlg + "', TAB_IMAGE_PATH = '" + _sImg + "',TAB_IMAGE_PATH1 = '" + _sImage + "', FOLLOWUP_DATE = TO_DATE('" + _sFollowDate + "','dd/MM/yyyy'), ALT_CONTACTNO = '" + _sAltContNo + "', ALT_EMAILID = '" + _sAltEmailID + "', TAB_FLAG = 'Y',RUNNINGAMT='" + RunningAmt + "',LATITUDE='" + Latitude + "',LONGITUDE='" + Longitude + "',AMOUNT='" + Amount + "',PDC_DATE=TO_DATE('" + pdcDate + "','dd/MM/yyyy') WHERE CA_NUMBER = '" + _sCANumber + "' AND UPDATION_ID_DTLS =TO_CHAR(ADD_MONTHS(SYSDATE, -1),'yyyyMM') AND ASSIGN_USER_CODE = '" + _sFEID + "'";
                    dt.TableName = "DEFLTR_CA_ASSIGN_LINK_DTLS";
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            con.Close();
            return true;
        }
    }
}