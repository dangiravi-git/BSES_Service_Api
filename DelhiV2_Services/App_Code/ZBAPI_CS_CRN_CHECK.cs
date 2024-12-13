using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;
using System.Data;

/// <summary>
/// Summary description for ZBAPI_CS_CRN_CHECK
/// </summary>
public class ZBAPI_CS_CRN_CHECK
{
    public ZBAPI_CS_CRN_CHECK()
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


    public DataTable getCADetailsFormattedTable(DataTable rawTable)
    {
        DataTable dtProcessedData = new DataTable();
        dtProcessedData = CADetailsFormDT();
        string _sOutFrmDate = string.Empty, _sOutTodate = string.Empty, _sMoveOutDate = string.Empty;
        int rowCnt = 0, colCnt = 0;
        rowCnt = rawTable.Rows.Count;
        colCnt = rawTable.Columns.Count;

        if (rawTable.Rows.Count > 0)
        {
            for (int intRowCnt = 0; intRowCnt < rawTable.Rows.Count; intRowCnt++)
            {
                for (int mCol = 0; mCol < colCnt; mCol++)
                {
                    if (mCol == 24)
                        _sOutFrmDate = GetDateFormate_YYYYMMDD1(rawTable.Rows[intRowCnt][24].ToString());
                    else if (mCol == 25)
                        _sOutTodate = GetDateFormate_YYYYMMDD1(rawTable.Rows[intRowCnt][25].ToString());
                    else if (mCol == 34)
                        _sMoveOutDate = GetDateFormate_YYYYMMDD(rawTable.Rows[intRowCnt][34].ToString());
                }

                CADEtailsAddRow(dtProcessedData, rawTable.Rows[intRowCnt][0].ToString(), rawTable.Rows[intRowCnt][1].ToString(), rawTable.Rows[intRowCnt][2].ToString(),
                             rawTable.Rows[intRowCnt][3].ToString(), rawTable.Rows[intRowCnt][4].ToString(), rawTable.Rows[intRowCnt][5].ToString(), rawTable.Rows[intRowCnt][6].ToString(),
                             rawTable.Rows[intRowCnt][7].ToString(), rawTable.Rows[intRowCnt][8].ToString(), rawTable.Rows[intRowCnt][9].ToString(), rawTable.Rows[intRowCnt][10].ToString(),
                             rawTable.Rows[intRowCnt][11].ToString(), rawTable.Rows[intRowCnt][12].ToString(), rawTable.Rows[intRowCnt][13].ToString(), rawTable.Rows[intRowCnt][14].ToString(),
                             rawTable.Rows[intRowCnt][15].ToString(), rawTable.Rows[intRowCnt][16].ToString(), rawTable.Rows[intRowCnt][17].ToString(), rawTable.Rows[intRowCnt][18].ToString(),
                             rawTable.Rows[intRowCnt][19].ToString(), rawTable.Rows[intRowCnt][20].ToString(), rawTable.Rows[intRowCnt][21].ToString(), rawTable.Rows[intRowCnt][22].ToString(),
                             rawTable.Rows[intRowCnt][23].ToString(), _sOutFrmDate, _sOutTodate, rawTable.Rows[intRowCnt][26].ToString(),
                             rawTable.Rows[intRowCnt][27].ToString(), rawTable.Rows[intRowCnt][28].ToString(), rawTable.Rows[intRowCnt][29].ToString(), rawTable.Rows[intRowCnt][30].ToString(),
                             rawTable.Rows[intRowCnt][31].ToString(), rawTable.Rows[intRowCnt][32].ToString(), rawTable.Rows[intRowCnt][33].ToString(), _sMoveOutDate,
                             rawTable.Rows[intRowCnt][35].ToString(), rawTable.Rows[intRowCnt][36].ToString(), rawTable.Rows[intRowCnt][37].ToString(), rawTable.Rows[intRowCnt][38].ToString(),
                             rawTable.Rows[intRowCnt][39].ToString(), rawTable.Rows[intRowCnt][40].ToString());

            }

        }
        return dtProcessedData;
    }


    public DataTable CADetailsFormDT()
    {
        DataTable dt = new DataTable("ISUSTDTable");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Ca_Number";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Bp_Number";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Bp_Name";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Bp_Type";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Search_Term1";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Search_Term2";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "House_Number";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "House_Number_Sup";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Floor";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Premise_Type";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Street";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Street2";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Street3";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Street4";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "City";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Post_Code";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Region";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Country";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Desc_Con_Object";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Reg_Str_Group";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Device_Sr_Number";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Telephone_No";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Mru";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Func_Descr";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Outage_Fromtime";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Outage_Totime";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Legacy_Acct";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Bill_Class";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Rate_Cat";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Activity";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Adr_Notes";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Tel1_Number";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Vertrag";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "E_Mail";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Move_Out_Date";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Con_Obj_No";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Clerk_Id";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Text";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Status";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Discreason";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "POLE_NO";
        dt.Columns.Add(dcol);

        return dt;
    }

