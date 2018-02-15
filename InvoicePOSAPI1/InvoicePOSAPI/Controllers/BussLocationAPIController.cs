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
    public class BussLocationAPIController : ApiController
    {
        BusinessLocationModel bl = new BusinessLocationModel();
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage GetAllBusinessLo(int id)
        {
            var str = (from a in db.TBL_BUSINESS_LOCATION
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new BusinessLocationModel
                       {
                           BUSINESS_LOCATION_ID = a.BUSINESS_LOCATION_ID,
                           BUSS_ADDRESS_1 = a.BUSS_ADDRESS_1,
                           BUSS_ADDRESS_2 = a.BUSS_ADDRESS_2,
                           CITY = a.CITY,
                           COMPANY = a.COMPANY,
                           COMPANY_ID = a.COMPANY_ID,
                           COUNTRY = a.COUNTRY,
                           DOCUMENT_NO = a.DOCUMENT_NO,
                           EMAIL = a.EMAIL,
                           EMAIL_SETTING = a.EMAIL_SETTING,
                           EMAIL_TEMPLATE_SETTING = a.EMAIL_TEMPLATE_SETTING,
                           GODOWN_TO_KEEP = a.GODOWN_TO_KEEP,
                           IMAGE = a.IMAGE,
                           IS_ASK_TO_PRIENT_EVERYTIME = a.IS_ASK_TO_PRIENT_EVERYTIME,
                           IS_DELETE_INVOICE_SPECIFIED_GODOWN = a.IS_DELETE_INVOICE_SPECIFIED_GODOWN,
                           IS_ENABLE_EMAIL = a.IS_ENABLE_EMAIL,
                           IS_SCEOND_RECEIPT_PRINTER = a.IS_SCEOND_RECEIPT_PRINTER,
                           IS_SECOND_DIFF_PRINT = a.IS_SECOND_DIFF_PRINT,
                           MOBILE_NO = a.MOBILE_NO,
                           NUMBER_OF_SECOND_RECEIPT_PRINT = a.NUMBER_OF_SECOND_RECEIPT_PRINT,
                           PHONE_NO = a.PHONE_NO,
                           PIN = a.PIN,
                           PREFIX_DOCUMENT = a.PREFIX_DOCUMENT,
                           PRINTER_SETTING = a.PRINTER_SETTING,
                           SCEOND_RECEIPT_PRINT_FORMATE = a.SCEOND_RECEIPT_PRINT_FORMATE,
                           SCEOND_RECEIPT_PRINTER = a.SCEOND_RECEIPT_PRINT_FORMATE,
                           SHOP_NAME = a.SHOP_NAME,
                           SMS_SETTING = a.SMS_SETTING,
                           STATE = a.STATE,
                           TIN_NUMBER = a.TIN_NUMBER,
                           WEBSITE = a.WEBSITE


                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetEditBussLocation(int id)
        {
            var str = (from a in db.TBL_BUSINESS_LOCATION
                       where a.BUSINESS_LOCATION_ID == id
                       select new BusinessLocationModel
                       {
                           BUSINESS_LOCATION_ID = a.BUSINESS_LOCATION_ID,
                           BUSS_ADDRESS_1 = a.BUSS_ADDRESS_1,
                           BUSS_ADDRESS_2 = a.BUSS_ADDRESS_2,
                           CITY = a.CITY,
                           COMPANY = a.COMPANY,
                           COMPANY_ID = a.COMPANY_ID,
                           COUNTRY = a.COUNTRY,
                           DOCUMENT_NO = a.DOCUMENT_NO,
                           EMAIL = a.EMAIL,
                           EMAIL_SETTING = a.EMAIL_SETTING,
                           EMAIL_TEMPLATE_SETTING = a.EMAIL_TEMPLATE_SETTING,
                           GODOWN_TO_KEEP = a.GODOWN_TO_KEEP,
                           IMAGE = a.IMAGE,
                           IS_ASK_TO_PRIENT_EVERYTIME = a.IS_ASK_TO_PRIENT_EVERYTIME,
                           IS_DELETE_INVOICE_SPECIFIED_GODOWN = a.IS_DELETE_INVOICE_SPECIFIED_GODOWN,
                           IS_ENABLE_EMAIL = a.IS_ENABLE_EMAIL,
                           IS_SCEOND_RECEIPT_PRINTER = a.IS_SCEOND_RECEIPT_PRINTER,
                           IS_SECOND_DIFF_PRINT = a.IS_SECOND_DIFF_PRINT,
                           MOBILE_NO = a.MOBILE_NO,
                           NUMBER_OF_SECOND_RECEIPT_PRINT = a.NUMBER_OF_SECOND_RECEIPT_PRINT,
                           PHONE_NO = a.PHONE_NO,
                           PIN = a.PIN,
                           PREFIX_DOCUMENT = a.PREFIX_DOCUMENT,
                           PRINTER_SETTING = a.PRINTER_SETTING,
                           SCEOND_RECEIPT_PRINT_FORMATE = a.SCEOND_RECEIPT_PRINT_FORMATE,
                           SCEOND_RECEIPT_PRINTER = a.SCEOND_RECEIPT_PRINT_FORMATE,
                           SHOP_NAME = a.SHOP_NAME,
                           SMS_SETTING = a.SMS_SETTING,
                           STATE = a.STATE,
                           TIN_NUMBER = a.TIN_NUMBER,
                           WEBSITE = a.WEBSITE,
                           IS_DELETE = a.IS_DELETE,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage DeleteBussLocation(int id)
        {
            var str = (from a in db.TBL_BUSINESS_LOCATION where a.BUSINESS_LOCATION_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpPost]
        public HttpResponseMessage BussLocationAdd(BusinessLocationModel _BusinessLocationModel)
        {
            TBL_BUSINESS_LOCATION loc = new TBL_BUSINESS_LOCATION();
            loc.BUSS_ADDRESS_1 = _BusinessLocationModel.BUSS_ADDRESS_1;
            loc.BUSS_ADDRESS_2 = _BusinessLocationModel.BUSS_ADDRESS_2;
            loc.CITY = _BusinessLocationModel.CITY;
            loc.COMPANY = _BusinessLocationModel.COMPANY;
            loc.COMPANY_ID = _BusinessLocationModel.COMPANY_ID;
            loc.COUNTRY = _BusinessLocationModel.COUNTRY;
            loc.DOCUMENT_NO = _BusinessLocationModel.DOCUMENT_NO;
            loc.EMAIL = _BusinessLocationModel.EMAIL;
            loc.EMAIL_SETTING = _BusinessLocationModel.EMAIL_SETTING;
            loc.EMAIL_TEMPLATE_SETTING = _BusinessLocationModel.EMAIL_TEMPLATE_SETTING;
            loc.GODOWN_TO_KEEP = _BusinessLocationModel.GODOWN_TO_KEEP;
            loc.IMAGE = _BusinessLocationModel.IMAGE;
            loc.IS_ASK_TO_PRIENT_EVERYTIME = _BusinessLocationModel.IS_ASK_TO_PRIENT_EVERYTIME;
            loc.IS_DELETE_INVOICE_SPECIFIED_GODOWN = _BusinessLocationModel.IS_DELETE_INVOICE_SPECIFIED_GODOWN;
            loc.IS_ENABLE_EMAIL = _BusinessLocationModel.IS_ENABLE_EMAIL;
            loc.IS_SCEOND_RECEIPT_PRINTER = _BusinessLocationModel.IS_SCEOND_RECEIPT_PRINTER;
            loc.IS_SECOND_DIFF_PRINT = _BusinessLocationModel.IS_SECOND_DIFF_PRINT;
            loc.MOBILE_NO = _BusinessLocationModel.MOBILE_NO;
            loc.NUMBER_OF_SECOND_RECEIPT_PRINT = _BusinessLocationModel.NUMBER_OF_SECOND_RECEIPT_PRINT;
            loc.PHONE_NO = _BusinessLocationModel.PHONE_NO;
            loc.PIN = _BusinessLocationModel.PIN;
            loc.PREFIX_DOCUMENT = _BusinessLocationModel.PREFIX_DOCUMENT;
            loc.PRINTER_SETTING = _BusinessLocationModel.PRINTER_SETTING;
            loc.SCEOND_RECEIPT_PRINT_FORMATE = _BusinessLocationModel.SCEOND_RECEIPT_PRINT_FORMATE;
            loc.SCEOND_RECEIPT_PRINTER = _BusinessLocationModel.SCEOND_RECEIPT_PRINTER;
            loc.SHOP_NAME = _BusinessLocationModel.SHOP_NAME;
            loc.SMS_SETTING = _BusinessLocationModel.SMS_SETTING;
            loc.STATE = _BusinessLocationModel.STATE;
            loc.TIN_NUMBER = _BusinessLocationModel.TIN_NUMBER;
            loc.WEBSITE = _BusinessLocationModel.WEBSITE;
            loc.IS_DELETE = false;
            db.TBL_BUSINESS_LOCATION.Add(loc);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");

        }
        [HttpPost]
        public HttpResponseMessage BussLocationUpdate(BusinessLocationModel _BusinessLocationModel)
        {
            var loc = (from a in db.TBL_BUSINESS_LOCATION where a.BUSINESS_LOCATION_ID == _BusinessLocationModel.BUSINESS_LOCATION_ID select a).FirstOrDefault();
            loc.BUSS_ADDRESS_1 = _BusinessLocationModel.BUSS_ADDRESS_1;
            loc.BUSS_ADDRESS_2 = _BusinessLocationModel.BUSS_ADDRESS_2;
            loc.CITY = _BusinessLocationModel.CITY;
            loc.COMPANY = _BusinessLocationModel.COMPANY;
            loc.COMPANY_ID = _BusinessLocationModel.COMPANY_ID;
            loc.COUNTRY = _BusinessLocationModel.COUNTRY;
            loc.DOCUMENT_NO = _BusinessLocationModel.DOCUMENT_NO;
            loc.EMAIL = _BusinessLocationModel.EMAIL;
            loc.EMAIL_SETTING = _BusinessLocationModel.EMAIL_SETTING;
            loc.EMAIL_TEMPLATE_SETTING = _BusinessLocationModel.EMAIL_TEMPLATE_SETTING;
            loc.GODOWN_TO_KEEP = _BusinessLocationModel.GODOWN_TO_KEEP;
            loc.IMAGE = _BusinessLocationModel.IMAGE;
            loc.IS_ASK_TO_PRIENT_EVERYTIME = _BusinessLocationModel.IS_ASK_TO_PRIENT_EVERYTIME;
            loc.IS_DELETE_INVOICE_SPECIFIED_GODOWN = _BusinessLocationModel.IS_DELETE_INVOICE_SPECIFIED_GODOWN;
            loc.IS_ENABLE_EMAIL = _BusinessLocationModel.IS_ENABLE_EMAIL;
            loc.IS_SCEOND_RECEIPT_PRINTER = _BusinessLocationModel.IS_SCEOND_RECEIPT_PRINTER;
            loc.IS_SECOND_DIFF_PRINT = _BusinessLocationModel.IS_SECOND_DIFF_PRINT;
            loc.MOBILE_NO = _BusinessLocationModel.MOBILE_NO;
            loc.NUMBER_OF_SECOND_RECEIPT_PRINT = _BusinessLocationModel.NUMBER_OF_SECOND_RECEIPT_PRINT;
            loc.PHONE_NO = _BusinessLocationModel.PHONE_NO;
            loc.PIN = _BusinessLocationModel.PIN;
            loc.PREFIX_DOCUMENT = _BusinessLocationModel.PREFIX_DOCUMENT;
            loc.PRINTER_SETTING = _BusinessLocationModel.PRINTER_SETTING;
            loc.SCEOND_RECEIPT_PRINT_FORMATE = _BusinessLocationModel.SCEOND_RECEIPT_PRINT_FORMATE;
            loc.SCEOND_RECEIPT_PRINTER = _BusinessLocationModel.SCEOND_RECEIPT_PRINTER;
            loc.SHOP_NAME = _BusinessLocationModel.SHOP_NAME;
            loc.SMS_SETTING = _BusinessLocationModel.SMS_SETTING;
            loc.STATE = _BusinessLocationModel.STATE;
            loc.TIN_NUMBER = _BusinessLocationModel.TIN_NUMBER;
            loc.WEBSITE = _BusinessLocationModel.WEBSITE;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
    }
}
