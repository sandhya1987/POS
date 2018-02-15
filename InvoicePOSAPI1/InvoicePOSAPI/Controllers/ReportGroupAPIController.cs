using InvoicePOSAPI.Models;
using InvoicePOSDATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace InvoicePOSAPI.Controllers
{
    public class ReportGroupAPIController : ApiController
    {
        NEW_POSEntities db = new NEW_POSEntities();
        ReportGroupModel _ReportGroupModel = new ReportGroupModel();
        [HttpGet]
        public HttpResponseMessage GetReportName(int id)
        {
            var str = (from a in db.TBL_REPORT_ADD
                       join b in db.TBL_REPORT_GROUP on a.REPORT_GROUP_ID equals b.REPORT_GRP_ID

                       select new ReportAddModel
                       {


                           REPORT_NAME = a.REPORT_NAME,
                           SEARCH_CODE = a.SEARCH_CODE,
                           REPORT_GROUP_ID = a.REPORT_GROUP_ID,
                           REPORT_ADD_ID = a.REPORT_ID,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetReportHirarchy(int id)
        {
            var str = (from a in db.VIEW_GROUP_SUBGROUP
                      
                       select new ReportGroupModel
                       {

                          DATE=System.DateTime.Now,
                           REPORT_GRP_ID = 0,
                           GROUP_NAME = a.GroupName,
                           REPORT_ID = 0,
                           REPORT_CHILD_ID=a.Child,
                           REPORT_PARENT_ID=a.ParentId,
                           REPORT_NAME = a.GroupName,
                           IS_DELETE = false,
                           CREATED_BY = 1,
                           COMPANY_ID = 1,

                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpPost]
        public HttpResponseMessage ReportGroupAdd(ReportGroupModel _ReportGroupModel)
        {
            if (_ReportGroupModel.REPORT_GRP_ID == null || _ReportGroupModel.REPORT_GRP_ID == 0)
            {
                TBL_REPORT_GROUP _gr = new TBL_REPORT_GROUP();
                _gr.DATE = DateTime.Now;
                _gr.DESCRIPTION = _ReportGroupModel.DESCRIPTION;
                _gr.GROUP_NAME = _ReportGroupModel.GROUP_NAME;
                //_gr.REPORT_ID = _ReportGroupModel.REPORT_ID;
                _gr.REPORT_ID = 4;
                _gr.IS_DELETE = _ReportGroupModel.IS_DELETE;
                _gr.COMPANY_ID = _ReportGroupModel.COMPANY_ID;
                _gr.CREATED_BY = _ReportGroupModel.CREATED_BY;
                _gr.REPORT_NAME = _ReportGroupModel.REPORT_NAME;
                _gr.SGROUP_CODE = _ReportGroupModel.SGROUP_CODE;

                db.TBL_REPORT_GROUP.Add(_gr);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
        [HttpPost]
        public HttpResponseMessage ReportAdd(ReportAddModel _ReportAddModel)
        {
            try
            {
                if (_ReportAddModel.REPORT_ADD_ID == null || _ReportAddModel.REPORT_ADD_ID == 0)
                {
                    TBL_REPORT_ADD _gr = new TBL_REPORT_ADD();
                    _gr.DESCRIPTION = _ReportAddModel.DESCRIPTION_ADD;
                    _gr.REPORT_NAME = _ReportAddModel.REPORT_NAME;
                    //_gr.REPORT_GROUP_ID = _ReportAddModel.REPORT_GROUP_ID;
                    _gr.REPORT_GROUP_ID = 4;
                    _gr.SEARCH_CODE = _ReportAddModel.SEARCH_CODE;
                    db.TBL_REPORT_ADD.Add(_gr);
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "ok");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        public HttpResponseMessage ReportGroupEdit(int id)
        {
            var str = (from a in db.TBL_REPORT_GROUP
                       where a.REPORT_GRP_ID == id && a.IS_DELETE == false
                       select new ReportGroupModel
                       {

                           DATE = a.DATE,
                           DESCRIPTION = a.DESCRIPTION,
                           REPORT_GRP_ID = a.REPORT_GRP_ID,
                           GROUP_NAME = a.GROUP_NAME,
                           REPORT_ID = a.REPORT_ID,
                           CREATED_BY = a.CREATED_BY,
                           COMPANY_ID = a.COMPANY_ID,
                           IS_DELETE = a.IS_DELETE,
                           REPORT_NAME = a.REPORT_NAME,
                           SGROUP_CODE = a.SGROUP_CODE,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage ReportGroupDelete(int id)
        {
            var str = (from a in db.TBL_REPORT_GROUP where a.REPORT_GRP_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "ok");
        }
    }

}
