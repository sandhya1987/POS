﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NEW_POS_DBEntities : DbContext
    {
        public NEW_POS_DBEntities()
            : base("name=NEW_POS_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AUTHORIZATION> AUTHORIZATIONs { get; set; }
        public DbSet<MODULE> MODULEs { get; set; }
        public DbSet<TBL_AUTO_GENERATE_CODE> TBL_AUTO_GENERATE_CODE { get; set; }
        public DbSet<TBL_AUTO_GENERATE_CODE_FOR_CUSTOMER> TBL_AUTO_GENERATE_CODE_FOR_CUSTOMER { get; set; }
        public DbSet<TBL_AUTO_GENERATE_CODE_FOR_LOYALTY> TBL_AUTO_GENERATE_CODE_FOR_LOYALTY { get; set; }
        public DbSet<TBL_AUTO_GENERATE_CODE_FOR_PRODUCT> TBL_AUTO_GENERATE_CODE_FOR_PRODUCT { get; set; }
        public DbSet<TBL_AUTO_GENERATE_INVOICE_NO> TBL_AUTO_GENERATE_INVOICE_NO { get; set; }
        public DbSet<TBL_BANK> TBL_BANK { get; set; }
        public DbSet<TBL_BANK_ACCOUNT> TBL_BANK_ACCOUNT { get; set; }
        public DbSet<TBL_BUSINESS_LOCATION> TBL_BUSINESS_LOCATION { get; set; }
        public DbSet<TBL_CASH_REG> TBL_CASH_REG { get; set; }
        public DbSet<TBL_CATAGORY> TBL_CATAGORY { get; set; }
        public DbSet<TBL_CHEQUE> TBL_CHEQUE { get; set; }
        public DbSet<TBL_COMPANY> TBL_COMPANY { get; set; }
        public DbSet<TBL_CUSTOMER> TBL_CUSTOMER { get; set; }
        public DbSet<TBL_CUSTOMER_BILLING_ADDRESS> TBL_CUSTOMER_BILLING_ADDRESS { get; set; }
        public DbSet<TBL_CUSTOMER_GROUP> TBL_CUSTOMER_GROUP { get; set; }
        public DbSet<TBL_CUSTOMER_SHIPPING_ADDRESS> TBL_CUSTOMER_SHIPPING_ADDRESS { get; set; }
        public DbSet<TBL_DEPARTMENT> TBL_DEPARTMENT { get; set; }
        public DbSet<TBL_DESIGNATION> TBL_DESIGNATION { get; set; }
        public DbSet<TBL_EMAIL_SETTINGS> TBL_EMAIL_SETTINGS { get; set; }
        public DbSet<TBL_EMPLOYEE> TBL_EMPLOYEE { get; set; }
        public DbSet<TBL_ESTIMATE1> TBL_ESTIMATE1 { get; set; }
        public DbSet<TBL_FINANCIAL> TBL_FINANCIAL { get; set; }
        public DbSet<TBL_GODOWN> TBL_GODOWN { get; set; }
        public DbSet<TBL_HOLD_INVOICE> TBL_HOLD_INVOICE { get; set; }
        public DbSet<TBL_INVOICE> TBL_INVOICE { get; set; }
        public DbSet<TBL_INVOICE_PAY> TBL_INVOICE_PAY { get; set; }
        public DbSet<TBL_ITEM_LOCATION> TBL_ITEM_LOCATION { get; set; }
        public DbSet<TBL_ITEMS> TBL_ITEMS { get; set; }
        public DbSet<TBL_ITEMS_ATTRIBUTE> TBL_ITEMS_ATTRIBUTE { get; set; }
        public DbSet<TBL_LOYALTY> TBL_LOYALTY { get; set; }
        public DbSet<TBL_LOYALTY_Exceedings> TBL_LOYALTY_Exceedings { get; set; }
        public DbSet<TBL_NEWCASHREGISTER> TBL_NEWCASHREGISTER { get; set; }
        public DbSet<TBL_OPENING_BALANCE> TBL_OPENING_BALANCE { get; set; }
        public DbSet<TBL_OPENING_STOCK> TBL_OPENING_STOCK { get; set; }
        public DbSet<TBL_ORDER> TBL_ORDER { get; set; }
        public DbSet<TBL_PAYMENT> TBL_PAYMENT { get; set; }
        public DbSet<TBL_PO> TBL_PO { get; set; }
        public DbSet<TBL_PO_ITEMS> TBL_PO_ITEMS { get; set; }
        public DbSet<TBL_PO_PAYMENT> TBL_PO_PAYMENT { get; set; }
        public DbSet<TBL_PURCHASE_ORDERS> TBL_PURCHASE_ORDERS { get; set; }
        public DbSet<TBL_RECEIVE_ITEM> TBL_RECEIVE_ITEM { get; set; }
        public DbSet<TBL_RECEIVE_ITEM_ITEMS> TBL_RECEIVE_ITEM_ITEMS { get; set; }
        public DbSet<TBL_RECEIVE_PAYMENT> TBL_RECEIVE_PAYMENT { get; set; }
        public DbSet<TBL_REPORT> TBL_REPORT { get; set; }
        public DbSet<TBL_REPORT_ADD> TBL_REPORT_ADD { get; set; }
        public DbSet<TBL_REPORT_GROUP> TBL_REPORT_GROUP { get; set; }
        public DbSet<TBL_SALE_ITEM> TBL_SALE_ITEM { get; set; }
        public DbSet<TBL_SALES_RETURN> TBL_SALES_RETURN { get; set; }
        public DbSet<TBL_STOCK_TRANSFER> TBL_STOCK_TRANSFER { get; set; }
        public DbSet<TBL_SUPP_PAYMENT> TBL_SUPP_PAYMENT { get; set; }
        public DbSet<TBL_SUPPLIER> TBL_SUPPLIER { get; set; }
        public DbSet<tbl_supppaydetails> tbl_supppaydetails { get; set; }
        public DbSet<TBL_TAX> TBL_TAX { get; set; }
        public DbSet<TBL_TRANSFER> TBL_TRANSFER { get; set; }
        public DbSet<TBL_TRANSFER_CASH> TBL_TRANSFER_CASH { get; set; }
        public DbSet<TBL_UNIT_MEASURING> TBL_UNIT_MEASURING { get; set; }
        public DbSet<TBL_USER> TBL_USER { get; set; }
        public DbSet<TBL_VENDORS> TBL_VENDORS { get; set; }
        public DbSet<tmp_invoice> tmp_invoice { get; set; }
        public DbSet<MODULE_RIGHTS> MODULE_RIGHTS { get; set; }
        public DbSet<VIEW_INVOICE> VIEW_INVOICE { get; set; }
        public DbSet<View_ITEM_ATTRIBUTE> View_ITEM_ATTRIBUTE { get; set; }
        public DbSet<VW_EMP_MODULE_ACCESS> VW_EMP_MODULE_ACCESS { get; set; }
    }
}
