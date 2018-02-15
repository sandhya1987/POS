using InvoicePOS.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InvoicePOS.Models;
using System.Windows.Input;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using InvoicePOS.UserControll.StockTransfer;
using InvoicePOS.UserControll.GoDown;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.Item;
using InvoicePOS.UserControll.Supplier;
using System.Collections;
using System.Text.RegularExpressions;

namespace InvoicePOS.ViewModels
{
    public class StockTransferViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {


        public HttpResponseMessage response;
        // public HttpClient client = new HttpClient();
        private readonly StockTransferModel OprModel;
        StockTransferModel _opr = new StockTransferModel();
        public ICommand select { get; set; }
        StockTransferModel[] data = null;
        public ItemModel items = new ItemModel();
        ObservableCollection<StockTransferModel> _ListGrid_Temp = new ObservableCollection<StockTransferModel>();
        ObservableCollection<StockTransferModel> _ListGrid_Temp1 = new ObservableCollection<StockTransferModel>();
        ObservableCollection<ItemModel> _ListGrid_Item = new ObservableCollection<ItemModel>();
        ObservableCollection<ItemModel> _ListGrid_Item1 = new ObservableCollection<ItemModel>();
        public StockTransferViewModel()
        {
            var COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedStockTransfer = App.Current.Properties["StockTransferEdit"] as StockTransferModel;
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"] == "Qnt")
            {
                SelectedStockTransfer = App.Current.Properties["StockTransferQnt"] as StockTransferModel;
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                SelectedStockTransfer = App.Current.Properties["StockTransferView"] as StockTransferModel;
                App.Current.Properties["Action"] = "";
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                OprModel = _opr;
                SelctTransferItem = new ItemListModel();
                //DATE =System.DateTime.Now;
                string barcode = "wrqr";
                GetStockTransfer(barcode);
                GetItem(COMPANY_ID);
                SelectedStockTransfer = new StockTransferModel { };
            }
            GetStockTransferNo();
        }
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

            //var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            //GridPOVisible = "Collapsed";
            //GridItemVisible = "Visible";
            //GetItemList(comp);

            GetStockbybarcode();
            //_Select_BarCode = "";
            //Select_BarCode = "";
            //ReceiveAddItem.ItemReff.Text = "";
            //ReceiveAddItem.SearchItemReff.Text = "";

            //}
        }

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



        private ICommand _TransferRequestClick { get; set; }
        public ICommand TransferRequestClick
        {
            get
            {
                if (_TransferRequestClick == null)
                {
                    _TransferRequestClick = new DelegateCommand(TransferRequest_Click);
                }
                return _TransferRequestClick;
            }
        }
        public void TransferRequest_Click()
        {
            // ModalService.NavigateTo(new Items(), delegate(bool returnValue) { });
        }

        private ICommand _AddStockTransferClick { get; set; }
        public ICommand AddStockTransferClick
        {
            get
            {
                if (_AddStockTransferClick == null)
                {
                    _AddStockTransferClick = new DelegateCommand(AddStockTransfer_Click);
                }
                return _AddStockTransferClick;
            }
        }
        public void AddStockTransfer_Click()
        {
            AddStockTransfer _stok = new AddStockTransfer();
            _stok.Show();
            // ModalService.NavigateTo(new AddStockTransfer(), delegate(bool returnValue) { });
        }







        private StockTransferModel _SelectedStockTransfer;
        public StockTransferModel SelectedStockTransfer
        {

            get { return _SelectedStockTransfer; }
            set
            {

                if (_SelectedStockTransfer != value)
                {
                    _SelectedStockTransfer = value;
                    OnPropertyChanged("SelectedStockTransfer");
                }

            }

        }


        private ItemListModel _SelctTransferItem;
        public ItemListModel SelctTransferItem
        {

            get { return _SelctTransferItem; }
            set
            {

                if (_SelctTransferItem != value)
                {
                    _SelctTransferItem = value;
                    OnPropertyChanged("SelctTransferItem");
                }

            }

        }




