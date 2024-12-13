using System;
using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Net.Http;
using System.IO;

namespace Power_Metering.BusinessLogicLayer
{
    public class PowerMeteringBL
    {
        
        OleDbConnection con;
        OleDbCommand cmd;
        public PowerMeteringBL()
        {
            con = new OleDbConnection();
            cmd= new OleDbCommand();
        }
        internal string byteArrayToImage(byte[] byteArrayIn, string filename)
        {
            string pth = string.Empty;
            string sl = string.Empty;
            Utilities.Network.NetworkDrive nd = new Utilities.Network.NetworkDrive();
            try
            {
                pth = @"\\10.125.64.236\UploadedImages";
                nd.MapNetworkDrive(@"\\10.125.64.236\UploadedImages", "Z:", "uploadimages", "Bses@123");
                sl = pth;
                string dirPath = sl + "\\" + "Power_Metering" + "\\" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                DirectoryInfo _DirInfo = new DirectoryInfo(dirPath);
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
                if (_sPath != "X")
                    File.WriteAllBytes(_sPath, byteArrayIn);
                return _sPath;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max).ToString();
        }

        internal string byteArrayToImageEnf(byte[] byteArrayIn, string filename)
        {
            string pth = string.Empty;
            string sl = string.Empty;
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            Utilities.Network.NetworkDrive nd = new Utilities.Network.NetworkDrive();
            try
            {
                pth = @"\\10.125.64.236\UploadedImages";
                nd.MapNetworkDrive(@"\\10.125.64.236\UploadedImages", "Z:", "uploadimages", "Bses@123");
                sl = pth;
                string dirPath = sl + "\\" + "Enforcement_File" + "\\" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                DirectoryInfo _DirInfo = new DirectoryInfo(dirPath);
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
                if (_sPath != "X")
                    File.WriteAllBytes(_sPath, byteArrayIn);
                return _sPath;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        internal void GetHooter_SMS(string _sEmpID, string _sAddress, string _sLogitude, string _sLatitude)
        {
            string sqlQuery = string.Empty; HttpResponseMessage Msg = new HttpResponseMessage();
            string connectionString = NDS.con();
            try
            {
                string _sSMSText = string.Empty, _sMobNo = string.Empty, _sMobNoL1 = string.Empty, _sMobNoL2 = string.Empty;
                using (con = new OleDbConnection(connectionString))
                {
                    sqlQuery = "select SMS,EMP_NAME,EMP_MOBILE,FIRST_PERSON_MOBILE,SECOND_PERSON_MOBILE from mobint.ENF_HOOTER_LOGIN_MST where  EMP_ID='" + _sEmpID + "'";
                    cmd = new OleDbCommand(sqlQuery, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        _sSMSText = rdr["SMS"].ToString();

                        _sMobNo = rdr["EMP_MOBILE"].ToString();
                        _sMobNo += " - " + rdr["EMP_NAME"].ToString();
                        _sAddress = "Coordinate(" + _sLogitude + "," + _sLatitude + ")";

                        _sMobNoL1 = rdr["FIRST_PERSON_MOBILE"].ToString();
                        _sMobNoL2 = rdr["SECOND_PERSON_MOBILE"].ToString();

                        _sSMSText = _sSMSText.Replace("{#XXXXXXXXXX#}", _sMobNo);
                        _sSMSText = _sSMSText.Replace("{#@@@@@#}", _sAddress);
                    }

                    if (_sMobNoL1.Trim() != "")
                    {
                        Random generator = new Random();
                        int r = generator.Next(1000, 1000000);

                        sqlQuery = "insert into mobint.MCR_SMS_APP_DETAILS(SMS_ID, COMPANY, MOBILE_NO, MESSAGE_TXT,APP_NAME)";
                        sqlQuery += " VALUES('" + r.ToString() + "','BRPL','" + _sMobNoL1 + "','" + _sSMSText + "','HOOTER')";

                        cmd = new OleDbCommand(sqlQuery, con);
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        cmd.ExecuteNonQuery();
                    }

                    if (_sMobNoL2.Trim() != "")
                    {
                        Random generator = new Random();
                        int r = generator.Next(1000, 1000000);

                        sqlQuery = "insert into mobint.MCR_SMS_APP_DETAILS(SMS_ID, COMPANY, MOBILE_NO, MESSAGE_TXT,APP_NAME)";
                        sqlQuery += " VALUES('" + r.ToString() + "','BRPL','" + _sMobNoL2 + "','" + _sSMSText + "','HOOTER')";

                        cmd = new OleDbCommand(sqlQuery, con);
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception )
            {

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        internal string ParseHttpWebResponse(HttpWebResponse httpRep)
        {
            string rep;
            System.IO.StreamReader sr = new System.IO.StreamReader(httpRep.GetResponseStream());
            rep = sr.ReadToEnd();
            return rep;
        }

        internal void Insert_MessageLog(string _sMsg, string _sMobileNo, string _sSMSType, string _sCompany, string _sStatus, string _sResponseId,
                            string _sResponseCode, string _sResponseStatus, string _sResponseTime, string _sVendor, string _sAppName, string _sEmpCode)
        {
            string sql = " INSERT INTO mobapp.dsk_sms_details (SMS_ID, SMS_TYPE, COMPANY, MOBILE_NO, MESSAGE_TXT,STATUS,SMS_SEND_DT,RESP_ID,RESP_INFO,RESP_CODE,RESP_TIME,VENDOR,APP_NAME,EMP_CODE) VALUES  ";
            sql = sql + "  (sms_id.NEXTVAL ,'" + _sSMSType + "' ,'" + _sCompany + "','" + _sMobileNo + "' ,'" + _sMsg + "' , ";
            sql = sql + "  '" + _sStatus + "' ,sysdate ,'" + _sResponseId + "', '" + _sResponseStatus + "','" + _sResponseCode + "' ,'" + _sResponseTime + "','" + _sVendor + "','" + _sAppName + "','" + _sEmpCode + "' )  ";

            dmlsinglequery(sql);
        }
        internal DataTable CheckSMS_Validity(string _sKey, string _sCompany)
        {
            string _sSql = string.Empty;
            DataTable _dt = new DataTable();

            _sSql = " SELECT API_URL FROM SMS_API_MST where ENCRYPTION_KEY='" + _sKey + "' and ACTIVE_STATUS='Y' ";
            _sSql += " and trunc(sysdate)<trunc(VALIDITY_END_DATE) and COMPANY_CODE='" + _sCompany + "' ";
            return dmlgetqueryCon(_sSql);
        }

        private DataTable dmlgetqueryCon(string sql)
        {
            DataTable dt = new DataTable();
            OleDbCommand dbcommand;
            //OleDbTransaction dbtrans;
            OleDbDataAdapter da;
            OleDbConnection ocon = new OleDbConnection(NDS.conMobApp());
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

        private bool dmlsinglequery(string sql)
        {
            OleDbCommand dbcommand;
            //OleDbTransaction dbtrans;

            OleDbConnection ocon = new OleDbConnection(NDS.conMobApp());
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
            catch (Exception )
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



    }
}