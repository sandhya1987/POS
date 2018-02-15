using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class StockTransferModel : IDataErrorInfo
    {
        private Regex numerciRegex = new Regex("[^0-9.-]+");
        private Regex emailRegex = new Regex(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$");

        public long STOCK_TRANSFER_ID { get; set; }
        public string STOCK_TRANSFER_NUMBER { get; set; }
        public DateTime DATE { get; set; }
        public string EMAIL { get; set; }
        public bool IS_SEND { get; set; }
        public long ITEM_ID { get; set; }
        public bool IS_NEGATIVE_STOCK_MESSAGE { get; set; }
        public int TRANS_QUANTITY { get; set; }
        public int tnsquantity { get; set; }
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
        public int? OPN_QNT { get; set; }
        public string PURCHASE_UNIT { get; set; }
        public int WeightQnt { get; set; }
        public int SLNO { get; set; }
        private string error = string.Empty;
        public string Error
        {
            get { return error; }
            set { error = value; }
        }
        public ObservableCollection<StockTransferModel> getAllStockTransfer = new ObservableCollection<StockTransferModel>();





        public string this[string columnName]
        {
            get
            {

                error = string.Empty;

                if (columnName == "BARCODE" && string.IsNullOrWhiteSpace(BARCODE))
                {
                    error = "Godown Name is required!";

                }
                if (columnName == "FROM_GODOWN" && string.IsNullOrWhiteSpace(FROM_GODOWN))
                {
                    error = "Godown Description is required!";

                }
                if (columnName == "RECEIVED_BY" && string.IsNullOrWhiteSpace(RECEIVED_BY))
                {
                    error = "Godown Description is required!";

                }
                if (columnName == "TO_GODOWN" && string.IsNullOrWhiteSpace(TO_GODOWN))
                {
                    error = "Godown Description is required!";

                }
                switch (columnName)
                {
                    case "TOTAL_STOCK_QNT": if ((TOTAL_STOCK_QNT <= 0)) error = "Tax paid is required!"; break;
                        //case "SALES_PRICE": if ((SALES_PRICE <= 0)) error = "Sales Price can not be blank"; break;
                        //case "TAX_COLLECTED": if ((TAX_COLLECTED <= 0)) error = "Tax Collected can not be blank"; break;
                        //case "OPN_QNT": if ((OPN_QNT <= 0)) error = "Opening Qnt can not be blank"; break;
                        //case "MRP": if ((MRP <= 0)) error = "MRP can not be blank"; break;
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
        public int SLNO { get; set; }
    }
}
