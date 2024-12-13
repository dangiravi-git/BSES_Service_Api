using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;
using System.Data;
using System.Globalization;
using System.Reflection;

/// <summary>
/// Summary description for clsBapiCall
/// </summary>
public class clsBapiCall
{
    static bool destinationIsInialised = false;

    public clsBapiCall()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /* ZBAPI_DISPLAY_BILL_WEB */
    public DataSet Get_ZBAPI_DISPLAY_BILL_WEB(string _gCa_nO, string _sBillMnth)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_DISPLAY_BILL_WEB _objOutPut = new ZBAPI_DISPLAY_BILL_WEB();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[5];
        DataTable dtBillDetails = new DataTable();
        DataTable dtMeterDetails = new DataTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();


        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination3");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_DISPLAY_BILL_WEB");
                testfn.SetValue("CONTRACT_ACCOUNT", _gCa_nO);
                testfn.SetValue("BILL_MONTH", _sBillMnth);
                testfn.Invoke(dest);

                IRfcTable billDetailsTable = testfn.GetTable("TX_BILLPRINT");
                IRfcTable meterDetailsTable = testfn.GetTable("TX_BILLMETER");
                IRfcTable SAPDATA_ErrorDataTable = testfn.GetTable("RETURN");
                IRfcTable ErrorTable = testfn.GetTable("ZERR");

                dtBillDetails = _objOutPut.converttodotnetatble(billDetailsTable);
                if (dtBillDetails.Rows[0][4].ToString().Trim() == "")
                {
                    dtBillDetails.Rows[0][4] = dtBillDetails.Rows[0]["LASTNAME"].ToString().Trim();
                    dtBillDetails.Rows[0]["LASTNAME"] = "";
                    dtBillDetails.AcceptChanges();
                }
                if ((dtBillDetails.Rows[0][8].ToString().Trim() == "0.00") && (dtBillDetails.Rows[0][9].ToString().Trim() == "0000-00-00"))
                {
                    DateTime DT = Convert.ToDateTime(dtBillDetails.Rows[0][2].ToString());
                    string DueDate = DT.AddDays(15).ToString("yyyyMMdd");
                    dtBillDetails.Rows[0][9] = DueDate;
                    dtBillDetails.AcceptChanges();

                }
                dtMeterDetails = _objOutPut.converttodotnetatble(meterDetailsTable);
                dtRet2Table = _objOutPut.converttodotnetatble(SAPDATA_ErrorDataTable);
                dtErrTable = _objOutPut.converttodotnetatble(ErrorTable);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = _objOutPut.transferBillData(dtBillDetails);
        bapiResult[1] = _objOutPut.transferMeterData(dtMeterDetails);
        bapiResult[2] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);
        bapiResult[3] = _objOutPut.pushDataInErrorTable(dtErrTable);
        bapiResult[4] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);
        dsBAPIOutput.Tables.Add(bapiResult[4]);

        return dsBAPIOutput;
    }

    /* ZBAPI_IVR_CREATESO_ISU */
    public DataSet Get_ZBAPI_IVR_CREATESO_ISU(string _strCANumber, string _strCACrn, string _strCAKNumber, string _strMeterNumber, string _strISUOrder,
                                                            string _strComplaintType, string _strContractNumber, string _strTelephoneNo, string _strDescription)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_IVR_CREATESO_ISU _objOutPut = new ZBAPI_IVR_CREATESO_ISU();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtOutputDetails = new DataTable();
        DataTable dtreturnTable2 = new DataTable();
        DataTable dtErrorTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                

                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_IVR_CREATESO_ISU");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPIIVRCOMP1");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("CA_CRN", _strCACrn);
                articol.SetValue("CA_KNO", _strCAKNumber);
                articol.SetValue("CA_NUMBER", _strCANumber);
                articol.SetValue("COMPLAINT_TYPE", _strComplaintType);
                articol.SetValue("CONTRACT_NO", _strContractNumber);
                articol.SetValue("DESCRIPTION", _strDescription);
                articol.SetValue("ISU_ORDER", _strISUOrder);
                articol.SetValue("METER_NO", _strMeterNumber);
                articol.SetValue("TEL1_NUMBR", _strTelephoneNo);

                testfn.SetValue("IMPORT_COMP", articol);              
                testfn.Invoke(dest);
                

                IRfcTable ErrorMessageTable = testfn.GetTable("BAPIMESSAGE");
                IRfcTable OutputTable = testfn.GetTable("EXPORT_COMP");                
                IRfcTable returnTable2 = testfn.GetTable("RETURN");

                dtOutputDetails = _objOutPut.converttodotnetatble(OutputTable);                
                dtreturnTable2 = _objOutPut.converttodotnetatble(returnTable2);
                dtErrorTable = _objOutPut.converttodotnetatble(ErrorMessageTable);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = _objOutPut.getIVRSOISUFormattedTable(dtOutputDetails);
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtreturnTable2);
        bapiResult[2] = _objOutPut.makeISUERRTable(dtErrorTable);     
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);
        

        return dsBAPIOutput;
    }

    ///* Z_BAPI_CMS_ISU_CA_DISPLAY */
    public DataSet Get_Z_BAPI_CMS_ISU_CA_DISPLAY(string strCANumber, string strSerialNumber, string strConsumerNumber, 
                                                                            string strTelephoneNumber,string strKNumber, string strContractNumber)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        Z_BAPI_CMS_ISU_CA_DISPLAY _objOutPut = new Z_BAPI_CMS_ISU_CA_DISPLAY();      

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[5];

        DataTable dtCADetails = new DataTable();
        DataTable dtCSDetails = new DataTable();
        DataTable dtreturnTable2 = new DataTable();
        DataTable dtErrorTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("Z_BAPI_CMS_ISU_CA_DISPLAY");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPIIVRST");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("CA_NUMBER", strCANumber);
                articol.SetValue("CONTRACT", strContractNumber);
                articol.SetValue("SERIALNO", strSerialNumber);

                testfn.SetValue("IMPORT_CANUMBER", articol);
                testfn.SetValue("IMPORT_TELEPHONE_NO", strTelephoneNumber);
                testfn.SetValue("IMPORT_KNUMBER", strKNumber);
                testfn.SetValue("IMPORT_CRNNUMBER", strConsumerNumber);

                testfn.Invoke(dest);

                IRfcTable ErrorMessageTable = testfn.GetTable("BAPIMESSAGE");
                IRfcTable OutputTable = testfn.GetTable("EXPORT_CADETAILS");
                IRfcTable CSDetailsTable = testfn.GetTable("EXPORT_CSDETAILS");
                IRfcTable returnTable2 = testfn.GetTable("RETURN");

                dtCADetails = _objOutPut.converttodotnetatble(OutputTable);
                dtCSDetails = _objOutPut.converttodotnetatble(CSDetailsTable);
                dtreturnTable2 = _objOutPut.converttodotnetatble(returnTable2);
                dtErrorTable = _objOutPut.converttodotnetatble(ErrorMessageTable);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }


        bapiResult[0] = _objOutPut.getCADetailsFormattedTable(dtCADetails);
        bapiResult[1] = _objOutPut.getCSDetailsFormattedTable(dtCSDetails);
        bapiResult[2] = _objOutPut.makeBAPIRET2TABLE(dtreturnTable2);
        bapiResult[3] = _objOutPut.makeISUERRTable(dtErrorTable);
        bapiResult[4] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);
        dsBAPIOutput.Tables.Add(bapiResult[4]);

        return dsBAPIOutput;
    }

    ///* ZBAPIDOCLIST */
    public DataSet Get_ZBAPIDOCLIST(string _strAufnr, string _strC_001, string _strC_002, string _strC_003, string _strC_004, string _strC_005, string _strC_007, string _strC_008, string _strC_009, string _strC_010,
        string _strC_011, string _strC_012, string _strC_013, string _strC_014, string _strC_015, string _strC_016, string _strC_017, string _strC_018, string _strC_019, string _strC_020, string _strC_021, string _strC_022,
        string _strC_023, string _strC_024, string _strC_025, string _strC_026, string _strC_027, string _strC_028, string _strC_029, string _strC_030, string _strC_031, string _strC_032, string _strC_033, string _strC_034,
        string _strC_035, string _strC_036, string _strC_037, string _strC_038, string _strC_039, string _strC_040, string _strC_041, string _strC_070, string _strR_Cdll, string _strR_Occ, string _strR_Own, string _strZ_Appltype)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPIDOCLIST _objOutPut = new ZBAPIDOCLIST();        

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];

        DataTable dtRet2Table = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();       

        try
        {
            clsConnect cfg = new clsConnect();

            _strAufnr = _strAufnr.PadLeft(12, '0');

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPIDOCLIST");
                testfn.SetValue("AUFNR", _strAufnr);
                testfn.SetValue("C_001", _strC_001);
                testfn.SetValue("C_002", _strC_002);
                testfn.SetValue("C_003", _strC_003);
                testfn.SetValue("C_004", _strC_004);
                testfn.SetValue("C_005", _strC_005);
                testfn.SetValue("C_007", _strC_007);
                testfn.SetValue("C_008", _strC_008);
                testfn.SetValue("C_009", _strC_009);
                testfn.SetValue("C_010", _strC_010);
                testfn.SetValue("C_011", _strC_011);
                testfn.SetValue("C_012", _strC_012);
                testfn.SetValue("C_013", _strC_013);
                testfn.SetValue("C_014", _strC_014);
                testfn.SetValue("C_015", _strC_015);
                testfn.SetValue("C_016", _strC_016);
                testfn.SetValue("C_017", _strC_017);
                testfn.SetValue("C_018", _strC_018);
                testfn.SetValue("C_019", _strC_019);
                testfn.SetValue("C_020", _strC_020);
                testfn.SetValue("C_021", _strC_021);
                testfn.SetValue("C_022", _strC_022);
                testfn.SetValue("C_023", _strC_023);
                testfn.SetValue("C_024", _strC_024);
                testfn.SetValue("C_025", _strC_025);
                testfn.SetValue("C_026", _strC_026);
                testfn.SetValue("C_027", _strC_027);
                testfn.SetValue("C_028", _strC_028);
                testfn.SetValue("C_029", _strC_029);
                testfn.SetValue("C_030", _strC_030);
                testfn.SetValue("C_031", _strC_031);
                testfn.SetValue("C_032", _strC_032);
                testfn.SetValue("C_033", _strC_033);
                testfn.SetValue("C_034", _strC_034);
                testfn.SetValue("C_035", _strC_035);
                testfn.SetValue("C_036", _strC_036);
                testfn.SetValue("C_037", _strC_037);
                testfn.SetValue("C_038", _strC_038);
                testfn.SetValue("C_039", _strC_039);
                testfn.SetValue("C_040", _strC_040);
                testfn.SetValue("C_041", _strC_041);
                testfn.SetValue("C_070", _strC_070);
                testfn.SetValue("R_CDL", _strR_Cdll);
                testfn.SetValue("R_OCC", _strR_Occ);
                testfn.SetValue("R_OWN", _strR_Own);
                testfn.SetValue("Z_APPLTYPE", _strZ_Appltype);

                testfn.Invoke(dest);                

                IRfcStructure bapiTable = testfn.GetStructure("RETURN");
                dtRet2Table = _objOutPut.formDataTableError();

                _objOutPut.addRowToDTError(dtRet2Table, bapiTable.GetString("TYPE"), bapiTable.GetString("ID"), bapiTable.GetString("NUMBER"), bapiTable.GetString("MESSAGE"), bapiTable.GetString("LOG_NO"),
                    bapiTable.GetString("LOG_MSG_NO"), bapiTable.GetString("MESSAGE_V1"), bapiTable.GetString("MESSAGE_V2"), bapiTable.GetString("MESSAGE_V3"), bapiTable.GetString("MESSAGE_V4"), "", "", "", "");


                messageCode = "00";
                messageText = "...";

                //dtRet2Table = _objOutPut.converttodotnetatble(Ret2Table);                
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;      
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);    

        return dsBAPIOutput;
    }


    ///* ZBAPI_CREATESO_POST */
    public DataSet Get_ZBAPI_CREATESO_POST(string _strPMAufart, string _strPlanPlant, string _strRegioGroup, string _strShortText, string _strILA, string _strMFText, string _strUserFieldCH20, 
                        string _StrControkey, string _strSerialNumber, string _strComplaintGroup,string _strCANumber, string _strContract, string _strMFText1)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_CREATESO_POST _objOutPut = new ZBAPI_CREATESO_POST();

        string messageText = "";
        string messageCode = "0";
        string strDoc_No = string.Empty, strNoptif_No = string.Empty, strOrderId = string.Empty;

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtOutputResult = new DataTable();
        DataTable dtreturnTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CREATESO_POST");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPI_CREATESO_POST_IMPORT");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("CONT_ACCT", _strCANumber.Trim());
                articol.SetValue("CONTRACT", _strContract.Trim());
                articol.SetValue("CONTROL_KEY", _StrControkey);
                articol.SetValue("ILA", _strILA);
                articol.SetValue("INV_CMPL_GRP", _strComplaintGroup);
                articol.SetValue("MFTEXT", _strMFText);
                articol.SetValue("MFTEXT1", _strMFText1);
                articol.SetValue("PLANPLANT", _strPlanPlant);
                articol.SetValue("PM_AUFART", _strPMAufart);
                articol.SetValue("REGIOGROUP", _strRegioGroup);
                articol.SetValue("SERIALNO", _strSerialNumber.Trim());
                articol.SetValue("SHORT_TEXT", _strShortText);
                articol.SetValue("USERFIELD_CH20", _strUserFieldCH20);

                testfn.SetValue("IMPORT", articol);               
                testfn.Invoke(dest);


                strDoc_No = (string)testfn.GetValue("DOC_NO");
                strNoptif_No = (string)testfn.GetValue("NOTIF_NO");
                strOrderId = (string)testfn.GetValue("ORDERID");
                IRfcTable returnTable2 = testfn.GetTable("RETURN");

                dtOutputResult = _objOutPut.CreateOutputDataTable(strDoc_No, strNoptif_No, strOrderId);
                dtreturnTable = _objOutPut.converttodotnetatble(returnTable2);                
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }


        bapiResult[0] = dtOutputResult;
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtreturnTable);       
        bapiResult[2] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);     

        return dsBAPIOutput;
    }


    /* ZBAPI_CALERT */
    public DataSet Get_ZBAPI_CALERT(string _strCompanyCode, string _strDate, string _strDivision, string _strUnit)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_CALERT _objOutPut = new ZBAPI_CALERT();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtOutputTable = new DataTable();
        DataTable dtreturnTable2 = new DataTable();
        
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();


        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CALERT");
                testfn.SetValue("COMPANYCODE", _strCompanyCode);
                testfn.SetValue("DATE", _strDate);
                testfn.SetValue("DIVISION", _strDivision);
                testfn.SetValue("UNIT", _strUnit);               
                testfn.Invoke(dest);

                IRfcStructure bapiTable = testfn.GetStructure("RETURN");
                IRfcTable OutputTable = testfn.GetTable("ZBAPI_ALERTDATA");

                dtOutputTable = _objOutPut.converttodotnetatble(OutputTable);

              dtreturnTable2=_objOutPut.CreateOutputDataTable(bapiTable.GetString("TYPE"), bapiTable.GetString("ID"), bapiTable.GetString("NUMBER"), bapiTable.GetString("MESSAGE"), bapiTable.GetString("LOG_NO"),
                    bapiTable.GetString("LOG_MSG_NO"), bapiTable.GetString("MESSAGE_V1"), bapiTable.GetString("MESSAGE_V2"), bapiTable.GetString("MESSAGE_V3"), bapiTable.GetString("MESSAGE_V4"),
                    bapiTable.GetString("PARAMETER"), bapiTable.GetString("ROW"), bapiTable.GetString("FIELD"), bapiTable.GetString("SYSTEM"));

            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = _objOutPut.GetFormattedSALERTTable(dtOutputTable);
        bapiResult[1] = dtreturnTable2;       
        bapiResult[2] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        
        return dsBAPIOutput;
    }

    /* ZBAPI_ONLINE_BILL_PDF */
    //public DataSet Get_ZBAPI_ONLINE_BILL_PDF(string _strCANumber, string _strEBSKNO)
    //{
    //    DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
    //    ZBAPI_ONLINE_BILL_PDF _objOutPut = new ZBAPI_ONLINE_BILL_PDF();

    //    string messageText = "";
    //    string messageCode = "0";

    //    string Contact;
    //    string Flag = "";

    //    DataTable[] bapiResult = new DataTable[2];

    //    DataTable dtOutputTable = new DataTable();
    //    DataTable dtMessageText = _objOutPut.makeMessageTextTable();

    //    try
    //    {
    //        clsConnect cfg = new clsConnect();

    //        if (destinationIsInialised == false)
    //        {
    //            RfcDestinationManager.RegisterDestinationConfiguration(cfg);
    //            destinationIsInialised = true;
    //        }

    //        if (destinationIsInialised == true)
    //        {
    //            if ((_strCANumber.Trim() != "") && (_strCANumber.Length > 3))
    //            {
    //                if (_strCANumber.Substring(3, 1) != "2")
    //                {
    //                    RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
    //                    RfcRepository repo = dest.Repository;
    //                    IRfcFunction testfn = repo.CreateFunction("ZBAPI_ONLINE_BILL_PDF");
    //                    testfn.SetValue("CONT_ACCT", _strCANumber);
    //                    testfn.SetValue("EBS_KNO", _strEBSKNO);

    //                    testfn.Invoke(dest);

    //                    Contact = (string)testfn.GetValue("CONTACT");
    //                    Flag = (string)testfn.GetValue("FLAG");
    //                    IRfcTable OutputTable = testfn.GetTable("PDF");
    //                    IRfcTable returnTable2 = testfn.GetTable("RETURN");

    //                    dtOutputTable = _objOutPut.converttodotnetatble(OutputTable);
    //                }
    //                else
    //                {
    //                    messageText = "Flag value 2 : Bill Not Available";
    //                    messageCode = "2";
    //                }                   
    //            }
    //            else
    //            {
    //                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
    //                RfcRepository repo = dest.Repository;
    //                IRfcFunction testfn = repo.CreateFunction("ZBAPI_ONLINE_BILL_PDF");
    //                testfn.SetValue("CONT_ACCT", _strCANumber);
    //                testfn.SetValue("EBS_KNO", _strEBSKNO);

    //                testfn.Invoke(dest);

    //                Contact = (string)testfn.GetValue("CONTACT");
    //                Flag = (string)testfn.GetValue("FLAG");
    //                IRfcTable OutputTable = testfn.GetTable("PDF");
    //                IRfcTable returnTable2 = testfn.GetTable("RETURN");

    //                dtOutputTable = _objOutPut.converttodotnetatble(OutputTable);
    //            }

    //            if (Flag == "1")
    //            {
    //                messageText = "Flag value 1 : CA Invalid";
    //                messageCode = "1";
    //            }
    //            if (Flag == "2")
    //            {
    //                messageText = "Flag value 2 : Bill Not Available";
    //                messageCode = "2";
    //            }
    //        }

    //    }
    //    catch (RfcCommunicationException ex)
    //    {
    //        messageText = "RfcCommunicationException :" + ex.Message.ToString();
    //        messageCode = "91";
    //    }
    //    catch (RfcLogonException ex)
    //    {
    //        messageText = "RfcLogonException :" + ex.Message.ToString();
    //        messageCode = "92";
    //    }
    //    catch (RfcAbapException ex)
    //    {
    //        messageText = "RfcAbapException :" + ex.Message.ToString();
    //        messageCode = "93";
    //    }
    //    catch (Exception ex)
    //    {
    //        messageText = ex.Message.ToString();
    //        messageCode = "94";
    //    }

    //    if (messageText.Trim() != "")
    //    {
    //        _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
    //    }

    //    bapiResult[0] = _objOutPut.GetFormattedZPDFTable(dtOutputTable);      
    //    bapiResult[1] = dtMessageText;

    //    dsBAPIOutput.Tables.Add(bapiResult[0]);
    //    dsBAPIOutput.Tables.Add(bapiResult[1]);
       
    //    return dsBAPIOutput;
    //}

    /* ZBAPI_DSS_SO */
    public DataSet Get_ZBAPI_DSS_SO(string _PARTNERCATEGORY, string _PARTNERTYPE, string _TITLE_KEY, string _FIRSTNAME, string _LASTNAME, string _MIDDLENAME,
                                string _FATHERSNAME, string _HOUSE_NO, string _BUILDING, string _STR_SUPPL1, string _STR_SUPPL2, string _STR_SUPPL3, string _POSTL_COD1,
                                string _CITY, string _E_MAIL, string _LANDLINE, string _MOBILE, string _FEMALE, string _MALE, string _JOBGR, string _LEGITTYPE,
                                string _IDNUMBER, string _ORDER_TYPE, string _SHORTTEXT, string _PLANNINGPLANT, string _WORKCENTRE, string _SYSTEMCOND,
                                string _PMACTIVITYTYPE, string _REQUESTNUM, string _NNUMBER, string _APPLIEDCAT, string _APPLIEDLOAD, string _CONNECTIONTYPE,
                                string _ORDERID, string _FLAG)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_DSS_SO _objOutPut = new ZBAPI_DSS_SO();
        ZBAPI_DSS_SO.I_BUSINESSPARTNER objBPartner = new ZBAPI_DSS_SO.I_BUSINESSPARTNER();
        ZBAPI_DSS_SO.I_NEWCONNECTION objConType = new ZBAPI_DSS_SO.I_NEWCONNECTION();

        bindObjectsZBAPI_DSS_SO(objBPartner, objConType, _PARTNERCATEGORY, _PARTNERTYPE, _TITLE_KEY, _FIRSTNAME, _LASTNAME, _MIDDLENAME, _FATHERSNAME, _HOUSE_NO,
                                _BUILDING, _STR_SUPPL1, _STR_SUPPL2, _STR_SUPPL3, _POSTL_COD1, _CITY, _E_MAIL, _LANDLINE, _MOBILE, _FEMALE, _MALE, _JOBGR, _LEGITTYPE, 
                                _IDNUMBER, _ORDER_TYPE, _SHORTTEXT, _PLANNINGPLANT, _WORKCENTRE, _SYSTEMCOND, _PMACTIVITYTYPE, _REQUESTNUM, _NNUMBER, _APPLIEDCAT, 
                                _APPLIEDLOAD, _CONNECTIONTYPE, _ORDERID, _FLAG);

        string messageText = "";
        string messageCode = "0";

        string E_Flag_Bp = "", E_Flag_So = "", E_New_Partner = "", E_Service_Order = "";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();        
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_DSS_SO");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPI_DSS_BP");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("BUILDING", objBPartner.BUILDING.Trim());
                articol.SetValue("CITY", objBPartner.CITY.Trim());
                articol.SetValue("E_MAIL", objBPartner.E_MAIL);
                articol.SetValue("FATHERSNAME", objBPartner.FATHERSNAME);
                articol.SetValue("FEMALE", objBPartner.FEMALE);
                articol.SetValue("FIRSTNAME", objBPartner.FIRSTNAME);
                articol.SetValue("HOUSE_NO", objBPartner.HOUSE_NO);
                articol.SetValue("IDNUMBER", objBPartner.IDNUMBER);
                articol.SetValue("JOBGR", objBPartner.JOBGR);
                articol.SetValue("LANDLINE", objBPartner.LANDLINE);
                articol.SetValue("LASTNAME", objBPartner.LASTNAME.Trim());
                //articol.SetValue("LEGITTYPE", objBPartner.LEGITTYPE);
                articol.SetValue("MALE", objBPartner.MALE);
                articol.SetValue("MIDDLENAME", objBPartner.MIDDLENAME);
                articol.SetValue("MOBILE", objBPartner.MOBILE);
                articol.SetValue("PARTNERCATEGORY", objBPartner.PARTNERCATEGORY);
                articol.SetValue("PARTNERTYPE", objBPartner.PARTNERTYPE);
                articol.SetValue("POSTL_COD1", objBPartner.POSTL_COD1);
                articol.SetValue("STR_SUPPL1", objBPartner.STR_SUPPL1);
                articol.SetValue("STR_SUPPL2", objBPartner.STR_SUPPL2);
                articol.SetValue("STR_SUPPL3", objBPartner.STR_SUPPL3);
                articol.SetValue("TITLE_KEY", objBPartner.TITLE_KEY);

                testfn.SetValue("I_BUSINESSPARTNER", articol);

                RfcStructureMetadata amI = repo.GetStructureMetadata("ZBAPI_DSS_NEWCONN");
                IRfcStructure articolI = amI.CreateStructure();
                articolI.SetValue("APPLIEDCAT", objConType.APPLIEDCAT.Trim());
                articolI.SetValue("APPLIEDLOAD", objConType.APPLIEDLOAD.Trim());
                articolI.SetValue("CONNECTIONTYPE", objConType.CONNECTIONTYPE);
                articolI.SetValue("FLAG", objConType.FLAG);
                articolI.SetValue("NNUMBER", objConType.NNUMBER);
                articolI.SetValue("ORDER_TYPE", objConType.ORDER_TYPE);
                articolI.SetValue("ORDERID", objConType.ORDERID);
                articolI.SetValue("PLANNINGPLANT", objConType.PLANNINGPLANT);
                articolI.SetValue("PMACTIVITYTYPE", objConType.PMACTIVITYTYPE);
                articolI.SetValue("REQUESTNUM", objConType.REQUESTNUM);
                articolI.SetValue("SHORTTEXT", objConType.SHORTTEXT.Trim());
                articolI.SetValue("SYSTEMCOND", objConType.SYSTEMCOND);
                articolI.SetValue("WORKCENTRE", objConType.WORKCENTRE);

                testfn.SetValue("I_NEWCONNECTION", articolI);

                testfn.Invoke(dest);

                E_Flag_Bp = (string)testfn.GetValue("E_FLAG_BP");
                E_Flag_So = (string)testfn.GetValue("E_FLAG_SO");
                E_New_Partner = (string)testfn.GetValue("E_NEW_PARTNER");
                E_Service_Order = (string)testfn.GetValue("E_SERVICE_ORDER");                
                IRfcTable returnTable2 = testfn.GetTable("RETURN");
                IRfcTable ErrTable = testfn.GetTable("ZERR");

                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
                dtErrTable = _objOutPut.converttodotnetatble(ErrTable);
               
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        _objOutPut.pushFlagsInDataTable(dtFlagsTable, E_Flag_Bp, E_Flag_So, E_New_Partner, E_Service_Order);

        bapiResult[0] = dtFlagsTable;
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);
        bapiResult[2] = _objOutPut.pushDataInErrorTable(dtErrTable);
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }


    public void bindObjectsZBAPI_DSS_SO(ZBAPI_DSS_SO.I_BUSINESSPARTNER objBPartner, ZBAPI_DSS_SO.I_NEWCONNECTION objConType, string PARTNERCATEGORY, string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, string CITY, string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string LEGITTYPE, string IDNUMBER, string ORDER_TYPE, string SHORTTEXT, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string PMACTIVITYTYPE, string REQUESTNUM, string NNUMBER, string APPLIEDCAT, string APPLIEDLOAD, string CONNECTIONTYPE, string ORDERID, string FLAG)
    {
        try
        {

            objBPartner.PARTNERCATEGORY = PARTNERCATEGORY;
            objBPartner.PARTNERTYPE = PARTNERTYPE;
            objBPartner.TITLE_KEY = TITLE_KEY;
            objBPartner.FIRSTNAME = FIRSTNAME;
            objBPartner.LASTNAME = LASTNAME;
            objBPartner.MIDDLENAME = MIDDLENAME;
            objBPartner.FATHERSNAME = FATHERSNAME;
            objBPartner.HOUSE_NO = HOUSE_NO.Replace("|", ",");
            objBPartner.BUILDING = BUILDING.Replace("|", ",");
            objBPartner.STR_SUPPL1 = STR_SUPPL1.Replace("|", ",");
            objBPartner.STR_SUPPL2 = STR_SUPPL2.Replace("|", ",");
            objBPartner.STR_SUPPL3 = STR_SUPPL3.Replace("|", ",");
            objBPartner.POSTL_COD1 = POSTL_COD1.Replace("|", ",");
            objBPartner.CITY = CITY;
            objBPartner.E_MAIL = E_MAIL;
            objBPartner.LANDLINE = LANDLINE;
            objBPartner.MOBILE = MOBILE;
            objBPartner.FEMALE = FEMALE;
            objBPartner.MALE = MALE;
            objBPartner.JOBGR = JOBGR;
            objBPartner.LEGITTYPE = LEGITTYPE;
            objBPartner.IDNUMBER = IDNUMBER;

            objConType.ORDER_TYPE = ORDER_TYPE;
            objConType.SHORTTEXT = SHORTTEXT.Replace("|", ",");
            objConType.PLANNINGPLANT = PLANNINGPLANT;
            objConType.WORKCENTRE = WORKCENTRE;
            objConType.SYSTEMCOND = SYSTEMCOND;
            objConType.PMACTIVITYTYPE = PMACTIVITYTYPE;
            objConType.REQUESTNUM = REQUESTNUM;
            objConType.NNUMBER = NNUMBER;
            objConType.APPLIEDCAT = APPLIEDCAT;
            objConType.APPLIEDLOAD = APPLIEDLOAD;
            objConType.CONNECTIONTYPE = CONNECTIONTYPE;
            objConType.ORDERID = ORDERID;
            objConType.FLAG = FLAG;

        }
        catch (Exception)
        {

        }

    }


    /* ZBAPI_DSS_SO_ECC */
    public DataSet Get_ZBAPI_DSS_SO_ECC(string _PARTNERCATEGORY, string _PARTNERTYPE, string _TITLE_KEY, string _FIRSTNAME, string _LASTNAME, string _MIDDLENAME,
                                string _FATHERSNAME, string _HOUSE_NO, string _BUILDING, string _STR_SUPPL1, string _STR_SUPPL2, string _STR_SUPPL3, string _POSTL_COD1,
                                string _CITY, string _E_MAIL, string _LANDLINE, string _MOBILE, string _FEMALE, string _MALE, string _JOBGR, string _IDTYPE,
                                string _IDNUMBER, string _ORDER_TYPE, string _SHORTTEXT, string _PLANNINGPLANT, string _WORKCENTRE, string _SYSTEMCOND,
                                string _PMACTIVITYTYPE, string _REQUESTNUM, string _NNUMBER, string _APPLIEDCAT, string _APPLIEDLOAD, string _CONNECTIONTYPE,
                                string _ORDERID, string _FLAG)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_DSS_SO_ECC _objOutPut = new ZBAPI_DSS_SO_ECC();
        ZBAPI_DSS_SO_ECC.I_BUSINESSPARTNER objBPartner = new ZBAPI_DSS_SO_ECC.I_BUSINESSPARTNER();
        ZBAPI_DSS_SO_ECC.I_NEWCONNECTION objConType = new ZBAPI_DSS_SO_ECC.I_NEWCONNECTION();

        bindObjectsZBAPI_DSS_SO_ECC(objBPartner, objConType, _PARTNERCATEGORY, _PARTNERTYPE, _TITLE_KEY, _FIRSTNAME, _LASTNAME, _MIDDLENAME, _FATHERSNAME, _HOUSE_NO,
                                _BUILDING, _STR_SUPPL1, _STR_SUPPL2, _STR_SUPPL3, _POSTL_COD1, _CITY, _E_MAIL, _LANDLINE, _MOBILE, _FEMALE, _MALE, _JOBGR, _IDTYPE,
                                _IDNUMBER, _ORDER_TYPE, _SHORTTEXT, _PLANNINGPLANT, _WORKCENTRE, _SYSTEMCOND, _PMACTIVITYTYPE, _REQUESTNUM, _NNUMBER, _APPLIEDCAT,
                                _APPLIEDLOAD, _CONNECTIONTYPE, _ORDERID, _FLAG);

        string messageText = "";
        string messageCode = "0";

        string E_Flag_Bp = "", E_Flag_So = "", E_New_Partner = "", E_Service_Order = "";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_DSS_SO");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPI_DSS_BP");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("BUILDING", objBPartner.BUILDING.Trim());
                articol.SetValue("CITY", objBPartner.CITY.Trim());
                articol.SetValue("E_MAIL", objBPartner.E_MAIL);
                articol.SetValue("FATHERSNAME", objBPartner.FATHERSNAME);
                articol.SetValue("FEMALE", objBPartner.FEMALE);
                articol.SetValue("FIRSTNAME", objBPartner.FIRSTNAME);
                articol.SetValue("HOUSE_NO", objBPartner.HOUSE_NO);
                articol.SetValue("IDNUMBER", objBPartner.IDNUMBER);
                articol.SetValue("JOBGR", objBPartner.JOBGR);
                articol.SetValue("LANDLINE", objBPartner.LANDLINE);
                articol.SetValue("LASTNAME", objBPartner.LASTNAME.Trim());
                articol.SetValue("IDTYPE", objBPartner.IDTYPE);
                articol.SetValue("MALE", objBPartner.MALE);
                articol.SetValue("MIDDLENAME", objBPartner.MIDDLENAME);
                articol.SetValue("MOBILE", objBPartner.MOBILE);
                articol.SetValue("PARTNERCATEGORY", objBPartner.PARTNERCATEGORY);
                articol.SetValue("PARTNERTYPE", objBPartner.PARTNERTYPE);
                articol.SetValue("POSTL_COD1", objBPartner.POSTL_COD1);
                articol.SetValue("STR_SUPPL1", objBPartner.STR_SUPPL1);
                articol.SetValue("STR_SUPPL2", objBPartner.STR_SUPPL2);
                articol.SetValue("STR_SUPPL3", objBPartner.STR_SUPPL3);
                articol.SetValue("TITLE_KEY", objBPartner.TITLE_KEY);

                testfn.SetValue("I_BUSINESSPARTNER", articol);

                RfcStructureMetadata amI = repo.GetStructureMetadata("ZBAPI_DSS_NEWCONN");
                IRfcStructure articolI = amI.CreateStructure();
                articolI.SetValue("APPLIEDCAT", objConType.APPLIEDCAT.Trim());
                articolI.SetValue("APPLIEDLOAD", objConType.APPLIEDLOAD.Trim());
                articolI.SetValue("CONNECTIONTYPE", objConType.CONNECTIONTYPE);
                articolI.SetValue("FLAG", objConType.FLAG);
                articolI.SetValue("NNUMBER", objConType.NNUMBER);
                articolI.SetValue("ORDER_TYPE", objConType.ORDER_TYPE);
                articolI.SetValue("ORDERID", objConType.ORDERID);
                articolI.SetValue("PLANNINGPLANT", objConType.PLANNINGPLANT);
                articolI.SetValue("PMACTIVITYTYPE", objConType.PMACTIVITYTYPE);
                articolI.SetValue("REQUESTNUM", objConType.REQUESTNUM);
                articolI.SetValue("SHORTTEXT", objConType.SHORTTEXT.Trim());
                articolI.SetValue("SYSTEMCOND", objConType.SYSTEMCOND);
                articolI.SetValue("WORKCENTRE", objConType.WORKCENTRE);

                testfn.SetValue("I_NEWCONNECTION", articolI);

                testfn.Invoke(dest);

                E_Flag_Bp = (string)testfn.GetValue("E_FLAG_BP");
                E_Flag_So = (string)testfn.GetValue("E_FLAG_SO");
                E_New_Partner = (string)testfn.GetValue("E_NEW_PARTNER");
                E_Service_Order = (string)testfn.GetValue("E_SERVICE_ORDER");

                IRfcTable returnTable2 = testfn.GetTable("RETURN");
                IRfcTable ErrTable = testfn.GetTable("ZERR");

                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
                dtErrTable = _objOutPut.converttodotnetatble(ErrTable);

            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        _objOutPut.pushFlagsInDataTable(dtFlagsTable, E_Flag_Bp, E_Flag_So, E_New_Partner, E_Service_Order);

        bapiResult[0] = dtFlagsTable;
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);
        bapiResult[2] = _objOutPut.pushDataInErrorTable(dtErrTable);
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }


    public void bindObjectsZBAPI_DSS_SO_ECC(ZBAPI_DSS_SO_ECC.I_BUSINESSPARTNER objBPartner, ZBAPI_DSS_SO_ECC.I_NEWCONNECTION objConType, string PARTNERCATEGORY, string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, string CITY, string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string IDTYPE, string IDNUMBER, string ORDER_TYPE, string SHORTTEXT, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string PMACTIVITYTYPE, string REQUESTNUM, string NNUMBER, string APPLIEDCAT, string APPLIEDLOAD, string CONNECTIONTYPE, string ORDERID, string FLAG)
    {
        try
        {

            objBPartner.PARTNERCATEGORY = PARTNERCATEGORY;
            objBPartner.PARTNERTYPE = PARTNERTYPE;
            objBPartner.TITLE_KEY = TITLE_KEY;
            objBPartner.FIRSTNAME = FIRSTNAME;
            objBPartner.LASTNAME = LASTNAME;
            objBPartner.MIDDLENAME = MIDDLENAME;
            objBPartner.FATHERSNAME = FATHERSNAME;
            objBPartner.HOUSE_NO = HOUSE_NO.Replace("|", ",");
            objBPartner.BUILDING = BUILDING.Replace("|", ",");
            objBPartner.STR_SUPPL1 = STR_SUPPL1.Replace("|", ",");
            objBPartner.STR_SUPPL2 = STR_SUPPL2.Replace("|", ",");
            objBPartner.STR_SUPPL3 = STR_SUPPL3.Replace("|", ",");
            objBPartner.POSTL_COD1 = POSTL_COD1.Replace("|", ",");
            objBPartner.CITY = CITY;
            objBPartner.E_MAIL = E_MAIL;
            objBPartner.LANDLINE = LANDLINE;
            objBPartner.MOBILE = MOBILE;
            objBPartner.FEMALE = FEMALE;
            objBPartner.MALE = MALE;
            objBPartner.JOBGR = JOBGR;
            objBPartner.IDTYPE = IDTYPE;
            objBPartner.IDNUMBER = IDNUMBER;

            objConType.ORDER_TYPE = ORDER_TYPE;
            objConType.SHORTTEXT = SHORTTEXT.Replace("|", ",");
            objConType.PLANNINGPLANT = PLANNINGPLANT;
            objConType.WORKCENTRE = WORKCENTRE;
            objConType.SYSTEMCOND = SYSTEMCOND;
            objConType.PMACTIVITYTYPE = PMACTIVITYTYPE;
            objConType.REQUESTNUM = REQUESTNUM;
            objConType.NNUMBER = NNUMBER;
            objConType.APPLIEDCAT = APPLIEDCAT;
            objConType.APPLIEDLOAD = APPLIEDLOAD;
            objConType.CONNECTIONTYPE = CONNECTIONTYPE;
            objConType.ORDERID = ORDERID;
            objConType.FLAG = FLAG;

        }
        catch (Exception)
        {

        }

    }


    /* Z_BAPI_ZDSS_WEB_LINK */
    public DataSet Get_Z_BAPI_ZDSS_WEB_LINK(string _I_ILART, string _I_VKONT, string _I_VKONA, string _PARTNERCATEGORY, string _PARTNERTYPE, string _TITLE_KEY, string _FIRSTNAME,
                                    string _LASTNAME, string _MIDDLENAME, string _FATHERSNAME, string _HOUSE_NO, string _BUILDING, string _STR_SUPPL1, string _STR_SUPPL2,
                                    string _STR_SUPPL3, string _POSTL_COD1, string _CITY, string _E_MAIL, string _LANDLINE, string _MOBILE, string _FEMALE, string _MALE,
                                    string _JOBGR, string _IDTYPE, string _IDNUMBER, string _PLANNINGPLANT, string _WORKCENTRE, string _SYSTEMCOND, string _APPLIEDCAT,
                                    string _APPLIEDLOAD, string _APPLIEDLOADKVA, string _CONNECTIONTYPE, string _STATEMENT_CA, string _START_DATE, string _START_TIME,
                                    string _FINISH_DATE, string _FINISH_TIME, string _SORTFIELD, string _ABKRS)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        Z_BAPI_ZDSS_WEB_LINK _objOutPut = new Z_BAPI_ZDSS_WEB_LINK();
        Z_BAPI_ZDSS_WEB_LINK.I_BUSINESSPARTNER_ECC objBPartner = new Z_BAPI_ZDSS_WEB_LINK.I_BUSINESSPARTNER_ECC();
        Z_BAPI_ZDSS_WEB_LINK.I_DSSORDER objIdssOrder = new Z_BAPI_ZDSS_WEB_LINK.I_DSSORDER();

        bindObjectsZ_BAPI_ZDSS_WEB_LINK(objBPartner, objIdssOrder, _PARTNERCATEGORY, _PARTNERTYPE, _TITLE_KEY, _FIRSTNAME, _LASTNAME, _MIDDLENAME, _FATHERSNAME, _HOUSE_NO, 
                                    _BUILDING, _STR_SUPPL1, _STR_SUPPL2, _STR_SUPPL3, _POSTL_COD1, _CITY, _E_MAIL, _LANDLINE, _MOBILE, _FEMALE, _MALE, _JOBGR, _IDTYPE, 
                                    _IDNUMBER, _PLANNINGPLANT, _WORKCENTRE, _SYSTEMCOND, _APPLIEDCAT, _APPLIEDLOAD, _APPLIEDLOADKVA, _CONNECTIONTYPE, _STATEMENT_CA, 
                                    _START_DATE, _START_TIME, _FINISH_DATE, _FINISH_TIME, _SORTFIELD, _ABKRS);

        string messageText = "";
        string messageCode = "0";

        string E_Flag_Ap = "", E_Flag_Bp = "", E_Flag_So = "", E_Flag_Us = "", E_New_Partner = "", E_Service_Order = "";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();        
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("Z_BAPI_ZDSS_WEB_LINK");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPI_DSS_BP");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("BUILDING", objBPartner.BUILDING.Trim());
                articol.SetValue("CITY", objBPartner.CITY.Trim());
                articol.SetValue("E_MAIL", objBPartner.E_MAIL);
                articol.SetValue("FATHERSNAME", objBPartner.FATHERSNAME);
                articol.SetValue("FEMALE", objBPartner.FEMALE);
                articol.SetValue("FIRSTNAME", objBPartner.FIRSTNAME);
                articol.SetValue("HOUSE_NO", objBPartner.HOUSE_NO);
                articol.SetValue("IDNUMBER", objBPartner.IDNUMBER);
                articol.SetValue("JOBGR", objBPartner.JOBGR);
                articol.SetValue("LANDLINE", objBPartner.LANDLINE);
                articol.SetValue("LASTNAME", objBPartner.LASTNAME.Trim());
                articol.SetValue("IDTYPE", objBPartner.IDTYPE);
                articol.SetValue("MALE", objBPartner.MALE);
                articol.SetValue("MIDDLENAME", objBPartner.MIDDLENAME);
                articol.SetValue("MOBILE", objBPartner.MOBILE);
                articol.SetValue("PARTNERCATEGORY", objBPartner.PARTNERCATEGORY);
                articol.SetValue("PARTNERTYPE", objBPartner.PARTNERTYPE);
                articol.SetValue("POSTL_COD1", objBPartner.POSTL_COD1);
                articol.SetValue("STR_SUPPL1", objBPartner.STR_SUPPL1);
                articol.SetValue("STR_SUPPL2", objBPartner.STR_SUPPL2);
                articol.SetValue("STR_SUPPL3", objBPartner.STR_SUPPL3);
                articol.SetValue("TITLE_KEY", objBPartner.TITLE_KEY);

                testfn.SetValue("I_BUSINESSPARTNER", articol);

                RfcStructureMetadata amI = repo.GetStructureMetadata("ZBAPI_DSS_ORDER");
                IRfcStructure articolI = amI.CreateStructure();
                articolI.SetValue("ABKRS", objIdssOrder.ABKRS.Trim());
                articolI.SetValue("APPLIEDCAT", objIdssOrder.APPLIEDCAT.Trim());
                articolI.SetValue("APPLIEDLOAD", objIdssOrder.APPLIEDLOAD);
                articolI.SetValue("APPLIEDLOADKVA", objIdssOrder.APPLIEDLOADKVA);
                articolI.SetValue("CONNECTIONTYPE", objIdssOrder.CONNECTIONTYPE);
                articolI.SetValue("FINISH_DATE", objIdssOrder.FINISH_DATE);
                articolI.SetValue("FINISH_TIME", objIdssOrder.FINISH_TIME);
                articolI.SetValue("PLANNINGPLANT", objIdssOrder.PLANNINGPLANT);
                articolI.SetValue("SORTFIELD", objIdssOrder.SORTFIELD);
                articolI.SetValue("START_DATE", objIdssOrder.START_DATE);
                articolI.SetValue("START_TIME", objIdssOrder.START_TIME.Trim());
                articolI.SetValue("STATEMENT_CA", objIdssOrder.STATEMENT_CA);
                articolI.SetValue("SYSTEMCOND", objIdssOrder.SYSTEMCOND);
                articolI.SetValue("WORKCENTRE", objIdssOrder.WORKCENTRE);

                testfn.SetValue("I_DSSORDER", articolI);

                testfn.SetValue("I_ILART", _I_ILART);
                testfn.SetValue("I_VKONA", _I_VKONA);
                testfn.SetValue("I_VKONT", _I_VKONT);

                testfn.Invoke(dest);

                E_Flag_Ap = (string)testfn.GetValue("E_FLAG_AP");
                E_Flag_Bp = (string)testfn.GetValue("E_FLAG_BP");
                E_Flag_So = (string)testfn.GetValue("E_FLAG_SO");
                E_Flag_Us = (string)testfn.GetValue("E_FLAG_US");
                E_New_Partner = (string)testfn.GetValue("E_NEW_PARTNER");
                E_Service_Order = (string)testfn.GetValue("E_SERVICE_ORDER");
                IRfcTable returnTable2 = testfn.GetTable("RETURN");
                IRfcTable ErrTable = testfn.GetTable("ZERR");

                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
                dtErrTable = _objOutPut.converttodotnetatble(ErrTable);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }
        
        _objOutPut.pushFlagsInDataTable(dtFlagsTable, E_Flag_Ap, E_Flag_Bp, E_Flag_So, E_Flag_Us, E_New_Partner, E_Service_Order);

        bapiResult[0] = dtFlagsTable;
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);
        bapiResult[2] = _objOutPut.pushDataInErrorTable(dtErrTable);
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }

    public void bindObjectsZ_BAPI_ZDSS_WEB_LINK(Z_BAPI_ZDSS_WEB_LINK.I_BUSINESSPARTNER_ECC objBPartner, Z_BAPI_ZDSS_WEB_LINK.I_DSSORDER objDSSOrder, string PARTNERCATEGORY, string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, string CITY, string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string IDTYPE, string IDNUMBER, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string APPLIEDCAT, string APPLIEDLOAD, string APPLIEDLOADKVA, string CONNECTIONTYPE, string STATEMENT_CA, string START_DATE, string START_TIME, string FINISH_DATE, string FINISH_TIME, string SORTFIELD, string ABKRS)
    {
        try
        {

            objBPartner.PARTNERCATEGORY = PARTNERCATEGORY;
            objBPartner.PARTNERTYPE = PARTNERTYPE;
            objBPartner.TITLE_KEY = TITLE_KEY;
            objBPartner.FIRSTNAME = FIRSTNAME;
            objBPartner.LASTNAME = LASTNAME;
            objBPartner.MIDDLENAME = MIDDLENAME;
            objBPartner.FATHERSNAME = FATHERSNAME;
            objBPartner.HOUSE_NO = HOUSE_NO.Replace("|", ",");
            objBPartner.BUILDING = BUILDING.Replace("|", ",");
            objBPartner.STR_SUPPL1 = STR_SUPPL1.Replace("|", ",");
            objBPartner.STR_SUPPL2 = STR_SUPPL2.Replace("|", ",");
            objBPartner.STR_SUPPL3 = STR_SUPPL3.Replace("|", ",");
            objBPartner.POSTL_COD1 = POSTL_COD1.Replace("|", ",");
            objBPartner.CITY = CITY;
            objBPartner.E_MAIL = E_MAIL;
            objBPartner.LANDLINE = LANDLINE;
            objBPartner.MOBILE = MOBILE;
            objBPartner.FEMALE = FEMALE;
            objBPartner.MALE = MALE;
            objBPartner.JOBGR = JOBGR;
            objBPartner.IDTYPE = IDTYPE;
            objBPartner.IDNUMBER = IDNUMBER;


            objDSSOrder.PLANNINGPLANT = PLANNINGPLANT;
            objDSSOrder.WORKCENTRE = WORKCENTRE;
            objDSSOrder.SYSTEMCOND = SYSTEMCOND;
            objDSSOrder.APPLIEDCAT = APPLIEDCAT;
            objDSSOrder.APPLIEDLOAD = APPLIEDLOAD;
            objDSSOrder.APPLIEDLOADKVA = APPLIEDLOADKVA;
            objDSSOrder.CONNECTIONTYPE = CONNECTIONTYPE;
            objDSSOrder.STATEMENT_CA = STATEMENT_CA;
            objDSSOrder.START_DATE = START_DATE;
            objDSSOrder.START_TIME = START_TIME;
            objDSSOrder.FINISH_DATE = FINISH_DATE;
            objDSSOrder.FINISH_TIME = FINISH_TIME;
            objDSSOrder.SORTFIELD = SORTFIELD;
            objDSSOrder.ABKRS = ABKRS;

        }
        catch (Exception)
        {

        }

    }


    /* ZBAPI_ENFORCEMENT */
    public DataSet Get_ZBAPI_ENFORCEMENT(string _strCANumber, string _strContract)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_ENFORCEMENT _objOutPut = new ZBAPI_ENFORCEMENT();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtEnfDetails = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();
        DataTable dtMetChangeTable = new DataTable();
        
        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_ENFORCEMENT");
                testfn.SetValue("CA", _strCANumber);
                testfn.SetValue("CONTRACT", _strContract);
             
                testfn.Invoke(dest);

                IRfcStructure bapiTable = testfn.GetStructure("RETURN");
                IRfcTable EnfDetails = testfn.GetTable("IT_FINAL");
                IRfcTable MetChangeTable = testfn.GetTable("IT_FINAL_MC");
                

                dtEnfDetails = _objOutPut.converttodotnetatble(EnfDetails);
                dtMetChangeTable = _objOutPut.converttodotnetatble(MetChangeTable);

                if (bapiTable.GetString("MESSAGE").Trim() != "")
				{
					messageCode = "0";
                    messageText = bapiTable.GetString("MESSAGE").Trim();
				}
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = _objOutPut.transferEnforcementData(dtEnfDetails);
        bapiResult[1] = _objOutPut.transferEnfMeterChangeData(dtMetChangeTable);       
        bapiResult[2] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
       
        return dsBAPIOutput;
    }


    /* ZBI_WEBBILL_HIST */
    public DataSet Get_ZBI_WEBBILL_HIST(string _strCANumber, string _strBillMonth)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBI_WEBBILL_HIST _objOutPut = new ZBI_WEBBILL_HIST();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtWebBillHistory = new DataTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();        
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();        

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBI_WEBBILL_HIST");
                testfn.SetValue("BILL_MONTH", _strBillMonth);
                testfn.SetValue("CONTRACT_ACCOUNT", _strCANumber);

                testfn.Invoke(dest);

                IRfcTable Ret2Table = testfn.GetTable("RETURN");
                IRfcTable WebBillHistory = testfn.GetTable("TX_BILLPRINT1");
                IRfcTable ErrTable = testfn.GetTable("ZERR");


                dtRet2Table = _objOutPut.converttodotnetatble(Ret2Table);
                dtWebBillHistory = _objOutPut.converttodotnetatble(WebBillHistory);
                dtErrTable = _objOutPut.converttodotnetatble(ErrTable);

                
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = _objOutPut.transferWebBillHistoryData(dtWebBillHistory);
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);
        bapiResult[2] = _objOutPut.pushDataInErrorTable(dtErrTable);
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }


    /*
     * Z_BAPI_IVRS
     */
    public DataSet Get_Z_BAPI_IVRS(string strContractAccountNumber)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        Z_BAPI_IVRS _objOutPut = new Z_BAPI_IVRS();

        string messageText = "";
        string messageCode = "0";

        string CURR_DUE_DATE = "", PREV_DUE_DATE = "", LAST_PAID_DATE = "";
        string strCURR_BILL_AMNT = "0", strPREV_BILL_AMNT = "0", strLAST_PAID_AMNT = "0";

        decimal CURR_BILL_AMNT = 0, PREV_BILL_AMNT = 0, LAST_PAID_AMNT = 0;

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtBapiOutput = _objOutPut.makeOutputDataTable();
        DataTable dtRet2Table = _objOutPut.formDataTableError();
        DataTable dtSAPErrorTable = _objOutPut.makeErrorTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("Z_BAPI_IVRS");
                testfn.SetValue("CONTRACT_ACCOUNT", strContractAccountNumber);
                testfn.Invoke(dest);

                strCURR_BILL_AMNT = (string)testfn.GetValue("CURR_BILL_AMNT");
                CURR_DUE_DATE = (string)testfn.GetValue("CURR_DUE_DATE");
                strLAST_PAID_AMNT = (string)testfn.GetValue("LAST_PAID_AMNT");
                LAST_PAID_DATE = (string)testfn.GetValue("LAST_PAID_DATE");
                strPREV_BILL_AMNT = (string)testfn.GetValue("PREV_BILL_AMNT");
                PREV_DUE_DATE = (string)testfn.GetValue("PREV_DUE_DATE");

                try
                {
                    CURR_BILL_AMNT = Convert.ToDecimal(strCURR_BILL_AMNT);
                }
                catch (Exception e)
                {
                    CURR_BILL_AMNT = 0;
                }

                try
                {
                    LAST_PAID_AMNT = Convert.ToDecimal(strLAST_PAID_AMNT);
                }
                catch (Exception e)
                {
                    LAST_PAID_AMNT = 0;
                }

                try
                {
                    PREV_BILL_AMNT = Convert.ToDecimal(strPREV_BILL_AMNT);
                }
                catch (Exception e)
                {
                    PREV_BILL_AMNT = 0;
                }

                IRfcTable rfcTableRETURN = testfn.GetTable("RETURN");
                IRfcTable rfcTableZERR = testfn.GetTable("ZERR");

                _objOutPut.pushOutputDataInDataTable(dtBapiOutput, CURR_BILL_AMNT, _objOutPut.GetDateFormate_YYYYMMDD(CURR_DUE_DATE), PREV_BILL_AMNT,
                                                    _objOutPut.GetDateFormate_YYYYMMDD(PREV_DUE_DATE), LAST_PAID_AMNT, _objOutPut.GetDateFormate_YYYYMMDD(LAST_PAID_DATE));
                               

                dtRet2Table = _objOutPut.converttodotnetatble(rfcTableRETURN);
                dtRet2Table = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);

                dtSAPErrorTable = _objOutPut.converttodotnetatble(rfcTableZERR);
                dtSAPErrorTable = _objOutPut.makeSAPErrorTable(dtSAPErrorTable);
                //_objOutPut.pushDataInSAPErrorTable(dtSAPErrorTable, dtRet2Table[0]["MESSAGE"].ToString());
                //dtSAPErrorTable.TableName = "SAP_ERROR_TABLE";             

                messageCode = "00";
                messageText = "...";
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }



        //bapiResult[0] = dtBapiOutput;
        //bapiResult[1] = _objOutPut.populateStructureWithData(dtSAPErrorTable, );
        //bapiResult[2] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);
        //bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(dtBapiOutput);
        dsBAPIOutput.Tables.Add(dtSAPErrorTable);
        dsBAPIOutput.Tables.Add(dtRet2Table);
        dsBAPIOutput.Tables.Add(dtMessageText);

        return dsBAPIOutput;
    }


    /* Z_BAPI_DSS_ISU_CA_DISPLAY */
    public DataSet Get_Z_BAPI_DSS_ISU_CA_DISPLAY(string strCANumber, string strCRNNumber)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        Z_BAPI_DSS_ISU_CA_DISPLAY _objOutPut = new Z_BAPI_DSS_ISU_CA_DISPLAY();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtCADetails = _objOutPut.makeCADetailsDataTable();
        DataTable dtreturnTable2 = new DataTable();
        DataTable dtErrorTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("Z_BAPI_DSS_ISU_CA_DISPLAY");
                testfn.SetValue("IMPORT_CANUMBER", strCANumber);
                testfn.SetValue("IMPORT_CRNNUMBER", strCRNNumber);

                testfn.Invoke(dest);

                IRfcTable irfcErrorTable = testfn.GetTable("BAPIMESSAGE");
                IRfcTable irfcCADetails = testfn.GetTable("EXPORT_CADETAILS");
                IRfcTable irfcReturnTable2 = testfn.GetTable("RETURN");


                dtCADetails = _objOutPut.converttodotnetatble(irfcCADetails);
                dtCADetails = _objOutPut.makeCADEtails(dtCADetails);

                dtreturnTable2 = _objOutPut.converttodotnetatble(irfcReturnTable2);
                dtErrorTable = _objOutPut.converttodotnetatble(irfcErrorTable);

            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtCADetails;
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtreturnTable2);
        bapiResult[2] = _objOutPut.makeISUERRTable(dtErrorTable);
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }


    public DataSet get_ZBAPI_FICA_PREPAID_MTR(string strDOC_ID, string strTRANS_ID, string strCA, string strCOMPANY,
    string strCONSUMER_TYPE, string strMETER_MANFR, string strCONTRACT, string strCA_VALID_ISU, string strENTRY_DATE,
    string strS_ENC_TKN_1, string strS_ENC_TKN_2, string strS_ENC_TKN_3, string strS_ENC_TKN_4, string strGENUS_RESP_CODE,
    string strMETER_NO, string strACC_CLASS, string strAMNT_BANK, string strAMNT_ISU, string strADDRESS, string strTARIFTYP,
    string strTARIFID, string strPAY_METHOD)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_FICA_PREPAID_MTR _objOutPut = new ZBAPI_FICA_PREPAID_MTR();

        string messageText = "";
        string messageCode = "0";
        DataTable[] bapiResult = new DataTable[6];

        DataTable dtIT_Input = new DataTable();
        DataTable dtIT_Output_DUPL = new DataTable();
        DataTable dtIT_Output_NU = new DataTable();
        DataTable dtIT_Output_FIN = new DataTable();
        DataTable dtIT_Return = _objOutPut.formDataTableError();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_FICA_PREPAID_MTR");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZFICA_PREPAID_MTR_STR");
                IRfcStructure articol = am.CreateStructure();
                IRfcTable artable = am.CreateTable();
                articol.SetValue("DOC_ID", strDOC_ID);
                articol.SetValue("TRANS_ID", strTRANS_ID);
                articol.SetValue("CA", strCA);
                articol.SetValue("COMPANY", strCOMPANY);
                articol.SetValue("CONSUMER_TYPE", strCONSUMER_TYPE);
                articol.SetValue("METER_MANFR", strMETER_MANFR);
                articol.SetValue("CONTRACT", strCONTRACT);
                articol.SetValue("CA_VALID_ISU", strCA_VALID_ISU);
                //articol.SetValue("ENTRY_DATE", DateTime.ParseExact(strENTRY_DATE, "yyyymmdd", CultureInfo.InvariantCulture));
                articol.SetValue("ENTRY_DATE", "20190115");
                articol.SetValue("S_ENC_TKN_1", strS_ENC_TKN_1);
                articol.SetValue("S_ENC_TKN_2", strS_ENC_TKN_2);
                articol.SetValue("S_ENC_TKN_3", strS_ENC_TKN_3);
                articol.SetValue("S_ENC_TKN_4", strS_ENC_TKN_4);
                articol.SetValue("GENUS_RESP_CODE", strGENUS_RESP_CODE);
                articol.SetValue("METER_NO", strMETER_NO);
                articol.SetValue("ACC_CLASS", strACC_CLASS);
                articol.SetValue("AMNT_BANK", strAMNT_BANK);
                articol.SetValue("AMNT_ISU", strAMNT_ISU);
                articol.SetValue("ADDRESS", strADDRESS);
                articol.SetValue("TARIFTYP", strTARIFTYP);
                articol.SetValue("TARIFID", strTARIFID);
                articol.SetValue("PAY_METHOD", strPAY_METHOD);
                artable.Insert(articol);

                testfn.SetValue("IT_INPUT", artable);
                testfn.Invoke(dest);

                IRfcTable _IT_INPUT = testfn.GetTable("IT_INPUT");
                IRfcTable _IT_Output_DUPL = testfn.GetTable("IT_OUTPUT_DUPL");
                IRfcTable _IT_Output_NU = testfn.GetTable("IT_OUTPUT_NU");
                IRfcTable _IT_Output_FIN = testfn.GetTable("IT_OUTPUT_FIN");
                IRfcTable irfcReturn = testfn.GetTable("IT_RETURN");

                dtIT_Input = _objOutPut.converttodotnetatble(_IT_INPUT);
                dtIT_Output_DUPL = _objOutPut.converttodotnetatble(_IT_Output_DUPL);
                dtIT_Output_NU = _objOutPut.converttodotnetatble(_IT_Output_NU);
                dtIT_Output_FIN = _objOutPut.converttodotnetatble(_IT_Output_FIN);
                dtIT_Return = _objOutPut.converttodotnetatble(irfcReturn);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtIT_Input;
        bapiResult[0].TableName = "IT_Input";
        bapiResult[1] = dtIT_Output_DUPL;
        bapiResult[1].TableName = "IT_Output_DUPL";
        bapiResult[2] = dtIT_Output_NU;
        bapiResult[2].TableName = "IT_Output_NU";
        bapiResult[3] = dtIT_Output_FIN;
        bapiResult[3].TableName = "IT_Output_FIN";
        bapiResult[4] = dtIT_Return;
        bapiResult[4].TableName = "IT_Return";
        bapiResult[5] = dtMessageText;
        bapiResult[5].TableName = "MessageText";

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);
        dsBAPIOutput.Tables.Add(bapiResult[4]);
        dsBAPIOutput.Tables.Add(bapiResult[5]);

        return dsBAPIOutput;
    }
    public DataSet get_ZBAPI_CA_OUTSTANDING_AMT(string strCA)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_CA_OUTSTANDING_AMT _objOutPut = new ZBAPI_CA_OUTSTANDING_AMT();

        string messageText = "";
        string messageCode = "0";
        string _sO_VKONT = "", _sO_FLAG = "", _sO_MSG = "";// 000400612200
        DataTable[] bapiResult = new DataTable[2];
        
        DataTable dtCADetails = _objOutPut.makeFlagsTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();       

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CA_OUTSTANDING_AMT");

                if (strCA.Length == 11)
                    strCA = "0" + strCA;
                else if (strCA.Length == 10)
                    strCA = "00" + strCA;
                else if (strCA.Length == 9)
                    strCA = "000" + strCA;

                testfn.SetValue("I_VKONT", strCA);                                           
                testfn.Invoke(dest);

                _sO_VKONT = testfn.GetString("O_VKONT").Trim();
                _sO_FLAG = testfn.GetString("O_FLAG").Trim();
                _sO_MSG = testfn.GetString("O_MSG").Trim();

                if (_sO_FLAG == "1")
                    _sO_MSG = "Dear Customer, your request is not processed futher as due are reflecting against the CA, may please pay for processing.";
                else if (_sO_FLAG == "0")
                    _sO_MSG = "Dear Customer, your request is successfully processing. May please contact EESL for futher details.";                
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        _objOutPut.pushOutputDataInDataTable(dtCADetails, _sO_VKONT, _sO_FLAG, _sO_MSG);

        bapiResult[0] = dtCADetails;        
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);        

        return dsBAPIOutput;
    }


    public DataSet get_ZFI_CURR_OUTS_FLAG(string strCA)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZFI_CURR_OUTS_FLAG _objOutPut = new ZFI_CURR_OUTS_FLAG();
        DataTable dtreturnTable1 = new DataTable();
        DataTable dtreturnTable2 = new DataTable();

        string messageText = "";
        string messageCode = "0";
        string _sO_FLAG = "", _sO_AMT = "", _sO_Name = "", _sO_MSG = "";
        DataTable[] bapiResult = new DataTable[2];

        DataTable dtCADetails = _objOutPut.makeFlagsTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZFI_CURR_OUTS_FLAG");

                if (strCA.Length == 11)
                    strCA = "0" + strCA;
                else if (strCA.Length == 10)
                    strCA = "00" + strCA;
                else if (strCA.Length == 9)
                    strCA = "000" + strCA;

                testfn.SetValue("VKONT", strCA);
                testfn.Invoke(dest);

                IRfcStructure bapiTable1 = testfn.GetStructure("CA_OUTSTANDING_AMT");
                IRfcStructure bapiTable = testfn.GetStructure("RETURN");


                dtreturnTable1 = _objOutPut.CreateOutputDataTable(bapiTable1.GetString("FLAG"), bapiTable1.GetString("AMOUNT"));

                if (dtreturnTable1.Rows.Count > 0)
                {
                    if (dtreturnTable1.Rows[0][0] != null)
                    {
                        _sO_FLAG = dtreturnTable1.Rows[0][0].ToString().Trim();

                        if (_sO_FLAG == "1")
                            _sO_MSG = "Dear Customer, your request is not processed futher as due are reflecting against the CA, may please pay for processing.";
                        else if (_sO_FLAG == "0")
                            _sO_MSG = "Dear Customer, your request is successfully processing. May please contact EESL for futher details.";
                        else
                            _sO_MSG = "Dear Customer, your request is not processed futher due to not reflecting CA in our ISU-Database.";
                    }

                    if (dtreturnTable1.Rows[0][1] != null)
                    {
                        _sO_AMT = dtreturnTable1.Rows[0][1].ToString().Trim();
                    }

                    _sO_Name = bapiTable1.GetString("NAME");
                }
                else
                {
                    _sO_MSG = "Dear Customer, your request is not processed futher due to not reflecting CA in our ISU-Database.";
                }
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        _objOutPut.pushOutputDataInDataTable(dtCADetails, strCA, _sO_Name, _sO_AMT, _sO_FLAG, _sO_MSG);

        bapiResult[0] = dtCADetails;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;
    }
    public DataSet Get_ZBAPI_LAST_MODE_PAY(string CA, string FLAG)
    {
        DataSet dsOutput = new DataSet("ZBAPI_LAST_MODE_PAY");
        ZBAPI_LAST_MODE_PAY objOutPut = new ZBAPI_LAST_MODE_PAY();
        string strMessage = string.Empty;
        string strMessagecode = "0";
        string sO_VKONT = string.Empty;
        string sO_MOD_OF_PAY = string.Empty;
        string sO_FLAG = string.Empty;
        DataTable[] BapiResult = new DataTable[2];
        DataTable dtOutputTable = new DataTable();
        DataTable dtreturnTable = new DataTable();
        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        DataTable dtCADetails = objOutPut.makeFlagsTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_LAST_MODE_PAY");
                testfn.SetValue("VKONT", CA);
                testfn.SetValue("IFLAG", FLAG);
                testfn.Invoke(dest);
                sO_VKONT = testfn.GetString("VKONT");
                sO_MOD_OF_PAY = testfn.GetString("MOD_OF_PAY");
                sO_FLAG = testfn.GetString("FLAG");
                objOutPut.pushOutputDataInDataTable(dtCADetails, sO_VKONT, sO_MOD_OF_PAY, sO_FLAG);
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }
        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtCADetails;
        BapiResult[1] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        dsOutput.Tables.Add(BapiResult[1]);
        return dsOutput;
    }
    public DataSet Get_BAPI_MTRREADDOC_GETLIST(string strSERIALNO)
    {
        DataSet dsOutput = new DataSet("ZBI_NOC_RESULT");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        string strMRDATEFROM = string.Empty;
        string strMRDATETO = string.Empty;
        string strMRDOCUMENTTYPE = string.Empty;
        strMRDATETO = DateTime.Now.Date.ToString("dd-MM-yyyy");
        strMRDATEFROM = DateTime.Now.Date.AddDays(-360).ToString("dd-MM-yyyy");

        //strMRDATETO = DateTime.Now.Date.ToString("dd.MM.yyyy");
        //strMRDATEFROM = DateTime.Now.Date.AddDays(-360).ToString("dd.MM.yyyy");

        BAPI_MTRREADDOC_GETLIST objOutPut = new BAPI_MTRREADDOC_GETLIST();
        DataTable[] BapiResult = new DataTable[3];

        DataTable dtOutputTable = new DataTable();
        DataTable dtreturnTable = new DataTable();

        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        DataTable dtCADetails = objOutPut.makeFlagsTable();

        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("BAPI_MTRREADDOC_GETLIST");
                testfn.SetValue("SERIALNO", "0000000000" + strSERIALNO);
                testfn.SetValue("MRDATEFROM", Convert.ToDateTime(strMRDATEFROM));
                testfn.SetValue("MRDATETO", Convert.ToDateTime(strMRDATETO));

                testfn.SetValue("MRDOCUMENTTYPE", "2");
                //testfn.SetValue("MRDOCUMENTTYPE", "1");
                testfn.Invoke(dest);
                IRfcTable _IT_INPUT = testfn.GetTable("MRDOCUMENTDATA");
                dtCADetails = objOutPut.converttodotnetatble(_IT_INPUT);
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtCADetails;
        BapiResult[1] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        dsOutput.Tables.Add(BapiResult[1]);
        return dsOutput;
    }

    public DataSet Get_ZBAPI_FETCH_ENF_USER_DET(string CANUMBER)
    {
        DataSet dsOutput = new DataSet("FETCH_ENF_USER_DET");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_FETCH_ENF_USER_DET objOutPut = new ZBAPI_FETCH_ENF_USER_DET();
        DataTable[] BapiResult = new DataTable[3];

        DataTable dtOutputTable = new DataTable();
        DataTable dtreturnTable = new DataTable();

        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        DataTable dtCADetails = objOutPut.makeFlagsTable();

        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_FETCH_ENF_USER_DET");
                testfn.SetValue("CA_NUMBER", CANUMBER);
                testfn.Invoke(dest);
                IRfcTable _IT_INPUT = testfn.GetTable("ZUSR_DETAIL");
                dtCADetails = objOutPut.converttodotnetatble(_IT_INPUT);
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtCADetails;
        BapiResult[1] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        dsOutput.Tables.Add(BapiResult[1]);
        return dsOutput;
    }

    public DataSet Get_ZBAPI_BILL_DET(string CANUMBER)
    {
        int i = 0;
        DataSet dsOutput = new DataSet("BILL_DET");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_BILL_DET objOutPut = new ZBAPI_BILL_DET();
        DataTable[] BapiResult = new DataTable[1];
        DataTable dtPDFDetails = new DataTable();

        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_BILL_DET");
                testfn.SetValue("VKONT", CANUMBER);
                testfn.Invoke(dest);

                IRfcTable _IT_INPUT = testfn.GetTable("T_PDF");
                dtPDFDetails = objOutPut.converttodotnetatble(_IT_INPUT);
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }

        BapiResult[0] = dtPDFDetails;
        dsOutput.Tables.Add(BapiResult[0]);

        return dsOutput;
    }

    public DataSet Get_ZBAPI_BILL_DET_64(string CANUMBER)
    {
        int i = 0;
        DataSet dsOutput = new DataSet("BILL_DET");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_BILL_DET_64 objOutPut = new ZBAPI_BILL_DET_64();
        DataTable[] BapiResult = new DataTable[1];
        DataTable dtPDFDetails = new DataTable();

        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_BILL_DET_64");
                testfn.SetValue("VKONT", CANUMBER);
                testfn.Invoke(dest);

                IRfcTable _IT_INPUT = testfn.GetTable("T_PDF");
                dtPDFDetails = objOutPut.converttodotnetatble(_IT_INPUT);
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }

        BapiResult[0] = dtPDFDetails;
        dsOutput.Tables.Add(BapiResult[0]);

        return dsOutput;
    }

    public DataSet Get_ZBAPI_STREET_DET_UPD(string COMPANY, string CANUMBER, string DATA_PROCESS_DATE, string STLWATT, string NO_OF_POINT, string INSTALLATION_DATE, string MOVEOUT_DATE, string ACTIVATION, string DEACTIVATION, string REQUESTID, string REQUEST_DATE, string DOCUMENT_UPLOADED)
    {
        DataSet dsOutput = new DataSet("STREET_DET_UPD");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_STREET_DET_UPD objOutPut = new ZBAPI_STREET_DET_UPD();
        DataTable[] BapiResult = new DataTable[1];
        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                //RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                //RfcRepository repo = dest.Repository;
                //IRfcFunction testfn = repo.CreateFunction("ZBAPI_STREET_DET_UPD");
                //testfn.SetValue("COMPANY", COMPANY);
                //testfn.SetValue("CANUMBER", CANUMBER);
                ////testfn.SetValue("DATA_PROCESS_DATE", "20200606");
                ////testfn.SetValue("DATA_PROCESS_DATE", DateTime.ParseExact(DATA_PROCESS_DATE, "yyyyddmm", CultureInfo.InvariantCulture));
                //testfn.SetValue("DATA_PROCESS_DATE", Convert.ToDateTime(DATA_PROCESS_DATE).ToString("MM-dd-yyyy"));
                //// testfn.SetValue("DATA_PROCESS_DATE", DateTime.ParseExact(DATA_PROCESS_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                //testfn.SetValue("STLWATT", STLWATT);
                //testfn.SetValue("NO_OF_POINT", NO_OF_POINT);
                //// //testfn.SetValue("INSTALLATION_DATE", DateTime.ParseExact(INSTALLATION_DATE, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                //// //testfn.SetValue("MOVEOUT_DATE", DateTime.ParseExact(MOVEOUT_DATE, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                //// testfn.SetValue("INSTALLATION_DATE", DateTime.ParseExact(INSTALLATION_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                //// testfn.SetValue("MOVEOUT_DATE", DateTime.ParseExact(MOVEOUT_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                //testfn.SetValue("INSTALLATION_DATE", Convert.ToDateTime(INSTALLATION_DATE).ToString("yyyyMMdd"));
                //testfn.SetValue("MOVEOUT_DATE", Convert.ToDateTime(MOVEOUT_DATE).ToString("yyyyMMdd"));
                //testfn.SetValue("ACTIVATION", ACTIVATION);
                //testfn.SetValue("DEACTIVATION", DEACTIVATION);
                //testfn.SetValue("REQUESTID", REQUESTID);
                ////// testfn.SetValue("REQUEST_DATE", DateTime.ParseExact(REQUEST_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                //// // testfn.SetValue("REQUEST_DATE", DateTime.ParseExact(REQUEST_DATE, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                //testfn.SetValue("REQUEST_DATE", Convert.ToDateTime(REQUEST_DATE).ToString("yyyyMMdd"));
                //// testfn.SetValue("DOCUMENT_UPLOADED", DOCUMENT_UPLOADED);
                //testfn.Invoke(dest);
                //string _sFlag = testfn.GetString("FLAG");
                //dtMessageText = objOutPut.CreateOutputDataTable(_sFlag);


                //Change By Babalu Kumar

                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_STREET_DET_UPD");
                DateTime date1 = DateTime.ParseExact(DATA_PROCESS_DATE, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime date2 = DateTime.ParseExact(INSTALLATION_DATE, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime date3 = DateTime.ParseExact(MOVEOUT_DATE, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime date4 = DateTime.ParseExact(REQUEST_DATE, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                testfn.SetValue("COMPANY", COMPANY);
                testfn.SetValue("CANUMBER", CANUMBER);
                testfn.SetValue("DATA_PROCESS_DATE", date1);
                //testfn.SetValue("DATA_PROCESS_DATE",Convert.ToDateTime(DATA_PROCESS_DATE).ToString("MM-dd-yyyy"));
                // testfn.SetValue("DATA_PROCESS_DATE", DateTime.ParseExact(DATA_PROCESS_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                testfn.SetValue("STLWATT", STLWATT);
                testfn.SetValue("NO_OF_POINT", NO_OF_POINT);
                testfn.SetValue("INSTALLATION_DATE", date2);
                testfn.SetValue("MOVEOUT_DATE", date3);
                // testfn.SetValue("INSTALLATION_DATE", DateTime.ParseExact(INSTALLATION_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                // testfn.SetValue("MOVEOUT_DATE", DateTime.ParseExact(MOVEOUT_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                //testfn.SetValue("INSTALLATION_DATE", Convert.ToDateTime(INSTALLATION_DATE).ToString("MM-dd-yyyy"));
                //testfn.SetValue("MOVEOUT_DATE", Convert.ToDateTime(MOVEOUT_DATE).ToString("MM-dd-yyyy"));
                testfn.SetValue("ACTIVATION", ACTIVATION);
                testfn.SetValue("DEACTIVATION", DEACTIVATION);
                testfn.SetValue("REQUESTID", REQUESTID);
                testfn.SetValue("REQUEST_DATE", date4);
                // testfn.SetValue("REQUEST_DATE", DateTime.ParseExact(REQUEST_DATE, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                //testfn.SetValue("REQUEST_DATE", Convert.ToDateTime(REQUEST_DATE).ToString("MM-dd-yyyy"));
                testfn.SetValue("DOCUMENT_UPLOADED", DOCUMENT_UPLOADED);
                testfn.Invoke(dest);
                string _sFlag = testfn.GetString("FLAG");
                dtMessageText = objOutPut.CreateOutputDataTable(_sFlag);

                //End
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        return dsOutput;
    }

    public DataSet Get_ZBI_PREPAID_MTR(string CANUMBER)
    {

        DataSet dsOutput = new DataSet("PREPAID_MTR");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBI_PREPAID_MTR objOutPut = new ZBI_PREPAID_MTR();
        DataTable[] BapiResult = new DataTable[3];
        DataTable dtDetails = new DataTable();
        DataTable dtReturn = new DataTable();


        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBI_PREPAID_MTR");
                testfn.SetValue("IN_VKONT", CANUMBER);

                testfn.Invoke(dest);

                //string Flag = (string)testfn.GetValue("OUT_PREPAID_FLAG");
                //objOutPut.pushOutputDataInDataTable(dtDetails, Flag);


                //IRfcTable irfcReturnTable2 = testfn.GetTable("RETURN");
                //dtReturn = objOutPut.converttodotnetatble(irfcReturnTable2);

                string _sFlag = testfn.GetString("OUT_PREPAID_FLAG");
                dtMessageText = objOutPut.CreateOutputDataTable(_sFlag);
            }
        }
        //catch (RfcCommunicationException ex)
        //{
        //    strMessage = "RfcCommunicationException :" + ex.Message.ToString();
        //    strMessagecode = "91";
        //}
        //catch (RfcLogonException ex)
        //{
        //    strMessage = "RfcLogonException :" + ex.Message.ToString();
        //    strMessagecode = "92";
        //}
        //catch (RfcAbapException ex)
        //{
        //    strMessage = "RfcAbapException :" + ex.Message.ToString();
        //    strMessagecode = "93";
        //}
        //catch (Exception ex)
        //{
        //    strMessage = ex.Message.ToString();
        //    strMessagecode = "94";
        //}

        //if (strMessage.Trim() != "")
        //{
        //    objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        //}

        //BapiResult[0] = dtDetails;
        //BapiResult[1] = objOutPut.makeBAPIRET2TABLE(dtReturn);
        //BapiResult[2] = dtMessageText;

        //dsOutput.Tables.Add(BapiResult[0]);
        //dsOutput.Tables.Add(BapiResult[1]);
        //dsOutput.Tables.Add(BapiResult[2]);

        //return dsOutput;

        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        return dsOutput;
    }

    public DataSet Get_ZBAPI_ONLINE_BILL_PDF(string _strCANumber, string _strEBSKNO)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_ONLINE_BILL_PDF _objOutPut = new ZBAPI_ONLINE_BILL_PDF();
        string messageText = "";
        string messageCode = "0";
        string Contact;
        string Flag = "";
        DataTable[] bapiResult = new DataTable[2];   
        DataTable dtOutputTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                if ((_strCANumber.Trim() != "") && (_strCANumber.Length > 3))
                {
                    if (_strCANumber.Substring(3, 1) != "2")
                    {
                        RfcDestination dest = RfcDestinationManager.GetDestination("P92");
                        RfcRepository repo = dest.Repository;
                        IRfcFunction testfn = repo.CreateFunction("ZBAPI_ONLINE_BILL_PDF");
                        testfn.SetValue("CONT_ACCT", _strCANumber);
                        testfn.SetValue("IN_OPBEL", _strEBSKNO);
                        testfn.Invoke(dest);
                        Contact = (string)testfn.GetValue("CONTACT");
                        Flag = (string)testfn.GetValue("FLAG");
                        IRfcTable OutputTable = testfn.GetTable("PDF");
                        IRfcTable returnTable2 = testfn.GetTable("RETURN");
                        dtOutputTable = _objOutPut.converttodotnetatble(OutputTable);
                    }
                    else
                    {
                        messageText = "Flag value 2 : Bill Not Available";
                        messageCode = "2";
                    }
                }
                else
                {
                    RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                    RfcRepository repo = dest.Repository;
                    IRfcFunction testfn = repo.CreateFunction("ZBAPI_ONLINE_BILL_PDF");
                    testfn.SetValue("CONT_ACCT", _strCANumber);
                    testfn.Invoke(dest);
                    Contact = (string)testfn.GetValue("CONTACT");
                    Flag = (string)testfn.GetValue("FLAG");
                    IRfcTable OutputTable = testfn.GetTable("PDF");
                    IRfcTable returnTable2 = testfn.GetTable("RETURN");
                    dtOutputTable = _objOutPut.converttodotnetatble(OutputTable);
                }
                if (Flag == "1")
                {
                    messageText = "Flag value 1 : CA Invalid";
                    messageCode = "1";
                }
                if (Flag == "2")
                {
                    messageText = "Flag value 2 : Bill Not Available";
                    messageCode = "2";
                }
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }
        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }
        bapiResult[0] = _objOutPut.GetFormattedZPDFTable(dtOutputTable);
        bapiResult[1] = dtMessageText;
        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        return dsBAPIOutput;
    }

    public DataSet Get_ZBI_BAPI_SOLAR1(string CA_NUMBER, string BILL_MONTH)
    {
        string messageText = string.Empty;
        string messageCode = "0";
        DataSet dsBAPIOutput = new DataSet("BAPI_SOLAR1");
        ZBI_BAPI_SOLAR1 _objOutPut = new ZBI_BAPI_SOLAR1();
        DataTable dtreturnTable1 = new DataTable();
        DataTable dtreturnTable2 = new DataTable();
        DataTable[] bapiResult = new DataTable[2];
        DataTable dtCADetails = _objOutPut.makeFlagsTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBI_BAPI_SOLAR1");
                testfn.SetValue("IN_VKONT", CA_NUMBER);
                testfn.SetValue("IN_BILL_MONTH", BILL_MONTH);
                testfn.Invoke(dest);
                IRfcStructure bapiTable1 = testfn.GetStructure("OUT_SOLAR");//ZBI_NETMTR_STRUC//OUT_SOLAR
                dtreturnTable1 = _objOutPut.CreateOutputDataTable(bapiTable1.GetString("MANDT"), bapiTable1.GetString("CONSUMER_NO"),
                    bapiTable1.GetString("INVOICE_NO"), bapiTable1.GetString("COMPANY_CODE"), bapiTable1.GetString("DATE_OF_INVOICE"),
                    bapiTable1.GetString("BILL_MONTH"), bapiTable1.GetString("FROM_DATE"),
                    bapiTable1.GetString("TO_DATE"), bapiTable1.GetString("NO_OF_DAYS"), bapiTable1.GetString("EXPORT_UNIT"),
                    bapiTable1.GetString("IMPORT_UNIT"), bapiTable1.GetString("SOLAR_MTR_UNIT"), bapiTable1.GetString("FIN_YEAR"),
                    bapiTable1.GetString("CUM_APR_TO_SEP"), bapiTable1.GetString("CUM_OCT_TO_MAR"), bapiTable1.GetString("GBI_APR_TO_SEP"),
                    bapiTable1.GetString("GBI_OCT_TO_MAR"), bapiTable1.GetString("CUM_FOR_FY"), bapiTable1.GetString("CUM_SINCE_INST"));
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        //  _objOutPut.pushOutputDataInDataTable(dtCADetails, "", "", "", "", "","","","","","","","","","","","","","","");

        bapiResult[0] = dtreturnTable1;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;
    }

    #region SAJID FOR RECORD MANAGEMENT SYSTEM 09092021
    public DataSet ZBAPI_CS_CRN_CHECK(string CRNNO, string KNO, string orderno)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_CS_CRN_CHECK _objOutPut = new ZBAPI_CS_CRN_CHECK();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[5];

        DataTable dtCADetails = new DataTable();
        DataTable dtCSDetails = new DataTable();
        DataTable dtreturnTable2 = new DataTable();
        DataTable dtErrorTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                //RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                //RfcRepository repo = dest.Repository;
                ////IRfcFunction testfn = repo.CreateFunction("ZBAPI_CS_CRN_CHECK");

                ////RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPI_CS_CRN_CHK_STR");
                //IRfcFunction testfn = repo.CreateFunction("ZBAPI_CS_CRN_CHECK");

                //RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPI_CS_CRN_CHK_STR");
                //IRfcStructure articol = am.CreateStructure();
                //articol.SetValue("CRNNUMBER", CRNNO);
                //articol.SetValue("KNUMBER", KNO);
                //articol.SetValue("AUFNR", orderno);

                ////testfn.SetValue("IMPORT_CANUMBER", articol);
                ////testfn.SetValue("IMPORT_TELEPHONE_NO", strTelephoneNumber);
                ////testfn.SetValue("IMPORT_KNUMBER", strKNumber);
                ////testfn.SetValue("IMPORT_CRNNUMBER", strConsumerNumber);

                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CS_CRN_CHECK");
                testfn.SetValue("CRNNUMBER", CRNNO);
                testfn.SetValue("KNUMBER", KNO);
                testfn.SetValue("ORDER_NUMBER", orderno);

                testfn.Invoke(dest);

                IRfcTable ErrorMessageTable = testfn.GetTable("BAPIMESSAGE_N");
                IRfcTable OutputTable = testfn.GetTable("EXPORT_CADETAILS_N");
                IRfcTable CSDetailsTable = testfn.GetTable("EXPORT_CSDETAILS_N");
                IRfcTable returnTable2 = testfn.GetTable("RETURN");

                dtCADetails = _objOutPut.converttodotnetatble(OutputTable);
                dtCSDetails = _objOutPut.converttodotnetatble(CSDetailsTable);
                dtreturnTable2 = _objOutPut.converttodotnetatble(returnTable2);
                dtErrorTable = _objOutPut.converttodotnetatble(ErrorMessageTable);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }


        bapiResult[0] = _objOutPut.getCADetailsFormattedTable(dtCADetails);
        bapiResult[1] = _objOutPut.getCSDetailsFormattedTable(dtCSDetails);
        bapiResult[2] = _objOutPut.makeBAPIRET2TABLE(dtreturnTable2);
        bapiResult[3] = _objOutPut.makeISUERRTable(dtErrorTable);
        bapiResult[4] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);
        dsBAPIOutput.Tables.Add(bapiResult[4]);

        return dsBAPIOutput;
    }

   #endregion

    public DataSet Get_ZBAPI_CS_BP_DETAIL(string CompName, string Date, string OrderType)
    {
        DataSet dsOutput = new DataSet("ZBAPI_CS_BP_DETAIL");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_CS_BP_DETAIL objOutPut = new ZBAPI_CS_BP_DETAIL();

        DataTable[] BapiResult = new DataTable[1];
        DataTable dtreturnTable1 = new DataTable();
        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CS_BP_DETAIL");

                testfn.SetValue("COMP_CODE", CompName);
                testfn.SetValue("CUR_DATE", DateTime.ParseExact(Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                testfn.SetValue("AUART", OrderType);

                testfn.Invoke(dest);
                IRfcStructure bapiTable1 = testfn.GetStructure("PARTNERDATA");
                IRfcStructure bapiTable = testfn.GetStructure("RETURN"); //ZSEWA_ORDER_DET
                IRfcTable OutputTable = testfn.GetTable("ZSEWA_ORDER_DET");

                dtreturnTable1 = objOutPut.converttodotnetatble(OutputTable);

                for (int i = 0; i < dtreturnTable1.Rows.Count; i++)
                {
                    dtreturnTable1.Rows[i]["ERDAT"] = Convert.ToDateTime(dtreturnTable1.Rows[i]["ERDAT"].ToString()).ToString("dd-MMM-yyyy");
                    dtreturnTable1.Rows[i]["GSTRP"] = Convert.ToDateTime(dtreturnTable1.Rows[i]["GSTRP"].ToString()).ToString("dd-MMM-yyyy");
                    dtreturnTable1.Rows[i]["MOVE_DATE"] = Convert.ToDateTime("01-Jan-1900").ToString("dd-MMM-yyyy");
                    dtreturnTable1.Rows[i]["VALIDFROMDATE"] = Convert.ToDateTime(dtreturnTable1.Rows[i]["VALIDFROMDATE"].ToString()).ToString("dd-MMM-yyyy");
                    dtreturnTable1.Rows[i]["VALIDTODATE"] = Convert.ToDateTime(dtreturnTable1.Rows[i]["VALIDTODATE"].ToString()).ToString("dd-MMM-yyyy");
                    dtreturnTable1.Rows[i]["STRMN"] = Convert.ToDateTime(dtreturnTable1.Rows[i]["STRMN"].ToString()).ToString("dd-MMM-yyyy");
                    dtreturnTable1.Rows[i]["LTRMN"] = Convert.ToDateTime("01-Jan-1900").ToString("dd-MMM-yyyy");
                }

                dtreturnTable1.AcceptChanges();
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtreturnTable1;
        dsOutput.Tables.Add(BapiResult[0]);
        return dsOutput;
    }


    public DataSet Get_ZBAPI_CREATE_SUBORDER(string _AUART, string _AUFNR, string _PARTNER, string _KTEXT, string _ILART, string _GSTRP,
                                          string _GLTRP, string _GSUZP, string _GLUZP)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_CREATE_SUBORDER _objOutPut = new ZBAPI_CREATE_SUBORDER();
        string messageText = "";
        string messageCode = "0";
        string ORD_SUB_ORDER = "";
        DataTable[] bapiResult = new DataTable[1];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                //IRfcFunction testfn = repo.CreateFunction("ZBAPI_CREATE_SUBORDER");
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CS_ENH46");
                testfn.SetValue("AUART", _AUART);
                testfn.SetValue("AUFNR", _AUFNR);
                testfn.SetValue("PARTNER", _PARTNER);
                testfn.SetValue("KTEXT", _KTEXT);
                testfn.SetValue("ILART", _ILART);
                testfn.SetValue("GSTRP", _GSTRP);
                testfn.SetValue("GLTRP", _GLTRP);
                testfn.SetValue("GSUZP", _GSUZP);
                testfn.SetValue("GLUZP", _GLUZP);

                testfn.Invoke(dest);
                ORD_SUB_ORDER = (string)testfn.GetValue("ORD_NUM");
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        _objOutPut.pushFlagsInDataTable(dtFlagsTable, ORD_SUB_ORDER);

        bapiResult[0] = dtFlagsTable;

        dsBAPIOutput.Tables.Add(bapiResult[0]);

        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_CREATEBP_SUBORDER(string _AUART, string _PARTNER, string _DIVCODE, string _ILART, string _ANLZU,
                                        string _ZZ_RLOAD, string _ZZ_RLOADKVA, string _ZZ_CONNTYPE)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_CREATE_SUBORDER _objOutPut = new ZBAPI_CREATE_SUBORDER();

        string messageText = "";
        string messageCode = "0";

        string ORD_SUB_ORDER = "";

        DataTable[] bapiResult = new DataTable[1];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_ZNCO_CREATE");
                testfn.SetValue("AUART", _AUART);
                testfn.SetValue("PARTNER", _PARTNER);
                testfn.SetValue("COMP_CODE", "BRPL");
                testfn.SetValue("COKEY", _DIVCODE);
                testfn.SetValue("ILART", _ILART);
                testfn.SetValue("ANLZU", _ANLZU);
                testfn.SetValue("ZZ_RLOAD", _ZZ_RLOAD);
                testfn.SetValue("ZZ_RLOADKVA", _ZZ_RLOADKVA);
                testfn.SetValue("ZZ_CONNTYPE", _ZZ_CONNTYPE);

                testfn.Invoke(dest);
                ORD_SUB_ORDER = (string)testfn.GetValue("AUFNR");
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        _objOutPut.pushFlagsInDataTable(dtFlagsTable, ORD_SUB_ORDER);

        bapiResult[0] = dtFlagsTable;

        dsBAPIOutput.Tables.Add(bapiResult[0]);

        return dsBAPIOutput;
    }



    public DataSet Get_BAPI_ISUSMORDER_USERSTATUSSET(string _NUMBER, string _INTERN, string _EXTERN, string _LANGU,
                                                    string _LANGU_ISO, string _INACTIVE)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        BAPI_ISUSMORDER_USERSTATUSSET _objOutPut = new BAPI_ISUSMORDER_USERSTATUSSET();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("BAPI_ISUSMORDER_USERSTATUSSET");

                RfcStructureMetadata am = repo.GetStructureMetadata("BAPI_STATUS");
                IRfcStructure articolI = am.CreateStructure();
                articolI.SetValue("INTERN", _INTERN.Trim());
                articolI.SetValue("EXTERN", _EXTERN.Trim());
                articolI.SetValue("LANGU", _LANGU.Trim());
                articolI.SetValue("LANGU_ISO", _LANGU_ISO.Trim());

                testfn.SetValue("NUMBER", _NUMBER);
                testfn.SetValue("STATUS", articolI);
                testfn.SetValue("INACTIVE", _INACTIVE);

                testfn.Invoke(dest);
                //ORD_SUB_ORDER = (string)testfn.GetValue("ORD_NUM");
                // IRfcTable returnTable2 = testfn.GetTable("RETURN");
                // dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);

                IRfcStructure bapiTable = testfn.GetStructure("RETURN");
                dtRet2Table = _objOutPut.formDataTableError();

                _objOutPut.addRowToDTError(dtRet2Table, bapiTable.GetString("TYPE"), bapiTable.GetString("ID"), bapiTable.GetString("NUMBER"), bapiTable.GetString("MESSAGE"), bapiTable.GetString("LOG_NO"),
                    bapiTable.GetString("LOG_MSG_NO"), bapiTable.GetString("MESSAGE_V1"), bapiTable.GetString("MESSAGE_V2"), bapiTable.GetString("MESSAGE_V3"), bapiTable.GetString("MESSAGE_V4"), "", "", "", "");


            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtMessageText;
        bapiResult[1] = dtRet2Table;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_UPD_TF_DET(string _Z_AUFNR, string _MNGRP, string _MNCO, string _MATXT)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_UPD_TF_DET _objOutPut = new ZBAPI_UPD_TF_DET();

        string messageText = "";
        string messageCode = "0";

        string _Result = "";

        DataTable[] bapiResult = new DataTable[2];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_UPD_TF_DET");

                IRfcTable tblPartner = testfn.GetTable("ZTABLE_MNGRP");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZSTR_TF_DET");
               // IRfcStructure articolI = am.CreateStructure();

                for (int s = 0; s < _MNGRP.Split(',').Length; s++)
                {
                    IRfcStructure articolI = am.CreateStructure();
                    articolI.SetValue("MNGRP", _MNGRP.Split(',')[s].ToString().Trim());
                    articolI.SetValue("MNCOD", _MNCO.Split(',')[s].ToString().Trim());
                    articolI.SetValue("MATXT", _MATXT.Split(',')[s].ToString().Trim());
                    tblPartner.Append(articolI);
                }

                testfn.SetValue("Z_AUFNR", _Z_AUFNR);

               // tblPartner.Append(articolI);
                testfn.SetValue("ZTABLE_MNGRP", tblPartner);

                testfn.Invoke(dest);
                _Result = testfn.GetValue("V_RETURN").ToString();

            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        _objOutPut.pushFlagsInDataTable(dtFlagsTable, _Result);

        bapiResult[0] = dtFlagsTable;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_NEWCON_STATUS(string _BUKRS, string _DATE)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        BAPI_ISUSMORDER_USERSTATUSSET _objOutPut = new BAPI_ISUSMORDER_USERSTATUSSET();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_NEWCON_STATUS");

                testfn.SetValue("BUKRS", _BUKRS);
                testfn.SetValue("DATE", _DATE);

                testfn.Invoke(dest);
                IRfcTable returnTable2 = testfn.GetTable("OUTPUT");
                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_TEAM_ALLOCATION(string _IMEI, string _ILART, string _DATE, string _TIME)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_TEAM_ALLOCATION _objOutPut = new ZBAPI_TEAM_ALLOCATION();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[3];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtCFTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_TEAM_ALLOCATION");
                testfn.SetValue("IEMI", _IMEI);
                testfn.SetValue("ILART", _ILART);
                testfn.SetValue("DATE", _DATE);
                testfn.SetValue("TIME", _TIME);
                testfn.Invoke(dest);

                IRfcTable returnTable2 = testfn.GetTable("DATA");
                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);

                IRfcTable returnTable3 = testfn.GetTable("CFDETAILS");
                dtCFTable = _objOutPut.converttodotnetatble(returnTable3);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;
        bapiResult[1] = dtCFTable;
        bapiResult[2] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);

        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_NONENG_ENF_RTGS(string _COMP_CODE, string _ORDER_NO, string _CONTRACT_ACCOUNT, string _ACCOUNT_TYPE, string _AMOUNT,
       string _FLAG)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_NONENG_ENF_RTGS _objOutPut = new ZBAPI_NONENG_ENF_RTGS();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[1];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_NONENG_ENF_RTGS");

                testfn.SetValue("COMP_CODE", _COMP_CODE);
                testfn.SetValue("ORDER_NO", _ORDER_NO);
                testfn.SetValue("CONTRACT_ACCOUNT", _CONTRACT_ACCOUNT);
                testfn.SetValue("ACCOUNT_TYPE", _ACCOUNT_TYPE);
                testfn.SetValue("AMOUNT", _AMOUNT);
                testfn.SetValue("FLAG", _FLAG);

                testfn.Invoke(dest);

                //ORD_SUB_ORDER = (string)testfn.GetValue("ORD_NUM");
                //IRfcTable returnTable2 = testfn.GetTable("RETURN");
                //dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);

                IRfcStructure bapiTable = testfn.GetStructure("OUTPUT_STRUC");
                dtRet2Table = _objOutPut.formDataTableError();

                _objOutPut.addRowToDTError(dtRet2Table, bapiTable.GetString("COMP_CODE"), bapiTable.GetString("CONTRACT_ACCOUNT"), bapiTable.GetString("OUT_AMT"), bapiTable.GetString("NAME"), bapiTable.GetString("ADDRESS"),
                    bapiTable.GetString("TEL1_NUMBR"), bapiTable.GetString("E_MAIL"), bapiTable.GetString("ACCT_DET_ID"), bapiTable.GetString("ACCT_CLASS"), bapiTable.GetString("FLAG"),
                    bapiTable.GetString("MESSAGE"), bapiTable.GetString("ORDER_NO"), bapiTable.GetString("OPBEL"), bapiTable.GetString("DUE_DATE"));

            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        // _objOutPut.pushFlagsInDataTable(dtFlagsTable, ORD_SUB_ORDER);

        //bapiResult[0] = dtFlagsTable;
        bapiResult[0] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        //dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_DM_NCON_OTTAB(string _DATE)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        BAPI_ISUSMORDER_USERSTATUSSET _objOutPut = new BAPI_ISUSMORDER_USERSTATUSSET();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_DM_NCON_OTTAB");

                testfn.SetValue("INP_DATE", _DATE);

                testfn.Invoke(dest);
                IRfcTable returnTable2 = testfn.GetTable("IT_OUT");
                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_IDENTIFICATION(string _Partner, string _IDType, string _IDNumber)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        BAPI_ISUSMORDER_USERSTATUSSET _objOutPut = new BAPI_ISUSMORDER_USERSTATUSSET();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_IDENTIFICATION");

                testfn.SetValue("PARTNER", _Partner);
                testfn.SetValue("IDTYP", _IDType);
                testfn.SetValue("IDNUM", _IDNumber);

                testfn.Invoke(dest);
                IRfcTable returnTable2 = testfn.GetTable("ZERR");
                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_CS_GET_KIT_DETAILS(string _Company, string _PostingDate)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        BAPI_ISUSMORDER_USERSTATUSSET _objOutPut = new BAPI_ISUSMORDER_USERSTATUSSET();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CS_GET_KIT_DETAILS");

                testfn.SetValue("COMPANY_CODE", _Company);
                testfn.SetValue("POSTING_DATE", _PostingDate);


                testfn.Invoke(dest);
                IRfcTable returnTable2 = testfn.GetTable("IT_OUT");
                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_ORDER_COUNT(string _OrderType, string _PMActivity, string _Company, string _Date)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        BAPI_ISUSMORDER_USERSTATUSSET _objOutPut = new BAPI_ISUSMORDER_USERSTATUSSET();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();
        string _sCompCode = string.Empty, _sCreationDate = string.Empty;

        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_ORDER_COUNT");
                // IRfcFunction testfn = repo.CreateFunction("ZBAPI_ORDER_COUNT_V1");

                IRfcTable tblOrdType = testfn.GetTable("ORDER_TYPE");
                RfcStructureMetadata am = repo.GetStructureMetadata("ZAUART");
                IRfcStructure articol = am.CreateStructure();

                articol.SetValue("AUART", _OrderType);
                tblOrdType.Append(articol);

                IRfcTable tblPMActivity = testfn.GetTable("PM_ACTIVITY");
                RfcStructureMetadata am1 = repo.GetStructureMetadata("ZILA");
                IRfcStructure articol1 = am1.CreateStructure();

                articol1.SetValue("ILART", _PMActivity);
                tblPMActivity.Append(articol1);

                testfn.SetValue("ORDER_TYPE", tblOrdType);
                testfn.SetValue("PM_ACTIVITY", tblPMActivity);

                //RfcStructureMetadata am2 = repo.GetStructureMetadata("ZBAPI_ORDER_COUNT_IN");
                //IRfcStructure articol2 = am2.CreateStructure();
                //articol2.SetValue("COMPANY_CODE", _Company);
                //articol2.SetValue("CREATION_DATE", "");                         
                //testfn.SetValue("COMPANY_CODE", articol2);

                //RfcStructureMetadata am3 = repo.GetStructureMetadata("ZBAPI_ORDER_COUNT_IN");
                //IRfcStructure articol3 = am3.CreateStructure();
                //articol3.SetValue("COMPANY_CODE", "");
                //articol3.SetValue("CREATION_DATE", _Date);
                //testfn.SetValue("CREATION_DATE", articol3);

                testfn.SetValue("COMPANY_CODE", _Company);
                testfn.SetValue("CREATION_DATE", _Date);

                testfn.Invoke(dest);

                _sCompCode = (string)testfn.GetValue("COMP_CODE");
                _sCreationDate = (string)testfn.GetValue("CREATIONDATE");

                IRfcTable returnTable2 = testfn.GetTable("IT_OUTPUT");
                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_ZDIN_STATUS(string _OrderNo)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        BAPI_ISUSMORDER_USERSTATUSSET _objOutPut = new BAPI_ISUSMORDER_USERSTATUSSET();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_ZDIN_STATUS");


                testfn.SetValue("ORDER_NO", _OrderNo);

                testfn.Invoke(dest);
                IRfcTable returnTable2 = testfn.GetTable("IT_OUTPUT");
                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_MCD_SEAL_UPD(string _DIARY_NO, string _DIVISION, string _MCD_ZONE, string _SDMC_LETTER_DTL, string _RCVD_DT, string _FIN_YR,
                  string _PROPERTY_ADD1, string _PROPERTY_ADD2, string _PROPERTY_ADD3, string _PROPERTY_ADD4, string _PROPERTY_ADD5, string _PROPERTY_ADD6,
                  string _PROPERTY_ADD7, string _PROPERTY_ADD8, string _PROPERTY_ADD9, string _PROPERTY_ADD10, string _PROPERTY_ADD11,
                  string _MTR_NO, string _CA_NO, string _ACTIVITY_TYP, string _STATUS, string _BIS, string _CREATED_BY, string _CREATED_ON,
                  string _REMARKS)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        BAPI_ISUSMORDER_USERSTATUSSET _objOutPut = new BAPI_ISUSMORDER_USERSTATUSSET();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_MCD_SEAL_UPD");

                testfn.SetValue("DIARY_NO", _DIARY_NO.ToUpper());
                testfn.SetValue("DIVISION", _DIVISION.ToUpper());
                testfn.SetValue("MCD_ZONE", _MCD_ZONE.ToUpper());
                testfn.SetValue("SDMC_LETTER_DTL", _SDMC_LETTER_DTL.ToUpper());
                testfn.SetValue("RCVD_DT", _RCVD_DT);
                testfn.SetValue("FIN_YR", _FIN_YR);
                testfn.SetValue("PROPERTY_ADD1", _PROPERTY_ADD1.ToUpper());
                testfn.SetValue("PROPERTY_ADD2", _PROPERTY_ADD3.ToUpper());
                testfn.SetValue("PROPERTY_ADD3", _PROPERTY_ADD3.ToUpper());
                testfn.SetValue("PROPERTY_ADD4", _PROPERTY_ADD4.ToUpper());
                testfn.SetValue("PROPERTY_ADD5", _PROPERTY_ADD5.ToUpper());
                testfn.SetValue("PROPERTY_ADD6", _PROPERTY_ADD6.ToUpper());
                testfn.SetValue("PROPERTY_ADD7", _PROPERTY_ADD7.ToUpper());
                testfn.SetValue("PROPERTY_ADD8", _PROPERTY_ADD8.ToUpper());
                testfn.SetValue("PROPERTY_ADD9", _PROPERTY_ADD9.ToUpper());
                testfn.SetValue("PROPERTY_ADD10", _PROPERTY_ADD10.ToUpper());
                testfn.SetValue("PROPERTY_ADD11", _PROPERTY_ADD11.ToUpper());

                testfn.SetValue("MTR_NO", _MTR_NO.ToUpper());
                testfn.SetValue("CA_NO", _CA_NO.ToUpper());
                testfn.SetValue("ACTIVITY_TYP", _ACTIVITY_TYP.ToUpper());
                testfn.SetValue("STATUS", _STATUS.ToUpper());
                testfn.SetValue("BIS", _BIS.ToUpper());
                testfn.SetValue("CREATED_BY", _CREATED_BY.ToUpper());
                testfn.SetValue("CREATED_ON", _CREATED_ON);
                testfn.SetValue("REMARKS", _REMARKS.ToUpper());

                testfn.Invoke(dest);

                messageCode = (string)testfn.GetValue("FLAG");
                messageText = (string)testfn.GetValue("MESSAGE");

                //IRfcTable returnTable2 = testfn.GetTable("IT_OUTPUT");
                //dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        return dsBAPIOutput;
    }

    #region Ebill
    public string ZBAPI_UPD_DISPATCH(string CANUMBER, string DISPATCH_CONTROL)
    {
      
        string strMessage = string.Empty;
        string strMessagecode = "0";
        string _sFlag = string.Empty;
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_UPD_DISPATCH");
                testfn.SetValue("CA", CANUMBER);
                testfn.SetValue("DISPATCH_CONTROL", DISPATCH_CONTROL);
                testfn.Invoke(dest);
                _sFlag = testfn.GetString("FLAG");
                //IRfcTable _IT_INPUT = testfn.GetTable("T_PDF");
                //dtPDFDetails = objOutPut.converttodotnetatble(_IT_INPUT);

            }
            return _sFlag;
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
            return strMessage;
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
            return strMessage;
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
            return strMessage;
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
            return strMessage;
        }




    }
    #endregion

    public DataSet Get_ZBAPI_FI_CA_TOTAL_DUES(string _CompanyCode, string _ContractAcc)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        BAPI_ISUSMORDER_USERSTATUSSET _objOutPut = new BAPI_ISUSMORDER_USERSTATUSSET();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_FI_CA_TOTAL_DUES");

                testfn.SetValue("COMPANY_CODE", _CompanyCode);
                testfn.SetValue("CONTRACT_ACCOUNT", _ContractAcc);
                testfn.Invoke(dest);

                IRfcTable returnTable2 = testfn.GetTable("IT_OUTPUT");
                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        return dsBAPIOutput;
    }

    #region "Billing Score Automation"

    public DataSet Get_ZBAPI_DM_SLA_DATA(string _DATE)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        BAPI_ISUSMORDER_USERSTATUSSET _objOutPut = new BAPI_ISUSMORDER_USERSTATUSSET();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                //RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                //RfcRepository repo = dest.Repository;
                //IRfcFunction testfn = repo.CreateFunction("ZBAPI_DM_SLA_DATA");

                //testfn.SetValue("DATE", _DATE);

                //testfn.Invoke(dest);
                //IRfcTable returnTable2 = testfn.GetTable("ZBAPI_DM_SLA_STR_OUT");
                //dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);

                //IRfcStructure bapiTable = testfn.GetStructure("ZBAPI_DM_SLA_STR_OUT");
                //dtRet2Table = _objOutPut.formDataTableError();

                //_objOutPut.addRowToDTError(dtRet2Table, bapiTable.GetString("TYPE"), bapiTable.GetString("ID"), bapiTable.GetString("NUMBER"), bapiTable.GetString("MESSAGE"), bapiTable.GetString("LOG_NO"),
                //    bapiTable.GetString("LOG_MSG_NO"), bapiTable.GetString("MESSAGE_V1"), bapiTable.GetString("MESSAGE_V2"), bapiTable.GetString("MESSAGE_V3"), bapiTable.GetString("MESSAGE_V4"), "", "", "", "");


                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                // RfcDestination dest = RfcDestinationManager.GetDestination("P92");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_DM_SLA_DATA");
                testfn.SetValue("DATE", _DATE);

                testfn.Invoke(dest);
                IRfcTable returnTable2 = testfn.GetTable("IT_OUTPUT");                
                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);


            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;
    }

    public DataSet ZBAPI_DM_SLA_DATA_REM(string _FrmDATE, string _ToDATE)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        BAPI_ISUSMORDER_USERSTATUSSET _objOutPut = new BAPI_ISUSMORDER_USERSTATUSSET();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_DM_SLA_DATA_REM");

                testfn.SetValue("FROM_DATE", _FrmDATE);
                testfn.SetValue("TO_DATE", _FrmDATE);

                testfn.Invoke(dest);
                IRfcTable returnTable2 = testfn.GetTable("IT_OUTPUT");
                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;
    }


    public DataSet Get_ZBAPI_UPDATE_TNO(string strCA_no, string strTelephone, string strMobile, string strEmail, string strLandmark, string strDISPATCH_CTRL)
    {
        string _sLandMark = string.Empty;
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_UPDATE_TNO _objOutPut = new ZBAPI_UPDATE_TNO();

        string messageText = "";
        string messageCode = "0";
        string StatusDispatch = string.Empty;

        if (strDISPATCH_CTRL == "A")
        {
            if (strEmail != "" && strMobile != "")
                StatusDispatch = "Z021";
        }

        if (strDISPATCH_CTRL == "C")
        {
            if (strEmail != "" && strMobile != "")
                StatusDispatch = "Z017";
        }

        if (strDISPATCH_CTRL == "D")
        {
            //if(strEmail=="" && strMobile!="")
            //  StatusDispatch = "Z013";

            if (strMobile != "")
                StatusDispatch = "Z013";

            //if (strEmail != "" && strMobile == "")
            //  StatusDispatch = "Z017";
        }

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtUpdateResult = _objOutPut.CreateOutputDataTable("", "", "", "", "", "", "", "", "", "", "");
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_UPDATE_TNO");
                testfn.SetValue("CON_ACC", strCA_no);
                testfn.SetValue("TELEPHONE", strTelephone);
                testfn.SetValue("MOBILE", strMobile);
                testfn.SetValue("E_MAIL", strEmail);
                testfn.SetValue("LANDMARK", _sLandMark);
                testfn.SetValue("DISPATCH_CTRL", StatusDispatch);

                testfn.Invoke(dest);

                string bapiMOBILE_NO = (string)testfn.GetString("MOBILE_NO");
                string bapiEMAILADDRESS = (string)testfn.GetString("EMAILADDRESS");
                string bapiLANDMARKADDRESS = (string)testfn.GetString("LANDMARKADDRESS");
                string bapiLAND_FLAG = (string)testfn.GetString("LAND_FLAG");
                string bapiMOBILE_FLAG = (string)testfn.GetString("MOBILE_FLAG");
                string bapiEMAIL_FLAG = (string)testfn.GetString("EMAIL_FLAG");
                string bapiLANDMARK_FLAG = (string)testfn.GetString("LANDMARK_FLAG");
                string bapiTELVALID_FLAG = (string)testfn.GetString("TELVALID_FLAG");
                string bapiMOBLAVID_FLAG = (string)testfn.GetString("MOBVALID_FLAG");
                string bapiDISPATCH_FLAG = (string)testfn.GetString("DISPATCH_FLAG");


                dtUpdateResult = _objOutPut.CreateOutputDataTable(strCA_no, bapiMOBILE_NO, bapiEMAILADDRESS, bapiLANDMARKADDRESS, bapiLAND_FLAG, bapiMOBILE_FLAG, bapiEMAIL_FLAG, bapiLANDMARK_FLAG, bapiTELVALID_FLAG, bapiMOBLAVID_FLAG, bapiDISPATCH_FLAG);

            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        dsBAPIOutput.Tables.Add(dtUpdateResult);

        return dsBAPIOutput;
    }

    public DataSet get_ZBAPI_DUNNING_NOTICE_WHATSAPP(string CompanyCode, string _VKONT)
    {
        DataTable[] bapiResult = new DataTable[2];
        DataSet dsBAPIOutput = new DataSet("ZBAPI_DUNNING_NOTICE_WHATSAPP");
        ZBAPI_DUNNING_NOTICE_WHATSAPP _objOutPut = new ZBAPI_DUNNING_NOTICE_WHATSAPP();

        string messageText = "";
        string messageCode = "0";

        DataTable dtPDF = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_DUNNING_NOTICE_WHATSAPP");

                testfn.SetValue("BUKRS", CompanyCode);
                testfn.SetValue("VKONT", _VKONT);

                testfn.Invoke(dest);

                IRfcTable _PDF = testfn.GetTable("PDF");

                dtPDF = _objOutPut.converttodotnetatble(_PDF);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtPDF;
        bapiResult[0].TableName = "PDF";
        bapiResult[1] = dtMessageText;
        bapiResult[1].TableName = "Error";

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;

    }

    public DataSet Get_ZBAPI_ELNOTICE_WHATSAPP(string _companyCode, string _caNumber)
    {
        DataSet dsOutput = new DataSet("BAPI_RESULT");
        ZBAPI_ELNOTICE_WHATSAPP objOutPut = new ZBAPI_ELNOTICE_WHATSAPP();
        string strMessage = string.Empty, strMessagecode = "0", sO_FLAG = string.Empty;

        DataTable[] BapiResult = new DataTable[3];

        DataTable dtIT_PDF = new DataTable();
        DataTable dtReturn = objOutPut.CreateReturnTable();
        DataTable dtMessageText = objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_ELNOTICE_WHATSAPP");
                testfn.SetValue("BUKRS", _companyCode);
                testfn.SetValue("VKONT", _caNumber);

                testfn.Invoke(dest);

                IRfcTable _T_PDF = testfn.GetTable("IT_PDF");
                //IRfcTable bapiReturn = testfn.GetTable("RETURN");

                //objOutPut.binReturnTable(dtReturn, bapiReturn.GetString("TYPE"), bapiReturn.GetString("ID"), bapiReturn.GetString("NUMBER"), bapiReturn.GetString("MESSAGE"), bapiReturn.GetString("LOG_NO"),
                //     bapiReturn.GetString("LOG_MSG_NO"), bapiReturn.GetString("MESSAGE_V1"), bapiReturn.GetString("MESSAGE_V2"), bapiReturn.GetString("MESSAGE_V3"), bapiReturn.GetString("MESSAGE_V4"), "", "", "", "");

                dtIT_PDF = objOutPut.converttodotnetatble(_T_PDF);


            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }
        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtIT_PDF;
        BapiResult[1] = dtReturn;
        BapiResult[2] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        dsOutput.Tables.Add(BapiResult[1]);
        dsOutput.Tables.Add(BapiResult[2]);
        return dsOutput;
    }

    public DataSet get_ZBAPI_WHATSAPP_STATUS(string _Ord_Date, string _Com_Code, string _IFlag)
    {
        DataTable[] bapiResult = new DataTable[2];
        DataSet dsBAPIOutput = new DataSet("BAPI");
        ZBAPI_WHATSAPP_STATUS _objOutPut = new ZBAPI_WHATSAPP_STATUS();

        string messageText = "";
        string messageCode = "0";

        DataTable dtOutput = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_WHATSAPP_STATUS");

                testfn.SetValue("ORD_DATE", DateTime.ParseExact(_Ord_Date, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                testfn.SetValue("COMP_CODE", _Com_Code);
                testfn.SetValue("IFLAG", _IFlag);

                testfn.Invoke(dest);

                IRfcTable _OutPut = testfn.GetTable("OUTPUT");

                dtOutput = _objOutPut.converttodotnetatble(_OutPut);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtOutput;
        bapiResult[0].TableName = "Output";
        bapiResult[1] = dtMessageText;
        bapiResult[1].TableName = "Error";

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;

    }

    public DataSet get_ZBAPI_MCR_DOC_NUM(string _gAR_Date, string _gCOMP_CODE)
    {

        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_MCR_DOC_NUM _objOutPut = new ZBAPI_MCR_DOC_NUM();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtReturn = _objOutPut.makeReturnSystem();
        DataTable dtORDER_NUMBER = _objOutPut.makeOrderNumber();
        DataTable dtIT_OUTPUT = _objOutPut.makeIT_Output();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();


        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_MCR_DOC_NUM");
                testfn.SetValue("AR_DATE", DateTime.ParseExact(_gAR_Date, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                testfn.SetValue("COMPANY_CODE", _gCOMP_CODE);
                testfn.Invoke(dest);

                IRfcTable _Return = testfn.GetTable("RETURN");
                IRfcTable _OrderNumber = testfn.GetTable("ORDER_NUMBER");
                IRfcTable _ITOutPut = testfn.GetTable("IT_OUTPUT");

                dtReturn = _objOutPut.converttodotnetatble(_Return);
                dtReturn.TableName = "Return";

                dtORDER_NUMBER.Merge(_objOutPut.converttodotnetatble(_OrderNumber));
                dtORDER_NUMBER.TableName = "ORDER_NUMBER";

                dtIT_OUTPUT.Merge(_objOutPut.converttodotnetatble(_ITOutPut));
                dtIT_OUTPUT.TableName = "IT_OUTPUT";
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtReturn;
        bapiResult[1] = dtORDER_NUMBER;
        bapiResult[2] = dtIT_OUTPUT;
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }


    #endregion


    #region SAPPASSWORDRESET
    public DataSet ZBAPI_SEND_EMAIL_ID(string BNAME, string SAPconnection)
    {

        DataSet dsOutput = new DataSet("ZBAPI_SEND_EMAIL_ID");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_SEND_EMAIL_ID objOutPut = new ZBAPI_SEND_EMAIL_ID();
        DataTable[] BapiResult = new DataTable[3];
        DataTable dtDetails = new DataTable();
        DataTable dtReturn = new DataTable();


        DataTable dtMessageText = objOutPut.makeTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest;
                if (SAPconnection == "R3") { dest = RfcDestinationManager.GetDestination("R3"); }
                else
                {
                    dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                }
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_SEND_EMAIL_ID");
                testfn.SetValue("BNAME", BNAME);

                testfn.Invoke(dest);


                string SMTP_ADDR = testfn.GetString("SMTP_ADDR");
                string LOCK_MSG = testfn.GetString("LOCK_MSG");
                string STATUS_CODE = testfn.GetString("STATUS_CODE");

                objOutPut.pushOutputDataInDataTable(dtMessageText, SMTP_ADDR, LOCK_MSG, STATUS_CODE);
            }
        }

        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }


        BapiResult[0] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        return dsOutput;
    }

    public DataSet ZBAPI_SEND_URL(string BNAME, string MAILID, string URL, string MESSAGE, string SAPconnection)
    {

        DataSet dsOutput = new DataSet("ZBAPI_SEND_URL");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_SEND_URL objOutPut = new ZBAPI_SEND_URL();
        DataTable[] BapiResult = new DataTable[3];
        DataTable dtDetails = new DataTable();
        DataTable dtReturn = new DataTable();


        DataTable dtMessageText = objOutPut.makeTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest;
                if (SAPconnection == "R3") { dest = RfcDestinationManager.GetDestination("R3"); }
                else
                {
                    dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                }
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_SEND_URL");
                testfn.SetValue("BNAME", BNAME);
                testfn.SetValue("SMTP_ADDR", MAILID);
                testfn.SetValue("URL", URL);
                testfn.SetValue("MESSAGE", MESSAGE);

                testfn.Invoke(dest);


                string LOCK_MSG = testfn.GetString("LOCK_MSG");


                objOutPut.pushOutputDataInDataTable(dtMessageText, BNAME, MAILID, URL, MESSAGE);
            }
        }

        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }


        BapiResult[0] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        return dsOutput;
    }


    public DataSet ZBAPI_reset_password(string BNAME, string PASSWORD, string SAPconnection)
    {

        DataSet dsOutput = new DataSet("ZBAPI_RESET_PASSWORD");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_reset_password objOutPut = new ZBAPI_reset_password();
        DataTable[] BapiResult = new DataTable[3];
        DataTable dtDetails = new DataTable();
        DataTable dtReturn = new DataTable();


        DataTable dtMessageText = objOutPut.makeTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest;
                if (SAPconnection == "R3") { dest = RfcDestinationManager.GetDestination("R3"); }
                else
                {
                    dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                }
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_RESET_PASSWORD");
                testfn.SetValue("BNAME", BNAME);
                testfn.SetValue("PASSWORD", PASSWORD);

                testfn.Invoke(dest);



                string LOCK_MSG = testfn.GetString("LOCK_MSG");
                string MESSAGE = testfn.GetString("MESSAGE");

                objOutPut.pushOutputDataInDataTable(dtMessageText, LOCK_MSG, MESSAGE);
            }
        }

        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }


        BapiResult[0] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        return dsOutput;
    }
    #endregion

    public DataSet get_ZBAPI_STATUS163(string _gVKONT)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_STATUS163 _objOutPut = new ZBAPI_STATUS163();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtString = _objOutPut.makeDataDetails();
        DataTable dtReturn = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_STATUS163");

                testfn.SetValue("VKONT", _gVKONT);
                testfn.Invoke(dest);

                string _table_Retun = testfn.GetString("STATUS");
                IRfcStructure _RETURN = testfn.GetStructure("RETURN");

                if (_table_Retun != "")
                    _objOutPut.bind_makeDataDetails(dtString, _table_Retun);
                dtReturn = _objOutPut.CreateOutputDataTable(_RETURN.GetString(0), _RETURN.GetString(1), _RETURN.GetString(2), _RETURN.GetString(3), _RETURN.GetString(4), _RETURN.GetString(5), _RETURN.GetString(6), _RETURN.GetString(7), _RETURN.GetString(8), _RETURN.GetString(9), _RETURN.GetString(10), _RETURN.GetString(11), _RETURN.GetString(12), _RETURN.GetString(13));
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtString;
        bapiResult[1] = dtReturn;
        bapiResult[2] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);

        return dsBAPIOutput;
    }

    public DataSet get_ZBI_BAPI_CA_DUES_NIC(string _gCA_NO)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBI_BAPI_CA_DUES_NIC _objOutPut = new ZBI_BAPI_CA_DUES_NIC();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtTX_BILLPRINT = _objOutPut.make_Result();
        DataTable dtRetrun = _objOutPut.formDataTableError();
        DataTable dtZERR = _objOutPut.make_ZERR();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();


        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBI_BAPI_CA_DUES_NIC");

                testfn.SetValue("CONTRACT_ACCOUNT", _gCA_NO);

                testfn.Invoke(dest);

                IRfcTable _TX_BILLPRINT = testfn.GetTable("TX_BILLPRINT");
                IRfcTable _RETURN = testfn.GetTable("RETURN");
                IRfcTable _ZERR = testfn.GetTable("ZERR");

                dtTX_BILLPRINT = _objOutPut.convertTX_BILLPRINT(_TX_BILLPRINT);
                dtRetrun = _objOutPut.converttodotnetatble(_RETURN);
                dtZERR = _objOutPut.convertTX_BILLPRINT(_ZERR);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtTX_BILLPRINT;
        bapiResult[0].TableName = "TX_BILLPRINT";
        bapiResult[1] = dtRetrun;
        bapiResult[1].TableName = "Retrun";
        bapiResult[2] = dtZERR;
        bapiResult[2].TableName = "ZERR";
        bapiResult[3] = dtMessageText;
        bapiResult[3].TableName = "messageTable";

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }
    /* Z_BAPI_DSS_ISU_CA_DISPLAY */

    public DataSet get_ZBAPI_WHATSAPP_INTEGRATION(string _orderID, string _iFlag)
    {
        DataTable[] bapiResult = new DataTable[4];
        DataSet dsBAPIOutput = new DataSet("ZBAPI_WHATSAPP_INTEGRATION");
        ZBAPI_WHATSAPP_INTEGRATION _objOutPut = new ZBAPI_WHATSAPP_INTEGRATION();

        string messageText = "";
        string messageCode = "0";

        DataTable dtFlag = new DataTable();
        DataTable dtReturn = _objOutPut.formDataTableError();
        DataTable dtT_PDF = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_WHATSAPP_INTEGRATION");

                testfn.SetValue("ORDERID", _orderID);
                testfn.SetValue("IFLAG", _iFlag);

                testfn.Invoke(dest);

                string _flag = testfn.GetString("FLAG");
                IRfcTable _T_PDF = testfn.GetTable("T_PDF");

                //IRfcStructure bapiReturn = testfn.GetStructure("RETURN");
                //dtReturn = _objOutPut.formDataTableError();

                //_objOutPut.addRowToDTError(dtReturn, bapiReturn.GetString("TYPE"), bapiReturn.GetString("ID"), bapiReturn.GetString("NUMBER"), bapiReturn.GetString("MESSAGE"), bapiReturn.GetString("LOG_NO"),
                //    bapiReturn.GetString("LOG_MSG_NO"), bapiReturn.GetString("MESSAGE_V1"), bapiReturn.GetString("MESSAGE_V2"), bapiReturn.GetString("MESSAGE_V3"), bapiReturn.GetString("MESSAGE_V4"), "", "", "", "");

                dtT_PDF = _objOutPut.converttodotnetatble(_T_PDF);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtFlag;
        bapiResult[0].TableName = "Flag";
        bapiResult[1] = dtReturn;
        bapiResult[1].TableName = "Return";
        bapiResult[2] = dtT_PDF;
        bapiResult[2].TableName = "T_PDF";
        bapiResult[3] = dtMessageText;
        bapiResult[3].TableName = "Error";

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;

    }

    public DataSet get_Z_BAPI_CA_DISPLAY_WHATSAPP(string _CA_NUMBER, string _CONTRACT, string _SERIALNO, string _IMPORT_CRNNUMBER, string _IMPORT_TELEPHONE_NO, string _IMPORT_KNUMBER)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        Z_BAPI_CA_DISPLAY_WHATSAPP _objOutPut = new Z_BAPI_CA_DISPLAY_WHATSAPP();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[5];

        DataTable dtEXPORT_CADETAILS = new DataTable();
        DataTable dtEXPORT_CSDETAILS = new DataTable();
        DataTable dtBAPIMESSAGE = new DataTable();
        DataTable dtRETURN = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {


                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("Z_BAPI_CA_DISPLAY_WHATSAPP");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPIIVRST");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("CA_NUMBER", _CA_NUMBER);
                articol.SetValue("CONTRACT", _CONTRACT);
                articol.SetValue("SERIALNO", _SERIALNO);

                testfn.SetValue("IMPORT_CANUMBER", articol);
                testfn.SetValue("IMPORT_CRNNUMBER", _IMPORT_CRNNUMBER);
                testfn.SetValue("IMPORT_TELEPHONE_NO", _IMPORT_TELEPHONE_NO);
                testfn.SetValue("IMPORT_KNUMBER", _IMPORT_KNUMBER);
                testfn.Invoke(dest);


                IRfcTable _EXPORT_CADETAILS = testfn.GetTable("EXPORT_CADETAILS");
                IRfcTable _EXPORT_CSDETAILS = testfn.GetTable("EXPORT_CSDETAILS");
                IRfcTable _BAPIMESSAGE = testfn.GetTable("BAPIMESSAGE");
                IRfcTable _RETURN = testfn.GetTable("RETURN");

                dtEXPORT_CADETAILS = _objOutPut.converttodotnetatble(_EXPORT_CADETAILS);
                dtEXPORT_CSDETAILS = _objOutPut.converttodotnetatble(_EXPORT_CSDETAILS);
                dtBAPIMESSAGE = _objOutPut.converttodotnetatble(_BAPIMESSAGE);
                dtRETURN = _objOutPut.converttodotnetatble(_RETURN);

                if (dtEXPORT_CADETAILS.Rows[0]["FAX_NUMBER"].ToString() == "")
                {
                    dtEXPORT_CADETAILS.Rows[0]["FAX_NUMBER"] = "null";
                    dtEXPORT_CADETAILS.AcceptChanges();
                }
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtEXPORT_CADETAILS;
        bapiResult[0].TableName = "EXPORT_CADETAILS";
        bapiResult[1] = dtEXPORT_CSDETAILS;
        bapiResult[1].TableName = "EXPORT_CSDETAILS";
        bapiResult[2] = dtBAPIMESSAGE;
        bapiResult[2].TableName = "BAPIMESSAGE";
        bapiResult[3] = dtRETURN;
        bapiResult[3].TableName = "RETURN";
        bapiResult[4] = dtMessageText;
        bapiResult[4].TableName = "Error";

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);
        dsBAPIOutput.Tables.Add(bapiResult[4]);


        return dsBAPIOutput;
    }

    public DataSet get_ZBAPI_CA_ORDDETAILS(string _V_VKONT)
    {

        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_CA_ORDDETAILS _objOutPut = new ZBAPI_CA_ORDDETAILS();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtFlag = _objOutPut.make_Table_Flag();
        DataTable dtRETURN_MESSAGES = new DataTable();
        DataTable dtIT_OUTPUT = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();


        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CA_ORDDETAILS");
                testfn.SetValue("V_VKONT", _V_VKONT);

                testfn.Invoke(dest);

                string _Flag = testfn.GetString("FLAG");
                IRfcTable _RETURN_MESSAGES = testfn.GetTable("RETURN_MESSAGES");
                IRfcTable _IT_OUTPUT = testfn.GetTable("IT_OUTPUT");

                _objOutPut.bind_Table_Flag(dtFlag, _Flag);
                dtRETURN_MESSAGES = _objOutPut.converttodotnetatble(_RETURN_MESSAGES, "RETURN_MESSAGES");
                dtIT_OUTPUT = _objOutPut.converttodotnetatble(_IT_OUTPUT, "IT_OUTPUT");
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtFlag;
        bapiResult[1] = dtRETURN_MESSAGES;
        bapiResult[2] = dtIT_OUTPUT;
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }

    public DataSet get_ZBAPI_CA_DISPLAY_CRM(string _IMPORT_CANUMBER)
    {

        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_CA_DISPLAY_CRM _objOutPut = new ZBAPI_CA_DISPLAY_CRM();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtEXPORT_CADETAILS = new DataTable();
        DataTable dtBAPIMESSAGE = new DataTable();
        DataTable dtRETURN = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();


        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CA_DISPLAY_CRM");
                testfn.SetValue("IMPORT_CANUMBER", _IMPORT_CANUMBER);

                testfn.Invoke(dest);

                IRfcTable _EXPORT_CADETAILS = testfn.GetTable("EXPORT_CADETAILS");
                IRfcTable _BAPIMESSAGE = testfn.GetTable("BAPIMESSAGE");
                IRfcTable _RETURN = testfn.GetTable("RETURN");

                dtEXPORT_CADETAILS = _objOutPut.converttodotnetatble(_EXPORT_CADETAILS);
                dtBAPIMESSAGE = _objOutPut.converttodotnetatble(_BAPIMESSAGE);
                dtRETURN = _objOutPut.converttodotnetatble(_RETURN);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtEXPORT_CADETAILS;
        bapiResult[1] = dtBAPIMESSAGE;
        bapiResult[2] = dtRETURN;
        bapiResult[3] = dtMessageText;



        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }

    public DataSet get_ZCSUPDAT_PERSONAL_DETAILS(string _PARTNER, string _NAME_FIRST, string _NAMEMIDDLE, string _NAME_LAST, string _NAME_LST2, string _STR_SUPPL1, string _STR_SUPPL2, string _HOUSE_NUM1, string _STREET, string _STR_SUPPL3, string _TEL_NUMBER, string _SMTP_ADDR, string _FAX_NUMBER)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZCSUPDAT_PERSONAL_DETAILS _objOutPut = new ZCSUPDAT_PERSONAL_DETAILS();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtFlag = _objOutPut.makeFlagTable();
        DataTable dtRet2Table = _objOutPut.formDataTableError();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();


        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZCSUPDAT_PERSONAL_DETAILS");

                testfn.SetValue("PARTNER", _PARTNER);
                testfn.SetValue("NAME_FIRST", _NAME_FIRST);
                testfn.SetValue("NAMEMIDDLE", _NAMEMIDDLE);
                testfn.SetValue("NAME_LAST", _NAME_LAST);
                testfn.SetValue("NAME_LST2", _NAME_LST2);
                testfn.SetValue("STR_SUPPL1", _STR_SUPPL1);
                testfn.SetValue("STR_SUPPL2", _STR_SUPPL2);
                testfn.SetValue("HOUSE_NUM1", _HOUSE_NUM1);
                testfn.SetValue("STREET", _STREET);
                testfn.SetValue("STR_SUPPL3", _STR_SUPPL3);
                testfn.SetValue("TEL_NUMBER", _TEL_NUMBER);
                testfn.SetValue("SMTP_ADDR", _SMTP_ADDR);
                testfn.SetValue("FAX_NUMBER", _FAX_NUMBER);

                testfn.Invoke(dest);

                string _Flag = testfn.GetString("FLAG");
                IRfcTable irfcReturn = testfn.GetTable("RETURN");

                DataTable _dtReturn = _objOutPut.converttodotnetatble(irfcReturn);

                if (_dtReturn.Rows.Count > 0)
                    _objOutPut.addRowToDTError(dtRet2Table, _dtReturn.Rows[0][0].ToString(), _dtReturn.Rows[0][1].ToString(), _dtReturn.Rows[0][2].ToString(), _dtReturn.Rows[0][3].ToString(), _dtReturn.Rows[0][4].ToString(), _dtReturn.Rows[0][5].ToString(), _dtReturn.Rows[0][6].ToString(), _dtReturn.Rows[0][7].ToString(), _dtReturn.Rows[0][8].ToString(), _dtReturn.Rows[0][9].ToString());

                if (_Flag != "")
                    _objOutPut.bindFlagTable(dtFlag, _Flag);
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtFlag;
        bapiResult[0].TableName = "FLAG";
        bapiResult[1] = dtRet2Table;
        bapiResult[1].TableName = "Return";
        bapiResult[2] = dtMessageText;
        bapiResult[2].TableName = "messagTable";

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);

        return dsBAPIOutput;
    }
    //public DataSet get_ZBAPI_ZCS_CLI_WEB(string _gTelephoneNumber)
    //{
    //    DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
    //    ZBAPI_ZCS_CLI_WEB _objOutPut = new ZBAPI_ZCS_CLI_WEB();
    //    string messageText = "";
    //    string messageCode = "0";

    //    DataTable[] bapiResult = new DataTable[3];

    //    DataTable dtConsumerDetails = _objOutPut.makeConsumerDetailsDataTable();
    //    DataTable dtreturnTable = _objOutPut.formDataTableError();
    //    DataTable dtMessageText = _objOutPut.makeMessageTextTable();
    //    try
    //    {
    //        clsConnect cfg = new clsConnect();

    //        if (destinationIsInialised == false)
    //        {
    //            RfcDestinationManager.RegisterDestinationConfiguration(cfg);
    //            destinationIsInialised = true;
    //        }

    //        if (destinationIsInialised == true)
    //        {
    //            RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
    //            RfcRepository repo = dest.Repository;
    //            IRfcFunction testfn = repo.CreateFunction("ZBAPI_ZCS_CLI_WEB");
    //            testfn.SetValue("TELEPHONE_NUMBER", _gTelephoneNumber);
    //            testfn.Invoke(dest);

    //            IRfcTable EnfDetails = testfn.GetTable("TX_CONSUMERDETAILS");
    //            IRfcTable bapiTable = testfn.GetTable("RETURN");

    //            dtConsumerDetails = _objOutPut.converttodotnetatble(EnfDetails);
    //            dtreturnTable = _objOutPut.converttodotnetatble(bapiTable);
    //        }
    //    }
    //    catch (RfcCommunicationException ex)
    //    {
    //        messageText = "RfcCommunicationException :" + ex.Message.ToString();
    //        messageCode = "91";
    //    }
    //    catch (RfcLogonException ex)
    //    {
    //        messageText = "RfcLogonException :" + ex.Message.ToString();
    //        messageCode = "92";
    //    }
    //    catch (RfcAbapException ex)
    //    {
    //        messageText = "RfcAbapException :" + ex.Message.ToString();
    //        messageCode = "93";
    //    }
    //    catch (Exception ex)
    //    {
    //        messageText = ex.Message.ToString();
    //        messageCode = "94";
    //    }

    //    if (messageText.Trim() != "")
    //    {
    //        _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
    //    }

    //    bapiResult[0] = _objOutPut.getCADetailsFormattedTable(dtConsumerDetails);
    //    bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtreturnTable);
    //    bapiResult[2] = dtMessageText;

    //    dsBAPIOutput.Tables.Add(bapiResult[0]);
    //    dsBAPIOutput.Tables.Add(bapiResult[1]);
    //    dsBAPIOutput.Tables.Add(bapiResult[2]);

    //    return dsBAPIOutput;

    //}
    public DataSet get_ZBAPI_ZCS_CLI_WEB(string _gTelephoneNumber)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_ZCS_CLI_WEB _objOutPut = new ZBAPI_ZCS_CLI_WEB();
        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtConsumerDetails = _objOutPut.makeConsumerDetailsDataTable();
        DataTable dtreturnTable = _objOutPut.formDataTableError();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_ZCS_CLI_WEB");
                testfn.SetValue("TELEPHONE_NUMBER", _gTelephoneNumber);
                testfn.Invoke(dest);

                IRfcTable EnfDetails = testfn.GetTable("TX_CONSUMERDETAILS");
                IRfcTable bapiTable = testfn.GetTable("RETURN");

                dtConsumerDetails = _objOutPut.converttodotnetatble(EnfDetails);
                dtreturnTable = _objOutPut.converttodotnetatble(bapiTable);
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = _objOutPut.getCADetailsFormattedTable(dtConsumerDetails);
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtreturnTable);
        bapiResult[2] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);

        return dsBAPIOutput;

    }

    public DataSet get_ZCS_DSS_ORDDETAILS(string _OrderNumber)
    {

        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZCS_DSS_ORDDETAILS _objOutPut = new ZCS_DSS_ORDDETAILS();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[6];

        DataTable dtCUST_NAME = _objOutPut.makeCUST_NAMETable("CUST_NAME");
        DataTable dtZORDDETAIL = _objOutPut.make_Table_ORDDETAIL();
        DataTable dtZPARTNER_ADDRESS = _objOutPut.make_Table_ZPARTNER_ADDRESS();
        DataTable dtZORDER_DETAIL = _objOutPut.make_Table_ZORDER_DETAIL();
        DataTable dtRETURN_MSG = _objOutPut.makeCUST_NAMETable("RETURN_MSG");
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();


        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZCS_DSS_ORDDETAILS");
                testfn.SetValue("VAUFNR", _OrderNumber);

                testfn.Invoke(dest);

                string _CUST_NAME = testfn.GetString("CUST_NAME");
                IRfcStructure _ZORDDETAIL = testfn.GetStructure("ZORDDETAIL");
                IRfcStructure _ZPARTNER_ADDRESS = testfn.GetStructure("ZPARTNER_ADDRESS");
                IRfcStructure _ZORDER_DETAIL = testfn.GetStructure("ZORDER_DETAIL");
                string _RETURN_MSG = testfn.GetString("RETURN_MSG");

                _objOutPut.bind_CUST_NAMETable(dtCUST_NAME, _CUST_NAME);
                _objOutPut.bind_CUST_NAMETable(dtRETURN_MSG, _RETURN_MSG);

                _objOutPut.bind_Table_ORDDETAIL(dtZORDDETAIL, _ZORDDETAIL.GetString(0).ToString(), _ZORDDETAIL.GetString(1).ToString(), _ZORDDETAIL.GetString(2).ToString(), _ZORDDETAIL.GetString(3).ToString(),
                    _ZORDDETAIL.GetString(4).ToString(), _ZORDDETAIL.GetString(5).ToString(), _ZORDDETAIL.GetString(6).ToString(), _ZORDDETAIL.GetString(7).ToString(), _ZORDDETAIL.GetString(8).ToString(),
                    _ZORDDETAIL.GetString(9).ToString(), _ZORDDETAIL.GetString(10).ToString(), _ZORDDETAIL.GetString(11).ToString(), _ZORDDETAIL.GetString(12).ToString(), _ZORDDETAIL.GetString(13).ToString(),
                    _ZORDDETAIL.GetString(14).ToString(), _ZORDDETAIL.GetString(15).ToString(), _ZORDDETAIL.GetString(16).ToString());

                _objOutPut.bind_Table_ZPARTNER_ADDRESS(dtZPARTNER_ADDRESS, _ZPARTNER_ADDRESS.GetString(0).ToString(), _ZPARTNER_ADDRESS.GetString(1).ToString(), _ZPARTNER_ADDRESS.GetString(2).ToString(), _ZPARTNER_ADDRESS.GetString(3).ToString(),
                    _ZPARTNER_ADDRESS.GetString(4).ToString(), _ZPARTNER_ADDRESS.GetString(5).ToString(), _ZPARTNER_ADDRESS.GetString(6).ToString(), _ZPARTNER_ADDRESS.GetString(7).ToString());

                _objOutPut.bind_Table_ZORDER_DETAIL(dtZORDER_DETAIL, _ZORDER_DETAIL.GetString(0).ToString(), _ZORDER_DETAIL.GetString(1).ToString(), _ZORDER_DETAIL.GetString(2).ToString(), _ZORDER_DETAIL.GetString(3).ToString(),
                    _ZORDER_DETAIL.GetString(4).ToString(), _ZORDER_DETAIL.GetString(5).ToString(), _ZORDER_DETAIL.GetString(6).ToString(), _ZORDDETAIL.GetString(7).ToString(), _ZORDER_DETAIL.GetString(8).ToString(),
                    _ZORDER_DETAIL.GetString(9).ToString(), _ZORDER_DETAIL.GetString(10).ToString(), _ZORDER_DETAIL.GetString(11).ToString(), _ZORDER_DETAIL.GetString(12).ToString());

            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtCUST_NAME;
        bapiResult[1] = dtZORDDETAIL;
        bapiResult[2] = dtZPARTNER_ADDRESS;
        bapiResult[3] = dtZORDER_DETAIL;
        bapiResult[4] = dtRETURN_MSG;
        bapiResult[5] = dtMessageText;



        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);
        dsBAPIOutput.Tables.Add(bapiResult[4]);
        dsBAPIOutput.Tables.Add(bapiResult[5]);

        return dsBAPIOutput;
    }

    public DataSet get_ZBAPI_FICA_DEMAND_DUE_DATE(string _gORDER_NO)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_FICA_DEMAND_NOTE _objOutPut = new ZBAPI_FICA_DEMAND_NOTE();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtDataDetail = _objOutPut.makeDataDetails_Dua_date();
        DataTable dtReturn = _objOutPut.makeReturnTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_FICA_DEMAND_DUE_DATE");

                testfn.SetValue("ORDER_NO", _gORDER_NO);
                testfn.Invoke(dest);

                IRfcTable _table_DataDetail = testfn.GetTable("OUTPUT");
                IRfcTable _table_Retun = testfn.GetTable("RETURN");

                if (_table_DataDetail.RowCount > 0)
                    dtDataDetail = _objOutPut.bindDataTable_Due_Date(_objOutPut.converttodotnetatble(_table_DataDetail));

                if (_table_Retun.RowCount > 0)
                    dtReturn = _objOutPut.makeBAPIRET2TABLE(_objOutPut.converttodotnetatble(_table_Retun));

            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtDataDetail;
        bapiResult[1] = dtReturn;
        bapiResult[2] = dtMessageText;


        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);

        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_MDI_LETTER(string CANUMBER)
    {
        int i = 0;
        DataSet dsOutput = new DataSet("BILL_DET");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_BILL_DET objOutPut = new ZBAPI_BILL_DET();
        DataTable[] BapiResult = new DataTable[1];
        DataTable dtPDFDetails = new DataTable();

        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_MDI_LETTER");
                testfn.SetValue("IN_VKONT", CANUMBER);
                testfn.Invoke(dest);

                IRfcTable _IT_INPUT = testfn.GetTable("T_PDF");
                dtPDFDetails = objOutPut.converttodotnetatble(_IT_INPUT);
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }

        BapiResult[0] = dtPDFDetails;
        dsOutput.Tables.Add(BapiResult[0]);

        return dsOutput;
    }

    public DataSet Get_ZBAPI_ENF_INSTL(string _CompanyCode, string _ContractAcc, string _CaseID)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        BAPI_ISUSMORDER_USERSTATUSSET _objOutPut = new BAPI_ISUSMORDER_USERSTATUSSET();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[6];
        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtOutputResult = new DataTable();
        DataTable dtOutputTot = new DataTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtRet3Table = new DataTable();
        DataTable dtRet4Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        string _sDIVISION = string.Empty, _sBP_NUMBER = string.Empty, _sUSER_NAME = string.Empty, _sR_C_NAME = string.Empty, _sADDRESS = string.Empty;
        string _sTELEPHONE_NO = string.Empty, _sALTERNATE_PAYER = string.Empty, _sDATE_OF_INSPECTION = string.Empty, _sCONN_LOAD = string.Empty;
        string _sSANCT_LOAD = string.Empty, _sENFORCEMENT_TYPE = string.Empty, _sTARIFF_CATEGORY = string.Empty, _sCASEID = string.Empty, _sCASEID_NUMBER = string.Empty, _NEW_CA = string.Empty;
        string _SOLD_CA = string.Empty, _sBILL_NO = string.Empty, _sBILL_ISSUE_DATE = string.Empty, _sDUE_DATE = string.Empty, _sTOTAL_CHARGE = string.Empty;
        string _sCREDIT_ASSED_PERIOD = string.Empty, _sSUNDRY = string.Empty, _sNET_CHARGE = string.Empty, _sLPSC_AMOUNT = string.Empty, _sMETER_COST = string.Empty;
        string _sMETER_TEST_CHARGE = string.Empty, _sLOCK_REASON = string.Empty, _sSETTLED_DATE = string.Empty, _sSETTLED_AMT_REMARK = string.Empty;
        string _sCHEQUE_DISHONORED = string.Empty, _sNET_DUE = string.Empty;

        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_ENF_INSTL");

                testfn.SetValue("COMPANYCODE", _CompanyCode);
                testfn.SetValue("CONTRACTACCOUNT", _ContractAcc);
                testfn.SetValue("CASEID", _CaseID);
                testfn.Invoke(dest);

                _sDIVISION = (string)testfn.GetValue("DIVISION");
                _sBP_NUMBER = (string)testfn.GetValue("BP_NUMBER");
                _sUSER_NAME = (string)testfn.GetValue("USER_NAME");
                _sR_C_NAME = (string)testfn.GetValue("R_C_NAME");
                _sADDRESS = (string)testfn.GetValue("ADDRESS");
                _sTELEPHONE_NO = (string)testfn.GetValue("TELEPHONE_NO");
                _sALTERNATE_PAYER = (string)testfn.GetValue("ALTERNATE_PAYER");
                _sDATE_OF_INSPECTION = (string)testfn.GetValue("DATE_OF_INSPECTION");
                _sCONN_LOAD = (string)testfn.GetValue("CONN_LOAD");
                _sSANCT_LOAD = (string)testfn.GetValue("SANCT_LOAD");
                _sENFORCEMENT_TYPE = (string)testfn.GetValue("ENFORCEMENT_TYPE");
                _sTARIFF_CATEGORY = (string)testfn.GetValue("TARIFF_CATEGORY");
                _sCASEID = (string)testfn.GetValue("CASEID");
                _sCASEID_NUMBER = (string)testfn.GetValue("CASEID_NUMBER");
                _NEW_CA = (string)testfn.GetValue("NEW_CA");
                _SOLD_CA = (string)testfn.GetValue("OLD_CA");
                _sBILL_NO = (string)testfn.GetValue("BILL_NO");
                _sBILL_ISSUE_DATE = (string)testfn.GetValue("BILL_ISSUE_DATE");
                _sDUE_DATE = (string)testfn.GetValue("DUE_DATE");
                _sTOTAL_CHARGE = (string)testfn.GetValue("TOTAL_CHARGE");
                _sCREDIT_ASSED_PERIOD = (string)testfn.GetValue("CREDIT_ASSED_PERIOD");
                _sSUNDRY = (string)testfn.GetValue("SUNDRY");
                _sNET_CHARGE = (string)testfn.GetValue("NET_CHARGE");
                _sLPSC_AMOUNT = (string)testfn.GetValue("LPSC_AMOUNT");
                _sMETER_COST = (string)testfn.GetValue("MTR_COST_INCL_IN_TOTAL_CHG");
                _sMETER_TEST_CHARGE = (string)testfn.GetValue("MTR_TEST_CH_INCL_IN_TOTAL_CHG");
                _sLOCK_REASON = (string)testfn.GetValue("LOCK_REASON");
                _sSETTLED_DATE = (string)testfn.GetValue("SETTLED_DATE");
                _sSETTLED_AMT_REMARK = (string)testfn.GetValue("SETTLED_AMT_REMARK");
                _sCHEQUE_DISHONORED = (string)testfn.GetValue("CHEQUE_DISHONORED");
                _sNET_DUE = (string)testfn.GetValue("NET_DUE");

                dtOutputResult = _objOutPut.CreateOutputDataTable1(_sDIVISION, _sBP_NUMBER, _sUSER_NAME, _sR_C_NAME, _sADDRESS, _sTELEPHONE_NO, _sALTERNATE_PAYER,
                                            _sDATE_OF_INSPECTION, _sCONN_LOAD, _sSANCT_LOAD, _sENFORCEMENT_TYPE, _sTARIFF_CATEGORY, _sCASEID,_sCASEID_NUMBER, _NEW_CA,
                                            _SOLD_CA, _sBILL_NO, _sBILL_ISSUE_DATE, _sDUE_DATE, _sTOTAL_CHARGE, _sCREDIT_ASSED_PERIOD, _sSUNDRY, _sNET_CHARGE,
                                            _sLPSC_AMOUNT, _sMETER_COST, _sMETER_TEST_CHARGE, _sLOCK_REASON, _sSETTLED_DATE, _sSETTLED_AMT_REMARK,
                                            _sCHEQUE_DISHONORED, _sNET_DUE);

                IRfcTable returnTable2 = testfn.GetTable("INSTALMENT_DETAILS");
                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);

                double _dTotPaidAmt = 0.0, _dTotLPSC = 0.0, _dTotNetAmt = 0.0;
                for (int i = 0; i < dtRet2Table.Rows.Count; i++)
                {
                    if (dtRet2Table.Rows[i]["STATUS"].ToString().Trim() == "Paid")
                    {
                        _dTotPaidAmt += Convert.ToDouble(dtRet2Table.Rows[i]["AMOUNT"].ToString().Trim());
                        _dTotLPSC += Convert.ToDouble(dtRet2Table.Rows[i]["LPSC"].ToString().Trim());
                        _dTotNetAmt += Convert.ToDouble(dtRet2Table.Rows[i]["NET"].ToString().Trim());
                    }
                }

                dtOutputTot = _objOutPut.CreateOutputData_PaidTable(_dTotPaidAmt.ToString(), _dTotLPSC.ToString(), _dTotNetAmt.ToString());

                //_objOutPut.addRowToDTOutPut(dtRet2Table, bapiTable.GetString("S_N"), bapiTable.GetString("DUE_DATE"), bapiTable.GetString("AMOUNT"), 
                //                            bapiTable.GetString("LPSC"), bapiTable.GetString("NET"),bapiTable.GetString("STATUS"), bapiTable.GetString("MOP"), 
                //                            bapiTable.GetString("RECEIPT_NO"), bapiTable.GetString("PAYMENT_DATE"));


                // dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);

                IRfcTable returnTable3 = testfn.GetTable("LOAD_WINTER_SUMMER");
                dtRet3Table = _objOutPut.converttodotnetatble(returnTable3);

                IRfcTable returnTable4 = testfn.GetTable("RETURN_MESSAGES");
                dtRet4Table = _objOutPut.converttodotnetatble(returnTable4);

                //IRfcStructure bapiTable = testfn.GetStructure("OUTPUT_STRUC");
                //dtRet2Table = _objOutPut.formDataTableOutPut();

                //_objOutPut.addRowToDTError(dtRet2Table, bapiTable.GetString("COMP_CODE"), bapiTable.GetString("CONTRACT_ACCOUNT"), bapiTable.GetString("OUT_AMT"), bapiTable.GetString("NAME"), bapiTable.GetString("ADDRESS"),
                //    bapiTable.GetString("TEL1_NUMBR"), bapiTable.GetString("E_MAIL"), bapiTable.GetString("ACCT_DET_ID"), bapiTable.GetString("ACCT_CLASS"), bapiTable.GetString("FLAG"),
                //    bapiTable.GetString("MESSAGE"), bapiTable.GetString("ORDER_NO"), bapiTable.GetString("OPBEL"), bapiTable.GetString("DUE_DATE"));


            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtOutputResult;
        bapiResult[1] = dtRet2Table;
        bapiResult[2] = dtRet3Table;
        bapiResult[3] = dtRet4Table;
        bapiResult[4] = dtMessageText;
        bapiResult[5] = dtOutputTot;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);
        dsBAPIOutput.Tables.Add(bapiResult[4]);
        dsBAPIOutput.Tables.Add(bapiResult[5]);

        return dsBAPIOutput;
    }

    public DataSet Get_ZBAPI_CANCEL_ORD_DATA(string _strComp_Code, string _strOrderType, string _strCancelDate, string _strCompDate)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_ENFORCEMENT _objOutPut = new ZBAPI_ENFORCEMENT();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtOrdDetails = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();
        DataTable dtMetChangeTable = new DataTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CANCEL_ORD_DATA");
                testfn.SetValue("COMPANY_CODE", _strComp_Code);
                testfn.SetValue("CANCEL_DATE", _strCancelDate);
                testfn.SetValue("ORDER_TYPE", _strOrderType);
                testfn.SetValue("TECH_COMP_DATE", _strCompDate);

                testfn.Invoke(dest);

                IRfcTable OrdDetails = testfn.GetTable("IT_OUTPUT");
                dtOrdDetails = _objOutPut.converttodotnetatble(OrdDetails);

                //IRfcStructure bapiTable = testfn.GetStructure("RETURN");
                //dtMetChangeTable = _objOutPut.converttodotnetatble(bapiTable);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtOrdDetails;
        bapiResult[1] = _objOutPut.transferEnfMeterChangeData(dtMetChangeTable);
        bapiResult[2] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);

        return dsBAPIOutput;
    }

    public DataSet GETZBAPI_CA_MCD_DETAILS(string caNumber, string companyCode)
    {
        string messageText = "";
        string messageCode = "0";

        DataTable dtMessageText = makeMessageTextTable();
        DataSet objDs = new DataSet("BAPI_RESULT");


        

        DataTable[] bapiResult = new DataTable[2];

        DataTable dataTable = new DataTable();       
        DataTable dtErrorTable = new DataTable();
       

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination destination = RfcDestinationManager.GetDestination("mySAPdestination1"); // Replace with your SAP destination name
                RfcRepository repo = destination.Repository;
                IRfcFunction function = repo.CreateFunction("ZBAPI_CA_MCD_DETAILS"); // Replace with your BAPI name


                function.SetValue("CA_NUMBER", caNumber);
                function.SetValue("COMP_CODE", companyCode);

                function.Invoke(destination);

                // Check for any errors returned by the BAPI
                // Retrieve the RETURN structure
                IRfcStructure returnStructure = function.GetStructure("RETURN");
                string type = returnStructure.GetString("TYPE");
                string message = returnStructure.GetString("MESSAGE");


                dataTable.Columns.Add("CA_NUMBER_O", typeof(string));
                dataTable.Columns.Add("DIVISION", typeof(string));
                //dataTable.Columns.Add("MOVE_IN_DATE", typeof(DateTime));
                dataTable.Columns.Add("MOVE_IN_DATE", typeof(string));
                dataTable.Columns.Add("COMP_CODE_O", typeof(string));
                dataTable.Columns.Add("UPIC_NO", typeof(string));
                dataTable.Columns.Add("LOAD_KWH_VALUE", typeof(string));
                dataTable.Columns.Add("LOAD_KVA_VALUE", typeof(string));
                dataTable.Columns.Add("CONS_TYPE", typeof(string));
                dataTable.Columns.Add("CONN_TYPE", typeof(string));
                //dataTable.Columns.Add("MOVE_OUT_DATE", typeof(DateTime));
                dataTable.Columns.Add("MOVE_OUT_DATE", typeof(string));
                dataTable.Columns.Add("CATEG_CONN", typeof(string));
                dataTable.Columns.Add("HOUSE_NUM1", typeof(string));
                dataTable.Columns.Add("STREET", typeof(string));
                dataTable.Columns.Add("STR_SUPPL1", typeof(string));
                dataTable.Columns.Add("STR_SUPPL2", typeof(string));
                dataTable.Columns.Add("STR_SUPPL3", typeof(string));
                dataTable.Columns.Add("CITY1", typeof(string));
                dataTable.Columns.Add("POST_CODE1", typeof(string));
                dataTable.Columns.Add("BP_NAME", typeof(string));

                dataTable.Columns.Add("NAME_FIRST", typeof(string));
                dataTable.Columns.Add("NAMEMIDDLE", typeof(string));
                dataTable.Columns.Add("NAME_LAST", typeof(string));
                dataTable.Columns.Add("NAME_ORG1", typeof(string));
                dataTable.Columns.Add("NAME_ORG2", typeof(string));


                dataTable.Columns.Add("MOBILE_NO", typeof(string));
                dataTable.Columns.Add("E_MAIL", typeof(string));
                dataTable.Columns.Add("GSTIN_NO", typeof(string));
                dataTable.Columns.Add("TYPE_OF_AREA", typeof(string));

                dataTable.Columns.Add("Message", typeof(string));


                // Add a row with retrieved values
                DataRow row = dataTable.NewRow();
                row["CA_NUMBER_O"] = function.GetString("CA_NUMBER_O");
                row["DIVISION"] = function.GetString("DIVISION");
                row["MOVE_IN_DATE"] = DateTime.Parse(function.GetString("MOVE_IN_DATE"), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal).ToString("dd.MM.yyyy");
                row["COMP_CODE_O"] = function.GetString("COMP_CODE_O");
                row["UPIC_NO"] = function.GetString("UPIC_NO");
                row["LOAD_KWH_VALUE"] = function.GetString("LOAD_KWH_VALUE");
                row["LOAD_KVA_VALUE"] = function.GetString("LOAD_KVA_VALUE");
                row["CONS_TYPE"] = function.GetString("CONS_TYPE");
                row["CONN_TYPE"] = function.GetString("CONN_TYPE");
                row["MOVE_OUT_DATE"] = DateTime.Parse(function.GetString("MOVE_OUT_DATE"), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal).ToString("dd.MM.yyyy");
                row["CATEG_CONN"] = function.GetString("CATEG_CONN");
                row["HOUSE_NUM1"] = function.GetString("HOUSE_NUM1");
                row["STREET"] = function.GetString("STREET");
                row["STR_SUPPL1"] = function.GetString("STR_SUPPL1");
                row["STR_SUPPL2"] = function.GetString("STR_SUPPL2");
                row["STR_SUPPL3"] = function.GetString("STR_SUPPL3");
                row["CITY1"] = function.GetString("CITY1");
                row["POST_CODE1"] = function.GetString("POST_CODE1");
                row["BP_NAME"] = function.GetString("BP_NAME");

                row["NAME_FIRST"] = function.GetString("NAME_FIRST");
                row["NAMEMIDDLE"] = function.GetString("NAMEMIDDLE");
                row["NAME_LAST"] = function.GetString("NAME_LAST");
                row["NAME_ORG1"] = function.GetString("NAME_ORG1");
                row["NAME_ORG2"] = function.GetString("NAME_ORG2");


                row["MOBILE_NO"] = function.GetString("MOBILE_NO");
                row["E_MAIL"] = function.GetString("E_MAIL");
                row["GSTIN_NO"] = function.GetString("GSTIN_NO");
                row["TYPE_OF_AREA"] = function.GetString("TYPE_OF_AREA");
                row["Message"] = message;


                dataTable.Rows.Add(row);


                bapiResult[0] = dataTable;
               
               

            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[1] = dtMessageText;

        objDs.Tables.Add(bapiResult[0]);
        objDs.Tables.Add(bapiResult[1]);
              
        return objDs;
    }

    public void pushMessageTextInDataTable(DataTable dt, string messageCode, string messageToPush)
    {
        DataRow dr = dt.NewRow();
        dr["messageCode"] = messageCode;
        dr["messageText"] = messageToPush;
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

    public DataSet get_ZBAPI_PREPAID_RTGS_Common(string _gCOMP_CODE, string _gCONTRACT_ACCOUNT, string _gACCOUNT_TYPE, string _gAMOUNT, string _gFLAG)
    {

        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_PREPAID_RTGS _objOutPut = new ZBAPI_PREPAID_RTGS();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtSTRUC_OUT = _objOutPut.make_STRUC_OUT();
        DataTable dtReturn = _objOutPut.formDataTableError();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();


        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_PREPAID_RTGS");
                testfn.SetValue("COMP_CODE", _gCOMP_CODE);
                testfn.SetValue("CONTRACT_ACCOUNT", _gCONTRACT_ACCOUNT);
                testfn.SetValue("ACCOUNT_TYPE", _gACCOUNT_TYPE);
                testfn.SetValue("AMOUNT", _gAMOUNT);
                testfn.SetValue("FLAG", _gFLAG);
                testfn.Invoke(dest);


                IRfcStructure _STRUC_OUT = testfn.GetStructure("STRUC_OUT");
                IRfcStructure irfcReturn = testfn.GetStructure("RETURN");

                if (_STRUC_OUT.Count > 0)
                    _objOutPut.bind_STRUC_OUT(dtSTRUC_OUT, _STRUC_OUT.GetString("COMP_CODE"), _STRUC_OUT.GetString("CONTRACT"), _STRUC_OUT.GetString("CONTRACT_ACCOUNT"), _STRUC_OUT.GetString("NAME"),
                        _STRUC_OUT.GetString("ADDRESS"), _STRUC_OUT.GetString("TEL1_NUMBR"), _STRUC_OUT.GetString("E_MAIL"), _STRUC_OUT.GetString("SERIALNO"), _STRUC_OUT.GetString("ACCT_DET_ID"),
                        _STRUC_OUT.GetString("ACCT_CLASS"), _STRUC_OUT.GetString("DOC_ID"), _STRUC_OUT.GetString("MANUFACTURER"), _STRUC_OUT.GetString("FLAG"), _STRUC_OUT.GetString("MESSAGE"));

                if (irfcReturn.Count > 0)
                    _objOutPut.addRowToDTError(dtReturn, irfcReturn.GetString("TYPE"), irfcReturn.GetString("ID"), irfcReturn.GetString("NUMBER"), irfcReturn.GetString("MESSAGE"), irfcReturn.GetString("LOG_NO"),
                                    irfcReturn.GetString("LOG_MSG_NO"), irfcReturn.GetString("MESSAGE_V1"), irfcReturn.GetString("MESSAGE_V2"), irfcReturn.GetString("MESSAGE_V3"), irfcReturn.GetString("MESSAGE_V4"),
                                    irfcReturn.GetString("Parameter"), irfcReturn.GetString("Row"), irfcReturn.GetString("Field"), irfcReturn.GetString("System"));
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtSTRUC_OUT;
        bapiResult[1] = dtReturn;
        bapiResult[2] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);

        return dsBAPIOutput;
    }


    public DataSet get_ZBAPI_PM_LT_FL_EQ(string _SWERK, string _BEBER, string _TPLNR, string _PLTXT)
    {
        DataTable[] bapiResult = new DataTable[4];
        DataSet dsBAPIOutput = new DataSet("ZBAPI_PM_LT_FL_EQ");
        ZBAPI_PM_LT_LOADRESPONSE _objOutPut = new ZBAPI_PM_LT_LOADRESPONSE();

        string messageText = "";
        string messageCode = "0";

        DataTable dataTableFL = new DataTable();
        DataTable dataTableDT = new DataTable();
        DataTable dataTableLTACB = new DataTable();
        DataTable dataTableERROR = new DataTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("R3");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_PM_LT_FL_EQ");

                testfn.SetValue("SWERK", _SWERK);
                testfn.SetValue("BEBER", _BEBER);
                testfn.SetValue("TPLNR", _TPLNR);
                testfn.SetValue("PLTXT", _PLTXT);


                testfn.Invoke(dest);

                IRfcTable RtnFL_LIST = testfn.GetTable("FL_LIST");
                IRfcTable RtnDT_LIST = testfn.GetTable("DT_LIST");
                IRfcTable RtnLTACB_LIST = testfn.GetTable("LTACB_LIST");
                IRfcStructure RtnMsgTable = testfn.GetStructure("RETURN");




                List<FL_LIST> fL_LISTs = new List<FL_LIST>();
                List<DT_LIST> dT_LISTs = new List<DT_LIST>();
                List<LTACB_LIST> lTACB_LISTs = new List<LTACB_LIST>();
                List<ReturnMsg> ReturnTable = new List<ReturnMsg>();

                if (RtnFL_LIST.Count > 0)
                { //Code need to written                  

                    for (int i = 0; i < RtnFL_LIST.Count; i++)
                    {
                        // Create a new instance of MyRecord and populate it with data from the current row
                        FL_LIST record = new FL_LIST
                        {

                            SWERK = RtnFL_LIST[i].GetString("SWERK").ToString(),
                            BEBER = RtnFL_LIST[i].GetString("BEBER").ToString(),
                            FING = RtnFL_LIST[i].GetString("FING").ToString(),
                            TPLNR = RtnFL_LIST[i].GetString("TPLNR").ToString(),
                            PLTXT = RtnFL_LIST[i].GetString("PLTXT").ToString()
                            // Add more fields as needed
                        };

                        // Add the record to the list
                        fL_LISTs.Add(record);
                    }


                    dataTableFL = ConvertToDataTable<FL_LIST>(fL_LISTs);
                }
                if (RtnDT_LIST.Count > 0)
                { //Code need to written

                    for (int i = 0; i < RtnDT_LIST.Count; i++)
                    {
                        // Create a new instance of MyRecord and populate it with data from the current row
                        DT_LIST record = new DT_LIST
                        {

                            SWERK = RtnDT_LIST[i].GetString("SWERK").ToString(),
                            BEBER = RtnDT_LIST[i].GetString("BEBER").ToString(),
                            FING = RtnDT_LIST[i].GetString("FING").ToString(),
                            TPLNR = RtnDT_LIST[i].GetString("TPLNR").ToString(),
                            PLTXT = RtnDT_LIST[i].GetString("PLTXT").ToString(),
                            EQART = RtnDT_LIST[i].GetString("EQART").ToString(),
                            EQUNR_DTR = RtnDT_LIST[i].GetString("EQUNR_DTR").ToString(),
                            EQ_DESC = RtnDT_LIST[i].GetString("EQ_DESC").ToString()
                            // Add more fields as needed
                        };

                        // Add the record to the list
                        dT_LISTs.Add(record);
                    }

                    dataTableDT = ConvertToDataTable<DT_LIST>(dT_LISTs);
                }
                if (RtnLTACB_LIST.Count > 0)
                { //Code need to written


                    for (int i = 0; i < RtnLTACB_LIST.Count; i++)
                    {
                        // Create a new instance of MyRecord and populate it with data from the current row
                        LTACB_LIST record = new LTACB_LIST
                        {

                            SWERK = RtnLTACB_LIST[i].GetString("SWERK").ToString(),
                            BEBER = RtnLTACB_LIST[i].GetString("BEBER").ToString(),
                            FING = RtnLTACB_LIST[i].GetString("FING").ToString(),
                            TPLNR = RtnLTACB_LIST[i].GetString("TPLNR").ToString(),
                            PLTXT = RtnLTACB_LIST[i].GetString("PLTXT").ToString(),
                            EQART = RtnLTACB_LIST[i].GetString("EQART").ToString(),
                            EQUNR_LTACB_SAP = RtnLTACB_LIST[i].GetString("EQUNR_LTACB_SAP").ToString(),
                            EQ_DESC = RtnLTACB_LIST[i].GetString("EQ_DESC").ToString(),
                            LTFDR_SAP = RtnLTACB_LIST[i].GetString("LTFDR_SAP").ToString()
                            // Add more fields as needed
                        };

                        // Add the record to the list
                        lTACB_LISTs.Add(record);
                    }

                    dataTableLTACB = ConvertToDataTable<LTACB_LIST>(lTACB_LISTs);
                }





                if (RtnMsgTable.Count > 0)
                {
                    ReturnMsg ReturnMsg = new ReturnMsg();
                    ReturnMsg.TYPE = RtnMsgTable.GetString("TYPE").ToString();
                    ReturnMsg.ID = RtnMsgTable.GetString("ID").ToString();
                    ReturnMsg.NUMBER = RtnMsgTable.GetString("NUMBER").ToString();
                    ReturnMsg.MESSAGE = RtnMsgTable.GetString("MESSAGE").ToString();
                    ReturnMsg.LOG_NO = RtnMsgTable.GetString("LOG_NO").ToString();
                    ReturnMsg.LOG_MSG_NO = RtnMsgTable.GetString("LOG_MSG_NO").ToString();
                    ReturnMsg.MESSAGE_V1 = RtnMsgTable.GetString("MESSAGE_V1").ToString();
                    ReturnMsg.MESSAGE_V2 = RtnMsgTable.GetString("MESSAGE_V2").ToString();
                    ReturnMsg.MESSAGE_V3 = RtnMsgTable.GetString("MESSAGE_V3").ToString();
                    ReturnMsg.MESSAGE_V4 = RtnMsgTable.GetString("MESSAGE_V4").ToString();
                    ReturnMsg.PARAMETER = RtnMsgTable.GetString("PARAMETER").ToString();
                    ReturnMsg.ROW = RtnMsgTable.GetString("ROW").ToString();
                    ReturnMsg.FIELD = RtnMsgTable.GetString("FIELD").ToString();
                    ReturnMsg.SYSTEM = RtnMsgTable.GetString("SYSTEM").ToString();
                    ReturnTable.Add(ReturnMsg);

                    dataTableERROR = ConvertToDataTable<ReturnMsg>(ReturnTable);
                }


            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }




        bapiResult[0] = dataTableFL;
        bapiResult[0].TableName = "FL";
        bapiResult[1] = dataTableDT;
        bapiResult[1].TableName = "DT";
        bapiResult[2] = dataTableLTACB;
        bapiResult[2].TableName = "LTACB";
        bapiResult[3] = dataTableERROR;
        bapiResult[3].TableName = "ERROR";

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;

    }


    public DataSet ZBAPI_PM_LT_LOAD(string _SWERK, string _BEBER, string _TPLNR, string _EQUNR_DTR, string _EQUNR_LTACB_SAP, string _LDATE, string _LTIME, string _FING, string _PLTXT, string _LTFDR_SAP, string _EQUNR_LTACB_SITE, string _LTFDR_SITE, string _CABLE1, string _CABLE2, string _R_PH_LD, string _Y_PH_LD, string _B_PH_LD, string _NEUT_LT, string _REMARK, string _LNAME, string _ADDLTACB, string _LUSID)
    {
        DataTable[] bapiResult = new DataTable[2];
        DataSet dsBAPIOutput = new DataSet("ZBAPI_PM_LT_LOAD");
        ZBAPI_PM_LT_LOADRESPONSE _objOutPut = new ZBAPI_PM_LT_LOADRESPONSE();
        string flag = "";

        string messageText = "";
        string messageCode = "0";

        DataTable dataTableERROR = new DataTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("R3");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_PM_LT_LOAD");

                testfn.SetValue("SWERK", _SWERK);
                testfn.SetValue("BEBER", _BEBER);
                testfn.SetValue("TPLNR", _TPLNR);
                testfn.SetValue("EQUNR_DTR", _EQUNR_DTR);
                testfn.SetValue("EQUNR_LTACB_SAP", _EQUNR_LTACB_SAP);



                testfn.SetValue("LDATE", _LDATE);
                testfn.SetValue("LTIME", _LTIME);
                testfn.SetValue("FING", _FING);
                testfn.SetValue("PLTXT", _PLTXT);
                testfn.SetValue("LTFDR_SAP", _LTFDR_SAP);
                testfn.SetValue("EQUNR_LTACB_SITE", _EQUNR_LTACB_SITE);
                testfn.SetValue("LTFDR_SITE", _LTFDR_SITE);
                testfn.SetValue("CABLE1", _CABLE1);
                testfn.SetValue("CABLE2", _CABLE2);
                testfn.SetValue("R_PH_LD", _R_PH_LD);
                testfn.SetValue("Y_PH_LD", _Y_PH_LD);
                testfn.SetValue("B_PH_LD", _B_PH_LD);

                int _neutLt;
                if (int.TryParse(_NEUT_LT, out _neutLt))
                {

                    // Set the integer parameter
                    testfn.SetValue("NEUT_LT", _neutLt);

                }
                // testfn.SetValue("NEUT_LT", _NEUT_LT);
                testfn.SetValue("REMARK", _REMARK);
                testfn.SetValue("LNAME", _LNAME);
                testfn.SetValue("ADDLTACB", _ADDLTACB);
                testfn.SetValue("LUSID", _LUSID);

                testfn.Invoke(dest);

                flag = testfn.GetValue("FLAG").ToString();
                IRfcStructure RtnMsgTable = testfn.GetStructure("RETURN");



                List<ReturnMsg> ReturnTable = new List<ReturnMsg>();
                if (RtnMsgTable.Count > 0)
                {

                    ReturnMsg ReturnMsg = new ReturnMsg();
                    ReturnMsg.TYPE = RtnMsgTable.GetString("TYPE");
                    ReturnMsg.ID = RtnMsgTable.GetString("ID").ToString();
                    ReturnMsg.NUMBER = RtnMsgTable.GetString("NUMBER").ToString();
                    ReturnMsg.MESSAGE = RtnMsgTable.GetString("MESSAGE").ToString();
                    ReturnMsg.LOG_NO = RtnMsgTable.GetString("LOG_NO").ToString();
                    ReturnMsg.LOG_MSG_NO = RtnMsgTable.GetString("LOG_MSG_NO").ToString();
                    ReturnMsg.MESSAGE_V1 = RtnMsgTable.GetString("MESSAGE_V1").ToString();
                    ReturnMsg.MESSAGE_V2 = RtnMsgTable.GetString("MESSAGE_V2").ToString();
                    ReturnMsg.MESSAGE_V3 = RtnMsgTable.GetString("MESSAGE_V3").ToString();
                    ReturnMsg.MESSAGE_V4 = RtnMsgTable.GetString("MESSAGE_V4").ToString();
                    ReturnMsg.PARAMETER = RtnMsgTable.GetString("PARAMETER").ToString();
                    ReturnMsg.ROW = RtnMsgTable.GetString("ROW").ToString();
                    ReturnMsg.FIELD = RtnMsgTable.GetString("FIELD").ToString();
                    ReturnMsg.SYSTEM = RtnMsgTable.GetString("SYSTEM").ToString();
                    ReturnTable.Add(ReturnMsg);

                    dataTableERROR = ConvertToDataTable<ReturnMsg>(ReturnTable);
                }

            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        DataTable dataTableResponse = new DataTable("FLAG");
        // Step 2: Define columns     
        dataTableResponse.Columns.Add("FLAG", typeof(string));

        // Step 3: Add rows
        dataTableResponse.Rows.Add(flag);

        bapiResult[0] = dataTableResponse;
        bapiResult[0].TableName = "RESPONSE";

        bapiResult[1] = dataTableERROR;
        bapiResult[1].TableName = "ERROR";


        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;

    }

    public static DataTable ConvertToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        // Get properties of the class
        PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Create columns in DataTable
        foreach (PropertyInfo property in properties)
        {
            dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
        }

        // Populate DataTable with data
        foreach (T item in items)
        {
            DataRow row = dataTable.NewRow();
            foreach (PropertyInfo property in properties)
            {
                row[property.Name] = property.GetValue(item, null);
            }
            dataTable.Rows.Add(row);
        }

        return dataTable;
    }

}

