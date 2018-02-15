using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Bank;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.Customer;
using InvoicePOS.UserControll.Invoice;
using InvoicePOS.UserControll.SalesReturn;
using InvoicePOS.Views.Main;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.Data;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Interop;
using System.Windows.Media;
using System.Printing;
using Microsoft.Win32;
using System.Data.OleDb;
//using static InvoicePOS.Helper.Simple;

namespace InvoicePOS.ViewModels
{
    public class InvoiceViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        ItemModel[] data = null;

        public InvoiceViewModel()
        {
            STARTDATE = STARTDATE;
            ENDDATE = ENDDATE;
            ObservableCollection<ItemModel> _hft = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;
            App.Current.Properties["Action"] = "";
            SelectInvoice = new InvoiceModel();
            //GetInvoiceNo();
            decimal GROSSAMT = 0;
            SelectInv = new GetInvoiceModel();
            //SelectInv1 = new GetInvoiceModel1();
            var datagrid = App.Current.Properties["DataGridL"] as ObservableCollection<ItemModel>;
            SelectInvoice.SelectedItem = datagrid;
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            //NUMBER_OF_ITEM = Convert.ToInt32(App.Current.Properties["TotalProduct"]);
            //QUANTITY_TOTAL = Convert.ToInt32(App.Current.Properties["TotalQnt"]);
            //BEFORE_ROUNDOFF = Convert.ToDecimal(App.Current.Properties["TotalAmount"]);
            if (App.Current.Properties["PyClick"] != null)
            {
                if (datagrid != null)
                {
                    DISCOUNT_INCLUDED = 0;
                    for (int i = 0; i < datagrid.Count; i++)
                    {
                        NUMBER_OF_ITEM = Convert.ToInt32(i + 1);
                        QUANTITY_TOTAL = Convert.ToInt32(datagrid[i].Current_Qty + QUANTITY_TOTAL);
                        BEFORE_ROUNDOFF = Convert.ToDecimal((datagrid[i].Total + BEFORE_ROUNDOFF).ToString("0.00"));
                        decimal asas = ((decimal)(datagrid[i].Current_Qty) * (datagrid[i].Total));
                        GROSSAMT = Convert.ToDecimal(((asas) + GROSSAMT).ToString("0.00"));
                        ITEM_ID = datagrid[i].ITEM_ID;
                        DISCOUNT_INCLUDED = datagrid[i].Discount + Convert.ToDecimal(DISCOUNT_INCLUDED);
                    }
                    INVOICE_NO = Convert.ToString(App.Current.Properties["CurrentInvoiceNo"]);
                    INVOICE_DATE = Convert.ToString(App.Current.Properties["CurrentInvoiceDate"]);
                    if (App.Current.Properties["CustomerManiPage"] != null)
                    {
                        CUSTOMER = Convert.ToString(App.Current.Properties["CustomerManiPage"]);
                    }
                    else
                    {
                        if (App.Current.Properties["SelectCustId"] != null)
                        {
                            CusEmailNo();
                        }
                    }
                    if (App.Current.Properties["BussLoactionMain"] != null)
                    {
                        BusinessLocation = Convert.ToString(App.Current.Properties["BussLoactionMain"]);
                    }
                    else
                    {
                        if (App.Current.Properties["BussLocName"] != null)
                        {
                            BusinessLocation = Convert.ToString(App.Current.Properties["BussLocName"]);
                        }

                    }

                    if (App.Current.Properties["CustomerName"] != null)
                    {
                        CUSTOMER = Convert.ToString(App.Current.Properties["CustomerName"]);

                    }


                    if (App.Current.Properties["BussMainName"] != null)
                    {
                        BusinessLocation = Convert.ToString(App.Current.Properties["BussMainName"]);

                    }
                    if(App.Current.Properties["CurrentOpeningBalanceAmount"] != null)
                    {
                        AVAILABLE_CREDIT_LIMIT = Convert.ToString(App.Current.Properties["CurrentOpeningBalanceAmount"]);
                    }


                    if (App.Current.Properties["CustomerPhoneNo"] != null)
                    {
                        CUSTOMER_MOBILE_NO = Convert.ToString(App.Current.Properties["CustomerPhoneNo"]);
                    }
                    if (App.Current.Properties["CustomerEmail"] != null)
                    {
                        CUSTOMER_EMAIL = Convert.ToString(App.Current.Properties["CustomerEmail"]);
                    }                    
                    //    if (App.Current.Properties["CreditLimit"] != null)
                    //{
                    //    AVAILABLE_CREDIT_LIMIT = Convert.ToString(App.Current.Properties["CreditLimit"]);
                    //}
                    decimal a1 = BEFORE_ROUNDOFF == 0 ? 0 : BEFORE_ROUNDOFF;
                    if (a1 != 0)
                    {
                        string[] str1 = a1.ToString().Split('.');
                        decimal num2 = Convert.ToDecimal("0." + str1[1]);
                        decimal res2;
                        if (Convert.ToDouble(num2) < 0.50)
                        {
                            res2 = Math.Floor(a1);
                            TOTAL_AMOUNT = Convert.ToInt32(res2);
                            PENDING_AMOUNT = Convert.ToInt32(res2);
                            ROUNDOFF_AMOUNT = Convert.ToInt32(res2);
                        }
                        else
                        {
                            res2 = Math.Ceiling(a1);
                            TOTAL_AMOUNT = Convert.ToInt32(res2);
                            PENDING_AMOUNT = Convert.ToInt32(res2);
                            ROUNDOFF_AMOUNT = Convert.ToInt32(res2);
                        }
                        App.Current.Properties["PyClick"] = null;
                    }
                    else
                    {
                        TOTAL_AMOUNT = 0;
                        PENDING_AMOUNT = 0;
                        ROUNDOFF_AMOUNT = 0;
                    }
                    //GetInvoice(comp);
                }
                else
                {
                    MessageBox.Show("There is no Active Item present");
                }
            }
            if (App.Current.Properties["InvoiceView"] != null)
            {
                SelectInv = App.Current.Properties["InvoiceView"] as GetInvoiceModel;
                InvoiceItem(comp);
                App.Current.Properties["InvoiceView"] = null;

            }
            GetInvoice(comp);
        }
        public async void CusEmailNo()
        {
            int a = Convert.ToInt32(App.Current.Properties["SelectCustId"]);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/CustomerAPI/EditCustomer?Custid=" + a + "").Result;
            if (response.IsSuccessStatusCode)
            {
                CustomerModel[] dataCua = JsonConvert.DeserializeObject<CustomerModel[]>(await response.Content.ReadAsStringAsync());
                CUSTOMER = dataCua.ElementAt(0).NAME;
                CUSTOMER_EMAIL = dataCua.ElementAt(0).SHIPPING_EMAIL_ADDRESS;
                CUSTOMER_MOBILE_NO = dataCua.ElementAt(0).SHIPPING_MOBILE_NO;
            }
        }
        //public InvoiceModel _SelectedInvoice;
        //public InvoiceModel SelectedInvoice
        //{
        //    get {return  _SelectedInvoice;}
        //    set{
        //        if (_SelectedInvoice!=value)
        //        {
        //            _SelectedInvoice = value;
        //            OnPropertyChanged("SelectedInvoice");
        //        }
        //    }
        //}
        private InvoiceModel _SelectInvoice;
        public InvoiceModel SelectInvoice
        {

            get { return _SelectInvoice; }
            set
            {

                if (_SelectInvoice != value)
                {
                    _SelectInvoice = value;
                    OnPropertyChanged("SelectInvoice");
                }

            }

        }

        //private ICommand _BussClick { get; set; }
        //public ICommand BussClick
        //{
        //    get
        //    {
        //        if (_BussClick == null)
        //        {
        //            _BussClick = new DelegateCommand(Buss_Click);
        //        }
        //        return _BussClick;
        //    }

        //}

        //public void Buss_Click()
        //{
        //    App.Current.Properties["BussMainReff"] = 1;
        //    Window_BusinessLocationList ex = new Window_BusinessLocationList();
        //    ex.ShowDialog();
        //}

        //private ICommand _PrintCommand { get; set; }
        //public ICommand PrintCommand
        //{
        //    get
        //    {
        //        if (_PrintCommand == null)
        //        {
        //            this._PrintCommand = new SimpleCommand<Grid>
        //            {
        //                CanExecuteDelegate = execute => true,
        //                ExecuteDelegate = grid =>
        //                {
        //                    PrintCharts(grid);
        //                }
        //            };
        //        }
        //        return _PrintCommand;
        //    }

