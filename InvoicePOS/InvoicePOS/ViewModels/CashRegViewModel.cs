using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Cash_Reg;
using InvoicePOS.UserControll.CashReg;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.Item;
using InvoicePOS.Views.Main;
using Newtonsoft.Json;
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
using System.Windows.Controls;
using System.Windows.Input;

namespace InvoicePOS.ViewModels
{
    public class CashRegViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        public HttpResponseMessage response;
        // public HttpClient client = new HttpClient();
        public ICommand select { get; set; }
        private readonly CashRegModel OprModel;
        CashRegModel _CashRegModel = new CashRegModel();
        private CashRegModel _selectedItem;
        CashRegModel[] data = null;

        ObservableCollection<CashRegModel> _ListGrid_Temp = new ObservableCollection<CashRegModel>();
        
        public CashRegViewModel()
        {
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedItem = App.Current.Properties["ItemEdit"] as CashRegModel;
                App.Current.Properties["Action"] = "";
                GetCashReg(comp);
            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                SelectedItem = App.Current.Properties["CashRegView"] as CashRegModel;
                App.Current.Properties["Action"] = "";
               // GetCashReg(comp);
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                SelectedItem = new CashRegModel();
                GetCashReg(comp);
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }



        public CashRegModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    App.Current.Properties["CASH_REG_NAME"] = SelectedItem.CASH_REG_NAME;
                    App.Current.Properties["CASH_REG_NAME_BUSINESS"] = SelectedItem.CASH_REG_NAME;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }
        private string _BUSINESS_LOCATION;
        public string BUSINESS_LOCATION
        {
            get
            {
                return SelectedItem.BUSINESS_LOCATION;
            }
            set
            {
                SelectedItem.BUSINESS_LOCATION = value;

                if (SelectedItem.BUSINESS_LOCATION != value)
                {
                    SelectedItem.BUSINESS_LOCATION = value;
                    OnPropertyChanged("BUSINESS_LOCATION");
                }
            }
        }
        private long _CASH_REG_NO;
        public long CASH_REG_NO
        {
            get
            {
                return SelectedItem.CASH_REG_NO;
            }
            set
            {
                SelectedItem.CASH_REG_NO = value;

                if (SelectedItem.CASH_REG_NO != value)
                {
                    SelectedItem.CASH_REG_NO = value;
                    OnPropertyChanged("CASH_REG_NO");
                }
            }
        }

        //private decimal _CASH_AMOUNT;
        //public decimal? CASH_AMOUNT
        //{
        //    get
        //    {
        //        return SelectedItem.CASH_AMOUNT;
        //    }
        //    set
        //    {
        //        SelectedItem.CASH_AMOUNT = value;

        //        if (SelectedItem.CASH_AMOUNT != value)
        //        {
        //            SelectedItem.CASH_AMOUNT = value;
        //            OnPropertyChanged("CASH_AMOUNT");
        //        }
        //    }
        //}

        private decimal? _CASH_AMOUNT;
        public decimal? CASH_AMOUNT
        {
            get
            {
                return _CASH_AMOUNT;
            }
            set
            {
                //if (SelectedItem != null)
                //{
                    _CASH_AMOUNT = value;
                //}
                //else
                //    MessageBox.Show("Please Select Item");
                    OnPropertyChanged("CASH_AMOUNT");

            }
        }

        //private int? _TOTAL_QTY;
        //public int? TOTAL_QTY
        //{
        //    get
        //    {
        //        return _TOTAL_QTY;
        //    }
        //    set
        //    {
        //        if (SelectedItem != null)
        //        {
        //            _TOTAL_QTY = value;
        //        }
        //        else
        //            MessageBox.Show("Please Select Item");
        //        OnPropertyChanged("TOTAL_QTY");

        //    }
        //}

        private string _CASH_REG_NAME;
        public string CASH_REG_NAME
        {
            get
            {
                return SelectedItem.CASH_REG_NAME;
            }
            set
            {
                SelectedItem.CASH_REG_NAME = value;

                if (SelectedItem.CASH_REG_NAME != value)
                {
                    SelectedItem.CASH_REG_NAME = value;
                    OnPropertyChanged("CASH_REG_NAME");
                }
            }
        }
        private string _CASH_REG_PREFIX;
        public string CASH_REG_PREFIX
        {
            get
            {
                return SelectedItem.CASH_REG_PREFIX;
            }
            set
            {
                SelectedItem.CASH_REG_PREFIX = value;

                if (SelectedItem.CASH_REG_PREFIX != value)
                {
                    SelectedItem.CASH_REG_PREFIX = value;
                    OnPropertyChanged("CASH_REG_PREFIX");
                }
            }
        }
        private string _LOGIN;
        public string LOGIN
        {
            get
            {
                return SelectedItem.LOGIN;
            }
            set
            {
                SelectedItem.LOGIN = value;

                if (SelectedItem.LOGIN != value)
                {
                    SelectedItem.LOGIN = value;
                    OnPropertyChanged("LOGIN");
                }
            }
        }
        private bool _ISADGUSTMENT;
        public bool ISADGUSTMENT
        {
            get
            {
                return SelectedItem.ISADGUSTMENT;
            }
            set
            {
                SelectedItem.ISADGUSTMENT = value;

                if (SelectedItem.ISADGUSTMENT != value)
                {
                    SelectedItem.ISADGUSTMENT = value;
                    OnPropertyChanged("ISADGUSTMENT");
                }
            }
        }
        private bool _IS_MAIN_CASH;
        public bool IS_MAIN_CASH
        {
            get
            {
                return SelectedItem.IS_MAIN_CASH;
            }
            set
            {
                SelectedItem.IS_MAIN_CASH = value;

                if (SelectedItem.IS_MAIN_CASH != value)
                {
                    SelectedItem.IS_MAIN_CASH = value;
                    OnPropertyChanged("IS_MAIN_CASH");
                }
            }
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
        public ICommand _InsertCashReg;
        public ICommand InsertCashReg
        {
            get
            {
                if (_InsertCashReg == null)
                {
                    _InsertCashReg = new DelegateCommand(Insert_CashReg);
                }
                return _InsertCashReg;
            }
        }
        public async void Insert_CashReg()
        {
            if (SelectedItem.BUSINESS_LOCATION == null || SelectedItem.BUSINESS_LOCATION=="")
            {
                MessageBox.Show("BUSINESS LOCATION is missing");
                return;
            }
            else if (SelectedItem.CASH_REG_NAME == null || SelectedItem.CASH_REG_NAME == "")
            {
                MessageBox.Show("CASH REG NAME is missing");
                return;
            }
            else if (SelectedItem.CASH_REG_PREFIX == null || SelectedItem.CASH_REG_PREFIX == "")
            {
                MessageBox.Show("CASH REG PREFIX is missing");
                return;
            }
            else
            {
                SelectedItem.CASH_AMOUNT = 0;
                //_opr.ITEM_NAME = ITEM_NAME;
                SelectedItem.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                App.Current.Properties["SelectCashRegItem"] = SelectedItem;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                var response = await client.PostAsJsonAsync("api/CashRegAPI/CreateCashReg/", SelectedItem);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Cash Reg Insert Successfuly");
                    Cancel_CashReg();
                    ModalService.NavigateTo(new CashReg(), delegate(bool returnValue) { });
                }
            }
        }
        public ICommand _UpdateCashReg;
        public ICommand UpdateCashReg
        {
            get
            {
                if (_UpdateCashReg == null)
                {
                    _UpdateCashReg = new DelegateCommand(Update_CashReg);
                }
                return _UpdateCashReg;
            }
        }
        public async void Update_CashReg()
        {
            if (SelectedItem.BUSINESS_LOCATION == null || SelectedItem.BUSINESS_LOCATION == "")
            {
                MessageBox.Show("BUSINESS LOCATION is missing");
                return;
            }
            else if (SelectedItem.CASH_REG_NAME == null || SelectedItem.CASH_REG_NAME == "")
            {
                MessageBox.Show("CASH REG NAME is missing");
                return;
            }
            else if (SelectedItem.CASH_REG_PREFIX == null || SelectedItem.CASH_REG_PREFIX == "")
            {
                MessageBox.Show("CASH REG PREFIX is missing");
                return;
            }
            else
            {
                //_opr.ITEM_NAME = ITEM_NAME;
                SelectedItem.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                var response = await client.PostAsJsonAsync("api/CashRegAPI/CashRegUpdate/", SelectedItem);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Cash Reg Update Successfuly");
                    ModalService.NavigateTo(new CashReg(), delegate(bool returnValue) { });
                }
            }
        }


        private List<CashRegModel> _CashRegData;
        public List<CashRegModel> CashRegData
        {
            get { return _CashRegData; }
            set
            {
                if (_CashRegData != value)
                {
                    _CashRegData = value;
                }
            }

        }
        public async Task<ObservableCollection<CashRegModel>> GetCashReg(int comp)
        {
            try
            {
                CashRegData = new List<CashRegModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CashRegAPI/GetAllCashReg?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CashRegModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new CashRegModel
                        {
                            SLNO=x,
                            BUSINESS_LOCATION = data[i].BUSINESS_LOCATION,
                            CASH_REG_NAME = data[i].CASH_REG_NAME,
                            CASH_REG_NO = data[i].CASH_REG_NO,
                            CASH_AMOUNT = data[i].CASH_AMOUNT,
                            CASH_REG_PREFIX = data[i].CASH_REG_PREFIX,
                            CASH_REGISTERID = data[i].CASH_REGISTERID,
                            ISADGUSTMENT = data[i].ISADGUSTMENT,
                            IS_MAIN_CASH = data[i].IS_MAIN_CASH,
                            LOGIN = data[i].LOGIN,
                        });
                    }

                }
                ListGrid = _ListGrid_Temp;
                return new ObservableCollection<CashRegModel>(_ListGrid_Temp);
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
                    _Cancel = new DelegateCommand(Cancel_CashReg);
                }
                return _Cancel;
            }
        }



        public void Cancel_CashReg()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public ICommand _DeleteCashReg;
        public ICommand DeleteCashReg
        {
            get
            {
                if (_DeleteCashReg == null)
                {
                    _DeleteCashReg = new DelegateCommand(Delete_CashReg);
                }
                return _DeleteCashReg;
            }
        }
        public async void Delete_CashReg()
        {
            if (SelectedItem.CASH_REGISTERID != null && SelectedItem.CASH_REGISTERID !=0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Cash Reg " + SelectedItem.CASH_REG_NO + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    var id = SelectedItem.CASH_REGISTERID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/CashRegAPI/DeleteCashReg?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("CashReg Delete Successfully");
                        ModalService.NavigateTo(new CashReg(), delegate(bool returnValue) { });
                    }
                }
                else
                {
                    Cancel_CashReg();
                }
            }
            else
            {
                MessageBox.Show("Select CashReg");
            }

        }


        public ICommand _AddCashReg;
        public ICommand AddCashReg
        {
            get
            {
                if (_AddCashReg == null)
                {
                    _AddCashReg = new DelegateCommand(Add_CashReg);
                }
                return _AddCashReg;
            }
        }

        public void Add_CashReg()
        {
            AddCashReg _AddCashReg = new AddCashReg();
            _AddCashReg.Show();
            //ModalService.NavigateTo(new AddCashReg(), delegate(bool returnValue) { });

        }


        public ICommand _EditCashReg;
        public ICommand EditCashReg
        {
            get
            {
                if (_EditCashReg == null)
                {
                    _EditCashReg = new DelegateCommand(Edit_CashReg);
                }
                return _EditCashReg;
            }
        }

        public async void Edit_CashReg()
        {
            if (SelectedItem.CASH_REGISTERID != null && SelectedItem.CASH_REGISTERID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                //ItemData = new List<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CashRegAPI/EditCashReg?id=" + SelectedItem.CASH_REGISTERID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CashRegModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedItem.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedItem.CASH_REG_NAME = data[i].CASH_REG_NAME;
                            SelectedItem.CASH_REG_NO = data[i].CASH_REG_NO;
                            SelectedItem.CASH_REG_PREFIX = data[i].CASH_REG_PREFIX;
                            SelectedItem.CASH_REGISTERID = data[i].CASH_REGISTERID;
                            SelectedItem.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedItem.IS_MAIN_CASH = data[i].IS_MAIN_CASH;
                            SelectedItem.ISADGUSTMENT = data[i].ISADGUSTMENT;
                            SelectedItem.LOGIN = data[i].LOGIN;
                        }
                        App.Current.Properties["ItemEdit"] = SelectedItem;
                        AddCashReg _AddCashReg=new AddCashReg();
                        _AddCashReg.Show();
                       //ModalService.NavigateTo(new AddCashReg(), delegate(bool returnValue) { });
                    }
                }
                
            }

            else
            {
                MessageBox.Show("Select Cash Reg first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
        }

        public ICommand _ViewCashReg;
        public ICommand ViewCashReg
        {
            get
            {
                if (_ViewCashReg == null)
                {
                    _ViewCashReg = new DelegateCommand(View_CashReg);
                }
                return _ViewCashReg;
            }
        }

        public async void View_CashReg()
        {
            if (SelectedItem.CASH_REGISTERID != null && SelectedItem.CASH_REGISTERID != 0)
            {
                App.Current.Properties["Action"] = "View";
                //ItemData = new List<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CashRegAPI/EditCashReg?id=" + SelectedItem.CASH_REGISTERID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CashRegModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedItem.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedItem.CASH_REG_NAME = data[i].CASH_REG_NAME;
                            SelectedItem.CASH_REG_NO = data[i].CASH_REG_NO;
                            SelectedItem.CASH_REG_PREFIX = data[i].CASH_REG_PREFIX;
                            SelectedItem.CASH_REGISTERID = data[i].CASH_REGISTERID;
                            SelectedItem.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedItem.IS_MAIN_CASH = data[i].IS_MAIN_CASH;
                            SelectedItem.ISADGUSTMENT = data[i].ISADGUSTMENT;
                            SelectedItem.LOGIN = data[i].LOGIN;
                        }
                        App.Current.Properties["CashRegView"] = SelectedItem;
                        ViewCashReg _view = new ViewCashReg();
                        _view.Show();
                    }
                }

            }

            else
            {
                MessageBox.Show("Select Cash Reg first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
        }
        
        public ObservableCollection<CashRegModel> _ListGrid { get; set; }
        public ObservableCollection<CashRegModel> ListGrid
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
            App.Current.Properties["BussCashReg"]=2;
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
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
        public void Select_Ok()
        {
            //var locList = App.Current.Properties["BussLocList"] as BusinessLocationModel;
            //_selectedItem.BUSINESS_LOCATION = locList.BUSINESS_LOCATION;
            //if (locList != null)
            //{
            //    _selectedItem.BUSINESS_LOCATION = locList.BUSINESS_LOCATION;

            //}
            if(App.Current.Properties["CASH_REG_NAME"] != null && InvoicePOS.Views.CashRegister.CashReg.CashRegName != null && Main.CashRegisterName != null)
            {
                //InvoicePOS.Views.CashRegister.ChangeBussinessLocation.CashReg.Text = _selectedItem.CASH_REG_NAME;
                InvoicePOS.Views.CashRegister.CashReg.CashRegName.Text = _selectedItem.CASH_REG_NAME;
                Main.CashRegisterName.Text = _selectedItem.CASH_REG_NAME;
                App.Current.Properties["SelectBusinessName"] = _selectedItem.BUSINESS_LOCATION;
                App.Current.Properties["CurrentCash"] = _selectedItem.CASH_AMOUNT;
            }
            if (App.Current.Properties["CASH_REG_NAME_BUSINESS"] != null && InvoicePOS.Views.CashRegister.ChangeBussinessLocation.CashReg != null)
            {
                InvoicePOS.Views.CashRegister.ChangeBussinessLocation.CashReg.Text = _selectedItem.CASH_REG_NAME;
                //InvoicePOS.Views.CashRegister.CashReg.CashRegName.Text = _selectedItem.CASH_REG_NAME;

            }
            
        }
        
    }
}
