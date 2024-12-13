using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

class ZBAPI_PM_LT_LOADRESPONSE
{

    public string FLAG { get; set; }
    public List<ReturnMsg> ReturnMsg
    {
        get;
        set;
    }


}

public class ReturnMsg
{
    public string TYPE { get; set; }
    public string ID { get; set; }
    public string NUMBER { get; set; }
    public string MESSAGE { get; set; }
    public string LOG_NO { get; set; }
    public string LOG_MSG_NO { get; set; }
    public string MESSAGE_V1 { get; set; }
    public string MESSAGE_V2 { get; set; }
    public string MESSAGE_V3 { get; set; }
    public string MESSAGE_V4 { get; set; }
    public string PARAMETER { get; set; }
    public string ROW { get; set; }
    public string FIELD { get; set; }
    public string SYSTEM { get; set; }
}

public class FL_LIST
{
    public string SWERK { get; set; }
    public string BEBER { get; set; }
    public string FING { get; set; }
    public string TPLNR { get; set; }
    public string PLTXT { get; set; }

}
public class DT_LIST
{
    public string SWERK { get; set; }
    public string BEBER { get; set; }
    public string FING { get; set; }
    public string TPLNR { get; set; }
    public string PLTXT { get; set; }

    public string EQART { get; set; }
    public string EQUNR_DTR { get; set; }
    public string EQ_DESC { get; set; }

}

public class LTACB_LIST
{
    public string SWERK { get; set; }
    public string BEBER { get; set; }
    public string FING { get; set; }
    public string TPLNR { get; set; }
    public string PLTXT { get; set; }

    public string EQART { get; set; }
    public string EQUNR_LTACB_SAP { get; set; }
    public string EQ_DESC { get; set; }

    public string LTFDR_SAP { get; set; }

}

