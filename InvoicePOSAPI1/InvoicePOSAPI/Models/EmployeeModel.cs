using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class EmployeeModel
    {
        public long? EMPLOYEE_ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string MARITAL_STATUS { get; set; }
        public string GENDER { get; set; }
        public Nullable<System.DateTime> DATE_OF_JOIN { get; set; }
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
        public string IMAGE { get; set; }
        public bool? IS_REQUEST_VAI_SMS { get; set; }
        public bool? IS_APPROVE_ACCESS_VAI_SMS { get; set; }
        public bool? IS_NOT_AN_EMPLOYEE { get; set; }
        public bool? IS_APPOINTMENT { get; set; }
        public bool? IS_ATTACHED_INVOICE { get; set; }
        public string WORKING_SHIFT { get; set; }
        public decimal? MAX_SPOT_DISCOUNT { get; set; }
        public long? COMPANY_ID { get; set; }
        public decimal? SALES_PERCENT { get; set; }
        public string COMMISSION_QUICK_POSITION { get; set; }
    }
}