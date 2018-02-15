using InvoicePOSAPI.Models;
using InvoicePOSDATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.ObjectModel;

namespace InvoicePOSAPI.Controllers
{
    public class SalesReturnAPIController : ApiController
    {
        NEW_POSEntities db = new NEW_POSEntities();
        SalesReturnModel cm = new SalesReturnModel();
        [HttpPost]
        public HttpResponseMessage CreateSalesReturn(SalesReturnModel _SalesReturnModel)
        {
            try
            {


                TBL_SALES_RETURN re = new TBL_SALES_RETURN();
                re.BUSINESS_LOCATION = _SalesReturnModel.BUSINESS_LOCATION;
                re.SALES_RETURN_NO = _SalesReturnModel.SALES_RETURN_NO;
                re.BARCODE = _SalesReturnModel.BARCODE;
                re.ITEM_NAME = _SalesReturnModel.ITEM_NAME;
                re.IS_WITHOUT_INVOICE = _SalesReturnModel.IS_WITHOUT_INVOICE;
                re.RETURN_DATE = System.DateTime.Now;
                re.GODOWN = _SalesReturnModel.GODOWN;
                re.CUSTOMER_ID = _SalesReturnModel.CUSTOMER_ID;
                re.OUTSTANDING_BALANCE = _SalesReturnModel.OUTSTANDING_BALANCE;
                re.INVOICE_NO = _SalesReturnModel.INVOICE_NO;
                re.REFERENCE_NUMBER = _SalesReturnModel.REFERENCE_NUMBER;
                re.TAX_AMOUNT = _SalesReturnModel.TAX_AMOUNT;
                re.FREIGHT_CHARGE = _SalesReturnModel.FREIGHT_CHARGE;
                re.PACKING_CHARGE = _SalesReturnModel.PACKING_CHARGE;
                re.TOTAL_AMOUNT = _SalesReturnModel.TOTAL_AMOUNT;
                re.ROUND_OFF_AMOUNT = _SalesReturnModel.ROUND_OFF_AMOUNT;
                re.GRAND_TOTAL = _SalesReturnModel.GRAND_TOTAL;
                re.COMPANY_ID = _SalesReturnModel.COMPANY_ID;
                re.SUBSIDY_AMOUN = _SalesReturnModel.SUBSIDY_AMOUN;
                re.CUS_PENDING_AMOUNT = _SalesReturnModel.CUS_PENDING_AMOUNT;
                re.NOTES = _SalesReturnModel.NOTES;
                re.SEARCH_CODE = _SalesReturnModel.SEARCH_CODE;
                re.SALES_EXECUTIVE = _SalesReturnModel.SALES_EXECUTIVE;
                re.IS_DELETE = false;
                db.TBL_SALES_RETURN.Add(re);
                db.SaveChanges();



                var str = (from a in db.TBL_INVOICE_PAY where a.INVOICE_NO == re.INVOICE_NO select a).FirstOrDefault();
                if (str != null)
                {
                    str.STATUS = "Cancelled";
                    str.SALES_RETURN_AMOUNT = re.GRAND_TOTAL;
                    //str.QUANTITY_TOTAL = str.QUANTITY_TOTAL-_SalesReturnModel.TOTAL_QTY;
                    //str.NUMBER_OF_ITEM = str.NUMBER_OF_ITEM-_SalesReturnModel.TOTAL_ITEM;
                    db.SaveChanges();
                }


                var strcash = (from a in db.TBL_NEWCASHREGISTER where a.IS_MAIN_CASH == true select a).FirstOrDefault();
                if (strcash != null)
                {
                    strcash.CASH_AMOUNT = strcash.CASH_AMOUNT - re.GRAND_TOTAL;
                    db.SaveChanges();
                }

                if (_SalesReturnModel.SelectedItem.Count > 0)
                {
                    foreach (var item in _SalesReturnModel.SelectedItem)
                    {
                        
                        var obj = (from a in db.TBL_SALE_ITEM where a.INVOICE_ID == item.INVOICE_ID && a.SALE_ID == item.SALE_ID select a).FirstOrDefault();

                        if (obj != null)
                        {
                            obj.STATUS = false;
                           // db.SaveChanges();
                        }


                        var ob = (from a in db.TBL_ITEMS where a.BARCODE == item.BARCODE && a.ITEM_NAME== item.ITEM_NAME select a).FirstOrDefault();

                        if (ob != null)
                        {
                            ob.OPN_QNT = ob.OPN_QNT + item.Current_Qty;
                            //db.SaveChanges();
                        }

                        var st = (from a in db.TBL_OPENING_STOCK where a.BARCODE == item.BARCODE && a.ITEM_NAME == item.ITEM_NAME select a).FirstOrDefault();

                        if (st != null)
                        {
                            st.OPN_QNT = st.OPN_QNT + item.Current_Qty;
                            //db.SaveChanges();
                        }
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "success");

        }
        [HttpPost]
        public HttpResponseMessage UpdateSalesReturn(SalesReturnModel _SalesReturnModel)
        {
            try
            {


                var re = (from a in db.TBL_SALES_RETURN where a.SALES_RETURN_ID == _SalesReturnModel.SALES_RETURN_ID select a).FirstOrDefault();
                re.BUSINESS_LOCATION = _SalesReturnModel.BUSINESS_LOCATION;
                re.SALES_RETURN_NO = _SalesReturnModel.SALES_RETURN_NO;
                re.BARCODE = _SalesReturnModel.BARCODE;
                re.ITEM_NAME = _SalesReturnModel.ITEM_NAME;
                re.IS_WITHOUT_INVOICE = _SalesReturnModel.IS_WITHOUT_INVOICE;
                re.RETURN_DATE = System.DateTime.Now;
                re.GODOWN = _SalesReturnModel.GODOWN;
                re.CUSTOMER_ID = _SalesReturnModel.CUSTOMER_ID;
                re.OUTSTANDING_BALANCE = _SalesReturnModel.OUTSTANDING_BALANCE;
                re.INVOICE_NO = _SalesReturnModel.INVOICE_NO;
                re.REFERENCE_NUMBER = _SalesReturnModel.REFERENCE_NUMBER;
                re.TAX_AMOUNT = _SalesReturnModel.TAX_AMOUNT;
                re.FREIGHT_CHARGE = _SalesReturnModel.FREIGHT_CHARGE;
                re.PACKING_CHARGE = _SalesReturnModel.PACKING_CHARGE;
                re.TOTAL_AMOUNT = _SalesReturnModel.TOTAL_AMOUNT;
                re.ROUND_OFF_AMOUNT = _SalesReturnModel.ROUND_OFF_AMOUNT;
                re.GRAND_TOTAL = _SalesReturnModel.GRAND_TOTAL;
                re.COMPANY_ID = _SalesReturnModel.COMPANY_ID;
                re.SUBSIDY_AMOUN = _SalesReturnModel.SUBSIDY_AMOUN;
                re.CUS_PENDING_AMOUNT = _SalesReturnModel.CUS_PENDING_AMOUNT;
                re.NOTES = _SalesReturnModel.NOTES;
                re.SEARCH_CODE = _SalesReturnModel.SEARCH_CODE;
                re.SALES_EXECUTIVE = _SalesReturnModel.SALES_EXECUTIVE;
                re.IS_DELETE = false;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, re);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpGet]
        public HttpResponseMessage DeleteSalesreturn(int id)
        {
            var str = (from a in db.TBL_SALES_RETURN where a.SALES_RETURN_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");


        }
        [HttpGet]
        public HttpResponseMessage GetAllSalesReturn(int id)
        {
            var str = (from a in db.TBL_SALES_RETURN
                       where a.COMPANY_ID == id && a.IS_DELETE == false 
                       select new SalesReturnModel
                       {
                           SALES_RETURN_ID = a.SALES_RETURN_ID,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           SALES_RETURN_NO = a.SALES_RETURN_NO,
                           BARCODE = a.BARCODE,
                           ITEM_NAME = a.ITEM_NAME,
                           IS_WITHOUT_INVOICE = a.IS_WITHOUT_INVOICE,
                           RETURN_DATE = a.RETURN_DATE,
                           GODOWN = a.GODOWN,
                           CUSTOMER_ID = a.CUSTOMER_ID,
                           OUTSTANDING_BALANCE = a.OUTSTANDING_BALANCE,
                           INVOICE_NO = a.INVOICE_NO,
                           REFERENCE_NUMBER = a.REFERENCE_NUMBER,
                           TAX_AMOUNT = a.TAX_AMOUNT,
                           FREIGHT_CHARGE = a.FREIGHT_CHARGE,
                           PACKING_CHARGE = a.PACKING_CHARGE,
                           TOTAL_AMOUNT = a.TOTAL_AMOUNT,
                           ROUND_OFF_AMOUNT = a.ROUND_OFF_AMOUNT,
                           GRAND_TOTAL = a.GRAND_TOTAL,
                           SUBSIDY_AMOUN = a.SUBSIDY_AMOUN,
                           CUS_PENDING_AMOUNT = a.CUS_PENDING_AMOUNT,
                           NOTES = a.NOTES,
                           SALES_EXECUTIVE = a.SALES_EXECUTIVE,
                           SEARCH_CODE = a.SEARCH_CODE,
                           COMPANY_ID=a.COMPANY_ID,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage EditSalesReturn(int id)
        {
            var str = (from a in db.TBL_SALES_RETURN
                       where a.SALES_RETURN_ID == id && a.IS_DELETE == false
                       select new SalesReturnModel
                       {
                           SALES_RETURN_ID = a.SALES_RETURN_ID,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           SALES_RETURN_NO = a.SALES_RETURN_NO,
                           BARCODE = a.BARCODE,
                           ITEM_NAME = a.ITEM_NAME,
                           IS_WITHOUT_INVOICE = a.IS_WITHOUT_INVOICE,
                           RETURN_DATE = a.RETURN_DATE,
                           GODOWN = a.GODOWN,
                           CUSTOMER_ID = a.CUSTOMER_ID,
                           OUTSTANDING_BALANCE = a.OUTSTANDING_BALANCE,
                           INVOICE_NO = a.INVOICE_NO,
                           REFERENCE_NUMBER = a.REFERENCE_NUMBER,
                           TAX_AMOUNT = a.TAX_AMOUNT,
                           FREIGHT_CHARGE = a.FREIGHT_CHARGE,
                           PACKING_CHARGE = a.PACKING_CHARGE,
                           TOTAL_AMOUNT = a.TOTAL_AMOUNT,
                           ROUND_OFF_AMOUNT = a.ROUND_OFF_AMOUNT,
                           GRAND_TOTAL = a.GRAND_TOTAL,
                           SUBSIDY_AMOUN = a.SUBSIDY_AMOUN,
                           CUS_PENDING_AMOUNT = a.CUS_PENDING_AMOUNT,
                           NOTES = a.NOTES,
                           SALES_EXECUTIVE = a.SALES_EXECUTIVE,
                           SEARCH_CODE = a.SEARCH_CODE,
                           COMPANY_ID = a.COMPANY_ID,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetSalesReturnNo()
        {

            string value = Convert.ToString(db.TBL_SALES_RETURN
                            .OrderByDescending(p => p.SALES_RETURN_NO)
                            .Select(r => r.SALES_RETURN_NO)
                            .First());
            var RefNumber = new
            {
                SuppRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
        [HttpGet]
        public HttpResponseMessage GetSalesReturnManager(string id)
        {
            var val = (from a in db.TBL_INVOICE_PAY
                       join b in db.TBL_SALES_RETURN on a.INVOICE_NO equals b.INVOICE_NO
                       select new ItemModel
                       {
                           BUSINESS_LOC = b.BUSINESS_LOCATION,
                           RETURNABLE_AMOUNT = a.RETURNABLE_AMOUNT.Value,
                           DATE = b.RETURN_DATE,
                           Invoice_No = b.INVOICE_NO,
                           Current_Qty = a.QUANTITY_TOTAL.Value,
                           CUSTOMER_NAME = a.CUSTOMER,
                           CUSTOMER_MOBILE = a.CUSTOMER_MOBILE_NO,
                           
                       }).ToList();
           
            //var str = db.TBL_SALES_RETURN.Where(m => m.INVOICE_NO == id).FirstOrDefault();
            return Request.CreateResponse(HttpStatusCode.OK, val);
        }
    }
}