        private List<StockTransferModel> _StockTransferData;
        public List<StockTransferModel> StockTransferData
        {
            get { return _StockTransferData; }
            set
            {
                if (_StockTransferData != value)
                {
                    _StockTransferData = value;
                }
            }
        }
        private long _BUSINESS_LOCATION_ID;
        public long BUSINESS_LOCATION_ID
        {
            get
            {
                return SelectedStockTransfer.BUSINESS_LOCATION_ID;
            }
            set
            {
                SelectedStockTransfer.BUSINESS_LOCATION_ID = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.BUSINESS_LOCATION_ID != value)
                {
                    SelectedStockTransfer.BUSINESS_LOCATION_ID = value;
                    OnPropertyChanged("BUSINESS_LOCATION_ID");
                }
            }
        }
        private string _BUSINESS_LOCATION;
        public string BUSINESS_LOCATION
        {
            get
            {
                return SelectedStockTransfer.BUSINESS_LOCATION;
            }
            set
            {
                SelectedStockTransfer.BUSINESS_LOCATION = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.BUSINESS_LOCATION != value)
                {
                    SelectedStockTransfer.BUSINESS_LOCATION = value;
                    OnPropertyChanged("BUSINESS_LOCATION");
                }
            }
        }
        public async Task<string> GetStockTransferNo()
        {

            string uhy = "";
            try
            {
                string nnnn = "";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/Stocktransfer/GetStockTransferNo/").Result;
                if (response.IsSuccessStatusCode)
                {
                    //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                    uhy = await response.Content.ReadAsStringAsync();
                    string dd = Convert.ToString(uhy);
                    string aaa = dd.Substring(3, 5);
                    int xx = Convert.ToInt32(aaa);
                    int noo = Convert.ToInt32(xx + 1);
                    nnnn = "P" + noo.ToString("D5");
                    STOCK_TRANSFER_NUMBER = nnnn;

                }
                else
                {
                    STOCK_TRANSFER_NUMBER = "ST00001";
                }
            }
            catch (Exception ex)
            {
                STOCK_TRANSFER_NUMBER = "ST00001";
            }

            return uhy;
        }
        private string _STOCK_TRANSFER_NUMBER;
        public string STOCK_TRANSFER_NUMBER
        {
            get
            {
                return SelectedStockTransfer.STOCK_TRANSFER_NUMBER;
            }
            set
            {
                SelectedStockTransfer.STOCK_TRANSFER_NUMBER = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedStockTransfer.STOCK_TRANSFER_NUMBER != value)
                {
                    SelectedStockTransfer.STOCK_TRANSFER_NUMBER = value;
                    OnPropertyChanged("STOCK_TRANSFER_NUMBER");
                }
            }
        }

        private int _TRANS_QUANTITY;
        public int TRANS_QUANTITY
        {
            get
            {
                return SelectedStockTransfer.TRANS_QUANTITY;
            }
            set
            {
                SelectedStockTransfer.TRANS_QUANTITY = value;
                if (SelectedStockTransfer.TRANS_QUANTITY != value)
                {
                    SelectedStockTransfer.TRANS_QUANTITY = value;
                    OnPropertyChanged("TRANS_QUANTITY");
                }
            }
        }
        private DateTime _DATE;
        public DateTime DATE
        {
            get
            {
                return SelectedStockTransfer.DATE;
            }
            set
            {
                SelectedStockTransfer.DATE = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.DATE != value)
                {
                    SelectedStockTransfer.DATE = System.DateTime.Now;
                    OnPropertyChanged("DATE");
                }
            }
        }
        private string _BARCODE;
        public string BARCODE
        {
            get
            {
                return SelectedStockTransfer.BARCODE;
            }
            set
            {
                SelectedStockTransfer.BARCODE = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedStockTransfer.BARCODE != value)
                {
                    SelectedStockTransfer.BARCODE = value;
                    OnPropertyChanged("BARCODE");
                }
            }
        }
        private string _ITEM_NAME;
        public string ITEM_NAME
        {
            get
            {
                return SelectedStockTransfer.ITEM_NAME;
            }
            set
            {
                SelectedStockTransfer.ITEM_NAME = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedStockTransfer.ITEM_NAME != value)
                {
                    SelectedStockTransfer.ITEM_NAME = value;
                    OnPropertyChanged("ITEM_NAME");
                }
            }
        }
        private int _tnsquantity;
        public int tnsquantity
        {
            get
            {
                return SelectedStockTransfer.tnsquantity;
            }
            set
            {
                SelectedStockTransfer.tnsquantity = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.tnsquantity != value)
                {
                    SelectedStockTransfer.tnsquantity = value;
                    OnPropertyChanged("tnsquantity");
                }
            }
        }
        public string _ItemsChange { get; set; }
        public string ItemsChange
        {
            get
            {
                return SelectedStockTransfer.ITEM_NAME;
            }
            set
            {
                SelectedStockTransfer.ITEM_NAME = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedStockTransfer.ITEM_NAME != value)
                {
                    SelectedStockTransfer.ITEM_NAME = value;
                    OnPropertyChanged("ItemsChange");
                }
            }
        }
        private string _SEARCH_CODE;
        public string SEARCH_CODE
        {
            get
            {
                return SelectedStockTransfer.SEARCH_CODE;
            }
            set
            {
                SelectedStockTransfer.SEARCH_CODE = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedStockTransfer.SEARCH_CODE != value)
                {
                    SelectedStockTransfer.SEARCH_CODE = value;
                    OnPropertyChanged("SEARCH_CODE");
                }
            }
        }
        private long _FROM_GODOWN_ID;
        public long FROM_GODOWN_ID
        {
            get
            {
                return SelectedStockTransfer.FROM_GODOWN_ID;
            }
            set
            {
                SelectedStockTransfer.FROM_GODOWN_ID = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.FROM_GODOWN_ID != value)
                {
                    SelectedStockTransfer.FROM_GODOWN_ID = value;
                    OnPropertyChanged("FROM_GODOWN_ID");
                }
            }
        }
        private string _FROM_GODOWN;
        public string FROM_GODOWN
        {
            get
            {
                return SelectedStockTransfer.FROM_GODOWN;
            }
            set
            {
                SelectedStockTransfer.FROM_GODOWN = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.FROM_GODOWN != value)
                {
                    SelectedStockTransfer.FROM_GODOWN = value;
                    OnPropertyChanged("FROM_GODOWN");
                }
            }
        }
        private long _TO_GODOWN_ID;
        public long TO_GODOWN_ID
        {
            get
            {
                return SelectedStockTransfer.TO_GODOWN_ID;
            }
            set
            {
                SelectedStockTransfer.TO_GODOWN_ID = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.TO_GODOWN_ID != value)
                {
                    SelectedStockTransfer.TO_GODOWN_ID = value;
                    OnPropertyChanged("TO_GODOWN_ID");
                }
            }
        }
        private string _TO_GODOWN;
        public string TO_GODOWN
        {
            get
            {
                return SelectedStockTransfer.TO_GODOWN;
            }
            set
            {
                SelectedStockTransfer.TO_GODOWN = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.TO_GODOWN != value)
                {
                    SelectedStockTransfer.TO_GODOWN = value;
                    OnPropertyChanged("TO_GODOWN");
                }
            }
        }
        private long _RECEIVED_BY_ID;
        public long RECEIVED_BY_ID
        {
            get
            {
                return SelectedStockTransfer.RECEIVED_BY_ID;
            }
            set
            {
                SelectedStockTransfer.RECEIVED_BY_ID = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.RECEIVED_BY_ID != value)
                {
                    SelectedStockTransfer.RECEIVED_BY_ID = value;
                    OnPropertyChanged("RECEIVED_BY_ID");
                }
            }
        }
        private string _RECEIVED_BY;
        public string RECEIVED_BY
        {
            get
            {
                return SelectedStockTransfer.RECEIVED_BY;
            }
            set
            {
                SelectedStockTransfer.RECEIVED_BY = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.RECEIVED_BY != value)
                {
                    SelectedStockTransfer.RECEIVED_BY = value;
                    OnPropertyChanged("RECEIVED_BY");
                }
            }
        }
        private string _EMAIL;
        public string EMAIL
        {
            get
            {
                return SelectedStockTransfer.EMAIL;
            }
            set
            {
                SelectedStockTransfer.EMAIL = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedStockTransfer.EMAIL != value)
                {
                    SelectedStockTransfer.EMAIL = value;
                    OnPropertyChanged("EMAIL");
                }
            }
        }
        private long _STOCK_TRANSFER_ID;
        public long STOCK_TRANSFER_ID
        {
            get
            {
                return SelectedStockTransfer.STOCK_TRANSFER_ID;
            }
            set
            {
                SelectedStockTransfer.STOCK_TRANSFER_ID = value;

                if (SelectedStockTransfer.STOCK_TRANSFER_ID != value)
                {
                    SelectedStockTransfer.STOCK_TRANSFER_ID = value;
                    OnPropertyChanged("STOCK_TRANSFER_ID");
                }
            }
        }
        private int _TOTAL_STOCK_QNT;
        public int TOTAL_STOCK_QNT
        {
            get
            {
                return SelectedStockTransfer.TOTAL_STOCK_QNT;
            }
            set
            {
                SelectedStockTransfer.TOTAL_STOCK_QNT = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.TOTAL_STOCK_QNT != value)
                {
                    SelectedStockTransfer.TOTAL_STOCK_QNT = value;
                    OnPropertyChanged("TOTAL_STOCK_QNT");
                }
            }
        }
        private int _CHNG_QNT;
        public int CHNG_QNT
        {
            get
            {
                return SelectedStockTransfer.CHNG_QNT;
            }
            set
            {
                SelectedStockTransfer.CHNG_QNT = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.CHNG_QNT != value)
                {
                    SelectedStockTransfer.CHNG_QNT = value;
                    OnPropertyChanged("CHNG_QNT");
                }
            }
        }
        private bool _IS_SEND;
        public bool IS_SEND
        {
            get
            {
                return SelectedStockTransfer.IS_SEND;
            }
            set
            {
                SelectedStockTransfer.IS_SEND = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedStockTransfer.IS_SEND != value)
                {
                    SelectedStockTransfer.IS_SEND = value;
                    OnPropertyChanged("IS_SEND");
                }
            }
        }

        private ItemModel _selectedItem { get; set; }
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





        public ICommand _EditStockTra { get; set; }
        public ICommand EditStockTra
        {
            get
            {
                if (_EditStockTra == null)
                {
                    _EditStockTra = new DelegateCommand(Edit_StockTra);
                }
                return _EditStockTra;
            }
        }


        public async void Edit_StockTra()
        {
            if (SelectedStockTransfer.STOCK_TRANSFER_ID != null && SelectedStockTransfer.STOCK_TRANSFER_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                //ItemData = new List<SuppPaymentModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/StocktransferAPI/EditStocktransfer?id=" + SelectedStockTransfer.STOCK_TRANSFER_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<StockTransferModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedStockTransfer.BARCODE = data[i].BARCODE;
                            SelectedStockTransfer.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedStockTransfer.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedStockTransfer.DATE = data[i].DATE;
                            SelectedStockTransfer.EMAIL = data[i].EMAIL;
                            SelectedStockTransfer.FROM_GODOWN = data[i].FROM_GODOWN;
                            SelectedStockTransfer.FROM_GODOWN_ID = data[i].FROM_GODOWN_ID;
                            SelectedStockTransfer.IS_SEND = data[i].IS_SEND;
                            SelectedStockTransfer.ITEM_NAME = data[i].ITEM_NAME;
                            SelectedStockTransfer.RECEIVED_BY = data[i].RECEIVED_BY;
                            SelectedStockTransfer.RECEIVED_BY_ID = data[i].RECEIVED_BY_ID;
                            SelectedStockTransfer.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedStockTransfer.STOCK_TRANSFER_ID = data[i].STOCK_TRANSFER_ID;
                            SelectedStockTransfer.STOCK_TRANSFER_NUMBER = data[i].STOCK_TRANSFER_NUMBER;
                            SelectedStockTransfer.TO_GODOWN = data[i].TO_GODOWN;
                            SelectedStockTransfer.TO_GODOWN_ID = data[i].TO_GODOWN_ID;
                            SelectedStockTransfer.TOTAL_STOCK_QNT = data[i].TOTAL_STOCK_QNT;
                            SelectedStockTransfer.IS_NEGATIVE_STOCK_MESSAGE = data[i].IS_NEGATIVE_STOCK_MESSAGE;
                        }
                        App.Current.Properties["StockTransferEdit"] = SelectedStockTransfer;
                        AddStockTransfer _AddStockTransfer = new AddStockTransfer();
                        _AddStockTransfer.Show();
                        //ModalService.NavigateTo(new AddStockTransfer(), delegate(bool returnValue) { });
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Stock transfer");
            }
        }


        public ICommand _ViewStockTra { get; set; }
        public ICommand ViewStockTra
        {
            get
            {
                if (_ViewStockTra == null)
                {
                    _ViewStockTra = new DelegateCommand(View_StockTra);
                }
                return _ViewStockTra;
            }
        }


        public async void View_StockTra()
        {
            if (SelectedStockTransfer.STOCK_TRANSFER_ID != null && SelectedStockTransfer.STOCK_TRANSFER_ID != 0)
            {
                App.Current.Properties["Action"] = "View";
                //ItemData = new List<SuppPaymentModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/StocktransferAPI/EditStocktransfer?id=" + SelectedStockTransfer.STOCK_TRANSFER_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<StockTransferModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedStockTransfer.BARCODE = data[i].BARCODE;
                            SelectedStockTransfer.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedStockTransfer.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedStockTransfer.DATE = data[i].DATE;
                            SelectedStockTransfer.EMAIL = data[i].EMAIL;
                            SelectedStockTransfer.FROM_GODOWN = data[i].FROM_GODOWN;
                            SelectedStockTransfer.FROM_GODOWN_ID = data[i].FROM_GODOWN_ID;
                            SelectedStockTransfer.IS_SEND = data[i].IS_SEND;
                            SelectedStockTransfer.ITEM_NAME = data[i].ITEM_NAME;
                            SelectedStockTransfer.RECEIVED_BY = data[i].RECEIVED_BY;
                            SelectedStockTransfer.RECEIVED_BY_ID = data[i].RECEIVED_BY_ID;
                            SelectedStockTransfer.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedStockTransfer.STOCK_TRANSFER_ID = data[i].STOCK_TRANSFER_ID;
                            SelectedStockTransfer.STOCK_TRANSFER_NUMBER = data[i].STOCK_TRANSFER_NUMBER;
                            SelectedStockTransfer.TO_GODOWN = data[i].TO_GODOWN;
                            SelectedStockTransfer.TO_GODOWN_ID = data[i].TO_GODOWN_ID;
                            SelectedStockTransfer.TOTAL_STOCK_QNT = data[i].TOTAL_STOCK_QNT;
                            SelectedStockTransfer.IS_NEGATIVE_STOCK_MESSAGE = data[i].IS_NEGATIVE_STOCK_MESSAGE;
                        }
                        App.Current.Properties["StockTransferView"] = SelectedStockTransfer;
                        StockTransferView _AddStockTransfer = new StockTransferView();
                        _AddStockTransfer.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Stock transfer");
            }
        }












        public async Task<ObservableCollection<ItemModel>> GetItem(int comp)
        {
            try
            {

                if (App.Current.Properties["DataGridTrn"] == null)
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
                            //_ListGrid_Item.Add(new ItemModel
                            //{
                            //    SLNO = x,
                            //    ITEM_NAME = dataItem[i].ITEM_NAME,
                            //    ITEM_ID = dataItem[i].ITEM_ID,
                            //    BARCODE = dataItem[i].BARCODE,
                            //    ACCESSORIES_KEYWORD = dataItem[i].ACCESSORIES_KEYWORD,
                            //    CATAGORY_ID = dataItem[i].CATAGORY_ID,
                            //    ITEM_DESCRIPTION = dataItem[i].ITEM_DESCRIPTION,
                            //    ITEM_INVOICE_ID = dataItem[i].ITEM_INVOICE_ID,
                            //    ITEM_PRICE = dataItem[i].ITEM_PRICE,
                            //    ITEM_PRODUCT_ID = dataItem[i].ITEM_PRODUCT_ID,
                            //    KEYWORD = dataItem[i].KEYWORD,
                            //    MRP = dataItem[i].MRP,
                            //    PURCHASE_UNIT = dataItem[i].PURCHASE_UNIT,
                            //    PURCHASE_UNIT_PRICE = dataItem[i].PURCHASE_UNIT_PRICE,
                            //    SALES_PRICE = dataItem[i].SALES_PRICE,
                            //    SALES_UNIT = dataItem[i].SALES_UNIT,
                            //    SEARCH_CODE = dataItem[i].SEARCH_CODE,
                            //    TAX_COLLECTED = dataItem[i].TAX_COLLECTED,
                            //    TAX_PAID = dataItem[i].TAX_PAID,
                            //    ALLOW_PURCHASE_ON_ESHOP = dataItem[i].ALLOW_PURCHASE_ON_ESHOP,
                            //    CATEGORY_NAME = dataItem[i].CATEGORY_NAME,
                            //    DISPLAY_INDEX = dataItem[i].DISPLAY_INDEX,
                            //    INCLUDE_TAX = dataItem[i].INCLUDE_TAX,
                            //    ITEM_GROUP_NAME = dataItem[i].ITEM_GROUP_NAME,
                            //    ITEM_UNIQUE_NAME = dataItem[i].ITEM_UNIQUE_NAME,
                            //    OPN_QNT = dataItem[i].OPN_QNT,
                            //    WeightQnt = Convert.ToInt32(dataItem[i].OPN_QNT),
                            //    REGIONAL_LANGUAGE = dataItem[i].REGIONAL_LANGUAGE,
                            //    SALES_PRICE_BEFOR_TAX = dataItem[i].SALES_PRICE_BEFOR_TAX,
                            //    MODIFICATION_DATE = dataItem[i].MODIFICATION_DATE,


                            //});

                        }

                    }
                }
                else
                {
                    _ListGrid_Item = App.Current.Properties["DataGridTrn"] as ObservableCollection<ItemModel>;

                }
                App.Current.Properties["DataGridTrn"] = _ListGrid_Item;
                //ItemListGrid = _ListGrid_Item;

                return new ObservableCollection<ItemModel>(_ListGrid_Item);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ObservableCollection<StockTransferModel>> GetStock()
        {
            try
            {


                int fromgodownid = Convert.ToInt32(App.Current.Properties["AddStockOpStockRefFrom"].ToString());
                ItemModel[] dataItem = null;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem1?id=" + fromgodownid + "&comp=" + 1 + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    dataItem = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < dataItem.Length; i++)
                    {

                        x++;
                        _ListGrid_Temp1.Add(new StockTransferModel
                        {
                            SLNO = x,
                            ITEM_NAME = dataItem[i].ITEM_NAME,
                            ITEM_ID = dataItem[i].ITEM_ID,
                            BARCODE = dataItem[i].BARCODE,
                            //ACCESSORIES_KEYWORD = dataItem[i].ACCESSORIES_KEYWORD,
                            //CATAGORY_ID = dataItem[i].CATAGORY_ID,
                            //ITEM_DESCRIPTION = dataItem[i].ITEM_DESCRIPTION,
                            //ITEM_INVOICE_ID = dataItem[i].ITEM_INVOICE_ID,
                            //ITEM_PRICE = dataItem[i].ITEM_PRICE,
                            //ITEM_PRODUCT_ID = dataItem[i].ITEM_PRODUCT_ID,
                            //KEYWORD = dataItem[i].KEYWORD,
                            //MRP = dataItem[i].MRP,
                            PURCHASE_UNIT = dataItem[i].PURCHASE_UNIT,
                            //PURCHASE_UNIT_PRICE = dataItem[i].PURCHASE_UNIT_PRICE,
                            //SALES_PRICE = dataItem[i].SALES_PRICE,
                            //SALES_UNIT = dataItem[i].SALES_UNIT,
                            SEARCH_CODE = dataItem[i].SEARCH_CODE,
                            TOTAL_STOCK_QNT = Convert.ToInt32(dataItem[i].OPN_QNT),
                            //TAX_COLLECTED = dataItem[i].TAX_COLLECTED,
                            //TAX_PAID = dataItem[i].TAX_PAID,
                            //ALLOW_PURCHASE_ON_ESHOP = dataItem[i].ALLOW_PURCHASE_ON_ESHOP,
                            //CATEGORY_NAME = dataItem[i].CATEGORY_NAME,
                            //DISPLAY_INDEX = dataItem[i].DISPLAY_INDEX,
                            //INCLUDE_TAX = dataItem[i].INCLUDE_TAX,
                            //ITEM_GROUP_NAME = dataItem[i].ITEM_GROUP_NAME,
                            //ITEM_UNIQUE_NAME = dataItem[i].ITEM_UNIQUE_NAME,
                            OPN_QNT = dataItem[i].OPN_QNT,
                            WeightQnt = Convert.ToInt32(dataItem[i].OPN_QNT),
                            tnsquantity = Convert.ToInt32(dataItem[i].OPN_QNT),
                            //REGIONAL_LANGUAGE = dataItem[i].REGIONAL_LANGUAGE,
                            //SALES_PRICE_BEFOR_TAX = dataItem[i].SALES_PRICE_BEFOR_TAX,
                            //MODIFICATION_DATE = dataItem[i].MODIFICATION_DATE,


                        });

                    }

                }
                App.Current.Properties["GETStock"] = _ListGrid_Temp1;
                //App.Current.Properties["DataGridTrn"] = _ListGrid_Item;
                ItemListGrid1 = _ListGrid_Temp1;
                SelectedStockTransfer.getAllStockTransfer = _ListGrid_Temp1;
                AddStockTransfer.ListGridRef.ItemsSource = _ListGrid_Temp1;

                return new ObservableCollection<StockTransferModel>(_ListGrid_Temp1);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ObservableCollection<StockTransferModel>> GetStockbybarcode()
        {
            try
            {

                string getbarcode = "";
                int fromgodownid = Convert.ToInt32(App.Current.Properties["AddStockOpStockRefFrom"].ToString());
                try
                {
                    if (App.Current.Properties["ItemSearchbarcode"] != null)
                    {
                        getbarcode = App.Current.Properties["ItemSearchbarcode"].ToString();
                    }
                }
                catch
                { }
                if (AddStockTransfer.getbarcode.Text != "")
                {
                    getbarcode = AddStockTransfer.getbarcode.Text;
                }
                if (App.Current.Properties["StockSearchCode"] != null)
                {
                    getbarcode = App.Current.Properties["StockSearchCodebar"].ToString();
                }
                ItemModel[] dataItem = null;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem1bybarcode?id=" + fromgodownid + "&comp=" + 1 + "&barcode=" + getbarcode + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    dataItem = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < dataItem.Length; i++)
                    {

                        x++;
                        _ListGrid_Temp1.Add(new StockTransferModel
                        {
                            SLNO = x,
                            ITEM_NAME = dataItem[i].ITEM_NAME,
                            ITEM_ID = dataItem[i].ITEM_ID,
                            BARCODE = dataItem[i].BARCODE,
                            //ACCESSORIES_KEYWORD = dataItem[i].ACCESSORIES_KEYWORD,
                            //CATAGORY_ID = dataItem[i].CATAGORY_ID,
                            //ITEM_DESCRIPTION = dataItem[i].ITEM_DESCRIPTION,
                            //ITEM_INVOICE_ID = dataItem[i].ITEM_INVOICE_ID,
                            //ITEM_PRICE = dataItem[i].ITEM_PRICE,
                            //ITEM_PRODUCT_ID = dataItem[i].ITEM_PRODUCT_ID,
                            //KEYWORD = dataItem[i].KEYWORD,
                            //MRP = dataItem[i].MRP,
                            PURCHASE_UNIT = dataItem[i].PURCHASE_UNIT,
                            //PURCHASE_UNIT_PRICE = dataItem[i].PURCHASE_UNIT_PRICE,
                            //SALES_PRICE = dataItem[i].SALES_PRICE,
                            //SALES_UNIT = dataItem[i].SALES_UNIT,
                            SEARCH_CODE = dataItem[i].SEARCH_CODE,
                            //TAX_COLLECTED = dataItem[i].TAX_COLLECTED,
                            //TAX_PAID = dataItem[i].TAX_PAID,
                            //ALLOW_PURCHASE_ON_ESHOP = dataItem[i].ALLOW_PURCHASE_ON_ESHOP,
                            //CATEGORY_NAME = dataItem[i].CATEGORY_NAME,
                            //DISPLAY_INDEX = dataItem[i].DISPLAY_INDEX,
                            //INCLUDE_TAX = dataItem[i].INCLUDE_TAX,
                            //ITEM_GROUP_NAME = dataItem[i].ITEM_GROUP_NAME,
                            //ITEM_UNIQUE_NAME = dataItem[i].ITEM_UNIQUE_NAME,
                            OPN_QNT = dataItem[i].OPN_QNT,
                            WeightQnt = Convert.ToInt32(dataItem[i].OPN_QNT),
                            tnsquantity = Convert.ToInt32(dataItem[i].OPN_QNT),
                            //REGIONAL_LANGUAGE = dataItem[i].REGIONAL_LANGUAGE,
                            //SALES_PRICE_BEFOR_TAX = dataItem[i].SALES_PRICE_BEFOR_TAX,
                            //MODIFICATION_DATE = dataItem[i].MODIFICATION_DATE,


                        });

                    }

                }
                App.Current.Properties["GETStock"] = _ListGrid_Temp1;
                //App.Current.Properties["DataGridTrn"] = _ListGrid_Item;
                ItemListGrid1 = _ListGrid_Temp1;
                SelectedStockTransfer.getAllStockTransfer = _ListGrid_Temp1;
                AddStockTransfer.ListGridRef.ItemsSource = _ListGrid_Temp1;

                return new ObservableCollection<StockTransferModel>(_ListGrid_Temp1);
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public ICommand _TransRemoveItem { get; set; }
        public ICommand TransRemoveItem
        {
            get
            {
                if (_TransRemoveItem == null)
                {
                    _TransRemoveItem = new DelegateCommand(TransRemove_Item);
                }
                return _TransRemoveItem;
            }
        }


        public async void TransRemove_Item()
        {
            if (SelectedItem.ITEM_ID != null && SelectedItem.ITEM_ID != 0)
            {

                ObservableCollection<ItemModel> ListGrid = new ObservableCollection<ItemModel>();
                ListGrid = App.Current.Properties["DataGridTrn"] as ObservableCollection<ItemModel>;


                var itemToRemove = ListGrid.Single(r => r.ITEM_ID == SelectedItem.ITEM_ID);
                ListGrid.Remove(itemToRemove);
                AddStockTransfer.ListGridRef.ItemsSource = ListGrid;
            }
            else
            {
                MessageBox.Show("Select Stock Item");
            }

        }

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

        public ObservableCollection<StockTransferModel> _ItemListGrid1 { get; set; }
        public ObservableCollection<StockTransferModel> ItemListGrid1
        {
            get
            {
                return _ItemListGrid1;
            }
            set
            {
                this._ItemListGrid1 = value;
                OnPropertyChanged("ItemListGrid1");
            }
        }









        public ICommand _ChngTranferQunt { get; set; }
        public ICommand ChngTranferQunt
        {
            get
            {
                if (_ChngTranferQunt == null)
                {
                    _ChngTranferQunt = new DelegateCommand(Chng_TranferQunt);
                }
                return _ChngTranferQunt;
            }
        }


        public async void Chng_TranferQunt()
        {
            if (SelectedItem.ITEM_ID != null && SelectedItem.ITEM_ID != 0)
            {
                App.Current.Properties["Action"] = "Qnt";
                //ItemData = new List<SuppPaymentModel>();
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                //client.DefaultRequestHeaders.Accept.Add(
                //    new MediaTypeWithQualityHeaderValue("application/json"));
                //client.Timeout = new TimeSpan(500000000000);
                //HttpResponseMessage response = client.GetAsync("api/StocktransferAPI/EditStocktransfer?id=" + SelectedStockTransfer.STOCK_TRANSFER_ID + "").Result;
                //if (response.IsSuccessStatusCode)
                //{
                //    data = JsonConvert.DeserializeObject<StockTransferModel[]>(await response.Content.ReadAsStringAsync());
                //    if (data.Length > 0)
                //    {
                //        for (int i = 0; i < data.Length; i++)
                //        {
                //            SelectedStockTransfer.BARCODE = data[i].BARCODE;
                //            SelectedStockTransfer.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                //            SelectedStockTransfer.COMPANY_ID = data[i].COMPANY_ID;
                //            SelectedStockTransfer.DATE = data[i].DATE;
                //            SelectedStockTransfer.EMAIL = data[i].EMAIL;
                //            SelectedStockTransfer.FROM_GODOWN = data[i].FROM_GODOWN;
                //            SelectedStockTransfer.FROM_GODOWN_ID = data[i].FROM_GODOWN_ID;
                //            SelectedStockTransfer.IS_SEND = data[i].IS_SEND;
                //            SelectedStockTransfer.ITEM_NAME = data[i].ITEM_NAME;
                //            SelectedStockTransfer.RECEIVED_BY = data[i].RECEIVED_BY;
                //            SelectedStockTransfer.RECEIVED_BY_ID = data[i].RECEIVED_BY_ID;
                //            SelectedStockTransfer.SEARCH_CODE = data[i].SEARCH_CODE;
                //            SelectedStockTransfer.STOCK_TRANSFER_ID = data[i].STOCK_TRANSFER_ID;
                //            SelectedStockTransfer.STOCK_TRANSFER_NUMBER = data[i].STOCK_TRANSFER_NUMBER;
                //            SelectedStockTransfer.TO_GODOWN = data[i].TO_GODOWN;
                //            SelectedStockTransfer.TO_GODOWN_ID = data[i].TO_GODOWN_ID;
                //            SelectedStockTransfer.TOTAL_STOCK_QNT = data[i].TOTAL_STOCK_QNT;
                //            SelectedStockTransfer.IS_NEGATIVE_STOCK_MESSAGE = data[i].IS_NEGATIVE_STOCK_MESSAGE;
                //        }

                //    }
                //}
                App.Current.Properties["ITEM_ID"] = SelectedItem.ITEM_ID;
                App.Current.Properties["StockTransferQnt"] = SelectedStockTransfer;
                ChngQntTransfer Qnt = new ChngQntTransfer();
                Qnt.Show();
            }
            else
            {
                MessageBox.Show("Select Stock Transfer");
            }
        }















        public ICommand _DeleteStockTranfer;
        public ICommand DeleteStockTranfer
        {
            get
            {
                if (_DeleteStockTranfer == null)
                {
                    _DeleteStockTranfer = new DelegateCommand(Delete_StockTranfer);
                }
                return _DeleteStockTranfer;
            }
        }
        public async void Delete_StockTranfer()
        {
            if (SelectedStockTransfer.STOCK_TRANSFER_ID != null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Transfer Name " + SelectedStockTransfer.STOCK_TRANSFER_NUMBER + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    var id = SelectedStockTransfer.STOCK_TRANSFER_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/StocktransferAPI/DeleteStocktransfer?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {

                        MessageBox.Show("Stocktransfer Delete Successfully");
                        ModalService.NavigateTo(new StockTransfer(), delegate (bool returnValue) { });
                    }
                }
                else
                {
                    Cancel_StockTr();
                }
            }
            else
            {
                MessageBox.Show("Select Stocktransfer");
            }

        }
        public ObservableCollection<StockTransferModel> _ListGrid { get; set; }
        public ObservableCollection<StockTransferModel> ListGrid
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

        private string _Select_BarCode;
        public string Select_BarCode
        {
            get { return _Select_BarCode; }
            set
            {
                _Select_BarCode = value;

                GetStockTransfer(_Select_BarCode);
                // call your required
                // method here
                this.NotifyPropertyChanged("Select_BarCode");
            }
        }

        public async Task<ObservableCollection<StockTransferModel>> GetStockTransfer(string id)
        {
            try
            {


                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                response = await client.GetAsync("api/StocktransferAPI/GetStocktransfer?Code=" + id + "");
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<StockTransferModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new StockTransferModel
                        {
                            SLNO = x,
                            BARCODE = data[i].BARCODE,
                            BUSINESS_LOCATION = data[i].BUSINESS_LOCATION,
                            COMPANY_ID = data[i].COMPANY_ID,
                            DATE = data[i].DATE,
                            EMAIL = data[i].EMAIL,
                            FROM_GODOWN = data[i].FROM_GODOWN,
                            FROM_GODOWN_ID = data[i].FROM_GODOWN_ID,
                            IS_SEND = data[i].IS_SEND,
                            ITEM_NAME = data[i].ITEM_NAME,
                            RECEIVED_BY = data[i].RECEIVED_BY,
                            RECEIVED_BY_ID = data[i].RECEIVED_BY_ID,
                            SEARCH_CODE = data[i].SEARCH_CODE,
                            STOCK_TRANSFER_ID = data[i].STOCK_TRANSFER_ID,
                            STOCK_TRANSFER_NUMBER = data[i].STOCK_TRANSFER_NUMBER,
                            TO_GODOWN = data[i].TO_GODOWN,
                            TO_GODOWN_ID = data[i].TO_GODOWN_ID,
                            TOTAL_STOCK_QNT = data[i].TOTAL_STOCK_QNT,
                        });
                    }

                }
                ListGrid = _ListGrid_Temp;
                // var dataw = await _ListGrid_Temp.ToList();//.ToListAsync();
                // return new ObservableCollection<ItemModel>(dataw);
                return new ObservableCollection<StockTransferModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_StockTr);
                }
                return _Cancel;
            }
        }



        public void Cancel_StockTr()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public ICommand _InsertStockTransfer { get; set; }
        public ICommand InsertStockTransfer
        {
            get
            {
                if (_InsertStockTransfer == null)
                {

                    _InsertStockTransfer = new DelegateCommand(Insert_StockTransfer);


                }
                return _InsertStockTransfer;
            }
        }

        public ICommand _getall { get; set; }
        public ICommand getall
        {
            get
            {
                if (_getall == null)
                {

                    _getall = new DelegateCommand(Getall_Click);


                }
                return _getall;
            }
        }
        public void Getall_Click()
        {
            //Supplier_Enable = false;
            //VisibleMyCode = "Collapsed";
            //VisibleAutoCode = "Visible";
            GetStock();




        }
        public async void Insert_StockTransfer()
        {

            //if (SelectedStockTransfer.BARCODE == "" || SelectedStockTransfer.BARCODE == null)
            //{
            //    MessageBox.Show("BARCODE is not blank");
            //}
            //else if (SelectedStockTransfer.FROM_GODOWN == "" || SelectedStockTransfer.FROM_GODOWN == null)
            //{
            //    MessageBox.Show("FROM GODOWN is not blank");
            //}
            //else if (SelectedStockTransfer.RECEIVED_BY == "" || SelectedStockTransfer.RECEIVED_BY == null)
            //{
            //    MessageBox.Show("RECEIVED BY is not blank");
            //}
            //else if (SelectedStockTransfer.TO_GODOWN == "" || SelectedStockTransfer.TO_GODOWN == null)
            //{
            //    MessageBox.Show("TO GODOWN is not blank");
            //}
            //else if (SelectedStockTransfer.TOTAL_STOCK_QNT == 0 || SelectedStockTransfer.TOTAL_STOCK_QNT == null)
            //{
            //    MessageBox.Show("TOTAL STOCK QUANTITY is not blank");
            //}

            //else if (SelectedStockTransfer.EMAIL == "" || SelectedStockTransfer.EMAIL == null)
            //{
            //    MessageBox.Show("EMAIL is not blank");
            //}
            //else if (!Regex.IsMatch(SelectedStockTransfer.EMAIL,
            //@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            //@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            //RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            //{
            //    MessageBox.Show("Please Check Email Format");
            //    //AddCustomer.ShippingemailAddressReff.Focus();
            //    return;
            //}

            //else if (App.Current.Properties["AddStockOpStockRefFrom"] != null)
            //{
            //    SelectedStockTransfer.FROM_GODOWN_ID = Convert.ToInt64(App.Current.Properties["AddStockOpStockRefFrom"]);

            //}

            //else if (App.Current.Properties["AddStockOpStockRefTo"] != null)
            //{
            //    SelectedStockTransfer.TO_GODOWN_ID = Convert.ToInt64(App.Current.Properties["AddStockOpStockRefTo"]);

            //}


            //else
            //{

            if (App.Current.Properties["AddStockOpStockRefFrom"] != null)
            {
                SelectedStockTransfer.FROM_GODOWN_ID = Convert.ToInt64(App.Current.Properties["AddStockOpStockRefFrom"]);

            }

            if (App.Current.Properties["AddStockOpStockRefTo"] != null)
            {
                SelectedStockTransfer.TO_GODOWN_ID = Convert.ToInt64(App.Current.Properties["AddStockOpStockRefTo"]);
            }
            //}
            //if (App.Current.Properties["BussStockName"] != null)
            //{
            //    SelectedStockTransfer.BUSINESS_LOCATION = App.Current.Properties["BussStockName"].ToString();

            //}
            //if (App.Current.Properties["StockItemName"] != null)
            //{
            //    SelectedStockTransfer.ITEM_NAME = App.Current.Properties["StockItemName"].ToString();

            //}
            //if (App.Current.Properties["StockSearchCode"] != null)
            //{
            //    SelectedStockTransfer.SEARCH_CODE = App.Current.Properties["StockSearchCode"].ToString();

            //}
            var get = items.getAllSTock;
            SelectedStockTransfer.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync("api/StocktransferAPI/CreateStocktransfer/", SelectedStockTransfer);
            if (SelectedStockTransfer.STOCK_TRANSFER_ID != null)
            {
                MessageBox.Show("Stocktransfer Insert Successfully");
                Cancel_StockTr();
                ModalService.NavigateTo(new StockTransfer(), delegate (bool returnValue) { });
            }
            //}
        }



        public ICommand _ChngStockTransfer { get; set; }
        public ICommand ChngStockTransfer
        {
            get
            {
                if (_ChngStockTransfer == null)
                {

                    _ChngStockTransfer = new DelegateCommand(Chng_StockTransfer);


                }
                return _ChngStockTransfer;
            }
        }
        public async void Chng_StockTransfer()
        {
            try
            {

                //SelectedStockTransfer.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                //client.DefaultRequestHeaders.Accept.Add(
                //    new MediaTypeWithQualityHeaderValue("application/json"));
                //var response = await client.PostAsJsonAsync("api/StocktransferAPI/ChngStocktransfer/", SelectedStockTransfer);
                if (SelectedStockTransfer.STOCK_TRANSFER_ID != null)
                {
                    get_list();
                    MessageBox.Show("Stocktransfer Qnt Update Successfully");
                    Cancel_StockTr();
                    ModalService.NavigateTo(new StockTransfer(), delegate (bool returnValue) { });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void get_list()
        {
            ObservableCollection<ItemModel> _ListGrid_Temp = new ObservableCollection<ItemModel>();
            var Load_Item = (((IEnumerable)App.Current.Properties["DataGridTrn"]).Cast<ItemModel>()).ToList();
            AddStockTransfer.ListGridRef.ItemsSource = null;

            //   Main.ItemModelList.Clear();

            foreach (var item in Load_Item)
            {


                if (item.ITEM_ID == Convert.ToInt64(App.Current.Properties["ITEM_ID"]))
                {
                    _ListGrid_Temp.Add(new ItemModel
                    {
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
                        OPN_QNT = item.OPN_QNT,
                        WeightQnt = TRANS_QUANTITY,
                        REGIONAL_LANGUAGE = item.REGIONAL_LANGUAGE,
                        SALES_PRICE_BEFOR_TAX = item.SALES_PRICE_BEFOR_TAX,

                    });
                }
                else
                {
                    _ListGrid_Temp.Add(new ItemModel
                    {
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
                        OPN_QNT = item.OPN_QNT,
                        WeightQnt = Convert.ToInt32(item.OPN_QNT),
                        REGIONAL_LANGUAGE = item.REGIONAL_LANGUAGE,
                        SALES_PRICE_BEFOR_TAX = item.SALES_PRICE_BEFOR_TAX,

                    });
                }
                App.Current.Properties["DataGridTrn"] = _ListGrid_Temp;

            }

            AddStockTransfer.ListGridRef.ItemsSource = null;
            // Main.ListGridRef.ClearValue();
            AddStockTransfer.ListGridRef.ItemsSource = _ListGrid_Temp.ToList();
        }


        public ICommand _GosownClick;
        public ICommand GodownClick
        {
            get
            {
                if (_GosownClick == null)
                {
                    _GosownClick = new DelegateCommand(GodownList_Click);
                }
                return _GosownClick;
            }
        }

        public void GodownList_Click()
        {
            App.Current.Properties["FrmDown"] = 1;
            Window_GodownList IA = new Window_GodownList();
            IA.Show();

        }



        public ICommand _GosownToClick;
        public ICommand GodownToClick
        {
            get
            {
                if (_GosownToClick == null)
                {
                    _GosownToClick = new DelegateCommand(GodownList_ToClick);
                }
                return _GosownToClick;
            }
        }

        public void GodownList_ToClick()
        {
            App.Current.Properties["ToDown"] = 2;
            Window_GodownList IA = new Window_GodownList();
            IA.Show();

        }








        public ICommand _BusinessLocationClick;
        public ICommand BusinessLocationClick
        {
            get
            {
                if (_BusinessLocationClick == null)
                {
                    _BusinessLocationClick = new DelegateCommand(BusinessLocationList_Click);
                }
                return _BusinessLocationClick;
            }
        }

        public void BusinessLocationList_Click()
        {
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();

        }

        public ICommand _ItemListClick;
        public ICommand ItemListClick
        {
            get
            {
                if (_ItemListClick == null)
                {

                    _ItemListClick = new DelegateCommand(ItemList_Click);
                }
                return _ItemListClick;
            }
        }

        public void ItemList_Click()
        {
            App.Current.Properties["itemre"] = 1;
            Window_ItemList IA = new Window_ItemList();
            IA.Show();

        }

        public ICommand _ItemListSearchClick;
        public ICommand ItemListSearchClick
        {
            get
            {
                if (_ItemListSearchClick == null)
                {
                    _ItemListSearchClick = new DelegateCommand(ItemList_SearchClick);
                }
                return _ItemListSearchClick;
            }
        }

        public void ItemList_SearchClick()
        {
            App.Current.Properties["itemcode"] = 2;
            Window_ItemList IA = new Window_ItemList();
            IA.Show();

        }





        public ICommand _SupplierListClick;
        public ICommand SupplierListClick
        {
            get
            {
                if (_SupplierListClick == null)
                {
                    _SupplierListClick = new DelegateCommand(SupplierList_Click);
                }
                return _SupplierListClick;
            }
        }

        public void SupplierList_Click()
        {
            Window_SupplierList IA = new Window_SupplierList();
            IA.Show();

        }




        public ICommand _UpdateStockTransfer { get; set; }
        public ICommand UpdateStockTransfer
        {
            get
            {
                if (_UpdateStockTransfer == null)
                {

                    _UpdateStockTransfer = new DelegateCommand(update_StockTransfer);


                }
                return _UpdateStockTransfer;
            }
        }
        public async void update_StockTransfer()
        {

            if (SelectedStockTransfer.BUSINESS_LOCATION == "" || SelectedStockTransfer.BUSINESS_LOCATION == null)
            {
                MessageBox.Show("Business Location is not Blank");
            }
            else if (SelectedStockTransfer.BARCODE == "" || SelectedStockTransfer.BARCODE == null)
            {
                MessageBox.Show("Barcode is not Blank");
            }
            else if (SelectedStockTransfer.ITEM_NAME == "" || SelectedStockTransfer.ITEM_NAME == null)
            {
                MessageBox.Show("ITEM NAME  is not blank");
            }
            else if (SelectedStockTransfer.SEARCH_CODE == "" || SelectedStockTransfer.SEARCH_CODE == null)
            {
                MessageBox.Show("Search code is not blank");
            }

            else if (SelectedStockTransfer.FROM_GODOWN == "" || SelectedStockTransfer.FROM_GODOWN == null)
            {
                MessageBox.Show("FROM GODOWN is not blank");
            }
            else if (SelectedStockTransfer.TO_GODOWN == "" || SelectedStockTransfer.TO_GODOWN == null)
            {
                MessageBox.Show("To GODOWN is not blank");
            }
            else if (SelectedStockTransfer.EMAIL == "" || SelectedStockTransfer.EMAIL == null)
            {
                MessageBox.Show("EMAIL is not blank");
            }
            else if (!Regex.IsMatch(SelectedStockTransfer.EMAIL,
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                MessageBox.Show("Please Check Email Format");
                //AddCustomer.ShippingemailAddressReff.Focus();
                return;
            }

            else
            {
                SelectedStockTransfer.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("api/StocktransferAPI/UpdateStocktransfer/", SelectedStockTransfer);
                if (SelectedStockTransfer.STOCK_TRANSFER_ID != null)
                {
                    MessageBox.Show("Stocktransfer Update Successfully");
                    Cancel_StockTr();
                    ModalService.NavigateTo(new StockTransfer(), delegate (bool returnValue) { });
                }
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
