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
using System.Collections;


/// <summary>
/// Summary description for DbConnection
/// </summary>
public class DbConnection :System.Web.UI.Page
{
    #region"Member Variables"
    OleDbConnection mCon; 
    //OleDbConnection mCon;
    OleDbCommand mDataCom;
    //OleDbCommand mDataCom;
    OleDbDataAdapter mDAdapter;
    //OleDbDataAdapter mDAdapter;

    #endregion

    #region"Constructor"
    public DbConnection()
    {try{
        //String conStr = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
        //mCon = new OleDbConnection(conStr);
        mCon = new OleDbConnection(NDS.GetDAMcon());
    }
    catch (Exception ex)
    {  }
    }
    #endregion

    #region"Methods"
    public bool OpenConnection()
    {
        try
        {

            if (mCon.State == ConnectionState.Closed)
            {
                mCon.Open();
            }
            mDataCom = new OleDbCommand();
            mDataCom.Connection = mCon;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool CloseConnection()
    {
        try
        {
            if (mCon.State == ConnectionState.Open)
            {
                mCon.Close();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public OleDbConnection GetConnection
    {
        get { return this.mCon; }
    }

    public bool DisposeConnection()
    {
        mCon.Dispose();
        return true;
    }

    public bool isDatabaseCanBeConnected()
    {
        if (OpenConnection())
            return true;
        else
            return false;
    }

    public int executeSql(string sqlQuery)
    {
        int intResult = -1;
        try
        {
            OpenConnection();
            mDataCom.CommandType = CommandType.Text;
            mDataCom.CommandText = sqlQuery;
            intResult = mDataCom.ExecuteNonQuery();
            CloseConnection();
            DisposeConnection();
        }
        catch (Exception ex)
        {
            Session["ErrorMessage"] = ex.Message.ToString();
        }
        return intResult;
    }

    public int executeArrayOfSql(ArrayList sqlQueryArr)
    {
        int intResult = 0;
        OpenConnection();
//        SqlTransaction tran;
        OleDbTransaction tran; 
        tran = mCon.BeginTransaction();
        try
        {
            for (int i = 0; i <= sqlQueryArr.Count - 1; i++) // changes by narendra <= has been added
            {
                mDataCom.CommandType = CommandType.Text;
                mDataCom.Transaction = tran;
                mDataCom.CommandText = sqlQueryArr[i].ToString();
                intResult = intResult + mDataCom.ExecuteNonQuery();
            }
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            Session["ErrorMessage"] = ex.Message.ToString();
        }
        CloseConnection();
        DisposeConnection();
        return intResult;
    }

    public DataTable getDataTable(string sqlQuery)
    {
        DataTable mDT = new DataTable();
        try
        {
            OpenConnection();
            mDAdapter = new OleDbDataAdapter(sqlQuery, mCon);
            mDAdapter.Fill(mDT);
            CloseConnection();
            DisposeConnection();
        }
        catch (Exception ex)
        {
            Session["ErrorMessage"] = ex.Message.ToString();
        }
        return mDT;
    }


    public DataTable getDataRows(string sqlQuery)
    {
        DataTable mDT = new DataTable();
        try
        {
            mDAdapter = new OleDbDataAdapter(sqlQuery, mCon);
            mDAdapter.Fill(mDT);
        }
        catch (Exception ex)
        {
            Session["ErrorMessage"] = ex.Message.ToString();
        }
        return mDT;
    }

    public object getParticularData(string sqlQuery)
    {
        object obj = "";
        try
        {
            OpenConnection();
            mDataCom.CommandType = CommandType.Text;
            mDataCom.CommandText = sqlQuery;
            obj = mDataCom.ExecuteScalar();
            CloseConnection();
            DisposeConnection();
        }
        catch (Exception ex)
        {
            Session["ErrorMessage"] = ex.Message.ToString();
        }
        return obj;
    }


    public DataTable getCustomizedDataTableInTreeStyle(DataTable dtToBeCustomized, string primaryKeyFieldName, string parentIdFieldName, string customizedFieldName, char customChar)
    {
        //int noOfDash;
        DataTable dtCustomized;
        DataColumn[] pKeys = new DataColumn[1] { dtToBeCustomized.Columns[primaryKeyFieldName] };
        dtToBeCustomized.PrimaryKey = pKeys;
        dtCustomized = dtToBeCustomized.Clone();
        if (dtToBeCustomized.Rows.Count > 0)
        {
            DataView dv = new DataView(dtToBeCustomized);
            dv.RowFilter = parentIdFieldName + "='0'";
            for (int i = 0; i < dv.Count; i++)
            {
                string str = dv[i][customizedFieldName].ToString();

                dtCustomized.ImportRow(dv[i].Row);
                CustomizeText(ref dtCustomized, dtToBeCustomized, primaryKeyFieldName, parentIdFieldName, customizedFieldName, dv[i][primaryKeyFieldName].ToString(), customChar, 1);
            }
        }
        return dtCustomized;
    }

    private void CustomizeText(ref DataTable dtCstm, DataTable dt, string primaryKeyFieldName, string parentIdFieldName, string customizedFieldName, string id, char customChar, int noOfDash)
    {
        DataView dv = new DataView(dt);
        dv.RowFilter = parentIdFieldName + "='" + id + "'";
        if (dv.Count > 0)
        {
            for (int i = 0; i < dv.Count; i++)
            {
                string str = dv[i][customizedFieldName].ToString();

                dtCstm.ImportRow(dv[i].Row);
                dtCstm.Rows.Find(dv[i][primaryKeyFieldName].ToString())[customizedFieldName] = dv[i][customizedFieldName].ToString().PadLeft(dv[i][customizedFieldName].ToString().Trim().Length + 3 * noOfDash, customChar);
                CustomizeText(ref dtCstm, dt, primaryKeyFieldName, parentIdFieldName, customizedFieldName, dv[i][primaryKeyFieldName].ToString(), customChar, noOfDash + 1);
            }
        }
    }


    #endregion

    public static OleDbParameter CreateParameter(string Parameter, DbType type, object Value)
    {
        //OleDbParameter _spm = new OleDbParameter();
        OleDbParameter _spm = new OleDbParameter();  
        _spm.ParameterName = Parameter;
        _spm.DbType = type;
        _spm.Value = Value;
        return _spm;
    }

    public static OleDbParameter CreateParameter(string Parameter, DbType type, object Value, ParameterDirection Direction)
    {
        OleDbParameter _spm = new OleDbParameter();
        _spm.ParameterName = Parameter;
        _spm.DbType = type;
        _spm.Value = Value;
        _spm.Direction = Direction;
        if (type == DbType.String)
        {
            _spm.Size = 200;
        }
        return _spm;
    }

}
