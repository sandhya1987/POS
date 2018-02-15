using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoicePOSAPI.Models;
using InvoicePOSDATA;

namespace InvoicePOSAPI.Controllers
{
    public class StocktransferAPIController : ApiController
    {


        NEW_POSEntities db = new NEW_POSEntities();
        StocktransferModel cm = new StocktransferModel();
        [HttpGet]
        public HttpResponseMessage GetStocktransfer(string Code)
        {
            var str = (from a in db.TBL_STOCK_TRANSFER
                       where a.BARCODE == Code || a.IS_DELETE == false
                       // where a.SERCH_CODE == Code || a.BARCODE == Code || a.ITEM_NAME == Code
                       select new StocktransferModel
                       {
                           STOCK_TRANSFER_ID = a.STOCK_TRANSFER_ID,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           BUSINESS_LOCATION_ID = a.BUSINESS_LOCATION_ID,
                           FROM_GODOWN_ID = a.FROM_GODOWN_ID,
                           RECEIVED_BY_ID = a.RECEIVED_BY_ID,
                           TO_GODOWN_ID = a.TO_GODOWN_ID,
                           TOTAL_STOCK_QNT = a.TOTAL_STOCK_QNT,
                           DATE = a.DATE,
                           EMAIL = a.EMAIL,
                           FROM_GODOWN = a.FROM_GODOWN,
                           IS_SEND = a.IS_SEND,
                           COMPANY_ID = a.COMPANY_ID,
                           ITEM_ID = a.ITEM_ID,
                           RECEIVED_BY = a.RECEIVED_BY,
                           STOCK_TRANSFER_NUMBER = a.STOCK_TRANSFER_NUMBER,
                           TO_GODOWN = a.TO_GODOW,
                           BARCODE = a.BARCODE,
                           ITEM_NAME = a.ITEM_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           TRANS_QUANTITY = a.TRANS_QUANTITY,
                           IS_NEGATIVE_STOCK_MESSAGE = a.IS_NEGATIVE_STOCK_MESSAGE,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetStocktDetails(int companyid, int godownid, int itemid)
        {
            var str = (from a in db.TBL_STOCK_TRANSFER
                       join b in db.TBL_ITEMS on a.ITEM_ID equals b.ITEM_ID
                       where a.COMPANY_ID == companyid && a.FROM_GODOWN_ID == godownid || a.TO_GODOWN_ID == godownid && a.ITEM_ID == itemid && a.IS_DELETE == false
                     
                       // where a.SERCH_CODE == Code || a.BARCODE == Code || a.ITEM_NAME == Code
                       select new StocktransferModel
                       {

                           STOCK_TRANSFER_ID = a.STOCK_TRANSFER_ID,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           BUSINESS_LOCATION_ID = a.BUSINESS_LOCATION_ID,
                           FROM_GODOWN_ID = a.FROM_GODOWN_ID,
                           RECEIVED_BY_ID = a.RECEIVED_BY_ID,
                           TO_GODOWN_ID = a.TO_GODOWN_ID,
                           TOTAL_STOCK_QNT = a.TOTAL_STOCK_QNT,
                           DATE = a.DATE,
                           EMAIL = a.EMAIL,
                           FROM_GODOWN = a.FROM_GODOWN,
                           IS_SEND = a.IS_SEND,
                           COMPANY_ID = a.COMPANY_ID,
                           ITEM_ID = a.ITEM_ID,
                           RECEIVED_BY = a.RECEIVED_BY,
                           STOCK_TRANSFER_NUMBER = a.STOCK_TRANSFER_NUMBER,
                           TO_GODOWN = a.TO_GODOW,
                           BARCODE = a.BARCODE,
                           ITEM_NAME = a.ITEM_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           TRANS_QUANTITY = a.TRANS_QUANTITY,
                           IS_NEGATIVE_STOCK_MESSAGE = a.IS_NEGATIVE_STOCK_MESSAGE,
                           OPN_QNT = b.OPN_QNT,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetStocktDetailsbydate(int companyid, int godownid, int itemid, DateTime from1, DateTime to)
        {
            var str = (from a in db.TBL_STOCK_TRANSFER
                       join b in db.TBL_ITEMS on a.ITEM_ID equals b.ITEM_ID
                       where a.COMPANY_ID == companyid && a.FROM_GODOWN_ID == godownid || a.TO_GODOWN_ID == godownid && a.ITEM_ID == itemid && a.IS_DELETE == false
                       where a.DATE >= from1
                       where a.DATE <= to
                       // where a.SERCH_CODE == Code || a.BARCODE == Code || a.ITEM_NAME == Code
                       select new StocktransferModel
                       {

                           STOCK_TRANSFER_ID = a.STOCK_TRANSFER_ID,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           BUSINESS_LOCATION_ID = a.BUSINESS_LOCATION_ID,
                           FROM_GODOWN_ID = a.FROM_GODOWN_ID,
                           RECEIVED_BY_ID = a.RECEIVED_BY_ID,
                           TO_GODOWN_ID = a.TO_GODOWN_ID,
                           TOTAL_STOCK_QNT = a.TOTAL_STOCK_QNT,
                           DATE = a.DATE,
                           EMAIL = a.EMAIL,
                           FROM_GODOWN = a.FROM_GODOWN,
                           IS_SEND = a.IS_SEND,
                           COMPANY_ID = a.COMPANY_ID,
                           ITEM_ID = a.ITEM_ID,
                           RECEIVED_BY = a.RECEIVED_BY,
                           STOCK_TRANSFER_NUMBER = a.STOCK_TRANSFER_NUMBER,
                           TO_GODOWN = a.TO_GODOW,
                           BARCODE = a.BARCODE,
                           ITEM_NAME = a.ITEM_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           TRANS_QUANTITY = a.TRANS_QUANTITY,
                           IS_NEGATIVE_STOCK_MESSAGE = a.IS_NEGATIVE_STOCK_MESSAGE,
                           OPN_QNT = b.OPN_QNT,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage EditStocktransfer(long id)
        {
            var str = (from a in db.TBL_STOCK_TRANSFER
                       where a.STOCK_TRANSFER_ID == id
                       // where a.SERCH_CODE == Code || a.BARCODE == Code || a.ITEM_NAME == Code
                       select new StocktransferModel
                       {
                           STOCK_TRANSFER_ID = a.STOCK_TRANSFER_ID,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           BUSINESS_LOCATION_ID = a.BUSINESS_LOCATION_ID,
                           FROM_GODOWN_ID = a.FROM_GODOWN_ID,
                           RECEIVED_BY_ID = a.RECEIVED_BY_ID,
                           TO_GODOWN_ID = a.TO_GODOWN_ID,
                           TOTAL_STOCK_QNT = a.TOTAL_STOCK_QNT,
                           DATE = a.DATE,
                           EMAIL = a.EMAIL,
                           FROM_GODOWN = a.FROM_GODOWN,
                           IS_SEND = a.IS_SEND,
                           COMPANY_ID = a.COMPANY_ID,
                           ITEM_ID = a.ITEM_ID,
                           RECEIVED_BY = a.RECEIVED_BY,
                           STOCK_TRANSFER_NUMBER = a.STOCK_TRANSFER_NUMBER,
                           TO_GODOWN = a.TO_GODOW,
                           BARCODE = a.BARCODE,
                           ITEM_NAME = a.ITEM_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           TRANS_QUANTITY = a.TRANS_QUANTITY,
                           IS_NEGATIVE_STOCK_MESSAGE = a.IS_NEGATIVE_STOCK_MESSAGE,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage DeleteStocktransfer(int id)
        {
            var str = (from a in db.TBL_STOCK_TRANSFER where a.STOCK_TRANSFER_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpGet]
        public HttpResponseMessage ChngStocktransfer(StocktransferModel _StocktransferModel)
        {
            var str = (from a in db.TBL_STOCK_TRANSFER where a.STOCK_TRANSFER_ID == _StocktransferModel.STOCK_TRANSFER_ID select a).FirstOrDefault();
            str.TOTAL_STOCK_QNT = _StocktransferModel.CHNG_QNT;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpPost]
        public HttpResponseMessage CreateStocktransfer(StocktransferModel _StocktransferModel)
        {
            try
            {


                //TBL_STOCK_TRANSFER _stock = new TBL_STOCK_TRANSFER();
                //_stock.COMPANY_ID = _StocktransferModel.COMPANY_ID;
                //_stock.BUSINESS_LOCATION = _StocktransferModel.BUSINESS_LOCATION;
                //_stock.BUSINESS_LOCATION_ID = _StocktransferModel.BUSINESS_LOCATION_ID;
                //_stock.DATE = DateTime.Now;
                //_stock.BARCODE = _StocktransferModel.BARCODE;
                //_stock.SEARCH_CODE = _StocktransferModel.SEARCH_CODE;
                //_stock.ITEM_NAME = _StocktransferModel.ITEM_NAME;
                //_stock.EMAIL = _StocktransferModel.EMAIL;
                //_stock.FROM_GODOWN = _StocktransferModel.FROM_GODOWN;
                //_stock.FROM_GODOWN_ID = _StocktransferModel.FROM_GODOWN_ID;
                //_stock.IS_SEND = _StocktransferModel.IS_SEND;
                //_stock.RECEIVED_BY_ID = _StocktransferModel.RECEIVED_BY_ID;
                //_stock.RECEIVED_BY = _StocktransferModel.RECEIVED_BY;
                //_stock.STOCK_TRANSFER_ID = _StocktransferModel.STOCK_TRANSFER_ID;
                //_stock.STOCK_TRANSFER_NUMBER = _StocktransferModel.STOCK_TRANSFER_NUMBER;
                //_stock.TO_GODOWN_ID = _StocktransferModel.TO_GODOWN_ID;
                //_stock.TO_GODOW = _StocktransferModel.TO_GODOWN;
                //_stock.TOTAL_STOCK_QNT = _StocktransferModel.TOTAL_STOCK_QNT;
                //_stock.IS_DELETE = false;
                //_stock.IS_NEGATIVE_STOCK_MESSAGE = _StocktransferModel.IS_NEGATIVE_STOCK_MESSAGE;
                //db.TBL_STOCK_TRANSFER.Add(_stock);
                //db.SaveChanges();
                if (_StocktransferModel.getAllStockTransfer.Count > 0)
                {
                    foreach (var item in _StocktransferModel.getAllStockTransfer)
                    {
                        if (item.TRANS_QUANTITY != 0)
                        {
                            TBL_STOCK_TRANSFER _stock1 = new TBL_STOCK_TRANSFER();
                            _stock1.COMPANY_ID = _StocktransferModel.COMPANY_ID;
                            _stock1.BUSINESS_LOCATION = _StocktransferModel.BUSINESS_LOCATION;
                            _stock1.BUSINESS_LOCATION_ID = _StocktransferModel.BUSINESS_LOCATION_ID;
                            _stock1.DATE = DateTime.Now;
                            _stock1.BARCODE = item.BARCODE;
                            _stock1.SEARCH_CODE = item.SEARCH_CODE;
                            _stock1.ITEM_NAME = item.ITEM_NAME;
                            _stock1.ITEM_ID = Convert.ToInt32(item.ITEM_ID);
                            _stock1.EMAIL = _StocktransferModel.EMAIL;
                            _stock1.FROM_GODOWN = _StocktransferModel.FROM_GODOWN;
                            _stock1.FROM_GODOWN_ID = _StocktransferModel.FROM_GODOWN_ID;
                            _stock1.IS_SEND = _StocktransferModel.IS_SEND;
                            _stock1.RECEIVED_BY_ID = _StocktransferModel.RECEIVED_BY_ID;
                            _stock1.RECEIVED_BY = _StocktransferModel.RECEIVED_BY;
                            _stock1.STOCK_TRANSFER_ID = _StocktransferModel.STOCK_TRANSFER_ID;
                            _stock1.STOCK_TRANSFER_NUMBER = _StocktransferModel.STOCK_TRANSFER_NUMBER;
                            _stock1.TO_GODOWN_ID = _StocktransferModel.TO_GODOWN_ID;
                            _stock1.TO_GODOW = _StocktransferModel.TO_GODOWN;
                            _stock1.TOTAL_STOCK_QNT = item.TOTAL_STOCK_QNT;

                            _stock1.TRANS_QUANTITY = Convert.ToInt32(item.TRANS_QUANTITY);
                            _stock1.IS_DELETE = false;
                            _stock1.IS_NEGATIVE_STOCK_MESSAGE = _StocktransferModel.IS_NEGATIVE_STOCK_MESSAGE;
                            db.TBL_STOCK_TRANSFER.Add(_stock1);
                            db.SaveChanges();
                            if (item.TRANS_QUANTITY != 0)
                            {
                                //TBL_ITEMS _item = new TBL_ITEMS();
                                var str = (from a in db.TBL_ITEMS where a.COMPANY_ID == _StocktransferModel.COMPANY_ID && a.BARCODE == item.BARCODE && a.GODOWN_ID == _StocktransferModel.FROM_GODOWN_ID select a).FirstOrDefault();
                                //_item.COMPANY_ID = Convert.ToInt32(_StocktransferModel.COMPANY_ID);
                                //_item.BARCODE = _StocktransferModel.BARCODE;
                                //int total = Convert.ToInt32(_StocktransferModel.TOTAL_STOCK_QNT - _StocktransferModel.TRANS_QUANTITY);
                                //_item.OPN_QNT = total;
                                //_item.GODOWN_ID = Convert.ToInt32(_StocktransferModel.TO_GODOWN_ID);
                                if (str != null)
                                {
                                    str.COMPANY_ID = Convert.ToInt32(_StocktransferModel.COMPANY_ID);
                                    str.BARCODE = item.BARCODE;


                                    str.GODOWN_ID = Convert.ToInt32(_StocktransferModel.TO_GODOWN_ID);
                                    int total = Convert.ToInt32(item.TOTAL_STOCK_QNT - item.TRANS_QUANTITY);
                                    str.OPN_QNT = total;
                                    str.ITEM_NAME = _StocktransferModel.ITEM_NAME;

                                    str.ITEM_DESCRIPTION = str.ITEM_DESCRIPTION;
                                    str.ITEM_PRICE = str.ITEM_PRICE;
                                    str.ITEM_INVOICE_ID = str.ITEM_INVOICE_ID;
                                    str.ITEM_PRODUCT_ID = str.ITEM_PRODUCT_ID;
                                    str.KEYWORD = str.KEYWORD;
                                    str.ACCESSORIES_KEYWORD = str.ACCESSORIES_KEYWORD;
                                    //str.BARCODE = str.BARCODE;
                                    str.CATAGORY_ID = str.CATAGORY_ID;
                                    str.CATEGORY_NAME = str.CATEGORY_NAME;
                                    str.SERCH_CODE = str.SERCH_CODE;
                                    str.TAX_PAID = str.TAX_PAID;
                                    str.TAX_COLLECTED = str.TAX_COLLECTED;
                                    str.PURCHASE_UNIT = str.PURCHASE_UNIT;
                                    str.SALES_UNIT = str.SALES_UNIT;
                                    str.PURCHASE_UNIT_PRICE = str.PURCHASE_UNIT_PRICE;
                                    str.SALES_PRICE = str.SALES_PRICE;
                                    str.MRP = str.MRP;
                                    str.DISPLAY_INDEX = str.DISPLAY_INDEX;
                                    str.ITEM_GROUP_NAME = str.ITEM_GROUP_NAME;
                                    str.ITEM_UNIQUE_NAME = str.ITEM_UNIQUE_NAME;
                                    str.REGIONAL_LANGUAGE = str.REGIONAL_LANGUAGE;
                                    str.SALES_PRICE_BEFOR_TAX = str.SALES_PRICE_BEFOR_TAX;
                                    str.WEIGHT_OF_CARDBOARD = str.WEIGHT_OF_CARDBOARD;
                                    str.WEIGHT_OF_FOAM = str.WEIGHT_OF_FOAM;
                                    str.WEIGHT_OF_PAPER = str.WEIGHT_OF_PAPER;
                                    str.WEIGHT_OF_PLASTIC = str.WEIGHT_OF_PLASTIC;
                                    str.IMAGE_PATH = str.IMAGE_PATH;
                                    str.TAX_COLLECTED_NAME = str.TAX_COLLECTED_NAME;
                                    str.TAX_PAID_NAME = str.TAX_PAID_NAME;
                                    str.MODIFICATION_DATE = str.MODIFICATION_DATE;



                                    str.BUSS_LOC_ID = str.BUSS_LOC_ID;
                                    //str.GODOWN_ID = str.GODOWN_ID;
                                    str.TAX_COLLECTED_ID = str.TAX_COLLECTED_ID;
                                    str.TAX_PAID_ID = str.TAX_PAID_ID;
                                    str.PURCHAGE_UNIT_ID = str.PURCHAGE_UNIT_ID;
                                    str.SALE_UNIT_ID = str.SALE_UNIT_ID;
                                    str.IS_ACIVE = str.IS_ACIVE;

                                    db.SaveChanges();
                                }





                            }
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpPost]
        public HttpResponseMessage UpdateStocktransfer(StocktransferModel _StocktransferModel)
        {
            try
            {
                var _stock = (from a in db.TBL_STOCK_TRANSFER where a.STOCK_TRANSFER_ID == _StocktransferModel.STOCK_TRANSFER_ID select a).FirstOrDefault();
                _stock.COMPANY_ID = _StocktransferModel.COMPANY_ID;
                _stock.BUSINESS_LOCATION = _StocktransferModel.BUSINESS_LOCATION;
                _stock.BUSINESS_LOCATION_ID = _StocktransferModel.BUSINESS_LOCATION_ID;
                _stock.DATE = DateTime.Now;
                _stock.BARCODE = _StocktransferModel.BARCODE;
                _stock.SEARCH_CODE = _StocktransferModel.SEARCH_CODE;
                _stock.ITEM_NAME = _StocktransferModel.ITEM_NAME;
                _stock.EMAIL = _StocktransferModel.EMAIL;
                _stock.FROM_GODOWN = _StocktransferModel.FROM_GODOWN;
                _stock.FROM_GODOWN_ID = _StocktransferModel.FROM_GODOWN_ID;
                _stock.IS_SEND = _StocktransferModel.IS_SEND;
                _stock.RECEIVED_BY_ID = _StocktransferModel.RECEIVED_BY_ID;
                _stock.RECEIVED_BY = _StocktransferModel.RECEIVED_BY;
                _stock.STOCK_TRANSFER_ID = _StocktransferModel.STOCK_TRANSFER_ID;
                _stock.STOCK_TRANSFER_NUMBER = _StocktransferModel.STOCK_TRANSFER_NUMBER;
                _stock.TO_GODOWN_ID = _StocktransferModel.TO_GODOWN_ID;
                _stock.TO_GODOW = _StocktransferModel.TO_GODOWN;
                _stock.TOTAL_STOCK_QNT = _StocktransferModel.TOTAL_STOCK_QNT;
                _stock.IS_DELETE = false;
                _stock.IS_NEGATIVE_STOCK_MESSAGE = _StocktransferModel.IS_NEGATIVE_STOCK_MESSAGE;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        [HttpGet]
        public HttpResponseMessage GetStockTransferNo()
        {

            string value = Convert.ToString(db.TBL_STOCK_TRANSFER
                            .OrderByDescending(p => p.STOCK_TRANSFER_NUMBER)
                            .Select(r => r.STOCK_TRANSFER_NUMBER)
                            .First());
            var RefNumber = new
            {
                SuppRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
    }
}
