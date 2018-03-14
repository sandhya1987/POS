using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class ReportModel
    {
        public long REPORT_ID { get; set; }
        public long? COMPANY_ID { get; set; }
        public int SLNO { get; set; }
        public string REPORT_NAME { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public bool? IS_DELETE { get; set; }
        public string STATUS { get; set; }
        
    }
}