//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InvoicePOSDATA
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_ITEMS_ATTRIBUTE
    {
        public long ATTRIBUTE_ID { get; set; }
        public Nullable<long> ITEM_ID { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<bool> NOT_FOR_SALE { get; set; }
        public Nullable<bool> IS_PURCHASED { get; set; }
        public Nullable<bool> IS_SERVICE { get; set; }
        public Nullable<bool> IS_GIFT_CARD { get; set; }
        public Nullable<bool> ONLY_ONLINE_SHOP { get; set; }
        public Nullable<bool> NOT_FOR_ONLINE_SHOP { get; set; }
        public Nullable<bool> ALLOW_PURCHASE_ON_ESHOP { get; set; }
        public Nullable<bool> INCLUDE_TAX { get; set; }
        public Nullable<bool> IS_SORTABLE_ITEM { get; set; }
        public Nullable<long> USER_ID { get; set; }
    }
}