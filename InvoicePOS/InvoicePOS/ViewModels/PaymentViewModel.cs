using InvoicePOS.Helpers;
using InvoicePOS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InvoicePOS.ViewModels
{
    public class PaymentViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        private readonly  Payment PaymentModel;
        Payment _ItemModel = new Payment();
        private Payment _selectedItem;
        Payment[] data = null;

        ObservableCollection<Payment> _ListGrid_Temp = new ObservableCollection<Payment>();
       


        public Payment SelectedInvoice
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }











        private string _Returnable_Amt;
        public string Returnable_Amt
        {
            get
            {
                return SelectedInvoice.Returnable_Amt;
            }
            set
            {
                SelectedInvoice.Returnable_Amt = value;

                if (SelectedInvoice.Returnable_Amt != value)
                {
                    SelectedInvoice.Returnable_Amt = value;
                    OnPropertyChanged("Returnable_Amt");
                }
            }
        }






        private string _Total_Rec_Amt;
        public string Total_Rec_Amt
        {
            get
            {
                return SelectedInvoice.Total_Rec_Amt;
            }
            set
            {
                SelectedInvoice.Total_Rec_Amt = value;

                if (SelectedInvoice.Total_Rec_Amt != value)
                {
                    SelectedInvoice.Total_Rec_Amt = value;
                    OnPropertyChanged("Total_Rec_Amt");
                }
            }
        }
        private string _Pending_Amount;
        public string Pending_Amount
        {
            get
            {
                return SelectedInvoice.Pending_Amount;
            }
            set
            {
                SelectedInvoice.Pending_Amount = value;

                if (SelectedInvoice.Pending_Amount != value)
                {
                    SelectedInvoice.Pending_Amount = value;
                    OnPropertyChanged("Pending_Amount");
                }
            }
        }





        private string _Total_Pending_Amount;
        public string Total_Pending_Amount
        {
            get
            {
                return SelectedInvoice.Total_Pending_Amount;
            }
            set
            {
                SelectedInvoice.Total_Pending_Amount = value;

                if (SelectedInvoice.Total_Pending_Amount != value)
                {
                    SelectedInvoice.Total_Pending_Amount = value;
                    OnPropertyChanged("Total_Pending_Amount");
                }
            }
        }
        private string _Other_Pending_Amount;
        public string Other_Pending_Amount
        {
            get
            {
                return SelectedInvoice.Other_Pending_Amount;
            }
            set
            {
                SelectedInvoice.Other_Pending_Amount = value;

                if (SelectedInvoice.Other_Pending_Amount != value)
                {
                    SelectedInvoice.Other_Pending_Amount = value;
                    OnPropertyChanged("Other_Pending_Amount");
                }
            }
        }






        private string _Current_Payble_Amt;
        public string Current_Payble_Amt
        {
            get
            {
                return SelectedInvoice.Current_Payble_Amt;
            }
            set
            {
                SelectedInvoice.Current_Payble_Amt = value;

                if (SelectedInvoice.Current_Payble_Amt != value)
                {
                    SelectedInvoice.Current_Payble_Amt = value;
                    OnPropertyChanged("Current_Payble_Amt");
                }
            }
        }
        private string _Is_Mail;
        public string Is_Mail
        {
            get
            {
                return SelectedInvoice.Is_Mail;
            }
            set
            {
                SelectedInvoice.Is_Mail = value;

                if (SelectedInvoice.Is_Mail != value)
                {
                    SelectedInvoice.Is_Mail = value;
                    OnPropertyChanged("Is_Mail");
                }
            }
        }

        private string _Customer_Mobile_No;
        public string Customer_Mobile_No
        {
            get
            {
                return SelectedInvoice.Customer_Mobile_No;
            }
            set
            {
                SelectedInvoice.Customer_Mobile_No = value;

                if (SelectedInvoice.Customer_Mobile_No != value)
                {
                    SelectedInvoice.Customer_Mobile_No = value;
                    OnPropertyChanged("Customer_Mobile_No");
                }
            }
        }
        private string _Customer_Email;
        public string Customer_Email
        {
            get
            {
                return SelectedInvoice.Customer_Email;
            }
            set
            {
                SelectedInvoice.Customer_Email = value;

                if (SelectedInvoice.Customer_Email != value)
                {
                    SelectedInvoice.Customer_Email = value;
                    OnPropertyChanged("Customer_Email");
                }
            }
        }
        private string _Customer_Id;
        public string Customer_Id
        {
            get
            {
                return SelectedInvoice.Customer_Id;
            }
            set
            {
                SelectedInvoice.Customer_Id = value;

                if (SelectedInvoice.Customer_Id != value)
                {
                    SelectedInvoice.Customer_Id = value;
                    OnPropertyChanged("Customer_Id");
                }
            }
        }

        private string _Bussiness_Location;
        public string Bussiness_Location
        {
            get
            {
                return SelectedInvoice.Bussiness_Location;
            }
            set
            {
                SelectedInvoice.Bussiness_Location = value;

                if (SelectedInvoice.Bussiness_Location != value)
                {
                    SelectedInvoice.Bussiness_Location = value;
                    OnPropertyChanged("Bussiness_Location");
                }
            }
        }


        public ObservableCollection<ItemModel> _ListGrid { get; set; }
        public ObservableCollection<ItemModel> ListGrid
        {
            get
            {
                return _ListGrid;
            }
            set
            {
                this._ListGrid = value;
                OnPropertyChanged("ListGrid");
            }
        }
      public PaymentViewModel()
      { 
      
      
      
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
      void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
      {

      }

      void IModalService.GoBackward(bool dialogReturnValue)
      {
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
