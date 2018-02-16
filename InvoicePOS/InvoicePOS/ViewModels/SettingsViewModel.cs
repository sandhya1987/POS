using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using InvoicePOS.Helpers;
using System.Windows.Controls;
using System.Windows.Input;
using InvoicePOS.Views;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Windows.Interop;

namespace InvoicePOS.ViewModels
{
    class SettingsViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        #region Command Binding



        private ICommand _InvoiceClick;
        public ICommand InvoiceClick
        {
            get
            {
                if (_InvoiceClick == null)
                {
                    _InvoiceClick = new DelegateCommand(Invoice_Click);
                }
                return _InvoiceClick;
            }
        }

        public void Invoice_Click()
        {
            InvoiceSettings invoiceSettingsView = new InvoiceSettings();
            invoiceSettingsView.Show();

        }


        private ICommand _CurrencyClick;
        public ICommand CurrencyClick
        {
            get
            {
                if (_CurrencyClick == null)
                {
                    _CurrencyClick = new DelegateCommand(Currency_Click);
                }
                return _CurrencyClick;
            }
        }

        public void Currency_Click()
        {
            CurrencySettings currencySettingsView = new CurrencySettings();
            currencySettingsView.Show();

        }


        private ICommand _PrinterClick;
        public ICommand PrinterClick
        {
            get
            {
                if (_PrinterClick == null)
                {
                    _PrinterClick = new DelegateCommand(Printer_Click);
                }
                return _PrinterClick;
            }
        }

        public void Printer_Click()
        {
            Views.PrinterSettings ps = new Views.PrinterSettings();
            ps.Show();
        }


        private ICommand _EmailClick;
        public ICommand EmailClick
        {
            get
            {
                if (_EmailClick == null)
                {
                    _EmailClick = new DelegateCommand(Email_Click);
                }
                return _EmailClick;
            }
        }

        public void Email_Click()
        {
            EmailSettingsManager _ESM = new EmailSettingsManager();
            _ESM.Show();
        }
        
        #endregion


        #region Interface Implementation
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

        private Stack<BackNavigationEventHandler> _backFunctions
           = new Stack<BackNavigationEventHandler>();

        void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
        {

        }

        void IModalService.GoBackward(bool dialogReturnValue)
        {
        }


        public static IModalService ModalService
        {
            get { return (IModalService)Application.Current.MainWindow; }
        }
        #endregion
    }

}
