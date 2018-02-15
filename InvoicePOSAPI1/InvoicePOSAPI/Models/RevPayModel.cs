using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class RevPayModel
    {
        public long? COMPANY_ID { get; set; }
        public long? RECEIVE_PAYMENT_ID { get; set; }
        public string RECEIVE_PAY_NO { get; set; }
        public long? BUSINESS_LOCATION_ID { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public DateTime? DATE { get; set; }
        public long? CUSTOMER_ID { get; set; }
        public string CUSTOMER { get; set; }
        public string CUSTOMER_EMAIL { get; set; }
        public bool? IS_EMAIL_SEND { get; set; }
        public bool? IS_DELETE { get; set; }
        public string CUSTOMER_CONTACT_NO { get; set; }
        public decimal? OTHER_PAY_AMT { get; set; }
        public decimal? CURRENT_PAY_AMT { get; set; }
        public decimal? TOTAL_SELECTED_PAY_AMT { get; set; }
        public decimal? PENDING_AMT { get; set; }
        public decimal? TOTAL_REC_AMT { get; set; }
        public decimal? RETURNABLE_AMT { get; set; }
    }
}