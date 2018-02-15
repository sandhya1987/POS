using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class EmailSettingsModel
    {
        public string BCC { get; set; }
        public string CC { get; set; }
        public string EMAIL { get; set; }
        public long ID { get; set; }
        public bool IS_GMAIL { get; set; }
        public bool? IS_REQ_ENCRYPT_CONN { get; set; }
        public string MAIL_TYPE { get; set; }
        public string NAME { get; set; }
        public string PASSWORD { get; set; }
        public string SMTP_SERVER { get; set; }
        public string SMTP_SERVER_PORT { get; set; }
        public long USER_ID { get; set; }
        public string USER_NAME { get; set; }
    }
}