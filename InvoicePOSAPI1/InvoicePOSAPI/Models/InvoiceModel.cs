using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class InvoiceModel
    {
        public string INVOICE_NO { get; set; }
        public string CUSTOMER { get; set; }
        public string BUSS_LOC { get; set; }
        public int? CUSTOMER_ID { get; set; }
        public decimal? AVAILABLE_CREDIT_LIMIT { get; set; }
        public string SALES_EXECUTIVE { get; set; }
        public long? SALES_EXECUTIVE_ID { get; set; }
        public string CUSTOMER_EMAIL { get; set; }
        public string CUSTOMER_MOBILE_NO { get; set; }
        public decimal? BEFORE_ROUNDOFF { get; set; }
        public int? ROUNDOFF_AMOUNT { get; set; }
        public int? TOTAL_AMOUNT { get; set; }
        public int? QUANTITY_TOTAL { get; set; }
        public int? NUMBER_OF_ITEM { get; set; }
        public decimal? DISCOUNT_INCLUDED { get; set; }
        public decimal? TAX_INCLUDED { get; set; }
        public string CASH_REG { get; set; }
        public decimal? CASH_AMOUNT { get; set; }
        public decimal? CHEQUE_AMOUNT { get; set; }
        public string CHEQUE_NO { get; set; }
        public string CHEQUE_BANK_BRANCH { get; set; }
        public string CHEQUE_DATE { get; set; }
        public long? CHEQUE_BANK_AC { get; set; }
        public bool? IS_CHEQUE_PRINT { get; set; }
        public string CHEQUE_PRINT { get; set; }
        public decimal? TRANSFER_AMOUNT { get; set; }
        public string TRANSFER_BRANCH { get; set; }
        public long? TRANSFER_BANK_AC { get; set; }
        public string TRANSFER_DATE { get; set; }
        public decimal? FINANCIAL_AMOUNT { get; set; }
        public long? FINANCIAL_AC_NO { get; set; }
        public decimal? DISCOUNT_FLAT { get; set; }
        public decimal? DISCOUNT_PERCENT { get; set; }
        public decimal? RECIVED_AMOUNT { get; set; }
        public decimal? RETURNABLE_AMOUNT { get; set; }
        public decimal? COMMISION_EXPENSE { get; set; }
        public decimal? PENDING_AMOUNT { get; set; }
        public string NOTE { get; set; }
        public bool? IS_GOODS_DELIVERED { get; set; }
        public bool IS_PRINT { get; set; }
        public bool IS_SAVE_RETURNABLE_AMOUNT { get; set; }
        public int COMPANY_ID { get; set; }
        public DateTime FINAL_INVOICE_DATE { get; set; }
        public ObservableCollection<ItemModel> SelectedItem { get; set; }
        public DateTime? INVOICE_DATE { get; set; }
        public decimal? TOTAL_TAX { get; set; }
        public long INVOICE_ID { get; set; }
        //public DateTime STARTDATE { get; set; }
        //public DateTime ENDDATE { get; set; }
    }
}