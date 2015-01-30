using MyRoom.Web.Infraestructure;
using MyRoom.Web.Infraestructure.MyRoom.API.Infraestructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MyRoom.Web.Controllers
{
    [RoutePrefix("api/files")]
    public class FilesController : ApiController
    {
        [HttpPost] // This is from System.Web.Http, and not from System.Web.Mvc
        public Task<IEnumerable<FileDesc>> Upload()
        {
            string folderName = "";
            String valorPar = Request.RequestUri.Query.Substring(5,1);
            if (valorPar == "1")
            {
                folderName  = ConfigurationManager.AppSettings["UploadImages"];//"/images";
            }
            string PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            string rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);

            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);
                var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IEnumerable<FileDesc>>(t =>
                {

                    if (t.IsFaulted || t.IsCanceled)
                    {
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                    }

                    var fileInfo = streamProvider.FileData.Select(i =>
                    {
                        var info = new FileInfo(i.LocalFileName);
                        return new FileDesc(info.Name, rootUrl + "/" + folderName + "/" + info.Name, info.Length / 1024);
                    });
                    return fileInfo;
                });

                return task;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }

        }
    }
}