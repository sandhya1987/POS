using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class CashRegTransModel
    {
        public DateTime? TRANSACTION_DATE { get; set; }
        public string TRANSACTION_TYPE { get; set; }
        public string CURRENCY_TYPE { get; set; }
        public decimal? CREDIT_AMOUNT { get; set; }
        public decimal? DEBIT_AMOUNT { get; set; }
        public string CREATOR { get; set; }
        public string NARRATION_TEXT { get; set; }
        public string TO_CASH_REGISTER { get; set; }
        public string FROM_CASH_REGISTER { get; set; }
        public long TRANSFER_ID { get; set; }
        public string INVOICE_NO { get; set; }
    }
}