using InvoicePOSAPI.Models;
using InvoicePOSDATA;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvoicePOSAPI.Controllers
{
    public class ItemAPIController : ApiController
    {
        ItemModel im = new ItemModel();
        NEW_POSEntities db = new NEW_POSEntities();

        [HttpGet]
        public HttpResponseMessage GetAllItem1(int id, int comp)
        {

            //var str = (from b in db.TBL_ITEM_LOCATION
            //           //join b in db.TBL_GODOWN on a.GODOWN_ID equals b.GODOWN_ID
            //           join a in db.TBL_ITEMS on b.GOWDOWN_ID equals a.GODOWN_ID
            //           where  b.GOWDOWN_ID == id && a.IS_DELETE == false
            //           select new ItemModel
            //           {


            var str = (from a in db.TBL_ITEMS
                       where a.GODOWN_ID == id && a.COMPANY_ID == comp && a.IS_DELETE == false
                       select new ItemModel
                       {

                           IMAGE_PATH = a.IMAGE_PATH,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_NAME = a.ITEM_NAME,
                           ITEM_LOCATION = a.ITEM_LOCATION,
                           ITEM_DESCRIPTION = a.ITEM_DESCRIPTION,
                           ITEM_PRICE = a.ITEM_PRICE,
                           ITEM_INVOICE_ID = a.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = a.ITEM_PRODUCT_ID,
                           KEYWORD = a.KEYWORD,
                           ITEM_LOCATION_NAME = a.ITEM_LOCATION_NAME,
                           //SORT_INDEX = b.SORT_INDEX,
                           ACCESSORIES_KEYWORD = a.ACCESSORIES_KEYWORD,
                           BARCODE = a.BARCODE,
                           CATAGORY_ID = a.CATAGORY_ID,
                           CATEGORY_NAME = a.CATEGORY_NAME,
                           SEARCH_CODE = a.SERCH_CODE,
                           TAX_PAID = a.TAX_PAID,
                           TAX_COLLECTED = a.TAX_COLLECTED,
                           //BUSINESS_LOC = a.BUSSINESS_LOCATION,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_SALES_ID = a.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = a.PURCHAGE_UNIT_ID,
                           PURCHASE_UNIT = a.PURCHASE_UNIT,
                           SALES_UNIT = a.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_PRICE = a.SALES_PRICE,
                           MRP = a.MRP,
                           COMPANY_ID = a.COMPANY_ID,
                           OPN_QNT = a.OPN_QNT,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = a.ITEM_GROUP_NAME,
                           ITEM_UNIQUE_NAME = a.ITEM_UNIQUE_NAME,
                           REGIONAL_LANGUAGE = a.REGIONAL_LANGUAGE,
                           SALES_PRICE_BEFOR_TAX = a.SALES_PRICE_BEFOR_TAX,
                           IS_DELETE = a.IS_DELETE,
                           INCLUDE_TAX = a.INCLUDE_TAX,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           MODIFICATION_DATE = a.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = a.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = a.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = a.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = a.WEIGHT_OF_PAPER,
                           //GODOWN = a.GODOWN,
                           DATE = System.DateTime.Now,
                           //COMPANY_NAME = a.COMPANY_NAME,
                           //TAX_COLLECTED_NAME = a.TAX_COLLECTED_NAME,
                           //TAX_PAID_NAME = a.TAX_PAID_NAME,
                           //TaxName = a.TAX_NAME,
                           //TaxValue = a.TAX_VALUE,
                           Total = 0,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetAllItem1bybarcode(int id, int comp, string barcode)
        {

            //var str = (from b in db.TBL_ITEM_LOCATION
            //           //join b in db.TBL_GODOWN on a.GODOWN_ID equals b.GODOWN_ID
            //           join a in db.TBL_ITEMS on b.GOWDOWN_ID equals a.GODOWN_ID
            //           where  b.GOWDOWN_ID == id && a.IS_DELETE == false
            //           select new ItemModel
            //           {


            var str = (from a in db.TBL_ITEMS
                       where a.GODOWN_ID == id && a.COMPANY_ID == comp && a.BARCODE == barcode && a.IS_DELETE == false
                       select new ItemModel
                       {

                           IMAGE_PATH = a.IMAGE_PATH,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_NAME = a.ITEM_NAME,
                           ITEM_LOCATION = a.ITEM_LOCATION,
                           ITEM_DESCRIPTION = a.ITEM_DESCRIPTION,
                           ITEM_PRICE = a.ITEM_PRICE,
                           ITEM_INVOICE_ID = a.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = a.ITEM_PRODUCT_ID,
                           KEYWORD = a.KEYWORD,
                           ITEM_LOCATION_NAME = a.ITEM_LOCATION_NAME,
                           //SORT_INDEX = b.SORT_INDEX,
                           ACCESSORIES_KEYWORD = a.ACCESSORIES_KEYWORD,
                           BARCODE = a.BARCODE,
                           CATAGORY_ID = a.CATAGORY_ID,
                           CATEGORY_NAME = a.CATEGORY_NAME,
                           SEARCH_CODE = a.SERCH_CODE,
                           TAX_PAID = a.TAX_PAID,
                           TAX_COLLECTED = a.TAX_COLLECTED,
                           //BUSINESS_LOC = a.BUSSINESS_LOCATION,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_SALES_ID = a.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = a.PURCHAGE_UNIT_ID,
                           PURCHASE_UNIT = a.PURCHASE_UNIT,
                           SALES_UNIT = a.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_PRICE = a.SALES_PRICE,
                           MRP = a.MRP,
                           COMPANY_ID = a.COMPANY_ID,
                           OPN_QNT = a.OPN_QNT,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = a.ITEM_GROUP_NAME,
                           ITEM_UNIQUE_NAME = a.ITEM_UNIQUE_NAME,
                           REGIONAL_LANGUAGE = a.REGIONAL_LANGUAGE,
                           SALES_PRICE_BEFOR_TAX = a.SALES_PRICE_BEFOR_TAX,
                           IS_DELETE = a.IS_DELETE,
                           INCLUDE_TAX = a.INCLUDE_TAX,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           MODIFICATION_DATE = a.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = a.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = a.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = a.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = a.WEIGHT_OF_PAPER,
                           //GODOWN = a.GODOWN,
                           DATE = System.DateTime.Now,
                           //COMPANY_NAME = a.COMPANY_NAME,
                           //TAX_COLLECTED_NAME = a.TAX_COLLECTED_NAME,
                           //TAX_PAID_NAME = a.TAX_PAID_NAME,
                           //TaxName = a.TAX_NAME,
                           //TaxValue = a.TAX_VALUE,
                           Total = 0,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage GetAllItem(int id)
        {
            //try
            //{
            var m = db.View_ITEM_ATTRIBUTE;
            var str = (from a in db.View_ITEM_ATTRIBUTE
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new ItemModel
                       {
                           SLNO = 0,
                           IMAGE_PATH = a.IMAGE_PATH,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_NAME = a.ITEM_NAME,
                           ITEM_LOCATION = a.ITEM_LOCATION,
                           ITEM_LOCATION_NAME = a.ITEM_LOCATION_NAME,
                           ITEM_DESCRIPTION = a.ITEM_DESCRIPTION,
                           ITEM_PRICE = a.ITEM_PRICE,
                           ITEM_INVOICE_ID = a.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = a.ITEM_PRODUCT_ID,
                           KEYWORD = a.KEYWORD,
                           ACCESSORIES_KEYWORD = a.ACCESSORIES_KEYWORD,
                           BARCODE = a.BARCODE,
                           CATAGORY_ID = a.CATAGORY_ID,
                           CATEGORY_NAME = a.CATEGORY_NAME,
                           SEARCH_CODE = a.SERCH_CODE,
                           TAX_PAID = a.TAX_PAID,
                           TAX_COLLECTED = a.TAX_COLLECTED,
                           BUSINESS_LOC = a.BUSSINESS_LOCATION,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_SALES_ID = a.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = a.PURCHAGE_UNIT_ID,
                           PURCHASE_UNIT = a.PURCHASE_UNIT,
                           SALES_UNIT = a.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_PRICE = a.SALES_PRICE,
                           MRP = a.MRP,
                           COMPANY_ID = a.COMPANY_ID,
                           OPN_QNT = a.OPN_QNT,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = a.ITEM_GROUP_NAME,
                           ITEM_UNIQUE_NAME = a.ITEM_UNIQUE_NAME,
                           REGIONAL_LANGUAGE = a.REGIONAL_LANGUAGE,
                           SALES_PRICE_BEFOR_TAX = a.SALES_PRICE_BEFOR_TAX,
                           IS_DELETE = a.IS_DELETE,
                           INCLUDE_TAX = a.INCLUDE_TAX,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           MODIFICATION_DATE = a.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = a.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = a.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = a.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = a.WEIGHT_OF_PAPER,
                           GODOWN = a.GODOWN,
                           DATE = a.DATE,
                           COMPANY_NAME = a.COMPANY_NAME,
                           TAX_COLLECTED_NAME = a.TAX_COLLECTED_NAME,
                           TAX_PAID_NAME = a.TAX_PAID_NAME,
                           TaxName = a.TAX_NAME,
                           TaxValue = a.TAX_VALUE,
                           Total = 0,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
            //}
            //catch
            //{

            //}

        }
        [HttpGet]
        public HttpResponseMessage GetItemDetails(int id, int itemid)
        {
            //try
            //{
            //var m = db.TBL_ITEMS;
            int total = 0;
            var str = (
                       //from a in db.TBL_RECEIVE_ITEM.ToList()
                       //join b in db.TBL_SALE_ITEM.ToList() on a.Item_Id equals b.SALE_ITEM_ID
                       //join c in db.TBL_ITEMS.ToList() on a.Item_Id equals c.ITEM_ID
                       //join d in db.TBL_BUSINESS_LOCATION.ToList() on c.BUSS_LOC_ID equals d.BUSINESS_LOCATION_ID
                       //join e in db.TBL_CUSTOMER.ToList() on d.BUSINESS_LOCATION_ID equals e.BUSINESS_LOCATION_ID 
                       //join f in db.TBL_CUSTOMER_BILLING_ADDRESS.ToList() on e.CUSTOMER_ID equals f.CUSTOMER_ID

                       from a in db.TBL_ITEMS.ToList()
                       join b in db.TBL_BUSINESS_LOCATION.ToList() on a.BUSS_LOC_ID.Value equals b.BUSINESS_LOCATION_ID
                       join c in db.TBL_RECEIVE_ITEM.ToList() on a.ITEM_ID equals c.Item_Id
                       join d in db.TBL_CUSTOMER.ToList() on b.BUSINESS_LOCATION_ID equals d.BUSINESS_LOCATION_ID
                       join e in db.TBL_CUSTOMER_BILLING_ADDRESS.ToList() on d.CUSTOMER_ID equals e.CUSTOMER_ID
                       join f in db.TBL_SALE_ITEM.ToList() on a.ITEM_ID equals f.SALE_ITEM_ID
                       join g in db.TBL_INVOICE_PAY.ToList() on f.INVOICE_ID.Value equals g.INVOICE_ID

                       where a.ITEM_ID == itemid && a.IS_DELETE == false && g.INVOICE_ID == id
                       select new ItemModel
                       {
                           //SUPPLIER_NAME = b.SUPPLIER_NAME,
                           CUSTOMER_NAME = d.FIRST_NAME + " "+d.LAST_NAME,
                           CUSTOMER_MOBILE = e.MOBILE,
                           CUSTOMER_EMAIL = e.EMAIL,
                           Invoice_No = g.INVOICE_NO,
                           Invoice_Date = g.INVOICE_DATE.Value,
                           Payment_Status = g.STATUS,
                           BARCODE = a.BARCODE,
                           DATE = c.SUPPLIER_INVOICE_DATE,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           //IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           PO_NUMBER = c.RECEIVED_ITEM_ENTRY_NO,
                           ITEM_NAME = a.ITEM_NAME,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_UNIT = a.SALES_UNIT,
                           SALES_PRICE = a.SALES_PRICE,
                           ITEM_ID = c.Item_Id,
                           ITEM_ID1 = c.Item_Id,
                           ITEM_PRICE = a.ITEM_PRICE,
                           INVOICE_ID = 0,
                           ITEM_INVOICE_ID = 0,
                           ITEM_PRODUCT_ID = 0,
                           CATAGORY_ID = a.CATAGORY_ID,
                           OPN_QNT = a.OPN_QNT,
                           TAX_PAID = c.TOTAL_TAX_AMT,
                           TAX_COLLECTED = 0,
                           MRP = a.SALES_PRICE,
                           COMPANY_ID = 0,
                           INCLUDE_TAX = false,
                           SALES_PRICE_BEFOR_TAX = 0,
                           MODIFICATION_DATE = System.DateTime.Now,
                           IS_ACTIVE = false,
                           IS_DELETE = false,
                           WEIGHT_OF_PAPER = 0,
                           WEIGHT_OF_PLASTIC = 0,
                           WEIGHT_OF_FOAM = 0,
                           WEIGHT_OF_CARDBOARD = 0,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_PURCHAGE_ID = 0,
                           UNIT_SALES_ID = 0,
                           TotalTax = f.ITEM_TAX,
                           Total = f.ITEM_TOTAL_AMOUNT,
                           Current_Qty = f.SALE_QTY,
                           TaxValue = 0,
                           TOTPO_QNT = 0,
                           Discount = f.ITEM_DISCOUNT.Value,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        
        [HttpGet]
        public HttpResponseMessage GetInvoiceDeliveryDetails(int id) 
        {
            var str = (from a in db.TBL_INVOICE_PAY
                       join b in db.TBL_EMPLOYEE on a.EMPLOYEE_ID equals b.EMPLOYEE_CODE
                       where a.INVOICE_ID == id
                       select new ItemModel
                       {
                           //CUSTOMER_NAME = d.FIRST_NAME + " " + d.LAST_NAME,
                           //CUSTOMER_MOBILE = e.MOBILE,
                           //CUSTOMER_EMAIL = e.EMAIL,
                           //Invoice_No = a.INVOICE_NO,
                           //Invoice_Date = a.INVOICE_DATE.Value,
                           //Payment_Status = g.STATUS,
                           //BARCODE = a.BARCODE,
                           //DATE = c.SUPPLIER_INVOICE_DATE,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           //IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           //PO_NUMBER = c.RECEIVED_ITEM_ENTRY_NO,
                           //ITEM_NAME = a.ITEM_NAME,
                           //PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           //SALES_UNIT = a.SALES_UNIT,
                           //SALES_PRICE = a.SALES_PRICE,
                           //ITEM_ID = c.Item_Id,
                           //ITEM_ID1 = c.Item_Id,
                           //ITEM_PRICE = a.ITEM_PRICE,
                           //INVOICE_ID = 0,
                           //ITEM_INVOICE_ID = 0,
                           //ITEM_PRODUCT_ID = 0,
                           //CATAGORY_ID = a.CATAGORY_ID,
                           //OPN_QNT = a.OPN_QNT,
                           //TAX_PAID = c.TOTAL_TAX_AMT,
                           //TAX_COLLECTED = 0,
                           //MRP = a.SALES_PRICE,
                           COMPANY_ID = 0,
                           INCLUDE_TAX = false,
                           SALES_PRICE_BEFOR_TAX = 0,
                           MODIFICATION_DATE = System.DateTime.Now,
                           IS_ACTIVE = false,
                           IS_DELETE = false,
                           WEIGHT_OF_PAPER = 0,
                           WEIGHT_OF_PLASTIC = 0,
                           WEIGHT_OF_FOAM = 0,
                           WEIGHT_OF_CARDBOARD = 0,
                           //BUSS_LOC_ID = a.BUSS_LOC_ID,
                           //GODOWN_ID = a.GODOWN_ID,
                           //UNIT_PURCHAGE_ID = 0,
                           //UNIT_SALES_ID = 0,
                           //TotalTax = f.ITEM_TAX,
                           //Total = f.ITEM_TOTAL_AMOUNT,
                           //Current_Qty = f.SALE_QTY,
                           //TaxValue = 0,
                           //TOTPO_QNT = 0,
                           //Discount = f.ITEM_DISCOUNT.Value,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);      
        }
        [HttpGet]
        public HttpResponseMessage GetAllSalesDetails(int id, int itemid)
        {
            //try
            //{
            //var m = db.TBL_ITEMS;
            var str = (from a in db.TBL_RECEIVE_ITEM.ToList()
                       join b in db.TBL_SALE_ITEM.ToList() on a.Item_Id equals b.SALE_ITEM_ID
                       join c in db.TBL_ITEMS.ToList() on a.Item_Id equals c.ITEM_ID

                       where a.Item_Id == itemid && a.IS_DELETE == false
                       select new ItemModel
                       {
                           //SUPPLIER_NAME = b.SUPPLIER_NAME,
                           DATE = a.SUPPLIER_INVOICE_DATE,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           //IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           PO_NUMBER = a.RECEIVED_ITEM_ENTRY_NO,
                           ITEM_NAME = a.ITEM_NAME,
                           PURCHASE_UNIT_PRICE = c.PURCHASE_UNIT_PRICE,
                           SALES_UNIT = c.SALES_UNIT,
                           SALES_PRICE = c.SALES_PRICE,
                           ITEM_ID = a.Item_Id,
                           ITEM_ID1 = a.Item_Id,
                           ITEM_PRICE = c.ITEM_PRICE,
                           INVOICE_ID = 0,
                           ITEM_INVOICE_ID = 0,
                           ITEM_PRODUCT_ID = 0,
                           CATAGORY_ID = c.CATAGORY_ID,
                           OPN_QNT = c.OPN_QNT,
                           TAX_PAID = a.TOTAL_TAX_AMT,
                           TAX_COLLECTED = 0,
                           MRP = c.SALES_PRICE,
                           COMPANY_ID = 0,
                           INCLUDE_TAX = false,
                           SALES_PRICE_BEFOR_TAX = 0,
                           MODIFICATION_DATE = System.DateTime.Now,
                           IS_ACTIVE = false,
                           IS_DELETE = false,
                           WEIGHT_OF_PAPER = 0,
                           WEIGHT_OF_PLASTIC=0,
                           WEIGHT_OF_FOAM=0,
                           WEIGHT_OF_CARDBOARD=0,
                           BUSS_LOC_ID=c.BUSS_LOC_ID,
                           GODOWN_ID=c.GODOWN_ID,
                           UNIT_PURCHAGE_ID=0,
                           UNIT_SALES_ID=0,
                           TotalTax=0,
                           Total=0,
                           Current_Qty=0,
                           TaxValue=0,
                           TOTPO_QNT=0,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
            //}
            //catch
            //{

            //}

        }

        [HttpGet]
        public HttpResponseMessage GetAllPurchaseDetails(int id, int itemid)
        {
            //try
            //{
            //var m = db.TBL_ITEMS;
            var str = (from a in db.TBL_ITEMS.ToList()
                       join b in db.TBL_RECEIVE_ITEM.ToList() on a.ITEM_ID equals b.Item_Id
                       join c in db.TBL_SUPPLIER.ToList() on b.SUPPLIER_ID equals c.SUPPLIER_ID
                       
                       where a.ITEM_ID == itemid && a.IS_DELETE == false
                       select new ItemModel
                       {
                           SUPPLIER_NAME = c.SUPPLIER_NAME,
                           DATE = b.SUPPLIER_INVOICE_DATE,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           //IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           PO_NUMBER = b.RECEIVED_ITEM_ENTRY_NO,
                           ITEM_NAME = a.ITEM_NAME,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,                           
                           SALES_UNIT = a.SALES_UNIT,
                           SALES_PRICE = a.SALES_PRICE,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_ID1 = a.ITEM_ID,
                           ITEM_PRICE = a.ITEM_PRICE,
                           INVOICE_ID = 0,
                           ITEM_INVOICE_ID = 0,
                           ITEM_PRODUCT_ID = 0,
                           CATAGORY_ID = a.CATAGORY_ID,
                           OPN_QNT = a.OPN_QNT,
                           TAX_PAID = b.TOTAL_TAX_AMT,
                           TAX_COLLECTED = a.TAX_PAID,
                           MRP = a.SALES_PRICE,
                           COMPANY_ID = 0,
                           INCLUDE_TAX = false,
                           SALES_PRICE_BEFOR_TAX = 0,
                           MODIFICATION_DATE = System.DateTime.Now,
                           IS_ACTIVE = false,
                           IS_DELETE = false,
                           WEIGHT_OF_PAPER = 0,
                           WEIGHT_OF_PLASTIC = 0,
                           WEIGHT_OF_FOAM = 0,
                           WEIGHT_OF_CARDBOARD = 0,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_PURCHAGE_ID = 0,
                           UNIT_SALES_ID = 0,
                           TotalTax = 0,
                           Total = 0,
                           Current_Qty = 0,
                           TaxValue = 0,
                           TOTPO_QNT = 0,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
            //}
            //catch
            //{

            //}

        }
        [HttpGet]
        public HttpResponseMessage GetItemSalesList(int id,int itemid) 
        {
            var str = (from a in db.TBL_ITEMS.ToList()
                       join b in db.TBL_BUSINESS_LOCATION.ToList() on a.BUSS_LOC_ID.Value equals b.BUSINESS_LOCATION_ID
                       join c in db.TBL_RECEIVE_ITEM.ToList() on a.ITEM_ID equals c.Item_Id
                       join d in db.TBL_CUSTOMER.ToList() on b.BUSINESS_LOCATION_ID equals d.BUSINESS_LOCATION_ID
                       join e in db.TBL_CUSTOMER_BILLING_ADDRESS.ToList() on d.CUSTOMER_ID equals e.CUSTOMER_ID 
                       join f in db.TBL_SALE_ITEM.ToList() on a.ITEM_ID equals f.SALE_ITEM_ID
                       join g in db.TBL_INVOICE_PAY.ToList() on f.INVOICE_ID.Value equals g.INVOICE_ID
                       join h in db.TBL_ESTIMATE1 on a.ITEM_ID equals h.ITEM_ID
                       //join c in db.TBL_BUSINESS_LOCATION on a.BUSS_LOC_ID equals c.BUSINESS_LOCATION_ID

                       where a.ITEM_ID == itemid && a.IS_DELETE == false 
                       select new ItemModel
                       {
                           //SUPPLIER_NAME = c.SUPPLIER_NAME,
                           ESTIMATE_DATE = h.ESTIMATE_DATE.Value,
                           Invoice_No = g.INVOICE_NO,
                           Invoice_Date = g.INVOICE_DATE.Value,
                           Payment_Status = g.STATUS,
                           CUSTOMER_NAME = d.FIRST_NAME + " " + d.LAST_NAME,
                           CUSTOMER_EMAIL = e.EMAIL,
                           CUSTOMER_MOBILE = e.MOBILE,
                           BUSINESS_LOC = b.SHOP_NAME,
                           DATE = c.SUPPLIER_INVOICE_DATE,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           PO_NUMBER = c.RECEIVED_ITEM_ENTRY_NO,
                           ITEM_NAME = a.ITEM_NAME,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_UNIT = a.SALES_UNIT,
                           SALES_PRICE = a.SALES_PRICE,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_ID1 = a.ITEM_ID,
                           ITEM_PRICE = a.ITEM_PRICE,
                           INVOICE_ID = g.INVOICE_ID,
                           ITEM_INVOICE_ID = a.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = 0,
                           CATAGORY_ID = a.CATAGORY_ID,
                           OPN_QNT = a.OPN_QNT,
                           TAX_PAID = c.TOTAL_TAX_AMT,
                           TAX_COLLECTED = 0,
                           MRP = a.SALES_PRICE,
                           COMPANY_ID = 0,
                           INCLUDE_TAX = false,
                           SALES_PRICE_BEFOR_TAX = 0,
                           MODIFICATION_DATE = System.DateTime.Now,
                           //IS_ACTIVE = false,
                           IS_DELETE = false,
                           WEIGHT_OF_PAPER = 0,
                           WEIGHT_OF_PLASTIC = 0,
                           WEIGHT_OF_FOAM = 0,
                           WEIGHT_OF_CARDBOARD = 0,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_PURCHAGE_ID = 0,
                           UNIT_SALES_ID = 0,
                           TotalTax = 0,
                           Total = c.TOTAL_AMT,
                           Current_Qty = 0,
                           TaxValue = 0,
                           TOTPO_QNT = 0,
                           
                       }).ToList();
           

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetItemSalesList(DateTime frmDt,DateTime toDt)
        {
            var str = (from a in db.TBL_ITEMS.ToList()
                       join b in db.TBL_BUSINESS_LOCATION.ToList() on a.BUSS_LOC_ID.Value equals b.BUSINESS_LOCATION_ID
                       join c in db.TBL_RECEIVE_ITEM.ToList() on a.ITEM_ID equals c.Item_Id
                       join d in db.TBL_CUSTOMER.ToList() on b.BUSINESS_LOCATION_ID equals d.BUSINESS_LOCATION_ID
                       join e in db.TBL_CUSTOMER_BILLING_ADDRESS.ToList() on d.CUSTOMER_ID equals e.CUSTOMER_ID
                       join f in db.TBL_SALE_ITEM.ToList() on a.ITEM_ID equals f.SALE_ITEM_ID
                       join g in db.TBL_INVOICE_PAY.ToList() on f.INVOICE_ID.Value equals g.INVOICE_ID
                       join h in db.TBL_ESTIMATE1 on a.ITEM_ID equals h.ITEM_ID
                       //join c in db.TBL_BUSINESS_LOCATION on a.BUSS_LOC_ID equals c.BUSINESS_LOCATION_ID

                       where (g.INVOICE_DATE >= frmDt && g.INVOICE_DATE <= toDt)
                       select new ItemModel
                       {
                           //SUPPLIER_NAME = c.SUPPLIER_NAME,
                           ESTIMATE_DATE = h.ESTIMATE_DATE.Value,
                           Invoice_No = g.INVOICE_NO,
                           Invoice_Date = g.INVOICE_DATE.Value,
                           Payment_Status = g.STATUS,
                           CUSTOMER_NAME = d.FIRST_NAME + " " + d.LAST_NAME,
                           CUSTOMER_EMAIL = e.EMAIL,
                           CUSTOMER_MOBILE = e.MOBILE,
                           BUSINESS_LOC = b.SHOP_NAME,
                           DATE = c.SUPPLIER_INVOICE_DATE,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           PO_NUMBER = c.RECEIVED_ITEM_ENTRY_NO,
                           ITEM_NAME = a.ITEM_NAME,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_UNIT = a.SALES_UNIT,
                           SALES_PRICE = a.SALES_PRICE,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_ID1 = a.ITEM_ID,
                           ITEM_PRICE = a.ITEM_PRICE,
                           INVOICE_ID = g.INVOICE_ID,
                           ITEM_INVOICE_ID = a.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = 0,
                           CATAGORY_ID = a.CATAGORY_ID,
                           OPN_QNT = a.OPN_QNT,
                           TAX_PAID = c.TOTAL_TAX_AMT,
                           TAX_COLLECTED = 0,
                           MRP = a.SALES_PRICE,
                           COMPANY_ID = 0,
                           INCLUDE_TAX = false,
                           SALES_PRICE_BEFOR_TAX = 0,
                           MODIFICATION_DATE = System.DateTime.Now,
                           //IS_ACTIVE = false,
                           IS_DELETE = false,
                           WEIGHT_OF_PAPER = 0,
                           WEIGHT_OF_PLASTIC = 0,
                           WEIGHT_OF_FOAM = 0,
                           WEIGHT_OF_CARDBOARD = 0,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_PURCHAGE_ID = 0,
                           UNIT_SALES_ID = 0,
                           TotalTax = 0,
                           Total = c.TOTAL_AMT,
                           Current_Qty = 0,
                           TaxValue = 0,
                           TOTPO_QNT = 0,

                       }).ToList();


            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetItemPurchaseList(int id, int itemid)
        {

            var str = (from a in db.TBL_ITEMS
                       join b in db.TBL_RECEIVE_ITEM on a.ITEM_ID equals b.Item_Id
                       //join c in db.TBL_BUSINESS_LOCATION on b.BUSINESS_LOCATION equals c.BUSINESS_LOCATION_ID
                       where a.ITEM_ID == itemid && a.IS_DELETE == false
                       select new ItemModel
                       {
                           ITEM_RECEIVE_DATE = b.RECEIVE_DATE.Value,
                           SUPPLIER_INVOICE_NO = b.SUPPLIER_INVOICE_NO,
                           SUPPLIER_NAME = b.SUPPLIER,
                           GODOWN = b.GODOWN,
                           BUSINESS_LOC = b.BUSINESS_LOCATION,
                           RECEIVE_ITEM_ENTRY_NO = b.RECEIVED_ITEM_ENTRY_NO,
                           RECEIVE_ITEM_ENTRY_DATE = b.RECEIVED_ITEM_ON_DATE.Value,
                           DATE = b.SUPPLIER_INVOICE_DATE,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           //IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           PO_NUMBER = b.RECEIVED_ITEM_ENTRY_NO,
                           ITEM_NAME = a.ITEM_NAME,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_UNIT = a.SALES_UNIT,
                           SALES_PRICE = a.SALES_PRICE,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_ID1 = a.ITEM_ID,
                           ITEM_PRICE = a.ITEM_PRICE,
                           INVOICE_ID = 0,
                           ITEM_INVOICE_ID = 0,
                           ITEM_PRODUCT_ID = 0,
                           CATAGORY_ID = a.CATAGORY_ID,
                           OPN_QNT = a.OPN_QNT,
                           TAX_PAID = b.TOTAL_TAX_AMT,
                           TAX_COLLECTED = 0,
                           MRP = a.SALES_PRICE,
                           COMPANY_ID = 0,
                           INCLUDE_TAX = false,
                           SALES_PRICE_BEFOR_TAX = 0,
                           MODIFICATION_DATE = System.DateTime.Now,
                           IS_ACTIVE = false,
                           IS_DELETE = false,
                           WEIGHT_OF_PAPER = 0,
                           WEIGHT_OF_PLASTIC = 0,
                           WEIGHT_OF_FOAM = 0,
                           WEIGHT_OF_CARDBOARD = 0,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_PURCHAGE_ID = 0,
                           UNIT_SALES_ID = 0,
                           TotalTax = b.TOTAL_TAX_AMT,
                           Total = b.TOTAL_AMT,
                           Current_Qty = 0,
                           TaxValue = 0,
                           TOTPO_QNT = 0,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage GetItemPurchaseList(DateTime frmDt,DateTime toDt)
        {

            var str = (from a in db.TBL_ITEMS
                       join b in db.TBL_RECEIVE_ITEM on a.ITEM_ID equals b.Item_Id
                       //join c in db.TBL_BUSINESS_LOCATION on b.BUSINESS_LOCATION equals c.BUSINESS_LOCATION_ID
                       where b.RECEIVED_ITEM_ON_DATE >= frmDt && b.RECEIVED_ITEM_ON_DATE <= toDt
                       select new ItemModel
                       {
                           ITEM_RECEIVE_DATE = b.RECEIVE_DATE.Value,
                           SUPPLIER_INVOICE_NO = b.SUPPLIER_INVOICE_NO,
                           SUPPLIER_NAME = b.SUPPLIER,
                           GODOWN = b.GODOWN,
                           BUSINESS_LOC = b.BUSINESS_LOCATION,
                           RECEIVE_ITEM_ENTRY_NO = b.RECEIVED_ITEM_ENTRY_NO,
                           RECEIVE_ITEM_ENTRY_DATE = b.RECEIVED_ITEM_ON_DATE.Value,
                           DATE = b.SUPPLIER_INVOICE_DATE,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           //IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           PO_NUMBER = b.RECEIVED_ITEM_ENTRY_NO,
                           ITEM_NAME = a.ITEM_NAME,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_UNIT = a.SALES_UNIT,
                           SALES_PRICE = a.SALES_PRICE,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_ID1 = a.ITEM_ID,
                           ITEM_PRICE = a.ITEM_PRICE,
                           INVOICE_ID = 0,
                           ITEM_INVOICE_ID = 0,
                           ITEM_PRODUCT_ID = 0,
                           CATAGORY_ID = a.CATAGORY_ID,
                           OPN_QNT = a.OPN_QNT,
                           TAX_PAID = b.TOTAL_TAX_AMT,
                           TAX_COLLECTED = 0,
                           MRP = a.SALES_PRICE,
                           COMPANY_ID = 0,
                           INCLUDE_TAX = false,
                           SALES_PRICE_BEFOR_TAX = 0,
                           MODIFICATION_DATE = System.DateTime.Now,
                           IS_ACTIVE = false,
                           IS_DELETE = false,
                           WEIGHT_OF_PAPER = 0,
                           WEIGHT_OF_PLASTIC = 0,
                           WEIGHT_OF_FOAM = 0,
                           WEIGHT_OF_CARDBOARD = 0,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_PURCHAGE_ID = 0,
                           UNIT_SALES_ID = 0,
                           TotalTax = b.TOTAL_TAX_AMT,
                           Total = b.TOTAL_AMT,
                           Current_Qty = 0,
                           TaxValue = 0,
                           TOTPO_QNT = 0,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage Getlocations(int id)
        {


            var str = (from a in db.TBL_ITEM_LOCATION
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new ManageLocation
                       {


                           ITEM_LOCATION_NAME1 = a.ITEM_LOCATION_NAME,
                           //SORT_INDEX=a.SORT_INDEX,


                       }).ToList();

            //var str1 = (from b in db.TBL_ITEMS where b.ITEM_LOCATION_NAME = a.ITEM_LOCATION_NAME select a).FirstOrDefault();
            //if (str1 != null)
            //{
            //}

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage GetIndex(int id)
        {


            var str = (from a in db.TBL_ITEM_LOCATION
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new ManageLocation
                       {


                           //ITEM_LOCATION_NAME1 = a.ITEM_LOCATION_NAME,
                           SORT_INDEX1 = a.SORT_INDEX,


                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }


        [HttpGet]
        public HttpResponseMessage GetlocationAuto(string id)
        {


            var str = (from a in db.TBL_ITEM_LOCATION
                       where a.ITEM_LOCATION_NAME == id && a.IS_DELETE == false
                       select new ManageLocation
                       {

                           ITEM_LOCATION_NAME1 = a.ITEM_LOCATION_NAME,
                           SORT_INDEX = a.SORT_INDEX,
                           LOCATION_ID = a.LOCATION_ID,

                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }


        [HttpGet]
        public HttpResponseMessage GetBarcode(string CompanyId)
        {
            string code = "";
            string value = Convert.ToString(db.TBL_AUTO_GENERATE_CODE_FOR_PRODUCT
                            .OrderByDescending(p => p.AUTO_GENERATE_CODE_FOR_PRODUCT)
                            .Select(r => r.AUTO_GENERATE_CODE_FOR_PRODUCT)
                            .FirstOrDefault());

            var str = (from a in db.TBL_ITEMS where a.BARCODE == value select a).ToList();
            if (str.Count != 0)
            {

                string dd = Convert.ToString(value);
                string aaa = dd.Substring(3, 5);
                int xx = Convert.ToInt32(aaa);
                int noo = Convert.ToInt32(xx + 1);
                code = "I-" + noo.ToString("D6");
                TBL_AUTO_GENERATE_CODE_FOR_PRODUCT _code = new TBL_AUTO_GENERATE_CODE_FOR_PRODUCT();
                _code.AUTO_GENERATE_CODE_FOR_PRODUCT = code;
                _code.COMPANY_ID = Convert.ToInt64(CompanyId);
                _code.DEFINE_CODE = "";
                _code.USER_ID = 0;
                db.TBL_AUTO_GENERATE_CODE_FOR_PRODUCT.Add(_code);
                db.SaveChanges();
            }
            else
            {
                string dd = Convert.ToString(value);
                string aaa = dd.Substring(3, 5);
                int xx = Convert.ToInt32(aaa);
                code = "I-" + xx.ToString("D6");

            }

            return Request.CreateResponse(HttpStatusCode.OK, code);
        }


        [HttpGet]
        public HttpResponseMessage BarCodeunique(string barcode)
        {
            var str = (from a in db.View_ITEM_ATTRIBUTE
                       where a.BARCODE == barcode && a.IS_DELETE == false
                       select new ItemModel
                       {
                           IMAGE_PATH = a.IMAGE_PATH,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_NAME = a.ITEM_NAME,
                           ITEM_DESCRIPTION = a.ITEM_DESCRIPTION,
                           ITEM_PRICE = a.ITEM_PRICE,
                           ITEM_INVOICE_ID = a.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = a.ITEM_PRODUCT_ID,
                           KEYWORD = a.KEYWORD,
                           ACCESSORIES_KEYWORD = a.ACCESSORIES_KEYWORD,
                           BARCODE = a.BARCODE,
                           CATAGORY_ID = a.CATAGORY_ID,
                           SEARCH_CODE = a.SERCH_CODE,
                           TAX_PAID = a.TAX_PAID,
                           TAX_COLLECTED = a.TAX_COLLECTED,

                           PURCHASE_UNIT = a.PURCHASE_UNIT,
                           SALES_UNIT = a.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_PRICE = a.SALES_PRICE,
                           MRP = a.MRP,
                           COMPANY_ID = a.COMPANY_ID,
                           CATEGORY_NAME = a.CATEGORY_NAME,
                           OPN_QNT = a.OPN_QNT,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = a.ITEM_GROUP_NAME,
                           ITEM_UNIQUE_NAME = a.ITEM_UNIQUE_NAME,
                           REGIONAL_LANGUAGE = a.REGIONAL_LANGUAGE,
                           SALES_PRICE_BEFOR_TAX = a.SALES_PRICE_BEFOR_TAX,
                           IS_DELETE = a.IS_DELETE,
                           INCLUDE_TAX = a.INCLUDE_TAX,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           MODIFICATION_DATE = a.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = a.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = a.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = a.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = a.WEIGHT_OF_PAPER,
                           GODOWN = a.GODOWN,
                           DATE = a.DATE,
                           COMPANY_NAME = a.COMPANY_NAME,
                           BUSINESS_LOC = a.BUSSINESS_LOCATION,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_SALES_ID = a.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = a.PURCHAGE_UNIT_ID,
                           TAX_COLLECTED_NAME = a.TAX_COLLECTED_NAME,
                           TAX_PAID_NAME = a.TAX_PAID_NAME,
                           Total = 0,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage SearchCodeunique(string SeaerchCode)
        {
            var str = (from a in db.View_ITEM_ATTRIBUTE
                       where a.SERCH_CODE == SeaerchCode && a.IS_DELETE == false
                       select new ItemModel
                       {
                           IMAGE_PATH = a.IMAGE_PATH,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_NAME = a.ITEM_NAME,
                           ITEM_DESCRIPTION = a.ITEM_DESCRIPTION,
                           ITEM_PRICE = a.ITEM_PRICE,
                           ITEM_INVOICE_ID = a.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = a.ITEM_PRODUCT_ID,
                           KEYWORD = a.KEYWORD,
                           ACCESSORIES_KEYWORD = a.ACCESSORIES_KEYWORD,
                           BARCODE = a.BARCODE,
                           CATAGORY_ID = a.CATAGORY_ID,
                           SEARCH_CODE = a.SERCH_CODE,
                           TAX_PAID = a.TAX_PAID,
                           TAX_COLLECTED = a.TAX_COLLECTED,

                           PURCHASE_UNIT = a.PURCHASE_UNIT,
                           SALES_UNIT = a.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_PRICE = a.SALES_PRICE,
                           MRP = a.MRP,
                           COMPANY_ID = a.COMPANY_ID,
                           CATEGORY_NAME = a.CATEGORY_NAME,
                           OPN_QNT = a.OPN_QNT,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = a.ITEM_GROUP_NAME,
                           ITEM_UNIQUE_NAME = a.ITEM_UNIQUE_NAME,
                           REGIONAL_LANGUAGE = a.REGIONAL_LANGUAGE,
                           SALES_PRICE_BEFOR_TAX = a.SALES_PRICE_BEFOR_TAX,
                           IS_DELETE = a.IS_DELETE,
                           INCLUDE_TAX = a.INCLUDE_TAX,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           MODIFICATION_DATE = a.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = a.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = a.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = a.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = a.WEIGHT_OF_PAPER,
                           GODOWN = a.GODOWN,
                           DATE = a.DATE,
                           COMPANY_NAME = a.COMPANY_NAME,



                           BUSINESS_LOC = a.BUSSINESS_LOCATION,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_SALES_ID = a.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = a.PURCHAGE_UNIT_ID,



                           TAX_COLLECTED_NAME = a.TAX_COLLECTED_NAME,
                           TAX_PAID_NAME = a.TAX_PAID_NAME,
                           Total = 0,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage SearchAllItem(string id)
        {
            var str = (from a in db.View_ITEM_ATTRIBUTE
                       where a.ITEM_NAME.Contains("" + id + "") || a.BARCODE.Contains("" + id + "") || a.SERCH_CODE.Contains("" + id + "") && a.IS_DELETE == false
                       select new ItemModel
                       {
                           ITEM_ID = a.ITEM_ID,
                           ITEM_NAME = a.ITEM_NAME,
                           ITEM_DESCRIPTION = a.ITEM_DESCRIPTION,
                           ITEM_PRICE = a.ITEM_PRICE,
                           ITEM_INVOICE_ID = a.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = a.ITEM_PRODUCT_ID,
                           KEYWORD = a.KEYWORD,
                           ACCESSORIES_KEYWORD = a.ACCESSORIES_KEYWORD,
                           BARCODE = a.BARCODE,
                           CATAGORY_ID = a.CATAGORY_ID,
                           SEARCH_CODE = a.SERCH_CODE,
                           TAX_PAID = a.TAX_PAID,
                           TAX_COLLECTED = a.TAX_COLLECTED,

                           PURCHASE_UNIT = a.PURCHASE_UNIT,
                           SALES_UNIT = a.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_PRICE = a.SALES_PRICE,
                           MRP = a.MRP,
                           COMPANY_ID = a.COMPANY_ID,
                           CATEGORY_NAME = a.CATEGORY_NAME,
                           OPN_QNT = a.OPN_QNT,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = a.ITEM_GROUP_NAME,
                           ITEM_UNIQUE_NAME = a.ITEM_UNIQUE_NAME,
                           REGIONAL_LANGUAGE = a.REGIONAL_LANGUAGE,
                           SALES_PRICE_BEFOR_TAX = a.SALES_PRICE_BEFOR_TAX,
                           IS_DELETE = a.IS_DELETE,
                           INCLUDE_TAX = a.INCLUDE_TAX,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           MODIFICATION_DATE = a.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = a.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = a.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = a.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = a.WEIGHT_OF_PAPER,

                           IMAGE_PATH = a.IMAGE_PATH,


                           BUSINESS_LOC = a.BUSSINESS_LOCATION,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_SALES_ID = a.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = a.PURCHAGE_UNIT_ID,



                           GODOWN = a.GODOWN,
                           DATE = a.DATE,
                           COMPANY_NAME = a.COMPANY_NAME,
                           TAX_COLLECTED_NAME = a.TAX_COLLECTED_NAME,
                           TAX_PAID_NAME = a.TAX_PAID_NAME,
                           Total = 0,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);

        }


        [HttpGet]
        public HttpResponseMessage BarcodeSearch(string id)
        {
            var str = (from a in db.View_ITEM_ATTRIBUTE
                       where a.BARCODE == id && a.IS_DELETE == false
                       select new ItemModel
                       {
                           ITEM_ID = a.ITEM_ID,
                           ITEM_NAME = a.ITEM_NAME,
                           ITEM_DESCRIPTION = a.ITEM_DESCRIPTION,
                           ITEM_PRICE = a.ITEM_PRICE,
                           ITEM_INVOICE_ID = a.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = a.ITEM_PRODUCT_ID,
                           KEYWORD = a.KEYWORD,
                           ACCESSORIES_KEYWORD = a.ACCESSORIES_KEYWORD,
                           BARCODE = a.BARCODE,
                           CATAGORY_ID = a.CATAGORY_ID,
                           SEARCH_CODE = a.SERCH_CODE,
                           TAX_PAID = a.TAX_PAID,
                           TAX_COLLECTED = a.TAX_COLLECTED,

                           PURCHASE_UNIT = a.PURCHASE_UNIT,
                           SALES_UNIT = a.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_PRICE = a.SALES_PRICE,
                           MRP = a.MRP,
                           COMPANY_ID = a.COMPANY_ID,
                           CATEGORY_NAME = a.CATEGORY_NAME,
                           OPN_QNT = a.OPN_QNT,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = a.ITEM_GROUP_NAME,
                           ITEM_UNIQUE_NAME = a.ITEM_UNIQUE_NAME,
                           REGIONAL_LANGUAGE = a.REGIONAL_LANGUAGE,
                           SALES_PRICE_BEFOR_TAX = a.SALES_PRICE_BEFOR_TAX,
                           IS_DELETE = a.IS_DELETE,
                           INCLUDE_TAX = a.INCLUDE_TAX,

                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           MODIFICATION_DATE = a.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = a.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = a.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = a.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = a.WEIGHT_OF_PAPER,
                           GODOWN = a.GODOWN,
                           DATE = a.DATE,
                           COMPANY_NAME = a.COMPANY_NAME,


                           IMAGE_PATH = a.IMAGE_PATH,


                           BUSINESS_LOC = a.BUSSINESS_LOCATION,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_SALES_ID = a.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = a.PURCHAGE_UNIT_ID,



                           TAX_COLLECTED_NAME = a.TAX_COLLECTED_NAME,
                           TAX_PAID_NAME = a.TAX_PAID_NAME,
                           Total = 0,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);

        }


        [HttpPost]
        public HttpResponseMessage UpdateLocation(ItemModel _ItemModel)
        {

            var str = (from a in db.TBL_ITEMS where a.ITEM_NAME == _ItemModel.ITEM_NAME && a.BARCODE == _ItemModel.BARCODE select a).FirstOrDefault();
            var str1 = (from a in db.TBL_ITEM_LOCATION where a.ITEM_LOCATION_NAME == str.ITEM_LOCATION_NAME && a.GOWDOWN_ID == _ItemModel.GODOWN_ID select a).FirstOrDefault();
            var strLoc = (from a in db.TBL_ITEM_LOCATION where a.LOCATION_ID == _ItemModel.LOCATION_ID && a.GOWDOWN_ID == _ItemModel.GODOWN_ID select a).FirstOrDefault();

            if (str1 != null)
            {
                str1.ITEM_LOCATION_NAME = _ItemModel.ITEM_LOCATION_NAME;
                str1.SORT_INDEX = _ItemModel.SORT_INDEX;
            }
            //if (strLoc != null)
            //{
            //    strLoc.ITEM_LOCATION_NAME = _ItemModel.ITEM_LOCATION_NAME;
            //    strLoc.SORT_INDEX = _ItemModel.SORT_INDEX;

            //}
            str.ITEM_LOCATION_NAME = _ItemModel.ITEM_LOCATION_NAME;

            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");

        }

        [HttpGet]
        public HttpResponseMessage TaxPaidValue(int id)
        {
            var str = (from a in db.TBL_ITEMS
                       join b in db.TBL_TAX on a.TAX_PAID_ID equals b.TAX_ID
                       where a.ITEM_ID == id && a.IS_DELETE == false
                       select new ItemModel
                       {
                           TAX_COLLECTED = a.TAX_COLLECTED,
                           TAX_COLLECTED_NAME = a.TAX_COLLECTED_NAME,
                           IMAGE_PATH = a.IMAGE_PATH,
                           ITEM_DESCRIPTION = a.ITEM_DESCRIPTION,
                           ITEM_PRICE = a.ITEM_PRICE,
                           ITEM_INVOICE_ID = a.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = a.ITEM_PRODUCT_ID,
                           KEYWORD = a.KEYWORD,
                           ACCESSORIES_KEYWORD = a.ACCESSORIES_KEYWORD,
                           CATAGORY_ID = a.CATAGORY_ID,
                           SEARCH_CODE = a.SERCH_CODE,

                           PURCHASE_UNIT = a.PURCHASE_UNIT,
                           SALES_UNIT = a.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           MRP = a.MRP,
                           COMPANY_ID = a.COMPANY_ID,
                           INCLUDE_TAX = a.INCLUDE_TAX,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           CATEGORY_NAME = a.CATEGORY_NAME,
                           OPN_QNT = a.OPN_QNT,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = a.ITEM_GROUP_NAME,
                           REGIONAL_LANGUAGE = a.REGIONAL_LANGUAGE,
                           ITEM_UNIQUE_NAME = a.ITEM_UNIQUE_NAME,
                           MODIFICATION_DATE = a.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = a.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = a.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = a.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = a.WEIGHT_OF_PAPER,
                           IS_DELETE = a.IS_DELETE,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_SALES_ID = a.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = a.PURCHAGE_UNIT_ID,
                           DATE = System.DateTime.Now,
                           ITEM_NAME = a.ITEM_NAME,
                           BARCODE = a.BARCODE,
                           TAX_PAID_NAME = b.TAX_NAME,
                           TAX_PAID = b.TAX_VALUE,
                           SALES_PRICE_BEFOR_TAX = a.SALES_PRICE_BEFOR_TAX,
                           SALES_PRICE = a.SALES_PRICE,
                           ITEM_ID = a.ITEM_ID,
                           Total = 0,
                       }
                    ).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage GetEditItem(int id)
        {

            var str = (from a in db.View_ITEM_ATTRIBUTE
                       where a.ITEM_ID == id && a.IS_DELETE == false
                       select new ItemModel
                       {
                           IMAGE_PATH = a.IMAGE_PATH,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_NAME = a.ITEM_NAME,
                           ITEM_LOCATION = a.ITEM_LOCATION,
                           ITEM_DESCRIPTION = a.ITEM_DESCRIPTION,
                           ITEM_PRICE = a.ITEM_PRICE,
                           ITEM_INVOICE_ID = a.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = a.ITEM_PRODUCT_ID,
                           KEYWORD = a.KEYWORD,
                           ACCESSORIES_KEYWORD = a.ACCESSORIES_KEYWORD,
                           BARCODE = a.BARCODE,
                           CATAGORY_ID = a.CATAGORY_ID,
                           SEARCH_CODE = a.SERCH_CODE,

                           TAX_PAID = a.TAX_PAID,
                           TAX_COLLECTED = a.TAX_COLLECTED,
                           PURCHASE_UNIT = a.PURCHASE_UNIT,
                           SALES_UNIT = a.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_PRICE = a.SALES_PRICE,
                           MRP = a.MRP,
                           COMPANY_ID = a.COMPANY_ID,
                           INCLUDE_TAX = a.INCLUDE_TAX,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           CATEGORY_NAME = a.CATEGORY_NAME,
                           OPN_QNT = a.OPN_QNT,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = a.ITEM_GROUP_NAME,
                           SALES_PRICE_BEFOR_TAX = a.SALES_PRICE_BEFOR_TAX,
                           REGIONAL_LANGUAGE = a.REGIONAL_LANGUAGE,
                           ITEM_UNIQUE_NAME = a.ITEM_UNIQUE_NAME,
                           MODIFICATION_DATE = a.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = a.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = a.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = a.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = a.WEIGHT_OF_PAPER,
                           IS_DELETE = a.IS_DELETE,
                           GODOWN = a.GODOWN,
                           DATE = a.DATE,
                           BUSINESS_LOC = a.BUSSINESS_LOCATION,
                           OPENING_STOCK_ID = a.OPENING_STOCK_ID,
                           TAX_PAID_NAME = a.TAX_PAID_NAME,
                           TAX_COLLECTED_NAME = a.TAX_COLLECTED_NAME,


                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_SALES_ID = a.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = a.PURCHAGE_UNIT_ID,
                           COMPANY_NAME = a.COMPANY_NAME,
                           Total = 0,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);


        }



        public HttpResponseMessage GetEditItem1(int id)
        {

            //var str = (from a in db.TBL_ITEMS
            //           join b in db.TBL_ITEM_LOCATION on a.ITEM_LOCATION_NAME  equals b.ITEM_LOCATION_NAME 
            //           where a.ITEM_ID == id && a.IS_DELETE == false
            //           select new ItemModel
            //           {

            var str = (from a in db.TBL_ITEMS
                       join b in db.TBL_ITEM_LOCATION on a.ITEM_LOCATION_NAME equals b.ITEM_LOCATION_NAME
                       where a.ITEM_ID == id && a.IS_DELETE == false
                       select new ItemModel
                       {

                           IMAGE_PATH = a.IMAGE_PATH,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_NAME = a.ITEM_NAME,
                           ITEM_LOCATION = a.ITEM_LOCATION,
                           ITEM_LOCATION_NAME = a.ITEM_LOCATION_NAME,
                           SORT_INDEX = b.SORT_INDEX,
                           ITEM_DESCRIPTION = a.ITEM_DESCRIPTION,
                           ITEM_PRICE = a.ITEM_PRICE,
                           ITEM_INVOICE_ID = a.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = a.ITEM_PRODUCT_ID,
                           KEYWORD = a.KEYWORD,
                           ACCESSORIES_KEYWORD = a.ACCESSORIES_KEYWORD,
                           BARCODE = a.BARCODE,
                           CATAGORY_ID = a.CATAGORY_ID,
                           SEARCH_CODE = a.SERCH_CODE,

                           TAX_PAID = a.TAX_PAID,
                           TAX_COLLECTED = a.TAX_COLLECTED,
                           PURCHASE_UNIT = a.PURCHASE_UNIT,
                           SALES_UNIT = a.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = a.PURCHASE_UNIT_PRICE,
                           SALES_PRICE = a.SALES_PRICE,
                           MRP = a.MRP,
                           COMPANY_ID = a.COMPANY_ID,
                           INCLUDE_TAX = a.INCLUDE_TAX,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = a.IS_ACIVE,
                           IS_Gift_Card = false,
                           CATEGORY_NAME = a.CATEGORY_NAME,
                           OPN_QNT = a.OPN_QNT,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = a.ITEM_GROUP_NAME,
                           SALES_PRICE_BEFOR_TAX = a.SALES_PRICE_BEFOR_TAX,
                           REGIONAL_LANGUAGE = a.REGIONAL_LANGUAGE,
                           ITEM_UNIQUE_NAME = a.ITEM_UNIQUE_NAME,
                           MODIFICATION_DATE = a.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = a.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = a.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = a.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = a.WEIGHT_OF_PAPER,
                           IS_DELETE = a.IS_DELETE,
                           //GODOWN = a.GODOWN,
                           //DATE = a.DATE,
                           DATE = System.DateTime.Now,
                           //BUSINESS_LOC = a.BUSSINESS_LOCATION,
                           //OPENING_STOCK_ID = a.OPENING_STOCK_ID,
                           TAX_PAID_NAME = a.TAX_PAID_NAME,
                           TAX_COLLECTED_NAME = a.TAX_COLLECTED_NAME,
                           BUSS_LOC_ID = a.BUSS_LOC_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           UNIT_SALES_ID = a.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = a.PURCHAGE_UNIT_ID,
                           //COMPANY_NAME = a.COMPANY_NAME,
                           Total = 0,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);


        }


        [HttpPost]
        public HttpResponseMessage ItemLocationAdd(ItemModel IM)
        {
            try
            {

                TBL_ITEM_LOCATION il = new TBL_ITEM_LOCATION();
                il.COMPANY_ID = IM.COMPANY_ID;
                il.GOWDOWN_ID = IM.GODOWN_ID;
                il.ITEM_LOCATION_NAME = IM.ITEM_LOCATION_NAME;
                il.SORT_INDEX = IM.SORT_INDEX;
                il.IS_DELETE = false;
                il.USER_ID = IM.USER_ID;
                il.IS_DELETE = false;
                //il.ITEM_NAME = IM.ITEM_NAME;
                //il.BARCODE = IM.BARCODE;
                db.TBL_ITEM_LOCATION.Add(il);
                db.SaveChanges();


            }

            catch (Exception ex)
            {

                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }


        [HttpGet]
        //public HttpResponseMessage DeleteLocation(int id)
        //{

        public HttpResponseMessage DeleteLocation(string id)
        {
            //var str = (from a in db.TBL_ITEM_LOCATION where a.ITEM_LOCATION_NAME == id select a).FirstOrDefault();
            //if (str != null)
            //{
            //    str.IS_DELETE = true;
            //}

            var str1 = (from a in db.TBL_ITEMS where a.ITEM_LOCATION_NAME == id select a).FirstOrDefault();
            if (str1 != null)
            {
                str1.ITEM_LOCATION_NAME = null;
            }
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");


        }



        [HttpGet]
        public HttpResponseMessage CheckItemQuantity(int id)
        {
            List<ItemModel> _ItemEstimate = new List<ItemModel>();
            //var str = (from a in db.TBL_ITEMS where a.ITEM_ID == id select a.OPN_QNT).FirstOrDefault();
            //return Request.CreateResponse(HttpStatusCode.OK, str);
            var str = (from a in db.TBL_ITEMS where a.ITEM_ID == id select a).FirstOrDefault();
            TBL_HOLD_INVOICE invoice = new TBL_HOLD_INVOICE();
            invoice.INVOICE_NO = Convert.ToString(str.ITEM_INVOICE_ID);
            invoice.INVOICE_ID = str.ITEM_INVOICE_ID.Value;
            invoice.BUSSINESS_LOCATION_ID = str.BUSS_LOC_ID;
            invoice.TOTAL_AMOUNT = Convert.ToInt32(str.ITEM_PRICE.Value);
            invoice.TOTAL_TAX = str.TAX_COLLECTED;
            db.TBL_HOLD_INVOICE.Add(invoice);
            if (str != null) 
            {
                _ItemEstimate.Add(new ItemModel
                {
                    IMAGE_PATH = str.IMAGE_PATH,
                    ITEM_ID = str.ITEM_ID,
                    ITEM_NAME = str.ITEM_NAME,
                    ITEM_DESCRIPTION = str.ITEM_DESCRIPTION,
                    ITEM_PRICE = str.ITEM_PRICE,
                    ITEM_INVOICE_ID = str.ITEM_INVOICE_ID,
                    ITEM_PRODUCT_ID = str.ITEM_PRODUCT_ID,
                    KEYWORD = str.KEYWORD,
                    ACCESSORIES_KEYWORD = str.ACCESSORIES_KEYWORD,
                    BARCODE = str.BARCODE,
                    CATAGORY_ID = str.CATAGORY_ID,
                    CATEGORY_NAME = str.CATEGORY_NAME,
                    //SEARCH_CODE = str.SEARCH_CODE,
                    TAX_PAID = str.TAX_PAID,
                    TAX_COLLECTED = str.TAX_COLLECTED,

                    //BUSINESS_LOC = str.b,
                    BUSS_LOC_ID = str.BUSS_LOC_ID,
                    GODOWN_ID = str.GODOWN_ID,
                    UNIT_SALES_ID = str.SALE_UNIT_ID,
                    UNIT_PURCHAGE_ID = str.PURCHAGE_UNIT_ID,
                    PURCHASE_UNIT = str.PURCHASE_UNIT,
                    SALES_UNIT = str.SALES_UNIT,
                    PURCHASE_UNIT_PRICE = str.PURCHASE_UNIT_PRICE,
                    SALES_PRICE = str.SALES_PRICE,
                    MRP = str.MRP,
                    COMPANY_ID = str.COMPANY_ID,

                    OPN_QNT = str.OPN_QNT,
                    DISPLAY_INDEX = str.DISPLAY_INDEX,
                    ITEM_GROUP_NAME = str.ITEM_GROUP_NAME,
                    ITEM_UNIQUE_NAME = str.ITEM_UNIQUE_NAME,
                    REGIONAL_LANGUAGE = str.REGIONAL_LANGUAGE,
                    SALES_PRICE_BEFOR_TAX = str.SALES_PRICE_BEFOR_TAX,
                    IS_DELETE = str.IS_DELETE,
                    INCLUDE_TAX = str.INCLUDE_TAX,
                    IS_Shortable_Item = false,
                    IS_Purchased = false,
                    IS_Service_Item = false,
                    IS_For_Online_Shop = false,
                    IS_Not_for_online_shop = false,
                    IS_Not_For_Sell = false,
                    ALLOW_PURCHASE_ON_ESHOP = false,
                    IS_ACTIVE = str.IS_ACIVE,
                    IS_Gift_Card = false,
                    MODIFICATION_DATE = str.MODIFICATION_DATE,
                    WEIGHT_OF_CARDBOARD = str.WEIGHT_OF_CARDBOARD,
                    WEIGHT_OF_FOAM = str.WEIGHT_OF_FOAM,
                    WEIGHT_OF_PLASTIC = str.WEIGHT_OF_PLASTIC,
                    WEIGHT_OF_PAPER = str.WEIGHT_OF_PAPER,
                    GODOWN = str.GODOWN_ID.Value.ToString(),
                    DATE = str.MODIFICATION_DATE,
                    //COMPANY_NAME = str.c,
                    TAX_COLLECTED_NAME = str.TAX_COLLECTED_NAME,
                    TAX_PAID_NAME = str.TAX_PAID_NAME,
                    TaxName = str.TAX_COLLECTED_NAME,
                    TaxValue = str.TAX_PAID,
                    //Current_Qty = str.,
                    Total = Convert.ToDecimal(str.ITEM_PRICE),
                });
            }
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, _ItemEstimate);
        }
        [HttpGet]
        public HttpResponseMessage DeleteItem(int id)
        {
            var str = (from a in db.TBL_ITEMS where a.ITEM_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");


        }
        [HttpGet]
        public HttpResponseMessage GetInvoiceItem(int id)
        {
            var str = (from a in db.TBL_ITEMS where a.ITEM_ID == id select a).FirstOrDefault();
            TBL_HOLD_INVOICE invoice = new TBL_HOLD_INVOICE();
            invoice.INVOICE_NO = Convert.ToString(str.ITEM_INVOICE_ID);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");


        }
        [HttpPost]
        public HttpResponseMessage UpdateItemQnt(ItemModel _ItemModel)
        {
            var str = (from a in db.TBL_ITEMS where a.ITEM_ID == _ItemModel.ITEM_ID select a).FirstOrDefault();
            str.OPN_QNT = _ItemModel.WeightQnt;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");


        }
        [HttpPost]
        public HttpResponseMessage CreateItem(ItemModel _ItemModel)
        {
            try
            {


                if (_ItemModel.ITEM_ID == null || _ItemModel.ITEM_ID == 0)
                {


                    TBL_ITEMS item = new TBL_ITEMS();

                    item.ITEM_NAME = _ItemModel.ITEM_NAME;
                    item.ITEM_DESCRIPTION = _ItemModel.ITEM_DESCRIPTION;
                    item.ITEM_PRICE = _ItemModel.ITEM_PRICE;
                    item.ITEM_INVOICE_ID = _ItemModel.ITEM_INVOICE_ID;
                    item.ITEM_PRODUCT_ID = _ItemModel.ITEM_PRODUCT_ID;
                    item.KEYWORD = _ItemModel.KEYWORD;
                    item.ACCESSORIES_KEYWORD = _ItemModel.ACCESSORIES_KEYWORD;
                    item.BARCODE = _ItemModel.BARCODE;
                    item.CATAGORY_ID = _ItemModel.CATAGORY_ID;
                    item.CATAGORY = _ItemModel.CATEGORY_NAME;
                    item.SERCH_CODE = _ItemModel.SEARCH_CODE;
                    item.TAX_PAID = _ItemModel.TAX_PAID;
                    item.TAX_COLLECTED = _ItemModel.TAX_COLLECTED;
                    item.PURCHASE_UNIT = _ItemModel.PURCHASE_UNIT;
                    item.SALES_UNIT = _ItemModel.SALES_UNIT;
                    item.PURCHASE_UNIT_PRICE = _ItemModel.PURCHASE_UNIT_PRICE;
                    item.SALES_PRICE = _ItemModel.SALES_PRICE;
                    item.MRP = _ItemModel.MRP;
                    item.COMPANY_ID = _ItemModel.COMPANY_ID;
                    item.DISPLAY_INDEX = _ItemModel.DISPLAY_INDEX;
                    item.ITEM_GROUP_NAME = _ItemModel.ITEM_GROUP_NAME;
                    item.ITEM_UNIQUE_NAME = _ItemModel.ITEM_UNIQUE_NAME;
                    item.REGIONAL_LANGUAGE = _ItemModel.REGIONAL_LANGUAGE;
                    item.SALES_PRICE_BEFOR_TAX = _ItemModel.SALES_PRICE_BEFOR_TAX;
                    item.MODIFICATION_DATE = System.DateTime.Now;
                    item.CATEGORY_NAME = _ItemModel.CATEGORY_NAME;
                    item.OPN_QNT = _ItemModel.OPN_QNT;
                    item.WEIGHT_OF_CARDBOARD = _ItemModel.WEIGHT_OF_CARDBOARD;
                    item.WEIGHT_OF_FOAM = _ItemModel.WEIGHT_OF_FOAM;
                    item.WEIGHT_OF_PAPER = _ItemModel.WEIGHT_OF_PAPER;
                    item.WEIGHT_OF_PLASTIC = _ItemModel.WEIGHT_OF_PLASTIC;
                    item.IS_DELETE = false;
                    item.BUSS_LOC_ID = _ItemModel.BUSS_LOC_ID;
                    item.GODOWN_ID = _ItemModel.GODOWN_ID;
                    item.TAX_COLLECTED_ID = _ItemModel.TAX_COLLECTED_ID;
                    item.TAX_PAID_ID = _ItemModel.TAX_PAID_ID;
                    item.PURCHAGE_UNIT_ID = _ItemModel.UNIT_PURCHAGE_ID;
                    item.SALE_UNIT_ID = _ItemModel.UNIT_SALES_ID;
                    item.ITEM_LOCATION = _ItemModel.BUSINESS_LOC;
                    item.IMAGE_PATH = _ItemModel.IMAGE_PATH;
                    item.TAX_COLLECTED_NAME = _ItemModel.TAX_COLLECTED_NAME;
                    item.TAX_PAID_NAME = _ItemModel.TAX_PAID_NAME;
                    item.INCLUDE_TAX = _ItemModel.INCLUDE_TAX;
                    item.IS_ACIVE = _ItemModel.IS_ACTIVE;
                    db.TBL_ITEMS.Add(item);
                    db.SaveChanges();
                    var itemid = item.ITEM_ID;
                    //TBL_ITEMS_ATTRIBUTE _ITEM_ATTRIBUTE = new TBL_ITEMS_ATTRIBUTE();
                    //_ITEM_ATTRIBUTE.ALLOW_PURCHASE_ON_ESHOP = _ItemModel.ALLOW_PURCHASE_ON_ESHOP;
                    //_ITEM_ATTRIBUTE.NOT_FOR_ONLINE_SHOP = _ItemModel.IS_Not_for_online_shop;
                    //_ITEM_ATTRIBUTE.IS_PURCHASED = _ItemModel.IS_Purchased;
                    //_ITEM_ATTRIBUTE.IS_SERVICE = _ItemModel.IS_Service_Item;
                    //_ITEM_ATTRIBUTE.ONLY_ONLINE_SHOP = _ItemModel.IS_For_Online_Shop;
                    //_ITEM_ATTRIBUTE.NOT_FOR_SALE = _ItemModel.IS_Not_For_Sell;
                    //_ITEM_ATTRIBUTE.INCLUDE_TAX = _ItemModel.INCLUDE_TAX;
                    //_ITEM_ATTRIBUTE.IS_ACTIVE = _ItemModel.IS_ACTIVE;
                    //_ITEM_ATTRIBUTE.IS_GIFT_CARD = _ItemModel.IS_Gift_Card;
                    //_ITEM_ATTRIBUTE.INCLUDE_TAX = _ItemModel.INCLUDE_TAX;
                    //_ITEM_ATTRIBUTE.IS_SORTABLE_ITEM = _ItemModel.IS_Shortable_Item;
                    //_ITEM_ATTRIBUTE.ITEM_ID = itemid;
                    //db.TBL_ITEMS_ATTRIBUTE.Add(_ITEM_ATTRIBUTE);
                    //db.SaveChanges();


                    TBL_OPENING_STOCK op = new TBL_OPENING_STOCK();
                    op.BARCODE = _ItemModel.BARCODE;
                    op.BUSINESS_LOC_ID = _ItemModel.BUSINESS_LOCATION_ID;
                    op.COMPANY_ID = _ItemModel.COMPANY_ID;
                    //op.DATE = System.DateTime.Now;
                    string dsf = Convert.ToDateTime(_ItemModel.DATE).ToString("yyyy-MM-dd");
                    op.DATE = Convert.ToDateTime(dsf);
                    op.GODOWN = _ItemModel.GODOWN;
                    op.BUSSINESS_LOCATION = _ItemModel.BUSINESS_LOC;
                    op.ITEM_ID = itemid;
                    op.ITEM_NAME = _ItemModel.ITEM_NAME;
                    op.OPN_QNT = _ItemModel.OPN_QNT;
                    db.TBL_OPENING_STOCK.Add(op);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "success");
                }
                else
                {
                    //-----Update Item----//
                    var str = (from a in db.TBL_ITEMS where a.ITEM_ID == _ItemModel.ITEM_ID select a).FirstOrDefault();

                    str.ITEM_NAME = _ItemModel.ITEM_NAME;
                    str.OPN_QNT = _ItemModel.OPN_QNT;
                    str.ITEM_DESCRIPTION = _ItemModel.ITEM_DESCRIPTION;
                    str.ITEM_PRICE = _ItemModel.ITEM_PRICE;
                    str.ITEM_INVOICE_ID = _ItemModel.ITEM_INVOICE_ID;
                    str.ITEM_PRODUCT_ID = _ItemModel.ITEM_PRODUCT_ID;
                    str.KEYWORD = _ItemModel.KEYWORD;
                    str.ACCESSORIES_KEYWORD = _ItemModel.ACCESSORIES_KEYWORD;
                    str.BARCODE = _ItemModel.BARCODE;
                    str.CATAGORY_ID = _ItemModel.CATAGORY_ID;
                    str.CATEGORY_NAME = _ItemModel.CATEGORY_NAME;
                    str.SERCH_CODE = _ItemModel.SEARCH_CODE;
                    str.TAX_PAID = _ItemModel.TAX_PAID;
                    str.TAX_COLLECTED = _ItemModel.TAX_COLLECTED;
                    str.PURCHASE_UNIT = _ItemModel.PURCHASE_UNIT;
                    str.SALES_UNIT = _ItemModel.SALES_UNIT;
                    str.PURCHASE_UNIT_PRICE = _ItemModel.PURCHASE_UNIT_PRICE;
                    str.SALES_PRICE = _ItemModel.SALES_PRICE;
                    str.MRP = _ItemModel.MRP;
                    str.DISPLAY_INDEX = _ItemModel.DISPLAY_INDEX;
                    str.ITEM_GROUP_NAME = _ItemModel.ITEM_GROUP_NAME;
                    str.ITEM_UNIQUE_NAME = _ItemModel.ITEM_UNIQUE_NAME;
                    str.REGIONAL_LANGUAGE = _ItemModel.REGIONAL_LANGUAGE;
                    str.SALES_PRICE_BEFOR_TAX = _ItemModel.SALES_PRICE_BEFOR_TAX;
                    str.WEIGHT_OF_CARDBOARD = _ItemModel.WEIGHT_OF_CARDBOARD;
                    str.WEIGHT_OF_FOAM = _ItemModel.WEIGHT_OF_FOAM;
                    str.WEIGHT_OF_PAPER = _ItemModel.WEIGHT_OF_PAPER;
                    str.WEIGHT_OF_PLASTIC = _ItemModel.WEIGHT_OF_PLASTIC;
                    str.IMAGE_PATH = _ItemModel.IMAGE_PATH;
                    str.TAX_COLLECTED_NAME = _ItemModel.TAX_COLLECTED_NAME;
                    str.TAX_PAID_NAME = _ItemModel.TAX_PAID_NAME;
                    str.MODIFICATION_DATE = System.DateTime.Now;



                    str.BUSS_LOC_ID = _ItemModel.BUSS_LOC_ID;
                    str.GODOWN_ID = _ItemModel.GODOWN_ID;
                    str.TAX_COLLECTED_ID = _ItemModel.TAX_COLLECTED_ID;
                    str.TAX_PAID_ID = _ItemModel.TAX_PAID_ID;
                    str.PURCHAGE_UNIT_ID = _ItemModel.UNIT_PURCHAGE_ID;
                    str.SALE_UNIT_ID = _ItemModel.UNIT_SALES_ID;
                    str.IS_ACIVE = _ItemModel.IS_ACTIVE;

                    var _ITEM_ATTRIBUTE = (from a in db.TBL_ITEMS_ATTRIBUTE where a.ITEM_ID == _ItemModel.ITEM_ID select a).FirstOrDefault();
                    _ITEM_ATTRIBUTE.ALLOW_PURCHASE_ON_ESHOP = _ItemModel.ALLOW_PURCHASE_ON_ESHOP;
                    _ITEM_ATTRIBUTE.NOT_FOR_ONLINE_SHOP = _ItemModel.IS_Not_for_online_shop;
                    _ITEM_ATTRIBUTE.IS_PURCHASED = _ItemModel.IS_Purchased;
                    _ITEM_ATTRIBUTE.IS_SERVICE = _ItemModel.IS_Service_Item;
                    _ITEM_ATTRIBUTE.ONLY_ONLINE_SHOP = _ItemModel.IS_For_Online_Shop;
                    _ITEM_ATTRIBUTE.NOT_FOR_SALE = _ItemModel.IS_Not_For_Sell;
                    _ITEM_ATTRIBUTE.INCLUDE_TAX = _ItemModel.INCLUDE_TAX;
                    _ITEM_ATTRIBUTE.IS_SORTABLE_ITEM = _ItemModel.IS_Shortable_Item;
                    _ITEM_ATTRIBUTE.IS_ACTIVE = _ItemModel.IS_ACTIVE;
                    _ITEM_ATTRIBUTE.IS_GIFT_CARD = _ItemModel.IS_Gift_Card;
                    _ITEM_ATTRIBUTE.INCLUDE_TAX = _ItemModel.INCLUDE_TAX;


                    var aa = (from a in db.TBL_OPENING_STOCK where a.OPENING_STOCK_ID == _ItemModel.OPENING_STOCK_ID select a).FirstOrDefault();
                    aa.BARCODE = _ItemModel.BARCODE;
                    aa.BUSSINESS_LOCATION = _ItemModel.BUSINESS_LOC;
                    aa.CLOSING_BAL = _ItemModel.CLOSING_BAL;
                    aa.COMPANY_ID = _ItemModel.COMPANY_ID;
                    aa.COMPANY_NAME = _ItemModel.COMPANY_NAME;
                    aa.DATE = System.DateTime.Now;
                    aa.GODOWN = _ItemModel.GODOWN;
                    aa.ITEM_NAME = _ItemModel.ITEM_NAME;
                    aa.OPN_QNT = _ItemModel.OPN_QNT;
                    aa.OPRNING_BAL = _ItemModel.OPRNING_BAL;


                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "success");

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        public HttpResponseMessage ItemUpdate(ItemModel _ItemModel)
        {
            var str = (from a in db.TBL_ITEMS where a.ITEM_ID == _ItemModel.ITEM_ID select a).FirstOrDefault();

            str.ITEM_NAME = _ItemModel.ITEM_NAME;
            str.OPN_QNT = _ItemModel.OPN_QNT;
            str.ITEM_DESCRIPTION = _ItemModel.ITEM_DESCRIPTION;
            str.ITEM_PRICE = _ItemModel.ITEM_PRICE;
            str.ITEM_INVOICE_ID = _ItemModel.ITEM_INVOICE_ID;
            str.ITEM_PRODUCT_ID = _ItemModel.ITEM_PRODUCT_ID;
            str.KEYWORD = _ItemModel.KEYWORD;
            str.ACCESSORIES_KEYWORD = _ItemModel.ACCESSORIES_KEYWORD;
            str.BARCODE = _ItemModel.BARCODE;
            str.CATAGORY_ID = _ItemModel.CATAGORY_ID;
            str.CATEGORY_NAME = _ItemModel.CATEGORY_NAME;
            str.SERCH_CODE = _ItemModel.SEARCH_CODE;
            str.TAX_PAID = _ItemModel.TAX_PAID;
            str.TAX_COLLECTED = _ItemModel.TAX_COLLECTED;
            str.PURCHASE_UNIT = _ItemModel.PURCHASE_UNIT;
            str.SALES_UNIT = _ItemModel.SALES_UNIT;
            str.PURCHASE_UNIT_PRICE = _ItemModel.PURCHASE_UNIT_PRICE;
            str.SALES_PRICE = _ItemModel.SALES_PRICE;
            str.MRP = _ItemModel.MRP;
            str.DISPLAY_INDEX = _ItemModel.DISPLAY_INDEX;
            str.ITEM_GROUP_NAME = _ItemModel.ITEM_GROUP_NAME;
            str.ITEM_UNIQUE_NAME = _ItemModel.ITEM_UNIQUE_NAME;
            str.REGIONAL_LANGUAGE = _ItemModel.REGIONAL_LANGUAGE;
            str.SALES_PRICE_BEFOR_TAX = _ItemModel.SALES_PRICE_BEFOR_TAX;
            str.WEIGHT_OF_CARDBOARD = _ItemModel.WEIGHT_OF_CARDBOARD;
            str.WEIGHT_OF_FOAM = _ItemModel.WEIGHT_OF_FOAM;
            str.WEIGHT_OF_PAPER = _ItemModel.WEIGHT_OF_PAPER;
            str.WEIGHT_OF_PLASTIC = _ItemModel.WEIGHT_OF_PLASTIC;
            str.IMAGE_PATH = _ItemModel.IMAGE_PATH;
            str.TAX_COLLECTED_NAME = _ItemModel.TAX_COLLECTED_NAME;
            str.TAX_PAID_NAME = _ItemModel.TAX_PAID_NAME;
            str.MODIFICATION_DATE = System.DateTime.Now;



            str.BUSS_LOC_ID = _ItemModel.BUSS_LOC_ID;
            str.GODOWN_ID = _ItemModel.GODOWN_ID;
            str.TAX_COLLECTED_ID = _ItemModel.TAX_COLLECTED_ID;
            str.TAX_PAID_ID = _ItemModel.TAX_PAID_ID;
            str.PURCHAGE_UNIT_ID = _ItemModel.UNIT_PURCHAGE_ID;
            str.SALE_UNIT_ID = _ItemModel.UNIT_SALES_ID;
            str.IS_ACIVE = _ItemModel.IS_ACTIVE;

            //var _ITEM_ATTRIBUTE = (from a in db.TBL_ITEMS_ATTRIBUTE where a.ITEM_ID == _ItemModel.ITEM_ID select a).FirstOrDefault();
            //_ITEM_ATTRIBUTE.ALLOW_PURCHASE_ON_ESHOP = _ItemModel.ALLOW_PURCHASE_ON_ESHOP;
            //_ITEM_ATTRIBUTE.NOT_FOR_ONLINE_SHOP = _ItemModel.IS_Not_for_online_shop;
            //_ITEM_ATTRIBUTE.IS_PURCHASED = _ItemModel.IS_Purchased;
            //_ITEM_ATTRIBUTE.IS_SERVICE = _ItemModel.IS_Service_Item;
            //_ITEM_ATTRIBUTE.ONLY_ONLINE_SHOP = _ItemModel.IS_For_Online_Shop;
            //_ITEM_ATTRIBUTE.NOT_FOR_SALE = _ItemModel.IS_Not_For_Sell;
            //_ITEM_ATTRIBUTE.INCLUDE_TAX = _ItemModel.INCLUDE_TAX;
            //_ITEM_ATTRIBUTE.IS_SORTABLE_ITEM = _ItemModel.IS_Shortable_Item;
            //_ITEM_ATTRIBUTE.IS_ACTIVE = _ItemModel.IS_ACTIVE;
            //_ITEM_ATTRIBUTE.IS_GIFT_CARD = _ItemModel.IS_Gift_Card;
            //_ITEM_ATTRIBUTE.INCLUDE_TAX = _ItemModel.INCLUDE_TAX;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

    }
}
