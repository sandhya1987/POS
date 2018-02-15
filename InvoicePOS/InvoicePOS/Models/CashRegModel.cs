using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class CashRegModel : IDataErrorInfo
    {
        public long CASH_REGISTERID { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public long CASH_REG_NO { get; set; }
        public string CASH_REG_NAME { get; set; }
        public string CASH_REG_PREFIX { get; set; }
        public bool ISADGUSTMENT { get; set; }
        public string LOGIN { get; set; }
        public bool IS_MAIN_CASH { get; set; }
        public long COMPANY_ID { get; set; }
        public int SLNO { get; set; }
        public decimal? CASH_AMOUNT { get; set; }
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

                if (columnName == "BUSINESS_LOCATION" && string.IsNullOrWhiteSpace(BUSINESS_LOCATION))
                {
                    error = "Godown Name is required!";

                }
                if (columnName == "CASH_REG_NAME" && string.IsNullOrWhiteSpace(CASH_REG_NAME))
                {
                    error = "Godown Description is required!";

                }
                if (columnName == "CASH_REG_PREFIX" && string.IsNullOrWhiteSpace(CASH_REG_PREFIX))
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
