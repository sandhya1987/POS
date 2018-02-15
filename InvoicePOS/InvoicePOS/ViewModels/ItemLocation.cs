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
using InvoicePOS.UserControll.Item;

namespace InvoicePOS.ViewModels
{
    public class ItemLocation : DependencyObject, INotifyPropertyChanged, IModalService
    {   

        public HttpResponseMessage response;
        private readonly ItemModel OprModel;
        ItemModel _opr = new ItemModel();
        public ICommand select { get; set; }
        ManageStockModel[] data = null;
        ItemModel[] dataItem = null;
        ManageLocation[] dataItem1 = null;
        ObservableCollection<ManageStockModel> _ListGrid_Godown = new ObservableCollection<ManageStockModel>();
        ObservableCollection<ItemModel> _ListGrid_Item = new ObservableCollection<ItemModel>();
        ObservableCollection<ItemModel> _ListGrid_Item2 = new ObservableCollection<ItemModel>();
        ObservableCollection<ManageLocation> _ListGrid_Item1 = new ObservableCollection<ManageLocation>();
        ObservableCollection<ManageLocation> _ListGrid_temp = new ObservableCollection<ManageLocation>();
        public ItemLocation()
        {
            SelectItemLocation = new ItemModel();
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());

            if (App.Current.Properties["Action"].ToString() == "Edit" )
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                ItemVisible = "Visible";
                BarVisible = "Visible";
                ItemTxtVisible = "Visible";
                BarTxtVisible = "Visible";
                ItemLocVisible = "Visible";
                ItemComVisible = "Visible";
                SelectItemLocation = App.Current.Properties["ItemLocEdit"] as ItemModel;
                //ItemLocationAdd.ItemSortReff.IsEnabled = false;
                //ItemLocationAdd.ItemLocReff.IsEnabled = false;
               //App.Current.Properties["Action"] = "";

            }


            if (App.Current.Properties["Action"].ToString() == "Add")
            {
                CreatVisible = "Visible";
                UpdVisible = "Collapsed";
                ItemVisible = "Collapsed";
                BarVisible = "Collapsed";
                ItemTxtVisible = "Collapsed";
                BarTxtVisible = "Collapsed";
                ItemLocVisible = "Visible";
                ItemComVisible = "Collapsed";
               // App.Current.Properties["Action"] = "";

            }

