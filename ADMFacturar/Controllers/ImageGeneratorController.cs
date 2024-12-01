using Microsoft.AspNetCore.Mvc;
using ADM.Service;
using ADM.Models.ApiMonster;
using System.Net.Http;
using ADM.Models;
using Newtonsoft.Json;
using System.Text;

namespace ADMFacturar.Controllers
{
    public class ImageGeneratorController : Controller
    {
        private readonly IMonsterApiService _monsterApiService;
        private readonly IRestrictionService _restrictionService;
        private readonly HttpClient _httpClient;

        public ImageGeneratorController(IMonsterApiService monsterApiService, IRestrictionService restrictionService, IHttpClientFactory httpClientFactory)
        {
            _monsterApiService = monsterApiService;
            _restrictionService = restrictionService;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://admfacturarapi-drg4cmduhxhhh8b7.centralus-01.azurewebsites.net/api");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Galeria()
        {
            var resp = await _httpClient.GetAsync("api/Marketing/ListarIMG");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var imgs = JsonConvert.DeserializeObject<IEnumerable<ImagePrompt>>(content);
                var imgList = imgs ?? new List<ImagePrompt>();
                return View(imgList);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateImg(string prompt)
        {
            var validationResult = _restrictionService.Restriction(prompt);

            if (!validationResult.IsValid)
            {
                ViewBag.Error = validationResult.ErrorMessage;
                ViewBag.ImageResult = null;
                return View("Index");
            }

            try
            {
                // Llama al servicio para generar la imagen
                string statusUrl = await _monsterApiService.GenerateImageAsync(prompt);

                // Espera un momento para que el proceso de generación tenga tiempo de completarse
                await Task.Delay(30000); // 30 segundos de espera, puedes ajustarlo según sea necesario

                // Consulta el estado del proceso
                string imageUrl = await _monsterApiService.GetImageStatusAsync(statusUrl);

                ViewBag.ImageResult = imageUrl;
                ViewBag.Error = null;

                ImagePrompt img = new ImagePrompt
                {
                    Id = 1,
                    Prompt = prompt,
                    Img = imageUrl
                };

                var json = JsonConvert.SerializeObject(img);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/Marketing/CrearIMG", content);
                var es = response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ImageResult = null;
            }

            return View("Index");
        }
    }
}
