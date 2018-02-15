using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace InvoicePOS.Models
{
    public class SalesModel
    {
    }
    public class SalesReturnModel : IDataErrorInfo
    {
        public long SALES_RETURN_ID { get; set; }
        public long COMPANY_ID { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public string SALES_RETURN_NO { get; set; }
        public string BARCODE { get; set; }
        public string ITEM_NAME { get; set; }
        public bool IS_WITHOUT_INVOICE { get; set; }
        public DateTime RETURN_DATE { get; set; }
        public string GODOWN { get; set; }
        public long CUSTOMER_ID { get; set; }
        public decimal OUTSTANDING_BALANCE { get; set; }
        public string INVOICE_NO { get; set; }
        public string REFERENCE_NUMBER { get; set; }
        public decimal TAX_AMOUNT { get; set; }
        public decimal FREIGHT_CHARGE { get; set; }
        public decimal PACKING_CHARGE { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public int ROUND_OFF_AMOUNT { get; set; }
        public decimal GRAND_TOTAL { get; set; }
        public decimal SUBSIDY_AMOUN { get; set; }
        public decimal CUS_PENDING_AMOUNT { get; set; }
        public string NOTES { get; set; }
        public string SEARCH_CODE { get; set; }
        public string SALES_EXECUTIVE { get; set; }
        public long BUSINESS_LOCATION_ID { get; set; }
        public long GODOWN_ID { get; set; }
        public string CUSTOMER { get; set; }
        public int? INVOICE_ID { get; set; }
       // public string INVOICE_NO { get; set; }
        public int SALE_ID { get; set; }
        public int SLNO { get; set; }
        public int? Current_Qty { get; set; }

        public decimal TAX_COLLECTED { get; set; }

        public decimal MRP { get; set; }
        public string SALES_UNIT { get; set; }
        public string PURCHASE_UNIT { get; set; }

        public decimal SALES_PRICE { get; set; }
        public int TOTAL_QTY { get; set; }
        public int TOTAL_ITEM { get; set; }
        public int SALE_QTY { get; set; }
        public ObservableCollection<SalesReturnModel> SelectedItem { get; set; }



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

                //if (columnName == "BARCODE" && string.IsNullOrWhiteSpace(BARCODE))
                //{
                //    error = "BARCODE is required!";

                //}
                //if (columnName == "GODOWN" && string.IsNullOrWhiteSpace(GODOWN))
                //{
                //    error = "Godown is required!";

                //}
                //if (columnName == "INVOICE_NO" && string.IsNullOrWhiteSpace(INVOICE_NO))
                //{
                //    error = "Godown is required!";

                //}
                //if (columnName == "SALES_EXECUTIVE" && string.IsNullOrWhiteSpace(SALES_EXECUTIVE))
                //{
                //    error = "Godown is required!";

                //}

                switch (columnName)
                {
                    //case "FREIGHT_CHARGE": if ((FREIGHT_CHARGE <= 0)) error = "FREIGHT CHARGE is required!"; break;
                    //case "PACKING_CHARGE": if ((PACKING_CHARGE <= 0)) error = "PACKING CHARGE can not be blank"; break;
                    //case "CUS_PENDING_AMOUNT": if ((CUS_PENDING_AMOUNT <= 0)) error = "CUS PENDING_AMOUNT can not be blank"; break;
                    //case "GRAND_TOTAL": if ((GRAND_TOTAL <= 0)) error = "GRAND_TOTAL can not be blank"; break;
                    //case "ROUND_OFF_AMOUNT": if ((ROUND_OFF_AMOUNT <= 0)) error = "ROUND OFF AMOUNT can not be blank"; break;
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
