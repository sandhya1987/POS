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
    public class ManageStockAPIController : ApiController
    {
        NEW_POSEntities db = new NEW_POSEntities();
        ManageStockModel ms = new ManageStockModel();
        [HttpGet]
        public HttpResponseMessage GetManageStock(int id)
        {
            //var str = (from a in db.TBL_COMPANY
            //           where a.COMAPNY_ID == id
            //           select new ManageStockModel
            //           {
            //               BUSSINESS_LOCATION = a.COMPANY_NAME,
            //               COMPANY_ID = a.COMAPNY_ID,
            //               //_itemModel = (from b in db.TBL_ITEMS
            //               //              where b.COMPANY_ID == id
            //               //              select new ItemModel
            //               //              {
            //               //                  ITEM_NAME = b.ITEM_NAME,
            //               //                  BARCODE = b.BARCODE,
            //               //                  OPN_QNT = b.OPN_QNT,
            //               //                  SALES_UNIT = b.SALES_UNIT,
            //               //                  TAX_PAID = b.TAX_PAID,
            //               //                  SALES_PRICE = b.SALES_PRICE,
            //               //              }).asquerable()
            //               // DESCRIPTION=a.
            //           }).ToList();
        //    return Request.CreateResponse(HttpStatusCode.OK, str);
        //}


            var str = (from a in db.TBL_GODOWN
                       join b in db.TBL_COMPANY on a.COMPANY_ID equals b.COMAPNY_ID
                       where b.COMAPNY_ID == id
                       select new ManageStockModel
                       {
                           BUSSINESS_LOCATION = b.COMPANY_NAME,
                           COMPANY_ID = b.COMAPNY_ID,
                           GODOWN_NAME = a.GODOWN_NAME,
                           GODOWN_ID=a.GODOWN_ID,
                           IS_ACTIVE = a.IS_ACTIVE,
                           IS_DEFAULT_GODOWN = a.IS_DEFAULT_GODOWN,
                           DESCRIPTION = a.GOSOWN_DESCRIPTION,

                       }).ToList();


                        return Request.CreateResponse(HttpStatusCode.OK, str);

                       }
            




    }
}
