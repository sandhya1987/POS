using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InvoicePOS.Models
{
    public class SupplierModel : IDataErrorInfo
    {
        public long SUPPLIER_ID { get; set; }
        public long COMPANY_ID { get; set; }
        public string SUPPLIER_CODE { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public string DisplaySuppName { get; set; }
        public string SEARCH_CODE { get; set; }
        public string VAT_NO { get; set; }
        public string CST_NO { get; set; }
        public string TIN_NO { get; set; }
        public string PAN_NO { get; set; }
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string ZIP_CODE { get; set; }
        public decimal OPENING_BALANCE { get; set; }
        public bool IS_PREFERRED_SUPPLIER { get; set; }
        public bool IS_ACTIVE { get; set; }
        public int PAYMENT_SETTLED { get; set; }
        public string IMAGE_PATH { get; set; }
        public string CONTACT_FRIST_NAME { get; set; }
        public string CONTACT_LAST_NAME { get; set; }
        public string CONTACT_TELEPHONE_NO { get; set; }
        public string CONTACT_FAX_NO { get; set; }
        public string CONTACT_MOBILE_NO { get; set; }
        public string CONTACT_WEBSITE { get; set; }
        public string CONTACT_EMAIL { get; set; }
        public string SEARCH_SUPPLIER { get; set; }
        public int SLNO { get; set; }


        private string error = string.Empty;
        public string Error
        {
            get { return error; }
            set { error = value; }
        }






        public string this[string columnName]
        {
            get
            {

                error = string.Empty;

                if (columnName == "SUPPLIER_NAME" && string.IsNullOrWhiteSpace(SUPPLIER_NAME))
                {
                    error = "SUPPLIER NAME  is required!";

                }
                if (columnName == "SEARCH_CODE" && string.IsNullOrWhiteSpace(SEARCH_CODE))
                {
                    error = "SEARCH CODE is required!";

                }
                if (columnName == "VAT_NO" && string.IsNullOrWhiteSpace(VAT_NO))
                {
                    error = "VAT NO is required!";

                }
                if (columnName == "CST_NO" && string.IsNullOrWhiteSpace(CST_NO))
                {
                    error = "CST NO is required!";

                }
                if (columnName == "TIN_NO" && string.IsNullOrWhiteSpace(TIN_NO))
                {
                    error = "TIN NO is required!";

                }
                if (columnName == "PAN_NO" && string.IsNullOrWhiteSpace(PAN_NO))
                {
                    error = "PAN NO is required!";

                }
                if (columnName == "ADDRESS_1" && string.IsNullOrWhiteSpace(ADDRESS_1))
                {
                    error = "ADDRESS is required!";

                }
                if (columnName == "ADDRESS_2" && string.IsNullOrWhiteSpace(ADDRESS_2))
                {
                    error = "ADDRESS_2  is required!";

                }
                if (columnName == "CITY" && string.IsNullOrWhiteSpace(CITY))
                {
                    error = "CITY  is required!";

                }
                if (columnName == "STATE" && string.IsNullOrWhiteSpace(STATE))
                {
                    error = "STATE  is required!";

                }
                if (columnName == "ZIP_CODE" && string.IsNullOrWhiteSpace(ZIP_CODE))
                {
                    error = "ZIP_CODE  is required!";

                }
                if (columnName == "CONTACT_FRIST_NAME" && string.IsNullOrWhiteSpace(CONTACT_FRIST_NAME))
                {
                    error = "CONTACT_FRIST_NAME  is required!";

                }
                if (columnName == "CONTACT_FAX_NO" && string.IsNullOrWhiteSpace(CONTACT_FAX_NO))
                {
                    error = "CONTACT_FAX_NO  is required!";

                }
                if (columnName == "CONTACT_TELEPHONE_NO" && string.IsNullOrWhiteSpace(CONTACT_TELEPHONE_NO))
                {
                    error = "CONTACT_TELEPHONE_NO  is required!";

                }
                if (columnName == "CONTACT_MOBILE_NO" && string.IsNullOrWhiteSpace(CONTACT_MOBILE_NO))
                {
                    error = "CONTACT_MOBILE_NO  is required!";

                }
                if (columnName == "CONTACT_WEBSITE" && string.IsNullOrWhiteSpace(CONTACT_WEBSITE))
                {
                    error = "CONTACT_WEBSITE  is required!";

                }
                if (columnName == "CONTACT_EMAIL" && string.IsNullOrWhiteSpace(CONTACT_EMAIL))
                {
                    error = "CONTACT_EMAIL  is required!";

                }
                switch (columnName)
                {
                    case "PAYMENT_SETTLED": if ((PAYMENT_SETTLED <= 0)) error = "PAYMENT SETTLED is required!"; break;
                };
                return error;
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
    }
    public class SuppPaymentModel : IDataErrorInfo
    {
        //public class SuppPaymentModel 
        //{
        public long SUPP_PAYMENT { get; set; }
        public string SUPP_pay_no { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public string SUPP { get; set; }
        public string SUPP_EMAIL { get; set; }
        public string SUPP_SMS { get; set; }
        public bool IS_SEND_EMAIL { get; set; }
        public bool IS_SEND_SMS { get; set; }
        public bool IS_PRINT_CHECK { get; set; }
        public decimal CURRENT_ADV_AMT { get; set; }
        public decimal TOTAL_RIE_AMT { get; set; }
        public decimal PENDING_AMT { get; set; }
        public string PAYMENT_NUMBER { get; set; }
        public DateTime PAYMENT_DATE { get; set; }
        public decimal CURRENT_PAYABLE_AMT { get; set; }
        public decimal TOTAL_PANDING { get; set; }
        public string FORMATTED_TOTAL_PANDING
        {
            get
            {
                CurrencySettingsModel CSM = (CurrencySettingsModel)Application.Current.Properties["CurrencySettings"];
                string tmp = TOTAL_PANDING.ToString(CSM.NUMBER_FORMAT);
                if (CSM.NORMAL_CURRENCY_SYMBOL_LEFT)
                {
                    tmp = CSM.NORMAL_CURRENCY_SYMBOL + tmp;
                }
                else
                {
                    tmp = tmp + CSM.NORMAL_CURRENCY_SYMBOL;
                }
                return tmp;
            }
        }
        public decimal SELECTED_AMT { get; set; }
        public string FORMATTED_SELECTED_AMT
        {
            get
            {
                CurrencySettingsModel CSM = (CurrencySettingsModel)Application.Current.Properties["CurrencySettings"];
                string tmp = SELECTED_AMT.ToString(CSM.NUMBER_FORMAT);
                if (CSM.NORMAL_CURRENCY_SYMBOL_LEFT)
                {
                    tmp = CSM.NORMAL_CURRENCY_SYMBOL + tmp;
                }
                else
                {
                    tmp = tmp + CSM.NORMAL_CURRENCY_SYMBOL;
                }
                return tmp;
            }
        }
        public decimal PENDING_AFTE_PAYMENT { get; set; }
        public string CASH_REG { get; set; }
        public decimal CASH_REG_AMT { get; set; }
        public decimal CHEQUE_AMT { get; set; }
        public string CHEQUE_NO { get; set; }
        public string CHEQUE_BANK_BRANCH { get; set; }
        public long CHEQUE_BANK_AC { get; set; }
        public DateTime CHEQUE_DATE { get; set; }
        public decimal TRANSFER_AMT { get; set; }
        public string TRANSFER_BANK_BRANCH { get; set; }
        public long TRANSFER_BANK_AC { get; set; }
        public DateTime TRANSFER_DATE { get; set; }
        public decimal FINANCAL_AMT { get; set; }
        public long FINACIAL_AC { get; set; }
        public int DISCOUNT_FLAT { get; set; }
        public int DISCOUNT_PERCENT { get; set; }
        public int TOTAL_PAYMENT_MODES { get; set; }
        public decimal CURRENT_PAYMENT { get; set; }
        public string NOTE { get; set; }
        public int SLNO { get; set; }
        public long COMPANY_ID { get; set; }

        public long BUSINESS_LOCATION_ID { get; set; }
        public long SUPP_ID { get; set; }
        public long CASH_REG_ID { get; set; }

        public DateTime? SUPPLIER_DATE { get; set; }
        public int? PAYMENT_TERMS { get; set; }
        public decimal? TOTAL_AMT { get; set; }
        public string SUPPLIER { get; set; }
        public decimal? ROUND_OFF_ADJUSTMENTAMT { get; set; }
        public string RECEIVED_ITEM_ENTRY_NO { get; set; }
        public DateTime? RECEIVED_ITEM_ON_DATE { get; set; }
        public string SUPPLIER_INVOICE_NO { get; set; }
        public decimal? TOTAL_TAX_AMT { get; set; }
        public string SUPPLIER_EMAIL { get; set; }
        public string SUPPLIER_MOBILE { get; set; }
        public bool ischecck { get; set; }
        private string error = string.Empty;
        public string Error
        {
            get { return error; }
            set { error = value; }
        }
        public ObservableCollection<SuppPaymentModel> getAllSupplier { get; set; }

        public string this[string columnName]
        {
            get
            {

                error = string.Empty;

                if (columnName == "SUPP" && string.IsNullOrWhiteSpace(SUPP))
                {
                    error = "SUPP is required!";

                }
                if (columnName == "SUPP_EMAIL" && string.IsNullOrWhiteSpace(SUPP_EMAIL))
                {
                    error = "SUPP_EMAIL is required!";

                }
                //if (columnName == "SUPP_SMS" && string.IsNullOrWhiteSpace(SUPP_SMS))
                //{
                //    error = "SUPP_SMS is required!";

                //}

                switch (columnName)
                {
                    //case "SELECTED_AMT": if ((SELECTED_AMT <= 0)) error = "SELECTED_AMT is required!"; break;
                    case "TOTAL_PANDING": if ((TOTAL_PANDING <= 0)) error = "TOTAL_PANDING can not be blank"; break;
                        //case "PENDING_AFTE_PAYMENT": if ((PENDING_AFTE_PAYMENT <= 0)) error = "PENDING AFTE PAYMENTcan not be blank"; break;
                        //case "TOTAL_PAYMENT_MODES": if ((TOTAL_PAYMENT_MODES <= 0)) error = "PENDING AFTE PAYMENTcan not be blank"; break;
                };
                return error;
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
    }
}
