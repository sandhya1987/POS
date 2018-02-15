using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace InvoicePOS.Models
{
    public class GetReceivePayment { }
    public class GetReceiveAmt : IDataErrorInfo
    {
        public string SELECT_PAYMENT { get; set; }
        public string Invoice_Number { get; set; }
        public string INVOICE_NO { get; set; }
        public string Invoice_Date { get; set; }
        public string Pending_Amount { get; set; }
        public string Adjusted_Amount { get; set; }
        public string Penalty_Amount { get; set; }
        public string Actual_Penalty_Amount { get; set; }
        public string Adjusted_Penalty_Amount { get; set; }
        public string Adjust_Balance { get; set; }
        public int COMPANY_ID { get; set; }
        public long RECEIVE_PAYMENT_ID { get; set; }
        public string RECEIVE_PAY_NO { get; set; }
        public long BUSINESS_LOCATION_ID { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public DateTime DATE { get; set; }
        public long CUSTOMER_ID { get; set; }
        public string CUSTOMER { get; set; }
        public string CUSTOMER_EMAIL { get; set; }
        public bool IS_EMAIL_SEND { get; set; }
        public string CUSTOMER_CONTACT_NO { get; set; }
        public decimal OTHER_PAY_AMT { get; set; }
        public decimal CURRENT_PAY_AMT { get; set; }
        public decimal TOTAL_SELECTED_PAY_AMT { get; set; }
        public decimal PENDING_AMT { get; set; }
        public decimal TOTAL_REC_AMT { get; set; }
        public decimal TOTAL_REC_AMT1 { get; set; }
        public decimal RETURNABLE_AMT { get; set; }
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
                throw new NotImplementedException();
            }
        }
    }
}
