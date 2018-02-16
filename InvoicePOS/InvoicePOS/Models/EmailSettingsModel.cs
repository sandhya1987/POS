using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;


namespace InvoicePOS.Models
{
    public class EmailSettingsModel : DependencyObject, INotifyPropertyChanged
    {
        public string BCC { get; set; }
        public string CC { get; set; }
        public string EMAIL { get; set; }
        public long ID { get; set; }
        public bool? IS_REQ_ENCRYPT_CONN { get; set; }
        public string MAIL_TYPE { get; set; }
        public string NAME { get; set; }
        public string PASSWORD { get; set; }
        public string SMTP_SERVER { get; set; }
        public string SMTP_SERVER_PORT { get; set; }
        public long USER_ID { get; set; }
        public string USER_NAME { get; set; }

        private bool _IS_GMAIL;
        public bool IS_GMAIL
        {
            get
            {
                return _IS_GMAIL;
            }
            set
            {
                this._IS_GMAIL = value;
                OnPropertyChanged("IS_GMAIL");
                NotifyPropertyChanged("ServerInformation");
            }
        }

        

        private string _ServerInformation;
        public string ServerInformation
        {
            get
            {
                if (IS_GMAIL)
                {
                    return "Collapsed";
                }
                return "Visible";
            }
            
        }

        #region interface implementation
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

        #endregion

    }
}
