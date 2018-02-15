using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class VendorModel
    {
        public long SUPPLIER_ID { get; set; }
        public string SUPPLIER_CODE { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public string VAT_NO { get; set; }
        public string CST_NO { get; set; }
        public string TIN_NO { get; set; }
        public string PAN_NO { get; set; }
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string ZIP_CODE { get; set; }
        public decimal OPENING_BALANCE { get; set; }
        public bool IS_PREFERRED_SUPPLIER { get; set; }
        public bool IS_ACTIVE { get; set; }
        public int PAYMENT_SETTLED { get; set; }
        public string IMAGE_PATH { get; set; }
        public string CONTACT_FRIST_NAME { get; set; }
        public string CONTACT_LAST_NAME { get; set; }
        public string CONTACT_TELEPHONE_NO { get; set; }
        public string CONTACT_FAX_NO { get; set; }
        public string CONTACT_MOBILE_NO { get; set; }
        public string CONTACT_WEBSITE { get; set; }
        public string CONTACT_EMAIL { get; set; }
    }
}
