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
    public class UnitAPIController : ApiController
    {
        UnitModel um = new UnitModel();
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage GetUnit(int id)
        {
           
            var str = (from a in db.TBL_UNIT_MEASURING
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new UnitModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           IMAGE_PATH = a.IMAGE_PATH,
                           MEASURING_NAME = a.MEASURING_NAME,
                           SHORT_INDAX = a.SHORT_INDAX,
                           UNIT_ID = a.UNIT_ID,
                           IS_DELETE = a.IS_DELETE,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage UnitEdit(int id)
        {
            var str = (from a in db.TBL_UNIT_MEASURING
                       where a.UNIT_ID == id
                       select new UnitModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           IMAGE_PATH = a.IMAGE_PATH,
                           MEASURING_NAME = a.MEASURING_NAME,
                           UNIT_ID = a.UNIT_ID,
                           SHORT_INDAX = a.SHORT_INDAX,
                           IS_DELETE = a.IS_DELETE,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage DeleteUnit(int id)
        {
            var str = (from a in db.TBL_UNIT_MEASURING where a.UNIT_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]
        public HttpResponseMessage UnitAdd(UnitModel _UnitModel)
        {
            if (_UnitModel.UNIT_ID == null || _UnitModel.UNIT_ID == 0)
            {
                TBL_UNIT_MEASURING gd = new TBL_UNIT_MEASURING();
                gd.COMPANY_ID = _UnitModel.COMPANY_ID;
                gd.IMAGE_PATH = _UnitModel.IMAGE_PATH;
                gd.MEASURING_NAME = _UnitModel.MEASURING_NAME;
                gd.SHORT_INDAX = _UnitModel.SHORT_INDAX;
                gd.IS_DELETE = false;
                db.TBL_UNIT_MEASURING.Add(gd);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]
        public HttpResponseMessage UnitUpdate(UnitModel _UnitModel)
        {
            if (_UnitModel.UNIT_ID != null || _UnitModel.UNIT_ID != 0)
            {
                var gd = (from a in db.TBL_UNIT_MEASURING where a.UNIT_ID == _UnitModel.UNIT_ID select a).FirstOrDefault();
                gd.COMPANY_ID = _UnitModel.COMPANY_ID;
                gd.IMAGE_PATH = _UnitModel.IMAGE_PATH;
                gd.MEASURING_NAME = _UnitModel.MEASURING_NAME;
                gd.SHORT_INDAX = _UnitModel.SHORT_INDAX;
                gd.IS_DELETE = false;
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
    }
}
