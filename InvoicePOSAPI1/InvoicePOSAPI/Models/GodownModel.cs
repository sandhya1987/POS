using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class GodownModel
    {
        public long? GODOWN_ID { get; set; }
        public string GODOWN_NAME { get; set; }
        public string GOSOWN_DESCRIPTION { get; set; }
        public bool? IS_ACTIVE { get; set; }
        public long? COMPANY_ID { get; set; }
        public bool? IS_DELETE { get; set; }
        public bool? IS_DEFAULT_GODOWN { get; set; }
    }
}