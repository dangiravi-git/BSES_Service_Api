using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class TF_Load_Data
    {
        public List<ALL_DT_DTR> ALL_DT;
        public List<TFMeter> TFMeterdata;


        string[] array = { "hi", "welcome", "to", "forget code" };
        //List<string> list = array.ToList();
        //    foreach (string item in list)
        //    {
        //        Console.WriteLine(item);
        //    }


        public string ORDERNO;
        public string POLE_NO;
        public string POLE_COORDINATE;
        public string DT_CODE;
        public string DT_SAP_CODE;
        public string DISTANCE;
        public string KVA_RATE;
        public string FL_CODE;
        public string FL_NAME;
        public string CA_NO;
        public string DT_MTR_NO;
        public string APPLY_LOAD;
        public string EXPECTED_LOAD;
        public string USER_ID;
        public string IMEI_NO;
        public string SUB_DIV;
        public string SUB_DIV_CD;

        public string FEEDER_DATA;
        public string METER_DATA;

        public class ALL_DT_DTR
        {
            public string DTCode;
            public string DTSapCode;
            public string Distance;
            public string FLCode;
            public string FLName;
            public string PoleNo;
            public string XY;
            public string feeder;
            public string kvrRating;
        }

        public class TFMeter
        {
            public string METER_NO;
        }

    }
}