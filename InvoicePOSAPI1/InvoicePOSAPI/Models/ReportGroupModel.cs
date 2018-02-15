using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOSAPI.Models
{
    public class ReportGroupModel
    {
      public long? REPORT_GRP_ID { get; set; }
        public string GROUP_NAME { get; set; }
        public string  DESCRIPTION { get; set; }
        public DateTime?  DATE { get; set; }

        public long? REPORT_ID { get; set; }
        public bool? IS_DELETE { get; set; }
        public long? COMPANY_ID { get; set; }
        public long? CREATED_BY { get; set; }
        public long? SGROUP_CODE { get; set; }
        public string REPORT_NAME { get; set; }
        public long? REPORT_PARENT_ID { get; set; }
        public long? REPORT_CHILD_ID { get; set; }

    }
    public class ReportGroupHirarchyModel
    {
        public long? REPORT_ID { get; set; }
        public long? REPORT_GRP_ID { get; set; }
        public long? REPORT_PARENT_ID { get; set; }
        public long? REPORT_CHILD_ID { get; set; }
        public string REPORT_NAME { get; set; }
        public string GROUP_NAME { get; set; }
    }
        public class ReportAddModel
    {
        public string REPORT_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public long? REPORT_GROUP_ID { get; set; }
        public string DESCRIPTION_ADD { get; set; }
        public long REPORT_ADD_ID { get; set; }
    }
}
