using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.Views.Main;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace InvoicePOS.ViewModels
{
    public class ChangQntViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        ItemModel[] data = null;
        public ChangQntViewModel()
        {
            if (App.Current.Properties["SelectedItem"] != null)
            {
                SelectedItem = App.Current.Properties["SelectedItem"] as ItemModel;
                WeightQnt = Convert.ToInt32(SelectedItem.Current_Qty);
            }

        }

        private decimal _WeightPrice;
        public decimal WeightPrice
        {
            get
            {
                return SelectedItem.WeightPrice;
            }
            set
            {
                SelectedItem.WeightPrice = value;
                OnPropertyChanged("WeightPrice");

            }
        }
        private int _WeightQnt;
        public int WeightQnt
        {
            get
            {
                return SelectedItem.WeightQnt;
            }
            set
            {
                SelectedItem.WeightQnt = value;
                WeightPrice = SelectedItem.WeightQnt * SelectedItem.SALES_PRICE;
                OnPropertyChanged("WeightQnt");
            }
        }
        private string _BARCODE;
        public string BARCODE
        {
            get
            {
                return SelectedItem.BARCODE;
            }
            set
            {
                SelectedItem.BARCODE = value;


                if (SelectedItem.BARCODE != value)
                {
                    SelectedItem.BARCODE = value;
                    OnPropertyChanged("BARCODE");
                }
            }
        }

        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_Item);
                }
                return _Cancel;
            }
        }



        public void Cancel_Item()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }


        private RelayCommand _UpdateQnt;

        public ICommand UpdateQnt
        {
            get
            {
                if (_UpdateQnt == null)
                {
                    _UpdateQnt = new RelayCommand(param => this.Change_Quantity(param));
                }
                return _UpdateQnt;
            }
            set
            {
                OnPropertyChanged("UpdateQnt");
            }
        }

        public void Change_Quantity(Object sender)
        {
            App.Current.Properties["Action"] = "Change";
            Button btn = sender as Button;
            get_list();
            Application.Current.MainWindow.Close();
        }


        public void get_list()
        {
            ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
            var Load_Item = (((IEnumerable)App.Current.Properties["DataGridL"]).Cast<ItemModel>()).ToList();
            Main.ListGridRef.ItemsSource = null;

            Main.GrossamountReff.Text = "0";
            Main.ListQnt.Text = "0";
            int x = 0;
            foreach (var item in Load_Item)
            {
                int? Quty = item.OPN_QNT;
                if (item.ITEM_ID == Convert.ToInt64(App.Current.Properties["ITEM_ID"]))
                {
                   if (Quty <= WeightQnt)
                    {
                       MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("This Item not Avable. Do you went to add this item?", "Add Item", System.Windows.MessageBoxButton.YesNo);
                       if (messageBoxResult == MessageBoxResult.Yes)
                       {
                           _ListGrid_Temp.Add(new ItemModel
                           {
                               SLNO = x + 1,
                               ITEM_NAME = item.ITEM_NAME,
                               ITEM_ID = item.ITEM_ID,
                               BARCODE = item.BARCODE,
                               ACCESSORIES_KEYWORD = item.ACCESSORIES_KEYWORD,
                               CATAGORY_ID = item.CATAGORY_ID,
                               ITEM_DESCRIPTION = item.ITEM_DESCRIPTION,
                               ITEM_INVOICE_ID = item.ITEM_INVOICE_ID,
                               ITEM_PRICE = item.ITEM_PRICE,
                               ITEM_PRODUCT_ID = item.ITEM_PRODUCT_ID,
                               KEYWORD = item.KEYWORD,
                               MRP = item.MRP,
                               PURCHASE_UNIT = item.PURCHASE_UNIT,
                               PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                               SALES_PRICE = item.SALES_PRICE,
                               SALES_UNIT = item.SALES_UNIT,
                               SEARCH_CODE = item.SEARCH_CODE,
                               TAX_COLLECTED = item.TAX_COLLECTED,
                               TAX_PAID = item.TAX_PAID,
                               ALLOW_PURCHASE_ON_ESHOP = item.ALLOW_PURCHASE_ON_ESHOP,
                               CATEGORY_NAME = item.CATEGORY_NAME,
                               DISPLAY_INDEX = item.DISPLAY_INDEX,
                               INCLUDE_TAX = item.INCLUDE_TAX,
                               ITEM_GROUP_NAME = item.ITEM_GROUP_NAME,
                               ITEM_UNIQUE_NAME = item.ITEM_UNIQUE_NAME,
                               Current_Qty = WeightQnt,
                               OPN_QNT = item.OPN_QNT,
                               REGIONAL_LANGUAGE = item.REGIONAL_LANGUAGE,
                               SALES_PRICE_BEFOR_TAX = item.SALES_PRICE_BEFOR_TAX,
                               Total = ((decimal)(WeightQnt) * (item.SALES_PRICE)),
                               Discount = item.Discount,
                               TaxName = item.TaxName,
                               TaxValue = item.TaxValue,
                           });
                       }
                    }
                    else
                    {
                        _ListGrid_Temp.Add(new ItemModel
                        {
                            SLNO = x + 1,
                            ITEM_NAME = item.ITEM_NAME,
                            ITEM_ID = item.ITEM_ID,
                            BARCODE = item.BARCODE,
                            ACCESSORIES_KEYWORD = item.ACCESSORIES_KEYWORD,
                            CATAGORY_ID = item.CATAGORY_ID,
                            ITEM_DESCRIPTION = item.ITEM_DESCRIPTION,
                            ITEM_INVOICE_ID = item.ITEM_INVOICE_ID,
                            ITEM_PRICE = item.ITEM_PRICE,
                            ITEM_PRODUCT_ID = item.ITEM_PRODUCT_ID,
                            KEYWORD = item.KEYWORD,
                            MRP = item.MRP,
                            PURCHASE_UNIT = item.PURCHASE_UNIT,
                            PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                            SALES_PRICE = item.SALES_PRICE,
                            SALES_UNIT = item.SALES_UNIT,
                            SEARCH_CODE = item.SEARCH_CODE,
                            TAX_COLLECTED = item.TAX_COLLECTED,
                            TAX_PAID = item.TAX_PAID,
                            ALLOW_PURCHASE_ON_ESHOP = item.ALLOW_PURCHASE_ON_ESHOP,
                            CATEGORY_NAME = item.CATEGORY_NAME,
                            DISPLAY_INDEX = item.DISPLAY_INDEX,
                            INCLUDE_TAX = item.INCLUDE_TAX,
                            ITEM_GROUP_NAME = item.ITEM_GROUP_NAME,
                            ITEM_UNIQUE_NAME = item.ITEM_UNIQUE_NAME,
                            Current_Qty = WeightQnt,
                            OPN_QNT = item.OPN_QNT,
                            REGIONAL_LANGUAGE = item.REGIONAL_LANGUAGE,
                            SALES_PRICE_BEFOR_TAX = item.SALES_PRICE_BEFOR_TAX,
                            Total = ((decimal)(WeightQnt) * (item.SALES_PRICE)),
                            Discount = item.Discount,
                            TaxName = item.TaxName,
                            TaxValue = item.TaxValue,
                        });
                    }

                    Main.ListQnt.Text = (WeightQnt + Convert.ToInt32(Main.ListQnt.Text)).ToString();

                    var GrossAmt = Main.GrossamountReff.Text;
                    var valgrss = ((decimal)(WeightQnt) * (item.SALES_PRICE));
                    var grodd = valgrss + Convert.ToDecimal(GrossAmt);

                    Main.GrossamountReff.Text = grodd.ToString();                   
                }
                else
                {
                    _ListGrid_Temp.Add(new ItemModel
                    {
                        SLNO = x + 1,
                        ITEM_NAME = item.ITEM_NAME,
                        ITEM_ID = item.ITEM_ID,
                        BARCODE = item.BARCODE,
                        ACCESSORIES_KEYWORD = item.ACCESSORIES_KEYWORD,
                        CATAGORY_ID = item.CATAGORY_ID,
                        ITEM_DESCRIPTION = item.ITEM_DESCRIPTION,
                        ITEM_INVOICE_ID = item.ITEM_INVOICE_ID,
                        ITEM_PRICE = item.ITEM_PRICE,
                        ITEM_PRODUCT_ID = item.ITEM_PRODUCT_ID,
                        KEYWORD = item.KEYWORD,
                        MRP = item.MRP,
                        PURCHASE_UNIT = item.PURCHASE_UNIT,
                        PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                        SALES_PRICE = item.SALES_PRICE,
                        SALES_UNIT = item.SALES_UNIT,
                        SEARCH_CODE = item.SEARCH_CODE,
                        TAX_COLLECTED = item.TAX_COLLECTED,
                        TAX_PAID = item.TAX_PAID,
                        ALLOW_PURCHASE_ON_ESHOP = item.ALLOW_PURCHASE_ON_ESHOP,
                        CATEGORY_NAME = item.CATEGORY_NAME,
                        DISPLAY_INDEX = item.DISPLAY_INDEX,
                        INCLUDE_TAX = item.INCLUDE_TAX,
                        ITEM_GROUP_NAME = item.ITEM_GROUP_NAME,
                        ITEM_UNIQUE_NAME = item.ITEM_UNIQUE_NAME,
                        Current_Qty = item.Current_Qty,
                        OPN_QNT = item.OPN_QNT,
                        REGIONAL_LANGUAGE = item.REGIONAL_LANGUAGE,
                        SALES_PRICE_BEFOR_TAX = item.SALES_PRICE_BEFOR_TAX,
                        Total = ((decimal)(item.Current_Qty) * (item.SALES_PRICE)),
                        Discount = item.Discount,
                        TaxName = item.TaxName,
                        TaxValue = item.TaxValue,
                    });

                    Main.ListQnt.Text = (item.OPN_QNT + Convert.ToInt32(Main.ListQnt.Text)).ToString();
                    var GrossAmt = Main.GrossamountReff.Text;
                    var valgrss = ((decimal)(item.OPN_QNT) * (item.SALES_PRICE));
                    var grodd = valgrss + Convert.ToDecimal(GrossAmt);

                    Main.GrossamountReff.Text = grodd.ToString(); 




                    //var GrossAmt = Main.GrossamountReff.Text;
                    //Main.GrossamountReff.Text = (((decimal)(WeightQnt) * (item.SALES_PRICE))+).ToString();
                }




                App.Current.Properties["DataGridL"] = _ListGrid_Temp;
                App.Current.Properties["DataGrid"] = _ListGrid_Temp;

            }

            Main.ListGridRef.ItemsSource = null;
            // Main.ListGridRef.ClearValue();
            Main.ListGridRef.ItemsSource = _ListGrid_Temp.ToList();

            var Load_Item1 = (((IEnumerable)App.Current.Properties["DataGridL"]).Cast<ItemModel>()).ToList();
            //_ListGrid_Temp = (Main.ListGridRef.ItemsSource.ToList();
        }





        private List<ItemModel> _ItemData;
        public List<ItemModel> ItemData
        {
            get { return _ItemData; }
            set
            {
                if (_ItemData != value)
                {
                    _ItemData = value;
                }
            }

        }
        public ItemModel _selectedItem;
        public ItemModel SelectedItem
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
        //private string _BARCODE;
        //public string BARCODE
        //{
        //    get
        //    {
        //        return SelectedItem.BARCODE;
        //    }
        //    set
        //    {
        //        SelectedItem.BARCODE = value;


        //        if (SelectedItem.BARCODE != value)
        //        {
        //            SelectedItem.BARCODE = value;
        //            OnPropertyChanged("BARCODE");
        //        }
        //    }
        //}

        private int? _OPN_QNT;
        public int? OPN_QNT
        {
            get
            {
                return _OPN_QNT;
            }
            set
            {
                _OPN_QNT = value;

                if (_OPN_QNT != value)
                {
                    _OPN_QNT = value;
                    OnPropertyChanged("OPN_QNT");
                }
            }
        }


        public void Close()
        {

            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
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
    }
}
