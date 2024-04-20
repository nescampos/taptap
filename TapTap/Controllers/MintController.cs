using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TapTap.Models;
using TapTap.Extensions;

namespace TapTap.Controllers
{
    public class MintController : Controller
    {
        private IConfiguration _configuration;
        public MintController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MintAsset()
        {
            MintAssetViewModel model = new MintAssetViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult MintAsset(MintAssetFormModel Form)
        {
            if(!ModelState.IsValid)
            {
                MintAssetViewModel model = new MintAssetViewModel();
                model.Form = Form;
                return View(model);
            }

            MintAssetRequest requestBody = new MintAssetRequest
            {
                asset = new MintAsset(),
                short_response = false
            };

            string macaroon = System.IO.File.ReadAllText(_configuration["MacaroonPath"]).ToHex();

            string url = $"https://{_configuration["RESTHost"]}/v1/taproot-assets/assets";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Grpc-Metadata-macaroon", macaroon);

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string jsonRequestBody = JsonConvert.SerializeObject(requestBody);
                streamWriter.Write(jsonRequestBody);
                streamWriter.Flush();
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }
    }
}
