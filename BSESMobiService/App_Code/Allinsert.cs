using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

/// <summary>
/// Summary description for AllInsert
/// </summary>
public class AllInsert
{
    private  DbFunction objdbfun = new DbFunction();
	public AllInsert()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public  bool registerivrs(string ca, string callernum)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO omsuser.CMS_IVRS_COMP ( CA_NO, CALLER_NO) VALUES ( " + ca + " , " + callernum + " )");
            objdbfun = new DbFunction();
            return false; //(objdbfun.dmlsinglequeryOMS(sb.ToString()));
        }
        catch (Exception ex)
        { throw ex; }
    }

    public  bool InsertBySQL(string _sSql)
    {
        return (objdbfun.dmlsinglequery(_sSql));
    }
    public  bool InsertByMultipleSQL(string[] _sSql)
    {
        return (objdbfun.dmlMultiplequery(_sSql));
    }
    public  bool InsertByMultipleSQL_ONM(string[] _sSql)
    {
        return (objdbfun.dmlMultiplequery_ONM(_sSql));
    }
     public  bool insUser(string USER_NAME,string PASSWORD,string NAME,string IMEI_NO,string DIV_CODE
         ,string ROL_RGT,string COMPANY_CODE, string _sDivRgt, bool _bUpdate)
    {
         bool _bResult=false;
         string _sSql="";
         if (!_bUpdate)
         {
             _sSql = " INSERT INTO ISUSTD_USER(USER_NAME, PASSWORD, NAME, IMEI_NO, DIV_CODE, ROL_RGT, COMPANY_CODE,DIV_RGT) ";
             _sSql += " VALUES('" + USER_NAME + "','" + PASSWORD + "','" + NAME + "','" + IMEI_NO + "','" + DIV_CODE + "','" + ROL_RGT + "','" + COMPANY_CODE + "','" + _sDivRgt + "') ";

         }
         else
         {
             _sSql = " update ISUSTD_USER set NAME='" + NAME + "', IMEI_NO='" + IMEI_NO + "',ROL_RGT='" + ROL_RGT + "',DIV_CODE='" + DIV_CODE + "', COMPANY_CODE='" + COMPANY_CODE + "'";
             _sSql += " ,DIV_RGT='" + _sDivRgt + "'";
             _sSql += " where USER_NAME='" + USER_NAME + "' ";

         }
         _bResult=objdbfun.dmlsinglequery(_sSql);

         if(ROL_RGT=="BEE" && _bResult)
         {
             if (!_bUpdate)
             {
                 _sSql = "INSERT INTO MOB_TEAM_ALLOCATION(NAME, EMP_ID,ACTIVITY)";
                 _sSql += " VALUES('" + NAME + "','" + USER_NAME + "','Y')";
             }
             else
             {
                 _sSql = " update MOB_TEAM_ALLOCATION set NAME='" + NAME + "' where EMP_ID='" + USER_NAME + "' ";
             }

             _bResult=objdbfun.dmlsinglequery(_sSql);
         }
         return _bResult;
    }

    public  bool NewRegistration_ARD(string strConsRef, string strUserName, string strPass
         , string strEmailId, string strMobileNo, string strPhoneNo, string strContactPerson)
    {
        bool _bResult = false;
      
        string sql = " INSERT INTO CCM_E_BILL_REG (CONS_REF, USERNAME, PASS, EMAIL_ID, MOB_NO ";
        sql += ", ENTERED_BY,  VIP_FLAG, PHONE_NO, CONTACT_PERSON, MASTERCA, SUBCA)	";
        sql += " VALUES('" + strConsRef + "','" + strUserName + "','" + strPass + "','" + strEmailId + "','" + strMobileNo + "'";
        sql += " ,'ANDROID_APP','0','" + strPhoneNo + "','" + strContactPerson + "','" + strConsRef + "','" + strConsRef + "')";

        _bResult = objdbfun.dmlsinglequery(sql);

        return _bResult;
    }


    public  bool InsertMeterRelatedDetails(string strConsRef, string strUserName, string strPass
         , string strEmailId, string strMobileNo, string strPhoneNo, string strContactPerson)
    {
        bool _bResult = false;

        string sql = " INSERT INTO CCM_E_BILL_REG (CONS_REF, USERNAME, PASS, EMAIL_ID, MOB_NO ";
        sql += ", ENTERED_BY,  VIP_FLAG, PHONE_NO, CONTACT_PERSON, MASTERCA, SUBCA)	";
        sql += " VALUES('" + strConsRef + "','" + strUserName + "','" + strPass + "','" + strEmailId + "','" + strMobileNo + "'";
        sql += " ,'ANDROID_APP','0','" + strPhoneNo + "','" + strContactPerson + "','" + strConsRef + "','" + strConsRef + "')";

        _bResult = objdbfun.dmlsinglequery(sql);

        return _bResult;
    }

    public  bool eDistrict(string CA_NUMBER, string CRN_NUMBER, string TXN_ID,string  BP_NUMBER, string BP_NAME, 
    string BP_TYPE, string SEARCH_TERM1, string SEARCH_TERM2, string HOUSE_NO,string  HOUSE_NO_SUP, 
    string  FLOOR, string PREMISE_TYPE, string STREET,string  STREET2, string STREET3, 
    string STREET4,string  CITY, string POSTAL_CODE,string  REGION, string COUNTRY, 
    string DESC_CON_OBJECT, string REG_STR_GROUP, string DEVICE_SERIAL_NUMBER, string PHONE_NUMBER,string  MRU, 
    string FUNC_DESCR,string OUTAGE_FROMTIME,string OUTAGE_TOTIME, string LEGACY_ACCT, string BILL_CLASS, 
    string RATE_CAT, string ACTIVITY, string ADR_NOTES, string TEL1_NUMBER, string VERTRAG, 
    string EMAIL, string MOVE_OUT_DATE, string CON_OBJ_NUMBER, string CLERK_ID, string TEXT, 
    string STATUS, string DISCREATION, string TARIFTYP, string WERT1, string CONS_SINCE,
    string POLE_NUMBER, string SEQUENCE_NUMBER, string LAST_BILL_DATE)
    {
        bool _bResult = false;



        string sql = " Insert into ISUSTD_EDISTRICT";
    sql+= " (CA_NUMBER, CRN_NUMBER, TXN_ID, BP_NUMBER, BP_NAME, ";
    sql+= " BP_TYPE, SEARCH_TERM1, SEARCH_TERM2, HOUSE_NO, HOUSE_NO_SUP,"; 
    sql+= " FLOOR, PREMISE_TYPE, STREET, STREET2, STREET3,";
    sql+= " STREET4, CITY, POSTAL_CODE, REGION, COUNTRY,"; 
    sql+= " DESC_CON_OBJECT, REG_STR_GROUP, DEVICE_SERIAL_NUMBER, PHONE_NUMBER, MRU,"; 
    sql+= " FUNC_DESCR, OUTAGE_FROMTIME, OUTAGE_TOTIME, LEGACY_ACCT, BILL_CLASS,"; 
    sql+= " RATE_CAT, ACTIVITY, ADR_NOTES, TEL1_NUMBER, VERTRAG,"; 
    sql+= "  EMAIL, MOVE_OUT_DATE, CON_OBJ_NUMBER, CLERK_ID, TEXT,"; 
    sql+= " STATUS, DISCREATION, TARIFTYP, WERT1, CONS_SINCE,"; 
    sql+= " POLE_NUMBER, SEQUENCE_NUMBER, LAST_BILL_DATE)";
    sql+= "  Values";
    sql += "('" + CA_NUMBER + "','" + CRN_NUMBER + "','" + TXN_ID + "','" + BP_NUMBER + "','" + BP_NAME + "','" +
      BP_TYPE + "','" + SEARCH_TERM1 + "','" + SEARCH_TERM2 + "','" + HOUSE_NO + "','" + HOUSE_NO_SUP + "','" +
      FLOOR + "','" + PREMISE_TYPE + "','" + STREET + "','" + STREET2 + "','" + STREET3 + "','" +
      STREET4 + "','" + CITY + "','" + POSTAL_CODE + "','" + REGION + "','" + COUNTRY + "','" +
      DESC_CON_OBJECT + "','" + REG_STR_GROUP + "','" + DEVICE_SERIAL_NUMBER + "','" + PHONE_NUMBER + "','" + MRU + "','" +
      FUNC_DESCR + "','" + OUTAGE_FROMTIME + "','" + OUTAGE_TOTIME + "','" + LEGACY_ACCT + "','" + BILL_CLASS + "','" +
      RATE_CAT + "','" + ACTIVITY + "','" + ADR_NOTES + "','" + TEL1_NUMBER + "','" + VERTRAG + "','" +
      EMAIL + "','" + MOVE_OUT_DATE + "','" + CON_OBJ_NUMBER + "','" + CLERK_ID + "','" + TEXT + "','" +
      STATUS + "','" + DISCREATION + "','" + TARIFTYP + "','" + WERT1 + "','" + CONS_SINCE + "','" +
      POLE_NUMBER + "','" + SEQUENCE_NUMBER + "','" + LAST_BILL_DATE + "')";

        _bResult = objdbfun.dmlsinglequery(sql);

        return _bResult;
    }

    #region Admin Support System

    public  bool InsertComplaintDetails(string strcompno, string strCompName, string strEmailID, string strMobileNo, string strPhoneNo,
                                    string strPeriorty, string strRmk, string strFileName, string strDivID, string strCompCentreID, string strCircle,
                                     string strReqType, string strAssignTo, string strDept, string strRequestFor)
    {
        bool _bResult = false;

        string sql = "INSERT INTO  ASS_COMP_MST(COMP_NO,COMPLAINANT_NAME,COMPLAINANT_EMAIL,COMPLAINANT_MOB,COMPLAINANT_PHONE,PRIORITY_IDX,REMARKS, ";
        sql = sql + " FILE_NAME,COMP_DATE,division,office,location,REQUEST_TYPE,REQUEST_SUB_TYPE,request_date,FWD_TO,dept,REQUEST_FOR) VALUES ( ";
        sql = sql + " '" + strcompno.ToString().Trim() + "','" + strCompName.ToString().Trim() + "','" + strEmailID.ToString().Trim() + "','" + strMobileNo.ToString().Trim() + "', ";
        sql = sql + " '" + strPhoneNo.ToString().Trim() + "','" + strPeriorty.ToString() + "','" + strRmk.Replace("'", "").ToString() + "','" + strFileName.ToString() + "',SYSDATE, ";
        sql = sql + " '" + strDivID.ToString() + "','" + strCompCentreID.ToString() + "','" + strCircle.ToString() + "','" + strReqType.ToString() + "',";
        sql = sql + " '" + strReqType.ToString() + "',SYSDATE,'" + strAssignTo.ToString() + "',";
        sql = sql + " '" + strDept.ToString() + "','" + strRequestFor.ToString() + "')";

        _bResult = objdbfun.dmlsinglequery_VSS(sql);

        return _bResult;
    }

    public  bool UpdateComplaintDetails(string strcompno, string strCompStatusID, string strRemarks, string strUserID, string strUserName)
    {
        bool _bResult = false;

        string sql = " INSERT INTO ASS_COMP_Close(COMP_NO,REMARKS,USER_ID,ENTRY_DATE,WIP_DATE,STATUS,USER_OWNER_NAME)VALUES( ";
        sql = sql + " '" + strcompno.ToString().Trim() + "','" + strRemarks.Replace("'", "") + "','" + strUserID.ToString() + "',sysdate,sysdate,'" + strCompStatusID.ToString() + "','" + strUserName.ToString() + "')";

        string sql1 = " Update ASS_COMP_MST set  COMP_STATUS='" + strCompStatusID.ToString() + "' where COMP_NO='" + strcompno.ToString().Trim() + "'";

        _bResult = objdbfun.dmlsinglequery_VSS(sql);
        _bResult = objdbfun.dmlsinglequery_VSS(sql1);

        return _bResult;
    }

    public  bool Insert_Attachment(string strComplaintNo, string str_img, string strFileName)
    {
        try
        {
            byte[] _byImg = null;
            string _sImgPath = string.Empty, _sIDProfImgPath = string.Empty;
            try
            {
                _byImg = Convert.FromBase64String(str_img);
                _sImgPath = byteArrayToImage(_byImg, strFileName, strComplaintNo);
            }
            catch (Exception ex)
            {
                WriteIntoFile(DateTime.Now.ToString() + "Error in sign image" + ex.ToString());
            }

            return true;
        }
        catch (Exception ex)
        {
            WriteIntoFile(DateTime.Now.ToString() + ex.ToString());
            return false;
        }

    }


    public  string byteArrayToImage(byte[] byteArrayIn, string filename, string strFolder)
    {
        try
        {
            DirectoryInfo _DirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Attachment") + "\\" + strFolder);
            if (_DirInfo.Exists == false)
                _DirInfo.Create();

            string _sPath = _DirInfo.FullName + "\\" + filename + ".jpeg";
            string _sFileNameWithoutExt = _DirInfo.FullName + "\\" + filename;
            int _iFileIndex = 1;
            while (File.Exists(_sPath))
            {
                _sPath = _sFileNameWithoutExt + "_" + _iFileIndex.ToString() + ".jpeg";
                _iFileIndex++;
            }

            BinaryWriter fs = new BinaryWriter(new FileStream(_sPath, FileMode.Append, FileAccess.Write));
            fs.Write(byteArrayIn);
            fs.Close();
            return _sPath;
        }
        catch (Exception ex)
        {
            WriteIntoFile(DateTime.Now.ToString() + ex.ToString());
            return "";
        }
    }

    public  void WriteIntoFile(string _smsg)
    {
        //using (StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath(@"~\App_Data\ApplicationLog.txt")))
        using (StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath(@"~\App_Data\ApplicationLog" + "_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".txt")))
        {
            sw.WriteLine(_smsg);
            sw.Close();
        }
    }

    #endregion

    #region"Service_log"

    public bool Insert_ServicesLog(string strServiceName, string strCANo, string strOrderNo, string strKCCNo, string strMobileno,
                                    string strSource,string _sFlag)
    {
        bool _bResult = false;
        string sqlinsert = string.Empty;
        sqlinsert = " Insert into MOBAPP.BSES_SERVICES_LOG_WP (SERVICE_NAME, CA_NUMBER, ORDER_NO, KCC_NO, MOBILE_NO, SOURCE_NAME,STATUS_FLAG) ";
        sqlinsert += "  VALUES   ('" + strServiceName + "', '" + strCANo + "', '" + strOrderNo + "', '" + strKCCNo + "','" + strMobileno + "', '"+ strSource + "','"+ _sFlag + "') ";

        _bResult = objdbfun.dmlsinglequery_mobApp(sqlinsert);

        return _bResult;
    }

    #endregion
}
