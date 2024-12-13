using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

class ZBAPI_SEND_URL
{
    public DataTable makeTable()
    {
        DataTable dtFlags = new DataTable("outputFlagsTable");
        DataRow dr = dtFlags.NewRow();
        DataColumn dtCol1 = new DataColumn("BNAME", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("MAIL", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("URL", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol3);
        DataColumn dtCol4 = new DataColumn("MESSAGE", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol4);
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


    public void pushOutputDataInDataTable(DataTable dt, string _BNAME, string _MAIL, string _URL, string _MESSAGE)
    {
        DataRow dr = dt.NewRow();
        dr["BNAME"] = _BNAME;
        dr["MAIL"] = _MAIL;
        dr["URL"] = _URL;
        dr["MESSAGE"] = _MESSAGE;
        dt.Rows.Add(dr);
    }
}
