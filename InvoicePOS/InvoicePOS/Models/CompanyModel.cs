using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace InvoicePOS.Models
{
    
    public class CompanyModel : INotifyPropertyChanged, IDataErrorInfo
    {
        
        public string EMAIL { get; set; }
        
        public string NAME { get; set; }

        public int COMPANY_ID { get; set; }

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

        public string IMAGE_PATH { get; set; }
        public string WEBSITE { get; set; }
        public decimal DEFAULT_TAX_RATE { get; set; }
        public bool IS_WARNED_FOR_NEGATIVE_STOCK { get; set; }
        public bool IS_WARNED_FOR_LESS_SALES_PRICE { get; set; }
        public string TAX_PRINTED_DESCRIPTION { get; set; }
        public short FRIST_DAY_OF_FINANCIAL_YEAR { get; set; }
        public short FIRST_MONTH_OF_FINANCIAL_YEAR { get; set; }
     //   public long BANK_ID { get; set; }

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


        



        private System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");


        private string error = string.Empty;
        public string Error
        {
            get { return error; }
             set {  error=value; }
        }
        public string this[string columnName]
        {
            get
            {
                // 
               
                error = string.Empty;
                //switch (columnName)
                //{
                //    case "NAME": if ((NAME == "")) error = "Code is required!"; break;
                //};
                if (columnName == "NAME" && string.IsNullOrWhiteSpace(NAME))
                {
                    error = "Name is required!";
                   
                }
                if (columnName == "PREFIX_NUM" && string.IsNullOrWhiteSpace(PREFIX_NUM))
                {
                    error = "Prefix Number  is required!";
                   
                }
                
                if (columnName == "SHOPNAME" && string.IsNullOrWhiteSpace(SHOPNAME))
                {
                    error = "ShopName name is required!";

                }
                if (EMAIL != null)
                {

                    if (columnName == "EMAIL" && !regex.IsMatch(this.EMAIL))
                    {
                        error = "Email Not Valid!";
                    }
                }

                if (columnName == "EMAIL" && string.IsNullOrWhiteSpace(EMAIL))
                {
                    error = "Email  is required!";

                }
                
                if (columnName == "TIN_NUMBER" && string.IsNullOrWhiteSpace(TIN_NUMBER))
                {
                    error = "Tin Number  is required!";
                   
                }
                if (columnName == "ADDRESS_1" && string.IsNullOrWhiteSpace(ADDRESS_1))
                {
                    error = "Address 1  is required!";
                   
                }
                if (columnName == "CITY" && string.IsNullOrWhiteSpace(CITY))
                {
                    error = "City name is required!";
                   
                }
                if (columnName == "STATE" && string.IsNullOrWhiteSpace(STATE))
                {
                    error = "State name is required!";
                   
                }
                if (columnName == "PIN" && string.IsNullOrWhiteSpace(PIN))
                {
                    error = "Pin name is required!";

                }
                if (columnName == "BANK_NAME" && string.IsNullOrWhiteSpace(BANK_NAME))
                {
                    error = "Bank Name is required!";

                }

                //if (columnName == "ACCOUNT_NUMBER")
                //{
                //    error = "Account Number is required!";

                //}
                if (columnName == "BANK_CODE" && string.IsNullOrWhiteSpace(BANK_CODE))
                {
                    error = "Ifs Code is required!";

                }
                if (columnName == "BRANCH_NAME" && string.IsNullOrWhiteSpace(BRANCH_NAME))
                {
                    error = "Branch name is required!";

                }
                // error = string.Empty;
                return error;

            }

        }


        //public string Error
        //{
        //    get { throw new NotImplementedException(); }
        //}


        //public string this[string propertyName]
        //{
        //    get
        //    {
        //        string result = string.Empty;
        //        switch (propertyName)
        //        {
        //            case "NAME": if (NAME == "" || string.IsNullOrWhiteSpace(NAME)) result = "Famaily Name is required!"; break;
        //            //case "First_Name": if (First_Name == "") result = "FirstName is required!"; break;
        //            //case "Phone": if (Phone == "" || IsValidNumbers) result = "Please Enter Phone No."; break;
        //            //case "Phone_ex": if (Phone_ex == "" || IsValidNumbers) result = "Please Enter Phone Ex. No."; break;
        //            //case "Mobile": if (Mobile == "" || IsValidNumbers) result = "Please Enter Mobile No."; break;
        //            //case "Fax": if (Fax == "" || IsValidNumbers) result = "Please Enter Fax No."; break;
        //            case "EMAIL": if (!regex.IsMatch(this.EMAIL)) result = "Please Enter Mail."; break;
        //            //case "Pin": if (Pin == "" || IsValidNumbers) result = "Please Enter Postal Code"; break;
        //            // case "Title": if (Title ==0 ) result = "Please select Title."; break;
        //        };
        //        return result;
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