            else if (App.Current.Properties["Action"].ToString() == "Show Items")
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                App.Current.Properties["Action"] = "";
                var godown = SelectedItem.GODOWN_ID;

            }

            else
            {
                OprModel = _opr;
                GetGodowns(comp);
                GetLocations();
                GetIndex();
                
            }

        }
        public ObservableCollection<ManageStockModel> _ListGrid { get; set; }
        public ObservableCollection<ManageStockModel> ListGrid
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
        public ObservableCollection<ItemModel> _ItemListGrid { get; set; }
        public ObservableCollection<ItemModel> ItemListGrid
        {
            get
            {
                return _ItemListGrid;
            }
            set
            {
                this._ItemListGrid = value;
                OnPropertyChanged("ItemListGrid");
            }
        }


        public ObservableCollection<ManageLocation> _ItemLocation { get; set; }
        public ObservableCollection<ManageLocation> ItemLocation1
        {
            get
            {
                return _ItemLocation;
            }
            set
            {
                this._ItemLocation = value;
                OnPropertyChanged("ItemLocation1");
            }
        }


        public ObservableCollection<ManageLocation> _ItemLocation2 { get; set; }
        public ObservableCollection<ManageLocation> ItemLocation2
        {
            get
            {
                return _ItemLocation2;
            }
            set
            {
                this._ItemLocation2 = value;
                OnPropertyChanged("ItemLocation2");
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


        private string _BarVisible { get; set; }
        public string BarVisible
        {

            get { return _BarVisible; }
            set
            {
                if (value != _BarVisible)
                {
                    _BarVisible = value;
                    OnPropertyChanged("BarVisible");
                }
            }
        }


        private string _ItemVisible { get; set; }
        public string ItemVisible
        {

            get { return _ItemVisible; }
            set
            {
                if (value != _ItemVisible)
                {
                    _ItemVisible = value;
                    OnPropertyChanged("ItemVisible");
                }
            }
        }

        private string _BarTxtVisible { get; set; }
        public string BarTxtVisible
        {

            get { return _BarTxtVisible; }
            set
            {
                if (value != _BarTxtVisible)
                {
                    _BarTxtVisible = value;
                    OnPropertyChanged("BarTxtVisible");
                }
            }
        }

        private string _ItemLocVisible { get; set; }
        public string ItemLocVisible
        {

            get { return _ItemLocVisible; }
            set
            {
                if (value != _ItemLocVisible)
                {
                    _ItemLocVisible = value;
                    OnPropertyChanged("ItemLocVisible");
                }
            }
        }

        private string _ItemComVisible { get; set; }
        public string ItemComVisible
        {

            get { return _ItemComVisible; }
            set
            {
                if (value != _ItemComVisible)
                {
                    _ItemComVisible = value;
                    OnPropertyChanged("ItemComVisible");
                }
            }
        }

        private string _ItemTxtVisible { get; set; }
        public string ItemTxtVisible
        {

            get { return _ItemTxtVisible; }
            set
            {
                if (value != _ItemTxtVisible)
                {
                    _ItemTxtVisible = value;
                    OnPropertyChanged("ItemTxtVisible");
                }
            }
        }


        private List<ManageStockModel> _ManageStockData;
        public List<ManageStockModel> ManageStockData
        {
            get { return _ManageStockData; }
            set
            {
                if (_ManageStockData != value)
                {
                    _ManageStockData = value;
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

        public ICommand _DeleteLocation;
        public ICommand DeleteLocation
        {
            get
            {
                if (_DeleteLocation == null)
                {
                    _DeleteLocation = new DelegateCommand(Delete_Location);
                }
                return _DeleteLocation;
            }
        }

        public async void Delete_Location()
        {
            if (SelectItemLocation.GODOWN_ID != null && SelectItemLocation.GODOWN_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Godown " + SelectItemLocation.ITEM_LOCATION_NAME + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    var item = SelectItemLocation.ITEM_LOCATION_NAME;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/ItemAPI/DeleteLocation?id=" + item + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Item Location Delete Successfully");
                        //ModalService.NavigateTo(new ItemLocationList(), delegate(bool returnValue) { });

                        var godown = SelectItemLocation.GODOWN_ID;
                        var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                        ItemData = new List<ItemModel>();
                        HttpClient client1 = new HttpClient();
                        client1.BaseAddress = new Uri(GlobalData.gblApiAdress);
                        client1.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        client1.Timeout = new TimeSpan(500000000000);
                        //HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem1?id1=" + comp + " &&?id2=" + godown + "").Result;
                        HttpResponseMessage response1 = client1.GetAsync("api/ItemAPI/GetAllItem1?id=" + godown + "").Result;
                        //_ListGrid_Item = new ObservableCollection<ItemModel>(_ListGrid_Item);
                        _ListGrid_Item.Clear();

                        if (response1.IsSuccessStatusCode)
                        {
                            dataItem = JsonConvert.DeserializeObject<ItemModel[]>(await response1.Content.ReadAsStringAsync());
                            int x = 0;
                            for (int i = 0; i < dataItem.Length; i++)
                            {

                                x++;
                                _ListGrid_Item.Add(new ItemModel
                                {
                                    SLNO = x,
                                    ITEM_NAME = dataItem[i].ITEM_NAME,
                                    ITEM_LOCATION = dataItem[i].ITEM_LOCATION,
                                    ITEM_LOCATION_NAME = dataItem[i].ITEM_LOCATION_NAME,
                                    ITEM_ID = dataItem[i].ITEM_ID,
                                    BARCODE = dataItem[i].BARCODE,
                                    ACCESSORIES_KEYWORD = dataItem[i].ACCESSORIES_KEYWORD,
                                    CATAGORY_ID = dataItem[i].CATAGORY_ID,
                                    ITEM_DESCRIPTION = dataItem[i].ITEM_DESCRIPTION,
                                    ITEM_INVOICE_ID = dataItem[i].ITEM_INVOICE_ID,
                                    ITEM_PRICE = dataItem[i].ITEM_PRICE,
                                    ITEM_PRODUCT_ID = dataItem[i].ITEM_PRODUCT_ID,
                                    GODOWN_ID = dataItem[i].GODOWN_ID,


                                });

                            }

                        }
                        ItemListGrid = _ListGrid_Item;


                    }
                }
                else
                {
                    Cancel_Item();
                }
            }
            else
            {
                MessageBox.Show("Select Item To Delete..");
            }

        }

        private List<ItemModel> _ItemData;
        public List<ItemModel> ItemData
        {
            get { return _ItemData; }
            set
            {
                if (_ItemData != value)
                {
                    _ItemData = value;
                }
            }

        }

        public async Task<ObservableCollection<ManageStockModel>> GetGodowns(long comp)
        {
            
                ManageStockData = new List<ManageStockModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ManageStockAPI/GetManageStock?id=" + comp + "").Result;
                _ListGrid_Godown.Clear();
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ManageStockModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Godown.Add(new ManageStockModel
                        {
                            SLNO = x,
                            BUSSINESS_LOCATION = data[i].BUSSINESS_LOCATION,
                            BUSSINESS_LOCATION_ID = data[i].BUSSINESS_LOCATION_ID,
                            COMPANY_ID = data[i].COMPANY_ID,
                            //DESCRIPTION = data[i].DESCRIPTION,
                            GODOWN_NAME = data[i].GODOWN_NAME,
                            GODOWN_ID = data[i].GODOWN_ID,
                            IS_ACTIVE = data[i].IS_ACTIVE,
                            IS_DEFAULT_GODOWN = data[i].IS_DEFAULT_GODOWN,
                            STOCK_CORRECTION = data[i].STOCK_CORRECTION,
                            DESCRIPTION = data[i].DESCRIPTION,

                        });
                    }
                }


                ListGrid = _ListGrid_Godown;
                ItemLocationList.ListGridMainRef.ItemsSource = _ListGrid_Godown;
                return new ObservableCollection<ManageStockModel>(_ListGrid_Godown);
            
        }

        public ICommand _AddItemLocation { get; set; }
        public ICommand AddItemLocation
        {
            get
            {
                if (_AddItemLocation == null)
                {

                    _AddItemLocation = new DelegateCommand(Add_ItemLocation);
                }
                return _AddItemLocation;
            }
        }

        public async void Add_ItemLocation()
        {
            SelectItemLocation.GODOWN_ID = Convert.ToInt32(App.Current.Properties["Godown"]);
            //SelectItemLocation.ITEM_LOCATION_NAME=SelectedItem.
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            SelectItemLocation.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            var response = await client.PostAsJsonAsync("api/ItemAPI/ItemLocationAdd/", SelectItemLocation);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("ItemLocation Inserted Successfully");
                Cancel_Item();
           
            }
        }
  

        public async void GetLocations()
        {
            try
            {
                //if (SelectedItem.GODOWN_ID != 0)
            
                    var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                    ItemData = new List<ItemModel>();
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    //HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem1?id1=" + comp + " &&?id2=" + godown + "").Result;
                    HttpResponseMessage response = client.GetAsync("api/ItemAPI/Getlocations?id=" + comp + "").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        dataItem1 = JsonConvert.DeserializeObject<ManageLocation[]>(await response.Content.ReadAsStringAsync());
                        if (dataItem1.Length > 0)
                        {
                            int x = 0;
                            for (int i = 0; i < dataItem1.Length; i++)
                            {

                                x++;
                                _ListGrid_temp.Add(new ManageLocation
                                {

                                    SLNO = x,
                                    ITEM_LOCATION_NAME1 = dataItem1[i].ITEM_LOCATION_NAME1,
                                });
                             
                            }
                        }

                    }
                   // ItemLocation1 = null;


                    ItemLocation1 = _ListGrid_temp;
                    if (ItemLocation1 == null)
                    {
                        MessageBox.Show("NO Item Location to Edit..Please Add Location Name..");

                    }

            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async void GetIndex()
       {
            try
            {
                
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                ItemData = new List<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                //HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem1?id1=" + comp + " &&?id2=" + godown + "").Result;
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetIndex?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    dataItem1 = JsonConvert.DeserializeObject<ManageLocation[]>(await response.Content.ReadAsStringAsync());
                    if (dataItem1.Length > 0)
                    {
                        int x = 0;
                        for (int i = 0; i < dataItem1.Length; i++)
                        {

                            x++;
                            _ListGrid_Item1.Add(new ManageLocation
                            {

                                SLNO = x,
                                SORT_INDEX1 = dataItem1[i].SORT_INDEX1,
                                
                            });
                            
                        }
                    }

                }
               // ItemLocation1 = null;

                ItemLocation2 = _ListGrid_Item1;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

            public async void GetValue11()

                {
                 if (App.Current.Properties["Action"].ToString() == "Edit")
                 {
                     var Index = ItemLocationAdd.ItemLocReff.Text;
                     if (Index == null)
                     {
                         ItemLocationAdd.ItemSortReff.IsEnabled = false;
                         ItemLocationAdd.ItemLocReff.IsEnabled = false;
                     }
                     else
                     {
                         ItemLocationAdd.ItemSortReff.IsEnabled = false;
                         ItemLocationAdd.ItemLocReff.IsEnabled = false;
                         //ItemLocationAdd.ItemSortReff.IsEnabled = true;
                         //ItemLocationAdd.ItemLocReff.IsEnabled = true;
                         ItemData = new List<ItemModel>();
                         HttpClient client = new HttpClient();
                         client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                         client.DefaultRequestHeaders.Accept.Add(
                             new MediaTypeWithQualityHeaderValue("application/json"));
                         client.Timeout = new TimeSpan(500000000000);
                         //HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem1?id1=" + comp + " &&?id2=" + godown + "").Result;
                         HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetlocationAuto?id=" + Index + "").Result;
                         if (response.IsSuccessStatusCode)
                         {
                             dataItem1 = JsonConvert.DeserializeObject<ManageLocation[]>(await response.Content.ReadAsStringAsync());
                             if (dataItem1.Length > 0)
                             {

                                 for (int i = 0; i < dataItem1.Length; i++)
                                 {

                                     SelectItemLocation.ITEM_LOCATION_NAME = dataItem1[i].ITEM_LOCATION_NAME1;
                                     SelectItemLocation.SORT_INDEX = dataItem1[i].SORT_INDEX;
                                     SelectItemLocation.LOCATION_ID = dataItem1[i].LOCATION_ID;
                                     ItemLocationAdd.ItemSortReff.Text = SelectItemLocation.SORT_INDEX;

                                 }
                             }
                             else
                             {
                                 //SelectItemLocation.ITEM_LOCATION_NAME1 = SelectItemLocation.ITEM_LOCATION_NAME;
                                 //SelectItemLocation.SORT_INDEX1 = SelectItemLocation.SORT_INDEX;
                                
                                 ItemLocationAdd.comboReff.Text = ItemLocationAdd.ItemLocReff.Text;
                                 ItemLocationAdd.combosortReff.Text = ItemLocationAdd.ItemSortReff.Text;
                                 
                             }

                         }

                        
                     }
                 }
            }

          public async void Show_Items()
            {
            try
            {
              
                if (SelectedItem.GODOWN_ID != 0 )
                {
                                
                    var godown = SelectedItem.GODOWN_ID;
                    App.Current.Properties["Godown"]=SelectedItem.GODOWN_ID;
                    var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                    ItemData = new List<ItemModel>();
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    //HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem1?id=" + godown + "").Result;
                    HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem1?id=" + godown + "&comp=" + comp + "").Result;
                    if (response.IsSuccessStatusCode)
                    {
                       // _ListGrid_Item = null;
                        dataItem = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                        if (dataItem.Length > 0)
                        {
                            int x = 0;
                            for (int i = 0; i < dataItem.Length; i++)
                            {

                                x++;
                                _ListGrid_Item.Add(new ItemModel
                                {
                                    // ITEM_ID = ItemId,
                                    SLNO = x,
                                    ITEM_NAME = dataItem[i].ITEM_NAME,
                                    ITEM_LOCATION_NAME = dataItem[i].ITEM_LOCATION_NAME,
                                    ITEM_ID = dataItem[i].ITEM_ID,
                                    BARCODE = dataItem[i].BARCODE,
                                    ACCESSORIES_KEYWORD = dataItem[i].ACCESSORIES_KEYWORD,
                                    CATAGORY_ID = dataItem[i].CATAGORY_ID,
                                    ITEM_DESCRIPTION = dataItem[i].ITEM_DESCRIPTION,
                                    ITEM_INVOICE_ID = dataItem[i].ITEM_INVOICE_ID,
                                    ITEM_PRICE = dataItem[i].ITEM_PRICE,
                                    ITEM_PRODUCT_ID = dataItem[i].ITEM_PRODUCT_ID,
                                    GODOWN_ID = dataItem[i].GODOWN_ID,


                                });

                            }
                        }
                        else
                        {
                            MessageBox.Show("No Item Found in this Warehouse...");

                        }

                    }
                    App.Current.Properties["ItemShow"] = SelectedItem;
                    App.Current.Properties["ItemToList"] = _ListGrid_Item;
                    ItemListGrid = _ListGrid_Item;
                    ItemLocationList.ListGridRef.ItemsSource = _ListGrid_Item;
                    
                   }
                   // return new ObservableCollection<ItemModel>(_ListGrid_Item);
                        else
                        {
                            MessageBox.Show("No Item Found in this Warehouse...");

                        }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        
        private int _comp;
        public int comp
        {
            get
            {
                return _comp;
            }
            set
            {
                _comp = value;

                if (_comp != value)
                {
                    _comp = value;
                    OnPropertyChanged("comp");
                }
            }
        }

        private string _Select_BarCode { get; set; }
        public string Select_BarCode
        {
            get
            {

                return _Select_BarCode;

            }
            set
            {
                // var compid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());

                if (_Select_BarCode != value)
                {
                    _Select_BarCode = value;

                    if (Select_BarCode != "" && Select_BarCode != null)
                    {

                        GetItem(comp);
                    }
                    else
                    {
                        GetItem(comp);

                    }
                    OnPropertyChanged("Select_BarCode");
                }
            }
        }

        public ICommand _EditItemLoc { get; set; }
        public ICommand EditItemLoc
        {
            get
            {
                if (_EditItemLoc == null)
                {

                    _EditItemLoc = new DelegateCommand(Edit_ItemLoc);
                }
                return _EditItemLoc;
            }
        }

        public async void Edit_ItemLoc()
        {
            try
            {
                if (SelectItemLocation.ITEM_NAME != null && SelectItemLocation.ITEM_NAME != "")
                {
                    App.Current.Properties["itemName"] = null;
                    App.Current.Properties["barcode"] = null;
                    App.Current.Properties["ItemLocAdd"] = null;
                    dataItem = null;
                    App.Current.Properties["Action"] = "Edit";
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetEditItem1?id=" + SelectItemLocation.ITEM_ID + "").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        dataItem = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                        if (dataItem.Length == 0)
                        {
                            SelectItemLocation.ITEM_NAME = SelectItemLocation.ITEM_NAME;
                            SelectItemLocation.BARCODE = SelectItemLocation.BARCODE;
                            
                        }
                        
                        if (dataItem.Length > 0)
                        {
                            //int x = 0;
                            for (int i = 0; i < dataItem.Length; i++)
                            {

                                SelectItemLocation.ITEM_NAME = dataItem[i].ITEM_NAME;
                                SelectItemLocation.BARCODE = dataItem[i].BARCODE;
                                SelectItemLocation.ITEM_LOCATION_NAME = dataItem[i].ITEM_LOCATION_NAME;
                                SelectItemLocation.SORT_INDEX = dataItem[i].SORT_INDEX;


                            }
                        }
                        

                        App.Current.Properties["ItemLocEdit"] = SelectItemLocation;
                       
                        ItemLocationAdd _IA = new ItemLocationAdd();
                        _IA.Show();
                        ItemLocationAdd.ItemSortReff.IsEnabled = false;
                        ItemLocationAdd.ItemLocReff.IsEnabled = false;
                        
                    }
                }
                else
                {

                    MessageBox.Show("Select Item first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public ICommand _ShowItems { get; set; }
        public ICommand ShowItems
        {
            get
            {
                if (_ShowItems == null)
                {

                    _ShowItems = new DelegateCommand(Show_Items);
                }
                return _ShowItems;
            }
        }

        private ManageStockModel _selectedItem;
        public ManageStockModel SelectedItem
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

        private string _SEARCHITEM_NAME { get; set; }
        public string SEARCHITEM_NAME
        {
            get
            {
                return _SEARCHITEM_NAME;
            }
            set
            {
                //var compid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                _SEARCHITEM_NAME = value;

                if (_SEARCHITEM_NAME != value)
                {
                    SEARCHITEM_NAME = value;

                    if (SEARCHITEM_NAME != "" && SEARCHITEM_NAME != null)
                    {

                        GetItem(comp);
                    }
                    else
                    {
                        GetItem(comp);

                    }
                    OnPropertyChanged("SEARCHITEM_NAME");
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
                    _Cancel = new DelegateCommand(Cancel_Item);
                }
                return _Cancel;
            }
        }

        public void Cancel_Item()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                   
                   // ItemListGrid = App.Current.Properties["ItemToList"] as ObservableCollection<ItemModel>;
                    //ItemListGrid.Clear();
                    _ListGrid_Item.Clear();
                    ItemListGrid = _ListGrid_Item;
                    
                }
            }
        }
        public ICommand _UpdateItemLocation;
        public ICommand UpdateItemLocation
        {
            get
            {
                if (_UpdateItemLocation == null)
                {
                    _UpdateItemLocation = new DelegateCommand(Update_ItemLocation);
                }
                return _UpdateItemLocation;
            }
        }

        private CategoryModel _SelectedItemList;
        public CategoryModel SelectedItemList
        {
            get { return _SelectedItemList; }
            set
            {
                if (_SelectedItemList != value)
                {
                    _SelectedItemList = value;
                    OnPropertyChanged("SelectedItemList");
                }
            }
        }

        private ICommand _AddItemLoc { get; set; }
        public ICommand AddItemLoc
        {
            get
            {
                if (_AddItemLoc == null)
                {
                    _AddItemLoc = new DelegateCommand(Add_Click);
                }
                return _AddItemLoc;
            }

        }

        public async void Add_Click()
        {
                       App.Current.Properties["Action"] = "Add";
                       ItemLocationAdd _II = new ItemLocationAdd();
                       _II.Show();
                       
        }

      
        public async void Update_ItemLocation()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/ItemAPI/UpdateLocation/", SelectItemLocation);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Item Location Update Successfully");
                Cancel_Item();
               // ModalService.NavigateTo(new ItemLocationList(), delegate(bool returnValue) { });


                var godown = SelectItemLocation.GODOWN_ID;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                ItemData = new List<ItemModel>();
                HttpClient client1 = new HttpClient();
                client1.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client1.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client1.Timeout = new TimeSpan(500000000000);
                //HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem1?id1=" + comp + " &&?id2=" + godown + "").Result;
                HttpResponseMessage response1 = client1.GetAsync("api/ItemAPI/GetAllItem1?id=" + godown + "").Result;
                if (response1.IsSuccessStatusCode)
                {
                    dataItem = JsonConvert.DeserializeObject<ItemModel[]>(await response1.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < dataItem.Length; i++)
                    {

                        x++;
                        _ListGrid_Item.Add(new ItemModel
                        {
                            // ITEM_ID = ItemId,
                            SLNO = x,
                            ITEM_NAME = dataItem[i].ITEM_NAME,
                            ITEM_LOCATION = dataItem[i].ITEM_LOCATION,
                            ITEM_LOCATION_NAME = dataItem[i].ITEM_LOCATION_NAME,
                            ITEM_ID = dataItem[i].ITEM_ID,
                            BARCODE = dataItem[i].BARCODE,
                            ACCESSORIES_KEYWORD = dataItem[i].ACCESSORIES_KEYWORD,
                            CATAGORY_ID = dataItem[i].CATAGORY_ID,
                            ITEM_DESCRIPTION = dataItem[i].ITEM_DESCRIPTION,
                            ITEM_INVOICE_ID = dataItem[i].ITEM_INVOICE_ID,
                            ITEM_PRICE = dataItem[i].ITEM_PRICE,
                            ITEM_PRODUCT_ID = dataItem[i].ITEM_PRODUCT_ID,
                            GODOWN_ID = dataItem[i].GODOWN_ID,


                        });

                    }

                }
                ItemListGrid = _ListGrid_Item;
           
            }

        }
               
        private string _SEARCHITEM_STOCK { get; set; }
        public string SEARCHITEM_STOCK
        {
            get
            {
                return _SEARCHITEM_STOCK;
            }
            set
            {
               
                _SEARCHITEM_STOCK = value;

                if (_SEARCHITEM_STOCK != value)
                {
                    _SEARCHITEM_STOCK = value;

                    if (_SEARCHITEM_STOCK != "" && _SEARCHITEM_STOCK != null)
                    {

                        GetItem(comp);
                    }
                    else
                    {
                        GetItem(comp);

                    }
                    OnPropertyChanged("SEARCHITEM_STOCK");
                }
            }
        }

        private string _SEARCHITEM_CODE { get; set; }
        public string SEARCHITEM_CODE
        {
            get
            {
                return _SEARCHITEM_CODE;
            }
            set
            {
                // var compid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                _SEARCHITEM_CODE = value;

                if (_SEARCHITEM_CODE != value)
                {
                    _SEARCHITEM_CODE = value;

                    if (_SEARCHITEM_CODE != "" && _SEARCHITEM_CODE != null)
                    {

                        GetItem(comp);
                    }
                    else
                    {
                        GetItem(comp);

                    }
                    OnPropertyChanged("SEARCHITEM_CODE");
                }
            }
        }

        private string _ITEM_NAME;
        public string ITEM_NAME
        {
            get
            {
                return SelectItemLocation.ITEM_NAME;
            }
            set
            {
                SelectItemLocation.ITEM_NAME = value;

                if (SelectItemLocation.ITEM_NAME != value)
                {
                    SelectItemLocation.ITEM_NAME = value;
                    OnPropertyChanged("ITEM_NAME");
                }
            }
        }

        private string _SORT_INDEX;
        public string SORT_INDEX
        {
            get
            {
                return SelectItemLocation.SORT_INDEX;
            }
            set
            {
                SelectItemLocation.SORT_INDEX = value;

                if (SelectItemLocation.SORT_INDEX != value)
                {
                    SelectItemLocation.SORT_INDEX = value;
                    OnPropertyChanged("SORT_INDEX");
                }
            }
        }

        private string _SORT_INDEX1;
        public string SORT_INDEX1
        {
            get
            {
                return SelectItemLocation.SORT_INDEX1;
            }
            set
            {
                SelectItemLocation.SORT_INDEX1 = value;

                if (SelectItemLocation.SORT_INDEX1 != value)
                {
                    SelectItemLocation.SORT_INDEX1 = value;
                    OnPropertyChanged("SORT_INDEX1");
                }
            }
        }

        public async Task<ObservableCollection<ItemModel>> GetItem(int comp)
        {
            try
            {
                ItemData = new List<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    dataItem = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < dataItem.Length; i++)
                    {

                        x++;
                        _ListGrid_Item.Add(new ItemModel
                        {
                            // ITEM_ID = ItemId,
                            SLNO=x,
                            ITEM_NAME = dataItem[i].ITEM_NAME,
                            ITEM_LOCATION = dataItem[i].ITEM_LOCATION,
                            ITEM_ID = dataItem[i].ITEM_ID,
                            BARCODE = dataItem[i].BARCODE,
                            ACCESSORIES_KEYWORD = dataItem[i].ACCESSORIES_KEYWORD,
                            CATAGORY_ID = dataItem[i].CATAGORY_ID,
                            ITEM_DESCRIPTION = dataItem[i].ITEM_DESCRIPTION,
                            ITEM_INVOICE_ID = dataItem[i].ITEM_INVOICE_ID,
                            ITEM_PRICE = dataItem[i].ITEM_PRICE,
                            ITEM_PRODUCT_ID = dataItem[i].ITEM_PRODUCT_ID,
                            KEYWORD = dataItem[i].KEYWORD,
                            MRP = dataItem[i].MRP,
                            PURCHASE_UNIT = dataItem[i].PURCHASE_UNIT,
                            PURCHASE_UNIT_PRICE = dataItem[i].PURCHASE_UNIT_PRICE,
                            SALES_PRICE = dataItem[i].SALES_PRICE,
                            SALES_UNIT = dataItem[i].SALES_UNIT,
                            SEARCH_CODE = dataItem[i].SEARCH_CODE,
                            TAX_COLLECTED = dataItem[i].TAX_COLLECTED,
                            TAX_PAID = dataItem[i].TAX_PAID,
                            ALLOW_PURCHASE_ON_ESHOP = dataItem[i].ALLOW_PURCHASE_ON_ESHOP,
                            CATEGORY_NAME = dataItem[i].CATEGORY_NAME,
                            DISPLAY_INDEX = dataItem[i].DISPLAY_INDEX,
                            INCLUDE_TAX = dataItem[i].INCLUDE_TAX,
                            ITEM_GROUP_NAME = dataItem[i].ITEM_GROUP_NAME,
                            ITEM_UNIQUE_NAME = dataItem[i].ITEM_UNIQUE_NAME,
                            OPN_QNT = dataItem[i].OPN_QNT,
                            REGIONAL_LANGUAGE = dataItem[i].REGIONAL_LANGUAGE,
                            SALES_PRICE_BEFOR_TAX = dataItem[i].SALES_PRICE_BEFOR_TAX,
                            MODIFICATION_DATE = dataItem[i].MODIFICATION_DATE,


                        });

                    }

                }
                App.Current.Properties["ItemShow"] = SelectedItem;
                ItemListGrid = _ListGrid_Item;
                return new ObservableCollection<ItemModel>(_ListGrid_Item);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private ItemModel _SelectItemLocation;
        public ItemModel SelectItemLocation
        {

            get { return _SelectItemLocation; }
            set
            {

                if (_SelectItemLocation != value)
                {
                    _SelectItemLocation = value;
                    OnPropertyChanged("SelectItemLocation");
                }

            }

        }

        private string _ITEM_LOCATION_NAME;
        public string ITEM_LOCATION_NAME
        {
            get
            {
                return SelectItemLocation.ITEM_LOCATION_NAME;
            }
            set
            {
                SelectItemLocation.ITEM_LOCATION_NAME = value;
                if (SelectItemLocation.ITEM_LOCATION_NAME != value)
                {
                    SelectItemLocation.ITEM_LOCATION_NAME = value;
                    OnPropertyChanged("SelectItemLocation");
                }
            }
        }

        private string _ITEM_LOCATION_NAME1;
        public string ITEM_LOCATION_NAME1
        {
            get
            {
                return SelectItemLocation.ITEM_LOCATION_NAME1;
            }
            set
            {
                SelectItemLocation.ITEM_LOCATION_NAME1 = value;
                if (SelectItemLocation.ITEM_LOCATION_NAME1 != value)
                {
                    SelectItemLocation.ITEM_LOCATION_NAME1 = value;
                    OnPropertyChanged("SelectItemLocation");
                }
            }
        }

        private long _GODOWN_ID;
        public long GODOWN_ID
        {
            get
            {
                return SelectedItem.GODOWN_ID;
            }
            set
            {
                SelectedItem.GODOWN_ID = value;
                if (SelectedItem.GODOWN_ID != value)
                {
                    SelectedItem.GODOWN_ID = value;
                    OnPropertyChanged("GODOWN_ID");
                }
            }
        }

        private string _BARCODE;
        public string BARCODE
        {
            get
            {
                return SelectItemLocation.BARCODE;
            }
            set
            {
                SelectItemLocation.BARCODE = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectItemLocation.BARCODE != value)
                {
                    SelectItemLocation.BARCODE = value;
                    OnPropertyChanged("BARCODE");
                }
            }
        }
      
        private string _SEARCH_CODE;
        public string SEARCH_CODE
        {
            get
            {
                return SelectItemLocation.SEARCH_CODE;
            }
            set
            {
                SelectItemLocation.SEARCH_CODE = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectItemLocation.SEARCH_CODE != value)
                {
                    SelectItemLocation.SEARCH_CODE = value;
                    OnPropertyChanged("SEARCH_CODE");
                }
            }
        }
        
        private bool? _IS_ACTIVE;
        public bool? IS_ACTIVE
        {
            get
            {
                return SelectedItem.IS_ACTIVE;
            }
            set
            {
                SelectedItem.IS_ACTIVE = value;

                if (SelectedItem.IS_ACTIVE != value)
                {
                    SelectedItem.IS_ACTIVE = value;
                    OnPropertyChanged("IS_ACTIVE");
                }
            }
        }

        public ICommand _ItemListClick;
        public ICommand ItemListClick
        {
            get
            {
                if (_ItemListClick == null)
                {
                    _ItemListClick = new DelegateCommand(ItemList_Click);
                }
                return _ItemListClick;
            }
        }

        public void ItemList_Click()
        {
            App.Current.Properties["ItemLocationReff"]=1;
            Window_ItemList IA = new Window_ItemList();
            IA.Show();

        }

        public bool Add()
        {
            return true;
        }
        public bool Select()
        {
            return true;
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
