using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class EmployeeModel : IDataErrorInfo
    {
        public long EMPLOYEE_ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public DateTime DOB { get; set; }
        public string MARITAL_STATUS { get; set; }
        public string GENDER { get; set; }
        public DateTime DATE_OF_JOIN { get; set; }
        public string BUSINESS_LOCATION { get; set; }
        public string DEPARTMENT { get; set; }
        public string EMPLOYEE_DESIGNATION { get; set; }
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string PIN_NO { get; set; }
        public string TELEPHONE_NO { get; set; }
        public string FAX_NO { get; set; }
        public string MOBILE_NO { get; set; }
        public string EMAIL { get; set; }
        public string WEBSITE { get; set; }
        public string IMAGE_PATH { get; set; }
        public bool IS_REQUEST_VAI_SMS { get; set; }
        public bool IS_APPROVE_ACCESS_VAI_SMS { get; set; }
        public bool IS_NOT_AN_EMPLOYEE { get; set; }
        public bool IS_APPOINTMENT { get; set; }
        public bool IS_ATTACHED_INVOICE { get; set; }
        public string WORKING_SHIFT { get; set; }
        public decimal MAX_SPOT_DISCOUNT { get; set; }
        public decimal SALES_PERCENT { get; set; }
        public string COMMISSION_QUICK_POSITION { get; set; }
        public long COMPANY_ID { get; set; }
        public int SLNO { get; set; }
        //public bool IS_REQUEST_VAI_SMS { get; set; }
        
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

                if (columnName == "FIRST_NAME" && string.IsNullOrWhiteSpace(FIRST_NAME))
                {
                    error = "FIRST_NAME is required!";

                }
                if (columnName == "SEARCH_CODE" && string.IsNullOrWhiteSpace(SEARCH_CODE))
                {
                    error = "SEARCH_CODE is required!";

                }
                if (columnName == "EMPLOYEE_DESIGNATION" && string.IsNullOrWhiteSpace(EMPLOYEE_DESIGNATION))
                {
                    error = "EMPLOYEE_DESIGNATION is required!";

                }
                if (columnName == "DEPARTMENT" && string.IsNullOrWhiteSpace(DEPARTMENT))
                {
                    error = "DEPARTMENT is required!";

                }

                if (columnName == "ADDRESS_1" && string.IsNullOrWhiteSpace(ADDRESS_1))
                {
                    error = "ADDRESS_1 is required!";

                }
                if (columnName == "PIN_NO" && string.IsNullOrWhiteSpace(PIN_NO))
                {
                    error = "PIN_NO is required!";

                }
                switch (columnName)
                {
                    case "MAX_SPOT_DISCOUNT": if ((MAX_SPOT_DISCOUNT <= 0)) error = "MAX_SPOT_DISCOUNT is required!"; break;
                    case "SALES_PERCENT": if ((SALES_PERCENT <= 0)) error = "COMMISSION_QUICK_POSITION can not be blank"; break;
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
