using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Data.OleDb;
using System.Text;

/// <summary>
/// Summary description for NDS
/// </summary>
public class NDS
{
    public NDS()
    {

    }
    public static string con()
    {
        string Strcon = string.Empty;
        string Database = string.Empty;
        string Userid = string.Empty;
        string Pass = string.Empty;
        string vs = string.Empty;
        try
        {
            Cryptograph crp = new Cryptograph();
            vs = AppDomain.CurrentDomain.BaseDirectory + "bsesmobint.INI";
            string PW_KEY = "o8??^am(*)";
            Userid = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            Pass = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            Database = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);
            Strcon = "Provider=oraoledb.oracle; User ID=" + Userid + "; Password=" + Pass + "; Data Source=" + Database + ";";
            //Strcon = "provider=oraoledb.oracle; User ID=piyush; Password=piyush; Data Source=ebsdbstd;";
            //Strcon = "provider=oraoledb.oracle; User ID=mobint; Password=mobint; Data Source=ebstestold;";
            //Strcon = "provider=oraoledb.oracle; User ID=piyush; Password=piyush; Data Source=ebsdbstd;";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Strcon;
    }


    public static string con_ITHELP()
    {
        string Strcon = string.Empty;
        string Database = string.Empty;
        string Userid = string.Empty;
        string Pass = string.Empty;
        string vs = string.Empty;
        try
        {
            Cryptograph crp = new Cryptograph();
            vs = AppDomain.CurrentDomain.BaseDirectory + "BSES.INI";
            string PW_KEY = "o8??^am(*)";
            Userid = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            Pass = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            Database = crp.Decrypt(NDSINI.GetINI(vs, "CCM", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);
            Strcon = "Provider=oraoledb.oracle; User ID=" + Userid + "; Password=" + Pass + "; Data Source=" + Database + ";";


            //Strcon = "Provider=oraoledb.oracle; User ID=ccm; Password=ccm; Data Source=ebstestold;";
            //  Strcon = "Provider=oraoledb.oracle; User ID=piyush; Password=piyush; Data Souce=EBSDBSTD;";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Strcon;
    }

    public static string conONM()
    {
        string Strcon = string.Empty;
        string Database = string.Empty;
        string Userid = string.Empty;
        string Pass = string.Empty;
        string vs = string.Empty;
        try
        {
            //Cryptograph crp = new Cryptograph();
            //vs = AppDomain.CurrentDomain.BaseDirectory + "bsesmobint.INI";
            //string PW_KEY = "o8??^am(*)";
            //Userid = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserid", PW_KEY), "?"), PW_KEY);
            //Pass = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbuserpwd", PW_KEY), "?"), PW_KEY);
            //Database = crp.Decrypt(NDSINI.GetINI(vs, "mobint", crp.Encrypt("dbconn", PW_KEY), "?"), PW_KEY);
            //Strcon = "Provider=oraoledb.oracle; User ID=" + Userid + "; Password=" + Pass + "; Data Source=" + Database + ";";
            Strcon = "provider=oraoledb.oracle; User ID=prodqry; Password=p12345; Data Source=ebsdb;";
            //Strcon = "provider=oraoledb.oracle; User ID=mobint; Password=mobint; Data Source=ebstestold;";
            //Strcon = "provider=oraoledb.oracle; User ID=piyush; Password=piyush; Data Source=ebsdbstd;";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Strcon;
    }
}