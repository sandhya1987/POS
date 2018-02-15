using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class CustomerModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private Regex numerciRegex = new Regex("[^0-9.-]+");
        private Regex emailRegex = new Regex(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$");
        public CustomerModel()
        {


        }
        public int CUSTOMER_ID { get; set; }
        public long COMPANY_ID { get; set; }
        public string CUSTOMER_NUMBER { get; set; }
        public string NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public string VAT_NUMBER { get; set; }
        public string CST_NUMBER { get; set; }

        public string TIN { get; set; }
        public string PAN { get; set; }
        public long BUSINESS_LOCATION_ID { get; set; }
        public string BUSINESS_LOCATION { get; set; }

        public bool IS_ACTIVE { get; set; }
        public string BILLING_TO_NAME { get; set; }
        public string BILLING_ADDRESS1 { get; set; }
        public string BILLING_ADDRESS2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string POSTAL_CODE { get; set; }
        public string COUNTRY { get; set; }
        public string MOBILE_NO { get; set; }
        public int SLNO { get; set; }
        public string TELEPHON_NUMBER { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public bool SAMEBILLINGANDSHIPPING_ADDRESS { get; set; }
        public bool IS_ENROLLED_FOR_LOYALITY { get; set; }
        public string NOTES { get; set; }
        public string LOYALTY_NO { get; set; }
        public string SHIPPING_ADDRESS1 { get; set; }
        public string SHIPPING_ADDRESS2 { get; set; }
        public string SHIPPING_CITY { get; set; }
        public string SHIPPING_STATE { get; set; }
        public string SHIPPING_POSTAL_CODE { get; set; }
        public string SHIPPING_COUNTRY { get; set; }
        public string SHIPPING_MOBILE_NO { get; set; }
        public string SHIPPING_TELEPHON_NUMBER { get; set; }
        public string SHIPPING_EMAIL_ADDRESS { get; set; }
        public string REFERRED_BY { get; set; }
        public decimal OPENING_AMT { get; set; }
        public string SEARCH_CUS { get; set; }
        public string IMAGE_PATH { get; set; }
        public decimal CURRENT_OPENING_BALANCE { get; set; }
        public bool DEFAULT_CREIT_LIMIT { get; set; }
        public string CUSTOMER_GROUP { get; set; }
        public string DISPLAY_CUS { get; set; }
        public string DISPLAY_CUS_LAST { get; set; }
        public string FULL_NAME { get; set; }


        public long OPENING_BALANCE_ID { get; set; }
        public DateTime DATE { get; set; }
        public long BAL_TYPE_ID { get; set; }
        public string BAL_TYPE_VALUE { get; set; }
        public decimal CLOSING_AMT { get; set; }
        public DateTime SYSTEM_DATE { get; set; }
        public decimal credit_Limits { get; set; }
        public decimal? credit_Limits1 { get; set; }
        private System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private System.Text.RegularExpressions.Regex No = new System.Text.RegularExpressions.Regex("[^0-9]");
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

                if (columnName == "NAME" && string.IsNullOrWhiteSpace(NAME))
                {
                    error = "Godown Name is required!";

                }
                if (columnName == "SEARCH_CODE" && string.IsNullOrWhiteSpace(SEARCH_CODE))
                {
                    error = "Godown Description is required!";

                }
                if (columnName == "VAT_NUMBER" && string.IsNullOrWhiteSpace(VAT_NUMBER))
                {
                    error = "VAT_NUMBER  is required!";

                }
                if (columnName == "CST_NUMBER" && string.IsNullOrWhiteSpace(CST_NUMBER))
                {
                    error = "CST_NUMBER  is required!";

                }


                if (columnName == "PAN" && string.IsNullOrWhiteSpace(PAN))
                {
                    error = "PAN  is required!";

                }
                if (columnName == "TIN" && string.IsNullOrWhiteSpace(TIN))
                {
                    error = "TIN  is required!";

                }

                if (columnName == "SHIPPING_ADDRESS1" && string.IsNullOrWhiteSpace(SHIPPING_ADDRESS1))
                {
                    error = "SHIPPING_ADDRESS1 is required!";

                }
                if (columnName == "SHIPPING_ADDRESS2" && string.IsNullOrWhiteSpace(SHIPPING_ADDRESS2))
                {
                    error = "SHIPPING_ADDRESS2 is required!";

                }
                if (columnName == "SHIPPING_CITY" && string.IsNullOrWhiteSpace(SHIPPING_CITY))
                {
                    error = "Godown Description is required!";

                }
                if (columnName == "SHIPPING_STATE" && string.IsNullOrWhiteSpace(SHIPPING_STATE))
                {
                    error = "SHIPPING_STATE is required!";

                }
                if (columnName == "SHIPPING_POSTAL_CODE" && string.IsNullOrWhiteSpace(SHIPPING_POSTAL_CODE))
                {
                    error = "SHIPPING_POSTAL_CODE is required!";

                }
                //if (columnName == "SHIPPING_TELEPHON_NUMBER" && No.IsMatch(SHIPPING_TELEPHON_NUMBER))
                //{
                //    error = "SHIPPING_TELEPHON_NUMBER is required!";

                //}
                //if (columnName == "SHIPPING_MOBILE_NO" && No.IsMatch(this.SHIPPING_MOBILE_NO))
                //{
                //    error = "SHIPPING_TELEPHON_NUMBER is required!";
                //}

                //if (columnName == "SHIPPING_MOBILE_NO" && string.IsNullOrWhiteSpace(SHIPPING_MOBILE_NO))
                //{

                //    error = "SHIPPING_TELEPHON_NUMBER is required!";

                //}
                //if (columnName == "SHIPPING_EMAIL_ADDRESS" && !regex.IsMatch(this.SHIPPING_EMAIL_ADDRESS))
                //{
                //    error = "SHIPPING_EMAIL_ADDRESS is required!";

                //}
                if (SHIPPING_EMAIL_ADDRESS != null)
                {
                    if (columnName == "SHIPPING_EMAIL_ADDRESS" && !regex.IsMatch(this.SHIPPING_EMAIL_ADDRESS))
                    {
                        error = "Email Not Valid!";
                    }
                }
                if (EMAIL_ADDRESS != null)
                {
                    if (columnName == "EMAIL_ADDRESS" && !regex.IsMatch(this.EMAIL_ADDRESS))
                    {
                        error = "Email Not Valid!";
                    }
                }
                if (columnName == "CUSTOMER_GROUP" && string.IsNullOrWhiteSpace(CUSTOMER_GROUP))
                {
                    error = "CUSTOMER_GROUP is required!";
                }
                if (columnName == "REFERRED_BY" && string.IsNullOrWhiteSpace(REFERRED_BY))
                {
                    error = "REFERRED_BY is required!";
                }

                //switch (columnName)
                //{
                //   // case "SHIPPING_MOBILE_NO": if (No.IsMatch(SHIPPING_MOBILE_NO)) error = "Tax paid is required!"; break;
                //    //case "SALES_PRICE": if ((SALES_PRICE <= 0)) error = "Sales Price can not be blank"; break;
                //    //case "TAX_COLLECTED": if ((TAX_COLLECTED <= 0)) error = "Tax Collected can not be blank"; break;
                //    //case "OPN_QNT": if ((OPN_QNT <= 0)) error = "Opening Qnt can not be blank"; break;
                //    //case "MRP": if ((MRP <= 0)) error = "MRP can not be blank"; break;
                //};
                return error;
            }
        }



        private bool IsValidNumbers
        {
            get
            {
                if (SHIPPING_MOBILE_NO != null)
                {
                    return numerciRegex.IsMatch(SHIPPING_MOBILE_NO);
                }
                if (SHIPPING_TELEPHON_NUMBER != null)
                {
                    return numerciRegex.IsMatch(SHIPPING_TELEPHON_NUMBER);
                }
                //if (Mobile != null)
                //{
                //    return numerciRegex.IsMatch(Mobile);
                //}
                //if (Fax != null)
                //{
                //    return numerciRegex.IsMatch(Fax);
                //}
                //if (Pin != null)
                //{
                //    return numerciRegex.IsMatch(Fax);
                //}
                else
                {
                    return false;
                }
            }
        }
        private bool IsValidEmailAddress
        {
            get
            {
                if (SHIPPING_EMAIL_ADDRESS != null)
                {
                    return emailRegex.IsMatch(SHIPPING_EMAIL_ADDRESS);
                }
                else
                {
                    return false;
                }
            }
        }






        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
