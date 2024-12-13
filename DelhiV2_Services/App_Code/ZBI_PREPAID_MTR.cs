using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;
using System.Data;

/// <summary>
/// Summary description for ZBI_PREPAID_MTR
/// </summary>
public class ZBI_PREPAID_MTR
{
    public ZBI_PREPAID_MTR()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //public DataTable converttodotnetatble(IRfcTable rfctable)
    //{
    //    DataTable dt = new DataTable();

    //    for (int i = 0; i < rfctable.ElementCount; i++)
    //    {
    //        RfcElementMetadata metadata = rfctable.GetElementMetadata(i);
    //        dt.Columns.Add(metadata.Name);
    //    }

    //    foreach (IRfcStructure row in rfctable)
    //    {
    //        DataRow dr = dt.NewRow();

    //        for (int i = 0; i < rfctable.ElementCount; i++)
    //        {
    //            RfcElementMetadata metadata = rfctable.GetElementMetadata(i);
    //            if (metadata.DataType == RfcDataType.BCD && metadata.Name == "ABC")
    //            {
    //                dr[i] = row.GetString(metadata.Name);
    //            }
    //            else
    //                dr[i] = row.GetString(metadata.Name);

    //        }
    //        dt.Rows.Add(dr);
    //    }

    //    return dt;
    //}



    //public void pushOutputDataInDataTable(DataTable dt, string FLAG)
    //{
    //    DataRow dr = dt.NewRow();
    //    dr["OUT_PREPAID_FLAG"] = FLAG;

    //    dt.Rows.Add(dr);
    //}

    //public DataTable transferMeterData(DataTable originalTable)
    //{
    //    DataTable dtOutPut = makeMeterDetailsTable();
    //    int rowCnt = 0, colCnt = 0;
    //    rowCnt = originalTable.Rows.Count;
    //    colCnt = originalTable.Columns.Count;

    //    if (rowCnt > 0)
    //    {
    //        for (int mRow = 0; mRow < rowCnt; mRow++)
    //        {
    //            DataRow dr = dtOutPut.NewRow();

    //            for (int mCol = 0; mCol < colCnt; mCol++)
    //            {
    //                if ((mCol == 2) || (mCol == 3))
    //                    dr[mCol] = GetDateFormate_YYYYMMDD(originalTable.Rows[mRow][mCol].ToString());
    //                else
    //                    dr[mCol] = originalTable.Rows[mRow][mCol].ToString();
    //            }
    //            dtOutPut.Rows.Add(dr);
    //        }
    //    }
    //    return dtOutPut;
    //}

    //public DataTable makeMeterDetailsTable()
    //{
    //    DataTable dtMeterDetails = new DataTable("meterDetailsTable");
    //    DataRow dr = dtMeterDetails.NewRow();

    //    DataColumn dtCol1 = new DataColumn("Meter_No", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol1);
    //    DataColumn dtCol2 = new DataColumn("Mf_Factor", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol2);
    //    DataColumn dtCol3 = new DataColumn("Pre_Mr_Date", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol3);
    //    DataColumn dtCol4 = new DataColumn("Curr_Mr_Date", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol4);
    //    DataColumn dtCol5 = new DataColumn("Prev_Mr_Kwh", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol5);
    //    DataColumn dtCol6 = new DataColumn("Curr_Mr_Kwh", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol6);
    //    DataColumn dtCol7 = new DataColumn("Unit_Consum_Kwh", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol7);
    //    DataColumn dtCol8 = new DataColumn("Prev_Mr_Kvah", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol8);
    //    DataColumn dtCol9 = new DataColumn("Curr_Mr_Kvah", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol9);
    //    DataColumn dtCol10 = new DataColumn("Unit_Consum_Kvah", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol10);
    //    DataColumn dtCol11 = new DataColumn("Prev_Mr_Kw", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol11);
    //    DataColumn dtCol12 = new DataColumn("Curr_Mr_Kw", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol12);
    //    DataColumn dtCol13 = new DataColumn("Unit_Consum_Kw", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol13);
    //    DataColumn dtCol14 = new DataColumn("Prev_Mr_Kva", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol14);
    //    DataColumn dtCol15 = new DataColumn("Curr_Mr_Kva", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol15);
    //    DataColumn dtCol16 = new DataColumn("Unit_Consum_Kva", System.Type.GetType("System.String"));
    //    dtMeterDetails.Columns.Add(dtCol16);

    //    return dtMeterDetails;
    //}

