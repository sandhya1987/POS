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
            else if (App.Current.Properties["Action"].ToString() == "ViewTransferCash")
            {
                SelectedItem = App.Current.Properties["TransferCashView"] as CashRegModel;
                App.Current.Properties["Action"] = "";
                // GetCashReg(comp);
            }

            else if (App.Current.Properties["Action"].ToString() == "ViewTransactionCash")
            {
                SelectedItem = App.Current.Properties["TranscationCashView"] as CashRegModel;
                App.Current.Properties["Action"] = "";
                // GetCashReg(comp);
            }

            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                SelectedItem = new CashRegModel();
                //IS_TRANSFER_CASH_REGISTER = true;
                TRANSFER_DATE = System.DateTime.Now;
                GetTransferNo();
                isenable = true;
                _isenable = true;
                GetCashReg(comp);
                GetTransferedCash(comp);
                //GetViewCashTranscation(comp);
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

        private bool _IS_TRANSFER__CASH_REGISTER;
        public bool IS_TRANSFER_CASH_REGISTER
        {
            get
            {
                return SelectedItem.IS_TRANSFER_CASH_REGISTER;
            }
            set
            {
                SelectedItem.IS_TRANSFER_CASH_REGISTER = value;
                if (SelectedItem.IS_TRANSFER_CASH_REGISTER != value)
                {
                    SelectedItem.IS_TRANSFER_CASH_REGISTER = value;
                    OnPropertyChanged("IS_TRANSFER_CASH_REGISTER");
                }
            }
        }


        private long _FROM_TRAN_CASH_REGISTER_ID;
        public long FROM_TRAN_CASH_REGISTER_ID
        {
            get
            {
                return SelectedItem.FROM_TRAN_CASH_REGISTER_ID;
            }
            set
            {
                SelectedItem.FROM_TRAN_CASH_REGISTER_ID = value;
                if (SelectedItem.FROM_TRAN_CASH_REGISTER_ID != value)
                {
                    SelectedItem.FROM_TRAN_CASH_REGISTER_ID = value;
                    OnPropertyChanged("FROM_TRAN_CASH_REGISTER_ID");
                }
            }
        }


        private string _FROM_TRAN_CASH_REGISTER;
        public string FROM_TRAN_CASH_REGISTER
        {
            get
            {
                return SelectedItem.FROM_TRAN_CASH_REGISTER;
            }
            set
            {
                SelectedItem.FROM_TRAN_CASH_REGISTER = value;
                if (SelectedItem.FROM_TRAN_CASH_REGISTER != value)
                {
                    SelectedItem.FROM_TRAN_CASH_REGISTER = value;
                    OnPropertyChanged("FROM_TRAN_CASH_REGISTER");
                }
            }
        }

        private long _TO_TRAN_CASH_REGISTER_ID;
        public long TO_TRAN_CASH_REGISTER_ID
        {
            get
            {
                return SelectedItem.TO_TRAN_CASH_REGISTER_ID;
            }
            set
            {
                SelectedItem.TO_TRAN_CASH_REGISTER_ID = value;
                if (SelectedItem.TO_TRAN_CASH_REGISTER_ID != value)
                {
                    SelectedItem.TO_TRAN_CASH_REGISTER_ID = value;
                    OnPropertyChanged("TO_TRAN_CASH_REGISTER_ID");
                }
            }
        }

        private string _TO_TRAN_CASH_REGISTER;
        public string TO_TRAN_CASH_REGISTER
        {
            get
            {
                return SelectedItem.TO_TRAN_CASH_REGISTER;
            }
            set
            {
                SelectedItem.TO_TRAN_CASH_REGISTER = value;
                if (SelectedItem.TO_TRAN_CASH_REGISTER != value)
                {
                    SelectedItem.TO_TRAN_CASH_REGISTER = value;
                    OnPropertyChanged("TO_TRAN_CASH_REGISTER");
                }
            }
        }

        private long _BUSINESS_LOCATION_ID;
        public long BUSINESS_LOCATION_ID
        {
            get
            {
                return SelectedItem.BUSINESS_LOCATION_ID;
            }
            set
            {
                SelectedItem.BUSINESS_LOCATION_ID = value;

                if (SelectedItem.BUSINESS_LOCATION_ID != value)
                {
                    SelectedItem.BUSINESS_LOCATION_ID = value;
                    OnPropertyChanged("BUSINESS_LOCATION_ID");
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

        private DateTime _TRANSFER_DATE;
        public DateTime TRANSFER_DATE
        {
            get
            {
                return SelectedItem.TRANSFER_DATE;
            }
            set
            {
                SelectedItem.TRANSFER_DATE = value;



                if (SelectedItem.TRANSFER_DATE != value)
                {
                    SelectedItem.TRANSFER_DATE = System.DateTime.Now;
                    OnPropertyChanged("TRANSFER_DATE");
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

        private string _STATUS;
        public string STATUS
        {
            get
            {
                return SelectedItem.STATUS;
            }
            set
            {
                SelectedItem.STATUS = value;

                if (SelectedItem.STATUS != value)
                {
                    SelectedItem.STATUS = value;
                    OnPropertyChanged("STATUS");
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



        //private decimal _CURRENT_REMAIN;
        //public decimal CURRENT_REMAIN
        //{
        //    get
        //    {
        //        return CURRENT_REMAIN;
        //    }
        //    set
        //    {
        //        //if (SelectedItem != null)
        //        //{
        //        CURRENT_REMAIN = 
        //        //}
        //        //else
        //        //    MessageBox.Show("Please Select Item");
        //        OnPropertyChanged("CURRENT_REMAIN");

        //    }
        //}


        private decimal _CURRENT_REMAIN;
        public decimal CURRENT_REMAIN
        {
            get
            {
                return SelectedItem.CURRENT_REMAIN;
            }
            set
            {
                //SelectedItem.CURRENT_REMAIN = value;

                //if (SelectedItem.CURRENT_REMAIN != value)
                //{
                SelectedItem.CURRENT_REMAIN = value;
                OnPropertyChanged("CURRENT_REMAIN");
                //}
            }
        }


        private decimal _SUBMITTED_CASH;
        public decimal SUBMITTED_CASH
        {
            get
            {
                return SelectedItem.SUBMITTED_CASH;
            }
            set
            {

                SelectedItem.SUBMITTED_CASH = value;

                OnPropertyChanged("SUBMITTED_CASH");

            }
        }

        //private string _STATUS;
        //public string STATUS
        //{
        //    get
        //    {
        //        return SelectedItem.STATUS;
        //    }
        //    set
        //    {

        //        SelectedItem.STATUS = value;

        //        OnPropertyChanged("STATUS");

        //    }
        //}

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

        private decimal _CURRENT_CASH;
        public decimal CURRENT_CASH
        {
            get
            {
                return SelectedItem.CURRENT_CASH;
            }
            set
            {
                SelectedItem.CURRENT_CASH = value;
                OnPropertyChanged("CURRENT_CASH");

            }
        }

        private decimal _CASH_TO_TRANSFER;
        public decimal CASH_TO_TRANSFER
        {
            get
            {
                return SelectedItem.CASH_TO_TRANSFER;
            }
            set
            {
                SelectedItem.CASH_TO_TRANSFER = value;
                //if (CURRENT_CASH >= CASH_TO_TRANSFER)
                //{
                //    CURRENT_REMAIN = CURRENT_CASH - CASH_TO_TRANSFER;
                //    CASH_TO_TRANSFER = CASH_TO_TRANSFER;
                //}
                //else
                //{
                //    MessageBox.Show("There is no more cash in This Cash Register's Drawer to transfer..");
                //    CASH_TO_TRANSFER = 0;

                //}

                OnPropertyChanged("CASH_TO_TRANSFER");

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

        private string _TRANSFER_CODE;
        public string TRANSFER_CODE
        {
            get
            {
                return SelectedItem.TRANSFER_CODE;
            }
            set
            {
                SelectedItem.TRANSFER_CODE = value;

                if (SelectedItem.TRANSFER_CODE != value)
                {
                    SelectedItem.TRANSFER_CODE = value;
                    OnPropertyChanged("TRANSFER_CODE");
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
        public async Task<ObservableCollection<CashRegModel>> GetTransferedCash(int comp)
        {
            try
            {
                // CashRegData = new List<CashRegModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CashRegAPI/GetAllTransferedCash?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CashRegModel[]>(await response.Content.ReadAsStringAsync());
                    _ListGrid_Temp.Clear();
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new CashRegModel
                        {
                            SLNO = x,
                            BUSINESS_LOCATION = data[i].BUSINESS_LOCATION,
                            TRANSFER_ID = data[i].TRANSFER_ID,
                            TRANSFER_CODE = data[i].TRANSFER_CODE,
                            FROM_TRAN_CASH_REGISTER = data[i].FROM_TRAN_CASH_REGISTER,
                            TO_TRAN_CASH_REGISTER = data[i].TO_TRAN_CASH_REGISTER,
                            CASH_TO_TRANSFER = data[i].CASH_TO_TRANSFER,
                            TRANSFER_DATE = data[i].TRANSFER_DATE,
                            IS_TRANSFER_CASH_REGISTER = data[i].IS_TRANSFER_CASH_REGISTER,
                            STATUS = data[i].STATUS,

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



        public async Task<ObservableCollection<CashRegModel>> GetCashReg(int comp)
        {
            try
            {
                ObservableCollection<CashRegModel> _ListGrid_Temp1 = new ObservableCollection<CashRegModel>();
                //CashRegData = new List<CashRegModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CashRegAPI/GetAllCashReg?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CashRegModel[]>(await response.Content.ReadAsStringAsync());
                    _ListGrid_Temp1.Clear();
                    //ListGrid.Clear();
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp1.Add(new CashRegModel
                        {
                            SLNO = x,
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
                ListGrid1 = _ListGrid_Temp1;

                return new ObservableCollection<CashRegModel>(_ListGrid_Temp1);
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
            if (SelectedItem.CASH_REGISTERID != null && SelectedItem.CASH_REGISTERID != 0)
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
                        AddCashReg _AddCashReg = new AddCashReg();
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



        public bool _isviewmode;
        public bool _isenable;
        public bool IsViewMode
        {
            get { return _isviewmode; }
            set
            {
                _isviewmode = value;
                // Call NotifyPropertyChanged when the source property is updated.
                NotifyPropertyChanged("IsViewMode");
            }
        }
        public bool isenable
        {
            get { return _isenable; }
            set
            {
                _isenable = value;
                // Call NotifyPropertyChanged when the source property is updated.
                NotifyPropertyChanged("isenable");
            }
        }


        public ICommand _MyTransferCode;
        public ICommand MyTransferCode
        {
            get
            {
                if (_MyTransferCode == null)
                {
                    _MyTransferCode = new DelegateCommand(MyTransfer_Code);
                }
                return _MyTransferCode;
            }

        }
        public string _ButtonText;
        public String ButtonText
        {
            get { return _ButtonText ?? (_ButtonText = "Define My Own"); }
            set
            {
                _ButtonText = value;
                NotifyPropertyChanged("ButtonText");
            }
        }

        public void MyTransfer_Code()
        {
            if (ButtonText == "Auto Generate")
            {
                ButtonText = "Define My Own";
                GetTransferNo();
                TransferCash.TransferNoRef.Text = TRANSFER_CODE;
                _isviewmode = true;
                IsViewMode = true;
            }
            else if (ButtonText == "Define My Own")
            {
                ButtonText = "Auto Generate";
                _isviewmode = false;
                IsViewMode = false;
                TransferCash.TransferNoRef.Text = "";

            }


        }

        public async Task<string> GetTransferNo()
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
                HttpResponseMessage response = client.GetAsync("api/CashRegAPI/GetTransferNo/").Result;
                if (response.IsSuccessStatusCode)
                {
                    //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                    uhy = await response.Content.ReadAsStringAsync();
                    string dd = Convert.ToString(uhy);
                    string aaa = dd.Substring(3, 5);
                    int xx = Convert.ToInt32(aaa);
                    int noo = Convert.ToInt32(xx + 1);
                    nnnn = "T" + noo.ToString("D6");
                    TRANSFER_CODE = nnnn;

                }
                else
                {
                    TRANSFER_CODE = "T000001";
                }
            }
            catch (Exception ex)
            { }

            return uhy;
        }


        public ICommand _TransferCashReg;
        public ICommand TransferCashReg
        {
            get
            {
                if (_TransferCashReg == null)
                {
                    _TransferCashReg = new DelegateCommand(Transfer_CashReg);
                }
                return _TransferCashReg;
            }
        }

        public void Transfer_CashReg()
        {
            TransferCash _TransferCashReg = new TransferCash();
            _TransferCashReg.Show();
            //ModalService.NavigateTo(new AddCashReg(), delegate(bool returnValue) { });

        }


        #region Insert TransferData

        public ICommand _InsertTransferData { get; set; }
        public ICommand InsertTransferData
        {

            get
            {
                if (_InsertTransferData == null)
                {
                    //isenable = false;
                    //_isenable = false;
                    _InsertTransferData = new DelegateCommand(Insert_TransferData);
                }
                return _InsertTransferData;
            }
        }


        public async void Insert_TransferData()
        {
            isenable = false;
            _isenable = false;

            if (SelectedItem.BUSINESS_LOCATION == null || SelectedItem.BUSINESS_LOCATION == "")
            {
                SelectedItem.BUSINESS_LOCATION_ID = Convert.ToInt32(App.Current.Properties["BussTransCashID"]);

                MessageBox.Show("BUSINESS LOCATION is missing");
                return;
            }
            if (SelectedItem.TRANSFER_CODE == null || SelectedItem.TRANSFER_CODE == "")
            {
                MessageBox.Show("TRANSFER_CODEis missing");
                return;
            }
            else if (SelectedItem.FROM_TRAN_CASH_REGISTER == null || SelectedItem.FROM_TRAN_CASH_REGISTER == "")
            {
                MessageBox.Show("FROM_TRAN_CASH_REGISTER is missing");
                return;
            }

            if (SelectedItem.TO_TRAN_CASH_REGISTER == null || SelectedItem.TO_TRAN_CASH_REGISTER == "")
            {
                MessageBox.Show("TO_TRAN_CASH_REGISTER is missing");
                return;
            }
            else if (SelectedItem.CASH_TO_TRANSFER == null || SelectedItem.CASH_TO_TRANSFER == 0)
            {
                MessageBox.Show("Please Enter Amount to Transfered..");
                return;
            }
            else if (SelectedItem.FROM_TRAN_CASH_REGISTER == SelectedItem.TO_TRAN_CASH_REGISTER)
            {
                MessageBox.Show("From Cash Register Should Not be Equal with To Cash Register..");
                return;
            }
            else if (SelectedItem.CURRENT_CASH < SelectedItem.CASH_TO_TRANSFER)
            {
                MessageBox.Show("There Is Not Enough Moneey In This Cash Register to be Transfered..");
                return;
            }

            else
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                //SelectedItem.CASH_REGISTERID_FROM = Convert.ToInt32(App.Current.Properties["CASH_REGISTERID_FROM"]);
                //SelectedItem.CASH_REGISTERID_TO = Convert.ToInt32(App.Current.Properties["CASH_REGISTERID_TO"]);
                SelectedItem.COMPANY_ID = 1;
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                var response = await client.PostAsJsonAsync("api/CashRegAPI/TransferCashAdd/", SelectedItem);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Transfered Cash Details Added Successfully");
                    //AddPO.TotTaxRef.Text = null;
                    //AddPO.TotRef.Text = null;
                    isenable = true;
                    _isenable = true;
                    Cancel_CashReg();
                    ModalService.NavigateTo(new TransferCashList(), delegate(bool returnValue) { });
                }
            }
        }

        #endregion

        #region View TransferData


        public ICommand _ViewCashTransfer { get; set; }
        public ICommand ViewCashTransfer
        {
            get
            {
                if (_ViewCashTransfer == null)
                {
                    _ViewCashTransfer = new DelegateCommand(ViewCash_Transfered);
                }
                return _ViewCashTransfer;
            }
        }

        public async void ViewCash_Transfered()
        {
            if (SelectedItem.TRANSFER_ID != null && SelectedItem.TRANSFER_ID != 0)
            {
                App.Current.Properties["Action"] = "ViewTransferCash";
                //ItemData = new List<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CashRegAPI/ViewTransferCash?id=" + SelectedItem.TRANSFER_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {


                    data = JsonConvert.DeserializeObject<CashRegModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {

                            SelectedItem.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            //TRANSFERED_ID = data[i].TRANSFERED_ID;
                            SelectedItem.TRANSFER_CODE = data[i].TRANSFER_CODE;
                            SelectedItem.FROM_TRAN_CASH_REGISTER = data[i].FROM_TRAN_CASH_REGISTER;
                            SelectedItem.TO_TRAN_CASH_REGISTER = data[i].TO_TRAN_CASH_REGISTER;
                            //SelectedItem.CASH_TO_TRANSFER = data[i].CASH_TO_TRANSFER;
                            SelectedItem.SUBMITTED_CASH = data[i].CASH_TO_TRANSFER;
                            if (data[i].TRANSFER_DATE != null)
                                SelectedItem.TRANSFER_DATE = data[i].TRANSFER_DATE;
                            //IS_TRANSFER_CASH_REGISTER = data[i].IS_TRANSFER_CASH_REGISTER,
                            SelectedItem.STATUS = data[i].STATUS;


                        }
                        App.Current.Properties["TransferCashView"] = SelectedItem;
                        View_Transfer_Cash _view = new View_Transfer_Cash();
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

        #endregion




        #region Delete TransferCash
        public ICommand _DeleteCashTransfer;
        public ICommand DeleteCashTransfer
        {
            get
            {
                if (_DeleteCashTransfer == null)
                {
                    _DeleteCashTransfer = new DelegateCommand(DeleteCash_Transfer);
                }
                return _DeleteCashTransfer;
            }
        }
        public async void DeleteCash_Transfer()
        {
            if (SelectedItem.TRANSFER_ID != null && SelectedItem.TRANSFER_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Cash Transfer Details " + SelectedItem.TRANSFER_CODE + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    var id = SelectedItem.TRANSFER_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/CashRegAPI/DeleteTransferCash?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("TransferCash Detalis Deleted Successfully..");
                        ModalService.NavigateTo(new TransferCashList(), delegate(bool returnValue) { });
                    }
                }
                else
                {
                    Cancel_CashReg();
                }
            }
            else
            {
                MessageBox.Show("Select Item To Delete");
            }

        }
        #endregion


        #region View TranscationData


        public ICommand _ViewCashTranscation { get; set; }
        public ICommand ViewCashTranscation
        {
            get
            {
                if (_ViewCashTranscation == null)
                {
                    _ViewCashTranscation = new DelegateCommand(ViewCash_Transcation);
                }
                return _ViewCashTranscation;
            }
        }

        public void ViewCash_Transcation()
        {
            if (SelectedItem.CASH_REGISTERID != null && SelectedItem.CASH_REGISTERID != 0)
            {
                App.Current.Properties["Action"] = "ViewTransactionCash";
                SelectedItem.BUSINESS_LOCATION = SelectedItem.BUSINESS_LOCATION;
                SelectedItem.CASH_REG_NAME = SelectedItem.CASH_REG_NAME;
                SelectedItem.CASH_REGISTERID = SelectedItem.CASH_REGISTERID;
                App.Current.Properties["TranscationCashView"] = SelectedItem;
                View_Transfer_Cash _view = new View_Transfer_Cash();
                _view.Show();

            }

            else
            {
                MessageBox.Show("Select Cash Reg first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
        }

        #endregion

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

        public ObservableCollection<CashRegModel> _ListGrid1 { get; set; }
        public ObservableCollection<CashRegModel> ListGrid1
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


        public ICommand _BusinessLocationClick;
        public ICommand BusinessLocationClick
        {
            get
            {
                if (_BusinessLocationClick == null)
                {
                    _BusinessLocationClick = new DelegateCommand(BusinessLocationList_Click);
                }
                return _BusinessLocClick;
            }
        }

        public void BusinessLocationList_Click()
        {
            App.Current.Properties["BussCashReg"] = 2;
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();

        }



        public ICommand _BusinessLocClick;
        public ICommand BusinessLocClick
        {
            get
            {
                if (_BusinessLocClick == null)
                {
                    _BusinessLocClick = new DelegateCommand(BusinessLocList_Click);
                }
                return _BusinessLocClick;
            }
        }

        public void BusinessLocList_Click()
        {
            App.Current.Properties["BussTransCash"] = 2;
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();

        }

        public ICommand _FROM_CASH_REGISTER;
        public ICommand FROM_CASH_REGISTER
        {
            get
            {
                if (_FROM_CASH_REGISTER == null)
                {
                    _FROM_CASH_REGISTER = new DelegateCommand(FromCash_Register);
                }
                return _FROM_CASH_REGISTER;
            }
        }

        public void FromCash_Register()
        {
            App.Current.Properties["TRANSFER_FROM_CASH"] = 2;
            Window_CashRegList IA = new Window_CashRegList();
            IA.Show();

        }


        public ICommand _TO_CASH_REGISTER;
        public ICommand TO_CASH_REGISTER
        {
            get
            {
                if (_TO_CASH_REGISTER == null)
                {
                    _TO_CASH_REGISTER = new DelegateCommand(ToCash_Register);
                }
                return _TO_CASH_REGISTER;
            }
        }

        public void ToCash_Register()
        {
            App.Current.Properties["TRANSFER_TO_CASH"] = 2;
            Window_CashRegList IA = new Window_CashRegList();
            IA.Show();

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

            try
            {

                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                var BussLoc = SelectedItem.BUSINESS_LOCATION;
                var frmDt = SelectedItem.FROM_DATE;
                var toDt = SelectedItem.TO_DATE;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                //HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetItemPurchaseList?frmDt=" + frmDt + "&toDt=" + toDt + "").Result;
                HttpResponseMessage response = client.GetAsync("api/CashRegAPI/GetAllCashTranscation?id1=" + comp + " &id2=" + BussLoc + " &id3=" + frmDt + " &id4=" + toDt + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CashRegModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new CashRegModel
                        {
                            SLNO = x,
                            BUSINESS_LOCATION = data[i].BUSINESS_LOCATION,
                            TRANSFER_ID = data[i].TRANSFER_ID,
                            TRANSFER_CODE = data[i].TRANSFER_CODE,
                            FROM_TRAN_CASH_REGISTER = data[i].FROM_TRAN_CASH_REGISTER,
                            TO_TRAN_CASH_REGISTER = data[i].TO_TRAN_CASH_REGISTER,
                            CASH_TO_TRANSFER = data[i].CASH_TO_TRANSFER,
                            TRANSFER_DATE = data[i].TRANSFER_DATE,
                            IS_TRANSFER_CASH_REGISTER = data[i].IS_TRANSFER_CASH_REGISTER,
                            STATUS = data[i].STATUS,

                        });
                    }

                }
                ListGrid = _ListGrid_Temp;

            }
            catch (Exception ex)
            {
                throw;
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
            if (App.Current.Properties["CASH_REG_NAME"] != null && InvoicePOS.Views.CashRegister.CashReg.CashRegName != null && Main.CashRegisterName != null)
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

            if (App.Current.Properties["TRANSFER_FROM_CASH"] != null)
            {
                TransferCash.FromCashRef.Text = _selectedItem.CASH_REG_NAME;
                SelectedItem.FROM_TRAN_CASH_REGISTER = _selectedItem.CASH_REG_NAME;

                //decimal x = _selectedItem.CASH_AMOUNT;
                //SelectedItem.CURRENT_CASH = _selectedItem.CASH_AMOUNT;
                decimal x = _selectedItem.CASH_AMOUNT;
                TransferCash.TotRef.Text = x.ToString();
                App.Current.Properties["CASH_REGISTERID_FROM"] = _selectedItem.CASH_REGISTERID;
                //SelectedItem.CASH_REGISTERID_FROM = _selectedItem.CASH_REGISTERID;
                //TransferCash.TotRef.Text = _selectedItem.CASH_AMOUNT;
                //Convert.ToInt32(_selectedItem.CASH_AMOUNT);
                App.Current.Properties["TRANSFER_FROM_CASH"] = null;
            }

            if (App.Current.Properties["TRANSFER_TO_CASH"] != null)
            {
                //SelectedItem.TO_CASH_REGISTER = _selectedItem.CASH_REG_NAME;
                //SelectedItem.CASH_REGISTERID_TO = _selectedItem.CASH_REGISTERID;
                App.Current.Properties["CASH_REGISTERID_TO"] = _selectedItem.CASH_REGISTERID;
                TransferCash.ToCashRef.Text = _selectedItem.CASH_REG_NAME;
                SelectedItem.TO_TRAN_CASH_REGISTER = _selectedItem.CASH_REG_NAME;
                App.Current.Properties["TRANSFER_TO_CASH"] = null;
            }


        }

    }
}
