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
    public class GodownAPIController : ApiController
    {
        GodownModel _GodownModel = new GodownModel();
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage GetGodown(int id)
        {
            var str = (from a in db.TBL_GODOWN
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new GodownModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           GODOWN_NAME = a.GODOWN_NAME,
                           GOSOWN_DESCRIPTION = a.GOSOWN_DESCRIPTION,
                           IS_ACTIVE = a.IS_ACTIVE,
                           IS_DELETE = a.IS_DELETE,
                           IS_DEFAULT_GODOWN = a.IS_DEFAULT_GODOWN,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GodownEdit(int id)
        {
            var str = (from a in db.TBL_GODOWN
                       where a.GODOWN_ID == id
                       select new GodownModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           GODOWN_NAME = a.GODOWN_NAME,
                           GOSOWN_DESCRIPTION = a.GOSOWN_DESCRIPTION,
                           IS_ACTIVE = a.IS_ACTIVE,
                           IS_DELETE = a.IS_DELETE,
                           IS_DEFAULT_GODOWN = a.IS_DEFAULT_GODOWN,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GodownSearch(string name)
        {
            var str = (from a in db.TBL_GODOWN
                       where a.GODOWN_NAME.Contains("" + name + "") && a.IS_DELETE == false
                       select new GodownModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           GODOWN_NAME = a.GODOWN_NAME,
                           GOSOWN_DESCRIPTION = a.GOSOWN_DESCRIPTION,
                           IS_ACTIVE = a.IS_ACTIVE,
                           IS_DELETE = a.IS_DELETE,
                           IS_DEFAULT_GODOWN = a.IS_DEFAULT_GODOWN,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage DeleteGodown(int id)
        {
            var str = (from a in db.TBL_GODOWN where a.GODOWN_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]
        public HttpResponseMessage GodownAdd(GodownModel _GodownModel)
        {
            if (_GodownModel.GODOWN_ID == null || _GodownModel.GODOWN_ID == 0)
            {
                TBL_GODOWN gd = new TBL_GODOWN();
                gd.COMPANY_ID = _GodownModel.COMPANY_ID;
                gd.GODOWN_NAME = _GodownModel.GODOWN_NAME;
                gd.GOSOWN_DESCRIPTION = _GodownModel.GOSOWN_DESCRIPTION;
                gd.IS_ACTIVE = _GodownModel.IS_ACTIVE;
                gd.IS_DELETE = false;
                gd.IS_DEFAULT_GODOWN = false;
                db.TBL_GODOWN.Add(gd);
                db.SaveChanges();
            }
            else
            {
                var str = (from a in db.TBL_GODOWN where a.GODOWN_ID == _GodownModel.GODOWN_ID select a).FirstOrDefault();
                str.COMPANY_ID = _GodownModel.COMPANY_ID;
                str.GODOWN_NAME = _GodownModel.GODOWN_NAME;
                str.GOSOWN_DESCRIPTION = _GodownModel.GOSOWN_DESCRIPTION;
                str.IS_ACTIVE = _GodownModel.IS_ACTIVE;
                str.IS_DELETE = false;
                str.IS_DEFAULT_GODOWN = false;
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpGet]
        public HttpResponseMessage GetStock(int Godownid, int companyid)
        {
            var str = (from a in db.TBL_ITEMS
                       where a.COMPANY_ID == companyid && a.GODOWN_ID == Godownid && a.IS_DELETE == false
                       select new ItemModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           GODOWN_ID = a.GODOWN_ID,
                           BARCODE = a.BARCODE,
                           OPN_QNT = a.OPN_QNT,
                           SEARCH_CODE = a.SERCH_CODE,
                           SALES_UNIT = a.SALES_UNIT
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
    }
}
