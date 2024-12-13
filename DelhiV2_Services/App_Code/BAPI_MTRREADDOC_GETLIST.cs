using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SAP.Middleware.Connector;
/// <summary>
/// Summary description for BAPI_MTRREADDOC_GETLIST
/// </summary>
public class BAPI_MTRREADDOC_GETLIST
{
    public BAPI_MTRREADDOC_GETLIST()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable Converttable(IRfcTable rftable)
    {
        DataTable dt = new DataTable();

        return dt;
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
    public DataTable makeFlagsTable()
    {
        DataTable dtFlags = new DataTable("outputFlagsTable");
        DataRow dr = dtFlags.NewRow();
        DataColumn dtCol1 = new DataColumn("E_O_CA", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("E_O_INVOICE_NO", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("E_O_POSTING_DT", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("E_O_DUE_DATE", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol4);
        DataColumn dtCol5 = new DataColumn("E_O_BILL_STS", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol5);
        DataColumn dtCol6 = new DataColumn("E_O_CURR_BILL_AMT", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol6);
        DataColumn dtCol7 = new DataColumn("E_O_ADJUSTMENT", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol7);
        DataColumn dtCol8 = new DataColumn("E_O_BALANCE_AMT", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol8);
        DataColumn dtCol9 = new DataColumn("E_O_LAST_BILL_AMT", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol9);
        DataColumn dtCol10 = new DataColumn("E_O_LAST_PYMT_AMT", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol10);
        DataColumn dtCol11 = new DataColumn("E_O_LAST_PYMT_DATE", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol11);
        DataColumn dtCol12 = new DataColumn("E_O_CONNECTION_TYPE", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol12);
        DataColumn dtCol13 = new DataColumn("E_O_BUKRS", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol13);
        DataColumn dtCol14 = new DataColumn("E_O_TARIFTYPE", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol14);

        return dtFlags;
    }
    public void pushMessageTextInDataTable(DataTable dt, string messageCode, string messageToPush)
    {
        DataRow dr = dt.NewRow();
        dr["messageCode"] = messageCode;
        dr["messageText"] = messageToPush;
        dt.Rows.Add(dr);

    }
    public void pushOutputDataInDataTable(DataTable dt, string strCANumber, string strInvoiceno, string strPostingdt, string strDuedate, string strBilllst,
                                          string strCurrbillamt, string strAdjustment, string strBalanceamt, string strLastbillamt, string strLastpymamt,
                                          string strLastpymdate, string strConnectiontype, string strBukrs, string strTariftyp)
    {
        DataRow dr = dt.NewRow();
        dr["E_O_CA"] = strCANumber;
        dr["E_O_INVOICE_NO"] = strInvoiceno;
        dr["E_O_POSTING_DT"] = strPostingdt;
        dr["E_O_DUE_DATE"] = strDuedate;
        dr["E_O_BILL_STS"] = strBilllst;
        dr["E_O_CURR_BILL_AMT"] = strCurrbillamt;
        dr["E_O_ADJUSTMENT"] = strAdjustment;
        dr["E_O_BALANCE_AMT"] = strBalanceamt;
        dr["E_O_LAST_BILL_AMT"] = strLastbillamt;
        dr["E_O_LAST_PYMT_AMT"] = strLastpymamt;
        dr["E_O_LAST_PYMT_DATE"] = strLastpymdate;
        dr["E_O_CONNECTION_TYPE"] = strConnectiontype;
        dr["E_O_BUKRS"] = strBukrs;
        dr["E_O_TARIFTYPE"] = strTariftyp;
        dt.Rows.Add(dr);
    }

    public DataTable CreateOutputDataTable(string strType, string strId, string strNumber, string strMessage, string strLog_No, string strLog_Msg_No,
                                         string strMsg1, string strMsg2, string strMsg3, string strMsg4, string strParameter, string strRow, string strField,
                                         string strSystem)
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

        DataRow dr1 = dtOPData.NewRow();
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
        dtOPData.Rows.Add(dr1);
        return dtOPData;
    }
}