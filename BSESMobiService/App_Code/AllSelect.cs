using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Collections.Generic;
using USEPMS;

/// <summary>
/// Summary description for AllSelect
/// </summary>
public class AllSelect
{   

    private  DbFunction objdbfun;
    private  bool Result;
    private  DataTable dt;
    private  string sql;

    //static OleDbConnection ocon_ccm = new OleDbConnection(NDS.con_CCM());
    //static OleDbConnection ocon_vss = new OleDbConnection(NDS.con_VSS());

    //OleDbConnection ocon_onm = new OleDbConnection(NDS.con_ONM());

    public AllSelect()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetShowDetails(string _ssql)
    {
        string sql = _ssql;
        dt = new DataTable();
        objdbfun = new DbFunction();
        dt = objdbfun.dmlgetquery_ONM(sql);
        return dt;
    }

    public DataTable GetShowDetails(string _ssql, string strCaNo, string strImei)
    {
        DataTable dt = new DataTable();
        List<DataColumn> listCols = new List<DataColumn>();

        NDS ndsCon = new NDS();
        OleDbConnection ocon_onm = new OleDbConnection(ndsCon.con_ONM());

        string sql = _ssql;
        try
        {
            if (ocon_onm.State == ConnectionState.Closed)
            {
                ocon_onm.Open();
            }
            OleDbCommand oleDbCommand = new OleDbCommand(sql, ocon_onm);

            OleDbParameter CA_NO = oleDbCommand.Parameters.Add("@CA_NO", OleDbType.VarChar, 12);
            OleDbParameter IMEI_NO = oleDbCommand.Parameters.Add("@IMEI_NO", OleDbType.VarChar, 16);

            CA_NO.Value = strCaNo;
            IMEI_NO.Value = strImei;

            OleDbDataReader rdr = oleDbCommand.ExecuteReader();

            DataTable dtSchema = rdr.GetSchemaTable();

            if (dtSchema != null)
            {
                foreach (DataRow drow in dtSchema.Rows)
                {
                    string columnName = System.Convert.ToString(drow["ColumnName"]);
                    DataColumn column = new DataColumn(columnName, (Type)(drow["DataType"]));
                    column.Unique = (bool)drow["IsUnique"];
                    column.AllowDBNull = (bool)drow["AllowDBNull"];
                    column.AutoIncrement = (bool)drow["IsAutoIncrement"];
                    listCols.Add(column);
                    dt.Columns.Add(column);
                }
            }

            while (rdr.Read())
            {
                string str = rdr.GetString(0);
                DataRow dataRow = dt.NewRow();
                for (int i = 0; i < listCols.Count; i++)
                {
                    dataRow[((DataColumn)listCols[i])] = rdr[i];
                }
                dt.Rows.Add(dataRow);
            }

        }
        catch (Exception ex)
        {
            NewClassFile newClassFile = new NewClassFile();
            newClassFile.WriteIntoFile(DateTime.Now.ToString() + ex.ToString() + sql);
            return dt;
        }
        finally
        {
            if (ocon_onm.State == ConnectionState.Open)
            {
                ocon_onm.Close();
                ocon_onm.Dispose();
            }
        }
        return dt;
    }

    public  DataTable GetGCMRegDetails(string _ssql)
    {
        string sql = _ssql;
        dt = new DataTable();
        objdbfun = new DbFunction();
        dt = objdbfun.dmlgetquery_ONM(sql);
        return dt;
    }

    public  DataTable TTS_Login(string sql)
    {
        NDS ndsCCM = new NDS();
        OleDbConnection ocon_ccm = new OleDbConnection(ndsCCM.con_CCM());

        DataSet ds = new DataSet();
        OleDbCommand cmd = new OleDbCommand(sql, ocon_ccm);
        ocon_ccm.Open();

        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        adapter.SelectCommand = new OleDbCommand(sql, ocon_ccm);
        adapter.Fill(ds, "TTS_LOGIN_MASTER");
        if (ocon_ccm.State == ConnectionState.Open)
        {
            ocon_ccm.Close();
            ocon_ccm.Dispose();
        }
        return ds.Tables["TTS_LOGIN_MASTER"];
    }

    public  DataTable TTS_Allotment(string sql)
    {
        NDS ndsCCM = new NDS();
        OleDbConnection ocon_ccm = new OleDbConnection(ndsCCM.con_CCM());

        DataSet ds = new DataSet();
        OleDbCommand cmd = new OleDbCommand(sql, ocon_ccm);
        ocon_ccm.Open();
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        adapter.SelectCommand = new OleDbCommand(sql, ocon_ccm);
        adapter.Fill(ds, "TTS_TASK_ALLOTMENT");
        if (ocon_ccm.State == ConnectionState.Open)
        {
            ocon_ccm.Close();
            ocon_ccm.Dispose();
        }
        return ds.Tables["TTS_TASK_ALLOTMENT"];
    }

    public  bool InsertByMultipleSQLCCM(string[] _sSql)
    {
        objdbfun = new DbFunction();
        return (objdbfun.dmlMultiplequery_CCM(_sSql));
    }

    #region Admin Support System

    public  DataTable VSS_ComplaintCentre(string sql)
    {        
        NDS ndsCCM = new NDS();
        OleDbConnection ocon_vss = new OleDbConnection(ndsCCM.con_VSS());

        DataSet ds = new DataSet();
        OleDbCommand cmd = new OleDbCommand(sql, ocon_vss);
        ocon_vss.Open();
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        adapter.SelectCommand = new OleDbCommand(sql, ocon_vss);
        adapter.Fill(ds, "ASS_DIVISION_LOCATION_DT ");
        if (ocon_vss.State == ConnectionState.Open)
        {
            ocon_vss.Close();
            ocon_vss.Dispose();
        }
        return ds.Tables["ASS_DIVISION_LOCATION_DT "];
    }

