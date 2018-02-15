using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class SearchCodeAutoListModel
    {
        private string[] keywordStrings;
        private string displayString;
        public string DisplayName
        {
            get { return displayString; }
            set { displayString = value; }
        }

        private long displayId;
        public long DisplayId
        {
            get { return displayId; }
            set { displayId = value; }
        }

        public string[] KeywordStrings
        {
            get
            {
                if (keywordStrings == null)
                {
                    keywordStrings = new string[] { displayString };
                }
                return keywordStrings;
            }
        }
        public override string ToString()
        {
            return displayString;
        }
        // public long CATAGORY_ID { get; set; }
    }


    public class ItemNameAutoListModel
    {
        private string[] keywordStrings;
        private string displayString;
        public string DisplayName
        {
            get { return displayString; }
            set { displayString = value; }
        }

        private long displayId;
        public long DisplayId
        {
            get { return displayId; }
            set { displayId = value; }
        }

        public string[] KeywordStrings
        {
            get
            {
                if (keywordStrings == null)
                {
                    keywordStrings = new string[] { displayString };
                }
                return keywordStrings;
            }
        }
        public override string ToString()
        {
            return displayString;
        }
        // public long CATAGORY_ID { get; set; }
    }







    public class ItemModel : IDataErrorInfo
    {
        //  public int ITEM_ID1 { get; set; }
        public int? ITEM_ID1 { get; set; }
        public int ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_DESCRIPTION { get; set; }
        public string SALES_RETURN_NO { get; set; }
        public DateTime FORM_DATE { get; set; }
        public DateTime TO_DATE { get; set; }

        public string CUSTOMER_NAME { get; set; }
        public string CUSTOMER_EMAIL { get; set; }
        public string CUSTOMER_MOBILE { get; set; }
        public decimal TOTAL_SUM { get; set; }
        public int TOTAL_QTY { get; set; }
        public decimal EFFECTIVE_RATE_PER_UNIT { get; set; }
        public DateTime ESTIMATE_DATE { get; set; }
        
        //public string INVOICE_NO { get; set; }
        public string SUPPLIER_INVOICE_NO { get; set; }
        public string PAYMENT_STATUS { get; set; }
        
        public double ITEM_PRICE { get; set; }
        public int ITEM_INVOICE_ID { get; set; }
        public int ITEM_PRODUCT_ID { get; set; }
        public string KEYWORD { get; set; }
        public string ACCESSORIES_KEYWORD { get; set; }
        public string BARCODE { get; set; }
        public long CATAGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public int? OPN_QNT { get; set; }
        public int? Current_Qty { get; set; }
        public string SEARCH_CODE { get; set; }
        public decimal TAX_PAID { get; set; }
        public decimal TAX_COLLECTED { get; set; }
        public string SORT_INDEX1 { get; set; }

        public string TAX_PAID_NAME { get; set; }
        public string TAX_COLLECTED_NAME { get; set; }

        public string ITEM_LOCATION_NAME { get; set; }
        public string ITEM_LOCATION_NAME1 { get; set; }
        public string SORT_INDEX { get; set; }
        public long LOCATION_ID { get; set; }

        public string SelectPaidTax { get; set; }
        public string SelectCollectTax { get; set; }

        public string PURCHASE_UNIT { get; set; }
        public string SALES_UNIT { get; set; }
        public decimal PURCHASE_UNIT_PRICE { get; set; }
        public decimal SALES_PRICE { get; set; }
        public decimal MRP { get; set; }
        public long COMPANY_ID { get; set; }
        public string DISPLAY_INDEX { get; set; }
        public string REGIONAL_LANGUAGE { get; set; }
        public string ITEM_UNIQUE_NAME { get; set; }
        public string ITEM_GROUP_NAME { get; set; }
        public decimal SALES_PRICE_BEFOR_TAX { get; set; }
        public decimal SALES_PRICE_BEFOR_TAX_QTY { get; set; }
        public DateTime MODIFICATION_DATE { get; set; }
        public string IMAGE_PATH { get; set; }
        public string SEARCH_ITEM { get; set; }
        public bool IS_Shortable_Item { get; set; }
        public bool INCLUDE_TAX { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_Not_For_Sell { get; set; }
        public bool IS_Purchased { get; set; }
        public bool IS_Service_Item { get; set; }
        public bool IS_Gift_Card { get; set; }
        public bool IS_For_Online_Shop { get; set; }
        public bool IS_Not_for_online_shop { get; set; }
        public bool ALLOW_PURCHASE_ON_ESHOP { get; set; }
        public int SLNO { get; set; }

        public string RECEIVE_ITEM_ENTRY_NO { get; set; }
        public DateTime RECEIVE_ITEM_ENTRY_DATE { get; set; }
        public DateTime ITEM_RECEIVE_DATE { get; set; }
        public bool PAYMENT_BY_CASH { get; set; }

        public string ITEM_LOCATION { get; set; }
        //public string ITEM_LOCATION_NAME { get; set; }
        //public string SORT_INDEX { get; set; }
        public decimal WeightPrice { get; set; }
        public int WeightQnt { get; set; }


        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal SALESPRICE { get; set; }

        public decimal NETAMT { get; set; }
        public decimal GROSSAMT { get; set; }
        public int? QUNT_TOTAL { get; set; }
        public int TOTAL_ITEM { get; set; }

        public decimal WEIGHT_OF_PAPER { get; set; }
        public decimal WEIGHT_OF_PLASTIC { get; set; }
        public decimal WEIGHT_OF_FOAM { get; set; }
        public decimal WEIGHT_OF_CARDBOARD { get; set; }
        public long BUSINESS_LOCATION_ID { get; set; }



        public long GODOWN_ID { get; set; }
        public int? INVOICE_ID { get; set; }
        public long OPENING_STOCK_ID { get; set; }
        public DateTime DATE { get; set; }
        public long BUSINESS_LOC_ID { get; set; }

        public string BUSINESS_LOC { get; set; }
        public decimal OPRNING_BAL { get; set; }
        public decimal CLOSING_BAL { get; set; }


        public string INVOICE_NO { get; set; }
        public DateTime INVOICE_DATE { get; set; }
        public string GODOWN { get; set; }
        public string COMPANY_NAME { get; set; }

        public string PO_NUMBER { get; set; }
        public int TAX_PAID_ID { get; set; }
        public int TAX_COLLECTED_ID { get; set; }

        public int BUSS_LOC_ID { get; set; }
        //public int GODOWN_ID { get; set; }

        public int UNIT_PURCHAGE_ID { get; set; }
        public int UNIT_SALES_ID { get; set; }

        public decimal? TaxValue { get; set; }
        public string TaxName { get; set; }
        public decimal? TotalTax { get; set; }
        public string COMPANY { get; set; }
        public decimal RETURNABLE_AMOUNT { get; set; }
        public int SALE_ID { get; set; }
        public int SALE_QTY { get; set; }
        //public int Current_Qty { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public ObservableCollection<ItemModel> getAllSTock = new ObservableCollection<ItemModel>();
        public decimal? SalePriceWithDiscount { get; set; }
        private string error = string.Empty;
        public string Error
        {
            get { return error; }
            set { error = value; }
        }






        public string this[string columnName]
        {
            get
            {

                error = string.Empty;

                if (columnName == "SEARCH_CODE" && string.IsNullOrWhiteSpace(SEARCH_CODE))
                {
                    error = "SEARCH_CODE is required!";

                }
                if (columnName == "CATEGORY_NAME" && string.IsNullOrWhiteSpace(CATEGORY_NAME))
                {
                    error = "CATEGORY_NAME is required!";

                }
                if (columnName == "BARCODE" && string.IsNullOrWhiteSpace(BARCODE))
                {
                    error = "BARCODE is required!";

                }
                if (columnName == "ITEM_NAME" && string.IsNullOrWhiteSpace(ITEM_NAME))
                {
                    error = "Item Name is required!";

                }

                if (columnName == "PURCHASE_UNIT" && string.IsNullOrWhiteSpace(PURCHASE_UNIT))
                {
                    error = "PURCHASE_UNIT is required!";

                }
                if (columnName == "SALES_UNIT" && string.IsNullOrWhiteSpace(SALES_UNIT))
                {
                    error = "SALES_UNIT is required!";

                }

                if (columnName == "SelectPaidTax" && string.IsNullOrWhiteSpace(SelectPaidTax))
                {
                    error = "PaidTax is required!";

                }
                if (columnName == "SelectCollectTax" && string.IsNullOrWhiteSpace(SelectCollectTax))
                {
                    error = "CollectTax is required!";

                }




                switch (columnName)
                {
                    case "TAX_PAID": if ((TAX_PAID <= 0)) error = "Tax paid is required!"; break;
                    case "SALES_PRICE": if ((SALES_PRICE <= 0)) error = "Sales Price can not be blank"; break;
                    case "TAX_COLLECTED": if ((TAX_COLLECTED <= 0)) error = "Tax Collected can not be blank"; break;
                    case "OPN_QNT": if ((OPN_QNT <= 0)) error = "Opening Qnt can not be blank"; break;
                    case "MRP": if ((MRP <= 0)) error = "MRP can not be blank"; break;
                };
                return error;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class ItemLocationListModel
    {
        public List<ItemListModel> _ItemList { get; set; }
    }

    public class ItemListModel
    {
        public int? ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
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
        public string BUSINESS_LOCATION_NAME { get; set; }

    }
    public class ManageLocation
    {
        public int SLNO { get; set; }
        public string ITEM_LOCATION_NAME1 { get; set; }
        public string SORT_INDEX { get; set; }
        public string SORT_INDEX1 { get; set; }
        public long LOCATION_ID { get; set; }
    }

    //public class POrderModel
    //{
    //    public int? ITEM_ID { get; set; }
    //    //public string ITEM_NAME { get; set; }
    //    public string ITEM_DESCRIPTION { get; set; }
    //    public double? ITEM_PRICE { get; set; }

    //    public string KEYWORD { get; set; }
    //    public string ACCESSORIES_KEYWORD { get; set; }
    //    public string BARCODE { get; set; }

    //    public string CATEGORY_NAME { get; set; }
    //    public int? OPN_QNT { get; set; }
    //    public string SERCH_CODE { get; set; }
    //    public decimal? TAX_PAID { get; set; }
    //    public decimal? TAX_COLLECTED { get; set; }
    //    public string PURCHASE_UNIT { get; set; }
    //    public string SALES_UNIT { get; set; }
    //    public decimal? PURCHASE_UNIT_PRICE { get; set; }
    //    public decimal? SALES_PRICE { get; set; }
    //    public decimal? MRP { get; set; }
    //    public string BUSINESS_LOCATION_NAME { get; set; }
    //    public decimal? DISCOUNT { get; set; }
    //}

    public class ReceiveItem : IDataErrorInfo
    {
        public long COMPANY_ID { get; set; }
        public int NEWRECEIVE_ID { get; set; }
        public long? SELECT_BUSINESS_LOCATION_ID { get; set; }
        public string RECEIVED_ITEM_ENTRY_NO { get; set; }
        public string BARCODE { get; set; }
        public string FILTER_HIERARCHY { get; set; }
        public string ITEM_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public string SUPPLIER_INVOICE_NO { get; set; }
        public DateTime SUPPLIER_INVOICE_DATE { get; set; }
        public DateTime RECEIVE_DATE { get; set; }
        public DateTime RECEIVED_ITEM_ON_DATE { get; set; }
        public long PO_NUMBER_ID { get; set; }
        public long SUPPLIER_ID { get; set; }
        public string PAYMENT_TERMS { get; set; }
        public long GODOWN_ID { get; set; }
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
        public bool IS_REC_WITH_PO { get; set; }
        public decimal INVOICE_DIS_AMT { get; set; }

        public string GODOWN { get; set; }
        public string PO_NUMBER { get; set; }
        public string SUPPLIER { get; set; }
        public string SELECT_BUSINESS_LOCATION { get; set; }

        public bool DISCOUNT_BEFOR_TAX { get; set; }
        public bool PAY_IN_CASH { get; set; }
        public bool APPLY_FPR_EFFECTVE_PRICE { get; set; }
        public bool AUTO_SAVE_IN_DRAFT { get; set; }
        public bool FOCUS_LAST_ADD_ITEM { get; set; }
        public bool ITEM_ENTRY_TEMPLATE { get; set; }
        public int SLNO { get; set; }
        public decimal PURCHASE_UNIT_PRICE { get; set; }
        public decimal MRP { get; set; }
        public decimal TAX_PAID { get; set; }
        public int? TOTAL_QTY { get; set; }
        public decimal? SUB_TOTAL_AFTER_TAX { get; set; }
        public decimal? SUB_TOTAL_BEFORE_TAX { get; set; }
        public decimal PURCHASE_PRICE_BEFORE_TAX { get; set; }
        public decimal SALES_PRICE { get; set; }
        public int? TOTPO_QNT { get; set; }
        public string PURCHASE_UNIT { get; set; }
        public string TaxName { get; set; }
        public decimal TOTAL_TAX { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public string TERMS { get; set; }
        public int Current_Qty { get; set; }
        public ObservableCollection<ReceiveItem> SelectedItem { get; set; }


        private string error = string.Empty;
        public string Error
        {
            get { return error; }
            set { error = value; }
        }

        public string this[string columnName]
        {
            get
            {

                error = string.Empty;

                if (columnName == "BARCODE" && string.IsNullOrWhiteSpace(BARCODE))
                {
                    error = "BarCode Name is required!";

                }
                if (columnName == "FILTER_HIERARCHY" && string.IsNullOrWhiteSpace(FILTER_HIERARCHY))
                {
                    error = "FILTER HIERARCHY is required!";

                }
                if (columnName == "SUPPLIER" && string.IsNullOrWhiteSpace(SUPPLIER))
                {
                    error = "FILTER HIERARCHY is required!";

                }
                if (columnName == "GODOWN" && string.IsNullOrWhiteSpace(GODOWN))
                {
                    error = "GODOWN is required!";

                }
                if (columnName == "RECEIVING_EMPLOYEE" && string.IsNullOrWhiteSpace(RECEIVING_EMPLOYEE))
                {
                    error = "RECEIVING EMPLOYEE is required!";

                }
                switch (columnName)
                {
                    // case "PAYMENT_TERMS": if ((PAYMENT_TERMS <= 0)) error = "PAYMENT TERMS is required!"; break;
                    case "DISCOUNT_FLAT": if ((DISCOUNT_FLAT <= 0)) error = "DISCOUNT FLAT can not be blank"; break;
                    case "DISCOUNT_PERCENT": if ((DISCOUNT_PERCENT <= 0)) error = "Tax Collected can not be blank"; break;
                    case "SUB_TOTAL_BEFORETAX": if ((SUB_TOTAL_BEFORETAX <= 0)) error = "SUB TOTAL BEFORETAX can not be blank"; break;
                    case "SUB_TOTAL": if ((SUB_TOTAL <= 0)) error = "SUB TOTAL can not be blank"; break;
                };
                return error;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }





    }
}
