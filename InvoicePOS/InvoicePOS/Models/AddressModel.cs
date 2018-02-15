using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class Billing_AddressModel
    {
        public long BILLING_ADRESS_ID { get; set; }
        public Nullable<int> CUSTOMER_ID { get; set; }
        public string BILLING_TO_NAME { get; set; }
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string POSTAL_CODE { get; set; }
        public string COUNTRY { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string TELEPHONE { get; set; }
    }
    public class AddressModel
    {
        public long SHIPPING_ADRESS_ID { get; set; }
        public Nullable<int> CUSTOMER_ID { get; set; }
        public string SHIPPING_TO_NAME { get; set; }
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string POSTAL_CODE { get; set; }
        public string COUNTRY { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string TELEPHONE { get; set; }
    }
}
