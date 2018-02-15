using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.Models
{
    public class EstimateModel
    {
        public int EstimateID { get; set; }
        public int CountItem { get; set; }
        public decimal TotalPrice { get; set; }
        //public DateTime EstimateDatetime { get; set; }
        public decimal TotalTax { get; set; }
        public string EstimateNo { get; set; }
        public string BusinessLocation { get; set; }
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public string EstimateNumber { get; set; }
        public DateTime EstimateDate { get; set; }
        public decimal GrnadTotal { get; set; }
        public decimal TaxGrandTotal { get; set; }
        public string TotalItemQty { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceNote { get; set; }
        public string HoldNote { get; set; }
        public bool InvoiceStatus { get; set; }
        public string EmployeeLogin { get; set; }
        public string CashRegister { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
