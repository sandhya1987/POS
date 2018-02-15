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
    public class SuppPaymentAPIController : ApiController
    {
        SuppPaymentModel im = new SuppPaymentModel();
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage GetSuppPayment(int id)
        {
            var str = (from a in db.TBL_SUPP_PAYMENT
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new SuppPaymentModel
                       {
                           SUPP_PAYMENT = a.SUPP_PAYMENT,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           SUPP = a.SUPP,
                           SUPP_ID = a.SUPP_ID,
                           SUPP_EMAIL = a.SUPP_EMAIL,
                           SUPP_SMS = a.SUPP_SMS,
                           IS_SEND_EMAIL = a.IS_SEND_EMAIL,
                           IS_SEND_SMS = a.IS_SEND_SMS,
                           CURRENT_ADV_AMT = a.CURRENT_ADV_AMT,
                           TOTAL_RIE_AMT = a.TOTAL_RIE_AMT,
                           PENDING_AMT = a.PENDING_AMT,
                           PAYMENT_NUMBER = a.PAYMENT_NUMBER,
                           PAYMENT_DATE = a.PAYMENT_DATE,
                           CURRENT_PAYABLE_AMT = a.CURRENT_PAYABLE_AMT,
                           TOTAL_PANDING = a.TOTAL_PANDING,
                           SELECTED_AMT = a.SELECTED_AMT,
                           PENDING_AFTE_PAYMENT = a.PENDING_AFTE_PAYMENT,
                           CASH_REG = a.CASH_REG,
                           CASH_REG_ID = a.CASH_REG_ID,
                           CASH_REG_AMT = a.CASH_REG_AMT,
                           CHEQUE_AMT = a.CHEQUE_AMT,
                           CHEQUE_NO = a.CHEQUE_NO,
                           CHEQUE_BANK_BRANCH = a.TRANSFER_BANK_BRANCH,
                           CHEQUE_BANK_AC = a.CHEQUE_BANK_AC,
                           CHEQUE_DATE = a.CHEQUE_DATE,
                           TRANSFER_AMT = a.TRANSFER_AMT,
                           TRANSFER_BANK_BRANCH = a.TRANSFER_BANK_BRANCH,
                           TRANSFER_BANK_AC = a.TRANSFER_BANK_AC,
                           TRANSFER_DATE = a.TRANSFER_DATE,
                           FINANCAL_AMT = a.FINACIAL_AC,
                           FINACIAL_AC = a.FINACIAL_AC,
                           DISCOUNT_FLAT = a.DISCOUNT_PERCENT,
                           DISCOUNT_PERCENT = a.DISCOUNT_PERCENT,
                           TOTAL_PAYMENT_MODES = a.TOTAL_PAYMENT_MODES,
                           CURRENT_PAYMENT = a.CURRENT_PAYMENT,
                           NOTE = a.NOTE,
                           IS_PRINT_CHECK = a.IS_PRINT_CHECK,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage GetSuppPaymentDetails(int id, int supid)
        {
            var str = (from a in db.TBL_RECEIVE_ITEM
                       join b in db.TBL_SUPPLIER on a.SUPPLIER_ID equals b.SUPPLIER_ID
                       where a.COMPANY_ID == id && a.IS_DELETE == false && a.SUPPLIER_ID == supid
                       select new SuppPaymentModel
                       {

                           TOTAL_AMT = a.TOTAL_AMT,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           SUPPLIER = a.SUPPLIER,
                           SUPPLIER_MOBILE = b.CONTACT_MOBILE_NO,
                           SUPPLIER_EMAIL = b.CONTACT_EMAIL,
                           //SUPPLIER_ID=a.SUPPLIER_ID,
                           ROUND_OFF_ADJUSTMENTAMT = a.ROUND_OFF_ADJUSTMENTAMT,
                           SUPP_DATE = a.SUPPLIER_INVOICE_DATE,
                           RECEIVED_ITEM_ENTRY_NO = a.RECEIVED_ITEM_ENTRY_NO,
                           RECEIVED_ITEM_ON_DATE = a.RECEIVED_ITEM_ON_DATE,
                           SUPPLIER_INVOICE_NO = a.SUPPLIER_INVOICE_NO,
                           PAYMENT_TERMS = a.PAYMENT_TERMS,
                           TOTAL_TAX_AMT = a.TOTAL_TAX_AMT,

                           SUPP_PAYMENT = 1,
                           SUPP = "11",
                           SUPP_ID = 1,
                           SUPP_EMAIL = "11",
                           SUPP_SMS = "11",
                           IS_SEND_EMAIL = true,
                           IS_SEND_SMS = true,
                           CURRENT_ADV_AMT = 1,
                           TOTAL_RIE_AMT = 1,
                           PENDING_AMT = 1,
                           PAYMENT_NUMBER = "11",
                           PAYMENT_DATE = System.DateTime.Now,
                           CURRENT_PAYABLE_AMT = 1,
                           TOTAL_PANDING = 1,
                           SELECTED_AMT = 1,
                           PENDING_AFTE_PAYMENT = 1,
                           CASH_REG = "11",
                           CASH_REG_ID = 1,
                           CASH_REG_AMT = 1,
                           CHEQUE_AMT = 1,
                           CHEQUE_NO = "11",
                           CHEQUE_BANK_BRANCH = "11",
                           CHEQUE_BANK_AC = "11",
                           CHEQUE_DATE = System.DateTime.Now,
                           TRANSFER_AMT = 1,
                           TRANSFER_BANK_BRANCH = "11",
                           TRANSFER_BANK_AC = 1,
                           TRANSFER_DATE = System.DateTime.Now,
                           FINANCAL_AMT = 1,
                           FINACIAL_AC = 1,
                           DISCOUNT_FLAT = 1,
                           DISCOUNT_PERCENT = 1,
                           TOTAL_PAYMENT_MODES = 1,
                           CURRENT_PAYMENT = 1,
                           NOTE = a.NOTE,
                           IS_PRINT_CHECK = true,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetSuppPaymentDetailsafterpayment(int supid)
        {
            var str = (from a in db.tbl_supppaydetails

                       where a.Supp_ID == supid
                       select new SuppPaymentModel
                       {


                           PENDING_AMT = a.pendingamoun,
                           ROUND_OFF_ADJUSTMENTAMT = a.adjustedamount,
                           TOTAL_AMT = a.totalamount,

                           SUPP_PAYMENT = 1,
                           SUPP = "11",
                           SUPP_ID = 1,
                           SUPP_EMAIL = "11",
                           SUPP_SMS = "11",
                           IS_SEND_EMAIL = true,
                           IS_SEND_SMS = true,
                           CURRENT_ADV_AMT = 1,
                           TOTAL_RIE_AMT = 1,
                           //PENDING_AMT = 1,
                           PAYMENT_NUMBER = "11",
                           PAYMENT_DATE = System.DateTime.Now,
                           CURRENT_PAYABLE_AMT = 1,
                           TOTAL_PANDING = 1,
                           SELECTED_AMT = 1,
                           PENDING_AFTE_PAYMENT = 1,
                           CASH_REG = "11",
                           CASH_REG_ID = 1,
                           CASH_REG_AMT = 1,
                           CHEQUE_AMT = 1,
                           CHEQUE_NO = "11",
                           CHEQUE_BANK_BRANCH = "11",
                           CHEQUE_BANK_AC = "11",
                           CHEQUE_DATE = System.DateTime.Now,
                           TRANSFER_AMT = 1,
                           TRANSFER_BANK_BRANCH = "11",
                           TRANSFER_BANK_AC = 1,
                           TRANSFER_DATE = System.DateTime.Now,
                           FINANCAL_AMT = 1,
                           FINACIAL_AC = 1,
                           DISCOUNT_FLAT = 1,
                           DISCOUNT_PERCENT = 1,
                           TOTAL_PAYMENT_MODES = 1,
                           CURRENT_PAYMENT = 1,
                           //NOTE = a.NOTE,
                           IS_PRINT_CHECK = true,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage GetViewItem(int id)
        {
            var str = (from a in db.TBL_SUPP_PAYMENT
                       where a.SUPP_PAYMENT == id && a.IS_DELETE == false
                       select new SuppPaymentModel
                       {
                           SUPP_PAYMENT = a.SUPP_PAYMENT,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           SUPP = a.SUPP,
                           SUPP_ID = a.SUPP_ID,
                           SUPP_EMAIL = a.SUPP_EMAIL,
                           SUPP_SMS = a.SUPP_SMS,
                           IS_SEND_EMAIL = a.IS_SEND_EMAIL,
                           IS_SEND_SMS = a.IS_SEND_SMS,
                           CURRENT_ADV_AMT = a.CURRENT_ADV_AMT,
                           TOTAL_RIE_AMT = a.TOTAL_RIE_AMT,
                           PENDING_AMT = a.PENDING_AMT,
                           PAYMENT_NUMBER = a.PAYMENT_NUMBER,
                           PAYMENT_DATE = a.PAYMENT_DATE,
                           CURRENT_PAYABLE_AMT = a.CURRENT_PAYABLE_AMT,
                           TOTAL_PANDING = a.TOTAL_PANDING,
                           SELECTED_AMT = a.SELECTED_AMT,
                           PENDING_AFTE_PAYMENT = a.PENDING_AFTE_PAYMENT,
                           CASH_REG_ID = a.CASH_REG_ID,
                           CASH_REG_AMT = a.CASH_REG_AMT,
                           CHEQUE_AMT = a.CHEQUE_AMT,
                           CHEQUE_NO = a.CHEQUE_NO,
                           CHEQUE_BANK_BRANCH = a.TRANSFER_BANK_BRANCH,
                           CHEQUE_BANK_AC = a.CHEQUE_BANK_AC,
                           CHEQUE_DATE = a.CHEQUE_DATE,
                           TRANSFER_AMT = a.TRANSFER_AMT,
                           TRANSFER_BANK_BRANCH = a.TRANSFER_BANK_BRANCH,
                           TRANSFER_BANK_AC = a.TRANSFER_BANK_AC,
                           TRANSFER_DATE = a.TRANSFER_DATE,
                           FINANCAL_AMT = a.FINACIAL_AC,
                           FINACIAL_AC = a.FINACIAL_AC,
                           DISCOUNT_FLAT = a.DISCOUNT_PERCENT,
                           DISCOUNT_PERCENT = a.DISCOUNT_PERCENT,
                           TOTAL_PAYMENT_MODES = a.TOTAL_PAYMENT_MODES,
                           CURRENT_PAYMENT = a.CURRENT_PAYMENT,
                           IS_PRINT_CHECK = a.IS_PRINT_CHECK,
                           NOTE = a.NOTE,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetDueDetails(int id, int companyid)
        {
            var str = (from a in db.TBL_SUPP_PAYMENT.OrderByDescending(p => p.SUPP_PAYMENT)

                       where a.SUPP_ID == id && a.COMPANY_ID == companyid && a.IS_DELETE == false
                       select new SuppPaymentModel
                       {
                           SUPP_PAYMENT = a.SUPP_PAYMENT,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           SUPP = a.SUPP,
                           SUPP_ID = a.SUPP_ID,
                           SUPP_EMAIL = a.SUPP_EMAIL,
                           SUPP_SMS = a.SUPP_SMS,
                           IS_SEND_EMAIL = a.IS_SEND_EMAIL,
                           IS_SEND_SMS = a.IS_SEND_SMS,
                           CURRENT_ADV_AMT = a.CURRENT_ADV_AMT,
                           TOTAL_RIE_AMT = a.TOTAL_RIE_AMT,
                           PENDING_AMT = a.PENDING_AMT,
                           PAYMENT_NUMBER = a.PAYMENT_NUMBER,
                           PAYMENT_DATE = a.PAYMENT_DATE,
                           CURRENT_PAYABLE_AMT = a.CURRENT_PAYABLE_AMT,
                           TOTAL_PANDING = a.TOTAL_PANDING,
                           SELECTED_AMT = a.SELECTED_AMT,
                           PENDING_AFTE_PAYMENT = a.PENDING_AFTE_PAYMENT,
                           CASH_REG_ID = a.CASH_REG_ID,
                           CASH_REG_AMT = a.CASH_REG_AMT,
                           CHEQUE_AMT = a.CHEQUE_AMT,
                           CHEQUE_NO = a.CHEQUE_NO,
                           CHEQUE_BANK_BRANCH = a.TRANSFER_BANK_BRANCH,
                           CHEQUE_BANK_AC = a.CHEQUE_BANK_AC,
                           CHEQUE_DATE = a.CHEQUE_DATE,
                           TRANSFER_AMT = a.TRANSFER_AMT,
                           TRANSFER_BANK_BRANCH = a.TRANSFER_BANK_BRANCH,
                           TRANSFER_BANK_AC = a.TRANSFER_BANK_AC,
                           TRANSFER_DATE = a.TRANSFER_DATE,
                           FINANCAL_AMT = a.FINACIAL_AC,
                           FINACIAL_AC = a.FINACIAL_AC,
                           DISCOUNT_FLAT = a.DISCOUNT_PERCENT,
                           DISCOUNT_PERCENT = a.DISCOUNT_PERCENT,
                           TOTAL_PAYMENT_MODES = a.TOTAL_PAYMENT_MODES,
                           CURRENT_PAYMENT = a.CURRENT_PAYMENT,
                           IS_PRINT_CHECK = a.IS_PRINT_CHECK,
                           NOTE = a.NOTE,
                       }).First();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage EditSuppPayment(int id)
        {
            var str = (from a in db.TBL_SUPP_PAYMENT
                       where a.SUPP_PAYMENT == id && a.IS_DELETE == false
                       select new SuppPaymentModel
                       {
                           SUPP_PAYMENT = a.SUPP_PAYMENT,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           SUPP = a.SUPP,
                           SUPP_ID = a.SUPP_ID,
                           SUPP_EMAIL = a.SUPP_EMAIL,
                           SUPP_SMS = a.SUPP_SMS,
                           IS_SEND_EMAIL = a.IS_SEND_EMAIL,
                           IS_SEND_SMS = a.IS_SEND_SMS,
                           CURRENT_ADV_AMT = a.CURRENT_ADV_AMT,
                           TOTAL_RIE_AMT = a.TOTAL_RIE_AMT,
                           PENDING_AMT = a.PENDING_AMT,
                           PAYMENT_NUMBER = a.PAYMENT_NUMBER,
                           PAYMENT_DATE = a.PAYMENT_DATE,
                           CURRENT_PAYABLE_AMT = a.CURRENT_PAYABLE_AMT,
                           TOTAL_PANDING = a.TOTAL_PANDING,
                           SELECTED_AMT = a.SELECTED_AMT,
                           PENDING_AFTE_PAYMENT = a.PENDING_AFTE_PAYMENT,
                           CASH_REG_ID = a.CASH_REG_ID,
                           CASH_REG = a.CASH_REG,
                           CASH_REG_AMT = a.CASH_REG_AMT,
                           CHEQUE_AMT = a.CHEQUE_AMT,
                           CHEQUE_NO = a.CHEQUE_NO,
                           CHEQUE_BANK_BRANCH = a.TRANSFER_BANK_BRANCH,
                           CHEQUE_BANK_AC = a.CHEQUE_BANK_AC,
                           CHEQUE_DATE = a.CHEQUE_DATE,
                           TRANSFER_AMT = a.TRANSFER_AMT,
                           TRANSFER_BANK_BRANCH = a.TRANSFER_BANK_BRANCH,
                           TRANSFER_BANK_AC = a.TRANSFER_BANK_AC,
                           TRANSFER_DATE = a.TRANSFER_DATE,
                           FINANCAL_AMT = a.FINACIAL_AC,
                           FINACIAL_AC = a.FINACIAL_AC,
                           DISCOUNT_FLAT = a.DISCOUNT_PERCENT,
                           DISCOUNT_PERCENT = a.DISCOUNT_PERCENT,
                           TOTAL_PAYMENT_MODES = a.TOTAL_PAYMENT_MODES,
                           CURRENT_PAYMENT = a.CURRENT_PAYMENT,
                           IS_PRINT_CHECK = a.IS_PRINT_CHECK,
                           NOTE = a.NOTE,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage DeleteSuppPayment(int id)
        {
            var str = (from a in db.TBL_SUPP_PAYMENT where a.SUPP_PAYMENT == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpPost]
        public HttpResponseMessage CreateSuppPayment(SuppPaymentModel _SuppPaymentModel)
        {
            TBL_SUPP_PAYMENT SuppPay = new TBL_SUPP_PAYMENT(); ;
            SuppPay.BUSINESS_LOCATION = _SuppPaymentModel.BUSINESS_LOCATION;
            SuppPay.SUPP_PAYMENT = _SuppPaymentModel.SUPP_PAYMENT;
            SuppPay.SuppPayNo = _SuppPaymentModel.SUPP_pay_no;
            SuppPay.SUPP_ID = _SuppPaymentModel.SUPP_ID;
            SuppPay.SUPP = _SuppPaymentModel.SUPP;
            SuppPay.SUPP_EMAIL = _SuppPaymentModel.SUPP_EMAIL;
            SuppPay.SUPP_SMS = _SuppPaymentModel.SUPP_SMS;
            SuppPay.IS_SEND_EMAIL = _SuppPaymentModel.IS_SEND_EMAIL;
            SuppPay.IS_SEND_SMS = _SuppPaymentModel.IS_SEND_SMS;
            SuppPay.CURRENT_ADV_AMT = _SuppPaymentModel.CURRENT_ADV_AMT;
            SuppPay.TOTAL_RIE_AMT = _SuppPaymentModel.TOTAL_RIE_AMT;
            SuppPay.PENDING_AMT = _SuppPaymentModel.PENDING_AMT;
            SuppPay.PAYMENT_NUMBER = _SuppPaymentModel.PAYMENT_NUMBER;
            SuppPay.PAYMENT_DATE = System.DateTime.Now;
            SuppPay.CURRENT_PAYABLE_AMT = _SuppPaymentModel.CURRENT_PAYABLE_AMT;
            SuppPay.TOTAL_PANDING = _SuppPaymentModel.TOTAL_PANDING;
            SuppPay.SELECTED_AMT = _SuppPaymentModel.SELECTED_AMT;
            SuppPay.PENDING_AFTE_PAYMENT = _SuppPaymentModel.PENDING_AFTE_PAYMENT;
            SuppPay.CASH_REG_ID = _SuppPaymentModel.CASH_REG_ID;
            SuppPay.CASH_REG = _SuppPaymentModel.CASH_REG;
            SuppPay.CASH_REG_AMT = _SuppPaymentModel.CASH_REG_AMT;
            SuppPay.CHEQUE_AMT = _SuppPaymentModel.CHEQUE_AMT;
            SuppPay.CHEQUE_NO = _SuppPaymentModel.CHEQUE_NO;
            SuppPay.CHEQUE_BANK_BRANCH = _SuppPaymentModel.CHEQUE_BANK_BRANCH;
            SuppPay.CHEQUE_BANK_AC = _SuppPaymentModel.CHEQUE_BANK_AC;
            //SuppPay.CHEQUE_DATE = _SuppPaymentModel.CHEQUE_DATE;
            SuppPay.CHEQUE_DATE = DateTime.Now;
            SuppPay.TRANSFER_AMT = _SuppPaymentModel.TRANSFER_AMT;
            SuppPay.TRANSFER_BANK_BRANCH = _SuppPaymentModel.TRANSFER_BANK_BRANCH;
            SuppPay.TRANSFER_BANK_AC = _SuppPaymentModel.TRANSFER_BANK_AC;
            //SuppPay.TRANSFER_DATE = _SuppPaymentModel.TRANSFER_DATE;
            SuppPay.TRANSFER_DATE = System.DateTime.Now;
            SuppPay.FINANCAL_AMT = _SuppPaymentModel.FINANCAL_AMT;
            SuppPay.FINACIAL_AC = _SuppPaymentModel.FINACIAL_AC;
            SuppPay.DISCOUNT_FLAT = _SuppPaymentModel.DISCOUNT_FLAT;
            SuppPay.DISCOUNT_PERCENT = _SuppPaymentModel.DISCOUNT_PERCENT;
            SuppPay.TOTAL_PAYMENT_MODES = _SuppPaymentModel.TOTAL_PAYMENT_MODES;
            SuppPay.CURRENT_PAYMENT = _SuppPaymentModel.CURRENT_PAYMENT;
            SuppPay.NOTE = _SuppPaymentModel.NOTE;
            SuppPay.IS_DELETE = false;
            SuppPay.IS_PRINT_CHECK = _SuppPaymentModel.IS_PRINT_CHECK;
            SuppPay.COMPANY_ID = _SuppPaymentModel.COMPANY_ID;
            SuppPay.USER_ID = _SuppPaymentModel.USER_ID;
            db.TBL_SUPP_PAYMENT.Add(SuppPay);
            db.SaveChanges();
            if (_SuppPaymentModel.getAllSupplier.Count > 0)
            {

                foreach (var item in _SuppPaymentModel.getAllSupplier)
                {
                    var str = (from a in db.tbl_supppaydetails where a.Supp_ID == _SuppPaymentModel.SUPP_ID && a.totalamount == item.TOTAL_AMT && a.pendingamoun == item.PENDING_AMT && a.adjustedamount == item.ROUND_OFF_ADJUSTMENTAMT select a).FirstOrDefault();
                    if (str != null)
                    {
                        
                        str.Supp_ID = _SuppPaymentModel.SUPP_ID;
                        str.adjustedamount = item.ROUND_OFF_ADJUSTMENTAMT;
                        str.pendingamoun = item.PENDING_AMT;
                        str.totalamount = item.TOTAL_AMT;
                       
                        db.SaveChanges();
                    }
                    else
                    {
                        tbl_supppaydetails suppdeatils = new tbl_supppaydetails();
                        suppdeatils.Supp_ID = _SuppPaymentModel.SUPP_ID;
                        suppdeatils.adjustedamount = item.ROUND_OFF_ADJUSTMENTAMT;
                        suppdeatils.pendingamoun = item.PENDING_AMT;
                        suppdeatils.totalamount = item.TOTAL_AMT;
                        db.tbl_supppaydetails.Add(suppdeatils);
                        db.SaveChanges();
                    }
                }


            }
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpPost]
        public HttpResponseMessage UpdateSuppPayment(SuppPaymentModel _SuppPaymentModel)
        {
            var SuppPay = (from a in db.TBL_SUPP_PAYMENT where a.SUPP_PAYMENT == _SuppPaymentModel.SUPP_PAYMENT select a).FirstOrDefault();
            SuppPay.BUSINESS_LOCATION = _SuppPaymentModel.BUSINESS_LOCATION;
            SuppPay.SUPP_ID = _SuppPaymentModel.SUPP_ID;
            SuppPay.SUPP = _SuppPaymentModel.SUPP;
            SuppPay.SUPP_EMAIL = _SuppPaymentModel.SUPP_EMAIL;
            SuppPay.SUPP_SMS = _SuppPaymentModel.SUPP_SMS;
            SuppPay.IS_SEND_EMAIL = _SuppPaymentModel.IS_SEND_EMAIL;
            SuppPay.IS_SEND_SMS = _SuppPaymentModel.IS_SEND_SMS;
            SuppPay.CURRENT_ADV_AMT = _SuppPaymentModel.CURRENT_ADV_AMT;
            SuppPay.TOTAL_RIE_AMT = _SuppPaymentModel.TOTAL_RIE_AMT;
            SuppPay.PENDING_AMT = _SuppPaymentModel.PENDING_AMT;
            SuppPay.PAYMENT_NUMBER = _SuppPaymentModel.PAYMENT_NUMBER;
            SuppPay.PAYMENT_DATE = System.DateTime.Now;
            SuppPay.CURRENT_PAYABLE_AMT = _SuppPaymentModel.CURRENT_PAYABLE_AMT;
            SuppPay.TOTAL_PANDING = _SuppPaymentModel.TOTAL_PANDING;
            SuppPay.SELECTED_AMT = _SuppPaymentModel.SELECTED_AMT;
            SuppPay.PENDING_AFTE_PAYMENT = _SuppPaymentModel.PENDING_AFTE_PAYMENT;
            SuppPay.CASH_REG_ID = _SuppPaymentModel.CASH_REG_ID;
            SuppPay.CASH_REG = _SuppPaymentModel.CASH_REG;
            SuppPay.CASH_REG_AMT = _SuppPaymentModel.CASH_REG_AMT;
            SuppPay.CHEQUE_AMT = _SuppPaymentModel.CHEQUE_AMT;
            SuppPay.CHEQUE_NO = _SuppPaymentModel.CHEQUE_NO;
            SuppPay.CHEQUE_BANK_BRANCH = _SuppPaymentModel.CHEQUE_BANK_BRANCH;
            SuppPay.CHEQUE_BANK_AC = _SuppPaymentModel.CHEQUE_BANK_AC;
            SuppPay.CHEQUE_DATE = _SuppPaymentModel.CHEQUE_DATE;
            SuppPay.TRANSFER_AMT = _SuppPaymentModel.TRANSFER_AMT;
            SuppPay.TRANSFER_BANK_BRANCH = _SuppPaymentModel.TRANSFER_BANK_BRANCH;
            SuppPay.TRANSFER_BANK_AC = _SuppPaymentModel.TRANSFER_BANK_AC;
            SuppPay.TRANSFER_DATE = _SuppPaymentModel.TRANSFER_DATE;
            SuppPay.FINANCAL_AMT = _SuppPaymentModel.FINANCAL_AMT;
            SuppPay.FINACIAL_AC = _SuppPaymentModel.FINACIAL_AC;
            SuppPay.DISCOUNT_FLAT = _SuppPaymentModel.DISCOUNT_FLAT;
            SuppPay.DISCOUNT_PERCENT = _SuppPaymentModel.DISCOUNT_PERCENT;
            SuppPay.TOTAL_PAYMENT_MODES = _SuppPaymentModel.TOTAL_PAYMENT_MODES;
            SuppPay.CURRENT_PAYMENT = _SuppPaymentModel.CURRENT_PAYMENT;
            SuppPay.NOTE = _SuppPaymentModel.NOTE;
            SuppPay.IS_PRINT_CHECK = _SuppPaymentModel.IS_PRINT_CHECK;
            SuppPay.IS_DELETE = false;
            SuppPay.COMPANY_ID = _SuppPaymentModel.COMPANY_ID;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpGet]
        public HttpResponseMessage GetSuppPayno()
        {

            string value = Convert.ToString(db.TBL_SUPP_PAYMENT
                            .OrderByDescending(p => p.SuppPayNo)
                            .Select(r => r.SuppPayNo)
                            .First());
            var RefNumber = new
            {
                SuppRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
    }
}
