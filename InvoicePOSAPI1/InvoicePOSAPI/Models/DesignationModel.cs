using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class DesignationModel
    {
        public long? DESIGNATION_ID { get; set; }
        public string DESIGNATION_NAME { get; set; }
        public long COMPANY_ID { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public long? CREATED_BY { get; set; }
        public bool? STATUS { get; set; }
        public long? SORT_INDEX { get; set; }
        public long SLNO { get; set; }
        public bool? IS_DELETE { get; set; }
    }


    public class DesgModel
    {
        public long? DESIGNATION_ID { get; set; }
        public string DESIGNATION_NAME { get; set; }
        
    }
}