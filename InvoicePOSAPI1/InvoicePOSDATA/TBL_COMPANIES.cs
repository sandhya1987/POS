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
    
    public partial class TBL_COMPANIES
    {
        public long COMPANY_ID { get; set; }
        public Nullable<System.DateTime> CREATION_DATE { get; set; }
        public string COMPANY_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string COUNTRY { get; set; }
        public string POSTCODE { get; set; }
        public string TELEPHONE { get; set; }
        public string FAX { get; set; }
        public string WEBSITE { get; set; }
        public string START_YEAR { get; set; }
        public Nullable<bool> IS_DELETE { get; set; }
        public string COMPANY_STATUS { get; set; }
    }
}
