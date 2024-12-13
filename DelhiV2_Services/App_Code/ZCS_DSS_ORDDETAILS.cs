using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ZCS_DSS_ORDDETAILS
/// </summary>
public class ZCS_DSS_ORDDETAILS
{
    public ZCS_DSS_ORDDETAILS()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable makeCUST_NAMETable(string TableName)
    {
        DataTable dt = new DataTable(TableName);
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = TableName;
        dt.Columns.Add(dcol);

        return dt;
    }

    public DataTable makeRETURN_MSGTable()
    {
        DataTable dt = new DataTable("RETURN_MSG");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "RETURN_MSG";
        dt.Columns.Add(dcol);

        return dt;
    }


    public DataTable makeMessageTextTable()
    {
        DataTable dtMessage = new DataTable("messageTable");
        DataRow dr = dtMessage.NewRow();
        DataColumn dtCol1 = new DataColumn("messageCode", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("messageText", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol2);
        return dtMessage;
    }

    public void pushMessageTextInDataTable(DataTable dt, string messageCode, string messageToPush)
    {
        DataRow dr = dt.NewRow();
        dr["messageCode"] = messageCode;
        dr["messageText"] = messageToPush;
        dt.Rows.Add(dr);
    }

    public void bind_CUST_NAMETable(DataTable dt, string dat1)
    {
        DataRow dr = dt.NewRow();
        dr[0] = dat1;
        dt.Rows.Add(dr);
    }


    public DataTable make_Table_ORDDETAIL()
    {
        DataTable dt = new DataTable("ORDDETAIL");
        DataColumn dcol0 = new DataColumn();
        dcol0.DataType = System.Type.GetType("System.String");
        dcol0.ColumnName = "AUFNR";
        dt.Columns.Add(dcol0);

        DataColumn dcol1 = new DataColumn();
        dcol1.DataType = System.Type.GetType("System.String");
        dcol1.ColumnName = "ZZ_VKONT";
        dt.Columns.Add(dcol1);

        DataColumn dcol2 = new DataColumn();
        dcol2.DataType = System.Type.GetType("System.String");
        dcol2.ColumnName = "KUNUM";
        dt.Columns.Add(dcol2);

        DataColumn dcol3 = new DataColumn();
        dcol3.DataType = System.Type.GetType("System.String");
        dcol3.ColumnName = "ILART";
        dt.Columns.Add(dcol3);

        DataColumn dcol4 = new DataColumn();
        dcol4.DataType = System.Type.GetType("System.String");
        dcol4.ColumnName = "STATUS_TXT04";
        dt.Columns.Add(dcol4);

        DataColumn dcol5 = new DataColumn();
        dcol5.DataType = System.Type.GetType("System.String");
        dcol5.ColumnName = "STATUS_TXT30";
        dt.Columns.Add(dcol5);

        DataColumn dcol6 = new DataColumn();
        dcol6.DataType = System.Type.GetType("System.String");
        dcol6.ColumnName = "VAPLZ";
        dt.Columns.Add(dcol6);

        DataColumn dcol7 = new DataColumn();
        dcol7.DataType = System.Type.GetType("System.String");
        dcol7.ColumnName = "ZZ_CONNTYPE";
        dt.Columns.Add(dcol7);

        DataColumn dcol8 = new DataColumn();
        dcol8.DataType = System.Type.GetType("System.String");
        dcol8.ColumnName = "ANLZU";
        dt.Columns.Add(dcol8);

        DataColumn dcol9 = new DataColumn();
        dcol9.DataType = System.Type.GetType("System.String");
        dcol9.ColumnName = "FECOD";
        dt.Columns.Add(dcol9);

        DataColumn dcol10 = new DataColumn();
        dcol10.DataType = System.Type.GetType("System.String");
        dcol10.ColumnName = "ZZ_RLOAD";
        dt.Columns.Add(dcol10);

        DataColumn dcol11 = new DataColumn();
        dcol11.DataType = System.Type.GetType("System.String");
        dcol11.ColumnName = "WERT1";
        dt.Columns.Add(dcol11);

        DataColumn dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "KURZTEXT";
        dt.Columns.Add(dcol);

        DataColumn dcol12 = new DataColumn();
        dcol12.DataType = System.Type.GetType("System.String");
        dcol12.ColumnName = "MATXT";
        dt.Columns.Add(dcol12);

        DataColumn dcol13 = new DataColumn();
        dcol13.DataType = System.Type.GetType("System.String");
        dcol13.ColumnName = "GSTRS";
        dt.Columns.Add(dcol13);

        DataColumn dcol14 = new DataColumn();
        dcol14.DataType = System.Type.GetType("System.String");
        dcol14.ColumnName = "GSUZS";
        dt.Columns.Add(dcol14);

        DataColumn dcol15 = new DataColumn();
        dcol15.DataType = System.Type.GetType("System.String");
        dcol15.ColumnName = "APP_DATE";
        dt.Columns.Add(dcol15);

        DataColumn dcol16 = new DataColumn();
        dcol16.DataType = System.Type.GetType("System.String");
        dcol16.ColumnName = "APP_TIME_ST";
        dt.Columns.Add(dcol16);

        return dt;
    }

    public DataTable make_Table_ZPARTNER_ADDRESS()
    {
        DataTable dt = new DataTable("ZPARTNER_ADDRESS");
        DataColumn dcol0 = new DataColumn();
        dcol0.DataType = System.Type.GetType("System.String");
        dcol0.ColumnName = "HOUSE_NUM1";
        dt.Columns.Add(dcol0);

        DataColumn dcol1 = new DataColumn();
        dcol1.DataType = System.Type.GetType("System.String");
        dcol1.ColumnName = "STREET";
        dt.Columns.Add(dcol1);

        DataColumn dcol2 = new DataColumn();
        dcol2.DataType = System.Type.GetType("System.String");
        dcol2.ColumnName = "STR_SUPPL1";
        dt.Columns.Add(dcol2);

        DataColumn dcol3 = new DataColumn();
        dcol3.DataType = System.Type.GetType("System.String");
        dcol3.ColumnName = "STR_SUPPL2";
        dt.Columns.Add(dcol3);

        DataColumn dcol4 = new DataColumn();
        dcol4.DataType = System.Type.GetType("System.String");
        dcol4.ColumnName = "STR_SUPPL3";
        dt.Columns.Add(dcol4);

        DataColumn dcol5 = new DataColumn();
        dcol5.DataType = System.Type.GetType("System.String");
        dcol5.ColumnName = "CITY1";
        dt.Columns.Add(dcol5);

        DataColumn dcol6 = new DataColumn();
        dcol6.DataType = System.Type.GetType("System.String");
        dcol6.ColumnName = "POST_CODE1";
        dt.Columns.Add(dcol6);

        DataColumn dcol7 = new DataColumn();
        dcol7.DataType = System.Type.GetType("System.String");
        dcol7.ColumnName = "TEL_NUMBER";
        dt.Columns.Add(dcol7);
        
        return dt;
    }

    public DataTable make_Table_ZORDER_DETAIL()
    {
        DataTable dt = new DataTable("ZORDER_DETAIL");
        DataColumn dcol0 = new DataColumn();
        dcol0.DataType = System.Type.GetType("System.String");
        dcol0.ColumnName = "TITLE_KEY1";
        dt.Columns.Add(dcol0);

        DataColumn dcol1 = new DataColumn();
        dcol1.DataType = System.Type.GetType("System.String");
        dcol1.ColumnName = "FIRSTNAME1";
        dt.Columns.Add(dcol1);

        DataColumn dcol2 = new DataColumn();
        dcol2.DataType = System.Type.GetType("System.String");
        dcol2.ColumnName = "MIDDLENAME1";
        dt.Columns.Add(dcol2);

        DataColumn dcol3 = new DataColumn();
        dcol3.DataType = System.Type.GetType("System.String");
        dcol3.ColumnName = "LASTNAME1";
        dt.Columns.Add(dcol3);

        DataColumn dcol4 = new DataColumn();
        dcol4.DataType = System.Type.GetType("System.String");
        dcol4.ColumnName = "FATHERSNAME1";
        dt.Columns.Add(dcol4);

        DataColumn dcol5 = new DataColumn();
        dcol5.DataType = System.Type.GetType("System.String");
        dcol5.ColumnName = "HOUSE_NUM1A";
        dt.Columns.Add(dcol5);

        DataColumn dcol6 = new DataColumn();
        dcol6.DataType = System.Type.GetType("System.String");
        dcol6.ColumnName = "BUILDING1";
        dt.Columns.Add(dcol6);

        DataColumn dcol7 = new DataColumn();
        dcol7.DataType = System.Type.GetType("System.String");
        dcol7.ColumnName = "STR_SUPPL1A";
        dt.Columns.Add(dcol7);

        DataColumn dcol8 = new DataColumn();
        dcol8.DataType = System.Type.GetType("System.String");
        dcol8.ColumnName = "STR_SUPPL2A";
        dt.Columns.Add(dcol8);

        DataColumn dcol9 = new DataColumn();
        dcol9.DataType = System.Type.GetType("System.String");
        dcol9.ColumnName = "STR_SUPPL3A";
        dt.Columns.Add(dcol9);

        DataColumn dcol10 = new DataColumn();
        dcol10.DataType = System.Type.GetType("System.String");
        dcol10.ColumnName = "POSTL_COD1A";
        dt.Columns.Add(dcol10);

        DataColumn dcol11 = new DataColumn();
        dcol11.DataType = System.Type.GetType("System.String");
        dcol11.ColumnName = "CITY1";
        dt.Columns.Add(dcol11);

        DataColumn dcol12 = new DataColumn();
        dcol12.DataType = System.Type.GetType("System.String");
        dcol12.ColumnName = "MOBILE1";
        dt.Columns.Add(dcol12);

        return dt;
    }

    public void bind_Table_ORDDETAIL(DataTable dt, string dat0, string dat1, string dat2, string dat3, string dat4, string dat5, string dat6, string dat7, 
        string dat8, string dat9, string dat10, string dat11, string dat12, string dat13, string dat14, string dat15, string dat16)
    {
        DataRow dr = dt.NewRow();
        dr[0] = dat0;
        dr[1] = dat1;
        dr[2] = dat2;
        dr[3] = dat3;
        dr[4] = dat4;
        dr[5] = dat5;
        dr[6] = dat6;
        dr[7] = dat7;
        dr[8] = dat8;
        dr[9] = dat9;
        dr[10] = dat10;
        dr[11] = dat11;
        dr[12] = dat12;
        dr[13] = dat13;
        dr[14] = dat14;
        dr[15] = dat15;
        dr[16] = dat16;
        dt.Rows.Add(dr);
    }

    public void bind_Table_ZPARTNER_ADDRESS(DataTable dt, string dat0, string dat1, string dat2, string dat3, string dat4, string dat5, string dat6, string dat7)
    {
        DataRow dr = dt.NewRow();
        dr[0] = dat0;
        dr[1] = dat1;
        dr[2] = dat2;
        dr[3] = dat3;
        dr[4] = dat4;
        dr[5] = dat5;
        dr[6] = dat6;
        dr[7] = dat7;
        dt.Rows.Add(dr);
    }

    public void bind_Table_ZORDER_DETAIL(DataTable dt, string dat0, string dat1, string dat2, string dat3, string dat4, string dat5, string dat6, string dat7,
         string dat8, string dat9, string dat10, string dat11, string dat12)
    {
        DataRow dr = dt.NewRow();
        dr[0] = dat0;
        dr[1] = dat1;
        dr[2] = dat2;
        dr[3] = dat3;
        dr[4] = dat4;
        dr[5] = dat5;
        dr[6] = dat6;
        dr[7] = dat7;
        dr[8] = dat8;
        dr[9] = dat9;
        dr[10] = dat10;
        dr[11] = dat11;
        dr[12] = dat12;
        dt.Rows.Add(dr);
    }



}