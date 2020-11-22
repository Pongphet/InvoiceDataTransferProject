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

namespace WebApi.Controllers
{
    public class InvoiceController : ApiController
    {
        private InvoiceUploadClass uploadCls = new InvoiceUploadClass();

        [HttpPost]
        public HttpResponseMessage Post()
        {
            var result = new UploadResult();
            var httpContext = HttpContext.Current;

            result = uploadCls.SaveFile(httpContext);

            if (result.isSuccess.Equals(true))
            {
                uploadCls.SubmitInvoiceTransaction(result.InvoiceDataTransaction);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}