using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

public partial class IVRSCallResponseRCV : System.Web.UI.Page
{
    
    OleDbCommand dbcommand;
    OleDbDataAdapter da;
    OleDbTransaction dbtrans;

    //static OleDbConnection ocon = new OleDbConnection(NDS.con());

    protected void Page_Load(object sender, EventArgs e)
    {
        //string SQL;
        OleDbConnection cn;
        string SqlurlUpdate;
        bool flgUrl;
        
        DbFunction objdbfun = new DbFunction();
        //dmlsinglequery
        if (!Page.IsPostBack)
        {
            try
            {
                
                //cn = NDS.con();
                



                string CID, Dest, Status, Error_Description, Error_code, Call_Duration, Stime;
                CID = Request.QueryString["CID"];
                Dest = Request.QueryString["Dest"];
                Status = Request.QueryString["Status"];
                Error_Description = Request.QueryString["Error_Description"];
                Error_code = Request.QueryString["Error_code"];
                Call_Duration = Request.QueryString["Call_Duration"];
                Stime = Request.QueryString["Stime"];

                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                SqlurlUpdate = "insert into IVRS_CALL_RESPONSE_DATA_LogURL(CID, URL)values('" + CID + "','" + url + "')";
                flgUrl = dmlsinglequerylog(SqlurlUpdate);

                bool flg;

               // string retSQL = "SELECT APIRESPONSE FROM DN_ISU_ORDER_MNGR where APIRESPONSE='" + CID + "'";
                //DataTable dtretSQL = new DataTable();
                //dtretSQL = objdbfun.dmlgetquery(retSQL);
                //cn.Open();
                //OleDbDataAdapter odaretSQL = new OleDbDataAdapter(retSQL, cn);
                //DataTable dtretSQL = new DataTable();
                //odaretSQL.Fill(dtretSQL);
                //cn.Close();
                //if (dtretSQL.Rows.Count > 0)
                //{
                    //cn.Open();
                if (String.IsNullOrEmpty(CID))
                {
                    lblmsg.Text = "CID parameter is null in the input string, API failed to insert the record";
                    return;
                }
                else if (String.IsNullOrEmpty(Dest))
                {
                    lblmsg.Text = "Dest parameter is null in the input string, API failed to insert the record";
                    return;
                }
                else if (String.IsNullOrEmpty(Status))
                {
                    lblmsg.Text = "Status parameter is null in the input string, API failed to insert the record";
                    return;
                }
                else if (String.IsNullOrEmpty(Error_Description))
                {
                    lblmsg.Text = "Error_Description parameter is null in the input string, API failed to insert the record";
                    return;
                }
                else if (String.IsNullOrEmpty(Error_code))
                {
                    lblmsg.Text = "Error_code parameter is null in the input string, API failed to insert the record";
                    return;
                }
                else if (String.IsNullOrEmpty(Call_Duration))
                {
                    lblmsg.Text = "Call_Duration parameter is null in the input string, API failed to insert the record";
                    return;
                }
                else if (String.IsNullOrEmpty(Stime))
                {
                    lblmsg.Text = "Stime parameter is null in the input string, API failed to insert the record";
                    return;
                }
                else
                {
                    string SQL_INSERT = "INSERT INTO IVRS_CALL_RESPONSE_DATA(CID,Dest,Status,Error_Description,Error_code,Call_Duration,Stime ) VALUES(";
                    SQL_INSERT += "'" + CID + "','" + Dest + "','" + Status + "','" + Error_Description + "','" + Error_code + "'," + Call_Duration + ",TO_DATE('" + Stime + "','yyyy/MM/dd HH24:MI:SS')" + ")";
                    flg = dmlsinglequerylog(SQL_INSERT);

                    if (flg == true)
                    {
                        lblmsg.Text = "Record successfully inserted.";
                    }
                    else
                    {
                        lblmsg.Text = "Input data is not in the correct format,please correct the data and re-insert";
                    }

                    //OleDbCommand cmd = new OleDbCommand(SQL_INSERT, cn);
                    //int res = cmd.ExecuteNonQuery();
                    //cn.Close();

                    //if (res > 0)
                    //{
                    //    lblmsg.Text = "Successfully Updated.";
                    //}
                    //else
                    //{
                    //    lblmsg.Text = "No record Updated";
                    //}

                    //}
                    // else
                    // {
                    //lblmsg.Text = "No matching data found against the CID : " + CID;

                    // }
                }

            }
            catch (Exception Ex)
            {
                lblmsg.Text = Ex.Message.ToString();

            }
            
        }
        
    }
    public bool dmlsinglequerylog(string sql)
    {
        string StrExce;
        NDS nds = new NDS();
        OleDbConnection ocon = new OleDbConnection(nds.con());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand = new OleDbCommand(sql, ocon);
            dbcommand.Transaction = dbtrans;
            dbcommand.ExecuteNonQuery();
           
            return true;
        }
        catch (Exception ex)
        {
            StrExce = "insert into Mobapp.IVRS_CALL_RESPONSE_DATA_LOG(MSG) values('" + ex.ToString() + "')";
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand = new OleDbCommand(StrExce, ocon);
            dbcommand.Transaction = dbtrans;
            dbcommand.ExecuteNonQuery();
            lblmsg.Text = ex.Message.ToString();
           
            //NewClassFile.WriteIntoFile(DateTime.Now.ToString()+ ex.ToString());
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
}
