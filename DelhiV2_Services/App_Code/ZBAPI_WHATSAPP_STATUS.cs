﻿using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ZBAPI_WHATSAPP_STATUS
/// </summary>
public class ZBAPI_WHATSAPP_STATUS
{
    public ZBAPI_WHATSAPP_STATUS()
    {
        //
        // TODO: Add constructor logic here
        //
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
}