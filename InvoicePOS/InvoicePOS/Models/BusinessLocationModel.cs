using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{


    public class AutoBussinessModel
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


    public class BusinessLocationModel : IDataErrorInfo
    {
        public long BUSINESS_LOCATION_ID { get; set; }

        public string BUSINESS_LOCATION { get; set; }
        public long COMPANY_ID { get; set; }
        public string COMPANY { get; set; }
        public string SHOP_NAME { get; set; }
        public string PREFIX_DOCUMENT { get; set; }
        public string DOCUMENT_NO { get; set; }
        public string TIN_NUMBER { get; set; }
        public string BUSS_ADDRESS_1 { get; set; }
        public string BUSS_ADDRESS_2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string COUNTRY { get; set; }
        public string PIN { get; set; }
        public string PHONE_NO { get; set; }
        public string MOBILE_NO { get; set; }
        public string EMAIL { get; set; }
        public string WEBSITE { get; set; }
        public string PRINTER_SETTING { get; set; }
        public string SMS_SETTING { get; set; }
        public string EMAIL_SETTING { get; set; }
        public string EMAIL_TEMPLATE_SETTING { get; set; }
        public string SCEOND_RECEIPT_PRINTER { get; set; }
        public string SCEOND_RECEIPT_PRINT_FORMATE { get; set; }
        public string NUMBER_OF_SECOND_RECEIPT_PRINT { get; set; }
        public string GODOWN_TO_KEEP { get; set; }
        public bool IS_ENABLE_EMAIL { get; set; }
        public bool IS_SCEOND_RECEIPT_PRINTER { get; set; }
        public bool IS_SECOND_DIFF_PRINT { get; set; }
        public bool IS_ASK_TO_PRIENT_EVERYTIME { get; set; }
        public bool IS_DELETE_INVOICE_SPECIFIED_GODOWN { get; set; }
        public int SLNO { get; set; }
        public string IMAGE { get; set; }



        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }

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

                if (columnName == "COMPANY" && string.IsNullOrWhiteSpace(COMPANY))
                {
                    error = "COMPANY Name is required!";

                }
                if (columnName == "SHOP_NAME" && string.IsNullOrWhiteSpace(SHOP_NAME))
                {
                    error = "SHOP_NAME is required!";

                }
                if (columnName == "TIN_NUMBER" && string.IsNullOrWhiteSpace(TIN_NUMBER))
                {
                    error = "TIN_NUMBER is required!";

                }
                if (columnName == "BUSS_ADDRESS_1" && string.IsNullOrWhiteSpace(BUSS_ADDRESS_1))
                {
                    error = "BUSS_ADDRESS_1 is required!";

                }
                if (columnName == "BUSS_ADDRESS_2" && string.IsNullOrWhiteSpace(BUSS_ADDRESS_2))
                {
                    error = "BUSS_ADDRESS_2 is required!";

                }
                if (columnName == "PIN" && string.IsNullOrWhiteSpace(PIN))
                {
                    error = "PIN is required!";

                }
                if (columnName == "PHONE_NO" && string.IsNullOrWhiteSpace(PHONE_NO))
                {
                    error = "PHONE_NO is required!";

                }
                if (columnName == "WEBSITE" && string.IsNullOrWhiteSpace(WEBSITE))
                {
                    error = "WEBSITE is required!";

                }
                if (columnName == "PRINTER_SETTING" && string.IsNullOrWhiteSpace(PRINTER_SETTING))
                {
                    error = "PRINTER_SETTING is required!";

                }
                if (columnName == "GODOWN_TO_KEEP" && string.IsNullOrWhiteSpace(GODOWN_TO_KEEP))
                {
                    error = "GODOWN_TO_KEEP is required!";

                }
                //switch (columnName)
                //{
                //    case "TAX_PAID": if ((TAX_PAID <= 0)) error = "Tax paid is required!"; break;
                //    case "SALES_PRICE": if ((SALES_PRICE <= 0)) error = "Sales Price can not be blank"; break;
                //    case "TAX_COLLECTED": if ((TAX_COLLECTED <= 0)) error = "Tax Collected can not be blank"; break;
                //    case "OPN_QNT": if ((OPN_QNT <= 0)) error = "Opening Qnt can not be blank"; break;
                //    case "MRP": if ((MRP <= 0)) error = "MRP can not be blank"; break;
                //};
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
