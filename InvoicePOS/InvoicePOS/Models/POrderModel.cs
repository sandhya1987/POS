using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class POrderModel: INotifyPropertyChanged, IDataErrorInfo
    {
        public int ITEM_ID { get; set; }
        public decimal Discount { get; set; }
        public decimal? Total { get; set; }
        public int Current_Qty { get; set; }
        public decimal? TotalTax { get; set; }
        public long PO_ID { get; set; }
        public int PO_NUMBER { get; set; }
        public string PO_NUMBER1 { get; set; }
        public string PO_STATUS { get; set; }
        public string PO_TYPE { get; set; }
        public DateTime DELIVERY_DATE { get; set; }
        public bool IS_SEND_MAIL { get; set; }
        public string BAR_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public long COMPANY_ID { get; set; }
        public string DELIVER { get; set; }
        public long DELIVER_ID { get; set; }
        public long SUPPLIER_ID { get; set; }
        public string SUPPLIER { get; set; }
        public long BUSINESS_LOCATION_ID { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public string DELIVER_TO { get; set; }
        public string SUPPLIER_EMAIL { get; set; }
        public string TERMS { get; set; }
        public decimal TOTAL_TAX { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public DateTime PO_DATE { get; set; }
        public int SLNO { get; set; }
        public string SearchStock { get; set; }
        public decimal PURCHASE_UNIT_PRICE { get; set; }
        public decimal TAX_PAID { get; set; }
        public decimal TAX_COLLECTED { get; set; }
        public decimal MRP { get; set; }
        public decimal? SUB_TOTAL_AFTER_TAX { get; set; }
        public decimal? SUB_TOTAL_BEFORE_TAX { get; set; }
        public decimal PURCHASE_PRICE_BEFORE_TAX { get; set; }
        public int? TOTAL_QTY { get; set; }
        public string TaxName { get; set; }
        public decimal? TaxValue { get; set; }

        //public ObservableCollection<ItemModel> SelectedItem { get; set; }

        public ObservableCollection<POrderModel> SelectedItem { get; set; }
        public ObservableCollection<POrderModel> SelectedItem1 { get; set; }

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
                if (columnName == "BAR_CODE" && string.IsNullOrWhiteSpace(BAR_CODE))
                {
                    error = "BAR_CODE is required!";

                }
                if (columnName == "ITEM_NAME" && string.IsNullOrWhiteSpace(ITEM_NAME))
                {
                    error = "ITEM_NAME is required!";

                }
                if (columnName == "SEARCH_CODE" && string.IsNullOrWhiteSpace(SEARCH_CODE))
                {
                    error = "SEARCH_CODE is required!";

                }
                if (columnName == "SUPPLIER" && string.IsNullOrWhiteSpace(SUPPLIER))
                {
                    error = "SUPPLIER is required!";

                }
                if (columnName == "SUPPLIER_EMAIL" && string.IsNullOrWhiteSpace(SUPPLIER_EMAIL))
                {
                    error = "SUPPLIER_EMAIL is required!";

                }
                if (columnName == "DELIVER_TO" && string.IsNullOrWhiteSpace(DELIVER_TO))
                {
                    error = "DELIVER_TO is required!";

                }
                
                if (columnName == "TOTAL_AMOUNT" && string.IsNullOrWhiteSpace(Convert.ToString(TOTAL_AMOUNT)))
                {
                    error = "TOTAL_AMOUNT is required!";

                }
                if (columnName == "MRP" && string.IsNullOrWhiteSpace(Convert.ToString(MRP)))
                {
                    error = "MRP is required!";

                }
               



                return error;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }


    public class GetPoItems
    {
        public int ITEM_ID { get; set; }
        public decimal Discount { get; set; }
        public decimal? Total { get; set; }
        public int? Current_Qty { get; set; }
        public decimal? TotalTax { get; set; }
        public long PO_ID { get; set; }
        public string PO_NUMBER { get; set; }
        public DateTime DELIVERY_DATE { get; set; }
        public bool IS_SEND_MAIL { get; set; }
        public string BAR_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public long COMPANY_ID { get; set; }
        public string DELIVER { get; set; }
        public long DELIVER_ID { get; set; }
        public long SUPPLIER_ID { get; set; }
        public string SUPPLIER { get; set; }
        public long BUSINESS_LOCATION_ID { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public string DELIVER_TO { get; set; }
        public string SUPPLIER_EMAIL { get; set; }
        public string TERMS { get; set; }
        public decimal TOTAL_TAX { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public DateTime PO_DATE { get; set; }
        public int SLNO { get; set; }
        public string SearchStock { get; set; }
        public decimal PURCHASE_UNIT_PRICE { get; set; }
        public decimal TAX_PAID { get; set; }
        public decimal TAX_COLLECTED { get; set; }
        public decimal MRP { get; set; }
        public decimal SUB_TOTAL_AFTER_TAX { get; set; }
        public decimal SUB_TOTAL_BEFORE_TAX { get; set; }
        public decimal PURCHASE_PRICE_BEFORE_TAX { get; set; }
        public int? TOTAL_QTY { get; set; }
        public string TaxName { get; set; }
        public decimal? TaxValue { get; set; }
    }


}
