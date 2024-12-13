using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for SolrResponse
/// </summary>
public class SolrResponse
{
    public SolrData response { get; set; }
}
public class SolrOptions
{
    public string SolrUrl { get; set; }
}
public class SolrData
{
    public int numFound { get; set; }
    public SolrDocument[] docs { get; set; }
}
public class SolrDocument
{
    [JsonProperty("SAP_DIVISION")]
    public string SAP_DIVISION { get; set; }

    [JsonProperty("ACCOUNT_CLASS")]
    public string ACCOUNT_CLASS { get; set; }

    [JsonProperty("CONSUMER_STATUS")]
    public string CONSUMER_STATUS { get; set; }
}




public class RootObject
{
    public ResponseHeader responseHeader { get; set; }
    public Response response { get; set; }
    public List<DocsModel> DocsModel { get; set; }
}

public class ResponseHeader
{
    public int status { get; set; }
    public int QTime { get; set; }
    public Dictionary<string, string> @params { get; set; }
}

public class Response
{
    public int numFound { get; set; }
    public int start { get; set; }
    public bool numFoundExact { get; set; }
    public List<Dictionary<string, object>> docs { get; set; }
}

public class DocsModel
{
    public string MOVE_OUT { get; set; }
    public string CONNECTION_TYPE { get; set; }
    public string ACTIVE { get; set; }
    public string SANCTIONED_LOAD { get; set; }
    public string RATE_CATEGORY { get; set; }
    public string BP_TYPE { get; set; }
    public string SAP_POLE_ID { get; set; }
    public string LAST_NAME { get; set; }
    public string TEL_NUMBER { get; set; }
    public string DIVISION { get; set; }
    public string FIRST_NAME { get; set; }
    public string MRU { get; set; }
    public string REC_ENTRY_DATE { get; set; }
    public string SUB_DIVISION { get; set; }
    public string DEVICE_NO { get; set; }
    public string INSTALLATION_TYPE { get; set; }
    public string CONSUMER_STATUS { get; set; }
    public string COMPANY_CODE { get; set; }
    public string ID { get; set; }
    public string CSTS_CD { get; set; }
    public string CONTRACT_ACCOUNT { get; set; }
    public string SAP_DEPARTMENT { get; set; }
    public string SEQUENCE_NO { get; set; }
    public string BILL_DISPATCH_CONTROL { get; set; }
    public string SAP_DIVISION { get; set; }
    public string ACCOUNT_CLASS { get; set; }
    public string MOBILE_NO { get; set; }
    public string SAP_NAME { get; set; }
    public string TARIFF { get; set; }
    public string CONS_REF { get; set; }
    public string SAP_ADDRESS { get; set; }
    public string MOVE_IN { get; set; }
    public string COMBINED_ADDRESS { get; set; }
    public string POLE_NO { get; set; }
    public string BUSINESS_PARTNER { get; set; }
    public string id { get; set; }
    public long _version_ { get; set; }
}
public class DocsInfo
{
    public string MOVE_OUT { get; set; }
    public string CONNECTION_TYPE { get; set; }
    public string ACTIVE { get; set; }
    public string SANCTIONED_LOAD { get; set; }
    public string RATE_CATEGORY { get; set; }
    public string BP_TYPE { get; set; }
    public string SAP_POLE_ID { get; set; }
    public string LAST_NAME { get; set; }
    public string TEL_NUMBER { get; set; }
    public string DIVISION { get; set; }
    public string FIRST_NAME { get; set; }
    public string MRU { get; set; }
    public string REC_ENTRY_DATE { get; set; }
    public string SUB_DIVISION { get; set; }
    public string DEVICE_NO { get; set; }
    public string INSTALLATION_TYPE { get; set; }
    public string CONSUMER_STATUS { get; set; }
    public string COMPANY_CODE { get; set; }
    public string ID { get; set; }
    public string CSTS_CD { get; set; }
    public string CONTRACT_ACCOUNT { get; set; }
    public string SAP_DEPARTMENT { get; set; }
    public string SEQUENCE_NO { get; set; }
    public string BILL_DISPATCH_CONTROL { get; set; }
    public string SAP_DIVISION { get; set; }
    public string ACCOUNT_CLASS { get; set; }
    public string MOBILE_NO { get; set; }
    public string SAP_NAME { get; set; }
    public string TARIFF { get; set; }
    public string CONS_REF { get; set; }
    public string SAP_ADDRESS { get; set; }
    public string MOVE_IN { get; set; }
    public string COMBINED_ADDRESS { get; set; }
    public string POLE_NO { get; set; }
    public string BUSINESS_PARTNER { get; set; }
    public string id { get; set; }
    public long _version_ { get; set; }
    public string Email { get; set; }
}