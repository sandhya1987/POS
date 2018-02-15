using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using InvoicePOSAPI.Models;

namespace InvoicePOSAPI.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/

        [HttpPost]
        public ActionResult InsertCompany(CompanyModel Model)
        {
            if (ModelState.IsValid)
            {
                //if (!attributeCommandRepository.CreateAttribute(testAttribute).Status)
                //    return null;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["AdminBaseUrl"].ToString());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                HttpContent content = new ObjectContent<CompanyModel>(Model, jsonFormatter);
                var response = client.PostAsync("api/CompanyAPIController/CreateCompany/", content).Result;
                if (!response.IsSuccessStatusCode)
                    return null;
            }
            return Json(new CompanyModel[] { Model });
        }

    }
}
