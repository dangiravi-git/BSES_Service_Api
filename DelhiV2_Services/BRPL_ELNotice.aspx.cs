using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class BRPL_ELNotice : System.Web.UI.Page
{
    clsBapiCall obj = new clsBapiCall();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["CA_NO"] != null)
        {
            GetBill_PdfView("BYPL", Request.QueryString["CA_NO"].ToString());
        }
    }

    private void GetBill_PdfView(string Company, string CA_NUMBER)
    {

        if (GetLog_FlagData(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER, "ZBAPI_ELNOTICE_WHATSAPP").ToString().Trim() == "N")
        {
            string _sFileName = CA_NUMBER + ".pdf";

            DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
            DataSet ds = obj1.ZBAPI_ELNOTICE_WHATSAPP(Company, CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER);

            //DataSet ds = obj1.ZBAPI_WHATSAPP_STATUS("20230120", "BRPL", "A");

            //DelhiWSV2Develop.WebService obj2 = new DelhiWSV2Develop.WebService();
            //DataSet ds = obj2.ZBAPI_DUNNING_NOTICE_WHATSAPP(Company, CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER);
            string str = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                DirectoryInfo _DirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\PDF\\" + DateTime.Now.ToString("yyyyMMdd"));
                if (_DirInfo.Exists == false)
                    _DirInfo.Create();

                using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\PDF\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + _sFileName))
                {
                    Insert_Service_Log(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER, "ZBAPI_ELNOTICE_WHATSAPP");

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        str += ds.Tables[0].Rows[i][0].ToString().Trim() + Environment.NewLine;
                        sw.WriteLine(ds.Tables[0].Rows[i][0].ToString().Trim());
                    }
                    sw.Close();
                }

                string strResponse = RedirectSAP(_sFileName);
                if (strResponse != "" && strResponse != null)
                {

                }
            }
            else
            {
                lblMessage.Text = "No BRPL Notice found";
            }
        }
        else
        {
            lblMessage.Text = "Dear Consumer, the said document already shared. May please refer our previous conversation with you. In case of any further assistance or query, call us on 19123 (Toll-Free). Thank you";
        }
    }
    public string RedirectSAP(string FileName)
    {
        string strRedirect = string.Empty;
        try
        {

            PDfifram.Visible = true;

            strRedirect = "PDF/" + DateTime.Now.ToString("yyyyMMdd") + "\\" + FileName;
            PDfifram.Attributes["src"] = strRedirect;
            PDfifram.Attributes["scrolling"] = "yes";
            PDfifram.Attributes["frameborder"] = "1";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //WriteIntoFile(ex.Message.Trim() + " -> Exception in method RedirectSAP");
        }
        return strRedirect;
    }
    public void WriteIntoFile(string _smsg)
    {
        using (StreamWriter sw = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(@"~\App_Data\ApplicationLog" + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".txt")))
        {
            sw.WriteLine(_smsg);
            sw.Close();
        }
    }


    private void Insert_Service_Log(string _sCA_No, string _sFunctionBPI)
    {
        string _sSql = string.Empty;
        _sSql = "INSERT INTO MOBAPP.NOTICE_LOG_DATA(CA_NUMBER,FUNCTION_NAME)VALUES('" + _sCA_No + "','" + _sFunctionBPI + "')  ";

        dmlsinglequery(_sSql);
    }
    public bool dmlsinglequery(string sql)
    {
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbConnection ocon = new OleDbConnection(objNDS.con());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }

            dbcommand = new OleDbCommand(sql, ocon);
            //dbcommand.Transaction = dbtrans;
            dbcommand.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
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
    private string GetLog_FlagData(string _sCANo, string _FunBAPI)
    {
        DataTable _dt = new DataTable();
        string _sSql = " select CA_NUMBER FROM MOBAPP.NOTICE_LOG_DATA WHERE CA_NUMBER='" + _sCANo + "' AND FUNCTION_NAME='" + _FunBAPI + "' ";
        _dt = dmlgetquery(_sSql);
        if (_dt.Rows.Count > 0)
            return "Y";
        else
            return "N";
    }
    public DataTable dmlgetquery(string sql)
    {
        DataTable dt = new DataTable();
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbDataAdapter da;

        OleDbConnection ocon = new OleDbConnection(objNDS.con());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            dbcommand = new OleDbCommand();
            dbcommand.Connection = ocon;
            da = new OleDbDataAdapter();
            da.SelectCommand = dbcommand;
            dt = null;
            dt = new DataTable();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandText = sql;
            //dbcommand.Transaction = dbtrans;
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ex.ToString();
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

    protected void btnPDFShow_Click(object sender, EventArgs e)
    {
        GetBill_PdfView("BYPL", TextBox2.Text);
    }
}