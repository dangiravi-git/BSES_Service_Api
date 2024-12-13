using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ZBAPI_ELNOTICE_WHATSAPP
/// </summary>
public class ZBAPI_ELNOTICE_WHATSAPP
{
    public ZBAPI_ELNOTICE_WHATSAPP()
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

    public DataTable CreateReturnTable()
    {
        DataTable dtOPData = new DataTable("OutputTable");
        DataRow dr = dtOPData.NewRow();
        DataColumn dtCol1 = new DataColumn("Type", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("Id", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("Number", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("Message", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol4);
        DataColumn dtCol5 = new DataColumn("Log_No", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol5);
        DataColumn dtCol6 = new DataColumn("Log_Msg_No", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol6);
        DataColumn dtCol7 = new DataColumn("Message_V1", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol7);
        DataColumn dtCol8 = new DataColumn("Message_V2", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol8);
        DataColumn dtCol9 = new DataColumn("Message_V3", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol9);
        DataColumn dtCol10 = new DataColumn("Message_V4", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol10);
        DataColumn dtCol11 = new DataColumn("Parameter", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol11);
        DataColumn dtCol12 = new DataColumn("Row", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol12);
        DataColumn dtCol13 = new DataColumn("Field", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol13);
        DataColumn dtCol14 = new DataColumn("System", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol14);

        return dtOPData;
    }

    public void binReturnTable(DataTable _dtTable, string strType, string strId, string strNumber, string strMessage, string strLog_No, string strLog_Msg_No,
                                         string strMsg1, string strMsg2, string strMsg3, string strMsg4, string strParameter, string strRow, string strField,
                                         string strSystem)
    {
        DataRow dr1 = _dtTable.NewRow();
        dr1["Type"] = strType;
        dr1["Id"] = strId;
        dr1["Number"] = strNumber;
        dr1["Message"] = strMessage;
        dr1["Log_No"] = strLog_No;
        dr1["Log_Msg_No"] = strLog_Msg_No;
        dr1["Message_V1"] = strMsg1;
        dr1["Message_V2"] = strMsg2;
        dr1["Message_V3"] = strMsg3;
        dr1["Message_V4"] = strMsg4;
        dr1["Parameter"] = strParameter;
        dr1["Row"] = strRow;
        dr1["Field"] = strField;
        dr1["System"] = strSystem;
        _dtTable.Rows.Add(dr1);
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