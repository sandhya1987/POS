using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Customer;
using InvoicePOS.UserControll.Loyalty;
using InvoicePOS.Views.Customer;
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
    public class CustomerGroupViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        public ICommand _AddCustomerGroupClick;
        public ICommand AddCustomerGroupClick
        {
            get
            {
                if (_AddCustomerGroupClick == null)
                {
                    _AddCustomerGroupClick = new DelegateCommand(AddCustomerGroup_Click);
                }
                return _AddCustomerGroupClick;
            }
        }
        public void AddCustomerGroup_Click()
        {
            Customergroup _AC = new Customergroup();
            _AC.Show();
            // ModalService.NavigateTo(new AddCustomer(), delegate(bool returnValue) { });
        }




        private bool _IS_DELETE;
        public bool IS_DELETE
        {
            get
            {
                return SelectedCustomerGroup.IS_DELETE;
            }
            set
            {
                SelectedCustomerGroup.IS_DELETE = value;


                if (SelectedCustomerGroup.IS_DELETE != value)
                {
                    SelectedCustomerGroup.IS_DELETE = value;
                    OnPropertyChanged("IS_DELETE");
                }
            }
        }
        private string _NAME;
        public string NAME
        {
            get
            {
                return SelectedCustomerGroup.NAME;
            }
            set
            {
                SelectedCustomerGroup.NAME = value;


                if (SelectedCustomerGroup.NAME != value)
                {
                    SelectedCustomerGroup.NAME = value;
                    OnPropertyChanged("NAME");
                }
            }
        }
        private string _DESCRIPTION;
        public string DESCRIPTION
        {
            get
            {
                return SelectedCustomerGroup.DESCRIPTION;
            }
            set
            {
                SelectedCustomerGroup.DESCRIPTION = value;


                if (SelectedCustomerGroup.DESCRIPTION != value)
                {
                    SelectedCustomerGroup.DESCRIPTION = value;
                    OnPropertyChanged("DESCRIPTION");
                }
            }
        }
        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedCustomerGroup.COMPANY_ID;
            }
            set
            {
                SelectedCustomerGroup.COMPANY_ID = value;


                if (SelectedCustomerGroup.COMPANY_ID != value)
                {
                    SelectedCustomerGroup.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
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
        private CustomerGroupModel _SelectedCustomerGroup;
        public CustomerGroupModel SelectedCustomerGroup
        {

            get { return _SelectedCustomerGroup; }
            set
            {

                if (_SelectedCustomerGroup != value)
                {
                    _SelectedCustomerGroup = value;
                    OnPropertyChanged("SelectedCustomerGroup");
                }

            }

        }
        CustomerGroupModel[] data = null;
        public CustomerGroupViewModel()
        {
            var cmp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            SelectedCustomer = new CustomerModel();
            if (App.Current.Properties["Action"] == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedCustomerGroup = App.Current.Properties["CustomerGroupEdit"] as CustomerGroupModel;
                //GetGodowns(COMPANY_ID);
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"] == "View")
            {
                SelectedCustomerGroup = App.Current.Properties["CustomerGroupView"] as CustomerGroupModel;
                App.Current.Properties["Action"] = "";
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                SelectedCustomerGroup = new CustomerGroupModel();
                GetCustomerGroup(cmp);
            }

        }

        List<CustomerGroupAutoModel> autoCustGroupList = new List<CustomerGroupAutoModel>();
        List<CustomerGroupModel> _ListGrid_CustomerGroup = new List<CustomerGroupModel>();
        public async Task<ObservableCollection<CustomerGroupModel>> GetCustomerGroup(long comp)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/CustomerGroupAPI/CustomerGroupList?id=" + comp + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<CustomerGroupModel[]>(await response.Content.ReadAsStringAsync());
                int x = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    x++;
                    _ListGrid_CustomerGroup.Add(new CustomerGroupModel
                    {
                        SLNO = x,
                        COMPANY_ID = data[i].COMPANY_ID,
                        CUSTOMER_GROUP_ID = data[i].CUSTOMER_GROUP_ID,
                        DESCRIPTION = data[i].DESCRIPTION,
                        NAME = data[i].NAME,
                        IS_DELETE = data[i].IS_DELETE,
                    });

                     autoCustGroupList.Add(new CustomerGroupAutoModel
                        {
                            DisplayName = data[i].NAME,
                            DisplayId = data[i].CUSTOMER_GROUP_ID
                        });
                    
                }
                App.Current.Properties["AutoCustGroup"] = autoCustGroupList;
            }
            ListGrid = _ListGrid_CustomerGroup;
            return new ObservableCollection<CustomerGroupModel>(_ListGrid_CustomerGroup);
        }
        public List<CustomerGroupModel> _ListGrid { get; set; }
        public List<CustomerGroupModel> ListGrid
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
        public ICommand _InsertCustomerGroup { get; set; }
        public ICommand InsertCustomerGroup
        {
            get
            {
                if (_InsertCustomerGroup == null)
                {

                    _InsertCustomerGroup = new DelegateCommand(Insert_CustomerGroup);
                }
                return _InsertCustomerGroup;
            }
        }
        public async void Insert_CustomerGroup()
        {
            var cmp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            SelectedCustomerGroup.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            var response = await client.PostAsJsonAsync("api/CustomerGroupAPI/CustomerGroupAdd/", SelectedCustomerGroup);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Customer Group insert Successfully");
                Cancel_CustomerGroup();
                GetCustomerGroup(cmp);
                //ModalService.NavigateTo(new Customergrouplist(), delegate(bool returnValue) { });
            }

        }
        public ICommand _DeleteCustomerGroup;
        public ICommand DeleteCustomerGroup
        {
            get
            {
                if (_DeleteCustomerGroup == null)
                {
                    _DeleteCustomerGroup = new DelegateCommand(Delete_CustomerGroup);
                }
                return _DeleteCustomerGroup;
            }
        }
        public async void Delete_CustomerGroup()
        {
            if (SelectedCustomerGroup.CUSTOMER_GROUP_ID != null && SelectedCustomerGroup.CUSTOMER_GROUP_ID != 0)
            {


                var id = SelectedCustomerGroup.CUSTOMER_GROUP_ID;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                HttpResponseMessage response = client.GetAsync("api/CustomerGroupAPI/CustomerGroupDelete?id=" + id + "").Result;
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Customer Group Delete Successfully");

                }
            }
            else
            {
                MessageBox.Show("Select Customer Group");
            }

        }

        public ICommand _UpdateCustomerGroup { get; set; }
        public ICommand UpdateCustomerGroup
        {
            get
            {
                if (_UpdateCustomerGroup == null)
                {

                    _UpdateCustomerGroup = new DelegateCommand(Update_CustomerGroup);
                }
                return _UpdateCustomerGroup;
            }
        }

        public async void Update_CustomerGroup()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            SelectedCustomerGroup.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            var response = await client.PostAsJsonAsync("api/CustomerGroupAPI/CustomerGroupUpdate/", SelectedCustomerGroup);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Customer Group Update Successfully");
                Cancel_CustomerGroup();
            }
        }
        public ICommand _ViewCustomerGroup { get; set; }
        public ICommand ViewCustomerGroup
        {
            get
            {
                if (_ViewCustomerGroup == null)
                {
                    _ViewCustomerGroup = new DelegateCommand(View_CustomerGroup);
                }
                return _ViewCustomerGroup;
            }
        }
        public async void View_CustomerGroup()
        {
            if (SelectedCustomerGroup.CUSTOMER_GROUP_ID != null && SelectedCustomerGroup.CUSTOMER_GROUP_ID != 0)
            {
                App.Current.Properties["Action"] = "View";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CustomerGroupAPI/CustomerGroupEdit?id=" + SelectedCustomerGroup.CUSTOMER_GROUP_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CustomerGroupModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedCustomerGroup.CUSTOMER_GROUP_ID = data[i].CUSTOMER_GROUP_ID;
                            SelectedCustomerGroup.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedCustomerGroup.DESCRIPTION = data[i].DESCRIPTION;
                            SelectedCustomerGroup.IS_DELETE = data[i].IS_DELETE;
                            SelectedCustomerGroup.NAME = data[i].NAME;
                        }

                    }
                    App.Current.Properties["CustomerGroupView"] = SelectedCustomerGroup;
                    CustomerGroupView vi = new CustomerGroupView();
                    vi.Show();
                }
            }
            else
            {
                MessageBox.Show("Select Godown");
                //ViewGoDown _AGD = new ViewGoDown();
                //_AGD.Show();
            }

            // ModalService.NavigateTo(new ItemLocationList(), delegate(bool returnValue) { });
        }

        public ICommand _EditCustomerGroup { get; set; }
        public ICommand EditCustomerGroup
        {
            get
            {
                if (_EditCustomerGroup == null)
                {
                    _EditCustomerGroup = new DelegateCommand(Edit_CustomerGroup);
                }
                return _EditCustomerGroup;
            }
        }
        public async void Edit_CustomerGroup()
        {
            if (SelectedCustomerGroup.CUSTOMER_GROUP_ID != null && SelectedCustomerGroup.CUSTOMER_GROUP_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                //ItemData = new List<SuppPaymentModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CustomerGroupAPI/CustomerGroupEdit?id=" + SelectedCustomerGroup.CUSTOMER_GROUP_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CustomerGroupModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedCustomerGroup.CUSTOMER_GROUP_ID = data[i].CUSTOMER_GROUP_ID;
                            SelectedCustomerGroup.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedCustomerGroup.DESCRIPTION = data[i].DESCRIPTION;
                            SelectedCustomerGroup.IS_DELETE = data[i].IS_DELETE;
                            SelectedCustomerGroup.NAME = data[i].NAME;
                        }
                        App.Current.Properties["CustomerGroupEdit"] = SelectedCustomerGroup;
                        AddCustomerGroup_Click();
                    }
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
                    _Cancel = new DelegateCommand(Cancel_CustomerGroup);
                }
                return _Cancel;
            }
        }



        public void Cancel_CustomerGroup()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
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
            //SelectedCustomer.NAME = fdrt.NAME;
            //SelectedCustomer.LOYALTY_NO = fdrt.LOYALTY_NO;
            SelectedCustomer.CUSTOMER_GROUP = SelectedCustomerGroup.NAME;
            // App.Current.Properties["CustomerDiffProperties"] = SelectedCustomer;

            //AddCustomer.ReferredRef.Text = null;
            //AddCustomer.ReferredRef.Text = SelectedCustomer.NAME;
            if (App.Current.Properties["CustomerGroup"] != null)
            {
                AddCustomer.CustGroupRef.Text = null;
                AddCustomer.CustGroupRef.Text = SelectedCustomer.CUSTOMER_GROUP;
                App.Current.Properties["CustomerGroup"] = null;
            }

            if (App.Current.Properties["LoyltyCustomerGroup"] != null)
            {
                AddLoyalty.LoyaltyCoustomerGroup.Text = null;
                AddLoyalty.LoyaltyCoustomerGroup.Text = SelectedCustomerGroup.NAME;
                App.Current.Properties["LoyltyCustomer"] = SelectedCustomerGroup.NAME;
                App.Current.Properties["LoyltyCustomerGroup"] = null;
            }

            Cancel_CustomerGroup();
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
