using ADM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;


namespace ADMFacturar.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]
    public class MarketingController : Controller
    {
        private readonly SmtpSettings _smtpSettings;

        public MarketingController(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GCIndex()
        {
            return View();
        }
        public IActionResult TemplateIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EnviarCorreo(string to, string subject, string body, List<IFormFile> attachments)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.UserName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                // Adjuntar archivos si los hay
                if (attachments != null)
                {
                    foreach (var file in attachments)
                    {
                        if (file.Length > 0)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.OpenReadStream(), file.FileName));
                        }
                    }
                }

                using (var smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
                {
                    smtpClient.Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password);
                    smtpClient.EnableSsl = _smtpSettings.EnableSsl;
                    await smtpClient.SendMailAsync(mailMessage);
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}