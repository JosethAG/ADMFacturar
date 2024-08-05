using ADM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ADMFacturar.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]
    public class MarketingController : Controller
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly HttpClient _httpClient;

        public MarketingController(IOptions<SmtpSettings> smtpSettings, IHttpClientFactory httpClientFactory)
        {
            _smtpSettings = smtpSettings.Value;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7270/api");
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

        [HttpPost]
        public async Task<IActionResult> GuardarRegistro([FromBody] Correo Correo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(Correo);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("/api/Marketing/Crear", content);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response from API: " + responseContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error al guardar el registro en la API" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Exception: " + ex.Message });
                }
            }
            else
            {
                // Log model state errors for debugging
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Datos del formulario inválidos", errors });
            }
        }

        public async Task<IActionResult> ListarCorreos()

        {
            var resp = await _httpClient.GetAsync($"api/Marketing/Listar");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var Correos = JsonConvert.DeserializeObject<IEnumerable<FacturaVM>>(content);
                return Json(Correos); // Devuelve 'Ingreso' en lugar de 'resp'
            }

            else
            {
                return BadRequest("Error al obtener los correos");
            }
        }


    }
}
