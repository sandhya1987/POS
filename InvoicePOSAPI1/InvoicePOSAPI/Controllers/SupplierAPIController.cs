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
    public class SupplierAPIController : ApiController
    {
        SupplierModel im = new SupplierModel();
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage GetSupplier(int id)
        {
            var str = (from a in db.TBL_SUPPLIER
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new SupplierModel
                       {
                           SUPPLIER_CODE = a.SUPPLIER_CODE,
                           SUPPLIER_NAME = a.SUPPLIER_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           VAT_NO = a.VAT_NO,
                           CST_NO = a.CST_NO,
                           TIN_NO = a.TIN_NO,
                           PAN_NO = a.PAN_NO,
                           ADDRESS_1 = a.ADDRESS_1,
                           ADDRESS_2 = a.ADDRESS_2,
                           CITY = a.CITY,
                           STATE = a.STATE,
                           ZIP_CODE = a.ZIP_CODE,
                           OPENING_BALANCE = a.OPENING_BALANCE,
                           IS_PREFERRED_SUPPLIER = a.IS_PREFERRED_SUPPLIER,
                           IS_ACTIVE = a.IS_ACTIVE,
                           PAYMENT_SETTLED = a.PAYMENT_SETTLED,
                           IMAGE_PATH = a.IMAGE_PATH,
                           COMPANY_ID = a.COMPANY_ID,
                           SUPPLIER_ID = a.SUPPLIER_ID,
                           CONTACT_FRIST_NAME = a.CONTACT_FRIST_NAME,
                           CONTACT_LAST_NAME = a.CONTACT_LAST_NAME,
                           CONTACT_TELEPHONE_NO = a.CONTACT_TELEPHONE_NO,
                           CONTACT_FAX_NO = a.CONTACT_FAX_NO,
                           CONTACT_MOBILE_NO = a.CONTACT_MOBILE_NO,
                           CONTACT_WEBSITE = a.CONTACT_WEBSITE,
                           CONTACT_EMAIL = a.CONTACT_EMAIL,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);

        }


        [HttpGet]
        public HttpResponseMessage SearchSupplier(string id)
        {
            var str = (from a in db.TBL_SUPPLIER
                       where a.SUPPLIER_NAME == id && a.IS_DELETE == false
                       select new SupplierModel
                       {
                           SUPPLIER_CODE = a.SUPPLIER_CODE,
                           SUPPLIER_NAME = a.SUPPLIER_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           VAT_NO = a.VAT_NO,
                           CST_NO = a.CST_NO,
                           TIN_NO = a.TIN_NO,
                           PAN_NO = a.PAN_NO,
                           ADDRESS_1 = a.ADDRESS_1,
                           ADDRESS_2 = a.ADDRESS_2,
                           CITY = a.CITY,
                           STATE = a.STATE,
                           ZIP_CODE = a.ZIP_CODE,
                           OPENING_BALANCE = a.OPENING_BALANCE,
                           IS_PREFERRED_SUPPLIER = a.IS_PREFERRED_SUPPLIER,
                           IS_ACTIVE = a.IS_ACTIVE,
                           PAYMENT_SETTLED = a.PAYMENT_SETTLED,
                           IMAGE_PATH = a.IMAGE_PATH,
                           COMPANY_ID = a.COMPANY_ID,
                           SUPPLIER_ID = a.SUPPLIER_ID,
                           CONTACT_FRIST_NAME = a.CONTACT_FRIST_NAME,
                           CONTACT_LAST_NAME = a.CONTACT_LAST_NAME,
                           CONTACT_TELEPHONE_NO = a.CONTACT_TELEPHONE_NO,
                           CONTACT_FAX_NO = a.CONTACT_FAX_NO,
                           CONTACT_MOBILE_NO = a.CONTACT_MOBILE_NO,
                           CONTACT_WEBSITE = a.CONTACT_WEBSITE,
                           CONTACT_EMAIL = a.CONTACT_EMAIL,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);

        }





        [HttpGet]
        public HttpResponseMessage EditSupplier(int id)
        {
            var str = (from a in db.TBL_SUPPLIER
                       where a.SUPPLIER_ID == id && a.IS_DELETE == false
                       select new SupplierModel
                       {
                           SUPPLIER_CODE = a.SUPPLIER_CODE,
                           SUPPLIER_NAME = a.SUPPLIER_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           VAT_NO = a.VAT_NO,
                           CST_NO = a.CST_NO,
                           TIN_NO = a.TIN_NO,
                           PAN_NO = a.PAN_NO,
                           ADDRESS_1 = a.ADDRESS_1,
                           ADDRESS_2 = a.ADDRESS_2,
                           CITY = a.CITY,
                           STATE = a.STATE,
                           ZIP_CODE = a.ZIP_CODE,
                           OPENING_BALANCE = a.OPENING_BALANCE,
                           IS_PREFERRED_SUPPLIER = a.IS_PREFERRED_SUPPLIER,
                           IS_ACTIVE = a.IS_ACTIVE,
                           PAYMENT_SETTLED = a.PAYMENT_SETTLED,
                           IMAGE_PATH = a.IMAGE_PATH,
                           COMPANY_ID = a.COMPANY_ID,
                           SUPPLIER_ID = a.SUPPLIER_ID,
                           CONTACT_FRIST_NAME = a.CONTACT_FRIST_NAME,
                           CONTACT_LAST_NAME = a.CONTACT_LAST_NAME,
                           CONTACT_TELEPHONE_NO = a.CONTACT_TELEPHONE_NO,
                           CONTACT_FAX_NO = a.CONTACT_FAX_NO,
                           CONTACT_MOBILE_NO = a.CONTACT_MOBILE_NO,
                           CONTACT_WEBSITE = a.CONTACT_WEBSITE,
                           CONTACT_EMAIL = a.CONTACT_EMAIL,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);

        }
        [HttpGet]
        public HttpResponseMessage DeleteSupp(int id)
        {
            var str = (from a in db.TBL_SUPPLIER where a.SUPPLIER_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");


        }
        [HttpPost]
        public HttpResponseMessage SupplierAdd(SupplierModel _SupplierModel)
        {

            TBL_SUPPLIER sup = new TBL_SUPPLIER();
            sup.SUPPLIER_CODE = _SupplierModel.SUPPLIER_CODE;
            sup.SUPPLIER_NAME = _SupplierModel.SUPPLIER_NAME;
            sup.SEARCH_CODE = _SupplierModel.SEARCH_CODE;
            sup.VAT_NO = _SupplierModel.VAT_NO;
            sup.CST_NO = _SupplierModel.CST_NO;
            sup.TIN_NO = _SupplierModel.TIN_NO;
            sup.PAN_NO = _SupplierModel.PAN_NO;
            sup.ADDRESS_1 = _SupplierModel.ADDRESS_1;
            sup.ADDRESS_2 = _SupplierModel.ADDRESS_2;
            sup.CITY = _SupplierModel.CITY;
            sup.STATE = _SupplierModel.STATE;
            sup.ZIP_CODE = _SupplierModel.ZIP_CODE;
            sup.OPENING_BALANCE = _SupplierModel.OPENING_BALANCE;
            sup.IS_PREFERRED_SUPPLIER = _SupplierModel.IS_PREFERRED_SUPPLIER;
            sup.IS_ACTIVE = _SupplierModel.IS_ACTIVE;
            sup.PAYMENT_SETTLED = _SupplierModel.PAYMENT_SETTLED;
            sup.IMAGE_PATH = _SupplierModel.IMAGE_PATH;
            sup.COMPANY_ID = _SupplierModel.COMPANY_ID;
            sup.SUPPLIER_ID = _SupplierModel.SUPPLIER_ID;
            sup.CONTACT_FRIST_NAME = _SupplierModel.CONTACT_FRIST_NAME;
            sup.CONTACT_LAST_NAME = _SupplierModel.CONTACT_LAST_NAME;
            sup.CONTACT_TELEPHONE_NO = _SupplierModel.CONTACT_TELEPHONE_NO;
            sup.CONTACT_FAX_NO = _SupplierModel.CONTACT_FAX_NO;
            sup.CONTACT_MOBILE_NO = _SupplierModel.CONTACT_MOBILE_NO;
            sup.CONTACT_WEBSITE = _SupplierModel.CONTACT_WEBSITE;
            sup.CONTACT_EMAIL = _SupplierModel.CONTACT_EMAIL;
            sup.IS_DELETE = false;
            db.TBL_SUPPLIER.Add(sup);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        [HttpPost]
        public HttpResponseMessage SupplierUpdate(SupplierModel _SupplierModel)
        {
            var sup = (from a in db.TBL_SUPPLIER where a.SUPPLIER_ID == _SupplierModel.SUPPLIER_ID select a).FirstOrDefault();
            sup.SUPPLIER_CODE = _SupplierModel.SUPPLIER_CODE;
            sup.SUPPLIER_NAME = _SupplierModel.SUPPLIER_NAME;
            sup.SEARCH_CODE = _SupplierModel.SEARCH_CODE;
            sup.VAT_NO = _SupplierModel.VAT_NO;
            sup.CST_NO = _SupplierModel.CST_NO;
            sup.TIN_NO = _SupplierModel.TIN_NO;
            sup.CST_NO = _SupplierModel.CST_NO;
            sup.ADDRESS_1 = _SupplierModel.ADDRESS_1;
            sup.ADDRESS_2 = _SupplierModel.ADDRESS_2;
            sup.CITY = _SupplierModel.CITY;
            sup.STATE = _SupplierModel.STATE;
            sup.ZIP_CODE = _SupplierModel.ZIP_CODE;
            sup.OPENING_BALANCE = _SupplierModel.OPENING_BALANCE;
            sup.IS_PREFERRED_SUPPLIER = _SupplierModel.IS_PREFERRED_SUPPLIER;
            sup.IS_ACTIVE = _SupplierModel.IS_ACTIVE;
            sup.PAYMENT_SETTLED = _SupplierModel.PAYMENT_SETTLED;
            sup.IMAGE_PATH = _SupplierModel.IMAGE_PATH;
            sup.COMPANY_ID = _SupplierModel.COMPANY_ID;
            sup.SUPPLIER_ID = _SupplierModel.SUPPLIER_ID;
            sup.CONTACT_FRIST_NAME = _SupplierModel.CONTACT_FRIST_NAME;
            sup.CONTACT_LAST_NAME = _SupplierModel.CONTACT_LAST_NAME;
            sup.CONTACT_TELEPHONE_NO = _SupplierModel.CONTACT_TELEPHONE_NO;
            sup.CONTACT_FAX_NO = _SupplierModel.CONTACT_FAX_NO;
            sup.CONTACT_MOBILE_NO = _SupplierModel.CONTACT_MOBILE_NO;
            sup.CONTACT_WEBSITE = _SupplierModel.CONTACT_WEBSITE;
            sup.CONTACT_EMAIL = _SupplierModel.CONTACT_EMAIL;
            sup.IS_DELETE = false;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }


        [HttpGet]
        public HttpResponseMessage GetSupplierNo()
        {

            string value = Convert.ToString(db.TBL_SUPPLIER
                            .OrderByDescending(p => p.SUPPLIER_CODE)
                            .Select(r => r.SUPPLIER_CODE)
                            .First());
            var RefNumber = new
            {
                SuppRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
    }
}
