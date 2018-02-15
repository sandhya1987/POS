using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public class EstimateModel
    {
        public int EstimateId { get; set; }
        public int CountItem { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? Datetime { get; set; }
        public decimal? TotalTax { get; set; }
        public string EstimateNo { get; set; }
        public int Item_Id { get; set; }
        public string ItemName { get; set; }
        public string Barcode { get; set; }
        public int CurrentQty { get; set; }
        public DateTime EstimateDate { get; set; }
        public string BusinessLoc { get; set; }
        public string CashRegister { get; set; }
    }
}