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

        public async Task<IActionResult> Index()
        {
            var resp = await _httpClient.GetAsync("api/Marketing/Listar");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var Correos = JsonConvert.DeserializeObject<IEnumerable<Correo>>(content);
                return View(Correos);
            }

            else
            {
                var Correos = new List<Correo>();
                return View(Correos);
            }
        }

        public async Task<IActionResult> ObtenerGC(string Id)
        {
            var resp = await _httpClient.GetAsync($"api/Marketing/ObtenerGC/{Id}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var Correos = JsonConvert.DeserializeObject<GrupoCorreo>(content);
                return Ok(Correos);
            }

            else
            {
                var Correos = new GrupoCorreo();
                return View(Correos);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateGC([FromForm] GrupoCorreo GC)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(GC);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/api/Marketing/CrearGC", content);
                
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GCIndex");
                }
                else
                {
                    return BadRequest("Error al obtener los articulo");
                }
            }
            else
            {
                // Log model state errors for debugging
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Datos del formulario inválidos", errors });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGC([FromForm] GrupoCorreo GC)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(GC);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Marketing/ActualizarGC", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GCIndex");
                }
                else
                {
                    return BadRequest("Error al obtener los articulo");
                }
            }
            else
            {
                // Log model state errors for debugging
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Datos del formulario inválidos", errors });
            }
        }


        [HttpPost]
        public async Task<IActionResult> EliminarGC(string ID)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(ID, Encoding.UTF8, "application/json");
                var resp = await _httpClient.PostAsync($"/api/Marketing/EliminarGC/{ID}", content);
                string responseContent = await resp.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);

                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return NotFound();

            }
            return Ok();
        }


        public async Task<IActionResult> GCIndex()
        {
            var resp = await _httpClient.GetAsync("api/Marketing/ListarGC");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var Correos = JsonConvert.DeserializeObject<IEnumerable<GrupoCorreo>>(content);
                return View(Correos);
            }

            else
            {
                var Correos = new List<Correo>();
                return View(Correos);
            }
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

        public async Task<IActionResult> ListarGrupoCorreos()
        {

            var respArts = await _httpClient.GetAsync("api/Marketing/ListarGC");


            if (respArts.IsSuccessStatusCode)
            {
                var grupocorreoJson = await respArts.Content.ReadAsStringAsync();
                var grupocorreos = JsonConvert.DeserializeObject<List<GrupoCorreo>>(grupocorreoJson);
                return Json(grupocorreos);
            }
            else
            {
                return BadRequest("Error al obtener los grupos de correo");
            }

        }


    }
}
