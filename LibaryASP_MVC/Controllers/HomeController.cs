using LibaryASP_MVC.Models;
using LibaryASP_MVC.Models.ViewModels;
using LibaryASP_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibaryASP_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IitemRepository iitemRepository;
        private readonly IAuthorRepository authorRepository;

        public HomeController(ILogger<HomeController> logger, IitemRepository iitemRepository, IAuthorRepository authorRepository)
        {
            _logger = logger;
            this.iitemRepository = iitemRepository;
            this.authorRepository = authorRepository;
        }

        public async Task<IActionResult> Index()
        {
            //alle items
            var items = await iitemRepository.GetAllAsync();

            //alle authors
            var authors = await authorRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                Items = items,
                Authors = authors
            };
            return View(model);
            

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