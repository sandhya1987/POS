using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{

    public class CustomerGroupAutoModel
    {
        private string[] keywordStrings;
        private string displayString;
        public string DisplayName
        {
            get { return displayString; }
            set { displayString = value; }
        }

        private long displayId;
        public long DisplayId
        {
            get { return displayId; }
            set { displayId = value; }
        }

        public string[] KeywordStrings
        {
            get
            {
                if (keywordStrings == null)
                {
                    keywordStrings = new string[] { displayString };
                }
                return keywordStrings;
            }
        }
        public override string ToString()
        {
            return displayString;
        }
    }



    public class CustomerGroupModel
    {
        public long CUSTOMER_GROUP_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public long COMPANY_ID { get; set; }
        public bool IS_DELETE { get; set; }
        public int SLNO { get; set; }
    }
}
