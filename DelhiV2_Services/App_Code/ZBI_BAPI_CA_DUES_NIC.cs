using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;

/// <summary>
/// Summary description for ZBI_BAPI_CA_DUES_NIC
/// </summary>
public class ZBI_BAPI_CA_DUES_NIC
{
    public ZBI_BAPI_CA_DUES_NIC()
    {
        //
        // TODO: Add constructor logic here
        //
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

    public DataTable formDataTableError()
    {
        DataTable dt = new DataTable("RETURN");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Type";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Id";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Number";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Message";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Log_No";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Log_Msg_No";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Message_V1";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Message_V2";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Message_V3";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Message_V4";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Parameter";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Row";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Field";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "System";
        dt.Columns.Add(dcol);

        return dt;
    }

    public DataTable make_ZERR()
    {
        DataTable dtMessage = new DataTable("ZERR");
        DataRow dr = dtMessage.NewRow();
        DataColumn dtCol1 = new DataColumn("Data", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol1);
        return dtMessage;
    }

    public DataTable make_Result()
    {
        DataTable dtMessage = new DataTable("TX_BILLPRINT");
        DataRow dr = dtMessage.NewRow();
        DataColumn dtCol1 = new DataColumn("INVOICE_NO", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("CONSUMER_NO", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("DATE_OF_INVOICE", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("CYCLE_NO", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol4);
        DataColumn dtCol5 = new DataColumn("FIRSTNAME", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol5);
        DataColumn dtCol6 = new DataColumn("HOUSE_NO", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol6);
        DataColumn dtCol7 = new DataColumn("UNIT_DESCR", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol7);
        DataColumn dtCol8 = new DataColumn("CIRCLE_DESCR", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol8);
        DataColumn dtCol9 = new DataColumn("DUE_DATE", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol9);
        DataColumn dtCol10 = new DataColumn("SANC_LOAD", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol10);
        DataColumn dtCol11 = new DataColumn("CONT_DEMAND", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol11);
        DataColumn dtCol12 = new DataColumn("TARIFF", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol12);
        DataColumn dtCol13 = new DataColumn("RATE_CATEGORY", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol13);
        DataColumn dtCol14 = new DataColumn("COMPANY_CODE", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol14);
        DataColumn dtCol15 = new DataColumn("LASTNAME", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol15);
        DataColumn dtCol16 = new DataColumn("MIDDLE_NAME", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol16);
        DataColumn dtCol17 = new DataColumn("FATHER_NAME", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol17);
        DataColumn dtCol18 = new DataColumn("STREET", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol18);
        DataColumn dtCol19 = new DataColumn("STR_SUPPL1", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol19);
        DataColumn dtCol20 = new DataColumn("STR_SUPPL2", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol20);
        DataColumn dtCol21 = new DataColumn("STR_SUPPL3", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol21);
        DataColumn dtCol22 = new DataColumn("CITY", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol22);
        DataColumn dtCol23 = new DataColumn("POSTL_COD1", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol23);
        DataColumn dtCol24 = new DataColumn("TEL_NO", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol24);
        DataColumn dtCol25 = new DataColumn("EMAIL_ID", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol25);
        DataColumn dtCol26 = new DataColumn("CONNECTION_TYPE", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol26);
        DataColumn dtCol27 = new DataColumn("CONSUMER_STATUS", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol27);
        DataColumn dtCol28 = new DataColumn("TOT_BIL_AMT", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol28);
        DataColumn dtCol29 = new DataColumn("TOT_ENF_AMT", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol29);
        DataColumn dtCol30 = new DataColumn("GROSS_AMT", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol30);
        DataColumn dtCol31 = new DataColumn("PARTNER", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol31);
        DataColumn dtCol32 = new DataColumn("TYPE", System.Type.GetType("System.String"));
        dtMessage.Columns.Add(dtCol32);
        return dtMessage;
    }
    public DataTable converttodotnetatble(IRfcTable rfctable)
    {
        DataTable dt = new DataTable();

        for (int i = 0; i < rfctable.ElementCount; i++)
        {
            RfcElementMetadata metadata = rfctable.GetElementMetadata(i);
            dt.Columns.Add(metadata.Name);
        }

        foreach (IRfcStructure row in rfctable)
        {
            DataRow dr = dt.NewRow();

            for (int i = 0; i < rfctable.ElementCount; i++)
            {
                RfcElementMetadata metadata = rfctable.GetElementMetadata(i);
                if (metadata.DataType == RfcDataType.BCD && metadata.Name == "ABC")
                {
                    dr[i] = row.GetString(metadata.Name);
                }
                else
                    dr[i] = row.GetString(metadata.Name);

            }
            dt.Rows.Add(dr);
        }

        return dt;
    }
    public DataTable convertTX_BILLPRINT(IRfcTable rfctable)
    {
        DataTable dt = new DataTable();

        for (int i = 0; i < rfctable.ElementCount; i++)
        {
            RfcElementMetadata metadata = rfctable.GetElementMetadata(i);
            dt.Columns.Add(metadata.Name);
        }

        foreach (IRfcStructure row in rfctable)
        {
            DataRow dr = dt.NewRow();

            for (int i = 0; i < rfctable.ElementCount; i++)
            {
                RfcElementMetadata metadata = rfctable.GetElementMetadata(i);
                if (metadata.DataType == RfcDataType.BCD && metadata.Name == "ABC")
                {
                    dr[i] = row.GetString(metadata.Name);
                }
                else
                    dr[i] = row.GetString(metadata.Name);

            }
            dt.Rows.Add(dr);
        }

        return dt;
    }
}