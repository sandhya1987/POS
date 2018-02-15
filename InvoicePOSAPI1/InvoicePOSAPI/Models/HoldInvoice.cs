using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class HoldInvoice
    {
        public int Invoice_ID { get; set; }
        public string Invoice_No { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Barcode { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalTax { get; set; }
    }
}