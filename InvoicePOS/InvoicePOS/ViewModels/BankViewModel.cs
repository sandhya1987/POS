using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Bank;
using InvoicePOS.UserControll.SuppPayment;
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
    public class BankViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        
        BankModel[] data = null;
        ObservableCollection<BankModel> _ListGrid_Temp = new ObservableCollection<BankModel>();
        List<BankAccountModel> _ListGrid_Temp_ac = new List<BankAccountModel>();
        public BankViewModel()
        {
            SelectedItem = new SuppPaymentModel();
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                selectedBank = App.Current.Properties["BankEdit"] as BankModel;
                App.Current.Properties["Action"] = "";
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                selectedBank = new BankModel();
                selectedBankAC = new BankAccountModel();
                GetBank(comp);
                GetBankAC(comp);
            }

        }
        private BankModel _selectedBank;
        public BankModel selectedBank
        {
            get { return _selectedBank; }
            set
            {
                if (_selectedBank != value)
                {
                    _selectedBank = value;
                    OnPropertyChanged("selectedBank");
                }
            }
        }
        private BankAccountModel _selectedBankAC;
        public BankAccountModel selectedBankAC
        {
            get { return _selectedBankAC; }
            set
            {
                if (_selectedBankAC != value)
                {
                    _selectedBankAC = value;
                    OnPropertyChanged("selectedBankAC");
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
        //private long _WEIGHTAGE { get; set; }
        //public long WEIGHTAGE
        private long _BANK_ID { get; set; }
        public long BANK_ID

        {
            get
            {
                return selectedBank.BANK_ID;
            }
            set
            {
                if (selectedBank.BANK_ID != value)
                {
                    selectedBank.BANK_ID = value;


                    OnPropertyChanged("BANK_ID");
                }
            }
        }
        private string _BANK_NAME { get; set; }
        public string BANK_NAME
        {
            get
            {
                return selectedBank.BANK_NAME;
            }
            set
            {
                if (selectedBank.BANK_NAME != value)
                {
                    selectedBank.BANK_NAME = value;


                    OnPropertyChanged("BANK_NAME");
                }
            }
        }
        private string _IFSC_CODE { get; set; }
        public string IFSC_CODE
        {
            get
            {
                return selectedBank.IFSC_CODE;
            }
            set
            {
                if (selectedBank.IFSC_CODE != value)
                {
                    selectedBank.IFSC_CODE = value;


                    OnPropertyChanged("IFSC_CODE");
                }
            }
        }
        private string _ADDRESS_1 { get; set; }
        public string ADDRESS_1
        {
            get
            {
                return selectedBank.ADDRESS_1;
            }
            set
            {
                if (selectedBank.ADDRESS_1 != value)
                {
                    selectedBank.ADDRESS_1 = value;
                    OnPropertyChanged("ADDRESS_1");
                }
            }
        }
        private string _ADDRESS_2 { get; set; }
        public string ADDRESS_2
        {
            get
            {
                return selectedBank.ADDRESS_2;
            }
            set
            {
                if (selectedBank.ADDRESS_2 != value)
                {
                    selectedBank.ADDRESS_2 = value;
                    OnPropertyChanged("ADDRESS_2");
                }
            }
        }
        private string _CITY { get; set; }
        public string CITY
        {
            get
            {
                return selectedBank.CITY;
            }
            set
            {
                if (selectedBank.CITY != value)
                {
                    selectedBank.CITY = value;
                    OnPropertyChanged("CITY");
                }
            }
        }
        private string _STATE { get; set; }
        public string STATE
        {
            get
            {
                return selectedBank.STATE;
            }
            set
            {
                if (selectedBank.STATE != value)
                {
                    selectedBank.STATE = value;
                    OnPropertyChanged("STATE");
                }
            }
        }
        private string _PIN_CODE { get; set; }
        public string PIN_CODE
        {
            get
            {
                return selectedBank.PIN_CODE;
            }
            set
            {
                if (selectedBank.PIN_CODE != value)
                {
                    selectedBank.PIN_CODE = value;
                    OnPropertyChanged("PIN_CODE");
                }
            }
        }
        private string _COUNTRY { get; set; }
        public string COUNTRY
        {
            get
            {
                return selectedBank.COUNTRY;
            }
            set
            {
                if (selectedBank.COUNTRY != value)
                {
                    selectedBank.COUNTRY = value;
                    OnPropertyChanged("COUNTRY");
                }
            }
        }
        private string _PHONE_NUMBER { get; set; }
        public string PHONE_NUMBER
        {
            get
            {
                return selectedBank.PHONE_NUMBER;
            }
            set
            {
                if (selectedBank.PHONE_NUMBER != value)
                {
                    selectedBank.PHONE_NUMBER = value;
                    OnPropertyChanged("PHONE_NUMBER");
                }
            }
        }
        private string _MOBILE_NUMBER { get; set; }
        public string MOBILE_NUMBER
        {
            get
            {
                return selectedBank.MOBILE_NUMBER;
            }
            set
            {
                if (selectedBank.MOBILE_NUMBER != value)
                {
                    selectedBank.MOBILE_NUMBER = value;
                    OnPropertyChanged("MOBILE_NUMBER");
                }
            }
        }
        private string _FAX_NUMBER { get; set; }
        public string FAX_NUMBER
        {
            get
            {
                return selectedBank.FAX_NUMBER;
            }
            set
            {
                if (selectedBank.FAX_NUMBER != value)
                {
                    selectedBank.FAX_NUMBER = value;
                    OnPropertyChanged("FAX_NUMBER");
                }
            }
        }
        private string _EMAIL { get; set; }
        public string EMAIL
        {
            get
            {
                return selectedBank.EMAIL;
            }
            set
            {
                if (selectedBank.EMAIL != value)
                {
                    selectedBank.EMAIL = value;
                    OnPropertyChanged("EMAIL");
                }
            }
        }
        private string _BANK_CODE { get; set; }
        public string BANK_CODE
        {
            get
            {
                return selectedBank.BANK_CODE;
            }
            set
            {
                if (selectedBank.BANK_CODE != value)
                {
                    selectedBank.BANK_CODE = value;
                    OnPropertyChanged("BANK_CODE");
                }
            }
        }
        //private long _BANK_ID { get; set; }
        //public long BANK_ID
        //{
        //    get
        //    {
        //        return selectedBankAC.BANK_ID;
        //    }
        //    set
        //    {
        //        if (selectedBankAC.BANK_ID != value)
        //        {
        //            selectedBankAC.BANK_ID = value;
        //            OnPropertyChanged("BANK_ID");
        //        }
        //    }
        //}
        private int _COMPANY_ID { get; set; }
        public int COMPANY_ID
        {
            get
            {
                return selectedBankAC.COMPANY_ID;
            }
            set
            {
                if (selectedBankAC.COMPANY_ID != value)
                {
                    selectedBankAC.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }
        private string _ACCOUNT_NUMBER { get; set; }
        public string ACCOUNT_NUMBER
        {
            get
            {
                return selectedBankAC.ACCOUNT_NUMBER;
            }
            set
            {
                if (selectedBankAC.ACCOUNT_NUMBER != value)
                {
                    selectedBankAC.ACCOUNT_NUMBER = value;
                    OnPropertyChanged("ACCOUNT_NUMBER");
                }
            }
        }
        private string _BRANCH_NAME { get; set; }
        public string BRANCH_NAME
        {
            get
            {
                return selectedBankAC.BRANCH_NAME;
            }
            set
            {
                if (selectedBankAC.BRANCH_NAME != value)
                {
                    selectedBankAC.BRANCH_NAME = value;
                    OnPropertyChanged("BRANCH_NAME");
                }
            }
        }
        private string _ACCOUNT_DESCRIPTION { get; set; }
        public string ACCOUNT_DESCRIPTION
        {
            get
            {
                return selectedBankAC.ACCOUNT_DESCRIPTION;
            }
            set
            {
                if (selectedBankAC.ACCOUNT_DESCRIPTION != value)
                {
                    selectedBankAC.ACCOUNT_DESCRIPTION = value;
                    OnPropertyChanged("ACCOUNT_DESCRIPTION");
                }
            }
        }
        private string _ACCOUNT_SEARCH_CODE { get; set; }
        public string ACCOUNT_SEARCH_CODE
        {
            get
            {
                return selectedBankAC.ACCOUNT_SEARCH_CODE;
            }
            set
            {
                if (selectedBankAC.ACCOUNT_SEARCH_CODE != value)
                {
                    selectedBankAC.ACCOUNT_SEARCH_CODE = value;
                    OnPropertyChanged("ACCOUNT_SEARCH_CODE");
                }
            }
        }
        private string _PRINTING_FORMAT { get; set; }
        public string PRINTING_FORMAT
        {
            get
            {
                return selectedBankAC.PRINTING_FORMAT;
            }
            set
            {
                if (selectedBankAC.PRINTING_FORMAT != value)
                {
                    selectedBankAC.PRINTING_FORMAT = value;
                    OnPropertyChanged("PRINTING_FORMAT");
                }
            }
        }
        public ObservableCollection<BankModel> _ListGrid { get; set; }
        public ObservableCollection<BankModel> ListGrid
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
        public List<BankAccountModel> _ListGridAc { get; set; }
        public List<BankAccountModel> ListGridAc
        {
            get
            {
                return _ListGridAc;
            }
            set
            {
                this._ListGridAc = value;
                OnPropertyChanged("ListGrid");
            }
        }
        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_Bank);
                }
                return _Cancel;
            }
        }



        public void Cancel_Bank()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        private string _SEARCH_BRANCH;
        public string SEARCH_BRANCH
        {
            get
            {
                return _SEARCH_BRANCH;
            }
            set
            {


                if (_SEARCH_BRANCH != value)
                {
                    _SEARCH_BRANCH = value;

                    if (_SEARCH_BRANCH != "" && _SEARCH_BRANCH != null)
                    {

                        GetBankAC(COMPANY_ID);
                    }
                    else
                    {
                        GetBankAC(COMPANY_ID);

                    }
                    OnPropertyChanged("SEARCH_BRANCH");
                }
            }
        }


        BankAccountModel[] acdata = null;
        public async Task<ObservableCollection<BankAccountModel>> GetBankAC(int comp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/BankAPI/GetBankAC?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    acdata = JsonConvert.DeserializeObject<BankAccountModel[]>(await response.Content.ReadAsStringAsync());
                    int x=0;
                    for (int i = 0; i < acdata.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp_ac.Add(new BankAccountModel
                        {
                            SLNO=x,
                            ACCOUNT_DESCRIPTION = acdata[i].ACCOUNT_DESCRIPTION,
                            ACCOUNT_NUMBER = acdata[i].ACCOUNT_NUMBER,
                            ACCOUNT_SEARCH_CODE = acdata[i].ACCOUNT_SEARCH_CODE,
                            BANK_ACCOUNT_ID = acdata[i].BANK_ACCOUNT_ID,
                            BANK_ID = acdata[i].BANK_ID,
                            BRANCH_NAME = acdata[i].BRANCH_NAME,
                            COMPANY_ID = acdata[i].COMPANY_ID,
                            IS_DELETE = acdata[i].IS_DELETE,
                            PRINTING_FORMAT = acdata[i].PRINTING_FORMAT,

                        });
                    }
                    if (SEARCH_BRANCH != "" && SEARCH_BRANCH != null)
                    {
                        var item1 = (from m in _ListGrid_Temp_ac where m.BRANCH_NAME.Contains(SEARCH_BRANCH) select m).ToList();

                        _ListGrid_Temp_ac = item1;
                    }
                   
                }
                ListGridAc = _ListGrid_Temp_ac;
                return new ObservableCollection<BankAccountModel>(_ListGrid_Temp_ac);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ObservableCollection<BankModel>> GetBank(int comp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/BankAPI/GetBank?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<BankModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new BankModel
                        {
                            SLNO=x,
                            ADDRESS_1 = data[i].ADDRESS_1,
                            ADDRESS_2 = data[i].ADDRESS_2,
                            BANK_CODE = data[i].BANK_CODE,
                            BANK_ID = data[i].BANK_ID,
                            BANK_NAME = data[i].BANK_NAME,
                            CITY = data[i].CITY,
                            COMPANY_ID = data[i].COMPANY_ID,
                            COUNTRY = data[i].COUNTRY,
                            EMAIL = data[i].EMAIL,
                            FAX_NUMBER = data[i].FAX_NUMBER,
                            IFSC_CODE = data[i].IFSC_CODE,
                            IS_DELETED = data[i].IS_DELETED,
                            MOBILE_NUMBER = data[i].MOBILE_NUMBER,
                            PHONE_NUMBER = data[i].PHONE_NUMBER,
                            PIN_CODE = data[i].PIN_CODE,
                            STATE = data[i].STATE,
                            WEBSITE = data[i].WEBSITE,
                        });
                    }
                    
                }
                ListGrid = _ListGrid_Temp;
                return new ObservableCollection<BankModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ICommand _InsertBank;
        public ICommand InsertBank
        {
            get
            {
                if (_InsertBank == null)
                {
                    _InsertBank = new DelegateCommand(Insert_Bank);
                }
                return _InsertBank;
            }
        }
        public async void Insert_Bank()
        {
            if (selectedBank.BANK_NAME==""||selectedBank.BANK_NAME==null)
            {
                MessageBox.Show("BANK NAME  is missing");
            }
            else if (selectedBank.IFSC_CODE == "" || selectedBank.IFSC_CODE == null)
            {
                MessageBox.Show("IFSC CODE  is missing");
            }
            else if (selectedBank.PIN_CODE == "" || selectedBank.PIN_CODE == null)
            {
                MessageBox.Show("PIN CODE  is missing");
            }
            else if (selectedBank.MOBILE_NUMBER == "" || selectedBank.MOBILE_NUMBER == null)
            {
                MessageBox.Show("MOBILE NUMBER  is missing");
            }
            else if (selectedBank.WEBSITE == "" || selectedBank.WEBSITE == null)
            {
                MessageBox.Show("WEBSITE  is missing");
            }
            else
            {

                selectedBank.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                var response = await client.PostAsJsonAsync("api/BankAPI/BankAdd/", selectedBank);
                if (response.StatusCode.ToString() == "OK")
                {

                    MessageBox.Show("Bank Added Successfully");
                    Cancel_Bank();
                    ModalService.NavigateTo(new BankList(), delegate(bool returnValue) { });
                }
            }
        }
        public ICommand _EditBank { get; set; }
        public ICommand EditBank
        {
            get
            {
                if (_EditBank == null)
                {
                    _EditBank = new DelegateCommand(Edit_Bank);
                }
                return _EditBank;
            }
        }
        public async void Edit_Bank()
        {
            if (selectedBank.BANK_ID != null && selectedBank.BANK_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/BankAPI/BankEdit?id=" + selectedBank.BANK_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<BankModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            selectedBank.ADDRESS_1 = data[i].ADDRESS_1;
                            selectedBank.ADDRESS_2 = data[i].ADDRESS_2;
                            selectedBank.BANK_CODE = data[i].BANK_CODE;
                            selectedBank.BANK_ID = data[i].BANK_ID;
                            selectedBank.BANK_NAME = data[i].BANK_NAME;
                            selectedBank.CITY = data[i].CITY;
                            selectedBank.COMPANY_ID = data[i].COMPANY_ID;
                            selectedBank.COUNTRY = data[i].COUNTRY;
                            selectedBank.EMAIL = data[i].EMAIL;
                            selectedBank.FAX_NUMBER = data[i].FAX_NUMBER;
                            selectedBank.IFSC_CODE = data[i].IFSC_CODE;
                            selectedBank.IS_DELETED = data[i].IS_DELETED;
                            selectedBank.MOBILE_NUMBER = data[i].MOBILE_NUMBER;
                            selectedBank.PHONE_NUMBER = data[i].PHONE_NUMBER;
                            selectedBank.PIN_CODE = data[i].PIN_CODE;
                            selectedBank.STATE = data[i].STATE;
                            selectedBank.WEBSITE = data[i].WEBSITE;
                        }
                        App.Current.Properties["BankEdit"] = selectedBank;
                        BankAccountList IA = new BankAccountList();
                        IA.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Bank first", "Bank Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        public ICommand _ViewBank { get; set; }
        public ICommand ViewBank
        {
            get
            {
                if (_ViewBank == null)
                {
                    _ViewBank = new DelegateCommand(View_Bank);
                }
                return _ViewBank;
            }
        }

        public async void View_Bank()
        {
            if (selectedBank.BANK_ID != null && selectedBank.BANK_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/BankAPI/BankEdit?id=" + selectedBank.BANK_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<BankModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            selectedBank.ADDRESS_1 = data[i].ADDRESS_1;
                            selectedBank.ADDRESS_2 = data[i].ADDRESS_2;
                            selectedBank.BANK_CODE = data[i].BANK_CODE;
                            selectedBank.BANK_ID = data[i].BANK_ID;
                            selectedBank.BANK_NAME = data[i].BANK_NAME;
                            selectedBank.CITY = data[i].CITY;
                            selectedBank.COMPANY_ID = data[i].COMPANY_ID;
                            selectedBank.COUNTRY = data[i].COUNTRY;
                            selectedBank.EMAIL = data[i].EMAIL;
                            selectedBank.FAX_NUMBER = data[i].FAX_NUMBER;
                            selectedBank.IFSC_CODE = data[i].IFSC_CODE;
                            selectedBank.IS_DELETED = data[i].IS_DELETED;
                            selectedBank.MOBILE_NUMBER = data[i].MOBILE_NUMBER;
                            selectedBank.PHONE_NUMBER = data[i].PHONE_NUMBER;
                            selectedBank.PIN_CODE = data[i].PIN_CODE;
                            selectedBank.STATE = data[i].STATE;
                            selectedBank.WEBSITE = data[i].WEBSITE;
                        }
                        App.Current.Properties["BankEdit"] = selectedBank;
                        BankAccountView IA = new BankAccountView();
                        IA.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Bank first", "Bank Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        public ICommand _UpdateBank;
        public ICommand UpdateBank
        {
            get
            {
                if (_UpdateBank == null)
                {
                    _UpdateBank = new DelegateCommand(Update_Bank);
                }
                return _UpdateBank;
            }
        }


        public async void Update_Bank()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/BankAPI/BankUpdate/", selectedBank);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Bank Update Successfully");
                Cancel_Bank();
                ModalService.NavigateTo(new BankList(), delegate(bool returnValue) { });
            }

        }
        public ICommand _DeleteBank;
        public ICommand DeleteBank
        {
            get
            {
                if (_DeleteBank == null)
                {
                    _DeleteBank = new DelegateCommand(Delete_Bank);
                }
                return _DeleteBank;
            }
        }
        public async void Delete_Bank()
        {
            if (selectedBank.BANK_ID != null && selectedBank.BANK_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Bank " + selectedBank.BANK_NAME + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var id = selectedBank.BANK_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/BankAPI/DeleteBank?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Bank Delete Successfully");
                        ModalService.NavigateTo(new BankList(), delegate(bool returnValue) { });
                    }
                }
                else
                {
                    Cancel_Bank();
                }
            }
            else
            {
                MessageBox.Show("Select Bank");
            }
        }
        private ICommand _BankAccountAdd { get; set; }
        public ICommand BankAccountAdd
        {
            get
            {
                if (_BankAccountAdd == null)
                {
                    _BankAccountAdd = new DelegateCommand(BankAccount_Add);
                }
                return _BankAccountAdd;
            }

        }

        public void BankAccount_Add()
        {
            BankAccountList _BankAccountList = new BankAccountList();
            _BankAccountList.Show();
        }
        private ICommand _BankAdd { get; set; }
        public ICommand BankAdd
        {
            get
            {
                if (_BankAdd == null)
                {
                    _BankAdd = new DelegateCommand(Bank_Add);
                }
                return _BankAdd;
            }

        }

        public void Bank_Add()
        {
            if (selectedBank.BANK_ID != null && selectedBank.BANK_ID != 0)
            {
                App.Current.Properties["BankId"] = selectedBank.BANK_ID;
                AddAccount _BankAccountList = new AddAccount();
                _BankAccountList.Show();
            }
            else
                MessageBox.Show("Please Select A Bank To Add Account..", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private SuppPaymentModel _selectedItem;
        public SuppPaymentModel SelectedItem
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
            var fdrt = App.Current.Properties["OppingDiffProperties"] as OpeningStockModel;
            SelectedItem.CHEQUE_BANK_BRANCH = selectedBankAC.BRANCH_NAME;
            SelectedItem.CHEQUE_BANK_AC = Convert.ToInt64(selectedBankAC.ACCOUNT_NUMBER);
            if (App.Current.Properties["ChequeBankBranch"] != null)
            {
                SuppPayAdd.ChequeBranchRef.Text = null;
                SuppPayAdd.ChequeBranchRef.Text = selectedBankAC.BRANCH_NAME;
                App.Current.Properties["ChequeBankBranch"] = null;
            }
            if (App.Current.Properties["ChequeBankACClick"] != null)
            {
                SuppPayAdd.ChequeAcRef.Text = null;
                SuppPayAdd.ChequeAcRef.Text = SelectedItem.CHEQUE_BANK_AC.ToString();
                App.Current.Properties["ChequeBankACClick"] = null;
            }
            if (App.Current.Properties["BankACClick"] != null)
            {
                SuppPayAdd.BankAcRef.Text = null;
                SuppPayAdd.BankAcRef.Text = SelectedItem.CHEQUE_BANK_AC.ToString();
                App.Current.Properties["BankACClick"] = null;
            }
            if (App.Current.Properties["BankBranchClick"] != null)
            {
                SuppPayAdd.BankBranchRef.Text = null;
                SuppPayAdd.BankBranchRef.Text = selectedBankAC.BRANCH_NAME;
                App.Current.Properties["BankBranchClick"] = null;
            }
            if (App.Current.Properties["FinancialACClick"] != null)
            {
                SuppPayAdd.FinancialAcRef.Text = null;
                SuppPayAdd.FinancialAcRef.Text = SelectedItem.CHEQUE_BANK_AC.ToString();
                App.Current.Properties["FinancialACClick"] = null;
            }
            if (App.Current.Properties["InvoiceChequeBankBranchReff"] != null)
            {
                PayNow.InvoiceChequeBankBranchReff.Text = null;
                PayNow.InvoiceChequeBankBranchReff.Text = selectedBankAC.BRANCH_NAME;
                App.Current.Properties["InvoiceChequeBankBranchReff"] = null;
            }
            if (App.Current.Properties["InvoiceChequeBankAcReff"] != null)
            {
                PayNow.InvoiceChequeBankAcReff.Text = null;
                PayNow.InvoiceChequeBankAcReff.Text = SelectedItem.CHEQUE_BANK_AC.ToString();
                App.Current.Properties["InvoiceChequeBankAcReff"] = null;
            }
            if (App.Current.Properties["InvoicetransferBankAcReff"] != null)
            {
                PayNow.InvoicetransferBankAcReff.Text = null;
                PayNow.InvoicetransferBankAcReff.Text = SelectedItem.CHEQUE_BANK_AC.ToString();
                App.Current.Properties["InvoicetransferBankAcReff"] = null;
            }
            if (App.Current.Properties["InvoicetransferBankBranchReff"] != null)
            {
                PayNow.InvoicetransferBankBranchReff.Text = null;
                PayNow.InvoicetransferBankBranchReff.Text = selectedBankAC.BRANCH_NAME;
                App.Current.Properties["InvoicetransferBankBranchReff"] = null;
            }
            if (App.Current.Properties["InvoiceFinancialBankAccountReff"] != null)
            {
                PayNow.InvoiceFinancialBankAccountReff.Text = null;
                PayNow.InvoiceFinancialBankAccountReff.Text = SelectedItem.CHEQUE_BANK_AC.ToString();
                App.Current.Properties["InvoiceFinancialBankAccountReff"] = null;
            }
            Cancel_Bank();
            // ItemViewModel iv=new ItemViewModel();
        }

        public ICommand _InsertBankAC;
        public ICommand InsertBankAC
        {
            get
            {
                if (_InsertBankAC == null)
                {
                    _InsertBankAC = new DelegateCommand(Insert_BankAC);
                }
                return _InsertBankAC;
            }
        }
        public async void Insert_BankAC()
        {
            if (selectedBankAC.ACCOUNT_NUMBER == "" || selectedBankAC.ACCOUNT_NUMBER == null)
            {
                MessageBox.Show("ACCOUNT NUMBER is missing");
            }
            else if (selectedBankAC.ACCOUNT_SEARCH_CODE == "" || selectedBankAC.ACCOUNT_SEARCH_CODE == null)
            {
                MessageBox.Show("ACCOUNT SEARCH CODE  is missing");
            }
            else if (selectedBankAC.BRANCH_NAME == "" || selectedBankAC.BRANCH_NAME == null)
            {
                MessageBox.Show("BRANCH NAME  is missing");
            }
            else
            {
                selectedBankAC.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                

                    selectedBankAC.BANK_ID = Convert.ToInt64(App.Current.Properties["BankId"]);

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    var response = await client.PostAsJsonAsync("api/BankAPI/BankAddAC/", selectedBankAC);
                    if (response.StatusCode.ToString() == "OK")
                    {

                        MessageBox.Show("Bank Account Added Successfully");
                        Cancel_Bank();
                        ModalService.NavigateTo(new BankList(), delegate(bool returnValue) { });
                    }
                    App.Current.Properties["BankId"] = null;
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
