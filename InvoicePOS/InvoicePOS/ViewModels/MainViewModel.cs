using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.Customer;
using InvoicePOS.UserControll.Item;
using InvoicePOS.Views.Main;
using InvoicePOS.Views.Report;
using InvoicePOS.Views.WelCome;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Net;
using System.Windows.Media.Imaging;
using System.Data;
using InvoicePOS.Views.CashRegister;
using InvoicePOS.UserControll.CashReg;
using InvoicePOS.UserControll.Estimate;
using InvoicePOS.Views.ChangeItem;

namespace InvoicePOS.ViewModels
{
    public class MainViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        #region Properties
        ObservableCollection<ItemModel> _ListGrid_Temp12 = new ObservableCollection<ItemModel>();
        ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
        ItemModel[] data = null;
        public ObservableCollection<ItemModel> _ListGrid { get; set; }
        public ObservableCollection<InvoiceModel> _ListInvoice { get; set; }
        int x = 0;
        decimal sum = 0;
        int? qut = 0;
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
                    App.Current.Properties["ItemList"] = value;
                }
            }
        }
        public HttpResponseMessage response;

        private string _BusinessLocAddress { get; set; }
        public string BusinessLocAddress
        {
            get { return _BusinessLocAddress; }
            set
            {
                _BusinessLocAddress = value;


                if (_BusinessLocAddress != value)
                {
                    _BusinessLocAddress = value;
                    OnPropertyChanged("BusinessLocAddress");
                }

            }

        }
        private string _BusinessLocName { get; set; }
        public string BusinessLocName
        {
            get { return _BusinessLocName; }
            set
            {
                _BusinessLocName = value;


                if (_BusinessLocName != value)
                {
                    _BusinessLocName = value;
                    OnPropertyChanged("BusinessLocName");
                }

            }

        }
        private string _COMP_NAME { get; set; }
        public string COMP_NAME
        {
            get { return _COMP_NAME; }
            set
            {
                _COMP_NAME = value;


                if (_COMP_NAME != value)
                {
                    _COMP_NAME = value;
                    OnPropertyChanged("COMP_NAME");
                }

            }

        }
        private string _CASH_NAME { get; set; }
        public string CASH_NAME
        {
            get { return _CASH_NAME; }
            set
            {
                _CASH_NAME = value;


                if (_CASH_NAME != value)
                {
                    _CASH_NAME = value;
                    OnPropertyChanged("CASH_NAME");
                }
                

            }

        }

        public ICommand _CashClick;
        public ICommand CashClick
        {
            get
            {
                if (_CashClick == null)
                {
                    _CashClick = new DelegateCommand(Cash_Click);
                }
                return _CashClick;
            }
        }

        public void Cash_Click()
        {

            // WelComePage.ItemPRef.Background = System.Windows.Media.Brushes.Red;


            //App.Current.Properties["ITemAdd"] = 1;
            //App.Current.Properties["Action"] = 123;
            //App.Current.Properties["itemName"] = null;
            //App.Current.Properties["barcode"] = null;
            //App.Current.Properties["BussLocation"] = null;
            //App.Current.Properties["Qunt"] = null;
            //App.Current.Properties["Godown"] = null;
            CashReg IA = new CashReg();
            IA.Show();
            // ModalService.NavigateTo(new ItemAdd(), delegate(bool returnValue) { });


        }
        public ICommand _CashRegisterAmountClick { get; set; }
        public ICommand CashRegisterAmountClick
        {
            get
            {
                if (_CashRegisterAmountClick == null)
                {
                    _CashRegisterAmountClick = new DelegateCommand(CashRegisterAmountClick_Ok);
                }
                return _CashRegisterAmountClick;
            }
        }
        public void CashRegisterAmountClick_Ok()
        {
            CashRegisterAmountDetails IA = new CashRegisterAmountDetails();
            IA.Show();
            if (App.Current.Properties["CashReg"] != null)
            {
                //var d = App.Current.Properties["ChangeBusinessLocation"] as BusinessLocationModel;
                CashRegisterAmountDetails.CashRegNo.Text = App.Current.Properties["CashReg"].ToString();
                CashRegisterAmountDetails.BusLocationName.Text = App.Current.Properties["SelectBusinessName"].ToString();
                CashRegisterAmountDetails.CurrentCash.Text = App.Current.Properties["CurrentCash"].ToString();
            }
        }
        public ICommand _AddCashRegisterClick { get; set; }
        public ICommand AddCashRegisterClick
        {
            get
            {
                if (_AddCashRegisterClick == null)
                {
                    _AddCashRegisterClick = new DelegateCommand(AddCashRegisterClick_Ok);
                }
                return _AddCashRegisterClick;
            }
        }
        public void AddCashRegisterClick_Ok()
        {
            AddNewCashTransfer IA = new AddNewCashTransfer();
            IA.Show();

        }
        public ICommand _BusinessClick;
        public ICommand BusinessClick
        {
            
            get
            {
                if (_BusinessClick == null)
                {
                    _BusinessClick = new DelegateCommand(BusinessLocation_Click);
                }
                return _BusinessClick;
            }
            
        }
        //public void Business_Click()
        //{
        //    var locList = 
        //}
        public void BusinessLocation_Click()
        {
            App.Current.Properties["ItemSearchMain"] = 1;
            Window_ItemList ex = new Window_ItemList();
            ex.ShowDialog();
        }
        public void Select_Location()
        {
            GetBusinessLocList();
        }
        public async Task<string> GetBusinessLocList()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/CashRegAPI/GetBusinessLoc").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                if (data.Length > 0)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        SelectedItem.ITEM_ID = data[i].ITEM_ID;
                        
                    }
                }
            
        }

            return null;
        }
        private string _COMP_EMAIL { get; set; }
        public string COMP_EMAIL
        {
            get { return _COMP_EMAIL; }
            set
            {
                _COMP_EMAIL = value;


                if (_COMP_EMAIL != value)
                {
                    _COMP_EMAIL = value;
                    OnPropertyChanged("COMP_EMAIL");
                }

            }

        }
        private string _COMP_ADDRESS1 { get; set; }
        public string COMP_ADDRESS1
        {
            get { return _COMP_ADDRESS1; }
            set
            {
                _COMP_ADDRESS1 = value;
                if (_COMP_ADDRESS1 != value)
                {
                    _COMP_ADDRESS1 = value;
                    OnPropertyChanged("COMP_ADDRESS1");
                }
            }
        }
        private string _COMP_ADDRESS2 { get; set; }
        public string COMP_ADDRESS2
        {
            get { return _COMP_ADDRESS2; }
            set
            {
                _COMP_ADDRESS2 = value;
                if (_COMP_ADDRESS2 != value)
                {
                    _COMP_ADDRESS2 = value;
                    OnPropertyChanged("COMP_ADDRESS2");
                }
            }
        }

        private string _INVOICE_NO { get; set; }
        public string INVOICE_NO
        {
            get { return _INVOICE_NO; }
            set
            {
                _INVOICE_NO = value;
                App.Current.Properties["CurrentInvoiceNo"] = _INVOICE_NO;
                OnPropertyChanged("INVOICE_NO");
            }
        }

        private string _SALES_PRICE { get; set; }
        public string SALES_PRICE
        {
            get { return _SALES_PRICE; }
            set
            {
                _SALES_PRICE = value;
                OnPropertyChanged("SALES_PRICE");
            }
        }


        private string _ItemNameStock { get; set; }
        public string ItemNameStock
        {
            get { return _ItemNameStock; }
            set
            {
                _ItemNameStock = value;
                if (_ItemNameStock != null)
                {
                    // TabChangeCode_Stock();
                }
                OnPropertyChanged("ItemNameStock");
            }

        }
        private string _Select_ItemName { get; set; }
        public string Select_ItemName
        {
            get
            {

                return _Select_ItemName;

            }
            set
            {
                _Select_ItemName = value;
                //App.Current.Properties["CurrentBarcode"] = value;
                OnPropertyChanged("Select_ItemName");


            }

        }


        private string _Select_BarCode { get; set; }
        public string Select_BarCode
        {
            get
            {

                return _Select_BarCode;

            }
            set
            {
                _Select_BarCode = value;
                App.Current.Properties["CurrentBarcode"] = value;
                OnPropertyChanged("Select_BarCode");


            }

        }

        //private ICommand _TabChangeCodeStock { get; set; }
        //public ICommand TabChangeCodeStock
        //{
        //    get
        //    {
        //        if (TabChangeCodeStock == null)
        //        {
        //            _TabChangeCodeStock = new DelegateCommand(TabChangeCode_Stock);
        //        }
        //        return _TabChangeCodeStock;
        //    }
        //}

        //public async void TabChangeCode_Stock()
        //{
        //    comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.Timeout = new TimeSpan(500000000000);
        //    HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
        //        if (data.Length > 0)
        //        {
        //            for (int i = 0; i < data.Length; i++)
        //            {
        //                _ListGrid_Temp12.Add(new ItemModel
        //                {
        //                    // ITEM_ID = ItemId,
        //                    ITEM_NAME = data[i].ITEM_NAME,
        //                    ITEM_ID = data[i].ITEM_ID,
        //                    BARCODE = data[i].BARCODE,
        //                    ACCESSORIES_KEYWORD = data[i].ACCESSORIES_KEYWORD,
        //                    CATAGORY_ID = data[i].CATAGORY_ID,
        //                    ITEM_DESCRIPTION = data[i].ITEM_DESCRIPTION,
        //                    ITEM_INVOICE_ID = data[i].ITEM_INVOICE_ID,
        //                    ITEM_PRICE = data[i].ITEM_PRICE,
        //                    ITEM_PRODUCT_ID = data[i].ITEM_PRODUCT_ID,
        //                    KEYWORD = data[i].KEYWORD,
        //                    MRP = data[i].MRP,
        //                    PURCHASE_UNIT = data[i].PURCHASE_UNIT,
        //                    PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE,
        //                    SALES_PRICE = data[i].SALES_PRICE,
        //                    SALES_UNIT = data[i].SALES_UNIT,
        //                    SEARCH_CODE = data[i].SEARCH_CODE,
        //                    TAX_COLLECTED = data[i].TAX_COLLECTED,
        //                    TAX_PAID = data[i].TAX_PAID,
        //                    ALLOW_PURCHASE_ON_ESHOP = data[i].ALLOW_PURCHASE_ON_ESHOP,
        //                    CATEGORY_NAME = data[i].CATEGORY_NAME,
        //                    DISPLAY_INDEX = data[i].DISPLAY_INDEX,
        //                    INCLUDE_TAX = data[i].INCLUDE_TAX,
        //                    ITEM_GROUP_NAME = data[i].ITEM_GROUP_NAME,
        //                    ITEM_UNIQUE_NAME = data[i].ITEM_UNIQUE_NAME,
        //                    Current_Qty = data[i].Current_Qty,
        //                    OPN_QNT = data[i].OPN_QNT,
        //                    REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE,
        //                    SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX,
        //                    GODOWN = data[i].GODOWN,
        //                });

        //            }

        //        }

        //    }
        //    if (ItemNameStock != null)
        //    {
        //        var str12 = (from a in _ListGrid_Temp12 where a.ITEM_NAME == ItemNameStock select a).FirstOrDefault();
        //        App.Current.Properties["ShowStockMain"] = str12;
        //        ShowStockGrid();
        //    }

        //}


        //private ICommand _ShowItemGrid { get; set; }
        //public ICommand ShowItemGrid
        //{
        //    get
        //    {
        //        if (_ShowItemGrid == null)
        //        {
        //            _ShowItemGrid = new DelegateCommand(ShowItem_Grid);
        //        }
        //        return _ShowItemGrid;
        //    }
        //}

        //public void ShowItem_Grid()
        //{

        //    GetBarcodeSearch(comp);

        //    _Select_BarCode = "";
        //    Select_BarCode = "";
        //}

        private string _SearchBarCodeStock { get; set; }
        public string SearchBarCodeStock
        {
            get { return _SearchBarCodeStock; }
            set
            {
                _SearchBarCodeStock = value;
                OnPropertyChanged("SearchBarCodeStock");
            }

        }

        # region tab Change on main page and Show Stock Page

        private ICommand _TabChangeCode { get; set; }
        public ICommand TabChangeCode
        {
            get
            {
                if (_TabChangeCode == null)
                {
                    _TabChangeCode = new DelegateCommand(TabChange_Code);
                }
                return _TabChangeCode;
            }
        }

        public void TabChange_Code()
        {

            GetBarcodeSearch(comp);

            _Select_BarCode = "";
            Select_BarCode = "";
        }


        #endregion
        //public async void ShowStockGrid()
        //{
        //    if (ListGrid2 != null)
        //    {
        //        if (ListGrid2.Count > 0)
        //        {
        //            ListGrid2 = new ObservableCollection<ItemModel>();
        //            _ListGrid_Temp12 = new ObservableCollection<ItemModel>();
        //        }
        //    }
        //    if (App.Current.Properties["ShowStockMain"] != null)
        //    {                SelectedItem = App.Current.Properties["ShowStockMain"] as ItemModel;

        //        _ListGrid_Temp12.Add(new ItemModel
        //        {
        //            GODOWN = SelectedItem.GODOWN,
        //            OPN_QNT = SelectedItem.OPN_QNT,
        //            BARCODE = SelectedItem.BARCODE,
        //            SALES_UNIT = SelectedItem.SALES_UNIT,
        //        });
        //        App.Current.Properties["ShowStockMain"] = null;
        //        // App.Current.Properties["StockGrid"] = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;
        //    }
        //    else if (App.Current.Properties["StockGridList"] != null)
        //    {

        //        var stocklist = App.Current.Properties["StockGridList"] as ObservableCollection<ItemModel>;
        //        if (Select_BarCode != "" && Select_BarCode != null)
        //        {
        //            var str12 = (from a in stocklist where a.BARCODE == Select_BarCode select a).FirstOrDefault();


        //            _ListGrid_Temp12.Add(new ItemModel
        //            {
        //                GODOWN = str12.GODOWN,
        //                OPN_QNT = str12.OPN_QNT,
        //                BARCODE = str12.BARCODE,
        //                SALES_UNIT = str12.SALES_UNIT,
        //            });
        //        }
        //        if (ShowStockList.ItemNameReff != null)
        //        {
        //            if (ShowStockList.ItemNameReff.Text != null && ShowStockList.ItemNameReff.Text != "")
        //            {
        //                var str12 = (from a in stocklist where a.ITEM_NAME == ShowStockList.ItemNameReff.Text select a).FirstOrDefault();


        //                _ListGrid_Temp12.Add(new ItemModel
        //                {
        //                    GODOWN = str12.GODOWN,
        //                    OPN_QNT = str12.OPN_QNT,
        //                    BARCODE = str12.BARCODE,
        //                    SALES_UNIT = str12.SALES_UNIT,
        //                });
        //                ShowStockList.ItemNameReff.Text = "";
        //            }
        //        }

        //        ShowStockList.ListGridRefr.ItemsSource = _ListGrid_Temp12;

        //    }

        //    App.Current.Properties["StockGrid"] = _ListGrid_Temp12;
        //    ListGrid2 = _ListGrid_Temp12;
        //    ITEM_NAME = SelectedItem.ITEM_NAME;
        //    BARCODE = SelectedItem.BARCODE;
        //    SALES_PRICE = Convert.ToString(SelectedItem.SALES_PRICE);

        //    // App.Current.Properties["ShowStockMain"] = null;
        //}




        private ICommand _TabChangeName { get; set; }
        public ICommand TabChangeName
        {
            get
            {
                if (_TabChangeName == null)
                {
                    _TabChangeName = new DelegateCommand(TabChange_Name);
                }
                return _TabChangeName;
            }
        }

        public void TabChange_Name()
        {
            GetSearchName(comp);
            _SEARCHITEM_NAME = "";
            SEARCHITEM_NAME = "";
        }


        private int _ITEM_ID;
        public int ITEM_ID
        {
            get
            {
                return SelectedItem.ITEM_ID;
            }
            set
            {
                SelectedItem.ITEM_ID = value;

                if (SelectedItem.ITEM_ID != value)
                {
                    SelectedItem.ITEM_ID = value;
                    OnPropertyChanged("ITEM_ID");
                }
            }
        }

        //private string _StockItemName;
        //public string StockItemName
        //{
        //    get
        //    {
        //        return _StockItemName;
        //    }
        //    set
        //    {
        //        _StockItemName = value;
        //        if (_StockItemName != null && _StockItemName != "")
        //        {
        //            GetStockItem();
        //        }
        //        OnPropertyChanged("StockItemName");

        //    }
        //}

        private string _COMP_MOBILE { get; set; }
        public string COMP_MOBILE
        {
            get { return _COMP_MOBILE; }
            set
            {
                _COMP_MOBILE = value;


                if (_COMP_MOBILE != value)
                {
                    _COMP_MOBILE = value;
                    OnPropertyChanged("COMP_MOBILE");
                }

            }

        }
        private decimal? _VATAMOUNTT { get; set; }
        public decimal? VATAMOUNTT
        {
            get { return _VATAMOUNTT; }
            set
            {
                _VATAMOUNTT = value;
                OnPropertyChanged("VATAMOUNTT");
            }

        }
        private bool _DiscountAmt { get; set; }
        public bool DiscountAmt
        {
            get { return _DiscountAmt; }
            set
            {
                _DiscountAmt = value;
                if (_DiscountAmt == true)
                {
                    DiscountAmtIsEnabel = true;
                    DiscountPerIsEnable = false;
                    DisPer = 0;
                }
                OnPropertyChanged("DiscountAmt");
            }
        }
        private bool _DisCountPer { get; set; }
        public bool DisCountPer
        {
            get { return _DisCountPer; }
            set
            {
                _DisCountPer = value;
                if (_DisCountPer == true)
                {
                    DiscountAmtIsEnabel = false;
                    DiscountPerIsEnable = true;
                    DisAmt = 0;
                }
                OnPropertyChanged("DisCountPer");
            }
        }
        private string _COMP_PHONE { get; set; }
        public string COMP_PHONE
        {
            get { return _COMP_PHONE; }
            set
            {
                _COMP_PHONE = value;


                if (_COMP_PHONE != value)
                {
                    _COMP_PHONE = value;
                    OnPropertyChanged("COMP_PHONE");
                }

            }

        }
        private string _CustomerMain { get; set; }
        public string CustomerMain
        {
            get { return _CustomerMain; }
            set
            {
                _CustomerMain = value;
                App.Current.Properties["CustomerManiPage"] = _CustomerMain;
                OnPropertyChanged("CustomerMain");
            }

        }
        private string _COMP_PIN { get; set; }
        public string COMP_PIN
        {
            get { return _COMP_PIN; }
            set
            {
                _COMP_PIN = value;


                if (_COMP_PIN != value)
                {
                    _COMP_PIN = value;
                    OnPropertyChanged("COMP_PIN");
                }

            }

        }
        private string _COMP_DATE { get; set; }
        public string COMP_DATE
        {
            get { return _COMP_DATE; }
            set
            {
                _COMP_DATE = value;
                App.Current.Properties["CurrentInvoiceDate"] = _COMP_DATE;
                OnPropertyChanged("COMP_DATE");
            }

        }
        //Tax Details
        private string _TaxName { get; set; }
        public string TaxName
        {
            get { return _TaxName; }
            set
            {
                _TaxName = value;
                OnPropertyChanged("TaxName");


            }

        }
        private string _TotalTax { get; set; }
        public string TotalTax
        {
            get { return _TotalTax; }
            set
            {
                _TotalTax = value;
                //OnPropertyChanged("TotalTax");
                NotifyPropertyChanged("TotalTax");
            }

        }
        private decimal _TaxValue { get; set; }
        public decimal TaxValue
        {
            get { return _TaxValue; }
            set
            {
                _TaxValue = value;
                OnPropertyChanged("TaxValue");
            }

        }

        private ICommand _TaxDetails { get; set; }
        public ICommand TaxDetails
        {
            get
            {
                if (_TaxDetails == null)
                {
                    _TaxDetails = new DelegateCommand(Tax_Details);
                }
                return _TaxDetails;
            }
        }
        //private RelayCommand TaxDetailsGrid;
        //private ICommand _TaxDetailsGrid { get; set; }
        //public ICommand TaxDetailsGrid
        //{
        //    get
        //    {
        //        if (TaxDetailsGrid == null)
        //            TaxDetailsGrid = new RelayCommand(param => this.ColumnShowHide(param));
        //        return TaxDetailsGrid;
        //    }
        //}
        //public void ColumnShowHide(object sender)
        //{

        //    CheckBox ckh = sender as CheckBox;
        //    DependencyObject currParent = VisualTreeHelper.GetParent(ckh);
        //    if (ckh.IsChecked == true)
        //    {
        //        while (!(currParent is UserControl))
        //        {
        //            currParent = VisualTreeHelper.GetParent(currParent);
        //        }
        //        // Items gft = currParent as Items;
        //        (currParent as Items).dataGrid1.Columns[1].Visibility = Visibility.Hidden;

        //    }
        //    else
        //    {
        //        while (!(currParent is UserControl))
        //        {
        //            currParent = VisualTreeHelper.GetParent(currParent);
        //        }
        //        (currParent as Items).dataGrid1.Columns[1].Visibility = Visibility.Visible;
        //    }

        //}
        public async void Tax_Details()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.ITEM_ID != null && SelectedItem.ITEM_ID != 0)
                {

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    HttpResponseMessage response = client.GetAsync("api/ItemAPI/TaxPaidValue?id=" + SelectedItem.ITEM_ID + "").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                        if (data.Length > 0)
                        {
                            for (int i = 0; i < data.Length; i++)
                            {
                                SelectedItem.ITEM_ID = data[i].ITEM_ID;
                                SelectedItem.ITEM_NAME = data[i].ITEM_NAME;
                                SelectedItem.BARCODE = data[i].BARCODE;
                                SelectedItem.SALES_PRICE = data[i].SALES_PRICE;
                                SelectedItem.BUSINESS_LOC = data[i].BUSINESS_LOC;
                                SelectedItem.GODOWN = data[i].GODOWN;
                                SelectedItem.COMPANY_NAME = data[i].COMPANY_NAME;
                                SelectedItem.DATE = data[i].DATE;
                                SelectedItem.BUSINESS_LOC = data[i].BUSINESS_LOC;
                                SelectedItem.OPENING_STOCK_ID = data[i].OPENING_STOCK_ID;

                                SelectedItem.Current_Qty = data[i].Current_Qty;
                                SelectedItem.OPN_QNT = data[i].OPN_QNT;
                                SelectedItem.TAX_PAID_ID = data[i].TAX_PAID_ID;
                                SelectedItem.TAX_PAID_NAME = data[i].TAX_PAID_NAME;
                                SelectedItem.TAX_PAID = data[i].TAX_PAID;
                                SelectedItem.TAX_COLLECTED_NAME = data[i].TAX_COLLECTED_NAME;
                                SelectedItem.TAX_COLLECTED_ID = data[i].TAX_COLLECTED_ID;
                                SelectedItem.SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX;
                            }
                        }
                    }
                    App.Current.Properties["TaxDetails"] = SelectedItem;
                    Taxdetails sost = new Taxdetails();
                    sost.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Select Item");

                }
            }
            else
            {
                MessageBox.Show("Please Restart Your Application");

            }
        }




        //public async void GetStockItem()
        //{
        //    ObservableCollection<ItemModel> _ListGrid_Temp12 = new ObservableCollection<ItemModel>();
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.Timeout = new TimeSpan(500000000000);
        //    HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
        //        if (data.Length > 0)
        //        {
        //            for (int i = 0; i < data.Length; i++)
        //            {
        //                _ListGrid_Temp.Add(new ItemModel
        //                {
        //                    // ITEM_ID = ItemId,
        //                    Discount = data[i].Discount,
        //                    SLNO = i + 1,
        //                    ITEM_NAME = data[i].ITEM_NAME,
        //                    ITEM_ID = data[i].ITEM_ID,
        //                    BARCODE = data[i].BARCODE,
        //                    ACCESSORIES_KEYWORD = data[i].ACCESSORIES_KEYWORD,
        //                    CATAGORY_ID = data[i].CATAGORY_ID,
        //                    ITEM_DESCRIPTION = data[i].ITEM_DESCRIPTION,
        //                    ITEM_INVOICE_ID = data[i].ITEM_INVOICE_ID,
        //                    ITEM_PRICE = data[i].ITEM_PRICE,
        //                    ITEM_PRODUCT_ID = data[i].ITEM_PRODUCT_ID,
        //                    PURCHASE_UNIT = data[i].PURCHASE_UNIT,
        //                    PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE,
        //                    SALES_PRICE = data[i].SALES_PRICE,
        //                    SALES_UNIT = data[i].SALES_UNIT,
        //                    SEARCH_CODE = data[i].SEARCH_CODE,
        //                    TAX_COLLECTED = data[i].TAX_COLLECTED,
        //                    TAX_PAID = data[i].SALES_PRICE,
        //                    ALLOW_PURCHASE_ON_ESHOP = data[i].ALLOW_PURCHASE_ON_ESHOP,
        //                    CATEGORY_NAME = data[i].CATEGORY_NAME,
        //                    DISPLAY_INDEX = data[i].DISPLAY_INDEX,
        //                    INCLUDE_TAX = data[i].INCLUDE_TAX,
        //                    ITEM_GROUP_NAME = data[i].ITEM_GROUP_NAME,
        //                    ITEM_UNIQUE_NAME = data[i].ITEM_UNIQUE_NAME,
        //                    Current_Qty = data[i].Current_Qty,
        //                    OPN_QNT = data[i].OPN_QNT,
        //                    GODOWN = data[i].GODOWN,
        //                    REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE,
        //                    SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX,
        //                });

        //            }
        //          //  App.Current.Properties["StockGrid"] = _ListGrid_Temp;

        //        }
        //        if (StockItemName != null && StockItemName != "")
        //        {
        //            var itemToRemove1 = (from m in _ListGrid_Temp where m.ITEM_NAME == StockItemName select m).ToList();
        //            ObservableCollection<ItemModel> myCollection = new ObservableCollection<ItemModel>(itemToRemove1);
        //            if (itemToRemove1 != null)
        //            {
        //                x = x + 1;
        //                _ListGrid_Temp12.Add(new ItemModel
        //                {
        //                    Discount = itemToRemove1[0].Discount,
        //                    SLNO = x,
        //                    ITEM_NAME = itemToRemove1[0].ITEM_NAME,
        //                    ITEM_ID = itemToRemove1[0].ITEM_ID,
        //                    BARCODE = itemToRemove1[0].BARCODE,
        //                    ACCESSORIES_KEYWORD = itemToRemove1[0].ACCESSORIES_KEYWORD,
        //                    CATAGORY_ID = itemToRemove1[0].CATAGORY_ID,
        //                    ITEM_DESCRIPTION = itemToRemove1[0].ITEM_DESCRIPTION,
        //                    ITEM_INVOICE_ID = itemToRemove1[0].ITEM_INVOICE_ID,
        //                    ITEM_PRICE = itemToRemove1[0].ITEM_PRICE,
        //                    ITEM_PRODUCT_ID = itemToRemove1[0].ITEM_PRODUCT_ID,
        //                    KEYWORD = itemToRemove1[0].KEYWORD,
        //                    MRP = itemToRemove1[0].MRP,
        //                    PURCHASE_UNIT = itemToRemove1[0].PURCHASE_UNIT,
        //                    GODOWN = itemToRemove1[0].GODOWN,
        //                    PURCHASE_UNIT_PRICE = itemToRemove1[0].PURCHASE_UNIT_PRICE,
        //                    SALES_PRICE = itemToRemove1[0].SALES_PRICE,
        //                    SALES_UNIT = itemToRemove1[0].SALES_UNIT,
        //                    SEARCH_CODE = itemToRemove1[0].SEARCH_CODE,
        //                    TAX_COLLECTED = itemToRemove1[0].TAX_COLLECTED,
        //                    TAX_PAID = itemToRemove1[0].SALES_PRICE,
        //                    ALLOW_PURCHASE_ON_ESHOP = itemToRemove1[0].ALLOW_PURCHASE_ON_ESHOP,
        //                    CATEGORY_NAME = itemToRemove1[0].CATEGORY_NAME,
        //                    DISPLAY_INDEX = itemToRemove1[0].DISPLAY_INDEX,
        //                    INCLUDE_TAX = itemToRemove1[0].INCLUDE_TAX,
        //                    ITEM_GROUP_NAME = itemToRemove1[0].ITEM_GROUP_NAME,
        //                    ITEM_UNIQUE_NAME = itemToRemove1[0].ITEM_UNIQUE_NAME,
        //                    Current_Qty = itemToRemove1[0].Current_Qty,
        //                    OPN_QNT = itemToRemove1[0].OPN_QNT,
        //                    REGIONAL_LANGUAGE = itemToRemove1[0].REGIONAL_LANGUAGE,
        //                    SALES_PRICE_BEFOR_TAX = itemToRemove1[0].SALES_PRICE_BEFOR_TAX,
        //                });
        //                //ListGrid2 = _ListGrid_Temp12;
        //                //ITEM_NAME = ListGrid2[0].ITEM_NAME;
        //                //BARCODE = ListGrid2[0].BARCODE;
        //                //SALES_PRICE = Convert.ToString(ListGrid2[0].SALES_PRICE);
        //            }
        //        }
        //    }
        //}




        private ICommand _ChangeQnt { get; set; }
        public ICommand ChangeQnt
        {
            get
            {
                if (_ChangeQnt == null)
                {
                    _ChangeQnt = new DelegateCommand(Change_Qnt);
                }
                return _ChangeQnt;
            }
            set
            {

                OnPropertyChanged("ChangeQnt");
            }

        }

        public async void Change_Qnt()
        {

            if (SelectedItem != null)
            {


                if (SelectedItem.ITEM_ID != null && SelectedItem.ITEM_ID != 0)
                {
                    App.Current.Properties["InvoiceQnt"] = 1;
                    App.Current.Properties["CODE"] = SelectedItem.BARCODE;
                    App.Current.Properties["UNIT"] = SelectedItem.SALES_UNIT;
                    App.Current.Properties["PRICE"] = SelectedItem.SALES_PRICE;
                    App.Current.Properties["ITEM_ID"] = SelectedItem.ITEM_ID;
                    App.Current.Properties["Qnt"] = SelectedItem.Current_Qty;
                    //   App.Current.Properties["Action"] = "";
                    if (SelectedItem.Discount == 0)
                    {
                        ChangeQnt tt = new ChangeQnt();
                        Application.Current.MainWindow = tt;
                        tt.ShowDialog();
                        ListGrid = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;
                        TotalBottom();
                    }
                    else
                    {
                        MessageBox.Show("Can't add this Item atfrist delete discount or delete item then you chnage Quentity", "Error");
                    }

                }
                else
                {
                    MessageBox.Show("Select Item");
                }
            }
            else
            {
                MessageBox.Show("Select Item");
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

        private ObservableCollection<ItemModel> _ItemData;
        public ObservableCollection<ItemModel> ItemData
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

        private ItemModel _selectedItem;
        //public ItemModel SelectedItem
        //{
        //    get { return _selectedItem; }
        //    set
        //    {
        //        if (_selectedItem != value)
        //        {
        //            _selectedItem = value;
        //            // App.Current.Properties["SelectedItem"] = SelectedItem;
        //            NotifyPropertyChanged("SelectedItem");
        //        }
        //    }
        //}


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




        private string _BARCODE;
        public string BARCODE
        {
            get
            {
                return _BARCODE;
            }
            set
            {
                _BARCODE = value;
                OnPropertyChanged("BARCODE");
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

        private string _BussLocation;
        public string BussLocation
        {
            get
            {
                return _BussLocation;
            }
            set
            {
                _BussLocation = value;
                App.Current.Properties["BussLoactionMain"] = _BussLocation;
                OnPropertyChanged("BussLocation");
            }
        }

        private int? _Current_Qty;
        public int? Current_Qty
        {
            get
            {
                return _Current_Qty;
            }
            set
            {
                _Current_Qty = value;
                OnPropertyChanged("Current_Qty");

            }
        }

        private decimal _TAX_PAID;
        public decimal TAX_PAID
        {
            get
            {
                return SelectedItem.TAX_PAID;
            }
            set
            {
                SelectedItem.TAX_PAID = value;
                if (SelectedItem.TAX_PAID != value)
                {
                    SelectedItem.TAX_PAID = value;
                    OnPropertyChanged("TAX_PAID");
                }
            }
        }

        private string _ITEM_NAME;
        public string ITEM_NAME
        {
            get
            {
                return _ITEM_NAME;
            }
            set
            {
                _ITEM_NAME = value;
                OnPropertyChanged("ITEM_NAME");

            }
        }
        private int _comp;
        public int comp
        {
            get
            {
                return _comp;
            }
            set
            {
                _comp = value;

                if (_comp != value)
                {
                    _comp = value;
                    OnPropertyChanged("comp");
                }
            }
        }

        private string _SEARCHITEM_NAME { get; set; }
        public string SEARCHITEM_NAME
        {
            get
            {
                return _SEARCHITEM_NAME;
            }
            set
            {
                _SEARCHITEM_NAME = value;

                //if (SEARCHITEM_NAME != "" && SEARCHITEM_NAME != null)
                //{

                //    GetSearchName(comp);
                //    _SEARCHITEM_NAME = "";
                //}
                OnPropertyChanged("SEARCHITEM_NAME");

            }
        }

        private int _SEARCHITEM_STOCK { get; set; }
        public int SEARCHITEM_STOCK
        {
            get
            {
                return _SEARCHITEM_STOCK;
            }
            set
            {
                if (_SEARCHITEM_STOCK != value)
                {
                    _SEARCHITEM_STOCK = value;

                    if (SEARCHITEM_STOCK != 0 && SEARCHITEM_STOCK != null)
                    {
                        GetBarcodeSearch(comp);
                    }
                    else
                    {
                        GetBarcodeSearch(comp);
                    }
                    OnPropertyChanged("SEARCHITEM_STOCK");
                }
            }
        }

        private string _SEARCHITEM_CODE { get; set; }
        public string SEARCHITEM_CODE
        {
            get
            {
                return _SEARCHITEM_CODE;
            }
            set
            {
                if (_SEARCHITEM_CODE != value)
                {
                    _SEARCHITEM_CODE = value;

                    if (SEARCHITEM_CODE != "" && SEARCHITEM_CODE != null)
                    {
                        GetBarcodeSearch(comp);
                    }
                    else
                    {
                        GetBarcodeSearch(comp);
                    }
                    OnPropertyChanged("SEARCHITEM_CODE");
                }
            }
        }
        #endregion

        #region PickUp/Hold Invoice

        public ICommand _PickInvoice;
        public ICommand PickInvoice
        {
            get
            {
                if (_PickInvoice == null)
                {
                    _PickInvoice = new DelegateCommand(Pick_Invoice);
                    //App.Current.Properties["Grid"] = _ListGrid_Temp;
                }
                return _PickInvoice;
            }

        }
        public void Pick_Invoice()
        {
            if (App.Current.Properties["Grid"] != null)
            {
                if (App.Current.Properties["DataGrid"] != null)
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure? ", "Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        App.Current.Properties["Action"] = "PickVoid";

                        // ObservableCollection<ItemModel> _hft = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;
                        ObservableCollection<ItemModel> _hft = App.Current.Properties["Grid"] as ObservableCollection<ItemModel>;
                        
                        ListGrid = _hft;
                        Main.ListGridRef.ItemsSource = null;
                        App.Current.Properties["DataGrid"] = _hft;
                        Main.ListGridRef.ItemsSource = _hft;
                        App.Current.Properties["Grid"] = null;
                        ExPay = true;
                        PayNow = true;
                        TOTAL_ITEM = ListGrid.Count;
                        GROSSAMT = 0;
                        for (int i = 0; i < ListGrid.Count; i++)
                        {
                            GROSSAMT = Convert.ToDecimal(ListGrid[i].Total + GROSSAMT);
                            App.Current.Properties["CurrentGrosAmount"] = GROSSAMT;
                            NETAMT = Convert.ToDecimal((ListGrid[i].SALES_PRICE) * (ListGrid[i].Current_Qty) + NETAMT);
                        }
                        QUNT_TOTAL = 0;
                        foreach (var qunt in ListGrid)
                        {
                            QUNT_TOTAL = qunt.OPN_QNT + QUNT_TOTAL;
                        }
                    }
                }
                else
                {
                    App.Current.Properties["Action"] = "PickVoid";
                    ObservableCollection<ItemModel> _hft = App.Current.Properties["Grid"] as ObservableCollection<ItemModel>;
                    ListGrid = _hft;
                    Main.ListGridRef.ItemsSource = null;
                    App.Current.Properties["DataGrid"] = _hft;
                    Main.ListGridRef.ItemsSource = _hft;
                    App.Current.Properties["Grid"] = null;
                    HttpClient client = new HttpClient();
                    // This instance has already started one or more requests. Properties can only be modified before sending the first request.
                    // _opr.NAME = SelectedCustomer.NAME;
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/PickInvoice?id=" + _hft[0].ITEM_ID + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show(" Pick Invoice");
                        //Cancel_Customer();
                        //ModalService.NavigateTo(new CustomerList(), delegate(bool returnValue) { });
                    }
                    if (ListGrid != null)
                    {

                        TotalBottom();
                        //ExPay = true;
                        //PayNow = true;
                        //TOTAL_ITEM = ListGrid.Count;
                        //GROSSAMT = 0;
                        //for (int i = 0; i < ListGrid.Count; i++)
                        //{
                        //    GROSSAMT = Convert.ToDecimal(ListGrid[i].Total + GROSSAMT);
                        //    App.Current.Properties["CurrentGrosAmount"] = GROSSAMT;
                        //    NETAMT = Convert.ToDecimal((ListGrid[i].SALES_PRICE) * (ListGrid[i].OPN_QNT) + NETAMT);
                        //}
                        //QUNT_TOTAL = 0;
                        //foreach (var qunt in ListGrid)
                        //{
                        //    QUNT_TOTAL = qunt.OPN_QNT + QUNT_TOTAL;
                        //}
                    }
                }
            }
            else
            {
                MessageBox.Show("Item Can't Blank");
            }
        }
             private InvoiceModel _SelectInvoice;
        public InvoiceModel SelectInvoice
        {

            get { return _SelectInvoice; }
            set
            {

                if (_SelectInvoice != value)
                {
                    _SelectInvoice = value;
                    OnPropertyChanged("SelectInvoice");
                }

            }

        }
        private ICommand _VoidInvoice;
        public ICommand VoidInvoice
        {
            get
            {
                if (_VoidInvoice == null)
                {
                    _VoidInvoice = new DelegateCommand(Void_Invoice);

                }
                return _VoidInvoice;
            }

        }
        public async void Void_Invoice()
        {
            
            if (ListGrid != null && ListGrid.Count > 0)
            {
                var dataItemID = SelectedItem.ITEM_ID;
                App.Current.Properties["Grid"] = SelectedItem;
                App.Current.Properties["Action"] = "Void";
                App.Current.Properties["Grid"] = App.Current.Properties["DataGrid"] ;
                var VoidGrid = new ObservableCollection<ItemModel>();
                Main.ListGridRef.ItemsSource = null;
                App.Current.Properties["DataGrid"] = null;
                ListGrid = new ObservableCollection<ItemModel>();
                SelectedItem = new ItemModel();
                NETAMT = 0;
                GROSSAMT = 0;
                QUNT_TOTAL = 0;
                TOTAL_ITEM = 0;
                ExPay = false;
                PayNow = false;
                VATAMOUNTT = 0;
                DiscountTotal = 0;

                if (App.Current.Properties["Grid"] != null)
                {
                    if (App.Current.Properties["Grid"] != null)
                    {
                        ObservableCollection<ItemModel> dataItem = App.Current.Properties["Grid"] as ObservableCollection<ItemModel>;
                        //HttpClient client = new HttpClient();
                        //client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                        //client.DefaultRequestHeaders.Accept.Add(
                        //    new MediaTypeWithQualityHeaderValue("application/json"));
                        ////client.Timeout = new TimeSpan(500000000000);
                        //HttpResponseMessage response = client.GetAsync("api/ItemAPI/CheckItemQuantity?id=" + dataItem[0].ITEM_ID + "").Result;
                        //   if (response.IsSuccessStatusCode)
                        //{
                        //    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                        //    if (data.Length > 0)
                        //    {
                        //        for (int i = 0; i < data.Length; i++)
                        //        {
                        //            SelectedItem.ITEM_ID = data[i].ITEM_ID;
                        //            SelectedItem.ITEM_NAME = data[i].ITEM_NAME;
                        //            SelectedItem.BARCODE = data[i].BARCODE;
                        //            SelectedItem.SALES_PRICE = data[i].SALES_PRICE;
                        //            SelectedItem.BUSINESS_LOC = data[i].BUSINESS_LOC;
                        //            SelectedItem.GODOWN = data[i].GODOWN;
                        //            SelectedItem.COMPANY_NAME = data[i].COMPANY_NAME;
                        //            SelectedItem.DATE = data[i].DATE;
                        //            SelectedItem.BUSINESS_LOC = data[i].BUSINESS_LOC;
                        //            SelectedItem.OPENING_STOCK_ID = data[i].OPENING_STOCK_ID;

                        //            SelectedItem.Current_Qty = data[i].Current_Qty;
                        //            SelectedItem.OPN_QNT = data[i].OPN_QNT;
                        //            SelectedItem.TAX_PAID_ID = data[i].TAX_PAID_ID;
                        //            SelectedItem.TAX_PAID_NAME = data[i].TAX_PAID_NAME;
                        //            SelectedItem.TAX_PAID = data[i].TAX_PAID;
                        //            SelectedItem.TAX_COLLECTED_NAME = data[i].TAX_COLLECTED_NAME;
                        //            SelectedItem.TAX_COLLECTED_ID = data[i].TAX_COLLECTED_ID;
                        //            SelectedItem.SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX;
                        //        }
                        //    }
                        //}

                        HttpClient client = new HttpClient();
                        // This instance has already started one or more requests. Properties can only be modified before sending the first request.
                        // _opr.NAME = SelectedCustomer.NAME;
                        client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        client.Timeout = new TimeSpan(500000000000);
                        HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/HoldInvoice?id=" + dataItemID + "").Result;
                        if (response.StatusCode.ToString() == "OK")
                        {
                            MessageBox.Show("Invoice Hold Successfuly");
                            //Cancel_Customer();
                            //ModalService.NavigateTo(new CustomerList(), delegate(bool returnValue) { });
                        }
                        App.Current.Properties["TaxDetails"] = SelectedItem;
                        //Taxdetails sost = new Taxdetails();
                        //sost.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Select Item");

                    }
                }
            }
            else
            {
                MessageBox.Show("Item Can't Blank");
            }
        }
        #endregion


        private ICommand _CancelInvoice;
        public ICommand CancelInvoice
        {
            get
            {
                if (_CancelInvoice == null)
                {
                    _CancelInvoice = new DelegateCommand(Cancel_Invoice);

                }
                return _CancelInvoice;
            }

        }
        private RelayCommand taxDetailsGridShow;
        public ICommand TaxDetailsGridShow
        {
            get
            {
                if (taxDetailsGridShow == null)
                    taxDetailsGridShow = new RelayCommand(param => this.ColumnShowHide(param));
                return taxDetailsGridShow;
            }
        }
        public void ColumnShowHide(object sender)
        {

            Button ckh = sender as Button;
            DependencyObject currParent = VisualTreeHelper.GetParent(ckh);
            while (!(currParent is Window))
            {
                // this porstion will be get error. Bes value null.
                currParent = VisualTreeHelper.GetParent(currParent);
            }
            (currParent as Main).dataGrid1.Columns[6].Visibility = Visibility.Visible;
            (currParent as Main).dataGrid1.Columns[7].Visibility = Visibility.Visible;
            //(currParent as Main).dataGrid1.Columns[8].Visibility = Visibility.Visible;
            //(currParent as Main).dataGrid1.Columns[7].Visibility = Visibility.Visible;
            ShowTaxVisibility = "Collapsed";
            HideTaxVisibility = "Visible";

        }
        private RelayCommand taxDetailsGridHide;
        public ICommand TaxDetailsGridHide
        {
            get
            {
                if (taxDetailsGridHide == null)
                    taxDetailsGridHide = new RelayCommand(param => this.ColumnTaxHide(param));
                return taxDetailsGridHide;
            }
        }
        public void ColumnTaxHide(object sender)
        {
            Button ckh = sender as Button;
            DependencyObject currParent = VisualTreeHelper.GetParent(ckh);
            while (!(currParent is Window))
            {
                // this porstion will be get error. Bes value null.
                currParent = VisualTreeHelper.GetParent(currParent);
            }
            (currParent as Main).dataGrid1.Columns[6].Visibility = Visibility.Hidden;
            (currParent as Main).dataGrid1.Columns[7].Visibility = Visibility.Hidden;
            //(currParent as Main).dataGrid1.Columns[8].Visibility = Visibility.Hidden;
            //(currParent as Main).dataGrid1.Columns[7].Visibility = Visibility.Hidden;
            ShowTaxVisibility = "Visible";
            HideTaxVisibility = "Collapsed";
        }

        public void Cancel_Invoice()
        {
            if (ListGrid != null && ListGrid.Count > 0)
            {
                App.Current.Properties["DataGrid"] = null;
                App.Current.Properties["DataGridL"] = null;
                ObservableCollection<ItemModel> listmodelgrd = new ObservableCollection<ItemModel>();
                Main.ListGridRef.ItemsSource = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;
                NETAMT = 0;
                GROSSAMT = 0;
                QUNT_TOTAL = 0;
                TOTAL_ITEM = 0;
                ExPay = false;
                PayNow = false;
                VATAMOUNTT = 0;
                BussLocation = null;
                CustomerMain = null;
                DiscountTotal = 0;
                SelectedItem = new ItemModel();
            }
            else
            {
                MessageBox.Show("Item Can't Blank");
            }
        }
        private ICommand _ChangePriceItem;
        public ICommand ChangePriceItem
        {
            get
            {
                if (_ChangePriceItem == null)
                {
                    _ChangePriceItem = new DelegateCommand(ChangePrice_ItemData);

                }
                return _ChangePriceItem;
            }

        }

        public void ChangePrice_ItemData()
        {
            
            //App.Current.Properties["ItemName"] = SelectedItem.ITEM_NAME;
            //App.Current.Properties["ItemCode"] = SelectedItem.ITEM_ID;
            //App.Current.Properties["OldMRP"] = SelectedItem.MRP;
            //App.Current.Properties["OldSalesPrice"] = SelectedItem.SALES_PRICE;
            //App.Current.Properties["OldBarcode"] = SelectedItem.BARCODE;
            //App.Current.Properties["OldBusinessName"] = SelectedItem.BUSINESS_LOC;

            if (SelectedItem.ITEM_ID != 0 && SelectedItem.ITEM_ID != null)
            {
                App.Current.Properties["SelectData"] = SelectedItem;
                ChangeItemPrice IA = new ChangeItemPrice();
                IA.Show();
            }           
             else
                {
                MessageBox.Show("Select Item");
            }
        }
        private string _ItemCode;

        public string ItemCode
        {
            get
            {
                return _ItemCode;
            }
            set
            {
                _ItemCode = value;
                OnPropertyChanged("ItemCode");
            }
        }
        private string _ItemName;
        public string ItemName
        {
            get
            {
                return _ItemName;
            }
            set
            {
                _ItemName = value;
                OnPropertyChanged("ItemName");
            }
        }
        private string _ItemOldMRP;
        public string ItemOldMRP
        {
            get
            {
                return _ItemOldMRP;
            }
            set
            {
                _ItemOldMRP = value;
                OnPropertyChanged("ItemOldMRP");
            }
        }
        private string _ItemOldPrice;
        public string ItemOldPrice
        {
            get
            {
                return _ItemOldPrice;
            }
            set
            {
                _ItemOldPrice = value;
                OnPropertyChanged("ItemOldPrice");
            }
        }

        private List<ItemModel> _ItemDataList;
        public List<ItemModel> ItemDataList
        {
            get { return _ItemDataList; }
            set
            {
                if (_ItemDataList != value)
                {
                    _ItemDataList = value;
                }
            }

        }

        
        private ICommand _RemoveItem;
        public ICommand RemoveItem
        {
            get
            {
                if (_RemoveItem == null)
                {
                    _RemoveItem = new DelegateCommand(Remove_Item);

                }
                return _RemoveItem;
            }

        }

        public void Remove_Item()
        {
            try
            {
                if (SelectedItem.ITEM_ID != 0 && SelectedItem.ITEM_ID != null)
                {

                    var itemToRemove = ListGrid.Single(r => r.ITEM_ID == SelectedItem.ITEM_ID);
                    ListGrid.Remove(itemToRemove);
                    Main.ListGridRef.ItemsSource = ListGrid;
                    TotalBottom();
                    App.Current.Properties["DataGrid"] = ListGrid;
                    LoadGrid();

                }
                else
                {
                    MessageBox.Show("Select Removed Item");
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            // ObservableCollection<ItemModel> _ListGrid = new ObservableCollection<ItemModel>();


        }

       
        private ICommand _StockItem;
        public ICommand StockItem
        {
            get
            {
                if (_StockItem == null)
                {
                    _StockItem = new DelegateCommand(Stock_Item);

                }
                return _StockItem;
            }
        }
        public void Stock_Item()
        {
            App.Current.Properties["DataGrid"] = null;
            GetItem(comp);
            Main.ListGridRef.ItemsSource = ListGrid;
        }


        private RelayCommand _EstimateInvc;
        public ICommand EstimateInvc
        {
            get
            {
                if (_EstimateInvc == null)
                    _EstimateInvc = new RelayCommand(param => this.Estimate_Invc(param));
                return _EstimateInvc;
            }
        }

        bool clickk = false;
        public void Estimate_Invc(object sender)
        {
            Button ckh = sender as Button;
            DependencyObject currParent = VisualTreeHelper.GetParent(ckh);
            while (!(currParent is Window))
            {
                currParent = VisualTreeHelper.GetParent(currParent);
            }
            PrintDialog dialog = new PrintDialog();
            dialog.PrintVisual((currParent as Main).dataGrid1, "ItemPrint");
        }

        //private ICommand _EstimateInvc;
        //public ICommand EstimateInvc
        //{
        //    get
        //    {
        //        if (_EstimateInvc == null)
        //        {
        //            _EstimateInvc = new DelegateCommand(Estimate_Invc);

        //        }
        //        return _EstimateInvc;
        //    }

        //}

        //public void Estimate_Invc()
        //{
        //    Main printPage = new Main();
        //    PrintDialog dialog = new PrintDialog();
        //    dialog.PrintVisual(printPage.dataGrid1, "ItemPrint");
        //}

        //public void Estimate_Invc()
        //{
        //    EstimateInvoice printPage = new EstimateInvoice();
        //    //  PrintDocument pd = new PrintDocument();  

        //    //SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    //if (saveFileDialog.ShowDialog() == true)
        //    //{
        //    PrintDialog dialog = new PrintDialog();
        //    // dialog.PrintVisual(hgy.grid11, "fffff");
        //    // saveFileDialog.OpenFile();
        //    dialog.PrintVisual(printPage.grid11, "ItemPrint");

        //    //}
        //}

        ObservableCollection<CompanyModel> _ListGrid_Temp1 = new ObservableCollection<CompanyModel>();
        private List<CompanyModel> _CompanyData;
        public List<CompanyModel> CompanyData
        {
            get { return _CompanyData; }
            set
            {
                if (_CompanyData != value)
                {
                    _CompanyData = value;
                }
            }
        }

        CompanyModel[] data1 = null;
        public HttpResponseMessage response1;
        public async Task<ObservableCollection<CompanyModel>> GetCompany()
        {
            try
            {

                string ftpServerIP = "115.115.196.30";
                string ftpUserID = "suvendu";
                string ftpPassword = "Passw0rd";

                string filename = "ftp://" + ftpServerIP + "//Upload//";


                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                //response = client.GetAsync("api/CompanyAPI/GetCompany?id=" + Id + "").Result;
                response1 = client.GetAsync("api/CompanyAPI/GetCompany").Result;
                if (response1.IsSuccessStatusCode)
                {
                    data1 = JsonConvert.DeserializeObject<CompanyModel[]>(await response1.Content.ReadAsStringAsync());
                    CompanyData = new List<CompanyModel>();
                    for (int i = 0; i < 1; i++)
                    {
                        int croId = Convert.ToInt32(data1[i].COMPANY_ID);
                        string NAME = data1[i].NAME;
                        string ADDRESS_1 = data1[i].ADDRESS_1;
                        string ADDRESS_2 = data1[i].ADDRESS_2;
                        string CITY = data1[i].CITY;
                        string img = data1[i].IMAGE_PATH;

                        _ListGrid_Temp1.Add(new CompanyModel
                        {
                            ADDRESS_1 = ADDRESS_1,
                            NAME = NAME,
                            CITY = CITY,
                            ADDRESS_2 = ADDRESS_2,
                            IMAGE_PATH = img
                        });
                        App.Current.Properties["Company_Id"] = Convert.ToInt32(data1[i].COMPANY_ID);
                        App.Current.Properties["Company_Name"] = data1[i].NAME;
                        App.Current.Properties["Company_Email"] = data1[i].EMAIL;
                        App.Current.Properties["Company_Address1"] = data1[i].ADDRESS_1;
                        App.Current.Properties["Company_Address2"] = data1[i].ADDRESS_2;
                        App.Current.Properties["Company_Mobile"] = data1[i].MOBILE_NUMBER;
                        App.Current.Properties["Company_Phone"] = data1[i].PHONE_NUMBER;
                        App.Current.Properties["Company_Pin"] = data1[i].PIN;


                        if (data1[i].IMAGE_PATH != null)
                        {


                            var fr = filename + data1[i].IMAGE_PATH;

                            var imageFile = new System.IO.FileInfo(data1[i].IMAGE_PATH);
                            string file = imageFile.Name;
                            var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                            // get your 'Uploaded' folder
                            var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));
                            if (!dir.Exists)
                                dir.Create();
                            // Copy file to your folder
                            //imageFile.CopyTo(System.IO.Path.Combine(dir.FullName, file), true);
                            string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);

                            FtpDown(path1, file);

                            IMAGE_PATH1 = new BitmapImage(new Uri(path1));
                            App.Current.Properties["ItemViewImg"] = IMAGE_PATH1;
                        }
                    }

                }
                //ListGrid = _ListGrid_Temp;
                //return new ObservableCollection<CompanyModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {


            }
            //ListGrid = _ListGrid_Temp;
            return new ObservableCollection<CompanyModel>(_ListGrid_Temp1);
        }


        public static void FtpDown(string sourceFile, string targetFile)
        {
            try
            {
                string ftpServerIP = "115.115.196.30";
                string ftpUserID = "suvendu";
                string ftpPassword = "Passw0rd";

                string filename = "ftp://" + ftpServerIP + "//Upload//" + targetFile;
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(filename);
                ftpReq.UseBinary = true;

                ftpReq.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpReq.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                byte[] b = System.IO.File.ReadAllBytes(sourceFile);

                ftpReq.ContentLength = b.Length;
                int d = b.Length;
                using (Stream s = ftpReq.GetRequestStream())
                {
                    s.Write(b, 0, b.Length);
                }

                FtpWebResponse ftpResp = (FtpWebResponse)ftpReq.GetResponse();

                if (ftpResp != null)
                {
                    string responseDesc = ftpResp.StatusDescription;
                }
            }
            catch (Exception ex)
            {
                string responseDesc = ex.ToString();
            }
        }


        private ImageSource _IMAGE_PATH1;
        public ImageSource IMAGE_PATH1
        {
            get { return _IMAGE_PATH1; }
            set
            {
                if (Equals(value, _IMAGE_PATH1)) return;
                _IMAGE_PATH1 = value;
                OnPropertyChanged("IMAGE_PATH1");
            }
        }

        //public async Task<string> GetInvoiceNo()
        //{
        //    string uhy = "";
        //    try
        //    {
        //        string nnnn = "";
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(GlobalData.gblApiAdress);
        //        client.DefaultRequestHeaders.Accept.Add(
        //            new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.Timeout = new TimeSpan(500000000000);
        //        HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceNo/").Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            uhy = await response.Content.ReadAsStringAsync();
        //        }
        //        string dd = Convert.ToString(uhy);
        //        string aaa = dd.Substring(5, 6);
        //        int xx = Convert.ToInt32(aaa);
        //        int noo = Convert.ToInt32(xx + 1);
        //        nnnn = "INV-" + noo.ToString("D6");
        //        INVOICE_NO = nnnn;

        //    }
        //    catch (Exception ex)
        //    { }

        //    return uhy;


        //}


        List<CustomerAutoCompleteListModel> autoCustModelList = new List<CustomerAutoCompleteListModel>();

        List<ItemNameAutoListModel> autoItemNameList = new List<ItemNameAutoListModel>();
        List<SearchCodeAutoListModel> autoScrCodeList = new List<SearchCodeAutoListModel>();

        ObservableCollection<ItemModel> AddListGrid = new ObservableCollection<ItemModel>();

        ObservableCollection<ItemModel> Load_Item = new ObservableCollection<ItemModel>();




        public async Task<ObservableCollection<ItemModel>> GetBarcodeSearch(int comp)
        {
            try
            {
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                ItemData = new ObservableCollection<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            _ListGrid_Temp.Add(new ItemModel
                            {
                                // ITEM_ID = ItemId,
                                Discount = data[i].Discount,
                                SLNO = i + 1,
                                ITEM_NAME = data[i].ITEM_NAME,
                                ITEM_ID = data[i].ITEM_ID,
                                BARCODE = data[i].BARCODE,
                                ACCESSORIES_KEYWORD = data[i].ACCESSORIES_KEYWORD,
                                CATAGORY_ID = data[i].CATAGORY_ID,
                                ITEM_DESCRIPTION = data[i].ITEM_DESCRIPTION,
                                ITEM_INVOICE_ID = data[i].ITEM_INVOICE_ID,
                                ITEM_PRICE = data[i].ITEM_PRICE,
                                ITEM_PRODUCT_ID = data[i].ITEM_PRODUCT_ID,
                                KEYWORD = data[i].KEYWORD,
                                MRP = data[i].MRP,
                                PURCHASE_UNIT = data[i].PURCHASE_UNIT,
                                PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE,
                                SALES_PRICE = data[i].SALES_PRICE,
                                SALES_UNIT = data[i].SALES_UNIT,
                                SEARCH_CODE = data[i].SEARCH_CODE,
                                TAX_COLLECTED = data[i].TAX_COLLECTED,
                                TAX_PAID = data[i].SALES_PRICE,
                                ALLOW_PURCHASE_ON_ESHOP = data[i].ALLOW_PURCHASE_ON_ESHOP,
                                CATEGORY_NAME = data[i].CATEGORY_NAME,
                                DISPLAY_INDEX = data[i].DISPLAY_INDEX,
                                INCLUDE_TAX = data[i].INCLUDE_TAX,
                                ITEM_GROUP_NAME = data[i].ITEM_GROUP_NAME,
                                ITEM_UNIQUE_NAME = data[i].ITEM_UNIQUE_NAME,
                                Current_Qty = 1,
                                OPN_QNT = data[i].OPN_QNT,
                                REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE,
                                SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX,
                                TaxName = data[i].TaxName,
                                TaxValue = data[i].TaxValue,
                                //Total = ((decimal)(data[i].OPN_QNT) * (data[i].SALES_PRICE)) + (data[i].SALES_PRICE - data[i].PURCHASE_UNIT_PRICE),

                                Total = ((decimal)(data[i].OPN_QNT) * (data[i].SALES_PRICE)),
                            });

                        }
                        App.Current.Properties["DataGridPuzzale"] = _ListGrid_Temp;
                    }

                    if (App.Current.Properties["DataGridL"] != null)
                    {
                        AddListGrid = App.Current.Properties["DataGridL"] as ObservableCollection<ItemModel>;
                    }
                    else
                    {
                        AddListGrid = new ObservableCollection<ItemModel>();
                    }

                    if (Select_BarCode != null && Select_BarCode != "")
                    {
                        //App.Current.Properties["ManualBarcode"] = Select_BarCode;
                        var itemToRemove = (from m in _ListGrid_Temp where m.BARCODE == Select_BarCode select m).ToList();
                        ObservableCollection<ItemModel> myCollection = new ObservableCollection<ItemModel>(itemToRemove);
                        var Item1 = (from a in AddListGrid where a.BARCODE == Select_BarCode select a).FirstOrDefault();

                        int opqunt = 0;
                        if (itemToRemove.Count != 0)
                        {
                            opqunt = (int)itemToRemove.ElementAt(0).Current_Qty;
                        }
                        else
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Do you want to add this Item?", "Add Item", System.Windows.MessageBoxButton.YesNo);
                            if (messageBoxResult == MessageBoxResult.Yes)
                            {
                                ItemAdd obj = new ItemAdd();
                                obj.ShowDialog();
                            }
                        }

                        if (Item1 != null)
                        {
                            if (Item1.Discount == 0)
                            {


                                AddListGrid.Remove(Item1);
                                x = x + 1;
                                AddListGrid.Add(new ItemModel
                                {
                                    Discount = Item1.Discount,
                                    SLNO = x,
                                    ITEM_NAME = Item1.ITEM_NAME,
                                    ITEM_ID = Item1.ITEM_ID,
                                    BARCODE = Item1.BARCODE,
                                    ACCESSORIES_KEYWORD = Item1.ACCESSORIES_KEYWORD,
                                    CATAGORY_ID = Item1.CATAGORY_ID,
                                    ITEM_DESCRIPTION = Item1.ITEM_DESCRIPTION,
                                    ITEM_INVOICE_ID = Item1.ITEM_INVOICE_ID,
                                    ITEM_PRICE = Item1.ITEM_PRICE,
                                    ITEM_PRODUCT_ID = Item1.ITEM_PRODUCT_ID,
                                    KEYWORD = Item1.KEYWORD,
                                    MRP = Item1.MRP,
                                    PURCHASE_UNIT = Item1.PURCHASE_UNIT,
                                    PURCHASE_UNIT_PRICE = Item1.PURCHASE_UNIT_PRICE,
                                    SALES_PRICE = Item1.SALES_PRICE,
                                    SALES_UNIT = Item1.SALES_UNIT,
                                    SEARCH_CODE = Item1.SEARCH_CODE,
                                    TAX_COLLECTED = Item1.TAX_COLLECTED,
                                    TAX_PAID = Item1.SALES_PRICE,
                                    ALLOW_PURCHASE_ON_ESHOP = Item1.ALLOW_PURCHASE_ON_ESHOP,
                                    CATEGORY_NAME = Item1.CATEGORY_NAME,
                                    DISPLAY_INDEX = Item1.DISPLAY_INDEX,
                                    INCLUDE_TAX = Item1.INCLUDE_TAX,
                                    ITEM_GROUP_NAME = Item1.ITEM_GROUP_NAME,
                                    ITEM_UNIQUE_NAME = Item1.ITEM_UNIQUE_NAME,
                                    Current_Qty = opqunt + (int)Item1.Current_Qty,
                                    OPN_QNT = Item1.OPN_QNT,
                                    REGIONAL_LANGUAGE = Item1.REGIONAL_LANGUAGE,
                                    SALES_PRICE_BEFOR_TAX = Item1.SALES_PRICE_BEFOR_TAX,
                                    TaxName = Item1.TaxName,
                                    TaxValue = Item1.TaxValue,
                                    Total = ((decimal)(Item1.Current_Qty) * (Item1.SALES_PRICE)) + Item1.SALES_PRICE,
                                });

                                Main.ListQnt.Text = (Item1.Current_Qty + Convert.ToInt32(Main.ListQnt.Text)).ToString();

                                var GrossAmt = Main.GrossamountReff.Text;
                                var valgrss = ((decimal)(Item1.Current_Qty) * (SelectedItem.SALES_PRICE));
                                var grodd = valgrss + Convert.ToDecimal(GrossAmt);

                                Main.GrossamountReff.Text = grodd.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Can't add this Item atfrist delete discount or delete item then you chnage Quentity", "Error");
                            }
                        }
                        else
                        {

                            foreach (var item in myCollection)
                            {
                                x = x + 1;
                                AddListGrid.Add(new ItemModel
                                {
                                    
                                    Discount = item.Discount,
                                    SLNO = x,
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
                                    TAX_PAID = item.SALES_PRICE,
                                    ALLOW_PURCHASE_ON_ESHOP = item.ALLOW_PURCHASE_ON_ESHOP,
                                    CATEGORY_NAME = item.CATEGORY_NAME,
                                    DISPLAY_INDEX = item.DISPLAY_INDEX,
                                    INCLUDE_TAX = item.INCLUDE_TAX,
                                    ITEM_GROUP_NAME = item.ITEM_GROUP_NAME,
                                    ITEM_UNIQUE_NAME = item.ITEM_UNIQUE_NAME,
                                    OPN_QNT = item.OPN_QNT,
                                    Current_Qty = item.Current_Qty,
                                    REGIONAL_LANGUAGE = item.REGIONAL_LANGUAGE,
                                    SALES_PRICE_BEFOR_TAX = item.SALES_PRICE_BEFOR_TAX,
                                    TaxName = item.TaxName,
                                    TaxValue = item.TaxValue,
                                    Total = ((decimal)(item.Current_Qty) * (item.SALES_PRICE)),
                                    
                                });

                            }
                        }
                        if (AddListGrid.Count > 0)
                        {
                            for (int i = 0; i < AddListGrid.Count; i++)
                            {
                                if (AddListGrid[i].OPN_QNT < AddListGrid[i].Current_Qty)
                                {
                                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("This Item not Avable. Do you went to add this item?", "Add Item", System.Windows.MessageBoxButton.YesNo);
                                    if (messageBoxResult != MessageBoxResult.Yes)
                                    {
                                        var RemoveItem = (from a in AddListGrid where a.ITEM_ID == AddListGrid[i].ITEM_ID select a).FirstOrDefault();
                                        AddListGrid.Remove(RemoveItem);
                                        x = x + 1;
                                        AddListGrid.Add(new ItemModel
                                        {
                                            Discount = Item1.Discount,
                                            SLNO = x,
                                            ITEM_NAME = Item1.ITEM_NAME,
                                            ITEM_ID = Item1.ITEM_ID,
                                            BARCODE = Item1.BARCODE,
                                            ACCESSORIES_KEYWORD = Item1.ACCESSORIES_KEYWORD,
                                            CATAGORY_ID = Item1.CATAGORY_ID,
                                            ITEM_DESCRIPTION = Item1.ITEM_DESCRIPTION,
                                            ITEM_INVOICE_ID = Item1.ITEM_INVOICE_ID,
                                            ITEM_PRICE = Item1.ITEM_PRICE,
                                            ITEM_PRODUCT_ID = Item1.ITEM_PRODUCT_ID,
                                            KEYWORD = Item1.KEYWORD,
                                            MRP = Item1.MRP,
                                            PURCHASE_UNIT = Item1.PURCHASE_UNIT,
                                            PURCHASE_UNIT_PRICE = Item1.PURCHASE_UNIT_PRICE,
                                            SALES_PRICE = Item1.SALES_PRICE,
                                            SALES_UNIT = Item1.SALES_UNIT,
                                            SEARCH_CODE = Item1.SEARCH_CODE,
                                            TAX_COLLECTED = Item1.TAX_COLLECTED,
                                            TAX_PAID = Item1.SALES_PRICE,
                                            ALLOW_PURCHASE_ON_ESHOP = Item1.ALLOW_PURCHASE_ON_ESHOP,
                                            CATEGORY_NAME = Item1.CATEGORY_NAME,
                                            DISPLAY_INDEX = Item1.DISPLAY_INDEX,
                                            INCLUDE_TAX = Item1.INCLUDE_TAX,
                                            ITEM_GROUP_NAME = Item1.ITEM_GROUP_NAME,
                                            ITEM_UNIQUE_NAME = Item1.ITEM_UNIQUE_NAME,
                                            Current_Qty = opqunt + (int)Item1.Current_Qty - 1,
                                            OPN_QNT = Item1.OPN_QNT,
                                            REGIONAL_LANGUAGE = Item1.REGIONAL_LANGUAGE,
                                            SALES_PRICE_BEFOR_TAX = Item1.SALES_PRICE_BEFOR_TAX,
                                            TaxName = Item1.TaxName,
                                            TaxValue = Item1.TaxValue,
                                            Total = ((decimal)(Item1.Current_Qty) * (Item1.SALES_PRICE)) + Item1.SALES_PRICE,
                                        });
                                    }


                                    if (messageBoxResult == MessageBoxResult.Yes)
                                    {
                                        var RemoveItem = (from a in AddListGrid where a.ITEM_ID == AddListGrid[i].ITEM_ID select a).FirstOrDefault();
                                        AddListGrid.Remove(RemoveItem);
                                        x = x + 1;
                                        AddListGrid.Add(new ItemModel
                                        {

                                            Discount = Item1.Discount,
                                            SLNO = x,
                                            ITEM_NAME = Item1.ITEM_NAME,
                                            ITEM_ID = Item1.ITEM_ID,
                                            BARCODE = Item1.BARCODE,
                                            ACCESSORIES_KEYWORD = Item1.ACCESSORIES_KEYWORD,
                                            CATAGORY_ID = Item1.CATAGORY_ID,
                                            ITEM_DESCRIPTION = Item1.ITEM_DESCRIPTION,
                                            ITEM_INVOICE_ID = Item1.ITEM_INVOICE_ID,
                                            ITEM_PRICE = Item1.ITEM_PRICE,
                                            ITEM_PRODUCT_ID = Item1.ITEM_PRODUCT_ID,
                                            KEYWORD = Item1.KEYWORD,
                                            MRP = Item1.MRP,
                                            PURCHASE_UNIT = Item1.PURCHASE_UNIT,
                                            PURCHASE_UNIT_PRICE = Item1.PURCHASE_UNIT_PRICE,
                                            SALES_PRICE = Item1.SALES_PRICE,
                                            SALES_UNIT = Item1.SALES_UNIT,
                                            SEARCH_CODE = Item1.SEARCH_CODE,
                                            TAX_COLLECTED = Item1.TAX_COLLECTED,
                                            TAX_PAID = Item1.SALES_PRICE,
                                            ALLOW_PURCHASE_ON_ESHOP = Item1.ALLOW_PURCHASE_ON_ESHOP,
                                            CATEGORY_NAME = Item1.CATEGORY_NAME,
                                            DISPLAY_INDEX = Item1.DISPLAY_INDEX,
                                            INCLUDE_TAX = Item1.INCLUDE_TAX,
                                            ITEM_GROUP_NAME = Item1.ITEM_GROUP_NAME,
                                            ITEM_UNIQUE_NAME = Item1.ITEM_UNIQUE_NAME,
                                            Current_Qty = opqunt + (int)Item1.Current_Qty,
                                            OPN_QNT = Item1.OPN_QNT,
                                            REGIONAL_LANGUAGE = Item1.REGIONAL_LANGUAGE,
                                            SALES_PRICE_BEFOR_TAX = Item1.SALES_PRICE_BEFOR_TAX,
                                            TaxName = Item1.TaxName,
                                            TaxValue = Item1.TaxValue,
                                            Total = ((decimal)(Item1.Current_Qty) * (Item1.SALES_PRICE)) + Item1.SALES_PRICE,
                                        });
                                    }


                                }
                            }

                        }
                        //}
                        App.Current.Properties["DataGridL"] = AddListGrid;
                        App.Current.Properties["DataGrid"] = AddListGrid;
                        Main.ListGridRef.ItemsSource = AddListGrid;
                        ListGrid = AddListGrid;
                        TotalBottom();
                        //ListGrid = AddListGrid;
                        LoadGrid();

                    }
                    else
                    {
                        Main.ScrRef.Text = "";
                    }
                }



                _Select_BarCode = "";
                Select_BarCode = "";
                SelectedItem = new ItemModel();
                Main.ScrRef.Text = "";
            }
            catch (Exception ex)
            {
                throw;
            }
            return ListGrid;

        }

        public async Task<ObservableCollection<ItemModel>> GetSearchName(int comp)
        {
            try
            {
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                ItemData = new ObservableCollection<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            _ListGrid_Temp.Add(new ItemModel
                            {
                                // ITEM_ID = ItemId,
                                Discount = data[i].Discount,
                                SLNO = i + 1,
                                ITEM_NAME = data[i].ITEM_NAME,
                                ITEM_ID = data[i].ITEM_ID,
                                BARCODE = data[i].BARCODE,
                                ACCESSORIES_KEYWORD = data[i].ACCESSORIES_KEYWORD,
                                CATAGORY_ID = data[i].CATAGORY_ID,
                                ITEM_DESCRIPTION = data[i].ITEM_DESCRIPTION,
                                ITEM_INVOICE_ID = data[i].ITEM_INVOICE_ID,
                                ITEM_PRICE = data[i].ITEM_PRICE,
                                ITEM_PRODUCT_ID = data[i].ITEM_PRODUCT_ID,
                                KEYWORD = data[i].KEYWORD,
                                MRP = data[i].MRP,
                                PURCHASE_UNIT = data[i].PURCHASE_UNIT,
                                PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE,
                                SALES_PRICE = data[i].SALES_PRICE,
                                SALES_UNIT = data[i].SALES_UNIT,
                                SEARCH_CODE = data[i].SEARCH_CODE,
                                TAX_COLLECTED = data[i].TAX_COLLECTED,
                                // TAX_PAID = data[i].SALES_PRICE - data[i].PURCHASE_UNIT_PRICE,
                                TAX_PAID = data[i].SALES_PRICE,
                                ALLOW_PURCHASE_ON_ESHOP = data[i].ALLOW_PURCHASE_ON_ESHOP,
                                CATEGORY_NAME = data[i].CATEGORY_NAME,
                                DISPLAY_INDEX = data[i].DISPLAY_INDEX,
                                INCLUDE_TAX = data[i].INCLUDE_TAX,
                                ITEM_GROUP_NAME = data[i].ITEM_GROUP_NAME,
                                ITEM_UNIQUE_NAME = data[i].ITEM_UNIQUE_NAME,
                                Current_Qty = 1,
                                OPN_QNT = data[i].OPN_QNT,
                                REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE,
                                TaxName = data[i].TaxName,
                                TaxValue = data[i].TaxValue,
                                SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX,
                                //Total = ((decimal)(data[i].OPN_QNT) * (data[i].SALES_PRICE)) + (data[i].SALES_PRICE - data[i].PURCHASE_UNIT_PRICE),

                                Total = ((decimal)(data[i].OPN_QNT) * (data[i].SALES_PRICE)),
                            });

                        }

                    }

                    if (App.Current.Properties["DataGridL"] != null)
                    {
                        AddListGrid = App.Current.Properties["DataGridL"] as ObservableCollection<ItemModel>;
                    }
                    else
                    {
                        AddListGrid = new ObservableCollection<ItemModel>();

                    }

                    if (Main.NameRef1.Text != null && Main.NameRef1.Text != "")
                    {
                        var itemToRemove = (from m in _ListGrid_Temp where m.ITEM_NAME == Main.NameRef1.Text select m).ToList();
                        ObservableCollection<ItemModel> myCollection = new ObservableCollection<ItemModel>(itemToRemove);
                        var Item1 = (from a in AddListGrid where a.ITEM_NAME == Main.NameRef1.Text select a).FirstOrDefault();
                        int opqunt = 0;
                        if (itemToRemove.Count != 0)
                        {
                            opqunt = (int)itemToRemove.ElementAt(0).Current_Qty;
                        }
                        else
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Do you want to add this Item ?", "Add Item", System.Windows.MessageBoxButton.YesNo);
                            if (messageBoxResult == MessageBoxResult.Yes)
                            {
                                ItemAdd obj = new ItemAdd();
                                obj.ShowDialog();
                            }
                        }

                        if (Item1 != null)
                        {
                            if (Item1.Discount == 0)
                            {


                                AddListGrid.Remove(Item1);

                                x = x + 1;
                                AddListGrid.Add(new ItemModel
                                {
                                    Discount = Item1.Discount,
                                    SLNO = x,
                                    ITEM_NAME = Item1.ITEM_NAME,
                                    ITEM_ID = Item1.ITEM_ID,
                                    BARCODE = Item1.BARCODE,
                                    ACCESSORIES_KEYWORD = Item1.ACCESSORIES_KEYWORD,
                                    CATAGORY_ID = Item1.CATAGORY_ID,
                                    ITEM_DESCRIPTION = Item1.ITEM_DESCRIPTION,
                                    ITEM_INVOICE_ID = Item1.ITEM_INVOICE_ID,
                                    ITEM_PRICE = Item1.ITEM_PRICE,
                                    ITEM_PRODUCT_ID = Item1.ITEM_PRODUCT_ID,
                                    KEYWORD = Item1.KEYWORD,
                                    MRP = Item1.MRP,
                                    PURCHASE_UNIT = Item1.PURCHASE_UNIT,
                                    PURCHASE_UNIT_PRICE = Item1.PURCHASE_UNIT_PRICE,
                                    SALES_PRICE = Item1.SALES_PRICE,
                                    SALES_UNIT = Item1.SALES_UNIT,
                                    SEARCH_CODE = Item1.SEARCH_CODE,
                                    TAX_COLLECTED = Item1.TAX_COLLECTED,
                                    TAX_PAID = Item1.SALES_PRICE,
                                    ALLOW_PURCHASE_ON_ESHOP = Item1.ALLOW_PURCHASE_ON_ESHOP,
                                    CATEGORY_NAME = Item1.CATEGORY_NAME,
                                    DISPLAY_INDEX = Item1.DISPLAY_INDEX,
                                    INCLUDE_TAX = Item1.INCLUDE_TAX,
                                    ITEM_GROUP_NAME = Item1.ITEM_GROUP_NAME,
                                    ITEM_UNIQUE_NAME = Item1.ITEM_UNIQUE_NAME,
                                    Current_Qty = opqunt + (int)Item1.Current_Qty,
                                    OPN_QNT = Item1.OPN_QNT,
                                    REGIONAL_LANGUAGE = Item1.REGIONAL_LANGUAGE,
                                    SALES_PRICE_BEFOR_TAX = Item1.SALES_PRICE_BEFOR_TAX,
                                    TaxName = Item1.TaxName,
                                    TaxValue = Item1.TaxValue,
                                    Total = ((decimal)(Item1.Current_Qty) * (Item1.SALES_PRICE)) + Item1.SALES_PRICE,
                                });

                            }
                            else
                            {
                                MessageBox.Show("Can't add this Item atfrist delete discount or delete item then you chnage Quentity", "Error");
                            }
                        }

                        else
                        {
                            foreach (var item in myCollection)
                            {

                                
                                x = x + 1;
                                AddListGrid.Add(new ItemModel
                                {
                                    Discount = item.Discount,
                                    SLNO = x,
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
                                    TAX_PAID = item.SALES_PRICE - item.PURCHASE_UNIT_PRICE,
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
                                    TaxName = item.TaxName,
                                    TaxValue = item.TaxValue,
                                    Total = ((decimal)(item.Current_Qty) * (item.SALES_PRICE)),
                                });
                                Main.ListQnt.Text = (item.Current_Qty + Convert.ToInt32(Main.ListQnt.Text)).ToString();

                                var GrossAmt = Main.GrossamountReff.Text;
                                var valgrss = ((decimal)(item.Current_Qty) * (SelectedItem.SALES_PRICE));
                                var grodd = valgrss + Convert.ToDecimal(GrossAmt);

                                Main.GrossamountReff.Text = grodd.ToString();
                            }
                        }
                        if (AddListGrid.Count > 0)
                        {
                            for (int i = 0; i < AddListGrid.Count; i++)
                            {
                                if (AddListGrid[i].OPN_QNT < AddListGrid[i].Current_Qty)
                                {
                                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("This Item not Avable. Do you went to add this item?", "Add Item", System.Windows.MessageBoxButton.YesNo);
                                    if (messageBoxResult != MessageBoxResult.Yes)
                                    {
                                        var RemoveItem = (from a in AddListGrid where a.ITEM_ID == AddListGrid[i].ITEM_ID select a).FirstOrDefault();
                                        AddListGrid.Remove(RemoveItem);
                                        x = x + 1;
                                        AddListGrid.Add(new ItemModel
                                        {
                                            Discount = Item1.Discount,
                                            SLNO = x,
                                            ITEM_NAME = Item1.ITEM_NAME,
                                            ITEM_ID = Item1.ITEM_ID,
                                            BARCODE = Item1.BARCODE,
                                            ACCESSORIES_KEYWORD = Item1.ACCESSORIES_KEYWORD,
                                            CATAGORY_ID = Item1.CATAGORY_ID,
                                            ITEM_DESCRIPTION = Item1.ITEM_DESCRIPTION,
                                            ITEM_INVOICE_ID = Item1.ITEM_INVOICE_ID,
                                            ITEM_PRICE = Item1.ITEM_PRICE,
                                            ITEM_PRODUCT_ID = Item1.ITEM_PRODUCT_ID,
                                            KEYWORD = Item1.KEYWORD,
                                            MRP = Item1.MRP,
                                            PURCHASE_UNIT = Item1.PURCHASE_UNIT,
                                            PURCHASE_UNIT_PRICE = Item1.PURCHASE_UNIT_PRICE,
                                            SALES_PRICE = Item1.SALES_PRICE,
                                            SALES_UNIT = Item1.SALES_UNIT,
                                            SEARCH_CODE = Item1.SEARCH_CODE,
                                            TAX_COLLECTED = Item1.TAX_COLLECTED,
                                            TAX_PAID = Item1.SALES_PRICE,
                                            ALLOW_PURCHASE_ON_ESHOP = Item1.ALLOW_PURCHASE_ON_ESHOP,
                                            CATEGORY_NAME = Item1.CATEGORY_NAME,
                                            DISPLAY_INDEX = Item1.DISPLAY_INDEX,
                                            INCLUDE_TAX = Item1.INCLUDE_TAX,
                                            ITEM_GROUP_NAME = Item1.ITEM_GROUP_NAME,
                                            ITEM_UNIQUE_NAME = Item1.ITEM_UNIQUE_NAME,
                                            Current_Qty = opqunt + (int)Item1.Current_Qty - 1,
                                            OPN_QNT = Item1.OPN_QNT,
                                            REGIONAL_LANGUAGE = Item1.REGIONAL_LANGUAGE,
                                            SALES_PRICE_BEFOR_TAX = Item1.SALES_PRICE_BEFOR_TAX,
                                            TaxName = Item1.TaxName,
                                            TaxValue = Item1.TaxValue,

                                            Total = ((decimal)(Item1.Current_Qty) * (Item1.SALES_PRICE)) + Item1.SALES_PRICE,
                                        });
                                    }
                                }
                            }
                        }
                    }
                    App.Current.Properties["DataGridL"] = AddListGrid;
                    App.Current.Properties["DataGrid"] = AddListGrid;
                    Main.ListGridRef.ItemsSource = AddListGrid;
                    ListGrid = AddListGrid;
                    TotalBottom();
                    LoadGrid();
                }
                if (App.Current.Properties["SelectedItem"] != null)
                {
                    SelectedItem = App.Current.Properties["SelectedItem"] as ItemModel;
                    WeightQnt = Convert.ToInt32(SelectedItem.Current_Qty);
                }



            }
            catch (Exception ex)
            {

            }
            return ListGrid;
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
                // WeightPrice = SelectedItem.WeightQnt * SelectedItem.SALES_PRICE;
                OnPropertyChanged("WeightQnt");
            }
        }


        public async Task<ObservableCollection<ItemModel>> GetSearchCode(int comp)
        {

            try
            {
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                ItemData = new ObservableCollection<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            _ListGrid_Temp.Add(new ItemModel
                            {
                                // ITEM_ID = ItemId,
                                Discount = data[i].Discount,
                                SLNO = i + 1,
                                ITEM_NAME = data[i].ITEM_NAME,
                                ITEM_ID = data[i].ITEM_ID,
                                BARCODE = data[i].BARCODE,
                                ACCESSORIES_KEYWORD = data[i].ACCESSORIES_KEYWORD,
                                CATAGORY_ID = data[i].CATAGORY_ID,
                                ITEM_DESCRIPTION = data[i].ITEM_DESCRIPTION,
                                ITEM_INVOICE_ID = data[i].ITEM_INVOICE_ID,
                                ITEM_PRICE = data[i].ITEM_PRICE,
                                ITEM_PRODUCT_ID = data[i].ITEM_PRODUCT_ID,
                                KEYWORD = data[i].KEYWORD,
                                MRP = data[i].MRP,
                                PURCHASE_UNIT = data[i].PURCHASE_UNIT,
                                PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE,
                                SALES_PRICE = data[i].SALES_PRICE,
                                SALES_UNIT = data[i].SALES_UNIT,
                                SEARCH_CODE = data[i].SEARCH_CODE,
                                TAX_COLLECTED = data[i].TAX_COLLECTED,
                                TAX_PAID = data[i].SALES_PRICE,
                                ALLOW_PURCHASE_ON_ESHOP = data[i].ALLOW_PURCHASE_ON_ESHOP,
                                CATEGORY_NAME = data[i].CATEGORY_NAME,
                                DISPLAY_INDEX = data[i].DISPLAY_INDEX,
                                INCLUDE_TAX = data[i].INCLUDE_TAX,
                                ITEM_GROUP_NAME = data[i].ITEM_GROUP_NAME,
                                ITEM_UNIQUE_NAME = data[i].ITEM_UNIQUE_NAME,
                                Current_Qty = 1,
                                OPN_QNT = data[i].OPN_QNT,
                                REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE,
                                SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX,
                                TaxName = data[i].TaxName,
                                TaxValue = data[i].TaxValue,
                                //Total = ((decimal)(data[i].OPN_QNT) * (data[i].SALES_PRICE)) + (data[i].SALES_PRICE - data[i].PURCHASE_UNIT_PRICE),

                                Total = ((decimal)(data[i].OPN_QNT) * (data[i].SALES_PRICE)),
                            });

                        }

                    }

                    if (App.Current.Properties["DataGridL"] != null)
                    {
                        AddListGrid = App.Current.Properties["DataGridL"] as ObservableCollection<ItemModel>;
                    }
                    else
                    {
                        AddListGrid = new ObservableCollection<ItemModel>();
                    }

                    if (Main.ScrRef.Text != null && Main.ScrRef.Text != "")
                    {
                        var itemToRemove = (from m in _ListGrid_Temp where m.SEARCH_CODE == Main.ScrRef.Text select m).ToList();
                        ObservableCollection<ItemModel> myCollection = new ObservableCollection<ItemModel>(itemToRemove);
                        var Item1 = (from a in AddListGrid where a.SEARCH_CODE == Main.ScrRef.Text select a).FirstOrDefault();

                        //MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Do you want to add this Item " + Main.ScrRef.Text + "?", "Add Item", System.Windows.MessageBoxButton.YesNo);
                        // if (messageBoxResult == MessageBoxResult.Yes)
                        // {
                        int opqunt = 0;
                        if (itemToRemove.Count != 0)
                        {
                            opqunt = (int)itemToRemove.ElementAt(0).Current_Qty;
                        }
                        else
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Do you want to add this Item " + Main.ScrRef.Text + "?", "Add Item", System.Windows.MessageBoxButton.YesNo);
                            if (messageBoxResult == MessageBoxResult.Yes)
                            {
                                ItemAdd obj = new ItemAdd();
                                obj.ShowDialog();
                            }
                        }

                        if (Item1 != null)
                        {
                            if (Item1.Discount == 0)
                            {


                                AddListGrid.Remove(Item1);
                                x = x + 1;
                                AddListGrid.Add(new ItemModel
                                {
                                    Discount = Item1.Discount,
                                    SLNO = x,
                                    ITEM_NAME = Item1.ITEM_NAME,
                                    ITEM_ID = Item1.ITEM_ID,
                                    BARCODE = Item1.BARCODE,
                                    ACCESSORIES_KEYWORD = Item1.ACCESSORIES_KEYWORD,
                                    CATAGORY_ID = Item1.CATAGORY_ID,
                                    ITEM_DESCRIPTION = Item1.ITEM_DESCRIPTION,
                                    ITEM_INVOICE_ID = Item1.ITEM_INVOICE_ID,
                                    ITEM_PRICE = Item1.ITEM_PRICE,
                                    ITEM_PRODUCT_ID = Item1.ITEM_PRODUCT_ID,
                                    KEYWORD = Item1.KEYWORD,
                                    MRP = Item1.MRP,
                                    PURCHASE_UNIT = Item1.PURCHASE_UNIT,
                                    PURCHASE_UNIT_PRICE = Item1.PURCHASE_UNIT_PRICE,
                                    SALES_PRICE = Item1.SALES_PRICE,
                                    SALES_UNIT = Item1.SALES_UNIT,
                                    SEARCH_CODE = Item1.SEARCH_CODE,
                                    TAX_COLLECTED = Item1.TAX_COLLECTED,
                                    TAX_PAID = Item1.SALES_PRICE,
                                    ALLOW_PURCHASE_ON_ESHOP = Item1.ALLOW_PURCHASE_ON_ESHOP,
                                    CATEGORY_NAME = Item1.CATEGORY_NAME,
                                    DISPLAY_INDEX = Item1.DISPLAY_INDEX,
                                    INCLUDE_TAX = Item1.INCLUDE_TAX,
                                    ITEM_GROUP_NAME = Item1.ITEM_GROUP_NAME,
                                    ITEM_UNIQUE_NAME = Item1.ITEM_UNIQUE_NAME,
                                    Current_Qty = opqunt + (int)Item1.Current_Qty,
                                    OPN_QNT = Item1.OPN_QNT,
                                    REGIONAL_LANGUAGE = Item1.REGIONAL_LANGUAGE,
                                    SALES_PRICE_BEFOR_TAX = Item1.SALES_PRICE_BEFOR_TAX,
                                    TaxName = Item1.TaxName,
                                    TaxValue = Item1.TaxValue,
                                    Total = ((decimal)(Item1.Current_Qty) * (Item1.SALES_PRICE)) + Item1.SALES_PRICE,
                                });
                            }
                            else
                            {
                                MessageBox.Show("Can't add this Item atfrist delete discount or delete item then you chnage Quentity", "Error");
                            }
                        }
                        else
                        {
                            foreach (var item in myCollection)
                            {
                                x = x + 1;
                                AddListGrid.Add(new ItemModel
                                {
                                    Discount = item.Discount,
                                    SLNO = x,
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
                                    TAX_PAID = item.SALES_PRICE,
                                    ALLOW_PURCHASE_ON_ESHOP = item.ALLOW_PURCHASE_ON_ESHOP,
                                    CATEGORY_NAME = item.CATEGORY_NAME,
                                    DISPLAY_INDEX = item.DISPLAY_INDEX,
                                    INCLUDE_TAX = item.INCLUDE_TAX,
                                    ITEM_GROUP_NAME = item.ITEM_GROUP_NAME,
                                    ITEM_UNIQUE_NAME = item.ITEM_UNIQUE_NAME,
                                    OPN_QNT = item.OPN_QNT,
                                    Current_Qty = item.Current_Qty,
                                    REGIONAL_LANGUAGE = item.REGIONAL_LANGUAGE,
                                    SALES_PRICE_BEFOR_TAX = item.SALES_PRICE_BEFOR_TAX,
                                    TaxName = item.TaxName,
                                    TaxValue = item.TaxValue,
                                    Total = ((decimal)(item.Current_Qty) * (item.SALES_PRICE)),
                                });

                                Main.ListQnt.Text = (item.Current_Qty + Convert.ToInt32(Main.ListQnt.Text)).ToString();

                                var GrossAmt = Main.GrossamountReff.Text;
                                var valgrss = ((decimal)(item.Current_Qty) * (SelectedItem.SALES_PRICE));
                                var grodd = valgrss + Convert.ToDecimal(GrossAmt);

                                Main.GrossamountReff.Text = grodd.ToString();
                            }

                            if (AddListGrid.Count > 0)
                            {
                                for (int i = 0; i < AddListGrid.Count; i++)
                                {
                                    if (AddListGrid[i].OPN_QNT < AddListGrid[i].Current_Qty)
                                    {
                                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("This Item not Avable. Do you went to add this item?", "Add Item", System.Windows.MessageBoxButton.YesNo);
                                        if (messageBoxResult != MessageBoxResult.Yes)
                                        {
                                            var RemoveItem = (from a in AddListGrid where a.ITEM_ID == AddListGrid[i].ITEM_ID select a).FirstOrDefault();
                                            AddListGrid.Remove(RemoveItem);
                                            x = x + 1;
                                            AddListGrid.Add(new ItemModel
                                            {
                                                Discount = Item1.Discount,
                                                SLNO = x,
                                                ITEM_NAME = Item1.ITEM_NAME,
                                                ITEM_ID = Item1.ITEM_ID,
                                                BARCODE = Item1.BARCODE,
                                                ACCESSORIES_KEYWORD = Item1.ACCESSORIES_KEYWORD,
                                                CATAGORY_ID = Item1.CATAGORY_ID,
                                                ITEM_DESCRIPTION = Item1.ITEM_DESCRIPTION,
                                                ITEM_INVOICE_ID = Item1.ITEM_INVOICE_ID,
                                                ITEM_PRICE = Item1.ITEM_PRICE,
                                                ITEM_PRODUCT_ID = Item1.ITEM_PRODUCT_ID,
                                                KEYWORD = Item1.KEYWORD,
                                                MRP = Item1.MRP,
                                                PURCHASE_UNIT = Item1.PURCHASE_UNIT,
                                                PURCHASE_UNIT_PRICE = Item1.PURCHASE_UNIT_PRICE,
                                                SALES_PRICE = Item1.SALES_PRICE,
                                                SALES_UNIT = Item1.SALES_UNIT,
                                                SEARCH_CODE = Item1.SEARCH_CODE,
                                                TAX_COLLECTED = Item1.TAX_COLLECTED,
                                                TAX_PAID = Item1.SALES_PRICE,
                                                ALLOW_PURCHASE_ON_ESHOP = Item1.ALLOW_PURCHASE_ON_ESHOP,
                                                CATEGORY_NAME = Item1.CATEGORY_NAME,
                                                DISPLAY_INDEX = Item1.DISPLAY_INDEX,
                                                INCLUDE_TAX = Item1.INCLUDE_TAX,
                                                ITEM_GROUP_NAME = Item1.ITEM_GROUP_NAME,
                                                ITEM_UNIQUE_NAME = Item1.ITEM_UNIQUE_NAME,
                                                Current_Qty = opqunt + (int)Item1.Current_Qty - 1,
                                                OPN_QNT = Item1.OPN_QNT,
                                                REGIONAL_LANGUAGE = Item1.REGIONAL_LANGUAGE,
                                                SALES_PRICE_BEFOR_TAX = Item1.SALES_PRICE_BEFOR_TAX,
                                                TaxName = Item1.TaxName,
                                                TaxValue = Item1.TaxValue,
                                                Total = ((decimal)(Item1.Current_Qty) * (Item1.SALES_PRICE)) + Item1.SALES_PRICE,
                                            });
                                        }
                                    }
                                }

                            }
                        }
                        //}
                        App.Current.Properties["DataGridL"] = AddListGrid;
                        App.Current.Properties["DataGrid"] = AddListGrid;
                        Main.ListGridRef.ItemsSource = AddListGrid;
                        ListGrid = AddListGrid;
                        TotalBottom();
                        ListGrid = AddListGrid;
                        LoadGrid();
                    }
                    else
                    {
                        Main.ScrRef.Text = "";
                    }
                }
                _Select_BarCode = "";
                Select_BarCode = "";
                SelectedItem = new ItemModel();
                Main.ScrRef.Text = "";
            }
            catch (Exception ex)
            {
                throw;
            }
            return ListGrid;

        }

        private decimal _NETAMT;
        public decimal NETAMT
        {
            get
            {
                return _NETAMT;
            }
            set
            {

                _NETAMT = value;


                OnPropertyChanged("NETAMT");

            }
        }

        private decimal _GROSSAMT;
        public decimal GROSSAMT
        {
            get
            {
                return _GROSSAMT;
            }
            set
            {

                _GROSSAMT = value;
                OnPropertyChanged("GROSSAMT");

            }
        }

        private int? _QUNT_TOTAL;
        public int? QUNT_TOTAL
        {
            get
            {
                return _QUNT_TOTAL;
            }
            set
            {
                _QUNT_TOTAL = value;
                OnPropertyChanged("QUNT_TOTAL");

            }
        }

        private string _BarcodeShow;
        public string BarcodeShow
        {
            get
            {
                return _BarcodeShow;
            }
            set
            {
                _BarcodeShow = value;
                OnPropertyChanged("BarcodeShow");

            }
        }

        private int _TOTAL_ITEM;
        public int TOTAL_ITEM
        {
            get
            {
                return _TOTAL_ITEM;
            }
            set
            {
                _TOTAL_ITEM = value;
                OnPropertyChanged("TOTAL_ITEM");

            }
        }

        private ICommand _RowSelectQNTChagne;
        public ICommand RowSelectQNTChagne
        {
            get
            {
                if (_RowSelectQNTChagne == null)
                {
                    if (App.Current.Properties["CODE"] == null)
                    {
                        _RowSelectQNTChagne = new DelegateCommand(RowSelect_QNTChagne);
                    }

                }
                return _RowSelectQNTChagne;
            }

        }

        public void RowSelect_QNTChagne()
        {
            if (SelectedItem != null)
            {

                SelectedItem.NETAMT = (decimal)((SelectedItem.Current_Qty) * (SelectedItem.SALESPRICE));
                var id = SelectedItem.ITEM_ID;
                App.Current.Properties["SelectedItem"] = SelectedItem;
            }

        }

        private decimal _Discount;
        public decimal Discount
        {
            get
            {
                return SelectedItem.Discount;
            }
            set
            {

                SelectedItem.Discount = value;


                OnPropertyChanged("Discount");

            }
        }

        private decimal _Total;
        public decimal Total
        {
            get
            {
                return SelectedItem.Total;
            }
            set
            {
                SelectedItem.Total = value;
                OnPropertyChanged("Total");

            }
        }

        // public ICommand ChangeCommand

        public async Task<ObservableCollection<ItemModel>> GetItemForAutoList(int comp)
        {
            try
            {
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                ItemData = new ObservableCollection<ItemModel>();
                if (App.Current.Properties["DataGrid"] == null)
                {


                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                        if (data.Length > 0)
                        {


                            for (int i = 0; i < data.Length; i++)
                            {
                                _ListGrid_Temp.Add(new ItemModel
                                {
                                    // ITEM_ID = ItemId,
                                    ITEM_NAME = data[i].ITEM_NAME,
                                    ITEM_ID = data[i].ITEM_ID,
                                    BARCODE = data[i].BARCODE,
                                    ACCESSORIES_KEYWORD = data[i].ACCESSORIES_KEYWORD,
                                    CATAGORY_ID = data[i].CATAGORY_ID,
                                    ITEM_DESCRIPTION = data[i].ITEM_DESCRIPTION,
                                    ITEM_INVOICE_ID = data[i].ITEM_INVOICE_ID,
                                    ITEM_PRICE = data[i].ITEM_PRICE,
                                    ITEM_PRODUCT_ID = data[i].ITEM_PRODUCT_ID,
                                    KEYWORD = data[i].KEYWORD,
                                    MRP = data[i].MRP,
                                    PURCHASE_UNIT = data[i].PURCHASE_UNIT,
                                    PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE,
                                    SALES_PRICE = data[i].SALES_PRICE,
                                    SALES_UNIT = data[i].SALES_UNIT,
                                    SEARCH_CODE = data[i].SEARCH_CODE,
                                    TAX_COLLECTED = data[i].TAX_COLLECTED,
                                    TAX_PAID = data[i].TAX_PAID,
                                    ALLOW_PURCHASE_ON_ESHOP = data[i].ALLOW_PURCHASE_ON_ESHOP,
                                    CATEGORY_NAME = data[i].CATEGORY_NAME,
                                    DISPLAY_INDEX = data[i].DISPLAY_INDEX,
                                    INCLUDE_TAX = data[i].INCLUDE_TAX,
                                    ITEM_GROUP_NAME = data[i].ITEM_GROUP_NAME,
                                    ITEM_UNIQUE_NAME = data[i].ITEM_UNIQUE_NAME,
                                    Current_Qty = data[i].Current_Qty,
                                    OPN_QNT = data[i].OPN_QNT,
                                    REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE,
                                    SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX,
                                    TaxName = data[i].TaxName,
                                    TaxValue = data[i].TaxValue,
                                });

                            }
                            App.Current.Properties["DataGrid"] = _ListGrid_Temp;
                        }

                    }

                }
                else
                {
                    return _ListGrid;
                }
                return ListGrid;

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<ObservableCollection<ItemModel>> GetItem(int comp)
        {
            try
            {
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                ItemData = new ObservableCollection<ItemModel>();
                if (App.Current.Properties["DataGrid"] == null)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                        if (data.Length > 0)
                        {
                            for (int i = 0; i < data.Length; i++)
                            {
                                _ListGrid_Temp.Add(new ItemModel
                                {
                                    // ITEM_ID = ItemId,
                                    ITEM_NAME = data[i].ITEM_NAME,
                                    ITEM_ID = data[i].ITEM_ID,
                                    BARCODE = data[i].BARCODE,
                                    ACCESSORIES_KEYWORD = data[i].ACCESSORIES_KEYWORD,
                                    CATAGORY_ID = data[i].CATAGORY_ID,
                                    ITEM_DESCRIPTION = data[i].ITEM_DESCRIPTION,
                                    ITEM_INVOICE_ID = data[i].ITEM_INVOICE_ID,
                                    ITEM_PRICE = data[i].ITEM_PRICE,
                                    ITEM_PRODUCT_ID = data[i].ITEM_PRODUCT_ID,
                                    KEYWORD = data[i].KEYWORD,
                                    MRP = data[i].MRP,
                                    PURCHASE_UNIT = data[i].PURCHASE_UNIT,
                                    PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE,
                                    SALES_PRICE = data[i].SALES_PRICE,
                                    SALES_UNIT = data[i].SALES_UNIT,
                                    SEARCH_CODE = data[i].SEARCH_CODE,
                                    TAX_COLLECTED = data[i].TAX_COLLECTED,
                                    TAX_PAID = data[i].TAX_PAID,
                                    ALLOW_PURCHASE_ON_ESHOP = data[i].ALLOW_PURCHASE_ON_ESHOP,
                                    CATEGORY_NAME = data[i].CATEGORY_NAME,
                                    DISPLAY_INDEX = data[i].DISPLAY_INDEX,
                                    INCLUDE_TAX = data[i].INCLUDE_TAX,
                                    ITEM_GROUP_NAME = data[i].ITEM_GROUP_NAME,
                                    ITEM_UNIQUE_NAME = data[i].ITEM_UNIQUE_NAME,
                                    Current_Qty = data[i].Current_Qty,
                                    OPN_QNT = data[i].OPN_QNT,
                                    GODOWN = data[i].GODOWN,
                                    REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE,
                                    SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX,
                                    TaxName = data[i].TaxName,
                                    TaxValue = data[i].TaxValue,
                                });

                            }
                            App.Current.Properties["DataGridPuzzale"] = _ListGrid_Temp;
                            App.Current.Properties["DataGrid"] = _ListGrid_Temp;

                            ListGrid = _ListGrid_Temp;
                        }

                    }

                }
                else
                {
                    Main.ListGridRef.ItemsSource = null;
                    ObservableCollection<ItemModel> listmodelgrd = new ObservableCollection<ItemModel>();
                    Main.ListGridRef.ItemsSource = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;

                    return _ListGrid;

                }
                return ListGrid;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private ObservableCollection<ItemModel> GetEmployeeList()
        {
            // var list = from e in context.Employees select e;
            return new ObservableCollection<ItemModel>(ListGrid);
        }

        public void LoadGrid()
        {
            // GetInvoiceNo();
            var comp = 1;
            COMP_NAME = "Infosolz";
            COMP_ADDRESS1 = "SDF,Sector5";
            COMP_DATE = DateTime.Now.ToString("dd/MMM/yyyy");

        }
        public void LoadCash()
        {
            // GetInvoiceNo();
            var comp = 1;
            CASH_NAME = "Infosolz";
           

        }
        public ICommand _CashRegBusinessLocationClick;
        public ICommand CashRegBusinessLocationClick
        {
            get
            {
                if (_CashRegBusinessLocationClick == null)
                {
                    _CashRegBusinessLocationClick = new DelegateCommand(CashRegBusinessLocationClick_Click);
                }
                return _CashRegBusinessLocationClick;
            }
        }

        public void CashRegBusinessLocationClick_Click()
        {
            //App.Current.Properties["BussCashReg"] = 2;
            App.Current.Properties["SelectData"] = SelectedItem;
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();
            Main.CashRegisterName.Text = CashReg.CashRegName.Text;
            if (App.Current.Properties["CASH_REG_NAME"] != null)
            {
                //InvoicePOS.Views.CashRegister.CashReg.CashRegName.Text = _selectedItem.CASH_REG_NAME;
                //textBox5.Text = "";
                Main.CashRegisterName.Text = CashReg.CashRegName.Text;
                App.Current.Properties["CashRegisterName"] = Main.CashRegisterName.Text;
                //CashRegisterAmountDetails.BusLocationName.Text = CashReg.CashRegName.Text;
            }
        }

        public ICommand _CashRegClick;
        public ICommand CashRegClick
        {
            get
            {
                if (_CashRegClick == null)
                {
                    _CashRegClick = new DelegateCommand(CashRegClick_Click);
                }
                return _CashRegClick;
            }
        }

        public void CashRegClick_Click()
        {
            App.Current.Properties["CashReg"] = 2;
            Window_CashRegList IA = new Window_CashRegList();
            IA.Show();

        }

        public ICommand _ChangeBussinessLocation;
        public ICommand ChangeBussinessLocation
        {
            get
            {
                if (_ChangeBussinessLocation == null)
                {
                    _ChangeBussinessLocation = new DelegateCommand(ChangeBussiness_Location);
                }
                return _ChangeBussinessLocation;
            }
        }
        public void ChangeBussiness_Location()
        {
            if(App.Current.Properties["SelectedItem"] != null)
            {
                ChangeBussinessLocationWarningMessage IA = new ChangeBussinessLocationWarningMessage();
                IA.Show();
                _ListGrid.Clear();
            }
            else
            {
                MessageBox.Show("Select Item");
            }

        }
        public ICommand _ChangeBussinessLocationItem { get; set; }
        public ICommand ChangeBussinessLocationItem
        {
            get
            {
                if (_ChangeBussinessLocationItem == null)
                {
                    _ChangeBussinessLocationItem = new DelegateCommand(ChangeBussinessLocation_Item);
                }

                return _ChangeBussinessLocationItem;
            }
        }

        public  void ChangeBussinessLocation_Item()
        {
            if (App.Current.Properties["SelectedItem"] != null)
            {
                InvoicePOS.Views.CashRegister.ChangeBussinessLocation IA = new InvoicePOS.Views.CashRegister.ChangeBussinessLocation();
                IA.Show();

            }
        }
      
       
        public ICommand _ClickOk { get; set; }
        public ICommand ClickOk
        {
            get
            {
                if (_ClickOk == null)
                {
                    _ClickOk = new DelegateCommand(Click_Ok);
                }
                return _ClickOk;
            }
        }
        public void Click_Ok()
        {
           
            if (App.Current.Properties["CASH_REG_NAME"] != null && CashRegisterAmountDetails.BusLocationName != null)
            {
                //InvoicePOS.Views.CashRegister.CashReg.CashRegName.Text = _selectedItem.CASH_REG_NAME;
                //textBox5.Text = "";
                Main.CashRegisterName.Text = CashReg.CashRegName.Text;
                App.Current.Properties["CashReg"] = CashReg.CashRegName.Text;
                CashRegisterAmountDetails.BusLocationName.Text = CashReg.CashRegName.Text;
            }
        }
    
        public MainViewModel()
        {
            SelectedItem = new ItemModel();

            ShowTaxVisibility = "Visible";
            HideTaxVisibility = "Collapsed";
            //TaxNameVisible = "Collapsed";
            //RiteVisible = "Collapsed";
            //TaxNameVisible = false;
            App.Current.Properties["Action"] = 1;
            if (App.Current.Properties["Company_Id"] != null)
            {
                comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            }
            else
            {
                GetCompany();
            }

            ListGrid = new ObservableCollection<ItemModel>();
            SelectedItem = new ItemModel();

            //GetStockItem();
            //Show_Stock();

            QUNT_TOTAL = 0;

            if (App.Current.Properties["ShowStockMain"] != null)
            {
                SelectedItem = App.Current.Properties["ShowStockMain"] as ItemModel;
                var gridItem = new ObservableCollection<ItemModel>();
                gridItem = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;

                // ShowStockGrid();
                ITEM_NAME = SelectedItem.ITEM_NAME;
                BARCODE = SelectedItem.BARCODE;
                SALES_PRICE = Convert.ToString(SelectedItem.SALES_PRICE);


                for (int i = 0; i < gridItem.Count; i++)
                {

                    autoItemNameList.Add(new ItemNameAutoListModel
                    {
                        DisplayName = gridItem[i].ITEM_NAME,
                        DisplayId = gridItem[i].ITEM_ID
                    });

                    autoScrCodeList.Add(new SearchCodeAutoListModel
                    {
                        DisplayName = gridItem[i].SEARCH_CODE,
                        DisplayId = gridItem[i].ITEM_ID
                    });
                }
                App.Current.Properties["AutoScrCodeList"] = autoScrCodeList;
                App.Current.Properties["AutoItemNameList"] = autoItemNameList;
                // App.Current.Properties["ShowStockMain"] = null;
                // App.Current.Properties["StockGridList"] = gridItem;
            }
            else if (App.Current.Properties["InvoiceDiscount"] != null)
            {
                var datagrid = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;

                for (int i = 0; i < datagrid.Count; i++)
                {

                    SelectedItem = App.Current.Properties["InvoiceDiscount"] as ItemModel;
                    if (DisCountPerSelectAmount != null) 
                    { 

                    }
                    var ItemTo = (from a in datagrid where a.ITEM_ID == SelectedItem.ITEM_ID select a).FirstOrDefault();
                    if (DisAmt == 0)
                    {
                        TotalDisAmt = SelectedItem.Discount;
                        GROSSAMT = Convert.ToDecimal(ItemTo.Total);
                        App.Current.Properties["CurrentGrosAmount"] = GROSSAMT;
                        NETAMT = Convert.ToDecimal((datagrid[i].SALES_PRICE) * (datagrid[i].OPN_QNT) + NETAMT);
                        DisNewAmount = (GROSSAMT - TotalDisAmt);
                    }
                    else
                    {
                        TotalDisAmt = DisAmt;
                        GROSSAMT = Convert.ToDecimal(ItemTo.Total);
                        App.Current.Properties["CurrentGrosAmount"] = GROSSAMT;
                        NETAMT = Convert.ToDecimal((datagrid[i].SALES_PRICE) * (datagrid[i].OPN_QNT) + NETAMT);
                        DisNewAmount = (GROSSAMT - TotalDisAmt);
                    }

                    //DisNewAmount = ItemTo.Total;
                    
                }
                App.Current.Properties["InvoiceDiscount"] = null;
            }
            else if (App.Current.Properties["TaxDetails"] != null)
            {
                SelectedItem = App.Current.Properties["TaxDetails"] as ItemModel;
                ITEM_NAME = SelectedItem.ITEM_NAME;
                BARCODE = SelectedItem.BARCODE;
                TaxName = SelectedItem.TAX_PAID_NAME;
                //TaxPrice = SelectedItem.TAX_COLLECTED;
                TotalTax = ((SelectedItem.SALES_PRICE * SelectedItem.TAX_PAID) / 100).ToString("##.##");
                App.Current.Properties["TaxDetails"] = null;
            }
            else
            {

                Invoice_No = false;
                ReadVisible = "Visible";
                RiteVisible = "Collapsed";
                LoadGrid();
                GetInvoiceNo();
                GetItemForAutoList(comp);
                GetBussList(comp);
                GetCustomer(comp);
                if (App.Current.Properties["DataGrid"] != null)
                {

                    var gridItem = new ObservableCollection<ItemModel>();
                    gridItem = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;
                    for (int i = 0; i < gridItem.Count; i++)
                    {

                        autoItemNameList.Add(new ItemNameAutoListModel
                        {
                            DisplayName = gridItem[i].ITEM_NAME,
                            DisplayId = gridItem[i].ITEM_ID
                        });

                        autoScrCodeList.Add(new SearchCodeAutoListModel
                        {
                            DisplayName = gridItem[i].SEARCH_CODE,
                            DisplayId = gridItem[i].ITEM_ID
                        });
                    }
                    App.Current.Properties["AutoScrCodeList"] = autoScrCodeList;
                    App.Current.Properties["AutoItemNameList"] = autoItemNameList;
                }
                //if(App.Current.Properties["BusinessLocName"] != null)
                //{
                //    BusinessLocName = App.Current.Properties["BusinessLocName"] as string;
                //    BusinessLocAddress = App.Current.Properties["BusinessLocAddress"] as string;
                //}
                App.Current.Properties["AutoScrCodeList"] = autoScrCodeList;
                App.Current.Properties["AutoItemNameList"] = autoItemNameList;

            }

        }

        CustomerModel[] dataCustomer = null;

        List<CustomerModel> _ListGridCustomer = new List<CustomerModel>();
        public List<CustomerModel> _ListGrid_Cust { get; set; }
        public List<CustomerModel> ListGrid_Cust
        {
            get
            {
                return _ListGrid_Cust;
            }
            set
            {
                this._ListGrid_Cust = value;
                OnPropertyChanged("ListGrid_Cust");
            }
        }


        private List<CustomerModel> _CustomerData;
        public List<CustomerModel> CustomerData
        {
            get { return _CustomerData; }
            set
            {
                if (_CustomerData != value)
                {
                    _CustomerData = value;
                }

            }
        }

        public async Task<ObservableCollection<CustomerModel>> GetCustomer(long Id)
        {
            try
            {
                _ListGridCustomer = new List<CustomerModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CustomerAPI/GetCustomer?id=" + Id + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    dataCustomer = JsonConvert.DeserializeObject<CustomerModel[]>(await response.Content.ReadAsStringAsync());
                    CustomerData = new List<CustomerModel>();
                    int x = 0;
                    for (int i = 0; i < dataCustomer.Length; i++)
                    {
                        x++;

                        autoCustModelList.Add(new CustomerAutoCompleteListModel
                    {
                        DisplayName = dataCustomer[i].NAME,
                        DisplayId = dataCustomer[i].CUSTOMER_ID
                    });

                    }


                    App.Current.Properties["AutoCustList"] = autoCustModelList;



                }

                return new ObservableCollection<CustomerModel>(_ListGridCustomer);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private bool _ExPay;
        public bool ExPay
        {

            get
            {
                return _ExPay;
            }
            set
            {

                _ExPay = value;
                OnPropertyChanged("ExPay");
            }
        }
        private bool _PayNow;
        public bool PayNow
        {

            get
            {
                return _PayNow;
            }
            set
            {

                _PayNow = value;
                OnPropertyChanged("PayNow");
            }
        }

        private decimal _DiscountTotal;
        public decimal DiscountTotal
        {

            get
            {
                return _DiscountTotal;
            }
            set
            {

                _DiscountTotal = value;
                OnPropertyChanged("DiscountTotal");
            }
        }


        #region Open Setup window

        private ICommand _SetUpAdmin { get; set; }
        public ICommand SetUpAdmin
        {
            get
            {
                if (_SetUpAdmin == null)
                {
                    _SetUpAdmin = new DelegateCommand(OpenSetUp);
                }
                return _SetUpAdmin;
            }

        }
        #endregion

        #region Open Close
        public void OpenSetUp()
        {
            WelComePage tt = new WelComePage();
            Application.Current.MainWindow = tt;
            ModalService.NavigateTo(new CustomerList(), delegate(bool returnValue) { });
            tt.Show();
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

        #endregion


        #region 09/02/2017
        private ICommand _ExPressPayClick { get; set; }
        public ICommand ExPressPayClick
        {
            get
            {
                if (_ExPressPayClick == null)
                {
                    _ExPressPayClick = new DelegateCommand(ExPressPay_Click);
                }
                return _ExPressPayClick;
            }

        }

        public void ExPressPay_Click()
        {
            if (App.Current.Properties["DataGridL"] != null)
            {
                App.Current.Properties["PyClick"] = 1;
                ExPressPay ex = new ExPressPay();
                ex.ShowDialog();
                GetInvoiceNo();
            }
        }

        private ICommand _PayClick { get; set; }
        public ICommand PayClick
        {
            get
            {
                if (_PayClick == null)
                {
                    _PayClick = new DelegateCommand(Pay_Click);
                }
                return _PayClick;
            }

        }

        public void Pay_Click()
        {
            if (App.Current.Properties["DataGridL"] != null)
            {
               
                App.Current.Properties["PyClick"] = 1;
                PayNow ex = new PayNow();

                ex.ShowDialog();
                GetInvoiceNo();
            }
        }

        private ICommand _CustomerClick { get; set; }
        public ICommand CustomerClick
        {
            get
            {
                if (_CustomerClick == null)
                {
                    _CustomerClick = new DelegateCommand(Customer_Click);
                }
                return _CustomerClick;
            }

        }

        public void Customer_Click()
        {
            App.Current.Properties["Action"] = "";
            App.Current.Properties["CustomerMainClick"] = 1;
            Window_CustomerList ex = new Window_CustomerList();
            ex.ShowDialog();
        }
        private ICommand _BussClick { get; set; }
        public ICommand BussClick
        {
            get
            {
                if (_BussClick == null)
                {
                    _BussClick = new DelegateCommand(Buss_Click);
                }
                return _BussClick;
            }

        }

        public void Buss_Click()
        {
            App.Current.Properties["BussMainReff"] = 1;
            Window_BusinessLocationList ex = new Window_BusinessLocationList();
            ex.ShowDialog();
        }
        BusinessLocationModel[] dataBuss = null;

        List<AutoBussinessModel> bussList = new List<AutoBussinessModel>();
        List<BusinessLocationModel> _ListGrid_Busniss = new List<BusinessLocationModel>();
        public async Task<ObservableCollection<BusinessLocationModel>> GetBussList(long copm)
        {
            try
            {
                bussList = new List<AutoBussinessModel>();
                _ListGrid_Busniss = new List<BusinessLocationModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/BussLocationAPI/GetAllBusinessLo?id=" + copm + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    dataBuss = JsonConvert.DeserializeObject<BusinessLocationModel[]>(await response.Content.ReadAsStringAsync());
                    for (int i = 0; i < dataBuss.Length; i++)
                    {

                        bussList.Add(new AutoBussinessModel
                        {
                            DisplayName = dataBuss[i].SHOP_NAME,
                            DisplayId = dataBuss[i].BUSINESS_LOCATION_ID
                        });
                    }
                    App.Current.Properties["BussLocList"] = bussList;
                }
                return new ObservableCollection<BusinessLocationModel>(_ListGrid_Busniss);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private ICommand _BancACClick { get; set; }
        public ICommand BancACClick
        {
            get
            {
                if (_BancACClick == null)
                {
                    _BancACClick = new DelegateCommand(BancAC_Click);
                }
                return _BancACClick;
            }

        }

        public void BancAC_Click()
        {
            Window_CustomerList ex = new Window_CustomerList();
            ex.ShowDialog();
        }


        #endregion
        private ICommand _ItemClick { get; set; }
        public ICommand ItemClick
        {
            get
            {
                if (_ItemClick == null)
                {
                    _ItemClick = new DelegateCommand(Item_Click);
                }
                return _ItemClick;
            }

        }

        public void Item_Click()
        {
            App.Current.Properties["ItemMain"] = 1;
            Window_ItemList ex = new Window_ItemList();
            ex.ShowDialog();
        }


        private ICommand _InvoiceDiscount { get; set; }
        public ICommand InvoiceDiscount
        {
            get
            {
                if (_InvoiceDiscount == null)
                {
                    _InvoiceDiscount = new DelegateCommand(Invoice_Discount);
                }
                return _InvoiceDiscount;
            }

        }

        # region click on main page ShowStock Button and asign Properties["ShowStockMain"]

        private ICommand _ShowStock { get; set; }
        public ICommand ShowStock
        {
            get
            {
                if (_ShowStock == null)
                {
                    _ShowStock = new DelegateCommand(Show_Stock);
                }
                return _ShowStock;
            }
        }
        public async void Show_Stock()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.ITEM_ID != null && SelectedItem.ITEM_ID != 0)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetEditItem?id=" + SelectedItem.ITEM_ID + "").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                        if (data.Length > 0)
                        {
                            for (int i = 0; i < data.Length; i++)
                            {
                                SelectedItem.ITEM_ID = data[i].ITEM_ID;
                                SelectedItem.ITEM_NAME = data[i].ITEM_NAME;
                                SelectedItem.BARCODE = data[i].BARCODE;
                                SelectedItem.SALES_PRICE = data[i].SALES_PRICE;
                                SelectedItem.BUSINESS_LOC = data[i].BUSINESS_LOC;
                                SelectedItem.GODOWN = data[i].GODOWN;
                                SelectedItem.COMPANY_NAME = data[i].COMPANY_NAME;
                                SelectedItem.DATE = data[i].DATE;
                                SelectedItem.BUSINESS_LOC = data[i].BUSINESS_LOC;
                                SelectedItem.OPENING_STOCK_ID = data[i].OPENING_STOCK_ID;
                                //SelectedItem.Current_Qty = data[i].Current_Qty;
                                SelectedItem.OPN_QNT = data[i].OPN_QNT;
                            }
                        }
                    }
                    App.Current.Properties["ShowStockMain"] = SelectedItem;
                    ShowStockList sost = new ShowStockList();
                    sost.ShowDialog();


                }
                else
                {
                    MessageBox.Show("Select Item");

                }
            }
            else
            {
                MessageBox.Show("Select Item");

            }
        }

        #endregion





        public void Invoice_Discount()
        {
            if (ListGrid != null)
            {
                if (ListGrid.Count != 0)
                {
                    if (SelectedItem.ITEM_ID != 0 && SelectedItem.ITEM_ID != null)
                    {
                        App.Current.Properties["InvoiceDiscount"] = SelectedItem;
                        Discount ex = new Discount();
                        ex.ShowDialog();
                    }
                       
                    else
                    {
                        MessageBox.Show("Select Item ");
                    }
                }
                else
                {
                    MessageBox.Show("Invoice Not Blanck");

                }
            }
            else
            {
                MessageBox.Show("Item Not Blanck");
            }

        }
        private ICommand _ItemSearchClick { get; set; }
        public ICommand ItemSearchClick
        {
            get
            {
                if (_ItemSearchClick == null)
                {
                    _ItemSearchClick = new DelegateCommand(ItemSearch_Click);
                }
                return _ItemSearchClick;
            }

        }

        public void ItemSearch_Click()
        {
            App.Current.Properties["ItemSearchMain"] = 1;
            Window_ItemList ex = new Window_ItemList();
            ex.ShowDialog();
        }

        private ICommand _ItemShowStock { get; set; }
        public ICommand ItemShowStock
        {
            get
            {
                if (_ItemShowStock == null)
                {
                    _ItemShowStock = new DelegateCommand(ItemShow_Stock);
                }
                return _ItemShowStock;
            }

        }

        public void ItemShow_Stock()
        {
            App.Current.Properties["ItemStock"] = 1;
            Window_ItemList ex = new Window_ItemList();
            ex.ShowDialog();

        }
        private bool _Invoice_No;
        public bool Invoice_No
        {

            get
            {
                return _Invoice_No;
            }
            set
            {
                _Invoice_No = value;
                OnPropertyChanged("Invoice_No");
            }
        }

        // For Discount Windows


        private ICommand _ButDiscount { get; set; }
        public ICommand ButDiscount
        {
            get
            {
                if (_ButDiscount == null)
                {
                    _ButDiscount = new DelegateCommand(ButDiscount_click);
                }
                return _ButDiscount;
            }

        }

        public void ButDiscount_click()
        {

            Main.GrossamountReff.Text = Convert.ToString(DisNewAmount);
            get_list();
            TotalBottom();
            this.Close();
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
                if (item.ITEM_ID == Convert.ToInt64(SelectedItem.ITEM_ID))
                {
                    _ListGrid_Temp.Add(new ItemModel
                    {
                        Discount = TotalDisAmt,
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
                        TaxName = item.TaxName,
                        TaxValue = item.TaxValue,
                        Total = ((decimal)(item.Current_Qty) * (item.SALES_PRICE)) - TotalDisAmt,

                    });
                }
                else
                {
                    _ListGrid_Temp.Add(new ItemModel
                    {
                        Discount = item.Discount,
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
                        Total = item.Total,
                        TaxName = item.TaxName,
                        TaxValue = item.TaxValue,
                    });
                }
                App.Current.Properties["DataGridL"] = _ListGrid_Temp;
                App.Current.Properties["DataGrid"] = _ListGrid_Temp;
            }
            ListGrid = _ListGrid_Temp;
            Main.ListGridRef.ItemsSource = null;
            Main.ListGridRef.ItemsSource = _ListGrid_Temp.ToList();
        }
        private decimal _DisAmt;
        public decimal DisAmt
        {
            get
            {
                return _DisAmt;
            }
            set
            {
                _DisAmt = value;
                if (DiscountAmt == true)
                {
                    
                        TotalDisAmt = 0;
                        TotalDisAmt = _DisAmt;
                        decimal IteTo = (GROSSAMT - _DisAmt) * (SelectedItem.TAX_COLLECTED / 100);
                        DisNewAmount = GROSSAMT - _DisAmt;
                        //DisNewAmount = Convert.ToDecimal((IteTo + (GROSSAMT - _DisAmt)).ToString("0.00"));
                        App.Current.Properties["CurrentGrosAmount"] = DisNewAmount;
             
                    //SelectedItem = App.Current.Properties["InvoiceDiscount"] as ItemModel;
                    //var datagrid = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;
                    //TotalDisAmt = 0;
                    //TotalDisAmt = _DisAmt;
                    //DisNewAmount = GROSSAMT - _DisAmt;
                    //App.Current.Properties["CurrentGrosAmount"] = DisNewAmount;
                }
                else
                {
                    _DisAmt = 0;
                }
                OnPropertyChanged("DisAmt");
            }
        }
       
     
        private decimal _DisPer;
        public decimal DisPer
        {
            get
            {
                return _DisPer;
            }
            set
            {
                _DisPer = value;
                if (DisCountPer == true)
                {
                    if (DisAmt == 0)
                    {
                        TotalDisAmt = 0;
                        TotalDisAmt = (GROSSAMT * _DisPer) / 100;
                        DisNewAmount = GROSSAMT - TotalDisAmt;
                        App.Current.Properties["CurrentGrosAmount"] = DisNewAmount;
                    }
                    else 
                    {
                        TotalDisAmt = 0;
                        TotalDisAmt = DisAmt;
                        DisNewAmount = GROSSAMT - TotalDisAmt;
                        App.Current.Properties["CurrentGrosAmount"] = DisNewAmount;
                    }
                }
                else
                {
                    _DisPer = 0;
                }
                OnPropertyChanged("DisPer");
            }
        }
        private decimal _TotalDisAmt;
        public decimal TotalDisAmt
        {
            get
            {
                return _TotalDisAmt;
            }
            set
            {
                _TotalDisAmt = value;
                OnPropertyChanged("TotalDisAmt");
            }
        }
        private decimal _TotalDisAmtPer;
        public decimal TotalDisAmtPer
        {
            get
            {
                return _TotalDisAmtPer;
            }
            set
            {
                _TotalDisAmtPer = value;
                OnPropertyChanged("TotalDisAmtPer");
            }
        }
        private bool _InvoiceBill;
        public bool InvoiceBill
        {

            get
            {
                return _InvoiceBill;
            }
            set
            {
                _InvoiceBill = value;
                OnPropertyChanged("InvoiceBill");
            }
        }
        private bool _TotalInvoiceBill;
        public bool TotalInvoiceBill
        {
            get { return _TotalInvoiceBill; }
            set { _TotalInvoiceBill = value;
            NotifyPropertyChanged("TotalInvoiceBill");
            }
        }
        private bool _InvoiceSelectAmount;
        public bool InvoiceSelectAmount
        {
            get { return _InvoiceSelectAmount; }
            set
            {
                _InvoiceSelectAmount = value;
                NotifyPropertyChanged("InvoiceSelectAmount");
            }
        }
        private ICommand _InvoiceChecked;
        public ICommand InvoiceChecked
        {

            get
            {
                if (_InvoiceChecked == null)
                {
                    _InvoiceChecked = new DelegateCommand(Invoice_Checked);
                }
                return _InvoiceChecked;
            }
            
        }
        public void Invoice_Checked()
        {
            if (DisCountInvoiceBill == true)
            {
                TotalInvoiceBill = true;
                InvoiceSelectAmount = false;
            }

        }
        private bool _DisCountInvoiceBill;
        public bool DisCountInvoiceBill
        {

            get
            {
                
                return _DisCountInvoiceBill;
            }
            set
            {
                _DisCountInvoiceBill = value;
                OnPropertyChanged("DisCountInvoiceBill");
            }
        }
       

        private bool _DisCountPerSelectAmount;
        public bool DisCountPerSelectAmount
        {

            get
            {
                return _DisCountPerSelectAmount;
            }
            set
            {
                _DisCountPerSelectAmount = value;
                OnPropertyChanged("DisCountPerSelectAmount");
            }
        }
        private bool _DiscountAmtIsEnabel;
        public bool DiscountAmtIsEnabel
        {

            get
            {
                return _DiscountAmtIsEnabel;
            }
            set
            {
                _DiscountAmtIsEnabel = value;
                OnPropertyChanged("DiscountAmtIsEnabel");
            }
        }
        private decimal _DisNewAmount;
        public decimal DisNewAmount
        {

            get
            {
                return _DisNewAmount;
            }
            set
            {

                _DisNewAmount = value;
                OnPropertyChanged("DisNewAmount");
            }
        }
        private decimal _TotalDisAmount;
        public decimal TotalDisAmount
        {

            get
            {
                return _TotalDisAmount;
            }
            set
            {

                _TotalDisAmount = value;
                OnPropertyChanged("TotalDisAmount");
            }
        }
        private bool _DiscountPerIsEnable;
        public bool DiscountPerIsEnable
        {

            get
            {
                return _DiscountPerIsEnable;
            }
            set
            {

                _DiscountPerIsEnable = value;
                OnPropertyChanged("DiscountPerIsEnable");
            }
        }
        private ICommand _AutoInvoiceNo { get; set; }
        public ICommand AutoInvoiceNo
        {
            get
            {
                if (_AutoInvoiceNo == null)
                {
                    _AutoInvoiceNo = new DelegateCommand(AutoInvoice_No);
                }
                return _AutoInvoiceNo;
            }

        }

        public void AutoInvoice_No()
        {
            Invoice_No = true;
            ReadVisible = "Collapsed";
            RiteVisible = "Visible";
            INVOICE_NO = "";
        }
        private string _ReadVisible { get; set; }
        public string ReadVisible
        {

            get { return _ReadVisible; }
            set
            {
                if (value != _ReadVisible)
                {
                    _ReadVisible = value;
                    OnPropertyChanged("ReadVisible");
                }
            }
        }

        private string _RiteVisible { get; set; }
        public string RiteVisible
        {

            get { return _RiteVisible; }
            set
            {
                if (value != _RiteVisible)
                {
                    _RiteVisible = value;
                    OnPropertyChanged("RiteVisible");
                }
            }
        }
        private ICommand _DiscountOk { get; set; }
        public ICommand DiscountOk
        {
            get
            {
                if (_DiscountOk == null)
                {
                    _DiscountOk = new DelegateCommand(Discount_Ok);
                }
                return _DiscountOk;
            }

        }
        public async void Discount_Ok() 
        {
            ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
            var data = App.Current.Properties["InvoiceDiscount"] as ItemModel;
            var Load_Item = (((IEnumerable)App.Current.Properties["DataGrid"]).Cast<ItemModel>()).ToList();
            Main.ListGridRef.ItemsSource = null;

            Main.GrossamountReff.Text = "0";
            Main.ListQnt.Text = "0";
            int x = 0;
            foreach (var item in Load_Item)
            {
                if (item.ITEM_ID == Convert.ToInt64(SelectedItem.ITEM_ID))
                {
                    _ListGrid_Temp.Add(new ItemModel
                    {
                        Discount = TotalDisAmt,
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
                        TaxName = item.TaxName,
                        TaxValue = item.TaxValue,
                        Total = DisNewAmount,
                        NETAMT = DisNewAmount,
                        GROSSAMT = Total
                    });
                }
                else
                {
                    _ListGrid_Temp.Add(new ItemModel
                    {
                        Discount = item.Discount,
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
                        Total = DisNewAmount,
                        TaxName = item.TaxName,
                        TaxValue = item.TaxValue,
                        NETAMT = DisNewAmount,
                        GROSSAMT = Total
                    });
                }
                App.Current.Properties["DataGridL"] = _ListGrid_Temp;
                App.Current.Properties["DataGrid"] = _ListGrid_Temp;
            }
            ListGrid = _ListGrid_Temp;
            Main.ListGridRef.ItemsSource = null;
            this.Close();
            Main.GrossamountReff.Text = GROSSAMT.ToString();
            Main.NetAmountMainReff.Text = DisNewAmount.ToString();
            Main.ListGridRef.ItemsSource = _ListGrid_Temp.ToList();
           




             // _ListGrid.Add(new ItemModel
             //{
             //    BARCODE = SelectedItem.BARCODE,
             //    ITEM_NAME = SelectedItem.ITEM_NAME,
             //    Total = DisNewAmount
             //});
          
            

        }

        private ICommand _AutoInvoiceNo1 { get; set; }
        public ICommand AutoInvoiceNo1
        {
            get
            {
                if (_AutoInvoiceNo1 == null)
                {
                    _AutoInvoiceNo1 = new DelegateCommand(AutoInvoice_No1);
                }
                return _AutoInvoiceNo1;
            }

        }

        public void AutoInvoice_No1()
        {
            Invoice_No = false;
            ReadVisible = "Visible";
            RiteVisible = "Collapsed";
            GetInvoiceNo();
        }


        public async Task<string> GetInvoiceNo()
        {

            string uhy = "";
            try
            {
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceNo1?CompanyId=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                    uhy = await response.Content.ReadAsStringAsync();
                }
                string dd = Convert.ToString(uhy);
                string aaa = dd.Substring(1, 10);
                INVOICE_NO = aaa;
            }
            catch (Exception ex)
            { }

            return uhy;
        }
        public void TotalBottom()
        {
            decimal Dis = 0;
            decimal vatAmount = 0;
            decimal netAmount = 0;
            decimal GossAmount = 0;
            decimal ItemToTal = 0;
            decimal netamount = 0;
            decimal texa = 0;
            decimal vatAm = 0;
            if (ListGrid.Count == 0)
            {
                ExPay = false;
                PayNow = false;
            }
            else
            {
                ExPay = true;
                PayNow = true;
                ItemToTal = ListGrid.Count;
                //for (int i = 0; i < ListGrid.Count; i++)
                //{
                //     netamount = ListGrid[i].SALES_PRICE_BEFOR_TAX - ListGrid[i].Discount;
                //    // decimal? Qamt = ListGrid[i].Total * ListGrid[i].Current_Qty;
                //    GossAmount = Convert.ToDecimal(ListGrid[i].Total + GossAmount);

                //    App.Current.Properties["CurrentGrosAmount"] = GROSSAMT;
                //    NETAMT = Convert.ToDecimal((ListGrid[i].SALES_PRICE) * (ListGrid[i].OPN_QNT) + NETAMT);
                //}

                QUNT_TOTAL = 0;
                foreach (var qunt in ListGrid)
                {
                    QUNT_TOTAL = qunt.Current_Qty + QUNT_TOTAL;

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
                        ListGrid[i].TotalTax = salPrice * (ListGrid[i].TAX_COLLECTED / 100);
                        ListGrid[i].SALES_PRICE_BEFOR_TAX_QTY = Convert.ToDecimal(ListGrid[i].Current_Qty * ListGrid[i].SALES_PRICE_BEFOR_TAX);
                        ListGrid[i].SalePriceWithDiscount = Convert.ToDecimal(ListGrid[i].Current_Qty * ListGrid[i].SALES_PRICE_BEFOR_TAX) - ListGrid[i].Discount;
                    }
                    else
                    {
                        ListGrid[i].TotalTax = ListGrid[i].Current_Qty * (ListGrid[i].SALES_PRICE - ListGrid[i].SALES_PRICE_BEFOR_TAX);

                        ListGrid[i].SALES_PRICE_BEFOR_TAX_QTY = Convert.ToDecimal(ListGrid[i].Current_Qty * ListGrid[i].SALES_PRICE_BEFOR_TAX);
                        ListGrid[i].SalePriceWithDiscount = Convert.ToDecimal(ListGrid[i].Current_Qty * ListGrid[i].SALES_PRICE_BEFOR_TAX);
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
            Main.ItemTotalMainReff.Text = Convert.ToString(QUNT_TOTAL);

            //Main.BussLocationMainReff.Text = Convert.ToString(QUNT_TOTAL);

        }

        #region TaxShow
        private string _TaxNameVisible;
        public string TaxNameVisible
        {

            get { return _TaxNameVisible; }
            set
            {
                _TaxNameVisible = value;
                OnPropertyChanged("TaxNameVisible");
            }
        }
        private bool _TaxPriceVisible;
        public bool TaxPriceVisible
        {

            get { return _TaxPriceVisible; }
            set
            {
                _TaxPriceVisible = value;
                OnPropertyChanged("TaxPriceVisible");
            }
        }
        private bool _TaxTotalVisible;
        public bool TaxTotalVisible
        {
            get { return _TaxTotalVisible; }
            set
            {
                _TaxTotalVisible = value;
                OnPropertyChanged("TaxTotalVisible");
            }
        }
        private string _ShowTaxVisibility;
        public string ShowTaxVisibility
        {
            get { return _ShowTaxVisibility; }
            set
            {
                _ShowTaxVisibility = value;
                OnPropertyChanged("ShowTaxVisibility");
            }
        }
        private string _HideTaxVisibility;
        public string HideTaxVisibility
        {
            get { return _HideTaxVisibility; }
            set
            {
                _HideTaxVisibility = value;
                OnPropertyChanged("HideTaxVisibility");
            }
        }
        #endregion
        #region Estimate 10.06.2017
        private ICommand _EstimateClick { get; set; }
        public ICommand EstimateClick
        {
            get
            {
                if (_EstimateClick == null)
                {
                    _EstimateClick = new DelegateCommand(Estimate_Click);
                }
                return _EstimateClick;
            }

        }

        public async void Estimate_Click()
        {
            App.Current.Properties["Estimate_Grid"] = App.Current.Properties["DataGrid"];
            _ListGrid_Temp = App.Current.Properties["Estimate_Grid"] as ObservableCollection<ItemModel>;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/InvoiceAPI/CreateEstimate/", _ListGrid_Temp);
            if (response.StatusCode.ToString() == "OK")
            {
               
                App.Current.Properties["DataGrid"] = null;
                App.Current.Properties["DataGridL"] = null;
                App.Current.Properties["DataGridEstimate"] = null;
                ObservableCollection<ItemModel> listmodelgrd = new ObservableCollection<ItemModel>();
                Main.ListGridRef.ItemsSource = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;
                
                NETAMT = 0;
                GROSSAMT = 0;
                QUNT_TOTAL = 0;
                TOTAL_ITEM = 0;
                ExPay = false;
                PayNow = false;
                VATAMOUNTT = 0;
                BussLocation = null;
                CustomerMain = null;
                DiscountTotal = 0;
                SelectedItem = new ItemModel();

            }
            
        }
        #endregion
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

        public Task CoreDispatcherPriority { get; private set; }


        // public event PropertyChangedEventHandler PropertyChanged;
        private CoreDispatcher _dispatcher = null;
        private async Task UIThreadAction(Action act)
        {
            //  await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => act.Invoke());
        }
        private async void PropertyChangedAsync(string property)
        {
            if (PropertyChanged != null)
                await UIThreadAction(() => PropertyChanged(this, new PropertyChangedEventArgs(property)));
        }

        //public void Page_Load(object sender, EventArgs e)
        //{
        //    //Populate DataTable
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("Name");
        //    dt.Columns.Add("Age");
        //    dt.Columns.Add("City");
        //    dt.Columns.Add("Country");
        //    dt.Rows.Add();
        //    dt.Rows[0]["Name"] = "Mudassar Khan";
        //    dt.Rows[0]["Age"] = "27";
        //    dt.Rows[0]["City"] = "Mumbai";
        //    dt.Rows[0]["Country"] = "India";

        //    //Bind Datatable to Labels
        //    lblName.Text = dt.Rows[0]["Name"].ToString();
        //    lblAge.Text = dt.Rows[0]["Age"].ToString();
        //    lblCity.Text = dt.Rows[0]["City"].ToString();
        //    lblCountry.Text = dt.Rows[0]["Country"].ToString();

        //}
        //public void btnExport_Click(object sender, EventArgs e)
        //{
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);
        //    pnlPerson.RenderControl(hw);
        //    StringReader sr = new StringReader(sw.ToString());
        //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //    pdfDoc.Open();
        //    htmlparser.Parse(sr);
        //    pdfDoc.Close();
        //    Response.Write(pdfDoc);
        //    Response.End();

        //}
    }

    internal class CoreDispatcher
    {
        internal Task RunAsync(object normal, Func<object> p)
        {
            throw new NotImplementedException();
        }
    }
}
