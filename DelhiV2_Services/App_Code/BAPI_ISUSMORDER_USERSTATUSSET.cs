using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;
using System.Data;

/// <summary>
/// Summary description for BAPI_ISUSMORDER_USERSTATUSSET
/// </summary>
public class BAPI_ISUSMORDER_USERSTATUSSET
{
    public BAPI_ISUSMORDER_USERSTATUSSET()
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
    public void pushMessageTextInDataTable(DataTable dt, string messageCode, string messageToPush)
    {
        DataRow dr = dt.NewRow();
        dr["messageCode"] = messageCode;
        dr["messageText"] = messageToPush;
        dt.Rows.Add(dr);

    }
    public DataTable CreateOutputDataTable(string strType)
    {
        DataTable dtOPData = new DataTable("OutputTable");
        DataRow dr = dtOPData.NewRow();
        DataColumn dtCol1 = new DataColumn("FLAG", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol1);
        DataRow dr1 = dtOPData.NewRow();
        dr1["FLAG"] = strType;
        dtOPData.Rows.Add(dr1);
        return dtOPData;
    }

    public void pushFlagsInDataTable(DataTable dt, string SUB_ORDER)
    {
        DataRow dr = dt.NewRow();
        dr["ORD_NUM"] = SUB_ORDER;
        dt.Rows.Add(dr);

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

    public DataTable makeFlagsTable()
    {
        DataTable dtFlags = new DataTable("outputFlagsTable");
        DataRow dr = dtFlags.NewRow();
        DataColumn dtCol0 = new DataColumn("ORD_NUM", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol0);
        return dtFlags;
    }

    public DataTable CreateOutputDataTable1(string _DIVISION, string _BP_NUMBER, string _USER_NAME, string _R_C_NAME, string _ADDRESS, string _TELEPHONE_NO,
                     string _ALTERNATE_PAYER, string _DATE_OF_INSPECTION, string _CONN_LOAD, string _SANCT_LOAD, string _ENFORCEMENT_TYPE, string _TARIFF_CATEGORY,
                     string _CASEID, string _CASEID_NUMBER, string _NEW_CA, string _OLD_CA, string _BILL_NO, string _BILL_ISSUE_DATE, string _DUE_DATE, string _TOTAL_CHARGE,
                     string _CREDIT_ASSED_PERIOD, string _SUNDRY, string _NET_CHARGE, string _LPSC_AMOUNT, string _METER_COST, string _METER_TEST_CHARGE,
                     string _LOCK_REASON, string _SETTLED_DATE, string _SETTLED_AMT_REMARK, string _CHEQUE_DISHONORED, string _NET_DUE)
    {
        DataTable dtOPData = new DataTable("OutputTable");
        DataRow dr = dtOPData.NewRow();
        DataColumn dtCol1 = new DataColumn("DIVISION", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("BP_NUMBER", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("USER_NAME", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("R_C_NAME", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol4);
        DataColumn dtCol5 = new DataColumn("ADDRESS", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol5);
        DataColumn dtCol6 = new DataColumn("TELEPHONE_NO", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol6);
        DataColumn dtCol7 = new DataColumn("ALTERNATE_PAYER", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol7);
        DataColumn dtCol8 = new DataColumn("DATE_OF_INSPECTION", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol8);
        DataColumn dtCol9 = new DataColumn("CONN_LOAD", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol9);
        DataColumn dtCol10 = new DataColumn("ENFORCEMENT_TYPE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol10);
        DataColumn dtCol11 = new DataColumn("TARIFF_CATEGORY", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol11);
        DataColumn dtCol12 = new DataColumn("CASEID", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol12);
        DataColumn dtCol13 = new DataColumn("CASEID_NUMBER", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol13);
        DataColumn dtCol14 = new DataColumn("NEW_CA", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol14);
        DataColumn dtCol15 = new DataColumn("OLD_CA", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol15);
        DataColumn dtCol16 = new DataColumn("BILL_NO", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol16);
        DataColumn dtCol17 = new DataColumn("BILL_ISSUE_DATE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol17);
        DataColumn dtCol18 = new DataColumn("DUE_DATE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol18);
        DataColumn dtCol19 = new DataColumn("TOTAL_CHARGE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol19);
        DataColumn dtCol20 = new DataColumn("CREDIT_ASSED_PERIOD", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol20);
        DataColumn dtCol21 = new DataColumn("SUNDRY", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol21);
        DataColumn dtCol22 = new DataColumn("NET_CHARGE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol22);
        DataColumn dtCol23 = new DataColumn("LPSC_AMOUNT", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol23);
        DataColumn dtCol24 = new DataColumn("METER_COST", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol24);
        DataColumn dtCol25 = new DataColumn("METER_TEST_CHARGE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol25);
        DataColumn dtCol26 = new DataColumn("LOCK_REASON", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol26);
        DataColumn dtCol27 = new DataColumn("SETTLED_DATE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol27);
        DataColumn dtCol28 = new DataColumn("SETTLED_AMT_REMARK", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol28);
        DataColumn dtCol29 = new DataColumn("CHEQUE_DISHONORED", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol29);
        DataColumn dtCol30 = new DataColumn("NET_DUE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol30);
        DataColumn dtCol31 = new DataColumn("SANCT_LOAD", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol31);

        DataRow dr1 = dtOPData.NewRow();
        dr1["DIVISION"] = _DIVISION;
        dr1["BP_NUMBER"] = _BP_NUMBER;
        dr1["USER_NAME"] = _USER_NAME;
        dr1["R_C_NAME"] = _R_C_NAME;
        dr1["ADDRESS"] = _ADDRESS;
        dr1["TELEPHONE_NO"] = _TELEPHONE_NO;
        dr1["ALTERNATE_PAYER"] = _ALTERNATE_PAYER;
        dr1["DATE_OF_INSPECTION"] = _DATE_OF_INSPECTION;
        dr1["CONN_LOAD"] = _CONN_LOAD;
        dr1["ENFORCEMENT_TYPE"] = _ENFORCEMENT_TYPE;
        dr1["TARIFF_CATEGORY"] = _TARIFF_CATEGORY;
        dr1["CASEID"] = _CASEID;
        dr1["CASEID_NUMBER"] = _CASEID_NUMBER;
        dr1["NEW_CA"] = _NEW_CA;
        dr1["OLD_CA"] = _OLD_CA;
        dr1["BILL_NO"] = _BILL_NO;
        dr1["BILL_ISSUE_DATE"] = _BILL_ISSUE_DATE;
        dr1["DUE_DATE"] = _DUE_DATE;
        dr1["TOTAL_CHARGE"] = _TOTAL_CHARGE;
        dr1["CREDIT_ASSED_PERIOD"] = _CREDIT_ASSED_PERIOD;
        dr1["SUNDRY"] = _SUNDRY;
        dr1["NET_CHARGE"] = _NET_CHARGE;
        dr1["LPSC_AMOUNT"] = _LPSC_AMOUNT;
        dr1["METER_COST"] = _METER_COST;
        dr1["METER_TEST_CHARGE"] = _METER_TEST_CHARGE;
        dr1["LOCK_REASON"] = _LOCK_REASON;
        dr1["SETTLED_DATE"] = _SETTLED_DATE;
        dr1["SETTLED_AMT_REMARK"] = _SETTLED_AMT_REMARK;
        dr1["CHEQUE_DISHONORED"] = _CHEQUE_DISHONORED;
        dr1["NET_DUE"] = _NET_DUE;
        dr1["SANCT_LOAD"] = _SANCT_LOAD;

        dtOPData.Rows.Add(dr1);

        return dtOPData;
    }

    public DataTable CreateOutputData_PaidTable(string _TotPaidAmt, string _TotLPSC, string _TotPaidNet)
    {
        DataTable dtOPData = new DataTable("OutputTablePaid");
        DataRow dr = dtOPData.NewRow();
        DataColumn dtCol1 = new DataColumn("TotPaidAMT", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("TotLPSC", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("TotPaidNet", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol3);

        DataRow dr1 = dtOPData.NewRow();
        dr1["TotPaidAMT"] = _TotPaidAmt;
        dr1["TotLPSC"] = _TotLPSC;
        dr1["TotPaidNet"] = _TotPaidNet;

        dtOPData.Rows.Add(dr1);

        return dtOPData;
    }

}