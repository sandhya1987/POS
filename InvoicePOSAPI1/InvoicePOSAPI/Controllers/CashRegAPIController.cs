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
    public class CashRegAPIController : ApiController
    {
        CashRegModel im = new CashRegModel();
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage GetAllCashReg(int id)
        {
            var str = (from a in db.TBL_NEWCASHREGISTER
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new CashRegModel
                       {
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           CASH_REG_NAME = a.CASH_REG_NAME,
                           CASH_REG_NO = a.CASH_REG_NO,
                           CASH_REG_PREFIX = a.CASH_REG_PREFIX,
                           CASH_REGISTERID = a.CASH_REGISTERID,
                           ISADGUSTMENT = a.IS_ADGUSTMENT,
                           LOGIN = a.LOGIN,
                           IS_MAIN_CASH = a.IS_MAIN_CASH,
                           COMPANY_ID = a.COMPANY_ID,
                           CASH_AMOUNT = a.CASH_AMOUNT,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetCASH()
        {

            string value = Convert.ToString(db.TBL_NEWCASHREGISTER
                            .OrderByDescending(p => p.CASH_AMOUNT)
                            .Select(r => r.CASH_AMOUNT)
                            .First());
            var RefNumber = new
            {
                SuppRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }

        [HttpGet]
        public HttpResponseMessage EditCashReg(int id)
        {
            var str = (from a in db.TBL_NEWCASHREGISTER
                       where a.CASH_REGISTERID == id
                       select new CashRegModel
                       {
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           CASH_REG_NAME = a.CASH_REG_NAME,
                           CASH_REG_NO = a.CASH_REG_NO,
                           CASH_REG_PREFIX = a.CASH_REG_PREFIX,
                           CASH_REGISTERID = a.CASH_REGISTERID,
                           ISADGUSTMENT = a.IS_ADGUSTMENT,
                           LOGIN = a.LOGIN,
                           IS_MAIN_CASH = a.IS_MAIN_CASH,
                           COMPANY_ID = a.COMPANY_ID,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage DeleteCashReg(int id)
        {
            var str = (from a in db.TBL_NEWCASHREGISTER where a.CASH_REGISTERID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");


        }
        [HttpPost]
        public HttpResponseMessage CreateCashReg(CashRegModel _CashRegModel)
        {

            TBL_NEWCASHREGISTER _CashReg = new TBL_NEWCASHREGISTER();
            _CashReg.COMPANY_ID = _CashRegModel.COMPANY_ID;
            _CashReg.BUSINESS_LOCATION = _CashRegModel.BUSINESS_LOCATION;
            _CashReg.CASH_REG_NAME = _CashRegModel.CASH_REG_NAME;
            _CashReg.CASH_REG_NO = _CashRegModel.CASH_REG_NO;
            _CashReg.CASH_REG_PREFIX = _CashRegModel.CASH_REG_PREFIX;
            _CashReg.CASH_REGISTERID = _CashRegModel.CASH_REGISTERID;
            _CashReg.IS_ADGUSTMENT = _CashRegModel.ISADGUSTMENT;
            _CashReg.LOGIN = _CashRegModel.LOGIN;
            _CashReg.IS_MAIN_CASH = _CashRegModel.IS_MAIN_CASH;
            _CashReg.IS_DELETE = false;
            _CashReg.CASH_AMOUNT = _CashRegModel.CASH_AMOUNT;

            db.TBL_NEWCASHREGISTER.Add(_CashReg);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpPost]
        public HttpResponseMessage CashRegUpdate(CashRegModel _CashRegModel)
        {
            var str = (from a in db.TBL_NEWCASHREGISTER where a.CASH_REGISTERID == _CashRegModel.CASH_REGISTERID select a).FirstOrDefault();
            str.COMPANY_ID = _CashRegModel.COMPANY_ID;
            str.BUSINESS_LOCATION = _CashRegModel.BUSINESS_LOCATION;
            str.CASH_REG_NAME = _CashRegModel.CASH_REG_NAME;
            str.CASH_REG_NO = _CashRegModel.CASH_REG_NO;
            str.CASH_REG_PREFIX = _CashRegModel.CASH_REG_PREFIX;
            str.CASH_REGISTERID = _CashRegModel.CASH_REGISTERID;
            str.IS_ADGUSTMENT = _CashRegModel.ISADGUSTMENT;
            str.LOGIN = _CashRegModel.LOGIN;
            str.IS_MAIN_CASH = _CashRegModel.IS_MAIN_CASH;
            str.IS_DELETE = false;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpGet]
        public HttpResponseMessage GetBusinessLoc()
        {

            string value = Convert.ToString(db.TBL_BUSINESS_LOCATION
                            .OrderByDescending(p => p.BUSS_ADDRESS_1)
                            .Select(r => r.BUSS_ADDRESS_1)
                            .First());
            var LocList = new
            {
                LocList = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
    }
}
