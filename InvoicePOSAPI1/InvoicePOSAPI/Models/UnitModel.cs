using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class UnitModel
    {
        public long? UNIT_ID { get; set; }
        public string MEASURING_NAME { get; set; }
        public string SHORT_INDAX { get; set; }
        public string IMAGE_PATH { get; set; }
        public bool? IS_DELETE { get; set; }
        public long? COMPANY_ID { get; set; }

    }
}