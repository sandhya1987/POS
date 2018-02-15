using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class InvoiceModel
    {
        public int INVOICE_ID { get; set; }
        public string INVOICE_NO { get; set; }
        public string CUSTOMER { get; set; }
        public long CUSTOMER_ID { get; set; }
        public string AVAILABLE_CREDIT_LIMIT { get; set; }
        public string SALES_EXECUTIVE { get; set; }
        public long SALES_EXECUTIVE_ID { get; set; }
        public string CUSTOMER_EMAIL { get; set; }
        public string CUSTOMER_MOBILE_NO { get; set; }
        public decimal BEFORE_ROUNDOFF { get; set; }
        public int ROUNDOFF_AMOUNT { get; set; }
        public int TOTAL_AMOUNT { get; set; }
        public int QUANTITY_TOTAL { get; set; }
        public int NUMBER_OF_ITEM { get; set; }
        public decimal DISCOUNT_INCLUDED { get; set; }
        public decimal TAX_INCLUDED { get; set; }
        public string CASH_REG { get; set; }
        public decimal CASH_AMOUNT { get; set; }
        public decimal CHEQUE_AMOUNT { get; set; }
        public string CHEQUE_NO { get; set; }
        public string CHEQUE_BANK_BRANCH { get; set; }
        public string CHEQUE_DATE { get; set; }
        public long CHEQUE_BANK_AC { get; set; }
        public bool IS_CHEQUE_PRINT { get; set; }
        public string CHEQUE_PRINT { get; set; }
        public decimal TRANSFER_AMOUNT { get; set; }
        public string TRANSFER_BRANCH { get; set; }
        public long TRANSFER_BANK_AC { get; set; }
        public string TRANSFER_DATE { get; set; }
        public decimal FINANCIAL_AMOUNT { get; set; }
        public long FINANCIAL_AC_NO { get; set; }
        public decimal DISCOUNT_FLAT { get; set; }
        public decimal DISCOUNT_PERCENT { get; set; }
        public decimal RECIVED_AMOUNT { get; set; }
        public decimal RETURNABLE_AMOUNT { get; set; }
        public string COMMISION_EXPENSE { get; set; }
        public decimal PENDING_AMOUNT { get; set; }
        public string NOTE { get; set; }
        public bool IS_GOODS_DELIVERED { get; set; }
        public bool IS_PRINT { get; set; }
        public bool SAVE_PRINT { get; set; }
        public bool GENERATE_PRINT { get; set; }
        public bool IS_SAVE_RETURNABLE_AMOUNT { get; set; }
        public string INVOICE_DATE { get; set; }
        public DateTime STARTDATE { get; set; }
        public DateTime ENDDATE { get; set; }
        public int ITEM_ID { get; set; }
        public bool IS_ACTIVE { get; set; }
        public decimal ADVANCED_AMOUNT { get; set; }
        public decimal CUSTOMERPENDING_AMOUNT { get; set; }
        public decimal CREDIT_AMOUNT { get; set; }
        public decimal? TOTAL_TAX { get; set; }
        public decimal? CreditsLimits { get; set; }
        //public string CASH_REG { get; set; }
        public ObservableCollection<ItemModel> SelectedItem { get; set; }
    }
    public class GetInvoiceModel
    {
        public long INVOICE_ID { get; set; }
        public string AVAILABLE_CREDIT_LIMIT { get; set; }
        public decimal BEFORE_ROUNDOFF { get; set; }
        public string COMMISION_EXPENSE { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public string GODOWN { get; set; }
        public string CUSTOMER { get; set; }
        public string CUSTOMER_EMAIL { get; set; }
        public string CUSTOMER_MOBILE_NO { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string DISCOUNT_FLAT { get; set; }
        public string INVOICE_NO { get; set; }
        public string NUMBER_OF_ITEM { get; set; }
        public string NOTE { get; set; }
        public string TOTAL_AMOUNT { get; set; }
        public string TAX_INCLUDED { get; set; }



        public string ITEM_ID { get; set; }
        public string PENDING_AMOUNT { get; set; }
        public string QUANTITY_TOTAL { get; set; }
        public string RECIVED_AMOUNT { get; set; }
        public string RETURNABLE_AMOUNT { get; set; }
        public string ROUNDOFF_AMOUNT { get; set; }
        public string SALES_EXECUTIVE { get; set; }
        public string SALES_EXECUTIVE_ID { get; set; }
        public DateTime INVOICE_DATE { get; set; }
        public decimal? TOTAL_TAX { get; set; }
        public bool IS_ACTIVE { get; set; }
        public DateTime STARTDATE { get; set; }
        public DateTime ENDDATE { get; set; }
        public string CASH_REG { get; set; }

    }

}
