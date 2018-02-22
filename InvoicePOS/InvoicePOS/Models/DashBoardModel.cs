using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class  DashBoardPendingInvoiceModel
    {
        public string INVOICE_NO { get; set; }
        public DateTime? INVOICE_DATE { get; set; }
        public string CUSTOMER_FIRST_NAME { get; set; }
        public string CUSTOMER_LAST_NAME { get; set; }
        public string CUSTOMER_NUMBER { get; set; }
        public int? TOTAL_AMOUNT { get; set; }
        public decimal? PENDING_AMOUNT { get; set; }
        public string CUSTOMER_NAME { get; set; }
    }
}
