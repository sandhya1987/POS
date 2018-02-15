using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
   public class TaxManagementModel
    {
        public int TAX_ID { get; set; }
        public string TAX_NAME { get; set; }
        public decimal TAX_VALUE { get; set; }
        public long COMPANY_ID { get; set; }
        public bool IS_SEPARATE { get; set; }
        public bool IS_DELETE { get; set; }
        public string COMPANY { get; set; }
    }

   public class TaxListModel
   {
      // public long TAX_ID { get; set; }
       public string TAX_NAME { get; set; }
       //public string COMPANY { get; set; }
       
   }
}
