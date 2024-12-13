using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;
using System.Data;

/// <summary>
/// Summary description for ZBAPI_DISPLAY_BILL_WEB
/// </summary>
public class ZBAPI_DISPLAY_BILL_WEB
{
    public ZBAPI_DISPLAY_BILL_WEB()
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

    public DataTable transferBillData(DataTable originalTable)
    {
        DataTable dtOutPut = makeBillDetailsTable();
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
                    if ((mCol == 2) || (mCol == 9) || (mCol == 54) || (mCol == 55) || (mCol == 66) || (mCol == 87))
                        dr[mCol] = GetDateFormate_YYYYMMDD(originalTable.Rows[mRow][mCol].ToString());
                    //else if (mCol !=92)
                    //    dr[mCol] = originalTable.Rows[mRow][mCol].ToString();
                    else
                        dr[mCol] = originalTable.Rows[mRow][mCol].ToString();
                }
                dtOutPut.Rows.Add(dr);
            }
        }
        return dtOutPut;
    }

    public DataTable makeBillDetailsTable()
    {
        DataTable dtBillDetails = new DataTable("billDetailsTable");
        DataRow dr = dtBillDetails.NewRow();

        DataColumn dtCol1 = new DataColumn("INVOICE_NO", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("CONSUMER_NO", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("DATE_OF_INVOICE", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("CYCLE_NO", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol4);
        DataColumn dtCol5 = new DataColumn("FIRSTNAME", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol5);
        DataColumn dtCol6 = new DataColumn("HOUSE_NO", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol6);
        DataColumn dtCol7 = new DataColumn("UNIT_DESCR", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol7);
        DataColumn dtCol8 = new DataColumn("CIRCLE_DESCR", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol8);
        DataColumn dtCol9 = new DataColumn("NET_AMNT", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol9);
        DataColumn dtCol10 = new DataColumn("DUE_DATE", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol10);
        DataColumn dtCol11 = new DataColumn("BILL_MONTH", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol11);
        DataColumn dtCol12 = new DataColumn("TOT_UNITS_BILLED", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol12);
        DataColumn dtCol13 = new DataColumn("BILL_BASIS", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol13);
        DataColumn dtCol14 = new DataColumn("SANC_LOAD", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol14);
        DataColumn dtCol15 = new DataColumn("RATE_CATEGORY", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol15);
        DataColumn dtCol16 = new DataColumn("CONT_DEMAND", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol16);
        DataColumn dtCol17 = new DataColumn("TOT_ENRGY_CHRG", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol17);
        DataColumn dtCol18 = new DataColumn("TOT_ENE_CHRG_AMD", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol18);
        DataColumn dtCol19 = new DataColumn("TOT_ENE_CHG_AMDA", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol19);
        DataColumn dtCol20 = new DataColumn("TOT_FIX_CHRG", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol20);
        DataColumn dtCol21 = new DataColumn("TOT_FIX_CHRG_AMD", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol21);
        DataColumn dtCol22 = new DataColumn("TOT_FIX_CHG_AMDA", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol22);
        DataColumn dtCol23 = new DataColumn("ELECT_TAX", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol23);
        DataColumn dtCol24 = new DataColumn("ELECT_TAX_AMNDS", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol24);
        DataColumn dtCol25 = new DataColumn("ELECT_TAX_AMDA", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol25);
        DataColumn dtCol26 = new DataColumn("REBATE", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol26);
        DataColumn dtCol27 = new DataColumn("REBATE_AMNDS", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol27);
        DataColumn dtCol28 = new DataColumn("REBATE_AMNDTAM", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol28);
        DataColumn dtCol29 = new DataColumn("RATE_SUBSIDY", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol29);
        DataColumn dtCol30 = new DataColumn("RATE_SUBSIDY_AMD", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol30);
        DataColumn dtCol31 = new DataColumn("RATE_SBSIDY_AMDA", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol31);
        DataColumn dtCol32 = new DataColumn("SPL_SUBSIDY", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol32);
        DataColumn dtCol33 = new DataColumn("SPL_SUBSIDY_AMN", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol33);
        DataColumn dtCol34 = new DataColumn("SPL_SBSIDY_AMDA", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol34);
        DataColumn dtCol35 = new DataColumn("OTHER_CHRGS", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol35);
        DataColumn dtCol36 = new DataColumn("OTHER_CHRGS_AMND", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol36);
        DataColumn dtCol37 = new DataColumn("OTHER_CHRGS_AMDA", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol37);
        DataColumn dtCol38 = new DataColumn("LPSC_CURRENT", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol38);
        DataColumn dtCol39 = new DataColumn("CUR_MTH_BILL_AMT", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol39);
        DataColumn dtCol40 = new DataColumn("CUR_MTH_BILL_AMD", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol40);
        DataColumn dtCol41 = new DataColumn("CUR_MTH_BIL_AMDA", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol41);
        DataColumn dtCol42 = new DataColumn("ARR_PAYABLE", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol42);
        DataColumn dtCol43 = new DataColumn("ARR_PAYABLE_AMD", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol43);
        DataColumn dtCol44 = new DataColumn("ARR_PAYABLE_AMDA", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol44);
        DataColumn dtCol45 = new DataColumn("ARR_ENRGY_CHRG", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol45);
        DataColumn dtCol46 = new DataColumn("ARR_ELECT_TAX", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol46);
        DataColumn dtCol47 = new DataColumn("ARR_OTHER_CHRGS", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol47);
        DataColumn dtCol48 = new DataColumn("ARR_LPSC", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol48);
        DataColumn dtCol49 = new DataColumn("AR_LAST_MTH_BILL", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol49);
        DataColumn dtCol50 = new DataColumn("REFUND", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol50);
        DataColumn dtCol51 = new DataColumn("DEFERRED_AMNT", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol51);
        DataColumn dtCol52 = new DataColumn("INST_NOT_DUE", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol52);
        DataColumn dtCol53 = new DataColumn("PYMT_RECD_AMNT", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol53);
        DataColumn dtCol54 = new DataColumn("ARREARS_PAYABLE", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol54);
        DataColumn dtCol55 = new DataColumn("AMEN_PERIOD_FRM", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol55);
        DataColumn dtCol56 = new DataColumn("AMEN_PERIOD_TO", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol56);
        DataColumn dtCol57 = new DataColumn("AMD_REASON", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol57);
        DataColumn dtCol58 = new DataColumn("TOT_ORIG_AMNT", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol58);
        DataColumn dtCol59 = new DataColumn("TOT_AMND_AMNT", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol59);
        DataColumn dtCol60 = new DataColumn("UNITS_1", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol60);
        DataColumn dtCol61 = new DataColumn("UNITS_2", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol61);
        DataColumn dtCol62 = new DataColumn("UNITS_3", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol62);
        DataColumn dtCol63 = new DataColumn("UNITS_4", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol63);
        DataColumn dtCol64 = new DataColumn("UNITS_5", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol64);
        DataColumn dtCol65 = new DataColumn("UNITS_6", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol65);
        DataColumn dtCol66 = new DataColumn("AMNT_AFT_DUE_DAT", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol66);
        DataColumn dtCol67 = new DataColumn("PYMT_ACCNTD_UPTO", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol67);
        DataColumn dtCol68 = new DataColumn("SEC_DEP_AMNT", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol68);
        DataColumn dtCol69 = new DataColumn("TARIFF", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol69);
        DataColumn dtCol70 = new DataColumn("METER_TYPE", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol70);
        DataColumn dtCol71 = new DataColumn("COMPANY_CODE", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol71);
        DataColumn dtCol72 = new DataColumn("LASTNAME", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol72);
        DataColumn dtCol73 = new DataColumn("MIDDLE_NAME", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol73);
        DataColumn dtCol74 = new DataColumn("FATHER_NAME", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol74);
        DataColumn dtCol75 = new DataColumn("STREET", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol75);
        DataColumn dtCol76 = new DataColumn("STR_SUPPL1", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol76);
        DataColumn dtCol77 = new DataColumn("STR_SUPPL2", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol77);
        DataColumn dtCol78 = new DataColumn("STR_SUPPL3", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol78);
        DataColumn dtCol79 = new DataColumn("CITY", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol79);
        DataColumn dtCol80 = new DataColumn("POSTL_COD1", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol80);
        DataColumn dtCol81 = new DataColumn("ADJUSTMENT", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol81);
        DataColumn dtCol82 = new DataColumn("PAYMENT_RECEIVED", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol82);
        DataColumn dtCol83 = new DataColumn("PAYMENT_DATE", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol83);
        DataColumn dtCol84 = new DataColumn("TOT_BIL_AMT", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol84);
        DataColumn dtCol85 = new DataColumn("TOT_BIL_AMNDS", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol85);
        DataColumn dtCol86 = new DataColumn("TOT_BIL_AMDTAM", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol86);
        DataColumn dtCol87 = new DataColumn("MDI", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol87);
        DataColumn dtCol89 = new DataColumn("LEGAL_ORG", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol89);
        DataColumn dtCol90 = new DataColumn("FLAG1", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol90);
        DataColumn dtCol91 = new DataColumn("FLAG2", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol91);
        DataColumn dtCol92 = new DataColumn("NET_AMT_AFT_TDS", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol92);
        DataColumn dtCol93 = new DataColumn("TDS_PERCENT", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol93);

        DataColumn dtCol94 = new DataColumn("CONNECTION_TYPE", System.Type.GetType("System.String"));
        dtBillDetails.Columns.Add(dtCol94);


        return dtBillDetails;

    }

    public DataTable transferMeterData(DataTable originalTable)
    {
        DataTable dtOutPut = makeMeterDetailsTable();
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
                    if ((mCol == 2) || (mCol == 3))
                        dr[mCol] = GetDateFormate_YYYYMMDD(originalTable.Rows[mRow][mCol].ToString());
                    else
                        dr[mCol] = originalTable.Rows[mRow][mCol].ToString();
                }
                dtOutPut.Rows.Add(dr);
            }
        }
        return dtOutPut;
    }

    public DataTable makeMeterDetailsTable()
    {
        DataTable dtMeterDetails = new DataTable("meterDetailsTable");
        DataRow dr = dtMeterDetails.NewRow();

        DataColumn dtCol1 = new DataColumn("Meter_No", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("Mf_Factor", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("Pre_Mr_Date", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("Curr_Mr_Date", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol4);
        DataColumn dtCol5 = new DataColumn("Prev_Mr_Kwh", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol5);
        DataColumn dtCol6 = new DataColumn("Curr_Mr_Kwh", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol6);
        DataColumn dtCol7 = new DataColumn("Unit_Consum_Kwh", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol7);
        DataColumn dtCol8 = new DataColumn("Prev_Mr_Kvah", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol8);
        DataColumn dtCol9 = new DataColumn("Curr_Mr_Kvah", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol9);
        DataColumn dtCol10 = new DataColumn("Unit_Consum_Kvah", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol10);
        DataColumn dtCol11 = new DataColumn("Prev_Mr_Kw", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol11);
        DataColumn dtCol12 = new DataColumn("Curr_Mr_Kw", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol12);
        DataColumn dtCol13 = new DataColumn("Unit_Consum_Kw", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol13);
        DataColumn dtCol14 = new DataColumn("Prev_Mr_Kva", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol14);
        DataColumn dtCol15 = new DataColumn("Curr_Mr_Kva", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol15);
        DataColumn dtCol16 = new DataColumn("Unit_Consum_Kva", System.Type.GetType("System.String"));
        dtMeterDetails.Columns.Add(dtCol16);

        return dtMeterDetails;
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