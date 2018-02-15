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
    public class CatagoryAPIController : ApiController
    {
        NEW_POSEntities db = new NEW_POSEntities();
        CatagoryModel ca = new CatagoryModel();
        [HttpGet]
        public HttpResponseMessage GetCatagory(int id)
        {
            var str = (from a in db.TBL_CATAGORY
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new CatagoryModel
                       {
                           BAR_CODE_PREFIX = a.BAR_CODE_PREFIX,
                           CATAGORY_DEC = a.CATAGORY_DEC,
                           CATAGORY_ID = a.CATAGORY_ID,
                           CATAGORY_NAME = a.CATAGORY_NAME,
                           COMPANY_ID = a.COMPANY_ID,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           IS_DELETE = a.IS_DELETE,
                           IS_NOT_PROTAL = a.IS_NOT_PROTAL,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage CatagoryEdit(int id)
        {
            var str = (from a in db.TBL_CATAGORY
                       where a.CATAGORY_ID == id && a.IS_DELETE == false
                       select new CatagoryModel
                       {
                           BAR_CODE_PREFIX = a.BAR_CODE_PREFIX,
                           CATAGORY_DEC = a.CATAGORY_DEC,
                          CATAGORY_ID = a.CATAGORY_ID,
                           CATAGORY_NAME = a.CATAGORY_NAME,
                           COMPANY_ID = a.COMPANY_ID,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           IS_DELETE = a.IS_DELETE,
                           IS_NOT_PROTAL = a.IS_NOT_PROTAL,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
            [HttpGet]
        public HttpResponseMessage SearchCatagory(string name)
        {
            var str = (from a in db.TBL_CATAGORY
                       where a.CATAGORY_NAME.Contains("" + name + "") && a.IS_DELETE == false
                       select new CatagoryModel
                       {
                           BAR_CODE_PREFIX = a.BAR_CODE_PREFIX,
                           CATAGORY_DEC = a.CATAGORY_DEC,
                          CATAGORY_ID = a.CATAGORY_ID,
                           CATAGORY_NAME = a.CATAGORY_NAME,
                           COMPANY_ID = a.COMPANY_ID,
                           DISPLAY_INDEX = a.DISPLAY_INDEX,
                           IS_DELETE = a.IS_DELETE,
                           IS_NOT_PROTAL = a.IS_NOT_PROTAL,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage DeleteCatagory(int id)
        {
            var str = (from a in db.TBL_CATAGORY
                       where a.CATAGORY_ID == id
                       select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpPost]
        public HttpResponseMessage CatagoryAdd(CatagoryModel _CatagoryModel)
        {
            TBL_CATAGORY cat = new TBL_CATAGORY();
            cat.BAR_CODE_PREFIX = _CatagoryModel.BAR_CODE_PREFIX;
            cat.CATAGORY_DEC = _CatagoryModel.CATAGORY_DEC;
            cat.CATAGORY_NAME = _CatagoryModel.CATAGORY_NAME;
            cat.COMPANY_ID = _CatagoryModel.COMPANY_ID;
            cat.DISPLAY_INDEX = _CatagoryModel.DISPLAY_INDEX;
            cat.IS_DELETE = false;
            cat.IS_NOT_PROTAL = _CatagoryModel.IS_NOT_PROTAL;
            db.TBL_CATAGORY.Add(cat);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        [HttpPost]
        public HttpResponseMessage CatagoryUpdate(CatagoryModel _CatagoryModel)
        {
            var str = (from a in db.TBL_CATAGORY where a.CATAGORY_ID == _CatagoryModel.CATAGORY_ID select a).FirstOrDefault();
            str.BAR_CODE_PREFIX = _CatagoryModel.BAR_CODE_PREFIX;
            str.CATAGORY_DEC = _CatagoryModel.CATAGORY_DEC;
            str.CATAGORY_NAME = _CatagoryModel.CATAGORY_NAME;
            str.COMPANY_ID = _CatagoryModel.COMPANY_ID;
            str.DISPLAY_INDEX = _CatagoryModel.DISPLAY_INDEX;
            str.IS_DELETE = false;
            str.IS_NOT_PROTAL = _CatagoryModel.IS_NOT_PROTAL;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }


    }
}
