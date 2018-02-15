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
using InvoicePOS.UserControll.Item;
using InvoicePOS.UserControll.SalesReturn;
using InvoicePOS.UserControll.StockLedger;

namespace InvoicePOS.ViewModels
{
    public class GodownViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {



        GodownModel[] data = null;
        public GodownViewModel()
        {
            var COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            SelectedOpeningStock = new OpeningStockModel();
            if (App.Current.Properties["Action"] == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedGosown = App.Current.Properties["GodownEdit"] as GodownModel;
                //GetGodowns(COMPANY_ID);
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"] == "View")
            {
                SelectedGosown = App.Current.Properties["GodownView"] as GodownModel;
                App.Current.Properties["Action"] = "";
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                SelectedGosown = new GodownModel();
                GetGodowns(COMPANY_ID);
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
        private long _GODOWN_ID;
        public long GODOWN_ID
        {
            get
            {
                return SelectedGosown.GODOWN_ID;
            }
            set
            {
                SelectedGosown.GODOWN_ID = value;


                if (SelectedGosown.GODOWN_ID != value)
                {
                    SelectedGosown.GODOWN_ID = value;
                    OnPropertyChanged("GODOWN_ID");
                }
            }
        }
        private string _GODOWN_NAME;
        public string GODOWN_NAME
        {
            get
            {
                return SelectedGosown.GODOWN_NAME;
            }
            set
            {
                SelectedGosown.GODOWN_NAME = value;


                if (SelectedGosown.GODOWN_NAME != value)
                {
                    SelectedGosown.GODOWN_NAME = value;
                    OnPropertyChanged("GODOWN_NAME");
                }
            }
        }
        private string _SEARCH_GODOWN;
        public string SEARCH_GODOWN
        {
            get
            {
                return _SEARCH_GODOWN;
            }
            set
            {
                if (_SEARCH_GODOWN != value)
                {
                    _SEARCH_GODOWN = value;

                    if (_SEARCH_GODOWN != "" && _SEARCH_GODOWN != null)
                    {

                        GetGodowns(COMPANY_ID);
                    }
                    else
                    {
                        GetGodowns(COMPANY_ID);

                    }
                    OnPropertyChanged("SEARCH_ITEM");
                }
            }
        }
        private string _GOSOWN_DESCRIPTION;
        public string GOSOWN_DESCRIPTION
        {
            get
            {
                return SelectedGosown.GOSOWN_DESCRIPTION;
            }
            set
            {
                SelectedGosown.GOSOWN_DESCRIPTION = value;


                if (SelectedGosown.GOSOWN_DESCRIPTION != value)
                {
                    SelectedGosown.GOSOWN_DESCRIPTION = value;
                    OnPropertyChanged("GOSOWN_DESCRIPTION");
                }
            }
        }
        private bool _IS_ACTIVE;
        public bool IS_ACTIVE
        {
            get
            {
                return SelectedGosown.IS_ACTIVE;
            }
            set
            {
                SelectedGosown.IS_ACTIVE = value;


                if (SelectedGosown.IS_ACTIVE != value)
                {
                    SelectedGosown.IS_ACTIVE = value;
                    OnPropertyChanged("IS_ACTIVE");
                }
            }
        }
        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedGosown.COMPANY_ID;
            }
            set
            {
                SelectedGosown.COMPANY_ID = value;


                if (SelectedGosown.COMPANY_ID != value)
                {
                    SelectedGosown.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }
        private bool _IS_DELETE;
        public bool IS_DELETE
        {
            get
            {
                return SelectedGosown.IS_DELETE;
            }
            set
            {
                SelectedGosown.IS_DELETE = value;


                if (SelectedGosown.IS_DELETE != value)
                {
                    SelectedGosown.IS_DELETE = value;
                    OnPropertyChanged("IS_DELETE");
                }
            }
        }
        public List<GodownModel> _ListGrid { get; set; }
        public List<GodownModel> ListGrid
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

        List<AutoGodownModel> GDownList = new List<AutoGodownModel>();
        List<GodownModel> _ListGrid_Godown = new List<GodownModel>();
        public async Task<ObservableCollection<GodownModel>> GetGodowns(long comp)
        {
            GDownList = new List<AutoGodownModel>();
            _ListGrid_Godown = new List<GodownModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/GodownAPI/GetGodown?id=" + comp + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<GodownModel[]>(await response.Content.ReadAsStringAsync());
                int x = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    x++;
                    _ListGrid_Godown.Add(new GodownModel
                    {
                        SLNO = x,
                        COMPANY_ID = data[i].COMPANY_ID,
                        GODOWN_ID = data[i].GODOWN_ID,
                        GODOWN_NAME = data[i].GODOWN_NAME,
                        GOSOWN_DESCRIPTION = data[i].GOSOWN_DESCRIPTION,
                        IS_ACTIVE = data[i].IS_ACTIVE,
                        IS_DELETE = data[i].IS_DELETE,
                    });
                    GDownList.Add(new AutoGodownModel
                    {
                        DisplayName = data[i].GODOWN_NAME,
                        DisplayId = data[i].GODOWN_ID
                    });
                }
                if (SEARCH_GODOWN != "" && SEARCH_GODOWN != null)
                {
                    var item1 = (from m in _ListGrid_Godown where m.GODOWN_NAME.Contains(SEARCH_GODOWN) select m).ToList();
                    _ListGrid_Godown = item1;
                }
            }

            App.Current.Properties["GoDownList"] = GDownList;
            ListGrid = _ListGrid_Godown;
            return new ObservableCollection<GodownModel>(_ListGrid_Godown);
        }
        public ICommand _InsertGodown { get; set; }
        public ICommand InsertGodown
        {
            get
            {
                if (_InsertGodown == null)
                {

                    _InsertGodown = new DelegateCommand(Add_Godown);
                }
                return _InsertGodown;
            }
        }

        public async void Add_Godown()
        {
            if (SelectedGosown.GODOWN_NAME == "" || SelectedGosown.GODOWN_NAME == null)
            {
                MessageBox.Show("GODOWN NAME is missing");
                return;
            }
            else if (SelectedGosown.GOSOWN_DESCRIPTION == "" || SelectedGosown.GOSOWN_DESCRIPTION == null)
            {
                MessageBox.Show("GODOWN DESCRIPTION is missing");
                return;
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                SelectedGosown.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                var response = await client.PostAsJsonAsync("api/GodownAPI/GodownAdd/", SelectedGosown);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Godown Inserted Successfully");
                    //Window_GodownList.GridRef.ItemsSource = null;
                    //Window_GodownList.GridRef.ItemsSource = _ListGrid_Godown.ToList();
                    Cancel_Godown();
                    ModalService.NavigateTo(new GodownList(), delegate (bool returnValue) { });
                }
            }
        }
        public ICommand _UpdateGodown { get; set; }
        public ICommand UpdateGodown
        {
            get
            {
                if (_UpdateGodown == null)
                {

                    _UpdateGodown = new DelegateCommand(Update_Godown);
                }
                return _UpdateGodown;
            }
        }

