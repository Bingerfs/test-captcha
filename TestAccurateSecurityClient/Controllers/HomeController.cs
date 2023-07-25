using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Xml.Linq;
using TestAccurateSecurityClient.Models;

namespace TestAccurateSecurityClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestCloudFlareCaptcha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TestCloudFlareCaptcha([FromForm] LoginCloudFlare loginCloudFlare)
        {
            var secret_sey = "0x4AAAAAAAHxYpiYQ_fRuLTXG8l1hg-aytc";

            using (var client = new HttpClient())
            {
                //var formContent = new FormUrlEncodedContent(new[]
                //{
                //    new KeyValuePair<string, string>("secret", secret_sey),
                //    new KeyValuePair<string, string>("response", loginCloudFlare.CfTurnstileResponse),
                //    new KeyValuePair<string, string>("remoteip", "152.0.117.184")
                //});

                var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(secret_sey), "secret");
                formData.Add(new StringContent(loginCloudFlare.CfTurnstileResponse), "response");
                formData.Add(new StringContent(Request.HttpContext.Connection.RemoteIpAddress.ToString()), "remoteip");

                //var json_content = new StringContent(JsonSerializer.Serialize(formContent));

                var url_cloudflare = "https://challenges.cloudflare.com/turnstile/v0/siteverify";

                client.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                //var result = await client.PostAsync(url_cloudflare, json_content);
                var result = await client.PostAsJsonAsync(url_cloudflare, formData);

                var content_result = await result.Content.ReadAsStringAsync();

                var json_result = JsonSerializer.Deserialize<CaptchaResponseCloudFlare>(content_result);

                if (json_result.Success)
                {
                    return View("Success");
                }
                else
                {
                    return View("Failed");
                }

            }
            return View();
        }

        public IActionResult TestGoogleCaptcha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TestGoogleCaptcha([FromForm] LoginCloudFlare loginCloudFlare)
        {
            var secret_sey = "6LeuN08nAAAAAHf3-e3Fzlfqal-M0juelOJziz2B";

            using (var client = new HttpClient())
            {
                //var formContent = new FormUrlEncodedContent(new[]
                //{
                //    new KeyValuePair<string, string>("secret", secret_sey),
                //    new KeyValuePair<string, string>("response", loginCloudFlare.CfTurnstileResponse),
                //    new KeyValuePair<string, string>("remoteip", "152.0.117.184")
                //});

                var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(secret_sey), "Secret");
                formData.Add(new StringContent(loginCloudFlare.CfTurnstileResponse), "respuesta");
                formData.Add(new StringContent(Request.HttpContext.Connection.RemoteIpAddress.ToString()), "control remoto");

                //var json_content = new StringContent(JsonSerializer.Serialize(formContent));

                var url_cloudflare = "https://challenges.cloudflare.com/turnstile/v0/siteverify";

                client.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                //var result = await client.PostAsync(url_cloudflare, json_content);
                var result = await client.PostAsJsonAsync(url_cloudflare, formData);

                var content_result = await result.Content.ReadAsStringAsync();

                var json_result = JsonSerializer.Deserialize<CaptchaResponseCloudFlare>(content_result);

                if (json_result.Success)
                {
                    return View("Success");
                }
                else
                {
                    return View("Failed");
                }

            }
        }

        [HttpPost]
        public IActionResult Index([FromBody]string tokenGoogle)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}