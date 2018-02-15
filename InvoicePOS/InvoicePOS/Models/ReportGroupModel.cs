using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class ReportGroupModel  
    {



        readonly List<ReportGroupModel> _children = new List<ReportGroupModel>();
        public IList<ReportGroupModel> Children
        {
            get { return _children; }
        }


        public string REPORT_NAME { get; set; }
        public long? REPORT_PARENT_ID { get; set; }
        public long? REPORT_CHILD_ID { get; set; }

        public long REPORT_GRP_ID { get; set; }
        public string GROUP_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public DateTime DATE { get; set; }
        public long REPORT_ID { get; set; }
        public bool IS_DELETE { get; set; }

        public bool IsExpanded { get; set; }
        public long COMPANY_ID { get; set; }
        public long CREATED_BY { get; set; }
        public int SLNO { get; set; }


    }
    public class ReportAddModel : IDataErrorInfo
    {
        public string REPORT_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public long REPORT_GROUP_ID { get; set; }
        public string DESCRIPTION_ADD { get; set; }
        public long REPORT_ADD_ID { get; set; }
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

                if (columnName == "REPORT_NAME" && string.IsNullOrWhiteSpace(REPORT_NAME))
                {
                    error = "REPORT_NAME is required!";

                }
                if (columnName == "SEARCH_CODE" && string.IsNullOrWhiteSpace(SEARCH_CODE))
                {
                    error = "SEARCH_CODE is required!";

                }

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

    public class ReportGroupHirarchyModel
    {
        readonly List<ReportGroupHirarchyModel> _children = new List<ReportGroupHirarchyModel>();
        public IList<ReportGroupHirarchyModel> Children
        {
            get { return _children; }
        }
        public long? REPORT_ID { get; set; }
        public long? REPORT_GRP_ID { get; set; }
        public long? REPORT_PARENT_ID { get; set; }
        public long? REPORT_CHILD_ID { get; set; }
        public string REPORT_NAME { get; set; }
        public string GROUP_NAME { get; set; }
    }
}
