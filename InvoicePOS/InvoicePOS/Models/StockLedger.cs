using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
   public class StockLedger
    {
        public long STOCK_TRANSFER_ID { get; set; }
        public string STOCK_TRANSFER_NUMBER { get; set; }
        public DateTime DATE { get; set; }
        public string EMAIL { get; set; }
        public bool IS_SEND { get; set; }
        public long ITEM_ID { get; set; }
        public bool IS_NEGATIVE_STOCK_MESSAGE { get; set; }
        public int TRANS_QUANTITY { get; set; }
        public string SEARCH_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string BARCODE { get; set; }
        public int TOTAL_STOCK_QNT { get; set; }
        public long COMPANY_ID { get; set; }
        public long FROM_GODOWN_ID { get; set; }
        public long TO_GODOWN_ID { get; set; }
        public long RECEIVED_BY_ID { get; set; }
        public long BUSINESS_LOCATION_ID { get; set; }
        public string FROM_GODOWN { get; set; }
        public string TO_GODOWN { get; set; }
        public string RECEIVED_BY { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public int CHNG_QNT { get; set; }
        public long stockin { get; set; }
        public int stockout { get; set; }
        public int? OPN_QNT { get; set; }
        public int SLNO { get; set; }
    }
}
