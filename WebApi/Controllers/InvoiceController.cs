using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using WebApi.Classes;
using System.Configuration;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class InvoiceController : ApiController
    {
        private InvoiceUploadClass uploadCls = new InvoiceUploadClass();
        private InquiryInvoiceData inquiryCls = new InquiryInvoiceData();
     [HttpPost]
        public HttpResponseMessage Post()
        {
            var result = new UploadResult();
            var httpContext = HttpContext.Current;

            result = uploadCls.SaveFile(httpContext);
            if (result.isSuccess.Equals(true))
            {
                uploadCls.SubmitInvoiceTransaction(result.InvoiceDataTransaction);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ReasonPhrase = result.ValidationMessage
                };
                throw new HttpResponseException(resp);
            }
        }

        [HttpGet]
        public IHttpActionResult Get(string criteria, string keyword, string startDate, string endDate)
        {
            var result = new List<InvoiceDataOutput>();
            result = inquiryCls.GetInvoiceTransaction(criteria, keyword, startDate, endDate);

            return Ok(result);
        }
    }
}