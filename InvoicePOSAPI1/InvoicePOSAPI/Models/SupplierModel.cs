using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class SupplierModel
    {
        public long SUPPLIER_ID { get; set; }
        public long? COMPANY_ID { get; set; }
        public string SUPPLIER_CODE { get; set; }
        public string SUPPLIER_NAME { get; set; }
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
        public decimal? OPENING_BALANCE { get; set; }
        public bool? IS_PREFERRED_SUPPLIER { get; set; }
        public bool? IS_ACTIVE { get; set; }
        public int? PAYMENT_SETTLED { get; set; }
        public string IMAGE_PATH { get; set; }
        public string CONTACT_FRIST_NAME { get; set; }
        public string CONTACT_LAST_NAME { get; set; }
        public string CONTACT_TELEPHONE_NO { get; set; }
        public string CONTACT_FAX_NO { get; set; }
        public string CONTACT_MOBILE_NO { get; set; }
        public string CONTACT_WEBSITE { get; set; }
        public string CONTACT_EMAIL { get; set; }
    }
    public class SuppPaymentModel
    {
        public long SUPP_PAYMENT { get; set; }
        public string SUPP_pay_no { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public string SUPP { get; set; }
        public string SUPP_EMAIL { get; set; }
        public string SUPP_SMS { get; set; }
        public bool? IS_SEND_EMAIL { get; set; }
        public bool? IS_SEND_SMS { get; set; }
        public decimal? CURRENT_ADV_AMT { get; set; }
        public decimal? TOTAL_RIE_AMT { get; set; }
        public decimal? PENDING_AMT { get; set; }
        public string PAYMENT_NUMBER { get; set; }
        public DateTime? PAYMENT_DATE { get; set; }
        public decimal? CURRENT_PAYABLE_AMT { get; set; }
        public decimal? TOTAL_PANDING { get; set; }
        public decimal? SELECTED_AMT { get; set; }
        public decimal? PENDING_AFTE_PAYMENT { get; set; }
        public string CASH_REG { get; set; }
        public decimal? CASH_REG_AMT { get; set; }
        public decimal? CHEQUE_AMT { get; set; }
        public string CHEQUE_NO { get; set; }
        public string CHEQUE_BANK_BRANCH { get; set; }
        public string CHEQUE_BANK_AC { get; set; }
        public DateTime? CHEQUE_DATE { get; set; }
        public decimal? TRANSFER_AMT { get; set; }
        public string TRANSFER_BANK_BRANCH { get; set; }
        public long? TRANSFER_BANK_AC { get; set; }
        public DateTime? TRANSFER_DATE { get; set; }
        public decimal? FINANCAL_AMT { get; set; }
        public long? FINACIAL_AC { get; set; }
        public int? DISCOUNT_FLAT { get; set; }
        public int? DISCOUNT_PERCENT { get; set; }
        public int? TOTAL_PAYMENT_MODES { get; set; }
        public decimal? CURRENT_PAYMENT { get; set; }
        public string NOTE { get; set; }
        public bool? IS_PRINT_CHECK { get; set; }
        public long COMPANY_ID { get; set; }

        public long BUSINESS_LOCATION_ID { get; set; }
        public long? SUPP_ID { get; set; }
        public long? CASH_REG_ID { get; set; }
        public long? USER_ID { get; set; }


        public DateTime? SUPP_DATE { get; set; }
        public int? PAYMENT_TERMS { get; set; }
        public decimal? TOTAL_AMT { get; set; }
        public string SUPPLIER { get; set; }
        public decimal? ROUND_OFF_ADJUSTMENTAMT { get; set; }
        public string RECEIVED_ITEM_ENTRY_NO { get; set; }
        public DateTime? RECEIVED_ITEM_ON_DATE { get; set; }
        public string SUPPLIER_INVOICE_NO { get; set; }
        public decimal? TOTAL_TAX_AMT { get; set; }
        public int? SUPPLIER_ID { get; set; }
        public string SUPPLIER_EMAIL { get; set; }
        public string SUPPLIER_MOBILE { get; set; }
        public ObservableCollection<SuppPaymentModel> getAllSupplier { get; set; }
    }
}