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
    public class LogInAPIController : ApiController
    {
        //NEW_POS_DBEntities db = new NEW_POS_DBEntities();

        NEW_POSEntities db = new NEW_POSEntities();

        public HttpResponseMessage GetUser(string id, string password)
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
                    var str = (from a in db.TBL_USER
                               where a.ROLE == id && a.PASSWORD == password
                               select new LogInModel
                               {
                                   COMPANY_ID = a.COMPANY_ID,
                                   CREATED_DATE = a.CREATED_DATE,
                                   LOGIN_TIME = a.LOGIN_TIME,
                                   MAC_ADDRESS = a.MAC_ADDRESS,
                                   PASSWORD = a.PASSWORD,
                                   ROLE = a.ROLE,
                                   USER_ID = a.USER_ID,
                               }).FirstOrDefault();

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
        /*
        [HttpGet]
        public HttpResponseMessage Synchronize()
        {
            try
            {
                string client = "data source=localhost;initial catalog=NEW_POS_DB;user id=makrov;password=Qsefthuk786;Integrated Security=False";
                string server = "data source=34.209.147.13;initial catalog=NEW_POS_DB;user id=makrov;password=Qsefthuk786;Integrated Security=False";
                DBSynchronizer.Synchronize(server, client);
            }
            catch (Exception ex)
            {
                throw;
            }

            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }*/

        //public HttpResponseMessage GetUser(string id, string password)
        //{
        //    var str = (from a in db.MODULE_RIGHTS
        //               where a.ROLE == id && a.PASSWORD == password
        //               select new LogInModel
        //               {
        //                   COMPANY_ID = a.COMPANY_ID,
        //                   ACTION_CREATE = a.ACTION_CREATE,
        //                   EDIT = a.EDIT,
        //                   ACTION_DELETE = a.ACTION_DELETE,
        //                   ACTION_VIEW = a.ACTION_VIEW,
        //                   ROLE = a.ROLE,
        //                   USER_ID = a.USER_ID,
        //               }).FirstOrDefault();
        //    return Request.CreateResponse(HttpStatusCode.OK, str);

        //}
    }
}
