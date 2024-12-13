using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ZBAPI_MCR_DOC_NUM
/// </summary>
public class ZBAPI_MCR_DOC_NUM
{
    public ZBAPI_MCR_DOC_NUM()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable makeReturnSystem()
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

    public DataTable makeOrderNumber()
    {
        DataTable dt = new DataTable("OrderNumber");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "ORDER_NUMBER";
        dt.Columns.Add(dcol);

        return dt;
    }

    public DataTable makeIT_Output()
    {
        DataTable dt = new DataTable("IT_OUTPUT");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "OBJECT_ID";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "ARC_DOC_ID";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "AR_DATE";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "ROOT";
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
}