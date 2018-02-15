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
    public class LoyaltyAPIController : ApiController
    {
        LoyaltyModel _LoyaltyModel = new LoyaltyModel();
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage GetLoyalty(int id)
        {
            var str = (from a in db.TBL_LOYALTY
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new LoyaltyModel
                       {
                           COLLECTION = a.COLLECTION,
                           COMPANY_ID = a.COMPANY_ID,
                           CUSTOMERGROUP = a.CUSTOMERGROUP,
                           FROMDATE = a.FROMDATE,
                           IS_DELETE = a.IS_DELETE,
                           ISACTIVE = a.ISACTIVE,
                           ISDEFAULT = a.ISDEFAULT,
                           LOYALTI_ID = a.LOYALTY_ID,
                           SETTINGSNAME = a.SETTINGSNAME,
                           TODATE = a.TODATE,
                           WEIGHTAGE = a.WEIGHTAGE,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetLoyaltyforflatpoints()
        {
            var str = (from a in db.TBL_LOYALTY_Exceedings

                       select new LoyaltyModel
                       {
                           FlatPoints1 = a.Flatpoints,
                           InvoiceExceeding1 = a.InvoiveExceeding,
                           LOYALTI_ID = 1,
                           FROMDATE = System.DateTime.Now,
                           TODATE = System.DateTime.Now,
                           COMPANY_ID = 1,
                           ISACTIVE = false,
                           ISDEFAULT = false,
                           IS_DELETE = false

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage LoyaltyEdit(int id)
        {
            var str = (from a in db.TBL_LOYALTY
                       where a.LOYALTY_ID == id
                       select new LoyaltyModel
                       {
                           COLLECTION = a.COLLECTION,
                           COMPANY_ID = a.COMPANY_ID,
                           CUSTOMERGROUP = a.CUSTOMERGROUP,
                           FROMDATE = a.FROMDATE,
                           IS_DELETE = a.IS_DELETE,
                           ISACTIVE = a.ISACTIVE,
                           ISDEFAULT = a.ISDEFAULT,
                           LOYALTI_ID = a.LOYALTY_ID,
                           SETTINGSNAME = a.SETTINGSNAME,
                           TODATE = a.TODATE,
                           WEIGHTAGE = a.WEIGHTAGE,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage DeleteLoyalty(int id)
        {
            var str = (from a in db.TBL_LOYALTY where a.LOYALTY_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]
        public HttpResponseMessage LoyaltyAdd(LoyaltyModel _LoyaltyModel)
        {
            if (_LoyaltyModel.LOYALTI_ID == null || _LoyaltyModel.LOYALTI_ID == 0)
            {
                TBL_LOYALTY gd = new TBL_LOYALTY();
                gd.COLLECTION = _LoyaltyModel.COLLECTION;
                gd.COMPANY_ID = _LoyaltyModel.COMPANY_ID;
                gd.CUSTOMERGROUP = _LoyaltyModel.CUSTOMERGROUP;
                gd.FROMDATE = DateTime.Now;
                gd.IS_DELETE = false;
                gd.ISACTIVE = _LoyaltyModel.ISACTIVE;
                gd.ISDEFAULT = _LoyaltyModel.ISDEFAULT;
                gd.SETTINGSNAME = _LoyaltyModel.SETTINGSNAME;
                gd.TODATE = DateTime.Now;
                gd.WEIGHTAGE = _LoyaltyModel.WEIGHTAGE;
                db.TBL_LOYALTY.Add(gd);
                db.SaveChanges();
                if (_LoyaltyModel.ListGrid1.Count > 0)
                {
                    foreach (var item in _LoyaltyModel.ListGrid1)
                    {

                        TBL_LOYALTY_Exceedings tbLexceed = new TBL_LOYALTY_Exceedings();
                        tbLexceed.Company_ID = _LoyaltyModel.COMPANY_ID;
                        tbLexceed.Flatpoints = item.FlatPoints;
                        tbLexceed.InvoiveExceeding = item.InvoiceExceeding;
                        tbLexceed.LOYALTY_ID = item.LOYALTI_ID;
                        db.TBL_LOYALTY_Exceedings.Add(tbLexceed);
                        db.SaveChanges();

                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]

        [HttpGet]
        public HttpResponseMessage GetLoyaltyNo()
        {

            string value = Convert.ToString(db.TBL_LOYALTY
                            .OrderByDescending(p => p.LOYALTY_ID)
                            .Select(r => r.LOYALTY_ID)
                            .First());
            var RefNumber = new
            {
                ItemRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
        public HttpResponseMessage LoyaltyUpdate(LoyaltyModel _LoyaltyModel)
        {
            if (_LoyaltyModel.LOYALTI_ID != null || _LoyaltyModel.LOYALTI_ID != 0)
            {
                var gd = (from a in db.TBL_LOYALTY where a.LOYALTY_ID == _LoyaltyModel.LOYALTI_ID select a).FirstOrDefault();
                gd.COLLECTION = _LoyaltyModel.COLLECTION;
                gd.COMPANY_ID = _LoyaltyModel.COMPANY_ID;
                gd.CUSTOMERGROUP = _LoyaltyModel.CUSTOMERGROUP;
                gd.FROMDATE = DateTime.Now;
                gd.IS_DELETE = false;
                gd.ISACTIVE = _LoyaltyModel.ISACTIVE;
                gd.ISDEFAULT = _LoyaltyModel.ISDEFAULT;
                gd.SETTINGSNAME = _LoyaltyModel.SETTINGSNAME;
                gd.TODATE = DateTime.Now;
                gd.WEIGHTAGE = _LoyaltyModel.WEIGHTAGE;
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
    }
}
