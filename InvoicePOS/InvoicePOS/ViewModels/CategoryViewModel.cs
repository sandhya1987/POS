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
using InvoicePOS.UserControll.Category;
using InvoicePOS.UserControll.Item;


namespace InvoicePOS.ViewModels
{
    public class CategoryViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {

        CategoryModel[] data = null;
        List<CategoryModel> _ListGrid_Catagory = new List<CategoryModel>();
        public CategoryViewModel()
        {
            SelectedCatagory = new CategoryModel();
            SelectedItem = new ItemModel();
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedCatagory = App.Current.Properties["CatagoryEdit"] as CategoryModel;
                App.Current.Properties["Action"] = "";

            }
            else if (App.Current.Properties["Action"] == "View")
            {
                SelectedCatagory = App.Current.Properties["CatagoryView"] as CategoryModel;
                App.Current.Properties["Action"] = "";
            }
            else
            {
                //App.Current.Properties["ItemDiffProperties"] = SelectedItem;
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                
                GetCatagory(comp);
            }

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





        public ICommand _UpdateCatagory { get; set; }
        public ICommand UpdateCatagory
        {
            get
            {
                if (_UpdateCatagory == null)
                {

                    _UpdateCatagory = new DelegateCommand(Update_Catagory);
                }
                return _UpdateCatagory;
            }
        }