        public async void Update_Godown()
        {
            if (SelectedGosown.GODOWN_NAME == "" || SelectedGosown.GODOWN_NAME == null)
            {
                MessageBox.Show("GODOWN NAME is missing");
                return;
            }
            else if (SelectedGosown.GOSOWN_DESCRIPTION == "" || SelectedGosown.GOSOWN_DESCRIPTION == null)
            {
                MessageBox.Show("GODOWN DESCRIPTION is missing");
                return;
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                SelectedGosown.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                var response = await client.PostAsJsonAsync("api/GodownAPI/GodownAdd/", SelectedGosown);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Godown Updated Successfully");
                    Cancel_Godown();
                    ModalService.NavigateTo(new GodownList(), delegate (bool returnValue) { });
                }
            }
        }
        public ICommand _SearchGodown { get; set; }
        public ICommand SearchGodown
        {
            get
            {
                if (_SearchGodown == null)
                {

                    _SearchGodown = new DelegateCommand(Search_Godown);
                }
                return _SearchGodown;
            }
        }

        public async void Search_Godown()
        {
            long Comp = Convert.ToInt64(App.Current.Properties["Company_Id"].ToString());
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/GodownAPI/GodownSearch?name=" + SelectedGosown.SEARCH_GODOWN + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<GodownModel[]>(await response.Content.ReadAsStringAsync());
                for (int i = 0; i < data.Length; i++)
                {
                    _ListGrid_Godown.Add(new GodownModel
                    {
                        COMPANY_ID = data[i].COMPANY_ID,
                        GODOWN_ID = data[i].GODOWN_ID,
                        GODOWN_NAME = data[i].GODOWN_NAME,
                        GOSOWN_DESCRIPTION = data[i].GOSOWN_DESCRIPTION,
                        IS_ACTIVE = data[i].IS_ACTIVE,
                        IS_DELETE = data[i].IS_DELETE,
                    });
                }
            }
            ListGrid = _ListGrid_Godown;
        }
        public ICommand _EditGodown { get; set; }
        public ICommand EditGodown
        {
            get
            {
                if (_EditGodown == null)
                {
                    _EditGodown = new DelegateCommand(Edit_Godown);
                }
                return _EditGodown;
            }
        }
        public async void Edit_Godown()
        {
            if (SelectedGosown.GODOWN_ID != null && SelectedGosown.GODOWN_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                //ItemData = new List<SuppPaymentModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/GodownAPI/GodownEdit?id=" + SelectedGosown.GODOWN_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<GodownModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedGosown.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedGosown.GODOWN_ID = data[i].GODOWN_ID;
                            SelectedGosown.GODOWN_NAME = data[i].GODOWN_NAME;
                            SelectedGosown.GOSOWN_DESCRIPTION = data[i].GOSOWN_DESCRIPTION;
                            SelectedGosown.IS_ACTIVE = data[i].IS_ACTIVE;
                            SelectedGosown.IS_DELETE = data[i].IS_DELETE;
                        }
                        App.Current.Properties["GodownEdit"] = SelectedGosown;
                        AddGOdown_Click();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Godown");
            }
        }
        public ICommand _DeleteGodown;
        public ICommand DeleteGodown
        {
            get
            {
                if (_DeleteGodown == null)
                {
                    _DeleteGodown = new DelegateCommand(Delete_Godown);
                }
                return _DeleteGodown;
            }
        }

        public ICommand _CancelGodown;
        public ICommand CancelGodown
        {
            get
            {
                if (_CancelGodown == null)
                {
                    _CancelGodown = new DelegateCommand(Cancel_Godown);
                }
                return _CancelGodown;
            }
        }



        public void Cancel_Godown()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public async void Delete_Godown()
        {
            if (SelectedGosown.GODOWN_ID != null && SelectedGosown.GODOWN_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Godown " + SelectedGosown.GODOWN_NAME + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    var id = SelectedGosown.GODOWN_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/GodownAPI/DeleteGodown?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Godown Delete Successfully");
                        ModalService.NavigateTo(new GodownList(), delegate (bool returnValue) { });

                    }
                }
                else
                {
                    Cancel_Godown();
                }
            }
            else
            {
                MessageBox.Show("Select Godown");
            }

        }
        private GodownModel _SelectedGosown;
        public GodownModel SelectedGosown
        {

            get { return _SelectedGosown; }
            set
            {

                if (_SelectedGosown != value)
                {
                    _SelectedGosown = value;
                    OnPropertyChanged("SelectedGosown");
                }

            }

        }

        private ICommand _AddGOdownClick { get; set; }
        public ICommand AddGOdownClick
        {
            get
            {
                if (_AddGOdownClick == null)
                {
                    _AddGOdownClick = new DelegateCommand(AddGOdown_Click);
                }
                return _AddGOdownClick;
            }

        }

        public void AddGOdown_Click()
        {
            if (App.Current.Properties["Action"] == "Edit")
            {
                SelectedGosown = App.Current.Properties["GodownEdit"] as GodownModel;
                AddGoDown _AGD = new AddGoDown();
                _AGD.Show();
            }
            else
            {
                AddGoDown _AGD = new AddGoDown();
                _AGD.Show();
            }

            // ModalService.NavigateTo(new ItemLocationList(), delegate(bool returnValue) { });
        }

        private ICommand _ViewGOdownClick { get; set; }
        public ICommand ViewGOdownClick
        {
            get
            {
                if (_ViewGOdownClick == null)
                {
                    _ViewGOdownClick = new DelegateCommand(ViewGOdown_Click);
                }
                return _ViewGOdownClick;
            }

        }
        public void ViewGOdown_Click()
        {
            if (App.Current.Properties["Action"] == "View")
            {
                SelectedGosown = App.Current.Properties["GodownView"] as GodownModel;
                ViewGoDown _AGD = new ViewGoDown();
                _AGD.Show();
            }
            else
            {
                ViewGoDown _AGD = new ViewGoDown();
                _AGD.Show();
            }

            // ModalService.NavigateTo(new ItemLocationList(), delegate(bool returnValue) { });
        }
        public ICommand _ViewGod { get; set; }
        public ICommand ViewGod
        {
            get
            {
                if (_ViewGod == null)
                {
                    _ViewGod = new DelegateCommand(ViewGOdown);
                }
                return _ViewGod;
            }
        }
        public async void ViewGOdown()
        {
            if (SelectedGosown.GODOWN_ID != null && SelectedGosown.GODOWN_ID != 0)
            {
                App.Current.Properties["Action"] = "View";
                //ItemData = new List<SuppPaymentModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/GodownAPI/GodownEdit?id=" + SelectedGosown.GODOWN_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<GodownModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedGosown.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedGosown.GODOWN_ID = data[i].GODOWN_ID;
                            SelectedGosown.GODOWN_NAME = data[i].GODOWN_NAME;
                            SelectedGosown.GOSOWN_DESCRIPTION = data[i].GOSOWN_DESCRIPTION;
                            SelectedGosown.IS_ACTIVE = data[i].IS_ACTIVE;
                            SelectedGosown.IS_DELETE = data[i].IS_DELETE;
                        }

                    }
                    App.Current.Properties["GodownView"] = SelectedGosown;
                    ViewGOdown_Click();
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




        private OpeningStockModel _SelectedOpeningStock;
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
            //var fdrt = App.Current.Properties["OppingDiffProperties"] as OpeningStockModel;
            //if (SelectedOpeningStock.BUSINESS_LOC != null)
            //{
            //    SelectedOpeningStock.BUSINESS_LOC = fdrt.BUSINESS_LOC;
            //}

            //SelectedOpeningStock.GODOWN = SelectedGosown.GODOWN_NAME;

            //if (SelectedOpeningStock.COMPANY_NAME != null)
            //{
            //    SelectedOpeningStock.COMPANY_NAME = fdrt.COMPANY_NAME;
            //}

            //SelectedStockTransfer

            App.Current.Properties["OppingDiffProperties"] = SelectedOpeningStock;
            if (App.Current.Properties["FrmDown"] != null)
            {
                AddStockTransfer.FrmGodownRef.Text = null;
                //AddStockTransfer.FrmGodownRef.Text = SelectedOpeningStock.GODOWN;
                AddStockTransfer.FrmGodownRef.Text = SelectedGosown.GODOWN_NAME;
                App.Current.Properties["AddStockOpStockRefFrom"] = SelectedGosown.GODOWN_ID;
                App.Current.Properties["FrmDown"] = null;
                AddStockTransfer.getbarcode.IsEnabled = true;
                AddStockTransfer.buttoncopy.IsEnabled = true;
                AddStockTransfer.buttoncopy2.IsEnabled = true;

            }


            if (App.Current.Properties["ToDown"] != null)
            {
                AddStockTransfer.ToGodownRef.Text = null;
                //AddStockTransfer.ToGodownRef.Text = SelectedOpeningStock.GODOWN;
                AddStockTransfer.ToGodownRef.Text = SelectedGosown.GODOWN_NAME;
                App.Current.Properties["AddStockOpStockRefTo"] = SelectedGosown.GODOWN_ID;
                App.Current.Properties["ToDown"] = null;
            }

            if (App.Current.Properties["GodownOpStockReff"] != null)
            {
                Window_Opening_stock.GodownRef.Text = null;
                Window_Opening_stock.GodownRef.Text = SelectedGosown.GODOWN_NAME;
                App.Current.Properties["GodownOpStockRef"] = SelectedGosown.GODOWN_ID;
                App.Current.Properties["GodownOpStockReff"] = null;
            }

            //if (Window_Opening_stock.BussRef != null)
            //{
            //    Window_Opening_stock.BussRef.Text = null;
            //    Window_Opening_stock.BussRef.Text = SelectedOpeningStock.BUSINESS_LOC;
            //}



            //if (Window_Opening_stock.GodownRef != null)
            //{
            //    Window_Opening_stock.GodownRef.Text = null;
            //    Window_Opening_stock.GodownRef.Text = Convert.ToString(SelectedOpeningStock.GODOWN);
            //}

            if (App.Current.Properties["GodownRecItem"] != null)
            {
                ReceiveAddItem.GodownReff.Text = null;
                ReceiveAddItem.GodownReff.Text = SelectedGosown.GODOWN_NAME;
                App.Current.Properties["RecItemGodown"] = SelectedGosown.GODOWN_NAME;
                App.Current.Properties["GodownRecItem"] = null;
            }
            if (App.Current.Properties["GodownSalesReturn"] != null)
            {
                SalesReturnAdd.GodownReff.Text = null;
                SalesReturnAdd.GodownReff.Text = SelectedGosown.GODOWN_NAME;
                App.Current.Properties["GodownSalesReturn"] = null;
            }
            if (App.Current.Properties["GodownStockLadger"] != null)
            {
                StockLedgerList.GodownStockLadger.Text = null;
                StockLedgerList.GodownStockLadger.Text = SelectedGosown.GODOWN_NAME;
                App.Current.Properties["getGodownStockLadgerid"] = SelectedGosown.GODOWN_ID;
                App.Current.Properties["GodownStockLadger"] = null;
               
            }
            Cancel_Godown();
            // ItemViewModel iv=new ItemViewModel();
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
