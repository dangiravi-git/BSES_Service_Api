using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SAP.Middleware.Connector;

/// <summary>
/// Summary description for ZBAPI_ZCS_CLI_WEB
/// </summary>
public class ZBAPI_ZCS_CLI_WEB
{
	public ZBAPI_ZCS_CLI_WEB()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable makeConsumerDetailsDataTable()
    {
        DataTable dt = new DataTable("ConsumerDetailsTable");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "CONTRACT_ACCOUNT_NUMBER";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "BUSINESS_PARTNER_NAME";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "ADDRESS";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "TELEPHONE";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "MOBILE_NO";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "E_MAIL";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "STREET4";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "PARTNER_TYPE";
        dt.Columns.Add(dcol);

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "LEGAL_ORG";
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

    public void pushMessageTextInDataTable(DataTable dt, string messageCode, string messageToPush)
    {
        DataRow dr = dt.NewRow();
        dr["messageCode"] = messageCode;
        dr["messageText"] = messageToPush;
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

    public DataTable CADetailsFormDT()
    {
        DataTable dt = new DataTable("ConsumerDetailsTable");
        DataColumn dcol;

        dcol = new DataColumn();
        dcol.DataType = System.Type.GetType("System.String");
        dcol.ColumnName = "CONTRACT_ACCOUNT_NUMBER";
        dt.Columns.Add(dcol);

        DataColumn dco2 = new DataColumn();
        dco2.DataType = System.Type.GetType("System.String");
        dco2.ColumnName = "BUSINESS_PARTNER_NAME";
        dt.Columns.Add(dco2);

        DataColumn dco3 = new DataColumn();
        dco3.DataType = System.Type.GetType("System.String");
        dco3.ColumnName = "ADDRESS";
        dt.Columns.Add(dco3);

        DataColumn dco4 = new DataColumn();
        dco4.DataType = System.Type.GetType("System.String");
        dco4.ColumnName = "TELEPHONE";
        dt.Columns.Add(dco4);

        DataColumn dco5 = new DataColumn();
        dco5.DataType = System.Type.GetType("System.String");
        dco5.ColumnName = "MOBILE_NO";
        dt.Columns.Add(dco5);

        DataColumn dco6 = new DataColumn();
        dco6.DataType = System.Type.GetType("System.String");
        dco6.ColumnName = "E_MAIL";
        dt.Columns.Add(dco6);

        DataColumn dco7 = new DataColumn();
        dco7.DataType = System.Type.GetType("System.String");
        dco7.ColumnName = "STREET4";
        dt.Columns.Add(dco7);

        DataColumn dco8 = new DataColumn();
        dco8.DataType = System.Type.GetType("System.String");
        dco8.ColumnName = "PARTNER_TYPE";
        dt.Columns.Add(dco8);

        DataColumn dco9 = new DataColumn();
        dco9.DataType = System.Type.GetType("System.String");
        dco9.ColumnName = "LEGAL_ORG";
        dt.Columns.Add(dco9);

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
               CADEtailsAddRow(dtProcessedData, rawTable.Rows[intRowCnt][0].ToString(), rawTable.Rows[intRowCnt][1].ToString(), rawTable.Rows[intRowCnt][2].ToString(),
                             rawTable.Rows[intRowCnt][3].ToString(), rawTable.Rows[intRowCnt][4].ToString(), rawTable.Rows[intRowCnt][5].ToString(), rawTable.Rows[intRowCnt][6].ToString(),
                             rawTable.Rows[intRowCnt][7].ToString(), rawTable.Rows[intRowCnt][8].ToString());

            }

        }
        return dtProcessedData;
    }

    public void CADEtailsAddRow(DataTable dt, string DATA1, string DATA2, string DATA3, string DATA4, string DATA5, string DATA6, string DATA7, string DATA8, string DATA9)
    {
        DataRow dr = dt.NewRow();
        dr["CONTRACT_ACCOUNT_NUMBER"] = DATA1;
        dr["BUSINESS_PARTNER_NAME"] = DATA2;
        dr["ADDRESS"] = DATA3;
        dr["TELEPHONE"] = DATA4;
        dr["MOBILE_NO"] = DATA5;
        dr["E_MAIL"] = DATA6;
        dr["STREET4"] = DATA7;
        dr["PARTNER_TYPE"] = DATA8;
        dr["LEGAL_ORG"] = DATA9.PadLeft(4,'0');
        dt.Rows.Add(dr);

    }

}