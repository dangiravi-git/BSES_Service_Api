using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;
using System.Data;

/// <summary>
/// Summary description for ZBAPI_DSS_SO
/// </summary>
public class ZBAPI_DSS_SO
{
	public ZBAPI_DSS_SO()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public class I_NEWCONNECTION
    {
        public string APPLIEDCAT;
        public string APPLIEDLOAD;
        public string CONNECTIONTYPE;
        public string FLAG;
        public string NNUMBER;
        public string ORDER_TYPE;
        public string ORDERID;
        public string PLANNINGPLANT;
        public string PMACTIVITYTYPE;
        public string REQUESTNUM;
        public string SHORTTEXT;
        public string SYSTEMCOND;
        public string WORKCENTRE;        
    }

    public class I_BUSINESSPARTNER
    {
        public string BUILDING;
        public string CITY;
        public string E_MAIL;
        public string FATHERSNAME;
        public string FEMALE;
        public string FIRSTNAME;
        public string HOUSE_NO;
        public string IDNUMBER;
        public string JOBGR;
        public string LANDLINE;
        public string LASTNAME;
        public string LEGITTYPE;
        public string MALE;
        public string MIDDLENAME;
        public string MOBILE;
        public string PARTNERCATEGORY;
        public string PARTNERTYPE;
        public string POSTL_COD1;
        public string STR_SUPPL1;
        public string STR_SUPPL2;
        public string STR_SUPPL3;
        public string TITLE_KEY;        
    }


    public DataTable makeFlagsTable()
    {
        DataTable dtFlags = new DataTable("outputFlagsTable");
        DataRow dr = dtFlags.NewRow();
        DataColumn dtCol1 = new DataColumn("E_Flag_Bp", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("E_Flag_So", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("E_New_Partner", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("E_Service_Order", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol4);
        return dtFlags;
    }

    public void pushFlagsInDataTable(DataTable dt, string E_Flag_Bp, string E_Flag_So, string E_New_Partner, string E_Service_Order)
    {
        DataRow dr = dt.NewRow();
        dr["E_Flag_Bp"] = E_Flag_Bp;
        dr["E_Flag_So"] = E_Flag_So;
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