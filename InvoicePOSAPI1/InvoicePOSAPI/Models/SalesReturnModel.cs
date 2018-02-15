using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;



namespace InvoicePOSAPI.Models
{
    public class SalesReturnModel
    {
        public long SALES_RETURN_ID { get; set; }
        public long? COMPANY_ID { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public string SALES_RETURN_NO { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_NAME { get; set; }
        public bool? IS_WITHOUT_INVOICE { get; set; }
        public DateTime? RETURN_DATE { get; set; }
        public string GODOWN { get; set; }
        public long? CUSTOMER_ID { get; set; }
        public decimal? OUTSTANDING_BALANCE { get; set; }
        public string INVOICE_NO { get; set; }
        public string REFERENCE_NUMBER { get; set; }
        public decimal? TAX_AMOUNT { get; set; }
        public decimal? FREIGHT_CHARGE { get; set; }
        public decimal? PACKING_CHARGE { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
        public int? ROUND_OFF_AMOUNT { get; set; }
        public decimal? GRAND_TOTAL { get; set; }
        public decimal? SUBSIDY_AMOUN { get; set; }
        public decimal? CUS_PENDING_AMOUNT { get; set; }
        public string SEARCH_CODE { get; set; }
        public string NOTES { get; set; }
        public string SALES_EXECUTIVE { get; set; }
        public int? INVOICE_ID { get; set; }
        public int SALE_ID { get; set; }
        public int OPN_QNT { get; set; }
        public int TOTAL_QTY { get; set; }
        public int TOTAL_ITEM { get; set; }
        public int Current_Qty { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string CUSTOMER_MOBILE { get; set; }
        public ObservableCollection<SalesReturnModel> SelectedItem { get; set; }
    }
}