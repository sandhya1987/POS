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
                           IS_TRANSFER_CASH_REGISTER = true,
                           TRANSFER_DATE = System.DateTime.Now,
                           CASH_TO_TRANSFER = 1,
                           COMPANY_ID = a.COMPANY_ID,
                           CASH_AMOUNT = a.CASH_AMOUNT,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetCashRegWithChequeNo()
        {
            var data = (from a in db.TBL_CASH_REG
                        join b in db.TBL_CHEQUE on a.INVOICE_ID equals b.INVOICE_ID
                        select new CashRegModel
                        {
                            CASH_REG_NO = 0,
                            ISADGUSTMENT = false,
                            IS_MAIN_CASH = false,
                            IS_TRANSFER_CASH_REGISTER = false,
                            TRANSFER_DATE = a.CASH_REG_DATE,
                            COMPANY_ID = 1,
                            CASH_AMOUNT = a.CASH_AMOUNT.Value,
                            CASH_TO_TRANSFER = 0,
                            CURRENT_CASH = a.CASH_AMOUNT.Value,
                            CHEQUE_NO = b.CHEQUE_NO,
                            CHEQUE_AMOUNT = b.CHEQUE_AMOUNT.Value
                        });
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        public HttpResponseMessage GetAllTransferedCash(int id)
        {
            var str = (from a in db.TBL_TRANSFER_CASH
                       //join b in db.TBL_BUSINESS_LOCATION on a.BUSINESS_LOC equals b.BUSINESS_LOCATION_ID
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new CashRegModel
                       {
                           BUSINESS_LOCATION = a.BUSINESS_LOC,
                           TRANSFER_ID = a.TRANSFER_ID,
                           TRANSFER_CODE = a.CASH_TRANSFER_NUMBER,
                           FROM_TRAN_CASH_REGISTER = a.FROM_CASH_REGISTER,
                           TO_TRAN_CASH_REGISTER = a.TO_CASH_REGISTER,
                           CASH_TO_TRANSFER = a.TOTAL_TRANSFERED_AMOUNT,
                           CASH_REG_NO = 1,
                           IS_MAIN_CASH = false,
                           ISADGUSTMENT = false,
                           COMPANY_ID = a.COMPANY_ID,
                           CASH_AMOUNT = 1,
                           //TRANSFER_DATE = a.TRANSFER_DATE,
                           TRANSFER_DATE = System.DateTime.Now,
                           IS_TRANSFER_CASH_REGISTER = a.IS_TRANSFER_CASH_REGISTER,
                           STATUS = a.STATUS,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        //[HttpGet]
        //public HttpResponseMessage GetAllCashTranscation(int id1,string id2, DateTime id3,DateTime id4)
        //{
        //var str = (from b in db.TBL_INVOICE_PAY
        //           join a in db.TBL_RECEIVE_PAYMENT on b.INVOICE_DATE equals a.DATE
        //           join c in db.TBL_TRANSFER_CASH on a.COMPANY_ID equals c.COMPANY_ID
        //           join d in db.TBL_NEWCASHREGISTER on c.COMPANY_ID equals d.COMPANY_ID
        //           where a.COMPANY_ID == id1 && a.BUSINESS_LOCATION == id2 && b.INVOICE_DATE >= id3 && b.INVOICE_DATE <= id4
        //           group b by new {a.DATE,b.INVOICE_DATE,c.TRANSFER_DATE,a.BUSINESS_LOCATION  }into g 

        //           select new CashRegModel
        //          {

        //              BUSINESS_LOCATION =a.BUSINESS_LOCATION,
        //              TRANSFER_ID = c.TRANSFER_ID,
        //              TRANSFER_CODE = a.CASH_TRANSFER_NUMBER,
        //              FROM_TRAN_CASH_REGISTER = a.FROM_CASH_REGISTER,
        //              TO_TRAN_CASH_REGISTER = a.TO_CASH_REGISTER,

        //              STATUS = a.STATUS,
        //          }).ToList();

        //return Request.CreateResponse(HttpStatusCode.OK, str);
        //}


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
        public HttpResponseMessage ViewTransferCash(int id)
        {
            var str = (from a in db.TBL_TRANSFER_CASH
                       //join b in db.TBL_BUSINESS_LOCATION on a.BUSINESS_LOC equals b.BUSINESS_LOCATION_ID
                       where a.TRANSFER_ID == id && a.IS_DELETE == false
                       select new CashRegModel
                       {
                           BUSINESS_LOCATION = a.BUSINESS_LOC,
                           TRANSFER_ID = a.TRANSFER_ID,
                           TRANSFER_CODE = a.CASH_TRANSFER_NUMBER,
                           FROM_TRAN_CASH_REGISTER = a.FROM_CASH_REGISTER,
                           TO_TRAN_CASH_REGISTER = a.TO_CASH_REGISTER,
                           CASH_TO_TRANSFER = a.TOTAL_TRANSFERED_AMOUNT,
                           IS_TRANSFER_CASH_REGISTER = a.IS_TRANSFER_CASH_REGISTER,
                           CASH_REG_NO = 1,
                           IS_MAIN_CASH = false,
                           ISADGUSTMENT = false,
                           COMPANY_ID = a.COMPANY_ID,
                           CASH_AMOUNT = 1,
                           TRANSFER_DATE = a.TRANSFER_DATE,
                           STATUS = a.STATUS,
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

        [HttpGet]
        public HttpResponseMessage DeleteTransferCash(int id)
        {
            var str = (from a in db.TBL_TRANSFER_CASH where a.TRANSFER_ID == id select a).FirstOrDefault();
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

        [HttpGet]
        public HttpResponseMessage GetTransferNo()
        {

            string value = Convert.ToString(db.TBL_TRANSFER_CASH
                            .OrderByDescending(p => p.CASH_TRANSFER_NUMBER)
                            .Select(r => r.CASH_TRANSFER_NUMBER)
                            .First());
            var RefNumber = new
            {
                SuppRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }


        [HttpPost]
        public HttpResponseMessage TransferCashAdd(CashRegModel crm)
        {
            try
            {

                TBL_TRANSFER_CASH tc = new TBL_TRANSFER_CASH();
                tc.COMPANY_ID = crm.COMPANY_ID;
                tc.BUSINESS_LOC_ID = crm.BUSINESS_LOCATION_ID;
                tc.BUSINESS_LOC = crm.BUSINESS_LOCATION;
                tc.CASH_TRANSFER_NUMBER = crm.TRANSFER_CODE;
                tc.FROM_CASH_REGISTER = crm.FROM_TRAN_CASH_REGISTER;
                tc.TO_CASH_REGISTER = crm.TO_TRAN_CASH_REGISTER;
                tc.TOTAL_TRANSFERED_AMOUNT = crm.CASH_TO_TRANSFER;
                tc.TRANSFER_DATE = crm.TRANSFER_DATE;
                tc.STATUS = "Approved";
                tc.IS_DELETE = false;
                db.TBL_TRANSFER_CASH.Add(tc);
                db.SaveChanges();


                var str = (from a in db.TBL_NEWCASHREGISTER where a.CASH_REGISTERID == crm.CASH_REGISTERID_FROM select a).FirstOrDefault();

                str.CASH_AMOUNT = crm.CURRENT_CASH - crm.CASH_TO_TRANSFER;
                var str1 = (from a in db.TBL_NEWCASHREGISTER where a.CASH_REGISTERID == crm.CASH_REGISTERID_TO select a).FirstOrDefault();

                str1.CASH_AMOUNT = str1.CASH_AMOUNT + crm.CASH_TO_TRANSFER;

                db.SaveChanges();

            }
            catch (Exception ex)
            {

                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

    }
}
