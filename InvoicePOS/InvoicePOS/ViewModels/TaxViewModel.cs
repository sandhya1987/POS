using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Item;
using InvoicePOS.UserControll.TaxManagement;
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
    public class TaxViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        TaxManagementModel[] data = null;
        ObservableCollection<TaxManagementModel> _ListGrid_Temp1 = new ObservableCollection<TaxManagementModel>();
        ObservableCollection<TaxManagementModel> _ListGrid_Temp = new ObservableCollection<TaxManagementModel>();
        public TaxViewModel()
        {
            SelectedItem = new ItemModel();
            SelectedTax = new TaxManagementModel();
            SelectedItem1 = new BusinessLocationModel();
            var COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            SelectedTax = new TaxManagementModel();
            GetBussinessList(COMPANY_ID);
            //Show_Taxes(COMPANY_ID);
            GetTaxList(COMPANY_ID);
        }
        private TaxManagementModel _SelectedTax;
        public TaxManagementModel SelectedTax
        {
            get { return _SelectedTax; }
            set
            {
                if (_SelectedTax != value)
                {
                    _SelectedTax = value;
                    OnPropertyChanged("SelectedTax");
                }
            }
        }
        private int _TAX_ID;
        public int TAX_ID
        {
            get
            {
                return SelectedTax.TAX_ID;
            }
            set
            {
                SelectedTax.TAX_ID = value;

                if (SelectedTax.TAX_ID != value)
                {
                    SelectedTax.TAX_ID = value;
                    OnPropertyChanged("TAX_ID");
                }
            }
        }
        private string _TAX_NAME;
        public string TAX_NAME
        {
            get
            {
                return SelectedTax.TAX_NAME;
            }
            set
            {
                SelectedTax.TAX_NAME = value;

                if (SelectedTax.TAX_NAME != value)
                {
                    SelectedTax.TAX_NAME = value;
                    OnPropertyChanged("TAX_NAME");
                }
            }
        }
        private decimal _TAX_VALUE;
        public decimal TAX_VALUE
        {
            get
            {
                return SelectedTax.TAX_VALUE;
            }
            set
            {
                SelectedTax.TAX_VALUE = value;

                if (SelectedTax.TAX_VALUE != value)
                {
                    SelectedTax.TAX_VALUE = value;
                    OnPropertyChanged("TAX_VALUE");
                }
            }
        }
        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedTax.COMPANY_ID;
            }
            set
            {
                SelectedTax.COMPANY_ID = value;

                if (SelectedTax.COMPANY_ID != value)
                {
                    SelectedTax.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }


        //private string _COMPANY;
        //public string COMPANY
        //{
        //    get
        //    {
        //        return SelectedItem.COMPANY;
        //    }
        //    set
        //    {
        //        SelectedItem.COMPANY = value;

        //        if (SelectedItem.COMPANY != value)
        //        {
        //            SelectedItem.COMPANY = value;
        //            OnPropertyChanged("COMPANY");
        //        }
        //    }
        //}

        private string _COMPANY;
        public string COMPANY
        {
            get
            {
                return SelectedTax.COMPANY;
            }
            set
            {
                SelectedTax.COMPANY = value;

                if (SelectedTax.COMPANY != value)
                {
                    SelectedTax.COMPANY = value;
                    OnPropertyChanged("COMPANY");
                }
            }
        }

        private bool _IS_SEPARATE;
        public bool IS_SEPARATE
        {
            get
            {
                return SelectedTax.IS_SEPARATE;
            }
            set
            {
                SelectedTax.IS_SEPARATE = value;

                if (SelectedTax.IS_SEPARATE != value)
                {
                    SelectedTax.IS_SEPARATE = value;
                    OnPropertyChanged("IS_SEPARATE");
                }
            }
        }
        private bool _IS_DELETE;
        public bool IS_DELETE
        {
            get
            {
                return SelectedTax.IS_DELETE;
            }
            set
            {
                SelectedTax.IS_DELETE = value;

                if (SelectedTax.IS_DELETE != value)
                {
                    SelectedTax.IS_DELETE = value;
                    OnPropertyChanged("IS_DELETE");
                }
            }
        }

        public ICommand _ShowTaxes { get; set; }
        public ICommand ShowTaxes
        {
            get
            {
                if (_ShowTaxes == null)
                {
                    //var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                    _ShowTaxes = new DelegateCommand(GetTaxList1);
                }
                return _ShowTaxes;
            }
        }

        public ObservableCollection<TaxManagementModel> _ListGrid { get; set; }
        public ObservableCollection<TaxManagementModel> ListGrid
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
       
        //public ObservableCollection<TaxManagementModel> _ListGrid2 { get; set; }
        //public ObservableCollection<TaxManagementModel> ListGrid2
        //{
        //    get
        //    {
        //        return _ListGrid2;
        //    }
        //    set
        //    {
        //        this._ListGrid2 = value;
        //        OnPropertyChanged("ListGrid2");
        //    }
        //}


       // List<AutoCompleteTax> TaxList1 = new List<AutoCompleteTax>();

        
        public async void GetTaxList1()
        {
            //TaxList1 = new List<AutoCompleteTax>();
            //_ListGrid_Temp = new List<TaxManagementModel>();
            try
            {
                //if( App.Current.Properties["TaxCmpny"]!=null)
                //{
                    if (SelectedItem1.COMPANY != null || App.Current.Properties["TaxCmpny"] != null)
                    {
                        App.Current.Properties["TaxCmpny"] = SelectedItem1.COMPANY;
                        var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                        var company = SelectedItem1.COMPANY;
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        client.Timeout = new TimeSpan(500000000000);
                        //HttpResponseMessage response = client.GetAsync("api/TaxAPI/GetTax?id=" + comp + " &&?id2="+company+"").Result;
                        HttpResponseMessage response = client.GetAsync("api/TaxAPI/GetTax1?id=" + company + "").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            _ListGrid_Temp1.Clear();
                            data = JsonConvert.DeserializeObject<TaxManagementModel[]>(await response.Content.ReadAsStringAsync());

                            for (int i = 0; i < data.Length; i++)
                            {
                                _ListGrid_Temp1.Add(new TaxManagementModel
                                {
                                    COMPANY_ID = data[i].COMPANY_ID,
                                    COMPANY = data[i].COMPANY,
                                    IS_DELETE = data[i].IS_DELETE,
                                    IS_SEPARATE = data[i].IS_SEPARATE,
                                    TAX_ID = data[i].TAX_ID,
                                    TAX_NAME = data[i].TAX_NAME,
                                    TAX_VALUE = data[i].TAX_VALUE,

                                });
                                //TaxList1.Add(new AutoCompleteTax
                                //{
                                //    DisplayName = data[i].TAX_NAME,
                                //    DisplayId = data[i].TAX_ID
                                //});
                            }
                            //TaxList.ListGridRef.ItemsSource = _ListGrid_Temp1;
                            ListGrid = _ListGrid_Temp1;
                            TaxList.ListGridRef.ItemsSource = _ListGrid_Temp1;
                            //App.Current.Properties["TaxPaidListAuto"] = TaxList1;

                            //App.Current.Properties["TaxPaList"] = _ListGrid_Temp;
                        }

                        //return new ObservableCollection<TaxManagementModel>(_ListGrid_Temp);
                    //}
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
        public List<BusinessLocationModel> _ListGridBuss { get; set; }
        public List<BusinessLocationModel> ListGridBuss
        {
            get
            {
                return _ListGridBuss;
            }
            set
            {
                this._ListGridBuss = value;
                OnPropertyChanged("ListGridBuss");
            }
        }
        BusinessLocationModel[] dataBuss = null;
        List<BusinessLocationModel> _ListGrid_Buss = new List<BusinessLocationModel>();
        public async Task<ObservableCollection<BusinessLocationModel>> GetBussinessList(long comp)
        {
            try
            {
                //BussLocadata = new List<BusinessLocationModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/BussLocationAPI/GetAllBusinessLo?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    dataBuss = JsonConvert.DeserializeObject<BusinessLocationModel[]>(await response.Content.ReadAsStringAsync());

                    for (int i = 0; i < dataBuss.Length; i++)
                    {


                        _ListGrid_Buss.Add(new BusinessLocationModel
                        {
                            BUSINESS_LOCATION_ID = dataBuss[i].BUSINESS_LOCATION_ID,
                            BUSS_ADDRESS_1 = dataBuss[i].BUSS_ADDRESS_1,
                            BUSS_ADDRESS_2 = dataBuss[i].BUSS_ADDRESS_2,
                            CITY = dataBuss[i].CITY,
                            COMPANY = dataBuss[i].COMPANY,
                            COMPANY_ID = dataBuss[i].COMPANY_ID,
                            COUNTRY = dataBuss[i].COUNTRY,
                            DOCUMENT_NO = dataBuss[i].DOCUMENT_NO,
                            EMAIL = dataBuss[i].EMAIL,
                            EMAIL_SETTING = dataBuss[i].EMAIL_SETTING,
                            EMAIL_TEMPLATE_SETTING = dataBuss[i].EMAIL_TEMPLATE_SETTING,
                            GODOWN_TO_KEEP = dataBuss[i].GODOWN_TO_KEEP,
                            IMAGE = dataBuss[i].IMAGE,
                            IS_ASK_TO_PRIENT_EVERYTIME = dataBuss[i].IS_ASK_TO_PRIENT_EVERYTIME,
                            IS_DELETE_INVOICE_SPECIFIED_GODOWN = dataBuss[i].IS_DELETE_INVOICE_SPECIFIED_GODOWN,
                            IS_ENABLE_EMAIL = dataBuss[i].IS_ENABLE_EMAIL,
                            IS_SCEOND_RECEIPT_PRINTER = dataBuss[i].IS_SCEOND_RECEIPT_PRINTER,
                            IS_SECOND_DIFF_PRINT = dataBuss[i].IS_SECOND_DIFF_PRINT,
                            MOBILE_NO = dataBuss[i].MOBILE_NO,
                            NUMBER_OF_SECOND_RECEIPT_PRINT = dataBuss[i].NUMBER_OF_SECOND_RECEIPT_PRINT,
                            PHONE_NO = dataBuss[i].PHONE_NO,
                            PIN = dataBuss[i].PIN,
                            PREFIX_DOCUMENT = dataBuss[i].PREFIX_DOCUMENT,
                            PRINTER_SETTING = dataBuss[i].PRINTER_SETTING,
                            SCEOND_RECEIPT_PRINT_FORMATE = dataBuss[i].SCEOND_RECEIPT_PRINT_FORMATE,
                            SCEOND_RECEIPT_PRINTER = dataBuss[i].SCEOND_RECEIPT_PRINTER,
                            SHOP_NAME = dataBuss[i].SHOP_NAME,
                            SMS_SETTING = dataBuss[i].SMS_SETTING,
                            STATE = dataBuss[i].STATE,
                            TIN_NUMBER = dataBuss[i].TIN_NUMBER,
                            WEBSITE = dataBuss[i].WEBSITE,
                        });
                    }
                }
                ListGridBuss = _ListGrid_Buss;
                TaxList.ListGrdMainRef.ItemsSource = _ListGrid_Buss;
                return new ObservableCollection<BusinessLocationModel>(_ListGrid_Buss);
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public ICommand _InsertTax;
        public ICommand InsertTax
        {
            get
            {
                if (_InsertTax == null)
                {
                    _InsertTax = new DelegateCommand(Insert_Tax);
                }
                return _InsertTax;
            }
        }




        List<AutoCompleteTax> TaxList1 = new List<AutoCompleteTax>();

        //List<TaxManagementModel> _ListGrid_Tax = new List<TaxManagementModel>(); 

        //List<TaxManagementModel> _ListGrid_Temp = new List<TaxManagementModel>();
        public async Task<ObservableCollection<TaxManagementModel>> GetTaxList(long comp)
        {
            TaxList1 = new List<AutoCompleteTax>();
           // _ListGrid_Temp = new ObservableCollection<TaxManagementModel>();
           
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/TaxAPI/GetTax?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<TaxManagementModel[]>(await response.Content.ReadAsStringAsync());

                    for (int i = 0; i < data.Length; i++)
                    {
                        _ListGrid_Temp.Add(new TaxManagementModel
                        {
                            COMPANY_ID = data[i].COMPANY_ID,
                            IS_DELETE = data[i].IS_DELETE,
                            IS_SEPARATE = data[i].IS_SEPARATE,
                            TAX_ID = data[i].TAX_ID,
                            TAX_NAME = data[i].TAX_NAME,
                            TAX_VALUE = data[i].TAX_VALUE,

                        });
                        TaxList1.Add(new AutoCompleteTax
                        {
                            DisplayName = data[i].TAX_NAME,
                            DisplayId = data[i].TAX_ID
                        });
                    }
                    ListGrid = _ListGrid_Temp;
                    App.Current.Properties["TaxPaidListAuto"] = TaxList1;

                    App.Current.Properties["TaxPaList"] = _ListGrid_Temp;
                }

                return new ObservableCollection<TaxManagementModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public async void Insert_Tax()
        {

            ////SelectedTax = App.Current.Properties["TaxPaList"] as TaxManagementModel;
            ////App.Current.Properties["TaxPaList"]

            //   App.Current.Properties["Tax"]=ListGrid;
            SelectedTax.COMPANY = App.Current.Properties["TaxCmpny"].ToString();
            //if (SelectedTax.COMPANY != null)
            //{
            //    if(SelectedItem1.COMPANY!=null)
            //{
                SelectedTax.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                var response = await client.PostAsJsonAsync("api/TaxAPI/TaxAdd/", SelectedTax);
                if (response.StatusCode.ToString() == "OK")
                {

                    MessageBox.Show("Tax Added Successfully");
                    Cancel_Tax();
                    App.Current.Properties["TaxCmpny"] = null;
                    //GetTaxList(SelectedTax.COMPANY_ID);
                    ModalService.NavigateTo(new TaxList(), delegate(bool returnValue) { });
                    //TaxList.CatGridRef.ItemsSource = null;
                    //TaxList.CatGridRef.ItemsSource = _ListGrid_Temp.ToList();

                    var company = SelectedTax.COMPANY;
                    HttpClient client1 = new HttpClient();
                    client1.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client1.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client1.Timeout = new TimeSpan(500000000000);
                    //HttpResponseMessage response = client.GetAsync("api/TaxAPI/GetTax?id=" + comp + " &&?id2="+company+"").Result;
                    HttpResponseMessage response1 = client.GetAsync("api/TaxAPI/GetTax?id=" + company + "").Result;
                    if (response1.IsSuccessStatusCode)
                    {
                        data = JsonConvert.DeserializeObject<TaxManagementModel[]>(await response1.Content.ReadAsStringAsync());
                        _ListGrid_Temp.Clear();
                        for (int i = 0; i < data.Length; i++)
                        {
                            _ListGrid_Temp.Add(new TaxManagementModel
                            {
                                COMPANY_ID = data[i].COMPANY_ID,
                                COMPANY = data[i].COMPANY,
                                IS_DELETE = data[i].IS_DELETE,
                                IS_SEPARATE = data[i].IS_SEPARATE,
                                TAX_ID = data[i].TAX_ID,
                                TAX_NAME = data[i].TAX_NAME,
                                TAX_VALUE = data[i].TAX_VALUE,

                            });
                            TaxList1.Add(new AutoCompleteTax
                            {
                                DisplayName = data[i].TAX_NAME,
                                DisplayId = data[i].TAX_ID
                            });
                        }

                        TaxList.ListGridRef.ItemsSource = _ListGrid_Temp;
                            //ListGrid2 = _ListGrid_Temp;
                        //ListGrid = _ListGrid_Temp;
                      
                    }




                }

            //}
            //else
            //{
            //    MessageBox.Show("Select Tax first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
        }
        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_Tax);
                }
                return _Cancel;
            }
        }



        public void Cancel_Tax()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public ICommand _AddTaxClick;
        public ICommand AddTaxClick
        {
            get
            {
                if (_AddTaxClick == null)
                {
                    _AddTaxClick = new DelegateCommand(AddTax_Click);
                }
                return _AddTaxClick;
            }
        }

        public void AddTax_Click()
        {

            SelectedTax = App.Current.Properties["TaxPaList"] as TaxManagementModel;
            AddTax IA = new AddTax();
            IA.Show();
            // ModalService.NavigateTo(new ItemAdd(), delegate(bool returnValue) { });

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


        private BusinessLocationModel _selectedItem1;
        public BusinessLocationModel SelectedItem1
        {
            get { return _selectedItem1; }
            set
            {
                if (_selectedItem1 != value)
                {
                    _selectedItem1 = value;
                    OnPropertyChanged("SelectedItem1");
                }
            }
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
            App.Current.Properties["TaxListsession"] = 1;
            if (App.Current.Properties["TaxListSellingClick"]!=null)
            {
                ItemAdd.TaxRef2.Text = null;
                ItemAdd.TaxRef2.Text = Convert.ToString(SelectedTax.TAX_NAME);
                App.Current.Properties["TaxListSellingClick"] = SelectedTax.TAX_ID;
                App.Current.Properties["TaxCollectid1"] = SelectedTax.TAX_ID;
                SelectedItem.TAX_COLLECTED_ID = SelectedTax.TAX_ID;
                App.Current.Properties["TaxCollectName"] = ItemAdd.TaxRef2.Text;
                ItemViewModel tax = new ItemViewModel();
                tax.taxcollect();
            }
            if (App.Current.Properties["TaxListPurchasingClick"]!=null)
            {
                ItemAdd.TaxRef1.Text = null;
                ItemAdd.TaxRef1.Text = Convert.ToString(SelectedTax.TAX_NAME);
                App.Current.Properties["TaxListPurchasingClick"] = SelectedTax.TAX_ID;
                App.Current.Properties["TaxPaidid"] = SelectedTax.TAX_ID;
            }
           
            Cancel_Tax();
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


    }
}
