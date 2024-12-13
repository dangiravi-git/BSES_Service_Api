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

public partial class BRPL_FinalBill : System.Web.UI.Page
{
    clsBapiCall obj = new clsBapiCall();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["CA_NO"] != null)
        {
            //GetFinal_PDF(Request.QueryString["CA_NO"].ToString());
            GetFinalBill_PdfView(Request.QueryString["CA_NO"].ToString());
        }
    }

    private void GetFinalBill_PdfView(string _sCA)
    {
        string _sFileName = _sCA + ".pdf";
        //DataSet ds = obj.Get_ZBAPI_BILL_DET(_sCA);

        DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
        DataSet ds = obj.Get_ZBAPI_ONLINE_BILL_PDF(_sCA.Length == 9 ? "000" + _sCA : _sCA, "");
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

               // Content_Display.InnerHtml = str;
            }

            RedirectSAP_New(_sFileName);

            //string strResponse = RedirectSAP(_sFileName);
            //if (strResponse != "" && strResponse != null)
            //{

            //}
            Blank_Display.Visible = false;
            Content_Display.Visible = true;
        }
        else
        {
            Content_Display.Visible = false;
            Blank_Display.Visible = true;
            lblMessage.Text = "No Record found";
        }
    }

    void RedirectSAP_New(string FileName)
    {

        try
        {
            PDfifram.Visible = true;
            string strRedirect = "PDF/" + DateTime.Now.ToString("yyyyMMdd") + "\\" + FileName;
            PDfifram.Attributes["src"] = strRedirect;
            PDfifram.Attributes["scrolling"] = "yes";
            PDfifram.Attributes["frameborder"] = "1";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    public string RedirectSAP(string FileName)
    {
        string strRedirect = string.Empty;
        try
        {

            PDfifram.Style["display"] = "block";

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
        string CA_NUMBER = TextBox1.Text;
        DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
        DataSet ds = obj1.ZBAPI_CA_DISPLAY_CRM(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER);

        if (ds.Tables[0].Rows[0]["MOVE_OUT_DATE"].ToString() != "9999-12-31")
            lblMessage.Text = "This is already Move Out Case.";
        else
            GetFinalBill_PdfView(TextBox1.Text);
    }

    private void GetFinal_PDF(string _CANo)
    {
        string CA_NUMBER = _CANo;
        DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
        DataSet ds = obj1.ZBAPI_CA_DISPLAY_CRM(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER);

        ////if (ds.Tables[0].Rows[0]["MOVE_OUT_DATE"].ToString() != "9999-12-31")
        ////    lblMessage.Text = "No BRPL Notice found";
        ////else
        //    GetFinalBill_PdfView(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER);

        if (ds.Tables[1].Rows.Count > 0)
        {
            if (ds.Tables[1].Rows[0][0].ToString().Trim() == "Consumer is not Live")
            {
                GetFinalBill_PdfView(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER);
            }
            else
            {
                Content_Display.Visible = false;
                Blank_Display.Visible = true;
                lblMessage.Text = "No Record found";
            }
        }
        else
        {
            Content_Display.Visible = false;
            Blank_Display.Visible = true;
            lblMessage.Text = "No Record found";
        }

    }
}