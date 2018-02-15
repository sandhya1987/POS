using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{

    public class UnitAutoListModel
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
        // public long CATAGORY_ID { get; set; }
    }
   public class UnitModel
    {
       public long UNIT_ID { get; set; }
       public string MEASURING_NAME { get; set; }
       public string SHORT_INDAX { get; set; }
       public string IMAGE_PATH { get; set; }
       public bool IS_DELETE { get; set; }
       public long COMPANY_ID { get; set; }
       public int SLNO { get; set; }
    }
}
