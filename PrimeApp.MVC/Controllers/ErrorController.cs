using Microsoft.AspNetCore.Mvc;

namespace PrimeApp.MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
            switch (statusCode)
            {
                case 401:
                    ViewBag.Title = "Yetkisiz Erişim";
                    ViewBag.Message = "Bu sayfaya erişmek için giriş yapmanız gerekiyor.";
                    break;
                case 403:
                    ViewBag.Title = "Erişim Reddedildi";
                    ViewBag.Message = "Bu sayfaya erişim yetkiniz yok.";
                    break;
                default:
                    ViewBag.Title = "Bir Hata Oluştu";
                    ViewBag.Message = "Beklenmedik bir durum oluştu.";
                    break;
            }

            Response.StatusCode = statusCode;
            return View("Status");
        }
    }
}
