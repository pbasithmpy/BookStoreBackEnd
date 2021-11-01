using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BookStoreBackEnd.Controllers
{
    [RoutePrefix("api/upload")]
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    //[DisableCors]
    public class ImageUploadController : ApiController
    {
        [Route("image")]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var imageFiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var domainName = "https://localhost:44332/";

                    var postedFile = httpRequest.Files[file];
                    var fileName = GetHashString(Path.GetFileNameWithoutExtension(postedFile.FileName));
                    var filePath = HttpContext.Current.Server.MapPath("../../Assets/Images/" + fileName+Path.GetExtension(postedFile.FileName));

                    if (!File.Exists(filePath))
                    {
                        postedFile.SaveAs(filePath);
                    }
                    var finalFilePath = domainName+"Assets/Images/" + fileName + Path.GetExtension(postedFile.FileName);
                    imageFiles.Add(finalFilePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, imageFiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }


        #region for getting hash string
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        #endregion
    }
}

