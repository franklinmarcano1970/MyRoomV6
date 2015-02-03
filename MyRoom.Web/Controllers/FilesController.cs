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
            string folderNameMod = "";
            string folderNameCat = "";
            string param = Request.RequestUri.Query.Substring(5);
            string[] split = param.Split(new Char[] { '-' });
            String action =split[0];
            String CatalogId = split[1];
            String Id = split[2];
            if (action == "1") //Hotel
            {
                folderName  = ConfigurationManager.AppSettings["UploadImages"];//"/images";
            }
            if (action == "2") //Catalog
            {
                folderName = ConfigurationManager.AppSettings["UploadImagesCatalog"] +"//" + CatalogId;//"/images";
                folderNameMod = ConfigurationManager.AppSettings["UploadImagesCatalog"] + "//" + CatalogId + "//modules";
                folderNameCat = ConfigurationManager.AppSettings["UploadImagesCatalog"] + "//" + CatalogId + "//categories";
            }
            if (action == "3") //Modules
            {
                folderName = ConfigurationManager.AppSettings["UploadImagesCatalog"] + "//" + CatalogId + "//modules";
            }
            if (action == "4") //Category
            {
                folderName = ConfigurationManager.AppSettings["UploadImagesCatalog"] + "//" + CatalogId + "//categories";
            }
            if (action == "5") //Product
            {
                folderName = ConfigurationManager.AppSettings["UploadImagesProduct"];//"/images";
            }
            string PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
            string PATHModule = HttpContext.Current.Server.MapPath("~/" + folderNameMod);
            string PATCategory = HttpContext.Current.Server.MapPath("~/" + folderNameCat);
            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH);
                if (action == "2")
                {
                    Directory.CreateDirectory(PATHModule);
                    Directory.CreateDirectory(PATCategory);
                }
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