using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class CustomerGroupModel
    {
        public long? CUSTOMER_GROUP_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public long? COMPANY_ID { get; set; }
        public bool? IS_DELETE { get; set; }
    }
}