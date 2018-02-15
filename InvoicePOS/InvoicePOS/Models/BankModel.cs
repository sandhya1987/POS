using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class BankModel : IDataErrorInfo
    {
        private Regex numerciRegex = new Regex("[^0-9.-]+");
        private Regex emailRegex = new Regex(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$");

        public long BANK_ID { get; set; }
        public long COMPANY_ID { get; set; }
        public string BANK_NAME { get; set; }
        public string IFSC_CODE { get; set; }
        public bool IS_DELETED { get; set; }
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string COUNTRY { get; set; }
        public string PIN_CODE { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string FAX_NUMBER { get; set; }
        public string EMAIL { get; set; }
        public string WEBSITE { get; set; }
        public int SLNO { get; set; }
        public string BANK_CODE { get; set; }
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

                if (columnName == "BANK_NAME" && string.IsNullOrWhiteSpace(BANK_NAME))
                {
                    error = "BANK_NAME is required!";

                }
                if (columnName == "IFSC_CODE" && string.IsNullOrWhiteSpace(IFSC_CODE))
                {
                    error = "IFSC_CODE is required!";

                }
                if (columnName == "PIN_CODE" && string.IsNullOrWhiteSpace(PIN_CODE))
                {
                    error = "BARCODE is required!";

                }
                if (columnName == "MOBILE_NUMBER" && string.IsNullOrWhiteSpace(MOBILE_NUMBER))
                {
                    error = "MOBILE_NUMBER is required!";

                }

                if (columnName == "WEBSITE" && string.IsNullOrWhiteSpace(WEBSITE))
                {
                    error = "WEBSITE is required!";

                }


                return error;
            }
        }

    }
    public class BankAccountModel : IDataErrorInfo
    {
        public long BANK_ACCOUNT_ID { get; set; }
        public long BANK_ID { get; set; }
        public int COMPANY_ID { get; set; }
        public string ACCOUNT_NUMBER { get; set; }
        public string BRANCH_NAME { get; set; }
        public string ACCOUNT_DESCRIPTION { get; set; }
        public string ACCOUNT_SEARCH_CODE { get; set; }
        public bool IS_DELETE { get; set; }
        public string PRINTING_FORMAT { get; set; }
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

                if (columnName == "ACCOUNT_NUMBER" && string.IsNullOrWhiteSpace(ACCOUNT_NUMBER))
                {
                    error = "ACCOUNT_NUMBER is required!";

                }
                if (columnName == "ACCOUNT_SEARCH_CODE" && string.IsNullOrWhiteSpace(ACCOUNT_SEARCH_CODE))
                {
                    error = "ACCOUNT_SEARCH_CODE is required!";

                }
                if (columnName == "BRANCH_NAME" && string.IsNullOrWhiteSpace(BRANCH_NAME))
                {
                    error = "BRANCH_NAME is required!";

                }
                return error;
            }
        }
    }
}
