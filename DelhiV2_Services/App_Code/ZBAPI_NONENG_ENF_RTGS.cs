using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;
using System.Data;

/// <summary>
/// Summary description for ZBAPI_NONENG_ENF_RTGS
/// </summary>
public class ZBAPI_NONENG_ENF_RTGS
{
    public ZBAPI_NONENG_ENF_RTGS()
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
        DataTable dt = new DataTable("STRUC_OUT");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "COMP_CODE";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "CONTRACT_ACCOUNT";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "OUT_AMT";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "NAME";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "ADDRESS";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "TEL1_NUMBR";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "E_MAIL";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "ACCT_DET_ID";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "ACCT_CLASS";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "FLAG";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "MESSAGE";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "ORDER_NO";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "OPBEL";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "DUE_DATE";
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
        dr["COMP_CODE"] = DATA1;
        dr["CONTRACT_ACCOUNT"] = DATA2;
        dr["OUT_AMT"] = DATA3;
        dr["NAME"] = DATA4;
        dr["ADDRESS"] = DATA5;
        dr["TEL1_NUMBR"] = DATA6;
        dr["E_MAIL"] = DATA7;
        dr["ACCT_DET_ID"] = DATA8;
        dr["ACCT_CLASS"] = DATA9;
        dr["FLAG"] = DATA10;
        dr["MESSAGE"] = DATA11;
        dr["ORDER_NO"] = DATA12;
        dr["OPBEL"] = DATA13;
        dr["DUE_DATE"] = DATA14;
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
}