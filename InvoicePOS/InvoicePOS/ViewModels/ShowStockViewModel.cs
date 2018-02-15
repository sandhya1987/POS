using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Item;
using InvoicePOS.Views.Main;
using Newtonsoft.Json;

namespace InvoicePOS.ViewModels
{
    public class ShowStockViewModel
    {

        ItemModel[] data = null;
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

        ObservableCollection<ItemModel> _ListGrid_Temp12 = new ObservableCollection<ItemModel>();
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
        public ObservableCollection<ItemModel> _ListGrid2 { get; set; }
        public ObservableCollection<ItemModel> ListGrid2
        {
            get
            {
                return _ListGrid2;

            }
            set
            {
                if (value != _ListGrid2)
                {
                    this._ListGrid2 = value;
                    OnPropertyChanged("ListGrid2");
                }
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
        public ShowStockViewModel()
        {
            ListGrid2 = new ObservableCollection<ItemModel>();
            if (App.Current.Properties["Company_Id"] != null)
            {
                comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            }
            else
            {
                //GetCompany();
            }


            GetItem(comp);

            if (App.Current.Properties["ShowStockMain"] != null)
            {
                SelectedItem = App.Current.Properties["ShowStockMain"] as ItemModel;
                ITEM_NAME = SelectedItem.ITEM_NAME;
                BARCODE = SelectedItem.BARCODE;
                SALES_PRICE = Convert.ToString(SelectedItem.SALES_PRICE);
                ListGrid2.Add(new ItemModel
                {
                    GODOWN = SelectedItem.GODOWN,
                    OPN_QNT = SelectedItem.OPN_QNT,
                    BARCODE = SelectedItem.BARCODE,
                    SALES_UNIT = SelectedItem.SALES_UNIT,
                    ITEM_NAME = SelectedItem.ITEM_NAME,
                    SALES_PRICE = SelectedItem.SALES_PRICE,

                });
                App.Current.Properties["ShowStockMain"] = null;
                App.Current.Properties["StockGrid"] = ListGrid2;
            }

            //if (App.Current.Properties["StockGridList"] != null)
            //{
            //    ShowStockGrid();
            //}

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
                    OnPropertyChanged("SelectedItem");
                }
            }
        }


        public async Task<ObservableCollection<ItemModel>> GetItem(int comp)
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
                            });

                        }
                        ListGrid = _ListGrid_Temp;
                        App.Current.Properties["StockGridList"] = ListGrid;
                    }

                }


                return ListGrid;

            }
            catch (Exception ex)
            {

                throw;
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
                OnPropertyChanged("Select_BarCode");
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

            ShowStockGrid();
            _Select_BarCode = "";
            Select_BarCode = "";
        }


        #endregion
        public async void ShowStockGrid()
        {
            if (App.Current.Properties["StockGridList"] != null)
            {
                ListGrid2 = new ObservableCollection<ItemModel>();
                var stocklist = App.Current.Properties["StockGridList"] as ObservableCollection<ItemModel>;
                var str12 = (from a in stocklist where a.BARCODE == Select_BarCode select a).FirstOrDefault();
                if (Select_BarCode != "" && Select_BarCode != null)
                {
                    
                    ListGrid2.Add(new ItemModel
                    {
                        GODOWN = str12.GODOWN,
                        OPN_QNT = str12.OPN_QNT,
                        BARCODE = str12.BARCODE,
                        SALES_UNIT = str12.SALES_UNIT,
                        ITEM_NAME = str12.ITEM_NAME,
                        SALES_PRICE = str12.SALES_PRICE,

                    });
                }
                 
                if (ShowStockList.ItemNameReff != null)
                {
                    if (ShowStockList.ItemNameReff.Text != null && ShowStockList.ItemNameReff.Text != "")
                    {
                        ListGrid2 = new ObservableCollection<ItemModel>();
                        var str121 = (from a in stocklist where a.ITEM_NAME == ShowStockList.ItemNameReff.Text select a).FirstOrDefault();
                        if (str121 != null)
                        {

                            ListGrid2.Add(new ItemModel
                            {
                                GODOWN = str121.GODOWN,
                                OPN_QNT = str121.OPN_QNT,
                                BARCODE = str121.BARCODE,
                                SALES_UNIT = str121.SALES_UNIT,
                                ITEM_NAME = str121.ITEM_NAME,
                                SALES_PRICE = str121.SALES_PRICE,
                            });
                        }
                        ShowStockList.ItemNameReff.Text = "";
                    }
                }
                ShowStockList.ListGridRefr.ItemsSource = ListGrid2;
                ShowStockList.ItemNaReff.Text = str12.ITEM_NAME;
                ShowStockList.BarrcodeReff.Text = Convert.ToString(str12.BARCODE);
                ShowStockList.SalepriceReff.Text = Convert.ToString(str12.SALES_PRICE);
            }
            App.Current.Properties["StockGrid"] = ListGrid2;
        }




        private ICommand _ItemClickStock { get; set; }
        public ICommand ItemClickStock
        {
            get
            {
                if (_ItemClickStock == null)
                {
                    _ItemClickStock = new DelegateCommand(ItemClick_Stock);
                }
                return _ItemClickStock;
            }

        }

        public void ItemClick_Stock()
        {
            App.Current.Properties["ItemMainStock"] = 1;
            Window_ItemList ex = new Window_ItemList();
            ex.ShowDialog();
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
