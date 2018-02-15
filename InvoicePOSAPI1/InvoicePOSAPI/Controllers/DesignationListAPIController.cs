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
    public class DesignationListAPIController : ApiController
    {
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage GetDesignation(int id)
        {
            var str = (from a in db.TBL_DESIGNATION
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new DesignationModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           IS_DELETE = a.IS_DELETE,
                           CREATED_BY = a.CREATED_BY,
                           CREATED_DATE = System.DateTime.Now,
                           DESIGNATION_ID = a.DESIGNATION_ID,
                           DESIGNATION_NAME = a.DESIGNATION_NAME,
                           SORT_INDEX = a.SORT_INDEX,
                           STATUS = a.STATUS,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage DesignationEdit(long id)
        {
            var str = (from a in db.TBL_DESIGNATION
                       where a.DESIGNATION_ID == id
                       select new DesignationModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           IS_DELETE = a.IS_DELETE,
                           CREATED_BY = a.CREATED_BY,
                           CREATED_DATE = System.DateTime.Now,
                           DESIGNATION_ID = a.DESIGNATION_ID,
                           DESIGNATION_NAME = a.DESIGNATION_NAME,
                           SORT_INDEX = a.SORT_INDEX,
                           STATUS = a.STATUS,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage DeleteDesignation(int id)
        {
            var str = (from a in db.TBL_DESIGNATION where a.DESIGNATION_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]
        public HttpResponseMessage DesignationAdd(DesignationModel _DesignationModel)
        {
            if (_DesignationModel.DESIGNATION_ID == null || _DesignationModel.DESIGNATION_ID == 0)
            {
                TBL_DESIGNATION gd = new TBL_DESIGNATION();
                gd.COMPANY_ID = _DesignationModel.COMPANY_ID;
                gd.DESIGNATION_NAME = _DesignationModel.DESIGNATION_NAME;
                gd.SORT_INDEX = _DesignationModel.SORT_INDEX;
                gd.IS_DELETE = false;
                gd.STATUS = _DesignationModel.STATUS;
                gd.CREATED_BY = _DesignationModel.CREATED_BY;
                gd.CREATED_DATE = System.DateTime.Now;
                db.TBL_DESIGNATION.Add(gd);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]
        public HttpResponseMessage DesignationUpdate(DesignationModel _DesignationModel)
        {
            if (_DesignationModel.DESIGNATION_ID != null || _DesignationModel.DESIGNATION_ID != 0)
            {
                var gd = (from a in db.TBL_DESIGNATION where a.DESIGNATION_ID == _DesignationModel.DESIGNATION_ID select a).FirstOrDefault();
                gd.COMPANY_ID = _DesignationModel.COMPANY_ID;
                gd.DESIGNATION_NAME = _DesignationModel.DESIGNATION_NAME;
                gd.SORT_INDEX = _DesignationModel.SORT_INDEX;
                gd.IS_DELETE = false;
                gd.STATUS = _DesignationModel.STATUS;
                gd.CREATED_BY = _DesignationModel.CREATED_BY;
                gd.CREATED_DATE = System.DateTime.Now;
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
    }
}
