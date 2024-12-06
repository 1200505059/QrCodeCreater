using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

namespace TicariOtomasyonMVC.Controllers
{
    public class QRController : Controller
    {
        // GET: Qr
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);

                    using (Bitmap bitmap = qrCode.GetGraphic(10))
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        string base64Image = Convert.ToBase64String(ms.ToArray());
                        ViewBag.QrCodeImage = "data:image/png;base64," + base64Image; // Hata burada düzeltilmiştir
                    }
                }
            }
            else
            {
                ViewBag.Error = "Lütfen bir kod girin!";
            }

            return View();
        }
    }
}
