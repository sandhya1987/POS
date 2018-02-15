using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Unit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using InvoicePOS.UserControll.Item;

namespace InvoicePOS.ViewModels
{
    public class UnitViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        UnitModel _UnitModel = new UnitModel();
        UnitModel[] data = null;
        List<UnitModel> _ListGrid_Temp = new List<UnitModel>();
        public UnitViewModel()
        {
            SelectedItem = new ItemModel();
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
               
                SelectedUnit = App.Current.Properties["UnitEdit"] as UnitModel;
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                SelectedUnit = App.Current.Properties["UnitView"] as UnitModel;
                App.Current.Properties["Action"] = "";
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                SelectedUnit = new UnitModel();
                GetUnit(comp);
            }


        }
        public ICommand _AddUnitClick;
        public ICommand AddUnitClick
        {
            get
            {
                if (_AddUnitClick == null)
                {
                    _AddUnitClick = new DelegateCommand(AddUnit_Click);
                }
                return _AddUnitClick;
            }
        }

        public void AddUnit_Click()
        {
            UnitAdd IA = new UnitAdd();
            IA.Show();

        }



        private UnitModel _selectedunit;
        public UnitModel SelectedUnit
        {
            get { return _selectedunit; }
            set
            {
                if (_selectedunit != value)
                {
                    _selectedunit = value;
                    OnPropertyChanged("SelectedUnit");
                }
            }
        }
        private string _MEASURING_NAME;
        public string MEASURING_NAME
        {
            get
            {
                return SelectedUnit.MEASURING_NAME;
            }
            set
            {
                SelectedUnit.MEASURING_NAME = value;

                if (SelectedUnit.MEASURING_NAME != value)
                {
                    SelectedUnit.MEASURING_NAME = value;
                    OnPropertyChanged("MEASURING_NAME");
                }
            }
        }
        private string _SHORT_INDAX;
        public string SHORT_INDAX
        {
            get
            {
                return SelectedUnit.SHORT_INDAX;
            }
            set
            {
                SelectedUnit.SHORT_INDAX = value;

                if (SelectedUnit.SHORT_INDAX != value)
                {
                    SelectedUnit.SHORT_INDAX = value;
                    OnPropertyChanged("SHORT_INDAX");
                }
            }
        }
        private string _IMAGE_PATH;
        public string IMAGE_PATH
        {
            get
            {
                return SelectedUnit.IMAGE_PATH;
            }
            set
            {
                SelectedUnit.IMAGE_PATH = value;

                if (SelectedUnit.IMAGE_PATH != value)
                {
                    SelectedUnit.IMAGE_PATH = value;
                    OnPropertyChanged("IMAGE_PATH");
                }
            }
        }
        private bool _IS_DELETE;
        public bool IS_DELETE
        {
            get
            {
                return SelectedUnit.IS_DELETE;
            }
            set
            {
                SelectedUnit.IS_DELETE = value;

                if (SelectedUnit.IS_DELETE != value)
                {
                    SelectedUnit.IS_DELETE = value;
                    OnPropertyChanged("IS_DELETE");
                }
            }
        }
        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedUnit.COMPANY_ID;
            }
            set
            {
                SelectedUnit.COMPANY_ID = value;

                if (SelectedUnit.COMPANY_ID != value)
                {
                    SelectedUnit.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }
        private long _UNIT_ID;
        public long UNIT_ID
        {
            get
            {
                return SelectedUnit.UNIT_ID;
            }
            set
            {
                SelectedUnit.UNIT_ID = value;

                if (SelectedUnit.UNIT_ID != value)
                {
                    SelectedUnit.UNIT_ID = value;
                    OnPropertyChanged("UNIT_ID");
                }
            }
        }
       
        List<UnitAutoListModel> unitList = new List<UnitAutoListModel>();
      

        public async Task<ObservableCollection<UnitModel>> GetUnit(long comp)
        {
            unitList = new List<UnitAutoListModel>();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/UnitAPI/GetUnit?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<UnitModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;

                        _ListGrid_Temp.Add(new UnitModel
                        {
                            SLNO = x,
                            COMPANY_ID = data[i].COMPANY_ID,
                            IMAGE_PATH = data[i].IMAGE_PATH,
                            IS_DELETE = data[i].IS_DELETE,
                            MEASURING_NAME = data[i].MEASURING_NAME,
                            SHORT_INDAX = data[i].SHORT_INDAX,
                            UNIT_ID = data[i].UNIT_ID,
                        });
                        unitList.Add(new UnitAutoListModel
                        {
                            DisplayName = data[i].MEASURING_NAME,
                            DisplayId = data[i].UNIT_ID
                        });
                    }
                    if (SEARCH_UNIT != "" && SEARCH_UNIT != null)
                    {
                        var item1 = (from m in _ListGrid_Temp where m.MEASURING_NAME.Contains(SEARCH_UNIT) select m).ToList();
                        _ListGrid_Temp = item1;
                    }
                }
                App.Current.Properties["unitAutoList"] = unitList;
                ListGrid = _ListGrid_Temp;
                return new ObservableCollection<UnitModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private string _SEARCH_UNIT;
        public string SEARCH_UNIT
        {
            get
            {
                return _SEARCH_UNIT;
            }
            set
            {


                if (_SEARCH_UNIT != value)
                {
                    _SEARCH_UNIT = value;

                    if (_SEARCH_UNIT != "" && _SEARCH_UNIT != null)
                    {

                        GetUnit(COMPANY_ID);
                    }
                    else
                    {
                        GetUnit(COMPANY_ID);

                    }
                    OnPropertyChanged("_SEARCH_UNIT");
                }
            }
        }
        public ICommand _InsertUnit;
        public ICommand InsertUnit
        {
            get
            {
                if (_InsertUnit == null)
                {
                    _InsertUnit = new DelegateCommand(Insert_Unit);
                }
                return _InsertUnit;
            }
        }



        public async void Insert_Unit()
        {
            SelectedUnit.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/UnitAPI/UnitAdd/", SelectedUnit);
            if (response.StatusCode.ToString() == "OK")
            {

                MessageBox.Show("Item Added Successfully");
                Cancel_Unit();
                ModalService.NavigateTo(new UnitList(), delegate(bool returnValue) { });
            }
        }
        public ICommand _EditUnit { get; set; }
        public ICommand EditUnit
        {
            get
            {
                if (_EditUnit == null)
                {
                    _EditUnit = new DelegateCommand(Edit_Unit);
                }
                return _EditUnit;
            }
        }

        private List<UnitModel> _UnitData;
        public List<UnitModel> UnitData
        {
            get { return _UnitData; }
            set
            {
                if (_UnitData != value)
                {
                    _UnitData = value;
                }
            }
        }
        public async void Edit_Unit()
        {
            if (SelectedUnit.UNIT_ID != null && SelectedUnit.UNIT_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/UnitAPI/UnitEdit?id=" + SelectedUnit.UNIT_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<UnitModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedUnit.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedUnit.IMAGE_PATH = data[i].IMAGE_PATH;
                            SelectedUnit.IS_DELETE = data[i].IS_DELETE;
                            SelectedUnit.MEASURING_NAME = data[i].MEASURING_NAME;
                            SelectedUnit.SHORT_INDAX = data[i].SHORT_INDAX;
                            SelectedUnit.UNIT_ID = data[i].UNIT_ID;
                        }
                        App.Current.Properties["UnitEdit"] = SelectedUnit;
                        UnitAdd IA = new UnitAdd();
                        IA.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select unit first", "Unit Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
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
            var fdrt = App.Current.Properties["ItemDiffProperties"] as ItemModel;
            //SelectedItem.CATEGORY_NAME = fdrt.CATEGORY_NAME; 
            //SelectedItem.TAX_PAID = fdrt.TAX_PAID;
            //SelectedItem.TAX_COLLECTED = fdrt.TAX_COLLECTED;
            SelectedItem.PURCHASE_UNIT = SelectedUnit.MEASURING_NAME;
            SelectedItem.SALES_UNIT = SelectedUnit.MEASURING_NAME;
            App.Current.Properties["ItemDiffProperties"] = SelectedItem;

            if (App.Current.Properties["UnitPurchaseClick"]!=null)
            {
                ItemAdd.UnitRef.Text = null;
                ItemAdd.UnitRef.Text = Convert.ToString(SelectedItem.PURCHASE_UNIT);
                App.Current.Properties["UnitPurchaseClick"] = SelectedUnit.UNIT_ID;
            }
            if (App.Current.Properties["UnitSalesClick"]!=null)
            {
                ItemAdd.SaleUnitRef.Text = null;
                ItemAdd.SaleUnitRef.Text = Convert.ToString(SelectedItem.SALES_UNIT);
                App.Current.Properties["UnitSalesClick"] = SelectedUnit.UNIT_ID;
            }
            //ItemAdd.CatRef.Text = null;
            //ItemAdd.CatRef.Text = SelectedItem.CATEGORY_NAME;

            //ItemAdd.TaxRef1.Text = null;
            //ItemAdd.TaxRef1.Text = Convert.ToString(SelectedItem.TAX_COLLECTED);

            //ItemAdd.TaxRef2.Text = null;
            //ItemAdd.TaxRef2.Text = Convert.ToString(SelectedItem.TAX_PAID);


            
            Cancel_Unit();
        }

        public ICommand _ViewUnit { get; set; }
        public ICommand ViewUnit
        {
            get
            {
                if (_ViewUnit == null)
                {
                    _ViewUnit = new DelegateCommand(View_Unit);
                }
                return _ViewUnit;
            }
        }
        public async void View_Unit()
        {
            if (SelectedUnit.UNIT_ID != null && SelectedUnit.UNIT_ID != 0)
            {
                App.Current.Properties["Action"] = "View";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/UnitAPI/UnitEdit?id=" + SelectedUnit.UNIT_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<UnitModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedUnit.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedUnit.IMAGE_PATH = data[i].IMAGE_PATH;
                            SelectedUnit.IS_DELETE = data[i].IS_DELETE;
                            SelectedUnit.MEASURING_NAME = data[i].MEASURING_NAME;
                            SelectedUnit.SHORT_INDAX = data[i].SHORT_INDAX;
                            SelectedUnit.UNIT_ID = data[i].UNIT_ID;
                        }
                        App.Current.Properties["UnitView"] = SelectedUnit;
                        UnitView IA = new UnitView();
                        IA.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select unit first", "Unit Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }





        public ICommand _UpdateUnit;
        public ICommand UpdateUnit
        {
            get
            {
                if (_UpdateUnit == null)
                {
                    _UpdateUnit = new DelegateCommand(Update_Unit);
                }
                return _UpdateUnit;
            }
        }


        public async void Update_Unit()
        {
            HttpClient client = new HttpClient();
            //UnitData = new List<UnitModel>();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/UnitAPI/UnitUpdate/", SelectedUnit);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Unit Update Successfully");
                Cancel_Unit();
                ModalService.NavigateTo(new UnitList(), delegate(bool returnValue) { });
            }

        }

        public ICommand _DeleteUnit;
        public ICommand DeleteUnit
        {
            get
            {
                if (_DeleteUnit == null)
                {
                    _DeleteUnit = new DelegateCommand(Delete_unit);
                }
                return _DeleteUnit;
            }
        }
        public async void Delete_unit()
        {
            if (SelectedUnit.UNIT_ID != null && SelectedUnit.UNIT_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Unit " + SelectedUnit.MEASURING_NAME + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    var id = SelectedUnit.UNIT_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/UnitAPI/DeleteUnit?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Unit Delete Successfully");
                        ModalService.NavigateTo(new UnitList(), delegate(bool returnValue) { });

                    }
                }
                else
                {
                    Cancel_Unit();
                }
            }
            else
            {
                MessageBox.Show("Select Unit");
            }

        }
        public List<UnitModel> _ListGrid { get; set; }
        public List<UnitModel> ListGrid
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

        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_Unit);
                }
                return _Cancel;
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

        public void Cancel_Unit()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
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
