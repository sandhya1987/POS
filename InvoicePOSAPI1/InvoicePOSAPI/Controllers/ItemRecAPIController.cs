using InvoicePOSAPI.Models;
using InvoicePOSDATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvoicePOSAPI.Controllers
{
    public class ItemRecAPIController : ApiController
    {
        ReceiveItem im = new ReceiveItem();
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage GetAllRecItem(int id)
        {

            string value = db.TBL_RECEIVE_ITEM
                            .OrderByDescending(p => p.RECEIVED_ITEM_ENTRY_NO)
                            .Select(r => r.RECEIVED_ITEM_ENTRY_NO)
                            .First().ToString();


            var str = (from a in db.TBL_RECEIVE_ITEM
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new ReceiveItem
                       {
                           NEWRECEIVE_ID = a.NEWRECEIVE_ID,
                           COMPANY_ID = a.COMPANY_ID,
                           SELECT_BUSINESS_LOCATION_ID = a.SELECT_BUSINESS_LOCATION_ID,
                           RECEIVED_ITEM_ENTRY_NO = a.RECEIVED_ITEM_ENTRY_NO,
                           BARCODE = a.BARCODE,
                           ITEM_NAME = a.ITEM_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           SUPPLIER_INVOICE_NO = a.SUPPLIER_INVOICE_NO,
                           SUPPLIER_INVOICE_DATE = a.SUPPLIER_INVOICE_DATE,
                           RECEIVE_DATE = a.RECEIVE_DATE,
                           RECEIVED_ITEM_ON_DATE = a.RECEIVED_ITEM_ON_DATE,
                           PO_NUMBER = a.PO_NUMBER,
                           SUPPLIER_ID = a.SUPPLIER_ID,
                           PAYMENT_TERMS = a.PAYMENT_TERMS,
                           GODOWN_ID = a.GODOWN_ID,
                           GODOWN = a.GODOWN,
                           IS_REC_WITH_PO = a.IS_REC_WITH_PO,
                           IS_DELETE = a.IS_DELETE,
                           RECEIVING_EMPLOYEE = a.RECEIVING_EMPLOYEE,
                           DISCOUNT_FLAT = a.DISCOUNT_FLAT,
                           DISCOUNT_PERCENT = a.DISCOUNT_PERCENT,
                           ADDTIONAL_CHARGES = a.ADDTIONAL_CHARGES,
                           SUB_TOTAL_BEFORETAX = a.SUB_TOTAL_BEFORETAX,
                           TOTAL_TAX_AMT = a.TOTAL_TAX_AMT,
                           SUB_TOTAL = a.SUB_TOTAL_BEFORETAX,
                           ROUND_OFF_ADJUSTMENTAMT = a.ROUND_OFF_ADJUSTMENTAMT,
                           TOTAL_AMT = a.TOTAL_AMT,
                           USE_SUPPLIER_ADVANCE_AMT = a.USE_SUPPLIER_ADVANCE_AMT,
                           GRAND_TOTAL = a.GRAND_TOTAL,
                           NOTE = a.NOTE,
                           SUPPLIER = a.SUPPLIER,
                           SELECT_BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           PO_NUMBER_ID = a.PO_NUMBER_ID,
                           FILTER_HIERARCHY = a.FILTER_HIERARCHY,
                           DISCOUNT_BEFOR_TAX = a.DISCOUNT_BEFOR_TAX,
                           PAY_IN_CASH = a.PAY_IN_CASH,
                           APPLY_FPR_EFFECTVE_PRICE = a.APPLY_FPR_EFFECTVE_PRICE,
                           AUTO_SAVE_IN_DRAFT = a.AUTO_SAVE_IN_DRAFT,
                           FOCUS_LAST_ADD_ITEM = a.FOCUS_LAST_ADD_ITEM,
                           ITEM_ENTRY_TEMPLATE = a.ITEM_ENTRY_TEMPLATE,
                           INVOICE_DIS_AMT = a.INVOICE_DIS_AMT,
                           TAX_PAID =0,
                           PURCHASE_UNIT_PRICE =0,
                           SALES_PRICE = 0,
                           MRP = 0,
                           Current_Qty=1,


                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        //[HttpGet]
        //public HttpResponseMessage GetEditRecvItem1(int id, int poid)
        //{
        //    var str = (from a in db.TBL_PO_ITEMS
        //               join b in db.TBL_ITEMS on a.PO_ITEM_ID equals b.ITEM_ID
        //               join c in db.TBL_PO_PAYMENT on a.PO_NUMBER equals c.PO_ID 
        //               where a.PO_NUMBER == poid && b.COMPANY_ID == id
        //               select new ItemModel
        //               {
        //                   IMAGE_PATH = b.IMAGE_PATH,
        //                   ITEM_ID = b.ITEM_ID,
        //                   ITEM_NAME = b.ITEM_NAME,
        //                   ITEM_DESCRIPTION = b.ITEM_DESCRIPTION,
        //                   ITEM_PRICE = b.ITEM_PRICE,
        //                   ITEM_INVOICE_ID = b.ITEM_INVOICE_ID,
        //                   ITEM_PRODUCT_ID = b.ITEM_PRODUCT_ID,
        //                   KEYWORD = b.KEYWORD,
        //                   ACCESSORIES_KEYWORD = b.ACCESSORIES_KEYWORD,
        //                   BARCODE = b.BARCODE,
        //                   Current_Qty = a.PO_QTY,
        //                   CATAGORY_ID = b.CATAGORY_ID,
        //                   SEARCH_CODE = b.SERCH_CODE,
        //                   TAX_PAID = b.TAX_PAID,
        //                   TAX_COLLECTED = a.PO_TAX,
        //                   TOTPO_QNT = a.PO_QTY,
        //                   PURCHASE_UNIT = b.PURCHASE_UNIT,
        //                   SALES_UNIT = b.SALES_UNIT,
        //                   PURCHASE_UNIT_PRICE = b.PURCHASE_UNIT_PRICE,
        //                   SALES_PRICE = b.SALES_PRICE,
        //                   MRP = b.MRP,
        //                   COMPANY_ID = b.COMPANY_ID,
        //                   CATEGORY_NAME = b.CATEGORY_NAME,
        //                   OPN_QNT = b.OPN_QNT,
        //                   DISPLAY_INDEX = b.DISPLAY_INDEX,
        //                   ITEM_GROUP_NAME = b.ITEM_GROUP_NAME,
        //                   ITEM_UNIQUE_NAME = b.ITEM_UNIQUE_NAME,
        //                   REGIONAL_LANGUAGE = b.REGIONAL_LANGUAGE,
        //                   SALES_PRICE_BEFOR_TAX = b.SALES_PRICE_BEFOR_TAX,
        //                   IS_DELETE = b.IS_DELETE,
        //                   INCLUDE_TAX = b.INCLUDE_TAX,
        //                   IS_Shortable_Item = false,
        //                   IS_Purchased = false,
        //                   IS_Service_Item = false,
        //                   IS_For_Online_Shop = false,
        //                   IS_Not_for_online_shop = false,
        //                   IS_Not_For_Sell = false,
        //                   ALLOW_PURCHASE_ON_ESHOP = false,
        //                   IS_ACTIVE = b.IS_ACIVE,
        //                   IS_Gift_Card = false,
        //                   MODIFICATION_DATE = b.MODIFICATION_DATE,
        //                   WEIGHT_OF_CARDBOARD = b.WEIGHT_OF_CARDBOARD,
        //                   WEIGHT_OF_FOAM = b.WEIGHT_OF_FOAM,
        //                   WEIGHT_OF_PLASTIC = b.WEIGHT_OF_PLASTIC,
        //                   WEIGHT_OF_PAPER = b.WEIGHT_OF_PAPER,
        //                   GODOWN = "",
        //                   DATE = System.DateTime.Now,
        //                   COMPANY_NAME = "Infosolz",


        //                   BUSINESS_LOC = "Infosolz",
        //                   BUSS_LOC_ID = b.BUSS_LOC_ID,
        //                   GODOWN_ID = b.GODOWN_ID,
        //                   UNIT_SALES_ID = b.SALE_UNIT_ID,
        //                   UNIT_PURCHAGE_ID = b.PURCHAGE_UNIT_ID,
        //                   //Current_Qty = a.SALE_QTY,

        //                   TAX_COLLECTED_NAME = b.TAX_COLLECTED_NAME,
        //                   TAX_PAID_NAME = b.TAX_PAID_NAME,
        //                   Total = 0,
        //               }).ToList();
        //    return Request.CreateResponse(HttpStatusCode.OK, str);
        //}

        [HttpGet]
        public HttpResponseMessage GetRecvItem1(int id, int poid)
        {
            var str = (from a in db.TBL_PO_ITEMS
                       join b in db.TBL_ITEMS on a.PO_ITEM_ID equals b.ITEM_ID
                       join c in db.TBL_PO_PAYMENT on a.PO_NUMBER equals c.PO_ID 
                       where a.PO_NUMBER == poid && b.COMPANY_ID == id
                       select new ItemModel
                       {
                           IMAGE_PATH = b.IMAGE_PATH,
                           ITEM_ID = b.ITEM_ID,
                           ITEM_NAME = b.ITEM_NAME,
                           ITEM_DESCRIPTION = b.ITEM_DESCRIPTION,
                           ITEM_PRICE = b.ITEM_PRICE,
                           ITEM_INVOICE_ID = b.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = b.ITEM_PRODUCT_ID,
                           KEYWORD = b.KEYWORD,
                           ACCESSORIES_KEYWORD = b.ACCESSORIES_KEYWORD,
                           BARCODE = b.BARCODE,
                           Current_Qty = a.PO_QTY,
                           SUPPLIER_NAME=c.SUPPLIER_NAME,
                           TERMS=c.TERMS,
                           CATAGORY_ID = b.CATAGORY_ID,
                           SEARCH_CODE = b.SERCH_CODE,
                           TAX_PAID = b.TAX_PAID,
                           TAX_COLLECTED = a.PO_TAX,
                           TOTPO_QNT = a.PO_QTY,
                           PURCHASE_UNIT = b.PURCHASE_UNIT,
                           SALES_UNIT = b.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = b.PURCHASE_UNIT_PRICE,
                           SALES_PRICE = b.SALES_PRICE,
                           MRP = b.MRP,
                           COMPANY_ID = b.COMPANY_ID,
                           CATEGORY_NAME = b.CATEGORY_NAME,
                           OPN_QNT = b.OPN_QNT,
                           DISPLAY_INDEX = b.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = b.ITEM_GROUP_NAME,
                           ITEM_UNIQUE_NAME = b.ITEM_UNIQUE_NAME,
                           REGIONAL_LANGUAGE = b.REGIONAL_LANGUAGE,
                           SALES_PRICE_BEFOR_TAX = b.SALES_PRICE_BEFOR_TAX,
                           IS_DELETE = b.IS_DELETE,
                           INCLUDE_TAX = b.INCLUDE_TAX,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = b.IS_ACIVE,
                           IS_Gift_Card = false,
                           MODIFICATION_DATE = b.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = b.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = b.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = b.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = b.WEIGHT_OF_PAPER,
                           GODOWN = "",
                           DATE = System.DateTime.Now,
                           COMPANY_NAME = "Infosolz",
                           PO_NUMBER=c.PO_NUMBER,

                           BUSINESS_LOC = "Infosolz",
                           BUSS_LOC_ID = b.BUSS_LOC_ID,
                           GODOWN_ID = b.GODOWN_ID,
                           UNIT_SALES_ID = b.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = b.PURCHAGE_UNIT_ID,
                           //Current_Qty = a.SALE_QTY,

                           TAX_COLLECTED_NAME = b.TAX_COLLECTED_NAME,
                           TAX_PAID_NAME = b.TAX_PAID_NAME,
                           Total = 0,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }



        //[HttpGet]
        //public HttpResponseMessage GetRecvItem(int id)
        //{
        //    var str = (from a in db.TBL_PO_ITEMS
        //               join b in db.TBL_ITEMS on a.PO_ITEM_ID equals b.ITEM_ID
                      
        //               where b.COMPANY_ID == id
        //               select new ItemModel
        //               {
        //                   IMAGE_PATH = b.IMAGE_PATH,
        //                   ITEM_ID = b.ITEM_ID,
        //                   ITEM_NAME = b.ITEM_NAME,
        //                   ITEM_DESCRIPTION = b.ITEM_DESCRIPTION,
        //                   ITEM_PRICE = b.ITEM_PRICE,
        //                   ITEM_INVOICE_ID = b.ITEM_INVOICE_ID,
        //                   ITEM_PRODUCT_ID = b.ITEM_PRODUCT_ID,
        //                   KEYWORD = b.KEYWORD,
        //                   ACCESSORIES_KEYWORD = b.ACCESSORIES_KEYWORD,
        //                   BARCODE = b.BARCODE,
        //                   Current_Qty = a.PO_QTY,
        //                   //SUPPLIER_NAME = c.SUPPLIER_NAME,
        //                   //TERMS = c.TERMS,
        //                   CATAGORY_ID = b.CATAGORY_ID,
        //                   SEARCH_CODE = b.SERCH_CODE,
        //                   TAX_PAID = b.TAX_PAID,
        //                   TAX_COLLECTED = a.PO_TAX,
        //                   TOTPO_QNT = a.PO_QTY,
        //                   PURCHASE_UNIT = b.PURCHASE_UNIT,
        //                   SALES_UNIT = b.SALES_UNIT,
        //                   PURCHASE_UNIT_PRICE = b.PURCHASE_UNIT_PRICE,
        //                   SALES_PRICE = b.SALES_PRICE,
        //                   MRP = b.MRP,
        //                   COMPANY_ID = b.COMPANY_ID,
        //                   CATEGORY_NAME = b.CATEGORY_NAME,
        //                   OPN_QNT = b.OPN_QNT,
        //                   DISPLAY_INDEX = b.DISPLAY_INDEX,
        //                   ITEM_GROUP_NAME = b.ITEM_GROUP_NAME,
        //                   ITEM_UNIQUE_NAME = b.ITEM_UNIQUE_NAME,
        //                   REGIONAL_LANGUAGE = b.REGIONAL_LANGUAGE,
        //                   SALES_PRICE_BEFOR_TAX = b.SALES_PRICE_BEFOR_TAX,
        //                   IS_DELETE = b.IS_DELETE,
        //                   INCLUDE_TAX = b.INCLUDE_TAX,
        //                   IS_Shortable_Item = false,
        //                   IS_Purchased = false,
        //                   IS_Service_Item = false,
        //                   IS_For_Online_Shop = false,
        //                   IS_Not_for_online_shop = false,
        //                   IS_Not_For_Sell = false,
        //                   ALLOW_PURCHASE_ON_ESHOP = false,
        //                   IS_ACTIVE = b.IS_ACIVE,
        //                   IS_Gift_Card = false,
        //                   MODIFICATION_DATE = b.MODIFICATION_DATE,
        //                   WEIGHT_OF_CARDBOARD = b.WEIGHT_OF_CARDBOARD,
        //                   WEIGHT_OF_FOAM = b.WEIGHT_OF_FOAM,
        //                   WEIGHT_OF_PLASTIC = b.WEIGHT_OF_PLASTIC,
        //                   WEIGHT_OF_PAPER = b.WEIGHT_OF_PAPER,
        //                   GODOWN = "",
        //                   DATE = System.DateTime.Now,
        //                   COMPANY_NAME = "Infosolz",
        //                  // PO_NUMBER = c.PO_NUMBER,

        //                   BUSINESS_LOC = "Infosolz",
        //                   BUSS_LOC_ID = b.BUSS_LOC_ID,
        //                   GODOWN_ID = b.GODOWN_ID,
        //                   UNIT_SALES_ID = b.SALE_UNIT_ID,
        //                   UNIT_PURCHAGE_ID = b.PURCHAGE_UNIT_ID,
        //                   //Current_Qty = a.SALE_QTY,

        //                   TAX_COLLECTED_NAME = b.TAX_COLLECTED_NAME,
        //                   TAX_PAID_NAME = b.TAX_PAID_NAME,
        //                   Total = 0,
        //               }).ToList();
        //    return Request.CreateResponse(HttpStatusCode.OK, str);
        //}



        [HttpGet]
        public HttpResponseMessage GetRecItemRefNo()
        {

            string value = Convert.ToString(db.TBL_RECEIVE_ITEM
                            .OrderByDescending(p => p.RECEIVED_ITEM_ENTRY_NO)
                            .Select(r => r.RECEIVED_ITEM_ENTRY_NO)
                            .First());
            var RefNumber = new
            {
                ItemRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }

        [HttpGet]
        public HttpResponseMessage GetEditRecvItem(int id)
        {

            var str = (from a in db.TBL_RECEIVE_ITEM
                       where a.NEWRECEIVE_ID == id
                       select new ReceiveItem
                       {
                           NEWRECEIVE_ID = a.NEWRECEIVE_ID,
                           COMPANY_ID = a.COMPANY_ID,
                           SELECT_BUSINESS_LOCATION_ID = a.SELECT_BUSINESS_LOCATION_ID,
                           RECEIVED_ITEM_ENTRY_NO = a.RECEIVED_ITEM_ENTRY_NO,
                           BARCODE = a.BARCODE,
                           ITEM_NAME = a.ITEM_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           SUPPLIER_INVOICE_NO = a.SUPPLIER_INVOICE_NO,
                           SUPPLIER_INVOICE_DATE = a.SUPPLIER_INVOICE_DATE,
                           RECEIVE_DATE = a.RECEIVE_DATE,
                           RECEIVED_ITEM_ON_DATE = a.RECEIVED_ITEM_ON_DATE,
                           PO_NUMBER = a.PO_NUMBER,
                           SUPPLIER_ID = a.SUPPLIER_ID,
                           PAYMENT_TERMS = a.PAYMENT_TERMS,
                           GODOWN = a.GODOWN,
                           IS_REC_WITH_PO = a.IS_REC_WITH_PO,
                           IS_DELETE = a.IS_DELETE,
                           GODOWN_ID = a.GODOWN_ID,
                           RECEIVING_EMPLOYEE = a.RECEIVING_EMPLOYEE,
                           DISCOUNT_FLAT = a.DISCOUNT_FLAT,
                           DISCOUNT_PERCENT = a.DISCOUNT_PERCENT,
                           ADDTIONAL_CHARGES = a.ADDTIONAL_CHARGES,
                           SUB_TOTAL_BEFORETAX = a.SUB_TOTAL_BEFORETAX,
                           TOTAL_TAX_AMT = a.TOTAL_TAX_AMT,
                           SUB_TOTAL = a.SUB_TOTAL,
                           ROUND_OFF_ADJUSTMENTAMT = a.ROUND_OFF_ADJUSTMENTAMT,
                           TOTAL_AMT = a.TOTAL_AMT,
                           USE_SUPPLIER_ADVANCE_AMT = a.USE_SUPPLIER_ADVANCE_AMT,
                           GRAND_TOTAL = a.GRAND_TOTAL,
                           NOTE = a.NOTE,
                           SUPPLIER = a.SUPPLIER,
                           SELECT_BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           PO_NUMBER_ID = a.PO_NUMBER_ID,
                           FILTER_HIERARCHY = a.FILTER_HIERARCHY,
                           DISCOUNT_BEFOR_TAX = a.DISCOUNT_BEFOR_TAX,
                           PAY_IN_CASH = a.PAY_IN_CASH,
                           APPLY_FPR_EFFECTVE_PRICE = a.APPLY_FPR_EFFECTVE_PRICE,
                           AUTO_SAVE_IN_DRAFT = a.AUTO_SAVE_IN_DRAFT,
                           FOCUS_LAST_ADD_ITEM = a.FOCUS_LAST_ADD_ITEM,
                           ITEM_ENTRY_TEMPLATE = a.ITEM_ENTRY_TEMPLATE,
                           INVOICE_DIS_AMT = a.INVOICE_DIS_AMT,
                       }).ToList();


            return Request.CreateResponse(HttpStatusCode.OK, str);
        }





        [HttpGet]
        public HttpResponseMessage DeleteRecItem(int id)
        {
            var str = (from a in db.TBL_RECEIVE_ITEM where a.NEWRECEIVE_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");


        }


        [HttpPost]
        public HttpResponseMessage CreateRecItem(ReceiveItem _ItemRecModel)
        {

            TBL_RECEIVE_ITEM _Rec = new TBL_RECEIVE_ITEM();
            _Rec.COMPANY_ID = _ItemRecModel.COMPANY_ID;
            _Rec.SELECT_BUSINESS_LOCATION_ID = _ItemRecModel.SELECT_BUSINESS_LOCATION_ID;
            _Rec.BUSINESS_LOCATION = _ItemRecModel.SELECT_BUSINESS_LOCATION;
            _Rec.RECEIVED_ITEM_ENTRY_NO = _ItemRecModel.RECEIVED_ITEM_ENTRY_NO;
            _Rec.BARCODE = _ItemRecModel.BARCODE;
            _Rec.ITEM_NAME = _ItemRecModel.ITEM_NAME;
            _Rec.SEARCH_CODE = _ItemRecModel.SEARCH_CODE;
            _Rec.SUPPLIER_INVOICE_NO = _ItemRecModel.SUPPLIER_INVOICE_NO;
            _Rec.SUPPLIER_INVOICE_DATE = _ItemRecModel.SUPPLIER_INVOICE_DATE;
            _Rec.RECEIVE_DATE = _ItemRecModel.RECEIVE_DATE;
            _Rec.RECEIVED_ITEM_ON_DATE = _ItemRecModel.RECEIVED_ITEM_ON_DATE;
            _Rec.PO_NUMBER_ID = _ItemRecModel.PO_NUMBER_ID;
            _Rec.PO_NUMBER = _ItemRecModel.PO_NUMBER;
            _Rec.SUPPLIER_ID = _ItemRecModel.SUPPLIER_ID;
            _Rec.SUPPLIER = _ItemRecModel.SUPPLIER;
            _Rec.PAYMENT_TERMS = _ItemRecModel.PAYMENT_TERMS;
            _Rec.GODOWN_ID = _ItemRecModel.GODOWN_ID;
            _Rec.GODOWN = _ItemRecModel.GODOWN;
            _Rec.RECEIVING_EMPLOYEE = _ItemRecModel.RECEIVING_EMPLOYEE;
            _Rec.DISCOUNT_FLAT = _ItemRecModel.DISCOUNT_FLAT;
            _Rec.DISCOUNT_PERCENT = _ItemRecModel.DISCOUNT_PERCENT;
            _Rec.ADDTIONAL_CHARGES = _ItemRecModel.ADDTIONAL_CHARGES;
            _Rec.SUB_TOTAL_BEFORETAX = _ItemRecModel.SUB_TOTAL_BEFORETAX;
            _Rec.TOTAL_TAX_AMT = _ItemRecModel.TOTAL_TAX_AMT;
            _Rec.SUB_TOTAL = _ItemRecModel.SUB_TOTAL;
            _Rec.ROUND_OFF_ADJUSTMENTAMT = _ItemRecModel.ROUND_OFF_ADJUSTMENTAMT;
            _Rec.TOTAL_AMT = _ItemRecModel.TOTAL_AMT;
            _Rec.USE_SUPPLIER_ADVANCE_AMT = _ItemRecModel.USE_SUPPLIER_ADVANCE_AMT;
            _Rec.GRAND_TOTAL = _ItemRecModel.GRAND_TOTAL;
            _Rec.IS_REC_WITH_PO = _ItemRecModel.IS_REC_WITH_PO;
            _Rec.NOTE = _ItemRecModel.NOTE;
            _Rec.FILTER_HIERARCHY = _ItemRecModel.FILTER_HIERARCHY;
            _Rec.IS_DELETE = false;
            _Rec.DISCOUNT_BEFOR_TAX = _ItemRecModel.DISCOUNT_BEFOR_TAX;
            _Rec.PAY_IN_CASH = _ItemRecModel.PAY_IN_CASH;
            _Rec.APPLY_FPR_EFFECTVE_PRICE = _ItemRecModel.APPLY_FPR_EFFECTVE_PRICE;
            _Rec.AUTO_SAVE_IN_DRAFT = _ItemRecModel.AUTO_SAVE_IN_DRAFT;
            _Rec.FOCUS_LAST_ADD_ITEM = _ItemRecModel.FOCUS_LAST_ADD_ITEM;
            _Rec.ITEM_ENTRY_TEMPLATE = _ItemRecModel.ITEM_ENTRY_TEMPLATE;
            _Rec.INVOICE_DIS_AMT = _ItemRecModel.INVOICE_DIS_AMT;
            db.TBL_RECEIVE_ITEM.Add(_Rec);
            db.SaveChanges();


            long rcvid = _Rec.NEWRECEIVE_ID;
            if (_ItemRecModel.SelectedItem.Count > 0)
            {
                foreach (var item in _ItemRecModel.SelectedItem)
                {
                    if (item.TOTAL_QTY != null || item.TOTAL_QTY != 0)
                    {
                        TBL_RECEIVE_ITEM_ITEMS RcvItem = new TBL_RECEIVE_ITEM_ITEMS();
                        RcvItem.RCV_ITEM_ID = item.ITEM_ID;
                        RcvItem.RCV_ITEM_ENTRY_ID = Convert.ToInt32(rcvid);
                       // RcvItem.PO_DISCOUNT = item.Discount;
                        RcvItem.RCV_AMOUNT = item.SUB_TOTAL_AFTER_TAX;
                        RcvItem.RCV_QTY = item.TOTAL_QTY;
                        //PoItem.PO_TAX = item.TotalTax;
                        RcvItem.RCV_TAX = item.SUB_TOTAL_AFTER_TAX - item.SUB_TOTAL_BEFORE_TAX;
                        db.TBL_RECEIVE_ITEM_ITEMS.Add(RcvItem);
                        db.SaveChanges();
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        [HttpPost]
        public HttpResponseMessage ItemRecUpdate(ReceiveItem _ItemRecModel)
        {
            var _Rec = (from a in db.TBL_RECEIVE_ITEM where a.NEWRECEIVE_ID == _ItemRecModel.NEWRECEIVE_ID select a).FirstOrDefault();
            _Rec.SELECT_BUSINESS_LOCATION_ID = _ItemRecModel.SELECT_BUSINESS_LOCATION_ID;
            _Rec.RECEIVED_ITEM_ENTRY_NO = _ItemRecModel.RECEIVED_ITEM_ENTRY_NO;
            _Rec.BARCODE = _ItemRecModel.BARCODE;
            _Rec.ITEM_NAME = _ItemRecModel.ITEM_NAME;
            _Rec.SEARCH_CODE = _ItemRecModel.SEARCH_CODE;
            _Rec.SUPPLIER_INVOICE_NO = _ItemRecModel.SUPPLIER_INVOICE_NO;
            _Rec.SUPPLIER_INVOICE_DATE = _ItemRecModel.SUPPLIER_INVOICE_DATE;
            _Rec.RECEIVE_DATE = _ItemRecModel.RECEIVE_DATE;
            _Rec.RECEIVED_ITEM_ON_DATE = _ItemRecModel.RECEIVED_ITEM_ON_DATE;
            _Rec.PO_NUMBER = _ItemRecModel.PO_NUMBER;
            _Rec.SUPPLIER_ID = _ItemRecModel.SUPPLIER_ID;
            _Rec.PAYMENT_TERMS = _ItemRecModel.PAYMENT_TERMS;
            _Rec.GODOWN_ID = _ItemRecModel.GODOWN_ID;
            _Rec.RECEIVING_EMPLOYEE = _ItemRecModel.RECEIVING_EMPLOYEE;
            _Rec.DISCOUNT_FLAT = _ItemRecModel.DISCOUNT_FLAT;
            _Rec.DISCOUNT_PERCENT = _ItemRecModel.DISCOUNT_PERCENT;
            _Rec.ADDTIONAL_CHARGES = _ItemRecModel.ADDTIONAL_CHARGES;
            _Rec.SUB_TOTAL_BEFORETAX = _ItemRecModel.SUB_TOTAL_BEFORETAX;
            _Rec.TOTAL_TAX_AMT = _ItemRecModel.TOTAL_TAX_AMT;
            _Rec.SUB_TOTAL = _ItemRecModel.SUB_TOTAL;
            _Rec.ROUND_OFF_ADJUSTMENTAMT = _ItemRecModel.ROUND_OFF_ADJUSTMENTAMT;
            _Rec.TOTAL_AMT = _ItemRecModel.TOTAL_AMT;
            _Rec.USE_SUPPLIER_ADVANCE_AMT = _ItemRecModel.USE_SUPPLIER_ADVANCE_AMT;
            _Rec.GRAND_TOTAL = _ItemRecModel.GRAND_TOTAL;
            _Rec.NOTE = _ItemRecModel.NOTE;
            _Rec.PO_NUMBER_ID = _ItemRecModel.PO_NUMBER_ID;
            _Rec.SUPPLIER = _ItemRecModel.SUPPLIER;
            _Rec.BUSINESS_LOCATION = _ItemRecModel.SELECT_BUSINESS_LOCATION;
            _Rec.FILTER_HIERARCHY = _ItemRecModel.FILTER_HIERARCHY;
            _Rec.DISCOUNT_BEFOR_TAX = _ItemRecModel.DISCOUNT_BEFOR_TAX;
            _Rec.PAY_IN_CASH = _ItemRecModel.PAY_IN_CASH;
            _Rec.APPLY_FPR_EFFECTVE_PRICE = _ItemRecModel.APPLY_FPR_EFFECTVE_PRICE;
            _Rec.AUTO_SAVE_IN_DRAFT = _ItemRecModel.AUTO_SAVE_IN_DRAFT;
            _Rec.FOCUS_LAST_ADD_ITEM = _ItemRecModel.FOCUS_LAST_ADD_ITEM;
            _Rec.ITEM_ENTRY_TEMPLATE = _ItemRecModel.ITEM_ENTRY_TEMPLATE;
            _Rec.INVOICE_DIS_AMT = _ItemRecModel.INVOICE_DIS_AMT;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");

        }
    }
}
