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
    public class BankAPIController : ApiController
    {
        NEW_POSEntities db = new NEW_POSEntities();
        BankModel _BankModel = new BankModel();
        [HttpGet]
        public HttpResponseMessage GetBank(int id)
        {
            var str = (from a in db.TBL_BANK
                       where a.COMPANY_ID == id && a.IS_DELETED == false
                       select new BankModel
                       {
                           ADDRESS_1 = a.ADDRESS_1,
                           ADDRESS_2 = a.ADDRESS_2,
                           BANK_CODE = a.BANK_CODE,
                           BANK_ID = a.BANK_ID,
                           BANK_NAME = a.BANK_NAME,
                           CITY = a.CITY,
                           COMPANY_ID = a.COMPANY_ID,
                           COUNTRY = a.COUNTRY,
                           EMAIL = a.EMAIL,
                           FAX_NUMBER = a.FAX_NUMBER,
                           IFSC_CODE = a.IFSC_CODE,
                           IS_DELETED = a.IS_DELETED,
                           MOBILE_NUMBER = a.MOBILE_NUMBER,
                           PHONE_NUMBER = a.PHONE_NUMBER,
                           PIN_CODE = a.PIN_CODE,
                           STATE = a.STATE,
                           WEBSITE = a.WEBSITE,


                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetBankAC(int id)
        {
            var str = (from a in db.TBL_BANK_ACCOUNT
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new BankAccountModel
                       {
                           ACCOUNT_DESCRIPTION = a.ACCOUNT_DESCRIPTION,
                           ACCOUNT_NUMBER = a.ACCOUNT_NUMBER,
                           ACCOUNT_SEARCH_CODE = a.ACCOUNT_SEARCH_CODE,
                           BANK_ACCOUNT_ID = a.BANK_ACCOUNT_ID,
                           BANK_ID = a.BANK_ID,
                           BRANCH_NAME = a.BRANCH_NAME,
                           COMPANY_ID = a.COMPANY_ID,
                           IS_DELETE = a.IS_DELETE,
                           PRINTING_FORMAT = a.PRINTING_FORMAT,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage BankEdit(int id)
        {
            var str = (from a in db.TBL_BANK
                       where a.BANK_ID == id
                       select new BankModel
                       {
                           ADDRESS_1 = a.ADDRESS_1,
                           ADDRESS_2 = a.ADDRESS_2,
                           BANK_CODE = a.BANK_CODE,
                           BANK_ID = a.BANK_ID,
                           BANK_NAME = a.BANK_NAME,
                           CITY = a.CITY,
                           COMPANY_ID = a.COMPANY_ID,
                           COUNTRY = a.COUNTRY,
                           EMAIL = a.EMAIL,
                           FAX_NUMBER = a.FAX_NUMBER,
                           IFSC_CODE = a.IFSC_CODE,
                           IS_DELETED = a.IS_DELETED,
                           MOBILE_NUMBER = a.MOBILE_NUMBER,
                           PHONE_NUMBER = a.PHONE_NUMBER,
                           PIN_CODE = a.PIN_CODE,
                           STATE = a.STATE,
                           WEBSITE = a.WEBSITE,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage DeleteBank(int id)
        {
            var str = (from a in db.TBL_BANK where a.BANK_ID == id select a).FirstOrDefault();
            str.IS_DELETED = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]
        public HttpResponseMessage BankAdd(BankModel _BankModel)
        {
            if (_BankModel.BANK_ID == null || _BankModel.BANK_ID == 0)
            {
                TBL_BANK gd = new TBL_BANK();
                gd.ADDRESS_1 = _BankModel.ADDRESS_1;
                gd.ADDRESS_2 = _BankModel.ADDRESS_2;
                gd.BANK_CODE = _BankModel.BANK_CODE;
                gd.BANK_ID = _BankModel.BANK_ID;
                gd.BANK_NAME = _BankModel.BANK_NAME;
                gd.CITY = _BankModel.CITY;
                gd.COMPANY_ID = _BankModel.COMPANY_ID;
                gd.COUNTRY = _BankModel.COUNTRY;
                gd.EMAIL = _BankModel.EMAIL;
                gd.FAX_NUMBER = _BankModel.FAX_NUMBER;
                gd.IFSC_CODE = _BankModel.IFSC_CODE;
                gd.IS_DELETED = false;
                gd.MOBILE_NUMBER = _BankModel.MOBILE_NUMBER;
                gd.PHONE_NUMBER = _BankModel.PHONE_NUMBER;
                gd.PIN_CODE = _BankModel.PIN_CODE;
                gd.STATE = _BankModel.STATE;
                gd.WEBSITE = _BankModel.WEBSITE;
                db.TBL_BANK.Add(gd);
                db.SaveChanges();
            }
             
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]
        public HttpResponseMessage BankUpdate(BankModel _BankModel)
        {
            if (_BankModel.BANK_ID != null || _BankModel.BANK_ID != 0)
            {
                var gd = (from a in db.TBL_BANK
                          where a.BANK_ID == _BankModel.BANK_ID
                          select a).FirstOrDefault();
                gd.ADDRESS_1 = _BankModel.ADDRESS_1;
                gd.ADDRESS_2 = _BankModel.ADDRESS_2;
                gd.BANK_CODE = _BankModel.BANK_CODE;
                gd.BANK_ID = _BankModel.BANK_ID;
                gd.BANK_NAME = _BankModel.BANK_NAME;
                gd.CITY = _BankModel.CITY;
                gd.COMPANY_ID = _BankModel.COMPANY_ID;
                gd.COUNTRY = _BankModel.COUNTRY;
                gd.EMAIL = _BankModel.EMAIL;
                gd.FAX_NUMBER = _BankModel.FAX_NUMBER;
                gd.IFSC_CODE = _BankModel.IFSC_CODE;
                gd.IS_DELETED = false;
                gd.MOBILE_NUMBER = _BankModel.MOBILE_NUMBER;
                gd.PHONE_NUMBER = _BankModel.PHONE_NUMBER;
                gd.PIN_CODE = _BankModel.PIN_CODE;
                gd.STATE = _BankModel.STATE;
                gd.WEBSITE = _BankModel.WEBSITE;
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]
        public HttpResponseMessage BankAddAC(BankAccountModel _BankAccountModel)
        {
            if (_BankAccountModel.BANK_ACCOUNT_ID == null || _BankAccountModel.BANK_ACCOUNT_ID == 0)
            {
                TBL_BANK_ACCOUNT gd = new TBL_BANK_ACCOUNT();
                gd.ACCOUNT_DESCRIPTION = _BankAccountModel.ACCOUNT_DESCRIPTION;
                gd.ACCOUNT_NUMBER = _BankAccountModel.ACCOUNT_NUMBER;
                gd.ACCOUNT_SEARCH_CODE = _BankAccountModel.ACCOUNT_SEARCH_CODE;
                gd.BANK_ID = _BankAccountModel.BANK_ID;
                gd.BRANCH_NAME = _BankAccountModel.BRANCH_NAME;
                gd.COMPANY_ID = _BankAccountModel.COMPANY_ID;
                gd.IS_DELETE = _BankAccountModel.IS_DELETE;
                gd.PRINTING_FORMAT = _BankAccountModel.PRINTING_FORMAT;
                db.TBL_BANK_ACCOUNT.Add(gd);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
    }
}
