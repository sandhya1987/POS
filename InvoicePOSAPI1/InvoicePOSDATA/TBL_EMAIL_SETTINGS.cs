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
    
    public partial class TBL_EMAIL_SETTINGS
    {
        public long USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string EMAIL { get; set; }
        public string NAME { get; set; }
        public string PASSWORD { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string SMTP_SERVER { get; set; }
        public Nullable<bool> IS_REQ_ENCRYPT_CONN { get; set; }
        public Nullable<bool> IS_DELETE { get; set; }
        public string SMTP_SERVER_PORT { get; set; }
        public long ID { get; set; }
        public bool IS_GMAIL { get; set; }
        public string MAIL_TYPE { get; set; }
    }
}