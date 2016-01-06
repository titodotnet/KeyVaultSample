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
            string devSecretId1 = CloudConfigurationManager.GetSetting(Constants.DevSecretId1);

            var stopwatch = Stopwatch.StartNew();
            var secretValueFromKeyVault = await KeyVaultAccessor.GetSecret(devSecretId1);
            stopwatch.Stop();
            ViewBag.InitialFetchSecretElapsedTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            var secretValue = KeyVaultAccessor.GetSecret(devSecretId1).Result;
            stopwatch.Stop();

            ViewBag.SecretId = devSecretId1;
            ViewBag.SecretValue = secretValue;
            ViewBag.FetchSecretElapsedTime = stopwatch.ElapsedMilliseconds;
            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";

            string vaultUri = CloudConfigurationManager.GetSetting(Constants.VaultUri);
            string devSecretName1 = CloudConfigurationManager.GetSetting(Constants.DevSecretName1);

            var stopwatch = Stopwatch.StartNew();
            var secretValueFromKeyVault = await KeyVaultAccessor.GetSecret(vaultUri, devSecretName1);
            stopwatch.Stop();
            ViewBag.InitialFetchSecretElapsedTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            var secretValue = KeyVaultAccessor.GetSecret(vaultUri, devSecretName1).Result;
            stopwatch.Stop();

            ViewBag.SecretId = devSecretName1;
            ViewBag.SecretValue = secretValue;
            ViewBag.FetchSecretElapsedTime = stopwatch.ElapsedMilliseconds;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}