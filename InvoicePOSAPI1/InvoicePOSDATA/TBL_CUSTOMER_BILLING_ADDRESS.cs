//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InvoicePOSDATA
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_CUSTOMER_BILLING_ADDRESS
    {
        public long BILLING_ADRESS_ID { get; set; }
        public Nullable<int> CUSTOMER_ID { get; set; }
        public string BILLING_TO_NAME { get; set; }
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string COUNTRY { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string TELEPHONE { get; set; }
        public string PIN { get; set; }
        public string POSTAL_CODE { get; set; }
        public Nullable<long> USER_ID { get; set; }
    }
}