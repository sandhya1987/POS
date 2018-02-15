using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class Payment
    {

        public InvoiceInfo _InvoiceInfo { get; set; }

        public PaymentDetails _PaymentDetails { get; set; }
        public string Bussiness_Location { get; set; }
        public string Customer_Id { get; set; }
        public string Customer_Email { get; set; }
        public string Customer_Mobile_No { get; set; }
        public string Is_Mail { get; set; }
        public string Current_Payble_Amt { get; set; }
        public string Other_Pending_Amount { get; set; }
        public string Total_Pending_Amount { get; set; }
        public string Pending_Amount { get; set; }
        public string Total_Rec_Amt { get; set; }
        public string Returnable_Amt { get; set; }
    }

    public class InvoiceInfo
    {
        public string SELECT_PAYMENT { get; set; }
        public string Invoice_Number { get; set; }
        public string Invoice_Date { get; set; }
        public string Pending_Amount { get; set; }
        public string Adjusted_Amount { get; set; }
        public string Penalty_Amount { get; set; }
        public string Actual_Penalty_Amount { get; set; }
        public string Adjusted_Penalty_Amount { get; set; }
        public string Adjust_Balance { get; set; }
    }
    public class PaymentDetails
    {

    }
    public class RecPayModel : IDataErrorInfo
    {
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

                error = string.Empty;

                if (columnName == "BUSINESS_LOCATION" && string.IsNullOrWhiteSpace(BUSINESS_LOCATION))
                {
                    error = "BUSINESS_LOCATION is required!";

                }
                if (columnName == "CUSTOMER" && string.IsNullOrWhiteSpace(CUSTOMER))
                {
                    error = "CUSTOMER is required!";

                }
                if (columnName == "CUSTOMER_EMAIL" && string.IsNullOrWhiteSpace(CUSTOMER_EMAIL))
                {
                    error = "CUSTOMER_EMAIL is required!";

                }
                if (columnName == "CUSTOMER_CONTACT_NO" && string.IsNullOrWhiteSpace(CUSTOMER_CONTACT_NO))
                {
                    error = "CUSTOMER_CONTACT_NO is required!";

                }
                switch (columnName)
                {
                    case "TOTAL_SELECTED_PAY_AMT": if ((TOTAL_SELECTED_PAY_AMT <= 0)) error = "TOTAL_SELECTED_PAY_AMT is required!"; break;
                    case "PENDING_AMT": if ((PENDING_AMT <= 0)) error = "PENDING_AMT can not be blank"; break;
                    case "TOTAL_REC_AMT": if ((TOTAL_REC_AMT <= 0)) error = "TOTAL_REC_AMT can not be blank"; break;
                    case "RETURNABLE_AMT": if ((RETURNABLE_AMT <= 0)) error = "RETURNABLE AMT can not be blank"; break;
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
