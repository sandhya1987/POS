using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class POModel
    {
        public int ITEM_ID { get; set; }
        public decimal Discount { get; set; }
        public decimal? Total { get; set; }
        public int Current_Qty { get; set; }
        public decimal? TotalTax { get; set; }
        public long PO_ID { get; set; }
        public string PO_NUMBER1 { get; set; }
        public int PO_NUMBER { get; set; }
        public string PO_STATUS { get; set; }
        public string PO_TYPE { get; set; }
        public DateTime? DELIVERY_DATE { get; set; }
        public DateTime? PO_DATE { get; set; }
        public bool? IS_SEND_MAIL { get; set; }
        public string BAR_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public long? COMPANY_ID { get; set; }
        public string DELIVER { get; set; }
        public long DELIVER_ID { get; set; }
        public long SUPPLIER_ID { get; set; }
        public string SUPPLIER { get; set; }
        public long BUSINESS_LOCATION_ID { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public string DELIVER_TO { get; set; }
        public string SUPPLIER_EMAIL { get; set; }
        public string TERMS { get; set; }
        public decimal? TOTAL_TAX { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
        public string SearchStock { get; set; }
        public decimal? SUB_TOTAL_AFTER_TAX { get; set; }
        public decimal? SUB_TOTAL_BEFORE_TAX { get; set; }
        public decimal PURCHASE_PRICE_BEFORE_TAX { get; set; }
        public int TOTAL_QTY { get; set; }
        //public ObservableCollection<ItemModel> SelectedItem { get; set; }
        public ObservableCollection<POModel> SelectedItem { get; set; }
        public ObservableCollection<POModel> SelectedItem1 { get; set; }
       

    }
}