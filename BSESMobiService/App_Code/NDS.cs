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

        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        //string vs = myServer.MapPath("IT-OPR.ini");
        //string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM_Live.INI";
        string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM.INI";

        // ASSING KEY TO CONNECT DATABASE.
        string PW_KEY = "o8??^am(*)"; // Enter Key Below Function Also...

        user_id = crp.Decrypt(NDSINI.GetINI(vs, "itpms", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "itpms", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        database = crp.Decrypt(NDSINI.GetINI(vs, "itpms", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        string str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";

        return str;
    }

    public string con_PCM()
    {

        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        //string vs = myServer.MapPath("IT-OPR.ini");
        //string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM_Live.INI";
        string vs = AppDomain.CurrentDomain.BaseDirectory + "PCM.INI";

        // ASSING KEY TO CONNECT DATABASE.
        string PW_KEY = "o8??^am(*)"; // Enter Key Below Function Also...

        user_id = crp.Decrypt(NDSINI.GetINI(vs, "PCM_ORA", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "PCM_ORA", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        database = crp.Decrypt(NDSINI.GetINI(vs, "PCM_ORA", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        string str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";

        return str;
    }

    public string con_PCM1()
    {

        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

       // string vs = myServer.MapPath("IT-OPR.ini");
        //string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM_Live.INI";
        string vs = AppDomain.CurrentDomain.BaseDirectory + "PCM.INI";

         //ASSING KEY TO CONNECT DATABASE.
        string PW_KEY = "o8??^am(*)"; // Enter Key Below Function Also...

        user_id = crp.Decrypt(NDSINI.GetINI(vs, "PCM_ORA", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "PCM_ORA", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        database = crp.Decrypt(NDSINI.GetINI(vs, "PCM_ORA", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        string str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        //string str = " User ID=rcmpa; Password=fer5gst3db; Data Source=ebsdb;";
        //string str = "Provider=MSDAORA.1; User ID=PIYUSH; Password=piyush; Data Source=ebsdbstd;";
        return str;
    }

    public string con_dcrep()
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
        string str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";

        return str;
    }

    public string con_ONM()
    {
        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        // ASSING KEY TO CONNECT DATABASE.
        //For Live
        string vs = AppDomain.CurrentDomain.BaseDirectory + "OMS.ini";

        string PW_KEY = "o8??^am(*)";  // Enter Key Below Function Also...

        database = crp.Decrypt(NDSINI.GetINI(vs, "OMS DBConfig", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        user_id = crp.Decrypt(NDSINI.GetINI(vs, "OMS DBConfig", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "OMS DBConfig", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        ////	For TEst
        //string vs = AppDomain.CurrentDomain.BaseDirectory + "onm_test.ini";
        //string PW_KEY = "key";

        //user_id = crp.Decrypt(NDSINI.GetINI(vs, "bses", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        //pass = crp.Decrypt(NDSINI.GetINI(vs, "bses", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        //database = crp.Decrypt(NDSINI.GetINI(vs, "bses", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        string str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        //str = "Provider=MSDAORA.1; User ID=onm; Password=onm; Data Source=ebstest;";
        return str;

    }

    public string con_CCM()
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

        //user_id = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        //pass = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        //database = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        //string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        string str = "Provider=MSDAORA.1; User ID=CCM; Password=CCM; Data Source=EBSTEST;";
        str = "Provider=OraOLEDB.Oracle;  User ID=ccm; Password=stg3zkl; Data Source=ebsdev;";


        return str;
    }

    public string con_VSS()
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

        //user_id = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        //pass = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        //database = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        //string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
       // string str = "Provider=MSDAORA.1; User ID=VSS; Password=VSS; Data Source=EBSTEST;";
        string str = "Provider=OraOLEDB.Oracle; User ID =VSS; Password =VSSEBSDB; Data Source =EBSDB";


        return str;
    }

    public string con_mobinternal()
    {

        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        //string vs = myServer.MapPath("IT-OPR.ini");
        //string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM_Live.INI";
        string vs = AppDomain.CurrentDomain.BaseDirectory + "bsesmobint.INI";

        // ASSING KEY TO CONNECT DATABASE.
        string PW_KEY = "o8??^am(*)"; // Enter Key Below Function Also...

        user_id = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        database = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        string str = "Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";

        return str;
    }

    public string con_mobinternal_new()
    {

        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        //string vs = myServer.MapPath("IT-OPR.ini");
        //string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM_Live.INI";
        string vs = AppDomain.CurrentDomain.BaseDirectory + "bsesmobint.INI";

        // ASSING KEY TO CONNECT DATABASE.
        string PW_KEY = "o8??^am(*)"; // Enter Key Below Function Also...

        user_id = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        database = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        string str = "Provider=OraOLEDB.Oracle; Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";

        return str;
    }

    public string con_RCM()
    {
        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        //string vs = myServer.MapPath("IT-OPR.ini");
        string vs = string.Empty;
        string PW_KEY = string.Empty;

        //string _sMYSQL_ORACLE = System.Configuration.ConfigurationManager.AppSettings["TEST_LIVE"].ToString();

        //if (_sMYSQL_ORACLE == "TEST")
        //{
        //    vs = AppDomain.CurrentDomain.BaseDirectory + "bses_test.ini";
        //    PW_KEY = "@!*fdfsfd+}|@";
        //}
        //else
        //{
        //    vs = AppDomain.CurrentDomain.BaseDirectory + "bses.ini"; //LIVE
        //    PW_KEY = "o8??^am(*)";
        //}

        //user_id = crp.Decrypt(NDSINI.GetINI(vs, "SAPRCM", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        //pass = crp.Decrypt(NDSINI.GetINI(vs, "SAPRCM", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        //database = crp.Decrypt(NDSINI.GetINI(vs, "SAPRCM", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here
        //string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";

        ////  string str = "Provider=MSDAORA.1; User ID=DCSLT; Password=DCSLT; Data Source=ebsdb;";
        //  string str = "Provider=OraOLEDB.Oracle.1; User ID=system; Password=reliance#1234; Data Source=orcl;"; // for the live bihar invironment

        string str = "Provider=OraOLEDB.Oracle; User ID=rcmpa; Password=fer5gst3db; Data Source=ebsdb;";

        return str;
    }

    public string con_Recovery()
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

        //////// For Test
        //string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM.ini";
        //string PW_KEY = "@!*fdfsfd+}|@";
        //user_id = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        //pass = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        //database = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        string str = " Provider=OraOLEDB.Oracle; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        //string str = "Provider=MSDAORA.1; User ID=recovery; Password=sy$temrec0very; Data Source=ebsdb; ";//recovery test        

        return str;
    }

    public string con_Mobapp()
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

        //user_id = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        //pass = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        //database = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        //string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        // string str = "Provider=MSDAORA.1; User ID=VSS; Password=VSS; Data Source=EBSTEST;";
        string str = "Provider=OraOLEDB.Oracle; user id=mobapp; password=mobileservice; data source=ebsdb;";


        return str;
    }

    #region TEST_API_CONN_DIVYESH

    public string con_mobinternal_TEST()
    {

        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        //string vs = myServer.MapPath("IT-OPR.ini");
        //string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM_Live.INI";
        string vs = AppDomain.CurrentDomain.BaseDirectory + "bsesmobint.INI";

        // ASSING KEY TO CONNECT DATABASE.
        string PW_KEY = "o8??^am(*)"; // Enter Key Below Function Also...

        user_id = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        database = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        string str = "Provider=OraOLEDB.Oracle; User ID=mobint; Password=mobint; Data Source=EBSTESTOLD;";

        return str;
    }

    public string con_Mobapp_TEST()
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

        //user_id = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        //pass = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        //database = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        //string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        // string str = "Provider=MSDAORA.1; User ID=VSS; Password=VSS; Data Source=EBSTEST;";
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




