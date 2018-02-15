using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Customer;
using InvoicePOS.UserControll.Loyalty;
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
    public class LoyaltyViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        LoyaltyModel[] data = null;
        ObservableCollection<LoyaltyModel> _ListGrid_Temp = new ObservableCollection<LoyaltyModel>();

        private LoyaltyModel _selectedLoyalty;
        public LoyaltyModel selectedLoyalty
        {
            get { return _selectedLoyalty; }
            set
            {
                if (_selectedLoyalty != value)
                {
                    _selectedLoyalty = value;
                    OnPropertyChanged("selectedLoyalty");
                }
            }
        }
        public LoyaltyViewModel()
        {
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                selectedLoyalty = App.Current.Properties["LoyaltyEdit"] as LoyaltyModel;
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                selectedLoyalty = App.Current.Properties["LoyaltyView"] as LoyaltyModel;
                App.Current.Properties["Action"] = "";
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                selectedLoyalty = new LoyaltyModel();
                selectedLoyalty.FROMDATE = System.DateTime.Now;
                selectedLoyalty.TODATE = System.DateTime.Now;
                GetLoyalty(comp);
            
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

        private ICommand _LoyaltyAdd { get; set; }
        public ICommand LoyaltyAdd
        {
            get
            {
                if (_LoyaltyAdd == null)
                {
                    _LoyaltyAdd = new DelegateCommand(Loyalty_Add);
                }
                return _LoyaltyAdd;
            }

        }

        public void Loyalty_Add()
        {
            AddLoyalty _AddLoyalty = new AddLoyalty();
            _AddLoyalty.Show();
            // ModalService.NavigateTo(new AddLoyalty(), delegate(bool returnValue) { });
        }
        private ICommand _CustomerGroupClick;
        public ICommand CustomerGroupClick
        {
            get {
                if (_CustomerGroupClick==null)
                {
                    _CustomerGroupClick = new DelegateCommand(Customer_Group_Click);
                }
                return _CustomerGroupClick;
            }
        
        }

        public void Customer_Group_Click()
        {
            App.Current.Properties["LoyltyCustomerGroup"]=1;
            Customergrouplist asd = new Customergrouplist();
            asd.Show();
        
        }




        private ICommand _LoyaltySearch { get; set; }
        public ICommand LoyaltySearch
        {
            get
            {
                if (_LoyaltySearch == null)
                {
                    _LoyaltySearch = new DelegateCommand(Loyalty_Search);
                }
                return _LoyaltySearch;
            }

        }

        public void Loyalty_Search()
        {
            //ModalService.NavigateTo(new ItemLocationList(), delegate(bool returnValue) { });
        }



        private string _LoyaltyText { get; set; }
        public string LoyaltyText
        {
            get
            {
                return _LoyaltyText;
            }
            set
            {
                if (_LoyaltyText != value)
                {
                    _LoyaltyText = value;


                    OnPropertyChanged("LoyaltyText");
                }
            }
        }
        private string _WEIGHTAGE { get; set; }
        public string WEIGHTAGE
        {
            get
            {
                return selectedLoyalty.WEIGHTAGE;
            }
            set
            {
                if (selectedLoyalty.WEIGHTAGE != value)
                {
                    selectedLoyalty.WEIGHTAGE = value;


                    OnPropertyChanged("WEIGHTAGE");
                }
            }
        }
        private string _COLLECTION { get; set; }
        public string COLLECTION
        {
            get
            {
                return selectedLoyalty.COLLECTION;
            }
            set
            {
                if (selectedLoyalty.COLLECTION != value)
                {
                    selectedLoyalty.COLLECTION = value;


                    OnPropertyChanged("COLLECTION");
                }
            }
        }
        private string _CUSTOMERGROUP { get; set; }
        public string CUSTOMERGROUP
        {
            get
            {
                return selectedLoyalty.CUSTOMERGROUP;
            }
            set
            {
                if (selectedLoyalty.CUSTOMERGROUP != value)
                {
                    selectedLoyalty.CUSTOMERGROUP = value;


                    OnPropertyChanged("CUSTOMERGROUP");
                }
            }
        }
        private string _SETTINGSNAME { get; set; }
        public string SETTINGSNAME
        {
            get
            {
                return selectedLoyalty.SETTINGSNAME;
            }
            set
            {
                if (selectedLoyalty.SETTINGSNAME != value)
                {
                    selectedLoyalty.SETTINGSNAME = value;


                    OnPropertyChanged("SETTINGSNAME");
                }
            }
        }


        private bool _ISACTIVE { get; set; }
        public bool ISACTIVE
        {
            get
            {
                return selectedLoyalty.ISACTIVE;
            }
            set
            {
                if (selectedLoyalty.ISACTIVE != value)
                {
                    selectedLoyalty.ISACTIVE = value;
                    OnPropertyChanged("ISACTIVE");
                }
            }
        }

        private long _LOYALTI_ID { get; set; }
        public long LOYALTI_ID
        {
            get
            {
                return selectedLoyalty.LOYALTI_ID;
            }
            set
            {
                if (selectedLoyalty.LOYALTI_ID != value)
                {
                    selectedLoyalty.LOYALTI_ID = value;
                    OnPropertyChanged("LOYALTI_ID");
                }
            }
        }
        private bool _ISDEFAULT { get; set; }
        public bool ISDEFAULT
        {
            get
            {
                return selectedLoyalty.ISDEFAULT;
            }
            set
            {
                if (selectedLoyalty.ISDEFAULT != value)
                {
                    selectedLoyalty.ISDEFAULT = value;
                    OnPropertyChanged("ISDEFAULT");
                }
            }
        }

        private DateTime _FROMDATE { get; set; }
        public DateTime FROMDATE
        {
            get
            {
                return selectedLoyalty.FROMDATE;
            }
            set
            {
                selectedLoyalty.FROMDATE = value;
                if (selectedLoyalty.FROMDATE != value)
                {
                    selectedLoyalty.FROMDATE = System.DateTime.Now;
                    OnPropertyChanged("FROMDATE");
                }
            }
        }


        private DateTime _TODATE { get; set; }
        public DateTime TODATE
        {
            get
            {
                return selectedLoyalty.TODATE;
            }
            set
            {
                selectedLoyalty.TODATE = value;
                if (selectedLoyalty.TODATE != value)
                {
                    selectedLoyalty.TODATE = System.DateTime.Now;
                    OnPropertyChanged("TODATE");
                }
            }
        }

        private string _REFERALPOINTS { get; set; }
        public string REFERALPOINTS
        {
            get
            {
                return _REFERALPOINTS;
            }
            set
            {
                if (_REFERALPOINTS != value)
                {
                    _REFERALPOINTS = value;
                    OnPropertyChanged("REFERALPOINTS");
                }
            }
        }


        private string _PERFORMANCE { get; set; }
        public string PERFORMANCE
        {
            get
            {
                return _PERFORMANCE;
            }
            set
            {
                if (_PERFORMANCE != value)
                {
                    _PERFORMANCE = value;
                    OnPropertyChanged("PERFORMANCE");
                }
            }
        }

        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_Loyalty);
                }
                return _Cancel;
            }
        }



        public void Cancel_Loyalty()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public ObservableCollection<LoyaltyModel> _ListGrid { get; set; }
        public ObservableCollection<LoyaltyModel> ListGrid
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

        public async Task<ObservableCollection<LoyaltyModel>> GetLoyalty(int comp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/LoyaltyAPI/GetLoyalty?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<LoyaltyModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new LoyaltyModel
                        {
                            SLNO=x,
                            COLLECTION = data[i].COLLECTION,
                            WEIGHTAGE = data[i].WEIGHTAGE,
                            TODATE = data[i].TODATE,
                            SETTINGSNAME = data[i].SETTINGSNAME,
                            LOYALTI_ID = data[i].LOYALTI_ID,
                            ISDEFAULT = data[i].ISDEFAULT,
                            ISACTIVE = data[i].ISACTIVE,
                            IS_DELETE = data[i].IS_DELETE,
                            FROMDATE = data[i].FROMDATE,
                            CUSTOMERGROUP = data[i].CUSTOMERGROUP,
                            COMPANY_ID = data[i].COMPANY_ID,
                        });
                    }
                }
                ListGrid = _ListGrid_Temp;
                selectedLoyalty.ListGrid1 = _ListGrid_Temp;
                return new ObservableCollection<LoyaltyModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {

                throw;
            }
        }





        public ICommand _InsertLoyalty;
        public ICommand InsertLoyalty
        {
            get
            {
                if (_InsertLoyalty == null)
                {
                    _InsertLoyalty = new DelegateCommand(Insert_Loyalty);
                }
                return _InsertLoyalty;
            }
        }
        public async void Insert_Loyalty()
        {
            selectedLoyalty.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());

                if (App.Current.Properties["LoyltyCustomer"]!=null)
            {
                selectedLoyalty.CUSTOMERGROUP = App.Current.Properties["LoyltyCustomer"].ToString();
               
            }
            //_opr.ITEM_NAME = ITEM_NAME;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/LoyaltyAPI/LoyaltyAdd/", selectedLoyalty);
            if (response.StatusCode.ToString() == "OK")
            {

                MessageBox.Show("Loyalty Added Successfully");
                Cancel_Loyalty();
                ModalService.NavigateTo(new LoyaltyList(), delegate(bool returnValue) { });
            }
        }





        public ICommand _EditLoyalty { get; set; }
        public ICommand EditLoyalty
        {
            get
            {
                if (_EditLoyalty == null)
                {
                    _EditLoyalty = new DelegateCommand(Edit_Loyalty);
                }
                return _EditLoyalty;
            }
        }
        public async void Edit_Loyalty()
        {
            if (selectedLoyalty.LOYALTI_ID != null && selectedLoyalty.LOYALTI_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/LoyaltyAPI/LoyaltyEdit?id=" + selectedLoyalty.LOYALTI_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<LoyaltyModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            selectedLoyalty.COLLECTION = data[i].COLLECTION;
                            selectedLoyalty.COMPANY_ID = data[i].COMPANY_ID;
                            selectedLoyalty.CUSTOMERGROUP = data[i].CUSTOMERGROUP;
                            selectedLoyalty.FROMDATE = data[i].FROMDATE;
                            selectedLoyalty.IS_DELETE = data[i].IS_DELETE;
                            selectedLoyalty.ISACTIVE = data[i].ISACTIVE;
                            selectedLoyalty.ISDEFAULT = data[i].ISDEFAULT;
                            selectedLoyalty.LOYALTI_ID = data[i].LOYALTI_ID;
                            selectedLoyalty.SETTINGSNAME = data[i].SETTINGSNAME;
                            selectedLoyalty.TODATE = data[i].TODATE;
                            selectedLoyalty.WEIGHTAGE = data[i].WEIGHTAGE;
                        }
                        App.Current.Properties["LoyaltyEdit"] = selectedLoyalty;
                        AddLoyalty IA = new AddLoyalty();
                        IA.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Loyalty first", "Loyalty Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }



        public ICommand _ViewLoyalty { get; set; }
        public ICommand ViewLoyalty
        {
            get
            {
                if (_ViewLoyalty == null)
                {
                    _ViewLoyalty = new DelegateCommand(View_Loyalty);
                }
                return _ViewLoyalty;
            }
        }
        public async void View_Loyalty()
        {
            if (selectedLoyalty.LOYALTI_ID != null && selectedLoyalty.LOYALTI_ID != 0)
            {
                App.Current.Properties["Action"] = "View";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/LoyaltyAPI/LoyaltyEdit?id=" + selectedLoyalty.LOYALTI_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<LoyaltyModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            selectedLoyalty.COLLECTION = data[i].COLLECTION;
                            selectedLoyalty.COMPANY_ID = data[i].COMPANY_ID;
                            selectedLoyalty.CUSTOMERGROUP = data[i].CUSTOMERGROUP;
                            selectedLoyalty.FROMDATE = data[i].FROMDATE;
                            selectedLoyalty.IS_DELETE = data[i].IS_DELETE;
                            selectedLoyalty.ISACTIVE = data[i].ISACTIVE;
                            selectedLoyalty.ISDEFAULT = data[i].ISDEFAULT;
                            selectedLoyalty.LOYALTI_ID = data[i].LOYALTI_ID;
                            selectedLoyalty.SETTINGSNAME = data[i].SETTINGSNAME;
                            selectedLoyalty.TODATE = data[i].TODATE;
                            selectedLoyalty.WEIGHTAGE = data[i].WEIGHTAGE;
                        }
                        App.Current.Properties["LoyaltyView"] = selectedLoyalty;
                        LoyaltyView IA = new LoyaltyView();
                        IA.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Loyalty first", "Loyalty Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }



        public ICommand _UpdateLoyalty;
        public ICommand UpdateLoyalty
        {
            get
            {
                if (_UpdateLoyalty == null)
                {
                    _UpdateLoyalty = new DelegateCommand(Update_Loyalty);
                }
                return _UpdateLoyalty;
            }
        }


        public async void Update_Loyalty()
        {

            if (App.Current.Properties["LoyltyCustomer"] != null)
            {
                selectedLoyalty.CUSTOMERGROUP = App.Current.Properties["LoyltyCustomer"].ToString();

            }
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/LoyaltyAPI/LoyaltyUpdate/", selectedLoyalty);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Loyalty Update Successfully");
                Cancel_Loyalty();
                ModalService.NavigateTo(new LoyaltyList(), delegate(bool returnValue) { });
            }

        }



        public ICommand _DeleteLoyalty;
        public ICommand DeleteLoyalty
        {
            get
            {
                if (_DeleteLoyalty == null)
                {
                    _DeleteLoyalty = new DelegateCommand(Delete_Loyalty);
                }
                return _DeleteLoyalty;
            }
        }
        public async void Delete_Loyalty()
        {
            if (selectedLoyalty.LOYALTI_ID != null && selectedLoyalty.LOYALTI_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Loyalty " + selectedLoyalty.SETTINGSNAME + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var id = selectedLoyalty.LOYALTI_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/LoyaltyAPI/DeleteLoyalty?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Loyalty Delete Successfully");
                        ModalService.NavigateTo(new LoyaltyList(), delegate(bool returnValue) { });

                    }
                }
                else
                {
                    Cancel_Loyalty();
                }
            }
            else
            {
                MessageBox.Show("Select Loyalty");
            }

        }
        private CustomerModel _SelectedCustomer;
        public CustomerModel SelectedCustomer
        {

            get { return _SelectedCustomer; }
            set
            {

                if (_SelectedCustomer != value)
                {
                    _SelectedCustomer = value;
                    OnPropertyChanged("SelectedCustomer");
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
            var fdrt = App.Current.Properties["CustomerDiffProperties"] as CustomerModel;
            SelectedCustomer.LOYALTY_NO = selectedLoyalty.SETTINGSNAME;
            SelectedCustomer.NAME = fdrt.NAME;
            SelectedCustomer.CUSTOMER_GROUP = fdrt.CUSTOMER_GROUP;
            App.Current.Properties["CustomerDiffProperties"] = SelectedCustomer;

            AddCustomer.ReferredRef.Text = null;
            AddCustomer.ReferredRef.Text = SelectedCustomer.NAME;

            AddCustomer.CustGroupRef.Text = null;
            AddCustomer.CustGroupRef.Text = SelectedCustomer.CUSTOMER_GROUP;



            AddCustomer.LoyaltyRef.Text = null;
            AddCustomer.LoyaltyRef.Text = SelectedCustomer.LOYALTY_NO;

            Cancel_Loyalty();
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