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
    
    public partial class TBL_REGISTER_COMPANIES
    {
        public long REGISTER_ID { get; set; }
        public Nullable<long> COMPANY_ID { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public Nullable<System.DateTime> REGISTERED_DATE { get; set; }
        public Nullable<bool> IS_DELETE { get; set; }
    }
}