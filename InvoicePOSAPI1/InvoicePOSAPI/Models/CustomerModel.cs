using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class CustomerModel 
    {
        public long? COMPANY_ID { get; set; }
        public int CUSTOMER_ID { get; set; }
        public string CUSTOMER_NUMBER { get; set; }
        public string NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public string VAT_NUMBER { get; set; }
        public string CST_NUMBER { get; set; }
        public bool IS_ACTIVE { get; set; }
        public string TIN { get; set; }
        public string PAN { get; set; }
        public long? BUSINESS_LOCATION_ID { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public string BILLING_TO_NAME { get; set; }
        public string BILLING_ADDRESS1 { get; set; }
        public string BILLING_ADDRESS2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string POSTAL_CODE { get; set; }
        public string COUNTRY { get; set; }
        public string MOBILE_NO { get; set; }
        public string TELEPHON_NUMBER { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public bool? SAMEBILLINGANDSHIPPING_ADDRESS { get; set; }
        public bool? IS_ENROLLED_FOR_LOYALITY { get; set; }
        public string NOTES { get; set; }
        public string LOYALTY_NO { get; set; }
        public string SHIPPING_ADDRESS1 { get; set; }
        public string SHIPPING_ADDRESS2 { get; set; }
        public string SHIPPING_CITY { get; set; }
        public string SHIPPING_STATE { get; set; }
        public string SHIPPING_POSTAL_CODE { get; set; }
        public string SHIPPING_COUNTRY { get; set; }
        public string SHIPPING_MOBILE_NO { get; set; }
        public string SHIPPING_TELEPHON_NUMBER { get; set; }
        public string SHIPPING_EMAIL_ADDRESS { get; set; }
        public string REFERRED_BY { get; set; }
        public decimal? OPENING_AMT { get; set; }


        public string IMAGE_PATH { get; set; }
        public decimal? CURRENT_OPENING_BALANCE { get; set; }
        public Nullable<bool> DEFAULT_CREIT_LIMIT { get; set; }
        public string CUSTOMER_GROUP { get; set; }

        public long OPENING_BALANCE_ID { get; set; }
        public DateTime DATE { get; set; }
        public long BAL_TYPE_ID { get; set; }
        public string BAL_TYPE_VALUE { get; set; }
        public decimal CLOSING_AMT { get; set; }
        public DateTime SYSTEM_DATE { get; set; }
        
    }

    //public class Openingbalnce  
    //{
        

    //}
}