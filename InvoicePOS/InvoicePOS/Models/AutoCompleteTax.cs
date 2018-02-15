using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class AutoCompleteTax
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
        //public CategoryAutoListModel(long Id,string NAME)
        //{
        //    displayId = Id;
        //    displayString = NAME;

        //}
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
}
