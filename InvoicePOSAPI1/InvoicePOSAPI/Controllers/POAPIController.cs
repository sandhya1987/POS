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
    public class POAPIController : ApiController
    {

        POModel pom = new POModel();
        //POModel pom1 = new POModel();
        NEW_POSEntities db = new NEW_POSEntities();

        //[HttpPost]
        //public HttpResponseMessage UpdatePOItem(ObservableCollection<POModel> SelectedItem)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    { }
        //    return Request.CreateResponse(HttpStatusCode.OK, "success");
        //}
        [HttpGet]
        public HttpResponseMessage PurchaseOrdList(int id)
        {
            var str = (from a in db.TBL_PO_PAYMENT
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new POModel
                       {
                           //BAR_CODE = a.BAR_CODE,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION_ID,
                           COMPANY_ID = a.COMPANY_ID,
                           DELIVER = a.DELIVER_ID,
                           DELIVER_TO = a.DELIVER_TO,
                           DELIVERY_DATE = a.DELIVERY_DATE,
                           IS_SEND_MAIL = a.IS_SEND_MAIL,
                           //ITEM_NAME = a.ITEM_NAME,
                           PO_ID = a.PO_ID,
                           PO_NUMBER1 = a.PO_NUMBER,
                           //SEARCH_CODE = a.SEARCH_CODE,
                           SUPPLIER = a.SUPPLIER_ID,
                           SUPPLIER_EMAIL = a.SUPPLIER_MAIL,
                           TERMS = a.TERMS,
                           TOTAL_AMOUNT = a.TOTAL_AMOUNT,
                           TOTAL_TAX = a.TOTAL_TAX,
                           PO_DATE = a.PO_DATE,

                           PO_STATUS = a.PO_STATUS
                           //PO_TYPE=a.PO_TYPE,



                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage SearchPO(string name)
        {
            var str = (from a in db.TBL_PO
                       where a.PO_NUMBER.Contains("" + name + "") && a.IS_DELETE == false
                       select new POModel
                       {
                           BAR_CODE = a.BAR_CODE,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION_ID,
                           COMPANY_ID = a.COMPANY_ID,
                           DELIVER = a.DELIVER_ID,
                           DELIVER_TO = a.DELIVER_TO,
                           DELIVERY_DATE = a.DELIVERY_DATE,
                           IS_SEND_MAIL = a.IS_SEND_MAIL,
                           ITEM_NAME = a.ITEM_NAME,
                           PO_ID = a.PO_ID,
                           PO_NUMBER1 = a.PO_NUMBER,
                           SEARCH_CODE = a.SEARCH_CODE,
                           SUPPLIER = a.SUPPLIER_ID,
                           SUPPLIER_EMAIL = a.SUPPLIER_EMAIL,
                           TERMS = a.TERMS,
                           TOTAL_AMOUNT = a.TOTAL_AMOUNT,
                           TOTAL_TAX = a.TOTAL_TAX,
                           PO_DATE = a.PO_DATE,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage PurchaseOrdEdit(int id)
        {
            var str = (from a in db.TBL_PO_PAYMENT
                       where a.PO_ID == id && a.IS_DELETE == false
                       select new POModel
                       {
                           //BAR_CODE = a.BAR_CODE,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           COMPANY_ID = a.COMPANY_ID,
                           DELIVER = a.DELIVER_ID,
                           DELIVER_TO = a.DELIVER_TO,
                           DELIVERY_DATE = a.DELIVERY_DATE,
                           IS_SEND_MAIL = a.IS_SEND_MAIL,
                           //ITEM_NAME = a.ITEM_NAME,
                           PO_ID = a.PO_ID,
                           PO_NUMBER1 = a.PO_NUMBER,
                           //SEARCH_CODE = a.SEARCH_CODE,
                           SUPPLIER = a.SUPPLIER_NAME,
                           SUPPLIER_EMAIL = a.SUPPLIER_MAIL,
                           TERMS = a.TERMS,
                           TOTAL_AMOUNT = a.TOTAL_AMOUNT,
                           TOTAL_TAX = a.TOTAL_TAX,
                           PO_DATE = a.PO_DATE,
                           PO_STATUS = a.PO_STATUS,
                           PO_TYPE = a.PO_TYPE,
                           //SearchStock=a.SEARCH_STOCK,                           
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage GetPoItem(int id)
        {
            var str = (from a in db.TBL_PO_ITEMS
                       join b in db.TBL_ITEMS on a.PO_ITEM_ID equals b.ITEM_ID
                       where a.PO_NUMBER == id
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
                           CATAGORY_ID = b.CATAGORY_ID,
                           SEARCH_CODE = b.SERCH_CODE,
                           TAX_PAID = b.TAX_PAID,
                           TAX_COLLECTED = a.PO_TAX,

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

        [HttpGet]
        public HttpResponseMessage DeletePO(int id)
        {
            var str = (from a in db.TBL_PO_PAYMENT where a.PO_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            //var str1 = (from a in db.TBL_PO_ITEMS where a.PO_NUMBER == id select a).FirstOrDefault();
            //str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");


        }
        [HttpPost]
        public HttpResponseMessage PurchaseOrdAdd(POModel pom)
        {
            try
            {
                //TBL_PO po = new TBL_PO();
                //po.BAR_CODE = pom.BAR_CODE;
                //po.BUSINESS_LOCATION_ID = pom.BUSINESS_LOCATION;
                //po.COMPANY_ID = pom.COMPANY_ID;
                //po.DELIVER_ID = pom.DELIVER;
                //po.DELIVER_TO = pom.DELIVER_TO;
                //// po.DELIVERY_DATE = pom.DELIVERY_DATE;
                //po.DELIVERY_DATE = DateTime.Now;
                //po.IS_SEND_MAIL = pom.IS_SEND_MAIL;
                //po.ITEM_NAME = pom.ITEM_NAME;
                //po.PO_NUMBER = pom.PO_NUMBER;
                //po.SEARCH_CODE = pom.SEARCH_CODE;
                //po.SUPPLIER_EMAIL = pom.SUPPLIER_EMAIL;
                //po.SUPPLIER_ID = pom.SUPPLIER;
                //po.TERMS = pom.TERMS;
                //po.TOTAL_AMOUNT = pom.TOTAL_AMOUNT;
                //po.TOTAL_TAX = pom.TOTAL_TAX;
                //po.IS_DELETE = false;
                //po.PO_DATE = DateTime.Now;
                //po.SEARCH_STOCK = pom.SearchStock;
                //db.TBL_PO.Add(po);
                //db.SaveChanges();

                TBL_PO_PAYMENT po = new TBL_PO_PAYMENT();
                //po.BAR_CODE = pom.BAR_CODE;
                po.BUSINESS_LOCATION = pom.BUSINESS_LOCATION;
                po.COMPANY_ID = pom.COMPANY_ID;
                po.DELIVER_ID = pom.DELIVER;
                po.DELIVER_TO = pom.DELIVER_TO;
                // po.DELIVERY_DATE = pom.DELIVERY_DATE;
                po.DELIVERY_DATE = DateTime.Now;
                po.IS_SEND_MAIL = pom.IS_SEND_MAIL;
                //po.ITEM_NAME = pom.ITEM_NAME;
                po.PO_NUMBER = pom.PO_NUMBER1;
                //po.SEARCH_CODE = pom.SEARCH_CODE;
                po.SUPPLIER_MAIL = pom.SUPPLIER_EMAIL;
                po.SUPPLIER_NAME = pom.SUPPLIER;
                po.TERMS = pom.TERMS;
                po.TOTAL_AMOUNT = pom.TOTAL_AMOUNT;
                po.TOTAL_TAX = pom.TOTAL_TAX;
                po.PO_STATUS = pom.PO_STATUS;
                po.IS_DELETE = false;
                po.PO_DATE = DateTime.Now;
                //po.SEARCH_STOCK = pom.SearchStock;
                db.TBL_PO_PAYMENT.Add(po);
                db.SaveChanges();


                long poid = po.PO_ID;
                if (pom.SelectedItem.Count > 0)
                {
                    foreach (var item in pom.SelectedItem)
                    {
                        if (item.TOTAL_QTY != null || item.TOTAL_QTY != 0)
                        {
                            TBL_PO_ITEMS PoItem = new TBL_PO_ITEMS();
                            PoItem.PO_ITEM_ID = item.ITEM_ID;
                            PoItem.PO_NUMBER = Convert.ToInt32(poid);
                            PoItem.PO_DISCOUNT = item.Discount;
                            PoItem.PO_TOTAL_AMOUNT = item.SUB_TOTAL_AFTER_TAX;
                            PoItem.PO_QTY = item.TOTAL_QTY;
                            //PoItem.PO_TAX = item.TotalTax;
                            PoItem.PO_TAX = item.SUB_TOTAL_AFTER_TAX - item.SUB_TOTAL_BEFORE_TAX;
                            db.TBL_PO_ITEMS.Add(PoItem);
                            db.SaveChanges();
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        [HttpPost]
        public HttpResponseMessage UpdatePO(POModel pom)
        {
            try
            {


                var str1 = (from a in db.TBL_PO_PAYMENT where a.PO_ID == pom.PO_ID select a).FirstOrDefault();
                //str.BAR_CODE = pom.BAR_CODE;
                str1.BUSINESS_LOCATION_ID = pom.BUSINESS_LOCATION;
                str1.COMPANY_ID = pom.COMPANY_ID;
                str1.DELIVER_ID = pom.DELIVER;
                str1.DELIVER_TO = pom.DELIVER_TO;
                str1.DELIVERY_DATE = pom.DELIVERY_DATE;
                str1.IS_SEND_MAIL = pom.IS_SEND_MAIL;
                //str.ITEM_NAME = pom.ITEM_NAME;
                str1.PO_NUMBER = pom.PO_NUMBER1;
                //str.SEARCH_CODE = pom.SEARCH_CODE;
                //str.SUPPLIER_EMAIL = pom.SUPPLIER_EMAIL;
                str1.SUPPLIER_ID = pom.SUPPLIER;
                str1.TERMS = pom.TERMS;
                str1.TOTAL_AMOUNT = pom.TOTAL_AMOUNT;
                str1.TOTAL_TAX = pom.TOTAL_TAX;
                str1.PO_DATE = DateTime.Now;
                str1.IS_DELETE = false;
                //str.SEARCH_STOCK = pom.SearchStock;
                db.SaveChanges();




                long poid = pom.PO_ID;
                if (pom.SelectedItem.Count > 0)
                {
                    foreach (var item in pom.SelectedItem)
                    {
                        if (item.TOTAL_QTY != 0)
                        {
                            //if (pom.PO_NUMBER1 != null)
                            //{
                            //    
                            //var str = (from a in db.TBL_PO_ITEMS where a.PO_NUMBER == pom.PO_ID && a.PO_ITEM_ID==item.ITEM_ID && a.PO_QTY !=item.TOTAL_QTY select a).FirstOrDefault();
                            var str = (from a in db.TBL_PO_ITEMS where a.PO_NUMBER == pom.PO_ID && a.PO_ITEM_ID == item.ITEM_ID select a).FirstOrDefault();

                            if (str != null)
                            {
                                str.PO_ITEM_ID = item.ITEM_ID;
                                str.PO_NUMBER = Convert.ToInt32(poid);
                                str.PO_DISCOUNT = item.Discount;
                                str.PO_TOTAL_AMOUNT = item.SUB_TOTAL_AFTER_TAX;
                                str.PO_QTY = item.TOTAL_QTY;
                                //PoItem.PO_TAX = item.TotalTax;
                                str.PO_TAX = item.SUB_TOTAL_AFTER_TAX - item.SUB_TOTAL_BEFORE_TAX;
                                //db.TBL_PO_ITEMS.Add(PoItem);
                                db.SaveChanges();
                            }

                            else
                            {
                                //foreach (var item in pom.SelectedItem)
                                //{
                                //    if (item.TOTAL_QTY != 0)
                                //    {
                                TBL_PO_ITEMS PoItem = new TBL_PO_ITEMS();
                                PoItem.PO_ITEM_ID = item.ITEM_ID;
                                PoItem.PO_NUMBER = Convert.ToInt32(poid);
                                PoItem.PO_DISCOUNT = item.Discount;
                                PoItem.PO_TOTAL_AMOUNT = item.SUB_TOTAL_AFTER_TAX;
                                PoItem.PO_QTY = item.TOTAL_QTY;
                                //PoItem.PO_TAX = item.TotalTax;
                                PoItem.PO_TAX = item.SUB_TOTAL_AFTER_TAX - item.SUB_TOTAL_BEFORE_TAX;
                                db.TBL_PO_ITEMS.Add(PoItem);
                                db.SaveChanges();
                                //    }

                                //}
                            }

                        }
                    }


                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Updated success..");
        }
        [HttpGet]
        public HttpResponseMessage GetItemByLocation(BusinessLocationModel LOCATION_MODEL)
        {
            var str = (from b in db.TBL_PO
                       join a in db.TBL_ITEMS on b.ITEM_ID equals a.ITEM_ID
                       join c in db.TBL_ITEMS_ATTRIBUTE on a.ITEM_ID equals c.ITEM_ID
                       where b.COMPANY_ID == LOCATION_MODEL.COMPANY_ID && b.BUSINESS_LOCATION_ID == "1"
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
                           INCLUDE_TAX = c.INCLUDE_TAX,
                           IS_Shortable_Item = c.IS_SORTABLE_ITEM,
                           IS_Purchased = c.IS_PURCHASED,
                           IS_Service_Item = c.IS_SERVICE,
                           IS_For_Online_Shop = c.ONLY_ONLINE_SHOP,
                           IS_Not_for_online_shop = c.NOT_FOR_ONLINE_SHOP,
                           IS_Not_For_Sell = c.NOT_FOR_SALE,
                           ALLOW_PURCHASE_ON_ESHOP = c.ALLOW_PURCHASE_ON_ESHOP,
                           IS_ACTIVE = c.IS_ACTIVE,
                           IS_Gift_Card = c.IS_GIFT_CARD,
                           MODIFICATION_DATE = a.MODIFICATION_DATE,


                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }


        [HttpPost]
        public HttpResponseMessage GetAllItemByLocation(BusinessLocationModel SelectedBusinessLoca)
        {
            DateTime ToDate = Convert.ToDateTime(SelectedBusinessLoca.ToDt);
            DateTime FromDate = Convert.ToDateTime(SelectedBusinessLoca.FromDt);
            var str = (from b in db.TBL_PO
                       join a in db.TBL_ITEMS on b.ITEM_ID equals a.ITEM_ID
                       join c in db.TBL_ITEMS_ATTRIBUTE on a.ITEM_ID equals c.ITEM_ID
                       where b.COMPANY_ID == SelectedBusinessLoca.COMPANY_ID && b.BUSSINESS_ID == 1
                      && (b.PO_DATE >= FromDate && b.PO_DATE <= ToDate)
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
                           INCLUDE_TAX = c.INCLUDE_TAX,
                           IS_Shortable_Item = c.IS_SORTABLE_ITEM,
                           IS_Purchased = c.IS_PURCHASED,
                           IS_Service_Item = c.IS_SERVICE,
                           IS_For_Online_Shop = c.ONLY_ONLINE_SHOP,
                           IS_Not_for_online_shop = c.NOT_FOR_ONLINE_SHOP,
                           IS_Not_For_Sell = c.NOT_FOR_SALE,
                           ALLOW_PURCHASE_ON_ESHOP = c.ALLOW_PURCHASE_ON_ESHOP,
                           IS_ACTIVE = c.IS_ACTIVE,
                           IS_Gift_Card = c.IS_GIFT_CARD,
                           MODIFICATION_DATE = a.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = a.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = a.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = a.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = a.WEIGHT_OF_PAPER,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetPoNo()
        {

            string value = Convert.ToString(db.TBL_PO_PAYMENT
                            .OrderByDescending(p => p.PO_NUMBER)
                            .Select(r => r.PO_NUMBER)
                            .First());
            var RefNumber = new
            {
                SuppRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }

    }
}
