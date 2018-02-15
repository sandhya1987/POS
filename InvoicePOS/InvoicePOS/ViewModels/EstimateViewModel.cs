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
    public class EstimateViewModel : DependencyObject, INotifyPropertyChanged
    {
        public EstimateViewModel()
        {
            SelectedItem = new ItemModel();
            ListGrid = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;
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
                if (value != _ListGrid)
                {
                    this._ListGrid = value;
                    OnPropertyChanged("ListGrid");
                }
            }
        }
        private ItemModel _selectedItem;
        public ItemModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    App.Current.Properties["SelectedItem"] = SelectedItem;
                    NotifyPropertyChanged("SelectedItem");
                }
            }
        }
        private int _OPN_QNT;
        public int? OPN_QNT
        {
            get
            {
                return SelectedItem.OPN_QNT;
            }
            set
            {
                SelectedItem.OPN_QNT = value;

                if (SelectedItem.OPN_QNT != value)
                {
                    SelectedItem.OPN_QNT = value;
                    OnPropertyChanged("OPN_QNT");
                }
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

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private Stack<BackNavigationEventHandler> _backFunctions
           = new Stack<BackNavigationEventHandler>();

        //void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
        //{

        //}

        //void IModalService.GoBackward(bool dialogReturnValue)
        //{
        //}


        //public static IModalService ModalService
        //{
        //    get { return (IModalService)Application.Current.MainWindow; }
        //}
    }
}
