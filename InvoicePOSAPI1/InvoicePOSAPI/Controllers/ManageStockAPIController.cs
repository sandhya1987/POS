using InvoicePOSAPI.Models;
using InvoicePOSDATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoicePOSAPI.Helpers;

namespace InvoicePOSAPI.Controllers
{
    public class ManageStockAPIController : ApiController
    {
        //NEW_POS_DBEntities db = new NEW_POS_DBEntities();
        NEW_POSEntities db = new NEW_POSEntities();

        ManageStockModel ms = new ManageStockModel();


        [HttpGet]
        public HttpResponseMessage GetManageStock(int id)
        {
            try
            {
                bool conn = false;
                conn = db.Database.Exists();
                if (!conn)
                {
                    ConnectionTools.changeToLocalDB(db);
                    conn = db.Database.Exists();
                }

                if (conn)
                {
                    var str = (from a in db.TBL_GODOWN
                               join b in db.TBL_COMPANY on a.COMPANY_ID equals b.COMAPNY_ID
                               where b.COMAPNY_ID == id
                               select new ManageStockModel
                               {
                                   BUSSINESS_LOCATION = b.COMPANY_NAME,
                                   COMPANY_ID = b.COMAPNY_ID,
                                   GODOWN_NAME = a.GODOWN_NAME,
                                   GODOWN_ID = a.GODOWN_ID,
                                   IS_ACTIVE = a.IS_ACTIVE,
                                   IS_DEFAULT_GODOWN = a.IS_DEFAULT_GODOWN,
                                   DESCRIPTION = a.GOSOWN_DESCRIPTION,

                               }).ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, str);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ConnectionTools.ChangeToRemoteDB(db);
            }
        }

    }
}
