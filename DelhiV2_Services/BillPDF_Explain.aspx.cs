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

public partial class BillPDF_Explain : System.Web.UI.Page
{
    clsBapiCall obj = new clsBapiCall();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void GetBill_PdfView(string _sCA)
    {
        string _sFileName = _sCA + ".pdf";
        DataSet ds = obj.Get_ZBAPI_BILL_DET(_sCA);
        string str = "";

        if (ds.Tables[0].Rows.Count > 0)
        {
            DirectoryInfo _DirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\PDF\\" + DateTime.Now.ToString("yyyyMMdd"));
            if (_DirInfo.Exists == false)
                _DirInfo.Create();

            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\PDF\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + _sFileName))
            {
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


    protected void btnPDFShow_Click(object sender, EventArgs e)
    {
        GetBill_PdfView(TextBox1.Text);
    }
}