using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KeyVaultSampleApp1.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var secretValueFromKeyVault = await KeyVaultAccessor.GetSecret(CloudConfigurationManager.GetSetting(Constants.DevSecretId1));

            var stopwatch = Stopwatch.StartNew();
            var secretValue = KeyVaultAccessor.GetSecret(CloudConfigurationManager.GetSetting(Constants.DevSecretId1)).Result;
            stopwatch.Stop();
            ViewBag.FetchSecretElapsedTime = stopwatch.ElapsedMilliseconds;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}