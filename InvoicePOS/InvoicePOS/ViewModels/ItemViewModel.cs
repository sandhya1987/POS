using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.ViewModels.Item;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.Caching;
using System.Windows.Media;
using System.Web;
using System.Drawing;
using System.Collections;
using System.Collections.ObjectModel;
using InvoicePOS.UserControll.Item;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.IO;
using InvoicePOS.UserControll.Employee;
using InvoicePOS.UserControll.PO;
using InvoicePOS.UserControll.Category;
using InvoicePOS.UserControll.Unit;
using InvoicePOS.UserControll.TaxManagement;
using System.Windows.Data;
using InvoicePOS.UserControll.GoDown;
using InvoicePOS.Views.Customer;
using InvoicePOS.UserControll.StockTransfer;
using InvoicePOS.UserControll.StockLedger;
using InvoicePOS.UserControll.Company;
using InvoicePOS.Views.Main;
using System.Data.OleDb;
using System.Data;
using System.Net;
using InvoicePOS.Views.WelCome;
using InvoicePOS.Views;
using InvoicePOS.Views.ChangeItem;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using InvoicePOS.UserControll.Item;

namespace InvoicePOS.ViewModels
{
    public class ItemViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        Dictionary<string, int> plist = new Dictionary<string, int>();
        public HttpResponseMessage response;
        // public HttpClient client = new HttpClient();
        public ICommand select { get; set; }
        private readonly ItemModel OprModel;
        ItemModel _ItemModel = new ItemModel();
        private ItemModel _selectedItem;
        ItemModel[] data = null;
        List<UnitAutoListModel> autoUnitList = new List<UnitAutoListModel>();
        List<AutoCompleteTax> autoTaxList = new List<AutoCompleteTax>();
        List<CategoryAutoListModel> autoCatList = new List<CategoryAutoListModel>();
        ObservableCollection<ItemModel> _getItemList = new ObservableCollection<ItemModel>();
        TaxManagementModel[] dataTax = null;
        BusinessLocationModel[] dataBusniss = null;

        List<ItemModel> _ListGrid_Temp = new List<ItemModel>();
        List<POrderModel> _ListGrid_Temp1 = new List<POrderModel>();

        #region Opening stock Properties

        private long _OPENING_STOCK_ID;
        public long OPENING_STOCK_ID
        {
            get
            {
                return SelectedItem.OPENING_STOCK_ID;
            }
            set
            {
                SelectedItem.OPENING_STOCK_ID = value;
                OnPropertyChanged("OPENING_STOCK_ID");
            }
        }

        private decimal _OPRNING_BAL;
        public decimal OPRNING_BAL
        {
            get
            {
                return SelectedItem.OPRNING_BAL;
            }
            set
            {
                SelectedItem.OPRNING_BAL = value;
                OnPropertyChanged("OPRNING_BAL");
            }
        }
        private decimal _CLOSING_BAL;
        public decimal CLOSING_BAL
        {
            get
            {
                return SelectedItem.CLOSING_BAL;
            }
            set
            {
                SelectedItem.CLOSING_BAL = value;
                OnPropertyChanged("CLOSING_BAL");
            }
        }

        #endregion

        #region Bussiness/Godown properties
        private long _BUSINESS_LOC_ID;
        public long BUSINESS_LOC_ID
        {
            get
            {
                return SelectedItem.BUSINESS_LOC_ID;
            }
            set
            {
                SelectedItem.BUSINESS_LOC_ID = value;
                OnPropertyChanged("BUSINESS_LOC_ID");
            }
        }

        private string _BUSINESS_LOC;
        public string BUSINESS_LOC
        {
            get
            {
                return SelectedItem.BUSINESS_LOC;
            }
            set
            {
                SelectedItem.BUSINESS_LOC = value;
                OnPropertyChanged("BUSINESS_LOC");
            }
        }

        private string _GODOWN;
        public string GODOWN
        {
            get
            {
                return SelectedItem.GODOWN;
            }
            set
            {
                SelectedItem.GODOWN = value;

                if (SelectedItem.GODOWN != value)
                {
                    SelectedItem.GODOWN = value;
                    OnPropertyChanged("GODOWN");
                }
            }
        }

        #endregion


        #region Bussiness/Godown Click Event
        public ICommand _BusinessLocationClick;
        public ICommand BusinessLocationClick
        {
            get
            {
                if (_BusinessLocationClick == null)
                {
                    _BusinessLocationClick = new DelegateCommand(BusinessLocation_Click);
                }
                return _BusinessLocationClick;
            }
        }
        public void BusinessLocation_Click()
        {
            App.Current.Properties["BussLocationRef"] = 1;
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();

        }


        public ICommand _GodownClick;
        public ICommand GodownClick
        {
            get
            {
                if (_GodownClick == null)
                {
                    _GodownClick = new DelegateCommand(Godown_Click);
                }
                return _GodownClick;
            }
        }
        public void Godown_Click()
        {
            App.Current.Properties["GodownOpStockReff"] = 1;
            Window_GodownList IA = new Window_GodownList();
            IA.Show();

        }

        #endregion

        #region inser opening stock

        public ICommand _InsertOpeningStock;
        public ICommand InsertOpeningStock
        {
            get
            {
                if (_InsertOpeningStock == null)
                {
                    _InsertOpeningStock = new DelegateCommand(Insert_OpeningStock);
                }

                return _InsertOpeningStock;
            }
        }

        public async void Insert_OpeningStock()
        {

            if (OPN_QNT == null || OPN_QNT == 0)
            {
                MessageBox.Show("Opening Stock is missing");
            }
            else
            {
                if (BARCODE != null)
                {
                    ItemAdd.BarCodeReff.Text = BARCODE;

                }
                if (ITEM_NAME != null)
                {
                    ItemAdd.ItemNameReff.Text = ITEM_NAME;

                }
                App.Current.Properties["itemName"] = ITEM_NAME;
                App.Current.Properties["barcode"] = BARCODE;
                App.Current.Properties["BussLocation"] = BUSINESS_LOC;
                App.Current.Properties["Date"] = DATE;
                App.Current.Properties["Qunt"] = OPN_QNT;
                App.Current.Properties["Godown"] = GODOWN;
                //App.Current.Properties["Company"] = COMPANY_NAME;


                if (App.Current.Properties["GodownOpStockRef"] != null)
                {

                    App.Current.Properties["GoDownName"] = Window_Opening_stock.GodownRef.Text;
                    SelectedItem.GODOWN = Window_Opening_stock.GodownRef.Text;
                    SelectedItem.GODOWN_ID = Convert.ToInt32(App.Current.Properties["GodownOpStockRef"]);
                    App.Current.Properties["GoDownId"] = SelectedItem.GODOWN_ID;

                }


                if (App.Current.Properties["BussLocationRef"] != null)
                {
                    SelectedItem.BUSINESS_LOC = Window_Opening_stock.BussRef.Text;
                    SelectedItem.BUSINESS_LOC_ID = Convert.ToInt32(App.Current.Properties["BussLocidRef"]);
                    //Window_Opening_stock.BussRef.Text = SelectedBusinessLoca.SHOP_NAME;
                }

                MessageBox.Show("Opening stock added successfully");
                Cancel_Item();
            }
        }

        #endregion


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



        // bussLocation listion in Textbox

        public List<BusinessLocationModel> _ListGridBusniss { get; set; }
        public List<BusinessLocationModel> ListGridBusniss
        {
            get
            {
                return _ListGridBusniss;
            }
            set
            {
                this._ListGridBusniss = value;
                OnPropertyChanged("ListGridBusniss");
            }
        }
        private string _SelectCollectBusinessLocation;
        public string SelectCollectBusinessLocation
        {
            get
            {
                return _SelectCollectBusinessLocation;
            }
            set
            {
                _SelectCollectBusinessLocation = value;


                IsVisibleTax = "Visible";

                OnPropertyChanged("SelectCollectTax");

            }
        }




        #region Properties
        private string _COMPANY_NAME;
        public string COMPANY_NAME
        {
            get
            {
                return SelectedItem.COMPANY_NAME;
            }
            set
            {
                SelectedItem.COMPANY_NAME = value;

                if (SelectedItem.COMPANY_NAME != value)
                {
                    SelectedItem.COMPANY_NAME = value;
                    OnPropertyChanged("COMPANY_NAME");
                }
            }
        }

        private string _SUPPLIER_NAME;
        public string SUPPLIER_NAME
        {
            get
            {
                return SelectedItem.SUPPLIER_NAME;
            }
            set
            {
                SelectedItem.SUPPLIER_NAME = value;

                App.Current.Properties["SUPPLIER_NAME"] = value;
                OnPropertyChanged("SUPPLIER_NAME");
            }
        }
        private DateTime _RECEIVEITEM_ON_DATE;
        public DateTime RECEIVEITEM_ON_DATE
        {
            get
            {
                return SelectedItem.RECEIVE_ITEM_ENTRY_DATE;
            }
            set
            {
                SelectedItem.RECEIVE_ITEM_ENTRY_DATE = value;

                App.Current.Properties["RECEIVEITEM_ON_DATE"] = value;
                OnPropertyChanged("RECEIVEITEM_ON_DATE");
            }
        }
        private string _RECEIVE_ITEM_ENTRYNO;
        public string RECEIVE_ITEM_ENTRYNO
        {
            get
            {
                return SelectedItem.RECEIVE_ITEM_ENTRY_NO;
            }
            set
            {
                SelectedItem.RECEIVE_ITEM_ENTRY_NO = value;

                App.Current.Properties["RECEIVE_ITEM_ENTRYNO"] = value;
                OnPropertyChanged("RECEIVE_ITEM_ENTRYNO");
            }
        }
        private string _ITEM_NAME;
        public string ITEM_NAME
        {
            get
            {
                return SelectedItem.ITEM_NAME;
            }
            set
            {
                SelectedItem.ITEM_NAME = value;
                ITEM_UNIQUE_NAME = SelectedItem.ITEM_NAME;
                ITEM_GROUP_NAME = SelectedItem.ITEM_NAME;
                App.Current.Properties["itemName"] = value;
                OnPropertyChanged("ITEM_NAME");
            }
        }

        private string _ITEM_DESCRIPTION;
        public string ITEM_DESCRIPTION
        {
            get
            {
                return SelectedItem.ITEM_DESCRIPTION;
            }
            set
            {
                SelectedItem.ITEM_DESCRIPTION = value;

                if (SelectedItem.ITEM_DESCRIPTION != value)
                {
                    SelectedItem.ITEM_DESCRIPTION = value;
                    OnPropertyChanged("ITEM_DESCRIPTION");
                }
            }
        }

        private double _ITEM_PRICE;
        public double ITEM_PRICE
        {
            get
            {
                return SelectedItem.ITEM_PRICE;
            }
            set
            {
                SelectedItem.ITEM_PRICE = value;
                if (SelectedItem.ITEM_PRICE != value)
                {
                    SelectedItem.ITEM_PRICE = value;
                    OnPropertyChanged("ITEM_PRICE");
                }
            }
        }

        private string _ITEM_LOCATION;
        public string ITEM_LOCATION
        {
            get
            {
                return SelectedItem.ITEM_LOCATION;
            }
            set
            {
                SelectedItem.ITEM_LOCATION = value;
                if (SelectedItem.ITEM_LOCATION != value)
                {
                    SelectedItem.ITEM_LOCATION = value;
                    OnPropertyChanged("ITEM_LOCATION");
                }
            }
        }



        private string _KEYWORD;
        public string KEYWORD
        {
            get
            {
                return SelectedItem.KEYWORD;
            }
            set
            {
                SelectedItem.KEYWORD = value;


                if (SelectedItem.KEYWORD != value)
                {
                    SelectedItem.KEYWORD = value;
                    OnPropertyChanged("KEYWORD");
                }
            }
        }

        private string _ACCESSORIES_KEYWORD;
        public string ACCESSORIES_KEYWORD
        {
            get
            {
                return SelectedItem.ACCESSORIES_KEYWORD;
            }
            set
            {
                SelectedItem.ACCESSORIES_KEYWORD = value;


                if (SelectedItem.ACCESSORIES_KEYWORD != value)
                {
                    SelectedItem.ACCESSORIES_KEYWORD = value;
                    OnPropertyChanged("ACCESSORIES_KEYWORD");
                }
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
                if (BARCODEENABLE == true)
                {
                    App.Current.Properties["barcode"] = value;
                }

                OnPropertyChanged("BARCODE");

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

        private string _TAX_PAID_NAME;
        public string TAX_PAID_NAME
        {
            get
            {
                return SelectedItem.TAX_PAID_NAME;
            }
            set
            {
                SelectedItem.TAX_PAID_NAME = value;
                if (SelectedItem.TAX_PAID_NAME != value)
                {
                    SelectedItem.TAX_PAID_NAME = value;
                    OnPropertyChanged("TAX_PAID_NAME");
                }
            }
        }

        private decimal _TAX_COLLECTED;
        public decimal TAX_COLLECTED
        {
            get
            {
                return SelectedItem.TAX_COLLECTED;
            }
            set
            {
                SelectedItem.TAX_COLLECTED = value;
                if (SelectedItem.TAX_COLLECTED != value)
                {
                    SelectedItem.TAX_COLLECTED = value;
                    OnPropertyChanged("TAX_COLLECTED");
                }
            }
        }

        private string _TAX_COLLECTED_NAME;
        public string TAX_COLLECTED_NAME
        {
            get
            {
                return SelectedItem.TAX_COLLECTED_NAME;
            }
            set
            {
                SelectedItem.TAX_COLLECTED_NAME = value;
                if (SelectedItem.TAX_COLLECTED_NAME != value)
                {
                    SelectedItem.TAX_COLLECTED_NAME = value;
                    OnPropertyChanged("TAX_COLLECTED_NAME");
                }
            }
        }

        private string _PURCHASE_UNIT;
        public string PURCHASE_UNIT
        {
            get
            {
                return SelectedItem.PURCHASE_UNIT;
            }
            set
            {
                SelectedItem.PURCHASE_UNIT = value;

                if (SelectedItem.PURCHASE_UNIT != value)
                {
                    SelectedItem.PURCHASE_UNIT = value;
                    OnPropertyChanged("PURCHASE_UNIT");
                }
            }
        }

        private string _SALES_UNIT;
        public string SALES_UNIT
        {
            get
            {
                return SelectedItem.SALES_UNIT;
            }
            set
            {
                SelectedItem.SALES_UNIT = value;


                if (SelectedItem.SALES_UNIT != value)
                {
                    SelectedItem.SALES_UNIT = value;
                    OnPropertyChanged("SALES_UNIT");
                }
            }
        }

        private decimal _PURCHASE_UNIT_PRICE;
        public decimal PURCHASE_UNIT_PRICE
        {
            get
            {
                return SelectedItem.PURCHASE_UNIT_PRICE;
            }
            set
            {
                SelectedItem.PURCHASE_UNIT_PRICE = value;

                if (SelectedItem.PURCHASE_UNIT_PRICE != value)
                {
                    SelectedItem.PURCHASE_UNIT_PRICE = value;
                    OnPropertyChanged("PURCHASE_UNIT_PRICE");
                }
            }
        }

        private decimal _SALES_PRICE;
        public decimal SALES_PRICE
        {
            get
            {
                return SelectedItem.SALES_PRICE;
            }
            set
            {
                _SALES_PRICE = value;
                if (_SALES_PRICE == 0)
                {
                    _SALES_PRICE = SelectedItem.SALES_PRICE;
                }
                else
                {
                    _SALES_PRICE = value;
                }

                if (App.Current.Properties["TaxCollectid1"] != null)
                {
                    App.Current.Properties["TaxCollectid"] = App.Current.Properties["TaxCollectid1"];
                }
                int collid = Convert.ToInt32(App.Current.Properties["TaxCollectid"].ToString());
                if (collid != 0)
                {
                    //listpaid = App.Current.Properties["TaxPaList"] as List<TaxManagementModel>;
                    listpaid = App.Current.Properties["TaxPaList"] as ObservableCollection<TaxManagementModel>;
                    TAX_COLLECTED = (from m in listpaid where m.TAX_ID == collid select m.TAX_VALUE).FirstOrDefault();
                }
                if (INCLUDE_TAX == true)
                {
                    decimal FinaliseAmt = 0;
                    FinaliseAmt = ((_SALES_PRICE * 100) / (Convert.ToDecimal(TAX_COLLECTED) + 100));
                    SALES_PRICE_BEFOR_TAX = FinaliseAmt;
                    // SelectedItem.SALES_PRICE = _SALES_PRICE;
                }
                SelectedItem.SALES_PRICE = _SALES_PRICE;
                OnPropertyChanged("SALES_PRICE");
            }
        }
        private decimal _MRP;
        public decimal MRP
        {
            get
            {
                return SelectedItem.MRP;
            }
            set
            {
                SelectedItem.MRP = value;

                if (SelectedItem.MRP != value)
                {
                    SelectedItem.MRP = value;
                    OnPropertyChanged("MRP");
                }
            }
        }

        private decimal _WEIGHT_OF_PAPER;
        public decimal WEIGHT_OF_PAPER
        {
            get
            {
                return SelectedItem.WEIGHT_OF_PAPER;
            }
            set
            {
                SelectedItem.WEIGHT_OF_PAPER = value;

                if (SelectedItem.WEIGHT_OF_PAPER != value)
                {
                    SelectedItem.WEIGHT_OF_PAPER = value;
                    OnPropertyChanged("WEIGHT_OF_PAPER");
                }
            }
        }

        private decimal _WEIGHT_OF_PLASTIC;
        public decimal WEIGHT_OF_PLASTIC
        {
            get
            {
                return SelectedItem.WEIGHT_OF_PLASTIC;
            }
            set
            {
                SelectedItem.WEIGHT_OF_PLASTIC = value;

                if (SelectedItem.WEIGHT_OF_PLASTIC != value)
                {
                    SelectedItem.WEIGHT_OF_PLASTIC = value;
                    OnPropertyChanged("WEIGHT_OF_PLASTIC");
                }
            }
        }

        private decimal _WEIGHT_OF_FOAM;
        public decimal WEIGHT_OF_FOAM
        {
            get
            {
                return SelectedItem.WEIGHT_OF_FOAM;
            }
            set
            {
                SelectedItem.WEIGHT_OF_FOAM = value;

                if (SelectedItem.WEIGHT_OF_FOAM != value)
                {
                    SelectedItem.WEIGHT_OF_FOAM = value;
                    OnPropertyChanged("WEIGHT_OF_FOAM");
                }
            }
        }

        private decimal _WEIGHT_OF_CARDBOARD;
        public decimal WEIGHT_OF_CARDBOARD
        {
            get
            {
                return SelectedItem.WEIGHT_OF_CARDBOARD;
            }
            set
            {
                SelectedItem.WEIGHT_OF_CARDBOARD = value;

                if (SelectedItem.WEIGHT_OF_CARDBOARD != value)
                {
                    SelectedItem.WEIGHT_OF_CARDBOARD = value;
                    OnPropertyChanged("WEIGHT_OF_CARDBOARD");
                }
            }
        }


        private string _CATEGORY_NAME;
        public string CATEGORY_NAME
        {
            get
            {
                return SelectedItem.CATEGORY_NAME;
            }
            set
            {
                if (SelectedItem.CATEGORY_NAME == value)
                {
                    OnPropertyChanged("CATEGORY_NAME");
                }
            }
        }


        private long _CATAGORY_ID;
        public long CATAGORY_ID
        {
            get
            {
                return SelectedItem.CATAGORY_ID;
            }
            set
            {
                SelectedItem.CATAGORY_ID = value;

                if (SelectedItem.CATAGORY_ID != value)
                {
                    SelectedItem.CATAGORY_ID = value;
                    OnPropertyChanged("CATAGORY_ID");
                }
            }
        }


        private int _OPN_QNT;
        public int OPN_QNT
        {
            get
            {
                return _OPN_QNT;
            }
            set
            {
                _OPN_QNT = value;
                App.Current.Properties["Qunt"] = value;
                OnPropertyChanged("OPN_QNT");
            }
        }


        private string _DISPLAY_INDEX;
        public string DISPLAY_INDEX
        {
            get
            {
                return SelectedItem.DISPLAY_INDEX;
            }
            set
            {
                SelectedItem.DISPLAY_INDEX = value;

                if (SelectedItem.DISPLAY_INDEX != value)
                {
                    SelectedItem.DISPLAY_INDEX = value;
                    OnPropertyChanged("DISPLAY_INDEX");
                }
            }
        }

        private string _ITEM_UNIQUE_NAME;
        public string ITEM_UNIQUE_NAME
        {
            get
            {
                return SelectedItem.ITEM_UNIQUE_NAME;
            }
            set
            {
                SelectedItem.ITEM_UNIQUE_NAME = value;
                OnPropertyChanged("ITEM_UNIQUE_NAME");

            }
        }

        private string _ITEM_GROUP_NAME;
        public string ITEM_GROUP_NAME
        {
            get
            {
                return SelectedItem.ITEM_GROUP_NAME;
            }
            set
            {
                SelectedItem.ITEM_GROUP_NAME = value;
                OnPropertyChanged("ITEM_GROUP_NAME");

            }
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

        private string _REGIONAL_LANGUAGE;
        public string REGIONAL_LANGUAGE
        {
            get
            {
                return SelectedItem.REGIONAL_LANGUAGE;
            }
            set
            {
                SelectedItem.REGIONAL_LANGUAGE = value;

                if (SelectedItem.REGIONAL_LANGUAGE != value)
                {
                    SelectedItem.REGIONAL_LANGUAGE = value;
                    OnPropertyChanged("REGIONAL_LANGUAGE");
                }
            }
        }
        //List<TaxManagementModel> listpaid = new List<TaxManagementModel>();
        ObservableCollection<TaxManagementModel> listpaid = new ObservableCollection<TaxManagementModel>();

        private decimal _SALES_PRICE_BEFOR_TAX;
        public decimal SALES_PRICE_BEFOR_TAX
        {
            get
            {
                return SelectedItem.SALES_PRICE_BEFOR_TAX;
            }
            set
            {

                _SALES_PRICE_BEFOR_TAX = value;
                if (_SALES_PRICE_BEFOR_TAX == 0)
                {
                    _SALES_PRICE_BEFOR_TAX = SelectedItem.SALES_PRICE_BEFOR_TAX;
                }
                else
                {
                    _SALES_PRICE_BEFOR_TAX = value;
                }

                if (App.Current.Properties["TaxCollectid1"] != null)
                {
                    App.Current.Properties["TaxCollectid"] = App.Current.Properties["TaxCollectid1"];
                }

                int collid = Convert.ToInt32(App.Current.Properties["TaxCollectid"].ToString());
                if (collid != 0)
                {
                    //listpaid = App.Current.Properties["TaxPaList"] as List<TaxManagementModel>;
                    listpaid = App.Current.Properties["TaxPaList"] as ObservableCollection<TaxManagementModel>;

                    TAX_COLLECTED = (from m in listpaid where m.TAX_ID == collid select m.TAX_VALUE).FirstOrDefault();
                }
                if (INCLUDE_TAX == false)
                {
                    decimal FinaliseAmt = 0;
                    FinaliseAmt = ((_SALES_PRICE_BEFOR_TAX * (Convert.ToDecimal(TAX_COLLECTED) + 100)) / 100);
                    SALES_PRICE = FinaliseAmt;
                    //SelectedItem.SALES_PRICE_BEFOR_TAX = _SALES_PRICE_BEFOR_TAX;
                }
                SelectedItem.SALES_PRICE_BEFOR_TAX = _SALES_PRICE_BEFOR_TAX;
                OnPropertyChanged("SALES_PRICE_BEFOR_TAX");
            }
        }

        private string _SelectPaidTax;
        public string SelectPaidTax
        {
            get
            {
                return _SelectPaidTax;
            }
            set
            {
                _SelectPaidTax = value;
                IsVisibleTax = "Visible";

                OnPropertyChanged("SelectPaidTax");

            }
        }

        private string _TAX_NAME;
        public string TAX_NAME
        {
            get
            {
                return _TAX_NAME;
            }
            set
            {
                _TAX_NAME = value;


                OnPropertyChanged("TAX_NAME");

            }
        }


        private string _SelectCollectTax;
        public string SelectCollectTax
        {
            get
            {
                return _SelectCollectTax;
            }
            set
            {
                _SelectCollectTax = value;
                IsVisibleTax = "Visible";
                OnPropertyChanged("SelectCollectTax");

            }
        }

        public bool _IS_Shortable_Item;
        public bool IS_Shortable_Item
        {
            get
            {
                return SelectedItem.IS_Shortable_Item;
            }
            set
            {
                if (SelectedItem.IS_Shortable_Item != value)
                {
                    SelectedItem.IS_Shortable_Item = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IS_Shortable_Item"));
                    }
                }
            }
        }

        public bool _IS_ACTIVE;
        public bool IS_ACTIVE
        {
            get
            {
                return SelectedItem.IS_ACTIVE;
            }
            set
            {
                if (SelectedItem.IS_ACTIVE != value)
                {
                    SelectedItem.IS_ACTIVE = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IS_Not_For_Sell"));
                    }
                }
            }
        }


        public bool _ACTIVESearch;
        public bool ACTIVESearch
        {
            get
            {
                return _ACTIVESearch;
            }
            set
            {
                _ACTIVESearch = value;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                if (_ACTIVESearch == true)
                {
                    GetItem(comp);
                }
                else
                {
                    GetItem(comp);
                }
                OnPropertyChanged("_ACTIVESearch");
            }
        }
        public bool _IS_ACTIVESearch;
        public bool IS_ACTIVESearch
        {
            get
            {
                return _IS_ACTIVESearch;
            }
            set
            {
                _IS_ACTIVESearch = value;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                if (_IS_ACTIVESearch == true)
                {

                    GetItem(comp);
                }
                else
                {
                    GetItem(comp);
                }
                OnPropertyChanged("IS_ACTIVESearch");
            }
        }

        //public bool _IS_ImageSearch;
        //public bool IS_ImageSearch
        //{
        //    get
        //    {
        //        return _IS_ImageSearch;
        //    }
        //    set
        //    {
        //        _IS_ImageSearch = value;
        //        var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
        //        if (_IS_ImageSearch == true)
        //        {
        //            _IMAGE_VISIBLE = "Visible";
        //            GetItem(comp);
        //        }
        //        else
        //        {
        //            _IMAGE_VISIBLE = "Hidden";
        //            GetItem(comp);
        //        }
        //        OnPropertyChanged("IS_ImageSearch");
        //    }
        //}

        private bool isChecked;
        private RelayCommand checkCommand;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        public ICommand CheckCommand
        {
            get
            {
                if (checkCommand == null)
                    checkCommand = new RelayCommand(param => this.ColumnShowHide(param));
                return checkCommand;
            }
        }

        public void ColumnShowHide(object sender)
        {

            CheckBox ckh = sender as CheckBox;
            DependencyObject currParent = VisualTreeHelper.GetParent(ckh);
            if (ckh.IsChecked == true)
            {
                while (!(currParent is UserControl))
                {
                    currParent = VisualTreeHelper.GetParent(currParent);
                }
                (currParent as Items).dataGridView1.Columns[1].Visibility = Visibility.Visible;
            }
            else
            {
                while (!(currParent is UserControl))
                {
                    currParent = VisualTreeHelper.GetParent(currParent);
                }
                (currParent as Items).dataGridView1.Columns[1].Visibility = Visibility.Hidden;
            }

        }

        private string _Action;
        public string Action
        {
            get
            {
                return _Action;
            }
            set
            {
                if (_Action != value)
                {
                    _Action = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Action"));
                    }
                }
            }

        }

