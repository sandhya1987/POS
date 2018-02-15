using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.Customer;
using InvoicePOS.UserControll.Payment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace InvoicePOS.ViewModels
{
    public class ReceivePaymentViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {

        public HttpResponseMessage response;
        // public HttpClient client = new HttpClient();
        public ICommand select { get; set; }
        private readonly RecPayModel OprModel;
        RecPayModel _RecPayModel = new RecPayModel();
        public GetReceiveAmt _RecPayModel1 = new GetReceiveAmt();
        private RecPayModel _SelectRecPay;
        RecPayModel[] data = null;
        GetReceiveAmt[] data1 = null;
        CashRegModel[] getcash=null;
        List<RecPayModel> _ListGrid_Temp = new List<RecPayModel>();
        ObservableCollection<GetReceiveAmt> _ListGrid_Temp1 = new ObservableCollection<GetReceiveAmt>();
        ObservableCollection<CashRegModel> _getcashreg = new ObservableCollection<CashRegModel>();
        decimal amount = 0;
        decimal amount1 = 0;
        public string Error
        {
            get { throw new NotImplementedException(); }
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
        public void getcheck(RoutedEventArgs e)
        {
            try
            {


                CheckBox checkBox = (CheckBox)e.OriginalSource;
                DataGridRow dataGridRow = FindAncestor<DataGridRow>(checkBox);

                GetReceiveAmt produit1 = (GetReceiveAmt)dataGridRow.DataContext;
                if ((bool)checkBox.IsChecked)
                {
                    amount = Convert.ToDecimal(produit1.Pending_Amount) + amount;
                }
                else
                {
                    amount = amount - Convert.ToDecimal(produit1.Pending_Amount);
                }

                TOTAL_SELECTED_PAY_AMT = amount;
                //showpendingamt.Text = amount.ToString();
                produit1.TOTAL_REC_AMT1 = amount;
                App.Current.Properties["pendamt"] = amount;

            }
            catch
            { }
        }
        public static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            current = VisualTreeHelper.GetParent(current);
            while (current != null)
            {
                if (current is T)
                {
                    return (T)current;
                }

                current = VisualTreeHelper.GetParent(current);
            };
            return null;
        }
        public ReceivePaymentViewModel()
        {

            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectRecPay = App.Current.Properties["ItemEdit"] as RecPayModel;
                App.Current.Properties["Action"] = "";
                // GetItem(comp);
            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                SelectRecPay = App.Current.Properties["ItemView"] as RecPayModel;
                App.Current.Properties["Action"] = "";
                App.Current.Properties["ItemView"] = "";
                // GetItem(comp);
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                SelectRecPay = new RecPayModel();
                DATE = System.DateTime.Now;
                if (App.Current.Properties["CustomerName"] != null && App.Current.Properties["CustomerName"] != "")
                {
                    SelectedCustomer = App.Current.Properties["CustomerName"] as CustomerModel;
                    //SelectRecPay.BUSINESS_LOCATION = SelectedCustomer.BUSINESS_LOCATION;
                    //SelectRecPay.CUSTOMER_CONTACT_NO = SelectedCustomer.SHIPPING_MOBILE_NO;
                    //SelectRecPay.CUSTOMER_EMAIL = SelectedCustomer.EMAIL_ADDRESS;
                    //SelectRecPay.CUSTOMER = SelectedCustomer.NAME;
                    App.Current.Properties["CustomerName"] = "";
                }

                GetRecPay(comp);

            }
            GetSpplierNo();
            IsViewMode = true;
            //GetRecPayByUser(4);
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

        private List<RecPayModel> _RecPayData;
        public List<RecPayModel> RecPayData
        {
            get { return _RecPayData; }
            set
            {
                if (_RecPayData != value)
                {
                    _RecPayData = value;
                }
            }

        }
        private List<GetReceiveAmt> _RecPayData1;
        public List<GetReceiveAmt> RecPayData1
        {
            get { return _RecPayData1; }
            set
            {
                if (_RecPayData1 != value)
                {
                    _RecPayData1 = value;
                }
            }

        }
        private string _SEARCH_REC;
        public string SEARCH_REC
        {
            get
            {
                return _SEARCH_REC;
            }
            set
            {


                if (_SEARCH_REC != value)
                {
                    _SEARCH_REC = value;

                    if (_SEARCH_REC != "" && _SEARCH_REC != null)
                    {

                        GetRecPay(COMPANY_ID);
                    }
                    else
                    {
                        GetRecPay(COMPANY_ID);

                    }
                    OnPropertyChanged("SEARCH_REC");
                }
            }
        }
        public async Task<ObservableCollection<RecPayModel>> GetRecPay(int comp)
        {
            try
            {
                RecPayData = new List<RecPayModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/RecPayAPI/GetRecPay?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<RecPayModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new RecPayModel
                        {
                            SLNO = x,
                            BUSINESS_LOCATION = data[i].BUSINESS_LOCATION,
                            BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID,
                            COMPANY_ID = data[i].COMPANY_ID,
                            CURRENT_PAY_AMT = data[i].CURRENT_PAY_AMT,
                            CUSTOMER = data[i].CUSTOMER,
                            CUSTOMER_CONTACT_NO = data[i].CUSTOMER_CONTACT_NO,
                            CUSTOMER_EMAIL = data[i].CUSTOMER_EMAIL,
                            CUSTOMER_ID = data[i].CUSTOMER_ID,
                            DATE = data[i].DATE,
                            IS_EMAIL_SEND = data[i].IS_EMAIL_SEND,
                            OTHER_PAY_AMT = data[i].OTHER_PAY_AMT,
                            PENDING_AMT = data[i].PENDING_AMT,
                            RECEIVE_PAY_NO = data[i].RECEIVE_PAY_NO,
                            RECEIVE_PAYMENT_ID = data[i].RECEIVE_PAYMENT_ID,
                            RETURNABLE_AMT = data[i].RETURNABLE_AMT,
                            TOTAL_REC_AMT = data[i].TOTAL_REC_AMT,
                            TOTAL_SELECTED_PAY_AMT = data[i].TOTAL_SELECTED_PAY_AMT,
                        });
                    }
                    if (SEARCH_REC != "" && SEARCH_REC != null)
                    {
                        var item1 = (from m in _ListGrid_Temp where m.RECEIVE_PAY_NO.Contains(SEARCH_REC) select m).ToList();

                        _ListGrid_Temp = item1;
                    }
                    ListGrid = _ListGrid_Temp;
                    return new ObservableCollection<RecPayModel>(_ListGrid_Temp);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            ListGrid = _ListGrid_Temp;
            return new ObservableCollection<RecPayModel>(_ListGrid_Temp);
        }
        public async Task<ObservableCollection<GetReceiveAmt>> GetRecPayByUser(int comp)
        {
            _ListGrid1 = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceByCustId?id=" + comp + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data1 = JsonConvert.DeserializeObject<GetReceiveAmt[]>(await response.Content.ReadAsStringAsync());
                // POData = new List<POrderModel>();
                // int x = 0;
                for (int i = 0; i < data1.Length; i++)
                {

                    _ListGrid_Temp1.Add(new GetReceiveAmt
                    {
                        SLNO = i + 1,

                        //PO_DATE = data1[i].PO_DATE,

                        Invoice_Number = data1[i].INVOICE_NO,
                        Invoice_Date = data1[i].Invoice_Date,
                        Pending_Amount = data1[i].Pending_Amount,
                        Penalty_Amount = data1[i].Penalty_Amount,
                        IS_EMAIL_SEND = false,
                        //PURCHASE_PRICE_BEFORE_TAX = ((decimal)(data1[i].PURCHASE_UNIT_PRICE * 100) / ((data1[i].TAX_PAID) + 100)),
                        //SUB_TOTAL_AFTER_TAX = ((decimal)(data1[i].Current_Qty) * (data1[i].PURCHASE_UNIT_PRICE)),
                        //SUB_TOTAL_BEFORE_TAX = ((decimal)(data1[i].Current_Qty) * (PURCHASE_PRICE_BEFORE_TAX)),

                    });
                }



                //_ListGrid_PoItem = _ListGrid_PoItem;
                ListGrid1 = _ListGrid_Temp1;
                //App.Current.Properties["EditGridData"] = ListGrid1;




            }
            return new ObservableCollection<GetReceiveAmt>(_ListGrid_Temp1);
        }
        //public async Task<ObservableCollection<GetReceiveAmt>> GetRecPayByUser(int comp)
        //{
        //    try
        //    {
        //        RecPayData1 = new List<GetReceiveAmt>();
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(GlobalData.gblApiAdress);
        //        client.DefaultRequestHeaders.Accept.Add(
        //            new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.Timeout = new TimeSpan(500000000000);
        //        HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceByCustId?id=" + comp + "").Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            data1 = JsonConvert.DeserializeObject<GetReceiveAmt[]>(await response.Content.ReadAsStringAsync());
        //            int x = 0;
        //            for (int i = 0; i < data1.Length; i++)
        //            {
        //                x++;
        //                _ListGrid_Temp1.Add(new GetReceiveAmt
        //                {
        //                    SLNO = x,
        //                    Invoice_Number = data1[i].INVOICE_NO,
        //                    BUSINESS_LOCATION = data1[i].BUSINESS_LOCATION,
        //                    BUSINESS_LOCATION_ID = data1[i].BUSINESS_LOCATION_ID,
        //                    COMPANY_ID = data1[i].COMPANY_ID,
        //                    CURRENT_PAY_AMT = data1[i].CURRENT_PAY_AMT,
        //                    CUSTOMER = data1[i].CUSTOMER,
        //                    CUSTOMER_CONTACT_NO = data1[i].CUSTOMER_CONTACT_NO,
        //                    CUSTOMER_EMAIL = data1[i].CUSTOMER_EMAIL,
        //                    CUSTOMER_ID = data1[i].CUSTOMER_ID,
        //                    DATE = data1[i].DATE,
        //                    IS_EMAIL_SEND = data1[i].IS_EMAIL_SEND,
        //                    OTHER_PAY_AMT = data1[i].OTHER_PAY_AMT,
        //                    PENDING_AMT = data1[i].PENDING_AMT,
        //                    RECEIVE_PAY_NO = data1[i].RECEIVE_PAY_NO,
        //                    RECEIVE_PAYMENT_ID = data1[i].RECEIVE_PAYMENT_ID,
        //                    RETURNABLE_AMT = data1[i].RETURNABLE_AMT,
        //                    TOTAL_REC_AMT = data1[i].TOTAL_REC_AMT,
        //                    TOTAL_SELECTED_PAY_AMT = data1[i].TOTAL_SELECTED_PAY_AMT,
        //                });
        //            }
        //            //if (SEARCH_REC != "" && SEARCH_REC != null)
        //            //{
        //            //    var item1 = (from m in _ListGrid_Temp1 where m.RECEIVE_PAY_NO.Contains(SEARCH_REC) select m).ToList();

        //            //    _ListGrid_Temp1 = item1;
        //            //}
        //            ListGrid1 = _ListGrid_Temp1;
        //            return new ObservableCollection<GetReceiveAmt>(_ListGrid_Temp1);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    ListGrid1 = _ListGrid_Temp1;
        //    return new ObservableCollection<GetReceiveAmt>(_ListGrid_Temp1);
        //}
        public List<RecPayModel> _ListGrid { get; set; }
        public List<RecPayModel> ListGrid
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
        public ObservableCollection<GetReceiveAmt> _ListGrid1 { get; set; }
        public ObservableCollection<GetReceiveAmt> ListGrid1
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
        public RecPayModel SelectRecPay
        {
            get { return _SelectRecPay; }
            set
            {
                if (_SelectRecPay != value)
                {
                    _SelectRecPay = value;
                    OnPropertyChanged("SelectRecPay");
                }
            }
        }
        private string _RECEIVE_PAY_NO;
        public string RECEIVE_PAY_NO
        {
            get
            {
                return SelectRecPay.RECEIVE_PAY_NO;
            }
            set
            {
                SelectRecPay.RECEIVE_PAY_NO = value;

                if (SelectRecPay.RECEIVE_PAY_NO != value)
                {
                    SelectRecPay.RECEIVE_PAY_NO = value;
                    OnPropertyChanged("RECEIVE_PAY_NO");
                }
            }
        }
        public string pomodel_code
        {
            get
            {
                return SelectRecPay.RECEIVE_PAY_NO;
            }
            set
            {
                SelectRecPay.RECEIVE_PAY_NO = value;
                OnPropertyChanged("pomodel_code");

            }
        }
        private string _RECEIVE_PAY;
        public string RECEIVE_PAY
        {
            get
            {
                return SelectRecPay.RECEIVE_PAY_NO;
            }
            set
            {
                SelectRecPay.RECEIVE_PAY_NO = value;

                if (SelectRecPay.RECEIVE_PAY_NO != value)
                {
                    SelectRecPay.RECEIVE_PAY_NO = value;
                    OnPropertyChanged("RECEIVE_PAY");
                }
            }
        }

        private long _BUSINESS_LOCATION_ID;
        public long BUSINESS_LOCATION_ID
        {
            get
            {
                return SelectRecPay.BUSINESS_LOCATION_ID;
            }
            set
            {
                SelectRecPay.BUSINESS_LOCATION_ID = value;

                if (SelectRecPay.BUSINESS_LOCATION_ID != value)
                {
                    SelectRecPay.BUSINESS_LOCATION_ID = value;
                    OnPropertyChanged("BUSINESS_LOCATION_ID");
                }
            }
        }
        private string _BUSINESS_LOCATION;
        public string BUSINESS_LOCATION
        {
            get
            {
                return SelectRecPay.BUSINESS_LOCATION;
            }
            set
            {
                SelectRecPay.BUSINESS_LOCATION = value;

                if (SelectRecPay.BUSINESS_LOCATION != value)
                {
                    SelectRecPay.BUSINESS_LOCATION = value;
                    OnPropertyChanged("BUSINESS_LOCATION");
                }
            }
        }
        private int _COMPANY_ID;
        public int COMPANY_ID
        {
            get
            {
                return SelectRecPay.COMPANY_ID;
            }
            set
            {
                SelectRecPay.COMPANY_ID = value;

                if (SelectRecPay.COMPANY_ID != value)
                {
                    SelectRecPay.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }
        private DateTime _DATE;
        public DateTime DATE
        {
            get
            {
                return SelectRecPay.DATE;
            }
            set
            {
                SelectRecPay.DATE = value;

                if (SelectRecPay.DATE != value)
                {
                    SelectRecPay.DATE = System.DateTime.Now;
                    OnPropertyChanged("DATE");
                }
            }
        }
        private long _CUSTOMER_ID;
        public long CUSTOMER_ID
        {
            get
            {
                return SelectRecPay.CUSTOMER_ID;
            }
            set
            {
                SelectRecPay.CUSTOMER_ID = value;

                if (SelectRecPay.CUSTOMER_ID != value)
                {
                    SelectRecPay.CUSTOMER_ID = value;
                    OnPropertyChanged("CUSTOMER_ID");
                }
            }
        }
        private string _CUSTOMER;
        public string CUSTOMER
        {
            get
            {
                return SelectRecPay.CUSTOMER;
            }
            set
            {


                SelectRecPay.CUSTOMER = value;
                OnPropertyChanged("CUSTOMER");

                if (App.Current.Properties["CustomerName"] != null)
                {
                    SelectRecPay.CUSTOMER = App.Current.Properties["CustomerName"].ToString();
                }
                if (App.Current.Properties["GetCustomerId"] != null)
                {
                    SelectRecPay.CUSTOMER_ID = Convert.ToInt64(App.Current.Properties["GetCustomerId"]);
                }
                if (App.Current.Properties["CustomerMobile"] != null)
                {
                    CUSTOMER_CONTACT_NO = App.Current.Properties["CustomerMobile"].ToString();
                }
                if (App.Current.Properties["GetCustomerId"] != null)
                {
                    GetRecPayByUser(Convert.ToInt32(App.Current.Properties["GetCustomerId"]));
                    App.Current.Properties["GetCustomerId"] = null;
                }
            }
        }

        private string _CUSTOMER_EMAIL;
        public string CUSTOMER_EMAIL
        {
            get
            {
                return SelectRecPay.CUSTOMER_EMAIL;
            }
            set
            {
                SelectRecPay.CUSTOMER_EMAIL = value;

                if (App.Current.Properties["CustomerEmailaddress"] != null)
                {
                    CUST_EMAIL = App.Current.Properties["CustomerEmailaddress"].ToString();
                }
                if (SelectRecPay.CUSTOMER_EMAIL != value)
                {
                    SelectRecPay.CUSTOMER_EMAIL = value;
                    OnPropertyChanged("CUSTOMER_EMAIL");
                }
            }
        }
        private string _CUST_EMAIL;
        public string CUST_EMAIL
        {
            get
            {
                return SelectRecPay.CUSTOMER_EMAIL;
            }
            set
            {
                SelectRecPay.CUSTOMER_EMAIL = value;


                if (SelectRecPay.CUSTOMER_EMAIL != value)
                {
                    SelectRecPay.CUSTOMER_EMAIL = value;
                    OnPropertyChanged("CUST_EMAIL");
                }
            }
        }
        private string _CASH_REG_AMT;
        public string CASH_REG_AMT
        {
            get
            {
                return _CASH_REG_AMT;
            }
            set
            {
                _CASH_REG_AMT = value;


                //if (_RecPayModel1.TOTAL_REC_AMT1 != value)
                //{
                //    _RecPayModel1.TOTAL_REC_AMT1 = value;
                OnPropertyChanged("CASH_REG_AMT");
                //}
            }
        }
        private string _subAmount;
        public string subAmount
        {
            get
            {
                return _subAmount;
            }
            set
            {
                _subAmount = value;


                //if (_RecPayModel1.TOTAL_REC_AMT1 != value)
                //{
                //    _RecPayModel1.TOTAL_REC_AMT1 = value;
                OnPropertyChanged("subAmount");
                //}
            }
        }
        private bool _IS_EMAIL_SEND;
        public bool IS_EMAIL_SEND
        {
            get
            {
                return SelectRecPay.IS_EMAIL_SEND;
            }
            set
            {
                SelectRecPay.IS_EMAIL_SEND = value;

                if (SelectRecPay.IS_EMAIL_SEND != value)
                {
                    SelectRecPay.IS_EMAIL_SEND = value;
                    OnPropertyChanged("IS_EMAIL_SEND");
                }
            }
        }
        private string _Invoice_Number;
        public string Invoice_Number
        {
            get
            {
                return _RecPayModel1.Invoice_Number;
            }
            set
            {
                _RecPayModel1.Invoice_Number = value;


                if (_RecPayModel1.Invoice_Number != value)
                {
                    _RecPayModel1.Invoice_Number = value;


                    OnPropertyChanged("Invoice_Number");
                }
            }
        }

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
        public ICommand _UpdAMT;

        public ICommand UpdAMT
        {
            get
            {
                if (_UpdAMT == null)
                {
                    _UpdAMT = new DelegateCommand(upamt_click);
                }
                return _UpdAMT;
            }

        }
        public void upamt_click()
        {
            //Supplier_Enable = false;
            //VisibleMyCode = "Collapsed";
            //VisibleAutoCode = "Visible";
            CASH_REG_AMT = SelectRecPay.TOTAL_SELECTED_PAY_AMT.ToString();

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
        public async Task<string> GetSpplierNo()
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
                HttpResponseMessage response = client.GetAsync("api/RecPayAPI/GetReceivePayID/").Result;
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                        uhy = await response.Content.ReadAsStringAsync();
                        string dd = Convert.ToString(uhy);
                        string aaa = dd.Substring(3, 5);
                        int xx = Convert.ToInt32(aaa);
                        int noo = Convert.ToInt32(xx + 1);
                        nnnn = "RP" + noo.ToString("D5");
                        pomodel_code = nnnn;
                    }
                    catch
                    {
                        pomodel_code = "RP00001";
                    }

                }
                else
                {
                    pomodel_code = "RP00001";
                }
            }
            catch (Exception ex)
            { }

            return uhy;
        }
        private ObservableCollection<CashRegModel> _selectedItem;
        public ObservableCollection<CashRegModel> SelectedItemcash
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
        public async Task<string> GetCash()
        {

            decimal amount = 0;
            string uhy = "";
            try
            {
                string nnnn = "";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CashRegAPI/GetAllCashReg?id=" + 1 + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        getcash = JsonConvert.DeserializeObject<CashRegModel[]>(await response.Content.ReadAsStringAsync());
                        App.Current.Properties["Getd"] = getcash[1].CASH_AMOUNT;
                        //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                        for (int i = 0; i < getcash.Length; i++)
                        {
                            _getcashreg.Add(new CashRegModel
                            {

                                //PO_DATE = data1[i].PO_DATE,

                                CASH_AMOUNT = getcash[1].CASH_AMOUNT - Convert.ToDecimal(CASH_REG_AMT),

                            });
                        }

                        SelectedItemcash = _getcashreg;
                    }
                    catch
                    {

                    }

                }
                else
                {

                }
            }
            catch (Exception ex)
            { }

            return uhy;
        }
        public void SupMyCode_Click()
        {
            //Supplier_Enable = false;
            //VisibleMyCode = "Collapsed";
            //VisibleAutoCode = "Visible";

            if (ButtonText == "Auto Generate")
            {
                ButtonText = "Define My Own";
                GetSpplierNo();

                //_isviewmode = true;
                IsViewMode = true;

            }
            else if (ButtonText == "Define My Own")
            {
                pomodel_code = "";
                ButtonText = "Auto Generate";
                //_isviewmode = false;
                IsViewMode = false;
            }
            //SelectedPO.PO_NUMBER1 = pomodel_code;


        }
        public bool _isviewmode;
        public bool _isenable;
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
        private string _CUSTOMER_CONTACT_NO;
        public string CUSTOMER_CONTACT_NO
        {
            get
            {
                return SelectRecPay.CUSTOMER_CONTACT_NO;
            }
            set
            {
                SelectRecPay.CUSTOMER_CONTACT_NO = value;
                if (App.Current.Properties["CustomerMobile"] != null)
                {
                    SelectRecPay.CUSTOMER_CONTACT_NO = App.Current.Properties["CustomerMobile"].ToString();
                }
                if (SelectRecPay.CUSTOMER_CONTACT_NO != value)
                {
                    SelectRecPay.CUSTOMER_CONTACT_NO = value;
                    OnPropertyChanged("CUSTOMER_CONTACT_NO");
                }
            }
        }
        private decimal _OTHER_PAY_AMT;
        public decimal OTHER_PAY_AMT
        {
            get
            {
                return SelectRecPay.OTHER_PAY_AMT;
            }
            set
            {
                SelectRecPay.OTHER_PAY_AMT = value;

                if (SelectRecPay.OTHER_PAY_AMT != value)
                {
                    SelectRecPay.OTHER_PAY_AMT = value;
                    OnPropertyChanged("OTHER_PAY_AMT");
                }
            }
        }
        private decimal _CURRENT_PAY_AMT;
        public decimal CURRENT_PAY_AMT
        {
            get
            {
                return SelectRecPay.CURRENT_PAY_AMT;
            }
            set
            {
                SelectRecPay.CURRENT_PAY_AMT = value;

                if (SelectRecPay.CURRENT_PAY_AMT != value)
                {
                    SelectRecPay.CURRENT_PAY_AMT = value;
                    OnPropertyChanged("CURRENT_PAY_AMT");
                }
            }
        }
        private decimal _TOTAL_SELECTED_PAY_AMT;
        public decimal TOTAL_SELECTED_PAY_AMT
        {
            get
            {
                return _RecPayModel1.TOTAL_REC_AMT1;
            }
            set
            {
                _RecPayModel1.TOTAL_REC_AMT1 = value;

                //if (_RecPayModel1.TOTAL_REC_AMT1 != value)
                //{
                //    _RecPayModel1.TOTAL_REC_AMT1 = value;
                OnPropertyChanged("SelectRecPay.TOTAL_SELECTED_PAY_AMT");
                //}
            }
        }
        private decimal _PENDING_AMT;
        public decimal PENDING_AMT
        {
            get
            {
                return SelectRecPay.PENDING_AMT;
            }
            set
            {
                SelectRecPay.PENDING_AMT = value;

                if (SelectRecPay.PENDING_AMT != value)
                {
                    SelectRecPay.PENDING_AMT = value;
                    OnPropertyChanged("PENDING_AMT");
                }
            }
        }
        private decimal _TOTAL_REC_AMT;
        public decimal TOTAL_REC_AMT
        {
            get
            {
                return SelectRecPay.TOTAL_REC_AMT;
            }
            set
            {
                SelectRecPay.TOTAL_REC_AMT = value;

                if (SelectRecPay.TOTAL_REC_AMT != value)
                {
                    SelectRecPay.TOTAL_REC_AMT = value;
                    OnPropertyChanged("TOTAL_REC_AMT");
                }
            }
        }
        private decimal _RETURNABLE_AMT;
        public decimal RETURNABLE_AMT
        {
            get
            {
                return SelectRecPay.RETURNABLE_AMT;
            }
            set
            {
                SelectRecPay.RETURNABLE_AMT = value;

                if (SelectRecPay.RETURNABLE_AMT != value)
                {
                    SelectRecPay.RETURNABLE_AMT = value;
                    OnPropertyChanged("RETURNABLE_AMT");
                }
            }
        }


        public ICommand _UpdateRecpay;
        public ICommand UpdateRecpay
        {
            get
            {
                if (_UpdateRecpay == null)
                {
                    _UpdateRecpay = new DelegateCommand(Update_RevPay);
                }
                return _UpdateRecpay;
            }
        }

        public async void Update_RevPay()
        {
            if (SelectRecPay.BUSINESS_LOCATION == null || SelectRecPay.BUSINESS_LOCATION == "")
            {
                MessageBox.Show("BUSINESS LOCATION should not blank");
            }
            else if (SelectRecPay.CUSTOMER == null || SelectRecPay.CUSTOMER == "")
            {
                MessageBox.Show("CUSTOMER should not blank");
            }
            else if (SelectRecPay.CUSTOMER_EMAIL == null || SelectRecPay.CUSTOMER_EMAIL == "")
            {
                MessageBox.Show("Email should not blank");
            }
            else if (SelectRecPay.CUSTOMER_CONTACT_NO == null || SelectRecPay.CUSTOMER_CONTACT_NO == "")
            {
                MessageBox.Show("CONTACT NO should not blank");
            }
            else if (SelectRecPay.TOTAL_SELECTED_PAY_AMT == null || SelectRecPay.TOTAL_SELECTED_PAY_AMT == 0)
            {
                MessageBox.Show("PAY AMOUNT should not blank");
            }
            else if (SelectRecPay.PENDING_AMT == null || SelectRecPay.PENDING_AMT == 0)
            {
                MessageBox.Show("PENDING AMOUNT should not blank");
            }
            else if (SelectRecPay.TOTAL_REC_AMT == null || SelectRecPay.TOTAL_REC_AMT == 0)
            {
                MessageBox.Show("TOTAL RECEIVE AMOUNT should not blank");
            }
            else if (SelectRecPay.RETURNABLE_AMT == null || SelectRecPay.RETURNABLE_AMT == 0)
            {
                MessageBox.Show("RETURNABLE AMOUNT should not blank");
            }
            else if (!Regex.IsMatch(SelectRecPay.CUSTOMER_EMAIL,
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                MessageBox.Show("Please check Email format");
                return;
            }

            else
            {
                SelectRecPay.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                var response = await client.PostAsJsonAsync("api/RecPayAPI/UpdateRecPay/", SelectRecPay);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("RecPay Update Successfuly");
                    ModalService.NavigateTo(new ReceivePayment(), delegate (bool returnValue) { });

                }
            }
        }
        public ICommand _InsertRecpay;
        public ICommand InsertRecpay
        {
            get
            {
                if (_InsertRecpay == null)
                {
                    _InsertRecpay = new DelegateCommand(Insert_RevPay);
                }
                return _InsertRecpay;
            }
        }

        public async void Insert_RevPay()
        {
            if (SelectRecPay.BUSINESS_LOCATION == null || SelectRecPay.BUSINESS_LOCATION == "")
            {
                MessageBox.Show("BUSINESS LOCATION should not blank");
            }
            else if (SelectRecPay.CUSTOMER == null || SelectRecPay.CUSTOMER == "")
            {
                MessageBox.Show("CUSTOMER should not blank");
            }
            else if (SelectRecPay.CUSTOMER_EMAIL == null || SelectRecPay.CUSTOMER_EMAIL == "")
            {
                MessageBox.Show("Email should not blank");
            }
            else if (SelectRecPay.CUSTOMER_CONTACT_NO == null || SelectRecPay.CUSTOMER_CONTACT_NO == "")
            {
                MessageBox.Show("CONTACT NO should not blank");
            }
            else if (SelectRecPay.TOTAL_SELECTED_PAY_AMT == null || SelectRecPay.TOTAL_SELECTED_PAY_AMT == 0)
            {
                MessageBox.Show("PAY AMOUNT should not blank");
            }
            else if (SelectRecPay.PENDING_AMT == null || SelectRecPay.PENDING_AMT == 0)
            {
                MessageBox.Show("PENDING AMOUNT should not blank");
            }
            else if (SelectRecPay.TOTAL_REC_AMT == null || SelectRecPay.TOTAL_REC_AMT == 0)
            {
                MessageBox.Show("TOTAL RECEIVE AMOUNT should not blank");
            }
            //else if (SelectRecPay.RETURNABLE_AMT == null || SelectRecPay.RETURNABLE_AMT == 0)
            //{
            //    MessageBox.Show("RETURNABLE AMOUNT should not blank");
            //}
            else if (!Regex.IsMatch(SelectRecPay.CUSTOMER_EMAIL,
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                MessageBox.Show("Please check Email format");
                return;
            }
            else
            {
                SelectRecPay.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                var response = await client.PostAsJsonAsync("api/RecPayAPI/InsertRecPay/", SelectRecPay);
                if (response.StatusCode.ToString() == "OK")
                {
                    GetCash();
                    MessageBox.Show("RecPay Insert Successfuly");
                    Cancel_RecPay();
                    ModalService.NavigateTo(new ReceivePayment(), delegate (bool returnValue) { });

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
                    _Cancel = new DelegateCommand(Cancel_RecPay);
                }
                return _Cancel;
            }
        }



        public void Cancel_RecPay()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public ICommand _DeletePayReceiveClick;
        public ICommand DeletePayReceiveClick
        {
            get
            {
                if (_DeletePayReceiveClick == null)
                {
                    _DeletePayReceiveClick = new DelegateCommand(DeletePayReceive_Click);
                }
                return _DeletePayReceiveClick;
            }
        }
        public void DeletePayReceive_Click()
        {
            if (SelectRecPay.RECEIVE_PAYMENT_ID != null && SelectRecPay.RECEIVE_PAYMENT_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Receive Payment " + SelectRecPay.RECEIVE_PAY_NO + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var id = SelectRecPay.RECEIVE_PAYMENT_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/RecPayAPI/DeleteRecPay?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Item Delete Successfully");
                        ModalService.NavigateTo(new ReceivePayment(), delegate (bool returnValue) { });

                    }
                }
                else
                {
                    Cancel_RecPay();
                }
            }
            else
            {
                MessageBox.Show("Select Item");
            }
        }



        public ICommand _EditPayReceiveClick;
        public ICommand EditPayReceiveClick
        {
            get
            {
                if (_EditPayReceiveClick == null)
                {
                    _EditPayReceiveClick = new DelegateCommand(EditPayReceive_Click);
                }
                return _EditPayReceiveClick;
            }
        }

        public async void EditPayReceive_Click()
        {
            if (SelectRecPay.RECEIVE_PAYMENT_ID != null && SelectRecPay.RECEIVE_PAYMENT_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/RecPayAPI/GetEditRecPay?id=" + SelectRecPay.RECEIVE_PAYMENT_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<RecPayModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectRecPay.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectRecPay.BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID;
                            SelectRecPay.COMPANY_ID = data[i].COMPANY_ID;
                            SelectRecPay.CURRENT_PAY_AMT = data[i].CURRENT_PAY_AMT;
                            SelectRecPay.CUSTOMER = data[i].CUSTOMER;
                            SelectRecPay.CUSTOMER_CONTACT_NO = data[i].CUSTOMER_CONTACT_NO;
                            SelectRecPay.CUSTOMER_EMAIL = data[i].CUSTOMER_EMAIL;
                            SelectRecPay.CUSTOMER_ID = data[i].CUSTOMER_ID;
                            SelectRecPay.DATE = data[i].DATE;
                            SelectRecPay.IS_EMAIL_SEND = data[i].IS_EMAIL_SEND;
                            SelectRecPay.OTHER_PAY_AMT = data[i].OTHER_PAY_AMT;
                            SelectRecPay.PENDING_AMT = data[i].PENDING_AMT;
                            SelectRecPay.RECEIVE_PAY_NO = data[i].RECEIVE_PAY_NO;
                            SelectRecPay.RECEIVE_PAYMENT_ID = data[i].RECEIVE_PAYMENT_ID;
                            SelectRecPay.RETURNABLE_AMT = data[i].RETURNABLE_AMT;
                            SelectRecPay.TOTAL_REC_AMT = data[i].TOTAL_REC_AMT;
                            SelectRecPay.TOTAL_SELECTED_PAY_AMT = data[i].TOTAL_SELECTED_PAY_AMT;
                        }
                        App.Current.Properties["ItemEdit"] = SelectRecPay;
                        AddReceivePayment _Recpay = new AddReceivePayment();
                        _Recpay.Show();
                        //  ModalService.NavigateTo(new AddReceivePayment(), delegate(bool returnValue) { });
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Receive Payment first", "RecPay Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }

        }
        public ICommand _ViewPayReceiveClick;
        public ICommand ViewPayReceiveClick
        {
            get
            {
                if (_ViewPayReceiveClick == null)
                {
                    _ViewPayReceiveClick = new DelegateCommand(ViewPayReceive_Click);
                }
                return _ViewPayReceiveClick;
            }
        }

        public async void ViewPayReceive_Click()
        {
            if (SelectRecPay.RECEIVE_PAYMENT_ID != null && SelectRecPay.RECEIVE_PAYMENT_ID != 0)
            {
                App.Current.Properties["Action"] = "View";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/RecPayAPI/GetEditRecPay?id=" + SelectRecPay.RECEIVE_PAYMENT_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<RecPayModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectRecPay.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectRecPay.BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID;
                            SelectRecPay.COMPANY_ID = data[i].COMPANY_ID;
                            SelectRecPay.CURRENT_PAY_AMT = data[i].CURRENT_PAY_AMT;
                            SelectRecPay.CUSTOMER = data[i].CUSTOMER;
                            SelectRecPay.CUSTOMER_CONTACT_NO = data[i].CUSTOMER_CONTACT_NO;
                            SelectRecPay.CUSTOMER_EMAIL = data[i].CUSTOMER_EMAIL;
                            SelectRecPay.CUSTOMER_ID = data[i].CUSTOMER_ID;
                            SelectRecPay.DATE = data[i].DATE;
                            SelectRecPay.IS_EMAIL_SEND = data[i].IS_EMAIL_SEND;
                            SelectRecPay.OTHER_PAY_AMT = data[i].OTHER_PAY_AMT;
                            SelectRecPay.PENDING_AMT = data[i].PENDING_AMT;
                            SelectRecPay.RECEIVE_PAY_NO = data[i].RECEIVE_PAY_NO;
                            SelectRecPay.RECEIVE_PAYMENT_ID = data[i].RECEIVE_PAYMENT_ID;
                            SelectRecPay.RETURNABLE_AMT = data[i].RETURNABLE_AMT;
                            SelectRecPay.TOTAL_REC_AMT = data[i].TOTAL_REC_AMT;
                            SelectRecPay.TOTAL_SELECTED_PAY_AMT = data[i].TOTAL_SELECTED_PAY_AMT;
                        }
                        App.Current.Properties["ItemView"] = SelectRecPay;
                        ViewReceivePayment _ss = new ViewReceivePayment();
                        _ss.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Receive Payment first", "RecPay Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }

        }
        public ICommand _AddPayReceiveClick;
        public ICommand AddPayReceiveClick
        {
            get
            {
                if (_AddPayReceiveClick == null)
                {
                    _AddPayReceiveClick = new DelegateCommand(AddPayReceive_Click);
                }
                return _AddPayReceiveClick;
            }
        }

        public void AddPayReceive_Click()
        {
            AddReceivePayment _AddReceivePayment = new AddReceivePayment();
            _AddReceivePayment.Show();
            //ModalService.NavigateTo(new AddReceivePayment(), delegate(bool returnValue) { });
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
            App.Current.Properties["ReceivePaymentBussReff"] = 1;
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();

        }

        public ICommand _CustomerClick;
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
            App.Current.Properties["CustomerRecivePaymentReff"] = 1;
            Window_CustomerList IA = new Window_CustomerList();
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
        public bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                RaisePropertyChanged("IsSelected");
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
