using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class LoyaltyModel
    {
        public long LOYALTI_ID { get; set; }
        public decimal InvoiceExceeding { get; set; }
        public decimal? InvoiceExceeding1 { get; set; }
        public decimal FlatPoints { get; set; }
        public decimal? FlatPoints1 { get; set; }
        public string SETTINGSNAME { get; set; }
        public string WEIGHTAGE { get; set; }
        public DateTime FROMDATE { get; set; }
        public DateTime TODATE { get; set; }
        public string CUSTOMERGROUP { get; set; }
        public string COLLECTION { get; set; }
        public long COMPANY_ID { get; set; }
        public bool ISACTIVE { get; set; }
        public bool ISDEFAULT { get; set; }
        public bool IS_DELETE { get; set; }
        public int SLNO { get; set; }
        public ObservableCollection<LoyaltyModel> ListGrid1 { get; set; }
    }
}
