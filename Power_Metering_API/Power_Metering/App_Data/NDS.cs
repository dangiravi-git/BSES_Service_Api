using System;
using System.Configuration;
public class NDS
{
    public NDS()
    {

    }

    public static string conSqlServer()
    {
        //return  System.Configuration.ConfigurationSettings.AppSettings["conSql"].ToString();
        var connectionString = ConfigurationManager.ConnectionStrings["conSql"].ConnectionString;
        return connectionString;
    }
    public static string con()
    {
        string str = "";
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";
            try
            {
                Cryptograph crp = new Cryptograph();
                //string vs = myServer.MapPath("IT-OPR.ini");
                string vs = AppDomain.CurrentDomain.BaseDirectory + "bsesmobint.ini"; //LIVE
                                                                                      //ASSING KEY TO CONNECT DATABASE.
                string PW_KEY = "o8??^am(*)";  // Insert Key Here
                user_id = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
                pass = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
                database = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here
                str = "Provider=OraOLEDB.Oracle.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
                //str = "Provider=OraOLEDB.Oracle.1; User ID=mobint; Password=mobint; Data Source=EBSTESTOLD;";   ///for test  
                //str = "Provider=MSDAORA.1; User ID=piyush; Password=piyush; Data Source=ebsdbstd;";
                //str = "Provider=MSDAORA.1; User ID=mobint; Password=mobintweb; Data Source=ebsdb;";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        else {
            str = "Provider=OraOLEDB.Oracle; User ID=mobint; Password=mobint; Data Source=EBSTESTOLD;";//for test connection string
        }
       
        return str;
    }

    public static string conMobApp()
    {
        string str = "";
        string database = "";
        string user_id = "";
        string pass = "";
        try
        {
            Cryptograph crp = new Cryptograph();
            //string vs = myServer.MapPath("IT-OPR.ini");
            string vs = AppDomain.CurrentDomain.BaseDirectory + "bsesmobint.ini"; //LIVE
            //ASSING KEY TO CONNECT DATABASE.
            string PW_KEY = "o8??^am(*)";  // Insert Key Here
            user_id = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            pass = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            database = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here
            str = "Provider=OraOLEDB.Oracle.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
            str = "Provider=MSDAORA.1; User ID=mobapp; Password=mobileservice; Data Source=ebsdb;";   ///for test  
            //str = "Provider=MSDAORA.1; User ID=piyush; Password=piyush; Data Source=ebsdbstd;";
            //str = "Provider=MSDAORA.1; User ID=mobint; Password=mobintweb; Data Source=ebsdb;";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        return str;
    }
}