using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoicePOSAPI.Models;
using InvoicePOSDATA;
using InvoicePOSAPI.Helpers;


namespace InvoicePOSAPI.Controllers
{
    public class ReportAPIController : ApiController
    {
        NEW_POSEntities db = new NEW_POSEntities();



        [HttpGet]
        public HttpResponseMessage GetReports(int id)
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
                    var str = (from a in db.TBL_REPORT
                               where a.COMPANY_ID == id && a.IS_DELETE == false
                               select new ReportModel
                               {

                                   REPORT_ID = a.REPORT_ID,
                                   COMPANY_ID = a.COMPANY_ID,
                                   REPORT_NAME = a.REPORT_NAME,
                                   IS_DELETE = a.IS_DELETE,
                                   CREATION_DATE = a.CREATION_DATE,
                                   CREATED_BY = a.CREATED_BY,
                                   STATUS = a.STATUS,

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
