using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class CatagoryModel
    {
        public long CATAGORY_ID { get; set; }
        public string CATAGORY_NAME { get; set; }
        public string BAR_CODE_PREFIX { get; set; }
        public string CATAGORY_DEC { get; set; }
        public string DISPLAY_INDEX { get; set; }
        public bool? IS_NOT_PROTAL { get; set; }
        public long? COMPANY_ID { get; set; }
        public bool? IS_DELETE { get; set; }
    }
}