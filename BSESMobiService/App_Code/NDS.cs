using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Text;
using System.Configuration;

public class NDS
{
    public NDS()
    {
    }

    public static bool IsNumeric(object value)
    {
        try
        {
            Double i = Convert.ToDouble(value.ToString());
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static void ClearControls(Control control)
    {
        for (int i = control.Controls.Count - 1; i >= 0; i--)
        {
            ClearControls(control.Controls[i]);
        }

        if (!(control is TableCell))
        {
            if (control.GetType().GetProperty("SelectedItem") != null)
            {
                LiteralControl literal = new LiteralControl();
                control.Parent.Controls.Add(literal);
                try
                {
                    literal.Text = (string)control.GetType().GetProperty("SelectedItem").GetValue(control, null);
                }
                catch
                {
                }
                control.Parent.Controls.Remove(control);
            }
            else
                if (control.GetType().GetProperty("Text") != null)
            {
                LiteralControl literal = new LiteralControl();
                control.Parent.Controls.Add(literal);
                literal.Text = (string)control.GetType().GetProperty("Text").GetValue(control, null);
                control.Parent.Controls.Remove(control);
            }
        }
        return;
    }

    #region connection

    public string con()
    {
        string str = string.Empty;
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";

            Cryptograph crp = new Cryptograph();
            HttpServerUtility myServer = HttpContext.Current.Server;

            string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM.INI";

            // ASSING KEY TO CONNECT DATABASE.
            string PW_KEY = "o8??^am(*)"; // Enter Key Below Function Also...

            user_id = crp.Decrypt(NDSINI.GetINI(vs, "itpms", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            pass = crp.Decrypt(NDSINI.GetINI(vs, "itpms", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            database = crp.Decrypt(NDSINI.GetINI(vs, "itpms", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

            str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        }
        else
        {
            str = "Provider=OraOLEDB.Oracle; User ID=mobapp; Password=mobapp; Data Source=EBSTESTOLD;";
        }
        return str;
    }

    public string con_PCM()
    {
        string str = string.Empty;
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";

            Cryptograph crp = new Cryptograph();
            HttpServerUtility myServer = HttpContext.Current.Server;

            string vs = AppDomain.CurrentDomain.BaseDirectory + "PCM.INI";

            // ASSING KEY TO CONNECT DATABASE.
            string PW_KEY = "o8??^am(*)"; // Enter Key Below Function Also...

            user_id = crp.Decrypt(NDSINI.GetINI(vs, "PCM_ORA", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            pass = crp.Decrypt(NDSINI.GetINI(vs, "PCM_ORA", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            database = crp.Decrypt(NDSINI.GetINI(vs, "PCM_ORA", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

            str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        }
        else
        {
            str = "Provider=OraOLEDB.Oracle; User ID=pcm; Password=pcm; Data Source=EBSTESTOLD;";
        }
        return str;
    }

    public string con_PCM1()
    {
        string str = string.Empty;
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";

            Cryptograph crp = new Cryptograph();
            HttpServerUtility myServer = HttpContext.Current.Server;

            string vs = AppDomain.CurrentDomain.BaseDirectory + "PCM.INI";

            //ASSING KEY TO CONNECT DATABASE.
            string PW_KEY = "o8??^am(*)"; // Enter Key Below Function Also...

            user_id = crp.Decrypt(NDSINI.GetINI(vs, "PCM_ORA", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            pass = crp.Decrypt(NDSINI.GetINI(vs, "PCM_ORA", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            database = crp.Decrypt(NDSINI.GetINI(vs, "PCM_ORA", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

            str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        }
        else
        {
            str = "Provider=OraOLEDB.Oracle; User ID=pcm; Password=pcm; Data Source=EBSTESTOLD;";
        }
        return str;
    }

    public string con_dcrep()
    {
        string str = string.Empty;
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";

            Cryptograph crp = new Cryptograph();
            HttpServerUtility myServer = HttpContext.Current.Server;

            //string vs = myServer.MapPath("IT-OPR.ini");
            string vs = AppDomain.CurrentDomain.BaseDirectory + "bses.ini";

            // ASSING KEY TO CONNECT DATABASE.
            string PW_KEY = "o8??^am(*)";  // Enter Key Below Function Also...   

            user_id = crp.Decrypt(NDSINI.GetINI(vs, "DCREP", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            pass = crp.Decrypt(NDSINI.GetINI(vs, "DCREP", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            database = crp.Decrypt(NDSINI.GetINI(vs, "DCREP", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here
            str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        }
        else
        {
            str = "Provider=OraOLEDB.Oracle; User ID=dcrep; Password=ccm; Data Source=EBSTESTOLD;";
        }
        return str;
    }

    public string con_ONM()
    {
        string str = string.Empty;
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";

            Cryptograph crp = new Cryptograph();
            HttpServerUtility myServer = HttpContext.Current.Server;

            string vs = AppDomain.CurrentDomain.BaseDirectory + "OMS.ini";

            string PW_KEY = "o8??^am(*)";  // Enter Key Below Function Also...

            database = crp.Decrypt(NDSINI.GetINI(vs, "OMS DBConfig", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            user_id = crp.Decrypt(NDSINI.GetINI(vs, "OMS DBConfig", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            pass = crp.Decrypt(NDSINI.GetINI(vs, "OMS DBConfig", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

            str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        }
        else
        {
            str = "";
        }
        return str;

    }

    public string con_CCM()
    {
        string str = string.Empty;
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";

            Cryptograph crp = new Cryptograph();
            HttpServerUtility myServer = HttpContext.Current.Server;

            //string vs = myServer.MapPath("IT-OPR.ini");
            string vs = AppDomain.CurrentDomain.BaseDirectory + "bses.ini";

            // ASSING KEY TO CONNECT DATABASE.

            string PW_KEY = "@!*fdfsfd+}|@";  // Enter Key Below Function Also...            

            user_id = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            pass = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            database = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

            str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        }
        else
        {
            str = "Provider=OraOLEDB.Oracle; User ID=ccm; Password=ccm; Data Source=EBSTESTOLD;";
        }
        return str;
    }

    public string con_VSS()
    {
        string str = string.Empty;
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";

            Cryptograph crp = new Cryptograph();
            HttpServerUtility myServer = HttpContext.Current.Server;

            //string vs = myServer.MapPath("IT-OPR.ini");
            string vs = AppDomain.CurrentDomain.BaseDirectory + "bses.ini";

            // ASSING KEY TO CONNECT DATABASE.

            string PW_KEY = "@!*fdfsfd+}|@";  // Enter Key Below Function Also...            

            user_id = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            pass = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            database = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

            str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        }
        else
        {
            str = "Provider=OraOLEDB.Oracle; User ID=vss; Password=vss; Data Source=EBSTESTOLD;";
        }
        return str;
    }

    public string con_mobinternal()
    {
        string str = string.Empty;
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";

            Cryptograph crp = new Cryptograph();
            HttpServerUtility myServer = HttpContext.Current.Server;

            string vs = AppDomain.CurrentDomain.BaseDirectory + "bsesmobint.INI";

            // ASSING KEY TO CONNECT DATABASE.
            string PW_KEY = "o8??^am(*)"; // Enter Key Below Function Also...

            user_id = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            pass = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            database = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

            str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        }
        else
        {
            str = "Provider=OraOLEDB.Oracle; User ID=mobint; Password=mobint; Data Source=EBSTESTOLD;";
        }
        return str;
    }

    public string con_mobinternal_new()
    {
        string str = string.Empty;
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";

            Cryptograph crp = new Cryptograph();
            HttpServerUtility myServer = HttpContext.Current.Server;

            string vs = AppDomain.CurrentDomain.BaseDirectory + "bsesmobint.INI";

            // ASSING KEY TO CONNECT DATABASE.
            string PW_KEY = "o8??^am(*)"; // Enter Key Below Function Also...

            user_id = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            pass = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            database = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

            str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        }
        else
        {
            str = "Provider=OraOLEDB.Oracle; User ID=mobint; Password=mobint; Data Source=EBSTESTOLD;";
        }
        return str;
    }

    public string con_RCM()
    {
        string str = string.Empty;
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";

            Cryptograph crp = new Cryptograph();
            HttpServerUtility myServer = HttpContext.Current.Server;

            //string vs = myServer.MapPath("IT-OPR.ini");
            string vs = string.Empty;
            string PW_KEY = string.Empty;

            str = "Provider=OraOLEDB.Oracle; User ID=rcmpa; Password=fer5gst3db; Data Source=ebsdb;";
        }
        else
        {
            str = "Provider=OraOLEDB.Oracle; User ID=rcmpa; Password=rcmpa; Data Source=EBSTESTOLD;";
        }
        return str;
    }

    public string con_Recovery()
    {
        string str = string.Empty;
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";

            Cryptograph crp = new Cryptograph();
            HttpServerUtility myServer = HttpContext.Current.Server;

            ////For live
            string vs = AppDomain.CurrentDomain.BaseDirectory + "bses.ini";

            string PW_KEY = "o8??^am(*)";  // Enter Key Below Function Also... 
            user_id = crp.Decrypt(NDSINI.GetINI(vs, "RECOVERY_LIVE", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            pass = crp.Decrypt(NDSINI.GetINI(vs, "RECOVERY_LIVE", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            database = crp.Decrypt(NDSINI.GetINI(vs, "RECOVERY_LIVE", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here
            str = " Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        }
        else
        {
            str = "Provider=OraOLEDB.Oracle; User ID=recovery; Password=recovery; Data Source=EBSTESTOLD; ";//recovery test
        }
        return str;
    }

    public string con_Mobapp()
    {
        string str = string.Empty;
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (environment == "LIVE")
        {
            string database = "";
            string user_id = "";
            string pass = "";

            Cryptograph crp = new Cryptograph();
            HttpServerUtility myServer = HttpContext.Current.Server;

            string vs = AppDomain.CurrentDomain.BaseDirectory + "bses.ini";

            // ASSING KEY TO CONNECT DATABASE.

            string PW_KEY = "@!*fdfsfd+}|@";  // Enter Key Below Function Also...            

            user_id = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            pass = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            database = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

            str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        }
        else
        {
            str = "Provider=OraOLEDB.Oracle; user id=mobapp; password=mobapp; data source=EBSTESTOLD;";
        }
        return str;
    }

    #region TEST_API_CONN_DIVYESH

    public string con_mobinternal_TEST()
    {
        string str = "Provider=OraOLEDB.Oracle; User ID=mobint; Password=mobint; Data Source=EBSTESTOLD;";
        return str;
    }

    public string con_Mobapp_TEST()
    {
        string str = "Provider=OraOLEDB.Oracle; user id=mobapp; password=mobapp; data source=EBSTESTOLD;";
        return str;
    }


    #endregion




    #endregion

    public static string GetDAMcon()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["EBSDBSTD"].ToString();
    }

    public static string GetOMSCon()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["OMSCON"].ToString();
    }

    public static string GetSSMSCon()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["SSMS"].ToString();
    }

    public static string GetGISDbcon()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["YPLGISDB"].ToString();
    }

    public static string GetMIDAcon()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["MIDA"].ToString();
    }
}




