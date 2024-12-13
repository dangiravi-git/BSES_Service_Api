using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class TF_Complete_Data
    {
        public List<CFdata> cfdata;

        public string UserId;
        public string IMEI;
        public string LAT;
        public string LONG;
        public string ORDERNO;
        public string DivisionCode;
        public string BPNo;
        public string TFEnggName;
        public string ConsumerName;
        public string CATEGORYCODE;
        public string FATHER_NAME;
        public string LoadKW;
        public string LoadKVA;
        public string ConsumerAddress;
        public string TELEPHONE;
        public string DOC_RECEIVED;
        public string AppointmentDate;
        public string AppointmentTime;
        public string OrderCreationDate;
        public string DivisionName;
        public string MOHALLA_KHASRA;
        public string HOUSE_NO;
        public string OTHER_ADDRESS;

        public string TFEnggID;
        public string ZDATE;
        public string CTIME;
        public string RequestType;
        public string ENTRY_DT;
        public string STATUS;
        public string VoltageLVL;
        public string SupplyType;
        public string CategoryOFsupply;
        public string AppliedLoadKW;
        public string VISITDATE;
        public string VISITTIME;
        public string TFAreaType;
        public string TFLocalityType;
        public string TFStructureOfPremises;
        public string TFNatureofUse;
        public string TFCableSize;
        public string TFMeterType;
        public string Encroachment;
        public string TFAugmentation;
        public string TFBusBarType;
        public string TFTransformers;
        public string TFRMU;
        public string TFMeteringCubicle;
        public string ONMSchemeApplied;
        public string AppliedLoadInKW;
        public string ServiceLineLengthmtr;
        public string PoleNoFeederPillarNo;
        public string DTMeterNo;
        public string DTCode;
        public string LeftMeterNo;

        public string NearbyExistingMeterNo;
        public string RightMeterNo;
        public string NormativeLoadKW;
        public string BuildupCoveredAreasqm;
        public string PlotAreasqm;
        public string NewBuilding;
        public string BuildingHeight;
        public string BuildingHieghtremarks;
        public string WiringCompleted;
        public string LiftInstalled;

        public string LiftCertificateRequired;
        public string AffidavitRequired;
        public string Cable;
        public string NumberOfFloor;
        public string AppliedArea;
        public string BuildingArea;
        public string TFSealNo;
        public string Pastedonproposedmeteringposition;
        public string Appliedportion;
        public string DwellingUnitwithseparateentry;
        public string Reasonfornewmeter;
        public string DPCCClearanceRequired;
        public string SStnSpaceAvailable;
        public string Totalloadofplotpremiseexistingapplied;
        public string Totalnoofconnectionsexpected;
        public string Ifanytheftbookedontheappliedpremises;
        public string TFRemarks;
        public string ConsumerSAvailavility;
        public string FinalRemark;

        public string BuildingIMG;
        public string MeterLocationIMG;
        public string ConsumerSignIMG;
        public string SiteLayoutIMG;
        public string PoleIMG;
        public string ConsumerIMG;
        public string OtherIMG1;
        public string OtherIMG2;
        public string OtherIMG3;

        public string Meterlocation;
        public string CoveredAreaRemarks;
        public string ELCB_Status;
        public string MCDBooked;
        public string ApplicantAvailability;
        public string TFCableSizeRequired;
        public string MCDLetters;
        public string MCDLettersFound;

        public string GISMeterlocation;
        public string ConsumerBuildingNum;
        public string ConsumerBuildingID;
        public string ConsmerSubType;
        public string ConsumerLocality;
        public string PoleLocation;
        public string PoleNum;
        public string PoleDTCode;
        public string PoleKvaRating;
        public string PoleSapID;
        public string PolFLName;
        public string PoleFLCode;

        public string Mtr_Instal_Space;
        public string Sufficient_Covered_Space;
        public string Sufficient_Space_Available;
        public string Space_Available_Mtr;
        public string Space_Mtr_Install;
        public string Seperate_Mtr_Available;
        public string Outside_Mtr_Projection;
        public string Meter_Panel;
        public string Customer_Wiring;
        public string Customer_Protection_Equip;
        public string ELCB_Available;
        public string MCB_Available;
        public string Space_Provision;
        public string Available_Mtr_Reading;
        public string Private_Bus_Bar;
        public string EnchorchmentLT;
        public string Sanction_Plan;
        public string Building_Comple_Cert;
        public string Mtr_Instal_Space_Img;
        public string Sufficient_Covered_Space_Img;
        public string Sufficient_Space_Available_Img;
        public string Space_Available_Mtr_Img;
        public string Space_Mtr_Install_Img;
        public string Seperate_Mtr_Available_Img;
        public string Outside_Mtr_Projection_Img;
        public string Meter_Panel_Img;
        public string ELCB_Available_Img;
        public string MCB_Available_Img;
        public string EnchorchmentLT_Img;

        public string PHOTO_IDENTITY_PROOF;
        public string OWNERSHIP_PROOF_NEW;
        public string SAFETY_COMPLIANCES;
        public string USES;

        public string ISHAZARDOUS;
        public string NODB;
        public string DB_BURNT;
        public string Db_OVERLOADED;
        public string POLE_dameged;
        public string pole_overloaded;
        public string no_of_cable;
        public string addition_pole_required;
        public string no_of_pole;
        public string db_overloadedimg;
        public string pole_overloadedimg;
        public string pole_damegedimg;
        public string Email;
        public string RCPremarks;
        public string MultipleOrders;
        public string MUL_ORD_NO;
        public string testReport;
        public string testReportImg;

        public string CivilCategory;
        public string CivilFloor;
        public string CivilBuilHeight;
        public string CivilActFloor;
        public string CivilImg;        

        public class CFdata
        {
            public string ORDERNO;
            public string NET_OUTSTAND_AMT;
            public string BPNo;
            public string CANO;
            public string MOV_OUT_DATE;
            public string LEGACYNO;
            public string NAME;
            public string ADDRESS;
            public string ENF_F;
            public string LAST_PAYMENT_MODE;
            public string SEQUENCE_NO;
            public string METER_NO;
            public string ENTRY_DT;
            public string Rel_NotRel;
            public string rbtn_Cheched;
        }

    }
}