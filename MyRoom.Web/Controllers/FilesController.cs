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
            string folderNamePro = "";
            string folderNameMor = "";
            string param = Request.RequestUri.Query.Substring(5);
            string[] split = param.Split(new Char[] { '-' });
            String action =split[0];
            String CatalogId = split[1];
            String Id = split[2];
            switch (action) 
            {
                case "1": //Hotel
                    folderName  = ConfigurationManager.AppSettings["UploadImages"];//"/images";
                    break;
                case "2": //Catalog
                    folderName    = ConfigurationManager.AppSettings["UploadImagesCatalog"] +"//" + CatalogId;//"/images";
                    folderNameMod = ConfigurationManager.AppSettings["UploadImagesCatalog"] + "//" + CatalogId + "//modules";
                    folderNameCat = ConfigurationManager.AppSettings["UploadImagesCatalog"] + "//" + CatalogId + "//categories";
                    folderNamePro = ConfigurationManager.AppSettings["UploadImagesCatalog"] + "//" + CatalogId + "//products";
                    folderNameMor = ConfigurationManager.AppSettings["UploadImagesCatalog"] + "//" + CatalogId + "//moreinfo";
                    break;
                case "3": //modules
                    folderName = ConfigurationManager.AppSettings["UploadImagesCatalog"] + "//" + CatalogId + "//modules";
                    break;
                case "4": //categories
                    folderName = ConfigurationManager.AppSettings["UploadImagesCatalog"] + "//" + CatalogId + "//categories";
                    break;
                case "5": //product
                    folderName = ConfigurationManager.AppSettings["UploadImagesCatalog"] + "//" + CatalogId + "//products";
                    break;
            }
            string PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
            string PATHModule = HttpContext.Current.Server.MapPath("~/" + folderNameMod);
            string PATCategory = HttpContext.Current.Server.MapPath("~/" + folderNameCat);
            string PATProduct = HttpContext.Current.Server.MapPath("~/" + folderNamePro);
            string PATMoreInfo = HttpContext.Current.Server.MapPath("~/" + folderNameMor);
            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH);
                if (action == "2")
                {
                    Directory.CreateDirectory(PATHModule);
                    Directory.CreateDirectory(PATCategory);
                    Directory.CreateDirectory(PATProduct);
                    Directory.CreateDirectory(PATMoreInfo);
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