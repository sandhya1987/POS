using InvoicePOSDATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class DepartmentModel
    {
        public long? DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public long COMPANY_ID { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public long? CREATED_BY { get; set; }
        public bool? STATUS { get; set; }
        public bool? IS_DELETE { get; set; }
        public long? SORT_INDEX { get; set; }
        
    }
    public class DeptModel
    {
        public long? DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }

    }
}