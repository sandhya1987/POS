using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class TaxModel
    {
        public int? TAX_ID { get; set; }
        public string COMPANY { get; set; }
        public string TAX_NAME { get; set; }
        public decimal? TAX_VALUE { get; set; }
        public long? COMPANY_ID { get; set; }
        public bool? IS_SEPARATE { get; set; }
        public bool? IS_DELETE { get; set; }
    }
}