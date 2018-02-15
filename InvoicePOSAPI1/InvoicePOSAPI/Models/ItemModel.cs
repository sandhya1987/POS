using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class ItemModel
    {
        public int? ITEM_ID1 { get; set; }
        public int? ITEM_ID { get; set; }
        public int SLNO { get; set; }       
        public string CUSTOMER_NAME { get; set; }
        public string CUSTOMER_MOBILE { get; set; }
        public string CUSTOMER_EMAIL { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_DESCRIPTION { get; set; }
        public string IMAGE_PATH { get; set; }
        public double? ITEM_PRICE { get; set; }
        public int? ITEM_INVOICE_ID { get; set; }
        public int? ITEM_PRODUCT_ID { get; set; }
        public string KEYWORD { get; set; }
        public string ACCESSORIES_KEYWORD { get; set; }
        public string BARCODE { get; set; }
        public long? CATAGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public int? OPN_QNT { get; set; }
        public string ITEM_LOCATION { get; set; }
        public string SEARCH_CODE { get; set; }
        public decimal? TAX_PAID { get; set; }
        public decimal? TAX_COLLECTED { get; set; }
        public string PURCHASE_UNIT { get; set; }
        public string SALES_UNIT { get; set; }
        public decimal? PURCHASE_UNIT_PRICE { get; set; }
        public decimal? SALES_PRICE { get; set; }
        public decimal? MRP { get; set; }
        public int? COMPANY_ID { get; set; }
        public bool? INCLUDE_TAX { get; set; }
        public string DISPLAY_INDEX { get; set; }
        public string REGIONAL_LANGUAGE { get; set; }
        public string ITEM_UNIQUE_NAME { get; set; }
        public string ITEM_GROUP_NAME { get; set; }
        public decimal? SALES_PRICE_BEFOR_TAX { get; set; }
        public DateTime? MODIFICATION_DATE { get; set; }
        public bool? IS_ACTIVE { get; set; }
        public bool? IS_Not_For_Sell { get; set; }
        public bool? IS_Purchased { get; set; }
        public bool? IS_Service_Item { get; set; }
        public bool? IS_Gift_Card { get; set; }
        public bool? IS_For_Online_Shop { get; set; }
        public bool? IS_Not_for_online_shop { get; set; }
        public bool? ALLOW_PURCHASE_ON_ESHOP { get; set; }
        public bool? IS_Shortable_Item { get; set; }
        public bool? IS_DELETE { get; set; }
        public decimal WeightPrice { get; set; }
        public int WeightQnt { get; set; }
        public string SORT_INDEX { get; set; }
        public string ITEM_LOCATION_NAME { get; set; }
        public string ITEM_LOCATION_NAME1 { get; set; }
        public long LOCATION_ID { get; set; }
        public long? INVOICE_ID { get; set; }
        public decimal? WEIGHT_OF_PAPER { get; set; }
        public decimal? WEIGHT_OF_PLASTIC { get; set; }
        public decimal? WEIGHT_OF_FOAM { get; set; }
        public decimal? WEIGHT_OF_CARDBOARD { get; set; }
        //public string ITEM_LOCATION_NAME { get; set; }
        //public string SORT_INDEX { get; set; }
        public string RECEIVE_ITEM_ENTRY_NO { get; set; }
        public DateTime RECEIVE_ITEM_ENTRY_DATE { get; set; }
        public DateTime ITEM_RECEIVE_DATE { get; set; }
        public string  SALES_RETURN_NO { get; set; }
        public DateTime ESTIMATE_DATE { get; set; }

        public string SUPPLIER_INVOICE_NO { get; set; }
        public long BUSINESS_LOCATION_ID { get; set; }

        public long OPENING_STOCK_ID { get; set; }
        public string BUSINESS_LOC { get; set; }
        public decimal OPRNING_BAL { get; set; }
        public decimal CLOSING_BAL { get; set; }
        public string GODOWN { get; set; }
        public DateTime? DATE { get; set; }
        public string COMPANY_NAME { get; set; }
        public long USER_ID { get; set; }

        public int TAX_PAID_ID { get; set; }
        public int TAX_COLLECTED_ID { get; set; }

        public int? BUSS_LOC_ID { get; set; }
        public int? GODOWN_ID { get; set; }

        public int? UNIT_PURCHAGE_ID { get; set; }
        public int? UNIT_SALES_ID { get; set; }

        public string TAX_PAID_NAME { get; set; }
        public string TAX_COLLECTED_NAME { get; set; }

        public decimal Discount { get; set; }
        public decimal? TotalTax { get; set; }
        public decimal? Total { get; set; }
        public int? Current_Qty { get; set; }
        public string Invoice_No { get; set; }
        public string Payment_Status { get; set; }
        public DateTime Invoice_Date { get; set; }

        public decimal? TaxValue { get; set; }
        public string TaxName { get; set; }
        public decimal RETURNABLE_AMOUNT { get; set; }
        public int SALE_ID { get; set; }
        public int? TOTPO_QNT { get; set; }
        public string PO_NUMBER { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public string TERMS { get; set; }
    }

    public class ItemListModel
    {
        public int? ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_LOCATION { get; set; }
        public string ITEM_DESCRIPTION { get; set; }
        public double? ITEM_PRICE { get; set; }
        
        public string KEYWORD { get; set; }
        public string ACCESSORIES_KEYWORD { get; set; }
        public string BARCODE { get; set; }
        
        public string CATEGORY_NAME { get; set; }
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

    public class ReceiveItem : ItemModel
    {
        public long? COMPANY_ID { get; set; }
        public int NEWRECEIVE_ID { get; set; }
        public long? SELECT_BUSINESS_LOCATION_ID { get; set; }
        public string RECEIVED_ITEM_ENTRY_NO { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public string SUPPLIER_INVOICE_NO { get; set; }
        public DateTime? SUPPLIER_INVOICE_DATE { get; set; }
        public DateTime? RECEIVE_DATE { get; set; }
        public string FILTER_HIERARCHY { get; set; }
        public DateTime? RECEIVED_ITEM_ON_DATE { get; set; }
        public long? PO_NUMBER_ID { get; set; }
        public long? SUPPLIER_ID { get; set; }
        public int? PAYMENT_TERMS { get; set; }
        public long? GODOWN_ID { get; set; }
        public string RECEIVING_EMPLOYEE { get; set; }
        public decimal? DISCOUNT_FLAT { get; set; }
        public decimal? DISCOUNT_PERCENT { get; set; }
        public decimal? ADDTIONAL_CHARGES { get; set; }
        public decimal? SUB_TOTAL_BEFORETAX { get; set; }
        public decimal? TOTAL_TAX_AMT { get; set; }
        public decimal? SUB_TOTAL { get; set; }
        public decimal? ROUND_OFF_ADJUSTMENTAMT { get; set; }
        public decimal? TOTAL_AMT { get; set; }
        public decimal? USE_SUPPLIER_ADVANCE_AMT { get; set; }
        public decimal? GRAND_TOTAL { get; set; }
        public string NOTE { get; set; }
        public bool? IS_REC_WITH_PO { get; set; }
        public string GODOWN { get; set; }
        public string PO_NUMBER { get; set; }
        public string SUPPLIER { get; set; }
        public string SELECT_BUSINESS_LOCATION { get; set; }
        public bool? DISCOUNT_BEFOR_TAX { get; set; }
        public bool? PAY_IN_CASH { get; set; }
        public bool? APPLY_FPR_EFFECTVE_PRICE { get; set; }
        public bool? AUTO_SAVE_IN_DRAFT { get; set; }
        public bool? FOCUS_LAST_ADD_ITEM { get; set; }
        public bool? ITEM_ENTRY_TEMPLATE { get; set; }
        public decimal? INVOICE_DIS_AMT { get; set; }
        public int? OPN_QNT { get; set; }
        public string SERCH_CODE { get; set; }
        public decimal? TAX_PAID { get; set; }
        public decimal? TAX_COLLECTED { get; set; }
        public string PURCHASE_UNIT { get; set; }
        public string SALES_UNIT { get; set; }
        public decimal? PURCHASE_UNIT_PRICE { get; set; }
        public decimal? SALES_PRICE { get; set; }
        public decimal? MRP { get; set; }
        public int? TOTPO_QNT { get; set; }
        public string TERMS { get; set; }
        public int? TOTAL_QTY { get; set; }
        public decimal? SUB_TOTAL_AFTER_TAX { get; set; }
        public decimal? SUB_TOTAL_BEFORE_TAX { get; set; }
        public ObservableCollection<ReceiveItem> SelectedItem { get; set; }

    }

    public class ManageLocation
    {
        public string ITEM_LOCATION_NAME1{ get; set; }
        public string ITEM_LOCATION_NAME { get; set; }

        public string SORT_INDEX { get; set; }
        public string SORT_INDEX1 { get; set; }
        public long LOCATION_ID { get; set; }
    }
       
}