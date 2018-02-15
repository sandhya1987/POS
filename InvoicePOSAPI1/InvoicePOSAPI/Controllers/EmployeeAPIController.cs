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
    public class EmployeeAPIController : ApiController
    {
        EmployeeModel emp = new EmployeeModel();
        NEW_POSEntities db = new NEW_POSEntities();
        [HttpGet]
        public HttpResponseMessage EmployeeList(int id)
        {
            var str = (from a in db.TBL_EMPLOYEE
                       where a.COMPANY_ID == id && a.IS_DELETE == false
                       select new EmployeeModel
                       {
                           ADDRESS_1 = a.ADDRESS_1,
                           ADDRESS_2 = a.ADDRESS_2,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           CITY = a.CITY,
                           COMMISSION_QUICK_POSITION = a.COMMISSION_QUICK_POSITION,
                           COMPANY_ID = a.COMPANY_ID,
                           DATE_OF_JOIN = a.DATE_OF_JOIN,
                           DEPARTMENT = a.DEPARTMENT,
                           DOB = a.DOB,
                           EMAIL = a.EMAIL,
                           EMPLOYEE_CODE = a.EMPLOYEE_CODE,
                           EMPLOYEE_DESIGNATION = a.EMPLOYEE_DESIGNATION,
                           EMPLOYEE_ID = a.EMPLOYEE_ID,
                           FAX_NO = a.FAX_NO,
                           FIRST_NAME = a.FIRST_NAME,
                           GENDER = a.GENDER,
                           IMAGE = a.IMAGE,
                           IS_APPOINTMENT = a.IS_APPOINTMENT,
                           IS_APPROVE_ACCESS_VAI_SMS = a.IS_APPROVE_ACCESS_VAI_SMS,
                           IS_ATTACHED_INVOICE = a.IS_ATTACHED_INVOICE,
                           IS_NOT_AN_EMPLOYEE = a.IS_NOT_AN_EMPLOYEE,
                           IS_REQUEST_VAI_SMS = a.IS_REQUEST_VAI_SMS,
                           LAST_NAME = a.LAST_NAME,
                           MARITAL_STATUS = a.MARITAL_STATUS,
                           MAX_SPOT_DISCOUNT = a.MAX_SPOT_DISCOUNT,
                           MIDDLE_NAME = a.MIDDLE_NAME,
                           MOBILE_NO = a.MOBILE_NO,
                           PIN_NO = a.PIN_NO,
                           SALES_PERCENT = a.SALES_PERCENT,
                           SEARCH_CODE = a.SEARCH_CODE,
                           STATE = a.STATE,
                           TELEPHONE_NO = a.TELEPHONE_NO,
                           WEBSITE = a.WEBSITE,
                           WORKING_SHIFT = a.WORKING_SHIFT,




                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage EmployeeEdit(int id)
        {
            var str = (from a in db.TBL_EMPLOYEE
                       where a.EMPLOYEE_ID == id
                       select new EmployeeModel
                       {
                           ADDRESS_1 = a.ADDRESS_1,
                           ADDRESS_2 = a.ADDRESS_2,
                           BUSINESS_LOCATION = a.BUSINESS_LOCATION,
                           CITY = a.CITY,
                           COMMISSION_QUICK_POSITION = a.COMMISSION_QUICK_POSITION,
                           COMPANY_ID = a.COMPANY_ID,
                           DATE_OF_JOIN = a.DATE_OF_JOIN,
                           DEPARTMENT = a.DEPARTMENT,
                           DOB = a.DOB,
                           EMAIL = a.EMAIL,
                           EMPLOYEE_CODE = a.EMPLOYEE_CODE,
                           EMPLOYEE_DESIGNATION = a.EMPLOYEE_DESIGNATION,
                           EMPLOYEE_ID = a.EMPLOYEE_ID,
                           FAX_NO = a.FAX_NO,
                           FIRST_NAME = a.FIRST_NAME,
                           GENDER = a.GENDER,
                           IMAGE = a.IMAGE,
                           IS_APPOINTMENT = a.IS_APPOINTMENT,
                           IS_APPROVE_ACCESS_VAI_SMS = a.IS_APPROVE_ACCESS_VAI_SMS,
                           IS_ATTACHED_INVOICE = a.IS_ATTACHED_INVOICE,
                           IS_NOT_AN_EMPLOYEE = a.IS_NOT_AN_EMPLOYEE,
                           IS_REQUEST_VAI_SMS = a.IS_REQUEST_VAI_SMS,
                           LAST_NAME = a.LAST_NAME,
                           MARITAL_STATUS = a.MARITAL_STATUS,
                           MAX_SPOT_DISCOUNT = a.MAX_SPOT_DISCOUNT,
                           MIDDLE_NAME = a.MIDDLE_NAME,
                           MOBILE_NO = a.MOBILE_NO,
                           PIN_NO = a.PIN_NO,
                           SALES_PERCENT = a.SALES_PERCENT,
                           SEARCH_CODE = a.SEARCH_CODE,
                           STATE = a.STATE,
                           TELEPHONE_NO = a.TELEPHONE_NO,
                           WEBSITE = a.WEBSITE,
                           WORKING_SHIFT = a.WORKING_SHIFT,




                       }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, str);
        }
        [HttpGet]
        public HttpResponseMessage EmployeeDelete(int id)
        {
            var str = (from a in db.TBL_EMPLOYEE where a.EMPLOYEE_ID == id select a).FirstOrDefault();
            str.IS_DELETE = true;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
        [HttpPost]
        public HttpResponseMessage EmployeeAdd(EmployeeModel _empModel)
        {
            try
            {
                TBL_EMPLOYEE emp = new TBL_EMPLOYEE();
                emp.ADDRESS_1 = _empModel.ADDRESS_1;
                emp.ADDRESS_2 = _empModel.ADDRESS_2;
                emp.BUSINESS_LOCATION = _empModel.BUSINESS_LOCATION;
                emp.CITY = _empModel.CITY;
                emp.COMPANY_ID = _empModel.COMPANY_ID;
                emp.DATE_OF_JOIN = _empModel.DATE_OF_JOIN;
                emp.DEPARTMENT = _empModel.DEPARTMENT;
                emp.DOB = _empModel.DOB;
                emp.EMAIL = _empModel.EMAIL;
                emp.EMPLOYEE_CODE = _empModel.EMPLOYEE_CODE;
                emp.EMPLOYEE_DESIGNATION = _empModel.EMPLOYEE_DESIGNATION;
                emp.FAX_NO = _empModel.FAX_NO;
                emp.FIRST_NAME = _empModel.FIRST_NAME;
                emp.GENDER = _empModel.GENDER;
                emp.IMAGE = _empModel.IMAGE;
                emp.IS_APPOINTMENT = _empModel.IS_APPOINTMENT;
                emp.IS_APPROVE_ACCESS_VAI_SMS = _empModel.IS_APPROVE_ACCESS_VAI_SMS;
                emp.IS_ATTACHED_INVOICE = _empModel.IS_ATTACHED_INVOICE;
                emp.IS_NOT_AN_EMPLOYEE = _empModel.IS_NOT_AN_EMPLOYEE;
                emp.IS_REQUEST_VAI_SMS = _empModel.IS_REQUEST_VAI_SMS;
                emp.LAST_NAME = _empModel.LAST_NAME;
                emp.MARITAL_STATUS = _empModel.MARITAL_STATUS;
                emp.MAX_SPOT_DISCOUNT = _empModel.MAX_SPOT_DISCOUNT;
                emp.MIDDLE_NAME = _empModel.MIDDLE_NAME;
                emp.MOBILE_NO = _empModel.MOBILE_NO;
                emp.PIN_NO = _empModel.PIN_NO;
                emp.SEARCH_CODE = _empModel.SEARCH_CODE;
                emp.STATE = _empModel.STATE;
                emp.TELEPHONE_NO = _empModel.TELEPHONE_NO;
                emp.WEBSITE = _empModel.WEBSITE;
                emp.WORKING_SHIFT = _empModel.WORKING_SHIFT;
                emp.SALES_PERCENT = _empModel.SALES_PERCENT;
                emp.COMMISSION_QUICK_POSITION = _empModel.COMMISSION_QUICK_POSITION;
                emp.IS_DELETE = false;
                db.TBL_EMPLOYEE.Add(emp);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        public HttpResponseMessage GetEmpNo()
        {

            string value = Convert.ToString(db.TBL_EMPLOYEE
                            .OrderByDescending(p => p.EMPLOYEE_CODE)
                            .Select(r => r.EMPLOYEE_CODE)
                            .First());
            var RefNumber = new
            {
                EmpRefNumber = value
            };
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
        [HttpPost]
        public HttpResponseMessage EmployeeUpdate(EmployeeModel _empModel)
        {
            var str = (from a in db.TBL_EMPLOYEE where a.EMPLOYEE_ID == _empModel.EMPLOYEE_ID select a).FirstOrDefault();
            str.ADDRESS_1 = _empModel.ADDRESS_1;
            str.ADDRESS_2 = _empModel.ADDRESS_2;
            str.BUSINESS_LOCATION = _empModel.BUSINESS_LOCATION;
            str.CITY = _empModel.CITY;
            str.COMPANY_ID = _empModel.COMPANY_ID;
            str.DATE_OF_JOIN = _empModel.DATE_OF_JOIN;
            str.DEPARTMENT = _empModel.DEPARTMENT;
            str.DOB = _empModel.DOB;
            str.EMAIL = _empModel.EMAIL;
            str.EMPLOYEE_CODE = _empModel.EMPLOYEE_CODE;
            str.EMPLOYEE_DESIGNATION = _empModel.EMPLOYEE_DESIGNATION;
            str.FAX_NO = _empModel.FAX_NO;
            str.FIRST_NAME = _empModel.FIRST_NAME;
            str.GENDER = _empModel.GENDER;
            str.IMAGE = _empModel.IMAGE;
            str.IS_APPOINTMENT = _empModel.IS_APPOINTMENT;
            str.IS_APPROVE_ACCESS_VAI_SMS = _empModel.IS_APPROVE_ACCESS_VAI_SMS;
            str.IS_ATTACHED_INVOICE = _empModel.IS_ATTACHED_INVOICE;
            str.IS_NOT_AN_EMPLOYEE = _empModel.IS_NOT_AN_EMPLOYEE;
            str.IS_REQUEST_VAI_SMS = _empModel.IS_REQUEST_VAI_SMS;
            str.LAST_NAME = _empModel.LAST_NAME;
            str.MARITAL_STATUS = _empModel.MARITAL_STATUS;
            str.MAX_SPOT_DISCOUNT = _empModel.MAX_SPOT_DISCOUNT;
            str.MIDDLE_NAME = _empModel.MIDDLE_NAME;
            str.MOBILE_NO = _empModel.MOBILE_NO;
            str.PIN_NO = _empModel.PIN_NO;
            str.SEARCH_CODE = _empModel.SEARCH_CODE;
            str.STATE = _empModel.STATE;
            str.TELEPHONE_NO = _empModel.TELEPHONE_NO;
            str.WEBSITE = _empModel.WEBSITE;
            str.WORKING_SHIFT = _empModel.WORKING_SHIFT;
            str.SALES_PERCENT = _empModel.SALES_PERCENT;
            str.COMMISSION_QUICK_POSITION = _empModel.COMMISSION_QUICK_POSITION;
            str.IS_DELETE = false;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
    }
}