    public  DataTable VSS_RequestType(string sql)
    {        
        NDS ndsCCM = new NDS();
        OleDbConnection ocon_vss = new OleDbConnection(ndsCCM.con_VSS());

        DataSet ds = new DataSet();
        OleDbCommand cmd = new OleDbCommand(sql, ocon_vss);
        ocon_vss.Open();
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        adapter.SelectCommand = new OleDbCommand(sql, ocon_vss);
        adapter.Fill(ds, "ASS_REQUEST_TYPE ");
        if (ocon_vss.State == ConnectionState.Open)
        {
            ocon_vss.Close();
            ocon_vss.Dispose();
        }
        return ds.Tables["ASS_REQUEST_TYPE "];
    }

    public  DataTable VSS_AssignDetails(string sql)
    {        
        NDS ndsCCM = new NDS();
        OleDbConnection ocon_vss = new OleDbConnection(ndsCCM.con_VSS());

        DataSet ds = new DataSet();
        OleDbCommand cmd = new OleDbCommand(sql, ocon_vss);
        ocon_vss.Open();
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        adapter.SelectCommand = new OleDbCommand(sql, ocon_vss);
        adapter.Fill(ds, "ASS_PROCESS_SPOC_ASSIGN ");
        if (ocon_vss.State == ConnectionState.Open)
        {
            ocon_vss.Close();
            ocon_vss.Dispose();
        }
        return ds.Tables["ASS_PROCESS_SPOC_ASSIGN "];
    }

    public  DataTable compl_No()
    {
        NDS ndsCCM = new NDS();
        OleDbConnection ocon_vss = new OleDbConnection(ndsCCM.con_VSS());

        string sql = "SELECT TO_CHAR(SYSDATE,'ddmmyy')||TO_CHAR(NVL(MAX(TO_NUMBER((SUBSTR(COMP_NO,7)))),0)+1) AS COMP_NO  FROM ASS_COMP_MST WHERE SUBSTR(COMP_NO,1,6)=TO_CHAR(SYSDATE,'ddmmyy')";

        DataSet ds = new DataSet();
        OleDbCommand cmd = new OleDbCommand(sql, ocon_vss);
        ocon_vss.Open();
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        adapter.SelectCommand = new OleDbCommand(sql, ocon_vss);
        adapter.Fill(ds, "ASS_COMP_MST ");
        if (ocon_vss.State == ConnectionState.Open)
        {
            ocon_vss.Close();
            ocon_vss.Dispose();
        }
        return ds.Tables["ASS_COMP_MST "];
    }

    public  DataTable VSS_GetComplaintDetails(string sql)
    {
        NDS ndsCCM = new NDS();
        OleDbConnection ocon_vss = new OleDbConnection(ndsCCM.con_VSS());

        DataSet ds = new DataSet();
        OleDbCommand cmd = new OleDbCommand(sql, ocon_vss);
        ocon_vss.Open();
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        adapter.SelectCommand = new OleDbCommand(sql, ocon_vss);
        adapter.Fill(ds, "ASS_COMP_MST ");
        if (ocon_vss.State == ConnectionState.Open)
        {
            ocon_vss.Close();
            ocon_vss.Dispose();
        }
        return ds.Tables["ASS_COMP_MST "];
    }

    public  DataTable AssignToID(string _sAssignName)
    {
        NDS ndsCCM = new NDS();
        OleDbConnection ocon_vss = new OleDbConnection(ndsCCM.con_VSS());

        string sql = "SELECT DISTINCT PROC_OWNER_ID FROM  ASS_PROCESS_SPOC_ASSIGN WHERE UPPER(PROCESS_OWNER_NAME) =UPPER('" + _sAssignName + "') ";

        DataSet ds = new DataSet();
        OleDbCommand cmd = new OleDbCommand(sql, ocon_vss);
        ocon_vss.Open();
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        adapter.SelectCommand = new OleDbCommand(sql, ocon_vss);
        adapter.Fill(ds, "ASS_PROCESS_SPOC_ASSIGN ");
        if (ocon_vss.State == ConnectionState.Open)
        {
            ocon_vss.Close();
            ocon_vss.Dispose();
        }
        return ds.Tables["ASS_PROCESS_SPOC_ASSIGN "];
    }

    public DataTable VSS_RemarkDetails(string sql)
    {
        NDS ndsCCM = new NDS();
        OleDbConnection ocon_vss = new OleDbConnection(ndsCCM.con_VSS());

        DataSet ds = new DataSet();
        OleDbCommand cmd = new OleDbCommand(sql, ocon_vss);
        ocon_vss.Open();
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        adapter.SelectCommand = new OleDbCommand(sql, ocon_vss);
        adapter.Fill(ds, "ASS_COMP_CLOSE ");
        if (ocon_vss.State == ConnectionState.Open)
        {
            ocon_vss.Close();
            ocon_vss.Dispose();
        }
        return ds.Tables["ASS_COMP_CLOSE "];
    }

    #endregion


    public DataTable GetShowPCMDetails(string _ssql)
    {
        string sql = _ssql;
        dt = new DataTable();
        objdbfun = new DbFunction();
        dt = objdbfun.dmlgetquery_PCM1(sql);
        return dt;
    }

}
