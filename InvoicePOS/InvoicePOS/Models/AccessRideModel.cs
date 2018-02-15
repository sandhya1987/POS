using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class AccessRideModel
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
        public List<DepartmentModel> AllDeperment { get; set; }
        public List<DesignationModel> AllDesignation { get; set; }
    }

    public class UserAccessModel
    {
        public bool ACTION_CREATE { get; set; }
        public bool EDIT { get; set; }
        public bool ACTION_DELETE { get; set; }
        public bool ACTION_VIEW { get; set; }
        public bool VERIFICATION { get; set; }
        public bool NOTIFICATION { get; set; }
        public bool MESSAGE { get; set; }
        public bool APPROVE { get; set; }
        public bool MAILBACK { get; set; }
        public long ROLE_ID { get; set; }
        public long MODULE_ID { get; set; }
        public long ID { get; set; }
        public string MODULE_NAME { get; set; }
        public bool EXPT_TO_EXCEL { get; set; }
        public bool IMORT_TO_EXCEL { get; set; }
        public long Company_Id { get; set; }
        public long User_Id { get; set; }
    }

}
