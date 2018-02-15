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
    public class AccessRightAPIController : ApiController
    {
        NEW_POSEntities db = new NEW_POSEntities();
        AccessRightsModel Model = new AccessRightsModel();

        public HttpResponseMessage GetDepartment(int id)
        {
            var str = (from a in db.TBL_DEPARTMENT
                       where a.COMPANY_ID == id
                       select new DeptModel
                       {
                           DEPARTMENT_ID = a.DEPARTMENT_ID,
                           DEPARTMENT_NAME = a.DEPARTMENT_NAME
                       }).ToList();
            // Model.AllDeperment = str;
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage GetDesignation(int id)
        {
            var str1 = (from a in db.TBL_DESIGNATION
                        where a.COMPANY_ID == id
                        select new DesgModel
                        {
                            DESIGNATION_ID = a.DESIGNATION_ID,
                            DESIGNATION_NAME = a.DESIGNATION_NAME

                        }).ToList();
            //.Model.AllDesignation = str1;
            return Request.CreateResponse(HttpStatusCode.OK, str1);
        }
        public HttpResponseMessage GetAccessRights(long EId)
        {
            
            var str = (from a in db.MODULE_RIGHTS where a.User_Id== EId
                       select new UserAccessModel
                       {
                           ACTION_CREATE = a.ACTION_CREATE,
                           ACTION_DELETE = a.ACTION_DELETE,
                           ACTION_VIEW = a.ACTION_VIEW,
                           APPROVE = a.APPROVE,
                           Company_Id = a.Company_Id,
                           EDIT = a.EDIT,
                           EXPT_TO_EXCEL = a.EXPT_TO_EXCEL,
                           ID = a.ID,
                           IMORT_TO_EXCEL = a.IMORT_TO_EXCEL,
                           MAILBACK = a.MAILBACK,
                           MESSAGE = a.MESSAGE,
                           MODULE_ID = a.MODULE_ID,
                           MODULE_NAME = a.MODULE_NAME,
                           NOTIFICATION = a.NOTIFICATION,
                           User_Id = a.User_Id,
                           VERIFICATION = a.VERIFICATION,
                           ROLE_ID = (long)a.ROLE_ID,
                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }


        [HttpPost]
        public HttpResponseMessage CreateAcessRight(ObservableCollection<UserAccessModel> _UserAccessModel)
        {
            try
            {
                AUTHORIZATION Acc = new AUTHORIZATION();
                long Eid =Convert.ToInt64( _UserAccessModel.ElementAt(0).User_Id);
                var Access = (from m in db.AUTHORIZATIONs where m.User_Id== Eid select m).FirstOrDefault();
                if (Access == null)
                {
                    foreach (var item in _UserAccessModel)
                    {
                        Acc.ACTION_CREATE = item.ACTION_CREATE;
                        Acc.ACTION_DELETE = item.ACTION_DELETE;
                        Acc.ACTION_VIEW = item.ACTION_VIEW;
                        Acc.APPROVE = item.APPROVE;
                        Acc.Company_Id = item.Company_Id;
                        Acc.EDIT = item.EDIT;
                        Acc.EXPT_TO_EXCEL = item.EXPT_TO_EXCEL;
                        Acc.IMORT_TO_EXCEL = item.IMORT_TO_EXCEL;
                        Acc.MAILBACK = item.MAILBACK;
                        Acc.MESSAGE = item.MESSAGE;
                        Acc.MODULE_ID = item.MODULE_ID;
                        Acc.NOTIFICATION = item.NOTIFICATION;
                        Acc.ROLE_ID = item.ROLE_ID;
                        Acc.User_Id = item.User_Id;
                        Acc.VERIFICATION = item.VERIFICATION;
                        //Acc.MODIFIED_DATE = _UserAccessModel.;
                        db.AUTHORIZATIONs.Add(Acc);
                        db.SaveChanges();
                    }
                   
                }
                else
                {
                    foreach (var item in _UserAccessModel)
                    {
                        var AccessModule = (from m in db.AUTHORIZATIONs where m.User_Id == Eid && m.MODULE_ID== item.MODULE_ID select m).FirstOrDefault();
                        AccessModule.ACTION_CREATE = item.ACTION_CREATE;
                        AccessModule.ACTION_DELETE = item.ACTION_DELETE;
                        AccessModule.ACTION_VIEW = item.ACTION_VIEW;
                        AccessModule.APPROVE = item.APPROVE;
                        AccessModule.Company_Id = item.Company_Id;
                        AccessModule.EDIT = item.EDIT;
                        AccessModule.EXPT_TO_EXCEL = item.EXPT_TO_EXCEL;
                        AccessModule.IMORT_TO_EXCEL = item.IMORT_TO_EXCEL;
                        AccessModule.MAILBACK = item.MAILBACK;
                        AccessModule.MESSAGE = item.MESSAGE;
                        //AccessModule.MODULE_ID = item.MODULE_ID;
                        AccessModule.NOTIFICATION = item.NOTIFICATION;
                       // AccessModule.ROLE_ID = item.ROLE_ID;
                       // AccessModule.User_Id = item.User_Id;
                        AccessModule.VERIFICATION = item.VERIFICATION;
                        //Acc.MODIFIED_DATE = _UserAccessModel.;
                        //db.AUTHORIZATIONs.Add(Acc);
                        db.SaveChanges();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
    }
}
