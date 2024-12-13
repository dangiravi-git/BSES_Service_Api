using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;
using System.Data;

/// <summary>
/// Summary description for Z_BAPI_IVRS
/// </summary>
public class Z_BAPI_IVRS
{
	public Z_BAPI_IVRS()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable makeSAPErrorTable(DataTable orgBAPIRET2TABLE)
    {
        DataTable ErrorDataTable = makeErrorTable();
        if (orgBAPIRET2TABLE.Rows.Count > 0)
        {
            for (int intRowCnt = 0; intRowCnt < orgBAPIRET2TABLE.Rows.Count; intRowCnt++)
            {
                pushDataInSAPErrorTable(ErrorDataTable, orgBAPIRET2TABLE.Rows[intRowCnt][0].ToString());
            }

        }

        return ErrorDataTable;

    }  

    public DataTable makeErrorTable()
    {
        DataTable dtSAPErrorTable = new DataTable("SAP_ERROR_TABLE");
        DataRow dr = dtSAPErrorTable.NewRow();
        DataColumn dtCol1 = new DataColumn("DATA", System.Type.GetType("System.String"));
        dtSAPErrorTable.Columns.Add(dtCol1);
        return dtSAPErrorTable;
    }

    public void pushDataInSAPErrorTable(DataTable dt, string DATA)
    {
        DataRow dr = dt.NewRow();
        dr["DATA"] = DATA;
        dt.Rows.Add(dr);
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

    public DataTable makeOutputDataTable()
    {

        DataTable dtOPData = new DataTable("BILL_DETAILS_TABLE");

        DataColumn dtCol1 = new DataColumn("CURR_BILL_AMNT", System.Type.GetType("System.Decimal"));
        dtOPData.Columns.Add(dtCol1);

        DataColumn dtCol2 = new DataColumn("CURR_DUE_DATE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol2);

        DataColumn dtCol3 = new DataColumn("PREV_BILL_AMNT", System.Type.GetType("System.Decimal"));
        dtOPData.Columns.Add(dtCol3);

        DataColumn dtCol4 = new DataColumn("PREV_DUE_DATE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol4);

        DataColumn dtCol5 = new DataColumn("LAST_PAID_AMNT", System.Type.GetType("System.Decimal"));
        dtOPData.Columns.Add(dtCol5);

        DataColumn dtCol6 = new DataColumn("LAST_PAID_DATE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol6);

        return dtOPData;
    }

    public void pushOutputDataInDataTable(DataTable dt, decimal CURR_BILL_AMNT, string CURR_DUE_DATE, decimal PREV_BILL_AMNT, string PREV_DUE_DATE, decimal LAST_PAID_AMNT, string LAST_PAID_DATE)
    {
        DataRow dr = dt.NewRow();
        dr["CURR_BILL_AMNT"] = CURR_BILL_AMNT;
        dr["CURR_DUE_DATE"] = CURR_DUE_DATE;
        dr["PREV_BILL_AMNT"] = PREV_BILL_AMNT;
        dr["PREV_DUE_DATE"] = PREV_DUE_DATE;
        dr["LAST_PAID_AMNT"] = LAST_PAID_AMNT;
        dr["LAST_PAID_DATE"] = LAST_PAID_DATE;
        dt.Rows.Add(dr);
    }

    public DataTable populateStructureWithData(DataTable originalStructuredTable, DataTable rawDataTable)
    {
        DataTable dtOutPut = originalStructuredTable;

        int rowCnt = 0, colCnt = 0;
        rowCnt = rawDataTable.Rows.Count;
        colCnt = rawDataTable.Columns.Count;

        if (rowCnt > 0)
        {
            for (int mRow = 0; mRow < rowCnt; mRow++)
            {
                DataRow dr = dtOutPut.NewRow();

                for (int mCol = 0; mCol < colCnt; mCol++)
                {
                    dr[mCol] = rawDataTable.Rows[mRow][mCol].ToString();
                }
                dtOutPut.Rows.Add(dr);
            }
        }
        return dtOutPut;
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