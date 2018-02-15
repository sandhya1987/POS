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
    public class DepartmentAPIController : ApiController
    {
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage GetDepartment(int id)
        {
            var str = (from a in db.TBL_DEPARTMENT
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new DepartmentModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           IS_DELETE = a.IS_DELETE,
                           CREATED_BY = a.CREATED_BY,
                           CREATED_DATE = System.DateTime.Now,
                            DEPARTMENT_ID = a.DEPARTMENT_ID,
                            DEPARTMENT_NAME = a.DEPARTMENT_NAME,
                           SORT_INDEX = a.SORT_INDEX,
                           STATUS = a.STATUS,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage DepartmentEdit(int id)
        {
            var str = (from a in db.TBL_DEPARTMENT
                       where a.DEPARTMENT_ID == id
                       select new DepartmentModel
                       {
                           COMPANY_ID = a.COMPANY_ID,
                           IS_DELETE = a.IS_DELETE,
                           CREATED_BY = a.CREATED_BY,
                           CREATED_DATE = System.DateTime.Now,
                           DEPARTMENT_ID = a.DEPARTMENT_ID,
                           DEPARTMENT_NAME = a.DEPARTMENT_NAME,
                           SORT_INDEX = a.SORT_INDEX,
                           STATUS = a.STATUS,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage DeleteDepartment(int id)
        {
            var str = (from a in db.TBL_DEPARTMENT where a.DEPARTMENT_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]
        public HttpResponseMessage DepartmentAdd(DepartmentModel _DepartmentModel)
        {
            if (_DepartmentModel.DEPARTMENT_ID == null || _DepartmentModel.DEPARTMENT_ID == 0)
            {
                TBL_DEPARTMENT gd = new TBL_DEPARTMENT();
                gd.COMPANY_ID = _DepartmentModel.COMPANY_ID;
                gd.DEPARTMENT_NAME = _DepartmentModel.DEPARTMENT_NAME;
                gd.SORT_INDEX = _DepartmentModel.SORT_INDEX;
                gd.IS_DELETE = false;
                gd.STATUS = _DepartmentModel.STATUS;
                gd.CREATED_BY = _DepartmentModel.CREATED_BY;
                gd.CREATED_DATE = System.DateTime.Now;
                db.TBL_DEPARTMENT.Add(gd);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]
        public HttpResponseMessage DepartmentUpdate(DepartmentModel _DepartmentModel)
        {
            if (_DepartmentModel.DEPARTMENT_ID != null || _DepartmentModel.DEPARTMENT_ID != 0)
            {
                var gd = (from a in db.TBL_DEPARTMENT where a.DEPARTMENT_ID == _DepartmentModel.DEPARTMENT_ID select a).FirstOrDefault();
                gd.COMPANY_ID = _DepartmentModel.COMPANY_ID;
                gd.DEPARTMENT_NAME = _DepartmentModel.DEPARTMENT_NAME;
                gd.SORT_INDEX = _DepartmentModel.SORT_INDEX;
                gd.IS_DELETE = false;
                gd.STATUS = _DepartmentModel.STATUS;
                gd.CREATED_BY = _DepartmentModel.CREATED_BY;
                gd.CREATED_DATE = System.DateTime.Now;
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
    }
}
