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
    public class RecPayAPIController : ApiController
    {
        NEW_POSEntities db = new NEW_POSEntities();
        RevPayModel _RecPay = new RevPayModel();
        [HttpGet]
        public HttpResponseMessage GetRecPay(int id)
        {
            var str = (from a in db.TBL_RECEIVE_PAYMENT
                       where a.COMPANY_ID == id && a.IS_DELETE==false
                       select new RevPayModel
                       {
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           BUSINESS_LOCATION_ID = a.BUSINESS_LOCATION_ID,
                           TOTAL_SELECTED_PAY_AMT = a.TOTAL_SELECTED_PAY_AMT,
                           TOTAL_REC_AMT = a.TOTAL_REC_AMT,
                           COMPANY_ID = a.COMPANY_ID,
                           CURRENT_PAY_AMT = a.CURRENT_PAY_AMT,
                           CUSTOMER = a.CUSTOMER,
                           CUSTOMER_CONTACT_NO = a.CUSTOMER_CONTACT_NO,
                           CUSTOMER_EMAIL = a.CUSTOMER_EMAIL,
                           CUSTOMER_ID = a.CUSTOMER_ID,
                           DATE = a.DATE,
                           IS_EMAIL_SEND = a.IS_EMAIL_SEND,
                           OTHER_PAY_AMT = a.OTHER_PAY_AMT,
                           PENDING_AMT = a.PENDING_AMT,
                           RECEIVE_PAY_NO = a.RECEIVE_PAY_NO,
                           RECEIVE_PAYMENT_ID = a.RECEIVE_PAYMENT_ID,
                           RETURNABLE_AMT = a.RETURNABLE_AMT,
                           IS_DELETE=a.IS_DELETE,


                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage DeleteRecPay(int id)
        {
            var str = (from a in db.TBL_RECEIVE_PAYMENT where a.RECEIVE_PAYMENT_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");


        }
        [HttpGet]
        public HttpResponseMessage GetEditRecPay(int id)
        {
            var str = (from a in db.TBL_RECEIVE_PAYMENT
                       where a.RECEIVE_PAYMENT_ID == id && a.IS_DELETE == false
                       select new RevPayModel
                       {
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           BUSINESS_LOCATION_ID = a.BUSINESS_LOCATION_ID,
                           TOTAL_SELECTED_PAY_AMT = a.TOTAL_SELECTED_PAY_AMT,
                           TOTAL_REC_AMT = a.TOTAL_REC_AMT,
                           COMPANY_ID = a.COMPANY_ID,
                           CURRENT_PAY_AMT = a.CURRENT_PAY_AMT,
                           CUSTOMER = a.CUSTOMER,
                           CUSTOMER_CONTACT_NO = a.CUSTOMER_CONTACT_NO,
                           CUSTOMER_EMAIL = a.CUSTOMER_EMAIL,
                           CUSTOMER_ID = a.CUSTOMER_ID,
                           DATE = a.DATE,
                           IS_EMAIL_SEND = a.IS_EMAIL_SEND,
                           OTHER_PAY_AMT = a.OTHER_PAY_AMT,
                           PENDING_AMT = a.PENDING_AMT,
                           RECEIVE_PAY_NO = a.RECEIVE_PAY_NO,
                           RECEIVE_PAYMENT_ID = a.RECEIVE_PAYMENT_ID,
                           RETURNABLE_AMT = a.RETURNABLE_AMT,
                           IS_DELETE = a.IS_DELETE,


                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpPost]
        public HttpResponseMessage InsertRecPay(RevPayModel _recPay)
        {
            try
            {
                TBL_RECEIVE_PAYMENT Rpay = new TBL_RECEIVE_PAYMENT();
                Rpay.BUSINESS_LOCATION = _recPay.BUSINESS_LOCATION;
                Rpay.BUSINESS_LOCATION_ID = 1;
                Rpay.CURRENT_PAY_AMT = _recPay.CURRENT_PAY_AMT;
                Rpay.CUSTOMER = _recPay.CUSTOMER;
                Rpay.CUSTOMER_CONTACT_NO = _recPay.CUSTOMER_CONTACT_NO;
                Rpay.CUSTOMER_EMAIL = _recPay.CUSTOMER_EMAIL;
                Rpay.CUSTOMER_ID = 1;
                Rpay.DATE = DateTime.Now;
                Rpay.IS_EMAIL_SEND = _recPay.IS_EMAIL_SEND;
                Rpay.OTHER_PAY_AMT = _recPay.OTHER_PAY_AMT;
                Rpay.PENDING_AMT = _recPay.PENDING_AMT;
                Rpay.RECEIVE_PAY_NO = _recPay.RECEIVE_PAY_NO;
                Rpay.RETURNABLE_AMT = _recPay.RETURNABLE_AMT;
                Rpay.TOTAL_REC_AMT = _recPay.TOTAL_REC_AMT;
                Rpay.TOTAL_SELECTED_PAY_AMT = _recPay.TOTAL_SELECTED_PAY_AMT;
                Rpay.COMPANY_ID = _recPay.COMPANY_ID;
                Rpay.IS_DELETE = false;
                db.TBL_RECEIVE_PAYMENT.Add(Rpay);
                db.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpPost]
        public HttpResponseMessage UpdateRecPay(RevPayModel _recPay)
        {
            try
            {
               var Rpay = (from a in db.TBL_RECEIVE_PAYMENT where a.RECEIVE_PAYMENT_ID== _recPay.RECEIVE_PAYMENT_ID select a).FirstOrDefault();
                Rpay.BUSINESS_LOCATION = _recPay.BUSINESS_LOCATION;
                Rpay.BUSINESS_LOCATION_ID = 1;
                Rpay.CURRENT_PAY_AMT = _recPay.CURRENT_PAY_AMT;
                Rpay.CUSTOMER = _recPay.CUSTOMER;
                Rpay.CUSTOMER_CONTACT_NO = _recPay.CUSTOMER_CONTACT_NO;
                Rpay.CUSTOMER_EMAIL = _recPay.CUSTOMER_EMAIL;
                Rpay.CUSTOMER_ID = 1;
                Rpay.DATE = DateTime.Now;
                Rpay.IS_EMAIL_SEND = _recPay.IS_EMAIL_SEND;
                Rpay.OTHER_PAY_AMT = _recPay.OTHER_PAY_AMT;
                Rpay.PENDING_AMT = _recPay.PENDING_AMT;
                Rpay.RECEIVE_PAY_NO = _recPay.RECEIVE_PAY_NO;
                Rpay.RETURNABLE_AMT = _recPay.RETURNABLE_AMT;
                Rpay.TOTAL_REC_AMT = _recPay.TOTAL_REC_AMT;
                Rpay.TOTAL_SELECTED_PAY_AMT = _recPay.TOTAL_SELECTED_PAY_AMT;
                Rpay.COMPANY_ID = _recPay.COMPANY_ID;
                Rpay.IS_DELETE = false;
                db.TBL_RECEIVE_PAYMENT.Add(Rpay);
                db.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpGet]
        public HttpResponseMessage GetReceivePayID()
        {

            string value = Convert.ToString(db.TBL_RECEIVE_PAYMENT
                            .OrderByDescending(p => p.RECEIVE_PAY_NO)
                            .Select(r => r.RECEIVE_PAY_NO)
                            .FirstOrDefault());
            var RefNumber = new
            {
                SuppRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }

    }
}
