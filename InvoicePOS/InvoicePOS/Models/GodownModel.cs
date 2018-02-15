using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class AutoGodownModel
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



    public class GodownModel : IDataErrorInfo
    {
        public long GODOWN_ID { get; set; }
        public string GODOWN_NAME { get; set; }
        public string BUSINESS_NAME { get; set; }
        public string STOCK_CORRECTION { get; set; }
        public string GOSOWN_DESCRIPTION { get; set; }
        public bool IS_ACTIVE { get; set; }
        public long COMPANY_ID { get; set; }
        public bool IS_DELETE { get; set; }
        public string SEARCH_GODOWN { get; set; }
        public bool IS_DEFAULT_GODOWN { get; set; }
        public int SLNO { get; set; }
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

                if (columnName == "GODOWN_NAME" && string.IsNullOrWhiteSpace(GODOWN_NAME))
                {
                    error = "Godown Name is required!";

                }
                if (columnName == "GOSOWN_DESCRIPTION" && string.IsNullOrWhiteSpace(GOSOWN_DESCRIPTION))
                {
                    error = "Godown Description is required!";

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
