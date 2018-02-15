using InvoicePOS.Helpers;
using InvoicePOS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InvoicePOS.ViewModels.Customer;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Runtime.Caching;
using System.Windows.Media;

using System.Web;
using System.Drawing;
using System.Collections;
using InvoicePOS.UserControll.Customer;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.Payment;
using InvoicePOS.UserControll.Customer;
using InvoicePOS.Views.Customer;
using InvoicePOS.UserControll.Loyalty;
using System.Data.OleDb;
using System.Data;
using InvoicePOS.Views.Main;
using System.Net;
using System.Text.RegularExpressions;
namespace InvoicePOS.ViewModels
{
    public class CustomerViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        public HttpResponseMessage response;
        // public HttpClient client = new HttpClient();
        private readonly CustomerModel OprModel;
        CustomerModel _opr = new CustomerModel();
        public ICommand select { get; set; }
        CustomerModel[] data = null;
        decimal data1 = 0;
        LoyaltyModel[] datal = null;
        CustomerGroupModel[] dataGroup = null;
        List<CustomerModel> _ListGrid_Temp = new List<CustomerModel>();
        InvoiceModel invoview = new InvoiceModel();
        public List<CustomerModel> _ListGrid { get; set; }
        public List<CustomerModel> ListGrid
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
        private string _CUSTOMER_NUMBER;
        public string CUSTOMER_NUMBER
        {
            get
            {

                return SelectedCustomer.CUSTOMER_NUMBER;
            }
            set
            {
                SelectedCustomer.CUSTOMER_NUMBER = value;
                OnPropertyChanged("CUSTOMER_NUMBER");

            }
        }

        private int _CUSTOMER_ID;
        public int CUSTOMER_ID
        {
            get
            {
                return SelectedCustomer.CUSTOMER_ID;
            }
            set
            {
                SelectedCustomer.CUSTOMER_ID = value;

                if (SelectedCustomer.CUSTOMER_ID != value)
                {
                    SelectedCustomer.CUSTOMER_ID = value;
                    OnPropertyChanged("CUSTOMER_ID");
                }
            }
        }
        public bool _IS_ACTIVESearch;
        public bool IS_ACTIVESearch
        {
            get
            {
                return _IS_ACTIVESearch;
            }
            set
            {
                _IS_ACTIVESearch = value;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                if (_IS_ACTIVESearch == true)
                {
                    GetCustomer(comp);
                }
                else
                {
                    GetCustomer(comp);
                }
                OnPropertyChanged("IS_ACTIVESearch");
            }
        }
        public bool _IS_InACTIVESearch;
        public bool IS_InACTIVESearch
        {
            get
            {
                return _IS_InACTIVESearch;
            }
            set
            {
                _IS_InACTIVESearch = value;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                if (_IS_InACTIVESearch == true)
                {
                    GetCustomer(comp);
                }
                else
                {
                    GetCustomer(comp);
                }
                OnPropertyChanged("IS_InACTIVESearch");
            }
        }

        private string _TIN;
        public string TIN
        {
            get
            {
                return SelectedCustomer.TIN;
            }
            set
            {
                SelectedCustomer.TIN = value;

                if (SelectedCustomer.TIN != value)
                {
                    SelectedCustomer.TIN = value;
                    OnPropertyChanged("TIN");
                }
            }
        }

        private string _PAN;
        public string PAN
        {
            get
            {
                return SelectedCustomer.PAN;
            }
            set
            {
                SelectedCustomer.PAN = value;

                if (SelectedCustomer.PAN != value)
                {
                    SelectedCustomer.PAN = value;
                    OnPropertyChanged("PAN");
                }
            }
        }


        private string _BUSINESS_LOCATION;
        public string BUSINESS_LOCATION
        {
            get
            {
                return SelectedCustomer.BUSINESS_LOCATION;
            }
            set
            {
                SelectedCustomer.BUSINESS_LOCATION = value;

                if (SelectedCustomer.BUSINESS_LOCATION != value)
                {
                    SelectedCustomer.BUSINESS_LOCATION = value;
                    OnPropertyChanged("BUSINESS_LOCATION");
                }
            }
        }

        private long _BUSINESS_LOCATION_ID;
        public long BUSINESS_LOCATION_ID
        {
            get
            {
                return SelectedCustomer.BUSINESS_LOCATION_ID;
            }
            set
            {
                SelectedCustomer.BUSINESS_LOCATION_ID = value;

                if (SelectedCustomer.BUSINESS_LOCATION_ID != value)
                {
                    SelectedCustomer.BUSINESS_LOCATION_ID = value;
                    OnPropertyChanged("BUSINESS_LOCATION_ID");
                }
            }
        }
        private string _NAME;
        public string NAME
        {
            get
            {
                return SelectedCustomer.NAME;
            }
            set
            {
                SelectedCustomer.NAME = value;
                DISPLAY_CUS = SelectedCustomer.NAME;
                OnPropertyChanged("NAME");

            }
        }

        private decimal _CREDIT_LIMIT_TEXT;
        public decimal CREDIT_LIMIT_TEXT
        {
            get
            {
                return SelectedCustomer.credit_Limits;
            }
            set
            {
                SelectedCustomer.credit_Limits = value;
                credit_lim = SelectedCustomer.credit_Limits;
                OnPropertyChanged("CREDIT_LIMIT_TEXT");

            }
        }
        private decimal _credit_lim;
        public decimal credit_lim
        {
            get
            {
                return SelectedCustomer.credit_Limits;
            }
            set
            {
                SelectedCustomer.credit_Limits = SelectedCustomer.credit_Limits;
                OnPropertyChanged("credit_lim");

            }
        }
        private string _DISPLAY_CUS;
        public string DISPLAY_CUS
        {
            get
            {
                return SelectedCustomer.DISPLAY_CUS;
            }
            set
            {
                SelectedCustomer.DISPLAY_CUS = SelectedCustomer.NAME;
                OnPropertyChanged("DISPLAY_CUS");

            }
        }
        //private string _DISPLAY_CUS_LAST;
        //public string DISPLAY_CUS_LAST
        //{
        //    get
        //    {
        //        return _DISPLAY_CUS_LAST;
        //    }
        //    set
        //    {
        //        _DISPLAY_CUS_LAST = value;
        //        OnPropertyChanged("DISPLAY_CUS_LAST");

        //    }
        //}


        private string _DISPLAY_CUS_LAST;
        public string DISPLAY_CUS_LAST
        {
            get
            {
                return SelectedCustomer.DISPLAY_CUS_LAST;
            }
            set
            {
                SelectedCustomer.DISPLAY_CUS_LAST = SelectedCustomer.LAST_NAME;
                OnPropertyChanged("DISPLAY_CUS_LAST");

            }
        }

        //private string _LAST_NAME;
        //public string LAST_NAME
        //{
        //    get
        //    {
        //        return _LAST_NAME;
        //    }
        //    set
        //    {
        //        _LAST_NAME = value;
        //        if (_LAST_NAME!=null&& _LAST_NAME!="")
        //        {
        //            DISPLAY_CUS_LAST = value;
        //        }
        //        OnPropertyChanged("LAST_NAME");

        //    }
        //}

        private string _LAST_NAME;
        public string LAST_NAME
        {
            get
            {
                return SelectedCustomer.LAST_NAME;
            }
            set
            {
                SelectedCustomer.LAST_NAME = value;
                DISPLAY_CUS_LAST = SelectedCustomer.LAST_NAME;
                OnPropertyChanged("LAST_NAME");

            }
        }


