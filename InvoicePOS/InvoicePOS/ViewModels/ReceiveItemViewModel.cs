using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.Employee;
using InvoicePOS.UserControll.GoDown;
using InvoicePOS.UserControll.Item;
using InvoicePOS.UserControll.PO;
using InvoicePOS.UserControll.Supplier;
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
    public class ReceiveItemViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {

        public HttpResponseMessage response;
        // public HttpClient client = new HttpClient();
        private readonly ReceiveItem RcvItemModel;
        ReceiveItem _RcvItemModel = new ReceiveItem();
        public ICommand select { get; set; }
        ReceiveItem[] data = null;
        ItemModel[] data1 = null;
        int x = 0;
        private ReceiveItem _selectedRecItem;
        ReceiveItem _ReceiveItem = new ReceiveItem();
        List<ReceiveItem> _ListGrid_Temp = new List<ReceiveItem>();
        public ObservableCollection<ReceiveItem> _ListGridd { get; set; }
        public ReceiveItemViewModel()
        {
            // SelectedRecItem = new ReceiveItem();

            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            //OprModel = _ItemModel;
            if (App.Current.Properties["Action"].ToString() == "Add")
            {
                CreatVisible = "Visible";
                UpdVisible = "Collapsed";
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                SelectedRecItem = App.Current.Properties["ItemRecvView"] as ReceiveItem;
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "Edit")
            {;
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedRecItem = App.Current.Properties["ItemRecvEdit"] as ReceiveItem;
                App.Current.Properties["Action"] = "";
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                var fdr = GetRecItemRefNo();
                //string dd = Convert.ToString(fdr.Result);
                //string val =Convert.ToString(Convert.ToInt32( dd.Substring(3, 5))+1);
                //var refno = "R-" + val;
                // SelectedRecItem = new ReceiveItem();
                // SelectedRecItem.RECEIVED_ITEM_ENTRY_NO = Convert.ToString(refno);
                string dd = Convert.ToString(fdr.Result);
                SelectedRecItem = new ReceiveItem();
                SelectedRecItem.RECEIVE_DATE = System.DateTime.Now;
                SelectedRecItem.SUPPLIER_INVOICE_DATE = System.DateTime.Now;
                RECEIVED_ITEM_ON_DATE = System.DateTime.Now;
                if (dd != null)
                {
                    string aaa = dd.Substring(3, 5);
                    int xx = Convert.ToInt32(aaa);
                    int noo = Convert.ToInt32(xx + 1);
                    string nnnn = "R-" + noo.ToString("D5");
                    SelectedRecItem.RECEIVED_ITEM_ENTRY_NO = Convert.ToString(nnnn);
                    GetRecItem(comp);
                }
                else
                {
                    SelectedRecItem.RECEIVED_ITEM_ENTRY_NO = "R-00000";
                
                }
            }
        }

        //  EditRecvItm

        
        private long? _SELECT_BUSINESS_LOCATION_ID;
        public long? SELECT_BUSINESS_LOCATION_ID
        {
            get
            {
                return SelectedRecItem.SELECT_BUSINESS_LOCATION_ID;
            }
            set
            {
                SelectedRecItem.SELECT_BUSINESS_LOCATION_ID = value;
                if (SelectedRecItem.SELECT_BUSINESS_LOCATION_ID != value)
                {
                    SelectedRecItem.SELECT_BUSINESS_LOCATION_ID = value;
                    OnPropertyChanged("SELECT_BUSINESS_LOCATION_ID");
                }
            }
        }
        private string _SELECT_BUSINESS_LOCATION;
        public string SELECT_BUSINESS_LOCATION
        {
            get
            {
                return SelectedRecItem.SELECT_BUSINESS_LOCATION;
            }
            set
            {
                SelectedRecItem.SELECT_BUSINESS_LOCATION = value;
                if (SelectedRecItem.SELECT_BUSINESS_LOCATION != value)
                {
                    SelectedRecItem.SELECT_BUSINESS_LOCATION = value;
                    OnPropertyChanged("SELECT_BUSINESS_LOCATION");
                }
            }
        }
        private string _RECEIVED_ITEM_ENTRY_NO;
        public string RECEIVED_ITEM_ENTRY_NO
        {
            get
            {
                return SelectedRecItem.RECEIVED_ITEM_ENTRY_NO;
            }
            set
            {
                SelectedRecItem.RECEIVED_ITEM_ENTRY_NO = "123456";
                if (SelectedRecItem.RECEIVED_ITEM_ENTRY_NO != value)
                {
                    SelectedRecItem.RECEIVED_ITEM_ENTRY_NO = value;
                    OnPropertyChanged("RECEIVED_ITEM_ENTRY_NO");
                }
            }
        }

        private decimal _PURCHASE_PRICE_BEFORE_TAX;
        public decimal PURCHASE_PRICE_BEFORE_TAX
        {
            get
            {
                return SelectedRecItem.PURCHASE_PRICE_BEFORE_TAX;
            }
            set
            {
                SelectedRecItem.PURCHASE_PRICE_BEFORE_TAX = value;


                if (SelectedRecItem.PURCHASE_PRICE_BEFORE_TAX != value)
                {
                    SelectedRecItem.PURCHASE_PRICE_BEFORE_TAX = value;
                    OnPropertyChanged("PURCHASE_PRICE_BEFORE_TAX");
                }
            }
        }

        

        private decimal? _SUB_TOTAL_BEFORE_TAX;
        public decimal? SUB_TOTAL_BEFORE_TAX
        {
            get
            {
                return SelectedRecItem.SUB_TOTAL_BEFORE_TAX;
            }
            set
            {
                SelectedRecItem.SUB_TOTAL_BEFORE_TAX = value;


                if (SelectedRecItem.SUB_TOTAL_BEFORE_TAX != value)
                {
                    SelectedRecItem.SUB_TOTAL_BEFORE_TAX = value;
                    OnPropertyChanged("SUB_TOTAL_BEFORE_TAX");
                }
            }
        }

        private decimal? _SUB_TOTAL_AFTER_TAX;
        public decimal? SUB_TOTAL_AFTER_TAX
        {
            get
            {
                return SelectedRecItem.SUB_TOTAL_AFTER_TAX;
            }
            set
            {
                SelectedRecItem.SUB_TOTAL_AFTER_TAX = value;


                if (SUB_TOTAL_AFTER_TAX != value)
                {
                    SelectedRecItem.SUB_TOTAL_AFTER_TAX = value;
                    OnPropertyChanged("SUB_TOTAL_AFTER_TAX");
                }
            }
        }


        //private decimal _TOTAL_AMOUNT;
        //public decimal TOTAL_AMOUNT
        //{
        //    get
        //    {
        //        return SelectedRecItem.TOTAL_AMOUNT;
        //    }
        //    set
        //    {
        //        SelectedRecItem.TOTAL_AMOUNT = value;


        //        if (SelectedRecItem.TOTAL_AMOUNT != value)
        //        {
        //            SelectedRecItem.TOTAL_AMOUNT = value;
        //            OnPropertyChanged("TOTAL_AMOUNT");
        //        }
        //    }
        //}


        //private decimal _TOTAL_TAX;
        //public decimal TOTAL_TAX
        //{
        //    get
        //    {
        //        return SelectedRecItem.TOTAL_TAX;
        //    }
        //    set
        //    {
        //        SelectedRecItem.TOTAL_TAX = value;


        //        if (SelectedRecItem.TOTAL_TAX != value)
        //        {
        //            SelectedRecItem.TOTAL_TAX = value;
        //            OnPropertyChanged("TOTAL_TAX");
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


                if (_TOTAL_AMOUNT != value)
                {
                    _TOTAL_AMOUNT = value;
                    OnPropertyChanged("TOTAL_AMOUNT");
                }
            }
        }


        private decimal _TOTAL_TAX;
        public decimal TOTAL_TAX
        {
            get
            {
                return _TOTAL_TAX;
            }
            set
            {
                _TOTAL_TAX = value;


                if (_TOTAL_TAX != value)
                {
                    _TOTAL_TAX = value;
                    OnPropertyChanged("TOTAL_TAX");
                }
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
                //if (SelectedItem != null)
                //{
                    _TOTAL_QTY = value;
                //}
                //else
                //    MessageBox.Show("Please Select Item");
                OnPropertyChanged("TOTAL_QTY");

            }
        }

        private int? _TOTPO_QNT;
        public int? TOTPO_QNT
        {
            get
            {
                return _TOTPO_QNT;
            }
            set
            {
                //if (SelectedItem != null)
                //{
                    _TOTPO_QNT = value;
                //}
                //else
                    //MessageBox.Show("Please Select Item");
                OnPropertyChanged("TOTPO_QNT");

            }
        }



        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedRecItem.COMPANY_ID;
            }
            set
            {
                SelectedRecItem.COMPANY_ID = value;
                if (SelectedRecItem.COMPANY_ID != value)
                {
                    SelectedRecItem.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }
        private string _BARCODE;
        public string BARCODE
        {
            get
            {
                return SelectedRecItem.BARCODE;
            }
            set
            {
                SelectedRecItem.BARCODE = value;
                if (SelectedRecItem.BARCODE != value)
                {
                    SelectedRecItem.BARCODE = value;
                    OnPropertyChanged("BARCODE");
                }
            }
        }
        private string _ITEM_NAME;
        public string ITEM_NAME
        {
            get
            {
                return SelectedRecItem.ITEM_NAME;
            }
            set
            {
                SelectedRecItem.ITEM_NAME = value;
                if (SelectedRecItem.ITEM_NAME != value)
                {
                    SelectedRecItem.ITEM_NAME = value;
                    OnPropertyChanged("ITEM_NAME");
                }
            }
        }
        private string _SEARCH_CODE;
        public string SEARCH_CODE
        {
            get
            {
                return SelectedRecItem.SEARCH_CODE;
            }
            set
            {
                SelectedRecItem.SEARCH_CODE = value;
                if (SelectedRecItem.SEARCH_CODE != value)
                {
                    SelectedRecItem.SEARCH_CODE = value;
                    OnPropertyChanged("SEARCH_CODE");
                }
            }
        }
        private string _SUPPLIER_INVOICE_NO;
        public string SUPPLIER_INVOICE_NO
        {
            get
            {
                return SelectedRecItem.SUPPLIER_INVOICE_NO;
            }
            set
            {
                SelectedRecItem.SUPPLIER_INVOICE_NO = value;
                if (SelectedRecItem.SUPPLIER_INVOICE_NO != value)
                {
                    SelectedRecItem.SUPPLIER_INVOICE_NO = value;
                    OnPropertyChanged("SUPPLIER_INVOICE_NO");
                }
            }
        }
        private DateTime _SUPPLIER_INVOICE_DATE;
        public DateTime SUPPLIER_INVOICE_DATE
        {
            get
            {
                return SelectedRecItem.SUPPLIER_INVOICE_DATE;
            }
            set
            {
                SelectedRecItem.SUPPLIER_INVOICE_DATE = value;
                if (SelectedRecItem.SUPPLIER_INVOICE_DATE != value)
                {
                    SelectedRecItem.SUPPLIER_INVOICE_DATE = System.DateTime.Now;
                    OnPropertyChanged("SUPPLIER_INVOICE_DATE");
                }
            }
        }


        private DateTime _RECEIVE_DATE;
        public DateTime RECEIVE_DATE
        {
            get
            {
                return SelectedRecItem.RECEIVE_DATE;
            }
            set
            {
                SelectedRecItem.RECEIVE_DATE = value;
                if (SelectedRecItem.RECEIVE_DATE != value)
                {
                    SelectedRecItem.RECEIVE_DATE = System.DateTime.Now;
                    OnPropertyChanged("RECEIVE_DATE");
                }
            }
        }
        private DateTime _RECEIVED_ITEM_ON_DATE;
        public DateTime RECEIVED_ITEM_ON_DATE
        {
            get
            {
                return SelectedRecItem.RECEIVED_ITEM_ON_DATE;
            }
            set
            {
                SelectedRecItem.RECEIVED_ITEM_ON_DATE = value;
                if (SelectedRecItem.RECEIVED_ITEM_ON_DATE != value)
                {
                    SelectedRecItem.RECEIVED_ITEM_ON_DATE = value;
                    OnPropertyChanged("RECEIVED_ITEM_ON_DATE");
                }
            }
        }
        private long _PO_NUMBER_ID;
        public long PO_NUMBER_ID
        {
            get
            {
                return SelectedRecItem.PO_NUMBER_ID;
            }
            set
            {
                SelectedRecItem.PO_NUMBER_ID = value;
                if (SelectedRecItem.PO_NUMBER_ID != value)
                {
                    SelectedRecItem.PO_NUMBER_ID = value;
                    OnPropertyChanged("PO_NUMBER_ID");
                }
            }
        }
        private string _PO_NUMBER;
        public string PO_NUMBER
        {
            get
            {
                return SelectedRecItem.PO_NUMBER;
            }
            set
            {
                SelectedRecItem.PO_NUMBER = value;
                if (SelectedRecItem.PO_NUMBER != value)
                {
                    SelectedRecItem.PO_NUMBER = value;
                    OnPropertyChanged("PO_NUMBER");
                }
            }
        }
        private long _SUPPLIER_ID;
        public long SUPPLIER_ID
        {
            get
            {
                return SelectedRecItem.SUPPLIER_ID;
            }
            set
            {
                SelectedRecItem.SUPPLIER_ID = value;
                if (SelectedRecItem.SUPPLIER_ID != value)
                {
                    SelectedRecItem.SUPPLIER_ID = value;
                    OnPropertyChanged("SUPPLIER_ID");
                }
            }
        }
        private string _SUPPLIER;
        public string SUPPLIER
        {
            get
            {
                return SelectedRecItem.SUPPLIER;
            }
            set
            {
                SelectedRecItem.SUPPLIER = value;
                if (SelectedRecItem.SUPPLIER != value)
                {
                    SelectedRecItem.SUPPLIER = value;
                    OnPropertyChanged("SUPPLIER");
                }
            }
        }
        private string _PAYMENT_TERMS;
        public string PAYMENT_TERMS
        {
            get
            {
                return SelectedRecItem.PAYMENT_TERMS;
            }
            set
            {
                SelectedRecItem.PAYMENT_TERMS = value;
                if (SelectedRecItem.PAYMENT_TERMS != value)
                {
                    SelectedRecItem.PAYMENT_TERMS = value;
                    OnPropertyChanged("PAYMENT_TERMS");
                }
            }
        }
        private long _GODOWN_ID;
        public long GODOWN_ID
        {
            get
            {
                return SelectedRecItem.GODOWN_ID;
            }
            set
            {
                SelectedRecItem.GODOWN_ID = value;
                if (SelectedRecItem.GODOWN_ID != value)
                {
                    SelectedRecItem.GODOWN_ID = value;
                    OnPropertyChanged("GODOWN_ID");
                }
            }
        }
        private string _GODOWN;
        public string GODOWN
        {
            get
            {
                return SelectedRecItem.GODOWN;
            }
            set
            {
                SelectedRecItem.GODOWN = value;
                if (SelectedRecItem.GODOWN != value)
                {
                    SelectedRecItem.GODOWN = value;
                    OnPropertyChanged("GODOWN");
                }
            }
        }
        private string _RECEIVING_EMPLOYEE;
        public string RECEIVING_EMPLOYEE
        {
            get
            {
                return SelectedRecItem.RECEIVING_EMPLOYEE;
            }
            set
            {
                SelectedRecItem.RECEIVING_EMPLOYEE = value;
                if (SelectedRecItem.RECEIVING_EMPLOYEE != value)
                {
                    SelectedRecItem.RECEIVING_EMPLOYEE = value;
                    OnPropertyChanged("RECEIVING_EMPLOYEE");
                }
            }
        }
        private decimal _DISCOUNT_FLAT;
        public decimal DISCOUNT_FLAT
        {
            get
            {
                return SelectedRecItem.DISCOUNT_FLAT;
            }
            set
            {
                SelectedRecItem.DISCOUNT_FLAT = value;
                if (SelectedRecItem.DISCOUNT_FLAT != value)
                {
                    SelectedRecItem.DISCOUNT_FLAT = value;
                    OnPropertyChanged("DISCOUNT_FLAT");
                }
            }
        }
        private decimal _DISCOUNT_PERCENT;
        public decimal DISCOUNT_PERCENT
        {
            get
            {
                return SelectedRecItem.DISCOUNT_PERCENT;
            }
            set
            {
                SelectedRecItem.DISCOUNT_PERCENT = value;
                if (SelectedRecItem.DISCOUNT_PERCENT != value)
                {
                    SelectedRecItem.DISCOUNT_PERCENT = value;
                    OnPropertyChanged("DISCOUNT_PERCENT");
                }
            }
        }
        private decimal _ADDTIONAL_CHARGES;
        public decimal ADDTIONAL_CHARGES
        {
            get
            {
                return SelectedRecItem.ADDTIONAL_CHARGES;
            }
            set
            {
                SelectedRecItem.ADDTIONAL_CHARGES = value;
                if (SelectedRecItem.ADDTIONAL_CHARGES != value)
                {
                    SelectedRecItem.ADDTIONAL_CHARGES = value;
                    OnPropertyChanged("ADDTIONAL_CHARGES");
                }
            }
        }
        //private decimal _SUB_TOTAL_BEFORETAX;
        //public decimal SUB_TOTAL_BEFORETAX
        //{
        //    get
        //    {
        //        return SelectedRecItem.SUB_TOTAL_BEFORETAX;
        //    }
        //    set
        //    {
        //        SelectedRecItem.SUB_TOTAL_BEFORETAX = value;
        //        if (SelectedRecItem.SUB_TOTAL_BEFORETAX != value)
        //        {
        //            SelectedRecItem.SUB_TOTAL_BEFORETAX = value;
        //            OnPropertyChanged("SUB_TOTAL_BEFORETAX");
        //        }
        //    }
        //}


        private decimal _SUB_TOTAL_BEFORETAX;
        public decimal SUB_TOTAL_BEFORETAX
        {
            get
            {
                return _SUB_TOTAL_BEFORETAX;
            }
            set
            {
                _SUB_TOTAL_BEFORETAX = value;
                if (_SUB_TOTAL_BEFORETAX != value)
                {
                    _SUB_TOTAL_BEFORETAX = value;
                    OnPropertyChanged("SUB_TOTAL_BEFORETAX");
                }
            }
        }
        //private decimal _TOTAL_TAX_AMT;
        //public decimal TOTAL_TAX_AMT
        //{
        //    get
        //    {
        //        return SelectedRecItem.TOTAL_TAX_AMT;
        //    }
        //    set
        //    {
        //        SelectedRecItem.TOTAL_TAX_AMT = value;
        //        if (SelectedRecItem.TOTAL_TAX_AMT != value)
        //        {
        //            SelectedRecItem.TOTAL_TAX_AMT = value;
        //            OnPropertyChanged("TOTAL_TAX_AMT");
        //        }
        //    }
        //}
        //private decimal _SUB_TOTAL;
        //public decimal SUB_TOTAL
        //{
        //    get
        //    {
        //        return SelectedRecItem.SUB_TOTAL;
        //    }
        //    set
        //    {
        //        SelectedRecItem.SUB_TOTAL = value;
        //        if (SelectedRecItem.SUB_TOTAL != value)
        //        {
        //            SelectedRecItem.SUB_TOTAL = value;
        //            OnPropertyChanged("SUB_TOTAL");
        //        }
        //    }
        //}
        //private decimal _ROUND_OFF_ADJUSTMENTAMT;
        //public decimal ROUND_OFF_ADJUSTMENTAMT
        //{
        //    get
        //    {
        //        return SelectedRecItem.ROUND_OFF_ADJUSTMENTAMT;
        //    }
        //    set
        //    {
        //        SelectedRecItem.ROUND_OFF_ADJUSTMENTAMT = value;
        //        if (SelectedRecItem.ROUND_OFF_ADJUSTMENTAMT != value)
        //        {
        //            SelectedRecItem.ROUND_OFF_ADJUSTMENTAMT = value;
        //            OnPropertyChanged("ROUND_OFF_ADJUSTMENTAMT");
        //        }
        //    }
        //}


        private decimal _TOTAL_TAX_AMT;
        public decimal TOTAL_TAX_AMT
        {
            get
            {
                return _TOTAL_TAX_AMT;
            }
            set
            {
                _TOTAL_TAX_AMT = value;
                if (_TOTAL_TAX_AMT != value)
                {
                    _TOTAL_TAX_AMT = value;
                    OnPropertyChanged("TOTAL_TAX_AMT");
                }
            }
        }
        private decimal _SUB_TOTAL;
        public decimal SUB_TOTAL
        {
            get
            {
                return _SUB_TOTAL;
            }
            set
            {
                _SUB_TOTAL = value;
                if (_SUB_TOTAL != value)
                {
                    _SUB_TOTAL = value;
                    OnPropertyChanged("SUB_TOTAL");
                }
            }
        }
        private decimal _ROUND_OFF_ADJUSTMENTAMT;
        public decimal ROUND_OFF_ADJUSTMENTAMT
        {
            get
            {
                return _ROUND_OFF_ADJUSTMENTAMT;
            }
            set
            {
                _ROUND_OFF_ADJUSTMENTAMT = value;
                if (_ROUND_OFF_ADJUSTMENTAMT != value)
                {
                    _ROUND_OFF_ADJUSTMENTAMT = value;
                    OnPropertyChanged("ROUND_OFF_ADJUSTMENTAMT");
                }
            }
        }

        private string _FILTER_HIERARCHY;
        public string FILTER_HIERARCHY
        {
            get
            {
                return SelectedRecItem.FILTER_HIERARCHY;
            }
            set
            {
                SelectedRecItem.FILTER_HIERARCHY = value;
                if (SelectedRecItem.FILTER_HIERARCHY != value)
                {
                    SelectedRecItem.FILTER_HIERARCHY = value;
                    OnPropertyChanged("FILTER_HIERARCHY");
                }
            }
        }
        private decimal _TOTAL_AMT;
        public decimal TOTAL_AMT
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
        private int _NEWRECEIVE_ID;
        public int NEWRECEIVE_ID
        {
            get
            {
                return SelectedRecItem.NEWRECEIVE_ID;
            }
            set
            {
                SelectedRecItem.NEWRECEIVE_ID = value;
                if (SelectedRecItem.NEWRECEIVE_ID != value)
                {
                    SelectedRecItem.NEWRECEIVE_ID = value;
                    OnPropertyChanged("NEWRECEIVE_ID");
                }
            }
        }
        private decimal _USE_SUPPLIER_ADVANCE_AMT;
        public decimal USE_SUPPLIER_ADVANCE_AMT
        {
            get
            {
                return SelectedRecItem.USE_SUPPLIER_ADVANCE_AMT;
            }
            set
            {
                SelectedRecItem.USE_SUPPLIER_ADVANCE_AMT = value;
                if (SelectedRecItem.USE_SUPPLIER_ADVANCE_AMT != value)
                {
                    SelectedRecItem.USE_SUPPLIER_ADVANCE_AMT = value;
                    OnPropertyChanged("USE_SUPPLIER_ADVANCE_AMT");
                }
            }
        }
        private decimal _GRAND_TOTAL;
        public decimal GRAND_TOTAL
        {
            get
            {
                return SelectedRecItem.GRAND_TOTAL;
            }
            set
            {
                SelectedRecItem.GRAND_TOTAL = value;
                if (SelectedRecItem.GRAND_TOTAL != value)
                {
                    SelectedRecItem.GRAND_TOTAL = value;
                    OnPropertyChanged("GRAND_TOTAL");
                }
            }
        }
        private decimal _INVOICE_DIS_AMT;
        public decimal INVOICE_DIS_AMT
        {
            get
            {
                return SelectedRecItem.INVOICE_DIS_AMT;
            }
            set
            {
                SelectedRecItem.INVOICE_DIS_AMT = value;
                if (SelectedRecItem.INVOICE_DIS_AMT != value)
                {
                    SelectedRecItem.INVOICE_DIS_AMT = value;
                    OnPropertyChanged("INVOICE_DIS_AMT");
                }
            }
        }
        private bool _PAY_IN_CASH;
        public bool PAY_IN_CASH
        {
            get
            {
                return SelectedRecItem.PAY_IN_CASH;
            }
            set
            {
                SelectedRecItem.PAY_IN_CASH = value;
                if (SelectedRecItem.PAY_IN_CASH != value)
                {
                    SelectedRecItem.PAY_IN_CASH = value;
                    OnPropertyChanged("PAY_IN_CASH");
                }
            }
        }
        private bool _DISCOUNT_BEFOR_TAX;
        public bool DISCOUNT_BEFOR_TAX
        {
            get
            {
                return SelectedRecItem.DISCOUNT_BEFOR_TAX;
            }
            set
            {
                SelectedRecItem.DISCOUNT_BEFOR_TAX = value;
                if (SelectedRecItem.DISCOUNT_BEFOR_TAX != value)
                {
                    SelectedRecItem.DISCOUNT_BEFOR_TAX = value;
                    OnPropertyChanged("DISCOUNT_BEFOR_TAX");
                }
            }
        }
        private bool _APPLY_FPR_EFFECTVE_PRICE;
        public bool APPLY_FPR_EFFECTVE_PRICE
        {
            get
            {
                return SelectedRecItem.APPLY_FPR_EFFECTVE_PRICE;
            }
            set
            {
                SelectedRecItem.APPLY_FPR_EFFECTVE_PRICE = value;
                if (SelectedRecItem.APPLY_FPR_EFFECTVE_PRICE != value)
                {
                    SelectedRecItem.APPLY_FPR_EFFECTVE_PRICE = value;
                    OnPropertyChanged("APPLY_FPR_EFFECTVE_PRICE");
                }
            }
        }
        private bool _AUTO_SAVE_IN_DRAFT;
        public bool AUTO_SAVE_IN_DRAFT
        {
            get
            {
                return SelectedRecItem.AUTO_SAVE_IN_DRAFT;
            }
            set
            {
                SelectedRecItem.AUTO_SAVE_IN_DRAFT = value;
                if (SelectedRecItem.AUTO_SAVE_IN_DRAFT != value)
                {
                    SelectedRecItem.AUTO_SAVE_IN_DRAFT = value;
                    OnPropertyChanged("AUTO_SAVE_IN_DRAFT");
                }
            }
        }
        private bool _FOCUS_LAST_ADD_ITEM;
        public bool FOCUS_LAST_ADD_ITEM
        {
            get
            {
                return SelectedRecItem.FOCUS_LAST_ADD_ITEM;
            }
            set
            {
                SelectedRecItem.FOCUS_LAST_ADD_ITEM = value;
                if (SelectedRecItem.FOCUS_LAST_ADD_ITEM != value)
                {
                    SelectedRecItem.FOCUS_LAST_ADD_ITEM = value;
                    OnPropertyChanged("FOCUS_LAST_ADD_ITEM");
                }
            }
        }
        private bool _ITEM_ENTRY_TEMPLATE;
        public bool ITEM_ENTRY_TEMPLATE
        {
            get
            {
                return SelectedRecItem.ITEM_ENTRY_TEMPLATE;
            }
            set
            {
                SelectedRecItem.ITEM_ENTRY_TEMPLATE = value;
                if (SelectedRecItem.ITEM_ENTRY_TEMPLATE != value)
                {
                    SelectedRecItem.ITEM_ENTRY_TEMPLATE = value;
                    OnPropertyChanged("ITEM_ENTRY_TEMPLATE");
                }
            }
        }
        private bool _IS_REC_WITH_PO;
        public bool IS_REC_WITH_PO
        {
            get
            {
                return SelectedRecItem.IS_REC_WITH_PO;
            }
            set
            {
                SelectedRecItem.IS_REC_WITH_PO = value;
                if (SelectedRecItem.IS_REC_WITH_PO != value)
                {
                    SelectedRecItem.IS_REC_WITH_PO = value;
                    OnPropertyChanged("IS_REC_WITH_PO");
                }
            }
        }
        private string _NOTE;
        public string NOTE
        {
            get
            {
                return SelectedRecItem.NOTE;
            }
            set
            {
                SelectedRecItem.NOTE = value;
                if (SelectedRecItem.NOTE != value)
                {
                    SelectedRecItem.NOTE = value;
                    OnPropertyChanged("NOTE");
                }
            }
        }

        public ReceiveItem SelectedRecItem
        {
            get { return _selectedRecItem; }
            set
            {
                if (_selectedRecItem != value)
                {
                    _selectedRecItem = value;
                    OnPropertyChanged("SelectedRecItem");
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



        private string _GridItemVisible { get; set; }
        public string GridItemVisible
        {

            get { return _GridItemVisible; }
            set
            {
                if (value != _GridItemVisible)
                {
                    _GridItemVisible = value;
                    OnPropertyChanged("GridItemVisible");
                }
            }
        }

        private string _GridPOVisible { get; set; }
        public string GridPOVisible
        {

            get { return _GridPOVisible; }
            set
            {
                if (value != _GridPOVisible)
                {
                    _GridPOVisible = value;
                    OnPropertyChanged("GridPOVisible");
                }
            }
        }

        private List<ItemModel> _ItemRecvData;
        public List<ItemModel> ItemRecvData
        {
            get { return _ItemRecvData; }
            set
            {
                if (_ItemRecvData != value)
                {
                    _ItemRecvData = value;
                }
            }

        }

        public ICommand _AddReceiveItemClick;
        public ICommand AddReceiveItemClick
        {
            get
            {
                if (_AddReceiveItemClick == null)
                {
                    _AddReceiveItemClick = new DelegateCommand(AddReceiveItem_Click);
                }
                return _AddReceiveItemClick;
            }
        }

        public void AddReceiveItem_Click()
        {
            App.Current.Properties["RecItemItemReff"] = 1;
            ReceiveAddItem _ReceiveAddItem = new ReceiveAddItem();
            _ReceiveAddItem.Show();

        }
        public ICommand _EditRecvItm;
        public ICommand EditRecvItm
        {
            get
            {
                if (_EditRecvItm == null)
                {
                    _EditRecvItm = new DelegateCommand(EditRecvItm_Click);
                }
                return _EditRecvItm;
            }
        }

        public ICommand _QtyCalculate { get; set; }
        public ICommand QtyCalculate
        {
            get
            {
                if (_QtyCalculate == null)
                {

                    _QtyCalculate = new DelegateCommand(Calculation);
                }
                return _QtyCalculate;
            }
        }

       // SelectedPoItem
        private ReceiveItem _SelectedPoItem;
        public ReceiveItem SelectedPoItem
        {
            get { return _SelectedPoItem; }
            set
            {
                if (_SelectedPoItem != value)
                {
                    _SelectedPoItem = value;
                    OnPropertyChanged("SelectedPoItem");
                }
            }
        }

        public void Calculation()
        {
            if (ListGrid1 != null)
            {
                if (SelectedPoItem != null)
                {
                    if (SelectedPoItem.BARCODE != null && SelectedPoItem.BARCODE != "")
                    {
                       // var Item = (from a in ListGrid1 where a.BARCODE == SelectedPoItem.BARCODE || a.PO_NUMBER == SelectedPoItem.PO_NUMBER || a.ITEM_NAME == ReceiveAddItem.ItemReff.Text || a.SEARCH_CODE == ReceiveAddItem.SearchItemReff.Text select a).FirstOrDefault();
                        var Item = (from a in ListGrid1 where  a.PO_NUMBER == SelectedPoItem.PO_NUMBER select a).FirstOrDefault();
                      //  var RemoveItem = (from a in ListGrid1 where a.BARCODE == SelectedRecItem.BARCODE || a.ITEM_NAME == ReceiveAddItem.ItemReff.Text || a.SEARCH_CODE == ReceiveAddItem.SearchItemReff.Text select a).FirstOrDefault();
                        var RemoveItem = (from a in ListGrid1 where a.PO_NUMBER == SelectedPoItem.PO_NUMBER select a).FirstOrDefault();


                        ListGrid1.Remove(RemoveItem);
                        //x = x + 1;
                        ListGrid1.Add(new ReceiveItem
                        {

                            SLNO = x,
                            ITEM_NAME = Item.ITEM_NAME,
                           // ITEM_ID = Item.ITEM_ID,
                            BARCODE = Item.BARCODE,
                            SEARCH_CODE = Item.SEARCH_CODE,
                            PURCHASE_UNIT_PRICE = Item.PURCHASE_UNIT_PRICE,
                            PO_NUMBER = Item.PO_NUMBER,
                            PURCHASE_UNIT = Item.PURCHASE_UNIT,
                            MRP = Item.MRP,

                           TAX_PAID = Item.TAX_PAID,
                            TOTPO_QNT = Item.TOTPO_QNT,


                            PURCHASE_PRICE_BEFORE_TAX = ((decimal)(Item.PURCHASE_UNIT_PRICE * 100) / ((Item.TAX_PAID) + 100)),

                            //PURCHASE_PRICE_BEFORE_TAX = Math.Round((Item.PURCHASE_UNIT_PRICE * 100) / ((Item.TAX_PAID) + 100)),
                            TOTAL_QTY = TOTAL_QTY,

                            SUB_TOTAL_AFTER_TAX = ((TOTAL_QTY) * (Item.PURCHASE_UNIT_PRICE)),
                            SUB_TOTAL_BEFORE_TAX = ((TOTAL_QTY) * (Item.PURCHASE_PRICE_BEFORE_TAX)),

                            //}



                            //SLNO = x,
                            //PO_NUMBER = data[i].PO_NUMBER,
                            //SUPPLIER_NAME = data[i].SUPPLIER_NAME,
                            //TERMS = data[i].TERMS,
                            //ITEM_NAME = data[i].ITEM_NAME,
                            //BARCODE = data[i].BARCODE,
                            //PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE,
                            //MRP = data[i].MRP,
                            //SEARCH_CODE = data[i].SEARCH_CODE,
                            //TOTPO_QNT = data[i].TOTPO_QNT,
                            //TOTAL_QTY = 1,
                            //TAX_PAID = data[i].TAX_PAID,
                            //SALES_PRICE = data[i].SALES_PRICE,
                            //PURCHASE_UNIT = data[i].PURCHASE_UNIT,
                            //SUB_TOTAL_AFTER_TAX = 0,
                            //SUB_TOTAL_BEFORE_TAX = 0,



                        });

                        //decimal b = SUB_TOTAL_BEFORE_TAX;

                        //Math.Round(b, 2);
                      //  App.Current.Properties["DataGridLPO"] = ListGrid1;
                        ListGrid1 = ListGrid1;
                        App.Current.Properties["DataGridLRCV"] = ListGrid1;
                        //ReceiveAddItem.ListGridRef.ItemsSource = null;
                        //ReceiveAddItem.ListGridRef.ItemsSource = ListGrid1;
                    }
                    else
                    {
                        MessageBox.Show("Please Select a Row From Grid For Quantity Calculation", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                    MessageBox.Show("Please Select Item");
                //TotalBottom();

                //TOTAL_AMOUNT = 0;
                //TOTAL_TAX = 0;
                //for (int i = 0; i < ListGrid1.Count; i++)
                //{
                //    TOTAL_AMOUNT = Convert.ToDecimal(ListGrid1[i].SUB_TOTAL_AFTER_TAX + TOTAL_AMOUNT);
                //    TOTAL_TAX = Convert.ToDecimal((ListGrid1[i].SUB_TOTAL_AFTER_TAX - ListGrid1[i].SUB_TOTAL_BEFORE_TAX) + TOTAL_TAX);

                //}
                //TOTAL_TAX = Math.Round(TOTAL_TAX, 2);

                //decimal a1 = TOTAL_AMOUNT == 0 ? 0 : TOTAL_AMOUNT;
                //if (a1 != 0)
                //{
                //    string[] str1 = a1.ToString().Split('.');
                //    decimal num2 = Convert.ToDecimal("0." + str1[1]);
                //    decimal res2;
                //    if (Convert.ToDouble(num2) < 0.50)
                //    {
                //        res2 = Math.Floor(a1);
                //        TOTAL_AMOUNT = Convert.ToInt32(res2);
                //    }
                //    else
                //    {
                //        res2 = Math.Ceiling(a1);
                //        TOTAL_AMOUNT = Convert.ToInt32(res2);
                //    }

                //}
                //else
                //{
                //    TOTAL_AMOUNT = 0;
                //}


                //decimal b1 = TOTAL_TAX == 0 ? 0 : TOTAL_TAX;
                //if (b1 != 0)
                //{
                //    string[] str1 = a1.ToString().Split('.');
                //    decimal num2 = Convert.ToDecimal("0." + str1[1]);
                //    decimal res2;
                //    if (Convert.ToDouble(num2) < 0.50)
                //    {
                //        res2 = Math.Floor(b1);
                //        TOTAL_TAX = Convert.ToInt32(res2);
                //    }
                //    else
                //    {
                //        res2 = Math.Ceiling(b1);
                //        TOTAL_TAX = Convert.ToInt32(res2);
                //    }

                //}
                //else
                //{
                //    TOTAL_TAX = 0;
                //}
              



            }
           

          else  if (ListGridd != null)
            {

                if (SelectedRecItem != null)
                {
                    if (SelectedRecItem.BARCODE != null && SelectedRecItem.BARCODE != "")
                    {
                        var Item = (from a in ListGridd where a.BARCODE == SelectedRecItem.BARCODE || a.ITEM_NAME == ReceiveAddItem.ItemReff.Text || a.SEARCH_CODE == ReceiveAddItem.SearchItemReff.Text select a).FirstOrDefault();
                        //var Item = (from a in ListGrid1 where a.PO_NUMBER == SelectedPoItem.PO_NUMBER select a).FirstOrDefault();
                        var RemoveItem = (from a in ListGridd where a.BARCODE == SelectedRecItem.BARCODE || a.ITEM_NAME == ReceiveAddItem.ItemReff.Text || a.SEARCH_CODE == ReceiveAddItem.SearchItemReff.Text select a).FirstOrDefault();
                       // var RemoveItem = (from a in ListGrid1 where a.PO_NUMBER == SelectedPoItem.PO_NUMBER select a).FirstOrDefault();


                        ListGridd.Remove(RemoveItem);
                        //x = x + 1;
                        ListGridd.Add(new ReceiveItem
                        {

                            SLNO = x,
                            ITEM_NAME = Item.ITEM_NAME,
                            // ITEM_ID = Item.ITEM_ID,
                            BARCODE = Item.BARCODE,
                            SEARCH_CODE = Item.SEARCH_CODE,
                            PURCHASE_UNIT_PRICE = Item.PURCHASE_UNIT_PRICE,
                            PO_NUMBER = Item.PO_NUMBER,
                            PURCHASE_UNIT = Item.PURCHASE_UNIT,
                            MRP = Item.MRP,

                            TAX_PAID = Item.TAX_PAID,
                            TOTPO_QNT = Item.TOTPO_QNT,


                            PURCHASE_PRICE_BEFORE_TAX = ((decimal)(Item.PURCHASE_UNIT_PRICE * 100) / ((Item.TAX_PAID) + 100)),

                            //PURCHASE_PRICE_BEFORE_TAX = Math.Round((Item.PURCHASE_UNIT_PRICE * 100) / ((Item.TAX_PAID) + 100)),
                            TOTAL_QTY = TOTAL_QTY,

                            SUB_TOTAL_AFTER_TAX = ((TOTAL_QTY) * (Item.PURCHASE_UNIT_PRICE)),
                            SUB_TOTAL_BEFORE_TAX = ((TOTAL_QTY) * (Item.PURCHASE_PRICE_BEFORE_TAX)),

                            //}



                            //SLNO = x,
                            //PO_NUMBER = data[i].PO_NUMBER,
                            //SUPPLIER_NAME = data[i].SUPPLIER_NAME,
                            //TERMS = data[i].TERMS,
                            //ITEM_NAME = data[i].ITEM_NAME,
                            //BARCODE = data[i].BARCODE,
                            //PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE,
                            //MRP = data[i].MRP,
                            //SEARCH_CODE = data[i].SEARCH_CODE,
                            //TOTPO_QNT = data[i].TOTPO_QNT,
                            //TOTAL_QTY = 1,
                            //TAX_PAID = data[i].TAX_PAID,
                            //SALES_PRICE = data[i].SALES_PRICE,
                            //PURCHASE_UNIT = data[i].PURCHASE_UNIT,
                            //SUB_TOTAL_AFTER_TAX = 0,
                            //SUB_TOTAL_BEFORE_TAX = 0,



                        });

                        //decimal b = SUB_TOTAL_BEFORE_TAX;

                        //Math.Round(b, 2);
                        //  App.Current.Properties["DataGridLPO"] = ListGrid1;
                        ListGridd = ListGridd;
                        App.Current.Properties["DataGridLRCV"] = ListGridd;
                        ReceiveAddItem.ListGridItemRef.ItemsSource = null;
                        ReceiveAddItem.ListGridItemRef.ItemsSource = ListGridd;
                    }
                    else
                    {
                        MessageBox.Show("Please Select a Row From Grid For Quantity Calculation", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                    MessageBox.Show("Please Select Item");
                TotalBottom();

                //TOTAL_AMOUNT = 0;
                //TOTAL_TAX = 0;
                //for (int i = 0; i < ListGrid1.Count; i++)
                //{
                //    TOTAL_AMOUNT = Convert.ToDecimal(ListGrid1[i].SUB_TOTAL_AFTER_TAX + TOTAL_AMOUNT);
                //    TOTAL_TAX = Convert.ToDecimal((ListGrid1[i].SUB_TOTAL_AFTER_TAX - ListGrid1[i].SUB_TOTAL_BEFORE_TAX) + TOTAL_TAX);

                //}
                //TOTAL_TAX = Math.Round(TOTAL_TAX, 2);

                //decimal a1 = TOTAL_AMOUNT == 0 ? 0 : TOTAL_AMOUNT;
                //if (a1 != 0)
                //{
                //    string[] str1 = a1.ToString().Split('.');
                //    decimal num2 = Convert.ToDecimal("0." + str1[1]);
                //    decimal res2;
                //    if (Convert.ToDouble(num2) < 0.50)
                //    {
                //        res2 = Math.Floor(a1);
                //        TOTAL_AMOUNT = Convert.ToInt32(res2);
                //    }
                //    else
                //    {
                //        res2 = Math.Ceiling(a1);
                //        TOTAL_AMOUNT = Convert.ToInt32(res2);
                //    }

                //}
                //else
                //{
                //    TOTAL_AMOUNT = 0;
                //}


                //decimal b1 = TOTAL_TAX == 0 ? 0 : TOTAL_TAX;
                //if (b1 != 0)
                //{
                //    string[] str1 = a1.ToString().Split('.');
                //    decimal num2 = Convert.ToDecimal("0." + str1[1]);
                //    decimal res2;
                //    if (Convert.ToDouble(num2) < 0.50)
                //    {
                //        res2 = Math.Floor(b1);
                //        TOTAL_TAX = Convert.ToInt32(res2);
                //    }
                //    else
                //    {
                //        res2 = Math.Ceiling(b1);
                //        TOTAL_TAX = Convert.ToInt32(res2);
                //    }

                //}
                //else
                //{
                //    TOTAL_TAX = 0;
                //}
              




            }

            else
            {
                MessageBox.Show("First Enter Barcode/Item Name/Search Code For Quantity Calculation", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
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
                _Select_BarCode = value;
              //  App.Current.Properties["CurrentBarcode"] = value;
                OnPropertyChanged("Select_BarCode");


            }

        }

        private ICommand _TabChangeCode { get; set; }
        public ICommand TabChangeCode
        {
            get
            {
                if (_TabChangeCode == null)
                {
                    _TabChangeCode = new DelegateCommand(TabChange_Code);
                }
                return _TabChangeCode;
            }
        }

        public void TabChange_Code()
        {
            //if (ReceiveAddItem.PoReff.Text != null || ReceiveAddItem.PoReff.Text!="")
            //{
            //    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure, You want to remove all data of the PO Number " + ReceiveAddItem.PoReff.Text + "?", "PO Confirmation", System.Windows.MessageBoxButton.YesNo);
            //    if (messageBoxResult == MessageBoxResult.Yes)
            //    {
            //        var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            //        GridPOVisible = "Collapsed";
            //        GridItemVisible = "Visible";
            //        GetItemList(comp);


            //        _Select_BarCode = "";
            //        Select_BarCode = "";
            //        ReceiveAddItem.ItemReff.Text = "";
            //        ReceiveAddItem.SearchItemReff.Text = "";
                    
            //    }
            //}
            //else
            //{
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                GridPOVisible = "Collapsed";
                GridItemVisible = "Visible";
                GetItemList(comp);


                _Select_BarCode = "";
                Select_BarCode = "";
                ReceiveAddItem.ItemReff.Text = "";
                ReceiveAddItem.SearchItemReff.Text = "";
                    
            //}
        }


        ObservableCollection<ReceiveItem> AddListGrid1 = new ObservableCollection<ReceiveItem>();
        public async void GetItemList(int comp)
        {
            ObservableCollection<ReceiveItem> _ListGrid_Temp1 = new ObservableCollection<ReceiveItem>();
            //ItemData = new ObservableCollection<ItemModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
             //HttpResponseMessage response = client.GetAsync("api/ItemRecAPI/GetRecvItem?id=" + comp + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data1 = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                if (data1.Length > 0)
                {
                    _ListGrid_Temp1.Clear();
                    int x=0;
                    for (int i = 0; i < data1.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp1.Add(new ReceiveItem
                        {

                            //Discount = data1[i].Discount,
                            SLNO = x,
                            ITEM_NAME = data1[i].ITEM_NAME,
                            // ITEMID = data1[i].ITEM_ID,
                            BARCODE = data1[i].BARCODE,
                            PURCHASE_UNIT_PRICE = data1[i].PURCHASE_UNIT_PRICE,
                            PURCHASE_UNIT=data1[i].PURCHASE_UNIT,
                            MRP = data1[i].MRP,
                            SEARCH_CODE = data1[i].SEARCH_CODE,
                            SALES_PRICE = data1[i].SALES_PRICE,
                            TAX_PAID = data1[i].TAX_PAID,
                            TaxName = data1[i].TAX_PAID_NAME,
                            //TOTPO_QNT = data[i].TOTPO_QNT,
                            TOTAL_QTY = 0,
                            SUB_TOTAL_AFTER_TAX = 0,
                            SUB_TOTAL_BEFORE_TAX = 0,
                           PURCHASE_PRICE_BEFORE_TAX = ((decimal)(data1[i].PURCHASE_UNIT_PRICE * 100) / ((data1[i].TAX_PAID) + 100)),

                        });
                    }

                    for (int i = 0; i < _ListGrid_Temp1.Count; i++)
                    {
                        (_ListGrid_Temp1[i].PURCHASE_PRICE_BEFORE_TAX) = ((decimal)(_ListGrid_Temp1[i].PURCHASE_UNIT_PRICE * 100) / ((_ListGrid_Temp1[i].TAX_PAID) + 100));
                        (_ListGrid_Temp1[i].SUB_TOTAL_AFTER_TAX) = ((decimal)(_ListGrid_Temp1[i].TOTAL_QTY) * (_ListGrid_Temp1[i].PURCHASE_UNIT_PRICE));
                        (_ListGrid_Temp1[i].SUB_TOTAL_BEFORE_TAX) = ((decimal)(_ListGrid_Temp1[i].TOTAL_QTY) * (_ListGrid_Temp1[i].PURCHASE_PRICE_BEFORE_TAX));
                    }

                    App.Current.Properties["DataGridPuzzale1"] = _ListGrid_Temp1;
                }

                if (App.Current.Properties["DataGridLPO"] != null)
                {
                    AddListGrid1 = App.Current.Properties["DataGridLPO"] as ObservableCollection<ReceiveItem>;
                    if (App.Current.Properties["EditGridDataa"] != null)
                    {
                        AddListGrid1 = App.Current.Properties["EditGridData"] as ObservableCollection<ReceiveItem>;
                        App.Current.Properties["EditGrid"] = (from a in AddListGrid1 where a.BARCODE == Select_BarCode || a.ITEM_NAME == ReceiveAddItem.ItemReff.Text || a.SEARCH_CODE == ReceiveAddItem.SearchItemReff.Text select a).FirstOrDefault();


                    }
                }
                else
                {
                    AddListGrid1 = new ObservableCollection<ReceiveItem>();
                    if (App.Current.Properties["EditGridDataa"] != null)
                    {
                        AddListGrid1 = App.Current.Properties["EditGridData"] as ObservableCollection<ReceiveItem>;
                        App.Current.Properties["EditGrid"] = (from a in AddListGrid1 where a.BARCODE == Select_BarCode || a.ITEM_NAME == ReceiveAddItem.ItemReff.Text || a.SEARCH_CODE == ReceiveAddItem.SearchItemReff.Text select a).FirstOrDefault();


                    }

                }


                if ((Select_BarCode != null && Select_BarCode != "") || (ReceiveAddItem.ItemReff.Text != null && ReceiveAddItem.ItemReff.Text != "") || (ReceiveAddItem.SearchItemReff.Text != null && ReceiveAddItem.SearchItemReff.Text != ""))
                {


                    //var EditItem = (from a in ListGrid1 where a.BAR_CODE == Select_BarCode || a.ITEM_NAME == AddPO.ItemRef.Text || a.SEARCH_CODE == AddPO.ScrRef.Text select a).FirstOrDefault();
                    //var  EditItem = App.Current.Properties["EditItemGrid"] ;
                    if (App.Current.Properties["EditGrid"] != null)
                    {
                        var EditItem = (from a in ListGridd where a.BARCODE == Select_BarCode || a.ITEM_NAME == ReceiveAddItem.ItemReff.Text || a.SEARCH_CODE == ReceiveAddItem.SearchItemReff.Text select a).FirstOrDefault();
                        //SelectedItem = EditItem;

                    }
                    else
                    {


                        var itemToRemove = (from m in _ListGrid_Temp1 where m.BARCODE == Select_BarCode || m.ITEM_NAME == ReceiveAddItem.ItemReff.Text || m.SEARCH_CODE == ReceiveAddItem.SearchItemReff.Text select m).ToList();
                        ObservableCollection<ReceiveItem> myCollection1 = new ObservableCollection<ReceiveItem>(itemToRemove);
                        var Item = (from a in AddListGrid1 where a.BARCODE == Select_BarCode || a.ITEM_NAME == ReceiveAddItem.ItemReff.Text || a.SEARCH_CODE == ReceiveAddItem.SearchItemReff.Text select a).FirstOrDefault();
                        var REMOVEI = (from a in AddListGrid1 where a.BARCODE == Select_BarCode || a.ITEM_NAME == ReceiveAddItem.ItemReff.Text || a.SEARCH_CODE == ReceiveAddItem.SearchItemReff.Text select a).FirstOrDefault();



                        foreach (var item in myCollection1)
                        {

                            AddListGrid1.Remove(REMOVEI);

                            x = x + 1;
                            AddListGrid1.Add(new ReceiveItem
                            {

                                SLNO = x,
                                ITEM_NAME = item.ITEM_NAME,
                                // ITEM_ID = item.ITEM_ID,
                                BARCODE = item.BARCODE,
                                TOTAL_QTY = item.TOTAL_QTY,
                                PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                                PURCHASE_UNIT = item.PURCHASE_UNIT,
                                MRP = item.MRP,
                                SEARCH_CODE = item.SEARCH_CODE,
                                //TAX_COLLECTED = data1[i].TAX_COLLECTED,
                                TAX_PAID = item.TAX_PAID,
                                TaxName = item.TaxName,
                                PURCHASE_PRICE_BEFORE_TAX = ((decimal)(item.PURCHASE_UNIT_PRICE * 100) / ((item.TAX_PAID) + 100)),
                                SUB_TOTAL_AFTER_TAX = 0,
                                SUB_TOTAL_BEFORE_TAX = 0,

                                
                            });

                        }

                        App.Current.Properties["DataGridLPO"] = AddListGrid1;
                        App.Current.Properties["DataGridPO"] = AddListGrid1;
                        ListGridd = AddListGrid1;
                        ReceiveAddItem.ListGridItemRef.ItemsSource = AddListGrid1;
                        TotalBottom();

                    }

                }
            }

        }


        public void TotalBottom()
        {
            SUB_TOTAL_BEFORETAX = 0;

            TOTAL_AMOUNT = 0;
            TOTAL_TAX = 0;
            if (ListGrid1 != null)
            {
                for (int i = 0; i < ListGrid1.Count; i++)
                {
                    SUB_TOTAL_BEFORETAX = Convert.ToDecimal(ListGrid1[i].SUB_TOTAL_BEFORE_TAX + SUB_TOTAL_BEFORETAX);
                    TOTAL_AMOUNT = Convert.ToDecimal(ListGrid1[i].SUB_TOTAL_AFTER_TAX + TOTAL_AMOUNT);
                    //App.Current.Properties["CurrentGrosAmount1"] = TOTAL_AMOUNT;
                    TOTAL_TAX = Convert.ToDecimal((ListGrid1[i].SUB_TOTAL_AFTER_TAX - ListGrid1[i].SUB_TOTAL_BEFORE_TAX) + TOTAL_TAX);
                    // SUB_TOTAL = TOTAL_AMOUNT;
                }
            }
            else if (ListGridd != null)
            {
                for (int i = 0; i < ListGridd.Count; i++)
                {
                    SUB_TOTAL_BEFORETAX = Convert.ToDecimal(ListGridd[i].SUB_TOTAL_BEFORE_TAX + SUB_TOTAL_BEFORETAX);
                    TOTAL_AMOUNT = Convert.ToDecimal(ListGridd[i].SUB_TOTAL_AFTER_TAX + TOTAL_AMOUNT);
                    //App.Current.Properties["CurrentGrosAmount1"] = TOTAL_AMOUNT;
                    TOTAL_TAX = Convert.ToDecimal((ListGridd[i].SUB_TOTAL_AFTER_TAX - ListGridd[i].SUB_TOTAL_BEFORE_TAX) + TOTAL_TAX);
                    // SUB_TOTAL = TOTAL_AMOUNT;
                }
            }
            TOTAL_TAX = Math.Round(TOTAL_TAX, 2);
            ROUND_OFF_ADJUSTMENTAMT = Math.Round(TOTAL_AMOUNT, 2);
            SUB_TOTAL_BEFORETAX = Math.Round(SUB_TOTAL_BEFORETAX, 2);
            ReceiveAddItem.TaxReff.Text = TOTAL_TAX.ToString();
            ReceiveAddItem.TotAmtReff.Text = TOTAL_AMOUNT.ToString();
            ReceiveAddItem.SubTotalReff.Text = TOTAL_AMOUNT.ToString();
            
            ReceiveAddItem.SubTotalBfrTxReff.Text = SUB_TOTAL_BEFORETAX.ToString();


            decimal a1 = ROUND_OFF_ADJUSTMENTAMT == 0 ? 0 : ROUND_OFF_ADJUSTMENTAMT;
            if (a1 != 0)
            {
                string[] str1 = a1.ToString().Split('.');
                decimal num2 = Convert.ToDecimal("0." + str1[1]);
                decimal res2;
                if (Convert.ToDouble(num2) < 0.50)
                {
                    res2 = Math.Floor(a1);
                    ROUND_OFF_ADJUSTMENTAMT = Convert.ToInt32(res2);
                }
                else
                {
                    res2 = Math.Ceiling(a1);
                    ROUND_OFF_ADJUSTMENTAMT = Convert.ToInt32(res2);
                }

            }
            else
            {
                ROUND_OFF_ADJUSTMENTAMT = 0;
            }

            ReceiveAddItem.SubTotalRoundOffReff.Text = ROUND_OFF_ADJUSTMENTAMT.ToString();
        }


        public async void EditRecvItm_Click()
        {
            //if (SelectedRecItem.ITEM_NAME != null && SelectedRecItem.ITEM_NAME != "")
            //{
            if (SelectedRecItem.RECEIVED_ITEM_ENTRY_NO != null && SelectedRecItem.RECEIVED_ITEM_ENTRY_NO != "")
            {

                
                App.Current.Properties["Action"] = "Edit";
                ItemRecvData = new List<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemRecAPI/GetEditRecvItem?id=" + SelectedRecItem.NEWRECEIVE_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ReceiveItem[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {


                        for (int i = 0; i < data.Length; i++)
                        {

                            SelectedRecItem.ITEM_NAME = data[i].ITEM_NAME;
                            SelectedRecItem.BARCODE = data[i].BARCODE;
                            SelectedRecItem.GODOWN_ID = data[i].GODOWN_ID;
                            SelectedRecItem.IS_REC_WITH_PO = data[i].IS_REC_WITH_PO;
                            SelectedRecItem.PAY_IN_CASH = data[i].PAY_IN_CASH;
                            SelectedRecItem.PAYMENT_TERMS = data[i].PAYMENT_TERMS;
                            SelectedRecItem.PO_NUMBER = data[i].PO_NUMBER;
                            SelectedRecItem.APPLY_FPR_EFFECTVE_PRICE = data[i].APPLY_FPR_EFFECTVE_PRICE;
                            SelectedRecItem.RECEIVE_DATE = data[i].RECEIVE_DATE;
                            SelectedRecItem.RECEIVED_ITEM_ENTRY_NO = data[i].RECEIVED_ITEM_ENTRY_NO;
                            SelectedRecItem.RECEIVED_ITEM_ON_DATE = data[i].RECEIVED_ITEM_ON_DATE;
                            SelectedRecItem.RECEIVING_EMPLOYEE = data[i].RECEIVING_EMPLOYEE;
                            SelectedRecItem.ROUND_OFF_ADJUSTMENTAMT = data[i].ROUND_OFF_ADJUSTMENTAMT;
                            SelectedRecItem.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedRecItem.SELECT_BUSINESS_LOCATION_ID = data[i].SELECT_BUSINESS_LOCATION_ID;
                            SelectedRecItem.SUB_TOTAL = data[i].SUB_TOTAL;
                            SelectedRecItem.SUB_TOTAL_BEFORETAX = data[i].SUB_TOTAL_BEFORETAX;
                            SelectedRecItem.SUPPLIER_ID = data[i].SUPPLIER_ID;
                            SelectedRecItem.SUPPLIER_INVOICE_DATE = data[i].SUPPLIER_INVOICE_DATE;
                            SelectedRecItem.SUPPLIER = data[i].SUPPLIER;
                            SelectedRecItem.SELECT_BUSINESS_LOCATION = data[i].SELECT_BUSINESS_LOCATION;
                            SelectedRecItem.GODOWN = data[i].GODOWN;
                            SelectedRecItem.PO_NUMBER_ID = data[i].PO_NUMBER_ID;
                            SelectedRecItem.FILTER_HIERARCHY = data[i].FILTER_HIERARCHY;
                            SelectedRecItem.AUTO_SAVE_IN_DRAFT = data[i].AUTO_SAVE_IN_DRAFT;
                            SelectedRecItem.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedRecItem.DISCOUNT_BEFOR_TAX = data[i].DISCOUNT_BEFOR_TAX;
                            SelectedRecItem.FOCUS_LAST_ADD_ITEM = data[i].FOCUS_LAST_ADD_ITEM;
                            SelectedRecItem.ITEM_ENTRY_TEMPLATE = data[i].ITEM_ENTRY_TEMPLATE;
                            SelectedRecItem.INVOICE_DIS_AMT = data[i].INVOICE_DIS_AMT;
                            //SelectedRecItem.SUPPLIER_INVOICE_NO= data[i] ;       
                            //SelectedRecItem.TOTAL_AMT=    data[i]     ;
                            //SelectedRecItem.TOTAL_TAX_AMT=  data[i]   ;
                            //SelectedRecItem.USE_SUPPLIER_ADVANCE_AMT = data[i];     
                        }
                        App.Current.Properties["Action"] = "Edit";
                        App.Current.Properties["ItemRecvEdit"] = SelectedRecItem;
                        //Close();
                        ReceiveAddItem _ReceiveAddItem = new ReceiveAddItem();
                        _ReceiveAddItem.Show();
                        //ModalService.NavigateTo(new ReceiveAddItem(), delegate(bool returnValue) { });
                    }



                }

            }
            else
            {
                MessageBox.Show("Select Item first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }




        public ICommand _ViewRecvItm;
        public ICommand ViewRecvItm
        {
            get
            {
                if (_ViewRecvItm == null)
                {
                    _ViewRecvItm = new DelegateCommand(ViewRecvItm_Click);
                }
                return _ViewRecvItm;
            }
        }


        public async void ViewRecvItm_Click()
        {
            if (SelectedRecItem.ITEM_NAME != null && SelectedRecItem.ITEM_NAME != "")
            {
                App.Current.Properties["Action"] = "View";
                ItemRecvData = new List<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemRecAPI/GetEditRecvItem?id=" + SelectedRecItem.NEWRECEIVE_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ReceiveItem[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {


                        for (int i = 0; i < data.Length; i++)
                        {

                            SelectedRecItem.ITEM_NAME = data[i].ITEM_NAME;
                            SelectedRecItem.BARCODE = data[i].BARCODE;
                            SelectedRecItem.GODOWN_ID = data[i].GODOWN_ID;
                            SelectedRecItem.IS_REC_WITH_PO = data[i].IS_REC_WITH_PO;
                            SelectedRecItem.PAY_IN_CASH = data[i].PAY_IN_CASH;
                            SelectedRecItem.PAYMENT_TERMS = data[i].PAYMENT_TERMS;
                            SelectedRecItem.PO_NUMBER = data[i].PO_NUMBER;
                            SelectedRecItem.APPLY_FPR_EFFECTVE_PRICE = data[i].APPLY_FPR_EFFECTVE_PRICE;
                            SelectedRecItem.RECEIVE_DATE = data[i].RECEIVE_DATE;
                            SelectedRecItem.RECEIVED_ITEM_ENTRY_NO = data[i].RECEIVED_ITEM_ENTRY_NO;
                            SelectedRecItem.RECEIVED_ITEM_ON_DATE = data[i].RECEIVED_ITEM_ON_DATE;
                            SelectedRecItem.RECEIVING_EMPLOYEE = data[i].RECEIVING_EMPLOYEE;
                            SelectedRecItem.ROUND_OFF_ADJUSTMENTAMT = data[i].ROUND_OFF_ADJUSTMENTAMT;
                            SelectedRecItem.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedRecItem.SELECT_BUSINESS_LOCATION_ID = data[i].SELECT_BUSINESS_LOCATION_ID;
                            SelectedRecItem.SUB_TOTAL = data[i].SUB_TOTAL;
                            SelectedRecItem.SUB_TOTAL_BEFORETAX = data[i].SUB_TOTAL_BEFORETAX;
                            SelectedRecItem.SUPPLIER_ID = data[i].SUPPLIER_ID;
                            SelectedRecItem.SUPPLIER_INVOICE_DATE = data[i].SUPPLIER_INVOICE_DATE;
                            SelectedRecItem.SUPPLIER = data[i].SUPPLIER;
                            SelectedRecItem.SELECT_BUSINESS_LOCATION = data[i].SELECT_BUSINESS_LOCATION;
                            SelectedRecItem.GODOWN = data[i].GODOWN;
                            SelectedRecItem.PO_NUMBER_ID = data[i].PO_NUMBER_ID;
                            SelectedRecItem.FILTER_HIERARCHY = data[i].FILTER_HIERARCHY;
                            SelectedRecItem.AUTO_SAVE_IN_DRAFT = data[i].AUTO_SAVE_IN_DRAFT;
                            SelectedRecItem.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedRecItem.DISCOUNT_BEFOR_TAX = data[i].DISCOUNT_BEFOR_TAX;
                            SelectedRecItem.FOCUS_LAST_ADD_ITEM = data[i].FOCUS_LAST_ADD_ITEM;
                            SelectedRecItem.ITEM_ENTRY_TEMPLATE = data[i].ITEM_ENTRY_TEMPLATE;
                            SelectedRecItem.INVOICE_DIS_AMT = data[i].INVOICE_DIS_AMT;
                            //SelectedRecItem.SUPPLIER_INVOICE_NO= data[i] ;       
                            //SelectedRecItem.TOTAL_AMT=    data[i]     ;
                            //SelectedRecItem.TOTAL_TAX_AMT=  data[i]   ;
                            //SelectedRecItem.USE_SUPPLIER_ADVANCE_AMT = data[i];     
                        }
                        App.Current.Properties["ItemRecvView"] = SelectedRecItem;
                        ReceiveView _ReceiveAddItem = new ReceiveView();
                        _ReceiveAddItem.Show();
                    }



                }

            }
            else
            {
                MessageBox.Show("Select Item first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }



        public ICommand _MakePaymentClick;
        public ICommand MakePaymentClick
        {
            get
            {
                if (_MakePaymentClick == null)
                {
                    _MakePaymentClick = new DelegateCommand(MakePayment_Click);
                }
                return _MakePaymentClick;
            }
        }

        public void MakePayment_Click()
        {
            //ModalService.NavigateTo(new ReceiveAddItem(), delegate(bool returnValue) { });

        }




        public ICommand _AddNewItemClick;
        public ICommand AddNewItemClick
        {
            get
            {
                if (_AddNewItemClick == null)
                {
                    _AddNewItemClick = new DelegateCommand(AddNewItem_Click);
                }
                return _AddNewItemClick;
            }
        }

        public void AddNewItem_Click()
        {

            ItemAdd IA = new ItemAdd();
            IA.Show();
            // ModalService.NavigateTo(new ItemAdd(), delegate(bool returnValue) { });

        }


        public ICommand _InserRectItem;
        public ICommand InsertRecItem
        {
            get
            {
                if (_InserRectItem == null)
                {
                    _InserRectItem = new DelegateCommand(Insert_Rec_Item);
                }
                return _InserRectItem;
            }
        }

        public async void Insert_Rec_Item()
        {
            var datagrid = App.Current.Properties["DataGridLRCV"] as ObservableCollection<ReceiveItem>;

            SelectedRecItem.SelectedItem = datagrid;

            if (SelectedRecItem.BARCODE == "" || SelectedRecItem.BARCODE == null)
            {
                MessageBox.Show("Barcode Should Not be Blank");
            }
            else if (SelectedRecItem.FILTER_HIERARCHY == "" || SelectedRecItem.FILTER_HIERARCHY == null)
            {
                MessageBox.Show("Filter Hierarchy Should not be Blank");
            }
            else if (SelectedRecItem.SUPPLIER == "" || SelectedRecItem.SUPPLIER == null)
            {
                MessageBox.Show("Supplier Should not be Blank");
            }
            //else if (SelectedRecItem.GODOWN == "" || SelectedRecItem.GODOWN==null)
            //{

            //}
            else if (SelectedRecItem.RECEIVING_EMPLOYEE == "" || SelectedRecItem.RECEIVING_EMPLOYEE == null)
            {
                MessageBox.Show("Receiving Employee Should not be Blank");
            }
            //else if (SelectedRecItem.PAYMENT_TERMS == null || SelectedRecItem.PAYMENT_TERMS==0)
            //{

            //}
            else if (SelectedRecItem.DISCOUNT_FLAT == null || SelectedRecItem.DISCOUNT_FLAT == 0)
            {
                MessageBox.Show("Discount Flat Should not be Blank");
            }
            else if (SelectedRecItem.DISCOUNT_PERCENT == null || SelectedRecItem.DISCOUNT_PERCENT == 0)
            {
                MessageBox.Show("Discount Percent Should not be Blank");
            }
            else if (SelectedRecItem.SUB_TOTAL_BEFORETAX == null || SelectedRecItem.SUB_TOTAL_BEFORETAX == 0)
            {
                MessageBox.Show("Sub Total Before Tax Should not be Blank");
            }
            else if (SelectedRecItem.SUB_TOTAL == null || SelectedRecItem.SUB_TOTAL == 0)
            {
                MessageBox.Show("Sub Total Should not be Blank");
            }
            else if (SelectedRecItem.SUPPLIER == null || SelectedRecItem.SUPPLIER == "")
            {
                MessageBox.Show("Please Select a Supplier..");
            }
            else
            {
                // App.Current.Properties["Action"] = "Add";

                if (App.Current.Properties["ReceiveAddBuss"] != null)
                {
                    SelectedRecItem.SELECT_BUSINESS_LOCATION= App.Current.Properties["ReceiveAddBuss"].ToString();
                }
                if (App.Current.Properties["SearchReffRecItem"] != null)
                {
                    SelectedRecItem.SEARCH_CODE = App.Current.Properties["SearchReffRecItem"].ToString();
                }
                if (App.Current.Properties["ItemNameRecItem"] != null)
                {
                    SelectedRecItem.ITEM_NAME = App.Current.Properties["ItemNameRecItem"].ToString();
                }
                if (App.Current.Properties["RecItemPo"] != null)
                {
                    SelectedRecItem.PO_NUMBER = App.Current.Properties["RecItemPo"].ToString();
                }
                if (App.Current.Properties["GodownOpStockRef"] != null)
                {
                    SelectedRecItem.GODOWN = App.Current.Properties["GodownOpStockRef"].ToString();
                }
                if (App.Current.Properties["RecItemSupplier"] != null)
                {
                    SelectedRecItem.SUPPLIER = App.Current.Properties["RecItemSupplier"].ToString();
                }

                //var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());

                SelectedRecItem.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                SelectedRecItem.SUPPLIER_ID = Convert.ToInt32(App.Current.Properties["RecItemSupplierId"]);
                //_opr.ITEM_NAME = ITEM_NAME;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                var response = await client.PostAsJsonAsync("api/ItemRecAPI/CreateRecItem/", SelectedRecItem);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Received Item Insert Successfuly");
                    Cancel_RecItem();
                    ModalService.NavigateTo(new ReceiveItems(), delegate(bool returnValue) { });

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
                    _Cancel = new DelegateCommand(Cancel_RecItem);
                }
                return _Cancel;
            }
        }



        public void Cancel_RecItem()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        public ICommand _UpdateRectItem;
        public ICommand UpdateRecItem
        {
            get
            {
                if (_UpdateRectItem == null)
                {
                    _UpdateRectItem = new DelegateCommand(Update_Rec_Item);
                }
                return _UpdateRectItem;
            }
        }

        public async void Update_Rec_Item()
        {
            // App.Current.Properties["Action"] = "Add";
            SelectedRecItem.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString()); ;
            //_opr.ITEM_NAME = ITEM_NAME;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/ItemRecAPI/ItemRecUpdate/", SelectedRecItem);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Received Item Upadte Successfuly");
                Cancel_RecItem();
                ModalService.NavigateTo(new ReceiveItems(), delegate(bool returnValue) { });

            }

        }



        public ICommand _DeleteRecItem;
        public ICommand DeleteRecItem
        {
            get
            {
                if (_DeleteRecItem == null)
                {
                    _DeleteRecItem = new DelegateCommand(Delete_RecItem);
                }
                return _DeleteRecItem;
            }
        }
        public async void Delete_RecItem()
        {
            if (SelectedRecItem.NEWRECEIVE_ID != null && SelectedRecItem.NEWRECEIVE_ID!=0)
            {

                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Received Payment?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var id = SelectedRecItem.NEWRECEIVE_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/ItemRecAPI/DeleteRecItem?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        //ModalService.NavigateTo(new Items(), delegate(bool returnValue) { });
                        MessageBox.Show("Item Delete Successfully");
                        ModalService.NavigateTo(new ReceiveItems(), delegate(bool returnValue) { });
                    }
                }
                else
                {
                    Cancel_RecItem();
                }
            }
            else
            {
                MessageBox.Show("Select Item Rec");
            }

        }

        private List<ReceiveItem> _ItemData;
        public List<ReceiveItem> ItemData
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

        public ObservableCollection<ReceiveItem> ListGridd
        {
            get
            {
                return _ListGridd;

            }
            set
            {
                if (value != _ListGridd)
                {
                    this._ListGridd = value;
                    OnPropertyChanged("ListGridd");
                }
            }
        }

        public List<ReceiveItem> _ListGrid { get; set; }
        public List<ReceiveItem> ListGrid
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

        public List<ReceiveItem> _ListGrid1 { get; set; }
        public List<ReceiveItem> ListGrid1
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


        public async Task<string> GetRecItemRefNo()
        {

            string uhy = "";
            try
            {
                ItemData = new List<ReceiveItem>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemRecAPI/GetRecItemRefNo").Result;
                if (response.IsSuccessStatusCode)
                {
                    //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                    uhy = await response.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            { }

            return uhy;
        }


        public async Task<ObservableCollection<ReceiveItem>> GetRcvItemList(long comp)
        {
            try
            {
                int poid = Convert.ToInt32(App.Current.Properties["RecItemPoId"]);
                ItemData = new List<ReceiveItem>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);

                HttpResponseMessage response = client.GetAsync("api/ItemRecAPI/GetRecvItem1?id=" + comp + " &poid=" + poid + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    _ListGrid_Temp.Clear();
                    data = JsonConvert.DeserializeObject<ReceiveItem[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    
                    for (int i = 0; i < data.Length; i++)
                    {
                        SelectedRecItem.SUPPLIER = data[i].SUPPLIER_NAME;
                        ReceiveAddItem.SupplierReff.Text = data[i].SUPPLIER_NAME;
                        SelectedRecItem.PAYMENT_TERMS = data[i].TERMS;
                        ReceiveAddItem.PaymentReff.Text = data[i].TERMS;
                        x++;
                        _ListGrid_Temp.Add(new ReceiveItem
                        {
                            SLNO = x,
                            PO_NUMBER = data[i].PO_NUMBER,
                            SUPPLIER_NAME = data[i].SUPPLIER_NAME,
                            TERMS = data[i].TERMS,
                            ITEM_NAME = data[i].ITEM_NAME,
                            BARCODE = data[i].BARCODE,
                            PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE,
                            MRP = data[i].MRP,
                            SEARCH_CODE = data[i].SEARCH_CODE,
                            TOTPO_QNT = data[i].TOTPO_QNT,
                            TOTAL_QTY = 1,
                            TAX_PAID = data[i].TAX_PAID,
                            SALES_PRICE = data[i].SALES_PRICE,
                            PURCHASE_UNIT = data[i].PURCHASE_UNIT,
                            SUB_TOTAL_AFTER_TAX = 0,
                            SUB_TOTAL_BEFORE_TAX = 0,

                    });
                }

                    for (int i = 0; i < _ListGrid_Temp.Count; i++)
                {
                    (_ListGrid_Temp[i].PURCHASE_PRICE_BEFORE_TAX) = ((decimal)(_ListGrid_Temp[i].PURCHASE_UNIT_PRICE * 100) / ((_ListGrid_Temp[i].TAX_PAID) + 100));
                    (_ListGrid_Temp[i].SUB_TOTAL_AFTER_TAX) = ((decimal)(_ListGrid_Temp[i].TOTAL_QTY) * (_ListGrid_Temp[i].PURCHASE_UNIT_PRICE));
                    (_ListGrid_Temp[i].SUB_TOTAL_BEFORE_TAX) = ((decimal)(_ListGrid_Temp[i].TOTAL_QTY) * (_ListGrid_Temp[i].PURCHASE_PRICE_BEFORE_TAX));
                }

                    ListGrid1 = _ListGrid_Temp;
                App.Current.Properties["EditGridData1"] = ListGrid1;
  

                }
                ListGrid1 = _ListGrid_Temp;
                TotalBottom();
                ReceiveAddItem.ListGridRef.ItemsSource = ListGrid1;
        
                return new ObservableCollection<ReceiveItem>(_ListGrid_Temp);
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<ObservableCollection<ReceiveItem>> GetRecItem(long comp)
        {
            try
            {
                ItemData = new List<ReceiveItem>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ItemRecAPI/GetAllRecItem?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ReceiveItem[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new ReceiveItem
                        {
                            SLNO = x,
                            ADDTIONAL_CHARGES = data[i].ADDTIONAL_CHARGES,
                            BARCODE = data[i].BARCODE,
                            DISCOUNT_FLAT = data[i].DISCOUNT_FLAT,
                            DISCOUNT_PERCENT = data[i].DISCOUNT_PERCENT,
                            GODOWN_ID = data[i].GODOWN_ID,
                            GRAND_TOTAL = data[i].GRAND_TOTAL,
                            IS_REC_WITH_PO = data[i].IS_REC_WITH_PO,
                            ITEM_NAME = data[i].ITEM_NAME,
                            NEWRECEIVE_ID = data[i].NEWRECEIVE_ID,
                            NOTE = data[i].NOTE,
                            PAY_IN_CASH = data[i].PAY_IN_CASH,
                            PAYMENT_TERMS = data[i].PAYMENT_TERMS,
                            PO_NUMBER = data[i].PO_NUMBER,
                            APPLY_FPR_EFFECTVE_PRICE = data[i].APPLY_FPR_EFFECTVE_PRICE,
                            RECEIVE_DATE = data[i].RECEIVE_DATE,
                            RECEIVED_ITEM_ENTRY_NO = data[i].RECEIVED_ITEM_ENTRY_NO,
                            RECEIVED_ITEM_ON_DATE = data[i].RECEIVED_ITEM_ON_DATE,
                            RECEIVING_EMPLOYEE = data[i].RECEIVING_EMPLOYEE,
                            ROUND_OFF_ADJUSTMENTAMT = data[i].ROUND_OFF_ADJUSTMENTAMT,
                            SEARCH_CODE = data[i].SEARCH_CODE,
                            SELECT_BUSINESS_LOCATION_ID = data[i].SELECT_BUSINESS_LOCATION_ID,
                            SUB_TOTAL = data[i].SUB_TOTAL,
                            SUB_TOTAL_BEFORETAX = data[i].SUB_TOTAL_BEFORETAX,
                            SUPPLIER_ID = data[i].SUPPLIER_ID,
                            SUPPLIER_INVOICE_DATE = data[i].SUPPLIER_INVOICE_DATE,
                            SUPPLIER_INVOICE_NO = data[i].SUPPLIER_INVOICE_NO,
                            TOTAL_AMT = data[i].TOTAL_AMT,
                            TOTAL_TAX_AMT = data[i].TOTAL_TAX_AMT,
                            USE_SUPPLIER_ADVANCE_AMT = data[i].USE_SUPPLIER_ADVANCE_AMT,
                            SUPPLIER = data[i].SUPPLIER,
                            SELECT_BUSINESS_LOCATION = data[i].SELECT_BUSINESS_LOCATION,
                            GODOWN = data[i].GODOWN,
                            PO_NUMBER_ID = data[i].PO_NUMBER_ID,
                            FILTER_HIERARCHY = data[i].FILTER_HIERARCHY,
                            AUTO_SAVE_IN_DRAFT = data[i].AUTO_SAVE_IN_DRAFT,
                            COMPANY_ID = data[i].COMPANY_ID,
                            DISCOUNT_BEFOR_TAX = data[i].DISCOUNT_BEFOR_TAX,
                            FOCUS_LAST_ADD_ITEM = data[i].FOCUS_LAST_ADD_ITEM,
                            ITEM_ENTRY_TEMPLATE = data[i].ITEM_ENTRY_TEMPLATE,
                            INVOICE_DIS_AMT = data[i].INVOICE_DIS_AMT,
                        });
                    }
                    if (SEARCH_REVITEM != "" && SEARCH_REVITEM != null)
                    {
                        var Revitem = (from m in _ListGrid_Temp where m.ITEM_NAME.Contains(SEARCH_REVITEM) select m).ToList();
                        _ListGrid_Temp = Revitem;
                    }

                }
                ListGrid = _ListGrid_Temp;
                // var dataw = await _ListGrid_Temp.ToList();//.ToListAsync();
                // return new ObservableCollection<ItemModel>(dataw);
                return new ObservableCollection<ReceiveItem>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private string _SEARCH_REVITEM;
        public string SEARCH_REVITEM
        {
            get
            {
                return _SEARCH_REVITEM;
            }
            set
            {
                if (_SEARCH_REVITEM != value)
                {
                    _SEARCH_REVITEM = value;

                    if (_SEARCH_REVITEM != "" && _SEARCH_REVITEM != null)
                    {

                        GetRecItem(COMPANY_ID);
                    }
                    else
                    {
                        GetRecItem(COMPANY_ID);

                    }
                    OnPropertyChanged("SEARCH_ITEM");
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
            App.Current.Properties["RecItemItemReff"] = 1;
            Window_ItemList IA = new Window_ItemList();
            IA.Show();

        }
        public ICommand _ItemListClick1;
        public ICommand ItemListClick1
        {
            get
            {
                if (_ItemListClick1 == null)
                {
                    _ItemListClick1 = new DelegateCommand(ItemList_Click1);
                }
                return _ItemListClick1;
            }
        }

        public void ItemList_Click1()
        {
            App.Current.Properties["RecItemSearchReff"] = 1;
            Window_ItemList IA = new Window_ItemList();
            IA.Show();

        }
        public ICommand _POListClick;
        public ICommand POListClick
        {
            get
            {
                if (_POListClick == null)
                {
                    _POListClick = new DelegateCommand(POList_Click);
                }
                return _POListClick;
            }
        }

        public void POList_Click()
        {
            App.Current.Properties["PoRecItem"]=1;
            Window_POList IA = new Window_POList();
            IA.Show();
            GridPOVisible = "Visible";
            GridItemVisible = "Collapsed";

        }
        public ICommand _EmpClick;
        public ICommand EmpClick
        {
            get
            {
                if (_EmpClick == null)
                {
                    _EmpClick = new DelegateCommand(Emp_Click);
                }
                return _EmpClick;
            }
        }

        public void Emp_Click()
        {
            App.Current.Properties["EMPRecItem"]=1;
            Window_Employee IA = new Window_Employee();
            IA.Show();

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
            App.Current.Properties["BussRecivedItem"]=1;
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();

        }
        public ICommand _GodownClick;
        public ICommand GodownClick
        {
            get
            {
                if (_GodownClick == null)
                {
                    _GodownClick = new DelegateCommand(Godown_Click);
                }
                return _GodownClick;
            }
        }

        public void Godown_Click()
        {
            App.Current.Properties["GodownRecItem"]=1;
            Window_GodownList IA = new Window_GodownList();
            IA.Show();

        }
        public ICommand _SupplierClick;
        public ICommand SupplierClick
        {
            get
            {
                if (_SupplierClick == null)
                {
                    _SupplierClick = new DelegateCommand(Supplier_Click);
                }
                return _SupplierClick;
            }
        }

        public void Supplier_Click()
        {
            App.Current.Properties["SupplierRecItem"] = 1;
            Window_SupplierList IA = new Window_SupplierList();
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
