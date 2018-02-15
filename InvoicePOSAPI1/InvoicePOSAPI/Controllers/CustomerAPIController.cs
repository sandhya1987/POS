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
    public class CustomerAPIController : ApiController
    {
        NEW_POSEntities db = new NEW_POSEntities();
        CustomerModel cm = new CustomerModel();
        [HttpGet]
        public HttpResponseMessage GetCustomer(int id)
        {
            //CompanyModel model = new CompanyModel();

            var str = (from a in db.TBL_CUSTOMER
                       join b in db.TBL_CUSTOMER_SHIPPING_ADDRESS on a.CUSTOMER_ID equals b.CUSTOMER_ID
                       join c in db.TBL_CUSTOMER_BILLING_ADDRESS on a.CUSTOMER_ID equals c.CUSTOMER_ID
                       join d in db.TBL_OPENING_BALANCE on a.CUSTOMER_ID equals d.CUSTOMER_ID
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new CustomerModel
                       {
                           IMAGE_PATH = a.IMAGE_PATH,
                           COMPANY_ID = a.COMPANY_ID,
                           NAME = a.FIRST_NAME,
                           LAST_NAME = a.LAST_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           VAT_NUMBER = a.VAT_NUMBER,
                           CST_NUMBER = a.CST_NUMBER,
                           LOYALTY_NO = a.LOYALTY_NO,
                           DEFAULT_CREIT_LIMIT = a.DEFAULT_CREIT_LIMIT,
                           SAMEBILLINGANDSHIPPING_ADDRESS = a.SAMEBILLINGANDSHIPPING_ADDRESS,
                           IS_ENROLLED_FOR_LOYALITY = a.IS_ENROLLED_FOR_LOYALITY,
                           CUSTOMER_ID = a.CUSTOMER_ID,
                           BILLING_ADDRESS1 = c.ADDRESS_1,
                           BILLING_ADDRESS2 = c.ADDRESS_2,
                           CITY = c.CITY,
                           TIN = a.TIN_NUMBER,
                           PAN = a.PAN_NUMBER,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           BUSINESS_LOCATION_ID = a.BUSINESS_LOCATION_ID,
                           COUNTRY = c.COUNTRY,
                           CUSTOMER_GROUP = a.CUSTOMER_GROUP,
                           CUSTOMER_NUMBER = a.CUSTOMER_NUMBER,
                           EMAIL_ADDRESS = c.EMAIL,
                           IS_ACTIVE = a.IS_ACTIVE,
                           MOBILE_NO = c.MOBILE,
                           NOTES = a.NOTES,
                           //OPENING_AMT=a.OPENING_BALANCE,
                           OPENING_AMT = a.OPENING_BALANCE,
                           POSTAL_CODE = c.POSTAL_CODE,
                           REFERRED_BY = a.REFERRED_BY,
                           STATE = c.STATE,
                           TELEPHON_NUMBER = c.TELEPHONE,
                           SHIPPING_ADDRESS1 = b.ADDRESS_1,
                           SHIPPING_ADDRESS2 = b.ADDRESS_2,
                           SHIPPING_CITY = b.CITY,
                           SHIPPING_COUNTRY = b.COUNTRY,
                           SHIPPING_EMAIL_ADDRESS = b.EMAIL,
                           SHIPPING_MOBILE_NO = b.MOBILE,
                           SHIPPING_TELEPHON_NUMBER = b.TELEPHONE,
                           SHIPPING_POSTAL_CODE = b.POSTAL_CODE,
                           SHIPPING_STATE = b.STATE,
                           BAL_TYPE_VALUE = d.BAL_TYPE_VALUE,
                           CURRENT_OPENING_BALANCE = d.CURRENT_OPENING_BALANCE,

                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);

        }
        [HttpGet]
        public HttpResponseMessage SearchCustomer(string name)
        {
            var str = (from a in db.TBL_CUSTOMER
                       join b in db.TBL_CUSTOMER_SHIPPING_ADDRESS on a.CUSTOMER_ID equals b.CUSTOMER_ID
                       join c in db.TBL_CUSTOMER_BILLING_ADDRESS on a.CUSTOMER_ID equals c.CUSTOMER_ID
                       join d in db.TBL_OPENING_BALANCE on a.CUSTOMER_ID equals d.CUSTOMER_ID
                       where a.SEARCH_CODE.Contains("" + name + "") && a.IS_DELETE == false
                       select new CustomerModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           NAME = a.FIRST_NAME,
                           LAST_NAME = a.LAST_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           VAT_NUMBER = a.VAT_NUMBER,
                           CST_NUMBER = a.CST_NUMBER,
                           LOYALTY_NO = a.LOYALTY_NO,
                           DEFAULT_CREIT_LIMIT = a.DEFAULT_CREIT_LIMIT,
                           SAMEBILLINGANDSHIPPING_ADDRESS = a.SAMEBILLINGANDSHIPPING_ADDRESS,
                           IS_ENROLLED_FOR_LOYALITY = a.IS_ENROLLED_FOR_LOYALITY,
                           CUSTOMER_ID = a.CUSTOMER_ID,
                           BILLING_ADDRESS1 = c.ADDRESS_1,
                           BILLING_ADDRESS2 = c.ADDRESS_2,
                           CITY = c.CITY,
                           TIN = a.TIN_NUMBER,
                           PAN = a.PAN_NUMBER,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           BUSINESS_LOCATION_ID = a.BUSINESS_LOCATION_ID,
                           COUNTRY = c.COUNTRY,
                           CUSTOMER_GROUP = a.CUSTOMER_GROUP,
                           CUSTOMER_NUMBER = a.CUSTOMER_NUMBER,
                           EMAIL_ADDRESS = c.EMAIL,
                           IS_ACTIVE = a.IS_ACTIVE,
                           MOBILE_NO = c.MOBILE,
                           NOTES = a.NOTES,
                           //OPENING_AMT=a.OPENING_BALANCE,
                           OPENING_AMT = a.OPENING_BALANCE,
                           POSTAL_CODE = c.POSTAL_CODE,
                           REFERRED_BY = a.REFERRED_BY,
                           STATE = c.STATE,
                           TELEPHON_NUMBER = c.TELEPHONE,
                           SHIPPING_ADDRESS1 = b.ADDRESS_1,
                           SHIPPING_ADDRESS2 = b.ADDRESS_2,
                           SHIPPING_CITY = b.CITY,
                           SHIPPING_COUNTRY = b.COUNTRY,
                           SHIPPING_EMAIL_ADDRESS = b.EMAIL,
                           SHIPPING_MOBILE_NO = b.MOBILE,
                           SHIPPING_TELEPHON_NUMBER = b.TELEPHONE,
                           SHIPPING_POSTAL_CODE = b.POSTAL_CODE,
                           SHIPPING_STATE = b.STATE,
                           BAL_TYPE_VALUE = d.BAL_TYPE_VALUE,
                           CURRENT_OPENING_BALANCE = d.CURRENT_OPENING_BALANCE,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);

        }
        [HttpGet]
        public HttpResponseMessage DeleteCustomer(int id)
        {
            var str = (from a in db.TBL_CUSTOMER where a.CUSTOMER_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");


        }
        [HttpGet]
        public HttpResponseMessage GetCustomerNo1(string CompanyId)
        {
            string code = "";
            string value = Convert.ToString(db.TBL_AUTO_GENERATE_CODE
                            .OrderByDescending(p => p.AUTO_GENERATE_CODE)
                            .Select(r => r.AUTO_GENERATE_CODE)
                            .First());
            var str = (from a in db.TBL_CUSTOMER where a.CST_NUMBER == value select a).ToList();
            if (str.Count != 0)
            {
                string dd = Convert.ToString(value);
                string aaa = dd.Substring(3, 5);
                int xx = Convert.ToInt32(aaa);
                int noo = Convert.ToInt32(xx + 1);
                code = "C-" + noo.ToString("D6");

                TBL_AUTO_GENERATE_CODE _code = new TBL_AUTO_GENERATE_CODE();
                _code.AUTO_GENERATE_CODE = code;
                _code.COMPANY_ID = Convert.ToInt64(CompanyId);
                _code.DEFINE_CODE = "";
                _code.USER_ID = 0;
                db.TBL_AUTO_GENERATE_CODE.Add(_code);
                db.SaveChanges();
            }
            else
            {
                string dd = Convert.ToString(value);
                string aaa = dd.Substring(3, 5);
                int xx = Convert.ToInt32(aaa);
                code = "C-" + xx.ToString("D6");
            
            }
            return Request.CreateResponse(HttpStatusCode.OK, code);
        }
        [HttpGet]
        public HttpResponseMessage GetCustomerNo()
        {

            string value = Convert.ToString(db.TBL_CUSTOMER
                            .OrderByDescending(p => p.CUSTOMER_NUMBER)
                            .Select(r => r.CUSTOMER_NUMBER)
                            .First());
            var RefNumber = new
            {
                ItemRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
        [HttpGet]
        public HttpResponseMessage GetLoyaltyNo(string CompanyId)
        {
            
            string code = "";
            string value = Convert.ToString(db.TBL_AUTO_GENERATE_CODE_FOR_LOYALTY
                            .OrderByDescending(p => p.AUTO_GENERATE_CODE_FOR_LOYALTY)
                            .Select(r => r.AUTO_GENERATE_CODE_FOR_LOYALTY)
                            .First());
            var str = (from a in db.TBL_CUSTOMER where a.LOYALTY_NO == value select a).ToList();
            if (str.Count != 0)
            {
                string dd = Convert.ToString(value);
                string aaa = dd.Substring(2, 5);
                int xx = Convert.ToInt32(aaa);
                int noo = Convert.ToInt32(xx + 1);
                code = "L-" + noo.ToString("D6");

                TBL_AUTO_GENERATE_CODE _code = new TBL_AUTO_GENERATE_CODE();
                _code.AUTO_GENERATE_CODE = code;
                _code.COMPANY_ID = Convert.ToInt64(CompanyId);
                _code.DEFINE_CODE = "";
                _code.USER_ID = 0;
                db.TBL_AUTO_GENERATE_CODE.Add(_code);
                db.SaveChanges();
            }
            else
            {
                string dd = Convert.ToString(value);
                string aaa = dd.Substring(2, 5);
                int xx = Convert.ToInt32(aaa);
                code = "L-" + xx.ToString("D6");
            
            }
            return Request.CreateResponse(HttpStatusCode.OK, code);
        
            //string value = Convert.ToString(db.TBL_CUSTOMER
            //                .OrderByDescending(p => p.LOYALTY_NO)
            //                .Select(r => r.LOYALTY_NO)
            //                .First());
            //var RefNumber = new
            //{
            //    ItemRefNumber = value
            //};
        }
        [HttpGet]
        public HttpResponseMessage EditCustomer(int Custid)
        {
            var str = (from a in db.TBL_CUSTOMER
                       join b in db.TBL_CUSTOMER_SHIPPING_ADDRESS on a.CUSTOMER_ID equals b.CUSTOMER_ID
                       join c in db.TBL_CUSTOMER_BILLING_ADDRESS on a.CUSTOMER_ID equals c.CUSTOMER_ID
                       join d in db.TBL_OPENING_BALANCE on a.CUSTOMER_ID equals d.CUSTOMER_ID
                       where a.CUSTOMER_ID == Custid
                       select new CustomerModel
                       {
                           IMAGE_PATH = a.IMAGE_PATH,
                           COMPANY_ID = a.COMPANY_ID,
                           NAME = a.FIRST_NAME,
                           LAST_NAME = a.LAST_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           VAT_NUMBER = a.VAT_NUMBER,
                           CST_NUMBER = a.CST_NUMBER,
                           LOYALTY_NO = a.LOYALTY_NO,
                           DEFAULT_CREIT_LIMIT = a.DEFAULT_CREIT_LIMIT,
                           SAMEBILLINGANDSHIPPING_ADDRESS = a.SAMEBILLINGANDSHIPPING_ADDRESS,
                           IS_ENROLLED_FOR_LOYALITY = a.IS_ENROLLED_FOR_LOYALITY,
                           CUSTOMER_ID = a.CUSTOMER_ID,
                           BILLING_ADDRESS1 = c.ADDRESS_1,
                           BILLING_ADDRESS2 = c.ADDRESS_2,
                           CITY = c.CITY,
                           COUNTRY = c.COUNTRY,
                           CUSTOMER_GROUP = a.CUSTOMER_GROUP,
                           CUSTOMER_NUMBER = a.CUSTOMER_NUMBER,
                           EMAIL_ADDRESS = c.EMAIL,
                           IS_ACTIVE = a.IS_ACTIVE,
                           MOBILE_NO = c.MOBILE,
                           NOTES = a.NOTES,
                           TIN = a.TIN_NUMBER,
                           PAN = a.PAN_NUMBER,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           BUSINESS_LOCATION_ID = a.BUSINESS_LOCATION_ID,
                           //OPENING_AMT=a.OPENING_BALANCE,
                           OPENING_AMT = a.OPENING_BALANCE,
                           POSTAL_CODE = c.POSTAL_CODE,
                           REFERRED_BY = a.REFERRED_BY,
                           STATE = c.STATE,
                           TELEPHON_NUMBER = c.TELEPHONE,
                           SHIPPING_ADDRESS1 = b.ADDRESS_1,
                           SHIPPING_ADDRESS2 = b.ADDRESS_2,
                           SHIPPING_CITY = b.CITY,
                           SHIPPING_COUNTRY = b.COUNTRY,
                           SHIPPING_EMAIL_ADDRESS = b.EMAIL,
                           SHIPPING_MOBILE_NO = b.MOBILE,
                           SHIPPING_TELEPHON_NUMBER = b.TELEPHONE,
                           SHIPPING_POSTAL_CODE = b.POSTAL_CODE,
                           SHIPPING_STATE = b.STATE,
                           BAL_TYPE_VALUE = d.BAL_TYPE_VALUE,
                           CURRENT_OPENING_BALANCE = d.CURRENT_OPENING_BALANCE,
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);

        }




        [HttpPost]
        public HttpResponseMessage CreateCustomer(CustomerModel _CustomerModel)
        {
            try
            {
                TBL_CUSTOMER cus = new TBL_CUSTOMER();
                cus.FIRST_NAME = _CustomerModel.NAME;
                cus.LAST_NAME = _CustomerModel.LAST_NAME;
                cus.SEARCH_CODE = _CustomerModel.SEARCH_CODE;
                cus.VAT_NUMBER = _CustomerModel.VAT_NUMBER;
                cus.CST_NUMBER = _CustomerModel.CST_NUMBER;
                cus.LOYALTY_NO = _CustomerModel.LOYALTY_NO;
                cus.DEFAULT_CREIT_LIMIT = _CustomerModel.DEFAULT_CREIT_LIMIT;
                cus.CUSTOMER_GROUP = _CustomerModel.CUSTOMER_GROUP;
               cus.IS_ACTIVE = _CustomerModel.IS_ACTIVE;
                cus.CUSTOMER_NUMBER = _CustomerModel.CUSTOMER_NUMBER;
                cus.REFERRED_BY = _CustomerModel.REFERRED_BY;
                cus.SAMEBILLINGANDSHIPPING_ADDRESS = _CustomerModel.SAMEBILLINGANDSHIPPING_ADDRESS;
                cus.IS_ENROLLED_FOR_LOYALITY = _CustomerModel.IS_ENROLLED_FOR_LOYALITY;
                cus.COMPANY_ID = _CustomerModel.COMPANY_ID;
                cus.OPENING_BALANCE = _CustomerModel.OPENING_AMT;
                cus.IS_DELETE = false;
                cus.CATEGORY_ID = 1;
                cus.IMAGE_PATH = _CustomerModel.IMAGE_PATH;
                cus.TIN_NUMBER = _CustomerModel.TIN;
                cus.PAN_NUMBER = _CustomerModel.PAN;
                cus.BUSINESS_LOCATION = _CustomerModel.BUSINESS_LOCATION;
                cus.BUSINESS_LOCATION_ID = _CustomerModel.BUSINESS_LOCATION_ID;
                cus.NOTES = _CustomerModel.NOTES;
                //cus.CUSTOMER_NUMBER = _CustomerModel.NOTES;
                db.TBL_CUSTOMER.Add(cus);
                db.SaveChanges();

                var ID = cus.CUSTOMER_ID;
                TBL_CUSTOMER_SHIPPING_ADDRESS SAdd = new TBL_CUSTOMER_SHIPPING_ADDRESS();
                SAdd.ADDRESS_1 = _CustomerModel.SHIPPING_ADDRESS1;
                SAdd.ADDRESS_2 = _CustomerModel.SHIPPING_ADDRESS2;
                SAdd.CITY = _CustomerModel.SHIPPING_CITY;
                SAdd.COUNTRY = _CustomerModel.SHIPPING_COUNTRY;
                SAdd.CUSTOMER_ID = ID;
                SAdd.EMAIL = _CustomerModel.SHIPPING_EMAIL_ADDRESS;
                SAdd.MOBILE = _CustomerModel.SHIPPING_MOBILE_NO;
                SAdd.TELEPHONE = _CustomerModel.SHIPPING_TELEPHON_NUMBER;
                SAdd.POSTAL_CODE = _CustomerModel.SHIPPING_POSTAL_CODE;
                SAdd.STATE = _CustomerModel.SHIPPING_STATE;
                db.TBL_CUSTOMER_SHIPPING_ADDRESS.Add(SAdd);
                db.SaveChanges();

                TBL_CUSTOMER_BILLING_ADDRESS BAdd = new TBL_CUSTOMER_BILLING_ADDRESS();
                BAdd.ADDRESS_1 = _CustomerModel.BILLING_ADDRESS1;
                BAdd.ADDRESS_2 = _CustomerModel.BILLING_ADDRESS2;
                BAdd.CITY = _CustomerModel.CITY;
                BAdd.COUNTRY = _CustomerModel.COUNTRY;
                BAdd.CUSTOMER_ID = ID;
                BAdd.EMAIL = _CustomerModel.EMAIL_ADDRESS;
                BAdd.MOBILE = _CustomerModel.MOBILE_NO;
                BAdd.POSTAL_CODE = _CustomerModel.POSTAL_CODE;
                BAdd.STATE = _CustomerModel.STATE;
                BAdd.TELEPHONE = _CustomerModel.TELEPHON_NUMBER;
                db.TBL_CUSTOMER_BILLING_ADDRESS.Add(BAdd);
                db.SaveChanges();

                TBL_OPENING_BALANCE OpeningBal = new TBL_OPENING_BALANCE();
                OpeningBal.BAL_TYPE_ID = _CustomerModel.BAL_TYPE_ID;
                OpeningBal.COMPANY_ID = _CustomerModel.COMPANY_ID;
                OpeningBal.BUSINESS_LOCATION = _CustomerModel.BUSINESS_LOCATION;
                OpeningBal.DATE = DateTime.Now;
                OpeningBal.BAL_TYPE_ID = _CustomerModel.BAL_TYPE_ID;
                OpeningBal.BAL_TYPE_VALUE = _CustomerModel.BAL_TYPE_VALUE;
                OpeningBal.OPENING_AMT = _CustomerModel.CURRENT_OPENING_BALANCE;
                OpeningBal.CURRENT_OPENING_BALANCE = _CustomerModel.CURRENT_OPENING_BALANCE;
                OpeningBal.CLOSING_AMT = _CustomerModel.CLOSING_AMT;
                OpeningBal.SYSTEM_DATE = DateTime.Now;
                OpeningBal.CUSTOMER_ID = ID;
                db.TBL_OPENING_BALANCE.Add(OpeningBal);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpPost]
        public HttpResponseMessage CustomerUpdate(CustomerModel _CustomerModel)
        {
            var cus = (from a in db.TBL_CUSTOMER where a.CUSTOMER_ID == _CustomerModel.CUSTOMER_ID select a).FirstOrDefault();
            //TBL_CUSTOMER cus = new TBL_CUSTOMER();
            cus.FIRST_NAME = _CustomerModel.NAME;
            cus.LAST_NAME = _CustomerModel.LAST_NAME;
            cus.SEARCH_CODE = _CustomerModel.SEARCH_CODE;
            cus.VAT_NUMBER = _CustomerModel.VAT_NUMBER;
            cus.CST_NUMBER = _CustomerModel.CST_NUMBER;
            cus.LOYALTY_NO = _CustomerModel.LOYALTY_NO;
            cus.OPENING_BALANCE = _CustomerModel.OPENING_AMT;
            cus.DEFAULT_CREIT_LIMIT = _CustomerModel.DEFAULT_CREIT_LIMIT;
            cus.CUSTOMER_GROUP = _CustomerModel.CUSTOMER_GROUP;
            cus.IS_ACTIVE = _CustomerModel.IS_ACTIVE;
            cus.SAMEBILLINGANDSHIPPING_ADDRESS = _CustomerModel.SAMEBILLINGANDSHIPPING_ADDRESS;
            cus.IS_ENROLLED_FOR_LOYALITY = _CustomerModel.IS_ENROLLED_FOR_LOYALITY;
            cus.CUSTOMER_NUMBER = _CustomerModel.CUSTOMER_NUMBER;
            cus.REFERRED_BY = _CustomerModel.REFERRED_BY;
            cus.TIN_NUMBER = _CustomerModel.TIN;
            cus.PAN_NUMBER = _CustomerModel.PAN;
            cus.BUSINESS_LOCATION = _CustomerModel.BUSINESS_LOCATION;
            cus.BUSINESS_LOCATION_ID = _CustomerModel.BUSINESS_LOCATION_ID;
            cus.IMAGE_PATH = _CustomerModel.IMAGE_PATH;
            //  cus.OPENING_BALANCE = _CustomerModel.OPENING_AMT;

            //db.TBL_CUSTOMER.Add(cus);
            db.SaveChanges();

            var ID = cus.CUSTOMER_ID;
            var SAdd = (from a in db.TBL_CUSTOMER_SHIPPING_ADDRESS where a.CUSTOMER_ID == _CustomerModel.CUSTOMER_ID select a).FirstOrDefault();


            // TBL_CUSTOMER_SHIPPING_ADDRESS SAdd = new TBL_CUSTOMER_SHIPPING_ADDRESS();
            SAdd.ADDRESS_1 = _CustomerModel.SHIPPING_ADDRESS1;
            SAdd.ADDRESS_2 = _CustomerModel.SHIPPING_ADDRESS2;
            SAdd.CITY = _CustomerModel.SHIPPING_CITY;
            SAdd.COUNTRY = _CustomerModel.SHIPPING_COUNTRY;
            SAdd.CUSTOMER_ID = _CustomerModel.CUSTOMER_ID;
            SAdd.EMAIL = _CustomerModel.SHIPPING_EMAIL_ADDRESS;
            SAdd.MOBILE = _CustomerModel.SHIPPING_MOBILE_NO;
            SAdd.TELEPHONE = _CustomerModel.SHIPPING_TELEPHON_NUMBER;
            SAdd.POSTAL_CODE = _CustomerModel.SHIPPING_POSTAL_CODE;
            SAdd.STATE = _CustomerModel.SHIPPING_STATE;
            // db.TBL_CUSTOMER_SHIPPING_ADDRESS.Add(SAdd);
            db.SaveChanges();
            var BAdd = (from a in db.TBL_CUSTOMER_BILLING_ADDRESS where a.CUSTOMER_ID == _CustomerModel.CUSTOMER_ID select a).FirstOrDefault();
            // TBL_CUSTOMER_BILLING_ADDRESS BAdd = new TBL_CUSTOMER_BILLING_ADDRESS();
            BAdd.ADDRESS_1 = _CustomerModel.BILLING_ADDRESS1;
            BAdd.ADDRESS_2 = _CustomerModel.BILLING_ADDRESS2;
            BAdd.CITY = _CustomerModel.CITY;
            BAdd.COUNTRY = _CustomerModel.COUNTRY;
            BAdd.CUSTOMER_ID = _CustomerModel.CUSTOMER_ID;
            BAdd.EMAIL = _CustomerModel.EMAIL_ADDRESS;
            BAdd.MOBILE = _CustomerModel.MOBILE_NO;
            BAdd.POSTAL_CODE = _CustomerModel.POSTAL_CODE;
            BAdd.STATE = _CustomerModel.STATE;
            BAdd.TELEPHONE = _CustomerModel.TELEPHON_NUMBER;
            //db.TBL_CUSTOMER_BILLING_ADDRESS.Add(BAdd);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

    }
}
