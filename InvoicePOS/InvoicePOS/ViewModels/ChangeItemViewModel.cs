using CrystalDecisions.Windows.Forms;
using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Item;
using InvoicePOS.Views.ChangeItem;
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


namespace InvoicePOS.ViewModels
{
    class ChangeItemViewModel: DependencyObject, INotifyPropertyChanged, IModalService
    {
        private ItemModel _selectedItem;
        ItemModel[] data = null;
        public ObservableCollection<ItemModel> _ListGrid { get; set; }
        public ItemModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    App.Current.Properties["ITEM_NAME"] = SelectedItem.ITEM_NAME;
                    //OnPropertyChanged("SelectedItem");
                }
            }
        }

        public ObservableCollection<ItemModel> ListGrid
        {
            get
            {
                return _ListGrid;

            }
            set
            {
                var data = App.Current.Properties["ItemList"] as ObservableCollection<ItemModel>;
                if (data != _ListGrid)
                {
                    this._ListGrid = data;
                    //OnPropertyChanged("ListGrid");
                }
            }
        }

        private decimal _Sales_Price;
        public decimal SalesPrice
        {
            get
            {
                return _Sales_Price;
            }
            set
            {
                _Sales_Price = value;

                if (_Sales_Price != value)
                {
                    _Sales_Price = value;
                    App.Current.Properties["NewSalesPrice"] = value;
                    //OnPropertyChanged("SalesPrice");
                }
            }
        }
        private decimal _MRP_Price;
        public decimal MRPPrice
        {
            get
            {
                return _MRP_Price;
            }
            set
            {
                _MRP_Price = value;

                if (_MRP_Price != value)
                {
                    _MRP_Price = value;
                    App.Current.Properties["NewMRP"] = MRPPrice;
                    //OnPropertyChanged("SalesPrice");
                }
            }
        }

        private decimal _Base_Price;
        public decimal BasePrice
        {
            get
            {
                return _Base_Price;
            }
            set
            {
                _Base_Price = value;

                if (_Base_Price != value)
                {
                    _Base_Price = value;
                    App.Current.Properties["BasePrice"] = BasePrice;
                    //OnPropertyChanged("SalesPrice");
                }
            }
        }
        private bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;

                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    App.Current.Properties["IsSelected"] = IsSelected;
                    //OnPropertyChanged("SalesPrice");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        void IModalService.GoBackward(bool dialogReturnValue)
        {
            throw new NotImplementedException();
        }

        void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
        {
            throw new NotImplementedException();
        }

        //private ICommand _ChangePriceItem;
        //public ICommand ChangePriceItem
        //{
        //    get
        //    {
        //        if (_ChangePriceItem == null)
        //        {
        //            _ChangePriceItem = new DelegateCommand(ChangePrice_ItemData);

        //        }
        //        return _ChangePriceItem;
        //    }

        //}

        //public void ChangePrice_ItemData()
        //{
        //    App.Current.Properties["SelectData"] = SelectedItem;
        //    App.Current.Properties["ItemName"] = SelectedItem.ITEM_NAME;
        //    App.Current.Properties["ItemCode"] = SelectedItem.ITEM_ID;
        //    App.Current.Properties["OldMRP"] = SelectedItem.MRP;
        //    App.Current.Properties["OldSalesPrice"] = SelectedItem.SALES_PRICE;
        //    App.Current.Properties["OldBarcode"] = SelectedItem.BARCODE;
        //    App.Current.Properties["OldBusinessName"] = SelectedItem.BUSINESS_LOC;


        //    ChangeItemPrice IA = new ChangeItemPrice();
        //    IA.Show();
        //}
        private bool _BARCODEENABLE;
        public bool BARCODEENABLE
        {

            get
            {
                return _BARCODEENABLE;
            }
            set
            {

                _BARCODEENABLE = value;
                OnPropertyChanged("BARCODEENABLE");
            }
        }

        private void OnPropertyChanged(string v)
        {
            throw new NotImplementedException();
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
                if (BARCODEENABLE == true)
                {
                    App.Current.Properties["barcode"] = value;
                }

                OnPropertyChanged("BARCODE");

            }
        }
        public ICommand _ChangePriceItem { get; set; }
        public ICommand ChangePriceItem
        {
            get
            {
                if (_ChangePriceItem == null)
                {
                    _ChangePriceItem = new DelegateCommand(ChangePrice_Item);
                }
            
                return _ChangePriceItem;
            }
        }
        private string _SEARCH_CODE;
        public string SEARCH_CODE
        {
            get
            {
                return SelectedItem.SEARCH_CODE;
            }
            set
            {
                SelectedItem.SEARCH_CODE = value;

                if (SelectedItem.SEARCH_CODE != value)
                {
                    SelectedItem.SEARCH_CODE = value;
                    OnPropertyChanged("SEARCH_CODE");
                }
            }
        }
        
        private decimal _SALES_PRICE_BEFOR_TAX_QTY;
        public decimal SALES_PRICE_BEFOR_TAX_QTY
        {
            get
            {
                return _SALES_PRICE_BEFOR_TAX_QTY;
            }
            set
            {
                _SALES_PRICE_BEFOR_TAX_QTY = value;

                if (_SALES_PRICE_BEFOR_TAX_QTY != value)
                {
                    _SALES_PRICE_BEFOR_TAX_QTY = value;
                    OnPropertyChanged("SALES_PRICE_BEFOR_TAX_QTY");
                }
            }
        }
        public async void ChangePrice_Item()
        {
            App.Current.Properties["Action"] = "Change";
            //Button btn = sender as Button;
            get_list();
            Application.Current.MainWindow.Close();
            try
            {

                //string ftpServerIP = "54.70.197.231";
                //string ftpUserID = "suvendu";
                //string ftpPassword = "vpY9dNp3W9AqhjGy";

                //string ftpServerIP = "34.209.147.13";
                //string ftpUserID = "makrov";
                //string ftpPassword = "Qsefthuk786";

                string ftpServerIP = "115.115.196.30";
                string ftpUserID = "suvendu";
                string ftpPassword = "Passw0rd";


                string filename = "ftp://" + ftpServerIP + "//Upload//";
                App.Current.Properties["Action"] = "Edit";
                SelectedItem = App.Current.Properties["SelectData"] as ItemModel;
                //HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
                // ItemAdd nh = new ItemAdd();
                //data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());

          
                if (App.Current.Properties["ItemName"] != null)
                {
                    SelectedItem.ITEM_NAME = App.Current.Properties["ItemName"] as string;
                }

                if (IsSelected == false)
                {
                    //GetSelectionAutoComplete();
                    BarcodeUnique();
                    SearchCodeUnique();

                    App.Current.Properties["SlectSalesPrice"] = SalesPrice;
                    App.Current.Properties["SlectMRPPrice"] = MRPPrice;
                    if (data.Count() > 1)
                    {
                        MessageBox.Show("" + BARCODE + " Barcode is already present");

                    }
                    else if (data.Count() > 1)
                    {
                        MessageBox.Show("" + SEARCH_CODE + " Search Code is already present");
                    }
                    //else
                    //{

                    if (App.Current.Properties["ItemName"] != null)
                    {
                        SelectedItem.ITEM_NAME = App.Current.Properties["ItemName"].ToString();
                    }
                    if (App.Current.Properties["OldBusinessName"] != null)
                    {
                        SelectedItem.BUSINESS_LOC = App.Current.Properties["OldBusinessName"].ToString();
                    }
                    if (App.Current.Properties["Qunt"] != null)
                    {
                        SelectedItem.OPN_QNT = Convert.ToInt32(App.Current.Properties["Qunt"].ToString());
                    }
                    if (App.Current.Properties["Godown"] != null)
                    {
                        SelectedItem.GODOWN = App.Current.Properties["Godown"].ToString();
                    }
                    if (App.Current.Properties["Date"] != null)
                    {
                        SelectedItem.DATE = Convert.ToDateTime(App.Current.Properties["Date"]);
                    }
                    if (SelectedItem.ITEM_NAME == "" || SelectedItem.ITEM_NAME == null)
                    {
                        MessageBox.Show("Item Name is missing");

                    }
                    if (SelectedItem.OPENING_STOCK_ID != 0)
                    {
                        SelectedItem.OPENING_STOCK_ID = Convert.ToInt64(App.Current.Properties["OPENING_STOCK_ID"].ToString());
                    }
                    if (SalesPrice != 0)
                    {
                        SelectedItem.SALES_PRICE = SalesPrice;
                    }
                    if (MRPPrice != 0)
                    {
                        SelectedItem.MRP = MRPPrice;
                    }

                    else if (SelectedItem.PURCHASE_UNIT_PRICE > SelectedItem.SALES_PRICE_BEFOR_TAX)
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Do you want to keep 'Sales Price' less than 'Purchase Price'?", "Information", System.Windows.MessageBoxButton.YesNo);
                        ItemAdd.PurchasePrice.Focus();
                        return;
                    }
                    else if (SelectedItem.PURCHASE_UNIT_PRICE > SelectedItem.SALES_PRICE)
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Do you want to keep 'Sales Price' less than 'Purchase Price'?", "Information", System.Windows.MessageBoxButton.YesNo);
                        ItemAdd.PurchasePrice.Focus();
                        return;
                    }
                    else if (SelectedItem.SALES_PRICE > SelectedItem.MRP)
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("'Sales Price' cannot be greater than 'MRP' Price'");
                        ItemAdd.SalesPrice.Focus();
                        return;
                    }
                    else if (SelectedItem.SALES_PRICE_BEFOR_TAX > SelectedItem.MRP)
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("'Sales Price Before Tax' cannot be greater than 'MRP' Price'");
                        ItemAdd.SalesPriceBeforeTax.Focus();
                        return;
                    }

                    else if (SelectedItem.BARCODE == "" || SelectedItem.BARCODE == null)
                    {
                        MessageBox.Show("Barcode is missing");
                    }
                    else if (SelectedItem.SEARCH_CODE == "" || SelectedItem.SEARCH_CODE == null)
                    {
                        MessageBox.Show("Search code is missing");
                    }
                    else if (SelectedItem.OPN_QNT == 0 || SelectedItem.OPN_QNT == null)
                    {
                        MessageBox.Show("missing Op. Stock");
                        Window_Opening_stock IA = new Window_Opening_stock();
                        IA.Show();
                    }
                    else if (SelectedItem.SALES_UNIT == "" || SelectedItem.SALES_UNIT == null)
                    {
                        MessageBox.Show("Sales Unit is missing");
                    }
                    else if (SelectedItem.MRP == 0 || SelectedItem.MRP == null)
                    {
                        MessageBox.Show("MRP is missing");
                    }
                    else if (SelectedItem.PURCHASE_UNIT == "" || SelectedItem.PURCHASE_UNIT == null)
                    {
                        MessageBox.Show("Purchase unit is missing");
                    }
                }
                else
                {
                    System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    var response = await client.PostAsJsonAsync("api/ItemAPI/ItemUpdate/", SelectedItem);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Item Updated Successfully");
                        //Cancel_Item();
                        //ModalService.NavigateTo(new Items(), delegate (bool returnValue) { });
                    }
                    else
                    {
                        MessageBox.Show("Barcode is missing");
                    }
                }
                    //{
                        
                        //else
                        //{
                        //    MessageBox.Show("Internal Problem");
                        //}
                    //}
                //}
                // }
                
            }

            catch (Exception ex)
            {

                throw;
            }

        }

        public async void BarcodeUnique()
        {
            //if (App.Current.Properties["ManualBarcode"] != "" || App.Current.Properties["ManualBarcode"] != null)
            //{
            //    BARCODE = App.Current.Properties["ManualBarcode"].ToString();
            //}
            //else
            //{
            try
            {


                string bra = BARCODE.ToString();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/BarCodeunique?barcode=" + bra + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                }
                if (data.Length == 0)
                {

                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            //}
        }
        public async void SearchCodeUnique()
        {
            try
            {


                string bra = SEARCH_CODE.ToString();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/SearchCodeunique?SeaerchCode=" + bra + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //private bool _ExPay;
        //public bool ExPay
        //{

        //    get
        //    {
        //        return _ExPay;
        //    }
        //    set
        //    {

        //        _ExPay = value;
        //        OnPropertyChanged("ExPay");
        //    }
        //}
        //private bool _PayNow;
        //public bool PayNow
        //{

        //    get
        //    {
        //        return _PayNow;
        //    }
        //    set
        //    {

        //        _PayNow = value;
        //        OnPropertyChanged("PayNow");
        //    }
        //}
        //private int? _QUNT_TOTAL;
        //public int? QUNT_TOTAL
        //{
        //    get
        //    {
        //        return _QUNT_TOTAL;
        //    }
        //    set
        //    {
        //        _QUNT_TOTAL = value;
        //        OnPropertyChanged("QUNT_TOTAL");

        //    }
        //}

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
        public bool _ChangePriceItemList { get; set; }
        public bool ChangePriceItemList
        {
            get
            {
                if (_ChangePriceItemList == null && ChangePriceItemList == true)
                {
                    //_ChangePriceItemList = new DelegateCommand(ChangePrice_ItemList);
                    Cancel_Item();
                    ChangePrice_ItemList();
                }
                return _ChangePriceItemList;
            }
        }

        //public ICommand UpdateQnt
        //{
        //    get
        //    {
        //        if (_UpdateQnt == null)
        //        {
        //            _UpdateQnt = new RelayCommand(param => this.Change_Quantity(param));
        //        }
        //        return _UpdateQnt;
        //    }
        //    set
        //    {
        //        OnPropertyChanged("UpdateQnt");
        //    }
        //}

        //public void Change_Quantity(Object sender)
        //{
        //    App.Current.Properties["Action"] = "Change";
        //    Button btn = sender as Button;
        //    get_list();
        //    Application.Current.MainWindow.Close();
        //}


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
                                MRP = MRPPrice,
                                PURCHASE_UNIT = item.PURCHASE_UNIT,
                                PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                                SALES_PRICE = SalesPrice,
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
                                //Current_Qty = WeightQnt,
                                OPN_QNT = item.OPN_QNT,
                                REGIONAL_LANGUAGE = item.REGIONAL_LANGUAGE,
                                SALES_PRICE_BEFOR_TAX = MRPPrice,
                                Total =  SalesPrice,
                                Discount = item.Discount,
                                TaxName = item.TaxName,
                                TaxValue = item.TaxValue,
                                SalePriceWithDiscount = SalesPrice,
                                SALES_PRICE_BEFOR_TAX_QTY = SalesPrice
                            });
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
                            MRP = MRPPrice,
                            PURCHASE_UNIT = item.PURCHASE_UNIT,
                            PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                            SALES_PRICE = SalesPrice,
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
                            //Current_Qty = WeightQnt,
                            OPN_QNT = item.OPN_QNT,
                            REGIONAL_LANGUAGE = item.REGIONAL_LANGUAGE,
                            SALES_PRICE_BEFOR_TAX = MRPPrice,
                            Total =  SalesPrice,
                            Discount = item.Discount,
                            TaxName = item.TaxName,
                            TaxValue = item.TaxValue,
                            SalePriceWithDiscount = SalesPrice,
                            SALES_PRICE_BEFOR_TAX_QTY = SalesPrice
                        });
                    }

                    //Main.ListQnt.Text = (WeightQnt + Convert.ToInt32(Main.ListQnt.Text)).ToString();

                    var GrossAmt = Main.GrossamountReff.Text;
                    //var valgrss = ((decimal)(WeightQnt) * (item.SALES_PRICE));
                    //var grodd = valgrss + Convert.ToDecimal(GrossAmt);

                    //Main.GrossamountReff.Text = grodd.ToString();
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
                        MRP = MRPPrice,
                        PURCHASE_UNIT = item.PURCHASE_UNIT,
                        PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                        SALES_PRICE = SalesPrice,
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
                        SALES_PRICE_BEFOR_TAX = SalesPrice,
                        Total =  SalesPrice,
                        Discount = item.Discount,
                        TaxName = item.TaxName,
                        TaxValue = item.TaxValue,
                        SalePriceWithDiscount = SalesPrice,
                        SALES_PRICE_BEFOR_TAX_QTY = SalesPrice
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

        public async void ChangePrice_ItemList()
        {
            decimal Dis = 0;
            decimal vatAmount = 0;
            decimal netAmount = 0;
            decimal GossAmount = 0;
            decimal ItemToTal = 0;
            decimal netamount = 0;
            decimal texa = 0;
            decimal vatAm = 0;
            ListGrid = App.Current.Properties["ItemList"] as ObservableCollection<ItemModel>;
            if (ListGrid.Count == 0)
            {
                //ExPay = false;
                //PayNow = false;
            }
            else
            {
                //ExPay = true;
                //PayNow = true;
                ItemToTal = ListGrid.Count;
                //for (int i = 0; i < ListGrid.Count; i++)
                //{
                //     netamount = ListGrid[i].SALES_PRICE_BEFOR_TAX - ListGrid[i].Discount;
                //    // decimal? Qamt = ListGrid[i].Total * ListGrid[i].Current_Qty;
                //    GossAmount = Convert.ToDecimal(ListGrid[i].Total + GossAmount);

                //    App.Current.Properties["CurrentGrosAmount"] = GROSSAMT;
                //    NETAMT = Convert.ToDecimal((ListGrid[i].SALES_PRICE) * (ListGrid[i].OPN_QNT) + NETAMT);
                //}

                //QUNT_TOTAL = 0;
                foreach (var qunt in ListGrid)
                {
                    //QUNT_TOTAL = qunt.Current_Qty + QUNT_TOTAL;

                }
                for (int i = 0; i < ListGrid.Count; i++)
                {
                    if (ListGrid[i].Discount != 0)
                    {
                        decimal ItemNet = ListGrid[i].SALES_PRICE_BEFOR_TAX - ListGrid[i].Discount;
                        decimal ItemVat = (ItemNet / 100) * ListGrid[i].TAX_COLLECTED;
                        decimal ItemGoss = ItemNet + ItemVat;
                        decimal ItemDis = ListGrid[i].Discount;
                        decimal salPrice = Convert.ToDecimal((ListGrid[i].Current_Qty * ListGrid[i].SALES_PRICE_BEFOR_TAX) - ListGrid[i].Discount);
                        ListGrid[i].Total = (salPrice * (ListGrid[i].TAX_COLLECTED / 100)) + salPrice;
                        ListGrid[i].TotalTax = SalesPrice * (ListGrid[i].TAX_COLLECTED / 100);
                        ListGrid[i].SALES_PRICE_BEFOR_TAX_QTY = Convert.ToDecimal(ListGrid[i].Current_Qty * MRPPrice);
                        ListGrid[i].SalePriceWithDiscount = Convert.ToDecimal(ListGrid[i].Current_Qty * MRPPrice) - ListGrid[i].Discount;
                    }
                    else
                    {
                        ListGrid[i].TotalTax = ListGrid[i].Current_Qty * (SalesPrice - ListGrid[i].SALES_PRICE_BEFOR_TAX);
                        ListGrid[i].SALES_PRICE_BEFOR_TAX_QTY = Convert.ToDecimal(ListGrid[i].Current_Qty * MRPPrice);
                        ListGrid[i].SalePriceWithDiscount = Convert.ToDecimal(ListGrid[i].Current_Qty * MRPPrice);
                    }
                }
                foreach (var Disss in ListGrid)
                {
                    if (Disss.Discount == 0 || Disss.Discount == null)
                    {

                        decimal ItemGoss = 0;
                        ItemGoss = Disss.Total;
                        decimal VatItem = Convert.ToDecimal(Disss.Current_Qty) * (Disss.SALES_PRICE - Disss.SALES_PRICE_BEFOR_TAX);
                        vatAmount = VatItem + vatAmount;
                        netAmount = (ItemGoss - VatItem) + netAmount;
                        GossAmount = ItemGoss + GossAmount;

                    }
                    else
                    {
                        decimal ItemNet = (Convert.ToDecimal(Disss.Current_Qty) * Disss.SALES_PRICE_BEFOR_TAX) - Disss.Discount;
                        decimal ItemVat = ItemNet * (Disss.TAX_COLLECTED / 100);
                        decimal ItemGoss = ItemNet + ItemVat;
                        decimal ItemDis = Disss.Discount;


                        netAmount = netAmount + ItemNet;
                        vatAmount = ItemVat + vatAmount;
                        GossAmount = ItemGoss + GossAmount;
                        Dis = ItemDis + Dis;
                    }

                }

            }
            Main.GrossamountReff.Text = Convert.ToString(GossAmount.ToString("0.00"));
            Main.DiscountMainReff.Text = Convert.ToString(Dis.ToString("0.00"));
            Main.VatMainReff.Text = Convert.ToString(vatAmount.ToString("0.00"));
            Main.NetAmountMainReff.Text = Convert.ToString(netAmount.ToString("0.00"));
            Main.ListQnt.Text = Convert.ToString(ItemToTal);
            //Main.ItemTotalMainReff.Text = Convert.ToString(QUNT_TOTAL);
        }
    }
}
