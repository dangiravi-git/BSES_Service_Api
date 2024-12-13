using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using System.Data.OleDb;
//using System.Data.SqlClient;
using USEPMS;
//using Oracle.DataAccess.Client;
using System.Data.OleDb;

public class DbFunction
{
    //OracleTransaction dbtrans;
    //OracleCommand dbcommand;
    //OracleDataAdapter da;
    DataSet ds;
    DataTable dt;

    OleDbTransaction dbtrans1;
    OleDbCommand dbcommand1;
    OleDbDataAdapter da1;

    //static OracleConnection ocon = new OracleConnection(NDS.con());
    //static OracleConnection ocon_dcrep = new OracleConnection(NDS.con_dcrep());
    //static OracleConnection ocon_ccm = new OracleConnection(NDS.con_CCM());
    //static OracleConnection ocon_ONM = new OracleConnection(NDS.con_ONM());
    //static OracleConnection ocon_vss = new OracleConnection(NDS.con_VSS());    
    //static OracleConnection ocon_mobinternal = new OracleConnection(NDS.con_mobinternal());
    //static OracleConnection ocon_RCM = new OracleConnection(NDS.con_RCM());
    //static OracleConnection ocon_REC = new OracleConnection(NDS.con_Recovery());
    //static OracleConnection ocon_Mobapp = new OracleConnection(NDS.con_Mobapp());

    public DbFunction()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void dbbegintrans()
    {
        NDS ndsCon = new NDS();
        OleDbConnection ocon = new OleDbConnection(ndsCon.con());

        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbtrans1 = ocon.BeginTransaction();
        }
        catch (Exception ex)
        {
            string str = ex.Message;
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + "DB Begintrans Error.");
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

    //For Transaction Commit
    public void dbcommittrans()
    {
        NDS ndsCon = new NDS();
        OleDbConnection ocon = new OleDbConnection(ndsCon.con());

        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbtrans1.Commit();
        }
        catch (Exception ex)
        {
            string str = ex.Message;
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + "DB Cmmit Transcation Error.");
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

    //for transaction rollback
    public void dbrollback()
    {
        NDS ndsCon = new NDS();
        OleDbConnection ocon = new OleDbConnection(ndsCon.con());

        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbtrans1.Rollback();
        }
        catch (Exception ex)
        {
            string str = ex.Message;
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + "DB Roll Back Error.");
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

