using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Bank;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.Supplier;
using InvoicePOS.UserControll.SuppPayment;
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

namespace InvoicePOS.ViewModels
{
    public class SuppPayViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        public HttpResponseMessage response;
        // public HttpClient client = new HttpClient();
        public ICommand select { get; set; }
        SuppPaymentModel[] datainv1 = null;
        SuppPaymentModel[] datainv0 = null;
        SuppPaymentModel[] datainv123 = null;
        SuppPaymentModel datainv12 = null;
        ItemModel[] datainv = null;
        private readonly SuppPaymentModel OprModel;
        SuppPaymentModel _SuppPayModel = new SuppPaymentModel();
        private SuppPaymentModel _selectedItem;
        SuppPaymentModel[] data = null;
        ObservableCollection<SuppPaymentModel> _ListGrid_Temp = new ObservableCollection<SuppPaymentModel>();
        ObservableCollection<SuppPaymentModel> _ListGrid_Temp1 = new ObservableCollection<SuppPaymentModel>();
        ObservableCollection<SuppPaymentModel> _ListGrid_Inv = new ObservableCollection<SuppPaymentModel>();
        //public string Error
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //public SuppPaymentModel SelectedItem
        //{
        //    get { return _selectedItem; }
        //    set
        //    {
        //        if (_selectedItem != value)
        //        {
        //            _selectedItem = value;
        //            OnPropertyChanged("SelectedItem");
        //        }
        //    }
        //}



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




        public ICommand _SuppPayList_Click { get; set; }
        public ICommand SuppPayList_Click
        {
            get
            {
                if (_SuppPayList_Click == null)
                {
                    _SuppPayList_Click = new DelegateCommand(SuppPay_ListClick);
                }
                return _SuppPayList_Click;
            }
        }


        public void SuppPay_ListClick()
        {
            App.Current.Properties["Action"] = "Add";
            SuppPayAdd _supp = new SuppPayAdd();
            _supp.Show();
            // ModalService.NavigateTo(new SuppPayAdd(), delegate(bool returnValue) { });


        }
        //public ICommand _SuppPayView_Click { get; set; }
        //public ICommand SuppPayView_Click
        //{
        //    get
        //    {
        //        if (_SuppPayView_Click == null)
        //        {
        //            _SuppPayView_Click = new DelegateCommand(SuppPay_ViewClick);
        //        }
        //        return _SuppPayView_Click;
        //    }
        //}


        //public void SuppPay_ViewClick()
        //{
        //    if (SelectedItem.SUPP_PAYMENT != null && SelectedItem.SUPP_PAYMENT != 0)
        //    {
        //        view_SuppPay();
        //        SupplierPayView _SPV = new SupplierPayView();
        //        _SPV.Show();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Select Supplier Payment");
        //    }
        //}




        public ICommand _SuppPayAdd1 { get; set; }
        public ICommand SuppPayAdd1
        {
            get
            {
                if (_SuppPayAdd1 == null)
                {
                    _SuppPayAdd1 = new DelegateCommand(Supp_PayAdd);
                }
                return _SuppPayAdd1;
            }
        }


        public void Supp_PayAdd()
        {
            SuppPayAdd _SuppPayAdd = new SuppPayAdd();
            _SuppPayAdd.Show();
            // ModalService.NavigateTo(new SuppPayAdd(), delegate(bool returnValue) { });


        }


        public ICommand _EditSupplier { get; set; }
        public ICommand EditSupplier
        {
            get
            {
                if (_EditSupplier == null)
                {
                    _EditSupplier = new DelegateCommand(Edit_Supplier);
                }
                return _EditSupplier;
            }
        }


