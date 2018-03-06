using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoicePOSAPI.Models;
using InvoicePOSDATA;

namespace InvoicePOSAPI.Controllers
{
    public class EmailSettingsAPIController : ApiController
    {
        NEW_POS_DBEntities db = new NEW_POS_DBEntities();

        [HttpGet]
        public HttpResponseMessage GetEmailSettings(long userId)
        {
            try
            {
                var str = (from a in db.TBL_EMAIL_SETTINGS
                           where a.USER_ID == userId && a.IS_DELETE == false
                           select new EmailSettingsModel
                           {
                               USER_NAME = a.USER_NAME,
                               SMTP_SERVER_PORT = a.SMTP_SERVER_PORT,
                               SMTP_SERVER = a.SMTP_SERVER,
                               PASSWORD = a.PASSWORD,
                               NAME = a.NAME,
                               IS_REQ_ENCRYPT_CONN = a.IS_REQ_ENCRYPT_CONN,
                               IS_GMAIL = a.IS_GMAIL,
                               EMAIL = a.EMAIL,
                               CC = a.CC,
                               BCC = a.BCC,
                               ID = a.ID,
                               MAIL_TYPE = a.MAIL_TYPE
                           }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, str);
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        [HttpPost]
        public HttpResponseMessage UpdateEmailSettings(EmailSettingsModel _EmailSettingsModel)
        {
            try
            {
                var updateSettings = (from a in db.TBL_EMAIL_SETTINGS
                           where a.ID == _EmailSettingsModel.ID && a.IS_DELETE == false
                           select a).FirstOrDefault();
                if (updateSettings == null)
                {
                    updateSettings = new TBL_EMAIL_SETTINGS();

                    updateSettings.BCC = _EmailSettingsModel.BCC;
                    updateSettings.CC = _EmailSettingsModel.CC;
                    updateSettings.EMAIL = _EmailSettingsModel.EMAIL;
                    updateSettings.IS_GMAIL = _EmailSettingsModel.IS_GMAIL;
                    updateSettings.IS_REQ_ENCRYPT_CONN = _EmailSettingsModel.IS_REQ_ENCRYPT_CONN;
                    updateSettings.NAME = _EmailSettingsModel.NAME;
                    updateSettings.PASSWORD = _EmailSettingsModel.PASSWORD;
                    updateSettings.SMTP_SERVER = _EmailSettingsModel.SMTP_SERVER;
                    updateSettings.SMTP_SERVER_PORT = _EmailSettingsModel.SMTP_SERVER_PORT;
                    updateSettings.USER_ID = _EmailSettingsModel.USER_ID;
                    updateSettings.USER_NAME = _EmailSettingsModel.USER_NAME;
                    updateSettings.MAIL_TYPE = _EmailSettingsModel.MAIL_TYPE;
                    updateSettings.IS_DELETE = false;
                    db.TBL_EMAIL_SETTINGS.Add(updateSettings);
                }
                else
                {
                    updateSettings.BCC = _EmailSettingsModel.BCC;
                    updateSettings.CC = _EmailSettingsModel.CC;
                    updateSettings.EMAIL = _EmailSettingsModel.EMAIL;
                    updateSettings.IS_GMAIL = _EmailSettingsModel.IS_GMAIL;
                    updateSettings.IS_REQ_ENCRYPT_CONN = _EmailSettingsModel.IS_REQ_ENCRYPT_CONN;
                    updateSettings.NAME = _EmailSettingsModel.NAME;
                    updateSettings.PASSWORD = _EmailSettingsModel.PASSWORD;
                    updateSettings.SMTP_SERVER = _EmailSettingsModel.SMTP_SERVER;
                    updateSettings.SMTP_SERVER_PORT = _EmailSettingsModel.SMTP_SERVER_PORT;
                    updateSettings.USER_ID = _EmailSettingsModel.USER_ID;
                    updateSettings.USER_NAME = _EmailSettingsModel.USER_NAME;
                    updateSettings.MAIL_TYPE = _EmailSettingsModel.MAIL_TYPE;
                    updateSettings.IS_DELETE = false;
                }
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {

                throw;
            };


        }

        [HttpPost]
        public HttpResponseMessage DeleteEmailSettings(EmailSettingsModel _EmailSettingsModel)
        {
            try
            {
                var updateSettings = (from a in db.TBL_EMAIL_SETTINGS
                                      where a.ID == _EmailSettingsModel.ID && a.IS_DELETE == false
                                      select a).FirstOrDefault();
                if (updateSettings != null)
                {
                   
                    updateSettings.IS_DELETE = true;
                }
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {

                throw;
            };


        }
    }
}
