using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;



namespace ADMFacturar.Controllers
{
    public class SeguridadController : Controller
    {
        private readonly HttpClient _httpClient;
        public SeguridadController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7270/api");
        }

        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            {
                var resp = await _httpClient.GetAsync("api/Seguridad/Listar");

                if (resp.IsSuccessStatusCode)
                {
                    var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                    var usuarios = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(content);
                    ViewData["Usuarios"] = usuarios ?? new List<Usuario>();
                    return View("Index");
                }

                return View();
            
        }
    }

        //[Authorize(Roles = "Administrador")]
        public IActionResult CrearSeguridad()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CrearSeguridad(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync("/api/Seguridad/Crear", content);

                // Log the response content for debugging
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
            return View(usuario);
        }

        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarSeguridad(int? PK)
        {
            var resp = await _httpClient.GetAsync($"api/Seguridad/Obtener/{PK}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var usuario = JsonConvert.DeserializeObject<Usuario>(content);
                return View("ActualizarSeguridad", usuario); // Devuelve 'usuario' en lugar de 'resp'
            }

            return NotFound();
        }


        [HttpPost]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarSeguridad(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync("/api/Seguridad/Actualizar", content);

                // Log the response content for debugging
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);

                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
            return View(usuario);
        }

        [HttpPost]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DesactivarSeguridad(string PK)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(PK, Encoding.UTF8, "application/json");
                var resp = await _httpClient.PostAsync($"/api/Seguridad/Desactivar/{PK}", content);
                string responseContent = await resp.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);

                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Devuelve 'Seguridad' en lugar de 'resp'
                }

                return NotFound();

            }
            return Ok();
        }

        public IActionResult Login()
        {
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contra)
        {
            var data = new
            {
                Correo = correo,
                Contra = contra
            };

            // Convertir el objeto a JSON
            var json = JsonConvert.SerializeObject(data);

            // Crear contenido JSON para la solicitud
            var cont = new StringContent(json, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST al API con los parámetros en el cuerpo
            var resp = await _httpClient.PostAsync("api/Seguridad/Login/", cont);

            var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
            var Iusuarios = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(content);
            List<Usuario> usuarios = Iusuarios.ToList();
            Usuario usuario = new Usuario();


            foreach (var u in usuarios)
            {
                usuario.PK_IdUsuario = u.PK_IdUsuario;
                usuario.Nombre = u.Nombre;
                usuario.Correo = u.Correo;
                usuario.Contra = u.Contra;
                usuario.Rol = u.Rol;
            }

            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim("Correo", usuario.Correo),
                    new Claim(ClaimTypes.Role, usuario.Rol)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            } else
            {
                return View();
            }

        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Seguridad");
        }


    }
}
