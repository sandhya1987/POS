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
    public class TaxAPIController : ApiController
    {
        TaxModel tx = new TaxModel();
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpPost]
        public HttpResponseMessage TaxAdd(TaxModel _TaxModel)
        {
            if (_TaxModel.TAX_ID == null || _TaxModel.TAX_ID == 0)
            {
                TBL_TAX tx = new TBL_TAX();
                tx.COMPANY_ID = _TaxModel.COMPANY_ID;
                tx.COMPANY = _TaxModel.COMPANY;
                tx.IS_DELETE = false;
                tx.IS_SEPARATE = _TaxModel.IS_SEPARATE;
                tx.TAX_NAME = _TaxModel.TAX_NAME;
                tx.TAX_VALUE = _TaxModel.TAX_VALUE;
                db.TBL_TAX.Add(tx);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpGet]
        //public HttpResponseMessage GetTax(int id, string id2)
        //{
            public HttpResponseMessage GetTax1(string id)
        {
            
            //var str = (from a in db.TBL_TAX
            //           join b in db.TBL_BUSINESS_LOCATION on a.COMPANY equals b.COMPANY
            //           where  a.COMPANY_ID == id && b.COMPANY==id2 && a.IS_DELETE == false
            //           select new TaxModel
            //           {

            var str = (from a in db.TBL_TAX
                       join b in db.TBL_BUSINESS_LOCATION on a.COMPANY equals b.COMPANY
                       where a.COMPANY == id  && a.IS_DELETE == false
                       select new TaxModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           COMPANY = a.COMPANY,
                           IS_SEPARATE = a.IS_SEPARATE,
                           TAX_ID = a.TAX_ID,
                           TAX_NAME = a.TAX_NAME,
                           TAX_VALUE = a.TAX_VALUE,
                           IS_DELETE = a.IS_DELETE,

                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }


        public HttpResponseMessage GetTax(int id)
        {
            var str = (from a in db.TBL_TAX
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new TaxModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           IS_SEPARATE = a.IS_SEPARATE,
                           TAX_ID = a.TAX_ID,
                           TAX_NAME = a.TAX_NAME,
                           TAX_VALUE = a.TAX_VALUE,
                           IS_DELETE = a.IS_DELETE,

                       }).ToList();
                       
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }


    }
}
