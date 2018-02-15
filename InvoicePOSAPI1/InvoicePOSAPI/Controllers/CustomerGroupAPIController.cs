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
    public class CustomerGroupAPIController : ApiController
    {
        CustomerGroupModel emp = new CustomerGroupModel();
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage CustomerGroupList(int id)
        {
            var str = (from a in db.TBL_CUSTOMER_GROUP
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new CustomerGroupModel
                       {
                           CUSTOMER_GROUP_ID=a.CUSTOMER_GROUP_ID,
                           NAME=a.NAME,
                           DESCRIPTION=a.DESCRIPTION,
                           COMPANY_ID=a.COMPANY_ID,
                           IS_DELETE=a.IS_DELETE,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }

        [HttpGet]
        public HttpResponseMessage CustomerGroupEdit(int id)
        {
            var str = (from a in db.TBL_CUSTOMER_GROUP
                       where a.CUSTOMER_GROUP_ID == id && a.IS_DELETE == false
                       select new CustomerGroupModel
                       {
                           CUSTOMER_GROUP_ID = a.CUSTOMER_GROUP_ID,
                           NAME = a.NAME,
                           DESCRIPTION = a.DESCRIPTION,
                           COMPANY_ID = a.COMPANY_ID,
                           IS_DELETE = a.IS_DELETE,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }


        [HttpPost]
        public HttpResponseMessage CustomerGroupAdd(CustomerGroupModel _CustomerGroupModel)
        {
            try
            {
                TBL_CUSTOMER_GROUP Customer = new TBL_CUSTOMER_GROUP();
                Customer.COMPANY_ID = _CustomerGroupModel.COMPANY_ID;
                Customer.DESCRIPTION = _CustomerGroupModel.DESCRIPTION;
                Customer.NAME = _CustomerGroupModel.NAME;
                Customer.IS_DELETE = false;
                db.TBL_CUSTOMER_GROUP.Add(Customer);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public HttpResponseMessage CustomerGroupUpdate(CustomerGroupModel _CustomerGroupModel)
        {
            var str = (from a in db.TBL_CUSTOMER_GROUP where a.COMPANY_ID == _CustomerGroupModel.COMPANY_ID select a).FirstOrDefault();
            str.COMPANY_ID = _CustomerGroupModel.COMPANY_ID;
            str.DESCRIPTION = _CustomerGroupModel.DESCRIPTION;
            str.NAME = _CustomerGroupModel.NAME;
            str.IS_DELETE = false;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
        [HttpGet]
        public HttpResponseMessage CustomerGroupDelete(int id)
        {
            var str = (from a in db.TBL_CUSTOMER_GROUP where a.CUSTOMER_GROUP_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
    }
}