    public void CADEtailsAddRow(DataTable dt, string DATA1, string DATA2, string DATA3, string DATA4, string DATA5, string DATA6, string DATA7, string DATA8, string DATA9, string DATA10,
            string DATA11, string DATA12, string DATA13, string DATA14, string DATA15, string DATA16, string DATA17, string DATA18, string DATA19, string DATA20, string DATA21,
            string DATA22, string DATA23, string DATA24, string DATA25, string DATA26, string DATA27, string DATA28, string DATA29, string DATA30, string DATA31, string DATA32,
            string DATA33, string DATA34, string DATA35, string DATA36, string DATA37, string DATA38, string DATA39, string DATA40, string DATA41)
    {
        DataRow dr = dt.NewRow();
        dr["Ca_Number"] = DATA1;
        dr["Bp_Number"] = DATA2;
        dr["Bp_Name"] = DATA3;
        dr["Bp_Type"] = DATA4;
        dr["Search_Term1"] = DATA5;
        dr["Search_Term2"] = DATA6;
        dr["House_Number"] = DATA7;
        dr["House_Number_Sup"] = DATA8;
        dr["Floor"] = DATA9;
        dr["Premise_Type"] = DATA10;
        dr["Street"] = DATA11;
        dr["Street2"] = DATA12;
        dr["Street3"] = DATA13;
        dr["Street4"] = DATA14;
        dr["City"] = DATA15;
        dr["Post_Code"] = DATA16;
        dr["Region"] = DATA17;
        dr["Country"] = DATA18;
        dr["Desc_Con_Object"] = DATA19;
        dr["Reg_Str_Group"] = DATA20;
        dr["Device_Sr_Number"] = DATA21;
        dr["Telephone_No"] = DATA22;
        dr["Mru"] = DATA23;
        dr["Func_Descr"] = DATA24;
        dr["Outage_Fromtime"] = DATA25;
        dr["Outage_Totime"] = DATA26;
        dr["Legacy_Acct"] = DATA27;
        dr["Bill_Class"] = DATA28;
        dr["Rate_Cat"] = DATA29;
        dr["Activity"] = DATA30;
        dr["Adr_Notes"] = DATA31;
        dr["Tel1_Number"] = DATA32;
        dr["Vertrag"] = DATA33;
        dr["E_Mail"] = DATA34;
        dr["Move_Out_Date"] = DATA35;
        dr["Con_Obj_No"] = DATA36;
        dr["Clerk_Id"] = DATA37;
        dr["Text"] = DATA38;
        dr["Status"] = DATA39;
        dr["Discreason"] = DATA40;
        dr["POLE_NO"] = DATA41;
        dt.Rows.Add(dr);

    }


    //Function to make a DataTable with same structure as ZBAPICMSSTTable table of BAPI
    public DataTable getCSDetailsFormattedTable(DataTable rawTable)
    {
        DataTable dtProcessedData = new DataTable();
        dtProcessedData = CSDetailsFormDT();

        if (rawTable.Rows.Count > 0)
        {
            for (int intRowCnt = 0; intRowCnt < rawTable.Rows.Count; intRowCnt++)
            {
                CSDEtailsAddRow(dtProcessedData, rawTable.Rows[intRowCnt][0].ToString(), rawTable.Rows[intRowCnt][1].ToString(), rawTable.Rows[intRowCnt][2].ToString(),
                    rawTable.Rows[intRowCnt][3].ToString(), rawTable.Rows[intRowCnt][4].ToString(), rawTable.Rows[intRowCnt][5].ToString(), rawTable.Rows[intRowCnt][6].ToString());
            }

        }
        return dtProcessedData;
    }

    public DataTable CSDetailsFormDT()
    {
        DataTable dt = new DataTable("CMSSTTable");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Ca_Number";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Contract";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Bp_Name";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "House_Number_Sup";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Street";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Street2";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Adr_notes";
        dt.Columns.Add(dcol);

        return dt;
    }

    public void CSDEtailsAddRow(DataTable dt, string DATA1, string DATA2, string DATA3, string DATA4, string DATA5, string DATA6, string DATA7)
    {
        DataRow dr = dt.NewRow();
        dr["Ca_Number"] = DATA1;
        dr["Bp_Number"] = DATA2;
        dr["Bp_Type"] = DATA3;
        dr["Search_Term1"] = DATA4;
        dr["Search_term2"] = DATA5;
        dr["House_Number"] = DATA6;
        dr["House_Number_Sup"] = DATA7;
        dt.Rows.Add(dr);

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
