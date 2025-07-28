using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using EncryptionTestApp;

namespace EncryptionDecryptionApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
             ViewBag.EncryptionKey = _configuration["EncryptionKey"];
            return View();
        }

        [HttpPost]
        public JsonResult EncryptData(string key, string plainText)
        {
            try
            {
                string encryptedText = AesOperation.EncryptString(key, plainText);
                return Json(new { success = true, encryptedText });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DecryptData(string key, string encryptedText)
        {
            try
            {
                string plainText = AesOperation.DecryptString(key, encryptedText);
                return Json(new { success = true, plainText });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