        private DateTime _DATE;
        public DateTime DATE
        {
            get
            {

                return _DATE;
            }
            set
            {
                _DATE = value;
                OnPropertyChanged("SelectedPO");
            }
        }



        public bool _INCLUDE_TAX;
        public bool INCLUDE_TAX
        {
            get
            {
                return SelectedItem.INCLUDE_TAX;
            }
            set
            {
                SelectedItem.INCLUDE_TAX = value;
                if (SelectedItem.INCLUDE_TAX == false)
                {
                    ENABLE_BEFOR_TAX = true;
                    ENABLE_SALES_PRICE = false;
                    if (SelectedItem.TAX_COLLECTED_ID != 0)
                    {
                        App.Current.Properties["TaxCollectid"] = SelectedItem.TAX_COLLECTED_ID;
                        //App.Current.Properties["TaxCollectid"] = App.Current.Properties["TaxCollectid1"];
                        SelectedItem.TAX_COLLECTED_ID = Convert.ToInt32(App.Current.Properties["TaxCollectid"].ToString());


                    }
                    else
                    {
                        App.Current.Properties["TaxCollectid"] = SelectedItem.TAX_COLLECTED_ID;
                        //mAdd.SalesPriceBeforeTax.Text = ItemAdd.SalesPriceBeforeTax.Text; 
                        //SALES_PRICE_BEFOR_TAX = SALES_PRICE_BEFOR_TAX;
                    }
                    //if (App.Current.Properties["TaxCollectid1"] != null)
                    //{
                    //    //ItemAdd.SalesPriceBeforeTax.Text = App.Current.Properties["SalesPriceBeforeTax"].ToString(); 
                    //    ItemAdd.SalesPriceBeforeTax.Text = ItemAdd.SalesPriceBeforeTax.Text; 
                    //}


                }
                else
                {
                    ENABLE_BEFOR_TAX = false;
                    ENABLE_SALES_PRICE = true;
                    if (SelectedItem.TAX_COLLECTED_ID != 0)
                    {
                        App.Current.Properties["TaxCollectid"] = SelectedItem.TAX_COLLECTED_ID;
                        //App.Current.Properties["TaxCollectid"] = App.Current.Properties["TaxCollectid1"];
                        //ItemAdd.SalesPrice.Text = App.Current.Properties["SalesPrice"].ToString();
                    }
                    else
                    {
                        App.Current.Properties["TaxCollectid"] = SelectedItem.TAX_COLLECTED_ID;
                        //App.Current.Properties["TaxCollectid"] = App.Current.Properties["TaxCollectid1"];

                        //SALES_PRICE = SALES_PRICE;
                    }

                    //if (App.Current.Properties["TaxCollectid1"] != null)
                    //{
                    //    //ItemAdd.SalesPrice.Text = App.Current.Properties["SalesPrice"].ToString();
                    //    ItemAdd.SalesPrice.Text = ItemAdd.SalesPrice.Text;
                    //}

                }

                OnPropertyChanged("INCLUDE_TAX");
            }
        }


        private bool _ENABLE_SALES_PRICE;
        public bool ENABLE_SALES_PRICE
        {

            get
            {
                return _ENABLE_SALES_PRICE;
            }
            set
            {

                _ENABLE_SALES_PRICE = value;
                OnPropertyChanged("ENABLE_SALES_PRICE");
            }
        }

        private bool _ENABLE_BEFOR_TAX;
        public bool ENABLE_BEFOR_TAX
        {

            get
            {
                return _ENABLE_BEFOR_TAX;
            }
            set
            {

                _ENABLE_BEFOR_TAX = value;
                OnPropertyChanged("ENABLE_BEFOR_TAX");
            }
        }


        //private decimal _TOTAL;
        //public decimal TOTAL
        //{
        //    get
        //    {
        //        return TOTAL;
        //    }
        //    set
        //    {
        //        //TOTAL = Convert.ToDecimal(SelectedItem.OPN_QNT * SelectedItem.ITEM_PRICE);
        //        _TOTAL = value;
        //        //SelectedItem.TOTAL_SUM = value;
        //        OnPropertyChanged("TOTAL");

        //    }
        //}

        #endregion

        #region Enable/Desable
        private bool _DeleteEnable;
        public bool DeleteEnable
        {

            get
            {
                return _DeleteEnable;
            }
            set
            {

                _DeleteEnable = value;
                OnPropertyChanged("DeleteEnable");
            }
        }


        private bool _ViewEnable;
        public bool ViewEnable
        {

            get
            {
                return _ViewEnable;
            }
            set
            {

                _ViewEnable = value;
                OnPropertyChanged("ViewEnable");
            }
        }
        private bool _EditEnable;
        public bool EditEnable
        {

            get
            {
                return _EditEnable;
            }
            set
            {

                _EditEnable = value;
                OnPropertyChanged("EditEnable");
            }
        }


        private bool _AddEnable;
        public bool AddEnable
        {

            get
            {
                return _AddEnable;
            }
            set
            {

                _AddEnable = value;
                OnPropertyChanged("AddEnable");
            }
        }
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

        private string _DEFINECODEVISIBLE;
        public string DEFINECODEVISIBLE
        {

            get
            {
                return _DEFINECODEVISIBLE;
            }
            set
            {

                _DEFINECODEVISIBLE = value;
                OnPropertyChanged("DEFINECODEVISIBLE");
            }
        }
        private string _AUTOCODEVISIBLE;
        public string AUTOCODEVISIBLE
        {

            get
            {
                return _AUTOCODEVISIBLE;
            }
            set
            {

                _AUTOCODEVISIBLE = value;
                OnPropertyChanged("AUTOCODEVISIBLE");
            }
        }

        #endregion

