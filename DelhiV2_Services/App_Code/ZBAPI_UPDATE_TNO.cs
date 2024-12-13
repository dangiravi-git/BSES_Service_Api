using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ZBAPI_UPDATE_TNO
/// </summary>
public class ZBAPI_UPDATE_TNO
{
	public ZBAPI_UPDATE_TNO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable CreateOutputDataTable(string _gCA_NO, string _gMOBILE_NO, string _gEMAILADDRESS, string _gLANDMARKADDRESS, string _gLAND_FLAG, string _gMOBILE_FLAG, string _gEMAIL_FLAG, string _gLANDMARK_FLAG, string _gTELVALID_FLAG, string _gMOBLAVID_FLAG, string _gDISPATCH_CTRL)
    {
        DataTable dtOPData = new DataTable("OutputTable");
        DataRow dr = dtOPData.NewRow();

        DataColumn dtCol1 = new DataColumn("CA_NO", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol1);

        DataColumn dtCol2 = new DataColumn("MOBILE_NO", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol2);

        DataColumn dtCol3 = new DataColumn("EMAILADDRESS", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol3);

        DataColumn dtCol4 = new DataColumn("LANDMARKADDRESS", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol4);

        DataColumn dtCol5 = new DataColumn("LAND_FLAG", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol5);

        DataColumn dtCol6 = new DataColumn("MOBILE_FLAG", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol6);

        DataColumn dtCol7 = new DataColumn("EMAIL_FLAG", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol7);

        DataColumn dtCol8 = new DataColumn("LANDMARK_FLAG", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol8);

        DataColumn dtCol9 = new DataColumn("TELVALID_FLAG", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol9);

        DataColumn dtCol10 = new DataColumn("MOBLAVID_FLAG", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol10);

        DataColumn dtCol11 = new DataColumn("DISPATCH_CTRL", System.Type.GetType("System.String"));
        dtOPData.Columns.Add(dtCol11);

        DataRow dr1 = dtOPData.NewRow();
        dr1["CA_NO"] = _gCA_NO;
        dr1["MOBILE_NO"] = _gMOBILE_NO;
        dr1["EMAILADDRESS"] = _gEMAILADDRESS;
        dr1["LANDMARKADDRESS"] = _gLANDMARKADDRESS;
        dr1["LAND_FLAG"] = _gLAND_FLAG;
        dr1["MOBILE_FLAG"] = _gMOBILE_FLAG;
        dr1["EMAIL_FLAG"] = _gEMAIL_FLAG;
        dr1["LANDMARK_FLAG"] = _gLANDMARK_FLAG;
        dr1["TELVALID_FLAG"] = _gTELVALID_FLAG;
        dr1["MOBLAVID_FLAG"] = _gMOBLAVID_FLAG;
        dr1["DISPATCH_CTRL"] = _gDISPATCH_CTRL;
        dtOPData.Rows.Add(dr1);

        return dtOPData;
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
}