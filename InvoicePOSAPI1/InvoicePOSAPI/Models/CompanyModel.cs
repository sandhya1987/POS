using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class CompanyModel
    {
        public int COMPANY_ID { get; set; }
        public string NAME { get; set; }
        public string SHOPNAME { get; set; }
        public string PREFIX { get; set; }
        public string PREFIX_NUM { get; set; }
        public string TIN_NUMBER { get; set; }
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string COUNTRY { get; set; }
        public string PIN { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string EMAIL { get; set; }
        public string WEBSITE { get; set; }
        public decimal? DEFAULT_TAX_RATE { get; set; }
        public Nullable<bool> IS_WARNED_FOR_NEGATIVE_STOCK { get; set; }
        public Nullable<bool> IS_WARNED_FOR_LESS_SALES_PRICE { get; set; }
        public string TAX_PRINTED_DESCRIPTION { get; set; }
        public Nullable<short> FIRST_DAY_OF_FINANCIAL_YEAR { get; set; }
        public Nullable<short> FIRST_MONTH_OF_FINANCIAL_YEAR { get; set; }
        public long? BANK_ID { get; set; }
        public string IMAGE_PATH { get; set; }

        public string BANK_NAME { get; set; }
        public string IFSC_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        public string ACCOUNT_NUMBER { get; set; }
        public string BANK_ADDRESS_1 { get; set; }
        public string BANK_ADDRESS_2 { get; set; }
        public string BANK_CITY { get; set; }
        public string BANK_STATE { get; set; }
        public string BANK_PIN_CODE { get; set; }
        public string BANK_PHONE_NUMBER { get; set; }
        public string BANK_CODE { get; set; }
        public long BANK_ACCOUNT_ID { get; set; }
       // public Bank_account _Bank_account { get; set; }
        
        // 9874169882 Chandra Sekhar KPC
    }
    public class BankModel
    {
        public long BANK_ID { get; set; }
        public Nullable<long> COMPANY_ID { get; set; }
        public string BANK_NAME { get; set; }
        public string IFSC_CODE { get; set; }
        public Nullable<bool> IS_DELETED { get; set; }
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string PIN_CODE { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string FAX_NUMBER { get; set; }
        public string EMAIL { get; set; }
        public string WEBSITE { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public Nullable<int> ACCOUNT_NUMBER { get; set; }
        public string BRANCH_NAME { get; set; }
        public string ACCOUNT_DESCRIPTION { get; set; }
        public string ACCOUNT_SEARCH_CODE { get; set; }
        public string BANK_CODE { get; set; }
        public string COUNTRY { get; set; }
    }
    public class BankAccountModel
    {
        public long? BANK_ACCOUNT_ID { get; set; }
        public long? BANK_ID { get; set; }
        public long? COMPANY_ID { get; set; }
        public string ACCOUNT_NUMBER { get; set; }
        public string BRANCH_NAME { get; set; }
        public string ACCOUNT_DESCRIPTION { get; set; }
        public string ACCOUNT_SEARCH_CODE { get; set; }
        public bool? IS_DELETE { get; set; }
        public string PRINTING_FORMAT { get; set; }
    }
}