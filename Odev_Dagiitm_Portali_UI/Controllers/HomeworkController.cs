using Microsoft.AspNetCore.Mvc;

namespace Odev_Dagiitm_Portali_UI.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public HomeworkController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult AddHomework()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();

        }
        public IActionResult HomeworkSubmission()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();

        }

        public IActionResult MyHomeworks()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();

        }

        [Route("EditHomework/{id}")]
        public IActionResult EditHomework(int id)
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            ViewBag.HomeworkId = id;
            return View();

        }

        public IActionResult StudentHomework()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;

            return View();
        }

        public IActionResult ShowHomework(int id)
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            ViewBag.HomeworkId = id;
            return View();
        }

        public IActionResult AddHomeworkSubmission(int id) {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            ViewBag.HomeworkId = id;
            return View();
        }


        public IActionResult MyHomeworkSubmission()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();
        }

        [Route("EdithomeworkSubmission/{id}")]
        public IActionResult EditHomeworkSubmission(int id)
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            ViewBag.HomeworkSubmissionId = id;
            return View();
      
        }

    }
}
