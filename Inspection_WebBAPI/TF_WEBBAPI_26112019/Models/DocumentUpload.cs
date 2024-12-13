using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF_WEBBAPI_26112019.Models
{
    public class DocumentUpload
    {
        public string CompanyName { get; set; }
        public string Circle { get; set; }
        public string Division { get; set; }
        public string Document_Date { get; set; }
        public string UploadBy { get; set; }
        public string Document_Name { get; set; }
        public string DETAIL_REMARKS { get; set; }
        public string DOC_TYPE { get; set; }
        public string DOC_PHOTO { get; set; }
        public string DOC_EXT { get; set; }

        public string MONTH_YEAR { get; set; }
    }
}