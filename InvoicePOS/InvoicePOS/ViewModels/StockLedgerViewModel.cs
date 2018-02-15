using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.GoDown;
using InvoicePOS.UserControll.Item;
using InvoicePOS.UserControll.StockLedger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InvoicePOS.ViewModels
{
    class StockLedgerViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        StockLedger[] data = null;
      private  int totstockin = 0;
      private  int totstockout = 0;
     private   int tottrans = 0;
        ObservableCollection<StockLedger> _ListGrid_Temp = new ObservableCollection<StockLedger>();
        ObservableCollection<StockLedger> _ListGrid_Temp1 = new ObservableCollection<StockLedger>();
        public StockLedgerViewModel()
        {
            var id = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            GetStockTransfer(id);
        }
        public async Task<ObservableCollection<StockLedger>> GetStockTransfer(long id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = await client.GetAsync("api/StocktransferAPI/GetStocktransfer?Code=" + id + "");
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<StockLedger[]>(await response.Content.ReadAsStringAsync());

                    for (int i = 0; i < data.Length; i++)
                    {
                        _ListGrid_Temp.Add(new StockLedger
                        {

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
                return new ObservableCollection<StockLedger>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ObservableCollection<StockLedger>> GetStockDetails()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            DateTime fromdate = DateTime.Now;
            DateTime todate = DateTime.Now;
            try
            {
                fromdate = Convert.ToDateTime(StockLedgerList.fromdate1.Text);
                todate = Convert.ToDateTime(StockLedgerList.todate1.Text);
            }
            catch (Exception ex)
            { }
            int companyid = 0;
            int godownid = 0;
            int itemid = 0;
            if (App.Current.Properties["getcompanyid"] != null)
            {
                companyid = Convert.ToInt32(App.Current.Properties["getcompanyid"].ToString());
                //companyid = 1;
            }
            if (App.Current.Properties["getGodownStockLadgerid"] != null)
            {
                godownid = Convert.ToInt32(App.Current.Properties["getGodownStockLadgerid"].ToString());
                //godownid = 1;
            }
            if (App.Current.Properties["ItemIdStockLadger"] != null)
            {
                itemid = Convert.ToInt32(App.Current.Properties["ItemIdStockLadger"].ToString());
                //itemid = 1;
            }
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                if (StockLedgerList.fromdate1.Text == "" && StockLedgerList.fromdate1.Text == "")
                {
                    response = await client.GetAsync("api/StocktransferAPI/GetStocktDetails?companyid=" + companyid + "&godownid=" + godownid + "&itemid=" + itemid + "");
                }
                else
                {
                    response = await client.GetAsync("api/StocktransferAPI/GetStocktDetailsbydate?companyid=" + companyid + "&godownid=" + godownid + "&itemid=" + itemid + "&from1=" + fromdate + "&to=" + todate + "");
                }
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<StockLedger[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp1.Add(new StockLedger
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
                            TRANS_QUANTITY = data[i].TRANS_QUANTITY,
                            OPN_QNT = data[i].OPN_QNT,


                        });
                    }

                }
                foreach (var a in _ListGrid_Temp1)
                {
                    if (a.FROM_GODOWN_ID == godownid)
                    {
                        a.stockout = a.TRANS_QUANTITY;
                        totstockin = totstockin + a.TRANS_QUANTITY;
                    }
                    if (a.TO_GODOWN_ID == godownid)
                    {
                        a.stockin = a.TRANS_QUANTITY;
                        totstockout =totstockout+ a.TRANS_QUANTITY;
                    }
                }
                ListGrid1 = _ListGrid_Temp1;
                StockLedgerList.getopeningstock.Text = _ListGrid_Temp1[0].OPN_QNT.ToString();
                tottrans = totstockin + totstockout;
                StockLedgerList.gettotaltrans.Text = tottrans.ToString();
                StockLedgerList.getstockin.Text = totstockin.ToString();
                StockLedgerList.getstockout.Text = totstockout.ToString();
                return new ObservableCollection<StockLedger>(_ListGrid_Temp1);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void TabChange_Code()
        {

            //var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            //GridPOVisible = "Collapsed";
            //GridItemVisible = "Visible";
            //GetItemList(comp);

            //GetStockbybarcode();
            //_Select_BarCode = "";
            //Select_BarCode = "";
            //ReceiveAddItem.ItemReff.Text = "";
            //ReceiveAddItem.SearchItemReff.Text = "";

            //}
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
        private StockLedger _SelectedStockTransfer;
        public StockLedger SelectedStockTransfer
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

                if (SelectedStockTransfer.DATE != value)
                {
                    SelectedStockTransfer.DATE = value;
                    OnPropertyChanged("DATE");
                }
            }
        }
        private long _comapany;
        public long comapany
        {
            get
            {
                return _comapany;
            }
            set
            {
                _comapany = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (_comapany != value)
                {
                    _comapany = value;
                    OnPropertyChanged("comapany");
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















        public ObservableCollection<StockLedger> _ListGrid { get; set; }
        public ObservableCollection<StockLedger> ListGrid
        {
            get
            {
                return _ListGrid;
            }
            set
            {
                this._ListGrid = value;
                OnPropertyChanged("ItemListGrid");
            }
        }
        public ObservableCollection<StockLedger> _ListGrid1 { get; set; }
        public ObservableCollection<StockLedger> ListGrid1
        {
            get
            {
                return _ListGrid1;
            }
            set
            {
                this._ListGrid1 = value;
                OnPropertyChanged("ListGrid1");
            }
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
            App.Current.Properties["GodownStockLadger"] = 1;
            Window_GodownList IA = new Window_GodownList();
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
        public ICommand _getdetails;
        public ICommand getdetails
        {
            get
            {
                if (_getdetails == null)
                {
                    _getdetails = new DelegateCommand(getdetails_Click);
                }
                return _getdetails;
            }
        }
        public void getdetails_Click()
        {
            GetStockDetails();

        }
        public ICommand _ComapnyClick;
        public ICommand ComapnyClick
        {
            get
            {
                if (_ComapnyClick == null)
                {
                    _ComapnyClick = new DelegateCommand(Company_Click);
                }
                return _ComapnyClick;
            }
        }
        public void Company_Click()
        {
            App.Current.Properties["getcompany"] = 1;
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();

        }
        public void ItemList_Click()
        {
            App.Current.Properties["ItemStockLadger"] = 1;
            Window_ItemList IA = new Window_ItemList();
            IA.Show();

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
