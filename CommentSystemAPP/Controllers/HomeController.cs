using CommentSystemAPP.Model;
using CommentSystemAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CommentSystemAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Comment> comments = new List<Comment>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7231/api/Comments/GetComments");
            var response = client.GetAsync("GetComments").Result;

            if(response.IsSuccessStatusCode)
            {
             comments = JsonConvert.DeserializeObject<List<Comment>>(response.Content.ReadAsStringAsync().Result);
            }
            ViewBag.Comments = comments;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}