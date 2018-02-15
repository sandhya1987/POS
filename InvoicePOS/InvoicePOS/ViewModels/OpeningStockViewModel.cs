using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.GoDown;
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

namespace InvoicePOS.ViewModels
{
    public class OpeningStockViewModel : BaseViewModel, INotifyPropertyChanged, IModalService
    {
        private OpeningStockModel _SelectedOpeningStock;
        OpeningStockModel[] data = null;
        //  private readonly DataCache dataCache = DataCache.Instance;
        //  private readonly DataCache dataCache;
        List<AutoBussinessModel> autoBusList = new List<AutoBussinessModel>();
        List<AutoGodownModel> autoGoDownList = new List<AutoGodownModel>();
        private readonly DataCache dataCache = DataCache.Instance;

        public OpeningStockViewModel()
        {
            //var name = App.Current.Properties["ItemName"];
            SelectedOpeningStock = new OpeningStockModel();
            // _BUSINESS_LOC = ;

            if (App.Current.Properties["Value"] != null)
            {
                _SelectedOpeningStock = App.Current.Properties["Value"] as OpeningStockModel;
                _BUSINESS_LOC = _SelectedOpeningStock.BUSINESS_LOC;
                _BUSINESS_LOC_ID = _SelectedOpeningStock.BUSINESS_LOC_ID;
                // Window_BusinessLocationList LOC = new Window_BusinessLocationList();
                // LOC.Close();
                dataCache.OnMessageChanged += RaiseMessageChanged;
                if (App.Current.Properties["OpDiffProperties"] != null)
                {
                    App.Current.Properties["OpDiffProperties"] = SelectedOpeningStock;
                }
            }
                App.Current.Properties["OppingDiffProperties"] = SelectedOpeningStock;
            
        }

        private void RaiseMessageChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("Message");
        }

        public string Message
        {
            get { return dataCache.OnScreenMessage; }
            set { dataCache.OnScreenMessage = value; }
        }
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
        private string _GODOWN;
        public string GODOWN
        {
            get
            {
                return SelectedOpeningStock.GODOWN;
            }
            set
            {
                SelectedOpeningStock.GODOWN = value;

                if (SelectedOpeningStock.GODOWN != value)
                {
                    SelectedOpeningStock.GODOWN = value;
                    OnPropertyChanged("GODOWN");
                }
            }
        }
        private int _OPN_QNT;
        public int OPN_QNT
        {
            get
            {
                return SelectedOpeningStock.OPN_QNT;
            }
            set
            {
                SelectedOpeningStock.OPN_QNT = value;

                if (SelectedOpeningStock.OPN_QNT != value)
                {
                    SelectedOpeningStock.OPN_QNT = value;
                    OnPropertyChanged("OPN_QNT");
                }
            }
        }
        private string _COMPANY_NAME;
        public string COMPANY_NAME
        {
            get
            {
                return SelectedOpeningStock.COMPANY_NAME;
            }
            set
            {
                SelectedOpeningStock.COMPANY_NAME = value;

                if (SelectedOpeningStock.COMPANY_NAME != value)
                {
                    SelectedOpeningStock.COMPANY_NAME = value;
                    OnPropertyChanged("SelectedOpeningStock");
                }
            }
        }

