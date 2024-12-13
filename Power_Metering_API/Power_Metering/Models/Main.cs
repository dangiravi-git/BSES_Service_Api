using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Power_Metering.Models
{
    public class Main
    {
    }
    public class CancelRemarks
    {
        public string Cancelid { get; set; }
        public string Remarks { get; set; }
    }
    public class EmpolyeeDetails
    {
        public string Empid { get; set; }
        public string Employeename { get; set; }
    }
    public class Logindetails
    {

        public string userID { get; set; }
        public string password { get; set; }
    }

    public class UserDetails
    {
        public string userID { get; set; }
        public string userName { get; set; }
        public string L1_Mobile { get; set; }
        public string JwtToken { get; set; }
    }
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public string Role { get; set; }
        public object Data { get; set; }

    }

    public class Response2
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public string Role { get; set; }
        public object Data { get; set; }
        public object EMP_Data { get; set; }

    }
    public class Response1
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object meterNumber { get; set; }
        public object SealNumber { get; set; }
        public object FetchAMRNo { get; set; }
        public object FetchMCRNO { get; set; }
        public object GunnyBagSerialNo { get; set; }
        public object GunnyBagSealSerialNo { get; set; }
        public object FetchMetreBoxDetails { get; set; }
        public object DivSubdivisionList { get; set; }
        public object ActivitySubactivityList { get; set; }
        public object PremiseMst { get; set; }
        public object VendorMst { get; set; }
        public object Employee { get; set; }

    }
    public class MeterNumber
    {
        public string meterNumber { get; set; }
    }
    public class AMRDetails
    {
        public string amrNumber { get; set; }
    }
    public class MCRNODetails
    {
        public string mcrNumber { get; set; }
    }
    public class GunnyBag
    {
        public string Gunny_BagnSrno { get; set; }
    }
    public class GunnyBagSrNo
    {
        public string Gunny_BagSealSrNo { get; set; }
    }
    public class PMPREMISEMST
    {
        public string premise { get; set; }
        public string sub_premise { get; set; }
    }

    public class VENDORMST
    {
        public string vendor_id { get; set; }
        public string vendor_name { get; set; }
        public string circle { get; set; }
        public string work_order_no { get; set; }
    }
    public class DivSubdiv
    {
        public string division { get; set; }
        public string division_desc { get; set; }
        public string sub_division { get; set; }
        public string sub_division_desc { get; set; }
        public string circle { get; set; }
    }
    public class ActivitySubactivity
    {
        public string activityType { get; set; }
        public string activityType_desc { get; set; }
        public string subActivityType { get; set; }
        public string subActivityType_desc { get; set; }

    }
    public class MeterBoxDetails
    {
        public string mbSerialNumber { get; set; }
    }
    public class SealNumber
    {
        public string sealNumber { get; set; }
    }
    public class CaseLists
    {
        public string orderId { get; set; }
        public string start_date { get; set; }
        public string finish_date { get; set; }
        public string order_type { get; set; }

        public string activity_type { get; set; }
        public string activityType_name { get; set; }
        public string planner_group { get; set; }

        public string division { get; set; }

        public string ca_no { get; set; }

        public string name { get; set; }
        public string father_name { get; set; }
        public string address { get; set; }
        public string tel_no { get; set; }

        public string meter_no { get; set; }
        public string sanctioned_load { get; set; }
        public string cable_length { get; set; }

        public string pole_no { get; set; }
        public string division_name { get; set; }

        public string ct_box_number { get; set; }  // ASK

        public string mf { get; set; } // ASK
        public string tariff_category { get; set; } // ASK
        public string user_responsible { get; set; } // ASK

        public string previous_reading_date { get; set; } // ASK

        public string kwh { get; set; }

        public string kw { get; set; }
        public string kvah { get; set; }
        public string kva { get; set; }

        public string other1 { get; set; }

        public string other2 { get; set; }
        public string other3 { get; set; }
        public string other4 { get; set; }

    }


    public class PMPunchOrder
    {

        public string UserId { get; set; }
        public string OrderNo { get; set; }
        public string newMtrBoxSrNum { get; set; }
        public string oldMtrBoxSrNum { get; set; }
        public string installMtrBoxType { get; set; }
        public string newMtrSrlNum { get; set; }
        public string oldMtrSrlNum { get; set; }
        public string installMtrType { get; set; }
        public string scanSrNum { get; set; }
        public string newAmrSrlNum { get; set; }
        public string oldAmrSrNum { get; set; }
        public string remarks { get; set; }
        public string mtrPhoto1 { get; set; }
        public string mtrPhoto2 { get; set; }
        public string mcrPhoto { get; set; }
        public string signaturePhoto { get; set; }
        public string gunnyBagPhoto { get; set; }
        public string consumerName { get; set; }
        public string consumerContactNum { get; set; }
        public string empName { get; set; }
        public string empNumber { get; set; }
        public string mtrBxReplacementResion { get; set; }
        public string mtrReplacementResion { get; set; }
        public string circleName { get; set; }
        public string divisionName { get; set; }
        public string sbDivisionName { get; set; }
        public string departmentName { get; set; }
        public string mcrType { get; set; }
        public string mcrNumber { get; set; }
        public string mcrDate { get; set; }
        public string mcrMeteringActivity { get; set; }
        public string mcrFieldActivityType { get; set; }
        public string mcrFieldSbActivityType { get; set; }
        public string mcrVenderName { get; set; }
        public string workOrderNum { get; set; }
        public string readingCord { get; set; }
        public string lugsuntity { get; set; }
        public string prcQuantity { get; set; }
        public string bushingProtectorQuan { get; set; }
        public string busbarQuan { get; set; }
        public string mSealQuan { get; set; }
        public string saddleClampQuan { get; set; }
        public string pipeQuan { get; set; }
        public string glAngleQuan { get; set; }
        public string cuWireQuan { get; set; }
        public string newMtrTerminalSeal1 { get; set; }
        public string newMtrTerminalSeal2 { get; set; }
        public string newMtrHeadSeal1 { get; set; }
        public string newMtrBoxSeal1 { get; set; }
        public string newMtrBoxSeal2 { get; set; }
        public string newMtrBoxSeal3 { get; set; }
        public string newMtrBoxICSeal1 { get; set; }
        public string newMtrBoxICSeal2 { get; set; }
        public string newMtrBoxICSeal3 { get; set; }
        public string newMtrBoxICSeal4 { get; set; }
        public string newMtrBoxOGSeal1 { get; set; }
        public string newMtrBoxOGSeal2 { get; set; }
        public string newMtrBoxOGSeal3 { get; set; }
        public string newMtrBoxOGSeal4 { get; set; }
        public string newBsBarSeal1 { get; set; }
        public string newBsBarSeal2 { get; set; }
        public string newHoloGramTerminalSeal1 { get; set; }
        public string newHoloGramMtrBoxSeal2 { get; set; }
        public string newHoloGramICSeal3 { get; set; }
        public string newHoloGramICSeal4 { get; set; }
        public string newHoloGramOGSeal5 { get; set; }
        public string newHoloGramOGSeal6 { get; set; }
        public string oldMtrTerminalSeal1 { get; set; }
        public string oldMtrTerminalSeal2 { get; set; }
        public string oldMtrHeadSeal1 { get; set; }
        public string oldMtrBoxSeal1 { get; set; }
        public string oldMtrBoxSeal2 { get; set; }
        public string oldMtrBoxSeal3 { get; set; }
        public string oldMtrBoxICSeal1 { get; set; }
        public string oldMtrBoxICSeal2 { get; set; }
        public string oldMtrBoxICSeal3 { get; set; }
        public string oldMtrBoxICSeal4 { get; set; }
        public string oldMtrBoxOGSeal1 { get; set; }
        public string oldMtrBoxOGSeal2 { get; set; }
        public string oldMtrBoxOGSeal3 { get; set; }
        public string oldMtrBoxOGSeal4 { get; set; }
        public string oldBsBarSeal1 { get; set; }
        public string oldBsBarSeal2 { get; set; }
        public string oldHoloGramTerminalSeal1 { get; set; }
        public string oldHoloGramMtrBoxSeal2 { get; set; }
        public string oldHoloGramICSeal3 { get; set; }
        public string oldHoloGramICSeal4 { get; set; }
        public string oldHoloGramOGSeal5 { get; set; }
        public string oldHoloGramOGSeal6 { get; set; }
        public string icCblType { get; set; }
        public string icCbleSize { get; set; }
        public string icCblLayingType { get; set; }
        public string icCblDrumNum { get; set; }
        public string icCblSrlNumFrom { get; set; }
        public string icCblSrlNumTo { get; set; }
        public string icTotalCblLength { get; set; }
        public string feedingPointType { get; set; }
        public string feedingPointSrlNum { get; set; }
        public string cblUsedInOG { get; set; }   //yes or No{ get; set; }
        public string ogCblType { get; set; }
        public string ogCbleSize { get; set; }
        public string ogCblLayingType { get; set; }
        public string ogCblDrumNum { get; set; }
        public string ogCblSrlNumFrom { get; set; }
        public string ogCblSrlNumTo { get; set; }
        public string ogTotalCblLength { get; set; }
        public string removedCblSize { get; set; }
        public string removedCblLength { get; set; }
        public string existingCblSize { get; set; }
        public string existingFeedingPointType { get; set; }
        public string existingFeedingSrNum { get; set; }
        public string newAmrType { get; set; }
        public string newAmrManufacturer { get; set; }
        public string newAmrSimId { get; set; }
        public string newAmrSimNum { get; set; }
        public string newAmrOther { get; set; }
        public string existingAmrType { get; set; }
        public string existingAmrManufacturer { get; set; }
        public string existingAmrSimId { get; set; }
        public string existingAmrSimNum { get; set; }
        public string existingAmrOther { get; set; }
        public string removedAmrType { get; set; }
        public string removedAmrManufacturer { get; set; }
        public string removedAmrSimId { get; set; }
        public string removedAmrSimNum { get; set; }
        public string removedAmrOther { get; set; }
        public string isGunnyBagPrepared { get; set; }
        public string gunnyBagNum { get; set; }
        public string gunnyBagColor { get; set; }
        public string gunnyBagSealNum { get; set; }
        public string labNoticeNum { get; set; }
        public string labTestingDate { get; set; }
        public string userType { get; set; }
        public string gunnyReason { get; set; }
        public string mtrImportReading { get; set; }
        public string mtrInstaniousVolt { get; set; }
        public string mtrInstaniousCurnt { get; set; }
        public string mtrInstaniousPwrFactor { get; set; }
        public string isExportParam { get; set; }
        public string mtrExportReading { get; set; }
        public string isMtrTestingCarriedOut { get; set; }
        public string mtrTestResult { get; set; }
        public string isCTPTCarriedOut { get; set; }
        public string ctptTestRslt { get; set; }
        public string newMtrMfcName { get; set; }
        public string newMtrMfcMonth { get; set; }
        public string newMtrMfcYear { get; set; }
        public string newMtrType { get; set; }
        public string newMtrPhase { get; set; }
        public string newMtrCofig { get; set; }
        public string newMtrAccuracyClass { get; set; }
        public string newMtrCostant { get; set; }
        public string newMtrConstantUnit { get; set; }
        public string newMtrVolatgeRating { get; set; }
        public string newMtrCurrentRating { get; set; }
        public string newMtrDetailOther { get; set; }
        public string removedMtrMfcName { get; set; }
        public string removedMtrMfcMonth { get; set; }
        public string removedMtrMfcYear { get; set; }
        public string removedMtrType { get; set; }
        public string removedMtrPhase { get; set; }
        public string removedMtrCofig { get; set; }
        public string removedMtrAccuracyClass { get; set; }
        public string removedMtrCostant { get; set; }
        public string removedMtrConstantUnit { get; set; }
        public string removedMtrVolatgeRating { get; set; }
        public string removedMtrCurrentRating { get; set; }
        public string removedMtrDetailOther { get; set; }
        public string existingMtrMfcName { get; set; }
        public string existingMtrMfcMonth { get; set; }
        public string existingMtrMfcYear { get; set; }
        public string existingMtrType { get; set; }
        public string existingMtrPhase { get; set; }
        public string existingMtrCofig { get; set; }
        public string existingMtrAccuracyClass { get; set; }
        public string existingMtrCostant { get; set; }
        public string existingMtrConstantUnit { get; set; }
        public string existingMtrVolatgeRating { get; set; }
        public string existingMtrCurrentRating { get; set; }
        public string existingMtrDetailOther { get; set; }
        public string newMtrBoxType { get; set; }
        public string newSinglePhaseMtrType { get; set; }
        public string newSinglePhasePreFittedBox { get; set; }
        public string newLtCtMonth { get; set; }
        public string newLtCtMfgYear { get; set; }
        public string newCtPrimaryValue { get; set; }
        public string newCtSecondaryValue { get; set; }
        public string newLtCtAccuracyClass { get; set; }
        public string newHtMtrType { get; set; }
        public string newHtMonth { get; set; }
        public string newHtMfgYear { get; set; }
        public string newHtCTPrimaryValue { get; set; }
        public string newHtCTSecondryVale { get; set; }
        public string newHtCTAccuracyClass { get; set; }
        public string newHtCTVA { get; set; }
        public string newHtPTPrimaryVale { get; set; }
        public string newHtPTSecondaryValue { get; set; }
        public string newHtPTAccuracyClass { get; set; }
        public string newHtPTVA { get; set; }
        public string newHtCoonectionConfig { get; set; }
        public string newEQId { get; set; }
        public string existingMtrBoxType { get; set; }
        public string existingSinglePhaseMtrType { get; set; }
        public string existingSinglePhasePreFittedBox { get; set; }
        public string existingLtCtMonth { get; set; }
        public string existingLtCtMfgYear { get; set; }
        public string existingCtPrimaryValue { get; set; }
        public string existingCtSecondaryValue { get; set; }
        public string existingLtCtAccuracyClass { get; set; }
        public string existingHtMtrType { get; set; }
        public string existingHtMonth { get; set; }
        public string existingHtMfgYear { get; set; }
        public string existingHtCTPrimaryValue { get; set; }
        public string existingHtCTSecondryVale { get; set; }
        public string existingHtCTAccuracyClass { get; set; }
        public string existingHtCTVA { get; set; }
        public string existingHtPTPrimaryVale { get; set; }
        public string existingHtPTSecondaryValue { get; set; }
        public string existingHtPTAccuracyClass { get; set; }
        public string existingHtPTVA { get; set; }
        public string existingHtCoonectionConfig { get; set; }
        public string existingEQId { get; set; }
        public string removedMtrBoxType { get; set; }
        public string removedSinglePhaseMtrType { get; set; }
        public string removedSinglePhasePreFittedBox { get; set; }
        public string removedLtCtMonth { get; set; }
        public string removedLtCtMfgYear { get; set; }
        public string removedCtPrimaryValue { get; set; }
        public string removedCtSecondaryValue { get; set; }
        public string removedLtCtAccuracyClass { get; set; }
        public string removedHtMtrType { get; set; }
        public string removedHtMonth { get; set; }
        public string removedHtMfgYear { get; set; }
        public string removedHtCTPrimaryValue { get; set; }
        public string removedHtCTSecondryVale { get; set; }
        public string removedHtCTAccuracyClass { get; set; }
        public string removedHtCTVA { get; set; }
        public string removedHtPTPrimaryVale { get; set; }
        public string removedHtPTSecondaryValue { get; set; }
        public string removedHtPTAccuracyClass { get; set; }
        public string removedHtPTVA { get; set; }
        public string removedHtCoonectionConfig { get; set; }
        public string removedEQId { get; set; }
        public string premiseCategory { get; set; }
        public string subPremiseCategory { get; set; }
        public string mtrLoc { get; set; }

        public string caNo { get; set; }
        public string meter_Num { get; set; }
        public string orderType { get; set; }
        public string pmActivityType { get; set; }
        public string oldMtrPhoto1 { get; set; }
        public string oldMtrPhoto2 { get; set; }

        public string consumerAddress { get; set; }
        public string consumerFatherName { get; set; }
        public string sectionedLoad { get; set; }
        public string tariffCategoryValue { get; set; }
        public string isMtrBxInstall { get; set; }
        public string isAmrInstall { get; set; }
        public string isAmrAvailableAtSite { get; set; }
        public string isNewCblInstall { get; set; }
        public string isServiceCblReplaced { get; set; }
        public string mtrImpRdkW { get; set; }
        public string mtrImpRdkVAh { get; set; }
        public string mtrImpRdkVA { get; set; }
        public string mtrInstV1 { get; set; }
        public string mtrInstV2 { get; set; }
        public string mtrInstV3 { get; set; }
        public string mtrInstL1 { get; set; }
        public string mtrInstL2 { get; set; }
        public string mtrInstL3 { get; set; }
        public string mtrPfSign { get; set; }
        public string mtrPfValue { get; set; }
        public string mtrExpKwh { get; set; }
        public string mtrExpkW { get; set; }
        public string mtrExpkVAh { get; set; }
        public string mtrExpkVA { get; set; }
        public string errKwhSign { get; set; }
        public string errkWhvalue { get; set; }
        public string errkVAhSign { get; set; }
        public string errkVAhValue { get; set; }
        public string ctTestRslt { get; set; }
        public string ptTestRslt { get; set; }

        public string mtrHeight { get; set; }
        public string mtrStatus { get; set; }
        public string mtrSupplyStatus { get; set; }
        public string mtrBxStatus { get; set; }
        public string mtrDisplayStatus { get; set; }
        public string mtrDownLoadingStatus { get; set; }
        public string mtrReadingCordStatus { get; set; }
        public string mtrAmrStatus { get; set; }
        public string mtrSrvcCblStatus { get; set; }
        public string mtrBodySealStatus { get; set; }
        public string mtrBodyUltraSonicStatus { get; set; }
        public string mtrTerminalSealStatus { get; set; }
        public string mtrBxSealStatus { get; set; }
        public string mtrBusBarSealStatus { get; set; }
        public string MFOnBill { get; set; }
        public string actualMF { get; set; }
        public string billMfValue { get; set; }
        public string tariffCategory { get; set; }
        public string GIS_LAT { get; set; }
        public string GIS_LONG { get; set; }
        public string looseFlag { get; set; }

        public string removedHtMfc { get; set; }
        public string newHtMfc { get; set; }
        public string rmvdPtRPhasePE { get; set; }
        public string rmvdPtRPhaseRE { get; set; }
        public string rmvdIsCTPTCarriedOut { get; set; }
        public string rmvdErrkVAhValue { get; set; }
        public string rmvdErrkVAhSign { get; set; }
        public string rmvdErrkWhvalue { get; set; }
        public string rmvdErrKwhSign { get; set; }
        public string rmvdIsMtrTestingCarriedOut { get; set; }
        public string rmvdMtrExpkVA { get; set; }
        public string rmvdMtrExpkVAh { get; set; }
        public string rmvdMtrExpkW { get; set; }
        public string rmvdMtrExpKwh { get; set; }
        public string rmvdIsExportParam { get; set; }
        public string rmvdMtrPfValue { get; set; }
        public string rmvdMtrPfSign { get; set; }

        public string rmvdMtrImpRdkWh { get; set; }
        public string rmvdMtrImpRdkW { get; set; }
        public string rmvdMtrImpRdkVAh { get; set; }
        public string rmvdMtrImpRdkVA { get; set; }
        public string rmvdMtrInstV1 { get; set; }
        public string rmvdMtrInstV2 { get; set; }
        public string rmvdMtrInstV3 { get; set; }
        public string rmvdMtrInstL1 { get; set; }
        public string rmvdMtrInstL2 { get; set; }
        public string rmvdMtrInstL3 { get; set; }

        public string ptBPhasePE { get; set; }
        public string ptBPhaseRE { get; set; }
        public string ptYPhasePE { get; set; }
        public string ptYPhaseRE { get; set; }
        public string ptRPhasePE { get; set; }
        public string ptRPhaseRE { get; set; }
        public string ctBPhasePE { get; set; }
        public string ctBPhaseRE { get; set; }
        public string ctYPhasePE { get; set; }
        public string ctYPhaseRE { get; set; }
        public string ctRPhasePE { get; set; }
        public string ctRPhaseRE { get; set; }
        public string existingAmrSrNum { get; set; }
        public string existingMtrBoxSrNum { get; set; }
        public string newLtCtMfc { get; set; }
        public string existingLtCtMfc { get; set; }
        public string removedLtCtMfc { get; set; }
        public string existingHtMfc { get; set; }
        public string rmvdCtRPhaseRE { get; set; }
        public string rmvdCtRPhasePE { get; set; }
        public string rmvdCtYPhaseRE { get; set; }
        public string rmvdCtYPhasePE { get; set; }
        public string rmvdCtBPhaseRE { get; set; }
        public string rmvdCtBPhasePE { get; set; }

        public string rmvdPtYPhaseRE { get; set; }
        public string rmvdPtYPhasePE { get; set; }
        public string rmvdPtBPhaseRE { get; set; }
        public string rmvdPtBPhasePE { get; set; }



        public string isOldMtrReplaced { get; set; }
        public string isOldMtrBxReplaced { get; set; }
        public string isAmrRmvdOrReplaced { get; set; }

        public string mtrImpRdkWh { get; set; }
        public string registerConsumerContactNum { get; set; }
        public string isNewMtrInstallAtSite { get; set; }
    }
    public class OrderCancellation
    {
        public string Ordertype { get; set; }
        public string UserId { get; set; }
        public string OrderNum { get; set; }
        public string ImagePath { get; set; }
        public string reason { get; set; }
        public string Remarks { get; set; }
        public string customerName { get; set; }
        public string customerNumber { get; set; }
        public string custDate { get; set; }
    }
    public class Change_Password
    {
        public string User_ID { get; set; }
        public string Current_Password { get; set; }
        public string New_Password { get; set; }
    }

    public class ENF_VALIDATE_CA
    {
        public string Company_Code { get; set; }
        public string ENF_CA { get; set; }
        public string CONSUMER_NAME { get; set; }
        public string DIVISION { get; set; }
        public string NEARBYMETER_NO { get; set; }
        public string EMP_NAME { get; set; }
        public string EMP_NUM { get; set; }
        public string OCCUPANTCONT_NO { get; set; }
        public string OCCUPANTCONT_NAME { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string siteTraceable { get; set; }
        public string TheftFound { get; set; }
        public string checkBoxInput { get; set; }
        public string Remarks { get; set; }
        public string Enforcement_Bill_No { get; set; }
        public string Punch_Date { get; set; }
    }

    public class Enf_Insert_Data
    {
        public string COMPANY { get; set; }
        public string DIVISION { get; set; }
        public string CA_No { get; set; }
        public string Consumer_Name { get; set; }
        public string OCCUPANTCONT_NAME { get; set; }
        public string OCCUPANTCONT_NO { get; set; }
        public string EMP_NAME { get; set; }
        public string EMP_NUM { get; set; }
        public string NEARBYMETER_NO { get; set; }
        public string Image_Path { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Remarks { get; set; }
        public string checkBoxInput { get; set; }
        public string siteTraceable { get; set; }
        public string TheftFounds { get; set; }
        public string IMEI { get; set; }
    }

    public class Emp_Data
    {
        public string Emp_Name { get; set; }
        public string Emp_Id { get; set; }
    }

    public class Version_Data
    {
        public string Module_Name { get; set; }
        public string Version { get; set; }
    }

    public class Power_Theft_CA
    {
        public string Inputvalue { get; set; }
        public string CA { get; set; }
        public string MeterNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Division { get; set; }
    }
    public class CA_Details
    {
        public string Inputvalue { get; set; }
        public string CA { get; set; }
        public string MeterNo { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string Division { get; set; }
        public string Tarriff_Cat { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Pin { get; set; }
        public string Bill_Class { get; set; }

        
    }

    public class Insert_Power_Theft
    {
        public string Area { get; set; }
        public string Division { get; set; }
        public string SearchBy { get; set; }
        public string OffenderName { get; set; }
        public string OffenderAddress { get; set; }
        public string NatureOfTheft { get; set; }
        public string Load { get; set; }
        public string Usage { get; set; }
        public string TheftDescription { get; set; }
        public string Landmark { get; set; }
        public string SendPersonalDetail { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
    }
    public class Area_List
    {
        public string AREA_NAME { get; set; }
    }
    public class Area_Division
    {
        public string AREA_NAME { get; set; }
        public string DIV_CODE { get; set; }
        public string DIVISION { get; set; }
    }

    public class EnactStatus_Input
    {
        public string Enact_API_Res_Input { get; set; }
        public string NM_Number_Input { get; set; }


    }
    public class EnactStatus_Return
    {
        public string Enact_API_Res_Input { get; set; }
        public string NM_Number_Input { get; set; }
        public string Enact_API_Res { get; set; }
        public string NM_Number { get; set; }
        public string Status_Consu { get; set; }
        public string Next_Step { get; set; }
        public string Remarks { get; set; }
        public string LastUpdated_Date { get; set; }

    }
    public class LeadDataCollection
    {
        public List<Data> data;
    }
    public class Data
    {
        public string additionalInfo { get; set; }
        public string address1 { get; set; }
        public string annual_electric_bill { get; set; }
        public string application_type { get; set; }
        public string bill_type { get; set; }
        public string company_name { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string latitude { get; set; }
        public string last_name { get; set; }
        public string longitude { get; set; }
        public string phone { get; set; }
        public string phone2 { get; set; }
        public string ca_number { get; set; }
        public string city { get; set; }
        public string consumer_category { get; set; }
        public string country { get; set; }
        public string create { get; set; }
        public string create_ts { get; set; }
        public string crn { get; set; }
        public string current_annual_consumption { get; set; }
        public string division_name { get; set; }
        public string financial_year { get; set; }
        public string financial_model { get; set; }
        public string includeAllRatesEnabled { get; set; }
        public string inverter_make { get; set; }
        public string inverter_numbers { get; set; }
        public string isLatLongAddress { get; set; }
        public string last_quote_amount { get; set; }
        public string last_update { get; set; }
        public string lead_generation_process { get; set; }
        public string module_make { get; set; }
        public string nm_number { get; set; }
        public string number_modules { get; set; }
        public string sanctioned_load { get; set; }
        public string source { get; set; }
        public string project_owner_email { get; set; }
        public string state { get; set; }
        public string status { get; set; }
        public string supply_type { get; set; }
        public string tariff_category { get; set; }
        public string type { get; set; }
        public string update_ts { get; set; }
        public string utility_price { get; set; }
        public string workflow_name { get; set; }
        public string zip_code { get; set; }
        public string existing_solar_capacity { get; set; }
        public string applied_solar_capacity { get; set; }
        public string inverter_model_number { get; set; }
        public string inverter_phase_type { get; set; }
        public string inverter_rating_kw { get; set; }
        public string module_rating_wp { get; set; }
        public string name_of_installer { get; set; }
        public string contact_number_of_installer { get; set; }
        public string email_id_of_installer { get; set; }
        public string net_meter_number { get; set; }
        public string solar_meter_number { get; set; }
        public string total_solar_load_on_ca { get; set; }
        public string first_site_visit_remarks { get; set; }
        public string second_site_visit_remarks { get; set; }
        public string third_site_visit_remarks { get; set; }
        public string net_meter_type { get; set; }
        public string solar_meter_type { get; set; }
        public string last_completed_task { get; set; }
        public string task_completed { get; set; }
    }

    #region "Hooter Variables"
    public class HooterLogindetails
    {

        public string userID { get; set; }
        public string password { get; set; }
    }

    public class Insert_Hooter_Data
    {
        public string Division { get; set; }
        public string CA_Number { get; set; }
        public string Meter_No { get; set; }
        public string Consumer_Name { get; set; }
        public string Consumer_Address { get; set; }
        public string Consumer_Mob { get; set; }
        public string Logitude { get; set; }
        public string Latitude { get; set; }
        public string Remarks { get; set; }
        public string IMEI { get; set; }
        public string Punch_ID { get; set; }
    }

    #endregion

    public class BSES_SMS_API_DATA
    {
        public string AppName { get; set; }
        public string EncryptionKey { get; set; }
        public string CompanyCode { get; set; }
        public string VendorCode { get; set; }
        public string EmpCode { get; set; }
        public string MobileNo { get; set; }
        public string OTPMsg { get; set; }       

    }
}