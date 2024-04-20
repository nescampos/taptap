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
            IndexMintViewModel model = new IndexMintViewModel();
            string macaroon = System.IO.File.ReadAllText(_configuration["MacaroonPath"]).ToHex();

            string url = $"https://{_configuration["RESTHost"]}/v1/taproot-assets/assets";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add("Grpc-Metadata-macaroon", macaroon);
                request.Headers.Add("rejectUnauthorized", "false");
                request.Headers.Add("json", "true");

            
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    ListAssetResponse jsonResponse = JsonConvert.DeserializeObject<ListAssetResponse>(result);
                    model.assets = jsonResponse.assets;
                    return View(model);
                }
            }
            catch (WebException ex)
            {
                model.assets = null;
                Console.WriteLine(ex.Message);
            }
            return View(model);
        }

        public IActionResult MintAsset()
        {
            MintAssetViewModel model = new MintAssetViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult MintAsset(MintAssetFormModel Form)
        {
            MintAssetViewModel model = new MintAssetViewModel();
            if (!ModelState.IsValid)
            {
                
                model.Form = Form;
                return View(model);
            }

            MintAssetRequest requestBody = new MintAssetRequest
            {
                asset = new MintAsset()
                {
                    name = Form.Name, amount = Form.Amount.Value.ToString(), asset_type = Form.Type,
                    asset_version = Form.Version, new_grouped_asset = true
                },
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
                    MintAssetResponse jsonResponse = JsonConvert.DeserializeObject<MintAssetResponse>(result);
                    ViewBag.MintedAsset = jsonResponse;
                    return RedirectToAction("Minted");
                }
            }
            catch (WebException ex)
            {
                model.Form = Form;
                model.Error = ex.Message;
                return View(model);
            }
        }

        public IActionResult Minted()
        {
            return View();
        }
    }
}
