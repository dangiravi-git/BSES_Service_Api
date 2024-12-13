using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SAP.Middleware.Connector;

/// <summary>
/// Summary description for ZBAPI_CS_BP_DETAIL
/// </summary>
public class ZBAPI_CS_BP_DETAIL
{
    public ZBAPI_CS_BP_DETAIL()
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

    public string AppenedZero(string parameter, int length)
    {
        for (int i = parameter.Length; i < length; i++)
        {
            parameter = "0" + parameter;
        }
        return parameter;
    }

    public void pushOutputDataInDataTable(DataTable dt, string _sO_MANDT, string _sCONSUMER_NO, string _sINVOICE_NO,
        string _sCOMPANY_CODE, string _sDATE_OF_INVOICE, string _SBILL_MONTH, string _sFROM_DATE, string _sTO_DATE,
        string _sNO_OF_DAYS, string _sEXPORT_UNIT, string _sIMPORT_UNIT, string _sSOLAR_MTR_UNIT, string _sFIN_YEAR,
        string _sCUM_APR_TO_SEP, string _sCUM_OCT_TO_MAR, string _sGBI_APR_TO_SEP, string _sGBI_OCT_TO_MAR, string _sCUM_FOR_FY,
        string _sCUM_SINCE_INST)
    {
        DataRow dr = dt.NewRow();
        dr["MANDT"] = _sO_MANDT;
        dr["CONSUMER_NO"] = _sCONSUMER_NO;
        dr["INVOICE_NO"] = _sINVOICE_NO;
        dr["COMPANY_CODE"] = _sCOMPANY_CODE;
        dr["DATE_OF_INVOICE"] = _sDATE_OF_INVOICE;
        dr["BILL_MONTH"] = _SBILL_MONTH;
        dr["FROM_DATE"] = _sFROM_DATE;
        dr["TO_DATE"] = _sTO_DATE;
        dr["NO_OF_DAYS"] = _sNO_OF_DAYS;
        dr["EXPORT_UNIT"] = _sEXPORT_UNIT;
        dr["IMPORT_UNIT"] = _sIMPORT_UNIT;
        dr["SOLAR_MTR_UNIT"] = _sSOLAR_MTR_UNIT;
        dr["FIN_YEAR"] = _sFIN_YEAR;
        dr["CUM_APR_TO_SEP"] = _sCUM_APR_TO_SEP;
        dr["CUM_OCT_TO_MAR"] = _sCUM_OCT_TO_MAR;
        dr["GBI_APR_TO_SEP"] = _sGBI_APR_TO_SEP;
        dr["GBI_OCT_TO_MAR"] = _sGBI_OCT_TO_MAR;
        dr["CUM_FOR_FY"] = _sCUM_FOR_FY;
        dr["CUM_SINCE_INST"] = _sCUM_SINCE_INST;





        dt.Rows.Add(dr);
    }

    public DataTable makeFlagsTable()
    {
        DataTable dtFlags = new DataTable("outputFlagsTable");
        DataRow dr = dtFlags.NewRow();
        DataColumn dtCol1 = new DataColumn("E_O_CA", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("E_O_NAME", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("E_O_AMT", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("E_O_FLAG", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol4);
        DataColumn dtCol5 = new DataColumn("E_O_MSG", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol5);
        return dtFlags;
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

    public DataTable CreateOutputDataTable(string strFLAG, string strAmount, string str1, string str2, string str3,
        string str4, string str5, string str6, string str7, string str8, string str9, string str10, string str11,
        string str12, string str13, string str14, string str15, string str16, string str17)
    {
        DataTable dtOPData = new DataTable("OutputTable");
        DataRow dr = dtOPData.NewRow();
        DataColumn dtCol1 = new DataColumn("MANDT", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("CONSUMER_NO", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("INVOICE_NO", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("COMPANY_CODE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol4);
        DataColumn dtCol5 = new DataColumn("DATE_OF_INVOICE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol5);
        DataColumn dtCol6 = new DataColumn("BILL_MONTH", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol6);
        DataColumn dtCol7 = new DataColumn("FROM_DATE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol7);
        DataColumn dtCol8 = new DataColumn("TO_DATE", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol8);
        DataColumn dtCol9 = new DataColumn("NO_OF_DAYS", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol9);
        DataColumn dtCol10 = new DataColumn("EXPORT_UNIT", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol10);
        DataColumn dtCol11 = new DataColumn("IMPORT_UNIT", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol11);
        DataColumn dtCol12 = new DataColumn("SOLAR_MTR_UNIT", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol12);
        DataColumn dtCol13 = new DataColumn("FIN_YEAR", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol13);
        DataColumn dtCol14 = new DataColumn("CUM_APR_TO_SEP", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol14);
        DataColumn dtCol15 = new DataColumn("CUM_OCT_TO_MAR", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol15);
        DataColumn dtCol16 = new DataColumn("GBI_APR_TO_SEP", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol16);
        DataColumn dtCol17 = new DataColumn("GBI_OCT_TO_MAR", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol17);
        DataColumn dtCol18 = new DataColumn("CUM_FOR_FY", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol18);
        DataColumn dtCol19 = new DataColumn("CUM_SINCE_INST", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol19);
        DataRow dr1 = dtOPData.NewRow();
        dr1["MANDT"] = strFLAG;
        dr1["CONSUMER_NO"] = strAmount;
        dr1["INVOICE_NO"] = str1;
        dr1["COMPANY_CODE"] = str2;
        dr1["DATE_OF_INVOICE"] = str3;
        dr1["BILL_MONTH"] = str4;
        dr1["FROM_DATE"] = str5;
        dr1["TO_DATE"] = str6;
        dr1["NO_OF_DAYS"] = str7;
        dr1["EXPORT_UNIT"] = str8;
        dr1["IMPORT_UNIT"] = str9;
        dr1["SOLAR_MTR_UNIT"] = str10;
        dr1["FIN_YEAR"] = str11;
        dr1["CUM_APR_TO_SEP"] = str12;
        dr1["CUM_OCT_TO_MAR"] = str13;
        dr1["GBI_APR_TO_SEP"] = str14;

        dr1["GBI_OCT_TO_MAR"] = str15;
        dr1["CUM_FOR_FY"] = str16;
        dr1["CUM_SINCE_INST"] = str17;
        dtOPData.Rows.Add(dr1);

        return dtOPData;
    }
}