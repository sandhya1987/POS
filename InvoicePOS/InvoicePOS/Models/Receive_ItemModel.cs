using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class Receive_ItemModel
    {
        public int NEWRECEIVE_ID { get; set; }
        public string SELECT_BUSINESS_LOCATION { get; set; }
        public string RECEIVED_ITEM_ENTRY_NO { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public string SUPPLIER_INVOICE_NO { get; set; }
        public DateTime SUPPLIER_INVOICE_DATE { get; set; }
        public DateTime DATE { get; set; }
        public DateTime RECEIVED_ITEM_ON_DATE { get; set; }
        public long PO_NUMBER { get; set; }
        public string SUPPLIER { get; set; }
        public int PAYMENT_TERMS { get; set; }
        public string GODOWN { get; set; }
        public string RECEIVING_EMPLOYEE { get; set; }
        public decimal DISCOUNT_FLAT { get; set; }
        public decimal DISCOUNT_PERCENT { get; set; }
        public decimal ADDTIONAL_CHARGES { get; set; }
        public decimal SUB_TOTAL_BEFORETAX { get; set; }
        public decimal TOTAL_TAX_AMT { get; set; }
        public decimal SUB_TOTAL { get; set; }
        public decimal ROUND_OFF_ADJUSTMENTAMT { get; set; }
        public decimal TOTAL_AMT { get; set; }
        public decimal USE_SUPPLIER_ADVANCE_AMT { get; set; }
        public decimal GRAND_TOTAL { get; set; }
        public string NOTE { get; set; }
        public long COMPANY_ID { get; set; }
        public int? OPN_QNT { get; set; }
        public string SERCH_CODE { get; set; }
        public decimal? TAX_PAID { get; set; }
        public decimal? TAX_COLLECTED { get; set; }
        public string PURCHASE_UNIT { get; set; }
        public string SALES_UNIT { get; set; }
        public decimal? PURCHASE_UNIT_PRICE { get; set; }
        public decimal? SALES_PRICE { get; set; }
        public decimal? MRP { get; set; }



    }
}
