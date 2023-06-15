using LibaryASP_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibaryASP_MVC.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IitemRepository iitemRepository;

        public ItemsController(IitemRepository iitemRepository)
        {
            this.iitemRepository = iitemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var items = await iitemRepository.GetByUrlHandleAsync(urlHandle);

            return View(items);
        }
    }
}
