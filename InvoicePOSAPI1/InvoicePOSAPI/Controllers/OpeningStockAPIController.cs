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
    public class OpeningStockAPIController : ApiController
    {
        OpenIngStockModel opqnt = new OpenIngStockModel();
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage OpenIngStockEdit(int id)
        {
            var str = (from a in db.TBL_OPENING_STOCK
                       where a.OPENING_STOCK_ID == id
                       select new OpenIngStockModel
                       {
                           BUSINESS_LOC_ID = a.BUSINESS_LOC_ID,
                           CLOSING_BAL = a.CLOSING_BAL,
                           COMPANY_ID = a.COMPANY_ID,
                           DATE = a.DATE,
                           ITEM_ID = a.ITEM_ID,
                           ITEM_NAME = a.ITEM_NAME,
                           BARCODE = a.BARCODE,
                           OPENING_STOCK_ID = a.OPENING_STOCK_ID,
                           OPRNING_BAL = a.OPENING_STOCK_ID,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpPost]
        public HttpResponseMessage OpenIngStockAdd(OpenIngStockModel _OpenIngStockModel)
        {
            if (_OpenIngStockModel.OPENING_STOCK_ID == null || _OpenIngStockModel.OPENING_STOCK_ID == 0)
            {
                TBL_OPENING_STOCK stock = new TBL_OPENING_STOCK();
                stock.BUSINESS_LOC_ID = _OpenIngStockModel.BUSINESS_LOC_ID;
                stock.CLOSING_BAL = _OpenIngStockModel.CLOSING_BAL;
                stock.COMPANY_ID = _OpenIngStockModel.COMPANY_ID;
               // stock.DATE = _OpenIngStockModel.DATE;
                stock.DATE = DateTime.Now;
                stock.ITEM_ID = _OpenIngStockModel.ITEM_ID;
                stock.OPRNING_BAL = _OpenIngStockModel.OPENING_STOCK_ID;
                stock.ITEM_NAME = _OpenIngStockModel.ITEM_NAME;
                stock.BARCODE = _OpenIngStockModel.BARCODE;
                stock.COMPANY_NAME = _OpenIngStockModel.COMPANY_NAME;
                stock.OPN_QNT = _OpenIngStockModel.OPN_QNT;
                stock.GODOWN = _OpenIngStockModel.GODOWN;



                db.TBL_OPENING_STOCK.Add(stock);
                db.SaveChanges();
            }
            else
            {
                var stock = (from a in db.TBL_OPENING_STOCK where a.OPENING_STOCK_ID == _OpenIngStockModel.OPENING_STOCK_ID select a).FirstOrDefault();
                stock.BUSINESS_LOC_ID = _OpenIngStockModel.BUSINESS_LOC_ID;
                stock.CLOSING_BAL = _OpenIngStockModel.CLOSING_BAL;
                stock.COMPANY_ID = _OpenIngStockModel.COMPANY_ID;
                stock.DATE = _OpenIngStockModel.DATE;
                stock.ITEM_ID = _OpenIngStockModel.ITEM_ID;
                stock.ITEM_NAME = _OpenIngStockModel.ITEM_NAME;
                stock.BARCODE = _OpenIngStockModel.BARCODE;
                stock.OPRNING_BAL = _OpenIngStockModel.OPENING_STOCK_ID;
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
    }
}
