using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.GoDown;
using InvoicePOS.UserControll.SalesReturn;
using InvoicePOS.UserControll.Invoice;
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
    public class SalesReturnViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        ItemModel[] datainv = null;
        SalesReturnModel[] datainv1 = null;
        public HttpResponseMessage response;
        public ICommand select { get; set; }
        private readonly SalesReturnModel OprModel;
        SalesReturnModel _SalesReturnModel = new SalesReturnModel();
        //private SalesReturnModel _selectedItem;
        SalesReturnModel[] data = null;
        ObservableCollection<SalesReturnModel> _ListGrid_Temp = new ObservableCollection<SalesReturnModel>();
        //ObservableCollection<InvoiceViewModel> _ListGrid_Inv = new ObservableCollection<InvoiceViewModel>();
        ObservableCollection<SalesReturnModel> _ListGrid_Inv = new ObservableCollection<SalesReturnModel>();

        public string Error
        {
            get { throw new NotImplementedException(); }
        }


        private SalesReturnModel _selectedItem;
        public SalesReturnModel SelectedItem
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
        public bool _isviewmode;
        public bool _isenable;
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
        public SalesReturnViewModel()
        {

            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            ////OprModel = _ItemModel;
            //if (App.Current.Properties["Action"].ToString() == "Add")
            //{
            //    CreatVisible = "Visible";
            //    UpdVisible = "Collapsed";
            //    App.Current.Properties["Action"] = "";
            //}
            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedItem = App.Current.Properties["SalesReturnEdit"] as SalesReturnModel;
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                SelectedItem = App.Current.Properties["SalesReturnView"] as SalesReturnModel;
                App.Current.Properties["Action"] = "";
            }
            // if (App.Current.Properties["GetInvoiceView"] != null)
            //{
            //    SelectedItem = App.Current.Properties["InvoiceView"] as SalesReturnModel;
            //    InvoiceItem(comp);
            //    App.Current.Properties["InvoiceView"] = null;

            //}

            else
            {
                CreatVisible = "Visible";
                UpdVisible = "Collapsed";
                SelectedItem = new SalesReturnModel();
                GetSalesReturn(comp);
                //GetSalesList(comp);
            }
            GetSlNo();
            isenable = true;
            _isenable = true;


        }
        public ObservableCollection<SalesReturnModel> _ListGrid { get; set; }
        public ObservableCollection<SalesReturnModel> ListGrid
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

        public ObservableCollection<SalesReturnModel> _ListGrid1 { get; set; }
        public ObservableCollection<SalesReturnModel> ListGrid1
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
        public ObservableCollection<SalesReturnModel> _ListGridSalesReturn { get; set; }
        public ObservableCollection<SalesReturnModel> ListGridSalesReturn
        {
            get
            {
                return _ListGridSalesReturn;
            }
            set
            {
                this._ListGridSalesReturn = value;
                OnPropertyChanged("ListGridSalesReturn");
            }
        }

        private List<SalesReturnModel> _SalesReturnData;
        public List<SalesReturnModel> SalesReturnData
        {
            get { return _SalesReturnData; }
            set
            {
                if (_SalesReturnData != value)
                {
                    _SalesReturnData = value;
                }
            }

        }
        public ICommand _AddNewSalesReturnClick;
        public ICommand AddNewSalesReturnClick
        {
            get
            {
                if (_AddNewSalesReturnClick == null)
                {
                    _AddNewSalesReturnClick = new DelegateCommand(AddNewSalesReturnClick_Click);
                }
                return _AddNewSalesReturnClick;
            }
        }

        public void AddNewSalesReturnClick_Click()
        {
            SalesReturnAdd _salesReturn = new SalesReturnAdd();
            _salesReturn.Show();
            //ModalService.NavigateTo(new SalesReturnAdd(), delegate(bool returnValue) { });

        }
        //List<ItemModel> _ListGrid_Inv = new List<ItemModel>();

        private int? _Current_Qty;
        public int? Current_Qty
        {
            get
            {
                return _Current_Qty;
            }
            set
            {
                if (SelectedItem != null)
                {
                    _Current_Qty = value;
                }
                OnPropertyChanged("Current_Qty");

            }
        }



        private int? _TOTAL_QTY;
        public int? TOTAL_QTY
        {
            get
            {
                return _TOTAL_QTY;
            }
            set
            {
               
                    _TOTAL_QTY = value;
                   OnPropertyChanged("TOTAL_QTY");

            }
        }


        private int? _TOTAL_ITEM;
        public int? TOTAL_ITEM
        {
            get
            {
                return _TOTAL_ITEM;
            }
            set
            {

                _TOTAL_ITEM = value;
                OnPropertyChanged("TOTAL_ITEM");

            }
        }

        public async void GetSalesList()
        {

            int inv = Convert.ToInt32(App.Current.Properties["Invoice_Id"]);
          //  BusinessLocation = Convert.ToString(App.Current.Properties["BussLocName"]);
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceById?id="  +inv + "").Result;
            //HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceItem?id=" + SelectInv.INVOICE_ID + "").Result;
            {
                datainv1 = JsonConvert.DeserializeObject<SalesReturnModel[]>(await response.Content.ReadAsStringAsync());
                for (int i = 0; i < datainv1.Length; i++)
                {
                    // SelectedItem.INVOICE_ID = datainv1[i].INVOICE_ID;
                    SelectedItem.BUSINESS_LOCATION = datainv1[i].BUSINESS_LOCATION;
                    SelectedItem.CUSTOMER = datainv1[i].CUSTOMER;
                    SelectedItem.GODOWN = datainv1[i].GODOWN;
                    
                }
                App.Current.Properties["GetInvoiceView"] = SelectedItem;
                //ViewInvoice obj = new ViewInvoice();
                //obj.ShowDialog();
                InvoiceItem(comp);
            }
        }

        public async Task<ObservableCollection<SalesReturnModel>> InvoiceItem(int comp)
        {

            //try
            //{
            // SelectedItem.INVOICE_NO = SalesReturnAdd.InvoiceReff.Text;
            int inv = Convert.ToInt32(App.Current.Properties["Invoice_Id"]);

            //ReportPO.ListGridRef = new DataGrid();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceItem1?id=" + inv + "").Result;
            if (response.IsSuccessStatusCode)
            {
                datainv = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                // POData = new List<POrderModel>();
                int x = 0;
                if (datainv.Length > 0)
                {
                    for (int i = 0; i < datainv.Length; i++)
                    {
                        x++;
                        _ListGrid_Inv.Add(new SalesReturnModel
                        {
                            SLNO = x,
                            ITEM_NAME = datainv[i].ITEM_NAME,
                            BARCODE = datainv[i].BARCODE,
                            SEARCH_CODE = datainv[i].SEARCH_CODE,
                            TAX_COLLECTED = datainv[i].TAX_COLLECTED,
                            Current_Qty = datainv[i].Current_Qty,
                            MRP = datainv[i].MRP,
                            SALES_PRICE = datainv[i].SALES_PRICE,
                           // SALE_QTY = datainv[i].SALE_QTY,
                            SALES_UNIT = datainv[i].SALES_UNIT,
                            //SALES_UNIT = datainv[i].SALES_UNIT,
                            PURCHASE_UNIT = datainv[i].PURCHASE_UNIT,
                            INVOICE_ID = datainv[i].INVOICE_ID,
                            SALE_ID = datainv[i].SALE_ID,


                        });
                    }
                }
                else
                {
                    MessageBox.Show("There is No More Item In This Invoice To Rerurn...");
                }

                for (int i = 0; i < _ListGrid_Inv.Count; i++)
                {
                    //(_ListGrid_Inv[i].PURCHASE_PRICE_BEFORE_TAX) = ((decimal)(_ListGrid_Inv[i].PURCHASE_UNIT_PRICE * 100) / ((_ListGrid_Inv[i].TAX_PAID) + 100));
                    //(_ListGrid_Inv[i].SUB_TOTAL_AFTER_TAX) = ((decimal)(_ListGrid_Inv[i].TOTAL_QTY) * (_ListGrid_Inv[i].PURCHASE_UNIT_PRICE));
                    //(_ListGrid_Inv[i].SUB_TOTAL_BEFORE_TAX) = ((decimal)(_ListGrid_Inv[i].TOTAL_QTY) * (_ListGrid_Inv[i].PURCHASE_PRICE_BEFORE_TAX));
                }

                //_ListGrid_PoItem = _ListGrid_PoItem;
                ListGrid1 = _ListGrid_Inv;
                SalesReturnAdd.ListGridRef.ItemsSource = _ListGrid_Inv;

                App.Current.Properties["DataGridLSR"] = _ListGrid_Inv;

                //App.Current.Properties["DataItem"] = SelectedItem;


                //App.Current.Properties["EditGridData"] = ListGrid1;
                TotalBottom();
            }
            return new ObservableCollection<SalesReturnModel>(_ListGrid_Inv);
               
        }
    



        public void TotalBottom()
        {
            TOTAL_AMOUNT = 0;
            GRAND_TOTAL = 0;
            TOTAL_QTY = 0;
            //TOTAL_TAX = 0;
            if (ListGrid1.Count > 0)
            {
                var TotalItem = ListGrid1.Count();
                for (int i = 0; i < ListGrid1.Count; i++)
                {
                    TOTAL_AMOUNT = Convert.ToDecimal(ListGrid1[i].SALES_PRICE + TOTAL_AMOUNT);
                   // App.Current.Properties["CurrentGrosAmount1"] = TOTAL_AMOUNT;
                    GRAND_TOTAL = Convert.ToDecimal(ListGrid1[i].SALES_PRICE + TOTAL_AMOUNT);
                    TOTAL_QTY = Convert.ToInt32(ListGrid1[i].Current_Qty + TOTAL_QTY);

                    //TOTAL_TAX = Convert.ToDecimal((ListGrid1[i].SUB_TOTAL_AFTER_TAX - ListGrid1[i].SUB_TOTAL_BEFORE_TAX) + TOTAL_TAX);
                }
                TOTAL_ITEM = Convert.ToInt32(TotalItem);
                SalesReturnAdd.TotitemReff.Text = TotalItem.ToString();
            }
            //TOTAL_TAX = Math.Round(TOTAL_TAX, 2);
            //AddPO.TotTaxRef.Text = TOTAL_TAX.ToString();
            //AddPO.TotRef.Text = TOTAL_AMOUNT.ToString();
            SalesReturnAdd.TotalReff.Text = TOTAL_AMOUNT.ToString();
            SalesReturnAdd.GrandReff.Text = TOTAL_AMOUNT.ToString();
            SalesReturnAdd.QtyReff.Text = TOTAL_QTY.ToString();
           
        }

        public async Task<ObservableCollection<SalesReturnModel>> GetSalesReturn(int comp)
        {
            SalesReturnData = new List<SalesReturnModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/SalesReturnAPI/GetAllSalesReturn/?id=" + comp + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<SalesReturnModel[]>(await response.Content.ReadAsStringAsync());
                int x = 0;
                for (int i = 0; i < data.Length; i++)
                {

                    x++;
                    _ListGrid_Temp.Add(new SalesReturnModel
                    {
                        SLNO = x,
                        // ITEM_ID = ItemId,
                        BARCODE = data[i].BARCODE,
                        BUSINESS_LOCATION = data[i].BUSINESS_LOCATION,
                        BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID,
                        COMPANY_ID = data[i].COMPANY_ID,
                        CUS_PENDING_AMOUNT = data[i].CUS_PENDING_AMOUNT,
                        CUSTOMER = data[i].CUSTOMER,
                        CUSTOMER_ID = data[i].CUSTOMER_ID,
                        FREIGHT_CHARGE = data[i].FREIGHT_CHARGE,
                        GODOWN = data[i].GODOWN,
                        GODOWN_ID = data[i].GODOWN_ID,
                        GRAND_TOTAL = data[i].GRAND_TOTAL,
                        INVOICE_ID = data[i].INVOICE_ID,
                        INVOICE_NO = data[i].INVOICE_NO,
                        IS_WITHOUT_INVOICE = data[i].IS_WITHOUT_INVOICE,
                        ITEM_NAME = data[i].ITEM_NAME,
                        NOTES = data[i].NOTES,
                        OUTSTANDING_BALANCE = data[i].OUTSTANDING_BALANCE,
                        PACKING_CHARGE = data[i].PACKING_CHARGE,
                        REFERENCE_NUMBER = data[i].REFERENCE_NUMBER,
                        RETURN_DATE = data[i].RETURN_DATE,
                        ROUND_OFF_AMOUNT = data[i].ROUND_OFF_AMOUNT,
                        SALES_EXECUTIVE = data[i].SALES_EXECUTIVE,
                        SALES_RETURN_ID = data[i].SALES_RETURN_ID,
                        SALES_RETURN_NO = data[i].SALES_RETURN_NO,
                        SEARCH_CODE = data[i].SEARCH_CODE,
                        SUBSIDY_AMOUN = data[i].SUBSIDY_AMOUN,
                        TAX_AMOUNT = data[i].TAX_AMOUNT,
                        TOTAL_AMOUNT = data[i].TOTAL_AMOUNT,
                        //INVOICE_ID = data[i].INVOICE_ID,
                    });

                }

            }
            ListGrid = _ListGrid_Temp;
            

            // var dataw = await _ListGrid_Temp.ToList();//.ToListAsync();
            // return new ObservableCollection<ItemModel>(dataw);
            return new ObservableCollection<SalesReturnModel>(_ListGrid_Temp);
        }
        
        public ICommand _RemoveItem;
        public ICommand RemoveItem
        {
            get
            {
                if (_RemoveItem == null)
                {
                    _RemoveItem = new DelegateCommand(Remove_Item);
                }
                return _RemoveItem;
            }
        }

        public  void Remove_Item()
        {
           
            if (SelectedItem.BARCODE != null && BARCODE != "")
            {
                //SelectedItem = App.Current.Properties["DataItem"] as SalesReturnModel;
                //App.Current.Properties["DataGridLSR"] = null;
                //ListGrid1 = SalesReturnAdd.ListGridRef.ItemsSource;
                ListGrid1 = App.Current.Properties["DataGridLSR"] as ObservableCollection<SalesReturnModel>;
                //var RemoveItem = (from a in ListGridRef where a.BARCODE == SelectedItem.BARCODE select a).FirstOrDefault();

                var RemoveItem = (from a in ListGrid1 where a.BARCODE == SelectedItem.BARCODE select a).FirstOrDefault();

                //var itemToRemove = ListGrid1.Single(r => r.ITEM_NAME == SelectedItem.ITEM_NAME);
                ListGrid1.Remove(RemoveItem);
                SelectedItem = new SalesReturnModel();
                SelectedItem.INVOICE_NO = App.Current.Properties["Inv"].ToString();
                if (App.Current.Properties["Cus"] != null)
                {
                    SelectedItem.CUSTOMER = App.Current.Properties["Cus"].ToString();
                }
                //_ListGrid_Inv.Remove(RemoveItem);
                //ListGrid1 = _ListGrid_Inv;
                //SalesReturnAdd.ListGridRef.ItemsSource = ListGrid1;
                //ListGrid1 = _ListGrid_Inv;
                App.Current.Properties["DataGridLSR"] = ListGrid1;
               // Main.ListGridRef.ItemsSource = ListGrid;
                TotalBottom();
                //App.Current.Properties["DataGrid"] = ListGrid;
                
            }
            else
            {
                MessageBox.Show("Select Removed Item");
            }
                

        }



        public ICommand _InsertSalesreturn;
        public ICommand InsertSalesreturn
        {
            get
            {
                if (_InsertSalesreturn == null)
                {
                    _InsertSalesreturn = new DelegateCommand(Insert_SalesReturn);
                }
                return _InsertSalesreturn;
            }
        }

        public async void Insert_SalesReturn()
        {
            ListGrid1 = App.Current.Properties["DataGridLSR"] as ObservableCollection<SalesReturnModel>;
            if (ListGrid1 != null)
            {
                

                var datagr = App.Current.Properties["DataGridLSR"] as ObservableCollection<SalesReturnModel>;
                // var datagr = App.Current.Properties["DataGridLSR"].ToString();

                SelectedItem.SelectedItem = datagr;


                //if (SelectedItem.BUSINESS_LOCATION == null || SelectedItem.BUSINESS_LOCATION == "")
                //{
                //    MessageBox.Show("BUSINESS LOCATION is missing");
                //}
                // if (SelectedItem.BARCODE == null || SelectedItem.BARCODE=="")
                //{
                //    MessageBox.Show("BARCODE is missing");
                //}
                //else if (SelectedItem.GODOWN == null || SelectedItem.GODOWN == "")
                //{
                //    MessageBox.Show("GODOWN is missing");
                //}
                //else if (SelectedItem.INVOICE_NO == null || SelectedItem.INVOICE_NO == "")
                //{
                //    MessageBox.Show("INVOICE NO is missing");
                //}
                //else if (SelectedItem.FREIGHT_CHARGE == null || SelectedItem.FREIGHT_CHARGE == 0)
                //{
                //    MessageBox.Show("FREIGHT CHARGE is missing");
                //}
                //else if (SelectedItem.PACKING_CHARGE == null || SelectedItem.PACKING_CHARGE == 0)
                //{
                //    MessageBox.Show("PACKING CHARGE is missing");
                //}
                //else if (SelectedItem.SALES_EXECUTIVE == null || SelectedItem.SALES_EXECUTIVE == "")
                //{
                //    MessageBox.Show("SALES EXECUTIVE is missing");
                //}
                //else if (SelectedItem.GRAND_TOTAL == null || SelectedItem.GRAND_TOTAL == 0)
                //{
                //    MessageBox.Show("GRAND TOTAL is missing");
                //}
                //else if (SelectedItem.CUS_PENDING_AMOUNT == null || SelectedItem.CUS_PENDING_AMOUNT == 0)
                //{
                //    MessageBox.Show("PENDING AMOUNT is missing");
                //}
                //else if (SelectedItem.ROUND_OFF_AMOUNT == null || SelectedItem.ROUND_OFF_AMOUNT == 0)
                //{

                //}
                //else
                {

                    if (App.Current.Properties["SalesReturnBuss"] != null)
                    {
                        SelectedItem.BUSINESS_LOCATION = App.Current.Properties["SalesReturnBuss"].ToString();
                    }
                    App.Current.Properties["Action"] = "Add";
                    SelectedItem.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                    //_opr.ITEM_NAME = ITEM_NAME;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    var response = await client.PostAsJsonAsync("api/SalesReturnAPI/CreateSalesReturn/", SelectedItem);


                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure, You want to return Invoice Amount by Cash " + SalesReturnAdd.GrandReff.Text + "?", "Cash Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        if (response.StatusCode.ToString() == "OK")
                        {
                            MessageBox.Show("Cash Refund Amount Is '" + SalesReturnAdd.GrandReff.Text + "'");
                            //MessageBox.Show("Sales Return Inserted Successfuly");
                            Cancel_SalesReturn();
                            ModalService.NavigateTo(new salesReturnList(), delegate(bool returnValue) { });
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select Invoice No...");
            }
        }
        public ICommand _UpdateSalesreturn;
        public ICommand UpdateSalesreturn
        {
            get
            {
                if (_UpdateSalesreturn == null)
                {
                    _UpdateSalesreturn = new DelegateCommand(Update_SalesReturn);
                }
                return _UpdateSalesreturn;
            }
        }

        public async void Update_SalesReturn()
        {
            
            if (SelectedItem.BARCODE == null || SelectedItem.BARCODE == "")
            {
                MessageBox.Show("BARCODE is missing");
            }
            else if (SelectedItem.GODOWN == null || SelectedItem.GODOWN == "")
            {
                MessageBox.Show("GODOWN is missing");
            }
            else if (SelectedItem.INVOICE_NO == null || SelectedItem.INVOICE_NO == "")
            {
                MessageBox.Show("INVOICE NO is missing");
            }
            else if (SelectedItem.FREIGHT_CHARGE == null || SelectedItem.FREIGHT_CHARGE == 0)
            {
                MessageBox.Show("FREIGHT CHARGE is missing");
            }
            else if (SelectedItem.PACKING_CHARGE == null || SelectedItem.PACKING_CHARGE == 0)
            {
                MessageBox.Show("PACKING CHARGE is missing");
            }
            else if (SelectedItem.SALES_EXECUTIVE == null || SelectedItem.SALES_EXECUTIVE == "")
            {
                MessageBox.Show("SALES EXECUTIVE is missing");
            }
            else if (SelectedItem.GRAND_TOTAL == null || SelectedItem.GRAND_TOTAL == 0)
            {
                MessageBox.Show("GRAND TOTAL is missing");
            }
            else if (SelectedItem.CUS_PENDING_AMOUNT == null || SelectedItem.CUS_PENDING_AMOUNT == 0)
            {
                MessageBox.Show("PENDING AMOUNT is missing");
            }
            else if (SelectedItem.ROUND_OFF_AMOUNT == null || SelectedItem.ROUND_OFF_AMOUNT == 0)
            {

            }
            else
            {
                SelectedItem.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                //_opr.ITEM_NAME = ITEM_NAME;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                var response = await client.PostAsJsonAsync("api/SalesReturnAPI/UpdateSalesReturn/", SelectedItem);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Sales Return Update Successfuly");
                    Cancel_SalesReturn();
                    ModalService.NavigateTo(new salesReturnList(), delegate(bool returnValue) { });
                }
            }   
        }
        public ICommand _EditSalesReturn { get; set; }
        public ICommand EditSalesReturn
        {
            get
            {
                if (_EditSalesReturn == null)
                {
                    _EditSalesReturn = new DelegateCommand(Edit_SalesReturn);
                }
                return _EditSalesReturn;
            }
        }
        public async void Edit_SalesReturn()
        {
            if (SelectedItem.SALES_RETURN_ID != null && SelectedItem.SALES_RETURN_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/SalesReturnAPI/EditSalesReturn?id=" + SelectedItem.SALES_RETURN_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<SalesReturnModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedItem.BARCODE = data[i].BARCODE;
                            SelectedItem.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedItem.BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID;
                            SelectedItem.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedItem.CUS_PENDING_AMOUNT = data[i].CUS_PENDING_AMOUNT;
                            SelectedItem.CUSTOMER = data[i].CUSTOMER;
                            SelectedItem.CUSTOMER_ID = data[i].CUSTOMER_ID;
                            SelectedItem.FREIGHT_CHARGE = data[i].FREIGHT_CHARGE;
                            SelectedItem.GODOWN = data[i].GODOWN;
                            SelectedItem.GODOWN_ID = data[i].GODOWN_ID;
                            SelectedItem.GRAND_TOTAL = data[i].GRAND_TOTAL;
                            SelectedItem.INVOICE_ID = data[i].INVOICE_ID;
                            SelectedItem.INVOICE_NO = data[i].INVOICE_NO;
                            SelectedItem.IS_WITHOUT_INVOICE = data[i].IS_WITHOUT_INVOICE;
                            SelectedItem.ITEM_NAME = data[i].ITEM_NAME;
                            SelectedItem.NOTES = data[i].NOTES;
                            SelectedItem.OUTSTANDING_BALANCE = data[i].OUTSTANDING_BALANCE;
                            SelectedItem.PACKING_CHARGE = data[i].PACKING_CHARGE;
                            SelectedItem.REFERENCE_NUMBER = data[i].REFERENCE_NUMBER;
                            SelectedItem.RETURN_DATE = data[i].RETURN_DATE;
                            SelectedItem.ROUND_OFF_AMOUNT = data[i].ROUND_OFF_AMOUNT;
                            SelectedItem.SALES_EXECUTIVE = data[i].SALES_EXECUTIVE;
                            SelectedItem.SALES_RETURN_ID = data[i].SALES_RETURN_ID;
                            SelectedItem.SALES_RETURN_NO = data[i].SALES_RETURN_NO;
                            SelectedItem.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedItem.SUBSIDY_AMOUN = data[i].SUBSIDY_AMOUN;
                            SelectedItem.TAX_AMOUNT = data[i].TAX_AMOUNT;
                            SelectedItem.TOTAL_AMOUNT = data[i].TOTAL_AMOUNT;
                        }
                        App.Current.Properties["SalesReturnEdit"] = SelectedItem;
                        SalesReturnAdd _SelRe = new SalesReturnAdd();
                        _SelRe.Show();
                      //  ModalService.NavigateTo(new SalesReturnAdd(), delegate(bool returnValue) { });
                    }
                }
            }
            else
            {
                MessageBox.Show("Select SalesReturn first", "SalesReturn Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }


        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_SalesReturn);
                }
                return _Cancel;
            }
        }



        public void Cancel_SalesReturn()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }


        public ICommand _ViewSalesReturn { get; set; }
        public ICommand ViewSalesReturn
        {
            get
            {
                if (_ViewSalesReturn == null)
                {
                    _ViewSalesReturn = new DelegateCommand(View_SalesReturn);
                }
                return _ViewSalesReturn;
            }
        }

        public ICommand _InvoiceListClick;
        public ICommand InvoiceListClick
        {
            get
            {
                if (_InvoiceListClick == null)
                {
                    _InvoiceListClick = new DelegateCommand(InvoiceList_Click);
                }
                return _InvoiceListClick;
            }
        }

        public void InvoiceList_Click()
        {
            App.Current.Properties["InvoiceList"] = "1";
            //AddPO.ScrRef.Text = "";
            Window_InvoiceList IL = new Window_InvoiceList();
            IL.Show();
           
        }

       

        public async void View_SalesReturn()
        {
            if (SelectedItem.SALES_RETURN_ID != null && SelectedItem.SALES_RETURN_ID != 0)
            {
                App.Current.Properties["Action"] = "View";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/SalesReturnAPI/EditSalesReturn?id=" + SelectedItem.SALES_RETURN_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<SalesReturnModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedItem.BARCODE = data[i].BARCODE;
                            SelectedItem.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedItem.BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID;
                            SelectedItem.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedItem.CUS_PENDING_AMOUNT = data[i].CUS_PENDING_AMOUNT;
                            SelectedItem.CUSTOMER = data[i].CUSTOMER;
                            SelectedItem.CUSTOMER_ID = data[i].CUSTOMER_ID;
                            SelectedItem.FREIGHT_CHARGE = data[i].FREIGHT_CHARGE;
                            SelectedItem.GODOWN = data[i].GODOWN;
                            SelectedItem.GODOWN_ID = data[i].GODOWN_ID;
                            SelectedItem.GRAND_TOTAL = data[i].GRAND_TOTAL;
                            SelectedItem.INVOICE_ID = data[i].INVOICE_ID;
                            SelectedItem.INVOICE_NO = data[i].INVOICE_NO;
                            SelectedItem.IS_WITHOUT_INVOICE = data[i].IS_WITHOUT_INVOICE;
                            SelectedItem.ITEM_NAME = data[i].ITEM_NAME;
                            SelectedItem.NOTES = data[i].NOTES;
                            SelectedItem.OUTSTANDING_BALANCE = data[i].OUTSTANDING_BALANCE;
                            SelectedItem.PACKING_CHARGE = data[i].PACKING_CHARGE;
                            SelectedItem.REFERENCE_NUMBER = data[i].REFERENCE_NUMBER;
                            SelectedItem.RETURN_DATE = data[i].RETURN_DATE;
                            SelectedItem.ROUND_OFF_AMOUNT = data[i].ROUND_OFF_AMOUNT;
                            SelectedItem.SALES_EXECUTIVE = data[i].SALES_EXECUTIVE;
                            SelectedItem.SALES_RETURN_ID = data[i].SALES_RETURN_ID;
                            SelectedItem.SALES_RETURN_NO = data[i].SALES_RETURN_NO;
                            SelectedItem.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedItem.SUBSIDY_AMOUN = data[i].SUBSIDY_AMOUN;
                            SelectedItem.TAX_AMOUNT = data[i].TAX_AMOUNT;
                            SelectedItem.TOTAL_AMOUNT = data[i].TOTAL_AMOUNT;
                        }
                        App.Current.Properties["SalesReturnView"] = SelectedItem;
                        SalesReturnView _SelRe = new SalesReturnView();
                        _SelRe.Show();
                        //  ModalService.NavigateTo(new SalesReturnAdd(), delegate(bool returnValue) { });
                    }
                }
            }
            else
            {
                MessageBox.Show("Select SalesReturn first", "SalesReturn Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }


        private long _SALES_RETURN_ID;
        public long SALES_RETURN_ID
        {
            get
            {
                return SelectedItem.SALES_RETURN_ID;
            }
            set
            {
                SelectedItem.SALES_RETURN_ID = value;


                if (SelectedItem.SALES_RETURN_ID != value)
                {
                    SelectedItem.SALES_RETURN_ID = value;
                    OnPropertyChanged("SALES_RETURN_ID");
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
        private string _SEARCH_CODE;
        public string SEARCH_CODE
        {
            get
            {
                return SelectedItem.SEARCH_CODE;
            }
            set
            {
                SelectedItem.SEARCH_CODE = value;


                if (SelectedItem.SEARCH_CODE != value)
                {
                    SelectedItem.SEARCH_CODE = value;
                    OnPropertyChanged("SEARCH_CODE");
                }
            }
        }
        private string _SALES_RETURN_NO;
        public string SALES_RETURN_NO
        {
            get
            {
                return SelectedItem.SALES_RETURN_NO;
            }
            set
            {
                SelectedItem.SALES_RETURN_NO = "Info_0001";


                if (SelectedItem.SALES_RETURN_NO != value)
                {
                    SelectedItem.SALES_RETURN_NO = value;
                    OnPropertyChanged("SALES_RETURN_NO");
                }
            }
        }
        private string _BARCODE;
        public string BARCODE
        {
            get
            {
                return SelectedItem.BARCODE;
            }
            set
            {
                SelectedItem.BARCODE = value;


                if (SelectedItem.BARCODE != value)
                {
                    SelectedItem.BARCODE = value;
                    OnPropertyChanged("BARCODE");
                }
            }
        }
        private string _ITEM_NAME;
        public string ITEM_NAME
        {
            get
            {
                return SelectedItem.ITEM_NAME;
            }
            set
            {
                SelectedItem.ITEM_NAME = value;


                if (SelectedItem.ITEM_NAME != value)
                {
                    SelectedItem.ITEM_NAME = value;
                    OnPropertyChanged("ITEM_NAME ");
                }
            }
        }
        private string _SALES_EXECUTIVE;
        public string SALES_EXECUTIVE
        {
            get
            {
                return SelectedItem.SALES_EXECUTIVE;
            }
            set
            {
                SelectedItem.SALES_EXECUTIVE = value;


                if (SelectedItem.SALES_EXECUTIVE != value)
                {
                    SelectedItem.SALES_EXECUTIVE = value;
                    OnPropertyChanged("SALES_EXECUTIVE ");
                }
            }
        }
        private bool _IS_WITHOUT_INVOICE;
        public bool IS_WITHOUT_INVOICE
        {
            get
            {
                return SelectedItem.IS_WITHOUT_INVOICE;
            }
            set
            {
                SelectedItem.IS_WITHOUT_INVOICE = value;


                if (SelectedItem.IS_WITHOUT_INVOICE != value)
                {
                    SelectedItem.IS_WITHOUT_INVOICE = value;
                    OnPropertyChanged("IS_WITHOUT_INVOICE");
                }
            }
        }
        private DateTime _RETURN_DATE;
        public DateTime RETURN_DATE
        {
            get
            {
                return SelectedItem.RETURN_DATE;
            }
            set
            {
                SelectedItem.RETURN_DATE = value;


                if (SelectedItem.RETURN_DATE != value)
                {
                    SelectedItem.RETURN_DATE = value;
                    OnPropertyChanged("RETURN_DATE");
                }
            }
        }
        private string _GODOWN;
        public string GODOWN
        {
            get
            {
                return SelectedItem.GODOWN;
            }
            set
            {
                SelectedItem.GODOWN = value;


                if (SelectedItem.GODOWN != value)
                {
                    SelectedItem.GODOWN = value;
                    OnPropertyChanged("GODOWN");
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
        private long _CUSTOMER_ID;
        public long CUSTOMER_ID
        {
            get
            {
                return SelectedItem.CUSTOMER_ID;
            }
            set
            {
                SelectedItem.CUSTOMER_ID = value;


                if (SelectedItem.CUSTOMER_ID != value)
                {
                    SelectedItem.CUSTOMER_ID = value;
                    OnPropertyChanged("CUSTOMER_ID");
                }
            }
        }
        private string _CUSTOMER;
        public string CUSTOMER
        {
            get
            {
                return SelectedItem.CUSTOMER;
            }
            set
            {
                SelectedItem.CUSTOMER = value;


                if (SelectedItem.CUSTOMER != value)
                {
                    SelectedItem.CUSTOMER = value;
                    OnPropertyChanged("CUSTOMER");
                }
            }
        }
        private string _INVOICE_NO;
        public string INVOICE_NO
        {
            get
            {
                return SelectedItem.INVOICE_NO;
            }
            set
            {
                SelectedItem.INVOICE_NO = value;


                if (SelectedItem.INVOICE_NO != value)
                {
                    SelectedItem.INVOICE_NO = value;
                    OnPropertyChanged("INVOICE_NO");
                }
            }
        }
        private int? _INVOICE_ID;
        public int? INVOICE_ID
        {
            get
            {
                return SelectedItem.INVOICE_ID;
            }
            set
            {
                SelectedItem.INVOICE_ID = value;


                if (SelectedItem.INVOICE_ID != value)
                {
                    SelectedItem.INVOICE_ID = value;
                    OnPropertyChanged("INVOICE_ID");
                }
            }
        }
        private string _REFERENCE_NUMBER;
        public string REFERENCE_NUMBER
        {
            get
            {
                return SelectedItem.REFERENCE_NUMBER;
            }
            set
            {
                SelectedItem.REFERENCE_NUMBER = value;


                if (SelectedItem.REFERENCE_NUMBER != value)
                {
                    SelectedItem.REFERENCE_NUMBER = value;
                    OnPropertyChanged("REFERENCE_NUMBER");
                }
            }
        }
        private decimal _TAX_AMOUNT;
        public decimal TAX_AMOUNT
        {
            get
            {
                return SelectedItem.TAX_AMOUNT = 14;
            }
            set
            {
                SelectedItem.TAX_AMOUNT = value;


                if (SelectedItem.TAX_AMOUNT != value)
                {
                    SelectedItem.TAX_AMOUNT = 14;
                    OnPropertyChanged("TAX_AMOUNT");
                }
            }
        }
        private decimal _FREIGHT_CHARGE;
        public decimal FREIGHT_CHARGE
        {
            get
            {
                return SelectedItem.FREIGHT_CHARGE;
            }
            set
            {
                SelectedItem.FREIGHT_CHARGE = value;


                if (SelectedItem.FREIGHT_CHARGE != value)
                {
                    SelectedItem.FREIGHT_CHARGE = value;
                    OnPropertyChanged("FREIGHT_CHARGE");
                }
            }
        }
        private decimal _PACKING_CHARGE;
        public decimal PACKING_CHARGE
        {
            get
            {
                return SelectedItem.PACKING_CHARGE;
            }
            set
            {
                SelectedItem.PACKING_CHARGE = value;


                if (SelectedItem.PACKING_CHARGE != value)
                {
                    SelectedItem.PACKING_CHARGE = value;
                    OnPropertyChanged("PACKING_CHARGE");
                }
            }
        }
        //private decimal _TOTAL_AMOUNT;
        //public decimal TOTAL_AMOUNT
        //{
        //    get
        //    {
        //        return SelectedItem.TOTAL_AMOUNT;
        //    }
        //    set
        //    {
        //        SelectedItem.TOTAL_AMOUNT = value;


        //        if (SelectedItem.TOTAL_AMOUNT != value)
        //        {
        //            SelectedItem.TOTAL_AMOUNT = value;
        //            OnPropertyChanged("TOTAL_AMOUNT");
        //        }
        //    }
        //}


        private decimal _TOTAL_AMOUNT;
        public decimal TOTAL_AMOUNT
        {
            get
            {
                return _TOTAL_AMOUNT;
            }
            set
            {

                _TOTAL_AMOUNT = value;
                OnPropertyChanged("TOTAL_AMOUNT");

            }
        }


        private decimal _GRAND_TOTAL;
        public decimal GRAND_TOTAL
        {
            get
            {
                return _GRAND_TOTAL;
            }
            set
            {

                _GRAND_TOTAL = value;
                OnPropertyChanged("GRAND_TOTAL");

            }
        }

        private int _ROUND_OFF_AMOUNT;
        public int ROUND_OFF_AMOUNT
        {
            get
            {
                return SelectedItem.ROUND_OFF_AMOUNT;
            }
            set
            {
                SelectedItem.ROUND_OFF_AMOUNT = value;


                if (SelectedItem.ROUND_OFF_AMOUNT != value)
                {
                    SelectedItem.ROUND_OFF_AMOUNT = value;
                    OnPropertyChanged("ROUND_OFF_AMOUNT");
                }
            }
        }

        private decimal _TAX_COLLECTED;
        public decimal TAX_COLLECTED
        {
            get
            {
                return SelectedItem.TAX_COLLECTED;
            }
            set
            {
                SelectedItem.TAX_COLLECTED = value;
                if (SelectedItem.TAX_COLLECTED != value)
                {
                    SelectedItem.TAX_COLLECTED = value;
                    OnPropertyChanged("TAX_COLLECTED");
                }
            }
        }
        //private decimal _GRAND_TOTAL;
        //public decimal GRAND_TOTAL
        //{
        //    get
        //    {
        //        return SelectedItem.GRAND_TOTAL;
        //    }
        //    set
        //    {
        //        SelectedItem.GRAND_TOTAL = value;


        //        if (SelectedItem.GRAND_TOTAL != value)
        //        {
        //            SelectedItem.GRAND_TOTAL = value;
        //            OnPropertyChanged("GRAND_TOTAL");
        //        }
        //    }
        //}
        private decimal _SUBSIDY_AMOUN;
        public decimal SUBSIDY_AMOUN
        {
            get
            {
                return SelectedItem.SUBSIDY_AMOUN;
            }
            set
            {
                SelectedItem.SUBSIDY_AMOUN = value;


                if (SelectedItem.SUBSIDY_AMOUN != value)
                {
                    SelectedItem.SUBSIDY_AMOUN = value;
                    OnPropertyChanged("SUBSIDY_AMOUN");
                }
            }
        }

        private decimal _CUS_PENDING_AMOUNT;
        public decimal CUS_PENDING_AMOUNT
        {
            get
            {
                return SelectedItem.CUS_PENDING_AMOUNT;
            }
            set
            {
                SelectedItem.CUS_PENDING_AMOUNT = value;


                if (SelectedItem.CUS_PENDING_AMOUNT != value)
                {
                    SelectedItem.CUS_PENDING_AMOUNT = value;
                    OnPropertyChanged("CUS_PENDING_AMOUNT");
                }
            }
        }
        private string _NOTES;
        public string NOTES
        {
            get
            {
                return SelectedItem.NOTES;
            }
            set
            {
                SelectedItem.NOTES = value;


                if (SelectedItem.NOTES != value)
                {
                    SelectedItem.NOTES = value;
                    OnPropertyChanged("NOTES");
                }
            }
        }
        public ICommand _DeleteSalesReturn;
        public ICommand DeleteSalesReturn
        {
            get
            {
                if (_DeleteSalesReturn == null)
                {
                    _DeleteSalesReturn = new DelegateCommand(Delete_SalesReturn);
                }
                return _DeleteSalesReturn;
            }
        }
        public async void Delete_SalesReturn()
        {
            if (SelectedItem.SALES_RETURN_ID != null && SelectedItem.SALES_RETURN_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Sales Return " + SelectedItem.SALES_RETURN_NO + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                     var id = SelectedItem.SALES_RETURN_ID;
                     HttpClient client = new HttpClient();
                     client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                     client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                     HttpResponseMessage response = client.GetAsync("api/SalesReturnAPI/DeleteSalesreturn?id=" + id + "").Result;
                     if (response.StatusCode.ToString() == "OK")
                     {
                         //ModalService.NavigateTo(new Items(), delegate(bool returnValue) { });
                         MessageBox.Show("sales return Delete Successfully");
                         ModalService.NavigateTo(new salesReturnList(), delegate(bool returnValue) { });
                     }
                 }
                 else
                 {
                     Cancel_SalesReturn();
                 }
            }
            else
            {
                MessageBox.Show("Select Item Rec");
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
            App.Current.Properties["BussSalesReturn"] = 1;
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();

        }
        public ICommand _GosownClick;
        public ICommand GodownClick
        {
            get
            {
                if (_GosownClick == null)
                {
                    _GosownClick = new DelegateCommand(GodownList_Click);
                }
                return _GosownClick;
            }
        }

        public void GodownList_Click()
        {
            App.Current.Properties["GodownSalesReturn"]=1;
            Window_GodownList IA = new Window_GodownList();
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

        //11.07.2017

        public ICommand _SupMyCode;

        public ICommand SupMyCode
        {
            get
            {
                if (_SupMyCode == null)
                {
                    _SupMyCode = new DelegateCommand(SupMyCode_Click);
                }
                return _SupMyCode;
            }

        }
        public string _ButtonText;
        public String ButtonText
        {
            get { return _ButtonText ?? (_ButtonText = "Auto Generate"); }
            set
            {
                _ButtonText = value;
                NotifyPropertyChanged("ButtonText");
            }
        }
        //public bool _isviewmode;
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
        public async Task<string> GetSlNo()
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
                HttpResponseMessage response = client.GetAsync("api/SalesReturnAPI/GetSalesReturnNo/").Result;
                if (response.IsSuccessStatusCode)
                {
                    //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                    uhy = await response.Content.ReadAsStringAsync();
                    string dd = Convert.ToString(uhy);
                    string aaa = dd.Substring(3, 5);
                    int xx = Convert.ToInt32(aaa);
                    int noo = Convert.ToInt32(xx + 1);
                    nnnn = "S" + noo.ToString("D6");
                    sl_code = nnnn;

                }
                else
                {
                    sl_code = "S000001";
                }
            }
            catch (Exception ex)
            { }

            return uhy;
        }
        public string sl_code
        {
            get
            {
                return SelectedItem.SALES_RETURN_NO;
            }
            set
            {
                SelectedItem.SALES_RETURN_NO = value;
                OnPropertyChanged("sl_code");

            }
        }
        public void SupMyCode_Click()
        {
            //Supplier_Enable = false;
            //VisibleMyCode = "Collapsed";
            //VisibleAutoCode = "Visible";

            if (ButtonText == "Auto Generate")
            {
                ButtonText = "Define My Own";
                GetSlNo();
                _isviewmode = true;
                IsViewMode = true;

            }
            else if (ButtonText == "Define My Own")
            {
                _SALES_RETURN_NO = "";
                ButtonText = "Auto Generate";
                _isviewmode = false;
                IsViewMode = false;
            }
            SelectedItem.SALES_RETURN_NO = _SALES_RETURN_NO;


        }

    }
}