        //}
        public async void ChackINvoiceNo(string InvoiceNo)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/ChackInvoice?InvoiceNo='" + InvoiceNo + "'").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
            }

        }
        public ICommand _InsertInvoice { get; set; }
        public ICommand InsertInvoice
        {
            get
            {
                if (_InsertInvoice == null)
                {
                    _InsertInvoice = new DelegateCommand(Insert_Invoice);
                    //this._InsertInvoice = new SimpleCommand<Grid>
                    //{
                    //    CanExecuteDelegate = execute => true,
                    //    ExecuteDelegate = grid =>
                    //    {
                    //        PrintCharts(grid);
                    //    }
                    //};
                }
                return _InsertInvoice;
            }
        }
        //public class SimpleCommand<T> : ICommand
        //{
        //    public Predicate<T> CanExecuteDelegate { get; set; }
        //    public Action<T> ExecuteDelegate { get; set; }

        //    #region ICommand Members

        //    public bool CanExecute(object parameter)
        //    {
        //        if (CanExecuteDelegate != null)
        //            return CanExecuteDelegate((T)parameter);
        //        return true;// if there is no can execute default to true
        //    }

        //    public event EventHandler CanExecuteChanged
        //    {
        //        add { CommandManager.RequerySuggested += value; }
        //        remove { CommandManager.RequerySuggested -= value; }
        //    }

        //    public void Execute(object parameter)
        //    {
        //        if (ExecuteDelegate != null)
        //            ExecuteDelegate((T)parameter);
        //    }

        //    #endregion
        //}
        public async void Insert_Invoice()
        {
            ChackINvoiceNo(SelectInvoice.INVOICE_NO);
            if (data.Count() > 0)
            {
                MessageBox.Show("Invoice No unick");
                return;
            }
            else if (RECIVED_AMOUNT == 0 || RECIVED_AMOUNT == null)
            {
                MessageBox.Show("Amount Can't be Zero");
                return;
            }
            else
            {

                App.Current.Properties["Action"] = "Add";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("api/InvoiceAPI/CreateInvoice/", SelectInvoice);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Invoice Insert Successfuly");
                    App.Current.Properties["CurrentGrosAmount"] = null;
                    App.Current.Properties["InvoiceDiscount"] = null;
                    App.Current.Properties["DataGrid"] = null;
                    App.Current.Properties["DataGridL"] = null;
                    ObservableCollection<ItemModel> listmodelgrd = new ObservableCollection<ItemModel>();
                    Main.ListGridRef.ItemsSource = App.Current.Properties["DataGrid"] as ObservableCollection<ItemModel>;
                    Main.GrossamountReff.Text = "0";
                    Main.DiscountMainReff.Text = "0";
                    Main.VatMainReff.Text = "0";
                    Main.NetAmountMainReff.Text = "0";
                    Main.ListQnt.Text = "0";
                    Main.ItemTotalMainReff.Text = "0";
                    Main.BussLocationMainReff.Text = null;
                    Main.CustomerMainReff.Text = null;

                    Cancel_Invoice();
                }

                }

            
        }
        private void PrintCharts(Grid grid)
        {
            PrintDialog print = new PrintDialog();
            if (print.ShowDialog() == true)
            {
                PrintCapabilities capabilities = print.PrintQueue.GetPrintCapabilities(print.PrintTicket);

                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / grid.ActualWidth,
                                        capabilities.PageImageableArea.ExtentHeight / grid.ActualHeight);

                Transform oldTransform = grid.LayoutTransform;

                grid.LayoutTransform = new ScaleTransform(scale, scale);

                System.Windows.Size oldSize = new System.Windows.Size(grid.ActualWidth, grid.ActualHeight);
                System.Windows.Size sz = new System.Windows.Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
                grid.Measure(sz);
                ((UIElement)grid).Arrange(new Rect(new System.Windows.Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight),
                    sz));

                print.PrintVisual(grid, "Print Results");
                grid.LayoutTransform = oldTransform;
                grid.Measure(oldSize);

                ((UIElement)grid).Arrange(new Rect(new System.Windows.Point(0, 0),
                    oldSize));
            }
        }
        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_Invoice);
                }
                return _Cancel;
            }
        }



        public void Cancel_Invoice()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        private ICommand _CustomerClick { get; set; }
        public ICommand CustomerClick
        {
            get
            {
                if (_CustomerClick == null)
                {
                    _CustomerClick = new DelegateCommand(Customer_Click);
                }
                return _CustomerClick;
            }

        }

        public void Customer_Click()
        {
            App.Current.Properties["InvoiceCustomerReff"] = 4;
            Window_CustomerList ex = new Window_CustomerList();
            ex.Show();
        }
        private ICommand _BancACClick { get; set; }
        public ICommand BancACClick
        {
            get
            {
                if (_BancACClick == null)
                {
                    _BancACClick = new DelegateCommand(BancAC_Click);
                }
                return _BancACClick;
            }

        }

        public void BancAC_Click()
        {
            App.Current.Properties["InvoiceChequeBankBranchReff"] = 1;
            Window_Bank_AccountList ex = new Window_Bank_AccountList();
            ex.Show();
        }
        private ICommand _BancACClick1 { get; set; }
        public ICommand BancACClick1
        {
            get
            {
                if (_BancACClick1 == null)
                {
                    _BancACClick1 = new DelegateCommand(BancAC_Click1);
                }
                return _BancACClick1;
            }

        }

        public void BancAC_Click1()
        {
            App.Current.Properties["InvoiceChequeBankAcReff"] = 1;
            Window_Bank_AccountList ex = new Window_Bank_AccountList();
            ex.Show();
        }

        private ICommand _BancACClick2 { get; set; }
        public ICommand BancACClick2
        {
            get
            {
                if (_BancACClick2 == null)
                {
                    _BancACClick2 = new DelegateCommand(BancAC_Click2);
                }
                return _BancACClick2;
            }

        }

        public void BancAC_Click2()
        {
            App.Current.Properties["InvoicetransferBankAcReff"] = 1;
            Window_Bank_AccountList ex = new Window_Bank_AccountList();
            ex.Show();
        }

        private ICommand _BancACClick3 { get; set; }
        public ICommand BancACClick3
        {
            get
            {
                if (_BancACClick3 == null)
                {
                    _BancACClick3 = new DelegateCommand(BancAC_Click3);
                }
                return _BancACClick3;
            }

        }

        public void BancAC_Click3()
        {
            App.Current.Properties["InvoicetransferBankBranchReff"] = 1;
            Window_Bank_AccountList ex = new Window_Bank_AccountList();
            ex.Show();
        }

        private ICommand _BancACClick4 { get; set; }
        public ICommand BancACClick4
        {
            get
            {
                if (_BancACClick4 == null)
                {
                    _BancACClick4 = new DelegateCommand(BancAC_Click4);
                }
                return _BancACClick4;
            }

        }

        public void BancAC_Click4()
        {
            App.Current.Properties["InvoiceFinancialBankAccountReff"] = 1;
            Window_Bank_AccountList ex = new Window_Bank_AccountList();
            ex.Show();
        }

        //private ICommand _Print_Command { get; set; }
        //public ICommand Print_Command
        //{
        //    get
        //    {
                
        //        //if (_Print_Command == null)
        //        //{
        //        //    _Print_Command = new DelegateCommand(Print_Command_Click);
        //        //}
        //        return _Print_Command;
        //    }
        //    set
        //    {
        //        this.Print_Command = new SimpleCommand<Grid>
        //        {
        //            CanExecuteDelegate = execute => true,
        //            ExecuteDelegate = grid =>
        //            {
        //                PrintCharts(grid);
        //            }
        //        };
        //    }

        //}


        //public void Print_Command_Click()
        //{
        //    this.Print_Command = new SimpleCommand<Grid>
        //    {
        //        CanExecuteDelegate = execute => true,
        //        ExecuteDelegate = grid =>
        //        {
        //            PrintCharts(grid);
        //        }
        //    };
        //}
        //public async Task<string> GetInvoiceNo()
        //{
        //    string uhy = "";
        //    try
        //    {
        //        var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(GlobalData.gblApiAdress);
        //        client.DefaultRequestHeaders.Accept.Add(
        //            new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.Timeout = new TimeSpan(500000000000);
        //        HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceNo1?CompanyId=" + comp + "").Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
        //            uhy = await response.Content.ReadAsStringAsync();
        //        }
        //        string dd = Convert.ToString(uhy);
        //        string aaa = dd.Substring(1, 10);
        //        INVOICE_NO = aaa;

            //    }
            //    catch (Exception ex)
            //    { }

            //    return uhy;


            //}
        private string _INVOICE_NO;
        public string INVOICE_NO
        {
            get
            {
                return SelectInvoice.INVOICE_NO;
            }
            set
            {
                SelectInvoice.INVOICE_NO = value;
                OnPropertyChanged("INVOICE_NO");

            }
        }


        //private string _CUSTOMER;
        //public string CUSTOMER
        //{
        //    get
        //    {
        //        return SelectInvoice.CUSTOMER;
        //    }
        //    set
        //    {
        //        SelectInvoice.CUSTOMER = value;
        //        if (SelectInvoice.CUSTOMER != value)
        //        {
        //            SelectInvoice.CUSTOMER = value;
        //            OnPropertyChanged("CUSTOMER");
        //        }
        //    }
        //}


        private string _CUSTOMER;
        public string CUSTOMER
        {
            get
            {
                return SelectInvoice.CUSTOMER;
            }
            set
            {
                SelectInvoice.CUSTOMER = value;
                if (SelectInvoice.CUSTOMER != value)
                {
                    SelectInvoice.CUSTOMER = value;
                    OnPropertyChanged("CUSTOMER");
                }
            }
        }


        private string _CUSTOMER_EMAIL;
        public string CUSTOMER_EMAIL
        {
            get
            {
                return SelectInvoice.CUSTOMER_EMAIL;
            }
            set
            {
                SelectInvoice.CUSTOMER_EMAIL = value;
                if (SelectInvoice.CUSTOMER_EMAIL != value)
                {
                    SelectInvoice.CUSTOMER_EMAIL = value;
                    OnPropertyChanged("CUSTOMER_EMAIL");
                }
            }
        }


        private string _CUSTOMER_MOBILE_NO;
        public string CUSTOMER_MOBILE_NO
        {
            get
            {
                return SelectInvoice.CUSTOMER_MOBILE_NO;
            }
            set
            {
                SelectInvoice.CUSTOMER_MOBILE_NO = value;
                if (SelectInvoice.CUSTOMER_MOBILE_NO != value)
                {
                    SelectInvoice.CUSTOMER_MOBILE_NO = value;
                    OnPropertyChanged("CUSTOMER_MOBILE_NO");
                }
            }
        }



        private string _BusinessLocation;
        public string BusinessLocation
        {
            get
            {
                return _BusinessLocation;
            }
            set
            {
                _BusinessLocation = value;
                //_BusinessLocation = Convert.ToString(App.Current.Properties["BussLocName"]);
                OnPropertyChanged("BusinessLocation");
            }
        }
        private long _CUSTOMER_ID;
        public long CUSTOMER_ID
        {
            get
            {
                return SelectInvoice.CUSTOMER_ID;
            }
            set
            {
                SelectInvoice.CUSTOMER_ID = value;
                if (SelectInvoice.CUSTOMER_ID != value)
                {
                    SelectInvoice.CUSTOMER_ID = value;
                    OnPropertyChanged("CUSTOMER_ID");
                }
            }
        }
        private string _AVAILABLE_CREDIT_LIMIT;
        public string AVAILABLE_CREDIT_LIMIT
        {
            get
            {
                return SelectInvoice.AVAILABLE_CREDIT_LIMIT;
            }
            set
            {
                SelectInvoice.AVAILABLE_CREDIT_LIMIT = value;
                if (SelectInvoice.AVAILABLE_CREDIT_LIMIT != value)
                {
                    SelectInvoice.AVAILABLE_CREDIT_LIMIT = value;
                    OnPropertyChanged("AVAILABLE_CREDIT_LIMIT");
                }
            }
        }
        private string _SALES_EXECUTIVE;
        public string SALES_EXECUTIVE
        {
            get
            {
                return SelectInvoice.SALES_EXECUTIVE;
            }
            set
            {
                SelectInvoice.SALES_EXECUTIVE = value;
                if (SelectInvoice.SALES_EXECUTIVE != value)
                {
                    SelectInvoice.SALES_EXECUTIVE = value;
                    OnPropertyChanged("SALES_EXECUTIVE");
                }
            }
        }
        private long _SALES_EXECUTIVE_ID;
        public long SALES_EXECUTIVE_ID
        {
            get
            {
                return SelectInvoice.SALES_EXECUTIVE_ID;
            }
            set
            {
                SelectInvoice.SALES_EXECUTIVE_ID = value;
                if (SelectInvoice.SALES_EXECUTIVE_ID != value)
                {
                    SelectInvoice.SALES_EXECUTIVE_ID = value;
                    OnPropertyChanged("SALES_EXECUTIVE_ID");
                }
            }
        }

        //private string _CUSTOMER_EMAIL;
        //public string CUSTOMER_EMAIL
        //{
        //    get
        //    {
        //        return _CUSTOMER_EMAIL;
        //    }
        //    set
        //    {
        //        _CUSTOMER_EMAIL = value;
        //        OnPropertyChanged("CUSTOMER_EMAIL");

        //    }
        //}

        //private string _CUSTOMER_MOBILE_NO;
        //public string CUSTOMER_MOBILE_NO
        //{
        //    get
        //    {
        //        return _CUSTOMER_MOBILE_NO;
        //    }
        //    set
        //    {
        //        _CUSTOMER_MOBILE_NO = value;
        //        OnPropertyChanged("CUSTOMER_MOBILE_NO");
        //    }
        //}




        private decimal _BEFORE_ROUNDOFF;
        public decimal BEFORE_ROUNDOFF
        {
            get
            {
                return SelectInvoice.BEFORE_ROUNDOFF;
            }
            set
            {
                SelectInvoice.BEFORE_ROUNDOFF = value;
                if (SelectInvoice.BEFORE_ROUNDOFF != value)
                {
                    SelectInvoice.BEFORE_ROUNDOFF = value;
                    OnPropertyChanged("BEFORE_ROUNDOFF");
                }
            }
        }
        private int _ITEM_ID;
        public int ITEM_ID
        {
            get
            {
                return SelectInvoice.ITEM_ID;
            }
            set
            {
                SelectInvoice.ITEM_ID = value;
                OnPropertyChanged("ROUNDOFF_AMOUNT");

            }
        }
        private int _ROUNDOFF_AMOUNT;
        public int ROUNDOFF_AMOUNT
        {
            get
            {
                return SelectInvoice.ROUNDOFF_AMOUNT;
            }
            set
            {
                SelectInvoice.ROUNDOFF_AMOUNT = value;
                OnPropertyChanged("ROUNDOFF_AMOUNT");

            }
        }
        private int _TOTAL_AMOUNT;
        public int TOTAL_AMOUNT
        {
            get
            {
                return SelectInvoice.TOTAL_AMOUNT;
            }
            set
            {
                SelectInvoice.TOTAL_AMOUNT = value;
                if (SelectInvoice.TOTAL_AMOUNT != value)
                {
                    SelectInvoice.TOTAL_AMOUNT = value;
                    OnPropertyChanged("TOTAL_AMOUNT");
                }
            }
        }

        private int _QUANTITY_TOTAL;
        public int QUANTITY_TOTAL
        {
            get
            {
                return SelectInvoice.QUANTITY_TOTAL;
            }
            set
            {
                SelectInvoice.QUANTITY_TOTAL = value;
                if (SelectInvoice.QUANTITY_TOTAL != value)
                {
                    SelectInvoice.QUANTITY_TOTAL = value;
                    OnPropertyChanged("QUANTITY_TOTAL");
                }
            }
        }

        private int _NUMBER_OF_ITEM;
        public int NUMBER_OF_ITEM
        {
            get
            {
                return SelectInvoice.NUMBER_OF_ITEM;
            }
            set
            {
                SelectInvoice.NUMBER_OF_ITEM = value;
                if (SelectInvoice.NUMBER_OF_ITEM != value)
                {
                    SelectInvoice.NUMBER_OF_ITEM = value;
                    OnPropertyChanged("NUMBER_OF_ITEM");
                }
            }
        }
        private decimal _DISCOUNT_INCLUDED;
        public decimal DISCOUNT_INCLUDED
        {
            get
            {
                return SelectInvoice.DISCOUNT_INCLUDED;
            }
            set
            {
                SelectInvoice.DISCOUNT_INCLUDED = value;
                if (SelectInvoice.DISCOUNT_INCLUDED != value)
                {
                    SelectInvoice.DISCOUNT_INCLUDED = value;
                    OnPropertyChanged("DISCOUNT_INCLUDED");
                }
            }
        }
        private decimal _TAX_INCLUDED;
        public decimal TAX_INCLUDED
        {
            get
            {
                return SelectInvoice.TAX_INCLUDED;
            }
            set
            {
                SelectInvoice.TAX_INCLUDED = value;
                if (SelectInvoice.TAX_INCLUDED != value)
                {
                    SelectInvoice.TAX_INCLUDED = value;
                    OnPropertyChanged("TAX_INCLUDED");
                }
            }
        }

        private string _CASH_REG;
        public string CASH_REG
        {
            get
            {
                return SelectInvoice.CASH_REG;
            }
            set
            {
                SelectInvoice.CASH_REG = value;
                if (SelectInvoice.CASH_REG != value)
                {
                    SelectInvoice.CASH_REG = value;
                    OnPropertyChanged("CASH_REG");
                }
            }
        }
        private decimal _CASH_AMOUNT;
        public decimal CASH_AMOUNT
        {
            get
            {
                return SelectInvoice.CASH_AMOUNT;
            }
            set
            {
                SelectInvoice.CASH_AMOUNT = value;
                decimal recivedAmount = 0;
                recivedAmount = CASH_AMOUNT + CHEQUE_AMOUNT + TRANSFER_AMOUNT + FINANCIAL_AMOUNT;
                decimal pendingAmount = TOTAL_AMOUNT - recivedAmount;
                decimal returnAmount = RECIVED_AMOUNT - TOTAL_AMOUNT;
                if (SelectInvoice.CASH_AMOUNT == value)
                {
                    
                    if (recivedAmount >= TOTAL_AMOUNT)
                    {
                        PENDING_AMOUNT = 0;
                        ADVANCED_AMOUNT = 0;
                        RETURNABLE_AMOUNT = recivedAmount - TOTAL_AMOUNT;
                        //RECIVED_AMOUNT = TOTAL_AMOUNT;
                        RECIVED_AMOUNT = recivedAmount;
                    }
                    else
                    {
                        RECIVED_AMOUNT = recivedAmount;
                        PENDING_AMOUNT = pendingAmount;
                        ADVANCED_AMOUNT = pendingAmount;
                        RETURNABLE_AMOUNT = 0;
                    }
                    OnPropertyChanged("CASH_AMOUNT");
                }
            }
        }

        private decimal _CHEQUE_AMOUNT;
        public decimal CHEQUE_AMOUNT
        {
            get
            {
                return SelectInvoice.CHEQUE_AMOUNT;
            }
            set
            {
                SelectInvoice.CHEQUE_AMOUNT = value;
                decimal recivedAmount = 0;
                recivedAmount = CASH_AMOUNT + CHEQUE_AMOUNT + TRANSFER_AMOUNT + FINANCIAL_AMOUNT;
                decimal pendingAmount = TOTAL_AMOUNT - recivedAmount;
                decimal returnAmount = RECIVED_AMOUNT - TOTAL_AMOUNT;
                if (SelectInvoice.CHEQUE_AMOUNT == value)
                {
                    if (recivedAmount >= TOTAL_AMOUNT)
                    {
                        PENDING_AMOUNT = 0;
                        ADVANCED_AMOUNT = 0;
                        RETURNABLE_AMOUNT = recivedAmount - TOTAL_AMOUNT;
                        RECIVED_AMOUNT = TOTAL_AMOUNT;
                    }
                    else
                    {
                        RECIVED_AMOUNT = recivedAmount;
                        PENDING_AMOUNT = pendingAmount;
                        ADVANCED_AMOUNT = pendingAmount;
                        RETURNABLE_AMOUNT = 0;
                    }
                    OnPropertyChanged("CHEQUE_AMOUNT");
                }
            }
        }

        private string _CHEQUE_NO;
        public string CHEQUE_NO
        {
            get
            {
                return SelectInvoice.CHEQUE_NO;
            }
            set
            {
                SelectInvoice.CHEQUE_NO = value;
                if (SelectInvoice.CHEQUE_NO != value)
                {
                    SelectInvoice.CHEQUE_NO = value;
                    OnPropertyChanged("CHEQUE_NO");
                }
            }
        }

        private string _CHEQUE_BANK_BRANCH;
        public string CHEQUE_BANK_BRANCH
        {
            get
            {
                return SelectInvoice.CHEQUE_BANK_BRANCH;
            }
            set
            {
                SelectInvoice.CHEQUE_BANK_BRANCH = value;
                if (SelectInvoice.CHEQUE_BANK_BRANCH == value)
                {
                    SelectInvoice.CHEQUE_BANK_BRANCH = value;
                    OnPropertyChanged("CHEQUE_BANK_BRANCH");
                }
            }
        }
        private string _CHEQUE_DATE;
        public string CHEQUE_DATE
        {
            get
            {
                return SelectInvoice.CHEQUE_DATE;
            }
            set
            {
                SelectInvoice.CHEQUE_DATE = value;
                if (SelectInvoice.CHEQUE_DATE != value)
                {
                    SelectInvoice.CHEQUE_DATE = value;
                    OnPropertyChanged("CHEQUE_DATE");
                }
            }
        }

        private long _CHEQUE_BANK_AC;
        public long CHEQUE_BANK_AC
        {
            get
            {
                return SelectInvoice.CHEQUE_BANK_AC;
            }
            set
            {
                SelectInvoice.CHEQUE_BANK_AC = value;
                if (SelectInvoice.CHEQUE_BANK_AC != value)
                {
                    SelectInvoice.CHEQUE_BANK_AC = value;
                    OnPropertyChanged("CHEQUE_BANK_AC");
                }
            }
        }

        private bool _IS_CHEQUE_PRINT;
        public bool IS_CHEQUE_PRINT
        {
            get
            {
                return SelectInvoice.IS_CHEQUE_PRINT;
            }
            set
            {
                SelectInvoice.IS_CHEQUE_PRINT = value;
                if (SelectInvoice.IS_CHEQUE_PRINT != value)
                {
                    SelectInvoice.IS_CHEQUE_PRINT = value;
                    OnPropertyChanged("IS_CHEQUE_PRINT");
                }
            }
        }
        private string _CHEQUE_PRINT;
        public string CHEQUE_PRINT
        {
            get
            {
                return SelectInvoice.CHEQUE_PRINT;
            }
            set
            {
                SelectInvoice.CHEQUE_PRINT = value;
                if (SelectInvoice.CHEQUE_PRINT != value)
                {
                    SelectInvoice.CHEQUE_PRINT = value;
                    OnPropertyChanged("CHEQUE_PRINT");
                }
            }
        }
        private decimal _TRANSFER_AMOUNT;
        public decimal TRANSFER_AMOUNT
        {
            get
            {
                return SelectInvoice.TRANSFER_AMOUNT;
            }
            set
            {
                SelectInvoice.TRANSFER_AMOUNT = value;
                decimal recivedAmount = 0;
                recivedAmount = CASH_AMOUNT + CHEQUE_AMOUNT + TRANSFER_AMOUNT + FINANCIAL_AMOUNT;
                decimal pendingAmount = TOTAL_AMOUNT - recivedAmount;
                decimal returnAmount = RECIVED_AMOUNT - TOTAL_AMOUNT;
                if (SelectInvoice.TRANSFER_AMOUNT == value)
                {
                    if (recivedAmount >= TOTAL_AMOUNT)
                    {
                        PENDING_AMOUNT = 0;
                        ADVANCED_AMOUNT = 0;
                        RETURNABLE_AMOUNT = recivedAmount - TOTAL_AMOUNT;
                        RECIVED_AMOUNT = TOTAL_AMOUNT;
                    }
                    else
                    {
                        RECIVED_AMOUNT = recivedAmount;
                        ADVANCED_AMOUNT = pendingAmount;
                        PENDING_AMOUNT = pendingAmount;
                        RETURNABLE_AMOUNT = 0;
                    }
                    OnPropertyChanged("TRANSFER_AMOUNT");
                }
            }
        }

        private string _TRANSFER_BRANCH;
        public string TRANSFER_BRANCH
        {
            get
            {
                return SelectInvoice.TRANSFER_BRANCH;
            }
            set
            {
                SelectInvoice.TRANSFER_BRANCH = value;
                if (SelectInvoice.TRANSFER_BRANCH != value)
                {
                    SelectInvoice.TRANSFER_BRANCH = value;
                    OnPropertyChanged("TRANSFER_BRANCH");
                }
            }
        }
        private long _TRANSFER_BANK_AC;
        public long TRANSFER_BANK_AC
        {
            get
            {
                return SelectInvoice.TRANSFER_BANK_AC;
            }
            set
            {
                SelectInvoice.TRANSFER_BANK_AC = value;
                if (SelectInvoice.TRANSFER_BANK_AC != value)
                {
                    SelectInvoice.TRANSFER_BANK_AC = value;
                    OnPropertyChanged("TRANSFER_BANK_AC");
                }
            }
        }

        private string _TRANSFER_DATE;
        public string TRANSFER_DATE
        {
            get
            {
                return SelectInvoice.TRANSFER_DATE;
            }
            set
            {
                SelectInvoice.TRANSFER_DATE = value;
                if (SelectInvoice.TRANSFER_DATE != value)
                {
                    SelectInvoice.TRANSFER_DATE = value;
                    OnPropertyChanged("TRANSFER_DATE");
                }
            }
        }
        private string _INVOICE_DATE;
        public string INVOICE_DATE
        {
            get
            {
                return SelectInvoice.INVOICE_DATE;
            }
            set
            {
                SelectInvoice.INVOICE_DATE = value;
                if (SelectInvoice.INVOICE_DATE == value)
                {
                    OnPropertyChanged("INVOICE_DATE");
                }
            }
        }
        private decimal _FINANCIAL_AMOUNT;
        public decimal FINANCIAL_AMOUNT
        {
            get
            {
                return SelectInvoice.FINANCIAL_AMOUNT;
            }
            set
            {
                SelectInvoice.FINANCIAL_AMOUNT = value;
                decimal recivedAmount = 0;
                recivedAmount = CASH_AMOUNT + CHEQUE_AMOUNT + TRANSFER_AMOUNT + FINANCIAL_AMOUNT;
                decimal pendingAmount = TOTAL_AMOUNT - recivedAmount;
                decimal returnAmount = RECIVED_AMOUNT - TOTAL_AMOUNT;
                if (SelectInvoice.FINANCIAL_AMOUNT == value)
                {
                    if (recivedAmount >= TOTAL_AMOUNT)
                    {
                        PENDING_AMOUNT = 0;
                        ADVANCED_AMOUNT = 0;
                        RETURNABLE_AMOUNT = recivedAmount - TOTAL_AMOUNT;
                        RECIVED_AMOUNT = TOTAL_AMOUNT;
                    }
                    else
                    {
                        RECIVED_AMOUNT = recivedAmount;
                        ADVANCED_AMOUNT = pendingAmount;
                        PENDING_AMOUNT = pendingAmount;
                        RETURNABLE_AMOUNT = 0;
                    }
                    //if (recivedAmount >= TOTAL_AMOUNT)
                    //{
                    //    RETURNABLE_AMOUNT = returnAmount;
                    //}
                    //else
                    //{
                    //    RECIVED_AMOUNT = recivedAmount;
                    //    PENDING_AMOUNT = recivedAmount;
                    //}
                    //if (RECIVED_AMOUNT >= TOTAL_AMOUNT)
                    //{
                    //    RETURNABLE_AMOUNT = RECIVED_AMOUNT - TOTAL_AMOUNT;
                    //}
                    //else
                    //{
                    //    RECIVED_AMOUNT = CASH_AMOUNT + CHEQUE_AMOUNT + TRANSFER_AMOUNT + FINANCIAL_AMOUNT;
                    //    PENDING_AMOUNT = PENDING_AMOUNT - FINANCIAL_AMOUNT;
                    //}
                    OnPropertyChanged("FINANCIAL_AMOUNT");
                }
            }
        }
        private long _FINANCIAL_AC_NO;
        public long FINANCIAL_AC_NO
        {
            get
            {
                return SelectInvoice.FINANCIAL_AC_NO;
            }
            set
            {
                SelectInvoice.FINANCIAL_AC_NO = value;
                if (SelectInvoice.FINANCIAL_AC_NO != value)
                {
                    SelectInvoice.FINANCIAL_AC_NO = value;
                    OnPropertyChanged("FINANCIAL_AC_NO");
                }
            }
        }

        private decimal _DISCOUNT_FLAT;
        public decimal DISCOUNT_FLAT
        {
            get
            {
                return SelectInvoice.DISCOUNT_FLAT;
            }
            set
            {
                SelectInvoice.DISCOUNT_FLAT = value;
                if (SelectInvoice.DISCOUNT_FLAT != value)
                {
                    SelectInvoice.DISCOUNT_FLAT = value;
                    OnPropertyChanged("DISCOUNT_FLAT");
                }
            }
        }

        private decimal _DISCOUNT_PERCENT;
        public decimal DISCOUNT_PERCENT
        {
            get
            {
                return SelectInvoice.DISCOUNT_PERCENT;
            }
            set
            {
                SelectInvoice.DISCOUNT_PERCENT = value;
                if (SelectInvoice.DISCOUNT_PERCENT != value)
                {
                    SelectInvoice.DISCOUNT_PERCENT = value;
                    OnPropertyChanged("DISCOUNT_PERCENT");
                }
            }
        }

        //private decimal _RECIVED_AMOUNT;
        //public decimal RECIVED_AMOUNT
        //{
        //    get
        //    {
        //        return _RECIVED_AMOUNT;
        //    }
        //    set
        //    {
        //        _RECIVED_AMOUNT = value;
        //        if (_RECIVED_AMOUNT != null && _RECIVED_AMOUNT != 0)
        //        {
        //            if (_RECIVED_AMOUNT <= TOTAL_AMOUNT)
        //            {
        //                RETURNABLE_AMOUNT = 0;
        //            }
        //            else
        //            {
        //                RETURNABLE_AMOUNT = _RECIVED_AMOUNT - TOTAL_AMOUNT;
        //            }
        //        }
        //        OnPropertyChanged("RECIVED_AMOUNT");
        //    }
        //}

        private decimal _RECIVED_AMOUNT;
        public decimal RECIVED_AMOUNT
        {
            get
            {
                return _RECIVED_AMOUNT;
            }
            set
            {
                _RECIVED_AMOUNT = value;
                if (_RECIVED_AMOUNT != null && _RECIVED_AMOUNT != 0)
                {
                    //if (_RECIVED_AMOUNT <= CASH_AMOUNT)
                    //{
                    //    RETURNABLE_AMOUNT = 0;
                    //}
                    //else
                    //{
                        RETURNABLE_AMOUNT = CASH_AMOUNT - TOTAL_AMOUNT;
                    //}
                }
                OnPropertyChanged("RECIVED_AMOUNT");
            }
        }


        
        private decimal _RETURNABLE_AMOUNT;
        public decimal RETURNABLE_AMOUNT
        {
            get
            {
                return _RETURNABLE_AMOUNT;
            }
            set
            {
                _RETURNABLE_AMOUNT = value;
                OnPropertyChanged("RETURNABLE_AMOUNT");
            }
        }
        private string _COMMISION_EXPENSE;
        public string COMMISION_EXPENSE
        {
            get
            {
                return SelectInvoice.COMMISION_EXPENSE;
            }
            set
            {
                SelectInvoice.COMMISION_EXPENSE = value;
                if (SelectInvoice.COMMISION_EXPENSE != value)
                {
                    SelectInvoice.COMMISION_EXPENSE = value;
                    OnPropertyChanged("COMMISION_EXPENSE");
                }
            }
        }
        private decimal _PENDING_AMOUNT;
        public decimal PENDING_AMOUNT
        {
            get
            {
                return SelectInvoice.PENDING_AMOUNT;
            }
            set
            {
                SelectInvoice.PENDING_AMOUNT = value;
                if (SelectInvoice.PENDING_AMOUNT == value)
                {
                    OnPropertyChanged("PENDING_AMOUNT");
                }
            }
        }

        private decimal _CREDIT_AMOUNT;
        public decimal CREDIT_AMOUNT
        {
            get
            {
               
                return SelectInvoice.CREDIT_AMOUNT;
            }
            set
            {
                SelectInvoice.CREDIT_AMOUNT = value;

                CREDIT_AMOUNT = Convert.ToDecimal(App.Current.Properties["CreditLimit"].ToString());
            }
        }
        //*29.11.2017*
        private decimal _ADVANCED_AMOUNT;
        public decimal ADVANCED_AMOUNT
        {
            get
            {

                return SelectInvoice.ADVANCED_AMOUNT;
            }
            set
            {
                SelectInvoice.CASH_AMOUNT = value;
                decimal recivedAmount = 0;
                recivedAmount = CASH_AMOUNT + CHEQUE_AMOUNT + TRANSFER_AMOUNT + FINANCIAL_AMOUNT;
                decimal pendingAmount = TOTAL_AMOUNT - recivedAmount;
                decimal returnAmount = RECIVED_AMOUNT - TOTAL_AMOUNT;
                if (SelectInvoice.CASH_AMOUNT == value)
                {
                    if (recivedAmount >= TOTAL_AMOUNT)
                    {
                        PENDING_AMOUNT = 0;
                        ADVANCED_AMOUNT = 0;
                        RETURNABLE_AMOUNT = recivedAmount - TOTAL_AMOUNT;
                        //RECIVED_AMOUNT = TOTAL_AMOUNT;
                        RECIVED_AMOUNT = recivedAmount;
                    }
                    else
                    {
                        RECIVED_AMOUNT = recivedAmount;
                        PENDING_AMOUNT = pendingAmount;
                        //ADVANCED_AMOUNT = pendingAmount;
                        RETURNABLE_AMOUNT = 0;
                    }
                }
            }
        }
        //*30.11.2017*
        private decimal _CUSTOMERPENDING_AMOUNT;
        public decimal CUSTOMERPENDING_AMOUNT
        {
            get
            {

                return SelectInvoice.CUSTOMERPENDING_AMOUNT;
            }
            set
            {
                SelectInvoice.CASH_AMOUNT = value;
                decimal recivedAmount = 0;
                recivedAmount = CUSTOMERPENDING_AMOUNT + CHEQUE_AMOUNT + TRANSFER_AMOUNT + FINANCIAL_AMOUNT;
                decimal pendingAmount = TOTAL_AMOUNT - recivedAmount;
                decimal returnAmount = RECIVED_AMOUNT - TOTAL_AMOUNT;
                if (SelectInvoice.CASH_AMOUNT == value)
                {
                    if (recivedAmount >= TOTAL_AMOUNT)
                    {
                        PENDING_AMOUNT = 0;
                        ADVANCED_AMOUNT = 0;
                        RETURNABLE_AMOUNT = recivedAmount - TOTAL_AMOUNT;
                        //RECIVED_AMOUNT = TOTAL_AMOUNT;
                        RECIVED_AMOUNT = recivedAmount;
                    }
                    else
                    {
                        RECIVED_AMOUNT = recivedAmount;
                        PENDING_AMOUNT = pendingAmount;
                        //ADVANCED_AMOUNT = pendingAmount;
                        RETURNABLE_AMOUNT = 0;
                    }
                }
            }
        }
        private string _NOTE;
        public string NOTE
        {
            get
            {
                return SelectInvoice.NOTE;
            }
            set
            {
                SelectInvoice.NOTE = value;
                if (SelectInvoice.NOTE != value)
                {
                    SelectInvoice.NOTE = value;
                    OnPropertyChanged("NOTE");
                }
            }
        }
        private bool _IS_GOODS_DELIVERED;
        public bool IS_GOODS_DELIVERED
        {
            get
            {
                return SelectInvoice.IS_GOODS_DELIVERED;
            }
            set
            {
                SelectInvoice.IS_GOODS_DELIVERED = value;
                if (SelectInvoice.IS_GOODS_DELIVERED != value)
                {
                    SelectInvoice.IS_GOODS_DELIVERED = value;
                    OnPropertyChanged("IS_GOODS_DELIVERED");
                }
            }
        }
        private bool _IS_PRINT;
        public bool IS_PRINT
        {
            get
            {
                return SelectInvoice.IS_PRINT;
            }
            set
            {
                SelectInvoice.IS_PRINT = value;
                if (SelectInvoice.IS_PRINT != value)
                {
                    SelectInvoice.IS_PRINT = value;
                    OnPropertyChanged("IS_PRINT");
                }
            }
        }
        private bool _SAVE_PRINT;
        public bool SAVE_PRINT
        {
            get
            {
                return SelectInvoice.SAVE_PRINT;
            }
            set
            {
                SelectInvoice.SAVE_PRINT = value;
                if (SelectInvoice.SAVE_PRINT != value)
                {
                    SelectInvoice.SAVE_PRINT = value;
                    OnPropertyChanged("SAVE_PRINT");
                }
            }
        }
        private string _SEARCH_CUS;
        public string SEARCH_CUS
        {
            get
            {
                return _SEARCH_CUS;
            }
            set
            {
                _SEARCH_CUS = value;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                if (_SEARCH_CUS != null)
                {
                    GetInvoice(comp);
                }
                OnPropertyChanged("SEARCH_CUS");
            }
        }

        private bool _IS_SAVE_RETURNABLE_AMOUNT;
        public bool IS_SAVE_RETURNABLE_AMOUNT
        {
            get
            {
                return SelectInvoice.IS_SAVE_RETURNABLE_AMOUNT;
            }
            set
            {
                SelectInvoice.IS_SAVE_RETURNABLE_AMOUNT = value;
                if (SelectInvoice.IS_SAVE_RETURNABLE_AMOUNT != value)
                {
                    SelectInvoice.IS_SAVE_RETURNABLE_AMOUNT = value;
                    OnPropertyChanged("IS_SAVE_RETURNABLE_AMOUNT");
                }
            }
        }

        private DateTime _START_DATE;
        public DateTime STARTDATE
        {
            get
            {
                return _START_DATE;
            }
            set
            {
                _START_DATE = value;
                if (_START_DATE == Convert.ToDateTime("01/01/0001 12:00:00 AM"))
                {
                    _START_DATE = DateTime.Now;
                }
                OnPropertyChanged("STARTDATE");

            }

        }


        private DateTime _END_DATE;
        public DateTime ENDDATE
        {
            get
            {
                return _END_DATE;
            }
            set
            {
                _END_DATE = value;
                if (_END_DATE == Convert.ToDateTime("01/01/0001 12:00:00 AM"))
                {
                    _END_DATE = DateTime.Now;
                }
                OnPropertyChanged("ENDDATE");

            }
        }


        private bool _DATE_CHECK;
        public bool DATE_CHECK
        {
            get
            {
                return _DATE_CHECK;
            }
            set
            {
                _DATE_CHECK = value;
                //GetInvoice1();
                //var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                //GetInvoice1(STARTDATE, ENDDATE);
                GetInvoice1();
                OnPropertyChanged("DATE_CHECK");

            }
        }
        #region GetInvoice
        GetInvoiceModel[] datainvoice = null;
        public List<GetInvoiceModel> _ListGrid { get; set; }
        public List<GetInvoiceModel> ListGrid
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

        public ICommand _SearchSuup;
        public ICommand SearchSupp
        {
            get
            {
                if (_SearchSuup == null)
                {
                    _SearchSuup = new DelegateCommand(Search_Supp);
                }
                return _SearchSuup;
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
            if (App.Current.Properties["InvoiceList"] != null)
            {
                //AddPO.ItemRef.Text = SelectedOpeningStock.ITEM_NAME;
                //AddPO.ScrRef.Text = null;

                SalesReturnAdd.InvoiceReff.Text = SelectInv.INVOICE_NO;
                App.Current.Properties["Inv"] = SelectInv.INVOICE_NO;
                SalesReturnAdd.CusReff.Text = SelectInv.CUSTOMER;
                
                App.Current.Properties["Cus"] = SelectInv.CUSTOMER;
                App.Current.Properties["Invoice_Id"] = SelectInv.INVOICE_ID;
               
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                SalesReturnViewModel SM = new SalesReturnViewModel();
                //SM.GetSalesList(comp);
                SM.GetSalesList();
              
                //POViewModel itn = new POViewModel();
                //itn.GetPOList1(comp);
                App.Current.Properties["InvoiceList"] = null;
            }
            Cancel_Invoice();
        }

        public async void Search_Supp()
        {
            try
            {
                List<GetInvoiceModel> _ListGrid_Temp = new List<GetInvoiceModel>();
                App.Current.Properties["Action"] = "Search";
                string comp = SEARCH_CUS;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/SupplierAPI/SearchSupplier?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());

                    for (int i = 0; i < data.Length; i++)
                    {
                        _ListGrid_Invoice.Add(new GetInvoiceModel
                        {
                            INVOICE_ID = datainvoice[i].INVOICE_ID,
                            AVAILABLE_CREDIT_LIMIT = datainvoice[i].AVAILABLE_CREDIT_LIMIT,
                            BEFORE_ROUNDOFF = datainvoice[i].BEFORE_ROUNDOFF,
                            COMMISION_EXPENSE = datainvoice[i].COMMISION_EXPENSE,
                            CUSTOMER = datainvoice[i].CUSTOMER,
                            CUSTOMER_EMAIL = datainvoice[i].CUSTOMER_EMAIL,
                            CUSTOMER_ID = datainvoice[i].CUSTOMER_ID,
                            CUSTOMER_MOBILE_NO = datainvoice[i].CUSTOMER_MOBILE_NO,
                            INVOICE_NO = datainvoice[i].INVOICE_NO,
                            ITEM_ID = datainvoice[i].ITEM_ID,
                            NOTE = datainvoice[i].NOTE,
                            NUMBER_OF_ITEM = datainvoice[i].NUMBER_OF_ITEM,
                            PENDING_AMOUNT = datainvoice[i].PENDING_AMOUNT,
                            QUANTITY_TOTAL = datainvoice[i].QUANTITY_TOTAL,
                            RECIVED_AMOUNT = datainvoice[i].RECIVED_AMOUNT,
                            RETURNABLE_AMOUNT = datainvoice[i].RETURNABLE_AMOUNT,
                            ROUNDOFF_AMOUNT = datainvoice[i].ROUNDOFF_AMOUNT,
                            SALES_EXECUTIVE = datainvoice[i].SALES_EXECUTIVE,
                            SALES_EXECUTIVE_ID = datainvoice[i].SALES_EXECUTIVE_ID,
                            INVOICE_DATE = datainvoice[i].INVOICE_DATE,
                            TAX_INCLUDED = datainvoice[i].TAX_INCLUDED,
                            TOTAL_AMOUNT = datainvoice[i].TOTAL_AMOUNT,
                            TOTAL_TAX = datainvoice[i].TOTAL_TAX,
                           
                        });
                    }
                }
                ListGrid = _ListGrid_Invoice;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        List<GetInvoiceModel> _ListGrid_Invoice = new List<GetInvoiceModel>();
        public async Task<ObservableCollection<GetInvoiceModel>> GetInvoice(int comp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoice?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    datainvoice = JsonConvert.DeserializeObject<GetInvoiceModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < datainvoice.Length; i++)
                    {
                        x++;
                        _ListGrid_Invoice.Add(new GetInvoiceModel
                        {
                            INVOICE_ID = datainvoice[i].INVOICE_ID,
                            AVAILABLE_CREDIT_LIMIT = datainvoice[i].AVAILABLE_CREDIT_LIMIT,
                            BEFORE_ROUNDOFF = datainvoice[i].BEFORE_ROUNDOFF,
                            COMMISION_EXPENSE = datainvoice[i].COMMISION_EXPENSE,
                            CUSTOMER = datainvoice[i].CUSTOMER,
                            CUSTOMER_EMAIL = datainvoice[i].CUSTOMER_EMAIL,
                            CUSTOMER_ID = datainvoice[i].CUSTOMER_ID,
                            CUSTOMER_MOBILE_NO = datainvoice[i].CUSTOMER_MOBILE_NO,
                            INVOICE_NO = datainvoice[i].INVOICE_NO,
                            ITEM_ID = datainvoice[i].ITEM_ID,
                            NOTE = datainvoice[i].NOTE,
                            NUMBER_OF_ITEM = datainvoice[i].NUMBER_OF_ITEM,
                            PENDING_AMOUNT = datainvoice[i].PENDING_AMOUNT,
                            QUANTITY_TOTAL = datainvoice[i].QUANTITY_TOTAL,
                            RECIVED_AMOUNT = datainvoice[i].RECIVED_AMOUNT,
                            RETURNABLE_AMOUNT = datainvoice[i].RETURNABLE_AMOUNT,
                            ROUNDOFF_AMOUNT = datainvoice[i].ROUNDOFF_AMOUNT,
                            SALES_EXECUTIVE = datainvoice[i].SALES_EXECUTIVE,
                            SALES_EXECUTIVE_ID = datainvoice[i].SALES_EXECUTIVE_ID,
                            INVOICE_DATE = datainvoice[i].INVOICE_DATE,
                            TAX_INCLUDED = datainvoice[i].TAX_INCLUDED,
                            TOTAL_AMOUNT = datainvoice[i].TOTAL_AMOUNT,
                            TOTAL_TAX = datainvoice[i].TOTAL_TAX,
                        });
                    }

                    if (SEARCH_CUS != "" && SEARCH_CUS != null)
                    {
                        //var item1 = (from m in _ListGrid_Invoice where m.SUPPLIER_NAME.Contains(SEARCH_CUS) || m.SUPPLIER_CODE.Contains(SEARCH_CUS) || m.SEARCH_CODE.Contains(SEARCH_CUS) select m).ToList();
                        var item1 = (from m in _ListGrid_Invoice where m.INVOICE_NO.Contains(SEARCH_CUS) select m).ToList();
                        _ListGrid_Invoice = item1;
                    }
                    //if (IS_InACTIVESearch == true)
                    //{
                    //var InActiveSupp = (from m in _ListGrid_Invoice where m.IS_ACTIVE == true select m).ToList();
                    //_ListGrid_Invoice = InActiveSupp;
                    //}
                    ListGrid = _ListGrid_Invoice;
                }

            }
            catch (Exception ex)
            {
            }
            return new ObservableCollection<GetInvoiceModel>(_ListGrid_Invoice);
        }
        #endregion

        #region ViewInvoice
        private GetInvoiceModel _SelectInv;
        public GetInvoiceModel SelectInv
        {

            get { return _SelectInv; }
            set
            {
                _SelectInv = value;
                OnPropertyChanged("SelectInv");
            }

        }
        private ICommand _ViewINvoice { get; set; }
        public ICommand ViewINvoice
        {
            get
            {
                if (_ViewINvoice == null)
                {
                    _ViewINvoice = new DelegateCommand(View_INvoice);
                }
                return _ViewINvoice;
            }

        }

        public async void View_INvoice()
        {
            try
            {
                if (SelectInv.INVOICE_ID != null && SelectInv.INVOICE_ID != 0)
                {
                    BusinessLocation = Convert.ToString(App.Current.Properties["BussLocName"]);
                    var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceById?id=" + SelectInv.INVOICE_ID + "").Result;
                    //HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceItem?id=" + SelectInv.INVOICE_ID + "").Result;
                    {
                        datainvoice = JsonConvert.DeserializeObject<GetInvoiceModel[]>(await response.Content.ReadAsStringAsync());
                        for (int i = 0; i < datainvoice.Length; i++)
                        {
                            SelectInv.INVOICE_ID = datainvoice[i].INVOICE_ID;
                            SelectInv.AVAILABLE_CREDIT_LIMIT = datainvoice[i].AVAILABLE_CREDIT_LIMIT;
                            SelectInv.BEFORE_ROUNDOFF = datainvoice[i].BEFORE_ROUNDOFF;
                            SelectInv.COMMISION_EXPENSE = datainvoice[i].COMMISION_EXPENSE;
                            SelectInv.CUSTOMER = datainvoice[i].CUSTOMER;
                            SelectInv.CUSTOMER_EMAIL = datainvoice[i].CUSTOMER_EMAIL;
                            SelectInv.CUSTOMER_MOBILE_NO = datainvoice[i].CUSTOMER_MOBILE_NO;
                            SelectInv.DISCOUNT_FLAT = datainvoice[i].DISCOUNT_FLAT;
                            SelectInv.INVOICE_DATE = datainvoice[i].INVOICE_DATE;
                            SelectInv.INVOICE_NO = datainvoice[i].INVOICE_NO;
                            SelectInv.NOTE = datainvoice[i].NOTE;
                            SelectInv.NUMBER_OF_ITEM = datainvoice[i].NUMBER_OF_ITEM;
                            SelectInv.PENDING_AMOUNT = datainvoice[i].PENDING_AMOUNT;
                            SelectInv.QUANTITY_TOTAL = datainvoice[i].QUANTITY_TOTAL;
                           // SelectInv.RETURNABLE_AMOUNT = datainvoice[i].RECIVED_AMOUNT;
                            SelectInv.RETURNABLE_AMOUNT = datainvoice[i].RETURNABLE_AMOUNT;
                            SelectInv.TOTAL_AMOUNT = datainvoice[i].TOTAL_AMOUNT;
                            SelectInv.ROUNDOFF_AMOUNT = datainvoice[i].ROUNDOFF_AMOUNT;
                            SelectInv.TAX_INCLUDED = datainvoice[i].TAX_INCLUDED;
                            SelectInv.CASH_REG = datainvoice[i].CASH_REG;
                        }
                        App.Current.Properties["InvoiceView"] = SelectInv;
                        ViewInvoice obj = new ViewInvoice();
                        obj.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Select Item");
                    return;
                }

            }
            catch (Exception ex)
            { }


        }
        ItemModel[] datainvoiceItem = null;
        public List<ItemModel> _ListGridItem { get; set; }
        public List<ItemModel> ListGridItem
        {
            get
            {
                return _ListGridItem;
            }
            set
            {
                this._ListGridItem = value;
                OnPropertyChanged("ListGridItem");
            }
        }
        List<ItemModel> _ListGrid_InvoiceItem = new List<ItemModel>();
        private ItemModel _SelectItem;
        public ItemModel SelectItem
        {

            get { return _SelectItem; }
            set
            {
                _SelectItem = value;
                OnPropertyChanged("SelectItem");
            }

        }
        public async Task<ObservableCollection<ItemModel>> InvoiceItem(int comp)
        {
            try
            {
                var comp1 = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceItem?id=" + SelectInv.INVOICE_ID + "").Result;
                {
                    datainvoiceItem = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < datainvoiceItem.Length; i++)
                    {
                        x++;
                        _ListGrid_InvoiceItem.Add(new ItemModel
                        {

                            SLNO = x,
                            ITEM_NAME = datainvoiceItem[i].ITEM_NAME,
                            BARCODE = datainvoiceItem[i].BARCODE,
                            SEARCH_CODE = datainvoiceItem[i].SEARCH_CODE,
                            TAX_COLLECTED = datainvoiceItem[i].TAX_COLLECTED,
                            Current_Qty = datainvoiceItem[i].Current_Qty,
                            MRP = datainvoiceItem[i].MRP,
                            SALES_PRICE = datainvoiceItem[i].SALES_PRICE,
                            RETURNABLE_AMOUNT = datainvoiceItem[i].RETURNABLE_AMOUNT,
                            SALES_UNIT = datainvoiceItem[i].SALES_UNIT,
                            PURCHASE_UNIT = datainvoiceItem[i].PURCHASE_UNIT,
                            CATEGORY_NAME = datainvoiceItem[i].CATEGORY_NAME,
                            DISPLAY_INDEX = datainvoiceItem[i].DISPLAY_INDEX,
                            ITEM_GROUP_NAME = datainvoiceItem[i].ITEM_GROUP_NAME,
                            REGIONAL_LANGUAGE = datainvoiceItem[i].REGIONAL_LANGUAGE,
                            MODIFICATION_DATE = datainvoiceItem[i].MODIFICATION_DATE,
                            //CASH_REG = datainvoiceItem[i].MODIFICATION_DATE,

                        });
                    }
                    ListGridItem = _ListGrid_InvoiceItem;

                }
            }
            catch (Exception ex)
            { }
            return new ObservableCollection<ItemModel>(_ListGrid_InvoiceItem);
        }


        #endregion

        #region GetInvoice1
        GetInvoiceModel[] datainvoice1 = null;
        public List<GetInvoiceModel> _ListGrid1 { get; set; }
        public List<GetInvoiceModel> ListGrid1
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
        List<GetInvoiceModel> _ListGrid_Invoice1 = new List<GetInvoiceModel>();
        //public async Task<ObservableCollection<GetInvoiceModel>> GetInvoice1(DateTime STARTDATE, DateTime ENDDATE)
        public async Task<ObservableCollection<GetInvoiceModel>> GetInvoice1()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                //HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoice1?id='" + STARTDATE + "','" + ENDDATE + "'").Result;
                HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoice1?STARTDATE=" + STARTDATE + "&ENDDATE=" + ENDDATE + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    datainvoice1 = JsonConvert.DeserializeObject<GetInvoiceModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < datainvoice1.Length; i++)
                    {
                        x++;
                        _ListGrid_Invoice1.Add(new GetInvoiceModel
                        {
                            INVOICE_ID = datainvoice[i].INVOICE_ID,
                            AVAILABLE_CREDIT_LIMIT = datainvoice[i].AVAILABLE_CREDIT_LIMIT,
                            BEFORE_ROUNDOFF = datainvoice[i].BEFORE_ROUNDOFF,
                            COMMISION_EXPENSE = datainvoice[i].COMMISION_EXPENSE,
                            CUSTOMER = datainvoice[i].CUSTOMER,
                            CUSTOMER_EMAIL = datainvoice[i].CUSTOMER_EMAIL,
                            CUSTOMER_ID = datainvoice[i].CUSTOMER_ID,
                            CUSTOMER_MOBILE_NO = datainvoice[i].CUSTOMER_MOBILE_NO,
                            INVOICE_NO = datainvoice[i].INVOICE_NO,
                            ITEM_ID = datainvoice[i].ITEM_ID,
                            NOTE = datainvoice[i].NOTE,
                            NUMBER_OF_ITEM = datainvoice[i].NUMBER_OF_ITEM,
                            PENDING_AMOUNT = datainvoice[i].PENDING_AMOUNT,
                            QUANTITY_TOTAL = datainvoice[i].QUANTITY_TOTAL,
                            RECIVED_AMOUNT = datainvoice[i].RECIVED_AMOUNT,
                            RETURNABLE_AMOUNT = datainvoice[i].RETURNABLE_AMOUNT,
                            ROUNDOFF_AMOUNT = datainvoice[i].ROUNDOFF_AMOUNT,
                            SALES_EXECUTIVE = datainvoice[i].SALES_EXECUTIVE,
                            SALES_EXECUTIVE_ID = datainvoice[i].SALES_EXECUTIVE_ID,
                            INVOICE_DATE = datainvoice[i].INVOICE_DATE,
                            TAX_INCLUDED = datainvoice[i].TAX_INCLUDED,
                            //CASH_REG =   datainvoice[i].CASH_REG;
                            TOTAL_AMOUNT = datainvoice[i].TOTAL_AMOUNT,
                            TOTAL_TAX = datainvoice[i].TOTAL_TAX,
                        });
                    }
                    ListGrid1 = _ListGrid_Invoice1;
                }
            }
            catch (Exception ex)
            {
            }
            return new ObservableCollection<GetInvoiceModel>(_ListGrid_Invoice1);
        }
        #endregion


        private string _ExcelPath;
        public string ExcelPath
        {
            get { return _ExcelPath; }
            set
            {
                if (Equals(value, _ExcelPath)) return;
                _ExcelPath = value;
                OnPropertyChanged("ExcelPath");
            }
        }
        public ICommand _ImportClick { get; set; }
        public ICommand ImportClick
        {
            get
            {
                if (_ImportClick == null)
                {
                    _ImportClick = new DelegateCommand(ImportClick_Click);
                }
                return _ImportClick;
            }
        }
        public void ImportClick_Click()
        {
           // ImportDataForInvoice sh = new ImportDataForInvoice();
            //sh.Show();
        }

        public ICommand _ExcelImportClick { get; set; }
        public ICommand ExcelImportClick
        {
            get
            {
                if (_ExcelImportClick == null)
                {
                    _ExcelImportClick = new DelegateCommand(ExcelImport_Click);
                }
                return _ExcelImportClick;
            }
        }
        public void ExcelImport_Click()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel file(*.xlsm;*.xlsx;*.xlsx;*.xlt; *.xlk;)|*.xlsm;*.xlsx;*.xlsx;*.xlt; *xlk";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
            }
            if (File.Exists(openFileDialog.FileName))
            {
                string xx = openFileDialog.FileName;
                ExcelPath = openFileDialog.FileName;
                var excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xx + ";Extended Properties=Excel 12.0;");
                OleDbConnection objOlecon = new OleDbConnection();
                objOlecon.ConnectionString = excelConnectionString;
                objOlecon.Open();
                OleDbDataAdapter objOleDa = new OleDbDataAdapter("Select * from [Sheet1$]", objOlecon);
                DataTable objdt = new DataTable();
                objOleDa.Fill(objdt);
                List<GetInvoiceModel> _ListGridTemp = new List<GetInvoiceModel>();
                if (objdt.Rows.Count > 0)
                {
                    for (int i = 0; i < objdt.Rows.Count; i++)
                    {
                        var df = objdt.Rows[0];

                        _ListGridTemp.Add(new GetInvoiceModel
                        {
                            INVOICE_NO = Convert.ToString(objdt.Rows[i].ItemArray[0]),
                            INVOICE_DATE = Convert.ToDateTime(objdt.Rows[i].ItemArray[1]),
                            TOTAL_AMOUNT = Convert.ToString(objdt.Rows[i].ItemArray[2]),
                            TOTAL_TAX = Convert.ToDecimal(objdt.Rows[i].ItemArray[3]),
                            QUANTITY_TOTAL = Convert.ToString(objdt.Rows[i].ItemArray[4]),
                            RETURNABLE_AMOUNT = Convert.ToString(objdt.Rows[i].ItemArray[5]),
                            PENDING_AMOUNT = Convert.ToString(objdt.Rows[i].ItemArray[6]),
                            CUSTOMER = Convert.ToString(objdt.Rows[i].ItemArray[7]),
                            CUSTOMER_MOBILE_NO = Convert.ToString(objdt.Rows[i].ItemArray[8]),
                            //INVOICE_NO = Convert.ToString(objdt.Rows[i].ItemArray[9]),
                            //CASH_REG = Convert.ToString(objdt.Rows[i].ItemArray[10]),
                            NUMBER_OF_ITEM = Convert.ToString(objdt.Rows[i].ItemArray[11]),
                        });
                    }


                }
                ListGrid = _ListGridTemp;
                App.Current.Properties["ExcelData"] = SelectInv;

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
        //pdf generate for invoice(09.11.2017)
        //private void GeneratePDF(string path, string fileName, bool download, string text)

        //{

        //    var document = new Document();

        //    try

        //    {
        //        if (download)

        //        {

        //            PdfWriter.GetInstance(document, Response.OutputStream);
        //        }

        //        else

        //        {
        //            PdfWriter.GetInstance(document, new FileStream(path + fileName, FileMode.Create));
        //        }



        //        // generates the grid first

        //        StringBuilder strB = new StringBuilder();

        //        document.Open();

        //        if (text.Length.Equals(0)) // export the text

        //        {
        //            BindMyGrid();
        //            using (StringWriter sWriter = new StringWriter(strB))

        //            {

        //                using (HtmlTextWriter htWriter = new HtmlTextWriter(sWriter))

        //                {

        //                    GridView1.RenderControl(htWriter);

        //                }
        //            }
        //        }

        //        else // export the grid

        //        {

        //            strB.Append(text);
        //        }


        //        // now read the Grid html one by one and add into the document object

        //        using (TextReader sReader = new StringReader(strB.ToString()))

        //        {

        //            List<IElement> list = HTMLWorker.ParseToList(sReader, new StyleSheet());

        //            foreach (IElement elm in list)

        //            {

        //                document.Add(elm);
        //            }
        //        }
        //    }

        //    catch (Exception ee)

        //    {

        //        lblMessage.Text = ee.ToString();
        //    }

        //    finally

        //    {

        //        document.Close();
        //    }
        //}
        //protected void GenerateOnlyPDF(object sender, EventArgs e)

        //{

        //    string path = Server.MapPath("~/");

        //    string fileName = "pdfDocument" + DateTime.Now.Ticks + ".pdf";



        //    GeneratePDF(path, fileName, false, "");
        //}
        //protected void GeneratePDFAndDownload(object sender, EventArgs e)

        //{

        //    string fileName = "pdfDocument" + DateTime.Now.Ticks + ".pdf";



        //    GeneratePDF("", fileName, true, "");



        //    Response.Clear();

        //    Response.ContentType = "application/pdf";

        //    Response.AddHeader("content-disposition", "attachment; filename=" + fileName);

        //    Response.Flush();

        //    Response.End();
        //}
        //public void GenerateInvoicePDF(object sender, EventArgs e)
        //{
        //    //Dummy data for Invoice (Bill).
        //    string companyName = "ASPSnippets";
        //    int orderNo = 2303;
        //    DataTable dt = new DataTable();
        //    dt.Columns.AddRange(new DataColumn[5] {
        //                    new DataColumn("ProductId", typeof(string)),
        //                    new DataColumn("Product", typeof(string)),
        //                    new DataColumn("Price", typeof(int)),
        //                    new DataColumn("Quantity", typeof(int)),
        //                    new DataColumn("Total", typeof(int))});
        //    dt.Rows.Add(101, "Sun Glasses", 200, 5, 1000);
        //    dt.Rows.Add(102, "Jeans", 400, 2, 800);
        //    dt.Rows.Add(103, "Trousers", 300, 3, 900);
        //    dt.Rows.Add(104, "Shirts", 550, 2, 1100);

        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //        {
        //            StringBuilder sb = new StringBuilder();

        //            //Generate Invoice (Bill) Header.
        //            sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
        //            sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Order Sheet</b></td></tr>");
        //            sb.Append("<tr><td colspan = '2'></td></tr>");
        //            sb.Append("<tr><td><b>Order No: </b>");
        //            sb.Append(orderNo);
        //            sb.Append("</td><td align = 'right'><b>Date: </b>");
        //            sb.Append(DateTime.Now);
        //            sb.Append(" </td></tr>");
        //            sb.Append("<tr><td colspan = '2'><b>Company Name: </b>");
        //            sb.Append(companyName);
        //            sb.Append("</td></tr>");
        //            sb.Append("</table>");
        //            sb.Append("<br />");

        //            //Generate Invoice (Bill) Items Grid.
        //            sb.Append("<table border = '1'>");
        //            sb.Append("<tr>");
        //            foreach (DataColumn column in dt.Columns)
        //            {
        //                sb.Append("<th style = 'background-color: #D20B0C;color:#ffffff'>");
        //                sb.Append(column.ColumnName);
        //                sb.Append("</th>");
        //            }
        //            sb.Append("</tr>");
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                sb.Append("<tr>");
        //                foreach (DataColumn column in dt.Columns)
        //                {
        //                    sb.Append("<td>");
        //                    sb.Append(row[column]);
        //                    sb.Append("</td>");
        //                }
        //                sb.Append("</tr>");
        //            }
        //            sb.Append("<tr><td align = 'right' colspan = '");
        //            sb.Append(dt.Columns.Count - 1);
        //            sb.Append("'>Total</td>");
        //            sb.Append("<td>");
        //            sb.Append(dt.Compute("sum(Total)", ""));
        //            sb.Append("</td>");
        //            sb.Append("</tr></table>");

        //            //Export HTML String as PDF.
        //            StringReader sr = new StringReader(sb.ToString());
        //            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //            //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //            //pdfDoc.Open();
        //            //htmlparser.Parse(sr);
        //            //pdfDoc.Close();
        //            //Response.ContentType = "application/pdf";
        //            //Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + orderNo + ".pdf");
        //            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            //Response.Write(pdfDoc);
        //            //Response.End();
        //        }
        //    }
        //}

        private bool _GENERAT_PRINT;
        public bool GENERATE_PRINT
        {
            get
            {
                return SelectInvoice.GENERATE_PRINT;
            }
            set
            {
                SelectInvoice.GENERATE_PRINT = value;
                if (SelectInvoice.GENERATE_PRINT != value)
                {
                    SelectInvoice.GENERATE_PRINT = value;
                    OnPropertyChanged("GENERATE_PRINT");
                }
            }

        }

        //public static BitmapSource CopyScreen()
        //{
        //    using (var screenBmp = new Bitmap(
        //       (int)SystemParameters.PrimaryScreenWidth,
        //        (int)SystemParameters.PrimaryScreenHeight,
        //       PixelFormat.Format32bppArgb))
        //    {
        //        using (var bmpGraphics = Graphics.FromImage(screenBmp))
        //        {
        //            bmpGraphics.CopyFromScreen(0, 0, 0, 0, screenBmp.Size);
        //            return Imaging.CreateBitmapSourceFromHBitmap(
        //                screenBmp.GetHbitmap(),
        //                IntPtr.Zero,
        //                Int32Rect.Empty,
        //                BitmapSizeOptions.FromEmptyOptions());
        //        }
        //    }
        //}
    }
}
