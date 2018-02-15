using InvoicePOSAPI.Models;
using InvoicePOSDATA;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvoicePOSAPI.Controllers
{
    public class InvoiceAPIController : ApiController
    {
        InvoiceModel Invoice = new InvoiceModel();
        NEW_POSEntities db = new NEW_POSEntities();



        [HttpPost]
        public HttpResponseMessage CreateInvoiceItem(ObservableCollection<ItemModel> SelectedItem)
        {
            try
            {

            }
            catch (Exception ex)
            { }
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpPost]
        public HttpResponseMessage CreateInvoice(InvoiceModel _InvoiceModel)
        {
            try
            {
                decimal? Totdal_tax = 0;
                decimal? Discount = 0;
                foreach (var item in _InvoiceModel.SelectedItem)
                {
                    Discount = item.Discount;
                    var str = (from a in db.TBL_ITEMS
                               where a.ITEM_ID == item.ITEM_ID
                               select new
                               {
                                   SalePriceBEforeTax = a.SALES_PRICE_BEFOR_TAX,
                                   SalePrice = a.SALES_PRICE,
                               }).FirstOrDefault();
                    Totdal_tax = (str.SalePrice - str.SalePriceBEforeTax) + Totdal_tax;
                }
                //var query = (from r in db.tmp_invoice select r).ToList();
                //foreach (var item in query)
                //{
                //    db.tmp_invoice.Remove(item);
                //    db.SaveChanges();
                //}
                TBL_INVOICE_PAY Invoice = new TBL_INVOICE_PAY();
                Invoice.AVAILABLE_CREDIT_LIMIT = _InvoiceModel.AVAILABLE_CREDIT_LIMIT;
                Invoice.BEFORE_ROUNDOFF = _InvoiceModel.BEFORE_ROUNDOFF;
                Invoice.COMMISION_EXPENSE = _InvoiceModel.COMMISION_EXPENSE;
                Invoice.CUSTOMER = _InvoiceModel.CUSTOMER;
                Invoice.CUSTOMER_ID = _InvoiceModel.CUSTOMER_ID;
                Invoice.CUSTOMER_EMAIL = _InvoiceModel.CUSTOMER_EMAIL;
                Invoice.CUSTOMER_MOBILE_NO = _InvoiceModel.CUSTOMER_MOBILE_NO;
                Invoice.DISCOUNT_INCLUDED = Discount;
                Invoice.INVOICE_DATE = System.DateTime.Now;
                //Invoice.INVOICE_DATE =Convert.ToDateTime(_InvoiceModel.FINAL_INVOICE_DATE);
                Invoice.INVOICE_NO = _InvoiceModel.INVOICE_NO;
                Invoice.IS_GOODS_DELIVERED = _InvoiceModel.IS_GOODS_DELIVERED;
                Invoice.IS_PRINT = _InvoiceModel.IS_PRINT;
                Invoice.IS_SAVE_RETURNABLE_AMOUNT = _InvoiceModel.IS_SAVE_RETURNABLE_AMOUNT;
                Invoice.NOTE = _InvoiceModel.NOTE;
                Invoice.NUMBER_OF_ITEM = _InvoiceModel.NUMBER_OF_ITEM;
                Invoice.PENDING_AMOUNT = _InvoiceModel.PENDING_AMOUNT;
                Invoice.QUANTITY_TOTAL = _InvoiceModel.QUANTITY_TOTAL;
                Invoice.RECIVED_AMOUNT = _InvoiceModel.RECIVED_AMOUNT;
                Invoice.RETURNABLE_AMOUNT = _InvoiceModel.RETURNABLE_AMOUNT;
                Invoice.ROUNDOFF_AMOUNT = _InvoiceModel.ROUNDOFF_AMOUNT;
                Invoice.SALES_EXECUTIVE = _InvoiceModel.SALES_EXECUTIVE;
                Invoice.SALES_EXECUTIVE_ID = _InvoiceModel.SALES_EXECUTIVE_ID;
                Invoice.TAX_INCLUDED = _InvoiceModel.TAX_INCLUDED;
                Invoice.TOTAL_AMOUNT = _InvoiceModel.TOTAL_AMOUNT;
                Invoice.TOTAL_TAX = Totdal_tax;
                Invoice.STATUS = "Paid";
                //BusinessLocation = _InvoiceModel.BUSS_LOC;
                db.TBL_INVOICE_PAY.Add(Invoice);
                db.SaveChanges();

                long invoiceid = Invoice.INVOICE_ID;
                if (_InvoiceModel.SelectedItem.Count > 0)
                {
                    foreach (var item in _InvoiceModel.SelectedItem)
                    {
                        var str = (from a in db.TBL_ITEMS
                                   where a.ITEM_ID == item.ITEM_ID
                                   select new
                                   {
                                       SalePriceBEforeTax = a.SALES_PRICE_BEFOR_TAX,
                                       SalePrice = a.SALES_PRICE,
                                   }).FirstOrDefault();
                        TBL_SALE_ITEM SaItem = new TBL_SALE_ITEM();
                        SaItem.SALE_ITEM_ID = item.ITEM_ID;
                        SaItem.INVOICE_ID = Convert.ToInt32(invoiceid);
                        SaItem.ITEM_DISCOUNT = item.Discount;
                        SaItem.ITEM_TOTAL_AMOUNT = item.Total;
                        SaItem.SALE_QTY = item.Current_Qty;
                        SaItem.STATUS = true;
                        SaItem.ITEM_TAX = str.SalePrice - str.SalePriceBEforeTax;
                        db.TBL_SALE_ITEM.Add(SaItem);
                        db.SaveChanges();


                        //tmp_invoice tem = new tmp_invoice();
                        //tem.Date = item.DATE;
                        //tem.Inv_No = _InvoiceModel.INVOICE_NO;
                        //tem.Items = item.ITEM_NAME;
                        //tem.Price = item.SALES_PRICE;
                        //tem.quantity = item.Current_Qty;
                        //tem.Taxable_Amt = item.SALES_PRICE_BEFOR_TAX;
                        //tem.Total_Amt = item.SALES_PRICE + item.SALES_PRICE_BEFOR_TAX;
                        //db.tmp_invoice.Add(tem);
                        //db.SaveChanges();



                        var str1 = (from a in db.TBL_OPENING_STOCK where a.ITEM_ID == item.ITEM_ID select a).FirstOrDefault();
                        int Qty = Convert.ToInt32(str1.OPN_QNT);
                        str1.OPN_QNT = Qty - item.Current_Qty;
                        db.SaveChanges();

                        var str2 = (from a in db.TBL_ITEMS where a.ITEM_ID == item.ITEM_ID select a).FirstOrDefault();
                        int Qty1 = Convert.ToInt32(str2.OPN_QNT);
                        str2.OPN_QNT = Qty1 - item.Current_Qty;
                        db.SaveChanges();
                    }
                }

                if (_InvoiceModel.CASH_AMOUNT != null && _InvoiceModel.CASH_AMOUNT != 0)
                {
                    TBL_CASH_REG cash = new TBL_CASH_REG();
                    cash.CASH_AMOUNT = _InvoiceModel.CASH_AMOUNT;
                    cash.CASH_REG_DATE = System.DateTime.Now;
                    cash.INVOICE_ID = invoiceid;
                    cash.CASH_REG = _InvoiceModel.CASH_REG;
                    db.TBL_CASH_REG.Add(cash);
                    db.SaveChanges();
                }

                if (_InvoiceModel.CHEQUE_AMOUNT != null && _InvoiceModel.CHEQUE_AMOUNT != 0)
                {
                    TBL_CHEQUE cheque = new TBL_CHEQUE();
                    cheque.CHAEQE_BANK_AC_ID = 1;
                    cheque.CHEQUE_AMOUNT = _InvoiceModel.CHEQUE_AMOUNT;
                    cheque.CHEQUE_BANK_AC = _InvoiceModel.CHEQUE_BANK_AC;
                    cheque.CHEQUE_BRANCH = _InvoiceModel.CHEQUE_BANK_BRANCH;
                    cheque.CHEQUE_DATE = System.DateTime.Now;
                    cheque.CHEQUE_NO = _InvoiceModel.CHEQUE_NO;
                    cheque.INVOICE_ID = invoiceid;
                    db.TBL_CHEQUE.Add(cheque);
                    db.SaveChanges();
                }

                if (_InvoiceModel.FINANCIAL_AMOUNT != null && _InvoiceModel.FINANCIAL_AMOUNT != 0)
                {
                    TBL_FINANCIAL fin = new TBL_FINANCIAL();
                    fin.FINANCIAL_AMOUNT = _InvoiceModel.FINANCIAL_AMOUNT;
                    fin.FINANCIAL_CREDIT_AC = _InvoiceModel.FINANCIAL_AC_NO;
                    fin.FINANCIAL_CREDIT_ID = 1;
                    fin.INVOICE_ID = invoiceid;
                    db.TBL_FINANCIAL.Add(fin);
                    db.SaveChanges();
                }
                if (_InvoiceModel.TRANSFER_AMOUNT != null && _InvoiceModel.TRANSFER_AMOUNT != 0)
                {
                    TBL_TRANSFER trn = new TBL_TRANSFER();
                    trn.INVOICE_ID = invoiceid;
                    trn.TRANSFER_AMOUNT = _InvoiceModel.TRANSFER_AMOUNT;
                    trn.TRANSFER_BANK_AC = _InvoiceModel.TRANSFER_BANK_AC;
                    trn.TRANSFER_BANK_ID = 1;
                    trn.TRANSFER_BRANCH = _InvoiceModel.TRANSFER_BRANCH;
                    //trn.TRANSFER_DATE = Convert.ToDateTime(_InvoiceModel.TRANSFER_DATE);
                    trn.TRANSFER_DATE = System.DateTime.Now;
                    db.TBL_TRANSFER.Add(trn);
                    db.SaveChanges();
                }
                //TBL_SUPP_PAYMENT SuppPay = new TBL_SUPP_PAYMENT(); ;
                //SuppPay.BUSINESS_LOCATION = _InvoiceModel.BUSINESS_LOCATION;
                //SuppPay.SUPP_ID = _InvoiceModel.SUPP_ID;
                //SuppPay.SUPP = _InvoiceModel.SUPP;
                //SuppPay.SUPP_EMAIL = _InvoiceModel.SUPP_EMAIL;
                //SuppPay.SUPP_SMS = _InvoiceModel.SUPP_SMS;
                //SuppPay.IS_SEND_EMAIL = _InvoiceModel.IS_SEND_EMAIL;
                //SuppPay.IS_SEND_SMS = _InvoiceModel.IS_SEND_SMS;
                //SuppPay.CURRENT_ADV_AMT = _InvoiceModel.CURRENT_ADV_AMT;
                //SuppPay.TOTAL_RIE_AMT = _InvoiceModel.TOTAL_RIE_AMT;
                //SuppPay.PENDING_AMT = _InvoiceModel.PENDING_AMT;
                //SuppPay.PAYMENT_NUMBER = _InvoiceModel.PAYMENT_NUMBER;
                //SuppPay.PAYMENT_DATE = System.DateTime.Now;
                //SuppPay.CURRENT_PAYABLE_AMT = _InvoiceModel.CURRENT_PAYABLE_AMT;
                //SuppPay.TOTAL_PANDING = _InvoiceModel.TOTAL_PANDING;
                //SuppPay.SELECTED_AMT = _InvoiceModel.SELECTED_AMT;
                //SuppPay.PENDING_AFTE_PAYMENT = _InvoiceModel.PENDING_AFTE_PAYMENT;
                //SuppPay.CASH_REG_ID = _InvoiceModel.CASH_REG_ID;
                //SuppPay.CASH_REG = _InvoiceModel.CASH_REG;
                //SuppPay.CASH_REG_AMT = _InvoiceModel.CASH_REG_AMT;
                //SuppPay.CHEQUE_AMT = _InvoiceModel.CHEQUE_AMT;
                //SuppPay.CHEQUE_NO = _InvoiceModel.CHEQUE_NO;
                //SuppPay.CHEQUE_BANK_BRANCH = _InvoiceModel.CHEQUE_BANK_BRANCH;
                //SuppPay.CHEQUE_BANK_AC = _InvoiceModel.CHEQUE_BANK_AC;
                //SuppPay.CHEQUE_DATE = _InvoiceModel.CHEQUE_DATE;
                //SuppPay.TRANSFER_AMT = _InvoiceModel.TRANSFER_AMT;
                //SuppPay.TRANSFER_BANK_BRANCH = _InvoiceModel.TRANSFER_BANK_BRANCH;
                //SuppPay.TRANSFER_BANK_AC = _InvoiceModel.TRANSFER_BANK_AC;
                //SuppPay.TRANSFER_DATE = _InvoiceModel.TRANSFER_DATE;
                //SuppPay.FINANCAL_AMT = _InvoiceModel.FINANCAL_AMT;
                //SuppPay.FINACIAL_AC = _InvoiceModel.FINACIAL_AC;
                //SuppPay.DISCOUNT_FLAT = _InvoiceModel.DISCOUNT_FLAT;
                //SuppPay.DISCOUNT_PERCENT = _InvoiceModel.DISCOUNT_PERCENT;
                //SuppPay.TOTAL_PAYMENT_MODES = _InvoiceModel.TOTAL_PAYMENT_MODES;
                //SuppPay.CURRENT_PAYMENT = _InvoiceModel.CURRENT_PAYMENT;
                //SuppPay.NOTE = _InvoiceModel.NOTE;
                //SuppPay.IS_DELETE = false;
                //SuppPay.IS_PRINT_CHECK = _InvoiceModel.IS_PRINT_CHECK;
                //SuppPay.COMPANY_ID = _InvoiceModel.COMPANY_ID;
                //db.TBL_SUPP_PAYMENT.Add(SuppPay);
                //db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpGet]
        public HttpResponseMessage GetInvoiceNo()
        {

            string value = Convert.ToString(db.TBL_INVOICE_PAY
                            .OrderByDescending(p => p.INVOICE_NO)
                            .Select(r => r.INVOICE_NO)
                            .First());
            var InvoiceRefNumber = new
            {
                InvoiceRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
        [HttpGet]
        public HttpResponseMessage ChackInvoice(string InvoiceNo)
        {
            var str = (from a in db.TBL_INVOICE_PAY where a.INVOICE_NO == InvoiceNo select a).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetInvoiceByCustId(int id)
        {
            var str1 = db.VIEW_INVOICE;
            var str = (from a in db.VIEW_INVOICE
                       where a.CUSTOMER_ID == id
                       select new InvoiceModel
                       {
                           AVAILABLE_CREDIT_LIMIT = a.AVAILABLE_CREDIT_LIMIT,
                           BEFORE_ROUNDOFF = a.BEFORE_ROUNDOFF,
                           COMMISION_EXPENSE = a.COMMISION_EXPENSE,
                           CUSTOMER = a.CUSTOMER,
                           CUSTOMER_EMAIL = a.CUSTOMER_EMAIL,
                           CUSTOMER_MOBILE_NO = a.CUSTOMER_MOBILE_NO,
                           CUSTOMER_ID = a.CUSTOMER_ID,
                           DISCOUNT_FLAT = a.DISCOUNT_INCLUDED,
                           INVOICE_NO = a.INVOICE_NO,
                           NUMBER_OF_ITEM = a.NUMBER_OF_ITEM,
                           NOTE = a.NOTE,
                           TOTAL_AMOUNT = a.TOTAL_AMOUNT,
                           TAX_INCLUDED = a.TAX_INCLUDED,
                           INVOICE_ID = a.INVOICE_ID,


                           PENDING_AMOUNT = a.PENDING_AMOUNT,
                           QUANTITY_TOTAL = a.QUANTITY_TOTAL,
                           RECIVED_AMOUNT = a.RECIVED_AMOUNT,
                           RETURNABLE_AMOUNT = a.RETURNABLE_AMOUNT,
                           ROUNDOFF_AMOUNT = a.ROUNDOFF_AMOUNT,
                           SALES_EXECUTIVE = a.SALES_EXECUTIVE,
                           SALES_EXECUTIVE_ID = a.SALES_EXECUTIVE_ID,
                           INVOICE_DATE = a.INVOICE_DATE,
                           TOTAL_TAX = a.TOTAL_TAX,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetInvoiceNo1(string CompanyId)
        {
            string code = "";
            string value = Convert.ToString(db.TBL_AUTO_GENERATE_INVOICE_NO
                            .OrderByDescending(p => p.INVOICE_NO)
                            .Select(r => r.INVOICE_NO)
                            .First());

            var str = (from a in db.TBL_INVOICE_PAY where a.INVOICE_NO == value select a).FirstOrDefault();
            if (str != null)
            {
                string dd = Convert.ToString(value);
                string aaa = dd.Substring(4, 6);
                int xx = Convert.ToInt32(aaa);
                int noo = Convert.ToInt32(xx + 1);
                code = "INV-" + noo.ToString("D6");

                TBL_AUTO_GENERATE_INVOICE_NO _code = new TBL_AUTO_GENERATE_INVOICE_NO();
                _code.INVOICE_NO = code;
                _code.COMPANY_ID = Convert.ToInt64(CompanyId);
                _code.USER_ID = 0;
                db.TBL_AUTO_GENERATE_INVOICE_NO.Add(_code);
                db.SaveChanges();
            }
            else
            {
                string dd = Convert.ToString(value);
                string aaa = dd.Substring(4, 6);
                int xx = Convert.ToInt32(aaa);
                code = "INV-" + xx.ToString("D6");

            }
            return Request.CreateResponse(HttpStatusCode.OK, code);
        }
        //[HttpGet]
        //public HttpResponseMessage CheckInvoice(DateTime startdate, DateTime enddate)
        //{
        //    var str = (from a in db.TBL_INVOICE_PAY where a.start == InvoiceNo select a).ToList();
        //    return Request.CreateResponse(HttpStatusCode.OK, str);
        //}
        [HttpGet]
        public HttpResponseMessage GetInvoice(int id)
        {
            var str1 = db.VIEW_INVOICE;
            var str = (from a in db.VIEW_INVOICE
                       select new InvoiceModel
                       {
                           AVAILABLE_CREDIT_LIMIT = a.AVAILABLE_CREDIT_LIMIT,
                           BEFORE_ROUNDOFF = a.BEFORE_ROUNDOFF,
                           COMMISION_EXPENSE = a.COMMISION_EXPENSE,
                           CUSTOMER = a.CUSTOMER,
                           CUSTOMER_EMAIL = a.CUSTOMER_EMAIL,
                           CUSTOMER_MOBILE_NO = a.CUSTOMER_MOBILE_NO,
                           CUSTOMER_ID = a.CUSTOMER_ID,
                           DISCOUNT_FLAT = a.DISCOUNT_INCLUDED,
                           INVOICE_NO = a.INVOICE_NO,
                           NUMBER_OF_ITEM = a.NUMBER_OF_ITEM,
                           NOTE = a.NOTE,
                           TOTAL_AMOUNT = a.TOTAL_AMOUNT,
                           TAX_INCLUDED = a.TAX_INCLUDED,
                           INVOICE_ID = a.INVOICE_ID,


                           PENDING_AMOUNT = a.PENDING_AMOUNT,
                           QUANTITY_TOTAL = a.QUANTITY_TOTAL,
                           RECIVED_AMOUNT = a.RECIVED_AMOUNT,
                           RETURNABLE_AMOUNT = a.RETURNABLE_AMOUNT,
                           ROUNDOFF_AMOUNT = a.ROUNDOFF_AMOUNT,
                           SALES_EXECUTIVE = a.SALES_EXECUTIVE,
                           SALES_EXECUTIVE_ID = a.SALES_EXECUTIVE_ID,
                           INVOICE_DATE = a.INVOICE_DATE,
                           TOTAL_TAX = a.TOTAL_TAX,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage GetInvoice1(DateTime STARTDATE, DateTime ENDDATE)
        {
            //DateTime ToDate = Convert.ToDateTime(InvoiceModel.STARTDATE);

            var str1 = db.VIEW_INVOICE;
            var str = (from a in db.VIEW_INVOICE
                       where
                       (a.INVOICE_DATE >= STARTDATE && a.INVOICE_DATE <= ENDDATE)
                       select new InvoiceModel
                       {
                           AVAILABLE_CREDIT_LIMIT = a.AVAILABLE_CREDIT_LIMIT,
                           BEFORE_ROUNDOFF = a.BEFORE_ROUNDOFF,
                           COMMISION_EXPENSE = a.COMMISION_EXPENSE,
                           CUSTOMER = a.CUSTOMER,
                           CUSTOMER_EMAIL = a.CUSTOMER_EMAIL,
                           CUSTOMER_MOBILE_NO = a.CUSTOMER_MOBILE_NO,
                           CUSTOMER_ID = a.CUSTOMER_ID,
                           DISCOUNT_FLAT = a.DISCOUNT_INCLUDED,
                           INVOICE_NO = a.INVOICE_NO,
                           NUMBER_OF_ITEM = a.NUMBER_OF_ITEM,
                           NOTE = a.NOTE,
                           TOTAL_AMOUNT = a.TOTAL_AMOUNT,
                           TAX_INCLUDED = a.TAX_INCLUDED,
                           INVOICE_ID = a.INVOICE_ID,


                           PENDING_AMOUNT = a.PENDING_AMOUNT,
                           QUANTITY_TOTAL = a.QUANTITY_TOTAL,
                           RECIVED_AMOUNT = a.RECIVED_AMOUNT,
                           RETURNABLE_AMOUNT = a.RETURNABLE_AMOUNT,
                           ROUNDOFF_AMOUNT = a.ROUNDOFF_AMOUNT,
                           SALES_EXECUTIVE = a.SALES_EXECUTIVE,
                           SALES_EXECUTIVE_ID = a.SALES_EXECUTIVE_ID,
                           INVOICE_DATE = a.INVOICE_DATE,
                           TOTAL_TAX = a.TOTAL_TAX,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }


        [HttpGet]
        public HttpResponseMessage GetInvoiceById(int id)
        {
            var str1 = db.VIEW_INVOICE;
            var str = (from a in db.VIEW_INVOICE
                       where a.INVOICE_ID == id
                       select new InvoiceModel
                       {
                           AVAILABLE_CREDIT_LIMIT = a.AVAILABLE_CREDIT_LIMIT,
                           BEFORE_ROUNDOFF = a.BEFORE_ROUNDOFF,
                           COMMISION_EXPENSE = a.COMMISION_EXPENSE,
                           CUSTOMER = a.CUSTOMER,
                           CUSTOMER_EMAIL = a.CUSTOMER_EMAIL,
                           CUSTOMER_MOBILE_NO = a.CUSTOMER_MOBILE_NO,
                           CUSTOMER_ID = a.CUSTOMER_ID,
                           DISCOUNT_FLAT = a.DISCOUNT_INCLUDED,
                           INVOICE_NO = a.INVOICE_NO,
                           NUMBER_OF_ITEM = a.NUMBER_OF_ITEM,
                           NOTE = a.NOTE,
                           TOTAL_AMOUNT = a.TOTAL_AMOUNT,
                           TAX_INCLUDED = a.TAX_INCLUDED,
                           INVOICE_ID = a.INVOICE_ID,


                           PENDING_AMOUNT = a.PENDING_AMOUNT,
                           QUANTITY_TOTAL = a.QUANTITY_TOTAL,
                           RECIVED_AMOUNT = a.RECIVED_AMOUNT,
                           RETURNABLE_AMOUNT = a.RETURNABLE_AMOUNT,
                           ROUNDOFF_AMOUNT = a.ROUNDOFF_AMOUNT,
                           SALES_EXECUTIVE = a.SALES_EXECUTIVE,
                           SALES_EXECUTIVE_ID = a.SALES_EXECUTIVE_ID,
                           INVOICE_DATE = a.INVOICE_DATE,
                           TOTAL_TAX = a.TOTAL_TAX,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        //[HttpGet]
        //public HttpResponseMessage GetInvoiceById1(int id)
        //{
        //   // var str1 = db.VIEW_INVOICE;
        //    var str = (from a in db.TBL_INVOICE_PAY
        //               where a.INVOICE_ID == id
        //               select new InvoiceModel
        //               {
        //                   AVAILABLE_CREDIT_LIMIT = a.AVAILABLE_CREDIT_LIMIT,
        //                   BEFORE_ROUNDOFF = a.BEFORE_ROUNDOFF,
        //                   COMMISION_EXPENSE = a.COMMISION_EXPENSE,
        //                   CUSTOMER = a.CUSTOMER,
        //                   CUSTOMER_EMAIL = a.CUSTOMER_EMAIL,
        //                   CUSTOMER_MOBILE_NO = a.CUSTOMER_MOBILE_NO,
        //                   CUSTOMER_ID = a.CUSTOMER_ID,
        //                   DISCOUNT_FLAT = a.DISCOUNT_INCLUDED,
        //                   INVOICE_NO = a.INVOICE_NO,
        //                   NUMBER_OF_ITEM = a.NUMBER_OF_ITEM,
        //                   NOTE = a.NOTE,
        //                   TOTAL_AMOUNT = a.TOTAL_AMOUNT,
        //                   TAX_INCLUDED = a.TAX_INCLUDED,
        //                   INVOICE_ID = a.INVOICE_ID,


        //                   PENDING_AMOUNT = a.PENDING_AMOUNT,
        //                   QUANTITY_TOTAL = a.QUANTITY_TOTAL,
        //                   RECIVED_AMOUNT = a.RECIVED_AMOUNT,
        //                   RETURNABLE_AMOUNT = a.RETURNABLE_AMOUNT,
        //                   ROUNDOFF_AMOUNT = a.ROUNDOFF_AMOUNT,
        //                   SALES_EXECUTIVE = a.SALES_EXECUTIVE,
        //                   SALES_EXECUTIVE_ID = a.SALES_EXECUTIVE_ID,
        //                   INVOICE_DATE = a.INVOICE_DATE,
        //                   TOTAL_TAX = a.TOTAL_TAX,

        //               }).ToList();
        //    return Request.CreateResponse(HttpStatusCode.OK, str);
        //}

        [HttpGet]
        public HttpResponseMessage GetInvoiceItem(int id)
        {
            var str = (from a in db.TBL_SALE_ITEM
                       join b in db.TBL_ITEMS on a.SALE_ITEM_ID equals b.ITEM_ID
                       where a.INVOICE_ID == id
                       select new ItemModel
                       {

                           IMAGE_PATH = b.IMAGE_PATH,
                           ITEM_ID = b.ITEM_ID,
                           ITEM_NAME = b.ITEM_NAME,
                           ITEM_DESCRIPTION = b.ITEM_DESCRIPTION,
                           ITEM_PRICE = b.ITEM_PRICE,
                           ITEM_INVOICE_ID = b.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = b.ITEM_PRODUCT_ID,
                           KEYWORD = b.KEYWORD,
                           ACCESSORIES_KEYWORD = b.ACCESSORIES_KEYWORD,
                           BARCODE = b.BARCODE,
                           CATAGORY_ID = b.CATAGORY_ID,
                           SEARCH_CODE = b.SERCH_CODE,
                           TAX_PAID = b.TAX_PAID,
                           TAX_COLLECTED = a.ITEM_TAX,

                           PURCHASE_UNIT = b.PURCHASE_UNIT,
                           SALES_UNIT = b.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = b.PURCHASE_UNIT_PRICE,
                           SALES_PRICE = b.SALES_PRICE,
                           MRP = b.MRP,
                           COMPANY_ID = b.COMPANY_ID,
                           CATEGORY_NAME = b.CATEGORY_NAME,
                           OPN_QNT = b.OPN_QNT,
                           DISPLAY_INDEX = b.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = b.ITEM_GROUP_NAME,
                           ITEM_UNIQUE_NAME = b.ITEM_UNIQUE_NAME,
                           REGIONAL_LANGUAGE = b.REGIONAL_LANGUAGE,
                           SALES_PRICE_BEFOR_TAX = b.SALES_PRICE_BEFOR_TAX,
                           IS_DELETE = b.IS_DELETE,
                           INCLUDE_TAX = b.INCLUDE_TAX,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = b.IS_ACIVE,
                           IS_Gift_Card = false,
                           MODIFICATION_DATE = b.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = b.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = b.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = b.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = b.WEIGHT_OF_PAPER,
                           GODOWN = "",
                           DATE = System.DateTime.Now,
                           COMPANY_NAME = "Infosolz",


                           BUSINESS_LOC = "Infosolz",
                           BUSS_LOC_ID = b.BUSS_LOC_ID,
                           GODOWN_ID = b.GODOWN_ID,
                           UNIT_SALES_ID = b.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = b.PURCHAGE_UNIT_ID,
                           Current_Qty = a.SALE_QTY,

                           TAX_COLLECTED_NAME = b.TAX_COLLECTED_NAME,
                           TAX_PAID_NAME = b.TAX_PAID_NAME,
                           Total = 0,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage GetInvoiceItem1(int id)
        {

            var str = (from a in db.TBL_SALE_ITEM
                       join b in db.TBL_ITEMS on a.SALE_ITEM_ID equals b.ITEM_ID
                       where a.INVOICE_ID == id && a.STATUS == true
                       select new ItemModel
                       {

                           IMAGE_PATH = b.IMAGE_PATH,
                           ITEM_ID = b.ITEM_ID,
                           ITEM_NAME = b.ITEM_NAME,
                           ITEM_DESCRIPTION = b.ITEM_DESCRIPTION,
                           ITEM_PRICE = b.ITEM_PRICE,
                           ITEM_INVOICE_ID = b.ITEM_INVOICE_ID,
                           ITEM_PRODUCT_ID = b.ITEM_PRODUCT_ID,
                           KEYWORD = b.KEYWORD,
                           ACCESSORIES_KEYWORD = b.ACCESSORIES_KEYWORD,
                           BARCODE = b.BARCODE,
                           CATAGORY_ID = b.CATAGORY_ID,
                           SEARCH_CODE = b.SERCH_CODE,
                           TAX_PAID = b.TAX_PAID,
                           TAX_COLLECTED = a.ITEM_TAX,

                           PURCHASE_UNIT = b.PURCHASE_UNIT,
                           SALES_UNIT = b.SALES_UNIT,
                           PURCHASE_UNIT_PRICE = b.PURCHASE_UNIT_PRICE,
                           SALES_PRICE = b.SALES_PRICE,
                           MRP = b.MRP,
                           COMPANY_ID = b.COMPANY_ID,
                           CATEGORY_NAME = b.CATEGORY_NAME,
                           OPN_QNT = b.OPN_QNT,
                           DISPLAY_INDEX = b.DISPLAY_INDEX,
                           ITEM_GROUP_NAME = b.ITEM_GROUP_NAME,
                           ITEM_UNIQUE_NAME = b.ITEM_UNIQUE_NAME,
                           REGIONAL_LANGUAGE = b.REGIONAL_LANGUAGE,
                           SALES_PRICE_BEFOR_TAX = b.SALES_PRICE_BEFOR_TAX,
                           IS_DELETE = b.IS_DELETE,
                           INCLUDE_TAX = b.INCLUDE_TAX,
                           IS_Shortable_Item = false,
                           IS_Purchased = false,
                           IS_Service_Item = false,
                           IS_For_Online_Shop = false,
                           IS_Not_for_online_shop = false,
                           IS_Not_For_Sell = false,
                           ALLOW_PURCHASE_ON_ESHOP = false,
                           IS_ACTIVE = b.IS_ACIVE,
                           IS_Gift_Card = false,
                           MODIFICATION_DATE = b.MODIFICATION_DATE,
                           WEIGHT_OF_CARDBOARD = b.WEIGHT_OF_CARDBOARD,
                           WEIGHT_OF_FOAM = b.WEIGHT_OF_FOAM,
                           WEIGHT_OF_PLASTIC = b.WEIGHT_OF_PLASTIC,
                           WEIGHT_OF_PAPER = b.WEIGHT_OF_PAPER,
                           GODOWN = "",
                           DATE = System.DateTime.Now,
                           COMPANY_NAME = "Infosolz",


                           BUSINESS_LOC = "Infosolz",
                           BUSS_LOC_ID = b.BUSS_LOC_ID,
                           GODOWN_ID = b.GODOWN_ID,
                           UNIT_SALES_ID = b.SALE_UNIT_ID,
                           UNIT_PURCHAGE_ID = b.PURCHAGE_UNIT_ID,
                           Current_Qty = a.SALE_QTY,

                           TAX_COLLECTED_NAME = b.TAX_COLLECTED_NAME,
                           TAX_PAID_NAME = b.TAX_PAID_NAME,
                           INVOICE_ID = a.INVOICE_ID,
                           SALE_ID = a.SALE_ID,
                           Total = 0,
                           //}
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage GetTotal(int id)
        {
            var total = 0;
            var str = (from a in db.TBL_INVOICE_PAY

                       where a.CUSTOMER_ID == id
                       select new InvoiceModel
                       {
                           TOTAL_AMOUNT = a.TOTAL_AMOUNT,

                           //}
                       }).ToList();
            foreach (var str1 in str)
            {
                total = total + Convert.ToInt32(str1.TOTAL_AMOUNT);
            }
            return Request.CreateResponse(HttpStatusCode.OK, total);
        }
        [HttpPost]
        public HttpResponseMessage CreateEstimate(ObservableCollection<ItemModel> _ListGrid_Temp)
        {
           if (_ListGrid_Temp.Count > 0)
            {
                int eNo = 0;
                var sat2 = (from a in db.TBL_ESTIMATE1 select a).ToList();
                if (sat2.Count > 0)
                {
                    var str1 = sat2.OrderByDescending(e => e.ESTIMATE_NO).FirstOrDefault();
                    eNo = Convert.ToInt16(str1.ESTIMATE_NO) + 1;
                }
                else
                {
                    eNo = 1;
                }
                foreach (var item in _ListGrid_Temp)
                {
                    TBL_ESTIMATE1 est = new TBL_ESTIMATE1();
                    est.BARCODE = item.BARCODE;
                    est.CURRENT_QTY = item.Current_Qty;
                    est.DISCOUNT = item.Discount;
                    est.ESTIMATE_NO = Convert.ToString(eNo);
                    est.ITEM_ID = item.ITEM_ID;
                    est.ITEM_NAME = item.ITEM_NAME;
                    est.MEASURING_UNIT = item.SALES_UNIT;
                    est.TAX_NAME = item.TaxName;
                    est.TAX_PRICE = item.TaxValue;
                    est.TOTAL = item.Total;
                    est.TOTAL_WITH_TAX = item.SALES_PRICE;
                    est.ESTIMATE_DATE = System.DateTime.Now;
                    db.TBL_ESTIMATE1.Add(est);
                    db.SaveChanges();
                }

            }
            return Request.CreateResponse(HttpStatusCode.OK, "success");
            }
        [HttpGet]
        public HttpResponseMessage HoldInvoice(int id)
        {

            var data = db.TBL_ITEMS.Where(m => m.ITEM_ID == id).ToList();
            
                decimal? Totdal_tax = 0;
                decimal? Discount = 0;
                foreach (var item in data)
                {
                    //Discount = item.Discount;
                    var str = (from a in db.TBL_ITEMS
                               where a.ITEM_ID == item.ITEM_ID
                               select new
                               {
                                   SalePriceBEforeTax = a.SALES_PRICE_BEFOR_TAX,
                                   SalePrice = a.SALES_PRICE,
                               }).FirstOrDefault();
                    Totdal_tax = (str.SalePrice - str.SalePriceBEforeTax) + Totdal_tax;
                }
           
                TBL_HOLD_INVOICE Invoice = new TBL_HOLD_INVOICE();
                Invoice.AVAILABLE_CREDIT_LIMIT = 0;
                //Invoice.BEFORE_ROUNDOFF = Convert.ToDecimal(data[0].ITEM_PRICE.Value);
                Invoice.COMMISION_EXPENSE = 0;
                Invoice.CUSTOMER = "";
                //Invoice.CUSTOMER_ID = 0;
                Invoice.CUSTOMER_EMAIL = "";
                Invoice.CUSTOMER_MOBILE_NO = "";
                //Invoice.DISCOUNT_INCLUDED = Discount;
                Invoice.INVOICE_DATE = System.DateTime.Now;
                //Invoice.INVOICE_DATE =Convert.ToDateTime(_InvoiceModel.FINAL_INVOICE_DATE);
                Invoice.INVOICE_NO = data[0].ITEM_INVOICE_ID.Value.ToString();
                Invoice.IS_GOODS_DELIVERED = false;
                Invoice.IS_PRINT = false;
                Invoice.ITEM_ID = data[0].ITEM_ID;
                //Invoice.IS_SAVE_RETURNABLE_AMOUNT = _InvoiceModel.IS_SAVE_RETURNABLE_AMOUNT;
                //Invoice.NOTE = _InvoiceModel.NOTE;
                //Invoice.NUMBER_OF_ITEM = data[0].i;
                //Invoice.PENDING_AMOUNT = _InvoiceModel.PENDING_AMOUNT;
                //Invoice.QUANTITY_TOTAL = _InvoiceModel.QUANTITY_TOTAL;
                //Invoice.RECIVED_AMOUNT = _InvoiceModel.RECIVED_AMOUNT;
                //Invoice.RETURNABLE_AMOUNT = _InvoiceModel.RETURNABLE_AMOUNT;
                //Invoice.ROUNDOFF_AMOUNT = _InvoiceModel.ROUNDOFF_AMOUNT;
                //Invoice.SALES_EXECUTIVE = _InvoiceModel.SALES_EXECUTIVE;
                //Invoice.SALES_EXECUTIVE_ID = _InvoiceModel.SALES_EXECUTIVE_ID;
                //Invoice.TAX_INCLUDED = _InvoiceModel.TAX_INCLUDED;
                Invoice.TOTAL_AMOUNT = Convert.ToInt32(data[0].ITEM_PRICE.Value);
                Invoice.TOTAL_TAX = data[0].TAX_PAID;
                Invoice.STATUS = "Cancelled";
                //BusinessLocation = _InvoiceModel.BUSS_LOC;
                db.TBL_HOLD_INVOICE.Add(Invoice);
                db.SaveChanges();


                //TBL_HOLD_INVOICE est = new TBL_HOLD_INVOICE();
                //est.INVOICE_NO = item.ITEM_INVOICE_ID.Value.ToString();
                //est.INVOICE_DATE = item.in;
                //est.TOTAL_AMOUNT = Convert.ToInt32(item.ITEM_PRICE.Value);
                //est.BUSSINESS_LOCATION_NAME = item.b
                //est.BARCODE = item.BARCODE;
                //est.CURRENT_QTY = item.Current_Qty;
                //est.DISCOUNT = item.Discount;
                //est.ESTIMATE_NO = Convert.ToString(eNo);
                //est.ITEM_ID = item.ITEM_ID;
                //est.ITEM_NAME = item.ITEM_NAME;
                //est.MEASURING_UNIT = item.SALES_UNIT;
                //est.TAX_NAME = item.TaxName;
                //est.TAX_PRICE = item.TaxValue;
                //est.TOTAL = item.Total;
                //est.TOTAL_WITH_TAX = item.SALES_PRICE;
                //est.ESTIMATE_DATE = System.DateTime.Now;
                //db.TBL_HOLD_INVOICE.Add(est);
                //db.SaveChanges();
            
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpGet]
        public HttpResponseMessage PickInvoice(int id)
        {

            var data = db.TBL_ITEMS.Where(m => m.ITEM_ID == id).ToList();

            decimal? Totdal_tax = 0;
            decimal? Discount = 0;
            foreach (var item in data)
            {
                //Discount = item.Discount;
                var str = (from a in db.TBL_ITEMS
                           where a.ITEM_ID == item.ITEM_ID
                           select new
                           {
                               SalePriceBEforeTax = a.SALES_PRICE_BEFOR_TAX,
                               SalePrice = a.SALES_PRICE,
                           }).FirstOrDefault();
                Totdal_tax = (str.SalePrice - str.SalePriceBEforeTax) + Totdal_tax;
            }
            var d = db.TBL_HOLD_INVOICE.Where(m => m.ITEM_ID == id && m.STATUS == "Cancelled").FirstOrDefault();
            if (d != null)
            {
                db.TBL_HOLD_INVOICE.Remove(d);
                db.SaveChanges();
                //TBL_HOLD_INVOICE Invoice = new TBL_HOLD_INVOICE();
                //Invoice.AVAILABLE_CREDIT_LIMIT = 0;
                ////Invoice.BEFORE_ROUNDOFF = Convert.ToDecimal(d.BEFORE_ROUNDOFF);
                //Invoice.COMMISION_EXPENSE = 0;
                //Invoice.CUSTOMER = "";
                ////Invoice.CUSTOMER_ID = 0;
                //Invoice.CUSTOMER_EMAIL = "";
                //Invoice.CUSTOMER_MOBILE_NO = "";
                ////Invoice.DISCOUNT_INCLUDED = Discount;
                //Invoice.INVOICE_DATE = System.DateTime.Now;
                ////Invoice.INVOICE_DATE =Convert.ToDateTime(_InvoiceModel.FINAL_INVOICE_DATE);
                //Invoice.INVOICE_NO = d.INVOICE_NO.ToString();
                //Invoice.IS_GOODS_DELIVERED = false;
                //Invoice.IS_PRINT = false;
                ////Invoice.IS_SAVE_RETURNABLE_AMOUNT = _InvoiceModel.IS_SAVE_RETURNABLE_AMOUNT;
                ////Invoice.NOTE = _InvoiceModel.NOTE;
                ////Invoice.NUMBER_OF_ITEM = data[0].i;
                ////Invoice.PENDING_AMOUNT = _InvoiceModel.PENDING_AMOUNT;
                ////Invoice.QUANTITY_TOTAL = _InvoiceModel.QUANTITY_TOTAL;
                ////Invoice.RECIVED_AMOUNT = _InvoiceModel.RECIVED_AMOUNT;
                ////Invoice.RETURNABLE_AMOUNT = _InvoiceModel.RETURNABLE_AMOUNT;
                ////Invoice.ROUNDOFF_AMOUNT = _InvoiceModel.ROUNDOFF_AMOUNT;
                ////Invoice.SALES_EXECUTIVE = _InvoiceModel.SALES_EXECUTIVE;
                ////Invoice.SALES_EXECUTIVE_ID = _InvoiceModel.SALES_EXECUTIVE_ID;
                ////Invoice.TAX_INCLUDED = _InvoiceModel.TAX_INCLUDED;
                //Invoice.TOTAL_AMOUNT = Convert.ToInt32(d.TOTAL_AMOUNT);
                //Invoice.TOTAL_TAX = d.TOTAL_TAX;
                //Invoice.STATUS = "Cancelled";
                //BusinessLocation = _InvoiceModel.BUSS_LOC;
                //db.TBL_INVOICE_PAY.Add(d);
                
            }

            //TBL_HOLD_INVOICE est = new TBL_HOLD_INVOICE();
            //est.INVOICE_NO = item.ITEM_INVOICE_ID.Value.ToString();
            //est.INVOICE_DATE = item.in;
            //est.TOTAL_AMOUNT = Convert.ToInt32(item.ITEM_PRICE.Value);
            //est.BUSSINESS_LOCATION_NAME = item.b
            //est.BARCODE = item.BARCODE;
            //est.CURRENT_QTY = item.Current_Qty;
            //est.DISCOUNT = item.Discount;
            //est.ESTIMATE_NO = Convert.ToString(eNo);
            //est.ITEM_ID = item.ITEM_ID;
            //est.ITEM_NAME = item.ITEM_NAME;
            //est.MEASURING_UNIT = item.SALES_UNIT;
            //est.TAX_NAME = item.TaxName;
            //est.TAX_PRICE = item.TaxValue;
            //est.TOTAL = item.Total;
            //est.TOTAL_WITH_TAX = item.SALES_PRICE;
            //est.ESTIMATE_DATE = System.DateTime.Now;
            //db.TBL_HOLD_INVOICE.Add(est);
            //db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
    [HttpGet]
        public HttpResponseMessage GetPoNo(int id)
        {

            //string value = Convert.ToString(db.TBL_SALES_RETURN
            //                .OrderByDescending(p => p.SALES_RETURN_NO)
            //                .Select(r => r.SALES_RETURN_NO)
            //                .First());
            //var RefNumber = new
            //{
            //    SuppRefNumber = value
            //};
            //return Request.CreateResponse(HttpStatusCode.OK, value);
            var value = db.TBL_INVOICE_PAY.Where(m => m.INVOICE_ID == id).ToList();
            TBL_HOLD_INVOICE invoice = new TBL_HOLD_INVOICE();
            invoice.INVOICE_NO = Convert.ToString(id);
            //invoice.CUSTOMER = value.CUSTOMER;
            db.TBL_HOLD_INVOICE.Add(invoice);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
        [HttpGet]
        public HttpResponseMessage GetHoldInvoice(int id)
        {
            var value = db.TBL_INVOICE_PAY.Where(m => m.INVOICE_ID == id).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
     
    }
}
