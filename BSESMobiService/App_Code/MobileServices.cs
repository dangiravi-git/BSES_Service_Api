using ESRI.ArcGIS.Client.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using ESRI.ArcGIS.Client.Geometry;
//using System.Data.OleDb;
using System.Reflection;
using System.Linq;
using USEPMS;
using Oracle.DataAccess.Client;
using ESRI.ArcGIS.Client.ValueConverters;
using System.Web.SessionState;
using System.Text;
using aejw.Network;
using System.Net;
using System.Data.OleDb;

[WebService(Namespace = "http://10.125.88.80/")]
//[WebService(Namespace = "http://tempuri.org/")]

[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class Service : System.Web.Services.WebService
{
    public Service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Locate Us Program

    public string url()
    {
        return ConfigurationManager.AppSettings["URL"].ToString();
    }

    public string GisSafetyUrl()
    {
        return ConfigurationManager.AppSettings["URL"].ToString(); //GIS_SAFETY
    }

    #region Hard Coded
    string invalswitchid = "'8583','8588','8558','8563','8559','8560','8914','8904','8908','13514','8902','8893','9477','9465'," +
                          "'9490','9482','9474','9483','9332','9320','9340','9335','9326','9338','9494','9508','9512','9509','9513','9518','9499','8623','8632'," +
                          "'8635','8617','8618','8620','8607','8606','8604','8590','8963','8969','8967','8956','8960','8957','8860','8854','8856','8846','8844'," +
                          "'8850','8851','8457','8434','8458','8437','8438','8433','9219','9216','9192','9200','9201','9197','8383','8378','8379','8380','8222'," +
                          "'8227','8208','8209','8248','8231','8234','8233','9247','9244','9231','9251','9234','14315','14317','9118','9126','9123','9125','9590'," +
                          "'9591','9593','9569','9570','9561','400074','400076','400081','400082','400328','400325','401067','12142','8539','8529','8527','8536','9395'," +
                          "'9407','9397','9410','8663','8656','8642','8646','8399','8406','8404','8400','8402','8394','8253','8257','8254','8250','8265','8258','8249'," +
                          "'13262','13263','8489','8462','8490','8473','8480','9545','9542','9536','9541','9544','9532','9535','9537','8758','8765','8773','8769','8768'," +
                          "'8757','8771','8760','8776','8775','9264','9284','9266','9269','9258','9268','8504','8497','8498','8495','8508','8507','12091','9022','9020','9009'," +
                          "'9011','12094','9651','9650','9647','9635','9642','9646','9641','8931','8937','8921','8922','8924','8925','12081','12080','8811','8836','8828','8812'," +
                          "'8821','8816','9043','9026','9024','9031','8732','8753','8735','8734','9156','9186','9157','9163','9164','9160','9348','9349','9347','9351','13408','13410'," +
                          "'9297','9298','9292','9294','14037','14036','8286','8313','8291','8293','8294','9610','9597','9609','9606','9605','9598','9599','9611','9430','9431','9447','9450'," +
                          "'9457','13603','8995','8996','8988','8984','9149','9151','9055','9060','8150','8678','13555','8670','8671','8673','8677','8679','8345','8346','8319','8322','8325'," +
                          "'8317','8371','8373','8354','8351','8720','8725','8702','8704','9097','9102','9080','9085'";
    #endregion

    #region For Important

    private DataSet GetDataAndFeatureSet(string url, Query query, ref FeatureSet result)
    {
        DataSet zzzz = new DataSet();
        QueryTask queryTask = new QueryTask(url);
        queryTask.Failed += queryTask_Failed;
        result = queryTask.Execute(query);
        DataTable dt = new DataTable();
        if (result.Features.Count > 0)
        {
            if (result.Features[0].Attributes.Count == 0)
            {
                return zzzz;
            }
            else
            {
                dt = getheader(dt, result.Features[0].Attributes);
                dt = getvalue(dt, result);
            }
        }
        else
        {
            return zzzz;
        }
        zzzz.Tables.Add(dt);
        return zzzz;
    }

    private DataSet getset(string url, Query query)
    {
        DataSet zzzz = new DataSet();
        QueryTask queryTask = new QueryTask(url);
        queryTask.Failed += queryTask_Failed;
        FeatureSet result = queryTask.Execute(query);
        DataTable dt = new DataTable();
        if (result.Features.Count > 0)
        {
            if (result.Features[0].Attributes.Count == 0)
            {
                return zzzz;
            }
            else
            {
                dt = getheader(dt, result.Features[0].Attributes);
                dt = getvalue(dt, result);
            }
        }
        else
        {
            return zzzz;
        }
        zzzz.Tables.Add(dt);
        return zzzz;
    }
    private DataTable getset_1(string url, Query query)
    {
        QueryTask queryTask = new QueryTask(url);
        queryTask.Failed += queryTask_Failed;
        FeatureSet result = queryTask.Execute(query);
        DataTable dt = new DataTable();
        if (result.Features.Count > 0 && result.Features[0].Attributes.Count > 0)
        {
            dt = getheader(dt, result.Features[0].Attributes);
            dt = getvalue(dt, result);
        }
        else
        {
            return dt;
        }
        return dt;
    }
    private DataTable getheader(DataTable dt, IDictionary<string, object> attr)
    {
        dt.Columns.AddRange(attr.Keys.Select(r => new DataColumn(r)).ToArray());
        return dt;
    }
    private DataTable getvalue(DataTable dt, FeatureSet results)
    {
        foreach (var features in results.Features)
        {
            dt.Rows.Add(features.Attributes.Values.Select(r => r).ToArray());
        }
        return dt;
    }
    private void queryTask_Failed(object sender, TaskFailedEventArgs args)
    {
        DataTable dt = new DataTable();
        dt.Rows.Add("Error in your query");
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
    }
    private void ExporttoExcel(DataTable table, string filename)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;

        for (int j = 0; j < columnscount; j++)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(table.Columns[j].ToString());
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
    public void CreateCSVFile(DataTable dt, string strFilePath)
    {
        try
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            int columnCount = dt.Columns.Count;
            for (int i = 0; i < columnCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < columnCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < columnCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region GISLATLONG

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet Get_Lat_Long(string Longitude, string Latitude)
    {
        string division = "";
        string subdivision = "";
        string div_code = "";
        string subdiv_code = "";
        double lng = Convert.ToDouble(Longitude);
        double lat = Convert.ToDouble(Latitude);

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt.Columns.Add("DIVISION", typeof(string)); dt.Columns.Add("DIVISION_C", typeof(string));
        dt.Columns.Add("SUBDIVISION", typeof(string)); dt.Columns.Add("SUBDIVISION_C", typeof(string));
        try
        {
            if (!String.IsNullOrEmpty(Latitude) && !String.IsNullOrEmpty(Longitude))//Longitude != "" && Latitude != ""
            {
                var point = new MapPoint(lng, lat, new SpatialReference("wkid: 4326"));
                //division AND subdivision
                QueryTask queryTask = new QueryTask(url() + "23");
                Query query1 = new Query();
                query1.ReturnGeometry = false;
                query1.OutFields.AddRange(new string[] { "*" });
                query1.Where = "1=1";
                query1.Geometry = point;
                query1.SpatialRelationship = SpatialRelationship.esriSpatialRelIntersects;
                FeatureSet resultz = queryTask.Execute(query1);

                if (resultz.Features.Count > 0)
                {
                    division = resultz.Features[0].Attributes["DIVISION_NAME"].ToString();
                    div_code = resultz.Features[0].Attributes["DIVISION_CODE"].ToString();
                    subdivision = resultz.Features[0].Attributes["SUBDIVISION_NAME"].ToString();
                    subdiv_code = resultz.Features[0].Attributes["SUBDIVISION_CODE"].ToString();
                    dt.Rows.Add(division, div_code, subdivision, subdiv_code);
                }

            }
            ds.Tables.Add(dt);
        }
        catch (Exception ex)
        { }
        return ds;
    }

    //    public string converLatLongToUtm (double strLat, double strLong)
    //{

    //}

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet Get_Lat_Long_Android(string Longitude, string Latitude)
    {
        string division = "";
        string erection = "";
        string div_code = "";
        string xmax = "";
        string ymax = "";
        string facilityid = "";
        //string comments = "";
        double lng = Convert.ToDouble(Longitude);
        double lat = Convert.ToDouble(Latitude);

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt.Columns.Add("DIVISION", typeof(string)); dt.Columns.Add("DIVISION_C", typeof(string));
        dt.Columns.Add("COMPCENTERNAME", typeof(string)); dt.Columns.Add("COMPCENTERADDRESS", typeof(string));
        dt.Columns.Add("ERECTIONCO", typeof(string)); dt.Columns.Add("FACILITYID", typeof(string)); //dt.Columns.Add("COMMENTS", typeof(string)); 
        dt.Columns.Add("LONGITUDE", typeof(string)); dt.Columns.Add("LATITUDE", typeof(string));
        try
        {
            if (!String.IsNullOrEmpty(Latitude) && !String.IsNullOrEmpty(Longitude))
            {


                //4326
                var point = new MapPoint(lng, lat, new SpatialReference("wkid: 4326"));
                //var point = new MapPoint(lng, lat, new SpatialReference("PROJCS[\"Reliance_LCC\",GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Lambert_Conformal_Conic\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",82.5],PARAMETER[\"Standard_Parallel_1\",11.16],PARAMETER[\"Standard_Parallel_2\",31.84],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]"));
                //division AND subdivision
                QueryTask queryTask = new QueryTask(GisSafetyUrl() + "23");
                Query query1 = new Query();
                query1.ReturnGeometry = true;
                query1.OutFields.AddRange(new string[] { "*" });
                query1.Where = "1=1";
                query1.Geometry = point;
                query1.SpatialRelationship = SpatialRelationship.esriSpatialRelIntersects;
                FeatureSet resultz = queryTask.Execute(query1);

                if (resultz.Features.Count > 0)
                {
                    division = resultz.Features[0].Attributes["DIVISION_NAME"].ToString();
                    div_code = resultz.Features[0].Attributes["DIVISION_CODE"].ToString();
                    QueryTask queryTask1 = new QueryTask(GisSafetyUrl() + "2");
                    Query query2 = new Query();
                    query2.ReturnGeometry = false;
                    query2.OutFields.AddRange(new string[] { "FACILITYID", "ERECTIONCO", "NAME", "ADDRESS", "COMMENTS" });
                    query2.Geometry = resultz.Features[0].Geometry;
                    query2.SpatialRelationship = SpatialRelationship.esriSpatialRelIntersects;
                    query2.Where = "FACILITYID IS NOT NULL AND ERECTIONCO IS NOT NULL ";
                    FeatureSet result = queryTask1.Execute(query2);
                    foreach (ESRI.ArcGIS.Client.Graphic set in result.Features)
                    {
                        facilityid = set.Attributes["FACILITYID"].ToString();
                        erection = set.Attributes["ERECTIONCO"].ToString();
                        //comments = set.Attributes["COMMENTS"].ToString();
                        Query query = new Query();
                        query.ReturnGeometry = true;
                        query.OutFields.AddRange(new string[] { "*" });
                        query.Geometry = set.Geometry;
                        query.SpatialRelationship = SpatialRelationship.esriSpatialRelIntersects;
                        query.Where = "FACILITYID = '" + facilityid + "'";
                        queryTask1 = new QueryTask(GisSafetyUrl() + "2");
                        FeatureSet resultNew = queryTask1.Execute(query);
                        xmax = resultNew.Features[0].Geometry.Extent.XMax.ToString();
                        ymax = resultNew.Features[0].Geometry.Extent.YMax.ToString();
                        dt.Rows.Add(division, div_code, set.Attributes["NAME"].ToString(), set.Attributes["ADDRESS"].ToString(), erection, facilityid, xmax, ymax);
                    }
                }
                else
                {
                    dt.Rows.Add("Not in BRPL", "NA", "NA", "NA", "NA", "NA", "NA", "NA");
                }
            }
            ds.Tables.Add(dt);
        }
        catch (Exception ex)
        { }
        return ds;
    }

    #endregion

    #endregion

    public UserCredentials consumer; //09012015

    #region "User Authontication"

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

    private void InsertCallWidFunction(string strWebMthdId, string ipAddressAll, string strSessionId, string strUserName, string strMethodName)
    {

        //string strDt = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
        //strDt = "to_date('" + strDt + "','dd-Mon-yyyy hh24:mi:ss')";

        //NewClassFile.insertUserCallWidFunction(strWebMthdId, ipAddressAll, strSessionId, strDt, strUserName, strMethodName);

    }

    private void UpdateOutputCallWidFunction(string strSessionId, string strWebMthdId, string strWebMethodName)
    {
        //string strDt = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
        //strDt = "to_date('" + strDt + "','dd-Mon-yyyy hh24:mi:ss')";

        //NewClassFile.updateUserCallWidFunction(strDt, strSessionId, strWebMthdId, strWebMethodName);
    }

    private DataTable InvaildAuthontication()
    {
        DataTable dterror = new DataTable();
        dterror.TableName = "AuthonticationTable";
        dterror.Columns.Add("AUTHONTICATION");
        dterror.Rows.Add("INVAILD");
        return dterror;
    }

    private DataSet InvaildAuthonticationds()
    {
        DataSet ds = new DataSet("AUTHONTICATION");
        DataTable dterror = new DataTable();
        dterror.TableName = "AuthonticationTable";
        dterror.Columns.Add("AUTHONTICATION");
        dterror.Rows.Add("INVAILD");
        ds.Tables.Add(dterror);
        return ds;
    }

    private DataSet InvaildAppCodeU01()
    {
        DataSet ds = new DataSet("BAPI_RESULT");

        //outputFlagsTable
        DataTable dtOutputFlag = new DataTable();
        dtOutputFlag.TableName = "outputFlagsTable";
        dtOutputFlag.Columns.Add("E_Flag_Ap");
        dtOutputFlag.Columns.Add("E_Flag_Bp");
        dtOutputFlag.Columns.Add("E_Flag_So");
        dtOutputFlag.Columns.Add("E_Flag_Us");
        dtOutputFlag.Columns.Add("E_New_Partner");
        dtOutputFlag.Columns.Add("E_Service_Order");
        DataRow row = dtOutputFlag.NewRow();
        row["E_Flag_Ap"] = string.Empty;
        row["E_Flag_Bp"] = string.Empty;
        row["E_Flag_So"] = string.Empty;
        row["E_Flag_Us"] = string.Empty;
        row["E_New_Partner"] = string.Empty;
        row["E_Service_Order"] = "We have released new version 2.1 to avail services please download our latest app from website or play store.";
        dtOutputFlag.Rows.Add(row);

        //SAPDATA_ErrorDataTable
        DataTable dterror = new DataTable();
        dterror.TableName = "SAPDATA_ErrorDataTable";
        dterror.Columns.Add("Type");
        dterror.Columns.Add("Id");
        dterror.Columns.Add("Number");
        dterror.Columns.Add("Message");
        dterror.Columns.Add("Log_No");
        dterror.Columns.Add("Log_Msg_No");
        dterror.Columns.Add("Message_V1");
        dterror.Columns.Add("Message_V2");
        dterror.Columns.Add("Message_V3");
        dterror.Columns.Add("Message_V4");
        dterror.Columns.Add("Parameter");
        dterror.Columns.Add("Row");
        dterror.Columns.Add("Field");
        dterror.Columns.Add("System");

        DataRow rowError = dterror.NewRow();
        rowError["Type"] = "E";
        rowError["Id"] = string.Empty;
        rowError["Number"] = "000";
        rowError["Message"] = "We have released new version 2.1 to avail services please download our latest app from website or play store.";
        rowError["Log_No"] = string.Empty;
        rowError["Log_Msg_No"] = "000000";
        rowError["Message_V1"] = string.Empty;
        rowError["Message_V2"] = string.Empty;
        rowError["Message_V3"] = string.Empty;
        rowError["Message_V4"] = string.Empty;
        rowError["Parameter"] = string.Empty;
        rowError["Row"] = "0";
        rowError["Field"] = string.Empty;
        rowError["System"] = string.Empty;
        dterror.Rows.Add(rowError);

        ds.Tables.Add(dtOutputFlag);
        ds.Tables.Add(dterror);

        return ds;
    }

    private DataSet InvaildAppCode()
    {
        DataSet ds = new DataSet("BAPI_RESULT");

        //outputFlagsTable
        DataTable dtOutputFlag = new DataTable();
        dtOutputFlag.TableName = "outputFlagsTable";
        dtOutputFlag.Columns.Add("E_Flag_Ap");
        dtOutputFlag.Columns.Add("E_Flag_Bp");
        dtOutputFlag.Columns.Add("E_Flag_So");
        dtOutputFlag.Columns.Add("E_Flag_Us");
        dtOutputFlag.Columns.Add("E_New_Partner");
        dtOutputFlag.Columns.Add("E_Service_Order");
        DataRow row = dtOutputFlag.NewRow();
        row["E_Flag_Ap"] = string.Empty;
        row["E_Flag_Bp"] = string.Empty;
        row["E_Flag_So"] = string.Empty;
        row["E_Flag_Us"] = string.Empty;
        row["E_New_Partner"] = string.Empty;
        row["E_Service_Order"] = string.Empty;
        dtOutputFlag.Rows.Add(row);

        //SAPDATA_ErrorDataTable
        DataTable dterror = new DataTable();
        dterror.TableName = "SAPDATA_ErrorDataTable";
        dterror.Columns.Add("Type");
        dterror.Columns.Add("Id");
        dterror.Columns.Add("Number");
        dterror.Columns.Add("Message");
        dterror.Columns.Add("Log_No");
        dterror.Columns.Add("Log_Msg_No");
        dterror.Columns.Add("Message_V1");
        dterror.Columns.Add("Message_V2");
        dterror.Columns.Add("Message_V3");
        dterror.Columns.Add("Message_V4");
        dterror.Columns.Add("Parameter");
        dterror.Columns.Add("Row");
        dterror.Columns.Add("Field");
        dterror.Columns.Add("System");

        DataRow rowError = dterror.NewRow();
        rowError["Type"] = "E";
        rowError["Id"] = string.Empty;
        rowError["Number"] = "000";
        rowError["Message"] = "We have released new version 2.1 to avail services please download our latest app from website or play store.";
        rowError["Log_No"] = string.Empty;
        rowError["Log_Msg_No"] = "000000";
        rowError["Message_V1"] = string.Empty;
        rowError["Message_V2"] = string.Empty;
        rowError["Message_V3"] = string.Empty;
        rowError["Message_V4"] = string.Empty;
        rowError["Parameter"] = string.Empty;
        rowError["Row"] = "0";
        rowError["Field"] = string.Empty;
        rowError["System"] = string.Empty;
        dterror.Rows.Add(rowError);

        ds.Tables.Add(dtOutputFlag);
        ds.Tables.Add(dterror);

        return ds;
    }

    private DataSet NotAvail_ds()
    {
        DataSet ds = new DataSet("Service");
        DataTable dterror = new DataTable();
        dterror.TableName = "ServicesResult";
        dterror.Columns.Add("Result");
        dterror.Rows.Add("OOPs! Not Available");
        ds.Tables.Add(dterror);
        return ds;
    }

    #endregion

    #region Mobile Service Live

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet ZBAPI_EDISTRICT(string strCANumber, string strCRNNumber, string txnID)
    {
        AllInsert allInsert = new AllInsert();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();
            string CA_NUMBER = "";
            string CRN_NUMBER = "";
            string TXN_ID = "";
            string BP_NUMBER = "";
            string BP_NAME = "";
            string BP_TYPE = "";
            string SEARCH_TERM1 = "";
            string SEARCH_TERM2 = "";
            string HOUSE_NO = "";
            string HOUSE_NO_SUP = "";
            string FLOOR = "";
            string PREMISE_TYPE = "";
            string STREET = "";
            string STREET2 = "";
            string STREET3 = "";
            string STREET4 = "";
            string CITY = "";
            string POSTAL_CODE = "";
            string REGION = "";
            string COUNTRY = "";
            string DESC_CON_OBJECT = "";
            string REG_STR_GROUP = "";
            string DEVICE_SERIAL_NUMBER = "";
            string PHONE_NUMBER = "";
            string MRU = "";
            string FUNC_DESCR = "";
            string OUTAGE_FROMTIME = "";
            string OUTAGE_TOTIME = "";
            string LEGACY_ACCT = "";
            string BILL_CLASS = "";
            string RATE_CAT = "";
            string ACTIVITY = "";
            string ADR_NOTES = "";
            string TEL1_NUMBER = "";
            string VERTRAG = "";
            string EMAIL = "";
            string MOVE_OUT_DATE = "";
            string CON_OBJ_NUMBER = "";
            string CLERK_ID = "";
            string TEXT = "";
            string STATUS = "";
            string DISCREATION = "";
            string TARIFTYP = "";
            string WERT1 = "";
            string CONS_SINCE = "";
            string POLE_NUMBER = "";
            string SEQUENCE_NUMBER = "";
            string LAST_BILL_DATE = "";

            DataSet ds = new DataSet();
            if (strCANumber.Length < 12 && strCANumber.Length > 0)
                strCANumber = strCANumber.PadLeft(12, '0');
            if (strCRNNumber.Length < 10 && strCRNNumber.Length > 0)
                strCRNNumber = strCRNNumber.PadLeft(10, '0');

            ds = isu.ZBAPI_EDISTRICT(strCANumber, strCRNNumber);
            if (ds.Tables["EDISTRCTTable"].Rows.Count > 0)
            {
                CA_NUMBER = ds.Tables["EDISTRCTTable"].Rows[0]["Ca_Number"].ToString().Trim();
                CRN_NUMBER = strCRNNumber;
                TXN_ID = txnID;
                BP_NUMBER = ds.Tables[0].Rows[0]["Bp_Number"].ToString().Trim();
                BP_NAME = ds.Tables[0].Rows[0]["Bp_Name"].ToString().Trim();
                BP_TYPE = ds.Tables[0].Rows[0]["Bp_Type"].ToString().Trim();
                SEARCH_TERM1 = ds.Tables[0].Rows[0]["Search_Term1"].ToString().Trim();
                SEARCH_TERM2 = ds.Tables[0].Rows[0]["Search_Term2"].ToString().Trim();
                HOUSE_NO = ds.Tables[0].Rows[0]["House_Number"].ToString().Trim();
                HOUSE_NO_SUP = ds.Tables[0].Rows[0]["House_Number_Sup"].ToString().Trim();
                FLOOR = ds.Tables[0].Rows[0]["Floor"].ToString().Trim();
                PREMISE_TYPE = ds.Tables[0].Rows[0]["Premise_Type"].ToString().Trim();
                STREET = ds.Tables[0].Rows[0]["Street"].ToString().Trim();
                STREET2 = ds.Tables[0].Rows[0]["Street2"].ToString().Trim();
                STREET3 = ds.Tables[0].Rows[0]["Street3"].ToString().Trim();
                STREET4 = ds.Tables[0].Rows[0]["Street4"].ToString().Trim();
                CITY = ds.Tables[0].Rows[0]["City"].ToString().Trim();
                POSTAL_CODE = ds.Tables[0].Rows[0]["Post_Code"].ToString().Trim();
                REGION = ds.Tables[0].Rows[0]["Region"].ToString().Trim();
                COUNTRY = ds.Tables[0].Rows[0]["Country"].ToString().Trim();
                DESC_CON_OBJECT = ds.Tables[0].Rows[0]["Desc_Con_Object"].ToString().Trim();
                REG_STR_GROUP = ds.Tables[0].Rows[0]["Reg_Str_Group"].ToString().Trim();
                DEVICE_SERIAL_NUMBER = ds.Tables[0].Rows[0]["Device_Sr_Number"].ToString().Trim();
                PHONE_NUMBER = ds.Tables[0].Rows[0]["Telephone_No"].ToString().Trim();
                MRU = ds.Tables[0].Rows[0]["Mru"].ToString().Trim();
                FUNC_DESCR = ds.Tables[0].Rows[0]["Func_Descr"].ToString().Trim();
                OUTAGE_FROMTIME = ds.Tables[0].Rows[0]["Outage_Fromtime"].ToString().Trim();
                OUTAGE_TOTIME = ds.Tables[0].Rows[0]["Outage_Totime"].ToString().Trim();
                LEGACY_ACCT = ds.Tables[0].Rows[0]["Legacy_Acct"].ToString().Trim();
                BILL_CLASS = ds.Tables[0].Rows[0]["Bill_Class"].ToString().Trim();
                RATE_CAT = ds.Tables[0].Rows[0]["Rate_Cat"].ToString().Trim();
                ACTIVITY = ds.Tables[0].Rows[0]["Activity"].ToString().Trim();
                ADR_NOTES = ds.Tables[0].Rows[0]["Adr_Notes"].ToString().Trim();
                TEL1_NUMBER = ds.Tables[0].Rows[0]["Tel1_Number"].ToString().Trim();
                VERTRAG = ds.Tables[0].Rows[0]["Vertrag"].ToString().Trim();
                EMAIL = ds.Tables[0].Rows[0]["E_Mail"].ToString().Trim();
                MOVE_OUT_DATE = ds.Tables[0].Rows[0]["Move_Out_Date"].ToString().Trim();
                CON_OBJ_NUMBER = ds.Tables[0].Rows[0]["Con_Obj_No"].ToString().Trim();
                CLERK_ID = ds.Tables[0].Rows[0]["Clerk_Id"].ToString().Trim();
                TEXT = ds.Tables[0].Rows[0]["Text"].ToString().Trim();
                STATUS = ds.Tables[0].Rows[0]["Status"].ToString().Trim();
                DISCREATION = ds.Tables[0].Rows[0]["Discreason"].ToString().Trim();
                TARIFTYP = ds.Tables[0].Rows[0]["TARIFTYP"].ToString().Trim();
                WERT1 = ds.Tables[0].Rows[0]["WERT1"].ToString().Trim();
                CONS_SINCE = ds.Tables[0].Rows[0]["CONS_SINCE"].ToString().Trim();
                POLE_NUMBER = ds.Tables[0].Rows[0]["POLE_NO"].ToString().Trim();
                SEQUENCE_NUMBER = ds.Tables[0].Rows[0]["SEQUENC_NO"].ToString().Trim();
                LAST_BILL_DATE = ds.Tables[0].Rows[0]["LAST_BILL_DATE"].ToString().Trim();
                bool isInserted = false;
                isInserted = allInsert.eDistrict(CA_NUMBER, CRN_NUMBER, TXN_ID, BP_NUMBER, BP_NAME,
            BP_TYPE, SEARCH_TERM1, SEARCH_TERM2, HOUSE_NO, HOUSE_NO_SUP,
            FLOOR, PREMISE_TYPE, STREET, STREET2, STREET3,
            STREET4, CITY, POSTAL_CODE, REGION, COUNTRY,
            DESC_CON_OBJECT, REG_STR_GROUP, DEVICE_SERIAL_NUMBER, PHONE_NUMBER, MRU,
            FUNC_DESCR, OUTAGE_FROMTIME, OUTAGE_TOTIME, LEGACY_ACCT, BILL_CLASS,
            RATE_CAT, ACTIVITY, ADR_NOTES, TEL1_NUMBER, VERTRAG,
            EMAIL, MOVE_OUT_DATE, CON_OBJ_NUMBER, CLERK_ID, TEXT,
            STATUS, DISCREATION, TARIFTYP, WERT1, CONS_SINCE,
            POLE_NUMBER, SEQUENCE_NUMBER, LAST_BILL_DATE);

                if (isInserted == true)
                {
                    return ds;
                }
                else
                    return new DataSet();
            }
            else
                return new DataSet();
        }
        else
        {
            return (InvaildAuthonticationds());
        }

    }

    [WebMethod]
    [SoapHeader("consumer", Required = true)]
    public DataTable Z_BAPI_DSS_ISU_CA_DISPLAY_1(string strCANumber)
    {
        AllSelect allSelect = new AllSelect();

        // if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            // if (strCANumber.Length < 12 && strCANumber.Length > 0)
            //     strCANumber = strCANumber.PadLeft(12, '0');
            //// dt = isu.Z_BAPI_DSS_ISU_CA_DISPLAY(strCANumber, "").Tables[0];            

            // string sql = " select CONTRACT_ACCOUNT Ca_Number,BUSINESS_PARTNER Bp_Number , TEL_NUMBER Telephone_No,EMAIL E_Mail, RATE_CATEGORY rate_cat, MOVE_OUT Move_Out_Date, ";
            // sql = sql + " ACCOUNT_CLASS Bill_Class ,'' Bp_Name,'' Bp_Type,'' Search_Term1,'' Search_Term2, ";
            // sql = sql + " '' House_Number,'' House_Number_Sup,'' Floor,'' Premise_Type,ADDRESS Street,'' Street2,'' Street3,'' Street4,'' City,'' Post_Code, ";
            // sql = sql + " '' Region,'' Country,'' Desc_Con_Object,'' Reg_Str_Group,'' Device_Sr_Number,'' Mru,'' Func_Descr,'' Outage_Fromtime,'' Outage_Totime,'' Legacy_Acct,  ";
            // sql = sql + " '' Activity,'' Adr_Notes,MOBILE_NO Tel1_Number,'' Vertrag,'' Con_Obj_No,'' Clerk_Id,'' Text,CONSUMER_STATUS Status,'' Discreason,'' TARIFTYP,'' WERT1 from pcm.CONSUMER_SAP_MASTER ";
            // sql = sql + " WHERE CONTRACT_ACCOUNT='"+strCANumber+"' ";

            //dt= allSelect.GetShowPCMDetails(sql);

            // if (dt.Rows.Count > 0)
            // {
            //     return dt;
            // }
            // else
            // {

            NewClassFile newClassFile = new NewClassFile();
            ds = new DataSet("BAPI_RESULT");

            if (strCANumber.Length == 11)
            {
                strCANumber = strCANumber.Substring(1, 10);
            }
            if (strCANumber.Length == 12)
            {
                strCANumber = strCANumber.Substring(2, 10);
            }
            if (strCANumber.Length == 9)
            {
                strCANumber = strCANumber.PadLeft(10, '0');
            }

            DataTable _dtSap = new DataTable("SAPDATA_ErrorDataTable");
            DataColumn dcol;

            dcol = new DataColumn();
            dcol.DataType = System.Type.GetType("System.String");
            dcol.ColumnName = "Message";
            _dtSap.Columns.Add(dcol);

            dcol = new DataColumn();
            dcol.DataType = System.Type.GetType("System.String");
            dcol.ColumnName = "Type";
            _dtSap.Columns.Add(dcol);

            DataRow row = _dtSap.NewRow();
            row["Message"] = "CA Number not found";
            row["Type"] = string.Empty;


            DataTable dtISUSTDTable = new DataTable("ISUSTDTable");
            dtISUSTDTable = newClassFile.GetRCMSAP_DataCAWise(strCANumber, "");
            if (dtISUSTDTable.Rows.Count > 0)
            {
                ds.Merge(dtISUSTDTable);

                return ds.Tables[0];
            }
            else
            {
                ds.Merge(_dtSap);
                return ds.Tables[0];
            }
            //}

            //return dt;
        }
        //else
        //{
        //  return (InvaildAuthontication());
        //}
    }



    [WebMethod]
    [SoapHeader("consumer", Required = true)]
    public DataTable Z_BAPI_DSS_ISU_CA_DISPLAY(string strCANumber)
    {
        //NewClassFile newClassFile = new NewClassFile();
        //DataSet ds = new DataSet("BAPI_RESULT");
        //if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        //{
        //    if (strCANumber.Length == 11)
        //    {
        //        strCANumber = strCANumber.Substring(1, 10);
        //    }
        //    if (strCANumber.Length == 12)
        //    {
        //        strCANumber = strCANumber.Substring(2, 10);
        //    }
        //    if (strCANumber.Length == 9)
        //    {
        //        strCANumber = strCANumber.PadLeft(10, '0');
        //    }

        //    DataTable _dtSap = new DataTable("SAPDATA_ErrorDataTable");
        //    DataColumn dcol;

        //    dcol = new DataColumn();
        //    dcol.DataType = System.Type.GetType("System.String");
        //    dcol.ColumnName = "Message";
        //    _dtSap.Columns.Add(dcol);

        //    dcol = new DataColumn();
        //    dcol.DataType = System.Type.GetType("System.String");
        //    dcol.ColumnName = "Type";
        //    _dtSap.Columns.Add(dcol);

        //    DataRow row = _dtSap.NewRow();
        //    row["Message"] = "CA Number not found";
        //    row["Type"] = string.Empty;


        //    DataTable dtISUSTDTable = new DataTable("ISUSTDTable");
        //    dtISUSTDTable = newClassFile.GetRCMSAP_DataCAWise(strCANumber);
        //    if (dtISUSTDTable.Rows.Count > 0)
        //    {
        //        ds.Merge(dtISUSTDTable);

        //        return ds.Tables[0];
        //    }
        //    else
        //    {
        //        ds.Merge(_dtSap);
        //        return ds.Tables[0];
        //    }
        //}
        //else
        //{
        //    return (InvaildAuthontication());
        //}

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (strCANumber.Length < 12 && strCANumber.Length > 0)
                strCANumber = strCANumber.PadLeft(12, '0');
            dt = isu.Z_BAPI_DSS_ISU_CA_DISPLAY(strCANumber, "").Tables[0];

            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {

                NewClassFile newClassFile = new NewClassFile();
                ds = new DataSet("BAPI_RESULT");

                if (strCANumber.Length == 11)
                {
                    strCANumber = strCANumber.Substring(1, 10);
                }
                if (strCANumber.Length == 12)
                {
                    strCANumber = strCANumber.Substring(2, 10);
                }
                if (strCANumber.Length == 9)
                {
                    strCANumber = strCANumber.PadLeft(10, '0');
                }

                DataTable _dtSap = new DataTable("SAPDATA_ErrorDataTable");
                DataColumn dcol;

                dcol = new DataColumn();
                dcol.DataType = System.Type.GetType("System.String");
                dcol.ColumnName = "Message";
                _dtSap.Columns.Add(dcol);

                dcol = new DataColumn();
                dcol.DataType = System.Type.GetType("System.String");
                dcol.ColumnName = "Type";
                _dtSap.Columns.Add(dcol);

                DataRow row = _dtSap.NewRow();
                row["Message"] = "CA Number not found";
                row["Type"] = string.Empty;


                DataTable dtISUSTDTable = new DataTable("ISUSTDTable");
                dtISUSTDTable = newClassFile.GetRCMSAP_DataCAWise(strCANumber, "");
                if (dtISUSTDTable.Rows.Count > 0)
                {
                    ds.Merge(dtISUSTDTable);

                    return ds.Tables[0];
                }
                else
                {
                    ds.Merge(_dtSap);
                    return ds.Tables[0];
                }
            }

            //return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable Z_BAPI_DSS_ISU_CA_DISPLAY_SAP(string strCANumber)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (strCANumber.Length < 12 && strCANumber.Length > 0)
                strCANumber = strCANumber.PadLeft(12, '0');
            dt = isu.Z_BAPI_DSS_ISU_CA_DISPLAY(strCANumber, "").Tables[0];

            return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable Z_BAPI_DSS_ISU_CA_DISPLAY_RCM(string strCANumber)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (strCANumber.Length < 12 && strCANumber.Length > 0)
                strCANumber = strCANumber.PadLeft(12, '0');
            dt = isu.Z_BAPI_DSS_ISU_CA_DISPLAY(strCANumber, "").Tables[0];

            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {

                NewClassFile newClassFile = new NewClassFile();
                ds = new DataSet("BAPI_RESULT");

                if (strCANumber.Length == 11)
                {
                    strCANumber = strCANumber.Substring(1, 10);
                }
                if (strCANumber.Length == 12)
                {
                    strCANumber = strCANumber.Substring(2, 10);
                }
                if (strCANumber.Length == 9)
                {
                    strCANumber = strCANumber.PadLeft(10, '0');
                }

                DataTable _dtSap = new DataTable("SAPDATA_ErrorDataTable");
                DataColumn dcol;

                dcol = new DataColumn();
                dcol.DataType = System.Type.GetType("System.String");
                dcol.ColumnName = "Message";
                _dtSap.Columns.Add(dcol);

                dcol = new DataColumn();
                dcol.DataType = System.Type.GetType("System.String");
                dcol.ColumnName = "Type";
                _dtSap.Columns.Add(dcol);

                DataRow row = _dtSap.NewRow();
                row["Message"] = "CA Number not found";
                row["Type"] = string.Empty;


                DataTable dtISUSTDTable = new DataTable("ISUSTDTable");
                dtISUSTDTable = newClassFile.GetRCMSAP_DataCAWise(strCANumber, "");
                if (dtISUSTDTable.Rows.Count > 0)
                {
                    ds.Merge(dtISUSTDTable);

                    return ds.Tables[0];
                }
                else
                {
                    ds.Merge(_dtSap);
                    return ds.Tables[0];
                }
            }

            //return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_CloseMeterComplaint(string strComplaintNo, string strDuesPending, string strGroupMeter, string strMeterStatus, string strActionRemarks,
        string strKNumber, string strContactNumber, string strByPassDt, string strPaperSealNo, string strTerminalSealNo, string strPlasticSealNo, string strMeterNo, string strMeterReading,
        string strPersonAttended, string strPoleNo, string strClosingDt, string strActualClosingDt)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //AllInsert.InsertMeterRelatedDetails(-- Insert Here into table -- CC_MTR_BURNT_FROM_ONM

            DataTable dt = new DataTable();
            dt = ONM_BurnMeterInsertSAP(strComplaintNo, strMeterNo, strKNumber, "TOTAB", strGroupMeter, strMeterStatus);
            dt.TableName = "onmDashboardReport";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet ZBAPI_ONLINE_BILL_PDF_V2(string strCANumber, string strEBSKNO) //rhn
    {
        AllInsert allInsert = new AllInsert();
        if (strCANumber == null)
            strCANumber = "";
        if (strEBSKNO == null)
            strEBSKNO = "";

        allInsert.Insert_ServicesLog("ZBAPI_ONLINE_BILL_PDF", strCANumber, "", strEBSKNO, "", "WHATSAPP", "N");

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            // DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();//Commented by Babalu Kumar on 18072021
            //  DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();
            DelhiV2.WebService isu = new DelhiV2.WebService();//Added by Babalu Kumar on 18072021
            DataSet ds = new DataSet();
            ds = isu.ZBAPI_ONLINE_BILL_PDF(strCANumber, strEBSKNO);

            allInsert.Insert_ServicesLog("ZBAPI_ONLINE_BILL_PDF", strCANumber, "", strEBSKNO, "", "WHATSAPP", "Y");
            return ds;
        }
        else
        {
            return (InvaildAuthonticationds());
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet ZBAPI_ONLINE_BILL_PDF(string strCANumber, string strEBSKNO) //rhn
    {
        AllInsert allInsert = new AllInsert();
        if (strCANumber == null)
            strCANumber = "";
        if (strEBSKNO == null)
            strEBSKNO = "";

        allInsert.Insert_ServicesLog("ZBAPI_ONLINE_BILL_PDF", strCANumber, "", strEBSKNO, "", "WHATSAPP", "N");

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();//Commented by Babalu Kumar on 18072021
            DelhiV2.WebService isu = new DelhiV2.WebService();//Added by Babalu Kumar on 18072021
            DataSet ds = new DataSet();
            ds = isu.ZBAPI_ONLINE_BILL_PDF(strCANumber, strEBSKNO);

            allInsert.Insert_ServicesLog("ZBAPI_ONLINE_BILL_PDF", strCANumber, "", strEBSKNO, "", "WHATSAPP", "Y");
            return ds;
        }
        else
        {
            return (InvaildAuthonticationds());
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet ZBAPI_ONLINE_BILL_PDF_MS(string strCANumber, string strEBSKNO)
    {
        AllInsert allInsert = new AllInsert();
        if (strCANumber == null)
            strCANumber = "";
        if (strEBSKNO == null)
            strEBSKNO = "";

        allInsert.Insert_ServicesLog("ZBAPI_ONLINE_BILL_PDF", strCANumber, "", strEBSKNO, "", "MOBAPP", "N");

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();//Commented by Babalu Kumar on 18072021
            DelhiV2.WebService isu = new DelhiV2.WebService();//Added by Babalu Kumar on 18072021
            DataSet ds = new DataSet();
            ds = isu.ZBAPI_ONLINE_BILL_PDF(strCANumber, strEBSKNO);

            allInsert.Insert_ServicesLog("ZBAPI_ONLINE_BILL_PDF", strCANumber, "", strEBSKNO, "", "MOBAPP", "Y");
            return ds;
        }
        else
        {
            return (InvaildAuthonticationds());
        }
    }

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataSet ZBAPI_ONLINE_BILL_PDF_MS(string strCANumber, string strEBSKNO)
    //{
    //    AllInsert allInsert = new AllInsert();
    //    if (strCANumber == null)
    //        strCANumber = "";
    //    if (strEBSKNO == null)
    //        strEBSKNO = "";

    //    allInsert.Insert_ServicesLog("ZBAPI_ONLINE_BILL_PDF", strCANumber, "", strEBSKNO, "", "MOBAPP", "N");

    //    if (checkConsumer(MethodBase.GetCurrentMethod().Name))
    //    {
    //        DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
    //        //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

    //        DataSet ds = new DataSet();
    //        ds = isu.ZBAPI_ONLINE_BILL_PDF(strCANumber, strEBSKNO);
    //        //DataTable dt = ds.Tables[0];
    //        //string str = "";

    //        DataSet dsNew = new DataSet("BAPI_RESULT");

    //        //outputFlagsTable
    //        DataTable dtNew = new DataTable();
    //        dtNew.TableName = "ZPDFTable";
    //        dtNew.Columns.Add("Tdline");

    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            DataRow row = dtNew.NewRow();
    //            row["Tdline"] = ds.Tables[0].Rows[i][0].ToString().Trim();
    //            dtNew.Rows.Add(row);
    //        }

    //        //using (StreamWriter sw = new StreamWriter("D:\\PDF\\" + DateTime.Now.ToString("yyyyMMdd") + "_" + strCANumber + ".pdf"))
    //        //{
    //        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        //    {
    //        //        //str += ds.Tables[0].Rows[i][0].ToString().Trim() + Environment.NewLine;
    //        //        sw.WriteLine(ds.Tables[0].Rows[i][0].ToString().Trim());
    //        //        DataRow row = dtNew.NewRow();
    //        //        row["Tdline"] = ds.Tables[0].Rows[i][0].ToString().Trim();
    //        //        dtNew.Rows.Add(row);
    //        //    }
    //        //    sw.Close();
    //        //}

    //        allInsert.Insert_ServicesLog("ZBAPI_ONLINE_BILL_PDF", strCANumber, "", strEBSKNO, "", "MOBAPP", "Y");
    //        dsNew.Tables.Add(dtNew);
    //        return dsNew;
    //    }
    //    else
    //    {
    //        return (InvaildAuthonticationds());
    //    }
    //}


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet ZBAPI_ONLINE_BILL_PDF_WA(string strCANumber, string strEBSKNO, string _sSource)
    {
        AllInsert allInsert = new AllInsert();
        if (strCANumber == null)
            strCANumber = "";
        if (strEBSKNO == null)
            strEBSKNO = "";

        if (_sSource == null)
            _sSource = "WHATSAPP";
        else if (_sSource.ToString().Trim() == "")
            _sSource = "WHATSAPP";

        allInsert.Insert_ServicesLog("ZBAPI_ONLINE_BILL_PDF", strCANumber, "", strEBSKNO, "", _sSource, "N");

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();//Commented by Babalu Kumar on 18072021
            DelhiV2.WebService isu = new DelhiV2.WebService();//Added by Babalu Kumar on 18072021
            DataSet ds = new DataSet();
            ds = isu.ZBAPI_ONLINE_BILL_PDF(strCANumber, strEBSKNO);

            allInsert.Insert_ServicesLog("ZBAPI_ONLINE_BILL_PDF", strCANumber, "", strEBSKNO, "", _sSource, "Y");
            return ds;
        }
        else
        {
            return (InvaildAuthonticationds());
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet ZBAPI_DEMAND_NOTE_ONLINE(string strOrdNumber)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();

            DataSet ds = new DataSet();
            //ds = isu.ZBAPI_FICA_DEMAND_NOTE(strOrdNumber);
            ds = isu.ZBAPI_DEMAND_NOTE_ONLINE(strOrdNumber);
            return ds;
        }
        else
        {
            return (InvaildAuthonticationds());
        }
    }


    //ritesh
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet GetAssignedComplaintsToTeam(string imei)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            BYPLOMSONLINE_Services_TestD.Service1 isuy = new BYPLOMSONLINE_Services_TestD.Service1();
            DataSet ds = new DataSet();
            ds = isuy.GetAssignedComplaintsToTeam("@CPbsESyaMUna", imei);
            return ds;
        }
        else
        {
            return (InvaildAuthonticationds());
        }
    }

    //ritesh
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet GetClosingRemarksFromCategory(string category)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            BYPLOMSONLINE_Services_TestD.Service1 isuy = new BYPLOMSONLINE_Services_TestD.Service1();
            DataSet ds = new DataSet();
            ds = isuy.GetClosingRemarksFromCategory("@CPbsESyaMUna", category);
            return ds;
        }
        else
        {
            return (InvaildAuthonticationds());
        }
    }

    //ritesh
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet CloseComplaint(string ComplaintNo, string CA, string FaultCategory, string FaultType, string ClosingRemark, string OtherRemarks, string IMEI)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            BYPLOMSONLINE_Services_TestD.Service1 isuy = new BYPLOMSONLINE_Services_TestD.Service1();
            DataSet ds = new DataSet();

            byte[] arr = { };

            ds = isuy.CloseComplaint("@CPbsESyaMUna", ComplaintNo, CA, FaultCategory, FaultType, ClosingRemark, OtherRemarks, IMEI, "", "", arr);
            return ds;
        }
        else
        {
            return (InvaildAuthonticationds());
        }
    }

    //Added Existing Webmethod -- Rajveer --> 05-08-2016
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet Z_BAPI_ZDSS_WEB_LINK(string I_ILART, string I_VKONT, string I_VKONA, string PARTNERCATEGORY,
        string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME,
        string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2,
        string STR_SUPPL3, string POSTL_COD1, string CITY, string E_MAIL, string LANDLINE, string MOBILE,
        string FEMALE, string MALE, string JOBGR, string IDTYPE, string IDNUMBER, string PLANNINGPLANT,
        string WORKCENTRE, string SYSTEMCOND, string APPLIEDCAT, string APPLIEDLOAD, string APPLIEDLOADKVA,
        string CONNECTIONTYPE, string STATEMENT_CA, string START_DATE, string START_TIME, string FINISH_DATE,
        string FINISH_TIME, string SORTFIELD, string ABKRS, string AppVersion)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            if (ABKRS.Contains("/"))
            {

                //for BRPL
                NewClassFile nfs = new NewClassFile();
                //if (PLANNINGPLANT == "D021") {   //commented by Babalu kumar on date 15092021

                //    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for BRPL");
                //    return StopDSSNewRequest();
                //}

                if (PLANNINGPLANT == "D031")
                {

                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for BYPL");
                    return StopDSSNewRequest();
                }

                //Stop for other also --- later

                if (START_TIME == "08:00:00")
                {
                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for 08:00:00");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }

                if (START_TIME == "10:00:00")
                {
                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for 10:00:00");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }

                if (START_TIME == "17:00:00")
                {
                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for 17:00:00");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }

                if (START_TIME == "13:00:00")
                {
                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for 13:00:00");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }

                if (START_TIME == "15:00:00")
                {
                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for 15:00:00");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }

                if (START_TIME == "12:00:00")
                {
                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for 12:00:00");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }



                //Check for Holiday Date
                if (START_DATE != "")
                {
                    if (new NewClassFile().validateHolidayDt(START_DATE))
                    {
                        nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for date : " + START_DATE);
                        return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                    }
                }

                #region Working code for generation of new connection and change particulars - changed by RV (26-May-20 )

                string[] sABKRS = ABKRS.Split('/');
                string strABKRS = string.Empty;
                strABKRS = sABKRS[0];

                DELHIWSTESTDV2.WebService WSisu = new DELHIWSTESTDV2.WebService();
                DelhiV2.WebService isu = new DelhiV2.WebService();

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                //Exceed limit for a mobile number
                ds = isu.ZBAPI_CS_MOBILE_APPCNT(MOBILE, "", "30", "");
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    int count = Convert.ToInt32(dt.Rows[0]["COUNT"].ToString());
                    if (count < 5)
                    {
                        //ILART -- For verifying BRPL / BYPL - change particulars
                        if (I_ILART != "U01")
                        {
                            string CaNumber = I_VKONT;

                            if (CaNumber.Length < 12)
                                CaNumber = CaNumber.PadLeft(12, '0');

                            string Reg_Str_Group = "", _company = "";

                            ds = new DataSet();
                            ds = WSisu.Z_BAPI_DSS_ISU_CA_DISPLAY(CaNumber, "");

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Reg_Str_Group = ds.Tables[0].Rows[0]["Reg_Str_Group"].ToString();

                                _company = Reg_Str_Group.Substring(0, 2);

                                if (_company == "W1" || _company == "W2" || _company == "S1" || _company == "S2")
                                {
                                    _company = "BRPL";
                                }
                                else
                                {
                                    _company = "BYPL";
                                }
                            }

                            if (_company == "BRPL")
                            {
                                nfs.WriteIntoFile("BlockingAppointments - Change Particulars" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for BRPL -- " + CaNumber);
                                return StopDSSNewRequest();
                            }
                            else
                            {
                                //ds = new DataSet();
                                //ds = isu.Z_BAPI_ZDSS_WEB_LINK(I_ILART, I_VKONT, I_VKONA, PARTNERCATEGORY, PARTNERTYPE, TITLE_KEY, FIRSTNAME, LASTNAME, MIDDLENAME, FATHERSNAME, HOUSE_NO, BUILDING, STR_SUPPL1, STR_SUPPL2, STR_SUPPL3, POSTL_COD1, CITY, E_MAIL, LANDLINE, MOBILE, FEMALE, MALE, JOBGR, IDTYPE, IDNUMBER, PLANNINGPLANT, WORKCENTRE, SYSTEMCOND, APPLIEDCAT, APPLIEDLOAD, APPLIEDLOADKVA, CONNECTIONTYPE, STATEMENT_CA, START_DATE, START_TIME, FINISH_DATE, FINISH_TIME, SORTFIELD, strABKRS);
                                //return ds;
                                nfs.WriteIntoFile("BlockingAppointments - Change Particulars" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for BYPL -- " + CaNumber);
                                return StopDSSNewRequest();
                            }
                        }
                        else //--New Connection
                        {
                            ds = new DataSet();
                            ds = isu.Z_BAPI_ZDSS_WEB_LINK(I_ILART, I_VKONT, I_VKONA, PARTNERCATEGORY, PARTNERTYPE, TITLE_KEY, FIRSTNAME, LASTNAME, MIDDLENAME, FATHERSNAME, HOUSE_NO, BUILDING,
                                                            STR_SUPPL1, STR_SUPPL2, STR_SUPPL3, POSTL_COD1, CITY, E_MAIL, LANDLINE, MOBILE, FEMALE, MALE, JOBGR, IDTYPE, IDNUMBER, PLANNINGPLANT,
                                                            WORKCENTRE, SYSTEMCOND, APPLIEDCAT, APPLIEDLOAD, APPLIEDLOADKVA, CONNECTIONTYPE, STATEMENT_CA, START_DATE, START_TIME, FINISH_DATE,
                                                            FINISH_TIME, SORTFIELD, strABKRS, "", "", "", WORKCENTRE);
                            return ds;
                            //return StopDSSNewRequest();
                        }
                    }
                    else
                    {
                        //Appointment exceeds with Mobile Number
                        nfs.WriteIntoFile("Appointment exceeds " + "Z_BAPI_ZDSS_WEB_LINK cancelled request for mobile number " + MOBILE);
                        return StopDSSNewRequestBYPL("Dear customer, numbers of request registration against a mobile number have crossed, kindly contact our customer care no. 19122.");
                    }
                }
                else
                {
                    //Error - Checking appointment exceeds with Mobile Number
                    nfs.WriteIntoFile("Error for checking appointment " + "Z_BAPI_ZDSS_WEB_LINK");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }
                #endregion

                //05-Nov-2020
                //return StopDSSNewRequest();
            }
            else
            {
                if (I_ILART.ToString() == "U01")
                {
                    return (InvaildAppCodeU01());
                }
                return (InvaildAppCode());
            }
        }
        else
        {
            return (InvaildAuthonticationds());
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet Z_BAPI_ZDSS_WEB_LINK_OLD(string I_ILART, string I_VKONT, string I_VKONA, string PARTNERCATEGORY,
        string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME,
        string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2,
        string STR_SUPPL3, string POSTL_COD1, string CITY, string E_MAIL, string LANDLINE, string MOBILE,
        string FEMALE, string MALE, string JOBGR, string IDTYPE, string IDNUMBER, string PLANNINGPLANT,
        string WORKCENTRE, string SYSTEMCOND, string APPLIEDCAT, string APPLIEDLOAD, string APPLIEDLOADKVA,
        string CONNECTIONTYPE, string STATEMENT_CA, string START_DATE, string START_TIME, string FINISH_DATE,
        string FINISH_TIME, string SORTFIELD, string ABKRS, string AppVersion)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            if (ABKRS.Contains("/"))
            {

                //for BRPL
                NewClassFile nfs = new NewClassFile();
                //if (PLANNINGPLANT == "D021") {   //commented by Babalu kumar on date 15092021

                //    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for BRPL");
                //    return StopDSSNewRequest();
                //}

                if (PLANNINGPLANT == "D031")
                {

                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for BYPL");
                    return StopDSSNewRequest();
                }

                //Stop for other also --- later

                if (START_TIME == "08:00:00")
                {
                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for 08:00:00");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }

                if (START_TIME == "10:00:00")
                {
                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for 10:00:00");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }

                if (START_TIME == "17:00:00")
                {
                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for 17:00:00");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }

                if (START_TIME == "13:00:00")
                {
                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for 13:00:00");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }

                if (START_TIME == "15:00:00")
                {
                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for 15:00:00");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }

                if (START_TIME == "12:00:00")
                {
                    nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for 12:00:00");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }



                //Check for Holiday Date
                if (START_DATE != "")
                {
                    if (new NewClassFile().validateHolidayDt(START_DATE))
                    {
                        nfs.WriteIntoFile("BlockingAppointments" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for date : " + START_DATE);
                        return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                    }
                }

                #region Working code for generation of new connection and change particulars - changed by RV (26-May-20 )
                string[] sABKRS = ABKRS.Split('/');
                string strABKRS = string.Empty;
                strABKRS = sABKRS[0];

                DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                //Exceed limit for a mobile number
                ds = isu.ZBAPI_CS_MOBILE_APPCNT(MOBILE, "", "30", "");
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    int count = Convert.ToInt32(dt.Rows[0]["COUNT"].ToString());
                    if (count < 5)
                    {
                        //ILART -- For verifying BRPL / BYPL - change particulars
                        if (I_ILART != "U01")
                        {
                            string CaNumber = I_VKONT;

                            if (CaNumber.Length < 12)
                                CaNumber = CaNumber.PadLeft(12, '0');

                            string Reg_Str_Group = "", _company = "";

                            ds = new DataSet();
                            ds = isu.Z_BAPI_DSS_ISU_CA_DISPLAY(CaNumber, "");

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Reg_Str_Group = ds.Tables[0].Rows[0]["Reg_Str_Group"].ToString();

                                _company = Reg_Str_Group.Substring(0, 2);

                                if (_company == "W1" || _company == "W2" || _company == "S1" || _company == "S2")
                                {
                                    _company = "BRPL";
                                }
                                else
                                {
                                    _company = "BYPL";
                                }
                            }

                            if (_company == "BRPL")
                            {
                                nfs.WriteIntoFile("BlockingAppointments - Change Particulars" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for BRPL -- " + CaNumber);
                                return StopDSSNewRequest();
                            }
                            else
                            {
                                //ds = new DataSet();
                                //ds = isu.Z_BAPI_ZDSS_WEB_LINK(I_ILART, I_VKONT, I_VKONA, PARTNERCATEGORY, PARTNERTYPE, TITLE_KEY, FIRSTNAME, LASTNAME, MIDDLENAME, FATHERSNAME, HOUSE_NO, BUILDING, STR_SUPPL1, STR_SUPPL2, STR_SUPPL3, POSTL_COD1, CITY, E_MAIL, LANDLINE, MOBILE, FEMALE, MALE, JOBGR, IDTYPE, IDNUMBER, PLANNINGPLANT, WORKCENTRE, SYSTEMCOND, APPLIEDCAT, APPLIEDLOAD, APPLIEDLOADKVA, CONNECTIONTYPE, STATEMENT_CA, START_DATE, START_TIME, FINISH_DATE, FINISH_TIME, SORTFIELD, strABKRS);
                                //return ds;
                                nfs.WriteIntoFile("BlockingAppointments - Change Particulars" + "Z_BAPI_ZDSS_WEB_LINK cancelled request for BYPL -- " + CaNumber);
                                return StopDSSNewRequest();
                            }
                        }
                        else //--New Connection
                        {
                            ds = new DataSet();
                            ds = isu.Z_BAPI_ZDSS_WEB_LINK(I_ILART, I_VKONT, I_VKONA, PARTNERCATEGORY, PARTNERTYPE, TITLE_KEY, FIRSTNAME, LASTNAME, MIDDLENAME, FATHERSNAME, HOUSE_NO, BUILDING, STR_SUPPL1, STR_SUPPL2, STR_SUPPL3, POSTL_COD1, CITY, E_MAIL, LANDLINE, MOBILE, FEMALE, MALE, JOBGR, IDTYPE, IDNUMBER, PLANNINGPLANT, WORKCENTRE, SYSTEMCOND, APPLIEDCAT, APPLIEDLOAD, APPLIEDLOADKVA, CONNECTIONTYPE, STATEMENT_CA, START_DATE, START_TIME, FINISH_DATE, FINISH_TIME, SORTFIELD, strABKRS);
                            return ds;
                            //return StopDSSNewRequest();
                        }
                    }
                    else
                    {
                        //Appointment exceeds with Mobile Number
                        nfs.WriteIntoFile("Appointment exceeds " + "Z_BAPI_ZDSS_WEB_LINK cancelled request for mobile number " + MOBILE);
                        return StopDSSNewRequestBYPL("Dear customer, numbers of request registration against a mobile number have crossed, kindly contact our customer care no. 19122.");
                    }
                }
                else
                {
                    //Error - Checking appointment exceeds with Mobile Number
                    nfs.WriteIntoFile("Error for checking appointment " + "Z_BAPI_ZDSS_WEB_LINK");
                    return StopDSSNewRequestBYPL("For appointment related queries, please contact our customer care no. 19122.");
                }
                #endregion

                //05-Nov-2020
                //return StopDSSNewRequest();
            }
            else
            {
                if (I_ILART.ToString() == "U01")
                {
                    return (InvaildAppCodeU01());
                }
                return (InvaildAppCode());
            }
        }
        else
        {
            return (InvaildAuthonticationds());
        }
    }

    private DataSet StopDSSNewRequest()
    {
        DataSet ds = new DataSet("BAPI_RESULT");

        //outputFlagsTable
        DataTable dtOutputFlag = new DataTable();
        dtOutputFlag.TableName = "outputFlagsTable";
        dtOutputFlag.Columns.Add("E_Flag_Ap");
        dtOutputFlag.Columns.Add("E_Flag_Bp");
        dtOutputFlag.Columns.Add("E_Flag_So");
        dtOutputFlag.Columns.Add("E_Flag_Us");
        dtOutputFlag.Columns.Add("E_New_Partner");
        dtOutputFlag.Columns.Add("E_Service_Order");
        DataRow row = dtOutputFlag.NewRow();
        row["E_Flag_Ap"] = string.Empty;
        row["E_Flag_Bp"] = string.Empty;
        row["E_Flag_So"] = string.Empty;
        row["E_Flag_Us"] = string.Empty;
        row["E_New_Partner"] = string.Empty;
        row["E_Service_Order"] = string.Empty;
        dtOutputFlag.Rows.Add(row);

        //SAPDATA_ErrorDataTable
        DataTable dterror = new DataTable();
        dterror.TableName = "SAPDATA_ErrorDataTable";
        dterror.Columns.Add("Type");
        dterror.Columns.Add("Id");
        dterror.Columns.Add("Number");
        dterror.Columns.Add("Message");
        dterror.Columns.Add("Log_No");
        dterror.Columns.Add("Log_Msg_No");
        dterror.Columns.Add("Message_V1");
        dterror.Columns.Add("Message_V2");
        dterror.Columns.Add("Message_V3");
        dterror.Columns.Add("Message_V4");
        dterror.Columns.Add("Parameter");
        dterror.Columns.Add("Row");
        dterror.Columns.Add("Field");
        dterror.Columns.Add("System");

        DataRow rowError = dterror.NewRow();
        rowError["Type"] = "E";
        rowError["Id"] = string.Empty;
        rowError["Number"] = "000";
        rowError["Message"] = "For appointment related queries, please contact our customer care no. 19122 / 19123 .";
        rowError["Log_No"] = string.Empty;
        rowError["Log_Msg_No"] = "000000";
        rowError["Message_V1"] = string.Empty;
        rowError["Message_V2"] = string.Empty;
        rowError["Message_V3"] = string.Empty;
        rowError["Message_V4"] = string.Empty;
        rowError["Parameter"] = string.Empty;
        rowError["Row"] = "0";
        rowError["Field"] = string.Empty;
        rowError["System"] = string.Empty;
        dterror.Rows.Add(rowError);

        ds.Tables.Add(dtOutputFlag);
        ds.Tables.Add(dterror);

        return ds;
    }

    private DataSet StopDSSNewRequestBYPL(string strMsg)
    {
        DataSet ds = new DataSet("BAPI_RESULT");

        //outputFlagsTable
        DataTable dtOutputFlag = new DataTable();
        dtOutputFlag.TableName = "outputFlagsTable";
        dtOutputFlag.Columns.Add("E_Flag_Ap");
        dtOutputFlag.Columns.Add("E_Flag_Bp");
        dtOutputFlag.Columns.Add("E_Flag_So");
        dtOutputFlag.Columns.Add("E_Flag_Us");
        dtOutputFlag.Columns.Add("E_New_Partner");
        dtOutputFlag.Columns.Add("E_Service_Order");
        DataRow row = dtOutputFlag.NewRow();
        row["E_Flag_Ap"] = string.Empty;
        row["E_Flag_Bp"] = string.Empty;
        row["E_Flag_So"] = string.Empty;
        row["E_Flag_Us"] = string.Empty;
        row["E_New_Partner"] = string.Empty;
        row["E_Service_Order"] = string.Empty;
        dtOutputFlag.Rows.Add(row);

        //SAPDATA_ErrorDataTable
        DataTable dterror = new DataTable();
        dterror.TableName = "SAPDATA_ErrorDataTable";
        dterror.Columns.Add("Type");
        dterror.Columns.Add("Id");
        dterror.Columns.Add("Number");
        dterror.Columns.Add("Message");
        dterror.Columns.Add("Log_No");
        dterror.Columns.Add("Log_Msg_No");
        dterror.Columns.Add("Message_V1");
        dterror.Columns.Add("Message_V2");
        dterror.Columns.Add("Message_V3");
        dterror.Columns.Add("Message_V4");
        dterror.Columns.Add("Parameter");
        dterror.Columns.Add("Row");
        dterror.Columns.Add("Field");
        dterror.Columns.Add("System");

        DataRow rowError = dterror.NewRow();
        rowError["Type"] = "E";
        rowError["Id"] = string.Empty;
        rowError["Number"] = "000";
        rowError["Message"] = strMsg;
        rowError["Log_No"] = string.Empty;
        rowError["Log_Msg_No"] = "000000";
        rowError["Message_V1"] = string.Empty;
        rowError["Message_V2"] = string.Empty;
        rowError["Message_V3"] = string.Empty;
        rowError["Message_V4"] = string.Empty;
        rowError["Parameter"] = string.Empty;
        rowError["Row"] = "0";
        rowError["Field"] = string.Empty;
        rowError["System"] = string.Empty;
        dterror.Rows.Add(rowError);

        ds.Tables.Add(dtOutputFlag);
        ds.Tables.Add(dterror);

        return ds;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string NewConnOTPRqstFrm(string strDiscom, string strFirstName, string strLastName, string strEmailId, string strMobileNo)
    {
        NewClassFile newClassFile = new NewClassFile();
        if (strDiscom == "BYPL")
        {
            newClassFile.WriteIntoFile("BlockingAppointments OTP Request for New Connection " + "NewConnOTPRqstFrm cancelled request for BYPL");
            return "";
        }
        if (strDiscom == "D031")
        {
            newClassFile.WriteIntoFile("BlockingAppointments OTP Request for New Connection " + "NewConnOTPRqstFrm cancelled request for BYPL");
            return "";
        }
        return (newClassFile.newConnOTPRqstFrmInsert(strDiscom, strFirstName, strLastName, strEmailId, strMobileNo));
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NewConnOTPVerifyFrm(string strOTP, string strLblId) // Update when sending wrong OTP no.
    {
        NewClassFile newClassFile = new NewClassFile();
        return (newClassFile.newConnOTPVerifyFrmRqst(strOTP, strLblId));
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NewConnResendOTPVerifyFrm(string strLblId)
    {
        NewClassFile newClassFile = new NewClassFile();
        return (newClassFile.newConnResendOTPVerifyFrmRqst(strLblId));
    }

    [WebMethod] //Updated New
    [SoapHeader("consumer", Required = true)]
    public bool App_log(string strIMEI, string strActionType, string strActionPerform) // App Log method is not included with security add new param key to verify method
    {
        NewClassFile newClassFile = new NewClassFile();
        return (newClassFile.App_log(strIMEI, strActionType, strActionPerform));
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable getPendingOrderOracle(string imeiNo)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            NewClassFile newClassFile = new NewClassFile();

            dt = newClassFile.getPendingOrder(imeiNo);
            dt.TableName = "orderTable";
            return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable getCompleteOrderOracle(string imeiNo)//27032019 Babalu Kumar
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            NewClassFile newClassFile = new NewClassFile();

            dt = newClassFile.getCompleteOrder(imeiNo);
            dt.TableName = "orderTable";
            return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable getPendingOrderOracleKCC(string imeiNo)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            NewClassFile newClassFile = new NewClassFile();

            dt = newClassFile.getPendingOrderKCC(imeiNo);
            dt.TableName = "orderTable";
            return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ChkUsrForClosingComplnt(string strEmpNo, string strPass) // Linemen Details
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            NewClassFile newClassFile = new NewClassFile();

            dt = newClassFile.onmChkValidUser(strEmpNo, strPass);
            dt.TableName = "onmChkValidUser";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool updateOrderStatus(string orderNo, string status)
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();

            if (status.Contains("/"))
            {
                string[] strStatus = status.Split('/');
                string strABKRS = string.Empty;
                strABKRS = strStatus[0];

                bool isTrue = newClassFile.updateOrderStatus(orderNo, strABKRS);

                JavaWebReference.ISUSERVICE javaRef = new JavaWebReference.ISUSERVICE();
                string strOutput = javaRef.BAPI_ISUSMORDER_USERSTATUSSET(orderNo, "SOCR", "");

                //if (strOutput == "SUCCESS")
                //{
                return isTrue;
                //}

            }

            return newClassFile.updateOrderStatus(orderNo, status);
        }
        else
        {
            return false;
        }

    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable getIVRSCallID()                       //Added By AJay 23/12/15
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getIVRSCallIDDL();
            dt.TableName = "IVRSCallID";
            return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable getCFOracle(string orderNo)
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getCF(orderNo);
            dt.TableName = "cfTable";
            return dt;
        }
        else
            return (InvaildAuthontication());

    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ZBAPI_CS_ORD_STAT(string strOrder)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (strOrder.Length < 12)
                strOrder = strOrder.PadLeft(12, '0');

            dt = isu.ZBAPI_CS_ORD_STAT(strOrder).Tables[0];

            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ZBAPI_DISPLAY_BILL_WEB(string strCANumber, string strBillMonth)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (strCANumber.Length < 12)
                strCANumber = strCANumber.PadLeft(12, '0');

            dt = isu.ZBAPI_DISPLAY_BILL_WEB(strCANumber, strBillMonth).Tables[0];
            return dt;
        }
        else
            return (InvaildAuthontication());
    }
    //Commented dated 310032023
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ZBAPI_DISPLAY_BILL_WEB_OPOWER(string strCANumber, string strBillMonth)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (strCANumber.Length < 12)
                strCANumber = strCANumber.PadLeft(12, '0');

            dt = isu.ZBAPI_DISPLAY_BILL_WEB(strCANumber, strBillMonth).Tables[0];

            DataTable dtOPower = new DataTable();
            dtOPower = NewClassFile.GetOPower_Data(strCANumber.Substring(3, 9));

            DataColumn dcolColumn = new DataColumn("OP", typeof(string));
            dt.Columns.Add(dcolColumn);
            DataRow drowItem;

            if (dtOPower.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Columns)
                {
                    drowItem = dt.NewRow();
                    drowItem["OP"] = "Y";
                    dt.Rows.Add(drowItem);
                }
            }
            else
            {
                foreach (DataRow row in dt.Columns)
                {
                    drowItem = dt.NewRow();
                    drowItem["OP"] = "N";
                    dt.Rows.Add(drowItem);
                }
            }


            return dt;
        }
        else
            return (InvaildAuthontication());
    }
    //Commented dated 310032023

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ZBAPI_DISPLAY_BILL_WEB_VALID(string strCANumber)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (strCANumber.Length < 12)
                strCANumber = strCANumber.PadLeft(12, '0');

            dt = isu.Z_BAPI_CMS_ISU_CA_DISPLAY(strCANumber, "", "", "", "", "").Tables[0];
            return dt;
        }
        else
            return (InvaildAuthontication());
    }
    //31102014
    [WebMethod(EnableSession = true, Description = "All parameter are optional.<br>Parameter for Circle:- (CENTRAL/EAST/SOUTH/WEST) and for Company:- (BYPL/BRPL) ")]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetTransformerSetupDetail_delhi(string Circle, string Company, string district)
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.GetTransformerSetupDetail(Circle.ToUpper().Trim(), Company.ToUpper().Trim(), district.ToUpper().Trim());
            dt.TableName = "onm_transformer_setup_detail";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable FIVE_COMPL(string _CA_NO)
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.FIVE_COMPL(_CA_NO.ToString().Trim());
            dt.TableName = "FIVE_COMPL";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool FEEDBACK(string name, string number, string ip_addr, string imei_no, string email, string feedback)
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return newClassFile.FEEDBACK(name, number, ip_addr, imei_no, email, feedback);
        }
        else
            return (false);
    }

    #region feedback form 03-Aug-2016 @ Rajveer

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool FEEDBACK_MOBAPP(string name, string number, string email, string answerOne, string answerTwo, string answerThree, string answerFour, string answerFive, string answerSix)
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return newClassFile.FEEDBACK_MobApp(name, number, email, answerOne, answerTwo, answerThree, answerFour, answerFive, answerSix);
        }
        else
            return (false);
    }

    #endregion

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable Area_Code(string _strDiv)
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.Area_Code(_strDiv.ToString().Trim().Substring(2, 3));
            dt.TableName = "Area_Code";
            return dt;
        }
        else
            return (InvaildAuthontication());

    }

    [WebMethod] //Updated New
    [SoapHeader("consumer", Required = true)]
    public DataTable GetARDAppVersion(string _sAppID)
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.GetARDAppVersion(_sAppID);
            dt.TableName = "Version";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NewRegistration(string _sIMEI_No, string _sUserName, string _sManager_EmpCode, string _sActive_Flag
        , string _sEmpCode, string _sDesignation, string _sMobileNo, string _sEmailID) //10072014 -new
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return (newClassFile.NewRegistration(_sIMEI_No, _sUserName, _sManager_EmpCode, _sActive_Flag, _sEmpCode, _sDesignation
            , _sMobileNo, _sEmailID));
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string NCC_Registration(string _sCANo, string cboPriority, string lstFaultCatg, string txtCustRemarks, string cboMinutes, string cboDays, string AreaCode)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            string chkComplClear = string.Empty;
            string dtpCompClearDate = string.Empty;
            string cboFixtue = string.Empty;
            string txtStreetNo = string.Empty;
            string txtCustPhone = string.Empty;
            string txtCustMob = string.Empty;
            string txtCustKNo = string.Empty;
            string lblAreaCode = string.Empty;
            string txtCustArea = string.Empty;
            string txtCustDistrict = string.Empty;
            string cboCallCateg = string.Empty;
            string txtCustName = string.Empty;
            string txtCustAdd1 = string.Empty;
            string txtCustAdd2 = string.Empty;
            string lblFCode = string.Empty;
            //string txtCustRemarks = string.Empty;
            string txtClearedBy = string.Empty;
            string lblTimeTaken = "0";
            string txtConsumerReferenceNo = string.Empty;
            string txtlandmark = string.Empty;
            string txtContractNo = string.Empty;
            // string cboMinutes = string.Empty;
            string txtAddlNo = string.Empty;
            // string cboDays = string.Empty;
            string optSmsYes = string.Empty;
            string optSmsNo = string.Empty;

            string _sConsumerRefNo = string.Empty;
            string gAreadiv = string.Empty;
            string txtEBSEMail = string.Empty;
            string StrConsumerType = string.Empty;
            string strDIST = string.Empty;
            string strGridCode = string.Empty;
            string strGridName = string.Empty;
            string strGridPhone = string.Empty;
            string strFeederCode = string.Empty;
            string strFeederName = string.Empty;
            string strTRCOde = string.Empty;
            string strCompCenter = string.Empty;
            string strCompCenterPhone = string.Empty;
            string strCircle = string.Empty;
            string strCompCenterForSapSerch = string.Empty;
            string UserName = "ANDRIOD".Trim();
            string strCLEAR_STATUS = "N";
            /// string strCompClearDate = "NULL";
            string VMODULENAME = "ANDRIOD".Trim();
            //  Session["ISONM"] = "ONM";

            //try
            //{

            //    DataTable _dtAlreadyReg = new DataTable();
            //    _dtAlreadyReg = AllSelect.GetShowDetails("SELECT  cc.OPERATIONAL_COMP_NO,cc.kno  FROM ONM_FAULT_COLLECTIONS flt, CC_DVB_COMP cc WHERE flt.COMPLAINT_NO=cc.COMPNO	AND FLT.kno like '%" + _sCANo + "'	AND flt.FAULT_DUR_IN_SEC =-1 ");
            //    if (_dtAlreadyReg.Rows.Count > 0)
            //    {
            //        return ("Your complaint is already registed with complaint No. " + _dtAlreadyReg.Rows[0]["OPERATIONAL_COMP_NO"].ToString());
            //    }

            //    DataTable _dtConDetails = new DataTable();
            //    DataSet _ds = new DataSet();
            //    _dtConDetails = CA_DISPLAY(_sCANo, "", "", "", "", "");

            //    if (_dtConDetails.Rows.Count > 0)
            //    {
            //        txtConsumerReferenceNo = _dtConDetails.Rows[0]["LEGACY_ACCT"].ToString();
            //        // _sCANo = _dtConDetails.Rows[0]["CA_NUMBER"].ToString();
            //        txtCustName = _dtConDetails.Rows[0]["BP_NAME"].ToString();
            //        txtCustAdd1 = _dtConDetails.Rows[0]["HOUSE_NUMBER"].ToString() + " " + _dtConDetails.Rows[0]["STREET2"].ToString();
            //        txtCustAdd2 = _dtConDetails.Rows[0]["STREET3"].ToString();


            //        DataTable _dt = new DataTable();
            //        if (_dtConDetails.Rows[0]["REG_STR_GROUP"].ToString() != "")
            //        {
            //            _dt = AllSelect.GetShowDetails("SELECT DIV_CODE,DIV_NAME FROM DIVISION where upper(SAP_CIRCLE_DIV)='" + _dtConDetails.Rows[0]["REG_STR_GROUP"].ToString() + "'");
            //            if (_dt.Rows.Count > 0)
            //            {
            //                gAreadiv = _dt.Rows[0]["DIV_CODE"].ToString();
            //                txtCustDistrict = _dt.Rows[0]["DIV_NAME"].ToString();
            //            }
            //        }
            //        if (_dtConDetails.Rows[0]["TELEPHONE_NO"] != null)
            //        {
            //            if (_dtConDetails.Rows[0]["TELEPHONE_NO"].ToString().Split(',').Length > 0)
            //                txtCustPhone = _dtConDetails.Rows[0]["TELEPHONE_NO"].ToString().Split(',')[0].ToString();
            //            else if (_dtConDetails.Rows[0]["TELEPHONE_NO"].ToString().Split(',').Length <= 0)
            //            {
            //                if (_dtConDetails.Rows[0]["TELEPHONE_NO"].ToString().Substring(0, 1) == "9")
            //                    txtCustMob = "";
            //                else
            //                    txtCustMob = _dtConDetails.Rows[0]["TELEPHONE_NO"].ToString();
            //            }
            //        }
            //        else
            //            txtCustMob = _dtConDetails.Rows[0]["TELEPHONE_NO"].ToString();


            //        txtEBSEMail = _dtConDetails.Rows[0]["E_MAIL"].ToString(); //strSplit(67)  ''objTable.Cell(1, "E_MAIL")
            //        txtAddlNo = _dtConDetails.Rows[0]["TEL1_NUMBER"].ToString(); //strSplit(63)   ''objTable.Cell(1, "TEL1_NUMBR")
            //        txtlandmark = _dtConDetails.Rows[0]["STREET4"].ToString(); //strSplit(27)   ''objTable.Cell(1, "STREET4")
            //        if (_dtConDetails.Rows[0]["BP_TYPE"].ToString() == "Normal")
            //            StrConsumerType = "";
            //        else
            //            StrConsumerType = "VIP";

            //        if (_dtConDetails.Rows[0]["BP_TYPE"].ToString() == "KCC")
            //        {
            //            StrConsumerType = "KCC";
            //        }
            //        else
            //        {
            //            StrConsumerType = _dtConDetails.Rows[0]["BP_TYPE"].ToString();
            //        }
            //        txtCustMob = Convert.ToString(_dtConDetails.Rows[0]["TEL1_NUMBER"]);  //strSplit(63)  ''objTable.Cell(1, "TEL1_NUMBR")
            //        txtlandmark = Convert.ToString(_dtConDetails.Rows[0]["STREET4"]);// strSplit(27)  ''objTable.Cell(1, "STREET4")

            //    }
            //    else
            //    {
            //        return "Invaild CA No.";
            //    }


            //    string fixture_typ = string.Empty;
            //    int no_fixture = 0;
            //    string strCallDate = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");
            //    string strOccuranceDate = DateTime.Now.ToString("dd-MMM-yyyy");
            //    string strOccuranceTime = DateTime.Now.ToString("HH:mm");
            //    strCallDate = "to_date('" + strCallDate + "','dd-Mon-yyyy hh24:mi')";
            //    string strComplaintNo = "";

            //    if (cboPriority == "STREET LIGHT")
            //    {
            //        fixture_typ = cboFixtue;
            //        int.TryParse(txtStreetNo, out no_fixture);
            //    }
            //    else
            //    {
            //        fixture_typ = "";
            //        no_fixture = 0;
            //    }

            //    string sql = "select Area_code from ONM_CONSUMER_MASTER_INFO where CA_NO like '%" + _sCANo + "'";
            //    DataTable rs = new DataTable();
            //    rs = AllSelect.GetShowDetails(sql);

            //    string _sAreaCode = string.Empty;
            //    if (rs.Rows.Count > 0)
            //        _sAreaCode = rs.Rows[0]["Area_code"].ToString();
            //    else
            //        _sAreaCode = "";

            //    if (_sAreaCode != "")
            //    {
            //        rs = new DataTable();
            //        sql = "SELECT distinct DIST, DIST_NAME, PRIMARY_GRID_CODE, PRIMARY_GRID_NAME, GRID_PHONENO, FEEDER_CODE,FEEDER_NAME, AREA_CODE, AREA_NAME, TR_CODE,COMPLAINT_CENTRE, COMPLAINT_CENTRE_PH_NO,COMPLAINT_CENTRE_CODE, CIRCLE  FROM ONM_TRANSFORMER_SETUP_DETAIL WHERE AREA_CODE='" + (_sAreaCode) + "'";
            //        rs = AllSelect.GetShowDetails(sql);
            //        if (rs.Rows.Count > 0)
            //        {
            //            txtCustArea = rs.Rows[0]["AREA_NAME"].ToString();
            //            txtCustDistrict = rs.Rows[0]["DIST_NAME"].ToString();
            //            lblAreaCode = rs.Rows[0]["AREA_CODE"].ToString();
            //            strDIST = rs.Rows[0]["DIST"].ToString();
            //            strGridCode = rs.Rows[0]["PRIMARY_GRID_CODE"].ToString();
            //            strGridName = rs.Rows[0]["PRIMARY_GRID_NAME"].ToString();
            //            strGridPhone = rs.Rows[0]["GRID_PHONENO"].ToString();
            //            strFeederCode = rs.Rows[0]["FEEDER_CODE"].ToString();
            //            strFeederName = rs.Rows[0]["FEEDER_NAME"].ToString();
            //            strTRCOde = rs.Rows[0]["TR_CODE"].ToString();
            //            strCompCenter = rs.Rows[0]["COMPLAINT_CENTRE"].ToString();
            //            strCompCenterPhone = rs.Rows[0]["COMPLAINT_CENTRE_PH_NO"].ToString();
            //            strCircle = rs.Rows[0]["CIRCLE"].ToString();
            //        }
            //    }
            //    else
            //    {
            //        //  return ("Unable to findout area right now! Please try again later.");

            //        // Change By Dharm 16-Jul-2014

            //        rs = new DataTable();
            //        sql = "SELECT distinct DIST, DIST_NAME, PRIMARY_GRID_CODE, PRIMARY_GRID_NAME, GRID_PHONENO, FEEDER_CODE,FEEDER_NAME, AREA_CODE, AREA_NAME, TR_CODE,COMPLAINT_CENTRE, COMPLAINT_CENTRE_PH_NO,COMPLAINT_CENTRE_CODE, CIRCLE  FROM ONM_TRANSFORMER_SETUP_DETAIL WHERE AREA_CODE='" + AreaCode.ToString().Trim() + "'";
            //        rs = AllSelect.GetShowDetails(sql);
            //        if (rs.Rows.Count > 0)
            //        {
            //            txtCustArea = rs.Rows[0]["AREA_NAME"].ToString();
            //            txtCustDistrict = rs.Rows[0]["DIST_NAME"].ToString();
            //            lblAreaCode = rs.Rows[0]["AREA_CODE"].ToString();
            //            strDIST = rs.Rows[0]["DIST"].ToString();
            //            strGridCode = rs.Rows[0]["PRIMARY_GRID_CODE"].ToString();
            //            strGridName = rs.Rows[0]["PRIMARY_GRID_NAME"].ToString();
            //            strGridPhone = rs.Rows[0]["GRID_PHONENO"].ToString();
            //            strFeederCode = rs.Rows[0]["FEEDER_CODE"].ToString();
            //            strFeederName = rs.Rows[0]["FEEDER_NAME"].ToString();
            //            strTRCOde = rs.Rows[0]["TR_CODE"].ToString();
            //            strCompCenter = rs.Rows[0]["COMPLAINT_CENTRE"].ToString();
            //            strCompCenterPhone = rs.Rows[0]["COMPLAINT_CENTRE_PH_NO"].ToString();
            //            strCircle = rs.Rows[0]["CIRCLE"].ToString();
            //        }

            //    }


            //    DataTable rsTmp = new DataTable();
            //    rsTmp = AllSelect.GetShowDetails("SELECT trim(SUBSTR(CIRCLE,1,1)||COMPLAINT_CENTRE_CODE|| DIST ||TO_Char(sysdate,'YYMMDD')) as strCompNo,COMPLAINT_CENTRE_CODE FROM ONM_TRANSFORMER_SETUP_DETAIL WHERE AREA_CODE='" + lblAreaCode + "'");
            //    if (rsTmp.Rows.Count > 0)
            //    {
            //        strComplaintNo = rsTmp.Rows[0]["strCompNo"].ToString();
            //        strCompCenterForSapSerch = rsTmp.Rows[0]["COMPLAINT_CENTRE_CODE"].ToString();
            //    }
            //    rsTmp = new DataTable();
            //    rsTmp = AllSelect.GetShowDetails("select SP_GENERATECOMPLAINTNO('" + lblAreaCode + "') from dual");
            //    if (rsTmp.Rows.Count > 0)
            //    {
            //        strComplaintNo = strComplaintNo.Trim() + rsTmp.Rows[0][0].ToString() + "P";
            //    }
            //    lblFCode = lstFaultCatg;


            //    string strsSQL = string.Empty;
            //    strsSQL = "INSERT INTO CC_DVB_COMP(CALL_DATE,KNO,AREA_CODE,AREA,DISTRICT,CALL_CATEG,";
            //    strsSQL = strsSQL + "NAME,ADD1,ADD2,PHONE,FCODE,REMARKS,COMPNO,CUST_REMARKS,OAP_USER,CUST_STATUS,CUST_TIME_CLEAR,DVB_CLEARED_BY,DVB_STATUS,TIME_TAKEN,XEN_STATUS,PRIORITY,NOOFCOMPLAINTS,CONS_REF,OPERATIONAL_COMP_NO,CM_HOUSE,TYP_FIXTURE, NO_FIXTURE,LAND_MARK,CONSUMER_TYPE,MOB_NO, ENTRY_KNO_ID,REG_MODULE_NAME,REG_AREA, REG_AREA_CODE,SAP_CONTRACT_NO,SINCE_LAST_MINUTES,Addl_Cont_No,SINCE_LAST_DAYS) VALUES";
            //    strsSQL = strsSQL + "(" + strCallDate + ",'";
            //    strsSQL = strsSQL + _sCANo + "','" + lblAreaCode + "','" + txtCustArea + "','" + txtCustDistrict + "','" + cboCallCateg + "','";
            //    strsSQL = strsSQL + txtCustName.Trim() + "','" + txtCustAdd1.Trim() + "','" + txtCustAdd2.Trim() + "','" + txtCustMob + "','";
            //    strsSQL = strsSQL + lblFCode + "','" + txtCustRemarks + "','" + strComplaintNo + "','" + txtCustRemarks + "','";
            //    strsSQL = strsSQL + UserName + "','" + strCLEAR_STATUS + "',NULL,'" + txtClearedBy + "',";
            //    strsSQL = strsSQL + "'" + strCLEAR_STATUS + "'," + lblTimeTaken + ",'N',";

            //    if (cboPriority == "EMERGENCY")
            //        strsSQL = strsSQL + " 'PCR',";
            //    else
            //        strsSQL = strsSQL + "'" + cboPriority.ToUpper() + "',";

            //    strsSQL = strsSQL + " 1,'" + txtConsumerReferenceNo.Trim() + "','" + strComplaintNo.Substring(8, 11) + "','N','" + fixture_typ + "'," + no_fixture + ",'";
            //    strsSQL = strsSQL + txtlandmark + "','" + StrConsumerType + "','" + txtCustMob.Trim() + "','" + txtCustKNo.Trim() + "','";
            //    strsSQL = strsSQL + VMODULENAME.Trim() + "','" + txtCustArea.Trim() + "','" + lblAreaCode.Trim() + "','" + txtContractNo.Trim() + "',";
            //    strsSQL = strsSQL + cboMinutes.Trim() + ",'" + txtAddlNo.Trim() + "'," + cboDays + ")";




            //    string strOFCSQL = string.Empty;
            //    DataTable _dtFaultid = new DataTable();
            //    string strFaultID = "";
            //    _dtFaultid = AllSelect.GetShowDetails("SELECT SP_GenerateFaultId('" + strDIST + "') FROM DUAL ");
            //    if (_dtFaultid.Rows.Count > 0)
            //        strFaultID = _dtFaultid.Rows[0][0].ToString();

            //    strOFCSQL = " Insert into ONM_Fault_Collections (KNO,FaultId,Dist,AreaCode,Area,FeederCode,FeederName,GridCode,GridName,";
            //    strOFCSQL = strOFCSQL + " GridPhone,FaultType,NoOfComplaints,Child,All_Transformer_No,Name,Complaint_Status,";
            //    strOFCSQL = strOFCSQL + " Complaint_No,InsertedFrom,COMPLAINT_CENTRE,COMPLAINT_CENTRE_PH_NO,Com_Name,Com_Add1,Com_Add2,Com_Phone,Circle,";
            //    strOFCSQL = strOFCSQL + " CCRemarks,Powercut_Type,PRIORITY,FCODE,TYP_FIXTURE,NO_FIXTURE,ROAD,LAND_MARK,OCCURANCE_DT,CC_REG_BY,CC_REG_IP,CONSUMER_TYPE)";
            //    strOFCSQL = strOFCSQL + " values";
            //    strOFCSQL = strOFCSQL + " ('" + _sCANo + "','" + strFaultID + "','" + strDIST + "','" + lblAreaCode + "','" + txtCustArea + "',";
            //    strOFCSQL = strOFCSQL + "'" + strFeederCode + "','" + strFeederName + "','" + strGridCode + "','" + strGridName + "',";
            //    strOFCSQL = strOFCSQL + "'" + strGridPhone + "','DT AND BELOW',1,'0','" + strTRCOde + "','ANDRIOD', ";
            //    strOFCSQL = strOFCSQL + "'P','" + strComplaintNo + "', ";
            //    strOFCSQL = strOFCSQL + "'CC" + strFaultID + "','" + strCompCenter + "','" + strCompCenterPhone + "','" + txtCustName.Trim() + "', ";
            //    strOFCSQL = strOFCSQL + "'" + txtCustAdd1.Trim() + "','" + txtCustAdd2.Trim() + "','" + txtCustMob + "','" + strCircle + "', ";
            //    strOFCSQL = strOFCSQL + "'" + txtCustRemarks.Trim() + "','CC',";

            //    if (cboPriority == "EMERGENCY")
            //        strOFCSQL = strOFCSQL + " 'PCR',";
            //    else
            //        strOFCSQL = strOFCSQL + "'" + cboPriority + "',";

            //    strOFCSQL = strOFCSQL + "'" + lblFCode + "',";
            //    strOFCSQL = strOFCSQL + "'" + fixture_typ.Trim() + "'," + no_fixture + ",'','" + txtlandmark.Trim() + "'," + strCallDate + ",'" + UserName + "',sys_context('USERENV','IP_ADDRESS'),'" + StrConsumerType + "')";

            //    string strOSFDSQL = string.Empty, strOOMFSQL = string.Empty;

            //    if (cboPriority == "STREET LIGHT")
            //    {
            //        strOSFDSQL = " INSERT INTO ONM_SL_FAULT_DETAILS(FAULTID) VALUES ('" + strFaultID + "')";
            //        // AllInsert.InsertBySQL(strOSFDSQL);
            //    }
            //    if (lblFCode == "F025")
            //    {
            //        strOOMFSQL = " INSERT INTO ONM_OFC_METER_FAULTS(FAULTID) VALUES ('" + strFaultID + "')";
            //        //  AllInsert.InsertBySQL(strOOMFSQL); 
            //    }

            //    string[] _smultipleQuery = new string[] { strsSQL, strOFCSQL, strOSFDSQL, strOOMFSQL };
            //    if (!AllInsert.InsertByMultipleSQL_ONM(_smultipleQuery))
            //    {
            //        return ("Not saved. Please try again.");
            //    }
            //    string lblFullComplaintNo = strComplaintNo;
            //    return strComplaintNo + ":" + strComplaintNo.Substring(8, 11);

            //}
            //catch (Exception ex)
            //{
            //    return "Unable to register right now! Please try again.";
            //    //MessageBox.show(ex.ToString());
            //    //Interaction.MsgBox(ex.ToString(), MsgBoxStyle.SystemModal);
            //}

            //return "Complaint Registration Service is temporarily down.To register complaint, please call customer care.";
            return "Temporarily not available.Will be back soon";
        }
        else
        {
            return ("Unauthorized Access!");
        }

    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable DISPLAY_BILL(string strCANumber)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (strCANumber.Length < 12)
                strCANumber = strCANumber.PadLeft(12, '0');
            dt = isu.ZBAPI_DISPLAY_BILL_WEB(strCANumber, "").Tables[0];
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable BILL_HIST(string strCANumber, string strBillMonth)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (strCANumber.Length < 12)
                strCANumber = strCANumber.PadLeft(12, '0');

            dt = isu.ZBI_WEBBILL_HIST(strCANumber, "").Tables[0];
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable Z_BAPI_IVRS(string strContractAccountNumber)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt = isu.Z_BAPI_IVRS("000" + strContractAccountNumber).Tables[0];
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet ZBAPI_IVR_CREATESO_ISU(string strCANumber, string strCACrn, string strCAKNumber
        , string strMeterNumber, string strISUOrder, string strComplaintType
        , string strContractNumber, string strTelephoneNo, string strDescription)//02-02-2015
    {

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataSet ds = new DataSet();
            //if (strCANumber.Length < 12)
            //    strCANumber = strCANumber.PadLeft(12, '0');
            ds = isu.ZBAPI_IVR_CREATESO_ISU(strCANumber, strCACrn, strCAKNumber
        , strMeterNumber, strISUOrder, strComplaintType
        , strContractNumber, strTelephoneNo, strDescription);

            return ds;
        }
        else
            return (InvaildAuthonticationds());

    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable CA_DISPLAY(string strCANumber, string strSerialNumber, string strConsumerNumber, string strTelephoneNumber, string strKNumber, string strContractNumber)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (strCANumber.Length < 12 && strCANumber.Length > 0)
                strCANumber = strCANumber.PadLeft(12, '0');
            if (strSerialNumber.Length < 18 && strSerialNumber.Length > 0)
                strSerialNumber = strSerialNumber.PadLeft(18, '0');

            dt = isu.Z_BAPI_CMS_ISU_CA_DISPLAY(strCANumber, strSerialNumber, "", "", "", "").Tables[0];
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool CA_ADDRESS_UPLOAD(string strCa_Number, string strBp_Name, string strHouse_Number
        , string strHouse_Number_Sup, string strFloor, string strStreet
        , string strStreet2, string strStreet3, string strStreet4
         , string strCity, string strPost_Code, string strTelephone_No,
        string strE_Mail, string strsign_Img, string strIDProof_Img, string strIMEI
        , string strLatitude, string strLongtitude, string strPoleNo
            , string strCustomerIdNo, string strEmpName, string strEmpId)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.Insert_CA_ADDRESS_UPLOAD(strCa_Number, strBp_Name, strHouse_Number,
                strHouse_Number_Sup, strFloor, strStreet, strStreet2, strStreet3, strStreet4,
                strCity, strPost_Code, strTelephone_No, strE_Mail, strIMEI, strsign_Img, strIDProof_Img,
                strLatitude, strLongtitude, strPoleNo, strCustomerIdNo, strEmpName, strEmpId);
            return _bReturn;
        }
        else
            return (false);
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ZBAPIDOCLIST(string strAufnr, string strC_001, string strC_002
        , string strC_003, string strC_004, string strC_005, string strC_007
        , string strC_008, string strC_009, string strC_010
        , string strC_011, string strC_012, string strC_013
        , string strC_014, string strC_015, string strC_016
        , string strC_017, string strC_018, string strC_019
        , string strC_020, string strC_021, string strC_022
        , string strC_023, string strC_024, string strC_025, string strC_026
        , string strC_027, string strC_028, string strC_029, string strC_030
        , string strC_031, string strC_032, string strC_033, string strC_034
        , string strC_035, string strC_036
        , string strC_037, string strC_038, string strC_039, string strC_040, string strC_041, string strC_070
        , string strR_Cdll, string strR_Occ, string strR_Own, string strZ_Appltype)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            try
            {
                //DELHIWSTESTD.ISUService isu1 = new DELHIWSTESTD.ISUService();

                DELHIWSTESTDV2.WebService isu1 = new DELHIWSTESTDV2.WebService();
                DataTable dt = new DataTable();
                // DataSet ds = new DataSet();
                dt = isu1.ZBAPIDOCLIST(strAufnr, strC_001, strC_002
            , strC_003, strC_004, strC_005, strC_007
            , strC_008, strC_009, strC_010
            , strC_011, strC_012, strC_013
            , strC_014, strC_015, strC_016
            , strC_017, strC_018, strC_019
            , strC_020, strC_021, strC_022
            , strC_023, strC_024, strC_025, strC_026
            , strC_027, strC_028, strC_029, strC_030
            , strC_031, strC_032, strC_033, strC_034
            , strC_035, strC_036, strC_037, strC_038, strC_039, strC_040, strC_041, strC_070
            , strR_Cdll, strR_Occ, strR_Own, strZ_Appltype).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                newClassFile.WriteIntoFile("ZBAPIDOCLIST Error :" + ex.ToString());
                return null;
            }
        }
        else
            return (InvaildAuthontication());
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NEW_CONNECTION_SITE_TASKS(
        string Connected_Load, string Usage_Category_Of_site,
        string Pole_No_Feeder_Pillar_No, string meter_No,
        string Service_Line_Length, string Building_Hieght,
        string Wiring_Completed, string Lift_Installed,
        string Lift_Certificate_Required, string Affidavit_Required,
        string Floor, string Tf_Seal_No, string metering_position,
        string Initial_Cf_OK, string CA_No, string Amount,
        string applied_premises, string remarks,
        string strOrderNo, string strsign_Img, string building_img1, string building_img2, string IMEI_NO, string strLatitude, string strLongtitude, string FILE_ATTACHMENT, string SITE_LAYOUT_IMG, string CABLE_TYPE, string LEFT_METER_NO, string RIGHT_METER_NO, string APPLIED_AREA, string BUILDING_AREA, string strPasted)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.Insert_NEW_CONNECTION_SITE_TASKS(Connected_Load, Usage_Category_Of_site,
             Pole_No_Feeder_Pillar_No, meter_No,
             Service_Line_Length, Building_Hieght,
             Wiring_Completed, Lift_Installed,
             Lift_Certificate_Required, Affidavit_Required,
             Floor, Tf_Seal_No, metering_position,
             Initial_Cf_OK, CA_No, Amount,
             applied_premises, remarks,
             strOrderNo, strsign_Img, building_img1, building_img2, IMEI_NO, strLatitude, strLongtitude, FILE_ATTACHMENT, SITE_LAYOUT_IMG, CABLE_TYPE, LEFT_METER_NO, RIGHT_METER_NO, APPLIED_AREA, BUILDING_AREA, strPasted);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NEW_CONNECTION_SITE_TASKS2(
                   string Connected_Load, string Usage_Category_Of_site,
                   string Pole_No_Feeder_Pillar_No, string meter_No,
                   string Service_Line_Length, string Building_Hieght,
                   string Wiring_Completed, string Lift_Installed,
                   string Lift_Certificate_Required, string Affidavit_Required,
                   string Floor, string Tf_Seal_No, string metering_position,
                   string Initial_Cf_OK, string CA_No, string Amount,
                   string applied_premises, string remarks,
                   string strOrderNo, string strsign_Img, string building_img1,
                   string building_img2, string IMEI_NO, string strLatitude,
                   string strLongtitude, string FILE_ATTACHMENT, string SITE_LAYOUT_IMG,
                   string CABLE_TYPE, string LEFT_METER_NO, string RIGHT_METER_NO,
                   string APPLIED_AREA, string BUILDING_AREA, string strPasted,
                   string NEW_BUILDING, string NORMATIVE_LOAD, string COVERED_AREA,
                   string PLOT_AREA, string Existing_Meter_No, string Encroachment)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.Insert_NEW_CONNECTION_SITE_TASKS2(Connected_Load, Usage_Category_Of_site,
             Pole_No_Feeder_Pillar_No, meter_No, Service_Line_Length, Building_Hieght, Wiring_Completed,
             Lift_Installed, Lift_Certificate_Required, Affidavit_Required, Floor, Tf_Seal_No, metering_position,
             Initial_Cf_OK, CA_No, Amount, applied_premises, remarks, strOrderNo, strsign_Img, building_img1,
             building_img2, IMEI_NO, strLatitude, strLongtitude, FILE_ATTACHMENT, SITE_LAYOUT_IMG, CABLE_TYPE,
             LEFT_METER_NO, RIGHT_METER_NO, APPLIED_AREA, BUILDING_AREA, strPasted, NEW_BUILDING, NORMATIVE_LOAD,
             COVERED_AREA, PLOT_AREA, Existing_Meter_No, Encroachment);


            return _bReturn;
        }
        else
            return (false);
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string NEW_CONNECTION_SITE_TASKS2_New(
               string Connected_Load, string Usage_Category_Of_site,
               string Pole_No_Feeder_Pillar_No, string meter_No,
               string Service_Line_Length, string Building_Hieght,
               string Wiring_Completed, string Lift_Installed,
               string Lift_Certificate_Required, string Affidavit_Required,
               string Floor, string Tf_Seal_No, string metering_position,
               string Initial_Cf_OK, string CA_No, string Amount,
               string applied_premises, string remarks,
               string strOrderNo, string strsign_Img, string building_img1,
               string building_img2, string IMEI_NO, string strLatitude,
               string strLongtitude, string FILE_ATTACHMENT, string SITE_LAYOUT_IMG,
               string CABLE_TYPE, string LEFT_METER_NO, string RIGHT_METER_NO,
               string APPLIED_AREA, string BUILDING_AREA, string strPasted,
               string NEW_BUILDING, string NORMATIVE_LOAD, string COVERED_AREA,
               string PLOT_AREA, string Existing_Meter_No, string Encroachment)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            string _bReturn = "";
            _bReturn = newClassFile.Insert_NEW_CONNECTION_SITE_TASKS2New(Connected_Load, Usage_Category_Of_site,
             Pole_No_Feeder_Pillar_No, meter_No, Service_Line_Length, Building_Hieght, Wiring_Completed,
             Lift_Installed, Lift_Certificate_Required, Affidavit_Required, Floor, Tf_Seal_No, metering_position,
             Initial_Cf_OK, CA_No, Amount, applied_premises, remarks, strOrderNo, strsign_Img, building_img1,
             building_img2, IMEI_NO, strLatitude, strLongtitude, FILE_ATTACHMENT, SITE_LAYOUT_IMG, CABLE_TYPE,
             LEFT_METER_NO, RIGHT_METER_NO, APPLIED_AREA, BUILDING_AREA, strPasted, NEW_BUILDING, NORMATIVE_LOAD,
             COVERED_AREA, PLOT_AREA, Existing_Meter_No, Encroachment);


            return _bReturn;
        }
        else
            return ("Not Access");
    }



    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NEW_CONNECTION_SITE_TASKS3(
                   string Connected_Load, string Usage_Category_Of_site,
                   string Pole_No_Feeder_Pillar_No, string meter_No,
                   string Service_Line_Length, string Building_Hieght,
                   string Wiring_Completed, string Lift_Installed,
                   string Lift_Certificate_Required, string Affidavit_Required,
                   string Floor, string Tf_Seal_No, string metering_position,
                   string Initial_Cf_OK, string CA_No, string Amount,
                   string applied_premises, string remarks,
                   string strOrderNo, string strsign_Img, string building_img1,
                   string building_img2, string IMEI_NO, string strLatitude,
                   string strLongtitude, string FILE_ATTACHMENT, string SITE_LAYOUT_IMG,
                   string CABLE_TYPE, string LEFT_METER_NO, string RIGHT_METER_NO,
                   string APPLIED_AREA, string BUILDING_AREA, string strPasted,
                   string NEW_BUILDING, string NORMATIVE_LOAD, string COVERED_AREA,
                   string PLOT_AREA, string Existing_Meter_No, string Encroachment,
                   string BuildingHeightRemarks, string ELCB)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;

            if (BuildingHeightRemarks.Length > 245)
                BuildingHeightRemarks = BuildingHeightRemarks.Substring(0, 244);

            _bReturn = newClassFile.Insert_NEW_CONNECTION_SITE_TASKS3(Connected_Load, Usage_Category_Of_site,
             Pole_No_Feeder_Pillar_No, meter_No, Service_Line_Length, Building_Hieght, Wiring_Completed,
             Lift_Installed, Lift_Certificate_Required, Affidavit_Required, Floor, Tf_Seal_No, metering_position,
             Initial_Cf_OK, CA_No, Amount, applied_premises, remarks, strOrderNo, strsign_Img, building_img1,
             building_img2, IMEI_NO, strLatitude, strLongtitude, FILE_ATTACHMENT, SITE_LAYOUT_IMG, CABLE_TYPE,
             LEFT_METER_NO, RIGHT_METER_NO, APPLIED_AREA, BUILDING_AREA, strPasted, NEW_BUILDING, NORMATIVE_LOAD,
             COVERED_AREA, PLOT_AREA, Existing_Meter_No, Encroachment, BuildingHeightRemarks, ELCB);


            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string updatebreakdownreadstatsu(string BD_ID)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            string _sResult = string.Empty;
            string _sBYPLServices = System.Web.Configuration.WebConfigurationManager.AppSettings["BYPLServicesLive_TestD"].ToString();
            if (_sBYPLServices == "TEST")
            {
                BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                _sResult = BYPLServices.updatebreakdownreadstatsu("@CPbsESyaMUna", BD_ID);
            }
            else
            {
                //BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                //_sResult1 = BYPLServices.updatebreakdownreadstatsu(BD_ID);

                _sResult = "OOPs! service not available";
                //BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                //ds = BYPLServices.getbreakdowndetails(IMEINO);

            }
            return _sResult;
        }
        else
            return ("INVAILD AUTHENTICATION");
    }

    //12-02-2015 New
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet getbreakdowndetails(string IMEINO)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataSet ds = new DataSet();
            string _sBYPLServices = System.Web.Configuration.WebConfigurationManager.AppSettings["BYPLServicesLive_TestD"].ToString();
            if (_sBYPLServices == "TEST")
            {
                BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                ds = BYPLServices.getbreakdowndetails("@CPbsESyaMUna", IMEINO);
            }
            else
            {
                BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                ds = BYPLServices.getbreakdowndetails("@CPbsESyaMUna", IMEINO);

                // ds = NotAvail_ds();
                //BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                //ds = BYPLServices.getbreakdowndetails(IMEINO);

            }
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string getbreakdownstatus(string BD_ID)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            string _sResult = string.Empty;
            string _sBYPLServices = System.Web.Configuration.WebConfigurationManager.AppSettings["BYPLServicesLive_TestD"].ToString();
            if (_sBYPLServices == "TEST")
            {
                BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                _sResult = BYPLServices.getbreakdownstatus("@CPbsESyaMUna", BD_ID);
            }
            else
            {
                //BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                //_sResult1 = BYPLServices.updatebreakdownreadstatsu(BD_ID);

                _sResult = "OOPs! service not available";
                //BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                //ds = BYPLServices.getbreakdowndetails(IMEINO);

            }
            return _sResult;
        }
        else
            return ("INVAILD AUTHENTICATION");
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool SCAN_FOR_AFFIDAVIT(string strOrderNo, string strScan_for_AFFIDAVIT)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.update_NEW_CONNECTION_SITE_TASKS_SCAN_FOR_AFFIDAVIT(strOrderNo, strScan_for_AFFIDAVIT);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NEW_CONNECTION_SITE_STATUS(string strOrderNo, string strStatus, string strNewRemarks, string strRescheduledate)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.NEW_CONNECTION_SITE_STATUS(strOrderNo, strStatus, strNewRemarks, strRescheduledate);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool SCAN_FOR_ADDRESS_PROOF(string strOrderNo, string strScan_for_AFFIDAVIT)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.update_NEW_CONNECTION_SITE_TASKS_SCAN_FOR_ADDRESS_PROOF(strOrderNo, strScan_for_AFFIDAVIT);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool SCAN_FOR_ID_PROOF(string strOrderNo, string strScan_for_AFFIDAVIT)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.update_NEW_CONNECTION_SITE_TASKS_SCAN_FOR_ID_PROOF(strOrderNo, strScan_for_AFFIDAVIT);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool SCAN_FOR_Ownership(string strOrderNo, string strScan_for_Ownership)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.update_NEW_CONNECTION_SITE_TASKS_SCAN_FOR_Ownership(strOrderNo, strScan_for_Ownership);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool SCAN_FOR_Application(string strOrderNo, string strScan_for_Application)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.update_NEW_CONNECTION_SITE_TASKS_SCAN_FOR_Application(strOrderNo, strScan_for_Application);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool SCAN_FOR_OTHER(string strOrderNo, string strScan_for_AFFIDAVIT)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.update_NEW_CONNECTION_SITE_TASKS_SCAN_FOR_OTHER(strOrderNo, strScan_for_AFFIDAVIT);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NEW_CONNECTION_SITE_TASKS1(string strOrderNo, string strCode_Group, string strTask_Code)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.NEW_CONNECTION_SITE_TASKS1(strOrderNo, strCode_Group, strTask_Code);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NEW_CONNECTION_SITE_SIGN_IMG(string strOrderNo, string strsign_Img, string building_img1, string building_img2)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.Insert_sign_Img(strOrderNo, strsign_Img, building_img1, building_img2);
            return _bReturn;
        }
        else
            return (false);
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NEW_CONNECTION_pole_TF_RC_Premises(string strOrderNo, string strPole_Img, string strTF_Img, string strRC_Premises_Img)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.Insert_pole_TF_RC_Premises(strOrderNo, strPole_Img, strTF_Img, strRC_Premises_Img);
            return _bReturn;
        }
        else
            return (false);
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NEW_CONNECTION_pole_TF_RC_Premises_KCC(string strOrderNo, string strPole_Img, string strTF_Img, string strRC_Premises_Img,
                                                   string strOtherImg2, string strOtherImg3, string strOtherImg4)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.Insert_pole_TF_RC_Premises_KCC(strOrderNo, strPole_Img, strTF_Img, strRC_Premises_Img, strOtherImg2,
                                                                         strOtherImg3, strOtherImg4);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ISUSTD_USER_Old(string strUser_Name, string strPassword)
    {


        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = NewClassFile.ISUSTD_USER_OLD(strUser_Name, strPassword);
            dt.TableName = "ISUSTD_USER";


            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ISUSTD_USER(string strUser_Name, string strPassword, string strIMEI_No)
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("password", typeof(string));
            dt.Columns.Add("NAME", typeof(string));
            dt.Columns.Add("EMEI_no", typeof(string));

            dt = newClassFile.ISUSTD_USER(strUser_Name, strPassword, strIMEI_No);
            dt.TableName = "ISUSTD_USER";
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add("Incorrect User or Password", "Incorrect User or Password", "", strIMEI_No);
                dt.AcceptChanges();
                return dt;
            }
            else if (strUser_Name != "" && strPassword != "" && strIMEI_No != "")
            {
                if (strUser_Name != dt.Rows[0]["User_name"].ToString())
                {
                    dt.Rows.Add("User Name Incorrect!", dt.Rows[0]["password"].ToString(), dt.Rows[0]["NAME"].ToString(), dt.Rows[0]["IMEI_NO"].ToString());
                    dt.AcceptChanges();
                }
                else if (strPassword != dt.Rows[0]["password"].ToString())
                {
                    dt.Rows.Add(dt.Rows[0]["User_name"].ToString(), "Password Incorrect", dt.Rows[0]["NAME"].ToString(), dt.Rows[0]["IMEI_NO"].ToString());
                    dt.AcceptChanges();
                }
                else if (strIMEI_No != dt.Rows[0]["IMEI_NO"].ToString())
                {
                    dt.Rows.Add(dt.Rows[0]["User_name"].ToString(), dt.Rows[0]["password"].ToString(), dt.Rows[0]["NAME"].ToString(), "IMEI Number Incorrect");
                    dt.AcceptChanges();
                }
                else if (strUser_Name == dt.Rows[0]["User_name"].ToString() && strPassword == dt.Rows[0]["password"].ToString() && strIMEI_No == dt.Rows[0]["IMEI_NO"].ToString())
                {
                    dt.Rows.Add(dt.Rows[0]["User_name"].ToString(), dt.Rows[0]["password"].ToString(), dt.Rows[0]["NAME"].ToString(), dt.Rows[0]["IMEI_no"].ToString());
                    dt.AcceptChanges();
                }
            }
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0].Delete();
                dt.AcceptChanges();
                return dt;
            }
            else
                return dt;
        }
        else
            return (InvaildAuthontication());
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool DOCLIST(string strOrderNo, string strC_001, string strC_002
        , string strC_003, string strC_004, string strC_005
        , string strC_006, string strC_007, string strC_008
        , string strC_009, string strC_010, string strC_011
        , string strC_012, string strC_013, string strC_014
        , string strC_015, string strC_016, string strC_017
        , string strC_018, string strC_019, string strC_020
        , string strC_021, string strC_022, string strC_023
        , string strC_024, string strC_025, string strC_026
        , string strC_027, string strC_028, string strC_029
        , string strC_030, string strC_031, string strC_032
        , string strC_033, string strC_034, string strC_035
        , string strC_036, string strC_037, string strC_038
        , string strC_039, string strC_040, string strC_041, string strC_070, string strsign_Img)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.Insert_DOCLIST(strOrderNo, strC_001, strC_002
            , strC_003, strC_004, strC_005
            , strC_006, strC_007, strC_008
            , strC_009, strC_010, strC_011
            , strC_012, strC_013, strC_014
            , strC_015, strC_016, strC_017
            , strC_018, strC_019, strC_020
            , strC_021, strC_022, strC_023
            , strC_024, strC_025, strC_026
            , strC_027, strC_028, strC_029
            , strC_030, strC_031, strC_032
            , strC_033, strC_034, strC_035
            , strC_036, strC_037, strC_038
            , strC_039, strC_040, strC_041, strC_070, strsign_Img);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool DOCLIST_NEW(string strOrderNo, string strDocument_Type)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.Insert_DOCLIST_NEW(strOrderNo, strDocument_Type);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool Insert_DOCLIST_sign(string strOrderNo, string strsign_Img)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.Insert_DOCLIST_sign(strOrderNo, strsign_Img);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool Insert_CA_building_img(string strCA, string strbuilding_img)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.Insert_CA_building_img(strCA, strbuilding_img);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool Insert_IR_DATA(string strOrderNo, string strdivision,
            string strbp, string strapplicantName, string strcategory,
            string strfathersName, string strloadKW, string strloadKVA, string strappliedAddress, string strcontactNo,
            string strvisitDate, string strtime, string strdocReceived, string strcreation, string strenggName, string strEmail_id)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.Insert_IR_DATA(strOrderNo, strdivision,
            strbp, strapplicantName, strcategory,
            strfathersName, strloadKW, strloadKVA, strappliedAddress, strcontactNo,
            strvisitDate, strtime, strdocReceived, strcreation, strenggName, strEmail_id);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool UPDATE_IR_DATA(string strOrderNo, string strdivision,
            string strbp, string strapplicantName, string strcategory,
            string strfathersName, string strloadKW, string strloadKVA, string strappliedAddress, string strcontactNo,
            string strvisitDate, string strtime, string strdocReceived, string strcreation, string strenggName)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.UPDATE_IR_DATA(strOrderNo, strdivision,
                strbp, strapplicantName, strcategory,
                strfathersName, strloadKW, strloadKVA, strappliedAddress, strcontactNo,
                strvisitDate, strtime, strdocReceived, strcreation, strenggName);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool Insert_CF_DATA(string strOrderNo, string strnetoutstandingAmt,
           string strbp, string strca, string strmoveOutDate,
           string strconsref, string strname, string straddress, string strcheckEnforcement, string strlastPaymentMode,
           string strsequenceNo, string strcheckRelated, string strUserType)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.Insert_CF_DATA(strOrderNo, strnetoutstandingAmt,
                strbp, strca, strmoveOutDate,
                strconsref, strname, straddress, strcheckEnforcement, strlastPaymentMode,
                strsequenceNo, strcheckRelated, strUserType);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool UPDATE_CF_DATA(string strOrderNo, string strnetoutstandingAmt,
           string strbp, string strca, string strmoveOutDate,
           string strconsref, string strname, string straddress, string strcheckEnforcement, string strlastPaymentMode,
           string strsequenceNo, string strcheckRelated)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.UPDATE_CF_DATA(strOrderNo, strnetoutstandingAmt,
                strbp, strca, strmoveOutDate,
                strconsref, strname, straddress, strcheckEnforcement, strlastPaymentMode,
                strsequenceNo, strcheckRelated);
            return _bReturn;
        }
        else
            return (false);
    }


    //26042016
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool Insert_RECF_DATA(string strOrderNo, string strmeter1,
           string stradd1, string strmeter2, string stradd2,
           string strmeter3, string stradd3, string strmeter4, string stradd4, string strmeter5,
           string stradd5)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.insert_ReCF_DATA(strOrderNo, strmeter1,
                stradd1, strmeter2, stradd2,
                strmeter3, stradd3, strmeter4, stradd4, strmeter5,
                stradd5);
            return _bReturn;
        }
        else
            return (false);
    }


    # region GCMPushnotification

    [WebMethod] //Updated New
    [SoapHeader("consumer", Required = true)]
    public String GCMRegistration(string singleParameter)
    {
        NewClassFile newClassFile = new NewClassFile();

        string strReturn = string.Empty;
        //if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        //{
        //    strReturn = newClassFile.GCMRegistration_ONM(singleParameter);
        //    return strReturn;
        //}
        //else
        //    return ("INVAILD AUTHENTICATION");

        return "1";
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable GCMUserMsgLog(string singleParameter)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.GCMUserMsgLog_ONM(singleParameter);
            dt.TableName = "CNS_USERMSG_LOG";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string GCMUpdateMsgLog(string singleParameter)
    {
        NewClassFile newClassFile = new NewClassFile();

        string strReturn = string.Empty;
        //if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        //{
        //    strReturn = newClassFile.GCMUpdateMsgLog_ONM(singleParameter);
        //    return strReturn;
        //}
        //else
        //    return ("INVAILD AUTHENTICATION");
        return "1";

    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string GCMCheckRegistrationLog(string singleParameter)
    {
        NewClassFile newClassFile = new NewClassFile();

        string strReturn = string.Empty;
        //if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        //{
        //  strReturn = newClassFile.GCMCheckRegistrationLog_ONM(singleParameter);
        //    return strReturn;
        //}
        // else
        // return ("INVAILD AUTHENTICATION");
        return "1";
    }


    # endregion

    #region "Bypl Services"

    //11-02-2015 New
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet RegisterSmartAppComplaint(string CA, string Phone, string FaultCategory, string SubFaultType
        , string CallerName, string Address, string Email, string Remarks)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataSet ds = new DataSet();
            string _sBYPLServices = System.Web.Configuration.WebConfigurationManager.AppSettings["BYPLServicesLive_TestD"].ToString();
            if (_sBYPLServices == "TEST")
            {
                BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                ds = BYPLServices.RegisterSmartAppComplaint("@CPbsESyaMUna", CA, Phone, FaultCategory, SubFaultType
                     , CallerName, Address, Email, Remarks);
            }
            else
            {
                BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                ds = BYPLServices.RegisterSmartAppComplaint("@CPbsESyaMUna", CA, Phone, FaultCategory, SubFaultType
                     , CallerName, Address, Email, Remarks);

            }
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string updatebreakdown(string BD_ID, int newhour, int newminutes)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            string _sResult = string.Empty;
            string _sBYPLServices = System.Web.Configuration.WebConfigurationManager.AppSettings["BYPLServicesLive_TestD"].ToString();
            if (_sBYPLServices == "TEST")
            {
                BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                _sResult = BYPLServices.updatebreakdown("@CPbsESyaMUna", BD_ID, newhour, newminutes);
            }
            else
            {
                BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                _sResult = BYPLServices.updatebreakdown("@CPbsESyaMUna", BD_ID, newhour, newminutes);

                // _sResult = "OOPs! service not available";
                //BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                //ds = BYPLServices.getbreakdowndetails(IMEINO);

            }
            return _sResult;
        }
        else
            return ("INVAILD AUTHENTICATION");
    }

    //12-02-2015 New
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet GetComplaintStatus(string ComplaintNo)
    {
        DataSet ds = new DataSet();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            string _sResult = string.Empty;
            string _sBYPLServices = System.Web.Configuration.WebConfigurationManager.AppSettings["BYPLServicesLive_TestD"].ToString();
            if (_sBYPLServices == "TEST")
            {
                BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                ds = BYPLServices.GetComplaintStatus("@CPbsESyaMUna", ComplaintNo, "");
            }
            else
            {
                BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                ds = BYPLServices.GetComplaintStatus("@CPbsESyaMUna", ComplaintNo, "");

                //ds = NotAvail_ds();

            }
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }

    //12-02-2015 New
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet GetComplaintDetailsCA(string CANo)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataSet ds = new DataSet();
            string _sResult = string.Empty;
            string _sBYPLServices = System.Web.Configuration.WebConfigurationManager.AppSettings["BYPLServicesLive_TestD"].ToString();
            if (_sBYPLServices == "TEST")
            {
                BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                ds = BYPLServices.GetComplaintDetailsCA("@CPbsESyaMUna", CANo);
            }
            else
            {
                BYPLOMSONLINE_Services_TestD.Service1 BYPLServices = new BYPLOMSONLINE_Services_TestD.Service1();
                ds = BYPLServices.GetComplaintDetailsCA("@CPbsESyaMUna", CANo);
                // ds = NotAvail_ds();

            }
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }

    #endregion

    #region "UserAccount"
    //18-02-2015 New
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetValidateUser(string _sUser, string _sPass)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.GetValidateUser(_sUser, _sPass);
            dt.TableName = "ValidateUser";
            return (dt);
        }
        else
            return (InvaildAuthontication());
    }

    //18-02-2015 New
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NewRegistration_ARD(string strConsRef, string strUserName, string strPass
         , string strEmailId, string strMobileNo, string strPhoneNo, string strContactPerson)
    {
        AllInsert allInsert = new AllInsert();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return (allInsert.NewRegistration_ARD(strConsRef, strUserName, strPass
         , strEmailId, strMobileNo, strPhoneNo, strContactPerson));
        }
        else
            return (false);
    }

    //18-02-2015 New
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string GetPasswordSMS(string strConsRef)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return (newClassFile.GetPasswordSMS(strConsRef));
        }
        else
            return ("Unauthorized Access!");
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable EMP_UserDetails(string strEmpNo, string strOtherIfAny)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.empCompanyUserDetails(strEmpNo, strOtherIfAny);
            dt.TableName = "empCompanyUserDetails";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    #endregion

    #region "OMS Linemen BRPL By Rajveer 04042016"

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_GetAssignedComplaintsToTeam(string imeiNo) //Added By Ajay
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onm_GetAssignedComplaintsToTeamDL(imeiNo);
            dt.TableName = "ONM_GetAssignedComplaintsToTeam";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool ONM_UpdateComplaintResolutiontime(string ComplaintNo, string imeiNo, string FCode,
        string Latitude, string longitude, string remarks, string resolutionStatus, int resolutionTime,
        string Area_Power_Restored, int Restoration_Time)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return newClassFile.ONM_UpdateComplaintResolutiontimeDL(ComplaintNo, imeiNo, FCode,
                Latitude, longitude, remarks, resolutionStatus, resolutionTime, Area_Power_Restored,
                Restoration_Time);
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_GetComplaintStatus(string CompNo, string IMEINo)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.GetComplaintStatusDL(CompNo, IMEINo);
            dt.TableName = "ONM_GetComplaintStatus";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool CloseComplaintSig(string ComplaintNo, string CA, string FCode, string ClosingRemark, string OtherRemarks, string IMEI, string Latitude, string Longitude, string SignatureData)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool flg = true;
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            string Sql;

            dt = newClassFile.onm_ValidCAnCompNo(ComplaintNo);
            if (dt.Rows.Count > 0)
            {
                flg = newClassFile.SavingSigntrData(ComplaintNo, CA, FCode, ClosingRemark, OtherRemarks, IMEI, Latitude, Longitude, SignatureData);
            }
            else
            {
                flg = false;
            }
            return flg;
        }
        else
            return false;
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool UpdateIMEILocation(string IMEINO, string Latitude, string Longitude, string Remarks)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DbFunction objdbfun = new DbFunction();
            DataTable Result;
            bool flg;
            if (String.IsNullOrEmpty(Remarks))
                Remarks = "";
            String Query = String.Format(" INSERT INTO OMM_IMEI_TRACKER ( IMEINO, LATITUDE, LONGITUDE, REMARKS) VALUES ( '{0}' , {1} , {2} , '{3}' ) "
                , IMEINO, Latitude, Longitude, Remarks);
            try
            {
                if (objdbfun.dmlsinglequery_ONM(Query))
                    flg = true;
                else
                    flg = false;
            }
            catch (Exception) { flg = false; }
            return flg;
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_LINEMEN_LOGIN(string strPassword, string strIMEI_No) //Linemen Login Rajveer
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onmLinemanLoginUser(strPassword, strIMEI_No);
            dt.TableName = "onmLinemanLoginUser";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool ONM_LINEMEN_LOGOUT(string strIMEI_No) //Linemen Login Rajveer
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.onmLinemanLogoutUser(strIMEI_No);
            return _bReturn;
        }
        else
            return false;
    }


    #endregion

    #region "Breakdown BYPL By Rajveer 04042016"

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_COMPLAINT_DETAIL(string strKeyMap, string imeiNo)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onm_Complaint_Detail(imeiNo);
            dt.TableName = "onm_complaint_detail";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool onm_mark_read(string complain_no, string fault_id, string occurance_dt, string imei_no, string lm_read_flag, string lm_latitude, string lm_longitude)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return newClassFile.oms_mark_read(complain_no, fault_id, occurance_dt, imei_no, lm_read_flag, lm_latitude, lm_longitude);
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool update_onm_complaint(string complain_no, string faultid, string sbmt_latitude, string sbmt_longtude, string sbmt_remarks,
           string resolve_status, string resolve_time, string area_power_restored, string time_for_restoration, string fcode, string fault)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return newClassFile.update_onm_complaint(complain_no, faultid, sbmt_latitude, sbmt_longtude, sbmt_remarks,
                resolve_status, resolve_time, area_power_restored, time_for_restoration, fcode, fault);
        }
        else
            return (false);

    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool ONM_UpdatebreakdowndetailsHour(string BreakDownID, string FaultID, string NewHour, string NewMin) // Vulnerabilities Issue
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return newClassFile.UpdatebreakdowndetailsHourDL(BreakDownID, FaultID, NewHour, NewMin);
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetbreakdowndetailsIMEI(string imeiNo) // Not Added Due to Frequently Called Method Via Android Background Service
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.GetbreakdowndetailsIMEIDL(imeiNo);
            dt.TableName = "GetbreakdowndetailsIMEI";

            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool ONM_updatebreakdownreadstatsu(string BREAK_DOWN_ID, string FAULTID) // Vulnerabilities Issue
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return newClassFile.updatebreakdownreadstatsuDL(BREAK_DOWN_ID, FAULTID);
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable getbreakdownstatusBD(string BREAK_DOWN_ID, string faultID) // Not Added Due to Frequently Called Method Via Android Background Service
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getbreakdownstatusBDDL(BREAK_DOWN_ID, faultID);
            dt.TableName = "getbreakdownstatusBD";

            return dt;
        }
        else
            return (InvaildAuthontication());

    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_getbreakdownbackfeedlist(string BREAK_DOWN_ID)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getbreakdownbackfeedlist(BREAK_DOWN_ID);
            dt.TableName = "getbreakdownbackfeedlist";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool ONM_updatebreakdownbackfeed(string BREAK_DOWN_ID, string FAULTID, string restoreddate, string restoredload, string restoresource, string restoretype, string remarks, string selectedsource)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return newClassFile.updatebreakdownbackfeed(BREAK_DOWN_ID, FAULTID, restoreddate, restoredload, restoresource, restoretype, remarks, selectedsource);
        }
        else
            return (false);
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool ONM_CloseCableDetails(string BREAK_DOWN_ID, string FAULTID, string restoreddate, string restoredload, string restoresource, string restoretype, string remarks, string selectedsource)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return newClassFile.updatebreakdownbackfeed(BREAK_DOWN_ID, FAULTID, restoreddate, restoredload, restoresource, restoretype, remarks, selectedsource);
        }
        else
            return (false);
    }


    #endregion

    #region "Smart Complaint Centre App BRPL By Rajveer 04042016"

    //13062016
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_BurnMeterUpdate(string strComp1)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            //dt = NewClassFile.onmDashboardReport(strStartDate, strEndDate, strDivision, strCompType);
            dt.TableName = "onmDashboardReport";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    //Meter burn open complaints
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_Get_Meter_OComp_List(string distName, string complaintCentre, string status, string areaName) // Complaint Meter Open
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getOpenOnmMeterBurntComplaintlist(distName, complaintCentre, status, areaName);
            dt.TableName = "getOnmMeterBurntComplaintlist";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_Get_Meter_CComp_List(string distName, string complaintCentre, string status, string areaName) // Complaint Meter Close
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getOpenOnmMeterBurntComplaintlist(distName, complaintCentre, status, areaName);
            dt.TableName = "getOnmMeterBurntComplaintlist";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }



    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_Dashboard_Report(string strStartDate, string strEndDate, string strDivision, string strCompType)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onmDashboardReport(strStartDate, strEndDate, strDivision, strCompType);
            dt.TableName = "onmDashboardReport";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_Dashboard_OnSelect(string strStartDate, string strEndDate, string strDivCode, string strCompType, string strCompSubType)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onmDashboardOnSelect(strStartDate, strEndDate, strDivCode, strCompType, strCompSubType);  // Updated 07062016
            dt.TableName = "onmDashboardOnSelect";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    //23052016 - Rajveer

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_Dashboard_Report_Old(string strStartDate, string strEndDate, string strDivision) //Not Used
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onmDashboardReportOld(strStartDate, strEndDate, strDivision);
            dt.TableName = "onmDashboardReport";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_Dashboard_OnSelect_Old(string strStartDate, string strEndDate, string strDivCode) //Not Used
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onmDashboardOnSelectOld(strStartDate, strEndDate, strDivCode);
            dt.TableName = "onmDashboardOnSelect";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string Test_Application(string strString)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            string dt = string.Empty;
            dt = newClassFile.onmSelect(strString);
            return dt;
        }
        else
            return "Invalid Authontication";
    }

    //end 23052016

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string ONM_NCC_Registration(string _sCANo, string cboPriority, string lstFaultCatg, string txtCustRemarks,
        string cboMinutes, string cboDays, string AreaCode, string _sMobileNo)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            string chkComplClear = string.Empty;
            string dtpCompClearDate = string.Empty;
            string cboFixtue = string.Empty;
            string txtStreetNo = string.Empty;
            string txtCustPhone = string.Empty;
            string txtCustMob = string.Empty;
            string txtCustKNo = string.Empty;
            string lblAreaCode = string.Empty;
            string txtCustArea = string.Empty;
            string txtCustDistrict = string.Empty;
            string cboCallCateg = string.Empty;
            string txtCustName = string.Empty;
            string txtCustAdd1 = string.Empty;
            string txtCustAdd2 = string.Empty;
            string lblFCode = string.Empty;
            //string txtCustRemarks = string.Empty;
            string txtClearedBy = string.Empty;
            string lblTimeTaken = "0";
            string txtConsumerReferenceNo = string.Empty;
            string txtlandmark = string.Empty;
            string txtContractNo = string.Empty;
            // string cboMinutes = string.Empty;
            string txtAddlNo = string.Empty;
            // string cboDays = string.Empty;
            string optSmsYes = string.Empty;
            string optSmsNo = string.Empty;

            string _sConsumerRefNo = string.Empty;
            string gAreadiv = string.Empty;
            string txtEBSEMail = string.Empty;
            string StrConsumerType = string.Empty;
            string strDIST = string.Empty;
            string strGridCode = string.Empty;
            string strGridName = string.Empty;
            string strGridPhone = string.Empty;
            string strFeederCode = string.Empty;
            string strFeederName = string.Empty;
            string strTRCOde = string.Empty;
            string strCompCenter = string.Empty;
            string strCompCenterPhone = string.Empty;
            string strCircle = string.Empty;
            string strCompCenterForSapSerch = string.Empty;
            string UserName = "TAB".Trim();
            string strCLEAR_STATUS = "N";
            /// string strCompClearDate = "NULL";
            string VMODULENAME = "TAB".Trim();
            //  Session["ISONM"] = "ONM";

            string complaineeMobileNo = _sMobileNo;

            //try
            //{

            //    DataTable _dtAlreadyReg = new DataTable();
            //    _dtAlreadyReg = AllSelect.GetShowDetails("SELECT  cc.OPERATIONAL_COMP_NO,cc.kno  FROM ONM_FAULT_COLLECTIONS flt, CC_DVB_COMP cc WHERE flt.COMPLAINT_NO=cc.COMPNO	AND FLT.kno like '%" + _sCANo + "'	AND flt.FAULT_DUR_IN_SEC =-1 ");
            //    if (_dtAlreadyReg.Rows.Count > 0)
            //    {
            //        return ("Your complaint is already registed with complaint No. " + _dtAlreadyReg.Rows[0]["OPERATIONAL_COMP_NO"].ToString());
            //    }

            //    DataTable _dtConDetails = new DataTable();
            //    DataSet _ds = new DataSet();
            //    _dtConDetails = CA_DISPLAY(_sCANo, "", "", "", "", "");

            //    if (_dtConDetails.Rows.Count > 0)
            //    {
            //        txtConsumerReferenceNo = _dtConDetails.Rows[0]["LEGACY_ACCT"].ToString();
            //        // _sCANo = _dtConDetails.Rows[0]["CA_NUMBER"].ToString();
            //        txtCustName = _dtConDetails.Rows[0]["BP_NAME"].ToString();
            //        txtCustAdd1 = _dtConDetails.Rows[0]["HOUSE_NUMBER"].ToString() + " " + _dtConDetails.Rows[0]["STREET2"].ToString();
            //        txtCustAdd2 = _dtConDetails.Rows[0]["STREET3"].ToString();


            //        DataTable _dt = new DataTable();
            //        if (_dtConDetails.Rows[0]["REG_STR_GROUP"].ToString() != "")
            //        {
            //            _dt = AllSelect.GetShowDetails("SELECT DIV_CODE,DIV_NAME FROM DIVISION where upper(SAP_CIRCLE_DIV)='" + _dtConDetails.Rows[0]["REG_STR_GROUP"].ToString() + "'");
            //            if (_dt.Rows.Count > 0)
            //            {
            //                gAreadiv = _dt.Rows[0]["DIV_CODE"].ToString();
            //                txtCustDistrict = _dt.Rows[0]["DIV_NAME"].ToString();
            //            }
            //        }
            //        if (_dtConDetails.Rows[0]["TELEPHONE_NO"] != null)
            //        {
            //            if (_dtConDetails.Rows[0]["TELEPHONE_NO"].ToString().Split(',').Length > 0)
            //                txtCustPhone = _dtConDetails.Rows[0]["TELEPHONE_NO"].ToString().Split(',')[0].ToString();
            //            else if (_dtConDetails.Rows[0]["TELEPHONE_NO"].ToString().Split(',').Length <= 0)
            //            {
            //                if (_dtConDetails.Rows[0]["TELEPHONE_NO"].ToString().Substring(0, 1) == "9")
            //                    txtCustMob = "";
            //                else
            //                    txtCustMob = _dtConDetails.Rows[0]["TELEPHONE_NO"].ToString();
            //            }
            //        }
            //        else
            //            txtCustMob = _dtConDetails.Rows[0]["TELEPHONE_NO"].ToString();


            //        txtEBSEMail = _dtConDetails.Rows[0]["E_MAIL"].ToString(); //strSplit(67)  ''objTable.Cell(1, "E_MAIL")
            //        txtAddlNo = _dtConDetails.Rows[0]["TEL1_NUMBER"].ToString(); //strSplit(63)   ''objTable.Cell(1, "TEL1_NUMBR")
            //        txtlandmark = _dtConDetails.Rows[0]["STREET4"].ToString(); //strSplit(27)   ''objTable.Cell(1, "STREET4")
            //        if (_dtConDetails.Rows[0]["BP_TYPE"].ToString() == "Normal")
            //            StrConsumerType = "";
            //        else
            //            StrConsumerType = "VIP";

            //        if (_dtConDetails.Rows[0]["BP_TYPE"].ToString() == "KCC")
            //        {
            //            StrConsumerType = "KCC";
            //        }
            //        else
            //        {
            //            StrConsumerType = _dtConDetails.Rows[0]["BP_TYPE"].ToString();
            //        }
            //        txtCustMob = Convert.ToString(_dtConDetails.Rows[0]["TEL1_NUMBER"]);  //strSplit(63)  ''objTable.Cell(1, "TEL1_NUMBR")
            //        txtlandmark = Convert.ToString(_dtConDetails.Rows[0]["STREET4"]);// strSplit(27)  ''objTable.Cell(1, "STREET4")

            //    }
            //    else
            //    {
            //        return "Invaild CA No.";
            //    }


            //    //////string CustPhNo = string.Empty, strCompCenterForSapSerch = string.Empty;
            //    //////DataTable rsForSapSerch = new DataTable();
            //    //////string strSapSerch = string.Empty, sSMSFlag = string.Empty, sSMSSql = string.Empty, SsortCA = string.Empty;
            //    ////////DataTable rsForSapSerch = new DataTable();
            //    //////CustPhNo = "";
            //    //////strCompCenterForSapSerch = "";
            //    //////string fixture_typ = string.Empty;
            //    //////int no_fixture = 0;
            //    //////string strCallDate = "";
            //    //////string strCLEAR_STATUS = "";
            //    //////string strCompClearDate = "", StrConsumerType = "", VMODULENAME = "", strDIST = "", strFeederCode = "", strFeederName = "", strGridCode = "",
            //    //////    strGridName = "", strGridPhone = "", strTRCOde = "", strCompCenter = "", strCompCenterPhone = "", strCircle = "", UserName="";


            //    string fixture_typ = string.Empty;
            //    int no_fixture = 0;
            //    string strCallDate = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");
            //    string strOccuranceDate = DateTime.Now.ToString("dd-MMM-yyyy");
            //    string strOccuranceTime = DateTime.Now.ToString("HH:mm");
            //    strCallDate = "to_date('" + strCallDate + "','dd-Mon-yyyy hh24:mi')";
            //    string strComplaintNo = "";

            //    if (cboPriority == "STREET LIGHT")
            //    {
            //        fixture_typ = cboFixtue;
            //        int.TryParse(txtStreetNo, out no_fixture);
            //    }
            //    else
            //    {
            //        fixture_typ = "";
            //        no_fixture = 0;
            //    }

            //    string sql = "select Area_code from ONM_CONSUMER_MASTER_INFO where CA_NO like '%" + _sCANo + "'";
            //    DataTable rs = new DataTable();
            //    rs = AllSelect.GetShowDetails(sql);

            //    string _sAreaCode = string.Empty; // Modified by Rajveer Due to Delhi Gate Complaint Center Issue Setting Area Code As Null 
            //    _sAreaCode = ""; // It will throw an error unable to register..so in all cases it will ask for near by complaint centre
            //    //if (rs.Rows.Count > 0)
            //    //    _sAreaCode = rs.Rows[0]["Area_code"].ToString();
            //    //else
            //    //    _sAreaCode = "";

            //    if (_sAreaCode != "")
            //    {
            //        rs = new DataTable();
            //        sql = "SELECT distinct DIST, DIST_NAME, PRIMARY_GRID_CODE, PRIMARY_GRID_NAME, GRID_PHONENO, FEEDER_CODE,FEEDER_NAME, AREA_CODE, AREA_NAME, TR_CODE,COMPLAINT_CENTRE, COMPLAINT_CENTRE_PH_NO,COMPLAINT_CENTRE_CODE, CIRCLE  FROM ONM_TRANSFORMER_SETUP_DETAIL WHERE AREA_CODE='" + (_sAreaCode) + "'";
            //        rs = AllSelect.GetShowDetails(sql);
            //        if (rs.Rows.Count > 0)
            //        {
            //            txtCustArea = rs.Rows[0]["AREA_NAME"].ToString();
            //            txtCustDistrict = rs.Rows[0]["DIST_NAME"].ToString();
            //            lblAreaCode = rs.Rows[0]["AREA_CODE"].ToString();
            //            strDIST = rs.Rows[0]["DIST"].ToString();
            //            strGridCode = rs.Rows[0]["PRIMARY_GRID_CODE"].ToString();
            //            strGridName = rs.Rows[0]["PRIMARY_GRID_NAME"].ToString();
            //            strGridPhone = rs.Rows[0]["GRID_PHONENO"].ToString();
            //            strFeederCode = rs.Rows[0]["FEEDER_CODE"].ToString();
            //            strFeederName = rs.Rows[0]["FEEDER_NAME"].ToString();
            //            strTRCOde = rs.Rows[0]["TR_CODE"].ToString();
            //            strCompCenter = rs.Rows[0]["COMPLAINT_CENTRE"].ToString();
            //            strCompCenterPhone = rs.Rows[0]["COMPLAINT_CENTRE_PH_NO"].ToString();
            //            strCircle = rs.Rows[0]["CIRCLE"].ToString();
            //        }
            //    }
            //    else
            //    {
            //        //  return ("Unable to findout area right now! Please try again later.");

            //        // Change By Dharm 16-Jul-2014

            //        rs = new DataTable();
            //        sql = "SELECT distinct DIST, DIST_NAME, PRIMARY_GRID_CODE, PRIMARY_GRID_NAME, GRID_PHONENO, FEEDER_CODE,FEEDER_NAME, AREA_CODE, AREA_NAME, TR_CODE,COMPLAINT_CENTRE, COMPLAINT_CENTRE_PH_NO,COMPLAINT_CENTRE_CODE, CIRCLE  FROM ONM_TRANSFORMER_SETUP_DETAIL WHERE AREA_CODE='" + AreaCode.ToString().Trim() + "'";
            //        rs = AllSelect.GetShowDetails(sql);
            //        if (rs.Rows.Count > 0)
            //        {
            //            txtCustArea = rs.Rows[0]["AREA_NAME"].ToString();
            //            txtCustDistrict = rs.Rows[0]["DIST_NAME"].ToString();
            //            lblAreaCode = rs.Rows[0]["AREA_CODE"].ToString();
            //            strDIST = rs.Rows[0]["DIST"].ToString();
            //            strGridCode = rs.Rows[0]["PRIMARY_GRID_CODE"].ToString();
            //            strGridName = rs.Rows[0]["PRIMARY_GRID_NAME"].ToString();
            //            strGridPhone = rs.Rows[0]["GRID_PHONENO"].ToString();
            //            strFeederCode = rs.Rows[0]["FEEDER_CODE"].ToString();
            //            strFeederName = rs.Rows[0]["FEEDER_NAME"].ToString();
            //            strTRCOde = rs.Rows[0]["TR_CODE"].ToString();
            //            strCompCenter = rs.Rows[0]["COMPLAINT_CENTRE"].ToString();
            //            strCompCenterPhone = rs.Rows[0]["COMPLAINT_CENTRE_PH_NO"].ToString();
            //            strCircle = rs.Rows[0]["CIRCLE"].ToString();
            //        }

            //    }


            //    DataTable rsTmp = new DataTable();
            //    rsTmp = AllSelect.GetShowDetails("SELECT trim(SUBSTR(CIRCLE,1,1)||COMPLAINT_CENTRE_CODE|| DIST ||TO_Char(sysdate,'YYMMDD')) as strCompNo,COMPLAINT_CENTRE_CODE FROM ONM_TRANSFORMER_SETUP_DETAIL WHERE AREA_CODE='" + lblAreaCode + "'");
            //    if (rsTmp.Rows.Count > 0)
            //    {
            //        strComplaintNo = rsTmp.Rows[0]["strCompNo"].ToString();
            //        strCompCenterForSapSerch = rsTmp.Rows[0]["COMPLAINT_CENTRE_CODE"].ToString();
            //    }
            //    rsTmp = new DataTable();
            //    rsTmp = AllSelect.GetShowDetails("select SP_GENERATECOMPLAINTNO('" + lblAreaCode + "') from dual");
            //    if (rsTmp.Rows.Count > 0)
            //    {
            //        strComplaintNo = strComplaintNo.Trim() + rsTmp.Rows[0][0].ToString() + "P";
            //    }
            //    lblFCode = lstFaultCatg;

            //    string strsSQL = string.Empty; // Updated Rajveer 18062016
            //    strsSQL = "INSERT INTO CC_DVB_COMP(CALL_DATE,KNO,AREA_CODE,AREA,DISTRICT,CALL_CATEG,";
            //    strsSQL = strsSQL + "NAME,ADD1,ADD2,PHONE,FCODE,REMARKS,COMPNO,CUST_REMARKS,OAP_USER,CUST_STATUS,CUST_TIME_CLEAR,DVB_CLEARED_BY,DVB_STATUS,TIME_TAKEN,XEN_STATUS,PRIORITY,NOOFCOMPLAINTS,CONS_REF,OPERATIONAL_COMP_NO,CM_HOUSE,TYP_FIXTURE, NO_FIXTURE,LAND_MARK,CONSUMER_TYPE,MOB_NO, ENTRY_KNO_ID,REG_MODULE_NAME,REG_AREA, REG_AREA_CODE,SAP_CONTRACT_NO,SINCE_LAST_MINUTES,Addl_Cont_No,SINCE_LAST_DAYS) VALUES";
            //    strsSQL = strsSQL + "(" + strCallDate + ", ";
            //    strsSQL = strsSQL + " '" + _sCANo + "','" + lblAreaCode + "','" + txtCustArea + "','" + txtCustDistrict + "','NC', "; // NC  --" + cboCallCateg + "
            //    strsSQL = strsSQL + " '" + txtCustName.Trim() + "','" + txtCustAdd1.Trim() + "','" + txtCustAdd2.Trim() + "', '" + txtCustMob + "', ";
            //    strsSQL = strsSQL + " '" + lblFCode + "','" + txtCustRemarks + "','" + strComplaintNo + "','" + txtCustRemarks + "', ";
            //    strsSQL = strsSQL + " '" + UserName + "','" + strCLEAR_STATUS + "',NULL,'" + txtClearedBy + "',";
            //    strsSQL = strsSQL + "'" + strCLEAR_STATUS + "'," + lblTimeTaken + ",'N',";
            //    if (cboPriority == "EMERGENCY")

            //        strsSQL = strsSQL + " 'PCR',";
            //    else
            //        strsSQL = strsSQL + "'" + cboPriority.ToUpper() + "',";

            //    strsSQL = strsSQL + " 1,'" + txtConsumerReferenceNo.Trim() + "','" + strComplaintNo.Substring(8, 11) + "','N','" + fixture_typ + "'," + no_fixture + ", ";
            //    strsSQL = strsSQL + " '" + txtlandmark + "','" + StrConsumerType + "',";

            //    if (complaineeMobileNo != "")  //Rajveer Mobile No. 09042016
            //    {
            //        strsSQL = strsSQL + "  '" + complaineeMobileNo.Trim() + "','" + txtCustKNo.Trim() + "', ";
            //    }
            //    else
            //    {
            //        strsSQL = strsSQL + " '" + txtCustMob.Trim() + "','" + txtCustKNo.Trim() + "', ";
            //    }

            //    strsSQL = strsSQL + " '" + VMODULENAME.Trim() + "','" + txtCustArea.Trim() + "','" + lblAreaCode.Trim() + "','" + txtContractNo.Trim() + "',";
            //    strsSQL = strsSQL + " " + cboMinutes.Trim() + ",'" + txtAddlNo.Trim() + "'," + cboDays + ") ";


            //    string strOFCSQL = string.Empty;
            //    DataTable _dtFaultid = new DataTable();
            //    string strFaultID = "";
            //    _dtFaultid = AllSelect.GetShowDetails("SELECT SP_GenerateFaultId('" + strDIST + "') FROM DUAL ");
            //    if (_dtFaultid.Rows.Count > 0)
            //        strFaultID = _dtFaultid.Rows[0][0].ToString();

            //    strOFCSQL = " Insert into ONM_Fault_Collections (KNO,FaultId,Dist,AreaCode,Area,FeederCode,FeederName,GridCode,GridName,";
            //    strOFCSQL = strOFCSQL + " GridPhone,FaultType,NoOfComplaints,Child,All_Transformer_No,Name,Complaint_Status,";
            //    strOFCSQL = strOFCSQL + " Complaint_No,InsertedFrom,COMPLAINT_CENTRE,COMPLAINT_CENTRE_PH_NO,Com_Name,Com_Add1,Com_Add2,Com_Phone,Circle,";
            //    strOFCSQL = strOFCSQL + " CCRemarks,Powercut_Type,PRIORITY,FCODE,TYP_FIXTURE,NO_FIXTURE,ROAD,LAND_MARK,OCCURANCE_DT,CC_REG_BY,CC_REG_IP,CONSUMER_TYPE)";
            //    strOFCSQL = strOFCSQL + " values";
            //    strOFCSQL = strOFCSQL + " ('" + _sCANo + "','" + strFaultID + "','" + strDIST + "','" + lblAreaCode + "','" + txtCustArea + "',";
            //    strOFCSQL = strOFCSQL + "'" + strFeederCode + "','" + strFeederName + "','" + strGridCode + "','" + strGridName + "',";
            //    strOFCSQL = strOFCSQL + "'" + strGridPhone + "','DT AND BELOW',1,'0','" + strTRCOde + "','TAB', ";
            //    strOFCSQL = strOFCSQL + "'P','" + strComplaintNo + "', ";
            //    strOFCSQL = strOFCSQL + "'CC" + strFaultID + "','" + strCompCenter + "','" + strCompCenterPhone + "','" + txtCustName.Trim() + "', ";
            //    strOFCSQL = strOFCSQL + "'" + txtCustAdd1.Trim() + "','" + txtCustAdd2.Trim() + "','" + txtCustMob + "' ";

            //    //if(txtCustMob

            //    strOFCSQL = strOFCSQL + " ,'" + strCircle + "', '" + txtCustRemarks.Trim() + "','CC',";

            //    if (cboPriority == "EMERGENCY")
            //        strOFCSQL = strOFCSQL + " 'PCR',";
            //    else
            //        strOFCSQL = strOFCSQL + "'" + cboPriority + "',";

            //    strOFCSQL = strOFCSQL + "'" + lblFCode + "',";
            //    strOFCSQL = strOFCSQL + "'" + fixture_typ.Trim() + "'," + no_fixture + ",'','" + txtlandmark.Trim() + "'," + strCallDate + ",'" + UserName + "',sys_context('USERENV','IP_ADDRESS'),'" + StrConsumerType + "')";



            //    //sSMSFlag = "";
            //    //SsortCA = "";
            //    //sSMSSql = "";

            //    //if (txtCustKNo.Trim() != "")
            //    //{
            //    //    if (txtCustKNo.Trim().Length == 9)
            //    //        SsortCA = txtCustKNo.Trim();
            //    //    else if (txtCustKNo.Trim().Length == 12 && txtCustKNo.Substring(0, 3) == "000")
            //    //        SsortCA = txtCustKNo.Trim().Substring(0, 4);

            //    //    if (SsortCA != "")
            //    //    {
            //    //        if (optSmsYes=="Y")
            //    //            sSMSFlag = "Y";
            //    //        else if (optSmsNo == "N")
            //    //            sSMSFlag = "N";
            //    //        else
            //    //            sSMSFlag = "";

            //    //        if (sSMSFlag != "")
            //    //            sSMSSql = "UPDATE CONSUMER_CA_ADDRESS SET SMS_Alert='" + sSMSFlag + "' where CA_NO='" + SsortCA + "'";
            //    //        else
            //    //            sSMSSql = "";
            //    //    }
            //    //}

            //    string strOSFDSQL = string.Empty, strOOMFSQL = string.Empty;

            //    if (cboPriority == "STREET LIGHT")
            //    {
            //        strOSFDSQL = " INSERT INTO ONM_SL_FAULT_DETAILS(FAULTID) VALUES ('" + strFaultID + "')";
            //        // AllInsert.InsertBySQL(strOSFDSQL);
            //    }
            //    if (lblFCode == "F025")
            //    {
            //        strOOMFSQL = " INSERT INTO ONM_OFC_METER_FAULTS(FAULTID) VALUES ('" + strFaultID + "')";
            //        //  AllInsert.InsertBySQL(strOOMFSQL); 
            //    }

            //    string[] _smultipleQuery = new string[] { strsSQL, strOFCSQL, strOSFDSQL, strOOMFSQL };
            //    if (!AllInsert.InsertByMultipleSQL_ONM(_smultipleQuery))
            //    {
            //        return ("Not saved. Please try again.");
            //    }
            //    string lblFullComplaintNo = strComplaintNo;
            //    return strComplaintNo + ":" + strComplaintNo.Substring(8, 11);

            //}
            //catch (Exception ex)
            //{
            //    return "Unable to register right now! Please try again.";
            //    //We have reached sunset of OMS please log into IOMS for complaint registration. For further call to 01139999694 !
            //    //MessageBox.show(ex.ToString());
            //    //Interaction.MsgBox(ex.ToString(), MsgBoxStyle.SystemModal);
            //}

            return "We have reached sunset of OMS please log into IOMS for complaint registration. For further call to 01139999694 !";
        }
        else
            return ("Unauthorized Access!");

    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_Area_Code(string _strDiv)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onm_AreaCode(_strDiv.ToString().Trim().Substring(2, 3));
            dt.TableName = "Area_Code";
            return dt;
        }
        else
            return (InvaildAuthontication());

    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_LOGIN_USER(string strUser_Name, string strPassword, string strIMEI_No) //Login
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onmLoginUser(strUser_Name, strPassword, strIMEI_No);
            dt.TableName = "onmLoginUser";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_LINEMEN_USER_LIST(string strDist, string strTOImeiNo) // Linemen Details
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onmLinemenUserList(strDist, strTOImeiNo);
            dt.TableName = "onmLinemenUserList";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_Get_Complaint_List(string distName, string complaintCentre, string status, string areaName)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getOnmComplaintlist(distName, complaintCentre, status, areaName);
            dt.TableName = "getOnmComplaintlist";
            return dt;
        }
        else
            return (InvaildAuthontication());

    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool ONM_Allocate_Complaint(string ccAssignBy, string ccAssignReason,
         string ccCompRegisterNo, string ccInfPerson, string faultId)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.updateOnmAllocateComplaint(ccAssignBy, ccAssignReason, ccCompRegisterNo,
                 ccInfPerson, faultId);
            return _bReturn;
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable BD_Get_Complaint_List(string distName, string areaName)  // Incomplete
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getBDGetComplaintlist(distName, areaName);
            dt.TableName = "getBDGetComplaintlist";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_Get_UserDevice_List(string strDistCode, string strDeviceImeinNo)  // Done
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onmGetUserDeviceList(strDistCode, strDeviceImeinNo);
            dt.TableName = "onmGetUserDeviceList";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_Get_Device_Name_List(string strDiviceName, string strDistName, string strTOImeiNo)  // Done  -- Update Here Add strDist string in code also
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onmGetDeviceNameList(strDiviceName, strDistName, strTOImeiNo);
            dt.TableName = "onmGetDeviceNameList";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool ONM_Update_UserDevice_List(string userName, string userEmpId, string userStartTime,
        string userEndTime, string userEntryDate, string deviceIMEI, string password)  //Done
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.updateUserDeviceList(userName, userEmpId, userStartTime, userEndTime, userEntryDate, deviceIMEI, password);
            return _bReturn;
        }
        else
            return false;
    }

    // Street Light

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable Street_Light_AreaList(string anyInput)  // Done
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onmStreetLightAreaList(anyInput);
            dt.TableName = "onmStreetLightAreaList";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable Street_Light_AreaList_All(string anyInput)  // Done
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.onmStreetLightAreaListAll();
            dt.TableName = "onmStreetLightAreaList";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string Street_Light_Registration(string caNo, string area_Code, string fault_Code, string sarvey_Typ, string fixture_Typ, string fixture_No,
       string since_LastInMin, string since_LastInDays, string cust_Name, string cust_Add1, string cust_Add2, string cust_MbNo,
        string cust_AddlContNo, string cust_Road, string cust_Landmark, string cust_Rmks)  // Done
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //string _bReturn = string.Empty;
            //_bReturn = newClassFile.onmInsrtStreetLightComplaint(caNo, area_Code, fault_Code, sarvey_Typ, fixture_Typ, fixture_No, since_LastInMin,
            //    since_LastInDays, cust_Name, cust_Add1, cust_Add2, cust_MbNo, cust_AddlContNo, cust_Road, cust_Landmark, cust_Rmks);
            //return _bReturn;
            return "Temporarily not available.Will be back soon";
        }
        else
            return "Invalid Authorization";
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable SL_Complaint_List(string distName, string complaintCentre, string status, string areaName) // Complaint List Of SL
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getSLComplaintlist(distName, complaintCentre, status, areaName);
            dt.TableName = "getSLComplaintlist";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool SL_Complaint_Allocation(string acknowledgeBy, string acknowledgeByDesig, string scheduleDt, string areaName,
        string areaCode, string operationalCompNo, string faultId) // Complaint List
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = newClassFile.updateSLAllocateComplaint(acknowledgeBy, acknowledgeByDesig, scheduleDt,
                 areaName, areaCode, operationalCompNo, faultId);
            return _bReturn;
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable SL_Vendor_List(string distName, string complaintCentre, string status, string areaName) // Vendor List On Selection
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getSLVendorlist(distName, complaintCentre, status, areaName);
            dt.TableName = "getSLComplaintlist";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    public DataTable ONM_BurnMeterInsertSAP(string strComlaintNo, string strSerialNumberIn, string strCANumberIn, string strUserName,
        string strGroupMeter, string strMeterRemarks)
    {
        string strPMAufart = string.Empty; // ZDIV
        string strPlanPlant = string.Empty;
        string strRegioGroup = string.Empty;
        string strShortText = string.Empty;	//Device Investigation GM ONM-16060100522
        string strILA = string.Empty;	//I09
        string strMFText = string.Empty;	//Device Bypassed
        string strUserFieldCH20 = string.Empty;	// 16060100522
        string StrControkey = string.Empty;	// ZWM2
        string strSerialNumber = string.Empty;	// 000000000029005471
        string strComplaintGroup = string.Empty;	// 040
        string strCANumber = string.Empty;	// 000100006355
        string strContract = string.Empty;
        string strMFText1 = string.Empty; // Rajveer

        string strCANumberGet = string.Empty;
        string strMeterStatus = strMeterRemarks;

        strCANumberGet = strCANumberIn;
        strUserFieldCH20 = strComlaintNo;
        strSerialNumber = strSerialNumberIn;
        strComplaintGroup = "040";
        strMFText1 = strUserName;
        strMFText = "Device Bypassed";
        StrControkey = "ZWM2";

        //DELHIWSTESTD.ISUService isu = new DELHIWSTESTD.ISUService();

        DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        if (strCANumberGet.Length < 12 && strCANumberGet.Length > 0)
            strCANumberGet = strCANumberGet.PadLeft(12, '0');

        if (strSerialNumber.Length < 18 && strSerialNumber.Length > 0)
            strSerialNumber = strSerialNumber.PadLeft(18, '0');

        if (strMeterStatus == "METER COMPLETELY BURNT")
        {
            strPMAufart = "ZDIV";
            strILA = "I09";

            if (strGroupMeter == "YES".ToUpper())
                strShortText = "Device Investigation GM ONM-" + strComlaintNo;
            else
                strShortText = "Device Investigation ONM-" + strComlaintNo;

        }
        else if (strMeterStatus == "METER COMPLETELY BURNT")
        {
            strPMAufart = "ZDRP";
            strILA = "E02";
            if (strGroupMeter == "YES".ToUpper())
                strShortText = "Device Replacement GM ONM-" + strComlaintNo;
            else
                strShortText = "Device Replacement ONM-" + strComlaintNo;

        }
        else
        {
            strPMAufart = "ZDIV";
            strILA = "I08";
            strMFText = "";
            strShortText = "Device Investigation ONM-" + strComlaintNo;

        }

        ds = isu.ZBAPI_CREATESO_POST(strPMAufart, strPlanPlant, strRegioGroup, strShortText, strILA, strMFText, strUserFieldCH20, StrControkey,
            strSerialNumber, strComplaintGroup, strCANumber, strContract, strMFText1);

        if (ds.Tables["OutputTable"].Rows[0]["OrderId"].ToString().Trim() != "")
        {
            dt = ds.Tables["OutputTable"];
        }
        else if (ds.Tables["SAPDATA_ErrorDataTable"].Rows[0]["Message"].ToString().Trim() != "")
        {
            dt = ds.Tables["SAPDATA_ErrorDataTable"];
        }

        return dt;
    }

    // Emergency Complaints
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string Emergency_Registration(string caNo, string area_Code, string fault_Code, string sarvey_Typ, string fixture_Typ, string fixture_No,
       string since_LastInMin, string since_LastInDays, string cust_Name, string cust_Add1, string cust_Add2, string cust_MbNo,
        string cust_AddlContNo, string cust_Road, string cust_Landmark, string cust_Rmks)  // Done
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            string _bReturn = string.Empty;
            _bReturn = newClassFile.onmInsrtEmergencyComplaint(caNo, area_Code, fault_Code, sarvey_Typ, fixture_Typ, fixture_No, since_LastInMin,
                since_LastInDays, cust_Name, cust_Add1, cust_Add2, cust_MbNo, cust_AddlContNo, cust_Road, cust_Landmark, cust_Rmks);
            return _bReturn;
        }
        else
            return "Invalid Authorization";
    }

    #endregion

    #endregion

    #region Internal Msg App @ Rajveer

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet IMSGLoginUser(string strUserName, string strPassword, string strGCMId, string strImeiNo, string strAppVersion)
    {
        DataSet ds = new DataSet("BAPIRESULT");
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.IMsgLoginCredentials(strUserName, strPassword, strGCMId, strImeiNo, strAppVersion);
            dt.TableName = "iMsgLoginCredentials";
            ds.Tables.Add(dt);
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet IMSGCompanyMst(string strEmpRole, string strComp)
    {
        NewClassFile newClassFile = new NewClassFile();
        DataSet ds = new DataSet("NewDataSet");

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.iMsgCompanyLoad(strEmpRole, strComp);
            dt.TableName = "iMsgCompanyLoadMst";
            ds.Tables.Add(dt);
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet IMSGCompGrpMst(string strCompId, string strEmpRole)
    {
        NewClassFile newClassFile = new NewClassFile();
        DataSet ds = new DataSet("DatasetElement");

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.iMsgCompanyGrpLoad(strCompId, strEmpRole);
            dt.TableName = "iMsgCompanyGrpLoadMst";
            ds.Tables.Add(dt);
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet IMSGCompSubGrpMst(string strCompId, string strEmpRole)
    {
        NewClassFile newClassFile = new NewClassFile();
        DataSet ds = new DataSet("DatasetElement");

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.iMsgCompanySubGrpLoad(strCompId, strEmpRole);
            dt.TableName = "iMsgCompanySbGrpLoadMst";
            ds.Tables.Add(dt);
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet IMsgGCMRegistration(string strLoginId, string strGCMID, string strStatus)
    {
        NewClassFile newClassFile = new NewClassFile();
        DataSet ds = new DataSet("DatasetElement");

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            //dt = NewClassFile.IMsgLoginCredentials(strLoginId, strGCMID);
            dt.TableName = "iMsgDeptMst";
            ds.Tables.Add(dt);
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet IMsgGCMRegIds(string strComp, string strCompGrpId, string strSubCompId, string strEmpId)
    {
        DataSet ds = new DataSet("DatasetElement");
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.IMsgGCMRegIds(strComp, strCompGrpId, strSubCompId, strEmpId);
            dt.TableName = "iMsgGCMRegIds";
            ds.Tables.Add(dt);
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string IMsgUpdateLog(string strMsgId, string strMsgEmp)
    {
        NewClassFile newClassFile = new NewClassFile();

        string strReturn = string.Empty;
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            strReturn = newClassFile.IMsgUpdateLog_IM(strMsgId, strMsgEmp);
            return strReturn;
        }
        else
            return ("INVAILD AUTHENTICATION");

    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet IMSGMsgSend(string strEmpId, string strDeviceGCMID, string strEmpRole, string strMsgSDt, string strMsgEDt, string strMsgRead)
    {
        DataSet ds = new DataSet("NEWDATA");
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.IMsgMsgSend(strEmpId, strDeviceGCMID, strEmpRole, strMsgSDt, strMsgEDt, strMsgRead);
            dt.TableName = "IMSGMsgSendReq";
            ds.Tables.Add(dt);
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool IMSGMsgTextTBSend(string strNotifyId, string strTxtMsg, string strTitle, string strComp, string strCompGrpId, string strSubCompId,
        string strEmpId, string strSenderId, string strMsgStatus, string strOther)
    {
        bool _bReturn = false;
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            _bReturn = newClassFile.iMSGMSGTBSEND(strNotifyId, strTxtMsg, strTitle, strComp, strCompGrpId, strSubCompId, strEmpId, strSenderId, strMsgStatus, strOther);
            if (_bReturn)
            {
                //Send Message Directly
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();

                string msg, msgTitle, COMPANY, SUBCOMPANY, SUBGRPCOMPANY, EMPID, NOTIFY_ID, GCMID, EMPID_IN, SENDER_ID, smsStatus;
                string otherEmpId = "";

                dt = newClassFile.GetMsgTBSendOn(true);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        msg = dt.Rows[i]["MSG_DESC"].ToString();
                        msgTitle = dt.Rows[i]["MSG_TITLE"].ToString();
                        COMPANY = dt.Rows[i]["COMPANY"].ToString();
                        SUBCOMPANY = dt.Rows[i]["SUBCOMPANY"].ToString();
                        SUBGRPCOMPANY = dt.Rows[i]["SUBGRPCOMPANY"].ToString();
                        EMPID = dt.Rows[i]["EMP_SEND_TO"].ToString();
                        NOTIFY_ID = dt.Rows[i]["NOTIFY_ID"].ToString();

                        SENDER_ID = dt.Rows[i]["SENDER_ID"].ToString();
                        otherEmpId = dt.Rows[i]["OTHER"].ToString();
                        smsStatus = dt.Rows[i]["SMS_STATUS"].ToString();

                        if (smsStatus.ToUpper() == "R")
                        {
                            dt2 = newClassFile.GetListOfGCMIds(COMPANY, SUBCOMPANY, SUBGRPCOMPANY, EMPID);
                            for (int j = 0; j < dt2.Rows.Count; j++)
                            {
                                GCMID = dt2.Rows[j]["GCM_ID"].ToString();
                                EMPID_IN = dt2.Rows[j]["LOGIN_ID"].ToString();

                                if (newClassFile.SendNotificationUsingJSON(GCMID, msg, msgTitle, SENDER_ID, EMPID_IN))
                                {
                                    string msgId = newClassFile.InsertUserMsg("", msgTitle, msg, GCMID, EMPID_IN, SENDER_ID, EMPID_IN);
                                    if (msgId != "")
                                    {
                                        if (newClassFile.CheckUserbasedCount(SENDER_ID, EMPID_IN) == false)
                                        {
                                            newClassFile.InsertChatList(msgId, "", SENDER_ID, EMPID_IN);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            dt2 = newClassFile.GetListOfGCMIds(COMPANY, SUBCOMPANY, SUBGRPCOMPANY, EMPID);
                            for (int j = 0; j < dt2.Rows.Count; j++)
                            {
                                GCMID = dt2.Rows[j]["GCM_ID"].ToString();
                                EMPID_IN = dt2.Rows[j]["LOGIN_ID"].ToString();

                                if (SENDER_ID != EMPID_IN)
                                {
                                    if (newClassFile.SendNotificationUsingJSON(GCMID, msg, msgTitle, SENDER_ID, EMPID_IN))
                                    {
                                        string msgId = newClassFile.InsertUserMsg("", msgTitle, msg, GCMID, EMPID_IN, SENDER_ID, EMPID_IN);
                                        if (msgId != "")
                                        {
                                            if (newClassFile.CheckUserbasedCount(SENDER_ID, EMPID_IN) == false)
                                            {
                                                newClassFile.InsertChatList(msgId, "", SENDER_ID, EMPID_IN);
                                            }
                                            if (newClassFile.CheckUserbasedCountBilateral(SENDER_ID, EMPID_IN) == false)
                                            {
                                                newClassFile.InsertChatList(msgId, "", EMPID_IN, SENDER_ID);
                                            }
                                        }
                                    }
                                }

                            }
                        }

                        newClassFile.UpdateStatus(dt.Rows[i]["NOTIFY_ID"].ToString());

                    }
                }
            }
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet IMsgLoadEmployees(string strCompId, string strEmpRole)
    {
        DataSet ds = new DataSet("DatasetElement");
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.iMsgLoadEmplyees(strCompId, strEmpRole);
            dt.TableName = "iMsgLoadEmplyeesOutPut";
            ds.Tables.Add(dt);
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet IMSGMsgChatRoom(string strEmpId, string strOther)
    {
        DataSet ds = new DataSet("CHATROOM");
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.IMsgMsgChatList(strEmpId);
            dt.TableName = "IMSGMsgChatRoom";
            ds.Tables.Add(dt);
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }

    #endregion

    #region TMS Application Services

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable TTS_LOGIN_MOBILE(string UserName, string Password)
    {
        AllSelect allSelect = new AllSelect();

        string sql = "SELECT L.USER_ID, INITCAP( L.NAME ) as Name, L.PASSWORD, L.PHONE_NUMBER, L.IMEI_NUMBER,L.MODULE_CODE,L.PHONE_NUMBER,L.EMAIL_ID,";
        sql = sql + " R.ROLE_ID,RM.ROLE_DESC,(CASE WHEN R.ROLE_ID = 'R0013' THEN (SELECT MODULE_CODE FROM ccm.TTS_MODULE_MST MM WHERE MM.MODULE_LEADER = L.USER_ID) ELSE '' END) MODULE_NAME, ";
        sql = sql + "  (SELECT COMPANY_CODE FROM ccm.TASK_COR_SYS_TYPE WHERE CORETYPE='FUNCTION_MODULE' AND CODE=L.MODULE_CODE)COMP_CODE ";
        sql = sql + " FROM TTS_LOGIN_MASTER L,TTS_ROLE_RIGHT_MASTER R,TTS_ROLE_MST RM ";
        sql = sql + " WHERE upper(L.USER_ID)=upper('" + UserName + "') ";
        sql = sql + " and  PASSWORD=encrypt('" + Password + "') ";
        sql = sql + " AND L.USER_ID=R.USER_ID ";
        sql = sql + " AND R.ROLE_ID=RM.ROLE_ID ";
        return allSelect.TTS_Login(sql);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable TMS_TaskAllocationAction_Data(string ModuleCode, string UserID)
    {
        AllSelect allSelect = new AllSelect();

        string sql = "select e.MODULE_CODE, e.MODULE_NAME, b.TYP_F_WRK_ID,b.TYP_F_WRK_NAME,a.TASK_DESCRIPTION,a.TASK_PURPOSE_OF_CHANGE,c.PRIORITY_name  ";
        sql = sql + ",a.TASK_UNIQUE_ID,a.TASK_ASSIGNED_BY ,a.TASK_ASSIGNED_DATE,initcap(d.name) as name  ,to_char(a.ENTRY_DATE,'dd-Mon-yyyy') as NewENTRY_DATE  ";
        sql = sql + " ,(TO_DATE(SYSDATE,'dd/mm/yyyy') - TO_DATE(a.ENTRY_DATE,'dd/mm/yyyy'))  delay_days from TTS_TASK_ENTRY a, TTS_TYP_F_WRK_MST b,TTS_PRIORITY_MST c,TTS_LOGIN_MASTER d,TTS_MODULE_MST e,ccm.TTS_TASK_ALLOTMENT f  ";
        sql = sql + " where a.ACTIVE= 'Y' and a.MODULE_ID = e.MODULE_CODE  ";
        sql = sql + "  and a.TASK_TYPE_OF_WORK = b.TYP_F_WRK_ID  ";
        sql = sql + "  and a.TASK_PRIORITY_TYPE = c.PRIORITY_ID  ";
        sql = sql + "  and a.USER_ID=d.USER_ID  AND f.TASK_ID = a.TASK_UNIQUE_ID  AND f.TASK_STATUS<>'S003' ";

        if (ModuleCode.Trim() != "")
            sql = sql + "  and d.MODULE_CODE IN (SELECT DISTINCT FUNC_MODULE FROM  TTS_SAP_TEAM_MEMBERS WHERE TL_ID='" + UserID.Trim().ToUpper() + "')";
        else
            sql = sql + "  ";


        if (ModuleCode.Trim() == "01")
            sql = sql + " AND (E.MODULE_LEADER = 'MS0537' OR E.MODULE_LEADER = '41014958')";
        else if (ModuleCode.Trim() == "")
            sql = sql + " ";
        else
            sql = sql + " AND ((E.MODULE_LEADER = '" + UserID.Trim().ToUpper() + "') OR (b.TYP_F_WRK_ID='W010' AND e.MODULE_CODE='C30'))";

        sql = sql + " order by a.entry_date desc  ";
        return allSelect.TTS_Allotment(sql);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public String TTS_TASK_ACTION(string UPDATED_BY, string TASK_ALLOTED_TO, string TASK_STATUS, string TASK_FINAL_STATUS, string TASK_ID, string TASK_REMARKS)
    {
        AllSelect allSelect = new AllSelect();

        string strSQL2 = "UPDATE TTS_TASK_ALLOTMENT SET UPDATED_BY='" + UPDATED_BY + "',TASK_ALLOTED_TO='" + TASK_ALLOTED_TO + "',TASK_STATUS='" + TASK_STATUS + "',TASK_FINAL_STATUS='" + TASK_FINAL_STATUS + "'  WHERE TASK_ID='" + TASK_ID + "'";

        string strSQL = "INSERT INTO TTS_REMARKS_DETAILS (TASK_ID, USER_ID, TASK_REMARKS ) VALUES ('" + TASK_ID + "', '" + UPDATED_BY + "',  '" + TASK_REMARKS + "' )";

        string strSQL1 = "UPDATE TTS_TASK_ENTRY SET  ACTIVE  = 'N' WHERE TASK_UNIQUE_ID='" + TASK_ID + "'";

        try
        {
            string[] strSqlList = { strSQL2, strSQL, strSQL1 };
            bool isTrue = allSelect.InsertByMultipleSQLCCM(strSqlList);
            if (isTrue)
            {
                return "Task Alloted succesfully.";
            }
            else
            {
                return "Error during task Allocation . Please try after some times .";
            }
        }
        catch (Exception ex)
        {
            return "Error during task Allocation . Please try after some times ." + ex.ToString();
        }

    }



    #endregion

    #region BSES Dashboard Application

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable BDSDTotalCounts(string strStartDate, string strEndDate, string strDivision, string strKeyParam) // Open BD & SD Counts
    {
        NewClassFile newClassFile = new NewClassFile();

        if (strKeyParam != "b$e$!ntern@a|@pp")
            return (InvaildAuthontication());

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getBDSDCountlist(strStartDate, strEndDate, strDivision);
            dt.TableName = "getBDSDCountlist";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable BDSDHTLTTotalCounts(string strStartDate, string strEndDate, string strDivision, string strHTLTTyp, string strBDSDTyp) // Open BD HT/LT & SD HT/LT Counts
    {
        NewClassFile newClassFile = new NewClassFile();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getBDSDHTLTTotalCountlist(strStartDate, strEndDate, strDivision, strHTLTTyp, strBDSDTyp);
            dt.TableName = "getBDSDHTLTTotalCountlist";
            //UpdateOutputCallWidFunction(strNewId, strWebId, MethodBase.GetCurrentMethod().Name);
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable BDSDHTLTDetailsOnTotal(string strStartDate, string strEndDate, string strDivision, string strHTLTTyp, string strBDSDTyp, string strOpenClose) // Details - BD / SD - Details
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.GetBDSDHTLTDetailsOnTotal(strStartDate, strEndDate, strDivision, strHTLTTyp, strBDSDTyp, strOpenClose);
            dt.TableName = "getBDSDHTLTDetailsOnTotal";
            //UpdateOutputCallWidFunction(strNewId, strWebId, MethodBase.GetCurrentMethod().Name);
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable BDSDHTLTOnSlctAreaList(string strBDSDId, string strFeederCode) // Details - BD / SD - Details - Area List
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.GetBDSDHTLTOnSlctAreaList(strBDSDId, strFeederCode);
            dt.TableName = "getBDSDHTLTOnSlctAreaList";
            //UpdateOutputCallWidFunction(strNewId, strWebId, MethodBase.GetCurrentMethod().Name);
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable BDSDHTLTOnAreaListConsCount(string strExternalId, string strDTCode) //strExternalId -- used as bd id - Area List Consumer Count
    {

        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.GetBDSDHTLTOnAreaListConsCount(strExternalId, strDTCode);
            dt.TableName = "getBDSDHTLTOnAreaListConsCount";
            //UpdateOutputCallWidFunction(strNewId, strWebId, MethodBase.GetCurrentMethod().Name);
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable BDSDContactList(string strDivCode) // Division based Admin contacts
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.GetBDSDContactList(strDivCode);
            dt.TableName = "getBDSDContactList";
            //UpdateOutputCallWidFunction(strNewId, strWebId, MethodBase.GetCurrentMethod().Name);
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable BDSDSMSCountList(string strBDSDId) // Division based Admin contacts
    {

        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.GetBDSDSMSCountList(strBDSDId);
            dt.TableName = "getBDSDSMSCountList";
            //UpdateOutputCallWidFunction(strNewId, strWebId, MethodBase.GetCurrentMethod().Name);
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    //No Current Complaint

    #endregion

    #region Breakdown Application

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ONM_getbreakdownbackfeedlist_New(string BREAK_DOWN_ID)
    {
        NewClassFile newClassFile = new NewClassFile();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = newClassFile.getbreakdownbackfeedlistNew(BREAK_DOWN_ID);
            dt.TableName = "getbreakdownbackfeedlist";

            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    #endregion

    #region Admin Support System

    [WebMethod]
    [SoapHeader("consumer", Required = true)]
    public DataTable VSS_GetCompliantCentre_DivisionWise(string strKeyParam, string DisivionName, string DivisionID)
    {
        AllSelect allSelect = new AllSelect();
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            string sql = "SELECT COMPLAINT_CENTRE,COMPLAINT_ID FROM ASS_DIVISION_LOCATION_DT WHERE UPPER(DIVISION_NAME)='" + DisivionName + "' AND UPPER(SDO_CD)='" + DivisionID + "' ORDER BY COMPLAINT_CENTRE ";
            return allSelect.VSS_ComplaintCentre(sql);
        }
        else
            return InvaildAuthontication();
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable VSS_GetRequestType_DeptpartmentWise(string strKeyParam, string DeptparmentID)
    {
        AllSelect allSelect = new AllSelect();
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            string sql = " SELECT SUB_REQ_NAME,SUB_REQ_ID FROM ASS_SUB_REQUEST_TYPE a,ASS_REQUEST_TYPE b WHERE a.req_id=b.req_id and a.Active='Y' and b.Active='Y' and a.DEPT_ID='" + DeptparmentID + "'  order by SUB_REQ_NAME ";
            return allSelect.VSS_RequestType(sql);
        }
        else
            return InvaildAuthontication();
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable VSS_GetAssginDetails_CircleDivCompDeptWise(string strKeyParam, string CircleID, string DivID, string CompID, string DeptID)
    {
        AllSelect allSelect = new AllSelect();
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            string sql = " SELECT PROC_OWNER_ID, PROCESS_OWNER_NAME, PROC_MOBILE_NO FROM ASS_PROCESS_SPOC_ASSIGN  WHERE UPPER(CIRCLE_ID)='" + CircleID + "' AND UPPER(SDO_CD)='" + DivID + "' AND UPPER(COMPLAINT_ID)='" + CompID + "' AND UPPER(REQEST_FOR_ID)='" + DeptID + "' ORDER BY PROCESS_OWNER_NAME  ";
            return allSelect.VSS_AssignDetails(sql);
        }
        else
            return InvaildAuthontication();
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string VSS_Insert_Complaint_Data(string strKeyParam, string strCompName, string strEmailID, string strMobileNo, string strPhoneNo,
                                    string strPeriorty, string strRmk, string strFileName, string strDivID, string strCompCentreID, string strCircle,
                                     string strReqType, string strAssignTo, string strDept, string strRequestFor, string strAttachment)
    {
        string StrCompNo = string.Empty;
        string StrAssign = string.Empty;
        bool _bReturn = false;

        AllSelect allSelect = new AllSelect();
        AllInsert allInsert = new AllInsert();

        if (strKeyParam == "@$$!ntern@a|@pp")
        {

            StrCompNo = allSelect.compl_No().Rows[0][0].ToString();
            StrAssign = allSelect.AssignToID(strAssignTo).Rows[0][0].ToString();

            _bReturn = allInsert.Insert_Attachment(StrCompNo, strAttachment, strFileName);

            _bReturn = allInsert.InsertComplaintDetails(StrCompNo, strCompName, strEmailID, strMobileNo, strPhoneNo, strPeriorty, strRmk,
                                                    strFileName, strDivID, strCompCentreID, strCircle, strReqType, StrAssign, strDept, strRequestFor);
        }

        if (_bReturn == true)
            return StrCompNo;
        else
            return "false";
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool VSS_Update_Complaint_Data(string strKeyParam, string strcompno, string strCompStatusID, string strRemarks, string strUserID, string strUserName)
    {
        AllInsert allInsert = new AllInsert();

        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            bool _bReturn = false;
            _bReturn = allInsert.UpdateComplaintDetails(strcompno, strCompStatusID, strRemarks, strUserID, strUserName);
            return _bReturn;
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable VSS_GetComplaintDetails_CompWise(string strKeyParam, string CompID)
    {
        AllSelect allSelect = new AllSelect();

        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            string sql = "  SELECT  DISTINCT DEPT,a.COMP_NO,a.REMARKS TM_REMARKS,TO_CHAR(COMP_DATE,'dd/MM/YYYY') COMP_DATE,COMPLAINANT_NAME,COMPLAINANT_EMAIL, ";
            sql += " COMPLAINANT_MOB,a.REMARKS,PRIORITY_IDX,DECODE(COMP_STATUS,'YL1','FORWORD','C','CLOSE','N','PENDING STATUS',COMP_STATUS) COMP_STATUS , ";
            sql += " DIVISION_NAME division ,g.PROCESS_OWNER_NAME OWNER_NAME,SUB_REQ_NAME,TO_CHAR(c.WIP_DATE,'dd-mm-yyyy') WIP_DATE,c.REMARKS REMAR FROM ASS_COMP_MST a , ";
            sql += " ASS_DIVISION_LOCATION_DT b,ASS_PROCESS_SPOC_ASSIGN g,ASS_COMP_CLOSE c, ASS_SUB_REQUEST_TYPE r WHERE a.COMP_NO='" + CompID + "'  ";
            sql += " AND a.DIVISION=b.SDO_CD AND a.FWD_TO=g.PROC_OWNER_ID AND r.SUB_REQ_ID=REQUEST_SUB_TYPE  AND a.COMP_NO = c.COMP_NO(+) ";

            return allSelect.VSS_GetComplaintDetails(sql);
        }
        else
            return InvaildAuthontication();
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable VSS_GetRemarksDetails_ReqWise(string strKeyParam, string ReqNo)
    {
        AllSelect allSelect = new AllSelect();

        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            string sql = " SELECT USER_OWNER_NAME,TO_CHAR(ENTRY_DATE,'dd-mm-yyyy') ENTRY_DATE,TO_CHAR(WIP_DATE,'dd-mm-yyyy') WIP_DATE,REMARKS,STATUS FROM ASS_COMP_CLOSE WHERE COMP_NO='" + ReqNo + "' ORDER BY ENTRY_DATE  ";
            return allSelect.VSS_RemarkDetails(sql);
        }
        else
            return InvaildAuthontication();
    }


    #endregion

    #region Seva Kendra Web Method

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string SK_RegDistMsgTxt(string strKeyParam, string strDist)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return (newClassFile.getSK_RegDistMsgTxt(strDist));
        }
        else
            return "Invalid";
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable SK_HolidayList(string strKeyParam, string strDist)
    {
        DataTable dt = new DataTable();
        NewClassFile newClassFile = new NewClassFile();

        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            dt = newClassFile.getSK_HolidayMst(strDist);
            dt.TableName = "SK_HolidayList";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable SK_TimeSlotList(string strKeyParam, string strDist)
    {
        DataTable dt = new DataTable();
        NewClassFile newClassFile = new NewClassFile();

        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            dt = newClassFile.getSK_TimeSlot(strDist);
            dt.TableName = "SK_TimeSlotList";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public Boolean SK_RegDistStatus(string strKeyParam, string strDist)
    {
        NewClassFile newClassFile = new NewClassFile();

        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            return (newClassFile.getSK_RegDistStatus(strDist));
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string SK_RegOrderNoRating(string strKeyParam, string strOrderNo, string strRating)
    {
        NewClassFile newClassFile = new NewClassFile();
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            return (newClassFile.getSK_RegOrderNoRating(strOrderNo, strRating));
        }
        else
            return "F";
    }

    #endregion

    #region LR Application- Web service

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Login_Details(string strKeyParam, string UserID, string Password)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.MobApp_ValidLogin(UserID, Password);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Scheme_DivisionWise(string strKeyParam, string DivisionID)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.MobApp_Scheme_DivisionWise(DivisionID);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Scheme_Vendor_User(string strKeyParam, string strImeiNo)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.MobApp_Scheme_User(strImeiNo);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Scheme_Vendor_UserPassword(string strKeyParam, string strUserName, string Password)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.MobApp_Scheme_UserPassword(strUserName, Password);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Scheme_ChangePassword(string strKeyParam, string strUserName, string OldPassword, string NewPassword, string ConfrimPassword)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            DataTable _dt = newClassFile.MobApp_Scheme_GetUserDetails(strUserName);
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0][0].ToString() != OldPassword)
                    return InvaildUserPassword();
                else
                {
                    if (NewPassword.Trim() == ConfrimPassword.Trim())
                    {
                        if (newClassFile.Update_UserPassword(strUserName, NewPassword) == true)
                            return SuccessfullyUserUpdate();
                        else
                            return InvaildAuthontication();
                    }
                    else
                        return InvaildConfrimPassword();
                }
            }
            else
                return InvaildUserID();

        }
        else
        {
            return InvaildAuthontication();
        }
    }

    private DataTable SuccessfullyUserUpdate()
    {
        DataTable dterror = new DataTable();
        dterror.TableName = "AuthonticationTable";
        dterror.Columns.Add("AUTHONTICATION");
        dterror.Rows.Add("SUCCESSFULLY UPDATED");
        return dterror;
    }

    private DataTable InvaildUserID()
    {
        DataTable dterror = new DataTable();
        dterror.TableName = "AuthonticationTable";
        dterror.Columns.Add("AUTHONTICATION");
        dterror.Rows.Add("INVAILD USER ID");
        return dterror;
    }

    private DataTable InvaildUserPassword()
    {
        DataTable dterror = new DataTable();
        dterror.TableName = "AuthonticationTable";
        dterror.Columns.Add("AUTHONTICATION");
        dterror.Rows.Add("INVAILD USER OLD PASSWORD");
        return dterror;
    }
    private DataTable InvaildConfrimPassword()
    {
        DataTable dterror = new DataTable();
        dterror.TableName = "AuthonticationTable";
        dterror.Columns.Add("AUTHONTICATION");
        dterror.Rows.Add("NEW PASSWORD AND CONFRIM PASSWORD NOT MATCH");
        return dterror;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Scheme_Vendor_Login(string strKeyParam, string strUserId, string strPassword)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.MobApp_Scheme_Login(strUserId, strPassword);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Vendor_DivisionWise(string strKeyParam, string DivisionID)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.MobApp_Vendor_DivisionWise(DivisionID);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public Boolean LR_Insert_Scheme_Vendor_MobData(string strKeyParam, string DivId, string Scheme, string Vendor, string TeamCount, string MetRplOnly, string MetRelOnly,
                        string BothActMet, string MetInsQul, string PoleDbRel, string ArmCblRel, string IMEI_No, string Flag_V_S, string SelectDate, string strSubDiv, string strArmCastTp)
    {
        string _sCircleName = string.Empty;
        DataTable _dtCircle = new DataTable();
        NewClassFile newClassFile = new NewClassFile();

        string _sSno = "0";

        if (strKeyParam == "@$$!ntern@a|@pp")
        {

            _dtCircle = newClassFile.MobApp_Circle_DivisionWise(DivId);
            if (_dtCircle.Rows.Count > 0)
                _sCircleName = _dtCircle.Rows[0][0].ToString();

            if (CheckedDuplicateData(Flag_V_S, IMEI_No, Scheme, SelectDate) == "0")
            {
                return (newClassFile.Insert_Scheme_Vendor_MobData(DivId, Scheme, Vendor, TeamCount, MetRplOnly, MetRelOnly, BothActMet, MetInsQul, PoleDbRel,
                                                                    ArmCblRel, Flag_V_S, IMEI_No, _sCircleName, SelectDate, strSubDiv));
            }
            else
            {
                if (Flag_V_S == "V")
                    _sSno = CheckedDuplicateData(Flag_V_S, IMEI_No, Vendor, SelectDate);
                else
                    _sSno = CheckedDuplicateData(Flag_V_S, IMEI_No, Scheme, SelectDate);

                return (newClassFile.Update_Scheme_Vendor_MobData(_sSno, DivId, TeamCount, MetRplOnly, MetRelOnly, BothActMet, MetInsQul, PoleDbRel,
                                                                    ArmCblRel, IMEI_No, _sCircleName, SelectDate));
            }
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public Boolean LR_Insert_Scheme_Vendor_MobData1(string strKeyParam, string DivId, string Scheme, string Vendor, string TeamCount, string MetRplOnly, string MetRelOnly,
                                                  string BothActMet, string MetInsQul, string PoleDbRel, string ArmCblRel, string IMEI_No, string Flag_V_S, string SelectDate,
                                                   string strSubDiv, string strArmCastTp, string strlatitude, string strlongitude)
    {
        string _sCircleName = string.Empty;
        DataTable _dtCircle = new DataTable();
        NewClassFile newClassFile = new NewClassFile();

        string _sSno = "0";

        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            _dtCircle = newClassFile.MobApp_Circle_DivisionWise(DivId);
            if (_dtCircle.Rows.Count > 0)
                _sCircleName = _dtCircle.Rows[0][0].ToString();

            if (CheckedDuplicateData(Flag_V_S, IMEI_No, Scheme, SelectDate) == "0")
            {
                return (newClassFile.Insert_Scheme_Vendor_MobData1(DivId, Scheme, Vendor, TeamCount, MetRplOnly, MetRelOnly, BothActMet, MetInsQul, PoleDbRel,
                                                                    ArmCblRel, Flag_V_S, IMEI_No, _sCircleName, SelectDate, strSubDiv, strlatitude, strlongitude));
            }
            else
            {
                if (Flag_V_S == "V")
                    _sSno = CheckedDuplicateData(Flag_V_S, IMEI_No, Vendor, SelectDate);
                else
                    _sSno = CheckedDuplicateData(Flag_V_S, IMEI_No, Scheme, SelectDate);

                return (newClassFile.Update_Scheme_Vendor_MobData(_sSno, DivId, TeamCount, MetRplOnly, MetRelOnly, BothActMet, MetInsQul, PoleDbRel,
                                                                    ArmCblRel, IMEI_No, _sCircleName, SelectDate));
            }
        }
        else
            return false;
    }


    private string CheckedDuplicateData(string _sFlag, string _sIMEINo, string _sData, string _sDate)
    {
        NewClassFile newClassFile = new NewClassFile();
        DataTable _dtDup = newClassFile.MobApp_Check_SchemeVendor_DuplicatteData(_sFlag, _sIMEINo, _sData, _sDate);
        if (_dtDup.Rows.Count > 0)
            return _dtDup.Rows[0][0].ToString();
        else
            return "0";
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public Boolean LR_Insert_ActivityType_MobData(string strKeyParam, string MeterNo, string PoleNo, string ActivityList1, string ActivityList2, string IMEI_No,
                                                     string newMeter, string meterbox, string busbarspin, string cableuse, string seal1, string seal2,
                                                              string seal3, string div_code, string schemeno, string dbobs, string piercing, string frompole,
                                                               string topole, string flagType)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return (newClassFile.Insert_Scheme_ActivityType_MobData(MeterNo, PoleNo, ActivityList1, ActivityList2, IMEI_No, newMeter, meterbox, busbarspin, cableuse,
                                                        seal1, seal2, seal3, div_code, schemeno, dbobs, piercing, frompole, topole, flagType));
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public Boolean LR_Insert_ActivityType_MobData1(string strKeyParam, string MeterNo, string PoleNo, string ActivityList1, string ActivityList2, string IMEI_No,
                                                     string newMeter, string meterbox, string busbarspin, string cableuse, string seal1, string seal2,
                                                              string seal3, string div_code, string schemeno, string dbobs, string piercing, string frompole,
                                                               string topole, string flagType, string strlatitude, string strlongitude)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return (newClassFile.Insert_Scheme_ActivityType_MobData1(MeterNo, PoleNo, ActivityList1, ActivityList2, IMEI_No, newMeter, meterbox, busbarspin, cableuse,
                                                        seal1, seal2, seal3, div_code, schemeno, dbobs, piercing, frompole, topole, flagType, strlatitude, strlongitude));
        }
        else
            return false;
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Dashboard_DateWise(string strKeyParam, string EntryDate)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.Mobapp_LRDashborad(EntryDate);
        }
        else
        {
            return InvaildAuthontication();
        }
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Dashboard_DateDivisionWise(string strKeyParam, string EntryDate)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.Mobapp_LRDashboradDivWise(EntryDate);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Dashboard_DateSubDivisionWise(string strKeyParam, string EntryDate)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.Mobapp_LRDashboradSubDivWise(EntryDate);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public bool LR_Surv_QC_Insert(string strKeyParam, string VistDate, string Circle, string Division,
    //                                                string MeterNo, string CANo,
    //                                               string Remarks, string QCType,
    //                                               string QCSlctd,
    //                                                string Other, string activity1, string activity2)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
    //        {
    //            string strImeiNo = "";
    //            string strPoleNo = "";
    //            if (!string.IsNullOrEmpty(activity2))
    //            {
    //                if (activity2.ToString().Contains("|"))
    //                {
    //                    string[] _arrnctyp = activity2.Trim().Split('|');
    //                    strImeiNo = _arrnctyp[0].ToString();
    //                    if (!string.IsNullOrEmpty(_arrnctyp[1].ToString()))
    //                    {
    //                        strPoleNo = _arrnctyp[1].ToString();
    //                    }                        
    //                }
    //                else
    //                {
    //                    strImeiNo = activity2.ToString();
    //                }
    //            }

    //            //if (strPoleNo == "null")
    //            //        strPoleNo = null;

    //            bool _bReturn = false;
    //            NewClassFile newClassFile = new NewClassFile();
    //            _bReturn = newClassFile.Insert_LR_QCheck(VistDate.ToString(), Circle.ToString(),
    //                                            Division.ToString(), MeterNo.ToString(), CANo.ToString(),
    //                                            Remarks.ToString(),
    //                                            QCType.ToString(), QCSlctd.ToString(), Other.ToString(), activity1, strImeiNo, strPoleNo);

    //            return _bReturn;
    //        }
    //        else
    //            return (false);
    //    }
    //    else
    //        return false;
    //} //Done


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string LR_Surv_QC_Insert_New(string strKeyParam, string VistDate, string Circle, string Division,
                                                    string MeterNo, string CANo,
                                                   string Remarks, string QCType,
                                                   string QCSlctd,
                                                    string Other, string activity1, string activity2, string strSubDiv)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            if (checkConsumer(MethodBase.GetCurrentMethod().Name))
            {
                if ((Circle.ToUpper() != "-SELECT-") && (Division.ToUpper() != "-SELECT-") && (strSubDiv.ToUpper() != "-SELECT-"))
                {
                    NewClassFile newClassFile = new NewClassFile();

                    string _sOutPutData = string.Empty;

                    string _sMetreCANO = string.Empty;
                    if (MeterNo.Trim() == "")
                    {
                        if (CANo.Trim() != "")
                        {
                            _sMetreCANO = CANo;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(activity2))
                            {
                                if (activity2.ToString().Contains("|"))
                                {
                                    string[] _arrnctyp = activity2.Trim().Split('|');
                                    if (!string.IsNullOrEmpty(_arrnctyp[1].ToString()))
                                    {
                                        _sMetreCANO = _arrnctyp[1].ToString();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        _sMetreCANO = MeterNo;
                    }

                    _sOutPutData = newClassFile.Check_QC_Duplicate(_sMetreCANO.ToString());

                    if (_sOutPutData.ToString().Trim() != "0")
                    {
                        return "Record already submitted on Date:- " + _sOutPutData;
                    }
                    else
                    {
                        string strImeiNo = "";
                        string strPoleNo = "";
                        if (!string.IsNullOrEmpty(activity2))
                        {
                            if (activity2.ToString().Contains("|"))
                            {
                                string[] _arrnctyp = activity2.Trim().Split('|');
                                strImeiNo = _arrnctyp[0].ToString();
                                if (!string.IsNullOrEmpty(_arrnctyp[1].ToString()))
                                {
                                    strPoleNo = _arrnctyp[1].ToString();
                                }
                            }
                            else
                            {
                                strImeiNo = activity2.ToString();
                            }
                        }

                        bool _bReturn = false;

                        _bReturn = newClassFile.Insert_LR_QCheck(VistDate.ToString(), Circle.ToString(),
                                                        Division.ToString(), MeterNo.ToString(), CANo.ToString(),
                                                        Remarks.ToString(),
                                                        QCType.ToString(), QCSlctd.ToString(), Other.ToString(), activity1, strImeiNo, strPoleNo, strSubDiv);

                        return "Successfully Submitted";

                    }
                }
                else
                {
                    return "Not Submit Sucessfully, Please select valid Circle/Division/Subdivision ";
                }
            }
            else
                return "Not Submit Sucessfully, Please Try Again";
        }
        else
            return "Not Submit Sucessfully, Please Try Again";
    }

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataTable LR_QC_MIS(string strKeyParam, string strDivName, string strRolRght)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        NewClassFile newClassFile = new NewClassFile();
    //        return newClassFile.LR_QC_MIS_List(strDivName, strRolRght);
    //    }
    //    else
    //    {
    //        return InvaildAuthontication();
    //    }
    //} //Modified

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataTable LR_QC_CIRCLE_MIS(string strKeyParam, string strDivName, string strRolRght)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        NewClassFile newClassFile = new NewClassFile();
    //        return newClassFile.LR_QC_CIRCLE_MIS_List(strDivName, strRolRght);
    //    }
    //    else
    //    {
    //        return InvaildAuthontication();
    //    }
    //} //Modified

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataTable LR_QC_DIVISION_MIS(string strKeyParam, string strDivName, string strRolRght)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        NewClassFile newClassFile = new NewClassFile();
    //        return newClassFile.LR_QC_DIVISION_MIS_List(strDivName, strRolRght);
    //    }
    //    else
    //    {
    //        return InvaildAuthontication();
    //    }
    //} //Modified


    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataTable LR_ActivityModuleMIS(string strKeyParam, string strDivName, string strRolRght, string strDate)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        DataTable dt = new DataTable();
    //        if (strRolRght != "SELECT" && strDivName != "")
    //        {
    //            dt = NewClassFile.LR_ActivityModuleMIS_List(strRolRght, strDate);
    //            return dt;
    //        }

    //        return NewClassFile.LR_ActivityModuleMIS_List(strDivName, strRolRght, strDate);
    //    }
    //    else
    //    {
    //        return InvaildAuthontication();
    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataTable LR_ActivityModuleSchemeMIS(string strKeyParam, string strDivName, string strRolRght, string strDate)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        return NewClassFile.LR_ActivityModuleSchemeMIS_List(strDivName, strRolRght, strDate);
    //    }
    //    else
    //    {
    //        return InvaildAuthontication();
    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataTable LR_SurvellanceDivMIS(string strKeyParam, string strDivName, string strRolRght)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        NewClassFile newClassFile = new NewClassFile();
    //        return newClassFile.LR_SurvellanceDivMIS_List(strDivName, strRolRght);
    //    }
    //    else
    //    {
    //        return InvaildAuthontication();
    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataTable LR_SurvellanceCircleMIS(string strKeyParam, string strDivName, string strRolRght)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        NewClassFile newClassFile = new NewClassFile();
    //        return newClassFile.LR_SurvellanceCircleMIS_List(strDivName, strRolRght);
    //    }
    //    else
    //    {
    //        return InvaildAuthontication();
    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataTable LR_SurvellanceCircleDivMIS(string strKeyParam, string strDivName, string strRolRght)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        NewClassFile newClassFile = new NewClassFile();
    //        return newClassFile.LR_SurvellanceCircleDivMIS_List(strDivName, strRolRght);
    //    }
    //    else
    //    {
    //        return InvaildAuthontication();
    //    }
    //}


    #endregion

    #region Outage Alert

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetOutageAlertRPL(string strCANumber) // Details - BD / SD - Details
    {

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            NewClassFile newClassFile = new NewClassFile();
            dt = newClassFile.GetOutageAlertRPLDetails(strCANumber);
            dt.TableName = "GetOutageAlertRPL";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    #endregion

    #region Supply code

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool SplyCode_DefyLtr(string strKeyParam, string stCheckBoxNo)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            bool _bReturn = false;
            //_bReturn = AllInsert.UpdateComplaintDetails(stCheckBoxNo, strCompStatusID, strRemarks, strUserID, strUserName);

            //stCheckBoxNo ---- is commma seprated with check selected in android

            return _bReturn;
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool DOCLIST_NEW_UPDATE_STS(string strOrderNo, string str_001, string str_002, string str_003, string str_004, string str_005, string str_006,
                                       string str_007, string str_008, string str_009, string str_010, string str_011, string str_012, string str_013,
                           string str_014, string str_015, string str_016, string str_017, string str_018, string str_019, string str_020, string str_021, string str_022)
    {

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            string strDocStatus = string.Empty;

            //string[] OrderDocList = strOrderNo.Split(',');

            for (int i = 0; i < 22; i++)
            {
                if (i == 0)
                    strDocStatus = str_001;
                else if (i == 1)
                    strDocStatus = str_002;
                else if (i == 2)
                    strDocStatus = str_003;
                else if (i == 3)
                    strDocStatus = str_004;
                else if (i == 4)
                    strDocStatus = str_005;
                else if (i == 5)
                    strDocStatus = str_006;
                else if (i == 6)
                    strDocStatus = str_007;
                else if (i == 7)
                    strDocStatus = str_008;
                else if (i == 8)
                    strDocStatus = str_009;
                else if (i == 9)
                    strDocStatus = str_010;
                else if (i == 10)
                    strDocStatus = str_011;
                else if (i == 11)
                    strDocStatus = str_012;
                else if (i == 12)
                    strDocStatus = str_013;
                else if (i == 13)
                    strDocStatus = str_014;
                else if (i == 14)
                    strDocStatus = str_015;
                else if (i == 15)
                    strDocStatus = str_016;
                else if (i == 16)
                    strDocStatus = str_017;
                else if (i == 17)
                    strDocStatus = str_018;
                else if (i == 18)
                    strDocStatus = str_019;
                else if (i == 19)
                    strDocStatus = str_020;
                else if (i == 20)
                    strDocStatus = str_021;
                else if (i == 21)
                    strDocStatus = str_022;

                NewClassFile newClassFile = new NewClassFile();
                _bReturn = newClassFile.Insert_DOCLIST_New_Update(strOrderNo.ToString(), strDocStatus.ToString(), (i + 1).ToString());
                //_bReturn = NewClassFile.Insert_DOCLIST_New_Update(OrderDocList[0].ToString(), OrderDocList[i + 1].ToString(),(i+1).ToString());
            }

            //_bReturn = NewClassFile.Insert_DOCLIST_New_Update(strOrderNo, strDocument_Type, strDocStatus);


            return _bReturn;
        }
        else
            return (false);
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool Deficiency_DocList_Update(string strOrderNo, string D1, string D2, string D3, string D4, string D5, string D6, string D7, string D8, string D9,
                               string D10, string D11, string D12, string D13, string D14, string D15, string D16, string D17, string D18, string D19,
                               string D20, string D21, string D22, string D23, string D24)
    {

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            string strDocID = string.Empty;
            // string[] OrderDocList = strOrderNo.Split(',');

            for (int i = 0; i < 24; i++)
            {
                if (i == 0)
                    strDocID = D1;
                else if (i == 1)
                    strDocID = D2;
                else if (i == 2)
                    strDocID = D3;
                else if (i == 3)
                    strDocID = D4;
                else if (i == 4)
                    strDocID = D5;
                else if (i == 5)
                    strDocID = D6;
                else if (i == 6)
                    strDocID = D7;
                else if (i == 7)
                    strDocID = D8;
                else if (i == 8)
                    strDocID = D9;
                else if (i == 9)
                    strDocID = D10;
                else if (i == 10)
                    strDocID = D11;
                else if (i == 11)
                    strDocID = D12;
                else if (i == 12)
                    strDocID = D13;
                else if (i == 13)
                    strDocID = D14;
                else if (i == 14)
                    strDocID = D15;
                else if (i == 15)
                    strDocID = D16;
                else if (i == 16)
                    strDocID = D17;
                else if (i == 17)
                    strDocID = D18;
                else if (i == 18)
                    strDocID = D19;
                else if (i == 19)
                    strDocID = D20;
                else if (i == 20)
                    strDocID = D21;
                else if (i == 21)
                    strDocID = D22;
                else if (i == 22)
                    strDocID = D23;
                else if (i == 23)
                    strDocID = D24;

                NewClassFile newClassFile = new NewClassFile();
                _bReturn = newClassFile.Insert_Deficiency_Update(strOrderNo.ToString(), strDocID.ToString());
                //_bReturn = NewClassFile.Insert_Deficiency_Update(OrderDocList[0].ToString(), OrderDocList[i+1].ToString());
            }


            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool SUPPLY_CODE_NEW_CONN_IMG(string strOrderNo, string strOtherImg1, string strOtherImg2, string strOtherImg3, string strOtherImg4)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            NewClassFile newClassFile = new NewClassFile();
            _bReturn = newClassFile.SUPPLY_CODE_NEW_CONN_IMG(strOrderNo, strOtherImg1, strOtherImg2, strOtherImg3, strOtherImg4);
            return _bReturn;
        }
        else
            return (false);
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool SUPPLY_CODE_NEW_CONN_DATA(string strOrderNo, string strAppliedPortion, string strDwellingUnit, string strResonForNewMtr, string strDPCCClrncRqd,
        string strSStnSpaceAvail, string strTotalLoadPlot, string strTotalNoConn, string strDTMeter, string strDTCode)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            NewClassFile newClassFile = new NewClassFile();
            _bReturn = newClassFile.SUPPLY_CODE_NEW_CONN_DATA(strOrderNo, strAppliedPortion, strDwellingUnit, strResonForNewMtr, strDPCCClrncRqd,
        strSStnSpaceAvail, strTotalLoadPlot, strTotalNoConn, strDTMeter, strDTCode);
            return _bReturn;
        }
        else
            return (false);
    }

    #endregion

    #region "LR_SURVEILLANCE"

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataTable LR_ObservationList(string strKeyParam, string strDivName)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        NewClassFile newClassFile = new NewClassFile();
    //        return newClassFile.Mobint_LRObservationList(strDivName);
    //    }
    //    else
    //    {
    //        return InvaildAuthontication();
    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public bool LR_Surveillance_Insert(string strKeyParam, string ObserType, string VistDate, string Circle, string Division, string MeterNo, string CANo,
    //                                               string PoleNo, string ActionTkFlg, string Remarks, string NCType, string TypeOfAbnormality, string SiteAddress,
    //                                                string AdjMeterNo1, string AdjMeterNo2, string NearPoleNo, string Other, string activity1, string activity2)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
    //        {
    //            bool _bReturn = false;

    //            NewClassFile newClassFile = new NewClassFile();
    //            _bReturn = newClassFile.Insert_LR_Surveillane(ObserType.ToString(), VistDate.ToString(), Circle.ToString(), Division.ToString(), MeterNo.ToString(), CANo.ToString(),
    //                                            PoleNo.ToString(), ActionTkFlg.ToString(), Remarks.ToString(), NCType.ToString(), TypeOfAbnormality.ToString(),
    //                                            SiteAddress.ToString(), AdjMeterNo1.ToString(), AdjMeterNo2.ToString(), NearPoleNo.ToString(), Other.ToString(), activity2.ToString());

    //            return _bReturn;
    //        }
    //        else
    //            return (false);
    //    }
    //    else
    //        return false;
    //}


    [WebMethod]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Scheme_SubClusterMapping(string strKeyParam, string subdivision)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.MobApp_Cluster_Mapping(subdivision);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string LR_Surveillance_Insert_New(string strKeyParam, string ObserType, string VistDate, string Circle, string Division, string MeterNo, string CANo,
                                                   string PoleNo, string ActionTkFlg, string Remarks, string NCType, string TypeOfAbnormality, string SiteAddress,
                                                    string AdjMeterNo1, string AdjMeterNo2, string NearPoleNo, string Other, string activity1,
                                            string activity2, string strSubDiv, string strSubCluster)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            if (checkConsumer(MethodBase.GetCurrentMethod().Name))
            {
                if ((Circle.ToUpper() != "-SELECT-") && (Division.ToUpper() != "-SELECT-") && (strSubDiv.ToUpper() != "-SELECT-"))
                {
                    bool _bReturn = false;

                    NewClassFile newClassFile = new NewClassFile();

                    string _sOutPutData = string.Empty;

                    string _sMetreCANO = string.Empty;
                    if (MeterNo.Trim() == "")
                    {
                        if (CANo.Trim() != "")
                        {
                            _sMetreCANO = CANo;
                        }
                        else if (PoleNo.Trim() != "")
                        {
                            _sMetreCANO = PoleNo;
                        }
                    }
                    else
                    {
                        _sMetreCANO = MeterNo;
                    }

                    _sOutPutData = newClassFile.Check_Surveillance_Duplicate(_sMetreCANO);

                    if (_sOutPutData.ToString().Trim() != "0")
                    {
                        return "Record already submitted on Date:- " + _sOutPutData;
                    }
                    else
                    {
                        _bReturn = newClassFile.Insert_LR_Surveillane(ObserType.ToString(), VistDate.ToString(), Circle.ToString(), Division.ToString(), MeterNo.ToString(), CANo.ToString(),
                                                        PoleNo.ToString(), ActionTkFlg.ToString(), Remarks.ToString(), NCType.ToString(), TypeOfAbnormality.ToString(),
                                                        SiteAddress.ToString(), AdjMeterNo1.ToString(), AdjMeterNo2.ToString(), NearPoleNo.ToString(), Other.ToString(), activity2.ToString(), strSubDiv, strSubCluster);

                        return "Successfully Submitted";
                    }
                }
                else
                {
                    return "Not Submit Sucessfully, Please select valid Circle/Division/Subdivision ";
                }
            }
            else
                return "Not Submit Sucessfully, Please Try Again";
        }
        else
            return "Not Submit Sucessfully, Please Try Again";
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string LR_Surveillance_Insert_New1(string strKeyParam, string ObserType, string VistDate, string Circle, string Division, string MeterNo, string CANo,
                                                   string PoleNo, string ActionTkFlg, string Remarks, string NCType, string TypeOfAbnormality, string SiteAddress,
                                                    string AdjMeterNo1, string AdjMeterNo2, string NearPoleNo, string Other, string activity1,
                                            string activity2, string strSubDiv, string strSubCluster, string strlatitude, string strlongitude)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            if (checkConsumer(MethodBase.GetCurrentMethod().Name))
            {
                if ((Circle.ToUpper() != "-SELECT-") && (Division.ToUpper() != "-SELECT-") && (strSubDiv.ToUpper() != "-SELECT-"))
                {
                    bool _bReturn = false;
                    NewClassFile newClassFile = new NewClassFile();
                    string _sOutPutData = string.Empty;
                    string _sMetreCANO = string.Empty;
                    if (MeterNo.Trim() == "")
                    {
                        if (CANo.Trim() != "")
                        {
                            _sMetreCANO = CANo;
                        }
                        else if (PoleNo.Trim() != "")
                        {
                            _sMetreCANO = PoleNo;
                        }
                    }
                    else
                    {
                        _sMetreCANO = MeterNo;
                    }

                    _sOutPutData = newClassFile.Check_Surveillance_Duplicate(_sMetreCANO);

                    if (_sOutPutData.ToString().Trim() != "0")
                    {
                        return "Record already submitted on Date:- " + _sOutPutData;
                    }
                    else
                    {
                        _bReturn = newClassFile.Insert_LR_Surveillane1(ObserType.ToString(), VistDate.ToString(), Circle.ToString(), Division.ToString(), MeterNo.ToString(), CANo.ToString(),
                                                        PoleNo.ToString(), ActionTkFlg.ToString(), Remarks.ToString(), NCType.ToString(), TypeOfAbnormality.ToString(),
                                                        SiteAddress.ToString(), AdjMeterNo1.ToString(), AdjMeterNo2.ToString(), NearPoleNo.ToString(), Other.ToString(),
                                                        activity2.ToString(), strSubDiv, strSubCluster, strlatitude, strlongitude);

                        return "Successfully Submitted";
                    }
                }
                else
                {
                    return "Not Submit Sucessfully, Please select valid Circle/Division/Subdivision ";
                }
            }
            else
                return "Not Submit Sucessfully, Please Try Again";
        }
        else
            return "Not Submit Sucessfully, Please Try Again";
    }

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public string LR_Surveillance_Insert_New(string strKeyParam, string ObserType, string VistDate, string Circle, string Division, string MeterNo, string CANo,
    //                                               string PoleNo, string ActionTkFlg, string Remarks, string NCType, string TypeOfAbnormality, string SiteAddress,
    //                                                string AdjMeterNo1, string AdjMeterNo2, string NearPoleNo, string Other, string activity1, string activity2, string strSubDiv)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
    //        {
    //            if ((Circle.ToUpper() != "-SELECT-") && (Division.ToUpper() != "-SELECT-") && (strSubDiv.ToUpper() != "-SELECT-"))
    //            {
    //                bool _bReturn = false;

    //                NewClassFile newClassFile = new NewClassFile();

    //                string _sOutPutData = string.Empty;

    //                string _sMetreCANO = string.Empty;
    //                if (MeterNo.Trim() == "")
    //                {
    //                    if (CANo.Trim() != "")
    //                    {
    //                        _sMetreCANO = CANo;
    //                    }
    //                    else if (PoleNo.Trim() != "")
    //                    {
    //                        _sMetreCANO = PoleNo;
    //                    }
    //                }
    //                else
    //                {
    //                    _sMetreCANO = MeterNo;
    //                }

    //                _sOutPutData = newClassFile.Check_Surveillance_Duplicate(_sMetreCANO);

    //                if (_sOutPutData.ToString().Trim() != "0")
    //                {
    //                    return "Record already submitted on Date:- " + _sOutPutData;
    //                }
    //                else
    //                {
    //                    _bReturn = newClassFile.Insert_LR_Surveillane(ObserType.ToString(), VistDate.ToString(), Circle.ToString(), Division.ToString(), MeterNo.ToString(), CANo.ToString(),
    //                                                    PoleNo.ToString(), ActionTkFlg.ToString(), Remarks.ToString(), NCType.ToString(), TypeOfAbnormality.ToString(),
    //                                                    SiteAddress.ToString(), AdjMeterNo1.ToString(), AdjMeterNo2.ToString(), NearPoleNo.ToString(), Other.ToString(), activity2.ToString(), strSubDiv);

    //                    return "Successfully Submitted";
    //                }
    //            }
    //            else
    //            {
    //                return "Not Submit Sucessfully, Please select valid Circle/Division/Subdivision ";
    //            }
    //        }
    //        else
    //            return "Not Submit Sucessfully, Please Try Again";
    //    }
    //    else
    //        return "Not Submit Sucessfully, Please Try Again";
    //}


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool LR_Surv_ATR_Insert(string strKeyParam, string ObserId, string ObserType, string Remarks, string NCTypeResolved, string NCTypeNotResolved, string TypOfAbnormResolv, string TypOfAbnormNtResolv, string activity1, string activity2, string enf_Case_ID)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            if (checkConsumer(MethodBase.GetCurrentMethod().Name))
            {
                bool _bReturn = false;

                string[] str = activity1.Split(',');

                NewClassFile newClassFile = new NewClassFile();

                for (int i = 0; i < str.Length; i++)
                {
                    _bReturn = newClassFile.Insert_LR_Surv_Atr(ObserId, ObserType.ToString(), Remarks, NCTypeResolved, NCTypeNotResolved, TypOfAbnormResolv, TypOfAbnormNtResolv, str[i], activity2, enf_Case_ID);
                }

                return _bReturn;
            }
            else
                return (false);
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool LR_Surv_ATR_Insert1(string strKeyParam, string ObserId, string ObserType, string Remarks, string NCTypeResolved, string NCTypeNotResolved,
                                    string TypOfAbnormResolv, string TypOfAbnormNtResolv, string activity1, string activity2, string enf_Case_ID,
                                    string strlatitude, string strlongitude, string leadRepeated)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            if (checkConsumer(MethodBase.GetCurrentMethod().Name))
            {
                bool _bReturn = false;

                string[] str = activity1.Split(',');

                NewClassFile newClassFile = new NewClassFile();

                for (int i = 0; i < str.Length; i++)
                {
                    _bReturn = newClassFile.Insert_LR_Surv_Atr1(ObserId, ObserType.ToString(), Remarks, NCTypeResolved, NCTypeNotResolved, TypOfAbnormResolv,
                                                            TypOfAbnormNtResolv, str[i], activity2, enf_Case_ID, strlatitude, strlongitude, leadRepeated);
                }

                return _bReturn;
            }
            else
                return (false);
        }
        else
            return false;
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_ObservationList_New(string strKeyParam, string strDivName, string strRolRght, string strSubDiv, string strSubCluster)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            string[] str = strRolRght.Split(',');
            DataTable _dtOutput = new DataTable();
            DataTable _dtFinData = new DataTable();

            NewClassFile newClassFile = new NewClassFile();

            for (int i = 0; i < str.Length; i++)
            {
                _dtOutput = newClassFile.Mobint_LRObservationList(strSubDiv, strDivName, str[i], strSubCluster);
                _dtFinData.Merge(_dtOutput);
            }

            _dtFinData.AcceptChanges();
            _dtFinData.TableName = "LR_OBSERVATION";
            return _dtFinData;

        }
        else
        {
            return InvaildAuthontication();
        }
    }

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataTable LR_ObservationList_New(string strKeyParam, string strDivName, string strRolRght, string strSubDiv, string strSubCluster)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        string[] str = strRolRght.Split(',');
    //        DataTable _dtOutput = new DataTable();
    //        DataTable _dtFinData = new DataTable();

    //        NewClassFile newClassFile = new NewClassFile();

    //        for (int i = 0; i < str.Length; i++)
    //        {
    //            _dtOutput = newClassFile.Mobint_LRObservationList(strSubDiv, strDivName, str[i], strSubCluster);
    //            _dtFinData.Merge(_dtOutput);
    //        }

    //        _dtFinData.AcceptChanges();
    //        _dtFinData.TableName = "LR_OBSERVATION";
    //        return _dtFinData;

    //    }
    //    else
    //    {
    //        return InvaildAuthontication();
    //    }
    //}
    #endregion

    #region MCR Punching

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable MCR_GetUserLoginDetails(string strUser, string strPassword)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = NewClassFile.CheckValid_UserID_DTDetails(strUser, strPassword);
            dt.TableName = "MCR_USER_DETAILS";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable MCR_GetUserMCR_INPUT_DT(string strUserType, string strUser, string stringLatitude, string stringLongitude)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = NewClassFile.Get_MCR_InputData_Details(strUserType, strUser, stringLatitude, stringLongitude);
            dt.TableName = "MCR_INPUT_DETAILS";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable MCR_GetUserMCR_INPUT_DTNEW(string strUserType, string strUser, string stringLatitude, string stringLongitude)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = NewClassFile.Get_MCR_InputData_DetailsNew(strUserType, strUser, stringLatitude, stringLongitude);
            dt.TableName = "MCR_INPUT_DETAILS";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    private string ReplaceSelect(string _sVal)
    {
        if (_sVal.ToUpper() == "SELECT")
            return "";
        else
            return _sVal;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public Boolean MCR_Insert_INPUT_Data(string ORDERNO, string DEVICENO, string OTHERSTICKER, string OTHER4, string INSTALLEDBUSBAR, string BUSBARSIZE,
                    string BUSBARNUMBER, string DRUMSIZE, string OTHER5, string CABLESIZE2, string CABLEINSTALLTYPE, string RUNNINGLENGTHFROM, string RUNNINGLENGTHTO,
                    string CABLELENGTH, string TERMINALSEAL1, string TERMINALSEAL2, string METERBOXSEAL1, string METERBOXSEAL2, string BUSBARSEAL1,
                    string BUSBARSEAL2, string INSTALLEDLOCATION, string POLENUMBER, string OTHER6, string OTHER7, string OTHER8, string OTHER9, string OTHER10, string OTHER11,
                    string TAKEPHOTOGRAPH, string METERDOWNLOAD, string DBLOCKED, string EARTHING, string HEIGHTOFMETER, string ANYJOINTS, string OVERHEADCABLE,
                    string OVERHEADCABLEPOLE, string FLOWMADE, string ADDITIONALACCESSORIES, string IMAGE1, string IMAGE2, string IMAGE3,
                    string IMEAGE_MCR, string IMAGE_METERTESTREPORT, string IMAGE_LABTESTINGREPORT, string IMAGE_SIGNATURE,
                    string OUTPUTBUSLENGTH, string OUTPUTCABLELENGTH, string EARTHINGPOLE, string IMAGE4, string PUNCHED,
                    string TAB_ID, string TAB_NAME, string GIS_LAT, string GIS_LONG, string GIS_STATUS, string IMEI_NO)
    {

        Boolean _bSave = false;

        TERMINALSEAL1 = ReplaceSelect(TERMINALSEAL1);
        TERMINALSEAL2 = ReplaceSelect(TERMINALSEAL2);
        METERBOXSEAL1 = ReplaceSelect(METERBOXSEAL1);
        METERBOXSEAL2 = ReplaceSelect(METERBOXSEAL2);
        BUSBARSEAL1 = ReplaceSelect(BUSBARSEAL1);
        BUSBARSEAL2 = ReplaceSelect(BUSBARSEAL2);

        if (METERBOXSEAL2.Trim() == BUSBARSEAL1.Trim())
            BUSBARSEAL1 = "";

        _bSave = NewClassFile.Insert_MCR_InputData(ORDERNO, DEVICENO, OTHERSTICKER, OTHER4, INSTALLEDBUSBAR, BUSBARSIZE, BUSBARNUMBER, DRUMSIZE, OTHER5, CABLESIZE2, CABLEINSTALLTYPE, RUNNINGLENGTHFROM, RUNNINGLENGTHTO,
                                                CABLELENGTH, TERMINALSEAL1, TERMINALSEAL2, METERBOXSEAL1, METERBOXSEAL2, BUSBARSEAL1, BUSBARSEAL2, INSTALLEDLOCATION, POLENUMBER, OTHER6,
                                                OTHER7, OTHER8, OTHER9, OTHER10, TAKEPHOTOGRAPH, METERDOWNLOAD, DBLOCKED, EARTHING, HEIGHTOFMETER, ANYJOINTS, OVERHEADCABLE, OVERHEADCABLEPOLE, FLOWMADE,
                                                ADDITIONALACCESSORIES, OUTPUTBUSLENGTH, OUTPUTCABLELENGTH, EARTHINGPOLE, PUNCHED,
                                                TAB_ID, TAB_NAME, GIS_LAT, GIS_LONG, GIS_STATUS, IMEI_NO);
        if (_bSave == true)
            NewClassFile.Update_MCRSealFlag(TERMINALSEAL1, ORDERNO);

        if (TERMINALSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(TERMINALSEAL2, ORDERNO);
        }
        if (METERBOXSEAL1 != "")
        {
            NewClassFile.Update_MCRSealFlag(METERBOXSEAL1, ORDERNO);
        }
        if (METERBOXSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(METERBOXSEAL2, ORDERNO);
        }
        if (BUSBARSEAL1 != "")
        {
            NewClassFile.Update_MCRSealFlag(BUSBARSEAL1, ORDERNO);
        }
        if (BUSBARSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(BUSBARSEAL2, ORDERNO);
        }

        string strCaNo = NewClassFile.GetCAOnOrderNo(ORDERNO);

        NewClassFile.Insert_MCR_IamgeList(ORDERNO, OTHERSTICKER, DEVICENO, IMAGE1, IMAGE2, IMAGE3,
                                           IMEAGE_MCR, IMAGE_METERTESTREPORT, IMAGE_LABTESTINGREPORT,
                                           IMAGE_SIGNATURE, IMAGE4, strCaNo);
        if (_bSave == true)
            _bSave = NewClassFile.PunchOrder_Installer_InputData(ORDERNO, OTHER11, DEVICENO);

        return _bSave;
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public Boolean MCR_Insert_INPUT_DataEXTRA7(string ORDERNO, string DEVICENO, string STICKERNO, string OTHERSTICKER, string OTHER4, string INSTALLEDBUSBAR, string BUSBARSIZE,
                    string BUSBARNUMBER, string DRUMSIZE, string OTHER5, string CABLESIZE2, string CABLEINSTALLTYPE, string RUNNINGLENGTHFROM, string RUNNINGLENGTHTO,
                    string CABLELENGTH, string TERMINALSEAL1, string TERMINALSEAL2, string METERBOXSEAL1, string METERBOXSEAL2, string BUSBARSEAL1,
                    string BUSBARSEAL2, string INSTALLEDLOCATION, string POLENUMBER, string OTHER6, string OTHER7, string OTHER8, string OTHER9, string OTHER10, string OTHER11,
                    string TAKEPHOTOGRAPH, string METERDOWNLOAD, string DBLOCKED, string EARTHING, string HEIGHTOFMETER, string ANYJOINTS, string OVERHEADCABLE,
                    string OVERHEADCABLEPOLE, string FLOWMADE, string ADDITIONALACCESSORIES, string IMAGE1, string IMAGE2, string IMAGE3,
                    string IMEAGE_MCR, string IMAGE_METERTESTREPORT, string IMAGE_LABTESTINGREPORT, string IMAGE_SIGNATURE,
                    string OUTPUTBUSLENGTH, string OUTPUTCABLELENGTH, string EARTHINGPOLE, string IMAGE4, string PUNCHED,
                    string TAB_ID, string TAB_NAME, string GIS_LAT, string GIS_LONG, string GIS_STATUS, string IMEI_NO, string SUBMIT_DATETIME,
                    string DRUMNUMBERBB, string VALINSTALLTYPEBB, string RUNNINGLENGTHFROMBB, string RUNNINGLENGTHTOBB, string ELCBSUBMITVAL, string METERSCANNEDVAL, string EXTRA1,
                    string EXTRA2, string EXTRA3, string EXTRA4, string EXTRA5, string EXTRA6, string EXTRA7)
    {
        Boolean _bSave = false;

        TERMINALSEAL1 = ReplaceSelect(TERMINALSEAL1);
        TERMINALSEAL2 = ReplaceSelect(TERMINALSEAL2);
        METERBOXSEAL1 = ReplaceSelect(METERBOXSEAL1);
        METERBOXSEAL2 = ReplaceSelect(METERBOXSEAL2);
        BUSBARSEAL1 = ReplaceSelect(BUSBARSEAL1);
        BUSBARSEAL2 = ReplaceSelect(BUSBARSEAL2);

        DataTable _dtOrderData = new DataTable();
        _dtOrderData = GetOrdType_PMAct_OrderWise(ORDERNO);

        string _sSAPFLAG = "N";
        string _sDivision = string.Empty, ORDER_TYPE = string.Empty, PM_ACTIVITY = string.Empty;

        if (_dtOrderData.Rows.Count > 0)
        {
            ORDER_TYPE = _dtOrderData.Rows[0]["AUART"].ToString();
            PM_ACTIVITY = _dtOrderData.Rows[0]["ILART_ACTIVITY_TYPE"].ToString();

            if (_dtOrderData.Rows[0]["SAP_FLAG"] != null)
            {
                if (_dtOrderData.Rows[0]["SAP_FLAG"].ToString().ToUpper() == "N")
                    _sSAPFLAG = "Z";
            }
            if (_dtOrderData.Rows[0]["DIVISION"] != null)
            {
                _sDivision = _dtOrderData.Rows[0]["DIVISION"].ToString();
            }
        }

        if (METERBOXSEAL2.Trim() == BUSBARSEAL1.Trim())
            BUSBARSEAL1 = "";

        _bSave = NewClassFile.Insert_MCR_InputDataEXTRA7(ORDERNO, DEVICENO, STICKERNO, OTHERSTICKER, OTHER4, INSTALLEDBUSBAR, BUSBARSIZE, BUSBARNUMBER, DRUMSIZE, OTHER5, CABLESIZE2, CABLEINSTALLTYPE, RUNNINGLENGTHFROM, RUNNINGLENGTHTO,
                                                 CABLELENGTH, TERMINALSEAL1, TERMINALSEAL2, METERBOXSEAL1, METERBOXSEAL2, BUSBARSEAL1, BUSBARSEAL2, INSTALLEDLOCATION, POLENUMBER, OTHER6,
                                                 OTHER7, OTHER8, OTHER9, OTHER10, TAKEPHOTOGRAPH, METERDOWNLOAD, DBLOCKED, EARTHING, HEIGHTOFMETER, ANYJOINTS, OVERHEADCABLE, OVERHEADCABLEPOLE, FLOWMADE,
                                                 ADDITIONALACCESSORIES, OUTPUTBUSLENGTH, OUTPUTCABLELENGTH, EARTHINGPOLE, PUNCHED,
                                                 TAB_ID, TAB_NAME, GIS_LAT, GIS_LONG, GIS_STATUS, IMEI_NO, SUBMIT_DATETIME, DRUMNUMBERBB, VALINSTALLTYPEBB,
                                                 RUNNINGLENGTHFROMBB, RUNNINGLENGTHTOBB, ELCBSUBMITVAL, METERSCANNEDVAL, EXTRA1, EXTRA2, EXTRA3, EXTRA4,
                                                 EXTRA5, EXTRA6, EXTRA7, ORDER_TYPE, PM_ACTIVITY, _sDivision);
        if (_bSave == true)
            NewClassFile.Update_MCRSealFlag(TERMINALSEAL1, ORDERNO);

        if (TERMINALSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(TERMINALSEAL2, ORDERNO);
        }
        if (METERBOXSEAL1 != "")
        {
            NewClassFile.Update_MCRSealFlag(METERBOXSEAL1, ORDERNO);
        }
        if (METERBOXSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(METERBOXSEAL2, ORDERNO);
        }
        if (BUSBARSEAL1 != "")
        {
            NewClassFile.Update_MCRSealFlag(BUSBARSEAL1, ORDERNO);
        }
        if (BUSBARSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(BUSBARSEAL2, ORDERNO);
        }

        string strCaNo = NewClassFile.GetCAOnOrderNo(ORDERNO);

        NewClassFile.Insert_MCR_IamgeList(ORDERNO, OTHERSTICKER, DEVICENO, IMAGE1, IMAGE2, IMAGE3,
                                           IMEAGE_MCR, IMAGE_METERTESTREPORT, IMAGE_LABTESTINGREPORT,
                                           IMAGE_SIGNATURE, IMAGE4, strCaNo);

        if (_bSave == true)
            _bSave = NewClassFile.PunchOrder_Installer_InputData(ORDERNO, OTHER11, DEVICENO);

        return _bSave;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public Boolean MCR_Insert_INPUT_DataNEW(string ORDERNO, string DEVICENO, string OTHERSTICKER, string OTHER4, string INSTALLEDBUSBAR, string BUSBARSIZE,
                    string BUSBARNUMBER, string DRUMSIZE, string OTHER5, string CABLESIZE2, string CABLEINSTALLTYPE, string RUNNINGLENGTHFROM, string RUNNINGLENGTHTO,
                    string CABLELENGTH, string TERMINALSEAL1, string TERMINALSEAL2, string METERBOXSEAL1, string METERBOXSEAL2, string BUSBARSEAL1,
                    string BUSBARSEAL2, string INSTALLEDLOCATION, string POLENUMBER, string OTHER6, string OTHER7, string OTHER8, string OTHER9, string OTHER10, string OTHER11,
                    string TAKEPHOTOGRAPH, string METERDOWNLOAD, string DBLOCKED, string EARTHING, string HEIGHTOFMETER, string ANYJOINTS, string OVERHEADCABLE,
                    string OVERHEADCABLEPOLE, string FLOWMADE, string ADDITIONALACCESSORIES, string IMAGE1, string IMAGE2, string IMAGE3,
                    string IMEAGE_MCR, string IMAGE_METERTESTREPORT, string IMAGE_LABTESTINGREPORT, string IMAGE_SIGNATURE,
                    string OUTPUTBUSLENGTH, string OUTPUTCABLELENGTH, string EARTHINGPOLE, string IMAGE4, string PUNCHED,
                    string TAB_ID, string TAB_NAME, string GIS_LAT, string GIS_LONG, string GIS_STATUS, string IMEI_NO, string SUBMIT_DATETIME,
                    string DRUMNUMBERBB, string VALINSTALLTYPEBB, string RUNNINGLENGTHFROMBB, string RUNNINGLENGTHTOBB, string ELCBSUBMITVAL, string METERSCANNEDVAL, string EXTRA1,
                    string EXTRA2, string EXTRA3, string EXTRA4, string EXTRA5, string EXTRA6, string EXTRA7,
                    string ORDER_TYPE, string PM_ACTIVITY, string OLD_M_READING, string MRKWH_OLD, string MRKW_OLD, string MRKVAH_OLD, string MRKVA_OLD,
                    string IMAGE1_OLD, string IMAGE2_OLD, string IMAGE_SIGNATURE_OLD, string INSTALLEDCABLE_OLD,
                    string CABLESIZE_OLD, string DRUMSIZE_OLD, string CABLEINSTALLTYPE_OLD, string RUNNINGLENGTHFROM_OLD, string RUNNINGLENGTHTO_OLD, string CABLELENGTH_OLD,
                    string OUTPUTBUSLENGTH_OLD, string TERMINALSEAL1_OLD, string TERMINALSEAL2_OLD, string METERBOXSEAL1_OLD, string METERBOXSEAL2_OLD, string BUSBARSEAL1_OLD, string BUSBARSEAL2_OLD,
                    string BOX_OLD, string GLANDS_OLD, string TCOVER_OLD, string BRASSSCREW_OLD, string BUSBAR_OLD, string THIMBLE_OLD, string SADDLE_OLD,
                    string GUNNYBAG_OLD, string GUNNYBAGSEAL_OLD, string LABTESTING_DATE_OLD, string METERRELOCATE_OLD)
    {
        Boolean _bSave = false;

        DataTable _dtOrderData = new DataTable();
        _dtOrderData = GetOrdType_PMAct_OrderWise(ORDERNO);

        string _sSAPFLAG = "N";
        string _sDivision = string.Empty;

        if (_dtOrderData.Rows.Count > 0)
        {
            ORDER_TYPE = _dtOrderData.Rows[0]["AUART"].ToString();
            PM_ACTIVITY = _dtOrderData.Rows[0]["ILART_ACTIVITY_TYPE"].ToString();

            if (_dtOrderData.Rows[0]["SAP_FLAG"] != null)
            {
                if (_dtOrderData.Rows[0]["SAP_FLAG"].ToString().ToUpper() == "N")
                    _sSAPFLAG = "Z";
            }
            if (_dtOrderData.Rows[0]["DIVISION"] != null)
            {
                _sDivision = _dtOrderData.Rows[0]["DIVISION"].ToString();
            }
        }


        if (METERBOXSEAL2.Trim() == BUSBARSEAL1.Trim())
            BUSBARSEAL1 = "";

        _bSave = NewClassFile.Insert_MCR_InputDataNEW(ORDERNO, DEVICENO, OTHERSTICKER, OTHER4, INSTALLEDBUSBAR, BUSBARSIZE, BUSBARNUMBER, DRUMSIZE, OTHER5, CABLESIZE2, CABLEINSTALLTYPE, RUNNINGLENGTHFROM, RUNNINGLENGTHTO,
                                                 CABLELENGTH, TERMINALSEAL1, TERMINALSEAL2, METERBOXSEAL1, METERBOXSEAL2, BUSBARSEAL1, BUSBARSEAL2, INSTALLEDLOCATION, POLENUMBER, OTHER6,
                                                 OTHER7, OTHER8, OTHER9, OTHER10, TAKEPHOTOGRAPH, METERDOWNLOAD, DBLOCKED, EARTHING, HEIGHTOFMETER, ANYJOINTS, OVERHEADCABLE, OVERHEADCABLEPOLE, FLOWMADE,
                                                 ADDITIONALACCESSORIES, OUTPUTBUSLENGTH, OUTPUTCABLELENGTH, EARTHINGPOLE, PUNCHED,
                                                 TAB_ID, TAB_NAME, GIS_LAT, GIS_LONG, GIS_STATUS, IMEI_NO, SUBMIT_DATETIME, DRUMNUMBERBB, VALINSTALLTYPEBB,
                                                 RUNNINGLENGTHFROMBB, RUNNINGLENGTHTOBB, ELCBSUBMITVAL, METERSCANNEDVAL, EXTRA1, EXTRA2, EXTRA3, EXTRA4, EXTRA5, EXTRA6, EXTRA7,
                                                 ORDER_TYPE, PM_ACTIVITY, OLD_M_READING, MRKWH_OLD, MRKW_OLD, MRKVAH_OLD, MRKVA_OLD, INSTALLEDCABLE_OLD, CABLESIZE_OLD,
                                                 DRUMSIZE_OLD, CABLEINSTALLTYPE_OLD, RUNNINGLENGTHFROM_OLD, RUNNINGLENGTHTO_OLD, CABLELENGTH_OLD, OUTPUTBUSLENGTH_OLD, TERMINALSEAL1_OLD,
                                                 TERMINALSEAL2_OLD, METERBOXSEAL1_OLD, METERBOXSEAL2_OLD, BUSBARSEAL1_OLD, BUSBARSEAL2_OLD, BOX_OLD, GLANDS_OLD, TCOVER_OLD, BRASSSCREW_OLD,
                                                 BUSBAR_OLD, THIMBLE_OLD, SADDLE_OLD, GUNNYBAG_OLD, GUNNYBAGSEAL_OLD, LABTESTING_DATE_OLD, METERRELOCATE_OLD, _sSAPFLAG, _sDivision);
        if (_bSave == true)
            NewClassFile.Update_MCRSealFlag(TERMINALSEAL1, ORDERNO);

        if (TERMINALSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(TERMINALSEAL2, ORDERNO);
        }
        if (METERBOXSEAL1 != "")
        {
            NewClassFile.Update_MCRSealFlag(METERBOXSEAL1, ORDERNO);
        }
        if (METERBOXSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(METERBOXSEAL2, ORDERNO);
        }
        if (BUSBARSEAL1 != "")
        {
            NewClassFile.Update_MCRSealFlag(BUSBARSEAL1, ORDERNO);
        }
        if (BUSBARSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(BUSBARSEAL2, ORDERNO);
        }

        string strCaNo = NewClassFile.GetCAOnOrderNo(ORDERNO);

        NewClassFile.Insert_MCR_IamgeListNEW(ORDERNO, OTHERSTICKER, DEVICENO, IMAGE1, IMAGE2, IMAGE3,
                                           IMEAGE_MCR, IMAGE_METERTESTREPORT, IMAGE_LABTESTINGREPORT,
                                           IMAGE_SIGNATURE, IMAGE4, IMAGE1_OLD, IMAGE2_OLD, IMAGE_SIGNATURE_OLD, strCaNo);

        if (_bSave == true)
            _bSave = NewClassFile.PunchOrder_Installer_InputData(ORDERNO, OTHER11, DEVICENO);

        return _bSave;
    }



    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string MCR_ValidateSEAL(string SealNo)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = NewClassFile.CheckValid_SealDeails(SealNo);
            dt.TableName = "MCR_SEAL_DETAILS";

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] != null)
                {
                    if (dt.Rows[0][0].ToString() == "Y")
                        return "T";
                    else
                        return "C";
                }
                else
                    return "F";
            }
            else
                return "F";
        }
        else
            return "F";
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string MCR_Create_Installer_Data(string INSTALLER_NAME, string INSTALLER_ID, string IMEI, string DIVISION, string VENDOR_ID)
    {
        NewClassFile.Insert_InstallerData(INSTALLER_NAME, INSTALLER_ID, IMEI, DIVISION, VENDOR_ID);
        NewClassFile.Insert_InstallerLoginData(INSTALLER_ID);

        return "T";
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable MCR_GetInstaller_Data(string VENDOR_ID)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = NewClassFile.GetInstaller_DataList_VendorWise(VENDOR_ID);
            dt.TableName = "MCR_USER_DETAILS";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string MCR_OrderAssign(string ORDERNO, string METERNO, string VENDOR_ID, string INSTALLER_ID)
    {
        NewClassFile.Assign_OrderInstaller_InputData(ORDERNO, VENDOR_ID);
        NewClassFile.MapData_OrderInstaller_InputData(ORDERNO, METERNO, VENDOR_ID, INSTALLER_ID);

        return "T";
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string MCR_Password_Update(string strEmpId, string strOldPass, string strNewPass)
    {
        NewClassFile.Update_PasswordData(strEmpId, strOldPass, strNewPass);
        return "T";
    }


    /// <summary>
    /// Developed by Gourav goutam on Date 15.11.2017 guide by swati kaushik,
    /// Developed for send Alloted Seal from Oracle Database to Mobile App
    /// </summary>
    /// <param name="strVendorID"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable MCR_GetSeal_Details(string Installer_ID)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = NewClassFile.GetSeal_Details_AllotedFrom_Vendor(Installer_ID);
            dt.TableName = "MCR_SEAL_DETAILS";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable MCR_GetMeter_Details(string Installer_ID)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = NewClassFile.GetMeter_DT_AllotedFrom_Vnd_LoosMtr(Installer_ID);
            dt.TableName = "MCR_METER_DETAILS";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string MCR_OrderCancel(string Installer_ID, string Order_ID, string Reason, string Image, string Remarks, string strCustName,
                                                                                                            string strCustNo, string strCustDate)
    {
        Boolean _bSave = false;
        //_bSave=NewClassFile.insert_OrderCancelDetails(Order_ID, Reason, Installer_ID, Remarks, Image);
        _bSave = NewClassFile.insert_OrderCancelDetails(Order_ID, Reason, Installer_ID, Remarks, Image, strCustName, strCustNo, strCustDate);

        if (_bSave == true)
            NewClassFile.Update_OrderStatus(Order_ID, Installer_ID);

        if (_bSave == true)
            return "T";
        else
            return "F";
    }





    #endregion

    #region MCR Punching New

    [WebMethod(EnableSession = true)]
    public Boolean MCR_Insert_INPUT_Data_RV(string ORDER_NO, string ORDER_TYPE, string PM_ACTIVITY, string DEVICE_NO, string ACTVTY_RSN, string TF_STCKR_STUS,
       string TF_STCKR_NO, string ELCB_INSLD_ON_SITE, string BUS_BAR_INSTALLD, string BUS_BAR_SIZE, string BUS_BAR_NO, string BUS_BAR_CBL_SIZE,
       string BUS_BAR_DRM_NO, string BUS_BAR_CBL_INSTL_TYP, string BUS_BR_RUN_LENTH_FRM, string BUS_BR_RUN_LENTH_TO, string BUS_BR_CBL_LNGTH, string BUS_BAR_B_OR_C,
       string HV_U_INSTALL_NW_CBL, string CBL_SIZE, string CABLEINSTALLTYPE, string CBL_RUN_LENTH_FRM, string CBL_RUN_LENTH_TO, string CABLELENGTH,
       string CBL_DRM_NUMBR, string OUTPUTCABLELENGTH, string TERMINALSEAL1, string TERMINALSEAL2, string METERBOXSEAL1, string METERBOXSEAL2,
       string BUSBARSEAL1, string BUSBARSEAL2, string MTR_MRKWH, string MTR_MRKW, string MTR_MRKVAH, string MTR_MRKVA, string QC_PHOTO_TKN,
       string QC_METERDOWNLOAD, string QC_DBLOCKED, string QC_EARTHING_CONS, string QC_EARTHING_POLE, string QC_HEIGHTOFMETER, string QC_ANYJOINTS,
       string QC_FIXTR_INSL_CONS, string QC_FIXTR_INSL_POLE, string QC_FLOWMADE, string QC_ADDNL_ACC_USD, string QC_RMKS, string ORDER_STATUS, string CANCEL_RMKS,
       string ACTIVITY_DATE, string INSTALLEDLOCATION, string METERSCANNEDVAL, string MTR_LOC_SHIFT, string RMVD_CBL_SIZE, string RMVD_CBL_RUN_LENTH_FRM,
       string RMVD_CBL_RUN_LENTH_TO, string RMVD_CBL_LENTH, string RMVD_TRMNL_SEAL1, string RMVD_TRMNL_SEAL2, string RMVD_MTR_BOX_SEAL1, string RMVD_MTR_BOX_SEAL2,
       string RMVD_BUS_BAR_SEAL1, string RMVD_BUS_BAR_SEAL2, string RMVD_MTR_BOX, string RMVD_MTR_GLND, string RMVD_MTR_T_COVR, string RMVD_MTR_BRS_SCRW,
       string RMVD_MTR_BUS_BAR, string RMVD_MTR_THMBL_LUG, string RMVD_MTR_SADL, string RMVD_MTR_BASE_PLT, string OLD_MTR_MRKWH, string OLD_MTR_MRKW,
       string OLD_MTR_MRKVAH, string OLD_MTR_MRKVA, string OLD_MTR_STATUS, string OLD_MTR_IF_AVBL, string OLD_MTR_IF_NOT_AVBL, string GUNNY_BAG_NO,
       string GUNNY_BAG_SEAL_NO, string LAB_TSTNG_DT, string LAB_TSTNG_NTC, string MTR_LOC_RELOCT, string TAB_ID, string TAB_NAME, string GIS_LAT,
       string GIS_LONG, string GIS_STATUS, string IMEI_NO, string SUBMIT_DATETIME, string POLENUMBER, string PUNCH_MODE)
    {
        NewClassFile.WriteIntoFile_MCR("Start MCR Function MCR_Insert_INPUT_Data_RV for " + ORDER_NO);

        Boolean _bSave = false;

        TERMINALSEAL1 = ReplaceSelect(TERMINALSEAL1);
        TERMINALSEAL2 = ReplaceSelect(TERMINALSEAL2);
        METERBOXSEAL1 = ReplaceSelect(METERBOXSEAL1);
        METERBOXSEAL2 = ReplaceSelect(METERBOXSEAL2);
        BUSBARSEAL1 = ReplaceSelect(BUSBARSEAL1);
        BUSBARSEAL2 = ReplaceSelect(BUSBARSEAL2);

        DataTable _dtOrderData = new DataTable();
        _dtOrderData = GetOrdType_PMAct_OrderWise(ORDER_NO);

        string _sSAPFLAG = "N";
        string _sDivision = string.Empty;

        if (_dtOrderData.Rows.Count > 0)
        {
            if (_dtOrderData.Rows[0]["SAP_FLAG"] != null)
            {
                if (_dtOrderData.Rows[0]["SAP_FLAG"].ToString().ToUpper() == "N")
                    _sSAPFLAG = "Z";
            }
            if (_dtOrderData.Rows[0]["DIVISION"] != null)
            {
                _sDivision = _dtOrderData.Rows[0]["DIVISION"].ToString();
            }
        }

        if (METERBOXSEAL2.Trim() == BUSBARSEAL1.Trim())
            BUSBARSEAL1 = "";

        _bSave = NewClassFile.Insert_MCR_InputDataNEWRV(ORDER_NO, DEVICE_NO, TF_STCKR_NO, ELCB_INSLD_ON_SITE, BUS_BAR_INSTALLD, BUS_BAR_SIZE, BUS_BAR_NO, CBL_DRM_NUMBR, ACTVTY_RSN, CBL_SIZE, CABLEINSTALLTYPE, CBL_RUN_LENTH_FRM, CBL_RUN_LENTH_TO,
                                                CABLELENGTH, TERMINALSEAL1, TERMINALSEAL2, METERBOXSEAL1, METERBOXSEAL2, BUSBARSEAL1, BUSBARSEAL2, INSTALLEDLOCATION, POLENUMBER, ACTIVITY_DATE,
                                                MTR_MRKWH, MTR_MRKW, MTR_MRKVAH, MTR_MRKVA, QC_PHOTO_TKN, QC_METERDOWNLOAD, QC_DBLOCKED, QC_EARTHING_CONS, QC_HEIGHTOFMETER, QC_ANYJOINTS, QC_FIXTR_INSL_CONS, QC_FIXTR_INSL_POLE, QC_FLOWMADE,
                                                QC_ADDNL_ACC_USD, BUS_BR_CBL_LNGTH, OUTPUTCABLELENGTH, QC_EARTHING_POLE, BUS_BAR_CBL_SIZE,
                                                TAB_ID, TAB_NAME, GIS_LAT, GIS_LONG, GIS_STATUS, IMEI_NO, SUBMIT_DATETIME, BUS_BAR_DRM_NO, BUS_BAR_CBL_INSTL_TYP,
                                                BUS_BR_RUN_LENTH_FRM, BUS_BR_RUN_LENTH_TO, ELCB_INSLD_ON_SITE, METERSCANNEDVAL, QC_RMKS, BUS_BAR_B_OR_C, "", "", "", "", PUNCH_MODE,
                                                ORDER_TYPE, PM_ACTIVITY, "OLD_M_READING", OLD_MTR_MRKWH, OLD_MTR_MRKW, OLD_MTR_MRKVAH, OLD_MTR_MRKVA, "INSTALLEDCABLE_OLD", RMVD_CBL_SIZE,
                                                "DRUMSIZE_OLD", "CABLEINSTALLTYPE_OLD", RMVD_CBL_RUN_LENTH_FRM, RMVD_CBL_RUN_LENTH_TO, RMVD_CBL_LENTH, "OUTPUTBUSLENGTH_OLD", RMVD_TRMNL_SEAL1,
                                                RMVD_TRMNL_SEAL2, RMVD_MTR_BOX_SEAL1, RMVD_MTR_BOX_SEAL2, RMVD_BUS_BAR_SEAL1, RMVD_BUS_BAR_SEAL2, RMVD_MTR_BOX, RMVD_MTR_GLND, RMVD_MTR_T_COVR, RMVD_MTR_BRS_SCRW,
                                                RMVD_MTR_BUS_BAR, RMVD_MTR_THMBL_LUG, RMVD_MTR_SADL, GUNNY_BAG_NO, GUNNY_BAG_SEAL_NO, LAB_TSTNG_DT, "METERRELOCATE_OLD", _sSAPFLAG,
                                                TF_STCKR_STUS, HV_U_INSTALL_NW_CBL, ORDER_STATUS, MTR_LOC_SHIFT, RMVD_MTR_BASE_PLT, OLD_MTR_STATUS,
                                                OLD_MTR_IF_AVBL, OLD_MTR_IF_NOT_AVBL, LAB_TSTNG_NTC, MTR_LOC_RELOCT, _sDivision, "OTHS",
                                                "INST_BB_CABLE_OPT", "BB_CABLE_USED", "CABLE_LEN_USED", "OUTPUT_CABLE_LEN_USED", "SIFT_DONE_BY", "CAB_REMOVE_FRM_SITE",
                                                "REPLACEMENT_DONE_CABL", "CAB_RMVD_FRM_SITE", "IS_GNY_BAG_PREPD", "GNY_PREPD_NO_RESN", "GNY_PREPD_NO_RESN_RMK", "MTR_READ_AVAIL", "METER_REMOVED_BY", "NOTICE_DATE",
                                                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");


        NewClassFile.WriteIntoFile_MCR("Insert status for MCR_Insert_INPUT_Data_RV " + _bSave.ToString());

        if (_bSave == true)
            NewClassFile.Update_MCRSealFlag(TERMINALSEAL1, ORDER_NO);

        if (TERMINALSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(TERMINALSEAL2, ORDER_NO);
        }
        if (METERBOXSEAL1 != "")
        {
            NewClassFile.Update_MCRSealFlag(METERBOXSEAL1, ORDER_NO);
        }
        if (METERBOXSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(METERBOXSEAL2, ORDER_NO);
        }
        if (BUSBARSEAL1 != "")
        {
            NewClassFile.Update_MCRSealFlag(BUSBARSEAL1, ORDER_NO);
        }
        if (BUSBARSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(BUSBARSEAL2, ORDER_NO);
        }


        if (_bSave == true)
            _bSave = NewClassFile.PunchOrder_Installer_InputData(ORDER_NO, TAB_ID, DEVICE_NO);

        NewClassFile.WriteIntoFile_MCR("PunchOrder_Installer_InputData " + _bSave.ToString());
        return _bSave;

    }

    [WebMethod(EnableSession = true)]
    public Boolean MCR_Insert_INPUT_Data_Img_RV(string ORDER_NO, string DEVICE_NO, string OTHERSTICKER, string MTR_PHOTO, string CMPLT_MTR_INTALL_PHOTO,
                                               string MCR_PHOTO, string MCR_CONS_SIGN_PHOTO, string MCR_POLE_N_PHOTO, string MCR_OTHR_PHOTO,
                                                string MCR_TST_RPT_PHOTO, string MCR_LAB_TST_RPT_PHOTO, string PREMISE_PHOTO, string RMVD_MTR_PHOTO,
                                                string RMVD_CMPLT_MTR_INTALL_PHOTO)
    {
        Boolean _bSave = false;
        string strCaNo = NewClassFile.GetCAOnOrderNo(ORDER_NO);

        DataTable _dtOrderData = new DataTable();
        _dtOrderData = GetOrdType_PMAct_OrderWise(ORDER_NO);

        string _sSAPFLAG = "N";
        if (_dtOrderData.Rows.Count > 0)
        {
            if (_dtOrderData.Rows[0]["SAP_FLAG"] != null)
            {
                if (_dtOrderData.Rows[0]["SAP_FLAG"].ToString().ToUpper() == "N")
                    _sSAPFLAG = "Z";
            }
        }

        PREMISE_PHOTO = "";

        _bSave = NewClassFile.Insert_MCR_IamgeListNEW(ORDER_NO, OTHERSTICKER, DEVICE_NO, MTR_PHOTO, CMPLT_MTR_INTALL_PHOTO, MCR_POLE_N_PHOTO,
                                         MCR_PHOTO, MCR_TST_RPT_PHOTO, MCR_LAB_TST_RPT_PHOTO, MCR_CONS_SIGN_PHOTO, MCR_OTHR_PHOTO,
                                         RMVD_MTR_PHOTO, RMVD_CMPLT_MTR_INTALL_PHOTO, PREMISE_PHOTO, strCaNo, _sSAPFLAG);

        NewClassFile.WriteIntoFile_MCR("Insert_MCR_IamgeList " + _bSave.ToString());

        return _bSave;
    }


    [WebMethod(EnableSession = true)]
    public Boolean MCR_Insert_INPUT_Data_RV_NEW(string ORDER_NO, string ORDER_TYPE, string PM_ACTIVITY, string DEVICE_NO, string ACTVTY_RSN, string TF_STCKR_STUS,
       string TF_STCKR_NO, string ELCB_INSLD_ON_SITE, string BUS_BAR_INSTALLD, string BUS_BAR_SIZE, string BUS_BAR_NO, string BUS_BAR_CBL_SIZE,
       string BUS_BAR_DRM_NO, string BUS_BAR_CBL_INSTL_TYP, string BUS_BR_RUN_LENTH_FRM, string BUS_BR_RUN_LENTH_TO, string BUS_BR_CBL_LNGTH, string BUS_BAR_B_OR_C,
       string HV_U_INSTALL_NW_CBL, string CBL_SIZE, string CABLEINSTALLTYPE, string CBL_RUN_LENTH_FRM, string CBL_RUN_LENTH_TO, string CABLELENGTH,
       string CBL_DRM_NUMBR, string OUTPUTCABLELENGTH, string TERMINALSEAL1, string TERMINALSEAL2, string METERBOXSEAL1, string METERBOXSEAL2,
       string BUSBARSEAL1, string BUSBARSEAL2, string MTR_MRKWH, string MTR_MRKW, string MTR_MRKVAH, string MTR_MRKVA, string QC_PHOTO_TKN,
       string QC_METERDOWNLOAD, string QC_DBLOCKED, string QC_EARTHING_CONS, string QC_EARTHING_POLE, string QC_HEIGHTOFMETER, string QC_ANYJOINTS,
       string QC_FIXTR_INSL_CONS, string QC_FIXTR_INSL_POLE, string QC_FLOWMADE, string QC_ADDNL_ACC_USD, string QC_RMKS, string ORDER_STATUS, string CANCEL_RMKS,
       string ACTIVITY_DATE, string INSTALLEDLOCATION, string METERSCANNEDVAL, string MTR_LOC_SHIFT, string RMVD_CBL_SIZE, string RMVD_CBL_RUN_LENTH_FRM,
       string RMVD_CBL_RUN_LENTH_TO, string RMVD_CBL_LENTH, string RMVD_TRMNL_SEAL1, string RMVD_TRMNL_SEAL2, string RMVD_MTR_BOX_SEAL1, string RMVD_MTR_BOX_SEAL2,
       string RMVD_BUS_BAR_SEAL1, string RMVD_BUS_BAR_SEAL2, string RMVD_MTR_BOX, string RMVD_MTR_GLND, string RMVD_MTR_T_COVR, string RMVD_MTR_BRS_SCRW,
       string RMVD_MTR_BUS_BAR, string RMVD_MTR_THMBL_LUG, string RMVD_MTR_SADL, string RMVD_MTR_BASE_PLT, string OLD_MTR_MRKWH, string OLD_MTR_MRKW,
       string OLD_MTR_MRKVAH, string OLD_MTR_MRKVA, string OLD_MTR_STATUS, string OLD_MTR_IF_AVBL, string OLD_MTR_IF_NOT_AVBL, string GUNNY_BAG_NO,
       string GUNNY_BAG_SEAL_NO, string LAB_TSTNG_DT, string LAB_TSTNG_NTC, string MTR_LOC_RELOCT, string TAB_ID, string TAB_NAME, string GIS_LAT,
       string GIS_LONG, string GIS_STATUS, string IMEI_NO, string SUBMIT_DATETIME, string POLENUMBER, string PUNCH_MODE, string MTR_PHOTO, string CMPLT_MTR_INTALL_PHOTO,
                                               string MCR_PHOTO, string MCR_CONS_SIGN_PHOTO, string MCR_POLE_N_PHOTO, string MCR_OTHR_PHOTO,
                                                string MCR_TST_RPT_PHOTO, string MCR_LAB_TST_RPT_PHOTO, string PREMISE_PHOTO, string RMVD_MTR_PHOTO,
                                                string RMVD_CMPLT_MTR_INTALL_PHOTO, string HAPPY_CODE,
        string INST_BB_CABLE_OPT, string BB_CABLE_USED, string CABLE_LEN_USED, string OUTPUT_CABLE_LEN_USED, string SIFT_DONE_BY, string CAB_REMOVE_FRM_SITE,
        string REPLACEMENT_DONE_CABL, string CAB_RMVD_FRM_SITE, string IS_GNY_BAG_PREPD, string GNY_PREPD_NO_RESN, string GNY_PREPD_NO_RESN_RMK,
        string MTR_READ_AVAIL, string METER_REMOVED_BY, string NOTICE_DATE,
        string startDate, string customerName, string customerAddress, string customerMobile, string customerPole, string customerMeter, string accountClass,
        string connCategory, string customerCA, string looseOther1, string looseOther2, string looseFlag,
        string cableNotInstallReason, string noHappyCodeReason, string smartMeterBool, string smartMeterSimNo, string smartMeterSimCode)
    {
        NewClassFile.WriteIntoFile_MCR("Start MCR Function MCR_Insert_INPUT_Data_RV_NEW for " + ORDER_NO);

        Boolean _bSave = false, _bISave = false;

        TERMINALSEAL1 = ReplaceSelect(TERMINALSEAL1);
        TERMINALSEAL2 = ReplaceSelect(TERMINALSEAL2);
        METERBOXSEAL1 = ReplaceSelect(METERBOXSEAL1);
        METERBOXSEAL2 = ReplaceSelect(METERBOXSEAL2);
        BUSBARSEAL1 = ReplaceSelect(BUSBARSEAL1);
        BUSBARSEAL2 = ReplaceSelect(BUSBARSEAL2);

        DataTable _dtOrderData = new DataTable();
        string _sSAPFLAG = "N";
        string _sDivision = string.Empty;

        if (looseFlag == "LOOSE")
        {
            _sSAPFLAG = "L";
        }
        else
        {
            _dtOrderData = GetOrdType_PMAct_OrderWise(ORDER_NO);


            if (_dtOrderData.Rows.Count > 0)
            {
                if (_dtOrderData.Rows[0]["SAP_FLAG"] != null)
                {
                    if (_dtOrderData.Rows[0]["SAP_FLAG"].ToString().ToUpper() == "N")
                        _sSAPFLAG = "Z";
                }
                if (_dtOrderData.Rows[0]["DIVISION"] != null)
                {
                    _sDivision = _dtOrderData.Rows[0]["DIVISION"].ToString();
                }
            }
        }

        if (METERBOXSEAL2.Trim() == BUSBARSEAL1.Trim())
            BUSBARSEAL1 = "";

        _bSave = NewClassFile.Insert_MCR_InputDataNEWRV(ORDER_NO, DEVICE_NO, TF_STCKR_NO, ELCB_INSLD_ON_SITE, BUS_BAR_INSTALLD, BUS_BAR_SIZE, BUS_BAR_NO, CBL_DRM_NUMBR, ACTVTY_RSN, CBL_SIZE, CABLEINSTALLTYPE, CBL_RUN_LENTH_FRM, CBL_RUN_LENTH_TO,
                                                CABLELENGTH, TERMINALSEAL1, TERMINALSEAL2, METERBOXSEAL1, METERBOXSEAL2, BUSBARSEAL1, BUSBARSEAL2, INSTALLEDLOCATION, POLENUMBER, ACTIVITY_DATE,
                                                MTR_MRKWH, MTR_MRKW, MTR_MRKVAH, MTR_MRKVA, QC_PHOTO_TKN, QC_METERDOWNLOAD, QC_DBLOCKED, QC_EARTHING_CONS, QC_HEIGHTOFMETER, QC_ANYJOINTS, QC_FIXTR_INSL_CONS, QC_FIXTR_INSL_POLE, QC_FLOWMADE,
                                                QC_ADDNL_ACC_USD, BUS_BR_CBL_LNGTH, OUTPUTCABLELENGTH, QC_EARTHING_POLE, BUS_BAR_CBL_SIZE,
                                                TAB_ID, TAB_NAME, GIS_LAT, GIS_LONG, GIS_STATUS, IMEI_NO, SUBMIT_DATETIME, BUS_BAR_DRM_NO, BUS_BAR_CBL_INSTL_TYP,
                                                BUS_BR_RUN_LENTH_FRM, BUS_BR_RUN_LENTH_TO, ELCB_INSLD_ON_SITE, METERSCANNEDVAL, QC_RMKS, BUS_BAR_B_OR_C, "", "", "", "", PUNCH_MODE,
                                                ORDER_TYPE, PM_ACTIVITY, "OLD_M_READING", OLD_MTR_MRKWH, OLD_MTR_MRKW, OLD_MTR_MRKVAH, OLD_MTR_MRKVA, "INSTALLEDCABLE_OLD", RMVD_CBL_SIZE,
                                                "DRUMSIZE_OLD", "CABLEINSTALLTYPE_OLD", RMVD_CBL_RUN_LENTH_FRM, RMVD_CBL_RUN_LENTH_TO, RMVD_CBL_LENTH, "OUTPUTBUSLENGTH_OLD", RMVD_TRMNL_SEAL1,
                                                RMVD_TRMNL_SEAL2, RMVD_MTR_BOX_SEAL1, RMVD_MTR_BOX_SEAL2, RMVD_BUS_BAR_SEAL1, RMVD_BUS_BAR_SEAL2, RMVD_MTR_BOX, RMVD_MTR_GLND, RMVD_MTR_T_COVR, RMVD_MTR_BRS_SCRW,
                                                RMVD_MTR_BUS_BAR, RMVD_MTR_THMBL_LUG, RMVD_MTR_SADL, GUNNY_BAG_NO, GUNNY_BAG_SEAL_NO, LAB_TSTNG_DT, "METERRELOCATE_OLD", _sSAPFLAG,
                                                TF_STCKR_STUS, HV_U_INSTALL_NW_CBL, ORDER_STATUS, MTR_LOC_SHIFT, RMVD_MTR_BASE_PLT, OLD_MTR_STATUS,
                                                OLD_MTR_IF_AVBL, OLD_MTR_IF_NOT_AVBL, LAB_TSTNG_NTC, MTR_LOC_RELOCT, _sDivision, HAPPY_CODE,
                                                INST_BB_CABLE_OPT, BB_CABLE_USED, CABLE_LEN_USED, OUTPUT_CABLE_LEN_USED, SIFT_DONE_BY, CAB_REMOVE_FRM_SITE,
                                                REPLACEMENT_DONE_CABL, CAB_RMVD_FRM_SITE, IS_GNY_BAG_PREPD, GNY_PREPD_NO_RESN, GNY_PREPD_NO_RESN_RMK,
                                                MTR_READ_AVAIL, METER_REMOVED_BY, NOTICE_DATE,
                                                startDate, customerName, customerAddress, customerMobile, customerPole, customerMeter, accountClass,
                                                connCategory, customerCA, looseOther1, looseOther2, looseFlag, cableNotInstallReason, noHappyCodeReason,
                                                smartMeterBool, smartMeterSimNo, smartMeterSimCode, "");


        NewClassFile.WriteIntoFile_MCR("Insert status for MCR_Insert_INPUT_Data_RV_NEW " + _bSave.ToString());

        if (_bSave == true)
            NewClassFile.Update_MCRSealFlag(TERMINALSEAL1, ORDER_NO);

        if (TERMINALSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(TERMINALSEAL2, ORDER_NO);
        }
        if (METERBOXSEAL1 != "")
        {
            NewClassFile.Update_MCRSealFlag(METERBOXSEAL1, ORDER_NO);
        }
        if (METERBOXSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(METERBOXSEAL2, ORDER_NO);
        }
        if (BUSBARSEAL1 != "")
        {
            NewClassFile.Update_MCRSealFlag(BUSBARSEAL1, ORDER_NO);
        }
        if (BUSBARSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(BUSBARSEAL2, ORDER_NO);
        }


        if (_bSave == true)
        {
            if (looseFlag == "LOOSE")
                _bSave = NewClassFile.PunchLooseOrder_Installer_InputData(TAB_ID, DEVICE_NO);
            else
                _bSave = NewClassFile.PunchOrder_Installer_InputData(ORDER_NO, TAB_ID, DEVICE_NO);
        }

        NewClassFile.WriteIntoFile_MCR("PunchOrder_Installer_InputData " + _bSave.ToString());

        PREMISE_PHOTO = "";
        string strCaNo = NewClassFile.GetCAOnOrderNo(ORDER_NO);

        if (looseFlag == "LOOSE")
        {
            _bISave = NewClassFile.Insert_MCR_IamgeListNEW("941", TF_STCKR_NO, DEVICE_NO, MTR_PHOTO, CMPLT_MTR_INTALL_PHOTO, MCR_POLE_N_PHOTO,
                                             MCR_PHOTO, MCR_TST_RPT_PHOTO, MCR_LAB_TST_RPT_PHOTO, MCR_CONS_SIGN_PHOTO, MCR_OTHR_PHOTO,
                                             RMVD_MTR_PHOTO, RMVD_CMPLT_MTR_INTALL_PHOTO, PREMISE_PHOTO, DEVICE_NO, _sSAPFLAG);
        }
        else
        {
            _bISave = NewClassFile.Insert_MCR_IamgeListNEW(ORDER_NO, TF_STCKR_NO, DEVICE_NO, MTR_PHOTO, CMPLT_MTR_INTALL_PHOTO, MCR_POLE_N_PHOTO,
                                             MCR_PHOTO, MCR_TST_RPT_PHOTO, MCR_LAB_TST_RPT_PHOTO, MCR_CONS_SIGN_PHOTO, MCR_OTHR_PHOTO,
                                             RMVD_MTR_PHOTO, RMVD_CMPLT_MTR_INTALL_PHOTO, PREMISE_PHOTO, strCaNo, _sSAPFLAG);
        }

        NewClassFile.WriteIntoFile_MCR("Image Log " + _bISave.ToString());

        return _bISave;

    }

    [WebMethod(EnableSession = true)]
    public Boolean MCR_Insert_INPUT_Data_RV_NEW1(string ORDER_NO, string ORDER_TYPE, string PM_ACTIVITY, string DEVICE_NO, string ACTVTY_RSN, string TF_STCKR_STUS,
       string TF_STCKR_NO, string ELCB_INSLD_ON_SITE, string BUS_BAR_INSTALLD, string BUS_BAR_SIZE, string BUS_BAR_NO, string BUS_BAR_CBL_SIZE,
       string BUS_BAR_DRM_NO, string BUS_BAR_CBL_INSTL_TYP, string BUS_BR_RUN_LENTH_FRM, string BUS_BR_RUN_LENTH_TO, string BUS_BR_CBL_LNGTH, string BUS_BAR_B_OR_C,
       string HV_U_INSTALL_NW_CBL, string CBL_SIZE, string CABLEINSTALLTYPE, string CBL_RUN_LENTH_FRM, string CBL_RUN_LENTH_TO, string CABLELENGTH,
       string CBL_DRM_NUMBR, string OUTPUTCABLELENGTH, string TERMINALSEAL1, string TERMINALSEAL2, string METERBOXSEAL1, string METERBOXSEAL2,
       string BUSBARSEAL1, string BUSBARSEAL2, string MTR_MRKWH, string MTR_MRKW, string MTR_MRKVAH, string MTR_MRKVA, string QC_PHOTO_TKN,
       string QC_METERDOWNLOAD, string QC_DBLOCKED, string QC_EARTHING_CONS, string QC_EARTHING_POLE, string QC_HEIGHTOFMETER, string QC_ANYJOINTS,
       string QC_FIXTR_INSL_CONS, string QC_FIXTR_INSL_POLE, string QC_FLOWMADE, string QC_ADDNL_ACC_USD, string QC_RMKS, string ORDER_STATUS, string CANCEL_RMKS,
       string ACTIVITY_DATE, string INSTALLEDLOCATION, string METERSCANNEDVAL, string MTR_LOC_SHIFT, string RMVD_CBL_SIZE, string RMVD_CBL_RUN_LENTH_FRM,
       string RMVD_CBL_RUN_LENTH_TO, string RMVD_CBL_LENTH, string RMVD_TRMNL_SEAL1, string RMVD_TRMNL_SEAL2, string RMVD_MTR_BOX_SEAL1, string RMVD_MTR_BOX_SEAL2,
       string RMVD_BUS_BAR_SEAL1, string RMVD_BUS_BAR_SEAL2, string RMVD_MTR_BOX, string RMVD_MTR_GLND, string RMVD_MTR_T_COVR, string RMVD_MTR_BRS_SCRW,
       string RMVD_MTR_BUS_BAR, string RMVD_MTR_THMBL_LUG, string RMVD_MTR_SADL, string RMVD_MTR_BASE_PLT, string OLD_MTR_MRKWH, string OLD_MTR_MRKW,
       string OLD_MTR_MRKVAH, string OLD_MTR_MRKVA, string OLD_MTR_STATUS, string OLD_MTR_IF_AVBL, string OLD_MTR_IF_NOT_AVBL, string GUNNY_BAG_NO,
       string GUNNY_BAG_SEAL_NO, string LAB_TSTNG_DT, string LAB_TSTNG_NTC, string MTR_LOC_RELOCT, string TAB_ID, string TAB_NAME, string GIS_LAT,
       string GIS_LONG, string GIS_STATUS, string IMEI_NO, string SUBMIT_DATETIME, string POLENUMBER, string PUNCH_MODE, string MTR_PHOTO, string CMPLT_MTR_INTALL_PHOTO,
                                               string MCR_PHOTO, string MCR_CONS_SIGN_PHOTO, string MCR_POLE_N_PHOTO, string MCR_OTHR_PHOTO,
                                                string MCR_TST_RPT_PHOTO, string MCR_LAB_TST_RPT_PHOTO, string PREMISE_PHOTO, string RMVD_MTR_PHOTO,
                                                string RMVD_CMPLT_MTR_INTALL_PHOTO, string HAPPY_CODE,
        string INST_BB_CABLE_OPT, string BB_CABLE_USED, string CABLE_LEN_USED, string OUTPUT_CABLE_LEN_USED, string SIFT_DONE_BY, string CAB_REMOVE_FRM_SITE,
        string REPLACEMENT_DONE_CABL, string CAB_RMVD_FRM_SITE, string IS_GNY_BAG_PREPD, string GNY_PREPD_NO_RESN, string GNY_PREPD_NO_RESN_RMK,
        string MTR_READ_AVAIL, string METER_REMOVED_BY, string NOTICE_DATE,
        string startDate, string customerName, string customerAddress, string customerMobile, string customerPole, string customerMeter, string accountClass,
        string connCategory, string customerCA, string looseOther1, string looseOther2, string looseFlag,
        string cableNotInstallReason, string noHappyCodeReason, string smartMeterBool, string smartMeterSimNo, string smartMeterSimCode,
         string SubDivisionCode, string mcrPDF)
    {
        NewClassFile.WriteIntoFile_MCR("Start MCR Function MCR_Insert_INPUT_Data_RV_NEW for " + ORDER_NO);

        Boolean _bSave = false, _bISave = false;

        TERMINALSEAL1 = ReplaceSelect(TERMINALSEAL1);
        TERMINALSEAL2 = ReplaceSelect(TERMINALSEAL2);
        METERBOXSEAL1 = ReplaceSelect(METERBOXSEAL1);
        METERBOXSEAL2 = ReplaceSelect(METERBOXSEAL2);
        BUSBARSEAL1 = ReplaceSelect(BUSBARSEAL1);
        BUSBARSEAL2 = ReplaceSelect(BUSBARSEAL2);

        DataTable _dtOrderData = new DataTable();
        string _sSAPFLAG = "N";
        string _sDivision = string.Empty;

        if (looseFlag == "LOOSE")
        {
            _sSAPFLAG = "L";
        }
        else
        {
            _dtOrderData = GetOrdType_PMAct_OrderWise(ORDER_NO);


            if (_dtOrderData.Rows.Count > 0)
            {
                if (_dtOrderData.Rows[0]["SAP_FLAG"] != null)
                {
                    if (_dtOrderData.Rows[0]["SAP_FLAG"].ToString().ToUpper() == "N")
                        _sSAPFLAG = "Z";
                }
                if (_dtOrderData.Rows[0]["DIVISION"] != null)
                {
                    _sDivision = _dtOrderData.Rows[0]["DIVISION"].ToString();
                }
            }
        }

        if (METERBOXSEAL2.Trim() == BUSBARSEAL1.Trim())
            BUSBARSEAL1 = "";

        _bSave = NewClassFile.Insert_MCR_InputDataNEWRV(ORDER_NO, DEVICE_NO, TF_STCKR_NO, ELCB_INSLD_ON_SITE, BUS_BAR_INSTALLD, BUS_BAR_SIZE, BUS_BAR_NO, CBL_DRM_NUMBR, ACTVTY_RSN, CBL_SIZE, CABLEINSTALLTYPE, CBL_RUN_LENTH_FRM, CBL_RUN_LENTH_TO,
                                                CABLELENGTH, TERMINALSEAL1, TERMINALSEAL2, METERBOXSEAL1, METERBOXSEAL2, BUSBARSEAL1, BUSBARSEAL2, INSTALLEDLOCATION, POLENUMBER, ACTIVITY_DATE,
                                                MTR_MRKWH, MTR_MRKW, MTR_MRKVAH, MTR_MRKVA, QC_PHOTO_TKN, QC_METERDOWNLOAD, QC_DBLOCKED, QC_EARTHING_CONS, QC_HEIGHTOFMETER, QC_ANYJOINTS, QC_FIXTR_INSL_CONS, QC_FIXTR_INSL_POLE, QC_FLOWMADE,
                                                QC_ADDNL_ACC_USD, BUS_BR_CBL_LNGTH, OUTPUTCABLELENGTH, QC_EARTHING_POLE, BUS_BAR_CBL_SIZE,
                                                TAB_ID, TAB_NAME, GIS_LAT, GIS_LONG, GIS_STATUS, IMEI_NO, SUBMIT_DATETIME, BUS_BAR_DRM_NO, BUS_BAR_CBL_INSTL_TYP,
                                                BUS_BR_RUN_LENTH_FRM, BUS_BR_RUN_LENTH_TO, ELCB_INSLD_ON_SITE, METERSCANNEDVAL, QC_RMKS, BUS_BAR_B_OR_C, "", "", "", "", PUNCH_MODE,
                                                ORDER_TYPE, PM_ACTIVITY, "OLD_M_READING", OLD_MTR_MRKWH, OLD_MTR_MRKW, OLD_MTR_MRKVAH, OLD_MTR_MRKVA, "INSTALLEDCABLE_OLD", RMVD_CBL_SIZE,
                                                "DRUMSIZE_OLD", "CABLEINSTALLTYPE_OLD", RMVD_CBL_RUN_LENTH_FRM, RMVD_CBL_RUN_LENTH_TO, RMVD_CBL_LENTH, "OUTPUTBUSLENGTH_OLD", RMVD_TRMNL_SEAL1,
                                                RMVD_TRMNL_SEAL2, RMVD_MTR_BOX_SEAL1, RMVD_MTR_BOX_SEAL2, RMVD_BUS_BAR_SEAL1, RMVD_BUS_BAR_SEAL2, RMVD_MTR_BOX, RMVD_MTR_GLND, RMVD_MTR_T_COVR, RMVD_MTR_BRS_SCRW,
                                                RMVD_MTR_BUS_BAR, RMVD_MTR_THMBL_LUG, RMVD_MTR_SADL, GUNNY_BAG_NO, GUNNY_BAG_SEAL_NO, LAB_TSTNG_DT, "METERRELOCATE_OLD", _sSAPFLAG,
                                                TF_STCKR_STUS, HV_U_INSTALL_NW_CBL, ORDER_STATUS, MTR_LOC_SHIFT, RMVD_MTR_BASE_PLT, OLD_MTR_STATUS,
                                                OLD_MTR_IF_AVBL, OLD_MTR_IF_NOT_AVBL, LAB_TSTNG_NTC, MTR_LOC_RELOCT, _sDivision, HAPPY_CODE,
                                                INST_BB_CABLE_OPT, BB_CABLE_USED, CABLE_LEN_USED, OUTPUT_CABLE_LEN_USED, SIFT_DONE_BY, CAB_REMOVE_FRM_SITE,
                                                REPLACEMENT_DONE_CABL, CAB_RMVD_FRM_SITE, IS_GNY_BAG_PREPD, GNY_PREPD_NO_RESN, GNY_PREPD_NO_RESN_RMK,
                                                MTR_READ_AVAIL, METER_REMOVED_BY, NOTICE_DATE,
                                                startDate, customerName, customerAddress, customerMobile, customerPole, customerMeter, accountClass,
                                                connCategory, customerCA, looseOther1, looseOther2, looseFlag, cableNotInstallReason, noHappyCodeReason,
                                                smartMeterBool, smartMeterSimNo, smartMeterSimCode, SubDivisionCode);


        NewClassFile.WriteIntoFile_MCR("Insert status for MCR_Insert_INPUT_Data_RV_NEW " + _bSave.ToString());

        if (_bSave == true)
            NewClassFile.Update_MCRSealFlag(TERMINALSEAL1, ORDER_NO);

        if (TERMINALSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(TERMINALSEAL2, ORDER_NO);
        }
        if (METERBOXSEAL1 != "")
        {
            NewClassFile.Update_MCRSealFlag(METERBOXSEAL1, ORDER_NO);
        }
        if (METERBOXSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(METERBOXSEAL2, ORDER_NO);
        }
        if (BUSBARSEAL1 != "")
        {
            NewClassFile.Update_MCRSealFlag(BUSBARSEAL1, ORDER_NO);
        }
        if (BUSBARSEAL2 != "")
        {
            NewClassFile.Update_MCRSealFlag(BUSBARSEAL2, ORDER_NO);
        }


        if (_bSave == true)
        {
            if (looseFlag == "LOOSE")
                _bSave = NewClassFile.PunchLooseOrder_Installer_InputData(TAB_ID, DEVICE_NO);
            else
                _bSave = NewClassFile.PunchOrder_Installer_InputData(ORDER_NO, TAB_ID, DEVICE_NO);
        }

        NewClassFile.WriteIntoFile_MCR("PunchOrder_Installer_InputData " + _bSave.ToString());

        PREMISE_PHOTO = "";
        string strCaNo = NewClassFile.GetCAOnOrderNo(ORDER_NO);

        if (looseFlag == "LOOSE")
        {
            _bISave = NewClassFile.Insert_MCR_IamgeListNEW("941", TF_STCKR_NO, DEVICE_NO, MTR_PHOTO, CMPLT_MTR_INTALL_PHOTO, MCR_POLE_N_PHOTO,
                                             MCR_PHOTO, MCR_TST_RPT_PHOTO, MCR_LAB_TST_RPT_PHOTO, MCR_CONS_SIGN_PHOTO, MCR_OTHR_PHOTO,
                                             RMVD_MTR_PHOTO, RMVD_CMPLT_MTR_INTALL_PHOTO, PREMISE_PHOTO, DEVICE_NO, _sSAPFLAG);
        }
        else
        {
            _bISave = NewClassFile.Insert_MCR_IamgeListNEW(ORDER_NO, TF_STCKR_NO, DEVICE_NO, MTR_PHOTO, CMPLT_MTR_INTALL_PHOTO, MCR_POLE_N_PHOTO,
                                             MCR_PHOTO, MCR_TST_RPT_PHOTO, MCR_LAB_TST_RPT_PHOTO, MCR_CONS_SIGN_PHOTO, MCR_OTHR_PHOTO,
                                             RMVD_MTR_PHOTO, RMVD_CMPLT_MTR_INTALL_PHOTO, PREMISE_PHOTO, strCaNo, _sSAPFLAG);
        }

        NewClassFile.WriteIntoFile_MCR("Image Log " + _bISave.ToString());

        return _bISave;

    }


    private DataTable GetOrdType_PMAct_OrderWise(string strOrderID)
    {
        DataTable _dtOrd = new DataTable();
        _dtOrd = NewClassFile.GetOrderTypeData_OrdIDWise(strOrderID);
        return _dtOrd;
    }

    [WebMethod(EnableSession = true)]
    public DataTable MCR_GetCancelRmkDetails(string _sOrderType)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = NewClassFile.GetCancel_RemarksData(_sOrderType);
            dt.TableName = "MCR_COR_SYS_MST";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable MCR_GetSubDiv_Division(string _sDivision)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = NewClassFile.GetSubDivision_DivWise(_sDivision);
            dt.TableName = "MCR_COR_SYS_MST";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable MCR_GetUserMCR_INPUT_COMP_DT(string strUser)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable dt = new DataTable();
            dt = NewClassFile.Get_MCR_Complete_InputData(strUser);
            dt.TableName = "MCR_INPUT_DETAILS";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    #endregion

    #region Recovery Services

    [WebMethod(EnableSession = true)]
    public DataSet RecServices_Category(string strKeyParam)
    {
        DataSet ds = new DataSet("Recovery_Results");

        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            try
            {
                DataTable dtCategory = new DataTable();
                dtCategory = NewClassFile.bindCategory();
                dtCategory.TableName = "Category";
                ds.Tables.Add(dtCategory);
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in RecServices_Category Method : " + ex.ToString());
            }
        }
        else
        {
            DataTable dterror = InvaildAuthontication();
            ds.Tables.Add(dterror);
        }

        return ds;
    }

    [WebMethod(EnableSession = true)]
    public DataSet RecServices_Amount_Bucket(string strKeyParam)
    {
        DataSet ds = new DataSet("Recovery_Results");

        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            try
            {
                DataTable dtAmtBkt = new DataTable();
                dtAmtBkt = NewClassFile.bindAmountBucket();
                dtAmtBkt.TableName = "Amount_Bucket";
                ds.Tables.Add(dtAmtBkt);
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in RecServices_AmtBkt Method : " + ex.ToString());
            }
        }
        else
        {
            DataTable dterror = InvaildAuthontication();
            ds.Tables.Add(dterror);
        }

        return ds;
    }

    [WebMethod(EnableSession = true)]
    public DataSet RecServices_Aging_Bucket(string strKeyParam)
    {
        DataSet ds = new DataSet("Recovery_Results");

        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            try
            {
                DataTable dtAgiBkt = new DataTable();
                dtAgiBkt = NewClassFile.bindAgingBucket();
                dtAgiBkt.TableName = "Aging_Bucket";
                ds.Tables.Add(dtAgiBkt);
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in RecServices_AgiBkt Method : " + ex.ToString());
            }
        }
        else
        {
            DataTable dterror = InvaildAuthontication();
            ds.Tables.Add(dterror);
        }

        return ds;
    }

    [WebMethod(EnableSession = true)]
    public DataSet RecServices_Account_Class(string strKeyParam)
    {
        DataSet ds = new DataSet("Recovery_Results");

        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            try
            {
                DataTable dtAccountClass = new DataTable();
                dtAccountClass = NewClassFile.bindAccountClass();
                dtAccountClass.TableName = "Account_Class";
                ds.Tables.Add(dtAccountClass);
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in RecServices_AClass Method : " + ex.ToString());
            }
        }
        else
        {
            DataTable dterror = InvaildAuthontication();
            ds.Tables.Add(dterror);
        }

        return ds;
    }

    [WebMethod(EnableSession = true)]
    public DataSet RecServices_ATR_Status(string strKeyParam)
    {
        DataSet ds = new DataSet("Recovery_Results");

        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            try
            {
                DataTable dtATRStatus = new DataTable();
                dtATRStatus = NewClassFile.bindATRStatus();
                dtATRStatus.TableName = "ATR_Status";
                ds.Tables.Add(dtATRStatus);
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in RecServices_ATRStatus : " + ex.ToString());
            }
        }
        else
        {
            DataTable dterror = InvaildAuthontication();
            ds.Tables.Add(dterror);
        }

        return ds;
    }

    [WebMethod(EnableSession = true)]
    public DataTable RecAllocDefltrs(string strKeyParam, string _sCategory, string _sAmtBktID, string _sAgeBktID, string _sAccountClass, string _sDishonorFlag, string _sFEID)
    {
        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            DataTable dtAllocDefltr = new DataTable();
            try
            {
                dtAllocDefltr = NewClassFile.bindAllocDefltr(_sCategory, _sAmtBktID, _sAgeBktID, _sAccountClass, _sDishonorFlag, _sFEID);
                dtAllocDefltr.TableName = "Allocated_Defaulters";
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in RecAllocDefltrs Method : " + ex.ToString());
            }

            return dtAllocDefltr;
        }
        else
        {
            return InvaildAuthontication();

        }
    }

    [WebMethod(EnableSession = true)]
    public DataTable searchDefltrs(string strKeyParam, string _sCANumber, string _sMeterNumber, string _sFEID)
    {
        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            DataTable dtSearchDefltr = new DataTable();
            try
            {
                dtSearchDefltr = NewClassFile.searchDefltrs(_sCANumber, _sMeterNumber, _sFEID);
                dtSearchDefltr.TableName = "Searched_Defaulter";
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in searchDefltrs Method : " + ex.ToString());
            }

            return dtSearchDefltr;
        }
        else
        {
            return InvaildAuthontication();

        }
    }

    [WebMethod(EnableSession = true)]   //28022018
    public DataTable getPayment(string strKeyParam, string _sCANumber, string _sMeterNumber)
    {
        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            DataTable dtPayment = new DataTable();
            try
            {
                dtPayment = NewClassFile.getPayment(_sCANumber, _sMeterNumber);
                dtPayment.TableName = "Payment";
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in getPayment Method : " + ex.ToString());
            }
            return dtPayment;
        }
        else
        {
            return InvaildAuthontication();

        }
    }


    [WebMethod(EnableSession = true)]   //28022018
    public bool insertATR(string strKeyParam, string _sCANumber, string _sStatus, string _sFEID, string _sTabUpdStatus, string _sRemarks, string _sImgFlg, string _sImgPath, string _sFollowDate, string _sAltContNo, string _sAltEmailID, string _sMobileNo, string _sMessage, string _sUpdationID)
    {
        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            try
            {
                bool res = false;
                string ipAddress = HttpContext.Current.Request.UserHostAddress;
                //string strNewId = HttpContext.Current.Session.SessionID;                

                DataTable dtCheckATR = NewClassFile.chkRecordExist(_sCANumber, _sFEID, "DEFLTR_ATR");
                if (dtCheckATR.Rows.Count > 0)  // Update
                    res = NewClassFile.updateTable(_sCANumber, _sStatus, _sFEID, ipAddress, "DEFLTR_ATR", "", "", "", "", "", "", "");
                else    // Insert
                    res = NewClassFile.insertTable(_sCANumber, _sStatus, _sFEID, ipAddress, "DEFLTR_ATR");

                if (res)
                {
                    if (_sStatus == "05")   //PPL Case
                        NewClassFile.insertSMS(_sMobileNo, _sMessage, _sCANumber, _sUpdationID, ipAddress);

                    return NewClassFile.updateTable(_sCANumber, _sStatus, _sFEID, ipAddress, "DEFLTR_CA_ASSIGN_LINK_DTLS", _sTabUpdStatus, _sRemarks, _sImgFlg, _sImgPath, _sFollowDate, _sAltContNo, _sAltEmailID);
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in insertATR Method : " + ex.ToString());
                return false;
            }
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    public DataSet DetailofMIS(string strKeyParam, string _sFEID)
    {
        DataSet ds = new DataSet("Recovery_MISResults");

        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            try
            {
                DataTable dtAllocDefltrs = new DataTable();
                dtAllocDefltrs = NewClassFile.getSummaryData(_sFEID, "Allocated_Defaulters");
                dtAllocDefltrs.TableName = "Allocated_Defaulters";
                ds.Tables.Add(dtAllocDefltrs);

                DataTable dtPaymentReceived = new DataTable();
                dtPaymentReceived = NewClassFile.getSummaryData(_sFEID, "Payment_Received");
                dtPaymentReceived.TableName = "Payment_Received";
                ds.Tables.Add(dtPaymentReceived);

                DataTable dtMeterRemoved = new DataTable();
                dtMeterRemoved = NewClassFile.getSummaryData(_sFEID, "Meter_Removed");
                dtMeterRemoved.TableName = "Meter_Removed";
                ds.Tables.Add(dtMeterRemoved);

                DataTable dtDisconnection = new DataTable();
                dtDisconnection = NewClassFile.getSummaryData(_sFEID, "Disconnection");
                dtDisconnection.TableName = "Disconnection";
                ds.Tables.Add(dtDisconnection);

                DataTable dtOtherATR = new DataTable();
                dtOtherATR = NewClassFile.getSummaryData(_sFEID, "Other_ATR");
                dtOtherATR.TableName = "Other_ATR";
                ds.Tables.Add(dtOtherATR);

                DataTable dtPendingCases = new DataTable();
                dtPendingCases = NewClassFile.getSummaryData(_sFEID, "Pending_Cases");
                dtPendingCases.TableName = "Pending_Cases";
                ds.Tables.Add(dtPendingCases);
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in DetailofMIS Method : " + ex.ToString());
            }
        }
        else
        {
            DataTable dterror = InvaildAuthontication();
            ds.Tables.Add(dterror);
        }

        return ds;
    }

    [WebMethod(EnableSession = true)]   //21022018
    public DataTable loginFE(string strKeyParam, string _sLogin, string _sPassword, string _sIMEINo)
    {
        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            DataTable dtLogin = new DataTable();
            try
            {
                dtLogin = NewClassFile.clsLogin(_sLogin, _sPassword, _sIMEINo);
                dtLogin.TableName = "Login";
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in loginFE Method : " + ex.ToString());
            }

            return dtLogin;
        }
        else
        {
            return InvaildAuthontication();

        }
    }

    [WebMethod(EnableSession = true)]   //15022018
    public bool changePassword(string strKeyParam, string _sLogin, string _sOldPassword, string _sNewPassword)
    {
        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            try
            {
                bool res = NewClassFile.changePassword(_sLogin, _sOldPassword, _sNewPassword);

                if (res)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in changePassword Method : " + ex.ToString());
                return false;
            }
        }
        else
            return false;
    }

    [WebMethod(EnableSession = true)]   //15022018
    public bool insertLog(string strKeyParam, string _sFEID, string _sCANO, string _sMeterNo, string _sInTime, string _sOutTime, string _sNetStatus, string _sLatitude, string _sLongitude, string _sInsDate, string strOne, string strTwo, string strThree, string strFour)
    {
        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            try
            {
                bool res = NewClassFile.insertLog(_sFEID, _sCANO, _sMeterNo, _sInTime, _sOutTime, _sNetStatus, _sLatitude, _sLongitude, _sInsDate);

                if (res)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in insertLog Method : " + ex.ToString());
                return false;
            }
        }
        else
            return false;
    }



    [WebMethod(EnableSession = true)]
    public string RecServices_New(string strKeyParam)
    {
        string str = "";
        if (strKeyParam == "@$$!ntern@a|@ppRec")
        {
            DataTable dtCategory = new DataTable();
            try
            {
                dtCategory = NewClassFile.bindAmountBucket();
                dtCategory.TableName = "Category";

                str = DataTableToJsonWithStringBuilder(dtCategory);
            }
            catch (Exception ex)
            {
                NewClassFile newClassFile = new NewClassFile();
                newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in RecServices_New Method : " + ex.ToString());
                str = "";
            }
        }
        return str;
    }

    #endregion

    //Rajveer
    public string DataTableToJsonWithStringBuilder(DataTable table)
    {
        var jsonString = new StringBuilder();
        if (table.Rows.Count > 0)
        {
            jsonString.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                     + "\":" + "\""
                     + table.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                     + "\":" + "\""
                     + table.Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    jsonString.Append("}");
                }
                else
                {
                    jsonString.Append("},");
                }
            }
            jsonString.Append("]");
        }

        return jsonString.ToString();

    }



    #region GIS BYPL

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataSet Z_BAPI_DSS_ISU_CA_DISPLAY_GIS(string strCANumber) //Added Field POLE_NO
    //{
    //    if (checkConsumer(MethodBase.GetCurrentMethod().Name))
    //    {
    //        DataSet ds = new DataSet();

    //        DataTable dtOutputStatus = new DataTable();
    //        dtOutputStatus.TableName = "OutputStatusTable";
    //        dtOutputStatus.Columns.Add("Status");
    //        string strOutputStatus = "";

    //        try
    //        {


    //            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
    //            DataTable dt = new DataTable();
    //            if (strCANumber.Length < 12 && strCANumber.Length > 0)
    //                strCANumber = strCANumber.PadLeft(12, '0');
    //            dt = isu.Z_BAPI_CMS_ISU_CA_DISPLAY(strCANumber, "","","","","").Tables[0];

    //            if (dt.Rows.Count > 0)
    //            {
    //                strOutputStatus = "CA Number : " + strCANumber + " details fetch from ISU Service.";
    //                dtOutputStatus.Rows.Add(strOutputStatus);
    //                ds.Tables.Add(dt.Copy());
    //                ds.Tables.Add(dtOutputStatus);
    //                return ds;
    //            }
    //            else
    //            {

    //                NewClassFile newClassFile = new NewClassFile();
    //                ds = new DataSet("BAPI_RESULT");

    //                if (strCANumber.Length == 11)
    //                {
    //                    strCANumber = strCANumber.Substring(1, 10);
    //                }
    //                if (strCANumber.Length == 12)
    //                {
    //                    strCANumber = strCANumber.Substring(2, 10);
    //                }
    //                if (strCANumber.Length == 9)
    //                {
    //                    strCANumber = strCANumber.PadLeft(10, '0');
    //                }

    //                DataTable _dtSap = new DataTable("SAPDATA_ErrorDataTable");
    //                DataColumn dcol;

    //                dcol = new DataColumn();
    //                dcol.DataType = System.Type.GetType("System.String");
    //                dcol.ColumnName = "Message";
    //                _dtSap.Columns.Add(dcol);

    //                dcol = new DataColumn();
    //                dcol.DataType = System.Type.GetType("System.String");
    //                dcol.ColumnName = "Type";
    //                _dtSap.Columns.Add(dcol);

    //                DataRow row = _dtSap.NewRow();
    //                row["Message"] = "CA Number not found";
    //                row["Type"] = string.Empty;


    //                DataTable dtISUSTDTable = new DataTable("ISUSTDTable");
    //                dtISUSTDTable = newClassFile.GetRCMSAP_DataCAWise(strCANumber);

    //                if (dtISUSTDTable.Rows.Count > 0)
    //                {
    //                    ds.Merge(dtISUSTDTable.Copy());
    //                    strOutputStatus = "CA Number : " + strCANumber + " details fetch from RCM Service.";
    //                    dtOutputStatus.Rows.Add(strOutputStatus);
    //                    ds.Tables.Add(dtOutputStatus);
    //                    return ds;
    //                }
    //                else
    //                {
    //                    ds.Merge(_dtSap.Copy());
    //                    strOutputStatus = "CA Number : " + strCANumber + " details unable to fetch from ISU & RCM Service.";
    //                    dtOutputStatus.Rows.Add(strOutputStatus);
    //                    ds.Tables.Add(dtOutputStatus);
    //                    return ds;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {

    //            NewClassFile newClassFile = new NewClassFile();
    //            ds = new DataSet("BAPI_RESULT");

    //            if (strCANumber.Length == 11)
    //            {
    //                strCANumber = strCANumber.Substring(1, 10);
    //            }
    //            if (strCANumber.Length == 12)
    //            {
    //                strCANumber = strCANumber.Substring(2, 10);
    //            }
    //            if (strCANumber.Length == 9)
    //            {
    //                strCANumber = strCANumber.PadLeft(10, '0');
    //            }

    //            DataTable _dtSap = new DataTable("SAPDATA_ErrorDataTable");
    //            DataColumn dcol;

    //            dcol = new DataColumn();
    //            dcol.DataType = System.Type.GetType("System.String");
    //            dcol.ColumnName = "Message";
    //            _dtSap.Columns.Add(dcol);

    //            dcol = new DataColumn();
    //            dcol.DataType = System.Type.GetType("System.String");
    //            dcol.ColumnName = "Type";
    //            _dtSap.Columns.Add(dcol);

    //            DataRow row = _dtSap.NewRow();
    //            row["Message"] = "CA Number not found";
    //            row["Type"] = string.Empty;


    //            DataTable dtISUSTDTable = new DataTable("ISUSTDTable");
    //            dtISUSTDTable = newClassFile.GetRCMSAP_DataCAWise(strCANumber);

    //            if (dtISUSTDTable.Rows.Count > 0)
    //            {
    //                ds.Merge(dtISUSTDTable.Copy());
    //                strOutputStatus = "CA Number : " + strCANumber + " details fetch from RCM Service.";
    //                dtOutputStatus.Rows.Add(strOutputStatus);
    //                ds.Tables.Add(dtOutputStatus);
    //                return ds;
    //            }
    //            else
    //            {
    //                ds.Merge(_dtSap.Copy());
    //                strOutputStatus = "CA Number : " + strCANumber + " details unable to fetch from ISU & RCM Service.";
    //                dtOutputStatus.Rows.Add(strOutputStatus);
    //                ds.Tables.Add(dtOutputStatus);
    //                return ds;
    //            }
    //        }

    //        //return ds;
    //    }
    //    else
    //    {
    //        return (InvaildAuthonticationds());
    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataSet Z_BAPI_DSS_ISU_CA_DISPLAY_GIS(string strCANumber) 
    //{
    //    if (checkConsumer(MethodBase.GetCurrentMethod().Name))
    //    {
    //        DataSet ds = new DataSet();

    //        DataTable dtOutputStatus = new DataTable();
    //        dtOutputStatus.TableName = "OutputStatusTable";
    //        dtOutputStatus.Columns.Add("Status");
    //        string strOutputStatus = "";

    //        try
    //        {

    //            NewClassFile newClassFile = new NewClassFile();
    //            ds = new DataSet("BAPI_RESULT");

    //            if (strCANumber.Length == 11)
    //            {
    //                strCANumber = strCANumber.Substring(1, 10);
    //            }
    //            if (strCANumber.Length == 12)
    //            {
    //                strCANumber = strCANumber.Substring(2, 10);
    //            }
    //            if (strCANumber.Length == 9)
    //            {
    //                strCANumber = strCANumber.PadLeft(10, '0');
    //            }

    //            DataTable _dtSap = new DataTable("SAPDATA_ErrorDataTable");
    //            DataColumn dcol;

    //            dcol = new DataColumn();
    //            dcol.DataType = System.Type.GetType("System.String");
    //            dcol.ColumnName = "Message";
    //            _dtSap.Columns.Add(dcol);

    //            dcol = new DataColumn();
    //            dcol.DataType = System.Type.GetType("System.String");
    //            dcol.ColumnName = "Type";
    //            _dtSap.Columns.Add(dcol);

    //            DataRow row = _dtSap.NewRow();
    //            row["Message"] = "CA Number not found";
    //            row["Type"] = string.Empty;


    //            DataTable dtISUSTDTable = new DataTable("ISUSTDTable");
    //            dtISUSTDTable = newClassFile.GetRCMSAP_DataCAWise(strCANumber);

    //            if (dtISUSTDTable.Rows.Count > 0)
    //            {
    //                ds.Merge(dtISUSTDTable.Copy());
    //                strOutputStatus = "CA Number : " + strCANumber + " details fetch from RCM Service.";
    //                dtOutputStatus.Rows.Add(strOutputStatus);
    //                ds.Tables.Add(dtOutputStatus);
    //                return ds;
    //            }
    //            else
    //            {
    //                ds.Merge(_dtSap.Copy());
    //                strOutputStatus = "CA Number : " + strCANumber + " details unable to fetch from RCM Service.";
    //                dtOutputStatus.Rows.Add(strOutputStatus);
    //                ds.Tables.Add(dtOutputStatus);
    //                return ds;
    //            }

    //        }
    //        catch (Exception ex)
    //        {

    //            NewClassFile newClassFile = new NewClassFile();
    //            ds = new DataSet("BAPI_RESULT");

    //            if (strCANumber.Length == 11)
    //            {
    //                strCANumber = strCANumber.Substring(1, 10);
    //            }
    //            if (strCANumber.Length == 12)
    //            {
    //                strCANumber = strCANumber.Substring(2, 10);
    //            }
    //            if (strCANumber.Length == 9)
    //            {
    //                strCANumber = strCANumber.PadLeft(10, '0');
    //            }

    //            DataTable _dtSap = new DataTable("SAPDATA_ErrorDataTable");
    //            DataColumn dcol;

    //            dcol = new DataColumn();
    //            dcol.DataType = System.Type.GetType("System.String");
    //            dcol.ColumnName = "Message";
    //            _dtSap.Columns.Add(dcol);

    //            dcol = new DataColumn();
    //            dcol.DataType = System.Type.GetType("System.String");
    //            dcol.ColumnName = "Type";
    //            _dtSap.Columns.Add(dcol);

    //            DataRow row = _dtSap.NewRow();
    //            row["Message"] = "CA Number not found";
    //            row["Type"] = string.Empty;

    //            ds.Merge(_dtSap.Copy());
    //            strOutputStatus = "CA Number : " + strCANumber + " details unable to fetch from RCM Service. (" + ex.ToString() +")";
    //            dtOutputStatus.Rows.Add(strOutputStatus);
    //            ds.Tables.Add(dtOutputStatus);
    //            return ds;
    //        }            
    //    }
    //    else
    //    {
    //        return (InvaildAuthonticationds());
    //    }
    //}


    #endregion

    #region DERC Email A/D By Rajveer

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ZBAPI_UPDATE_TNO(string strCA_no, string strTelephone, string strMobile, string strEmail, string strLandmark, string strDISPATCH_CTRL)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //DELHIWSTESTD.WebService isu = new DELHIWSTESTD.WebService();
            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();

            if (strCA_no.Length < 12 && strCA_no.Length > 0)
                strCA_no = strCA_no.PadLeft(12, '0');
            dt = isu.ZBAPI_UPDATE_TNO(strCA_no, strTelephone, strMobile, strEmail, strLandmark, strDISPATCH_CTRL).Tables[0];

            return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }


    #endregion


    #region Added By Ajay For Active and De-active 03-05-2021

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ZBAPI_UPDATE_TNO_New(string strCA_no, string strTelephone, string strMobile, string strEmail, string strLandmark, string strDISPATCH_CTRL)
    {
        string strDISPATCH_CTRL_New = string.Empty;
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            if (strDISPATCH_CTRL == "A" || strDISPATCH_CTRL == "Z021")
            {
                strDISPATCH_CTRL_New = "A";
            }
            if (strDISPATCH_CTRL == "D" || strDISPATCH_CTRL == "Z013")
            {
                strDISPATCH_CTRL_New = "D";
            }

            if (strDISPATCH_CTRL == "C" || strDISPATCH_CTRL == "Z017")
            {
                strDISPATCH_CTRL_New = "C";
            }
            // Not required already decoded in delhi v2



            //  DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DelhiV2.WebService isu = new DelhiV2.WebService();//Added by prasoon dated 15122023 due to dispatch control not updated
            DataTable dt = new DataTable();

            if (strCA_no.Length < 12 && strCA_no.Length > 0)
                strCA_no = strCA_no.PadLeft(12, '0');


            dt = isu.ZBAPI_UPDATE_TNO(strCA_no, strTelephone, strMobile, strEmail, strLandmark, strDISPATCH_CTRL_New).Tables[0];

            return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }


    #endregion

    #region Appointment Slot Count

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ZBAPI_CNTAPP_DETAILMOB(string strOrderType, string strDiv, string strApp_DT, string strAPPTM, string strCount)
    {
        // strAPPTM = Flag
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {

            //if (strDiv == "W1PJB" || strDiv == "W1MDK" || strDiv == "W1NGL")
            //{

            DataTable _dtSlot = new DataTable();
            string _sTimeSlot = string.Empty;

            if (strAPPTM.Trim() == "Y")
            {
                _dtSlot = NewClassFile.GetTimeSlot(strAPPTM);

                for (int i = 0; i < _dtSlot.Rows.Count; i++)
                {
                    _sTimeSlot += _dtSlot.Rows[i][0].ToString();
                    _sTimeSlot += ",";
                }

                if (_sTimeSlot.Length > 1)
                    _sTimeSlot = _sTimeSlot.Substring(0, _sTimeSlot.Length - 1);

                DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
                DataSet ds = new DataSet();

                ds = isu.ZBAPI_CNT_APP_DETAIL_MOB(strOrderType, strDiv, strApp_DT, _sTimeSlot, strCount);

                DataTable _dtMaxSlot = new DataTable();
                string _sSlotMaxCnt = "0";
                _dtMaxSlot = NewClassFile.GetMaxCnt_TimeSlot(strDiv);
                if (_dtMaxSlot.Rows.Count > 0)
                {
                    if (_dtMaxSlot.Rows[0][0] != null)
                        _sSlotMaxCnt = _dtMaxSlot.Rows[0][0].ToString();
                }

                DataTable dtOutPut = new DataTable();
                dtOutPut.TableName = "OutPutTable";
                dtOutPut.Columns.Add("Slot");

                for (int t = 0; t < ds.Tables[0].Rows.Count; t++)
                {
                    if (Convert.ToInt16(ds.Tables[0].Rows[t]["REC_COUNT"].ToString()) < Convert.ToInt16(_sSlotMaxCnt))
                    {
                        DataRow dr = dtOutPut.NewRow();
                        for (int m = 0; m < _dtSlot.Rows.Count; m++)
                        {
                            if (ds.Tables[0].Rows[t]["APPOINTMENT_TIME"].ToString() == _dtSlot.Rows[m][0].ToString())
                                dtOutPut.Rows.Add(_dtSlot.Rows[m][1].ToString());
                        }
                    }
                }

                dtOutPut.AcceptChanges();
                return dtOutPut;
            }
            else if (strAPPTM.Trim() == "S")
            {
                _dtSlot = NewClassFile.GetTimeSlot(strAPPTM);

                for (int i = 0; i < _dtSlot.Rows.Count; i++)
                {
                    _sTimeSlot += _dtSlot.Rows[i][0].ToString();
                    _sTimeSlot += ",";
                }

                if (_sTimeSlot.Length > 1)
                    _sTimeSlot = _sTimeSlot.Substring(0, _sTimeSlot.Length - 1);

                DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
                DataSet ds = new DataSet();

                ds = isu.ZBAPI_CNT_APP_DETAIL_MOB(strOrderType, strDiv, strApp_DT, _sTimeSlot, strCount);

                DataTable _dtMaxSlot = new DataTable();
                string _sSlotMaxCnt = "0";
                _dtMaxSlot = NewClassFile.GetMaxCnt_TimeSlot(strDiv);
                if (_dtMaxSlot.Rows.Count > 0)
                {
                    if (_dtMaxSlot.Rows[0][0] != null)
                        _sSlotMaxCnt = _dtMaxSlot.Rows[0][0].ToString();
                }

                DataTable dtOutPut = new DataTable();
                dtOutPut.TableName = "OutPutTable";
                dtOutPut.Columns.Add("Slot");

                for (int t = 0; t < ds.Tables[0].Rows.Count; t++)
                {
                    if (Convert.ToInt16(ds.Tables[0].Rows[t]["REC_COUNT"].ToString()) < Convert.ToInt16(_sSlotMaxCnt))
                    {
                        DataRow dr = dtOutPut.NewRow();
                        for (int m = 0; m < _dtSlot.Rows.Count; m++)
                        {
                            if (ds.Tables[0].Rows[t]["APPOINTMENT_TIME"].ToString() == _dtSlot.Rows[m][0].ToString())
                                dtOutPut.Rows.Add(_dtSlot.Rows[m][1].ToString());
                        }
                    }
                }

                dtOutPut.AcceptChanges();
                return dtOutPut;
            }
            else
            {
                DataTable _dtTimeSlot = NewClassFile.GetTimeSlot(strAPPTM);

                DataTable dtOutPut = new DataTable();
                dtOutPut.TableName = "OutPutTable";
                dtOutPut.Columns.Add("Slot");

                for (int m = 0; m < _dtTimeSlot.Rows.Count; m++)
                {
                    DataRow dr = dtOutPut.NewRow();
                    dtOutPut.Rows.Add(_dtTimeSlot.Rows[m][1].ToString());
                }

                dtOutPut.AcceptChanges();
                return dtOutPut;
            }
            //}
            //else
            //{
            //    return (InvaildAuthontication());
            //}
        }
        else
        {
            return (InvaildAuthontication());
        }
    }

    #endregion

    #region "BSES Mobi App New Services"

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]

    public DataSet ZBAPI_CREATESO_POST(string strKeyParam, string strPMAufart, string strPlanPlant, string strRegioGroup, string strShortText, string strILA, string strMFText,
                                   string strUserFieldCH20, string StrControkey, string strSerialNumber, string strComplaintGroup, string strCANumber,
                                   string strContract, string strMFText1)
    {
        if (strKeyParam == "@$$!ntern@a|@ppMobi")
        {
            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataSet ds_PostData = new DataSet();
            DataTable dt = new DataTable();

            if (strCANumber.Length < 12 && strCANumber.Length > 0)
                strCANumber = strCANumber.PadLeft(12, '0');

            dt = isu.Z_BAPI_DSS_ISU_CA_DISPLAY(strCANumber, "").Tables[0];

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Reg_Str_Group"] != null)
                    strRegioGroup = dt.Rows[0]["Reg_Str_Group"].ToString();

                if (dt.Rows[0]["Device_Sr_Number"] != null)
                    strSerialNumber = dt.Rows[0]["Device_Sr_Number"].ToString();

                // strSerialNumber = strSerialNumber.Replace("0", "");
                strPlanPlant = GetPlanPlat(strRegioGroup);

                if (strPlanPlant == "D021")
                    StrControkey = "ZWM1";
                else
                    StrControkey = "ZWM2";

                ds_PostData = isu.ZBAPI_CREATESO_POST(strPMAufart, strPlanPlant, strRegioGroup, strShortText, strILA, strMFText, strUserFieldCH20, StrControkey,
                                                        strSerialNumber, strComplaintGroup, strCANumber, strContract, strMFText1);
                return ds_PostData;
            }
            else
                return InvaildAuthonticationds();
        }
        else
        {
            return InvaildAuthonticationds();
        }
    }


    public string GetPlanPlat(string _sRegionGrp)
    {
        string _sPlanPlat = string.Empty;
        if ((_sRegionGrp == "S1ALN") || (_sRegionGrp == "S1KHP") || (_sRegionGrp == "S1NHP") || (_sRegionGrp == "S1NZD") || (_sRegionGrp == "S1SVR") || (_sRegionGrp == "S2HKS") ||
            (_sRegionGrp == "S2RKP") || (_sRegionGrp == "S2SKT") || (_sRegionGrp == "S2VKJ") || (_sRegionGrp == "W1JFR") || (_sRegionGrp == "W1MDK") || (_sRegionGrp == "W1NGL") || (_sRegionGrp == "W1NJF")
            || (_sRegionGrp == "W1PJB") || (_sRegionGrp == "W2DWK") || (_sRegionGrp == "W2JKP") || (_sRegionGrp == "W2PLM") || (_sRegionGrp == "W2TGN") || (_sRegionGrp == "W2VKP")
            || (_sRegionGrp == "W2MGN") || (_sRegionGrp == "W2UTN"))
            _sPlanPlat = "D021";
        else if ((_sRegionGrp == "CECCK") || (_sRegionGrp == "CEDRG") || (_sRegionGrp == "CEPHG") || (_sRegionGrp == "CEPNR") || (_sRegionGrp == "CESRD") ||
                (_sRegionGrp == "ENGTR") || (_sRegionGrp == "ENKWR") || (_sRegionGrp == "ENNNG") || (_sRegionGrp == "ENYVR") || (_sRegionGrp == "ESKKD") || (_sRegionGrp == "ESKNR")
            || (_sRegionGrp == "ESLNR") || (_sRegionGrp == "ESMVR") || (_sRegionGrp == "ESVSE"))
            _sPlanPlat = "D031";

        return _sPlanPlat;
    }

    #endregion

    #region LR New Webmethods

    [WebMethod]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Scheme_DivSchmMapping(string strKeyParam, string circle, string division, string subdivision, string scheme)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.MobApp_Scheme_Select(circle, division, subdivision, scheme);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    [WebMethod]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Scheme_DivSchmMapping_110524(string strKeyParam, string circle, string division, string subdivision, string scheme)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.MobApp_Scheme_Select_110524(circle, division, subdivision, scheme);
        }
        else
        {
            return InvaildAuthontication();
        }
    }




    [WebMethod]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Scheme_AllDivSchmMapping(string strKeyParam, string circle, string division, string subdivision, string scheme)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.MobApp_Scheme_ALL(circle, division, subdivision, scheme);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_NewActivityModuleSchemeMIS(string strKeyParam, string strDivName, string strRolRght, string strDate, string strToDate, string strSubDiv)
    {
        DataTable dt = new DataTable();

        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            //For Division
            if (strRolRght == "ALLD")
            {
                dt = NewClassFile.LR_ActivityModuleSchemeMIS_DIV(strDivName, strRolRght, strDate, strToDate);
                return dt;
            }
            if (strRolRght == "SLCD")
            {
                dt = NewClassFile.LR_ActivityModuleSchemeMIS_SDIV(strDivName, strRolRght, strDate, strToDate);
                return dt;
            }

            //Sub Division
            if (strRolRght == "ALLSD")
            {
                dt = NewClassFile.LR_ActivityModuleSchemeMIS_SUBDIV(strDivName, strRolRght, strDate, strToDate);
                return dt;
            }
            if (strRolRght == "SLCSD")
            {
                dt = NewClassFile.LR_ActivityModuleSchemeMIS_SSUBDIV(strDivName, strRolRght, strDate, strToDate);
                return dt;
            }

            //Scheme
            if (strRolRght == "ALLSCHM")
            {
                dt = NewClassFile.LR_ActivityModuleSchemeMIS_SCHM(strDivName, strRolRght, strDate, strToDate);
                return dt;
            }
            if (strRolRght == "SLCSCHM")
            {
                dt = NewClassFile.LR_ActivityModuleSchemeMIS_SLCDSCHM(strDivName, strRolRght, strDate, strToDate);
                return dt;
            }

            return NewClassFile.LR_ActivityModuleSchemeMIS_List(strDivName, strRolRght, strDate, strToDate);
        }
        else
        {
            return InvaildAuthontication();
        }
    }



    //[WebMethod(EnableSession = true)]
    //[SoapHeader("consumer", Required = true)]
    //public DataTable LR_NewSurvellanceCircleDivMIS(string strKeyParam, string strDivName, string strRolRght, string frmDate, string toDate, string strSubDiv)
    //{
    //    if (strKeyParam == "@$$!ntern@a|@pp")
    //    {
    //        DataTable dt = new DataTable();
    //        //strDivName , strSubDiv , strRolRght
    //        //Circle
    //        if (strDivName == "ALLC")
    //        {
    //            dt = NewClassFile.LR_ActivityModuleSchemeMIS_ALLC(strDivName, strRolRght, frmDate, toDate, strSubDiv);
    //            return dt;
    //        }
    //        if (strDivName == "SLCC")
    //        {
    //            dt = NewClassFile.LR_ActivityModuleSchemeMIS_SLCDC(strDivName, strRolRght, frmDate, toDate, strSubDiv);
    //            return dt;
    //        }

    //        //For Division
    //        if (strDivName == "ALLD")
    //        {
    //            dt = NewClassFile.LR_ActivityModuleSchemeMIS_ALLD(strDivName, strRolRght, frmDate, toDate, strSubDiv);
    //            return dt;
    //        }
    //        if (strDivName == "SLCD")
    //        {
    //            dt = NewClassFile.LR_ActivityModuleSchemeMIS_SLCD(strDivName, strRolRght, frmDate, toDate, strSubDiv);
    //            return dt;
    //        }

    //        //Sub Division
    //        if (strDivName == "SLCSD")
    //        {
    //            dt = NewClassFile.LR_ActivityModuleSchemeMIS_SLCSD(strDivName, strRolRght, frmDate, toDate, strSubDiv);
    //            return dt;
    //        }

    //        NewClassFile newClassFile = new NewClassFile();
    //        return newClassFile.LR_SurvellanceCircleDivMIS_List(strDivName, strRolRght);
    //    }
    //    else
    //    {
    //        return InvaildAuthontication();
    //    }
    //}



    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_NewSurvellanceCircleDivMIS(string strKeyParam, string strDivName, string strRolRght, string frmDate, string toDate, string strSubDiv)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            DataTable dt = new DataTable();
            //strDivName , strSubDiv , strRolRght
            //Circle
            if (strDivName == "ALLC")
            {
                dt = NewClassFile.LR_ActivityModuleSchemeMIS_ALLC(strDivName, strRolRght, frmDate, toDate, strSubDiv);
                return dt;
            }
            if (strDivName == "SLCC")
            {
                dt = NewClassFile.LR_ActivityModuleSchemeMIS_SLCDC(strDivName, strRolRght, frmDate, toDate, strSubDiv);
                return dt;
            }

            //For Division
            if (strDivName == "ALLD")
            {
                dt = NewClassFile.LR_ActivityModuleSchemeMIS_ALLD(strDivName, strRolRght, frmDate, toDate, strSubDiv);
                return dt;
            }
            if (strDivName == "SLCD")
            {
                dt = NewClassFile.LR_ActivityModuleSchemeMIS_SLCD(strDivName, strRolRght, frmDate, toDate, strSubDiv);
                return dt;
            }

            //Sub Division
            if (strDivName == "SLCSD")
            {
                dt = NewClassFile.LR_ActivityModuleSchemeMIS_SLCSD(strDivName, strRolRght, frmDate, toDate, strSubDiv);
                return dt;
            }

            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.LR_SurvellanceCircleDivMIS_List(strDivName, strRolRght);
        }
        else
        {
            return InvaildAuthontication();
        }
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_NEW_QC_MIS(string strKeyParam, string strDivName, string strRolRght, string frmDate, string toDate, string strSubDiv)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            DataTable dt = new DataTable();

            //Circle
            if (strDivName == "ALLC")
            {
                dt = NewClassFile.LR_QC_MIS_List_ALLC(strDivName, strRolRght, frmDate, toDate, strSubDiv);
                return dt;
            }
            if (strDivName == "SLCC")
            {
                dt = NewClassFile.LR_QC_MIS_List_SLCCC(strDivName, strRolRght, frmDate, toDate, strSubDiv);
                return dt;
            }

            //For Division
            if (strDivName == "ALLD")
            {
                dt = NewClassFile.LR_QC_MIS_List_ALLD(strDivName, strRolRght, frmDate, toDate, strSubDiv);
                return dt;
            }
            if (strDivName == "SLCD")
            {
                dt = NewClassFile.LR_QC_MIS_List_SLCD(strDivName, strRolRght, frmDate, toDate, strSubDiv);
                return dt;
            }

            //Sub Division
            if (strDivName == "SLCSD")
            {
                dt = NewClassFile.LR_QC_MIS_List_SLCSD(strDivName, strRolRght, frmDate, toDate, strSubDiv);
                return dt;
            }

            //Scheme
            if (strDivName == "SLCSCHM")
            {
                dt = NewClassFile.LR_QC_MIS_List_SLCSCHM(strDivName, strRolRght, frmDate, toDate, strSubDiv);
                return dt;
            }

            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.LR_QC_MIS_List(strDivName, strRolRght);
        }
        else
        {
            return InvaildAuthontication();
        }
    }


    #endregion

    #region"Seva Kendra"
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet Seva_get_Login_Details(string strUser_id, string strPassword, string strIMEI, string strLongitude, string strLatitude)
    {
        DataSet ds = new DataSet();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable _dsTable = NewClassFile.get_Seva_Kendra_Login(strUser_id, strPassword, strIMEI, strLongitude, strLatitude);
            ds.Tables.Add(_dsTable);
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }

    [WebMethod]
    public string Seva_change_Password(string strUser_id, string strCurrent_Password, string strNew_Password)
    {
        string result = "false";
        try
        {
            DataTable _dtDetails = NewClassFile.get_SevaKendra_Login_Details(strUser_id.Trim(), strCurrent_Password.Trim());
            if (_dtDetails.Rows.Count > 0)
            {
                result = (NewClassFile.update_Seva_User_Password(strUser_id, strCurrent_Password, strNew_Password)).ToString();
            }
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in Update Password Method : " + ex.ToString());
            result = "false";
        }

        return result;
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet Seva_get_Order_Details(string strUser_id, string strDivision)
    {
        DataSet ds = new DataSet();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            string _gDivision = strDivision.Replace(",", "','");
            DataTable _dsTable = NewClassFile.get_SevaKendra_Order_Details(strUser_id, _gDivision);
            ds.Tables.Add(_dsTable);
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet Seva_get_Order_Wise_Complete_Details(string strOrder_no)
    {
        DataSet ds = new DataSet();
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataTable _dsTable = NewClassFile.get_SevaKendra_Order_Wise_Complete_Details(strOrder_no);
            ds.Tables.Add(_dsTable);
            return ds;
        }
        else
            return (InvaildAuthonticationds());
    }


    [WebMethod]
    public string Seva_Insert_Data_New_Conn(string strKYC, string strProfile, string strTechDetails, string strORDER_NO, string strREGTYPE, string strFIRST_NAME,
                                            string strMIDDLE_NAME, string strLAST_NAME, string strTITLE, string strGENDER, string strDOB, string strFATHER_NAME,
                                            string strMOTHER_NAME, string strDESIGNATION_AS, string strTYPE_ORG, string strDOI, string strHOUSE_NO,
                                            string strBUILDING_NAME, string strSTREET, string strAREA, string strPIN, string strLANDMARK, string strMOBILE_NO,
                                            string strPHONE_NO, string strEMAIL, string strHOUSE_NO_PA, string strBUILDING_NAME_PA, string strSTREET_PA,
                                            string strAREA_PA, string strPIN_PA, string strLANDMARK_PA, string strMOBILE_NO_PA, string strPHONE_NO_PA,
                                            string strEMAIL_PA, string strAPPLIED_CATEGORY, string strNEWOREXISTING, string strSERVICE_REQ, string strBILLING_TYPE,
                                            string strAREA_TYPE, string strPREMISES_TYPE, string strPURPOSE, string strMETER_CHOICE, string strAPPLIED_LOAD,
                                            string strAPPLIED_VOLTAGE_LVL, string strAPPLIED_PHASE, string strPAN_NO, string strID_NO, string strPIC_NAME,
                                            string strSIG_NAME, string strCOMPANY, string strLOAD_TYPE, string strAADHAR_NO, string strFN_AS, string strMN_AS,
                                            string strLN_AS, string strFINGER_NAME, string strDOA, string strCF_REMARK, string strZZ_CONNTYPE, string strUser_id)
    {
        string result = "false";
        string _PicName = string.Empty, _SIG_NAME = string.Empty;
        try
        {

            if (strKYC == "1")
                Seva_Update_KYC(strORDER_NO, strUser_id, strMOBILE_NO, strEMAIL);
            if (strProfile == "1")
                Seva_Update_Prsnal_Information(strORDER_NO, strUser_id, strLAST_NAME, strFIRST_NAME, strFATHER_NAME, strMIDDLE_NAME, strMOBILE_NO, strEMAIL, strBUILDING_NAME, strHOUSE_NO, strSTREET, strAREA, strLANDMARK);

            if (strPIC_NAME != "")
                _PicName = byteArrayToImage_SevaKendra(strPIC_NAME, strCOMPANY, strORDER_NO, "1");
            if (strSIG_NAME != "")
                _SIG_NAME = byteArrayToImage_SevaKendra(strSIG_NAME, strCOMPANY, strORDER_NO, "2");

            if (NewClassFile.Seva_kendra_insertRequest(strORDER_NO, strREGTYPE, strFIRST_NAME, strMIDDLE_NAME, strLAST_NAME, strTITLE, strGENDER, strDOB, strFATHER_NAME, strMOTHER_NAME, strFN_AS, strMN_AS, strLN_AS, strDESIGNATION_AS, strREGTYPE, strDOI,
              strHOUSE_NO, strBUILDING_NAME, strSTREET, strAREA, strPIN, strLANDMARK, strMOBILE_NO, strLANDMARK, strEMAIL, strHOUSE_NO_PA, strBUILDING_NAME_PA, strSTREET_PA, strAREA_PA, strPIN_PA, strLANDMARK_PA, strMOBILE_NO_PA, strLANDMARK_PA,
              strEMAIL_PA, strAPPLIED_CATEGORY, strNEWOREXISTING, strSERVICE_REQ, strBILLING_TYPE, strAREA_TYPE, strPREMISES_TYPE, strPURPOSE, strMETER_CHOICE, strLOAD_TYPE, strAPPLIED_LOAD, strAPPLIED_VOLTAGE_LVL, strAPPLIED_PHASE, strPAN_NO, strAADHAR_NO, strID_NO,
             _PicName, _SIG_NAME, strFINGER_NAME, strCOMPANY, strDOA, "", strZZ_CONNTYPE) == "true")
            {
                // NewClassFile.Seva_Kendra_cancelOrderNO(strORDER_NO, strUser_id, "X", "OK", strUser_id);

                if (strCOMPANY == "BYPL")
                {
                    result = (NewClassFile.Seva_UpdateSapService(strORDER_NO, "ICFP", "ICFP at the time of New Connection for BYPL", strUser_id)).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in Save Data New Connection Method : " + ex.ToString());
            result = "false";
        }

        return result;
    }


    public void Seva_Update_KYC(string strOrderNO, string User_ID, string MobileNO, string EmailID)
    {
        DataTable _gdt = NewClassFile.seva_getPresonalInformation(strOrderNO);
        if (_gdt.Rows.Count > 0)
        {
            if (NewClassFile.Seva_UpdateInformation(strOrderNO, _gdt.Rows[0]["KUNUM"].ToString().Trim(), _gdt.Rows[0]["NAME_LAST"].ToString().Trim(), _gdt.Rows[0]["NAME_FIRST"].ToString().Trim(), _gdt.Rows[0]["NAME_LAST2"].ToString().Trim(), _gdt.Rows[0]["NAMEMIDDLE"].ToString().Trim(), _gdt.Rows[0]["TEL_NUMBER"].ToString().Trim(), _gdt.Rows[0]["E_MAIL"].ToString().Trim(), _gdt.Rows[0]["STREET"].ToString().Trim(), _gdt.Rows[0]["HOUSE_NUM1"].ToString().Trim(), _gdt.Rows[0]["STR_SUPPL1"].ToString().Trim(), _gdt.Rows[0]["STR_SUPPL2"].ToString().Trim(), _gdt.Rows[0]["STR_SUPPL3"].ToString().Trim(), User_ID) == true)
            {
                NewClassFile.Seva_UpdateIntoMSTForKYC(strOrderNO, _gdt.Rows[0]["KUNUM"].ToString().Trim(), MobileNO, EmailID);
                NewClassFile.Seva_UpdateSapServiceTable(strOrderNO, "KYCUpdate", "Update KYC.", User_ID);
            }
        }
    }

    public void Seva_Update_Prsnal_Information(string strOrderNO, string User_ID, string LName, string Fname, string L2Name, string MName, string MobileNumber, string Email, string Street, string HouseNumber, string Street1, string Street2, string Street3)
    {
        DataTable _gdt = NewClassFile.seva_getPresonalInformation(strOrderNO);
        if (_gdt.Rows.Count > 0)
        {
            if (NewClassFile.Seva_UpdateInformation(strOrderNO, _gdt.Rows[0]["KUNUM"].ToString().Trim(), _gdt.Rows[0]["NAME_LAST"].ToString().Trim(), _gdt.Rows[0]["NAME_FIRST"].ToString().Trim(), _gdt.Rows[0]["NAME_LAST2"].ToString().Trim(), _gdt.Rows[0]["NAMEMIDDLE"].ToString().Trim(), _gdt.Rows[0]["TEL_NUMBER"].ToString().Trim(), _gdt.Rows[0]["E_MAIL"].ToString().Trim(), _gdt.Rows[0]["STREET"].ToString().Trim(), _gdt.Rows[0]["HOUSE_NUM1"].ToString().Trim(), _gdt.Rows[0]["STR_SUPPL1"].ToString().Trim(), _gdt.Rows[0]["STR_SUPPL2"].ToString().Trim(), _gdt.Rows[0]["STR_SUPPL3"].ToString().Trim(), User_ID) == true)
            {
                NewClassFile.Seva_UpdateIntoMST(strOrderNO, _gdt.Rows[0]["KUNUM"].ToString().Trim(), LName.ToUpper(), Fname.ToUpper(), L2Name.ToUpper(), MName.ToUpper(), MobileNumber.ToUpper(), Email.ToUpper(), Street.ToUpper(), HouseNumber.ToUpper(), Street1.ToUpper(), Street2.ToUpper(), Street3.ToUpper());
                NewClassFile.Seva_UpdateSapServiceTable(strOrderNO, "Update", "Update Sap Data Like Name etc.", User_ID);
            }
        }
    }

    public string byteArrayToImage_SevaKendra(string strImage1, string _Company, string _OrderNo, string Type)
    {
        NetworkDrive oNetDrive = new NetworkDrive();
        try
        {
            string pth = @"\\10.125.64.235\sevakendra";
            string username = "sevakendra";
            string password = "bses@123";

            oNetDrive.LocalDrive = "ZZ:";
            oNetDrive.ShareName = pth;

            oNetDrive.MapDrive(username, password);
            string sl = oNetDrive.ShareName;

            string _sDir = sl + "\\" + _Company + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "\\" + _OrderNo;

            DirectoryInfo _DirInfo = new DirectoryInfo(_sDir);
            if (_DirInfo.Exists == false)
                _DirInfo.Create();

            string NameFile = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss") + "_" + Type;
            String Filename = _sDir + "\\" + NameFile + ".png";

            string Pic_Path = Filename;
            //HttpContext.Current.Session["Name"] = NameFile;
            using (FileStream fs = new FileStream(Pic_Path, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(strImage1);
                    bw.Write(data);
                    bw.Close();
                }
            }

            return Pic_Path;
        }
        catch (Exception ex)
        {
            return "";
        }
        finally
        {
            oNetDrive.UnMapDrive();
        }
    }


    [WebMethod]
    public string Seva_Insert_Data_Other_Connection(string strKYC, string strORDER_NO, string strCA_NUMBER, string strReqType, string strFIRST_NAME, string strMIDDLE_NAME, string strLAST_NAME, string strTITLE, string strGENDER, string strDOB, string strFATHER_NAME, string strsMOTHER_NAME, string strFN_AS, string strMN_AS, string strLN_AS, string strDESIGNATION_AS, string strTYPE_ORG, string strDOI, string strHOUSE_NO, string strBUILDING_NAME, string strSTREET, string strAREA, string strPIN, string strLANDMARK, string strMOBILE_NO, string strPHONE_NO, string strEMAIL, string strPAN_NO, string strID_NO, string strREASON_S3,
                                         string strNAME_S3, string strGENDER_S3, string strDOB_S3, string strFATHEERNAME_S3, string strELOAD_S4, string strRLOAD_S4, string strMETERCHOICE_S4, string strVLEVEL_S4, string strPHASE_S4, string strPURPOSE_S5, string strDESC_S5, string strREASON_S6, string strHOUSENO_S6, string strBNAME_S6, string strSTREET_S6, string strAREA_S6, string strPIN_S6, string strLANDMARK_S6, string strLBP_S7, string strREASON_S8, string strDOV_S8, string strMODE_S8, string strREASON_S9, string strUPTODATE_S9,
                                         string strAUTO_DEBIT_S10, string strImgName, string strSignName, string strCompany, string strDOA, string strRemark, string strUser_ID, string strIDName)
    {
        string result = "false";
        string _PicName = string.Empty, _SIG_NAME = string.Empty, _ID_Name = string.Empty;
        try
        {
            if (strKYC == "1")
                Seva_Update_KYC(strORDER_NO, strUser_ID, strMOBILE_NO, strEMAIL);

            if (strImgName != "")
                _PicName = byteArrayToImage_SevaKendra(strImgName, strCompany, strORDER_NO, "1");
            if (strSignName != "")
                _SIG_NAME = byteArrayToImage_SevaKendra(strSignName, strCompany, strORDER_NO, "2");
            if (strIDName != "")
                _ID_Name = byteArrayToImage_SevaKendra(strIDName, strCompany, strORDER_NO, "3");

            if (NewClassFile.Seva_Kendra_InsertConnctChange(strORDER_NO, strCA_NUMBER, strReqType, strReqType, strFIRST_NAME, strMIDDLE_NAME, strLAST_NAME, strTITLE, strGENDER, strDOB, strFATHER_NAME, strsMOTHER_NAME, strFN_AS, strMN_AS, strLN_AS, strDESIGNATION_AS, strTYPE_ORG, strDOI, strHOUSE_NO, strBUILDING_NAME, strSTREET, strAREA, strPIN, strLANDMARK, strMOBILE_NO, strPHONE_NO, strEMAIL, strPAN_NO, strID_NO, strREASON_S3, strNAME_S3, strGENDER_S3, strDOB_S3, strFATHEERNAME_S3, strELOAD_S4, strRLOAD_S4, strMETERCHOICE_S4, strVLEVEL_S4, strPHASE_S4
                 , strPURPOSE_S5, strDESC_S5, strREASON_S6, strHOUSENO_S6, strBNAME_S6, strSTREET_S6, strAREA_S6, strPIN_S6, strLANDMARK_S6, strLBP_S7, strREASON_S8, strDOV_S8, strMODE_S8, strREASON_S9, strUPTODATE_S9, strAUTO_DEBIT_S10,
                                         _PicName, "Mobile", "", _SIG_NAME, "", "", strCompany, strDOA, strRemark, _ID_Name, "", "") == "true")
            {
                // NewClassFile.Seva_Kendra_cancelOrderNO(strORDER_NO, strUser_ID, "X", "OK", strUser_ID);

                if (strCompany == "BYPL")
                {
                    result = (NewClassFile.Seva_UpdateSapService(strORDER_NO, "TALR", "TALR at the time of Change Connection for BYPL", strUser_ID)).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + "Error in Save Data New Connection Method : " + ex.ToString());
            result = "false";
        }

        return result;

    }
    #endregion

    #region GIS

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataSet Z_BAPI_DSS_ISU_CA_DISPLAY_GIS(string strCANumber, string strMetrNo)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DataSet ds = new DataSet();

            DataTable dtOutputStatus = new DataTable();
            dtOutputStatus.TableName = "OutputStatusTable";
            dtOutputStatus.Columns.Add("Status");
            string strOutputStatus = "";

            try
            {

                NewClassFile newClassFile = new NewClassFile();
                ds = new DataSet("BAPI_RESULT");

                if (strCANumber.Length == 11)
                {
                    strCANumber = strCANumber.Substring(1, 10);
                }
                if (strCANumber.Length == 12)
                {
                    strCANumber = strCANumber.Substring(2, 10);
                }
                if (strCANumber.Length == 9)
                {
                    strCANumber = strCANumber.PadLeft(10, '0');
                }

                //if (strMetrNo.ToString() != "") {

                //}

                DataTable _dtSap = new DataTable("SAPDATA_ErrorDataTable");
                DataColumn dcol;

                dcol = new DataColumn();
                dcol.DataType = System.Type.GetType("System.String");
                dcol.ColumnName = "Message";
                _dtSap.Columns.Add(dcol);

                dcol = new DataColumn();
                dcol.DataType = System.Type.GetType("System.String");
                dcol.ColumnName = "Type";
                _dtSap.Columns.Add(dcol);

                DataRow row = _dtSap.NewRow();
                row["Message"] = "CA Number not found";
                row["Type"] = string.Empty;


                DataTable dtISUSTDTable = new DataTable("ISUSTDTable");
                dtISUSTDTable = newClassFile.GetRCMSAP_DataCAWise(strCANumber, strMetrNo);

                if (dtISUSTDTable.Rows.Count > 0)
                {
                    ds.Merge(dtISUSTDTable.Copy());
                    strOutputStatus = "CA Number : " + strCANumber + "Meter Number : " + strMetrNo + " details fetch from RCM Service.";
                    dtOutputStatus.Rows.Add(strOutputStatus);
                    ds.Tables.Add(dtOutputStatus);
                    return ds;
                }
                else
                {
                    ds.Merge(_dtSap.Copy());
                    strOutputStatus = "CA Number : " + strCANumber + "Meter Number : " + strMetrNo + " details unable to fetch from RCM Service.";
                    dtOutputStatus.Rows.Add(strOutputStatus);
                    ds.Tables.Add(dtOutputStatus);
                    return ds;
                }

            }
            catch (Exception ex)
            {

                NewClassFile newClassFile = new NewClassFile();
                ds = new DataSet("BAPI_RESULT");

                if (strCANumber.Length == 11)
                {
                    strCANumber = strCANumber.Substring(1, 10);
                }
                if (strCANumber.Length == 12)
                {
                    strCANumber = strCANumber.Substring(2, 10);
                }
                if (strCANumber.Length == 9)
                {
                    strCANumber = strCANumber.PadLeft(10, '0');
                }

                DataTable _dtSap = new DataTable("SAPDATA_ErrorDataTable");
                DataColumn dcol;

                dcol = new DataColumn();
                dcol.DataType = System.Type.GetType("System.String");
                dcol.ColumnName = "Message";
                _dtSap.Columns.Add(dcol);

                dcol = new DataColumn();
                dcol.DataType = System.Type.GetType("System.String");
                dcol.ColumnName = "Type";
                _dtSap.Columns.Add(dcol);

                DataRow row = _dtSap.NewRow();
                row["Message"] = "CA Number not found";
                row["Type"] = string.Empty;

                ds.Merge(_dtSap.Copy());
                strOutputStatus = "CA Number : " + strCANumber + " details unable to fetch from RCM Service. (" + ex.ToString() + ")";
                dtOutputStatus.Rows.Add(strOutputStatus);
                ds.Tables.Add(dtOutputStatus);
                return ds;
            }
        }
        else
        {
            return (InvaildAuthonticationds());
        }
    }

    #endregion

    #region"MeterInfo"

    [WebMethod]

    public DataTable GET_METERDETAILS(string METERNO)
    {
        string CONS_REF = string.Empty;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable("TEST");
        DataRow row = dt.NewRow();
        dt.Columns.Add("CONS_REF", typeof(string));
        dt.Rows.Add(row);
        DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();

        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            ds = isu.Z_BAPI_CMS_ISU_CA_DISPLAY("", "0000000000" + METERNO, "", "", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt.Rows[0]["CONS_REF"] = ds.Tables[0].Rows[0]["Ca_Number"].ToString().Substring(2, 10);
            }
            else
            {
                dt.Rows.Add("Record Not Available");
                dt.Rows.RemoveAt(0);
                dt.AcceptChanges();
                return dt;
            }
        }
        return dt;
    }
    #endregion

    #region Theft Application

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetMeterCA_Details(string strKeyParam, string strMeterNo, string strCANo)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.GetMeterCA_Data(strMeterNo, strCANo);
        }
        else
        {
            return InvaildAuthontication();
        }
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable PowerThef_DivMaster(string strKeyParam, string strCircle)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.GetPT_Division_Master(strCircle);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    [WebMethod]
    public DataTable GET_METER_CA_DETAILS(string METERNO, string CA_Number)
    {
        string CONS_REF = string.Empty;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable("METER_DATA");
        DataRow row = dt.NewRow();
        string NAME = ""; string Mobile_Number = "";
        string Email = ""; string Address = "";
        string Poleno = ""; string division = "";
        string div_code = ""; string subdivision = "";
        string dtcode = ""; string meter = ""; string Seq_no = "";

        dt.Columns.Add("COMPANY");
        dt.Columns.Add("NAME");
        dt.Columns.Add("Mobile_Number");
        dt.Columns.Add("Email");
        dt.Columns.Add("Address");
        dt.Columns.Add("PoleNo");
        dt.Columns.Add("Division");
        dt.Columns.Add("Subdivision");
        dt.Columns.Add("CA_NO");
        dt.Columns.Add("Meter_No");
        dt.Columns.Add("Sequence_No");

        DataTable _dtMeter_CA = new DataTable();
        NewClassFile newClassFile = new NewClassFile();
        string _sMeterCA_Flag = "N";
        if (METERNO != "")
        {
            _dtMeter_CA = newClassFile.GetMeter_CANo_Duplicate(METERNO.Trim(), "");
            if (_dtMeter_CA.Rows.Count > 0)
                _sMeterCA_Flag = "ME";
        }
        else
        {
            _dtMeter_CA = newClassFile.GetMeter_CANo_Duplicate("", CA_Number.Trim());
            if (_dtMeter_CA.Rows.Count > 0)
                _sMeterCA_Flag = "CE";
        }

        if (_sMeterCA_Flag == "ME")
        {
            dt.Rows.Add("Observation ID already exist !!");
            dt.AcceptChanges();
            return dt;
        }
        else if (_sMeterCA_Flag == "CE")
        {
            dt.Rows.Add("Observation ID already exist !!");
            dt.AcceptChanges();
            return dt;
        }
        else
        {
            if (CA_Number.Length == 11)
                CA_Number = "0" + CA_Number;
            else if (CA_Number.Length == 10)
                CA_Number = "00" + CA_Number;
            else if (CA_Number.Length == 9)
                CA_Number = "000" + CA_Number;

            DELHIWSTESTDV2.WebService objbapi = new DELHIWSTESTDV2.WebService();
            if (checkConsumer(MethodBase.GetCurrentMethod().Name))
            {
                if (METERNO != "")
                    ds = objbapi.Z_BAPI_CMS_ISU_CA_DISPLAY("", "0000000000" + METERNO, "", "", "", "");
                else
                    ds = objbapi.Z_BAPI_CMS_ISU_CA_DISPLAY(CA_Number, "", "", "", "", "");


                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Bp_Name"].ToString() == "")
                    {
                        NAME = "";
                    }
                    else
                    {
                        NAME = ds.Tables[0].Rows[0]["Bp_Name"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["Tel1_Number"].ToString() == "")
                    {
                        Mobile_Number = "";
                    }
                    else
                    {
                        Mobile_Number = ds.Tables[0].Rows[0]["Tel1_Number"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["E_Mail"].ToString() == "")
                    {
                        Email = "";
                    }
                    else
                    {
                        Email = ds.Tables[0].Rows[0]["E_Mail"].ToString();
                    }
                    Address = ds.Tables[0].Rows[0]["Street"].ToString() + " , " + ds.Tables[0].Rows[0]["Street2"].ToString() + " , " + ds.Tables[0].Rows[0]["Street3"].ToString();
                    Poleno = ds.Tables[0].Rows[0]["POLE_NO"].ToString();
                    string Seq_no_ = ds.Tables[0].Rows[0]["Mru"].ToString();
                    if (Seq_no_ == null || Seq_no_ == "")
                    {
                        Seq_no = "";
                    }
                    else
                    {
                        Seq_no = Seq_no_.Substring(0, 5);
                    }
                    string CANO = ds.Tables[0].Rows[0]["Ca_Number"].ToString();
                    string subCA = CANO.Substring(3);
                    division = ds.Tables[0].Rows[0]["Reg_Str_Group"].ToString();
                    meter = ds.Tables[0].Rows[0]["Device_Sr_Number"].ToString();

                    CANO = CANO.TrimStart('0');
                    meter = meter.TrimStart('0');

                    dt.Rows.Add("BRPL", NAME, Mobile_Number, Email, Address, Poleno, division, subdivision,
                                                CANO, meter, Seq_no);

                    return dt;
                }
                else
                {
                    dt.Rows.Add("Enter valid CA or Meter Number.");
                    dt.AcceptChanges();
                    return dt;
                }
            }
        }

        return dt;
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string PowerThef_Insert_INPUTData(string _sOBSERVATION_TYPE, string _sDIVISION, string _sDIV_CODE, string _sMETER_NO, string _sCA_NUMBER,
                  string _sNAME, string _sADDRESS, string _sSITE_ADDRESS, string _sLANDMARK, string _sLOAD_RANGE, string _sREMARKS, string _sOTHER_RKS,
                  string _sIMEI_NO, string _sUserID, string _sDOC_ATTACHED, string _sLATITUDE, string _sLONGITUDE)
    {
        string _sInsertFlag = "0";
        Boolean _bSave = false;

        DataTable _dtCircleData = new DataTable();
        NewClassFile newClassFile = new NewClassFile();
        _dtCircleData = newClassFile.GetPT_Circle_DivWise(_sDIV_CODE);

        string _sCircle = string.Empty;

        if (_dtCircleData.Rows.Count > 0)
            _sCircle = _dtCircleData.Rows[0][0].ToString();

        _sInsertFlag = NewClassFile.Insert_PTheft_Details(_sOBSERVATION_TYPE, _sCircle, _sDIVISION, _sDIV_CODE, _sMETER_NO, _sCA_NUMBER, _sNAME, _sSITE_ADDRESS,
                                                      _sLANDMARK, _sADDRESS, _sLOAD_RANGE, _sREMARKS, _sLATITUDE, _sLONGITUDE, _sUserID, _sIMEI_NO);

        if ((_sInsertFlag != "0") && (_sDOC_ATTACHED != ""))
            _bSave = NewClassFile.Insert_PTheft_Image(_sInsertFlag, _sDOC_ATTACHED);
        else if ((_sInsertFlag != "0") && (_sDOC_ATTACHED.ToString() == ""))
            _bSave = true;

        return _sInsertFlag;
    }



    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LeadStatus_PowerTheftMIS(string strFrom, string strTodate, string strUserID)
    {
        DataTable dt = new DataTable();
        try
        {
            NewClassFile newClassFile = new NewClassFile();
            dt = newClassFile.MIS_REPORT(strFrom, strTodate, strUserID);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable Mobile_PowerTheftMIS(string Fromdate, string Todate, string Division, string Circle)
    {
        DataTable dt = new DataTable();
        try
        {
            NewClassFile newClassFile = new NewClassFile();
            dt = newClassFile.THEFT_MISREPORT(Fromdate, Todate, Division, Circle);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable Mobile_PowerTheftMIS_Summary(string Fromdate, string Todate, string Division, string Circle)
    {
        DataTable dt = new DataTable();
        try
        {
            NewClassFile newClassFile = new NewClassFile();
            dt = newClassFile.THEFT_MISREPORT_Summary_Data(Fromdate, Todate, Division, Circle);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public DataTable ENF_Redem_Reward(string Userid, string RedemType)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("LEAD_ID");
        dt.Columns.Add("OBSERVATION_ID");
        dt.Columns.Add("LEAD_DATE");
        dt.Columns.Add("OBSERVATION_TYPE");
        dt.Columns.Add("REWARD_POINTS_EARNED");
        try
        {
            NewClassFile objnewfile = new NewClassFile();
            dt = objnewfile.Redempoint(Userid, RedemType);
            // if (dt.Rows.Count > 0)            
            return dt;

            //else
            //{
            //    dt.Rows.Add("Record not found", "Record not found", "Record not found", "Record not found", "Record not found");
            //    dt.AcceptChanges();
            //    return dt;
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public Boolean ENF_Reward_Earned(string LEADID, string OBSID)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            bool _bReturn = false;
            _bReturn = NewClassFile.Earnedreward(LEADID, OBSID);
            return _bReturn;
        }
        else
        {
            return (false);
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string PowerTheft_RegisterUser(string strIMEI, string strMobileNo, string strName, string strDOB, string strDepartment, string strOtherDepart)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            return (NewClassFile.InsertUpdate_PTheft_User(strIMEI, strMobileNo, strName, strDOB, strDepartment, strOtherDepart));
        }
        else
        {
            return "0";
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable PowerTheft_LoginFE(string _sLogin, string _sPassword, string _sIMEINo)
    {
        DataTable dtLogin = new DataTable();
        dtLogin = NewClassFile.Login_PowerTheft(_sLogin, _sPassword, _sIMEINo);
        dtLogin.TableName = "Login";
        return dtLogin;
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string NewConnOTPRqstFrm_theft(string strIMEI, string strMobileNo)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //UpdateOutputCallWidFunction(strNewId, strWebId, MethodBase.GetCurrentMethod().Name);
            return (NewClassFile.newConnOTPRqstFrmInsert_theft(strMobileNo, strIMEI));
        }
        else
        {
            return ("Unauthorized Access!");
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string PowerTheft_GetPassword(string strMobileNo)
    {
        string _sPasswordOTP = string.Empty;
        string _sMessage = string.Empty;
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            _sPasswordOTP = NewClassFile.PowerTheft_GetPassword_Data(strMobileNo);
            if (_sPasswordOTP != "N")
            {
                _sMessage = "Your password to access Power Theft Mobile Application is " + _sPasswordOTP;
                NewClassFile.SendSMS_PWTFT_HelpDesk(strMobileNo, _sMessage);
                return "Y";
            }
            else
                return "N";
        }
        else
        {
            return ("Unauthorized Access!");
        }
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool NewConnResendOTPVerifyFrm_theft(string strLblId)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //UpdateOutputCallWidFunction(strNewId, strWebId, MethodBase.GetCurrentMethod().Name);
            return (NewClassFile.newConnResendOTPVerifyFrmRqst_theft(strLblId));
        }
        else
        {
            return (false);
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string NewConnOTPVerifyFrm_theft(string strOTP, string strLblId)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            //UpdateOutputCallWidFunction(strNewId, strWebId, MethodBase.GetCurrentMethod().Name);
            return (NewClassFile.newConnOTPVerifyFrmRqst_theft(strOTP, strLblId));
        }
        else
        {
            return "false";
        }
    }

    [WebMethod(EnableSession = true)]
    public DataTable loginFE_theft(string _sLogin, string _sPassword, string _sIMEINo)
    {
        //if (strKeyParam == "@$$!ntern@a|@ppRec")
        //{
        DataTable dtLogin = new DataTable();
        dtLogin = NewClassFile.clsLogin_theft(_sLogin, _sPassword, _sIMEINo);
        dtLogin.TableName = "Login";
        return dtLogin;
        //}
        //else
        //{
        //    return InvaildAuthontication();

        //}
    }

    [WebMethod(EnableSession = true)]
    public string complaintSubmit_theft(string _sDivision, string _sMeter_No, string _sAddress, string _sLandmark, string _sLoad_Range, string _sRemarks, string _sLatitude, string _sLongitude, string _sUser_ID)
    {
        //if (strKeyParam == "@$$!ntern@a|@ppRec")
        //{
        return NewClassFile.insert_theft_details(_sDivision, _sMeter_No, _sAddress, _sLandmark, _sLoad_Range, _sRemarks, _sLatitude, _sLongitude, _sUser_ID);
        //}
        //else
        //{
        //    return InvaildAuthontication();

        //}
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool PowerThef_ResetPassword(string _sPassword, string _sMobileNo, string _strOldPws)
    {
        return NewClassFile.ResetPassword_PTheft(_sMobileNo, _sPassword, _strOldPws);
    }

    #endregion

    #region "BYPL New Saturday"

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable SK_HolidayList_NEW(string strKeyParam, string strDist)
    {
        DataTable dt = new DataTable();
        NewClassFile newClassFile = new NewClassFile();

        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            dt = newClassFile.getSK_HolidayMstNew(strDist);
            dt.TableName = "SK_HolidayList";
            return dt;
        }
        else
            return (InvaildAuthontication());
    }

    #endregion

    #region"Babloo Service-11092019"
    [WebMethod]
    public string PHOTOMETER_READING(string MTR_RD_ID, string CA_NUMBER, string CONSUMER_NAME, string CONSUMER_ADD,
                                         string KWH, string KW, string KVAH, string KVA, string MTR_RD_IMG_KWH, string MTR_NUMBER,
                                         string MTR_RD_IMG_KVAH, string MTR_LAT, string MTR_LONG, string CONSUMER_CAT,
                                         string PUNCH_BY, string MTR_RD_IMG, string CA_DIVISION)
    {
        string Result = string.Empty;
        string Image1 = string.Empty;
        string Image2 = string.Empty;

        DataTable dt = new DataTable();
        try
        {
            //dt = NewClassFile.PhotoMeter(CA_NUMBER);
            //if (dt.Rows.Count <= 0)
            //{
            if (!String.IsNullOrEmpty(MTR_RD_IMG_KWH))
            {
                byte[] _byImage = Convert.FromBase64String(MTR_RD_IMG_KWH);
                Image1 = byteArrayToImage(_byImage, CA_NUMBER + ".jpg");
            }
            else
            {
                Result = "Your KWH meter reading photo not found, Please capture and try again.";
                return Result;
            }

            if (!String.IsNullOrEmpty(MTR_RD_IMG_KVAH))
            {
                byte[] _byImage1 = Convert.FromBase64String(MTR_RD_IMG_KVAH);
                Image2 = byteArrayToImage(_byImage1, CA_NUMBER + ".jpg");
            }

            bool result = NewClassFile.Insert_Photo_Meter(MTR_RD_ID, CA_NUMBER, CONSUMER_NAME, CONSUMER_ADD, KWH, KW, KVAH, KVA, Image1, MTR_NUMBER,
                                      Image2, MTR_LAT, MTR_LONG, CONSUMER_CAT, PUNCH_BY, MTR_RD_IMG, CA_DIVISION);
            if (result == true)
            {
                //Result = "Your meter reading saved successfully. Thankyou for your support -Team BSES";

                Result = "Your meter reading submitted successfully. Thank you. \nPlease Note : The downloaded  Meter Reading by a BSES meter reader will be considered as the final meter reading for the concerned bill month. Team BRPL.";

                return Result;
            }
            else
            {
                Result = "Fail to submit your meter reading details, Please try again!";
                return Result;
            }
            //}
            //else
            //{
            //    Result = "Your meter reading was already submitted for a month.";
            //    return Result;
            //}
        }
        catch
        {
            Result = "Not able to insert data please check and try again.";
            return Result;
        }
        finally
        {

        }
    }

    public static string byteArrayToImage(byte[] byteArrayIn, string filename)
    {
        string pth = string.Empty;
        string sl = string.Empty;
        Utilities.Network.NetworkDrive nd = new Utilities.Network.NetworkDrive();
        try
        {
            string username = @"BRPL\uploadimages";
            pth = @"\\10.125.126.32\photometer";
            nd.MapNetworkDrive(@"\\10.125.126.32\photometer", "Z:", username, "Bses@123");
            sl = pth;
            string dirPath = sl + "\\" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
            DirectoryInfo _DirInfo = new DirectoryInfo(dirPath);
            if (_DirInfo.Exists == false)
                _DirInfo.Create();
            string _sPath = _DirInfo.FullName + "\\" + filename;
            string _sFileNameWithoutExt = _DirInfo.FullName + "\\" + filename;
            int _iFileIndex = 1;
            while (File.Exists(_sPath))
            {
                _sPath = _sFileNameWithoutExt + "_" + _iFileIndex.ToString();
                _iFileIndex++;
            }
            if (_sPath != "X")
                File.WriteAllBytes(_sPath, byteArrayIn);
            return _sPath;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    #endregion

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable Z_ZBAPI_FETCH_ENFCA(string strBPNumber)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();

            dt = isu.ZBAPI_FETCH_ENFCA(strBPNumber).Tables[0];

            return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable BAPI_MTRREADDOC_GETLIST(string METERNO)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            dt = isu.BAPI_MTRREADDOC_GETLIST(METERNO).Tables[0];
            return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ZBAPI_CS_FETCH_LOAD(string strINORDERNO, string strINBUSINESSPART, string strINCONTRACTACCNT, string strINMETERNUM)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            dt = isu.ZBAPI_CS_FETCH_LOAD(strINORDERNO, strINBUSINESSPART, strINCONTRACTACCNT, strINMETERNUM).Tables[0];
            return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }
    //Commented dated 31032023
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ZBAPI_DISPLAY_BILL_WEB_OPOWER_NEW(string strCANumber, string strBillMonth)
    {
        string CA = string.Empty;
        CA = strCANumber;
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {

            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataColumn newcolumn = new DataColumn("OPOWER", typeof(System.String));
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (strCANumber.Length < 12 && strCANumber.Length > 0)
                strCANumber = strCANumber.PadLeft(12, '0');
            //dt = isu.Z_BAPI_DSS_ISU_CA_DISPLAY(strCANumber, "").Tables[0];
            dt = isu.ZBAPI_DISPLAY_BILL_WEB(strCANumber, strBillMonth).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataTable dtLogin = new DataTable();
                dtLogin = NewClassFile.Get_CA_Details(CA);
                if (dtLogin.Rows.Count > 0)
                {
                    newcolumn.DefaultValue = "Yes";
                }
                else
                {
                    newcolumn.DefaultValue = "No";
                }
                dt.Columns.Add(newcolumn);
            }
            return dt;
        }
        else
        {
            return (InvaildAuthontication());
        }
    }
    // Commented dated 31032023
    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable ZBAPI_FICA_DEMAND_NOTE(string OrderNo)
    {
        if (checkConsumer(MethodBase.GetCurrentMethod().Name))
        {
            DELHIWSTESTDV2.WebService isu = new DELHIWSTESTDV2.WebService();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (OrderNo.Length < 12)
                OrderNo = OrderNo.PadLeft(12, '0');
            dt = isu.ZBAPI_FICA_DEMAND_NOTE(OrderNo).Tables[0];
            return dt;
        }
        else
            return (InvaildAuthontication());
    }


    #region NewService_Added

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string LR_Validate_User_TOOTP(string strKeyParam, string mobileNumber, string EMP_ID)
    {
        string otp = "";
        string Message = string.Empty;
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            DataTable dtmobile = new DataTable();
            DbFunction objdbfun = new DbFunction();
            NewClassFile newClassFile = new NewClassFile();
            dtmobile = newClassFile.OTP_GENERATE(EMP_ID, mobileNumber);
            if (dtmobile.Rows.Count > 0)
            {
                string Mnumber = Convert.ToString(dtmobile.Rows[0]["MOBILENUMBER"]);
                if (Mnumber.Trim() != mobileNumber.Trim())
                    return "NOTMATCHED";
                else
                    return "SUCCESS";
            }
            else
                return "FAILED";
        }
        else
            return "INVAILD";
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string LR_Forgot_Password(string strKeyParam, string EMP_ID, string mobileNumber, string NewPassword, string ConfirmPassword)
    {
        var rowsAffected = 0;
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            DataTable dtmobile = new DataTable();
            DbFunction objdbfun = new DbFunction();
            NewClassFile newClassFile = new NewClassFile();
            dtmobile = newClassFile.OTP_GENERATE(EMP_ID, mobileNumber);

            if (dtmobile.Rows.Count > 0)
            {
                string Mnumber = Convert.ToString(dtmobile.Rows[0]["MOBILENUMBER"]);
                if (Mnumber.Trim() != mobileNumber.Trim())
                    return "NOTMATCHED";
                else
                {
                    if (NewPassword == ConfirmPassword)
                    {
                        NDS ndsMobinternal = new NDS();
                        using (var connection = new OleDbConnection(ndsMobinternal.con_mobinternal_new()))
                        {
                            connection.Open();
                            var query = "update MOBINT.DIV_SCHEME_VENDOR_USER_121223 set PASSWORD = :PASSWORD where EMP_ID= :EMP_ID ";
                            using (var command = new OleDbCommand(query, connection))
                            {
                                command.Parameters.AddWithValue(":PASSWORD", OracleDbType.Varchar2).Value = NewPassword;
                                command.Parameters.AddWithValue(":EMP_ID", OracleDbType.Varchar2).Value = EMP_ID;
                                rowsAffected = command.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                        return "PASSWORD AND CONFIRM-PASSWORD NOT MATCHED";
                }
            }
        }
        else
            return "INVALID";
        if (rowsAffected > 0)
            return "PASSWORD UPDATED SUCCESSFULLY";
        else
            return "PASSWORD UPDATION FAILED";
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public Boolean LR_Insert_Scheme_Vendor_MobData1_10052024(string strKeyParam, string DivId, string Scheme, string Vendor, string TeamCount, string MetRplOnly, string MetRelOnly,
                                              string BothActMet, string MetInsQul, string PoleDbRel, string ArmCblRel, string IMEI_No, string Flag_V_S, string SelectDate,
                                               string strSubDiv, string strArmCastTp, string strlatitude, string strlongitude, string empId)
    {
        string _sCircleName = string.Empty;
        DataTable _dtCircle = new DataTable();
        NewClassFile newClassFile = new NewClassFile();

        string _sSno = "0";

        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            _dtCircle = newClassFile.MobApp_Circle_DivisionWise(DivId);
            if (_dtCircle.Rows.Count > 0)
                _sCircleName = _dtCircle.Rows[0][0].ToString();

            if (CheckedDuplicateData_10052024(Flag_V_S, IMEI_No, Scheme, SelectDate) == "0")
            {
                return (newClassFile.Insert_Scheme_Vendor_MobData1_10052024(DivId, Scheme, Vendor, TeamCount, MetRplOnly, MetRelOnly, BothActMet, MetInsQul, PoleDbRel,
                                                                    ArmCblRel, Flag_V_S, IMEI_No, _sCircleName, SelectDate, strSubDiv, strlatitude, strlongitude, empId));
            }
            else
            {
                if (Flag_V_S == "V")
                    _sSno = CheckedDuplicateData_10052024(Flag_V_S, IMEI_No, Vendor, SelectDate);
                else
                    _sSno = CheckedDuplicateData_10052024(Flag_V_S, IMEI_No, Scheme, SelectDate);

                return (newClassFile.Update_Scheme_Vendor_MobData_10052024(_sSno, DivId, TeamCount, MetRplOnly, MetRelOnly, BothActMet, MetInsQul, PoleDbRel,
                                                                    ArmCblRel, IMEI_No, _sCircleName, SelectDate, empId));
            }
        }
        else
            return false;
    }

    private string CheckedDuplicateData_10052024(string _sFlag, string empId, string _sData, string _sDate)
    {
        NewClassFile newClassFile = new NewClassFile();
        DataTable _dtDup = newClassFile.MobApp_Check_SchemeVendor_DuplicatteData(_sFlag, empId, _sData, _sDate);
        if (_dtDup.Rows.Count > 0)
            return _dtDup.Rows[0][0].ToString();
        else
            return "0";
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public Boolean LR_Insert_ActivityType_MobData1_10052024(string strKeyParam, string MeterNo, string PoleNo, string ActivityList1, string ActivityList2, string IMEI_No,
                                                     string newMeter, string meterbox, string busbarspin, string cableuse, string seal1, string seal2,
                                                              string seal3, string div_code, string schemeno, string dbobs, string piercing, string frompole,
                                                               string topole, string flagType, string strlatitude, string strlongitude, string strBeforePhoto, string strAfterPhoto, string empId)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return (newClassFile.Insert_Scheme_ActivityType_MobData1_10052024(MeterNo, PoleNo, ActivityList1, ActivityList2, IMEI_No, newMeter, meterbox, busbarspin, cableuse,
                                                        seal1, seal2, seal3, div_code, schemeno, dbobs, piercing, frompole, topole, flagType, strlatitude, strlongitude, strBeforePhoto, strAfterPhoto, empId));
        }
        else
            return false;
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string LR_Surveillance_Insert_New1_09052024(string strKeyParam, string ObserType, string VistDate, string Circle, string Division, string MeterNo, string CANo,
                                                   string PoleNo, string ActionTkFlg, string Remarks, string NCType, string TypeOfAbnormality, string SiteAddress,
                                                    string AdjMeterNo1, string AdjMeterNo2, string NearPoleNo, string Other, string activity1,
                                            string activity2, string strSubDiv, string strSubCluster, string strlatitude, string strlongitude, string empId)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            if (checkConsumer(MethodBase.GetCurrentMethod().Name))
            {
                if ((Circle.ToUpper() != "-SELECT-") && (Division.ToUpper() != "-SELECT-") && (strSubDiv.ToUpper() != "-SELECT-"))
                {
                    bool _bReturn = false;
                    NewClassFile newClassFile = new NewClassFile();
                    string _sOutPutData = string.Empty;
                    string _sMetreCANO = string.Empty;
                    if (MeterNo.Trim() == "")
                    {
                        if (CANo.Trim() != "")
                        {
                            _sMetreCANO = CANo;
                        }
                        else if (PoleNo.Trim() != "")
                        {
                            _sMetreCANO = PoleNo;
                        }
                    }
                    else
                    {
                        _sMetreCANO = MeterNo;
                    }

                    _sOutPutData = newClassFile.Check_Surveillance_Duplicate(_sMetreCANO);

                    if (_sOutPutData.ToString().Trim() != "0")
                    {
                        return "Record already submitted on Date:- " + _sOutPutData;
                    }
                    else
                    {
                        _bReturn = newClassFile.Insert_LR_Surveillane1_09052024(ObserType.ToString(), VistDate.ToString(), Circle.ToString(), Division.ToString(), MeterNo.ToString(), CANo.ToString(),
                                                        PoleNo.ToString(), ActionTkFlg.ToString(), Remarks.ToString(), NCType.ToString(), TypeOfAbnormality.ToString(),
                                                        SiteAddress.ToString(), AdjMeterNo1.ToString(), AdjMeterNo2.ToString(), NearPoleNo.ToString(), Other.ToString(),
                                                        activity2.ToString(), strSubDiv, strSubCluster, strlatitude, strlongitude, empId);

                        return "Successfully Submitted";
                    }
                }
                else
                {
                    return "Not Submit Sucessfully, Please select valid Circle/Division/Subdivision ";
                }
            }
            else
                return "Not Submit Sucessfully, Please Try Again";
        }
        else
            return "Not Submit Sucessfully, Please Try Again";
    }



    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public bool LR_Surv_ATR_Insert1_09052024(string strKeyParam, string ObserId, string ObserType, string Remarks, string NCTypeResolved, string NCTypeNotResolved,
                                    string TypOfAbnormResolv, string TypOfAbnormNtResolv, string activity1, string activity2, string enf_Case_ID,
                                    string strlatitude, string strlongitude, string leadRepeated, string empId)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            if (checkConsumer(MethodBase.GetCurrentMethod().Name))
            {
                bool _bReturn = false;

                string[] str = activity1.Split(',');

                NewClassFile newClassFile = new NewClassFile();

                for (int i = 0; i < str.Length; i++)
                {
                    _bReturn = newClassFile.Insert_LR_Surv_Atr1_09052024(ObserId, ObserType.ToString(), Remarks, NCTypeResolved, NCTypeNotResolved, TypOfAbnormResolv,
                                                            TypOfAbnormNtResolv, str[i], activity2, enf_Case_ID, strlatitude, strlongitude, leadRepeated, empId);
                }

                return _bReturn;
            }
            else
                return (false);
        }
        else
            return false;
    }







    #endregion

    #region "New_Image_28052024"

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public Boolean LR_Insert_ActivityType_MobData1_20052024(string strKeyParam, string MeterNo, string PoleNo, string ActivityList1, string ActivityList2, string IMEI_No,
                                                     string newMeter, string meterbox, string busbarspin, string cableuse, string seal1, string seal2,
                                                              string seal3, string div_code, string schemeno, string dbobs, string piercing, string frompole,
                                                               string topole, string flagType, string strlatitude, string strlongitude, string strBeforePhoto, string strAfterPhoto, string empId, string busbarseal1, string busbarseal2, string strMcrPhoto)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return (newClassFile.Insert_Scheme_ActivityType_MobData1_20052024(MeterNo, PoleNo, ActivityList1, ActivityList2, IMEI_No, newMeter, meterbox, busbarspin, cableuse, seal1, seal2,
                                                              seal3, div_code, schemeno, dbobs, piercing, frompole, topole, flagType, strlatitude, strlongitude, strBeforePhoto, strAfterPhoto, empId, busbarseal1, busbarseal2, strMcrPhoto));
        }
        else
            return false;
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_ObservationList_New_27052024(string strKeyParam, string strDivName, string strRolRght, string strSubDiv, string strSubCluster)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            string[] str = strRolRght.Split(',');
            DataTable _dtOutput = new DataTable();
            DataTable _dtFinData = new DataTable();

            NewClassFile newClassFile = new NewClassFile();

            for (int i = 0; i < str.Length; i++)
            {
                _dtOutput = newClassFile.Mobint_LRObservationList_27052024(strSubDiv, strDivName, str[i], strSubCluster);
                _dtFinData.Merge(_dtOutput);
            }

            _dtFinData.AcceptChanges();
            _dtFinData.TableName = "LR_OBSERVATION";
            return _dtFinData;

        }
        else
        {
            return InvaildAuthontication();
        }
    }


    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_ObservationList_New_29082024(string strKeyParam, string strDivName, string strRolRght, string strSubDiv, string strSubCluster, string frmDate, string toDate)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            string[] str = strRolRght.Split(',');
            DataTable _dtOutput = new DataTable();
            DataTable _dtFinData = new DataTable();

            NewClassFile newClassFile = new NewClassFile();

            for (int i = 0; i < str.Length; i++)
            {
                _dtOutput = newClassFile.Mobint_LRObservationList_29082024(strSubDiv, strDivName, str[i], strSubCluster, frmDate, toDate);
                _dtFinData.Merge(_dtOutput);
            }

            _dtFinData.AcceptChanges();
            _dtFinData.TableName = "LR_OBSERVATION";
            return _dtFinData;

        }
        else
        {
            return InvaildAuthontication();
        }
    }




    #region New_Function_Replica_05062024

    [WebMethod]
    [SoapHeader("consumer", Required = true)]
    public DataTable LR_Scheme_AllDivSchmMapping_05062024(string strKeyParam, string circle, string division, string subdivision, string scheme)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            NewClassFile newClassFile = new NewClassFile();
            return newClassFile.MobApp_Scheme_ALL_05062024(circle, division, subdivision, scheme);
        }
        else
        {
            return InvaildAuthontication();
        }
    }

    [WebMethod(EnableSession = true)]
    [SoapHeader("consumer", Required = true)]
    public string LR_Surv_QC_Insert_New_07082024(string strKeyParam, string VistDate, string Circle, string Division,
                                                    string MeterNo, string CANo,
                                                   string Remarks, string QCType,
                                                   string QCSlctd,
                                                    string Other, string activity1, string activity2, string strSubDiv, string empId)
    {
        if (strKeyParam == "@$$!ntern@a|@pp")
        {
            if (checkConsumer(MethodBase.GetCurrentMethod().Name))
            {
                if ((Circle.ToUpper() != "-SELECT-") && (Division.ToUpper() != "-SELECT-") && (strSubDiv.ToUpper() != "-SELECT-"))
                {
                    NewClassFile newClassFile = new NewClassFile();

                    string _sOutPutData = string.Empty;

                    string _sMetreCANO = string.Empty;
                    if (MeterNo.Trim() == "")
                    {
                        if (CANo.Trim() != "")
                        {
                            _sMetreCANO = CANo;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(activity2))
                            {
                                if (activity2.ToString().Contains("|"))
                                {
                                    string[] _arrnctyp = activity2.Trim().Split('|');
                                    if (!string.IsNullOrEmpty(_arrnctyp[1].ToString()))
                                    {
                                        _sMetreCANO = _arrnctyp[1].ToString();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        _sMetreCANO = MeterNo;
                    }

                    _sOutPutData = newClassFile.Check_QC_Duplicate(_sMetreCANO.ToString());

                    if (_sOutPutData.ToString().Trim() != "0")
                    {
                        return "Record already submitted on Date:- " + _sOutPutData;
                    }
                    else
                    {
                        string strImeiNo = "";
                        string strPoleNo = "";
                        if (!string.IsNullOrEmpty(activity2))
                        {
                            if (activity2.ToString().Contains("|"))
                            {
                                string[] _arrnctyp = activity2.Trim().Split('|');
                                strImeiNo = _arrnctyp[0].ToString();
                                if (!string.IsNullOrEmpty(_arrnctyp[1].ToString()))
                                {
                                    strPoleNo = _arrnctyp[1].ToString();
                                }
                            }
                            else
                            {
                                strImeiNo = activity2.ToString();
                            }
                        }

                        bool _bReturn = false;

                        _bReturn = newClassFile.Insert_LR_QCheck_08082024(VistDate.ToString(), Circle.ToString(),
                                                        Division.ToString(), MeterNo.ToString(), CANo.ToString(),
                                                        Remarks.ToString(),
                                                        QCType.ToString(), QCSlctd.ToString(), Other.ToString(), activity1, strImeiNo, strPoleNo, strSubDiv, empId);

                        return "Successfully Submitted";

                    }
                }
                else
                {
                    return "Not Submit Sucessfully, Please select valid Circle/Division/Subdivision ";
                }
            }
            else
                return "Not Submit Sucessfully, Please Try Again";
        }
        else
            return "Not Submit Sucessfully, Please Try Again";
    }



    #endregion


    #endregion

}

[Serializable]
public class UserCredentials : System.Web.Services.Protocols.SoapHeader
{
    public string userName;
    public string password;

}