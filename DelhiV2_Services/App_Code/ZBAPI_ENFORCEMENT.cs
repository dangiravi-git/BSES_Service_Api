using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;
using System.Data;

/// <summary>
/// Summary description for ZBAPI_ENFORCEMENT
/// </summary>
public class ZBAPI_ENFORCEMENT
{
	public ZBAPI_ENFORCEMENT()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable makeFlagsTable()
    {
        DataTable dtFlags = new DataTable("outputFlagsTable");
        DataRow dr = dtFlags.NewRow();
        DataColumn dtCol0 = new DataColumn("E_Flag_Ap", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol0);
        DataColumn dtCol1 = new DataColumn("E_Flag_Bp", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("E_Flag_So", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol2);
        DataColumn dtCol5 = new DataColumn("E_Flag_Us", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol5);
        DataColumn dtCol3 = new DataColumn("E_New_Partner", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("E_Service_Order", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol4);
        return dtFlags;
    }

    //Function to populate the above table[EnforcementDetailsTable] with data

    public DataTable transferEnforcementData(DataTable originalTable)
    {
        DataTable dtOutPut = makeEnforcementDetailsTable();
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

    public DataTable makeEnforcementDetailsTable()
    {
        DataTable dtEnfDetails = new DataTable("enforcementDetailsTable");
        DataRow dr = dtEnfDetails.NewRow();

        DataColumn dtCol1 = new DataColumn("EBS_NO", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("INVOICE_NO", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("CONSUMER_NO", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("CONTRACT_NO", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol4);
        DataColumn dtCol5 = new DataColumn("DATE_OF_INVOICE", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol5);
        DataColumn dtCol6 = new DataColumn("CYCLE_NO", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol6);
        DataColumn dtCol7 = new DataColumn("BOOK_NO", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol7);
        DataColumn dtCol8 = new DataColumn("FIRSTNAME", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol8);
        DataColumn dtCol9 = new DataColumn("LASTNAME", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol9);
        DataColumn dtCol10 = new DataColumn("MIDDLE_NAME", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol10);
        DataColumn dtCol11 = new DataColumn("FATHER_NAME", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol11);
        DataColumn dtCol12 = new DataColumn("HOUSE_NO", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol12);
        DataColumn dtCol13 = new DataColumn("STREET", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol13);
        DataColumn dtCol14 = new DataColumn("STR_SUPPL1", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol14);
        DataColumn dtCol15 = new DataColumn("STR_SUPPL2", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol15);
        DataColumn dtCol16 = new DataColumn("STR_SUPPL3", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol16);
        DataColumn dtCol17 = new DataColumn("CITY", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol17);
        DataColumn dtCol18 = new DataColumn("POSTL_COD1", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol18);
        DataColumn dtCol19 = new DataColumn("UNIT_DESCR", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol19);
        DataColumn dtCol20 = new DataColumn("CIRCLE_DESCR", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol20);
        DataColumn dtCol21 = new DataColumn("BILL_MONTH", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol21);
        DataColumn dtCol22 = new DataColumn("TOT_UNITS_BILLED", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol22);
        DataColumn dtCol23 = new DataColumn("BILL_BASIS", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol23);
        DataColumn dtCol24 = new DataColumn("SANC_LOAD", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol24);
        DataColumn dtCol25 = new DataColumn("RATE_CATEGORY", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol25);
        DataColumn dtCol26 = new DataColumn("METER_TYPE", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol26);
        DataColumn dtCol27 = new DataColumn("TOT_ENRGY_CHRG", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol27);
        DataColumn dtCol28 = new DataColumn("TOT_FIX_CHRG", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol28);
        DataColumn dtCol29 = new DataColumn("ELECT_TAX", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol29);
        DataColumn dtCol30 = new DataColumn("RATE_SUBSIDY", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol30);
        DataColumn dtCol31 = new DataColumn("SPL_SUBSIDY", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol31);
        DataColumn dtCol32 = new DataColumn("REFUND", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol32);
        DataColumn dtCol33 = new DataColumn("METER_NO", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol33);
        DataColumn dtCol34 = new DataColumn("MF_FACTOR", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol34);
        DataColumn dtCol35 = new DataColumn("MR_NOTE", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol35);
        DataColumn dtCol36 = new DataColumn("PRE_MR_DATE", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol36);
        DataColumn dtCol37 = new DataColumn("CURR_MR_DATE", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol37);
        DataColumn dtCol38 = new DataColumn("PREV_MR_KWH", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol38);
        DataColumn dtCol39 = new DataColumn("CURR_MR_KWH", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol39);
        DataColumn dtCol40 = new DataColumn("PREV_MR_KVAH", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol40);
        DataColumn dtCol41 = new DataColumn("CURR_MR_KVAH", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol41);
        DataColumn dtCol42 = new DataColumn("BILL_ON_KWH_KVAH", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol42);
        DataColumn dtCol43 = new DataColumn("METER_INSTALL", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol43);
        DataColumn dtCol44 = new DataColumn("DOWNLOAD_FLAG", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol44);
        DataColumn dtCol45 = new DataColumn("NO_OF_DGT", System.Type.GetType("System.String"));
        dtEnfDetails.Columns.Add(dtCol45);

        return dtEnfDetails;

    }


    //Function to populate the above table[EnfMeterChangeTable] with data

    public DataTable transferEnfMeterChangeData(DataTable originalTable)
    {
        DataTable dtOutPut = makeEnfMeterChangeTable();
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


    ////Function to form EnfMeterChangeTable used in ZBAPI_ENFORCEMENT

    public DataTable makeEnfMeterChangeTable()
    {
        DataTable dtMeterChange = new DataTable("enfMeterChangeTable");
        DataRow dr = dtMeterChange.NewRow();

        DataColumn dtCol1 = new DataColumn("EBS_NO", System.Type.GetType("System.String"));
        dtMeterChange.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("INVOICE_NO", System.Type.GetType("System.String"));
        dtMeterChange.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("CONSUMER_NO", System.Type.GetType("System.String"));
        dtMeterChange.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("CONTRACT_NO", System.Type.GetType("System.String"));
        dtMeterChange.Columns.Add(dtCol4);
        DataColumn dtCol5 = new DataColumn("METER_NO", System.Type.GetType("System.String"));
        dtMeterChange.Columns.Add(dtCol5);
        DataColumn dtCol6 = new DataColumn("MTR_CHANGE_DATE", System.Type.GetType("System.String"));
        dtMeterChange.Columns.Add(dtCol6);
        DataColumn dtCol7 = new DataColumn("INT_MR_KWH", System.Type.GetType("System.String"));
        dtMeterChange.Columns.Add(dtCol7);
        DataColumn dtCol8 = new DataColumn("INT_MR_KVAH", System.Type.GetType("System.String"));
        dtMeterChange.Columns.Add(dtCol8);
        DataColumn dtCol9 = new DataColumn("FINAL_MR_KWH", System.Type.GetType("System.String"));
        dtMeterChange.Columns.Add(dtCol9);
        DataColumn dtCol10 = new DataColumn("FINAL_MR_KVAH", System.Type.GetType("System.String"));
        dtMeterChange.Columns.Add(dtCol10);

        return dtMeterChange;

    }

    public void pushFlagsInDataTable(DataTable dt, string E_Flag_Ap, string E_Flag_Bp, string E_Flag_So, string E_Flag_Us, string E_New_Partner, string E_Service_Order)
    {
        DataRow dr = dt.NewRow();
        dr["E_Flag_Ap"] = E_Flag_Ap;
        dr["E_Flag_Bp"] = E_Flag_Bp;
        dr["E_Flag_So"] = E_Flag_So;
        dr["E_Flag_Us"] = E_Flag_Us;
        dr["E_New_Partner"] = E_New_Partner;
        dr["E_Service_Order"] = E_Service_Order;
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

    public DataTable CreateOutputDataTable(string strDoc_No, string strNoptif_No, string strOrderId)
    {
        DataTable dtOPData = new DataTable("OutputTable");
        DataRow dr = dtOPData.NewRow();
        DataColumn dtCol1 = new DataColumn("OrderId", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("Noptif_No", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("Doc_No", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol3);

        DataRow dr1 = dtOPData.NewRow();
        dr1["OrderId"] = strOrderId;
        dr1["Noptif_No"] = strNoptif_No;
        dr1["Doc_No"] = strDoc_No;
        dtOPData.Rows.Add(dr1);

        return dtOPData;
    }

    //Function to make a DataTable with same structure as ZBAPIISUERRTable table of BAPI
    public DataTable makeISUERRTable(DataTable orgZBAPIERRTable)
    {
        DataTable BapiErrorDataTable = formDataTableBapiError();


        if (orgZBAPIERRTable.Rows.Count > 0)
        {
            for (int intRowCnt = 0; intRowCnt < orgZBAPIERRTable.Rows.Count; intRowCnt++)
            {
                addRowToDT_BAPIError(BapiErrorDataTable, orgZBAPIERRTable.Rows[intRowCnt][0].ToString());
            }

        }

        return BapiErrorDataTable;

    }

    public DataTable formDataTableBapiError()
    {
        DataTable dt = new DataTable("SAPDATA_BAPIErrorDataTable");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Message";
        dt.Columns.Add(dcol);

        return dt;
    }

    //Function to add a row to the Table BAPIRET2TABLE
    public void addRowToDT_BAPIError(DataTable dt, string DATA1)
    {
        DataRow dr = dt.NewRow();
        dr["Message"] = DATA1;
        dt.Rows.Add(dr);

    }

    //Function to make a DataTable with same structure as BAPIRET2TABLE table of BAPI
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

    public string GetDateFormate_YYYYMMDD1(string _sDate)
    {
        string _sDateformate = string.Empty;
        string[] DateFormate = _sDate.ToString().Split(':');

        for (int i = 0; i < DateFormate.Length; i++)
        {
            _sDateformate += DateFormate[i];
        }

        return _sDateformate;
    }
}