using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;
using System.Data;

/// <summary>
/// Summary description for ZBI_WEBBILL_HIST
/// </summary>
public class ZBI_WEBBILL_HIST
{
	public ZBI_WEBBILL_HIST()
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

    //Function to populate the above table[WebBillHistoryTable] with data
    public DataTable transferWebBillHistoryData(DataTable originalTable)
    {
        DataTable dtOutPut = makeWebBillHistoryTable();
        int rowCnt = 0, colCnt = 0;
        rowCnt = originalTable.Rows.Count;
        colCnt = originalTable.Columns.Count;

        if (rowCnt > 0)
        {
            for (int mRow = 0; mRow < rowCnt; mRow++)
            {
                DataRow dr = dtOutPut.NewRow();

                for (int mCol = 0; mCol < colCnt; mCol++)
                {
                    if ((mCol == 3) || (mCol == 17) || (mCol == 22))
                        dr[mCol] = GetDateFormate_YYYYMMDD(originalTable.Rows[mRow][mCol].ToString());
                    else
                        dr[mCol] = originalTable.Rows[mRow][mCol].ToString();
                }
                dtOutPut.Rows.Add(dr);
            }
        }
        return dtOutPut;
    }

    ////Function to form WebBillHistoryTable used in ZBI_WEBBILL_HIST
    ///
    public DataTable makeWebBillHistoryTable()
    {
        DataTable dtWebBillHistory = new DataTable("webBillHistoryTable");
        DataRow dr = dtWebBillHistory.NewRow();

        DataColumn dtCol1 = new DataColumn("BILL_MONTH", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("INVOICE_NO", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("NET_AMNT", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("DUE_DATE", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol4);
        DataColumn dtCol5 = new DataColumn("LPSC_CURRENT", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol5);
        DataColumn dtCol6 = new DataColumn("CUR_MTH_BILL_AMT", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol6);
        DataColumn dtCol7 = new DataColumn("CUR_MTH_BILL_AMD", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol7);
        DataColumn dtCol8 = new DataColumn("CUR_MTH_BIL_AMDA", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol8);
        DataColumn dtCol9 = new DataColumn("ARR_PAYABLE", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol9);
        DataColumn dtCol10 = new DataColumn("ARR_PAYABLE_AMD", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol10);
        DataColumn dtCol11 = new DataColumn("ARR_PAYABLE_AMDA", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol11);
        DataColumn dtCol12 = new DataColumn("ARR_LPSC", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol12);
        DataColumn dtCol13 = new DataColumn("REFUND", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol13);
        DataColumn dtCol14 = new DataColumn("REFUND_AMNDTAM", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol14);
        DataColumn dtCol15 = new DataColumn("REFUND_AMNDS", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol15);
        DataColumn dtCol16 = new DataColumn("ADJUSTMENT", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol16);
        DataColumn dtCol17 = new DataColumn("PAYMENT_AMOUNT", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol17);
        DataColumn dtCol18 = new DataColumn("PAYMENT_DATE", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol18);
        DataColumn dtCol19 = new DataColumn("TOT_BIL_AMT", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol19);
        DataColumn dtCol20 = new DataColumn("TOT_BIL_AMNDS", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol20);
        DataColumn dtCol21 = new DataColumn("TOT_BIL_AMDTAM", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol21);
        DataColumn dtCol22 = new DataColumn("UNITS", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol22);
        DataColumn dtCol1a = new DataColumn("DATE_OF_INVOICE", System.Type.GetType("System.String"));
        dtWebBillHistory.Columns.Add(dtCol1a);

        return dtWebBillHistory;
    }


    public DataTable makeBAPIRET2TABLE(DataTable orgBAPIRET2TABLE)
    {
        DataTable ErrorDataTable = formDataTableError();
        if (orgBAPIRET2TABLE.Rows.Count > 0)
        {
            for (int intRowCnt = 0; intRowCnt < orgBAPIRET2TABLE.Rows.Count; intRowCnt++)
            {
                addRowToDTError(ErrorDataTable, orgBAPIRET2TABLE.Rows[intRowCnt][0].ToString(),
                                orgBAPIRET2TABLE.Rows[intRowCnt][1].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][2].ToString(),
                                orgBAPIRET2TABLE.Rows[intRowCnt][3].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][4].ToString(),
                                orgBAPIRET2TABLE.Rows[intRowCnt][5].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][6].ToString(),
                                orgBAPIRET2TABLE.Rows[intRowCnt][7].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][8].ToString(),
                                orgBAPIRET2TABLE.Rows[intRowCnt][9].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][10].ToString(),
                                orgBAPIRET2TABLE.Rows[intRowCnt][11].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][12].ToString(),
                                orgBAPIRET2TABLE.Rows[intRowCnt][13].ToString());
            }

        }

        return ErrorDataTable;

    }

    public DataTable formDataTableError()
    {
        DataTable dt = new DataTable("SAPDATA_ErrorDataTable");
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

    public DataTable pushDataInErrorTable(DataTable originalTable)
    {

        DataTable dtOutPut = makeErrorTable();

        int rowCnt = 0, colCnt = 0;
        rowCnt = originalTable.Rows.Count;
        colCnt = originalTable.Columns.Count;

        if (rowCnt > 0)
        {
            for (int mRow = 0; mRow < rowCnt; mRow++)
            {
                DataRow dr = dtOutPut.NewRow();

                for (int mCol = 0; mCol < colCnt; mCol++)
                {
                    dr[mCol] = originalTable.Rows[mRow][mCol].ToString();
                }
                dtOutPut.Rows.Add(dr);
            }
        }
        return dtOutPut;

    }

    public DataTable makeErrorTable()
    {
        DataTable dtErrorTable = new DataTable("ErrorTable");
        DataRow dr = dtErrorTable.NewRow();

        DataColumn dtCol1 = new DataColumn("Data", System.Type.GetType("System.String"));
        dtErrorTable.Columns.Add(dtCol1);

        return dtErrorTable;
    }
    public void pushMessageTextInDataTable(DataTable dt, string messageCode, string messageToPush)
    {
        DataRow dr = dt.NewRow();
        dr["messageCode"] = messageCode;
        dr["messageText"] = messageToPush;
        dt.Rows.Add(dr);

    }

    public void addRowToDTError(DataTable dt, string DATA1, string DATA2, string DATA3, string DATA4, string DATA5, string DATA6, string DATA7, string DATA8, string DATA9, string DATA10, string DATA11, string DATA12, string DATA13, string DATA14)
    {
        DataRow dr = dt.NewRow();
        dr["Type"] = DATA1;
        dr["Id"] = DATA2;
        dr["Number"] = DATA3;
        dr["Message"] = DATA4;
        dr["Log_No"] = DATA5;
        dr["Log_Msg_No"] = DATA6;
        dr["Message_V1"] = DATA7;
        dr["Message_V2"] = DATA8;
        dr["Message_V3"] = DATA9;
        dr["Message_V4"] = DATA10;
        dr["Parameter"] = DATA11;
        dr["Row"] = DATA12;
        dr["Field"] = DATA13;
        dr["System"] = DATA14;
        dt.Rows.Add(dr);

    }

    public string GetDateFormate_YYYYMMDD(string _sDate)
    {
        string _sDateformate = string.Empty;
        string[] DateFormate = _sDate.ToString().Split('-');

        for (int i = 0; i < DateFormate.Length; i++)
        {
            _sDateformate += DateFormate[i];
        }

        return _sDateformate;
    }

}