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
    
    public partial class TBL_TRANSFER_CASH
    {
        public long TRANSFER_ID { get; set; }
        public string CASH_TRANSFER_NUMBER { get; set; }
        public Nullable<long> COMPANY_ID { get; set; }
        public Nullable<long> BUSINESS_LOC_ID { get; set; }
        public string BUSINESS_LOC { get; set; }
        public string FROM_CASH_REGISTER { get; set; }
        public string TO_CASH_REGISTER { get; set; }
        public Nullable<long> CASH_REGISTERID_FROM { get; set; }
        public Nullable<long> CASH_REGISTERID_TO { get; set; }
        public Nullable<decimal> TOTAL_TRANSFERED_AMOUNT { get; set; }
        public Nullable<System.DateTime> TRANSFER_DATE { get; set; }
        public Nullable<bool> IS_TRANSFER_CASH_REGISTER { get; set; }
        public Nullable<bool> IS_DELETE { get; set; }
        public string STATUS { get; set; }
    }
}