        private long _OPENING_STOCK_ID;
        public long OPENING_STOCK_ID
        {
            get
            {
                return SelectedOpeningStock.OPENING_STOCK_ID;
            }
            set
            {
                SelectedOpeningStock.OPENING_STOCK_ID = value;


                if (SelectedOpeningStock.OPENING_STOCK_ID != value)
                {
                    SelectedOpeningStock.OPENING_STOCK_ID = value;
                    OnPropertyChanged("OPENING_STOCK_ID");
                }
            }
        }
        private long _ITEM_ID;
        public long ITEM_ID
        {
            get
            {
                return SelectedOpeningStock.ITEM_ID;
            }
            set
            {
                SelectedOpeningStock.ITEM_ID = value;


                if (SelectedOpeningStock.ITEM_ID != value)
                {
                    SelectedOpeningStock.ITEM_ID = value;
                    OnPropertyChanged("ITEM_ID");
                }
            }
        }
        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedOpeningStock.COMPANY_ID;
            }
            set
            {
                SelectedOpeningStock.COMPANY_ID = value;


                if (SelectedOpeningStock.COMPANY_ID != value)
                {
                    SelectedOpeningStock.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }





        private long _BUSINESS_LOC_ID;
        public long BUSINESS_LOC_ID
        {
            get { return _BUSINESS_LOC_ID; }
            set
            {
                _BUSINESS_LOC_ID = value;
                OnPropertyChanged("BUSINESS_LOC_ID");
            }
        }
        private DateTime _DATE;
        public DateTime DATE
        {
            get
            {
                return SelectedOpeningStock.DATE;
            }
            set
            {
                SelectedOpeningStock.DATE = value;


                if (SelectedOpeningStock.DATE != value)
                {
                    SelectedOpeningStock.DATE = value;
                    OnPropertyChanged("DATE");
                }
            }
        }

        private decimal _OPRNING_BAL;
        public decimal OPRNING_BAL
        {
            get
            {
                return SelectedOpeningStock.OPRNING_BAL;
            }
            set
            {
                SelectedOpeningStock.OPRNING_BAL = value;


                if (SelectedOpeningStock.OPRNING_BAL != value)
                {
                    SelectedOpeningStock.OPRNING_BAL = value;
                    OnPropertyChanged("OPRNING_BAL");
                }
            }
        }


        private string _BUSINESS_LOC;
        public string BUSINESS_LOC
        {
            get
            {
                return SelectedOpeningStock.BUSINESS_LOC;
            }
            set
            {
                SelectedOpeningStock.BUSINESS_LOC = value;


                if (SelectedOpeningStock.BUSINESS_LOC != value)
                {
                    SelectedOpeningStock.BUSINESS_LOC = value;
                    OnPropertyChanged("BUSINESS_LOC");
                }
            }
        }


        private string _BARCODE;
        public string BARCODE
        {
            get
            {
                return SelectedOpeningStock.BARCODE;
            }
            set
            {
                SelectedOpeningStock.BARCODE = value;


                if (SelectedOpeningStock.BARCODE != value)
                {
                    SelectedOpeningStock.BARCODE = value;
                    OnPropertyChanged("BARCODE");
                }
            }
        }
        private string _ITEM_NAME;
        public string ITEM_NAME
        {
            get
            {
                return SelectedOpeningStock.ITEM_NAME;
            }
            set
            {
                SelectedOpeningStock.ITEM_NAME = value;


                if (SelectedOpeningStock.ITEM_NAME != value)
                {
                    SelectedOpeningStock.ITEM_NAME = value;
                    OnPropertyChanged("ITEM_NAME");
                }
            }
        }
        private decimal _CLOSING_BAL;
        public decimal CLOSING_BAL
        {
            get
            {
                return SelectedOpeningStock.CLOSING_BAL;
            }
            set
            {
                SelectedOpeningStock.CLOSING_BAL = value;


                if (SelectedOpeningStock.CLOSING_BAL != value)
                {
                    SelectedOpeningStock.CLOSING_BAL = value;
                    OnPropertyChanged("CLOSING_BAL");
                }
            }
        }
        public ICommand _EditItem;
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
            if (SelectedOpeningStock.OPENING_STOCK_ID != null && SelectedOpeningStock.OPENING_STOCK_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetEditItem?id=" + SelectedOpeningStock.OPENING_STOCK_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<OpeningStockModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedOpeningStock.BUSINESS_LOC_ID = data[i].BUSINESS_LOC_ID;
                            SelectedOpeningStock.CLOSING_BAL = data[i].CLOSING_BAL;
                            SelectedOpeningStock.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedOpeningStock.DATE = data[i].DATE;
                            SelectedOpeningStock.ITEM_ID = data[i].ITEM_ID;
                            SelectedOpeningStock.OPENING_STOCK_ID = data[i].OPENING_STOCK_ID;
                            SelectedOpeningStock.OPRNING_BAL = data[i].OPRNING_BAL;
                        }
                    }
                    App.Current.Properties["Stockopen"]=SelectedOpeningStock;
                }
            }
        }
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
            App.Current.Properties["Stockopen"] = SelectedOpeningStock;
            MessageBox.Show("Opening Stock Insert Successfully");
            Cancel_OppStock();
            //SelectedOpeningStock.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            ////_opr.ITEM_NAME = ITEM_NAME;
            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));
            //client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            //var response = await client.PostAsJsonAsync("api/OpeningStockAPI/OpenIngStockAdd/", SelectedOpeningStock);
            //if (response.StatusCode.ToString() == "OK")
            //{

            //    MessageBox.Show("Opening Stock Insert Successfully");
            //    Cancel_OppStock();
            //}

        }


        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_OppStock);
                }
                return _Cancel;
            }
        }



        public void Cancel_OppStock()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
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
            Window_GodownList IA = new Window_GodownList();
            IA.Show();

        }
        public ICommand _CompanyClick;
        public ICommand CompanyClick
        {
            get
            {
                if (_CompanyClick == null)
                {
                    _CompanyClick = new DelegateCommand(Company_Click);
                }
                return _CompanyClick;
            }
        }

        public void Company_Click()
        {
            Window_GodownList IA = new Window_GodownList();
            IA.Show();

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

        //public static object DataCache { get; private set; }
    }
}
