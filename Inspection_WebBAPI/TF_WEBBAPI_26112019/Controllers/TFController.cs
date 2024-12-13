using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.OleDb;
using TF_WEBBAPI_26112019.Models;
using System.IO;
using System.Text;
using System.Web;
using System.Net.Http.Formatting;
using System.Text.RegularExpressions;

namespace TF_WEBBAPI_26112019.Controllers
{
    public class TFController : ApiController
    {
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataAdapter adp;
        DataTable dt = new DataTable();
        HttpResponseMessage Msg;
        string sql = string.Empty;
        ResultResponse objresult = new ResultResponse();
        public string strConnection = NDS.con();
        public string strConnectionONM = NDS.conONM();
        public string strConnectionITHELP = NDS.con_ITHELP();
        public object Response { get; private set; }

        [Route("API/TF/Insert_Inspection_DATA")]
        [HttpPost]
        public HttpResponseMessage Insert_Inspection_Data([FromBody] Inspection_Data IP_Data)
        {
            try
            {
                string _sFLAG = "N";
                string ImageToBase = IP_Data.INSTALLATIONPHOTO;
                Byte[] _byImgBuild = null;
                string _sCancelImgPath = string.Empty;

                if (ImageToBase != null)
                {
                    _byImgBuild = Convert.FromBase64String(IP_Data.INSTALLATIONPHOTO);
                    _sCancelImgPath = byteArrayToImage(_byImgBuild, IP_Data.ORDER_NO + "_sCancelImgPath");
                }

                if (Saved_Inspection_Data(IP_Data.ISTABCASE, IP_Data.CA_ORDERNO,
                    IP_Data.METER_NO, IP_Data.CA_NO, IP_Data.ORDER_NO, IP_Data.ORDERTYPE, IP_Data.ACTIVITYTYPE,
                    IP_Data.ACTIVITYDATE, IP_Data.INSTAL_METER, IP_Data.INSTAL_CABLE,
                    IP_Data.INSTAL_BUSBARREMARK, IP_Data.INSTAL_SEALING, IP_Data.INSTAL_METERREMARKS,
                    IP_Data.INSTAL_CABLEREMARK, IP_Data.INSTAL_BUSBAR, IP_Data.INSTAL_SEALINGREMARK,
                    IP_Data.QUALITY_METER, IP_Data.QUALITY_CABLE, IP_Data.QUALITY_FEEDINGPOINTS,
                    IP_Data.QUALITY_MARKING, IP_Data.QUALITY_METERREMARK, IP_Data.QUALITY_CABLEREMARK,
                    IP_Data.QUALITY_FEEDINGPOINTSREMARK, IP_Data.QUALITY_MARKINGREMARK,
                    IP_Data.INSTALLATIONSAFETY, IP_Data.INSTALLATIONSAFETYREMARK, IP_Data.OTHEROBSERVATION,
                    _sCancelImgPath, IP_Data.CREATEDBY, IP_Data.DIVISION, IP_Data.INSPECTION_TYPE) == true)
                {
                    if (Saved_Inspection_Photo(IP_Data.CA_ORDERNO, IP_Data.METER_NO, _sCancelImgPath, IP_Data.CA_NO) == true)
                    {
                        if (IP_Data.ID != null)
                        {
                            if (_sFLAG == "Y")
                            {
                                objresult.Key = "Success";
                                objresult.Status = "200";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                                return Msg;
                            }
                            else
                            {
                                objresult.Key = "Failed";
                                objresult.Status = "421";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                                return Msg;
                            }
                        }
                        else
                        {
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }
                    }
                    else
                    {
                        objresult.Key = "Failed";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        public string byteArrayToImage(byte[] byteArrayIn, string filename)
        {
            Utilities.Network.NetworkDrive nd = new Utilities.Network.NetworkDrive();
            string pth = string.Empty, sl = string.Empty;

            try
            {
                pth = @"\\10.125.64.236\UploadedImages";
                nd.MapNetworkDrive(@"\\10.125.64.236\UploadedImages", "Z:", "uploadimages", "Bses@123");
                sl = pth;

                string _sDir = sl + @"\MAMS_INSPECTION\";

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

                if (File.Exists(_sPath) == true)
                {
                    FileInfo fi = new FileInfo(_sPath);
                    if (fi.Length == 0)
                    {
                        File.Delete(_sPath);
                        _sPath = "";
                        // File.Move(_sPath, _sPath);
                    }
                }


                return _sPath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string byteArrayToImage1(byte[] byteArrayIn, string filename)
        {
            Utilities.Network.NetworkDrive nd = new Utilities.Network.NetworkDrive();
            string pth = string.Empty, sl = string.Empty;

            try
            {
                pth = @"\\10.125.64.236\UploadedImages";
                nd.MapNetworkDrive(@"\\10.125.64.236\UploadedImages", "Z:", "uploadimages", "Bses@123");
                sl = pth;

                string _sDir = sl + @"\MAMS_INSPECTION\";

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

                if (File.Exists(_sPath) == true)
                {
                    FileInfo fi = new FileInfo(_sPath);
                    if (fi.Length == 0)
                    {
                        File.Delete(_sPath);
                        _sPath = "";
                        // File.Move(_sPath, _sPath);
                    }
                }


                return _sPath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string byteArrayToImage2(byte[] byteArrayIn, string filename)
        {
            Utilities.Network.NetworkDrive nd = new Utilities.Network.NetworkDrive();
            string pth = string.Empty, sl = string.Empty;

            try
            {
                pth = @"\\10.125.64.236\UploadedImages";
                nd.MapNetworkDrive(@"\\10.125.64.236\UploadedImages", "Z:", "uploadimages", "Bses@123");
                sl = pth;

                string _sDir = sl + @"\MAMS_INSPECTION\";

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

                if (File.Exists(_sPath) == true)
                {
                    FileInfo fi = new FileInfo(_sPath);
                    if (fi.Length == 0)
                    {
                        File.Delete(_sPath);
                        _sPath = "";
                        // File.Move(_sPath, _sPath);
                    }
                }


                return _sPath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string byteArrayToDOC(byte[] byteArrayIn, string filename, string Extension)
        {
            Utilities.Network.NetworkDrive nd = new Utilities.Network.NetworkDrive();
            string pth = string.Empty, sl = string.Empty;

            try
            {
                pth = @"\\10.125.64.236\UploadedImages";
                nd.MapNetworkDrive(@"\\10.125.64.236\UploadedImages", "Z:", "uploadimages", "Bses@123");
                sl = pth;

                string _sDir = sl + @"\MAMS_INSPECTION\";

                DirectoryInfo _DirInfo = new DirectoryInfo(_sDir);
                if (_DirInfo.Exists == false)
                    _DirInfo.Create();

                string _sPath = _DirInfo.FullName + "\\" + filename + Extension;
                string _sFileNameWithoutExt = _DirInfo.FullName + "\\" + filename;
                int _iFileIndex = 1;

                while (File.Exists(_sPath))
                {
                    _sPath = _sFileNameWithoutExt + "_" + _iFileIndex.ToString() + Extension;
                    _iFileIndex++;
                }

                File.WriteAllBytes(_sPath, byteArrayIn);

                if (File.Exists(_sPath) == true)
                {
                    FileInfo fi = new FileInfo(_sPath);
                    if (fi.Length == 0)
                    {
                        File.Delete(_sPath);
                        _sPath = "";
                        // File.Move(_sPath, _sPath);
                    }
                }


                return _sPath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        static string base64String = null;
        public string ImageToBase64()
        {
            string path = "D:\\Background.png";
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
        public System.Drawing.Image Base64ToImage()
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }


        private bool Saved_Inspection_Photo(string _sCA_ORDERNO, string _sMETER_NO, string _sINSTALLATIONPHOTO, string _sCA_NO)
        {
            using (con = new OleDbConnection(strConnection))
            {
                sql = "INSERT INTO mobint.MCR_INSPECTION_IMAGE(ORDERNO,DEVICENO,IMAGE1,CA_NUMBER,TYPE)";
                sql = sql + " VALUES";
                sql = sql + "('" + _sCA_ORDERNO + "','" + _sMETER_NO + "','" + _sINSTALLATIONPHOTO + "','" + _sCA_NO + "', 'INSPECTION')";

                cmd = new OleDbCommand(sql, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int i = cmd.ExecuteNonQuery();
                if (i != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool Saved_BurntMeter_Photo(string _sCA_ORDERNO, string _sMETER_NO, string _sINSTALLATIONPHOTO, string _sCA_NO)
        {
            using (con = new OleDbConnection(strConnection))
            {
                sql = "INSERT INTO mobint.MCR_INSPECTION_IMAGE(ORDERNO,DEVICENO,IMAGE1,CA_NUMBER,TYPE)";
                sql = sql + " VALUES";
                sql = sql + "('" + _sCA_ORDERNO + "','" + _sMETER_NO + "','" + _sINSTALLATIONPHOTO + "','" + _sCA_NO + "', 'BURNTMETER')";

                cmd = new OleDbCommand(sql, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int i = cmd.ExecuteNonQuery();
                if (i != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool Saved_Inspection_Data(string _sISTABCASE, string _sCA_ORDERNO, string _sMETER_NO,
                    string _sCA_NO, string _sORDER_NO, string _sORDERTYPE, string _sACTIVITYTYPE, string _sACTIVITYDATE,
                    string _sINSTAL_METER, string _sINSTAL_CABLE, string _sINSTAL_BUSBARREMARK,
                    string _sINSTAL_SEALING, string _sINSTAL_METERREMARKS, string _sINSTAL_CABLEREMARK,
                    string _sINSTAL_BUSBAR, string _sINSTAL_SEALINGREMARK, string _sQUALITY_METER,
                    string _sQUALITY_CABLE, string _sQUALITY_FEEDINGPOINTS, string _sQUALITY_MARKING,
                    string _sQUALITY_METERREMARK, string _sQUALITY_CABLEREMARK, string _sQUALITY_FEEDINGPOINTSREMARK,
                    string _sQUALITY_MARKINGREMARK, string _sINSTALLATIONSAFETY, string _sINSTALLATIONSAFETYREMARK,
                    string _sOTHEROBSERVATION, string _sINSTALLATIONPHOTO, string _sCREATEDBY, string _sDIVISION, string _sINSPECTION_TYPE)
        {
            Random generator = new Random();
            int _autoID = generator.Next(1, 10000);

            using (con = new OleDbConnection(strConnection))
            {
                sql = "INSERT INTO mobint.MCR_INSPECTION_MASTER(ID,ISTABCASE,CA_ORDERNO,METER_NO,CA_NO,";
                sql = sql + "ORDER_NO,ORDER_TYPE,ACTIVITY_TYPE, ACTIVITYDATE,INSTAL_METER,INSTAL_CABLE,INSTAL_BUSBARREMARK,";
                sql = sql + "INSTAL_SEALING,INSTAL_METERREMARKS,INSTAL_CABLEREMARK,INSTAL_BUSBAR,INSTAL_SEALINGREMARK,";
                sql = sql + "QUALITY_METER,QUALITY_CABLE,QUALITY_FEEDINGPOINTS,QUALITY_MARKING,QUALITY_METERREMARK,";
                sql = sql + "QUALITY_CABLEREMARK,QUALITY_FEEDINGPOINTSREMARK,QUALITY_MARKINGREMARK,INSTALLATIONSAFETY,";
                sql = sql + "INSTALLATIONSAFETYREMARK,OTHEROBSERVATION,INSTALLATIONPHOTO,CREATEDDATE,CREATEDBY,DIVISION,INSPECTION_TYPE)";
                sql = sql + " VALUES";
                sql = sql + "('" + _autoID + "','" + _sISTABCASE + "','" + _sCA_ORDERNO + "','" + _sMETER_NO + "','" + _sCA_NO + "','" + _sORDER_NO + "','" + _sORDERTYPE + "',";
                sql = sql + "'" + _sACTIVITYTYPE + "',to_date('" + _sACTIVITYDATE + "','dd-mm-yyyy'),'" + _sINSTAL_METER + "','" + _sINSTAL_CABLE + "','" + _sINSTAL_BUSBARREMARK + "',";
                sql = sql + "'" + _sINSTAL_SEALING + "','" + _sINSTAL_METERREMARKS + "','" + _sINSTAL_CABLEREMARK + "','" + _sINSTAL_BUSBAR + "',";
                sql = sql + "'" + _sINSTAL_SEALINGREMARK + "','" + _sQUALITY_METER + "','" + _sQUALITY_CABLE + "','" + _sQUALITY_FEEDINGPOINTS + "',";
                sql = sql + "'" + _sQUALITY_MARKING + "','" + _sQUALITY_METERREMARK + "','" + _sQUALITY_CABLEREMARK + "','" + _sQUALITY_FEEDINGPOINTSREMARK + "',";
                sql = sql + "'" + _sQUALITY_MARKINGREMARK + "','" + _sINSTALLATIONSAFETY + "','" + _sINSTALLATIONSAFETYREMARK + "','" + _sOTHEROBSERVATION + "'," +
                    "'" + _sINSTALLATIONPHOTO + "',SYSDATE,'" + _sCREATEDBY + "','" + _sDIVISION + "','" + _sINSPECTION_TYPE + "')";

                cmd = new OleDbCommand(sql, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int i = cmd.ExecuteNonQuery();
                if (i != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        [Route("API/TF/Insert_Inspection_Data_New")]   //updated by chesta on 19-09-2024
        [HttpPost]
        public HttpResponseMessage Insert_Inspection_Data_New([FromBody] Inspection_Data IP_Data)
        {
            try
            {
                string _sFLAG = "N";
                string ImageToBase = IP_Data.INSTALLATIONPHOTO;
                Byte[] _byImgBuild = null;
                string _sCancelImgPath = string.Empty;

                if (ImageToBase != null)
                {
                    _byImgBuild = Convert.FromBase64String(IP_Data.INSTALLATIONPHOTO);
                    _sCancelImgPath = byteArrayToImage(_byImgBuild, IP_Data.ORDER_NO + "_sCancelImgPath");
                }

                if (Saved_Inspection_Data_New(IP_Data.ISTABCASE, IP_Data.CA_ORDERNO,
                    IP_Data.METER_NO, IP_Data.CA_NO, IP_Data.ORDER_NO, IP_Data.ORDERTYPE, IP_Data.ACTIVITYTYPE,
                    IP_Data.ACTIVITYDATE, IP_Data.INSTAL_METER, IP_Data.INSTAL_CABLE,
                    IP_Data.INSTAL_BUSBARREMARK, IP_Data.INSTAL_SEALING, IP_Data.INSTAL_METERREMARKS,
                    IP_Data.INSTAL_CABLEREMARK, IP_Data.INSTAL_BUSBAR, IP_Data.INSTAL_SEALINGREMARK,
                    IP_Data.QUALITY_METER, IP_Data.QUALITY_CABLE, IP_Data.QUALITY_FEEDINGPOINTS,
                    IP_Data.QUALITY_MARKING, IP_Data.QUALITY_METERREMARK, IP_Data.QUALITY_CABLEREMARK,
                    IP_Data.QUALITY_FEEDINGPOINTSREMARK, IP_Data.QUALITY_MARKINGREMARK,
                    IP_Data.INSTALLATIONSAFETY, IP_Data.INSTALLATIONSAFETYREMARK, IP_Data.OTHEROBSERVATION,
                    _sCancelImgPath, IP_Data.CREATEDBY, IP_Data.DIVISION, IP_Data.INSPECTION_TYPE,
                    IP_Data.INSPECTEDALONG, IP_Data.LINEMANNAME) == true)
                {
                    if (Saved_Inspection_Photo(IP_Data.CA_ORDERNO, IP_Data.METER_NO, _sCancelImgPath, IP_Data.CA_NO) == true)
                    {
                        if (IP_Data.ID != null)
                        {
                            if (_sFLAG == "Y")
                            {
                                objresult.Key = "Success";
                                objresult.Status = "200";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                                return Msg;
                            }
                            else
                            {
                                objresult.Key = "Failed";
                                objresult.Status = "421";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                                return Msg;
                            }
                        }
                        else
                        {
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }
                    }
                    else
                    {
                        objresult.Key = "Failed";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        //updated by chesta on 19-09-2024
        private bool Saved_Inspection_Data_New(string _sISTABCASE, string _sCA_ORDERNO, string _sMETER_NO,
                   string _sCA_NO, string _sORDER_NO, string _sORDERTYPE, string _sACTIVITYTYPE, string _sACTIVITYDATE,
                   string _sINSTAL_METER, string _sINSTAL_CABLE, string _sINSTAL_BUSBARREMARK,
                   string _sINSTAL_SEALING, string _sINSTAL_METERREMARKS, string _sINSTAL_CABLEREMARK,
                   string _sINSTAL_BUSBAR, string _sINSTAL_SEALINGREMARK, string _sQUALITY_METER,
                   string _sQUALITY_CABLE, string _sQUALITY_FEEDINGPOINTS, string _sQUALITY_MARKING,
                   string _sQUALITY_METERREMARK, string _sQUALITY_CABLEREMARK, string _sQUALITY_FEEDINGPOINTSREMARK,
                   string _sQUALITY_MARKINGREMARK, string _sINSTALLATIONSAFETY, string _sINSTALLATIONSAFETYREMARK,
                   string _sOTHEROBSERVATION, string _sINSTALLATIONPHOTO, string _sCREATEDBY, string _sDIVISION, string _sINSPECTION_TYPE,
                   string _sINSPECTEDALONG, string _sLINEMANNAME)
        {
            Random generator = new Random();
            int _autoID = generator.Next(1, 10000);

            using (con = new OleDbConnection(strConnection))
            {
                sql = "INSERT INTO mobint.MCR_INSPECTION_MASTER(ID,ISTABCASE,CA_ORDERNO,METER_NO,CA_NO,";
                sql = sql + "ORDER_NO,ORDER_TYPE,ACTIVITY_TYPE, ACTIVITYDATE,INSTAL_METER,INSTAL_CABLE,INSTAL_BUSBARREMARK,";
                sql = sql + "INSTAL_SEALING,INSTAL_METERREMARKS,INSTAL_CABLEREMARK,INSTAL_BUSBAR,INSTAL_SEALINGREMARK,";
                sql = sql + "QUALITY_METER,QUALITY_CABLE,QUALITY_FEEDINGPOINTS,QUALITY_MARKING,QUALITY_METERREMARK,";
                sql = sql + "QUALITY_CABLEREMARK,QUALITY_FEEDINGPOINTSREMARK,QUALITY_MARKINGREMARK,INSTALLATIONSAFETY,";
                sql = sql + "INSTALLATIONSAFETYREMARK,OTHEROBSERVATION,INSTALLATIONPHOTO,CREATEDDATE,CREATEDBY,DIVISION,INSPECTION_TYPE,INSPECTEDALONG,LINEMANNAME)";
                sql = sql + " VALUES";
                sql = sql + "('" + _autoID + "','" + _sISTABCASE + "','" + _sCA_ORDERNO + "','" + _sMETER_NO + "','" + _sCA_NO + "','" + _sORDER_NO + "','" + _sORDERTYPE + "',";
                sql = sql + "'" + _sACTIVITYTYPE + "',";
                if (_sACTIVITYDATE != "")
                    sql = sql += " to_date('" + _sACTIVITYDATE + "','dd-mm-yyyy'),";
                else
                    sql = sql += " '" + DBNull.Value + "',";
                sql = sql + "'" + _sINSTAL_METER + "','" + _sINSTAL_CABLE + "','" + _sINSTAL_BUSBARREMARK + "',";
                sql = sql + "'" + _sINSTAL_SEALING + "','" + _sINSTAL_METERREMARKS + "','" + _sINSTAL_CABLEREMARK + "','" + _sINSTAL_BUSBAR + "',";
                sql = sql + "'" + _sINSTAL_SEALINGREMARK + "','" + _sQUALITY_METER + "','" + _sQUALITY_CABLE + "','" + _sQUALITY_FEEDINGPOINTS + "',";
                sql = sql + "'" + _sQUALITY_MARKING + "','" + _sQUALITY_METERREMARK + "','" + _sQUALITY_CABLEREMARK + "','" + _sQUALITY_FEEDINGPOINTSREMARK + "',";
                sql = sql + "'" + _sQUALITY_MARKINGREMARK + "','" + _sINSTALLATIONSAFETY + "','" + _sINSTALLATIONSAFETYREMARK + "','" + _sOTHEROBSERVATION + "'," +
                    "'" + _sINSTALLATIONPHOTO + "',SYSDATE,'" + _sCREATEDBY + "','" + _sDIVISION + "','" + _sINSPECTION_TYPE + "','" + _sINSPECTEDALONG + "','" + _sLINEMANNAME + "')";

                cmd = new OleDbCommand(sql, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int i = cmd.ExecuteNonQuery();
                if (i != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        [Route("API/TF/Insert_General_Site_Inspection")] //updated by chesta on 19-09-2024
        [HttpPost]
        public HttpResponseMessage Insert_General_Site_Inspection([FromBody] Gen_Site_Inspection GenSiteInspect)
        {
            try
            {
                string _sFLAG = "N";
                string ImageToBase = GenSiteInspect.SITEPHOTO;
                Byte[] _byImgSite = null;
                string _sSitePhotoImgPath = string.Empty;

                if (ImageToBase != null)
                {
                    _byImgSite = Convert.FromBase64String(GenSiteInspect.SITEPHOTO);
                    _sSitePhotoImgPath = byteArrayToImage(_byImgSite, GenSiteInspect.CONSUMERNAME + "_sSiteImgPath");
                }

                if (General_Site_Inspec_Save(GenSiteInspect.INSPECTIONTYPE, GenSiteInspect.INSPECTIONPURPOSE,
                    GenSiteInspect.CONSUMERNAME, GenSiteInspect.CONSUMERADDRESS, GenSiteInspect.CONSUMERPHONENO, GenSiteInspect.SITEOBSERVATION,
                    _sSitePhotoImgPath, GenSiteInspect.UPLOADBY, GenSiteInspect.INSPECTEDALONG, GenSiteInspect.LINEMANNAME
                    , GenSiteInspect.CIRCLE, GenSiteInspect.DIVISION) == true)
                {
                    objresult.Key = "Success";
                    objresult.Status = "200";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    return Msg;
                }
                else
                {
                    objresult.Key = "Failed";
                    objresult.Status = "421";
                    Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    return Msg;
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        private bool General_Site_Inspec_Save(string _INSPECTIONTYPE, string _INSPECTIONPURPOSE, string _CONSUMERNAME,
                   string _CONSUMERADDRESS, string _CONSUMERPHONENO, string _SITEOBSERVATION, string _sSitePhotoImgPath, string _UPLOADBY,
                   string _INSPECTEDALONG, string _LINEMANNAME, string _CIRCLE, string _DIVISION) //updated by chesta on 19-09-2024
        {
            Random generator = new Random();
            int _autoID = generator.Next(1, 10000);

            using (con = new OleDbConnection(strConnection))
            {
                sql = "INSERT INTO mobint.MCR_GEN_SITE_INSPECTION(ID,INSPECTIONTYPE,INSPECTIONPURPOSE,CONSUMERNAME,CONSUMERADDRESS,";
                sql = sql + "CONSUMERPHONENO,SITEOBSERVATION, SitePhotoImgPath,UPLOADBY,INSPECTEDALONG,LINEMANNAME,CREATEDDATE";
                sql = sql + " ,CIRCLE,DIVISION)";
                sql = sql + " VALUES";
                sql = sql + "('" + _autoID + "','" + _INSPECTIONTYPE + "','" + _INSPECTIONPURPOSE + "','" + _CONSUMERNAME + "','" + _CONSUMERADDRESS + "','" + _CONSUMERPHONENO + "','" + _SITEOBSERVATION + "',";
                sql = sql + "'" + _sSitePhotoImgPath + "','" + _UPLOADBY + "','" + _INSPECTEDALONG + "','" + _LINEMANNAME + "',SYSDATE";
                sql = sql + ",'" + _CIRCLE + "' ,'" + _DIVISION + "')";

                cmd = new OleDbCommand(sql, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int i = cmd.ExecuteNonQuery();
                if (i != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Route("API/TF/CheckInsert_Burnt_Meter_DATA")]
        [HttpPost]
        public HttpResponseMessage CheckInsert_Burnt_Meter_DATA([FromBody] Burnt_Meter_Inspection CBMI_DT)
        {
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    sql = "select * from mobint.MCR_BURNTMETERIP_MASTER where METERNO = '" + CBMI_DT.METERNO + "'";
                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        objresult.Key = "Failed";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                    else
                    {
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        [Route("API/TF/Insert_Burnt_Meter_DATA")]
        [HttpPost]
        public HttpResponseMessage Insert_Burnt_Meter_DATA([FromBody] Burnt_Meter_Inspection BMI_DT)
        {
            try
            {
                Random generator = new Random();
                int _autoID = generator.Next(1, 10000);

                string ImageToBase = BMI_DT.BURNTMETERPHOTO;
                Byte[] _byImgBuild = null;
                string _sCancelImgPath = string.Empty;

                if (ImageToBase != null)
                {
                    _byImgBuild = Convert.FromBase64String(BMI_DT.BURNTMETERPHOTO);
                    _sCancelImgPath = byteArrayToImage(_byImgBuild, BMI_DT.METERNO + "_sCancelImgPath");
                }

                using (con = new OleDbConnection(strConnection))
                {
                    sql = " INSERT INTO mobint.MCR_BURNTMETERIP_MASTER(ID,METERNO ,RE_METERNO ,CA_NO ,RE_CA_NO ,SANCTION_LOAD," +
                        " MDI,CONNECTION_TYPE ,USEOFSUPPLY ,USESUPPLYREMARK ,METER_LOCATION ,METERLOC_REMARK ,METER_INSTALLATION ,SUPPLY_CONDITION ,CONDITIONOF_BURNTMETER ,CON_BURNTMETER_REMARK ," +
                        " BUSBAR_STATUS ,CABLE_CONDITION ,NEWCABLE_REQUIRED ,REQUIREDCABLE_SIZE ,REQUIREDCABLE_LENGTH ,MCB_STATUS ," +
                        " ELCB_STATUS ,SITE_OBSERVATION ,CREATEDDATE,CREATEDBY,DIVISION,BURNTMETERPHOTO,BUSBAR_CABLE_CONDITION) ";
                    sql = sql + " VALUES ";
                    sql = sql + " ('" + _autoID + "','" + BMI_DT.METERNO + "','" + BMI_DT.RE_METERNO + "','" + BMI_DT.CA_NO + "','" + BMI_DT.RE_CA_NO + "'," +
                        "'" + BMI_DT.SANCTION_LOAD + "','" + BMI_DT.MDI + "','" + BMI_DT.CONNECTION_TYPE + "','" + BMI_DT.USEOFSUPPLY + "','" + BMI_DT.USESUPPLYREMARK + "'," +
                        "'" + BMI_DT.METER_LOCATION + "','" + BMI_DT.METERLOC_REMARK + "','" + BMI_DT.METER_INSTALLATION + "','" + BMI_DT.SUPPLY_CONDITION + "'," +
                        "'" + BMI_DT.CONDITIONOF_BURNTMETER + "','" + BMI_DT.CON_BURNTMETER_REMARK + "'," +
                        "'" + BMI_DT.BUSBAR_STATUS + "','" + BMI_DT.CABLE_CONDITION + "','" + BMI_DT.NEWCABLE_REQUIRED + "','" + BMI_DT.REQUIREDCABLE_SIZE + "'," +
                        "'" + BMI_DT.REQUIREDCABLE_LENGTH + "','" + BMI_DT.MCB_STATUS + "','" + BMI_DT.ELCB_STATUS + "','" + BMI_DT.SITE_OBSERVATION + "'," +
                        "SYSDATE,'" + BMI_DT.CREATEDBY + "','" + BMI_DT.DIVISION + "','" + _sCancelImgPath + "','" + BMI_DT.BUSBAR_CABLE_CONDITION + "') ";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        if (Saved_BurntMeter_Photo(BMI_DT.RE_CA_NO, BMI_DT.METERNO, _sCancelImgPath, BMI_DT.CA_NO) == true)
                        {
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                        else
                        {
                            objresult.Key = "Failed";
                            objresult.Status = "421";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else
                    {
                        objresult.Key = "Failed";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        //Created By Chesta on 23-03-2023

        [Route("API/TF/Insert_Compliance_DATA")]
        [HttpPost]
        public HttpResponseMessage Insert_Compliance_DATA([FromBody] Compliance_Master CM_DT)
        {

            //trunc(TO_DATE('" + CM_DT.CreatedDate + "', 'DD-MON-YYYY'))
            try
            {
                Random generator = new Random();
                int _autoID = generator.Next(1, 10000);

                using (con = new OleDbConnection(strConnection))
                {
                    sql = "select * from mobint.MCR_Compliance_Master where trunc(CREATEDDATE) = trunc(TO_DATE('" + CM_DT.CreatedDate + "', 'DD-MON-YYYY')) and DIVISION = '" + CM_DT.DIVISION + "'";
                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count >= 1)
                    {
                        objresult.Key = "Failed";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                    else
                    {
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }


                    sql = " INSERT INTO mobint.MCR_Compliance_Master(ID ,Safety_Talk ,SafetyTalk_Remark ,Safety_Talk_Register,SafetyTalkRegister_Remark," +
                        "Checking_Record_Registers,Checking_Record_Remark,MaterialIssuedBR,MaterialIssuedBRRemark," +
                        "CableMaterialIssuans,CableMaterialIssuanceRemark,CableDrumIssueConsumpt,CableDrumIssueConsumptRemark," +
                        "Reusable_OldCableConsumpt,Reusable_OldCableRemark,CheckPreviousDay_MCRs,CheckPreviousDayMCRs_Remark,Inspection_BurntMeters," +
                        "Inspection_BurntMeters_Remark,Inspection_NewConnections,Inspection_NewConnectionRemark,NoOfVendorTeamsDeputed," +
                        "NoOfVendorTeamsDeputedRemark,NoOfVehicles_Deputed,NoOfVehicles_DeputedRemark,QCVisitSurveillance,QCVisitSurveillance_Remark," +
                        "NotificationInISUReason,NotificationInISUReason_Remark,ConsumerRemarksVerification,ConsumerRkVerificationRemark,CreatedDate,CREATEDBY,DIVISION,ANY_OTHER_ISSUEREMARK) ";
                    sql = sql + " VALUES ";
                    sql = sql + " ('" + _autoID + "','" + CM_DT.Safety_Talk + "','" + CM_DT.SafetyTalk_Remark + "','" + CM_DT.Safety_Talk_Register + "','" + CM_DT.SafetyTalkRegister_Remark + "'," +
                        "'" + CM_DT.Checking_Record_Registers + "','" + CM_DT.Checking_Record_Remark + "','" + CM_DT.MaterialIssuedBR + "','" + CM_DT.MaterialIssuedBRRemark + "'," +
                        "'" + CM_DT.CableMaterialIssuans + "','" + CM_DT.CableMaterialIssuanceRemark + "','" + CM_DT.CableDrumIssueConsumpt + "','" + CM_DT.CableDrumIssueConsumptRemark + "'," +
                        "'" + CM_DT.Reusable_OldCableConsumpt + "','" + CM_DT.Reusable_OldCableRemark + "','" + CM_DT.CheckPreviousDay_MCRs + "','" + CM_DT.CheckPreviousDayMCRs_Remark + "','" + CM_DT.Inspection_BurntMeters + "'," +
                        "'" + CM_DT.Inspection_BurntMeters_Remark + "','" + CM_DT.Inspection_NewConnections + "','" + CM_DT.Inspection_NewConnectionRemark + "','" + CM_DT.NoOfVendorTeamsDeputed + "'," +
                        "'" + CM_DT.NoOfVendorTeamsDeputedRemark + "','" + CM_DT.NoOfVehicles_Deputed + "','" + CM_DT.NoOfVehicles_DeputedRemark + "','" + CM_DT.QCVisitSurveillance + "','" + CM_DT.QCVisitSurveillance_Remark + "'," +
                        "'" + CM_DT.NotificationInISUReason + "','" + CM_DT.NotificationInISUReason_Remark + "','" + CM_DT.ConsumerRemarksVerification + "','" + CM_DT.ConsumerRkVerificationRemark + "',SYSDATE,'" + CM_DT.CREATEDBY + "','" + CM_DT.DIVISION + "','" + CM_DT.ANY_OTHER_ISSUEREMARK + "') ";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else
                    {
                        objresult.Key = "Failed";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        [Route("API/TF/Insert_KCC_MeterSafety")]
        [HttpPost]
        public HttpResponseMessage Insert_KCC_MeterSafety([FromBody] KCC_Meter_Safety KCC_MS)
        {
            try
            {
                Random generator = new Random();
                int _autoID = generator.Next(1, 10000);

                //string DOCToBase = DocToBase64();
                string ImageToBase = KCC_MS.METER_IMAGE;
                string ImageToBase1 = KCC_MS.METER_IMAGE1;
                string ImageToBase2 = KCC_MS.METER_IMAGE2;

                Byte[] _byImgBuild = null;
                Byte[] _byImgBuild1 = null;
                Byte[] _byImgBuild2 = null;

                string _sCancelImgPath = string.Empty;
                string _sCancelImgPath1 = string.Empty;
                string _sCancelImgPath2 = string.Empty;

                if (ImageToBase != null)
                {
                    _byImgBuild = Convert.FromBase64String(KCC_MS.METER_IMAGE);
                    _sCancelImgPath = byteArrayToImage(_byImgBuild, KCC_MS.UNIQUE_REF_NO + "_sCancelImgPath");
                }

                if (ImageToBase1 != null)
                {
                    _byImgBuild1 = Convert.FromBase64String(KCC_MS.METER_IMAGE1);
                    _sCancelImgPath1 = byteArrayToImage1(_byImgBuild1, KCC_MS.UNIQUE_REF_NO + "_sCancelImgPath1");
                }

                if (ImageToBase2 != null)
                {
                    _byImgBuild2 = Convert.FromBase64String(KCC_MS.METER_IMAGE2);
                    _sCancelImgPath2 = byteArrayToImage2(_byImgBuild2, KCC_MS.UNIQUE_REF_NO + "_sCancelImgPath2");
                }

                using (con = new OleDbConnection(strConnection))
                {
                    sql = " select * from mobint.KCC_INSPECTION where UNIQUE_REF_NO = '" + KCC_MS.UNIQUE_REF_NO + "'";
                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["METER_IMAGE"].ToString() != null && dt.Rows[0]["METER_IMAGE1"].ToString() == string.Empty && dt.Rows[0]["METER_IMAGE2"].ToString() == string.Empty && KCC_MS.METER_IMAGE1 != null)
                            sql = " update mobint.KCC_INSPECTION set METER_IMAGE1 = '" + _sCancelImgPath1 + "' where UNIQUE_REF_NO = '" + KCC_MS.UNIQUE_REF_NO + "' and CA_NO = '" + KCC_MS.CA_NO + "' and METER_NO = '" + KCC_MS.METER_NO + "'";
                        else if (dt.Rows[0]["METER_IMAGE"].ToString() != null && dt.Rows[0]["METER_IMAGE1"].ToString() != null && dt.Rows[0]["METER_IMAGE2"].ToString() == string.Empty && KCC_MS.METER_IMAGE2 != null)
                            sql = " update mobint.KCC_INSPECTION set METER_IMAGE2 = '" + _sCancelImgPath2 + "' where UNIQUE_REF_NO = '" + KCC_MS.UNIQUE_REF_NO + "' and CA_NO = '" + KCC_MS.CA_NO + "' and METER_NO = '" + KCC_MS.METER_NO + "'";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                        else
                        {
                            objresult.Key = "Failed";
                            objresult.Status = "421";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else
                    {
                        string Prefix = "0000000000" + KCC_MS.METER_NO;
                        string CA_NOPrefix = "000" + KCC_MS.CA_NO;
                        sql = " select * FROM mobint.KCC_INSPECTION  where (CA_NO = '" + KCC_MS.CA_NO + "' or CA_NO = '" + CA_NOPrefix + "') and (METER_NO = '" + KCC_MS.METER_NO + "' or METER_NO = '" + Prefix + "')";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        adp = new OleDbDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            objresult.Key = "Failed";
                            objresult.Status = "421";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }
                        else
                        {
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }

                        sql = " insert into MOBINT.KCC_INSPECTION (UNIQUE_REF_NO,CA_NO,METER_NO,DIVISION,NAME,ADDRESS,PHONE_NO,REQUEST_TYPE,METER_SAFE_LOCATION,METER_IMAGE,REMARKS,PUNCH_DATE,PUNCH_BY) ";
                        sql = sql + " VALUES ";
                        sql = sql + " ('" + KCC_MS.UNIQUE_REF_NO + "','" + KCC_MS.CA_NO + "','" + KCC_MS.METER_NO + "','" + KCC_MS.DIVISION + "','" + KCC_MS.NAME + "'," +
                            "'" + KCC_MS.ADDRESS + "','" + KCC_MS.PHONE_NO + "','" + KCC_MS.REQUEST_TYPE + "','" + KCC_MS.METER_SAFE_LOCATION + "'," +
                            "'" + _sCancelImgPath + "','" + KCC_MS.REMARKS + "',sysdate,'" + KCC_MS.PUNCH_BY + "') ";

                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                        else
                        {
                            objresult.Key = "Failed";
                            objresult.Status = "421";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        //Created By Chesta on 29-03-2023

        [Route("API/TF/GET_DIVISION_MASTER")]
        [HttpPost]
        public HttpResponseMessage GET_DIVISION_MASTER(Division div)
        {
            List<Division> divisionlist = new List<Division>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    sql = "SELECT DIST_CD, DIVISION_NAME,DIST_CIRCLE FROM MOBINT.mcr_division WHERE DIST_CIRCLE = '" + div.DIST_CIRCLE + "' and  STATUS='Y' ORDER BY DIVISION_NAME ";

                    cmd = new OleDbCommand(sql, con);
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
                            Division objCivil = new Division();
                            objCivil.DIST_CD = dt.Rows[i]["DIST_CD"].ToString();
                            objCivil.DIVISION_NAME = dt.Rows[i]["DIVISION_NAME"].ToString();
                            objCivil.DIST_CIRCLE = dt.Rows[i]["DIST_CIRCLE"].ToString();

                            divisionlist.Add(objCivil);
                            objresult.Result = divisionlist;
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        //Created By Chesta on 29-03-2023

        [Route("API/TF/GET_Meter_Activity")]
        [HttpPost]
        public HttpResponseMessage GET_Meter_Activity(MCR_Details mcrData)
        {
            List<MCR_Details> datalist = new List<MCR_Details>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    //sql = "SELECT AUART,I.ERDAT,I.DIVISION,I.ACCOUNT_CLASS,CA_NO,I.ORDERID,SANCTIONED_LOAD,NAME,TEL_NO,ADDRESS,METER_NO," +
                    //    "TERMINAL_SEAL,'' TrSeal2,METERBOXSEAL1,METERBOXSEAL2,BUSBARSEAL1,BUSBARSEAL2,BUS_BAR_DRUM_NO DURM_NO_PoleMtr,DURM_NO DURM_NO_BB_MTR,'' DURM_NO_OP,B_BAR_CABLE_SIZE CableSize_PoleMtr,CABLESIZE2 CableSize_BB_MTR," +
                    //    " OUTPUTCABLESIZE CableSize_OP,RUNNING_LENGTH_FROM_BB SrNoFrom_PoleMtr,RUNNINGLENGTHFROM SrNoFrom_BB_MTR,'' SrNoFrom_OP,RUNNING_LENGTH_TO_BB SrNoTo_PoleMtr,RUNNINGLENGTHTO SrNoTo_BB_MTR,'' SrNoTo_OP,OUTPUTBUSLENGTH Length_PoleMtr,CABLELENGTH Length_BB_MTR,OUTPUTCABLELENGTH Length_OP," +
                    //    " BB_CABLE_USED CableType,VALINSTALL_TYPE_BB Laying,BUS_BAR_NO,BUSBARSIZE,'' BBConfig,'' BusbarType,ELCB_INSTALLED,'' MCB,I.Punch_By,'' ID FROM mobint.MCR_Details D, mobint.MCR_INPUT_Details I " +
                    //    "where I.orderid = D.orderid and I.Orderid = '" + mcrData.ORDERID + "' ";

                    string Prefix = "0000000000" + mcrData.ORDERID;
                    sql = " SELECT AUART,I.ERDAT, D.ACTIVITY_DATE, D.TAB_LN_ID_NAME, I.DIVISION,I.ACCOUNT_CLASS,CA_NO,I.ORDERID,SANCTIONED_LOAD,NAME,TEL_NO,ADDRESS,METER_NO," +
                        "TERMINAL_SEAL,OTHER_SEAL TrSeal2,METERBOXSEAL1,METERBOXSEAL2,BUSBARSEAL1,BUSBARSEAL2," +
                        " DRUM_NUMBER_BB DURM_NO_PoleMtr,DURM_NO DURM_NO_BB_MTR,'' DURM_NO_OP,  CABLESIZE2 CableSize_PoleMtr," +
                        " B_BAR_CABLE_SIZE     CableSize_BB_MTR,OUTPUTCABLESIZE CableSize_OP,RUNNING_LENGTH_FROM_BB SrNoFrom_PoleMtr,RUNNINGLENGTHFROM SrNoFrom_BB_MTR, " +
                        " '' SrNoFrom_OP, RUNNINGLENGTHTO  SrNoTo_PoleMtr, RUNNING_LENGTH_TO_BB SrNoTo_BB_MTR,'' SrNoTo_OP,OUTPUTBUSLENGTH Length_PoleMtr, " +
                        " CABLELENGTH Length_BB_MTR,OUTPUTCABLELENGTH Length_OP,BB_CABLE_USED CableType,VALINSTALL_TYPE_BB Laying,BUS_BAR_NO,BUSBARSIZE, " +
                        " '' BBConfig,'' BusbarType,ELCB_INSTALLED,'' MCB,TAB_LN_ID_NAME Punch_By,TAB_LOGIN_ID ID FROM mobint.MCR_Details D, mobint.MCR_INPUT_Details I " +
                        " where I.orderid = D.orderid and (I.Orderid = '" + mcrData.ORDERID + "' or METER_NO = '" + mcrData.ORDERID + "' or METER_NO = '" + Prefix + "')";


                    cmd = new OleDbCommand(sql, con);
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
                            MCR_Details objMCRData = new MCR_Details();
                            objMCRData.AUART = dt.Rows[i]["AUART"].ToString();
                            objMCRData.ERDAT = dt.Rows[i]["ACTIVITY_DATE"].ToString();
                            objMCRData.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                            objMCRData.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                            objMCRData.CA_NO = dt.Rows[i]["CA_NO"].ToString();
                            objMCRData.ORDERID = dt.Rows[i]["ORDERID"].ToString();
                            objMCRData.SANCTIONED_LOAD = dt.Rows[i]["SANCTIONED_LOAD"].ToString();
                            objMCRData.NAME = dt.Rows[i]["NAME"].ToString();
                            objMCRData.TEL_NO = dt.Rows[i]["TEL_NO"].ToString();
                            objMCRData.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                            objMCRData.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                            objMCRData.TERMINAL_SEAL = dt.Rows[i]["TERMINAL_SEAL"].ToString();
                            objMCRData.TrSeal2 = dt.Rows[i]["TrSeal2"].ToString();
                            objMCRData.METERBOXSEAL1 = dt.Rows[i]["METERBOXSEAL1"].ToString();
                            objMCRData.METERBOXSEAL2 = dt.Rows[i]["METERBOXSEAL2"].ToString();
                            objMCRData.BUSBARSEAL1 = dt.Rows[i]["BUSBARSEAL1"].ToString();
                            objMCRData.BUSBARSEAL2 = dt.Rows[i]["BUSBARSEAL2"].ToString();
                            objMCRData.DURM_NO_PoleMtr = dt.Rows[i]["DURM_NO_PoleMtr"].ToString();
                            objMCRData.DURM_NO_BB_MTR = dt.Rows[i]["DURM_NO_BB_MTR"].ToString();
                            objMCRData.DURM_NO_OP = dt.Rows[i]["DURM_NO_OP"].ToString();
                            objMCRData.CableSize_PoleMtr = dt.Rows[i]["CableSize_PoleMtr"].ToString();
                            objMCRData.CableSize_BB_MTR = dt.Rows[i]["CableSize_BB_MTR"].ToString();
                            objMCRData.CableSize_OP = dt.Rows[i]["CableSize_OP"].ToString();
                            objMCRData.SrNoFrom_PoleMtr = dt.Rows[i]["SrNoFrom_PoleMtr"].ToString();
                            objMCRData.SrNoFrom_BB_MTR = dt.Rows[i]["SrNoFrom_BB_MTR"].ToString();
                            objMCRData.SrNoFrom_OP = dt.Rows[i]["SrNoFrom_OP"].ToString();
                            objMCRData.SrNoTo_PoleMtr = dt.Rows[i]["SrNoTo_PoleMtr"].ToString();
                            objMCRData.SrNoTo_BB_MTR = dt.Rows[i]["SrNoTo_BB_MTR"].ToString();
                            objMCRData.SrNoTo_OP = dt.Rows[i]["SrNoTo_OP"].ToString();
                            objMCRData.Length_PoleMtr = dt.Rows[i]["Length_PoleMtr"].ToString();
                            objMCRData.Length_BB_MTR = dt.Rows[i]["Length_BB_MTR"].ToString();
                            objMCRData.Length_OP = dt.Rows[i]["Length_OP"].ToString();
                            objMCRData.CableType = dt.Rows[i]["CableType"].ToString();
                            objMCRData.Laying = dt.Rows[i]["Laying"].ToString();
                            objMCRData.BUS_BAR_NO = dt.Rows[i]["BUS_BAR_NO"].ToString();
                            objMCRData.BUSBARSIZE = dt.Rows[i]["BUSBARSIZE"].ToString();
                            objMCRData.BBConfig = dt.Rows[i]["BBConfig"].ToString();
                            objMCRData.BusbarType = dt.Rows[i]["BusbarType"].ToString();
                            objMCRData.ELCB_INSTALLED = dt.Rows[i]["ELCB_INSTALLED"].ToString();
                            objMCRData.MCB = dt.Rows[i]["MCB"].ToString();
                            objMCRData.Punch_By = dt.Rows[i]["TAB_LN_ID_NAME"].ToString();
                            objMCRData.ID = dt.Rows[i]["ID"].ToString();

                            datalist.Add(objMCRData);
                            objresult.Result = datalist;
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        //Created by Faraz----Started

        [Route("API/TF/GET_Meter_Activity29072024")]
        [HttpPost]
        public HttpResponseMessage GET_Meter_Activitys(MCR_Details mcrData)
        {
            List<MCR_Details> datalist = new List<MCR_Details>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    //sql = "SELECT AUART,I.ERDAT,I.DIVISION,I.ACCOUNT_CLASS,CA_NO,I.ORDERID,SANCTIONED_LOAD,NAME,TEL_NO,ADDRESS,METER_NO," +
                    //    "TERMINAL_SEAL,'' TrSeal2,METERBOXSEAL1,METERBOXSEAL2,BUSBARSEAL1,BUSBARSEAL2,BUS_BAR_DRUM_NO DURM_NO_PoleMtr,DURM_NO DURM_NO_BB_MTR,'' DURM_NO_OP,B_BAR_CABLE_SIZE CableSize_PoleMtr,CABLESIZE2 CableSize_BB_MTR," +
                    //    " OUTPUTCABLESIZE CableSize_OP,RUNNING_LENGTH_FROM_BB SrNoFrom_PoleMtr,RUNNINGLENGTHFROM SrNoFrom_BB_MTR,'' SrNoFrom_OP,RUNNING_LENGTH_TO_BB SrNoTo_PoleMtr,RUNNINGLENGTHTO SrNoTo_BB_MTR,'' SrNoTo_OP,OUTPUTBUSLENGTH Length_PoleMtr,CABLELENGTH Length_BB_MTR,OUTPUTCABLELENGTH Length_OP," +
                    //    " BB_CABLE_USED CableType,VALINSTALL_TYPE_BB Laying,BUS_BAR_NO,BUSBARSIZE,'' BBConfig,'' BusbarType,ELCB_INSTALLED,'' MCB,I.Punch_By,'' ID FROM mobint.MCR_Details D, mobint.MCR_INPUT_Details I " +
                    //    "where I.orderid = D.orderid and I.Orderid = '" + mcrData.ORDERID + "' ";

                    // Fetch the PUNCH_MODE value
                    string Prefix2 = "0000000000" + mcrData.ORDERID;
                    //   string sql2 = "SELECT PUNCH_MODE FROM MOBINT.MCR_DETAILS WHERE ORDERID = ?";


                    string sql2 = "SELECT PUNCH_MODE FROM MOBINT.MCR_DETAILS WHERE ORDERID = ? OR DEVICENO = ?";

                    OleDbCommand cmd2 = new OleDbCommand(sql2, con);
                    cmd2.Parameters.AddWithValue("?", mcrData.ORDERID);
                    cmd2.Parameters.AddWithValue("?", Prefix2);


                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    object punchModeObj = cmd2.ExecuteScalar();
                    string punchMode = punchModeObj != null ? punchModeObj.ToString() : string.Empty;

                    // Define SQL query based on regex check for "Version" followed by any characters
                    string sql = "";
                    Regex versionRegex = new Regex(@"Version.*", RegexOptions.IgnoreCase);

                    if (versionRegex.IsMatch(punchMode))
                    {

                        string Prefix = "0000000000" + mcrData.ORDERID;
                        sql = " SELECT AUART,I.ERDAT, D.ACTIVITY_DATE, D.TAB_LN_ID_NAME, I.DIVISION,I.ACCOUNT_CLASS,CA_NO,I.ORDERID,SANCTIONED_LOAD,NAME,TEL_NO,ADDRESS,METER_NO," +
                       "TERMINAL_SEAL,OTHER_SEAL TrSeal2,METERBOXSEAL1,METERBOXSEAL2,BUSBARSEAL1,BUSBARSEAL2," +
                       " DRUMSIZE DURM_NO_PoleMtr,DURM_NO DURM_NO_BB_MTR,MTR_OUPT_CABLE_DRUMNO DURM_NO_OP,B_BAR_CABLE_SIZE CableSize_PoleMtr," +
                       " CABLESIZE2 CableSize_BB_MTR,OUTPUTCABLESIZE CableSize_OP,RUNNINGLENGTHFROM SrNoFrom_PoleMtr,RUNNING_LENGTH_FROM_BB SrNoFrom_BB_MTR, " +
                       " MTR_OUPT_CABLE_SRNO_FROM SrNoFrom_OP,BB_MTR_CABLE_SRNO_TO SrNoTo_PoleMtr, RUNNINGLENGTHTO SrNoTo_BB_MTR,MTR_OUPT_CABLE_SRNO_TO SrNoTo_OP,CABLELENGTH Length_PoleMtr, " +
                       " OUTPUTBUSLENGTH Length_BB_MTR,OUTPUTCABLELENGTH Length_OP,CABLE_LEN_USED CableType,OVERHEAD_UG Laying,BUS_BAR_NO,BUSBAR_WAY_ BUSBARSIZE, " +
                       " BUSBAR_PH BBConfig,BUSBAR_TYP BusbarType,ELCB_INSTALLED,MCB_INSTALLED MCB,TAB_LN_ID_NAME Punch_By,TAB_LOGIN_ID ID FROM mobint.MCR_Details D, mobint.MCR_INPUT_Details I " +
                       " where I.orderid = D.orderid and (I.Orderid = '" + mcrData.ORDERID + "' or METER_NO = '" + mcrData.ORDERID + "' or METER_NO = '" + Prefix + "')";

                        cmd = new OleDbCommand(sql, con);
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
                                MCR_Details objMCRData = new MCR_Details();
                                objMCRData.AUART = dt.Rows[i]["AUART"].ToString();
                                objMCRData.ERDAT = dt.Rows[i]["ACTIVITY_DATE"].ToString();
                                objMCRData.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                objMCRData.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                objMCRData.CA_NO = dt.Rows[i]["CA_NO"].ToString();
                                objMCRData.ORDERID = dt.Rows[i]["ORDERID"].ToString();
                                objMCRData.SANCTIONED_LOAD = dt.Rows[i]["SANCTIONED_LOAD"].ToString();
                                objMCRData.NAME = dt.Rows[i]["NAME"].ToString();
                                objMCRData.TEL_NO = dt.Rows[i]["TEL_NO"].ToString();
                                objMCRData.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                                objMCRData.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                objMCRData.TERMINAL_SEAL = dt.Rows[i]["TERMINAL_SEAL"].ToString();
                                objMCRData.TrSeal2 = dt.Rows[i]["TrSeal2"].ToString();
                                objMCRData.METERBOXSEAL1 = dt.Rows[i]["METERBOXSEAL1"].ToString();
                                objMCRData.METERBOXSEAL2 = dt.Rows[i]["METERBOXSEAL2"].ToString();
                                objMCRData.BUSBARSEAL1 = dt.Rows[i]["BUSBARSEAL1"].ToString();
                                objMCRData.BUSBARSEAL2 = dt.Rows[i]["BUSBARSEAL2"].ToString();
                                objMCRData.DURM_NO_PoleMtr = dt.Rows[i]["DURM_NO_PoleMtr"].ToString();//done
                                objMCRData.DURM_NO_BB_MTR = dt.Rows[i]["DURM_NO_BB_MTR"].ToString();//done
                                objMCRData.DURM_NO_OP = dt.Rows[i]["DURM_NO_OP"].ToString();//done
                                objMCRData.CableSize_PoleMtr = dt.Rows[i]["CableSize_PoleMtr"].ToString();//done
                                objMCRData.CableSize_BB_MTR = dt.Rows[i]["CableSize_BB_MTR"].ToString();//done
                                objMCRData.CableSize_OP = dt.Rows[i]["CableSize_OP"].ToString();//done
                                objMCRData.SrNoFrom_PoleMtr = dt.Rows[i]["SrNoFrom_PoleMtr"].ToString();//done
                                objMCRData.SrNoFrom_BB_MTR = dt.Rows[i]["SrNoFrom_BB_MTR"].ToString();//done
                                objMCRData.SrNoFrom_OP = dt.Rows[i]["SrNoFrom_OP"].ToString();//done
                                objMCRData.SrNoTo_PoleMtr = dt.Rows[i]["SrNoTo_PoleMtr"].ToString();//done
                                objMCRData.SrNoTo_BB_MTR = dt.Rows[i]["SrNoTo_BB_MTR"].ToString();//done
                                objMCRData.SrNoTo_OP = dt.Rows[i]["SrNoTo_OP"].ToString();//done
                                objMCRData.Length_PoleMtr = dt.Rows[i]["Length_PoleMtr"].ToString();//done
                                objMCRData.Length_BB_MTR = dt.Rows[i]["Length_BB_MTR"].ToString();//done
                                objMCRData.Length_OP = dt.Rows[i]["Length_OP"].ToString();//done
                                objMCRData.CableType = dt.Rows[i]["CableType"].ToString();//DONE
                                objMCRData.Laying = dt.Rows[i]["Laying"].ToString();//done
                                objMCRData.BUS_BAR_NO = dt.Rows[i]["BUS_BAR_NO"].ToString();//done
                                objMCRData.BUSBARSIZE = dt.Rows[i]["BUSBARSIZE"].ToString();//done
                                objMCRData.BBConfig = dt.Rows[i]["BBConfig"].ToString();//done
                                objMCRData.BusbarType = dt.Rows[i]["BusbarType"].ToString();//done
                                objMCRData.ELCB_INSTALLED = dt.Rows[i]["ELCB_INSTALLED"].ToString();//done
                                objMCRData.MCB = dt.Rows[i]["MCB"].ToString();//done
                                objMCRData.Punch_By = dt.Rows[i]["TAB_LN_ID_NAME"].ToString();
                                objMCRData.ID = dt.Rows[i]["ID"].ToString();

                                datalist.Add(objMCRData);
                                objresult.Result = datalist;
                                objresult.Key = "Success";
                                objresult.Status = "200";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            }
                        }
                        else if (dt.Rows.Count <= 0)
                        {
                            objresult.Key = "Record not found try again";
                            objresult.Status = "421";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }



                    }
                    else
                    {

                        string Prefix = "0000000000" + mcrData.ORDERID;
                        sql = " SELECT AUART,I.ERDAT, D.ACTIVITY_DATE, D.TAB_LN_ID_NAME, I.DIVISION,I.ACCOUNT_CLASS,CA_NO,I.ORDERID,SANCTIONED_LOAD,NAME,TEL_NO,ADDRESS,METER_NO," +
                       "TERMINAL_SEAL,OTHER_SEAL TrSeal2,METERBOXSEAL1,METERBOXSEAL2,BUSBARSEAL1,BUSBARSEAL2," +
                       " DRUM_NUMBER_BB DURM_NO_PoleMtr,DURM_NO DURM_NO_BB_MTR,'' DURM_NO_OP,  CABLESIZE2 CableSize_PoleMtr," +
                       " B_BAR_CABLE_SIZE     CableSize_BB_MTR,OUTPUTCABLESIZE CableSize_OP,RUNNING_LENGTH_FROM_BB SrNoFrom_PoleMtr,RUNNINGLENGTHFROM SrNoFrom_BB_MTR, " +
                       " '' SrNoFrom_OP, RUNNINGLENGTHTO  SrNoTo_PoleMtr, RUNNING_LENGTH_TO_BB SrNoTo_BB_MTR,'' SrNoTo_OP,OUTPUTBUSLENGTH Length_PoleMtr, " +
                       " CABLELENGTH Length_BB_MTR,OUTPUTCABLELENGTH Length_OP,BB_CABLE_USED CableType,VALINSTALL_TYPE_BB Laying,BUS_BAR_NO,BUSBARSIZE, " +
                       " '' BBConfig,'' BusbarType,ELCB_INSTALLED,'' MCB,TAB_LN_ID_NAME Punch_By,TAB_LOGIN_ID ID FROM mobint.MCR_Details D, mobint.MCR_INPUT_Details I " +
                       " where I.orderid = D.orderid and (I.Orderid = '" + mcrData.ORDERID + "' or METER_NO = '" + mcrData.ORDERID + "' or METER_NO = '" + Prefix + "')";

                        cmd = new OleDbCommand(sql, con);
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
                                MCR_Details objMCRData = new MCR_Details();
                                objMCRData.AUART = dt.Rows[i]["AUART"].ToString();
                                objMCRData.ERDAT = dt.Rows[i]["ACTIVITY_DATE"].ToString();
                                objMCRData.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                objMCRData.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                objMCRData.CA_NO = dt.Rows[i]["CA_NO"].ToString();
                                objMCRData.ORDERID = dt.Rows[i]["ORDERID"].ToString();
                                objMCRData.SANCTIONED_LOAD = dt.Rows[i]["SANCTIONED_LOAD"].ToString();
                                objMCRData.NAME = dt.Rows[i]["NAME"].ToString();
                                objMCRData.TEL_NO = dt.Rows[i]["TEL_NO"].ToString();
                                objMCRData.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                                objMCRData.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                objMCRData.TERMINAL_SEAL = dt.Rows[i]["TERMINAL_SEAL"].ToString();
                                objMCRData.TrSeal2 = dt.Rows[i]["TrSeal2"].ToString();
                                objMCRData.METERBOXSEAL1 = dt.Rows[i]["METERBOXSEAL1"].ToString();
                                objMCRData.METERBOXSEAL2 = dt.Rows[i]["METERBOXSEAL2"].ToString();
                                objMCRData.BUSBARSEAL1 = dt.Rows[i]["BUSBARSEAL1"].ToString();
                                objMCRData.BUSBARSEAL2 = dt.Rows[i]["BUSBARSEAL2"].ToString();
                                objMCRData.DURM_NO_PoleMtr = dt.Rows[i]["DURM_NO_PoleMtr"].ToString();
                                objMCRData.DURM_NO_BB_MTR = dt.Rows[i]["DURM_NO_BB_MTR"].ToString();
                                objMCRData.DURM_NO_OP = dt.Rows[i]["DURM_NO_OP"].ToString();
                                objMCRData.CableSize_PoleMtr = dt.Rows[i]["CableSize_PoleMtr"].ToString();
                                objMCRData.CableSize_BB_MTR = dt.Rows[i]["CableSize_BB_MTR"].ToString();
                                objMCRData.CableSize_OP = dt.Rows[i]["CableSize_OP"].ToString();
                                objMCRData.SrNoFrom_PoleMtr = dt.Rows[i]["SrNoFrom_PoleMtr"].ToString();
                                objMCRData.SrNoFrom_BB_MTR = dt.Rows[i]["SrNoFrom_BB_MTR"].ToString();
                                objMCRData.SrNoFrom_OP = dt.Rows[i]["SrNoFrom_OP"].ToString();
                                objMCRData.SrNoTo_PoleMtr = dt.Rows[i]["SrNoTo_PoleMtr"].ToString();
                                objMCRData.SrNoTo_BB_MTR = dt.Rows[i]["SrNoTo_BB_MTR"].ToString();
                                objMCRData.SrNoTo_OP = dt.Rows[i]["SrNoTo_OP"].ToString();
                                objMCRData.Length_PoleMtr = dt.Rows[i]["Length_PoleMtr"].ToString();
                                objMCRData.Length_BB_MTR = dt.Rows[i]["Length_BB_MTR"].ToString();
                                objMCRData.Length_OP = dt.Rows[i]["Length_OP"].ToString();
                                objMCRData.CableType = dt.Rows[i]["CableType"].ToString();
                                objMCRData.Laying = dt.Rows[i]["Laying"].ToString();
                                objMCRData.BUS_BAR_NO = dt.Rows[i]["BUS_BAR_NO"].ToString();
                                objMCRData.BUSBARSIZE = dt.Rows[i]["BUSBARSIZE"].ToString();
                                objMCRData.BBConfig = dt.Rows[i]["BBConfig"].ToString();
                                objMCRData.BusbarType = dt.Rows[i]["BusbarType"].ToString();
                                objMCRData.ELCB_INSTALLED = dt.Rows[i]["ELCB_INSTALLED"].ToString();
                                objMCRData.MCB = dt.Rows[i]["MCB"].ToString();
                                objMCRData.Punch_By = dt.Rows[i]["TAB_LN_ID_NAME"].ToString();
                                objMCRData.ID = dt.Rows[i]["ID"].ToString();

                                datalist.Add(objMCRData);
                                objresult.Result = datalist;
                                objresult.Key = "Success";
                                objresult.Status = "200";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            }
                        }
                        else if (dt.Rows.Count <= 0)
                        {
                            objresult.Key = "Record not found try again";
                            objresult.Status = "421";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }


                    }




                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        //Created by Faraz----ended


        [Route("API/TF/GET_KCC_Details")]
        [HttpPost]
        public HttpResponseMessage GET_KCC_Details(KCC_FETCH_DETAILS KCCData)
        {
            List<KCC_FETCH_DETAILS> datalist = new List<KCC_FETCH_DETAILS>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    string Prefix = "0000000000" + KCCData.CA_NO;
                    string CA_NOPrefix = "000" + KCCData.CA_NO;
                    sql = " select CA_NO,METER_NO,DIVISION,NAME,ADDRESS,TEL_NO FROM mobint.MCR_INPUT_Details  where (CA_NO = '" + KCCData.CA_NO + "' or CA_NO = '" + CA_NOPrefix + "') or (METER_NO = '" + KCCData.CA_NO + "' or METER_NO = '" + Prefix + "')";
                    cmd = new OleDbCommand(sql, con);
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
                            KCC_FETCH_DETAILS objKCCData = new KCC_FETCH_DETAILS();
                            objKCCData.CA_NO = dt.Rows[i]["CA_NO"].ToString();
                            objKCCData.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                            objKCCData.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                            objKCCData.NAME = dt.Rows[i]["NAME"].ToString();
                            objKCCData.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                            objKCCData.PHONE_NO = dt.Rows[i]["TEL_NO"].ToString();
                            datalist.Add(objKCCData);
                            objresult.Result = datalist;
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        [Route("API/TF/Check_OrderID")]
        [HttpPost]
        public HttpResponseMessage Check_OrderID(MCR_Details mcrData)
        {
            List<MCR_Details> datalist = new List<MCR_Details>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    string Prefix = "0000000000" + mcrData.METER_NO;
                    if (mcrData.TABTYPE.ToUpper() == "TAB")
                        sql = "select * from mobint.MCR_INSPECTION_MASTER where ORDER_NO = '" + mcrData.ORDERID + "' or (METER_NO = '" + mcrData.METER_NO + "' or METER_NO = '" + Prefix + "')";
                    else if (mcrData.TABTYPE.ToUpper() == "NONTAB")
                        sql = "select * from mobint.MCR_INSPECTION_MASTER where ORDER_NO = '" + mcrData.ORDERID + "' and (METER_NO = '" + mcrData.METER_NO + "' or METER_NO = '" + Prefix + "')";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                    else
                    {
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        //Created by Chesta on 21-04-2023 
        [Route("API/TF/GET_DOCUMENT_MASTER")]
        [HttpGet]
        public HttpResponseMessage GET_DOCUMENT_MASTER()
        {
            List<Document> documentlist = new List<Document>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    sql = "SELECT DOC_ID, DOC_NAME FROM MOBINT.MCR_DOCUMENT_MASTER WHERE STATUS='Y' ORDER BY DOC_NAME ";

                    cmd = new OleDbCommand(sql, con);
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
                            Document objCivil = new Document();
                            objCivil.DOC_ID = dt.Rows[i]["DOC_ID"].ToString();
                            objCivil.DOC_NAME = dt.Rows[i]["DOC_NAME"].ToString();

                            documentlist.Add(objCivil);
                            objresult.Result = documentlist;
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        public string DocToBase64()
        {
            string file = "";
            string path = "D:\\ELEC.pdf";
            Byte[] bytes = File.ReadAllBytes(path);
            return file = Convert.ToBase64String(bytes);
        }

        [Route("API/TF/Insert_DOCUMENTUPLOAD_MASTER")]
        [HttpPost]
        public HttpResponseMessage Insert_DOCUMENTUPLOAD_MASTER([FromBody] DocumentUpload CM_DT)
        {
            try
            {
                Random generator = new Random();
                int _autoID = generator.Next(1, 10000);

                //string DOCToBase = DocToBase64();
                string ImageToBase = CM_DT.DOC_PHOTO;

                Byte[] _byImgBuild = null;
                string _sCancelImgPath = string.Empty;

                if (CM_DT.DOC_TYPE.ToUpper() == "FILE")
                {
                    if (ImageToBase != null)
                    {
                        _byImgBuild = Convert.FromBase64String(CM_DT.DOC_PHOTO);
                        _sCancelImgPath = byteArrayToDOC(_byImgBuild, _autoID.ToString() + "_sCancelImgPath", CM_DT.DOC_EXT);
                    }
                }
                else if (CM_DT.DOC_TYPE.ToUpper() == "IMAGE")
                {
                    if (ImageToBase != null)
                    {
                        _byImgBuild = Convert.FromBase64String(CM_DT.DOC_PHOTO);
                        _sCancelImgPath = byteArrayToImage(_byImgBuild, _autoID.ToString() + "_sCancelImgPath");
                    }
                }

                using (con = new OleDbConnection(strConnection))
                {
                    sql = " INSERT INTO MCR_DOCUMENTUPLOAD_MASTER (DOC_UPID,COMPANYNAME,CIRCLE,DIVISION,DOCUMENT_DATE,DOCUMENT_NAME,DETAIL_REMARKS,UPLOAD,DOC_PHOTO,DOC_TYPE,DOC_EXT) ";
                    sql = sql + " VALUES ";
                    sql = sql + " ('" + _autoID + "','" + CM_DT.CompanyName + "','" + CM_DT.Circle + "','" + CM_DT.Division + "',to_date('" + CM_DT.Document_Date + "','dd-mm-yyyy'),'" + CM_DT.Document_Name + "','" + CM_DT.DETAIL_REMARKS + "','" + CM_DT.UploadBy + "','" + _sCancelImgPath + "','" + CM_DT.DOC_TYPE + "','" + CM_DT.DOC_EXT + "') ";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else
                    {
                        objresult.Key = "Failed";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        #region Divyesh Jain

        [Route("API/TF/GET_ITHELP_LOGIN")]
        [HttpPost]
        public HttpResponseMessage GET_ITHELP_LOGIN(ITHELPDESK_LOGIN IT_Help)
        {
            try
            {
                using (con = new OleDbConnection(strConnectionITHELP))
                {
                    sql = "SELECT NAME,USER_ID,ROLE,EMAIL_ID, IMEI_NUMBER FROM CCM.ITHLPDSK_LOGIN_MASTER WHERE USER_ID='" + IT_Help.USER_ID + "' AND PASSWORD='" + IT_Help.PASSWORD + "' AND ACTIVE='Y' ";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        objresult.Result = dt;
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        [Route("API/TF/GET_ITHELP_DATA")]
        [HttpPost]
        public HttpResponseMessage GET_ITHELP_DATA(GET_ITHELP_DATA IT_Data)
        {
            try
            {
                using (con = new OleDbConnection(strConnectionITHELP))
                {
                    string EndDate = Convert.ToString(DateTime.Now.Date.ToString("dd-MMM-yyyy"));
                    //DateTime EndDate = DateTime.Now.Date;
                    string FromDate = Convert.ToString(Convert.ToDateTime(EndDate).AddDays(-30).ToString("dd-MMM-yyyy"));

                    //string sql = " select '0' SNO,HELPDESK_ID,INITIATOR_NAME,MOBILE_NO, ";
                    //sql = sql + " (SELECT DESCRIPTION FROM ccm.TASK_COR_SYS_TYPE WHERE CORETYPE='HELPDESK_CAT' AND CODE=PROB_TYPE AND ROWNUM <2)PROB_TYPE_DESC,PROB_SUBTYPE_DESC, ";
                    //sql = sql + " (SELECT DESCRIPTION FROM ccm.TASK_COR_SYS_TYPE WHERE CORETYPE='HELPDESK_PRBAR' AND COMPANY_CODE=PROB_SUBTYPE AND CODE=PROB_AREA AND ROWNUM <2)PROB_AREA_DESC, ";
                    //sql = sql + " DIVISION,(select DESCRIPTION from ccm.TASK_COR_SYS_TYPE where CORETYPE='IT_SUPPORT_LOC' and CODE=LOC_CODE)LOCATION, ";
                    //sql = sql + " TO_CHAR(ENTRY_DATE,'dd-Mon-yyyy') ENTRY_DT, (case when STATUS ='P' then 'Pending' when STATUS ='R' then 'Re-Assign' when STATUS ='I' then 'Inprogress' when STATUS ='C' then 'Closed' when STATUS ='PC' then 'Partial-Closed' end) STATUS, ";
                    //sql = sql + " (select Distinct PROB_CAT from CCM.IT_HELPDESK_REQ_LOG where HELPDSK_ID = IHM.HELPDESK_ID and rownum <= 1) PROB_CAT, ";
                    //sql = sql + " (select Distinct(case when PROB_CAT = 'DATA CENTER' then 'D' when PROB_CAT = 'HARDWARE' then 'H' ";
                    //sql = sql + " when PROB_CAT = 'NETWORK' then 'N' when PROB_CAT = 'NON-SAP' then 'NS' when PROB_CAT = 'SAP' then 'S' ";
                    //sql = sql + " when PROB_CAT = 'TELECOM' then 'T' end) PROB_CAT_CODE ";
                    //sql = sql + " from CCM.IT_HELPDESK_REQ_LOG where HELPDSK_ID = IHM.HELPDESK_ID and rownum <= 1) PROB_CAT_CODE,  PROB_AREA, PROB_TYPE, PROB_SUBTYPE  ";
                    //sql = sql + " from ccm.IT_HELPDESK_MGM IHM where ASSIGN_TO='" + IT_Data.ASSIGN_TO + "'";
                    ////sql = sql + " and TRUNC(ENTRY_DATE) >= trunc(to_date('" + FromDate + "', 'DD-MON-YYYY')) and trunc(ENTRY_DATE) <= trunc(to_date('" + EndDate + "', 'DD-MON-YYYY'))";
                    //sql = sql + " and STATUS !='C'";

                    string sql = " select '0' SNO,HELPDESK_ID,INITIATOR_NAME,MOBILE_NO, ";
                    sql = sql + " (SELECT DESCRIPTION FROM ccm.TASK_COR_SYS_TYPE WHERE CORETYPE='HELPDESK_CAT' AND CODE=PROB_TYPE AND ROWNUM <2)PROB_TYPE_DESC,PROB_SUBTYPE_DESC, ";
                    sql = sql + " (SELECT DESCRIPTION FROM ccm.TASK_COR_SYS_TYPE WHERE CORETYPE='HELPDESK_PRBAR' AND COMPANY_CODE=PROB_SUBTYPE AND CODE=PROB_AREA AND ROWNUM <2)PROB_AREA_DESC, ";
                    sql = sql + " DIVISION,(select DESCRIPTION from ccm.TASK_COR_SYS_TYPE where CORETYPE='IT_SUPPORT_LOC' and CODE=LOC_CODE)LOCATION, ";
                    sql = sql + " TO_CHAR(ENTRY_DATE,'dd-Mon-yyyy') ENTRY_DT, (case when STATUS ='P' then 'Pending' when STATUS ='R' then 'Re-Assign' when STATUS ='I' then 'Inprogress' when STATUS ='C' then 'Closed' when STATUS ='PC' then 'Partial-Closed' end) STATUS, ";
                    sql = sql + " (select Distinct PROB_CAT from CCM.IT_HELPDESK_REQ_LOG where HELPDSK_ID = IHM.HELPDESK_ID and DC_TIMESTAMP = (select max(DC_TIMESTAMP) from CCM.IT_HELPDESK_REQ_LOG where HELPDSK_ID = IHM.HELPDESK_ID)) PROB_CAT, ";
                    sql = sql + " (select Distinct(case when PROB_CAT = 'DATA CENTER' then 'D' when PROB_CAT = 'HARDWARE' then 'H' ";
                    sql = sql + " when PROB_CAT = 'NETWORK' then 'N' when PROB_CAT = 'NON-SAP' then 'NS' when PROB_CAT = 'SAP' then 'S' ";
                    sql = sql + " when PROB_CAT = 'TELECOM' then 'T' end) PROB_CAT_CODE ";
                    sql = sql + " from CCM.IT_HELPDESK_REQ_LOG where HELPDSK_ID = IHM.HELPDESK_ID and rownum <= 1) PROB_CAT_CODE,  PROB_AREA, PROB_TYPE, PROB_SUBTYPE  ";
                    sql = sql + " from ccm.IT_HELPDESK_MGM IHM where ASSIGN_TO='" + IT_Data.ASSIGN_TO + "'";
                    //sql = sql + " and TRUNC(ENTRY_DATE) >= trunc(to_date('" + FromDate + "', 'DD-MON-YYYY')) and trunc(ENTRY_DATE) <= trunc(to_date('" + EndDate + "', 'DD-MON-YYYY'))";
                    sql = sql + " and STATUS !='C'";



                    if (IT_Data.HELPDESK_ID.Trim() != "")
                    {
                        sql = sql + " and HELPDESK_ID like '%" + IT_Data.HELPDESK_ID + "%' ";
                    }

                    sql = sql + " order by ENTRY_DATE";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        objresult.Result = dt;
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        [Route("API/TF/GET_REPORT_DATA")]
        [HttpPost]
        public HttpResponseMessage GET_REPORT_DATA(GET_REPORT_DATA RT_Data)
        {
            try
            {
                using (con = new OleDbConnection(strConnectionITHELP))
                {
                    //string sql = @" select Distinct ";
                    //sql = sql + " (select Distinct count(*) OpenCompl from ccm.IT_HELPDESK_MGM ";
                    //sql = sql + " where trunc(ENTRY_DATE) = trunc(sysdate) and Status != 'C' and ASSIGN_TO = '" + RT_Data.ASSIGN_TO + "') OpenCompl, ";
                    //sql = sql + " (select Distinct count(*) Within24 from ccm.IT_HELPDESK_MGM ";
                    //sql = sql + " where (trunc(sysdate) - trunc(ENTRY_DATE)) = 1 and Status != 'C' and ASSIGN_TO = '" + RT_Data.ASSIGN_TO + "') Within24, ";
                    //sql = sql + " (select Distinct count(*) Within48 from ccm.IT_HELPDESK_MGM ";
                    //sql = sql + " where(trunc(sysdate) - trunc(ENTRY_DATE)) = 2 and Status != 'C' and ASSIGN_TO = '" + RT_Data.ASSIGN_TO + "')Within48, ";
                    //sql = sql + " (select Distinct count(*) from ccm.IT_HELPDESK_MGM ";
                    //sql = sql + " where ASSIGN_TO = '" + RT_Data.ASSIGN_TO + "') TotalTickets, ";
                    //sql = sql + " (SELECT COUNT(*) FROM (SELECT DISTINCT TO_CHAR(ENTRY_DATE, 'DD/MM/YYYY') AS ENTRY_DATE               ";
                    //sql = sql + " from ccm.IT_HELPDESK_MGM                                                                                                 ";
                    //sql = sql + " where ASSIGN_TO = '" + RT_Data.ASSIGN_TO + "' and STATUS = 'C' and trunc(ENTRY_DATE) between trunc(sysdate, 'mm')/*current month*/ AND SYSDATE";
                    //sql = sql + " )) ActualWorking ";
                    //sql = sql + " from ccm.IT_HELPDESK_MGM";


                    //string EndDate = Convert.ToString(DateTime.Now.Date.ToString("dd-MMM-yyyy"));
                    //string FromDate = Convert.ToString(Convert.ToDateTime(EndDate).AddDays(-30).ToString("dd-MMM-yyyy"));
                    //string sql = @" select Distinct ";
                    //sql = sql + " (select Distinct count(*) OpenCompl from ccm.IT_HELPDESK_MGM ";
                    //sql = sql + " where TRUNC(ENTRY_DATE) >= trunc(to_date('" + FromDate + "', 'DD-MON-YYYY')) and trunc(ENTRY_DATE) <= trunc(to_date('" + EndDate + "', 'DD-MON-YYYY')) and Status != 'C' and ASSIGN_TO = '" + RT_Data.ASSIGN_TO + "') OpenCompl, ";
                    //sql = sql + " (select Distinct count(*) Within24 from ccm.IT_HELPDESK_MGM ";
                    //sql = sql + " where TRUNC(ENTRY_DATE) >= trunc(to_date('" + FromDate + "', 'DD-MON-YYYY')) and trunc(ENTRY_DATE) <= trunc(to_date('" + EndDate + "', 'DD-MON-YYYY')) and Status = 'PC' and ASSIGN_TO = '" + RT_Data.ASSIGN_TO + "') Partial, ";
                    //sql = sql + " (select Distinct count(*) Within48 from ccm.IT_HELPDESK_MGM ";
                    //sql = sql + " where TRUNC(ENTRY_DATE) >= trunc(to_date('" + FromDate + "', 'DD-MON-YYYY')) and trunc(ENTRY_DATE) <= trunc(to_date('" + EndDate + "', 'DD-MON-YYYY')) and Status = 'C' and ASSIGN_TO = '" + RT_Data.ASSIGN_TO + "') Closed ";
                    //sql = sql + " from ccm.IT_HELPDESK_MGM";

                    string EndDate = Convert.ToString(DateTime.Now.Date.ToString("dd-MMM-yyyy"));
                    string FromDate = Convert.ToString(Convert.ToDateTime(EndDate).AddDays(-30).ToString("dd-MMM-yyyy"));
                    string sql = @" select Distinct ";
                    sql = sql + " (select Distinct count(*) OpenCompl from ccm.IT_HELPDESK_MGM ";
                    sql = sql + " where Status != 'C' and ASSIGN_TO = '" + RT_Data.ASSIGN_TO + "') OpenCompl, ";
                    sql = sql + " (select Distinct count(*) Within24 from ccm.IT_HELPDESK_MGM ";
                    sql = sql + " where TRUNC(ENTRY_DATE) >= trunc(to_date('" + FromDate + "', 'DD-MON-YYYY')) and trunc(ENTRY_DATE) <= trunc(to_date('" + EndDate + "', 'DD-MON-YYYY')) and Status = 'PC' and ASSIGN_TO = '" + RT_Data.ASSIGN_TO + "') Partial, ";
                    sql = sql + " (select Distinct count(*) Within48 from ccm.IT_HELPDESK_MGM ";
                    sql = sql + " where TRUNC(ENTRY_DATE) >= trunc(to_date('" + FromDate + "', 'DD-MON-YYYY')) and trunc(ENTRY_DATE) <= trunc(to_date('" + EndDate + "', 'DD-MON-YYYY')) and Status = 'C' and ASSIGN_TO = '" + RT_Data.ASSIGN_TO + "') Closed ";
                    sql = sql + " from ccm.IT_HELPDESK_MGM";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        objresult.Result = dt;
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        [Route("API/TF/GET_CATEGORY_TYPE")]
        [HttpPost]
        public HttpResponseMessage GET_CATEGORY_TYPE(GET_CATEGORY_TYPE CT_Data)
        {
            try
            {
                using (con = new OleDbConnection(strConnectionITHELP))
                {
                    string sql = @" SELECT '0' CODE,'-SELECT-' DESCRIPTION ";
                    sql = sql + " FROM DUAL UNION ";
                    sql = sql + " select Distinct CODE,DESCRIPTION ";
                    sql = sql + " from ccm.TASK_COR_SYS_TYPE ";
                    sql = sql + " where CORETYPE='HELPDESK_CAT' ";
                    sql = sql + " AND COMPANY_CODE='" + CT_Data.CATEGORY_CODE + "' order by DESCRIPTION";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        objresult.Result = dt;
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        [Route("API/TF/UPDATE_COMPLAINT_STATUS")]
        [HttpPost]
        public HttpResponseMessage UPDATE_COMPLAINT_STATUS(UPDATE_COMPLAINT_STATUS UCS_Data)
        {
            try
            {
                using (con = new OleDbConnection(strConnectionITHELP))
                {
                    string sql = "";


                    if (UCS_Data.ASSIGN_TO != "" && UCS_Data.STATUS.ToUpper() == "R")
                    {
                        sql = "INSERT INTO CCM.IT_HELPDESK_REQ_LOG(HELPDSK_ID, HDSK_COMMENT, EMP_CODE, EMPNAME, SATAUS,ASSIGN_EMP_CODE,ASSIGN_EMPNAME,PROB_AREA,PROB_SUBTYPE,PROB_TYPE,PROB_CAT,CLOSED_BY) VALUES " +
                            " ('" + UCS_Data.HELPDESK_ID + "', '" + UCS_Data.REMARK + "', '" + UCS_Data.EMP_CODE + "', '" + UCS_Data.EMPNAME + "', '" + UCS_Data.STATUS + "', '" + UCS_Data.ASSIGN_TO + "'," +
                            " '" + UCS_Data.ASSIGN_NAME + "', '" + UCS_Data.PROBAREA_TEXT + "', '" + UCS_Data.PROBSUBTYPE_TEXT + "', '" + UCS_Data.PROBTYPE_TEXT + "', '" + UCS_Data.PROBCAT_TEXT + "', '') ";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            sql = @" update ccm.IT_HELPDESK_MGM set STATUS='" + UCS_Data.STATUS + "' , UPDATE_DATE= sysdate, REMARKS_DESC = '" + UCS_Data.REMARK + "', REASON = '" + UCS_Data.REASON + "', PROB_SUBTYPE='" + UCS_Data.SUBTYPE + "', PROB_SUBTYPE_DESC = '" + UCS_Data.SUBTYPE_DESC + "', PROB_TYPE='" + UCS_Data.TYPE + "', PROB_AREA='" + UCS_Data.PROBLEMAREA + "', SUPPORT_TYPE='" + UCS_Data.SUPPORTTYPE + "', ASSIGN_TO='" + UCS_Data.ASSIGN_TO + "' where HELPDESK_ID='" + UCS_Data.HELPDESK_ID + "'";
                            cmd = new OleDbCommand(sql, con);
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                objresult.Result = dt;
                                objresult.Key = "Success";
                                objresult.Status = "200";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            }
                            else if (dt.Rows.Count <= 0)
                            {
                                objresult.Key = "Record not found try again";
                                objresult.Status = "421";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                                return Msg;
                            }
                        }
                        else if (dt.Rows.Count <= 0)
                        {
                            objresult.Key = "Record not found try again";
                            objresult.Status = "421";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }

                    }
                    else if (UCS_Data.ASSIGN_TO == "" && (UCS_Data.STATUS.ToUpper() == "C" || UCS_Data.STATUS.ToUpper() == "PC"))
                    {
                        sql = "INSERT INTO CCM.IT_HELPDESK_REQ_LOG(HELPDSK_ID, HDSK_COMMENT, EMP_CODE, EMPNAME, SATAUS,ASSIGN_EMP_CODE,ASSIGN_EMPNAME,PROB_AREA,PROB_SUBTYPE,PROB_TYPE,PROB_CAT,CLOSED_BY) VALUES " +
                           " ('" + UCS_Data.HELPDESK_ID + "', '" + UCS_Data.REMARK + "', '" + UCS_Data.EMP_CODE + "', '" + UCS_Data.EMPNAME + "', '" + UCS_Data.STATUS + "', '" + UCS_Data.ASSIGN_TO + "'," +
                           " '" + UCS_Data.ASSIGN_NAME + "', '" + UCS_Data.PROBAREA_TEXT + "', '" + UCS_Data.PROBSUBTYPE_TEXT + "', '" + UCS_Data.PROBTYPE_TEXT + "', '" + UCS_Data.PROBCAT_TEXT + "', '" + UCS_Data.USER_IDTEXT + "') ";
                        cmd = new OleDbCommand(sql, con);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            sql = @" update ccm.IT_HELPDESK_MGM set STATUS='" + UCS_Data.STATUS + "' , UPDATE_DATE= sysdate, REMARKS_DESC = '" + UCS_Data.REMARK + "', REASON = '" + UCS_Data.REASON + "', PROB_SUBTYPE='" + UCS_Data.SUBTYPE + "', PROB_SUBTYPE_DESC = '" + UCS_Data.SUBTYPE_DESC + "', PROB_TYPE='" + UCS_Data.TYPE + "', PROB_AREA='" + UCS_Data.PROBLEMAREA + "', SUPPORT_TYPE='" + UCS_Data.SUPPORTTYPE + "' where HELPDESK_ID='" + UCS_Data.HELPDESK_ID + "'";

                            cmd = new OleDbCommand(sql, con);
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                objresult.Result = dt;
                                objresult.Key = "Success";
                                objresult.Status = "200";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            }
                            else if (dt.Rows.Count <= 0)
                            {
                                objresult.Key = "Record not found try again";
                                objresult.Status = "421";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                                return Msg;
                            }
                        }
                        else if (dt.Rows.Count <= 0)
                        {
                            objresult.Key = "Record not found try again";
                            objresult.Status = "421";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }
                    }

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        objresult.Result = dt;
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        //[Route("API/TF/GETHELPDESK_DROPDOWNMASTER")]
        //[HttpGet]
        //public HttpResponseMessage GETHELPDESK_DROPDOWNMASTER()
        //{
        //    try
        //    {
        //        using (con = new OleDbConnection(strConnectionITHELP))
        //        {
        //            //sql = "select code,DESCRIPTION,CORETYPE,code as VALUE from ccm.TASK_COR_SYS_TYPE order by DESCRIPTION  ";



        //            cmd = new OleDbCommand(sql, con);
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            adp = new OleDbDataAdapter(cmd);
        //            adp.Fill(dt);
        //            if (dt.Rows.Count > 0)
        //            {
        //                objresult.Result = dt;
        //                objresult.Key = "Success";
        //                objresult.Status = "200";
        //                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);

        //            }
        //            else if (dt.Rows.Count <= 0)
        //            {
        //                objresult.Key = "Record not found try again";
        //                objresult.Status = "421";
        //                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
        //                return Msg;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objresult.Key = ex.Message.ToString();
        //        objresult.Status = "421";
        //        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
        //        return Msg;
        //    }
        //    finally
        //    {
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //    }
        //    return Msg;
        //}


        [Route("API/TF/GETHELPDESK_DROPDOWNMASTER")]
        [HttpPost]
        public HttpResponseMessage GETHELPDESK_DROPDOWNMASTER(GETHELPDESK_DROPDOWNMASTER GDM_Data)
        {
            try
            {
                using (con = new OleDbConnection(strConnectionITHELP))
                {
                    string sql = "";
                    if (GDM_Data.HELPDESK_ID != "" && GDM_Data.USER_ID != "" && GDM_Data.CATEGORY != "")
                    {
                        //sql = "  select '0' USER_ID , '-SELECT-' USER_NAME  FROM DUAL " +
                        //    "union  " +
                        //    "select distinct USER_ID||'-'||EMAIL_ID USER_ID,USER_NAME " +
                        //    "from ccm.IT_HELPDESK_ASSIGN where PROB_A_TYPE=(select LOC_CODE from ccm.IT_HELPDESK_MGM where HELPDESK_ID='" + GDM_Data.HELPDESK_ID + "') AND USER_ID NOT in('" + GDM_Data.USER_ID + "') " +
                        //    "union  " +
                        //    "select distinct L1_ID||'-'||L1_EMAIL_ID USER_ID,L1_NAME " +
                        //    "from  ccm.IT_HELPDESK_REASSIGN where PROB_CATEGORY='" + GDM_Data.CATEGORY + "' AND USER_ID in('" + GDM_Data.USER_ID + "')  " +
                        //    "union  " +
                        //    "select distinct USER_ID||'-'||EMAIL_ID USER_ID,USER_NAME " +
                        //    "from  ccm.IT_HELPDESK_REASSIGN where PROB_CATEGORY='" + GDM_Data.CATEGORY + "' AND USER_ID NOT in('" + GDM_Data.USER_ID + "')  " +
                        //    "union  select distinct L1_ID ||'-'|| L1_EMAIL_ID L1_ID,L1_NAME " +
                        //    "from ccm.IT_HELPDESK_ASSIGN  where PROB_A_TYPE=(select LOC_CODE from ccm.IT_HELPDESK_MGM where HELPDESK_ID='" + GDM_Data.HELPDESK_ID + "') AND L1_ID NOT in('" + GDM_Data.USER_ID + "')  order by USER_NAME";
                        if (GDM_Data.ACTION == "R")
                        {
                            if (GDM_Data.CATEGORY == "S" || GDM_Data.CATEGORY == "NS")
                            {
                                sql = "select L1_ID as USER_ID,L1_NAME as USER_NAME from ccm.IT_HELPDESK_SOFTWARE_ASSIGN  where PROB_SUBTYPE='" + GDM_Data.SUBTYPE_CODE + "'";
                            }
                            else
                            {
                                sql = "  select '0' USER_ID , '-SELECT-' USER_NAME  FROM DUAL " +
                                        "union  " +
                                        "select distinct USER_ID,USER_NAME " +
                                        "from ccm.IT_HELPDESK_ASSIGN where PROB_A_TYPE=(select LOC_CODE from ccm.IT_HELPDESK_MGM where HELPDESK_ID='" + GDM_Data.HELPDESK_ID + "') AND USER_ID NOT in('" + GDM_Data.USER_ID + "') " +
                                        "union  " +
                                        "select distinct L1_ID USER_ID,L1_NAME " +
                                        "from  ccm.IT_HELPDESK_REASSIGN where PROB_CATEGORY='" + GDM_Data.CATEGORY + "' AND USER_ID in('" + GDM_Data.USER_ID + "')  " +
                                        "union  " +
                                        "select distinct USER_ID USER_ID,USER_NAME " +
                                        "from  ccm.IT_HELPDESK_REASSIGN where PROB_CATEGORY='" + GDM_Data.CATEGORY + "' AND USER_ID NOT in('" + GDM_Data.USER_ID + "')  " +
                                        "union  select distinct L1_ID L1_ID,L1_NAME " +
                                        "from ccm.IT_HELPDESK_ASSIGN  where PROB_A_TYPE=(select LOC_CODE from ccm.IT_HELPDESK_MGM where HELPDESK_ID='" + GDM_Data.HELPDESK_ID + "') AND L1_ID NOT in('" + GDM_Data.USER_ID + "')  order by USER_NAME";
                            }
                        }
                        else if (GDM_Data.ACTION == "C" || GDM_Data.ACTION == "PC")
                        {
                            if (GDM_Data.USER_ID == "9875")
                            {
                                sql = "select USER_NAME,USER_ID from CCM.IT_HELPDESK_CLOSEDBY WHERE USER_ID in ('HUD01','HUD02','HUD03','HUD04','HUD05','HUD06','HUD07') ORDER BY USER_NAME";
                            }
                            else
                            {
                                sql = "select USER_NAME,USER_ID from CCM.IT_HELPDESK_CLOSEDBY WHERE PROB_CATEGORY='" + GDM_Data.CATEGORY + "' ORDER BY USER_NAME";
                            }
                        }
                    }
                    else
                    {
                        sql = "SELECT '0' CODE,'-SELECT-' DESCRIPTION FROM DUAL " +
                            "UNION " +
                            "select CODE, DESCRIPTION " +
                            "from ccm.TASK_COR_SYS_TYPE " +
                            "where CORETYPE = '" + GDM_Data.CORETYPE + "'  AND COMPANY_CODE = '" + GDM_Data.COMPANY_CODE + "' order by DESCRIPTION";
                    }
                    //sql = @" update ccm.IT_HELPDESK_MGM set STATUS='" + UCS_Data.STATUS + "' , UPDATE_DATE= sysdate, REMARKS_DESC = '" + UCS_Data.REMARK + "', REASON = '" + UCS_Data.REASON + "', PROB_SUBTYPE='" + UCS_Data.SUBTYPE + "', PROB_TYPE='" + UCS_Data.TYPE + "', PROB_AREA='" + UCS_Data.PROBLEMAREA + "', SUPPORT_TYPE='" + UCS_Data.SUPPORTTYPE + "', ASSIGN_TO='" + UCS_Data.ASSIGN_TO + "' where HELPDESK_ID='" + UCS_Data.HELPDESK_ID + "'";
                    //else
                    //    sql = @" update ccm.IT_HELPDESK_MGM set STATUS='" + UCS_Data.STATUS + "' , UPDATE_DATE= sysdate, REMARKS_DESC = '" + UCS_Data.REMARK + "', REASON = '" + UCS_Data.REASON + "', PROB_SUBTYPE='" + UCS_Data.SUBTYPE + "', PROB_TYPE='" + UCS_Data.TYPE + "', PROB_AREA='" + UCS_Data.PROBLEMAREA + "', SUPPORT_TYPE='" + UCS_Data.SUPPORTTYPE + "' where HELPDESK_ID='" + UCS_Data.HELPDESK_ID + "'";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        objresult.Result = dt;
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        #endregion

        // Login NewAPI
        [Route("API/TF/GET_INSPECTION_LOGIN")]
        [HttpPost]
        public HttpResponseMessage GET_INSPECTION_LOGIN(Inspection_Login IL_Data)
        {
            List<Inspection_Data> documentlist = new List<Inspection_Data>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    //sql = "SELECT COUNT(*) as totalCount FROM MOBINT.MCR_INSPECTION_MASTER where CREATEDBY = '" + IP_Data.CREATEDBY + "'";

                    //sql = " SELECT EMP_NAME, EMP_ID, IMEI_NO, DIVISION, LOGIN_DATE, ROLE, MUD.ACTIVE_FLAG,LOGIN_TYPE,(SELECT version FROM MOBINT.MCR_APP_VERSION WHERE status='Y' AND ROWNUM=1) APP_VERSION_WEB FROM MOBINT.MCR_USER_DETAILS MUD, MOBINT.INSPECTION_LOGIN MLM WHERE MUD.EMP_ID= MLM.LOGIN_ID AND UPPER(LOGIN_ID)=UPPER('" + IL_Data.UserName + "') AND PASSWORD='" + IL_Data.Password + "' and LOGIN_TYPE = 'IM'  ";

                    sql = "SELECT LOGIN_NAME EMP_NAME, LOGIN_ID EMP_ID, IMEI_NO, DIVISION_ID DIVISION, PASS_UPDATED_DATE LOGIN_DATE, LOGIN_TYPE ROLE, ACTIVE_FLAG,LOGIN_TYPE,";
                    sql += " APP_VERSION_WEB FROM MOBINT.INSPECTION_LOGIN WHERE UPPER(LOGIN_ID)=UPPER('" + IL_Data.UserName + "') AND PASSWORD='" + IL_Data.Password + "' and LOGIN_TYPE = 'IM' ";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        objresult.Result = dt;
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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




        [Route("API/TF/GET_INSPECTION_MASTER")]
        [HttpPost]
        public HttpResponseMessage GET_INSPECTION_MASTER(Inspection_Data IP_Data)
        {
            List<Inspection_Data> documentlist = new List<Inspection_Data>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    //sql = "SELECT COUNT(*) as totalCount FROM MOBINT.MCR_INSPECTION_MASTER where CREATEDBY = '" + IP_Data.CREATEDBY + "'";

                    sql = " select 'During' as Type, count(*) as Total_Count FROM MOBINT.MCR_INSPECTION_MASTER where upper(INSPECTION_TYPE) = 'DURING INSTALLATION' and CREATEDBY = '" + IP_Data.CREATEDBY + "' ";
                    sql += " union ";
                    sql += " select 'Post' as Type, count(*) as Total_Count FROM MOBINT.MCR_INSPECTION_MASTER where upper(INSPECTION_TYPE) = 'POST INSTALLATION' and CREATEDBY = '" + IP_Data.CREATEDBY + "' ";
                    sql += " union ";
                    sql += " select 'Other' as Type, count(*) as Total_Count FROM MOBINT.MCR_INSPECTION_MASTER where upper(INSPECTION_TYPE) = 'OTHER' and CREATEDBY = '" + IP_Data.CREATEDBY + "' ";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    string Data = "";
                    int count = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Data == "")
                                Data = dt.Rows[i]["Type"].ToString() + ":" + dt.Rows[i]["Total_Count"].ToString();
                            else
                                Data = Data + ";" + dt.Rows[i]["Type"].ToString() + ":" + dt.Rows[i]["Total_Count"].ToString();
                            count += Convert.ToInt32(dt.Rows[i]["Total_Count"].ToString());
                        }
                        if (count > 0)
                            Data = Data + ";" + count.ToString();
                        objresult.Result = Data;
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        [Route("API/TF/GET_INSPECTION_MASTER_MONYR")]
        [HttpPost]
        public HttpResponseMessage GET_INSPECTION_MASTER_MONYR(Inspection_Data IP_Data)
        {
            List<Inspection_Data> documentlist = new List<Inspection_Data>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    //sql = "SELECT COUNT(*) as totalCount FROM MOBINT.MCR_INSPECTION_MASTER where CREATEDBY = '" + IP_Data.CREATEDBY + "'";

                    sql = " select 'During' as Type, count(*) as Total_Count FROM MOBINT.MCR_INSPECTION_MASTER where upper(INSPECTION_TYPE) = 'DURING INSTALLATION' and CREATEDBY = '" + IP_Data.CREATEDBY + "'  and to_char(CREATEDDATE,'MMYYYY')='" + IP_Data.MONTH_YEAR + "' ";
                    sql += " union ";
                    sql += " select 'Post' as Type, count(*) as Total_Count FROM MOBINT.MCR_INSPECTION_MASTER where upper(INSPECTION_TYPE) = 'POST INSTALLATION' and CREATEDBY = '" + IP_Data.CREATEDBY + "'  and to_char(CREATEDDATE,'MMYYYY')='" + IP_Data.MONTH_YEAR + "' ";
                    sql += " union ";
                    sql += " select 'Other' as Type, count(*) as Total_Count FROM MOBINT.MCR_INSPECTION_MASTER where upper(INSPECTION_TYPE) = 'OTHER' and CREATEDBY = '" + IP_Data.CREATEDBY + "'  and to_char(CREATEDDATE,'MMYYYY')='" + IP_Data.MONTH_YEAR + "' ";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    string Data = "";
                    int count = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Data == "")
                                Data = dt.Rows[i]["Type"].ToString() + ":" + dt.Rows[i]["Total_Count"].ToString();
                            else
                                Data = Data + ";" + dt.Rows[i]["Type"].ToString() + ":" + dt.Rows[i]["Total_Count"].ToString();
                            count += Convert.ToInt32(dt.Rows[i]["Total_Count"].ToString());
                        }
                        if (count > 0)
                            Data = Data + ";" + count.ToString();
                        objresult.Result = Data;
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        [Route("API/TF/GET_BURNTMETERIP_MASTER")]
        [HttpPost]
        public HttpResponseMessage GET_BURNTMETERIP_MASTER(Burnt_Meter_Inspection BMI_DT)
        {
            List<Burnt_Meter_Inspection> documentlist = new List<Burnt_Meter_Inspection>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    sql = "SELECT COUNT(*) as totalCount FROM MOBINT.MCR_BURNTMETERIP_MASTER where CREATEDBY = '" + BMI_DT.CREATEDBY + "'";

                    cmd = new OleDbCommand(sql, con);
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
                            objresult.Result = dt.Rows[i]["totalCount"].ToString();
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        [Route("API/TF/GET_BURNTMETERIP_MASTER_MONYR")]
        [HttpPost]
        public HttpResponseMessage GET_BURNTMETERIP_MASTER_MONYR(Burnt_Meter_Inspection BMI_DT)
        {
            List<Burnt_Meter_Inspection> documentlist = new List<Burnt_Meter_Inspection>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    sql = "SELECT COUNT(*) as totalCount FROM MOBINT.MCR_BURNTMETERIP_MASTER where CREATEDBY = '" + BMI_DT.CREATEDBY + "'   and to_char(CREATEDDATE,'MMYYYY')='" + BMI_DT.MONTH_YEAR + "' ";

                    cmd = new OleDbCommand(sql, con);
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
                            objresult.Result = dt.Rows[i]["totalCount"].ToString();
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        [Route("API/TF/GET_Compliance_MASTER")]
        [HttpPost]
        public HttpResponseMessage GET_Compliance_MASTER(Compliance_Master CM_DT)
        {
            List<Compliance_Master> Compliance_Master = new List<Compliance_Master>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    sql = "SELECT COUNT(*) as totalCount FROM MOBINT.MCR_Compliance_Master where CREATEDBY = '" + CM_DT.CREATEDBY + "'";

                    cmd = new OleDbCommand(sql, con);
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
                            objresult.Result = dt.Rows[i]["totalCount"].ToString();
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        [Route("API/TF/GET_Compliance_MASTER_MONYR")]
        [HttpPost]
        public HttpResponseMessage GET_Compliance_MASTER_MONYR(Compliance_Master CM_DT)
        {
            List<Compliance_Master> Compliance_Master = new List<Compliance_Master>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    sql = "SELECT COUNT(*) as totalCount FROM MOBINT.MCR_Compliance_Master where CREATEDBY = '" + CM_DT.CREATEDBY + "'    and to_char(CREATEDDATE,'MMYYYY')='" + CM_DT.MONTH_YEAR + "' ";

                    cmd = new OleDbCommand(sql, con);
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
                            objresult.Result = dt.Rows[i]["totalCount"].ToString();
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        [Route("API/TF/GET_DOCUMENTUPLOAD_MASTER")]
        [HttpPost]
        public HttpResponseMessage GET_DOCUMENTUPLOAD_MASTER(DocumentUpload CM_DT)
        {
            List<DocumentUpload> documentlist = new List<DocumentUpload>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    sql = "SELECT COUNT(*) as totalCount FROM MOBINT.MCR_DOCUMENTUPLOAD_MASTER where UPLOAD = '" + CM_DT.UploadBy + "'";

                    cmd = new OleDbCommand(sql, con);
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
                            objresult.Result = dt.Rows[i]["totalCount"].ToString();
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        [Route("API/TF/GET_DOCUMENTUPLOAD_MASTER_MONYR")]
        [HttpPost]
        public HttpResponseMessage GET_DOCUMENTUPLOAD_MASTER_MONYR(DocumentUpload CM_DT)
        {
            List<DocumentUpload> documentlist = new List<DocumentUpload>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    sql = "SELECT COUNT(*) as totalCount FROM MOBINT.MCR_DOCUMENTUPLOAD_MASTER where UPLOAD = '" + CM_DT.UploadBy + "'    and to_char(CREATEDDATE,'MMYYYY')='" + CM_DT.MONTH_YEAR + "' ";

                    cmd = new OleDbCommand(sql, con);
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
                            objresult.Result = dt.Rows[i]["totalCount"].ToString();
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        [Route("API/TF/GET_GENERALSITE_INSPECTION_MASTER_MONYR")]  //created by chesta on 19-09-2024
        [HttpPost]
        public HttpResponseMessage GET_GENERALSITE_INSPECTION_MASTER_MONYR(DocumentUpload CM_DT)
        {
            List<DocumentUpload> documentlist = new List<DocumentUpload>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    sql = "SELECT COUNT(*) as totalCount FROM MOBINT.MCR_GEN_SITE_INSPECTION where UPLOADBY = '" + CM_DT.UploadBy + "'    and to_char(CREATEDDATE,'MMYYYY')='" + CM_DT.MONTH_YEAR + "' ";

                    cmd = new OleDbCommand(sql, con);
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
                            objresult.Result = dt.Rows[i]["totalCount"].ToString();
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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



        #region Chesta Mam MMG Scorcared
        [Route("API/TF/GET_Meter_Activity_ScoreCard")]
        [HttpPost]
        public HttpResponseMessage GET_Meter_Activity_ScoreCard(MCR_Details mcrData)
        {
            List<MCR_Details> datalist = new List<MCR_Details>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    string Prefix = "0000000000" + mcrData.ORDERID;
                    sql = " SELECT AUART,I.ERDAT, D.ACTIVITY_DATE, D.TAB_LN_ID_NAME, I.DIVISION,I.ACCOUNT_CLASS,CA_NO,I.ORDERID,SANCTIONED_LOAD,NAME,TEL_NO,ADDRESS,METER_NO," +
                        "TERMINAL_SEAL,OTHER_SEAL TrSeal2,METERBOXSEAL1,METERBOXSEAL2,BUSBARSEAL1,BUSBARSEAL2," +
                        " DRUM_NUMBER_BB DURM_NO_PoleMtr,DURM_NO DURM_NO_BB_MTR,'' DURM_NO_OP,  CABLESIZE2 CableSize_PoleMtr," +
                        " B_BAR_CABLE_SIZE     CableSize_BB_MTR,OUTPUTCABLESIZE CableSize_OP,RUNNING_LENGTH_FROM_BB SrNoFrom_PoleMtr,RUNNINGLENGTHFROM SrNoFrom_BB_MTR, " +
                        " '' SrNoFrom_OP, RUNNINGLENGTHTO  SrNoTo_PoleMtr, RUNNING_LENGTH_TO_BB SrNoTo_BB_MTR,'' SrNoTo_OP,OUTPUTBUSLENGTH Length_PoleMtr, " +
                        " CABLELENGTH Length_BB_MTR,OUTPUTCABLELENGTH Length_OP,BB_CABLE_USED CableType,VALINSTALL_TYPE_BB Laying,BUS_BAR_NO,BUSBARSIZE, " +
                        " '' BBConfig,'' BusbarType,ELCB_INSTALLED,'' MCB,TAB_LN_ID_NAME Punch_By,TAB_LOGIN_ID ID," +
                        "DURM_NO,RUNNINGLENGTHTO,RUNNINGLENGTHFROM,CABLELENGTH, CABLESIZE2 ,CABLE_LEN_USED,CABLEINSTALLTYPE,OUTPUTBUSLENGTH,B_BAR_CABLE_SIZE," +
                        "BB_CABLE_USED,OUTPUTCABLELENGTH,OUTPUT_CABLE_LEN_USED,INSTALLEDBUSBAR,OTHER_SEAL,REM_CABLE_LEN,REM_CABLE_SIZE,ANGLE_IRON_POLE_END_QTY," +
                        "ANGLE_IRON_CONSUMER_END_QTY,OLD_DEVICE_NO,VALINSTALL_TYPE_BB," +
                        "D.BUS_BAR_DRUM_NO,D.RUNNING_LENGTH_FROM_BB,D.RUNNING_LENGTH_TO_BB,D.OUTPUTCABLESIZE,D.ACCES_THIMBLE_QTY,D.ACCES_SADDLE_CLAMP_QTY," +
                        "D.ANCHOR_POLE_END_QTY,D.ANCHOR_CONSUMER_END_QTY,D.FASTNER,D.PIPE_POLE_END_QTY,D.GUNNYBAG_OLD,D.GUNNYBAGSEAL_OLD,D.LAB_TSTNG_NTC " +
                        "FROM mobint.MCR_Details D, mobint.MCR_INPUT_Details I " +
                        " where I.orderid = D.orderid and (I.Orderid = '" + mcrData.ORDERID + "' or METER_NO = '" + mcrData.ORDERID + "' or METER_NO = '" + Prefix + "')";


                    cmd = new OleDbCommand(sql, con);
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
                            MCR_Details objMCRData = new MCR_Details();
                            objMCRData.AUART = dt.Rows[i]["AUART"].ToString();
                            objMCRData.ERDAT = dt.Rows[i]["ERDAT"].ToString();
                            objMCRData.ACTIVITY_DATE = dt.Rows[i]["ACTIVITY_DATE"].ToString();
                            objMCRData.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                            objMCRData.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                            objMCRData.CA_NO = dt.Rows[i]["CA_NO"].ToString();
                            objMCRData.ORDERID = dt.Rows[i]["ORDERID"].ToString();
                            objMCRData.SANCTIONED_LOAD = dt.Rows[i]["SANCTIONED_LOAD"].ToString();
                            objMCRData.NAME = dt.Rows[i]["NAME"].ToString();
                            objMCRData.TEL_NO = dt.Rows[i]["TEL_NO"].ToString();
                            objMCRData.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                            objMCRData.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                            objMCRData.TERMINAL_SEAL = dt.Rows[i]["TERMINAL_SEAL"].ToString();
                            objMCRData.TrSeal2 = dt.Rows[i]["TrSeal2"].ToString();
                            objMCRData.METERBOXSEAL1 = dt.Rows[i]["METERBOXSEAL1"].ToString();
                            objMCRData.METERBOXSEAL2 = dt.Rows[i]["METERBOXSEAL2"].ToString();
                            objMCRData.BUSBARSEAL1 = dt.Rows[i]["BUSBARSEAL1"].ToString();
                            objMCRData.BUSBARSEAL2 = dt.Rows[i]["BUSBARSEAL2"].ToString();
                            objMCRData.DURM_NO_PoleMtr = dt.Rows[i]["DURM_NO_PoleMtr"].ToString();
                            objMCRData.DURM_NO_BB_MTR = dt.Rows[i]["DURM_NO_BB_MTR"].ToString();
                            objMCRData.DURM_NO_OP = dt.Rows[i]["DURM_NO_OP"].ToString();
                            objMCRData.CableSize_PoleMtr = dt.Rows[i]["CableSize_PoleMtr"].ToString();
                            objMCRData.CableSize_BB_MTR = dt.Rows[i]["CableSize_BB_MTR"].ToString();
                            objMCRData.CableSize_OP = dt.Rows[i]["CableSize_OP"].ToString();
                            objMCRData.SrNoFrom_PoleMtr = dt.Rows[i]["SrNoFrom_PoleMtr"].ToString();
                            objMCRData.SrNoFrom_BB_MTR = dt.Rows[i]["SrNoFrom_BB_MTR"].ToString();
                            objMCRData.SrNoFrom_OP = dt.Rows[i]["SrNoFrom_OP"].ToString();
                            objMCRData.SrNoTo_PoleMtr = dt.Rows[i]["SrNoTo_PoleMtr"].ToString();
                            objMCRData.SrNoTo_BB_MTR = dt.Rows[i]["SrNoTo_BB_MTR"].ToString();
                            objMCRData.SrNoTo_OP = dt.Rows[i]["SrNoTo_OP"].ToString();
                            objMCRData.Length_PoleMtr = dt.Rows[i]["Length_PoleMtr"].ToString();
                            objMCRData.Length_BB_MTR = dt.Rows[i]["Length_BB_MTR"].ToString();
                            objMCRData.Length_OP = dt.Rows[i]["Length_OP"].ToString();
                            objMCRData.CableType = dt.Rows[i]["CableType"].ToString();
                            objMCRData.Laying = dt.Rows[i]["Laying"].ToString();
                            objMCRData.BUS_BAR_NO = dt.Rows[i]["BUS_BAR_NO"].ToString();
                            objMCRData.BUSBARSIZE = dt.Rows[i]["BUSBARSIZE"].ToString();
                            objMCRData.BBConfig = dt.Rows[i]["BBConfig"].ToString();
                            objMCRData.BusbarType = dt.Rows[i]["BusbarType"].ToString();
                            objMCRData.ELCB_INSTALLED = dt.Rows[i]["ELCB_INSTALLED"].ToString();
                            objMCRData.MCB = dt.Rows[i]["MCB"].ToString();
                            objMCRData.Punch_By = dt.Rows[i]["TAB_LN_ID_NAME"].ToString();
                            objMCRData.ID = dt.Rows[i]["ID"].ToString();
                            objMCRData.DURM_NO = dt.Rows[i]["DURM_NO"].ToString();
                            objMCRData.RUNNINGLENGTHTO = dt.Rows[i]["RUNNINGLENGTHTO"].ToString();
                            objMCRData.RUNNINGLENGTHFROM = dt.Rows[i]["RUNNINGLENGTHFROM"].ToString();
                            objMCRData.CABLELENGTH = dt.Rows[i]["CABLELENGTH"].ToString();
                            objMCRData.CABLESIZE2 = dt.Rows[i]["CABLESIZE2"].ToString();
                            objMCRData.CABLE_LEN_USED = dt.Rows[i]["CABLE_LEN_USED"].ToString();
                            objMCRData.CABLEINSTALLTYPE = dt.Rows[i]["CABLEINSTALLTYPE"].ToString();
                            objMCRData.OUTPUTBUSLENGTH = dt.Rows[i]["OUTPUTBUSLENGTH"].ToString();
                            objMCRData.B_BAR_CABLE_SIZE = dt.Rows[i]["B_BAR_CABLE_SIZE"].ToString();
                            objMCRData.BB_CABLE_USED = dt.Rows[i]["BB_CABLE_USED"].ToString();
                            objMCRData.OUTPUTCABLELENGTH = dt.Rows[i]["OUTPUTCABLELENGTH"].ToString();
                            objMCRData.OUTPUT_CABLE_LEN_USED = dt.Rows[i]["OUTPUT_CABLE_LEN_USED"].ToString();
                            objMCRData.INSTALLEDBUSBAR = dt.Rows[i]["INSTALLEDBUSBAR"].ToString();
                            objMCRData.OTHER_SEAL = dt.Rows[i]["OTHER_SEAL"].ToString();
                            objMCRData.REM_CABLE_LEN = dt.Rows[i]["REM_CABLE_LEN"].ToString();
                            objMCRData.REM_CABLE_SIZE = dt.Rows[i]["REM_CABLE_SIZE"].ToString();
                            objMCRData.ANGLE_IRON_POLE_END_QTY = dt.Rows[i]["ANGLE_IRON_POLE_END_QTY"].ToString();
                            objMCRData.ANGLE_IRON_CONSUMER_END_QTY = dt.Rows[i]["ANGLE_IRON_CONSUMER_END_QTY"].ToString();
                            objMCRData.OLD_DEVICE_NO = dt.Rows[i]["OLD_DEVICE_NO"].ToString();
                            objMCRData.VALINSTALL_TYPE_BB = dt.Rows[i]["VALINSTALL_TYPE_BB"].ToString();
                            objMCRData.BUS_BAR_DRUM_NO = dt.Rows[i]["BUS_BAR_DRUM_NO"].ToString();
                            objMCRData.RUNNING_LENGTH_FROM_BB = dt.Rows[i]["RUNNING_LENGTH_FROM_BB"].ToString();
                            objMCRData.RUNNING_LENGTH_TO_BB = dt.Rows[i]["RUNNING_LENGTH_TO_BB"].ToString();
                            objMCRData.OUTPUTCABLESIZE = dt.Rows[i]["OUTPUTCABLESIZE"].ToString();
                            objMCRData.ACCES_THIMBLE_QTY = dt.Rows[i]["ACCES_THIMBLE_QTY"].ToString();
                            objMCRData.ACCES_SADDLE_CLAMP_QTY = dt.Rows[i]["ACCES_SADDLE_CLAMP_QTY"].ToString();
                            objMCRData.ANCHOR_POLE_END_QTY = dt.Rows[i]["ANCHOR_POLE_END_QTY"].ToString();
                            objMCRData.ANCHOR_CONSUMER_END_QTY = dt.Rows[i]["ANCHOR_CONSUMER_END_QTY"].ToString();
                            objMCRData.FASTNER = dt.Rows[i]["FASTNER"].ToString();
                            objMCRData.PIPE_POLE_END_QTY = dt.Rows[i]["PIPE_POLE_END_QTY"].ToString();
                            objMCRData.GUNNYBAG_OLD = dt.Rows[i]["GUNNYBAG_OLD"].ToString();
                            objMCRData.GUNNYBAGSEAL_OLD = dt.Rows[i]["GUNNYBAGSEAL_OLD"].ToString();
                            objMCRData.LAB_TSTNG_NTC = dt.Rows[i]["LAB_TSTNG_NTC"].ToString();

                            datalist.Add(objMCRData);
                            objresult.Result = datalist;
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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
        #endregion

        #region SAFETY_MOBILE_API_05042024

        [Route("API/TF/GET_Safety_Meter_Activity")]
        [HttpPost]
        public HttpResponseMessage GET_Safety_Meter_Activity(Safety_Details mcrData)
        {
            List<Safety_Details> datalist = new List<Safety_Details>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    string Prefix = "0000000000" + mcrData.METER_NO;
                    sql = " SELECT AUART,ERDAT, DIVISION,ACCOUNT_CLASS,CA_NO,NAME,TEL_NO,ADDRESS,METER_NO,FLAG " +
                        " FROM mobint.SAFETY_INPUT_DETAILS " +
                        " WHERE (METER_NO = '" + mcrData.METER_NO + "' or METER_NO = '" + Prefix + "')";


                    cmd = new OleDbCommand(sql, con);
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
                            Safety_Details objData = new Safety_Details();
                            objData.AUART = dt.Rows[i]["AUART"].ToString();
                            objData.ERDAT = dt.Rows[i]["ERDAT"].ToString();
                            objData.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                            objData.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                            objData.CA_NO = dt.Rows[i]["CA_NO"].ToString();
                            objData.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                            objData.NAME = dt.Rows[i]["NAME"].ToString();
                            objData.TEL_NO = dt.Rows[i]["TEL_NO"].ToString();
                            objData.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                            objData.FLAG = dt.Rows[i]["FLAG"].ToString();

                            datalist.Add(objData);
                            objresult.Result = datalist;
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        sql = " SELECT DIVISION,ACCOUNT_CLASS,CA_NO,NAME,TEL_NO,ADDRESS,METER_NO ,STATUS_FLAG from MOBINT.EXECUTIVE_PUNCH_DETAILS " +
                            " WHERE (METER_NO = '" + mcrData.METER_NO + "' or METER_NO = '" + Prefix + "')";


                        cmd = new OleDbCommand(sql, con);
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
                                Safety_Details objData = new Safety_Details();
                                objData.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                objData.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                objData.CA_NO = dt.Rows[i]["CA_NO"].ToString();
                                objData.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                objData.NAME = dt.Rows[i]["NAME"].ToString();
                                objData.TEL_NO = dt.Rows[i]["TEL_NO"].ToString();
                                objData.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                                objData.FLAG = dt.Rows[i]["STATUS_FLAG"].ToString();

                                datalist.Add(objData);
                                objresult.Result = datalist;
                                objresult.Key = "Success";
                                objresult.Status = "200";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            }
                        }
                        else
                        {
                            sql = " SELECT DIVISION,ACCOUNT_CLASS,CA_NO,NAME,TEL_NO,ADDRESS,METER_NO ,STATUS_FLAG from MOBINT.SAFETY_PUNCH_DETAILS " +
                        " WHERE (METER_NO = '" + mcrData.METER_NO + "' or METER_NO = '" + Prefix + "') and STATUS_FLAG = 'S' ";

                            cmd = new OleDbCommand(sql, con);
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
                                    Safety_Details objData = new Safety_Details();
                                    objData.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                    objData.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                    objData.CA_NO = dt.Rows[i]["CA_NO"].ToString();
                                    objData.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                    objData.NAME = dt.Rows[i]["NAME"].ToString();
                                    objData.TEL_NO = dt.Rows[i]["TEL_NO"].ToString();
                                    objData.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                                    objData.FLAG = dt.Rows[i]["STATUS_FLAG"].ToString();

                                    datalist.Add(objData);
                                    objresult.Result = datalist;
                                    objresult.Key = "Success";
                                    objresult.Status = "200";
                                    Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                                }
                            }
                            else
                            {
                                objresult.Key = "Record not found try again";
                                objresult.Status = "421";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                                return Msg;
                            }
                        }
                    }
                    else
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        [Route("API/TF/GET_Execution_Data")]
        [HttpPost]
        public HttpResponseMessage GET_Execution_Data(Safety_Details mcrData)
        {
            List<Safety_Details> datalist = new List<Safety_Details>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    string Prefix = "0000000000" + mcrData.METER_NO;

                    sql = " SELECT DIVISION,ACCOUNT_CLASS,CA_NO,NAME,TEL_NO,ADDRESS,METER_NO ,STATUS_FLAG from MOBINT.EXECUTIVE_PUNCH_DETAILS " +
                        " WHERE (METER_NO = '" + mcrData.METER_NO + "' or METER_NO = '" + Prefix + "') and STATUS_FLAG = 'E' ";

                    cmd = new OleDbCommand(sql, con);
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
                            Safety_Details objData = new Safety_Details();
                            objData.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                            objData.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                            objData.CA_NO = dt.Rows[i]["CA_NO"].ToString();
                            objData.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                            objData.NAME = dt.Rows[i]["NAME"].ToString();
                            objData.TEL_NO = dt.Rows[i]["TEL_NO"].ToString();
                            objData.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                            objData.FLAG = dt.Rows[i]["STATUS_FLAG"].ToString();

                            datalist.Add(objData);
                            objresult.Result = datalist;
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        }
                    }
                    else
                    {
                        sql = " SELECT DIVISION,ACCOUNT_CLASS,CA_NO,NAME,TEL_NO,ADDRESS,METER_NO ,STATUS_FLAG from MOBINT.SAFETY_PUNCH_DETAILS " +
                        " WHERE (METER_NO = '" + mcrData.METER_NO + "' or METER_NO = '" + Prefix + "') and (STATUS_FLAG = 'S' OR STATUS_FLAG ='E')";

                        cmd = new OleDbCommand(sql, con);
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
                                Safety_Details objData = new Safety_Details();
                                objData.DIVISION = dt.Rows[i]["DIVISION"].ToString();
                                objData.ACCOUNT_CLASS = dt.Rows[i]["ACCOUNT_CLASS"].ToString();
                                objData.CA_NO = dt.Rows[i]["CA_NO"].ToString();
                                objData.METER_NO = dt.Rows[i]["METER_NO"].ToString();
                                objData.NAME = dt.Rows[i]["NAME"].ToString();
                                objData.TEL_NO = dt.Rows[i]["TEL_NO"].ToString();
                                objData.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                                objData.FLAG = dt.Rows[i]["STATUS_FLAG"].ToString();

                                datalist.Add(objData);
                                objresult.Result = datalist;
                                objresult.Key = "Success";
                                objresult.Status = "200";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            }
                        }
                        else
                        {
                            objresult.Key = "Record not found try again";
                            objresult.Status = "421";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        [Route("API/TF/GET_CaseList_Details")]
        [HttpPost]
        public HttpResponseMessage GET_CaseList_Details(Case_List CLData)
        {
            List<Case_List> datalist = new List<Case_List>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    sql = " SELECT DIVISION,ACCOUNT_CLASS,CA_NO,NAME,TEL_NO,ADDRESS,METER_NO ,STATUS_FLAG from mobint.EXECUTIVE_PUNCH_DETAILS where STATUS_FLAG = 'E' and EXEC_METER_NOT_SHIFTED_REASON = 'SAFETY'";
                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        int totalRows = dt.Rows.Count;
                        int rowsToDisplay = (int)Math.Ceiling(0.1 * totalRows);
                        var selectedRows = dt.AsEnumerable().Take(rowsToDisplay);
                        foreach (var row in selectedRows)
                        {
                            Case_List objCLData = new Case_List();
                            objCLData.DIVISION = row["DIVISION"].ToString();
                            objCLData.ACCOUNT_CLASS = row["ACCOUNT_CLASS"].ToString();
                            objCLData.CA_NO = row["CA_NO"].ToString();
                            objCLData.METER_NO = row["METER_NO"].ToString();
                            objCLData.NAME = row["NAME"].ToString();
                            objCLData.TEL_NO = row["TEL_NO"].ToString();
                            objCLData.ADDRESS = row["ADDRESS"].ToString();
                            objCLData.FLAG = row["STATUS_FLAG"].ToString();
                            datalist.Add(objCLData);
                        }
                        objresult.Result = datalist;
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }

                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        [Route("API/TF/Insert_Safety_Execution_DATA")]
        [HttpPost]
        public HttpResponseMessage Insert_Safety_Execution_Data([FromBody] Safety_Execution_Data IP_Data)
        {
            try
            {
                string _sFLAG = "N";
                string ImageToBase = IP_Data.EXEC_IMAGE_UPLOAD;

                string Image_SAFE_PLACE_METER = IP_Data.SAFE_PLACE_METER_IMAGE;
                string Image_FABRICATION = IP_Data.FABRICATION_IMAGE;
                string Image_REALLOCATION_SAFE_PLACE = IP_Data.REALLOCATION_SAFE_PLACE_IMAGE;

                Byte[] _byImgBuild = null;

                Byte[] _byImgSAFE_PLACE = null;
                Byte[] _byImgFABRICATION = null;
                Byte[] _byImgREALLOCATION = null;

                string _sCancelImgPath = string.Empty;

                string _sSAFE_PLACE_METERImgPath = string.Empty;
                string _sFABRICATIONImgPath = string.Empty;
                string _sREALLOCATION_SAFE_PLACEImgPath = string.Empty;

                if (ImageToBase != null && ImageToBase != "" && ImageToBase != string.Empty)
                {
                    _byImgBuild = Convert.FromBase64String(IP_Data.EXEC_IMAGE_UPLOAD);
                    _sCancelImgPath = byteArrayToFile(_byImgBuild, IP_Data.METER_NO + "_EXEC_IMGPATH");
                }

                if (Image_SAFE_PLACE_METER != null && Image_SAFE_PLACE_METER != "" && Image_SAFE_PLACE_METER != string.Empty)
                {
                    _byImgSAFE_PLACE = Convert.FromBase64String(IP_Data.SAFE_PLACE_METER_IMAGE);
                    _sSAFE_PLACE_METERImgPath = byteArrayToFile(_byImgSAFE_PLACE, IP_Data.METER_NO + "_SAFE_PLACE_IMGPATH");
                }

                if (Image_FABRICATION != null && Image_FABRICATION != "" && Image_FABRICATION != string.Empty)
                {
                    _byImgFABRICATION = Convert.FromBase64String(IP_Data.FABRICATION_IMAGE);
                    _sFABRICATIONImgPath = byteArrayToFile(_byImgFABRICATION, IP_Data.METER_NO + "_FABRICATION_IMGPATH");
                }

                if (Image_REALLOCATION_SAFE_PLACE != null && Image_REALLOCATION_SAFE_PLACE != "" && Image_REALLOCATION_SAFE_PLACE != string.Empty)
                {
                    _byImgREALLOCATION = Convert.FromBase64String(IP_Data.REALLOCATION_SAFE_PLACE_IMAGE);
                    _sREALLOCATION_SAFE_PLACEImgPath = byteArrayToFile(_byImgREALLOCATION, IP_Data.METER_NO + "_REALLOCATION_SAFE_IMGPATH");
                }

                if (EXECUTIVE_PUNCH_DETAILS(IP_Data.COMPANY_CODE, IP_Data.DIVISION, IP_Data.METER_NO, IP_Data.ORDERID, IP_Data.CA_NO, IP_Data.NAME, IP_Data.ADDRESS, IP_Data.ACCOUNT_CLASS, IP_Data.EXEC_SAFE_LOC, IP_Data.EXEC_SAFE_REMARK,
                                     IP_Data.EXEC_METER_SHIFTED, _sCancelImgPath, IP_Data.EXEC_METER_SHIFT_REMARK, IP_Data.EXEC_METER_NOT_SHIFTED_REASON, IP_Data.EXEC_METER_NOT_SHIFTED_REMARK, IP_Data.EXEC_PUNCH_ID, IP_Data.EXEC_PUNCH_NAME, IP_Data.PUNCH_MODE, IP_Data.PUNCH_FLAG, IP_Data.GIS_LAT, IP_Data.GIS_LONG, IP_Data.GIS_STATUS, IP_Data.STATUS_FLAG,
                                     _sSAFE_PLACE_METERImgPath, IP_Data.SAFE_PLACE_METER_REMARK, IP_Data.METER_SHIFTING_POSSIBLE, IP_Data.METER_SHIFTED_PHYSICALLY, IP_Data.CUSTOMER_AGREE, IP_Data.FABRICATION_STATUS, _sFABRICATIONImgPath, IP_Data.FABRICATION_REMARK, _sREALLOCATION_SAFE_PLACEImgPath, IP_Data.REALLOCATION_SAFE_PLACE_REMARK) == true)
                {
                    if (IP_Data.ID != null)
                    {
                        if (_sFLAG == "Y")
                        {
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }
                        else
                        {
                            objresult.Key = "Failed";
                            objresult.Status = "421";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }
                    }
                    else
                    {
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        [Route("API/TF/Insert_Safety_Team")]
        [HttpPost]
        public HttpResponseMessage Insert_Safety_Team([FromBody] Safety_Data SFD_Data)
        {
            try
            {
                string _sFLAG = "N";
                string ImageToBase = SFD_Data.SAFETY_IMAGE_UPLOAD;

                //string METER_NF_STEAM_ImageToBase = SFD_Data.METER_NF_STEAM_IMAGE;

                Byte[] _byImgBuild = null;

                //Byte[] _byMETER_NF_STEAMBuild = null;

                string _sCancelImgPath = string.Empty;

                //string _sMETER_NF_STEAMImgPath = string.Empty;

                if (ImageToBase != null && ImageToBase != "" && ImageToBase != string.Empty)
                {
                    _byImgBuild = Convert.FromBase64String(SFD_Data.SAFETY_IMAGE_UPLOAD);
                    _sCancelImgPath = byteArrayToFile(_byImgBuild, SFD_Data.METER_NO + "_SAFETY_IMGPATH");
                }

                //if (METER_NF_STEAM_ImageToBase != null)
                //{
                //    _byMETER_NF_STEAMBuild = Convert.FromBase64String(SFD_Data.METER_NF_STEAM_IMAGE);
                //    _sMETER_NF_STEAMImgPath = byteArrayToFile(_byMETER_NF_STEAMBuild, SFD_Data.METER_NO + "_METER_NF_STEAM_IMGPATH");
                //}

                if (Saved_Safety_Data(SFD_Data.COMPANY_CODE, SFD_Data.DIVISION, SFD_Data.METER_NO, SFD_Data.ORDERID, SFD_Data.CA_NO, SFD_Data.NAME, SFD_Data.ADDRESS, SFD_Data.TEL_NO, SFD_Data.ACCOUNT_CLASS, SFD_Data.SAFETY_SAFE_LOC, SFD_Data.SAFETY_SAFE_REMARK,
                                         _sCancelImgPath, SFD_Data.SAFETY_PUNCH_ID, SFD_Data.SAFETY_PUNCH_NAME, SFD_Data.PUNCH_MODE, SFD_Data.PUNCH_FLAG, SFD_Data.GIS_LAT, SFD_Data.GIS_LONG, SFD_Data.GIS_STATUS, SFD_Data.STATUS_FLAG) == true)
                {
                    if (SFD_Data.ID != null)
                    {
                        if (_sFLAG == "Y")
                        {
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }
                        else
                        {
                            objresult.Key = "Failed";
                            objresult.Status = "421";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }
                    }
                    else
                    {
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        [Route("API/TF/RevertBySafetyTeam")]
        [HttpPost]
        public HttpResponseMessage RevertBySafetyTeam([FromBody] Safety_Data IP_Data)
        {
            try
            {
                string _sFLAG = "N";

                string METER_NF_STEAM_ImageToBase = IP_Data.METER_NF_STEAM_IMAGE;
                Byte[] _byMETER_NF_STEAMBuild = null;
                string _sMETER_NF_STEAMImgPath = string.Empty;

                if (METER_NF_STEAM_ImageToBase != null)
                {
                    _byMETER_NF_STEAMBuild = Convert.FromBase64String(IP_Data.METER_NF_STEAM_IMAGE);
                    _sMETER_NF_STEAMImgPath = byteArrayToFile(_byMETER_NF_STEAMBuild, IP_Data.METER_NO + "_METER_NF_STEAM_IMGPATH");
                }

                if (Update_Safety_Data(IP_Data.COMPANY_CODE, IP_Data.DIVISION, IP_Data.METER_NO, IP_Data.ORDERID, IP_Data.CA_NO, IP_Data.NAME, IP_Data.ADDRESS, IP_Data.TEL_NO, IP_Data.ACCOUNT_CLASS, IP_Data.SAFETY_SAFE_LOC,
                                          IP_Data.SAFETY_PUNCH_ID, IP_Data.SAFETY_PUNCH_NAME, IP_Data.PUNCH_MODE, IP_Data.PUNCH_FLAG, IP_Data.GIS_LAT, IP_Data.GIS_LONG, IP_Data.GIS_STATUS, IP_Data.STATUS_FLAG, IP_Data.EXEC_METER_NOT_SHIFTED_REASON, _sMETER_NF_STEAMImgPath, IP_Data.METER_NF_STEAM_REMARK) == true)
                {
                    if (IP_Data.ID != null)
                    {
                        if (_sFLAG == "Y")
                        {
                            objresult.Key = "Success";
                            objresult.Status = "200";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }
                        else
                        {
                            objresult.Key = "Failed";
                            objresult.Status = "421";
                            Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            return Msg;
                        }
                    }
                    else
                    {
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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


        [Route("API/TF/Reset_Password")]
        [HttpPost]
        public HttpResponseMessage Reset_Password(UserReset user)
        {
            List<UserReset> documentlist = new List<UserReset>();
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    sql = "SELECT LOGIN_NAME EMP_NAME, LOGIN_ID EMP_ID, IMEI_NO, DIVISION_ID DIVISION, PASS_UPDATED_DATE LOGIN_DATE, LOGIN_TYPE ROLE, ACTIVE_FLAG,LOGIN_TYPE,";
                    sql += " APP_VERSION_WEB FROM MOBINT.INSPECTION_LOGIN WHERE UPPER(LOGIN_ID)=UPPER('" + user.UserName + "') AND PASSWORD='" + user.OldPass + "' ";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        OleDbConnection con22;
                        OleDbCommand cmd22;
                        using (con22 = new OleDbConnection(strConnection))
                        {
                            //return true;
                            string sql22 = "UPDATE MOBINT.INSPECTION_LOGIN SET PASSWORD = '" + user.Password + "', OLD_PASSWORD = '" + user.OldPass + "' ";
                            sql22 = sql22 + " WHERE LOGIN_ID IN ('" + user.UserName + "')";
                            cmd22 = new OleDbCommand(sql22, con22);
                            if (con22.State == ConnectionState.Closed)
                            {
                                con22.Open();
                            }
                            int z = cmd22.ExecuteNonQuery();
                            if (z != 0)
                            {
                                objresult.Result = dt;
                                objresult.Key = "Success";
                                objresult.Status = "200";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                            }
                            else
                            {
                                objresult.Key = "Record not found try again";
                                objresult.Status = "421";
                                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                                return Msg;
                            }
                        }

                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        public string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

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

        private bool Update_Safety_Data(string _sCOMPANY_CODE, string _sDIVISION, string _sMETER_NO, string _sORDERID, string _sCA_NO, string _sNAME, string _sADDRESS, string _sTEL_NO, string _sACCOUNT_CLASS, string _sSAFETY_SAFE_LOC,
                                      string _sSAFETY_PUNCH_ID, string _sSAFETY_PUNCH_NAME, string _sPUNCH_MODE, string _sPUNCH_FLAG, string _sGIS_LAT, string _sGIS_LONG, string _sGIS_STATUS, string _sSTATUS_FLAG, string _sEXEC_METER_NOT_SHIFTED_REASON, string _sMETER_NF_STEAMImgPath, string _sMETER_NF_STEAM_REMARK)
        {
            Random generator = new Random();
            int _autoID = generator.Next(1, 10000);

            string _sIPAddress = GetIPAddress();

            using (con = new OleDbConnection(strConnection))
            {
                sql = "INSERT INTO mobint.SAFETY_PUNCH_DETAILS(UNIQUE_REF_NO,COMPANY_CODE, DIVISION, METER_NO, ORDERID, CA_NO, NAME, ADDRESS, TEL_NO, ACCOUNT_CLASS, SAFETY_SAFE_LOC, ";
                sql += "SAFETY_ACTIVITY_DATE,SAFETY_PUNCH_ID, SAFETY_PUNCH_NAME, PUNCH_MODE, PUNCH_FLAG, GIS_LAT, GIS_LONG, GIS_STATUS,STATUS_FLAG, MACHINE_ADD, METER_NF_STEAM_IMAGE, METER_NF_STEAM_REMARK)";
                sql = sql + " VALUES";
                sql = sql + "('" + _autoID + "','" + _sCOMPANY_CODE + "','" + _sDIVISION + "','" + _sMETER_NO + "','" + _sORDERID + "','" + _sCA_NO + "','" + _sNAME + "','" + _sADDRESS + "','" + _sTEL_NO + "','" + _sACCOUNT_CLASS + "','" + _sSAFETY_SAFE_LOC + "', ";
                sql = sql + "SYSDATE,'" + _sSAFETY_PUNCH_ID + "','" + _sSAFETY_PUNCH_NAME + "','" + _sPUNCH_MODE + "','" + _sPUNCH_FLAG + "','" + _sGIS_LAT + "','" + _sGIS_LONG + "','" + _sGIS_STATUS + "','" + _sSTATUS_FLAG + "','" + _sIPAddress + "', '" + _sMETER_NF_STEAMImgPath + "', '" + _sMETER_NF_STEAM_REMARK + "')";

                cmd = new OleDbCommand(sql, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int i = cmd.ExecuteNonQuery();
                if (i != 0)
                {
                    OleDbConnection con1;
                    OleDbCommand cmd1;
                    OleDbDataAdapter adp1;
                    DataTable dt1 = new DataTable();
                    using (con1 = new OleDbConnection(strConnection))
                    {
                        string sql1 = " SELECT AUART,ERDAT, DIVISION,ACCOUNT_CLASS,CA_NO,NAME,TEL_NO,ADDRESS,METER_NO,FLAG " +
                         " FROM mobint.SAFETY_INPUT_DETAILS " +
                         " WHERE (METER_NO = '" + _sMETER_NO + "' and FLAG = 'E')";
                        cmd1 = new OleDbCommand(sql1, con1);
                        if (con1.State == ConnectionState.Closed)
                        {
                            con1.Open();
                        }
                        adp1 = new OleDbDataAdapter(cmd1);
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            OleDbConnection con2;
                            OleDbCommand cmd2;
                            using (con2 = new OleDbConnection(strConnection))
                            {
                                //return true;
                                string sql2 = "UPDATE mobint.SAFETY_INPUT_DETAILS SET FLAG = '" + _sSTATUS_FLAG + "', WEB_FLAG = '" + _sSTATUS_FLAG + "'";
                                sql2 = sql2 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                cmd2 = new OleDbCommand(sql2, con2);
                                if (con2.State == ConnectionState.Closed)
                                {
                                    con2.Open();
                                }
                                int j = cmd2.ExecuteNonQuery();
                                if (j != 0)
                                {
                                    OleDbConnection con22;
                                    OleDbCommand cmd22;
                                    using (con22 = new OleDbConnection(strConnection))
                                    {
                                        //return true;
                                        string sql22 = "UPDATE mobint.EXECUTIVE_PUNCH_DETAILS SET STATUS_FLAG = '" + _sSTATUS_FLAG + "', EXEC_METER_NOT_SHIFTED_REASON = '" + _sEXEC_METER_NOT_SHIFTED_REASON + "'";
                                        sql22 = sql22 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                        cmd22 = new OleDbCommand(sql22, con22);
                                        if (con22.State == ConnectionState.Closed)
                                        {
                                            con22.Open();
                                        }
                                        int z = cmd22.ExecuteNonQuery();
                                        if (z != 0)
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            OleDbConnection con2;
                            OleDbCommand cmd2;
                            using (con2 = new OleDbConnection(strConnection))
                            {
                                //return true;
                                string sql2 = "UPDATE mobint.EXECUTIVE_PUNCH_DETAILS SET STATUS_FLAG = '" + _sSTATUS_FLAG + "', EXEC_METER_NOT_SHIFTED_REASON = '" + _sEXEC_METER_NOT_SHIFTED_REASON + "'";
                                sql2 = sql2 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                cmd2 = new OleDbCommand(sql2, con2);
                                if (con2.State == ConnectionState.Closed)
                                {
                                    con2.Open();
                                }
                                int j = cmd2.ExecuteNonQuery();
                                if (j != 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
        }


        private bool Saved_Safety_Data(string _sCOMPANY_CODE, string _sDIVISION, string _sMETER_NO, string _sORDERID, string _sCA_NO, string _sNAME, string _sADDRESS, string _sTEL_NO, string _sACCOUNT_CLASS, string _sSAFETY_SAFE_LOC, string _sSAFETY_SAFE_REMARK,
                                     string _sSAFETY_IMAGE_UPLOAD, string _sSAFETY_PUNCH_ID, string _sSAFETY_PUNCH_NAME, string _sPUNCH_MODE, string _sPUNCH_FLAG, string _sGIS_LAT, string _sGIS_LONG, string _sGIS_STATUS, string _sSTATUS_FLAG)
        {
            Random generator = new Random();
            int _autoID = generator.Next(1, 10000);

            string _sIPAddress = GetIPAddress();

            using (con = new OleDbConnection(strConnection))
            {
                sql = "SELECT METER_NO FROM mobint.SAFETY_PUNCH_DETAILS where METER_NO='" + _sMETER_NO + "'";
                cmd = new OleDbCommand(sql, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                adp = new OleDbDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    sql = "UPDATE mobint.SAFETY_PUNCH_DETAILS SET SAFETY_SAFE_LOC = '" + _sSAFETY_SAFE_LOC + "', SAFETY_SAFE_REMARK='" + _sSAFETY_SAFE_REMARK + "', ";
                    sql = sql + "SAFETY_IMAGE_UPLOAD='" + _sSAFETY_IMAGE_UPLOAD + "', SAFETY_ACTIVITY_DATE=SYSDATE,SAFETY_PUNCH_ID= '" + _sSAFETY_PUNCH_ID + "', ";
                    sql = sql + "SAFETY_PUNCH_NAME ='" + _sSAFETY_PUNCH_NAME + "', PUNCH_MODE='" + _sPUNCH_MODE + "', PUNCH_FLAG='" + _sPUNCH_FLAG + "', GIS_LAT='" + _sGIS_LAT + "',";
                    sql = sql + "GIS_LONG ='" + _sGIS_LONG + "', GIS_STATUS='" + _sGIS_STATUS + "',STATUS_FLAG='" + _sSTATUS_FLAG + "', MACHINE_ADD ='" + _sIPAddress + "' ";
                    sql = sql + " WHERE METER_NO = '" + _sMETER_NO + "' ";
                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    int i = cmd.ExecuteNonQuery();
                    if (i != 0)
                    {
                        OleDbConnection con1;
                        OleDbCommand cmd1;
                        OleDbDataAdapter adp1;
                        DataTable dt1 = new DataTable();
                        using (con1 = new OleDbConnection(strConnection))
                        {
                            string sql1 = " SELECT AUART, ERDAT, DIVISION,ACCOUNT_CLASS,CA_NO,NAME,TEL_NO,ADDRESS,METER_NO,FLAG " +
                            " FROM mobint.SAFETY_INPUT_DETAILS " +
                            " WHERE (METER_NO = '" + _sMETER_NO + "' and FLAG = 'E')";
                            cmd1 = new OleDbCommand(sql1, con1);
                            if (con1.State == ConnectionState.Closed)
                            {
                                con1.Open();
                            }
                            adp1 = new OleDbDataAdapter(cmd1);
                            adp1.Fill(dt1);
                            if (dt1.Rows.Count > 0)
                            {
                                OleDbConnection con2;
                                OleDbCommand cmd2;
                                using (con2 = new OleDbConnection(strConnection))
                                {
                                    //return true;
                                    string sql2 = "UPDATE mobint.SAFETY_INPUT_DETAILS SET FLAG = '" + _sSTATUS_FLAG + "'";
                                    sql2 = sql2 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                    cmd2 = new OleDbCommand(sql2, con2);
                                    if (con2.State == ConnectionState.Closed)
                                    {
                                        con2.Open();
                                    }
                                    int j = cmd2.ExecuteNonQuery();
                                    if (j != 0)
                                    {
                                        OleDbConnection con22;
                                        OleDbCommand cmd22;
                                        using (con22 = new OleDbConnection(strConnection))
                                        {
                                            //return true;
                                            string sql22 = "UPDATE mobint.EXECUTIVE_PUNCH_DETAILS SET STATUS_FLAG = '" + _sSTATUS_FLAG + "'";
                                            sql22 = sql22 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                            cmd22 = new OleDbCommand(sql22, con22);
                                            if (con22.State == ConnectionState.Closed)
                                            {
                                                con22.Open();
                                            }
                                            int z = cmd22.ExecuteNonQuery();
                                            if (z != 0)
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                OleDbConnection con2;
                                OleDbCommand cmd2;
                                using (con2 = new OleDbConnection(strConnection))
                                {
                                    //return true;
                                    string sql2 = "UPDATE mobint.EXECUTIVE_PUNCH_DETAILS SET STATUS_FLAG = '" + _sSTATUS_FLAG + "'";
                                    sql2 = sql2 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                    cmd2 = new OleDbCommand(sql2, con2);
                                    if (con2.State == ConnectionState.Closed)
                                    {
                                        con2.Open();
                                    }
                                    int j = cmd2.ExecuteNonQuery();
                                    if (j != 0)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    sql = "INSERT INTO mobint.SAFETY_PUNCH_DETAILS(UNIQUE_REF_NO,COMPANY_CODE, DIVISION, METER_NO, ORDERID, CA_NO, NAME, ADDRESS, TEL_NO, ACCOUNT_CLASS, SAFETY_SAFE_LOC, SAFETY_SAFE_REMARK, ";
                    sql += "SAFETY_IMAGE_UPLOAD, SAFETY_ACTIVITY_DATE,SAFETY_PUNCH_ID, SAFETY_PUNCH_NAME, PUNCH_MODE, PUNCH_FLAG, GIS_LAT, GIS_LONG, GIS_STATUS,STATUS_FLAG, MACHINE_ADD)";
                    sql = sql + " VALUES";
                    sql = sql + "('" + _autoID + "','" + _sCOMPANY_CODE + "','" + _sDIVISION + "','" + _sMETER_NO + "','" + _sORDERID + "','" + _sCA_NO + "','" + _sNAME + "','" + _sADDRESS + "','" + _sTEL_NO + "','" + _sACCOUNT_CLASS + "','" + _sSAFETY_SAFE_LOC + "','" + _sSAFETY_SAFE_REMARK + "', ";
                    sql = sql + "'" + _sSAFETY_IMAGE_UPLOAD + "',SYSDATE,'" + _sSAFETY_PUNCH_ID + "','" + _sSAFETY_PUNCH_NAME + "','" + _sPUNCH_MODE + "','" + _sPUNCH_FLAG + "','" + _sGIS_LAT + "','" + _sGIS_LONG + "','" + _sGIS_STATUS + "','" + _sSTATUS_FLAG + "','" + _sIPAddress + "')";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    int i = cmd.ExecuteNonQuery();
                    if (i != 0)
                    {
                        OleDbConnection con1;
                        OleDbCommand cmd1;
                        OleDbDataAdapter adp1;
                        DataTable dt1 = new DataTable();
                        using (con1 = new OleDbConnection(strConnection))
                        {
                            string sql1 = " SELECT AUART,ERDAT, DIVISION,ACCOUNT_CLASS,CA_NO,NAME,TEL_NO,ADDRESS,METER_NO,FLAG " +
                            " FROM mobint.SAFETY_INPUT_DETAILS " +
                            " WHERE (METER_NO = '" + _sMETER_NO + "' and FLAG = 'E')";
                            cmd1 = new OleDbCommand(sql1, con1);
                            if (con1.State == ConnectionState.Closed)
                            {
                                con1.Open();
                            }
                            adp1 = new OleDbDataAdapter(cmd1);
                            adp1.Fill(dt1);
                            if (dt1.Rows.Count > 0)
                            {
                                OleDbConnection con2;
                                OleDbCommand cmd2;
                                using (con2 = new OleDbConnection(strConnection))
                                {
                                    //return true;
                                    string sql2 = "UPDATE mobint.SAFETY_INPUT_DETAILS SET FLAG = '" + _sSTATUS_FLAG + "'";
                                    sql2 = sql2 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                    cmd2 = new OleDbCommand(sql2, con2);
                                    if (con2.State == ConnectionState.Closed)
                                    {
                                        con2.Open();
                                    }
                                    int j = cmd2.ExecuteNonQuery();
                                    if (j != 0)
                                    {
                                        OleDbConnection con22;
                                        OleDbCommand cmd22;
                                        using (con22 = new OleDbConnection(strConnection))
                                        {
                                            //return true;
                                            string sql22 = "UPDATE mobint.EXECUTIVE_PUNCH_DETAILS SET STATUS_FLAG = '" + _sSTATUS_FLAG + "'";
                                            sql22 = sql22 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                            cmd22 = new OleDbCommand(sql22, con22);
                                            if (con22.State == ConnectionState.Closed)
                                            {
                                                con22.Open();
                                            }
                                            int z = cmd22.ExecuteNonQuery();
                                            if (z != 0)
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                OleDbConnection con2;
                                OleDbCommand cmd2;
                                using (con2 = new OleDbConnection(strConnection))
                                {
                                    //return true;
                                    string sql2 = "UPDATE mobint.EXECUTIVE_PUNCH_DETAILS SET STATUS_FLAG = '" + _sSTATUS_FLAG + "'";
                                    sql2 = sql2 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                    cmd2 = new OleDbCommand(sql2, con2);
                                    if (con2.State == ConnectionState.Closed)
                                    {
                                        con2.Open();
                                    }
                                    int j = cmd2.ExecuteNonQuery();
                                    if (j != 0)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }


        private bool EXECUTIVE_PUNCH_DETAILS(string _sCOMPANY_CODE, string _sDIVISION, string _sMETER_NO, string _sORDERID, string _sCA_NO, string _sNAME, string _sADDRESS, string _sACCOUNT_CLASS, string _sEXEC_SAFE_LOC, string _sEXEC_SAFE_REMARK,
                                        string _sEXEC_METER_SHIFTED, string _sEXEC_IMAGE_UPLOAD, string _sEXEC_METER_SHIFT_REMARK, string _EXEC_METER_NOT_SHIFTED_REASON, string _sEXEC_METER_NOT_SHIFTED_REMARK, string _sEXEC_PUNCH_ID, string _sEXEC_PUNCH_NAME, string _sPUNCH_MODE, string _sPUNCH_FLAG, string _sGIS_LAT, string _sGIS_LONG, string _sGIS_STATUS, string _sSTATUS_FLAG,
                                        string _sSAFE_PLACE_METER_IMAGE, string _sSAFE_PLACE_METER_REMARK, string _sMETER_SHIFTING_POSSIBLE, string _sMETER_SHIFTED_PHYSICALLY, string _sCUSTOMER_AGREE, string _sFABRICATION_STATUS, string _sFABRICATION_IMAGE, string _sFABRICATION_REMARK, string _sREALLOCATION_SAFE_PLACE_IMAGE, string _sREALLOCATION_SAFE_PLACE_REMARK)
        {
            Random generator = new Random();
            int _autoID = generator.Next(1, 10000);

            string _sIPAddress = GetIPAddress();

            using (con = new OleDbConnection(strConnection))
            {
                string sqlget = "SELECT METER_NO FROM mobint.EXECUTIVE_PUNCH_DETAILS where METER_NO='" + _sMETER_NO + "'";
                cmd = new OleDbCommand(sqlget, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                adp = new OleDbDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    sql = "UPDATE mobint.EXECUTIVE_PUNCH_DETAILS SET EXEC_SAFE_LOC='" + _sEXEC_SAFE_LOC + "', EXEC_SAFE_REMARK='" + _sEXEC_SAFE_REMARK + "',";
                    sql = sql + " EXEC_METER_SHIFTED='" + _sEXEC_METER_SHIFTED + "', EXEC_IMAGE_UPLOAD='" + _sEXEC_IMAGE_UPLOAD + "', EXEC_METER_SHIFT_REMARK='" + _sEXEC_METER_SHIFT_REMARK + "', ";
                    sql = sql + "EXEC_METER_NOT_SHIFTED_REASON ='" + _EXEC_METER_NOT_SHIFTED_REASON + "', EXEC_METER_NOT_SHIFTED_REMARK='" + _sEXEC_METER_NOT_SHIFTED_REMARK + "' ,EXEC_ACTIVITY_DATE=SYSDATE, ";
                    sql = sql + "EXEC_PUNCH_ID ='" + _sEXEC_PUNCH_ID + "', EXEC_PUNCH_NAME='" + _sEXEC_PUNCH_NAME + "', MACHINE_ADD='" + _sIPAddress + "',PUNCH_MODE='" + _sPUNCH_MODE + "',";
                    sql = sql + " PUNCH_FLAG='" + _sPUNCH_FLAG + "',GIS_LAT='" + _sGIS_LAT + "',GIS_LONG='" + _sGIS_LONG + "',GIS_STATUS='" + _sGIS_STATUS + "',STATUS_FLAG='" + _sSTATUS_FLAG + "', ";
                    sql = sql + " SAFE_PLACE_METER_IMAGE = '" + _sSAFE_PLACE_METER_IMAGE + "', SAFE_PLACE_METER_REMARK = '" + _sSAFE_PLACE_METER_REMARK + "', METER_SHIFTING_POSSIBLE = '" + _sMETER_SHIFTING_POSSIBLE + "',METER_SHIFTED_PHYSICALLY = '" + _sMETER_SHIFTED_PHYSICALLY + "', CUSTOMER_AGREE = '" + _sCUSTOMER_AGREE + "', ";
                    sql = sql + " FABRICATION_STATUS = '" + _sFABRICATION_STATUS + "',  FABRICATION_IMAGE = '" + _sFABRICATION_IMAGE + "', FABRICATION_REMARK = '" + _sFABRICATION_REMARK + "', REALLOCATION_SAFE_PLACE_IMAGE = '" + _sREALLOCATION_SAFE_PLACE_IMAGE + "', REALLOCATION_SAFE_PLACE_REMARK = '" + _sREALLOCATION_SAFE_PLACE_REMARK + "' ";
                    sql = sql + " WHERE METER_NO = '" + _sMETER_NO + "' ";
                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        OleDbConnection con1;
                        OleDbCommand cmd1;
                        OleDbDataAdapter adp1;
                        DataTable dt1 = new DataTable();
                        using (con1 = new OleDbConnection(strConnection))
                        {
                            string sql1 = " SELECT AUART, ERDAT, DIVISION,ACCOUNT_CLASS,CA_NO,NAME,TEL_NO,ADDRESS,METER_NO,FLAG " +
                                " FROM mobint.SAFETY_INPUT_DETAILS " +
                                " WHERE (METER_NO = '" + _sMETER_NO + "') and (FLAG = 'N' or FLAG = 'E')";
                            cmd1 = new OleDbCommand(sql1, con1);
                            if (con1.State == ConnectionState.Closed)
                            {
                                con1.Open();
                            }
                            adp1 = new OleDbDataAdapter(cmd1);
                            adp1.Fill(dt1);
                            if (dt1.Rows.Count > 0)
                            {
                                OleDbConnection con2;
                                OleDbCommand cmd2;
                                using (con2 = new OleDbConnection(strConnection))
                                {
                                    //return true;
                                    string sql2 = "UPDATE mobint.SAFETY_INPUT_DETAILS SET FLAG = '" + _sSTATUS_FLAG + "', WEB_FLAG = '" + _sSTATUS_FLAG + "' ";
                                    //if (_EXEC_METER_NOT_SHIFTED_REASON == "CR")
                                    //{
                                    //    sql2 = sql2 + " , WEB_FLAG = 'PAONM' ";
                                    //}
                                    //else if (_EXEC_METER_NOT_SHIFTED_REASON == "NAS")
                                    //{
                                    //    sql2 = sql2 + " , WEB_FLAG = 'PAMMG' ";
                                    //}
                                    //else if (_EXEC_METER_NOT_SHIFTED_REASON == "SC")
                                    //{
                                    //    sql2 = sql2 + " , WEB_FLAG = 'PACH' ";
                                    //}
                                    sql2 = sql2 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                    cmd2 = new OleDbCommand(sql2, con2);
                                    if (con2.State == ConnectionState.Closed)
                                    {
                                        con2.Open();
                                    }
                                    int j = cmd2.ExecuteNonQuery();
                                    if (j > 0)
                                    {
                                        OleDbConnection con22;
                                        OleDbCommand cmd22;
                                        using (con22 = new OleDbConnection(strConnection))
                                        {
                                            //return true;
                                            string sql22 = "UPDATE mobint.EXECUTIVE_PUNCH_DETAILS SET STATUS_FLAG = '" + _sSTATUS_FLAG + "'";
                                            sql22 = sql22 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                            cmd22 = new OleDbCommand(sql22, con22);
                                            if (con22.State == ConnectionState.Closed)
                                            {
                                                con22.Open();
                                            }
                                            int z = cmd22.ExecuteNonQuery();
                                            if (z > 0)
                                                return true;
                                            else
                                                return false;
                                        }
                                    }
                                    else
                                        return false;
                                }
                            }
                            else
                            {

                                sql = "Insert into MOBINT.SAFETY_INPUT_DETAILS (COMP_CODE, METER_NO, CA_NO, DIVISION, SUB_DIVISION, ";
                                sql += " NAME, ADDRESS, TEL_NO, ACCOUNT_CLASS, SANCTIONED_LOAD, TARIFTYP_TARIFFTYPE, THFT_EXCP3, SEQUENCE, CATEGORY, ERDAT, ";
                                sql += " FLAG, WEB_FLAG, WEB_DATE, MACHINE_USER, MACHINE_ADD) ";
                                sql += " Values ";
                                sql += " ('" + _sCOMPANY_CODE + "', '" + _sMETER_NO + "', '" + _sCA_NO + "', '', '', '" + _sNAME + "', '" + _sADDRESS + "', '', '" + _sACCOUNT_CLASS + "', '', ";
                                sql += " '', '', '', '', sysdate, 'N', 'N', sysdate, 'MOBINT', '" + _sIPAddress + "') ";

                                cmd = new OleDbCommand(sql, con);
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                int z = cmd.ExecuteNonQuery();
                                if (z > 0)
                                {
                                    OleDbConnection con2;
                                    OleDbCommand cmd2;
                                    OleDbDataAdapter adp2;
                                    using (con2 = new OleDbConnection(strConnection))
                                    {
                                        //return true;
                                        string sql2 = "UPDATE mobint.SAFETY_INPUT_DETAILS SET FLAG = '" + _sSTATUS_FLAG + "', WEB_FLAG = '" + _sSTATUS_FLAG + "' ";

                                        //if (_EXEC_METER_NOT_SHIFTED_REASON == "CR")
                                        //{
                                        //    sql2 = sql2 + " , WEB_FLAG = 'PAONM' ";
                                        //}
                                        //else if (_EXEC_METER_NOT_SHIFTED_REASON == "NAS")
                                        //{
                                        //    sql2 = sql2 + " , WEB_FLAG = 'PAMMG' ";
                                        //}
                                        //else if (_EXEC_METER_NOT_SHIFTED_REASON == "SC")
                                        //{
                                        //    sql2 = sql2 + " , WEB_FLAG = 'PACH' ";
                                        //}
                                        sql2 = sql2 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                        cmd2 = new OleDbCommand(sql2, con2);
                                        if (con2.State == ConnectionState.Closed)
                                        {
                                            con2.Open();
                                        }
                                        int j = cmd2.ExecuteNonQuery();
                                        if (j > 0)
                                        {
                                            OleDbConnection con22;
                                            OleDbCommand cmd22;
                                            using (con22 = new OleDbConnection(strConnection))
                                            {
                                                //return true;
                                                string sql22 = "UPDATE mobint.EXECUTIVE_PUNCH_DETAILS SET STATUS_FLAG = '" + _sSTATUS_FLAG + "'";
                                                sql22 = sql22 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                                cmd22 = new OleDbCommand(sql22, con22);
                                                if (con22.State == ConnectionState.Closed)
                                                {
                                                    con22.Open();
                                                }
                                                int zy = cmd22.ExecuteNonQuery();
                                                if (zy > 0)
                                                    return true;
                                                else
                                                    return false;
                                            }
                                        }
                                        else
                                            return false;
                                    }
                                }
                                else
                                    return false;
                            }
                        }
                    }
                    else
                    {
                        OleDbConnection con2;
                        OleDbCommand cmd2;
                        using (con2 = new OleDbConnection(strConnection))
                        {
                            //return true;
                            string sql2 = "UPDATE mobint.EXECUTIVE_PUNCH_DETAILS SET STATUS_FLAG = '" + _sSTATUS_FLAG + "'";
                            sql2 = sql2 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                            cmd2 = new OleDbCommand(sql2, con2);
                            if (con2.State == ConnectionState.Closed)
                            {
                                con2.Open();
                            }
                            int j = cmd2.ExecuteNonQuery();
                            if (j > 0)
                                return true;
                            else
                                return false;
                        }
                    }
                }
                else
                {
                    sql = "INSERT INTO mobint.EXECUTIVE_PUNCH_DETAILS(UNIQUE_REF_NO,COMPANY_CODE, DIVISION, METER_NO, ORDERID, CA_NO,NAME,ADDRESS, ACCOUNT_CLASS, EXEC_SAFE_LOC, EXEC_SAFE_REMARK,";
                    sql = sql + " EXEC_METER_SHIFTED, EXEC_IMAGE_UPLOAD, EXEC_METER_SHIFT_REMARK, EXEC_METER_NOT_SHIFTED_REASON, EXEC_METER_NOT_SHIFTED_REMARK ,EXEC_ACTIVITY_DATE, EXEC_PUNCH_ID, EXEC_PUNCH_NAME, MACHINE_ADD,PUNCH_MODE, PUNCH_FLAG,GIS_LAT,GIS_LONG,GIS_STATUS,STATUS_FLAG,SAFE_PLACE_METER_IMAGE, SAFE_PLACE_METER_REMARK, METER_SHIFTING_POSSIBLE,METER_SHIFTED_PHYSICALLY, CUSTOMER_AGREE, FABRICATION_STATUS, FABRICATION_IMAGE, FABRICATION_REMARK, REALLOCATION_SAFE_PLACE_IMAGE, REALLOCATION_SAFE_PLACE_REMARK)";

                    sql = sql + " VALUES";
                    sql = sql + "('" + _autoID + "','" + _sCOMPANY_CODE + "','" + _sDIVISION + "','" + _sMETER_NO + "','" + _sORDERID + "','" + _sCA_NO + "','" + _sNAME + "','" + _sADDRESS + "','" + _sACCOUNT_CLASS + "',";
                    sql = sql + "'" + _sEXEC_SAFE_LOC + "','" + _sEXEC_SAFE_REMARK + "','" + _sEXEC_METER_SHIFTED + "','" + _sEXEC_IMAGE_UPLOAD + "','" + _sEXEC_METER_SHIFT_REMARK + "','" + _EXEC_METER_NOT_SHIFTED_REASON + "','" + _sEXEC_METER_NOT_SHIFTED_REMARK + "',sysdate,";
                    sql = sql + "'" + _sEXEC_PUNCH_ID + "','" + _sEXEC_PUNCH_NAME + "','" + _sIPAddress + "','" + _sPUNCH_MODE + "','" + _sPUNCH_FLAG + "','" + _sGIS_LAT + "','" + _sGIS_LONG + "','" + _sGIS_STATUS + "','" + _sSTATUS_FLAG + "', '" + _sSAFE_PLACE_METER_IMAGE + "', '" + _sSAFE_PLACE_METER_REMARK + "', '" + _sMETER_SHIFTING_POSSIBLE + "','" + _sMETER_SHIFTED_PHYSICALLY + "', '" + _sCUSTOMER_AGREE + "', '" + _sFABRICATION_STATUS + "', '" + _sFABRICATION_IMAGE + "', '" + _sFABRICATION_REMARK + "', '" + _sREALLOCATION_SAFE_PLACE_IMAGE + "', '" + _sREALLOCATION_SAFE_PLACE_REMARK + "')";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        OleDbConnection con1;
                        OleDbCommand cmd1;
                        OleDbDataAdapter adp1;
                        DataTable dt1 = new DataTable();
                        using (con1 = new OleDbConnection(strConnection))
                        {
                            string sql1 = " SELECT AUART,ERDAT, DIVISION,ACCOUNT_CLASS,CA_NO,NAME,TEL_NO,ADDRESS,METER_NO,FLAG " +
                             " FROM mobint.SAFETY_INPUT_DETAILS " +
                             " WHERE (METER_NO = '" + _sMETER_NO + "') and (FLAG = 'N' or FLAG = 'E')";
                            cmd1 = new OleDbCommand(sql1, con1);
                            if (con1.State == ConnectionState.Closed)
                                con1.Open();
                            adp1 = new OleDbDataAdapter(cmd1);
                            adp1.Fill(dt1);
                            if (dt1.Rows.Count > 0)
                            {
                                OleDbConnection con2;
                                OleDbCommand cmd2;
                                OleDbDataAdapter adp2;
                                using (con2 = new OleDbConnection(strConnection))
                                {
                                    //return true;
                                    string sql2 = "UPDATE mobint.SAFETY_INPUT_DETAILS SET FLAG = '" + _sSTATUS_FLAG + "', WEB_FLAG = '" + _sSTATUS_FLAG + "' ";

                                    //if (_EXEC_METER_NOT_SHIFTED_REASON == "CR")
                                    //{
                                    //    sql2 = sql2 + " , WEB_FLAG = 'PAONM' ";
                                    //}
                                    //else if (_EXEC_METER_NOT_SHIFTED_REASON == "NAS")
                                    //{
                                    //    sql2 = sql2 + " , WEB_FLAG = 'PAMMG' ";
                                    //}
                                    //else if (_EXEC_METER_NOT_SHIFTED_REASON == "SC")
                                    //{
                                    //    sql2 = sql2 + " , WEB_FLAG = 'PACH' ";
                                    //}
                                    sql2 = sql2 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                    cmd2 = new OleDbCommand(sql2, con2);
                                    if (con2.State == ConnectionState.Closed)
                                        con2.Open();
                                    int j = cmd2.ExecuteNonQuery();
                                    if (j > 0)
                                    {
                                        OleDbConnection con22;
                                        OleDbCommand cmd22;
                                        using (con22 = new OleDbConnection(strConnection))
                                        {
                                            //return true;
                                            string sql22 = "UPDATE mobint.EXECUTIVE_PUNCH_DETAILS SET STATUS_FLAG = '" + _sSTATUS_FLAG + "'";
                                            sql22 = sql22 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                            cmd22 = new OleDbCommand(sql22, con22);
                                            if (con22.State == ConnectionState.Closed)
                                                con22.Open();
                                            int z = cmd22.ExecuteNonQuery();
                                            if (z > 0)
                                                return true;
                                            else
                                                return false;
                                        }
                                    }
                                    else
                                        return false;
                                }
                            }
                            else
                            {
                                sql = "Insert into MOBINT.SAFETY_INPUT_DETAILS (COMP_CODE, METER_NO, CA_NO, DIVISION, SUB_DIVISION, ";
                                sql += " NAME, ADDRESS, TEL_NO, ACCOUNT_CLASS, SANCTIONED_LOAD, TARIFTYP_TARIFFTYPE, THFT_EXCP3, SEQUENCE, CATEGORY, ERDAT, ";
                                sql += " FLAG, WEB_FLAG, WEB_DATE, MACHINE_USER, MACHINE_ADD) ";
                                sql += " Values ";
                                sql += " ('" + _sCOMPANY_CODE + "', '" + _sMETER_NO + "', '" + _sCA_NO + "', '', '', '" + _sNAME + "', '" + _sADDRESS + "', '', '" + _sACCOUNT_CLASS + "', '', ";
                                sql += " '', '', '', '', sysdate, 'N', 'N', sysdate, 'MOBINT', '" + _sIPAddress + "') ";

                                cmd = new OleDbCommand(sql, con);
                                if (con.State == ConnectionState.Closed)
                                    con.Open();
                                int z = cmd.ExecuteNonQuery();
                                if (z > 0)
                                {
                                    OleDbConnection con2;
                                    OleDbCommand cmd2;
                                    OleDbDataAdapter adp2;
                                    using (con2 = new OleDbConnection(strConnection))
                                    {
                                        //return true;
                                        string sql2 = "UPDATE mobint.SAFETY_INPUT_DETAILS SET FLAG = '" + _sSTATUS_FLAG + "', WEB_FLAG = '" + _sSTATUS_FLAG + "' ";

                                        //if (_EXEC_METER_NOT_SHIFTED_REASON == "CR")
                                        //{
                                        //    sql2 = sql2 + " , WEB_FLAG = 'PAONM' ";
                                        //}
                                        //else if (_EXEC_METER_NOT_SHIFTED_REASON == "NAS")
                                        //{
                                        //    sql2 = sql2 + " , WEB_FLAG = 'PAMMG' ";
                                        //}
                                        //else if (_EXEC_METER_NOT_SHIFTED_REASON == "SC")
                                        //{
                                        //    sql2 = sql2 + " , WEB_FLAG = 'PACH' ";
                                        //}
                                        sql2 = sql2 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                        cmd2 = new OleDbCommand(sql2, con2);
                                        if (con2.State == ConnectionState.Closed)
                                        {
                                            con2.Open();
                                        }
                                        int j = cmd2.ExecuteNonQuery();
                                        if (j > 0)
                                        {
                                            OleDbConnection con22;
                                            OleDbCommand cmd22;
                                            using (con22 = new OleDbConnection(strConnection))
                                            {
                                                //return true;
                                                string sql22 = "UPDATE mobint.EXECUTIVE_PUNCH_DETAILS SET STATUS_FLAG = '" + _sSTATUS_FLAG + "'";
                                                sql22 = sql22 + " WHERE METER_NO IN ('" + _sMETER_NO + "')";
                                                cmd22 = new OleDbCommand(sql22, con22);
                                                if (con22.State == ConnectionState.Closed)
                                                    con22.Open();
                                                int zh = cmd22.ExecuteNonQuery();
                                                if (zh > 0)
                                                    return true;
                                                else
                                                    return false;
                                            }
                                        }
                                        else
                                            return false;
                                    }
                                }
                                else
                                    return false;
                            }
                        }
                    }
                    else
                        return false;
                }
            }
        }




        public string byteArrayToFile(byte[] byteArrayIn, string filename)
        {
            Utilities.Network.NetworkDrive nd = new Utilities.Network.NetworkDrive();
            string pth = string.Empty, sl = string.Empty;

            try
            {
                pth = @"\\10.125.64.236\UploadedImages";
                nd.MapNetworkDrive(@"\\10.125.64.236\UploadedImages", "Z:", "uploadimages", "Bses@123");
                sl = pth;
                DateTime date = new DateTime();
                string month_name = date.ToString("MMMM");
                //string year_name = date.ToString("yyyy");
                string year_name = DateTime.Now.Year.ToString();

                string _sDir = sl + @"\MAMS_SAFETY\" + year_name + "\\" + month_name;

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

                if (File.Exists(_sPath) == true)
                {
                    FileInfo fi = new FileInfo(_sPath);
                    if (fi.Length == 0)
                    {
                        File.Delete(_sPath);
                        _sPath = "";
                        // File.Move(_sPath, _sPath);
                    }
                }


                return _sPath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }



        [Route("API/TF/GET_SAFETY_INSPEC_LOGIN")]
        [HttpPost]
        public HttpResponseMessage GET_SAFETY_INSPEC_LOGIN(Safety_Login IL_Data)
        {
            try
            {
                using (con = new OleDbConnection(strConnection))
                {
                    //sql = "SELECT COUNT(*) as totalCount FROM MOBINT.MCR_INSPECTION_MASTER where CREATEDBY = '" + IP_Data.CREATEDBY + "'";

                    //sql = " SELECT EMP_NAME, EMP_ID, IMEI_NO, DIVISION, LOGIN_DATE, ROLE, MUD.ACTIVE_FLAG,LOGIN_TYPE,(SELECT version FROM MOBINT.MCR_APP_VERSION WHERE status='Y' AND ROWNUM=1) APP_VERSION_WEB FROM MOBINT.MCR_USER_DETAILS MUD, MOBINT.INSPECTION_LOGIN MLM WHERE MUD.EMP_ID= MLM.LOGIN_ID AND UPPER(LOGIN_ID)=UPPER('" + IL_Data.UserName + "') AND PASSWORD='" + IL_Data.Password + "' and LOGIN_TYPE = 'IM'  ";

                    sql = "SELECT LOGIN_NAME EMP_NAME, LOGIN_ID EMP_ID, IMEI_NO, DIVISION_ID DIVISION, PASS_UPDATED_DATE LOGIN_DATE, LOGIN_TYPE ROLE, ACTIVE_FLAG,LOGIN_TYPE,";
                    sql += " APP_VERSION_WEB FROM MOBINT.INSPECTION_LOGIN WHERE UPPER(LOGIN_ID)=UPPER('" + IL_Data.UserName + "') AND PASSWORD='" + IL_Data.Password + "' ";

                    cmd = new OleDbCommand(sql, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        objresult.Result = dt;
                        objresult.Key = "Success";
                        objresult.Status = "200";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        objresult.Key = "Record not found try again";
                        objresult.Status = "421";
                        Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                        return Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                objresult.Key = ex.Message.ToString();
                objresult.Status = "421";
                Msg = Request.CreateResponse(HttpStatusCode.OK, objresult);
                return Msg;
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

        #endregion
    }
}