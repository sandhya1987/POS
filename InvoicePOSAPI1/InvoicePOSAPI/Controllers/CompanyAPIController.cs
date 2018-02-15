using InvoicePOSAPI.Models;
using InvoicePOSDATA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace InvoicePOSAPI.Controllers
{
    public class CompanyAPIController : ApiController
    {

        NEW_POSEntities db = new NEW_POSEntities();
        CompanyModel cm = new CompanyModel();




        [HttpGet]
        public HttpResponseMessage GetCompany()
        {
            //CompanyModel model = new CompanyModel();
            try
            {

            var str = (from a in db.TBL_COMPANY
                       join b in db.TBL_BANK on a.COMAPNY_ID equals b.COMPANY_ID
                       join c in db.TBL_BANK_ACCOUNT on b.BANK_ID equals c.BANK_ID
                       select new CompanyModel
                       {
                           NAME = a.COMPANY_NAME,
                           COMPANY_ID = a.COMAPNY_ID,
                           ADDRESS_1 = a.ADDRESS_1,
                           ADDRESS_2 = a.ADDRESS_2,
                           EMAIL = a.EMAIL,
                           MOBILE_NUMBER = a.MOBILE_NUMBER,
                           PHONE_NUMBER = a.PHONE_NUMBER,
                           PIN = a.PIN,
                           DEFAULT_TAX_RATE = a.DEFAULT_TAX_RATE,
                           IS_WARNED_FOR_LESS_SALES_PRICE = a.IS_WARNED_FOR_LESS_SALES_PRICE,
                           IS_WARNED_FOR_NEGATIVE_STOCK = a.IS_WARNED_FOR_NEGATIVE_STOCK,
                           FIRST_DAY_OF_FINANCIAL_YEAR = a.FIRST_DAY_OF_FINANCIAL_YEAR,
                           FIRST_MONTH_OF_FINANCIAL_YEAR = a.FIRST_MONTH_OF_FINANCIAL_YEAR,
                           BANK_ID = a.BANK_ID,
                           BANK_ADDRESS_2 = b.ADDRESS_2,
                           BANK_ADDRESS_1 = b.ADDRESS_1,
                           ACCOUNT_NUMBER=c.ACCOUNT_NUMBER,
                           BANK_CITY = b.CITY,
                           BANK_CODE = b.BANK_CODE,
                           BANK_NAME = b.BANK_NAME,
                           BANK_PHONE_NUMBER = b.PHONE_NUMBER,
                           BANK_PIN_CODE = b.PIN_CODE,
                           BANK_STATE = b.STATE,
                           CITY = b.CITY,
                           PREFIX = a.PREFIX,
                           PREFIX_NUM = a.PREFIX_NUM,
                           SHOPNAME = a.SHOPNAME,
                           STATE = b.STATE,
                           TAX_PRINTED_DESCRIPTION = a.TAX_PRINTED_DESCRIPTION,
                           TIN_NUMBER = a.TIN_NUMBER,
                           WEBSITE = a.WEBSITE,
                           IFSC_CODE = b.IFSC_CODE,
                           IMAGE_PATH=a.IMAGE_PATH,


                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, str);

            }
            catch (Exception ex)
            {

                throw;
            }

        }







        [HttpGet]
        public HttpResponseMessage EditCompany(int id)
        {
            try
            {

                var str = (from a in db.TBL_COMPANY
                           join b in db.TBL_BANK on a.COMAPNY_ID equals b.COMPANY_ID
                           join c in db.TBL_BANK_ACCOUNT on b.BANK_ID equals c.BANK_ID
                           where a.COMAPNY_ID == id
                           select new CompanyModel
                           {
                               NAME = a.COMPANY_NAME,
                               COMPANY_ID = a.COMAPNY_ID,
                               ADDRESS_1 = a.ADDRESS_1,
                               ADDRESS_2 = a.ADDRESS_2,
                               EMAIL = a.EMAIL,
                               MOBILE_NUMBER = a.MOBILE_NUMBER,
                               PHONE_NUMBER = a.PHONE_NUMBER,
                               PIN = a.PIN,
                               DEFAULT_TAX_RATE = a.DEFAULT_TAX_RATE,
                               IS_WARNED_FOR_LESS_SALES_PRICE = a.IS_WARNED_FOR_LESS_SALES_PRICE,
                               IS_WARNED_FOR_NEGATIVE_STOCK = a.IS_WARNED_FOR_NEGATIVE_STOCK,
                               FIRST_DAY_OF_FINANCIAL_YEAR = a.FIRST_DAY_OF_FINANCIAL_YEAR,
                               FIRST_MONTH_OF_FINANCIAL_YEAR = a.FIRST_MONTH_OF_FINANCIAL_YEAR,
                               BANK_ID = a.BANK_ID,
                               BANK_ADDRESS_2 = b.ADDRESS_2,
                               BANK_ADDRESS_1 = b.ADDRESS_1,
                               ACCOUNT_NUMBER = c.ACCOUNT_NUMBER,
                               BANK_CITY = b.CITY,
                               BANK_CODE = b.BANK_CODE,
                               BANK_NAME = b.BANK_NAME,
                               BANK_PHONE_NUMBER = b.PHONE_NUMBER,
                               BANK_PIN_CODE = b.PIN_CODE,
                               BANK_STATE = b.STATE,
                               CITY = b.CITY,
                               PREFIX = a.PREFIX,
                               PREFIX_NUM = a.PREFIX_NUM,
                               SHOPNAME = a.SHOPNAME,
                               STATE = b.STATE,
                               TAX_PRINTED_DESCRIPTION = a.TAX_PRINTED_DESCRIPTION,
                               TIN_NUMBER = a.TIN_NUMBER,
                               WEBSITE = a.WEBSITE,
                               IFSC_CODE = b.IFSC_CODE,
                               IMAGE_PATH = a.IMAGE_PATH,


                           }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, str);

            }
            catch (Exception ex)
            {

                throw;
            }

        }













        [HttpPost]
        public HttpResponseMessage CreateCompany(CompanyModel _CompanyModel)
        {




            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }
        
    




            TBL_COMPANY com = new TBL_COMPANY();
            com.COMPANY_NAME = _CompanyModel.NAME;
            com.SHOPNAME = _CompanyModel.SHOPNAME;
            com.PREFIX = _CompanyModel.PREFIX;
            com.PREFIX_NUM = _CompanyModel.PREFIX_NUM;
            com.TIN_NUMBER = _CompanyModel.TIN_NUMBER;
            com.ADDRESS_1 = _CompanyModel.ADDRESS_1;
            com.ADDRESS_2 = _CompanyModel.ADDRESS_2;
            com.CITY = _CompanyModel.CITY;
            com.STATE = _CompanyModel.STATE;
            com.COUNTRY = _CompanyModel.COUNTRY;
            com.PIN = _CompanyModel.PIN;
            com.PHONE_NUMBER = _CompanyModel.PHONE_NUMBER;
            com.MOBILE_NUMBER = _CompanyModel.MOBILE_NUMBER;
            com.EMAIL = _CompanyModel.EMAIL;
            com.WEBSITE = _CompanyModel.WEBSITE;
            com.DEFAULT_TAX_RATE = _CompanyModel.DEFAULT_TAX_RATE;
            com.IS_WARNED_FOR_NEGATIVE_STOCK = _CompanyModel.IS_WARNED_FOR_NEGATIVE_STOCK;
            com.IS_WARNED_FOR_LESS_SALES_PRICE = _CompanyModel.IS_WARNED_FOR_LESS_SALES_PRICE;
            com.TAX_PRINTED_DESCRIPTION = _CompanyModel.TAX_PRINTED_DESCRIPTION;
            com.FIRST_DAY_OF_FINANCIAL_YEAR = _CompanyModel.FIRST_DAY_OF_FINANCIAL_YEAR;
            com.FIRST_MONTH_OF_FINANCIAL_YEAR = _CompanyModel.FIRST_MONTH_OF_FINANCIAL_YEAR;
            com.IMAGE_PATH = _CompanyModel.IMAGE_PATH;
            db.TBL_COMPANY.Add(com);
            db.SaveChanges();
            int ID = com.COMAPNY_ID;
            TBL_BANK Bank = new TBL_BANK();
            Bank.ADDRESS_1 = _CompanyModel.BANK_ADDRESS_1;
            Bank.ADDRESS_2 = _CompanyModel.BANK_ADDRESS_2;
            Bank.BANK_NAME = _CompanyModel.BANK_NAME;
            Bank.CITY = _CompanyModel.BANK_CITY;
            Bank.COMPANY_ID = ID;
            Bank.BANK_CODE = _CompanyModel.BANK_CODE;
            Bank.PIN_CODE = _CompanyModel.BANK_PIN_CODE;
            Bank.PHONE_NUMBER = _CompanyModel.BANK_PHONE_NUMBER;
            Bank.STATE = _CompanyModel.BANK_STATE;
            db.TBL_BANK.Add(Bank);
            db.SaveChanges();
            long ba = Bank.BANK_ID;
            TBL_BANK_ACCOUNT ac = new TBL_BANK_ACCOUNT();
            ac.ACCOUNT_NUMBER = _CompanyModel.ACCOUNT_NUMBER;
            ac.BRANCH_NAME = _CompanyModel.BRANCH_NAME;
            ac.COMPANY_ID = ID;
            ac.BANK_ID = ba;
            ac.BRANCH_NAME = _CompanyModel.BRANCH_NAME;
            db.TBL_BANK_ACCOUNT.Add(ac);
            db.SaveChanges();
            TBL_GODOWN gd = new TBL_GODOWN();
            gd.COMPANY_ID = ID;
            gd.GODOWN_NAME = _CompanyModel.NAME;
            gd.GOSOWN_DESCRIPTION = "Default Godown";
            gd.IS_ACTIVE = true;
            gd.IS_DELETE = false;
            gd.IS_DEFAULT_GODOWN = true;
            return Request.CreateResponse(HttpStatusCode.OK, com);
        }

        [HttpPost]
        public HttpResponseMessage UpdateCompany(CompanyModel _CompanyModel)
        {




            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }


            if (_CompanyModel.COMPANY_ID != null && _CompanyModel.COMPANY_ID != 0)
            {
                var CompnyUp = (from a in db.TBL_COMPANY where a.COMAPNY_ID == _CompanyModel.COMPANY_ID select a).FirstOrDefault();
                CompnyUp.COMPANY_NAME = _CompanyModel.NAME;
                CompnyUp.SHOPNAME = _CompanyModel.SHOPNAME;
                CompnyUp.PREFIX = _CompanyModel.PREFIX;
                CompnyUp.PREFIX_NUM = _CompanyModel.PREFIX_NUM;
                CompnyUp.TIN_NUMBER = _CompanyModel.TIN_NUMBER;
                CompnyUp.ADDRESS_1 = _CompanyModel.ADDRESS_1;
                CompnyUp.ADDRESS_2 = _CompanyModel.ADDRESS_2;
                CompnyUp.CITY = _CompanyModel.CITY;
                CompnyUp.STATE = _CompanyModel.STATE;
                CompnyUp.COUNTRY = _CompanyModel.COUNTRY;
                CompnyUp.PIN = _CompanyModel.PIN;
                CompnyUp.PHONE_NUMBER = _CompanyModel.PHONE_NUMBER;
                CompnyUp.MOBILE_NUMBER = _CompanyModel.MOBILE_NUMBER;
                CompnyUp.EMAIL = _CompanyModel.EMAIL;
                CompnyUp.WEBSITE = _CompanyModel.WEBSITE;
                CompnyUp.DEFAULT_TAX_RATE = _CompanyModel.DEFAULT_TAX_RATE;
                CompnyUp.IS_WARNED_FOR_NEGATIVE_STOCK = _CompanyModel.IS_WARNED_FOR_NEGATIVE_STOCK;
                CompnyUp.IS_WARNED_FOR_LESS_SALES_PRICE = _CompanyModel.IS_WARNED_FOR_LESS_SALES_PRICE;
                CompnyUp.TAX_PRINTED_DESCRIPTION = _CompanyModel.TAX_PRINTED_DESCRIPTION;
                CompnyUp.FIRST_DAY_OF_FINANCIAL_YEAR = _CompanyModel.FIRST_DAY_OF_FINANCIAL_YEAR;
                CompnyUp.FIRST_MONTH_OF_FINANCIAL_YEAR = _CompanyModel.FIRST_MONTH_OF_FINANCIAL_YEAR;
                CompnyUp.IMAGE_PATH = _CompanyModel.IMAGE_PATH;
                db.SaveChanges();

                int ID = CompnyUp.COMAPNY_ID;
                if (_CompanyModel.BANK_ID != 0 && _CompanyModel.BANK_ID != null)
                {
                    var BankUp = (from a in db.TBL_BANK where a.BANK_ID == _CompanyModel.BANK_ID select a).FirstOrDefault();

                    BankUp.ADDRESS_1 = _CompanyModel.BANK_ADDRESS_1;
                    BankUp.ADDRESS_2 = _CompanyModel.BANK_ADDRESS_2;
                    BankUp.BANK_NAME = _CompanyModel.BANK_NAME;
                    BankUp.CITY = _CompanyModel.BANK_CITY;
                    BankUp.COMPANY_ID = ID;
                    BankUp.BANK_CODE = _CompanyModel.BANK_CODE;
                    BankUp.PIN_CODE = _CompanyModel.BANK_PIN_CODE;
                    BankUp.PHONE_NUMBER = _CompanyModel.BANK_PHONE_NUMBER;
                    BankUp.STATE = _CompanyModel.BANK_STATE;
                    db.SaveChanges();
                    long ba = BankUp.BANK_ID;
                    if (_CompanyModel.BANK_ACCOUNT_ID!=null && _CompanyModel.BANK_ACCOUNT_ID!=0 )
                    {
                        var BankAcUp = (from a in db.TBL_BANK_ACCOUNT where a.BANK_ACCOUNT_ID == _CompanyModel.BANK_ACCOUNT_ID select a).FirstOrDefault();
                        BankAcUp.ACCOUNT_NUMBER = _CompanyModel.ACCOUNT_NUMBER;
                        BankAcUp.BRANCH_NAME = _CompanyModel.BRANCH_NAME;
                        BankAcUp.COMPANY_ID = ID;
                        BankAcUp.BANK_ID = ba;
                        BankAcUp.BRANCH_NAME = _CompanyModel.BRANCH_NAME;
                        db.SaveChanges();
                    }
                    
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Ok");
        }



        //[HttpPut]
        //public HttpResponseMessage PutCompany(int id)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, "su");
        //}
    }
}
