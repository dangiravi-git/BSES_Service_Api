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
    public static string str = "";


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
    public string conPCM_New()
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

        user_id = crp.Decrypt(NDSINI.GetINI(vs, "PCM", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "PCM", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        database = crp.Decrypt(NDSINI.GetINI(vs, "PCM", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        //string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        // str = "Provider=MSDAORA.1; User ID=CCM; Password=ccm; Data Source=EBSTESTOLD;";
        // str = "Provider=MSDAORA.1; User ID=ccm; Password=stg3zkl; Data Source=ebsdev;";
        //str = "Provider=MSDAORA.1; User ID=ccm; Password=stg3zkl; Data Source=ebsdbstd;";
        str = "Provider=MSDAORA.1; User ID=pcm; Password=pcm123prod; Data Source=ebsdb;";

        return str;
    }
    public string con()
    {
        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        //string vs = myServer.MapPath("IT-OPR.ini");
        string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM.ini";

        // ASSING KEY TO CONNECT DATABASE.

        string PW_KEY = "o8??^am(*)";  // Enter Key Below Function Also...            

        user_id = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        database = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        //string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        string str = "Provider=OraOLEDB.Oracle.1; user id=" + user_id + "; password=" + pass + "; data source=" + database + ";";

        //  str = "Provider=MSDAORA.1; User ID=CCM; Password=ccm; Data Source=EBSTESTOLD;";

        //str = "Provider=MSDAORA.1; User ID=mobapp; Password=mobapp; Data Source=EBSTESTOLD;";
        // string str = "User ID=piyush; Password=piyush; Data Source=ebsdbstd;";

        //  str = "Provider=MSDAORA.1; User ID=piyush; Password=piyush; Data Source=ebsdbstd;";

        return str;
    }
    public string con_IOMSDB()
    {
        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        //string vs = myServer.MapPath("IT-OPR.ini");
        string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM.ini";

        // ASSING KEY TO CONNECT DATABASE.

        string PW_KEY = "o8??^am(*)";  // Enter Key Below Function Also...            

        user_id = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        database = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        //string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
         //string str = "Provider=OraOLEDB.Oracle.1; user id=" + user_id + "; password=" + pass + "; data source=" + database + ";";

         str = "Provider=OraOLEDB.Oracle.1; User ID=BRPLCRM; Password=brplcrm; Data Source=IOMSDB;";

        //str = "Provider=MSDAORA.1; User ID=mobapp; Password=mobapp; Data Source=EBSTESTOLD;";
        // string str = "User ID=piyush; Password=piyush; Data Source=ebsdbstd;";

        //  str = "Provider=MSDAORA.1; User ID=piyush; Password=piyush; Data Source=ebsdbstd;";

        return str;
    }
    public static string conWeb()
    {
        string database = "";
        string user_id = "";
        string pass = "";

        //Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        //string vs = myServer.MapPath("IT-OPR.ini");
        string vs = AppDomain.CurrentDomain.BaseDirectory + "bses.ini";

        // ASSING KEY TO CONNECT DATABASE.

        string PW_KEY = "@!*fdfsfd+}|@";  // Enter Key Below Function Also...            

        //user_id = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        //pass = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        //database = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        //string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        //str = "Provider=MSDAORA.1; User ID=CCM; Password=ccm; Data Source=EBSTESTOLD;";
        // str = "Provider=MSDAORA.1; User ID=ccm; Password=stg3zkl; Data Source=ebsdev;";
        //str = "Provider=MSDAORA.1; User ID=ccm; Password=stg3zkl; Data Source=ebsdbstd;";
        // str = "Provider=MSDAORA.1; User ID=piyush; Password=piyush; Data Source=ebsdbstd;";

        return str;
    }

    public string conPCM()
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

        user_id = crp.Decrypt(NDSINI.GetINI(vs, "PCM", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "PCM", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        database = crp.Decrypt(NDSINI.GetINI(vs, "PCM", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
       // str = "Provider=MSDAORA.1; User ID=CCM; Password=ccm; Data Source=EBSTESTOLD;";
        // str = "Provider=MSDAORA.1; User ID=ccm; Password=stg3zkl; Data Source=ebsdev;";
        //str = "Provider=MSDAORA.1; User ID=ccm; Password=stg3zkl; Data Source=ebsdbstd;";
        //str = "Provider=MSDAORA.1; User ID=pcm; Password=pcm123prod; Data Source=ebsdb;";

        return str;
    }

    public string Mobcon()
    {
        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        //string vs = myServer.MapPath("IT-OPR.ini");
        string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM.ini";

        // ASSING KEY TO CONNECT DATABASE.

        //string PW_KEY = "o8??^am(*)";  // Enter Key Below Function Also...            

        //user_id = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        //pass = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        //database = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        //string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        //  str = "Provider=MSDAORA.1; User ID=CCM; Password=ccm; Data Source=EBSTESTOLD;";

        string str = "Provider=MSDAORA.1; User ID=mobapp; Password=mobileservice; Data Source=ebsdb;";

        // string str = "User ID=piyush; Password=piyush; Data Source=ebsdbstd;";

        //  str = "Provider=MSDAORA.1; User ID=piyush; Password=piyush; Data Source=ebsdbstd;";

        return str;
    }

    public string STDcon()
    {
        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        //string vs = myServer.MapPath("IT-OPR.ini");
        string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM.ini";

        // ASSING KEY TO CONNECT DATABASE.

        //string PW_KEY = "o8??^am(*)";  // Enter Key Below Function Also...            

        //user_id = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        //pass = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        //database = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        //string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        //  str = "Provider=MSDAORA.1; User ID=CCM; Password=ccm; Data Source=EBSTESTOLD;";

        //string str = "Provider=MSDAORA.1; User ID=mobapp; Password=mobileservice; Data Source=ebsdb;";
        string str = "Provider=OraOLEDB.Oracle.1; User ID=PIYUSH; Password=piyush; Data Source=ebsdbdr2;";


        // string str = "User ID=piyush; Password=piyush; Data Source=ebsdbstd;";

        //  str = "Provider=MSDAORA.1; User ID=piyush; Password=piyush; Data Source=ebsdbstd;";

        return str;
    }

    public string Mobintcon()
    {
        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        HttpServerUtility myServer = HttpContext.Current.Server;

        //string vs = myServer.MapPath("IT-OPR.ini");
        string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM.ini";

        // ASSING KEY TO CONNECT DATABASE.

        string PW_KEY = "o8??^am(*)";  // Enter Key Below Function Also...            

        user_id = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
        database = crp.Decrypt(NDSINI.GetINI(vs, "ITPMS", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        str = "Provider=MSDAORA.1; User ID=mobint; Password=mobintweb; Data Source=EBSDB;";

        //str = "Provider=MSDAORA.1; User ID=mobapp; Password=mobapp; Data Source=EBSTESTOLD;";
        // string str = "User ID=piyush; Password=piyush; Data Source=ebsdbstd;";

        //  str = "Provider=MSDAORA.1; User ID=piyush; Password=piyush; Data Source=ebsdbstd;";

        return str;
    }

    public string conYotta()
    {
        string database = "";
        string user_id = "";
        string pass = "";

        Cryptograph crp = new Cryptograph();
        // HttpServerUtility myServer = HttpContext.Current.Server;

        // For live
        //string vs = AppDomain.CurrentDomain.BaseDirectory + "bses.ini";


        //////// For Test
        string vs = AppDomain.CurrentDomain.BaseDirectory + "PRM.ini";
        string PW_KEY = "o8??^am(*)";

        database = crp.Decrypt(NDSINI.GetINI(vs, "itpms", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);// 
        user_id = crp.Decrypt(NDSINI.GetINI(vs, "itpms", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
        pass = crp.Decrypt(NDSINI.GetINI(vs, "itpms", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);// Put user code to initialize the page here

        string str = "Provider=OraOLEDB.Oracle; User ID=MOBAPP; Password=mobapp; Data Source=DSK;";
        //string str = "Provider=MSDAORA.1; User ID=" + user_id + "; Password=" + pass + "; Data Source=" + database + ";";
        return str;
    }

}




