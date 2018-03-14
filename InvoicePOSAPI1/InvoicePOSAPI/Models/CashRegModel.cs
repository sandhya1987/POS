using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class CashRegModel
    {
        public long CASH_REGISTERID { get; set; }
        public long CASH_REGISTERID_FROM { get; set; }
        public long CASH_REGISTERID_TO { get; set; }
        public long TRANSFER_ID { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public long BUSINESS_LOCATION_ID { get; set; }
        public long? CASH_REG_NO { get; set; }
        public string CASH_REG_NAME { get; set; }
        public string CASH_REG_PREFIX { get; set; }
        public bool? ISADGUSTMENT { get; set; }
        public string LOGIN { get; set; }
        public string TRANSFER_CODE { get; set; }
        public bool? IS_MAIN_CASH { get; set; }
        public bool? IS_TRANSFER_CASH_REGISTER { get; set; }
        public string TO_TRAN_CASH_REGISTER { get; set; }
        public string FROM_TRAN_CASH_REGISTER { get; set; }
        public string TO_CASH_REGISTER { get; set; }
        public DateTime? TRANSFER_DATE { get; set; }
        public long? COMPANY_ID { get; set; }
        public int SLNO { get; set; }
        public decimal? CASH_AMOUNT { get; set; }
        public decimal CURRENT_CASH { get; set; }
        public decimal? CASH_TO_TRANSFER { get; set; }
        public decimal SUBMITTED_CASH { get; set; }
        public decimal CURRENT_REMAIN { get; set; }
        public string STATUS { get; set; }
        public string CHEQUE_NO { get; set; }
        public decimal CHEQUE_AMOUNT { get; set; }

    }
}