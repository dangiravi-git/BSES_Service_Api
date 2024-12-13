using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

class ZBAPI_SEND_EMAIL_ID
{
    public DataTable makeTable()
    {
        DataTable dtFlags = new DataTable("outputFlagsTable");
        DataRow dr = dtFlags.NewRow();
        DataColumn dtCol1 = new DataColumn("SMTP_ADDR", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol1);
        DataColumn dtCol2 = new DataColumn("LOCK_MSG", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol2);
        DataColumn dtCol3 = new DataColumn("STATUS_CODE", System.Type.GetType("System.String"));
        dtFlags.Columns.Add(dtCol3);
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


    public void pushOutputDataInDataTable(DataTable dt, string _SMTP_ADDR, string _LOCK_MSG, string _STATUS_CODE)
    {
        DataRow dr = dt.NewRow();
        dr["SMTP_ADDR"] = _SMTP_ADDR;
        dr["LOCK_MSG"] = _LOCK_MSG;
        dr["STATUS_CODE"] = _STATUS_CODE;
        dt.Rows.Add(dr);
    }

}
