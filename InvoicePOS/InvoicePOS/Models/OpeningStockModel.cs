using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InvoicePOS.Models
{
    public class OpeningStockModel: DependencyObject, INotifyPropertyChanged
    {
        public long OPENING_STOCK_ID { get; set; }
        public DateTime DATE { get; set; }
        public long ITEM_ID { get; set; }
       public long BUSINESS_LOC_ID { get; set; }

        public string BUSINESS_LOC { get; set; }
        public long COMPANY_ID { get; set; }
        public decimal OPRNING_BAL { get; set; }
        public decimal CLOSING_BAL { get; set; }
        public string BARCODE { get; set; }

        public string SEARCH_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string GODOWN { get; set; }
        public string COMPANY_NAME { get; set; }
        public int OPN_QNT { get; set; }











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

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }


   
}
