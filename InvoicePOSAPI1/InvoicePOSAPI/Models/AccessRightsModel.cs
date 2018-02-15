using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoicePOSAPI.Models
{
    public class AccessRightsModel
    {
        public List<DeptModel> AllDeperment { get; set; }
        public List<DesgModel> AllDesignation { get; set; }
    }
    public class UserAccessModel
    {
        public bool? ACTION_CREATE { get; set; }
        public bool? EDIT { get; set; }
        public bool? ACTION_DELETE { get; set; }
        public bool? ACTION_VIEW { get; set; }
        public bool? VERIFICATION { get; set; }
        public bool? NOTIFICATION { get; set; }
        public bool? MESSAGE { get; set; }
        public bool? APPROVE { get; set; }
        public bool? MAILBACK { get; set; }
        public long ROLE_ID { get; set; }
        public long MODULE_ID { get; set; }
        public long? ID { get; set; }
        public string MODULE_NAME { get; set; }
        public bool? EXPT_TO_EXCEL { get; set; }
        public bool? IMORT_TO_EXCEL { get; set; }
        public long? Company_Id { get; set; }
        public long? User_Id { get; set; }
    }
}