        private string _IsVisibleTax;
        public string IsVisibleTax
        {

            get
            {
                return _IsVisibleTax;
            }
            set
            {

                _IsVisibleTax = value;
                OnPropertyChanged("IsVisibleTax");
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
        private DateTime _FORM_DATE;
        public DateTime FORM_DATE
        {
            get
            {
                return SelectedItem.FORM_DATE;
            }
            set
            {
                SelectedItem.FORM_DATE = value;
                OnPropertyChanged("FORM_DATE");
            }
        }
        private DateTime _TO_DATE;
        public DateTime TO_DATE
        {
            get
            {
                return SelectedItem.TO_DATE;
            }
            set
            {
                SelectedItem.TO_DATE = value;
                OnPropertyChanged("TO_DATE");
            }
        }
        public ICommand _APPLY_DATE_CHANGE;
        public ICommand APPLY_DATE_CHANGE
        {
            get
            {
                if (_APPLY_DATE_CHANGE == null)
                {
                    _APPLY_DATE_CHANGE = new DelegateCommand(APPLY_DATE_CHANGE_Click);
                }
                return _APPLY_DATE_CHANGE;
            }
        }

        public async void APPLY_DATE_CHANGE_Click()
        {
            if (App.Current.Properties["GetItemDetails"] != null)
            {
                App.Current.Properties["Action"] = "SalesHistory";
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                var ItemName = SelectedItem.ITEM_NAME;
                var frmDt = SelectedItem.FORM_DATE;
                var toDt = SelectedItem.TO_DATE;
                //ListGridSales = new List<ItemModel>();
                ItemModel get = App.Current.Properties["GetItemDetails"] as ItemModel;
                int itemid = get.ITEM_ID;
                int companyid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetItemPurchaseList?frmDt=" + frmDt + "&toDt=" + toDt + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    //data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    //for (int i = 0; i < data.Length; i++)
                    //{
                    foreach (var item in data)
                    {
                        _ListGrid_Temp.Add(new ItemModel
                        {
                            //SUPPLIER_NAME = item.SUPPLIER_NAME,
                            //INVOICE_DATE = item.DATE,
                            //PO_NUMBER = item.PO_NUMBER,
                            //ITEM_NAME = item.ITEM_NAME,
                            //PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                            //SALES_UNIT = item.SALES_UNIT,
                            //SALES_PRICE = item.SALES_PRICE,
                            BUSINESS_LOC = item.BUSINESS_LOC,
                            RECEIVE_ITEM_ENTRY_DATE = item.RECEIVE_ITEM_ENTRY_DATE,
                            RECEIVE_ITEM_ENTRY_NO = item.RECEIVE_ITEM_ENTRY_NO,
                            PAYMENT_STATUS = item.PAYMENT_STATUS,
                            GODOWN = item.GODOWN,
                            SUPPLIER_NAME = item.SUPPLIER_NAME,
                            Total = item.Total,
                            TotalTax = item.TotalTax,
                            DATE = item.DATE,
                            SUPPLIER_INVOICE_NO = item.SUPPLIER_INVOICE_NO,
                            ITEM_RECEIVE_DATE = item.ITEM_RECEIVE_DATE,
                            PAYMENT_BY_CASH = item.PAYMENT_BY_CASH

                        });

                    }
                    //ListGridSales.Clear();

                    ListGridItemPurchase = _ListGrid_Temp;
                    App.Current.Properties["GetItemsellsDetails"] = _getItemList;
                    App.Current.Properties["ListGridSales"] = _ListGrid_Temp;
                    //App.Current.Properties["ItemNameSales"] = SelectedItem.ITEM_NAME;
                    //App.Current.Properties["ItemCodeSales"] = SelectedItem.ITEM_ID;
                    //ITEM_NAME = App.Current.Properties["ItemNameSales"].ToString();
                    //ITEM_ID = Convert.ToInt32(App.Current.Properties["ItemCodeSales"].ToString());
                    //ListGridSales = App.Current.Properties["ListGridSales"] as List<ItemModel>;
                    
                }

            }
            else
            {
                MessageBox.Show("Please select item");
                return;
            }
        }

        public ICommand _APPLY_DATE;
        public ICommand APPLY_DATE
        {
            get
            {
                if (_APPLY_DATE == null)
                {
                    _APPLY_DATE = new DelegateCommand(APPLYDATE_Click);
                }
                return _APPLY_DATE;
            }
        }

        public async void APPLYDATE_Click()
        {
            if (App.Current.Properties["GetItemDetails"] != null)
            {

                App.Current.Properties["Action"] = "SalesHistory";
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                var ItemName = SelectedItem.ITEM_NAME;
                var frmDt = SelectedItem.FORM_DATE;
                var toDt = SelectedItem.TO_DATE;
                //ListGridSales = new List<ItemModel>();
                ItemModel get = App.Current.Properties["GetItemDetails"] as ItemModel;
                int itemid = get.ITEM_ID;
                int companyid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetItemSalesList?frmDt=" + frmDt + "&toDt=" + toDt + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    //for (int i = 0; i < data.Length; i++)
                    //{
                    foreach (var item in data)
                    {
                        int total = 0;
                        _ListGrid_Temp.Add(new ItemModel
                        {

                            SUPPLIER_NAME = item.SUPPLIER_NAME,
                            INVOICE_DATE = item.INVOICE_DATE,
                            PO_NUMBER = item.PO_NUMBER,
                            ITEM_NAME = item.ITEM_NAME,
                            PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                            SALES_UNIT = item.SALES_UNIT,
                            SALES_PRICE = item.SALES_PRICE,
                            BUSINESS_LOC = item.BUSINESS_LOC,
                            Total = item.Total,
                            QUNT_TOTAL = item.QUNT_TOTAL,
                            CUSTOMER_EMAIL = item.CUSTOMER_EMAIL,
                            CUSTOMER_MOBILE = item.CUSTOMER_MOBILE,
                            CUSTOMER_NAME = item.CUSTOMER_NAME,
                            INVOICE_NO = item.INVOICE_NO,
                            TAX_COLLECTED = item.TAX_COLLECTED,
                            INVOICE_ID = item.INVOICE_ID,
                            ESTIMATE_DATE = item.ESTIMATE_DATE,
                            ITEM_INVOICE_ID = item.ITEM_INVOICE_ID
                        });

                    }
                    //ListGridSales.Clear();

                    ListGridItemSales = _ListGrid_Temp;
                    App.Current.Properties["GetItemsellsDetails"] = _getItemList;
                    App.Current.Properties["ListGridSales"] = _ListGrid_Temp;
                    //App.Current.Properties["ItemNameSales"] = SelectedItem.ITEM_NAME;
                    //App.Current.Properties["ItemCodeSales"] = SelectedItem.ITEM_ID;
                    //ITEM_NAME = App.Current.Properties["ItemNameSales"].ToString();
                    //ITEM_ID = Convert.ToInt32(App.Current.Properties["ItemCodeSales"].ToString());
                    //ListGridSales = App.Current.Properties["ListGridSales"] as List<ItemModel>;
                    ViewItemSalesHistory sh = new ViewItemSalesHistory();
                    sh.DataContext = this;
                    sh.Show();
                }

            }
            else
            {
                MessageBox.Show("Please select item");
                return;
            }
        }
        private string _IMAGE_PATH;
        public string IMAGE_PATH
        {
            get
            {
                return SelectedItem.IMAGE_PATH;
            }
            set
            {
                SelectedItem.IMAGE_PATH = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }
                if (SelectedItem.IMAGE_PATH != value)
                {
                    SelectedItem.IMAGE_PATH = value;
                    OnPropertyChanged("IMAGE_PATH");
                }
            }
        }


        public ICommand _AddItemClick;
        public ICommand AddItemClick
        {
            get
            {
                if (_AddItemClick == null)
                {
                    _AddItemClick = new DelegateCommand(AddItem_Click);
                }
                return _AddItemClick;
            }
        }

        public void AddItem_Click()
        {

            // WelComePage.ItemPRef.Background = System.Windows.Media.Brushes.Red;


            App.Current.Properties["ITemAdd"] = 1;
            App.Current.Properties["Action"] = 123;
            App.Current.Properties["itemName"] = null;
            App.Current.Properties["barcode"] = null;
            App.Current.Properties["BussLocation"] = null;
            App.Current.Properties["Qunt"] = null;
            App.Current.Properties["Godown"] = null;
            ItemAdd IA = new ItemAdd();
            IA.Show();
            // ModalService.NavigateTo(new ItemAdd(), delegate(bool returnValue) { });


        }


        #region AutoComplete Category/Tax/unit

        public ICommand _CatagoryClick;
        public ICommand CatagoryClick
        {
            get
            {
                if (_CatagoryClick == null)
                {
                    _CatagoryClick = new DelegateCommand(CatagoryList_Click);
                }
                return _CatagoryClick;
            }
        }

        public void CatagoryList_Click()
        {
            App.Current.Properties["CatagoryItemReff"] = 1;
            Window_CatagoryList IA = new Window_CatagoryList();
            IA.Show();

        }

        public ICommand _UnitPurchaseClick;
        public ICommand UnitPurchaseClick
        {
            get
            {
                if (_UnitPurchaseClick == null)
                {
                    _UnitPurchaseClick = new DelegateCommand(UnitList_Purchase_Click);
                }
                return _UnitPurchaseClick;
            }
        }

        public void UnitList_Purchase_Click()
        {
            App.Current.Properties["UnitPurchaseClick"] = 1;
            Window_UnitList IA = new Window_UnitList();
            IA.Show();

        }
        public ICommand _UnitSalesClick;
        public ICommand UnitSalesClick
        {
            get
            {
                if (_UnitSalesClick == null)
                {
                    _UnitSalesClick = new DelegateCommand(UnitList_Sales_Click);
                }
                return _UnitSalesClick;
            }
        }

        public void UnitList_Sales_Click()
        {
            App.Current.Properties["UnitSalesClick"] = 2;
            Window_UnitList IA = new Window_UnitList();
            IA.Show();

        }

        #endregion


        #region Delete

        public ICommand _DeleteItem;
        public ICommand DeleteItem
        {
            get
            {
                if (_DeleteItem == null)
                {
                    _DeleteItem = new DelegateCommand(Delete_Item);
                }
                return _DeleteItem;
            }
        }
        public async void Delete_Item()
        {
            if (SelectedItem.ITEM_ID != null && SelectedItem.ITEM_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure to delete this item  " + SelectedItem.ITEM_NAME + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    var id = SelectedItem.ITEM_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/ItemAPI/DeleteItem?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Item Deleted Successfully");
                        ModalService.NavigateTo(new Items(), delegate (bool returnValue) { });

                    }
                }
                else
                {
                    Cancel_Item();
                }
            }
            else
            {
                MessageBox.Show("Select Item");
            }

        }
        #endregion


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

        ItemModel[] data1 = null;
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
                    data1 = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void GetSelectionAutoComplete()
        {
            try
            {
                if (SelectedItem.CATAGORY_ID == 0)
                {
                    App.Current.Properties["SelectCat"] = App.Current.Properties["SelectCat"].ToString();
                    if (App.Current.Properties["SelectCat"] != null)
                    {
                        SelectedItem.CATAGORY_ID = Convert.ToInt64(App.Current.Properties["SelectCat"].ToString());
                        SelectedItem.CATEGORY_NAME = App.Current.Properties["SelectCatName"].ToString();
                        App.Current.Properties["SelectCat"] = null;
                        App.Current.Properties["SelectCatName"] = null;
                    }

                }
                else
                {

                    SelectedItem.CATAGORY_ID = Convert.ToInt64(App.Current.Properties["CatagoryItemReff"].ToString());
                    SelectedItem.CATEGORY_NAME = ItemAdd.CatRef.Text;
                    App.Current.Properties["CatagoryItemReff"] = null;
                }

                //if (SelectedItem.BUSS_LOC_ID == null)
                //{
                if (App.Current.Properties["BussinessId"] != null)
                {

                    SelectedItem.BUSS_LOC_ID = Convert.ToInt32(Convert.ToInt64(App.Current.Properties["BussinessId"].ToString()));
                    // SelectedItem.BUSS_LOC_ID = Convert.ToInt32(Convert.ToInt64(App.Current.Properties["BussinessId"].ToString()));

                    SelectedItem.BUSINESS_LOC = App.Current.Properties["BussLocName"].ToString();

                    App.Current.Properties["BussinessId"] = null;
                    App.Current.Properties["BussLocName"] = null;
                }
                //}
                else
                {
                    SelectedItem.BUSS_LOC_ID = Convert.ToInt32(Convert.ToInt64(App.Current.Properties["BussMainReff"].ToString()));
                    SelectedItem.BUSS_LOC_ID = Convert.ToInt32(Convert.ToInt64(App.Current.Properties["BussMainReff"].ToString()));
                    App.Current.Properties["BussMainReff"] = null;
                }



                if (App.Current.Properties["GoDownId"] != null)
                {

                    SelectedItem.GODOWN_ID = Convert.ToInt32(App.Current.Properties["GoDownId"].ToString());

                    SelectedItem.GODOWN = App.Current.Properties["GoDownName"].ToString();

                    App.Current.Properties["GoDownName"] = null;
                    App.Current.Properties["GoDownId"] = null;

                }
                else
                {
                    SelectedItem.GODOWN_ID = Convert.ToInt32(App.Current.Properties["GodownOpStockRef"].ToString());

                    App.Current.Properties["GodownOpStockRef"] = null;

                }

                if (App.Current.Properties["TaxCollectid"] != null)
                {

                    SelectedItem.TAX_COLLECTED_ID = Convert.ToInt32(App.Current.Properties["TaxCollectid1"].ToString());
                    SelectedItem.TAX_COLLECTED_NAME = App.Current.Properties["TaxCollectName"].ToString();
                    //App.Current.Properties["TaxCollectName"] = null;
                    //App.Current.Properties["TaxCollectid"] = null;
                }
                else
                {
                    SelectedItem.TAX_COLLECTED_ID = Convert.ToInt32(App.Current.Properties["TaxListSellingClick"].ToString());

                    App.Current.Properties["TaxListSellingClick"] = null;

                }

                if (App.Current.Properties["TaxPaidid"] != null)
                {

                    SelectedItem.TAX_PAID_ID = Convert.ToInt32(App.Current.Properties["TaxPaidid"].ToString());

                    SelectedItem.TAX_PAID_NAME = App.Current.Properties["TaxPaidName"].ToString();
                    App.Current.Properties["TaxPaidName"] = null;
                    App.Current.Properties["TaxPaidid"] = null;
                }
                else
                {
                    SelectedItem.TAX_PAID_ID = Convert.ToInt32(App.Current.Properties["TaxListPurchasingClick"].ToString());

                    App.Current.Properties["TaxListPurchasingClick"] = null;

                }




                if (App.Current.Properties["PurchaseUnitId"] != null)
                {
                    SelectedItem.UNIT_PURCHAGE_ID = Convert.ToInt32(App.Current.Properties["PurchaseUnitId"].ToString());
                    SelectedItem.PURCHASE_UNIT = App.Current.Properties["PurchaseUnitName"].ToString();
                    App.Current.Properties["PurchaseUnitName"] = null;
                    App.Current.Properties["PurchaseUnitId"] = null;
                }
                else
                {

                    SelectedItem.UNIT_PURCHAGE_ID = Convert.ToInt32(App.Current.Properties["UnitPurchaseClick"].ToString());

                    App.Current.Properties["UnitPurchaseClick"] = null;
                }


                if (App.Current.Properties["SalesUnitId"] != null)
                {
                    SelectedItem.UNIT_SALES_ID = Convert.ToInt32(App.Current.Properties["SalesUnitId"].ToString());
                    SelectedItem.SALES_UNIT = App.Current.Properties["SalesUnitName"].ToString();
                    App.Current.Properties["SalesUnitId"] = null;
                    App.Current.Properties["SalesUnitName"] = null;
                }
                else
                {
                    SelectedItem.UNIT_SALES_ID = Convert.ToInt32(App.Current.Properties["UnitSalesClick"].ToString());

                    App.Current.Properties["UnitSalesClick"] = null;
                }
            }
            catch (Exception ex)
            {
            }

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

        public async void taxcollect()
        {


            if (App.Current.Properties["TaxCollectid1"] != null)
            {
                SelectedItem.TAX_COLLECTED_ID = Convert.ToInt32(App.Current.Properties["TaxCollectid1"].ToString());
                SelectedItem.TAX_COLLECTED_NAME = App.Current.Properties["TaxCollectName"].ToString();
                _SALES_PRICE = Convert.ToDecimal(ItemAdd.SalesPrice.Text);
                _SALES_PRICE_BEFOR_TAX = Convert.ToDecimal(ItemAdd.SalesPriceBeforeTax.Text);
            }

            int collid = Convert.ToInt32(App.Current.Properties["TaxCollectid1"]);
            if (collid != 0)
            {
                //listpaid = App.Current.Properties["TaxPaList"] as List<TaxManagementModel>;
                listpaid = App.Current.Properties["TaxPaList"] as ObservableCollection<TaxManagementModel>;

                TAX_COLLECTED = (from m in listpaid where m.TAX_ID == collid select m.TAX_VALUE).FirstOrDefault();
            }

            if (ItemAdd.chk.IsChecked == true)
            {

                decimal FinaliseAmt = 0;
                FinaliseAmt = ((_SALES_PRICE * 100) / (Convert.ToDecimal(TAX_COLLECTED) + 100));
                SelectedItem.SALES_PRICE_BEFOR_TAX = FinaliseAmt;
                ItemAdd.SalesPriceBeforeTax.Text = FinaliseAmt.ToString();
            }

            if (ItemAdd.chk.IsChecked == false)
            {
                decimal FinaliseAmt = 0;
                FinaliseAmt = ((Convert.ToDecimal(ItemAdd.SalesPriceBeforeTax.Text) * (Convert.ToDecimal(TAX_COLLECTED) + 100) / 100));
                ItemAdd.SalesPrice.Text = FinaliseAmt.ToString();
            }
        }

        public async Task<ObservableCollection<ItemModel>> GetItem(long comp)
        {
            try
            {
                var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                // get your 'Uploaded' folder
                var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));

                List<ItemModel> _ListGrid_Temp = new List<ItemModel>();
                ItemData = new List<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new ItemModel
                        {
                            SLNO = x,
                            IMAGE_PATH = data[i].IMAGE_PATH,
                            //IMAGE_PATH = System.IO.Path.Combine(dir.FullName + "\\", data[3].IMAGE_PATH),
                            // IMAGE_PATH="C:/Users/Suvendu/Desktop/24.04.2017/InvoicePOS/InvoicePOS/bin/Debug/uploaded/319200865402.jpg",
                            ITEM_NAME = data[i].ITEM_NAME,
                            ITEM_LOCATION = data[i].ITEM_LOCATION,
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
                            OPN_QNT = data[i].OPN_QNT,
                            REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE,
                            SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX,
                            MODIFICATION_DATE = data[i].MODIFICATION_DATE,
                            IS_ACTIVE = data[i].IS_ACTIVE,

                        });
                    }



                    if (IS_ACTIVESearch == true)
                    {
                        var item1 = (from m in _ListGrid_Temp where m.IS_ACTIVE == false select m).ToList();
                        _ListGrid_Temp = null;
                        _ListGrid_Temp = item1;
                    }
                    else
                    {

                        var item1 = (from m in _ListGrid_Temp select m).ToList();
                        _ListGrid_Temp = null;
                        _ListGrid_Temp = item1;
                    }
                    if (ACTIVESearch == true)
                    {
                        var item1 = (from m in _ListGrid_Temp where m.IS_ACTIVE == true select m).ToList();
                        _ListGrid_Temp = null;
                        _ListGrid_Temp = item1;
                    }
                    else
                    {

                        var item1 = (from m in _ListGrid_Temp select m).ToList();
                        _ListGrid_Temp = null;
                        _ListGrid_Temp = item1;
                    }
                    if (IS_ACTIVESearch == true)
                    {
                        _IMAGE_VISIBLE = "Visible";
                        var item1 = (from m in _ListGrid_Temp where m.IS_ACTIVE == false select m).ToList();
                        _ListGrid_Temp = null;
                        _ListGrid_Temp = item1;

                    }

                    if (SEARCH_ITEM != "" && SEARCH_ITEM != null)
                    {


                        var item1 = (from m in _ListGrid_Temp where m.BARCODE.Contains(SEARCH_ITEM) || m.ITEM_NAME.Contains(SEARCH_ITEM) || m.SEARCH_CODE.Contains(SEARCH_ITEM) select m).ToList();
                        if (item1 != null)
                        {
                            if (item1.Count > 0)
                            {
                                _ListGrid_Temp = item1;
                            }
                        }

                    }
                    if (SEARCH_ITEM != "" && SEARCH_ITEM != null && IS_ACTIVESearch == true)
                    {
                        var item1 = (from m in _ListGrid_Temp where m.IS_ACTIVE == false && m.BARCODE.Contains(SEARCH_ITEM) select m).ToList();
                        _ListGrid_Temp = null;
                        _ListGrid_Temp = item1;
                    }
                    if (SEARCH_ITEM != "" && SEARCH_ITEM != null)
                    {
                        var item1 = (from m in _ListGrid_Temp where m.IS_ACTIVE == true && m.BARCODE.Contains(SEARCH_ITEM) select m).ToList();
                        _ListGrid_Temp = null;
                        _ListGrid_Temp = item1;
                    }

                }
                ListGrid = _ListGrid_Temp;
                return new ObservableCollection<ItemModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private bool _dgVisibility;
        public bool DataGridVisibility
        {
            get { return _dgVisibility; }
            set
            {
                _dgVisibility = value;
                OnPropertyChanged("DataGridVisibility");
            }
        }


        private string _SEARCH_ITEM;
        public string SEARCH_ITEM
        {
            get
            {
                return _SEARCH_ITEM;
            }
            set
            {
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                //SEARCH_ITEM = value;
                _SEARCH_ITEM = value;
                if (_SEARCH_ITEM != "" && _SEARCH_ITEM != null)
                {
                    GetItem(comp);
                }
                else
                {
                    _SEARCH_ITEM = null;
                }
                OnPropertyChanged("SEARCH_ITEM");
            }
        }

        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedItem.COMPANY_ID;
            }
            set
            {
                SelectedItem.COMPANY_ID = value;

                if (SelectedItem.COMPANY_ID != value)
                {
                    SelectedItem.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }
        private string _CUSTOMER_EMAIL;
        public string CUSTOMER_EMAIL 
        {
            get 
            {
                return SelectedItem.CUSTOMER_EMAIL;

            }
            set
            {
                SelectedItem.CUSTOMER_EMAIL = value;

                if (SelectedItem.CUSTOMER_EMAIL != value)
                {
                    SelectedItem.CUSTOMER_EMAIL = value;
                    OnPropertyChanged("CUSTOMER_EMAIL");
                }
            }
        }
        private string _CUSTOMER_MOBILE;
        public string CUSTOMER_MOBILE
        {
            get
            {
                return SelectedItem.CUSTOMER_MOBILE;

            }
            set
            {
                SelectedItem.CUSTOMER_MOBILE = value;

                if (SelectedItem.CUSTOMER_MOBILE != value)
                {
                    SelectedItem.CUSTOMER_MOBILE = value;
                    OnPropertyChanged("CUSTOMER_MOBILE");
                }
            }
        }
        private decimal _TOTAL_SUM;
        public decimal TOTAL_SUM
        {
            get
            {
                return SelectedItem.TOTAL_SUM;

            }
            set
            {
                SelectedItem.TOTAL_SUM = value;

                if (SelectedItem.TOTAL_SUM != value)
                {
                    SelectedItem.TOTAL_SUM = value;
                    OnPropertyChanged("TOTAL_SUM");
                }
            }
        }
        private int _TOTAL_QTY;
        public int TOTAL_QTY
        {
            get
            {
                return SelectedItem.TOTAL_QTY;

            }
            set
            {
                SelectedItem.TOTAL_QTY = value;

                if (SelectedItem.TOTAL_QTY != value)
                {
                    SelectedItem.TOTAL_QTY = value;
                    OnPropertyChanged("TOTAL_QTY");
                }
            }
        }
        private int _TOTAL_ITEM;
        public int TOTAL_ITEM
        {
            get
            {
                return SelectedItem.TOTAL_ITEM;

            }
            set
            {
                SelectedItem.TOTAL_ITEM = value;

                if (SelectedItem.TOTAL_ITEM != value)
                {
                    SelectedItem.TOTAL_ITEM = value;
                    OnPropertyChanged("TOTAL_ITEM");
                }
            }
        }
        private string _INVOICE_NO;
        public string INVOICE_NO
        {
            get
            {
                return SelectedItem.INVOICE_NO;

            }
            set
            {
                SelectedItem.INVOICE_NO = value;

                if (SelectedItem.INVOICE_NO != value)
                {
                    SelectedItem.INVOICE_NO = value;
                    OnPropertyChanged("INVOICE_NO");
                }
            }
        }
        private int _INVOICE_ID;
        public int INVOICE_ID
        {
            get
            {
                return SelectedItem.INVOICE_ID.Value;

            }
            set
            {
                SelectedItem.INVOICE_ID = value;

                if (SelectedItem.INVOICE_ID != value)
                {
                    SelectedItem.INVOICE_ID = value;
                    OnPropertyChanged("INVOICE_NO");
                }
            }
        }
        public ICommand _SearchItem;
        public ICommand SearchItem
        {
            get
            {
                if (_SearchItem == null)
                {
                    _SearchItem = new DelegateCommand(Search_Item);
                }
                return _SearchItem;
            }
        }

        public async void Search_Item()
        {
            try
            {
                List<ItemModel> _ListGrid_Temp = new List<ItemModel>();
                //_ListGrid_Temp = 0;
                App.Current.Properties["Action"] = "Search";
                string comp = SelectedItem.SEARCH_ITEM;
                ItemData = new List<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/SearchAllItem?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());


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
                            OPN_QNT = data[i].OPN_QNT,
                            REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE,
                            SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX,
                            MODIFICATION_DATE = data[i].MODIFICATION_DATE,
                        });
                    }
                }
                ListGrid = _ListGrid_Temp;
                // var dataw = await _ListGrid_Temp.ToList();//.ToListAsync();
                // return new ObservableCollection<ItemModel>(dataw);
                //App.Current.Properties["ItemSearch"] = SelectedItem;
                // ModalService.NavigateTo(new Items(), delegate(bool returnValue) { });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #region opening stock
        private OpeningStockModel _SelectedOpeningStock;
        public OpeningStockModel SelectedOpeningStock
        {
            get { return _SelectedOpeningStock; }
            set
            {
                if (_SelectedOpeningStock != value)
                {
                    _SelectedOpeningStock = value;

                    OnPropertyChanged("SelectedOpeningStock");
                }
            }
        }
        private POrderModel _SelectedPO;
        public POrderModel SelectedPO
        {
            get { return _SelectedPO; }
            set
            {
                if (_SelectedPO != value)
                {
                    _SelectedPO = value;
                    OnPropertyChanged("SelectedPO");
                }

            }
        }


        public ICommand _OpeningStockClick;
        public ICommand OpeningStockClick
        {
            get
            {
                if (_OpeningStockClick == null)
                {
                    _OpeningStockClick = new DelegateCommand(peningStock_Click);
                }
                return _OpeningStockClick;
            }
        }

        public void peningStock_Click()
        {
            if (SelectedItem.ITEM_ID != 0 && SelectedItem.ITEM_ID != null)
            {
                App.Current.Properties["Action"] = "Edit";
            }
            OPN_QNT = Convert.ToInt32(App.Current.Properties["Qunt"]);
            App.Current.Properties["GoDownName"] = SelectedItem.GODOWN;

            App.Current.Properties["GoDownId"] = SelectedItem.GODOWN_ID;

            App.Current.Properties["BussinessId"] = SelectedItem.BUSS_LOC_ID;
            App.Current.Properties["BussLocName"] = SelectedItem.BUSINESS_LOC;


            Window_Opening_stock IA = new Window_Opening_stock();

            Window_Opening_stock.BussRef.Text = null;
            Window_Opening_stock.BussRef.Text = SelectedItem.BUSINESS_LOC;
            Window_Opening_stock.GodownRef.Text = null;
            Window_Opening_stock.GodownRef.Text = SelectedItem.GODOWN;
            IA.ShowDialog();
            //if (App.Current.Properties["GodownOpStockRef"] != null)
            //{

            //    App.Current.Properties["GoDownName"] = Window_Opening_stock.GodownRef.Text;
            //    SelectedItem.GODOWN = Window_Opening_stock.GodownRef.Text;
            //    SelectedItem.GODOWN_ID = Convert.ToInt32(App.Current.Properties["GodownOpStockRef"]);
            //    App.Current.Properties["GoDownId"] = SelectedItem.GODOWN_ID;

            //}


            //if (App.Current.Properties["BussLocationRef"] != null)
            //{
            //    SelectedItem.BUSINESS_LOC = Window_Opening_stock.BussRef.Text;
            //    SelectedItem.BUSINESS_LOC_ID = Convert.ToInt32(App.Current.Properties["BussLocidRef"]);
            //    //Window_Opening_stock.BussRef.Text = SelectedBusinessLoca.SHOP_NAME;
            //}


        }

        public ICommand _SelectOk { get; set; }
        public ICommand SelectOk
        {
            get
            {
                if (_SelectOk == null)
                {
                    _SelectOk = new DelegateCommand(Select_Ok);
                }
                return _SelectOk;
            }
        }

        public async void GetPOList1(int comp)
        {
            //public async Task<ObservableCollection<ItemModel>> GetPOList1(int comp)
            ////{
            //ObservableCollection<POrderModel> _ListGrid_Temp1 = new ObservableCollection<POrderModel>();
            //ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
            //ItemData = new ObservableCollection<ItemModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data1 = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                if (data1.Length > 0)
                {
                    for (int i = 0; i < data1.Length; i++)
                    {
                        _ListGrid_Temp1.Add(new POrderModel
                        {
                            // ITEM_ID = ItemId,
                            //Discount = data1[i].Discount,
                            SLNO = i + 1,
                            ITEM_NAME = data1[i].ITEM_NAME,
                            //ITEM_ID = data1[i].ITEM_ID,
                            BAR_CODE = data1[i].BARCODE,
                            PURCHASE_UNIT_PRICE = data1[i].PURCHASE_UNIT_PRICE,
                            MRP = data1[i].MRP,
                            //ACCESSORIES_KEYWORD = data1[i].ACCESSORIES_KEYWORD,
                            //CATAGORY_ID = data1[i].CATAGORY_ID,
                            //ITEM_DESCRIPTION = data1[i].ITEM_DESCRIPTION,
                            //ITEM_INVOICE_ID = data1[i].ITEM_INVOICE_ID,
                            //ITEM_PRICE = data1[i].ITEM_PRICE,
                            //ITEM_PRODUCT_ID = data1[i].ITEM_PRODUCT_ID,
                            //KEYWORD = data1[i].KEYWORD,
                            //MRP = data1[i].MRP,
                            //PURCHASE_UNIT = data1[i].PURCHASE_UNIT,
                            //PURCHASE_UNIT_PRICE = data1[i].PURCHASE_UNIT_PRICE,
                            //SALES_PRICE = data1[i].SALES_PRICE,
                            //SALES_UNIT = data1[i].SALES_UNIT,
                            //SEARCH_CODE = data1[i].SEARCH_CODE,
                            //TAX_COLLECTED = data1[i].TAX_COLLECTED,
                            TAX_PAID = data1[i].TAX_PAID,
                            //TAX_PAID = data1[i].SALES_PRICE,
                            //ALLOW_PURCHASE_ON_ESHOP = data1[i].ALLOW_PURCHASE_ON_ESHOP,
                            //CATEGORY_NAME = data1[i].CATEGORY_NAME,
                            //DISPLAY_INDEX = data1[i].DISPLAY_INDEX,
                            //INCLUDE_TAX = data1[i].INCLUDE_TAX,
                            //ITEM_GROUP_NAME = data1[i].ITEM_GROUP_NAME,
                            //ITEM_UNIQUE_NAME = data1[i].ITEM_UNIQUE_NAME,
                            //Current_Qty = 1,
                            //OPN_QNT = data1[i].OPN_QNT,
                            //REGIONAL_LANGUAGE = data1[i].REGIONAL_LANGUAGE,
                            //SALES_PRICE_BEFOR_TAX = data1[i].SALES_PRICE_BEFOR_TAX,
                            //TaxName = data1[i].TaxName,
                            //TaxValue = data1[i].TaxValue,
                            //Total = ((decimal)(data[i].OPN_QNT) * (data[i].SALES_PRICE)) + (data[i].SALES_PRICE - data[i].PURCHASE_UNIT_PRICE),

                            //Total = ((decimal)(data1[i].OPN_QNT) * (data1[i].SALES_PRICE)),

                            //ListGrid[i].PURCHASE_PRICE_BEFOR_TAX_QTY = Convert.ToDecimal(ListGrid[i].Current_Qty * ListGrid[i].SALES_PRICE_BEFOR_TAX);
                            //PURCHASE_PRICE_BEFORE_TAX = ((PURCHASE_UNIT_PRICE * 100) / (Convert.ToDecimal(TAX_COLLECTED) + 100)),

                            PURCHASE_PRICE_BEFORE_TAX = ((decimal)(data1[i].PURCHASE_UNIT_PRICE * 100) / ((data1[i].TAX_PAID) + 100)),
                            //SUB_TOTAL_AFTR_TAX = ((decimal)(data1[i].TOTAL_QNT) * (data[i].PURCHASE_PRICE_BEFOR_TAX)),

                            //SUB_TOTAL_AFTER_TAX = ((TOTAL_QTY) * (data1[i].PURCHASE_UNIT_PRICE)),
                            //SUB_TOTAL_BEFORE_TAX = ((TOTAL_QTY) * (PURCHASE_PRICE_BEFORE_TAX)),
                        });
                    }


                    //if (Select_BarCode != null && Select_BarCode != "")
                    //{

                    if (AddPO.ItemRef.Text != null && AddPO.ItemRef.Text != "")
                    {
                        //App.Current.Properties["ManualBarcode"] = Select_BarCode;
                        //var itemToRemove = (from m in _ListGrid_Temp where m.BAR_CODE == Select_BarCode select m).ToList();

                        var item = (from m in _ListGrid_Temp1 where m.ITEM_NAME == AddPO.ItemRef.Text select m).ToList();
                        //ObservableCollection<POrderModel> myCollection = new ObservableCollection<POrderModel>(item);
                        //var item1 = (from a in ListGrid where a.BAR_CODE == Select_BarCode select a).FirstOrDefault();

                        //int opqunt = 0;
                        //if (itemToRemove.Count != 0)
                        //{
                        //    opqunt = (int)itemToRemove.ElementAt(0).Current_Qty;
                        //}

                        _ListGrid_Temp1 = item;

                        if (item != null)
                        {
                            //if (item1.Count > 0)
                            //{
                            App.Current.Properties["DataGridL1"] = item;
                            //_ListGrid_Temp = (item);
                            //ListGrid1 = _ListGrid_Temp;
                            //Calculation();

                            //SUB_TOTAL_AFTER_TAX = ((TOTAL_QTY) * (PURCHASE_UNIT_PRICE));
                            //SUB_TOTAL_BEFORE_TAX = ((TOTAL_QTY) * (PURCHASE_PRICE_BEFORE_TAX));
                            AddPO.ListGridRef.ItemsSource = (item);

                            //AddPO.ListGridRef= _ListGrid_Temp1.ToList();
                            //Calculation();
                            //ListGrid1 = _ListGrid_Temp;

                            //}
                        }

                    }

                }
            }

        }


        public void Select_Ok()
        {
            var fdrt = App.Current.Properties["OppingDiffProperties"] as OpeningStockModel;
            SelectedOpeningStock.ITEM_NAME = SelectedItem.ITEM_NAME;
            SelectedOpeningStock.SEARCH_CODE = SelectedItem.SEARCH_CODE;
            App.Current.Properties["ItemSearchbarcode"] = SelectedItem.BARCODE;

            //SelectedPO.ITEM_NAME = SelectedItem.ITEM_NAME;

            if (App.Current.Properties["ItemSearchMain"] != null)
            {
                if (App.Current.Properties["ItemSearchMain"].ToString() == "1")
                {

                    Main.ScrRef.Text = null;
                    Main.ScrRef.Text = SelectedItem.SEARCH_CODE;
                    App.Current.Properties["ItemMain"] = "0";
                }
            }
            if (App.Current.Properties["ItemMain"] != null)
            {
                if (App.Current.Properties["ItemMain"].ToString() == "1")
                {
                    Main.NameRef1.Text = null;
                    Main.NameRef1.Text = SelectedItem.ITEM_NAME;
                    App.Current.Properties["ItemSearchMain"] = 0;


                }
            }



            if (fdrt != null)
            {
                SelectedOpeningStock.GODOWN = fdrt.GODOWN;
                SelectedOpeningStock.COMPANY_NAME = fdrt.COMPANY_NAME;

            }
            App.Current.Properties["OppingDiffProperties"] = SelectedOpeningStock;
            if (Openingbalance.BussRef != null)
            {
                Openingbalance.BussRef.Text = null;
                Openingbalance.BussRef.Text = SelectedOpeningStock.BUSINESS_LOC;
            }
            //if (AddPO.BussRef != null)
            //{
            //    AddPO.BussRef.Text = null;
            //    AddPO.BussRef.Text = SelectedOpeningStock.BUSINESS_LOC;
            //}
            //if (AddPO.ItemRef != null)
            //{
            //    AddPO.ItemRef.Text = null;
            //    AddPO.ItemRef.Text = SelectedOpeningStock.ITEM_NAME;

            //}

            //if (AddPO.ScrRef != null)
            //{
            //    AddPO.ScrRef.Text = null;           
            //}




            if (App.Current.Properties["ItemList"] != null)
            {
                AddPO.ItemRef.Text = SelectedOpeningStock.ITEM_NAME;
                AddPO.ScrRef.Text = null;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                POViewModel itn = new POViewModel();
                itn.GetPOList1(comp);
                App.Current.Properties["ItemList"] = null;
            }

            if (App.Current.Properties["Search"] != null)
            {
                AddPO.ScrRef.Text = SelectedOpeningStock.SEARCH_CODE;
                AddPO.ItemRef.Text = null;
                //App.Current.Properties["AddPOItem"] = SelectedPO.ITEM_NAME;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                POViewModel sc = new POViewModel();
                sc.GetPOList1(comp);
                App.Current.Properties["Search"] = null;
            }


            if (App.Current.Properties["RecItemItemReff"] != null)
            {
                ReceiveAddItem.ItemReff.Text = SelectedOpeningStock.ITEM_NAME;
                ReceiveAddItem.SearchItemReff.Text = null;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                ReceiveItemViewModel itn = new ReceiveItemViewModel();
                //itn.GetItemList(comp);
                App.Current.Properties["RecItemItemReff"] = null;
            }


            if (App.Current.Properties["RecItemSearchReff"] != null)
            {
                ReceiveAddItem.SearchItemReff.Text = SelectedOpeningStock.SEARCH_CODE;
                ReceiveAddItem.ItemReff.Text = null;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                ReceiveItemViewModel sc = new ReceiveItemViewModel();
                //  sc.GetItemList(comp);
                App.Current.Properties["RecItemSearchReff"] = null;
            }


            if (App.Current.Properties["itemre"] != null)
            {
                if (AddStockTransfer.ItemRef1 != null)
                {
                    AddStockTransfer.ItemRef1.Text = null;
                    AddStockTransfer.ItemRef1.Text = SelectedOpeningStock.ITEM_NAME;
                    App.Current.Properties["StockItemName"] = SelectedOpeningStock.ITEM_NAME;

                    App.Current.Properties["itemre"] = null;

                }
            }
            //if (App.Current.Properties["ItemMain"] != null)
            //{
            //    Main.ItemMainReff.Text = null;
            //    Main.ItemMainReff.Text = SelectedItem.ITEM_NAME;
            //}
            //if (App.Current.Properties["ItemSearchMain"] != null)
            //{
            //    Main.ItemSearchMainReff.Text = null;
            //    Main.ItemSearchMainReff.Text = SelectedItem.SEARCH_CODE;
            //}
            if (App.Current.Properties["itemcode"] != null)
            {


                if (AddStockTransfer.ItemRef2 != null)
                {

                    AddStockTransfer.ItemRef2.Text = null;
                    AddStockTransfer.ItemRef2.Text = SelectedOpeningStock.SEARCH_CODE;
                    App.Current.Properties["StockSearchCode"] = SelectedOpeningStock.SEARCH_CODE;
                    App.Current.Properties["StockSearchCodebar"] = SelectedOpeningStock.BARCODE;
                    App.Current.Properties["itemcode"] = null;

                }
            }
            //if (AddPO.SuppRef != null)
            //{
            //    AddPO.SuppRef.Text = null;
            //    AddPO.SuppRef.Text = SelectedPO.SUPPLIER;
            //}
            if (Window_Opening_stock.GodownRef != null)
            {
                Window_Opening_stock.GodownRef.Text = null;
                Window_Opening_stock.GodownRef.Text = Convert.ToString(SelectedOpeningStock.GODOWN);
            }
            if (App.Current.Properties["ItemMainStock"] != null)
            {
                ShowStockList.ItemNameReff.Text = null;
                ShowStockList.ItemNameReff.Text = Convert.ToString(SelectedOpeningStock.ITEM_NAME);
            }
            if (App.Current.Properties["RecItemItemReff"] != null)
            {
                ReceiveAddItem.ItemReff.Text = null;
                ReceiveAddItem.ItemReff.Text = SelectedOpeningStock.ITEM_NAME;
                App.Current.Properties["RecItemItemReff"] = null;
            }
            if (App.Current.Properties["RecItemSearchReff"] != null)
            {
                ReceiveAddItem.SearchItemReff.Text = null;
                ReceiveAddItem.SearchItemReff.Text = SelectedOpeningStock.SEARCH_CODE;
                App.Current.Properties["SearchReffRecItem"] = SelectedOpeningStock.SEARCH_CODE;
                App.Current.Properties["ItemNameRecItem"] = SelectedOpeningStock.ITEM_NAME;
                App.Current.Properties["RecItemSearchReff"] = null;
            }
            if (App.Current.Properties["ItemLocationReff"] != null)
            {
                ItemLocationAdd.ItemLocationReff.Text = null;
                ItemLocationAdd.ItemLocationReff.Text = SelectedOpeningStock.SEARCH_CODE;
                //ItemLocationAdd.ItemLocReff.Text = SelectedItem.ITEM_LOCATION;

                App.Current.Properties["ItemLocationReff"] = null;
            }
            if (App.Current.Properties["ItemStockLadger"] != null)
            {
                StockLedgerList.ItemStockLadger.Text = null;
                StockLedgerList.ItemStockLadger.Text = SelectedOpeningStock.ITEM_NAME;
                App.Current.Properties["ItemIdStockLadger"] = SelectedOpeningStock.ITEM_ID;
                App.Current.Properties["ItemStockLadger"] = null;

            }
            Cancel_Item();
        }

        #endregion

        #region Insert Item
        public ICommand _InsertItem;
        public ICommand InsertItem
        {
            get
            {
                if (_InsertItem == null)
                {
                    _InsertItem = new DelegateCommand(Insert_Item);
                }
                return _InsertItem;
            }
        }


        public async void Insert_Item()
        {
            try
            {
                if (SEARCH_CODE != null && SEARCH_CODE != "")
                {
                    GetSelectionAutoComplete();
                    BarcodeUnique();
                    SearchCodeUnique();
                    if (data.Count() != 0)
                    {
                        MessageBox.Show("" + BARCODE + "Barcode is already present");
                    }

                    else if (data1.Count() != 0)
                    {
                        MessageBox.Show("" + SEARCH_CODE + "Search Code is already present");
                    }
                    else
                    {
                        if (App.Current.Properties["TaxCollectid"] != null)
                        {
                            SelectedItem.TAX_COLLECTED_ID = Convert.ToInt32(App.Current.Properties["TaxCollectid"].ToString());
                        }
                        if (App.Current.Properties["itemName"] != null)
                        {
                            ITEM_NAME = App.Current.Properties["itemName"].ToString();
                        }
                        if (App.Current.Properties["BussLocation"] != null)
                        {
                            BUSINESS_LOC = App.Current.Properties["BussLocation"].ToString();
                        }
                        if (App.Current.Properties["Qunt"] != null)
                        {
                            SelectedItem.OPN_QNT = Convert.ToInt32(App.Current.Properties["Qunt"].ToString());
                        }
                        if (App.Current.Properties["Godown"] != null)
                        {
                            GODOWN = App.Current.Properties["Godown"].ToString();
                        }
                        if (App.Current.Properties["Date"] != null)
                        {
                            SelectedItem.DATE = Convert.ToDateTime(App.Current.Properties["Date"]);
                        }
                        if (SelectedItem.ITEM_NAME == "" || SelectedItem.ITEM_NAME == null)
                        {
                            MessageBox.Show("Item Name is missing");
                        }
                        //if (App.Current.Properties["GodownOpStockRef"] != null)
                        //{
                        //    SelectedItem.GODOWN_ID = Convert.ToInt32(App.Current.Properties["GodownOpStockRef"]);
                        //}

                        else if (SelectedItem.PURCHASE_UNIT_PRICE > SelectedItem.SALES_PRICE_BEFOR_TAX)
                        {
                            if (ENABLE_BEFOR_TAX == true)
                            {
                                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Do you want to keep 'Sales Price' less than 'Purchase Price'?", "Information", System.Windows.MessageBoxButton.YesNo);
                                ItemAdd.PurchasePrice.Focus();
                                return;
                            }

                        }
                        else if (SelectedItem.PURCHASE_UNIT_PRICE > SelectedItem.SALES_PRICE)
                        {
                            if (ENABLE_SALES_PRICE == true)
                            {
                                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Do you want to keep 'Sales Price' less than 'Purchase Price'?", "Information", System.Windows.MessageBoxButton.YesNo);
                                ItemAdd.PurchasePrice.Focus();
                                return;
                            }

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
                        else if (SelectedItem.SALES_PRICE == 0 || SelectedItem.SALES_PRICE == null)
                        {
                            MessageBox.Show("Sales Prices is missing");
                        }
                        else if (SelectedItem.OPN_QNT == 0 || SelectedItem.OPN_QNT == null)
                        {
                            MessageBox.Show("missing Op. Stock");
                            Window_Opening_stock IA = new Window_Opening_stock();
                            IA.ShowDialog();
                        }
                        else if (SelectedItem.MRP == 0 || SelectedItem.MRP == null)
                        {
                            MessageBox.Show("MRP is missing");
                        }
                        else if (SelectedItem.PURCHASE_UNIT_PRICE == null && SelectedItem.PURCHASE_UNIT_PRICE == 0)
                        {
                            MessageBox.Show("Purchase price is missing");
                        }
                        else
                        {

                            App.Current.Properties["Action"] = "Add";
                            SelectedItem.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                            HttpClient client = new HttpClient();
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
                            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                            var response = await client.PostAsJsonAsync("api/ItemAPI/CreateItem/", SelectedItem);
                            if (response.StatusCode.ToString() == "OK")
                            {

                                MessageBox.Show("Item Added Successfully");
                                Cancel_Item();
                                App.Current.Properties["SelectCat"] = null;
                                App.Current.Properties["itemName"] = null;
                                App.Current.Properties["barcode"] = null;
                                App.Current.Properties["BussLocation"] = null;
                                App.Current.Properties["Qunt"] = null;
                                App.Current.Properties["Godown"] = null;
                                ModalService.NavigateTo(new Items(), delegate (bool returnValue) { });
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Search Code is missing");
                }
            }
            catch
            {

            }

        }

        #endregion

        #region Edit/View
        public ICommand _EditItem { get; set; }
        public ICommand EditItem
        {
            get
            {
                if (_EditItem == null)
                {
                    _EditItem = new DelegateCommand(Edit_Item);
                }
                return _EditItem;
            }
        }
        public async void Edit_Item()
        {
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
                // ItemAdd nh = new ItemAdd();
                if (SelectedItem.ITEM_NAME != null && SelectedItem.ITEM_NAME != "")
                {
                    App.Current.Properties["itemName"] = null;
                    App.Current.Properties["barcode"] = null;
                    App.Current.Properties["BussLocation"] = null;
                    App.Current.Properties["Qunt"] = null;
                    App.Current.Properties["Godown"] = null;
                    App.Current.Properties["Action"] = "Edit";
                    ItemData = new List<ItemModel>();
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
                                // SelectedItem.ITEM_ID1 = data[i].ITEM_ID;
                                SelectedItem.IMAGE_PATH = data[i].IMAGE_PATH;
                                SelectedItem.ITEM_ID = data[i].ITEM_ID;
                                SelectedItem.ITEM_NAME = data[i].ITEM_NAME;
                                SelectedItem.BARCODE = data[i].BARCODE;
                                App.Current.Properties["Item_Edit"] = SelectedItem.BARCODE;
                                //ACCESSORIES_KEYWORD = data[i].ACCESSORIES_KEYWORD;
                                SelectedItem.ITEM_DESCRIPTION = data[i].ITEM_DESCRIPTION;
                                SelectedItem.ITEM_PRICE = data[i].ITEM_PRICE;
                                SelectedItem.KEYWORD = data[i].KEYWORD;
                                SelectedItem.MRP = data[i].MRP;
                                SelectedItem.PURCHASE_UNIT = data[i].PURCHASE_UNIT;
                                SelectedItem.PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE;
                                SelectedItem.SALES_PRICE = data[i].SALES_PRICE;
                                SelectedItem.SALES_UNIT = data[i].SALES_UNIT;
                                SelectedItem.SEARCH_CODE = data[i].SEARCH_CODE;
                                SelectedItem.TAX_COLLECTED = data[i].TAX_COLLECTED;
                                SelectedItem.TAX_PAID = data[i].TAX_PAID;

                                SelectedItem.TAX_COLLECTED_ID = data[i].TAX_COLLECTED_ID;
                                SelectedItem.TAX_PAID_ID = data[i].TAX_PAID_ID;
                                SelectedItem.UNIT_PURCHAGE_ID = data[i].UNIT_PURCHAGE_ID;
                                SelectedItem.UNIT_SALES_ID = data[i].UNIT_SALES_ID;
                                SelectedItem.GODOWN_ID = data[i].GODOWN_ID;
                                SelectedItem.BUSS_LOC_ID = data[i].BUSS_LOC_ID;


                                SelectedItem.CATEGORY_NAME = data[i].CATEGORY_NAME;
                                SelectedItem.CATAGORY_ID = data[i].CATAGORY_ID;

                                SelectedItem.SelectCollectTax = data[i].SelectCollectTax;
                                SelectedItem.SelectPaidTax = data[i].SelectPaidTax;

                                SelectedItem.DISPLAY_INDEX = data[i].DISPLAY_INDEX;
                                SelectedItem.ITEM_UNIQUE_NAME = data[i].ITEM_UNIQUE_NAME;
                                SelectedItem.ITEM_GROUP_NAME = data[i].ITEM_GROUP_NAME;
                                SelectedItem.REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE;
                                SelectedItem.SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX;

                                SelectedItem.IS_Shortable_Item = data[i].IS_Shortable_Item;
                                SelectedItem.INCLUDE_TAX = data[i].INCLUDE_TAX;
                                SelectedItem.IS_ACTIVE = data[i].IS_ACTIVE;
                                SelectedItem.IS_Not_For_Sell = data[i].IS_Not_For_Sell;
                                SelectedItem.IS_Purchased = data[i].IS_Purchased;
                                SelectedItem.IS_Service_Item = data[i].IS_Service_Item;
                                SelectedItem.IS_Gift_Card = data[i].IS_Gift_Card;
                                SelectedItem.IS_For_Online_Shop = data[i].IS_For_Online_Shop;
                                SelectedItem.IS_Not_for_online_shop = data[i].IS_Not_for_online_shop;
                                SelectedItem.ALLOW_PURCHASE_ON_ESHOP = data[i].ALLOW_PURCHASE_ON_ESHOP;

                                SelectedItem.WEIGHT_OF_CARDBOARD = data[i].WEIGHT_OF_CARDBOARD;
                                SelectedItem.WEIGHT_OF_FOAM = data[i].WEIGHT_OF_FOAM;
                                SelectedItem.WEIGHT_OF_PAPER = data[i].WEIGHT_OF_PAPER;
                                SelectedItem.WEIGHT_OF_PLASTIC = data[i].WEIGHT_OF_PLASTIC;
                                //SelectedItem.BUSINESS_LOC = data[i].BUSINESS_LOC;
                                SelectedItem.GODOWN = data[i].GODOWN;
                                SelectedItem.COMPANY_NAME = data[i].COMPANY_NAME;
                                SelectedItem.DATE = data[i].DATE;
                                SelectedItem.BUSINESS_LOC = data[i].BUSINESS_LOC;
                                SelectedItem.OPENING_STOCK_ID = data[i].OPENING_STOCK_ID;
                                SelectedItem.TAX_COLLECTED_NAME = data[i].TAX_COLLECTED_NAME;
                                SelectedItem.TAX_PAID_NAME = data[i].TAX_PAID_NAME;



                                App.Current.Properties["SelectCat"] = SelectedItem.CATAGORY_ID;
                                App.Current.Properties["SelectCatName"] = SelectedItem.CATEGORY_NAME;

                                App.Current.Properties["OPENING_STOCK_ID"] = SelectedItem.OPENING_STOCK_ID;
                                App.Current.Properties["SalesUnitId"] = SelectedItem.UNIT_SALES_ID;
                                App.Current.Properties["SalesUnitName"] = SelectedItem.SALES_UNIT;

                                App.Current.Properties["PurchaseUnitName"] = SelectedItem.PURCHASE_UNIT;
                                App.Current.Properties["PurchaseUnitId"] = SelectedItem.UNIT_PURCHAGE_ID;


                                App.Current.Properties["TaxPaidName"] = SelectedItem.TAX_PAID_NAME;
                                App.Current.Properties["TaxPaidid"] = SelectedItem.TAX_PAID_ID;


                                App.Current.Properties["GoDownName"] = SelectedItem.GODOWN;
                                App.Current.Properties["GoDownId"] = SelectedItem.GODOWN_ID;
                                App.Current.Properties["BussinessId"] = SelectedItem.BUSS_LOC_ID;
                                App.Current.Properties["BussLocName"] = SelectedItem.BUSINESS_LOC;

                                App.Current.Properties["TaxCollectName"] = SelectedItem.TAX_COLLECTED_NAME;
                                App.Current.Properties["TaxCollectid"] = SelectedItem.TAX_COLLECTED_ID;
                                var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                                // get your 'Uploaded' folder
                                var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));
                                if (data[i].IMAGE_PATH != null)
                                {
                                    var fr = filename + data[i].IMAGE_PATH;

                                    var imageFile = new System.IO.FileInfo(data[i].IMAGE_PATH);
                                    string file = imageFile.Name;

                                    if (!dir.Exists)
                                        dir.Create();
                                    // Copy file to your folder
                                    //imageFile.CopyTo(System.IO.Path.Combine(dir.FullName, file), true);
                                    string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);

                                    FtpDown(path1, file);

                                    IMAGE_PATH1 = new BitmapImage(new Uri(path1));
                                    SelectedItem.IMAGE_PATH = file;

                                }
                                else
                                {
                                    var imageFile = new System.IO.FileInfo("NoImage.jpg");
                                    string file = imageFile.Name;
                                    string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);
                                    IMAGE_PATH1 = new BitmapImage(new Uri(path1));
                                }
                                App.Current.Properties["ItemViewImg"] = IMAGE_PATH1;
                                App.Current.Properties["TaxListsession"] = "";
                            }

                        }
                    }


                    App.Current.Properties["ItemEdit"] = SelectedItem;
                    //Close();
                    ItemAdd IA = new ItemAdd();

                    ItemAdd.CatRef.Text = null;
                    ItemAdd.CatRef.Text = SelectedItem.CATEGORY_NAME;
                    ItemAdd.TaxRef2.Text = null;
                    ItemAdd.TaxRef2.Text = SelectedItem.TAX_COLLECTED_NAME;
                    ItemAdd.TaxRef1.Text = null;
                    ItemAdd.TaxRef1.Text = SelectedItem.TAX_PAID_NAME;
                    ItemAdd.UnitRef.Text = null;

                    ItemAdd.UnitRef.Text = SelectedItem.PURCHASE_UNIT;
                    ItemAdd.SaleUnitRef.Text = null;

                    ItemAdd.SaleUnitRef.Text = SelectedItem.SALES_UNIT;

                    IA.ShowDialog();
                    // ModalService.NavigateTo(new ItemAdd(), delegate(bool returnValue) { });

                }
                else
                {
                    MessageBox.Show("Select Item first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {

                throw;
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
        public async void ChangePrice_Item()
        {
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
                // ItemAdd nh = new ItemAdd();

                if (App.Current.Properties["ItemName"] != null)
                {
                    SelectedItem.ITEM_NAME = App.Current.Properties["ItemName"] as string;
                }
               
                if (App.Current.Properties["OldBarcode"] != null && App.Current.Properties["OldBarcode"] != "")
                {
                    GetSelectionAutoComplete();
                    BarcodeUnique();
                    SearchCodeUnique();
                    if (data.Count() > 1)
                    {
                        MessageBox.Show("" + BARCODE + " Barcode is already present");

                    }
                    else if (data1.Count() > 1)
                    {
                        MessageBox.Show("" + SEARCH_CODE + " Search Code is already present");
                    }
                    else
                    {

                        if (App.Current.Properties["ItemName"] != null)
                        {
                            SelectedItem.ITEM_NAME = App.Current.Properties["ItemName"].ToString();
                        }
                        if (App.Current.Properties["OldBusinessName"] != null)
                        {
                            SelectedItem.BUSINESS_LOC = App.Current.Properties["OldBusinessName"].ToString();
                        }
                        //if (App.Current.Properties["Qunt"] != null)
                        //{
                        //    SelectedItem.OPN_QNT = Convert.ToInt32(App.Current.Properties["Qunt"].ToString());
                        //}
                        //if (App.Current.Properties["Godown"] != null)
                        //{
                        //    SelectedItem.GODOWN = App.Current.Properties["Godown"].ToString();
                        //}
                        //if (App.Current.Properties["Date"] != null)
                        //{
                        //    SelectedItem.DATE = Convert.ToDateTime(App.Current.Properties["Date"]);
                        //}
                        //if (SelectedItem.ITEM_NAME == "" || SelectedItem.ITEM_NAME == null)
                        //{
                        //    MessageBox.Show("Item Name is missing");
                        //}
                        if (App.Current.Properties["NewSalesPrice"] != null)
                        {
                            SelectedItem.SALES_PRICE = Convert.ToDecimal(App.Current.Properties["NewSalesPrice"]);
                        }
                        if (App.Current.Properties["NewMRP"] != null)
                        {
                            SelectedItem.MRP = Convert.ToDecimal(App.Current.Properties["NewMRP"]);
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

                        else
                        {
                            HttpClient client = new HttpClient();
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
                            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                            var response = await client.PostAsJsonAsync("api/ItemAPI/CreateItem/", SelectedItem);
                            if (response.StatusCode.ToString() == "OK")
                            {
                                MessageBox.Show("Item Updated Successfully");
                                Cancel_Item();
                                ModalService.NavigateTo(new Items(), delegate (bool returnValue) { });
                            }
                            else
                            {
                                MessageBox.Show("Internal Problem");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Barcode is missing");
                }
            }

            catch (Exception ex)
            {

                throw;
            }

        }
        public ICommand _ViewItem { get; set; }
        public ICommand ViewItem
        {
            get
            {
                if (_ViewItem == null)
                {
                    _ViewItem = new DelegateCommand(View_Item);
                }
                return _ViewItem;
            }
        }
        public async void View_Item()
        {
            if (SelectedItem.ITEM_NAME != null && SelectedItem.ITEM_NAME != "")
            {

                //string ftpServerIP = "115.115.196.30";
                //string ftpUserID = "suvendu";
                //string ftpPassword = "Passw0rd";


                string ftpServerIP = "54.70.197.231";
                string ftpUserID = "suvendu";
                string ftpPassword = "vpY9dNp3W9AqhjGy";

                string filename = "ftp://" + ftpServerIP + "//Upload//";



                App.Current.Properties["Action"] = "View";
                ItemData = new List<ItemModel>();
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
                            SelectedItem.IMAGE_PATH = data[i].IMAGE_PATH;
                            SelectedItem.ITEM_NAME = data[i].ITEM_NAME;
                            SelectedItem.BARCODE = data[i].BARCODE;
                            //ACCESSORIES_KEYWORD = data[i].ACCESSORIES_KEYWORD;
                            SelectedItem.ITEM_DESCRIPTION = data[i].ITEM_DESCRIPTION;
                            SelectedItem.ITEM_PRICE = data[i].ITEM_PRICE;
                            SelectedItem.KEYWORD = data[i].KEYWORD;
                            SelectedItem.MRP = data[i].MRP;
                            SelectedItem.PURCHASE_UNIT = data[i].PURCHASE_UNIT;
                            SelectedItem.PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE;
                            SelectedItem.SALES_PRICE = data[i].SALES_PRICE;
                            SelectedItem.SALES_UNIT = data[i].SALES_UNIT;
                            SelectedItem.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedItem.TAX_COLLECTED = data[i].TAX_COLLECTED;
                            SelectedItem.TAX_PAID = data[i].TAX_PAID;

                            SelectedItem.SelectCollectTax = data[i].SelectCollectTax;
                            SelectedItem.SelectPaidTax = data[i].SelectPaidTax;

                            SelectedItem.DISPLAY_INDEX = data[i].DISPLAY_INDEX;
                            SelectedItem.ITEM_UNIQUE_NAME = data[i].ITEM_UNIQUE_NAME;
                            SelectedItem.ITEM_GROUP_NAME = data[i].ITEM_GROUP_NAME;
                            SelectedItem.REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE;
                            SelectedItem.SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX;

                            SelectedItem.IS_Shortable_Item = data[i].IS_Shortable_Item;
                            SelectedItem.INCLUDE_TAX = data[i].INCLUDE_TAX;
                            SelectedItem.IS_ACTIVE = data[i].IS_ACTIVE;
                            SelectedItem.IS_Not_For_Sell = data[i].IS_Not_For_Sell;
                            SelectedItem.IS_Purchased = data[i].IS_Purchased;
                            SelectedItem.IS_Service_Item = data[i].IS_Service_Item;
                            SelectedItem.IS_Gift_Card = data[i].IS_Gift_Card;
                            SelectedItem.IS_For_Online_Shop = data[i].IS_For_Online_Shop;
                            SelectedItem.IS_Not_for_online_shop = data[i].IS_Not_for_online_shop;
                            SelectedItem.ALLOW_PURCHASE_ON_ESHOP = data[i].ALLOW_PURCHASE_ON_ESHOP;


                            SelectedItem.WEIGHT_OF_CARDBOARD = data[i].WEIGHT_OF_CARDBOARD;
                            SelectedItem.WEIGHT_OF_FOAM = data[i].WEIGHT_OF_FOAM;
                            SelectedItem.WEIGHT_OF_PAPER = data[i].WEIGHT_OF_PAPER;
                            SelectedItem.WEIGHT_OF_PLASTIC = data[i].WEIGHT_OF_PLASTIC;



                            SelectedItem.TAX_COLLECTED_NAME = data[i].TAX_COLLECTED_NAME;
                            SelectedItem.TAX_PAID_NAME = data[i].TAX_PAID_NAME;




                            App.Current.Properties["SelectCat"] = SelectedItem.CATAGORY_ID;
                            App.Current.Properties["SelectCatName"] = SelectedItem.CATEGORY_NAME;


                            App.Current.Properties["SalesUnitId"] = SelectedItem.UNIT_SALES_ID;
                            App.Current.Properties["SalesUnitName"] = SelectedItem.SALES_UNIT;

                            App.Current.Properties["PurchaseUnitName"] = SelectedItem.PURCHASE_UNIT;
                            App.Current.Properties["PurchaseUnitId"] = SelectedItem.UNIT_PURCHAGE_ID;


                            App.Current.Properties["TaxPaidName"] = SelectedItem.TAX_PAID_NAME;
                            App.Current.Properties["TaxPaidid"] = SelectedItem.TAX_PAID_ID;


                            App.Current.Properties["GoDownName"] = SelectedItem.GODOWN;
                            App.Current.Properties["GoDownId"] = SelectedItem.GODOWN_ID;
                            App.Current.Properties["BussinessId"] = SelectedItem.BUSS_LOC_ID;
                            App.Current.Properties["BussLocName"] = SelectedItem.BUSINESS_LOC;

                            App.Current.Properties["TaxCollectName"] = SelectedItem.TAX_COLLECTED_NAME;
                            App.Current.Properties["TaxCollectid"] = SelectedItem.TAX_COLLECTED_ID;
                            var fr = filename + data[i].IMAGE_PATH;
                            var Image = data[i].IMAGE_PATH;
                            if (Image != null)
                            {
                                var imageFile = new System.IO.FileInfo(data[i].IMAGE_PATH);
                                string file = imageFile.Name;

                                var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                                // get your 'Uploaded' folder
                                var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));
                                if (!dir.Exists)
                                    dir.Create();
                                // Copy file to your folder
                                // imageFile.CopyTo(System.IO.Path.Combine(dir.FullName, file), true);
                                string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);

                                FtpDown(path1, file);


                                IMAGE_PATH1 = new BitmapImage(new Uri(path1));
                                App.Current.Properties["ItemViewImg"] = IMAGE_PATH1;
                            }
                        }
                        App.Current.Properties["ItemView"] = SelectedItem;
                        ViewItem IA = new ViewItem();
                        IA.ShowDialog();

                    }
                }

            }
            else
            {
                MessageBox.Show("Select Item first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
        #endregion


        #region Update Item
        public ICommand _UpdateItem;
        public ICommand UpdateItem
        {
            get
            {
                if (_UpdateItem == null)
                {
                    _UpdateItem = new DelegateCommand(Update_Item);
                }
                return _UpdateItem;
            }
        }
        public async void Update_Item()
        {
            if (App.Current.Properties["SelectCat"] != null)
            {
                SelectedItem.CATAGORY_ID = Convert.ToInt64(App.Current.Properties["SelectCat"] as string);
            }
            else
            {


            }
            if (BARCODE != null && BARCODE != "")
            {
                GetSelectionAutoComplete();
                BarcodeUnique();
                SearchCodeUnique();
                if (data.Count() > 1)
                {
                    MessageBox.Show("" + BARCODE + " Barcode is already present");

                }
                else if (data1.Count() > 1)
                {
                    MessageBox.Show("" + SEARCH_CODE + " Search Code is already present");
                }
                else
                {

                    if (App.Current.Properties["itemName"] != null)
                    {
                        SelectedItem.ITEM_NAME = App.Current.Properties["itemName"].ToString();
                    }
                    if (App.Current.Properties["BussLocation"] != null)
                    {
                        SelectedItem.BUSINESS_LOC = App.Current.Properties["BussLocation"].ToString();
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

                    else
                    {
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                        var response = await client.PostAsJsonAsync("api/ItemAPI/CreateItem/", SelectedItem);
                        if (response.StatusCode.ToString() == "OK")
                        {
                            MessageBox.Show("Item Updated Successfully");
                            Cancel_Item();
                            ModalService.NavigateTo(new Items(), delegate (bool returnValue) { });
                        }
                        else
                        {
                            MessageBox.Show("Internal Problem");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Barcode is missing");
            }

        }

        #endregion

        public ICommand _ShowCustomerList;
        public ICommand ShowCustomerList
        {
            get 
            {
                if (_ShowCustomerList == null)
                {
                    _ShowCustomerList = new DelegateCommand(ShowCustomerList_Click);
                }
                return _ShowCustomerList;
            }
        }

        public async void ShowCustomerList_Click()
        {
            ChangeInvoiceCustomer sh = new UserControll.Item.ChangeInvoiceCustomer();
            sh.Show();
        }
        #region cancel Item
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

        #endregion

      

        public ICommand _ImportItems { get; set; }
        public ICommand ImportItems
        {
            get
            {
                if (_ImportItems == null)
                {
                    _ImportItems = new DelegateCommand(ImportItems_Click);
                }
                return _ImportItems;
            }
        }

        public async void ImportItems_Click()
        {
            ImportDataItem sh = new UserControll.Item.ImportDataItem();
            sh.Show();
        }


        public ICommand _UploadItems { get; set; }
        public ICommand UploadItems
        {
            get
            {
                if (_UploadItems == null)
                {
                    _UploadItems = new DelegateCommand(UploadItems_Click);
                }
                return _UploadItems;
            }
        }

        public async void UploadItems_Click()
    {
        //OpenFileDialog openfile = new OpenFileDialog();
        //openfile.DefaultExt = ".xls";
        //openfile.Filter = "(.xls)|*.xls";
        ////openfile.ShowDialog();

        //var browsefile = openfile.ShowDialog();

        //GetFile();

        //OpenFileDialog openfile = new OpenFileDialog();
        //openfile.DefaultExt = ".xlsx";
        //openfile.Filter = "(.xlsx)|*.xlsx";
        ////openfile.ShowDialog();

        //var browsefile = openfile.ShowDialog();
        //ExcelPath = openfile.FileName;
        //GetFile();

        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Excel file(*.xlsm;*.xlsx;*.xlsx;*.xlt; *.xlk;*.xls;)|*.xlsm;*.xlsx;*.xlsx;*.xlt;*.xls; *xlk" ;
        openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (openFileDialog.ShowDialog() == true)
        {
        }
        if (File.Exists(openFileDialog.FileName))
        {
            string xx = openFileDialog.FileName;
            ExcelPath = openFileDialog.FileName;
            var excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xx + ";Extended Properties=Excel 12.0;");
            OleDbConnection objOlecon = new OleDbConnection();
            objOlecon.ConnectionString = excelConnectionString;
            objOlecon.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = objOlecon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string sheet_Name = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            string query = String.Format("select * from [{0}$]", sheet_Name);
            OleDbDataAdapter objOleDa = new OleDbDataAdapter("Select * from [" + sheet_Name + "]", objOlecon);
            List<ItemModel> _ListGrid_Temp = new List<ItemModel>();
            DataTable objdt = new DataTable();
            objOleDa.Fill(objdt);
            if (objdt.Rows.Count > 0)
            {
                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    var df = objdt.Rows[i];
                    SelectedItem.CATEGORY_NAME = Convert.ToString(objdt.Rows[i].ItemArray[0]);
                    SelectedItem.BARCODE = Convert.ToString(objdt.Rows[i].ItemArray[1]);
                    SelectedItem.SEARCH_CODE = Convert.ToString(objdt.Rows[i].ItemArray[0]);
                    SelectedItem.ITEM_NAME = Convert.ToString(objdt.Rows[i].ItemArray[0]);
                    if (objdt.Rows[i].ItemArray[i] == DBNull.Value)
                    {

                    }
                    /*_ListGrid_Temp.Add(new ItemModel
                  {
                      SLNO = (objdt.Rows[i].ItemArray[0]==null)?0:Convert.ToInt32(objdt.Rows[i].ItemArray[0]),
                      BARCODE = (objdt.Rows[i].ItemArray[2] == null)?"":Convert.ToString(objdt.Rows[i].ItemArray[2]),
                      IMAGE_PATH = (objdt.Rows[i].ItemArray[1] == null)?"":Convert.ToString(objdt.Rows[i].ItemArray[1]),
                      ITEM_NAME = (objdt.Rows[i].ItemArray[3] == null)?"":Convert.ToString(objdt.Rows[i].ItemArray[3]),
                      SEARCH_CODE = (objdt.Rows[i].ItemArray[4] == null)?"":Convert.ToString(objdt.Rows[i].ItemArray[4]),
                      MRP = (objdt.Rows[i].ItemArray[5] == null)?0:Convert.ToDecimal(objdt.Rows[i].ItemArray[5]),
                      SALES_PRICE = (objdt.Rows[i].ItemArray[6] == null)?0:Convert.ToDecimal(objdt.Rows[i].ItemArray[6]),
                      SALES_UNIT = (objdt.Rows[i].ItemArray[7] == null)?"":Convert.ToString(objdt.Rows[i].ItemArray[7]),
                      PURCHASE_UNIT = (objdt.Rows[i].ItemArray[8] == null)?"":Convert.ToString(objdt.Rows[i].ItemArray[8]),
                      //MODIFICATION_DATE = Convert.ToDateTime(Convert.ToString(objdt.Rows[i].ItemArray[9])),
                      CATEGORY_NAME = (objdt.Rows[i].ItemArray[10] == null)?"":Convert.ToString(objdt.Rows[i].ItemArray[10]),
                      DISPLAY_INDEX = (objdt.Rows[i].ItemArray[11] == null)?"":Convert.ToString(objdt.Rows[i].ItemArray[11]),
                      OPN_QNT = (objdt.Rows[i].ItemArray[12] == null)?0:Convert.ToInt32(objdt.Rows[i].ItemArray[12]),
                      ITEM_GROUP_NAME = (objdt.Rows[i].ItemArray[13] == null)?"":Convert.ToString(objdt.Rows[i].ItemArray[13]),
                      REGIONAL_LANGUAGE = (objdt.Rows[i].ItemArray[14] == null)?"":Convert.ToString(objdt.Rows[i].ItemArray[14]),

                  });*/

                    ItemModel im = new ItemModel();

                    im.SLNO = (objdt.Rows[i].ItemArray[0]!=null)?Convert.ToInt32(objdt.Rows[i].ItemArray[0]):0;
                    im.BARCODE = (objdt.Rows[i].ItemArray[2] != null)?Convert.ToString(objdt.Rows[i].ItemArray[2]):"";
                    im.IMAGE_PATH = (objdt.Rows[i].ItemArray[1] != null)?Convert.ToString(objdt.Rows[i].ItemArray[1]):"";
                    im.ITEM_NAME = (objdt.Rows[i].ItemArray[3] != null)?Convert.ToString(objdt.Rows[i].ItemArray[3]):"";
                    im.SEARCH_CODE = (objdt.Rows[i].ItemArray[4] != null)?Convert.ToString(objdt.Rows[i].ItemArray[4]):"";
                    im.MRP = (objdt.Rows[i].ItemArray[5] != null)?Convert.ToDecimal(objdt.Rows[i].ItemArray[5]):0;
                    im.SALES_PRICE = (objdt.Rows[i].ItemArray[6] != null)?Convert.ToDecimal(objdt.Rows[i].ItemArray[6]):0;
                    im.SALES_UNIT = (objdt.Rows[i].ItemArray[7] != null)?Convert.ToString(objdt.Rows[i].ItemArray[7]):"";
                    im.PURCHASE_UNIT = (objdt.Rows[i].ItemArray[8] != null)?Convert.ToString(objdt.Rows[i].ItemArray[8]):"";
                    //im.MODIFICATION_DATE = (objdt.Rows[i].ItemArray[10] != null)?Convert.ToDateTime(objdt.Rows[i].ItemArray[10]);
                    im.CATEGORY_NAME = (objdt.Rows[i].ItemArray[10] != null)?Convert.ToString(objdt.Rows[i].ItemArray[11]):"";
                    im.DISPLAY_INDEX = (objdt.Rows[i].ItemArray[11] != null)?Convert.ToString(objdt.Rows[i].ItemArray[12]):"";
                    im.OPN_QNT = (objdt.Rows[i].ItemArray[11] != null) ? Convert.ToInt32(objdt.Rows[i].ItemArray[13]) : 0;
                    //if (objdt.Rows[i].ItemArray[12] != null)
                    //{
                    //    string ex = objdt.Rows[i].ItemArray[12].ToString();
                    //    if (!String.IsNullOrEmpty(ex))
                    //    {
                    //        im.OPN_QNT = Convert.ToInt32(objdt.Rows[i].ItemArray[12]);
                    //    }
                    //    else
                    //    {
                    //        im.OPN_QNT = 0;
                    //    }
                    //}
                    //else
                    //{
                    //    im.OPN_QNT = 0;
                    //}
                    im.ITEM_GROUP_NAME = (objdt.Rows[i].ItemArray[13] != null)?Convert.ToString(objdt.Rows[i].ItemArray[14]):"";
                    im.REGIONAL_LANGUAGE = (objdt.Rows[i].ItemArray[14] != null) ? Convert.ToString(objdt.Rows[i].ItemArray[15]):"";

                      _ListGrid_Temp.Add(im);

                }
               
              
            }
            ListGrid = _ListGrid_Temp;
            
            App.Current.Properties["ExcelData"] = ListGrid;

        }

    }

        public async void GetFile() 
        {
           
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            //Static File From Base Path...........
            //Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "TestExcel.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //Dynamic File Using Uploader...........
            Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(ExcelPath.ToString(), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
            Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

            string strCellData = "";
            double douCellData;
            int rowCnt = 0;
            int colCnt = 0;

            DataTable dt = new DataTable();
            for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
            {
                string strColumn = "";
                strColumn = (string)(excelRange.Cells[1, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                //dt.Columns.Add(strColumn, typeof(string));
            }

            for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
            {
                string strData = "";
                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    try
                    {
                        strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                        if (strCellData == null) 
                        { 
                            strCellData = " ";
                        }
                        strData += strCellData + "|";
                    }
                    catch (Exception ex)
                    {
                        douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                        strData += douCellData.ToString() + "|";
                    }
                }
                strData = strData.Remove(strData.Length - 1, 1);
                dt.Rows.Add(strData.Split('|'));
                ListGrid.Add(new ItemModel
                {
                    INVOICE_ID = Convert.ToInt32(strData[0]),
                    ITEM_NAME = Convert.ToString(strData[1])
                });
            }

            //dataGridView1.ItemsSource = dt.DefaultView;

            excelBook.Close(true, null, null);
            excelApp.Quit();
        }



        public ICommand _PurchaseSlaesHistory { get; set; }
        public ICommand PurchaseSlaesHistory
        {
            get
            {
                if (_PurchaseSlaesHistory == null)
                {
                    _PurchaseSlaesHistory = new DelegateCommand(PurchaseSalesItem);
                }
                return _PurchaseSlaesHistory;
            }
        }
        public async void PurchaseSalesItem()
        {
            if (App.Current.Properties["GetItemDetails"] != null)
            {
                App.Current.Properties["Action"] = "PurchaseHistory";
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                var ItemName = SelectedItem.ITEM_NAME;
                //ListGridSales = new List<ItemModel>();
                ItemModel get = App.Current.Properties["GetItemDetails"] as ItemModel;
                int itemid = get.ITEM_ID;
                int companyid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllPurchaseDetails?id=" + companyid + "&itemid=" + itemid + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    //for (int i = 0; i < data.Length; i++)
                    //{
                    foreach (var item in data)
                    {
                        _ListGrid_Temp.Add(new ItemModel
                        {
                            SUPPLIER_NAME = item.SUPPLIER_NAME,
                            INVOICE_DATE = item.DATE,
                            PO_NUMBER = item.PO_NUMBER,
                            ITEM_NAME = item.ITEM_NAME,
                            PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                            SALES_UNIT = item.SALES_UNIT,
                            SALES_PRICE = item.SALES_PRICE,
                            RECEIVE_ITEM_ENTRY_NO = item.PO_NUMBER,
                            RECEIVE_ITEM_ENTRY_DATE = item.RECEIVE_ITEM_ENTRY_DATE,
                            EFFECTIVE_RATE_PER_UNIT = item.PURCHASE_UNIT_PRICE + item.TAX_COLLECTED
                        });

                    }
                    //ListGridSales.Clear();

                    ListGridPurchase = _ListGrid_Temp;
                    App.Current.Properties["GetItemsellsDetails"] = _getItemList;
                    App.Current.Properties["ListGridSales"] = _ListGrid_Temp;
                    //App.Current.Properties["ItemNameSales"] = SelectedItem.ITEM_NAME;
                    //App.Current.Properties["ItemCodeSales"] = SelectedItem.ITEM_ID;
                    //ITEM_NAME = App.Current.Properties["ItemNameSales"].ToString();
                    //ITEM_ID = Convert.ToInt32(App.Current.Properties["ItemCodeSales"].ToString());
                    //ListGridSales = App.Current.Properties["ListGridSales"] as List<ItemModel>;
                    ViewPurchaseHistory sh = new ViewPurchaseHistory();
                    sh.DataContext = this;
                    sh.Show();
                }

            }
            else
            {
                MessageBox.Show("Please select item");
                return;
            }
        }
        public ICommand _ItemSalesHistory { get; set; }
        public ICommand ItemSalesHistory
        {
            get
            {
                if (_ItemSalesHistory == null)
                {
                    _ItemSalesHistory = new DelegateCommand(ItemSalesHistory_Click);
                }
                return _ItemSalesHistory;
            }
        }
        public async void ItemSalesHistory_Click()
        {
            if (App.Current.Properties["GetItemDetails"] != null)
            {
                
                App.Current.Properties["Action"] = "SalesHistory";
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                var ItemName = SelectedItem.ITEM_NAME;
                //ListGridSales = new List<ItemModel>();
                ItemModel get = App.Current.Properties["GetItemDetails"] as ItemModel;
                int itemid = get.ITEM_ID;
                int companyid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetItemSalesList?id=" + companyid + "&itemid=" + itemid + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    //for (int i = 0; i < data.Length; i++)
                    //{
                    foreach (var item in data)
                    {
                        int total = 0;
                        _ListGrid_Temp.Add(new ItemModel
                        {
                            
                            SUPPLIER_NAME = item.SUPPLIER_NAME,
                            INVOICE_DATE = item.INVOICE_DATE,
                            PO_NUMBER = item.PO_NUMBER,
                            ITEM_NAME = item.ITEM_NAME,
                            PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                            SALES_UNIT = item.SALES_UNIT,
                            SALES_PRICE = item.SALES_PRICE,
                            BUSINESS_LOC = item.BUSINESS_LOC,
                            Total = item.Total,
                            QUNT_TOTAL = item.QUNT_TOTAL,
                            CUSTOMER_EMAIL = item.CUSTOMER_EMAIL,
                            CUSTOMER_MOBILE = item.CUSTOMER_MOBILE,
                            CUSTOMER_NAME = item.CUSTOMER_NAME,
                            INVOICE_NO = item.INVOICE_NO,
                            TAX_COLLECTED = item.TAX_COLLECTED,
                            INVOICE_ID = item.INVOICE_ID,
                            ESTIMATE_DATE = item.ESTIMATE_DATE,
                            ITEM_INVOICE_ID = item.ITEM_INVOICE_ID
                        });
                        
                    }
                    //ListGridSales.Clear();

                    ListGridItemSales = _ListGrid_Temp;
                    App.Current.Properties["GetItemsellsDetails"] = _getItemList;
                    App.Current.Properties["ListGridSales"] = _ListGrid_Temp;
                    //App.Current.Properties["ItemNameSales"] = SelectedItem.ITEM_NAME;
                    //App.Current.Properties["ItemCodeSales"] = SelectedItem.ITEM_ID;
                    //ITEM_NAME = App.Current.Properties["ItemNameSales"].ToString();
                    //ITEM_ID = Convert.ToInt32(App.Current.Properties["ItemCodeSales"].ToString());
                    //ListGridSales = App.Current.Properties["ListGridSales"] as List<ItemModel>;
                    ViewItemSalesHistory sh = new ViewItemSalesHistory();
                    SelectedItem.TO_DATE = DateTime.Now.Date;
                    SelectedItem.FORM_DATE = DateTime.Now.Date;
                    sh.DataContext = this;
                    sh.Show();
                }

            }
            else
            {
                MessageBox.Show("Please select item");
                return;
            }
        }
        public ICommand _ShowPurchaseHistory { get; set; }
        public ICommand ShowPurchaseHistory
        {
            get
            {
                if (_ShowPurchaseHistory == null)
                {
                    _ShowPurchaseHistory = new DelegateCommand(ShowPurchaseItem);
                }
                return _ShowPurchaseHistory;
            }
        }
        public async void ShowPurchaseItem()
        {
            if (App.Current.Properties["GetItemDetails"] != null)
            {
                App.Current.Properties["Action"] = "SalesHistory";
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                var ItemName = SelectedItem.ITEM_NAME;
                //ListGridSales = new List<ItemModel>();
                ItemModel get = App.Current.Properties["GetItemDetails"] as ItemModel;
                int itemid = get.ITEM_ID;
                int companyid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetItemPurchaseList?id=" + companyid + "&itemid=" + itemid + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    //data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    //for (int i = 0; i < data.Length; i++)
                    //{
                    foreach (var item in data)
                    {
                        _ListGrid_Temp.Add(new ItemModel
                        {
                            //SUPPLIER_NAME = item.SUPPLIER_NAME,
                            //INVOICE_DATE = item.DATE,
                            //PO_NUMBER = item.PO_NUMBER,
                            //ITEM_NAME = item.ITEM_NAME,
                            //PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                            //SALES_UNIT = item.SALES_UNIT,
                            //SALES_PRICE = item.SALES_PRICE,
                            BUSINESS_LOC = item.BUSINESS_LOC,
                            RECEIVE_ITEM_ENTRY_DATE = item.RECEIVE_ITEM_ENTRY_DATE,
                            RECEIVE_ITEM_ENTRY_NO = item.RECEIVE_ITEM_ENTRY_NO,
                            PAYMENT_STATUS = item.PAYMENT_STATUS,
                            GODOWN = item.GODOWN,
                            SUPPLIER_NAME = item.SUPPLIER_NAME,
                            Total = item.Total,
                            TotalTax = item.TotalTax,
                            DATE = item.DATE,
                            SUPPLIER_INVOICE_NO = item.SUPPLIER_INVOICE_NO,
                            ITEM_RECEIVE_DATE = item.ITEM_RECEIVE_DATE,
                            PAYMENT_BY_CASH = item.PAYMENT_BY_CASH
                            
                        });

                    }
                    //ListGridSales.Clear();

                    ListGridItemPurchase = _ListGrid_Temp;
                    App.Current.Properties["GetItemsellsDetails"] = _getItemList;
                    App.Current.Properties["ListGridSales"] = _ListGrid_Temp;
                    //App.Current.Properties["ItemNameSales"] = SelectedItem.ITEM_NAME;
                    //App.Current.Properties["ItemCodeSales"] = SelectedItem.ITEM_ID;
                    //ITEM_NAME = App.Current.Properties["ItemNameSales"].ToString();
                    //ITEM_ID = Convert.ToInt32(App.Current.Properties["ItemCodeSales"].ToString());
                    //ListGridSales = App.Current.Properties["ListGridSales"] as List<ItemModel>;
                    ViewItemPurchaseHistory sh = new ViewItemPurchaseHistory();
                    SelectedItem.FORM_DATE = System.DateTime.Now.Date;
                    SelectedItem.TO_DATE = System.DateTime.Now.Date;
                    sh.DataContext = this;
                    sh.Show();
                }

            }
            else
            {
                MessageBox.Show("Please select item");
                return;
            }
        }

        public ICommand _ItemPurchase { get; set; }
        public ICommand ItemPurchase
        {
            get
            {
                if (_ItemPurchase == null)
                {
                    _ItemPurchase = new DelegateCommand(ItemPurchaseClick);
                }
                return _ItemPurchase;
            }
        }
        public async void ItemPurchaseClick()
        {
            if (App.Current.Properties["GetItemDetails"] != null)
            {
                App.Current.Properties["Action"] = "SalesHistory";
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                var ItemName = SelectedItem.ITEM_NAME;
                //ListGridSales = new List<ItemModel>();
                ItemModel get = App.Current.Properties["GetItemDetails"] as ItemModel;
                int itemid = get.ITEM_ID;
                int companyid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                int invoiceid = SelectedItem.INVOICE_ID.Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetItemPurchaseList?id=" + invoiceid + "&itemid=" + itemid + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    //for (int i = 0; i < data.Length; i++)
                    //{
                    int total = 0;
                    int totalItem = 0;
                    int totalQty = 0;
                    foreach (var item in data)
                    {
                        total += Convert.ToInt32(item.Total);
                        totalItem += data.Count();
                        totalQty += item.Current_Qty.Value;
                        _ListGrid_Temp.Add(new ItemModel
                        {
                            PURCHASE_UNIT = item.PURCHASE_UNIT_PRICE.ToString(),
                            TaxName = item.TaxName,
                            TaxValue = item.TaxValue,
                            INVOICE_NO = item.INVOICE_NO,
                            PAYMENT_STATUS = item.PAYMENT_STATUS,
                            SUPPLIER_NAME = item.SUPPLIER_NAME,
                            INVOICE_DATE = item.DATE,
                            PO_NUMBER = item.PO_NUMBER,
                            ITEM_NAME = item.ITEM_NAME,
                            PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                            SALES_UNIT = item.SALES_UNIT,
                            SALES_PRICE = item.SALES_PRICE,
                            SLNO = item.SLNO,
                            BARCODE = item.BARCODE,
                            BUSINESS_LOC = item.BUSINESS_LOC,
                            CUSTOMER_NAME = item.CUSTOMER_NAME,
                            CUSTOMER_MOBILE = item.CUSTOMER_MOBILE,
                            CUSTOMER_EMAIL = item.CUSTOMER_EMAIL,
                            Current_Qty = item.Current_Qty,
                            Total = item.Total,
                            TOTAL_ITEM = item.TOTAL_ITEM,
                            TotalTax = item.TotalTax,
                            Discount = item.Discount,
                            QUNT_TOTAL = item.QUNT_TOTAL
                        });

                    }
                    //ListGridSales.Clear();
                    TOTAL_SUM = total;
                    TOTAL_ITEM = totalItem;
                    TOTAL_QTY = totalQty;
                    ListGridItemPurchase = _ListGrid_Temp;
                    App.Current.Properties["GetItemsellsDetails"] = _getItemList;
                    App.Current.Properties["ListGridSales"] = _ListGrid_Temp;
                    //App.Current.Properties["ItemNameSales"] = SelectedItem.ITEM_NAME;
                    //App.Current.Properties["ItemCodeSales"] = SelectedItem.ITEM_ID;
                    //ITEM_NAME = App.Current.Properties["ItemNameSales"].ToString();
                    //ITEM_ID = Convert.ToInt32(App.Current.Properties["ItemCodeSales"].ToString());
                    //ListGridSales = App.Current.Properties["ListGridSales"] as List<ItemModel>;
                    ViewItemPurchaseHistory sh = new ViewItemPurchaseHistory();
                    sh.DataContext = this;
                    sh.Show();
                }

            }
            else
            {
                MessageBox.Show("Please select item");
                return;
            }
        }
       
        public ICommand _InvoiceDeliveryDetails { get; set; }
        public ICommand InvoiceDeliveryDetails
        {
            get
            {
                if (_InvoiceDeliveryDetails == null)
                {
                    _InvoiceDeliveryDetails = new DelegateCommand(InvoiceDeliveryDetails_Click);
                }
                return _InvoiceDeliveryDetails;
            }
        }
        public async void InvoiceDeliveryDetails_Click()
        {
            if (App.Current.Properties["GetItemDetails"] != null)
            {
                App.Current.Properties["Action"] = "SalesHistory";
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                var ItemName = SelectedItem.ITEM_NAME;
                //ListGridSales = new List<ItemModel>();
                ItemModel get = App.Current.Properties["GetItemDetails"] as ItemModel;
                int itemid = get.ITEM_ID;
                int companyid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                int invoiceid = SelectedItem.INVOICE_ID.Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetItemDetails?id=" + invoiceid + "&itemid=" + itemid + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    //for (int i = 0; i < data.Length; i++)
                    //{
                    int total = 0;
                    int totalItem = 0;
                    int totalQty = 0;
                    foreach (var item in data)
                    {
                        total += Convert.ToInt32(item.Total);
                        totalItem += data.Count();
                        totalQty += item.Current_Qty.Value;
                        _ListGrid_Temp.Add(new ItemModel
                        {

                        });

                    }

                    ListGridItemDetails = _ListGrid_Temp;
                    App.Current.Properties["GetItemsellsDetails"] = _getItemList;
                    App.Current.Properties["ListGridSales"] = _ListGrid_Temp;
                    InvoiceDeliveryDetails sh = new InvoiceDeliveryDetails();
                    sh.Show();
                }
                
            }
            else
            {
                MessageBox.Show("Please select item");
                return;
            }
        }

        public ICommand _ItemView { get; set; }
        public ICommand ItemView
        {
            get
            {
                if (_ItemView == null)
                {
                    _ItemView = new DelegateCommand(ItemViewClick);
                }
                return _ItemView;
            }
        }
        public async void ItemViewClick()
        {
            if (App.Current.Properties["GetItemDetails"] != null)
            {
                App.Current.Properties["Action"] = "SalesHistory";
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                var ItemName = SelectedItem.ITEM_NAME;
                //ListGridSales = new List<ItemModel>();
                ItemModel get = App.Current.Properties["GetItemDetails"] as ItemModel;
                int itemid = get.ITEM_ID;
                int companyid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                int invoiceid = SelectedItem.INVOICE_ID.Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetItemDetails?id=" + invoiceid + "&itemid=" + itemid + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    //for (int i = 0; i < data.Length; i++)
                    //{
                    int total = 0;
                    int totalItem = 0;
                    int totalQty = 0;
                    foreach (var item in data)
                    {
                        total += Convert.ToInt32(item.Total);
                        totalItem += data.Count();
                        totalQty += item.Current_Qty.Value;
                        _ListGrid_Temp.Add(new ItemModel
                        {
                            PURCHASE_UNIT = item.PURCHASE_UNIT_PRICE.ToString(),
                            TaxName = item.TaxName,
                            TaxValue = item.TaxValue,
                            INVOICE_NO = item.INVOICE_NO,
                            PAYMENT_STATUS = item.PAYMENT_STATUS,
                            SUPPLIER_NAME = item.SUPPLIER_NAME,
                            INVOICE_DATE = item.DATE,
                            PO_NUMBER = item.PO_NUMBER,
                            ITEM_NAME = item.ITEM_NAME,
                            PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                            SALES_UNIT = item.SALES_UNIT,
                            SALES_PRICE = item.SALES_PRICE,
                            SLNO = item.SLNO,
                            BARCODE = item.BARCODE,
                            BUSINESS_LOC = item.BUSINESS_LOC,
                            CUSTOMER_NAME = item.CUSTOMER_NAME,
                            CUSTOMER_MOBILE = item.CUSTOMER_MOBILE,
                            CUSTOMER_EMAIL = item.CUSTOMER_EMAIL,
                            Current_Qty = item.Current_Qty,
                            Total = item.Total,                            
                            TOTAL_ITEM  = item.TOTAL_ITEM,
                            TotalTax = item.TotalTax,
                            Discount = item.Discount,
                            QUNT_TOTAL = item.QUNT_TOTAL
                        });
                        
                    }
                    //ListGridSales.Clear();
                    TOTAL_SUM = total;
                    TOTAL_ITEM = totalItem;
                    TOTAL_QTY = totalQty;
                    ListGridItemDetails = _ListGrid_Temp;
                    App.Current.Properties["GetItemsellsDetails"] = _getItemList;
                    App.Current.Properties["ListGridSales"] = _ListGrid_Temp;
                    //App.Current.Properties["ItemNameSales"] = SelectedItem.ITEM_NAME;
                    //App.Current.Properties["ItemCodeSales"] = SelectedItem.ITEM_ID;
                    //ITEM_NAME = App.Current.Properties["ItemNameSales"].ToString();
                    //ITEM_ID = Convert.ToInt32(App.Current.Properties["ItemCodeSales"].ToString());
                    //ListGridSales = App.Current.Properties["ListGridSales"] as List<ItemModel>;
                    ViewSalesInvoiceDetails sh = new ViewSalesInvoiceDetails();
                    sh.DataContext = this;

                    sh.Show();
                }

            }
            else
            {
                MessageBox.Show("Please select item");
                return;
            }
        }
        
        public ICommand _SalesReturnInvoice { get; set; }
        public ICommand SalesReturnInvoice
        {
            get
            {
                if (_SalesReturnInvoice == null)
                {
                    _SalesReturnInvoice = new DelegateCommand(SalesReturnInvoice_Click);
                }
                return _SalesReturnInvoice;
            }
        }
        public async void SalesReturnInvoice_Click()
        {
            if (App.Current.Properties["GetItemDetails"] != null)
            {
                App.Current.Properties["Action"] = "SalesReturnManger";
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                var ItemName = SelectedItem.ITEM_NAME;
                //ListGridSales = new List<ItemModel>();
                ItemModel get = App.Current.Properties["GetItemDetails"] as ItemModel;
                string itemid = get.INVOICE_NO;

                int companyid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/SalesRetunAPI/GetSalesReturnManager?id=" + itemid + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    //for (int i = 0; i < data.Length; i++)
                    //{
                    foreach (var item in data)
                    {
                        int total = 0;
                        _ListGrid_Temp.Add(new ItemModel
                        {
                           BUSINESS_LOC = item.BUSINESS_LOC,
                           SALES_RETURN_NO = item.SALES_RETURN_NO,
                           DATE = item.DATE,
                           INVOICE_NO = item.INVOICE_NO,
                           Current_Qty = item.Current_Qty,
                           RETURNABLE_AMOUNT = item.RETURNABLE_AMOUNT,
                           CUSTOMER_NAME = item.CUSTOMER_NAME,
                           CUSTOMER_MOBILE = item.CUSTOMER_MOBILE,
                           
                        });

                    }
                    //ListGridSales.Clear();

                    ListGridSalesReturn = _ListGrid_Temp;

                    App.Current.Properties["ListGridSalesReturn"] = _ListGrid_Temp;
                    SalesReturnManager sh = new SalesReturnManager();
                    sh.DataContext = this;
                    sh.Show();
                }

            }
            else
            {
                MessageBox.Show("Please select item");
                return;
            }
        }




        public ICommand _salesreturn { get; set; }
        public ICommand salesreturn
        {
            get
            {
                if (_salesreturn == null)
                {
                    _salesreturn = new DelegateCommand(salesreturn_click);
                }
                return _salesreturn;
            }
        }

        public async void salesreturn_click()
        {
            
                App.Current.Properties["Action"] = "SalesReturnManger";
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                var ItemName = SelectedItem.ITEM_NAME;
                //ListGridSales = new List<ItemModel>();
                ItemModel get = App.Current.Properties["GetItemDetails"] as ItemModel;
                string itemid = get.INVOICE_NO;

                int companyid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/SalesReturnAPI/GetSalesReturnManager?id=" + itemid + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    //for (int i = 0; i < data.Length; i++)
                    //{
                    foreach (var item in data)
                    {
                        int total = 0;
                        _ListGrid_Temp.Add(new ItemModel
                        {
                            BUSINESS_LOC = item.BUSINESS_LOC,
                            SALES_RETURN_NO = item.SALES_RETURN_NO,
                            DATE = item.DATE,
                            INVOICE_NO = item.INVOICE_NO,
                            Current_Qty = item.Current_Qty,
                            RETURNABLE_AMOUNT = item.RETURNABLE_AMOUNT,
                            CUSTOMER_NAME = item.CUSTOMER_NAME,
                            CUSTOMER_MOBILE = item.CUSTOMER_MOBILE,
                            
                        });

                    }
                    //ListGridSales.Clear();

                    ListGridSalesReturn = _ListGrid_Temp;

                    App.Current.Properties["ListGridSalesReturn"] = _ListGrid_Temp;
                    SalesReturnManager sh = new SalesReturnManager();
                    sh.DataContext = this;
                    sh.Show();
                }

          
            
        }



        public ICommand _showslaeshistory { get; set; }
        public ICommand showslaeshistory
        {
            get
            {
                if (_showslaeshistory == null)
                {
                    _showslaeshistory = new DelegateCommand(ShowSalesItem);
                }
                return _showslaeshistory;
            }
        }
        public async void ShowSalesItem()
        {
            if (App.Current.Properties["GetItemDetails"] != null)
            {
                App.Current.Properties["Action"] = "SalesHistory";
                ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
                var ItemName = SelectedItem.ITEM_NAME;
                //ListGridSales = new List<ItemModel>();
                ItemModel get = App.Current.Properties["GetItemDetails"] as ItemModel;
                int itemid = get.ITEM_ID;
                int companyid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllSalesDetails?id=" + companyid + "&itemid=" + itemid + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    //for (int i = 0; i < data.Length; i++)
                    //{
                        foreach(var item in data)
                        {
                            _ListGrid_Temp.Add(new ItemModel
                        {
                            SUPPLIER_NAME = item.SUPPLIER_NAME,
                            INVOICE_DATE = item.DATE,
                            PO_NUMBER = item.PO_NUMBER,
                            ITEM_NAME = item.ITEM_NAME,
                            PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                            SALES_UNIT = item.SALES_UNIT,
                            SALES_PRICE = item.SALES_PRICE,
                            
                        });
                            
                    }
                        //ListGridSales.Clear();
                       
                        ListGridSales = _ListGrid_Temp;
                    App.Current.Properties["GetItemsellsDetails"] = _getItemList;
                    App.Current.Properties["ListGridSales"] = _ListGrid_Temp;
                    //App.Current.Properties["ItemNameSales"] = SelectedItem.ITEM_NAME;
                    //App.Current.Properties["ItemCodeSales"] = SelectedItem.ITEM_ID;
                    //ITEM_NAME = App.Current.Properties["ItemNameSales"].ToString();
                    //ITEM_ID = Convert.ToInt32(App.Current.Properties["ItemCodeSales"].ToString());
                    //ListGridSales = App.Current.Properties["ListGridSales"] as List<ItemModel>;
                    ViewSalesHistory sh = new ViewSalesHistory();
                    sh.DataContext = this;
                    sh.Show();
                }
               
            }
            else
            {
                MessageBox.Show("Please select item");
                return;
            }
        }
        public ICommand _CloseClick;
        public ICommand CloseClick
        {
            get
            {
                if (_CloseClick == null)
                {
                    _CloseClick = new DelegateCommand(Close);
                }
                return _CloseClick;
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
        public ObservableCollection<ItemModel> _ListGridExcelData { get; set; }
        public ObservableCollection<ItemModel> ListGridExcelData
        {
            get
            {
                return _ListGridExcelData;
            }
            set
            {
                this._ListGridExcelData = value;
                OnPropertyChanged("ListGridExcelData");
            }
        }
       
        public ObservableCollection<ItemModel> _ListGridItemDetails { get; set; }
        public ObservableCollection<ItemModel> ListGridItemDetails
        {
            get
            {
                return _ListGridItemDetails;
            }
            set
            {
                this._ListGridItemDetails = value;
                OnPropertyChanged("ListGridItemDetails");
            }
        }
        public ObservableCollection<ItemModel> _ListGridItemPurchase { get; set; }
        public ObservableCollection<ItemModel> ListGridItemPurchase
        {
            get
            {
                return _ListGridItemPurchase;
            }
            set
            {
                this._ListGridItemPurchase = value;
                OnPropertyChanged("ListGridItemPurchase");
            }
        }
        public ObservableCollection<ItemModel> _ListGridSalesReturn { get; set; }
        public ObservableCollection<ItemModel> ListGridSalesReturn
        {
            get
            {
                return _ListGridSalesReturn;
            }
            set
            {
                this._ListGridSalesReturn = value;
                OnPropertyChanged("ListGridSalesReturn");
            }
        }
        public ObservableCollection<ItemModel> _ListGridItemSales { get; set; }
        public ObservableCollection<ItemModel> ListGridItemSales
        {
            get
            {
                return _ListGridItemSales;
            }
            set
            {
                this._ListGridItemSales = value;
                OnPropertyChanged("ListGridItemSales");
            }
        }
        public ObservableCollection<ItemModel> _ListGridPurchase { get; set; }
        public ObservableCollection<ItemModel> ListGridPurchase
        {
            get
            {
                return _ListGridPurchase;
            }
            set
            {
                this._ListGridPurchase = value;
                OnPropertyChanged("ListGridPurchase");
            }
        }
        public ObservableCollection<ItemModel> _ListGridSales { get; set; }
        public ObservableCollection<ItemModel> ListGridSales
        {
            get
            {
                return _ListGridSales;
            }
            set
            {
                this._ListGridSales = value;
                OnPropertyChanged("ListGridSales");
            }
        }
       
        public List<ItemModel> _ListGrid { get; set; }
        public List<ItemModel> ListGrid
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
        public ObservableCollection<ItemModel> _ListGrid1 { get; set; }
        public ObservableCollection<ItemModel> ListGrid1
        {
            get
            {
                return _ListGrid1;
            }
            set
            {
                this._ListGrid1 = value;
                OnPropertyChanged("_ListGrid1");
            }
        }
        public List<TaxManagementModel> _ListGridTax { get; set; }
        public List<TaxManagementModel> ListGridTax
        {
            get
            {
                return _ListGridTax;
            }
            set
            {
                this._ListGridTax = value;
                OnPropertyChanged("ListGridTax");
            }
        }



        #region Excel Properties/Function

        public ICommand _ExcelClick;
        public ICommand ExcelClick
        {
            get
            {
                if (_ExcelClick == null)
                {
                    _ExcelClick = new DelegateCommand(Excel_Click);
                }
                return _ExcelClick;
            }
        }
        public void Excel_Click()
        {
            ImportExcel _AC = new ImportExcel();
            _AC.ShowDialog();
            // ModalService.NavigateTo(new AddCustomer(), delegate(bool returnValue) { });
            //MessageBox.Show("", "", MessageBoxButton.YesNo);


        }

        public ICommand _DownloadExcel;
        public ICommand DownloadExcel
        {
            get
            {
                if (_ExcelClick == null)
                {
                    _ExcelClick = new DelegateCommand(Download_Excel);
                }
                return _ExcelClick;
            }
        }
        public virtual void Download_Excel()
        {
            var str = "http://10.10.10.108/upload/Customer_Excel.xlsx";


        }

        private string _ExcelPath;
        public string ExcelPath
        {
            get { return _ExcelPath; }
            set
            {
                if (Equals(value, _ExcelPath)) return;
                _ExcelPath = value;
                OnPropertyChanged("ExcelPath");
            }
        }

        public ICommand _ExcelImportClick { get; set; }
        public ICommand ExcelImportClick
        {
            get
            {
                if (_ExcelImportClick == null)
                {
                    _ExcelImportClick = new DelegateCommand(ExcelImport_Click);
                }
                return _ExcelImportClick;
            }
        }
        public void ExcelImport_Click()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel file(*.xlsm;*.xlsx;*.xlsx;*.xlt;*.xls; )|*.xlsm;*.xlsx;*.xlsx;*.xlt;*.xls;";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (openFileDialog.ShowDialog() == true)
                {
                }
                if (File.Exists(openFileDialog.FileName))
                {
                    string xx = openFileDialog.FileName;
                    ExcelPath = openFileDialog.FileName;
                    var excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xx + ";Extended Properties=Excel 12.0;");
                    OleDbConnection objOlecon = new OleDbConnection();
                    objOlecon.ConnectionString = excelConnectionString;
                    objOlecon.Open();
                    OleDbDataAdapter objOleDa = new OleDbDataAdapter("Select * from [Sheet1$]", objOlecon);
                    DataTable objdt = new DataTable();
                    objOleDa.Fill(objdt);
                    if (objdt.Rows.Count > 0)
                    {

                        var df = objdt.Rows[0];
                        SelectedItem.BARCODE = Convert.ToString(df.ItemArray[0]);
                        SelectedItem.ITEM_NAME = Convert.ToString(df.ItemArray[6]);
                        SelectedItem.ITEM_DESCRIPTION = Convert.ToString(df.ItemArray[3]);
                        SelectedItem.ITEM_UNIQUE_NAME = Convert.ToString(df.ItemArray[1]);
                        SelectedItem.ITEM_GROUP_NAME = Convert.ToString(df.ItemArray[2]);
                        SelectedItem.REGIONAL_LANGUAGE = Convert.ToString(df.ItemArray[4]);
                        SelectedItem.CATEGORY_NAME = Convert.ToString(df.ItemArray[5]);
                        SelectedItem.SEARCH_CODE = Convert.ToString(df.ItemArray[7]);
                        SelectedItem.TAX_PAID = Convert.ToDecimal(df.ItemArray[11]);
                        SelectedItem.TAX_COLLECTED = Convert.ToDecimal(df.ItemArray[12]);
                        SelectedItem.PURCHASE_UNIT = Convert.ToString(df.ItemArray[13]);
                        SelectedItem.SALES_UNIT = Convert.ToString(df.ItemArray[14]);
                        //SelectedItem.PURCHASE_UNIT_PRICE = Convert.ToDecimal(df.ItemArray[10]);
                        //SelectedItem.SALES_PRICE = Convert.ToDecimal(df.ItemArray[8]);
                        SelectedItem.MRP = Convert.ToDecimal(df.ItemArray[15]);
                        //SelectedItem.SALES_PRICE_BEFOR_TAX = Convert.ToDecimal(df.ItemArray[16]);
                        SelectedItem.IMAGE_PATH = Convert.ToString(df.ItemArray[20]);
                        SelectedItem.ITEM_DESCRIPTION = Convert.ToString(df.ItemArray[21]);
                        SelectedItem.KEYWORD = Convert.ToString(df.ItemArray[18]);
                        //SelectedItem.WEIGHT_OF_PAPER = Convert.ToDecimal(df.ItemArray[17]);
                        //SelectedItem.WEIGHT_OF_PLASTIC = Convert.ToDecimal(df.ItemArray[17]);
                        //SelectedItem.WEIGHT_OF_FOAM = Convert.ToDecimal(df.ItemArray[17]);
                        //SelectedItem.WEIGHT_OF_CARDBOARD = Convert.ToDecimal(df.ItemArray[17]);
                        //SelectedItem.ACCESSORIES_KEYWORD = Convert.ToString(df.ItemArray[17]);
                        //SelectedItem.ACCESSORIES_KEYWORD = Convert.ToString(df.ItemArray[17]);
                    }

                    App.Current.Properties["ExcelData"] = SelectedItem;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ICommand _InsertToExcel { get; set; }
        public ICommand InsertToExcel
        {
            get
            {
                if (_InsertToExcel == null)
                {
                    _InsertToExcel = new DelegateCommand(InsertTo_Excel);
                }
                return _InsertToExcel;
            }
        }
        public void InsertTo_Excel()
        {
            if (App.Current.Properties["ExcelData"] != null)
            {
                SelectedItem = App.Current.Properties["ExcelData"] as ItemModel;
                Insert_Item();
                App.Current.Properties["ExcelData"] = null;
            }
        }

        #endregion
        

        public ICommand _ExcelImportClickItem { get; set; }
        public ICommand ExcelImportClickItem
        {
            get
            {
                if (_ExcelImportClickItem == null)
                {
                    _ExcelImportClickItem = new DelegateCommand(ExcelImportItem_Click);
                }
                return _ExcelImportClick;
            }
        }
        public void ExcelImportItem_Click()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel file(*.xlsm;*.xlsx;*.xlsx;*.xlt; )|*.xlsm;*.xlsx;*.xlsx;*.xlt;";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (openFileDialog.ShowDialog() == true)
                {
                }
                if (File.Exists(openFileDialog.FileName))
                {
                    string xx = openFileDialog.FileName;
                    ExcelPath = openFileDialog.FileName;
                    var excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xx + ";Extended Properties=Excel 12.0;");
                    OleDbConnection objOlecon = new OleDbConnection();
                    objOlecon.ConnectionString = excelConnectionString;
                    objOlecon.Open();
                    OleDbDataAdapter objOleDa = new OleDbDataAdapter("Select * from [Sheet1$]", objOlecon);
                    DataTable objdt = new DataTable();
                    objOleDa.Fill(objdt);
                    if (objdt.Rows.Count > 0)
                    {

                        var df = objdt.Rows[0];
                        SelectedItem.BARCODE = Convert.ToString(df.ItemArray[0]);
                        SelectedItem.ITEM_NAME = Convert.ToString(df.ItemArray[6]);
                        SelectedItem.ITEM_DESCRIPTION = Convert.ToString(df.ItemArray[3]);
                        SelectedItem.ITEM_UNIQUE_NAME = Convert.ToString(df.ItemArray[1]);
                        SelectedItem.ITEM_GROUP_NAME = Convert.ToString(df.ItemArray[2]);
                        SelectedItem.REGIONAL_LANGUAGE = Convert.ToString(df.ItemArray[4]);
                        SelectedItem.CATEGORY_NAME = Convert.ToString(df.ItemArray[5]);
                        SelectedItem.SEARCH_CODE = Convert.ToString(df.ItemArray[7]);
                        SelectedItem.TAX_PAID = Convert.ToDecimal(df.ItemArray[11]);
                        SelectedItem.TAX_COLLECTED = Convert.ToDecimal(df.ItemArray[12]);
                        SelectedItem.PURCHASE_UNIT = Convert.ToString(df.ItemArray[13]);
                        SelectedItem.SALES_UNIT = Convert.ToString(df.ItemArray[14]);
                        SelectedItem.PURCHASE_UNIT_PRICE = Convert.ToDecimal(df.ItemArray[10]);
                        SelectedItem.SALES_PRICE = Convert.ToDecimal(df.ItemArray[8]);
                        SelectedItem.MRP = Convert.ToDecimal(df.ItemArray[15]);
                        SelectedItem.SALES_PRICE_BEFOR_TAX = Convert.ToDecimal(df.ItemArray[16]);
                        SelectedItem.IMAGE_PATH = Convert.ToString(df.ItemArray[20]);
                        SelectedItem.ITEM_DESCRIPTION = Convert.ToString(df.ItemArray[21]);
                        SelectedItem.KEYWORD = Convert.ToString(df.ItemArray[18]);
                        SelectedItem.WEIGHT_OF_PAPER = Convert.ToDecimal(df.ItemArray[17]);
                        SelectedItem.WEIGHT_OF_PLASTIC = Convert.ToDecimal(df.ItemArray[17]);
                        SelectedItem.WEIGHT_OF_FOAM = Convert.ToDecimal(df.ItemArray[17]);
                        SelectedItem.WEIGHT_OF_CARDBOARD = Convert.ToDecimal(df.ItemArray[17]);
                        SelectedItem.ACCESSORIES_KEYWORD = Convert.ToString(df.ItemArray[17]);
                        SelectedItem.ACCESSORIES_KEYWORD = Convert.ToString(df.ItemArray[17]);
                    }

                    App.Current.Properties["ExcelData"] = SelectedItem;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }





        #region Item Attribute properties

        public bool _IS_Not_For_Sell;
        public bool IS_Not_For_Sell
        {
            get
            {
                return SelectedItem.IS_Not_For_Sell;
            }
            set
            {
                if (SelectedItem.IS_Not_For_Sell != value)
                {
                    SelectedItem.IS_Not_For_Sell = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IS_Not_For_Sell"));
                    }
                }
            }
        }

        public bool _IS_Purchased;
        public bool IS_Purchased
        {
            get
            {
                return SelectedItem.IS_Purchased;
            }
            set
            {
                if (SelectedItem.IS_Purchased != value)
                {
                    SelectedItem.IS_Purchased = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IS_Purchased"));
                    }
                }
            }
        }

        public bool _IS_Service_Item;
        public bool IS_Service_Item
        {
            get
            {
                return SelectedItem.IS_Service_Item;
            }
            set
            {
                if (SelectedItem.IS_Service_Item != value)
                {
                    SelectedItem.IS_Service_Item = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IS_Service_Item"));
                    }
                }
            }
        }

        public bool _IS_Gift_Card;
        public bool IS_Gift_Card
        {
            get
            {
                return SelectedItem.IS_Gift_Card;
            }
            set
            {
                if (SelectedItem.IS_Gift_Card != value)
                {
                    SelectedItem.IS_Gift_Card = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IS_Gift_Card"));
                    }
                }
            }
        }

        public bool _ALLOW_PURCHASE_ON_ESHOP;
        public bool ALLOW_PURCHASE_ON_ESHOP
        {
            get
            {
                return SelectedItem.ALLOW_PURCHASE_ON_ESHOP;
            }
            set
            {
                if (SelectedItem.ALLOW_PURCHASE_ON_ESHOP != value)
                {
                    SelectedItem.ALLOW_PURCHASE_ON_ESHOP = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ALLOW_PURCHASE_ON_ESHOP"));
                    }
                }
            }
        }

        public bool _IS_For_Online_Shop;
        public bool IS_For_Online_Shop
        {
            get
            {
                return SelectedItem.IS_For_Online_Shop;
            }
            set
            {
                if (SelectedItem.IS_For_Online_Shop != value)
                {
                    SelectedItem.IS_For_Online_Shop = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IS_For_Online_Shop"));
                    }
                }
            }
        }

        public bool _IS_Not_for_online_shop;
        public bool IS_Not_for_online_shop
        {
            get
            {
                return SelectedItem.IS_Not_for_online_shop;
            }
            set
            {
                if (SelectedItem.IS_Not_for_online_shop != value)
                {
                    SelectedItem.IS_Not_for_online_shop = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IS_Not_for_online_shop"));
                    }
                }
            }
        }

        #endregion


        private string _CreatVisible { get; set; }
        public string CreatVisible
        {

            get { return _CreatVisible; }
            set
            {
                if (value != _CreatVisible)
                {
                    _CreatVisible = value;
                    OnPropertyChanged("CreatVisible");
                }
            }
        }

        private string _UpdVisible { get; set; }
        public string UpdVisible
        {

            get { return _UpdVisible; }
            set
            {
                if (value != _UpdVisible)
                {
                    _UpdVisible = value;
                    OnPropertyChanged("UpdVisible");
                }
            }
        }

        private string _IMAGE_VISIBLE;
        public string IMAGE_VISIBLE
        {

            get { return _IMAGE_VISIBLE; }
            set
            {

                _IMAGE_VISIBLE = value;
                OnPropertyChanged("IMAGE_VISIBLE");

            }
        }

        #region Image Load/Remove
        public ICommand _ImgLoad { get; set; }
        public ICommand ImgLoad
        {
            get
            {
                if (_ImgLoad == null)
                {
                    _ImgLoad = new DelegateCommand(Img_Load);

                }
                return _ImgLoad;
            }
        }
        public void Img_Load()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files(*.jpg; .jpeg; .gif; .bmp)|*.jpg; .jpeg; .gif; .bmp";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {

                if (File.Exists(openFileDialog.FileName))
                {
                    IMAGE_PATH1 = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                    // IMAGE_PATH = Convert.ToString( new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute)));
                    string imagepath = openFileDialog.FileName.ToString();
                    var imageFile = new System.IO.FileInfo(imagepath);
                    string file = imageFile.Name;
                    var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                    // get your 'Uploaded' folder
                    var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));
                    //if (!dir.Exists)
                    //    dir.Create();
                    // Copy file to your folder
                    imageFile.CopyTo(System.IO.Path.Combine(dir.FullName + "\\", file), true);
                    string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);

                    Ftpup(path1, openFileDialog.SafeFileName);
                    SelectedItem.IMAGE_PATH = openFileDialog.SafeFileName;
                }


            }


        }


        public ICommand _RemovedImg { get; set; }
        public ICommand RemovedImg
        {
            get
            {
                if (_RemovedImg == null)
                {
                    _RemovedImg = new DelegateCommand(Removed_Img);
                }
                return _RemovedImg;
            }
        }
        public void Removed_Img()
        {
            IMAGE_PATH1 = null;
        }

        #endregion

        #region FileUpload /DownLoad
        public static void Ftpup(string sourceFile, string targetFile)
        {
            try
            {
                //string ftpServerIP = "115.115.196.30";
                //string ftpUserID = "suvendu";
                //string ftpPassword = "Passw0rd";

                string ftpServerIP = "54.70.197.231";
                string ftpUserID = "suvendu";
                string ftpPassword = "vpY9dNp3W9AqhjGy";

                string filename = "ftp://" + ftpServerIP + "//Upload//" + targetFile;
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(filename);
                ftpReq.UseBinary = true;

                ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
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

        public static void FtpDown(string sourceFile, string targetFile)
        {
            try
            {
                //string ftpServerIP = "115.115.196.30";
                //string ftpUserID = "suvendu";
                //string ftpPassword = "Passw0rd";

                string ftpServerIP = "54.70.197.231";
                string ftpUserID = "suvendu";
                string ftpPassword = "vpY9dNp3W9AqhjGy";

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
        #endregion







        public CategoryViewModel _userVM;
        public CategoryViewModel userVM
        {
            get { return _userVM; }
            set
            {
                _userVM = value;
                RaisePropertyChanged("userVM");
            }
        }


        public DelegateCommand _executeCmd;
        public DelegateCommand ExecuteCmd
        {
            get { return _executeCmd; }
            set
            {
                _executeCmd = value;
                RaisePropertyChanged("ExecuteCmd");
            }
        }
        CategoryModel[] dataCat = null;
        List<CategoryModel> _ListGrid_Catagory = new List<CategoryModel>();
        public async Task<ObservableCollection<CategoryModel>> GetCatagory(long comp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CatagoryAPI/GetCatagory?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    dataCat = JsonConvert.DeserializeObject<CategoryModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < dataCat.Length; i++)
                    {
                        x++;
                        _ListGrid_Catagory.Add(new CategoryModel
                        {
                            SLNO = x,
                            BAR_CODE_PREFIX = dataCat[i].BAR_CODE_PREFIX,
                            CATAGORY_DEC = dataCat[i].CATAGORY_DEC,
                            CATAGORY_ID = dataCat[i].CATAGORY_ID,
                            CATAGORY_NAME = dataCat[i].CATAGORY_NAME,
                            COMPANY_ID = dataCat[i].COMPANY_ID,
                            DISPLAY_INDEX = dataCat[i].DISPLAY_INDEX,
                            IS_DELETE = dataCat[i].IS_DELETE,
                            IS_NOT_PROTAL = dataCat[i].IS_NOT_PROTAL,

                        });
                        autoCatList.Add(new CategoryAutoListModel
                        {
                            DisplayName = dataCat[i].CATAGORY_NAME,
                            DisplayId = dataCat[i].CATAGORY_ID
                        });
                    }

                }
                // ListGrid = _ListGrid_Catagory;
                App.Current.Properties["AutoCatList"] = autoCatList;
                return new ObservableCollection<CategoryModel>(_ListGrid_Catagory);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public ItemViewModel()
        {

            
            //FORM_DATE = System.DateTime.Today;
            //TO_DATE = System.DateTime.Today;
            App.Current.Properties["IMG_HideShow"] = false;
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            DATE = System.DateTime.Now;
            SelectedPO = new POrderModel();
            SelectedItem = new ItemModel();
            SelectedOpeningStock = new OpeningStockModel();
            var Load_ModuleAccess = (((IEnumerable)App.Current.Properties["AccessModuleByUser"]).Cast<UserAccessModel>()).ToList();
            _IMAGE_VISIBLE = "Hidden";
            IMAGE_VISIBLE = "Hidden";
            foreach (var item in Load_ModuleAccess)
            {
                if (item.MODULE_ID == 8)
                {
                    AddEnable = item.ACTION_CREATE;
                    EditEnable = item.EDIT;
                    ViewEnable = item.ACTION_VIEW;
                    DeleteEnable = item.ACTION_DELETE;
                }
            }
            IsVisibleTax = "Collapsed";
            DEFINECODEVISIBLE = "Visible";
            AUTOCODEVISIBLE = "Collapsed";
            BARCODEENABLE = false;
            App.Current.Properties["ItemDiffProperties"] = SelectedItem;

            if (App.Current.Properties["Action"].ToString() == "Edit")
            {

                BARCODEENABLE = true;
                AUTOCODEVISIBLE = "Visible";
                DEFINECODEVISIBLE = "Collapsed";
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedItem = App.Current.Properties["ItemEdit"] as ItemModel;
                INCLUDE_TAX = SelectedItem.INCLUDE_TAX;
                if (App.Current.Properties["Qunt"] != null && App.Current.Properties["Qunt"] != "0")
                {
                    OPN_QNT = Convert.ToInt32(App.Current.Properties["Qunt"]);
                }
                else
                {
                    OPN_QNT = Convert.ToInt32(SelectedItem.OPN_QNT);
                }

                DATE = Convert.ToDateTime(SelectedItem.DATE);
                App.Current.Properties["barcode"] = SelectedItem.BARCODE;
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "Add")
            {
                CreatVisible = "Visible";
                UpdVisible = "Collapsed";
                GetItem(comp);
                App.Current.Properties["Action"] = "";


            }
            else if (App.Current.Properties["Action"].ToString() == "SalesHistory")
            {
                CreatVisible = "Visible";
                UpdVisible = "Collapsed";
                //GetItem(comp);
                App.Current.Properties["Action"] = "";


            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                SelectedItem = App.Current.Properties["ItemView"] as ItemModel;

                App.Current.Properties["Action"] = "";
                // GetItem(comp);
            }
            else if (App.Current.Properties["Action"].ToString() == "Search")
            {
                SelectedItem = App.Current.Properties["ItemEdit"] as ItemModel;
                App.Current.Properties["Action"] = "";

            }
            else if (App.Current.Properties["Action"].ToString() == "MainPageAdd")
            {
                SelectedItem.BARCODE = App.Current.Properties["BareCode"].ToString();
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["GetItemDetails"] != null)
            {
                ItemModel getitem = App.Current.Properties["GetItemDetails"] as ItemModel;
                if (App.Current.Properties["GetItemsellsDetails"] != null)
                {
                    ListGrid1 = App.Current.Properties["GetItemsellsDetails"] as ObservableCollection<ItemModel>;
                    //string kj=ListGrid1[0].itemid
                    //ViewSalesHistory.datagridref.ItemsSource = ListGrid1;
                }
            }
            else
            {
                //if (App.Current.Properties["ItemDiffProperties"] != null)
                //{
                //    var ss = GetbarcodeCode();
                //    SelectedItem = App.Current.Properties["ItemDiffProperties"] as ItemModel;
                //    //SelectedItem=App.Current.Properties["ItemDiffPropertiesTax"] as ItemModel;
                //    _CATEGORY_NAME = SelectedItem.CATEGORY_NAME;
                //    CATEGORY_NAME = _CATEGORY_NAME;
                //    TAX_PAID = SelectedItem.TAX_PAID;
                //    TAX_COLLECTED = SelectedItem.TAX_COLLECTED;
                //    SelectedItem.CATAGORY_ID = SelectedItem.CATAGORY_ID;
                //    UpdVisible = "Collapsed";
                //    CreatVisible = "Visible";

                //}
                //else
                //{
                if (App.Current.Properties["ITemAdd"] != null)
                {
                    GetbarcodeCode();
                    if (App.Current.Properties["itemName"] != null && App.Current.Properties["itemName"] != "")
                    {
                        ITEM_NAME = App.Current.Properties["itemName"].ToString();
                    }

                }
                else
                {

                }
                if (App.Current.Properties["Qunt"] != null && App.Current.Properties["Qunt"] != "0")
                {
                    OPN_QNT = Convert.ToInt32(App.Current.Properties["Qunt"]);
                }
                INCLUDE_TAX = true;
                ENABLE_BEFOR_TAX = false;
                ENABLE_SALES_PRICE = true;
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                GetItem(comp);
                if (App.Current.Properties["CurrentBarcode"] != null)
                {
                    SelectedItem.BARCODE = App.Current.Properties["CurrentBarcode"].ToString();
                    App.Current.Properties["CurrentBarcode"] = null;
                }

                // }
            }
            GetCatagory(comp);

            TaxViewModel TVM = new TaxViewModel();
            //TVM.GetTaxList(comp);
            TVM.GetTaxList(comp);


            BussinessLocationViewModel BLM = new BussinessLocationViewModel();
            BLM.GetBussinessList(comp);

            GodownViewModel GVM = new GodownViewModel();
            GVM.GetGodowns(comp);


            UnitViewModel UVM = new UnitViewModel();
            UVM.GetUnit(comp);
            //SelectedItem = new ItemModel();
            SelectedItem.FORM_DATE = System.DateTime.Now.Date;
            SelectedItem.TO_DATE = System.DateTime.Now.Date;
            //if (App.Current.Properties["TaxCollectid1"] != null)
            //{
            //    SelectedItem.TAX_COLLECTED_ID = Convert.ToInt32(App.Current.Properties["TaxCollectid1"].ToString());
            //}

            // SelectedItem.TAX_COLLECTED_ID = Convert.ToInt32(App.Current.Properties["TaxCollectid1"].ToString());
            // Items.ListGridRef.Columns[1].Visibility = Visibility.Hidden;

        }

        public async Task<string> GetbarcodeCode()
        {
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            string uhy = "";
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetBarcode?CompanyId=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                    uhy = await response.Content.ReadAsStringAsync();
                }

                string dd = Convert.ToString(uhy);
                string aaa = dd.Substring(1, 8);
                BARCODE = aaa;
            }
            catch (Exception ex)
            { }

            return uhy;
        }

        public ICommand _AUTOCODE;
        public ICommand AUTOCODE
        {
            get
            {
                if (_AUTOCODE == null)
                {
                    _AUTOCODE = new DelegateCommand(AUTO_CODE);
                }
                return _AUTOCODE;
            }
        }
        public void AUTO_CODE()
        {
            DEFINECODEVISIBLE = "Visible";
            AUTOCODEVISIBLE = "Collapsed";
            BARCODEENABLE = false;
            GetbarcodeCode();
            //SelectedItem.BARCODE = App.Current.Properties["Item_Edit"].ToString();
            SelectedItem.BARCODE = null;
        }

        public ICommand _ItemBarcode;
        public ICommand ItemBarcode
        {
            get
            {
                if (_ItemBarcode == null)
                {
                    _ItemBarcode = new DelegateCommand(ItemBar_code);
                }
                return _ItemBarcode;
            }
        }
        public void ItemBar_code()
        {

            DEFINECODEVISIBLE = "Collapsed";
            AUTOCODEVISIBLE = "Visible";
            BARCODEENABLE = true;
            if (App.Current.Properties["barcode"] != null)
            {
                BARCODE = App.Current.Properties["barcode"].ToString();
            }
            else
            {
                BARCODE = "";
            }

            //var yt = GetbarcodeCode();
            // SelectedItem.BARCODE =Convert.ToString(yt);
        }





        public ICommand _TaxListSellingClick;
        public ICommand TaxListSellingClick
        {
            get
            {
                if (_TaxListSellingClick == null)
                {
                    _TaxListSellingClick = new DelegateCommand(TaxList_Selling_Click);
                }
                return _TaxListSellingClick;
            }
        }

        public void TaxList_Selling_Click()
        {
            App.Current.Properties["TaxListSellingClick"] = 1;
            Window_TaxList IA = new Window_TaxList();
            IA.Show();

        }
        public ICommand _TaxListPurchasingClick;
        public ICommand TaxListPurchasingClick
        {
            get
            {
                if (_TaxListPurchasingClick == null)
                {
                    _TaxListPurchasingClick = new DelegateCommand(TaxList_Purchasing_Click);
                }
                return _TaxListPurchasingClick;
            }
        }

        public void TaxList_Purchasing_Click()
        {
            App.Current.Properties["TaxListPurchasingClick"] = 1;
            Window_TaxList IA = new Window_TaxList();
            IA.ShowDialog();

        }




        private string _CatVal;
        public string CatVal
        {
            get
            {
                return _CatVal;
            }
            set
            {
                _CatVal = value;
                OnPropertyChanged("CatVal");
            }
        }

        //public CategoryAutoScr MySearchProviderEng { get { return new CategoryAutoScr(); } }








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

        private Stack<BackNavigationEventHandler> _backFunctions = new Stack<BackNavigationEventHandler>();

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
