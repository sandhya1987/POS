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
    
    public partial class TBL_INVOICE_PAY
    {
        public long INVOICE_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public Nullable<System.DateTime> INVOICE_DATE { get; set; }
        public string CUSTOMER { get; set; }
        public Nullable<int> CUSTOMER_ID { get; set; }
        public Nullable<decimal> AVAILABLE_CREDIT_LIMIT { get; set; }
        public string SALES_EXECUTIVE { get; set; }
        public Nullable<long> SALES_EXECUTIVE_ID { get; set; }
        public string CUSTOMER_EMAIL { get; set; }
        public string CUSTOMER_MOBILE_NO { get; set; }
        public Nullable<decimal> BEFORE_ROUNDOFF { get; set; }
        public Nullable<int> ROUNDOFF_AMOUNT { get; set; }
        public Nullable<int> TOTAL_AMOUNT { get; set; }
        public Nullable<int> QUANTITY_TOTAL { get; set; }
        public Nullable<int> NUMBER_OF_ITEM { get; set; }
        public Nullable<decimal> DISCOUNT_INCLUDED { get; set; }
        public Nullable<decimal> TAX_INCLUDED { get; set; }
        public Nullable<decimal> RECIVED_AMOUNT { get; set; }
        public Nullable<decimal> RETURNABLE_AMOUNT { get; set; }
        public Nullable<decimal> COMMISION_EXPENSE { get; set; }
        public Nullable<decimal> PENDING_AMOUNT { get; set; }
        public string NOTE { get; set; }
        public Nullable<bool> IS_GOODS_DELIVERED { get; set; }
        public Nullable<bool> IS_PRINT { get; set; }
        public Nullable<bool> IS_SAVE_RETURNABLE_AMOUNT { get; set; }
        public Nullable<long> USER_ID { get; set; }
        public Nullable<decimal> TOTAL_TAX { get; set; }
        public Nullable<long> BUSINESS_LOCATION_ID { get; set; }
        public string BUSINESS_LOCATION_NAME { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public string STATUS { get; set; }
        public Nullable<decimal> SALES_RETURN_AMOUNT { get; set; }
        public string EMPLOYEE_ID { get; set; }
    }
}