        public async void Edit_Supplier()
        {
            if (SelectedItem.SUPP_PAYMENT != null && SelectedItem.SUPP_PAYMENT != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                //ItemData = new List<SuppPaymentModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/SuppPaymentAPI/EditSuppPayment?id=" + SelectedItem.SUPP_PAYMENT + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<SuppPaymentModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedItem.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedItem.BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID;
                            SelectedItem.CASH_REG = data[i].CASH_REG;
                            SelectedItem.CASH_REG_AMT = data[i].CASH_REG_AMT;
                            SelectedItem.CASH_REG_ID = data[i].CASH_REG_ID;
                            SelectedItem.CHEQUE_AMT = data[i].CHEQUE_AMT;
                            SelectedItem.CHEQUE_BANK_AC = data[i].CHEQUE_BANK_AC;
                            SelectedItem.CHEQUE_BANK_BRANCH = data[i].CHEQUE_BANK_BRANCH;
                            SelectedItem.CHEQUE_DATE = data[i].CHEQUE_DATE;
                            SelectedItem.CHEQUE_NO = data[i].CHEQUE_NO;
                            SelectedItem.CURRENT_ADV_AMT = data[i].CURRENT_ADV_AMT;
                            SelectedItem.IS_SEND_SMS = data[i].IS_SEND_SMS;
                            SelectedItem.IS_SEND_EMAIL = data[i].IS_SEND_EMAIL;
                            SelectedItem.CURRENT_PAYABLE_AMT = data[i].CURRENT_PAYABLE_AMT;
                            SelectedItem.CURRENT_PAYMENT = data[i].CURRENT_PAYMENT;
                            SelectedItem.DISCOUNT_FLAT = data[i].DISCOUNT_FLAT;
                            SelectedItem.DISCOUNT_PERCENT = data[i].DISCOUNT_PERCENT;
                            SelectedItem.FINACIAL_AC = data[i].FINACIAL_AC;
                            SelectedItem.FINANCAL_AMT = data[i].FINANCAL_AMT;
                            SelectedItem.NOTE = data[i].NOTE;
                            SelectedItem.PAYMENT_DATE = data[i].PAYMENT_DATE;
                            SelectedItem.PAYMENT_NUMBER = data[i].PAYMENT_NUMBER;
                            SelectedItem.PENDING_AFTE_PAYMENT = data[i].PENDING_AFTE_PAYMENT;
                            SelectedItem.PENDING_AMT = data[i].PENDING_AMT;
                            SelectedItem.SELECTED_AMT = data[i].SELECTED_AMT;
                            SelectedItem.SUPP = data[i].SUPP;
                            SelectedItem.SUPP_EMAIL = data[i].SUPP_EMAIL;
                            SelectedItem.SUPP_ID = data[i].SUPP_ID;
                            SelectedItem.SUPP_PAYMENT = data[i].SUPP_PAYMENT;
                            SelectedItem.SUPP_SMS = data[i].SUPP_SMS;
                            SelectedItem.TOTAL_PANDING = data[i].TOTAL_PANDING;
                            SelectedItem.TOTAL_PAYMENT_MODES = data[i].TOTAL_PAYMENT_MODES;
                            SelectedItem.TOTAL_RIE_AMT = data[i].TOTAL_RIE_AMT;
                            SelectedItem.TRANSFER_AMT = data[i].TRANSFER_AMT;
                            SelectedItem.TRANSFER_BANK_AC = data[i].TRANSFER_BANK_AC;
                            SelectedItem.TRANSFER_BANK_BRANCH = data[i].TRANSFER_BANK_BRANCH;
                            SelectedItem.TRANSFER_DATE = data[i].TRANSFER_DATE;
                        }
                        App.Current.Properties["SuppPayEdit"] = SelectedItem;
                        SuppPayAdd _SuppPayAdd = new SuppPayAdd();
                        _SuppPayAdd.Show();

                        // ModalService.NavigateTo(new SuppPayAdd(), delegate(bool returnValue) { });
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Supplier Payment");
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

        private decimal? _TOTAL_AMT;
        public decimal? TOTAL_AMT
        {
            get
            {
                return _TOTAL_AMT;
            }
            set
            {
                _TOTAL_AMT = value;
                if (_TOTAL_AMT != value)
                {
                    _TOTAL_AMT = value;
                    OnPropertyChanged("TOTAL_AMT");
                }
            }
        }

        private string _SUPPLIER;
        public string SUPPLIER
        {
            get
            {
                return SelectedItem.SUPPLIER;
            }
            set
            {
                SelectedItem.SUPPLIER = value;
                if (SelectedItem.SUPPLIER != value)
                {
                    SelectedItem.SUPPLIER = value;
                    OnPropertyChanged("SUPPLIER");
                }
            }
        }


        private decimal? _ROUND_OFF_ADJUSTMENTAMT;
        public decimal? ROUND_OFF_ADJUSTMENTAMT
        {
            get
            {
                return SelectedItem.ROUND_OFF_ADJUSTMENTAMT;
            }
            set
            {
                SelectedItem.ROUND_OFF_ADJUSTMENTAMT = value;
                if (SelectedItem.ROUND_OFF_ADJUSTMENTAMT != value)
                {
                    SelectedItem.ROUND_OFF_ADJUSTMENTAMT = value;
                    OnPropertyChanged("ROUND_OFF_ADJUSTMENTAMT");
                }
            }
        }



        private decimal? _TOTAL_TAX_AMT;
        public decimal? TOTAL_TAX_AMT
        {
            get
            {
                return SelectedItem.TOTAL_TAX_AMT;
            }
            set
            {
                SelectedItem.TOTAL_TAX_AMT = value;
                if (SelectedItem.TOTAL_TAX_AMT != value)
                {
                    SelectedItem.TOTAL_TAX_AMT = value;
                    OnPropertyChanged("TOTAL_TAX_AMT");
                }
            }
        }


        private DateTime? _SUPPLIER_DATE;
        public DateTime? SUPPLIER_DATE
        {
            get
            {
                return SelectedItem.SUPPLIER_DATE;
            }
            set
            {
                SelectedItem.SUPPLIER_DATE = value;
                if (SelectedItem.SUPPLIER_DATE != value)
                {
                    SelectedItem.SUPPLIER_DATE = value;
                    OnPropertyChanged("SUPPLIER_DATE");
                }
            }
        }


        private DateTime? _RECEIVED_ITEM_ON_DATE;
        public DateTime? RECEIVED_ITEM_ON_DATE
        {
            get
            {
                return SelectedItem.RECEIVED_ITEM_ON_DATE;
            }
            set
            {
                SelectedItem.RECEIVED_ITEM_ON_DATE = value;
                if (SelectedItem.RECEIVED_ITEM_ON_DATE != value)
                {
                    SelectedItem.RECEIVED_ITEM_ON_DATE = value;
                    OnPropertyChanged("SUPPLIER_DATE");
                }
            }
        }

        private string _RECEIVED_ITEM_ENTRY_NO;
        public string RECEIVED_ITEM_ENTRY_NO
        {
            get
            {
                return SelectedItem.RECEIVED_ITEM_ENTRY_NO;
            }
            set
            {
                SelectedItem.RECEIVED_ITEM_ENTRY_NO = "123456";
                if (SelectedItem.RECEIVED_ITEM_ENTRY_NO != value)
                {
                    SelectedItem.RECEIVED_ITEM_ENTRY_NO = value;
                    OnPropertyChanged("RECEIVED_ITEM_ENTRY_NO");
                }
            }
        }




        private string _SUPPLIER_INVOICE_NO;
        public string SUPPLIER_INVOICE_NO
        {
            get
            {
                return SelectedItem.SUPPLIER_INVOICE_NO;
            }
            set
            {
                SelectedItem.SUPPLIER_INVOICE_NO = value;
                if (SelectedItem.SUPPLIER_INVOICE_NO != value)
                {
                    SelectedItem.SUPPLIER_INVOICE_NO = value;
                    OnPropertyChanged("RECEIVED_ITEM_ENTRY_NO");
                }
            }
        }

        private int? _PAYMENT_TERMS;
        public int? PAYMENT_TERMS
        {
            get
            {
                return SelectedItem.PAYMENT_TERMS;
            }
            set
            {
                SelectedItem.PAYMENT_TERMS = value;
                if (SelectedItem.PAYMENT_TERMS != value)
                {
                    SelectedItem.PAYMENT_TERMS = value;
                    OnPropertyChanged("PAYMENT_TERMS");
                }
            }
        }
        private bool _check;
        public bool check
        {
            get
            {
                return SelectedItem.ischecck;
            }
            set
            {
                SelectedItem.ischecck = value;
                if (SelectedItem.ischecck != value)
                {
                    SelectedItem.ischecck = value;
                    OnPropertyChanged("check");
                }
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
            SuppPayAdd.TotPendingRef.Text = _selectedItem.TOTAL_PANDING.ToString();

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
        public void getadjust()
        {

            {
                if (SuppPayAdd.TotPendingRef.Text != "0" && SuppPayAdd.TotPendingRef.Text != "")

                {
                    decimal gettotpaid = Convert.ToDecimal(SuppPayAdd.TotPendingRef.Text);
                    decimal adjustamout = 0;
                    decimal totalpending = 0;
                    int getindex = SuppPayAdd.ListGridRef1.SelectedIndex;
                    if (App.Current.Properties["getAllSupplierList"] != null)
                    {
                        ListGrid1 = App.Current.Properties["getAllSupplierList"] as ObservableCollection<SuppPaymentModel>;
                    }
                    for (int i = 0; i < ListGrid1.Count; i++)
                    {
                        if (i != getindex)
                        {
                            adjustamout = Convert.ToDecimal(ListGrid1[i].PENDING_AMT) + adjustamout;
                            ListGrid1[i].ROUND_OFF_ADJUSTMENTAMT = Convert.ToDecimal(ListGrid1[i].PENDING_AMT);
                            ListGrid1[i].PENDING_AMT = 0;
                        }

                    }
                    adjustamout = gettotpaid - adjustamout;
                    ListGrid1[getindex].ROUND_OFF_ADJUSTMENTAMT = adjustamout;
                    ListGrid1[getindex].PENDING_AMT = Convert.ToDecimal(ListGrid1[getindex].PENDING_AMT) - adjustamout;
                    SuppPayAdd.ListGridRef1.ItemsSource = null;
                    
                    SuppPayAdd.ListGridRef1.ItemsSource = ListGrid1;
                    //for (int n = 0; n < ListGrid1.Count; n++)
                    //{
                    //    if (ListGrid1[n].ischecck == true)
                    //    {
                    //        totalpending = totalpending + ListGrid1[n].TOTAL_PANDING;
                    //    }
                    //}
                    SelectedItem.getAllSupplier = ListGrid1;
                    //SELECTED_AMT.Text = totalpending.ToString();
                    //TOTALPAYMENT_MODES.Text = (totalpending - Convert.ToDecimal(CASH_REG_AMT.Text)).ToString();

                    App.Current.Properties["getAllSupplierList"] = ListGrid1;
                    App.Current.Properties["getAdjutamount"] = 1;
                    //suppay.getAllSupplier = ListGrid1;
                    //TextBlock textblock = (TextBlock)sender;
                    //decimal id = (decimal)textblock.Tag;
                    //decimal getafterselect = Convert.ToDecimal(TOTAL_PANDING.Text) - id;

                    //if (App.Current.Properties["getData"] != null)
                    //{
                    //    _ListGrid_Temp1 = App.Current.Properties["getData"] as SuppPaymentModel[];

                    //    foreach (SuppPaymentModel a in _ListGrid_Temp1)
                    //    {
                    //        if (a.ROUND_OFF_ADJUSTMENTAMT == id)
                    //        {
                    //            a.ROUND_OFF_ADJUSTMENTAMT = Convert.ToDecimal(CASH_REG_AMT.Text);
                    //        }
                    //    }

                    //}
                }
                else
                {
                    MessageBox.Show("Please enter payble amount");
                    return;
                }
            }
        }
        public SuppPayViewModel()
        {
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            if (App.Current.Properties["Action"] == "View")
            {
                SelectedItem = App.Current.Properties["SuppPayView"] as SuppPaymentModel;
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"] == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedItem = App.Current.Properties["SuppPayEdit"] as SuppPaymentModel;
                App.Current.Properties["Action"] = "";
            }

            else if (App.Current.Properties["Action"] == "Add")
            {
                CreatVisible = "Visible";
                UpdVisible = "Collapsed";
                SelectedItem = new SuppPaymentModel();
                App.Current.Properties["Action"] = "";
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                SelectedItem = new SuppPaymentModel();
                GetSupPay(comp);
            }
            App.Current.Properties["SuppPayView"] = "";
            GetSupplierList();
            //GetDuePay();
            GetSpplierNo();
            SelectedItem.SUPP_pay_no = PAYMENT_NUMBER; 
            isenable = true;
            _isenable = true;
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
                HttpResponseMessage response = client.GetAsync("api/SuppPaymentAPI/GetSuppPayno/").Result;
                if (response.IsSuccessStatusCode)
                {
                    //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                    uhy = await response.Content.ReadAsStringAsync();
                    string dd = Convert.ToString(uhy);
                    string aaa = dd.Substring(3, 5);
                    int xx = Convert.ToInt32(aaa);
                    int noo = Convert.ToInt32(xx + 1);
                    nnnn = "P" + noo.ToString("D6");
                    PAYMENT_NUMBER = nnnn;

                }
                else
                {
                    PAYMENT_NUMBER = "P000001";
                }
            }
            catch (Exception ex)
            {
                PAYMENT_NUMBER = "P000001";
            }

            return SuppPayAdd.Gencode.Text;
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
                _isviewmode = true;
                IsViewMode = true;

            }
            else if (ButtonText == "Define My Own")
            {
                //pomodel_code = "";
                PAYMENT_NUMBER = "";
                ButtonText = "Auto Generate";
                _isviewmode = false;
                IsViewMode = false;
            }
            SelectedItem.SUPP_pay_no = PAYMENT_NUMBER;


        }
        public async Task<ObservableCollection<SuppPaymentModel>> GetSupPay(int comp)
        {
            try
            {
                ItemData = new List<SuppPaymentModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/SuppPaymentAPI/GetSuppPayment?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<SuppPaymentModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new SuppPaymentModel
                        {
                            SLNO = x,
                            BUSINESS_LOCATION = data[i].BUSINESS_LOCATION,
                            BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID,
                            CASH_REG = data[i].CASH_REG,
                            CASH_REG_AMT = data[i].CASH_REG_AMT,
                            CASH_REG_ID = data[i].CASH_REG_ID,
                            CHEQUE_AMT = data[i].CHEQUE_AMT,
                            CHEQUE_BANK_AC = data[i].CHEQUE_BANK_AC,
                            CHEQUE_BANK_BRANCH = data[i].CHEQUE_BANK_BRANCH,
                            CHEQUE_DATE = data[i].CHEQUE_DATE,
                            CHEQUE_NO = data[i].CHEQUE_NO,
                            CURRENT_ADV_AMT = data[i].CURRENT_ADV_AMT,
                            IS_SEND_SMS = data[i].IS_SEND_SMS,
                            IS_SEND_EMAIL = data[i].IS_SEND_EMAIL,
                            CURRENT_PAYABLE_AMT = data[i].CURRENT_PAYABLE_AMT,
                            CURRENT_PAYMENT = data[i].CURRENT_PAYMENT,
                            DISCOUNT_FLAT = data[i].DISCOUNT_FLAT,
                            DISCOUNT_PERCENT = data[i].DISCOUNT_PERCENT,
                            FINACIAL_AC = data[i].FINACIAL_AC,
                            FINANCAL_AMT = data[i].FINANCAL_AMT,
                            NOTE = data[i].NOTE,
                            PAYMENT_DATE = data[i].PAYMENT_DATE,
                            PAYMENT_NUMBER = data[i].PAYMENT_NUMBER,
                            PENDING_AFTE_PAYMENT = data[i].PENDING_AFTE_PAYMENT,
                            PENDING_AMT = data[i].PENDING_AMT,
                            SELECTED_AMT = data[i].SELECTED_AMT,
                            SUPP = data[i].SUPP,
                            SUPP_EMAIL = data[i].SUPP_EMAIL,
                            SUPP_ID = data[i].SUPP_ID,
                            SUPP_PAYMENT = data[i].SUPP_PAYMENT,
                            SUPP_SMS = data[i].SUPP_SMS,
                            TOTAL_PANDING = data[i].TOTAL_PANDING,
                            TOTAL_PAYMENT_MODES = data[i].TOTAL_PAYMENT_MODES,
                            TOTAL_RIE_AMT = data[i].TOTAL_RIE_AMT,
                            TRANSFER_AMT = data[i].TRANSFER_AMT,
                            TRANSFER_BANK_AC = data[i].TRANSFER_BANK_AC,
                            TRANSFER_BANK_BRANCH = data[i].TRANSFER_BANK_BRANCH,
                            TRANSFER_DATE = data[i].TRANSFER_DATE,
                        });
                    }
                }
                ListGrid = _ListGrid_Temp;
                // var dataw = await _ListGrid_Temp.ToList();//.ToListAsync();
                // return new ObservableCollection<ItemModel>(dataw);
                return new ObservableCollection<SuppPaymentModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ObservableCollection<SuppPaymentModel>> GetSupplierList()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            decimal totalpending = 0;
            int suppid = Convert.ToInt32(App.Current.Properties["SupplierId"]);
            //  BusinessLocation = Convert.ToString(App.Current.Properties["BussLocName"]);
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);

            //response = client.GetAsync("api/SuppPaymentAPI/GetSuppPaymentDetailsafterpayment?id=" + comp + "&supid=" + suppid + "").Result;
            //if (response.IsSuccessStatusCode)
            //{

            //}
            //else
            {
                response = client.GetAsync("api/SuppPaymentAPI/GetSuppPaymentDetails?id=" + comp + "&supid=" + suppid + "").Result;
            }

            //HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceItem?id=" + SelectInv.INVOICE_ID + "").Result;
            if (response.IsSuccessStatusCode)
            {
                datainv1 = JsonConvert.DeserializeObject<SuppPaymentModel[]>(await response.Content.ReadAsStringAsync());
                App.Current.Properties["getData"] = datainv123;
                int x = 0;
                _ListGrid_Temp1.Clear();
                for (int i = 0; i < datainv1.Length; i++)
                {
                    //x++;
                    _ListGrid_Temp1.Add(new SuppPaymentModel
                    {

                        //SelectedItem.BUSINESS_LOCATION = datainv1[i].BUSINESS_LOCATION,
                        TOTAL_AMT = datainv1[i].TOTAL_AMT,
                        SUPPLIER = datainv1[i].SUPPLIER,
                        ROUND_OFF_ADJUSTMENTAMT = datainv1[i].ROUND_OFF_ADJUSTMENTAMT,
                        SUPPLIER_DATE = datainv1[i].SUPPLIER_DATE,
                        RECEIVED_ITEM_ENTRY_NO = datainv1[i].RECEIVED_ITEM_ENTRY_NO,
                        RECEIVED_ITEM_ON_DATE = datainv1[i].RECEIVED_ITEM_ON_DATE,
                        SUPPLIER_INVOICE_NO = datainv1[i].SUPPLIER_INVOICE_NO,
                        PAYMENT_TERMS = datainv1[i].PAYMENT_TERMS,
                        TOTAL_TAX_AMT = datainv1[i].TOTAL_TAX_AMT,
                        SUPP_EMAIL = datainv1[i].SUPPLIER_EMAIL,
                        SUPP_SMS = datainv1[i].SUPPLIER_MOBILE,
                        PENDING_AMT = Convert.ToDecimal(datainv1[i].TOTAL_AMT)

                    });
                    //if (datainv1[i].SUPP_PAYMENT == 0.00)
                    {
                        totalpending = totalpending + Convert.ToDecimal(datainv1[i].TOTAL_AMT);
                    }
                    //else
                    //{
                    //    totalpending = totalpending + Convert.ToDecimal(datainv1[i].SUPP_PAYMENT);
                    //}

                }
                try
                {
                    response = client.GetAsync("api/SuppPaymentAPI/GetSuppPaymentDetailsafterpayment?supid=" + suppid + "").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        datainv0 = null;
                        try
                        {
                            datainv0 = JsonConvert.DeserializeObject<SuppPaymentModel[]>(await response.Content.ReadAsStringAsync());
                        }
                        catch (Exception ex)
                        { }
                        for (int i = 0; i < _ListGrid_Temp1.Count; i++)
                        {
                            if (datainv0[i].TOTAL_AMT == _ListGrid_Temp1[i].TOTAL_AMT)
                            {
                                //if (datainv0[i].PENDING_AMT == 0)
                                //{
                                //    _ListGrid_Temp1.RemoveAt(i);
                                //}
                                //else
                                {
                                    _ListGrid_Temp1[i].PENDING_AMT = datainv0[i].PENDING_AMT;
                                }
                            }
                        }
                    }
                }
                catch
                { }
                //SuppPayAdd.TotCashRef.Text = totalpending.ToString();
                ListGrid1 = _ListGrid_Temp1;
                SelectedItem.getAllSupplier = _ListGrid_Temp1;
                App.Current.Properties["getAllSupplierList"] = _ListGrid_Temp1;
                SuppPayAdd.TotCashRef.Text = totalpending.ToString();
                App.Current.Properties["getTotalPending"] = totalpending;
                //SuppPayAdd.ListGridRef1.ItemsSource = _ListGrid_Temp;
                if (App.Current.Properties["getTotalPending"] != null)
                {
                    if (App.Current.Properties["getTotalPending"].ToString() != "0.00")
                    {
                        ListGrid1 = _ListGrid_Temp1;
                        //SuppPayAdd.ListGridRef1.ItemsSource = _ListGrid_Temp1;
                    }
                    else
                    {
                        ListGrid1 = null;
                        SuppPayAdd.ListGridRef1.ItemsSource = null;
                    }
                }

                //TotalBottom();

                GetDuePay();
            }
            return new ObservableCollection<SuppPaymentModel>(_ListGrid_Temp1);

        }
        public async void GetDuePay()
        {
            try
            {
                int suppid = Convert.ToInt32(App.Current.Properties["SupplierId"]);
                //  BusinessLocation = Convert.ToString(App.Current.Properties["BussLocName"]);
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/SuppPaymentAPI/GetDueDetails?id=" + comp + "&companyid=" + suppid + "").Result;
                //HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceItem?id=" + SelectInv.INVOICE_ID + "").Result;
                {


                    datainv12 = JsonConvert.DeserializeObject<SuppPaymentModel>(await response.Content.ReadAsStringAsync());
                    if (datainv12.PENDING_AFTE_PAYMENT.ToString() == "0")
                    {
                        TotalBottom();
                    }
                    else
                    {
                        SuppPayAdd.TotCashRef.Text = datainv12.PENDING_AFTE_PAYMENT.ToString();
                        App.Current.Properties["getTotalPending"] = datainv12.PENDING_AFTE_PAYMENT.ToString();
                    }


                }
            }
            catch
            {
                TotalBottom();
            }

        }

        public void TotalBottom()
        {
            //SUB_TOTAL_BEFORETAX = 0;

            TOTAL_PANDING = 0;
            //TOTAL_TAX = 0;
            if (ListGrid1 != null)
            {
                for (int i = 0; i < ListGrid1.Count; i++)
                {
                    //SUB_TOTAL_BEFORETAX = Convert.ToDecimal(ListGrid1[i].SUB_TOTAL_BEFORE_TAX + SUB_TOTAL_BEFORETAX);
                    TOTAL_PANDING = Convert.ToDecimal(ListGrid1[i].TOTAL_AMT + TOTAL_PANDING);
                    //App.Current.Properties["CurrentGrosAmount1"] = TOTAL_AMOUNT;
                    //TOTAL_TAX = Convert.ToDecimal((ListGrid1[i].SUB_TOTAL_AFTER_TAX - ListGrid1[i].SUB_TOTAL_BEFORE_TAX) + TOTAL_TAX);
                    // SUB_TOTAL = TOTAL_AMOUNT;
                }
                //CASH_REG_AMT = TOTAL_PANDING;
                SuppPayAdd.TotCashRef.Text = TOTAL_PANDING.ToString();
                App.Current.Properties["getTotalPending"] = TOTAL_PANDING.ToString();
                //SuppPayAdd.TotPendingRef.Text = TOTAL_PANDING.ToString();
            }
        }


        public ObservableCollection<SuppPaymentModel> _ListGrid { get; set; }
        public ObservableCollection<SuppPaymentModel> ListGrid
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

        public ObservableCollection<SuppPaymentModel> _ListGrid1 { get; set; }
        public ObservableCollection<SuppPaymentModel> ListGrid1
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

        private List<SuppPaymentModel> _ItemData;
        public List<SuppPaymentModel> ItemData
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
                    OnPropertyChanged("BUSINESS_LOCATION");
                }
            }
        }
        private long _SUPP_ID;
        public long SUPP_ID
        {
            get
            {
                return SelectedItem.SUPP_ID;
            }
            set
            {
                SelectedItem.SUPP_ID = value;

                if (SelectedItem.SUPP_ID != value)
                {
                    SelectedItem.SUPP_ID = value;
                    OnPropertyChanged("SUPP_ID");
                }
            }
        }
        private string _SUPP;
        public string SUPP
        {
            get
            {
                return SelectedItem.SUPP;

            }
            set
            {
                //SelectedItem.SUPP = value;

                //if (SelectedItem.SUPP != value)
                //{
                SelectedItem.SUPP = value;
                OnPropertyChanged("SUPP");
                //}
                GetSupplierList();
            }
        }
        private string _SUPP_EMAIL;
        public string SUPP_EMAIL
        {
            get
            {
                return SelectedItem.SUPP_EMAIL;
            }
            set
            {
                SelectedItem.SUPP_EMAIL = value;

                if (SelectedItem.SUPP_EMAIL != value)
                {
                    SelectedItem.SUPP_EMAIL = value;
                    OnPropertyChanged("SUPP_EMAIL");
                }
            }
        }
        private string _SUPP_SMS;
        public string SUPP_SMS
        {
            get
            {
                return SelectedItem.SUPP_SMS;
            }
            set
            {
                SelectedItem.SUPP_SMS = value;

                if (SelectedItem.SUPP_SMS != value)
                {
                    SelectedItem.SUPP_SMS = value;
                    OnPropertyChanged("SUPP_SMS");
                }
            }
        }
        private bool _IS_SEND_EMAIL;
        public bool IS_SEND_EMAIL
        {
            get
            {
                return SelectedItem.IS_SEND_EMAIL;
            }
            set
            {
                SelectedItem.IS_SEND_EMAIL = value;

                if (SelectedItem.IS_SEND_EMAIL != value)
                {
                    SelectedItem.IS_SEND_EMAIL = value;
                    OnPropertyChanged("IS_SEND_EMAIL");
                }
            }
        }
        private bool _ischecck;
        public bool ischecck
        {
            get
            {
                return SelectedItem.ischecck;
            }
            set
            {
                SelectedItem.ischecck = value;

                if (SelectedItem.ischecck != value)
                {
                    SelectedItem.ischecck = value;
                    OnPropertyChanged("ischecck");
                }
            }
        }
        private bool _IS_PRINT_CHECK;
        public bool IS_PRINT_CHECK
        {
            get
            {
                return SelectedItem.IS_PRINT_CHECK;
            }
            set
            {
                SelectedItem.IS_PRINT_CHECK = value;

                if (SelectedItem.IS_PRINT_CHECK != value)
                {
                    SelectedItem.IS_PRINT_CHECK = value;
                    OnPropertyChanged("IS_PRINT_CHECK");
                }
            }
        }
        private bool _IS_SEND_SMS;
        public bool IS_SEND_SMS
        {
            get
            {
                return SelectedItem.IS_SEND_SMS;
            }
            set
            {
                SelectedItem.IS_SEND_SMS = value;

                if (SelectedItem.IS_SEND_SMS != value)
                {
                    SelectedItem.IS_SEND_SMS = value;
                    OnPropertyChanged("IS_SEND_SMS");
                }
            }
        }
        private decimal _CURRENT_ADV_AMT;
        public decimal CURRENT_ADV_AMT
        {
            get
            {
                return SelectedItem.CURRENT_ADV_AMT;
            }
            set
            {
                SelectedItem.CURRENT_ADV_AMT = value;

                if (SelectedItem.CURRENT_ADV_AMT != value)
                {
                    SelectedItem.CURRENT_ADV_AMT = value;
                    OnPropertyChanged("CURRENT_ADV_AMT");
                }
            }
        }
        private decimal _TOTAL_RIE_AMT;
        public decimal TOTAL_RIE_AMT
        {
            get
            {
                return SelectedItem.TOTAL_RIE_AMT;
            }
            set
            {
                SelectedItem.TOTAL_RIE_AMT = value;

                if (SelectedItem.TOTAL_RIE_AMT != value)
                {
                    SelectedItem.TOTAL_RIE_AMT = value;
                    OnPropertyChanged("TOTAL_RIE_AMT");
                }
            }
        }
        private decimal _PENDING_AMT;
        public decimal PENDING_AMT
        {
            get
            {
                return SelectedItem.PENDING_AMT;
            }
            set
            {
                SelectedItem.PENDING_AMT = value;

                if (SelectedItem.PENDING_AMT != value)
                {
                    SelectedItem.PENDING_AMT = value;
                    OnPropertyChanged("PENDING_AMT");
                }
            }
        }
        private string _PAYMENT_NUMBER;
        public string PAYMENT_NUMBER
        {
            get
            {
                return SelectedItem.PAYMENT_NUMBER;
            }
            set
            {
                //SelectedItem.PAYMENT_NUMBER = value;

                //if (SelectedItem.PAYMENT_NUMBER != value)
                //{
                SelectedItem.PAYMENT_NUMBER = value;
                OnPropertyChanged("PAYMENT_NUMBER");
                ////}
            }
        }
        private DateTime _PAYMENT_DATE;
        public DateTime PAYMENT_DATE
        {
            get
            {
                return SelectedItem.PAYMENT_DATE;
            }
            set
            {
                SelectedItem.PAYMENT_DATE = value;

                if (SelectedItem.PAYMENT_DATE != value)
                {
                    SelectedItem.PAYMENT_DATE = value;
                    OnPropertyChanged("PAYMENT_DATE");
                }
            }
        }
        private decimal _CURRENT_PAYABLE_AMT;
        public decimal CURRENT_PAYABLE_AMT
        {
            get
            {
                return SelectedItem.CURRENT_PAYABLE_AMT;
            }
            set
            {
                SelectedItem.CURRENT_PAYABLE_AMT = value;

                if (SelectedItem.CURRENT_PAYABLE_AMT != value)
                {
                    SelectedItem.CURRENT_PAYABLE_AMT = value;
                    OnPropertyChanged("CURRENT_PAYABLE_AMT");
                }
            }
        }
        private decimal _TOTAL_PANDING;
        public decimal TOTAL_PANDING
        {
            get
            {
                return SelectedItem.TOTAL_PANDING;
            }
            set
            {
                SelectedItem.TOTAL_PANDING = value;

                if (SelectedItem.TOTAL_PANDING != value)
                {
                    SelectedItem.TOTAL_PANDING = value;
                    OnPropertyChanged("TOTAL_PANDING");
                }
            }
        }
        private decimal _SELECTED_AMT;
        public decimal SELECTED_AMT
        {
            get
            {
                return SelectedItem.SELECTED_AMT;
            }
            set
            {
                SelectedItem.SELECTED_AMT = value;

                if (SelectedItem.SELECTED_AMT != value)
                {
                    SelectedItem.SELECTED_AMT = value;
                    OnPropertyChanged("SELECTED_AMT");
                }
            }
        }
        private decimal _PENDING_AFTE_PAYMENT;
        public decimal PENDING_AFTE_PAYMENT
        {
            get
            {
                return SelectedItem.PENDING_AFTE_PAYMENT;
            }
            set
            {
                SelectedItem.PENDING_AFTE_PAYMENT = value;

                if (SelectedItem.PENDING_AFTE_PAYMENT != value)
                {
                    SelectedItem.PENDING_AFTE_PAYMENT = value;
                    OnPropertyChanged("PENDING_AFTE_PAYMENT");
                }
            }
        }
        private long _CASH_REG_ID;
        public long CASH_REG_ID
        {
            get
            {
                return SelectedItem.CASH_REG_ID;
            }
            set
            {
                SelectedItem.CASH_REG_ID = value;

                if (SelectedItem.CASH_REG_ID != value)
                {
                    SelectedItem.CASH_REG_ID = value;
                    OnPropertyChanged("CASH_REG_ID");
                }
            }
        }
        private string _CASH_REG;
        public string CASH_REG
        {
            get
            {
                return SelectedItem.CASH_REG;
            }
            set
            {
                SelectedItem.CASH_REG = value;

                if (SelectedItem.CASH_REG != value)
                {
                    SelectedItem.CASH_REG = value;
                    OnPropertyChanged("CASH_REG");
                }
            }
        }
        private decimal _CASH_REG_AMT;
        public decimal CASH_REG_AMT
        {
            get
            {
                return _CASH_REG_AMT;
            }
            set
            {
                _CASH_REG_AMT = value;

                if (_CASH_REG_AMT != value)
                {
                    _CASH_REG_AMT = value;
                    OnPropertyChanged("CASH_REG_AMT");
                }
            }
        }
        private decimal _CHEQUE_AMT;
        public decimal CHEQUE_AMT
        {
            get
            {
                return SelectedItem.CHEQUE_AMT;
            }
            set
            {
                SelectedItem.CHEQUE_AMT = value;

                if (SelectedItem.CHEQUE_AMT != value)
                {
                    SelectedItem.CHEQUE_AMT = value;
                    OnPropertyChanged("CHEQUE_AMT");
                }
            }
        }
        private string _CHEQUE_NO;
        public string CHEQUE_NO
        {
            get
            {
                return SelectedItem.CHEQUE_NO;
            }
            set
            {
                SelectedItem.CHEQUE_NO = value;

                if (SelectedItem.CHEQUE_NO != value)
                {
                    SelectedItem.CHEQUE_NO = value;
                    OnPropertyChanged("CHEQUE_NO");
                }
            }
        }
        private string _CHEQUE_BANK_BRANCH;
        public string CHEQUE_BANK_BRANCH
        {
            get
            {
                return SelectedItem.CHEQUE_BANK_BRANCH;
            }
            set
            {
                SelectedItem.CHEQUE_BANK_BRANCH = value;

                if (SelectedItem.CHEQUE_BANK_BRANCH != value)
                {
                    SelectedItem.CHEQUE_BANK_BRANCH = value;
                    OnPropertyChanged("CHEQUE_BANK_BRANCH");
                }
            }
        }
        private long _CHEQUE_BANK_AC;
        public long CHEQUE_BANK_AC
        {
            get
            {
                return SelectedItem.CHEQUE_BANK_AC;
            }
            set
            {
                SelectedItem.CHEQUE_BANK_AC = value;

                if (SelectedItem.CHEQUE_BANK_AC != value)
                {
                    SelectedItem.CHEQUE_BANK_AC = value;
                    OnPropertyChanged("CHEQUE_BANK_AC");
                }
            }
        }
        private DateTime _CHEQUE_DATE;
        public DateTime CHEQUE_DATE
        {
            get
            {
                return SelectedItem.CHEQUE_DATE;
            }
            set
            {
                SelectedItem.CHEQUE_DATE = value;

                if (SelectedItem.CHEQUE_DATE != value)
                {
                    SelectedItem.CHEQUE_DATE = value;
                    OnPropertyChanged("CHEQUE_DATE");
                }
            }
        }
        private decimal _TRANSFER_AMT;
        public decimal TRANSFER_AMT
        {
            get
            {
                return SelectedItem.TRANSFER_AMT;
            }
            set
            {
                SelectedItem.TRANSFER_AMT = value;

                if (SelectedItem.TRANSFER_AMT != value)
                {
                    SelectedItem.TRANSFER_AMT = value;
                    OnPropertyChanged("TRANSFER_AMT");
                }
            }
        }
        private string _TRANSFER_BANK_BRANCH;
        public string TRANSFER_BANK_BRANCH
        {
            get
            {
                return SelectedItem.TRANSFER_BANK_BRANCH;
            }
            set
            {
                SelectedItem.TRANSFER_BANK_BRANCH = value;

                if (SelectedItem.TRANSFER_BANK_BRANCH != value)
                {
                    SelectedItem.TRANSFER_BANK_BRANCH = value;
                    OnPropertyChanged("TRANSFER_BANK_BRANCH");
                }
            }
        }
        private long _TRANSFER_BANK_AC;
        public long TRANSFER_BANK_AC
        {
            get
            {
                return SelectedItem.TRANSFER_BANK_AC;
            }
            set
            {
                SelectedItem.TRANSFER_BANK_AC = value;

                if (SelectedItem.TRANSFER_BANK_AC != value)
                {
                    SelectedItem.TRANSFER_BANK_AC = value;
                    OnPropertyChanged("TRANSFER_BANK_AC");
                }
            }
        }
        private DateTime _TRANSFER_DATE;
        public DateTime TRANSFER_DATE
        {
            get
            {
                return SelectedItem.TRANSFER_DATE;
            }
            set
            {
                SelectedItem.TRANSFER_DATE = value;

                if (SelectedItem.TRANSFER_DATE != value)
                {
                    SelectedItem.TRANSFER_DATE = value;
                    OnPropertyChanged("TRANSFER_DATE");
                }
            }
        }
        private decimal _FINANCAL_AMT;
        public decimal FINANCAL_AMT
        {
            get
            {
                return SelectedItem.FINANCAL_AMT;
            }
            set
            {
                SelectedItem.FINANCAL_AMT = value;

                if (SelectedItem.FINANCAL_AMT != value)
                {
                    SelectedItem.FINANCAL_AMT = value;
                    OnPropertyChanged("FINANCAL_AMT");
                }
            }
        }
        private long _FINACIAL_AC;
        public long FINACIAL_AC
        {
            get
            {
                return SelectedItem.FINACIAL_AC;
            }
            set
            {
                SelectedItem.FINACIAL_AC = value;

                if (SelectedItem.FINACIAL_AC != value)
                {
                    SelectedItem.FINACIAL_AC = value;
                    OnPropertyChanged("FINACIAL_AC");
                }
            }
        }
        private int _DISCOUNT_FLAT;
        public int DISCOUNT_FLAT
        {
            get
            {
                return SelectedItem.DISCOUNT_FLAT;
            }
            set
            {
                SelectedItem.DISCOUNT_FLAT = value;

                if (SelectedItem.DISCOUNT_FLAT != value)
                {
                    SelectedItem.DISCOUNT_FLAT = value;
                    OnPropertyChanged("DISCOUNT_FLAT");
                }
            }
        }
        private int _DISCOUNT_PERCENT;
        public int DISCOUNT_PERCENT
        {
            get
            {
                return SelectedItem.DISCOUNT_PERCENT;
            }
            set
            {
                SelectedItem.DISCOUNT_PERCENT = value;

                if (SelectedItem.DISCOUNT_PERCENT != value)
                {
                    SelectedItem.DISCOUNT_PERCENT = value;
                    OnPropertyChanged("DISCOUNT_PERCENT");
                }
            }
        }
        private int _TOTAL_PAYMENT_MODES;
        public int TOTAL_PAYMENT_MODES
        {
            get
            {
                return SelectedItem.TOTAL_PAYMENT_MODES;
            }
            set
            {
                SelectedItem.TOTAL_PAYMENT_MODES = value;

                if (SelectedItem.TOTAL_PAYMENT_MODES != value)
                {
                    SelectedItem.TOTAL_PAYMENT_MODES = value;
                    OnPropertyChanged("TOTAL_PAYMENT_MODES");
                }
            }
        }
        private decimal _CURRENT_PAYMENT;
        public decimal CURRENT_PAYMENT
        {
            get
            {
                return SelectedItem.CURRENT_PAYMENT;
            }
            set
            {
                SelectedItem.CURRENT_PAYMENT = value;

                if (SelectedItem.CURRENT_PAYMENT != value)
                {
                    SelectedItem.CURRENT_PAYMENT = value;
                    OnPropertyChanged("CURRENT_PAYMENT");
                }
            }
        }
        private string _NOTE;
        public string NOTE
        {
            get
            {
                return SelectedItem.NOTE;
            }
            set
            {
                SelectedItem.NOTE = value;

                if (SelectedItem.NOTE != value)
                {
                    SelectedItem.NOTE = value;
                    OnPropertyChanged("NOTE ");
                }
            }
        }
        public ICommand _UpdateSuppPayment;
        public ICommand UpdateSuppPayment
        {
            get
            {
                if (_UpdateSuppPayment == null)
                {
                    _UpdateSuppPayment = new DelegateCommand(Update_SuppPayment);
                }
                return _UpdateSuppPayment;
            }
        }
        public async void Update_SuppPayment()
        {
            if (SelectedItem.SUPP == "" || SelectedItem.SUPP == null)
            {
                MessageBox.Show("Supplier can not be blank");
            }

            else if (SelectedItem.SUPP_SMS == "" || SelectedItem.SUPP_SMS == null)
            {
                MessageBox.Show("Supplier Sms can not be blank");
            }
            else if (SelectedItem.SELECTED_AMT == 0 || SelectedItem.SELECTED_AMT == null)
            {
                MessageBox.Show("Selected Amount can not be blank");
            }
            else if (SelectedItem.TOTAL_PANDING == 0 || SelectedItem.TOTAL_PANDING == null)
            {
                MessageBox.Show("Total Pending Amount can not be blank");
            }
            else if (SelectedItem.PENDING_AFTE_PAYMENT == 0 || SelectedItem.PENDING_AFTE_PAYMENT == null)
            {

            }
            else if (SelectedItem.TOTAL_PAYMENT_MODES == 0 || SelectedItem.SUPP == null)
            {

            }
            else if (SelectedItem.SUPP_EMAIL == "" || SelectedItem.SUPP_EMAIL == null)
            {
                MessageBox.Show("EMAIL can not be blank");
            }
            else if (!Regex.IsMatch(SelectedItem.SUPP_EMAIL,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                MessageBox.Show("Please Check Email Format");
                //AddCustomer.ShippingemailAddressReff.Focus();
                return;
            }
            // App.Current.Properties["Action"] = "Add";
            SelectedItem.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            //_opr.ITEM_NAME = ITEM_NAME;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/SuppPaymentAPI/UpdateSuppPayment/", SelectedItem);
            if (response.StatusCode.ToString() == "OK")
            {

                MessageBox.Show("Supplier Payment Update Successfully");
                ModalService.NavigateTo(new SuppPayList(), delegate (bool returnValue) { });
            }

        }
        public ICommand _InsertSuppPayment;
        public ICommand InsertSuppPayment
        {
            get
            {
                if (_InsertSuppPayment == null)
                {
                    _InsertSuppPayment = new DelegateCommand(Insert_SuppPayment);
                }
                return _InsertSuppPayment;
            }
        }
        public async void Insert_SuppPayment()
        {
            if (App.Current.Properties["getAdjutamount"] != null)
            {
                //if (App.Current.Properties["getAllSupplierList"] != null)
                //{
                //    SelectedItem.getAllSupplier = null;
                //SelectedItem.getAllSupplier = App.Current.Properties["getAllSupplierList"] as ObservableCollection<SuppPaymentModel>;
                //}
                //if (SelectedItem.SUPP == "" || SelectedItem.SUPP == null)
                //{
                //    MessageBox.Show("Supplier can not be blank");
                //}

                //else if (SelectedItem.SUPP_SMS == "" || SelectedItem.SUPP_SMS == null)
                //{
                //    MessageBox.Show("Supplier Sms can not be blank");
                //}
                //else if (SelectedItem.SELECTED_AMT == 0 || SelectedItem.SELECTED_AMT == null)
                //{
                //    MessageBox.Show("Selected Amount can not be blank");
                //}
                //else if (SelectedItem.TOTAL_PANDING == 0 || SelectedItem.TOTAL_PANDING == null)
                //{
                //    MessageBox.Show("Total Pending Amount can not be blank");
                //}
                //else if (SelectedItem.PENDING_AFTE_PAYMENT == 0|| SelectedItem.PENDING_AFTE_PAYMENT == null)
                //{
                //    MessageBox.Show("Total Pending Amount can not be blank");
                //}
                //else if (SelectedItem.TOTAL_PAYMENT_MODES == 0 || SelectedItem.SUPP == null)
                //{
                //    MessageBox.Show("Total Pending Amount can not be blank");
                //}
                //else if (SelectedItem.SUPP_EMAIL == "" || SelectedItem.SUPP_EMAIL == null)
                //{
                //    MessageBox.Show("EMAIL can not be blank");
                //}
                //else if (!Regex.IsMatch(SelectedItem.SUPP_EMAIL,
                //    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                //    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                //    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                //{
                //    MessageBox.Show("Please Check Email Format");
                //    //AddCustomer.ShippingemailAddressReff.Focus();
                //    return;
                //}
                //else
                {
                    // App.Current.Properties["Action"] = "Add";
                    SelectedItem.SUPP_ID = Convert.ToInt32(App.Current.Properties["SupplierId"].ToString());
                    SelectedItem.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                    //_opr.ITEM_NAME = ITEM_NAME;
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsJsonAsync("api/SuppPaymentAPI/CreateSuppPayment/", SelectedItem);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Supplier Payment Insert Successfully");
                        //ModalService.NavigateTo(new SuppPayList(), delegate (bool returnValue) { });
                        SuppPayViewModel supv = new SuppPayViewModel();
                        SuppPayAdd suppay = new SuppPayAdd();
                        suppay.Close();

                    }
                }
            }
            else if (SuppPayAdd.TotPendingRef.Text == SuppPayAdd.TotCashRef.Text)
            {
                {
                    // App.Current.Properties["Action"] = "Add";
                    SelectedItem.SUPP_ID = Convert.ToInt32(App.Current.Properties["SupplierId"].ToString());
                    SelectedItem.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                    //_opr.ITEM_NAME = ITEM_NAME;
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsJsonAsync("api/SuppPaymentAPI/CreateSuppPayment/", SelectedItem);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Supplier Payment Insert Successfully");
                        ModalService.NavigateTo(new SuppPayList(), delegate (bool returnValue) { });
                        //SuppPayViewModel supv = new SuppPayViewModel();
                        SuppPayAdd suppay = new SuppPayAdd();
                        suppay.Close();

                    }
                }
            }
            else
            {
                MessageBox.Show("Please select adjust amount");
                return;
            }

        }
        public ICommand _DeleteSuppPayment;
        public ICommand DeleteSuppPayment
        {
            get
            {
                if (_DeleteSuppPayment == null)
                {
                    _DeleteSuppPayment = new DelegateCommand(Delete_SuppPayment);
                }
                return _DeleteSuppPayment;
            }
        }
        public async void Delete_SuppPayment()
        {
            if (SelectedItem.SUPP_PAYMENT != null && SelectedItem.SUPP_PAYMENT != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Supplier Payment " + SelectedItem.PAYMENT_NUMBER + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    long id = SelectedItem.SUPP_PAYMENT;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    var response = client.GetAsync("api/SuppPaymentAPI/DeleteSuppPayment?id=" + SelectedItem.SUPP_PAYMENT + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Supplier Payment Delete Successfully");
                        ModalService.NavigateTo(new SuppPayList(), delegate (bool returnValue) { });
                    }
                }
                else
                {
                    Cancel_SupPay();
                }
            }
            else
            {
                MessageBox.Show("Select Supplier Payment");
            }
        }
        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_SupPay);
                }
                return _Cancel;
            }
        }



        public void Cancel_SupPay()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public ICommand _ViewtSuppPay { get; set; }
        public ICommand SuppPayView_Click
        {
            get
            {
                if (_ViewtSuppPay == null)
                {
                    _ViewtSuppPay = new DelegateCommand(view_SuppPay);
                }
                return _ViewtSuppPay;
            }
        }
        public async void view_SuppPay()
        {
            if (SelectedItem.SUPP_PAYMENT != null && SelectedItem.SUPP_PAYMENT != 0)
            {
                App.Current.Properties["Action"] = "View";
                ItemData = new List<SuppPaymentModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/SuppPaymentAPI/GetViewItem?id=" + SelectedItem.SUPP_PAYMENT + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<SuppPaymentModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedItem.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedItem.BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID;
                            SelectedItem.CASH_REG = data[i].CASH_REG;
                            SelectedItem.CASH_REG_AMT = data[i].CASH_REG_AMT;
                            SelectedItem.CASH_REG_ID = data[i].CASH_REG_ID;
                            SelectedItem.CHEQUE_AMT = data[i].CHEQUE_AMT;
                            SelectedItem.CHEQUE_BANK_AC = data[i].CHEQUE_BANK_AC;
                            SelectedItem.CHEQUE_BANK_BRANCH = data[i].CHEQUE_BANK_BRANCH;
                            SelectedItem.CHEQUE_DATE = data[i].CHEQUE_DATE;
                            SelectedItem.CHEQUE_NO = data[i].CHEQUE_NO;
                            SelectedItem.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedItem.CURRENT_ADV_AMT = data[i].CURRENT_ADV_AMT;
                            SelectedItem.CURRENT_PAYABLE_AMT = data[i].CURRENT_PAYABLE_AMT;
                            SelectedItem.CURRENT_PAYMENT = data[i].CURRENT_PAYMENT;
                            SelectedItem.DISCOUNT_FLAT = data[i].DISCOUNT_FLAT;
                            SelectedItem.DISCOUNT_PERCENT = data[i].DISCOUNT_PERCENT;
                            SelectedItem.FINACIAL_AC = data[i].FINACIAL_AC;
                            SelectedItem.FINANCAL_AMT = data[i].FINANCAL_AMT;
                            SelectedItem.IS_SEND_EMAIL = data[i].IS_SEND_EMAIL;
                            SelectedItem.IS_SEND_SMS = data[i].IS_SEND_SMS;
                            SelectedItem.NOTE = data[i].NOTE;
                            SelectedItem.PAYMENT_DATE = data[i].PAYMENT_DATE;
                            SelectedItem.PAYMENT_NUMBER = data[i].PAYMENT_NUMBER;
                            SelectedItem.PENDING_AFTE_PAYMENT = data[i].PENDING_AFTE_PAYMENT;
                            SelectedItem.PENDING_AMT = data[i].PENDING_AMT;
                            SelectedItem.SELECTED_AMT = data[i].SELECTED_AMT;
                            SelectedItem.SUPP = data[i].SUPP;
                            SelectedItem.SUPP_EMAIL = data[i].SUPP_EMAIL;
                            SelectedItem.SUPP_ID = data[i].SUPP_ID;
                            SelectedItem.SUPP_PAYMENT = data[i].SUPP_PAYMENT;
                            SelectedItem.SUPP_SMS = data[i].SUPP_SMS;
                            SelectedItem.TOTAL_PANDING = data[i].TOTAL_PANDING;
                            SelectedItem.TOTAL_PAYMENT_MODES = data[i].TOTAL_PAYMENT_MODES;
                            SelectedItem.TOTAL_RIE_AMT = data[i].TOTAL_RIE_AMT;
                            SelectedItem.TRANSFER_AMT = data[i].TRANSFER_AMT;
                            SelectedItem.TRANSFER_BANK_AC = data[i].TRANSFER_BANK_AC;
                            SelectedItem.TRANSFER_BANK_BRANCH = data[i].TRANSFER_BANK_BRANCH;
                            SelectedItem.TRANSFER_DATE = data[i].TRANSFER_DATE;
                        }
                        App.Current.Properties["SuppPayView"] = SelectedItem;
                        SupplierPayView _SPV = new SupplierPayView();
                        _SPV.Show();
                    }
                }
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
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();

        }
        public ICommand _SupplierListClick;
        public ICommand SupplierListClick
        {
            get
            {
                if (_SupplierListClick == null)
                {
                    _SupplierListClick = new DelegateCommand(SupplierList_Click);
                }
                return _SupplierListClick;
            }
        }

        public void SupplierList_Click()
        {
            App.Current.Properties["SupplierSupItem"] = 1;
            Window_SupplierList IA = new Window_SupplierList();
            IA.Show();

        }
        public ICommand _ChequeBankBranchClick;
        public ICommand ChequeBankBranchClick
        {
            get
            {
                if (_ChequeBankBranchClick == null)
                {
                    _ChequeBankBranchClick = new DelegateCommand(Cheque_Bank_BranchClick);
                }
                return _ChequeBankBranchClick;
            }
        }
        public void Cheque_Bank_BranchClick()
        {
            App.Current.Properties["ChequeBankBranch"] = 2;
            Window_Bank_AccountList aa = new Window_Bank_AccountList();
            aa.Show();
        }
        public ICommand _ChequeBankACClick;
        public ICommand ChequeBankACClick
        {
            get
            {
                if (_ChequeBankACClick == null)
                {
                    _ChequeBankACClick = new DelegateCommand(Cheque_Bank_ACClick);
                }
                return _ChequeBankACClick;
            }
        }
        public void Cheque_Bank_ACClick()
        {
            App.Current.Properties["ChequeBankACClick"] = 1;
            Window_Bank_AccountList aa = new Window_Bank_AccountList();
            aa.Show();
        }
        public ICommand _BankACClick;
        public ICommand BankACClick
        {
            get
            {
                if (_BankACClick == null)
                {
                    _BankACClick = new DelegateCommand(Bank_AC_Click);
                }
                return _BankACClick;
            }
        }
        public void Bank_AC_Click()
        {
            App.Current.Properties["BankACClick"] = 3;
            Window_Bank_AccountList aa = new Window_Bank_AccountList();
            aa.Show();
        }
        public ICommand _BankBranchClick;
        public ICommand BankBranchClick
        {
            get
            {
                if (_BankBranchClick == null)
                {
                    _BankBranchClick = new DelegateCommand(Bank_Branch_Click);
                }
                return _BankBranchClick;
            }
        }
        public void Bank_Branch_Click()
        {
            App.Current.Properties["BankBranchClick"] = 4;
            Window_Bank_AccountList aa = new Window_Bank_AccountList();
            aa.Show();
        }
        public ICommand _FinancialACClick;
        public ICommand FinancialACClick
        {
            get
            {
                if (_FinancialACClick == null)
                {
                    _FinancialACClick = new DelegateCommand(Financial_AC_Click);
                }
                return _FinancialACClick;
            }
        }
        public void Financial_AC_Click()
        {
            App.Current.Properties["FinancialACClick"] = 5;
            Window_Bank_AccountList aa = new Window_Bank_AccountList();
            aa.Show();
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
