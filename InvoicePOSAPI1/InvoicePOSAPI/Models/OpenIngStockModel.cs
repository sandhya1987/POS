using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class OpenIngStockModel
    {
        public long OPENING_STOCK_ID { get; set; }
        public DateTime? DATE { get; set; }
        public long? ITEM_ID { get; set; }
        public long? BUSINESS_LOC_ID { get; set; }
        public long? COMPANY_ID { get; set; }
        public decimal? OPRNING_BAL { get; set; }
        public decimal? CLOSING_BAL { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_NAME { get; set; }

        public string GODOWN { get; set; }
        public string COMPANY_NAME { get; set; }
        public int OPN_QNT { get; set; }
    }
}