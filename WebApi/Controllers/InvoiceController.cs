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
        public UploadResult Post()
        {
            var result = new UploadResult();
            var httpContext = HttpContext.Current;

            result = uploadCls.SaveFile(httpContext);

            if (result.isSuccess.Equals(true))
            {
                result.ResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                return result;
            }
            else
            {
                result.ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
                return result;
            }
        }
    }
}