    //public DataTable makeBAPIRET2TABLE(DataTable orgBAPIRET2TABLE)
    //{
    //    DataTable ErrorDataTable = formDataTableError();
    //    if (orgBAPIRET2TABLE.Rows.Count > 0)
    //    {
    //        for (int intRowCnt = 0; intRowCnt < orgBAPIRET2TABLE.Rows.Count; intRowCnt++)
    //        {
    //            addRowToDTError(ErrorDataTable, orgBAPIRET2TABLE.Rows[intRowCnt][0].ToString(),
    //                            orgBAPIRET2TABLE.Rows[intRowCnt][1].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][2].ToString(),
    //                            orgBAPIRET2TABLE.Rows[intRowCnt][3].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][4].ToString(),
    //                            orgBAPIRET2TABLE.Rows[intRowCnt][5].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][6].ToString(),
    //                            orgBAPIRET2TABLE.Rows[intRowCnt][7].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][8].ToString(),
    //                            orgBAPIRET2TABLE.Rows[intRowCnt][9].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][10].ToString(),
    //                            orgBAPIRET2TABLE.Rows[intRowCnt][11].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][12].ToString(),
    //                            orgBAPIRET2TABLE.Rows[intRowCnt][13].ToString());
    //        }

    //    }

    //    return ErrorDataTable;

    //}

    //public DataTable formDataTableError()
    //{
    //    DataTable dt = new DataTable("SAPDATA_ErrorDataTable");
    //    DataColumn dcol;

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Type";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Id";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Number";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Message";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Log_No";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Log_Msg_No";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Message_V1";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Message_V2";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Message_V3";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Message_V4";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Parameter";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Row";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "Field";
    //    dt.Columns.Add(dcol);

    //    dcol = new DataColumn();
    //    dcol.DataType = System.Type.GetType("System.String");
    //    dcol.ColumnName = "System";
    //    dt.Columns.Add(dcol);

    //    return dt;
    //}

    //public DataTable makeMessageTextTable()
    //{
    //    DataTable dtMessage = new DataTable("messageTable");
    //    DataRow dr = dtMessage.NewRow();
    //    DataColumn dtCol1 = new DataColumn("messageCode", System.Type.GetType("System.String"));
    //    dtMessage.Columns.Add(dtCol1);
    //    DataColumn dtCol2 = new DataColumn("messageText", System.Type.GetType("System.String"));
    //    dtMessage.Columns.Add(dtCol2);
    //    return dtMessage;
    //}

    //public DataTable pushDataInErrorTable(DataTable originalTable)
    //{

    //    DataTable dtOutPut = makeErrorTable();

    //    int rowCnt = 0, colCnt = 0;
    //    rowCnt = originalTable.Rows.Count;
    //    colCnt = originalTable.Columns.Count;

    //    if (rowCnt > 0)
    //    {
    //        for (int mRow = 0; mRow < rowCnt; mRow++)
    //        {
    //            DataRow dr = dtOutPut.NewRow();

    //            for (int mCol = 0; mCol < colCnt; mCol++)
    //            {
    //                dr[mCol] = originalTable.Rows[mRow][mCol].ToString();
    //            }
    //            dtOutPut.Rows.Add(dr);
    //        }
    //    }
    //    return dtOutPut;

    //}

    //public DataTable makeErrorTable()
    //{
    //    DataTable dtErrorTable = new DataTable("ErrorTable");
    //    DataRow dr = dtErrorTable.NewRow();

    //    DataColumn dtCol1 = new DataColumn("Data", System.Type.GetType("System.String"));
    //    dtErrorTable.Columns.Add(dtCol1);

    //    return dtErrorTable;
    //}
    //public void pushMessageTextInDataTable(DataTable dt, string messageCode, string messageToPush)
    //{
    //    DataRow dr = dt.NewRow();
    //    dr["messageCode"] = messageCode;
    //    dr["messageText"] = messageToPush;
    //    dt.Rows.Add(dr);

    //}

    //public void addRowToDTError(DataTable dt, string DATA1, string DATA2, string DATA3, string DATA4, string DATA5, string DATA6, string DATA7, string DATA8, string DATA9, string DATA10, string DATA11, string DATA12, string DATA13, string DATA14)
    //{
    //    DataRow dr = dt.NewRow();
    //    dr["Type"] = DATA1;
    //    dr["Id"] = DATA2;
    //    dr["Number"] = DATA3;
    //    dr["Message"] = DATA4;
    //    dr["Log_No"] = DATA5;
    //    dr["Log_Msg_No"] = DATA6;
    //    dr["Message_V1"] = DATA7;
    //    dr["Message_V2"] = DATA8;
    //    dr["Message_V3"] = DATA9;
    //    dr["Message_V4"] = DATA10;
    //    dr["Parameter"] = DATA11;
    //    dr["Row"] = DATA12;
    //    dr["Field"] = DATA13;
    //    dr["System"] = DATA14;
    //    dt.Rows.Add(dr);

    //}

    //public string GetDateFormate_YYYYMMDD(string _sDate)
    //{
    //    string _sDateformate = string.Empty;
    //    string[] DateFormate = _sDate.ToString().Split('-');

    //    for (int i = 0; i < DateFormate.Length; i++)
    //    {
    //        _sDateformate += DateFormate[i];
    //    }

    //    return _sDateformate;
    //}


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
}