        public async void Update_Catagory()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            SelectedCatagory.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            var response = await client.PostAsJsonAsync("api/CatagoryAPI/CatagoryUpdate/", SelectedCatagory);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Catagory Update Successfully");
                Cancel_Catagory();
                ModalService.NavigateTo(new CategoryList(), delegate(bool returnValue) { });
            }
        }
        public ICommand _InsertCatagory { get; set; }
        public ICommand InsertCatagory
        {
            get
            {
                if (_InsertCatagory == null)
                {

                    _InsertCatagory = new DelegateCommand(Add_Catagory);
                }
                return _InsertCatagory;
            }
        }

        public async void Add_Catagory()
        {
            if (SelectedCatagory.CATAGORY_NAME == "" || SelectedCatagory.CATAGORY_NAME == null)
            {
                MessageBox.Show("Catagory Name is missing");
            }
           
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                SelectedCatagory.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                var response = await client.PostAsJsonAsync("api/CatagoryAPI/CatagoryAdd/", SelectedCatagory);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Catagory insert Successfully");
                    Cancel_Catagory();
                    ModalService.NavigateTo(new CategoryList(), delegate(bool returnValue) { });
                    //GetCatagory(SelectedCatagory.COMPANY_ID);
                    //Window_CatagoryList.CatGridRef.ItemsSource = null;
                    // Main.ListGridRef.ClearValue();
                    //Window_CatagoryList.CatGridRef.ItemsSource = _ListGrid_Catagory.ToList();
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
                    _Cancel = new DelegateCommand(Cancel_Catagory);
                }
                return _Cancel;
            }
        }



        public void Cancel_Catagory()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
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
            
            App.Current.Properties["ItemDiffProperties"] = SelectedItem;
           
            if (App.Current.Properties["CatagoryItemReff"] != null)
            {
                ItemAdd.CatRef.Text = null;
                ItemAdd.CatRef.Text = SelectedCatagory.CATAGORY_NAME;
                App.Current.Properties["CatagoryItemReff"] = SelectedCatagory.CATAGORY_ID;
                SelectedItem.CATAGORY_ID = SelectedCatagory.CATAGORY_ID;
            }
            Cancel_Catagory();
           
        }
        private ICommand _ViewCatagoryClick { get; set; }
        public ICommand ViewCatagoryClick
        {
            get
            {
                if (_ViewCatagoryClick == null)
                {
                    _ViewCatagoryClick = new DelegateCommand(ViewCatagory_Click);
                }
                return _ViewCatagoryClick;
            }

        }
        public void ViewCatagory_Click()
        {
            if (App.Current.Properties["Action"] == "View")
            {
                SelectedCatagory = App.Current.Properties["GodownView"] as CategoryModel;
                CatagoryView _AGD = new CatagoryView();
                _AGD.Show();
            }
            else
            {
                CatagoryView _AGD = new CatagoryView();
                _AGD.Show();
            }

        }
        public ICommand _ViewCategoryClick { get; set; }
        public ICommand ViewCategoryClick
        {
            get
            {
                if (_ViewCategoryClick == null)
                {

                    _ViewCategoryClick = new DelegateCommand(View_Catagory);
                }
                return _ViewCategoryClick;
            }
        }

        public async void View_Catagory()
        {
            try
            {
                if (SelectedCatagory.CATAGORY_ID != null && SelectedCatagory.CATAGORY_ID != 0)
                {
                    data = null;
                    App.Current.Properties["Action"] = "View";
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    HttpResponseMessage response = client.GetAsync("api/CatagoryAPI/CatagoryEdit?id=" + SelectedCatagory.CATAGORY_ID + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        data = JsonConvert.DeserializeObject<CategoryModel[]>(await response.Content.ReadAsStringAsync());
                        if (data.Length > 0)
                        {
                            for (int i = 0; i < data.Length; i++)
                            {
                                SelectedCatagory.BAR_CODE_PREFIX = data[i].BAR_CODE_PREFIX;
                                SelectedCatagory.CATAGORY_DEC = data[i].CATAGORY_DEC;
                                SelectedCatagory.CATAGORY_ID = data[i].CATAGORY_ID;
                                SelectedCatagory.CATAGORY_NAME = data[i].CATAGORY_NAME;
                                SelectedCatagory.COMPANY_ID = data[i].COMPANY_ID;
                                SelectedCatagory.DISPLAY_INDEX = data[i].DISPLAY_INDEX;
                                SelectedCatagory.IS_DELETE = data[i].IS_DELETE;
                                SelectedCatagory.IS_NOT_PROTAL = data[i].IS_NOT_PROTAL;
                            }
                            App.Current.Properties["CatagoryView"] = SelectedCatagory;
                            ViewCatagory_Click();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Select Catagory first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ICommand _SearchCategoryClick { get; set; }
        public ICommand SearchCategoryClick
        {
            get
            {
                if (_SearchCategoryClick == null)
                {

                    _SearchCategoryClick = new DelegateCommand(Search_Catagory);
                }
                return _SearchCategoryClick;
            }
        }

        public async void Search_Catagory()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/CatagoryAPI/SearchCatagory?name=" + SelectedCatagory.SEARCH_CATAGORY + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<CategoryModel[]>(await response.Content.ReadAsStringAsync());
                for (int i = 0; i < data.Length; i++)
                {
                    _ListGrid_Catagory.Add(new CategoryModel
                    {
                        BAR_CODE_PREFIX = data[i].BAR_CODE_PREFIX,
                        CATAGORY_DEC = data[i].CATAGORY_DEC,
                        CATAGORY_ID = data[i].CATAGORY_ID,
                        CATAGORY_NAME = data[i].CATAGORY_NAME,
                        COMPANY_ID = data[i].COMPANY_ID,
                        DISPLAY_INDEX = data[i].DISPLAY_INDEX,
                        IS_DELETE = data[i].IS_DELETE,
                        IS_NOT_PROTAL = data[i].IS_NOT_PROTAL,

                    });
                }
            }
            ListGrid = _ListGrid_Catagory;
        }



        public ICommand _EditCatagory { get; set; }
        public ICommand EditCatagory
        {
            get
            {
                if (_EditCatagory == null)
                {

                    _EditCatagory = new DelegateCommand(Edit_Catagory);
                }
                return _EditCatagory;
            }
        }

        public async void Edit_Catagory()
        {
            try
            {
                if (SelectedCatagory.CATAGORY_ID != null && SelectedCatagory.CATAGORY_ID != 0)
                {
                    data = null;
                    App.Current.Properties["Action"] = "Edit";
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    HttpResponseMessage response = client.GetAsync("api/CatagoryAPI/CatagoryEdit?id=" + SelectedCatagory.CATAGORY_ID + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        data = JsonConvert.DeserializeObject<CategoryModel[]>(await response.Content.ReadAsStringAsync());
                        if (data.Length > 0)
                        {
                            for (int i = 0; i < data.Length; i++)
                            {
                                SelectedCatagory.BAR_CODE_PREFIX = data[i].BAR_CODE_PREFIX;
                                SelectedCatagory.CATAGORY_DEC = data[i].CATAGORY_DEC;
                                SelectedCatagory.CATAGORY_ID = data[i].CATAGORY_ID;
                                SelectedCatagory.CATAGORY_NAME = data[i].CATAGORY_NAME;
                                SelectedCatagory.COMPANY_ID = data[i].COMPANY_ID;
                                SelectedCatagory.DISPLAY_INDEX = data[i].DISPLAY_INDEX;
                                SelectedCatagory.IS_DELETE = data[i].IS_DELETE;
                                SelectedCatagory.IS_NOT_PROTAL = data[i].IS_NOT_PROTAL;
                            }
                            App.Current.Properties["CatagoryEdit"] = SelectedCatagory;
                            CategoryAdd _CategoryAdd = new CategoryAdd();
                            _CategoryAdd.Show();
                        }

                    }
                }
                else
                {

                    MessageBox.Show("Select Catagory first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ObservableCollection<CategoryModel>> GetCatagory(long comp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CatagoryAPI/GetCatagory?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CategoryModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Catagory.Add(new CategoryModel
                        {
                            SLNO = x,
                            BAR_CODE_PREFIX = data[i].BAR_CODE_PREFIX,
                            CATAGORY_DEC = data[i].CATAGORY_DEC,
                            CATAGORY_ID = data[i].CATAGORY_ID,
                            CATAGORY_NAME = data[i].CATAGORY_NAME,
                            COMPANY_ID = data[i].COMPANY_ID,
                            DISPLAY_INDEX = data[i].DISPLAY_INDEX,
                            IS_DELETE = data[i].IS_DELETE,
                            IS_NOT_PROTAL = data[i].IS_NOT_PROTAL,

                        });
                    }
                    if (SEARCH_CATAGORY != "" && SEARCH_CATAGORY != null)
                    {
                        var item1 = (from m in _ListGrid_Catagory where m.CATAGORY_NAME.Contains(SEARCH_CATAGORY) select m).ToList();
                        _ListGrid_Catagory = item1;
                    }
                }
                ListGrid = _ListGrid_Catagory;
                App.Current.Properties["CategoryList"] = ListGrid;
                return new ObservableCollection<CategoryModel>(_ListGrid_Catagory);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<CategoryModel> _ListGrid { get; set; }
        public List<CategoryModel> ListGrid
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

        private CategoryModel _SelectedCatagory;
        public CategoryModel SelectedCatagory
        {
            get { return _SelectedCatagory; }
            set
            {
                if (_SelectedCatagory != value)
                {
                    _SelectedCatagory = value;
                    OnPropertyChanged("_SelectedCatagory");
                }
            }
        }
        public void AddCategory_Click()
        {
            CategoryAdd _CategoryAdd = new CategoryAdd();
            _CategoryAdd.Show();
        }
        private ICommand _AddCategoryClick { get; set; }
        public ICommand AddCategoryClick
        {
            get
            {
                if (_AddCategoryClick == null)
                {
                    _AddCategoryClick = new DelegateCommand(AddCategory_Click);
                }
                return _AddCategoryClick;
            }

        }
        public ICommand _DeleteCatagory;
        public ICommand DeleteCatagory
        {
            get
            {
                if (_DeleteCatagory == null)
                {
                    _DeleteCatagory = new DelegateCommand(Delete_Catagory);
                }
                return _DeleteCatagory;
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

        public async void Delete_Catagory()
        {
            if (SelectedCatagory.CATAGORY_ID != null && SelectedCatagory.CATAGORY_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Catagory " + SelectedCatagory.CATAGORY_NAME + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var id = SelectedCatagory.CATAGORY_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/CatagoryAPI/DeleteCatagory?id=" + SelectedCatagory.CATAGORY_ID + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Catagory Delete Successfully");
                        ModalService.NavigateTo(new CategoryList(), delegate(bool returnValue) { });

                    }
                }
                else
                {
                    Cancel_Catagory();
                }
            }
            else
            {
                MessageBox.Show("Select Catagory");
            }

        }

        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedCatagory.COMPANY_ID;
            }
            set
            {
                SelectedCatagory.COMPANY_ID = value;


                if (SelectedCatagory.COMPANY_ID != value)
                {
                    SelectedCatagory.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }
        private long _CATAGORY_ID;
        public long CATAGORY_ID
        {
            get
            {
                return SelectedCatagory.CATAGORY_ID;
            }
            set
            {
                SelectedCatagory.CATAGORY_ID = value;


                if (SelectedCatagory.CATAGORY_ID != value)
                {
                    SelectedCatagory.CATAGORY_ID = value;
                    OnPropertyChanged("CATAGORY_ID");
                }
            }
        }
        private string _CATAGORY_NAME;
        public string CATAGORY_NAME
        {
            get
            {
                return SelectedCatagory.CATAGORY_NAME;
            }
            set
            {
                SelectedCatagory.CATAGORY_NAME = value;


                if (SelectedCatagory.CATAGORY_NAME != value)
                {
                    SelectedCatagory.CATAGORY_NAME = value;
                    OnPropertyChanged("CATAGORY_NAME");
                }
            }
        }
        private string _SEARCH_CATAGORY;
        public string SEARCH_CATAGORY
        {
            get
            {
                return _SEARCH_CATAGORY;
            }
            set
            {
                if (SelectedCatagory.SEARCH_CATAGORY != value)
                {
                    _SEARCH_CATAGORY = value;

                    if (_SEARCH_CATAGORY != "" && _SEARCH_CATAGORY != null)
                    {

                        GetCatagory(COMPANY_ID);
                    }
                    else
                    {
                        GetCatagory(COMPANY_ID);

                    }
                    OnPropertyChanged("SEARCH_ITEM");
                }
            }
        }
        private string _BAR_CODE_PREFIX;
        public string BAR_CODE_PREFIX
        {
            get
            {
                return SelectedCatagory.BAR_CODE_PREFIX;
            }
            set
            {
                SelectedCatagory.BAR_CODE_PREFIX = value;


                if (SelectedCatagory.BAR_CODE_PREFIX != value)
                {
                    SelectedCatagory.BAR_CODE_PREFIX = value;
                    OnPropertyChanged("BAR_CODE_PREFIX");
                }
            }
        }
        private string _CATAGORY_DEC;
        public string CATAGORY_DEC
        {
            get
            {
                return SelectedCatagory.CATAGORY_DEC;
            }
            set
            {
                SelectedCatagory.CATAGORY_DEC = value;


                if (SelectedCatagory.CATAGORY_DEC != value)
                {
                    SelectedCatagory.CATAGORY_DEC = value;
                    OnPropertyChanged("CATAGORY_DEC");
                }
            }
        }
        private string _DISPLAY_INDEX;
        public string DISPLAY_INDEX
        {
            get
            {
                return SelectedCatagory.DISPLAY_INDEX;
            }
            set
            {
                SelectedCatagory.DISPLAY_INDEX = value;


                if (SelectedCatagory.DISPLAY_INDEX != value)
                {
                    SelectedCatagory.DISPLAY_INDEX = value;
                    OnPropertyChanged("DISPLAY_INDEX");
                }
            }
        }
        private bool _IS_NOT_PROTAL;
        public bool IS_NOT_PROTAL
        {
            get
            {
                return SelectedCatagory.IS_NOT_PROTAL;
            }
            set
            {
                SelectedCatagory.IS_NOT_PROTAL = value;


                if (SelectedCatagory.IS_NOT_PROTAL != value)
                {
                    SelectedCatagory.IS_NOT_PROTAL = value;
                    OnPropertyChanged("IS_NOT_PROTAL");
                }
            }
        }
        private bool _IS_DELETE;
        public bool IS_DELETE
        {
            get
            {
                return SelectedCatagory.IS_DELETE;
            }
            set
            {
                SelectedCatagory.IS_DELETE = value;
                if (SelectedCatagory.IS_DELETE != value)
                {
                    SelectedCatagory.IS_DELETE = value;
                    OnPropertyChanged("IS_DELETE");
                }
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
    }
}
