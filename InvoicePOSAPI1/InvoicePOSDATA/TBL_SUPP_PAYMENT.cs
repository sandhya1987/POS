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
    
    public partial class TBL_SUPP_PAYMENT
    {
        public long SUPP_PAYMENT { get; set; }
        public string SUPP_EMAIL { get; set; }
        public string SUPP_SMS { get; set; }
        public Nullable<bool> IS_SEND_EMAIL { get; set; }
        public Nullable<bool> IS_SEND_SMS { get; set; }
        public Nullable<decimal> CURRENT_ADV_AMT { get; set; }
        public Nullable<decimal> TOTAL_RIE_AMT { get; set; }
        public Nullable<decimal> PENDING_AMT { get; set; }
        public Nullable<System.DateTime> PAYMENT_DATE { get; set; }
        public Nullable<decimal> CURRENT_PAYABLE_AMT { get; set; }
        public Nullable<decimal> TOTAL_PANDING { get; set; }
        public Nullable<decimal> SELECTED_AMT { get; set; }
        public Nullable<decimal> PENDING_AFTE_PAYMENT { get; set; }
        public Nullable<decimal> CASH_REG_AMT { get; set; }
        public Nullable<decimal> CHEQUE_AMT { get; set; }
        public string CHEQUE_NO { get; set; }
        public string CHEQUE_BANK_BRANCH { get; set; }
        public string CHEQUE_BANK_AC { get; set; }
        public Nullable<System.DateTime> CHEQUE_DATE { get; set; }
        public Nullable<decimal> TRANSFER_AMT { get; set; }
        public string TRANSFER_BANK_BRANCH { get; set; }
        public Nullable<long> TRANSFER_BANK_AC { get; set; }
        public Nullable<System.DateTime> TRANSFER_DATE { get; set; }
        public Nullable<decimal> FINANCAL_AMT { get; set; }
        public Nullable<long> FINACIAL_AC { get; set; }
        public Nullable<int> DISCOUNT_FLAT { get; set; }
        public Nullable<int> DISCOUNT_PERCENT { get; set; }
        public Nullable<int> TOTAL_PAYMENT_MODES { get; set; }
        public Nullable<decimal> CURRENT_PAYMENT { get; set; }
        public string NOTE { get; set; }
        public Nullable<long> COMPANY_ID { get; set; }
        public string CASH_REG { get; set; }
        public string SUPP { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public string PAYMENT_NUMBER { get; set; }
        public Nullable<long> SUPP_ID { get; set; }
        public Nullable<long> CASH_REG_ID { get; set; }
        public Nullable<bool> IS_DELETE { get; set; }
        public Nullable<bool> IS_PRINT_CHECK { get; set; }
        public Nullable<long> USER_ID { get; set; }
        public string SuppPayNo { get; set; }
    }
}