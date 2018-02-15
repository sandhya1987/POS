using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class StocktransferModel
    {

        public long STOCK_TRANSFER_ID { get; set; }
        public string STOCK_TRANSFER_NUMBER { get; set; }
        public DateTime? DATE { get; set; }
        public string EMAIL { get; set; }
        public bool? IS_SEND { get; set; }
        public bool? IS_NEGATIVE_STOCK_MESSAGE { get; set; }
        public long? ITEM_ID { get; set; }
        public int? TRANS_QUANTITY { get; set; }
        public string SEARCH_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string BARCODE { get; set; }
        public int? TOTAL_STOCK_QNT { get; set; }
        public long? COMPANY_ID { get; set; }
        public long? FROM_GODOWN_ID { get; set; }
        public long? TO_GODOWN_ID { get; set; }
        public long? RECEIVED_BY_ID { get; set; }
        public long? BUSINESS_LOCATION_ID { get; set; }
        public string FROM_GODOWN { get; set; }
        public string TO_GODOWN { get; set; }
        public string RECEIVED_BY { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public int CHNG_QNT { get; set; }
        public int? OPN_QNT { get; set; }
        public string PURCHASE_UNIT { get; set; }
        public int WeightQnt { get; set; }
        public ObservableCollection<StocktransferModel> getAllStockTransfer { get; set; }
    }
    public class ManageStockModel
    {
        public long COMPANY_ID { get; set; }
        public long BUSSINESS_LOCATION_ID { get; set; }
        public string BUSSINESS_LOCATION { get; set; }
        public long GODOWN_NAME_ID { get; set; }
        public long GODOWN_ID { get; set; }
        public string GODOWN_NAME { get; set; }
        public bool? IS_ACTIVE { get; set; }
        public bool? IS_DEFAULT_GODOWN { get; set; }
        public string STOCK_CORRECTION { get; set; }
        public string DESCRIPTION { get; set; }

        public List<ItemModel> _itemModel { get; set; }

    }
}