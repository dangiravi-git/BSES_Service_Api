using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SAP.Middleware.Connector;

/// <summary>
/// Summary description for ZBAPI_FICA_DEMAND_NOTE
/// </summary>
public class ZBAPI_FICA_DEMAND_NOTE
{
    public ZBAPI_FICA_DEMAND_NOTE()
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

    public DataTable makeDataDetails_Dua_date()
    {
        DataTable dt = new DataTable("OutputTable");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Doc_Number";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Amount";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Ca_Number";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Message";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Total";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Consumer_Name";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Company";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Telephone";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Email_ID";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Address";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Due_Date";
        dt.Columns.Add(dcol);

        return dt;
    }

    public void pushMessageTextInDataTable(DataTable dt, string messageCode, string messageToPush)
    {
        DataRow dr = dt.NewRow();
        dr["messageCode"] = messageCode;
        dr["messageText"] = messageToPush;
        dt.Rows.Add(dr);
    }

    public DataTable makeDataDetails()
    {
        DataTable dt = new DataTable("OutputTable");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Doc_Number";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Amount";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Ca_Number";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Message";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Total";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Consumer_Name";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Company";
        dt.Columns.Add(dcol);

        //dcol = new DataColumn();
        //dcol.DataType = System.Type.GetType("System.String");
        //dcol.ColumnName = "Telephone";
        //dt.Columns.Add(dcol);

        //dcol = new DataColumn();
        //dcol.DataType = System.Type.GetType("System.String");
        //dcol.ColumnName = "Email_ID";
        //dt.Columns.Add(dcol);

        return dt;
    }

    public DataTable bindDataTable(DataTable _tableData)
    {
        DataTable _dt = makeDataDetails();
        for (int i = 0; i < _tableData.Rows.Count; i++)
        {
            DataRow dr = _dt.NewRow();
            dr["Doc_Number"] = _tableData.Rows[i]["OPBEL"].ToString();
            dr["Amount"] = _tableData.Rows[i]["BETRW"].ToString();
            dr["Ca_Number"] = _tableData.Rows[i]["VKONT"].ToString();
            dr["Message"] = _tableData.Rows[i]["HVORG_TEXT"].ToString();
            dr["Total"] = _tableData.Rows[i]["TOTAL"].ToString();
            dr["Consumer_Name"] = _tableData.Rows[i]["CONSUMER_NAME"].ToString();
            dr["Company"] = _tableData.Rows[i]["OPBUK"].ToString();
            //dr["Telephone"] = _tableData.Rows[i]["TELEPHONE"].ToString();
            //dr["Email_ID"] = _tableData.Rows[i]["EMAIL_ID"].ToString();
            _dt.Rows.Add(dr);
        }
        return _dt;
    }

    public DataTable bindDataTable_Due_Date(DataTable _tableData)
    {
        DataTable _dt = makeDataDetails_Dua_date();
        for (int i = 0; i < _tableData.Rows.Count; i++)
        {
            DataRow dr = _dt.NewRow();
            dr["Doc_Number"] = _tableData.Rows[i]["OPBEL"].ToString();
            dr["Amount"] = _tableData.Rows[i]["BETRW"].ToString();
            dr["Ca_Number"] = _tableData.Rows[i]["VKONT"].ToString();
            dr["Message"] = _tableData.Rows[i]["HVORG_TEXT"].ToString();
            dr["Total"] = _tableData.Rows[i]["TOTAL"].ToString();
            dr["Consumer_Name"] = _tableData.Rows[i]["CONSUMER_NAME"].ToString();
            dr["Company"] = _tableData.Rows[i]["OPBUK"].ToString();
            dr["Telephone"] = _tableData.Rows[i]["TELEPHONE"].ToString();
            dr["Email_ID"] = _tableData.Rows[i]["EMAIL_ID"].ToString();
            dr["Address"] = _tableData.Rows[i]["ADDRESS"].ToString();
            dr["Due_Date"] = _tableData.Rows[i]["FAEDN"].ToString();
            _dt.Rows.Add(dr);
        }
        return _dt;
    }

    public DataTable makeReturnTable()
    {
        DataTable dt = new DataTable("SAPDATA_ErrorDataTable");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "Type";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "CODE";
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


    public void addRowToDTError(DataTable dt, string DATA1, string DATA2, string DATA4, string DATA5, string DATA6, string DATA7, string DATA8, string DATA9, string DATA10)
    {
        DataRow dr = dt.NewRow();
        dr["Type"] = DATA1;
        dr["Code"] = DATA2;
        dr["Message"] = DATA4;
        dr["Log_No"] = DATA5;
        dr["Log_Msg_No"] = DATA6;
        dr["Message_V1"] = DATA7;
        dr["Message_V2"] = DATA8;
        dr["Message_V3"] = DATA9;
        dr["Message_V4"] = DATA10;
        dt.Rows.Add(dr);

    }

    public DataTable makeBAPIRET2TABLE(DataTable orgBAPIRET2TABLE)
    {
        DataTable ErrorDataTable = makeReturnTable();
        if (orgBAPIRET2TABLE.Rows.Count > 0)
        {
            for (int intRowCnt = 0; intRowCnt < orgBAPIRET2TABLE.Rows.Count; intRowCnt++)
            {
                addRowToDTError(ErrorDataTable, orgBAPIRET2TABLE.Rows[intRowCnt][0].ToString(),
                                orgBAPIRET2TABLE.Rows[intRowCnt][1].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][2].ToString(),
                                orgBAPIRET2TABLE.Rows[intRowCnt][3].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][4].ToString(),
                                orgBAPIRET2TABLE.Rows[intRowCnt][5].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][6].ToString(),
                                orgBAPIRET2TABLE.Rows[intRowCnt][7].ToString(), orgBAPIRET2TABLE.Rows[intRowCnt][8].ToString());
            }

        }

        return ErrorDataTable;

    }
}