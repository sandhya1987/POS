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
    public class LogInAPIController : ApiController
    {
        NEW_POSEntities db = new NEW_POSEntities();

        public HttpResponseMessage GetUser(string id, string password)
        {
            try
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
            catch (Exception ex)
            {

                throw;
            }
            

        }



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