        private string _SEARCH_CODE;
        public string SEARCH_CODE
        {
            get
            {
                return SelectedCustomer.SEARCH_CODE;
            }
            set
            {
                SelectedCustomer.SEARCH_CODE = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.SEARCH_CODE != value)
                {
                    SelectedCustomer.SEARCH_CODE = value;
                    OnPropertyChanged("SEARCH_CODE");
                }
            }
        }
        private decimal _credit_Limits;
        public decimal credit_Limits
        {
            get
            {
                return SelectedCustomer.credit_Limits;
            }
            set
            {
                SelectedCustomer.credit_Limits = value;


                if (SelectedCustomer.credit_Limits != value)
                {
                    SelectedCustomer.credit_Limits = value;
                    OnPropertyChanged("credit_Limits");
                }
            }
        }
        private string _VAT_NUMBER;
        public string VAT_NUMBER
        {
            get
            {
                return SelectedCustomer.VAT_NUMBER;
            }
            set
            {
                SelectedCustomer.VAT_NUMBER = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.VAT_NUMBER != value)
                {
                    SelectedCustomer.VAT_NUMBER = value;
                    OnPropertyChanged("VAT_NUMBER");
                }
            }
        }
        private string _CST_NUMBER;
        public string CST_NUMBER
        {
            get
            {
                return SelectedCustomer.CST_NUMBER;
            }
            set
            {
                SelectedCustomer.CST_NUMBER = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.CST_NUMBER != value)
                {
                    SelectedCustomer.CST_NUMBER = value;
                    OnPropertyChanged("CST_NUMBER");
                }
            }
        }
        private string _BILLING_TO_NAME;
        public string BILLING_TO_NAME
        {
            get
            {
                return SelectedCustomer.BILLING_TO_NAME;
            }
            set
            {
                SelectedCustomer.BILLING_TO_NAME = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.BILLING_TO_NAME != value)
                {
                    SelectedCustomer.BILLING_TO_NAME = value;
                    OnPropertyChanged("BILLING_TO_NAME");
                }
            }
        }
        private string _BILLING_ADDRESS1;
        public string BILLING_ADDRESS1
        {
            get
            {
                return SelectedCustomer.BILLING_ADDRESS1;
            }
            set
            {
                SelectedCustomer.BILLING_ADDRESS1 = value;
                OnPropertyChanged("BILLING_ADDRESS1");
            }
        }
        private string _BILLING_ADDRESS2;
        public string BILLING_ADDRESS2
        {
            get
            {
                return SelectedCustomer.BILLING_ADDRESS2;
            }
            set
            {
                SelectedCustomer.BILLING_ADDRESS2 = value;
                OnPropertyChanged("BILLING_ADDRESS2");

            }
        }
        private string _LOYALTY_NO;
        public string LOYALTY_NO
        {
            get
            {
                return SelectedCustomer.LOYALTY_NO;
            }
            set
            {
                SelectedCustomer.LOYALTY_NO = value;
                if (SelectedCustomer.LOYALTY_NO != value)
                {
                    SelectedCustomer.LOYALTY_NO = value;
                    OnPropertyChanged("LOYALTY_NO");
                }
            }
        }
        private string _CITY;
        public string CITY
        {
            get
            {
                return SelectedCustomer.CITY;
            }
            set
            {
                SelectedCustomer.CITY = value;

                OnPropertyChanged("CITY");

            }
        }
        private string _STATE;
        public string STATE
        {
            get
            {
                return SelectedCustomer.STATE;
            }
            set
            {
                SelectedCustomer.STATE = value;
                OnPropertyChanged("STATE");

            }
        }
        private string _POSTAL_CODE;
        public string POSTAL_CODE
        {
            get
            {
                return SelectedCustomer.POSTAL_CODE;
            }
            set
            {
                SelectedCustomer.POSTAL_CODE = value;
                OnPropertyChanged("POSTAL_CODE");

            }
        }
        private string _COUNTRY;
        public string COUNTRY
        {
            get
            {
                return SelectedCustomer.COUNTRY;
            }
            set
            {
                SelectedCustomer.COUNTRY = value;


                if (SelectedCustomer.COUNTRY != value)
                {
                    SelectedCustomer.COUNTRY = value;
                    OnPropertyChanged("COUNTRY");
                }
            }
        }
        private string _MOBILE_NO;
        public string MOBILE_NO
        {
            get
            {
                return SelectedCustomer.MOBILE_NO;
            }
            set
            {
                SelectedCustomer.MOBILE_NO = value;
                OnPropertyChanged("MOBILE_NO");

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
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                _SEARCH_CUS = value;
                GetCustomer(comp);
                OnPropertyChanged("SEARCH_CUS");

            }
        }
        private string _TELEPHON_NUMBER;
        public string TELEPHON_NUMBER
        {
            get
            {
                return SelectedCustomer.TELEPHON_NUMBER;
            }
            set
            {
                SelectedCustomer.TELEPHON_NUMBER = value;
                OnPropertyChanged("TELEPHON_NUMBER");

            }
        }
        private string _EMAIL_ADDRESS;
        public string EMAIL_ADDRESS
        {
            get
            {
                return SelectedCustomer.EMAIL_ADDRESS;
            }
            set
            {
                SelectedCustomer.EMAIL_ADDRESS = value;
                OnPropertyChanged("EMAIL_ADDRESS");

            }
        }
        private string _SHIPPING_ADDRESS1;
        public string SHIPPING_ADDRESS1
        {
            get
            {
                return SelectedCustomer.SHIPPING_ADDRESS1;
            }
            set
            {
                SelectedCustomer.SHIPPING_ADDRESS1 = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.SHIPPING_ADDRESS1 != value)
                {
                    SelectedCustomer.SHIPPING_ADDRESS1 = value;
                    SelectedCustomer.BILLING_ADDRESS1 = SelectedCustomer.SHIPPING_ADDRESS1;
                    OnPropertyChanged("SHIPPING_ADDRESS1");
                }
            }
        }
        private string _SHIPPING_ADDRESS2;
        public string SHIPPING_ADDRESS2
        {
            get
            {
                return SelectedCustomer.SHIPPING_ADDRESS2;
            }
            set
            {
                SelectedCustomer.SHIPPING_ADDRESS2 = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.SHIPPING_ADDRESS2 != value)
                {
                    SelectedCustomer.SHIPPING_ADDRESS2 = value;
                    OnPropertyChanged("SHIPPING_ADDRESS2");
                }
            }
        }
        private string _SHIPPING_CITY;
        public string SHIPPING_CITY
        {
            get
            {
                return SelectedCustomer.SHIPPING_CITY;
            }
            set
            {
                SelectedCustomer.SHIPPING_CITY = value;


                if (SelectedCustomer.SHIPPING_CITY != value)
                {
                    SelectedCustomer.SHIPPING_CITY = value;
                    OnPropertyChanged("SHIPPING_CITY");
                }
            }
        }
        private string _SHIPPING_STATE;
        public string SHIPPING_STATE
        {
            get
            {
                return SelectedCustomer.SHIPPING_STATE;
            }
            set
            {
                SelectedCustomer.SHIPPING_STATE = value;


                if (SelectedCustomer.SHIPPING_STATE != value)
                {
                    SelectedCustomer.SHIPPING_STATE = value;
                    OnPropertyChanged("SHIPPING_STATE");
                }
            }
        }
        private string _SHIPPING_POSTAL_CODE;
        public string SHIPPING_POSTAL_CODE
        {
            get
            {
                return SelectedCustomer.SHIPPING_POSTAL_CODE;
            }
            set
            {
                SelectedCustomer.SHIPPING_POSTAL_CODE = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.SHIPPING_POSTAL_CODE != value)
                {
                    SelectedCustomer.SHIPPING_POSTAL_CODE = value;
                    OnPropertyChanged("SHIPPING_POSTAL_CODE");
                }
            }
        }
        private bool _DEFAULT_CREIT_LIMIT;
        public bool DEFAULT_CREIT_LIMIT
        {
            get
            {
                return SelectedCustomer.DEFAULT_CREIT_LIMIT;
            }
            set
            {
                SelectedCustomer.DEFAULT_CREIT_LIMIT = value;


                if (SelectedCustomer.DEFAULT_CREIT_LIMIT == false)
                {
                    VISIBLE_CREDIT_LIMIT = "Visible";
                    CREDIT_LIMIT = "Collapsed";
                }
                else
                {
                    VISIBLE_CREDIT_LIMIT = "Collapsed";
                    CREDIT_LIMIT = "Visible";
                }



                OnPropertyChanged("DEFAULT_CREIT_LIMIT");

            }
        }


        private string _VISIBLE_CREDIT_LIMIT;
        public string VISIBLE_CREDIT_LIMIT
        {

            get
            {
                return _VISIBLE_CREDIT_LIMIT;

            }
            set
            {
                _VISIBLE_CREDIT_LIMIT = value;

                OnPropertyChanged("VISIBLE_CREDIT_LIMIT");
            }
        }


        private string _CREDIT_LIMIT;
        public string CREDIT_LIMIT
        {

            get
            {
                return _CREDIT_LIMIT;

            }
            set
            {
                _CREDIT_LIMIT = value;

                OnPropertyChanged("CREDIT_LIMIT");
            }
        }


        private bool _IS_ACTIVE;
        public bool IS_ACTIVE
        {
            get
            {
                return SelectedCustomer.IS_ACTIVE;
            }
            set
            {
                SelectedCustomer.IS_ACTIVE = value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("Field not be blank");
                //}

                if (SelectedCustomer.IS_ACTIVE != value)
                {
                    SelectedCustomer.IS_ACTIVE = value;
                    OnPropertyChanged("IS_ACTIVE");
                }
            }
        }
        private string _CUSTOMER_GROUP;
        public string CUSTOMER_GROUP
        {
            get
            {
                return SelectedCustomer.CUSTOMER_GROUP;
            }
            set
            {
                SelectedCustomer.CUSTOMER_GROUP = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.CUSTOMER_GROUP != value)
                {
                    SelectedCustomer.CUSTOMER_GROUP = value;
                    OnPropertyChanged("CUSTOMER_GROUP");
                }
            }
        }
        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedCustomer.COMPANY_ID;
            }
            set
            {
                SelectedCustomer.COMPANY_ID = value;

                if (SelectedCustomer.COMPANY_ID != value)
                {
                    SelectedCustomer.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }
        private string _REFERRED_BY;
        public string REFERRED_BY
        {
            get
            {
                return SelectedCustomer.REFERRED_BY;
            }
            set
            {
                SelectedCustomer.REFERRED_BY = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.REFERRED_BY != value)
                {
                    SelectedCustomer.REFERRED_BY = value;
                    OnPropertyChanged("REFERRED_BY");
                }
            }
        }
        private DateTime _DATE;
        public DateTime DATE
        {
            get
            {
                return SelectedCustomer.DATE;
            }
            set
            {
                SelectedCustomer.DATE = value;

                if (SelectedCustomer.DATE != value)
                {
                    SelectedCustomer.DATE = value;
                    OnPropertyChanged("DATE");
                }
            }
        }


        private decimal _OPENING_AMT;
        public decimal OPENING_AMT
        {
            get
            {
                return SelectedCustomer.OPENING_AMT;
            }
            set
            {
                SelectedCustomer.OPENING_AMT = value;

                if (SelectedCustomer.OPENING_AMT != value)
                {
                    SelectedCustomer.OPENING_AMT = value;
                    OnPropertyChanged("OPENING_AMT");
                }
            }
        }
        private decimal _FA_BALANCE;
        public decimal FA_BALANCE
        {
            
            get
            {
                if (SelectedCustomer.BAL_TYPE_VALUE == "cr")
                {
                    //var a = ()
                }
                    return SelectedCustomer.OPENING_AMT;
            }
            set
            {
                SelectedCustomer.OPENING_AMT = value;

                if (SelectedCustomer.OPENING_AMT != value)
                {
                    SelectedCustomer.OPENING_AMT = value;
                    OnPropertyChanged("OPENING_AMT");
                }
            }
        }
       
        private string _SHIPPING_COUNTRY;
        public string SHIPPING_COUNTRY
        {
            get
            {
                return SelectedCustomer.SHIPPING_COUNTRY;
            }
            set
            {
                SelectedCustomer.SHIPPING_COUNTRY = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.SHIPPING_COUNTRY != value)
                {
                    SelectedCustomer.SHIPPING_COUNTRY = value;
                    OnPropertyChanged("SHIPPING_COUNTRY");
                }
            }
        }
        private string _SHIPPING_MOBILE_NO;
        public string SHIPPING_MOBILE_NO
        {
            get
            {
                return SelectedCustomer.SHIPPING_MOBILE_NO;
            }
            set
            {
                SelectedCustomer.SHIPPING_MOBILE_NO = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.SHIPPING_MOBILE_NO != value)
                {
                    SelectedCustomer.SHIPPING_MOBILE_NO = value;
                    OnPropertyChanged("SHIPPING_MOBILE_NO");
                }
            }
        }

        /// <summary>
        /// //COmment on 25/02/2017

        /// </summary>
        //private bool _SAMEBILLINGANDSHIPPING_ADDRESS;
        //public bool SAMEBILLINGANDSHIPPING_ADDRESS
        //{
        //    get
        //    {
        //        return SelectedCustomer.SAMEBILLINGANDSHIPPING_ADDRESS;
        //    }
        //    set
        //    {
        //        SelectedCustomer.SAMEBILLINGANDSHIPPING_ADDRESS = value;

        //        if (SelectedCustomer.SAMEBILLINGANDSHIPPING_ADDRESS == true)
        //        {
        //            SelectedCustomer.SAMEBILLINGANDSHIPPING_ADDRESS = value;
        //             ShippingAddressSameAsBillingAddress(value);
        //            if (SelectedCustomer.SAMEBILLINGANDSHIPPING_ADDRESS == true)
        //            {
        //                _BILLING_ADDRESS1 = SHIPPING_ADDRESS1;
        //            }


        //            OnPropertyChanged("SAMEBILLINGANDSHIPPING_ADDRESS");
        //        }
        //    }
        //}
        ///// <summary>
        /// //////////////////////////////
        /// </summary> Code by 25/02/2017
        /// 
        /// 
        /// 

        //private RelayCommand _SAMEBILLINGANDSHIPPING_ADDRESS1;
        //public ICommand SAMEBILLINGANDSHIPPING_ADDRESS1
        //{
        //    get
        //    {
        //        if (_SAMEBILLINGANDSHIPPING_ADDRESS1 == null)
        //        {
        //            _SAMEBILLINGANDSHIPPING_ADDRESS1 = new RelayCommand(param => this.ShippingAddressSameAsBillingAddress(param));
        //        }
        //        return _SAMEBILLINGANDSHIPPING_ADDRESS1;
        //    }
        //}


        ///

        private bool isChecked;
        private RelayCommand checkCommand;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public ICommand CheckCommand
        {
            get
            {
                if (checkCommand == null)
                    //checkCommand = new RelayCommand(i => ShippingAddressSameAsBillingAddress(i), null);
                    checkCommand = new RelayCommand(param => this.ShippingAddressSameAsBillingAddress(param));
                return checkCommand;
            }
            //set
            //{
            //    checkCommand = value;
            //    OnPropertyChanged("CheckCommand");
            //}
        }

        private long _OPENING_BALANCE_ID;
        public long OPENING_BALANCE_ID
        {
            get
            {
                return SelectedCustomer.OPENING_BALANCE_ID;
            }
            set
            {
                SelectedCustomer.OPENING_BALANCE_ID = value;

                if (SelectedCustomer.OPENING_BALANCE_ID != value)
                {
                    SelectedCustomer.OPENING_BALANCE_ID = value;
                    OnPropertyChanged("OPENING_BALANCE_ID");
                }
            }
        }
        //private long _COMPANY_ID;
        //public long COMPANY_ID
        //{
        //    get
        //    {
        //        return SelectedCustomer.COMPANY_ID;
        //    }
        //    set
        //    {
        //        SelectedCustomer.COMPANY_ID = value;

        //        if (SelectedCustomer.COMPANY_ID != value)
        //        {
        //            SelectedCustomer.COMPANY_ID = value;
        //            OnPropertyChanged("COMPANY_ID");
        //        }
        //    }
        //}
        //private string  _BUSINESS_LOCATION;
        //public string  BUSINESS_LOCATION
        //{
        //    get
        //    {
        //        return SelectedCustomer.BUSINESS_LOCATION;
        //    }
        //    set
        //    {
        //        SelectedCustomer.BUSINESS_LOCATION = value;

        //        if (SelectedCustomer.BUSINESS_LOCATION != value)
        //        {
        //            SelectedCustomer.BUSINESS_LOCATION = value;
        //            OnPropertyChanged("BUSINESS_LOCATION");
        //        }
        //    }
        //}





        private long _BAL_TYPE_ID;
        public long BAL_TYPE_ID
        {
            get
            {
                return SelectedCustomer.BAL_TYPE_ID;
            }
            set
            {
                SelectedCustomer.BAL_TYPE_ID = value;

                if (SelectedCustomer.BAL_TYPE_ID != value)
                {
                    SelectedCustomer.BAL_TYPE_ID = value;
                    OnPropertyChanged("BAL_TYPE_ID");
                }
            }
        }

        private decimal _CURRENT_OPENING_BALANCE;
        public decimal CURRENT_OPENING_BALANCE
        {
            get
            {
                return SelectedCustomer.CURRENT_OPENING_BALANCE;
            }
            set
            {
                SelectedCustomer.CURRENT_OPENING_BALANCE = value;

                if (SelectedCustomer.CURRENT_OPENING_BALANCE != value)
                {
                    SelectedCustomer.CURRENT_OPENING_BALANCE = value;
                    OnPropertyChanged("CURRENT_OPENING_BALANCE");
                }
            }
        }
        private string _BAL_TYPE_VALUE;
        public string BAL_TYPE_VALUE
        {
            get
            {
                return SelectedCustomer.BAL_TYPE_VALUE;
            }
            set
            {
                SelectedCustomer.BAL_TYPE_VALUE = value;

                if (SelectedCustomer.BAL_TYPE_VALUE != value)
                {
                    SelectedCustomer.BAL_TYPE_VALUE = value;
                    OnPropertyChanged("BAL_TYPE_VALUE");
                }
            }
        }




        private decimal _CLOSING_AMT;
        public decimal CLOSING_AMT
        {
            get
            {
                return SelectedCustomer.CLOSING_AMT;
            }
            set
            {
                SelectedCustomer.CLOSING_AMT = value;

                if (SelectedCustomer.CLOSING_AMT != value)
                {
                    SelectedCustomer.CLOSING_AMT = value;
                    OnPropertyChanged("CLOSING_AMT");
                }
            }
        }

        private DateTime _SYSTEM_DATE;
        public DateTime SYSTEM_DATE
        {
            get
            {
                return SelectedCustomer.SYSTEM_DATE;
            }
            set
            {
                SelectedCustomer.SYSTEM_DATE = value;

                if (SelectedCustomer.SYSTEM_DATE != value)
                {
                    SelectedCustomer.SYSTEM_DATE = value;
                    OnPropertyChanged("SYSTEM_DATE");
                }
            }
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        /// 

        public void ShippingAddressSameAsBillingAddress(object sender)
        {
            CheckBox chk = sender as CheckBox;
            if (chk.IsChecked == true)
            {
                SelectedCustomer.BILLING_ADDRESS1 = SHIPPING_ADDRESS1;
                BILLING_ADDRESS2 = SHIPPING_ADDRESS2;
                CITY = SHIPPING_CITY;
                STATE = SHIPPING_STATE;
                POSTAL_CODE = SHIPPING_POSTAL_CODE;
                //  TELEPHON_NUMBER = SHIPPING_TELEPHON_NUMBER;
                MOBILE_NO = SHIPPING_MOBILE_NO;
                // EMAIL_ADDRESS = SHIPPING_EMAIL_ADDRESS;
                DependencyObject currParent = VisualTreeHelper.GetParent(chk);
                while (!(currParent is Window))
                {
                    //pageFrame = currParent as Frame;
                    currParent = VisualTreeHelper.GetParent(currParent);
                }
                (currParent as AddCustomer).BILLING_ADDRESS1.Text = SHIPPING_ADDRESS1;
                (currParent as AddCustomer).BILLING_ADDRESS2.Text = SHIPPING_ADDRESS2;
                (currParent as AddCustomer).CITY.Text = SHIPPING_CITY;
                (currParent as AddCustomer).STATE.Text = SHIPPING_STATE;
                (currParent as AddCustomer).POSTAL_CODE.Text = SHIPPING_POSTAL_CODE;
                (currParent as AddCustomer).TELEPHON_NUMBER.Text = SHIPPING_TELEPHON_NUMBER;
                (currParent as AddCustomer).MOBILE_NO.Text = SHIPPING_MOBILE_NO;
                (currParent as AddCustomer).EMAIL_ADDRESS.Text = SHIPPING_EMAIL_ADDRESS;
            }
            else
            {
                BILLING_ADDRESS1 = "";
                BILLING_ADDRESS2 = "";
                CITY = "";
                STATE = "";
                POSTAL_CODE = "";
                TELEPHON_NUMBER = "";
                MOBILE_NO = "";
                EMAIL_ADDRESS = "";



            }

            //CustomerAdd.txtBillAddressRef1 = (TextBox)SelectedCustomer.BLLING_ADDRESS1.ToString();
            //CustomerAdd.txtBillAddressRef2.Text = SHIPPING_ADDRESS2;
            //CustomerAdd.txtBillCityRef.Text = SHIPPING_CITY;
            //CustomerAdd.txtBillStateRef.Text = SHIPPING_STATE;
            //CustomerAdd.txtBillZipRef.Text = SHIPPING_POSTAL_CODE;
            //CustomerAdd.txtBillTeleRef.Text = SHIPPING_TELEPHON_NUMBER;
            //CustomerAdd.txtBillMobileRef.Text = SHIPPING_MOBILE_NO;
            //CustomerAdd.txtBillEmailRef.Text = SHIPPING_EMAIL_ADDRESS;

        }

        //public CustomerModel ShippingAddressSameAsBillingAddress(bool flag)
        //{

        //    if (flag == true)
        //    {
        //        SelectedCustomer.BILLING_ADDRESS1 = SHIPPING_ADDRESS1;
        //        BILLING_ADDRESS2 = SHIPPING_ADDRESS2;
        //        CITY = SHIPPING_CITY;
        //        STATE = SHIPPING_STATE;
        //        POSTAL_CODE = SHIPPING_POSTAL_CODE;
        //        TELEPHON_NUMBER = SHIPPING_TELEPHON_NUMBER;
        //        MOBILE_NO = SHIPPING_MOBILE_NO;
        //        EMAIL_ADDRESS = SHIPPING_EMAIL_ADDRESS;
        //        DependencyObject currParent = VisualTreeHelper.GetParent(this);
        //        while (currParent != null )
        //        {
        //            //pageFrame = currParent as Frame;
        //            currParent = VisualTreeHelper.GetParent(currParent);
        //        }
        //        //CustomerAdd.txtBillAddressRef1 = (TextBox)SelectedCustomer.BILLING_ADDRESS1.ToString();
        //        //CustomerAdd.txtBillAddressRef2.Text = SHIPPING_ADDRESS2;
        //        //CustomerAdd.txtBillCityRef.Text = SHIPPING_CITY;
        //        //CustomerAdd.txtBillStateRef.Text = SHIPPING_STATE;
        //        //CustomerAdd.txtBillZipRef.Text = SHIPPING_POSTAL_CODE;
        //        //CustomerAdd.txtBillTeleRef.Text = SHIPPING_TELEPHON_NUMBER;
        //        //CustomerAdd.txtBillMobileRef.Text = SHIPPING_MOBILE_NO;
        //        //CustomerAdd.txtBillEmailRef.Text = SHIPPING_EMAIL_ADDRESS;
        //    }
        //    else
        //    {
        //        SelectedCustomer.BILLING_ADDRESS1 = String.Empty;
        //        SelectedCustomer.BILLING_ADDRESS2 = String.Empty;
        //        SelectedCustomer.CITY = String.Empty;
        //        SelectedCustomer.STATE = String.Empty;
        //        SelectedCustomer.POSTAL_CODE = String.Empty;
        //        SelectedCustomer.TELEPHON_NUMBER = String.Empty;
        //        SelectedCustomer.MOBILE_NO = String.Empty;
        //        SelectedCustomer.EMAIL_ADDRESS = string.Empty;
        //    }


        //    OnPropertyChanged("BILLING_ADDRESS1");
        //    OnPropertyChanged("BILLING_ADDRESS2");
        //    OnPropertyChanged("CITY");
        //    OnPropertyChanged("STATE");
        //    OnPropertyChanged("POSTAL_CODE");
        //    OnPropertyChanged("TELEPHON_NUMBER");
        //    OnPropertyChanged("MOBILE_NO");
        //    OnPropertyChanged("EMAIL_ADDRESS");
        //    return SelectedCustomer;
        //}



        /// <summary>
        /// /
        /// </summary>


        private bool _IS_ENROLLED_FOR_LOYALTY;
        public bool IS_ENROLLED_FOR_LOYALITY
        {
            get
            {
                return SelectedCustomer.IS_ENROLLED_FOR_LOYALITY;
            }
            set
            {
                SelectedCustomer.IS_ENROLLED_FOR_LOYALITY = value;

                if (SelectedCustomer.IS_ENROLLED_FOR_LOYALITY == false)
                {
                    MYDEFAULT = false;
                    AUTOGENERATE = false;
                    ENABLE_LOYALTY = false;
                    AUTOVISIBLE = "Visible";
                    DEFAULTVISIBLE = "Collapsed";
                    LOYALTY_DATE_VISIBLE = "Hidden";
                }
                else
                {
                    MYDEFAULT = true;
                    AUTOGENERATE = false;
                    ENABLE_LOYALTY = false;
                    AUTOVISIBLE = "Collapsed";
                    DEFAULTVISIBLE = "Visible";
                    LOYALTY_DATE_VISIBLE = "Visible";
                }

                OnPropertyChanged("IS_ENROLLED_FOR_LOYALTY");
            }
        }






        private string _AUTOVISIBLE;
        public string AUTOVISIBLE
        {

            get
            {
                return _AUTOVISIBLE;
            }
            set
            {

                _AUTOVISIBLE = value;
                OnPropertyChanged("AUTOVISIBLE");
            }
        }


        private string _DEFAULTVISIBLE;
        public string DEFAULTVISIBLE
        {

            get
            {
                return _DEFAULTVISIBLE;
            }
            set
            {

                _DEFAULTVISIBLE = value;
                OnPropertyChanged("DEFAULTVISIBLE");
            }
        }







        private bool _ENABLE_LOYALTY;
        public bool ENABLE_LOYALTY
        {

            get
            {
                return _ENABLE_LOYALTY;
            }
            set
            {

                _ENABLE_LOYALTY = value;
                OnPropertyChanged("ENABLE_LOYALTY");
            }
        }





        private bool _MYDEFAULT;
        public bool MYDEFAULT
        {

            get
            {
                return _MYDEFAULT;
            }
            set
            {

                _MYDEFAULT = value;
                OnPropertyChanged("MYDEFAULT");
            }
        }


        private bool _AUTOGENERATE;
        public bool AUTOGENERATE
        {

            get
            {
                return _AUTOGENERATE;
            }
            set
            {

                _AUTOGENERATE = value;
                OnPropertyChanged("AUTOGENERATE");
            }
        }




        private string _SHIPPING_TELEPHON_NUMBER;
        public string SHIPPING_TELEPHON_NUMBER
        {
            get
            {
                return SelectedCustomer.SHIPPING_TELEPHON_NUMBER;
            }
            set
            {
                SelectedCustomer.SHIPPING_TELEPHON_NUMBER = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.SHIPPING_TELEPHON_NUMBER != value)
                {
                    SelectedCustomer.SHIPPING_TELEPHON_NUMBER = value;
                    OnPropertyChanged("SHIPPING_TELEPHON_NUMBER");
                }
            }
        }
        private string _SHIPPING_EMAIL_ADDRESS;
        public string SHIPPING_EMAIL_ADDRESS
        {
            get
            {
                return SelectedCustomer.SHIPPING_EMAIL_ADDRESS;
            }
            set
            {
                SelectedCustomer.SHIPPING_EMAIL_ADDRESS = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCustomer.SHIPPING_EMAIL_ADDRESS != value)
                {
                    SelectedCustomer.SHIPPING_EMAIL_ADDRESS = value;
                    OnPropertyChanged("SHIPPING_EMAIL_ADDRESS");
                }
            }
        }
        private string _IMAGE_PATH;
        public string IMAGE_PATH
        {
            get
            {
                return SelectedCustomer.IMAGE_PATH;
            }
            set
            {
                SelectedCustomer.IMAGE_PATH = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }
                if (SelectedCustomer.IMAGE_PATH != value)
                {
                    SelectedCustomer.IMAGE_PATH = value;
                    OnPropertyChanged("IMAGE_PATH");
                }
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
        public ICommand _CustomerOpeningBal;
        public ICommand CustomerOpeningBal
        {
            get
            {
                if (_CustomerOpeningBal == null)
                {
                    _CustomerOpeningBal = new DelegateCommand(CustomerOpeningBal_Click);
                }
                return _CustomerOpeningBal;
            }
        }
        public void CustomerOpeningBal_Click()
        {
            CurrentOpeningBalanceDetails sh = new UserControll.Customer.CurrentOpeningBalanceDetails();
            InvoicePOS.UserControll.Customer.CurrentOpeningBalanceDetails.BussinessLocationName.Text = BUSINESS_LOCATION_ID.ToString();
            InvoicePOS.UserControll.Customer.CurrentOpeningBalanceDetails.Date.Text = DATE.ToString();
            sh.Show();
            
        }
        public ICommand _CustomerList;
        public ICommand CustomerList
        {
            get
            {
                if (_CustomerList == null)
                {
                    _CustomerList = new DelegateCommand(CustomerList_Click);
                }
                return _CustomerList;
            }
        }
        public  void CustomerList_Click()
        {
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            GetCustomer(comp);
        }
        public ICommand _AddCustomerClick;
        public ICommand AddCustomerClick
        {
            get
            {
                if (_AddCustomerClick == null)
                {
                    _AddCustomerClick = new DelegateCommand(AddCustomer_Click);
                }
                return _AddCustomerClick;
            }
        }
        public void AddCustomer_Click()
        {
            App.Current.Properties["GenerateCode"] = 1;
            AddCustomer _AC = new AddCustomer();
            _AC.ShowDialog();

            // ModalService.NavigateTo(new AddCustomer(), delegate(bool returnValue) { });
        }
        public ICommand _AdvancedSearchClick;
        public ICommand AdvancedSearchClick
        {
            get
            {
                if (_AdvancedSearchClick == null)
                {
                    _AdvancedSearchClick = new DelegateCommand(AdvancedSearch_Click);
                }
                return _AdvancedSearchClick;
            }
        }
        public void AdvancedSearch_Click()
        {
            AdvancedSearch _AC = new AdvancedSearch();
            _AC.ShowDialog();

            // ModalService.NavigateTo(new AddCustomer(), delegate(bool returnValue) { });
        }

        public ICommand _CustomerGroupList;
        public ICommand CustomerGroupList
        {
            get
            {
                if (_CustomerGroupList == null)
                {
                    _CustomerGroupList = new DelegateCommand(Customer_GroupList);
                }
                return _CustomerGroupList;
            }
        }
        public void Customer_GroupList()
        {
            App.Current.Properties["CustomerGroup"] = 1;
            Customergrouplist _AC = new Customergrouplist();
            _AC.ShowDialog();
            // ModalService.NavigateTo(new AddCustomer(), delegate(bool returnValue) { });
        }
        public ICommand _BusinessList;
        public ICommand BusinessList
        {
            get
            {
                if (_BusinessList == null)
                {
                    _BusinessList = new DelegateCommand(BusinessLocationClick_Click);
                }
                return _BusinessList;
            }
        }

        public void BusinessLocationClick_Click()
        {
            //App.Current.Properties["BussCashReg"] = 2;
            App.Current.Properties["SelectData"] = SelectedCustomer;
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();            
           
        }

        public ICommand _ViewCustomerClick;
        public ICommand ViewCustomerClick
        {
            get
            {
                if (_ViewCustomerClick == null)
                {
                    _ViewCustomerClick = new DelegateCommand(ViewCustomer_Click);
                }
                return _ViewCustomerClick;
            }
        }
        public void ViewCustomer_Click()
        {
            ViewCustomer _AC = new ViewCustomer();
            _AC.ShowDialog();
        }
        //public ICommand _SearchCustomer { get; set; }
        //public ICommand SearchCustomer
        //{
        //    get
        //    {
        //        if (_SearchCustomer == null)
        //        {
        //            _SearchCustomer = new DelegateCommand(Search_Customer);
        //        }
        //        return _SearchCustomer;
        //    }
        //}

        //public async void Search_Customer()
        //{
        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(GlobalData.gblApiAdress);
        //        client.DefaultRequestHeaders.Accept.Add(
        //            new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.Timeout = new TimeSpan(500000000000);
        //        response = await client.GetAsync("api/CustomerAPI/SearchCustomer?name=" + SelectedCustomer.SEARCH_CUS + "");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            data = JsonConvert.DeserializeObject<CustomerModel[]>(await response.Content.ReadAsStringAsync());
        //            CustomerData = new List<CustomerModel>();
        //            for (int i = 0; i < data.Length; i++)
        //            {
        //                _ListGrid_Temp.Add(new CustomerModel
        //                {
        //                    NAME = data[i].NAME,
        //                    COMPANY_ID = data[i].COMPANY_ID,
        //                    LAST_NAME = data[i].LAST_NAME,
        //                    SEARCH_CODE = data[i].SEARCH_CODE,
        //                    VAT_NUMBER = data[i].VAT_NUMBER,
        //                    CST_NUMBER = data[i].CST_NUMBER,
        //                    LOYALTY_NO = data[i].LOYALTY_NO,
        //                    CUSTOMER_ID = data[i].CUSTOMER_ID,
        //                    BILLING_ADDRESS1 = data[i].BILLING_ADDRESS1,
        //                    BILLING_ADDRESS2 = data[i].BILLING_ADDRESS2,
        //                    BILLING_TO_NAME = data[i].BILLING_TO_NAME,
        //                    IS_ENROLLED_FOR_LOYALITY = data[i].IS_ENROLLED_FOR_LOYALITY,
        //                    CITY = data[i].CITY,
        //                    COUNTRY = data[i].COUNTRY,
        //                    CUSTOMER_GROUP = data[i].CUSTOMER_GROUP,
        //                    CUSTOMER_NUMBER = data[i].CUSTOMER_NUMBER,
        //                    DEFAULT_CREIT_LIMIT = data[i].DEFAULT_CREIT_LIMIT,
        //                    EMAIL_ADDRESS = data[i].EMAIL_ADDRESS,
        //                    IS_ACTIVE = data[i].IS_ACTIVE,
        //                    MOBILE_NO = data[i].MOBILE_NO,
        //                    NOTES = data[i].NAME,
        //                    TIN = data[i].TIN,
        //                    PAN = data[i].PAN,
        //                    BUSINESS_LOCATION = data[i].BUSINESS_LOCATION,
        //                    BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID,



        //                    OPENING_AMT = data[i].OPENING_AMT,
        //                    CURRENT_OPENING_BALANCE = data[i].CURRENT_OPENING_BALANCE,
        //                    POSTAL_CODE = data[i].POSTAL_CODE,
        //                    REFERRED_BY = data[i].REFERRED_BY,
        //                    IMAGE_PATH = data[i].IMAGE_PATH,
        //                    //   SAMEBILLINGANDSHIPPING_ADDRESS = data[i].SAMEBILLINGANDSHIPPING_ADDRESS,
        //                    STATE = data[i].STATE,
        //                    TELEPHON_NUMBER = data[i].TELEPHON_NUMBER,
        //                    SHIPPING_ADDRESS1 = data[i].SHIPPING_ADDRESS1,
        //                    SHIPPING_ADDRESS2 = data[i].SHIPPING_ADDRESS2,
        //                    SHIPPING_CITY = data[i].SHIPPING_CITY,
        //                    SHIPPING_COUNTRY = data[i].SHIPPING_COUNTRY,
        //                    SHIPPING_EMAIL_ADDRESS = data[i].SHIPPING_EMAIL_ADDRESS,
        //                    SHIPPING_MOBILE_NO = data[i].SHIPPING_MOBILE_NO,
        //                    SHIPPING_TELEPHON_NUMBER = data[i].SHIPPING_TELEPHON_NUMBER,
        //                    SHIPPING_POSTAL_CODE = data[i].SHIPPING_POSTAL_CODE,
        //                    SHIPPING_STATE = data[i].SHIPPING_STATE,
        //                });
        //            }
        //        }
        //        ListGrid = _ListGrid_Temp;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        public ICommand _ViewCustomer { get; set; }
        public ICommand ViewCustomer
        {
            get
            {
                if (_ViewCustomer == null)
                {
                    _ViewCustomer = new DelegateCommand(View_Customer);
                }
                return _ViewCustomer;
            }
        }

        public async void View_Customer()
        {
            if (SelectedCustomer.CUSTOMER_ID != null && SelectedCustomer.CUSTOMER_ID != 0)
            {
                App.Current.Properties["Action"] = "View";
                // data = new List<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CustomerAPI/EditCustomer?Custid=" + SelectedCustomer.CUSTOMER_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CustomerModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {

                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedCustomer.IMAGE_PATH = data[i].IMAGE_PATH;
                            SelectedCustomer.CUSTOMER_NUMBER = data[i].CUSTOMER_NUMBER;
                            SelectedCustomer.CUSTOMER_ID = data[i].CUSTOMER_ID;
                            SelectedCustomer.CURRENT_OPENING_BALANCE = data[i].CURRENT_OPENING_BALANCE;
                            //  SelectedCustomer.SAMEBILLINGANDSHIPPING_ADDRESS = data[i].SAMEBILLINGANDSHIPPING_ADDRESS;
                            SelectedCustomer.CUSTOMER_GROUP = data[i].CUSTOMER_GROUP;
                            SelectedCustomer.CST_NUMBER = data[i].CST_NUMBER;
                            //  SelectedCustomer.CUSTOMER_NUMBER = data[i].CUSTOMER_NUMBER;
                            SelectedCustomer.IS_ENROLLED_FOR_LOYALITY = data[i].IS_ENROLLED_FOR_LOYALITY;
                            SelectedCustomer.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedCustomer.REFERRED_BY = data[i].REFERRED_BY;
                            SelectedCustomer.POSTAL_CODE = data[i].POSTAL_CODE;
                            SelectedCustomer.NAME = data[i].NAME;
                            SelectedCustomer.MOBILE_NO = data[i].MOBILE_NO;
                            SelectedCustomer.LOYALTY_NO = data[i].LOYALTY_NO;
                            SelectedCustomer.LAST_NAME = data[i].LAST_NAME;
                            SelectedCustomer.IMAGE_PATH = data[i].IMAGE_PATH;
                            SelectedCustomer.OPENING_AMT = data[i].OPENING_AMT;
                            SelectedCustomer.IS_ACTIVE = data[i].IS_ACTIVE;
                            SelectedCustomer.VAT_NUMBER = data[i].VAT_NUMBER;
                            SelectedCustomer.TELEPHON_NUMBER = data[i].TELEPHON_NUMBER;
                            SelectedCustomer.STATE = data[i].STATE;
                            SelectedCustomer.TIN = data[i].TIN;
                            SelectedCustomer.PAN = data[i].PAN;
                            SelectedCustomer.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedCustomer.BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID;
                            SelectedCustomer.SHIPPING_TELEPHON_NUMBER = data[i].SHIPPING_TELEPHON_NUMBER;
                            SelectedCustomer.SHIPPING_STATE = data[i].SHIPPING_STATE;
                            SelectedCustomer.DISPLAY_CUS = data[i].NAME;
                            SelectedCustomer.SHIPPING_POSTAL_CODE = data[i].SHIPPING_POSTAL_CODE;
                            SelectedCustomer.SHIPPING_MOBILE_NO = data[i].SHIPPING_MOBILE_NO;
                            SelectedCustomer.SHIPPING_EMAIL_ADDRESS = data[i].SHIPPING_EMAIL_ADDRESS;
                            SelectedCustomer.SHIPPING_CITY = data[i].SHIPPING_CITY;
                            SelectedCustomer.SHIPPING_ADDRESS2 = data[i].SHIPPING_ADDRESS2;
                            SelectedCustomer.SHIPPING_ADDRESS1 = data[i].SHIPPING_ADDRESS1;
                            SelectedCustomer.BAL_TYPE_VALUE = data[i].BAL_TYPE_VALUE;
                        }
                        App.Current.Properties["ViewCustomer"] = SelectedCustomer;
                        ViewCustomer_Click();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Customer first", "Customer Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }



        public ICommand _CustomerCode;
        public ICommand CustomerCode
        {
            get
            {
                if (_CustomerCode == null)
                {
                    _CustomerCode = new DelegateCommand(CustomerCode_Buttom);
                }
                return _CustomerCode;
            }
        }



        public void CustomerCode_Buttom()
        {
            CUSTNUM_ENABLE = true;
            ReadVisible = "Collapsed";
            RiteVisible = "Visible";
            CUSTOMER_NUMBER = "";
            //GetCustomerNo();
        }



        public ICommand _CustomerCode1;
        public ICommand CustomerCode1
        {
            get
            {
                if (_CustomerCode1 == null)
                {
                    _CustomerCode1 = new DelegateCommand(Customer1_Buttom);
                }
                return _CustomerCode1;
            }
        }



        public void Customer1_Buttom()
        {
            CUSTNUM_ENABLE = false;
            ReadVisible = "Visible";
            RiteVisible = "Collapsed";
            GetCustomerNo();
        }














        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_Customer);
                }
                return _Cancel;
            }
        }



        public void Cancel_Customer()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }





        public ICommand _RecvPaymentClick;
        public ICommand RecvPaymentClick
        {
            get
            {
                if (_RecvPaymentClick == null)
                {
                    _RecvPaymentClick = new DelegateCommand(RecvPayment_Click);
                }
                return _RecvPaymentClick;
            }
        }
        public void RecvPayment_Click()
        {

            // ModalService.NavigateTo(new AddCustomer(), delegate(bool returnValue) { });
        }
        public ICommand _CustFAAcctClick;
        public ICommand CustFAAcctClick
        {
            get
            {
                if (_CustFAAcctClick == null)
                {
                    _CustFAAcctClick = new DelegateCommand(CustFAAcct_Click);
                }
                return _CustFAAcctClick;
            }
        }
        public void CustFAAcct_Click()
        {

            // ModalService.NavigateTo(new AddCustomer(), delegate(bool returnValue) { });
        }



        public ICommand _DeleteCustomer;
        public ICommand DeleteCustomer
        {
            get
            {
                if (_DeleteCustomer == null)
                {
                    _DeleteCustomer = new DelegateCommand(Delete_Customer);
                }
                return _DeleteCustomer;
            }
        }


        public async void Delete_Customer()
        {
            if (SelectedCustomer.COMPANY_ID != null && SelectedCustomer.CUSTOMER_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure delete this customer " + SelectedCustomer.CUSTOMER_NUMBER + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var id = SelectedCustomer.CUSTOMER_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/CustomerAPI/DeleteCustomer?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        ModalService.NavigateTo(new CustomerList(), delegate (bool returnValue) { });
                        MessageBox.Show("Customer Deleted Successfully");
                    }
                }
                else
                {
                    Cancel_Customer();
                }
            }
            else
            {
                MessageBox.Show("Select Customer");
            }

        }
        public ICommand _EditCustomer { get; set; }
        public ICommand EditCustomer
        {
            get
            {
                if (_EditCustomer == null)
                {
                    _EditCustomer = new DelegateCommand(Edit_Customer);
                }
                return _EditCustomer;
            }
        }

        public async void Edit_Customer()
        {
            string ftpServerIP = "115.115.196.30";
            string ftpUserID = "suvendu";
            string ftpPassword = "Passw0rd";


            //string ftpServerIP = "54.70.197.231";
            //string ftpUserID = "suvendu";
            //string ftpPassword = "vpY9dNp3W9AqhjGy";

            string filename = "ftp://" + ftpServerIP + "//Upload//";
            if (SelectedCustomer.CUSTOMER_ID != null && SelectedCustomer.CUSTOMER_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                // data = new List<ItemModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CustomerAPI/EditCustomer?Custid=" + SelectedCustomer.CUSTOMER_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CustomerModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {


                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedCustomer.IMAGE_PATH = data[i].IMAGE_PATH;
                            SelectedCustomer.CUSTOMER_NUMBER = data[i].CUSTOMER_NUMBER;
                            SelectedCustomer.CUSTOMER_ID = data[i].CUSTOMER_ID;
                            //   SelectedCustomer.SAMEBILLINGANDSHIPPING_ADDRESS = data[i].SAMEBILLINGANDSHIPPING_ADDRESS;
                            SelectedCustomer.CUSTOMER_GROUP = data[i].CUSTOMER_GROUP;
                            SelectedCustomer.CST_NUMBER = data[i].CST_NUMBER;
                            //  SelectedCustomer.CUSTOMER_NUMBER = data[i].CUSTOMER_NUMBER;
                            SelectedCustomer.IS_ENROLLED_FOR_LOYALITY = data[i].IS_ENROLLED_FOR_LOYALITY;
                            SelectedCustomer.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedCustomer.REFERRED_BY = data[i].REFERRED_BY;
                            SelectedCustomer.POSTAL_CODE = data[i].POSTAL_CODE;
                            SelectedCustomer.NAME = data[i].NAME;
                            SelectedCustomer.MOBILE_NO = data[i].MOBILE_NO;
                            SelectedCustomer.LOYALTY_NO = data[i].LOYALTY_NO;
                            SelectedCustomer.LAST_NAME = data[i].LAST_NAME;
                            SelectedCustomer.IMAGE_PATH = data[i].IMAGE_PATH;
                            SelectedCustomer.TIN = data[i].TIN;
                            SelectedCustomer.PAN = data[i].PAN;
                            SelectedCustomer.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedCustomer.BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID;
                            SelectedCustomer.OPENING_AMT = data[i].OPENING_AMT;
                            SelectedCustomer.IS_ACTIVE = data[i].IS_ACTIVE;
                            SelectedCustomer.VAT_NUMBER = data[i].VAT_NUMBER;
                            SelectedCustomer.TELEPHON_NUMBER = data[i].TELEPHON_NUMBER;
                            SelectedCustomer.STATE = data[i].STATE;
                            SelectedCustomer.SHIPPING_TELEPHON_NUMBER = data[i].SHIPPING_TELEPHON_NUMBER;
                            SelectedCustomer.SHIPPING_STATE = data[i].SHIPPING_STATE;
                            SelectedCustomer.DISPLAY_CUS = data[i].NAME;
                            SelectedCustomer.SHIPPING_POSTAL_CODE = data[i].SHIPPING_POSTAL_CODE;
                            SelectedCustomer.SHIPPING_MOBILE_NO = data[i].SHIPPING_MOBILE_NO;
                            SelectedCustomer.SHIPPING_EMAIL_ADDRESS = data[i].SHIPPING_EMAIL_ADDRESS;
                            SelectedCustomer.SHIPPING_CITY = data[i].SHIPPING_CITY;
                            SelectedCustomer.SHIPPING_ADDRESS2 = data[i].SHIPPING_ADDRESS2;
                            SelectedCustomer.SHIPPING_ADDRESS1 = data[i].SHIPPING_ADDRESS1;
                            SelectedCustomer.BAL_TYPE_VALUE = data[i].BAL_TYPE_VALUE;
                            SelectedCustomer.FULL_NAME = "" + data[i].NAME + " " + data[i].LAST_NAME + "";




                            var fr = filename + data[i].IMAGE_PATH;

                            var imageFile = new System.IO.FileInfo(data[i].IMAGE_PATH);
                            string file = imageFile.Name;
                            var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                            // get your 'Uploaded' folder
                            var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));
                            if (!dir.Exists)
                                dir.Create();
                            // Copy file to your folder
                            //imageFile.CopyTo(System.IO.Path.Combine(dir.FullName, file), true);
                            string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);

                            FtpDown(path1, file);

                            IMAGE_PATH1 = new BitmapImage(new Uri(path1));
                            App.Current.Properties["ItemViewImg"] = IMAGE_PATH1;



                            SelectedCustomer.IMAGE_PATH = file;
                        }

                        App.Current.Properties["EditCustomer"] = SelectedCustomer;
                        GetDuePay(SelectedCustomer.CUSTOMER_ID);
                        AddCustomer AS = new AddCustomer();

                        AS.ShowDialog();
                        //ModalService.NavigateTo(new AddCustomer(), delegate(bool returnValue) { });
                    }



                }

            }
            else
            {

                MessageBox.Show("Select Customer first", "Customer Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
        public async void GetDuePay(int custid)
        {
            try
            {
                decimal getflatpoints = 0;
                int suppid = Convert.ToInt32(App.Current.Properties["SupplierId"]);
                //  BusinessLocation = Convert.ToString(App.Current.Properties["BussLocName"]);
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetTotal?id=" + custid + "").Result;
                HttpResponseMessage response1 = client.GetAsync("api/LoyaltyAPI/GetLoyaltyforflatpoints/").Result;
                //HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetInvoiceItem?id=" + SelectInv.INVOICE_ID + "").Result;
                {

                    data1 = JsonConvert.DeserializeObject<decimal>(await response.Content.ReadAsStringAsync());
                    data1 = Convert.ToDecimal(data1.ToString("0000000000.00"));
                    datal = JsonConvert.DeserializeObject<LoyaltyModel[]>(await response1.Content.ReadAsStringAsync());
                    decimal flatpoint = 0;
                    if (data1 != 0)
                    {

                        for (int i = 0; i < datal.Length; i++)
                        {
                            try
                            {
                                if (datal[i].InvoiceExceeding1 >= data1)
                                {
                                    if (datal[i + 1].InvoiceExceeding1 > data1)
                                    {
                                        getflatpoints = Convert.ToDecimal(datal[i].FlatPoints1);
                                        break;
                                    }
                                    else
                                    {
                                        getflatpoints = Convert.ToDecimal(datal[i].FlatPoints1);
                                    }
                                }
                            }
                            catch
                            {

                            }
                        }
                        App.Current.Properties["getTotalPending"] = getflatpoints;
                        AddCustomer.showloyality.Text = getflatpoints.ToString();
                    }

                    //if (data1.l > 0)
                    //{

                    //}

                }
            }
            catch (Exception ex)
            {

            }

        }

        //public ICommand _UpdateCustomer;
        //public ICommand UpdateCustomer
        //{
        //    get
        //    {
        //        if (_UpdateCustomer == null)
        //        {
        //            _UpdateCustomer = new DelegateCommand(Update_Customer);
        //        }
        //        return _UpdateCustomer;
        //    }
        //}


        //public async void Update_Customer()
        //{
        //    App.Current.Properties["Action"] = "Edit";
        //    SelectedCustomer = new CustomerModel { NAME = NAME };
        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
        //    var response = await client.PostAsJsonAsync("api/CustomerAPI/CreateCustomer/", SelectedCustomer);
        //    if (response.StatusCode.ToString() == "OK")
        //    {
        //        ModalService.NavigateTo(new CustomerList(), delegate(bool returnValue) { });
        //    }

        //}


        private string _CreateVisible { get; set; }
        public string CreateVisible
        {

            get { return _CreateVisible; }
            set
            {
                if (value != _CreateVisible)
                {
                    _CreateVisible = value;
                    OnPropertyChanged("CreateVisible");
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


        private string _ReadVisible { get; set; }
        public string ReadVisible
        {

            get { return _ReadVisible; }
            set
            {
                if (value != _ReadVisible)
                {
                    _ReadVisible = value;
                    OnPropertyChanged("ReadVisible");
                }
            }
        }

        private string _RiteVisible { get; set; }
        public string RiteVisible
        {

            get { return _RiteVisible; }
            set
            {
                if (value != _RiteVisible)
                {
                    _RiteVisible = value;
                    OnPropertyChanged("RiteVisible");
                }
            }
        }
        public ICommand _InsertCustomer { get; set; }
        public ICommand InsertCustomer
        {
            get
            {
                if (_InsertCustomer == null)
                {
                    _InsertCustomer = new DelegateCommand(Insert_Customer);
                }
                return _InsertCustomer;
            }
        }

        public async void Insert_Customer()
        {
            if (SelectedCustomer.CUSTOMER_NUMBER == "" || SelectedCustomer.CUSTOMER_NUMBER == null)
            {
                MessageBox.Show("customer Code is not Blank");
            }
            else if (SelectedCustomer.NAME == "" || SelectedCustomer.NAME == null)
            {
                MessageBox.Show("customer name is not Blank");
            }
            else if (SelectedCustomer.SEARCH_CODE == "" || SelectedCustomer.SEARCH_CODE == null)
            {
                MessageBox.Show("Search code is not blank");
            }
            else if (SelectedCustomer.VAT_NUMBER == "" || SelectedCustomer.VAT_NUMBER == null)
            {
                MessageBox.Show("VAT no is not blank");
            }
            else if (SelectedCustomer.CST_NUMBER == "" || SelectedCustomer.CST_NUMBER == null)
            {
                MessageBox.Show("CST no is not blank");
            }
            //else if (SelectedCustomer.LOYALTY_NO == "" || SelectedCustomer.LOYALTY_NO == null)
            //{
            //    MessageBox.Show("Loyality No is not blank");
            //}
            else if (SelectedCustomer.SHIPPING_ADDRESS1 == "" || SelectedCustomer.SHIPPING_ADDRESS1 == null)
            {
                MessageBox.Show("Address1 is not blank");
            }
            else if (SelectedCustomer.SHIPPING_ADDRESS2 == "" || SelectedCustomer.SHIPPING_ADDRESS2 == null)
            {
                MessageBox.Show("Address2 is not blank");
            }
            else if (SelectedCustomer.SHIPPING_CITY == "" || SelectedCustomer.SHIPPING_CITY == null)
            {
                MessageBox.Show("City is not blank");
            }
            else if (SelectedCustomer.SHIPPING_STATE == "" || SelectedCustomer.SHIPPING_STATE == null)
            {
                MessageBox.Show("State is not blank");
            }
            else if (SelectedCustomer.SHIPPING_POSTAL_CODE == "" || SelectedCustomer.SHIPPING_POSTAL_CODE == null)
            {
                MessageBox.Show("Postal Code is not blank");
            }
            else if (SelectedCustomer.SHIPPING_TELEPHON_NUMBER == "" || SelectedCustomer.SHIPPING_TELEPHON_NUMBER == null)
            {
                MessageBox.Show("Telephone No is not blank");
            }
            else if (SelectedCustomer.SHIPPING_MOBILE_NO == "" || SelectedCustomer.SHIPPING_MOBILE_NO == null)
            {
                MessageBox.Show("Mobile No is not blank");
            }
            else if (SelectedCustomer.SHIPPING_EMAIL_ADDRESS == "" || SelectedCustomer.SHIPPING_EMAIL_ADDRESS == null)
            {
                MessageBox.Show("Email is not blank");
            }
            //else if (SelectedCustomer.CUSTOMER_GROUP == "" || SelectedCustomer.CUSTOMER_GROUP == null)
            //{
            //    MessageBox.Show("Customer Group is not blank");
            //}
            //else if (SelectedCustomer.REFERRED_BY == "" || SelectedCustomer.REFERRED_BY == null)
            //{
            //    MessageBox.Show("Referred by is not blank");
            //}

            else if (!Regex.IsMatch(SelectedCustomer.SHIPPING_EMAIL_ADDRESS,
           @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
           RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                MessageBox.Show("Please check Email format");
                return;
            }
            else if (!Regex.IsMatch(SelectedCustomer.EMAIL_ADDRESS,
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                MessageBox.Show("Please check Email format");
                return;
            }
            else
            {
                if (App.Current.Properties["OpeningBalance"] != null)
                {
                    var OpenBal = App.Current.Properties["OpeningBalance"] as CustomerModel;

                    SelectedCustomer.BAL_TYPE_ID = OpenBal.BAL_TYPE_ID;
                    SelectedCustomer.BAL_TYPE_VALUE = OpenBal.BAL_TYPE_VALUE;
                    SelectedCustomer.BUSINESS_LOCATION = OpenBal.BUSINESS_LOCATION;
                    SelectedCustomer.BUSINESS_LOCATION_ID = OpenBal.BUSINESS_LOCATION_ID;
                    SelectedCustomer.CLOSING_AMT = OpenBal.CLOSING_AMT;
                    SelectedCustomer.CURRENT_OPENING_BALANCE = OpenBal.CURRENT_OPENING_BALANCE;
                    SelectedCustomer.OPENING_AMT = OpenBal.OPENING_AMT;
                    SelectedCustomer.SYSTEM_DATE = OpenBal.SYSTEM_DATE;
                    SelectedCustomer.DATE = OpenBal.DATE;
                }

                App.Current.Properties["Action"] = "Add";
                SelectedCustomer.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                // This instance has already started one or more requests. Properties can only be modified before sending the first request.
                // _opr.NAME = SelectedCustomer.NAME;
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("api/CustomerAPI/CreateCustomer/", SelectedCustomer);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Customer Insert Successfuly");
                    Cancel_Customer();
                    ModalService.NavigateTo(new CustomerList(), delegate (bool returnValue) { });
                }
            }
        }

        private ImageSource _IMAGE_PATH1;
        public ImageSource IMAGE_PATH1
        {
            get { return _IMAGE_PATH1; }
            set
            {
                if (Equals(value, _IMAGE_PATH1)) return;
                _IMAGE_PATH1 = value;
                OnPropertyChanged("IMAGE_PATH1");
            }
        }

        public ICommand _ImgLoad { get; set; }
        public ICommand ImgLoad
        {
            get
            {
                if (_ImgLoad == null)
                {
                    _ImgLoad = new DelegateCommand(Img_Load);
                }
                return _ImgLoad;
            }
        }


        public ICommand _RemovedImg { get; set; }
        public ICommand RemovedImg
        {
            get
            {
                if (_RemovedImg == null)
                {
                    _RemovedImg = new DelegateCommand(Removed_Img);
                }
                return _RemovedImg;
            }
        }
        public void Img_Load()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files(*.jpg; .jpeg; .gif; .bmp)|*.jpg; .jpeg; .gif; .bmp";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {

                if (File.Exists(openFileDialog.FileName))
                {
                    IMAGE_PATH1 = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                    // IMAGE_PATH = Convert.ToString( new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute)));
                    string imagepath = openFileDialog.FileName.ToString();
                    var imageFile = new System.IO.FileInfo(imagepath);
                    string file = imageFile.Name;
                    var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                    // get your 'Uploaded' folder
                    var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));
                    //if (!dir.Exists)
                    //    dir.Create();
                    // Copy file to your folder
                    imageFile.CopyTo(System.IO.Path.Combine(dir.FullName + "\\", file), true);
                    string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);

                    Ftpup(path1, openFileDialog.SafeFileName);
                    SelectedCustomer.IMAGE_PATH = openFileDialog.SafeFileName;
                }


            }


        }




        public static void Ftpup(string sourceFile, string targetFile)
        {
            try
            {
                string ftpServerIP = "115.115.196.30";
                string ftpUserID = "suvendu";
                string ftpPassword = "Passw0rd";

                //string ftpServerIP = "54.70.197.231";
                //string ftpUserID = "suvendu";
                //string ftpPassword = "vpY9dNp3W9AqhjGy";



                string filename = "ftp://" + ftpServerIP + "//Upload//" + targetFile;
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(filename);
                ftpReq.UseBinary = true;

                ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
                ftpReq.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                byte[] b = System.IO.File.ReadAllBytes(sourceFile);

                ftpReq.ContentLength = b.Length;
                int d = b.Length;
                using (Stream s = ftpReq.GetRequestStream())
                {
                    s.Write(b, 0, b.Length);
                }

                FtpWebResponse ftpResp = (FtpWebResponse)ftpReq.GetResponse();

                if (ftpResp != null)
                {
                    string responseDesc = ftpResp.StatusDescription;
                }
            }
            catch (Exception ex)
            {
                string responseDesc = ex.ToString();
            }
        }




        public static void FtpDown(string sourceFile, string targetFile)
        {
            try
            {
                string ftpServerIP = "115.115.196.30";
                string ftpUserID = "suvendu";
                string ftpPassword = "Passw0rd";

                //string ftpServerIP = "54.70.197.231";
                //string ftpUserID = "suvendu";
                //string ftpPassword = "vpY9dNp3W9AqhjGy";

                string filename = "ftp://" + ftpServerIP + "//Upload//" + targetFile;
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(filename);
                ftpReq.UseBinary = true;

                ftpReq.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpReq.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                byte[] b = System.IO.File.ReadAllBytes(sourceFile);

                ftpReq.ContentLength = b.Length;
                int d = b.Length;
                using (Stream s = ftpReq.GetRequestStream())
                {
                    s.Write(b, 0, b.Length);
                }

                FtpWebResponse ftpResp = (FtpWebResponse)ftpReq.GetResponse();

                if (ftpResp != null)
                {
                    string responseDesc = ftpResp.StatusDescription;
                }
            }
            catch (Exception ex)
            {
                string responseDesc = ex.ToString();
            }
        }
        public void Removed_Img()
        {

            IMAGE_PATH1 = null;
            //IMAGE_PATH1 = new BitmapImage((Uri.));
            AddCustomer.ImageReff.Source = null;
            SelectedCustomer.IMAGE_PATH = null;



        }
        public ICommand _UpdateCustomer { get; set; }
        public ICommand UpdateCustomer
        {
            get
            {
                if (_UpdateCustomer == null)
                {
                    _UpdateCustomer = new DelegateCommand(Update_Customer);
                }
                return _UpdateCustomer;
            }
        }

        public async void Update_Customer()
        {
            if (SelectedCustomer.CUSTOMER_NUMBER == "" || SelectedCustomer.CUSTOMER_NUMBER == null)
            {
                MessageBox.Show("customer Code is not Blank");


            }
            else if (SelectedCustomer.NAME == "" || SelectedCustomer.NAME == null)
            {
                MessageBox.Show("customer name is not Blank");
            }
            else if (SelectedCustomer.SEARCH_CODE == "" || SelectedCustomer.SEARCH_CODE == null)
            {
                MessageBox.Show("Search code is not blank");
            }
            else if (SelectedCustomer.VAT_NUMBER == "" || SelectedCustomer.VAT_NUMBER == null)
            {
                MessageBox.Show("VAT no is not blank");
            }
            else if (SelectedCustomer.CST_NUMBER == "" || SelectedCustomer.CST_NUMBER == null)
            {
                MessageBox.Show("CST no is not blank");
            }
            //else if (SelectedCustomer.LOYALTY_NO == "" || SelectedCustomer.LOYALTY_NO == null)
            //{
            //    MessageBox.Show("Loyality No is not blank");
            //}
            else if (SelectedCustomer.SHIPPING_ADDRESS1 == "" || SelectedCustomer.SHIPPING_ADDRESS1 == null)
            {
                MessageBox.Show("Address1 is not blank");
            }
            else if (SelectedCustomer.SHIPPING_ADDRESS2 == "" || SelectedCustomer.SHIPPING_ADDRESS2 == null)
            {
                MessageBox.Show("Address2 is not blank");
            }
            else if (SelectedCustomer.SHIPPING_CITY == "" || SelectedCustomer.SHIPPING_CITY == null)
            {
                MessageBox.Show("City is not blank");
            }
            else if (SelectedCustomer.SHIPPING_STATE == "" || SelectedCustomer.SHIPPING_STATE == null)
            {
                MessageBox.Show("State is not blank");
            }
            else if (SelectedCustomer.SHIPPING_POSTAL_CODE == "" || SelectedCustomer.SHIPPING_POSTAL_CODE == null)
            {
                MessageBox.Show("Postal Code is not blank");
            }
            else if (SelectedCustomer.SHIPPING_TELEPHON_NUMBER == "" || SelectedCustomer.SHIPPING_TELEPHON_NUMBER == null)
            {
                MessageBox.Show("Telephone No is not blank");
            }
            else if (SelectedCustomer.SHIPPING_MOBILE_NO == "" || SelectedCustomer.SHIPPING_MOBILE_NO == null)
            {
                MessageBox.Show("Mobile No is not blank");
            }
            else if (SelectedCustomer.SHIPPING_EMAIL_ADDRESS == "" || SelectedCustomer.SHIPPING_EMAIL_ADDRESS == null)
            {
                MessageBox.Show("Email is not blank");
            }
            else if (SelectedCustomer.CUSTOMER_GROUP == "" || SelectedCustomer.CUSTOMER_GROUP == null)
            {
                MessageBox.Show("Customer Group is not blank");
            }
            else if (SelectedCustomer.REFERRED_BY == "" || SelectedCustomer.REFERRED_BY == null)
            {
                MessageBox.Show("Referred by is not blank");
            }
            else if (!Regex.IsMatch(SelectedCustomer.SHIPPING_EMAIL_ADDRESS,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                MessageBox.Show("Please Check Email Format in Shipping Address");
                AddCustomer.ShippingemailAddressReff.Focus();
                return;
            }
            else if (!Regex.IsMatch(SelectedCustomer.EMAIL_ADDRESS,
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                MessageBox.Show("Please Check Email Format in Billing Address");
                //AddCustomer.emailAddressReff.Focus();
                return;
            }
            else
            {
                App.Current.Properties["Action"] = "Add";
                SelectedCustomer.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                // This instance has already started one or more requests. Properties can only be modified before sending the first request.
                // _opr.NAME = SelectedCustomer.NAME;
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("api/CustomerAPI/CustomerUpdate/", SelectedCustomer);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Customer Update Successfuly");
                    Cancel_Customer();
                    ModalService.NavigateTo(new CustomerList(), delegate (bool returnValue) { });
                }
            }

        }


        private CustomerModel _CustModelItem;

        public CustomerModel CustModelItem
        {

            get { return _CustModelItem; }
            set
            {

                _CustModelItem = value;

                OnPropertyChanged("CustModelItem");


            }

        }


        List<CustomerGroupAutoModel> autoCustGroupList = new List<CustomerGroupAutoModel>();
        List<CustomerGroupModel> _ListGrid_CustomerGroup = new List<CustomerGroupModel>();
        public async Task<ObservableCollection<CustomerGroupModel>> GetCustomerGroup(long comp)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/CustomerGroupAPI/CustomerGroupList?id=" + comp + "").Result;
            if (response.IsSuccessStatusCode)
            {
                dataGroup = JsonConvert.DeserializeObject<CustomerGroupModel[]>(await response.Content.ReadAsStringAsync());
                int x = 0;
                for (int i = 0; i < dataGroup.Length; i++)
                {
                    x++;


                    autoCustGroupList.Add(new CustomerGroupAutoModel
                    {
                        DisplayName = dataGroup[i].NAME,
                        DisplayId = dataGroup[i].CUSTOMER_GROUP_ID
                    });

                }
                App.Current.Properties["AutoCustGroup"] = autoCustGroupList;
            }
            // ListGrid = _ListGrid_CustomerGroup;
            return new ObservableCollection<CustomerGroupModel>(_ListGrid_CustomerGroup);
        }






        private List<CustomerModel> _CustomerData;
        public List<CustomerModel> CustomerData
        {
            get { return _CustomerData; }
            set
            {
                if (_CustomerData != value)
                {
                    _CustomerData = value;
                    // RaisePropertyChanged(() => HomeImproveData);
                }

            }
        }
        List<CustomerAutoCompleteListModel> autoCustModelList = new List<CustomerAutoCompleteListModel>();

        public async Task GetCustomer(long Id)
        {
            try
            {
                _ListGrid_Temp = new List<CustomerModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                response = await client.GetAsync("api/CustomerAPI/GetCustomer?id=" + Id + "");
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CustomerModel[]>(await response.Content.ReadAsStringAsync());
                    CustomerData = new List<CustomerModel>();
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new CustomerModel
                        {
                            SLNO = x,
                            NAME = data[i].NAME,
                            COMPANY_ID = data[i].COMPANY_ID,
                            LAST_NAME = data[i].LAST_NAME,
                            DISPLAY_CUS_LAST = data[i].LAST_NAME,
                            SEARCH_CODE = data[i].SEARCH_CODE,
                            VAT_NUMBER = data[i].VAT_NUMBER,
                            CST_NUMBER = data[i].CST_NUMBER,
                            LOYALTY_NO = data[i].LOYALTY_NO,
                            CUSTOMER_ID = data[i].CUSTOMER_ID,
                            BILLING_ADDRESS1 = data[i].BILLING_ADDRESS1,
                            BILLING_ADDRESS2 = data[i].BILLING_ADDRESS2,
                            BILLING_TO_NAME = data[i].BILLING_TO_NAME,
                            IS_ENROLLED_FOR_LOYALITY = data[i].IS_ENROLLED_FOR_LOYALITY,
                            CITY = data[i].CITY,
                            COUNTRY = data[i].COUNTRY,
                            CUSTOMER_GROUP = data[i].CUSTOMER_GROUP,
                            CUSTOMER_NUMBER = data[i].CUSTOMER_NUMBER,
                            DEFAULT_CREIT_LIMIT = data[i].DEFAULT_CREIT_LIMIT,
                            EMAIL_ADDRESS = data[i].EMAIL_ADDRESS,
                            IS_ACTIVE = data[i].IS_ACTIVE,
                            MOBILE_NO = data[i].MOBILE_NO,
                            NOTES = data[i].NAME,
                            credit_Limits1 = data[i].credit_Limits1,
                            TIN = data[i].TIN,
                            PAN = data[i].PAN,
                            BUSINESS_LOCATION = data[i].BUSINESS_LOCATION,
                            BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID,


                            OPENING_AMT = data[i].OPENING_AMT,
                            CURRENT_OPENING_BALANCE = data[i].CURRENT_OPENING_BALANCE,
                            POSTAL_CODE = data[i].POSTAL_CODE,
                            REFERRED_BY = data[i].REFERRED_BY,
                            IMAGE_PATH = data[i].IMAGE_PATH,
                            // SAMEBILLINGANDSHIPPING_ADDRESS = data[i].SAMEBILLINGANDSHIPPING_ADDRESS,
                            STATE = data[i].STATE,
                            TELEPHON_NUMBER = data[i].TELEPHON_NUMBER,
                            SHIPPING_ADDRESS1 = data[i].SHIPPING_ADDRESS1,
                            SHIPPING_ADDRESS2 = data[i].SHIPPING_ADDRESS2,
                            SHIPPING_CITY = data[i].SHIPPING_CITY,
                            SHIPPING_COUNTRY = data[i].SHIPPING_COUNTRY,
                            SHIPPING_EMAIL_ADDRESS = data[i].SHIPPING_EMAIL_ADDRESS,
                            SHIPPING_MOBILE_NO = data[i].SHIPPING_MOBILE_NO,
                            SHIPPING_TELEPHON_NUMBER = data[i].SHIPPING_TELEPHON_NUMBER,
                            SHIPPING_POSTAL_CODE = data[i].SHIPPING_POSTAL_CODE,
                            SHIPPING_STATE = data[i].SHIPPING_STATE,
                            BAL_TYPE_VALUE = data[i].BAL_TYPE_VALUE,
                            FULL_NAME = "" + data[i].NAME + " " + data[i].LAST_NAME + "",
                        });
                        autoCustModelList.Add(new CustomerAutoCompleteListModel
                        {
                            DisplayName = data[i].NAME,
                            DisplayId = data[i].CUSTOMER_ID
                        });
                    }
                    App.Current.Properties["AutoCustList"] = autoCustModelList;
                    if (IS_ACTIVESearch == true)
                    {
                        var item1 = (from m in _ListGrid_Temp where m.IS_ACTIVE == true select m).ToList();
                        if (item1.Count > 0)
                        {
                            _ListGrid_Temp = item1;
                        }
                    }
                    if (IS_InACTIVESearch == true)
                    {

                        var item1 = (from m in _ListGrid_Temp where m.IS_ACTIVE == false select m).ToList();
                        if (item1.Count > 0)
                        {
                            _ListGrid_Temp = item1;
                        }
                    }
                    if (SEARCH_CUS != "" && SEARCH_CUS != null && IS_ACTIVESearch == true)
                    {

                        var item1 = (from m in _ListGrid_Temp where m.NAME.Contains(SEARCH_CUS) && m.IS_ACTIVE == true select m).ToList();
                        if (item1 != null)
                        {
                            if (item1.Count > 0)
                            {
                                _ListGrid_Temp = item1;
                            }
                        }


                        var item2 = (from m in _ListGrid_Temp where m.SEARCH_CODE.Contains(SEARCH_CUS) && m.IS_ACTIVE == true select m).ToList();
                        if (item2 != null)
                        {
                            if (item2.Count > 0)
                            {
                                _ListGrid_Temp = item2;
                            }
                        }

                        var item3 = (from m in _ListGrid_Temp where m.CUSTOMER_NUMBER.Contains(SEARCH_CUS) && m.IS_ACTIVE == true select m).ToList();
                        if (item3 != null)
                        {
                            if (item3.Count > 0)
                            {
                                _ListGrid_Temp = item3;
                            }
                        }
                    }
                    if (SEARCH_CUS != "" && SEARCH_CUS != null && IS_ACTIVESearch == false)
                    {
                        var item1 = (from m in _ListGrid_Temp where m.NAME.Contains(SEARCH_CUS) || m.SEARCH_CODE.Contains(SEARCH_CUS) || m.CUSTOMER_NUMBER.Contains(SEARCH_CUS) select m).ToList();
                        if (item1 != null)
                        {
                            if (item1.Count > 0)
                            {
                                _ListGrid_Temp = item1;
                            }
                        }


                        // var item2 = (from m in _ListGrid_Temp where m.SEARCH_CODE.Contains(SEARCH_CUS) select m).ToList();
                        //if (item2 != null)
                        //{
                        //    if (item2.Count > 0)
                        //    {
                        //        _ListGrid_Temp = item2;
                        //    }
                        //}

                        //var item3 = (from m in _ListGrid_Temp where m.CUSTOMER_NUMBER.Contains(SEARCH_CUS) select m).ToList();
                        //if (item3 != null)
                        //{
                        //    if (item3.Count > 0)
                        //    {
                        //        _ListGrid_Temp = item3;
                        //    }
                        //}
                    }
                }

                ListGrid = _ListGrid_Temp;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public CustomerViewModel()
        {
            //CheckCommand = new RelayCommand(ShippingAddressSameAsBillingAddress);
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            GetCustomerGroup(comp);
            if (App.Current.Properties["Action"].ToString() == "Add")
            {
                CreateVisible = "Visible";
                UpdVisible = "Collapsed";
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreateVisible = "Collapsed";
                UpdVisible = "Visible";
                ReadVisible = "Collapsed";
                RiteVisible = "Visible";
                CUSTNUM_ENABLE = true;
                SelectedCustomer = App.Current.Properties["EditCustomer"] as CustomerModel;
                //if (SelectedCustomer.IMAGE_PATH!=null)
                //{
                //    IMAGE_PATH1 = new BitmapImage(new Uri(SelectedCustomer.IMAGE_PATH));
                //}
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                SelectedCustomer = App.Current.Properties["ViewCustomer"] as CustomerModel;
                //string ftpServerIP = "115.115.196.30";
                //string ftpUserID = "suvendu";
                //string ftpPassword = "Passw0rd";


                string ftpServerIP = "54.70.197.231";
                string ftpUserID = "suvendu";
                string ftpPassword = "vpY9dNp3W9AqhjGy";

                string filename = "ftp://" + ftpServerIP + "//Upload//";

                var fr = filename + SelectedCustomer.IMAGE_PATH;

                var imageFile = new System.IO.FileInfo(SelectedCustomer.IMAGE_PATH);
                string file = imageFile.Name;
                var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                // get your 'Uploaded' folder
                var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));
                if (!dir.Exists)
                    dir.Create();
                // Copy file to your folder
                //imageFile.CopyTo(System.IO.Path.Combine(dir.FullName, file), true);
                string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);

                FtpDown(path1, file);

                IMAGE_PATH1 = new BitmapImage(new Uri(path1));
                App.Current.Properties["ItemViewImg"] = IMAGE_PATH1;



                SelectedCustomer.IMAGE_PATH = file;
                Application.Current.Properties["Action"] = "";
            }
            else
            {
                SelectedCustomer = new CustomerModel
                {
                    BILLING_ADDRESS1 = "",
                    BILLING_ADDRESS2 = "",
                    EMAIL_ADDRESS = "",
                    SHIPPING_ADDRESS1 = "",
                    SHIPPING_ADDRESS2 = "",
                    SHIPPING_CITY = "",
                    SHIPPING_EMAIL_ADDRESS = "",
                    SHIPPING_MOBILE_NO = "",
                    SHIPPING_POSTAL_CODE = "",
                    SHIPPING_STATE = "",
                    SHIPPING_TELEPHON_NUMBER = "",
                    POSTAL_CODE = "",
                    CITY = "",
                    MOBILE_NO = "",
                    STATE = "",


                };
                // ENABLE_LOYALTY = false;
                CUSTNUM_ENABLE = false;
                CreateVisible = "Visible";
                UpdVisible = "Collapsed";
                ReadVisible = "Visible";
                RiteVisible = "Collapsed";
                ENABLE_LOYALTY = false;
                DEFAULTVISIBLE = "Collapsed";
                AUTOVISIBLE = "Visible";
                OprModel = _opr;
                if (App.Current.Properties["GenerateCode"] == null || App.Current.Properties["GenerateCode"] == "")
                {
                    GetCustomer(comp);
                }
                GetLoyaltyNo();
                if (App.Current.Properties["GenerateCode"] != null && App.Current.Properties["GenerateCode"] != "")
                {
                    GetCustomerNo();
                    App.Current.Properties["GenerateCode"] = null;
                }
                LOYALTY_DATE = DateTime.Now.ToString("dd/MMM/yyyy");

                if (App.Current.Properties["CustomerDiffProperties"] == null)
                {
                    App.Current.Properties["CustomerDiffProperties"] = SelectedCustomer;
                }
            }
            IsChecked = true;
            _Tab_Ship = "False";
            _Tab_Bill = "False";
            // App.Current.Properties["CustomerDiffProperties"] = SelectedCustomer;

        }
        private int _selected;
        public int Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                SelectedCustomer = new CustomerModel();
                OnPropertyChanged("Selected");
            }
        }
        private string _Tab_Ship;
        public string Tab_Ship
        {
            get { return _Tab_Ship; }
            set
            {
                if (value != _Tab_Ship)
                {
                    _Tab_Ship = value;

                    OnPropertyChanged("Tab_Ship");
                    SelectedCustomer = new CustomerModel();
                }
            }
        }
        private string _Tab_Bill;
        public string Tab_Bill
        {
            get { return _Tab_Bill; }
            set
            {
                if (value != _Tab_Bill)
                {
                    _Tab_Bill = value;
                    SelectedCustomer = new CustomerModel();
                    OnPropertyChanged("Tab_Bill");
                    SelectedCustomer = new CustomerModel();
                }
            }
        }







        private string _LOYALTY_DATE { get; set; }
        public string LOYALTY_DATE
        {
            get { return _LOYALTY_DATE; }
            set
            {
                _LOYALTY_DATE = value;


                if (_LOYALTY_DATE != value)
                {
                    _LOYALTY_DATE = value;
                    OnPropertyChanged("LOYALTY_DATE");
                }

            }

        }

        private string _LOYALTY_DATE_VISIBLE;
        public string LOYALTY_DATE_VISIBLE
        {

            get
            {
                return _LOYALTY_DATE_VISIBLE;
            }
            set
            {
                _LOYALTY_DATE_VISIBLE = value;
                OnPropertyChanged("LOYALTY_DATE_VISIBLE");
            }
        }



        private bool _CUSTNUM_ENABLE;
        public bool CUSTNUM_ENABLE
        {

            get
            {
                return _CUSTNUM_ENABLE;
            }
            set
            {

                _CUSTNUM_ENABLE = value;
                OnPropertyChanged("CUSTNUM_ENABLE");
            }
        }
        public async Task<string> GetLoyaltyNo()
        {

            string uhy = "";
            try
            {

                string Cmd = App.Current.Properties["Company_Id"].ToString();
                SelectedCustomer.LOYALTY_NO = "";
                // string uhy = "";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CustomerAPI/GetLoyaltyNo?CompanyId=" + Cmd + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    uhy = await response.Content.ReadAsStringAsync();
                    string dd = Convert.ToString(uhy);
                    string aaa = dd.Substring(3, 6);
                    int xx = Convert.ToInt32(aaa);
                    string nnnn = "L-" + xx.ToString("D5");
                    SelectedCustomer.LOYALTY_NO = nnnn;
                }
                else
                {
                    SelectedCustomer.LOYALTY_NO = "L-00001";
                }

                //string nnnn = "";
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                //client.DefaultRequestHeaders.Accept.Add(
                //    new MediaTypeWithQualityHeaderValue("application/json"));
                //client.Timeout = new TimeSpan(500000000000);
                //HttpResponseMessage response = client.GetAsync("api/LoyaltyAPI/GetLoyaltyNo/").Result;
                //if (response.IsSuccessStatusCode)
                //{
                //    //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                //    uhy = await response.Content.ReadAsStringAsync();
                //}
                //string dd = Convert.ToString(uhy);
                ////string aaa = dd.Substring(3, 5);
                //int xx = Convert.ToInt32(dd);
                //int noo = Convert.ToInt32(xx + 1);
                //nnnn = "LN" + noo.ToString("D5");
                //SelectedCustomer.LOYALTY_NO = nnnn;

            }
            catch (Exception ex)
            { }

            return uhy;
        }

        public async Task<string> GetCustomerNo()
        {

            string uhy = "";
            try
            {
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CustomerAPI/GetCustomerNo1?CompanyId=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                    uhy = await response.Content.ReadAsStringAsync();
                    string dd = Convert.ToString(uhy);
                    string aaa = dd.Substring(1, 8);
                    CUSTOMER_NUMBER = aaa;
                }
                else
                {
                    CUSTOMER_NUMBER = "C-00001";
                }
            }
            catch (Exception ex)
            { }

            return uhy;
        }
        //public async Task<string> GetCustomerNo1()
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
        //        HttpResponseMessage response = client.GetAsync("api/CustomerAPI/GetCustomerNo").Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
        //            uhy = await response.Content.ReadAsStringAsync();
        //        }
        //        string dd = Convert.ToString(uhy);
        //        string aaa = dd.Substring(1, 8);
        //        SelectedCustomer.CUSTOMER_NUMBER = aaa;

        //    }
        //    catch (Exception ex)
        //    { }

        //    return uhy;
        //}
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
                List<CustomerModel> _ListGridTemp = new List<CustomerModel>();
                if (objdt.Rows.Count > 0)
                {
                    for (int i = 0; i < objdt.Rows.Count; i++)
              
                    {
                        var df = objdt.Rows[0];
                        SelectedCustomer.SLNO = Convert.ToInt32(objdt.Rows[i].ItemArray[0]);
                        SelectedCustomer.CUSTOMER_NUMBER = Convert.ToString(objdt.Rows[i].ItemArray[1]);
                        SelectedCustomer.FULL_NAME = Convert.ToString(objdt.Rows[i].ItemArray[2]);
                        SelectedCustomer.SEARCH_CODE = Convert.ToString(objdt.Rows[i].ItemArray[3]);
                        SelectedCustomer.CUSTOMER_GROUP = Convert.ToString(objdt.Rows[i].ItemArray[4]);
                        SelectedCustomer.IS_ENROLLED_FOR_LOYALITY = Convert.ToBoolean(objdt.Rows[i].ItemArray[5]);
                        SelectedCustomer.LOYALTY_NO = Convert.ToString(objdt.Rows[i].ItemArray[6]);
                        SelectedCustomer.SHIPPING_MOBILE_NO = Convert.ToString(objdt.Rows[i].ItemArray[7]);
                        SelectedCustomer.SHIPPING_TELEPHON_NUMBER = Convert.ToString(objdt.Rows[i].ItemArray[8]);
                        SelectedCustomer.SHIPPING_ADDRESS1 = Convert.ToString(objdt.Rows[i].ItemArray[9]);
                        SelectedCustomer.SHIPPING_ADDRESS2 = Convert.ToString(objdt.Rows[i].ItemArray[10]);
                        SelectedCustomer.SHIPPING_CITY = Convert.ToString(objdt.Rows[i].ItemArray[11]);
                        SelectedCustomer.SHIPPING_STATE = Convert.ToString(objdt.Rows[i].ItemArray[12]);
                        SelectedCustomer.VAT_NUMBER = Convert.ToString(objdt.Rows[i].ItemArray[13]);
                        SelectedCustomer.CST_NUMBER = Convert.ToString(objdt.Rows[i].ItemArray[14]);
                        SelectedCustomer.EMAIL_ADDRESS = Convert.ToString(objdt.Rows[i].ItemArray[15]);
                        SelectedCustomer.NAME = Convert.ToString(objdt.Rows[i].ItemArray[16]);

                        _ListGridTemp.Add(new CustomerModel
                        {
                            SLNO = Convert.ToInt32(objdt.Rows[i].ItemArray[0]),
                            CUSTOMER_NUMBER = Convert.ToString(objdt.Rows[i].ItemArray[1]),
                            FULL_NAME = Convert.ToString(objdt.Rows[i].ItemArray[2]),
                            CUSTOMER_GROUP = Convert.ToString(objdt.Rows[i].ItemArray[4]),
                            IS_ENROLLED_FOR_LOYALITY = Convert.ToBoolean(objdt.Rows[i].ItemArray[5]),
                            LOYALTY_NO = Convert.ToString(objdt.Rows[i].ItemArray[6]),
                            SHIPPING_MOBILE_NO = Convert.ToString(objdt.Rows[i].ItemArray[7]),
                            SHIPPING_TELEPHON_NUMBER = Convert.ToString(objdt.Rows[i].ItemArray[8]),
                            SHIPPING_ADDRESS1 = Convert.ToString(objdt.Rows[i].ItemArray[9]),
                            SHIPPING_ADDRESS2 = Convert.ToString(objdt.Rows[i].ItemArray[10]),
                            SHIPPING_CITY = Convert.ToString(objdt.Rows[i].ItemArray[11]),
                            SHIPPING_STATE = Convert.ToString(objdt.Rows[i].ItemArray[12]),
                            VAT_NUMBER = Convert.ToString(objdt.Rows[i].ItemArray[13]),
                            CST_NUMBER = Convert.ToString(objdt.Rows[i].ItemArray[14]),
                            EMAIL_ADDRESS = Convert.ToString(objdt.Rows[i].ItemArray[15]),
                            NAME = Convert.ToString(objdt.Rows[i].ItemArray[16])
                        });
                    }
                    

                }
                ListGrid = _ListGridTemp;
                App.Current.Properties["ExcelData"] = SelectedCustomer;

            }

        }
        public ICommand _InsertToExcel { get; set; }
        public ICommand InsertToExcel
        {
            get
            {
                if (_InsertToExcel == null)
                {
                    _InsertToExcel = new DelegateCommand(InsertTo_Excel);
                }
                return _InsertToExcel;
            }
        }
        public void InsertTo_Excel()
        {
            if (App.Current.Properties["ExcelData"] != null)
            {
                SelectedCustomer = App.Current.Properties["ExcelData"] as CustomerModel;
                Insert_Customer();
                App.Current.Properties["ExcelData"] = null;
            }
        }
        public ICommand _LoyalityClick;
        public ICommand LoyalityClick
        {
            get
            {
                if (_LoyalityClick == null)
                {
                    _LoyalityClick = new DelegateCommand(Loyality_Click);
                }
                return _LoyalityClick;
            }
        }
        public async void Loyality_Click()
        {
            AUTOGENERATE = false;
            MYDEFAULT = true;
            ENABLE_LOYALTY = false;
            AUTOVISIBLE = "Collapsed";
            DEFAULTVISIBLE = "Visible";
            GetLoyaltyNo();

            //    string uhy = "";
            //    HttpClient client = new HttpClient();
            //    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            //    client.DefaultRequestHeaders.Accept.Add(
            //        new MediaTypeWithQualityHeaderValue("application/json"));
            //    client.Timeout = new TimeSpan(500000000000);
            //    HttpResponseMessage response = client.GetAsync("api/CustomerAPI/GetLoyaltyNo/").Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        uhy = await response.Content.ReadAsStringAsync();
            //        string dd = Convert.ToString(uhy);
            //        string aaa = dd.Substring(3, 6);
            //        int xx = Convert.ToInt32(aaa);
            //        int noo = Convert.ToInt32(xx + 1);
            //        string nnnn = "L-" + noo.ToString("D5");
            //        SelectedCustomer.LOYALTY_NO = nnnn;
            //    }
            //}
            //else
            //{
            //    SelectedCustomer.LOYALTY_NO = "";
            //}

        }



        public ICommand _LoyaltyDefault;
        public ICommand LoyaltyDefault
        {
            get
            {
                if (_LoyaltyDefault == null)
                {
                    _LoyaltyDefault = new DelegateCommand(Loyalty_Default);
                }
                return _LoyaltyDefault;
            }
        }


        public async void Loyalty_Default()
        {
            ENABLE_LOYALTY = true;
            AUTOVISIBLE = "Visible";
            MYDEFAULT = true;
            DEFAULTVISIBLE = "Collapsed";
            AUTOGENERATE = true;
            SelectedCustomer.LOYALTY_NO = "";
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
            IA.ShowDialog();

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
            App.Current.Properties["CustomerReff"] = 1;
            Window_CustomerList IA = new Window_CustomerList();
            IA.ShowDialog();

        }

        public ICommand _LoyaltyClick;
        public ICommand LoyaltyClick
        {
            get
            {
                if (_CustomerClick == null)
                {
                    _LoyaltyClick = new DelegateCommand(Loyalty_Click);
                }
                return _LoyaltyClick;
            }
        }

        public void Loyalty_Click()
        {
            WindowLoyaltyList IA = new WindowLoyaltyList();
            IA.ShowDialog();

        }
        public ICommand _OpeningBalance;
        public ICommand OpeningBalance
        {
            get
            {
                if (_OpeningBalance == null)
                {
                    _OpeningBalance = new DelegateCommand(Opening_Balance);
                }
                return _OpeningBalance;
            }
        }

        public void Opening_Balance()
        {
            App.Current.Properties["OpeningBalance"] = SelectedCustomer;
            AddCustomer.OpBalReff.Text = Convert.ToString(SelectedCustomer.CURRENT_OPENING_BALANCE);
            App.Current.Properties["OpeningBalanceAmount"] = Convert.ToString(SelectedCustomer.CURRENT_OPENING_BALANCE);
            AddCustomer.BalanceTyprReff.Text = Convert.ToString(SelectedCustomer.BAL_TYPE_VALUE);
            Cancel_Customer();
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
            var fdrt = App.Current.Properties["CustomerDiffProperties"] as CustomerModel;
            SelectedCustomer.NAME = SelectedCustomer.NAME;
            SelectedCustomer.LOYALTY_NO = fdrt.LOYALTY_NO;
            //App.Current.Properties["CustomerLoyality"] = SelectedCustomer.LOYALTY_NO;
            SelectedCustomer.CUSTOMER_GROUP = fdrt.CUSTOMER_GROUP;
            App.Current.Properties["CustomerDiffProperties"] = SelectedCustomer;
            App.Current.Properties["CustomerName"] = SelectedCustomer.NAME;
            App.Current.Properties["GetCustomerId"] = SelectedCustomer.CUSTOMER_ID;

            App.Current.Properties["CurrentOpeningBalanceAmount"] = SelectedCustomer.CURRENT_OPENING_BALANCE;

            App.Current.Properties["CustomerMobile"] = SelectedCustomer.MOBILE_NO;
            App.Current.Properties["CustomerEmailaddress"] = SelectedCustomer.EMAIL_ADDRESS;
            App.Current.Properties["CustomerName"] = SelectedCustomer.NAME;
            App.Current.Properties["CustomerLimits"] = SelectedCustomer.credit_Limits1;
            App.Current.Properties["CURRENT_OPENING_BALANCE"] = SelectedCustomer.CURRENT_OPENING_BALANCE;
            invoview.CreditsLimits = SelectedCustomer.credit_Limits1;
            Main.CustomerMainReff.Text = SelectedCustomer.NAME;
            if (App.Current.Properties["CustomerReff"] != null)
            {
                AddCustomer.ReferredRef.Text = null;
                AddCustomer.ReferredRef.Text = SelectedCustomer.NAME;
                App.Current.Properties["CustomerReff"] = null;
            }
            if (App.Current.Properties["CustomerRecivePaymentReff"] != null)
            {
                AddReceivePayment.CustomerRecivePaymentReff.Text = null;
                AddReceivePayment.CustomerRecivePaymentReff.Text = SelectedCustomer.NAME;
                AddReceivePayment.ReceiveCustomeremail.Text = null;
                AddReceivePayment.ReceiveCustomeremail.Text = SelectedCustomer.EMAIL_ADDRESS;
                AddReceivePayment.ReceiveCustomercontact.Text = null;
                AddReceivePayment.ReceiveCustomercontact.Text = SelectedCustomer.MOBILE_NO;
                App.Current.Properties["CustomerRecivePaymentReff"] = null;
            }

            if (App.Current.Properties["LoyltyCustomerGroup"] != null)
            {
                //AddLoyality.CustomerRecivePaymentReff.Text = null;
                //AddReceivePayment.CustomerRecivePaymentReff.Text = SelectedCustomer.NAME;
                //App.Current.Properties["CustomerRecivePaymentReff"] = null;

                //App.Current.Properties["LoyltyCustomer"] = SelectedCustomer.NAME;
            }



            if (App.Current.Properties["InvoiceCustomerReff"] != null)
            {
                PayNow.InvoiceCustomerReff.Text = null;
                PayNow.InvoiceCustomerReff.Text = SelectedCustomer.NAME;
                PayNow.InvoiceCustomerMobileReff.Text = null;
                PayNow.InvoiceCustomerMobileReff.Text = SelectedCustomer.MOBILE_NO;
                PayNow.InvoiceCustomerEmailReff.Text = null;
                PayNow.InvoiceCustomerEmailReff.Text = SelectedCustomer.EMAIL_ADDRESS;
                PayNow.InvoiceCustomerLimits.Text = SelectedCustomer.credit_Limits1.ToString();
                PayNow.InvoiceCustomerLimits1.Text = SelectedCustomer.credit_Limits1.ToString();
                App.Current.Properties["CreditLimit"] = SelectedCustomer.credit_Limits1.ToString();
                App.Current.Properties["AdvancedAmount"] = SelectedCustomer.CURRENT_OPENING_BALANCE.ToString();
                App.Current.Properties["InvoiceCustomerReff"] = null;
            }
            if (App.Current.Properties["CustomerMainClick"] != null)
            {
                App.Current.Properties["CustomerPhoneNo"] = null;
                App.Current.Properties["CustomerEmail"] = null;
                Main.CustomerMainReff.Text = null;
                Main.CustomerMainReff.Text = SelectedCustomer.NAME;
                App.Current.Properties["CustomerPhoneNo"] = SelectedCustomer.SHIPPING_TELEPHON_NUMBER;
                App.Current.Properties["CustomerEmail"] = SelectedCustomer.EMAIL_ADDRESS;
                App.Current.Properties["CreditLimit"] = SelectedCustomer.credit_Limits1.ToString();
                App.Current.Properties["CustomerMainClick"] = null;
            }
            //AddCustomer.LoyaltyRef.Text = null;
            //AddCustomer.LoyaltyRef.Text = SelectedCustomer.LOYALTY_NO;
            //AddCustomer.CustGroupRef.Text = null;
            //AddCustomer.CustGroupRef.Text = SelectedCustomer.CUSTOMER_GROUP;
            Cancel_Customer();
        }
        public ICommand _ReceivePaymentClick;
        public ICommand ReceivePaymentClick
        {
            get
            {
                if (_ReceivePaymentClick == null)
                {
                    _ReceivePaymentClick = new DelegateCommand(ReceivePayment_Click);
                }
                return _ReceivePaymentClick;
            }
        }

        public void ReceivePayment_Click()
        {
            App.Current.Properties["CustomerName"] = SelectedCustomer;
            if (SelectedCustomer.CUSTOMER_ID != null && SelectedCustomer.CUSTOMER_ID != 0)
            {
                AddReceivePayment IA = new AddReceivePayment();
                IA.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select Customer");
            }


        }

        public ICommand _OpeningBalanceClick;
        public ICommand OpeningBalanceClick
        {
            get
            {
                if (_OpeningBalanceClick == null)
                {
                    _OpeningBalanceClick = new DelegateCommand(OpeningBalance_Click);
                }
                return _OpeningBalanceClick;
            }
        }

        public void OpeningBalance_Click()
        {
            Openingbalance IA = new Openingbalance();
            IA.ShowDialog();

        }
        public ICommand _ExcelClick;
        public ICommand ExcelClick
        {
            get
            {
                if (_ExcelClick == null)
                {
                    _ExcelClick = new DelegateCommand(Excel_Click);
                }
                return _ExcelClick;
            }
        }
        public void Excel_Click()
        {
            ImportExcel _AC = new ImportExcel();
            _AC.ShowDialog();
            // ModalService.NavigateTo(new AddCustomer(), delegate(bool returnValue) { });
            //MessageBox.Show("", "", MessageBoxButton.YesNo);


        }

        public ICommand _DownloadExcel;
        public ICommand DownloadExcel
        {
            get
            {
                if (_ExcelClick == null)
                {
                    _ExcelClick = new DelegateCommand(Download_Excel);
                }
                return _ExcelClick;
            }
        }
        public virtual void Download_Excel()
        {
            var str = "http://10.10.10.108/upload/Customer_Excel.xlsx";
           
        }

        //if (MessageBox.Show( "Customer Delete Or Not", "Please confirm..", MessageBoxButtons.YesNo) == DialogResult.No)
        //                {
        //                    base.Close();
        //                }
        //IMessageBoxManager _messageBoxManager;
        //if(HasUnsavedChanges)
        //{
        //    var result = _messageBoxManager.ShowMessageBox(
        //                     "Unsaved changes, save them before close?", 
        //                     "Confirmation", MessageBoxButtons.YesNoCancel);
        //    if(result == MessageBoxResult.Yes)
        //        Save();
        //    else if(result == MessageBoxResult.Cancel)
        //        return; // <- Don't close window
        //    else if(result == MessageBoxResult.No)
        //        RevertUnsavedChanges();
        //}
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