    public DataTable dmlgetqueryLog(string sql)
    {
        NDS ndsCon = new NDS();
        OleDbConnection ocon = new OleDbConnection(ndsCon.con());

        string StrExce;
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand1 = new OleDbCommand();
            dbcommand1.Connection = ocon;
            da1 = new OleDbDataAdapter();
            da1.SelectCommand = dbcommand1;
            dt = null;
            dt = new DataTable();
            dbcommand1.CommandType = CommandType.Text;
            dbcommand1.CommandText = sql;
            dbcommand1.Transaction = dbtrans1;
            da1.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            StrExce = "insert into Mobapp.IVRS_CALL_RESPONSE_DATA_LOG(MSG) values('" + ex.ToString() + "')";
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand1 = new OleDbCommand(StrExce, ocon);
            dbcommand1.Transaction = dbtrans1;
            dbcommand1.ExecuteNonQuery();

            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
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
    public bool dmlsinglequerylog(string sql)
    {
        NDS ndsCon = new NDS();
        OleDbConnection ocon = new OleDbConnection(ndsCon.con());

        string StrExce;
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand1 = new OleDbCommand(sql, ocon);
            dbcommand1.Transaction = dbtrans1;
            dbcommand1.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            StrExce = "insert into Mobapp.IVRS_CALL_RESPONSE_DATA_LOG(MSG) values('" + ex.ToString() + "')";
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand1 = new OleDbCommand(StrExce, ocon);
            dbcommand1.Transaction = dbtrans1;
            dbcommand1.ExecuteNonQuery();

            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
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

    public DataTable dmlgetquery(string sql)
    {
        NDS ndsCon = new NDS();
        OleDbConnection ocon = new OleDbConnection(ndsCon.con());

        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand1 = new OleDbCommand();
            dbcommand1.Connection = ocon;
            da1 = new OleDbDataAdapter();
            da1.SelectCommand = dbcommand1;
            dt = null;
            dt = new DataTable();
            dbcommand1.CommandType = CommandType.Text;
            dbcommand1.CommandText = sql;
            dbcommand1.Transaction = dbtrans1;
            da1.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
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

    public DataTable dmlgetquery_dcrep(string sql)
    {
        NDS ndsDcrep = new NDS();
        OleDbConnection ocon_dcrep = new OleDbConnection(ndsDcrep.con_dcrep());

        try
        {
            if (ocon_dcrep.State == ConnectionState.Closed)
            {
                ocon_dcrep.Open();
            }
            dbcommand1 = new OleDbCommand();
            dbcommand1.Connection = ocon_dcrep;
            da1 = new OleDbDataAdapter();
            da1.SelectCommand = dbcommand1;
            dt = null;
            dt = new DataTable();
            dbcommand1.CommandType = CommandType.Text;
            dbcommand1.CommandText = sql;
            dbcommand1.Transaction = dbtrans1;
            da1.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return dt;
        }
        finally
        {
            if (ocon_dcrep.State == ConnectionState.Open)
            {
                ocon_dcrep.Close();
                ocon_dcrep.Dispose();
            }
        }
    }

    public DataTable dmlgetquery_Rec(string sql)
    {
        NDS ndsCon = new NDS();
        OleDbConnection ocon_REC = new OleDbConnection(ndsCon.con_Recovery());

        try
        {
            if (ocon_REC.State == ConnectionState.Closed)
            {
                ocon_REC.Open();
            }
            dbcommand1 = new OleDbCommand();
            dbcommand1.Connection = ocon_REC;
            da1 = new OleDbDataAdapter();
            da1.SelectCommand = dbcommand1;
            dt = null;
            dt = new DataTable();
            dbcommand1.CommandType = CommandType.Text;
            dbcommand1.CommandText = sql;
            dbcommand1.Transaction = dbtrans1;
            da1.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return dt;
        }
        finally
        {
            if (ocon_REC.State == ConnectionState.Open)
            {
                ocon_REC.Close();
                ocon_REC.Dispose();
            }
        }
    }

    public bool dmlsinglequery(string sql)
    {
        NDS ndsCon = new NDS();
        OleDbConnection ocon = new OleDbConnection(ndsCon.con());

        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand1 = new OleDbCommand(sql, ocon);
            dbcommand1.Transaction = dbtrans1;
            dbcommand1.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
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

    public string dmlsinglequeryExp(string sql)
    {
        NDS ndsCon = new NDS();
        OleDbConnection ocon = new OleDbConnection(ndsCon.con());

        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand1 = new OleDbCommand(sql, ocon);
            dbcommand1.Transaction = dbtrans1;
            dbcommand1.ExecuteNonQuery();
            return "true";
        }
        catch (Exception ex)
        {
            //NewClassFile newClassFile = new NewClassFile();
            //newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return ex.Message.ToString();
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

    public bool dmlsinglequery_dcrep(string sql)
    {
        NDS ndsDcrep = new NDS();
        OleDbConnection ocon_dcrep = new OleDbConnection(ndsDcrep.con_dcrep());

        try
        {
            if (ocon_dcrep.State == ConnectionState.Closed)
            {
                ocon_dcrep.Open();
            }
            dbcommand1 = new OleDbCommand(sql, ocon_dcrep);
            dbcommand1.Transaction = dbtrans1;
            dbcommand1.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return false;
        }
        finally
        {
            if (ocon_dcrep.State == ConnectionState.Open)
            {
                ocon_dcrep.Close();
                ocon_dcrep.Dispose();
            }
        }
    }

    public bool dmlsinglequery_Rec(string sql)
    {
        NDS ndsRec = new NDS();
        OleDbConnection ocon_REC = new OleDbConnection(ndsRec.con_Recovery());

        try
        {
            if (ocon_REC.State == ConnectionState.Closed)
            {
                ocon_REC.Open();
            }
            dbcommand1 = new OleDbCommand(sql, ocon_REC);
            dbcommand1.Transaction = dbtrans1;
            dbcommand1.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return false;
        }
        finally
        {
            if (ocon_REC.State == ConnectionState.Open)
            {
                ocon_REC.Close();
                ocon_REC.Dispose();
            }
        }
    }

    public bool dmlMultiplequery_CCM(string[] sql)
    {
        NDS ndsCcm = new NDS();
        OleDbConnection ocon_ccm = new OleDbConnection(ndsCcm.con_CCM());

        int tem1 = 0;
        OleDbTransaction dbtrans1 = null;
        try
        {

            if (ocon_ccm.State == ConnectionState.Closed)
            {
                ocon_ccm.Open();
            }

            dbtrans1 = ocon_ccm.BeginTransaction();
            if (sql[0] != null && sql[0] != "")
            {
                dbcommand1 = new OleDbCommand(sql[0], ocon_ccm);
                dbcommand1.Transaction = dbtrans1;
                tem1 = dbcommand1.ExecuteNonQuery();
            }
            else
            {
                tem1 = 1;
            }
            if (tem1 > 0)
            {
                int tem2 = 0;
                for (int i = 1; i < sql.Length; i++)
                {
                    if (sql[i] != null && sql[i] != "")
                    {
                        dbcommand1 = new OleDbCommand(sql[i], ocon_ccm);
                        dbcommand1.Transaction = dbtrans1;
                        tem2 = dbcommand1.ExecuteNonQuery();
                        if (tem2 == 0)
                        {
                            dbtrans1.Rollback();
                            return false;
                        }
                    }
                }
                dbtrans1.Commit();
                return true;
            }
            else
            {
                dbtrans1.Rollback();
                return false;
            }


        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            dbtrans1.Rollback();
            return false;
        }
        finally
        {
            if (ocon_ccm.State == ConnectionState.Open)
            {
                ocon_ccm.Close();
                ocon_ccm.Dispose();
            }

        }
    }



    public bool dmlMultiplequery(string[] sql)
    {

        NDS ndsCon = new NDS();
        OleDbConnection ocon = new OleDbConnection(ndsCon.con());

        int tem1 = 0;
        OleDbTransaction dbtrans1 = null;
        try
        {

            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }

            dbtrans1 = ocon.BeginTransaction();
            if (sql[0] != null && sql[0] != "")
            {
                dbcommand1 = new OleDbCommand(sql[0], ocon);
                dbcommand1.Transaction = dbtrans1;
                tem1 = dbcommand1.ExecuteNonQuery();
            }
            else
            {
                tem1 = 1;
            }
            if (tem1 > 0)
            {
                int tem2 = 0;
                for (int i = 1; i < sql.Length; i++)
                {
                    if (sql[i] != null && sql[i] != "")
                    {
                        dbcommand1 = new OleDbCommand(sql[i], ocon);
                        dbcommand1.Transaction = dbtrans1;
                        tem2 = dbcommand1.ExecuteNonQuery();
                        if (tem2 == 0)
                        {
                            dbtrans1.Rollback();
                            return false;
                        }
                    }
                }
                dbtrans1.Commit();
                return true;
            }
            else
            {
                dbtrans1.Rollback();
                return false;
            }


        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            dbtrans1.Rollback();
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

    // for onm
    public void dbbegintrans_ONM()
    {
        NDS ndsONM = new NDS();
        OleDbConnection ocon_ONM = new OleDbConnection(ndsONM.con_ONM());

        try
        {
            if (ocon_ONM.State == ConnectionState.Closed)
            {
                ocon_ONM.Open();
            }
            dbtrans1 = ocon_ONM.BeginTransaction();
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + "ONM Begin Transaction");
            //return false;
        }
        finally
        {
            if (ocon_ONM.State == ConnectionState.Open)
            {
                ocon_ONM.Close();
                ocon_ONM.Dispose();
            }
        }
    }

    public bool dmlsinglequeryONM_New(string sql)
    {
        NDS ndsONM = new NDS();
        OleDbConnection ocon_ONM = new OleDbConnection(ndsONM.con_ONM());

        try
        {
            if (ocon_ONM.State == ConnectionState.Closed)
            {
                ocon_ONM.Open();
            }
            dbcommand1 = new OleDbCommand(sql, ocon_ONM);
            //dbcommand1.Transaction = dbtrans1;
            dbcommand1.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return false;
        }
        finally
        {
            if (ocon_ONM.State == ConnectionState.Open)
            {
                ocon_ONM.Close();
                ocon_ONM.Dispose();
            }
        }
    }

    public void dbcommittrans_ONM()
    {
        NDS ndsONM = new NDS();
        OleDbConnection ocon_ONM = new OleDbConnection(ndsONM.con_ONM());

        try
        {
            if (ocon_ONM.State == ConnectionState.Closed)
            {
                ocon_ONM.Open();
            }
            dbtrans1.Commit();
        }
        catch (Exception ex)
        {
            string str = ex.Message;
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + str);
        }
        finally
        {
            if (ocon_ONM.State == ConnectionState.Open)
            {
                ocon_ONM.Close();
                ocon_ONM.Dispose();
            }
        }
    }

    public void dbrollback_ONM()
    {
        NDS ndsONM = new NDS();
        OleDbConnection ocon_ONM = new OleDbConnection(ndsONM.con_ONM());

        try
        {
            if (ocon_ONM.State == ConnectionState.Closed)
            {
                ocon_ONM.Open();
            }
            dbtrans1.Rollback();
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + "ONM Roll Back");

        }
        finally
        {
            if (ocon_ONM.State == ConnectionState.Open)
            {
                ocon_ONM.Close();
                ocon_ONM.Dispose();
            }
        }
    }

    public DataTable dmlgetquery_PCM1(string sql)
    {
        NDS ndsPCM = new NDS();
        OleDbConnection ocon_PCM1 = new OleDbConnection(ndsPCM.con_PCM1());

        try
        {
            if (ocon_PCM1.State == ConnectionState.Closed)
            {
                ocon_PCM1.Open();
            }
            dbcommand1 = new OleDbCommand();
            dbcommand1.Connection = ocon_PCM1;
            da1 = new OleDbDataAdapter();
            da1.SelectCommand = dbcommand1;
            dt = null;
            dt = new DataTable();
            dbcommand1.CommandType = CommandType.Text;
            dbcommand1.CommandText = sql;
            dbcommand1.Transaction = dbtrans1;
            da1.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return dt;
        }
        finally
        {
            if (ocon_PCM1.State == ConnectionState.Open)
            {
                ocon_PCM1.Close();
                ocon_PCM1.Dispose();
            }
        }
    }


    public String dmlgetquery_Test(string sql)
    {
        NDS ndsONM = new NDS();
        OleDbConnection ocon_ONM = new OleDbConnection(ndsONM.con_ONM());

        try
        {
            if (ocon_ONM.State == ConnectionState.Closed)
            {
                ocon_ONM.Open();

            }
            dbcommand1 = new OleDbCommand();
            dbcommand1.Connection = ocon_ONM;
            da1 = new OleDbDataAdapter();
            da1.SelectCommand = dbcommand1;
            dt = null;
            dt = new DataTable();
            dbcommand1.CommandType = CommandType.Text;
            dbcommand1.CommandText = sql;
            dbcommand1.Transaction = dbtrans1;
            //da1.Fill(dt);

            return "connected";
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return ex.ToString();
        }
        finally
        {
            if (ocon_ONM.State == ConnectionState.Open)
            {
                ocon_ONM.Close();
                ocon_ONM.Dispose();
            }
        }
    }

    public DataTable dmlgetquery_ONM(string sql)
    {
        NDS ndsONM = new NDS();
        OleDbConnection ocon_ONM = new OleDbConnection(ndsONM.con_ONM());

        try
        {
            if (ocon_ONM.State == ConnectionState.Closed)
            {
                ocon_ONM.Open();

            }
            dbcommand1 = new OleDbCommand();
            dbcommand1.Connection = ocon_ONM;
            da1 = new OleDbDataAdapter();
            da1.SelectCommand = dbcommand1;
            dt = null;
            dt = new DataTable();
            dbcommand1.CommandType = CommandType.Text;
            dbcommand1.CommandText = sql;
            dbcommand1.Transaction = dbtrans1;
            da1.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return dt;
        }
        finally
        {
            if (ocon_ONM.State == ConnectionState.Open)
            {
                ocon_ONM.Close();
                ocon_ONM.Dispose();
            }
        }
    }

    public bool dmlsinglequery_ONM(string sql)
    {
        NDS ndsONM = new NDS();
        OleDbConnection ocon_ONM = new OleDbConnection(ndsONM.con_ONM());

        try
        {
            if (ocon_ONM.State == ConnectionState.Closed)
            {
                ocon_ONM.Open();
            }
            dbcommand1 = new OleDbCommand(sql, ocon_ONM);
            dbcommand1.Transaction = dbtrans1;
            dbcommand1.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return false;
        }
        finally
        {
            if (ocon_ONM.State == ConnectionState.Open)
            {
                ocon_ONM.Close();
                ocon_ONM.Dispose();
            }
        }
    }

    public bool dmlMultiplequery_ONM(string[] sql)
    {
        NDS ndsONM = new NDS();
        OleDbConnection ocon_ONM = new OleDbConnection(ndsONM.con_ONM());

        int tem1 = 0;
        OleDbTransaction dbtrans1 = null;
        try
        {

            if (ocon_ONM.State == ConnectionState.Closed)
            {
                ocon_ONM.Open();
            }

            dbtrans1 = ocon_ONM.BeginTransaction();
            if (sql[0] != null && sql[0] != "")
            {
                dbcommand1 = new OleDbCommand(sql[0], ocon_ONM);
                dbcommand1.Transaction = dbtrans1;
                tem1 = dbcommand1.ExecuteNonQuery();
            }
            else
            {
                tem1 = 1;
            }
            if (tem1 > 0)
            {
                int tem2 = 0;
                for (int i = 1; i < sql.Length; i++)
                {
                    if (sql[i] != null && sql[i] != "")
                    {
                        dbcommand1 = new OleDbCommand(sql[i], ocon_ONM);
                        dbcommand1.Transaction = dbtrans1;
                        tem2 = dbcommand1.ExecuteNonQuery();
                        if (tem2 == 0)
                        {
                            dbtrans1.Rollback();
                            return false;
                        }
                    }
                }
                dbtrans1.Commit();
                return true;
            }
            else
            {
                dbtrans1.Rollback();
                return false;
            }


        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            dbtrans1.Rollback();
            return false;
        }
        finally
        {
            if (ocon_ONM.State == ConnectionState.Open)
            {
                ocon_ONM.Close();
                ocon_ONM.Dispose();
            }

        }
    }

    public bool dmlsinglequery_VSS(string sql)
    {
        NDS ndsVss = new NDS();
        OleDbConnection ocon_vss = new OleDbConnection(ndsVss.con_VSS());

        try
        {
            if (ocon_vss.State == ConnectionState.Closed)
            {
                ocon_vss.Open();
            }
            dbcommand1 = new OleDbCommand(sql, ocon_vss);
            dbcommand1.Transaction = dbtrans1;
            dbcommand1.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return false;
        }
        finally
        {
            if (ocon_vss.State == ConnectionState.Open)
            {
                ocon_vss.Close();
                ocon_vss.Dispose();
            }
        }
    }

    public DataTable dmlgetquery_mobint(string sql)
    {
        NDS ndsMobinternal = new NDS();
        OleDbConnection ocon_mobinternal = new OleDbConnection(ndsMobinternal.con_mobinternal());

        try
        {
            if (ocon_mobinternal.State == ConnectionState.Closed)
            {
                ocon_mobinternal.Open();
            }
            dbcommand1 = new OleDbCommand();
            dbcommand1.Connection = ocon_mobinternal;
            da1 = new OleDbDataAdapter();
            da1.SelectCommand = dbcommand1;
            dt = null;
            dt = new DataTable();
            dbcommand1.CommandType = CommandType.Text;
            dbcommand1.CommandText = sql;
            dbcommand1.Transaction = dbtrans1;
            da1.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return dt;
        }
        finally
        {
            if (ocon_mobinternal.State == ConnectionState.Open)
            {
                ocon_mobinternal.Close();
                ocon_mobinternal.Dispose();
            }
        }
    }

    public bool dmlsinglequery_mobint(string sql)
    {
        NDS ndsMobinternal = new NDS();
        OleDbConnection ocon_mobinternal = new OleDbConnection(ndsMobinternal.con_mobinternal());

        try
        {
            if (ocon_mobinternal.State == ConnectionState.Closed)
            {
                ocon_mobinternal.Open();
            }
            dbcommand1 = new OleDbCommand(sql, ocon_mobinternal);
            dbcommand1.Transaction = dbtrans1;
            dbcommand1.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return false;
        }
        finally
        {
            if (ocon_mobinternal.State == ConnectionState.Open)
            {
                ocon_mobinternal.Close();
                ocon_mobinternal.Dispose();
            }
        }
    }

    public int dmlsinglequery_mobintUpdate(string sql)
    {
        NDS ndsMobinternal = new NDS();
        OleDbConnection ocon_mobinternal = new OleDbConnection(ndsMobinternal.con_mobinternal());

        try
        {
            if (ocon_mobinternal.State == ConnectionState.Closed)
            {
                ocon_mobinternal.Open();
            }
            dbcommand1 = new OleDbCommand(sql, ocon_mobinternal);
            dbcommand1.Transaction = dbtrans1;
            return dbcommand1.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return -1;
        }
        finally
        {
            if (ocon_mobinternal.State == ConnectionState.Open)
            {
                ocon_mobinternal.Close();
                ocon_mobinternal.Dispose();
            }
        }
    }

    public DataTable dmlgetquery_RCM(string sql)
    {
        NDS ndsRCM = new NDS();
        OleDbConnection ocon_RCM = new OleDbConnection(ndsRCM.con_RCM());

        try
        {
            if (ocon_RCM.State == ConnectionState.Closed)
            {
                ocon_RCM.Open();

            }
            dbcommand1 = new OleDbCommand();
            dbcommand1.Connection = ocon_RCM;
            da1 = new OleDbDataAdapter();
            da1.SelectCommand = dbcommand1;
            dt = null;
            dt = new DataTable();
            dbcommand1.CommandType = CommandType.Text;
            dbcommand1.CommandText = sql;
            dbcommand1.Transaction = dbtrans1;
            da1.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return dt;
        }
        finally
        {
            if (ocon_RCM.State == ConnectionState.Open)
            {
                ocon_RCM.Close();
                ocon_RCM.Dispose();
            }
        }
    }

    public DataTable dmlgetquery_mobApp(string sql)
    {
        NDS ndsMobinternal = new NDS();
        OleDbConnection ocon_Mobapp = new OleDbConnection(ndsMobinternal.con_Mobapp());

        try
        {
            if (ocon_Mobapp.State == ConnectionState.Closed)
            {
                ocon_Mobapp.Open();
            }
            dbcommand1 = new OleDbCommand();
            dbcommand1.Connection = ocon_Mobapp;
            da1 = new OleDbDataAdapter();
            da1.SelectCommand = dbcommand1;
            dt = null;
            dt = new DataTable();
            dbcommand1.CommandType = CommandType.Text;
            dbcommand1.CommandText = sql;
            dbcommand1.Transaction = dbtrans1;
            da1.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return dt;
        }
        finally
        {
            if (ocon_Mobapp.State == ConnectionState.Open)
            {
                ocon_Mobapp.Close();
                ocon_Mobapp.Dispose();
            }
        }
    }

    public bool dmlsinglequery_mobApp(string sql)
    {
        NDS ndsMobinternal = new NDS();
        OleDbConnection ocon_Mobapp = new OleDbConnection(ndsMobinternal.con_Mobapp());

        try
        {
            if (ocon_Mobapp.State == ConnectionState.Closed)
            {
                ocon_Mobapp.Open();
            }
            dbcommand1 = new OleDbCommand(sql, ocon_Mobapp);
            dbcommand1.Transaction = dbtrans1;
            dbcommand1.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return false;
        }
        finally
        {
            if (ocon_Mobapp.State == ConnectionState.Open)
            {
                ocon_Mobapp.Close();
                ocon_Mobapp.Dispose();
            }
        }
    }
}
