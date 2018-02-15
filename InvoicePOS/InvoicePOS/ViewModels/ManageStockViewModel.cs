using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Item;
using InvoicePOS.UserControll.StockLedger;
using InvoicePOS.UserControll.StockTransfer;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
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
    public class ManageStockViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {

        public HttpResponseMessage response;
        // public HttpClient client = new HttpClient();
        public ICommand select { get; set; }
        private readonly ManageStockModel OprModel;
        ManageStockModel _ManageStockModel = new ManageStockModel();
        private ManageStockModel _selectedManageStock;
        GodownModel[] data = null;
        ItemModel[] dataItem = null;
        ObservableCollection<GodownModel> _ListGrid_Temp = new ObservableCollection<GodownModel>();
        public ManageStockViewModel()
        {
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            _selectedManageStock = new ManageStockModel();
            GetGodowns(comp);
            GetItem(comp);
        }


        private ICommand _StockCurrection { get; set; }
        public ICommand StockCurrection
        {
            get
            {
                if (_StockCurrection == null)
                {
                    _StockCurrection = new DelegateCommand(Stock_Currection);
                }
                return _StockCurrection;
            }
        }
        public void Stock_Currection()
        {
            // ModalService.NavigateTo(new ReceivePayment(), delegate(bool returnValue) { });
        }

        private ICommand _StockTransferClick { get; set; }
        public ICommand StockTransferClick
        {
            get
            {
                if (_StockTransferClick == null)
                {
                    _StockTransferClick = new DelegateCommand(StockTransfer_Click);
                }
                return _StockTransferClick;
            }
        }
        public void StockTransfer_Click()
        {
            ModalService.NavigateTo(new StockTransfer(), delegate(bool returnValue) { });
        }

        private ICommand _ShowTransactionClick { get; set; }
        public ICommand ShowTransactionClick
        {
            get
            {
                if (_ShowTransactionClick == null)
                {
                    _ShowTransactionClick = new DelegateCommand(ShowTransaction_Click);
                }
                return _ShowTransactionClick;
            }
        }
        public void ShowTransaction_Click()
        {
            //  ModalService.NavigateTo(new ReceivePayment(), delegate(bool returnValue) { });
        }

        private ICommand _StockDetailsClick { get; set; }
        public ICommand StockDetailsClick
        {
            get
            {
                if (_StockDetailsClick == null)
                {
                    _StockDetailsClick = new DelegateCommand(StockDetails_Click);
                }
                return _StockDetailsClick;
            }
        }
        public void StockDetails_Click()
        {
            // ModalService.NavigateTo(new ReceivePayment(), delegate(bool returnValue) { });
        }



        private ICommand _ShowItemClick { get; set; }
        public ICommand ShowItemClick
        {
            get
            {
                if (_ShowItemClick == null)
                {
                    _ShowItemClick = new DelegateCommand(ShowItem_Click);
                }
                return _ShowItemClick;
            }
        }
        public void ShowItem_Click()
        {
            ModalService.NavigateTo(new Items(), delegate(bool returnValue) { });
        }


        private ICommand _StockLedgerClick { get; set; }
        public ICommand StockLedgerClick
        {
            get
            {
                if (_StockLedgerClick == null)
                {
                    _StockLedgerClick = new DelegateCommand(StockLedger_Click);
                }
                return _StockLedgerClick;
            }
        }
        public void StockLedger_Click()
        {
             ModalService.NavigateTo(new StockLedgerList(), delegate(bool returnValue) { });
        }
        private List<ManageStockModel> _ManageStockData;
        public List<ManageStockModel> ManageStockData
        {
            get { return _ManageStockData; }
            set
            {
                if (_ManageStockData != value)
                {
                    _ManageStockData = value;
                }
            }

        }

        private GodownModel _selectedItem;
        public GodownModel SelectedItem
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
        //public async Task<ObservableCollection<ManageStockModel>> GetSelectedManageStock(int comp)
        //{
        //    ManageStockData = new List<ManageStockModel>();
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.Timeout = new TimeSpan(500000000000);
        //    HttpResponseMessage response = client.GetAsync("api/ManageStockAPI/GetManageStock?id=" + comp + "").Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        data = JsonConvert.DeserializeObject<ManageStockModel[]>(await response.Content.ReadAsStringAsync());
        //        for (int i = 0; i < data.Length; i++)
        //        {
        //            _ListGrid_Temp.Add(new ManageStockModel
        //            {
        //                BUSSINESS_LOCATION = data[i].BUSSINESS_LOCATION,
        //                BUSSINESS_LOCATION_ID = data[i].BUSSINESS_LOCATION_ID,
        //                COMPANY_ID = data[i].COMPANY_ID,
        //                DESCRIPTION = data[i].DESCRIPTION,
        //                GODOWN_NAME = data[i].GODOWN_NAME,
        //                GODOWN_NAME_ID = data[i].GODOWN_NAME_ID,
        //                IS_ACTIVE = data[i].IS_ACTIVE,
        //                IS_DEFAULT_GODOWN = data[i].IS_DEFAULT_GODOWN,
        //                STOCK_CORRECTION = data[i].STOCK_CORRECTION,
        //            });
        //        }
        //    }
        //    ListGrid = _ListGrid_Temp;
        //    return new ObservableCollection<ManageStockModel>(_ListGrid_Temp);
        //}

        public ICommand _ShowItem { get; set; }
        public ICommand ShowItem
        {
            get
            {
                if (_ShowItem == null)
                {

                    _ShowItem = new DelegateCommand(Show_Item);
                }
                return _ShowItem;
            }
        }



        public async void Show_Item()
        {
            try
            {

                if (SelectedItem.GODOWN_ID != 0)
                {

                    var godown = SelectedItem.GODOWN_ID;
                    App.Current.Properties["Godown"] = SelectedItem.GODOWN_ID;
                    var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                 //   ItemData = new List<ItemModel>();
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    //HttpResponseMessage response = client.GetAsync("api/LogInAPI/GetUser?id=" + USERNAME + "&password=" + PASSWORD + "").Result;
                    HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem1?id=" + godown + "&comp=" + comp + "").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        _ListGrid_Item.Clear();
                        dataItem = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                        if (dataItem.Length > 0)
                        {
                            int x = 0;
                            for (int i = 0; i < dataItem.Length; i++)
                            {

                                x++;
                                _ListGrid_Item.Add(new ItemModel
                                {
                                    // ITEM_ID = ItemId,
                                    SLNO = x,
                                    ITEM_NAME = dataItem[i].ITEM_NAME,
                                    //ITEM_LOCATION_NAME = dataItem[i].ITEM_LOCATION_NAME,
                                    ITEM_ID = dataItem[i].ITEM_ID,
                                    BARCODE = dataItem[i].BARCODE,
                                    ACCESSORIES_KEYWORD = dataItem[i].ACCESSORIES_KEYWORD,
                                    CATAGORY_ID = dataItem[i].CATAGORY_ID,
                                    ITEM_DESCRIPTION = dataItem[i].ITEM_DESCRIPTION,
                                    ITEM_INVOICE_ID = dataItem[i].ITEM_INVOICE_ID,
                                    ITEM_PRICE = dataItem[i].ITEM_PRICE,
                                    ITEM_PRODUCT_ID = dataItem[i].ITEM_PRODUCT_ID,
                                    GODOWN_ID = dataItem[i].GODOWN_ID,

                                    KEYWORD = dataItem[i].KEYWORD,
                                    MRP = dataItem[i].MRP,
                                    PURCHASE_UNIT = dataItem[i].PURCHASE_UNIT,
                                    PURCHASE_UNIT_PRICE = dataItem[i].PURCHASE_UNIT_PRICE,
                                    SALES_PRICE = dataItem[i].SALES_PRICE,
                                    SALES_UNIT = dataItem[i].SALES_UNIT,
                                    SEARCH_CODE = dataItem[i].SEARCH_CODE,
                                    TAX_COLLECTED = dataItem[i].TAX_COLLECTED,
                                    TAX_PAID = dataItem[i].TAX_PAID,
                                    ALLOW_PURCHASE_ON_ESHOP = dataItem[i].ALLOW_PURCHASE_ON_ESHOP,
                                    CATEGORY_NAME = dataItem[i].CATEGORY_NAME,
                                    DISPLAY_INDEX = dataItem[i].DISPLAY_INDEX,
                                    INCLUDE_TAX = dataItem[i].INCLUDE_TAX,
                                    ITEM_GROUP_NAME = dataItem[i].ITEM_GROUP_NAME,
                                    ITEM_UNIQUE_NAME = dataItem[i].ITEM_UNIQUE_NAME,
                                    OPN_QNT = dataItem[i].OPN_QNT,
                                    REGIONAL_LANGUAGE = dataItem[i].REGIONAL_LANGUAGE,
                                    SALES_PRICE_BEFOR_TAX = dataItem[i].SALES_PRICE_BEFOR_TAX,
                                    MODIFICATION_DATE = dataItem[i].MODIFICATION_DATE,


                                });

                            }
                        }
                        else
                        {
                            MessageBox.Show("No Item Found in this Warehouse...");

                        }

                    }
                    //App.Current.Properties["ItemShow"] = SelectedItem;
                    //App.Current.Properties["ItemToList"] = _ListGrid_Item;
                    ItemListGrid = _ListGrid_Item;
                   // ItemLocationList.ListGridRef.ItemsSource = _ListGrid_Item;

                }
                // return new ObservableCollection<ItemModel>(_ListGrid_Item);
                else
                {
                    MessageBox.Show("No Item Found in this Warehouse...");

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }






        public async Task<ObservableCollection<GodownModel>> GetGodowns(long comp)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/GodownAPI/GetGodown?id=" + comp + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<GodownModel[]>(await response.Content.ReadAsStringAsync());
                int x = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    x++;
                    _ListGrid_Temp.Add(new GodownModel
                    {
                        SLNO=x,
                        COMPANY_ID = data[i].COMPANY_ID,
                        GODOWN_ID = data[i].GODOWN_ID,
                        GODOWN_NAME = data[i].GODOWN_NAME,
                        GOSOWN_DESCRIPTION = data[i].GOSOWN_DESCRIPTION,
                        IS_ACTIVE = data[i].IS_ACTIVE,
                        IS_DELETE = data[i].IS_DELETE,
                          IS_DEFAULT_GODOWN=data[i].IS_DEFAULT_GODOWN,
                    });
                }
            }
            ListGrid = _ListGrid_Temp;
            return new ObservableCollection<GodownModel>(_ListGrid_Temp);
        }
        public ObservableCollection<GodownModel> _ListGrid { get; set; }
        public ObservableCollection<GodownModel> ListGrid
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
        public ManageStockModel selectedManageStock
        {
            get { return _selectedManageStock; }
            set
            {
                if (_selectedManageStock != value)
                {
                    _selectedManageStock = value;
                    OnPropertyChanged("selectedManageStock");
                }
            }
        }
        private string _BUSSINESS_LOCATION;
        public string BUSSINESS_LOCATION
        {
            get
            {
                return selectedManageStock.BUSSINESS_LOCATION;
            }
            set
            {
                selectedManageStock.BUSSINESS_LOCATION = value;

                if (selectedManageStock.BUSSINESS_LOCATION != value)
                {
                    selectedManageStock.BUSSINESS_LOCATION = value;
                    OnPropertyChanged("BUSSINESS_LOCATION");
                }
            }
        }
        private long _BUSSINESS_LOCATION_ID;
        public long BUSSINESS_LOCATION_ID
        {
            get
            {
                return selectedManageStock.BUSSINESS_LOCATION_ID;
            }
            set
            {
                selectedManageStock.BUSSINESS_LOCATION_ID = value;

                if (selectedManageStock.BUSSINESS_LOCATION_ID != value)
                {
                    selectedManageStock.BUSSINESS_LOCATION_ID = value;
                    OnPropertyChanged("BUSSINESS_LOCATION_ID");
                }
            }
        }

        private long _GODOWN_ID;
        public long GODOWN_ID
        {
            get
            {
                return SelectedItem.GODOWN_ID;
            }
            set
            {
                SelectedItem.GODOWN_ID = value;
                if (SelectedItem.GODOWN_ID != value)
                {
                    SelectedItem.GODOWN_ID = value;
                    OnPropertyChanged("GODOWN_ID");
                }
            }
        }

        private string _GODOWN_NAME;
        public string GODOWN_NAME
        {
            get
            {
                return selectedManageStock.GODOWN_NAME;
            }
            set
            {
                selectedManageStock.GODOWN_NAME = value;

                if (selectedManageStock.GODOWN_NAME != value)
                {
                    selectedManageStock.GODOWN_NAME = value;
                    OnPropertyChanged("GODOWN_NAME");
                }
            }
        }
        private long _GODOWN_NAME_ID;
        public long GODOWN_NAME_ID
        {
            get
            {
                return selectedManageStock.GODOWN_NAME_ID;
            }
            set
            {
                selectedManageStock.GODOWN_NAME_ID = value;

                if (selectedManageStock.GODOWN_NAME_ID != value)
                {
                    selectedManageStock.GODOWN_NAME_ID = value;
                    OnPropertyChanged("GODOWN_NAME_ID");
                }
            }
        }
        private bool? _IS_ACTIVE;
        public bool? IS_ACTIVE
        {
            get
            {
                return selectedManageStock.IS_ACTIVE;
            }
            set
            {
                selectedManageStock.IS_ACTIVE = value;

                if (selectedManageStock.IS_ACTIVE != value)
                {
                    selectedManageStock.IS_ACTIVE = value;
                    OnPropertyChanged("IS_ACTIVE");
                }
            }
        }
        private string _STOCK_CORRECTION;
        public string STOCK_CORRECTION
        {
            get
            {
                return selectedManageStock.STOCK_CORRECTION;
            }
            set
            {
                selectedManageStock.STOCK_CORRECTION = value;

                if (selectedManageStock.STOCK_CORRECTION != value)
                {
                    selectedManageStock.STOCK_CORRECTION = value;
                    OnPropertyChanged("STOCK_CORRECTION");
                }
            }
        }
        private string _DESCRIPTION;
        public string DESCRIPTION
        {
            get
            {
                return selectedManageStock.DESCRIPTION;
            }
            set
            {
                selectedManageStock.DESCRIPTION = value;

                if (selectedManageStock.DESCRIPTION != value)
                {
                    selectedManageStock.DESCRIPTION = value;
                    OnPropertyChanged("DESCRIPTION");
                }
            }
        }
        public async Task<ObservableCollection<ItemModel>> GetItem(int comp)
        {
            try
            {

                ItemModel[] dataItem = null;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    dataItem = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < dataItem.Length; i++)
                    {
                        x++;

                        _ListGrid_Item.Add(new ItemModel
                        {
                            SLNO=x,
                            // ITEM_ID = ItemId,
                            ITEM_NAME = dataItem[i].ITEM_NAME,
                            ITEM_ID = dataItem[i].ITEM_ID,
                            BARCODE = dataItem[i].BARCODE,
                            ACCESSORIES_KEYWORD = dataItem[i].ACCESSORIES_KEYWORD,
                            CATAGORY_ID = dataItem[i].CATAGORY_ID,
                            ITEM_DESCRIPTION = dataItem[i].ITEM_DESCRIPTION,
                            ITEM_INVOICE_ID = dataItem[i].ITEM_INVOICE_ID,
                            ITEM_PRICE = dataItem[i].ITEM_PRICE,
                            ITEM_PRODUCT_ID = dataItem[i].ITEM_PRODUCT_ID,
                            KEYWORD = dataItem[i].KEYWORD,
                            MRP = dataItem[i].MRP,
                            PURCHASE_UNIT = dataItem[i].PURCHASE_UNIT,
                            PURCHASE_UNIT_PRICE = dataItem[i].PURCHASE_UNIT_PRICE,
                            SALES_PRICE = dataItem[i].SALES_PRICE,
                            SALES_UNIT = dataItem[i].SALES_UNIT,
                            SEARCH_CODE = dataItem[i].SEARCH_CODE,
                            TAX_COLLECTED = dataItem[i].TAX_COLLECTED,
                            TAX_PAID = dataItem[i].TAX_PAID,
                            ALLOW_PURCHASE_ON_ESHOP = dataItem[i].ALLOW_PURCHASE_ON_ESHOP,
                            CATEGORY_NAME = dataItem[i].CATEGORY_NAME,
                            DISPLAY_INDEX = dataItem[i].DISPLAY_INDEX,
                            INCLUDE_TAX = dataItem[i].INCLUDE_TAX,
                            ITEM_GROUP_NAME = dataItem[i].ITEM_GROUP_NAME,
                            ITEM_UNIQUE_NAME = dataItem[i].ITEM_UNIQUE_NAME,
                            OPN_QNT = dataItem[i].OPN_QNT,
                            REGIONAL_LANGUAGE = dataItem[i].REGIONAL_LANGUAGE,
                            SALES_PRICE_BEFOR_TAX = dataItem[i].SALES_PRICE_BEFOR_TAX,
                            MODIFICATION_DATE = dataItem[i].MODIFICATION_DATE,


                        });

                    }

                }
                ItemListGrid = _ListGrid_Item;
                // var dataw = await _ListGrid_Temp.ToList();//.ToListAsync();
                // return new ObservableCollection<ItemModel>(dataw);
                return new ObservableCollection<ItemModel>(_ListGrid_Item);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        ObservableCollection<ItemModel> _ListGrid_Item = new ObservableCollection<ItemModel>();

        public ObservableCollection<ItemModel> _ItemListGrid { get; set; }
        public ObservableCollection<ItemModel> ItemListGrid
        {
            get
            {
                return _ItemListGrid;
            }
            set
            {
                this._ItemListGrid = value;
                OnPropertyChanged("ItemListGrid");
            }
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

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel file(*.xlsm;*.xlsx;*.xlsx;*.xlt; *.xlk;)|*.xlsm;*.xlsx;*.xlsx;*.xlt; *xlk";
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
                ObservableCollection<GodownModel> _ListGridTemp = new ObservableCollection<GodownModel>();
                if (objdt.Rows.Count > 0)
                {
                    for (int i = 0; i < objdt.Rows.Count; i++)
                    {
                        var df = objdt.Rows[0];

                        _ListGridTemp.Add(new GodownModel
                        {
                            SLNO = (objdt.Rows[i].ItemArray[0] != null) ? Convert.ToInt32(objdt.Rows[i].ItemArray[0]) : 0,
                            BUSINESS_NAME = (objdt.Rows[i].ItemArray[1] != null) ? Convert.ToString(objdt.Rows[i].ItemArray[1]) : "",
                            GODOWN_NAME = (objdt.Rows[i].ItemArray[1] != null) ? Convert.ToString(objdt.Rows[i].ItemArray[2]) : "",
                            GODOWN_ID = (objdt.Rows[i].ItemArray[2] != null) ? Convert.ToInt32(objdt.Rows[i].ItemArray[3]) : 0,
                            //IS_ACTIVE = (objdt.Rows[i].ItemArray[3]!=null)?Convert.ToBoolean(objdt.Rows[i].ItemArray[4]):false,
                            IS_DEFAULT_GODOWN = (objdt.Rows[i].ItemArray[4] != null) ? Convert.ToBoolean(objdt.Rows[i].ItemArray[5]) : false,
                            STOCK_CORRECTION = (objdt.Rows[i].ItemArray[4] != null) ? Convert.ToString(objdt.Rows[i].ItemArray[6]) : "",
                            GOSOWN_DESCRIPTION = (objdt.Rows[i].ItemArray[5] != null) ? Convert.ToString(objdt.Rows[i].ItemArray[7]) : ""
                        });
                    }


                }
                ListGrid = _ListGridTemp;
                App.Current.Properties["ExcelData"] = SelectedItem;

            }

        }


        public bool Add()
        {
            return true;
        }
        public bool Select()
        {
            return true;
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
