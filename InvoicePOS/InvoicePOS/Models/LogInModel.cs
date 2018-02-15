using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class LogInModel
    {
        public long USER_ID { get; set; }
        public string PASSWORD { get; set; }
        public long COMPANY_ID { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public byte[] LOGIN_TIME { get; set; }
        public string MAC_ADDRESS { get; set; }
        public string ROLE { get; set; }
    }
}
